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
    public partial class STD1110 : Miracom.SmartWeb.FWX.udcUIBase
    {
        public STD1110()
        {
            InitializeComponent();
        }

        private void STD1110_Load(object sender, EventArgs e)
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
            
            spdData_Sheet1.RowCount=0;
            this.Refresh();
            dt = CmnFunction.oComm.GetFuncDataTable("STD1110", QueryCond);
            if (dt.Rows.Count == 0) { spdData.Visible = false;}
            else {spdData.Visible = true;}
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
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1110");
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
            RptUChart.UChartOption.UChartTitle(ultraChart1, "STD1110 " + label3.Text);
            RptUChart.UChartOption.DataPointLabel(ultraChart1, true, "ColumnChart", "<DATA_VALUE:0>");
            RptUChart.ShowUChart(ultraChart1, Table, "ColumnChart");
            RptUChart.UChartOption.AxisItemFormat(ultraChart1, "", "<DATA_VALUE:0.0>");

            DT.Dispose();
        }
        
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (ultraChart1.Visible == false && spdData.Sheets[0].RowCount == 0)
            {
                MessageBox.Show(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage), "STD1110");
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

        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            DataTable dtDetail = null;
            string QueryCond = null;

            if (e.Row >= 0)
            {
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, Convert.ToString(spdData.Sheets[0].GetValue(e.Row, 0)));

                spdDataDetail_Sheet1.RowCount = 0;
                this.Refresh();
                dtDetail = CmnFunction.oComm.GetFuncDataTable("STD1110_1", QueryCond);

                if (dtDetail.Rows.Count > 0)
                {
                    spdDataDetail.Visible = true;
                    spdDataDetail_Sheet1.DataSource = dtDetail;
                    CmnFunction.FitColumnHeader(spdDataDetail);
                    spdDataDetail_Sheet1.Columns[10].Width = 0;
                    spdDataDetail_Sheet1.Columns[11].Width = 0;
                    spdDataDetail_Sheet1.Columns[12].Width = 0;
                }
                else
                {
                    spdDataDetail.Visible = false;
                    spdDataDetail_Sheet1.DataSource = dtDetail;
                }
                dtDetail.Dispose();
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

                spdDataDetail_Sheet1.RowCount = 0;
                this.Refresh();
                dtDetail = CmnFunction.oComm.GetFuncDataTable("STD1110_1", QueryCond);

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