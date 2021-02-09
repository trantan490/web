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
        public DataTable STD1103(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable DetailDT = new DataTable();
            DataTable ReRetDT = new DataTable();
            string DynamicQuery = null;
            int i;
            int j;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_WIP_STATUS_XML_FILE);
                //Material ID 조회
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    DynamicQuery = "WHERE WS.FACTORY=MD.FACTORY AND WS.MAT_ID=MD.MAT_ID AND WS.MAT_VER = MD.MAT_VER AND WS.FACTORY= '" + CondList[0] + "'";
                    if (CondList[2] != null && CondList[2].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND MD.MAT_TYPE = '" + CondList[2] + "'";
                    }
                    if (CondList[4] != null && CondList[3].TrimEnd().Length > 0)
                    {
                        if (CondList[4] == "1")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_1= '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "2")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_2= '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "3")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_3= '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "4")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_4= '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "5")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_5= '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "6")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_6= '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "7")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_7= '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "8")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_8= '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "9")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_9= '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "10")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_10= '" + CondList[3] + "'";
                        }
                    }
                    DynamicQuery += " ORDER BY WS.MAT_ID, WS.MAT_VER, WS.MAT_VER)";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1103", new string[] { DynamicQuery }, CondList);

                    if (RetDT.Rows.Count > 0)
                    {
                        //int[] Total = new int[RetDT.Rows.Count];
                        //Material ID가 Column이므로 새로운 DataTable에 가변적인 Column생성
                        for (i = -1; i < RetDT.Rows.Count; i++)
                        {
                            DataColumn MatCol = new DataColumn();

                            MatCol.ReadOnly = false;
                            MatCol.Unique = false;

                            if (i == -1)
                            {
                                MatCol.ColumnName = "Operation";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else
                            {
                                MatCol.ColumnName = Convert.ToString(RetDT.Rows[i].ItemArray[0]).TrimEnd();
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(MatCol);
                        }

                        //Material ID에 대한 각 공정별 Detail
                        DynamicQuery = null;
                        if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                        {
                            DynamicQuery = "WHERE FACTORY= '" + CondList[0] + "'";
                            if (CondList[1] != null && CondList[1].TrimEnd().Length > 0)
                            {
                                DynamicQuery += " AND LOT_TYPE = '" + CondList[1] + "'";
                            }
                            DynamicQuery += " GROUP BY OPER, MAT_ID, MAT_VER ORDER BY OPER, MAT_ID, MAT_VER)";
                        }
                        DetailDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1103_1", new string[] { DynamicQuery }, CondList);

                        //Material ID조회한 DataTable과 Material ID에 대한 각 공정별 Detail DataTable을 Material ID로 매칭 시켜 Row추가
                        string NewOper = "";
                        string CurrentOper = "";
                        int iIndex = 0;

                        for (i = 0; i < DetailDT.Rows.Count; i++)
                        {
                            NewOper = Convert.ToString(DetailDT.Rows[i].ItemArray[0]);
                            if (i == 0 || CurrentOper != NewOper)
                            {
                                DataRow OperRow = null;
                                OperRow = ReRetDT.NewRow();
                                OperRow[0] = DetailDT.Rows[i].ItemArray[0];
                                ReRetDT.Rows.Add(OperRow);
                                iIndex = ReRetDT.Rows.Count - 1;
                                CurrentOper = NewOper;
                            }
                            for (j = 1; j < ReRetDT.Columns.Count; j++)
                            {
                                ReRetDT.Rows[iIndex][j] = "0";
                                if (Convert.ToString(DetailDT.Rows[i].ItemArray[1]) == Convert.ToString(ReRetDT.Columns[j].ColumnName))
                                {
                                    if (CondList[5] == "0")
                                    {
                                        ReRetDT.Rows[iIndex][j] = (Convert.ToString(DetailDT.Rows[i].ItemArray[2]).TrimEnd() == "" ? "0" : DetailDT.Rows[i].ItemArray[2]);
                                    }
                                    else if (CondList[5] == "1")
                                    {
                                        ReRetDT.Rows[iIndex][j] = (Convert.ToString(DetailDT.Rows[i].ItemArray[3]).TrimEnd() == "" ? "0" : DetailDT.Rows[i].ItemArray[3]);
                                    }
                                    else if (CondList[5] == "2")
                                    {
                                        ReRetDT.Rows[iIndex][j] = (Convert.ToString(DetailDT.Rows[i].ItemArray[4]).TrimEnd() == "" ? "0" : DetailDT.Rows[i].ItemArray[4]);
                                    }
                                }
                            }
                        }

                        //Total 계산
                        if (DetailDT.Rows.Count > 0)
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

        //public DataTable STD1103_1(DataTable ReRetDT,  string[] CondList)
        //{
        //    string DynamicQuery = null;
        //    int i = 0;

        //    try
        //    {
        //        SvrGlobalVariable.DBQuery.InitQueryComponent(SvrGlobalVariable.QueryPath + @"\" + CUSSvrGlobalVariable.MP_WIP_STATUS_XML_FILE);
        //        if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
        //        {
        //            DynamicQuery = "WHERE FACTORY= '" + CondList[0] + "'";
        //            if (CondList[1] != null && CondList[1].TrimEnd().Length > 0)
        //            {
        //                DynamicQuery += " AND LOT_TYPE = '" + CondList[1] + "'";
        //            }

        //            DynamicQuery += " GROUP BY OPER, MAT_ID ORDER BY OPER, MAT_ID";
        //        }

        //        ReRetDT = SvrGlobalVariable.DBQuery.GetFuncDataTable("STD1103_1", new string[] { DynamicQuery }, CondList);
                                
        //        return ReRetDT;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (ReRetDT != null) ReRetDT.Dispose();
        //    }
        //}

    }
}
