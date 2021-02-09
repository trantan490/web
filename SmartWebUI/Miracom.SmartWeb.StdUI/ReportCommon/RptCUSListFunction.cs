using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.UI
{
    public sealed class RptCUSListFunction
    {
        public static bool GetFactoryList(Control ctlList)
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

                rtDataTable = CmnFunction.oComm.FillData("MWIPFACDEF", "1", QueryCond);

                if (rtDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        if (ctlList is ListView)
                        {
                            itmX = new ListViewItem(rtDataTable.Rows[i]["FACTORY"].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_FACTORY);
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

        public static bool GetMatList(Control ctlList, string sFactory)
        {
            DataTable rtDataTable = new DataTable();
            ListViewItem itmX = null;
            string DinamicQuery = " ";

            try
            {
                if (ctlList is ListView)
                {
                    CmnInitFunction.InitListView((ListView)ctlList);
                }

                CmnFunction.oComm.SetUrl();

                if (sFactory != "")
                {
                    DinamicQuery += " WHERE FACTORY = '" + sFactory + "'";

                }
                DinamicQuery += " ORDER BY MAT_ID";

                rtDataTable = CmnFunction.oComm.FillData("MWIPMATDEF", "1", DinamicQuery);

                if (rtDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        if (ctlList is ListView)
                        {
                            itmX = new ListViewItem(rtDataTable.Rows[i][0].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_MATERIAL);
                            if (((ListView)ctlList).Columns.Count > 1)
                            {
                                itmX.SubItems.Add(rtDataTable.Rows[i][1].ToString().TrimEnd());
                            }
                            ((ListView)ctlList).Items.Add(itmX);
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

        public static bool GetFlwList(Control ctlList, string sFactory)
        {
            DataTable rtDataTable = new DataTable();
            ListViewItem itmX = null;
            string DinamicQuery = " ";

            try
            {
                if (ctlList is ListView)
                {
                    CmnInitFunction.InitListView((ListView)ctlList);
                }

                CmnFunction.oComm.SetUrl();

                if (sFactory != "")
                {
                    DinamicQuery += " WHERE FACTORY = '" + sFactory + "'";
                }

                rtDataTable = CmnFunction.oComm.FillData("MWIPFLWDEF", "4", DinamicQuery);

                if (rtDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        if (ctlList is ListView)
                        {
                            itmX = new ListViewItem(rtDataTable.Rows[i][0].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_FLOW);
                            if (((ListView)ctlList).Columns.Count > 1)
                            {
                                itmX.SubItems.Add(rtDataTable.Rows[i][1].ToString().TrimEnd());
                            }
                            ((ListView)ctlList).Items.Add(itmX);
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

        public static bool GetOprList(Control ctlList, string sFactory)
        {
            return GetOprList(ctlList, null, sFactory);
        }

        public static bool GetOprList(Control ctl1, Control ctl2, string sFactory)
        {
            DataTable rtDataTable = new DataTable();
            ListViewItem itmX = null;
            string DinamicQuery = " ";

            try
            {
                if (ctl1 is ListView)
                {
                    CmnInitFunction.InitListView((ListView)ctl1);
                }

                CmnFunction.oComm.SetUrl();

                if (sFactory != "")
                {
                    DinamicQuery += " WHERE FACTORY = '" + sFactory + "' ";
                }
                DinamicQuery += " AND OPER NOT IN ('00001', '00002')";
                //2010-01-29-임종우 : 원자재 공정 제외
                DinamicQuery += " AND OPER NOT LIKE 'V%'";
                DinamicQuery += " ORDER BY DECODE(OPER_CMF_2, ' ', 99999, TO_NUMBER(OPER_CMF_2)), OPER";
                rtDataTable = CmnFunction.oComm.FillData("MWIPOPRDEF", "3", DinamicQuery);

                if (rtDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        if (ctl1 is ListView)
                        {
                            itmX = new ListViewItem(rtDataTable.Rows[i][0].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_FLOW);
                            if (((ListView)ctl1).Columns.Count > 1)
                            {
                                itmX.SubItems.Add(rtDataTable.Rows[i][1].ToString().TrimEnd());
                            }
                            ((ListView)ctl1).Items.Add(itmX);
                        }
                        else if (ctl1 is TextBox)
                        {
                            if (i == 0) ctl1.Text = rtDataTable.Rows[i][0].ToString().TrimEnd();
                            if (i == rtDataTable.Rows.Count - 1) ctl2.Text = rtDataTable.Rows[i][0].ToString().TrimEnd();
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

        public static bool GetTableList(Control ctlList, string sFactory, string sTable_Name)
        {
            DataTable rtDataTable = new DataTable();
            ListViewItem itmX = null;

            try
            {
                if (ctlList is ListView)
                {
                    CmnInitFunction.InitListView((ListView)ctlList);
                }

                CmnFunction.oComm.SetUrl();

                string QueryCond = null;

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, sFactory);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, sTable_Name);

                rtDataTable = CmnFunction.oComm.GetFuncDataTable("ViewGCMTableList", QueryCond);

                if (rtDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        if (ctlList is ListView)
                        {
                            itmX = new ListViewItem(rtDataTable.Rows[i][2].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_CODE_DATA);
                            if (((ListView)ctlList).Columns.Count > 1)
                            {
                                itmX.SubItems.Add(rtDataTable.Rows[i][12].ToString().TrimEnd());
                            }
                            ((ListView)ctlList).Items.Add(itmX);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }
        }

        public static bool GetTableList(Control ctlList, string sDynamicQuery, string sCodeColumnName, string sValueColumnName)
        {
            DataTable rtDataTable = new DataTable();
            ListViewItem itmX = null;

            try
            {
                if (ctlList is ListView)
                {
                    CmnInitFunction.InitListView((ListView)ctlList);
                }

                CmnFunction.oComm.SetUrl();

                rtDataTable = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", sDynamicQuery);

                if (rtDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        if (ctlList is ListView)
                        {
                            itmX = new ListViewItem(rtDataTable.Rows[i][sCodeColumnName].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_CODE_DATA);
                            if (((ListView)ctlList).Columns.Count > 1)
                            {
                                itmX.SubItems.Add(rtDataTable.Rows[i][sValueColumnName].ToString().TrimEnd());
                            }
                            ((ListView)ctlList).Items.Add(itmX);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }
        }
    }
}
