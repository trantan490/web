using System;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Miracom.SmartWeb.FWX;
using System.Drawing;

namespace Miracom.SmartWeb.UI
{
    public partial class SEC1103 : Miracom.SmartWeb.FWX.udcUIBase
	{
        /// <summary>
        /// Class              : SEC1103<br/>
        /// Class Description  : Function Setup Class<br/>
        /// Developer          : W.Y Choi<br/>
        /// Create Date        : 2008-10-17<br/>
        /// Detail Description : Function Setup Class<br/>
        /// Modify             : 1. <br/>
        /// </summary>
        /// 2016-05-23-임종우 : 화면 개발 요청자 추가.
        /// 2016-05-27-임종우 : 삭제 정보 추가.
        /// 2016-05-27-임종우 : 삭제 된 메뉴는 색상 표시 함.
        /// 2016-05-30-임종우 : 삭제 버튼 클릭하면 데이터 삭제가 아닌 DEL User, Time 업데이트 되도록 변경
		public SEC1103()
		{
			InitializeComponent();
        }

        #region " Constant Definition "

        #endregion

        #region " Variable Definition "

        #endregion

        #region " Function Definition "

        /// <summary>
        /// Select Function List.
        /// </summary>
        /// <param name="ctlList">View Control Object</param>
        /// <returns>bool : True or False</returns>
        private bool ViewFunctionList(ListView ctlList)
        {
            string funcName, funcDesc;

            try
            {
                DataTable rtDataTable = new DataTable();

                rtDataTable = CmnFunction.oComm.FillData("RWEBFUNDEF", "1", "");
                if (rtDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        funcName = rtDataTable.Rows[i]["FUNC_NAME"].ToString().TrimEnd();
                        funcDesc = rtDataTable.Rows[i]["FUNC_DESC"].ToString().TrimEnd();

                        ListViewItem itmX = null;
                        itmX = new ListViewItem(funcName, (int)SMALLICON_INDEX.IDX_REPORT_FUNCTION);
                        if (ctlList.Columns.Count > 1)
                        {
                            itmX.SubItems.Add(funcDesc);
                        }
                        ctlList.Items.Add(itmX);

                        // 2016-05-27-임종우 : 삭제 된 메뉴는 색상 표시 함.
                        if (rtDataTable.Rows[i]["DELETE_TIME"].ToString() != " ")
                        {
                            ctlList.Items[i].ForeColor = Color.Pink;
                        }
                    }
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    rtDataTable.Dispose();
                }

                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1103.ViewFunctionList() : " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <returns>bool : True or False</returns>
        private bool FindFunctionInfo(string funcName)
        {
            string QueryCond, value = "";

            try
            {
                DataTable rtDataTable = new DataTable();

                QueryCond = "";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, funcName);

                rtDataTable = CmnFunction.oComm.SelectData("RWEBFUNDEF", "1", QueryCond);
                if (rtDataTable.Rows.Count > 0)
                {
                    txtFunc.Text = rtDataTable.Rows[0]["FUNC_NAME"].ToString().Trim();
                    txtDesc.Text = rtDataTable.Rows[0]["FUNC_DESC"].ToString().Trim();
                    
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

                    // 2016-05-23-임종우 : 화면 개발 요청자 추가
                    value = rtDataTable.Rows[0]["HELP_URL"].ToString().Trim();
                    if (value != "")
                    {
                        txtRequester.Text = value;
                    }
                    else
                    {
                        txtRequester.Text = "";
                    }

                    // 2016-05-27-임종우 : 삭제 User 정보 추가
                    value = rtDataTable.Rows[0]["DELETE_USER_ID"].ToString().Trim();
                    if (value != "")
                    {
                        txtDeleteUser.Text = value;
                    }
                    else
                    {
                        txtDeleteUser.Text = "";
                    }

                    // 2016-05-27-임종우 : 삭제 시간 정보 추가
                    value = rtDataTable.Rows[0]["DELETE_TIME"].ToString().Trim();
                    if (value != "")
                    {
                        txtDeleteTime.Text = CmnFunction.ChangeDateTimeString(value, "yyyyMMddHHmmss", "yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        txtDeleteTime.Text = "";
                    }

                    // 2019-11-22-임종우 : LANGUAGE 부분 추가
                    value = rtDataTable.Rows[0]["LANGUAGE_1"].ToString().Trim();
                    if (value != "")
                    {
                        txtLang1.Text = value;
                    }
                    else
                    {
                        txtLang1.Text = "";
                    }

                    value = rtDataTable.Rows[0]["LANGUAGE_2"].ToString().Trim();
                    if (value != "")
                    {
                        txtLang2.Text = value;
                    }
                    else
                    {
                        txtLang2.Text = "";
                    }

                    //// 20200130 chulho.wee ; lang 4 (vietnam), 5, etc added
                    value = rtDataTable.Rows[0]["LANGUAGE_3"].ToString().Trim();
                    if (value != "")
                    {
                        txtLang3.Text = value;
                    }
                    else
                    {
                        txtLang3.Text = "";
                    }

                    //// 20200130 chulho.wee ; lang 4 (brazil), 5, etc added
                    value = rtDataTable.Rows[0]["LANGUAGE_4"].ToString().Trim();
                    if (value != "")
                    {
                        txtLang4.Text = value;
                    }
                    else
                    {
                        txtLang4.Text = "";
                    }

                    //// 20200130 chulho.wee ; lang 5 (etcm), 5, etc added
                    value = rtDataTable.Rows[0]["LANGUAGE_5"].ToString().Trim();
                    if (value != "")
                    {
                        txtLang5.Text = value;
                    }
                    else
                    {
                        txtLang5.Text = "";
                    }
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    rtDataTable.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1103.FindFunctionInfo() : " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Insert RWEBGRPFUN Table.
        /// </summary>
        /// <returns>bool : True or False</returns>
        private bool Create_Function()
        {
            string QueryCond, InsertValue = "";
            string sysCurrentDate;
            DataTable rtDataTable = new DataTable();

            try
            {
                QueryCond = "";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtFunc.Text.Trim());

                rtDataTable = CmnFunction.oComm.SelectData("RWEBFUNDEF", "1", QueryCond);
                if (rtDataTable.Rows.Count > 0)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD019", GlobalVariable.gcLanguage));
                    return false;
                }

                sysCurrentDate = "";
                sysCurrentDate = CmnFunction.GetSysDateTime();
                if (sysCurrentDate == null)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                InsertValue = "";
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, txtFunc.Text.Trim());
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, txtDesc.Text.Trim());
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, txtRequester.Text.Trim()); // 요청자 정보 추가
               
                //CREATE_USER_ID
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, GlobalVariable.gsUserID);
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, sysCurrentDate);
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, " ");

                //2019-11-22-임종우 : Language 저장 추가
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, txtLang1.Text.Trim());
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, txtLang2.Text.Trim());
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, txtLang3.Text.Trim());

                //// 20200130 chulho.wee ; lang 2ea added. vietnam, brazil
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, txtLang4.Text.Trim());
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, txtLang5.Text.Trim());

                if (CmnFunction.oComm.InsertData("RWEBFUNDEF", "1", InsertValue) != true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }
 
                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1103.Create_Function() : " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Update RWEBGRPFUN Table.
        /// </summary>
        /// <param name="functionName">Function name</param>
        /// <returns>bool : True or False</returns>
        private bool Update_Function(string functionName)
        {
            string QueryCond, UpdateValue = "";
            string sysCurrentDate;
            DataTable rtDataTable = new DataTable();

            try
            {

                QueryCond = "";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, functionName);

                rtDataTable = CmnFunction.oComm.SelectData("RWEBFUNDEF", "1", QueryCond);
                if (rtDataTable.Rows.Count <= 0)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD018", GlobalVariable.gcLanguage));
                    return false;
                }

                sysCurrentDate = "";
                sysCurrentDate = CmnFunction.GetSysDateTime();
                if (sysCurrentDate == null)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                UpdateValue = "";
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, functionName);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtDesc.Text.Trim());
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtRequester.Text.Trim()); // 요청자 정보 추가
                
                //CREATE_USER_ID
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtCreateUser.Text.Trim());
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, CmnFunction.ChangeDateTimeString(txtCreateTime.Text, "yyyy-MM-dd HH:mm:ss", "yyyyMMddHHmmss"));
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsUserID);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, sysCurrentDate);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtDeleteUser.Text.Trim());
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, CmnFunction.ChangeDateTimeString(txtDeleteTime.Text, "yyyy-MM-dd HH:mm:ss", "yyyyMMddHHmmss"));

                //2019-11-22-임종우 : Language 부분 추가
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtLang1.Text.Trim());
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtLang2.Text.Trim());
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtLang3.Text.Trim());

                //// 20200130 chulho.wee ; lang 2ea added. vietnam, brazil
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtLang4.Text.Trim());
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtLang5.Text.Trim());

                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, functionName);

                if (CmnFunction.oComm.UpdateData("RWEBFUNDEF", "1", UpdateValue) != true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1103.Update_Function() : " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Delete RWEBGRPFUN Table.
        /// </summary>
        /// <param name="functionName">Function name</param>
        /// <returns>bool : True or False</returns>
        private bool Delete_Function(string functionName)
        {
            string QueryCond = null;
            string UpdateValue = "";
            string sysCurrentDate;
            DataTable rtDataTable = new DataTable();

            try
            {

                QueryCond = "";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, functionName);

                rtDataTable = CmnFunction.oComm.SelectData("RWEBFUNDEF", "1", QueryCond);
                if (rtDataTable.Rows.Count <= 0)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD018", GlobalVariable.gcLanguage));
                    return false;
                }

                sysCurrentDate = "";
                sysCurrentDate = CmnFunction.GetSysDateTime();
                if (sysCurrentDate == null)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                UpdateValue = "";
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, functionName);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtDesc.Text.Trim());
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, " ");
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtRequester.Text.Trim()); // 요청자 정보 추가
                                
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtCreateUser.Text.Trim());
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, CmnFunction.ChangeDateTimeString(txtCreateTime.Text, "yyyy-MM-dd HH:mm:ss", "yyyyMMddHHmmss"));
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtUpdateUser.Text.Trim());
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, CmnFunction.ChangeDateTimeString(txtUpdateUser.Text, "yyyy-MM-dd HH:mm:ss", "yyyyMMddHHmmss"));
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsUserID);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, sysCurrentDate);                

                //2019-11-22-임종우 : Language 부분 추가
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtLang1.Text.Trim());
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtLang2.Text.Trim());
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtLang3.Text.Trim());

                //// 20200130 chulho.wee ; lang 2ea added. vietnam, brazil
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtLang4.Text.Trim());
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, txtLang5.Text.Trim());

                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, functionName);

                // 2016-05-30-임종우 : 실제 데이터를 삭제 하지 않고 업데이트로 변경 함.
                if (CmnFunction.oComm.UpdateData("RWEBFUNDEF", "1", UpdateValue) != true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }
                
                //if (CmnFunction.oComm.DeleteData("RWEBFUNDEF", "1", QueryCond) != true)
                //{
                //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                //    return false;
                //}

                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1103.Delete_Function() : " + ex.ToString());
                return false;
            }
        }

        #endregion

        private void SEC1103_Load(object sender, EventArgs e)
        {
            lisFunc.SmallImageList = ((System.Windows.Forms.TabControl)((System.Windows.Forms.TabPage)this.Parent).Parent).ImageList;
            CmnFunction.oComm.SetUrl();
            CmnInitFunction.InitListView(lisFunc);
            ViewFunctionList(lisFunc);
        }

        private void SEC1103_Resize(object sender, EventArgs e)
        {
            tableLayoutPanel1.Width = this.Width - 10;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFunc.Text.Trim().Length == 0) CmnFunction.ShowMsgBox("Function Name : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                if (Create_Function() == true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF001", GlobalVariable.gcLanguage));

                    ListViewItem lstItem;

                    lstItem = lisFunc.Items.Add(txtFunc.Text.Trim(), (int)SMALLICON_INDEX.IDX_REPORT_FUNCTION);
                    lstItem.SubItems.Add(txtDesc.Text.Trim());
                    lstItem.Selected = true;
                    lisFunc.Sorting = SortOrder.Ascending;
                    lisFunc.Sort();
                    lstItem.EnsureVisible();
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1103.btnCreate_Click() : " + ex.ToString());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFunc.Text.Trim().Length == 0) CmnFunction.ShowMsgBox("Function Name : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                if (CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF003", GlobalVariable.gcLanguage), "Update", MessageBoxButtons.YesNo, 2) != System.Windows.Forms.DialogResult.Yes) return;
                if (Update_Function(txtFunc.Text.Trim()) == true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF001", GlobalVariable.gcLanguage));

                    CmnInitFunction.FieldClear(this, null, false);
                    lisFunc.SelectedIndices.Clear();
                    lisFunc.Items.Clear();
                    ViewFunctionList(lisFunc);
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1103.btnUpdate_Click() : " + ex.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFunc.Text.Trim().Length == 0) CmnFunction.ShowMsgBox("Function Name : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                if (CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF002", GlobalVariable.gcLanguage), "Delete", MessageBoxButtons.YesNo, 2) != System.Windows.Forms.DialogResult.Yes) return;
                if (Delete_Function(txtFunc.Text.Trim()) == true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF001", GlobalVariable.gcLanguage));

                    CmnInitFunction.FieldClear(this, null, false);
                    lisFunc.SelectedIndices.Clear();
                    lisFunc.Items.Clear();
                    ViewFunctionList(lisFunc);
                    //lisFunc.Items.Remove(lisFunc.SelectedItems[0]);
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1103.btnDelete_Click() : " + ex.Message);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                CmnExcelFunction.ExportToExcel(lisFunc, "Function List In Web Report", "", true, false, false, -1, -1);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1103.btnExcel_Click() : " + ex.ToString());
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
                CmnFunction.ShowMsgBox("SEC1103.btnClose_Click() : " + ex.ToString());
            }
        }

        private void lisFunc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lisFunc.SelectedIndices.Count > 0)
            {
                FindFunctionInfo(lisFunc.SelectedItems[0].Text);
            }
        }
	}
}
