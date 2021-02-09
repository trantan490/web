using System;
using System.Data;

using Miracom.Query;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.RO
{

    public class TableInsert 
    {

        public TableInsert() 
		{
        }

        public bool InsertData(string TableName, string Step, string[] CondList)
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
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("INS_RWEBFUNDEF", null, CondList);
                        }
                        break;
                    case "RWEBGRPDEF":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("INS_RWEBGRPDEF", null, CondList);
                        }
                        break;
                    case "RWEBFUNITM":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("INS_RWEBFUNITM", null, CondList);
                        }
                        break;
                    case "RWEBUSRDEF":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("INS_RWEBUSRDEF", null, CondList);
                        }
                        break;
                    case "RWEBGRPFUN":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("INS_RWEBGRPFUN", null, CondList);
                        }
                        break;
                    case "RWEBFLXINQ":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("INS_RWEBFLXINQ", null, CondList);
                        }
                        break;
                    case "RWEBFLXCOL":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("INS_RWEBFLXCOL", null, CondList);
                        }
                        break;
                    case "RWEBUSRLOG":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("INS_RWEBUSRLOG", null, CondList);
                        }
                        break;
                    case "RWEBFUNLOG":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("INS_RWEBFUNLOG", null, CondList);
                        }
                        break;
					case "CWIPPLNWEK_N":
						switch (Step)
						{
							case "I1":		// 2013-09-26-정비재 : INSERT
								bResult = StdGlobalVariable.DBQuery.FuncExecute("INSERT_CWIPPLNWEK_N_1", null, CondList);
								break;
						}
						break;
					case "RWIPPLNWEK_N":
						switch (Step)
						{
							case "I1":		// 2013-09-26-정비재 : INSERT
								bResult = StdGlobalVariable.DBQuery.FuncExecute("INSERT_RWIPPLNWEK_N_1", null, CondList);
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
