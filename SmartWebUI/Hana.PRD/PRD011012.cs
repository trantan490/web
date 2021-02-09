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
    public partial class PRD011012 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD011012<br/>
        /// 클래스요약: 일일 실행력 지수<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2013-10-23<br/>
        /// 상세  설명: 일일 실행력 지수(임태성 요청)<br/>
        /// 변경  내용: <br/>
        /// 2013-11-11-임종우 : LIPAS(ITEM) 계획 부분 누계 차질 로직 포함
        /// 2013-11-13-임종우 : 실적부분 조회일자까지 계산되도록 수정. Total 부분도 조회일자까지 계산되도록 수정
        /// 2013-11-15-임종우 : COB 제품 제외, SEC --> Hynix --> Fabless --> HT-micron 순서대로 정렬, LIPAS & VOLPAS 용어 설명 표시 (임태성 요청)
        /// 2013-11-15-임종우 : LIPAS(ITEM) 달성율 기준을 99.5 -> 98.5 로 변경 (임태성 요청)
        /// 2013-11-19-김민우 : 주차 오류 OTD인데 SE로 해놨음 종우선배가!!!! (임태성 요청)
        /// 2013-11-19-김민우 : 조회한 날짜기준 미래 구간은 ERP Fix 계획 그대로.
        ///                     과거 및 현재 계획 ERP Fix 계획 + 전일 차질 또는 선행 계획 값으로 나오면 됨 [현재 logic 그대로..](임태성 요청)
        /// 2014-03-06-임종우 : HMKT 부분도 추가 (박민정 요청)
        /// 2014-04-01-임종우 : HMKT 삼성 계획 OMS 계획 사용으로 인한 과거 데이터 오류 부분 수정
        /// 2014-04-08-임종우 : MACOM 제품의 경우 Product 상 '-' 앞 + PKG CODE 기준으로 MAT_ID 사용 (박민정 요청)
        /// 2014-04-29-임종우 : FCI 제품의 경우 Product 상 '-' 앞 + PKG CODE 기준으로 MAT_ID 사용 (박민정 요청)
        /// 2014-06-27-임종우 : FREESCALE 제품의 경우 Product 상 앞에서 5자리 + 뒤에서 3자리를 기준으로 MAT_ID 사용 (박민정D 요청)
        /// 2016-04-25-임종우 : 계획 값 산출 시 SEC 제품은 예외적으로 원래 계획 표시 되는 부분 삭제함. 모든 제품 동일한 로직으로 계획 표시 (임태성K 요청)
        /// 2016-04-27-임종우 : 실적 값 산출 시 SEC 제품은 예외적으로 원래 계획 표시 되는 부분 삭제함. 모든 제품 동일한 로직으로 계획 표시 (임태성K 요청)
        /// 2016-09-28-임종우 : 일별 누계 값 추가, 계획 값 3가지 버전 추가, 삼우 MAT_ID -> SALES_CODE로 표시 (임태성K 요청)
        /// 2017-11-15-임종우 : S-LSI 주간 계획 월~일 계획 -> 토~금 로 변경 (최연희D 요청)
        /// 2018-01-17-임종우 : AO 실적을 Close 실적으로 변경 (임태성C 요청)
        /// 2019-03-13-임종우 : Close 실적을 다시 AO 실적으로 변경 (임태성차장 요청)
        /// 2020-03-18-김미경 : VOLPAS 고정 PLAN 수정; PLAN 등록되지 않은 PART 출하실적 가지고 오지 못하는 오류 수정 (한선호 S)
        /// 2020-03-19-김미경 : Lipas(Volume); 계획 대비 실적이 초과하는 경우 초과한 수량은 달성률 계산에 포함하지 않음. (한선호 S)
        /// </summary>

        DataTable DateDT = null;
        GlobalVariable.FindWeek FindWeek = new GlobalVariable.FindWeek();

        public PRD011012()
        {
            InitializeComponent();
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;

            //udcWIPCondition1.Text = "SE";
            //udcWIPCondition1.Enabled = false;
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
            GetDayArray();

            if ( comboBox1.SelectedItem.ToString() == "월요일")
            {
                FindWeek = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "SE");
            }
            else
            {
                FindWeek = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");
            }

            spdData.RPT_ColumnInit();            

            try
            {
                spdData.RPT_AddBasicColumn("TYPE", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Customer", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Major", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Family", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LD Count", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Package", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("MAT_ID", 0, 7, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 180);
                spdData.RPT_AddBasicColumn("PKG Code", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("구분", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);               

                spdData.RPT_AddBasicColumn(DateDT.Rows[0][1].ToString(), 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(DateDT.Rows[0][3].ToString(), 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(DateDT.Rows[1][1].ToString(), 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(DateDT.Rows[1][3].ToString(), 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(DateDT.Rows[2][1].ToString(), 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(DateDT.Rows[2][3].ToString(), 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(DateDT.Rows[3][1].ToString(), 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(DateDT.Rows[3][3].ToString(), 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(DateDT.Rows[4][1].ToString(), 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(DateDT.Rows[4][3].ToString(), 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(DateDT.Rows[5][1].ToString(), 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(DateDT.Rows[5][3].ToString(), 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(DateDT.Rows[6][1].ToString(), 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(DateDT.Rows[6][3].ToString(), 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("WW" + FindWeek.ThisWeek.Substring(4, 2) + " TOTAL", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Daily run", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Weekly run", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 17, 2);

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 8, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 9, 2);                

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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type", "DECODE(CUST_TYPE, 'SEC', 1, 'Hynix', 2, 'Fabless', 3, 4)", "CUST_TYPE", "CUST_TYPE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Major", "MAT_GRP_9", "MAT_GRP_9", "MAT_GRP_9", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_10", "MAT_GRP_10", "MAT_GRP_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Mat ID", "CONV_MAT_ID", "CONV_MAT_ID", "CONV_MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG Code", "MAT_CMF_11", "MAT_CMF_11", "MAT_CMF_11", false);
        }
        #endregion


        #region 금주 토요일부터 금요일 일자 가져오기
        private void GetDayArray()
        {            
            StringBuilder strSqlString = new StringBuilder();

            string sCalendar = null;
            //1191214 이희석 : 조회기준 따라 날짜 변경
            if (comboBox1.SelectedItem.ToString() == "월요일")
            {
                sCalendar = "SE";
            }
            else
            {
                sCalendar = "OTD";
            }

            strSqlString.Append("SELECT SYS_DATE");
            strSqlString.Append("     , SUBSTR(SYS_DATE, 5, 2)||'월 '||SUBSTR(SYS_DATE, 7, 2)||'일' AS CONV");
            strSqlString.Append("     , TO_CHAR(TO_DATE('" + cdvDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') AS DY");
            strSqlString.Append("     , TO_CHAR(TO_DATE(SYS_DATE, 'YYYYMMDD'), 'DY') AS DY2");
            strSqlString.Append("  FROM MWIPCALDEF");
            strSqlString.Append(" WHERE CALENDAR_ID = '" + sCalendar + "'");            
            strSqlString.Append("   AND (PLAN_YEAR, PLAN_WEEK) = (SELECT PLAN_YEAR, PLAN_WEEK FROM MWIPCALDEF WHERE CALENDAR_ID = '" + sCalendar + "' AND SYS_DATE = '" + cdvDate.Value.ToString("yyyyMMdd") + "') ");
            strSqlString.Append(" ORDER BY SYS_DATE");

            DateDT = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
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
            string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값
            string ckdDay;
            string sWeekTotal_1;
            string sWeekTotal_2;
            string[] sDayArray = new string[7];

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            ckdDay = DateDT.Rows[0][2].ToString();

            // TEST는 시작이 월요일, ASSY는 토요일
            //1191214 이희석 : 조회기준 따라 날짜 변경
            if (comboBox1.SelectedItem.ToString() =="월요일")
            {
                sDayArray[0] = "월";
                sDayArray[1] = "화";
                sDayArray[2] = "수";
                sDayArray[3] = "목";
                sDayArray[4] = "금";
                sDayArray[5] = "토";
                sDayArray[6] = "일";
            }
            else
            {
                sDayArray[0] = "토";
                sDayArray[1] = "일";
                sDayArray[2] = "월";
                sDayArray[3] = "화";
                sDayArray[4] = "수";
                sDayArray[5] = "목";
                sDayArray[6] = "금";
            }

            if (ckdDay == sDayArray[0])
            {
                sWeekTotal_1 = "D0_PLN";
                sWeekTotal_2 = "D0_QTY";
            }
            else if (ckdDay == sDayArray[1])
            {
                sWeekTotal_1 = "D0_PLN + D1_PLN";
                sWeekTotal_2 = "D0_QTY + D1_QTY";
            }
            else if (ckdDay == sDayArray[2])
            {
                sWeekTotal_1 = "D0_PLN + D1_PLN + D2_PLN";
                sWeekTotal_2 = "D0_QTY + D1_QTY + D2_QTY";
            }
            else if (ckdDay == sDayArray[3])
            {
                sWeekTotal_1 = "D0_PLN + D1_PLN + D2_PLN + D3_PLN";
                sWeekTotal_2 = "D0_QTY + D1_QTY + D2_QTY + D3_QTY";
            }
            else if (ckdDay == sDayArray[4])
            {
                sWeekTotal_1 = "D0_PLN + D1_PLN + D2_PLN + D3_PLN + D4_PLN";
                sWeekTotal_2 = "D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY";
            }
            else if (ckdDay == sDayArray[5])
            {
                sWeekTotal_1 = "D0_PLN + D1_PLN + D2_PLN + D3_PLN + D4_PLN + D5_PLN";
                sWeekTotal_2 = "D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY";
            }
            else
            {
                sWeekTotal_1 = "D0_PLN + D1_PLN + D2_PLN + D3_PLN + D4_PLN + D5_PLN + D6_PLN";
                sWeekTotal_2 = "D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY + D6_QTY";
            }

            // kpcs 선택에 의한 분모 값 저장 한다.
            if (ckbKpcs.Checked == true)
            {
                sKpcsValue = "1000";
            }
            else
            {
                sKpcsValue = "1";
            }
            
            
            strSqlString.Append("WITH DT AS" + "\n");
            strSqlString.Append("(" + "\n");
            strSqlString.Append(" SELECT MAT_ID" + "\n");
            strSqlString.Append("      , SUM(DECODE(WORK_DATE, '" + DateDT.Rows[0][0].ToString() + "', SHP_QTY, 0)) AS D0_SHP" + "\n");
            strSqlString.Append("      , SUM(DECODE(WORK_DATE, '" + DateDT.Rows[1][0].ToString() + "', SHP_QTY, 0)) AS D1_SHP" + "\n");
            strSqlString.Append("      , SUM(DECODE(WORK_DATE, '" + DateDT.Rows[2][0].ToString() + "', SHP_QTY, 0)) AS D2_SHP" + "\n");
            strSqlString.Append("      , SUM(DECODE(WORK_DATE, '" + DateDT.Rows[3][0].ToString() + "', SHP_QTY, 0)) AS D3_SHP" + "\n");
            strSqlString.Append("      , SUM(DECODE(WORK_DATE, '" + DateDT.Rows[4][0].ToString() + "', SHP_QTY, 0)) AS D4_SHP" + "\n");
            strSqlString.Append("      , SUM(DECODE(WORK_DATE, '" + DateDT.Rows[5][0].ToString() + "', SHP_QTY, 0)) AS D5_SHP" + "\n");
            strSqlString.Append("      , SUM(DECODE(WORK_DATE, '" + DateDT.Rows[6][0].ToString() + "', SHP_QTY, 0)) AS D6_SHP" + "\n");
            strSqlString.Append("      , SUM(SHP_QTY) AS WW_SHP" + "\n");
            strSqlString.Append("      , SUM(CASE WHEN WORK_DATE <= '" + DateDT.Rows[0][0].ToString() + "' THEN SHP_QTY ELSE 0 END) AS SUM_D0_SHP" + "\n");
            strSqlString.Append("      , SUM(CASE WHEN WORK_DATE <= '" + DateDT.Rows[1][0].ToString() + "' THEN SHP_QTY ELSE 0 END) AS SUM_D1_SHP" + "\n");
            strSqlString.Append("      , SUM(CASE WHEN WORK_DATE <= '" + DateDT.Rows[2][0].ToString() + "' THEN SHP_QTY ELSE 0 END) AS SUM_D2_SHP" + "\n");
            strSqlString.Append("      , SUM(CASE WHEN WORK_DATE <= '" + DateDT.Rows[3][0].ToString() + "' THEN SHP_QTY ELSE 0 END) AS SUM_D3_SHP" + "\n");
            strSqlString.Append("      , SUM(CASE WHEN WORK_DATE <= '" + DateDT.Rows[4][0].ToString() + "' THEN SHP_QTY ELSE 0 END) AS SUM_D4_SHP" + "\n");
            strSqlString.Append("      , SUM(CASE WHEN WORK_DATE <= '" + DateDT.Rows[5][0].ToString() + "' THEN SHP_QTY ELSE 0 END) AS SUM_D5_SHP" + "\n");
            strSqlString.Append("      , SUM(CASE WHEN WORK_DATE <= '" + DateDT.Rows[6][0].ToString() + "' THEN SHP_QTY ELSE 0 END) AS SUM_D6_SHP" + "\n");            
            strSqlString.Append("   FROM (" + "\n");

            //strSqlString.Append("         SELECT WORK_DATE, MAT_ID" + "\n");
            //strSqlString.Append("              , SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1) AS SHP_QTY " + "\n");
            //strSqlString.Append("           FROM RSUMWIPMOV" + "\n");
            //strSqlString.Append("          WHERE 1=1" + "\n");
            //strSqlString.Append("            AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            //strSqlString.Append("            AND WORK_DATE BETWEEN '" + DateDT.Rows[0][0].ToString() + "' AND '" + cdvDate.Value.ToString("yyyyMMdd") + "'" + "\n");
            //strSqlString.Append("            AND LOT_TYPE = 'W' " + "\n");
            //strSqlString.Append("            AND OPER = 'A2100' " + "\n");
            //strSqlString.Append("            AND MAT_ID NOT LIKE 'HX%'" + "\n");
            //strSqlString.Append("          GROUP BY WORK_DATE, MAT_ID" + "\n");
            //strSqlString.Append("          UNION ALL" + "\n");
            //strSqlString.Append("         SELECT WORK_DATE, MAT_ID" + "\n");
            //strSqlString.Append("              , SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1) AS SHP_QTY " + "\n");
            //strSqlString.Append("           FROM CSUMWIPMOV" + "\n");
            //strSqlString.Append("          WHERE 1=1" + "\n");
            //strSqlString.Append("            AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            //strSqlString.Append("            AND WORK_DATE BETWEEN '" + DateDT.Rows[0][0].ToString() + "' AND '" + cdvDate.Value.ToString("yyyyMMdd") + "'" + "\n");
            //strSqlString.Append("            AND LOT_TYPE = 'W' " + "\n");
            //strSqlString.Append("            AND OPER = 'A2100' " + "\n");
            //strSqlString.Append("            AND MAT_ID LIKE 'HX%'" + "\n");
            //strSqlString.Append("          GROUP BY WORK_DATE, MAT_ID" + "\n"); 

            strSqlString.Append("         SELECT WORK_DATE, MAT_ID" + "\n");
            strSqlString.Append("              , SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) AS SHP_QTY " + "\n");
            strSqlString.Append("           FROM RSUMFACMOV" + "\n");
            strSqlString.Append("          WHERE 1=1" + "\n");
            strSqlString.Append("            AND WORK_DATE BETWEEN '" + DateDT.Rows[0][0].ToString() + "' AND '" + cdvDate.Value.ToString("yyyyMMdd") + "'" + "\n");
            strSqlString.Append("            AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("            AND CM_KEY_1 = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("            AND FACTORY NOT IN ('RETURN') " + "\n");
            strSqlString.Append("            AND MAT_ID NOT LIKE 'HX%'" + "\n");
            strSqlString.Append("          GROUP BY WORK_DATE, MAT_ID" + "\n");
            strSqlString.Append("          UNION ALL" + "\n");
            strSqlString.Append("         SELECT WORK_DATE, MAT_ID" + "\n");
            strSqlString.Append("              , SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) AS SHP_QTY " + "\n");
            strSqlString.Append("           FROM CSUMFACMOV" + "\n");
            strSqlString.Append("          WHERE 1=1" + "\n");
            strSqlString.Append("            AND WORK_DATE BETWEEN '" + DateDT.Rows[0][0].ToString() + "' AND '" + cdvDate.Value.ToString("yyyyMMdd") + "'" + "\n");
            strSqlString.Append("            AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("            AND CM_KEY_1 = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("            AND FACTORY NOT IN ('RETURN') " + "\n");
            strSqlString.Append("            AND MAT_ID LIKE 'HX%'" + "\n");
            strSqlString.Append("          GROUP BY WORK_DATE, MAT_ID" + "\n");            
            strSqlString.Append("        ) " + "\n");
            strSqlString.Append("  GROUP BY MAT_ID" + "\n");
            strSqlString.Append(")" + "\n");

            #region 1.Lipas(item)
            if (rdbLipas1.Checked == true)
            {
                strSqlString.AppendFormat("SELECT {0}, ' ' " + "\n", QueryCond3);
                strSqlString.Append("     , SUM(D0_PLN) AS D0_PLN" + "\n");
                strSqlString.Append("     , SUM(D1_PLN) AS D1_PLN" + "\n");
                strSqlString.Append("     , SUM(D2_PLN) AS D2_PLN" + "\n");
                strSqlString.Append("     , SUM(D3_PLN) AS D3_PLN" + "\n");
                strSqlString.Append("     , SUM(D4_PLN) AS D4_PLN" + "\n");
                strSqlString.Append("     , SUM(D5_PLN) AS D5_PLN" + "\n");
                strSqlString.Append("     , SUM(D6_PLN) AS D6_PLN" + "\n");
                strSqlString.Append("     , SUM(" + sWeekTotal_1 + ") AS WW_PLN_1" + "\n");
                strSqlString.Append("     , SUM(WW_PLN_2) AS WW_PLN_2" + "\n");
                strSqlString.Append("     , SUM(D0_SHP) AS D0_SHP" + "\n");
                strSqlString.Append("     , SUM(D1_SHP) AS D1_SHP" + "\n");
                strSqlString.Append("     , SUM(D2_SHP) AS D2_SHP" + "\n");
                strSqlString.Append("     , SUM(D3_SHP) AS D3_SHP" + "\n");
                strSqlString.Append("     , SUM(D4_SHP) AS D4_SHP" + "\n");
                strSqlString.Append("     , SUM(D5_SHP) AS D5_SHP" + "\n");
                strSqlString.Append("     , SUM(D6_SHP) AS D6_SHP" + "\n");
                strSqlString.Append("     , SUM(D0_SHP+D1_SHP+D2_SHP+D3_SHP+D4_SHP+D5_SHP+D6_SHP) AS WW_SHP_1" + "\n");
                strSqlString.Append("     , SUM(WW_SHP_2) AS WW_SHP_2" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D0_PLN) <= 0 AND SUM(D0_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D0_PLN) <= 0 THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D0_SHP) / SUM(D0_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D0_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D1_PLN) <= 0 AND SUM(D1_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D1_PLN) <= 0 THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D1_SHP) / SUM(D1_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D1_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D2_PLN) <= 0 AND SUM(D2_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D2_PLN) <= 0 THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D2_SHP) / SUM(D2_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D2_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D3_PLN) <= 0 AND SUM(D3_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D3_PLN) <= 0 THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D3_SHP) / SUM(D3_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D3_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D4_PLN) <= 0 AND SUM(D4_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D4_PLN) <= 0 THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D4_SHP) / SUM(D4_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D4_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D5_PLN) <= 0 AND SUM(D5_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D5_PLN) <= 0 THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D5_SHP) / SUM(D5_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D5_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D6_PLN) <= 0 AND SUM(D6_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D6_PLN) <= 0 THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D6_SHP) / SUM(D6_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D6_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(" + sWeekTotal_1 + ") <= 0 AND SUM(D0_SHP+D1_SHP+D2_SHP+D3_SHP+D4_SHP+D5_SHP+D6_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(" + sWeekTotal_1 + ") <= 0 THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D0_SHP+D1_SHP+D2_SHP+D3_SHP+D4_SHP+D5_SHP+D6_SHP) / SUM(" + sWeekTotal_1 + ") * 100" + "\n");
                strSqlString.Append("             END, 0) AS WW_PER_1" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(WW_PLN_2) <= 0 AND SUM(WW_SHP_2) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(WW_PLN_2) <= 0 THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(WW_SHP_2) / SUM(WW_PLN_2) * 100" + "\n");
                strSqlString.Append("             END, 0) AS WW_PER_2" + "\n");
                strSqlString.Append("     , SUM(D0_SHP) - SUM(D0_PLN) AS SUM_D0_DEF" + "\n");
                strSqlString.Append("     , SUM(D0_SHP + D1_SHP) - SUM(D0_PLN + D1_PLN) AS SUM_D1_DEF" + "\n");
                strSqlString.Append("     , SUM(D0_SHP + D1_SHP + D2_SHP) - SUM(D0_PLN + D1_PLN + D2_PLN) AS SUM_D2_DEF" + "\n");
                strSqlString.Append("     , SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP) - SUM(D0_PLN + D1_PLN + D2_PLN + D3_PLN) AS SUM_D3_DEF" + "\n");
                strSqlString.Append("     , SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP + D4_SHP) - SUM(D0_PLN + D1_PLN + D2_PLN + D3_PLN + D4_PLN) AS SUM_D4_DEF" + "\n");
                strSqlString.Append("     , SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP + D4_SHP + D5_SHP) - SUM(D0_PLN + D1_PLN + D2_PLN + D3_PLN + D4_PLN + D5_PLN) AS SUM_D5_DEF" + "\n");
                strSqlString.Append("     , SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP + D4_SHP + D5_SHP + D6_SHP) - SUM(D0_PLN + D1_PLN + D2_PLN + D3_PLN + D4_PLN + D5_PLN + D6_PLN) AS SUM_D6_DEF" + "\n");
                strSqlString.Append("     , SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP + D4_SHP + D5_SHP + D6_SHP) - SUM(" + sWeekTotal_1 + ") AS WW_DEF_1" + "\n");
                strSqlString.Append("     , SUM(WW_SHP_2) - SUM(WW_PLN_2) AS WW_DEF_2" + "\n");
                strSqlString.Append("     , 0 AS D0_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , 0 AS D1_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , 0 AS D2_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , 0 AS D3_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , 0 AS D4_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , 0 AS D5_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , 0 AS D6_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , 0 AS WW_EXCEED_PLAN_1" + "\n");
                strSqlString.Append("     , 0 AS WW_EXCEED_PLAN_2" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT CUST_TYPE, MAT_GRP_1, MAT_GRP_2, MAT_GRP_4, MAT_GRP_6, MAT_GRP_9, MAT_GRP_10, MAT_CMF_11, CONV_MAT_ID " + "\n");
                strSqlString.Append("             , DECODE(SUM(D0_PLN), 0, 0, 1) AS D0_PLN" + "\n");
                strSqlString.Append("             , DECODE(SUM(D1_PLN), 0, 0, 1) AS D1_PLN" + "\n");
                strSqlString.Append("             , DECODE(SUM(D2_PLN), 0, 0, 1) AS D2_PLN" + "\n");
                strSqlString.Append("             , DECODE(SUM(D3_PLN), 0, 0, 1) AS D3_PLN" + "\n");
                strSqlString.Append("             , DECODE(SUM(D4_PLN), 0, 0, 1) AS D4_PLN" + "\n");
                strSqlString.Append("             , DECODE(SUM(D5_PLN), 0, 0, 1) AS D5_PLN" + "\n");
                strSqlString.Append("             , DECODE(SUM(D6_PLN), 0, 0, 1) AS D6_PLN" + "\n");
                strSqlString.Append("             , DECODE(SUM(WW_PLN), 0, 0, 1) AS WW_PLN_2" + "\n");                    
                strSqlString.Append("             , CASE WHEN SUM(D0_PLN) = 0 THEN 0" + "\n");
                strSqlString.Append("                    WHEN SUM(D0_SHP) / SUM(D0_PLN) * 100 >= 98.5 THEN 1" + "\n");
                strSqlString.Append("                    ELSE 0" + "\n");
                strSqlString.Append("               END D0_SHP" + "\n");
                strSqlString.Append("             , CASE WHEN SUM(D1_PLN) = 0 THEN 0" + "\n");
                strSqlString.Append("                    WHEN SUM(D1_SHP) / SUM(D1_PLN) * 100 >= 98.5 THEN 1" + "\n");
                strSqlString.Append("                    ELSE 0" + "\n");
                strSqlString.Append("               END D1_SHP" + "\n");
                strSqlString.Append("             , CASE WHEN SUM(D2_PLN) = 0 THEN 0" + "\n");
                strSqlString.Append("                    WHEN SUM(D2_SHP) / SUM(D2_PLN) * 100 >= 98.5 THEN 1" + "\n");
                strSqlString.Append("                    ELSE 0" + "\n");
                strSqlString.Append("               END D2_SHP" + "\n");
                strSqlString.Append("             , CASE WHEN SUM(D3_PLN) = 0 THEN 0" + "\n");
                strSqlString.Append("                    WHEN SUM(D3_SHP) / SUM(D3_PLN) * 100 >= 98.5 THEN 1" + "\n");
                strSqlString.Append("                    ELSE 0" + "\n");
                strSqlString.Append("               END D3_SHP" + "\n");
                strSqlString.Append("             , CASE WHEN SUM(D4_PLN) = 0 THEN 0" + "\n");
                strSqlString.Append("                    WHEN SUM(D4_SHP) / SUM(D4_PLN) * 100 >= 98.5 THEN 1" + "\n");
                strSqlString.Append("                    ELSE 0" + "\n");
                strSqlString.Append("               END D4_SHP" + "\n");
                strSqlString.Append("             , CASE WHEN SUM(D5_PLN) = 0 THEN 0" + "\n");
                strSqlString.Append("                    WHEN SUM(D5_SHP) / SUM(D5_PLN) * 100 >= 98.5 THEN 1" + "\n");
                strSqlString.Append("                    ELSE 0" + "\n");
                strSqlString.Append("               END D5_SHP" + "\n");
                strSqlString.Append("             , CASE WHEN SUM(D6_PLN) = 0 THEN 0" + "\n");
                strSqlString.Append("                    WHEN SUM(D6_SHP) / SUM(D6_PLN) * 100 >= 98.5 THEN 1" + "\n");
                strSqlString.Append("                    ELSE 0" + "\n");
                strSqlString.Append("               END D6_SHP" + "\n");
                strSqlString.Append("             , CASE WHEN SUM(WW_PLN) = 0 THEN 0" + "\n");
                strSqlString.Append("                    WHEN SUM(WW_SHP) / SUM(WW_PLN) * 100 >= 98.5 THEN 1" + "\n");
                strSqlString.Append("                    ELSE 0" + "\n");
                strSqlString.Append("               END WW_SHP_2" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT CUST_TYPE, MAT_GRP_1, MAT_GRP_2, MAT_GRP_4, MAT_GRP_6, MAT_GRP_9, MAT_GRP_10, MAT_CMF_11, CONV_MAT_ID" + "\n");
                strSqlString.Append("                     , SUM(NVL(D0_PLN,0)) AS D0_PLN" + "\n");                
                strSqlString.Append("                     , CASE WHEN " + DateDT.Rows[0][0].ToString() + " > " + cdvDate.Value.ToString("yyyyMMdd") + " THEN SUM(NVL(D1_PLN,0))" + "\n");
                strSqlString.Append("                            ELSE CASE WHEN SUM(NVL(SUM_D1_PLN,0) - NVL(SUM_D0_SHP,0)) <= 0 THEN 0" + "\n");
                strSqlString.Append("                                 ELSE SUM(NVL(SUM_D1_PLN,0) - NVL(SUM_D0_SHP,0)) " + "\n");
                strSqlString.Append("                             END" + "\n");
                strSqlString.Append("                        END AS D1_PLN" + "\n");                
                strSqlString.Append("                     , CASE WHEN " + DateDT.Rows[1][0].ToString() + " > " + cdvDate.Value.ToString("yyyyMMdd") + " THEN SUM(NVL(D2_PLN,0))" + "\n");
                strSqlString.Append("                            ELSE CASE WHEN SUM(NVL(SUM_D2_PLN,0) - NVL(SUM_D1_SHP,0)) <= 0 THEN 0" + "\n");
                strSqlString.Append("                                 ELSE SUM(NVL(SUM_D2_PLN,0) - NVL(SUM_D1_SHP,0)) " + "\n");
                strSqlString.Append("                             END" + "\n");
                strSqlString.Append("                        END AS D2_PLN" + "\n");                
                strSqlString.Append("                     , CASE WHEN " + DateDT.Rows[2][0].ToString() + " > " + cdvDate.Value.ToString("yyyyMMdd") + " THEN SUM(NVL(D3_PLN,0))" + "\n");
                strSqlString.Append("                            ELSE CASE WHEN SUM(NVL(SUM_D3_PLN,0) - NVL(SUM_D2_SHP,0)) <= 0 THEN 0" + "\n");
                strSqlString.Append("                                 ELSE SUM(NVL(SUM_D3_PLN,0) - NVL(SUM_D2_SHP,0)) " + "\n");
                strSqlString.Append("                             END" + "\n");
                strSqlString.Append("                        END AS D3_PLN" + "\n");                
                strSqlString.Append("                     , CASE WHEN " + DateDT.Rows[3][0].ToString() + " > " + cdvDate.Value.ToString("yyyyMMdd") + " THEN SUM(NVL(D4_PLN,0))" + "\n");
                strSqlString.Append("                            ELSE CASE WHEN SUM(NVL(SUM_D4_PLN,0) - NVL(SUM_D3_SHP,0)) <= 0 THEN 0" + "\n");
                strSqlString.Append("                                 ELSE SUM(NVL(SUM_D4_PLN,0) - NVL(SUM_D3_SHP,0)) " + "\n");
                strSqlString.Append("                             END" + "\n");
                strSqlString.Append("                        END AS D4_PLN" + "\n");                
                strSqlString.Append("                     , CASE WHEN " + DateDT.Rows[4][0].ToString() + " > " + cdvDate.Value.ToString("yyyyMMdd") + " THEN SUM(NVL(D5_PLN,0))" + "\n");
                strSqlString.Append("                            ELSE CASE WHEN SUM(NVL(SUM_D5_PLN,0) - NVL(SUM_D4_SHP,0)) <= 0 THEN 0" + "\n");
                strSqlString.Append("                                 ELSE SUM(NVL(SUM_D5_PLN,0) - NVL(SUM_D4_SHP,0)) " + "\n");
                strSqlString.Append("                             END" + "\n");
                strSqlString.Append("                        END AS D5_PLN" + "\n");                
                strSqlString.Append("                     , CASE WHEN " + DateDT.Rows[5][0].ToString() + " > " + cdvDate.Value.ToString("yyyyMMdd") + " THEN SUM(NVL(D6_PLN,0))" + "\n");
                strSqlString.Append("                            ELSE CASE WHEN SUM(NVL(SUM_D6_PLN,0) - NVL(SUM_D5_SHP,0)) <= 0 THEN 0" + "\n");
                strSqlString.Append("                                 ELSE SUM(NVL(SUM_D6_PLN,0) - NVL(SUM_D5_SHP,0)) " + "\n");
                strSqlString.Append("                             END" + "\n");
                strSqlString.Append("                        END AS D6_PLN" + "\n");                
                strSqlString.Append("                     , SUM(NVL(WW_PLN,0)) AS WW_PLN, SUM(NVL(D0_SHP,0)) AS D0_SHP, SUM(NVL(D1_SHP,0)) AS D1_SHP, SUM(NVL(D2_SHP,0)) AS D2_SHP" + "\n");
                strSqlString.Append("                     , SUM(NVL(D3_SHP,0)) AS D3_SHP, SUM(NVL(D4_SHP,0)) AS D4_SHP, SUM(NVL(D5_SHP,0)) AS D5_SHP, SUM(NVL(D6_SHP,0)) AS D6_SHP" + "\n");
                strSqlString.Append("                     , SUM(NVL(SUM_D6_SHP,0)) AS WW_SHP" + "\n");
                strSqlString.Append("                  FROM (" + "\n");
                strSqlString.Append("                        SELECT A.*" + "\n");
                strSqlString.Append("                             , CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.MAT_GRP_1 = 'FC' AND INSTR(A.MAT_ID, '-') = 0 THEN A.MAT_ID" + "\n");
                strSqlString.Append("                                    WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.MAT_GRP_1 = 'FC' THEN SUBSTR(A.MAT_ID, 1, INSTR(A.MAT_ID, '-')-1) || A.MAT_CMF_11" + "\n");
                strSqlString.Append("                                    WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.MAT_GRP_1 = 'FS' THEN SUBSTR(A.MAT_ID, 1, 5) || SUBSTR(A.MAT_ID, -3)" + "\n");
                strSqlString.Append("                                    WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' THEN A.MAT_CMF_7" + "\n");
                strSqlString.Append("                                    WHEN A.MAT_GRP_1 = 'SE' AND A.MAT_GRP_9 = 'MEMORY' THEN 'SEK_________-___' || SUBSTR(A.MAT_ID, -3)" + "\n");
                strSqlString.Append("                                    WHEN A.MAT_GRP_1 = 'HX' THEN A.MAT_CMF_10" + "\n");
                strSqlString.Append("                                    WHEN A.MAT_GRP_1 = 'CC' THEN A.MAT_CMF_8" + "\n");
                strSqlString.Append("                                    ELSE A.MAT_ID" + "\n");
                strSqlString.Append("                               END CONV_MAT_ID" + "\n");
                strSqlString.Append("                             , NVL((SELECT DATA_10 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND  KEY_1 = MAT_GRP_1), '-') AS CUST_TYPE " + "\n");
                strSqlString.Append("                             , B.*, C.*" + "\n");
                strSqlString.Append("                          FROM MWIPMATDEF A" + "\n");
                strSqlString.Append("                             , DT B" + "\n");
                strSqlString.Append("                             , (" + "\n");
                strSqlString.Append("                                SELECT MAT_ID, D0_QTY AS D0_PLN, D1_QTY AS D1_PLN, D2_QTY AS D2_PLN" + "\n");
                strSqlString.Append("                                     , D3_QTY AS D3_PLN, D4_QTY AS D4_PLN, D5_QTY AS D5_PLN, D6_QTY AS D6_PLN" + "\n");
                strSqlString.Append("                                     , " + sWeekTotal_2 + " AS WW_PLN" + "\n");
                strSqlString.Append("                                     , D0_QTY + D1_QTY AS SUM_D1_PLN" + "\n");
                strSqlString.Append("                                     , D0_QTY + D1_QTY + D2_QTY AS SUM_D2_PLN" + "\n");
                strSqlString.Append("                                     , D0_QTY + D1_QTY + D2_QTY + D3_QTY AS SUM_D3_PLN" + "\n");
                strSqlString.Append("                                     , D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY AS SUM_D4_PLN" + "\n");
                strSqlString.Append("                                     , D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY AS SUM_D5_PLN" + "\n");
                strSqlString.Append("                                     , D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY + D6_QTY AS SUM_D6_PLN" + "\n");
                strSqlString.Append("                                  FROM (" + "\n");
                //1191214 이희석 : 월요일 날짜 기준 조회
                if (comboBox1.SelectedItem.ToString() == "월요일")
                {
                    strSqlString.Append("                                        SELECT MAT_ID, D0_QTY, D1_QTY, D2_QTY, D3_QTY, D4_QTY, D5_QTY, D6_QTY" + "\n");
                    strSqlString.Append("                                          FROM RWIPPLNWEK" + "\n");
                    strSqlString.Append("                                         WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");
                    strSqlString.Append("                                           AND PLAN_WEEK = '" + FindWeek.ThisWeek + "'" + "\n");
                    strSqlString.Append("                                           AND GUBUN = '3'" + "\n");
                    strSqlString.Append("                                           AND MAT_ID NOT LIKE 'SES%'" + "\n");
                    strSqlString.Append("                                         UNION ALL" + "\n");
                }
                else
                {
                    strSqlString.Append("                                        SELECT MAT_ID,0 D0_QTY,0 D1_QTY,0 D2_QTY,0 D3_QTY,0 D4_QTY, D5_QTY, D6_QTY" + "\n");
                    strSqlString.Append("                                          FROM RWIPPLNWEK" + "\n");
                    strSqlString.Append("                                         WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");
                    strSqlString.Append("                                           AND PLAN_WEEK = '" + FindWeek.LastWeek  + "'" + "\n");
                    strSqlString.Append("                                           AND GUBUN = '3'" + "\n");
                    strSqlString.Append("                                           AND MAT_ID NOT LIKE 'SES%'" + "\n");
                    strSqlString.Append("                                         UNION ALL" + "\n");
                    strSqlString.Append("                                        SELECT MAT_ID, D0_QTY, D1_QTY, D2_QTY, D3_QTY, D4_QTY,0 D5_QTY,0 D6_QTY" + "\n");
                    strSqlString.Append("                                          FROM RWIPPLNWEK" + "\n");
                    strSqlString.Append("                                         WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");
                    strSqlString.Append("                                           AND PLAN_WEEK = '" + FindWeek.ThisWeek + "'" + "\n");
                    strSqlString.Append("                                           AND GUBUN = '3'" + "\n");
                    strSqlString.Append("                                           AND MAT_ID NOT LIKE 'SES%'" + "\n");
                    strSqlString.Append("                                         UNION ALL" + "\n");
                }
                
                // TEST 조회시 SE 제품은 OMS 계획 사용, 그 외는 SAP 계획 사용
                if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
                {
                    strSqlString.Append("                                        SELECT MAT_ID" + "\n");
                    strSqlString.Append("                                             , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[0][0].ToString() + "', PLAN_QTY, 0)) AS D0_QTY" + "\n");
                    strSqlString.Append("                                             , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[1][0].ToString() + "', PLAN_QTY, 0)) AS D1_QTY" + "\n");
                    strSqlString.Append("                                             , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[2][0].ToString() + "', PLAN_QTY, 0)) AS D2_QTY" + "\n");
                    strSqlString.Append("                                             , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[3][0].ToString() + "', PLAN_QTY, 0)) AS D3_QTY" + "\n");
                    strSqlString.Append("                                             , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[4][0].ToString() + "', PLAN_QTY, 0)) AS D4_QTY" + "\n");
                    strSqlString.Append("                                             , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[5][0].ToString() + "', PLAN_QTY, 0)) AS D5_QTY" + "\n");
                    strSqlString.Append("                                             , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[6][0].ToString() + "', PLAN_QTY, 0)) AS D6_QTY" + "\n");
                    strSqlString.Append("                                          FROM (" + "\n");
                    strSqlString.Append("                                                SELECT PLAN_DAY, MAT_ID, PLAN_QTY" + "\n");
                    strSqlString.Append("                                                  FROM CWIPPLNDAY" + "\n");
                    strSqlString.Append("                                                 WHERE 1=1" + "\n");
                    strSqlString.Append("                                                   AND PLAN_DAY BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + FindWeek.EndDay_ThisWeek + "'" + "\n");
                    strSqlString.Append("                                                   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                                                 UNION" + "\n");
                    strSqlString.Append("                                                SELECT PLAN_DAY, MAT_ID, PLAN_QTY" + "\n");
                    strSqlString.Append("                                                  FROM CWIPPLNSNP@RPTTOMES " + "\n");
                    strSqlString.Append("                                                 WHERE 1=1" + "\n");
                    strSqlString.Append("                                                   AND SNAPSHOT_DAY BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + FindWeek.EndDay_ThisWeek + "'" + "\n");
                    strSqlString.Append("                                                   AND PLAN_DAY = SNAPSHOT_DAY" + "\n");
                    strSqlString.Append("                                                   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                                               )" + "\n");
                    strSqlString.Append("                                         GROUP BY MAT_ID " + "\n");
                }
                else
                {
                    strSqlString.Append("                                        SELECT MAT_ID" + "\n");
                    //1191214 이희석 : 월요일 날짜 기준 조회
                    if (comboBox1.SelectedItem.ToString() == "월요일")
                    {
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D0_QTY, 0)) AS D0_QTY" + "\n");
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D1_QTY, 0)) AS D1_QTY" + "\n");
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D2_QTY, 0)) AS D2_QTY" + "\n");
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D3_QTY, 0)) AS D3_QTY" + "\n");
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D4_QTY, 0)) AS D4_QTY" + "\n");
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D5_QTY, 0)) AS D5_QTY" + "\n");
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D6_QTY, 0)) AS D6_QTY" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.LastWeek + "', D5_QTY, 0)) AS D0_QTY" + "\n");
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.LastWeek + "', D6_QTY, 0)) AS D1_QTY" + "\n");
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D0_QTY, 0)) AS D2_QTY" + "\n");
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D1_QTY, 0)) AS D3_QTY" + "\n");
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D2_QTY, 0)) AS D4_QTY" + "\n");
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D3_QTY, 0)) AS D5_QTY" + "\n");
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D4_QTY, 0)) AS D6_QTY" + "\n");
                    }
                    
                    strSqlString.Append("                                          FROM RWIPPLNWEK" + "\n");
                    strSqlString.Append("                                         WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");
                    strSqlString.Append("                                           AND PLAN_WEEK IN ('" + FindWeek.LastWeek + "', '" + FindWeek.ThisWeek + "')" + "\n");
                    strSqlString.Append("                                           AND GUBUN = '3'" + "\n");
                    strSqlString.Append("                                           AND MAT_ID LIKE 'SES%'" + "\n");
                    strSqlString.Append("                                         GROUP BY MAT_ID " + "\n");
                }

                strSqlString.Append("                                       )" + "\n");
                strSqlString.Append("                               ) C" + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID(+)" + "\n");
                strSqlString.Append("                           AND A.MAT_ID = C.MAT_ID(+)" + "\n");
                strSqlString.Append("                           AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                           AND A.MAT_TYPE = 'FG'" + "\n");
                strSqlString.Append("                           AND A.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                           AND A.MAT_GRP_3 <> 'COB'" + "\n");
                strSqlString.Append("                           AND A.MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");

                //상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("                           AND A.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("                           AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("                           AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("                           AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("                           AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("                           AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("                           AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("                           AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("                           AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("                 GROUP BY CUST_TYPE, MAT_GRP_1, MAT_GRP_2, MAT_GRP_4, MAT_GRP_6, MAT_GRP_9, MAT_GRP_10, MAT_CMF_11, CONV_MAT_ID" + "\n");
                strSqlString.Append("               )" + "\n");
                strSqlString.Append("         GROUP BY CUST_TYPE, MAT_GRP_1, MAT_GRP_2, MAT_GRP_4, MAT_GRP_6, MAT_GRP_9, MAT_GRP_10, MAT_CMF_11, CONV_MAT_ID" + "\n");
                strSqlString.Append("       )" + "\n");
                strSqlString.Append(" GROUP BY " + QueryCond2 + "\n");
                strSqlString.Append("HAVING SUM(NVL(WW_PLN_2,0) + NVL(WW_SHP_2,0)) > 0 " + "\n");
                strSqlString.Append(" ORDER BY " + QueryCond1 + "\n");
            }
            #endregion

            #region 2.Lipas(Volume)
            else if (rdbLipas2.Checked == true)
            {
                strSqlString.AppendFormat("SELECT {0}, ' ' " + "\n", QueryCond3);
                strSqlString.Append("     , ROUND(SUM(D0_PLN) / " + sKpcsValue + ", 0) AS D0_PLN" + "\n");
                strSqlString.Append("     , ROUND(SUM(D1_PLN) / " + sKpcsValue + ", 0) AS D1_PLN" + "\n");
                strSqlString.Append("     , ROUND(SUM(D2_PLN) / " + sKpcsValue + ", 0) AS D2_PLN" + "\n");
                strSqlString.Append("     , ROUND(SUM(D3_PLN) / " + sKpcsValue + ", 0) AS D3_PLN" + "\n");
                strSqlString.Append("     , ROUND(SUM(D4_PLN) / " + sKpcsValue + ", 0) AS D4_PLN" + "\n");
                strSqlString.Append("     , ROUND(SUM(D5_PLN) / " + sKpcsValue + ", 0) AS D5_PLN" + "\n");
                strSqlString.Append("     , ROUND(SUM(D6_PLN) / " + sKpcsValue + ", 0) AS D6_PLN" + "\n");
                strSqlString.Append("     , ROUND(SUM(" + sWeekTotal_1 + ") / " + sKpcsValue + ", 0) AS WW_PLN_1" + "\n");
                strSqlString.Append("     , ROUND(SUM(WW_PLN_2) / " + sKpcsValue + ", 0) AS WW_PLN_2" + "\n");
                strSqlString.Append("     , ROUND(SUM(D0_SHP) / " + sKpcsValue + ", 0) AS D0_SHP" + "\n");
                strSqlString.Append("     , ROUND(SUM(D1_SHP) / " + sKpcsValue + ", 0) AS D1_SHP" + "\n");
                strSqlString.Append("     , ROUND(SUM(D2_SHP) / " + sKpcsValue + ", 0) AS D2_SHP" + "\n");
                strSqlString.Append("     , ROUND(SUM(D3_SHP) / " + sKpcsValue + ", 0) AS D3_SHP" + "\n");
                strSqlString.Append("     , ROUND(SUM(D4_SHP) / " + sKpcsValue + ", 0) AS D4_SHP" + "\n");
                strSqlString.Append("     , ROUND(SUM(D5_SHP) / " + sKpcsValue + ", 0) AS D5_SHP" + "\n");
                strSqlString.Append("     , ROUND(SUM(D6_SHP) / " + sKpcsValue + ", 0) AS D6_SHP" + "\n");
                strSqlString.Append("     , ROUND(SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP + D4_SHP + D5_SHP + D6_SHP) / " + sKpcsValue + ", 0) AS WW_SHP_1" + "\n");
                strSqlString.Append("     , ROUND(SUM(WW_SHP_2) / " + sKpcsValue + ", 0) AS WW_SHP_2" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D0_PLN) <= 0 AND SUM(D0_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D0_PLN) <= 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D0_SHP) > SUM(D0_PLN) THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D0_SHP) / SUM(D0_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D0_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D1_PLN) <= 0 AND SUM(D1_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D1_PLN) <= 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D1_SHP) > SUM(D1_PLN) THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D1_SHP) / SUM(D1_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D1_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D2_PLN) <= 0 AND SUM(D2_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D2_PLN) <= 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D2_SHP) > SUM(D2_PLN) THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D2_SHP) / SUM(D2_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D2_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D3_PLN) <= 0 AND SUM(D3_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D3_PLN) <= 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D3_SHP) > SUM(D3_PLN) THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D3_SHP) / SUM(D3_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D3_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D4_PLN) <= 0 AND SUM(D4_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D4_PLN) <= 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D4_SHP) > SUM(D4_PLN) THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D4_SHP) / SUM(D4_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D4_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D5_PLN) <= 0 AND SUM(D5_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D5_PLN) <= 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D5_SHP) > SUM(D5_PLN) THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D5_SHP) / SUM(D5_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D5_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D6_PLN) <= 0 AND SUM(D6_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D6_PLN) <= 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D6_SHP) > SUM(D6_PLN) THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D6_SHP) / SUM(D6_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D6_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(" + sWeekTotal_1 + ") <= 0 AND SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP + D4_SHP + D5_SHP + D6_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(" + sWeekTotal_1 + ") <= 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP + D4_SHP + D5_SHP + D6_SHP) > SUM(" + sWeekTotal_1 + ") THEN 100 " + "\n");
                strSqlString.Append("                  ELSE SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP + D4_SHP + D5_SHP + D6_SHP) / SUM(" + sWeekTotal_1 + ") * 100" + "\n");
                strSqlString.Append("             END, 0) AS WW_PER_1" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(WW_PLN_2) <= 0 AND SUM(WW_SHP_2) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(WW_PLN_2) <= 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(WW_SHP_2) > SUM(WW_PLN_2) THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(WW_SHP_2) / SUM(WW_PLN_2) * 100" + "\n");
                strSqlString.Append("             END, 0) AS WW_PER_2" + "\n");
                strSqlString.Append("     , ROUND((SUM(D0_SHP) - SUM(D0_PLN)) / " + sKpcsValue + ", 0) AS SUM_D0_DEF" + "\n");
                strSqlString.Append("     , ROUND((SUM(D0_SHP + D1_SHP) - SUM(D0_PLN + D1_PLN)) / " + sKpcsValue + ", 0) AS SUM_D1_DEF" + "\n");
                strSqlString.Append("     , ROUND((SUM(D0_SHP + D1_SHP + D2_SHP) - SUM(D0_PLN + D1_PLN + D2_PLN)) / " + sKpcsValue + ", 0) AS SUM_D2_DEF" + "\n");
                strSqlString.Append("     , ROUND((SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP) - SUM(D0_PLN + D1_PLN + D2_PLN + D3_PLN)) / " + sKpcsValue + ", 0) AS SUM_D3_DEF" + "\n");
                strSqlString.Append("     , ROUND((SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP + D4_SHP) - SUM(D0_PLN + D1_PLN + D2_PLN + D3_PLN + D4_PLN)) / " + sKpcsValue + ", 0) AS SUM_D4_DEF" + "\n");
                strSqlString.Append("     , ROUND((SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP + D4_SHP + D5_SHP) - SUM(D0_PLN + D1_PLN + D2_PLN + D3_PLN + D4_PLN + D5_PLN)) / " + sKpcsValue + ", 0) AS SUM_D5_DEF" + "\n");
                strSqlString.Append("     , ROUND((SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP + D4_SHP + D5_SHP + D6_SHP) - SUM(D0_PLN + D1_PLN + D2_PLN + D3_PLN + D4_PLN + D5_PLN + D6_PLN)) / " + sKpcsValue + ", 0) AS SUM_D6_DEF" + "\n");
                strSqlString.Append("     , ROUND((SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP + D4_SHP + D5_SHP + D6_SHP) - SUM(" + sWeekTotal_1 + ")) / " + sKpcsValue + ", 0) AS WW_DEF_1" + "\n");
                strSqlString.Append("     , ROUND((SUM(WW_SHP_2) - SUM(WW_PLN_2)) / " + sKpcsValue + ", 0) AS WW_DEF_2" + "\n");
                strSqlString.Append("     , CASE WHEN (SUM(D0_SHP) / " + sKpcsValue + ") > (SUM(D0_PLN) / " + sKpcsValue + ") THEN ROUND((SUM(D0_SHP) / " + sKpcsValue + ") - (SUM(D0_PLN) / " + sKpcsValue + "), 0)" + "\n");
                strSqlString.Append("            ELSE 0 END AS D0_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , CASE WHEN (SUM(D1_SHP) / " + sKpcsValue + ") > (SUM(D1_PLN) / " + sKpcsValue + ") THEN ROUND((SUM(D1_SHP) / " + sKpcsValue + ") - (SUM(D1_PLN) / " + sKpcsValue + "), 0)" + "\n");
                strSqlString.Append("            ELSE 0 END AS D1_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , CASE WHEN (SUM(D2_SHP) / " + sKpcsValue + ") > (SUM(D2_PLN) / " + sKpcsValue + ") THEN ROUND((SUM(D2_SHP) / " + sKpcsValue + ") - (SUM(D2_PLN) / " + sKpcsValue + "), 0)" + "\n");
                strSqlString.Append("            ELSE 0 END AS D2_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , CASE WHEN (SUM(D3_SHP) / " + sKpcsValue + ") > (SUM(D3_PLN) / " + sKpcsValue + ") THEN ROUND((SUM(D3_SHP) / " + sKpcsValue + ") - (SUM(D3_PLN) / " + sKpcsValue + "), 0)" + "\n");
                strSqlString.Append("            ELSE 0 END AS D3_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , CASE WHEN (SUM(D4_SHP) / " + sKpcsValue + ") > (SUM(D4_PLN) / " + sKpcsValue + ") THEN ROUND((SUM(D4_SHP) / " + sKpcsValue + ") - (SUM(D4_PLN) / " + sKpcsValue + "), 0)" + "\n");
                strSqlString.Append("            ELSE 0 END AS D4_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , CASE WHEN (SUM(D5_SHP) / " + sKpcsValue + ") > (SUM(D5_PLN) / " + sKpcsValue + ") THEN ROUND((SUM(D5_SHP) / " + sKpcsValue + ") - (SUM(D5_PLN) / " + sKpcsValue + "), 0)" + "\n");
                strSqlString.Append("            ELSE 0 END AS D5_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , CASE WHEN (SUM(D6_SHP) / " + sKpcsValue + ") > (SUM(D6_PLN) / " + sKpcsValue + ") THEN ROUND((SUM(D6_SHP) / " + sKpcsValue + ") - (SUM(D6_PLN) / " + sKpcsValue + "), 0)" + "\n");
                strSqlString.Append("            ELSE 0 END AS D6_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , CASE WHEN (SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP + D4_SHP + D5_SHP + D6_SHP) / " + sKpcsValue + ") > (SUM(" + sWeekTotal_1 + ") / " + sKpcsValue + ") THEN ROUND((SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP + D4_SHP + D5_SHP + D6_SHP) / " + sKpcsValue + ") - (SUM(" + sWeekTotal_1 + ") / " + sKpcsValue + "), 0)" + "\n");
                strSqlString.Append("            ELSE 0 END AS WW_EXCEED_PLAN_1" + "\n");
                strSqlString.Append("     , CASE WHEN (SUM(WW_SHP_2) / " + sKpcsValue + ") > (SUM(WW_PLN_2) / " + sKpcsValue + ") THEN ROUND((SUM(WW_SHP_2) / " + sKpcsValue + ") - (SUM(WW_PLN_2) / " + sKpcsValue + "), 0)" + "\n");
                strSqlString.Append("            ELSE 0 END AS WW_EXCEED_PLAN_2" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT CUST_TYPE, MAT_GRP_1, MAT_GRP_2, MAT_GRP_4, MAT_GRP_6, MAT_GRP_9, MAT_GRP_10, MAT_CMF_11, CONV_MAT_ID" + "\n");                
                strSqlString.Append("             , SUM(NVL(D0_PLN,0)) AS D0_PLN" + "\n");
                                
                //if (cboVersion.Text == "고정 Plan")
                if (cboVersion.SelectedIndex == 2)
                {
                    #region 고정 Plan
                    strSqlString.Append("             , SUM(NVL(D1_PLN,0)) AS D1_PLN" + "\n");
                    strSqlString.Append("             , SUM(NVL(D2_PLN,0)) AS D2_PLN" + "\n");
                    strSqlString.Append("             , SUM(NVL(D3_PLN,0)) AS D3_PLN" + "\n");
                    strSqlString.Append("             , SUM(NVL(D4_PLN,0)) AS D4_PLN" + "\n");
                    strSqlString.Append("             , SUM(NVL(D5_PLN,0)) AS D5_PLN" + "\n");
                    strSqlString.Append("             , SUM(NVL(D6_PLN,0)) AS D6_PLN" + "\n");
                    strSqlString.Append("             , SUM(NVL(WW_PLN,0)) AS WW_PLN_2" + "\n");
                    strSqlString.Append("             , SUM(NVL(D0_SHP,0)) AS D0_SHP" + "\n");
                    strSqlString.Append("             , SUM(NVL(D1_SHP,0)) AS D1_SHP" + "\n");
                    strSqlString.Append("             , SUM(NVL(D2_SHP,0)) AS D2_SHP" + "\n");
                    strSqlString.Append("             , SUM(NVL(D3_SHP,0)) AS D3_SHP" + "\n");
                    strSqlString.Append("             , SUM(NVL(D4_SHP,0)) AS D4_SHP" + "\n");
                    strSqlString.Append("             , SUM(NVL(D5_SHP,0)) AS D5_SHP" + "\n");
                    strSqlString.Append("             , SUM(NVL(D6_SHP,0)) AS D6_SHP" + "\n");                    
                    strSqlString.Append("             , SUM(NVL(SUM_D6_SHP,0)) AS WW_SHP_2" + "\n");
                    #endregion
                }
                else
                {
                    #region 그 외
                    strSqlString.Append("             , CASE WHEN " + DateDT.Rows[0][0].ToString() + " > " + cdvDate.Value.ToString("yyyyMMdd") + " THEN SUM(NVL(D1_PLN,0))" + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN SUM(NVL(SUM_D1_PLN,0) - NVL(SUM_D0_SHP,0)) <= 0 THEN 0" + "\n");
                    strSqlString.Append("                              ELSE SUM(NVL(SUM_D1_PLN,0) - NVL(SUM_D0_SHP,0)) " + "\n");
                    strSqlString.Append("                          END" + "\n");
                    strSqlString.Append("                END AS D1_PLN" + "\n");
                    strSqlString.Append("             , CASE WHEN " + DateDT.Rows[1][0].ToString() + " > " + cdvDate.Value.ToString("yyyyMMdd") + " THEN SUM(NVL(D2_PLN,0))" + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN SUM(NVL(SUM_D2_PLN,0) - NVL(SUM_D1_SHP,0)) <= 0 THEN 0" + "\n");
                    strSqlString.Append("                              ELSE SUM(NVL(SUM_D2_PLN,0) - NVL(SUM_D1_SHP,0)) " + "\n");
                    strSqlString.Append("                          END" + "\n");
                    strSqlString.Append("                END AS D2_PLN" + "\n");
                    strSqlString.Append("             , CASE WHEN " + DateDT.Rows[2][0].ToString() + " > " + cdvDate.Value.ToString("yyyyMMdd") + " THEN SUM(NVL(D3_PLN,0))" + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN SUM(NVL(SUM_D3_PLN,0) - NVL(SUM_D2_SHP,0)) <= 0 THEN 0" + "\n");
                    strSqlString.Append("                              ELSE SUM(NVL(SUM_D3_PLN,0) - NVL(SUM_D2_SHP,0)) " + "\n");
                    strSqlString.Append("                          END" + "\n");
                    strSqlString.Append("                END AS D3_PLN" + "\n");
                    strSqlString.Append("             , CASE WHEN " + DateDT.Rows[3][0].ToString() + " > " + cdvDate.Value.ToString("yyyyMMdd") + " THEN SUM(NVL(D4_PLN,0))" + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN SUM(NVL(SUM_D4_PLN,0) - NVL(SUM_D3_SHP,0)) <= 0 THEN 0" + "\n");
                    strSqlString.Append("                              ELSE SUM(NVL(SUM_D4_PLN,0) - NVL(SUM_D3_SHP,0)) " + "\n");
                    strSqlString.Append("                          END" + "\n");
                    strSqlString.Append("                END AS D4_PLN" + "\n");
                    strSqlString.Append("             , CASE WHEN " + DateDT.Rows[4][0].ToString() + " > " + cdvDate.Value.ToString("yyyyMMdd") + " THEN SUM(NVL(D5_PLN,0))" + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN SUM(NVL(SUM_D5_PLN,0) - NVL(SUM_D4_SHP,0)) <= 0 THEN 0" + "\n");
                    strSqlString.Append("                              ELSE SUM(NVL(SUM_D5_PLN,0) - NVL(SUM_D4_SHP,0)) " + "\n");
                    strSqlString.Append("                          END" + "\n");
                    strSqlString.Append("                END AS D5_PLN" + "\n");
                    strSqlString.Append("             , CASE WHEN " + DateDT.Rows[5][0].ToString() + " > " + cdvDate.Value.ToString("yyyyMMdd") + " THEN SUM(NVL(D6_PLN,0))" + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN SUM(NVL(SUM_D6_PLN,0) - NVL(SUM_D5_SHP,0)) <= 0 THEN 0" + "\n");
                    strSqlString.Append("                              ELSE SUM(NVL(SUM_D6_PLN,0) - NVL(SUM_D5_SHP,0)) " + "\n");
                    strSqlString.Append("                          END" + "\n");
                    strSqlString.Append("                END AS D6_PLN" + "\n");
                    strSqlString.Append("             , SUM(WW_PLN) AS WW_PLN_2" + "\n");
                    strSqlString.Append("             , CASE WHEN SUM(NVL(D0_PLN,0)) <= SUM(NVL(D0_SHP,0)) THEN SUM(NVL(D0_PLN,0)) ELSE SUM(NVL(D0_SHP,0)) END AS D0_SHP" + "\n");
                    strSqlString.Append("             , CASE WHEN SUM(NVL(SUM_D1_PLN,0)) - SUM(NVL(SUM_D0_SHP,0)) <= 0 THEN 0 " + "\n");
                    strSqlString.Append("                    WHEN SUM(NVL(SUM_D1_PLN,0)) - SUM(NVL(SUM_D0_SHP,0)) <= SUM(NVL(D1_SHP,0)) THEN SUM(NVL(SUM_D1_PLN,0)) - SUM(NVL(SUM_D0_SHP,0)) " + "\n");
                    strSqlString.Append("                    ELSE SUM(NVL(D1_SHP,0)) END AS D1_SHP " + "\n");
                    strSqlString.Append("             , CASE WHEN SUM(NVL(SUM_D2_PLN,0)) - SUM(NVL(SUM_D1_SHP,0)) <= 0 THEN 0 " + "\n");
                    strSqlString.Append("                    WHEN SUM(NVL(SUM_D2_PLN,0)) - SUM(NVL(SUM_D1_SHP,0)) <= SUM(NVL(D2_SHP,0)) THEN SUM(NVL(SUM_D2_PLN,0)) - SUM(NVL(SUM_D1_SHP,0)) " + "\n");
                    strSqlString.Append("                    ELSE SUM(NVL(D2_SHP,0)) END AS D2_SHP " + "\n");
                    strSqlString.Append("             , CASE WHEN SUM(NVL(SUM_D3_PLN,0)) - SUM(NVL(SUM_D2_SHP,0)) <= 0 THEN 0 " + "\n");
                    strSqlString.Append("                    WHEN SUM(NVL(SUM_D3_PLN,0)) - SUM(NVL(SUM_D2_SHP,0)) <= SUM(NVL(D3_SHP,0)) THEN SUM(NVL(SUM_D3_PLN,0)) - SUM(NVL(SUM_D2_SHP,0)) " + "\n");
                    strSqlString.Append("                    ELSE SUM(NVL(D3_SHP,0)) END AS D3_SHP " + "\n");
                    strSqlString.Append("             , CASE WHEN SUM(NVL(SUM_D4_PLN,0)) - SUM(NVL(SUM_D3_SHP,0)) <= 0 THEN 0 " + "\n");
                    strSqlString.Append("                    WHEN SUM(NVL(SUM_D4_PLN,0)) - SUM(NVL(SUM_D3_SHP,0)) <= SUM(NVL(D4_SHP,0)) THEN SUM(NVL(SUM_D4_PLN,0)) - SUM(NVL(SUM_D3_SHP,0)) " + "\n");
                    strSqlString.Append("                    ELSE SUM(NVL(D4_SHP,0)) END AS D4_SHP " + "\n");
                    strSqlString.Append("             , CASE WHEN SUM(NVL(SUM_D5_PLN,0)) - SUM(NVL(SUM_D4_SHP,0)) <= 0 THEN 0 " + "\n");
                    strSqlString.Append("                    WHEN SUM(NVL(SUM_D5_PLN,0)) - SUM(NVL(SUM_D4_SHP,0)) <= SUM(NVL(D5_SHP,0)) THEN SUM(NVL(SUM_D5_PLN,0)) - SUM(NVL(SUM_D4_SHP,0)) " + "\n");
                    strSqlString.Append("                    ELSE SUM(NVL(D5_SHP,0)) END AS D5_SHP " + "\n");
                    strSqlString.Append("             , CASE WHEN SUM(NVL(SUM_D6_PLN,0)) - SUM(NVL(SUM_D5_SHP,0)) <= 0 THEN 0 " + "\n");
                    strSqlString.Append("                    WHEN SUM(NVL(SUM_D6_PLN,0)) - SUM(NVL(SUM_D5_SHP,0)) <= SUM(NVL(D6_SHP,0)) THEN SUM(NVL(SUM_D6_PLN,0)) - SUM(NVL(SUM_D5_SHP,0)) " + "\n");
                    strSqlString.Append("                    ELSE SUM(NVL(D6_SHP,0)) END AS D6_SHP " + "\n");
                    strSqlString.Append("             , CASE WHEN SUM(NVL(WW_PLN,0)) <= SUM(NVL(SUM_D6_SHP,0)) THEN SUM(NVL(WW_PLN,0)) ELSE SUM(NVL(SUM_D6_SHP,0)) END AS WW_SHP_2" + "\n");
                    #endregion
                }

                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT A.*" + "\n");
                strSqlString.Append("                     , CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.MAT_GRP_1 = 'FC' AND INSTR(A.MAT_ID, '-') = 0 THEN A.MAT_ID" + "\n");
                strSqlString.Append("                            WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.MAT_GRP_1 = 'FC' THEN SUBSTR(A.MAT_ID, 1, INSTR(A.MAT_ID, '-')-1) || A.MAT_CMF_11" + "\n");                
                strSqlString.Append("                            WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.MAT_GRP_1 = 'FS' THEN SUBSTR(A.MAT_ID, 1, 5) || SUBSTR(A.MAT_ID, -3)" + "\n");
                strSqlString.Append("                            WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' THEN A.MAT_CMF_7" + "\n");
                strSqlString.Append("                            WHEN A.MAT_GRP_1 = 'SE' AND A.MAT_GRP_9 = 'MEMORY' THEN 'SEK_________-___' || SUBSTR(A.MAT_ID, -3)" + "\n");
                strSqlString.Append("                            WHEN A.MAT_GRP_1 = 'HX' THEN A.MAT_CMF_10" + "\n");
                strSqlString.Append("                            WHEN A.MAT_GRP_1 = 'CC' THEN A.MAT_CMF_8" + "\n");
                strSqlString.Append("                            ELSE A.MAT_ID" + "\n");
                strSqlString.Append("                       END CONV_MAT_ID" + "\n");
                strSqlString.Append("                     , NVL((SELECT DATA_10 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND  KEY_1 = MAT_GRP_1), '-') AS CUST_TYPE " + "\n");
                strSqlString.Append("                     , B.*, C.*" + "\n");
                strSqlString.Append("                  FROM MWIPMATDEF A" + "\n");
                strSqlString.Append("                     , DT B" + "\n");
                strSqlString.Append("                     , (" + "\n");
                strSqlString.Append("                        SELECT MAT_ID, D0_QTY AS D0_PLN, D1_QTY AS D1_PLN, D2_QTY AS D2_PLN" + "\n");
                strSqlString.Append("                             , D3_QTY AS D3_PLN, D4_QTY AS D4_PLN, D5_QTY AS D5_PLN, D6_QTY AS D6_PLN" + "\n");
                strSqlString.Append("                             , " + sWeekTotal_2 + " AS WW_PLN" + "\n");
                strSqlString.Append("                             , D0_QTY + D1_QTY AS SUM_D1_PLN" + "\n");
                strSqlString.Append("                             , D0_QTY + D1_QTY + D2_QTY AS SUM_D2_PLN" + "\n");
                strSqlString.Append("                             , D0_QTY + D1_QTY + D2_QTY + D3_QTY AS SUM_D3_PLN" + "\n");
                strSqlString.Append("                             , D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY AS SUM_D4_PLN" + "\n");
                strSqlString.Append("                             , D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY AS SUM_D5_PLN" + "\n");
                strSqlString.Append("                             , D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY + D6_QTY AS SUM_D6_PLN" + "\n");
                strSqlString.Append("                          FROM (" + "\n");
                strSqlString.Append("                                SELECT MAT_ID, D0_QTY, D1_QTY, D2_QTY, D3_QTY, D4_QTY, D5_QTY, D6_QTY" + "\n");
                strSqlString.Append("                                  FROM RWIPPLNWEK" + "\n");
                strSqlString.Append("                                 WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                                   AND PLAN_WEEK = '" + FindWeek.ThisWeek + "'" + "\n");

                //if (cboVersion.Text == "영업 Plan")
                if (cboVersion.SelectedIndex == 0)
                {
                    strSqlString.Append("                                   AND GUBUN = '0'" + "\n");
                }
                else
                {
                    strSqlString.Append("                                   AND GUBUN = '3'" + "\n");
                }

                strSqlString.Append("                                   AND MAT_ID NOT LIKE 'SES%'" + "\n");
                strSqlString.Append("                                 UNION ALL" + "\n");

                // TEST 조회시 SE 제품은 OMS 계획 사용, 그 외는 SAP 계획 사용
                if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
                {
                    strSqlString.Append("                                SELECT MAT_ID" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[0][0].ToString() + "', PLAN_QTY, 0)) AS D0_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[1][0].ToString() + "', PLAN_QTY, 0)) AS D1_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[2][0].ToString() + "', PLAN_QTY, 0)) AS D2_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[3][0].ToString() + "', PLAN_QTY, 0)) AS D3_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[4][0].ToString() + "', PLAN_QTY, 0)) AS D4_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[5][0].ToString() + "', PLAN_QTY, 0)) AS D5_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[6][0].ToString() + "', PLAN_QTY, 0)) AS D6_QTY" + "\n");
                    strSqlString.Append("                                  FROM (" + "\n");
                    strSqlString.Append("                                        SELECT PLAN_DAY, MAT_ID, PLAN_QTY" + "\n");
                    strSqlString.Append("                                          FROM CWIPPLNDAY" + "\n");
                    strSqlString.Append("                                         WHERE 1=1" + "\n");
                    strSqlString.Append("                                           AND PLAN_DAY BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + FindWeek.EndDay_ThisWeek + "'" + "\n");
                    strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                                         UNION" + "\n");
                    strSqlString.Append("                                        SELECT PLAN_DAY, MAT_ID, PLAN_QTY" + "\n");
                    strSqlString.Append("                                          FROM CWIPPLNSNP@RPTTOMES " + "\n");
                    strSqlString.Append("                                         WHERE 1=1" + "\n");
                    strSqlString.Append("                                           AND SNAPSHOT_DAY BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + FindWeek.EndDay_ThisWeek + "'" + "\n");
                    strSqlString.Append("                                           AND PLAN_DAY = SNAPSHOT_DAY" + "\n");
                    strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                                       )" + "\n");
                    strSqlString.Append("                                 GROUP BY MAT_ID " + "\n");
                }
                else
                {
                    strSqlString.Append("                                SELECT MAT_ID" + "\n");
                    //1191214 이희석 : 월요일 기준으로 데이터 가져올경우
                    if (comboBox1.SelectedItem.ToString() == "월요일")
                    {
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D0_QTY, 0)) AS D0_QTY" + "\n");
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D1_QTY, 0)) AS D1_QTY" + "\n");
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D2_QTY, 0)) AS D2_QTY" + "\n");
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D3_QTY, 0)) AS D3_QTY" + "\n");
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D4_QTY, 0)) AS D4_QTY" + "\n");
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D5_QTY, 0)) AS D5_QTY" + "\n");
                        strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D6_QTY, 0)) AS D6_QTY" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.LastWeek + "', D5_QTY, 0)) AS D0_QTY" + "\n");
                        strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.LastWeek + "', D6_QTY, 0)) AS D1_QTY" + "\n");
                        strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D0_QTY, 0)) AS D2_QTY" + "\n");
                        strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D1_QTY, 0)) AS D3_QTY" + "\n");
                        strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D2_QTY, 0)) AS D4_QTY" + "\n");
                        strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D3_QTY, 0)) AS D5_QTY" + "\n");
                        strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D4_QTY, 0)) AS D6_QTY" + "\n");
                    }
                    strSqlString.Append("                                  FROM RWIPPLNWEK" + "\n");
                    strSqlString.Append("                                 WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");
                    strSqlString.Append("                                   AND PLAN_WEEK IN ('" + FindWeek.LastWeek + "', '" + FindWeek.ThisWeek + "')" + "\n");

                    //if (cboVersion.Text == "영업 Plan")
                    if (cboVersion.SelectedIndex == 0)
                    {
                        strSqlString.Append("                                   AND GUBUN = '0'" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("                                   AND GUBUN = '3'" + "\n");
                    }

                    strSqlString.Append("                                   AND MAT_ID LIKE 'SES%'" + "\n");
                    strSqlString.Append("                                 GROUP BY MAT_ID " + "\n");
                }




                // TEST 조회시 SE 제품은 OMS 계획 사용, 그 외는 SAP 계획 사용
                if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
                {
                    strSqlString.Append("                                 UNION ALL" + "\n");
                    strSqlString.Append("                                SELECT MAT_ID" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[0][0].ToString() + "', PLAN_QTY, 0)) AS D0_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[1][0].ToString() + "', PLAN_QTY, 0)) AS D1_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[2][0].ToString() + "', PLAN_QTY, 0)) AS D2_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[3][0].ToString() + "', PLAN_QTY, 0)) AS D3_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[4][0].ToString() + "', PLAN_QTY, 0)) AS D4_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[5][0].ToString() + "', PLAN_QTY, 0)) AS D5_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[6][0].ToString() + "', PLAN_QTY, 0)) AS D6_QTY" + "\n");
                    strSqlString.Append("                                  FROM (" + "\n");
                    strSqlString.Append("                                        SELECT PLAN_DAY, MAT_ID, PLAN_QTY" + "\n");
                    strSqlString.Append("                                          FROM CWIPPLNDAY" + "\n");
                    strSqlString.Append("                                         WHERE 1=1" + "\n");
                    strSqlString.Append("                                           AND PLAN_DAY BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + FindWeek.EndDay_ThisWeek + "'" + "\n");
                    strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                                         UNION" + "\n");
                    strSqlString.Append("                                        SELECT PLAN_DAY, MAT_ID, PLAN_QTY" + "\n");
                    strSqlString.Append("                                          FROM CWIPPLNSNP@RPTTOMES " + "\n");
                    strSqlString.Append("                                         WHERE 1=1" + "\n");
                    strSqlString.Append("                                           AND SNAPSHOT_DAY BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + FindWeek.EndDay_ThisWeek + "'" + "\n");
                    strSqlString.Append("                                           AND PLAN_DAY = SNAPSHOT_DAY" + "\n");
                    strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                                       )" + "\n");                    
                    strSqlString.Append("                                 GROUP BY MAT_ID " + "\n");
                }
                
                strSqlString.Append("                               )" + "\n");
                strSqlString.Append("                       ) C" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
                strSqlString.Append("                   AND A.MAT_ID = C.MAT_ID(+)" + "\n");
                strSqlString.Append("                   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                   AND A.MAT_TYPE = 'FG'" + "\n");
                strSqlString.Append("                   AND A.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                   AND A.MAT_GRP_3 <> 'COB'" + "\n");
                strSqlString.Append("                   AND A.MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");

                //상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                strSqlString.Append("               )" + "\n");
                strSqlString.Append("         GROUP BY CUST_TYPE, MAT_GRP_1, MAT_GRP_2, MAT_GRP_4, MAT_GRP_6, MAT_GRP_9, MAT_GRP_10, MAT_CMF_11, CONV_MAT_ID" + "\n");
                strSqlString.Append("       )" + "\n");
                strSqlString.Append(" GROUP BY " + QueryCond2 + "\n");
                strSqlString.Append("HAVING SUM(NVL(D0_PLN,0) + NVL(D1_PLN,0) + NVL(D2_PLN,0) + NVL(D3_PLN,0) + NVL(D4_PLN,0) + NVL(D5_PLN,0) + NVL(D6_PLN,0) + NVL(WW_PLN_2,0) + NVL(WW_SHP_2,0)) > 0 " + "\n");
                strSqlString.Append(" ORDER BY " + QueryCond1 + "\n");
            }
            #endregion

            #region 3.Volpas
            else
            {
                strSqlString.AppendFormat("SELECT {0}, ' ' " + "\n", QueryCond3);
                strSqlString.Append("     , ROUND(SUM(D0_PLN) / " + sKpcsValue + ", 0) AS D0_PLN" + "\n");
                strSqlString.Append("     , ROUND(SUM(D1_PLN) / " + sKpcsValue + ", 0) AS D1_PLN" + "\n");
                strSqlString.Append("     , ROUND(SUM(D2_PLN) / " + sKpcsValue + ", 0) AS D2_PLN" + "\n");
                strSqlString.Append("     , ROUND(SUM(D3_PLN) / " + sKpcsValue + ", 0) AS D3_PLN" + "\n");
                strSqlString.Append("     , ROUND(SUM(D4_PLN) / " + sKpcsValue + ", 0) AS D4_PLN" + "\n");
                strSqlString.Append("     , ROUND(SUM(D5_PLN) / " + sKpcsValue + ", 0) AS D5_PLN" + "\n");
                strSqlString.Append("     , ROUND(SUM(D6_PLN) / " + sKpcsValue + ", 0) AS D6_PLN" + "\n");
                strSqlString.Append("     , ROUND(SUM(" + sWeekTotal_1 + ") / " + sKpcsValue + ", 0) AS WW_PLN_1" + "\n");
                strSqlString.Append("     , ROUND(SUM(WW_PLN_2) / " + sKpcsValue + ", 0) AS WW_PLN_2" + "\n");
                strSqlString.Append("     , ROUND(SUM(D0_SHP) / " + sKpcsValue + ", 0) AS D0_SHP" + "\n");
                strSqlString.Append("     , ROUND(SUM(D1_SHP) / " + sKpcsValue + ", 0) AS D1_SHP" + "\n");
                strSqlString.Append("     , ROUND(SUM(D2_SHP) / " + sKpcsValue + ", 0) AS D2_SHP" + "\n");
                strSqlString.Append("     , ROUND(SUM(D3_SHP) / " + sKpcsValue + ", 0) AS D3_SHP" + "\n");
                strSqlString.Append("     , ROUND(SUM(D4_SHP) / " + sKpcsValue + ", 0) AS D4_SHP" + "\n");
                strSqlString.Append("     , ROUND(SUM(D5_SHP) / " + sKpcsValue + ", 0) AS D5_SHP" + "\n");
                strSqlString.Append("     , ROUND(SUM(D6_SHP) / " + sKpcsValue + ", 0) AS D6_SHP" + "\n");
                strSqlString.Append("     , ROUND(SUM(WW_SHP_1) / " + sKpcsValue + ", 0) AS WW_SHP_1" + "\n");
                strSqlString.Append("     , ROUND(SUM(WW_SHP_2) / " + sKpcsValue + ", 0) AS WW_SHP_2" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D0_PLN) <= 0 AND SUM(D0_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D0_PLN) <= 0 THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D0_SHP) / SUM(D0_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D0_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D1_PLN) <= 0 AND SUM(D1_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D1_PLN) <= 0 THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D1_SHP) / SUM(D1_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D1_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D2_PLN) <= 0 AND SUM(D2_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D2_PLN) <= 0 THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D2_SHP) / SUM(D2_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D2_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D3_PLN) <= 0 AND SUM(D3_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D3_PLN) <= 0 THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D3_SHP) / SUM(D3_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D3_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D4_PLN) <= 0 AND SUM(D4_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D4_PLN) <= 0 THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D4_SHP) / SUM(D4_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D4_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D5_PLN) <= 0 AND SUM(D5_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D5_PLN) <= 0 THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D5_SHP) / SUM(D5_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D5_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(D6_PLN) <= 0 AND SUM(D6_SHP) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(D6_PLN) <= 0 THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(D6_SHP) / SUM(D6_PLN) * 100" + "\n");
                strSqlString.Append("             END, 0) AS D6_PER" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(" + sWeekTotal_1 + ") <= 0 AND SUM(WW_SHP_1) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(" + sWeekTotal_1 + ") <= 0 THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(WW_SHP_1) / SUM(" + sWeekTotal_1 + ") * 100" + "\n");
                strSqlString.Append("             END, 0) AS WW_PER_1" + "\n");
                strSqlString.Append("     , ROUND(CASE WHEN SUM(WW_PLN_2) <= 0 AND SUM(WW_SHP_2) = 0 THEN 0" + "\n");
                strSqlString.Append("                  WHEN SUM(WW_PLN_2) <= 0 THEN 100" + "\n");
                strSqlString.Append("                  ELSE SUM(WW_SHP_2) / SUM(WW_PLN_2) * 100" + "\n");
                strSqlString.Append("             END, 0) AS WW_PER_2" + "\n");
                strSqlString.Append("     , ROUND((SUM(D0_SHP) - SUM(D0_PLN)) / " + sKpcsValue + ", 0) AS SUM_D0_DEF" + "\n");
                strSqlString.Append("     , ROUND((SUM(D0_SHP + D1_SHP) - SUM(D0_PLN + D1_PLN)) / " + sKpcsValue + ", 0) AS SUM_D1_DEF" + "\n");
                strSqlString.Append("     , ROUND((SUM(D0_SHP + D1_SHP + D2_SHP) - SUM(D0_PLN + D1_PLN + D2_PLN)) / " + sKpcsValue + ", 0) AS SUM_D2_DEF" + "\n");
                strSqlString.Append("     , ROUND((SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP) - SUM(D0_PLN + D1_PLN + D2_PLN + D3_PLN)) / " + sKpcsValue + ", 0) AS SUM_D3_DEF" + "\n");
                strSqlString.Append("     , ROUND((SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP + D4_SHP) - SUM(D0_PLN + D1_PLN + D2_PLN + D3_PLN + D4_PLN)) / " + sKpcsValue + ", 0) AS SUM_D4_DEF" + "\n");
                strSqlString.Append("     , ROUND((SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP + D4_SHP + D5_SHP) - SUM(D0_PLN + D1_PLN + D2_PLN + D3_PLN + D4_PLN + D5_PLN)) / " + sKpcsValue + ", 0) AS SUM_D5_DEF" + "\n");
                strSqlString.Append("     , ROUND((SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP + D4_SHP + D5_SHP + D6_SHP) - SUM(D0_PLN + D1_PLN + D2_PLN + D3_PLN + D4_PLN + D5_PLN + D6_PLN)) / " + sKpcsValue + ", 0) AS SUM_D6_DEF" + "\n");
                strSqlString.Append("     , ROUND((SUM(D0_SHP + D1_SHP + D2_SHP + D3_SHP + D4_SHP + D5_SHP + D6_SHP) - SUM(" + sWeekTotal_1 + ")) / " + sKpcsValue + ", 0) AS WW_DEF_1" + "\n");
                strSqlString.Append("     , ROUND((SUM(WW_SHP_2) - SUM(WW_PLN_2)) / " + sKpcsValue + ", 0) AS WW_DEF_2" + "\n");
                strSqlString.Append("     , 0 AS D0_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , 0 AS D1_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , 0 AS D2_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , 0 AS D3_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , 0 AS D4_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , 0 AS D5_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , 0 AS D6_EXCEED_PLAN" + "\n");
                strSqlString.Append("     , 0 AS WW_EXCEED_PLAN_1" + "\n");
                strSqlString.Append("     , 0 AS WW_EXCEED_PLAN_2" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT CUST_TYPE, MAT_GRP_1, MAT_GRP_2, MAT_GRP_4, MAT_GRP_6, MAT_GRP_9, MAT_GRP_10, MAT_CMF_11, CONV_MAT_ID" + "\n");
                strSqlString.Append("             , SUM(NVL(D0_PLN,0)) AS D0_PLN" + "\n");

                //if (cboVersion.Text == "고정 Plan")
                if (cboVersion.SelectedIndex == 2)
                {
                    #region 고정 Plan
                    strSqlString.Append("             , SUM(NVL(D1_PLN,0)) AS D1_PLN" + "\n");
                    strSqlString.Append("             , SUM(NVL(D2_PLN,0)) AS D2_PLN" + "\n");
                    strSqlString.Append("             , SUM(NVL(D3_PLN,0)) AS D3_PLN" + "\n");
                    strSqlString.Append("             , SUM(NVL(D4_PLN,0)) AS D4_PLN" + "\n");
                    strSqlString.Append("             , SUM(NVL(D5_PLN,0)) AS D5_PLN" + "\n");
                    strSqlString.Append("             , SUM(NVL(D6_PLN,0)) AS D6_PLN" + "\n");
                    strSqlString.Append("             , SUM(NVL(WW_PLN,0)) AS WW_PLN_2" + "\n");
                    strSqlString.Append("             , SUM(NVL(D0_SHP,0)) AS D0_SHP" + "\n");
                    strSqlString.Append("             , SUM(NVL(D1_SHP,0)) AS D1_SHP" + "\n");
                    strSqlString.Append("             , SUM(NVL(D2_SHP,0)) AS D2_SHP" + "\n");
                    strSqlString.Append("             , SUM(NVL(D3_SHP,0)) AS D3_SHP" + "\n");
                    strSqlString.Append("             , SUM(NVL(D4_SHP,0)) AS D4_SHP" + "\n");
                    strSqlString.Append("             , SUM(NVL(D5_SHP,0)) AS D5_SHP" + "\n");
                    strSqlString.Append("             , SUM(NVL(D6_SHP,0)) AS D6_SHP" + "\n");
                    strSqlString.Append("             , SUM(NVL(SUM_D6_SHP,0)) AS WW_SHP_1" + "\n");
                    strSqlString.Append("             , SUM(NVL(SUM_D6_SHP,0)) AS WW_SHP_2" + "\n");
                    #endregion
                }
                else
                {
                    #region 그 외
                    strSqlString.Append("             , CASE WHEN " + DateDT.Rows[0][0].ToString() + " > " + cdvDate.Value.ToString("yyyyMMdd") + " THEN SUM(NVL(D1_PLN,0))" + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN SUM(NVL(SUM_D1_PLN,0) - NVL(SUM_D0_SHP,0)) <= 0 THEN 0" + "\n");
                    strSqlString.Append("                              ELSE SUM(NVL(SUM_D1_PLN,0) - NVL(SUM_D0_SHP,0)) " + "\n");
                    strSqlString.Append("                          END" + "\n");
                    strSqlString.Append("                END AS D1_PLN" + "\n");
                    strSqlString.Append("             , CASE WHEN " + DateDT.Rows[1][0].ToString() + " > " + cdvDate.Value.ToString("yyyyMMdd") + " THEN SUM(NVL(D2_PLN,0))" + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN SUM(NVL(SUM_D2_PLN,0) - NVL(SUM_D1_SHP,0)) <= 0 THEN 0" + "\n");
                    strSqlString.Append("                              ELSE SUM(NVL(SUM_D2_PLN,0) - NVL(SUM_D1_SHP,0)) " + "\n");
                    strSqlString.Append("                          END" + "\n");
                    strSqlString.Append("                END AS D2_PLN" + "\n");
                    strSqlString.Append("             , CASE WHEN " + DateDT.Rows[2][0].ToString() + " > " + cdvDate.Value.ToString("yyyyMMdd") + " THEN SUM(NVL(D3_PLN,0))" + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN SUM(NVL(SUM_D3_PLN,0) - NVL(SUM_D2_SHP,0)) <= 0 THEN 0" + "\n");
                    strSqlString.Append("                              ELSE SUM(NVL(SUM_D3_PLN,0) - NVL(SUM_D2_SHP,0)) " + "\n");
                    strSqlString.Append("                          END" + "\n");
                    strSqlString.Append("                END AS D3_PLN" + "\n");
                    strSqlString.Append("             , CASE WHEN " + DateDT.Rows[3][0].ToString() + " > " + cdvDate.Value.ToString("yyyyMMdd") + " THEN SUM(NVL(D4_PLN,0))" + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN SUM(NVL(SUM_D4_PLN,0) - NVL(SUM_D3_SHP,0)) <= 0 THEN 0" + "\n");
                    strSqlString.Append("                              ELSE SUM(NVL(SUM_D4_PLN,0) - NVL(SUM_D3_SHP,0)) " + "\n");
                    strSqlString.Append("                          END" + "\n");
                    strSqlString.Append("                END AS D4_PLN" + "\n");
                    strSqlString.Append("             , CASE WHEN " + DateDT.Rows[4][0].ToString() + " > " + cdvDate.Value.ToString("yyyyMMdd") + " THEN SUM(NVL(D5_PLN,0))" + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN SUM(NVL(SUM_D5_PLN,0) - NVL(SUM_D4_SHP,0)) <= 0 THEN 0" + "\n");
                    strSqlString.Append("                              ELSE SUM(NVL(SUM_D5_PLN,0) - NVL(SUM_D4_SHP,0)) " + "\n");
                    strSqlString.Append("                          END" + "\n");
                    strSqlString.Append("                END AS D5_PLN" + "\n");
                    strSqlString.Append("             , CASE WHEN " + DateDT.Rows[5][0].ToString() + " > " + cdvDate.Value.ToString("yyyyMMdd") + " THEN SUM(NVL(D6_PLN,0))" + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN SUM(NVL(SUM_D6_PLN,0) - NVL(SUM_D5_SHP,0)) <= 0 THEN 0" + "\n");
                    strSqlString.Append("                              ELSE SUM(NVL(SUM_D6_PLN,0) - NVL(SUM_D5_SHP,0)) " + "\n");
                    strSqlString.Append("                          END" + "\n");
                    strSqlString.Append("                END AS D6_PLN" + "\n");
                    strSqlString.Append("             , SUM(NVL(WW_PLN,0)) AS WW_PLN_2, SUM(NVL(D0_SHP,0)) AS D0_SHP, SUM(NVL(D1_SHP,0)) AS D1_SHP, SUM(NVL(D2_SHP,0)) AS D2_SHP" + "\n");
                    strSqlString.Append("             , SUM(NVL(D3_SHP,0)) AS D3_SHP, SUM(NVL(D4_SHP,0)) AS D4_SHP, SUM(NVL(D5_SHP,0)) AS D5_SHP, SUM(NVL(D6_SHP,0)) AS D6_SHP" + "\n");
                    strSqlString.Append("             , SUM(NVL(SUM_D6_SHP,0)) AS WW_SHP_1, SUM(NVL(SUM_D6_SHP,0)) AS WW_SHP_2" + "\n");
                    #endregion
                }


                
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT A.*" + "\n");
                strSqlString.Append("                     , CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.MAT_GRP_1 = 'FC' AND INSTR(A.MAT_ID, '-') = 0 THEN A.MAT_ID" + "\n");
                strSqlString.Append("                            WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.MAT_GRP_1 = 'FC' THEN SUBSTR(A.MAT_ID, 1, INSTR(A.MAT_ID, '-')-1) || A.MAT_CMF_11" + "\n");
                strSqlString.Append("                            WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.MAT_GRP_1 = 'FS' THEN SUBSTR(A.MAT_ID, 1, 5) || SUBSTR(A.MAT_ID, -3)" + "\n");
                strSqlString.Append("                            WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' THEN A.MAT_CMF_7" + "\n");
                strSqlString.Append("                            WHEN A.MAT_GRP_1 = 'SE' AND A.MAT_GRP_9 = 'MEMORY' THEN 'SEK_________-___' || SUBSTR(A.MAT_ID, -3)" + "\n");
                strSqlString.Append("                            WHEN A.MAT_GRP_1 = 'HX' THEN A.MAT_CMF_10" + "\n");
                strSqlString.Append("                            WHEN A.MAT_GRP_1 = 'CC' THEN A.MAT_CMF_8" + "\n");
                strSqlString.Append("                            ELSE A.MAT_ID" + "\n");
                strSqlString.Append("                       END CONV_MAT_ID" + "\n");
                strSqlString.Append("                     , NVL((SELECT DATA_10 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND  KEY_1 = MAT_GRP_1), '-') AS CUST_TYPE " + "\n");
                strSqlString.Append("                     , B.*, C.*" + "\n");
                strSqlString.Append("                  FROM MWIPMATDEF A" + "\n");
                strSqlString.Append("                     , DT B" + "\n");
                strSqlString.Append("                     , (" + "\n");
                strSqlString.Append("                        SELECT MAT_ID, D0_QTY AS D0_PLN, D1_QTY AS D1_PLN, D2_QTY AS D2_PLN" + "\n");
                strSqlString.Append("                             , D3_QTY AS D3_PLN, D4_QTY AS D4_PLN, D5_QTY AS D5_PLN, D6_QTY AS D6_PLN" + "\n");
                strSqlString.Append("                             , " + sWeekTotal_2 + " AS WW_PLN" + "\n");
                strSqlString.Append("                             , D0_QTY + D1_QTY AS SUM_D1_PLN" + "\n");
                strSqlString.Append("                             , D0_QTY + D1_QTY + D2_QTY AS SUM_D2_PLN" + "\n");
                strSqlString.Append("                             , D0_QTY + D1_QTY + D2_QTY + D3_QTY AS SUM_D3_PLN" + "\n");
                strSqlString.Append("                             , D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY AS SUM_D4_PLN" + "\n");
                strSqlString.Append("                             , D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY AS SUM_D5_PLN" + "\n");
                strSqlString.Append("                             , D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY + D6_QTY AS SUM_D6_PLN" + "\n");
                strSqlString.Append("                          FROM (" + "\n");
                strSqlString.Append("                                SELECT MAT_ID, D0_QTY, D1_QTY, D2_QTY, D3_QTY, D4_QTY, D5_QTY, D6_QTY" + "\n");
                strSqlString.Append("                                  FROM RWIPPLNWEK" + "\n");
                strSqlString.Append("                                 WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                                   AND PLAN_WEEK = '" + FindWeek.ThisWeek + "'" + "\n");

                //if (cboVersion.Text == "영업 Plan")
                if (cboVersion.SelectedIndex == 0)
                {
                    strSqlString.Append("                                   AND GUBUN = '0'" + "\n");
                }
                else
                {
                    strSqlString.Append("                                   AND GUBUN = '3'" + "\n");
                }
                                
                strSqlString.Append("                                   AND MAT_ID NOT LIKE 'SES%'" + "\n");
                strSqlString.Append("                                 UNION ALL" + "\n");

                // TEST 조회시 SE 제품은 OMS 계획 사용, 그 외는 SAP 계획 사용
                if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
                {
                    strSqlString.Append("                                SELECT MAT_ID" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[0][0].ToString() + "', PLAN_QTY, 0)) AS D0_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[1][0].ToString() + "', PLAN_QTY, 0)) AS D1_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[2][0].ToString() + "', PLAN_QTY, 0)) AS D2_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[3][0].ToString() + "', PLAN_QTY, 0)) AS D3_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[4][0].ToString() + "', PLAN_QTY, 0)) AS D4_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[5][0].ToString() + "', PLAN_QTY, 0)) AS D5_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_DAY, '" + DateDT.Rows[6][0].ToString() + "', PLAN_QTY, 0)) AS D6_QTY" + "\n");
                    strSqlString.Append("                                  FROM (" + "\n");
                    strSqlString.Append("                                        SELECT PLAN_DAY, MAT_ID, PLAN_QTY" + "\n");
                    strSqlString.Append("                                          FROM CWIPPLNDAY" + "\n");
                    strSqlString.Append("                                         WHERE 1=1" + "\n");
                    strSqlString.Append("                                           AND PLAN_DAY BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + FindWeek.EndDay_ThisWeek + "'" + "\n");
                    strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                                         UNION" + "\n");
                    strSqlString.Append("                                        SELECT PLAN_DAY, MAT_ID, PLAN_QTY" + "\n");
                    strSqlString.Append("                                          FROM CWIPPLNSNP@RPTTOMES " + "\n");
                    strSqlString.Append("                                         WHERE 1=1" + "\n");
                    strSqlString.Append("                                           AND SNAPSHOT_DAY BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + FindWeek.EndDay_ThisWeek + "'" + "\n");
                    strSqlString.Append("                                           AND PLAN_DAY = SNAPSHOT_DAY" + "\n");
                    strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                                       )" + "\n");
                    strSqlString.Append("                                 GROUP BY MAT_ID " + "\n");
                }
                else
                {
                    strSqlString.Append("                                SELECT MAT_ID" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.LastWeek + "', D5_QTY, 0)) AS D0_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.LastWeek + "', D6_QTY, 0)) AS D1_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D0_QTY, 0)) AS D2_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D1_QTY, 0)) AS D3_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D2_QTY, 0)) AS D4_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D3_QTY, 0)) AS D5_QTY" + "\n");
                    strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D4_QTY, 0)) AS D6_QTY" + "\n");
                    strSqlString.Append("                                  FROM RWIPPLNWEK" + "\n");
                    strSqlString.Append("                                 WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");
                    strSqlString.Append("                                   AND PLAN_WEEK IN ('" + FindWeek.LastWeek + "', '" + FindWeek.ThisWeek + "')" + "\n");

                    //if (cboVersion.Text == "영업 Plan")
                    if (cboVersion.SelectedIndex == 0)
                    {
                        strSqlString.Append("                                   AND GUBUN = '0'" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("                                   AND GUBUN = '3'" + "\n");
                    }
                                        
                    strSqlString.Append("                                   AND MAT_ID LIKE 'SES%'" + "\n");
                    strSqlString.Append("                                 GROUP BY MAT_ID " + "\n");
                }

                strSqlString.Append("                               )" + "\n");
                strSqlString.Append("                       ) C" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
                strSqlString.Append("                   AND A.MAT_ID = C.MAT_ID(+)" + "\n");
                strSqlString.Append("                   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                   AND A.MAT_TYPE = 'FG'" + "\n");
                strSqlString.Append("                   AND A.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                   AND A.MAT_GRP_3 <> 'COB'" + "\n");
                strSqlString.Append("                   AND A.MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");

                //상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                strSqlString.Append("               )" + "\n");
                strSqlString.Append("         GROUP BY CUST_TYPE, MAT_GRP_1, MAT_GRP_2, MAT_GRP_4, MAT_GRP_6, MAT_GRP_9, MAT_GRP_10, MAT_CMF_11, CONV_MAT_ID" + "\n");
                strSqlString.Append("       )" + "\n");
                strSqlString.Append(" GROUP BY " + QueryCond2 + "\n");
                strSqlString.Append("HAVING SUM(NVL(D0_PLN,0) + NVL(D1_PLN,0) + NVL(D2_PLN,0) + NVL(D3_PLN,0) + NVL(D4_PLN,0) + NVL(D5_PLN,0) + NVL(D6_PLN,0) + WW_PLN_2 + WW_SHP_2) > 0 " + "\n");
                strSqlString.Append(" ORDER BY " + QueryCond1 + "\n");                
            }
            #endregion

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

            StringBuilder strSqlString = new StringBuilder();      

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
                                
                spdData.DataSource = dt;
                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                               
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 11, null, null, btnSort);
                int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, sub + 1, 9, 10, 5, 9, btnSort);

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 9, 0, 5, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_FillColumnData(9, new string[] { "plan", "Actual", "Achievement rate", "cumulative total", "exceed_plan"});

                SetAvgVertical2();

                spdData.RPT_AutoFit(false);

                Color color = spdData.ActiveSheet.Cells[5, 9].BackColor;

                for (int i = 8; i < spdData.ActiveSheet.Rows.Count; i = i + 5)
                {
                    if (spdData.ActiveSheet.Cells[i, 9].BackColor == color)
                    {
                        spdData.ActiveSheet.Rows[i].BackColor = Color.LightYellow;
                    }
                }

                // 2020-03-19-김미경 : 계획 대비 실적 초과하는 수량에 대해 계산한 행에 대해서는 보이지 않도록 한다.
                for (int i = 4; i < spdData.ActiveSheet.Rows.Count; i = i + 5)
                {
                    spdData.ActiveSheet.Rows[i].Visible = false;
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
        /// 
        /// </summary>
        public void SetAvgVertical2()
        {
            Color color = spdData.ActiveSheet.Cells[3, 10].BackColor;
            double iship = 0;
            double iplan = 0;
            double iexceed_plan = 0;         // 2020-03-19-김미경 : 계획 대비 실적 초과하는 수량은 달성률 계산식에서 제외

            // Grand TTL
            for (int i = 10; i <= 18; i++)
            {
                iship = Convert.ToDouble(spdData.ActiveSheet.Cells[1, i].Value);
                iplan = Convert.ToDouble(spdData.ActiveSheet.Cells[0, i].Value);
                iexceed_plan = Convert.ToDouble(spdData.ActiveSheet.Cells[4, i].Value);

                if (iplan != 0)
                {
                    spdData.ActiveSheet.Cells[2, i].Value = ((iship - iexceed_plan) / iplan) * 100;
                }
                else
                {
                    spdData.ActiveSheet.Cells[2, i].Value = 0;
                }
            }
            
            // SUB TTL
            for (int i = 5; i < spdData.ActiveSheet.Rows.Count; i = i + 5)
            {
                if (spdData.ActiveSheet.Cells[i, 10].BackColor != color)
                {
                    for (int y = 10; y <= 18; y++)
                    {
                        iship = Convert.ToDouble(spdData.ActiveSheet.Cells[i + 1, y].Value);
                        iplan = Convert.ToDouble(spdData.ActiveSheet.Cells[i, y].Value);
                        iexceed_plan = Convert.ToDouble(spdData.ActiveSheet.Cells[i + 4, y].Value);

                        if (iplan != 0)
                        {
                            spdData.ActiveSheet.Cells[i + 2, y].Value = (iship - iexceed_plan) / iplan * 100;
                        }
                        else
                        {
                            spdData.ActiveSheet.Cells[i + 2, y].Value = 0;
                        }
                    }
                }
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

                ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null, true);

            }
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            //spdData.ExportExcel();            
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);         
        }
        #endregion
        
        private void rdbLipas1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbLipas1.Checked == true)
                cboVersion.Enabled = false;
            else
                cboVersion.Enabled = true;
        }
    }
}
