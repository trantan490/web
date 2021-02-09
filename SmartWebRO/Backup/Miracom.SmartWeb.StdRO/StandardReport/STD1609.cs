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
        public DataTable STD1609(ArrayList TempList)
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
                    DynamicQuery = " AND OLD_FACTORY = '" + CondList[0] + "'";

                    if (CondList[1].Trim() != "")
                    {
                        DynamicQuery += " AND OLD_OPER= '" + CondList[1] + "'";
                    }

                    if (CondList[2].Trim() != "")
                    {
                        DynamicQuery += " AND LOT_ID = '" + CondList[2] + "'";
                    }

                    DynamicQuery += " AND OPER_IN_TIME > '" + CondList[3] + "'";
                    DynamicQuery += " AND OPER_IN_TIME <= '" + CondList[4] + "'";

                    if (CondList[5].Trim() == "")
                    {
                        DynamicQuery += " AND QTY_1 <> 0 ";
                    }

                    DynamicQuery += "ORDER BY LOT_ID, HIST_SEQ ";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1609", new string[] { DynamicQuery }, CondList);
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
