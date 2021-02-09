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
        public DataTable STD1104(ArrayList TempList)
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
                    DynamicQuery = "WHERE WS.FACTORY=MD.FACTORY AND WS.MAT_ID=MD.MAT_ID AND WS.MAT_VER = MD.MAT_VER AND WS.FACTORY = '" + CondList[0] + "'";
                    if (CondList[2] != null && CondList[2].TrimEnd().Length > 0)
                    {
                        DynamicQuery += " AND MD.MAT_TYPE = '" + CondList[2] + "'";
                    }
                    if (CondList[4] != null && CondList[3].TrimEnd().Length > 0)
                    {
                        if (CondList[4] == "1")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_1 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "2")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_2 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "3")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_3 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "4")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_4 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "5")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_5 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "6")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_6 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "7")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_7 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "8")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_8 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "9")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_9 = '" + CondList[3] + "'";
                        }
                        else if (CondList[4] == "10")
                        {
                            DynamicQuery += " AND MD.MAT_GRP_10 = '" + CondList[3] + "'";
                        }
                    }
                    DynamicQuery += " ORDER BY WS.OPER";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1104", new string[] { DynamicQuery }, CondList);

                    if (RetDT.Rows.Count > 0)
                    {
                        //Operation가 Column이므로 새로운 DataTable에 가변적인 Column생성
                        for (i = -2; i < RetDT.Rows.Count; i++)
                        {
                            DataColumn MatCol = new DataColumn();

                            MatCol.ReadOnly = false;
                            MatCol.Unique = false;

                            if (i == -2)
                            {
                                MatCol.ColumnName = "Material";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == -1)
                            {
                                MatCol.ColumnName = "Material Ver";
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            else
                            {
                                MatCol.ColumnName = Convert.ToString(RetDT.Rows[i].ItemArray[0]).TrimEnd();
                                MatCol.DataType = System.Type.GetType("System.String");
                            }
                            ReRetDT.Columns.Add(MatCol);
                        }

                        //Operation에 대한 Material별 Detail
                        DynamicQuery = null;
                        if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                        {
                            DynamicQuery = "WHERE RS.FACTORY=MM.FACTORY AND RS.MAT_ID=MM.MAT_ID AND RS.MAT_VER = MM.MAT_VER AND RS.FACTORY= '" + CondList[0] + "'";
                            if (CondList[1] != null && CondList[1].TrimEnd().Length > 0)
                            {
                                DynamicQuery += " AND RS.LOT_TYPE = '" + CondList[1] + "'";
                            }
                            if (CondList[2] != null && CondList[2].TrimEnd().Length > 0)
                            {
                                DynamicQuery += " AND MM.MAT_TYPE= '" + CondList[2] + "'";
                            }
                            if (CondList[4] != null && CondList[3].TrimEnd().Length > 0)
                            {
                                if (CondList[4] == "1")
                                {
                                    DynamicQuery += " AND MM.MAT_GRP_1= '" + CondList[3] + "'";
                                }
                                else if (CondList[4] == "2")
                                {
                                    DynamicQuery += " AND MM.MAT_GRP_2= '" + CondList[3] + "'";
                                }
                                else if (CondList[4] == "3")
                                {
                                    DynamicQuery += " AND MM.MAT_GRP_3= '" + CondList[3] + "'";
                                }
                                else if (CondList[4] == "4")
                                {
                                    DynamicQuery += " AND MM.MAT_GRP_4= '" + CondList[3] + "'";
                                }
                                else if (CondList[4] == "5")
                                {
                                    DynamicQuery += " AND MM.MAT_GRP_5= '" + CondList[3] + "'";
                                }
                                else if (CondList[4] == "6")
                                {
                                    DynamicQuery += " AND MM.MAT_GRP_6= '" + CondList[3] + "'";
                                }
                                else if (CondList[4] == "7")
                                {
                                    DynamicQuery += " AND MM.MAT_GRP_7= '" + CondList[3] + "'";
                                }
                                else if (CondList[4] == "8")
                                {
                                    DynamicQuery += " AND MM.MAT_GRP_8= '" + CondList[3] + "'";
                                }
                                else if (CondList[4] == "9")
                                {
                                    DynamicQuery += " AND MM.MAT_GRP_9= '" + CondList[3] + "'";
                                }
                                else if (CondList[4] == "10")
                                {
                                    DynamicQuery += " AND MM.MAT_GRP_10= '" + CondList[3] + "'";
                                }
                            }
                            DynamicQuery += " GROUP BY RS.MAT_ID, RS.MAT_VER, RS.OPER ORDER BY RS.MAT_ID, RS.MAT_VER, RS.OPER";
                        }
                        DetailDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1104_1", new string[] { DynamicQuery }, CondList);

                        //Operation을 조회한 DataTable과 Operation에 대한 각 Material별 Detail DataTable을 Operation으로 매칭 시켜 Row추가
                        string NewMat = "";
                        string CurrentMat = "";
                        string NewMatVer = "";
                        string CurrentMatVer = "";
                        int iIndex = 0;
                        
                        for (i = 0; i < DetailDT.Rows.Count; i++)
                        {
                            NewMat = Convert.ToString(DetailDT.Rows[i].ItemArray[1]);
                            NewMatVer = Convert.ToString(DetailDT.Rows[i].ItemArray[2]);
                            if (i == 0 || (CurrentMat != NewMat) || (CurrentMat == NewMat && CurrentMatVer != NewMatVer))
                            {
                                DataRow Row = null;
                                Row = ReRetDT.NewRow();
                                Row[0] = DetailDT.Rows[i].ItemArray[1];
                                ReRetDT.Rows.Add(Row);                                
                                iIndex = ReRetDT.Rows.Count - 1;
                                ReRetDT.Rows[iIndex][1] = DetailDT.Rows[i].ItemArray[2];
                                CurrentMat = NewMat;
                                CurrentMatVer = NewMatVer;
                            }
                            for (j = 1; j < ReRetDT.Columns.Count; j++)
                            {
                                //ReRetDT.Rows[iIndex][j] = "0";
                                if (Convert.ToString(DetailDT.Rows[i].ItemArray[0]) == Convert.ToString(ReRetDT.Columns[j].ColumnName))
                                {
                                    if (CondList[5] == "0")
                                    {
                                        ReRetDT.Rows[iIndex][j] = (Convert.ToString(DetailDT.Rows[i].ItemArray[2]).TrimEnd() == "" ? "0" : DetailDT.Rows[i].ItemArray[3]);
                                    }
                                    else if (CondList[5] == "1")
                                    {
                                        ReRetDT.Rows[iIndex][j] = (Convert.ToString(DetailDT.Rows[i].ItemArray[3]).TrimEnd() == "" ? "0" : DetailDT.Rows[i].ItemArray[4]);
                                    }
                                    else if (CondList[5] == "2")
                                    {
                                        ReRetDT.Rows[iIndex][j] = (Convert.ToString(DetailDT.Rows[i].ItemArray[4]).TrimEnd() == "" ? "0" : DetailDT.Rows[i].ItemArray[5]);
                                    }
                                }
                                if (ReRetDT.Rows[iIndex][j] == null || Convert.ToString(ReRetDT.Rows[iIndex][j]) == "")
                                {
                                    ReRetDT.Rows[iIndex][j] = "0";
                                }
                            }                            
                        }

                        //Total 계산
                        if (ReRetDT.Rows.Count > 0)
                        {
                            DataRow TotRow = null;
                            int Total;

                            TotRow = ReRetDT.NewRow();
                            TotRow[0] = "Total";
                            ReRetDT.Rows.Add(TotRow);
                            for (i = 2; i < ReRetDT.Columns.Count; i++)
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
