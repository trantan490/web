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
        public DataTable STD1903(ArrayList TempList)
        {
            DataTable RetDT = new DataTable();
            string DynamicQuery = null;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_TRACEABILITY_XML_FILE);
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0 && CondList[1] != null && CondList[1].TrimEnd().Length > 0)
                {
                    DynamicQuery = " WHERE FACTORY= '" + CondList[0] + "'" + "AND RES_ID= '" + CondList[1] + "'";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1903", new string[] { DynamicQuery }, CondList);
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
