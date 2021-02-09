using System;
using System.Data;

using Miracom.Query;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.RO
{

    public class TableUpdate 
    {

        public TableUpdate() 
		{
        }

        public bool UpdateData(string TableName, string Step, string[] CondList)
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
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("UDP_RWEBFUNDEF", null, CondList);
                        }
                        break;
                    case "RWEBFUNITM":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("UDP_RWEBFUNITM_FUNC_SEQINC", null, CondList);
                        }
                        else if (Step == "2")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("UDP_RWEBFUNITM_FUNC_SEQDEC", null, CondList);
                        }
                        break;
                    case "RWEBGRPDEF":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("UPD_RWEBGRPDEF", null, CondList);
                        }
                        break;
                    case "RWEBUSRDEF":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("UPD_RWEBUSRDEF", null, CondList);
                        }
                        break;
                        
                    case "RWEBGRPFUN":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("UDP_3C_RWEBGRPFUN_GRPSEQINC", null, CondList);
                        }
                        else if (Step == "2")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("UDP_3C_RWEBGRPFUN_GRPID", null, CondList);
                        }
                        else if (Step == "3")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("UDP_3C_RWEBGRPFUN_GRPSEQDEC", null, CondList);
                        }
                        else if (Step == "4")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("UDP_3C_RWEBGRPFUN_GRPSEQ", null, CondList);
                        }
                        else if (Step == "5")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("UDP_3C_RWEBGRPFUN_FUNCSEQDEC", null, CondList);
                        }
                        else if (Step == "6")
                        {
                            // 2010-12-02-정비재 : Report Menu의 Function의 순서를 변경하기 위하여 추가함.
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("UDP_3C_RWEBGRPFUN_FUNSEQ", null, CondList);
                        }
                        break;
                    case "RWEBFLXINQ":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("UDP_FACTORY.INQ", null, CondList);
                        }
                        break;
                    case "RWEBFLXCOL":
                        if (Step == "1")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("UDP_FACTORY.INQ.COL", null, CondList);
                        }
                        else if (Step == "2")
                        {
                            bResult = StdGlobalVariable.DBQuery.FuncExecute("UDP_FACTORY.INQ.SEQ", null, CondList);
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
