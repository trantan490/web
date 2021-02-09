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
        public DataTable STD1404(ArrayList TempList)
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
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_PRODUCTIVITY_XML_FILE);
                //
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    DynamicQuery = " WHERE FACTORY= '" + CondList[0] + "'";
                    DynamicQuery += " AND DOWN_TRAN_TIME BETWEEN '" + CondList[2] + "' AND '" + CondList[3] + "'";

                    if (CondList[1] != null && CondList[1].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND RES_ID= '" + CondList[1] + "'";
                    }

                    DynamicQuery += " ORDER BY RES_ID, DOWN_HIST_SEQ";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1404", new string[] { DynamicQuery }, CondList);

                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 48; i++)
                        {
                            DataColumn Col = new DataColumn();

                            Col.ReadOnly = false;
                            Col.Unique = false;

                            if (i == 0)
                            {
                                Col.ColumnName = "Factory";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                Col.ColumnName = "Resource";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                Col.ColumnName = "Down Hist Seq";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                Col.ColumnName = "Down Event ID";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                Col.ColumnName = "Down Tran Time";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 5)
                            {
                                Col.ColumnName = "Down Primary Status";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 6)
                            {
                                Col.ColumnName = "Down New Status 1";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 7)
                            {
                                Col.ColumnName = "Down New Status 2";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 8)
                            {
                                Col.ColumnName = "Down New Status 3";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 9)
                            {
                                Col.ColumnName = "Down New Status 4";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 10)
                            {
                                Col.ColumnName = "Down New Status 5";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 11)
                            {
                                Col.ColumnName = "Down New Status 6";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 12)
                            {
                                Col.ColumnName = "Down New Status 7";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 13)
                            {
                                Col.ColumnName = "Down New Status 8";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 14)
                            {
                                Col.ColumnName = "Down New Status 9";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 15)
                            {
                                Col.ColumnName = "Down New Status 10";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 16)
                            {
                                Col.ColumnName = "Down Tran User ID";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 17)
                            {
                                Col.ColumnName = "Down Tran Comment";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 18)
                            {
                                Col.ColumnName = "Down Interval(second)";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 19)
                            {
                                Col.ColumnName = "Up Hist Seq";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 20)
                            {
                                Col.ColumnName = "Up Event ID";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 21)
                            {
                                Col.ColumnName = "Up Tran Time";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 22)
                            {
                                Col.ColumnName = "Up Primary Status";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 23)
                            {
                                Col.ColumnName = "Up New Status 1";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 24)
                            {
                                Col.ColumnName = "Up New Status 2";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 25)
                            {
                                Col.ColumnName = "Up New Status 3";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 26)
                            {
                                Col.ColumnName = "Up New Status 4";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 27)
                            {
                                Col.ColumnName = "Up New Status 5";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 28)
                            {
                                Col.ColumnName = "Up New Status 6";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 29)
                            {
                                Col.ColumnName = "Up New Status 7";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 30)
                            {
                                Col.ColumnName = "Up New Status 8";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 31)
                            {
                                Col.ColumnName = "Up New Status 9";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 32)
                            {
                                Col.ColumnName = "Up New Status 10";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 33)
                            {
                                Col.ColumnName = "Up Tran User ID";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 34)
                            {
                                Col.ColumnName = "Up Tran Comment";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 35)
                            {
                                Col.ColumnName = "User ID 1";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 36)
                            {
                                Col.ColumnName = "User Time 1";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 37)
                            {
                                Col.ColumnName = "User Comment 1";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 38)
                            {
                                Col.ColumnName = "User ID 2";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 39)
                            {
                                Col.ColumnName = "User Time 2";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 40)
                            {
                                Col.ColumnName = "User Comment 2";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 41)
                            {
                                Col.ColumnName = "User ID 3";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 42)
                            {
                                Col.ColumnName = "User Time 3";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 43)
                            {
                                Col.ColumnName = "User Comment 3";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 44)
                            {
                                Col.ColumnName = "Hist Del Flag";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 45)
                            {
                                Col.ColumnName = "Hist Del Time";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 46)
                            {
                                Col.ColumnName = "Hist Del User ID";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 47)
                            {
                                Col.ColumnName = "Hist Del Comment";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(Col);
                        }

                        for (i = 0; i < RetDT.Rows.Count; i++)
                        {
                            DataRow PlanRow = null;
                            PlanRow = ReRetDT.NewRow();
                            PlanRow[0] = Convert.ToString(RetDT.Rows[i].ItemArray[0]);
                            ReRetDT.Rows.Add(PlanRow);
                            for (j = 1; j < RetDT.Columns.Count; j++)
                            {
                                if (RetDT.Columns[j].ColumnName.IndexOf("TIME") > 0)
                                {
                                    if (RetDT.Rows[i].ItemArray[j].ToString().Trim() != "")
                                    {
                                        ReRetDT.Rows[i][j] = ToDate(RetDT.Rows[i].ItemArray[j].ToString().Trim());
                                    }
                                    else
                                    {
                                        ReRetDT.Rows[i][j] = RetDT.Rows[i].ItemArray[j];
                                    }
                                }
                                else
                                {
                                    ReRetDT.Rows[i][j] = RetDT.Rows[i].ItemArray[j];
                                }
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
