using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.UI
{
    public sealed class CmnListFunction
    {
        public CmnListFunction()
        {

        }
        /// <summary>
        /// View Security User List.
        /// </summary>
        /// <param name="ctlList">View Control Object</param>
        /// <returns>bool : True or False</returns>
        public static bool ViewSecUserList(System.Windows.Forms.Control ctlList)
        {
            ListViewItem itmX = null;
            TreeNode nodeX = null;
            string secUserId, secUserDesc;

            try
            {
                DataTable rtDataTable = new DataTable();

                string QueryCond = null;

                if (ctlList is ListView)
                {
                    CmnInitFunction.InitListView((ListView)ctlList);
                }

                CmnFunction.oComm.SetUrl();

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);

                rtDataTable = CmnFunction.oComm.FillData("RWEBUSRDEF", "2", QueryCond);

                if (rtDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        secUserId = rtDataTable.Rows[i]["USER_ID"].ToString().TrimEnd();
                        secUserDesc = rtDataTable.Rows[i]["USER_DESC"].ToString().TrimEnd();

                        if (ctlList is ListView)
                        {
                            itmX = new ListViewItem(secUserId, (int)SMALLICON_INDEX.IDX_USER);
                            if (((ListView)ctlList).Columns.Count > 1)
                            {
                                itmX.SubItems.Add(secUserDesc);
                            }
                            ((ListView)ctlList).Items.Add(itmX);
                        }
                        else if (ctlList is TreeView)
                        {
                            nodeX = new TreeNode(secUserId + " : " + secUserDesc, (int)SMALLICON_INDEX.IDX_USER, (int)SMALLICON_INDEX.IDX_USER);
                            ((TreeView)ctlList).Nodes.Add(nodeX);
                        }
                        else if (ctlList is ComboBox)
                        {
                            ((ComboBox)ctlList).Items.Add(secUserId);
                        }
                    }
                }
                else
                {
                    //CmnFunction.ShowMsgBox("RPT-0002");
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
        /// View Security User List.
        /// </summary>
        /// <param name="ctlList">View Control Object</param>
        /// <returns>bool : True or False</returns>
        public static bool ViewSecGroupList(System.Windows.Forms.Control ctlList)
        {
            ListViewItem itmX = null;
            TreeNode nodeX = null;
            string secGroupId, secGroupDesc;

            try
            {
                DataTable rtDataTable = new DataTable();

                string QueryCond = null;

                if (ctlList is ListView)
                {
                    CmnInitFunction.InitListView((ListView)ctlList);
                }

                CmnFunction.oComm.SetUrl(); 

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsCentralFactory);

                rtDataTable = CmnFunction.oComm.FillData("RWEBGRPDEF", "1", QueryCond);

                if (rtDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        secGroupId = rtDataTable.Rows[i]["SEC_GRP_ID"].ToString().TrimEnd();
                        secGroupDesc = rtDataTable.Rows[i]["SEC_GRP_DESC"].ToString().TrimEnd();

                        if (ctlList is ListView)
                        {
                            itmX = new ListViewItem(secGroupId, (int)SMALLICON_INDEX.IDX_SEC_GROUP);
                            if (((ListView)ctlList).Columns.Count > 1)
                            {
                                itmX.SubItems.Add(secGroupDesc);
                            }
                            ((ListView)ctlList).Items.Add(itmX);
                        }
                        else if (ctlList is TreeView)
                        {
                            nodeX = new TreeNode(secGroupId + " : " + secGroupDesc, (int)SMALLICON_INDEX.IDX_SEC_GROUP, (int)SMALLICON_INDEX.IDX_SEC_GROUP);
                            ((TreeView)ctlList).Nodes.Add(nodeX);
                        }
                        else if (ctlList is ComboBox)
                        {
                            ((ComboBox)ctlList).Items.Add(secGroupId);
                        }
                    }
                }
                else
                {
                    //CmnFunction.ShowMsgBox("RPT-0002");
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

        public static bool ViewDataList(System.Windows.Forms.Control ctlList, char cStep, string sFactory, string sTable)
        {
            DataTable rtDataTable = new DataTable();
            ListViewItem itmX = null;
            string QueryCond = null;

            try
            {
                if (ctlList is ListView)
                {
                    CmnInitFunction.InitListView((ListView)ctlList);
                }

                CmnFunction.oComm.SetUrl();

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, sFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, sTable);

                rtDataTable = CmnFunction.oComm.FillData("MGCMTBLDAT", cStep.ToString(), QueryCond);

                if (rtDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        if (ctlList is ListView)
                        {
                            itmX = new ListViewItem(rtDataTable.Rows[i]["KEY_1"].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_SEC_GROUP);
                            if (((ListView)ctlList).Columns.Count > 1)
                            {
                                itmX.SubItems.Add(rtDataTable.Rows[i]["DATA_1"].ToString().TrimEnd());
                            }
                            if (((ListView)ctlList).Columns.Count > 2)
                            {
                                itmX.SubItems.Add(rtDataTable.Rows[i]["DATA_2"].ToString().TrimEnd());
                            }
                            if (((ListView)ctlList).Columns.Count > 3)
                            {
                                itmX.SubItems.Add(rtDataTable.Rows[i]["DATA_3"].ToString().TrimEnd());
                            }
                            ((ListView)ctlList).Items.Add(itmX);
                        }
                        else if (ctlList is TreeView)
                        {
                            
                        }
                        else if (ctlList is ComboBox)
                        {
                            
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
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }
        }

        public static bool ViewOperList(System.Windows.Forms.Control ctlList, char cStep, string sFactory)
        {
            DataTable rtDataTable = new DataTable();
            ListViewItem itmX = null;
            string QueryCond = null;

            try
            {
                if (ctlList is ListView)
                {
                    CmnInitFunction.InitListView((ListView)ctlList);
                }

                CmnFunction.oComm.SetUrl();

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, sFactory);

                rtDataTable = CmnFunction.oComm.FillData("MWIPOPRDEF", cStep.ToString(), QueryCond);

                if (rtDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        if (ctlList is ListView)
                        {
                            itmX = new ListViewItem(rtDataTable.Rows[i]["OPER"].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_SEC_GROUP);
                            if (((ListView)ctlList).Columns.Count > 1)
                            {
                                itmX.SubItems.Add(rtDataTable.Rows[i]["OPER_DESC"].ToString().TrimEnd());
                            }
                            ((ListView)ctlList).Items.Add(itmX);
                        }
                        else if (ctlList is TreeView)
                        {

                        }
                        else if (ctlList is ComboBox)
                        {

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
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }
        }

        public static bool ViewFlowList(System.Windows.Forms.Control ctlList, char cStep, string sFactory)
        {
            DataTable rtDataTable = new DataTable();
            ListViewItem itmX = null;
            string QueryCond = null;

            try
            {
                if (ctlList is ListView)
                {
                    CmnInitFunction.InitListView((ListView)ctlList);
                }

                CmnFunction.oComm.SetUrl();

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, sFactory);

                rtDataTable = CmnFunction.oComm.FillData("MWIPFLWDEF", cStep.ToString(), QueryCond);

                if (rtDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        if (ctlList is ListView)
                        {
                            itmX = new ListViewItem(rtDataTable.Rows[i]["FLOW"].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_SEC_GROUP);
                            if (((ListView)ctlList).Columns.Count > 1)
                            {
                                itmX.SubItems.Add(rtDataTable.Rows[i]["FLOW_DESC"].ToString().TrimEnd());
                            }
                            ((ListView)ctlList).Items.Add(itmX);
                        }
                        else if (ctlList is TreeView)
                        {

                        }
                        else if (ctlList is ComboBox)
                        {

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
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }
        }

        public static bool ViewFactoryList(System.Windows.Forms.Control ctlList, char cStep, string sType)
        {
            DataTable rtDataTable = new DataTable();
            ListViewItem itmX = null;
            string QueryCond = null;

            try
            {
                if (ctlList is ListView)
                {
                    CmnInitFunction.InitListView((ListView)ctlList);
                }

                CmnFunction.oComm.SetUrl();

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, sType);

                rtDataTable = CmnFunction.oComm.FillData("MWIPFACDEF", cStep.ToString(), QueryCond);

                if (rtDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        if (ctlList is ListView)
                        {
                            itmX = new ListViewItem(rtDataTable.Rows[i]["FACTORY"].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_SEC_GROUP);
                            if (((ListView)ctlList).Columns.Count > 1)
                            {
                                itmX.SubItems.Add(rtDataTable.Rows[i]["FAC_DESC"].ToString().TrimEnd());
                            }
                            ((ListView)ctlList).Items.Add(itmX);
                        }
                        else if (ctlList is TreeView)
                        {

                        }
                        else if (ctlList is ComboBox)
                        {

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
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }
        }

        public static bool View_Inquiry_List(System.Windows.Forms.Control ctlList, char cStep, string sFactory)
        {
            return View_Inquiry_List(ctlList, cStep, sFactory, "");
        }
        public static bool View_Inquiry_List(System.Windows.Forms.Control ctlList, char cStep, string sFactory, string sGroup)
        {

            DataTable rtDataTable = new DataTable();
            ListViewItem itmX = null;
            string QueryCond = null;

            try
            {
                if (ctlList is ListView)
                {
                    CmnInitFunction.InitListView((ListView)ctlList);
                }


                QueryCond = FwxCmnFunction.PackCondition(QueryCond, sFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, sGroup);

                rtDataTable = CmnFunction.oComm.FillData("RWEBFLXINQ", cStep.ToString(), QueryCond);

                if (rtDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        if (ctlList is ListView)
                        {
                            itmX = new ListViewItem(rtDataTable.Rows[i]["INQUIRY_NAME"].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_FUNCTION);
                            itmX.SubItems.Add(rtDataTable.Rows[i]["INQUIRY_DESC"].ToString().TrimEnd());

                            ((ListView)ctlList).Items.Add(itmX);
                        }
                        else if (ctlList is TreeView)
                        {

                        }
                        else if (ctlList is ComboBox)
                        {

                        }
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
    }
}
