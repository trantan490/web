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
    public partial class STD1608 : Miracom.SmartWeb.FWX.udcUIBase
    {
        public STD1608()
        {
            InitializeComponent();
        }

        private void STD1608_Load(object sender, EventArgs e)
        {
            CmnInitFunction.InitSpread(spdData);
            CmnInitFunction.InitSpread(spdDetail);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable RetDT = null;
            DataTable ReRetDT = new DataTable();
            string QueryCond = null;

            if (CheckField() == false) return;

            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvOper.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtLotID.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, dtpFromDate.Value.ToString("yyyyMMdd"));
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, dtpToDate.Value.ToString("yyyyMMdd"));

            spdData_Sheet1.RowCount = 0;
            this.Refresh();

            RetDT = CmnFunction.oComm.GetFuncDataTable("STD1608", QueryCond);

            if (RetDT.Rows.Count == 0) { spdData.Visible = false; }
            else
            {
                spdData.Visible = true;

                for (int i = 0; i < RetDT.Columns.Count; i++)
                {
                    DataColumn DetailCol = new DataColumn();

                    DetailCol.ReadOnly = false;
                    DetailCol.Unique = false;

                    DetailCol.ColumnName = RetDT.Columns[i].ColumnName;
                    DetailCol.DataType = System.Type.GetType("System.String");

                    ReRetDT.Columns.Add(DetailCol);
                }

                //New DataTable Value Á¤ÀÇ
                for (int i = 0; i < RetDT.Rows.Count; i++)
                {
                    DataRow OperRow = null;
                    OperRow = ReRetDT.NewRow();
                    OperRow[0] = RetDT.Rows[i].ItemArray[0];
                    ReRetDT.Rows.Add(OperRow);
                    for (int j = 1; j < RetDT.Columns.Count; j++)
                    {
                        if (j == 4 || j == 5)
                        {
                            ReRetDT.Rows[i][j] = CmnFunction.ToDate(RetDT.Rows[i].ItemArray[j].ToString().Trim());
                        }
                        else
                        {
                            if (j == 6)
                            {
                                ReRetDT.Rows[i][j] = CmnFunction.ToDbl(RetDT.Rows[i].ItemArray[j]).ToString("#########0.####");
                            }
                            else
                            {
                                ReRetDT.Rows[i][j] = RetDT.Rows[i].ItemArray[j];
                            }
                        }
                    }

                }

                spdData_Sheet1.DataSource = ReRetDT;
            }

            spdData_Sheet1.SetColumnAllowAutoSort(0, spdData_Sheet1.ColumnCount, true);
            CmnFunction.FitColumnHeader(spdData);
            CmnInitFunction.InitLockSpread(spdData);

            RetDT.Dispose();
        }

        private Boolean CheckField()
        {
            Boolean Check = false;

            if (cdvFactory.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1608");
                Check = false;
            }
            else { Check = true; }

            return Check;
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {

            Control[] obj = null;
            obj = new Control[1];
            obj[0] = spdData;

            CmnExcelFunction.ExportToExcelEx(obj, null, 1, "Production Output", "", true, false, false, -1, -1);
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

        private void cdvOper_ButtonPress(object sender, EventArgs e)
        {
            cdvOper.Init();
            CmnInitFunction.InitListView(cdvOper.GetListView);
            cdvOper.Columns.Add("KEY_1", 30, HorizontalAlignment.Left);
            cdvOper.Columns.Add("DATA_1", 70, HorizontalAlignment.Left);
            cdvOper.SelectedSubItemIndex = 0;

            RptListFunction.ViewOperList(cdvOper.GetListView, cdvFactory.Text, "");
        }

        private void rbtLotID_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtLotID.Checked)
            {
                txtLotID.ReadOnly = false;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
            }
        }

        private void rbtPeriod_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtPeriod.Checked)
            {
                txtLotID.ReadOnly = true;
                dtpFromDate.Enabled = true;
                dtpToDate.Enabled = true;
                txtLotID.Text = "";
            }
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            DataTable RetDT = null;
            string QueryCond = null;

            if (e.Row < 0) return;

            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, Convert.ToString(spdData.Sheets[0].GetValue(e.Row, 2)));
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, Convert.ToString(spdData.Sheets[0].GetValue(e.Row, 0)));
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, Convert.ToDateTime(spdData.Sheets[0].GetValue(e.Row, 5)).ToString("yyyyMMddHHmmss"));
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, Convert.ToDateTime(spdData.Sheets[0].GetValue(e.Row, 4)).ToString("yyyyMMddHHmmss"));

            spdDetail_Sheet1.RowCount = 0;
            this.Refresh();

            RetDT = CmnFunction.oComm.GetFuncDataTable("STD1608_1", QueryCond);

            if (RetDT.Rows.Count == 0) { spdDetail.Visible = false; }
            else
            {
                spdDetail.Visible = true;
                spdDetail_Sheet1.DataSource = RetDT;
            }

            spdDetail_Sheet1.SetColumnAllowAutoSort(0, spdDetail_Sheet1.ColumnCount, true);
            CmnFunction.FitColumnHeader(spdDetail);
            CmnInitFunction.InitLockSpread(spdDetail);

            RetDT.Dispose();
        }
    }
}