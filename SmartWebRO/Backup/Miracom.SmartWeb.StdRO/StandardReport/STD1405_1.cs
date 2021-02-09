﻿using System;
using System.Data;

using Miracom.Query;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.RO;
using System.Collections;

namespace Miracom.SmartWeb.RO
{
    public partial class StandardFunction 
    {
        public DataTable STD1405_1(ArrayList TempList)
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
                    DynamicQuery += " AND DOWN_TRAN_TIME BETWEEN '" + CondList[1] + "' AND '" + CondList[2] + "'";

                    if (CondList[3] != null && CondList[3].TrimEnd().Length > 0)
                    {
                        DynamicQuery += "AND  DOWN_NEW_STS_1= '" + CondList[3] + "'";
                    }

                    DynamicQuery += " ORDER BY DOWN_HIST_SEQ ";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1405_1", new string[] { DynamicQuery }, CondList);

                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 33; i++)
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
                                Col.ColumnName = "Factory";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                Col.ColumnName = "Resource";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                Col.ColumnName = "Down Hist Seq";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                Col.ColumnName = "Down Event ID";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 5)
                            {
                                Col.ColumnName = "Down Tran Time";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 6)
                            {
                                Col.ColumnName = "Down Pri Sts";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 7)
                            {
                                Col.ColumnName = "Down New Sts 1";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 8)
                            {
                                Col.ColumnName = "Down New Sts 2";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 9)
                            {
                                Col.ColumnName = "Down New Sts 3";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 10)
                            {
                                Col.ColumnName = "Down New Sts 4";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 11)
                            {
                                Col.ColumnName = "Down New Sts 5";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 12)
                            {
                                Col.ColumnName = "Down New Sts 6";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 13)
                            {
                                Col.ColumnName = "Down New Sts 7";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 14)
                            {
                                Col.ColumnName = "Down New Sts 8";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 15)
                            {
                                Col.ColumnName = "Down New Sts 9";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 16)
                            {
                                Col.ColumnName = "Down New Sts 10";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 17)
                            {
                                Col.ColumnName = "Down Tran User ID";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 18)
                            {
                                Col.ColumnName = "Down Tran Comment";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 19)
                            {
                                Col.ColumnName = "Down Interval";
                                Col.DataType = System.Type.GetType("System.String");
                            }                           
                            else if (i == 20)
                            {
                                Col.ColumnName = "User ID 1";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 21)
                            {
                                Col.ColumnName = "User Time 1";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 22)
                            {
                                Col.ColumnName = "User Comment 1";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 23)
                            {
                                Col.ColumnName = "User ID 2";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 24)
                            {
                                Col.ColumnName = "User Time 2";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 25)
                            {
                                Col.ColumnName = "User Comment 2";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 26)
                            {
                                Col.ColumnName = "User ID 3";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 27)
                            {
                                Col.ColumnName = "User Time 3";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 28)
                            {
                                Col.ColumnName = "User Comment 3";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 29)
                            {
                                Col.ColumnName = "Hist Del Flag";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 30)
                            {
                                Col.ColumnName = "Hist Del Time";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 31)
                            {
                                Col.ColumnName = "Hist Del User ID";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 32)
                            {
                                Col.ColumnName = "Hist Del Comment";
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