using System;
using System.Data;
using System.Collections;

using Miracom.Query;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.RO;


namespace Miracom.SmartWeb.RO
{
    public partial class StandardFunction 
    {
        public DataTable STD1101(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable ReRetDT = new DataTable();
            string DynamicQuery = null;
            int i = 0;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_WIP_STATUS_XML_FILE);
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    DynamicQuery = "WHERE RS.FACTORY=MO.FACTORY AND RS.OPER=MO.OPER AND RS.FACTORY= '" + CondList[0] + "'";
                    if (CondList[1] != null && CondList[1].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND RS.LOT_TYPE= '" + CondList[1] + "'";
                    }
                    DynamicQuery += " GROUP BY RS.OPER, MO.OPER_DESC ORDER BY RS.OPER";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1101", new string[] { DynamicQuery }, CondList);

                    //Total값을 구하기 위해 DataTable 재구성
                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 14; i++)
                        {
                            DataColumn OperCol = new DataColumn();

                            OperCol.ReadOnly = false;
                            OperCol.Unique = false;

                            if (i == 0)
                            {
                                OperCol.ColumnName = "Oper";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                OperCol.ColumnName = "Desc";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                OperCol.ColumnName = "TOT_LOT";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                OperCol.ColumnName = "TOT_QTY_1";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                OperCol.ColumnName = "TOT_QTY_2";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 5)
                            {
                                OperCol.ColumnName = "PROCESS_LOT";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 6)
                            {
                                OperCol.ColumnName = "PROCESS_QTY_1";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 7)
                            {
                                OperCol.ColumnName = "PROCESS_QTY_2";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 8)
                            {
                                OperCol.ColumnName = "HOLD_LOT";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 9)
                            {
                                OperCol.ColumnName = "HOLD_QTY_1";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 10)
                            {
                                OperCol.ColumnName = "HOLD_QTY_2";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 11)
                            {
                                OperCol.ColumnName = "RWK_LOT";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 12)
                            {
                                OperCol.ColumnName = "RWK_QTY_1";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 13)
                            {
                                OperCol.ColumnName = "RWK_QTY_2";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(OperCol);
                        }

                        for (i = 0; i < RetDT.Rows.Count; i++)
                        {
                            DataRow OperRow = null;
                            OperRow = ReRetDT.NewRow();
                            OperRow[0] = RetDT.Rows[i].ItemArray[0];
                            OperRow[1] = RetDT.Rows[i].ItemArray[1];
                            OperRow[2] = RetDT.Rows[i].ItemArray[2];
                            OperRow[3] = RetDT.Rows[i].ItemArray[3];
                            OperRow[4] = RetDT.Rows[i].ItemArray[4];
                            OperRow[5] = RetDT.Rows[i].ItemArray[5];
                            OperRow[6] = RetDT.Rows[i].ItemArray[6];
                            OperRow[7] = RetDT.Rows[i].ItemArray[7];
                            OperRow[8] = RetDT.Rows[i].ItemArray[8];
                            OperRow[9] = RetDT.Rows[i].ItemArray[9];
                            OperRow[10] = RetDT.Rows[i].ItemArray[10];
                            OperRow[11] = RetDT.Rows[i].ItemArray[11];
                            OperRow[12] = RetDT.Rows[i].ItemArray[12];
                            OperRow[13] = RetDT.Rows[i].ItemArray[13];

                            ReRetDT.Rows.Add(OperRow);
                        }

                        DataRow TotRow = null;
                        double Tot_TotLot = 0, Tot_TotQty1 = 0, Tot_TotQty2 = 0;
                        double Tot_ProcLot = 0, Tot_ProcQty1 = 0, Tot_ProcQty2 = 0;
                        double Tot_HoldLot = 0, Tot_HoldQty1 = 0, Tot_HoldQty2 = 0;
                        double Tot_ReLot = 0, Tot_ReQty1 = 0, Tot_ReQty2 = 0;

                        TotRow = ReRetDT.NewRow();
                        TotRow[0] = "Total";
                        for (i = 0; i < ReRetDT.Rows.Count; i++)
                        {
                            Tot_TotLot = Tot_TotLot + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[2]);
                            Tot_TotQty1 = Tot_TotQty1 + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[3]);
                            Tot_TotQty2 = Tot_TotQty2 + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[4]);
                            Tot_ProcLot = Tot_ProcLot + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[5]);
                            Tot_ProcQty1 = Tot_ProcQty1 + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[6]);
                            Tot_ProcQty2 = Tot_ProcQty2 + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[7]);
                            Tot_HoldLot = Tot_HoldLot + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[8]);
                            Tot_HoldQty1 = Tot_HoldQty1 + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[9]);
                            Tot_HoldQty2 = Tot_HoldQty2 + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[10]);
                            Tot_ReLot = Tot_ReLot + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[11]);
                            Tot_ReQty1 = Tot_ReQty1 + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[12]);
                            Tot_ReQty2 = Tot_ReQty2 + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[13]);
                        }
                        TotRow[2] = Tot_TotLot; TotRow[3] = Tot_TotQty1; TotRow[4] = Tot_TotQty2;
                        TotRow[5] = Tot_ProcLot; TotRow[6] = Tot_ProcQty1; TotRow[7] = Tot_ProcQty2;
                        TotRow[8] = Tot_HoldLot; TotRow[9] = Tot_HoldQty1; TotRow[10] = Tot_HoldQty2;
                        TotRow[11] = Tot_ReLot; TotRow[12] = Tot_ReQty1; TotRow[13] = Tot_ReQty2;

                        ReRetDT.Rows.Add(TotRow);
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
