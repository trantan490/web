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

namespace Hana.RAS
{
    /// <summary>
    /// 클  래  스: RAS020208<br/>
    /// 클래스요약: 가동률 설비별 상세<br/>
    /// 작  성  자: 미라콤 양형석<br/>
    /// 최초작성일: 2009-01-09<br/>
    /// 상세  설명: 가동률 설비별 상세<br/>
    /// 변경  내용: <br/>
    /// 
    /// 2009-09-22-임종우
    /// 월 선택 시 조업시간 : (금일 - 1)로 계산 되도록 변경.
    /// 변경이유 -> 2009-09-22일날 조회시(월초~금일까지) : Day선택시 2009-09-01~2009-09-21 이고, Month 선택시 당일 22일까지 계산되어
    /// Day(21일) 와 Month(22일) 조회시의 조업시간이 틀리게 계산됨.
    /// 
    /// 2009-11-30 임종우 : 공정별 데이터 조회 안되어 MRASRESMFO 테이블 이용 조회 가능하도록 수정 함.
    /// 2011-04-04 김민우 : $$$ 설비는 가동률 제외
    /// 2011-09-15-임종우 : 설비 공정간 맵핑 정보의 중복으로 인해 조업시간 오류 문제 수정.
    /// 2012-05-23-김민우 : GROUP 설정 시 RATIO 값 LOGIC 오류 수정 
    /// 2012-06-12-임종우 : MTBF, MTTC Total 값 평균으로 수정 (박남규 요청)
    /// 2012-07-18-임종우 : 쿼리 정리 작업... 열 맞추고 줄 맞추고...
    /// 2012-10-11-임종우 : MTBF 로직 변경 (김진석 요청)
    ///                     1. 조업시간 = 정지시간 이면 0
    ///                     2. (조업시간 - 정지시간) / 고장건수 : 고장건수가 0이면 MTBF는 조업시간, 고장건수가 1이면 +1을 하여 건수 2로 계산함.
    /// 2012-10-16-김민우 : RUN_DOWN 100% 초과하는 문제해결
    /// 2012-12-04-임종우 : TEST 전용 MTBF 추가 - 기존 고장건수 + H1XX, H2XX, H3XX, H4XX(품질 고장 일부) CODE 합을 고장 건수로 계산 (김만수 요청)
    /// 2013-02-05-임종우 : TEST MTBF 로직 변경 (박남규 요청) 
    ///                     2. (조업시간 - 정지시간) / 고장건수 : 고장건수가 0이면 MTBF는 조업시간 -> 가동시간으로 변경
    /// 2013-03-04-임종우 : 고장건수가 1이면 +1을 하여 건수 2로 계산함. -> 고장건수 1이이어도 그대로 계산함. (박남규 요청)
    /// 2013-08-23-임종우 : 가동시간이 Null(그때 당시 설비 use_state 가 N 인 경우) 이면 1440 -> 0 으로 변경 (김준용 요청)
    /// 2014-01-06-임종우 : 원부자재 제품 제외 (김상천 요청)
    /// 2014-05-29-임종우 : USE_STATE 에 따른 검색 기능 추가 (김준용K 요청)
    /// 2014-07-18-임종우 : 실적 환산 검색 기능 추가 (임태성K 요청)
    /// 2014-11-12-임종우 : SUB 설비 조회 기능 추가 (김준용K 요청)
    /// 2015-07-09-임종우 : Line Sub 설비 조회, Sub 설비 조회 분리 검색 기능 추가 (배진우C 요청)
    /// 2015-11-19-임종우 : 지정 설비 검색 기능 추가 (배진우C 요청)
    /// 2017-04-10-임종우 : SMT설비 원부자재 생산 실적도 표시되도록 수정 (박춘훈K 요청)
    /// 2017-04-21-임종우 : D126, D216, D316 코드 품종교체 데이터에서 제외 (이준엽D 요청)
    /// 2017-05-16-임종우 : Block 정보 추가 (배진우C 요청)
    /// 2018-09-05-임종우 : 금일은 전일 22시 부터 현재까지의 경과 시간으로 계산한다. (권순태이사 요청)
    /// 2020-02-07-김미경 : 설비 SERIAL NO, 자산구분, OLD/NEW 그룹 추가 (이창훈 D 요청)
    /// 2020-03-27-김미경 : Handler 정보 표현(Handler 명, Serial no, 자산 구분, Maker (이창훈 D 요청)
    /// 2020-06-03-이희석 : 삼성귀책시간 컬럼 표시,HMKT와 지정설비 체크일 경우(최연희 대리 요청)
    /// </summary>
    public partial class RAS020208 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        public RAS020208()
        {
            InitializeComponent();
            udcFromToDate.yesterday_flag = true;
            udcFromToDate.AutoBinding();

            cdvMonth.DaySelector.SelectedValue = "MONTH";
            cdvMonth.AutoBindingUserSetting(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
            cdvMonth.ToYearMonth.Visible = false;
            cdvMonth.DaySelector.Visible = false;

            SortInit();
            GridColumnInit();
        }

        #region " Function Definition "

        /// <summary 0. 삼성귀책 표시조건>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckFaults()
        {
            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)//Test일 경우
            {
                if (cdvCare.SelectedIndex == 1) //지정설비 체크
                {
                    return true;
                }
            }

            return false;
        }

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
            spdData.ActiveSheet.RowHeader.ColumnCount = 0;
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("Team in charge", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Part", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("EQP Type", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Block", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Maker", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Model", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Serial No.", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Asset classification", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("OLD/NEW", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Equipment name", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);

            for (int i = 0; i < 10; i++)
            {
                spdData.RPT_MerageHeaderRowSpan(0, i, 2);
            }

            // Handler
            spdData.RPT_AddBasicColumn("Handler", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Handler", 1, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Model", 1, 11, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Serial No.", 1, 12, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Asset classification", 1, 13, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_MerageHeaderColumnSpan(0, 10, 4);

            spdData.RPT_AddBasicColumn("Synthesis", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
            spdData.RPT_AddBasicColumn("Performance", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
            spdData.RPT_AddBasicColumn("time", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
            spdData.RPT_AddBasicColumn("Automation utilization", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
            spdData.RPT_AddBasicColumn("load", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
            spdData.RPT_AddBasicColumn("Operating hours", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("value operation", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("Uptime", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("Automation uptime", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("Stop time", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("Production performance", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

            spdData.RPT_AddBasicColumn("MTBF", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("MTTC", 0, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

            for (int i = 14; i < 27; i++)
            {
                spdData.RPT_MerageHeaderRowSpan(0, i, 2);
            }

            spdData.RPT_AddBasicColumn("Ratio", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
            spdData.RPT_AddBasicColumn("breakdown", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
            spdData.RPT_AddBasicColumn("Run Down", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
            spdData.RPT_AddBasicColumn("Preventive Check", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
            spdData.RPT_AddBasicColumn("Replacement of product", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
            spdData.RPT_AddBasicColumn("ER Down", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
            spdData.RPT_AddBasicColumn("Super Down", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
            spdData.RPT_AddBasicColumn("UT Down", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
            spdData.RPT_AddBasicColumn("Quality failure", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
            spdData.RPT_MerageHeaderColumnSpan(0, 27, 8);

            spdData.RPT_AddBasicColumn("time", 0, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("breakdown", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("Run Down", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("Preventive Check", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("Replacement of product", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("ER Down", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("Super Down", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("UT Down", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("Quality failure", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            

            //2020-05-28-이희석 : 삼성귀책 컬럼추가
            int timecol_count = 0;//span값 변동
            if (CheckFaults() == true)
            {
                spdData.RPT_AddBasicColumn("SEC accountability(UP std.)", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                timecol_count++;
            }
            spdData.RPT_MerageHeaderColumnSpan(0, 35, 8 + timecol_count);
            spdData.RPT_AddBasicColumn("number of cases", 0, 43 + timecol_count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
            spdData.RPT_AddBasicColumn("breakdown", 1, 43 + timecol_count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("Run Down", 1, 44 + timecol_count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("Preventive Check", 1, 45 + timecol_count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("Replacement of product", 1, 46 + timecol_count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("ER Down", 1, 47 + timecol_count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("Super Down", 1, 48 + timecol_count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("UT Down", 1, 49 + timecol_count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("Quality failure", 1, 50 + timecol_count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_MerageHeaderColumnSpan(0, 43 + timecol_count, 8);

            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                spdData.RPT_AddBasicColumn("MTBF(TEST)", 0, 51 + timecol_count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            }
            else
            {
                spdData.RPT_AddBasicColumn("MTBF(TEST)", 0, 51 + timecol_count, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            }

            spdData.RPT_MerageHeaderRowSpan(0, 51 + timecol_count, 2);

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.            
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Team in charge", "TEAM", "RES.RES_GRP_1", "RES.RES_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Part", "PART", "RES.RES_GRP_2", "RES.RES_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("EQP Type", "EQP_TYPE", "RES_GRP_3", "RES_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Block", "SUB_AREA_ID", "RES.SUB_AREA_ID", "SUB_AREA_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Resource Maker", "MAKER", "RES.RES_GRP_5", "RES.RES_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Resource Model", "MODEL", "RES.RES_GRP_6", "RES.RES_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Resource S/N", "SERIAL", "RES.RES_CMF_1", "RES.RES_CMF_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Resource Asset classification", "ASSET_TYPE", "RES.RES_CMF_4", "RES.RES_CMF_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Resource OLD/NEW", "OLD_NEW", "RES.RES_CMF_19", "RES.RES_CMF_19", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment name", "RES_ID", "RES.RES_ID", "RES.RES_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Handler", "H_HDR_ID", "HDR.HDR_ID", "HDR.HDR_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Handler Model", "H_MODEL", "HDR.MODEL", "HDR.MODEL", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Handler S/N", "H_SERIAL", "HDR.SN", "HDR.SN", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Handler Asset classification", "H_ASSET_TYPE", "HDR.OWNER", "HDR.OWNER", false);
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        /// 

        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();
                        
            DateTime dtTo = DateTime.MinValue;
            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
                        
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            string strFromDate = udcFromToDate.ExactFromDate;
            string strToDate = udcFromToDate.ExactToDate;
            string strFromDay = udcFromToDate.HmFromDay;
            string strToDay = udcFromToDate.HmToDay;
            string JoupTime = GetJoupTime(strFromDate, strToDate);

            //if (cdvEquipType.Text != "Line 구성 설비")    
            if (cdvEquipType.SelectedIndex != 2)          
            {
                #region 1.기본
                strSqlString.AppendFormat("SELECT " + QueryCond1 + "\n");
                strSqlString.AppendFormat("     , CASE WHEN NVL(SUM(VALUE_RUN_TIME), 0) = 0 THEN 0 " + "\n");
                strSqlString.AppendFormat("            WHEN (NVL(SUM(RUN_TIME), 1440) - (NVL(SUM(TIME_SUM), 0) / 60)) = 0 THEN 0 " + "\n");
                strSqlString.AppendFormat("            ELSE ROUND(((NVL(SUM(RUN_TIME), 1440) / DECODE(SUM(TOTAL_RUN_TIME), 0, 1440, SUM(TOTAL_RUN_TIME))) * 100) * (NVL(ROUND((SUM(VALUE_RUN_TIME) / DECODE(SUM(RUN_TIME), 0, 1440, SUM(RUN_TIME))) * 100, 2), 0)) / 100, 2) " + "\n");
                strSqlString.AppendFormat("       END AS TOTAL_PERCENT " + "\n");
                strSqlString.AppendFormat("     , CASE WHEN NVL(SUM(VALUE_RUN_TIME), 0) = 0 THEN 0 " + "\n");
                strSqlString.AppendFormat("            WHEN NVL(SUM(RUN_TIME),0) = 0 THEN 0 " + "\n");
                strSqlString.AppendFormat("            ELSE NVL(ROUND((SUM(VALUE_RUN_TIME) / DECODE(SUM(RUN_TIME), 0, 1440, SUM(RUN_TIME))) * 100, 2), 0) " + "\n");
                strSqlString.AppendFormat("       END AS ABILITY_PERCENT " + "\n");
                strSqlString.AppendFormat("     , ROUND((NVL(SUM(RUN_TIME), 1440) / NVL(SUM(TOTAL_RUN_TIME), 1440)) * 100, 2) AS TIME_PERCENT " + "\n");
                strSqlString.AppendFormat("     , ROUND((NVL(SUM(REALTIME), 1440) / NVL(SUM(TOTAL_RUN_TIME), 1440)) * 100, 2) AS AUTO_TIME_PERCENT " + "\n");
                strSqlString.AppendFormat("     , ROUND((NVL(SUM(TOTAL_RUN_TIME), 1440) - SUM(DOWN_TIME_RUNDOWN)) / NVL(SUM(TOTAL_RUN_TIME), 1440) * 100, 2) AS LOSS_PERCENT " + "\n");
                strSqlString.AppendFormat("     , SUM(TOTAL_RUN_TIME) AS TOTAL_RUN_TIME " + "\n");
                strSqlString.AppendFormat("     , SUM(VALUE_RUN_TIME) AS VALUE_RUN_TIME" + "\n");
                strSqlString.AppendFormat("     , SUM(RUN_TIME) AS RUN_TIME" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(REALTIME), 2) AS AUTO_REALTIME" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_TIME) AS DOWN_TIME" + "\n");
                strSqlString.AppendFormat("     , SUM(QTY) AS QTY" + "\n");

                // 2012-10-11-임종우 : MTBF 로직 변경 함.
                //strSqlString.AppendFormat("     , DECODE(SUM(MTBF),0," + JoupTime + ", SUM(MTBF)) AS MTBF" + "\n");
                strSqlString.AppendFormat("     , ROUND(CASE WHEN SUM(TOTAL_RUN_TIME) = SUM(DOWN_TIME) THEN 0 " + "\n");
                strSqlString.AppendFormat("                  WHEN NVL(SUM(RUN_TIME),0) = 0 THEN 0  " + "\n");
                strSqlString.AppendFormat("                  WHEN SUM(DOWN_CNT_BM) = 0 THEN SUM(TOTAL_RUN_TIME) " + "\n");
                strSqlString.AppendFormat("                  WHEN SUM(DOWN_CNT_BM) = 1 THEN (SUM(TOTAL_RUN_TIME) - SUM(DOWN_TIME)) / 2 " + "\n");
                strSqlString.AppendFormat("                  ELSE (SUM(TOTAL_RUN_TIME) - SUM(DOWN_TIME)) / SUM(DOWN_CNT_BM) " + "\n");
                strSqlString.AppendFormat("             END, 0) AS MTBF " + "\n");

                strSqlString.AppendFormat("     , SUM(MTTC) AS MTTC" + "\n");

                // 2012-05-23-김민우- 최종 GROUP BY 전에 계산 하여 GROUP BY 값이 안 맞은 오류 수정
                strSqlString.AppendFormat("     , ROUND(SUM(RAITO_BM) / SUM(RAITO_RUN_TIME)*100,2) AS RAITO_BM" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(RAITO_RUNDOWN) / SUM(RAITO_RUN_TIME)*100,2) AS RAITO_BM" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(RAITO_PM) / SUM(RAITO_RUN_TIME)*100,2) AS RAITO_BM" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(RAITO_DEVCHG) / SUM(RAITO_RUN_TIME)*100,2) AS RAITO_BM" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(RAITO_ENGDOWN) / SUM(RAITO_RUN_TIME)*100,2) AS RAITO_BM" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(RAITO_SUPERDOWN) / SUM(RAITO_RUN_TIME)*100,2) AS RAITO_BM" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(RAITO_UTILDOWN) / SUM(RAITO_RUN_TIME)*100,2) AS RAITO_BM" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(RAITO_ETC) / SUM(RAITO_RUN_TIME)*100,2) AS RAITO_BM" + "\n");

                strSqlString.AppendFormat("     , SUM(DOWN_TIME_BM)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_TIME_RUNDOWN)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_TIME_PM)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_TIME_DEVCHG)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_TIME_ENGDOWN)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_TIME_SUPERDOWN)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_TIME_UTILDOWN)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_TIME_ETC)" + "\n");
                if (CheckFaults() == true)
                {
                    strSqlString.AppendFormat("     , SUM(DOWN_TIME_FAULTS_SEC)" + "\n");
                }
                strSqlString.AppendFormat("     , SUM(DOWN_CNT_BM)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_CNT_RUNDOWN)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_CNT_PM)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_CNT_DEVCHG)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_CNT_ENGDOWN)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_CNT_SUPERDOWN)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_CNT_UTILDOWN)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_CNT_ETC)" + "\n");

                // 2012-12-04-임종우 : TEST 전용 MTBF 추가 (김만수 요청)
                strSqlString.AppendFormat("     , ROUND(CASE WHEN SUM(TOTAL_RUN_TIME) = SUM(DOWN_TIME) THEN 0 " + "\n");
                strSqlString.AppendFormat("                  WHEN SUM(DOWN_CNT_H) = 0 THEN SUM(RUN_TIME) " + "\n");
                //strSqlString.AppendFormat("                  WHEN SUM(DOWN_CNT_H) = 1 THEN (SUM(TOTAL_RUN_TIME) - SUM(DOWN_TIME)) / 2 " + "\n");
                strSqlString.AppendFormat("                  ELSE (SUM(TOTAL_RUN_TIME) - SUM(DOWN_TIME)) / SUM(DOWN_CNT_H) " + "\n");
                strSqlString.AppendFormat("             END, 0) AS MTBF_TEST " + "\n");


                strSqlString.AppendFormat("  FROM (" + "\n");
                strSqlString.AppendFormat("        SELECT NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_1), '-') AS TEAM " + "\n");
                strSqlString.AppendFormat("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_2), '-') AS PART" + "\n");
                strSqlString.AppendFormat("             , RES.RES_GRP_3 AS EQP_TYPE, RES.RES_GRP_5 AS MAKER, RES.RES_GRP_6 AS MODEL, RES.RES_ID AS RES_ID, RES.SUB_AREA_ID" + "\n");
                strSqlString.AppendFormat("             , RES_CMF_1 AS SERIAL, RES_CMF_4 AS ASSET_TYPE, RES_CMF_19 AS OLD_NEW" + "\n");
                strSqlString.AppendFormat("             , CASE WHEN SUM(NVL(MOV.VALUE_RUN_TIME,0)) = 0 THEN 0" + "\n");
                strSqlString.AppendFormat("                    WHEN (SUM(NVL(MOV.RUN_TIME," + JoupTime + ")) - (SUM(NVL(DWH.TIME_SUM,0)) / 60)) = 0 THEN 0" + "\n");
                strSqlString.AppendFormat("                    ELSE ROUND((ROUND((SUM(MOV.VALUE_RUN_TIME) / (SUM(NVL(MOV.RUN_TIME," + JoupTime + ")) - NVL((SUM(DWH.TIME_SUM)/60),0)))*100,2) * ROUND(((SUM(NVL(MOV.RUN_TIME," + JoupTime + ")) - (SUM(NVL(DWH.TIME_SUM,0))/60)) / SUM(NVL(MOV.RUN_TIME," + JoupTime + ")))*100,2))/100,2)" + "\n");
                strSqlString.AppendFormat("               END  AS TOTAL_PERCENT" + "\n");
                strSqlString.AppendFormat("             , CASE WHEN SUM(NVL(MOV.VALUE_RUN_TIME,0)) = 0 THEN 0" + "\n");
                strSqlString.AppendFormat("                    WHEN SUM(NVL(MOV.RUN_TIME," + JoupTime + ")) - (SUM(NVL(DWH.TIME_SUM,0))/60) = 0 THEN 0" + "\n");
                strSqlString.AppendFormat("                    ELSE NVL(ROUND((SUM(MOV.VALUE_RUN_TIME) / (SUM(NVL(MOV.RUN_TIME," + JoupTime + ")) - NVL((SUM(DWH.TIME_SUM)/60),0)))*100,2),0)" + "\n");
                strSqlString.AppendFormat("               END  AS ABILITY_PERCENT" + "\n");
                strSqlString.AppendFormat("             , ROUND(((SUM(NVL(MOV.RUN_TIME," + JoupTime + ")) - NVL((SUM(TIME_SUM)/60),0)) / SUM(NVL(MOV.RUN_TIME," + JoupTime + ")))*100,2) AS TIME_PERCENT" + "\n");
                strSqlString.AppendFormat("             , DECODE(SUM(DWH.DOWN_TIME_RUNDOWN),0,100, ROUND((((SUM(NVL(MOV.RUN_TIME," + JoupTime + ")) - SUM(DWH.DOWN_TIME_RUNDOWN)))/SUM(NVL(MOV.RUN_TIME," + JoupTime + "))*100),2)) AS LOSS_PERCENT" + "\n");
                //strSqlString.AppendFormat("             , DECODE(SUM(NVL(MOV.RUN_TIME," + JoupTime + ")),'', " + JoupTime + ", SUM(NVL(MOV.RUN_TIME," + JoupTime + "))) AS TOTAL_RUN_TIME" + "\n");
                strSqlString.AppendFormat("             , DECODE(NVL(MOV.RUN_TIME," + JoupTime + "),'', " + JoupTime + ", NVL(MOV.RUN_TIME," + JoupTime + ")) AS TOTAL_RUN_TIME" + "\n");
                strSqlString.AppendFormat("             , NVL(SUM(MOV.VALUE_RUN_TIME),0) AS VALUE_RUN_TIME" + "\n");
                strSqlString.AppendFormat("             , CASE WHEN SUM(NVL(MOV.RUN_TIME,0)) = 0 THEN 0" + "\n");
                strSqlString.AppendFormat("                    ELSE ROUND(SUM(NVL(MOV.RUN_TIME,0)) - NVL(ROUND((SUM(DWH.TIME_SUM)/60),2),0),2)" + "\n");
                strSqlString.AppendFormat("               END AS RUN_TIME" + "\n");
                strSqlString.AppendFormat("             , NVL(ROUND((SUM(DWH.TIME_SUM)/60),2),0) AS DOWN_TIME" + "\n");
                strSqlString.AppendFormat("             , NVL(SUM(MOV.QTY),0) AS QTY" + "\n");
                strSqlString.AppendFormat("             , DECODE(SUM(NVL(DWH.DOWN_CNT_BM,0)),0,0,ROUND((SUM(NVL(MOV.RUN_TIME," + JoupTime + ")) - (SUM(DWH.TIME_SUM)/60))/(SUM(DWH.DOWN_CNT_BM)+SUM(DWH.DOWN_CNT_PM) + SUM(DWH.DOWN_CNT_ETC)),2)) AS MTBF" + "\n");
                strSqlString.AppendFormat("             , DECODE(SUM(NVL(DWH.DOWN_CNT_DEVCHG,0)),0,0,ROUND((SUM(DWH.DOWN_TIME_DEVCHG)) / SUM(DWH.DOWN_CNT_DEVCHG),2)) AS MTTC" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.RAITO_BM,0)) AS RAITO_BM" + "\n");
                //2012-10-16-김민우 : RUN_DOWN 100% 초과하는 문제해결 
                //strSqlString.AppendFormat("             , SUM(NVL(MOV.RUN_TIME,1440)) AS RAITO_RUN_TIME" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(MOV.RUN_TIME," + JoupTime + ")) AS RAITO_RUN_TIME" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.RAITO_RUNDOWN,0)) AS RAITO_RUNDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.RAITO_PM,0)) AS RAITO_PM" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.RAITO_DEVCHG,0)) AS RAITO_DEVCHG" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.RAITO_ENGDOWN,0)) AS RAITO_ENGDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.RAITO_SUPERDOWN,0)) AS RAITO_SUPERDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.RAITO_UTILDOWN,0)) AS RAITO_UTILDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.RAITO_ETC,0)) AS RAITO_ETC" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_TIME_BM,0)) AS DOWN_TIME_BM" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_TIME_RUNDOWN,0)) AS DOWN_TIME_RUNDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_TIME_PM,0)) AS DOWN_TIME_PM" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_TIME_DEVCHG,0)) AS DOWN_TIME_DEVCHG" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_TIME_ENGDOWN,0)) AS DOWN_TIME_ENGDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_TIME_SUPERDOWN,0)) AS DOWN_TIME_SUPERDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_TIME_UTILDOWN,0)) AS DOWN_TIME_UTILDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_TIME_ETC,0)) AS DOWN_TIME_ETC" + "\n");
                if (CheckFaults() == true)
                {
                    strSqlString.AppendFormat("         , SUM(NVL(DWH.DOWN_TIME_FAULTS_SEC,0)) AS DOWN_TIME_FAULTS_SEC" + "\n");
                }
                strSqlString.AppendFormat("             , SUM(NVL(TIME_SUM,0)) AS TIME_SUM " + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_CNT_BM,0)) AS DOWN_CNT_BM" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_CNT_RUNDOWN,0)) AS DOWN_CNT_RUNDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_CNT_PM,0)) AS DOWN_CNT_PM" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_CNT_DEVCHG,0)) AS DOWN_CNT_DEVCHG" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_CNT_ENGDOWN,0)) AS DOWN_CNT_ENGDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_CNT_SUPERDOWN,0)) AS DOWN_CNT_SUPERDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_CNT_UTILDOWN,0)) AS DOWN_CNT_UTILDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_CNT_ETC,0)) AS DOWN_CNT_ETC" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_CNT_H,0)) + SUM(NVL(DWH.DOWN_CNT_BM,0)) AS DOWN_CNT_H" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(RAL.RUN_TIME/60,0)) AS REALTIME" + "\n");
                strSqlString.AppendFormat("             , HDR.HDR_ID AS H_HDR_ID" + "\n");
                strSqlString.AppendFormat("             , HDR.MODEL AS H_MODEL" + "\n");
                strSqlString.AppendFormat("             , HDR.SN AS H_SERIAL" + "\n");
                strSqlString.AppendFormat("             , HDR.OWNER AS H_ASSET_TYPE" + "\n");
                strSqlString.AppendFormat("          FROM (" + "\n");
                strSqlString.AppendFormat("                SELECT FACTORY,RES_ID" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'BM',TIME_SUM/60, 0)) AS RAITO_BM" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'RUNDOWN', TIME_SUM/60, 0)) AS RAITO_RUNDOWN" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'PM',TIME_SUM/60, 0)) AS RAITO_PM" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'DEVCHG', TIME_SUM/60, 0)) AS RAITO_DEVCHG" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'ENGDOWN', TIME_SUM/60, 0)) AS RAITO_ENGDOWN" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'SUPERDOWN', TIME_SUM/60, 0)) AS RAITO_SUPERDOWN" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'UTILDOWN', TIME_SUM/60, 0)) AS RAITO_UTILDOWN" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'ETC', TIME_SUM/60, 0)) AS RAITO_ETC" + "\n");
                strSqlString.AppendFormat("                     , ROUND(SUM(DECODE(DOWN_EVENT_ID, 'BM', TIME_SUM/60, 0)), 2) AS DOWN_TIME_BM" + "\n");
                strSqlString.AppendFormat("                     , ROUND(SUM(DECODE(DOWN_EVENT_ID, 'RUNDOWN', TIME_SUM/60, 0)), 2) AS DOWN_TIME_RUNDOWN" + "\n");
                strSqlString.AppendFormat("                     , ROUND(SUM(DECODE(DOWN_EVENT_ID, 'PM', TIME_SUM/60, 0)), 2) AS DOWN_TIME_PM" + "\n");
                strSqlString.AppendFormat("                     , ROUND(SUM(DECODE(DOWN_EVENT_ID, 'DEVCHG', TIME_SUM/60, 0)), 2) AS DOWN_TIME_DEVCHG" + "\n");
                strSqlString.AppendFormat("                     , ROUND(SUM(DECODE(DOWN_EVENT_ID, 'ENGDOWN', TIME_SUM/60, 0)), 2) AS DOWN_TIME_ENGDOWN" + "\n");
                strSqlString.AppendFormat("                     , ROUND(SUM(DECODE(DOWN_EVENT_ID, 'SUPERDOWN', TIME_SUM/60, 0)), 2) AS DOWN_TIME_SUPERDOWN" + "\n");
                strSqlString.AppendFormat("                     , ROUND(SUM(DECODE(DOWN_EVENT_ID, 'UTILDOWN', TIME_SUM/60, 0)), 2) AS DOWN_TIME_UTILDOWN" + "\n");
                strSqlString.AppendFormat("                     , ROUND(SUM(DECODE(DOWN_EVENT_ID, 'ETC', TIME_SUM/60, 0)), 2) AS DOWN_TIME_ETC" + "\n");
                if (CheckFaults() == true)
                {
                    strSqlString.AppendFormat("                     ,ROUND(SUM(DECODE(DOWN_SEC, 'DOWN_SEC', TIME_SUM/60, 0)), 2) AS DOWN_TIME_FAULTS_SEC" + "\n");
                }
                strSqlString.AppendFormat("                     , SUM(TIME_SUM) AS TIME_SUM" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'BM', DOWN_CNT, 0)) AS DOWN_CNT_BM" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'RUNDOWN', DOWN_CNT, 0)) AS DOWN_CNT_RUNDOWN" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'PM', DOWN_CNT, 0)) AS DOWN_CNT_PM" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'DEVCHG', DOWN_CNT, 0)) AS DOWN_CNT_DEVCHG" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'ENGDOWN', DOWN_CNT, 0)) AS DOWN_CNT_ENGDOWN" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'SUPERDOWN', DOWN_CNT, 0)) AS DOWN_CNT_SUPERDOWN" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'UTILDOWN', DOWN_CNT, 0)) AS DOWN_CNT_UTILDOWN" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'ETC', DOWN_CNT, 0)) AS DOWN_CNT_ETC" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(H_CODE, 'H', DOWN_CNT, 0)) AS DOWN_CNT_H" + "\n");
                strSqlString.AppendFormat("                  FROM (" + "\n");
                strSqlString.AppendFormat("                        SELECT MNT.FACTORY,MNT.RES_ID, MNT.MAINT_CODE" + "\n");
                strSqlString.AppendFormat("                             , CASE WHEN SUBSTR(MAINT_CODE,0,2) IN ('H1','H2','H3','H4') THEN 'H' END AS H_CODE" + "\n");
                if (CheckFaults() == true)
                {
                    strSqlString.AppendFormat("                             , CASE WHEN MAINT_CODE IN (select KEY_1 from MGCMTBLDAT where table_name='H_SEC_FAULT_CODE') THEN 'DOWN_SEC' END AS DOWN_SEC" + "\n");
                }
                strSqlString.AppendFormat("                             , CASE WHEN SUBSTR(MAINT_CODE,0,1)='A' THEN 'BM'" + "\n");
                strSqlString.AppendFormat("                                    WHEN SUBSTR(MAINT_CODE,0,1)='B' THEN 'RUNDOWN'" + "\n");
                strSqlString.AppendFormat("                                    WHEN SUBSTR(MAINT_CODE,0,1)='C' THEN 'PM'" + "\n");
                strSqlString.AppendFormat("                                    WHEN SUBSTR(MAINT_CODE,0,1)='D' THEN 'DEVCHG'" + "\n");
                strSqlString.AppendFormat("                                    WHEN SUBSTR(MAINT_CODE,0,1)='E' THEN 'ENGDOWN'" + "\n");
                strSqlString.AppendFormat("                                    WHEN SUBSTR(MAINT_CODE,0,1)='F' THEN 'SUPERDOWN'" + "\n");
                strSqlString.AppendFormat("                                    WHEN SUBSTR(MAINT_CODE,0,1)='G' THEN 'UTILDOWN'" + "\n");
                strSqlString.AppendFormat("                                    WHEN SUBSTR(MAINT_CODE,0,1)='H' THEN 'ETC'" + "\n");
                strSqlString.AppendFormat("                               END AS DOWN_EVENT_ID,NVL(SUM(TIME_SUM),0) AS TIME_SUM" + "\n");
                strSqlString.AppendFormat("                             , DECODE(SUBSTR(MAINT_CODE,0,1) " + "\n");
                strSqlString.AppendFormat("                                      , 'B', NVL(DECODE(MAINT_STEP,'D_WAIT',SUM(FREQUENCY)),0)" + "\n");
                strSqlString.AppendFormat("                                      , 'D', NVL(DECODE(MAINT_STEP,'D_WAIT',SUM(FREQUENCY)),0) " + "\n");
                strSqlString.AppendFormat("                                      , 'E', NVL(DECODE(MAINT_STEP,'D_WAIT',SUM(FREQUENCY)),0) " + "\n");
                strSqlString.AppendFormat("                                      , 'F', NVL(DECODE(MAINT_STEP,'D_WAIT',SUM(FREQUENCY)),0)" + "\n");
                strSqlString.AppendFormat("                                      , 'G', NVL(DECODE(MAINT_STEP,'D_WAIT',SUM(FREQUENCY)),0)" + "\n");
                strSqlString.AppendFormat("                                      ,  NVL(DECODE(MAINT_STEP,'D_MAINT',SUM(FREQUENCY)),0)" + "\n");
                strSqlString.AppendFormat("                               ) AS DOWN_CNT" + "\n");
                strSqlString.AppendFormat("                          FROM CSUMRESMNT@RPTTOMES MNT" + "\n");
                strSqlString.AppendFormat("                             , MRASRESDEF RES" + "\n");
                strSqlString.AppendFormat("                         WHERE 1=1" + "\n");
                strSqlString.AppendFormat("                           AND MNT.FACTORY = RES.FACTORY" + "\n");
                strSqlString.AppendFormat("                           AND MNT.RES_ID = RES.RES_ID" + "\n");
                strSqlString.AppendFormat("                           AND RES.RES_CMF_20 < '" + strFromDate + "'" + "\n");
                strSqlString.AppendFormat("                           AND MNT.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.AppendFormat("                           AND MNT.WORK_DATE BETWEEN '{0}' AND '{1}' " + "\n", strFromDay, strToDay);
                strSqlString.AppendFormat("                           AND MNT.MAINT_CODE NOT IN ('D126','D216','D316')" + "\n");
                strSqlString.AppendFormat("                         GROUP BY MNT.FACTORY, MNT.RES_ID, MNT.MAINT_CODE, MNT.MAINT_STEP" + "\n");
                strSqlString.AppendFormat("                       ) RES" + "\n");
                strSqlString.AppendFormat("                 GROUP BY FACTORY, RES_ID" + "\n");
                strSqlString.AppendFormat("                 ORDER BY RES_ID" + "\n");
                strSqlString.AppendFormat("               ) DWH" + "\n");
                strSqlString.AppendFormat("             , (" + "\n");
                strSqlString.AppendFormat("                SELECT RES_ID,SUM(RUN_TIME) AS RUN_TIME " + "\n");
                strSqlString.AppendFormat("                  FROM CSUMRASTIM@RPTTOMES" + "\n");
                strSqlString.AppendFormat("                 WHERE 1 = 1" + "\n");
                strSqlString.AppendFormat("                   AND WORK_DATE BETWEEN '" + strFromDay + "' AND '" + strToDay + "'" + "\n");
                strSqlString.AppendFormat("                 GROUP BY RES_ID " + "\n");
                strSqlString.AppendFormat("               ) RAL" + "\n");
                strSqlString.AppendFormat("             , (" + "\n");
                strSqlString.AppendFormat("                SELECT MOV.FACTORY" + "\n");
                strSqlString.AppendFormat("                     , MOV.RES_ID" + "\n");                
                strSqlString.AppendFormat("                     , SUM(LOSS_QTY) AS LOSS_QTY" + "\n");
                strSqlString.AppendFormat("                     , " + JoupTime + " AS RUN_TIME" + "\n");

                // 2019-04-19-임종우 : 과거 UPEH를 기준으로 가치 가동 시간 구하기 (배진우부장 요청)
                if (ckbUPEH.Checked == true)
                {
                    strSqlString.AppendFormat("                     , CASE WHEN ROUND(SUM((MOV.QTY / UPH.UPEH) * 60),3) IS NULL THEN SUM(VALUE_RUN_TIME)" + "\n");
                    strSqlString.AppendFormat("                            ELSE ROUND(SUM((MOV.QTY / UPH.UPEH) * 60),3)" + "\n");
                    strSqlString.AppendFormat("                       END AS VALUE_RUN_TIME" + "\n");
                }
                else
                {
                    strSqlString.AppendFormat("                     , SUM(VALUE_RUN_TIME) AS VALUE_RUN_TIME" + "\n");
                }

                // 2014-07-18-임종우 : 실적 환산 수량으로 구하기
                if (ckbConv.Checked == true)
                {
                    strSqlString.AppendFormat("                     , ROUND(SUM(CASE WHEN MOV.OPER LIKE 'A06%' THEN (QTY + LOSS_QTY) * NVL(WIRE_CNT, DECODE(MAT_GRP_6,'-',1,'0',1,MAT_GRP_6))" + "\n");
                    strSqlString.AppendFormat("                                      WHEN MOV.OPER = 'A1300' THEN (QTY + LOSS_QTY) * DECODE(MAT_GRP_6,'-',1,'0',1,NULL,1,MAT_GRP_6)" + "\n");
                    strSqlString.AppendFormat("                                      WHEN MOV.OPER IN ('A0020', 'A0030', 'A0040', 'A0200', 'A0230') THEN (QTY + LOSS_QTY) / (TO_NUMBER(DECODE(MAT_CMF_13,' ',1,'-',1,'0',1,MAT_CMF_13)) * 0.85)" + "\n");
                    strSqlString.AppendFormat("                                      ELSE (QTY + LOSS_QTY)" + "\n");
                    strSqlString.AppendFormat("                                 END),0) AS QTY" + "\n");
                    strSqlString.AppendFormat("                  FROM CSUMRASMOV@RPTTOMES MOV" + "\n");
                    strSqlString.AppendFormat("                     , MWIPMATDEF MAT " + "\n");
                    strSqlString.AppendFormat("                     , ( " + "\n");
                    strSqlString.AppendFormat("                        SELECT MAT_ID, OPER, TO_NUMBER(TCD_CMF_2) AS WIRE_CNT " + "\n");
                    strSqlString.AppendFormat("                          FROM CWIPTCDDEF@RPTTOMES " + "\n");
                    strSqlString.AppendFormat("                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.AppendFormat("                           AND SET_FLAG = 'Y' " + "\n");
                    strSqlString.AppendFormat("                           AND TCD_CMF_2 <> ' ' " + "\n");
                    strSqlString.AppendFormat("                       ) WIR" + "\n");

                    if (ckbUPEH.Checked == true)
                    {
                        strSqlString.AppendFormat("                     , CRASUPHDEF_BOH UPH " + "\n");
                        strSqlString.AppendFormat("                 WHERE 1=1" + "\n");
                        strSqlString.AppendFormat("                   AND MOV.MAT_ID = WIR.MAT_ID(+)" + "\n");
                        strSqlString.AppendFormat("                   AND MOV.OPER = WIR.OPER(+)" + "\n");
                        strSqlString.AppendFormat("                   AND MOV.FACTORY = UPH.FACTORY(+)" + "\n");
                        strSqlString.AppendFormat("                   AND MOV.OPER = UPH.OPER(+)" + "\n");
                        strSqlString.AppendFormat("                   AND MOV.RESV_FIELD_9 = UPH.RES_MODEL(+)" + "\n");
                        strSqlString.AppendFormat("                   AND MOV.RESV_FIELD_10 = UPH.UPEH_GRP(+)" + "\n");
                        strSqlString.AppendFormat("                   AND MOV.MAT_ID = UPH.MAT_ID(+)" + "\n");
                        strSqlString.AppendFormat("                   AND UPH.WORK_MONTH(+) = '" + cdvMonth.FromYearMonth.Value.ToString("yyyyMM") + "'" + "\n");
                    }
                    else
                    {
                        strSqlString.AppendFormat("                 WHERE 1=1" + "\n");
                        strSqlString.AppendFormat("                   AND MOV.MAT_ID = WIR.MAT_ID(+)" + "\n");
                        strSqlString.AppendFormat("                   AND MOV.OPER = WIR.OPER(+)" + "\n");
                    }
                }
                else
                {
                    strSqlString.AppendFormat("                     , SUM(QTY + LOSS_QTY) AS QTY" + "\n");
                    strSqlString.AppendFormat("                  FROM CSUMRASMOV@RPTTOMES MOV" + "\n");
                    strSqlString.AppendFormat("                     , MWIPMATDEF MAT " + "\n");

                    if (ckbUPEH.Checked == true)
                    {
                        strSqlString.AppendFormat("                     , CRASUPHDEF_BOH UPH " + "\n");
                        strSqlString.AppendFormat("                 WHERE 1=1" + "\n");
                        strSqlString.AppendFormat("                   AND MOV.FACTORY = UPH.FACTORY(+)" + "\n");
                        strSqlString.AppendFormat("                   AND MOV.OPER = UPH.OPER(+)" + "\n");
                        strSqlString.AppendFormat("                   AND MOV.RESV_FIELD_9 = UPH.RES_MODEL(+)" + "\n");
                        strSqlString.AppendFormat("                   AND MOV.RESV_FIELD_10 = UPH.UPEH_GRP(+)" + "\n");
                        strSqlString.AppendFormat("                   AND MOV.MAT_ID = UPH.MAT_ID(+)" + "\n");
                        strSqlString.AppendFormat("                   AND UPH.WORK_MONTH(+) = '" + cdvMonth.FromYearMonth.Value.ToString("yyyyMM") + "'" + "\n");
                    }
                    else
                    {
                        strSqlString.AppendFormat("                 WHERE 1=1" + "\n");
                    }
                }

                strSqlString.AppendFormat("                   AND MOV.FACTORY = MAT.FACTORY " + "\n");
                strSqlString.AppendFormat("                   AND MOV.MAT_ID = MAT.MAT_ID " + "\n");
                strSqlString.AppendFormat("                   AND MOV.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.AppendFormat("                   AND MOV.WORK_DAY BETWEEN '" + strFromDay + "' AND '" + strToDay + "'" + "\n");
                strSqlString.AppendFormat("                   AND MOV.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                //strSqlString.AppendFormat("                   AND MAT.MAT_TYPE = 'FG' " + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (cdvFactory.txtValue == "HMKB1")
                {
                    if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                        strSqlString.AppendFormat("            AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                    if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                        strSqlString.AppendFormat("            AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                    if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                        strSqlString.AppendFormat("            AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                    if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                        strSqlString.AppendFormat("            AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                    if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                        strSqlString.AppendFormat("            AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                    if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                        strSqlString.AppendFormat("            AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                    if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                        strSqlString.AppendFormat("            AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                    if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                    if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                    if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                    if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                    if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                }
                else
                {
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                }
                #endregion

                strSqlString.AppendFormat("                 GROUP BY MOV.FACTORY,MOV.RES_ID" + "\n");
                strSqlString.AppendFormat("               ) MOV" + "\n");
                strSqlString.AppendFormat("             , MRASRESDEF RES" + "\n");
                strSqlString.AppendFormat("             , CRASHDRDEF@RPTTOMES HDR" + "\n");

                // 2009-12-04-임종우 : 공정검색 할때만 사용 (한개의 설비가 여러 공정에 사용하기에 공정지정안하고 검색하면 중복데이터 발생함)
                // 2011-09-15-임종우 : 설비 공정간 맵핑 정보의 중복으로 인해 조업시간 오류 문제 수정. 
                if (cdvOper.Text != "ALL" && cdvOper.Text != "")
                {
                    strSqlString.AppendFormat("             , (SELECT DISTINCT RES_ID FROM MRASRESMFO" + "\n");
                    strSqlString.AppendFormat("                 WHERE OPER " + cdvOper.SelectedValueToQueryString + ") MFO" + "\n");
                }

                strSqlString.AppendFormat("         WHERE 1=1 " + "\n");
                strSqlString.AppendFormat("           AND RES.FACTORY = DWH.FACTORY(+)  " + "\n");
                strSqlString.AppendFormat("           AND RES.FACTORY = MOV.FACTORY(+)   " + "\n");
                strSqlString.AppendFormat("           AND RES.RES_ID = DWH.RES_ID(+)  " + "\n");
                strSqlString.AppendFormat("           AND RES.RES_ID = RAL.RES_ID(+)  " + "\n");
                strSqlString.AppendFormat("           AND RES.RES_ID = MOV.RES_ID(+)   " + "\n");
                strSqlString.AppendFormat("           AND RES.RES_ID = HDR.RESV_FIELD10(+)   " + "\n");
                strSqlString.AppendFormat("           AND RES.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.AppendFormat("           AND RES.RES_CMF_20 < '" + strFromDate + "'   " + "\n");

                // 2009-11-30-임종우 : 박남규K 요청에 의해 모든 설비 데이터 표시로 변경-> 공정별로 설비를 구분할 수 있으므로...
                // HMKT1은 자원타입이 TESTER인것만 효율 반영(그 이외의 것 데이터 안봄)
                //if (cdvFactory.txtValue.Equals(GlobalVariable.gsTestDefaultFactory))
                //{
                //    strSqlString.AppendFormat("       AND RES.RES_TYPE = 'TESTER'" + "\n");
                //}
                //else
                //{

                //if (cdvEquipType.Text == "EQUIP/LINE")
                if (cdvEquipType.SelectedIndex == 0)
                {
                    strSqlString.AppendFormat("           AND RES.RES_TYPE IN ('EQUIPMENT', 'LINE')" + "\n");                    
                }
                else
                {
                    strSqlString.AppendFormat("           AND RES.RES_TYPE IN ('SUB EQUIPMENT')" + "\n");
                }
                //}

                // 2014-05-24-임종우 : USE_STATE 에 따른 검색 기능 추가
                if (ckdUse.Checked == true)
                {
                    strSqlString.AppendFormat("           AND RES.RES_CMF_9='Y'" + "\n");
                }

                strSqlString.AppendFormat("           AND RES.DELETE_FLAG = ' '" + "\n");
                // 2011-04-04 김민우 : $$$ 설비 제외
                strSqlString.AppendFormat("           AND RES.RES_ID NOT LIKE '%$$$%'" + "\n");

                // 2009.12.04(임종우) 공정검색 할때만 사용 (한개의 설비가 여러 공정에 사용하기에 공정지정안하고 검색하면 중복데이터 발생함)
                if (cdvOper.Text != "ALL" && cdvOper.Text != "")
                {
                    strSqlString.AppendFormat("           AND RES.FACTORY = MOV.FACTORY(+)   " + "\n");
                    strSqlString.AppendFormat("           AND RES.RES_ID = MFO.RES_ID  " + "\n");
                    //strSqlString.AppendFormat("           AND MFO.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                }

                // 2015-11-19-임종우 : 지정설비 검색 조건 추가
                //if (cdvCare.Text == "Designated Equipment")
                if (cdvCare.SelectedIndex == 1)
                {
                    strSqlString.AppendFormat("           AND RES.RES_ID IN (SELECT KEY_1 FROM MGCMTBLDAT WHERE FACTORY = '" + cdvFactory.Text + "' AND TABLE_NAME = 'H_RES_CARE_LIST')  " + "\n");
                }
                //else if (cdvCare.Text == "Unspecified equipment")
                else if (cdvCare.SelectedIndex == 2)
                {
                    strSqlString.AppendFormat("           AND RES.RES_ID NOT IN (SELECT KEY_1 FROM MGCMTBLDAT WHERE FACTORY = '" + cdvFactory.Text + "' AND TABLE_NAME = 'H_RES_CARE_LIST')  " + "\n");
                }

                #region " RAS 상세 조회 "
                if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                    strSqlString.AppendFormat("           AND RES.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

                if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                    strSqlString.AppendFormat("           AND RES.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

                if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                    strSqlString.AppendFormat("           AND RES.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

                if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                    strSqlString.AppendFormat("           AND RES.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

                if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                    strSqlString.AppendFormat("           AND RES.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

                if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    strSqlString.AppendFormat("           AND RES.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

                if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                    strSqlString.AppendFormat("           AND RES.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);
                #endregion

                strSqlString.AppendFormat("         GROUP BY RES.FACTORY,RES.RES_GRP_1, RES.RES_GRP_2, RES_GRP_3, RES.RES_GRP_5, RES.RES_GRP_6, RES.RES_ID, MOV.RUN_TIME, RES.SUB_AREA_ID, RES.RES_CMF_1, RES.RES_CMF_4, RES.RES_CMF_19, HDR.HDR_ID, HDR.MODEL, HDR.SN, HDR.OWNER" + "\n");
                strSqlString.AppendFormat("         ORDER BY RES.FACTORY,RES.RES_GRP_1, RES.RES_GRP_2, RES_GRP_3, RES.RES_GRP_5, RES.RES_GRP_6, RES.RES_ID, RES.SUB_AREA_ID, RES.RES_CMF_1, RES.RES_CMF_4, RES.RES_CMF_19, HDR.HDR_ID, HDR.MODEL, HDR.SN, HDR.OWNER" + "\n");
                strSqlString.AppendFormat("       )" + "\n");
                strSqlString.AppendFormat(" GROUP BY TEAM," + QueryCond1 + "\n");
                strSqlString.AppendFormat(" ORDER BY TEAM," + QueryCond1 + "\n");

                #endregion
            }
            else
            {
                #region 2. Line SUB 설비
                strSqlString.AppendFormat("SELECT " + QueryCond1 + "\n");
                strSqlString.AppendFormat("     , CASE WHEN NVL(SUM(VALUE_RUN_TIME), 0) = 0 THEN 0 " + "\n");
                strSqlString.AppendFormat("            WHEN (NVL(SUM(RUN_TIME), 1440) - (NVL(SUM(TIME_SUM), 0) / 60)) = 0 THEN 0 " + "\n");
                strSqlString.AppendFormat("            ELSE ROUND(((NVL(SUM(RUN_TIME), 1440) / DECODE(SUM(TOTAL_RUN_TIME), 0, 1440, SUM(TOTAL_RUN_TIME))) * 100) * (NVL(ROUND((SUM(VALUE_RUN_TIME) / DECODE(SUM(RUN_TIME), 0, 1440, SUM(RUN_TIME))) * 100, 2), 0)) / 100, 2) " + "\n");
                strSqlString.AppendFormat("       END AS TOTAL_PERCENT " + "\n");
                strSqlString.AppendFormat("     , CASE WHEN NVL(SUM(VALUE_RUN_TIME), 0) = 0 THEN 0 " + "\n");
                strSqlString.AppendFormat("            WHEN NVL(SUM(RUN_TIME),0) = 0 THEN 0 " + "\n");
                strSqlString.AppendFormat("            ELSE NVL(ROUND((SUM(VALUE_RUN_TIME) / DECODE(SUM(RUN_TIME), 0, 1440, SUM(RUN_TIME))) * 100, 2), 0) " + "\n");
                strSqlString.AppendFormat("       END AS ABILITY_PERCENT " + "\n");
                strSqlString.AppendFormat("     , ROUND((NVL(SUM(RUN_TIME), 1440) / NVL(SUM(TOTAL_RUN_TIME), 1440)) * 100, 2) AS TIME_PERCENT " + "\n");
                strSqlString.AppendFormat("     , ROUND((NVL(SUM(REALTIME), 1440) / NVL(SUM(TOTAL_RUN_TIME), 1440)) * 100, 2) AS AUTO_TIME_PERCENT " + "\n");
                strSqlString.AppendFormat("     , ROUND((NVL(SUM(TOTAL_RUN_TIME), 1440) - SUM(DOWN_TIME_RUNDOWN)) / NVL(SUM(TOTAL_RUN_TIME), 1440) * 100, 2) AS LOSS_PERCENT " + "\n");
                strSqlString.AppendFormat("     , SUM(TOTAL_RUN_TIME) AS TOTAL_RUN_TIME " + "\n");
                strSqlString.AppendFormat("     , SUM(VALUE_RUN_TIME) AS VALUE_RUN_TIME" + "\n");
                strSqlString.AppendFormat("     , SUM(RUN_TIME) AS RUN_TIME" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(REALTIME), 2) AS AUTO_REALTIME" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_TIME) AS DOWN_TIME" + "\n");
                strSqlString.AppendFormat("     , SUM(QTY) AS QTY" + "\n");
                strSqlString.AppendFormat("     , ROUND(CASE WHEN SUM(TOTAL_RUN_TIME) = SUM(DOWN_TIME) THEN 0 " + "\n");
                strSqlString.AppendFormat("                  WHEN NVL(SUM(RUN_TIME),0) = 0 THEN 0  " + "\n");
                strSqlString.AppendFormat("                  WHEN SUM(DOWN_CNT_BM) = 0 THEN SUM(TOTAL_RUN_TIME) " + "\n");
                strSqlString.AppendFormat("                  WHEN SUM(DOWN_CNT_BM) = 1 THEN (SUM(TOTAL_RUN_TIME) - SUM(DOWN_TIME)) / 2 " + "\n");
                strSqlString.AppendFormat("                  ELSE (SUM(TOTAL_RUN_TIME) - SUM(DOWN_TIME)) / SUM(DOWN_CNT_BM) " + "\n");
                strSqlString.AppendFormat("             END, 0) AS MTBF " + "\n");
                strSqlString.AppendFormat("     , SUM(MTTC) AS MTTC" + "\n");

                // 2012-05-23-김민우- 최종 GROUP BY 전에 계산 하여 GROUP BY 값이 안 맞은 오류 수정
                strSqlString.AppendFormat("     , ROUND(SUM(RAITO_BM) / SUM(RAITO_RUN_TIME)*100,2) AS RAITO_BM" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(RAITO_RUNDOWN) / SUM(RAITO_RUN_TIME)*100,2) AS RAITO_BM" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(RAITO_PM) / SUM(RAITO_RUN_TIME)*100,2) AS RAITO_BM" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(RAITO_DEVCHG) / SUM(RAITO_RUN_TIME)*100,2) AS RAITO_BM" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(RAITO_ENGDOWN) / SUM(RAITO_RUN_TIME)*100,2) AS RAITO_BM" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(RAITO_SUPERDOWN) / SUM(RAITO_RUN_TIME)*100,2) AS RAITO_BM" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(RAITO_UTILDOWN) / SUM(RAITO_RUN_TIME)*100,2) AS RAITO_BM" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(RAITO_ETC) / SUM(RAITO_RUN_TIME)*100,2) AS RAITO_BM" + "\n");

                strSqlString.AppendFormat("     , SUM(DOWN_TIME_BM)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_TIME_RUNDOWN)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_TIME_PM)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_TIME_DEVCHG)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_TIME_ENGDOWN)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_TIME_SUPERDOWN)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_TIME_UTILDOWN)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_TIME_ETC)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_CNT_BM)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_CNT_RUNDOWN)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_CNT_PM)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_CNT_DEVCHG)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_CNT_ENGDOWN)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_CNT_SUPERDOWN)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_CNT_UTILDOWN)" + "\n");
                strSqlString.AppendFormat("     , SUM(DOWN_CNT_ETC)" + "\n");
                                
                strSqlString.AppendFormat("     , ROUND(CASE WHEN SUM(TOTAL_RUN_TIME) = SUM(DOWN_TIME) THEN 0 " + "\n");
                strSqlString.AppendFormat("                  WHEN SUM(DOWN_CNT_H) = 0 THEN SUM(RUN_TIME) " + "\n");                
                strSqlString.AppendFormat("                  ELSE (SUM(TOTAL_RUN_TIME) - SUM(DOWN_TIME)) / SUM(DOWN_CNT_H) " + "\n");
                strSqlString.AppendFormat("             END, 0) AS MTBF_TEST " + "\n");

                strSqlString.AppendFormat("  FROM (" + "\n");
                strSqlString.AppendFormat("        SELECT NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.SUBRES_GRP_1), '-') AS TEAM " + "\n");
                strSqlString.AppendFormat("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.SUBRES_GRP_2), '-') AS PART" + "\n");
                strSqlString.AppendFormat("             , RES.SUBRES_GRP_3 AS EQP_TYPE, RES.SUBRES_GRP_5 AS MAKER, RES.SUBRES_GRP_6 AS MODEL, RES.SUBRES_ID AS RES_ID, RES.SUB_AREA_ID" + "\n");
                strSqlString.AppendFormat("             , CASE WHEN SUM(NVL(MOV.VALUE_RUN_TIME,0)) = 0 THEN 0" + "\n");
                strSqlString.AppendFormat("                    WHEN (SUM(NVL(DWH.RUN_TIME," + JoupTime + ")) - (SUM(NVL(DWH.TIME_SUM,0)) / 60)) = 0 THEN 0" + "\n");
                strSqlString.AppendFormat("                    ELSE ROUND((ROUND((SUM(MOV.VALUE_RUN_TIME) / (SUM(NVL(DWH.RUN_TIME," + JoupTime + ")) - NVL((SUM(DWH.TIME_SUM)/60),0)))*100,2) * ROUND(((SUM(NVL(DWH.RUN_TIME," + JoupTime + ")) - (SUM(NVL(DWH.TIME_SUM,0))/60)) / SUM(NVL(DWH.RUN_TIME," + JoupTime + ")))*100,2))/100,2)" + "\n");
                strSqlString.AppendFormat("               END  AS TOTAL_PERCENT" + "\n");
                strSqlString.AppendFormat("             , CASE WHEN SUM(NVL(MOV.VALUE_RUN_TIME,0)) = 0 THEN 0" + "\n");
                strSqlString.AppendFormat("                    WHEN SUM(NVL(DWH.RUN_TIME," + JoupTime + ")) - (SUM(NVL(DWH.TIME_SUM,0))/60) = 0 THEN 0" + "\n");
                strSqlString.AppendFormat("                    ELSE NVL(ROUND((SUM(MOV.VALUE_RUN_TIME) / (SUM(NVL(DWH.RUN_TIME," + JoupTime + ")) - NVL((SUM(DWH.TIME_SUM)/60),0)))*100,2),0)" + "\n");
                strSqlString.AppendFormat("               END  AS ABILITY_PERCENT" + "\n");
                strSqlString.AppendFormat("             , ROUND(((SUM(NVL(DWH.RUN_TIME," + JoupTime + ")) - NVL((SUM(TIME_SUM)/60),0)) / SUM(NVL(DWH.RUN_TIME," + JoupTime + ")))*100,2) AS TIME_PERCENT" + "\n");
                strSqlString.AppendFormat("             , DECODE(SUM(DWH.DOWN_TIME_RUNDOWN),0,100, ROUND((((SUM(NVL(DWH.RUN_TIME," + JoupTime + ")) - SUM(DWH.DOWN_TIME_RUNDOWN)))/SUM(NVL(DWH.RUN_TIME," + JoupTime + "))*100),2)) AS LOSS_PERCENT" + "\n");                
                strSqlString.AppendFormat("             , DECODE(NVL(DWH.RUN_TIME," + JoupTime + "),'', " + JoupTime + ", NVL(DWH.RUN_TIME," + JoupTime + ")) AS TOTAL_RUN_TIME" + "\n");
                strSqlString.AppendFormat("             , NVL(SUM(MOV.VALUE_RUN_TIME),0) AS VALUE_RUN_TIME" + "\n");
                strSqlString.AppendFormat("             , CASE WHEN SUM(NVL(DWH.RUN_TIME,0)) = 0 THEN 0" + "\n");
                strSqlString.AppendFormat("                    ELSE ROUND(SUM(NVL(DWH.RUN_TIME,0)) - NVL(ROUND((SUM(DWH.TIME_SUM)/60),2),0),2)" + "\n");
                strSqlString.AppendFormat("               END AS RUN_TIME" + "\n");
                strSqlString.AppendFormat("             , NVL(ROUND((SUM(DWH.TIME_SUM)/60),2),0) AS DOWN_TIME" + "\n");
                strSqlString.AppendFormat("             , NVL(SUM(MOV.QTY),0) AS QTY" + "\n");
                strSqlString.AppendFormat("             , DECODE(SUM(NVL(DWH.DOWN_CNT_BM,0)),0,0,ROUND((SUM(NVL(DWH.RUN_TIME," + JoupTime + ")) - (SUM(DWH.TIME_SUM)/60))/(SUM(DWH.DOWN_CNT_BM)+SUM(DWH.DOWN_CNT_PM) + SUM(DWH.DOWN_CNT_ETC)),2)) AS MTBF" + "\n");
                strSqlString.AppendFormat("             , DECODE(SUM(NVL(DWH.DOWN_CNT_DEVCHG,0)),0,0,ROUND((SUM(DWH.DOWN_TIME_DEVCHG)) / SUM(DWH.DOWN_CNT_DEVCHG),2)) AS MTTC" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.RAITO_BM,0)) AS RAITO_BM" + "\n");                
                strSqlString.AppendFormat("             , SUM(NVL(DWH.RUN_TIME," + JoupTime + ")) AS RAITO_RUN_TIME" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.RAITO_RUNDOWN,0)) AS RAITO_RUNDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.RAITO_PM,0)) AS RAITO_PM" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.RAITO_DEVCHG,0)) AS RAITO_DEVCHG" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.RAITO_ENGDOWN,0)) AS RAITO_ENGDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.RAITO_SUPERDOWN,0)) AS RAITO_SUPERDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.RAITO_UTILDOWN,0)) AS RAITO_UTILDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.RAITO_ETC,0)) AS RAITO_ETC" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_TIME_BM,0)) AS DOWN_TIME_BM" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_TIME_RUNDOWN,0)) AS DOWN_TIME_RUNDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_TIME_PM,0)) AS DOWN_TIME_PM" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_TIME_DEVCHG,0)) AS DOWN_TIME_DEVCHG" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_TIME_ENGDOWN,0)) AS DOWN_TIME_ENGDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_TIME_SUPERDOWN,0)) AS DOWN_TIME_SUPERDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_TIME_UTILDOWN,0)) AS DOWN_TIME_UTILDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_TIME_ETC,0)) AS DOWN_TIME_ETC" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(TIME_SUM,0)) AS TIME_SUM " + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_CNT_BM,0)) AS DOWN_CNT_BM" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_CNT_RUNDOWN,0)) AS DOWN_CNT_RUNDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_CNT_PM,0)) AS DOWN_CNT_PM" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_CNT_DEVCHG,0)) AS DOWN_CNT_DEVCHG" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_CNT_ENGDOWN,0)) AS DOWN_CNT_ENGDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_CNT_SUPERDOWN,0)) AS DOWN_CNT_SUPERDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_CNT_UTILDOWN,0)) AS DOWN_CNT_UTILDOWN" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_CNT_ETC,0)) AS DOWN_CNT_ETC" + "\n");
                strSqlString.AppendFormat("             , SUM(NVL(DWH.DOWN_CNT_H,0)) + SUM(NVL(DWH.DOWN_CNT_BM,0)) AS DOWN_CNT_H" + "\n");
                strSqlString.AppendFormat("             , 0 AS REALTIME" + "\n");
                strSqlString.AppendFormat("          FROM (" + "\n");
                strSqlString.AppendFormat("                SELECT FACTORY,RES_ID" + "\n");
                strSqlString.AppendFormat("                     , " + JoupTime + " AS RUN_TIME" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'BM',TIME_SUM/60, 0)) AS RAITO_BM" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'RUNDOWN', TIME_SUM/60, 0)) AS RAITO_RUNDOWN" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'PM',TIME_SUM/60, 0)) AS RAITO_PM" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'DEVCHG', TIME_SUM/60, 0)) AS RAITO_DEVCHG" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'ENGDOWN', TIME_SUM/60, 0)) AS RAITO_ENGDOWN" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'SUPERDOWN', TIME_SUM/60, 0)) AS RAITO_SUPERDOWN" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'UTILDOWN', TIME_SUM/60, 0)) AS RAITO_UTILDOWN" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'ETC', TIME_SUM/60, 0)) AS RAITO_ETC" + "\n");
                strSqlString.AppendFormat("                     , ROUND(SUM(DECODE(DOWN_EVENT_ID, 'BM', TIME_SUM/60, 0)), 2) AS DOWN_TIME_BM" + "\n");
                strSqlString.AppendFormat("                     , ROUND(SUM(DECODE(DOWN_EVENT_ID, 'RUNDOWN', TIME_SUM/60, 0)), 2) AS DOWN_TIME_RUNDOWN" + "\n");
                strSqlString.AppendFormat("                     , ROUND(SUM(DECODE(DOWN_EVENT_ID, 'PM', TIME_SUM/60, 0)), 2) AS DOWN_TIME_PM" + "\n");
                strSqlString.AppendFormat("                     , ROUND(SUM(DECODE(DOWN_EVENT_ID, 'DEVCHG', TIME_SUM/60, 0)), 2) AS DOWN_TIME_DEVCHG" + "\n");
                strSqlString.AppendFormat("                     , ROUND(SUM(DECODE(DOWN_EVENT_ID, 'ENGDOWN', TIME_SUM/60, 0)), 2) AS DOWN_TIME_ENGDOWN" + "\n");
                strSqlString.AppendFormat("                     , ROUND(SUM(DECODE(DOWN_EVENT_ID, 'SUPERDOWN', TIME_SUM/60, 0)), 2) AS DOWN_TIME_SUPERDOWN" + "\n");
                strSqlString.AppendFormat("                     , ROUND(SUM(DECODE(DOWN_EVENT_ID, 'UTILDOWN', TIME_SUM/60, 0)), 2) AS DOWN_TIME_UTILDOWN" + "\n");
                strSqlString.AppendFormat("                     , ROUND(SUM(DECODE(DOWN_EVENT_ID, 'ETC', TIME_SUM/60, 0)), 2) AS DOWN_TIME_ETC" + "\n");
                strSqlString.AppendFormat("                     , SUM(TIME_SUM) AS TIME_SUM" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'BM', DOWN_CNT, 0)) AS DOWN_CNT_BM" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'RUNDOWN', DOWN_CNT, 0)) AS DOWN_CNT_RUNDOWN" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'PM', DOWN_CNT, 0)) AS DOWN_CNT_PM" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'DEVCHG', DOWN_CNT, 0)) AS DOWN_CNT_DEVCHG" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'ENGDOWN', DOWN_CNT, 0)) AS DOWN_CNT_ENGDOWN" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'SUPERDOWN', DOWN_CNT, 0)) AS DOWN_CNT_SUPERDOWN" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'UTILDOWN', DOWN_CNT, 0)) AS DOWN_CNT_UTILDOWN" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(DOWN_EVENT_ID, 'ETC', DOWN_CNT, 0)) AS DOWN_CNT_ETC" + "\n");
                strSqlString.AppendFormat("                     , SUM(DECODE(H_CODE, 'H', DOWN_CNT, 0)) AS DOWN_CNT_H" + "\n");
                strSqlString.AppendFormat("                  FROM (" + "\n");
                strSqlString.AppendFormat("                        SELECT MNT.FACTORY,MNT.RES_ID, MNT.MAINT_CODE" + "\n");
                strSqlString.AppendFormat("                             , CASE WHEN SUBSTR(MAINT_CODE,0,2) IN ('H1','H2','H3','H4') THEN 'H' END AS H_CODE" + "\n");
                strSqlString.AppendFormat("                             , CASE WHEN SUBSTR(MAINT_CODE,0,1)='A' THEN 'BM'" + "\n");
                strSqlString.AppendFormat("                                    WHEN SUBSTR(MAINT_CODE,0,1)='B' THEN 'RUNDOWN'" + "\n");
                strSqlString.AppendFormat("                                    WHEN SUBSTR(MAINT_CODE,0,1)='C' THEN 'PM'" + "\n");
                strSqlString.AppendFormat("                                    WHEN SUBSTR(MAINT_CODE,0,1)='D' THEN 'DEVCHG'" + "\n");
                strSqlString.AppendFormat("                                    WHEN SUBSTR(MAINT_CODE,0,1)='E' THEN 'ENGDOWN'" + "\n");
                strSqlString.AppendFormat("                                    WHEN SUBSTR(MAINT_CODE,0,1)='F' THEN 'SUPERDOWN'" + "\n");
                strSqlString.AppendFormat("                                    WHEN SUBSTR(MAINT_CODE,0,1)='G' THEN 'UTILDOWN'" + "\n");
                strSqlString.AppendFormat("                                    WHEN SUBSTR(MAINT_CODE,0,1)='H' THEN 'ETC'" + "\n");
                strSqlString.AppendFormat("                               END AS DOWN_EVENT_ID,NVL(SUM(TIME_SUM),0) AS TIME_SUM" + "\n");
                strSqlString.AppendFormat("                             , DECODE(SUBSTR(MAINT_CODE,0,1) " + "\n");
                strSqlString.AppendFormat("                                      , 'B', NVL(DECODE(MAINT_STEP,'D_WAIT',SUM(FREQUENCY)),0)" + "\n");
                strSqlString.AppendFormat("                                      , 'D', NVL(DECODE(MAINT_STEP,'D_WAIT',SUM(FREQUENCY)),0) " + "\n");
                strSqlString.AppendFormat("                                      , 'E', NVL(DECODE(MAINT_STEP,'D_WAIT',SUM(FREQUENCY)),0) " + "\n");
                strSqlString.AppendFormat("                                      , 'F', NVL(DECODE(MAINT_STEP,'D_WAIT',SUM(FREQUENCY)),0)" + "\n");
                strSqlString.AppendFormat("                                      , 'G', NVL(DECODE(MAINT_STEP,'D_WAIT',SUM(FREQUENCY)),0)" + "\n");
                strSqlString.AppendFormat("                                      ,  NVL(DECODE(MAINT_STEP,'D_MAINT',SUM(FREQUENCY)),0)" + "\n");
                strSqlString.AppendFormat("                               ) AS DOWN_CNT" + "\n");
                strSqlString.AppendFormat("                          FROM CSUMRESMNT@RPTTOMES MNT" + "\n");
                strSqlString.AppendFormat("                             , MRASSRSDEF@RPTTOMES RES" + "\n");
                strSqlString.AppendFormat("                         WHERE 1=1" + "\n");
                strSqlString.AppendFormat("                           AND MNT.FACTORY = RES.FACTORY" + "\n");
                strSqlString.AppendFormat("                           AND MNT.RES_ID = RES.SUBRES_ID" + "\n");
                strSqlString.AppendFormat("                           AND RES.SUBRES_CMF_20 < '" + strFromDate + "'" + "\n");
                strSqlString.AppendFormat("                           AND MNT.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.AppendFormat("                           AND MNT.WORK_DATE BETWEEN '{0}' AND '{1}' " + "\n", strFromDay, strToDay);
                strSqlString.AppendFormat("                           AND MNT.RESV_FIELD_1 <> ' '" + "\n");
                strSqlString.AppendFormat("                           AND MNT.MAINT_CODE NOT IN ('D126','D216','D316')" + "\n");
                strSqlString.AppendFormat("                         GROUP BY MNT.FACTORY, MNT.RES_ID, MNT.MAINT_CODE, MNT.MAINT_STEP" + "\n");
                strSqlString.AppendFormat("                       ) RES" + "\n");
                strSqlString.AppendFormat("                 GROUP BY FACTORY, RES_ID" + "\n");
                strSqlString.AppendFormat("                 ORDER BY RES_ID" + "\n");
                strSqlString.AppendFormat("               ) DWH" + "\n");                
                strSqlString.AppendFormat("             , (" + "\n");
                strSqlString.AppendFormat("                SELECT MOV.FACTORY" + "\n");
                strSqlString.AppendFormat("                     , MOV.RES_ID" + "\n");
                strSqlString.AppendFormat("                     , SUM(VALUE_RUN_TIME) AS VALUE_RUN_TIME" + "\n");
                strSqlString.AppendFormat("                     , SUM(LOSS_QTY) AS LOSS_QTY" + "\n");
                //strSqlString.AppendFormat("                     , " + JoupTime + " AS RUN_TIME" + "\n");

                // 2014-07-18-임종우 : 실적 환산 수량으로 구하기
                if (ckbConv.Checked == true)
                {
                    strSqlString.AppendFormat("                     , ROUND(SUM(CASE WHEN MOV.OPER LIKE 'A06%' THEN (QTY + LOSS_QTY) * NVL(WIRE_CNT, MAT_GRP_6)" + "\n");
                    strSqlString.AppendFormat("                                      WHEN MOV.OPER = 'A1300' THEN (QTY + LOSS_QTY) * NVL(MAT_GRP_6, 1)" + "\n");
                    strSqlString.AppendFormat("                                      WHEN MOV.OPER IN ('A0020', 'A0030', 'A0040', 'A0200', 'A0230') THEN (QTY + LOSS_QTY) / (TO_NUMBER(DECODE(MAT_CMF_13,' ',1,MAT_CMF_13)) * 0.85)" + "\n");
                    strSqlString.AppendFormat("                                      ELSE (QTY + LOSS_QTY)" + "\n");
                    strSqlString.AppendFormat("                                 END),0) AS QTY" + "\n");
                    strSqlString.AppendFormat("                  FROM CSUMRASMOV@RPTTOMES MOV" + "\n");
                    strSqlString.AppendFormat("                     , MWIPMATDEF MAT " + "\n");
                    strSqlString.AppendFormat("                     , ( " + "\n");
                    strSqlString.AppendFormat("                        SELECT MAT_ID, OPER, TO_NUMBER(TCD_CMF_2) AS WIRE_CNT " + "\n");
                    strSqlString.AppendFormat("                          FROM CWIPTCDDEF@RPTTOMES " + "\n");
                    strSqlString.AppendFormat("                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.AppendFormat("                           AND SET_FLAG = 'Y' " + "\n");
                    strSqlString.AppendFormat("                           AND TCD_CMF_2 <> ' ' " + "\n");
                    strSqlString.AppendFormat("                       ) WIR" + "\n");
                    strSqlString.AppendFormat("                 WHERE 1=1" + "\n");
                    strSqlString.AppendFormat("                   AND MOV.MAT_ID = WIR.MAT_ID(+)" + "\n");
                    strSqlString.AppendFormat("                   AND MOV.OPER = WIR.OPER(+)" + "\n");
                }
                else
                {
                    strSqlString.AppendFormat("                     , SUM(QTY + LOSS_QTY) AS QTY" + "\n");
                    strSqlString.AppendFormat("                  FROM CSUMRASMOV@RPTTOMES MOV" + "\n");
                    strSqlString.AppendFormat("                     , MWIPMATDEF MAT " + "\n");
                    strSqlString.AppendFormat("                 WHERE 1=1" + "\n");
                }

                strSqlString.AppendFormat("                   AND MOV.FACTORY = MAT.FACTORY " + "\n");
                strSqlString.AppendFormat("                   AND MOV.MAT_ID = MAT.MAT_ID " + "\n");
                strSqlString.AppendFormat("                   AND MOV.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.AppendFormat("                   AND MOV.WORK_DAY BETWEEN '" + strFromDay + "' AND '" + strToDay + "'" + "\n");
                strSqlString.AppendFormat("                   AND MOV.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                strSqlString.AppendFormat("                   AND MAT.MAT_TYPE = 'FG' " + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (cdvFactory.txtValue == "HMKB1")
                {
                    if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                        strSqlString.AppendFormat("            AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                    if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                        strSqlString.AppendFormat("            AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                    if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                        strSqlString.AppendFormat("            AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                    if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                        strSqlString.AppendFormat("            AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                    if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                        strSqlString.AppendFormat("            AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                    if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                        strSqlString.AppendFormat("            AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                    if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                        strSqlString.AppendFormat("            AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                    if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                    if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_CMF_1 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                    if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                    if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                    if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                }
                else
                {
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("                   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                }
                #endregion

                
                strSqlString.AppendFormat("                 GROUP BY MOV.FACTORY,MOV.RES_ID" + "\n");
                strSqlString.AppendFormat("               ) MOV" + "\n");
                strSqlString.AppendFormat("             , MRASSRSDEF@RPTTOMES RES" + "\n");

                // 2009-12-04-임종우 : 공정검색 할때만 사용 (한개의 설비가 여러 공정에 사용하기에 공정지정안하고 검색하면 중복데이터 발생함)
                // 2011-09-15-임종우 : 설비 공정간 맵핑 정보의 중복으로 인해 조업시간 오류 문제 수정. 
                if (cdvOper.Text != "ALL" && cdvOper.Text != "")
                {
                    strSqlString.AppendFormat("             , (SELECT DISTINCT RES_ID FROM MRASRESMFO" + "\n");
                    strSqlString.AppendFormat("                 WHERE OPER " + cdvOper.SelectedValueToQueryString + ") MFO" + "\n");
                }

                strSqlString.AppendFormat("         WHERE 1=1 " + "\n");
                strSqlString.AppendFormat("           AND RES.FACTORY = DWH.FACTORY(+)  " + "\n");
                strSqlString.AppendFormat("           AND RES.FACTORY = MOV.FACTORY(+)   " + "\n");
                strSqlString.AppendFormat("           AND RES.SUBRES_ID = DWH.RES_ID(+)  " + "\n");                
                strSqlString.AppendFormat("           AND RES.SUBRES_ID = MOV.RES_ID(+)   " + "\n");
                strSqlString.AppendFormat("           AND RES.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.AppendFormat("           AND RES.SUBRES_CMF_20 < '" + strFromDate + "'   " + "\n");  
                strSqlString.AppendFormat("           AND RES.SUBRES_TYPE NOT IN ('DUMMY')" + "\n");
              

                // 2014-05-24-임종우 : USE_STATE 에 따른 검색 기능 추가
                if (ckdUse.Checked == true)
                {
                    strSqlString.AppendFormat("           AND RES.SUBRES_CMF_9='Y'" + "\n");
                }

                strSqlString.AppendFormat("           AND RES.DELETE_FLAG = ' '" + "\n");
                // 2011-04-04 김민우 : $$$ 설비 제외
                strSqlString.AppendFormat("           AND RES.SUBRES_ID NOT LIKE '%$$$%'" + "\n");

                // 2009.12.04(임종우) 공정검색 할때만 사용 (한개의 설비가 여러 공정에 사용하기에 공정지정안하고 검색하면 중복데이터 발생함)
                if (cdvOper.Text != "ALL" && cdvOper.Text != "")
                {
                    strSqlString.AppendFormat("           AND RES.FACTORY = MOV.FACTORY(+)   " + "\n");
                    strSqlString.AppendFormat("           AND RES.SUBRES_ID = MFO.RES_ID  " + "\n");
                    //strSqlString.AppendFormat("           AND MFO.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                }

                // 2015-11-19-임종우 : 지정설비 검색 조건 추가
                //if (cdvCare.Text == "Designated Equipment")
                if (cdvCare.SelectedIndex == 1)
                {
                    strSqlString.AppendFormat("           AND RES.RES_ID IN (SELECT KEY_1 FROM MGCMTBLDAT WHERE FACTORY = '" + cdvFactory.Text + "' AND TABLE_NAME = 'H_RES_CARE_LIST')  " + "\n");
                }
                //else if (cdvCare.Text == "Unspecified equipment")
                else if (cdvCare.SelectedIndex == 2)
                {
                    strSqlString.AppendFormat("           AND RES.RES_ID NOT IN (SELECT KEY_1 FROM MGCMTBLDAT WHERE FACTORY = '" + cdvFactory.Text + "' AND TABLE_NAME = 'H_RES_CARE_LIST')  " + "\n");
                }

                #region " RAS 상세 조회 "
                if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                    strSqlString.AppendFormat("           AND RES.SUBRES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

                if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                    strSqlString.AppendFormat("           AND RES.SUBRES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

                if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                    strSqlString.AppendFormat("           AND RES.SUBRES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

                if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                    strSqlString.AppendFormat("           AND RES.SUBRES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

                if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                    strSqlString.AppendFormat("           AND RES.SUBRES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

                if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    strSqlString.AppendFormat("           AND RES.SUBRES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

                if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                    strSqlString.AppendFormat("           AND RES.SUBRES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);
                #endregion

                strSqlString.AppendFormat("         GROUP BY RES.FACTORY,RES.SUBRES_GRP_1, RES.SUBRES_GRP_2, RES.SUBRES_GRP_3, RES.SUBRES_GRP_5, RES.SUBRES_GRP_6, RES.SUBRES_ID, DWH.RUN_TIME, RES.SUB_AREA_ID" + "\n");
                strSqlString.AppendFormat("         ORDER BY RES.FACTORY,RES.SUBRES_GRP_1, RES.SUBRES_GRP_2, RES.SUBRES_GRP_3, RES.SUBRES_GRP_5, RES.SUBRES_GRP_6, RES.SUBRES_ID, RES.SUB_AREA_ID" + "\n");
                strSqlString.AppendFormat("       )" + "\n");
                strSqlString.AppendFormat(" GROUP BY TEAM," + QueryCond1 + "\n");
                strSqlString.AppendFormat(" ORDER BY TEAM," + QueryCond1 + "\n");

                #endregion
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private string GetJoupTime(string strFromDate, string strToDate)
        {
            int year = 0;
            int month = 0;
            int day = 0;

            DateTime dtFrom = DateTime.MinValue;
            DateTime dtTo = DateTime.MinValue;

            int retValue = 0;

            try
            {
                // 전일 22시 부터 현재 시간까지 경과 시간 구하기
                DateTime baseTime = DateTime.ParseExact(DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "220000", "yyyyMMddHHmmss", null);
                TimeSpan diff = DateTime.Now - baseTime;

                year = Convert.ToInt32(strFromDate.Substring(0, 4));
                month = Convert.ToInt32(strFromDate.Substring(4, 2));
                day = Convert.ToInt32(strFromDate.Substring(6, 2));

                dtFrom = new DateTime(year, month, day);

                string NowDate = Convert.ToString(DateTime.Now).Substring(0, 7);
                string NowDate2 = Convert.ToString(DateTime.Now).Substring(0, 10);

                year = Convert.ToInt32(strToDate.Substring(0, 4));
                month = Convert.ToInt32(strToDate.Substring(4, 2));
                day = Convert.ToInt32(strToDate.Substring(6, 2));

                dtTo = new DateTime(year, month, day);

                string TmpsTo = Convert.ToString(dtTo).Substring(0, 7);
                string TmpsTo2 = Convert.ToString(dtTo).Substring(0, 10);

                if (udcFromToDate.DaySelector.SelectedValue.ToString() == "MONTH" && TmpsTo == NowDate)
                {
                    // (금일 - 1) 로 변경 (2009.09.22 임종우)
                    retValue = (new TimeSpan(DateTime.Now.AddDays(-1).Ticks - dtFrom.Ticks)).Days * 1440;
                }
                else if (udcFromToDate.DaySelector.SelectedValue.ToString() == "DAY" && TmpsTo2 == NowDate2)
                {   // 금일 포함이면 금일은 전일 22시 부터 현재까지의 경과 시간으로 계산한다.
                    retValue = (((new TimeSpan(dtTo.Ticks - dtFrom.Ticks)).Days - 1) * 1440) + Convert.ToInt32(diff.TotalMinutes);
                }
                else
                {
                    retValue = (new TimeSpan(dtTo.Ticks - dtFrom.Ticks)).Days * 1440;
                }
            }
            catch
            {
                throw new InvalidCastException(RptMessages.GetMessage("STD099", GlobalVariable.gcLanguage));
            }


            return retValue.ToString();
        }

        #endregion

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

                //by John
                //1.Griid 합계 표시
                //spdData.DataSource = dt;
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 14, null, null, btnSort);
                //spdData.Sheets[0].Rows[0].Remove();
                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 14, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                SetAvgVertical(1, 18, false);   //부하
                SetAvgVertical(1, 17, false);    //자동화시간
                SetAvgVertical(1, 16, false);    //시간
                SetAvgVertical(1, 15, false);    //성능
                SetAvgVertical(1, 14, false);    //종합
                SetAvgVertical(1, 25, false);   //MTBF
                SetAvgVertical(1, 26, false);   //MTTC
                SetAvgVertical(1, 27, false);
                SetAvgVertical(1, 28, false);
                SetAvgVertical(1, 29, false);
                SetAvgVertical(1, 30, false);
                SetAvgVertical(1, 31, false);
                SetAvgVertical(1, 32, false);
                SetAvgVertical(1, 33, false);
                SetAvgVertical(1, 34, false);
                SetAvgVertical(1, 51, false);
                
                //ShowChart(5);
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

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ");
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, "기간 : " + udcFromToDate.HmFromDay + " ~ " + udcFromToDate.HmToDay, " ");
            spdData.ExportExcel();
        }

        public void SetAvgVertical(int nSampleNormalRowPos, int nColPos, bool bWithNull) // Group 2단까지만 됨. 그 이상 적용 안됨.
        {
            double sum = 0;
            double totalSum = 0;
            double subSum = 0;

            double divide = 0;
            double totalDivide = 0;
            double subDivide = 0;

            int checkValue = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(1);

            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;
            Color checkValueColor = spdData.ActiveSheet.Cells[nSampleNormalRowPos, checkValue].BackColor; // 첫번째 그룹의 첫 열 색 (기준색)

            for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                if (spdData.ActiveSheet.Cells[i, checkValue].BackColor == checkValueColor) //subTotal Sum 구하기 위함.
                {
                    if (spdData.ActiveSheet.Cells[i, nColPos].BackColor == color)
                    {
                        sum += Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos].Value);
                        subSum += Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos].Value);
                        totalSum += Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos].Value);

                        if (!bWithNull && (spdData.ActiveSheet.Cells[i, nColPos].Value == null || spdData.ActiveSheet.Cells[i, nColPos].Value.ToString().Trim() == "" || Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos].Value) == 0))
                            continue;



                        divide += 1;
                        subDivide += 1;                        
                        totalDivide += 1;

                    }
                    else
                    {
                        // 주의!! subTotal Sum 구할때나, GrandTotal 구할때는 각 항목을 더해 Avg 구해야함. SubTatal을 더한 것으로 Avg 구하면 수치 차이가 심함.
                        // 예로 35.00 + 47.81 + 7.49 = 90.30 (avg:30.10) , 15.97 + 4.66 + 9.35 + 19.25 = 49.23 (avg:12.31) 일때
                        // 1.각 항목 더해서 구할때 : 35.00 + 47.81 + 7.49 + 15.97 + 4.66 + 9.35 + 19.25 = 139.53 (avg:19.93), 2. 30.10 + 12.31 = 21.20 나옴.

                        // SubTotal 구함.
                        if (divide != 0)
                        {
                            if (nColPos == 18)
                            {
                                // 부하율 = (조업시간-Run Down)/조업시간 *100
                                spdData.ActiveSheet.Cells[i, nColPos].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[i, 19].Value) - Convert.ToDouble(spdData.ActiveSheet.Cells[i, 36].Value)) / Convert.ToDouble(spdData.ActiveSheet.Cells[i, 19].Value)) * 100;
                            }
                            else if (nColPos == 17)
                            {
                                // 자동화 시간가동율 = 자동화 가동시간 / 조업시간 *100
                                spdData.ActiveSheet.Cells[i, nColPos].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[i, 22].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[i, 19].Value)) * 100;
                            }
                            else if (nColPos == 16)
                            {
                                // 시간가동율 = 가동시간 / 조업시간 *100
                                spdData.ActiveSheet.Cells[i, nColPos].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[i, 21].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[i, 19].Value)) * 100;
                            }
                            else if (nColPos == 15)
                            {
                                //성능가동율 = 가치가동시간 / 가동시간 *100
                                spdData.ActiveSheet.Cells[i, nColPos].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[i, 20].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[i, 21].Value)) * 100;
                            }
                            else if (nColPos == 14)
                            {
                                //설비 종합효율 = 성능가동율 * 시간가동율 / 100
                                spdData.ActiveSheet.Cells[i, nColPos].Value = Math.Round((Math.Round(Convert.ToDouble(spdData.ActiveSheet.Cells[i, 15].Value), 2) * Math.Round(Convert.ToDouble(spdData.ActiveSheet.Cells[i, 16].Value), 2)) / 100, 2);
                            }
                            else if (nColPos == 25 || nColPos == 26 || nColPos == 51)
                            {
                                spdData.ActiveSheet.Cells[i, nColPos].Value = sum / divide;
                            }
                            else
                            {
                                spdData.ActiveSheet.Cells[i, nColPos].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos + 8].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[i, 19].Value)) * 100;
                            }
                        }
                        //spdData.ActiveSheet.Cells[i, nColPos].Value = sum / divide;

                        //subSum += sum;
                        //subDivide += divide;

                        //totalSum += sum;
                        //totalDivide += divide;

                        sum = 0;
                        divide = 0;

                        //subSum += Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos].Value);

                        // 2009.06.08 BongjunPark 공백인것도 카운드하여 %율구하기위해 주석
                        //if (!bWithNull && (spdData.ActiveSheet.Cells[i, nColPos].Value == null || spdData.ActiveSheet.Cells[i, nColPos].Value.ToString().Trim() == ""))
                        //    continue;

                        //subDivide += 1;
                    }
                }
                else
                {
                    // SubTotal Sum 구함.
                    if (subDivide != 0)
                        if (nColPos == 18)
                        {
                            // 부하율 = (조업시간-Run Down)/조업시간 *100
                            spdData.ActiveSheet.Cells[i, nColPos].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[i, 19].Value) - Convert.ToDouble(spdData.ActiveSheet.Cells[i, 36].Value)) / Convert.ToDouble(spdData.ActiveSheet.Cells[i, 19].Value)) * 100;
                        }
                        else if (nColPos == 17)
                        {
                            // 자동화 시간가동율 = 자동화 가동시간 / 조업시간 *100
                            spdData.ActiveSheet.Cells[i, nColPos].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[i, 22].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[i, 19].Value)) * 100;
                        }
                        else if (nColPos == 16)
                        {
                            // 시간가동율 = 가동시간 / 조업시간 *100
                            spdData.ActiveSheet.Cells[i, nColPos].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[i, 21].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[i, 19].Value)) * 100;
                        }
                        else if (nColPos == 15)
                        {
                            //성능가동율 = 가치가동시간 / 가동시간 *100
                            spdData.ActiveSheet.Cells[i, nColPos].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[i, 20].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[i, 21].Value)) * 100;
                        }
                        else if (nColPos == 14)
                        {
                            //설비 종합효율 = 성능가동율 * 시간가동율 / 100
                            spdData.ActiveSheet.Cells[i, nColPos].Value = Math.Round((Math.Round(Convert.ToDouble(spdData.ActiveSheet.Cells[i, 15].Value), 2) * Math.Round(Convert.ToDouble(spdData.ActiveSheet.Cells[i, 16].Value), 2)) / 100, 2);
                        }
                        else if (nColPos == 25 || nColPos == 26 || nColPos == 51)
                        {
                            spdData.ActiveSheet.Cells[i, nColPos].Value = subSum / subDivide;
                        }
                        else
                        {
                            spdData.ActiveSheet.Cells[i, nColPos].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos + 8].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[i, 19].Value)) * 100;
                        }

                    //spdData.ActiveSheet.Cells[i, nColPos].Value = subSum / subDivide;

                    subSum = 0;
                    subDivide = 0;
                }
            }

            // GrandTotal 구함.
            if (totalDivide != 0)
            {
                if (nColPos == 18)
                {
                    spdData.ActiveSheet.Cells[0, nColPos].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[0, 19].Value) - Convert.ToDouble(spdData.ActiveSheet.Cells[0, 36].Value)) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 19].Value)) * 100;
                }
                else if (nColPos == 17)
                {
                    spdData.ActiveSheet.Cells[0, nColPos].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, 22].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 19].Value)) * 100;
                }
                else if (nColPos == 16)
                {
                    spdData.ActiveSheet.Cells[0, nColPos].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, 21].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 19].Value)) * 100;
                }
                else if (nColPos == 15)
                {
                    spdData.ActiveSheet.Cells[0, nColPos].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, 20].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 21].Value)) * 100;
                }
                else if (nColPos == 14)
                {
                    spdData.ActiveSheet.Cells[0, nColPos].Value = (Math.Round(Convert.ToDouble(spdData.ActiveSheet.Cells[0, 15].Value), 2) * Math.Round(Convert.ToDouble(spdData.ActiveSheet.Cells[0, 16].Value), 2)) / 100;
                }
                else if (nColPos == 25 || nColPos == 26 || nColPos == 51)
                {
                    spdData.ActiveSheet.Cells[0, nColPos].Value = totalSum / totalDivide; 
                }
                else
                {
                    spdData.ActiveSheet.Cells[0, nColPos].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, nColPos + 8].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 19].Value)) * 100;
                }
                //spdData.ActiveSheet.Cells[0, nColPos].Value = totalSum / totalDivide;            
                //spdData.ActiveSheet.Cells[0, nColPos].Value = totalSum*100;
                totalDivide = 0;
            }
            else
            {
                // subTotal이 하나도 없을때 즉 Raw Data만 있을때 GrandTotal 구함.
                if (divide != 0)
                {
                    if (nColPos == 18)
                    {
                        spdData.ActiveSheet.Cells[0, nColPos].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[0, 19].Value) - Convert.ToDouble(spdData.ActiveSheet.Cells[0, 36].Value)) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 19].Value)) * 100;
                    }
                    else if (nColPos == 17)
                    {
                        spdData.ActiveSheet.Cells[0, nColPos].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, 22].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 19].Value)) * 100;
                    }
                    else if (nColPos == 16)
                    {
                        spdData.ActiveSheet.Cells[0, nColPos].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, 21].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 19].Value)) * 100;
                    }
                    else if (nColPos == 15)
                    {
                        spdData.ActiveSheet.Cells[0, nColPos].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, 20].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 21].Value)) * 100;
                    }
                    else if (nColPos == 14)
                    {
                        spdData.ActiveSheet.Cells[0, nColPos].Value = (Math.Round(Convert.ToDouble(spdData.ActiveSheet.Cells[0, 15].Value), 2) * Math.Round(Convert.ToDouble(spdData.ActiveSheet.Cells[0, 16].Value), 2)) / 100;
                    }
                    else if (nColPos == 25 || nColPos == 26 || nColPos == 51)
                    {
                        spdData.ActiveSheet.Cells[0, nColPos].Value = sum / divide;
                    }
                    else
                    {
                        spdData.ActiveSheet.Cells[0, nColPos].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, nColPos + 8].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 19].Value)) * 100;
                    }

                    divide = 0;
                    //spdData.ActiveSheet.Cells[0, nColPos].Value = sum / divide;
                }
            }
            return;
        }

        #region 기타
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            //Add. 20150602
            if (cdvFactory.txtValue == "HMKB1")
            {
                BaseFormType = eBaseFormType.BOTH_BUMP;
                pnlBUMPDetail.Visible = false;
                pnlRASDetail.Visible = false;
            }
            else
            {
                BaseFormType = eBaseFormType.BOTH;
                pnlWIPDetail.Visible = false;
                pnlRASDetail.Visible = false;
            }

            udcRASCondition1.Text = "";
            udcRASCondition2.Text = "";
            udcRASCondition3.Text = "";
            udcRASCondition4.Text = "";
            udcRASCondition5.Text = "";
            udcRASCondition6.Text = "";
            udcRASCondition7.Text = "";

            this.SetFactory(cdvFactory.txtValue);
            cdvOper.sFactory = cdvFactory.txtValue;
        }

        private void ckbUPEH_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbUPEH.Checked == true)
            {
                cdvMonth.Visible = true;
            }
            else
            {
                cdvMonth.Visible = false;
            }
        }

        #endregion     

    }
}