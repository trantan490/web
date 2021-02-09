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
        public DataTable STD1402(ArrayList TempList)
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
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_PRODUCTIVITY_XML_FILE);
                //Material ID 조회
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0) 
                {
                    DynamicQuery = "AND WM.FACTORY= '" + CondList[0] + "'";
                    DynamicQuery += " AND WM.WORK_DATE BETWEEN '" + CondList[1] + "' AND '" + CondList[2] + "'";

                    if (CondList[3] != null && CondList[3].TrimEnd().Length > 0 && CondList[6] != null && CondList[6].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND WM.MAT_ID= '" + CondList[3] + "' AND WM.MAT_VER= '" + CondList[6] + "'";
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

                    DynamicQuery += " GROUP BY WM.OPER, OD.OPER_DESC ORDER BY WM.OPER ";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1402", new string[] { DynamicQuery }, CondList);

                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 13; i++)
                        {
                            DataColumn MPCol = new DataColumn();

                            MPCol.ReadOnly = false;
                            MPCol.Unique = false;

                            if (i == 0)
                            {
                                MPCol.ColumnName = "Operation";
                                MPCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                MPCol.ColumnName = "Operation Desc";
                                MPCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                MPCol.ColumnName = "Queue Time (s)";
                                MPCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                MPCol.ColumnName = "Proc Time";
                                MPCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                MPCol.ColumnName = "Cycle Time (s)";
                                MPCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 5)
                            {
                                MPCol.ColumnName = "Out Lot Count";
                                MPCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 6)
                            {
                                MPCol.ColumnName = "Out Qty1";
                                MPCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 7)
                            {
                                MPCol.ColumnName = "Queue Time per Lot (s)";
                                MPCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 8)
                            {
                                MPCol.ColumnName = "QueueTime per Qty1 (s)";
                                MPCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 9)
                            {
                                MPCol.ColumnName = "Proc Time per Lot (s)";
                                MPCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 10)
                            {
                                MPCol.ColumnName = "Proc Time per Qty1 (s)";
                                MPCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 11)
                            {
                                MPCol.ColumnName = "Cycle Time per Lot (s)";
                                MPCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 12)
                            {
                                MPCol.ColumnName = "Cycle Time per Qty1 (s)";
                                MPCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(MPCol);
                        }
                    }
                                        
                    double LotCnt;
                    double OutQty1;
                    double QueueTime;
                    double ProcTime;
                    double CycleTime;

                    for(i = 0; i < RetDT.Rows.Count; i++)
                    {
                        QueueTime = Convert.ToDouble(RetDT.Rows[i].ItemArray[2]);
                        ProcTime = Convert.ToDouble(RetDT.Rows[i].ItemArray[3]);
                        CycleTime = QueueTime + ProcTime;
                        LotCnt = Convert.ToInt32(RetDT.Rows[i].ItemArray[4]);
                        OutQty1 = Convert.ToInt32(RetDT.Rows[i].ItemArray[5]);

                        DataRow Row = null;
                        Row = ReRetDT.NewRow();
                        Row[0] = RetDT.Rows[i].ItemArray[0];
                        ReRetDT.Rows.Add(Row);

                        ReRetDT.Rows[i][1] = RetDT.Rows[i].ItemArray[1];
                        ReRetDT.Rows[i][2] = QueueTime;
                        ReRetDT.Rows[i][3] = ProcTime;
                        ReRetDT.Rows[i][4] = CycleTime;
                        ReRetDT.Rows[i][5] = LotCnt;
                        ReRetDT.Rows[i][6] = OutQty1;

                        if (LotCnt > 0)
                        {
                            ReRetDT.Rows[i][7] = Math.Round(QueueTime / LotCnt, 2);
                            ReRetDT.Rows[i][9] = Math.Round(ProcTime / LotCnt, 2);
                            ReRetDT.Rows[i][11] = Math.Round(CycleTime / LotCnt, 2);
                        }
                        else
                        {
                            ReRetDT.Rows[i][7] = "0";
                            ReRetDT.Rows[i][9] = "0";
                            ReRetDT.Rows[i][11] = "0";
                        }
                        if (OutQty1 > 0)
                        {
                            ReRetDT.Rows[i][8] = Math.Round(QueueTime / OutQty1, 2);
                            ReRetDT.Rows[i][10] = Math.Round(ProcTime / OutQty1, 2);
                            ReRetDT.Rows[i][12] = Math.Round(CycleTime / OutQty1, 2);
                        }
                        else
                        {
                            ReRetDT.Rows[i][8] = "0";
                            ReRetDT.Rows[i][10] = "0";
                            ReRetDT.Rows[i][12] = "0";
                        }
                    }

                    //Total 계산
                    if (RetDT.Rows.Count > 0)
                    {
                        DataRow TotRow = null;
                        double Total;

                        TotRow = ReRetDT.NewRow();
                        TotRow[0] = "Total";
                        ReRetDT.Rows.Add(TotRow);
                        for (i = 2; i < ReRetDT.Columns.Count; i++)
                        {
                            Total = 0;
                            for (j = 0; j < ReRetDT.Rows.Count - 1; j++)
                            {
                                Total += Convert.ToDouble(ReRetDT.Rows[j].ItemArray[i]);
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
