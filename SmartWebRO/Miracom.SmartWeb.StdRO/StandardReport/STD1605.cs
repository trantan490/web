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
        public DataTable STD1605(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable ReRetDT = new DataTable();
            int i;
            int j;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_QUALITY_XML_FILE);
                //
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1605", null, new string[] { CondList[0], CondList[1], CondList[2]});

                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 4; i++)
                        {
                            DataColumn Col = new DataColumn();

                            Col.ReadOnly = false;
                            Col.Unique = false;

                            if (i == 0)
                            {
                                Col.ColumnName = "Rank";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                Col.ColumnName = "Rework Code";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                Col.ColumnName = "Rework Code Desc";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                Col.ColumnName = "Rework Count";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(Col);
                        }
                        
                        for (i = 0; i < RetDT.Rows.Count; i++)
                        {
                            DataRow Row = null;
                            Row = ReRetDT.NewRow();
                            Row[0] = i+1;
                            ReRetDT.Rows.Add(Row);

                            for (j = 0; j < RetDT.Columns.Count; j++)
                            {
                                ReRetDT.Rows[i][j + 1] = RetDT.Rows[i].ItemArray[j];
                            }
                        }


                        //Total 계산
                        if (RetDT.Rows.Count > 0)
                        {
                            DataRow TotRow = null;
                            int Total;

                            TotRow = ReRetDT.NewRow();
                            TotRow[0] = "Total";
                            ReRetDT.Rows.Add(TotRow);
                            for (i = 3; i < ReRetDT.Columns.Count; i++)
                            {
                                Total = 0;
                                for (j = 0; j < ReRetDT.Rows.Count - 1; j++)
                                {
                                    Total += Convert.ToInt32(ReRetDT.Rows[j].ItemArray[i]);
                                }
                                ReRetDT.Rows[ReRetDT.Rows.Count - 1][i] = Total;
                            }
                        }
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
