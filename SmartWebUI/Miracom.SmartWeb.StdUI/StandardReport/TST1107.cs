using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Miracom.SmartWeb.FWX;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Miracom.SmartWeb.UI
{
    public partial class TST1107 : Miracom.SmartWeb.FWX.udcUIBase
    {
        public TST1107()
        {
            InitializeComponent();
        }


        private void TST1107_Load(object sender, EventArgs e)
        {
            CmnInitFunction.InitSpread(spdData);
        }

        private void MakeChart(DataTable DT)
        {
            ultraChart1.Visible = true;

            DataTable Table = new DataTable();
            Table = RptUChart.GetData(DT, 0, 1, 1);

            RptUChart.UChartOption.Legend.ShowLegend(ultraChart1, "Right");
            RptUChart.UChartOption.UChartTitle(ultraChart1, "TST1107 " + label3.Text);
            RptUChart.UChartOption.DataPointLabel(ultraChart1, true, "PieChart", "<DATA_VALUE:0>");
            RptUChart.ShowUChart(ultraChart1, Table, "PieChart");
            RptUChart.UChartOption.AxisItemFormat(ultraChart1, "<ITEM_LABEL>", "<DATA_VALUE:0.0>");

            DT.Dispose();
        }

        private string StartDate()
        {
            string FromDate = null;

            if (optYesterday.Checked == true)
            {
                FromDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optLastWeek.Checked == true)
            {
                FromDate = DateTime.Now.AddDays(-(int)(DateTime.Now.DayOfWeek) - 7).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optLastMonth.Checked == true)
            {
                FromDate = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd").Substring(0, 6) + "01";
            }
            else if (optPeriod.Checked == true)
            {
                FromDate = dtpFromDate.Value.ToString("yyyyMMdd");
            }
            else if (optToday.Checked == true)
            {
                FromDate = DateTime.Now.ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optThisWeek.Checked == true)
            {
                FromDate = DateTime.Now.AddDays(-(int)(DateTime.Now.DayOfWeek)).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optThisMonth.Checked == true)
            {
                FromDate = DateTime.Now.ToString("yyyyMMdd").Substring(0, 6) + "01";
            }
            return FromDate + "000000";
        }

        private string EndDate()
        {
            string ToDate = null;

            if (optYesterday.Checked == true)
            {
                ToDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optLastWeek.Checked == true)
            {
                ToDate = DateTime.Now.AddDays(-(int)(DateTime.Now.DayOfWeek) - 1).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optLastMonth.Checked == true)
            {
                ToDate = DateTime.Now.AddDays(-(DateTime.Now.Day)).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optPeriod.Checked == true)
            {
                ToDate = dtpToDate.Value.ToString("yyyyMMdd");
            }
            else if (optToday.Checked == true)
            {
                ToDate = DateTime.Now.ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optThisWeek.Checked == true)
            {
                ToDate = DateTime.Now.AddDays(-(int)(DateTime.Now.DayOfWeek) + 6).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optThisMonth.Checked == true)
            {
                DataTable dt1 = null;               
                
                string temp = DateTime.Now.ToString("yyyyMM").ToString();
                ToDate = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('"+temp+"', 'YYYYMM')), 'YYYYMMDD') ||'215959' FROM DUAL)";

                dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", ToDate);

                temp = dt1.Rows[0][0].ToString();
                return temp;
            }
            return ToDate + "235959";
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (ultraChart1.Visible == false && spdData.Sheets[0].RowCount == 0)
            {
                MessageBox.Show(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage), "STD1606");
                return;
            }

            Control[] obj = null;
            string sImageFileName = null;
            obj = new Control[1];
            obj[0] = spdData;

            sImageFileName = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\myChart.png";
            CmnExcelFunction.ExportToExcelEx(obj, sImageFileName, 1, "Report Log", "", true, false, false, -1, -1);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.OnCloseLayoutForm();
                this.Dispose();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
            }
        }

        private void optPeriod_CheckedChanged(object sender, EventArgs e)
        {
            if (optPeriod.Checked == true)
            {
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
            }
            else
            {
                dtpFromDate.Visible = false;
                dtpToDate.Visible = false;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            string QueryCond = null;
            try
            {
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, StartDate());
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, EndDate());

                spdData_Sheet1.RowCount = 0;
                this.Refresh();
                dt = CmnFunction.oComm.GetFuncDataTable("TST1107", QueryCond);

                if (dt.Rows.Count == 0) { spdData.Visible = false; }
                else { spdData.Visible = true; }

                //SubTotal사용
                CmnSpdFunction.DataBindingWithSubTotal(spdData, dt, 0, 0, 1);

                ultraChart1.Visible = false;
                if (dt.Rows.Count > 0)
                {
                    CmnFunction.FitColumnHeader(spdData);
                    spdData.Sheets[0].Columns[2].Width = 120;
                    MakeChart(dt);
                    ultraChart1.SaveTo(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\myChart.png", System.Drawing.Imaging.ImageFormat.Png);
                    dt.Dispose();
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            finally
            {
                dt.Dispose();
            }
        }

        private void spdData_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            DataTable dtDetail = null;
            string QueryCond = null;

            if (e.Row > 0)
            {
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, Convert.ToString(spdData.Sheets[0].GetValue(e.Row, 0)));
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, StartDate());
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, EndDate());                

                spdDataDetail_Sheet1.RowCount = 0;
                this.Refresh();
                dtDetail = CmnFunction.oComm.GetFuncDataTable("TST1107_1", QueryCond);

               if (dtDetail.Rows.Count > 0) { spdDataDetail.Visible = true; }
                else { spdDataDetail.Visible = false; }

                //spdDataDetail_Sheet1.DataSource = dtDetail;
                //SubTotal사용
                CmnSpdFunction.DataBindingWithSubTotal(spdDataDetail, dtDetail, 0, 1, 3);

                CmnFunction.FitColumnHeader(spdDataDetail);

                spdDataDetail.Sheets[0].Columns[3].Width = 120;

                dtDetail.Dispose();
            }
        }

    }
}
