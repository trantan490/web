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
    public partial class STD1206 : Miracom.SmartWeb.FWX.udcUIBase
    {
        public STD1206()
        {
            InitializeComponent();
        }

        private void STD1206_Load(object sender, EventArgs e)
        {
            CmnInitFunction.InitSpread(spdData);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            string QueryCond = null;

            CheckField();
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvLotType.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvMatType.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtMatGrp.Text);
                        
            spdData_Sheet1.RowCount=0;
            this.Refresh();
            dt = CmnFunction.oComm.GetFuncDataTable("STD1206", QueryCond);
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
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1206");
                Check = false;
            }
            else if (cdvMatType.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD003", GlobalVariable.gcLanguage), "STD1206");
                Check = false;
            }
            else if (txtMatGrp.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD004", GlobalVariable.gcLanguage), "STD1206");
                Check = false;
            }
            else { Check = true; }

            return Check;
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.Sheets[0].RowCount == 0)
            {
                MessageBox.Show(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage), "STD1206");
                return;
            }

            CmnExcelFunction.ExportToExcel(spdData, "WIP Status", "", true, false, false, -1, -1);
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

        private void cdvMatGrp_SelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            txtMatGrp.Text = cdvMatGrp.Items[cdvMatGrp.SelectedItem.Index].SubItems[1].Text;                         
        }

        private void cdvMatGrp_TextBoxTextChanged(object sender, EventArgs e)
        {            
            if (cdvMatGrp.Text.TrimEnd() == "")
            {
                txtMatGrp.Text = "";
            }
        }
    }
}