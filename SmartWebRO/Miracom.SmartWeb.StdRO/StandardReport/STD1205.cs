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
        public DataTable STD1205(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable DetailDT = new DataTable();
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
                //Material ID 조회
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0) 
                {
                    DynamicQuery = "WHERE WM.FACTORY = MD.FACTORY AND WM.MAT_ID=MD.MAT_ID AND WM.MAT_VER = MD.MAT_VER AND WM.FACTORY= '" + CondList[0] + "'";
                    DynamicQuery += " AND WM.WORK_DATE BETWEEN '" + CondList[1] + "' AND '" + CondList[2] + "'";
                                        

                    if (CondList[4] != null && CondList[3].TrimEnd().Length > 0)
                    {
                        if (CondList[4] == "1")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_1 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "2")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_2 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "3")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_3 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "4")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_4 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "5")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_5 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "6")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_6 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "7")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_7 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "8")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_8 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "9")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_9 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "10")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_10 = '" + CondList[3] + "'";
                        }
                    }
                    if (CondList[5] != null && CondList[5].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND WM.OPER= '" + CondList[5] + "'";
                    }

                    DynamicQuery += " GROUP BY WM.MAT_ID, WM.MAT_VER, MD.MAT_DESC, WM.OPER ORDER BY WM.MAT_ID, WM.MAT_VER, WM.OPER ";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1205", new string[] { DynamicQuery }, CondList);

                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 7; i++)
                        {
                            DataColumn MOMCol = new DataColumn();

                            MOMCol.ReadOnly = false;
                            MOMCol.Unique = false;

                            if (i == 0)
                            {
                                MOMCol.ColumnName = "Material";
                                MOMCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                MOMCol.ColumnName = "Material Ver";
                                MOMCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                MOMCol.ColumnName = "Material Desc";
                                MOMCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                MOMCol.ColumnName = "Operation";
                                MOMCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                MOMCol.ColumnName = "Out Lot";
                                MOMCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 5)
                            {
                                MOMCol.ColumnName = "Out Qty1";
                                MOMCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 6)
                            {
                                MOMCol.ColumnName = "Out Qty2";
                                MOMCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(MOMCol);
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

                                for (j = 4; j < ReRetDT.Columns.Count; j++)
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
