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
        public DataTable STD1403(ArrayList TempList)
        {
            DataTable RetDT = new DataTable();            
            string DynamicQuery = null;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_PRODUCTIVITY_XML_FILE);
                
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0) 
                {
                    DynamicQuery = "WHERE FACTORY= '" + CondList[0] + "'";
                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1403", new string[] { DynamicQuery }, CondList);
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
