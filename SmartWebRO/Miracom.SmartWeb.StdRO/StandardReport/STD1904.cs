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
        public DataTable STD1904(ArrayList TempList)
        {

            DataTable RetDT = null;
            DataTable ReRetDT = new DataTable();
            string DynamicQuery = null;
            int i;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_TRACEABILITY_XML_FILE);
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0 && CondList[1] != null && CondList[1].TrimEnd().Length > 0)
                {
                    DynamicQuery = " WHERE FACTORY= '" + CondList[0] + "'" + "AND RES_ID= '" + CondList[1] + "'";
                    DynamicQuery += " AND TRAN_TIME BETWEEN '" + CondList[2] + "'" + "AND '" + CondList[3] + "'";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1904", new string[] { DynamicQuery }, CondList);

                    if (RetDT.Rows.Count > 0)
                    {
                        for (i = 0; i < 37; i++)
                        {
                            DataColumn Col = new DataColumn();

                            Col.ReadOnly = false;
                            Col.Unique = false;

                            if (i == 0)
                            {
                                Col.ColumnName = "Hist Seq";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 1)
                            {
                                Col.ColumnName = "Event ID";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 2)
                            {
                                Col.ColumnName = "Tran Time";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 3)
                            {
                                Col.ColumnName = "New Up Down Flag";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 4)
                            {
                                Col.ColumnName = "New Pri Status";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 5)
                            {
                                Col.ColumnName = "New Status 1";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 6)
                            {
                                Col.ColumnName = "New Status 2";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 7)
                            {
                                Col.ColumnName = "New Status 3";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 8)
                            {
                                Col.ColumnName = "New Status 4";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 9)
                            {
                                Col.ColumnName = "New Status 5";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 10)
                            {
                                Col.ColumnName = "New Status 6";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 11)
                            {
                                Col.ColumnName = "New Status 7";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 12)
                            {
                                Col.ColumnName = "New Status 8";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 13)
                            {
                                Col.ColumnName = "New Status 9";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 14)
                            {
                                Col.ColumnName = "New Status 10";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 15)
                            {
                                Col.ColumnName = "Lot Exist Flag";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 16)
                            {
                                Col.ColumnName = "Col Set ID";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 17)
                            {
                                Col.ColumnName = "Col Set Ver";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 18)
                            {
                                Col.ColumnName = "Tran CMF1";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 19)
                            {
                                Col.ColumnName = "Tran CMF2";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 20)
                            {
                                Col.ColumnName = "Tran CMF3";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 21)
                            {
                                Col.ColumnName = "Tran CMF4";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 22)
                            {
                                Col.ColumnName = "Tran CMF5";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 23)
                            {
                                Col.ColumnName = "Tran CMF6";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 24)
                            {
                                Col.ColumnName = "Tran CMF7";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 25)
                            {
                                Col.ColumnName = "Tran CMF8";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 26)
                            {
                                Col.ColumnName = "Tran CMF9";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 27)
                            {
                                Col.ColumnName = "Tran CMF10";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 28)
                            {
                                Col.ColumnName = "Tran User ID";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 29)
                            {
                                Col.ColumnName = "Tran Comment";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 30)
                            {
                                Col.ColumnName = "Last Down Time";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 31)
                            {
                                Col.ColumnName = "Last Down Hist Seq";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 32)
                            {
                                Col.ColumnName = "Hist Start Seq";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 33)
                            {
                                Col.ColumnName = "Hist Del Flag";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 34)
                            {
                                Col.ColumnName = "Hist Del Time";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 35)
                            {
                                Col.ColumnName = "Hist Del User ID";
                                Col.DataType = System.Type.GetType("System.String");
                            }
                            else if (i == 36)
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
                            Row[0] = Convert.ToString(RetDT.Rows[i].ItemArray[2]); // HIST_SEQ
                            ReRetDT.Rows.Add(Row);

                            ReRetDT.Rows[i][1] = RetDT.Rows[i].ItemArray[3]; // EVENT_ID
                            ReRetDT.Rows[i][2] = ToDate(RetDT.Rows[i].ItemArray[4].ToString()); // TRAN_TIME
                            ReRetDT.Rows[i][3] = RetDT.Rows[i].ItemArray[22]; // NEW_UP_DOWN_FLAG
                            ReRetDT.Rows[i][4] = RetDT.Rows[i].ItemArray[23]; // NEW_PRI_STS
                            ReRetDT.Rows[i][5] = RetDT.Rows[i].ItemArray[24]; // NEW_STS_1
                            ReRetDT.Rows[i][6] = RetDT.Rows[i].ItemArray[25]; // NEW_STS_2
                            ReRetDT.Rows[i][7] = RetDT.Rows[i].ItemArray[26]; // NEW_STS_3
                            ReRetDT.Rows[i][8] = RetDT.Rows[i].ItemArray[27]; // NEW_STS_4
                            ReRetDT.Rows[i][9] = RetDT.Rows[i].ItemArray[28]; // NEW_STS_5
                            ReRetDT.Rows[i][10] = RetDT.Rows[i].ItemArray[29]; // NEW_STS_6
                            ReRetDT.Rows[i][11] = RetDT.Rows[i].ItemArray[30]; // NEW_STS_7
                            ReRetDT.Rows[i][12] = RetDT.Rows[i].ItemArray[31]; // NEW_STS_8
                            ReRetDT.Rows[i][13] = RetDT.Rows[i].ItemArray[32]; // NEW_STS_9
                            ReRetDT.Rows[i][14] = RetDT.Rows[i].ItemArray[33]; // NEW_STS_10
                            ReRetDT.Rows[i][15] = RetDT.Rows[i].ItemArray[39]; // LOT_EXIST_FLAG 
                            ReRetDT.Rows[i][16] = RetDT.Rows[i].ItemArray[40]; // COL_SET_ID
                            ReRetDT.Rows[i][17] = RetDT.Rows[i].ItemArray[41]; // COL_SET_VERSION
                            ReRetDT.Rows[i][18] = RetDT.Rows[i].ItemArray[42]; // TRAN_CMF_1
                            ReRetDT.Rows[i][19] = RetDT.Rows[i].ItemArray[43]; // TRAN_CMF_2
                            ReRetDT.Rows[i][20] = RetDT.Rows[i].ItemArray[44]; // TRAN_CMF_3
                            ReRetDT.Rows[i][21] = RetDT.Rows[i].ItemArray[45]; // TRAN_CMF_4
                            ReRetDT.Rows[i][22] = RetDT.Rows[i].ItemArray[46]; // TRAN_CMF_5
                            ReRetDT.Rows[i][23] = RetDT.Rows[i].ItemArray[47]; // TRAN_CMF_6
                            ReRetDT.Rows[i][24] = RetDT.Rows[i].ItemArray[48]; // TRAN_CMF_7
                            ReRetDT.Rows[i][25] = RetDT.Rows[i].ItemArray[49]; // TRAN_CMF_8
                            ReRetDT.Rows[i][26] = RetDT.Rows[i].ItemArray[50]; // TRAN_CMF_9
                            ReRetDT.Rows[i][27] = RetDT.Rows[i].ItemArray[51]; // TRAN_CMF_10
                            ReRetDT.Rows[i][28] = RetDT.Rows[i].ItemArray[52]; // TRAN_USER_ID
                            ReRetDT.Rows[i][29] = RetDT.Rows[i].ItemArray[53]; // TRAN_COMMENT
                            ReRetDT.Rows[i][30] = ToDate(RetDT.Rows[i].ItemArray[54].ToString()); // LAST_DOWN_TIME
                            ReRetDT.Rows[i][31] = RetDT.Rows[i].ItemArray[55]; // LAST_DOWN_HIST_SEQ
                            ReRetDT.Rows[i][32] = RetDT.Rows[i].ItemArray[56]; // HIST_START_SEQ
                            ReRetDT.Rows[i][33] = RetDT.Rows[i].ItemArray[57]; // HIST_DEL_FLAG
                            ReRetDT.Rows[i][34] = ToDate(RetDT.Rows[i].ItemArray[58].ToString()); // HIST_DEL_TIME
                            ReRetDT.Rows[i][35] = RetDT.Rows[i].ItemArray[59]; // HIST_DEL_USER_ID
                            ReRetDT.Rows[i][36] = RetDT.Rows[i].ItemArray[60]; // HIST_DEL_COMMENT
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
