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
        public DataTable STD1102(ArrayList TempList)
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
                    DynamicQuery = "WHERE RS.FACTORY=MM.FACTORY AND RS.MAT_ID=MM.MAT_ID AND RS.MAT_VER = MM.MAT_VER AND RS.FACTORY= '" + CondList[0] + "'";
                    if (CondList[1] != null && CondList[1].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND RS.LOT_TYPE= '" + CondList[1] + "'";
                    }
                    if (CondList[2] != null && CondList[2].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND MM.MAT_TYPE= '" + CondList[2] + "'";
                    }
                    if (CondList[4] != null && CondList[4].TrimEnd().Length > 0)
                    {
                        if (CondList[4] == "1")
                        {
                            DynamicQuery += " AND MM.MAT_GRP_1= '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "2")
                        {
                            DynamicQuery += " AND MM.MAT_GRP_2= '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "3")
                        {
                            DynamicQuery += " AND MM.MAT_GRP_3= '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "4")
                        {
                            DynamicQuery += " AND MM.MAT_GRP_4= '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "5")
                        {
                            DynamicQuery += " AND MM.MAT_GRP_5= '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "6")
                        {
                            DynamicQuery += " AND MM.MAT_GRP_6= '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "7")
                        {
                            DynamicQuery += " AND MM.MAT_GRP_7= '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "8")
                        {
                            DynamicQuery += " AND MM.MAT_GRP_8= '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "9")
                        {
                            DynamicQuery += " AND MM.MAT_GRP_9= '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "10")
                        {
                            DynamicQuery += " AND MM.MAT_GRP_10= '" + CondList[3] + "'";
                        }
                    }
                    DynamicQuery += " GROUP BY RS.MAT_ID, RS.MAT_VER, MM.MAT_DESC ORDER BY RS.MAT_ID, RS.MAT_VER";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1102", new string[] { DynamicQuery }, CondList);

                    //Total값을 구하기 위해 DataTable 재구성
                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 15; i++)
                        {
                            DataColumn MatCol = new DataColumn();

                            MatCol.ReadOnly = false;
                            MatCol.Unique = false;

                            if (i == 0)
                            {
                                MatCol.ColumnName = "Material";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                MatCol.ColumnName = "Material Version";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                MatCol.ColumnName = "Desc";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                MatCol.ColumnName = "TOT_LOT";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                MatCol.ColumnName = "TOT_QTY_1";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 5)
                            {
                                MatCol.ColumnName = "TOT_QTY_2";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 6)
                            {
                                MatCol.ColumnName = "PROCESS_LOT";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 7)
                            {
                                MatCol.ColumnName = "PROCESS_QTY_1";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 8)
                            {
                                MatCol.ColumnName = "PROCESS_QTY_2";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 9)
                            {
                                MatCol.ColumnName = "HOLD_LOT";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 10)
                            {
                                MatCol.ColumnName = "HOLD_QTY_1";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 11)
                            {
                                MatCol.ColumnName = "HOLD_QTY_2";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 12)
                            {
                                MatCol.ColumnName = "RWK_LOT";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 13)
                            {
                                MatCol.ColumnName = "RWK_QTY_1";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 14)
                            {
                                MatCol.ColumnName = "RWK_QTY_2";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(MatCol);
                        }

                        for (i = 0; i < RetDT.Rows.Count; i++)
                        {
                            DataRow MatRow = null;
                            MatRow = ReRetDT.NewRow();
                            MatRow[0] = RetDT.Rows[i].ItemArray[0];
                            MatRow[1] = RetDT.Rows[i].ItemArray[1];
                            MatRow[2] = RetDT.Rows[i].ItemArray[2];
                            MatRow[3] = RetDT.Rows[i].ItemArray[3];
                            MatRow[4] = RetDT.Rows[i].ItemArray[4];
                            MatRow[5] = RetDT.Rows[i].ItemArray[5];
                            MatRow[6] = RetDT.Rows[i].ItemArray[6];
                            MatRow[7] = RetDT.Rows[i].ItemArray[7];
                            MatRow[8] = RetDT.Rows[i].ItemArray[8];
                            MatRow[9] = RetDT.Rows[i].ItemArray[9];
                            MatRow[10] = RetDT.Rows[i].ItemArray[10];
                            MatRow[11] = RetDT.Rows[i].ItemArray[11];
                            MatRow[12] = RetDT.Rows[i].ItemArray[12];
                            MatRow[13] = RetDT.Rows[i].ItemArray[13];
                            MatRow[14] = RetDT.Rows[i].ItemArray[14];

                            ReRetDT.Rows.Add(MatRow);
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
                            Tot_TotLot = Tot_TotLot + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[3]);
                            Tot_TotQty1 = Tot_TotQty1 + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[4]);
                            Tot_TotQty2 = Tot_TotQty2 + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[5]);
                            Tot_ProcLot = Tot_ProcLot + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[6]);
                            Tot_ProcQty1 = Tot_ProcQty1 + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[7]);
                            Tot_ProcQty2 = Tot_ProcQty2 + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[8]);
                            Tot_HoldLot = Tot_HoldLot + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[9]);
                            Tot_HoldQty1 = Tot_HoldQty1 + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[10]);
                            Tot_HoldQty2 = Tot_HoldQty2 + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[11]);
                            Tot_ReLot = Tot_ReLot + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[12]);
                            Tot_ReQty1 = Tot_ReQty1 + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[13]);
                            Tot_ReQty2 = Tot_ReQty2 + Convert.ToDouble(ReRetDT.Rows[i].ItemArray[14]);
                        }
                        TotRow[3] = Tot_TotLot; TotRow[4] = Tot_TotQty1; TotRow[5] = Tot_TotQty2;
                        TotRow[6] = Tot_ProcLot; TotRow[7] = Tot_ProcQty1; TotRow[8] = Tot_ProcQty2;
                        TotRow[9] = Tot_HoldLot; TotRow[10] = Tot_HoldQty1; TotRow[11] = Tot_HoldQty2;
                        TotRow[12] = Tot_ReLot; TotRow[13] = Tot_ReQty1; TotRow[14] = Tot_ReQty2;

                        ReRetDT.Rows.Add(TotRow);
                    }
                }
                //
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
