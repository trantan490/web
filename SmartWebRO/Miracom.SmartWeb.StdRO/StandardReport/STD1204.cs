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
        public DataTable STD1204(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable ReRetDT = new DataTable();
            string DynamicQuery = null;            
            int i;
            int j;
            int k;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_PRODUCTION_OUTPUT_XML_FILE);
                //
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    DynamicQuery = " WHERE WM.FACTORY=OD.FACTORY AND WM.OPER=OD.OPER AND WM.FACTORY= '" + CondList[0] + "'";
                    DynamicQuery += " AND WM.WORK_DATE= '" + CondList[1] + "'";

                    if (CondList[3] != null && CondList[2].TrimEnd().Length > 0)
                    {
                        if (CondList[3] == "1")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_1 = '" + CondList[2] + "'";
                        }
                        else if (CondList[3] == "2")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_2 = '" + CondList[2] + "'";
                        }
                        else if (CondList[3] == "3")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_3 = '" + CondList[2] + "'";
                        }
                        else if (CondList[3] == "4")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_4 = '" + CondList[2] + "'";
                        }
                        else if (CondList[3] == "5")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_5 = '" + CondList[2] + "'";
                        }
                        else if (CondList[3] == "6")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_6 = '" + CondList[2] + "'";
                        }
                        else if (CondList[3] == "7")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_7 = '" + CondList[2] + "'";
                        }
                        else if (CondList[3] == "8")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_8 = '" + CondList[2] + "'";
                        }
                        else if (CondList[3] == "9")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_9 = '" + CondList[2] + "'";
                        }
                        else if (CondList[3] == "10")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_10 = '" + CondList[2] + "'";
                        }
                    }

                    DynamicQuery += " GROUP BY WM.OPER, OD.OPER_DESC, WM.MAT_ID, WM.MAT_VER ORDER BY WM.OPER, OD.OPER_DESC, WM.MAT_ID, WM.MAT_VER";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1204", new string[] { DynamicQuery }, CondList);

                    //Total을 구하기 위해 New DataTable Column 정의
                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 7; i++)
                        {
                            DataColumn OMMCol = new DataColumn();

                            OMMCol.ReadOnly = false;
                            OMMCol.Unique = false;

                            if (i == 0)
                            {
                                OMMCol.ColumnName = "Operation";
                                OMMCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                OMMCol.ColumnName = "Description";
                                OMMCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                OMMCol.ColumnName = "Material";
                                OMMCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                OMMCol.ColumnName = "Material Ver";
                                OMMCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                OMMCol.ColumnName = "Lot";
                                OMMCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 5)
                            {
                                OMMCol.ColumnName = "Qty1";
                                OMMCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 6)
                            {
                                OMMCol.ColumnName = "Qty2";
                                OMMCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(OMMCol);
                        }
                    }

                    //Sub Total 계산
                    int iIndex;
                    int subTotal;                    
                    string CurrentGroup="";
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

                                for (j = 4; j<ReRetDT.Columns.Count; j++)
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
                            ReRetDT.Rows[ReRetDT.Rows.Count-1][j] = RetDT.Rows[i].ItemArray[j];
                        }

                        if (i == RetDT.Rows.Count - 1)
                        {
                            DataRow SubTotRow = null;
                            SubTotRow = ReRetDT.NewRow();
                            SubTotRow[0] = "Sub Total";
                            ReRetDT.Rows.Add(SubTotRow);

                            for (j = 4; j < ReRetDT.Columns.Count; j++)
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
                        for (i = 4; i < ReRetDT.Columns.Count; i++)
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
