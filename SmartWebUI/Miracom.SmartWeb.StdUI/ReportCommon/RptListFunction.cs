using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.UI
{
    public sealed class RptListFunction
    {
        public static DataTable MaxMaterialVer(string Factory, string MatID)
        {
            DataTable dt = new DataTable();
            string QueryCond = null;

            QueryCond = FwxCmnFunction.PackCondition(QueryCond, Factory);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, MatID);
            dt = CmnFunction.oComm.GetFuncDataTable("ViewMaxMaterialVerList", QueryCond);
            return dt;
        }

        public static void ViewMatList(Control objControl, string Factory, string MatID)
        {
            DataTable dt = null;
            ListViewItem itmX = null;
            int iCnt;

            string QueryCond = null;
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, Factory);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, MatID);
            dt = CmnFunction.oComm.GetFuncDataTable("ViewMaterialList", QueryCond);
            if (objControl is ListView)
            {
                for (iCnt = 0; iCnt < dt.Rows.Count; iCnt++)
                {
                    itmX = new ListViewItem(dt.Rows[iCnt]["MAT_ID"].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_MATERIAL);
                    if (((ListView)objControl).Columns.Count > 1)
                    {
                        itmX.SubItems.Add(dt.Rows[iCnt]["MAT_DESC"].ToString().TrimEnd());
                    }
                    ((ListView)objControl).Items.Add(itmX);

                }
            }
        }

        public static void ViewMatVerList(Control objControl, string Factory, string MatID)
        {
            DataTable dt = null;
            ListViewItem itmX = null;
            int iCnt;
            if (Factory.TrimEnd() == "" || MatID.TrimEnd() == "")
            {
                return;
            }
            string QueryCond = null;
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, Factory);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, MatID);
            dt = CmnFunction.oComm.GetFuncDataTable("ViewMaterialVerList", QueryCond);
            if (objControl is ListView)
            {
                for (iCnt = 0; iCnt < dt.Rows.Count; iCnt++)
                {
                    itmX = new ListViewItem(dt.Rows[iCnt]["MAT_VER"].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_MATERIAL);
                    ((ListView)objControl).Items.Add(itmX);
                }
            }
        }

        public static void ViewFlowList(Control objControl, string Factory, string MatID, string MatVer)
        {
            DataTable dt = null;
            ListViewItem itmX = null;
            int iCnt;
            if (Factory.TrimEnd() == "")
            {
                return;
            }
            string QueryCond = null;
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, Factory);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, MatID);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, MatVer);
            dt = CmnFunction.oComm.GetFuncDataTable("ViewFlowList", QueryCond);
            if (objControl is ListView)
            {
                for (iCnt = 0; iCnt < dt.Rows.Count; iCnt++)
                {
                    if (MatID.TrimEnd() == "")
                    {
                        itmX = new ListViewItem(dt.Rows[iCnt]["FLOW"].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_MATERIAL);
                        if (((ListView)objControl).Columns.Count > 1)
                        {
                            itmX.SubItems.Add(dt.Rows[iCnt]["FLOW_DESC"].ToString().TrimEnd());
                        }
                    }
                    else
                    {
                        itmX = new ListViewItem(dt.Rows[iCnt]["FLOW"].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_MATERIAL);
                        if (((ListView)objControl).Columns.Count > 1)
                        {
                            itmX.SubItems.Add(dt.Rows[iCnt]["FLOW_SEQ_NUM"].ToString().TrimEnd());
                        }
                    }
                    
                    ((ListView)objControl).Items.Add(itmX);
                }
            }
        }

        public static void ViewOperList(Control objControl, string Factory, string Flow)
        {
            DataTable dt = null;
            ListViewItem itmX = null;
            int iCnt;
            if (Factory.TrimEnd() == "")
            {
                return;
            }
            string QueryCond = null;
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, Factory);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, Flow);
            dt = CmnFunction.oComm.GetFuncDataTable("ViewOperList", QueryCond);
            if (objControl is ListView)
            {
                for (iCnt = 0; iCnt < dt.Rows.Count; iCnt++)
                {
                    if (Flow.TrimEnd() == "")
                    {
                        itmX = new ListViewItem(dt.Rows[iCnt]["OPER"].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_MATERIAL);
                        if (((ListView)objControl).Columns.Count > 1)
                        {
                            itmX.SubItems.Add(dt.Rows[iCnt]["OPER_DESC"].ToString().TrimEnd());
                        }
                    }
                    else
                    {
                        itmX = new ListViewItem(dt.Rows[iCnt]["OPER"].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_MATERIAL);
                    }

                    ((ListView)objControl).Items.Add(itmX);
                }
            }
        }

        public static void ViewFactoryList(Control objControl)
        {
            DataTable dt = null;
            ListViewItem itmX = null;
            int iCnt;

            string QueryCond = null;
            
            dt = CmnFunction.oComm.GetFuncDataTable("ViewFactoryList", QueryCond);
            if (objControl is ListView)
            {
                for (iCnt = 0; iCnt < dt.Rows.Count; iCnt++)
                {
                    itmX = new ListViewItem(dt.Rows[iCnt]["FACTORY"].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_FACTORY);
                    if (((ListView)objControl).Columns.Count > 1)
                    {
                        itmX.SubItems.Add(dt.Rows[iCnt]["FAC_DESC"].ToString().TrimEnd());
                    }
                    ((ListView)objControl).Items.Add(itmX);

                }
            }
        }

        public static void ViewResourceList(Control objControl, string Factory)
        {
            DataTable dt = null;
            ListViewItem itmX = null;
            int iCnt;
            if (Factory.TrimEnd() == "")
            {
                return;
            }
            string QueryCond = null;
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, Factory);
            
            dt = CmnFunction.oComm.GetFuncDataTable("ViewResourceList", QueryCond);
            if (objControl is ListView)
            {
                for (iCnt = 0; iCnt < dt.Rows.Count; iCnt++)
                {
                    itmX = new ListViewItem(dt.Rows[iCnt]["RES_ID"].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_MATERIAL);
                    if (((ListView)objControl).Columns.Count > 1)
                    {
                        itmX.SubItems.Add(dt.Rows[iCnt]["RES_DESC"].ToString().TrimEnd());
                    }
                    ((ListView)objControl).Items.Add(itmX);

                }
            }
        }

        public static void ViewGCMTableList(Control objControl, string Factory, string Table_Name)
        {
            DataTable dt = null;
            ListViewItem itmX = null;
            int iCnt;
            if (Factory.TrimEnd() == "")
            {
                return;
            }
            string QueryCond = null;
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, Factory);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, Table_Name);
            dt = CmnFunction.oComm.GetFuncDataTable("ViewGCMTableList", QueryCond);
            if (objControl is ListView)
            {
                for (iCnt = 0; iCnt < dt.Rows.Count; iCnt++)
                {
                    itmX = new ListViewItem(dt.Rows[iCnt]["KEY_1"].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_MATERIAL);
                    if (((ListView)objControl).Columns.Count > 1)
                    {
                        itmX.SubItems.Add(dt.Rows[iCnt]["DATA_1"].ToString().TrimEnd());
                    }
                    ((ListView)objControl).Items.Add(itmX);
                }
            }
        }

        public static void ViewMATGroupList(Control objControl, string Factory, string Item_Name)
        {
            DataTable dt = null;
            ListViewItem itmX = null;
            int iCnt;
            if (Factory.TrimEnd() == "")
            {
                return;
            }
            string QueryCond = null;
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, Factory);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, Item_Name);
            dt = CmnFunction.oComm.GetFuncDataTable("ViewMATGroupList", QueryCond);

            if (objControl is ListView)
            {
                for (iCnt = 0; iCnt < dt.Rows.Count; iCnt++)
                {
                    itmX = new ListViewItem(dt.Rows[iCnt]["Prompt"].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_MATERIAL);
                    if (((ListView)objControl).Columns.Count > 1)
                    {
                        itmX.SubItems.Add(dt.Rows[iCnt]["Group"].ToString().TrimEnd());
                    }
                    ((ListView)objControl).Items.Add(itmX);
                }
            }
        }

        public static void ViewMATTypeList(Control objControl, string Factory, string Table_Name)
        {
            //DataTable dt = null;
            //ListViewItem itmX = null;
            //int iCnt;
            //if (Factory.TrimEnd() == "")
            //{
            //    return;
            //}
            //string QueryCond = null;
            //QueryCond = FwxCmnFunction.PackCondition(QueryCond, Factory);
            //QueryCond = FwxCmnFunction.PackCondition(QueryCond, Table_Name);
            //dt = CmnFunction.oComm.GetFuncDataTable("ViewMATTypeList", QueryCond);

            //if (objControl is ListView)
            //{
            //    for (iCnt = 0; iCnt < dt.Rows.Count; iCnt++)
            //    {
            //        itmX = new ListViewItem(dt.Rows[iCnt]["KEY_1"].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_MATERIAL);
            //        if (((ListView)objControl).Columns.Count > 1)
            //        {
            //            itmX.SubItems.Add(dt.Rows[iCnt]["DATA_1"].ToString().TrimEnd());
            //        }
            //        ((ListView)objControl).Items.Add(itmX);
            //    }
            //}
        }
//========================================================================================================//

        public static string DefaultGcmTableName(string Item_Name, int seq)
        {
           string DefaultTblName="";

            switch (Item_Name)
            {
                case "GRP_MATERIAL":
                    DefaultTblName = GlobalConstant.MP_GCM_MATERIAL_GRP[seq];
                   break;
                case "GRP_FLOW":
                    break;
                case "GRP_OPER":
                    break;
            }
            return DefaultTblName;
        }
        
        public static void ViewFacCMFData(Control objControl, string Factory, string Item_Name)
        {
            DataTable dt = null;
            DataTable Redt = new DataTable();
            ListViewItem itmX = null;
            int iCnt;
            if (Factory.TrimEnd() == "")
            {
                return;
            }
            string QueryCond = null;
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, Factory);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, Item_Name);
            dt = CmnFunction.oComm.GetFuncDataTable("VIEW_FACCMF_LIST", QueryCond);
            ((ListView)objControl).Items.Clear();

            if (dt.Rows.Count > 0)
            {
                if (objControl is ListView)
                {
                    for (iCnt = 0; iCnt < 20; iCnt++)
                    {
                        string PrtData = dt.Rows[0].ItemArray[2 + iCnt].ToString();
                        string TblData = dt.Rows[0].ItemArray[62 + iCnt].ToString();
                        itmX = new ListViewItem(PrtData);
                        if (PrtData != " ")
                        {
                            if (TblData == " ")
                            {
                                TblData = DefaultGcmTableName(Item_Name, iCnt);
                                itmX.SubItems.Add(TblData);
                            }
                            else
                            {
                                itmX.SubItems.Add(TblData);
                            }
                            itmX.Tag = dt;
                            ((ListView)objControl).Items.Add(itmX);
                        }
                    }
                }
            }
        }


        public static void ViewGCMGrpList(Control objControl, string Factory, string SelPrt, string SelTbl)
        {
            DataTable dt = null;
            ListViewItem itmX = null;
            int iCnt;
            string QueryCond = null;

            QueryCond = FwxCmnFunction.PackCondition(QueryCond, Factory);
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, SelTbl);
            dt = CmnFunction.oComm.GetFuncDataTable("VIEW_GCMGRP_LIST", QueryCond);

            ((ListView)objControl).Items.Clear();
            if (objControl is ListView)
            {
                for (iCnt = 0; iCnt < dt.Rows.Count; iCnt++)
                {
                    itmX = new ListViewItem(dt.Rows[iCnt]["GROUP"].ToString().TrimEnd());
                    itmX.SubItems.Add(dt.Rows[iCnt]["DESC"].ToString().TrimEnd());
                    itmX.Tag = dt;
                    ((ListView)objControl).Items.Add(itmX);
                }
            }
        }

        public static void ViewCmfGcmGrpList(Control objControl, string Factory, string Item_Name, string Default_Table_Name)
        {
            DataTable dt_Fac = null;
            DataTable dt_Gcm = null;
            string QueryCondFac = null;
            string QueryCondGcm = null;
            string Tbl_Nm = "";
            ListViewItem itmX = null;
            int iCnt;

            QueryCondFac = FwxCmnFunction.PackCondition(QueryCondFac, Factory);
            QueryCondFac = FwxCmnFunction.PackCondition(QueryCondFac, Item_Name);
            dt_Fac = CmnFunction.oComm.GetFuncDataTable("VIEW_FACCMF_LIST", QueryCondFac);
            if (dt_Fac.Rows.Count > 0)
            {
                Tbl_Nm = dt_Fac.Rows[0].ItemArray[62].ToString();

                if (Tbl_Nm == " ")
                {
                    Tbl_Nm = Default_Table_Name;
                }
            }
            else
            {
                Tbl_Nm = Default_Table_Name;
            }

            QueryCondGcm = FwxCmnFunction.PackCondition(QueryCondGcm, Factory);
            QueryCondGcm = FwxCmnFunction.PackCondition(QueryCondGcm, Tbl_Nm);
            dt_Gcm = CmnFunction.oComm.GetFuncDataTable("VIEW_GCMGRP_LIST", QueryCondGcm);

            ((ListView)objControl).Items.Clear();
            if (objControl is ListView)
            {
                for (iCnt = 0; iCnt < dt_Gcm.Rows.Count; iCnt++)
                {
                    itmX = new ListViewItem(dt_Gcm.Rows[iCnt]["GROUP"].ToString().TrimEnd());
                    itmX.SubItems.Add(dt_Gcm.Rows[iCnt]["DESC"].ToString().TrimEnd());
                    itmX.Tag = dt_Gcm;
                    ((ListView)objControl).Items.Add(itmX);
                }
            }
        }

        public static void ViewFunctionList(Control objControl, string sStep, string QueryCond)
        {
            DataTable dt = null;
            ListViewItem itmX = null;
            int iCnt;
            dt = CmnFunction.oComm.FillData("RWEBFUNDEF", sStep, QueryCond);
            if (objControl is ListView)
            {
                for (iCnt = 0; iCnt < dt.Rows.Count; iCnt++)
                {
                    itmX = new ListViewItem(dt.Rows[iCnt][0].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_MATERIAL);
                    if (((ListView)objControl).Columns.Count > 1)
                    {
                        itmX.SubItems.Add(dt.Rows[iCnt][1].ToString().TrimEnd());
                    }
                    ((ListView)objControl).Items.Add(itmX);
                }
            }
        }

        public static void ViewOperationList(Control objControl, string Factory)
        {
            DataTable dt = null;
            ListViewItem itmX = null;
            int iCnt;
            string QueryCond = null;
            QueryCond = FwxCmnFunction.PackCondition(QueryCond, Factory);
            dt = CmnFunction.oComm.GetFuncDataTable("VIEW_OPER_LIST", QueryCond);
            if (dt != null)
            {
                if (objControl is ListView)
                {
                    for (iCnt = 0; iCnt < dt.Rows.Count; iCnt++)
                    {
                        itmX = new ListViewItem(dt.Rows[iCnt][0].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_MATERIAL);
                        if (((ListView)objControl).Columns.Count > 1)
                        {
                            itmX.SubItems.Add(dt.Rows[iCnt][1].ToString().TrimEnd());
                        }

                        ((ListView)objControl).Items.Add(itmX);
                    }
                }
            }
        }

        public static void ViewFuncItemList(Control objControl, string sStep, string QueryCond)
        {
            DataTable dt = null;
            ListViewItem itmX = null;
            int iCnt;
            dt = CmnFunction.oComm.FillData("RWEBFUNITM", sStep, QueryCond);
            if (dt != null)
            {
                if (objControl is ListView)
                {
                    for (iCnt = 0; iCnt < dt.Rows.Count; iCnt++)
                    {
                        itmX = new ListViewItem(dt.Rows[iCnt][0].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_MATERIAL);
                        if (((ListView)objControl).Columns.Count > 1)
                        {
                            itmX.SubItems.Add(dt.Rows[iCnt][1].ToString().TrimEnd());
                        }
                        ((ListView)objControl).Items.Add(itmX);
                    }
                }
            }
            else return;
        }
        
    }
}
