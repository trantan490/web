using System;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.UI
{
    public partial class SEC1102 : Miracom.SmartWeb.FWX.udcUIBase
	{
        /// <summary>
        /// Class              : SEC1102<br/>
        /// Class Description  : Security Group Setup Class<br/>
        /// Developer          : W.Y Choi<br/>
        /// Create Date        : 2008-10-17<br/>
        /// Detail Description : Security Group Setup Class<br/>
        /// Modify             : 1. <br/>
        /// </summary>
        
		public SEC1102()
		{
			InitializeComponent();
        }

        #region " Constant Definition "

        #endregion

        #region " Variable Definition "

        #endregion

        #region " Function Definition "

        /// <summary>
        /// View Security Group List.
        /// </summary>
        /// <returns>bool : True or False</returns>
        private bool View_SecGrp()
        {
            string QueryCond;
            try
            {
                DataTable rtDataTable = new DataTable();

                QueryCond = "";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, lisSecGrp.SelectedItems[0].Text);

                rtDataTable = CmnFunction.oComm.SelectData("RWEBGRPDEF", "1", QueryCond);
                if (rtDataTable.Rows.Count > 0)
                {
                    txtSecGrp.Text = rtDataTable.Rows[0]["SEC_GRP_ID"].ToString();
                    txtDesc.Text = rtDataTable.Rows[0]["SEC_GRP_DESC"].ToString();

                    txtCreateUser.Text = rtDataTable.Rows[0]["CREATE_USER_ID"].ToString();
                    txtCreateTime.Text = CmnFunction.ChangeDateTimeString(rtDataTable.Rows[0]["CREATE_TIME"].ToString(), "yyyyMMddHHmmss", "yyyy-MM-dd HH:mm:ss");
                    txtUpdateUser.Text = rtDataTable.Rows[0]["UPDATE_USER_ID"].ToString();
                    txtUpdateTime.Text = CmnFunction.ChangeDateTimeString(rtDataTable.Rows[0]["UPDATE_TIME"].ToString(), "yyyyMMddHHmmss", "yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD014", GlobalVariable.gcLanguage));
                    return false;
                }

                rtDataTable.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1102.View_SecGrp() : " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Insert RWEBGRPDEF Table.
        /// </summary>
        /// <returns>bool : True or False</returns>
        private bool Create_Security_Group()
        {
            string QueryCond, InsertValue = "";
            string sysCurrentDate;
            DataTable rtDataTable = new DataTable();

            try
            {

                sysCurrentDate = "";
                sysCurrentDate = CmnFunction.GetSysDateTime();
                if (sysCurrentDate == null)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                QueryCond = "";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtSecGrp.Text);

                rtDataTable = CmnFunction.oComm.SelectData("RWEBGRPDEF", "1", QueryCond);
                if (rtDataTable.Rows.Count < 0)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD016", GlobalVariable.gcLanguage));
                    return false;
                }
                else
                {

                    InsertValue = "";
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, GlobalVariable.gsCentralFactory);
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, txtSecGrp.Text.Trim());
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, txtDesc.Text.Trim());
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, GlobalVariable.gsUserID);
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, sysCurrentDate);
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, "");
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, "");

                    if (CmnFunction.oComm.InsertData("RWEBGRPDEF", "1", InsertValue) != true)
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1102.Create_Security_Group() : " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Update RWEBGRPDEF Table.
        /// </summary>
        /// <param name="secGroupId"></param>
        /// <returns>bool : True or False</returns>
        private bool Update_Security_Group(string secGroupId)
        {
            string QueryCond, UpdateValue = "";
            string sysCurrentDate;
            DataTable rtDataTable = new DataTable();

            try
            {
                sysCurrentDate = "";
                sysCurrentDate = CmnFunction.GetSysDateTime();
                if (sysCurrentDate == null)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                QueryCond = "";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, secGroupId);

                rtDataTable = CmnFunction.oComm.SelectData("RWEBGRPDEF", "1", QueryCond);
                if (rtDataTable.Rows.Count > 0)
                {
                    UpdateValue = "";
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, rtDataTable.Rows[0]["FACTORY"].ToString());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, rtDataTable.Rows[0]["SEC_GRP_ID"].ToString());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtDesc.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtCreateUser.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtCreateTime.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsUserID);
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, sysCurrentDate);
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, rtDataTable.Rows[0]["FACTORY"].ToString());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, rtDataTable.Rows[0]["SEC_GRP_ID"].ToString());

                    if (CmnFunction.oComm.UpdateData("RWEBGRPDEF", "1", UpdateValue) != true)
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                        return false;
                    }
                }
                else
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD014", GlobalVariable.gcLanguage));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1102.Update_Security_Group() : " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Delete RWEBGRPDEF Table.
        /// </summary>
        /// <param name="secGroupId"></param>
        /// <returns>bool : True or False</returns>
        private bool Delete_Security_Group(string secGroupId)
        {
            string QueryCond, DeleteCond = "";
            DataTable rtDataTable = new DataTable();

            try
            {

                QueryCond = "";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, secGroupId);

                rtDataTable = CmnFunction.oComm.SelectData("RWEBUSRDEF", "2", QueryCond);
                if (rtDataTable.Rows.Count > 0)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD017", GlobalVariable.gcLanguage));
                    return false;
                }

                rtDataTable.Clear();

                rtDataTable = CmnFunction.oComm.SelectData("RWEBGRPDEF", "1", QueryCond);
                if (rtDataTable.Rows.Count > 0)
                {

                    DeleteCond = "";
                    DeleteCond = FwxCmnFunction.PackCondition(DeleteCond, rtDataTable.Rows[0]["FACTORY"].ToString());
                    DeleteCond = FwxCmnFunction.PackCondition(DeleteCond, rtDataTable.Rows[0]["SEC_GRP_ID"].ToString());

                    if (CmnFunction.oComm.DeleteData("RWEBGRPDEF", "1", DeleteCond) != true)
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                        return false;
                    }

                    return true;
                }
                else
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD014", GlobalVariable.gcLanguage));
                    return false;
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1102.Delete_Security_Group() : " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Copy RWEBGRPDEF Table.
        /// </summary>
        /// <param name="secGroupId"></param>
        /// <returns>bool : True or False</returns>
        private bool Copy_Security_Group(string secGroupId)
        {
            string QueryCond, InsertValue = "";
            string sysCurrentDate;
            DataTable rtDataTable = new DataTable();

            try
            {
                //Vlidation Check - From Security Group
                QueryCond = "";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, secGroupId);

                rtDataTable = CmnFunction.oComm.SelectData("RWEBGRPDEF", "1", QueryCond);
                if (rtDataTable.Rows.Count < 0)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD016", GlobalVariable.gcLanguage));
                    return false;
                }

                sysCurrentDate = "";
                sysCurrentDate = CmnFunction.GetSysDateTime();
                if (sysCurrentDate == null)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                QueryCond = "";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtSecGrp.Text.Trim());

                rtDataTable = CmnFunction.oComm.SelectData("RWEBGRPDEF", "1", QueryCond);
                if (rtDataTable.Rows.Count > 0)
                {
                    InsertValue = "";
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, GlobalVariable.gsCentralFactory);
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, secGroupId);
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, "Copy From " + rtDataTable.Rows[0]["SEC_GRP_DESC"].ToString());
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, GlobalVariable.gsUserID);
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, sysCurrentDate);
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, "");
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, "");

                    if (CmnFunction.oComm.InsertData("RWEBGRPDEF", "1", InsertValue) != true)
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                        return false;
                    }
                    else
                    {
                        InsertValue = "";
                        InsertValue = FwxCmnFunction.PackCondition(InsertValue, secGroupId);
                        InsertValue = FwxCmnFunction.PackCondition(InsertValue, GlobalVariable.gsUserID);
                        InsertValue = FwxCmnFunction.PackCondition(InsertValue, sysCurrentDate);
                        InsertValue = FwxCmnFunction.PackCondition(InsertValue, GlobalVariable.gsCentralFactory);
                        InsertValue = FwxCmnFunction.PackCondition(InsertValue, txtSecGrp.Text.Trim());
                        
                        if (CmnFunction.oComm.InsertData("RWEBGRPFUN", "2", InsertValue) != true)
                        {
                            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                            return false;
                        }
                    }
                }
                else
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD014", GlobalVariable.gcLanguage));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1102.Copy_Security_Group() : " + ex.ToString());
                return false;
            }
        }

        #endregion

        private void SEC1102_Load(object sender, EventArgs e)
		{
            lisSecGrp.SmallImageList = ((System.Windows.Forms.TabControl)((System.Windows.Forms.TabPage)this.Parent).Parent).ImageList;
            CmnFunction.oComm.SetUrl(); 
            CmnInitFunction.InitListView(lisSecGrp);
            CmnListFunction.ViewSecGroupList(lisSecGrp);
		}

        private void SEC1102_Resize(object sender, EventArgs e)
        {
            tableLayoutPanel1.Width = this.Width - 10;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSecGrp.Text.Trim().Length == 0) CmnFunction.ShowMsgBox("Security Group : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                if (Create_Security_Group() == true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF001", GlobalVariable.gcLanguage));

                    ListViewItem lstItem;

                    lstItem = lisSecGrp.Items.Add(txtSecGrp.Text.Trim(), (int)SMALLICON_INDEX.IDX_SEC_GROUP);
                    lstItem.SubItems.Add(txtDesc.Text.Trim());
                    lstItem.Selected = true;
                    lisSecGrp.Sorting = SortOrder.Ascending;
                    lisSecGrp.Sort();
                    lstItem.EnsureVisible();
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1102.btnCreate_Click() : " + ex.ToString());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSecGrp.Text.Trim().Length == 0) CmnFunction.ShowMsgBox("Security Group : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                if (CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF003", GlobalVariable.gcLanguage), "Update", MessageBoxButtons.YesNo, 2) != System.Windows.Forms.DialogResult.Yes) return;
                if (Update_Security_Group(txtSecGrp.Text.Trim()) == true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF001", GlobalVariable.gcLanguage));

                    CmnInitFunction.FieldClear(this, null, false);
                    lisSecGrp.SelectedIndices.Clear();
                    lisSecGrp.Items.Clear();
                    CmnListFunction.ViewSecGroupList(lisSecGrp);
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1102.btnUpdate_Click() : " + ex.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSecGrp.Text.Trim().Length == 0) CmnFunction.ShowMsgBox("Security Group : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                if (CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF002", GlobalVariable.gcLanguage), "Delete", MessageBoxButtons.YesNo, 2) != System.Windows.Forms.DialogResult.Yes) return;
                if (Delete_Security_Group(txtSecGrp.Text.Trim()) == true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF001", GlobalVariable.gcLanguage));

                    CmnInitFunction.FieldClear(this, null, false);
                    lisSecGrp.Items.Remove(lisSecGrp.SelectedItems[0]);
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1102.btnDelete_Click() : " + ex.ToString());
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSecGrp.Text.Trim().Length == 0) CmnFunction.ShowMsgBox("Security Group : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                if (txtToSecGrp.Text.Trim().Length == 0) CmnFunction.ShowMsgBox("Security Group : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                if (CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF003", GlobalVariable.gcLanguage), "Copy", MessageBoxButtons.YesNo, 2) != System.Windows.Forms.DialogResult.Yes) return;
                if (Copy_Security_Group(txtToSecGrp.Text.Trim()) == true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF001", GlobalVariable.gcLanguage));

                    ListViewItem lstItem;
                    lstItem = lisSecGrp.Items.Add(txtToSecGrp.Text.Trim(), (int)SMALLICON_INDEX.IDX_SEC_GROUP);
                    lstItem.SubItems.Add("Copy From " + txtSecGrp.Text.Trim());
                    lstItem.Selected = true;
                    lisSecGrp.Sorting = SortOrder.Ascending;
                    lisSecGrp.Sort();
                    lstItem.EnsureVisible();
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1102.btnCopy_Click() : " + ex.ToString());
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                CmnExcelFunction.ExportToExcel(lisSecGrp, "Security Group List In Web Report", "", true, false, false, -1, -1);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1102.btnExcel_Click() : " + ex.ToString());
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
                CmnFunction.ShowMsgBox("SEC1102.btnClose_Click() : " + ex.ToString());
            }

        }

        private void lisSecGrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lisSecGrp.SelectedIndices.Count > 0)
                {
                    View_SecGrp();
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1102.lisSecGrp_SelectedIndexChanged() : " + ex.ToString());
            }
        }
	}
}
