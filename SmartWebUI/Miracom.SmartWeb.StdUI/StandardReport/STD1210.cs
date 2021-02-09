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
    public partial class STD1210 : Miracom.SmartWeb.FWX.udcUIBase
    {
        public STD1210()
        {
            InitializeComponent();
        }

        private void STD1210_Load(object sender, EventArgs e)
        {
            CmnInitFunction.InitSpread(spdData);
        }

        private void btnView_Click(object sender, EventArgs e)
        {

            DataTable dt = null;
            string QueryCond = null;
            string FromDate="";
            string ToDate="";

            if (CheckField() == false) { return; }
            if (optYesterday.Checked == true)
            {
                FromDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd").Substring(0, 8);
                ToDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optLastWeek.Checked == true)
            {
                FromDate = DateTime.Now.AddDays(-(int)(DateTime.Now.DayOfWeek) - 7).ToString("yyyyMMdd").Substring(0, 8);
                ToDate =DateTime.Now.AddDays(-(int)(DateTime.Now.DayOfWeek) - 1).ToString("yyyyMMdd").Substring(0, 8);
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
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, FromDate);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, ToDate);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvMat.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvOperGrpList.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtOperGrp.Text);
            
            if (optLot.Checked == true)
            {
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, "0");
            }
            else if (optQty1.Checked == true)
            {
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, "1");
            }
            else if (optQty2.Checked == true)
            {
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, "2");
            }
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvMatVer.Text);

            spdData_Sheet1.RowCount=0;
            this.Refresh();
            dt = CmnFunction.oComm.GetFuncDataTable("STD1210", QueryCond);
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
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1210");
                Check = false;
            }
            else if (cdvMat.Text.TrimEnd() != "")
            {
                if (cdvMatVer.Text.TrimEnd() == "")
                {
                    MessageBox.Show(RptMessages.GetMessage("STD011", GlobalVariable.gcLanguage), "STD1210");
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
            Table = RptUChart.GetDataColKey(DT, 0, 1, 1);

            RptUChart.UChartOption.Legend.ShowLegend(ultraChart1, "Right");
            RptUChart.UChartOption.UChartTitle(ultraChart1, "STD1210 " + label3.Text);
            RptUChart.UChartOption.DataPointLabel(ultraChart1, true, "LineChart", "<DATA_VALUE:0>");
            RptUChart.ShowUChart(ultraChart1, Table, "LineChart");
            RptUChart.UChartOption.AxisItemFormat(ultraChart1, "<ITEM_LABEL>", "<DATA_VALUE:0.0>");

            DT.Dispose();
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (ultraChart1.Visible == false && spdData.Sheets[0].RowCount == 0)
            {
                MessageBox.Show(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage), "STD1210");
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

        private void cdvOperGrp_ButtonPress(object sender, EventArgs e)
        {
            cdvOperGrp.Init();
            CmnInitFunction.InitListView(cdvOperGrp.GetListView);
            cdvOperGrp.Columns.Add("Prompt", 30, HorizontalAlignment.Left);
            cdvOperGrp.Columns.Add("Group", 70, HorizontalAlignment.Left);
            cdvOperGrp.SelectedSubItemIndex = 0;

            RptListFunction.ViewMATGroupList(cdvOperGrp.GetListView, cdvFactory.Text, "GRP_OPER");
        }

        private void cdvOperGrp_SelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            txtOperGrp.Text = cdvOperGrp.Items[cdvOperGrp.SelectedItem.Index].SubItems[1].Text;
        }

        private void cdvOperGrp_TextBoxTextChanged(object sender, EventArgs e)
        {
            cdvOperGrpList.Text = "";
            if (cdvOperGrp.Text.TrimEnd() == "")
            {
                txtOperGrp.Text = "";
            }
        }

        private void cdvOperGrpList_ButtonPress(object sender, EventArgs e)
        {
            if (cdvOperGrp.Text.TrimEnd() == "")
            {
                cdvOperGrpList.ListClear();
                return;
            }
            cdvOperGrpList.Init();
            CmnInitFunction.InitListView(cdvOperGrpList.GetListView);
            cdvOperGrpList.Columns.Add("KEY_1", 30, HorizontalAlignment.Left);
            cdvOperGrpList.Columns.Add("DATA_1", 70, HorizontalAlignment.Left);
            cdvOperGrpList.SelectedSubItemIndex = 0;

            string OperGrp = "";
            if (txtOperGrp.Text == "1") OperGrp = "OPER_GRP_1";
            else if (txtOperGrp.Text == "2") OperGrp = "OPER_GRP_2";
            else if (txtOperGrp.Text == "3") OperGrp = "OPER_GRP_3";
            else if (txtOperGrp.Text == "4") OperGrp = "OPER_GRP_4";
            else if (txtOperGrp.Text == "5") OperGrp = "OPER_GRP_5";
            else if (txtOperGrp.Text == "6") OperGrp = "OPER_GRP_6";
            else if (txtOperGrp.Text == "7") OperGrp = "OPER_GRP_7";
            else if (txtOperGrp.Text == "8") OperGrp = "OPER_GRP_8";
            else if (txtOperGrp.Text == "9") OperGrp = "OPER_GRP_9";
            else if (txtOperGrp.Text == "10") OperGrp = "OPER_GRP_10";
            else if (txtOperGrp.Text == "11") OperGrp = "OPER_GRP_11";
            else if (txtOperGrp.Text == "12") OperGrp = "OPER_GRP_12";
            else if (txtOperGrp.Text == "13") OperGrp = "OPER_GRP_13";
            else if (txtOperGrp.Text == "14") OperGrp = "OPER_GRP_14";
            else if (txtOperGrp.Text == "15") OperGrp = "OPER_GRP_15";
            else if (txtOperGrp.Text == "16") OperGrp = "OPER_GRP_16";
            else if (txtOperGrp.Text == "17") OperGrp = "OPER_GRP_17";
            else if (txtOperGrp.Text == "18") OperGrp = "OPER_GRP_18";
            else if (txtOperGrp.Text == "19") OperGrp = "OPER_GRP_19";
            else if (txtOperGrp.Text == "20") OperGrp = "OPER_GRP_20";

            RptListFunction.ViewGCMTableList(cdvOperGrpList.GetListView, cdvFactory.Text, OperGrp);
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

        private void cdvMat_ButtonPress(object sender, EventArgs e)
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1210");
                return;
            }
            cdvMat.Init();
            CmnInitFunction.InitListView(cdvMat.GetListView);
            cdvMat.Columns.Add("KEY_1", 30, HorizontalAlignment.Left);
            cdvMat.Columns.Add("DATA_1", 70, HorizontalAlignment.Left);
            cdvMat.SelectedSubItemIndex = 0;

            RptListFunction.ViewMatList(cdvMat.GetListView, cdvFactory.Text, "");
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

        private void cdvMatVer_ButtonPress(object sender, EventArgs e)
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1210");
                return;
            }
            else if (cdvMat.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD006", GlobalVariable.gcLanguage), "STD1210");
                return;
            }
            cdvMatVer.Init();
            CmnInitFunction.InitListView(cdvMatVer.GetListView);
            cdvMatVer.Columns.Add("MAT_VER", 30, HorizontalAlignment.Left);
            cdvMatVer.SelectedSubItemIndex = 0;

            RptListFunction.ViewMatVerList(cdvMatVer.GetListView, cdvFactory.Text, cdvMat.Text);
        }
    }
}