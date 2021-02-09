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
        public DataTable STD1602(ArrayList TempList)
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
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_QUALITY_XML_FILE);
                //
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    DynamicQuery = " AND WM.FACTORY= '" + CondList[0] + "'";
                    DynamicQuery += " AND WM.WORK_DATE BETWEEN '" + CondList[1] + "' AND '" + CondList[2] + "'";

                    DynamicQuery += " GROUP BY WM.OPER, OD.OPER_DESC ORDER BY WM.OPER";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1602", new string[] { DynamicQuery }, CondList);

                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 3; i++)
                        {
                            DataColumn Col = new DataColumn();

                            Col.ReadOnly = false;
                            Col.Unique = false;

                            if (i == 0)
                            {
                                Col.ColumnName = "Operation";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                Col.ColumnName = "Operation Desc";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                Col.ColumnName = "Loss Rate(%)";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(Col);
                        }

                        double LossQty;
                        double EndQty;
                        double LossRate;
                        
                        for (i = 0; i < RetDT.Rows.Count; i++)
                        {
                            DataRow Row = null;
                            Row = ReRetDT.NewRow();
                            Row[0] = RetDT.Rows[i].ItemArray[0];
                            ReRetDT.Rows.Add(Row);
                           
                            ReRetDT.Rows[i][1] = RetDT.Rows[i].ItemArray[1];
                            LossQty = Convert.ToDouble(RetDT.Rows[i].ItemArray[2]);
                            EndQty = Convert.ToDouble(RetDT.Rows[i].ItemArray[3]);
                            if (LossQty + EndQty > 0)
                            {
                                LossRate = LossQty / (LossQty + EndQty);
                                LossRate = Math.Round(LossRate, 2);
                                ReRetDT.Rows[i][2] = LossRate;
                            }
                            else
                            {
                                ReRetDT.Rows[i][2] = 0;
                            }
                        }

                        //Total 계산
                        if (RetDT.Rows.Count > 0)
                        {
                            DataRow TotRow = null;
                            double Total;

                            TotRow = ReRetDT.NewRow();
                            TotRow[0] = "Total";
                            ReRetDT.Rows.Add(TotRow);
                            for (i = 2; i < ReRetDT.Columns.Count; i++)
                            {
                                Total = 0;
                                for (j = 0; j < ReRetDT.Rows.Count - 1; j++)
                                {
                                    Total += Convert.ToDouble(ReRetDT.Rows[j].ItemArray[i]);
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
