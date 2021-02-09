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
        public DataTable TST1107(ArrayList TempList)
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
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_RWEBUSRLOG_XML_FILE);

                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_GRP_RWEBUSRLOG", null, new string[] { CondList[0], CondList[1] });

                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 3; i++)
                        {
                            DataColumn Col = new DataColumn();

                            Col.ReadOnly = false;
                            Col.Unique = false;

                            if (i == 0)
                            {
                                Col.ColumnName = "User Group";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                Col.ColumnName = "Login Count";
                                Col.DataType = System.Type.GetType("System.Int32");
                            }
                            else if (i == 2)
                            {
                                Col.ColumnName = "Time Interval";
                                Col.DataType = System.Type.GetType("System.Int32");
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


        public DataTable TST1107_1(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable ReRetDT = new DataTable();
            int i;
            int j;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_RWEBUSRLOG_XML_FILE);

                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_GRP_DETAIL_RWEBUSRLOG", null, new string[] { CondList[0], CondList[1], CondList[2] });

                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 4; i++)
                        {
                            DataColumn Col = new DataColumn();

                            Col.ReadOnly = false;
                            Col.Unique = false;

                            if (i == 0)
                            {
                                Col.ColumnName = "User ID";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                Col.ColumnName = "Login Time";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                Col.ColumnName = "Logout Time";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                Col.ColumnName = "Time Interval(분)";
                                Col.DataType = System.Type.GetType("System.Int32");
                            }
                            ReRetDT.Columns.Add(Col);
                        }

                        for (i = 0; i < RetDT.Rows.Count; i++)
                        {
                            DataRow Row = null;
                            Row = ReRetDT.NewRow();
                            Row[0] = i + 1;
                            ReRetDT.Rows.Add(Row);

                            for (j = 0; j <4; j++)
                            {
                                ReRetDT.Rows[i][j] = RetDT.Rows[i].ItemArray[j];
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
