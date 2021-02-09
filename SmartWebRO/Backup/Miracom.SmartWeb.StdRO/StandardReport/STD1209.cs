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
        public DataTable STD1209(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable DetailDT = new DataTable();
            DataTable ReRetDT = new DataTable();
            string DynamicQuery = null;
            int i;
            int j;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_PRODUCTION_OUTPUT_XML_FILE);
                //Material ID 조회
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0 && CondList[3] != null && CondList[3].TrimEnd().Length > 0 && CondList[6] != null && CondList[6].TrimEnd().Length > 0) 
                {
                    DynamicQuery = "WHERE WM.FACTORY=MD.FACTORY AND WM.FACTORY= '" + CondList[0] + "'";
                    DynamicQuery += " AND WM.WORK_DATE BETWEEN '" + CondList[1] + "' AND '" + CondList[2] + "'";
                    DynamicQuery += " AND WM.OPER= '" + CondList[3] + "'";
                    
                    if (CondList[4] != null && CondList[4].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND MD.MAT_TYPE= '" + CondList[4] + "'";
                    }

                    if (CondList[6] != null && CondList[5].TrimEnd().Length > 0)
                    {
                        if (CondList[6] == "1")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_1 = '" + CondList[5] +  "'";
                        }
                        else if (CondList[6] == "2")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_2 = '" + CondList[5] + "'";
                        }
                        else if (CondList[6] == "3")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_3 = '" + CondList[5] + "'";
                        }
                        else if (CondList[6] == "4")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_4 = '" + CondList[5] + "'";
                        }
                        else if (CondList[6] == "5")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_5 = '" + CondList[5] + "'";
                        }
                        else if (CondList[6] == "6")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_6 = '" + CondList[5] + "'";
                        }
                        else if (CondList[6] == "7")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_7 = '" + CondList[5] + "'";
                        }
                        else if (CondList[6] == "8")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_8 = '" + CondList[5] + "'";
                        }
                        else if (CondList[6] == "9")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_9 = '" + CondList[5] + "'";
                        }
                        else if (CondList[6] == "10")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_10 = '" + CondList[5] + "'";
                        }
                    }
                    DynamicQuery += " GROUP BY WM.WORK_DATE ORDER BY WM.WORK_DATE ";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1209", new string[] { DynamicQuery }, CondList);

                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 7; i++)
                        {
                            DataColumn MGMTCol = new DataColumn();

                            MGMTCol.ReadOnly = false;
                            MGMTCol.Unique = false;

                            if (i == 0)
                            {
                                MGMTCol.ColumnName = "Date";
                                MGMTCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                MGMTCol.ColumnName = "START Lot";
                                MGMTCol.DataType = System.Type.GetType("System.String");
                            }                           
                            else if (i == 2)
                            {
                                MGMTCol.ColumnName = "START Qty1";
                                MGMTCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                MGMTCol.ColumnName = "START Qty2";
                                MGMTCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                MGMTCol.ColumnName = "END Lot";
                                MGMTCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 5)
                            {
                                MGMTCol.ColumnName = "END Qty1";
                                MGMTCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 6)
                            {
                                MGMTCol.ColumnName = "END Qty2";
                                MGMTCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(MGMTCol);
                        }
                    }

                    //Total을 구하기 위해 New DataTable Value 정의
                    for (i = 0; i < RetDT.Rows.Count; i++)
                    {
                        DataRow MMRow = null;
                        MMRow = ReRetDT.NewRow();
                        MMRow[0] = ToDate(RetDT.Rows[i].ItemArray[0].ToString());
                        ReRetDT.Rows.Add(MMRow);
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
                        for (i = 1; i < ReRetDT.Columns.Count; i++)
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
