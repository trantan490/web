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
        public DataTable STD1109(ArrayList TempList)
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
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_WIP_STATUS_XML_FILE);
                //
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    DynamicQuery = "WHERE FACTORY = '" + CondList[0] + "' AND WORK_DATE BETWEEN '" + CondList[1] + "' AND '" + CondList[2] + "'";
                    DynamicQuery += " GROUP BY WORK_DATE ORDER BY WORK_DATE";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1109", new string[] { DynamicQuery }, CondList);

                    //Total을 구하기 위해 New DataTable Column 정의
                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 7; i++)
                        {
                            DataColumn BOH_EOHCol = new DataColumn();

                            BOH_EOHCol.ReadOnly = false;
                            BOH_EOHCol.Unique = false;

                            if (i == 0)
                            {
                                BOH_EOHCol.ColumnName = "Date";
                                BOH_EOHCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                BOH_EOHCol.ColumnName = "BOH Lot";
                                BOH_EOHCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                BOH_EOHCol.ColumnName = "BOH Qty1";
                                BOH_EOHCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                BOH_EOHCol.ColumnName = "BOH Qty2";
                                BOH_EOHCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                BOH_EOHCol.ColumnName = "EOH Lot";
                                BOH_EOHCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 5)
                            {
                                BOH_EOHCol.ColumnName = "EOH Qty1";
                                BOH_EOHCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 6)
                            {
                                BOH_EOHCol.ColumnName = "EOH Qty2";
                                BOH_EOHCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(BOH_EOHCol);
                        }
                    }

                    //Total을 구하기 위해 New DataTable Value 정의
                    for (i = 0; i < RetDT.Rows.Count; i++)
                    {
                        string BOH_Date = null;
                        DataRow MGRow = null;
                        MGRow = ReRetDT.NewRow();
                        BOH_Date = Convert.ToString(RetDT.Rows[i].ItemArray[0]);
                        MGRow[0] = ToDate(BOH_Date);
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
