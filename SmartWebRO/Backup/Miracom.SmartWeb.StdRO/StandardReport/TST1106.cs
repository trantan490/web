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
        public DataTable TST1106(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable ReRetDT = new DataTable();
            int i;
            int j;
            int k;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_RWEBFUNLOG_XML_FILE);
                
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_ALL_FUNCGROUP", null, new string[] { CondList[0], CondList[1] });

                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 4; i++)
                        {
                            DataColumn Col = new DataColumn();

                            Col.ReadOnly = false;
                            Col.Unique = false;

                            if (i == 0)
                            {
                                Col.ColumnName = "Function Group";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                Col.ColumnName = "Function Name";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                Col.ColumnName = "View Count";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                Col.ColumnName = "View Percent";
                                Col.DataType = System.Type.GetType("System.Double");
                            }
                            ReRetDT.Columns.Add(Col);
                        }
                    }
                }

                //Row 등록
                for (i = 0; i < RetDT.Rows.Count; i++)
                {
                    DataRow Row = null;
                    Row = ReRetDT.NewRow();
                    Row[0] = i;
                    ReRetDT.Rows.Add(Row);

                    for (j = 0; j < 4; j++)
                    {
                        ReRetDT.Rows[i][j] = RetDT.Rows[i].ItemArray[j];
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


        public DataTable TST1106_Chart(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable ReRetDT = new DataTable();
            int i;
            int j;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_RWEBFUNLOG_XML_FILE);

                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_CHART_FUNGROUP", null, new string[] { CondList[0], CondList[1] });

                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 3; i++)
                        {
                            DataColumn Col = new DataColumn();

                            Col.ReadOnly = false;
                            Col.Unique = false;

                            if (i == 0)
                            {
                                Col.ColumnName = "Function Group";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                Col.ColumnName = "View Count";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                Col.ColumnName = "View Percent";
                                Col.DataType = System.Type.GetType("System.Double");
                            }
                            ReRetDT.Columns.Add(Col);
                        }
                    }
                }

                //Row 등록
                for (i = 0; i < RetDT.Rows.Count; i++)
                {
                    DataRow Row = null;
                    Row = ReRetDT.NewRow();
                    Row[0] = i;
                    ReRetDT.Rows.Add(Row);

                    for (j = 0; j < 3; j++)
                    {
                        ReRetDT.Rows[i][j] = RetDT.Rows[i].ItemArray[j];
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


        public DataTable TST1106_1(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable ReRetDT = new DataTable();
            int i;
            int j;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_RWEBFUNLOG_XML_FILE);

                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_VIEW_FUNCTION", null, new string[] { CondList[0], CondList[1], CondList[2] });

                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 3; i++)
                        {
                            DataColumn Col = new DataColumn();

                            Col.ReadOnly = false;
                            Col.Unique = false;

                            if (i == 0)
                            {
                                Col.ColumnName = "Function Name";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                Col.ColumnName = "View Count";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                Col.ColumnName = "View Percent";
                                Col.DataType = System.Type.GetType("System.Double");
                            }
                            ReRetDT.Columns.Add(Col);
                        }
                    }
                }

                //Row 등록
                for (i = 0; i < RetDT.Rows.Count; i++)
                {
                    DataRow Row = null;
                    Row = ReRetDT.NewRow();
                    Row[0] = i;
                    ReRetDT.Rows.Add(Row);

                    for (j = 0; j < 3; j++)
                    {
                        ReRetDT.Rows[i][j] = RetDT.Rows[i].ItemArray[j];
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
