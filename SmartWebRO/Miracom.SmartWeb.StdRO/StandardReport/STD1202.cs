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
        public DataTable STD1202(ArrayList TempList)
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
                    DynamicQuery = " WHERE WM.FACTORY = OD.FACTORY AND WM.OPER=OD.OPER AND WM.FACTORY= '" + CondList[0] + "'";
                    DynamicQuery += " AND WM.WORK_DATE BETWEEN '" + CondList[1] + "' AND '" + CondList[2] + "'";

                    if (CondList[4] != null && CondList[3].TrimEnd().Length > 0)
                    {
                        if (CondList[4] == "1")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_1 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "2")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_2 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "3")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_3 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "4")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_4 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "5")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_5 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "6")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_6 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "7")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_7 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "8")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_8 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "9")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_9 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "10")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_10 = '" + CondList[3] + "'";
                        }
                    }

                    DynamicQuery += " GROUP BY WM.OPER, OD.OPER_DESC ORDER BY WM.OPER";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1202", new string[] { DynamicQuery }, CondList);

                    //Total을 구하기 위해 New DataTable Column 정의
                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 5; i++)
                        {
                            DataColumn OMCol = new DataColumn();

                            OMCol.ReadOnly = false;
                            OMCol.Unique = false;

                            if (i == 0)
                            {
                                OMCol.ColumnName = "Operation";
                                OMCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                OMCol.ColumnName = "Description";
                                OMCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                OMCol.ColumnName = "Lot";
                                OMCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                OMCol.ColumnName = "Qty1";
                                OMCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                OMCol.ColumnName = "Qty2";
                                OMCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(OMCol);
                        }
                    }

                    //Total을 구하기 위해 New DataTable Value 정의
                    for (i = 0; i < RetDT.Rows.Count; i++)
                    {                        
                        DataRow OMRow = null;
                        OMRow = ReRetDT.NewRow();
                        OMRow[0] = RetDT.Rows[i].ItemArray[0];
                        ReRetDT.Rows.Add(OMRow);
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
