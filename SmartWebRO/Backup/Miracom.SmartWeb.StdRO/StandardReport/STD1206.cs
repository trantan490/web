﻿using System;
using System.Data;

using Miracom.Query;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.RO;
using System.Collections;

namespace Miracom.SmartWeb.RO
{
    public partial class StandardFunction 
    {
        public DataTable STD1206(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable ReRetDT = new DataTable();
            string DynamicQuery = null;
            string Mat_Grp_Name = null;
            int i;
            int j;
            int k;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_PRODUCTION_OUTPUT_XML_FILE);
                //
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0 && CondList[2] != null && CondList[2].TrimEnd().Length > 0 && CondList[3] != null && CondList[3].TrimEnd().Length > 0)
                {
                    if (CondList[3] == "1") { Mat_Grp_Name = "MAT_GRP_1"; }
                    else if (CondList[3] == "2") { Mat_Grp_Name = "MAT_GRP_2"; }
                    else if (CondList[3] == "3") { Mat_Grp_Name = "MAT_GRP_3"; }
                    else if (CondList[3] == "4") { Mat_Grp_Name = "MAT_GRP_4"; }
                    else if (CondList[3] == "5") { Mat_Grp_Name = "MAT_GRP_5"; }
                    else if (CondList[3] == "6") { Mat_Grp_Name = "MAT_GRP_6"; }
                    else if (CondList[3] == "7") { Mat_Grp_Name = "MAT_GRP_7"; }
                    else if (CondList[3] == "8") { Mat_Grp_Name = "MAT_GRP_8"; }
                    else if (CondList[3] == "9") { Mat_Grp_Name = "MAT_GRP_9"; }
                    else if (CondList[3] == "10") { Mat_Grp_Name = "MAT_GRP_10"; }
                    //else { Mat_Grp_Name = "MAT_GRP_1"; }

                    DynamicQuery = "SELECT WM." + Mat_Grp_Name + " AS GROUP_NAME, TD.DATA_1 AS GROUP_DESC, WM.MAT_ID, WM.MAT_VER, WM.MAT_DESC, WM.LOT, WM.QTY_1, WM.QTY_2";
                    DynamicQuery += " FROM (SELECT MD.FACTORY, MD." + Mat_Grp_Name + " , MD.MAT_ID, MD.MAT_VER, MD.MAT_DESC, SUM(WM.S1_MOVE_LOT+WM.S2_MOVE_LOT+WM.S3_MOVE_LOT+WM.S4_MOVE_LOT) AS LOT,";
                    DynamicQuery += " SUM(WM.S1_MOVE_QTY_1+WM.S2_MOVE_QTY_1+WM.S3_MOVE_QTY_1+WM.S4_MOVE_QTY_1) AS QTY_1, ";
                    DynamicQuery += " SUM(WM.S1_MOVE_QTY_2+WM.S2_MOVE_QTY_2+WM.S3_MOVE_QTY_2+WM.S4_MOVE_QTY_2) AS QTY_2";
                    DynamicQuery += " FROM MWIPMATDEF MD, RSUMWIPMOV WM WHERE MD.FACTORY=WM.FACTORY AND MD.MAT_ID = WM.MAT_ID AND MD.MAT_VER = WM.MAT_VER";
                    DynamicQuery += " AND MD.FACTORY='" + CondList[0] + "'";
                    DynamicQuery += " AND MD." + Mat_Grp_Name + "<> ' '";

                    if (CondList[1].TrimEnd() != "") { DynamicQuery += " AND WM.LOT_TYPE= '" + CondList[1] + "'"; }
                    if (CondList[2].TrimEnd() != "") { DynamicQuery += " AND MD.MAT_TYPE= '" + CondList[2] + "'"; }

                    DynamicQuery += " GROUP BY MD.FACTORY, MD." + Mat_Grp_Name + ", MD.MAT_ID, MD.MAT_VER, MD.MAT_DESC) WM LEFT OUTER JOIN MGCMTBLDAT TD";
                    DynamicQuery += " ON TD.FACTORY=WM.FACTORY AND TD.TABLE_NAME = 'MATERIAL_GRP_" + CondList[3] + "' AND TD.KEY_1 = WM." + Mat_Grp_Name + " ORDER BY WM." + Mat_Grp_Name;

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("DYNAMIC", new string[] { DynamicQuery }, CondList);

                    //Total 및 SubTatal 구하기 위해 New DataTable Column 정의
                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 8; i++)
                        {
                            DataColumn MatGrpCol = new DataColumn();

                            MatGrpCol.ReadOnly = false;
                            MatGrpCol.Unique = false;

                            if (i == 0)
                            {
                                MatGrpCol.ColumnName = "Group";
                                MatGrpCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                MatGrpCol.ColumnName = "Group Desc";
                                MatGrpCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                MatGrpCol.ColumnName = "Material";
                                MatGrpCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                MatGrpCol.ColumnName = "Material Ver";
                                MatGrpCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                MatGrpCol.ColumnName = "Material Desc";
                                MatGrpCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 5)
                            {
                                MatGrpCol.ColumnName = "Total Lot";
                                MatGrpCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 6)
                            {
                                MatGrpCol.ColumnName = "Total Qty1";
                                MatGrpCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 7)
                            {
                                MatGrpCol.ColumnName = "Total Qty2";
                                MatGrpCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(MatGrpCol);
                        }
                    }

                    //Sub Total 계산
                    int iIndex;
                    int subTotal;
                    string CurrentGroup = "";
                    string NewGroup;

                    iIndex = 0;
                    for (i = 0; i < RetDT.Rows.Count; i++)
                    {
                        NewGroup = Convert.ToString(RetDT.Rows[i].ItemArray[0]);
                        if (i == 0 || CurrentGroup != NewGroup)
                        {
                            CurrentGroup = NewGroup;
                            if (i != 0)
                            {
                                DataRow SubTotRow = null;
                                SubTotRow = ReRetDT.NewRow();
                                SubTotRow[0] = "Sub Total";
                                ReRetDT.Rows.Add(SubTotRow);

                                for (j = 5; j < ReRetDT.Columns.Count; j++)
                                {
                                    subTotal = 0;
                                    for (k = iIndex; k < ReRetDT.Rows.Count - 1; k++)
                                    {
                                        subTotal += Convert.ToInt32(ReRetDT.Rows[k].ItemArray[j]);
                                    }
                                    ReRetDT.Rows[ReRetDT.Rows.Count - 1][j] = subTotal;
                                }
                                iIndex = ReRetDT.Rows.Count;
                            }
                        }
                        DataRow MGRow = null;
                        MGRow = ReRetDT.NewRow();
                        MGRow[0] = RetDT.Rows[i].ItemArray[0];
                        ReRetDT.Rows.Add(MGRow);

                        for (j = 1; j < RetDT.Columns.Count; j++)
                        {
                            ReRetDT.Rows[ReRetDT.Rows.Count - 1][j] = RetDT.Rows[i].ItemArray[j];
                        }

                        if (i == RetDT.Rows.Count - 1)
                        {
                            DataRow SubTotRow = null;
                            SubTotRow = ReRetDT.NewRow();
                            SubTotRow[0] = "Sub Total";
                            ReRetDT.Rows.Add(SubTotRow);

                            for (j = 5; j < ReRetDT.Columns.Count; j++)
                            {
                                subTotal = 0;
                                for (k = iIndex; k < ReRetDT.Rows.Count - 1; k++)
                                {
                                    subTotal += Convert.ToInt32(ReRetDT.Rows[k].ItemArray[j]);
                                }
                                ReRetDT.Rows[ReRetDT.Rows.Count - 1][j] = subTotal;
                            }
                        }
                    }

                    //Total 계산
                    if (RetDT.Rows.Count > 0)
                    {
                        DataRow TotRow = null;
                        int Total;

                        TotRow = ReRetDT.NewRow();
                        TotRow[0] = "Total";
                        ReRetDT.Rows.Add(TotRow);
                        for (i = 5; i < ReRetDT.Columns.Count; i++)
                        {
                            Total = 0;
                            for (j = 0; j < ReRetDT.Rows.Count - 1; j++)
                            {
                                if (Convert.ToString(ReRetDT.Rows[j].ItemArray[0]) != "Sub Total")
                                {
                                    Total += Convert.ToInt32(ReRetDT.Rows[j].ItemArray[i]);
                                }
                            }
                            ReRetDT.Rows[ReRetDT.Rows.Count - 1][i] = Total;
                        }
                    }
                }                
                return ReRetDT;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RetDT != null) RetDT.Dispose();
            }
        }
    }
}
