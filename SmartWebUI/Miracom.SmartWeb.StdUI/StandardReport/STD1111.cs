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
    public partial class STD1111 : Miracom.SmartWeb.FWX.udcUIBase
    {
        public STD1111()
        {
            InitializeComponent();
        }

        private void STD1111_Load(object sender, EventArgs e)
        {
            CmnInitFunction.InitSpread(spdData);
            CmnInitFunction.InitSpread(spdDataDetail);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            string QueryCond = null;

            CheckField();
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvLotType.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvMatType.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvMatGrpList.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtMatGrp.Text);
                        
            spdData_Sheet1.RowCount=0;
            this.Refresh();
            dt = CmnFunction.oComm.GetFuncDataTable("STD1111", QueryCond);
            if (dt.Rows.Count == 0){spdData.Visible = false;}
            else{spdData.Visible = true;}
            spdDataDetail.Visible = false;
            spdDataDetail.Sheets[0].RowCount = 0;
            spdDataDetail.Sheets[0].ColumnCount = 0;
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
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1111");
                Check = false;
            }
            else { Check = true; }

            return Check;
        }


        private void MakeChart(DataTable DT)
        {
            ultraChart1.Visible = true;

            DataTable Table = new DataTable();
            if (optLot.Checked == true) { Table = RptUChart.GetData(DT, 0, 3, 3); }
            else if (optQty1.Checked == true) { Table = RptUChart.GetData(DT, 0, 4, 3); }
            else if (optQty2.Checked == true) { Table = RptUChart.GetData(DT, 0, 5, 3); }

            RptUChart.UChartOption.Legend.ShowLegend(ultraChart1, "Right");
            RptUChart.UChartOption.UChartTitle(ultraChart1, "STD1111 " + label3.Text);
            RptUChart.UChartOption.DataPointLabel(ultraChart1, true, "ColumnChart", "<DATA_VALUE:0>");
            RptUChart.ShowUChart(ultraChart1, Table, "ColumnChart");
            RptUChart.UChartOption.AxisItemFormat(ultraChart1, "", "<DATA_VALUE:0.0>");

            DT.Dispose();
        }
        
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (ultraChart1.Visible == false && spdData.Sheets[0].RowCount == 0)
            {
                MessageBox.Show(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage), "STD1111");
                return;
            }
            
            Control[] obj = null;
            string sImageFileName = null;
            obj = new Control[2];
            obj[0] = spdData;
            obj[1] = spdDataDetail;

            sImageFileName = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\myChart.png";
            CmnExcelFunction.ExportToExcelEx(obj, sImageFileName, 1, "WIP Status", "", true, false, false, -1, -1);
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

       
        private void cdvLotType_ButtonPress(object sender, EventArgs e)
        {
            cdvLotType.Init();
            CmnInitFunction.InitListView(cdvLotType.GetListView);
            cdvLotType.Columns.Add("KEY_1", 30, HorizontalAlignment.Left);
            cdvLotType.Columns.Add("DATA_1", 70, HorizontalAlignment.Left);
            cdvLotType.SelectedSubItemIndex = 0;

            RptListFunction.ViewGCMTableList(cdvLotType.GetListView, cdvFactory.Text, "LOT_TYPE" );
        }

        private void cdvMatGrp_ButtonPress(object sender, EventArgs e)
        {
            cdvMatGrp.Init();
            CmnInitFunction.InitListView(cdvMatGrp.GetListView);
            cdvMatGrp.Columns.Add("Prompt", 30, HorizontalAlignment.Left);
            cdvMatGrp.Columns.Add("Group", 70, HorizontalAlignment.Left);
            cdvMatGrp.SelectedSubItemIndex = 0;

            RptListFunction.ViewMATGroupList(cdvMatGrp.GetListView, cdvFactory.Text, "GRP_MATERIAL");
        }

        private void cdvMatType_ButtonPress(object sender, EventArgs e)
        {
            cdvMatType.Init();
            CmnInitFunction.InitListView(cdvMatGrp.GetListView);
            cdvMatType.Columns.Add("KEY_1", 30, HorizontalAlignment.Left);
            cdvMatType.Columns.Add("DATA_1", 70, HorizontalAlignment.Left);
            cdvMatType.SelectedSubItemIndex = 0;

            RptListFunction.ViewMATTypeList(cdvMatType.GetListView, cdvFactory.Text, "MATERIAL_TYPE");
        }

        private void cdvMatGrpList_ButtonPress(object sender, EventArgs e)
        {
            if (cdvMatGrp.Text.TrimEnd() == "")
            {
                cdvMatGrpList.ListClear();
                return;
            }
            cdvMatGrpList.Init();
            CmnInitFunction.InitListView(cdvMatGrpList.GetListView);
            cdvMatGrpList.Columns.Add("KEY_1", 30, HorizontalAlignment.Left);
            cdvMatGrpList.Columns.Add("DATA_1", 70, HorizontalAlignment.Left);
            cdvMatGrpList.SelectedSubItemIndex = 0;
            
            string MatGrp = "";
            if (txtMatGrp.Text == "1") MatGrp = "MATERIAL_GRP_1";
            else if (txtMatGrp.Text == "2") MatGrp = "MATERIAL_GRP_2";
            else if (txtMatGrp.Text == "3") MatGrp = "MATERIAL_GRP_3";
            else if (txtMatGrp.Text == "4") MatGrp = "MATERIAL_GRP_4";
            else if (txtMatGrp.Text == "5") MatGrp = "MATERIAL_GRP_5";
            else if (txtMatGrp.Text == "6") MatGrp = "MATERIAL_GRP_6";
            else if (txtMatGrp.Text == "7") MatGrp = "MATERIAL_GRP_7";
            else if (txtMatGrp.Text == "8") MatGrp = "MATERIAL_GRP_8";
            else if (txtMatGrp.Text == "9") MatGrp = "MATERIAL_GRP_9";
            else if (txtMatGrp.Text == "10") MatGrp = "MATERIAL_GRP_10";
            else if (txtMatGrp.Text == "11") MatGrp = "MATERIAL_GRP_11";
            else if (txtMatGrp.Text == "12") MatGrp = "MATERIAL_GRP_12";
            else if (txtMatGrp.Text == "13") MatGrp = "MATERIAL_GRP_13";
            else if (txtMatGrp.Text == "14") MatGrp = "MATERIAL_GRP_14";
            else if (txtMatGrp.Text == "15") MatGrp = "MATERIAL_GRP_15";
            else if (txtMatGrp.Text == "16") MatGrp = "MATERIAL_GRP_16";
            else if (txtMatGrp.Text == "17") MatGrp = "MATERIAL_GRP_17";
            else if (txtMatGrp.Text == "18") MatGrp = "MATERIAL_GRP_18";
            else if (txtMatGrp.Text == "19") MatGrp = "MATERIAL_GRP_19";
            else if (txtMatGrp.Text == "20") MatGrp = "MATERIAL_GRP_20";

            RptListFunction.ViewGCMTableList(cdvMatGrpList.GetListView, cdvFactory.Text, MatGrp);
        }

        private void cdvMatGrp_SelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            txtMatGrp.Text = cdvMatGrp.Items[cdvMatGrp.SelectedItem.Index].SubItems[1].Text;                         
        }

        private void cdvMatGrp_TextBoxTextChanged(object sender, EventArgs e)
        {
            cdvMatGrpList.Text = "";
            if (cdvMatGrp.Text.TrimEnd() == "")
            {
                txtMatGrp.Text = "";
            }
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            DataTable dtDetail = null;
            string QueryCond = null;

            if (e.Row >= 0)
            {
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, Convert.ToString(spdData.Sheets[0].GetValue(e.Row, 0)));
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, Convert.ToString(spdData.Sheets[0].GetValue(e.Row, 1)));

                spdDataDetail_Sheet1.RowCount = 0;
                this.Refresh();
                dtDetail = CmnFunction.oComm.GetFuncDataTable("STD1111_1", QueryCond);

                if (dtDetail.Rows.Count > 0) { spdDataDetail.Visible = true; }
                else { spdDataDetail.Visible = false; }
                spdDataDetail_Sheet1.DataSource = dtDetail;
                CmnFunction.FitColumnHeader(spdDataDetail);
                dtDetail.Dispose();
            }
        }

        private void spdDataDetail_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            frmLotHistory VLTH = new frmLotHistory(spdDataDetail.ActiveSheet.GetValue(spdDataDetail.ActiveSheet.ActiveRowIndex, 0).ToString());
            VLTH.ShowDialog();
        }
    }
}