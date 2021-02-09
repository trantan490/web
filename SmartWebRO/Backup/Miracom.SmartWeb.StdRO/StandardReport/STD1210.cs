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
        public DataTable STD1210(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable ResultDT = null;
            DataTable ReRetDT = new DataTable();
            string DynamicQuery = null;
            string DynamicField1 = null;
            string DynamicField2 = null;
            int i;
            int j;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_COMMON_XML_FILE);
                //
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    DynamicQuery = "SELECT REPLACE(OPER, '-', '') AS OPER FROM MWIPOPRDEF WHERE FACTORY = '" + CondList[0] + "'";
                    
                    if (CondList[5] != null && CondList[4].TrimEnd().Length > 0)
                    {
                        if (CondList[5] == "1")
                        {
                            DynamicQuery += " AND OPER_GRP_1 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "2")
                        {
                            DynamicQuery += " AND OPER_GRP_2 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "3")
                        {
                            DynamicQuery += " AND OPER_GRP_3 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "4")
                        {
                            DynamicQuery += " AND OPER_GRP_4 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "5")
                        {
                            DynamicQuery += " AND OPER_GRP_5 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "6")
                        {
                            DynamicQuery += " AND OPER_GRP_6 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "7")
                        {
                            DynamicQuery += " AND OPER_GRP_7 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "8")
                        {
                            DynamicQuery += " AND OPER_GRP_8 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "9")
                        {
                            DynamicQuery += " AND OPER_GRP_9 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "10")
                        {
                            DynamicQuery += " AND OPER_GRP_10 = '" + CondList[4] + "'";
                        }
                    }
                                        
                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("DYNAMIC", new string[] { DynamicQuery }, CondList);
                                        
                    for (i = 0; i < RetDT.Rows.Count; i++)
                    {
                         DynamicField2 += " ,NVL(SUM(AA.OP" + RetDT.Rows[i].ItemArray[0] + "),0) AS \"" + RetDT.Rows[i].ItemArray[0] + "\" ";
                        if (CondList[6] == "0")
                        {
                            DynamicField1 += ", DECODE(DD.OPER, '";
                            DynamicField1 += RetDT.Rows[i].ItemArray[0] + "' , (DD.S1_MOVE_LOT+DD.S2_MOVE_LOT+DD.S3_MOVE_LOT+DD.S4_MOVE_LOT)) AS OP" + RetDT.Rows[i].ItemArray[0] + " ";
                        }
                        else if (CondList[6] == "1")
                        {
                            DynamicField1 += ", DECODE(DD.OPER, '";
                            DynamicField1 += RetDT.Rows[i].ItemArray[0] + "' , (DD.S1_MOVE_QTY_1+DD.S2_MOVE_QTY_1+DD.S3_MOVE_QTY_1+DD.S4_MOVE_QTY_1)) AS OP" + RetDT.Rows[i].ItemArray[0] + " ";
                        }
                        else if (CondList[6] == "2")
                        {
                            DynamicField1 += ", DECODE(DD.OPER, '";
                            DynamicField1 += RetDT.Rows[i].ItemArray[0] + "' , (DD.S1_MOVE_QTY_2+DD.S2_MOVE_QTY_2+DD.S3_MOVE_QTY_2+DD.S4_MOVE_QTY_2)) AS OP" + RetDT.Rows[i].ItemArray[0] + " ";
                        }
                    }

                    StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_PRODUCTION_OUTPUT_XML_FILE);

                    DynamicQuery = "";
                    DynamicQuery = "SELECT  AA.WORK_DATE" + DynamicField2 + "  FROM (SELECT DD.WORK_DATE" + DynamicField1;
                    DynamicQuery += " FROM RSUMWIPMOV DD, MWIPOPRDEF OD WHERE DD.OPER = OD.OPER AND DD.FACTORY = OD.FACTORY ";
                    DynamicQuery += " AND DD.FACTORY= '" + CondList[0] + "' AND DD.WORK_DATE BETWEEN '" + CondList[1] + "' AND '" + CondList[2] + "' ";

                    if (CondList[3] != null && CondList[3].TrimEnd().Length > 0 && CondList[7] != null && CondList[7].TrimEnd().Length > 0)
                    {
                        DynamicQuery += "AND DD.MAT_ID= '" + CondList[3] + "' AND DD.MAT_VER= '" + CondList[7] + "'";
                    }

                    if (CondList[5] != null && CondList[4].TrimEnd().Length > 0)
                    {
                        if (CondList[5] == "1")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_1 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "2")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_2 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "3")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_3 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "4")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_4 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "5")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_5 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "6")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_6 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "7")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_7 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "8")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_8 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "9")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_9 = '" + CondList[4] + "'";
                        }
                        else if (CondList[5] == "10")
                        {
                            DynamicQuery += " AND OD.OPER_GRP_10 = '" + CondList[4] + "'";
                        }
                    }

                    DynamicQuery += " ) AA GROUP BY AA.WORK_DATE ORDER BY AA.WORK_DATE ";

                    ResultDT = StdGlobalVariable.DBQuery.GetFuncDataTable("DYNAMIC", new string[] { DynamicQuery }, CondList);

                    if (ResultDT.Rows.Count > 0)
                    {
                        //가변적 Column생성
                        for (i = 0; i < ResultDT.Columns.Count; i++)
                        {
                            DataColumn OperCol = new DataColumn();

                            OperCol.ReadOnly = false;
                            OperCol.Unique = false;
                            
                            OperCol.ColumnName = Convert.ToString(ResultDT.Columns[i].ColumnName).TrimEnd();
                            OperCol.DataType = System.Type.GetType("System.String");
                            
                            ReRetDT.Columns.Add(OperCol);
                        }

                        //Total을 구하기 위해 New DataTable Value 정의
                        for (i = 0; i < ResultDT.Rows.Count; i++)
                        {
                            string MOMT_Date = null;
                            DataRow MOMTRow = null;
                            MOMTRow = ReRetDT.NewRow();
                            MOMT_Date = Convert.ToString(ResultDT.Rows[i].ItemArray[0]);
                            MOMTRow[0] = ToDate(MOMT_Date);
                            ReRetDT.Rows.Add(MOMTRow);
                            for (j = 1; j < ResultDT.Columns.Count; j++)
                            {
                                ReRetDT.Rows[i][j] = ResultDT.Rows[i].ItemArray[j];
                            }
                        }
                    }

                    //Total 계산
                    if (ResultDT.Rows.Count > 0)
                    {
                        DataRow TotRow = null;
                        int Total;

                        TotRow = ReRetDT.NewRow();
                        TotRow[0] = "Total";
                        ReRetDT.Rows.Add(TotRow);
                        for (i = 1; i < ReRetDT.Columns.Count; i++)
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
