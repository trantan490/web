using System;
using System.Data;

using Miracom.Query;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.RO
{

    public class TableSelectCount 
    {

        public TableSelectCount() 
		{
        }

        public int SelectDataCount(string TableName, string Step, string[] CondList)
        {

            int i = 0;
            try
            {
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + "DB_" + TableName + ".xml");

                switch (TableName)
                {
                    case "RWEBGRPFUN":
                        if (Step == "1")
                        {
                            i = StdGlobalVariable.DBQuery.GetFuncDataInt("SEL_CNT_FACT.SECGRP.FUNCGRP", null, new string[] { CondList[0], CondList[1], CondList[2] });
                        }
                        else if (Step == "2")
                        {
                            i = StdGlobalVariable.DBQuery.GetFuncDataInt("SEL_MAXSEQ_FACT.SECGRP", null, new string[] { CondList[0], CondList[1] });
                        }
                        else if (Step == "3")
                        {
                            i = StdGlobalVariable.DBQuery.GetFuncDataInt("SEL_MAXSEQ_FACT.SECGRP.FUNCGRP", null, new string[] { CondList[0], CondList[1], CondList[2] });
                        }
                        else if (Step == "4")
                        {
                            // 2010-12-02-정비재 : Report 메뉴의 권한을 부여하기 위하여 신규로 추가함.
                            i = StdGlobalVariable.DBQuery.GetFuncDataInt("SEL_MAXSEQ_FACT.SECGRP.FUNCGRP2", null, new string[] { CondList[0], CondList[1], CondList[2], CondList[3] });
                        }

                        break;
                    case "RWEBFUNLOG":
                        if (Step == "1")
                        {
                            i = StdGlobalVariable.DBQuery.GetFuncDataInt("SEL_COUNT_FUNLOG", null, new string[] { CondList[0], CondList[1], CondList[2], CondList[3] });
                        }
                        break;

                }

                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
