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
        public DataTable STD1207(ArrayList TempList)
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
                    DynamicQuery = " WHERE FACTORY = '" + CondList[0] + "' AND OPER = '" + CondList[1] + "'";
                    DynamicQuery += " AND WORK_DATE BETWEEN '" + CondList[2] + "' AND '" + CondList[3] + "'";

                    if (CondList[4] != null && CondList[4].TrimEnd().Length > 0 && CondList[5] != null && CondList[5].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND MAT_ID= '" + CondList[4] + "' AND MAT_VER= '" + CondList[5] + "'";
                    }

                    DynamicQuery += " GROUP BY WORK_DATE ORDER BY WORK_DATE";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1207", new string[] { DynamicQuery }, CondList);

                    //Total을 구하기 위해 New DataTable Column 정의
                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 4; i++)
                        {
                            DataColumn OMTCol = new DataColumn();

                            OMTCol.ReadOnly = false;
                            OMTCol.Unique = false;

                            if (i == 0)
                            {
                                OMTCol.ColumnName = "Date";
                                OMTCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                OMTCol.ColumnName = "Out Lot Count";
                                OMTCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                OMTCol.ColumnName = "Out Qty1";
                                OMTCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                OMTCol.ColumnName = "Out Qty2";
                                OMTCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(OMTCol);
                        }
                    }

                    //Total을 구하기 위해 New DataTable Value 정의
                    for (i = 0; i < RetDT.Rows.Count; i++)
                    {
                        string OMT_Date = null;
                        DataRow OMTRow = null;
                        OMTRow = ReRetDT.NewRow();
                        OMT_Date = Convert.ToString(RetDT.Rows[i].ItemArray[0]);
                        OMTRow[0] = ToDate(OMT_Date);
                        ReRetDT.Rows.Add(OMTRow);
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
