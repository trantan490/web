using System;
using System.Data;

using Miracom.Query;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.RO;
using System.Collections;

namespace Miracom.SmartWeb.RO
{
    public partial class StandardFunction 
    {
        public DataTable STD1105(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable ReRetDT = new DataTable();
            string DynamicQuery = null;
            string Mat_Grp_Name = null;
            int i;
            int j;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_WIP_STATUS_XML_FILE);
                //
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0 && CondList[2] != null && CondList[2].TrimEnd().Length > 0 && CondList[3] != null && CondList[3].TrimEnd().Length > 0)
                {
                    if (CondList[3] == "1") {Mat_Grp_Name = "MAT_GRP_1";}
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

                    DynamicQuery = "SELECT WM." + Mat_Grp_Name + " AS GROUP_NAME, TD.DATA_1 AS GROUP_DESC, WM.TOT_LOT, WM.TOT_QTY_1, WM.TOT_QTY_2";
                    DynamicQuery += " FROM (SELECT MD.FACTORY, MD." + Mat_Grp_Name + " , SUM(WS.TOT_LOT) AS TOT_LOT, SUM(WS.TOT_QTY_1) AS TOT_QTY_1, SUM(WS.TOT_QTY_2) AS TOT_QTY_2";
                    DynamicQuery += " FROM MWIPMATDEF MD, RSUMWIPSTS WS WHERE MD.FACTORY=WS.FACTORY AND MD.MAT_ID = WS.MAT_ID";
                    DynamicQuery += " AND MD.FACTORY='" + CondList[0] + "'";
                    DynamicQuery += " AND MD." + Mat_Grp_Name + " <> ' '";

                    if (CondList[1].TrimEnd() != "") { DynamicQuery += " AND WS.LOT_TYPE= '" + CondList[1] + "'"; }
                    if (CondList[2].TrimEnd() != "") { DynamicQuery += " AND MD.MAT_TYPE= '" + CondList[2] + "'"; }

                    DynamicQuery += " GROUP BY MD.FACTORY, MD." + Mat_Grp_Name + ") WM LEFT OUTER JOIN MGCMTBLDAT TD";
                    DynamicQuery += " ON TD.FACTORY=WM.FACTORY AND TD.TABLE_NAME = 'MATERIAL_GRP_" + CondList[3] + "' AND TD.KEY_1 = WM." + Mat_Grp_Name + " ORDER BY WM." + Mat_Grp_Name;

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("DYNAMIC", new string[] { DynamicQuery }, CondList);

                    //Total 및 SubTatal 구하기 위해 New DataTable Column 정의
                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 5; i++)
                        {
                            DataColumn MatCol = new DataColumn();

                            MatCol.ReadOnly = false;
                            MatCol.Unique = false;

                            if (i == 0)
                            {
                                MatCol.ColumnName = "Group";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                MatCol.ColumnName = "Group Desc";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                MatCol.ColumnName = "Total Lot";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                MatCol.ColumnName = "Total Qty1";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                MatCol.ColumnName = "Total Qty2";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(MatCol);
                        }
                    }
                    //for (i = 0; i < RetDT.Columns.Count; i++)
                    //{
                    //    DataColumn MGCol = new DataColumn();

                    //    MGCol.ReadOnly = false;
                    //    MGCol.Unique = false;

                    //    MGCol.ColumnName = RetDT.Columns[i].ColumnName;
                    //    MGCol.DataType = System.Type.GetType("System.String");

                    //    ReRetDT.Columns.Add(MGCol);
                    //}

                    //Total 및 SubTotal 구하기 위해 New DataTable Value 정의
                    for (i = 0; i < RetDT.Rows.Count; i++)
                    {
                        DataRow MGRow = null;
                        MGRow = ReRetDT.NewRow();
                        MGRow[0] = RetDT.Rows[i].ItemArray[0];
                        ReRetDT.Rows.Add(MGRow);
                        for (j = 1; j < RetDT.Columns.Count; j++)
                        {
                            ReRetDT.Rows[i][j] = RetDT.Rows[i].ItemArray[j];
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
                        for (i = 2; i < ReRetDT.Columns.Count; i++)
                        {
                            Total = 0;
                            for (j = 0; j < ReRetDT.Rows.Count - 1; j++)
                            {
                                Total += Convert.ToInt32(ReRetDT.Rows[j].ItemArray[i]);
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
