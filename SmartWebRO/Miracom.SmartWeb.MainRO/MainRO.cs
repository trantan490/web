using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Collections;

using Miracom.Query;
using Miracom.SmartWeb.FWX;
using System.IO.Compression;
using System.Data.OleDb;

namespace Miracom.SmartWeb.RO
{
    public class MainRO : Miracom.Query.QueryComponent, Miracom.SmartWeb.FWX.RemoteInterface
    {
        private StdROMain oStdMain = new StdROMain();
        private StandardFunction oStdFunc = new StandardFunction();
                
        #region Handle by Function

        public bool RequestReply(string FunctionName, string sRequest, ref string sReply)
        {
            try
            {
                SPLTransferImp SPLFunc = new SPLTransferImp();
                FMBTransferImp FMBFunc = new FMBTransferImp();
                WIPTransferImp WIPFunc = new WIPTransferImp();
                
                Object[] oPara = new Object[2];
                oPara[0] = sRequest;
                oPara[1] = sReply;

                Type FunctionType = SPLFunc.GetType();
                MethodInfo mi = FunctionType.GetMethod("recv_" + FunctionName);
                if (mi != null)
                {
                    mi.Invoke(SPLFunc, oPara);
                }
                else
                {
                    FunctionType = FMBFunc.GetType();
                    mi = FunctionType.GetMethod("recv_" + FunctionName);
                    if (mi != null)
                    {
                        mi.Invoke(FMBFunc, oPara);
                    }
                    else
                    {
                        FunctionType = WIPFunc.GetType();
                        mi = FunctionType.GetMethod("recv_" + FunctionName);
                        if (mi != null)
                        {
                            mi.Invoke(WIPFunc, oPara);
                        }
                    }
                }
                sReply = oPara[1].ToString();
                
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetFuncDataTable(string FunctionName, string QueryCond)
        {
            try
            {
                DataTable dtRet = null;
                string sRet;
                string[] CondList;
                ArrayList FieldArray = new ArrayList();
                int i;

                if (QueryCond != null)
                {
                    CondList = FwxCmnFunction.UnPackCondition(QueryCond);
                    for (i = 0; i < CondList.Length; i++)
                    {
                        FieldArray.Add(CondList[i]);
                    }
                }
                else
                {
                    CondList = null;
                }
                
                Object[] oPara = new Object[1];
                oPara[0] = FieldArray;

                Type FunctionType = oStdFunc.GetType();
                MethodInfo mi = FunctionType.GetMethod(FunctionName);
                if (mi != null)
                {
                    dtRet = (DataTable)mi.Invoke(oStdFunc, oPara);
                }
                else
                {
                    dtRet = oStdMain.GetFuncDataTable(FunctionName, QueryCond);
                }

                sRet = StandardFunction.GetErrorMessage() + FwxCmnFunction.DataTableToString(dtRet);

                return sRet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetFuncDataSet(string FunctionName, string QueryCond)
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
                    default:
                        return oStdMain.GetFuncDataString(FunctionName, QueryCond);

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

        public string SelectData(string TableName, string Step, string QueryCond)
        {
            try
            {
                DataTable dtRet = null;
                string sRet = "";
                string[] CondList;
                if (QueryCond != null)
                {
                    CondList = FwxCmnFunction.UnPackCondition(QueryCond);
                }
                else
                {
                    CondList = null;
                }
                switch (TableName)
                {
                    //case "RWEBFUNDEF":
                    //    if (Step == "1")
                    //    {
                    //        dt = SvrGlobalVariable.DBQuery.GetFuncDataTable("SEL_0CFUNCNAME", null, new string[] { CondList[0] });
                    //    }
                    //    break;
                    default:
                        dtRet = oStdMain.SelectData(TableName, Step, QueryCond); 
                        break;
                        
                }

                sRet = StandardFunction.GetErrorMessage() + FwxCmnFunction.DataTableToString(dtRet);
                return sRet;
                
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
                switch (TableName)
                {
                    //case "RWEBFUNDEF":
                    //    if (Step == "1")
                    //    {
                    //        dt = SvrGlobalVariable.DBQuery.GetFuncDataTable("SEL_0CFUNCNAME", null, new string[] { CondList[0] });
                    //    }
                    //    break;
                    default:
                        return oStdMain.SelectDataCount(TableName, Step, QueryCond);
                }

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string FillData(string TableName, string Step, string QueryCond)
        {
            try
            {
                DataTable dtRet = null;
                string sRet = "";
                string[] CondList;
                if (QueryCond != null)
                {
                    CondList = FwxCmnFunction.UnPackCondition(QueryCond);
                }
                else
                {
                    CondList = null;
                }
                switch (TableName)
                {
                    //case "RWEBFUNDEF":
                    //    if (Step == "1")
                    //    {
                    //        dt = SvrGlobalVariable.DBQuery.GetFuncDataTable("SEL_0CFUNCNAME", null, new string[] { CondList[0] });
                    //    }
                    //    break;
                    default:
                        dtRet = oStdMain.FillData(TableName, Step, QueryCond);
                        break;
                        
                }
                sRet = StandardFunction.GetErrorMessage() + FwxCmnFunction.DataTableToString(dtRet);

                return sRet;
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
                switch (TableName)
                {
                    //case "RWEBFUNDEF":
                    //    if (Step == "1")
                    //    {
                    //        dt = SvrGlobalVariable.DBQuery.GetFuncDataTable("SEL_0CFUNCNAME", null, new string[] { CondList[0] });
                    //    }
                    //    break;
                    default:
                        return oStdMain.InsertData(TableName, Step, QueryCond);
                }
                
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
                switch (TableName)
                {
                    //case "RWEBFUNDEF":
                    //    if (Step == "1")
                    //    {
                    //        dt = SvrGlobalVariable.DBQuery.GetFuncDataTable("SEL_0CFUNCNAME", null, new string[] { CondList[0] });
                    //    }
                    //    break;
                    default:
                        return oStdMain.UpdateData(TableName, Step, QueryCond);
                }
                
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
                switch (TableName)
                {
                    //case "RWEBFUNDEF":
                    //    if (Step == "1")
                    //    {
                    //        dt = SvrGlobalVariable.DBQuery.GetFuncDataTable("SEL_0CFUNCNAME", null, new string[] { CondList[0] });
                    //    }
                    //    break;
                    default:
                        return oStdMain.DeleteData(TableName, Step, DeleteCond);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
