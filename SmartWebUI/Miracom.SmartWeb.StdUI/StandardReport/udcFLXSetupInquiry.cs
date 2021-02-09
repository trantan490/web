using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.UI
{
    public partial class udcFLXSetupInquiry : Miracom.SmartWeb.FWX.udcUIBase
    {
        public udcFLXSetupInquiry()
        {
            InitializeComponent();
        }

        #region " Constant Definition "

        #endregion

        #region " Variable Definition "


        #endregion

        #region " Function Definition "

        //
        // ClearData()
        //       - Initalize form fields
        // Return Value
        //       -
        // Arguments
        //       - Optional ByVal ProcStep As String ("1", "2")
        //
        private void ClearData(string ProcStep)
        {
            //try
            //{
                
            //}
            //catch (Exception ex)
            //{

            //}

        }

        private bool CheckCondition(string FuncName)
        {
            switch (FuncName)
            {
                case "CREATE_INQ":
                case "UPDATE_INQ":
                    if (txtInqName.Text.Trim() == "")
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                        txtInqName.Focus();
                        return false;
                    }
                    if (cdvFactory.Text.Trim() == "")
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                        cdvFactory.Focus();
                        return false;
                    }
                    if (cdvInqGrp.Text.Trim() == "")
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                        cdvInqGrp.Focus();
                        return false;
                    }
                    if (cdvSelItem.Text.Trim() == "")
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                        cdvSelItem.Focus();
                        return false;
                    }
                    if (cdvSelValue.Text.Trim() == "")
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                        cdvSelValue.Focus();
                        return false;
                    }
                    if (cdvSelGrpItem.Text.Trim() == "")
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                        cdvSelGrpItem.Focus();
                        return false;
                    }
                    break;
                case "DELETE_INQ":
                    if (txtInqName.Text.Trim() == "")
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                        txtInqName.Focus();
                        return false;
                    }
                    break;
                case "ATTACH_COL":
                    if (txtInqName.Text.Trim() == "")
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                        txtInqName.Focus();
                        return false;
                    }
                    break;
                case "DETACH_COL":
                    if (txtInqName.Text.Trim() == "")
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                        txtInqName.Focus();
                        return false;
                    }
                    break;
            }
            return true;
        }

        private void InitCodeView()
        {
            cdvConFactory.Init();
            CmnInitFunction.InitListView(cdvConFactory.GetListView);
            cdvConFactory.Columns.Add("Factory", 150, HorizontalAlignment.Left);
            cdvConFactory.Columns.Add("Desc", 200, HorizontalAlignment.Left);
            cdvConFactory.SelectedSubItemIndex = 0;
            CmnListFunction.ViewFactoryList(cdvConFactory.GetListView, '1', "");

            cdvFactory.Init();
            CmnInitFunction.InitListView(cdvFactory.GetListView);
            cdvFactory.Columns.Add("Factory", 150, HorizontalAlignment.Left);
            cdvFactory.Columns.Add("Desc", 200, HorizontalAlignment.Left);
            cdvFactory.SelectedSubItemIndex = 0;
            CmnListFunction.ViewFactoryList(cdvFactory.GetListView, '1', "");

            cdvConInqGrp.Init();
            CmnInitFunction.InitListView(cdvConInqGrp.GetListView);
            cdvConInqGrp.Columns.Add("Group", 150, HorizontalAlignment.Left);
            cdvConInqGrp.Columns.Add("Desc", 200, HorizontalAlignment.Left);
            cdvConInqGrp.SelectedSubItemIndex = 0;

            cdvInqGrp.Init();
            CmnInitFunction.InitListView(cdvInqGrp.GetListView);
            cdvInqGrp.Columns.Add("Group", 150, HorizontalAlignment.Left);
            cdvInqGrp.Columns.Add("Desc", 200, HorizontalAlignment.Left);
            cdvInqGrp.SelectedSubItemIndex = 0;

            cdvSelItem.Init();
            CmnInitFunction.InitListView(cdvSelItem.GetListView);
            cdvSelItem.Columns.Add("Item", 150, HorizontalAlignment.Left);
            cdvSelItem.Columns.Add("Desc", 200, HorizontalAlignment.Left);
            cdvSelItem.SelectedSubItemIndex = 0;

            cdvSelValue.Init();
            CmnInitFunction.InitListView(cdvSelValue.GetListView);
            cdvSelValue.Columns.Add("Item", 150, HorizontalAlignment.Left);
            cdvSelValue.Columns.Add("Desc", 200, HorizontalAlignment.Left);
            cdvSelValue.SelectedSubItemIndex = 0;

            cdvSelGrpItem.Init();
            CmnInitFunction.InitListView(cdvSelGrpItem.GetListView);
            cdvSelGrpItem.Columns.Add("Item", 150, HorizontalAlignment.Left);
            cdvSelGrpItem.Columns.Add("Desc", 200, HorizontalAlignment.Left);
            cdvSelGrpItem.SelectedSubItemIndex = 0;

            cdvFilter.Init();
            CmnInitFunction.InitListView(cdvFilter.GetListView);
            cdvFilter.Columns.Add("Item", 150, HorizontalAlignment.Left);
            cdvFilter.Columns.Add("Desc", 200, HorizontalAlignment.Left);
            cdvFilter.SelectedSubItemIndex = 0;

            cdvFilterValue.Init();
            CmnInitFunction.InitListView(cdvFilterValue.GetListView);
            cdvFilterValue.Columns.Add("Item", 150, HorizontalAlignment.Left);
            cdvFilterValue.Columns.Add("Desc", 200, HorizontalAlignment.Left);
            cdvFilterValue.SelectedSubItemIndex = 0;

            CmnListFunction.ViewDataList(lisColList, '1', "SYSTEM", GlobalConstant.MP_GCM_FLEXWIP_COL_TBL);
        }

        #endregion

        public Boolean View_Inquiry(char cStep, string sFactory, string sInqName)
        {

            DataTable rtDataTable = new DataTable();
            string QueryCond = null;

            int i = 0;

            try
            {
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, sFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, sInqName);

                rtDataTable = CmnFunction.oComm.SelectData("RWEBFLXINQ", cStep.ToString(), QueryCond);

                if (rtDataTable.Rows.Count > 0)
                {
                    txtInqName.Text = rtDataTable.Rows[i]["INQUIRY_NAME"].ToString();
                    txtDesc.Text = rtDataTable.Rows[i]["INQUIRY_DESC"].ToString();
                    cdvFactory.Text = rtDataTable.Rows[i]["FACTORY"].ToString();
                    cdvInqGrp.Text = rtDataTable.Rows[i]["INQUIRY_GROUP"].ToString();
                    cdvSelItem.Text = rtDataTable.Rows[i]["SELECT_ITEM"].ToString();
                    cdvSelValue.Text = rtDataTable.Rows[i]["SELECT_VALUE"].ToString();
                    cdvSelGrpItem.Text = rtDataTable.Rows[i]["GROUP_ITEM"].ToString();
                    txtQuery.Text = rtDataTable.Rows[i]["FILTER_QUERY"].ToString();
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    rtDataTable.Dispose();
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }

            return true;
        }

        public Boolean View_Inquiry_Column(char cStep, string sFactory, string sInqName)
        {

            DataTable rtDataTable = new DataTable();
            string QueryCond = null;

            int i;
            try
            {
                CmnInitFunction.InitListView(lisAttachCol);

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, sFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, sInqName);

                rtDataTable = CmnFunction.oComm.FillData("RWEBFLXCOL", "1", QueryCond);

                if (rtDataTable.Rows.Count == 0)
                {
                    return false;

                }

                if (rtDataTable.Rows.Count > 0)
                {
                    for (i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        ListViewItem itmX = null;

                        itmX = new ListViewItem(rtDataTable.Rows[i]["COLUMN_SEQ"].ToString());
                        itmX.SubItems.Add(rtDataTable.Rows[i]["COLUMN_PERIOD"].ToString());
                        itmX.SubItems.Add(rtDataTable.Rows[i]["COLUMN_NAME"].ToString());

                        lisAttachCol.Items.Add(itmX);
                    }
                }
                if (rtDataTable.Rows.Count > 0)
                {
                    rtDataTable.Dispose();
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }

            return true;
        }

        private bool Update_Inquiry(char cStep)
        {
            string UpdateValue = null;
            DataTable dtDataTable = new DataTable();    

			try
			{
                UpdateValue = "";
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, cdvFactory.Text);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtInqName.Text.Trim());

                dtDataTable = CmnFunction.oComm.SelectData("RWEBFLXINQ", "1", UpdateValue);

                //Create
                if (cStep == '1')
                {
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtDesc.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, cdvInqGrp.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, cdvSelItem.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, cdvSelValue.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, cdvSelGrpItem.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtQuery.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");

                    if (dtDataTable.Rows.Count > 0)
                    {
                        CmnFunction.ShowMsgBox("This Inquiry is already exist. Please check Inquiry name.");
                        return false;
                    }

                    if (CmnFunction.oComm.InsertData("RWEBFLXINQ", "1", UpdateValue) != true)
                    {
                        CmnFunction.ShowMsgBox("Fatal database error is occured. Please contact admin person.");
                        return false;
                    }
                }
                //Update
                else if (cStep == '2')
                {
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtDesc.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, cdvInqGrp.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, cdvSelItem.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, cdvSelValue.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, cdvSelGrpItem.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtQuery.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");

                    if (dtDataTable.Rows.Count < 1)
                    {
                        CmnFunction.ShowMsgBox("This Inquiry is not exist.");
                        return false;
                    }

                    if (CmnFunction.oComm.UpdateData("RWEBFLXINQ", "1", UpdateValue) != true)
                    {
                        CmnFunction.ShowMsgBox("Fatal database error is occured. Please contact admin person.");
                        return false;
                    }
                }
                //Delete
                else if (cStep == '3')
                {
                    if (dtDataTable.Rows.Count < 1)
                    {
                        CmnFunction.ShowMsgBox("This Inquiry is not exist.");
                        return false;
                    }

                    //Delete Inquiry
                    if (CmnFunction.oComm.DeleteData("RWEBFLXINQ", "1", UpdateValue) != true)
                    {
                        CmnFunction.ShowMsgBox("Fatal database error is occured. Please contact admin person.");
                        return false;
                    }

                    //Delete Column Attached Inquiry
                    if (CmnFunction.oComm.DeleteData("RWEBFLXCOL", "1", UpdateValue) != true)
                    {
                        CmnFunction.ShowMsgBox("Fatal database error is occured. Please contact admin person.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }

            return true;
        }

        private bool Attach_Column(int iRowIndex)
        {
            string InsertValue = "";
            int iSeq = 0;
            DataTable dtDataTable = new DataTable();

            try
            {
                iSeq = lisAttachCol.Items.Count + 1;

                InsertValue = "";
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, cdvFactory.Text);
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, txtInqName.Text.Trim());
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, iSeq.ToString());
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, lisColList.Items[iRowIndex].Text);
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, cboPeriod.Text.Substring(0, 3).Trim());
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");

                //Validation Check
                dtDataTable = CmnFunction.oComm.SelectData("RWEBFLXINQ", "1", InsertValue);

                if (dtDataTable.Rows.Count < 1)
                {
                    CmnFunction.ShowMsgBox("This Inquiry is not exist. Please setup Inquiry first.");
                    return false;
                }

                //Validation Check
                dtDataTable = CmnFunction.oComm.SelectData("RWEBFLXCOL", "1", InsertValue);

                if (dtDataTable.Rows.Count > 0)
                {
                    CmnFunction.ShowMsgBox("This Column is already exist. Please check Column.");
                }

                if (CmnFunction.oComm.InsertData("RWEBFLXCOL", "1", InsertValue) != true)
                {
                    CmnFunction.ShowMsgBox("Fatal database error is occured. Please contact admin person.");
                    return false;
                }

                ListViewItem itmX = null;

                itmX = new ListViewItem(iSeq.ToString());
                itmX.SubItems.Add(cboPeriod.Text.Substring(0, 3).Trim());
                itmX.SubItems.Add(lisColList.Items[iRowIndex].Text);

                lisAttachCol.Items.Add(itmX);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }

            return true;
        }

        private bool Detach_Column(int iRowIndex)
        {
            string QueryCond = null;
            DataTable dtDataTable = new DataTable();

            try
            {
                QueryCond = "";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtInqName.Text);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, lisAttachCol.Items[iRowIndex].Text);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, lisAttachCol.Items[iRowIndex].SubItems[2].Text);

                //Validation Check
                dtDataTable = CmnFunction.oComm.SelectData("RWEBFLXINQ", "1", QueryCond);

                if (dtDataTable.Rows.Count < 1)
                {
                    CmnFunction.ShowMsgBox("This Inquiry is not exist. Please setup Inquiry first.");
                    return false;
                }

                dtDataTable = CmnFunction.oComm.SelectData("RWEBFLXCOL", "1", QueryCond);

                if (dtDataTable.Rows.Count == 0)
                {
                    CmnFunction.ShowMsgBox("This Column is not exist.");
                }

                if (CmnFunction.oComm.DeleteData("RWEBFLXCOL", "2", QueryCond) != true)
                {
                    CmnFunction.ShowMsgBox("Fatal database error is occured. Please contact admin person.");
                    return false;
                }
                else
                {
                    //Sequence Sorter
                    QueryCond = "";
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtInqName.Text);
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, lisAttachCol.Items[iRowIndex].Text);

                    if (CmnFunction.oComm.UpdateData("RWEBFLXCOL", "2", QueryCond) != true)
                    {
                        CmnFunction.ShowMsgBox("Fatal database error is occured. Please contact admin person.");
                        return false;
                    }

                    //lisAttachCol.Items[iRowIndex].Remove();
                }

                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }
            
        }

        private bool Chane_Seq(bool bUpDownFlag, int iRowIndex)
        {
            string UpdateValue = "";
            DataTable dtDataTable = new DataTable();

            int iSeq;

            try
            {
                if (bUpDownFlag == true)
                {
                    iSeq = System.Convert.ToInt32(lisAttachCol.Items[iRowIndex].Text) - 1;
                }
                else
                {
                    iSeq = System.Convert.ToInt32(lisAttachCol.Items[iRowIndex].Text) + 1;
                }                

                UpdateValue = "";
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, cdvFactory.Text);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtInqName.Text.Trim());
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, lisAttachCol.Items[iRowIndex].Text);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, lisAttachCol.Items[iRowIndex].SubItems[2].Text);

                //Validation Check
                dtDataTable = CmnFunction.oComm.SelectData("RWEBFLXINQ", "1", UpdateValue);

                if (dtDataTable.Rows.Count < 1)
                {
                    CmnFunction.ShowMsgBox("This Inquiry is not exist. Please setup Inquiry first.");
                    return false;
                }

                //Validation Check
                dtDataTable = CmnFunction.oComm.SelectData("RWEBFLXCOL", "1", UpdateValue);

                if (dtDataTable.Rows.Count == 0)
                {
                    CmnFunction.ShowMsgBox("This Column is not exist.");
                }

                UpdateValue = "";
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, cdvFactory.Text);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtInqName.Text.Trim());
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, iSeq.ToString());
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, lisAttachCol.Items[iRowIndex].SubItems[2].Text);

                if (CmnFunction.oComm.UpdateData("RWEBFLXCOL", "1", UpdateValue) != true)
                {
                    CmnFunction.ShowMsgBox("Fatal database error is occured. Please contact admin person.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }

            return true;
        }

        private void udcFLXSetupInquiry_Load(object sender, EventArgs e)
        {
            CmnFunction.oComm.SetUrl();

            CmnInitFunction.InitListView(lisInqList);
            CmnInitFunction.InitListView(lisColList);
            CmnInitFunction.InitListView(lisAttachCol);

            InitCodeView();

            cboPeriod.SelectedIndex = 0;
            cboLogical.SelectedIndex = 0;
            cboOperator.SelectedIndex = 0;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (CheckCondition("CREATE_INQ") == true)
            {
                if (Update_Inquiry('1') == true)
                {
                    CmnFunction.ShowMsgBox("Successfully completed.");

                    if (cdvConFactory.Text != "" && cdvConInqGrp.Text != "")
                    {
                        CmnListFunction.View_Inquiry_List(lisInqList, '2', cdvConFactory.Text, cdvConInqGrp.Text);
                    }
                    else
                    {
                        CmnListFunction.View_Inquiry_List(lisInqList, '1', cdvConFactory.Text);
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (CheckCondition("UPDATE_INQ") == true)
            {
                if (Update_Inquiry('2') == true)
                {
                    CmnFunction.ShowMsgBox("Successfully completed.");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CheckCondition("DELETE_INQ") == true)
            {
                if (CmnFunction.ShowMsgBox("Are you sure you want to delete the data?", "Delete", MessageBoxButtons.YesNo, 2) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (Update_Inquiry('3') == true)
                    {
                        CmnFunction.ShowMsgBox("Successfully completed.");

                        if (cdvConFactory.Text != "" && cdvConInqGrp.Text != "")
                        {
                            CmnListFunction.View_Inquiry_List(lisInqList, '2', cdvConFactory.Text, cdvConInqGrp.Text);
                        }
                        else
                        {
                            CmnListFunction.View_Inquiry_List(lisInqList, '1', cdvConFactory.Text);
                        }
                    }
                }
            }
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

        private void btnAttach_Click(object sender, EventArgs e)
        {
            int i;

            if (CheckCondition("ATTACH_COL") == true)
            {
                if (lisColList.SelectedItems.Count < 1) return;

                for (i = 0; i < lisColList.Items.Count; i++)
                {
                    if (lisColList.Items[i].Selected)
                    {
                        Attach_Column(i);
                    }
                }
            }
        }

        private void btnDetach_Click(object sender, EventArgs e)
        {
            int i;

            if (CheckCondition("DETACH_COL") == true)
            {
                if (lisAttachCol.SelectedItems.Count < 1) return;

                for (i = 0; i < lisAttachCol.Items.Count; i++)
                {
                    if (lisAttachCol.Items[i].Selected)
                    {
                        Detach_Column(i);
                    }
                }
                View_Inquiry_Column('1', cdvConFactory.Text, lisInqList.SelectedItems[0].Text);
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (lisAttachCol.SelectedItems.Count < 1) return;
            if (lisAttachCol.SelectedItems[0].Index == 0) return;

            if (Chane_Seq(true, lisAttachCol.SelectedItems[0].Index) == false) return;
            if (Chane_Seq(false, lisAttachCol.SelectedItems[0].Index - 1) == false) return;

            View_Inquiry_Column('1', cdvConFactory.Text, lisInqList.SelectedItems[0].Text);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (lisAttachCol.SelectedItems.Count < 1) return;
            if (lisAttachCol.SelectedItems[0].Index == lisAttachCol.Items.Count - 1) return;

            if (Chane_Seq(false, lisAttachCol.SelectedItems[0].Index) == false) return;
            if (Chane_Seq(true, lisAttachCol.SelectedItems[0].Index + 1) == false) return;

            View_Inquiry_Column('1', cdvConFactory.Text, lisInqList.SelectedItems[0].Text);
        }

        private void cdvConFactory_SelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            CmnListFunction.View_Inquiry_List(lisInqList, '1', cdvConFactory.Text);
        }

        private void cdvConInqGrp_ButtonPress(object sender, EventArgs e)
        {
            CmnListFunction.ViewDataList(cdvConInqGrp.GetListView, '1', cdvConFactory.Text, GlobalConstant.MP_GCM_FLEXWIP_GROUP);
        }
        private void cdvConInqGrp_SelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            CmnListFunction.View_Inquiry_List(lisInqList, '2', cdvConFactory.Text, cdvConInqGrp.Text);
        }

        private void cdvInqGrp_ButtonPress(object sender, EventArgs e)
        {
            CmnListFunction.ViewDataList(cdvInqGrp.GetListView, '1', cdvFactory.Text, GlobalConstant.MP_GCM_FLEXWIP_GROUP);
        }
        private void cdvInqGrp_SelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            CmnListFunction.View_Inquiry_List(cdvInqGrp.GetListView, '2', cdvFactory.Text, cdvInqGrp.Text);
        }

        private void cdvSelItem_ButtonPress(object sender, EventArgs e)
        {
            CmnListFunction.ViewDataList(cdvSelItem.GetListView, '1', cdvFactory.Text, GlobalConstant.MP_GCM_FLEXWIP_GROUP_ITEM);
        }
        private void cdvSelItem_SelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {

        }

        private void cdvSelGrpItem_ButtonPress(object sender, EventArgs e)
        {
            CmnListFunction.ViewDataList(cdvSelGrpItem.GetListView, '1', cdvFactory.Text, GlobalConstant.MP_GCM_FLEXWIP_GROUP_ITEM);
        }
        private void cdvSelGrpItem_SelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            
        }

        private void cdvSelValue_ButtonPress(object sender, EventArgs e)
        {
            if (cdvSelItem.Text == "OPER")
            {
                CmnListFunction.ViewOperList(cdvSelValue.GetListView, '1', cdvFactory.Text);
            }
            else if (cdvSelItem.Text == "FLOW")
            {
                CmnListFunction.ViewFlowList(cdvSelValue.GetListView, '1', cdvFactory.Text);
            }
            else
            {
                CmnListFunction.ViewDataList(cdvSelValue.GetListView, '1', cdvFactory.Text, cdvSelItem.Text);
            }
        }

        private void cdvFilter_ButtonPress(object sender, EventArgs e)
        {
            CmnListFunction.ViewDataList(cdvFilter.GetListView, '1', cdvFactory.Text, GlobalConstant.MP_GCM_FLEXWIP_GROUP_ITEM);
        }

        private void cdvFilterValue_ButtonPress(object sender, EventArgs e)
        {
            if (cdvFilter.Text == "OPER")
            {
                CmnListFunction.ViewOperList(cdvFilterValue.GetListView, '1', cdvFactory.Text);
            }
            else if (cdvFilter.Text == "FLOW")
            {
                CmnListFunction.ViewFlowList(cdvFilterValue.GetListView, '1', cdvFactory.Text);
            }
            else
            {
                CmnListFunction.ViewDataList(cdvFilterValue.GetListView, '1', cdvFactory.Text, cdvFilter.Text);
            }
        }

        private void lisInqList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lisInqList.SelectedItems.Count < 1) return;

            if (View_Inquiry('1', cdvConFactory.Text, lisInqList.SelectedItems[0].Text) == true)
            {
                View_Inquiry_Column('1', cdvConFactory.Text, lisInqList.SelectedItems[0].Text);
            }
        }

        private void tabFilterColumn_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0:
                    btnCreate.Enabled = true;
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                    break;
                case 1:
                    btnCreate.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnDelete.Enabled = false;
                    break;
            }
        }

        private void pnlAttachColumnFill_Resize(object sender, EventArgs e)
        {
            CmnFunction.FieldAdjust(pnlAttachColumnFill, pnlAttachColumn, pnlColumnList, pnlAttachColumnMid, 40);
        }

        private void btnAddFiter_Click(object sender, EventArgs e)
        {
            string sMatID = "";

            if (cdvFilter.Text.Trim() == "") return;
            if (cdvFilterValue.Text.Trim() == "") return;

            switch (cdvFilter.Text.Trim())
            {
                case "MAT":
                    txtQuery.Text += cboLogical.Text + " MAT." +
                                cdvFilter.Text + " " +
                                cboOperator.Text.Substring(0, cboOperator.Text.IndexOf(" ")) + " '" +
                                cdvFilterValue.Text + "' \n";
                    break;
                case "FLOW":
                    txtQuery.Text += cboLogical.Text + " FLW." +
                                cdvFilter.Text + " " +
                                cboOperator.Text.Substring(0, cboOperator.Text.IndexOf(" ")) + " '" +
                                cdvFilterValue.Text + "' \n";
                    break;
                case "OPER":
                    txtQuery.Text += cboLogical.Text + " OPR." +
                                cdvFilter.Text + " " +
                                cboOperator.Text.Substring(0, cboOperator.Text.IndexOf(" ")) + " '" +
                                cdvFilterValue.Text + "' \n";
                    break;

                default:

                    if (cdvFilter.Text.LastIndexOf("MAT") > -1)
                    {
                        sMatID = cdvFilter.Text.Replace("MATERIAL", "MAT");
                        txtQuery.Text += cboLogical.Text + " MAT." +
                                sMatID + " " +
                                cboOperator.Text.Substring(0, cboOperator.Text.IndexOf(" ")) + " '" +
                                cdvFilterValue.Text + "' \n";
                    }
                    else if (cdvFilter.Text.LastIndexOf("FLOW") > -1)
                    {
                        txtQuery.Text += cboLogical.Text + " FLW." +
                                cdvFilter.Text + " " +
                                cboOperator.Text.Substring(0, cboOperator.Text.IndexOf(" ")) + " '" +
                                cdvFilterValue.Text + "' \n";
                        break;
                    }
                    else if (cdvFilter.Text.LastIndexOf("OPER") > -1)
                    {
                        txtQuery.Text += cboLogical.Text + " OPR." +
                                cdvFilter.Text + " " +
                                cboOperator.Text.Substring(0, cboOperator.Text.IndexOf(" ")) + " '" +
                                cdvFilterValue.Text + "' \n";
                        break;
                    }

                    break;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtQuery.Text = "";
        }
    }
}