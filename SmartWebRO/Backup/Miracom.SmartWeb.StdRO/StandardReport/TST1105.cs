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
        public DataTable TST1105(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable ReRetDT = new DataTable();
            string DynamicQuery = null;
            int i;
            int j;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_MWIPLOTSTS_XML_FILE);

                DynamicQuery = "";
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    DynamicQuery = " WHERE OPER_IN_TIME < '" + CondList[0].TrimEnd() + "'||TO_CHAR(SYSDATE,'HH24MISS')";
                }
                if (CondList[1] != null && CondList[1].TrimEnd().Length > 0)
                {
                    DynamicQuery += " AND FACTORY = '" + CondList[1].TrimEnd() + "'";
                }
                if (CondList[2] != null && CondList[2].TrimEnd().Length > 0)
                {
                    DynamicQuery += " AND MAT_ID = '" + CondList[2].TrimEnd() + "'";
                }
                if (CondList[2] != null && CondList[2].TrimEnd().Length > 0 && CondList[3] != null && CondList[3].TrimEnd().Length > 0)
                {
                    DynamicQuery += " AND MAT_VER = '" + CondList[3].TrimEnd() + "'";
                }
                if (CondList[4] != null && CondList[4].TrimEnd().Length > 0)
                {
                    DynamicQuery += " AND FLOW = '" + CondList[4].TrimEnd() + "'";
                }
                if (CondList[4] != null && CondList[4].TrimEnd().Length > 0 && CondList[5] != null && CondList[5].TrimEnd().Length > 0)
                {
                    DynamicQuery += " AND FLOW_SEQ_NUM = '" + CondList[5].TrimEnd() + "'";
                }
                if (CondList[6] != null && CondList[6].TrimEnd().Length > 0)
                {
                    DynamicQuery += " AND OPER = '" + CondList[6].TrimEnd() + "'";
                }

                    DynamicQuery += " ORDER BY FACTORY,MAT_ID,MAT_VER,FLOW, FLOW_SEQ_NUM, OPER ";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_MFO_LOTSTS", new string[] { DynamicQuery }, CondList);


                    //가변적 Column생성
                    for (i = 0; i < RetDT.Columns.Count; i++)
                    {
                        DataColumn OperCol = new DataColumn();

                        OperCol.ReadOnly = false;
                        OperCol.Unique = false;

                        OperCol.ColumnName = Convert.ToString(RetDT.Columns[i].ColumnName).TrimEnd();
                        OperCol.DataType = System.Type.GetType("System.String");

                        ReRetDT.Columns.Add(OperCol);
                    }

                    //Row 등록
                    for (i = 0; i < RetDT.Rows.Count; i++)
                    {
                        DataRow LotsRow = null;
                        LotsRow = ReRetDT.NewRow();
                        LotsRow[0] = RetDT.Rows[i].ItemArray[0];
                        ReRetDT.Rows.Add(LotsRow);
                        for (j = 1; j < RetDT.Columns.Count; j++)
                        {
                            ReRetDT.Rows[i][j] = RetDT.Rows[i].ItemArray[j];
                        }
                    }

                return ReRetDT;
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