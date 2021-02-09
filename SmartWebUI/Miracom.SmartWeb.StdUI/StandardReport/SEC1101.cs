using System;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.UI
{
    public partial class SEC1101 : Miracom.SmartWeb.FWX.udcUIBase
	{
        /// <summary>
        /// Class              : SEC1101<br/>
        /// Class Description  : User Setup Class<br/>
        /// Developer          : W.Y Choi<br/>
        /// Create Date        : 2008-10-17<br/>
        /// Detail Description : User Setup Class<br/>
        /// Modify             : 1. <br/>
        /// </summary>
        /// 
        public SEC1101()
		{
			InitializeComponent();
            ChkCustomer_Menu();
        }


        #region " Constant Definition "

        #endregion

        #region " Variable Definition "

        #endregion

        #region " Function Definition "

        private bool ChkCustomer()
        {
            if (cdvSecGrpId.Text == "CUSTOM_GROUP" && cdvCustomer.Text == "")
            {
                MessageBox.Show("Customer Code를 반드시 지정해 주십시요", "경고");
                return false;
            }

            return true;
        }
        

        private void ChkCustomer_Menu()
        {
            if (cdvSecGrpId.Text == "CUSTOM_GROUP")
            {
                lblCustomer.Enabled = true;
                cdvCustomer.Enabled = true;
            }
            else
            {
                lblCustomer.Enabled = false;
                cdvCustomer.Enabled = false;
            }
        }
        
        /// <summary>
        /// Select UserID detail.
        /// </summary>
        /// <param name="factoryId">FactoryID value</param>
        /// <param name="userID">UserID value</param>
        /// <returns>bool : True or False</returns>
        private bool FindUserInfo(string factoryId, string userID)
        {
            string value = "";

            try
            {
                DataTable rtDataTable = new DataTable();

                string QueryCond = null;
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, factoryId);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, userID);

                rtDataTable = CmnFunction.oComm.SelectData("RWEBUSRDEF", "1", QueryCond);

                if (rtDataTable.Rows.Count > 0)
                {
                    txtUserId.Text = rtDataTable.Rows[0]["USER_ID"].ToString().Trim();
                    txtUserDesc.Text = rtDataTable.Rows[0]["USER_DESC"].ToString().Trim();
                    txtPassword.Text = rtDataTable.Rows[0]["PASSWORD"].ToString().Trim();
                    cdvSecGrpId.Text = rtDataTable.Rows[0]["SEC_GRP_ID"].ToString().Trim();
                    txtEmail.Text = rtDataTable.Rows[0]["EMAIL_ID"].ToString().Trim();
                    txtPhoneOffice.Text = rtDataTable.Rows[0]["PHONE_OFFICE"].ToString().Trim();
                    txtPhoneMobile.Text = rtDataTable.Rows[0]["PHONE_MOBILE"].ToString().Trim();
                    txtPhoneHome.Text = rtDataTable.Rows[0]["PHONE_HOME"].ToString().Trim();
                    txtPhoneOther.Text = rtDataTable.Rows[0]["PHONE_OTHER"].ToString().Trim();
                    cdvCustomer.Text = rtDataTable.Rows[0]["USER_GRP_3"].ToString().Trim();
                    if (rtDataTable.Rows[0]["SEX_FLAG"].ToString().Trim() == "M")
                    {
                        rbnMale.Checked = true;
                        rbnFemale.Checked = false;
                    }
                    else
                    {
                        rbnMale.Checked = false;
                        rbnFemale.Checked = true;
                    }

                    value = rtDataTable.Rows[0]["CHG_PASS_FLAG"].ToString().Trim();
                    if (value == "Y")
                    {
                        chkChangePassFlag.Checked = true;
                    }
                    else
                    {
                        chkChangePassFlag.Checked = false;
                    }

                    dtpBirthDate.CustomFormat = " ";
                    value = rtDataTable.Rows[0]["BIRTHDAY"].ToString().Trim();
                    if (value != "")
                    {
                        DateTime dtTemp;
                        if (CmnFunction.StringToDateTime(value, "yyyyMMdd", out dtTemp))
                        {
                            dtpBirthDate.Text = dtTemp.ToString();
                        }
                    }

                    dtpEnterDate.CustomFormat = " ";
                    value = rtDataTable.Rows[0]["ENTER_DATE"].ToString().Trim();
                    if (value != "")
                    {
                        DateTime dtTemp;
                        if (CmnFunction.StringToDateTime(value, "yyyyMMdd", out dtTemp))
                        {
                            dtpEnterDate.Text = dtTemp.ToString();
                        }
                    }

                    dtpRetireDate.CustomFormat = " ";
                    value = rtDataTable.Rows[0]["RETIRE_DATE"].ToString().Trim();
                    if (value != "")
                    {
                        DateTime dtTemp;
                        if (CmnFunction.StringToDateTime(value, "yyyyMMdd", out dtTemp))
                        {
                            dtpRetireDate.Text = dtTemp.ToString();
                        }
                    }

                    value = rtDataTable.Rows[0]["CREATE_USER_ID"].ToString().Trim();
                    if (value != "")
                    {
                        txtCreateUser.Text = value;
                    }
                    else
                    {
                        txtCreateUser.Text = "";
                    }

                    value = rtDataTable.Rows[0]["CREATE_TIME"].ToString().Trim();
                    if (value != "")
                    {
                        txtCreateTime.Text = CmnFunction.ChangeDateTimeString(value, "yyyyMMddHHmmss", "yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        txtCreateTime.Text = "";
                    }

                    value = rtDataTable.Rows[0]["UPDATE_USER_ID"].ToString().Trim();
                    if (value != "")
                    {
                        txtUpdateUser.Text = value;
                    }
                    else
                    {
                        txtUpdateUser.Text = "";
                    }

                    value = rtDataTable.Rows[0]["UPDATE_TIME"].ToString().Trim();
                    if (value != "")
                    {
                        txtUpdateTime.Text = CmnFunction.ChangeDateTimeString(value, "yyyyMMddHHmmss", "yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        txtUpdateTime.Text = "";
                    }
                }
                else
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD013", GlobalVariable.gcLanguage));
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    rtDataTable.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1101.FindUserInfo() : " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Insert RWEBUSRDEF Table.
        /// </summary>
        /// <returns>bool : True or False</returns>
        private bool Create_Security_User()
        {
            string sysCurrentDate, tempValue = "";
            DataTable rtDataTable = new DataTable();

            try
            {
                string QueryCond;
                string InsertValue;

                QueryCond = "";

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvSecGrpId.Text);

                rtDataTable = CmnFunction.oComm.SelectData("RWEBGRPDEF", "1", QueryCond);
                if (rtDataTable.Rows.Count <= 0)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD014", GlobalVariable.gcLanguage));
                    return false;
                }

                QueryCond = "";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtUserId.Text);

                rtDataTable = CmnFunction.oComm.SelectData("RWEBUSRDEF", "1", QueryCond);
                if (rtDataTable.Rows.Count > 0)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD015", GlobalVariable.gcLanguage));
                    return false;
                }
                else
                {
                    QueryCond = "";
                    sysCurrentDate = "";
                    sysCurrentDate = CmnFunction.oComm.GetFuncDataString("GetSysTime", QueryCond);
                    if (sysCurrentDate == null)
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                        return false;
                    }

                    InsertValue = "";
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, GlobalVariable.gsCentralFactory);
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, txtUserId.Text.Trim());
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, txtUserDesc.Text.Trim());
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, txtPassword.Text.Trim());
                    if (chkChangePassFlag.Checked == true)
                    {
                        InsertValue = FwxCmnFunction.PackCondition(InsertValue, "Y");
                    }
                    else
                    {
                        InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                    }

                    //USER_GRP_?
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, cdvCustomer.Text.Trim());
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                    //USER_CMF_?
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
                    //SEC_GRP_ID
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, cdvSecGrpId.Text.Trim());
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, txtPhoneOffice.Text.Trim());
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, txtPhoneMobile.Text.Trim());
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, txtPhoneHome.Text.Trim());
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, txtPhoneOther.Text.Trim());

                    tempValue = CmnFunction.ChangeDateTimeString(dtpEnterDate.Text, "yyyy-MM-dd", "yyyyMMdd");
                    if (tempValue != "")
                    {
                        InsertValue = FwxCmnFunction.PackCondition(InsertValue, tempValue);
                    }
                    else
                    {
                        InsertValue = FwxCmnFunction.PackCondition(InsertValue, "");
                    }

                    tempValue = CmnFunction.ChangeDateTimeString(dtpRetireDate.Text, "yyyy-MM-dd", "yyyyMMdd");
                    if (tempValue != "")
                    {
                        InsertValue = FwxCmnFunction.PackCondition(InsertValue, tempValue);
                    }
                    else
                    {
                        InsertValue = FwxCmnFunction.PackCondition(InsertValue, "");
                    }

                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, txtEmail.Text.Trim());

                    tempValue = CmnFunction.ChangeDateTimeString(dtpBirthDate.Text, "yyyy-MM-dd", "yyyyMMdd");
                    if (tempValue != "")
                    {
                        InsertValue = FwxCmnFunction.PackCondition(InsertValue, tempValue);
                    }
                    else
                    {
                        InsertValue = FwxCmnFunction.PackCondition(InsertValue, "");
                    }

                    //SEX_FLAG
                    if (rbnFemale.Checked == true)
                    {
                        InsertValue = FwxCmnFunction.PackCondition(InsertValue, "F");
                    }
                    else if (rbnMale.Checked == true)
                    {
                        InsertValue = FwxCmnFunction.PackCondition(InsertValue, "M");
                    }

                    //CREATE_USER_ID
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, GlobalVariable.gsUserID);
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, sysCurrentDate);
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, "");
                    InsertValue = FwxCmnFunction.PackCondition(InsertValue, "");

                    if (CmnFunction.oComm.InsertData("RWEBUSRDEF", "1", InsertValue) != true)
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1101.Create_Security_User() : " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Update RWEBUSRDEF Table.
        /// </summary>
        /// <param name="secUserId">Update UserID value</param>
        /// <returns>bool : True or False</returns>
        private bool Update_Security_User(string secUserId)
        {
            string sysCurrentDate, tempValue = "";
            DataTable rtDataTable = new DataTable();

            if (ChkCustomer() == false)
                return false;
            
            try
            {
                string QueryCond;
                string UpdateValue;

                QueryCond = "";

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvSecGrpId.Text);

                rtDataTable = CmnFunction.oComm.SelectData("RWEBGRPDEF", "1", QueryCond);
                if (rtDataTable.Rows.Count <= 0)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD014", GlobalVariable.gcLanguage));
                    return false;
                }

                QueryCond = "";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtUserId.Text);

                rtDataTable = CmnFunction.oComm.SelectData("RWEBUSRDEF", "1", QueryCond);
                if (rtDataTable.Rows.Count <= 0)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD013", GlobalVariable.gcLanguage));
                    return false;
                }
                else
                {
                    QueryCond = "";
                    sysCurrentDate = "";
                    sysCurrentDate = CmnFunction.oComm.GetFuncDataString("GetSystemTime", QueryCond);
                    if (sysCurrentDate == null)
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                        return false;
                    }

                    UpdateValue = "";
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsCentralFactory);
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtUserId.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtUserDesc.Text.Trim());
                    //UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtPassword.Text.Trim());
                    if (chkChangePassFlag.Checked == true)
                    {
                        UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "Y");
                    }
                    else
                    {
                        UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "");
                    }

                    //USER_GRP_?
                    // 2011-08-26-임종우 : 사용자 부서, 직급, 외부접속 허용 데이터를 업데이트 시 기존에 초기화 시키고 있음. -> 기존 데이터 저장하도록 변경.                    
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, rtDataTable.Rows[0][5].ToString());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, rtDataTable.Rows[0][6].ToString());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, cdvCustomer.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "");
                    //USER_CMF_?
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "");
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, rtDataTable.Rows[0][24].ToString());
                    //SEC_GRP_ID
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, cdvSecGrpId.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtPhoneOffice.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtPhoneMobile.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtPhoneHome.Text.Trim());
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtPhoneOther.Text.Trim());

                    tempValue = CmnFunction.ChangeDateTimeString(dtpEnterDate.Text, "yyyy-MM-dd", "yyyyMMdd");
                    if (tempValue != "")
                    {
                        UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, tempValue);
                    }
                    else
                    {
                        UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "");
                    }

                    tempValue = CmnFunction.ChangeDateTimeString(dtpRetireDate.Text, "yyyy-MM-dd", "yyyyMMdd");
                    if (tempValue != "")
                    {
                        UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, tempValue);
                    }
                    else
                    {
                        UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "");
                    }

                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtEmail.Text.Trim());

                    tempValue = CmnFunction.ChangeDateTimeString(dtpBirthDate.Text, "yyyy-MM-dd", "yyyyMMdd");
                    if (tempValue != "")
                    {
                        UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, tempValue);
                    }
                    else
                    {
                        UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "");
                    }

                    //SEX_FLAG
                    if (rbnFemale.Checked == true)
                    {
                        UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "F");
                    }
                    else if (rbnMale.Checked == true)
                    {
                        UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, "M");
                    }

                    //CREATE_USER_ID
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtCreateUser.Text);
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, CmnFunction.ChangeDateTimeString(txtCreateTime.Text, "yyyy-MM-dd HH:mm:ss", "yyyyMMddHHmmss"));
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsUserID);
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, sysCurrentDate);
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsCentralFactory);
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtUserId.Text.Trim());

                    if (CmnFunction.oComm.UpdateData("RWEBUSRDEF", "1", UpdateValue) != true)
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1101.Update_Security_User() : " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Delete RWEBUSRDEF Table.
        /// </summary>
        /// <param name="secUserId">Delete UserID value</param>
        /// <returns>bool : True or False</returns>
        private bool Delete_Security_User(string secUserId)
        {
            DataTable rtDataTable = new DataTable();

            try
            {
                string DeleteCond;

                DeleteCond = "";

                DeleteCond = FwxCmnFunction.PackCondition(DeleteCond, GlobalVariable.gsCentralFactory);
                DeleteCond = FwxCmnFunction.PackCondition(DeleteCond, secUserId);

                rtDataTable = CmnFunction.oComm.SelectData("RWEBUSRDEF", "1", DeleteCond);
                if (rtDataTable.Rows.Count <= 0)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD013", GlobalVariable.gcLanguage));
                    return false;
                }
                else
                {
                    if (CmnFunction.oComm.DeleteData("RWEBUSRDEF", "1", DeleteCond) != true)
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1101.Delete_Security_User() : " + ex.ToString());
                return false;
            }
        }
        
        #endregion
        
        private void SEC1101_Load(object sender, EventArgs e)
        {
            lisUser.SmallImageList = ((System.Windows.Forms.TabControl)((System.Windows.Forms.TabPage)this.Parent).Parent).ImageList;
            CmnFunction.oComm.SetUrl(); 
            CmnInitFunction.InitListView(lisUser);
            CmnListFunction.ViewSecUserList(lisUser);
        }

        private void SEC1101_Resize(object sender, EventArgs e)
        {
            tableLayoutPanel1.Width = this.Width - 10;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserId.Text.Trim().Length == 0) CmnFunction.ShowMsgBox("User ID : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                if (txtPassword.Text.Trim().Length == 0) CmnFunction.ShowMsgBox("Password : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                if (cdvSecGrpId.Text.Trim().Length == 0) CmnFunction.ShowMsgBox("Secutiry Group : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                if (Create_Security_User() == true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF001", GlobalVariable.gcLanguage));
                    
                    ListViewItem lstItem;
                    lstItem = lisUser.Items.Add(txtUserId.Text.Trim(), (int)SMALLICON_INDEX.IDX_USER);
                    lstItem.SubItems.Add(txtUserDesc.Text.Trim());
                    lstItem.Selected = true;
                    lisUser.Sorting = SortOrder.Ascending;
                    lisUser.Sort();
                    lstItem.EnsureVisible();
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1101.btnCreate_Click() : " + ex.ToString());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserId.Text.Trim().Length == 0) CmnFunction.ShowMsgBox("User ID : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                if (txtPassword.Text.Trim().Length == 0) CmnFunction.ShowMsgBox("Password : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                if (cdvSecGrpId.Text.Trim().Length == 0) CmnFunction.ShowMsgBox("Secutiry Group : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                if (CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF003", GlobalVariable.gcLanguage), "Update", MessageBoxButtons.YesNo, 2) != System.Windows.Forms.DialogResult.Yes) return;
                if (Update_Security_User(txtUserId.Text.Trim()) == true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF001", GlobalVariable.gcLanguage));
                 
                    CmnInitFunction.FieldClear(this, null, false);
                    CmnListFunction.ViewSecUserList(lisUser);
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1101.btnUpdate_Click() : " + ex.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserId.Text.Trim().Length == 0) CmnFunction.ShowMsgBox("User ID : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                if (CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF002", GlobalVariable.gcLanguage), "Delete", MessageBoxButtons.YesNo, 2) != System.Windows.Forms.DialogResult.Yes) return;
                if (Delete_Security_User(txtUserId.Text.Trim()) == true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF001", GlobalVariable.gcLanguage));

                    CmnInitFunction.FieldClear(this, null, false);
                    lisUser.Items.Remove(lisUser.SelectedItems[0]);
                }
                
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1101.btnDelete_Click() : " + ex.Message);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                CmnExcelFunction.ExportToExcel(lisUser, "User List In Web Report", "", true, false, false, -1, -1);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1101.btnExcel_Click() : " + ex.ToString());
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
                CmnFunction.ShowMsgBox("SEC1101.btnClose_Click() : " + ex.ToString());
            }
        }

        private void lisUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lisUser.SelectedIndices.Count > 0)
            {
                FindUserInfo(GlobalVariable.gsCentralFactory, lisUser.SelectedItems[0].Text);
            }
        }

        private void dtpBirthDate_ValueChanged(object sender, EventArgs e)
        {
            if (this.dtpBirthDate.CustomFormat.Trim() == "")
            {
                this.dtpBirthDate.CustomFormat = "yyyy-MM-dd";
            }
        }

        private void dtpBirthDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                this.dtpBirthDate.Value = this.dtpBirthDate.MinDate;
                this.dtpBirthDate.CustomFormat = " ";
            }
        }

        private void dtpEnterDate_ValueChanged(object sender, EventArgs e)
        {
            if (this.dtpEnterDate.CustomFormat.Trim() == "")
            {
                this.dtpEnterDate.CustomFormat = "yyyy-MM-dd";
            }
        }

        private void dtpEnterDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                this.dtpEnterDate.Value = this.dtpEnterDate.MinDate;
                this.dtpEnterDate.CustomFormat = " ";
            }
        }

        private void dtpRetireDate_ValueChanged(object sender, EventArgs e)
        {
            if (this.dtpRetireDate.CustomFormat.Trim() == "")
            {
                this.dtpRetireDate.CustomFormat = "yyyy-MM-dd";
            }
        }

        private void dtpRetireDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                this.dtpRetireDate.Value = this.dtpRetireDate.MinDate;
                this.dtpRetireDate.CustomFormat = " ";
            }
        }

        private void dtpExpireDate_ValueChanged(object sender, EventArgs e)
        {
            if (this.dtpExpireDate.CustomFormat.Trim() == "")
            {
                this.dtpExpireDate.CustomFormat = "yyyy-MM-dd";
            }
        }

        private void dtpExpireDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                this.dtpExpireDate.Value = this.dtpExpireDate.MinDate;
                this.dtpExpireDate.CustomFormat = " ";
            }
        }

        private void cdvSecGrpId_ButtonPress(object sender, EventArgs e)
        {
            cdvSecGrpId.Init();
            cdvSecGrpId.Columns.Add("Group", 100);
            cdvSecGrpId.Columns.Add("Group Desc", 100);
            cdvSecGrpId.SelectedSubItemIndex = 0;
            CmnListFunction.ViewSecGroupList(cdvSecGrpId.GetListView);
        }

        private void cdvCustomer_ButtonPress(object sender, EventArgs e)
        {
            cdvCustomer.Init();
            cdvCustomer.Columns.Add("Group", 100);
            cdvCustomer.Columns.Add("Group Desc", 100);
            cdvCustomer.SelectedSubItemIndex = 0;
            RptListFunction.ViewGCMTableList(cdvCustomer.GetListView, GlobalVariable.gsAssyDefaultFactory, "H_CUSTOMER");
        }

        private void cdvSecGrpId_SelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            ChkCustomer_Menu();
        }

        private void cdvCustomer_TextBoxTextChanged(object sender, EventArgs e)
        {
            ChkCustomer_Menu();
        }

        private void TxtUsrName_TextChanged(object sender, EventArgs e)
        {
           // 병주 : 일일이 찾기 귀찮아서 하나 만듬 ㅋ
            for (int i = 0; i < lisUser.Items.Count ; i++)
            {
                if (TxtUsrName.Text.Length != lisUser.Items[i].Text.Length) continue;
                if (TxtUsrName.Text == lisUser.Items[i].Text.Substring(0, TxtUsrName.Text.Length))
                {
                   lisUser.Items[i].Selected = true;
                }
            }  
        }

        // 2010-06-02-임종우 : 사용자 이름 검색 기능 추가
        private void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lisUser.Items.Count; i++)
            {
                if (txtSearchName.Text.Length != lisUser.Items[i].SubItems[1].Text.Length) continue;
                if (txtSearchName.Text == lisUser.Items[i].SubItems[1].Text)
                {
                    lisUser.Items[i].Selected = true;
                }
            }
        }
    }
}
