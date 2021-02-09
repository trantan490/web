using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.UI
{
    public partial class SEC1104 : Miracom.SmartWeb.FWX.udcUIBase
	{
        /// <summary>
        /// Class              : SEC1104<br/>
        /// Class Description  : Menu Setup And Attach Function to Menu Class<br/>
        /// Developer          : W.Y Choi<br/>
        /// Create Date        : 2008-10-17<br/>
        /// Detail Description : Menu Setup And Attach Function to Menu Class<br/>
        /// Modify             : 1. <br/>
        /// </summary>
        /// 
		public SEC1104()
		{
			InitializeComponent();
        }

        #region " Constant Definition "

        #endregion

        #region " Variable Definition "

        #endregion

        #region " Function Definition "

        /// <summary>
        /// View_Function_List
        /// </summary>
        /// <param name="_step"></param>
        /// <param name="ctlList"></param>
        /// <returns></returns>
        private bool View_Function_List(int _step, System.Windows.Forms.Control ctlList)
        {
            string menuName, menuTemp = "";
            string funcName, funcDesc;

            TreeNode rootNode = new TreeNode();
            TreeNode childNode = new TreeNode();

            int iImageIndex = 0;

            try
            {
                DataTable rtDataTable = new DataTable();
                string QueryCond = "";

                switch (_step)
                {
                    case 1: // Avaliable Function list.
                        rtDataTable = CmnFunction.oComm.FillData("RWEBFUNDEF", "1", QueryCond);
                        if (rtDataTable.Rows.Count <= 0)
                        {
                            return false;
                        }
                        break;
                    case 2: // Attached Menu list.
                        QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                        QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvSecGrp.Text.Trim());

                        if (cdvSecGrp.Text.Trim() == null || cdvSecGrp.Text.Trim().Length <= 0)
                        {
                            CmnFunction.ShowMsgBox("Secutity Group ID : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                            return false;
                        }

                        rtDataTable = CmnFunction.oComm.GetFuncDataTable("GetFunctionList", QueryCond);

                        if (rtDataTable.Rows.Count <= 0)
                        {
                            return false;
                        }
                        break;
                    default:
                        break;
                }

                // Control Clear.
                if (ctlList is ListView)
                {
                    if (((ListView)ctlList).MultiSelect == true)
                    {
                        CmnInitFunction.InitListView(((ListView)ctlList));
                        ((ListView)ctlList).MultiSelect = true;
                    }
                    else
                    {
                        CmnInitFunction.InitListView(((ListView)ctlList));
                    }
                }
                else if (ctlList is TreeView)
                {
                    ((TreeView)ctlList).Nodes.Clear();
                }

                // Import Control item list.
                for (int i = 0; i < rtDataTable.Rows.Count; i++)
                {
                    funcName = rtDataTable.Rows[i]["FUNC_NAME"].ToString().TrimEnd();
                    funcDesc = rtDataTable.Rows[i]["FUNC_DESC"].ToString().TrimEnd();

                    if (ctlList is ListView)
                    {
                        ListViewItem itmX = null;
                        itmX = new ListViewItem(funcName, (int)SMALLICON_INDEX.IDX_REPORT_FUNCTION);
                        if (((ListView)ctlList).Columns.Count > 1)
                        {
                            itmX.SubItems.Add(funcDesc);
                        }
                        ((ListView)ctlList).Items.Add(itmX);
                    }
                    else if (ctlList is TreeView)
                    {
                        menuName = rtDataTable.Rows[i]["FUNC_GRP_ID"].ToString().TrimEnd();

                        if ((string)rtDataTable.Rows[i]["FUNC_GRP_ID"] != menuTemp)
                        {
                            menuTemp = (string)rtDataTable.Rows[i]["FUNC_GRP_ID"];
                            menuName = menuTemp;

                            funcDesc = (string)rtDataTable.Rows[i]["FUNC_DESC"];
                            funcName = (string)rtDataTable.Rows[i]["FUNC_NAME"];

                            if (menuName == "Security")
                            {
                                iImageIndex = (int)SMALLICON_INDEX.IDX_REPORT_SEC_GRP;
                            }
                            else
                            {
                                iImageIndex = (int)SMALLICON_INDEX.IDX_REPORT_FUNCTION;
                            }

                            rootNode = ((TreeView)ctlList).Nodes.Add(menuName, LanguageFunction.FindLanguage(menuName, 2), (int)SMALLICON_INDEX.IDX_REPORT_MENU, (int)SMALLICON_INDEX.IDX_REPORT_MENU);
                            rootNode.Tag = menuName;

                            if (rootNode.PrevNode != null)
                            {
                                childNode = rootNode.PrevNode.Nodes.Add("Attach New Function...", "Attach New Function...", (int)SMALLICON_INDEX.IDX_VERSION_REQUEST, (int)SMALLICON_INDEX.IDX_VERSION_REQUEST);
                                childNode.Tag = "Attach New Function...";
                                childNode.ForeColor =  Color.Silver;
                            }
                        }
                        else
                        {
                            funcDesc = (string)rtDataTable.Rows[i]["FUNC_DESC"];
                            funcName = (string)rtDataTable.Rows[i]["FUNC_NAME"];

                            childNode = rootNode.Nodes.Add(funcName, funcName + " : " + LanguageFunction.FindLanguage(funcDesc, 2), iImageIndex, iImageIndex);
                            childNode.Tag = funcName;
                        }
                    }
                }

                if (ctlList is TreeView)
                {
                    rootNode = ((TreeView)ctlList).Nodes.Add("Attach New Menu...", "Attach New Menu...", (int)SMALLICON_INDEX.IDX_VERSION_REQUEST, (int)SMALLICON_INDEX.IDX_VERSION_REQUEST);
                    rootNode.Tag = "Attach New Menu...";
                    rootNode.ForeColor = Color.Silver;

                    childNode = rootNode.PrevNode.Nodes.Add("Attach New Function...", "Attach New Function...", (int)SMALLICON_INDEX.IDX_VERSION_REQUEST, (int)SMALLICON_INDEX.IDX_VERSION_REQUEST);
                    childNode.Tag = "Attach New Function...";
                    childNode.ForeColor = Color.Silver;
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    rtDataTable.Dispose();
                }

                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Add Function group menu
        /// </summary>
        /// <param name="menuName">Function MenuName</param>
        /// <param name="selectedMenu">Insert selected MenuName</param>
        /// <returns>bool : True or False</returns>
        private bool Create_GroupFunctionMenu(string menuName, string selectedMenu)
        {
            int iMenuSeq, iTemp;
            string InsertValue, UpdateValue, QueryCond = "";
            string secGroupName, sysCurrentDate = "";
            DataTable rtDataTable = null;

            try
            {
                secGroupName = cdvSecGrp.Text.Trim();
                if (secGroupName == "")
                {
                    CmnFunction.ShowMsgBox("Secutity Group ID : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                    return false;
                }

                if (txtMenu.Text.Trim() == "")
                {
                    CmnFunction.ShowMsgBox("Function Menu : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                    return false;
                }

                sysCurrentDate = CmnFunction.GetSysDateTime();
                if (sysCurrentDate == null)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                rtDataTable = new DataTable();

                // menuName 중복 확인
                QueryCond = "";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, secGroupName);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, menuName);

                iTemp = CmnFunction.oComm.SelectDataCount("RWEBGRPFUN", "1", QueryCond);
                if (iTemp > 0)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD020", GlobalVariable.gcLanguage));
                    return false;
                }

                rtDataTable.Clear();
                QueryCond = "";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, secGroupName);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, selectedMenu);

                if (selectedMenu == null || selectedMenu == "Attach New Menu...")
                { // 지정된 메뉴위치가 없으면,.. 
                    iMenuSeq = CmnFunction.oComm.SelectDataCount("RWEBGRPFUN", "2", QueryCond) + 1;
                }
                else
                {
                    iMenuSeq = CmnFunction.oComm.SelectDataCount("RWEBGRPFUN", "3", QueryCond);

                    // 기존 메뉴 순서 조정.
                    UpdateValue = "";
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsUserID);
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, sysCurrentDate);
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsCentralFactory);
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, secGroupName);
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, iMenuSeq.ToString());

                    if (CmnFunction.oComm.UpdateData("RWEBGRPFUN", "1", UpdateValue) != true)
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                        return false;
                    }
                }

                //RWEBGRPFUN.FACTORY
                //RWEBGRPFUN.SEC_GRP_ID
                //RWEBGRPFUN.FUNC_GRP_ID
                //RWEBGRPFUN.FUNC_GRP_SEQ
                //RWEBGRPFUN.UPDATE_USER_ID
                //RWEBGRPFUN.UPDATE_TIME
                InsertValue = "";
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, GlobalVariable.gsCentralFactory);
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, secGroupName);
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, menuName);
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, string.Format("{0:D}", iMenuSeq));
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, "");
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, "0");
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, GlobalVariable.gsUserID);
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, sysCurrentDate);
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, "");
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, "");

                if (CmnFunction.oComm.InsertData("RWEBGRPFUN", "1", InsertValue) != true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }
                else
                {
                    TreeNode node;
                    if (selectedMenu != null)
                    {
                        node = tvMenu.Nodes.Insert(tvMenu.Nodes[selectedMenu].Index, menuName, LanguageFunction.FindLanguage(menuName, 2), (int)SMALLICON_INDEX.IDX_REPORT_MENU, (int)SMALLICON_INDEX.IDX_REPORT_MENU);
                        node.Tag = menuName;
                    }
                    else
                    {
                        node = tvMenu.Nodes.Add(menuName, LanguageFunction.FindLanguage(menuName, 2), (int)SMALLICON_INDEX.IDX_REPORT_MENU, (int)SMALLICON_INDEX.IDX_REPORT_MENU);
                        node.Tag = menuName;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1104.Create_GroupFunctionMenu() : " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Update_GroupFunctionMenu
        /// </summary>
        /// <param name="menuName">Function MenuName</param>
        /// <param name="oldMenuName">Befor(old) Function MenuName</param>
        /// <returns>bool : True or False</returns>
        private bool Update_GroupFunctionMenu(string menuName, string oldMenuName)
        {
            int iTemp;
            string secGroupName, sysCurrentDate = "";
            string QueryCond, UpdateValue = "";
            DataTable rtDataTable = null;

            try
            {
                secGroupName = cdvSecGrp.Text.Trim();
                if (secGroupName == "")
                {
                    CmnFunction.ShowMsgBox("Secutity Group ID : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                    return false;
                }

                if (txtMenu.Text.Trim() == "")
                {
                    CmnFunction.ShowMsgBox("Function Menu : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                    return false;
                }

                sysCurrentDate = CmnFunction.GetSysDateTime();
                if (sysCurrentDate == null)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                rtDataTable = new DataTable();

                // menuName 존재 확인
                QueryCond = "";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, secGroupName);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, oldMenuName);

                iTemp = CmnFunction.oComm.SelectDataCount("RWEBGRPFUN", "1", QueryCond);
                if (iTemp <= 0)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD021", GlobalVariable.gcLanguage));
                    return false;
                }

                rtDataTable.Clear();
                QueryCond = "";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, secGroupName);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, menuName);

                iTemp = CmnFunction.oComm.SelectDataCount("RWEBGRPFUN", "1", QueryCond);
                if (iTemp > 0)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD020", GlobalVariable.gcLanguage));
                    return false;
                }

                //RWEBGRPFUN.FACTORY
                //RWEBGRPFUN.SEC_GRP_ID
                //RWEBGRPFUN.FUNC_GRP_ID
                //RWEBGRPFUN.NEW_FUNC_GRP_ID
                //RWEBGRPFUN.UPDATE_USER_ID
                //RWEBGRPFUN.UPDATE_TIME

                UpdateValue = "";
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, menuName);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsUserID);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, sysCurrentDate);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsCentralFactory);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, secGroupName);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, oldMenuName);


                if (CmnFunction.oComm.UpdateData("RWEBGRPFUN", "2", UpdateValue) != true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }
                else
                {
                    TreeNode node;
                    if (((TreeNode)tvMenu.SelectedNodes[0]).Parent == null)
                    {
                        node = (TreeNode)tvMenu.SelectedNodes[0];
                        node.Text = menuName;
                        node.Tag = menuName;
                    }
                    else
                    {
                        node = ((TreeNode)tvMenu.SelectedNodes[0]).Parent;
                        node.Text = menuName;
                        node.Tag = menuName;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1104.Update_GroupFunctionMenu() : " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Delete Function group menu
        /// </summary>
        /// <param name="menuName">Function MenuName</param>
        /// <returns>bool : True or False</returns>
        private bool Delete_GroupFunctionMenu(string menuName)
        {
            int iMenuSeq;
            string QueryCond, DeleteCond, UpdateValue = "";
            string secGroupName, sysCurrentDate = "";
            DataTable rtDataTable = null;

            try
            {
                secGroupName = cdvSecGrp.Text.Trim();
                if (secGroupName == "")
                {
                    CmnFunction.ShowMsgBox("Secutity Group ID : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                    return false;
                }

                if (txtMenu.Text.Trim() == "")
                {
                    CmnFunction.ShowMsgBox("Function Menu : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                    return false;
                }

                rtDataTable = new DataTable();

                // menuName 존재 확인
                QueryCond = "";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, secGroupName);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, menuName);

                iMenuSeq = CmnFunction.oComm.SelectDataCount("RWEBGRPFUN", "3", QueryCond);

                if (iMenuSeq == -1)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD021", GlobalVariable.gcLanguage));
                    return false;
                }

                // MenuName을 사용하는지 확인.
                rtDataTable.Clear();

                DeleteCond = "";
                DeleteCond = FwxCmnFunction.PackCondition(DeleteCond, GlobalVariable.gsCentralFactory);
                DeleteCond = FwxCmnFunction.PackCondition(DeleteCond, secGroupName);
                DeleteCond = FwxCmnFunction.PackCondition(DeleteCond, menuName);

                //RWEBGRPFUN.FACTORY
                //RWEBGRPFUN.SEC_GRP_ID
                //RWEBGRPFUN.FUNC_GRP_ID
                if (CmnFunction.oComm.DeleteData("RWEBGRPFUN", "1", DeleteCond) != true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                // menuName 삭제
                UpdateValue = "";
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsUserID);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, sysCurrentDate);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsCentralFactory);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, secGroupName);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, string.Format("{0:D}", iMenuSeq));

                if (CmnFunction.oComm.UpdateData("RWEBGRPFUN", "3", UpdateValue) == false)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                tvMenu.Nodes.Remove(tvMenu.Nodes[menuName]);

                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1104.Delete_GroupFunctionMenu() : " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Attach Function To Security Group
        /// </summary>
        /// <param name="menuName">Menu Name</param>
        /// <param name="functionName">Function name</param>
        /// <returns>bool : True or False</returns>
        private bool Add_GroupFunction(string menuName, string functionName, string selectedMenu)
        {
            int iMenuSeq, iFuncSeq;
            string QueryCond, UpdateValue = "", InsertValue = "";
            string secGroupName, secMenuName, sysCurrentDate;
            DataTable rtDataTable = null;

            try
            {
                secGroupName = cdvSecGrp.Text.Trim();
                if (secGroupName == "")
                {
                    CmnFunction.ShowMsgBox("Secutity Group ID : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                    return false;
                }

                secMenuName = menuName;
                if (secMenuName == "")
                {
                    CmnFunction.ShowMsgBox("Function Menu : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                    return false;
                }

                sysCurrentDate = CmnFunction.GetSysDateTime();
                if (sysCurrentDate == null)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                rtDataTable = new DataTable();

                // Function 중복 확인
                QueryCond = "";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, secGroupName);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, secMenuName);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, functionName);

                rtDataTable = CmnFunction.oComm.SelectData("RWEBGRPFUN", "2", QueryCond);
                if (rtDataTable.Rows.Count > 0)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD022", GlobalVariable.gcLanguage));
                    return false;
                }
                else
                {
                    // Get menu group의 function sequence
                    QueryCond = "";
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, secGroupName);
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, secMenuName);

                    rtDataTable = CmnFunction.oComm.SelectData("RWEBGRPFUN", "3", QueryCond);

                    if (rtDataTable.Rows.Count <= 0)
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                        return false;
                    }
                    else
                    {
                        iMenuSeq = int.Parse(rtDataTable.Rows[0][0].ToString());
                        iFuncSeq = int.Parse(rtDataTable.Rows[0][1].ToString());
                    }

                    if (selectedMenu != null)
                    {
                        rtDataTable.Clear();
                        // Function 중복 확인
                        QueryCond = "";
                        QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                        QueryCond = FwxCmnFunction.PackCondition(QueryCond, secGroupName);
                        QueryCond = FwxCmnFunction.PackCondition(QueryCond, secMenuName);
                        QueryCond = FwxCmnFunction.PackCondition(QueryCond, selectedMenu);

                        rtDataTable = CmnFunction.oComm.SelectData("RWEBGRPFUN", "2", QueryCond);
                        if (rtDataTable.Rows.Count > 0)
                        {
                            iMenuSeq = int.Parse(rtDataTable.Rows[0]["FUNC_GRP_SEQ"].ToString());
                            iFuncSeq = int.Parse(rtDataTable.Rows[0]["FUNC_SEQ"].ToString()) - 1;

                            // 기존 메뉴 순서 조정.
                            UpdateValue = "";
                            UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsUserID);
                            UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, sysCurrentDate);
                            UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsCentralFactory);
                            UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, secGroupName);
                            UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, secMenuName);
                            UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, iFuncSeq.ToString());

                            if (CmnFunction.oComm.UpdateData("RWEBGRPFUN", "6", UpdateValue) != true)
                            {
                                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                                return false;
                            }
                        }
                    }
                }

                // Function 추가.
                InsertValue = "";
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, GlobalVariable.gsCentralFactory);
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, secGroupName);
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, secMenuName);
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, string.Format("{0:D}", iMenuSeq));
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, functionName);
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, string.Format("{0:D}", iFuncSeq + 1));
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, GlobalVariable.gsUserID);
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, sysCurrentDate);
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, "");
                InsertValue = FwxCmnFunction.PackCondition(InsertValue, "");

                if (CmnFunction.oComm.InsertData("RWEBGRPFUN", "1", InsertValue) != true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1104.Add_GroupFunction() : " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Detach Function From Security Group
        /// </summary>
        /// <param name="menuName">Menu Name</param>
        /// <param name="functionName">Function name</param>
        /// <returns>bool : True or False</returns>
        private bool Delete_GroupFunction(string menuName, string functionName)
        {
            int iMenuSeq, iFuncSeq;
            string QueryCond, UpdateValue = "";
            string secGroupName, secMenuName, sysCurrentDate;
            DataTable rtDataTable = null;

            try
            {
                secGroupName = cdvSecGrp.Text.Trim();
                if (secGroupName == "")
                {
                    CmnFunction.ShowMsgBox("Secutity Group ID : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                    return false;
                }

                secMenuName = menuName;
                if (secMenuName == "")
                {
                    CmnFunction.ShowMsgBox("Function Menu : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                    return false;
                }

                sysCurrentDate = CmnFunction.GetSysDateTime();
                if (sysCurrentDate == null)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                rtDataTable = new DataTable();

                // Function 존재 확인
                QueryCond = "";
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, secGroupName);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, secMenuName);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, functionName);
                rtDataTable = CmnFunction.oComm.SelectData("RWEBGRPFUN", "2", QueryCond);
                if (rtDataTable.Rows.Count <= 0)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD023", GlobalVariable.gcLanguage));
                    return false;
                }
                else
                {
                    // 지정 Function Menu/Function Id의 Sequence
                    iMenuSeq = int.Parse(rtDataTable.Rows[0]["FUNC_GRP_SEQ"].ToString());
                    iFuncSeq = int.Parse(rtDataTable.Rows[0]["FUNC_SEQ"].ToString());
                }

                // Function delete.
                if (CmnFunction.oComm.DeleteData("RWEBGRPFUN", "2", QueryCond) != true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                //FACTORY = paramValues[0];
                //SEC_GRP_ID = paramValues[1];
                //FUNC_GRP_ID = paramValues[2];
                //FUNC_SEQ = Int16.Parse(paramValues[3]);
                //UPDATE_USER_ID = paramValues[4];
                //UPDATE_TIME = paramValues[5];

                // Function sequence decrements.
                UpdateValue = "";
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsUserID);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, sysCurrentDate);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsCentralFactory);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, secGroupName);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, secMenuName);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, string.Format("{0:D}", iFuncSeq));

                if (CmnFunction.oComm.UpdateData("RWEBGRPFUN", "5", UpdateValue) != true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1104.Delete_GroupFunction() : " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Group Function Menu Up/Down 
        /// </summary>
        /// <param name="_step">1:Menu position Up, 2:Menu position Down</param>
        /// <returns>bool : True or False</returns>
        private bool Move_GroupFunctionMenu(int _step)
        {
            int iMenuSeq, iNewMenuSeq;
            string QueryCond = "";
            string UpdateValue = "";
            string secGroupName, nextMenuName, sysCurrentDate = "";
            DataTable rtDataTable = null;
            try
            {
                secGroupName = cdvSecGrp.Text.Trim();
                if (secGroupName == "")
                {
                    CmnFunction.ShowMsgBox("Secutity Group ID : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                    return false;
                }

                if (((TreeNode)tvMenu.SelectedNodes[0]).Tag.ToString() == "")
                {
                    CmnFunction.ShowMsgBox("Function Menu : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                    return false;
                }

                sysCurrentDate = CmnFunction.GetSysDateTime();
                if (sysCurrentDate == null)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                if (((TreeNode)tvMenu.SelectedNodes[0]).Index == 0 && _step == 1) return true;
                if (((TreeNode)tvMenu.SelectedNodes[0]).Index == (tvMenu.Nodes.Count - 1) && _step == 2) return true;

                rtDataTable = new DataTable();

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, secGroupName);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, ((TreeNode)tvMenu.SelectedNodes[0]).Tag.ToString());

                // 2010-12-03-정비재 : Report Menu의 Group 항목의 Index를 조회한다.
                iMenuSeq = CmnFunction.oComm.SelectDataCount("RWEBGRPFUN", "3", QueryCond);

                //RWEBGRPFUN.FACTORY
                //RWEBGRPFUN.SEC_GRP_ID
                //RWEBGRPFUN.FUNC_GRP_ID
                //RWEBGRPFUN.FUNC_GRP_SEQ
                //RWEBGRPFUN.UPDATE_USER_ID
                //RWEBGRPFUN.UPDATE_TIME
                if (_step == 1)
                { // Menu UP
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, string.Format("{0:D}", iMenuSeq - 1));
                }
                else
                { // Menu Down
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, string.Format("{0:D}", iMenuSeq + 1));
                }
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsUserID);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, sysCurrentDate);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsCentralFactory);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, secGroupName);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, ((TreeNode)tvMenu.SelectedNodes[0]).Tag.ToString());

                // 2010-12-03-정비재 : Report Menu의 Group 항목의 Index를 수정한다.
                if (CmnFunction.oComm.UpdateData("RWEBGRPFUN", "4", UpdateValue) != true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                // 이동한 메뉴 다음(이전)메뉴의 순서를 변경.
                if (_step == 1)
                { // Menu UP
                    nextMenuName = tvMenu.Nodes[((TreeNode)tvMenu.SelectedNodes[0]).Index - 1].Tag.ToString();
                }
                else
                { // Menu Down
                    nextMenuName = tvMenu.Nodes[((TreeNode)tvMenu.SelectedNodes[0]).Index + 1].Tag.ToString();
                }

                rtDataTable.Clear();

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, secGroupName);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, nextMenuName);

                // 2010-12-03-정비재 : Report Menu의 Group 항목의 Index를 조회한다.
                iNewMenuSeq = CmnFunction.oComm.SelectDataCount("RWEBGRPFUN", "3", QueryCond);

                //RWEBGRPFUN.FACTORY
                //RWEBGRPFUN.SEC_GRP_ID
                //RWEBGRPFUN.FUNC_GRP_ID
                //RWEBGRPFUN.FUNC_GRP_SEQ
                //RWEBGRPFUN.UPDATE_USER_ID
                //RWEBGRPFUN.UPDATE_TIME
                UpdateValue = "";
                if (_step == 1)
                { // Menu UP
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, string.Format("{0:D}", iNewMenuSeq + 1));
                }
                else
                { // Menu Down
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, string.Format("{0:D}", iNewMenuSeq - 1));
                }
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsUserID);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, sysCurrentDate);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsCentralFactory);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, secGroupName);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, nextMenuName);

                // 2010-12-03-정비재 : Report Menu의 Group 항목의 Index를 수정한다.
                if (CmnFunction.oComm.UpdateData("RWEBGRPFUN", "4", UpdateValue) != true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1104.Move_GroupFunctionMenu() : " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Function Menu Up/Down 
        /// </summary>
        /// <param name="_step">1:Menu position Up, 2:Menu position Down</param>
        /// <returns>bool : True or False</returns>
        private bool Move_Function(int _step)
        {
            int iFuncSeq, iNewFuncSeq;
            string QueryCond = "";
            string UpdateValue = "";
            string secGroupName, nextFunction, sysCurrentDate = "";
            DataTable rtDataTable = null;
            try
            {
                secGroupName = cdvSecGrp.Text.Trim();
                if (secGroupName == "")
                {
                    CmnFunction.ShowMsgBox("Secutity Group ID : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                    return false;
                }

                if (((TreeNode)tvMenu.SelectedNodes[0]).Tag.ToString() == "")
                {
                    CmnFunction.ShowMsgBox("Function : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                    return false;
                }

                sysCurrentDate = CmnFunction.GetSysDateTime();
                if (sysCurrentDate == null)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                if (((TreeNode)tvMenu.SelectedNodes[0]).Index == 0 && _step == 1) return true;
                if (((TreeNode)tvMenu.SelectedNodes[0]).Index == (((TreeNode)tvMenu.SelectedNodes[0]).Nodes.Count - 1) && _step == 2) return true;

                rtDataTable = new DataTable();

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, secGroupName);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, ((TreeNode)tvMenu.SelectedNodes[0]).Parent.Tag.ToString());
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, ((TreeNode)tvMenu.SelectedNodes[0]).Tag.ToString());

                iFuncSeq = CmnFunction.oComm.SelectDataCount("RWEBGRPFUN", "4", QueryCond);
                //iFuncSeq = CmnFunction.oComm.SelectDataCount("RWEBGRPFUN", "3", QueryCond);

                //RWEBGRPFUN.FACTORY
                //RWEBGRPFUN.SEC_GRP_ID
                //RWEBGRPFUN.FUNC_GRP_ID
                //RWEBGRPFUN.FUNC_GRP_SEQ
                //RWEBGRPFUN.UPDATE_USER_ID
                //RWEBGRPFUN.UPDATE_TIME
                if (_step == 1)
                { // Menu UP
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, string.Format("{0:D}", iFuncSeq - 1));
                }
                else
                { // Menu Down
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, string.Format("{0:D}", iFuncSeq + 1));
                }
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsUserID);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, sysCurrentDate);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsCentralFactory);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, secGroupName);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, ((TreeNode)tvMenu.SelectedNodes[0]).Parent.Tag.ToString());
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, ((TreeNode)tvMenu.SelectedNodes[0]).Tag.ToString());

                // 2010-09-06-임종우 : 메뉴 순서 변경이 안됨.. 기존에 Step이 "7" 이었으나 내부 소스에는 실제 존재 하지 않는 Step임..
                //                     임의 대로 "4"으로 변경 함.
                //if (CmnFunction.oComm.UpdateData("RWEBGRPFUN", "4", UpdateValue) != true)
                // 2010-12-02-정비재 : Report Menu의 순서를 변경하기 위하여 Step를 추가함.
                if (CmnFunction.oComm.UpdateData("RWEBGRPFUN", "6", UpdateValue) != true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                // 이동한 메뉴 다음(이전)메뉴의 순서를 변경.
                if (_step == 1)
                { // Menu UP
                    nextFunction = tvMenu.Nodes[((TreeNode)tvMenu.SelectedNodes[0]).Parent.Tag.ToString()].Nodes[((TreeNode)tvMenu.SelectedNodes[0]).Index - 1].Tag.ToString();
                }
                else
                { // Menu Down
                    nextFunction = tvMenu.Nodes[((TreeNode)tvMenu.SelectedNodes[0]).Parent.Tag.ToString()].Nodes[((TreeNode)tvMenu.SelectedNodes[0]).Index + 1].Tag.ToString();
                }

                rtDataTable.Clear();

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, secGroupName);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, ((TreeNode)tvMenu.SelectedNodes[0]).Parent.Tag.ToString());
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, nextFunction);

                iNewFuncSeq = CmnFunction.oComm.SelectDataCount("RWEBGRPFUN", "4", QueryCond);
                //iNewFuncSeq = CmnFunction.oComm.SelectDataCount("RWEBGRPFUN", "3", QueryCond);

                //RWEBGRPFUN.FACTORY
                //RWEBGRPFUN.SEC_GRP_ID
                //RWEBGRPFUN.FUNC_GRP_ID
                //RWEBGRPFUN.FUNC_GRP_SEQ
                //RWEBGRPFUN.UPDATE_USER_ID
                //RWEBGRPFUN.UPDATE_TIME
                UpdateValue = "";
                if (_step == 1)
                { // Menu UP
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, string.Format("{0:D}", iNewFuncSeq + 1));
                }
                else
                { // Menu Down
                    UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, string.Format("{0:D}", iNewFuncSeq - 1));
                }

                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsUserID);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, sysCurrentDate);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, GlobalVariable.gsCentralFactory);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, secGroupName);
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, ((TreeNode)tvMenu.SelectedNodes[0]).Parent.Tag.ToString());
                UpdateValue = FwxCmnFunction.PackCondition(UpdateValue, nextFunction);

                // 2010-09-06-임종우 : 메뉴 순서 변경이 안됨.. 기존에 Step이 "7" 이었으나 내부 소스에는 실제 존재 하지 않는 Step임..
                //                     임의 대로 "4"으로 변경 함.
                //if (CmnFunction.oComm.UpdateData("RWEBGRPFUN", "4", UpdateValue) != true)
                // 2010-12-02-정비재 : Report Menu의 순서를 변경하기 위하여 Step를 추가함.
                if (CmnFunction.oComm.UpdateData("RWEBGRPFUN", "6", UpdateValue) != true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD000", GlobalVariable.gcLanguage));
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1104.Move_Function() : " + ex.ToString());
                return false;
            }
        }

        #endregion

        private void SEC1104_Load(object sender, EventArgs e)
        {
            tvMenu.ImageList = ((System.Windows.Forms.TabControl)((System.Windows.Forms.TabPage)this.Parent).Parent).ImageList;
            lisAvailFunc.SmallImageList = ((System.Windows.Forms.TabControl)((System.Windows.Forms.TabPage)this.Parent).Parent).ImageList;
            View_Function_List(1, lisAvailFunc);
            grpMenu_Resize(null, null);
        }

        private void grpMenu_Resize(object sender, EventArgs e)
        {
            CmnFunction.FieldAdjust(grpMenu, pnlLeft, pnlRight, pnlAttachFuncMid, 40);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string funcMenu;

            funcMenu = CmnFunction.Trim(txtMenu.Text);

            try
            {
                if (funcMenu != "")
                {
                    if (tvMenu.SelectedNodes.Count > 0)
                    {
                        string selectFuncMenu;
                        TreeNode node;

                        node = (TreeNode)tvMenu.SelectedNodes[0];

                        if (node.Parent == null)
                        {
                            selectFuncMenu = node.Tag.ToString();
                        }
                        else
                        {
                            selectFuncMenu = node.Parent.Tag.ToString();
                        }

                        if (Create_GroupFunctionMenu(funcMenu, selectFuncMenu) == true) CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF001", GlobalVariable.gcLanguage));
                    }
                    else
                    {
                        if (Create_GroupFunctionMenu(funcMenu, null) == true) CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF001", GlobalVariable.gcLanguage));
                    }
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1104.btnCreate_Click() : " + ex.ToString());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string funcMenu;

            funcMenu = CmnFunction.Trim(txtMenu.Text);

            try
            {
                if (funcMenu != "")
                {
                    if (tvMenu.SelectedNodes.Count > 0)
                    {
                        string selectFuncMenu;
                        TreeNode node;

                        node = (TreeNode)tvMenu.SelectedNodes[0];

                        if (node.Parent == null)
                        {
                            selectFuncMenu = node.Tag.ToString();
                        }
                        else
                        {
                            selectFuncMenu = node.Parent.Tag.ToString();
                        }

                        if (funcMenu != selectFuncMenu)
                        {
                            if (selectFuncMenu == "Attach New Menu..." || selectFuncMenu == "Attach New Function...") return;
                            if (CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF003", GlobalVariable.gcLanguage), "Update", MessageBoxButtons.YesNo, 2) != System.Windows.Forms.DialogResult.Yes) return;
                            if (Update_GroupFunctionMenu(CmnFunction.Trim(txtMenu.Text), selectFuncMenu) == true) CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF001", GlobalVariable.gcLanguage));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1104.btnUpdate_Click() : " + ex.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvMenu.SelectedNodes.Count > 0)
                {
                    string selectFuncMenu;
                    TreeNode node;

                    node = (TreeNode)tvMenu.SelectedNodes[0];

                    if (node.Parent == null)
                    {
                        selectFuncMenu = node.Tag.ToString();
                    }
                    else
                    {
                        selectFuncMenu = node.Parent.Tag.ToString();
                    }

                    if (CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF002", GlobalVariable.gcLanguage), "Delete", MessageBoxButtons.YesNo, 2) != System.Windows.Forms.DialogResult.Yes) return;
                    if (Delete_GroupFunctionMenu(selectFuncMenu) == true) CmnFunction.ShowMsgBox(RptMessages.GetMessage("INF001", GlobalVariable.gcLanguage));
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1104.btnDelete_Click() : " + ex.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int i;
            string funcMenu;

            try
            {
                if (cdvSecGrp.Text.Trim() == "")
                {
                    CmnFunction.ShowMsgBox("Secutity Group ID : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                    return;
                }

                if (tvMenu.SelectedNode == null)
                {
                    CmnFunction.ShowMsgBox("Function Menu : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                    return;
                }

                TreeNode node;
                node = tvMenu.SelectedNode;

                if (node.Parent == null)
                {
                    funcMenu = node.Tag.ToString();
                }
                else
                {
                    funcMenu = node.Parent.Tag.ToString();
                }

                for (i = 0; i < lisAvailFunc.Items.Count; i++)
                {
                    if (lisAvailFunc.Items[i].Selected == true)
                    {
                        if (node.Parent == null)
                        {
                            if (Add_GroupFunction(funcMenu, lisAvailFunc.Items[i].Text, null) == true)
                            {
                                TreeNode node_temp;
                                node_temp = node.Nodes.Add(lisAvailFunc.Items[i].Text, lisAvailFunc.Items[i].Text + " : " + LanguageFunction.FindLanguage(lisAvailFunc.Items[i].SubItems[1].Text, 2), (int)SMALLICON_INDEX.IDX_REPORT_FUNCTION, (int)SMALLICON_INDEX.IDX_REPORT_FUNCTION);
                                node_temp.Tag = lisAvailFunc.Items[i].Text;
                                node_temp.Parent.Expand();
                            }
                        }
                        else
                        {
                            if (Add_GroupFunction(funcMenu, lisAvailFunc.Items[i].Text, node.Tag.ToString()) == true)
                            {
                                TreeNode node_temp;
                                node_temp = node.Parent.Nodes.Insert(tvMenu.Nodes[funcMenu].Nodes[node.Tag.ToString()].Index, lisAvailFunc.Items[i].Text, lisAvailFunc.Items[i].Text + " : " + LanguageFunction.FindLanguage(lisAvailFunc.Items[i].SubItems[1].Text, 2), (int)SMALLICON_INDEX.IDX_REPORT_FUNCTION, (int)SMALLICON_INDEX.IDX_REPORT_FUNCTION);
                                node_temp.Tag = lisAvailFunc.Items[i].Text;
                                node_temp.Parent.Expand();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1104.btnAdd_Click() : " + ex.ToString());
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int i;

            try
            {
                if (cdvSecGrp.Text.Trim() == "")
                {
                    CmnFunction.ShowMsgBox("Secutity Group ID : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                    return;
                }

                if (tvMenu.SelectedNode == null)
                {
                    CmnFunction.ShowMsgBox("Function Menu : " + RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                    return;
                }

                if (tvMenu.SelectedNode.Text == "Attach New Menu..." || tvMenu.SelectedNode.Text == "Attach New Function...") return;

                TreeNode node;

                for (i = 0; i < tvMenu.SelectedNodes.Count; i++)
                {
                    node = (TreeNode)tvMenu.SelectedNodes[i];

                    if (node.Parent != null)
                    {
                        if (Delete_GroupFunction(node.Parent.Tag.ToString(), node.Tag.ToString()) == false)
                        {
                            return;
                        }

                        node.Remove();
                    }
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1104.btnDel_Click() : " + ex.ToString());
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (tvMenu.SelectedNode.Text == "Attach New Menu..." || tvMenu.SelectedNode.Text == "Attach New Function...") return;

            if (((TreeNode)tvMenu.SelectedNodes[0]).Parent == null)
            {
                if (((TreeNode)tvMenu.SelectedNodes[0]).Index == 0) return;

                if (Move_GroupFunctionMenu(1) == true)
                {
                    if (tvMenu.SelectedNodes.Count < 1) return;

                    try
                    {
                        TreeNode tn_temp;
                        TreeNode tn_curr;

                        tn_curr = (TreeNode)tvMenu.SelectedNodes[0];
                        tn_temp = (TreeNode)tn_curr.Clone();
                        tvMenu.Nodes.Insert(tn_curr.PrevNode.Index, tn_temp);
                        tvMenu.Nodes.Remove(tn_curr);
                        tn_temp.Expand();
                        tvMenu.SelectedNode = null;
                        tvMenu.SelectedNode = tn_temp;
                    }
                    catch (Exception ex)
                    {
                        CmnFunction.ShowMsgBox("SEC1104.btnUp_Click() : " + ex.ToString());
                    }
                }
            }
            else
            {
                if (((TreeNode)tvMenu.SelectedNodes[0]).Index == 0) return;

                if (Move_Function(1) == true)
                {
                    if (((TreeNode)tvMenu.SelectedNodes[0]).Parent.Nodes.Count < 1) return;

                    try
                    {
                        TreeNode tn_temp;
                        TreeNode tn_curr;

                        tn_curr = (TreeNode)tvMenu.SelectedNodes[0];
                        tn_temp = (TreeNode)tn_curr.Clone();
                        ((TreeNode)tvMenu.SelectedNodes[0]).Parent.Nodes.Insert(tn_curr.PrevNode.Index, tn_temp);
                        ((TreeNode)tvMenu.SelectedNodes[0]).Parent.Nodes.Remove(tn_curr);
                        tn_temp.Expand();
                        tvMenu.SelectedNode = null;
                        tvMenu.SelectedNode = tn_temp;
                    }
                    catch (Exception ex)
                    {
                        CmnFunction.ShowMsgBox("SEC1104.btnUp_Click() : " + ex.ToString());
                    }
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (tvMenu.SelectedNode.Text == "Attach New Menu..." || tvMenu.SelectedNode.Text == "Attach New Function...") return;
            if (tvMenu.SelectedNode.NextNode.Text == "Attach New Menu..." || tvMenu.SelectedNode.NextNode.Text == "Attach New Function...") return;

            if (((TreeNode)tvMenu.SelectedNodes[0]).Parent == null)
            {
                if (((TreeNode)tvMenu.SelectedNodes[0]).Index == tvMenu.Nodes.Count - 1) return;

                if (Move_GroupFunctionMenu(2) == true)
                {
                    if (tvMenu.SelectedNodes.Count < 1) return;

                    try
                    {
                        TreeNode tn_temp;
                        TreeNode tn_curr;

                        tn_curr = (TreeNode)tvMenu.SelectedNodes[0];
                        tn_temp = (TreeNode)tn_curr.Clone();
                        tvMenu.Nodes.Insert(tn_curr.NextNode.Index + 1, tn_temp);
                        tvMenu.Nodes.Remove(tn_curr);
                        tn_temp.Expand();
                        tvMenu.SelectedNode = null;
                        tvMenu.SelectedNode = tn_temp;
                    }
                    catch (Exception ex)
                    {
                        CmnFunction.ShowMsgBox("SEC1104.btnDown_Click() : " + ex.ToString());
                    }
                }
            }
            else
            {
                if (((TreeNode)tvMenu.SelectedNodes[0]).Index == ((TreeNode)tvMenu.SelectedNodes[0]).Parent.Nodes.Count - 1) return;

                if (Move_Function(2) == true)
                {
                    if (((TreeNode)tvMenu.SelectedNodes[0]).Parent.Nodes.Count < 1) return;

                    try
                    {
                        TreeNode tn_temp;
                        TreeNode tn_curr;

                        tn_curr = (TreeNode)tvMenu.SelectedNodes[0];
                        tn_temp = (TreeNode)tn_curr.Clone();
                        ((TreeNode)tvMenu.SelectedNodes[0]).Parent.Nodes.Insert(tn_curr.NextNode.Index + 1, tn_temp);
                        ((TreeNode)tvMenu.SelectedNodes[0]).Parent.Nodes.Remove(tn_curr);
                        tn_temp.Expand();
                        tvMenu.SelectedNode = null;
                        tvMenu.SelectedNode = tn_temp;
                    }
                    catch (Exception ex)
                    {
                        CmnFunction.ShowMsgBox("SEC1104.btnDown_Click() : " + ex.ToString());
                    }
                }
            }
        }

        private void btnExpandAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TreeNode node in tvMenu.Nodes)
                {
                    if (node.GetNodeCount(false) > 0)
                    {
                        node.ExpandAll();
                    }
                }

                if (tvMenu.Nodes.Count > 0)
                {
                    tvMenu.Nodes[0].EnsureVisible();
                }
                
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1104.btnExpandAll_Click() : " + ex.ToString());
            }
        }

        private void btnCollapseAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TreeNode node in tvMenu.Nodes)
                {
                    if (node.GetNodeCount(false) > 0)
                    {
                        node.Collapse(false);
                    }
                }

                if (tvMenu.Nodes.Count > 0)
                {
                    tvMenu.Nodes[0].Expand();
                    tvMenu.Nodes[0].EnsureVisible();
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1104.btnCollapseAll_Click() : " + ex.ToString());
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
                CmnFunction.ShowMsgBox("SEC1104.btnClose_Click() : " + ex.ToString());
            }
        }

        private void cdvSecGrp_ButtonPress(object sender, EventArgs e)
        {
            cdvSecGrp.Init();
            cdvSecGrp.Columns.Add("Group", 100);
            cdvSecGrp.Columns.Add("Group Desc", 100);
            cdvSecGrp.SelectedSubItemIndex = 0;
            CmnListFunction.ViewSecGroupList(cdvSecGrp.GetListView);
        }

        private void cdvSecGrp_SelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            tvMenu.Nodes.Clear();
            txtDesc.Text = cdvSecGrp.SelectedItem.SubItems[1].Text;
            View_Function_List(2, tvMenu);
            tvMenu.ExpandAll();
            if (tvMenu.Nodes.Count > 0) tvMenu.Nodes[0].EnsureVisible();
        }

        private void tvMenu_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (tvMenu.SelectedNode != null)
                {
                    TreeNode node;

                    node = tvMenu.SelectedNode;

                    if (node.Parent == null)
                    {
                        if (tvMenu.SelectedNode.Text == "Attach New Menu..." || tvMenu.SelectedNode.Text == "Attach New Function...") return;
                        txtMenu.Text = node.Tag.ToString();
                    }
                    else
                    {
                        txtMenu.Text = node.Parent.Tag.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1104.tvMenu_NodeMouseClick() : " + ex.ToString());
            }
        }

        private void tvMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (tvMenu.SelectedNode != null)
                {
                    TreeNode node;

                    node = tvMenu.SelectedNode;

                    if (node.Parent == null)
                    {
                        if (tvMenu.SelectedNode.Text == "Attach New Menu..." || tvMenu.SelectedNode.Text == "Attach New Function...") return;
                        txtMenu.Text = node.Tag.ToString();
                    }
                    else
                    {
                        txtMenu.Text = node.Parent.Tag.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("SEC1104.tvMenu_AfterSelect() : " + ex.ToString());
            }
        }
    }
}