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
        public DataTable STD1401(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable ReRetDT = new DataTable();
            int i;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_PRODUCTIVITY_XML_FILE);
                //
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1401", null, new string[] { CondList[0], CondList[1], CondList[2], CondList[3] });

                    //Total을 구하기 위해 New DataTable Column 정의
                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 8; i++)
                        {
                            DataColumn ProCol = new DataColumn();

                            ProCol.ReadOnly = false;
                            ProCol.Unique = false;

                            if (i == 0)
                            {
                                ProCol.ColumnName = "Start Time";
                                ProCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                ProCol.ColumnName = "End Time";
                                ProCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                ProCol.ColumnName = "Interval Time(Min)";
                                ProCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                ProCol.ColumnName = "Event ID";
                                ProCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                ProCol.ColumnName = "Up Down Flag";
                                ProCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 5)
                            {
                                ProCol.ColumnName = "Primary Status";
                                ProCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 6)
                            {
                                ProCol.ColumnName = "Lot Count";
                                ProCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 7)
                            {
                                ProCol.ColumnName = "Total Qty";
                                ProCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(ProCol);
                        }
                    }

                    string StartTime = null;
                    string EndTime = null;
                    string TmpStart = null;
                    string TmpEnd = null;

                    for (i = 0; i < RetDT.Rows.Count; i++)
                    {
                        StartTime = "";
                        EndTime = "";
                        TmpStart = "";
                        TmpEnd = "";

                        TmpStart = Convert.ToString(RetDT.Rows[i].ItemArray[1]);
                        StartTime = TmpStart.Substring(0, 4) + "-" + TmpStart.Substring(4, 2) + "-" + TmpStart.Substring(6, 2) + " ";
                        StartTime += TmpStart.Substring(8, 2) + ":" + TmpStart.Substring(10, 2) + ":" + TmpStart.Substring(12, 2);

                        DataRow Row = null;
                        Row = ReRetDT.NewRow();
                        Row[0] = ToDate(TmpStart);
                        ReRetDT.Rows.Add(Row);

                        if (i == RetDT.Rows.Count - 1)
                        {
                            TmpEnd = Convert.ToString(RetDT.Rows[i].ItemArray[1]);
                            TmpEnd = TmpEnd.Substring(0, 8) + "235959";
                            EndTime = TmpEnd.Substring(0, 4) + "-" + TmpEnd.Substring(4, 2) + "-" + TmpEnd.Substring(6, 2) + " ";
                            EndTime += TmpEnd.Substring(8, 2) + ":" + TmpEnd.Substring(10, 2) + ":" + TmpEnd.Substring(12, 2);
                            ReRetDT.Rows[i][1] = ToDate(TmpEnd);
                        }
                        else
                        {
                            TmpEnd = Convert.ToString(RetDT.Rows[i + 1].ItemArray[1]);
                            EndTime = TmpEnd.Substring(0, 4) + "-" + TmpEnd.Substring(4, 2) + "-" + TmpEnd.Substring(6, 2) + " ";
                            EndTime += TmpEnd.Substring(8, 2) + ":" + TmpEnd.Substring(10, 2) + ":" + TmpEnd.Substring(12, 2);
                            ReRetDT.Rows[i][1] = ToDate(TmpEnd);
                        }

                        DateTime DateStart = Convert.ToDateTime(StartTime);
                        DateTime DateEnd = Convert.ToDateTime(EndTime);

                        TimeSpan IntervalTime = DateEnd.Subtract(DateStart);
                        ReRetDT.Rows[i][2] = Math.Round(IntervalTime.TotalMinutes,2);
                        ReRetDT.Rows[i][3] = RetDT.Rows[i].ItemArray[2];
                        ReRetDT.Rows[i][4] = RetDT.Rows[i].ItemArray[3];
                        ReRetDT.Rows[i][5] = RetDT.Rows[i].ItemArray[4];
                        ReRetDT.Rows[i][6] = RetDT.Rows[i].ItemArray[6];
                        ReRetDT.Rows[i][7] = RetDT.Rows[i].ItemArray[7];
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
