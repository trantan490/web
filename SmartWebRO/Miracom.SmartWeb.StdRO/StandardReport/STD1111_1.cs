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
        public DataTable STD1111_1(ArrayList TempList)
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
                    DynamicQuery = "WHERE MAT_ID = '" + CondList[1] + "' AND MAT_VER = '" + CondList[2] + "' AND FACTORY = '" + CondList[0] + "' AND LOT_DEL_FLAG = ' '";
                    DynamicQuery += " ORDER BY LOT_ID";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1111_1", new string[] { DynamicQuery }, CondList);
                                        
                    for (i = 0; i < RetDT.Columns.Count; i++)
                    {
                        DataColumn DetailCol = new DataColumn();

                        DetailCol.ReadOnly = false;
                        DetailCol.Unique = false;
                        
                        DetailCol.ColumnName = RetDT.Columns[i].ColumnName;
                        DetailCol.DataType = System.Type.GetType("System.String");

                        ReRetDT.Columns.Add(DetailCol);
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
                            if (RetDT.Columns[j].ColumnName.IndexOf("TIME") > 0)
                            {
                                if (RetDT.Rows[i].ItemArray[j].ToString().Trim() != "")
                                {
                                    ReRetDT.Rows[i][j] = ToDate(RetDT.Rows[i].ItemArray[j].ToString().Trim());
                                }
                                else
                                {
                                    ReRetDT.Rows[i][j] = RetDT.Rows[i].ItemArray[j];
                                }

                            }
                            else
                            {
                                ReRetDT.Rows[i][j] = RetDT.Rows[i].ItemArray[j];
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
