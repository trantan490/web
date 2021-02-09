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
        public DataTable STD1110(ArrayList TempList)
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
                    DynamicQuery = "WHERE RS.FACTORY=MO.FACTORY AND RS.OPER=MO.OPER AND RS.FACTORY= '" + CondList[0] + "'";
                    if (CondList[1] != null && CondList[1].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND RS.LOT_TYPE= '" + CondList[1] + "'";
                    }
                    DynamicQuery += " GROUP BY RS.OPER, MO.OPER_DESC ORDER BY RS.OPER";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1110", new string[] { DynamicQuery }, CondList);

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
                                OperCol.ColumnName = "TOT_QTY1";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                OperCol.ColumnName = "TOT_QTY2";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 5)
                            {
                                OperCol.ColumnName = "PROCESS_LOT";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 6)
                            {
                                OperCol.ColumnName = "PROCESS_QTY1";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 7)
                            {
                                OperCol.ColumnName = "PROCESS_QTY2";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 8)
                            {
                                OperCol.ColumnName = "HOLD_LOT";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 9)
                            {
                                OperCol.ColumnName = "HOLD_QTY1";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 10)
                            {
                                OperCol.ColumnName = "HOLD_QTY2";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 11)
                            {
                                OperCol.ColumnName = "RWK_LOT";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 12)
                            {
                                OperCol.ColumnName = "RWK_QTY1";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 13)
                            {
                                OperCol.ColumnName = "RWK_QTY2";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(OperCol);
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
                    //
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
