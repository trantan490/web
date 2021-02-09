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
        public DataTable STD1608(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable ReRetDT = new DataTable();
            string DynamicQuery = null;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_QUALITY_XML_FILE);
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    DynamicQuery = " AND A.OLD_FACTORY= '" + CondList[0] + "'";

                    if (CondList[1].Trim() != "")
                    {
                        DynamicQuery += " AND A.OLD_OPER= '" + CondList[1] + "'";
                    }

                    if (CondList[2].Trim() != "")
                    {
                        DynamicQuery += " AND A.LOT_ID = '" + CondList[2] + "'";
                    }
                    else
                    {
                        DynamicQuery += " AND A.TRAN_TIME > '" + CondList[3] + "'";
                        DynamicQuery += " AND A.TRAN_TIME <= '" + CondList[4] + "'";
                    }

                    DynamicQuery += " ORDER BY A.LOT_ID, A.HIST_SEQ ";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1608", new string[] { DynamicQuery }, CondList);
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
    }
}
