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
        public DataTable STD1301(ArrayList TempList)
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
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_PRODUCTION_PLAN_XML_FILE);
                //
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0 && CondList[1] != null && CondList[1].TrimEnd().Length > 0 )
                {
                    DynamicQuery = " WHERE WM.FACTORY=MD.FACTORY AND WM.FACTORY=PD.FACTORY AND WM.MAT_ID=MD.MAT_ID AND WM.MAT_VER=MD.MAT_VER AND WM.WORK_DATE=PD.WORK_DATE";
                    DynamicQuery += " AND WM.FACTORY= '" + CondList[0] + "' AND WM.OPER= '" + CondList[1] + "'";
                    DynamicQuery += " AND WM.WORK_DATE BETWEEN '" + CondList[2] + "' AND '" + CondList[3] + "'";

                    if (CondList[4] != null && CondList[4].TrimEnd().Length > 0 && CondList[5] != null && CondList[5].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND WM.MAT_ID= '" + CondList[4] + "' AND WM.MAT_VER= '" + CondList[5] + "'";
                    }

                    DynamicQuery += " GROUP BY WM.MAT_ID, WM.MAT_VER, MD.MAT_DESC ORDER BY WM.MAT_ID, WM.MAT_VER";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1301", new string[] { DynamicQuery }, CondList);

                    //Total을 구하기 위해 New DataTable Column 정의
                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 6; i++)
                        {
                            DataColumn PlanCol = new DataColumn();

                            PlanCol.ReadOnly = false;
                            PlanCol.Unique = false;

                            if (i == 0)
                            {
                                PlanCol.ColumnName = "Material";
                                PlanCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                PlanCol.ColumnName = "Material Ver";
                                PlanCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                PlanCol.ColumnName = "Material Description";
                                PlanCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                PlanCol.ColumnName = "Plan Qty";
                                PlanCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                PlanCol.ColumnName = "Out Lot Count";
                                PlanCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 5)
                            {
                                PlanCol.ColumnName = "Out Qty1";
                                PlanCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(PlanCol);
                        }
                    }

                    //Total을 구하기 위해 New DataTable Value 정의
                    for (i = 0; i < RetDT.Rows.Count; i++)
                    {                        
                        DataRow PlanRow = null;
                        PlanRow = ReRetDT.NewRow();
                        PlanRow[0] = Convert.ToString(RetDT.Rows[i].ItemArray[0]);
                        ReRetDT.Rows.Add(PlanRow);
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
                        for (i = 3; i < ReRetDT.Columns.Count; i++)
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
