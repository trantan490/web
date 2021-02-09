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
        public DataTable STD1501(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable ReRetDT = new DataTable();
            string DynamicQuery = null;
            int i;
            int j;
            int k;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_RESOURCE_ANALYSIS_XML_FILE);
                //
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    DynamicQuery = " AND RM.FACTORY= '" + CondList[0] + "'";
                    DynamicQuery += " AND RM.WORK_DATE BETWEEN '" + CondList[2] + "' AND '" + CondList[3] + "'";

                    if (CondList[1] != null && CondList[1].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND RM.RES_ID= '" + CondList[1] + "'";
                    }
                    if (CondList[4] != null && CondList[4].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND RM.OPER= '" + CondList[4] + "'";
                    }
                    
                    DynamicQuery += " GROUP BY RM.RES_ID, RD.RES_DESC, RM.OPER ORDER BY RM.RES_ID, RM.OPER";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1501", new string[] { DynamicQuery }, CondList);

                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 6; i++)
                        {
                            DataColumn Col = new DataColumn();

                            Col.ReadOnly = false;
                            Col.Unique = false;

                            if (i == 0)
                            {
                                Col.ColumnName = "Resource";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                Col.ColumnName = "Resource Desc";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                Col.ColumnName = "Operation";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                Col.ColumnName = "End Lot";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                Col.ColumnName = "End Qty1";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 5)
                            {
                                Col.ColumnName = "End Qty2";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(Col);
                        }

                        //Sub Total 계산
                        int iIndex;
                        int subTotal;
                        string CurrentGroup = "";
                        string NewGroup;

                        iIndex = 0;
                        for (i = 0; i < RetDT.Rows.Count; i++)
                        {
                            NewGroup = Convert.ToString(RetDT.Rows[i].ItemArray[0]);
                            if (i == 0 || CurrentGroup != NewGroup)
                            {
                                CurrentGroup = NewGroup;
                                if (i != 0)
                                {
                                    DataRow SubTotRow = null;
                                    SubTotRow = ReRetDT.NewRow();
                                    SubTotRow[0] = "Sub Total";
                                    ReRetDT.Rows.Add(SubTotRow);

                                    for (j = 3; j < ReRetDT.Columns.Count; j++)
                                    {
                                        subTotal = 0;
                                        for (k = iIndex; k < ReRetDT.Rows.Count - 1; k++)
                                        {
                                            subTotal += Convert.ToInt32(ReRetDT.Rows[k].ItemArray[j]);
                                        }
                                        ReRetDT.Rows[ReRetDT.Rows.Count - 1][j] = subTotal;
                                    }
                                    iIndex = ReRetDT.Rows.Count;
                                }
                            }
                            DataRow MGRow = null;
                            MGRow = ReRetDT.NewRow();
                            MGRow[0] = RetDT.Rows[i].ItemArray[0];
                            ReRetDT.Rows.Add(MGRow);

                            for (j = 1; j < RetDT.Columns.Count; j++)
                            {
                                ReRetDT.Rows[ReRetDT.Rows.Count - 1][j] = RetDT.Rows[i].ItemArray[j];
                            }

                            if (i == RetDT.Rows.Count - 1)
                            {
                                DataRow SubTotRow = null;
                                SubTotRow = ReRetDT.NewRow();
                                SubTotRow[0] = "Sub Total";
                                ReRetDT.Rows.Add(SubTotRow);

                                for (j = 3; j < ReRetDT.Columns.Count; j++)
                                {
                                    subTotal = 0;
                                    for (k = iIndex; k < ReRetDT.Rows.Count - 1; k++)
                                    {
                                        subTotal += Convert.ToInt32(ReRetDT.Rows[k].ItemArray[j]);
                                    }
                                    ReRetDT.Rows[ReRetDT.Rows.Count - 1][j] = subTotal;
                                }
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
                                    if (Convert.ToString(ReRetDT.Rows[j].ItemArray[0]) != "Sub Total")
                                    {
                                        Total += Convert.ToInt32(ReRetDT.Rows[j].ItemArray[i]);
                                    }
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
