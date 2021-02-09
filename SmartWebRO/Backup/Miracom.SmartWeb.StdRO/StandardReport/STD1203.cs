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
        public DataTable STD1203(ArrayList TempList)
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
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0) 
                {
                    DynamicQuery = "WHERE WM.FACTORY=MD.FACTORY AND WM.FACTORY=OD.FACTORY AND WM.MAT_ID=MD.MAT_ID AND WM.OPER=OD.OPER AND WM.FACTORY= '" + CondList[0] + "'";
                    DynamicQuery += " AND WM.WORK_DATE= '" + CondList[1] + "'";

                    if (CondList[2] != null && CondList[2].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND WM.OPER= '" + CondList[2] + "'";
                    }
                    
                    if (CondList[3] != null && CondList[3].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND MD.MAT_TYPE = '" + CondList[3] + "'";
                    }
                    if (CondList[5] != null && CondList[4].TrimEnd().Length > 0)
                    {
                        if (CondList[5] == "1")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_1 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "2")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_2 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "3")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_3 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "4")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_4 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "5")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_5 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "6")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_6 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "7")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_7 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "8")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_8 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "9")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_9 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "10")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_10 = '" + CondList[4] + "'";
                        }
                    }
                    DynamicQuery += " GROUP BY WM.OPER,OD.OPER_DESC ORDER BY WM.OPER, OD.OPER_DESC ";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1203", new string[] { DynamicQuery }, CondList);

                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 5; i++)
                        {
                            DataColumn MMCol = new DataColumn();

                            MMCol.ReadOnly = false;
                            MMCol.Unique = false;

                            if (i == 0)
                            {
                                MMCol.ColumnName = "Operation";
                                MMCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                MMCol.ColumnName = "Description";
                                MMCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                MMCol.ColumnName = "Lot";
                                MMCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                MMCol.ColumnName = "Qty1";
                                MMCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                MMCol.ColumnName = "Qty2";
                                MMCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(MMCol);
                        }
                    }

                    //Total을 구하기 위해 New DataTable Value 정의
                    for (i = 0; i < RetDT.Rows.Count; i++)
                    {
                        DataRow MMRow = null;
                        MMRow = ReRetDT.NewRow();
                        MMRow[0] = RetDT.Rows[i].ItemArray[0];
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
