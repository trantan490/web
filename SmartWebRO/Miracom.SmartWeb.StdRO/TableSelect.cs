using System;
using System.Data;

using Miracom.Query;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.RO
{

    public class TableSelect 
    {

        public TableSelect() 
		{
        }

        public DataTable SelectData(string TableName, string Step, string[] CondList)
        {

            StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + "DB_" + TableName + ".xml");
            DataTable dt = null;
            try
            {
                switch (TableName)
                {
                    case "RWEBFUNDEF":
                        if (Step == "1")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_0CFUNCNAME", null, new string[] { CondList[0] });
                        }
                        break;
                    case "RWEBGRPDEF":
                        if (Step == "1")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_ISFACANDGRP", null, new string[] { CondList[0], CondList[1] });
                        }
                        break;
                    case "RWEBUSRDEF":
                        if (Step == "1")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_0CFACANDUSER", null, new string[] { CondList[0], CondList[1] });
                        } else if (Step == "2")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_0CFACANDGRP", null, new string[] { CondList[0], CondList[1] });
                        }
                        else if (Step == "T") //WebService를 TEST하는 쿼리호출(변경하지마세요)
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("CONN_TEST", null, new string[] { CondList[0] });
                        }
                        break;
                    case "RWEBGRPFUN":
                        if (Step == "1")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_ISFACANDGRP", null, new string[] { CondList[0], CondList[1] });
                        }
                        else if (Step == "2")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_0C_FAC.SECGRP.FUNCGRP.FUNCNAME", null, new string[] { CondList[0], CondList[1], CondList[2], CondList[3] });
                        }
                        else if (Step == "3")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_MAXSEQ_FACT.SECGRP.FUNCGRP", null, new string[] { CondList[0], CondList[1], CondList[2] });
                        }
                        break;
                    case "MWIPFACDEF":
                        if (Step == "1")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_FACTORY", null, new string[] { CondList[0] });
                        }
                        break;
                    case "MWIPCALDEF":
                        if (Step == "1")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_FACTORY.YEAR.MONTH.DAY", null, new string[] { CondList[0], CondList[1], CondList[2], CondList[3] });
                        }
                        break;
                    case "RWEBFLXINQ":
                        if (Step == "1")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_FACTORY.INQ", null, new string[] { CondList[0], CondList[1] });
                        }
                        else if (Step == "2")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_FACTORY", null, new string[] { CondList[0], CondList[1], CondList[2], CondList[3] });
                        }
                        break;
                    case "RWEBFLXCOL":
                        if (Step == "1")
                        {
                            dt = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_FACTORY", null, new string[] { CondList[0], CondList[1], CondList[2], CondList[3] });
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
