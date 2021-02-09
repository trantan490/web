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
    public partial class STD1204 : Miracom.SmartWeb.FWX.udcUIBase
    {
        public STD1204()
        {
            InitializeComponent();
        }

        private void STD1204_Load(object sender, EventArgs e)
        {
            CmnInitFunction.InitSpread(spdData);
        }

        private void btnView_Click(object sender, EventArgs e)
        {

            DataTable dt = null;
            string QueryCond = null;

            CheckField();
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, dtpDate.Value.ToString("yyyyMMdd"));
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvOperGrpList.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtOperGrp.Text);

            spdData_Sheet1.RowCount=0;
            this.Refresh();
            dt = CmnFunction.oComm.GetFuncDataTable("STD1204", QueryCond);
            if (dt.Rows.Count == 0) { spdData.Visible = false; }
            else { spdData.Visible = true; }
            spdData_Sheet1.DataSource = dt;
            CmnFunction.FitColumnHeader(spdData);
            dt.Dispose();
        }

        private Boolean CheckField()
        {
            Boolean Check = false;

            if (cdvFactory.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1204");
                Check = false;
            }
            else { Check = true; }

            return Check;
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.Sheets[0].RowCount == 0)
            {
                MessageBox.Show(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage), "STD1204");
                return;
            }

            CmnExcelFunction.ExportToExcel(spdData, "Production Output", "", true, false, false, -1, -1);
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
    }
}