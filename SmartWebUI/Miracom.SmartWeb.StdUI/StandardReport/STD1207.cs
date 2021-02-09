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
    public partial class STD1207 : Miracom.SmartWeb.FWX.udcUIBase
    {
        public STD1207()
        {
            InitializeComponent();
        }

        private void STD1207_Load(object sender, EventArgs e)
        {
            CmnInitFunction.InitSpread(spdData);
        }

        private void btnView_Click(object sender, EventArgs e)
        {

            DataTable dt = null;
            string QueryCond = null;
            string FromDate="";
            string ToDate="";

            if (CheckField() == false)
            {
                return;
            }
            if (optYesterday.Checked == true)
            {
                FromDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd").Substring(0, 8);
                ToDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optLastWeek.Checked == true)
            {
                FromDate = DateTime.Now.AddDays(-(int)(DateTime.Now.DayOfWeek) - 7).ToString("yyyyMMdd").Substring(0, 8);
                ToDate = DateTime.Now.AddDays(-(int)(DateTime.Now.DayOfWeek) - 1).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optLastMonth.Checked == true)
            {
                FromDate = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd").Substring(0, 6) + "01";
                ToDate = DateTime.Now.AddDays(-(DateTime.Now.Day)).ToString("yyyyMMdd").Substring(0, 8);                
            }
            else if (optPeriod.Checked == true)
            {
                FromDate = dtpFromDate.Value.ToString("yyyyMMdd");
                ToDate = dtpToDate.Value.ToString("yyyyMMdd");
            }
            else if (optToday.Checked == true)
            {
                FromDate = DateTime.Now.ToString("yyyyMMdd").Substring(0, 8);
                ToDate = DateTime.Now.ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optThisWeek.Checked == true)
            {
                FromDate = DateTime.Now.AddDays(-(int)(DateTime.Now.DayOfWeek)).ToString("yyyyMMdd").Substring(0, 8);
                ToDate = DateTime.Now.AddDays(-(int)(DateTime.Now.DayOfWeek) + 6).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optThisMonth.Checked == true)
            {
                FromDate = DateTime.Now.ToString("yyyyMMdd").Substring(0, 6) + "01";
                ToDate = DateTime.Now.ToString("yyyyMMdd").Substring(0, 6) + "31";
            }

            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvOper.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, FromDate);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, ToDate);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvMat.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvMatVer.Text);


            spdData_Sheet1.RowCount=0;
            this.Refresh();
            dt = CmnFunction.oComm.GetFuncDataTable("STD1207", QueryCond);
            if (dt.Rows.Count == 0) { spdData.Visible = false; }
            else { spdData.Visible = true; }
            spdData_Sheet1.DataSource = dt;
            ultraChart1.Visible = false;
            if (dt.Rows.Count > 0)
            {
                CmnFunction.FitColumnHeader(spdData);
                MakeChart(dt);
                ultraChart1.SaveTo(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\myChart.png", System.Drawing.Imaging.ImageFormat.Png);
            }
            dt.Dispose();

        }

        private Boolean CheckField()
        {
            Boolean Check = false;

            if (cdvFactory.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1207");
                Check = false;
            }
            else if (cdvOper.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage), "STD1207");
                Check = false;
            }
            else if (cdvMat.Text.TrimEnd() != "")
            {
                if (cdvMatVer.Text.TrimEnd() == "")
                {
                    MessageBox.Show(RptMessages.GetMessage("STD011", GlobalVariable.gcLanguage), "STD1207");
                    Check = false;
                }
                Check = true;
            }
            else { Check = true; }

            return Check;
        }

        private void MakeChart(DataTable DT)
        {
            ultraChart1.Visible = true;

            DataTable Table = new DataTable();
            Table = RptUChart.GetData(DT, 0, 1, 1);

            RptUChart.UChartOption.Legend.ShowLegend(ultraChart1, "Right");
            RptUChart.UChartOption.UChartTitle(ultraChart1, "STD1207 " + label3.Text);
            RptUChart.UChartOption.DataPointLabel(ultraChart1, true, "ColumnChart", "<DATA_VALUE:0>");
            RptUChart.ShowUChart(ultraChart1, Table, "ColumnChart");
            RptUChart.UChartOption.AxisItemFormat(ultraChart1, "", "<DATA_VALUE:0.0>");

            DT.Dispose();
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (ultraChart1.Visible == false && spdData.Sheets[0].RowCount == 0)
            {
                MessageBox.Show(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage), "STD1207");
                return;
            }

            Control[] obj = null;
            string sImageFileName = null;
            obj = new Control[1];
            obj[0] = spdData;

            sImageFileName = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\myChart.png";
            CmnExcelFunction.ExportToExcelEx(obj, sImageFileName, 1, "Production Output", "", true, false, false, -1, -1);
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

        private void cdvFactory_ButtonPress(object sender, EventArgs e)
        {
            cdvFactory.Init();
            CmnInitFunction.InitListView(cdvFactory.GetListView);
            cdvFactory.Columns.Add("Factory", 50, HorizontalAlignment.Left);
            cdvFactory.Columns.Add("Desc", 100, HorizontalAlignment.Left);
            cdvFactory.SelectedSubItemIndex = 0;

            RptListFunction.ViewFactoryList(cdvFactory.GetListView);
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

        private void cdvOper_ButtonPress(object sender, EventArgs e)
        {
            cdvOper.Init();
            CmnInitFunction.InitListView(cdvOper.GetListView);
            cdvOper.Columns.Add("KEY_1", 30, HorizontalAlignment.Left);
            cdvOper.Columns.Add("DATA_1", 70, HorizontalAlignment.Left);
            cdvOper.SelectedSubItemIndex = 0;

            RptListFunction.ViewOperList(cdvOper.GetListView, cdvFactory.Text, "");
        }

        private void cdvMat_ButtonPress(object sender, EventArgs e)
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1207");
                return;
            }
            cdvMat.Init();
            CmnInitFunction.InitListView(cdvMat.GetListView);
            cdvMat.Columns.Add("KEY_1", 30, HorizontalAlignment.Left);
            cdvMat.Columns.Add("DATA_1", 70, HorizontalAlignment.Left);
            cdvMat.SelectedSubItemIndex = 0;

            RptListFunction.ViewMatList(cdvMat.GetListView, cdvFactory.Text, "");
        }

        private void cdvMatVer_ButtonPress(object sender, EventArgs e)
        {

            if (cdvFactory.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1207");
                return;
            }
            else if (cdvMat.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD006", GlobalVariable.gcLanguage), "STD1207");
                return;
            }
            cdvMatVer.Init();
            CmnInitFunction.InitListView(cdvMatVer.GetListView);
            cdvMatVer.Columns.Add("MAT_VER", 30, HorizontalAlignment.Left);
            cdvMatVer.SelectedSubItemIndex = 0;

            RptListFunction.ViewMatVerList(cdvMatVer.GetListView, cdvFactory.Text, cdvMat.Text);
        }

        private void cdvMat_TextBoxTextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            if (cdvMat.Text != "")
            {
                dt = RptListFunction.MaxMaterialVer(cdvFactory.Text, cdvMat.Text);
                cdvMatVer.Text = Convert.ToString(dt.Rows[0].ItemArray[0]);
            }
            else
            {
                cdvMatVer.Text = "";
            }
        }
    }
}