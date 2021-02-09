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
        public DataTable STD1604_1(ArrayList TempList)
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
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_QUALITY_XML_FILE);
                //
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    DynamicQuery = " WHERE FACTORY= '" + CondList[0] + "' AND QTY_FLAG='1'";
                    DynamicQuery += " AND TRAN_TIME BETWEEN '" + CondList[1] + "' AND '" + CondList[2] + "'";

                    if (CondList[3] != null && CondList[3].TrimEnd().Length > 0)
                    {
                        DynamicQuery += "AND LOSS_CODE= '" + CondList[3] + "' AND HIST_DEL_FLAG=' '";
                    }

                    DynamicQuery += " ORDER BY TRAN_TIME DESC, LOT_ID ASC, HIST_SEQ DESC, SEQ_NUM ASC ";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1604_1", new string[] { DynamicQuery }, CondList);

                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 14; i++)
                        {
                            DataColumn Col = new DataColumn();

                            Col.ReadOnly = false;
                            Col.Unique = false;

                            if (i == 0)
                            {
                                Col.ColumnName = "Lot ID";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                Col.ColumnName = "Hist Seq";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                Col.ColumnName = "Qty Flag";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                Col.ColumnName = "Tran Time";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                Col.ColumnName = "Hist Del Flag";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 5)
                            {
                                Col.ColumnName = "Factory";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 6)
                            {
                                Col.ColumnName = "Material";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 7)
                            {
                                Col.ColumnName = "Material Ver";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 8)
                            {
                                Col.ColumnName = "Flow";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 9)
                            {
                                Col.ColumnName = "Operation";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 10)
                            {
                                Col.ColumnName = "Resource";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 11)
                            {
                                Col.ColumnName = "Seq Num";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 12)
                            {
                                Col.ColumnName = "Loss Code";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 13)
                            {
                                Col.ColumnName = "Loss Qty";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(Col);
                        }

                        for (i = 0; i < RetDT.Rows.Count; i++)
                        {
                            DataRow Row = null;
                            Row = ReRetDT.NewRow();
                            Row[0] = RetDT.Rows[i].ItemArray[0];
                            ReRetDT.Rows.Add(Row);
                            for (j = 1; j < RetDT.Columns.Count; j++)
                            {
                                if (j == 3)
                                {
                                    ReRetDT.Rows[i][j] = ToDate(RetDT.Rows[i].ItemArray[j].ToString().Trim());
                                }
                                else
                                {
                                    ReRetDT.Rows[i][j] = RetDT.Rows[i].ItemArray[j];
                                }
                            }
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
