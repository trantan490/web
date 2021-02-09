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
        public DataTable STD1201(ArrayList TempList)
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
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    DynamicQuery = " WHERE FACTORY = '" + CondList[0] + "' AND WORK_DATE BETWEEN '" + CondList[1] + "' AND '" + CondList[2] + "'";
                    DynamicQuery += " GROUP BY WORK_DATE ORDER BY WORK_DATE";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1201", new string[] { DynamicQuery }, CondList);

                    //Total을 구하기 위해 New DataTable Column 정의
                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 7; i++)
                        {
                            DataColumn IN_OUTCol = new DataColumn();

                            IN_OUTCol.ReadOnly = false;
                            IN_OUTCol.Unique = false;

                            if (i == 0)
                            {
                                IN_OUTCol.ColumnName = "Date";
                                IN_OUTCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                IN_OUTCol.ColumnName = "IN Lot";
                                IN_OUTCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                IN_OUTCol.ColumnName = "IN Qty1";
                                IN_OUTCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                IN_OUTCol.ColumnName = "IN Qty2";
                                IN_OUTCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                IN_OUTCol.ColumnName = "OUT Lot";
                                IN_OUTCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 5)
                            {
                                IN_OUTCol.ColumnName = "OUT Qty1";
                                IN_OUTCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 6)
                            {
                                IN_OUTCol.ColumnName = "OUT Qty2";
                                IN_OUTCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(IN_OUTCol);
                        }
                    }

                    //Total을 구하기 위해 New DataTable Value 정의
                    for (i = 0; i < RetDT.Rows.Count; i++)
                    {
                        string INOUT_Date = null;
                        DataRow FacRow = null;
                        FacRow = ReRetDT.NewRow();
                        INOUT_Date = Convert.ToString(RetDT.Rows[i].ItemArray[0]);
                        FacRow[0] = ToDate(INOUT_Date);
                        ReRetDT.Rows.Add(FacRow);
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
