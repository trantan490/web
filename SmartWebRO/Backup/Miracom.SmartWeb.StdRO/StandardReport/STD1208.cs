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
        public DataTable STD1208(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable ReRetDT = new DataTable();
            string DynamicQuery = null;
            int i;
            int j;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_PRODUCTION_OUTPUT_XML_FILE);
                //
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0 && CondList[1] != null && CondList[1].TrimEnd().Length > 0 )
                {
                    DynamicQuery = " WHERE FACTORY = '" + CondList[0] + "'";
                    DynamicQuery += " AND WORK_DATE BETWEEN '" + CondList[2] + "' AND '" + CondList[3] + "'";

                    if (CondList[1] != null && CondList[1].TrimEnd().Length > 0 && CondList[4] != null && CondList[4].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND MAT_ID= '" + CondList[1] + "'" + " AND MAT_VER= '" + CondList[4] + "'";
                    }

                    DynamicQuery += " GROUP BY WORK_DATE ORDER BY WORK_DATE";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1208", new string[] { DynamicQuery }, CondList);

                    //Total을 구하기 위해 New DataTable Column 정의
                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 7; i++)
                        {
                            DataColumn MMTCol = new DataColumn();

                            MMTCol.ReadOnly = false;
                            MMTCol.Unique = false;

                            if (i == 0)
                            {
                                MMTCol.ColumnName = "Date";
                                MMTCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                MMTCol.ColumnName = "START Lot";
                                MMTCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                MMTCol.ColumnName = "START Qty1";
                                MMTCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                MMTCol.ColumnName = "START Qty2";
                                MMTCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                MMTCol.ColumnName = "END Lot";
                                MMTCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 5)
                            {
                                MMTCol.ColumnName = "END Qty1";
                                MMTCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 6)
                            {
                                MMTCol.ColumnName = "END Qty2";
                                MMTCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(MMTCol);
                        }
                    }

                    //Total을 구하기 위해 New DataTable Value 정의
                    for (i = 0; i < RetDT.Rows.Count; i++)
                    {
                        string MMT_Date = null;
                        DataRow MMTRow = null;
                        MMTRow = ReRetDT.NewRow();
                        MMT_Date = Convert.ToString(RetDT.Rows[i].ItemArray[0]);
                        MMTRow[0] = ToDate(MMT_Date);
                        ReRetDT.Rows.Add(MMTRow);
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
