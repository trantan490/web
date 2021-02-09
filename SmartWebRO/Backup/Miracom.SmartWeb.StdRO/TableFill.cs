using System;
using System.Data;

using Miracom.Query;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.RO
{

    public class TableFill 
    {

        public TableFill() 
		{
        }

        public DataTable FillData(string TableName, string Step, string[] CondList)
        {

            DataTable dt = null;
            try
            {
                string QueryName = "DB_" + TableName + ".xml";
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + QueryName);
                switch (TableName)
                {
                    case "RWEBFUNDEF":
                        if (Step == "1")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_OCALL", null, null);
                        } else if (Step == "2")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_2CFACANDGRP", null, new string[] { CondList[0], CondList[1], CondList[2] });
                        }
                        break;

                    case "RWEBFUNITM":
                        if (Step == "1")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_RWEBFUNITM_LIST", null, new string[] { CondList[0], CondList[1],CondList[2] });
                        }
                        break;                        

                    case "RWEBGRPDEF":
                        if (Step == "1")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEC_ISFACTORY", null, new string[] { CondList[0] });
                        }
                        break;

                    case "RWEBUSRDEF":
                        if (Step == "2")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_0CFACTORY", null, new string[] { CondList[0] });
                        }
                        break;

                    case "RWEBGRPFUN":
                        if (Step == "1")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_2C_FACTORY.SECGRP", null, new string[] { CondList[0], CondList[1] });
                        }
                        break;

                    case "MWIPFACDEF":
                        if (Step == "1")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_", null, null);
                        }
                        else if(Step == "2")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_TYPE", null, new string[] { CondList[0] });
                        }
                        break;

                    case "MWIPMATDEF":
                        if (Step == "1")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_MAT", new string[] { CondList[0] }, null);
                        }
                        else if (Step == "2")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_MAT_VER", new string[] { CondList[0] }, null);
                        }
                        break;

                    case "MWIPFLWDEF":
                        if (Step == "1")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_FACTORY", null, new string[] { CondList[0] });
                        }
                        else if (Step == "2")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_FLW_MAT", new string[] { CondList[0] }, null);
                        }
                        else if (Step == "3")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_FLW_SEQ", new string[] { CondList[0] }, null);
                        }
                        else if (Step == "4")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_FLW", new string[] { CondList[0] }, null);
                        }
                        break;

                    case "MWIPOPRDEF":
                        if (Step == "1")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_FACTORY", null, new string[] { CondList[0] });
                        }
                        else if (Step == "2")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_OPR_FLW", new string[] { CondList[0] }, null);
                        }
                        else if (Step == "3")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_OPR", new string[] { CondList[0] }, null);
                        }
                        break;

                    case "MGCMTBLDAT":
                        if (Step == "1")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_FACTORY.TABLE_NAME", null, new string[] { CondList[0], CondList[1] });
                        }
                        break;

                    case "RWEBFLXINQ":
                        if (Step == "1")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_FACTORY", null, new string[] { CondList[0] });
                        }
                        else if (Step == "2")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_FACTORY.GROUP", null, new string[] { CondList[0], CondList[1] });
                        }
                        break;

                    case "RWEBFLXCOL":
                        if (Step == "1")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_RWEBFLXCOL", null, new string[] { CondList[0], CondList[1] });
                        }
                        break;

                    case "RSUMWIPLTH":
                        if (Step == "1")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_FLEX", new string[] { CondList[0] }, null);
                        }
                        break;
                }
                
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null) dt.Dispose();
            }
        }

    }
}
