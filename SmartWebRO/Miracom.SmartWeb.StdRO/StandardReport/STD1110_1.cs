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
        public DataTable STD1110_1(ArrayList TempList)
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
                    DynamicQuery = "WHERE OPER = '"  + CondList[1] + "' AND FACTORY = '" + CondList[0] +"' AND LOT_DEL_FLAG = ' '";
                    DynamicQuery += " ORDER BY LOT_ID";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1110_1", new string[] { DynamicQuery }, CondList);

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
                                OperCol.ColumnName = "LOT_ID";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                OperCol.ColumnName = "LOT_DESC";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                OperCol.ColumnName = "Factory";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                OperCol.ColumnName = "MAT_ID";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                OperCol.ColumnName = "MAT_VER";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 5)
                            {
                                OperCol.ColumnName = "FLOW";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 6)
                            {
                                OperCol.ColumnName = "Oper";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 7)
                            {
                                OperCol.ColumnName = "Qty1";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 8)
                            {
                                OperCol.ColumnName = "Qty2";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 9)
                            {
                                OperCol.ColumnName = "Qty3";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 10)
                            {
                                OperCol.ColumnName = "OPER_IN_TIME";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 11)
                            {
                                OperCol.ColumnName = "LOT_TYPE";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 12)
                            {
                                OperCol.ColumnName = "CREATE_CODE";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 13)
                            {
                                OperCol.ColumnName = "OWNER_CODE";
                                OperCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(OperCol);
                        }

                        //New DataTable Value 정의
                        for (i = 0; i < RetDT.Rows.Count; i++)
                        {                            
                            DataRow OperRow = null;
                            OperRow = ReRetDT.NewRow();
                            OperRow[0] = RetDT.Rows[i].ItemArray[0];
                            ReRetDT.Rows.Add(OperRow);
                            for (j = 1; j < RetDT.Columns.Count; j++)
                            {
                                if (j == 10)
                                {
                                    ReRetDT.Rows[i][j] = ToDate(RetDT.Rows[i].ItemArray[j].ToString());
                                }
                                else
                                {
                                    ReRetDT.Rows[i][j] = RetDT.Rows[i].ItemArray[j];
                                }
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
