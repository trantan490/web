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
    public partial class PRD010611 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010611<br/>
        /// 클래스요약: 일별 AO, WAFER PLAN 대비 실적<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2013-07-04<br/>
        /// 상세  설명: 일별 AO, WAFER PLAN 대비 실적<br/>
        /// 변경  내용: <br/>
        /// 2013-07-09-임종우 : PIN_TYPE 추가, PART 검색 조건 추가, 차주 계획 최신 버전으로 가져오게.., 차주 Plan 에 대해서 음영표시(Wafer 입고 계획만)
        /// 2013-07-25-임종우 : 계획 테이블 변경 (CWIPPLNWEK_N -> RWIPPLNWEK)
        /// 2013-07-25-임종우 : 과부족, 누계차이 부분 해당일자까지 표시되도록 수정, 단 과부족의 TTL 전체로 계산&누계차이의 TTL은 마지막 일자 데이터로 표시 (임태성 요청)
        /// 2013-07-26-임종우 : COB 계획 값 WF 장수로 표현, 실적은 A/O 로 변경
        /// 2013-08-07-임종우 : 구분1 값 검색 기능 추가 (임태성 요청)
        /// 2013-10-21-임종우 : 실적 06시 기준 추가 (임태성 요청)
        /// 2017-10-25-임종우 : SES%의 계획의 경우도 토~금 기준으로 가져오도록 변경 (최연희D 요청)
        /// </summary>
        GlobalVariable.FindWeek FindWeek = new GlobalVariable.FindWeek();
        GlobalVariable.FindWeek FindWeek_SE = new GlobalVariable.FindWeek();

        string[] dayArray = new string[7];
        string[] dayArry2 = new string[7];

        public PRD010611()
        {
            InitializeComponent();
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
        }

        #region 유효성검사 및 초기화
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            GetWorkDay();
            spdData.RPT_ColumnInit();           

            try
            {                
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Major Code", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);                
                spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD Count", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);                
                spdData.RPT_AddBasicColumn("Density", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Generation", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PKG CODE", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("Weekly SOP", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("START WIP", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WF remaining", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);

                if (cdvType.Text == "ALL")
                {
                    spdData.RPT_AddBasicColumn("Category 1", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.Always, Formatter.String, 70);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Category 1", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                }

                spdData.RPT_AddBasicColumn("Category 2", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);

                spdData.RPT_AddBasicColumn(dayArry2[0], 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Sat.", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(dayArry2[1], 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("일", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(dayArry2[2], 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("월", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(dayArry2[3], 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Tue.", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(dayArry2[4], 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Wed.", 1, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(dayArry2[5], 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Thur.", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(dayArry2[6], 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Fri.", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                                
                spdData.RPT_AddBasicColumn("TTL", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("CUT_OFF", 0, 22, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.Number, 50);

                for (int i = 0; i <= 13; i++)
                {
                    spdData.RPT_MerageHeaderRowSpan(0, i, 2);
                }

                spdData.RPT_MerageHeaderRowSpan(0, 21, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 22, 2);

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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT.MAT_GRP_1", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Major Code", "MAT.MAT_GRP_9", "MAT_GRP_9", "MAT.MAT_GRP_9", false);            
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_10", "MAT_GRP_10", "MAT.MAT_GRP_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6", "MAT_GRP_6", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4", "MAT_GRP_4", "MAT.MAT_GRP_4", false);            
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7", "MAT_GRP_7", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8", "MAT_GRP_8", "MAT.MAT_GRP_8", false);            
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "MAT.MAT_CMF_11", "MAT_CMF_11", "MAT.MAT_CMF_11", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10", "MAT_CMF_10", "MAT.MAT_CMF_10", false);  
        }
        #endregion


        #region 시간관련 함수
        private void GetWorkDay()
        {            
            FindWeek = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");
            FindWeek_SE = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "SE");

            DateTime Now = DateTime.ParseExact(FindWeek.StartDay_ThisWeek, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

            for (int i = 0; i < 7; i++)
            {                
                dayArray[i] = Now.ToString("yyyyMMdd");
                dayArry2[i] = Now.ToString("MM.dd");
                Now = Now.AddDays(1);
            }
                       
        }

        #endregion


        #region SQL 쿼리 Build
        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1;
            string QueryCond2;
            string QueryCond3;            
            string Yesterday;            
            string Today;
            string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값
            
            Today = cdvDate.Value.ToString("yyyyMMdd");
            Yesterday = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            // kpcs 선택에 의한 분모 값 저장 한다.
            if (ckbKpcs.Checked == true)
            {
                sKpcsValue = "1000";
            }
            else
            {
                sKpcsValue = "1";
            }
            
            strSqlString.Append("WITH CUT_OFF AS" + "\n");
            strSqlString.Append("(" + "\n");
            strSqlString.Append(" SELECT SAP_CODE, WORK_DATE, SYS_DATE, W1_CUTOFF" + "\n");
            strSqlString.Append("      , CASE WHEN SYS_DATE - W1_CUTOFF <= -1 THEN 24" + "\n");
            strSqlString.Append("             WHEN SYS_DATE - W1_CUTOFF > 0 THEN 0" + "\n");
            strSqlString.Append("             ELSE ABS((SYS_DATE - W1_CUTOFF) * 24)" + "\n");
            strSqlString.Append("        END AS THIS_WEEK_TIME" + "\n");
            strSqlString.Append("      , CASE WHEN SYS_DATE - W1_CUTOFF <= -1 THEN 0" + "\n");
            strSqlString.Append("             WHEN SYS_DATE - W1_CUTOFF > 0 THEN 24" + "\n");
            strSqlString.Append("             ELSE 24 - ABS((SYS_DATE - W1_CUTOFF) * 24)" + "\n");
            strSqlString.Append("        END AS NEXT_WEEK_TIME" + "\n");
            strSqlString.Append("      , TO_CHAR(W1_CUTOFF,'D') AS CKD_DAY" + "\n");
            strSqlString.Append("   FROM RSUMCUTOFF A" + "\n");
            strSqlString.Append("      , (" + "\n");
            strSqlString.Append("         SELECT TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_WEEK,2,'0') AS PLAN_WEEK, SYS_DATE AS WORK_DATE, TO_DATE(SYS_DATE, 'YYYYMMDD') AS SYS_DATE" + "\n");
            strSqlString.Append("           FROM MWIPCALDEF" + "\n");
            strSqlString.Append("          WHERE CALENDAR_ID = 'OTD'" + "\n");
            strSqlString.Append("            AND (PLAN_YEAR, PLAN_WEEK) = (SELECT PLAN_YEAR, PLAN_WEEK FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = '" + Today +"') " + "\n");
            strSqlString.Append("        ) B" + "\n");
            strSqlString.Append("  WHERE 1=1" + "\n");
            strSqlString.Append("    AND A.WORK_WEEK = B.PLAN_WEEK" + "\n");
            strSqlString.Append("    AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("    AND A.OPER IN ('A0020', 'A0100')" + "\n");
            strSqlString.Append("), " + "\n");
            strSqlString.Append("DT AS" + "\n");
            strSqlString.Append("(" + "\n");
            strSqlString.Append("SELECT STD.VENDOR_ID" + "\n");
            strSqlString.Append("     , STD.WORK_DATE" + "\n");
            strSqlString.Append("     , DECODE(RNK, 1, W1_QTY) AS W1_QTY" + "\n");
            strSqlString.Append("     , DECODE(RNK, 1, TTL_WIP) AS TTL_WIP" + "\n");
            strSqlString.Append("     , DECODE(RNK, 1, WF_DEF) AS WF_DEF" + "\n");
            strSqlString.Append("     , TTL_WEEK_NEED" + "\n");
            strSqlString.Append("     , SUM(NVL(TTL_WEEK_NEED,0)) OVER(PARTITION BY STD.VENDOR_ID ORDER BY STD.WORK_DATE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS SUM_TTL_WEEK_NEED" + "\n");
            strSqlString.Append("     , RCV_QTY" + "\n");
            strSqlString.Append("     , SUM(NVL(RCV_QTY,0)) OVER(PARTITION BY STD.VENDOR_ID ORDER BY STD.WORK_DATE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS SUM_RCV_QTY " + "\n");
            strSqlString.Append("     , PLN_QTY" + "\n");
            strSqlString.Append("     , SUM(NVL(PLN_QTY,0)) OVER(PARTITION BY STD.VENDOR_ID ORDER BY STD.WORK_DATE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS SUM_PLN_QTY " + "\n");
            strSqlString.Append("     , SHP_QTY" + "\n");
            strSqlString.Append("     , SUM(NVL(SHP_QTY,0)) OVER(PARTITION BY STD.VENDOR_ID ORDER BY STD.WORK_DATE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS SUM_SHP_QTY" + "\n");
            strSqlString.Append("     , NVL(RCV_QTY,0) - NVL(TTL_WEEK_NEED,0) AS SHORT_WF" + "\n");
            strSqlString.Append("     , NVL(SHP_QTY,0) - NVL(PLN_QTY,0) AS SHORT_SHP" + "\n");
            strSqlString.Append("     , SUM(NVL(RCV_QTY,0)) OVER(PARTITION BY STD.VENDOR_ID ORDER BY STD.WORK_DATE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) -" + "\n");
            strSqlString.Append("       SUM(NVL(TTL_WEEK_NEED,0)) OVER(PARTITION BY STD.VENDOR_ID ORDER BY STD.WORK_DATE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS SUM_SHORT_WF" + "\n");
            strSqlString.Append("     , SUM(NVL(SHP_QTY,0)) OVER(PARTITION BY STD.VENDOR_ID ORDER BY STD.WORK_DATE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) -" + "\n");
            strSqlString.Append("       SUM(NVL(PLN_QTY,0)) OVER(PARTITION BY STD.VENDOR_ID ORDER BY STD.WORK_DATE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS SUM_SHORT_SHP" + "\n");
            strSqlString.Append("     , CKD_DAY" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT A.VENDOR_ID" + "\n");
            strSqlString.Append("             , B.WORK_DATE" + "\n");
            strSqlString.Append("             , A.W1_QTY" + "\n");
            strSqlString.Append("             , A.TTL_WIP" + "\n");
            strSqlString.Append("             , A.WF_DEF" + "\n");
            strSqlString.Append("             , A.W2_QTY" + "\n");
            strSqlString.Append("             , A.THIS_WEEK_TIME_IN" + "\n");
            strSqlString.Append("             , A.NEXT_WEEK_TIME_IN " + "\n");
            strSqlString.Append("             , ROUND(B.THIS_WEEK_TIME) AS THIS_WEEK_TIME" + "\n");
            strSqlString.Append("             , ROUND(B.NEXT_WEEK_TIME) AS NEXT_WEEK_TIME" + "\n");
            strSqlString.Append("             , ROUND(A.THIS_WEEK_TIME_IN * THIS_WEEK_TIME, 2) AS THIS_WEEK_NEED" + "\n");
            strSqlString.Append("             , ROUND(A.NEXT_WEEK_TIME_IN * NEXT_WEEK_TIME, 2) AS NEXT_WEEK_NEED" + "\n");
            strSqlString.Append("             , ROUND((A.THIS_WEEK_TIME_IN * THIS_WEEK_TIME) + (A.NEXT_WEEK_TIME_IN * NEXT_WEEK_TIME), 2) AS TTL_WEEK_NEED" + "\n");
            strSqlString.Append("             , CASE WHEN TO_CHAR(TO_DATE(B.WORK_DATE,'YYYYMMDD'),'D') = 7 THEN D0_QTY" + "\n");
            strSqlString.Append("                    WHEN TO_CHAR(TO_DATE(B.WORK_DATE,'YYYYMMDD'),'D') = 1 THEN D1_QTY" + "\n");
            strSqlString.Append("                    WHEN TO_CHAR(TO_DATE(B.WORK_DATE,'YYYYMMDD'),'D') = 2 THEN D2_QTY" + "\n");
            strSqlString.Append("                    WHEN TO_CHAR(TO_DATE(B.WORK_DATE,'YYYYMMDD'),'D') = 3 THEN D3_QTY" + "\n");
            strSqlString.Append("                    WHEN TO_CHAR(TO_DATE(B.WORK_DATE,'YYYYMMDD'),'D') = 4 THEN D4_QTY" + "\n");
            strSqlString.Append("                    WHEN TO_CHAR(TO_DATE(B.WORK_DATE,'YYYYMMDD'),'D') = 5 THEN D5_QTY" + "\n");
            strSqlString.Append("                    WHEN TO_CHAR(TO_DATE(B.WORK_DATE,'YYYYMMDD'),'D') = 6 THEN D6_QTY" + "\n");
            strSqlString.Append("                    ELSE 0" + "\n");
            strSqlString.Append("               END PLN_QTY " + "\n");
            strSqlString.Append("             , ROW_NUMBER() OVER(PARTITION BY A.VENDOR_ID ORDER BY B.WORK_DATE) AS RNK" + "\n");
            strSqlString.Append("             , CKD_DAY" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT A.*" + "\n");
            strSqlString.Append("                     , ROUND(DECODE(THIS_TIME_SUM, 0, 0, WF_DEF / THIS_TIME_SUM), 2) AS THIS_WEEK_TIME_IN" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT VENDOR_ID" + "\n");
            strSqlString.Append("                             , W1_QTY" + "\n");
            strSqlString.Append("                             , TTL_WIP" + "\n");
            strSqlString.Append("                             , NVL(W1_QTY,0) - NVL(TTL_WIP,0) AS WF_DEF" + "\n");
            strSqlString.Append("                             , W2_QTY" + "\n");
            strSqlString.Append("                             , D0_QTY, D1_QTY, D2_QTY, D3_QTY, D4_QTY, D5_QTY, D6_QTY" + "\n");
            strSqlString.Append("                             , ROUND(W2_QTY / 168,2) AS NEXT_WEEK_TIME_IN" + "\n");
            strSqlString.Append("                             , NVL((       " + "\n");
            strSqlString.Append("                                    SELECT SUM(THIS_WEEK_TIME) AS THIS_WEEK_TIME" + "\n");
            strSqlString.Append("                                      FROM CUT_OFF" + "\n");
            strSqlString.Append("                                     WHERE SAP_CODE = VENDOR_ID" + "\n");
            strSqlString.Append("                                     GROUP BY SAP_CODE " + "\n");            
            strSqlString.Append("                                   ),0) AS THIS_TIME_SUM" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT VENDOR_ID" + "\n");
            strSqlString.Append("                                     , SUM(CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN W1_QTY/TO_NUMBER(DECODE(MAT_CMF_13,' ',1,MAT_CMF_13)) ELSE W1_QTY END) AS W1_QTY" + "\n");
            strSqlString.Append("                                     , SUM(CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN W2_QTY/TO_NUMBER(DECODE(MAT_CMF_13,' ',1,MAT_CMF_13)) ELSE W2_QTY END) AS W2_QTY" + "\n");
            strSqlString.Append("                                     , SUM(CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN D0_QTY/TO_NUMBER(DECODE(MAT_CMF_13,' ',1,MAT_CMF_13)) ELSE D0_QTY END) AS D0_QTY" + "\n");
            strSqlString.Append("                                     , SUM(CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN D1_QTY/TO_NUMBER(DECODE(MAT_CMF_13,' ',1,MAT_CMF_13)) ELSE D1_QTY END) AS D1_QTY" + "\n");
            strSqlString.Append("                                     , SUM(CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN D2_QTY/TO_NUMBER(DECODE(MAT_CMF_13,' ',1,MAT_CMF_13)) ELSE D2_QTY END) AS D2_QTY" + "\n");
            strSqlString.Append("                                     , SUM(CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN D3_QTY/TO_NUMBER(DECODE(MAT_CMF_13,' ',1,MAT_CMF_13)) ELSE D3_QTY END) AS D3_QTY" + "\n");
            strSqlString.Append("                                     , SUM(CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN D4_QTY/TO_NUMBER(DECODE(MAT_CMF_13,' ',1,MAT_CMF_13)) ELSE D4_QTY END) AS D4_QTY" + "\n");
            strSqlString.Append("                                     , SUM(CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN D5_QTY/TO_NUMBER(DECODE(MAT_CMF_13,' ',1,MAT_CMF_13)) ELSE D5_QTY END) AS D5_QTY" + "\n");
            strSqlString.Append("                                     , SUM(CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN D6_QTY/TO_NUMBER(DECODE(MAT_CMF_13,' ',1,MAT_CMF_13)) ELSE D6_QTY END) AS D6_QTY" + "\n");
            strSqlString.Append("                                     , SUM(CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN TTL_WIP/TO_NUMBER(DECODE(MAT_CMF_13,' ',1,MAT_CMF_13)) ELSE TTL_WIP END) AS TTL_WIP" + "\n");
            strSqlString.Append("                                  FROM (" + "\n");
            strSqlString.Append("                                        SELECT MAT_ID " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', WW_QTY, 0)) AS W1_QTY " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.NextWeek + "', WW_QTY, 0)) AS W2_QTY " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D0_QTY, 0)) AS D0_QTY " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D1_QTY, 0)) AS D1_QTY " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D2_QTY, 0)) AS D2_QTY " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D3_QTY, 0)) AS D3_QTY " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D4_QTY, 0)) AS D4_QTY " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D5_QTY, 0)) AS D5_QTY " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D6_QTY, 0)) AS D6_QTY " + "\n");
            strSqlString.Append("                                          FROM RWIPPLNWEK" + "\n");            
            strSqlString.Append("                                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                           AND GUBUN = '3' " + "\n");
            strSqlString.Append("                                           AND PLAN_WEEK IN ('" + FindWeek.ThisWeek + "', '" + FindWeek.NextWeek + "')" + "\n");
            strSqlString.Append("                                           AND MAT_ID NOT LIKE 'SES%' " + "\n");
            strSqlString.Append("                                         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("                                         UNION ALL" + "\n");

            // 2017-10-25-임종우 : 토,일의 경우 금주(토,일), 차주(월~금)의 데이터 사용함
            if (cdvDate.Value.DayOfWeek == DayOfWeek.Saturday || cdvDate.Value.DayOfWeek == DayOfWeek.Sunday)
            {
                strSqlString.Append("                                        SELECT MAT_ID " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', WW_QTY, 0)) AS W1_QTY " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.NextWeek + "', WW_QTY, 0)) AS W2_QTY " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D5_QTY, 0)) AS D0_QTY " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D6_QTY, 0)) AS D1_QTY " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.NextWeek + "', D0_QTY, 0)) AS D2_QTY " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.NextWeek + "', D1_QTY, 0)) AS D3_QTY " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.NextWeek + "', D2_QTY, 0)) AS D4_QTY " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.NextWeek + "', D3_QTY, 0)) AS D5_QTY " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.NextWeek + "', D4_QTY, 0)) AS D6_QTY " + "\n");
                strSqlString.Append("                                          FROM RWIPPLNWEK" + "\n");
                strSqlString.Append("                                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                           AND GUBUN = '3' " + "\n");
                strSqlString.Append("                                           AND PLAN_WEEK IN ('" + FindWeek.ThisWeek + "', '" + FindWeek.NextWeek + "')" + "\n");
                strSqlString.Append("                                           AND MAT_ID LIKE 'SES%' " + "\n");
                strSqlString.Append("                                         GROUP BY MAT_ID" + "\n");
            }
            else // 월~금의 경우 전주(토,일), 금주(월~금)의 데이터 사용함
            {
                strSqlString.Append("                                        SELECT MAT_ID " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', WW_QTY, 0)) AS W1_QTY " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.NextWeek + "', WW_QTY, 0)) AS W2_QTY " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.LastWeek + "', D5_QTY, 0)) AS D0_QTY " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.LastWeek + "', D6_QTY, 0)) AS D1_QTY " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D0_QTY, 0)) AS D2_QTY " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D1_QTY, 0)) AS D3_QTY " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D2_QTY, 0)) AS D4_QTY " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D3_QTY, 0)) AS D5_QTY " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D4_QTY, 0)) AS D6_QTY " + "\n");
                strSqlString.Append("                                          FROM RWIPPLNWEK" + "\n");
                strSqlString.Append("                                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                           AND GUBUN = '3' " + "\n");
                strSqlString.Append("                                           AND PLAN_WEEK IN ('" + FindWeek.LastWeek + "', '" + FindWeek.ThisWeek + "', '" + FindWeek.NextWeek + "')" + "\n");
                strSqlString.Append("                                           AND MAT_ID LIKE 'SES%' " + "\n");
                strSqlString.Append("                                         GROUP BY MAT_ID" + "\n");
            }
            strSqlString.Append("                                       ) PLN" + "\n");
            strSqlString.Append("                                     , (" + "\n");
            strSqlString.Append("                                        SELECT A.MAT_ID" + "\n");
            strSqlString.Append("                                             , HALF_WIP / NVL(DATA_1,1) AS HALF_WIP" + "\n");
            strSqlString.Append("                                             , STD_WIP" + "\n");
            strSqlString.Append("                                             , NVL(HALF_WIP / NVL(DATA_1,1),0) + NVL(STD_WIP, 0) AS TTL_WIP" + "\n");
            strSqlString.Append("                                          FROM (" + "\n");
            strSqlString.Append("                                                SELECT MAT_ID" + "\n");
            strSqlString.Append("                                                     , ROUND(SUM(CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') AND OPER BETWEEN 'A0000' AND 'A0395' THEN QTY_1/TO_NUMBER(DECODE(MAT_CMF_13,' ',1,MAT_CMF_13))" + "\n");
            strSqlString.Append("                                                                      WHEN OPER BETWEEN 'A0000' AND 'A0395' THEN QTY_1" + "\n");
            strSqlString.Append("                                                                      ELSE 0" + "\n");
            strSqlString.Append("                                                                 END),0) HALF_WIP" + "\n");
            strSqlString.Append("                                                     , ROUND(SUM(CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') AND OPER > 'A0395' THEN QTY_1/TO_NUMBER(DECODE(MAT_CMF_13,' ',1,MAT_CMF_13))" + "\n");
            strSqlString.Append("                                                                      WHEN OPER > 'A0395' THEN QTY_1" + "\n");
            strSqlString.Append("                                                                      ELSE 0" + "\n");
            strSqlString.Append("                                                                 END),0) STD_WIP" + "\n");
            strSqlString.Append("                                                  FROM (" + "\n");
            strSqlString.Append("                                                        SELECT OPER" + "\n");
            strSqlString.Append("                                                             , CASE WHEN B.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') " + "\n");
            strSqlString.Append("                                                                         THEN DECODE(B.MAT_GRP_5, '1st', 0, QTY_1)" + "\n");
            strSqlString.Append("                                                                    WHEN (B.MAT_GRP_5 IN ('-', '1st', 'Merge') OR B.MAT_GRP_5 LIKE 'Middle%') THEN QTY_1" + "\n");
            strSqlString.Append("                                                               END QTY_1" + "\n");
            strSqlString.Append("                                                             , B.*" + "\n");
            strSqlString.Append("                                                          FROM RWIPLOTSTS_BOH A" + "\n");
            strSqlString.Append("                                                             , MWIPMATDEF B" + "\n");
            strSqlString.Append("                                                         WHERE 1=1" + "\n");
            strSqlString.Append("                                                           AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                                                           AND A.MAT_ID = B.MAT_ID    " + "\n");
            strSqlString.Append("                                                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");

            //if (cdvTime.Text == "22시")
            if (cdvTime.SelectedIndex == 0)
            {
                strSqlString.Append("                                                           AND A.CUTOFF_DT = '" + FindWeek.EndDay_LastWeek + "22'" + "\n");
            }
            else
            {
                strSqlString.Append("                                                           AND A.CUTOFF_DT = '" + FindWeek.StartDay_ThisWeek + "06'" + "\n");
            }

            strSqlString.Append("                                                           AND A.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                                                           AND A.LOT_DEL_FLAG = ' '  " + "\n");
            strSqlString.Append("                                                           AND B.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                                                           AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
            strSqlString.Append("                                                           AND B.MAT_GRP_2 <> '-'" + "\n");
            strSqlString.Append("                                                           AND (B.MAT_GRP_5 IN ('-', '1st', '2nd', 'Merge') OR B.MAT_GRP_5 LIKE 'Middle%')" + "\n");
            strSqlString.Append("                                                       )" + "\n");
            strSqlString.Append("                                                 GROUP BY MAT_ID" + "\n");
            strSqlString.Append("                                               ) A" + "\n");
            strSqlString.Append("                                             , (" + "\n");
            strSqlString.Append("                                                SELECT KEY_1 AS MAT_ID, DATA_1" + "\n");
            strSqlString.Append("                                                  FROM MGCMTBLDAT " + "\n");
            strSqlString.Append("                                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                                   AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')" + "\n");
            strSqlString.Append("                                               ) B" + "\n");
            strSqlString.Append("                                         WHERE A.MAT_ID = B.MAT_ID(+) " + "\n");
            strSqlString.Append("                                       ) WIP" + "\n");
            strSqlString.Append("                                     , MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                                 WHERE 1=1" + "\n");
            strSqlString.Append("                                   AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
            strSqlString.Append("                                   AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");
            strSqlString.Append("                                   AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                   AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                   AND MAT.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                                 GROUP BY VENDOR_ID" + "\n");
            strSqlString.Append("                               )" + "\n");
            strSqlString.Append("                         WHERE W1_QTY > 0" + "\n");
            strSqlString.Append("                       ) A                 " + "\n");
            strSqlString.Append("               ) A" + "\n");
            strSqlString.Append("             , CUT_OFF B" + "\n");            
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND A.VENDOR_ID = B.SAP_CODE" + "\n");
            strSqlString.Append("       ) STD" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT VENDOR_ID, WORK_DATE, SUM(RCV_QTY) AS RCV_QTY" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT WORK_DATE, A.MAT_ID" + "\n");
            strSqlString.Append("                     , (SELECT VENDOR_ID FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_ID = A.MAT_ID) AS VENDOR_ID" + "\n");
            strSqlString.Append("                     , RCV_QTY_1 / NVL(DATA_1,1) AS RCV_QTY    " + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT WORK_DATE, MAT_ID" + "\n");
            strSqlString.Append("                             , ROUND(SUM(CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN RCV_QTY_1/TO_NUMBER(DECODE(MAT_CMF_13,' ',1,MAT_CMF_13))                              " + "\n");
            strSqlString.Append("                                              ELSE RCV_QTY_1" + "\n");
            strSqlString.Append("                                         END),0) AS RCV_QTY_1             " + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT WORK_DATE" + "\n");
            strSqlString.Append("                                     , CASE WHEN B.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') " + "\n");
            strSqlString.Append("                                                 THEN DECODE(B.MAT_GRP_5, '1st', 0, RCV_QTY_1)" + "\n");
            strSqlString.Append("                                            WHEN B.MAT_GRP_5 IN ('-', '1st') THEN RCV_QTY_1" + "\n");
            strSqlString.Append("                                       END RCV_QTY_1" + "\n");
            strSqlString.Append("                                     , B.* " + "\n");

            //if (cdvTime.Text == "22시")
            if (cdvTime.SelectedIndex == 0)
            {
                strSqlString.Append("                                  FROM VSUMWIPRCV A" + "\n");
            }
            else
            {
                strSqlString.Append("                                  FROM VSUMWIPRCV_HX A" + "\n");
            }

            strSqlString.Append("                                     , MWIPMATDEF B" + "\n");
            strSqlString.Append("                                 WHERE 1=1" + "\n");
            strSqlString.Append("                                   AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                                   AND A.MAT_ID = B.MAT_ID    " + "\n");
            strSqlString.Append("                                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                   AND A.WORK_DATE BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + Today + "'" + "\n");
            strSqlString.Append("                                   AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                                   AND B.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                                   AND A.CM_KEY_3 LIKE 'P%'" + "\n");
            strSqlString.Append("                                   AND B.MAT_GRP_2 <> '-'" + "\n");
            strSqlString.Append("                                   AND B.MAT_GRP_5 IN ('-', '1st', '2nd')" + "\n");
            strSqlString.Append("                               )" + "\n");
            strSqlString.Append("                         GROUP BY WORK_DATE, MAT_ID" + "\n");
            strSqlString.Append("                       ) A" + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT KEY_1 AS MAT_ID, DATA_1" + "\n");
            strSqlString.Append("                          FROM MGCMTBLDAT " + "\n");
            strSqlString.Append("                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')" + "\n");
            strSqlString.Append("                       ) B" + "\n");
            strSqlString.Append("                 WHERE" + "\n");
            strSqlString.Append("                 A.MAT_ID NOT IN (SELECT MAT_ID FROM MWIPMATDEF WHERE FIRST_FLOW = 'A-BANK' AND DELETE_FLAG = ' ')" + "\n");
            strSqlString.Append("                 AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY VENDOR_ID, WORK_DATE" + "\n");
            strSqlString.Append("       ) RCV" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT VENDOR_ID, WORK_DATE, SUM(SHP_QTY_1) AS SHP_QTY" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT WORK_DATE" + "\n");
            strSqlString.Append("                     , (SELECT VENDOR_ID FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_ID = A.MAT_ID) AS VENDOR_ID" + "\n");
            strSqlString.Append("                     , A.MAT_ID " + "\n");
            strSqlString.Append("                     , CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN (S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1)/TO_NUMBER(DECODE(MAT_CMF_13,' ',1,MAT_CMF_13))" + "\n");
            strSqlString.Append("                            ELSE S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1 " + "\n");
            strSqlString.Append("                       END AS SHP_QTY_1 " + "\n");

            //if (cdvTime.Text == "22시")           
            if (cdvTime.SelectedIndex == 0)
            {
                strSqlString.Append("                  FROM RSUMFACMOV A" + "\n");
            }
            else
            {
                strSqlString.Append("                  FROM CSUMFACMOV A" + "\n");
            }

            strSqlString.Append("                     , MWIPMATDEF B" + "\n");
            strSqlString.Append("                 WHERE 1 = 1 " + "\n");
            strSqlString.Append("                   AND A.MAT_ID NOT IN (SELECT MAT_ID FROM MWIPMATDEF WHERE FIRST_FLOW = 'A-BANK' AND DELETE_FLAG = ' ')" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                   AND A.CM_KEY_1 = B.FACTORY" + "\n");
            strSqlString.Append("                   AND A.WORK_DATE BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + Today + "'" + "\n");
            strSqlString.Append("                   AND A.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                   AND A.CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND A.CM_KEY_2 = 'PROD'" + "\n");
            strSqlString.Append("                   AND A.CM_KEY_3 LIKE 'P%'" + "\n");
            strSqlString.Append("                   AND A.FACTORY NOT IN ('RETURN')" + "\n");
            strSqlString.Append("                   AND S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1 > 0" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY VENDOR_ID, WORK_DATE " + "\n");
            strSqlString.Append("       ) SHP" + "\n");
            strSqlString.Append(" WHERE 1=1   " + "\n");
            strSqlString.Append("   AND STD.WORK_DATE  = RCV.WORK_DATE(+)" + "\n");
            strSqlString.Append("   AND STD.WORK_DATE = SHP.WORK_DATE(+)   " + "\n");
            strSqlString.Append("   AND STD.VENDOR_ID = RCV.VENDOR_ID(+)" + "\n");
            strSqlString.Append("   AND STD.VENDOR_ID = SHP.VENDOR_ID(+)   " + "\n");
            strSqlString.Append(")" + "\n");
            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond3);
            strSqlString.Append("     , ROUND(NVL(SUM(W1_QTY), 0) / " + sKpcsValue + ", 0) AS W1_QTY" + "\n");
            strSqlString.Append("     , ROUND(NVL(SUM(TTL_WIP), 0) / " + sKpcsValue + ", 0) AS TTL_WIP" + "\n");
            strSqlString.Append("     , ROUND(NVL(SUM(WF_DEF), 0) / " + sKpcsValue + ", 0) AS WF_DEF" + "\n");
            strSqlString.Append("     , GUBUN_1" + "\n");
            strSqlString.Append("     , GUBUN_2" + "\n");
            strSqlString.Append("     , ROUND(SUM(D0) / " + sKpcsValue + ", 0) AS D0" + "\n");
            strSqlString.Append("     , ROUND(CASE WHEN GUBUN_2 IN ('과부족','누계차') AND TO_CHAR(TO_DATE('" + Today + "','YYYYMMDD'),'D') = 7 THEN 0 ELSE ROUND(SUM(D1), 0) END / " + sKpcsValue + ", 0) AS D1" + "\n");
            strSqlString.Append("     , ROUND(CASE WHEN GUBUN_2 IN ('과부족','누계차') AND TO_CHAR(TO_DATE('" + Today + "','YYYYMMDD'),'D') IN (7,1) THEN 0 ELSE ROUND(SUM(D2), 0) END / " + sKpcsValue + ", 0) AS D2" + "\n");
            strSqlString.Append("     , ROUND(CASE WHEN GUBUN_2 IN ('과부족','누계차') AND TO_CHAR(TO_DATE('" + Today + "','YYYYMMDD'),'D') IN (7,1,2) THEN 0 ELSE ROUND(SUM(D3), 0) END / " + sKpcsValue + ", 0) AS D3" + "\n");
            strSqlString.Append("     , ROUND(CASE WHEN GUBUN_2 IN ('과부족','누계차') AND TO_CHAR(TO_DATE('" + Today + "','YYYYMMDD'),'D') IN (7,1,2,3) THEN 0 ELSE ROUND(SUM(D4), 0) END / " + sKpcsValue + ", 0) AS D4" + "\n");
            strSqlString.Append("     , ROUND(CASE WHEN GUBUN_2 IN ('과부족','누계차') AND TO_CHAR(TO_DATE('" + Today + "','YYYYMMDD'),'D') IN (7,1,2,3,4) THEN 0 ELSE ROUND(SUM(D5), 0) END / " + sKpcsValue + ", 0) AS D5" + "\n");
            strSqlString.Append("     , ROUND(CASE WHEN GUBUN_2 IN ('과부족','누계차') AND TO_CHAR(TO_DATE('" + Today + "','YYYYMMDD'),'D') IN (7,1,2,3,4,5) THEN 0 ELSE ROUND(SUM(D6), 0) END / " + sKpcsValue + ", 0) AS D6" + "\n");
            strSqlString.Append("     , ROUND(CASE WHEN GUBUN_2 IN ('과부족','누계차') AND TO_CHAR(TO_DATE('" + Today + "','YYYYMMDD'),'D') = 7 THEN ROUND(SUM(D0), 0)" + "\n");
            strSqlString.Append("            WHEN GUBUN_2 = '누계차' AND TO_CHAR(TO_DATE('" + Today + "','YYYYMMDD'),'D') = 1 THEN ROUND(SUM(D1), 0)" + "\n");
            strSqlString.Append("            WHEN GUBUN_2 = '누계차' AND TO_CHAR(TO_DATE('" + Today + "','YYYYMMDD'),'D') = 2 THEN ROUND(SUM(D2), 0)" + "\n");
            strSqlString.Append("            WHEN GUBUN_2 = '누계차' AND TO_CHAR(TO_DATE('" + Today + "','YYYYMMDD'),'D') = 3 THEN ROUND(SUM(D3), 0)" + "\n");
            strSqlString.Append("            WHEN GUBUN_2 = '누계차' AND TO_CHAR(TO_DATE('" + Today + "','YYYYMMDD'),'D') = 4 THEN ROUND(SUM(D4), 0)" + "\n");
            strSqlString.Append("            WHEN GUBUN_2 = '누계차' AND TO_CHAR(TO_DATE('" + Today + "','YYYYMMDD'),'D') = 5 THEN ROUND(SUM(D5), 0)" + "\n");
            strSqlString.Append("            WHEN GUBUN_2 = '누계차' AND TO_CHAR(TO_DATE('" + Today + "','YYYYMMDD'),'D') = 6 THEN ROUND(SUM(D6), 0)" + "\n");
            strSqlString.Append("            ELSE ROUND(NVL(SUM(D0),0) + NVL(SUM(D1),0) + NVL(SUM(D2),0) + NVL(SUM(D3),0) + NVL(SUM(D4),0) + NVL(SUM(D5),0) + NVL(SUM(D6),0), 0)" + "\n");
            strSqlString.Append("       END / " + sKpcsValue + ", 0) AS TTL" + "\n");
            strSqlString.Append("     , MAX(CKD_DAY) AS CKD_DAY" + "\n"); 
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT VENDOR_ID, SUM(W1_QTY) AS W1_QTY, SUM(TTL_WIP) AS TTL_WIP, SUM(WF_DEF) AS WF_DEF, MAX(CKD_DAY) AS CKD_DAY" + "\n");
            strSqlString.Append("          FROM DT" + "\n");
            strSqlString.Append("         GROUP BY VENDOR_ID" + "\n");
            strSqlString.Append("       ) STD" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT VENDOR_ID" + "\n");
            strSqlString.Append("             , 'WAFER' AS GUBUN_1" + "\n");
            strSqlString.Append("             , '계획' AS GUBUN_2" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[0].ToString() + "', TTL_WEEK_NEED)) AS D0" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[1].ToString() + "', TTL_WEEK_NEED)) AS D1" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[2].ToString() + "', TTL_WEEK_NEED)) AS D2" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[3].ToString() + "', TTL_WEEK_NEED)) AS D3" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[4].ToString() + "', TTL_WEEK_NEED)) AS D4" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[5].ToString() + "', TTL_WEEK_NEED)) AS D5" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[6].ToString() + "', TTL_WEEK_NEED)) AS D6" + "\n");
            strSqlString.Append("          FROM DT" + "\n");
            strSqlString.Append("         GROUP BY VENDOR_ID" + "\n");
            strSqlString.Append("         UNION ALL" + "\n");
            strSqlString.Append("        SELECT VENDOR_ID" + "\n");
            strSqlString.Append("             , 'WAFER' AS GUBUN_1" + "\n");
            strSqlString.Append("             , '실적' AS GUBUN_2" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[0].ToString() + "', RCV_QTY)) AS D0" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[1].ToString() + "', RCV_QTY)) AS D1" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[2].ToString() + "', RCV_QTY)) AS D2" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[3].ToString() + "', RCV_QTY)) AS D3" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[4].ToString() + "', RCV_QTY)) AS D4" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[5].ToString() + "', RCV_QTY)) AS D5" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[6].ToString() + "', RCV_QTY)) AS D6" + "\n");
            strSqlString.Append("          FROM DT" + "\n");
            strSqlString.Append("         GROUP BY VENDOR_ID" + "\n");
            strSqlString.Append("         UNION ALL" + "\n"); ;
            strSqlString.Append("        SELECT VENDOR_ID" + "\n");
            strSqlString.Append("             , 'WAFER' AS GUBUN_1" + "\n");
            strSqlString.Append("             , '과부족' AS GUBUN_2" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[0].ToString() + "', SHORT_WF)) AS D0" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[1].ToString() + "', SHORT_WF)) AS D1" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[2].ToString() + "', SHORT_WF)) AS D2" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[3].ToString() + "', SHORT_WF)) AS D3" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[4].ToString() + "', SHORT_WF)) AS D4" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[5].ToString() + "', SHORT_WF)) AS D5" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[6].ToString() + "', SHORT_WF)) AS D6" + "\n");
            strSqlString.Append("          FROM DT" + "\n");
            strSqlString.Append("         GROUP BY VENDOR_ID" + "\n");
            strSqlString.Append("         UNION ALL" + "\n"); ;
            strSqlString.Append("        SELECT VENDOR_ID" + "\n");
            strSqlString.Append("             , 'WAFER' AS GUBUN_1" + "\n");
            strSqlString.Append("             , '누계차' AS GUBUN_2" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[0].ToString() + "', SUM_SHORT_WF)) AS D0" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[1].ToString() + "', SUM_SHORT_WF)) AS D1" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[2].ToString() + "', SUM_SHORT_WF)) AS D2" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[3].ToString() + "', SUM_SHORT_WF)) AS D3" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[4].ToString() + "', SUM_SHORT_WF)) AS D4" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[5].ToString() + "', SUM_SHORT_WF)) AS D5" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[6].ToString() + "', SUM_SHORT_WF)) AS D6" + "\n");
            strSqlString.Append("          FROM DT" + "\n");
            strSqlString.Append("         GROUP BY VENDOR_ID" + "\n");
            strSqlString.Append("         UNION ALL" + "\n");
            strSqlString.Append("        SELECT VENDOR_ID" + "\n");
            strSqlString.Append("             , '일출하' AS GUBUN_1" + "\n");
            strSqlString.Append("             , '계획' AS GUBUN_2" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[0].ToString() + "', PLN_QTY)) AS D0" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[1].ToString() + "', PLN_QTY)) AS D1" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[2].ToString() + "', PLN_QTY)) AS D2" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[3].ToString() + "', PLN_QTY)) AS D3" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[4].ToString() + "', PLN_QTY)) AS D4" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[5].ToString() + "', PLN_QTY)) AS D5" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[6].ToString() + "', PLN_QTY)) AS D6" + "\n");
            strSqlString.Append("          FROM DT" + "\n");
            strSqlString.Append("         GROUP BY VENDOR_ID" + "\n");
            strSqlString.Append("         UNION ALL" + "\n");
            strSqlString.Append("        SELECT VENDOR_ID" + "\n");
            strSqlString.Append("             , '일출하' AS GUBUN_1" + "\n");
            strSqlString.Append("             , '실적' AS GUBUN_2" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[0].ToString() + "', SHP_QTY)) AS D0" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[1].ToString() + "', SHP_QTY)) AS D1" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[2].ToString() + "', SHP_QTY)) AS D2" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[3].ToString() + "', SHP_QTY)) AS D3" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[4].ToString() + "', SHP_QTY)) AS D4" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[5].ToString() + "', SHP_QTY)) AS D5" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[6].ToString() + "', SHP_QTY)) AS D6" + "\n");
            strSqlString.Append("          FROM DT" + "\n");
            strSqlString.Append("         GROUP BY VENDOR_ID" + "\n");
            strSqlString.Append("         UNION ALL" + "\n");
            strSqlString.Append("        SELECT VENDOR_ID" + "\n");
            strSqlString.Append("             , '일출하' AS GUBUN_1" + "\n");
            strSqlString.Append("             , '과부족' AS GUBUN_2" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[0].ToString() + "', SHORT_SHP)) AS D0" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[1].ToString() + "', SHORT_SHP)) AS D1" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[2].ToString() + "', SHORT_SHP)) AS D2" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[3].ToString() + "', SHORT_SHP)) AS D3" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[4].ToString() + "', SHORT_SHP)) AS D4" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[5].ToString() + "', SHORT_SHP)) AS D5" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[6].ToString() + "', SHORT_SHP)) AS D6" + "\n");
            strSqlString.Append("          FROM DT" + "\n");
            strSqlString.Append("         GROUP BY VENDOR_ID" + "\n");
            strSqlString.Append("         UNION ALL" + "\n");
            strSqlString.Append("        SELECT VENDOR_ID" + "\n");
            strSqlString.Append("             , '일출하' AS GUBUN_1" + "\n");
            strSqlString.Append("             , '누계차' AS GUBUN_2" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[0].ToString() + "', SUM_SHORT_SHP)) AS D0" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[1].ToString() + "', SUM_SHORT_SHP)) AS D1" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[2].ToString() + "', SUM_SHORT_SHP)) AS D2" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[3].ToString() + "', SUM_SHORT_SHP)) AS D3" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[4].ToString() + "', SUM_SHORT_SHP)) AS D4" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[5].ToString() + "', SUM_SHORT_SHP)) AS D5" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArray[6].ToString() + "', SUM_SHORT_SHP)) AS D6" + "\n");
            strSqlString.Append("          FROM DT" + "\n");
            strSqlString.Append("         GROUP BY VENDOR_ID" + "\n");
            strSqlString.Append("       ) DAT" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.AppendFormat("        SELECT DISTINCT {0}, VENDOR_ID " + "\n", QueryCond2);            
            strSqlString.Append("          FROM MWIPMATDEF" + "\n");
            strSqlString.Append("         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("           AND DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("           AND VENDOR_ID <> ' '" + "\n");
            strSqlString.Append("           AND SUBSTR(VENDOR_ID, 5, 4) <> '0000'" + "\n");
            strSqlString.Append("           AND MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");

            //상세 조회에 따른 SQL문 생성                                    
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.Append("       ) MAT" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND MAT.VENDOR_ID = STD.VENDOR_ID " + "\n");
            strSqlString.Append("   AND MAT.VENDOR_ID = DAT.VENDOR_ID(+) " + "\n");

            if (cdvType.Text != "ALL")
            {
                strSqlString.Append("   AND DAT.GUBUN_1(+) = '" + cdvType.Text + "' " + "\n");
            }

            strSqlString.AppendFormat(" GROUP BY {0}, GUBUN_1, GUBUN_2 " + "\n", QueryCond1);
            strSqlString.AppendFormat(" ORDER BY {0}, GUBUN_1, DECODE(GUBUN_2, '계획', 1, '실적', 2, '과부족', 3, 4) " + "\n", QueryCond1);            

           if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion


        #region EVENT 처리
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            if (CheckField() == false) return;

            GridColumnInit();            

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 12, null, null, btnSort);

                //Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 12, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.DataSource = dt;                
                spdData.RPT_AutoFit(false);

                for (int i = 0; i <= 13; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LemonChiffon;
                }
                                
                // WF 잔량에 따른 WAFER 계획 색깔 표시 (차주계획 사용시 : red)
                for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i = i + 8)
                {                    
                    int iStartColumn = 0;
                    int iEndColumn = 20;
                    int iCutOff = 0;
                    

                    iCutOff = Convert.ToInt32(spdData.ActiveSheet.Cells[i, 22].Value);

                    if (iCutOff == 7)
                    {
                        iStartColumn = 15;                        
                    }
                    else if (iCutOff == 1)
                    {
                        iStartColumn = 16;                        
                    }
                    else if (iCutOff == 2)
                    {
                        iStartColumn = 17;                        
                    }
                    else if (iCutOff == 3)
                    {
                        iStartColumn = 18;                        
                    }
                    else if (iCutOff == 4)
                    {
                        iStartColumn = 19;                        
                    }
                    else if (iCutOff == 5)
                    {
                        iStartColumn = 20;                        
                    }
                    else
                    {
                        continue;
                    }

                    for (int y = iStartColumn; y <= iEndColumn; y++)
                    {
                        if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, y].Value) != 0)
                        {
                            //spdData.ActiveSheet.Cells[i, y].ForeColor = Color.Red;
                            spdData.ActiveSheet.Cells[i, y].BackColor = Color.Pink;
                        }
                    }
                                          
                   
                }


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
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.ActiveSheet.Rows.Count > 0)
            {
                StringBuilder Condition = new StringBuilder();                

                ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);
                //(데이타, 제목(타이틀), 좌측문구, 우측문구, 자동사이즈조정)

            }
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            //spdData.ExportExcel();            
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);

            if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                cdvTime.Enabled = true;
            else cdvTime.Enabled = false;
        }
        #endregion
    }
}
