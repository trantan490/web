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
    public partial class PRD011010 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD011010<br/>
        /// 클래스요약: DA/WB 차수별 Capa 대비 실적 현황 <br/>
        /// 작  성  자: 에이스텍 황종환<br/>
        /// 최초작성일: 2013-08-21<br/>
        /// 상세  설명: DA/WB 차수별 Capa 대비 실적 현황 (김권수 대리 요청)<br/>
        /// 2013-08-23-황종환 : 메인 쿼리 수정(SUB,CUSTOMER 토탈을 구하는 쿼리가 중복되어 비효율적임, WITH절로 각 부분별 데이터 취합후 해당 데이터를 grouping)<br/>
        /// 2013-08-23-황종환 : 0인 값은 안 보이게
        /// 2013-08-26-황종환 : 김권수 대리 수정 요청
        ///                     1. Total 값 도 설비 현황 표시
        ///                     2. 팝업 창 전일 기준 조회 시 전일 22시 기준의 Assign 현황으로 표시
        ///                     3. Capa 효율 부분 현재 시간 감안 하여 표시 실적 / ( Capa Assign / 24 * 경과시간 )
        ///                     4. 설비 대수 중 Run 중인 제품만 별도 표시 항목 추가 ( 설비 대수 밑 )
        ///                     5. 환산 기준 Check BOX 표시
        /// 2013-10-02-황종환 : single 제품도 merge 쪽에 재공이 나오도록 설정 (김권수 대리 요청)
        /// 2013-12-24-임종우 : 설비 효율 통일화 -> DA : 70%, WB : 75% (임태성 요청)
        /// 2013-12-26-임종우 : 2 Depth 팝업 창에 설비 상태 관련 데이터 추가 (김권수 요청)
        /// 2014-04-25-임종우 : PIN TYPE 추가 (임태성 요청)
        /// 2014-07-24-임종우 : 재공 CHIP 부분 쿼리 수정 & 쿼리 줄 맞추기 작업 (임태성 요청)
        /// 2014-07-30-임종우 : 상세 팝업에서 DOWN TIME 추가 (백성호 요청)
        /// 2014-10-28-임종우 : C200 코드의 설비 제외 (임태성K 요청)
        /// 2014-11-06-임종우 : B199 코드의 설비 제외 (임태성K 요청)
        /// 2014-11-14-임종우 : CAPA 효율 변경 - WB : 75% -> 67.5%, 그 외 : 70% -> 63% (임태성K 요청)
        /// 2014-11-17-임종우 : PIN TYPE 표시 후 팝업창의 데이터와 설비 댓수가 맞지 않는 오류 수정
        /// 2014-11-19-임종우 : CAPA 효율 변경 - WB : 67.5% -> 75%, 그 외 : 63% -> 70% (임태성K 요청)
        /// 2015-02-17-임종우 : 9단 STACK 제품까지 표시 되도록 추가 (임태성K 요청)
        /// 2015-07-06-임종우 : 설비 대수 집계 시 DISPATCH 기준 정보가 'Y" 인 설비만 집계 (임태성K 요청)
        /// 2015-09-15-임종우 : C200, B199 설비 제외시 해당코드로 'Down' 된 설비만 제외 (김보람 요청)
        /// 2015-09-24-임종우 : 고객사 명 하드 코딩 되어 있는것을 기준정보로 변경 (임태성K 요청)
        /// 2016-07-12-임종우 : CAPA 효율 GlobalVariable 로 선언하여 변경함.
        /// 2016-07-27-임종우 : 재공-CHIP, MAIN, MAIN_M 3개로 분리. 로직수정 (임태성K 요청)
        /// </summary>
        /// 
        List<string> selectDate = new List<string>();

        private static int posDataColumnStart = 8;
        private static int totalHeaderRowCnt = 8;
        private static int headerColumnCnt = 6;


        public PRD011010()
        {
            InitializeComponent();
            SortInit();
            GridColumnInit();
            cdvDate.Value = DateTime.Now;
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
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 2;
            selectDate.Clear();

            try
            {
                spdData.RPT_AddBasicColumn("업체", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Major", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PKG", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD_CNT", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PKG_CODE", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PIN_TYPE", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("구분", 0, 6, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 35);
                spdData.RPT_AddBasicColumn("", 0, 7, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 50);

                spdData.RPT_AddBasicColumn("D/A", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("W/B", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("", 0, 22, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("", 0, 23, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("", 0, 24, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("", 0, 25, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("", 0, 26, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("", 0, 27, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("TOTAL", 1, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("1차", 1, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("2차", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("3차", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("4차", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("5차", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("6차", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("7차", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("8차", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("9차", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("TOTAL", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("1차", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("2차", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("3차", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("4차", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("5차", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("6차", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("7차", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("8차", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("9차", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);

                spdData.RPT_MerageHeaderColumnSpan(0, 6, 2);
                spdData.RPT_MerageHeaderColumnSpan(0, 8, 10);
                spdData.RPT_MerageHeaderColumnSpan(0, 18, 10);


                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 7, 2);


                DateTime stDate = Convert.ToDateTime(cdvDate.Value.ToString("yyyy-MM") + "-01");
                DateTime edDate = Convert.ToDateTime(cdvDate.Value.ToString("yyyy-MM-dd"));

                int kk = 0;

                for (DateTime ii = stDate; ii <= edDate; ii = ii.AddDays(1))
                {
                    selectDate.Add(ii.ToString("yyyyMMdd"));

                    kk++;
                }

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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "A.MAT_GRP_1 AS CUSTOMER", "A.MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = CUSTOMER)  CUSTOMER", "CUSTOMER", "DECODE(TEMP_VIEW.CUSTOMER, NULL, 1, 'SE', 2, 'HX', 3, 'IM', 4, 'FC', 5, 6), TEMP_VIEW.CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "A.MAT_GRP_9 AS FAMILY", "A.MAT_GRP_9", "FAMILY", "FAMILY", "FAMILY", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "A.MAT_GRP_10 AS PKG", "A.MAT_GRP_10", "PKG", "PKG", "PKG", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lead", "A.MAT_GRP_6 AS LD_CNT", "A.MAT_GRP_6", "LD_CNT", "LD_CNT", "LD_CNT", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG Code", "A.MAT_CMF_11 AS PKG_CODE", "A.MAT_CMF_11", "PKG_CODE", "PKG_CODE", "PKG_CODE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "A.MAT_CMF_10 AS PIN_TYPE", "A.MAT_CMF_10", "PIN_TYPE", "PIN_TYPE", "PIN_TYPE", false);
        }
        
        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        /// 

        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string QueryCond4 = null;
            string QueryCond5 = null;
            string QueryCond1NotNull;
            string QueryCond2NotNull;
            string QueryCond3NotNull;
            string QueryCond4NotNull;
            string QueryCond5NotNull;
            string groupingSetValue = ""; // for subtotal
            string[] groupingSetList = null;// for subtotal
            string start_date;
            string yesterday;
            string date;
            string month;
            string year;
            string lastMonth;
            String Check_Day;
            Boolean isToday = false;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;
            QueryCond5 = tableForm.SelectedValue5ToQueryContainNull;

            QueryCond1NotNull = tableForm.SelectedValueToQuery;
            QueryCond2NotNull = tableForm.SelectedValue2ToQuery;
            QueryCond3NotNull = tableForm.SelectedValue3ToQuery;
            QueryCond4NotNull = tableForm.SelectedValue4ToQuery;
            QueryCond5NotNull = tableForm.SelectedValue5ToQuery;

            groupingSetList = QueryCond4NotNull.Split(',');

            // 총 4개의 그룹이 필요 : grand total, sub total 2 , detail data
            // detail data
            groupingSetValue += "(";
            for (int j = 0; j < groupingSetList.Length; j++) // grand total + subtotal 2 + detail data = 4
            {
                groupingSetValue = groupingSetValue + (j == 0 ? "" : ",") + groupingSetList[j];
            }
            groupingSetValue += ", GUBUN, GUBUN_SEQ)";
            if (groupingSetList.Length > 2)
            {
                // 2st sub total
                groupingSetValue += ", (";
                for (int j = 0; j < 2; j++) // grand total + subtotal 2 + detail data = 4
                {
                    groupingSetValue = groupingSetValue + (j == 0 ? "" : ",") + groupingSetList[j];
                }
                groupingSetValue += ", GUBUN, GUBUN_SEQ)";
            }
            if (groupingSetList.Length > 1)
            {
                // 1st sub total
                groupingSetValue += ", (";
                for (int j = 0; j < 1; j++) // grand total + subtotal 2 + detail data = 4
                {
                    groupingSetValue = groupingSetValue + (j == 0 ? "" : ",") + groupingSetList[j];
                }
                groupingSetValue += ", GUBUN, GUBUN_SEQ)";
            }
            // grand total
            groupingSetValue += ", (GUBUN, GUBUN_SEQ)";



            //for (int i = 0; i < groupingSetList.Length; i++)
            //{
            //    groupingSetValue += "(";
            //    for (int j = 0; j <= 4; j++) // grand total + subtotal 2 + detail data = 4
            //    {
            //        groupingSetValue = groupingSetValue + "," + groupingSetList[j];
            //    }
            //    groupingSetValue += ")";
            //}

            date = cdvDate.SelectedValue();

            DateTime Select_date;
            Select_date = DateTime.Parse(cdvDate.Text.ToString());

            year = Select_date.ToString("yyyy");
            month = Select_date.ToString("yyyyMM");
            start_date = month + "01";
            yesterday = Select_date.AddDays(-1).ToString("yyyyMMdd");
            lastMonth = Select_date.AddMonths(-1).ToString("yyyyMM"); // 지난달

            // 지난달 마지막일 구하기
            DataTable dt1 = null;
            string Last_Month_Last_day = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + lastMonth + "', 'YYYYMM')),'YYYYMMDD') FROM DUAL)";
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", Last_Month_Last_day);
            Last_Month_Last_day = dt1.Rows[0][0].ToString();

            // 달의 마지막일 구하기
            DataTable dt2 = null;
            string last_day = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + month + "', 'YYYYMM')),'YYYYMMDD') FROM DUAL)";
            dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", last_day);
            last_day = dt2.Rows[0][0].ToString();

            // 지난주차의 마지막일 가져오기
            dt1 = null;
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(year, Select_date.ToString("yyyyMMdd")));
            string Lastweek_lastday = dt1.Rows[0][0].ToString();

            // 선택한 날짜가 오늘인지 체크
            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                isToday = true;
            }
            else
            {
                isToday = false;
            }

            Check_Day = selectDate[selectDate.Count - 1];

            strSqlString.Append("WITH TEMP_VIEW AS (" + "\n");            
            strSqlString.Append("SELECT " + QueryCond1 + "\n");
            strSqlString.Append("     , DECODE(SEQ, 1, '대수', 2, 'RUN', 3, 'CAPA', 4, 'CHIP', 5, 'MAIN', 6, 'MAIN_M', 7, '실적', 8, 'CAPA 효율') GUBUN, SEQ GUBUN_SEQ " + "\n");
            strSqlString.Append("     , SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , A.DA_TTL, 0 ) ) DA_TTL " + "\n");
            strSqlString.Append("     , SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , A.WB_TTL, 0 ) ) WB_TTL " + "\n");
            strSqlString.Append("     , SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , A.DA1, 0 ) ) DA1 " + "\n");
            strSqlString.Append("     , SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , A.DA2, 0 ) ) DA2 " + "\n");
            strSqlString.Append("     , SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , A.DA3, 0 ) ) DA3 " + "\n");
            strSqlString.Append("     , SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , A.DA4, 0 ) ) DA4 " + "\n");
            strSqlString.Append("     , SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , A.DA5, 0 ) ) DA5 " + "\n");
            strSqlString.Append("     , SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , A.DA6, 0 ) ) DA6 " + "\n");
            strSqlString.Append("     , SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , A.DA7, 0 ) ) DA7 " + "\n");
            strSqlString.Append("     , SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , A.DA8, 0 ) ) DA8 " + "\n");
            strSqlString.Append("     , SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , A.DA9, 0 ) ) DA9 " + "\n");
            strSqlString.Append("     , SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , A.WB1, 0 ) ) WB1 " + "\n");
            strSqlString.Append("     , SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , A.WB2, 0 ) ) WB2 " + "\n");
            strSqlString.Append("     , SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , A.WB3, 0 ) ) WB3 " + "\n");
            strSqlString.Append("     , SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , A.WB4, 0 ) ) WB4 " + "\n");
            strSqlString.Append("     , SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , A.WB5, 0 ) ) WB5 " + "\n");
            strSqlString.Append("     , SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , A.WB6, 0 ) ) WB6 " + "\n");
            strSqlString.Append("     , SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , A.WB7, 0 ) ) WB7 " + "\n");
            strSqlString.Append("     , SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , A.WB8, 0 ) ) WB8 " + "\n");
            strSqlString.Append("     , SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , A.WB9, 0 ) ) WB9 " + "\n");
            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT B.MAT_GRP_1,  B.MAT_GRP_2,  B.MAT_GRP_3,  B.MAT_GRP_4,  B.MAT_GRP_5,  B.MAT_GRP_6,  B.MAT_GRP_7,  B.MAT_GRP_8,  B.MAT_GRP_9,  B.MAT_GRP_10,  B.MAT_CMF_10, B.MAT_CMF_11, GUBUN,  DA_TTL, WB_TTL, DA1, WB1, DA2, WB2, DA3, WB3, DA4, WB4, DA5, WB5, DA6, WB6, DA7, WB7, DA8, WB8, DA9, WB9 " + "\n");
            strSqlString.Append("          FROM ( " + "\n");

            #region 1.대수, 2.RUN
            strSqlString.Append("                SELECT DECODE(V.V_COL,'RES', '1_설비댓수', '2_RUN_CNT') GUBUN, MAT_ID " + "\n");
            strSqlString.Append("                     , SUM( CASE WHEN OPER LIKE 'A040%' THEN  DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT)  ELSE 0  END ) DA_TTL " + "\n");
            strSqlString.Append("                     , SUM( CASE WHEN OPER LIKE 'A060%' THEN  DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT)  ELSE 0  END ) WB_TTL " + "\n");
            strSqlString.Append("                     , SUM( DECODE(OPER, 'A0400', DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT), 'A0401', DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT), 0) ) DA1 " + "\n");
            strSqlString.Append("                     , SUM( DECODE(OPER, 'A0600', DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT), 'A0601', DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT), 0)) WB1 " + "\n");
            strSqlString.Append("                     , SUM( DECODE(OPER, 'A0402', DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT), 0)) DA2, SUM( DECODE(OPER, 'A0602', DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT), 0)) WB2 " + "\n");
            strSqlString.Append("                     , SUM( DECODE(OPER, 'A0403', DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT), 0)) DA3,SUM( DECODE(OPER, 'A0603', DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT), 0)) WB3 " + "\n");
            strSqlString.Append("                     , SUM( DECODE(OPER, 'A0404', DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT), 0)) DA4, SUM( DECODE(OPER, 'A0604', DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT), 0)) WB4 " + "\n");
            strSqlString.Append("                     , SUM( DECODE(OPER, 'A0405', DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT), 0)) DA5, SUM( DECODE(OPER, 'A0605', DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT), 0)) WB5 " + "\n");
            strSqlString.Append("                     , SUM( DECODE(OPER, 'A0406', DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT), 0)) DA6, SUM( DECODE(OPER, 'A0606', DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT), 0)) WB6 " + "\n");
            strSqlString.Append("                     , SUM( DECODE(OPER, 'A0407', DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT), 0)) DA7, SUM( DECODE(OPER, 'A0607', DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT), 0)) WB7 " + "\n");
            strSqlString.Append("                     , SUM( DECODE(OPER, 'A0408', DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT), 0)) DA8, SUM( DECODE(OPER, 'A0608', DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT), 0)) WB8 " + "\n");
            strSqlString.Append("                     , SUM( DECODE(OPER, 'A0409', DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT), 0)) DA9, SUM( DECODE(OPER, 'A0609', DECODE(V.V_COL,'RES', RES_CNT, RUN_CNT), 0)) WB9 " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT RAS.FACTORY, RAS.RES_STS_2 MAT_ID, RES_GRP_6 RES_MODEL, RES_GRP_7 UPEH_GROUP, RES_STS_8 OPER , COUNT(RES_ID) RES_CNT, NVL(SUM(DECODE(RES_UP_DOWN_FLAG, 'U', DECODE(NVL(LOT.LOT_ID, '-'), '-', 0, 1))), 0)  RUN_CNT " + "\n");

            if (isToday)
            {
                strSqlString.Append("                          FROM MRASRESDEF RAS " + "\n");                
            }
            else
            {
                strSqlString.Append("                          FROM MRASRESDEF_BOH RAS " + "\n");
            }

            strSqlString.Append("                             , ( " + "\n");
            strSqlString.Append("                                SELECT * " + "\n");
            
            if (isToday)
            {
                strSqlString.Append("                                  FROM MWIPLOTSTS LOT " + "\n");
            }
            else
            {
                strSqlString.Append("                                  FROM RWIPLOTSTS_BOH LOT " + "\n");
            }

            strSqlString.Append("                                 WHERE LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");

            if (!isToday)
            {
                strSqlString.Append("                                   AND LOT.CUTOFF_DT = '" + selectDate[selectDate.Count - 1] + "22' " + "\n");
            }

            strSqlString.Append("                                   AND LOT.LOT_TYPE = 'W' AND LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                                   AND LOT.LOT_CMF_5 LIKE 'P%' " + "\n");
            strSqlString.Append("                                   AND (LOT.OPER LIKE 'A040%' OR LOT.OPER LIKE 'A060%' ) " + "\n");
            strSqlString.Append("                                   AND LOT.LOT_STATUS = 'PROC' " + "\n");
            strSqlString.Append("                               ) LOT " + "\n");
            strSqlString.Append("                         WHERE RAS.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");

            if (!isToday)
            {
                strSqlString.Append("                           AND RAS.CUTOFF_DT = '" + selectDate[selectDate.Count - 1] + "22' " + "\n");
            }

            strSqlString.Append("                           AND RAS.FACTORY = LOT.FACTORY(+) " + "\n");
            strSqlString.Append("                           AND RES_ID = LOT.START_RES_ID(+) " + "\n");
            strSqlString.Append("                           AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("                           AND RES_CMF_7 = 'Y' " + "\n");
            strSqlString.Append("                           AND DELETE_FLAG  = ' ' " + "\n");
            strSqlString.Append("                           AND RES_TYPE  = 'EQUIPMENT' " + "\n");
            strSqlString.Append("                           AND (RES_STS_8 LIKE 'A040%' OR RES_STS_8 LIKE 'A060%' ) " + "\n");            
            strSqlString.Append("                           AND (RES_STS_1 NOT IN ('C200', 'B199') OR RES_UP_DOWN_FLAG = 'U') " + "\n");
            strSqlString.Append("                         GROUP BY RAS.FACTORY, RES_STS_2, RES_GRP_6, RES_GRP_7, RES_STS_8 " + "\n");
            strSqlString.Append("                       ) " + "\n");
            strSqlString.Append("                     , (SELECT 'RES' V_COL FROM DUAL UNION ALL SELECT 'RUN' FROM DUAL) V " + "\n");
            strSqlString.Append("                 WHERE MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
            strSqlString.Append("                 GROUP BY  MAT_ID, V.V_COL " + "\n");
            #endregion

            strSqlString.Append("                 UNION ALL " + "\n");

            #region 3.CAPA
            strSqlString.Append("                SELECT '3_CAPA' GUBUN, MAT_ID " + "\n");
            strSqlString.Append("                     , SUM( CASE WHEN OPER LIKE 'A040%' THEN   RES_CNT * NVL(UPEH,0) * 24 * CONV_RATE  ELSE 0  END ) RES_DA_TTL " + "\n");
            strSqlString.Append("                     , SUM( CASE WHEN  OPER LIKE 'A060%' THEN   RES_CNT * NVL(UPEH,0) * 24 * CONV_RATE  ELSE 0  END ) RES_WB_TTL " + "\n");
            strSqlString.Append("                     , SUM( CASE WHEN OPER IN ( 'A0400',  'A0401' ) THEN  RES_CNT * NVL(UPEH,0) * 24 * CONV_RATE ELSE 0 END ) RES_CNT_DA1 " + "\n");
            strSqlString.Append("                     , SUM( CASE WHEN OPER IN ( 'A0600',  'A0601' ) THEN  RES_CNT * NVL(UPEH,0) * 24 * CONV_RATE ELSE 0 END )   RES_CNT_WB1 " + "\n");
            strSqlString.Append("                     , SUM( DECODE( OPER, 'A0402',  RES_CNT * NVL(UPEH,0) * 24 * CONV_RATE, 0)) RES_CNT_DA2, SUM( DECODE( OPER, 'A0602', RES_CNT *  NVL(UPEH,0) * 24 * CONV_RATE, 0))  RES_CNT_WB2 " + "\n");
            strSqlString.Append("                     , SUM( DECODE( OPER, 'A0403',  RES_CNT * NVL(UPEH,0) * 24 * CONV_RATE, 0)) RES_CNT_DA3, SUM( DECODE( OPER, 'A0603',  RES_CNT * NVL(UPEH,0) * 24 * CONV_RATE, 0))  RES_CNT_WB3 " + "\n");
            strSqlString.Append("                     , SUM( DECODE( OPER, 'A0404',  RES_CNT * NVL(UPEH,0) * 24 * CONV_RATE, 0)) RES_CNT_DA4, SUM( DECODE( OPER, 'A0604',  RES_CNT * NVL(UPEH,0) * 24 * CONV_RATE, 0))  RES_CNT_WB4 " + "\n");
            strSqlString.Append("                     , SUM( DECODE( OPER, 'A0405',  RES_CNT * NVL(UPEH,0) * 24 * CONV_RATE, 0)) RES_CNT_DA5, SUM( DECODE( OPER, 'A0605',  RES_CNT * NVL(UPEH,0) * 24 * CONV_RATE, 0))  RES_CNT_WB5 " + "\n");
            strSqlString.Append("                     , SUM( DECODE( OPER, 'A0406',  RES_CNT * NVL(UPEH,0) * 24 * CONV_RATE, 0)) RES_CNT_DA6, SUM( DECODE( OPER, 'A0606',  RES_CNT * NVL(UPEH,0) * 24 * CONV_RATE, 0))  RES_CNT_WB6 " + "\n");
            strSqlString.Append("                     , SUM( DECODE( OPER, 'A0407',  RES_CNT * NVL(UPEH,0) * 24 * CONV_RATE, 0)) RES_CNT_DA7, SUM( DECODE( OPER, 'A0607',  RES_CNT * NVL(UPEH,0) * 24 * CONV_RATE, 0))  RES_CNT_WB7 " + "\n");
            strSqlString.Append("                     , SUM( DECODE( OPER, 'A0408',  RES_CNT * NVL(UPEH,0) * 24 * CONV_RATE, 0)) RES_CNT_DA8, SUM( DECODE( OPER, 'A0608',  RES_CNT * NVL(UPEH,0) * 24 * CONV_RATE, 0))  RES_CNT_WB8 " + "\n");
            strSqlString.Append("                     , SUM( DECODE( OPER, 'A0409',  RES_CNT * NVL(UPEH,0) * 24 * CONV_RATE, 0)) RES_CNT_DA9, SUM( DECODE( OPER, 'A0609',  RES_CNT * NVL(UPEH,0) * 24 * CONV_RATE, 0))  RES_CNT_WB9 " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT RAS.FACTORY, RAS.RES_GRP_6 RES_MODEL, RAS.RES_STS_2 MAT_ID, RAS.RES_STS_8 OPER , COUNT(RES_ID) RES_CNT " + "\n");
            strSqlString.Append("                             , MAX(DECODE(SUBSTR(RAS.RES_STS_8, 1, 3), 'A04', NVL(UPEH.UPEH, 0) * " + GlobalVariable.gdPer_da + ",  NVL(UPEH.UPEH, 0) * " + GlobalVariable.gdPer_wb + " )  ) UPEH " + "\n");

            if (rdbMain.Checked == true)
            {
                strSqlString.Append("                             , 1 CONV_RATE " + "\n");
            }
            else
            {
                strSqlString.Append("                             , CASE WHEN RAS.RES_STS_8 LIKE 'A040%' AND MAX(DATA_1) IS NOT NULL THEN TO_NUMBER(MAX(DATA_1)) " + "\n");
                strSqlString.Append("                                    WHEN RAS.RES_STS_8 LIKE 'A060%' AND MAX(WIRE_CNT) IS NOT NULL THEN TO_NUMBER(MAX(WIRE_CNT)) " + "\n");
                strSqlString.Append("                                    ELSE 1 " + "\n");
                strSqlString.Append("                               END CONV_RATE " + "\n");
            }

            if (isToday)
            {
                strSqlString.Append("                          FROM MRASRESDEF RAS" + "\n");            
            }
            else
            {
                strSqlString.Append("                          FROM MRASRESDEF_BOH RAS" + "\n");
            }

            strSqlString.Append("                             , CRASUPHDEF UPEH" + "\n");
            // 기본,환산(WIRE_CNT, LOSS 반영)
            strSqlString.Append("                             , ( " + "\n");
            strSqlString.Append("                                SELECT MAT_ID, OPER, TCD_CMF_2 AS WIRE_CNT " + "\n");
            strSqlString.Append("                                  FROM CWIPTCDDEF@RPTTOMES " + "\n");
            strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                   AND OPER LIKE 'A060%' " + "\n");
            strSqlString.Append("                                   AND TCD_CMF_2 <> ' ' " + "\n");
            strSqlString.Append("                                   AND SET_FLAG = 'Y' " + "\n");
            strSqlString.Append("                               ) WIR " + "\n");
            strSqlString.Append("                             , ( " + "\n");
            strSqlString.Append("                                SELECT KEY_1 AS MAT_ID, DATA_1 " + "\n");
            strSqlString.Append("                                  FROM MGCMTBLDAT " + "\n");
            strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                   AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') " + "\n");
            strSqlString.Append("                               ) GCM " + "\n");
            strSqlString.Append("                         WHERE RAS.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");

            if (!isToday)
            {
                strSqlString.Append("                           AND RAS.CUTOFF_DT = '" + selectDate[selectDate.Count - 1] + "22' " + "\n");
            }

            strSqlString.Append("                           AND RAS.RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("                           AND RAS.RES_CMF_7 = 'Y' " + "\n");
            strSqlString.Append("                           AND RAS.DELETE_FLAG  = ' ' " + "\n");
            strSqlString.Append("                           AND RAS.RES_TYPE  = 'EQUIPMENT' " + "\n");
            strSqlString.Append("                           AND (RAS.RES_STS_8 LIKE 'A040%' OR RAS.RES_STS_8 LIKE 'A060%') " + "\n");
            strSqlString.Append("                           AND RAS.RES_STS_2 LIKE '" + txtSearchProduct.Text + "'" + "\n");
            strSqlString.Append("                           AND RAS.FACTORY = UPEH.FACTORY(+) " + "\n");
            strSqlString.Append("                           AND RAS.RES_GRP_6 = UPEH.RES_MODEL(+) " + "\n");
            strSqlString.Append("                           AND RAS.RES_STS_2 = UPEH.MAT_ID(+) " + "\n");
            strSqlString.Append("                           AND RAS.RES_STS_8 = UPEH.OPER(+) " + "\n");
            strSqlString.Append("                           AND RAS.RES_STS_2 = WIR.MAT_ID(+) " + "\n");
            strSqlString.Append("                           AND RAS.RES_STS_8 = WIR.OPER(+) " + "\n");
            strSqlString.Append("                           AND RAS.RES_STS_2 = GCM.MAT_ID(+) " + "\n");
            strSqlString.Append("                           AND (RAS.RES_STS_1 NOT IN ('C200', 'B199') OR RAS.RES_UP_DOWN_FLAG = 'U') " + "\n");
            strSqlString.Append("                         GROUP BY RAS.FACTORY, RAS.RES_GRP_6, RAS.RES_STS_2, RAS.RES_STS_8 " + "\n");
            strSqlString.Append("                       ) " + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            #endregion

            strSqlString.Append("                 UNION ALL " + "\n");

            #region 6.MAIN_M
            strSqlString.Append("                SELECT GUBUN, MAT_ID " + "\n");
            strSqlString.Append("                     , DA_1+DA_2+DA_3+DA_4+DA_5+DA_6+DA_7+DA_8+DA_9 AS DA_WIP_TTL " + "\n");
            strSqlString.Append("                     , WB_1+WB_2+WB_3+WB_4+WB_5+WB_6+WB_7+WB_8+WB_9 AS WB_WIP_TTL " + "\n");
            strSqlString.Append("                     , DA_1 " + "\n");
            strSqlString.Append("                     , WB_1 " + "\n");
            strSqlString.Append("                     , DA_2 " + "\n");
            strSqlString.Append("                     , WB_2 " + "\n");
            strSqlString.Append("                     , DA_3 " + "\n");
            strSqlString.Append("                     , WB_3 " + "\n");
            strSqlString.Append("                     , DA_4 " + "\n");
            strSqlString.Append("                     , WB_4 " + "\n");
            strSqlString.Append("                     , DA_5 " + "\n");
            strSqlString.Append("                     , WB_5 " + "\n");
            strSqlString.Append("                     , DA_6 " + "\n");
            strSqlString.Append("                     , WB_6 " + "\n");
            strSqlString.Append("                     , DA_7 " + "\n");
            strSqlString.Append("                     , WB_7 " + "\n");
            strSqlString.Append("                     , DA_8 " + "\n");
            strSqlString.Append("                     , WB_8 " + "\n");
            strSqlString.Append("                     , DA_9 " + "\n");
            strSqlString.Append("                     , WB_9 " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");            
            strSqlString.Append("                        SELECT '6_WIP_MAIN_M' AS GUBUN, LOT.MAT_ID " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 IN ('-', '1st') AND OPER IN ('A0250', 'A0400', 'A0401') THEN QTY_1 ELSE 0 END ) DA_1 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 IN ('Middle', 'Merge' ) AND OPER = 'A0402' THEN QTY_1 ELSE 0 END ) DA_2 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 IN ('Middle 1', 'Merge' ) AND OPER = 'A0403' THEN QTY_1 ELSE 0 END ) DA_3 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 IN ('Middle 2', 'Merge' ) AND OPER = 'A0404' THEN QTY_1 ELSE 0 END ) DA_4 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 IN ('Middle 3', 'Merge' ) AND OPER = 'A0405' THEN QTY_1 ELSE 0 END ) DA_5 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 IN ('Middle 4', 'Merge' ) AND OPER = 'A0406' THEN QTY_1 ELSE 0 END ) DA_6 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 IN ('Middle 5', 'Merge' ) AND OPER = 'A0407' THEN QTY_1 ELSE 0 END ) DA_7 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 IN ('Middle 6', 'Merge' ) AND OPER = 'A0408' THEN QTY_1 ELSE 0 END ) DA_8 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 IN ('Middle 7', 'Merge' ) AND OPER = 'A0409' THEN QTY_1 ELSE 0 END ) DA_9 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER IN ('A0550', 'A0551', 'A0600', 'A0601') THEN QTY_1 ELSE 0 END ) WB_1 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER IN ('A0552', 'A0602') THEN QTY_1 ELSE 0 END ) WB_2 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER IN ('A0553', 'A0603') THEN QTY_1 ELSE 0 END ) WB_3 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER IN ('A0554', 'A0604') THEN QTY_1 ELSE 0 END ) WB_4 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER IN ('A0555', 'A0605') THEN QTY_1 ELSE 0 END ) WB_5 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER IN ('A0556', 'A0606') THEN QTY_1 ELSE 0 END ) WB_6 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER IN ('A0557', 'A0607') THEN QTY_1 ELSE 0 END ) WB_7 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER IN ('A0558', 'A0608') THEN QTY_1 ELSE 0 END ) WB_8 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER IN ('A0559', 'A0609') THEN QTY_1 ELSE 0 END ) WB_9 " + "\n");

            if (isToday)
            {
                strSqlString.Append("                          FROM RWIPLOTSTS LOT " + "\n");
                strSqlString.Append("                             , VWIPMATDEF MAT " + "\n");
                strSqlString.Append("                         WHERE 1 = 1 " + "\n");
            }
            else
            {
                strSqlString.Append("                          FROM RWIPLOTSTS_BOH LOT " + "\n");
                strSqlString.Append("                             , VWIPMATDEF MAT " + "\n");
                strSqlString.Append("                         WHERE LOT.CUTOFF_DT = '" + selectDate[selectDate.Count - 1] + "22' " + "\n");
            }

            strSqlString.Append("                           AND LOT.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("                           AND LOT.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("                           AND LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND LOT.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                           AND LOT.LOT_CMF_5 LIKE 'P%' " + "\n");
            strSqlString.Append("                           AND LOT.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                           AND MAT.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                           AND MAT.DELETE_FLAG <> 'Y' " + "\n");
            strSqlString.Append("                           AND MAT.MAT_GRP_2 <> '-' " + "\n");
            strSqlString.Append("                           AND LOT.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
            strSqlString.Append("                         GROUP BY LOT.MAT_ID " + "\n");
            strSqlString.Append("                       ) " + "\n");
            #endregion

            strSqlString.Append("                 UNION ALL " + "\n");

            #region 5.MAIN
            strSqlString.Append("                SELECT GUBUN, MAT_ID " + "\n");
            strSqlString.Append("                     , DA_1+DA_2+DA_3+DA_4+DA_5+DA_6+DA_7+DA_8+DA_9 AS DA_WIP_TTL " + "\n");
            strSqlString.Append("                     , 0 AS WB_WIP_TTL " + "\n");
            strSqlString.Append("                     , DA_1 " + "\n");
            strSqlString.Append("                     , 0 AS WB_1 " + "\n");
            strSqlString.Append("                     , DA_2 " + "\n");
            strSqlString.Append("                     , 0 AS WB_2 " + "\n");
            strSqlString.Append("                     , DA_3 " + "\n");
            strSqlString.Append("                     , 0 AS WB_3 " + "\n");
            strSqlString.Append("                     , DA_4 " + "\n");
            strSqlString.Append("                     , 0 AS WB_4 " + "\n");
            strSqlString.Append("                     , DA_5 " + "\n");
            strSqlString.Append("                     , 0 AS WB_5 " + "\n");
            strSqlString.Append("                     , DA_6 " + "\n");
            strSqlString.Append("                     , 0 AS WB_6 " + "\n");
            strSqlString.Append("                     , DA_7 " + "\n");
            strSqlString.Append("                     , 0 AS WB_7 " + "\n");
            strSqlString.Append("                     , DA_8 " + "\n");
            strSqlString.Append("                     , 0 AS WB_8 " + "\n");
            strSqlString.Append("                     , DA_9 " + "\n");
            strSqlString.Append("                     , 0 AS WB_9 " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");            
            strSqlString.Append("                        SELECT '5_WIP_MAIN' AS GUBUN, LOT.MAT_ID " + "\n");            
            strSqlString.Append("                             , 0 AS DA_1 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 = '1st' AND OPER IN ('A0402', 'A0500', 'A0501', 'A0530', 'A0531') THEN QTY_1 ELSE 0 END ) DA_2 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 = 'Middle' AND OPER IN ('A0403', 'A0502', 'A0532') THEN QTY_1 ELSE 0 END ) DA_3 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 = 'Middle 1' AND OPER IN ('A0404', 'A0503', 'A0533') THEN QTY_1 ELSE 0 END ) DA_4 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 = 'Middle 2' AND OPER IN ('A0405', 'A0504', 'A0534') THEN QTY_1 ELSE 0 END ) DA_5 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 = 'Middle 3' AND OPER IN ('A0406', 'A0505', 'A0535') THEN QTY_1 ELSE 0 END ) DA_6 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 = 'Middle 4' AND OPER IN ('A0407', 'A0506', 'A0536') THEN QTY_1 ELSE 0 END ) DA_7 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 = 'Middle 5' AND OPER IN ('A0408', 'A0507', 'A0537') THEN QTY_1 ELSE 0 END ) DA_8 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 = 'Middle 6' AND OPER IN ('A0409', 'A0508', 'A0538') THEN QTY_1 ELSE 0 END ) DA_9 " + "\n");
            
            if (isToday)
            {
                strSqlString.Append("                          FROM RWIPLOTSTS LOT " + "\n");
                strSqlString.Append("                             , VWIPMATDEF MAT " + "\n");
                strSqlString.Append("                         WHERE 1 = 1 " + "\n");
            }
            else
            {
                strSqlString.Append("                          FROM RWIPLOTSTS_BOH LOT " + "\n");
                strSqlString.Append("                             , VWIPMATDEF MAT " + "\n");
                strSqlString.Append("                         WHERE LOT.CUTOFF_DT = '" + selectDate[selectDate.Count - 1] + "22' " + "\n");
            }

            strSqlString.Append("                           AND LOT.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("                           AND LOT.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("                           AND LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND LOT.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                           AND LOT.LOT_CMF_5 LIKE 'P%' " + "\n");
            strSqlString.Append("                           AND LOT.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                           AND MAT.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                           AND MAT.DELETE_FLAG <> 'Y' " + "\n");
            strSqlString.Append("                           AND MAT.MAT_GRP_2 <> '-' " + "\n");
            strSqlString.Append("                           AND LOT.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
            strSqlString.Append("                         GROUP BY LOT.MAT_ID " + "\n");
            strSqlString.Append("                       ) " + "\n");
            #endregion

            strSqlString.Append("                 UNION ALL " + "\n");

            #region 4.CHIP
            strSqlString.Append("                SELECT '4_WIP_CHIP' GUBUN, MAT_ID " + "\n");
            strSqlString.Append("                     , SUM(QTY_1) AS DA_WIP_TTL " + "\n");
            strSqlString.Append("                     , 0 AS WB_WIP_TTL " + "\n");
            strSqlString.Append("                     , 0 AS DA_1 " + "\n");
            strSqlString.Append("                     , 0 AS WB_1 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0250', 'A0402') AND MAT_GRP_5 = '2nd' THEN QTY_1 ELSE 0 END) AS DA_2 " + "\n");
            strSqlString.Append("                     , 0 AS WB_2 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0250', 'A0403') AND MAT_GRP_5 = '3rd' THEN QTY_1 ELSE 0 END) AS DA_3 " + "\n");
            strSqlString.Append("                     , 0 AS WB_3 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0250', 'A0404') AND MAT_GRP_5 = '4th' THEN QTY_1 ELSE 0 END) AS DA_4 " + "\n");
            strSqlString.Append("                     , 0 AS WB_4 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0250', 'A0405') AND MAT_GRP_5 = '5th' THEN QTY_1 ELSE 0 END) AS DA_5 " + "\n");
            strSqlString.Append("                     , 0 AS WB_5 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0250', 'A0406') AND MAT_GRP_5 = '6th' THEN QTY_1 ELSE 0 END) AS DA_6 " + "\n");
            strSqlString.Append("                     , 0 AS WB_6 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0250', 'A0407') AND MAT_GRP_5 = '7th' THEN QTY_1 ELSE 0 END) AS DA_7 " + "\n");
            strSqlString.Append("                     , 0 AS WB_7 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0250', 'A0408') AND MAT_GRP_5 = '8th' THEN QTY_1 ELSE 0 END) AS DA_8 " + "\n");
            strSqlString.Append("                     , 0 AS WB_8 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0250', 'A0409') AND MAT_GRP_5 = '9th' THEN QTY_1 ELSE 0 END) AS DA_9 " + "\n");
            strSqlString.Append("                     , 0 AS WB_9 " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT A.MAT_ID, A.OPER, B.MAT_GRP_5, A.QTY_1 " + "\n");
                        
            if (isToday)
            {
                strSqlString.Append("                          FROM RWIPLOTSTS A, MWIPMATDEF B " + "\n");
                strSqlString.Append("                         WHERE 1 = 1 " + "\n");
            }
            else
            {
                strSqlString.Append("                          FROM RWIPLOTSTS_BOH A, MWIPMATDEF B " + "\n");
                strSqlString.Append("                         WHERE A.CUTOFF_DT = '" + selectDate[selectDate.Count - 1] + "22' " + "\n");
            }


            strSqlString.Append("                           AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                           AND A.LOT_CMF_5 LIKE 'P%' " + "\n");
            strSqlString.Append("                           AND A.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                           AND A.OPER IN ('A0250', 'A0402', 'A0403', 'A0404', 'A0405', 'A0406', 'A0407', 'A0408', 'A0409') " + "\n");
            strSqlString.Append("                           AND B.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                           AND B.MAT_GRP_4 NOT IN ('-','FD','FU') " + "\n");
            strSqlString.Append("                           AND B.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th') " + "\n");
            strSqlString.Append("                           AND B.DELETE_FLAG <> 'Y' " + "\n");
            strSqlString.Append("                           AND B.MAT_GRP_2 <> '-' " + "\n");
            strSqlString.Append("                       ) " + "\n");
            strSqlString.Append("                 WHERE MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            #endregion

            strSqlString.Append("                 UNION ALL " + "\n");

            #region 7.실적
            strSqlString.Append("                SELECT '7_실적' GUBUN, MAT_ID " + "\n");
            strSqlString.Append("                     , SUM( CASE WHEN OPER LIKE 'A040%' THEN  QTY * CONV_RATE  ELSE 0  END ) DA_RST_TTL ,  SUM( CASE WHEN OPER LIKE 'A060%' THEN  QTY * CONV_RATE  ELSE 0  END ) WB_RST_TTL " + "\n");
            strSqlString.Append("                     , SUM( CASE WHEN OPER IN ( 'A0400', 'A0401') THEN QTY * CONV_RATE  ELSE 0 END ) DA401 , SUM( CASE WHEN OPER IN ( 'A0600', 'A0601') THEN QTY * CONV_RATE ELSE 0 END )  WB601 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0402', QTY * CONV_RATE,  0)) DA402  , SUM(DECODE(OPER, 'A0602', QTY * CONV_RATE,  0)) WB602 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0403', QTY * CONV_RATE,  0)) DA403  , SUM(DECODE(OPER, 'A0603', QTY * CONV_RATE,  0)) WB603 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0404', QTY * CONV_RATE,  0)) DA404  , SUM(DECODE(OPER, 'A0604', QTY * CONV_RATE,  0)) WB604 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0405', QTY * CONV_RATE,  0)) DA405  , SUM(DECODE(OPER, 'A0605', QTY * CONV_RATE,  0)) WB605 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0406', QTY * CONV_RATE,  0)) DA406  , SUM(DECODE(OPER, 'A0606', QTY * CONV_RATE,  0)) WB606 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0407', QTY * CONV_RATE,  0)) DA407  , SUM(DECODE(OPER, 'A0607', QTY * CONV_RATE,  0)) WB607 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0408', QTY * CONV_RATE,  0)) DA408  , SUM(DECODE(OPER, 'A0608', QTY * CONV_RATE,  0)) WB608 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0409', QTY * CONV_RATE,  0)) DA409  , SUM(DECODE(OPER, 'A0609', QTY * CONV_RATE,  0)) WB609 " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT A.MAT_ID, A.OPER, SUM(S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ) QTY " + "\n");
            
            if (rdbMain.Checked == true)
            {
                strSqlString.Append("                             , 1 CONV_RATE " + "\n");
            }
            else
            {
                strSqlString.Append("                             , CASE WHEN A.OPER LIKE 'A040%' AND MAX(DATA_1) IS NOT NULL THEN TO_NUMBER(MAX(DATA_1)) " + "\n");
                strSqlString.Append("                                    WHEN A.OPER LIKE 'A060%' AND MAX(WIRE_CNT) IS NOT NULL THEN TO_NUMBER(MAX(WIRE_CNT)) " + "\n");
                strSqlString.Append("                                    ELSE 1 " + "\n");
                strSqlString.Append("                               END CONV_RATE " + "\n");
            }
            strSqlString.Append("                          FROM RSUMWIPMOV A " + "\n");
            strSqlString.Append("                             , ( " + "\n");
            strSqlString.Append("                                SELECT MAT_ID, OPER, TCD_CMF_2 AS WIRE_CNT " + "\n");
            strSqlString.Append("                                  FROM CWIPTCDDEF@RPTTOMES " + "\n");
            strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                   AND OPER LIKE 'A060%' " + "\n");
            strSqlString.Append("                                   AND TCD_CMF_2 <> ' ' " + "\n");
            strSqlString.Append("                                   AND SET_FLAG = 'Y' " + "\n");
            strSqlString.Append("                               ) WIR " + "\n");
            strSqlString.Append("                             , ( " + "\n");
            strSqlString.Append("                                SELECT KEY_1 AS MAT_ID, DATA_1 " + "\n");
            strSqlString.Append("                                  FROM MGCMTBLDAT " + "\n");
            strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                   AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') " + "\n");
            strSqlString.Append("                               ) GCM " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND A.MAT_VER  = 1 " + "\n");
            if (isToday)
            {
                strSqlString.Append("                           AND A.WORK_DATE = GET_WORK_DATE(TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS'), 'D')" + "\n");
            }
            else
            {
                strSqlString.Append("                           AND A.WORK_DATE = '" + selectDate[selectDate.Count - 1] + "'" + "\n");
            }
            strSqlString.Append("                           AND A.LOT_TYPE  = 'W' " + "\n");
            strSqlString.Append("                           AND A.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
            strSqlString.Append("                           AND ( A.OPER LIKE  'A040%' OR  A.OPER LIKE  'A060%'  ) " + "\n");
            strSqlString.Append("                           AND A.CM_KEY_3 LIKE 'P%' " + "\n");
            strSqlString.Append("                           AND A.MAT_ID = WIR.MAT_ID(+) " + "\n");
            strSqlString.Append("                           AND A.OPER  = WIR.OPER(+) " + "\n");
            strSqlString.Append("                           AND A.MAT_ID = GCM.MAT_ID(+) " + "\n");
            strSqlString.Append("                         GROUP BY A.MAT_ID, A.OPER " + "\n");
            strSqlString.Append("                       ) " + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            #endregion

            strSqlString.Append("               ) A " + "\n");
            strSqlString.Append("             , (SELECT * FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  AND MAT_TYPE = 'FG' AND MAT_VER =  1 AND  DELETE_FLAG <> 'Y' ) B " + "\n");
            strSqlString.Append("         WHERE A.MAT_ID = B.MAT_ID(+) " + "\n");
            strSqlString.Append("       ) A " + "\n");
            strSqlString.Append("     , ( SELECT SEQ FROM HRTDSUMSEQ@RPTTOMES WHERE SEQ <= 8 ) C " + "\n");
            strSqlString.Append(" WHERE 1 = 1 " + "\n");
            
            //상세조회
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);
            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);
            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);
            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);
            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);
            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);
            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.Append("   AND A.MAT_GRP_1 IS NOT NULL " + "\n");
            strSqlString.Append(" GROUP BY " + QueryCond2 + " \n");
            strSqlString.Append("        , DECODE(SEQ, 1, '설비대수', 2, 'RUN', 3, 'CAPA', 4, 'CHIP', 5, 'MAIN', 6, 'MAIN_M', 7, '실적', 8, 'CAPA 효율'), SEQ" + "\n");
            strSqlString.Append(") " + "\n");
            strSqlString.Append("SELECT " + QueryCond3 + " \n");
            strSqlString.Append("     , DECODE(GUBUN_SEQ, 1, '설비', 2, '설비', 3, 'CAPA', 4, '재공', 5, '재공', 6, '재공', 7, '실적', 8, 'CAPA 효율') AS GUBUN0 " + "\n");
            strSqlString.Append("     , GUBUN " + "\n");

            if (chkKcps.Checked)
            {
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, DA_TTL, 2, DA_TTL, DA_TTL/1000)), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, DA_TTL, 2, DA_TTL, DA_TTL/1000)), '999,999,999'))) AS DA_TTL " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, DA1, 2, DA1, DA1/1000)), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, DA1, 2, DA1, DA1/1000)), '999,999,999')) ) AS DA1 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, DA2, 2, DA2, DA2/1000)), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, DA2, 2, DA2, DA2/1000)), '999,999,999')) ) AS DA2 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, DA3, 2, DA3, DA3/1000)), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, DA3, 2, DA3, DA3/1000)), '999,999,999')) ) AS DA3 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, DA4, 2, DA4, DA4/1000)), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, DA4, 2, DA4, DA4/1000)), '999,999,999')) ) AS DA4 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, DA5, 2, DA5, DA5/1000)), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, DA5, 2, DA5, DA5/1000)), '999,999,999')) ) AS DA5 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, DA6, 2, DA6, DA6/1000)), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, DA6, 2, DA6, DA6/1000)), '999,999,999')) ) AS DA6 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, DA7, 2, DA7, DA7/1000)), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, DA7, 2, DA7, DA7/1000)), '999,999,999')) ) AS DA7 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, DA8, 2, DA8, DA8/1000)), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, DA8, 2, DA8, DA8/1000)), '999,999,999')) ) AS DA8 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, DA9, 2, DA9, DA9/1000)), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, DA9, 2, DA9, DA9/1000)), '999,999,999')) ) AS DA9 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, WB_TTL, 2, WB_TTL, WB_TTL/1000)), '999,999,999')),'0', '',TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, WB_TTL, 2, WB_TTL, WB_TTL/1000)), '999,999,999'))) AS WB_TTL " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, WB1, 2, WB1, WB1/1000)), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, WB1, 2, WB1, WB1/1000)), '999,999,999'))) AS WB1 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, WB2, 2, WB2, WB2/1000)), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, WB2, 2, WB2, WB2/1000)), '999,999,999'))) AS WB2 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, WB3, 2, WB3, WB3/1000)), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, WB3, 2, WB3, WB3/1000)), '999,999,999'))) AS WB3 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, WB4, 2, WB4, WB4/1000)), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, WB4, 2, WB4, WB4/1000)), '999,999,999'))) AS WB4 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, WB5, 2, WB5, WB5/1000)), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, WB5, 2, WB5, WB5/1000)), '999,999,999'))) AS WB5 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, WB6, 2, WB6, WB6/1000)), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, WB6, 2, WB6, WB6/1000)), '999,999,999'))) AS WB6 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, WB7, 2, WB7, WB7/1000)), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, WB7, 2, WB7, WB7/1000)), '999,999,999'))) AS WB7 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, WB8, 2, WB8, WB8/1000)), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, WB8, 2, WB8, WB8/1000)), '999,999,999'))) AS WB8 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, WB9, 2, WB9, WB9/1000)), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(DECODE(GUBUN_SEQ, 1, WB9, 2, WB9, WB9/1000)), '999,999,999'))) AS WB9 " + "\n");
            }
            else
            {
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DA_TTL), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(DA_TTL), '999,999,999'))) AS DA_TTL " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DA1), '999,999,999'))   ,'0','',TRIM(TO_CHAR(SUM(DA1), '999,999,999'))   ) AS DA1 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DA2), '999,999,999'))   ,'0','',TRIM(TO_CHAR(SUM(DA2), '999,999,999'))   ) AS DA2 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DA3), '999,999,999'))   ,'0','',TRIM(TO_CHAR(SUM(DA3), '999,999,999'))   ) AS DA3 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DA4), '999,999,999'))   ,'0','',TRIM(TO_CHAR(SUM(DA4), '999,999,999'))   ) AS DA4 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DA5), '999,999,999'))   ,'0','',TRIM(TO_CHAR(SUM(DA5), '999,999,999'))   ) AS DA5 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DA6), '999,999,999'))   ,'0','',TRIM(TO_CHAR(SUM(DA6), '999,999,999'))   ) AS DA6 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DA7), '999,999,999'))   ,'0','',TRIM(TO_CHAR(SUM(DA7), '999,999,999'))   ) AS DA7 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DA8), '999,999,999'))   ,'0','',TRIM(TO_CHAR(SUM(DA8), '999,999,999'))   ) AS DA8 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(DA9), '999,999,999'))   ,'0','',TRIM(TO_CHAR(SUM(DA9), '999,999,999'))   ) AS DA9 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(WB_TTL), '999,999,999')),'0','',TRIM(TO_CHAR(SUM(WB_TTL), '999,999,999'))) AS WB_TTL " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(WB1), '999,999,999'))   ,'0','',TRIM(TO_CHAR(SUM(WB1), '999,999,999'))   ) AS WB1 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(WB2), '999,999,999'))   ,'0','',TRIM(TO_CHAR(SUM(WB2), '999,999,999'))   ) AS WB2 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(WB3), '999,999,999'))   ,'0','',TRIM(TO_CHAR(SUM(WB3), '999,999,999'))   ) AS WB3 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(WB4), '999,999,999'))   ,'0','',TRIM(TO_CHAR(SUM(WB4), '999,999,999'))   ) AS WB4 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(WB5), '999,999,999'))   ,'0','',TRIM(TO_CHAR(SUM(WB5), '999,999,999'))   ) AS WB5 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(WB6), '999,999,999'))   ,'0','',TRIM(TO_CHAR(SUM(WB6), '999,999,999'))   ) AS WB6 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(WB7), '999,999,999'))   ,'0','',TRIM(TO_CHAR(SUM(WB7), '999,999,999'))   ) AS WB7 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(WB8), '999,999,999'))   ,'0','',TRIM(TO_CHAR(SUM(WB8), '999,999,999'))   ) AS WB8 " + "\n");
                strSqlString.Append("     , DECODE(TRIM(TO_CHAR(SUM(WB9), '999,999,999'))   ,'0','',TRIM(TO_CHAR(SUM(WB9), '999,999,999'))   ) AS WB9 " + "\n");

            }
            strSqlString.Append("  FROM TEMP_VIEW " + "\n");
            strSqlString.Append(" GROUP BY GROUPING SETS(" + groupingSetValue + " ) " + "\n");
            strSqlString.Append(" ORDER BY " + QueryCond5NotNull + ",GUBUN_SEQ " + "\n");



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

        // Cell 색상 변화
        private void ChangeCellColor()
        {
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            string[] colList = tableForm.SelectedValueToQueryContainNull.Split(',');
            int[] subTotalIndexList = new int[2];
            int notNullColCnt = tableForm.SelectedValueToQuery.Split(',').Length;
            for (int i = 1, j = 0; i < colList.Length; i++)
            {
                if (!colList[i].Trim().Equals("' '"))
                {
                    subTotalIndexList[j] = i;
                    j++;
                    if (j == 2 || notNullColCnt == j)
                    {
                        break;
                    }
                }
            }

            // 칼럼 고정(total sum fixed)
            spdData.Sheets[0].FrozenColumnCount = posDataColumnStart;
            spdData.Sheets[0].FrozenRowCount = totalHeaderRowCnt;

            // Column Auto Fit
            spdData.RPT_AutoFit(false);

            // Cell Color 
            #region CELL COLOR CHANGE
            //  1. column header
            spdData.ActiveSheet.Columns[0].BackColor = Color.LemonChiffon;
            spdData.ActiveSheet.Columns[1].BackColor = Color.LemonChiffon;
            spdData.ActiveSheet.Columns[2].BackColor = Color.LemonChiffon;
            spdData.ActiveSheet.Columns[3].BackColor = Color.LemonChiffon;
            spdData.ActiveSheet.Columns[4].BackColor = Color.LemonChiffon;
            spdData.ActiveSheet.Columns[5].BackColor = Color.LemonChiffon;
            //  2. grand total Cell Color setting
            spdData.ActiveSheet.Rows[0].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
            spdData.ActiveSheet.Rows[1].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
            spdData.ActiveSheet.Rows[2].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
            spdData.ActiveSheet.Rows[3].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
            spdData.ActiveSheet.Rows[4].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
            spdData.ActiveSheet.Rows[5].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
            spdData.ActiveSheet.Rows[6].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));

            // 시트에 대한 색 지정, 콜스팬, 텍스트 지정
            int colCnt = spdData.ActiveSheet.Columns.Count;
            for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                if (spdData.ActiveSheet.Cells[i, subTotalIndexList[0]].Text.Trim().Equals("") && !spdData.ActiveSheet.Cells[i, 0].Text.Trim().Equals("")) // customer total
                {
                    spdData.ActiveSheet.Rows[i].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));
                    spdData.ActiveSheet.Cells[i, subTotalIndexList[0]].Text = spdData.ActiveSheet.Cells[i, 0].Text + " TOTAL";
                    spdData.ActiveSheet.Cells[i, subTotalIndexList[1]].Text = "  ";
                }
                else if (spdData.ActiveSheet.Cells[i, subTotalIndexList[1]].Text.Trim().Equals("") && !spdData.ActiveSheet.Cells[i, 0].Text.Trim().Equals("")) // family total
                {
                    spdData.ActiveSheet.Rows[i].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));
                    spdData.ActiveSheet.Cells[i, subTotalIndexList[1]].Text = spdData.ActiveSheet.Cells[i, subTotalIndexList[0]].Text + " TOTAL";
                }
                // 구분의 재공 관련 col span
                if (!spdData.ActiveSheet.Cells[i, 6].Text.Equals("재공") && !spdData.ActiveSheet.Cells[i, 6].Text.Equals("설비"))
                {
                    spdData.ActiveSheet.Cells[i, 6].ColumnSpan = 2;
                }
                // 실적/CAPA효율 컬럼 색 지정
                if (spdData.ActiveSheet.Cells[i, 6].Text.Equals("CAPA 효율") || spdData.ActiveSheet.Cells[i, 6].Text.Equals("Performance"))
                {
                    spdData.ActiveSheet.Rows[i].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(221)), ((System.Byte)(217)), ((System.Byte)(195)));
                }
            }

            // grand total 관련 col,row span 설정
            for (int i = 0; i < totalHeaderRowCnt; i++)
            {
                spdData.ActiveSheet.Cells[i, 0].ColumnSpan = headerColumnCnt;
            }
            spdData.ActiveSheet.Cells[0, 0].RowSpan = 8;

            spdData.ActiveSheet.Cells[0, 0, 6, 5].Text = "TOTAL";

            spdData.ActiveSheet.Columns.Get(6).Width = 35;
            spdData.ActiveSheet.Columns.Get(7).Width = 50;
            #endregion
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (CheckField() == false) return;

            DataTable dt = null;

            GridColumnInit();

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                spdData_Sheet1.RowCount = 0;
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
                ChangeCellColor();


                // Capa 효율 계산
                #region CAPA 효율 계산
                double output = 0; // 실적
                double capa = 0;
                string capaEff = "";
                double todayPastRate = 1; // 오늘 날짜 조회할 경우 오늘이 흐른 비율을 CAPA값에 곱하기 위해 선언

                // 선택한 날짜가 오늘인지 체크
                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    if (Convert.ToInt32(DateTime.Now.ToString("HH")) >= 22)
                    {
                        DateTime baseTime = DateTime.ParseExact(DateTime.Now.ToString("yyyyMMdd") + "220000", "yyyyMMddHHmmss", null);
                        TimeSpan diff = DateTime.Now - baseTime;
                        todayPastRate = diff.TotalSeconds / (24 * 60 * 60);
                    }
                    else
                    {
                        DateTime baseTime = DateTime.ParseExact(DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "220000", "yyyyMMddHHmmss", null);
                        TimeSpan diff = DateTime.Now - baseTime;
                        todayPastRate = diff.TotalSeconds / (24 * 60 * 60);
                    }
                }

                for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    if (spdData.ActiveSheet.Cells[i, 6].Text.Equals("CAPA 효율")) // 구분 항목
                    {
                        for (int j = posDataColumnStart; j < spdData.ActiveSheet.Columns.Count; j++)
                        {
                            output = Convert.ToInt32(spdData.ActiveSheet.Cells[i - 1, j].Text.Replace(",", "").Equals("") ? "0" : spdData.ActiveSheet.Cells[i - 1, j].Text.Replace(",", ""));
                            capa = Convert.ToInt32(spdData.ActiveSheet.Cells[i - 5, j].Text.Replace(",", "").Equals("") ? "0" : spdData.ActiveSheet.Cells[i - 5, j].Text.Replace(",", ""));
                            if (capa == 0)
                            {
                                capaEff = "0%";
                            }
                            else
                            {
                                // 오늘 날짜를 조회할 경우 오늘의 흐른 시간 대비 CAPA를 결정한다.
                                // 실적 / (CAPA*오늘이 흐른 비율)
                                capaEff = string.Concat(((int)(output * 100 / (capa * todayPastRate))).ToString(), "%");

                            }
                            spdData.ActiveSheet.Cells[i, j].Text = capaEff;
                        }
                    }
                }
                #endregion

                // 6번째 구분 칼럼까지 칼럼 자동 머지 설정
                for (int i = 0; i < posDataColumnStart; i++)
                {
                    spdData.ActiveSheet.SetColumnMerge(i, FarPoint.Win.Spread.Model.MergePolicy.Always);
                }
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

        // 지난 주차의 마지막일 가져오기
        private string MakeSqlString2(string year, string date)
        {
            StringBuilder sqlString = new StringBuilder();

            sqlString.Append("SELECT MIN(SYS_DATE-1) " + "\n");
            sqlString.Append("  FROM MWIPCALDEF " + "\n");
            sqlString.Append(" WHERE 1=1" + "\n");
            sqlString.Append("   AND CALENDAR_ID='SE'" + "\n");
            sqlString.Append("   AND PLAN_YEAR='" + year + "'\n");
            sqlString.Append("   AND PLAN_WEEK=(" + "\n");
            sqlString.Append("                  SELECT PLAN_WEEK " + "\n");
            sqlString.Append("                    FROM MWIPCALDEF " + "\n");
            sqlString.Append("                   WHERE 1=1 " + "\n");
            sqlString.Append("                     AND CALENDAR_ID='SE' " + "\n");
            sqlString.Append("                     AND SYS_DATE=TO_CHAR(TO_DATE('" + date + "','YYYYMMDD'),'YYYYMMDD')" + "\n");
            sqlString.Append("                 )" + "\n");

            return sqlString.ToString();
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
            spdData.ExportExcel("DAWB차수별 CAPA 대비 실적 현황");
        }

        #endregion

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string colHeaderStepStr = "";
            string colHeaderChipStr = "";
            string rowHeaderCustomStr = "";
            string rowHeaderMajorStr = "";
            string rowHeaderPKGStr = "";
            string rowHeaderLDStr = "";
            string rowHeaderPKGCDStr = "";
            string rowHeaderGubunStr = "";
            string rowHeaderPinTypeStr = "";

            if (pnlEqp.Visible == true) pnlEqp.Visible = false;

            try
            {
                if (e.Row >= 0 && e.Column >= 8)
                {
                    colHeaderStepStr = spdData.Sheets[0].ColumnHeader.Cells[0, e.Column >= 18 ? 18 : 8].Text; // D/A, W/B 구분
                    colHeaderChipStr = spdData.Sheets[0].ColumnHeader.Cells[1, e.Column].Text;                // 1,2,3,4,5차, TOTAL

                    for (int i = 0; i < spdData.ActiveSheet.ColumnCount; i++)
                    {
                        if ("CUSTOMER" == spdData.ActiveSheet.GetColumnLabel(0, i))
                        {
                            rowHeaderCustomStr = spdData.Sheets[0].Cells[e.Row, i].Text;
                        }
                        if ("MAJOR CODE" == spdData.ActiveSheet.GetColumnLabel(0, i))
                        {
                            rowHeaderMajorStr = spdData.Sheets[0].Cells[e.Row, i].Text;
                        }
                        if ("PACKAGE" == spdData.ActiveSheet.GetColumnLabel(0, i))
                        {
                            rowHeaderPKGStr = spdData.Sheets[0].Cells[e.Row, i].Text;
                        }
                        if ("Lead" == spdData.ActiveSheet.GetColumnLabel(0, i))
                        {
                            rowHeaderLDStr = spdData.Sheets[0].Cells[e.Row, i].Text;
                        }
                        if ("PKG Code" == spdData.ActiveSheet.GetColumnLabel(0, i))
                        {
                            rowHeaderPKGCDStr = spdData.Sheets[0].Cells[e.Row, i].Text;
                        }
                        if ("PIN TYPE" == spdData.ActiveSheet.GetColumnLabel(0, i))
                        {
                            rowHeaderPinTypeStr = spdData.Sheets[0].Cells[e.Row, i].Text;
                        }
                    }
                    
                    rowHeaderGubunStr = spdData.Sheets[0].Cells[e.Row, 6].Text;

                    if (!spdData.Sheets[0].Cells[e.Row, e.Column].Text.Trim().Equals(""))
                    {
                        showThePopup(rowHeaderCustomStr, rowHeaderMajorStr, rowHeaderPKGStr, rowHeaderLDStr, rowHeaderPKGCDStr, rowHeaderGubunStr, colHeaderChipStr, colHeaderStepStr, rowHeaderPinTypeStr);
                    }
                }
            }
            catch (Exception)
            {
                pnlEqp.Visible = false;
            }


        }

        //POPUP 쿼리 및 WINDOW 실행.
        private void showThePopup(string rowCustomStr, string rowMajorStr, string rowPKGStr, string rowLDStr, string rowPKGCDStr, string rowGubunStr, string colChipStr, string colStepStr, string rowPinTypeStr)
        {
            DataTable dtPop = null;
            FarPoint.Win.Spread.FpSpread spdTemp = new FarPoint.Win.Spread.FpSpread();

            if (rowCustomStr.Trim().Equals("") || rowCustomStr.EndsWith("TOTAL"))
            {
                rowCustomStr = "%";
            }
            if (rowMajorStr.Trim().Equals("") || rowMajorStr.EndsWith("TOTAL"))
            {
                rowMajorStr = "%";
            }
            if (rowPKGStr.Trim().Equals("") || rowPKGStr.EndsWith("TOTAL"))
            {
                rowPKGStr = "%";
            }
            if (rowLDStr.Trim().Equals("") || rowLDStr.EndsWith("TOTAL"))
            {
                rowLDStr = "%";
            }
            if (rowPKGCDStr.Trim().Equals("") || rowPKGCDStr.EndsWith("TOTAL"))
            {
                rowPKGCDStr = "%";
            }
            if (rowPinTypeStr.Trim().Equals("") || rowPinTypeStr.EndsWith("TOTAL"))
            {
                rowPinTypeStr = "%";
            }

            try
            {
                //"Arange 댓수", "CAPA" 만 수행.
                if (rowGubunStr.Equals("설비") || rowGubunStr.Equals("CAPA"))
                {
                    LoadingPopUp.LoadIngPopUpShow(this);
                    spdData_Eqp_Pos.Sheets[0].RowCount = 0;

                    this.Refresh();

                    if (rowGubunStr.Equals("설비") || rowGubunStr.Equals("CAPA"))
                    {
                        if (colChipStr.Equals("TOTAL"))
                        {
                            dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", popupWindowEQPSQL(rowCustomStr, rowMajorStr, rowPKGStr, rowLDStr, rowPKGCDStr, colStepStr, rowPinTypeStr));
                        }
                        else if (!colChipStr.Equals("TTL") && !colChipStr.Equals("Etc Chip"))
                        {
                            if (colStepStr.Equals("D/A"))
                            {
                                dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", popupWindowDAEQPSQL(rowCustomStr, rowMajorStr, rowPKGStr, rowLDStr, rowPKGCDStr, colChipStr, rowPinTypeStr));
                            }
                            else
                            {
                                dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", popupWindowWBEQPSQL(rowCustomStr, rowMajorStr, rowPKGStr, rowLDStr, rowPKGCDStr, colChipStr, rowPinTypeStr));
                            }
                        }

                        if (dtPop != null && dtPop.Rows.Count > 0)
                        {
                            LoadingPopUp.LoadingPopUpHidden();
                            System.Windows.Forms.Form frm = new PRD011010_P1(rowCustomStr + ", " + rowMajorStr + ", " + rowPKGStr + ", " + rowLDStr + ", " + rowPKGCDStr + ", " + colStepStr + ", " + colChipStr + ", " + rowPinTypeStr, dtPop, this);
                            frm.ShowDialog();
                        }
                    }
                    else
                    {
                        LoadingPopUp.LoadingPopUpHidden();
                        return;
                    }

                    if (dtPop.Rows.Count == 0)
                    {
                        dtPop.Dispose();
                        LoadingPopUp.LoadingPopUpHidden();
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD066", GlobalVariable.gcLanguage));
                        return;
                    }
                }


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


        private string popupWindowDAEQPSQL(string rowCustomStr, string rowMajorStr, string rowPKGStr, string rowLDStr, string rowPKGCDStr, string colChipStr, string rowPinTypeStr)
        {
            StringBuilder sqlString = new StringBuilder();
            string strCustomer = "";
            Boolean isToday = false;

            // 선택한 날짜가 오늘인지 체크
            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                isToday = true;
            }
            else
            {
                isToday = false;
            }

            switch (rowCustomStr)
            {
                case "SEC":
                    strCustomer = "SE";
                    break;
                case "HYNIX":
                    strCustomer = "HX";
                    break;
                case "iML":
                    strCustomer = "IM";
                    break;
                case "FCI":
                    strCustomer = "FC";
                    break;
                case "IMAGIS":
                    strCustomer = "IG";
                    break;
                case "%":
                    strCustomer = "%";
                    break;
                default:
                    strCustomer = "-";
                    break;
            }

            sqlString.Append("SELECT DECODE(RES_MODEL, 'Z TOTAL', 'TOTAL', RES_MODEL) RES_MODEL, RES_CNT, UPEH, CAPA, RUN_CNT, WAIT_CNT, RES_DOWN_CNT, DEV_CHANG_CNT" + "\n");
            sqlString.Append("  FROM ( SELECT  DECODE(TTLSEQ.SEQ, 2, 'Z TOTAL', RAS.RES_MODEL) RES_MODEL, SUM(NVL(RAS.RES_CNT, 0)) RES_CNT, MAX( NVL(UPEH.UPEH, 0) ) UPEH," + "\n");
            sqlString.Append("                           SUM( DECODE(SUBSTR(RAS.OPER, 1, 3), 'A04',   NVL(RAS.RES_CNT, 0) *  NVL(UPEH.UPEH,0) * 24 * " + GlobalVariable.gdPer_da + ",  NVL(RAS.RES_CNT, 0) *  NVL(UPEH.UPEH,0) * 24 * " + GlobalVariable.gdPer_wb + "  ) ) CAPA," + "\n");
            sqlString.Append("                           SUM(RAS.WAIT_CNT)  WAIT_CNT, SUM(RAS.RUN_CNT) RUN_CNT, SUM(RAS.RES_DOWN_CNT) RES_DOWN_CNT, SUM(RAS.DEV_CHANG_CNT) DEV_CHANG_CNT" + "\n");
            sqlString.Append("                 FROM ( SELECT RAS.FACTORY, RAS.RES_GRP_6 RES_MODEL, MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11, MAT.MAT_CMF_10,  RAS.RES_STS_2 MAT_ID, RAS.RES_STS_8 OPER , COUNT(RES_ID) RES_CNT," + "\n");
            sqlString.Append("                                         NVL(SUM(DECODE(RES_UP_DOWN_FLAG, 'U', DECODE(NVL(LOT.LOT_ID, '-'), '-', 1, 0))), 0)  WAIT_CNT," + "\n");
            sqlString.Append("                                         NVL(SUM(DECODE(RES_UP_DOWN_FLAG, 'U', DECODE(NVL(LOT.LOT_ID, '-'), '-', 0, 1))), 0)  RUN_CNT,         \n");
            sqlString.Append("                                         SUM(DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(NVL(SUBSTR(RES_STS_1, 1,1), '-'), 'D', 0, 1), 0) ) RES_DOWN_CNT," + "\n");
            sqlString.Append("                                         SUM(DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(NVL(SUBSTR(RES_STS_1, 1,1), '-'), 'D', 1, 0), 0) ) DEV_CHANG_CNT" + "\n");
            if (isToday)
            {
                sqlString.Append("                              FROM  MRASRESDEF RAS," + "\n");
            }
            else
            {
                sqlString.Append("                              FROM  MRASRESDEF_BOH RAS," + "\n");
            }
            sqlString.Append("                                        ( SELECT *" + "\n");
            if (isToday)
            {
                sqlString.Append("                                             FROM MWIPLOTSTS LOT" + "\n");
            }
            else
            {
                sqlString.Append("                                             FROM RWIPLOTSTS_BOH LOT" + "\n");
            }

            sqlString.Append("                                           WHERE LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            if (!isToday)
            {
                sqlString.Append("                                             AND LOT.CUTOFF_DT = '" + selectDate[selectDate.Count - 1] + "22' " + "\n");
            }
            sqlString.Append("                                                AND LOT.LOT_TYPE = 'W' AND LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                                                AND LOT.LOT_CMF_5 LIKE 'P%' " + "\n");
            sqlString.Append("                                                AND LOT.OPER LIKE 'A040%' " + "\n");
            sqlString.Append("                                                AND LOT.LOT_STATUS = 'PROC' ) LOT, MWIPMATDEF MAT" + "\n");
            sqlString.Append("                         WHERE RAS.FACTORY   = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            if (!isToday)
            {
                sqlString.Append("                          AND RAS.CUTOFF_DT = '" + selectDate[selectDate.Count - 1] + "22' " + "\n");
            }
            sqlString.Append("                             AND RAS.RES_CMF_9 = 'Y'" + "\n");
            sqlString.Append("                             AND RAS.RES_CMF_7 = 'Y'" + "\n");
            sqlString.Append("                             AND RAS.DELETE_FLAG  = ' '" + "\n");
            sqlString.Append("                             AND RAS.RES_TYPE  = 'EQUIPMENT'" + "\n");
            sqlString.Append("                             AND RAS.RES_STS_8 LIKE 'A040%' " + "\n");
            sqlString.Append("                             AND RAS.FACTORY = LOT.FACTORY(+)" + "\n");
            sqlString.Append("                             AND RAS.RES_ID = LOT.START_RES_ID(+)" + "\n");
            sqlString.Append("                             AND RAS.FACTORY = MAT.FACTORY(+)" + "\n");
            sqlString.Append("                             AND RAS.RES_STS_2 = MAT.MAT_ID(+)" + "\n");
            sqlString.Append("                             AND RAS.RES_STS_2 LIKE '" + txtSearchProduct.Text + "'" + "\n");
            sqlString.Append("                             AND MAT.MAT_TYPE = 'FG'" + "\n");
            sqlString.Append("                             AND MAT.DELETE_FLAG = ' '" + "\n");
            sqlString.Append("                             AND MAT.MAT_VER = 1" + "\n");
            //sqlString.Append("                             AND RAS.RES_STS_1 NOT IN ('C200', 'B199')" + "\n");
            sqlString.Append("                             AND (RAS.RES_STS_1 NOT IN ('C200', 'B199') OR RAS.RES_UP_DOWN_FLAG = 'U')" + "\n");

            //상세조회
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);
            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);
            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);
            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);
            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);
            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);
            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            sqlString.Append("                         GROUP BY RAS.FACTORY,RAS. RES_STS_2,RAS.RES_GRP_6,RAS.RES_STS_8,MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11, MAT.MAT_CMF_10 ) RAS, CRASUPHDEF UPEH , ( SELECT SEQ FROM HRTDSUMSEQ@RPTTOMES WHERE SEQ < 3 ) TTLSEQ" + "\n");
            sqlString.Append("          WHERE RAS.FACTORY = UPEH.FACTORY(+)" + "\n");
            sqlString.Append("               AND RAS.RES_MODEL = UPEH.RES_MODEL(+)" + "\n");
            sqlString.Append("               AND RAS.MAT_ID = UPEH.MAT_ID(+)" + "\n");
            sqlString.Append("               AND RAS.OPER = UPEH.OPER(+)" + "\n");

            if (strCustomer.Equals("-"))
            {
                sqlString.Append("              AND RAS.MAT_GRP_1 in   ( SELECT KEY_1" + "\n");
                sqlString.Append("                                                         FROM MGCMTBLDAT@RPTTOMES" + "\n");
                sqlString.Append("                                                       WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                sqlString.Append("                                                           AND TABLE_NAME = 'H_CUSTOMER'" + "\n");
                sqlString.Append("                                                           AND DATA_1 LIKE '" + rowCustomStr + "' )" + "\n");
            }
            else sqlString.Append("              AND RAS.MAT_GRP_1 LIKE '" + strCustomer + "'" + "\n");

            sqlString.Append("              AND RAS.MAT_GRP_9 LIKE '" + rowMajorStr + "'" + "\n");
            sqlString.Append("              AND RAS.MAT_GRP_10 LIKE '" + rowPKGStr + "'" + "\n");
            sqlString.Append("              AND RAS.MAT_GRP_6 LIKE '" + rowLDStr + "'" + "\n");
            sqlString.Append("              AND RAS.MAT_CMF_11 LIKE '" + rowPKGCDStr + "'" + "\n");
            sqlString.Append("              AND RAS.MAT_CMF_10 LIKE '" + rowPinTypeStr + "'" + "\n");

            if (colChipStr.Substring(0, 1).Equals("1"))
            {
                sqlString.Append("              AND ( RAS.OPER = 'A0400' OR  RAS.OPER = 'A0401')  " + " \n");
            }
            else
            {
                sqlString.Append("              AND RAS.OPER = 'A040" + colChipStr.Substring(0, 1) + "'" + "\n");
            }

            sqlString.Append("    GROUP BY DECODE(TTLSEQ.SEQ, 2, 'Z TOTAL', RAS.RES_MODEL)" + "\n");
            sqlString.Append("    ORDER BY RES_MODEL" + "\n");
            sqlString.Append("   )" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(sqlString.ToString());
            }

            return sqlString.ToString();

        }

        private string popupWindowWBEQPSQL(string rowCustomStr, string rowMajorStr, string rowPKGStr, string rowLDStr, string rowPKGCDStr, string colChipStr, string rowPinTypeStr)
        {
            StringBuilder sqlString = new StringBuilder();
            string strCustomer = "";
            Boolean isToday = false;

            // 선택한 날짜가 오늘인지 체크
            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                isToday = true;
            }
            else
            {
                isToday = false;
            }

            switch (rowCustomStr)
            {
                case "SEC":
                    strCustomer = "SE";
                    break;
                case "HYNIX":
                    strCustomer = "HX";
                    break;
                case "iML":
                    strCustomer = "IM";
                    break;
                case "FCI":
                    strCustomer = "FC";
                    break;
                case "IMAGIS":
                    strCustomer = "IG";
                    break;
                case "%":
                    strCustomer = "%";
                    break;
                default:
                    strCustomer = "-";
                    break;
            }

            sqlString.Append("SELECT DECODE(RES_MODEL, 'Z TOTAL', 'TOTAL', RES_MODEL) RES_MODEL, RES_CNT, UPEH, CAPA, RUN_CNT, WAIT_CNT, RES_DOWN_CNT, DEV_CHANG_CNT" + "\n");
            sqlString.Append("  FROM ( SELECT  DECODE(TTLSEQ.SEQ, 2, 'Z TOTAL', RAS.RES_MODEL) RES_MODEL, SUM(NVL(RAS.RES_CNT, 0)) RES_CNT, MAX( NVL(UPEH.UPEH, 0) ) UPEH," + "\n");
            sqlString.Append("                           SUM( DECODE(SUBSTR(RAS.OPER, 1, 3), 'A04',   NVL(RAS.RES_CNT, 0) *  NVL(UPEH.UPEH,0) * 24 * " + GlobalVariable.gdPer_da + ",  NVL(RAS.RES_CNT, 0) *  NVL(UPEH.UPEH,0) * 24 * " + GlobalVariable.gdPer_wb + "  ) )CAPA," + "\n");
            sqlString.Append("                           SUM(RAS.WAIT_CNT)  WAIT_CNT, SUM(RAS.RUN_CNT) RUN_CNT, SUM(RAS.RES_DOWN_CNT) RES_DOWN_CNT, SUM(RAS.DEV_CHANG_CNT) DEV_CHANG_CNT" + "\n");
            sqlString.Append("                 FROM ( SELECT RAS.FACTORY, RAS.RES_GRP_6 RES_MODEL, MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11, MAT.MAT_CMF_10,  RAS.RES_STS_2 MAT_ID, RAS.RES_STS_8 OPER , COUNT(RES_ID) RES_CNT," + "\n");
            sqlString.Append("                                         NVL(SUM(DECODE(RES_UP_DOWN_FLAG, 'U', DECODE(NVL(LOT.LOT_ID, '-'), '-', 1, 0))), 0)  WAIT_CNT," + "\n");
            sqlString.Append("                                         NVL(SUM(DECODE(RES_UP_DOWN_FLAG, 'U', DECODE(NVL(LOT.LOT_ID, '-'), '-', 0, 1))), 0)  RUN_CNT,         \n");
            sqlString.Append("                                         SUM(DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(NVL(SUBSTR(RES_STS_1, 1,1), '-'), 'D', 0, 1), 0) ) RES_DOWN_CNT," + "\n");
            sqlString.Append("                                         SUM(DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(NVL(SUBSTR(RES_STS_1, 1,1), '-'), 'D', 1, 0), 0) ) DEV_CHANG_CNT" + "\n");
            if (isToday)
            {
                sqlString.Append("                              FROM  MRASRESDEF RAS," + "\n");
            }
            else
            {
                sqlString.Append("                              FROM  MRASRESDEF_BOH RAS," + "\n");
            }
            sqlString.Append("                                        ( SELECT *" + "\n");
            if (isToday)
            {
                sqlString.Append("                                             FROM MWIPLOTSTS LOT" + "\n");
            }
            else
            {
                sqlString.Append("                                             FROM RWIPLOTSTS_BOH LOT" + "\n");
            }

            sqlString.Append("                                           WHERE LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            if (!isToday)
            {
                sqlString.Append("                                             AND LOT.CUTOFF_DT = '" + selectDate[selectDate.Count - 1] + "22' " + "\n");
            }
            sqlString.Append("                                                AND LOT.LOT_TYPE = 'W' AND LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                                                AND LOT.LOT_CMF_5 LIKE 'P%' " + "\n");
            sqlString.Append("                                                AND LOT.OPER LIKE 'A060%' " + "\n");
            sqlString.Append("                                                AND LOT.LOT_STATUS = 'PROC' ) LOT, MWIPMATDEF MAT" + "\n");
            sqlString.Append("                         WHERE RAS.FACTORY   = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            if (!isToday)
            {
                sqlString.Append("                          AND RAS.CUTOFF_DT = '" + selectDate[selectDate.Count - 1] + "22' " + "\n");
            }
            sqlString.Append("                             AND RAS.RES_CMF_9 = 'Y'" + "\n");
            sqlString.Append("                             AND RAS.RES_CMF_7 = 'Y'" + "\n");
            sqlString.Append("                             AND RAS.DELETE_FLAG  = ' '" + "\n");
            sqlString.Append("                             AND RAS.RES_TYPE  = 'EQUIPMENT'" + "\n");
            sqlString.Append("                             AND (RAS.RES_STS_8 LIKE 'A060%')" + "\n");
            sqlString.Append("                             AND RAS.FACTORY = LOT.FACTORY(+)" + "\n");
            sqlString.Append("                             AND RAS.RES_ID = LOT.START_RES_ID(+)" + "\n");
            sqlString.Append("                             AND RAS.FACTORY = MAT.FACTORY(+)" + "\n");
            sqlString.Append("                             AND RAS.RES_STS_2 = MAT.MAT_ID(+)" + "\n");
            sqlString.Append("                             AND RAS.RES_STS_2 LIKE '" + txtSearchProduct.Text + "'" + "\n");
            sqlString.Append("                             AND MAT.MAT_TYPE = 'FG'" + "\n");
            sqlString.Append("                             AND MAT.DELETE_FLAG = ' '" + "\n");
            sqlString.Append("                             AND MAT.MAT_VER = 1" + "\n");
            //sqlString.Append("                             AND RAS.RES_STS_1 NOT IN ('C200', 'B199')" + "\n");
            sqlString.Append("                             AND (RAS.RES_STS_1 NOT IN ('C200', 'B199') OR RAS.RES_UP_DOWN_FLAG = 'U')" + "\n");

            //상세조회
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);
            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);
            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);
            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);
            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);
            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);
            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            sqlString.Append("                         GROUP BY RAS.FACTORY,RAS. RES_STS_2,RAS.RES_GRP_6,RAS.RES_STS_8,MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11, MAT.MAT_CMF_10  ) RAS, CRASUPHDEF UPEH , ( SELECT SEQ FROM HRTDSUMSEQ@RPTTOMES WHERE SEQ < 3 ) TTLSEQ" + "\n");
            sqlString.Append("          WHERE RAS.FACTORY = UPEH.FACTORY(+)" + "\n");
            sqlString.Append("               AND RAS.RES_MODEL = UPEH.RES_MODEL(+)" + "\n");
            sqlString.Append("               AND RAS.MAT_ID = UPEH.MAT_ID(+)" + "\n");
            sqlString.Append("               AND RAS.OPER = UPEH.OPER(+)" + "\n");

            if (strCustomer.Equals("-"))
            {
                sqlString.Append("              AND RAS.MAT_GRP_1 IN   ( SELECT KEY_1" + "\n");
                sqlString.Append("                                                         FROM MGCMTBLDAT@RPTTOMES" + "\n");
                sqlString.Append("                                                       WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                sqlString.Append("                                                           AND TABLE_NAME = 'H_CUSTOMER'" + "\n");
                sqlString.Append("                                                           AND DATA_1 LIKE '" + rowCustomStr + "' )" + "\n");
            }
            else sqlString.Append("              AND RAS.MAT_GRP_1 LIKE '" + strCustomer + "'" + "\n");

            sqlString.Append("              AND RAS.MAT_GRP_9 LIKE '" + rowMajorStr + "'" + "\n");
            sqlString.Append("              AND RAS.MAT_GRP_10 LIKE '" + rowPKGStr + "'" + "\n");
            sqlString.Append("              AND RAS.MAT_GRP_6 LIKE '" + rowLDStr + "'" + "\n");
            sqlString.Append("              AND RAS.MAT_CMF_11 LIKE '" + rowPKGCDStr + "'" + "\n");
            sqlString.Append("              AND RAS.MAT_CMF_10 LIKE '" + rowPinTypeStr + "'" + "\n");

            if (colChipStr.Substring(0, 1).Equals("1"))
            {
                sqlString.Append("              AND ( RAS.OPER = 'A0600' OR  RAS.OPER = 'A0601')  " + " \n");
            }
            else
            {
                sqlString.Append("              AND RAS.OPER = 'A060" + colChipStr.Substring(0, 1) + "'" + "\n");
            }

            sqlString.Append("    GROUP BY DECODE(TTLSEQ.SEQ, 2, 'Z TOTAL', RAS.RES_MODEL)" + "\n");
            sqlString.Append("    ORDER BY RES_MODEL" + "\n");
            sqlString.Append("   )" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(sqlString.ToString());
            }

            return sqlString.ToString();

        }

        private string popupWindowEQPSQL(string rowCustomStr, string rowMajorStr, string rowPKGStr, string rowLDStr, string rowPKGCDStr, string colStepStr, string rowPinTypeStr)
        {
            StringBuilder sqlString = new StringBuilder();
            string strCustomer = "";
            Boolean isToday = false;

            // 선택한 날짜가 오늘인지 체크
            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                isToday = true;
            }
            else
            {
                isToday = false;
            }

            switch (rowCustomStr)
            {
                case "SEC":
                    strCustomer = "SE";
                    break;
                case "HYNIX":
                    strCustomer = "HX";
                    break;
                case "iML":
                    strCustomer = "IM";
                    break;
                case "FCI":
                    strCustomer = "FC";
                    break;
                case "IMAGIS":
                    strCustomer = "IG";
                    break;
                case "%":
                    strCustomer = "%";
                    break;
                default:
                    strCustomer = "-";
                    break;
            }

            sqlString.Append("SELECT DECODE(RES_MODEL, 'Z TOTAL', 'TOTAL', RES_MODEL) RES_MODEL, RES_CNT, UPEH, CAPA, RUN_CNT, WAIT_CNT, RES_DOWN_CNT, DEV_CHANG_CNT" + "\n");
            sqlString.Append("  FROM ( SELECT  DECODE(TTLSEQ.SEQ, 2, 'Z TOTAL', RAS.RES_MODEL) RES_MODEL, SUM(NVL(RAS.RES_CNT, 0)) RES_CNT, MAX( NVL(UPEH.UPEH, 0) ) UPEH," + "\n");
            sqlString.Append("                           SUM( DECODE(SUBSTR(RAS.OPER, 1, 3), 'A04',   NVL(RAS.RES_CNT, 0) *  NVL(UPEH.UPEH,0) * 24 * " + GlobalVariable.gdPer_da + ",  NVL(RAS.RES_CNT, 0) *  NVL(UPEH.UPEH,0) * 24 * " + GlobalVariable.gdPer_wb + "  ) )CAPA," + "\n");
            sqlString.Append("                           SUM(RAS.WAIT_CNT)  WAIT_CNT, SUM(RAS.RUN_CNT) RUN_CNT, SUM(RAS.RES_DOWN_CNT) RES_DOWN_CNT, SUM(RAS.DEV_CHANG_CNT) DEV_CHANG_CNT" + "\n");
            sqlString.Append("                 FROM ( SELECT RAS.FACTORY, RAS.RES_GRP_6 RES_MODEL, MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11, MAT.MAT_CMF_10,  RAS.RES_STS_2 MAT_ID, RAS.RES_STS_8 OPER , COUNT(RES_ID) RES_CNT," + "\n");
            sqlString.Append("                                         NVL(SUM(DECODE(RES_UP_DOWN_FLAG, 'U', DECODE(NVL(LOT.LOT_ID, '-'), '-', 1, 0))), 0)  WAIT_CNT," + "\n");
            sqlString.Append("                                         NVL(SUM(DECODE(RES_UP_DOWN_FLAG, 'U', DECODE(NVL(LOT.LOT_ID, '-'), '-', 0, 1))), 0)  RUN_CNT,         \n");
            sqlString.Append("                                         SUM(DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(NVL(SUBSTR(RES_STS_1, 1,1), '-'), 'D', 0, 1), 0) ) RES_DOWN_CNT," + "\n");
            sqlString.Append("                                         SUM(DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(NVL(SUBSTR(RES_STS_1, 1,1), '-'), 'D', 1, 0), 0) ) DEV_CHANG_CNT" + "\n");
            if (isToday)
            {
                sqlString.Append("                              FROM  MRASRESDEF RAS," + "\n");
            }
            else
            {
                sqlString.Append("                              FROM  MRASRESDEF_BOH RAS," + "\n");
            }
            sqlString.Append("                                        ( SELECT *" + "\n");
            if (isToday)
            {
                sqlString.Append("                                             FROM MWIPLOTSTS LOT" + "\n");
            }
            else
            {
                sqlString.Append("                                             FROM RWIPLOTSTS_BOH LOT" + "\n");
            }

            sqlString.Append("                                           WHERE LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            if (!isToday)
            {
                sqlString.Append("                                             AND LOT.CUTOFF_DT = '" + selectDate[selectDate.Count - 1] + "22' " + "\n");
            }
            sqlString.Append("                                                AND LOT.LOT_TYPE = 'W' AND LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                                                AND LOT.LOT_CMF_5 LIKE 'P%' " + "\n");
            sqlString.Append("                                                AND (LOT.OPER LIKE 'A040%' OR LOT.OPER LIKE 'A060%')" + "\n");
            sqlString.Append("                                                AND LOT.LOT_STATUS = 'PROC' ) LOT, MWIPMATDEF MAT" + "\n");
            sqlString.Append("                         WHERE RAS.FACTORY   = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            if (!isToday)
            {
                sqlString.Append("                          AND RAS.CUTOFF_DT = '" + selectDate[selectDate.Count - 1] + "22' " + "\n");
            }
            sqlString.Append("                             AND RAS.RES_CMF_9 = 'Y'" + "\n");
            sqlString.Append("                             AND RAS.RES_CMF_7 = 'Y'" + "\n");
            sqlString.Append("                             AND RAS.DELETE_FLAG  = ' '" + "\n");
            sqlString.Append("                             AND RAS.RES_TYPE  = 'EQUIPMENT'" + "\n");
            sqlString.Append("                             AND (RAS.RES_STS_8 LIKE 'A040%' OR RAS.RES_STS_8 LIKE 'A060%')" + "\n");
            sqlString.Append("                             AND RAS.FACTORY = LOT.FACTORY(+)" + "\n");
            sqlString.Append("                             AND RAS.RES_ID = LOT.START_RES_ID(+)" + "\n");
            sqlString.Append("                             AND RAS.FACTORY = MAT.FACTORY(+)" + "\n");
            sqlString.Append("                             AND RAS.RES_STS_2 = MAT.MAT_ID(+)" + "\n");
            sqlString.Append("                             AND RAS.RES_STS_2 LIKE '" + txtSearchProduct.Text + "'" + "\n");
            sqlString.Append("                             AND MAT.MAT_TYPE = 'FG'" + "\n");
            sqlString.Append("                             AND MAT.DELETE_FLAG = ' '" + "\n");
            sqlString.Append("                             AND MAT.MAT_VER = 1" + "\n");
            //sqlString.Append("                             AND RAS.RES_STS_1 NOT IN ('C200', 'B199')" + "\n");
            sqlString.Append("                             AND (RAS.RES_STS_1 NOT IN ('C200', 'B199') OR RAS.RES_UP_DOWN_FLAG = 'U')" + "\n");
            
            //상세조회
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);
            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);
            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);
            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);
            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);
            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);
            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                sqlString.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            sqlString.Append("                         GROUP BY RAS.FACTORY,RAS. RES_STS_2,RAS.RES_GRP_6,RAS.RES_STS_8,MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11, MAT.MAT_CMF_10  ) RAS, CRASUPHDEF UPEH , ( SELECT SEQ FROM HRTDSUMSEQ@RPTTOMES WHERE SEQ < 3 ) TTLSEQ" + "\n");
            sqlString.Append("          WHERE RAS.FACTORY = UPEH.FACTORY(+)" + "\n");
            sqlString.Append("               AND RAS.RES_MODEL = UPEH.RES_MODEL(+)" + "\n");
            sqlString.Append("               AND RAS.MAT_ID = UPEH.MAT_ID(+)" + "\n");
            sqlString.Append("               AND RAS.OPER = UPEH.OPER(+)" + "\n");

            if (strCustomer.Equals("-"))
            {
                sqlString.Append("              AND RAS.MAT_GRP_1 IN   ( SELECT KEY_1" + "\n");
                sqlString.Append("                                                         FROM MGCMTBLDAT@RPTTOMES" + "\n");
                sqlString.Append("                                                       WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                sqlString.Append("                                                           AND TABLE_NAME = 'H_CUSTOMER'" + "\n");
                sqlString.Append("                                                           AND DATA_1 LIKE '" + rowCustomStr + "' )" + "\n");
            }
            else sqlString.Append("              AND RAS.MAT_GRP_1 LIKE '" + strCustomer + "'" + "\n");

            sqlString.Append("              AND RAS.MAT_GRP_9 LIKE '" + rowMajorStr + "'" + "\n");
            sqlString.Append("              AND RAS.MAT_GRP_10 LIKE '" + rowPKGStr + "'" + "\n");
            sqlString.Append("              AND RAS.MAT_GRP_6 LIKE '" + rowLDStr + "'" + "\n");
            sqlString.Append("              AND RAS.MAT_CMF_11 LIKE '" + rowPKGCDStr + "'" + "\n");
            sqlString.Append("              AND RAS.MAT_CMF_10 LIKE '" + rowPinTypeStr + "'" + "\n");            

            if (colStepStr.Equals("W/B"))
                sqlString.Append("              AND RAS.OPER LIKE 'A060%'" + "\n");
            else sqlString.Append("              AND RAS.OPER LIKE 'A040%'" + "\n");

            sqlString.Append("    GROUP BY DECODE(TTLSEQ.SEQ, 2, 'Z TOTAL', RAS.RES_MODEL)" + "\n");
            sqlString.Append("    ORDER BY RES_MODEL" + "\n");
            sqlString.Append("   )" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(sqlString.ToString());
            }

            return sqlString.ToString();

        }

        public string popupWindowResListSQL(string rowCustomStr, string rowMajorStr, string rowPKGStr, string rowLDStr, string rowPKGCDStr, string colStepStr, string colChipStr, string strColumn, string strModelName)
        {
            StringBuilder strSqlString = new StringBuilder();
            string strCustomer = "";
            string strCond1 = "";
            string strCond2 = "";
            string strCond3 = "";
            Boolean isToday = false;


            if (colChipStr.Equals("TOTAL"))
            {
                strCond1 = "                   AND (LOT.OPER LIKE 'A040%' OR LOT.OPER LIKE 'A060%')" + "\n";
                strCond2 = "           AND (RAS.RES_STS_8 LIKE 'A040%' OR RAS.RES_STS_8 LIKE 'A060%')" + "\n";

                if (colStepStr.Equals("W/B"))
                    strCond3 = "   AND RAS.OPER LIKE 'A060%'" + "\n";
                else strCond3 = "   AND RAS.OPER LIKE 'A040%'" + "\n";
            }
            else if (!colChipStr.Equals("TTL") && !colChipStr.Equals("Etc Chip"))
            {
                if (colStepStr.Equals("D/A"))
                {
                    strCond1 = "                   AND LOT.OPER LIKE 'A040%' " + "\n";
                    strCond2 = "           AND RAS.RES_STS_8 LIKE 'A040%' " + "\n";

                    if (colChipStr.Substring(0, 1).Equals("1"))
                    {
                        strCond3 = "   AND ( RAS.OPER = 'A0400' OR  RAS.OPER = 'A0401')  " + " \n";
                    }
                    else
                    {
                        strCond3 = "   AND RAS.OPER = 'A040" + colChipStr.Substring(0, 1) + "'" + "\n";
                    }
                }
                else
                {
                    strCond1 = "                   AND LOT.OPER LIKE 'A060%' " + "\n";
                    strCond2 = "           AND (RAS.RES_STS_8 LIKE 'A060%')" + "\n";

                    if (colChipStr.Substring(0, 1).Equals("1"))
                    {
                        strCond3 = "   AND ( RAS.OPER = 'A0600' OR  RAS.OPER = 'A0601')  " + " \n";
                    }
                    else
                    {
                        strCond3 = "   AND RAS.OPER = 'A060" + colChipStr.Substring(0, 1) + "'" + "\n";
                    }
                }
            }



            // 선택한 날짜가 오늘인지 체크
            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                isToday = true;
            }
            else
            {
                isToday = false;
            }

            switch (rowCustomStr)
            {
                case "SEC":
                    strCustomer = "SE";
                    break;
                case "HYNIX":
                    strCustomer = "HX";
                    break;
                case "iML":
                    strCustomer = "IM";
                    break;
                case "FCI":
                    strCustomer = "FC";
                    break;
                case "IMAGIS":
                    strCustomer = "IG";
                    break;
                case "%":
                    strCustomer = "%";
                    break;
                default:
                    strCustomer = "-";
                    break;
            }
            strSqlString.Append("SELECT REPLACE(MAT_GROUP, '  ', ' ') AS MAT_GROUP, MAT_ID, RES_ID, STATE, RES_STS_1 " + "\n");
            strSqlString.Append("     , CASE WHEN STATE = 'DOWN' THEN TRUNC((SYSDATE - TO_DATE(LAST_DOWN_TIME, 'YYYYMMDDHH24MISS')) * 24, 2) ELSE 0 END DOWN_TIME" + "\n");
            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT RAS.FACTORY, RAS.RES_GRP_6 RES_MODEL, RAS.RES_ID, MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11,  RAS.RES_STS_2 MAT_ID, RAS.RES_STS_8 OPER " + "\n");
            strSqlString.Append("             , REPLACE(MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_GRP_7 || ' ' || MAT_GRP_8 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 || ' ' || " + "\n");
            strSqlString.Append("               ( SELECT ATTR_VALUE " + "\n");
            strSqlString.Append("                   FROM MATRNAMSTS" + "\n");
            strSqlString.Append("                  WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                    AND ATTR_TYPE = 'MAT_ETC'" + "\n");
            strSqlString.Append("                    AND ATTR_NAME = DECODE(MAT_GRP_1, 'SE', 'SEC_VERSION', 'HX_VERSION')" + "\n");
            strSqlString.Append("                    AND ATTR_KEY = RAS.RES_STS_2" + "\n");
            strSqlString.Append("               ), '-', '') AS MAT_GROUP" + "\n");
            strSqlString.Append("             , DECODE(RES_UP_DOWN_FLAG, 'U', 'UP', 'DOWN') AS STATE " + "\n");
            strSqlString.Append("             , RES_STS_1 " + "\n");
            strSqlString.Append("             , NVL(SUM(DECODE(RES_UP_DOWN_FLAG, 'U', DECODE(NVL(LOT.LOT_ID, '-'), '-', 1, 0))), 0)  WAIT_CNT " + "\n");
            strSqlString.Append("             , NVL(SUM(DECODE(RES_UP_DOWN_FLAG, 'U', DECODE(NVL(LOT.LOT_ID, '-'), '-', 0, 1))), 0)  RUN_CNT " + "\n");
            strSqlString.Append("             , SUM(DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(NVL(SUBSTR(RES_STS_1, 1,1), '-'), 'D', 0, 1), 0) ) RES_DOWN_CNT " + "\n");
            strSqlString.Append("             , SUM(DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(NVL(SUBSTR(RES_STS_1, 1,1), '-'), 'D', 1, 0), 0) ) DEV_CHANG_CNT " + "\n");
            strSqlString.Append("             , LAST_DOWN_TIME " + "\n");
            
            if (isToday)
            {
                strSqlString.Append("          FROM  MRASRESDEF RAS" + "\n");
            }
            else
            {
                strSqlString.Append("          FROM  MRASRESDEF_BOH RAS" + "\n");
            }

            strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT * " + "\n");

            if (isToday)
            {
                strSqlString.Append("                  FROM MWIPLOTSTS LOT" + "\n");
            }
            else
            {
                strSqlString.Append("                  FROM RWIPLOTSTS_BOH LOT" + "\n");
            }

            strSqlString.Append("                 WHERE LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");

            if (!isToday)
            {
                strSqlString.Append("                   AND LOT.CUTOFF_DT = '" + selectDate[selectDate.Count - 1] + "22' " + "\n");
            }

            strSqlString.Append("                   AND LOT.LOT_TYPE = 'W' AND LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND LOT.LOT_CMF_5 LIKE 'P%'  " + "\n");
            strSqlString.Append(strCond1);
            strSqlString.Append("                   AND LOT.LOT_STATUS = 'PROC' " + "\n");
            strSqlString.Append("               ) LOT " + "\n");
            strSqlString.Append("         WHERE RAS.FACTORY   = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");

            if (!isToday)
            {
                strSqlString.Append("           AND RAS.CUTOFF_DT = '" + selectDate[selectDate.Count - 1] + "22' " + "\n");
            }

            strSqlString.Append("           AND RAS.RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("           AND RAS.RES_CMF_7 = 'Y' " + "\n");
            strSqlString.Append("           AND RAS.DELETE_FLAG  = ' ' " + "\n");
            strSqlString.Append("           AND RAS.RES_TYPE  = 'EQUIPMENT' " + "\n");
            strSqlString.Append(strCond2);
            strSqlString.Append("           AND RAS.FACTORY = LOT.FACTORY(+) " + "\n");
            strSqlString.Append("           AND RAS.RES_ID = LOT.START_RES_ID(+) " + "\n");
            strSqlString.Append("           AND RAS.FACTORY = MAT.FACTORY(+) " + "\n");
            strSqlString.Append("           AND RAS.RES_STS_2 = MAT.MAT_ID(+) " + "\n");
            strSqlString.Append("           AND RAS.RES_STS_2 LIKE '" + txtSearchProduct.Text + "'" + "\n");
            strSqlString.Append("           AND MAT.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("           AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND MAT.MAT_VER = 1 " + "\n");
            //strSqlString.Append("           AND RAS.RES_STS_1 NOT IN ('C200', 'B199')" + "\n");
            strSqlString.Append("           AND (RAS.RES_STS_1 NOT IN ('C200', 'B199') OR RAS.RES_UP_DOWN_FLAG = 'U')" + "\n");

            //상세조회
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);
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

            strSqlString.Append("         GROUP BY RAS.FACTORY,RAS. RES_STS_2,RAS.RES_GRP_6, RAS.RES_ID, RAS.RES_STS_8,MAT.MAT_GRP_1, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_CMF_11, RES_UP_DOWN_FLAG, RES_STS_1, LAST_DOWN_TIME " + "\n");
            strSqlString.Append("       ) RAS " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");

            if (strCustomer.Equals("-"))
            {
                strSqlString.Append("   AND RAS.MAT_GRP_1 IN ( SELECT KEY_1" + "\n");
                strSqlString.Append("                            FROM MGCMTBLDAT@RPTTOMES" + "\n");
                strSqlString.Append("                           WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                             AND TABLE_NAME = 'H_CUSTOMER'" + "\n");
                strSqlString.Append("                             AND DATA_1 LIKE '" + rowCustomStr + "' )" + "\n");
            }
            else strSqlString.Append("   AND RAS.MAT_GRP_1 LIKE '" + strCustomer + "'" + "\n");

            strSqlString.Append("   AND RAS.MAT_GRP_9 LIKE '" + rowMajorStr + "'" + "\n");
            strSqlString.Append("   AND RAS.MAT_GRP_10 LIKE '" + rowPKGStr + "'" + "\n");
            strSqlString.Append("   AND RAS.MAT_GRP_6 LIKE '" + rowLDStr + "'" + "\n");
            strSqlString.Append("   AND RAS.MAT_CMF_11 LIKE '" + rowPKGCDStr + "'" + "\n");
            strSqlString.Append(strCond3);
            
            if ("TOTAL" != strModelName)
                strSqlString.Append("   AND RAS.RES_MODEL = '" + strModelName + "' " + "\n");

            strSqlString.Append(" GROUP BY MAT_GROUP, MAT_ID, RAS.RES_MODEL, RES_ID, STATE, RES_STS_1, LAST_DOWN_TIME " + "\n");
            strSqlString.Append("HAVING " + strColumn + " = 1 " + "\n");
            strSqlString.Append(" ORDER BY MAT_GROUP, MAT_ID, RAS.RES_MODEL, RES_ID, STATE, RES_STS_1 " + "\n");


            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private void lblEqp_Click(object sender, EventArgs e)
        {
            if (pnlEqp.Visible == true) pnlEqp.Visible = false;
        }
    }
}
