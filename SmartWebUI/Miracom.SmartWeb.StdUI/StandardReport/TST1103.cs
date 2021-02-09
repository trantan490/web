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
    public partial class TST1103 : Miracom.SmartWeb.FWX.udcUIBase
    {
        //=======임시사용 ItemName 선언===========//
        string ItemName = null;
        //=======임시사용 ItemName 선언===========//

        string SelTbl = " ";
        string SelPrt = " ";

        public TST1103()
        {
            InitializeComponent();
        }
        private void TST1103_Load(object sender, EventArgs e)
        {

            CmnInitFunction.InitSpread(spdData);
        }

        private void btnClose_Click(object sender, System.EventArgs e)
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

        private void cdvFactory_SelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            //=======ItemName 임시적용===========//
            ItemName = "GRP_MATERIAL";
            //=======ItemName 임시적용===========//

            CheckField(); //factory 선택 check

            listPrt.GetListView.Items.Clear();
            listGrp.GetListView.Items.Clear();
            SelTbl = " ";

            listGrp.GetListView.Columns.Clear();// 헤더init
            listGrp.GetListView.Columns.Add("GROUP", 100, HorizontalAlignment.Left);
            listGrp.GetListView.Columns.Add("DESC", 130, HorizontalAlignment.Left);

            RptListFunction.ViewFacCMFData(listPrt.GetListView, cdvFactory.Text, ItemName);
        }

        private Boolean CheckField()
        {
            Boolean Check = false;

            if (cdvFactory.Text.TrimEnd() == "")
            {
                MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "TST1103");
                Check = false;
            }
            else { Check = true; }

            return Check;
        }

        private void listPrt_SelectedItemChanged(object sender, EventArgs e)
        {
            SelPrt = " ";
            SelTbl = " ";
            foreach (ListViewItem TempItems in listPrt.SelectedItems)
            {
                SelPrt = TempItems.Text;
                SelTbl = TempItems.SubItems[1].Text;
            }
            listGrp.GetListView.Columns.Clear();
            listGrp.GetListView.Columns.Add(SelPrt, 100, HorizontalAlignment.Left);//선택된Prompt명을 헤더로사용
            listGrp.GetListView.Columns.Add("DESC", 130, HorizontalAlignment.Left);
            RptListFunction.ViewGCMGrpList(listGrp.GetListView, cdvFactory.Text, SelPrt, SelTbl);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            string QueryCond = null;

            CheckField();


            QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, SelPrt);//Prompt TableName

            foreach (ListViewItem TempItems in listGrp.SelectedItems)
            {
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, TempItems.Text);
            }

            spdData_Sheet1.RowCount = 0;
            this.Refresh();
            dt = CmnFunction.oComm.GetFuncDataTable("TST1103", QueryCond);
            if (dt.Rows.Count == 0)
            {
                spdData.Visible = false;
            }
            else { spdData.Visible = true; }
            spdData_Sheet1.DataSource = dt;

            if (dt.Rows.Count > 0)
            {
                CmnFunction.FitColumnHeader(spdData);
            }
            dt.Dispose();

        }

    }
}
