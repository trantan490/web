using System;
using System.Data;

using Miracom.Query;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.RO
{

    public class TableDelete 
    {

        public TableDelete() 
		{
        }

        public bool DeleteData(string TableName, string Step, string[] CondList)
        {

            bool bResult = false;
            try
            {
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + "DB_" + TableName + ".xml");

                switch (TableName)
                {
                    case "RWEBFUNDEF":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("DEL_RWEBFUNDEF", null, CondList);
                        }
                        break;
                    case "RWEBFUNITM":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("DEL_RWEBFUNITM", null, CondList);
                        }
                        break;
                    case "RWEBGRPDEF":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("DEL_RWEBGRPDEF", null, CondList);
                        }
                        break;
                    case "RWEBUSRDEF":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("DEL_RWEBUSRDEF", null, CondList);
                        }
                        break;
                    case "RWEBGRPFUN":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("DEL_0C_FAC.SECGRP.FUNCGRP", null, CondList);
                        }
                        else if (Step == "2")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("DEL_0C_FAC.SECGRP.FUNCGRP.FUNCNAME", null, CondList);
                        }                        
                        break;
                    case "RWEBFLXCOL":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("DEL_FACTROY.INQUIRY", null, CondList);
                        }
                        else if (Step == "2")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("DEL_FACTORY.INQUIRY.SEQ.COLUMN", null, CondList);
                        }
                        break;
                    case "RWEBFLXINQ":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("DEL_REBFLXINQ", null, CondList);
                        }
                        break;
					case "CWIPPLNWEK_N":
						switch (Step)
						{
							case "D1":		// 2013-09-26-정비재 : DELETE
								bResult = StdGlobalVariable.DBQuery.FuncExecute("DELETE_CWIPPLNWEK_N_1", null, CondList);
								break;
						}
						break;
					case "RWIPPLNWEK_N":
						switch (Step)
						{
							case "D1":		// 2013-09-26-정비재 : DELETE
								bResult = StdGlobalVariable.DBQuery.FuncExecute("DELETE_RWIPPLNWEK_N_1", null, CondList);
								break;
						}
						break;
                }

                return bResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
