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
    public partial class STD1607 : Miracom.SmartWeb.FWX.udcUIBase
    {
        public STD1607()
        {
            InitializeComponent();
        }

        private void STD1607_Load(object sender, EventArgs e)
        {
            CmnInitFunction.InitSpread(spdData);
        }

        private void btnView_Click(object sender, EventArgs e)
        {

            DataTable dt = null;
            DataTable RetDT = new DataTable();
            string QueryCond = null;

            CheckField();
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvMat.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvMatVer.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvOper.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvMatGrpList.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtMatGrp.Text);
                        
            spdData_Sheet1.RowCount=0;
            this.Refresh();
            dt = CmnFunction.oComm.GetFuncDataTable("STD1607", QueryCond);
            if (dt.Rows.Count == 0) { spdData.Visible = false; }
            else { spdData.Visible = true; }


            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataColumn DetailCol = new DataColumn();

                DetailCol.ReadOnly = false;
                DetailCol.Unique = false;

                DetailCol.ColumnName = dt.Columns[i].ColumnName;
                DetailCol.DataType = System.Type.GetType("System.String");

                RetDT.Columns.Add(DetailCol);
            }

            //New DataTable Value Á¤ÀÇ
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow OperRow = null;
                OperRow = RetDT.NewRow();
                OperRow[0] = dt.Rows[i].ItemArray[0];
                RetDT.Rows.Add(OperRow);
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    if (j == 5 || j == 6 || j == 11)
                    {
                        RetDT.Rows[i][j] = CmnFunction.ToDate(dt.Rows[i].ItemArray[j].ToString().Trim());
                    }
                    else
                    {
                        if (j > 6 && j < 11)
                        {
                            RetDT.Rows[i][j] = CmnFunction.ToDbl(dt.Rows[i].ItemArray[j]).ToString("#########0.####");
                        }
                        else
                        {
                            RetDT.Rows[i][j] = dt.Rows[i].ItemArray[j];
                        }
                    }
                }
            }

            spdData_Sheet1.DataSource = RetDT;
            spdData_Sheet1.SetColumnAllowAutoSort(0, spdData_Sheet1.ColumnCount, true);
            CmnFunction.FitColumnHeader(spdData);
            CmnInitFunction.InitLockSpread(spdData);
            dt.Dispose();

        }

        private Boolean CheckField()
        {
            Boolean Check = false;

            if (cdvFactory.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1607");
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

            CmnExcelFunction.ExportToExcelEx(obj, null, 1, "Cycle Time", "", true, false, false, -1, -1);
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
        
        private void cdvMatGrp_ButtonPress(object sender, EventArgs e)
        {
            cdvMatGrp.Init();
            CmnInitFunction.InitListView(cdvMatGrp.GetListView);
            cdvMatGrp.Columns.Add("Prompt", 30, HorizontalAlignment.Left);
            cdvMatGrp.Columns.Add("Group", 70, HorizontalAlignment.Left);
            cdvMatGrp.SelectedSubItemIndex = 0;

            RptListFunction.ViewMATGroupList(cdvMatGrp.GetListView, cdvFactory.Text, "GRP_MATERIAL");
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

        private void cdvOper_ButtonPress(object sender, EventArgs e)
        {
            cdvOper.Init();
            CmnInitFunction.InitListView(cdvOper.GetListView);
            cdvOper.Columns.Add("KEY_1", 30, HorizontalAlignment.Left);
            cdvOper.Columns.Add("DATA_1", 70, HorizontalAlignment.Left);
            cdvOper.SelectedSubItemIndex = 0;

            RptListFunction.ViewOperList(cdvOper.GetListView, cdvFactory.Text, "");
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

        private void cdvMat_ButtonPress(object sender, EventArgs e)
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1607");
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
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1607");
                return;
            }
            else if (cdvMat.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD006", GlobalVariable.gcLanguage), "STD1607");
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