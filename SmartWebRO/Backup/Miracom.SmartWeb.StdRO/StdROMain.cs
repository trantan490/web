using System;
using System.Data;
using System.Reflection;

using Miracom.Query;
using Miracom.SmartWeb.FWX;
using System.Data.OleDb;

namespace Miracom.SmartWeb.RO
{
    
    public class StdROMain
    {
        private TableDelete oTableDelete = new TableDelete();
        private TableFill oTableFill = new TableFill();
        private TableInsert oTableInsert = new TableInsert();
        private TableSelect oTableSelect = new TableSelect();
        private TableSelectCount oTableSelectCount = new TableSelectCount();
        private TableUpdate oTableUpdate = new TableUpdate();

        private StandardFunction oStdFunction = new StandardFunction();

        public StdROMain()
		{
            StdGlobalVariable.ROXml.WebcfgFile = @"/bin/StdROEnv.xml";
            StdGlobalVariable.QueryPath = System.AppDomain.CurrentDomain.BaseDirectory + "Query";
        }

        #region Handle by Function

        public DataTable GetFuncDataTable(string FunctionName, string QueryCond)
        {
            try
            {
                DataTable dtRet = null;
                Object[] objParam = new Object[1];

                objParam[0] = QueryCond;
                Type FunctionType = oStdFunction.GetType();
                MethodInfo mi = FunctionType.GetMethod(FunctionName);
                if (mi != null)
                {
                    if (QueryCond == null)
                    {
                        dtRet = (DataTable)mi.Invoke(oStdFunction, null);
                    }
                    else
                    {
                        dtRet = (DataTable)mi.Invoke(oStdFunction, objParam);
                    }
                    
                }

                return dtRet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetFuncDataSet(string FunctionName, string QueryCond)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetFuncDataInt(string FunctionName, string QueryCond)
        {
            int i = 0;
            try
            {
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetFuncDataString(string FunctionName, string QueryCond)
        {
            try
            {
                string sData = "";
                string[] CondList;
                if (QueryCond != null)
                {
                    CondList = FwxCmnFunction.UnPackCondition(QueryCond);
                }
                else
                {
                    CondList = null;
                }
                switch (FunctionName)
                {
                    case "GetSysTime":
                        sData = oStdFunction.GetSysDate();
                        break;
                }

                return sData;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Handle by Table

        public DataTable SelectData(string TableName, string Step, string QueryCond)
        {
            try
            {
                DataTable dtRet = null;
                string[] CondList;
                if (QueryCond != null)
                {
                    CondList = FwxCmnFunction.UnPackCondition(QueryCond);
                }
                else
                {
                    CondList = null;
                }

                dtRet = oTableSelect.SelectData(TableName, Step, CondList);
                return dtRet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SelectDataCount(string TableName, string Step, string QueryCond)
        {
            try
            {
                string[] CondList;
                if (QueryCond != null)
                {
                    CondList = FwxCmnFunction.UnPackCondition(QueryCond);
                }
                else
                {
                    CondList = null;
                }

                return oTableSelectCount.SelectDataCount(TableName, Step, CondList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable FillData(string TableName, string Step, string QueryCond)
        {
            try
            {
                DataTable dtRet = null;
                string[] CondList;
                if (QueryCond != null)
                {
                    CondList = FwxCmnFunction.UnPackCondition(QueryCond);
                }
                else
                {
                    CondList = null;
                }

                dtRet = oTableFill.FillData(TableName, Step, CondList);
                return dtRet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertData(string TableName, string Step, string QueryCond)
        {
            try
            {
                string[] CondList;
                if (QueryCond != null)
                {
                    CondList = FwxCmnFunction.UnPackCondition(QueryCond);
                }
                else
                {
                    CondList = null;
                }

                return oTableInsert.InsertData(TableName, Step, CondList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateData(string TableName, string Step, string QueryCond)
        {
            try
            {
                string[] CondList;
                if (QueryCond != null)
                {
                    CondList = FwxCmnFunction.UnPackCondition(QueryCond);
                }
                else
                {
                    CondList = null;
                }

                return oTableUpdate.UpdateData(TableName, Step, CondList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteData(string TableName, string Step, string DeleteCond)
        {
            try
            {
                string[] CondList;
                if (DeleteCond != null)
                {
                    CondList = FwxCmnFunction.UnPackCondition(DeleteCond);
                }
                else
                {
                    CondList = null;
                }

                return oTableDelete.DeleteData(TableName, Step, CondList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


    }
}
