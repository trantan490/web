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
        public DataTable STD1403_1(ArrayList TempList)
        {
            DataTable RetDT = null;
            string DynamicQuery = null;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_PRODUCTIVITY_XML_FILE);
                //
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    DynamicQuery = " AND RT.FACTORY= '" + CondList[0] + "'";
                    DynamicQuery += " AND RT.WORK_DATE BETWEEN '" + CondList[2] + "' AND '" + CondList[3] + "'";

                    if (CondList[1] != null && CondList[1].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND RT.RES_ID= '" + CondList[1] + "'";
                    }

                    DynamicQuery += " GROUP BY RT.RES_ID, RD.RES_DESC, RT.RES_UP_DOWN_FLAG, RT.RES_PRI_STS ORDER BY RT.RES_ID";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1403_1", new string[] { DynamicQuery }, CondList);                    
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
