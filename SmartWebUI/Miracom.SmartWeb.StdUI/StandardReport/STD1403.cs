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
    public partial class STD1403 : Miracom.SmartWeb.FWX.udcUIBase
    {
        public STD1403()
        {
            InitializeComponent();
        }

        private void STD1403_Load(object sender, EventArgs e)
        {
            CmnInitFunction.InitSpread(spdData);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string QueryCond = null;
            string FromDate="";
            string ToDate="";
                        
            CheckField();
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
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvRes.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, FromDate);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, ToDate);
            
            spdData_Sheet1.RowCount=0;
            this.Refresh();
            dt = CmnFunction.oComm.GetFuncDataTable("STD1403", QueryCond);

            ultraChart1.Visible = false;

            if (dt.Rows.Count != 0)
            {
                spdData.Visible = true;
                MakeSpread(dt);
                dt.Dispose();  
                DataTable DetailDT = new DataTable();
                DetailDT = CmnFunction.oComm.GetFuncDataTable("STD1403_1", QueryCond);
                if (DetailDT.Rows.Count == 0) 
                {
                    spdData.Sheets[0].ColumnCount = 0;
                }
                else 
                {
                    FillSpread(DetailDT);
                    MakeDataTable();
                    
                }
                DetailDT.Dispose();      
            }
            else
            {
                spdData.Visible = false;
                spdData_Sheet1.DataSource = dt;
                dt.Dispose();  
            }

            CmnFunction.FitColumnHeader(spdData);
        }

        private Boolean CheckField()
        {
            Boolean Check = false;

            if (cdvFactory.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1403");
                Check = false;
            }
            else { Check = true; }

            return Check;
        }

        private void MakeChart(DataTable DT)
        {
            ultraChart1.Visible = true;

            DataTable Table = new DataTable();
            Table = RptUChart.GetData(DT, 0, 2, 1);

            RptUChart.UChartOption.Legend.ShowLegend(ultraChart1, "Right");
            RptUChart.UChartOption.UChartTitle(ultraChart1, "STD1403 " + label3.Text);
            RptUChart.UChartOption.DataPointLabel(ultraChart1, true, "ColumnChart", "<DATA_VALUE:0>");
            RptUChart.ShowUChart(ultraChart1, Table, "ColumnChart");
            RptUChart.UChartOption.AxisItemFormat(ultraChart1, "", "<DATA_VALUE:0.0>");

            DT.Dispose();
        }

        private void MakeDataTable()
        {
            DataTable ChartDT = new DataTable();
            int i;
            int j;

            for (i = 0; i < 6; i++)
            {
                DataColumn Col = new DataColumn();

                Col.ReadOnly = false;
                Col.Unique = false;

                if (i == 0)
                {
                    Col.ColumnName = "Resource";
                    Col.DataType = System.Type.GetType("System.String");
                }
                else if (i == 1)
                {
                    Col.ColumnName = "Desc";
                    Col.DataType = System.Type.GetType("System.String");
                }
                else if (i == 2)
                {
                    Col.ColumnName = "Up PROC";
                    Col.DataType = System.Type.GetType("System.String");
                }
                else if (i == 3)
                {
                    Col.ColumnName = "Up WAIT";
                    Col.DataType = System.Type.GetType("System.String");
                }
                else if (i == 4)
                {
                    Col.ColumnName = "Down PROC";
                    Col.DataType = System.Type.GetType("System.String");
                }
                else if (i == 5)
                {
                    Col.ColumnName = "Down WAIT";
                    Col.DataType = System.Type.GetType("System.String");
                }
                ChartDT.Columns.Add(Col);
            }

            for (i = 0; i < spdData.Sheets[0].RowCount; i++)
            {
                DataRow Row = null;
                string Value = null;

                Row = ChartDT.NewRow();
                Row[0] = Convert.ToString( spdData.Sheets[0].GetValue(i, 0));
                ChartDT.Rows.Add(Row);

                for (j = 1; j < spdData.Sheets[0].ColumnCount; j++)
                {
                    Value = Convert.ToString(spdData.Sheets[0].GetValue(i, j));
                    ChartDT.Rows[i][j] = (Value == "" || Value == null ? "0" : Value);
                }
            }
            MakeChart(ChartDT);
            ultraChart1.SaveTo(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\myChart.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        private void MakeSpread(DataTable RetDT)
        {
            int i;
            string RES_PRI_STS;

            spdData.Sheets[0].ColumnCount = (RetDT.Rows.Count) * 2 + 2;
            
            spdData.Sheets[0].ColumnHeader.Cells[0, 0].Text = "Resource";
            spdData.Sheets[0].ColumnHeader.Cells[0, 0].RowSpan = 2;
            spdData.Sheets[0].ColumnHeader.Cells[0, 1].RowSpan = 2;
            spdData.Sheets[0].ColumnHeader.Cells[0, 1].Text = "Desc";
            spdData.Sheets[0].ColumnHeader.Cells[0, 2].ColumnSpan = RetDT.Rows.Count;
            spdData.Sheets[0].ColumnHeader.Cells[0, 2 + RetDT.Rows.Count].ColumnSpan = RetDT.Rows.Count;
            spdData.Sheets[0].ColumnHeader.Cells[0, 2].Text = "Up(Sec)";
            spdData.Sheets[0].ColumnHeader.Cells[0, 2 + RetDT.Rows.Count].Text = "Down(Sec)";

            for (i = 0; i < RetDT.Rows.Count; i++)
            {
                RES_PRI_STS = Convert.ToString(RetDT.Rows[i].ItemArray[0]).TrimEnd();
                spdData.Sheets[0].ColumnHeader.Cells[1, i + 2].Text = (RES_PRI_STS == "" ? " " : RES_PRI_STS);
            }
            for (i = 0; i < RetDT.Rows.Count; i++)
            {
                RES_PRI_STS = Convert.ToString(RetDT.Rows[i].ItemArray[0]).TrimEnd();
                spdData.Sheets[0].ColumnHeader.Cells[1, i + RetDT.Rows.Count + 2].Text = (RES_PRI_STS == "" ? " " : RES_PRI_STS);
            }
            RetDT.Dispose();
        }

        private void FillSpread(DataTable RetDT)
        {
            int i;
            int j;
            double total;
            int iIndex;
            string CurrentRow = null;
            string NewRow = null;

            iIndex = 0;

            spdData.Sheets[0].RowCount = 0;

            for (i = 0; i < RetDT.Rows.Count; i++)
            {
                NewRow = Convert.ToString(RetDT.Rows[i].ItemArray[0]);

                if (i == 0 || CurrentRow != NewRow)
                {
                    spdData.Sheets[0].RowCount += 1;
                    CurrentRow = NewRow;
                    if (i != 0) { iIndex += 1; }
                }
                spdData.Sheets[0].SetValue(iIndex, 0, RetDT.Rows[i][0]);
                spdData.Sheets[0].SetValue(iIndex, 1, RetDT.Rows[i][1]);

                for (j = 2; j < spdData.Sheets[0].ColumnCount; j++)
                {
                    if (j < 4 && Convert.ToString(RetDT.Rows[i][2]) == "U")
                    {
                        if (spdData.Sheets[0].ColumnHeader.Cells[1, j].Text.TrimEnd() == Convert.ToString(RetDT.Rows[i][3]))
                        {
                            spdData.Sheets[0].SetValue(iIndex, j, RetDT.Rows[i][4]);
                        }
                    }
                    if (j >= 4 && Convert.ToString(RetDT.Rows[i][2]) == "D")
                    {
                        if (spdData.Sheets[0].ColumnHeader.Cells[1, j].Text.TrimEnd() == Convert.ToString(RetDT.Rows[i][3]))
                        {
                            spdData.Sheets[0].SetValue(iIndex, j, RetDT.Rows[i][4]);
                        }
                    }
                }
            }

            RetDT.Dispose();

            spdData.Sheets[0].RowCount += 1;
            spdData.Sheets[0].SetValue(spdData.Sheets[0].RowCount - 1, 0, "Total");

            for (i = 2; i < spdData.Sheets[0].ColumnCount; i++)
            {
                total = 0;
                for (j = 0; j < spdData.Sheets[0].RowCount - 1; j++)
                {
                    total += Convert.ToDouble(spdData.Sheets[0].Cells[j, i].Value);
                }
                spdData.Sheets[0].Cells[spdData.Sheets[0].RowCount - 1, i].Value = total;
            }
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (ultraChart1.Visible == false && spdData.Sheets[0].RowCount == 0)
            {
                MessageBox.Show(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage), "STD1403");
                return;
            }

            Control[] obj = null;
            string sImageFileName = null;
            obj = new Control[1];
            obj[0] = spdData;

            sImageFileName = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\myChart.png";
            CmnExcelFunction.ExportToExcelEx(obj, sImageFileName, 1, "Productivity", "", true, false, false, -1, -1);
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
              
        private void cdvRes_ButtonPress(object sender, EventArgs e)
        {
            cdvRes.Init();
            CmnInitFunction.InitListView(cdvRes.GetListView);
            cdvRes.Columns.Add("Factory", 50, HorizontalAlignment.Left);
            cdvRes.Columns.Add("Desc", 100, HorizontalAlignment.Left);
            cdvRes.SelectedSubItemIndex = 0;

            RptListFunction.ViewResourceList(cdvRes.GetListView, cdvFactory.Text.TrimEnd());
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

        private void ultraChart1_ChartDataClicked(object sender, Infragistics.UltraChart.Shared.Events.ChartDataEventArgs e)
        {

        }
    }
}