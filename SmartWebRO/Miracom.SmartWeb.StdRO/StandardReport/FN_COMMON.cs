using System;
using System.Data;
using System.Collections;
using Miracom.Query;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.RO;

using System.Data.OleDb;

namespace Miracom.SmartWeb.RO
{
    public partial class StandardFunction
    {
        public DataTable VIEW_FACCMF_LIST(ArrayList TempList)
        {
            DataTable RetDT = null;
            string DynamicQuery = null;

            string[] CondList = null;

            CondList = (String[])TempList.ToArray(typeof(string));

            try
            {
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_COMMON_XML_FILE);
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    DynamicQuery = "WHERE FACTORY = '" + CondList[0] + "'";
                    DynamicQuery += "AND  ITEM_NAME = '" + CondList[1] + "'";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("VIEW_FACCMF_LIST", new string[] { DynamicQuery }, CondList);
                }
                return RetDT;
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

        public DataTable VIEW_GCMGRP_LIST(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable ReRetDT = new DataTable();
            string DynamicQuery = null;
            int i = 0;

            string[] CondList = null;

            CondList = (String[])TempList.ToArray(typeof(string));

            try
            {
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_COMMON_XML_FILE);
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("VIEW_GCMGRP_LIST", new string[] { DynamicQuery }, CondList);

                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 2; i++)
                        {
                            DataColumn GRPCol = new DataColumn();

                            GRPCol.ReadOnly = false;
                            GRPCol.Unique = false;

                            if (i == 0)
                            {
                                GRPCol.ColumnName = "GROUP";
                                GRPCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                GRPCol.ColumnName = "DESC";
                                GRPCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(GRPCol);
                        }

                        for (i = 0; i < RetDT.Rows.Count; i++)
                        {
                            DataRow GRPRow = null;
                            GRPRow = ReRetDT.NewRow();
                            GRPRow[0] = RetDT.Rows[i].ItemArray[0];
                            GRPRow[1] = RetDT.Rows[i].ItemArray[1];

                            ReRetDT.Rows.Add(GRPRow);
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

        public DataTable VIEW_OPER_LIST(ArrayList TempList)
        {
            DataTable RetDT = null;
            string DynamicQuery = null;

            string[] CondList = null;

            CondList = (String[])TempList.ToArray(typeof(string));

            try
            {
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_COMMON_XML_FILE);

                RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("VIEW_OPER_LIST", new string[] { DynamicQuery }, CondList);
                return RetDT;
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

        public DataTable VIEW_FUNCGRP_LIST(ArrayList TempList)
        {
            DataTable RetDT = null;
            string DynamicQuery = null;

            string[] CondList = null;

            CondList = (String[])TempList.ToArray(typeof(string));

            try
            {
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_RWEBGRPFUN_XML_FILE);
                
                RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_FUNCGROUP", new string[] { DynamicQuery }, CondList);

                return RetDT;
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

        public DataTable DYNAMIC(ArrayList TempList)
        {
            DataTable RetDT = new DataTable();
            string[] DynamicQuery = null;
            string[] CondList = null;
            DynamicQuery = (String[])TempList.ToArray(typeof(string));

            OleDbConnection odcon = null;
            OleDbCommand odcmd = null;
            OleDbDataAdapter odda = null;
            try
            {
                // 2010-08-12-정비재 : Oracle Package/Procedure를 실행하기 위하여 추가함.
                if (DynamicQuery[0] != "PROCEDURE" && DynamicQuery[0] != "PACKAGE")
                {
                    StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_COMMON_XML_FILE);
                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("DYNAMIC", new string[] { DynamicQuery[0] }, CondList);
                }
                else
                {
                    // 2010-08-11-정비재 : DataBase 연결하는 부분
                    //                   : 기존읜 연결문자열은 OLEDB로 Oracle Package/Procedure를 실행하지 못하여 새로운 연결문자열을 추가함.
                    odcon = new OleDbConnection(StdGlobalVariable.ROXml.GetValue("//appSettings//add[@key='RPTConnectionString']"));
                    odcon.Open();

                    odcmd = new OleDbCommand();
                    odcmd.Connection = odcon;
                    odcmd.CommandText = DynamicQuery[1];
                    odcmd.CommandType = CommandType.StoredProcedure;

                    for (int iRow = 2; iRow < DynamicQuery.Length; iRow++)
                    {
                        if (DynamicQuery[iRow] != "")
                        {
                            odcmd.Parameters.Add(DynamicQuery[iRow].Split(':')[0].Trim(), OleDbType.VarChar).Value = DynamicQuery[iRow].Split(':')[1].Trim();
                        }
                    }
                    // 2010-08-12-정비재 : Oracle Package/Procedure를 실행한다.
                    odda = new OleDbDataAdapter(odcmd);
                    odda.Fill(RetDT);
                }
                return RetDT;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (odcon != null) odcon.Close();
                if (odcmd != null) odcmd.Dispose();
                if (odda != null) odda.Dispose();
                if (RetDT != null) RetDT.Dispose();
            }
        }
    }
}
    
