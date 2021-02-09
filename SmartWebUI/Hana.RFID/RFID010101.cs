using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI.Controls;

namespace Hana.PRD
{
    public partial class RFID010101 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /****************************************************
         * comment : RFID처리 현황에 대한 내역을 조회한다.
         * 
         * created by : bee-jae jung(2012-12-17-월요일)
         * 
         * modified by : bee-jae jung(2012-12-17-월요일)
         ****************************************************/


        #region " RFID010101 : Program Initial "

        public RFID010101()
        {
            InitializeComponent();
        }

        #endregion


        #region " Common Function "

        private void fnSSInitial(Miracom.SmartWeb.UI.Controls.udcFarPoint SS)
        {
            /****************************************************
             * comment : ss의 header를 설정한다.
             * 
             * created by : bee-jae jung(2012-12-17-월요일)
             * 
             * modified by : bee-jae jung(2012-12-17-월요일)
             ****************************************************/
            int iindex = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                SS.RPT_ColumnInit();
                // CUSTOMER
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                {
                    iindex = 0;
                    SS.RPT_AddBasicColumn("CUSTOMER", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // FAMILY
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("FAMILY", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // PACKAGE
                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("PACKAGE", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // TYPE1
                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("TYPE1", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // TYPE2
                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("TYPE2", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // LEAD COUNT
                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("LEAD_COUNT", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // DENSITY
                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("DENSITY", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // GENERATION
                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("GENERATION", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // 2012-12-17-정비재 : sheet의 header를 설정한다.
                SS.RPT_AddBasicColumn("RES_ID", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                SS.RPT_AddBasicColumn("LOT_ID", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                SS.RPT_AddBasicColumn("OPER", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                SS.RPT_AddBasicColumn("RFID_START_TIME", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                SS.RPT_AddBasicColumn("MES_START_TIME", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                SS.RPT_AddBasicColumn("START_TIME_DIFF", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                SS.RPT_AddBasicColumn("RFID_START_FLAG", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                SS.RPT_AddBasicColumn("MES_START_FLAG", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                SS.RPT_AddBasicColumn("RFID_MES_START_COMPARE", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                SS.RPT_AddBasicColumn("RFID_END_TIME", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                SS.RPT_AddBasicColumn("MES_END_TIME", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                SS.RPT_AddBasicColumn("END_TIME_DIFF", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                SS.RPT_AddBasicColumn("RFID_END_FLAG", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                SS.RPT_AddBasicColumn("MES_END_FLAG", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                SS.RPT_AddBasicColumn("RFID_MES_END_COMPARE", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                SS.RPT_AddBasicColumn("UNIQUE_ID_L", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                SS.RPT_AddBasicColumn("MAGAZINE_ID_L", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 100);
                SS.RPT_AddBasicColumn("RFID_ERASE_FLAG", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                SS.RPT_AddBasicColumn("UNIQUE_ID_U", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                SS.RPT_AddBasicColumn("MAGAZINE_ID_U", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                SS.RPT_AddBasicColumn("RFID_WRITE_FLAG", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void fnSSSortInit()
        {
            /****************************************************
             * comment : ss의 데이터의 정렬규칙을 설정하다.
             * 
             * created by : bee-jae jung(2012-12-17-월요일)
             * 
             * modified by : bee-jae jung(2012-12-17-월요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                // CUSTOMER
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "B.MAT_GRP_1", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
                }
                // FAMILY
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "B.MAT_GRP_2", "MAT_GRP_2", "B.MAT_GRP_2", true);
                }
                // PACKAGE
                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "B.MAT_GRP_3", "MAT_GRP_3", "B.MAT_GRP_3", true);
                }
                // TYPE1
                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "B.MAT_GRP_4", "MAT_GRP_4", "B.MAT_GRP_4", true);
                }
                // TYPE2
                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "B.MAT_GRP_5", "MAT_GRP_5", "B.MAT_GRP_5", true);
                }
                // LEAD COUNT
                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LEAD_COUNT", "B.MAT_GRP_6", "MAT_GRP_6", "B.MAT_GRP_6", true);
                }
                // DENSITY
                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "B.MAT_GRP_7", "MAT_GRP_7", "B.MAT_GRP_7", true);
                }
                // GENERATION
                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "B.MAT_GRP_8", "MAT_GRP_8", "B.MAT_GRP_8", true);
                }
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.MAT_ID", "MAT_ID", "A.MAT_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RETURN_TYPE", "A.RETURN_TYPE", "RETURN_TYPE", "A.RETURN_TYPE", true);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private Boolean fnBusinessRule()
        {
            /****************************************************
             * comment : 데이터를 처리를 위한 조건을 검사한다.
             * 
             * created by : bee-jae jung(2012-12-20-목요일)
             * 
             * modified by : bee-jae jung(2012-12-20-목요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (rb01.Checked == true)
                {
                    // D/A공정
                }
                else if (rb02.Checked == true)
                {
                    // W/B공정
                    // 2012-12-20-정비재 : wb공정은 설비번호가 들어가야 한다.(속도문제 때문에)
                    //                   : 설비번호의 최소자리수는 5자리 이므로... 
                    if (txtResourceID.Text.Replace("%", "").Length < 5)
                    {
                        if (CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD082", GlobalVariable.gcLanguage), this.Title, MessageBoxButtons.YesNo, 2) == DialogResult.No)
                        {
                            return false;
                        }
                    }
                }
                else if (rb03.Checked == true)
                {
                    // M/D공정
                }
                else if (rb04.Checked == true)
                {
                    // M/K공정
                }
                else if (rb05.Checked == true)
                {
                    // SBA공정
                }
                else if (rb06.Checked == true)
                {
                    // SST공정
                }

                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return false;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private Boolean fnDataFind()
        {
            /****************************************************
             * comment : database에 저장된 데이터를 조회한다.
             * 
             * created by : bee-jae jung(2012-12-17-월요일)
             * 
             * modified by : bee-jae jung(2012-12-17-월요일)
             ****************************************************/
            DataTable dt = null;
            String QRY = "";
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                LoadingPopUp.LoadIngPopUpShow(this);

                // 2012-12-17-정비재 : Sheet / Listview를 초기화 한다.
                CmnInitFunction.ClearList(SS01, true);

                // 2012-12-17-정비재 : 각 공정별로 Query문을 정의한다.
                if (rb01.Checked == true)
                {
                    // D/A공정
                    QRY = "SELECT A.RES_ID AS RES_ID \n"
                        + "     , A.LOT_ID AS LOT_ID \n"
                        + "     , A.OPER AS OPER \n"
                        + "     , A.RFID_START_TIME AS RFID_START_TIME \n"
                        + "     , A.MES_START_TIME AS MES_START_TIME \n"
                        + "     , A.START_TIME_DIFF AS START_TIME_DIFF \n"
                        + "     , A.RFID_START_FLAG AS RFID_START_FLAG \n"
                        + "     , A.MES_START_FLAG AS MES_START_FLAG \n"
                        + "     , A.RFID_MES_START_COMPARE AS RFID_MES_START_COMPARE \n"
                        + "     , B.RFID_END_TIME AS RFID_END_TIME \n"
                        + "     , B.MES_END_TIME AS MES_END_TIME \n"
                        + "     , B.END_TIME_DIFF AS END_TIME_DIFF \n"
                        + "     , B.RFID_END_FLAG AS RFID_END_FLAG \n"
                        + "     , B.MES_END_FLAG AS MES_END_FLAG \n"
                        + "     , B.RFID_MES_END_COMPARE AS RFID_MES_END_COMPARE \n"
                        + "     , A.UNIQUE_ID_L AS UNIQUE_ID_L \n"
                        + "     , A.MAGAZINE_ID_L AS MAGAZINE_ID_L \n"
                        + "     , A.RFID_ERASE_FLAG AS RFID_ERASE_FLAG \n"
                        + "     , A.UNIQUE_ID_U AS UNIQUE_ID_U \n"
                        + "     , A.MAGAZINE_ID_U AS MAGAZINE_ID_U \n"
                        + "     , A.RFID_WRITE_FLAG AS RFID_WRITE_FLAG \n"
                        + "  FROM (SELECT A.RES_ID AS RES_ID \n"
                        + "             , A.LOT_ID AS LOT_ID \n"
                        + "             , A.UNIQUE_ID_L AS UNIQUE_ID_L \n"
                        + "             , A.MAGAZINE_ID_L AS MAGAZINE_ID_L \n"
                        + "             , A.UNIQUE_ID_U AS UNIQUE_ID_U \n"
                        + "             , A.MAGAZINE_ID_U AS MAGAZINE_ID_U \n"
                        + "             , B.OPER AS OPER \n"
                        + "             , ROW_NUMBER () OVER (PARTITION BY A.RES_ID, A.LOT_ID, B.OPER ORDER BY A.RES_ID ASC, A.LOT_ID ASC, B.OPER ASC, A.RFID_START_TIME ASC) AS STATUS_IDX \n"
                        + "             , A.RFID_START_TIME AS RFID_START_TIME \n"
                        + "             , B.MES_START_TIME AS MES_START_TIME \n"
                        + "             , TO_CHAR(ROUND((TO_DATE(A.RFID_START_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(B.MES_START_TIME, 'YYYYMMDDHH24MISS')) * 86400, 1)) AS START_TIME_DIFF \n"
                        + "             , A.RFID_START_FLAG AS RFID_START_FLAG \n"
                        + "             , A.RFID_START_ERR_MSG AS RFID_START_ERR_MSG \n"
                        + "             , A.RFID_START_ERR_FIELD AS RFID_START_ERR_FIELD \n"
                        + "             , B.MES_START_FLAG AS MES_START_FLAG \n"
                        + "             , CASE WHEN A.RFID_START_FLAG = 'SUCCESS' AND ROUND(((TO_DATE(A.RFID_START_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(B.MES_START_TIME, 'YYYYMMDDHH24MISS')) * 86400), 1) BETWEEN 0 AND 20  THEN 'OK' \n"
                        + "                    ELSE ' ' \n"
                        + "               END AS RFID_MES_START_COMPARE \n"
                        + "             , C.RFID_ERASE_FLAG AS RFID_ERASE_FLAG \n"
                        + "             , C.RFID_ERASE_ERR_MSG AS RFID_ERASE_ERR_MSG \n"
                        + "             , C.RFID_ERASE_ERR_FIELD AS RFID_ERASE_ERR_FIELD \n"
                        + "             , D.RFID_WRITE_FLAG AS RFID_WRITE_FLAG \n"
                        + "             , D.RFID_WRITE_ERR_MSG AS RFID_WRITE_ERR_MSG \n"
                        + "             , D.RFID_WRITE_ERR_FIELD AS RFID_WRITE_ERR_FIELD \n"
                        + "          FROM (SELECT RES_ID AS RES_ID \n"
                        + "                     , LOT_ID AS LOT_ID \n"
                        + "                     , UNIQUE_ID_L AS UNIQUE_ID_L \n"
                        + "                     , MAGAZINE_ID_L AS MAGAZINE_ID_L \n"
                        + "                     , UNIQUE_ID_U AS UNIQUE_ID_U \n"
                        + "                     , MAGAZINE_ID_U AS MAGAZINE_ID_U \n"
                        + "                     , RFID_START_TIME AS RFID_START_TIME \n"
                        + "                     , RFID_START_FLAG AS RFID_START_FLAG \n"
                        + "                     , RFID_START_ERR_MSG AS RFID_START_ERR_MSG \n"
                        + "                     , RFID_START_ERR_FIELD AS RFID_START_ERR_FIELD \n"
                        + "                  FROM (SELECT RES_ID AS RES_ID \n"
                        + "                             , LOT_ID_1 AS LOT_ID \n"
                        + "                             , UNIQUE_ID_1 AS UNIQUE_ID_L \n"
                        + "                             , MAGAZINE_ID_1 AS MAGAZINE_ID_L \n"
                        + "                             , UNIQUE_ID_2 AS UNIQUE_ID_U \n"
                        + "                             , MAGAZINE_ID_2 AS MAGAZINE_ID_U \n"
                        + "                             , SYS_TIME AS RFID_START_TIME \n"
                        + "                             , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "                                           WHEN '1' THEN 'FAIL' \n"
                        + "                               END AS RFID_START_FLAG \n"
                        + "                             , ERR_MSG AS RFID_START_ERR_MSG \n"
                        + "                             , ERR_FIELD_MSG AS RFID_START_ERR_FIELD \n"
                        + "                          FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "                         WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "                           AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "                           AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "                           AND ANTENNA_PORT_1 = '1' \n"
                        + "                           AND MAGAZINE_ID_1 <> ' ' \n"
                        + "                           AND LOT_ID_1 <> ' ' \n"
                        + "                           AND ANTENNA_PORT_2 = '2' \n"
                        + "                           AND MAGAZINE_ID_2 <> ' ' \n"
                        + "                           AND LOT_ID_2 = ' ' \n"
                        + "                           AND RES_ID LIKE 'BD%' \n"
                        + "                           AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "                        UNION ALL \n"
                        + "                        SELECT RES_ID AS RES_ID \n"
                        + "                             , LOT_ID_2 AS LOT_ID \n"
                        + "                             , UNIQUE_ID_2 AS UNIQUE_ID_L \n"
                        + "                             , MAGAZINE_ID_2 AS MAGAZINE_ID_L \n"
                        + "                             , UNIQUE_ID_1 AS UNIQUE_ID_U \n"
                        + "                             , MAGAZINE_ID_1 AS MAGAZINE_ID_U \n"
                        + "                             , SYS_TIME AS RFID_START_TIME \n"
                        + "                             , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "                                           WHEN '1' THEN 'FAIL' \n"
                        + "                               END AS RFID_START_FLAG \n"
                        + "                             , ERR_MSG AS RFID_START_ERR_MSG \n"
                        + "                             , ERR_FIELD_MSG AS RFID_START_ERR_FIELD \n"
                        + "                          FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "                         WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "                           AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "                           AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "                           AND ANTENNA_PORT_1 = '2' \n"
                        + "                           AND MAGAZINE_ID_1 <> ' ' \n"
                        + "                           AND LOT_ID_1 = ' ' \n"
                        + "                           AND ANTENNA_PORT_2 = '1' \n"
                        + "                           AND MAGAZINE_ID_2 <> ' ' \n"
                        + "                           AND LOT_ID_2 <> ' ' \n"
                        + "                           AND RES_ID LIKE 'BD%' \n"
                        + "                           AND RES_ID LIKE '" + txtResourceID.Text + "%')) A \n"
			            + "                     , (SELECT START_RES_ID AS RES_ID \n"
					    + "                              , LOT_ID AS LOT_ID \n"
					    + "                              , OPER AS OPER \n"
					    + "                              , TRAN_TIME AS MES_START_TIME \n"
					    + "                              , CASE TRAN_CMF_19 WHEN 'RFID' THEN 'RFID' \n"
                        + "                                                 ELSE 'MESCLIENT' \n"
					    + "                                END AS MES_START_FLAG \n"
				        + "                           FROM RWIPLOTHIS \n"
				        + "                          WHERE LOT_TYPE = 'W' \n"
				        + "                            AND TRAN_CODE = 'START' \n" 
				        + "                            AND HIST_DEL_FLAG = ' ' \n"
				        + "                            AND OPER LIKE 'A040%' \n"
                        + "                            AND TRAN_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
				        + "                            AND LOT_ID IN (SELECT LOT_ID AS LOT_ID \n"
                        + "                                             FROM (SELECT LOT_ID_1 AS LOT_ID \n"
                        + "                                                     FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "                                                    WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "                                                      AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
						+ "                                                      AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "                                                      AND ANTENNA_PORT_1 = '1' \n"
						+ "                                                      AND MAGAZINE_ID_1 <> ' ' \n"
						+ "                                                      AND LOT_ID_1 <> ' ' \n"
						+ "                                                      AND ANTENNA_PORT_2 = '2' \n"
						+ "                                                      AND MAGAZINE_ID_2 <> ' ' \n"
						+ "                                                      AND LOT_ID_2 = ' ' \n"
						+ "                                                      AND RES_ID LIKE 'BD%' \n"
                        + "                                                      AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
						+ "                                                    GROUP BY LOT_ID_1 \n"
						+ "                                                   UNION ALL \n"
						+ "                                                   SELECT LOT_ID_2 AS LOT_ID \n"
						+ "                                                     FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "                                                    WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "                                                      AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
						+ "                                                      AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "                                                      AND ANTENNA_PORT_1 = '2' \n"
                        + "                                                      AND MAGAZINE_ID_1 <> ' ' \n"
                        + "                                                      AND LOT_ID_1 = ' ' \n"
                        + "                                                      AND ANTENNA_PORT_2 = '1' \n"
                        + "                                                      AND MAGAZINE_ID_2 <> ' ' \n"
                        + "                                                      AND LOT_ID_2 <> ' ' \n"
                        + "                                                      AND RES_ID LIKE 'BD%' \n"
                        + "                                                      AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "                                                    GROUP BY LOT_ID_2) \n"
                        + "                                            GROUP BY LOT_ID)) B \n"
			            + "                     , (SELECT SYS_TIME AS RFID_WRITE_TIME \n"
					    + "                             , UNIQUE_ID_1 AS UNIQUE_ID \n"
					    + "                             , MAGAZINE_ID_1 AS MAGAZINE_ID \n"
					    + "                             , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "                                           ELSE 'FAIL' \n"
					    + "                               END AS RFID_ERASE_FLAG \n"
					    + "                             , ERR_MSG AS RFID_ERASE_ERR_MSG \n"
					    + "                             , ERR_FIELD_MSG AS RFID_ERASE_ERR_FIELD \n"
				        + "                          FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "                         WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "                           AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
				        + "                           AND UPPER(FUNCTION_NAME) = 'SEND_EIS_RFID_WRITE_LOT_ID_REQ' \n"
				        + "                           AND ANTENNA_PORT_1 = '1' \n"
				        + "                           AND MAGAZINE_ID_1 <> ' ' \n"
				        + "                           AND LOT_ID_1 = ' ' \n"
				        + "                           AND RES_ID LIKE 'BD%' \n"
                        + "                           AND RES_ID LIKE '" + txtResourceID.Text + "%') C \n"
			            + "                     , (SELECT SYS_TIME AS RFID_WRITE_TIME \n"
					    + "                             , UNIQUE_ID_1 AS UNIQUE_ID \n"
					    + "                             , MAGAZINE_ID_1 AS MAGAZINE_ID \n"
					    + "                             , LOT_ID_1 AS LOT_ID \n"
					    + "                             , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "                                           ELSE 'FAIL' \n"
					    + "                               END AS RFID_WRITE_FLAG \n"
					    + "                             , ERR_MSG AS RFID_WRITE_ERR_MSG \n"
					    + "                             , ERR_FIELD_MSG AS RFID_WRITE_ERR_FIELD \n"
				        + "                          FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "                         WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "                           AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
				        + "                           AND UPPER(FUNCTION_NAME) = 'SEND_EIS_RFID_WRITE_LOT_ID_REQ' \n"
				        + "                           AND ANTENNA_PORT_1 = '2' \n"
				        + "                           AND MAGAZINE_ID_1 <> ' ' \n"
				        + "                           AND LOT_ID_1 <> ' ' \n"
                        + "                           AND RES_ID LIKE 'BD%' \n"
                        + "                           AND RES_ID LIKE '" + txtResourceID.Text + "%') D \n"
		                + "		 WHERE A.RES_ID = B.RES_ID(+) \n"
		                + "		   AND A.LOT_ID = B.LOT_ID(+) \n"
		                + "		   AND A.UNIQUE_ID_L = C.UNIQUE_ID(+) \n"
   		                + "		   AND A.MAGAZINE_ID_L = C.MAGAZINE_ID(+) \n"
   		                + "		   AND A.UNIQUE_ID_U = D.UNIQUE_ID(+) \n"
   		                + "		   AND A.MAGAZINE_ID_U = D.MAGAZINE_ID(+)) A \n"
	                    + "     , (SELECT A.RES_ID AS RES_ID \n"
			            + "             , A.LOT_ID AS LOT_ID \n"
			            + "             , B.OPER AS OPER \n"
			            + "             , ROW_NUMBER () OVER (PARTITION BY A.RES_ID, A.LOT_ID, B.OPER ORDER BY A.RES_ID ASC, A.LOT_ID ASC, B.OPER ASC, A.RFID_END_TIME ASC) AS STATUS_IDX \n"
			            + "             , A.RFID_END_TIME AS RFID_END_TIME \n"
			            + "             , B.MES_END_TIME AS MES_END_TIME \n"
			            + "             , TO_CHAR(ROUND(((TO_DATE(A.RFID_END_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(B.MES_END_TIME, 'YYYYMMDDHH24MISS')) * 86400), 1)) AS END_TIME_DIFF \n"
                        + "             , A.RFID_END_FLAG AS RFID_END_FLAG \n"
                        + "             , A.RFID_END_ERR_MSG AS RFID_END_ERR_MSG \n"
                        + "             , A.RFID_END_ERR_FIELD AS RFID_END_ERR_FIELD \n"
                        + "             , B.MES_END_FLAG AS MES_END_FLAG \n"
                        + "             , CASE WHEN A.RFID_END_FLAG = 'SUCCESS' AND ROUND(((TO_DATE(A.RFID_END_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(B.MES_END_TIME, 'YYYYMMDDHH24MISS')) * 86400), 1) BETWEEN 0 AND 20 THEN 'OK' \n"
                        + "                    ELSE ' ' \n"
                        + "               END AS RFID_MES_END_COMPARE \n"
                        + "          FROM (SELECT RES_ID AS RES_ID \n"
                        + "                     , LOT_ID AS LOT_ID \n"
                        + "                     , RFID_END_TIME AS RFID_END_TIME \n"
                        + "                     , RFID_END_FLAG AS RFID_END_FLAG \n"
                        + "                     , RFID_END_ERR_MSG AS RFID_END_ERR_MSG \n"
                        + "                     , RFID_END_ERR_FIELD AS RFID_END_ERR_FIELD \n"
                        + "                  FROM (SELECT RES_ID AS RES_ID \n"
                        + "                             , LOT_ID_1 AS LOT_ID \n"
                        + "                             , SYS_TIME AS RFID_END_TIME \n"
                        + "                             , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "                                           WHEN '1' THEN 'FAIL' \n"
                        + "                               END AS RFID_END_FLAG \n"
                        + "                             , ERR_MSG AS RFID_END_ERR_MSG \n"
                        + "                             , ERR_FIELD_MSG AS RFID_END_ERR_FIELD \n"
                        + "                          FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "                         WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "                           AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "                           AND UPPER(FUNCTION_NAME) = 'RFID_EIS_END_MAGAZINE_REQ' \n"
                        + "                           AND ANTENNA_PORT_1 = '2' \n"
                        + "                           AND MAGAZINE_ID_1 <> ' ' \n"
                        + "                           AND LOT_ID_1 <> ' ' \n"
                        + "                           AND ANTENNA_PORT_2 = '0' \n"
                        + "                           AND MAGAZINE_ID_2 = ' ' \n"
                        + "                           AND LOT_ID_2 = ' ' \n"
                        + "                           AND RES_ID LIKE 'BD%' \n"
                        + "                           AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "                        UNION ALL \n"
                        + "                        SELECT RES_ID AS RES_ID \n"
                        + "                             , LOT_ID_1 AS LOT_ID \n"
                        + "                             , SYS_TIME AS RFID_END_TIME \n"
                        + "                             , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "                                           WHEN '1' THEN 'FAIL' \n"
                        + "                               END AS RFID_END_FLAG \n"
                        + "                             , ERR_MSG AS RFID_END_ERR_MSG \n"
                        + "                             , ERR_FIELD_MSG AS RFID_END_ERR_FIELD \n"
                        + "                          FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "                         WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "                           AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "                           AND UPPER(FUNCTION_NAME) = 'RFID_EIS_END_MAGAZINE_REQ' \n"
                        + "                           AND ANTENNA_PORT_1 = '2' \n"
                        + "                           AND MAGAZINE_ID_1 <> ' ' \n"
                        + "                           AND LOT_ID_1 <> ' ' \n"
                        + "                           AND ANTENNA_PORT_2 = '1' \n"
                        + "                           AND MAGAZINE_ID_2 <> ' ' \n"
                        + "                           AND LOT_ID_1 <> LOT_ID_2 \n"
                        + "                           AND RES_ID LIKE 'BD%' \n"
                        + "                           AND RES_ID LIKE '" + txtResourceID.Text + "%')) A \n"
                        + "             , (SELECT END_RES_ID AS RES_ID \n"
                        + "                     , LOT_ID AS LOT_ID \n"
                        + "                     , OLD_OPER AS OPER \n"
                        + "                     , TRAN_TIME AS MES_END_TIME \n"
                        + "                     , CASE TRAN_CMF_19 WHEN 'RFID' THEN 'RFID' \n"
                        + "                                        ELSE 'MESCLIENT' \n"
                        + "                       END AS MES_END_FLAG \n"
                        + "                  FROM RWIPLOTHIS \n"
                        + "                 WHERE LOT_TYPE = 'W' \n"
                        + "                   AND TRAN_CODE = 'END' \n"
                        + "                   AND HIST_DEL_FLAG = ' ' \n"
                        + "                   AND OLD_OPER LIKE 'A040%' \n"
                        + "                   AND TRAN_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "                   AND LOT_ID IN (SELECT LOT_ID AS LOT_ID \n"
                        + "                                    FROM (SELECT LOT_ID_1 AS LOT_ID \n"
                        + "                     				       FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "                                           WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "                                             AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "                                             AND UPPER(FUNCTION_NAME) = 'RFID_EIS_END_MAGAZINE_REQ' \n"
                        + "                                             AND ANTENNA_PORT_1 = '2' \n"
                        + "                                             AND MAGAZINE_ID_1 <> ' ' \n"
                        + "                                             AND LOT_ID_1 <> ' ' \n"
                        + "                                             AND ANTENNA_PORT_2 = '0' \n"
                        + "                                             AND MAGAZINE_ID_2 = ' ' \n"
                        + "                                             AND LOT_ID_2 = ' ' \n"
                        + "                                             AND RES_ID LIKE 'BD%' \n"
                        + "                                             AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "                                           GROUP BY LOT_ID_1 \n"
                        + "                                          UNION ALL \n"
                        + "                                          SELECT LOT_ID_1 AS LOT_ID \n"
                        + "                                            FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "                                           WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "                                             AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "                                             AND UPPER(FUNCTION_NAME) = 'RFID_EIS_END_MAGAZINE_REQ' \n"
                        + "                                             AND ANTENNA_PORT_1 = '2' \n"
                        + "                                             AND MAGAZINE_ID_1 <> ' ' \n"
                        + "                                             AND LOT_ID_1 <> ' ' \n"
                        + "                                             AND ANTENNA_PORT_2 = '1' \n"
                        + "                                             AND MAGAZINE_ID_2 <> ' ' \n"
                        + "                                             AND LOT_ID_1 <> LOT_ID_2 \n"
                        + "                                             AND RES_ID LIKE 'BD%' \n"
                        + "                                             AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "                                           GROUP BY LOT_ID_1))) B \n"
                        + "		 WHERE A.RES_ID = B.RES_ID(+) \n"
                        + "		   AND A.LOT_ID = B.LOT_ID(+)) B \n"
                        + " WHERE A.RES_ID = B.RES_ID(+) \n"
                        + "   AND A.LOT_ID = B.LOT_ID(+) \n"
                        + "   AND A.OPER = B.OPER(+) \n"
                        + "   AND A.STATUS_IDX = B.STATUS_IDX(+) \n"
                        + " ORDER BY A.RES_ID ASC, A.LOT_ID ASC, A.RFID_START_TIME ASC, B.RFID_END_TIME ASC";
                }
                else if (rb02.Checked == true)
                {
                    // W/B공정
                    QRY = "SELECT A.RES_ID AS RES_ID \n"
                        + "		, A.LOT_ID AS LOT_ID \n"
                        + "     , A.OPER AS OPER \n"
                        + "     , A.RFID_START_TIME AS RFID_START_TIME \n"
                        + "	    , A.MES_START_TIME AS MES_START_TIME \n"
                        + "	    , A.START_TIME_DIFF AS START_TIME_DIFF \n"
                        + "	    , A.RFID_START_FLAG AS RFID_START_FLAG \n"
                        + "	    , A.MES_START_FLAG AS MES_START_FLAG \n"
                        + "	    , A.RFID_MES_START_COMPARE AS RFID_MES_START_COMPARE \n"
                        + "	    , B.RFID_END_TIME AS RFID_END_TIME \n"
                        + "	    , B.MES_END_TIME AS MES_END_TIME \n"
                        + "	    , B.END_TIME_DIFF AS END_TIME_DIFF \n"
                        + "	    , B.RFID_END_FLAG AS RFID_END_FLAG \n"
                        + "	    , B.MES_END_FLAG AS MES_END_FLAG \n"
                        + "	    , B.RFID_MES_END_COMPARE AS RFID_MES_END_COMPARE \n"
                        + "	    , A.UNIQUE_ID_L AS UNIQUE_ID_L \n"
                        + "	    , A.MAGAZINE_ID_L AS MAGAZINE_ID_L \n"
                        + "	    , A.RFID_ERASE_FLAG AS RFID_ERASE_FLAG \n"
                        + "	    , A.UNIQUE_ID_U AS UNIQUE_ID_U \n"
                        + "	    , A.MAGAZINE_ID_U AS MAGAZINE_ID_U \n"
                        + "	    , A.RFID_WRITE_FLAG AS RFID_WRITE_FLAG \n"
                        + "  FROM (SELECT A.RES_ID AS RES_ID \n"
                        + "			    , A.LOT_ID AS LOT_ID \n"
                        + "			    , A.UNIQUE_ID_L AS UNIQUE_ID_L \n"
                        + "			    , A.MAGAZINE_ID_L AS MAGAZINE_ID_L \n"
                        + "			    , A.UNIQUE_ID_U AS UNIQUE_ID_U \n"
                        + "			    , A.MAGAZINE_ID_U AS MAGAZINE_ID_U \n"
                        + "			    , B.OPER AS OPER \n"
                        + "			    , ROW_NUMBER () OVER (PARTITION BY A.RES_ID, A.LOT_ID, B.OPER ORDER BY A.RES_ID ASC, A.LOT_ID ASC, B.OPER ASC, A.RFID_START_TIME ASC) AS STATUS_IDX \n"
                        + "			    , A.RFID_START_TIME AS RFID_START_TIME \n"
                        + "			    , B.MES_START_TIME AS MES_START_TIME \n"
                        + "			    , TO_CHAR(ROUND((TO_DATE(A.RFID_START_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(B.MES_START_TIME, 'YYYYMMDDHH24MISS')) * 86400, 1)) AS START_TIME_DIFF \n"
                        + "			    , A.RFID_START_FLAG AS RFID_START_FLAG \n"
                        + "			    , A.RFID_START_ERR_MSG AS RFID_START_ERR_MSG \n"
                        + "			    , A.RFID_START_ERR_FIELD AS RFID_START_ERR_FIELD \n"
                        + "			    , B.MES_START_FLAG AS MES_START_FLAG \n"
                        + "			    , CASE WHEN A.RFID_START_FLAG = 'SUCCESS' AND ROUND(((TO_DATE(A.RFID_START_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(B.MES_START_TIME, 'YYYYMMDDHH24MISS')) * 86400), 1) BETWEEN 0 AND 20  THEN 'OK' \n"
                        + "					   ELSE ' ' \n"
                        + "			      END AS RFID_MES_START_COMPARE \n"
                        + "			    , C.RFID_ERASE_FLAG AS RFID_ERASE_FLAG \n"
                        + "			    , C.RFID_ERASE_ERR_MSG AS RFID_ERASE_ERR_MSG \n"
                        + "			    , C.RFID_ERASE_ERR_FIELD AS RFID_ERASE_ERR_FIELD \n"
                        + "			    , D.RFID_WRITE_FLAG AS RFID_WRITE_FLAG \n"
                        + "			    , D.RFID_WRITE_ERR_MSG AS RFID_WRITE_ERR_MSG \n"
                        + "			    , D.RFID_WRITE_ERR_FIELD AS RFID_WRITE_ERR_FIELD \n"
                        + "		     FROM (SELECT RES_ID AS RES_ID \n"
                        + "                     , LOT_ID AS LOT_ID \n"
                        + "					    , UNIQUE_ID_L AS UNIQUE_ID_L \n"
                        + "					    , MAGAZINE_ID_L AS MAGAZINE_ID_L \n"
                        + "					    , UNIQUE_ID_U AS UNIQUE_ID_U \n"
                        + "					    , MAGAZINE_ID_U AS MAGAZINE_ID_U \n"
                        + "					    , RFID_START_TIME AS RFID_START_TIME \n"
                        + "					    , RFID_START_FLAG AS RFID_START_FLAG \n"
                        + "					    , RFID_START_ERR_MSG AS RFID_START_ERR_MSG \n"
                        + "					    , RFID_START_ERR_FIELD AS RFID_START_ERR_FIELD \n"
                        + "				     FROM (SELECT RES_ID AS RES_ID \n"
                        + "							    , LOT_ID_1 AS LOT_ID \n"
                        + "							    , UNIQUE_ID_1 AS UNIQUE_ID_L \n"
                        + "							    , MAGAZINE_ID_1 AS MAGAZINE_ID_L \n"
                        + "							    , UNIQUE_ID_2 AS UNIQUE_ID_U \n"
                        + "							    , MAGAZINE_ID_2 AS MAGAZINE_ID_U \n"
                        + "							    , SYS_TIME AS RFID_START_TIME \n"
                        + "							    , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "										      WHEN '1' THEN 'FAIL' \n"
                        + "							      END AS RFID_START_FLAG \n"
                        + "							    , ERR_MSG AS RFID_START_ERR_MSG \n"
                        + "							    , ERR_FIELD_MSG AS RFID_START_ERR_FIELD \n"
                        + "						     FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "						    WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "						      AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "						      AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "						      AND ANTENNA_PORT_1 = '1' \n"
                        + "						      AND MAGAZINE_ID_1 <> ' ' \n"
                        + "						      AND LOT_ID_1 <> ' ' \n"
                        + "						      AND ANTENNA_PORT_2 = '0' \n"
                        + "						      AND MAGAZINE_ID_2 = ' ' \n"
                        + "						      AND LOT_ID_2 = ' ' \n"
                        + "						      AND RES_ID LIKE 'BW%' \n"
                        + "						      AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "						   UNION ALL \n"
                        + "						   SELECT RES_ID AS RES_ID \n"
                        + "							    , LOT_ID_2 AS LOT_ID \n"
                        + "							    , UNIQUE_ID_2 AS UNIQUE_ID_L \n"
                        + "							    , MAGAZINE_ID_2 AS MAGAZINE_ID_L \n"
                        + "							    , UNIQUE_ID_1 AS UNIQUE_ID_U \n"
                        + "							    , MAGAZINE_ID_1 AS MAGAZINE_ID_U \n"
                        + "							    , SYS_TIME AS RFID_START_TIME \n"
                        + "							    , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "										      WHEN '1' THEN 'FAIL' \n"
                        + "							      END AS RFID_START_FLAG \n"
                        + "							    , ERR_MSG AS RFID_START_ERR_MSG \n"
                        + "							    , ERR_FIELD_MSG AS RFID_START_ERR_FIELD \n"
                        + "						     FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "						    WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "						      AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "						      AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "						      AND ANTENNA_PORT_1 = '2' \n"
                        + "						      AND MAGAZINE_ID_1 <> ' ' \n"
                        + "						      AND LOT_ID_1 = ' ' \n"
                        + "						      AND ANTENNA_PORT_2 = '1' \n"
                        + "						      AND MAGAZINE_ID_2 <> ' ' \n"
                        + "						      AND LOT_ID_2 <> ' ' \n"
                        + "						      AND RES_ID LIKE 'BW%' \n"
                        + "						      AND RES_ID LIKE '" + txtResourceID.Text + "%')) A \n"
                        + "             , (SELECT RES_ID AS RES_ID \n"
                        + "                     , LOT_ID AS LOT_ID \n"
                        + "					    , OPER AS OPER \n"
                        + "					    , MES_START_TIME AS MES_START_TIME \n"
                        + "					    , MES_START_FLAG AS MES_START_FLAG \n"
                        + "				     FROM (SELECT A.RES_ID AS RES_ID \n"
                        + "                             , A.LOT_ID AS LOT_ID \n"
                        + "							    , A.OPER AS OPER \n"
                        + "							    , A.MES_START_TIME AS MES_START_TIME \n"
                        + "							    , A.MES_START_FLAG AS MES_START_FLAG \n"
                        + "						     FROM (SELECT START_RES_ID AS RES_ID \n"
                        + "									    , LOT_ID AS LOT_ID \n"
                        + "									    , OPER AS OPER \n"
                        + "									    , TRAN_TIME AS MES_START_TIME \n"
                        + "									    , CASE TRAN_CMF_19 WHEN 'RFID' THEN 'RFID' \n"
                        + "														   ELSE 'MESCLIENT' \n"
                        + "									      END AS MES_START_FLAG \n"
                        + "								     FROM RWIPLOTHIS \n"
                        + "								    WHERE LOT_TYPE = 'W' \n"
                        + "								      AND TRAN_CODE = 'START' \n"
                        + "								      AND OPER LIKE 'A060%' \n"
                        + "								      AND TRAN_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "								      AND ADD_ORDER_ID_2 = 'MULTIEQP_MOT' \n"
                        + "								      AND HIST_DEL_FLAG = ' ') A \n"
                        + "						        , (SELECT RES_ID AS RES_ID \n"
                        + "                                     , LOT_ID AS LOT_ID \n"
                        + "						             FROM (SELECT RES_ID AS RES_ID \n"
                        + "									            , LOT_ID_1 AS LOT_ID \n"
                        + "										     FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "										    WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "										      AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "										      AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "										      AND ANTENNA_PORT_1 = '1' \n"
                        + "										      AND MAGAZINE_ID_1 <> ' ' \n"
                        + "										      AND LOT_ID_1 <> ' ' \n"
                        + "										      AND ANTENNA_PORT_2 = '0' \n"
                        + "										      AND MAGAZINE_ID_2 = ' ' \n"
                        + "										      AND LOT_ID_2 = ' ' \n"
                        + "										      AND RES_ID LIKE 'BW%' \n"
                        + "										      AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "										    GROUP BY RES_ID, LOT_ID_1 \n"
                        + "										   UNION ALL \n"
                        + "										   SELECT RES_ID AS RES_ID \n"
                        + "											    , LOT_ID_2 AS LOT_ID \n"
                        + "										     FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "										    WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "										      AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "										      AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "										      AND ANTENNA_PORT_1 = '2' \n"
                        + "										      AND MAGAZINE_ID_1 <> ' ' \n"
                        + "										      AND LOT_ID_1 = ' ' \n"
                        + "										      AND ANTENNA_PORT_2 = '1' \n"
                        + "										      AND MAGAZINE_ID_2 <> ' ' \n"
                        + "										      AND LOT_ID_2 <> ' ' \n"
                        + "										      AND RES_ID LIKE 'BW%' \n"
                        + "										      AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "										    GROUP BY RES_ID, LOT_ID_2) \n"
                        + "							      GROUP BY RES_ID, LOT_ID) B \n"
                        + "						 WHERE A.RES_ID = B.RES_ID \n"
                        + "						   AND A.LOT_ID = B.LOT_ID \n"
                        + "						UNION ALL \n"
                        + "						SELECT B.RES_ID AS RES_ID \n"
                        + "							 , A.LOT_ID AS LOT_ID \n"
                        + "							 , B.OPER AS OPER \n"
                        + "							 , B.MES_START_TIME AS MES_START_TIME \n"
                        + "							 , B.MES_START_FLAG AS MES_START_FLAG \n"
                        + "						  FROM (SELECT RES_ID AS RES_ID \n"
                        + "									 , LOT_ID AS LOT_ID \n"
                        + "								  FROM (SELECT RES_ID AS RES_ID \n"
                        + "											 , LOT_ID_1 AS LOT_ID \n"
                        + "										  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "										 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "										   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "										   AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "										   AND ANTENNA_PORT_1 = '1' \n"
                        + "										   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "										   AND LOT_ID_1 <> ' ' \n"
                        + "										   AND ANTENNA_PORT_2 = '0' \n"
                        + "										   AND MAGAZINE_ID_2 = ' ' \n"
                        + "										   AND LOT_ID_2 = ' ' \n"
                        + "										   AND RES_ID LIKE 'BW%' \n"
                        + "										   AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "										 GROUP BY RES_ID, LOT_ID_1 \n"
                        + "										UNION ALL \n"
                        + "										SELECT RES_ID AS RES_ID \n"
                        + "											 , LOT_ID_2 AS LOT_ID \n"
                        + "										  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "										 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "										   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "										   AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "										   AND ANTENNA_PORT_1 = '2' \n"
                        + "										   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "										   AND LOT_ID_1 = ' ' \n"
                        + "										   AND ANTENNA_PORT_2 = '1' \n"
                        + "										   AND MAGAZINE_ID_2 <> ' ' \n"
                        + "										   AND LOT_ID_2 <> ' ' \n"
                        + "										   AND RES_ID LIKE 'BW%' \n"
                        + "										   AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "										 GROUP BY RES_ID, LOT_ID_2) \n"
                        + "								GROUP BY RES_ID, LOT_ID) A \n"
                        + "							 , (SELECT START_RES_ID AS RES_ID \n"
                        + "									 , LOT_ID AS LOT_ID \n"
                        + "									 , OPER AS OPER \n"
                        + "									 , TRAN_TIME AS MES_START_TIME \n"
                        + "									 , CASE TRAN_CMF_19 WHEN 'RFID' THEN 'RFID' \n"
                        + "														ELSE 'MESCLIENT' \n"
                        + "									   END AS MES_START_FLAG \n"
                        + "								  FROM RWIPLOTHIS \n"
                        + "								 WHERE LOT_TYPE = 'W' \n"
                        + "								   AND TRAN_CODE = 'START' \n"
                        + "								   AND OPER LIKE 'A060%' \n"
                        + "								   AND TRAN_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "								   AND ADD_ORDER_ID_2 = 'MULTIEQP' \n"
                        + "								   AND HIST_DEL_FLAG = ' ') B \n"
                        + "						 WHERE A.RES_ID = B.RES_ID \n"
                        + "						   AND A.LOT_ID = SUBSTR(B.LOT_ID, 1, LENGTH(B.LOT_ID) - 2))) B \n"
                        + "			 , (SELECT SYS_TIME AS RFID_WRITE_TIME \n"
                        + "					 , UNIQUE_ID_1 AS UNIQUE_ID \n"
                        + "					 , MAGAZINE_ID_1 AS MAGAZINE_ID \n"
                        + "					 , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "								   ELSE 'FAIL' \n"
                        + "					   END AS RFID_ERASE_FLAG \n"
                        + "					 , ERR_MSG AS RFID_ERASE_ERR_MSG \n"
                        + "					 , ERR_FIELD_MSG AS RFID_ERASE_ERR_FIELD \n"
                        + "				  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "				 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "				   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "				   AND UPPER(FUNCTION_NAME) = 'SEND_EIS_RFID_WRITE_LOT_ID_REQ' \n"
                        + "				   AND ANTENNA_PORT_1 = '1' \n"
                        + "				   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "				   AND LOT_ID_1 = ' ' \n"
                        + "				   AND RES_ID LIKE 'BW%' \n"
                        + "				   AND RES_ID LIKE '" + txtResourceID.Text + "%') C \n"
                        + "			 , (SELECT SYS_TIME AS RFID_WRITE_TIME \n"
                        + "					 , UNIQUE_ID_1 AS UNIQUE_ID \n"
                        + "					 , MAGAZINE_ID_1 AS MAGAZINE_ID \n"
                        + "					 , LOT_ID_1 AS LOT_ID \n"
                        + "					 , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "								   ELSE 'FAIL' \n"
                        + "					   END AS RFID_WRITE_FLAG \n"
                        + "					 , ERR_MSG AS RFID_WRITE_ERR_MSG \n"
                        + "					 , ERR_FIELD_MSG AS RFID_WRITE_ERR_FIELD \n"
                        + "				  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "				 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "				   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "				   AND UPPER(FUNCTION_NAME) = 'SEND_EIS_RFID_WRITE_LOT_ID_REQ' \n"
                        + "				   AND ANTENNA_PORT_1 = '2' \n"
                        + "				   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "				   AND LOT_ID_1 <> ' ' \n"
                        + "				   AND RES_ID LIKE 'BW%' \n"
                        + "				   AND RES_ID LIKE '" + txtResourceID.Text + "%') D \n"
                        + "		 WHERE A.RES_ID = B.RES_ID(+) \n"
                        + "		   AND A.LOT_ID = B.LOT_ID(+) \n"
                        + "		   AND A.UNIQUE_ID_L = C.UNIQUE_ID(+) \n"
                        + "		   AND A.MAGAZINE_ID_L = C.MAGAZINE_ID(+) \n"
                        + "		   AND A.UNIQUE_ID_U = D.UNIQUE_ID(+) \n"
                        + "		   AND A.MAGAZINE_ID_U = D.MAGAZINE_ID(+)) A \n"
                        + " 	 , (SELECT RES_ID AS RES_ID \n"
                        + "			     , LOT_ID AS LOT_ID \n"
                        + "			     , OPER AS OPER \n"
                        + "			     , 1 AS STATUS_IDX \n"
                        + "			     , ' ' AS RFID_END_TIME \n"
                        + "			     , MES_END_TIME AS MES_END_TIME \n"
                        + "			     , ' ' AS END_TIME_DIFF \n"
                        + "			     , ' ' AS RFID_END_FLAG \n"
                        + "			     , ' ' AS RFID_END_ERR_MSG \n"
                        + "			     , ' ' AS RFID_END_ERR_FIELD \n"
                        + "			     , MES_END_FLAG AS MES_END_FLAG \n"
                        + "			     , ' ' AS RFID_MES_END_COMPARE \n"
                        + "		      FROM (SELECT RES_ID AS RES_ID \n"
                        + "					     , LOT_ID AS LOT_ID \n"
                        + "					     , OPER AS OPER \n"
                        + "					     , MES_END_TIME AS MES_END_TIME \n"
                        + "					     , MES_END_FLAG AS MES_END_FLAG \n"
                        + "				      FROM (SELECT A.RES_ID AS RES_ID \n"
                        + "							     , A.LOT_ID AS LOT_ID \n"
                        + "							     , A.OPER AS OPER \n"
                        + "							     , A.MES_END_TIME AS MES_END_TIME \n"
                        + "							     , A.MES_END_FLAG AS MES_END_FLAG \n"
                        + "						      FROM (SELECT END_RES_ID AS RES_ID \n"
                        + "									     , LOT_ID AS LOT_ID \n"
                        + "									     , OLD_OPER AS OPER \n"
                        + "									     , TRAN_TIME AS MES_END_TIME \n"
                        + "									     , CASE TRAN_CMF_19 WHEN 'RFID' THEN 'RFID' \n"
                        + "														    ELSE 'MESCLIENT' \n"
                        + "									       END AS MES_END_FLAG \n"
                        + "								      FROM RWIPLOTHIS \n"
                        + "								     WHERE LOT_TYPE = 'W' \n"
                        + "								       AND TRAN_CODE = 'END' \n"
                        + "								       AND OLD_OPER LIKE 'A060%' \n"
                        + "								       AND TRAN_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "								       AND ADD_ORDER_ID_2 = 'MULTIEQP_MOT' \n"
                        + "								       AND HIST_DEL_FLAG = ' ') A \n"
                        + "							 , (SELECT RES_ID AS RES_ID \n"
                        + "									 , LOT_ID AS LOT_ID \n"
                        + "								  FROM (SELECT RES_ID AS RES_ID \n"
                        + "											 , LOT_ID_1 AS LOT_ID \n"
                        + "										  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "										 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "										   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "										   AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "										   AND ANTENNA_PORT_1 = '1' \n"
                        + "										   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "										   AND LOT_ID_1 <> ' ' \n"
                        + "										   AND ANTENNA_PORT_2 = '0' \n"
                        + "										   AND MAGAZINE_ID_2 = ' ' \n"
                        + "										   AND LOT_ID_2 = ' ' \n"
                        + "										   AND RES_ID LIKE 'BW%' \n"
                        + "                                        AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "										 GROUP BY RES_ID, LOT_ID_1 \n"
                        + "										UNION ALL \n"
                        + "										SELECT RES_ID AS RES_ID \n"
                        + "											 , LOT_ID_2 AS LOT_ID \n"
                        + "										  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "										 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "										   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "										   AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "										   AND ANTENNA_PORT_1 = '2' \n"
                        + "										   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "										   AND LOT_ID_1 = ' ' \n"
                        + "										   AND ANTENNA_PORT_2 = '1' \n"
                        + "										   AND MAGAZINE_ID_2 <> ' ' \n"
                        + "										   AND LOT_ID_2 <> ' ' \n"
                        + "										   AND RES_ID LIKE 'BW%' \n"
                        + "										   AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "										 GROUP BY RES_ID, LOT_ID_2) \n"
                        + "							   GROUP BY RES_ID, LOT_ID) B \n"
                        + "						 WHERE A.RES_ID = B.RES_ID \n"
                        + "						   AND A.LOT_ID = B.LOT_ID \n"
                        + "						UNION ALL \n"
                        + "						SELECT B.RES_ID AS RES_ID \n"
                        + "							 , A.LOT_ID AS LOT_ID \n"
                        + "							 , B.OPER AS OPER \n"
                        + "							 , B.MES_END_TIME AS MES_END_TIME \n"
                        + "							 , B.MES_END_FLAG AS MES_END_FLAG \n"
                        + "						  FROM (SELECT RES_ID AS RES_ID \n"
                        + "									 , LOT_ID AS LOT_ID \n"
                        + "								  FROM (SELECT RES_ID AS RES_ID \n"
                        + "											 , LOT_ID_1 AS LOT_ID \n"
                        + "										  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "										 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "										   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "										   AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "										   AND ANTENNA_PORT_1 = '1' \n"
                        + "										   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "										   AND LOT_ID_1 <> ' ' \n"
                        + "										   AND ANTENNA_PORT_2 = '0' \n"
                        + "										   AND MAGAZINE_ID_2 = ' ' \n"
                        + "										   AND LOT_ID_2 = ' ' \n"
                        + "										   AND RES_ID LIKE 'BW%' \n"
                        + "										   AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "										 GROUP BY RES_ID, LOT_ID_1 \n"
                        + "										UNION ALL \n"
                        + "										SELECT RES_ID AS RES_ID \n"
                        + "											 , LOT_ID_2 AS LOT_ID \n"
                        + "										  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "										 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "										   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "										   AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "										   AND ANTENNA_PORT_1 = '2' \n"
                        + "										   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "										   AND LOT_ID_1 = ' ' \n"
                        + "										   AND ANTENNA_PORT_2 = '1' \n"
                        + "										   AND MAGAZINE_ID_2 <> ' ' \n"
                        + "										   AND LOT_ID_2 <> ' ' \n"
                        + "										   AND RES_ID LIKE 'BW%' \n"
                        + "										   AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "										 GROUP BY RES_ID, LOT_ID_2) \n"
                        + "								GROUP BY RES_ID, LOT_ID) A \n"
                        + "							 , (SELECT END_RES_ID AS RES_ID \n"
                        + "									 , LOT_ID AS LOT_ID \n"
                        + "									 , OLD_OPER AS OPER \n"
                        + "									 , TRAN_TIME AS MES_END_TIME \n"
                        + "									 , CASE TRAN_CMF_19 WHEN 'RFID' THEN 'RFID' \n"
                        + "														ELSE 'MESCLIENT' \n"
                        + "									   END AS MES_END_FLAG \n"
                        + "								  FROM RWIPLOTHIS \n"
                        + "								 WHERE LOT_TYPE = 'W' \n"
                        + "								   AND TRAN_CODE = 'END' \n"
                        + "								   AND OLD_OPER LIKE 'A060%' \n"
                        + "								   AND TRAN_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "								   AND ADD_ORDER_ID_2 = 'MULTIEQP' \n"
                        + "								   AND HIST_DEL_FLAG = ' ') B \n"
                        + "						 WHERE A.RES_ID = B.RES_ID \n"
                        + "						   AND A.LOT_ID = SUBSTR(B.LOT_ID, 1, LENGTH(B.LOT_ID) - 2)))) B \n"
                        + " WHERE A.RES_ID = B.RES_ID(+) \n"
                        + "   AND A.LOT_ID = B.LOT_ID(+) \n"
                        + "   AND A.OPER = B.OPER(+) \n"
                        + "   AND A.STATUS_IDX = B.STATUS_IDX(+) \n"
                        + " ORDER BY A.LOT_ID ASC, A.RES_ID ASC, A.RFID_START_TIME ASC, B.RFID_END_TIME ASC";
                }
                else if (rb03.Checked == true)
                {
                    // M/D공정
                    QRY = "SELECT A.RES_ID AS RES_ID \n"
                        + "			 , A.LOT_ID AS LOT_ID \n"
                        + "			 , A.OPER AS OPER \n"
                        + "			 , A.RFID_START_TIME AS RFID_START_TIME \n"
                        + "			 , A.MES_START_TIME AS MES_START_TIME \n"
                        + "			 , A.START_TIME_DIFF AS START_TIME_DIFF \n"
                        + "			 , A.RFID_START_FLAG AS RFID_START_FLAG \n"
                        + "			 , A.MES_START_FLAG AS MES_START_FLAG \n"
                        + "			 , A.RFID_MES_START_COMPARE AS RFID_MES_START_COMPARE \n"
                        + "			 , B.RFID_END_TIME AS RFID_END_TIME \n"
                        + "			 , B.MES_END_TIME AS MES_END_TIME \n"
                        + "			 , B.END_TIME_DIFF AS END_TIME_DIFF \n"
                        + "			 , B.RFID_END_FLAG AS RFID_END_FLAG \n"
                        + "			 , B.MES_END_FLAG AS MES_END_FLAG \n"
                        + "			 , B.RFID_MES_END_COMPARE AS RFID_MES_END_COMPARE \n"
                        + "			 , A.UNIQUE_ID_L AS UNIQUE_ID_L \n"
                        + "			 , A.MAGAZINE_ID_L AS MAGAZINE_ID_L \n"
                        + "			 , A.RFID_ERASE_FLAG AS RFID_ERASE_FLAG \n"
                        + "			 , A.UNIQUE_ID_U AS UNIQUE_ID_U \n"
                        + "			 , A.MAGAZINE_ID_U AS MAGAZINE_ID_U \n"
                        + "			 , A.RFID_WRITE_FLAG AS RFID_WRITE_FLAG \n"
                        + "		  FROM (SELECT A.RES_ID AS RES_ID \n"
                        + "					 , A.LOT_ID AS LOT_ID \n"
                        + "					 , A.UNIQUE_ID_L AS UNIQUE_ID_L \n"
                        + "					 , A.MAGAZINE_ID_L AS MAGAZINE_ID_L \n"
                        + "					 , A.UNIQUE_ID_U AS UNIQUE_ID_U \n"
                        + "					 , A.MAGAZINE_ID_U AS MAGAZINE_ID_U \n"
                        + "					 , B.OPER AS OPER \n"
                        + "					 , ROW_NUMBER () OVER (PARTITION BY A.RES_ID, A.LOT_ID, B.OPER ORDER BY A.RES_ID ASC, A.LOT_ID ASC, B.OPER ASC, A.RFID_START_TIME ASC) AS STATUS_IDX \n"
                        + "					 , A.RFID_START_TIME AS RFID_START_TIME \n"
                        + "					 , B.MES_START_TIME AS MES_START_TIME \n"
                        + "					 , TO_CHAR(ROUND((TO_DATE(A.RFID_START_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(B.MES_START_TIME, 'YYYYMMDDHH24MISS')) * 86400, 1)) AS START_TIME_DIFF \n"
                        + "					 , A.RFID_START_FLAG AS RFID_START_FLAG \n"
                        + "					 , A.RFID_START_ERR_MSG AS RFID_START_ERR_MSG \n"
                        + "					 , A.RFID_START_ERR_FIELD AS RFID_START_ERR_FIELD \n"
                        + "					 , B.MES_START_FLAG AS MES_START_FLAG \n"
                        + "					 , CASE WHEN A.RFID_START_FLAG = 'SUCCESS' AND ROUND(((TO_DATE(A.RFID_START_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(B.MES_START_TIME, 'YYYYMMDDHH24MISS')) * 86400), 1) BETWEEN 0 AND 20  THEN 'OK' \n"
                        + "							ELSE ' ' \n"
                        + "					   END AS RFID_MES_START_COMPARE \n"
                        + "					 , ' ' AS RFID_ERASE_FLAG \n"
                        + "					 , ' ' AS RFID_ERASE_ERR_MSG \n"
                        + "					 , ' ' AS RFID_ERASE_ERR_FIELD \n"
                        + "					 , D.RFID_WRITE_FLAG AS RFID_WRITE_FLAG \n"
                        + "					 , D.RFID_WRITE_ERR_MSG AS RFID_WRITE_ERR_MSG \n"
                        + "					 , D.RFID_WRITE_ERR_FIELD AS RFID_WRITE_ERR_FIELD \n"
                        + "				  FROM (SELECT RES_ID AS RES_ID \n"
                        + "							 , LOT_ID AS LOT_ID \n"
                        + "							 , UNIQUE_ID_L AS UNIQUE_ID_L \n"
                        + "							 , MAGAZINE_ID_L AS MAGAZINE_ID_L \n"
                        + "							 , UNIQUE_ID_U AS UNIQUE_ID_U \n"
                        + "							 , MAGAZINE_ID_U AS MAGAZINE_ID_U \n"
                        + "							 , RFID_START_TIME AS RFID_START_TIME \n"
                        + "							 , RFID_START_FLAG AS RFID_START_FLAG \n"
                        + "							 , RFID_START_ERR_MSG AS RFID_START_ERR_MSG \n"
                        + "							 , RFID_START_ERR_FIELD AS RFID_START_ERR_FIELD \n"
                        + "						  FROM (SELECT RES_ID AS RES_ID \n"
                        + "									 , LOT_ID_1 AS LOT_ID \n"
                        + "									 , UNIQUE_ID_1 AS UNIQUE_ID_L \n"
                        + "									 , MAGAZINE_ID_1 AS MAGAZINE_ID_L \n"
                        + "									 , UNIQUE_ID_2 AS UNIQUE_ID_U \n"
                        + "									 , MAGAZINE_ID_2 AS MAGAZINE_ID_U \n"
                        + "									 , SYS_TIME AS RFID_START_TIME \n"
                        + "									 , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "												   WHEN '1' THEN 'FAIL' \n"
                        + "									   END AS RFID_START_FLAG \n"
                        + "									 , ERR_MSG AS RFID_START_ERR_MSG \n"
                        + "									 , ERR_FIELD_MSG AS RFID_START_ERR_FIELD \n"
                        + "								  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "								 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "								   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "								   AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "								   AND ANTENNA_PORT_1 = '1' \n"
                        + "								   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "								   AND LOT_ID_1 <> ' ' \n"
                        + "								   AND ANTENNA_PORT_2 = '2' \n"
                        + "								   AND MAGAZINE_ID_2 <> ' ' \n"
                        + "								   AND LOT_ID_2 = ' ' \n"
                        + "								   AND RES_ID LIKE 'M-A%' \n"
                        + "								   AND RES_ID LIKE '" + txtResourceID.Text + "%')) A \n"
                        + "					 , (SELECT START_RES_ID AS RES_ID \n"
                        + "							 , LOT_ID AS LOT_ID \n"
                        + "							 , OPER AS OPER \n"
                        + "							 , TRAN_TIME AS MES_START_TIME \n"
                        + "							 , CASE TRAN_CMF_19 WHEN 'RFID' THEN 'RFID' \n"
                        + "												ELSE 'MESCLIENT' \n"
                        + "							   END AS MES_START_FLAG \n"
                        + "						  FROM RWIPLOTHIS \n"
                        + "						 WHERE LOT_TYPE = 'W' \n"
                        + "						   AND TRAN_CODE = 'START' \n"
                        + "						   AND HIST_DEL_FLAG = ' ' \n"
                        + "						   AND OPER = 'A1000' \n"
                        + "						   AND TRAN_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "						   AND LOT_ID IN (SELECT LOT_ID AS LOT_ID \n"
                        + "						   					FROM (SELECT LOT_ID_1 AS LOT_ID \n"
                        + "													FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "												   WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "													 AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "													 AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "													 AND ANTENNA_PORT_1 = '1' \n"
                        + "												     AND MAGAZINE_ID_1 <> ' ' \n"
                        + "												     AND LOT_ID_1 <> ' ' \n"
                        + "												     AND ANTENNA_PORT_2 = '2' \n"
                        + "												     AND MAGAZINE_ID_2 <> ' ' \n"
                        + "												     AND LOT_ID_2 = ' ' \n"
                        + "								                     AND RES_ID LIKE 'M-A%' \n"
                        + "								                     AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "												   GROUP BY LOT_ID_1) \n"
                        + "						   				   GROUP BY LOT_ID)) B \n"
                        + "					 , (SELECT SYS_TIME AS RFID_WRITE_TIME \n"
                        + "							 , UNIQUE_ID_1 AS UNIQUE_ID \n"
                        + "							 , MAGAZINE_ID_1 AS MAGAZINE_ID \n"
                        + "							 , LOT_ID_1 AS LOT_ID \n"
                        + "							 , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "										   ELSE 'FAIL' \n"
                        + "							   END AS RFID_WRITE_FLAG \n"
                        + "							 , ERR_MSG AS RFID_WRITE_ERR_MSG \n"
                        + "							 , ERR_FIELD_MSG AS RFID_WRITE_ERR_FIELD \n"
                        + "						  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "						 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "						   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "						   AND UPPER(FUNCTION_NAME) = 'SEND_EIS_RFID_WRITE_LOT_ID_REQ' \n"
                        + "						   AND ANTENNA_PORT_1 = '2' \n"
                        + "						   AND UNIQUE_ID_1 <> ' ' \n"
                        + "						   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "						   AND LOT_ID_1 <> ' ' \n"
                        + "						   AND RES_ID LIKE 'M-A%' \n"
                        + "						   AND RES_ID LIKE '" + txtResourceID.Text + "%') D \n"
                        + "				 WHERE A.RES_ID = B.RES_ID(+) \n"
                        + "				   AND A.LOT_ID = B.LOT_ID(+) \n"
                        + "		   		   AND A.UNIQUE_ID_U = D.UNIQUE_ID(+) \n"
                        + "		   		   AND A.MAGAZINE_ID_U = D.MAGAZINE_ID(+) \n"
                        + "		   		   AND A.LOT_ID = D.LOT_ID(+)) A \n"
                        + "		 	 , (SELECT A.RES_ID AS RES_ID \n"
                        + "					 , A.LOT_ID AS LOT_ID \n"
                        + "					 , B.OPER AS OPER \n"
                        + "					 , ROW_NUMBER () OVER (PARTITION BY A.RES_ID, A.LOT_ID, B.OPER ORDER BY A.RES_ID ASC, A.LOT_ID ASC, B.OPER ASC, A.RFID_END_TIME ASC) AS STATUS_IDX \n"
                        + "					 , A.RFID_END_TIME AS RFID_END_TIME \n"
                        + "					 , B.MES_END_TIME AS MES_END_TIME \n"
                        + "					 , TO_CHAR(ROUND(((TO_DATE(A.RFID_END_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(B.MES_END_TIME, 'YYYYMMDDHH24MISS')) * 86400), 1)) AS END_TIME_DIFF \n"
                        + "					 , A.RFID_END_FLAG AS RFID_END_FLAG \n"
                        + "					 , A.RFID_END_ERR_MSG AS RFID_END_ERR_MSG \n"
                        + "					 , A.RFID_END_ERR_FIELD AS RFID_END_ERR_FIELD \n"
                        + "					 , B.MES_END_FLAG AS MES_END_FLAG \n"
                        + "					 , CASE WHEN A.RFID_END_FLAG = 'SUCCESS' AND ROUND(((TO_DATE(A.RFID_END_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(B.MES_END_TIME, 'YYYYMMDDHH24MISS')) * 86400), 1) BETWEEN 0 AND 20 THEN 'OK' \n"
                        + "					 		ELSE ' ' \n"
                        + "					   END AS RFID_MES_END_COMPARE \n"
                        + "				  FROM (SELECT RES_ID AS RES_ID \n"
                        + "							 , LOT_ID AS LOT_ID \n"
                        + "							 , RFID_END_TIME AS RFID_END_TIME \n"
                        + "							 , RFID_END_FLAG AS RFID_END_FLAG \n"
                        + "							 , RFID_END_ERR_MSG AS RFID_END_ERR_MSG \n"
                        + "							 , RFID_END_ERR_FIELD AS RFID_END_ERR_FIELD \n"
                        + "						  FROM (SELECT RES_ID AS RES_ID \n"
                        + "									 , LOT_ID_1 AS LOT_ID \n"
                        + "									 , SYS_TIME AS RFID_END_TIME \n"
                        + "									 , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "												   WHEN '1' THEN 'FAIL' \n"
                        + "									   END AS RFID_END_FLAG \n"
                        + "									 , ERR_MSG AS RFID_END_ERR_MSG \n"
                        + "									 , ERR_FIELD_MSG AS RFID_END_ERR_FIELD \n"
                        + "								  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "								 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "								   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "								   AND UPPER(FUNCTION_NAME) = 'RFID_EIS_END_MAGAZINE_REQ' \n"
                        + "								   AND ANTENNA_PORT_1 = '2' \n"
                        + "								   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "								   AND LOT_ID_1 <> ' ' \n"
                        + "								   AND ANTENNA_PORT_2 = '0' \n"
                        + "								   AND MAGAZINE_ID_2 = ' ' \n"
                        + "								   AND LOT_ID_2 = ' ' \n"
                        + "						           AND RES_ID LIKE 'M-A%' \n"
                        + "						           AND RES_ID LIKE '" + txtResourceID.Text + "%')) A \n"
                        + "					 , (SELECT END_RES_ID AS RES_ID \n"
                        + "							 , LOT_ID AS LOT_ID \n"
                        + "							 , OLD_OPER AS OPER \n"
                        + "							 , TRAN_TIME AS MES_END_TIME \n"
                        + "							 , CASE TRAN_CMF_19 WHEN 'RFID' THEN 'RFID' \n"
                        + "												ELSE 'MESCLIENT' \n"
                        + "							   END AS MES_END_FLAG \n"
                        + "						  FROM RWIPLOTHIS \n"
                        + "						 WHERE LOT_TYPE = 'W' \n"
                        + "						   AND TRAN_CODE = 'END' \n"
                        + "						   AND HIST_DEL_FLAG = ' ' \n"
                        + "						   AND OLD_OPER = 'A1000' \n"
                        + "						   AND TRAN_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "						   AND LOT_ID IN (SELECT LOT_ID AS LOT_ID \n"
                        + "										    FROM (SELECT LOT_ID_1 AS LOT_ID \n"
                        + "												    FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "												   WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "												     AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "												     AND UPPER(FUNCTION_NAME) = 'RFID_EIS_END_MAGAZINE_REQ' \n"
                        + "												     AND ANTENNA_PORT_1 = '2' \n"
                        + "												     AND MAGAZINE_ID_1 <> ' ' \n"
                        + "												     AND LOT_ID_1 <> ' ' \n"
                        + "												     AND ANTENNA_PORT_2 = '0' \n"
                        + "												     AND MAGAZINE_ID_2 = ' ' \n"
                        + "												     AND LOT_ID_2 = ' ' \n"
                        + "						                             AND RES_ID LIKE 'M-A%' \n"
                        + "						                             AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "												   GROUP BY LOT_ID_1))) B \n"
                        + "				 WHERE A.RES_ID = B.RES_ID(+) \n"
                        + "				   AND A.LOT_ID = B.LOT_ID(+)) B \n"
                        + "		 WHERE A.RES_ID = B.RES_ID(+) \n"
                        + "		   AND A.LOT_ID = B.LOT_ID(+) \n"
                        + "		   AND A.OPER = B.OPER(+) \n"
                        + "		   AND A.STATUS_IDX = B.STATUS_IDX(+) \n"
                        + "		 ORDER BY A.RES_ID ASC, A.LOT_ID ASC, A.RFID_START_TIME ASC, B.RFID_END_TIME ASC \n";
                }
                else if (rb04.Checked == true)
                {
                    // M/K공정
                    QRY = "SELECT A.RES_ID AS RES_ID \n"
                        + "		 , A.LOT_ID AS LOT_ID \n"
                        + "		 , A.OPER AS OPER \n"
                        + "		 , A.RFID_START_TIME AS RFID_START_TIME \n"
                        + "		 , A.MES_START_TIME AS MES_START_TIME \n"
                        + "		 , A.START_TIME_DIFF AS START_TIME_DIFF \n"
                        + "		 , A.RFID_START_FLAG AS RFID_START_FLAG \n"
                        + "		 , A.MES_START_FLAG AS MES_START_FLAG \n"
                        + "		 , A.RFID_MES_START_COMPARE AS RFID_MES_START_COMPARE \n"
                        + "		 , B.RFID_END_TIME AS RFID_END_TIME \n"
                        + "		 , B.MES_END_TIME AS MES_END_TIME \n"
                        + "		 , B.END_TIME_DIFF AS END_TIME_DIFF \n"
                        + "		 , B.RFID_END_FLAG AS RFID_END_FLAG \n"
                        + "		 , B.MES_END_FLAG AS MES_END_FLAG \n"
                        + "		 , B.RFID_MES_END_COMPARE AS RFID_MES_END_COMPARE \n"
                        + "		 , A.UNIQUE_ID_L AS UNIQUE_ID_L \n"
                        + "		 , A.MAGAZINE_ID_L AS MAGAZINE_ID_L \n"
                        + "		 , A.RFID_ERASE_FLAG AS RFID_ERASE_FLAG \n"
                        + "		 , A.UNIQUE_ID_U AS UNIQUE_ID_U \n"
                        + "		 , A.MAGAZINE_ID_U AS MAGAZINE_ID_U \n"
                        + "		 , A.RFID_WRITE_FLAG AS RFID_WRITE_FLAG \n"
                        + "	  FROM (SELECT A.RES_ID AS RES_ID \n"
                        + "				 , A.LOT_ID AS LOT_ID \n"
                        + "				 , A.UNIQUE_ID_L AS UNIQUE_ID_L \n"
                        + "				 , A.MAGAZINE_ID_L AS MAGAZINE_ID_L \n"
                        + "				 , A.UNIQUE_ID_U AS UNIQUE_ID_U \n"
                        + "				 , A.MAGAZINE_ID_U AS MAGAZINE_ID_U \n"
                        + "				 , B.OPER AS OPER \n"
                        + "				 , ROW_NUMBER () OVER (PARTITION BY A.RES_ID, A.LOT_ID, B.OPER ORDER BY A.RES_ID ASC, A.LOT_ID ASC, B.OPER ASC, A.RFID_START_TIME ASC) AS STATUS_IDX \n"
                        + "				 , A.RFID_START_TIME AS RFID_START_TIME \n"
                        + "				 , B.MES_START_TIME AS MES_START_TIME \n"
                        + "				 , TO_CHAR(ROUND((TO_DATE(A.RFID_START_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(B.MES_START_TIME, 'YYYYMMDDHH24MISS')) * 86400, 1)) AS START_TIME_DIFF \n"
                        + "				 , A.RFID_START_FLAG AS RFID_START_FLAG \n"
                        + "				 , A.RFID_START_ERR_MSG AS RFID_START_ERR_MSG \n"
                        + "				 , A.RFID_START_ERR_FIELD AS RFID_START_ERR_FIELD \n"
                        + "				 , B.MES_START_FLAG AS MES_START_FLAG \n"
                        + "				 , CASE WHEN A.RFID_START_FLAG = 'SUCCESS' AND ROUND(((TO_DATE(A.RFID_START_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(B.MES_START_TIME, 'YYYYMMDDHH24MISS')) * 86400), 1) BETWEEN 0 AND 20  THEN 'OK' \n"
                        + "						ELSE ' ' \n"
                        + "				   END AS RFID_MES_START_COMPARE \n"
                        + "				 , C.RFID_ERASE_FLAG AS RFID_ERASE_FLAG \n"
                        + "				 , C.RFID_ERASE_ERR_MSG AS RFID_ERASE_ERR_MSG \n"
                        + "				 , C.RFID_ERASE_ERR_FIELD AS RFID_ERASE_ERR_FIELD \n"
                        + "				 , D.RFID_WRITE_FLAG AS RFID_WRITE_FLAG \n"
                        + "				 , D.RFID_WRITE_ERR_MSG AS RFID_WRITE_ERR_MSG \n"
                        + "				 , D.RFID_WRITE_ERR_FIELD AS RFID_WRITE_ERR_FIELD \n"
                        + "			  FROM (SELECT RES_ID AS RES_ID \n"
                        + "						 , LOT_ID AS LOT_ID \n"
                        + "						 , UNIQUE_ID_L AS UNIQUE_ID_L \n"
                        + "						 , MAGAZINE_ID_L AS MAGAZINE_ID_L \n"
                        + "						 , UNIQUE_ID_U AS UNIQUE_ID_U \n"
                        + "						 , MAGAZINE_ID_U AS MAGAZINE_ID_U \n"
                        + "						 , RFID_START_TIME AS RFID_START_TIME \n"
                        + "						 , RFID_START_FLAG AS RFID_START_FLAG \n"
                        + "						 , RFID_START_ERR_MSG AS RFID_START_ERR_MSG \n"
                        + "						 , RFID_START_ERR_FIELD AS RFID_START_ERR_FIELD \n"
                        + "					  FROM (SELECT RES_ID AS RES_ID \n"
                        + "								 , LOT_ID_2 AS LOT_ID \n"
                        + "								 , UNIQUE_ID_1 AS UNIQUE_ID_L \n"
                        + "								 , MAGAZINE_ID_1 AS MAGAZINE_ID_L \n"
                        + "								 , UNIQUE_ID_2 AS UNIQUE_ID_U \n"
                        + "								 , MAGAZINE_ID_2 AS MAGAZINE_ID_U \n"
                        + "								 , SYS_TIME AS RFID_START_TIME \n"
                        + "								 , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "											   WHEN '1' THEN 'FAIL' \n"
                        + "								   END AS RFID_START_FLAG \n"
                        + "								 , ERR_MSG AS RFID_START_ERR_MSG \n"
                        + "								 , ERR_FIELD_MSG AS RFID_START_ERR_FIELD \n"
                        + "							  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "							 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "							   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "							   AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "							   AND ANTENNA_PORT_1 = '2' \n"
                        + "							   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "							   AND LOT_ID_1 = ' ' \n"
                        + "							   AND ANTENNA_PORT_2 = '1' \n"
                        + "							   AND MAGAZINE_ID_2 <> ' ' \n"
                        + "							   AND LOT_ID_2 <> ' ' \n"
                        + "							   AND RES_ID LIKE 'FLM%' \n"
                        + "                            AND RES_ID LIKE '" + txtResourceID.Text + "%')) A \n"
                        + "				 , (SELECT START_RES_ID AS RES_ID \n"
                        + "						 , LOT_ID AS LOT_ID \n"
                        + "						 , OPER AS OPER \n"
                        + "						 , TRAN_TIME AS MES_START_TIME \n"
                        + "						 , CASE TRAN_CMF_19 WHEN 'RFID' THEN 'RFID' \n"
                        + "											ELSE 'MESCLIENT' \n"
                        + "						   END AS MES_START_FLAG \n"
                        + "					  FROM RWIPLOTHIS \n"
                        + "					 WHERE LOT_TYPE = 'W' \n"
                        + "					   AND TRAN_CODE = 'START' \n"
                        + "					   AND HIST_DEL_FLAG = ' ' \n"
                        + "					   AND OPER IN ('A1110', 'A1150', 'A1500') \n"
                        + "					   AND TRAN_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "					   AND LOT_ID IN (SELECT LOT_ID_2 AS LOT_ID \n"
                        + "										FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "									   WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "									     AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "									     AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "									     AND ANTENNA_PORT_1 = '2' \n"
                        + "									     AND MAGAZINE_ID_1 <> ' ' \n"
                        + "									     AND LOT_ID_1 = ' ' \n"
                        + "									     AND ANTENNA_PORT_2 = '1' \n"
                        + "									     AND MAGAZINE_ID_2 <> ' ' \n"
                        + "									     AND LOT_ID_2 <> ' ' \n"
                        + "							             AND RES_ID LIKE 'FLM%' \n"
                        + "                                      AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "									   GROUP BY LOT_ID_2)) B \n"
                        + "				 , (SELECT SYS_TIME AS RFID_WRITE_TIME \n"
                        + "						 , UNIQUE_ID_1 AS UNIQUE_ID \n"
                        + "						 , MAGAZINE_ID_1 AS MAGAZINE_ID \n"
                        + "						 , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "									   ELSE 'FAIL' \n"
                        + "						   END AS RFID_ERASE_FLAG \n"
                        + "						 , ERR_MSG AS RFID_ERASE_ERR_MSG \n"
                        + "						 , ERR_FIELD_MSG AS RFID_ERASE_ERR_FIELD \n"
                        + "					  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "					 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "					   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "					   AND UPPER(FUNCTION_NAME) = 'SEND_EIS_RFID_WRITE_LOT_ID_REQ' \n"
                        + "					   AND ANTENNA_PORT_1 = '1' \n"
                        + "					   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "					   AND LOT_ID_1 = ' ' \n"
                        + "					   AND RES_ID LIKE 'FLM%' \n"
                        + "                    AND RES_ID LIKE '" + txtResourceID.Text + "%') C \n"
                        + "				 , (SELECT SYS_TIME AS RFID_WRITE_TIME \n"
                        + "						 , UNIQUE_ID_1 AS UNIQUE_ID \n"
                        + "						 , MAGAZINE_ID_1 AS MAGAZINE_ID \n"
                        + "						 , LOT_ID_1 AS LOT_ID \n"
                        + "						 , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "									   ELSE 'FAIL' \n"
                        + "						   END AS RFID_WRITE_FLAG \n"
                        + "						 , ERR_MSG AS RFID_WRITE_ERR_MSG \n"
                        + "						 , ERR_FIELD_MSG AS RFID_WRITE_ERR_FIELD \n"
                        + "					  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "					 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "					   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "					   AND UPPER(FUNCTION_NAME) = 'SEND_EIS_RFID_WRITE_LOT_ID_REQ' \n"
                        + "					   AND ANTENNA_PORT_1 = '2' \n"
                        + "					   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "					   AND LOT_ID_1 <> ' ' \n"
                        + "					   AND RES_ID LIKE 'FLM%' \n"
                        + "                    AND RES_ID LIKE '" + txtResourceID.Text + "%') D \n"
                        + "			 WHERE A.RES_ID = B.RES_ID(+) \n"
                        + "			   AND A.LOT_ID = B.LOT_ID(+) \n"
                        + "			   AND A.UNIQUE_ID_L = C.UNIQUE_ID(+) \n"
                        + "	   		   AND A.MAGAZINE_ID_L = C.MAGAZINE_ID(+) \n"
                        + "	   		   AND A.UNIQUE_ID_U = D.UNIQUE_ID(+) \n"
                        + "	   		   AND A.MAGAZINE_ID_U = D.MAGAZINE_ID(+)) A \n"
                        + "	 	 , (SELECT A.RES_ID AS RES_ID \n"
                        + "				 , A.LOT_ID AS LOT_ID \n"
                        + "				 , B.OPER AS OPER \n"
                        + "				 , ROW_NUMBER () OVER (PARTITION BY A.RES_ID, A.LOT_ID, B.OPER ORDER BY A.RES_ID ASC, A.LOT_ID ASC, B.OPER ASC, A.RFID_END_TIME ASC) AS STATUS_IDX \n"
                        + "				 , A.RFID_END_TIME AS RFID_END_TIME \n"
                        + "				 , B.MES_END_TIME AS MES_END_TIME \n"
                        + "				 , TO_CHAR(ROUND(((TO_DATE(A.RFID_END_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(B.MES_END_TIME, 'YYYYMMDDHH24MISS')) * 86400), 1)) AS END_TIME_DIFF \n"
                        + "				 , A.RFID_END_FLAG AS RFID_END_FLAG \n"
                        + "				 , A.RFID_END_ERR_MSG AS RFID_END_ERR_MSG \n"
                        + "				 , A.RFID_END_ERR_FIELD AS RFID_END_ERR_FIELD \n"
                        + "				 , B.MES_END_FLAG AS MES_END_FLAG \n"
                        + "				 , CASE WHEN A.RFID_END_FLAG = 'SUCCESS' AND ROUND(((TO_DATE(A.RFID_END_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(B.MES_END_TIME, 'YYYYMMDDHH24MISS')) * 86400), 1) BETWEEN 0 AND 20 THEN 'OK' \n"
                        + "				 		ELSE ' ' \n"
                        + "				   END AS RFID_MES_END_COMPARE \n"
                        + "			  FROM (SELECT RES_ID AS RES_ID \n"
                        + "						 , LOT_ID AS LOT_ID \n"
                        + "						 , RFID_END_TIME AS RFID_END_TIME \n"
                        + "						 , RFID_END_FLAG AS RFID_END_FLAG \n"
                        + "						 , RFID_END_ERR_MSG AS RFID_END_ERR_MSG \n"
                        + "						 , RFID_END_ERR_FIELD AS RFID_END_ERR_FIELD \n"
                        + "					  FROM (SELECT RES_ID AS RES_ID \n"
                        + "								 , LOT_ID_1 AS LOT_ID \n"
                        + "								 , SYS_TIME AS RFID_END_TIME \n"
                        + "								 , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "											   WHEN '1' THEN 'FAIL' \n"
                        + "								   END AS RFID_END_FLAG \n"
                        + "								 , ERR_MSG AS RFID_END_ERR_MSG \n"
                        + "								 , ERR_FIELD_MSG AS RFID_END_ERR_FIELD \n"
                        + "							  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "							 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "							   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "							   AND UPPER(FUNCTION_NAME) = 'RFID_EIS_END_MAGAZINE_REQ' \n"
                        + "							   AND ANTENNA_PORT_1 = '2' \n"
                        + "							   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "							   AND LOT_ID_1 <> ' ' \n"
                        + "							   AND ANTENNA_PORT_2 = '0' \n"
                        + "							   AND MAGAZINE_ID_2 = ' ' \n"
                        + "							   AND LOT_ID_2 = ' ' \n"
                        + "							   AND RES_ID LIKE 'FLM%' \n"
                        + "                            AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "							UNION ALL \n"
                        + "							SELECT RES_ID AS RES_ID \n"
                        + "								 , LOT_ID_1 AS LOT_ID \n"
                        + "								 , SYS_TIME AS RFID_END_TIME \n"
                        + "								 , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "											   WHEN '1' THEN 'FAIL' \n"
                        + "								   END AS RFID_END_FLAG \n"
                        + "								 , ERR_MSG AS RFID_END_ERR_MSG \n"
                        + "								 , ERR_FIELD_MSG AS RFID_END_ERR_FIELD \n"
                        + "							  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "							 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "							   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "							   AND UPPER(FUNCTION_NAME) = 'RFID_EIS_END_MAGAZINE_REQ' \n"
                        + "							   AND ANTENNA_PORT_1 = '2' \n"
                        + "							   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "							   AND LOT_ID_1 <> ' ' \n"
                        + "							   AND ANTENNA_PORT_2 = '1' \n"
                        + "							   AND MAGAZINE_ID_2 <> ' ' \n"
                        + "							   AND LOT_ID_2 = ' ' \n"
                        + "							   AND RES_ID LIKE 'FLM%' \n"
                        + "                            AND RES_ID LIKE '" + txtResourceID.Text + "%')) A \n"
                        + "				 , (SELECT END_RES_ID AS RES_ID \n"
                        + "						 , LOT_ID AS LOT_ID \n"
                        + "						 , OLD_OPER AS OPER \n"
                        + "						 , TRAN_TIME AS MES_END_TIME \n"
                        + "						 , CASE TRAN_CMF_19 WHEN 'RFID' THEN 'RFID' \n"
                        + "											ELSE 'MESCLIENT' \n"
                        + "						   END AS MES_END_FLAG \n"
                        + "					  FROM RWIPLOTHIS \n"
                        + "					 WHERE LOT_TYPE = 'W' \n"
                        + "					   AND TRAN_CODE = 'END' \n"
                        + "					   AND HIST_DEL_FLAG = ' ' \n"
                        + "					   AND OLD_OPER IN ('A1110', 'A1150', 'A1500') \n"
                        + "					   AND TRAN_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "					   AND LOT_ID IN (SELECT LOT_ID AS LOT_ID \n"
                        + "									    FROM (SELECT LOT_ID_1 AS LOT_ID \n"
                        + "											    FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "											   WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "											     AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "											     AND UPPER(FUNCTION_NAME) = 'RFID_EIS_END_MAGAZINE_REQ' \n"
                        + "											     AND ANTENNA_PORT_1 = '2' \n"
                        + "											     AND MAGAZINE_ID_1 <> ' ' \n"
                        + "											     AND LOT_ID_1 <> ' ' \n"
                        + "											     AND ANTENNA_PORT_2 = '0' \n"
                        + "											     AND MAGAZINE_ID_2 = ' ' \n"
                        + "											     AND LOT_ID_2 = ' ' \n"
                        + "							                     AND RES_ID LIKE 'FLM%' \n"
                        + "                                              AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "											  UNION ALL \n"
                        + "											  SELECT LOT_ID_1 AS LOT_ID \n"
                        + "											    FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "											   WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "											     AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "											     AND UPPER(FUNCTION_NAME) = 'RFID_EIS_END_MAGAZINE_REQ' \n"
                        + "											     AND ANTENNA_PORT_1 = '2' \n"
                        + "											     AND MAGAZINE_ID_1 <> ' ' \n"
                        + "											     AND LOT_ID_1 <> ' ' \n"
                        + "											     AND ANTENNA_PORT_2 = '1' \n"
                        + "											     AND MAGAZINE_ID_2 <> ' ' \n"
                        + "											     AND LOT_ID_2 = ' ' \n"
                        + "							                     AND RES_ID LIKE 'FLM%' \n"
                        + "                                              AND RES_ID LIKE '" + txtResourceID.Text + "%') \n"
                        + "									   GROUP BY LOT_ID)) B \n"
                        + "			 WHERE A.RES_ID = B.RES_ID(+) \n"
                        + "			   AND A.LOT_ID = B.LOT_ID(+)) B \n"
                        + "	 WHERE A.RES_ID = B.RES_ID(+) \n"
                        + "	   AND A.LOT_ID = B.LOT_ID(+) \n"
                        + "	   AND A.OPER = B.OPER(+) \n"
                        + "	   AND A.STATUS_IDX = B.STATUS_IDX(+) \n"
                        + "	 ORDER BY A.RES_ID ASC, A.LOT_ID ASC, A.RFID_START_TIME ASC, B.RFID_END_TIME ASC";
                }
                else if (rb05.Checked == true)
                {
                    // SBA공정
                    QRY = "SELECT A.RES_ID AS RES_ID \n"
                        + "		 , A.LOT_ID AS LOT_ID \n"
                        + "		 , A.OPER AS OPER \n"
                        + "		 , A.RFID_START_TIME AS RFID_START_TIME \n"
                        + "		 , A.MES_START_TIME AS MES_START_TIME \n"
                        + "		 , A.START_TIME_DIFF AS START_TIME_DIFF \n"
                        + "		 , A.RFID_START_FLAG AS RFID_START_FLAG \n"
                        + "		 , A.MES_START_FLAG AS MES_START_FLAG \n"
                        + "		 , A.RFID_MES_START_COMPARE AS RFID_MES_START_COMPARE \n"
                        + "		 , B.RFID_END_TIME AS RFID_END_TIME \n"
                        + "		 , B.MES_END_TIME AS MES_END_TIME \n"
                        + "		 , B.END_TIME_DIFF AS END_TIME_DIFF \n"
                        + "		 , B.RFID_END_FLAG AS RFID_END_FLAG \n"
                        + "		 , B.MES_END_FLAG AS MES_END_FLAG \n"
                        + "		 , B.RFID_MES_END_COMPARE AS RFID_MES_END_COMPARE \n"
                        + "		 , A.UNIQUE_ID_L AS UNIQUE_ID_L \n"
                        + "		 , A.MAGAZINE_ID_L AS MAGAZINE_ID_L \n"
                        + "		 , A.RFID_ERASE_FLAG AS RFID_ERASE_FLAG \n"
                        + "		 , A.UNIQUE_ID_U AS UNIQUE_ID_U \n"
                        + "		 , A.MAGAZINE_ID_U AS MAGAZINE_ID_U \n"
                        + "		 , A.RFID_WRITE_FLAG AS RFID_WRITE_FLAG \n"
                        + "	  FROM (SELECT A.RES_ID AS RES_ID \n"
                        + "				 , A.LOT_ID AS LOT_ID \n"
                        + "				 , A.UNIQUE_ID_L AS UNIQUE_ID_L \n"
                        + "				 , A.MAGAZINE_ID_L AS MAGAZINE_ID_L \n"
                        + "				 , A.UNIQUE_ID_U AS UNIQUE_ID_U \n"
                        + "				 , A.MAGAZINE_ID_U AS MAGAZINE_ID_U \n"
                        + "				 , B.OPER AS OPER \n"
                        + "				 , ROW_NUMBER () OVER (PARTITION BY A.RES_ID, A.LOT_ID, B.OPER ORDER BY A.RES_ID ASC, A.LOT_ID ASC, B.OPER ASC, A.RFID_START_TIME ASC) AS STATUS_IDX \n"
                        + "				 , A.RFID_START_TIME AS RFID_START_TIME \n"
                        + "				 , B.MES_START_TIME AS MES_START_TIME \n"
                        + "				 , TO_CHAR(ROUND((TO_DATE(A.RFID_START_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(B.MES_START_TIME, 'YYYYMMDDHH24MISS')) * 86400, 1)) AS START_TIME_DIFF \n"
                        + "				 , A.RFID_START_FLAG AS RFID_START_FLAG \n"
                        + "				 , A.RFID_START_ERR_MSG AS RFID_START_ERR_MSG \n"
                        + "				 , A.RFID_START_ERR_FIELD AS RFID_START_ERR_FIELD \n"
                        + "				 , B.MES_START_FLAG AS MES_START_FLAG \n"
                        + "				 , CASE WHEN A.RFID_START_FLAG = 'SUCCESS' AND ROUND(((TO_DATE(A.RFID_START_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(B.MES_START_TIME, 'YYYYMMDDHH24MISS')) * 86400), 1) BETWEEN 0 AND 20  THEN 'OK' \n"
                        + "						ELSE ' ' \n"
                        + "				   END AS RFID_MES_START_COMPARE \n"
                        + "				 , C.RFID_ERASE_FLAG AS RFID_ERASE_FLAG \n"
                        + "				 , C.RFID_ERASE_ERR_MSG AS RFID_ERASE_ERR_MSG \n"
                        + "				 , C.RFID_ERASE_ERR_FIELD AS RFID_ERASE_ERR_FIELD \n"
                        + "				 , D.RFID_WRITE_FLAG AS RFID_WRITE_FLAG \n"
                        + "				 , D.RFID_WRITE_ERR_MSG AS RFID_WRITE_ERR_MSG \n"
                        + "				 , D.RFID_WRITE_ERR_FIELD AS RFID_WRITE_ERR_FIELD \n"
                        + "			  FROM (SELECT RES_ID AS RES_ID \n"
                        + "						 , LOT_ID AS LOT_ID \n"
                        + "						 , UNIQUE_ID_L AS UNIQUE_ID_L \n"
                        + "						 , MAGAZINE_ID_L AS MAGAZINE_ID_L \n"
                        + "						 , UNIQUE_ID_U AS UNIQUE_ID_U \n"
                        + "						 , MAGAZINE_ID_U AS MAGAZINE_ID_U \n"
                        + "						 , RFID_START_TIME AS RFID_START_TIME \n"
                        + "						 , RFID_START_FLAG AS RFID_START_FLAG \n"
                        + "						 , RFID_START_ERR_MSG AS RFID_START_ERR_MSG \n"
                        + "						 , RFID_START_ERR_FIELD AS RFID_START_ERR_FIELD \n"
                        + "					  FROM (SELECT RES_ID AS RES_ID \n"
                        + "								 , LOT_ID_1 AS LOT_ID \n"
                        + "								 , UNIQUE_ID_1 AS UNIQUE_ID_L \n"
                        + "								 , MAGAZINE_ID_1 AS MAGAZINE_ID_L \n"
                        + "								 , UNIQUE_ID_2 AS UNIQUE_ID_U \n"
                        + "								 , MAGAZINE_ID_2 AS MAGAZINE_ID_U \n"
                        + "								 , SYS_TIME AS RFID_START_TIME \n"
                        + "								 , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "											   WHEN '1' THEN 'FAIL' \n"
                        + "								   END AS RFID_START_FLAG \n"
                        + "								 , ERR_MSG AS RFID_START_ERR_MSG \n"
                        + "								 , ERR_FIELD_MSG AS RFID_START_ERR_FIELD \n"
                        + "							  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "							 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "							   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "							   AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "							   AND ANTENNA_PORT_1 = '1' \n"
                        + "							   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "							   AND LOT_ID_1 <> ' ' \n"
                        + "							   AND ANTENNA_PORT_2 = '0' \n"
                        + "							   AND MAGAZINE_ID_2 = ' ' \n"
                        + "							   AND LOT_ID_2 = ' ' \n"
                        + "							   AND RES_ID LIKE 'SBA%' \n"
                        + "                            AND RES_ID LIKE '" + txtResourceID.Text + "%')) A \n"
                        + "				 , (SELECT START_RES_ID AS RES_ID \n"
                        + "						 , LOT_ID AS LOT_ID \n"
                        + "						 , OPER AS OPER \n"
                        + "						 , TRAN_TIME AS MES_START_TIME \n"
                        + "						 , CASE WHEN TRAN_COMMENT LIKE 'EIS Start Lot%' THEN 'EIS(RFID)' \n"
                        + "								ELSE 'MESCLIENT' \n"
                        + "						   END AS MES_START_FLAG \n"
                        + "					  FROM RWIPLOTHIS \n"
                        + "					 WHERE LOT_TYPE = 'W' \n"
                        + "					   AND TRAN_CODE = 'START' \n"
                        + "					   AND HIST_DEL_FLAG = ' ' \n"
                        + "					   AND OPER = 'A1300' \n"
                        + "					   AND TRAN_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "					   AND LOT_ID IN (SELECT LOT_ID AS LOT_ID \n"
                        + "										FROM (SELECT LOT_ID_1 AS LOT_ID \n"
                        + "												FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "											   WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "												 AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "												 AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "												 AND ANTENNA_PORT_1 = '1' \n"
                        + "												 AND MAGAZINE_ID_1 <> ' ' \n"
                        + "												 AND LOT_ID_1 <> ' ' \n"
                        + "												 AND ANTENNA_PORT_2 = '0' \n"
                        + "												 AND MAGAZINE_ID_2 = ' ' \n"
                        + "												 AND LOT_ID_2 = ' ' \n"
                        + "							                     AND RES_ID LIKE 'SBA%' \n"
                        + "                                              AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "											   GROUP BY LOT_ID_1) \n"
                        + "									   GROUP BY LOT_ID)) B \n"
                        + "				 , (SELECT SYS_TIME AS RFID_WRITE_TIME \n"
                        + "						 , UNIQUE_ID_1 AS UNIQUE_ID \n"
                        + "						 , MAGAZINE_ID_1 AS MAGAZINE_ID \n"
                        + "						 , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "									   ELSE 'FAIL' \n"
                        + "						   END AS RFID_ERASE_FLAG \n"
                        + "						 , ERR_MSG AS RFID_ERASE_ERR_MSG \n"
                        + "						 , ERR_FIELD_MSG AS RFID_ERASE_ERR_FIELD \n"
                        + "					  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "					 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "					   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "					   AND UPPER(FUNCTION_NAME) = 'SEND_EIS_RFID_WRITE_LOT_ID_REQ' \n"
                        + "					   AND ANTENNA_PORT_1 = '1' \n"
                        + "					   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "					   AND LOT_ID_1 = ' ' \n"
                        + "					   AND RES_ID LIKE 'SBA%' \n"
                        + "                    AND RES_ID LIKE '" + txtResourceID.Text + "%') C \n"
                        + "				 , (SELECT SYS_TIME AS RFID_WRITE_TIME \n"
                        + "						 , UNIQUE_ID_1 AS UNIQUE_ID \n"
                        + "						 , MAGAZINE_ID_1 AS MAGAZINE_ID \n"
                        + "						 , LOT_ID_1 AS LOT_ID \n"
                        + "						 , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "									   ELSE 'FAIL' \n"
                        + "						   END AS RFID_WRITE_FLAG \n"
                        + "						 , ERR_MSG AS RFID_WRITE_ERR_MSG \n"
                        + "						 , ERR_FIELD_MSG AS RFID_WRITE_ERR_FIELD \n"
                        + "					  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "					 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "					   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "					   AND UPPER(FUNCTION_NAME) = 'SEND_EIS_RFID_WRITE_LOT_ID_REQ' \n"
                        + "					   AND ANTENNA_PORT_1 = '2' \n"
                        + "					   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "					   AND LOT_ID_1 <> ' ' \n"
                        + "					   AND RES_ID LIKE 'SBA%' \n"
                        + "                    AND RES_ID LIKE '" + txtResourceID.Text + "%') D \n"
                        + "			 WHERE A.RES_ID = B.RES_ID(+) \n"
                        + "			   AND A.LOT_ID = B.LOT_ID(+) \n"
                        + "			   AND A.UNIQUE_ID_L = C.UNIQUE_ID(+) \n"
                        + "			   AND A.MAGAZINE_ID_L = C.MAGAZINE_ID(+) \n"
                        + "			   AND A.UNIQUE_ID_U = D.UNIQUE_ID(+) \n"
                        + "			   AND A.MAGAZINE_ID_U = D.MAGAZINE_ID(+)) A \n"
                        + "	 	 , (SELECT RES_ID AS RES_ID \n"
                        + "				 , LOT_ID AS LOT_ID \n"
                        + "				 , OPER AS OPER \n"
                        + "				 , 1  AS STATUS_IDX \n"
                        + "				 , ' ' AS RFID_END_TIME \n"
                        + "				 , MES_END_TIME AS MES_END_TIME \n"
                        + "				 , ' ' AS END_TIME_DIFF \n"
                        + "				 , ' ' AS RFID_END_FLAG \n"
                        + "				 , ' ' AS RFID_END_ERR_MSG \n"
                        + "				 , ' ' AS RFID_END_ERR_FIELD \n"
                        + "				 , MES_END_FLAG AS MES_END_FLAG \n"
                        + "				 , ' ' AS RFID_MES_END_COMPARE \n"
                        + "			  FROM (SELECT END_RES_ID AS RES_ID \n"
                        + "						 , LOT_ID AS LOT_ID \n"
                        + "						 , OLD_OPER AS OPER \n"
                        + "						 , TRAN_TIME AS MES_END_TIME \n"
                        + "						 , CASE WHEN TRAN_COMMENT LIKE 'EIS End Lot%' THEN 'EIS(RFID)' \n"
                        + "								ELSE 'MESCLIENT' \n"
                        + "						   END AS MES_END_FLAG \n"
                        + "					  FROM RWIPLOTHIS \n"
                        + "					 WHERE LOT_TYPE = 'W' \n"
                        + "					   AND TRAN_CODE = 'END' \n"
                        + "					   AND HIST_DEL_FLAG = ' ' \n"
                        + "					   AND OLD_OPER  = 'A1300' \n"
                        + "					   AND TRAN_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "					   AND LOT_ID IN (SELECT LOT_ID AS LOT_ID \n"
                        + "									    FROM (SELECT LOT_ID_1 AS LOT_ID \n"
                        + "											    FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "											   WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "											     AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "											     AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "											     AND ANTENNA_PORT_1 = '1' \n"
                        + "											     AND MAGAZINE_ID_1 <> ' ' \n"
                        + "											     AND LOT_ID_1 <> ' ' \n"
                        + "											     AND ANTENNA_PORT_2 = '0' \n"
                        + "											     AND MAGAZINE_ID_2 = ' ' \n"
                        + "											     AND LOT_ID_2 = ' ' \n"
                        + "					                             AND RES_ID LIKE 'SBA%' \n"
                        + "                                              AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "											   GROUP BY LOT_ID_1)))) B \n"
                        + "	 WHERE A.RES_ID = B.RES_ID(+) \n"
                        + "	   AND A.LOT_ID = B.LOT_ID(+) \n"
                        + "	   AND A.OPER = B.OPER(+) \n"
                        + "	   AND A.STATUS_IDX = B.STATUS_IDX(+) \n"
                        + "	 ORDER BY A.RES_ID ASC, A.LOT_ID ASC, A.RFID_START_TIME ASC, B.RFID_END_TIME ASC";
                }
                else if (rb06.Checked == true)
                {
                    // SST공정
                    QRY = "SELECT A.RES_ID AS RES_ID \n"
                        + "     , A.LOT_ID AS LOT_ID \n"
                        + "		, A.OPER AS OPER \n"
                        + "		, A.RFID_START_TIME AS RFID_START_TIME \n"
                        + "		, A.MES_START_TIME AS MES_START_TIME \n"
                        + "		, A.START_TIME_DIFF AS START_TIME_DIFF \n"
                        + "		, A.RFID_START_FLAG AS RFID_START_FLAG \n"
                        + "		, A.MES_START_FLAG AS MES_START_FLAG \n"
                        + "		, A.RFID_MES_START_COMPARE AS RFID_MES_START_COMPARE \n"
                        + "		, B.RFID_END_TIME AS RFID_END_TIME \n"
                        + "		, B.MES_END_TIME AS MES_END_TIME \n"
                        + "		, B.END_TIME_DIFF AS END_TIME_DIFF \n"
                        + "		, B.RFID_END_FLAG AS RFID_END_FLAG \n"
                        + "		, B.MES_END_FLAG AS MES_END_FLAG \n"
                        + "		, B.RFID_MES_END_COMPARE AS RFID_MES_END_COMPARE \n"
                        + "		, A.UNIQUE_ID_L AS UNIQUE_ID_L \n"
                        + "		, A.MAGAZINE_ID_L AS MAGAZINE_ID_L \n"
                        + "		, A.RFID_ERASE_FLAG AS RFID_ERASE_FLAG \n"
                        + "		, A.UNIQUE_ID_U AS UNIQUE_ID_U \n"
                        + "		, A.MAGAZINE_ID_U AS MAGAZINE_ID_U \n"
                        + "		, A.RFID_WRITE_FLAG AS RFID_WRITE_FLAG \n"
                        + "	 FROM (SELECT A.RES_ID AS RES_ID \n"
                        + "				, A.LOT_ID AS LOT_ID \n"
                        + "				, A.UNIQUE_ID_L AS UNIQUE_ID_L \n"
                        + "				, A.MAGAZINE_ID_L AS MAGAZINE_ID_L \n"
                        + "				, A.UNIQUE_ID_U AS UNIQUE_ID_U \n"
                        + "				, A.MAGAZINE_ID_U AS MAGAZINE_ID_U \n"
                        + "				, B.OPER AS OPER \n"
                        + "				, ROW_NUMBER () OVER (PARTITION BY A.RES_ID, A.LOT_ID, B.OPER ORDER BY A.RES_ID ASC, A.LOT_ID ASC, B.OPER ASC, A.RFID_START_TIME ASC) AS STATUS_IDX \n"
                        + "				, A.RFID_START_TIME AS RFID_START_TIME \n"
                        + "				, B.MES_START_TIME AS MES_START_TIME \n"
                        + "				, TO_CHAR(ROUND((TO_DATE(A.RFID_START_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(B.MES_START_TIME, 'YYYYMMDDHH24MISS')) * 86400, 1)) AS START_TIME_DIFF \n"
                        + "				, A.RFID_START_FLAG AS RFID_START_FLAG \n"
                        + "				, A.RFID_START_ERR_MSG AS RFID_START_ERR_MSG \n"
                        + "				, A.RFID_START_ERR_FIELD AS RFID_START_ERR_FIELD \n"
                        + "				, B.MES_START_FLAG AS MES_START_FLAG \n"
                        + "				, CASE WHEN A.RFID_START_FLAG = 'SUCCESS' AND ROUND(((TO_DATE(A.RFID_START_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(B.MES_START_TIME, 'YYYYMMDDHH24MISS')) * 86400), 1) BETWEEN 0 AND 20  THEN 'OK' \n"
                        + "					   ELSE ' ' \n"
                        + "				  END AS RFID_MES_START_COMPARE \n"
                        + "				, C.RFID_ERASE_FLAG AS RFID_ERASE_FLAG \n"
                        + "				, C.RFID_ERASE_ERR_MSG AS RFID_ERASE_ERR_MSG \n"
                        + "				, C.RFID_ERASE_ERR_FIELD AS RFID_ERASE_ERR_FIELD \n"
                        + "				, D.RFID_WRITE_FLAG AS RFID_WRITE_FLAG \n"
                        + "				, D.RFID_WRITE_ERR_MSG AS RFID_WRITE_ERR_MSG \n"
                        + "				, D.RFID_WRITE_ERR_FIELD AS RFID_WRITE_ERR_FIELD \n"
                        + "			 FROM (SELECT RES_ID AS RES_ID \n"
                        + "						, LOT_ID AS LOT_ID \n"
                        + "						, UNIQUE_ID_L AS UNIQUE_ID_L \n"
                        + "						, MAGAZINE_ID_L AS MAGAZINE_ID_L \n"
                        + "						, UNIQUE_ID_U AS UNIQUE_ID_U \n"
                        + "						, MAGAZINE_ID_U AS MAGAZINE_ID_U \n"
                        + "						, RFID_START_TIME AS RFID_START_TIME \n"
                        + "						, RFID_START_FLAG AS RFID_START_FLAG \n"
                        + "						, RFID_START_ERR_MSG AS RFID_START_ERR_MSG \n"
                        + "						, RFID_START_ERR_FIELD AS RFID_START_ERR_FIELD \n"
                        + "					 FROM (SELECT RES_ID AS RES_ID \n"
                        + "								, LOT_ID_1 AS LOT_ID \n"
                        + "								, UNIQUE_ID_1 AS UNIQUE_ID_L \n"
                        + "								, MAGAZINE_ID_1 AS MAGAZINE_ID_L \n"
                        + "								, UNIQUE_ID_2 AS UNIQUE_ID_U \n"
                        + "								, MAGAZINE_ID_2 AS MAGAZINE_ID_U \n"
                        + "								, SYS_TIME AS RFID_START_TIME \n"
                        + "								, CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "										      WHEN '1' THEN 'FAIL' \n"
                        + "								  END AS RFID_START_FLAG \n"
                        + "								, ERR_MSG AS RFID_START_ERR_MSG \n"
                        + "								, ERR_FIELD_MSG AS RFID_START_ERR_FIELD \n"
                        + "							 FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "							WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "							  AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "							  AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "							  AND ANTENNA_PORT_1 = '1' \n"
                        + "							  AND MAGAZINE_ID_1 <> ' ' \n"
                        + "							  AND LOT_ID_1 <> ' ' \n"
                        + "							  AND ANTENNA_PORT_2 = '0' \n"
                        + "							  AND MAGAZINE_ID_2 = ' ' \n"
                        + "							  AND LOT_ID_2 = ' ' \n"
                        + "							  AND RES_ID LIKE 'SST%' \n"
                        + "                           AND RES_ID LIKE '" + txtResourceID.Text + "%')) A \n"
                        + "				, (SELECT START_RES_ID AS RES_ID \n"
                        + "					    , LOT_ID AS LOT_ID \n"
                        + "						, OPER AS OPER \n"
                        + "						, TRAN_TIME AS MES_START_TIME \n"
                        + "						, CASE WHEN TRAN_COMMENT LIKE 'EIS Start Lot%' THEN 'EIS(RFID)' \n"
                        + "							   ELSE 'MESCLIENT' \n"
                        + "						  END AS MES_START_FLAG \n"
                        + "					 FROM RWIPLOTHIS \n"
                        + "					WHERE LOT_TYPE = 'W' \n"
                        + "					  AND TRAN_CODE = 'START' \n"
                        + "					  AND HIST_DEL_FLAG = ' ' \n"
                        + "					  AND OPER IN ('A1720', 'A1750') \n"
                        + "					  AND TRAN_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "					  AND LOT_ID IN (SELECT LOT_ID AS LOT_ID \n"
                        + "									   FROM (SELECT LOT_ID_1 AS LOT_ID \n"
                        + "											   FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "											  WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "												AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "												AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "												AND ANTENNA_PORT_1 = '1' \n"
                        + "												AND MAGAZINE_ID_1 <> ' ' \n"
                        + "												AND LOT_ID_1 <> ' ' \n"
                        + "												AND ANTENNA_PORT_2 = '0' \n"
                        + "												AND MAGAZINE_ID_2 = ' ' \n"
                        + "												AND LOT_ID_2 = ' ' \n"
                        + "							                    AND RES_ID LIKE 'SST%' \n"
                        + "                                             AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "											  GROUP BY LOT_ID_1) \n"
                        + "									   GROUP BY LOT_ID)) B \n"
                        + "				 , (SELECT SYS_TIME AS RFID_WRITE_TIME \n"
                        + "						 , UNIQUE_ID_1 AS UNIQUE_ID \n"
                        + "						 , MAGAZINE_ID_1 AS MAGAZINE_ID \n"
                        + "						 , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "									   ELSE 'FAIL' \n"
                        + "						   END AS RFID_ERASE_FLAG \n"
                        + "						 , ERR_MSG AS RFID_ERASE_ERR_MSG \n"
                        + "						 , ERR_FIELD_MSG AS RFID_ERASE_ERR_FIELD \n"
                        + "					  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "					 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "					   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "					   AND UPPER(FUNCTION_NAME) = 'SEND_EIS_RFID_WRITE_LOT_ID_REQ' \n"
                        + "					   AND ANTENNA_PORT_1 = '1' \n"
                        + "					   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "					   AND LOT_ID_1 = ' ' \n"
                        + "					   AND RES_ID LIKE 'SST%' \n"
                        + "                    AND RES_ID LIKE '" + txtResourceID.Text + "%') C \n"
                        + "				 , (SELECT SYS_TIME AS RFID_WRITE_TIME \n"
                        + "						 , UNIQUE_ID_1 AS UNIQUE_ID \n"
                        + "						 , MAGAZINE_ID_1 AS MAGAZINE_ID \n"
                        + "						 , LOT_ID_1 AS LOT_ID \n"
                        + "						 , CASE RESULT WHEN '0' THEN 'SUCCESS' \n"
                        + "									   ELSE 'FAIL' \n"
                        + "						   END AS RFID_WRITE_FLAG \n"
                        + "						 , ERR_MSG AS RFID_WRITE_ERR_MSG \n"
                        + "						 , ERR_FIELD_MSG AS RFID_WRITE_ERR_FIELD \n"
                        + "					  FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "					 WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "					   AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "					   AND UPPER(FUNCTION_NAME) = 'SEND_EIS_RFID_WRITE_LOT_ID_REQ' \n"
                        + "					   AND ANTENNA_PORT_1 = '2' \n"
                        + "					   AND MAGAZINE_ID_1 <> ' ' \n"
                        + "					   AND LOT_ID_1 <> ' ' \n"
                        + "					   AND RES_ID LIKE 'SST%' \n"
                        + "                    AND RES_ID LIKE '" + txtResourceID.Text + "%') D \n"
                        + "			 WHERE A.RES_ID = B.RES_ID(+) \n"
                        + "			   AND A.LOT_ID = B.LOT_ID(+) \n"
                        + "			   AND A.UNIQUE_ID_L = C.UNIQUE_ID(+) \n"
                        + "			   AND A.MAGAZINE_ID_L = C.MAGAZINE_ID(+) \n"
                        + "			   AND A.UNIQUE_ID_U = D.UNIQUE_ID(+) \n"
                        + "			   AND A.MAGAZINE_ID_U = D.MAGAZINE_ID(+)) A \n"
                        + "	 	 , (SELECT RES_ID AS RES_ID \n"
                        + "				 , LOT_ID AS LOT_ID \n"
                        + "				 , OPER AS OPER \n"
                        + "				 , 1  AS STATUS_IDX \n"
                        + "				 , ' ' AS RFID_END_TIME \n"
                        + "				 , MES_END_TIME AS MES_END_TIME \n"
                        + "				 , ' ' AS END_TIME_DIFF \n"
                        + "				 , ' ' AS RFID_END_FLAG \n"
                        + "				 , ' ' AS RFID_END_ERR_MSG \n"
                        + "				 , ' ' AS RFID_END_ERR_FIELD \n"
                        + "				 , MES_END_FLAG AS MES_END_FLAG \n"
                        + "				 , ' ' AS RFID_MES_END_COMPARE \n"
                        + "			  FROM (SELECT END_RES_ID AS RES_ID \n"
                        + "						 , LOT_ID AS LOT_ID \n"
                        + "						 , OLD_OPER AS OPER \n"
                        + "						 , TRAN_TIME AS MES_END_TIME \n"
                        + "						 , CASE WHEN TRAN_COMMENT LIKE 'EIS End Lot%' THEN 'EIS(RFID)' \n"
                        + "								ELSE 'MESCLIENT' \n"
                        + "						   END AS MES_END_FLAG \n"
                        + "					  FROM RWIPLOTHIS \n"
                        + "					 WHERE LOT_TYPE = 'W' \n"
                        + "					   AND TRAN_CODE = 'END' \n"
                        + "					   AND HIST_DEL_FLAG = ' ' \n"
                        + "					   AND OLD_OPER IN ('A1720', 'A1750') \n"
                        + "					   AND TRAN_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "					   AND LOT_ID IN (SELECT LOT_ID AS LOT_ID \n"
                        + "									    FROM (SELECT LOT_ID_1 AS LOT_ID \n"
                        + "											    FROM MESMGR.CEISMESLOG@RPTTOMES \n"
                        + "											   WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                        + "											     AND SYS_TIME BETWEEN '" + cdvFromToTime.Start_Tran_Time + "' AND '" + cdvFromToTime.End_Tran_Time + "' \n"
                        + "											     AND UPPER(FUNCTION_NAME) = 'RFID_EIS_START_MAGAZINE_REQ' \n"
                        + "											     AND ANTENNA_PORT_1 = '1' \n"
                        + "											     AND MAGAZINE_ID_1 <> ' ' \n"
                        + "											     AND LOT_ID_1 <> ' ' \n"
                        + "											     AND ANTENNA_PORT_2 = '0' \n"
                        + "											     AND MAGAZINE_ID_2 = ' ' \n"
                        + "											     AND LOT_ID_2 = ' ' \n"
                        + "							                     AND RES_ID LIKE 'SST%' \n"
                        + "                                              AND RES_ID LIKE '" + txtResourceID.Text + "%' \n"
                        + "											   GROUP BY LOT_ID_1)))) B \n"
                        + "	 WHERE A.RES_ID = B.RES_ID(+) \n"
                        + "	   AND A.LOT_ID = B.LOT_ID(+) \n"
                        + "	   AND A.OPER = B.OPER(+) \n"
                        + "	   AND A.STATUS_IDX = B.STATUS_IDX(+) \n"
                        + "	 ORDER BY A.RES_ID ASC, A.LOT_ID ASC, A.RFID_START_TIME ASC, B.RFID_END_TIME ASC";
                }
                
                // 2012-12-20-정비재 : 관리자일 경우만 query문을 복사한다.
                if (GlobalVariable.gsUserID == "ADMIN" || GlobalVariable.gsUserID == "WEBADMIN")
                {
                    System.Windows.Forms.Clipboard.SetText(QRY.Replace((char)Keys.Tab, ' '));
                }
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", QRY.Replace((char)Keys.Tab, ' '));

                if (dt.Rows.Count <= 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return false;
                }

                int[] rowType = SS01.RPT_DataBindingWithSubTotal(dt, 0, 0, 1, null, null);

                return true;
            }
            catch (Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(ex.Message);
                return false;
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion


        #region " Form Event "

        private void RFID010101_Load(object sender, EventArgs e)
        {
            /****************************************************
             * Comment : form이 실행될 때 발생되는 이벤트
             * 
             * Created By : bee-jae jung(2012-12-17-월요일)
             * 
             * Modified By : bee-jae jung(2012-12-17-월요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // 2012-12-17-정비재 : 상세조회, 그룹정보를 사용하지 못하게 한다.
                btnDetail.Enabled = false;
                btnSort.Enabled = false;
                
                // 2012-12-18-정비재 : control의 초기값을 설정한다.
                cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
                cdvFromToTime.FromDate.Value = DateTime.Now.AddDays(-1);
                cdvFromToTime.ToDate.Value = DateTime.Now; 
                cdvFromToTime.FromTimeSelector.SelectedIndex = 23;      // 2012-12-28-정비재 : 기본시간을 24시로 설정한다.
                cdvFromToTime.ToTimeSelector.SelectedIndex = 23;

                fnSSInitial(SS01);
                fnSSSortInit();
                
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            /****************************************************
             * Comment : View Button을 클릭하면
             * 
             * Created By : bee-jae jung(2010-05-11-화요일)
             * 
             * Modified By : bee-jae jung(2010-05-11-화요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                fnSSInitial(SS01);

                if (fnBusinessRule() == true)
                {
                    fnDataFind();
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            /****************************************************
             * Comment : Excel Export Button을 클릭하면
             * 
             * Created By : bee-jae jung(2010-05-11-화요일)
             * 
             * Modified By : bee-jae jung(2010-05-11-화요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                SS01.ExportExcel();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion


    }
}
