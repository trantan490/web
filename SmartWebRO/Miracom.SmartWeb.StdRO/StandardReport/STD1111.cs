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
        public DataTable STD1111(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable ReRetDT = new DataTable();
            string DynamicQuery = null;
            int i = 0;
            int j = 0;
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

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1111", new string[] { DynamicQuery }, CondList);

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
                                MatCol.ColumnName = "Material Ver";
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
                                MatCol.ColumnName = "TOT_QTY1";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 5)
                            {
                                MatCol.ColumnName = "TOT_QTY2";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 6)
                            {
                                MatCol.ColumnName = "PROCESS_LOT";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 7)
                            {
                                MatCol.ColumnName = "PROCESS_QTY1";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 8)
                            {
                                MatCol.ColumnName = "PROCESS_QTY2";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 9)
                            {
                                MatCol.ColumnName = "HOLD_LOT";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 10)
                            {
                                MatCol.ColumnName = "HOLD_QTY1";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 11)
                            {
                                MatCol.ColumnName = "HOLD_QTY2";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 12)
                            {
                                MatCol.ColumnName = "RWK_LOT";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 13)
                            {
                                MatCol.ColumnName = "RWK_QTY1";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 14)
                            {
                                MatCol.ColumnName = "RWK_QTY2";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(MatCol);
                        }

                        //Total을 구하기 위해 New DataTable Value 정의
                        for (i = 0; i < RetDT.Rows.Count; i++)
                        {
                            DataRow OperRow = null;
                            OperRow = ReRetDT.NewRow();
                            OperRow[0] = RetDT.Rows[i].ItemArray[0];
                            ReRetDT.Rows.Add(OperRow);
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
