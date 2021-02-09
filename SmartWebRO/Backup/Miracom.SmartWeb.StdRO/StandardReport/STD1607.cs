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
        public DataTable STD1607(ArrayList TempList)
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
                    DynamicQuery = " AND LT.FACTORY= '" + CondList[0] + "'";
                    if (CondList[1] != null && CondList[1].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND LT.MAT_ID= '" + CondList[1] + "'";
                    }
                    if (CondList[2] != null && CondList[2].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND LT.MAT_VER= '" + CondList[2] + "'";
                    }
                    if (CondList[3] != null && CondList[3].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND LT.OPER= '" + CondList[3] + "'";
                    }

                    if (CondList[5] != null && CondList[4].TrimEnd().Length > 0)
                    {
                        if (CondList[5] == "1")
                        {
                            DynamicQuery += " AND MM.MAT_GRP_1= '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "2")
                        {
                            DynamicQuery += " AND MM.MAT_GRP_2= '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "3")
                        {
                            DynamicQuery += " AND MM.MAT_GRP_3= '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "4")
                        {
                            DynamicQuery += " AND MM.MAT_GRP_4= '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "5")
                        {
                            DynamicQuery += " AND MM.MAT_GRP_5= '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "6")
                        {
                            DynamicQuery += " AND MM.MAT_GRP_6= '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "7")
                        {
                            DynamicQuery += " AND MM.MAT_GRP_7= '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "8")
                        {
                            DynamicQuery += " AND MM.MAT_GRP_8= '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "9")
                        {
                            DynamicQuery += " AND MM.MAT_GRP_9= '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "10")
                        {
                            DynamicQuery += " AND MM.MAT_GRP_10= '" + CondList[4] + "'";
                        }
                    }
                    if (CondList[6] != null && CondList[6].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND CT.RATE > " + CondList[6] ;
                    }

                    DynamicQuery = " ORDER BY LT.LOT_ID  ";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1607", new string[] { DynamicQuery }, CondList);

                    //for (i = 0; i < RetDT.Columns.Count; i++)
                    //{
                    //    DataColumn DetailCol = new DataColumn();

                    //    DetailCol.ReadOnly = false;
                    //    DetailCol.Unique = false;

                    //    DetailCol.ColumnName = RetDT.Columns[i].ColumnName;
                    //    DetailCol.DataType = System.Type.GetType("System.String");

                    //    ReRetDT.Columns.Add(DetailCol);
                    //}

                    ////New DataTable Value 정의
                    //for (i = 0; i < RetDT.Rows.Count; i++)
                    //{
                    //    DataRow OperRow = null;
                    //    OperRow = ReRetDT.NewRow();
                    //    OperRow[0] = RetDT.Rows[i].ItemArray[0];
                    //    ReRetDT.Rows.Add(OperRow);
                    //    for (j = 1; j < RetDT.Columns.Count; j++)
                    //    {
                    //        if (RetDT.Columns[j].ColumnName.IndexOf("TIME") > 0)
                    //        {
                    //            if (RetDT.Rows[i].ItemArray[j].ToString().Trim() != "" && j != 9)
                    //            {
                    //                ReRetDT.Rows[i][j] = CmnComFunction.ToDate(RetDT.Rows[i].ItemArray[j].ToString().Trim());
                    //            }
                    //            else
                    //            {
                    //                ReRetDT.Rows[i][j] = RetDT.Rows[i].ItemArray[j];
                    //            }

                    //        }
                    //        else
                    //        {
                    //            if (j > 6 && j < 10)
                    //            {
                    //                ReRetDT.Rows[i][j] = Convert.ToDouble(RetDT.Rows[i].ItemArray[j]).ToString("#########0.000");
                    //            }
                    //            else
                    //            {
                    //                ReRetDT.Rows[i][j] = RetDT.Rows[i].ItemArray[j];
                    //            }
                    //        }
                    //    }

                    //}                    

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
