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
    public partial class PRD011011 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD011011<br/>
        /// 클래스요약: RFID 적용 트렌드 <br/>
        /// 작  성  자: 에이스텍 황종환<br/>
        /// 최초작성일: 2013-08-14<br/>
        /// 상세  설명: RFID 적용 트렌드(김권수 대리 요청)<br/>        
        /// 2013-08-27-황종환 : 위치 변경 RFID -> PRD
        /// 2013-10-29-황종환 : D/A공정 AUTO SPLIT진행시 RFID START로 진행이 되어도 작업자 사번으로 표시됨 
        ///                     D/A AUTO SPLIT시 RFID Start : Trans_cmf_5에 "AUTOSPLITSTART_DIEATTACH" 값으로 RFID START임을 확인하게 로직 변경
        /// 2013-10-29-임종우 : M/K 공정(A1150) 제외 (임태성 요청)
        /// 2013-11-12-임종우 : D/A AUTO SPLIT시 RFID Start : Trans_cmf_4에 "RFID TAG WRITE : SUCCESS" 값으로 RFID START임을 확인하게 로직 변경
        /// 2013-12-06-임종우 : START 로직 변경 - 기존 TRAN_CMF_18 = 'EIS' -> RESV_FLAG_5 = 'Y' 로 변경
        ///                     END 로직 변경 - 기존 TRAN_USER_ID = 'EIS' -> TRAN_USER_ID = 'EIS' AND TRAN_CMF_19 = 'RFID' 로 변경
        /// 2014-03-04-임종우 : START_RATE, FAIL_RATE 중복 오류 수정
        /// 2015-03-17-임종우 : FAIL_RATE 에서 CV_RATE 분리 (배진우C 요청)
        /// </summary>

        public PRD011011()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            SortInit();
            GridColumnInit();



            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            cdvFactory.Enabled = false;
        }
        #region " Constant Definition "

        #endregion

        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;

            try
            {
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Major Code", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Family", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD Count", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Density", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Generation", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PKG Code", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Product", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Operation", 0, 12, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Classification", 0, 13, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);

                //spdData.RPT_AddBasicColumn("Vendor description", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                //spdData.RPT_AddBasicColumn("PKG2", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                //spdData.RPT_AddBasicColumn("Lead", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);

                //spdData.RPT_AddBasicColumn("PKG code", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                //spdData.RPT_AddBasicColumn("Step", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                //spdData.RPT_AddBasicColumn("Classification", 0, 5, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);

                spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("TOTAL", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 80);

                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT_GRP_1", "DECODE(MAT_GRP_1, 'SE', 1, 'HX', 2, 'HH', 9, 3), MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "MAT_GRP_9", "MAT.MAT_GRP_9", "MAT_GRP_9", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT_GRP_2", "MAT.MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT_GRP_10", "MAT.MAT_GRP_10", "MAT_GRP_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT_GRP_4", "MAT.MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT_GRP_5", "MAT.MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "MAT.MAT_GRP_6", "MAT_GRP_6", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT_GRP_7", "MAT.MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT_GRP_8", "MAT.MAT_GRP_8", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "MAT_CMF_10", "MAT.MAT_CMF_10", "MAT_CMF_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG_CODE", "MAT_CMF_11", "MAT.MAT_CMF_11", "MAT_CMF_11", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT_ID", "MAT.MAT_ID", "MAT_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation", "OPER", "OPER", "OPER", true);
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        /// 
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string from = cdvFromToDate.ExactFromDate;
            string to = cdvFromToDate.ExactToDate;

            string[] selectDate = new string[cdvFromToDate.SubtractBetweenFromToDate + 1];
            selectDate = cdvFromToDate.getSelectDate();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;


            strSqlString.Append("SELECT " + QueryCond3 + ", GUBUN \n");

            for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                strSqlString.AppendFormat("     , \"{0}\" \n", selectDate[i].ToString());
            }
            strSqlString.Append("     , TOTAL \n");
            strSqlString.Append("  FROM ( \n");
            strSqlString.Append("        SELECT " + QueryCond1 + " \n");
            strSqlString.Append("             , GUBUN \n");
            for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                strSqlString.AppendFormat("             , DECODE(GUBUN,'LOT QTY',TO_CHAR(LOT_CNT_{0}), 'START', TO_CHAR(START_{0}), 'END', TO_CHAR(END_{0}), 'START_RATE', TO_CHAR(START_RATE_{0})||'%', 'FAIL_RATE', TO_CHAR(FAIL_RATE_{0})||'%', 'CV_RATE', TO_CHAR(CV_RATE_{0})||'%') \"{0}\" \n", selectDate[i].ToString());
            }
            strSqlString.Append("             , DECODE(GUBUN,'LOT QTY',TO_CHAR(LOT_CNT_TOTAL), 'START', TO_CHAR(START_TOTAL), 'END', TO_CHAR(END_TOTAL), 'START_RATE', TO_CHAR(START_RATE_TOTAL)||'%', 'FAIL_RATE', TO_CHAR(FAIL_RATE_TOTAL)||'%', 'CV_RATE', TO_CHAR(CV_RATE_TOTAL)||'%') \"TOTAL\" \n");
            strSqlString.Append("          FROM ( \n");
            strSqlString.Append("                SELECT " + QueryCond2 + " \n");

            for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                strSqlString.AppendFormat("                     , SUM(DECODE(DAY, '{0}', LOT_CNT,0)) AS \"LOT_CNT_{0}\" \n", selectDate[i].ToString());
            }
            for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                strSqlString.AppendFormat("                     , SUM(DECODE(DAY, '{0}', START_,0)) AS \"START_{0}\" \n", selectDate[i].ToString());
            }
            for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                strSqlString.AppendFormat("                     , SUM(DECODE(DAY, '{0}', END_,0)) AS \"END_{0}\" \n", selectDate[i].ToString());
            }
            for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                strSqlString.AppendFormat("                     , SUM(DECODE(DAY, '{0}', CV_,0)) AS \"CV_{0}\" \n", selectDate[i].ToString());
            }
            for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                //strSqlString.AppendFormat("                     , ROUND(SUM(DECODE(DAY, '{0}', START_/LOT_CNT*100,0)),0) AS \"START_RATE_{0}\"         \n", selectDate[i].ToString());                
                strSqlString.AppendFormat("                     , CASE WHEN SUM(DECODE(DAY, '{0}', LOT_CNT,0)) = 0 THEN 0 \n", selectDate[i].ToString());
                strSqlString.AppendFormat("                            ELSE ROUND(SUM(DECODE(DAY, '{0}', START_,0)) / SUM(DECODE(DAY, '{0}', LOT_CNT,0)) * 100,0) \n", selectDate[i].ToString());
                strSqlString.AppendFormat("                       END AS \"START_RATE_{0}\" \n", selectDate[i].ToString());
            }
            for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                //strSqlString.AppendFormat("                     , ROUND(SUM(DECODE(DAY, '{0}', NVL((START_- END_)/DECODE(START_,0,NULL,START_)*100,0),0)),0) AS \"FAIL_RATE_{0}\"         \n", selectDate[i].ToString());                                
                strSqlString.AppendFormat("                     , CASE WHEN SUM(DECODE(DAY, '{0}', START_,0)) = 0 THEN 0 \n", selectDate[i].ToString());
                strSqlString.AppendFormat("                            ELSE ROUND(SUM(DECODE(DAY, '{0}', (START_- END_-CV_))) / SUM(DECODE(DAY, '{0}', START_,0)) * 100,0) \n", selectDate[i].ToString());
                strSqlString.AppendFormat("                       END AS \"FAIL_RATE_{0}\" \n", selectDate[i].ToString());
            }
            for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                //strSqlString.AppendFormat("                     , ROUND(SUM(DECODE(DAY, '{0}', NVL((START_- END_)/DECODE(START_,0,NULL,START_)*100,0),0)),0) AS \"FAIL_RATE_{0}\"         \n", selectDate[i].ToString());                                
                strSqlString.AppendFormat("                     , CASE WHEN SUM(DECODE(DAY, '{0}', START_,0)) = 0 THEN 0 \n", selectDate[i].ToString());
                strSqlString.AppendFormat("                            ELSE ROUND(SUM(DECODE(DAY, '{0}', (CV_))) / SUM(DECODE(DAY, '{0}', START_,0)) * 100,0) \n", selectDate[i].ToString());
                strSqlString.AppendFormat("                       END AS \"CV_RATE_{0}\" \n", selectDate[i].ToString());
            }
            strSqlString.Append("                     , SUM(LOT_CNT) AS LOT_CNT_TOTAL \n");
            strSqlString.Append("                     , SUM(START_) AS START_TOTAL \n");
            strSqlString.Append("                     , SUM(END_) AS END_TOTAL \n");
            strSqlString.Append("                     , SUM(CV_) AS CV_TOTAL \n");
            strSqlString.Append("                     , ROUND(SUM(START_)/SUM(LOT_CNT)*100,0) AS START_RATE_TOTAL \n");
            strSqlString.Append("                     , ROUND(NVL((SUM(START_)-SUM(END_)-SUM(CV_))/DECODE(SUM(START_),0,NULL,SUM(START_))*100,0),0) AS FAIL_RATE_TOTAL \n");
            strSqlString.Append("                     , ROUND(NVL(SUM(CV_)/DECODE(SUM(START_),0,NULL,SUM(START_))*100,0),0) AS CV_RATE_TOTAL \n");
            strSqlString.Append("                  FROM ( \n");
            strSqlString.Append("                        SELECT LOTHIS.MAT_ID \n");
            strSqlString.Append("                             , LOTHIS.OPER \n");
            strSqlString.Append("                             , DAY \n");
            strSqlString.Append("                             , SUM(LOT_CNT) AS LOT_CNT \n");
            strSqlString.Append("                             , SUM(CASE WHEN LOTHIS.OPER LIKE 'A040%' THEN (CASE WHEN (START_LOT.TRAN_CMF_4 = 'RFID TAG WRITE : SUCCESS') \n");
            strSqlString.Append("                                                                                   OR (START_LOT.RESV_FLAG_5 = 'Y' AND START_LOT.TRAN_USER_ID = 'EIS' AND START_LOT.TRAN_CMF_19 = 'RFID') \n");
            strSqlString.Append("                                                                                 THEN 1 \n");
            strSqlString.Append("                                                                                 ELSE 0 \n");
            strSqlString.Append("                                                                            END) \n");
            strSqlString.Append("                                         ELSE (CASE WHEN START_LOT.TRAN_CMF_19 = 'RFID' AND START_LOT.TRAN_USER_ID = 'EIS' THEN 1 ELSE 0 END) \n");
            strSqlString.Append("                               END) AS START_ \n");
            strSqlString.Append("                             , SUM(END_) AS END_ \n");
            strSqlString.Append("                             , SUM(CASE WHEN CV_ >= 1 THEN (CASE WHEN START_LOT.TRAN_CMF_19 = 'RFID' AND START_LOT.TRAN_USER_ID = 'EIS' THEN 1 ELSE 0 END) \n");
            strSqlString.Append("                                        ELSE 0 \n");
            strSqlString.Append("                               END) AS CV_ \n");
            strSqlString.Append("                          FROM ( \n");
            strSqlString.Append("                                SELECT A.MAT_ID \n");
            strSqlString.Append("                                     , A.LOT_ID \n");
            strSqlString.Append("                                     , OLD_OPER AS OPER \n");

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                strSqlString.Append("                                     , GET_WORK_DATE(TRAN_TIME, 'M') AS DAY \n");
            }
            else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                strSqlString.Append("                                     , GET_WORK_DATE(TRAN_TIME, 'W') AS DAY \n");
            }
            else
            {
                strSqlString.Append("                                     , GET_WORK_DATE(TRAN_TIME, 'D') AS DAY \n");
            }

            strSqlString.Append("                                     , SUM(CASE WHEN TRAN_CODE IN ('END','SHIP') THEN 1 ELSE 0 END) AS LOT_CNT \n");
            strSqlString.Append("                                     , SUM(CASE WHEN TRAN_CODE IN ('END', 'SHIP') AND RESV_FLAG_5 = 'Y' THEN CASE WHEN OLD_OPER LIKE 'A040%'AND TRAN_CMF_19 = 'RFID' AND TRAN_USER_ID = 'EIS' THEN 1 \n");
            strSqlString.Append("                                                                                                                  WHEN OLD_OPER LIKE 'A060%' AND TRAN_CMF_18 = 'EIS' AND TRAN_CMF_19 = 'RFID' AND TRAN_USER_ID = 'EIS' THEN 1 \n");
            strSqlString.Append("                                                                                                                  ELSE 0 \n");
            strSqlString.Append("                                                                                                             END \n");
            strSqlString.Append("                                                ELSE 0 \n");
            strSqlString.Append("                                           END) AS END_ \n");
            strSqlString.Append("                                     , SUM(CASE WHEN RESV_FLAG_5 = 'Y' AND TRAN_CMF_17 NOT IN (' ', 'CV Different: 0 (Single)', 'CV Different: 0 (Multi)') THEN 1 ELSE 0 END) AS CV_ \n");
            strSqlString.Append("                                     , MIN(START_TIME) MIN_START \n");
            strSqlString.Append("                                  FROM RWIPLOTHIS A \n");
            strSqlString.Append("                                 WHERE 1=1 \n");
            strSqlString.Append("                                   AND TRAN_TIME BETWEEN '" + from + "' AND '" + to + "' \n");
            strSqlString.Append("                                   AND OLD_FACTORY = '" + cdvFactory.Text + "' \n");
            strSqlString.Append("                                   AND MAT_ID LIKE '" + txtSearchProduct.Text.Trim() + "%' \n");
            strSqlString.Append("                                   AND TRAN_CODE IN ('END','SHIP') \n");
            strSqlString.Append("                                   AND LOT_TYPE = 'W' \n");
            strSqlString.Append("                                   AND HIST_DEL_FLAG = ' ' \n");
            if (cdvStep.Text.Equals("ALL"))
            {
                strSqlString.Append("                                   AND REGEXP_LIKE(OLD_OPER, 'A040*|A060*|A1000') \n");
            }
            else
            {
                strSqlString.AppendFormat("                                   AND OLD_OPER LIKE '{0}' \n", cdvStep.Text);
            }
            strSqlString.Append("                                   AND END_RES_ID LIKE '%' \n");
            if (!cdvLotType.Text.Equals("ALL"))
            {
                strSqlString.Append("                                   AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "' \n");
            }
            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                strSqlString.Append("                                 GROUP BY A.MAT_ID,A.LOT_ID, OLD_OPER, GET_WORK_DATE(TRAN_TIME, 'M') \n");
            }
            else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                strSqlString.Append("                                 GROUP BY A.MAT_ID,A.LOT_ID, OLD_OPER, GET_WORK_DATE(TRAN_TIME, 'W') \n");
            }
            else
            {
                strSqlString.Append("                                 GROUP BY A.MAT_ID,A.LOT_ID, OLD_OPER, GET_WORK_DATE(TRAN_TIME, 'D') \n");
            }
            strSqlString.Append("                                 ORDER BY A.MAT_ID, OLD_OPER \n");
            strSqlString.Append("                               ) LOTHIS \n");
            strSqlString.Append("                             , ( \n");
            strSqlString.Append("                                SELECT LOT_ID, TRAN_TIME, TRAN_CMF_4, TRAN_CMF_19, TRAN_USER_ID, RESV_FLAG_5 \n");
            strSqlString.Append("                                  FROM RWIPLOTHIS \n");
            strSqlString.Append("                                 WHERE 1 = 1 \n");
            strSqlString.Append("                                   AND MAT_ID LIKE '" + txtSearchProduct.Text.Trim() + "%' \n");
            strSqlString.Append("                                   AND OLD_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' \n");
            strSqlString.Append("                                   AND LOT_TYPE = 'W' \n");
            strSqlString.Append("                                   AND HIST_DEL_FLAG = ' ' \n");
            strSqlString.Append("                                   AND TRAN_CODE = 'START' \n");
            strSqlString.Append("                               ) START_LOT \n");
            strSqlString.Append("                         WHERE LOTHIS.LOT_ID = START_LOT.LOT_ID \n");
            strSqlString.Append("                           AND LOTHIS.MIN_START = START_LOT.TRAN_TIME \n");
            strSqlString.Append("                         GROUP BY LOTHIS.MAT_ID, DAY,  LOTHIS.OPER \n");
            strSqlString.Append("                       ) A \n");
            strSqlString.Append("                     , ( \n");
            strSqlString.Append("                        SELECT MAT.* \n");
            strSqlString.Append("                          FROM MWIPMATDEF MAT \n");
            strSqlString.Append("                             , MGCMTBLDAT GCM \n");
            strSqlString.Append("                         WHERE 1 = 1 \n");
            strSqlString.Append("                             AND GCM.TABLE_NAME = 'H_RFID_MAT' \n");
            strSqlString.Append("                             AND GCM.DATA_1 = 'Y' \n");

            // 상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0}" + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.Append("                             AND MAT.MAT_CMF_11 = GCM.KEY_1 \n");
            strSqlString.Append("                       ) MAT \n");
            strSqlString.Append("                 WHERE 1=1 \n");
            strSqlString.Append("                   AND A.MAT_ID = MAT.MAT_ID \n");
            strSqlString.Append("                   AND MAT.FACTORY = '" + cdvFactory.Text + "' \n");
            strSqlString.Append("                   AND MAT.MAT_TYPE = 'FG' \n");
            strSqlString.Append("                 GROUP BY " + QueryCond2 + " \n");
            strSqlString.Append("                 ORDER BY " + QueryCond2 + " \n");
            strSqlString.Append("               ) LOTHIS \n");
            strSqlString.Append("             , ( \n");
            strSqlString.Append("                SELECT 'LOT QTY' AS GUBUN FROM DUAL \n");
            strSqlString.Append("                 UNION ALL \n");
            strSqlString.Append("                SELECT 'START' FROM DUAL \n");
            strSqlString.Append("                 UNION ALL \n");
            strSqlString.Append("                SELECT 'END' FROM DUAL \n");
            strSqlString.Append("                 UNION ALL \n");
            strSqlString.Append("                SELECT 'START_RATE' FROM DUAL \n");
            strSqlString.Append("                 UNION ALL \n");
            strSqlString.Append("                SELECT 'FAIL_RATE' FROM DUAL \n");
            strSqlString.Append("                 UNION ALL \n");
            strSqlString.Append("                SELECT 'CV_RATE' FROM DUAL \n");
            strSqlString.Append("               ) DUMMY \n");
            strSqlString.Append("       ) \n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        /// <summary>
        /// RFID 실패 내역을 조회하기 위한 쿼리
        /// </summary>
        /// 
        /// <returns>쿼리</returns>
        private string MakeSqlStringForPopup(string factory, string pkgCode, string step, string timeType, string timeData, string totalFrom, string totalTo, string rateType)
        {
            string from = "";
            string to = "";
            DateTime dtFrom;
            DateTime dtTo;

            if (timeData.Equals("TOTAL"))
            {
                from = totalFrom;
                to = totalTo;
            }
            else
            {
                if (timeType.Equals("M"))
                {
                    dtFrom = new DateTime(int.Parse(timeData.Substring(0, 2)), int.Parse(timeData.Substring(3, 2)), 1).AddDays(-1);
                    dtTo = new DateTime(int.Parse(timeData.Substring(0, 2)), int.Parse(timeData.Substring(3, 2)), 1).AddMonths(1).AddDays(-1);
                    from = String.Concat(dtFrom.ToString("yyyyMMdd"), "220000");
                    to = String.Concat(dtTo.ToString("yyyyMMdd"), "215959");
                }
                else if (timeType.Equals("W"))
                {
                    // 밑에 쿼리에서 처리
                }
                else // DAY일 때
                {
                    string yearFrom = cdvFromToDate.FromDate.Text.Substring(0, 4);

                    dtFrom = new DateTime(int.Parse(yearFrom), int.Parse(timeData.Substring(0, 2)), int.Parse(timeData.Substring(3, 2))).AddDays(-1);
                    dtTo = new DateTime(int.Parse(yearFrom), int.Parse(timeData.Substring(0, 2)), int.Parse(timeData.Substring(3, 2)));

                    from = String.Concat(dtFrom.ToString("yyyyMMdd"), "220000");
                    to = String.Concat(dtTo.ToString("yyyyMMdd"), "215959");
                }
            }


            StringBuilder strSqlString = new StringBuilder();
            if (timeType.Equals("W") && !timeData.Equals("TOTAL"))
            {
                strSqlString.Append("WITH WEEK_TIME AS (\n");
                strSqlString.Append("    SELECT TO_CHAR(TO_DATE(MIN(SYS_DATE), 'YYYYMMDD') - 1, 'YYYYMMDD')||'220000' START_T \n");
                strSqlString.Append("         , TO_CHAR(TO_DATE(MAX(SYS_DATE), 'YYYYMMDD') , 'YYYYMMDD')||'215959' END_T \n");
                strSqlString.Append("      FROM MWIPCALDEF \n");
                strSqlString.Append("     WHERE 1 = 1 \n");
                strSqlString.Append("       AND CALENDAR_ID = 'HM' \n");
                strSqlString.Append("       AND PLAN_YEAR = '" + cdvFromToDate.FromDate.Text.Substring(0, 4) + "' \n");
                strSqlString.Append("       AND PLAN_WEEK = '" + timeData.Substring(2, 2) + "' \n");
                strSqlString.Append(") \n");
            }
            strSqlString.Append("SELECT A.MAT_ID, A.LOT_ID, A.QTY, A.OPER, ERR.RES_ID, ERR.ERR_MSG \n");
            strSqlString.Append("  FROM (SELECT A.LOT_ID \n");
            strSqlString.Append("             , A.MAT_ID \n");
            strSqlString.Append("             , OLD_OPER AS OPER \n");
            strSqlString.Append("             , QTY_1 AS QTY \n");
            strSqlString.Append("         FROM RWIPLOTHIS A \n");
            if (timeType.Equals("W") && !timeData.Equals("TOTAL"))
            {
                strSqlString.Append("            , WEEK_TIME \n");
            }
            strSqlString.Append("        WHERE 1 = 1 \n");
            if (timeType.Equals("W") && !timeData.Equals("TOTAL"))
            {
                strSqlString.Append("          AND TRAN_TIME BETWEEN WEEK_TIME.START_T AND WEEK_TIME.END_T \n");

            }
            else
            {
                strSqlString.Append("          AND TRAN_TIME BETWEEN '" + from + "' AND '" + to + "' \n");
            }
            strSqlString.Append("          AND OLD_FACTORY = '" + factory + "' \n");
            strSqlString.Append("          AND TRAN_CODE IN ('END', 'SHIP') \n");
            strSqlString.Append("          AND LOT_TYPE = 'W' \n");
            strSqlString.Append("          AND HIST_DEL_FLAG = ' ' \n");
            if (step.Equals(" "))
            {
                strSqlString.Append("          AND REGEXP_LIKE(OLD_OPER, 'A040*|A060*|A1000') \n");
            }
            else
            {
                strSqlString.Append("          AND OLD_OPER LIKE '" + step + "' \n");
            }
            strSqlString.Append("          AND TRAN_USER_ID <> 'EIS' \n");

            if (rateType == "FAIL_RATE")
            {
                strSqlString.Append("          AND TRAN_CMF_17 IN (' ', 'CV Different: 0 (Single)') \n");
            }
            else
            {
                strSqlString.Append("          AND TRAN_CMF_17 NOT IN (' ', 'CV Different: 0 (Single)') \n");
            }

            strSqlString.Append("        ORDER BY A.MAT_ID, OLD_OPER \n");
            strSqlString.Append("       ) A \n");
            strSqlString.Append("     , (SELECT MAT.* \n");
            strSqlString.Append("          FROM MWIPMATDEF MAT \n");
            strSqlString.Append("             , MGCMTBLDAT GCM \n");
            strSqlString.Append("         WHERE 1 = 1 \n");
            strSqlString.Append("           AND GCM.TABLE_NAME = 'H_RFID_MAT' \n");
            strSqlString.Append("           AND GCM.DATA_1 = 'Y' \n");
            strSqlString.Append("           AND MAT.MAT_CMF_11 = GCM.KEY_1 \n");
            if (pkgCode.Equals(" "))
            {
                strSqlString.Append("           AND MAT.MAT_CMF_11 LIKE '%') MAT \n");
            }
            else
            {
                strSqlString.Append("           AND MAT.MAT_CMF_11 = '" + pkgCode + "') MAT \n");
            }


            strSqlString.Append("     , (SELECT RES_ID AS RES_ID \n");
            strSqlString.Append("             , LOT_ID_1 AS LOT_ID \n");
            strSqlString.Append("             , FUNCTION_NAME \n");
            strSqlString.Append("             , ERR_MSG AS ERR_MSG \n");
            strSqlString.Append("             , ERR_FIELD_MSG AS ERR_FIELD_MSG \n");
            strSqlString.Append("          FROM MESMGR.CEISMESLOG@RPTTOMES \n");
            if (timeType.Equals("W") && !timeData.Equals("TOTAL"))
            {
                strSqlString.Append("            , WEEK_TIME \n");
            }
            strSqlString.Append("         WHERE FACTORY = '" + factory + "' \n");
            if (timeType.Equals("W") && !timeData.Equals("TOTAL"))
            {
                strSqlString.Append("          AND SYS_TIME BETWEEN WEEK_TIME.START_T AND WEEK_TIME.END_T \n");

            }
            else
            {
                strSqlString.Append("          AND SYS_TIME BETWEEN '" + from + "' AND '" + to + "' \n");
            }
            strSqlString.Append("           AND UPPER(FUNCTION_NAME) <> 'RFID_EIS_START_MAGAZINE_REQ' \n");
            strSqlString.Append("           AND ANTENNA_PORT_1 = '1' \n");
            strSqlString.Append("           AND MAGAZINE_ID_1 <> ' ' \n");
            strSqlString.Append("           AND LOT_ID_1 <> ' ' \n");
            strSqlString.Append("           AND ANTENNA_PORT_2 = '2' \n");
            strSqlString.Append("           AND MAGAZINE_ID_2 <> ' ' \n");
            strSqlString.Append("           AND LOT_ID_2 = ' ' \n");
            strSqlString.Append("           AND RES_ID LIKE '%%' \n");
            strSqlString.Append("           AND RESULT = 1 \n");
            strSqlString.Append("        UNION ALL \n");
            strSqlString.Append("        SELECT RES_ID AS RES_ID \n");
            strSqlString.Append("             , LOT_ID_2 AS LOT_ID \n");
            strSqlString.Append("             , FUNCTION_NAME \n");
            strSqlString.Append("             , ERR_MSG AS ERR_MSG \n");
            strSqlString.Append("             , ERR_FIELD_MSG AS ERR_FIELD_MSG \n");
            strSqlString.Append("          FROM MESMGR.CEISMESLOG@RPTTOMES \n");
            if (timeType.Equals("W") && !timeData.Equals("TOTAL"))
            {
                strSqlString.Append("            , WEEK_TIME \n");
            }
            strSqlString.Append("         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' \n");
            if (timeType.Equals("W") && !timeData.Equals("TOTAL"))
            {
                strSqlString.Append("          AND SYS_TIME BETWEEN WEEK_TIME.START_T AND WEEK_TIME.END_T \n");

            }
            else
            {
                strSqlString.Append("          AND SYS_TIME BETWEEN '" + from + "' AND '" + to + "' \n");
            }
            strSqlString.Append("           AND UPPER(FUNCTION_NAME) <> 'RFID_EIS_START_MAGAZINE_REQ' \n");
            strSqlString.Append("           AND ANTENNA_PORT_1 = '2' \n");
            strSqlString.Append("           AND MAGAZINE_ID_1 <> ' ' \n");
            strSqlString.Append("           AND LOT_ID_1 = ' ' \n");
            strSqlString.Append("           AND ANTENNA_PORT_2 = '1' \n");
            strSqlString.Append("           AND MAGAZINE_ID_2 <> ' ' \n");
            strSqlString.Append("           AND LOT_ID_2 <> ' ' \n");
            strSqlString.Append("           AND RESULT = 1 \n");
            strSqlString.Append("       ) ERR \n");
            strSqlString.Append(" WHERE 1 = 1 \n");
            strSqlString.Append("   AND A.MAT_ID = MAT.MAT_ID \n");
            strSqlString.Append("   AND MAT.FACTORY = '" + factory + "' \n");
            strSqlString.Append("   AND MAT.MAT_TYPE = 'FG' \n");
            strSqlString.Append("   AND A.LOT_ID = ERR.LOT_ID(+) \n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        /// <summary>
        /// 5. Chart 생성
        /// </summary>
        /// <param name="DT">Chart를 생성할 데이터 테이블</param>

        #endregion

        #region EventHandler

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (CheckField() == false) return;

            GridColumnInit();

            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);

                this.Refresh();

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                spdData.DataSource = dt;
                for (int i = 0; i <= 13; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LemonChiffon;
                }

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                dt.Dispose();

            }
            catch (Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        /// <summary>
        /// 스프레드를 클릭했을 때 Part 에 해당하는 잔량 및 cutoff day를 화면에 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            if (e.Column < 14 || (!spdData.ActiveSheet.GetText(e.Row, 13).Equals("FAIL_RATE") && !spdData.ActiveSheet.GetText(e.Row, 13).Equals("CV_RATE")))
            {
                return;
            }

            if (spdData.ActiveSheet.GetText(e.Row, e.Column).Equals("0%"))
            {
                return;
            }
            
            string factory = cdvFactory.Text;
            string pkgCode = spdData.ActiveSheet.GetText(e.Row, 10);
            string step = spdData.ActiveSheet.GetText(e.Row, 12);
            string timeType = "";
            string timeData = spdData.ActiveSheet.ColumnHeader.Cells[0, e.Column].Text;
            string from = cdvFromToDate.ExactFromDate;
            string to = cdvFromToDate.ExactToDate;
            string rateType = spdData.ActiveSheet.GetText(e.Row, 13).ToString();


            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                timeType = "M";
            }
            else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                timeType = "W";
            }
            else
            {
                timeType = "D";
            }

            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForPopup(factory, pkgCode, step, timeType, timeData, from, to, rateType));

            if (dt != null && dt.Rows.Count > 0)
            {
                System.Windows.Forms.Form frm = new PRD011011_P1("RF ID Fail History", dt);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("해당 하는 Part의 세부 정보가 없습니다.");
            }
        }

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();
        }

        #endregion
    }
}
