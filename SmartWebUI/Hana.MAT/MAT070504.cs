using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI.Controls;

namespace Hana.MAT
{
    public partial class MAT070504 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        /// <summary>
        /// 클  래  스: MAT070504<br/>
        /// 클래스요약: SMT 외주 관리<br/>
        /// 작  성  자: 에이스텍 황종환<br/>
        /// 최초작성일: 2013-09-03<br/>
        /// 상세  설명: Dynamic TAT(요청자 : 임태성 과장)<br/>
        /// 변경  내용: <br/>        
        /// 2013-09-05-황종환 : 1. 그룹핑 항목에 ERP 플랜코드(SAPCODE) 추가, MAT_ID 삭제
        ///                     2. 팝업의 당주 잔량 로직 변경
        ///                        before : 당주 계획 - 토요일 이후 AO실적 - DA1차 이후 재공
        ///                        after  : 당주 계획 - 토요일 이후 AO실적
        /// 2013-09-06-황종환 : 임태성 과장 수정 요청
        ///                     1. 중앙 패널의 날짜를 선택할 날자로 표시되게 수정
        ///                     2. 주계획에 GUBUN = '3' 항목 추가
        ///                     3. 현장 재고 분리 - 현장  : SMT 재고수량은 제외한 모든 현장 재고 수량
        ///                                         SMT  : M0330 공정에 있는 재고 수량
        ///                     4. 월 기준 작업 완료량 Logic 변경
        ///                           현재 : 월누계 출하 + DA1차 이후 재공
        ///                           변경 : 월누계 출하 + DA1차 이후 재공 + 현장 재고
        ///                     5. 과거 날짜 기준 조회 시 재고현황이 항상 현재 시점으로 나오는 오류 수정
        /// 2013-09-09-황종환 : 임태성 과장 수정 요청
        ///                     1. DESCRIPTION이 다수 일 때 1개만 표시
        ///                        -> 제품코드별로 1개의 DESCRIPTION만 가져오게 변경하였습니다. 외주,PCB있을 땐 외주 우선 표시
        ///                     2. SAP CODE가 없거나 PMSE000%인 데이터 제외
        ///                        -> 해당 데이터 제외
        ///                     3. 자재 유형 PB체크를 BOM -> MWIPMATDEF 로 변경
        ///                     4. PKG CODE 그룹 항목 추가 (기본적으로 선택된 상태)
        ///                     5. 차주 불출 잔량 -> 일 필요 로직 변경
        ///                        -> 기존 로직은 유지하고 토,일요일일 경우 기존 로직 대신 무조건 /7을 하게 수정
        /// 2013-09-12-황종환 : 임태성 과장 수정 요청
        ///                     1. 당주+차주 PCB 부족 칸
        ///                        A. 당주+차주 PCB부족 칼럼명 변경 -> 당주+차주 PCB과부족
        ///                        B. 로직 변경 (당주계획+차주계획) – PCB 보유 총 수량 à PCB 보유 총수량 – (당주계획 + 차주계획)
        ///                     2. PCB 불출 실적칸은 삭제
        /// 2014-01-16-임종우 : TTL -> 외주 클릭시 팝업창 뜨도록 수정..소스상에 컬럼지정이 잘 못 되어 있음.. (임태성 요청)
        /// 2014-02-27-임종우 : 공정투입 대기 자재는 공정창고에 포함되도록 수정 (김권수D 요청)
        /// 2019-02-07-임종우 : 창고 재고 과거 데이터 테이블 변경 ZHMMT111@SAPREAL -> CWMSLOTSTS_BOH
        /// 2020-09-24-임종우 : V2 PCB Code 추가 - G16%
        /// </summary>
        /// 

        private string[] weekList = null;
        private string sSelectedDate = null;
        private string sHeaderPrevDay = null;
        private string sHeaderSelectedDay = null;
        private string sHeaderMonth = null;

        public MAT070504()
        {
            InitializeComponent();

            cdvDate.Value = DateTime.Now;

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.udcWIPCondition1.sFactory = GlobalVariable.gsAssyDefaultFactory;
            cdvFactory.Enabled = false;
            SortInit();
            GridColumnInit(); //헤더 한줄짜리
            SetMiddleDayString();
        }

        #region 중간 패널 현재 요일과 경과일수 표시
        private void SetMiddleDayString()
        {
            DateTime dateToday = cdvDate.Value;
            CultureInfo cultures = CultureInfo.CreateSpecificCulture("ko-KR");

            string strNow_DayOfWeek = dateToday.ToString(string.Format("ddd요일", cultures));

            labelToday.Text = strNow_DayOfWeek;
            switch (strNow_DayOfWeek)
            {
                case "월요일":
                    labelPastDay.Text = "0";
                    break;
                case "화요일":
                    labelPastDay.Text = "1";
                    break;
                case "수요일":
                    labelPastDay.Text = "2";
                    break;
                case "목요일":
                    labelPastDay.Text = "3";
                    break;
                case "금요일":
                    labelPastDay.Text = "4";
                    break;
                case "토요일":
                    labelPastDay.Text = "5";
                    break;
                case "일요일":
                    labelPastDay.Text = "6";
                    break;
            }

            // 선택한 날짜가 오늘인지 체크
            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                label11.Text = "Today";
            }
            else
            {
                label11.Text = "Select day";
            }
        }
        #endregion

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MATCODE", "MAT.MATCODE", "V_MAT.MATCODE", "V_MAT.MATCODE", "MATCODE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DESCRIPT", "MAT.DESCRIPT", "MAX(V_MAT.DESCRIPT) DESCRIPT", "' '", "DESCRIPT", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SAPCODE", "MAT.VENDOR_ID", "V_MAT.VENDOR_ID", "V_MAT.VENDOR_ID", "VENDOR_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "MAT.MAT_CMF_11", "V_MAT.MAT_CMF_11", "V_MAT.MAT_CMF_11", "MAT_CMF_11", true);
        }

        #endregion

        #region 한줄헤더생성

        /// ㄱ<summary>
        /// 한줄헤더생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {
            DataTable dt = null;

            weekList = new string[4];
            sSelectedDate = cdvDate.Value.ToString("yyyyMMdd");
            sHeaderPrevDay = cdvDate.Value.AddDays(-1).ToString("MM.dd");
            sHeaderSelectedDay = cdvDate.Value.ToString("MM.dd");
            sHeaderMonth = cdvDate.Value.ToString("MM");

            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT MAX((SELECT LPAD(PLAN_WEEK,2,'0') FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = TO_CHAR(TO_DATE('" + sSelectedDate + "', 'YYYYMMDD') - 7, 'YYYYMMDD'))) PREV_WEEK                \n");
            strSqlString.Append("     , MAX((SELECT LPAD(PLAN_WEEK,2,'0') FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = '" + sSelectedDate + "')) CUR_WEEK                  \n");
            strSqlString.Append("     , MAX((SELECT LPAD(PLAN_WEEK,2,'0') FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = TO_CHAR(TO_DATE('" + sSelectedDate + "', 'YYYYMMDD') + 7, 'YYYYMMDD'))) NEXT_WEEK                  \n");
            strSqlString.Append("     , MAX((SELECT LPAD(PLAN_WEEK,2,'0') FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = TO_CHAR(TO_DATE('" + sSelectedDate + "', 'YYYYMMDD') + 14, 'YYYYMMDD'))) NEXT_NEXT_WEEK                  \n");
            strSqlString.Append("FROM DUAL         \n");

            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());

            for (int i = 0; i <= 3; i++)
            {
                weekList[i] = dt.Rows[0][i].ToString();
            }

            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Material code", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Material name", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("SAP CODE", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PKG CODE", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);

            spdData.RPT_AddBasicColumn("" + weekList[1] + "주 SMT 입고 잔량", 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("" + weekList[2] + "주 불출 잔량", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("총량", 1, 5, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Daily requirement", 1, 6, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("" + weekList[2] + "주 입고 잔량", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("총량", 1, 7, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Daily requirement", 1, 8, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("" + weekList[3] + "주차", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Receiving required", 1, 9, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Daily requirement", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("SOP PLAN", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("" + weekList[1] + "주 잔량", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("" + weekList[2] + "주", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("" + weekList[3] + "주", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("Inventory status", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("TTL", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("현장", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("SMT", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Outsourcing stock", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Process warehouse", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("창고", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("" + weekList[1] + "주+" + weekList[2] + "주 PC과부족", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Remaining Order", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("M/Z", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("MZ Required / Day", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);

            // PCB 불출 실적은 화면에서 안보이게 설정한다.
            spdData.RPT_AddBasicColumn("PCB release performance", 0, 23, Visibles.False, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("" + sHeaderPrevDay + "", 1, 23, Visibles.False, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("" + sHeaderSelectedDay + "", 1, 24, Visibles.False, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("PCB warehousing performance", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("" + sHeaderPrevDay + "", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("" + sHeaderSelectedDay + "", 1, 26, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("" + sHeaderMonth + "Monthly plan", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Monthly plan", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Work Complete", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("SMT remaining", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("WIP after DA", 0, 30, Visibles.False, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Weekly AO performance", 0, 31, Visibles.False, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Month AO Performance", 0, 32, Visibles.False, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);

            spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 4, 2);

            spdData.RPT_MerageHeaderRowSpan(0, 20, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 21, 2);

            spdData.RPT_MerageHeaderColumnSpan(0, 5, 2);
            spdData.RPT_MerageHeaderColumnSpan(0, 7, 2);
            spdData.RPT_MerageHeaderColumnSpan(0, 9, 2);
            spdData.RPT_MerageHeaderColumnSpan(0, 11, 3);
            spdData.RPT_MerageHeaderColumnSpan(0, 13, 5);

            spdData.RPT_MerageHeaderColumnSpan(0, 23, 2);

            spdData.RPT_MerageHeaderColumnSpan(0, 25, 2);

            spdData.RPT_MerageHeaderColumnSpan(0, 27, 3);

            spdData.ActiveSheet.FrozenColumnCount = 4;

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
        }

        #endregion

        #region 조회

        /// <summary>
        /// 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            if (!CheckField()) return;

            SetMiddleDayString();
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

                spdData.RPT_AutoFit(false);

                //  1. column header
                for (int i = 0; i < 4; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LemonChiffon;
                }

                //발주잔량에서 입고계획이 있는 자재는 셀을 빨강으로..
                SetPlanCellColor();

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

        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {
            return true;
        }

        #endregion

        #region MakeSqlString

        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string QueryCond4 = null;
            string QueryCond1NotNull;
            string QueryCond2NotNull;
            string QueryCond3NotNull;
            string QueryCond4NotNull;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;
            QueryCond1NotNull = tableForm.SelectedValueToQuery; ;
            QueryCond2NotNull = tableForm.SelectedValue2ToQuery;
            QueryCond3NotNull = tableForm.SelectedValue3ToQuery;
            QueryCond4NotNull = tableForm.SelectedValue4ToQuery;

            // 시간 관련 셋팅
            string sToday = DateTime.Now.ToString("yyyyMMdd");
            Boolean isToday = false;

            // 선택한 날짜가 오늘인지 체크
            if (sToday == cdvDate.SelectedValue())
            {
                isToday = true;
            }
            else
            {
                isToday = false;
            }

            DateTime dt = new DateTime(cdvDate.Value.Year, cdvDate.Value.Month, 1);

            string sSelectedDate = cdvDate.Value.ToString("yyyyMMdd");

            // 쿼리
            strSqlString.Append("WITH V_CAL AS (         \n");
            strSqlString.Append("SELECT MIN(DAYSTR) START_DAY         \n");
            strSqlString.Append("     , MAX(DAYSTR) END_DAY         \n");
            strSqlString.Append("     , MAX((SELECT SYS_YEAR FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = '" + sSelectedDate + "' )) YEAR         \n");
            strSqlString.Append("     , MAX((SELECT SYS_MONTH FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = '" + sSelectedDate + "')) MONTH         \n");
            strSqlString.Append("     , MAX((SELECT PLAN_WEEK FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = '" + sSelectedDate + "')) CUR_WEEK         \n");
            strSqlString.Append("     , MAX((SELECT PLAN_WEEK FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = TO_CHAR(TO_DATE('" + sSelectedDate + "', 'YYYYMMDD') + 7, 'YYYYMMDD'))) NEXT_WEEK         \n");
            strSqlString.Append("     , MAX((SELECT TO_CHAR(TO_DATE('" + sSelectedDate + "', 'YYYYMMDD') - 1, 'YYYYMMDD') FROM DUAL)) PREV_DAY         \n");
            strSqlString.Append("     , '" + sSelectedDate + "' TODAY         \n");
            strSqlString.Append("     , MAX((SELECT SYS_YEAR||LPAD(PLAN_WEEK,2,'0') FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = '" + sSelectedDate + "')) YEAR_CUR_WEEK         \n");
            strSqlString.Append("     , MAX((SELECT SYS_YEAR||LPAD(PLAN_WEEK,2,'0') FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = TO_CHAR(TO_DATE('" + sSelectedDate + "', 'YYYYMMDD') + 7, 'YYYYMMDD'))) YEAR_NEXT_WEEK         \n");
            strSqlString.Append("     , MAX((SELECT SYS_YEAR||LPAD(PLAN_WEEK,2,'0') FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = TO_CHAR(TO_DATE('" + sSelectedDate + "', 'YYYYMMDD') + 14, 'YYYYMMDD'))) YEAR_NEXT_NEXT_WEEK         \n");
            strSqlString.Append("     , MAX(CASE WHEN GUBUN = 'MON' THEN START_T ELSE '' END) MONTH_START         \n");
            strSqlString.Append("     , MAX(CASE WHEN GUBUN = 'MON' THEN END_T ELSE '' END) MONTH_END         \n");
            strSqlString.Append("     , MAX(CASE WHEN GUBUN = 'WEEK' THEN START_T ELSE '' END) WEEK_START         \n");
            strSqlString.Append("     , MAX(CASE WHEN GUBUN = 'WEEK' THEN END_T ELSE '' END) WEEK_END         \n");
            strSqlString.Append("  FROM (SELECT 'MON' GUBUN, B.SYS_YEAR, B.SYS_MONTH, B.SYS_DAY, B.PLAN_WEEK, B.SYS_YEAR || LPAD(B.SYS_MONTH,2,'0') ||  LPAD(B.SYS_DAY,2,'0') AS DAYSTR, MIN(B.SYS_YEAR || LPAD(B.SYS_MONTH,2,'0') ||  LPAD(B.SYS_DAY,2,'0')) OVER() START_T, MAX(B.SYS_YEAR || LPAD(B.SYS_MONTH,2,'0') ||  LPAD(B.SYS_DAY,2,'0')) OVER() END_T         \n");
            strSqlString.Append("          FROM MWIPCALDEF A, MWIPCALDEF B         \n");
            strSqlString.Append("         WHERE A.CALENDAR_ID = 'OTD'         \n");
            strSqlString.Append("           AND A.SYS_DATE = '" + sSelectedDate + "'         \n");
            strSqlString.Append("           AND A.CALENDAR_ID = B.CALENDAR_ID         \n");
            strSqlString.Append("           AND A.SYS_YEAR = B.SYS_YEAR         \n");
            strSqlString.Append("           AND A.SYS_MONTH = B.SYS_MONTH         \n");
            strSqlString.Append("           AND A.SYS_DATE >= B.SYS_DATE         \n");
            strSqlString.Append("        UNION ALL         \n");
            strSqlString.Append("        SELECT 'WEEK', B.SYS_YEAR, B.SYS_MONTH, B.SYS_DAY, B.PLAN_WEEK, B.SYS_YEAR || LPAD(B.SYS_MONTH,2,'0') ||  LPAD(B.SYS_DAY,2,'0') AS DAYSTR, MIN(B.SYS_YEAR || LPAD(B.SYS_MONTH,2,'0') ||  LPAD(B.SYS_DAY,2,'0')) OVER(), MAX(B.SYS_YEAR || LPAD(B.SYS_MONTH,2,'0') ||  LPAD(B.SYS_DAY,2,'0')) OVER()         \n");
            strSqlString.Append("          FROM MWIPCALDEF A, MWIPCALDEF B         \n");
            strSqlString.Append("         WHERE A.CALENDAR_ID = 'OTD'         \n");
            strSqlString.Append("           AND A.SYS_DATE = '" + sSelectedDate + "'         \n");
            strSqlString.Append("           AND A.CALENDAR_ID = B.CALENDAR_ID         \n");
            strSqlString.Append("           AND A.SYS_YEAR = B.SYS_YEAR         \n");
            strSqlString.Append("           AND A.PLAN_WEEK = B.PLAN_WEEK         \n");
            strSqlString.Append("           AND A.SYS_DATE >= B.SYS_DATE)         \n");
            strSqlString.Append(")          \n");
            strSqlString.Append(", V_MAT AS (         \n");
            strSqlString.Append("SELECT DISTINCT REPLACE(BOM.MATCODE, '-O', '') AS MATCODE, MAX(DES.DESCRIPT) DESCRIPT/*, MAT.MAT_ID*/, MAT.VENDOR_ID, MAT.MAT_CMF_11, NVL(PROD.MAT_ID,MAT.MAT_ID) MAT_ID                                            \n");
            strSqlString.Append("  FROM MWIPMATDEF MAT                    \n");
            strSqlString.Append("     , (SELECT /*+ ORDERED */MATCODE, PARTNUMBER, RESV_FLAG_1                   \n");
            strSqlString.Append("          FROM CWIPBOMDEF B                  \n");
            strSqlString.Append("             , MWIPMATDEF M                  \n");
            strSqlString.Append("         WHERE B.MATCODE = M.MAT_ID                  \n");
            strSqlString.Append("           AND M.MAT_TYPE = 'PB'                  \n");
            strSqlString.Append("           AND B.RESV_FLAG_1 = 'Y'                  \n");
            strSqlString.Append("           AND B.DELFLAG = ' ') BOM                           \n");
            strSqlString.Append("     , (SELECT DISTINCT REPLACE(MATCODE, '-O', '') MATCODE, MAX(DESCRIPT) DESCRIPT                            \n");
            strSqlString.Append("          FROM CWIPBOMDEF                            \n");
            strSqlString.Append("         WHERE 1 = 1                           \n");
            strSqlString.Append("           AND RESV_FLAG_1 = 'Y'                                              \n");
            strSqlString.Append("           AND DELFLAG = ' '                           \n");
            strSqlString.Append("         GROUP BY REPLACE(MATCODE, '-O', '')) DES          \n");
            strSqlString.Append("     , HRTDMCPROUT@RPTTOMES PROD                                          \n");
            strSqlString.Append(" WHERE MAT.MAT_ID = BOM.PARTNUMBER                                             \n");
            strSqlString.Append("   AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'                                             \n");
            strSqlString.Append("   AND MAT.MAT_GRP_10 IN ('EMCP', 'EMMC')                                             \n");
            strSqlString.Append("   AND MAT.MAT_ID LIKE '" + txtSearchProduct.Text + "'        \n");
            strSqlString.Append("   AND MAT.VENDOR_ID <> ' '                           \n");
            strSqlString.Append("   AND MAT.VENDOR_ID NOT LIKE 'PMSE000%'              \n");
            strSqlString.Append("   AND REPLACE(BOM.MATCODE, '-O', '') = DES.MATCODE            \n");
            strSqlString.Append("   AND PROD.MAT_KEY(+) = MAT.MAT_ID                        \n");
            #region 상세조회
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
            #endregion
            strSqlString.Append(" GROUP BY REPLACE(BOM.MATCODE, '-O', ''), MAT.MAT_ID, MAT.VENDOR_ID, MAT.MAT_CMF_11, PROD.MAT_ID             \n");
            strSqlString.Append(")         \n");
            strSqlString.Append(", V_WEEK_PLAN AS (         \n");
            strSqlString.Append("SELECT MAT.MAT_ID                  \n");
            strSqlString.Append("     , SUM(CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN W0 / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE W0 END) AS W0                  \n");
            strSqlString.Append("     , SUM(CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN W1 / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE W1 END) AS W1                  \n");
            strSqlString.Append("     , SUM(CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN W2 / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE W2 END) AS W2                  \n");
            strSqlString.Append("  FROM MWIPMATDEF MAT                  \n");
            strSqlString.Append("     , (                  \n");
            strSqlString.Append("        SELECT MAT_ID                  \n");
            strSqlString.Append("             , SUM(DECODE(PLAN_WEEK, V_CAL.YEAR_CUR_WEEK, WW_QTY, 0)) AS W0                  \n");
            strSqlString.Append("             , SUM(DECODE(PLAN_WEEK, V_CAL.YEAR_NEXT_WEEK, WW_QTY, 0)) AS W1                  \n");
            strSqlString.Append("             , SUM(DECODE(PLAN_WEEK, V_CAL.YEAR_NEXT_NEXT_WEEK, WW_QTY, 0)) AS W2                  \n");
            strSqlString.Append("          FROM RWIPPLNWEK, V_CAL                  \n");
            strSqlString.Append("         WHERE 1=1                  \n");
            strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'                  \n");
            strSqlString.Append("           AND GUBUN = '3'         \n");
            strSqlString.Append("           AND PLAN_WEEK BETWEEN V_CAL.YEAR_CUR_WEEK AND V_CAL.YEAR_NEXT_NEXT_WEEK                  \n");
            strSqlString.Append("         GROUP BY MAT_ID                  \n");
            strSqlString.Append("       ) PLN                  \n");
            strSqlString.Append(" WHERE 1=1                  \n");
            strSqlString.Append("   AND MAT.MAT_ID = PLN.MAT_ID                  \n");
            strSqlString.Append("   AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'                  \n");
            strSqlString.Append("   AND MAT.DELETE_FLAG = ' '                  \n");
            strSqlString.Append("   AND MAT.MAT_TYPE = 'FG'                  \n");
            strSqlString.Append(" GROUP BY MAT.MAT_ID                 \n");
            strSqlString.Append(")         \n");
            strSqlString.Append(", V_WIP AS (         \n");
            strSqlString.Append("SELECT MAT_ID, DA1_AFTER_WIP AS TTL         \n");
            strSqlString.Append("  FROM (SELECT MAT_ID         \n");
            strSqlString.Append("             , SUM(CASE WHEN OPER BETWEEN 'A0402' AND 'AZ010' THEN QTY ELSE 0 END) DA1_AFTER_WIP         \n");
            strSqlString.Append("          FROM (         \n");
            strSqlString.Append("                SELECT MAT_ID, OPER, OPER_GRP_1, MAT_GRP_4, MAT_GRP_5         \n");
            strSqlString.Append("                     , CASE WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1) ELSE QTY_1 END AS QTY         \n");
            strSqlString.Append("                  FROM (         \n");
            strSqlString.Append("                        SELECT A.MAT_ID, A.OPER, B.OPER_GRP_1, C.MAT_GRP_4, C.MAT_GRP_5, QTY_1         \n");
            strSqlString.Append("                             , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') AND KEY_1 = A.MAT_ID) AS COMP_CNT          \n");
            if (isToday)
            {
                strSqlString.Append("                          FROM RWIPLOTSTS A          \n");
            }
            else
            {
                strSqlString.Append("                          FROM RWIPLOTSTS_BOH A, V_CAL         \n");
            }
            strSqlString.Append("                             , MWIPOPRDEF B         \n");
            strSqlString.Append("                             , MWIPMATDEF C         \n");
            strSqlString.Append("                         WHERE  1 = 1           \n");
            if (isToday)
            {
                ;
            }
            else
            {
                strSqlString.Append("                           AND A.CUTOFF_DT =  V_CAL.TODAY||'22'                \n");
            }
            strSqlString.Append("                           AND A.FACTORY = B.FACTORY          \n");
            strSqlString.Append("                           AND A.FACTORY = C.FACTORY          \n");
            strSqlString.Append("                           AND A.OPER = B.OPER         \n");
            strSqlString.Append("                           AND A.MAT_ID = C.MAT_ID         \n");
            strSqlString.Append("                           AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'          \n");
            strSqlString.Append("                           AND A.LOT_DEL_FLAG = ' '         \n");
            strSqlString.Append("                           AND A.LOT_TYPE = 'W'         \n");
            strSqlString.Append("                           AND A.LOT_CMF_5 LIKE 'P%'          \n");
            strSqlString.Append("                           AND (C.MAT_GRP_5 IN ('1st','Merge', '-') OR C.MAT_GRP_5 LIKE 'Middle%')         \n");
            strSqlString.Append("                           AND C.DELETE_FLAG = ' '          \n");
            strSqlString.Append("                       )         \n");
            strSqlString.Append("               )         \n");
            strSqlString.Append("         GROUP BY MAT_ID         \n");
            strSqlString.Append("       )                  \n");
            strSqlString.Append(")         \n");
            strSqlString.Append(", V_AO AS (         \n");
            strSqlString.Append("SELECT MAT_ID, SUM(WEEK_AO) WEEK_AO, SUM(MONTH_AO) MONTH_AO         \n");
            strSqlString.Append("  FROM (SELECT A.MAT_ID          \n");
            strSqlString.Append("             , SUM(SHIP_QTY_1) QTY         \n");
            strSqlString.Append("             , CASE WHEN WORK_DATE BETWEEN MAX(V_CAL.WEEK_START) AND MAX(V_CAL.WEEK_END) THEN SUM(SHIP_QTY_1)          \n");
            strSqlString.Append("                    ELSE 0 END AS WEEK_AO         \n");
            strSqlString.Append("             , CASE WHEN WORK_DATE BETWEEN MAX(V_CAL.MONTH_START) AND MAX(V_CAL.MONTH_END) THEN SUM(SHIP_QTY_1)          \n");
            strSqlString.Append("                    ELSE 0 END AS MONTH_AO                  \n");
            strSqlString.Append("         FROM (                  \n");
            strSqlString.Append("              SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3                         \n");
            strSqlString.Append("                   , SHIP_QTY_1 AS SHIP_QTY_1                  \n");
            strSqlString.Append("                   , SHIP_QTY_2 AS SHIP_QTY_2                  \n");
            strSqlString.Append("                FROM (                  \n");
            strSqlString.Append("                     SELECT CM_KEY_1 AS FACTORY, MAT_ID                  \n");
            strSqlString.Append("                          , DECODE(CM_KEY_1,'" + GlobalVariable.gsAssyDefaultFactory + "','AZ010','" + GlobalVariable.gsTestDefaultFactory + "','TZ010','FGS','F0000','HMKE1','EZ010','HMKS1','SZ010') OPER                  \n");
            strSqlString.Append("                          , LOT_TYPE, MAT_VER,  WORK_DATE,CM_KEY_3                        \n");
            strSqlString.Append("                         , S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1 SHIP_QTY_1             \n");
            strSqlString.Append("                         , S1_FAC_OUT_QTY_2+S2_FAC_OUT_QTY_2+S3_FAC_OUT_QTY_2 SHIP_QTY_2              \n");
            strSqlString.Append("                       FROM RSUMFACMOV                   \n");
            strSqlString.Append("                      WHERE FACTORY NOT IN ('RETURN')                       \n");
            strSqlString.Append("                     )                        \n");
            strSqlString.Append("              )A                  \n");
            strSqlString.Append("              , MWIPMATDEF B         \n");
            strSqlString.Append("              , V_CAL                 \n");
            strSqlString.Append("         WHERE 1=1                   \n");
            strSqlString.Append("           AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'                   \n");
            strSqlString.Append("           AND A.FACTORY = B.FACTORY                   \n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID                                  \n");
            strSqlString.Append("           AND A.MAT_VER = 1                   \n");
            strSqlString.Append("           AND B.MAT_VER = 1                   \n");
            strSqlString.Append("           AND B.MAT_TYPE = 'FG'                   \n");
            strSqlString.Append("           AND A.OPER IN ('AZ010')                  \n");
            strSqlString.Append("           AND A.MAT_ID LIKE '%'                    \n");
            strSqlString.Append("           AND A.OPER NOT IN ('00001','00002')                   \n");
            strSqlString.Append("           AND A.WORK_DATE BETWEEN V_CAL.START_DAY AND V_CAL.END_DAY               \n");
            strSqlString.Append("           AND B.MAT_GRP_3 <> '-'         \n");
            strSqlString.Append("           AND (B.MAT_GRP_5 IN ('1st','-','Merge')  OR B.MAT_GRP_5 LIKE 'Middle%')         \n");
            strSqlString.Append("         GROUP BY A.MAT_ID, WORK_DATE         \n");
            strSqlString.Append("     )         \n");
            strSqlString.Append(" GROUP BY MAT_ID         \n");
            strSqlString.Append(")         \n");
            strSqlString.Append(", V_STOCK AS (         \n");
            strSqlString.Append("SELECT REPLACE(A.MAT_ID,'-O','') AS MAT_ID         \n");
            strSqlString.Append("     , SUM(B.INV_QTY) AS \"창고재고\"         \n");
            strSqlString.Append("     , SUM(B.INV_L_QTY) AS \"공정창고\"         \n");
            strSqlString.Append("     , SUM(E.WIK_WIP) AS \"외주재고\"          \n");
            strSqlString.Append("     , SUM(C.QTY_TTL) AS \"현장재고\"         \n");
            strSqlString.Append("     , SUM(C.QTY_SMT) AS \"SMT\"         \n");
            strSqlString.Append("     , SUM(NVL(B.INV_QTY,0)) + SUM(NVL(B.INV_L_QTY,0)) + SUM(NVL(C.QTY_TTL,0)) + SUM(NVL(E.WIK_WIP,0)) AS \"재고합\"         \n");
            strSqlString.Append("  FROM MWIPMATDEF A          \n");
            strSqlString.Append("     , (          \n");
            strSqlString.Append("        SELECT MAT_ID          \n");
            strSqlString.Append("             , SUM(DECODE(STORAGE_LOCATION, '1000', QUANTITY, '1003', QUANTITY, 0)) AS INV_QTY            \n");
            strSqlString.Append("             , SUM(DECODE(STORAGE_LOCATION, '1001', QUANTITY, 0)) AS INV_L_QTY          \n");

            if (isToday)
            {                
                strSqlString.Append("          FROM CWMSLOTSTS@RPTTOMES          \n");
                strSqlString.Append("         WHERE 1=1          \n");                
            }
            else
            {
                strSqlString.Append("          FROM CWMSLOTSTS_BOH@RPTTOMES       \n");
                strSqlString.Append("         WHERE CUTOFF_DT = '" + sSelectedDate + "22'" + "\n"); 
            }

            strSqlString.Append("           AND QUANTITY> 0          \n");
            strSqlString.Append("           AND STORAGE_LOCATION IN ('1000', '1001', '1003')          \n");
            strSqlString.Append("         GROUP BY MAT_ID          \n");
            strSqlString.Append("         UNION ALL " + "\n");
            strSqlString.Append("        SELECT MAT_ID, 0 AS INV_QTY, SUM(QTY_1) AS INV_L_QTY " + "\n");
            strSqlString.Append("          FROM CWIPMATSLP@RPTTOMES " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND RECV_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND (MAT_ID LIKE 'R16%' OR MAT_ID LIKE 'G16%') " + "\n");
            strSqlString.Append("           AND IN_TIME BETWEEN '" + cdvDate.Value.AddDays(-2).ToString("yyyyMMdd") + "000000' AND '" + sSelectedDate + "235959' " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("       ) B          \n");
            strSqlString.Append("     , (          \n");
            strSqlString.Append("        SELECT MAT_ID         \n");
            strSqlString.Append("             , SUM(CASE WHEN A.LOT_CMF_9 = 'A' AND B.LOT_ID IS NULL THEN 1          \n");
            strSqlString.Append("                        ELSE 0          \n");
            strSqlString.Append("                   END) AS GRADE_A          \n");
            strSqlString.Append("             , SUM(CASE WHEN A.LOT_CMF_9 = 'B' AND B.LOT_ID IS NULL THEN 1          \n");
            strSqlString.Append("                        ELSE 0         \n");
            strSqlString.Append("                   END) AS GRADE_B          \n");
            strSqlString.Append("             , SUM(CASE WHEN B.LOT_ID IS NOT NULL THEN 1          \n");
            strSqlString.Append("                        ELSE 0          \n");
            strSqlString.Append("                   END) AS IN_QTY          \n");
            strSqlString.Append("             , COUNT(*) AS LOT_TTL          \n");
            strSqlString.Append("             , SUM(CASE WHEN A.OPER = 'M0330' THEN QTY_1         \n");
            strSqlString.Append("                        ELSE 0 END) QTY_SMT                   \n");
            strSqlString.Append("             , SUM(CASE WHEN A.OPER <> 'M0330' THEN QTY_1         \n");
            strSqlString.Append("                        ELSE 0 END) QTY_TTL           \n");

            if (isToday)
            {
                strSqlString.Append("          FROM RWIPLOTSTS A " + "\n");
                strSqlString.Append("             , CRASRESMAT B " + "\n");
                strSqlString.Append("         WHERE 1=1  " + "\n");
            }
            else
            {
                strSqlString.Append("          FROM RWIPLOTSTS_BOH A " + "\n");
                strSqlString.Append("             , CRASRESMAT B " + "\n");
                strSqlString.Append("         WHERE 1=1  " + "\n");
                strSqlString.Append("           AND A.CUTOFF_DT = '" + sSelectedDate + "' || '22' " + "\n");
            }
            strSqlString.Append("           AND A.FACTORY = B.FACTORY(+)          \n");
            strSqlString.Append("           AND A.LOT_ID = B.LOT_ID(+)          \n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'         \n");
            strSqlString.Append("           AND A.LOT_TYPE != 'W'         \n");
            strSqlString.Append("           AND A.LOT_DEL_FLAG = ' '         \n");
            strSqlString.Append("           AND A.LOT_CMF_2 = '-'          \n");
            strSqlString.Append("           AND A.LOT_CMF_9 != ' '          \n");
            strSqlString.Append("           AND A.QTY_1 > 0          \n");
            strSqlString.Append("           AND A.OPER NOT IN  ('00001', '00002', 'V0000')          \n");
            strSqlString.Append("         GROUP BY A.MAT_ID           \n");
            strSqlString.Append("       ) C          \n");
            strSqlString.Append("     , (          \n");
            strSqlString.Append("        SELECT MAT_ID, SUM(LOT_QTY) AS WIK_WIP          \n");
            strSqlString.Append("          FROM ISTMWIKWIP@RPTTOMES, V_CAL          \n");
            strSqlString.Append("         WHERE 1=1          \n");
            if (isToday)
            {
                strSqlString.Append("                   AND CUTOFF_DT = '" + sSelectedDate + "' || TO_CHAR(SYSDATE, 'HH24')" + "          \n");
            }
            else
            {
                strSqlString.Append("                   AND CUTOFF_DT = '" + sSelectedDate + "22'         \n");
            }
            strSqlString.Append("         GROUP BY MAT_ID          \n");
            strSqlString.Append("       ) E          \n");
            strSqlString.Append(" WHERE 1=1         \n");
            strSqlString.Append("   AND A.MAT_ID = B.MAT_ID(+)         \n");
            strSqlString.Append("   AND A.MAT_ID = C.MAT_ID(+)         \n");
            strSqlString.Append("   AND A.MAT_ID = E.MAT_ID(+)         \n");
            strSqlString.Append("   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'         \n");
            strSqlString.Append("   AND A.MAT_ID LIKE '%'         \n");
            strSqlString.Append(" GROUP BY REPLACE(A.MAT_ID,'-O','')         \n");
            strSqlString.Append(" HAVING SUM(NVL(B.INV_QTY,0)) + SUM(NVL(B.INV_L_QTY,0)) + SUM(NVL(C.LOT_TTL,0)) + SUM(NVL(E.WIK_WIP,0)) > 0         \n");
            strSqlString.Append(" ORDER BY REPLACE(A.MAT_ID,'-O','')         \n");
            strSqlString.Append(")         \n");
            strSqlString.Append(", V_PCB AS (         \n");
            strSqlString.Append("SELECT REPLACE(A.MAT_ID,'-O','')  AS MAT_ID         \n");
            strSqlString.Append("     , SUM(DECODE(WORK_DATE, V_CAL.PREV_DAY, END_QTY)) AS PREV_DAY         \n");
            strSqlString.Append("     , SUM(DECODE(WORK_DATE, V_CAL.TODAY, END_QTY)) AS CURRENT_DAY         \n");
            strSqlString.Append("  FROM (         \n");
            strSqlString.Append("        SELECT FACTORY, MAT_ID, OPER, WORK_DATE, CM_KEY_3         \n");
            strSqlString.Append("             , SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1) AS END_QTY         \n");
            strSqlString.Append("          FROM RSUMWIPMOV, V_CAL         \n");
            strSqlString.Append("         WHERE 1=1         \n");
            strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'         \n");
            strSqlString.Append("           AND WORK_DATE BETWEEN V_CAL.PREV_DAY AND V_CAL.TODAY         \n");
            strSqlString.Append("           AND (OPER LIKE 'V%' OR OPER LIKE 'M%')         \n");
            strSqlString.Append("         GROUP BY FACTORY, MAT_ID, OPER, WORK_DATE, CM_KEY_3          \n");
            strSqlString.Append("       ) A         \n");
            strSqlString.Append("     , MWIPMATDEF B         \n");
            strSqlString.Append("     , V_CAL         \n");
            strSqlString.Append(" WHERE 1=1         \n");
            strSqlString.Append("   AND A.FACTORY = B.FACTORY         \n");
            strSqlString.Append("   AND A.MAT_ID = B.MAT_ID         \n");
            strSqlString.Append("   AND A.END_QTY > 0         \n");
            strSqlString.Append("   AND A.OPER IN ('V0000')         \n");
            strSqlString.Append(" GROUP BY REPLACE(A.MAT_ID,'-O','')         \n");
            strSqlString.Append(")         \n");
            strSqlString.Append(", V_MON_PLN AS (         \n");
            strSqlString.Append("SELECT MAT_ID, RESV_FIELD3 SOP_PLN         \n");
            strSqlString.Append("  FROM CWIPPLNMON, V_CAL         \n");
            strSqlString.Append(" WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'         \n");
            strSqlString.Append("   AND PLAN_MONTH = V_CAL.YEAR || LPAD(V_CAL.MONTH, 2, '0')           \n");
            strSqlString.Append("   AND RESV_FIELD3 <> ' '          \n");
            strSqlString.Append(")         \n");
            strSqlString.Append(", V_ORDER AS (         \n");
            strSqlString.Append("SELECT REPLACE(SMM.MATCODE, '-O', '')    AS MATCODE         \n");
            strSqlString.Append("     , SUM(SMM.ORDER_QTY) AS ORDER_QTY          \n");
            strSqlString.Append("  FROM RSUMWIPMAT SMM         \n");
            strSqlString.Append(" GROUP BY REPLACE(SMM.MATCODE, '-O', '')         \n");
            strSqlString.Append(")         \n");
            strSqlString.Append("SELECT " + QueryCond1 + "         \n");
            if (ckbKpcs.Checked)
            {
                strSqlString.Append("     , ROUND((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" - BY_MATCODE.\"현장재고\")/1000,0) AS \"당주 SMT 잔량\"         \n");
                strSqlString.Append("     , ROUND((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" - (BY_MATCODE.\"현장재고\" + BY_MATCODE.\"외주재고\"))/1000,0) AS \"차주 불출 잔량\"         \n");
                if (isToday)
                {
                    if (labelToday.Text == "토요일" || labelToday.Text == "일요일")
                    {
                        strSqlString.Append("     , ROUND(((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" - (BY_MATCODE.\"현장재고\" + BY_MATCODE.\"외주재고\")) / 7 )/1000,0)  AS \"차주 일필요\"         \n");
                    }
                    else
                    {
                        strSqlString.Append("     , ROUND(((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" - (BY_MATCODE.\"현장재고\" + BY_MATCODE.\"외주재고\")) / (7 - (SYSDATE - (TO_DATE(TO_CHAR(NEXT_DAY(SYSDATE,2) - 7, 'YYYYMMDD')||'220000', 'YYYYMMDDHH24MISS'))) - 1))/1000,0)  AS \"차주 일필요\"         \n");
                    }
                }
                else
                {
                    if (labelToday.Text == "토요일" || labelToday.Text == "일요일")
                    {
                        strSqlString.Append("     , ROUND(((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" - (BY_MATCODE.\"현장재고\" + BY_MATCODE.\"외주재고\")) / 7 )/1000,0)  AS \"차주 일필요\"         \n");
                    }
                    else
                    {
                        strSqlString.Append("     , ROUND(((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" - (BY_MATCODE.\"현장재고\" + BY_MATCODE.\"외주재고\")) / NULLIF((7 - (TO_DATE('" + sSelectedDate + "220000', 'YYYYMMDDHH24MISS') - (NEXT_DAY(TO_DATE('" + sSelectedDate + "220000', 'YYYYMMDDHH24MISS'),2) - 7 )) - 1),0))/1000,0)  AS \"차주 일필요\"         \n");
                    }
                }
                strSqlString.Append("     , ROUND(((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\") + MAT.\"SOP 차주 계획\" - BY_MATCODE.\"현장재고\")/1000,0) AS \"차주 입고 총량\"         \n");
                if (isToday)
                {
                    if (labelToday.Text == "토요일" || labelToday.Text == "일요일")
                    {
                        strSqlString.Append("     , ROUND(((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" -(BY_MATCODE.\"현장재고\")) / 7 )/1000,0)  AS \"차주 입고 일필요\"         \n");
                    }
                    else
                    {
                        strSqlString.Append("     , ROUND(((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" -(BY_MATCODE.\"현장재고\")) / (7 - (SYSDATE - (TO_DATE(TO_CHAR(NEXT_DAY(SYSDATE,2) - 7, 'YYYYMMDD')||'220000', 'YYYYMMDDHH24MISS'))) ))/1000,0)  AS \"차주 입고 일필요\"         \n");
                    }
                }
                else
                {
                    if (labelToday.Text == "토요일" || labelToday.Text == "일요일")
                    {
                        strSqlString.Append("     , ROUND(((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" -(BY_MATCODE.\"현장재고\")) / 7 )/1000,0)  AS \"차주 입고 일필요\"         \n");
                    }
                    else
                    {
                        strSqlString.Append("     , ROUND(((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" -(BY_MATCODE.\"현장재고\")) / NULLIF((7 - (TO_DATE('" + sSelectedDate + "220000', 'YYYYMMDDHH24MISS') - (NEXT_DAY(TO_DATE('" + sSelectedDate + "220000', 'YYYYMMDDHH24MISS'),2) - 7 )) ),0))/1000,0)  AS \"차주 입고 일필요\"         \n");
                    }
                }
                strSqlString.Append("     , ROUND(((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\") + MAT.\"SOP 차주 계획\" - BY_MATCODE.\"현장재고\" + MAT.\"SOP 차차주 계획\")/1000,0) AS \"차차주 입고 필요량\"         \n");
                if (isToday)
                {
                    if (labelToday.Text == "토요일" || labelToday.Text == "일요일")
                    {
                        strSqlString.Append("     , ROUND(((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" -(BY_MATCODE.\"현장재고\") + MAT.\"SOP 차차주 계획\") / 14 )/1000,0)  AS \"차차주 일필요량\"         \n");
                    }
                    else
                    {
                        strSqlString.Append("     , ROUND(((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" -(BY_MATCODE.\"현장재고\") + MAT.\"SOP 차차주 계획\") / (14 - (SYSDATE - (TO_DATE(TO_CHAR(NEXT_DAY(SYSDATE,2) - 7, 'YYYYMMDD')||'220000', 'YYYYMMDDHH24MISS'))) ))/1000,0)  AS \"차차주 일필요량\"         \n");
                    }
                }
                else
                {
                    if (labelToday.Text == "토요일" || labelToday.Text == "일요일")
                    {
                        strSqlString.Append("     , ROUND(((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" -(BY_MATCODE.\"현장재고\") + MAT.\"SOP 차차주 계획\") / 14 )/1000,0)  AS \"차차주 일필요량\"         \n");
                    }
                    else
                    {
                        strSqlString.Append("     , ROUND(((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" -(BY_MATCODE.\"현장재고\") + MAT.\"SOP 차차주 계획\") / NULLIF((14 - (TO_DATE('" + sSelectedDate + "220000', 'YYYYMMDDHH24MISS') - (NEXT_DAY(TO_DATE('" + sSelectedDate + "220000', 'YYYYMMDDHH24MISS'),2) - 7 )) ),0))/1000,0)  AS \"차차주 일필요량\"         \n");
                    }
                }
                strSqlString.Append("     , ROUND((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\")/1000,0) AS \"SOP 당주 잔량\"         \n");
                strSqlString.Append("     , ROUND((MAT.\"SOP 차주 계획\")/1000,0) AS \"SOP 차주 계획\"         \n");
                strSqlString.Append("     , ROUND((MAT.\"SOP 차차주 계획\")/1000,0) AS \"SOP 차차주 계획\"         \n");
                strSqlString.Append("     , ROUND((BY_MATCODE.TTL)/1000,0) AS TTL         \n");
                strSqlString.Append("     , ROUND((BY_MATCODE.\"현장재고\")/1000,0) AS \"현장재고\"         \n");
                strSqlString.Append("     , ROUND((BY_MATCODE.\"SMT\")/1000,0) AS \"SMT\"         \n");
                strSqlString.Append("     , ROUND((BY_MATCODE.\"외주재고\")/1000,0) AS \"외주재고\"         \n");
                strSqlString.Append("     , ROUND((BY_MATCODE.\"공정창고\")/1000,0) AS \"공정창고\"          \n");
                strSqlString.Append("     , ROUND((BY_MATCODE.\"창고재고\")/1000,0) AS \"창고재고\"          \n");
                strSqlString.Append("     , ROUND((BY_MATCODE.TTL - (MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\"))/1000,0) AS \"당주+차주 PCB 부족\"         \n");
                strSqlString.Append("     , ROUND((BY_MATCODE.\"발주잔량\")/1000,0) AS \"발주잔량\"         \n");
                strSqlString.Append("     , 0 AS \"MZ 소요량\"         \n");
                strSqlString.Append("     , 0 AS \"전일 PCB 불출실적\"         \n");
                strSqlString.Append("     , 0 AS \"당일 PCB 불출실적\"         \n");
                strSqlString.Append("     , ROUND((BY_MATCODE.\"전일 PCB 입고 실적\")/1000,0) AS \"전일 PCB 입고 실적\"         \n");
                strSqlString.Append("     , ROUND((BY_MATCODE.\"당일 PCB 입고 실적\")/1000,0) AS \"당일 PCB 입고 실적\"         \n");
                strSqlString.Append("     , ROUND((MAT.\"월계획\")/1000,0) AS \"월계획\"         \n");
                strSqlString.Append("     , ROUND((MAT.\"달AO실적\"+MAT.\"DA이후재공\"+BY_MATCODE.\"현장재고\")/1000,0) AS \"작업 완료\"         \n");
                strSqlString.Append("     , ROUND((MAT.\"월계획\" - (MAT.\"달AO실적\"+MAT.\"DA이후재공\"+BY_MATCODE.\"현장재고\"))/1000,0) AS \"SMT 잔량\"              \n");
                strSqlString.Append("     , ROUND((MAT.\"DA이후재공\")/1000,0) AS \"DA이후재공\"         \n");
                strSqlString.Append("     , ROUND((MAT.\"주AO실적\")/1000,0) AS \"주AO실적\"          \n");
                strSqlString.Append("     , ROUND((MAT.\"달AO실적\")/1000,0) AS \"달AO실적\"          \n");
            }
            else
            {
                strSqlString.Append("     , ROUND(MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" - BY_MATCODE.\"현장재고\",0) AS \"당주 SMT 잔량\"         \n");
                strSqlString.Append("     , ROUND(MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" - (BY_MATCODE.\"현장재고\" + BY_MATCODE.\"외주재고\"),0) AS \"차주 불출 잔량\"         \n");
                if (isToday)
                {
                    if (labelToday.Text == "토요일" || labelToday.Text == "일요일")
                    {
                        strSqlString.Append("     , ROUND((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" - (BY_MATCODE.\"현장재고\" + BY_MATCODE.\"외주재고\")) / 7 ,0)  AS \"차주 일필요\"         \n");
                    }
                    else
                    {
                        strSqlString.Append("     , ROUND((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" - (BY_MATCODE.\"현장재고\" + BY_MATCODE.\"외주재고\")) / (7 - (SYSDATE - (TO_DATE(TO_CHAR(NEXT_DAY(SYSDATE,2) - 7, 'YYYYMMDD')||'220000', 'YYYYMMDDHH24MISS'))) - 1),0)  AS \"차주 일필요\"         \n");
                    }
                }
                else
                {
                    if (labelToday.Text == "토요일" || labelToday.Text == "일요일")
                    {
                        strSqlString.Append("     , ROUND((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" - (BY_MATCODE.\"현장재고\" + BY_MATCODE.\"외주재고\")) / 7 ,0)  AS \"차주 일필요\"         \n");
                    }
                    else
                    {
                        strSqlString.Append("     , ROUND((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" - (BY_MATCODE.\"현장재고\" + BY_MATCODE.\"외주재고\")) / NULLIF((7 - (TO_DATE('" + sSelectedDate + "220000', 'YYYYMMDDHH24MISS') - (NEXT_DAY(TO_DATE('" + sSelectedDate + "220000', 'YYYYMMDDHH24MISS'),2) - 7 )) - 1),0),0)  AS \"차주 일필요\"         \n");
                    }
                }
                strSqlString.Append("     , ROUND((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\") + MAT.\"SOP 차주 계획\" - BY_MATCODE.\"현장재고\",0) AS \"차주 입고 총량\"         \n");
                if (isToday)
                {
                    if (labelToday.Text == "토요일" || labelToday.Text == "일요일")
                    {
                        strSqlString.Append("     , ROUND((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" -(BY_MATCODE.\"현장재고\")) / 7 ,0)  AS \"차주 입고 일필요\"         \n");
                    }
                    else
                    {
                        strSqlString.Append("     , ROUND((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" -(BY_MATCODE.\"현장재고\")) / (7 - (SYSDATE - (TO_DATE(TO_CHAR(NEXT_DAY(SYSDATE,2) - 7, 'YYYYMMDD')||'220000', 'YYYYMMDDHH24MISS'))) ),0)  AS \"차주 입고 일필요\"         \n");
                    }
                }
                else
                {
                    if (labelToday.Text == "토요일" || labelToday.Text == "일요일")
                    {
                        strSqlString.Append("     , ROUND((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" -(BY_MATCODE.\"현장재고\")) / 7 ,0)  AS \"차주 입고 일필요\"         \n");
                    }
                    else
                    {
                        strSqlString.Append("     , ROUND((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" -(BY_MATCODE.\"현장재고\")) / NULLIF((7 - (TO_DATE('" + sSelectedDate + "220000', 'YYYYMMDDHH24MISS') - (NEXT_DAY(TO_DATE('" + sSelectedDate + "220000', 'YYYYMMDDHH24MISS'),2) - 7 )) ),0),0)  AS \"차주 입고 일필요\"         \n");
                    }
                }
                strSqlString.Append("     , ROUND(((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\") + MAT.\"SOP 차주 계획\" - BY_MATCODE.\"현장재고\" + MAT.\"SOP 차차주 계획\")/1,0) AS \"차차주 입고 필요량\"         \n");
                if (isToday)
                {
                    if (labelToday.Text == "토요일" || labelToday.Text == "일요일")
                    {
                        strSqlString.Append("     , ROUND(((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" -(BY_MATCODE.\"현장재고\") + MAT.\"SOP 차차주 계획\") / 14 )/1,0)  AS \"차차주 일필요량\"         \n");
                    }
                    else
                    {
                        strSqlString.Append("     , ROUND(((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" -(BY_MATCODE.\"현장재고\") + MAT.\"SOP 차차주 계획\") / (14 - (SYSDATE - (TO_DATE(TO_CHAR(NEXT_DAY(SYSDATE,2) - 7, 'YYYYMMDD')||'220000', 'YYYYMMDDHH24MISS'))) ))/1,0)  AS \"차차주 일필요량\"         \n");
                    }
                }
                else
                {
                    if (labelToday.Text == "토요일" || labelToday.Text == "일요일")
                    {

                        strSqlString.Append("     , ROUND(((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" -(BY_MATCODE.\"현장재고\") + MAT.\"SOP 차차주 계획\") / 14 )/1,0)  AS \"차차주 일필요량\"         \n");
                    }
                    else
                    {
                        strSqlString.Append("     , ROUND(((MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\" -(BY_MATCODE.\"현장재고\") + MAT.\"SOP 차차주 계획\") / NULLIF((14 - (TO_DATE('" + sSelectedDate + "220000', 'YYYYMMDDHH24MISS') - (NEXT_DAY(TO_DATE('" + sSelectedDate + "220000', 'YYYYMMDDHH24MISS'),2) - 7 )) ),0))/1,0)  AS \"차차주 일필요량\"         \n");
                    }
                }
                strSqlString.Append("     , ROUND(MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\",0) AS \"SOP 당주 잔량\"         \n");
                strSqlString.Append("     , ROUND(MAT.\"SOP 차주 계획\",0) AS \"SOP 차주 계획\"         \n");
                strSqlString.Append("     , ROUND(MAT.\"SOP 차차주 계획\",0) AS \"SOP 차차주 계획\"         \n");
                strSqlString.Append("     , ROUND(BY_MATCODE.TTL,0) AS TTL         \n");
                strSqlString.Append("     , ROUND(BY_MATCODE.\"현장재고\",0) AS \"현장재고\"         \n");
                strSqlString.Append("     , ROUND(BY_MATCODE.\"SMT\",0) AS \"SMT\"         \n");
                strSqlString.Append("     , ROUND(BY_MATCODE.\"외주재고\",0) AS \"외주재고\"         \n");
                strSqlString.Append("     , ROUND(BY_MATCODE.\"공정창고\",0) AS \"공정창고\"          \n");
                strSqlString.Append("     , ROUND(BY_MATCODE.\"창고재고\",0) AS \"창고재고\"          \n");
                strSqlString.Append("     , (BY_MATCODE.TTL - (MAT.\"SOP 당주 계획\" - MAT.\"주AO실적\" - MAT.\"DA이후재공\" + MAT.\"SOP 차주 계획\")) AS \"당주+차주 PCB 부족\"         \n");
                strSqlString.Append("     , ROUND(BY_MATCODE.\"발주잔량\",0) AS \"발주잔량\"         \n");
                strSqlString.Append("     , 0 AS \"MZ 소요량\"         \n");
                strSqlString.Append("     , 0 AS \"전일 PCB 불출실적\"         \n");
                strSqlString.Append("     , 0 AS \"당일 PCB 불출실적\"         \n");
                strSqlString.Append("     , ROUND(BY_MATCODE.\"전일 PCB 입고 실적\",0) AS \"전일 PCB 입고 실적\"         \n");
                strSqlString.Append("     , ROUND(BY_MATCODE.\"당일 PCB 입고 실적\",0) AS \"당일 PCB 입고 실적\"         \n");
                strSqlString.Append("     , ROUND(MAT.\"월계획\",0) AS \"월계획\"         \n");
                strSqlString.Append("     , ROUND(MAT.\"달AO실적\"+MAT.\"DA이후재공\"+BY_MATCODE.\"현장재고\",0) AS \"작업 완료\"         \n");
                strSqlString.Append("     , ROUND(MAT.\"월계획\" - (MAT.\"달AO실적\"+MAT.\"DA이후재공\"+BY_MATCODE.\"현장재고\") ,0) AS \"SMT 잔량\"              \n");
                strSqlString.Append("     , ROUND(MAT.\"DA이후재공\",1) AS \"DA이후재공\"         \n");
                strSqlString.Append("     , ROUND(MAT.\"주AO실적\",1) AS \"주AO실적\"          \n");
                strSqlString.Append("     , ROUND(MAT.\"달AO실적\",1) AS \"달AO실적\"          \n");
            }
            strSqlString.Append("  FROM (SELECT          \n");
            strSqlString.Append("               " + QueryCond2 + "          \n");
            strSqlString.Append("             , NVL(SUM(V_WEEK_PLAN.W0),0) AS \"SOP 당주 계획\"          \n");
            strSqlString.Append("             , NVL(SUM(V_WEEK_PLAN.W1),0) AS \"SOP 차주 계획\"         \n");
            strSqlString.Append("             , NVL(SUM(V_WEEK_PLAN.W2),0) AS \"SOP 차차주 계획\"         \n");
            strSqlString.Append("             , NVL(SUM(V_AO.WEEK_AO),0)   AS \"주AO실적\"         \n");
            strSqlString.Append("             , NVL(SUM(V_AO.MONTH_AO),0)  AS \"달AO실적\"         \n");
            strSqlString.Append("             , NVL(SUM(V_MON_PLN.SOP_PLN),0) AS \"월계획\"         \n");
            strSqlString.Append("             , NVL(SUM(V_AO.MONTH_AO + V_WIP.TTL),0) AS \"작업 완료\"         \n");
            strSqlString.Append("             , NVL(SUM(V_MON_PLN.SOP_PLN - (V_AO.MONTH_AO + V_WIP.TTL)),0) AS \"SMT 잔량\"         \n");
            strSqlString.Append("             , NVL(SUM(V_WIP.TTL),0) AS \"DA이후재공\"         \n");
            strSqlString.Append("          FROM V_MAT         \n");
            strSqlString.Append("             , V_WEEK_PLAN         \n");
            strSqlString.Append("             , V_WIP         \n");
            strSqlString.Append("             , V_AO         \n");
            strSqlString.Append("             , V_MON_PLN         \n");
            strSqlString.Append("         WHERE V_MAT.MAT_ID = V_WEEK_PLAN.MAT_ID(+)         \n");
            strSqlString.Append("           AND V_MAT.MAT_ID = V_WIP.MAT_ID(+)         \n");
            strSqlString.Append("           AND V_MAT.MAT_ID = V_AO.MAT_ID(+)         \n");
            strSqlString.Append("           AND V_MAT.MAT_ID = V_MON_PLN.MAT_ID(+)         \n");
            strSqlString.Append("         GROUP BY " + QueryCond3NotNull + "          \n");
            strSqlString.Append("       ) MAT         \n");
            strSqlString.Append("     , (SELECT V_STOCK.MAT_ID         \n");
            strSqlString.Append("             , NVL(V_STOCK.\"창고재고\",0) + NVL(V_STOCK.\"공정창고\",0) + NVL(V_STOCK.\"외주재고\",0)  + NVL(V_STOCK.\"현장재고\",0) + NVL(V_STOCK.\"SMT\",0) AS TTL         \n");
            strSqlString.Append("             , NVL(V_STOCK.\"현장재고\",0) AS \"현장재고\"         \n");
            strSqlString.Append("             , NVL(V_STOCK.\"SMT\",0) AS \"SMT\"         \n");
            strSqlString.Append("             , NVL(V_STOCK.\"외주재고\",0) AS \"외주재고\"         \n");
            strSqlString.Append("             , NVL(V_STOCK.\"공정창고\",0) AS \"공정창고\"         \n");
            strSqlString.Append("             , NVL(V_STOCK.\"창고재고\",0) AS \"창고재고\"         \n");
            strSqlString.Append("             , NVL(V_ORDER.ORDER_QTY, 0) AS \"발주잔량\"         \n");
            strSqlString.Append("             , NVL(V_PCB.PREV_DAY,0) AS \"전일 PCB 입고 실적\"         \n");
            strSqlString.Append("             , NVL(V_PCB.CURRENT_DAY,0) AS \"당일 PCB 입고 실적\"         \n");
            strSqlString.Append("          FROM V_STOCK         \n");
            strSqlString.Append("             , V_PCB         \n");
            strSqlString.Append("             , V_ORDER         \n");
            strSqlString.Append("         WHERE 1 = 1         \n");
            strSqlString.Append("           AND V_STOCK.MAT_ID = V_PCB.MAT_ID(+)         \n");
            strSqlString.Append("           AND V_STOCK.MAT_ID = V_ORDER.MATCODE(+)         \n");
            strSqlString.Append("       ) BY_MATCODE                    \n");
            strSqlString.Append(" WHERE MAT.MATCODE = BY_MATCODE.MAT_ID(+)                \n");
            strSqlString.Append("   AND MAT.MATCODE LIKE '" + txtMatCode.Text + "'         \n");
            strSqlString.Append(" ORDER BY 1,2,3,4                \n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region MakeSqlStringPopupMatCode
        private string MakeSqlStringPopupMatCode(string matCode)
        {
            StringBuilder strSqlString = new StringBuilder();

            // 시간 관련 셋팅
            string sToday = DateTime.Now.ToString("yyyyMMdd");
            Boolean isToday = false;

            // 선택한 날짜가 오늘인지 체크
            if (sToday == cdvDate.SelectedValue())
            {
                isToday = true;
            }
            else
            {
                isToday = false;
            }

            DateTime dt = new DateTime(cdvDate.Value.Year, cdvDate.Value.Month, 1);

            string sSelectedDate = cdvDate.Value.ToString("yyyyMMdd");

            // 쿼리
            strSqlString.Append("WITH V_CAL AS (         \n");
            strSqlString.Append("SELECT MIN(DAYSTR) START_DAY         \n");
            strSqlString.Append("     , MAX(DAYSTR) END_DAY         \n");
            strSqlString.Append("     , MAX((SELECT SYS_YEAR FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = '" + sSelectedDate + "' )) YEAR         \n");
            strSqlString.Append("     , MAX((SELECT SYS_MONTH FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = '" + sSelectedDate + "')) MONTH         \n");
            strSqlString.Append("     , MAX((SELECT PLAN_WEEK FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = '" + sSelectedDate + "')) CUR_WEEK         \n");
            strSqlString.Append("     , MAX((SELECT PLAN_WEEK FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = TO_CHAR(TO_DATE('" + sSelectedDate + "', 'YYYYMMDD') + 7, 'YYYYMMDD'))) NEXT_WEEK         \n");
            strSqlString.Append("     , MAX((SELECT TO_CHAR(TO_DATE('" + sSelectedDate + "', 'YYYYMMDD') - 1, 'YYYYMMDD') FROM DUAL)) PREV_DAY         \n");
            strSqlString.Append("     , '" + sSelectedDate + "' TODAY         \n");
            strSqlString.Append("     , MAX((SELECT SYS_YEAR||LPAD(PLAN_WEEK,2,'0') FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = '" + sSelectedDate + "')) YEAR_CUR_WEEK         \n");
            strSqlString.Append("     , MAX((SELECT SYS_YEAR||LPAD(PLAN_WEEK,2,'0') FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = TO_CHAR(TO_DATE('" + sSelectedDate + "', 'YYYYMMDD') + 7, 'YYYYMMDD'))) YEAR_NEXT_WEEK         \n");
            strSqlString.Append("     , MAX(CASE WHEN GUBUN = 'MON' THEN START_T ELSE '' END) MONTH_START         \n");
            strSqlString.Append("     , MAX(CASE WHEN GUBUN = 'MON' THEN END_T ELSE '' END) MONTH_END         \n");
            strSqlString.Append("     , MAX(CASE WHEN GUBUN = 'WEEK' THEN START_T ELSE '' END) WEEK_START         \n");
            strSqlString.Append("     , MAX(CASE WHEN GUBUN = 'WEEK' THEN END_T ELSE '' END) WEEK_END         \n");
            strSqlString.Append("  FROM (SELECT 'MON' GUBUN, B.SYS_YEAR, B.SYS_MONTH, B.SYS_DAY, B.PLAN_WEEK, B.SYS_YEAR || LPAD(B.SYS_MONTH,2,'0') ||  LPAD(B.SYS_DAY,2,'0') AS DAYSTR, MIN(B.SYS_YEAR || LPAD(B.SYS_MONTH,2,'0') ||  LPAD(B.SYS_DAY,2,'0')) OVER() START_T, MAX(B.SYS_YEAR || LPAD(B.SYS_MONTH,2,'0') ||  LPAD(B.SYS_DAY,2,'0')) OVER() END_T         \n");
            strSqlString.Append("          FROM MWIPCALDEF A, MWIPCALDEF B         \n");
            strSqlString.Append("         WHERE A.CALENDAR_ID = 'OTD'         \n");
            strSqlString.Append("           AND A.SYS_DATE = '" + sSelectedDate + "'         \n");
            strSqlString.Append("           AND A.CALENDAR_ID = B.CALENDAR_ID         \n");
            strSqlString.Append("           AND A.SYS_YEAR = B.SYS_YEAR         \n");
            strSqlString.Append("           AND A.SYS_MONTH = B.SYS_MONTH         \n");
            strSqlString.Append("           AND A.SYS_DATE >= B.SYS_DATE         \n");
            strSqlString.Append("        UNION ALL         \n");
            strSqlString.Append("        SELECT 'WEEK', B.SYS_YEAR, B.SYS_MONTH, B.SYS_DAY, B.PLAN_WEEK, B.SYS_YEAR || LPAD(B.SYS_MONTH,2,'0') ||  LPAD(B.SYS_DAY,2,'0') AS DAYSTR, MIN(B.SYS_YEAR || LPAD(B.SYS_MONTH,2,'0') ||  LPAD(B.SYS_DAY,2,'0')) OVER(), MAX(B.SYS_YEAR || LPAD(B.SYS_MONTH,2,'0') ||  LPAD(B.SYS_DAY,2,'0')) OVER()         \n");
            strSqlString.Append("          FROM MWIPCALDEF A, MWIPCALDEF B         \n");
            strSqlString.Append("         WHERE A.CALENDAR_ID = 'OTD'         \n");
            strSqlString.Append("           AND A.SYS_DATE = '" + sSelectedDate + "'         \n");
            strSqlString.Append("           AND A.CALENDAR_ID = B.CALENDAR_ID         \n");
            strSqlString.Append("           AND A.SYS_YEAR = B.SYS_YEAR         \n");
            strSqlString.Append("           AND A.PLAN_WEEK = B.PLAN_WEEK         \n");
            strSqlString.Append("           AND A.SYS_DATE >= B.SYS_DATE)         \n");
            strSqlString.Append(")          \n");
            strSqlString.Append(", V_AO AS (         \n");
            strSqlString.Append("SELECT MAT_ID, SUM(WEEK_AO) WEEK_AO, SUM(MONTH_AO) MONTH_AO         \n");
            strSqlString.Append("  FROM (SELECT B.MAT_ID         \n");
            strSqlString.Append("             , SUM(SHIP_QTY_1) QTY         \n");
            strSqlString.Append("             , CASE WHEN WORK_DATE BETWEEN MAX(V_CAL.WEEK_START) AND MAX(V_CAL.WEEK_END) THEN SUM(SHIP_QTY_1)          \n");
            strSqlString.Append("                    ELSE 0 END AS WEEK_AO         \n");
            strSqlString.Append("             , CASE WHEN WORK_DATE BETWEEN MAX(V_CAL.MONTH_START) AND MAX(V_CAL.MONTH_END) THEN SUM(SHIP_QTY_1)          \n");
            strSqlString.Append("                    ELSE 0 END AS MONTH_AO                  \n");
            strSqlString.Append("         FROM (                  \n");
            strSqlString.Append("              SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3                         \n");
            strSqlString.Append("                   , SHIP_QTY_1 AS SHIP_QTY_1                  \n");
            strSqlString.Append("                   , SHIP_QTY_2 AS SHIP_QTY_2                  \n");
            strSqlString.Append("                FROM (                  \n");
            strSqlString.Append("                     SELECT CM_KEY_1 AS FACTORY, MAT_ID                  \n");
            strSqlString.Append("                          , DECODE(CM_KEY_1,'" + GlobalVariable.gsAssyDefaultFactory + "','AZ010','" + GlobalVariable.gsTestDefaultFactory + "','TZ010','FGS','F0000','HMKE1','EZ010','HMKS1','SZ010') OPER                  \n");
            strSqlString.Append("                          , LOT_TYPE, MAT_VER,  WORK_DATE,CM_KEY_3                        \n");
            strSqlString.Append("                         , S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1 SHIP_QTY_1             \n");
            strSqlString.Append("                         , S1_FAC_OUT_QTY_2+S2_FAC_OUT_QTY_2+S3_FAC_OUT_QTY_2 SHIP_QTY_2              \n");
            strSqlString.Append("                       FROM RSUMFACMOV                   \n");
            strSqlString.Append("                      WHERE FACTORY NOT IN ('RETURN')                       \n");
            strSqlString.Append("                     )                        \n");
            strSqlString.Append("              )A                  \n");
            strSqlString.Append("              , MWIPMATDEF B         \n");
            strSqlString.Append("              , V_CAL                 \n");
            strSqlString.Append("         WHERE 1=1                   \n");
            strSqlString.Append("           AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'                   \n");
            strSqlString.Append("           AND A.FACTORY = B.FACTORY                   \n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID                                  \n");
            strSqlString.Append("           AND A.MAT_VER = 1                   \n");
            strSqlString.Append("           AND B.MAT_VER = 1                   \n");
            strSqlString.Append("           AND B.MAT_TYPE = 'FG'                   \n");
            strSqlString.Append("           AND A.OPER IN ('AZ010')                  \n");
            strSqlString.Append("           AND A.MAT_ID LIKE '%'                    \n");
            strSqlString.Append("           AND A.OPER NOT IN ('00001','00002')                   \n");
            strSqlString.Append("           AND A.WORK_DATE BETWEEN V_CAL.START_DAY AND V_CAL.END_DAY           \n");
            strSqlString.Append("           AND B.MAT_GRP_3 <> '-'         \n");
            strSqlString.Append("           AND (B.MAT_GRP_5 IN ('1st','-','Merge')  OR B.MAT_GRP_5 LIKE 'Middle%')         \n");
            strSqlString.Append("         GROUP BY B.MAT_ID, WORK_DATE         \n");
            strSqlString.Append("     )         \n");
            strSqlString.Append(" GROUP BY MAT_ID         \n");
            strSqlString.Append(")         \n");
            strSqlString.Append(", V_WIP AS (         \n");
            strSqlString.Append("SELECT MAT_ID, DA1_AFTER_WIP AS TTL         \n");
            strSqlString.Append("  FROM (SELECT MAT_ID         \n");
            strSqlString.Append("             , SUM(CASE WHEN OPER BETWEEN 'A0402' AND 'AZ010' THEN QTY ELSE 0 END) DA1_AFTER_WIP         \n");
            strSqlString.Append("          FROM (         \n");
            strSqlString.Append("                SELECT MAT_ID, OPER, OPER_GRP_1, MAT_GRP_4, MAT_GRP_5         \n");
            strSqlString.Append("                     , CASE WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1) ELSE QTY_1 END AS QTY         \n");
            strSqlString.Append("                  FROM (         \n");
            strSqlString.Append("                        SELECT A.MAT_ID, A.OPER, B.OPER_GRP_1, C.MAT_GRP_4, C.MAT_GRP_5, QTY_1         \n");
            strSqlString.Append("                             , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') AND KEY_1 = A.MAT_ID) AS COMP_CNT          \n");
            if (isToday)
            {
                strSqlString.Append("                          FROM RWIPLOTSTS A          \n");
            }
            else
            {
                strSqlString.Append("                          FROM RWIPLOTSTS_BOH A, V_CAL         \n");
            }
            strSqlString.Append("                             , MWIPOPRDEF B         \n");
            strSqlString.Append("                             , MWIPMATDEF C         \n");
            strSqlString.Append("                         WHERE  1 = 1           \n");
            if (isToday)
            {
                ;
            }
            else
            {
                strSqlString.Append("                           AND A.CUTOFF_DT =  V_CAL.TODAY||'22'                \n");
            }
            strSqlString.Append("                           AND A.FACTORY = B.FACTORY          \n");
            strSqlString.Append("                           AND A.FACTORY = C.FACTORY          \n");
            strSqlString.Append("                           AND A.OPER = B.OPER         \n");
            strSqlString.Append("                           AND A.MAT_ID = C.MAT_ID         \n");
            strSqlString.Append("                           AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'          \n");
            strSqlString.Append("                           AND A.LOT_DEL_FLAG = ' '         \n");
            strSqlString.Append("                           AND A.LOT_TYPE = 'W'         \n");
            strSqlString.Append("                           AND A.LOT_CMF_5 LIKE 'P%'          \n");
            strSqlString.Append("                           AND (C.MAT_GRP_5 IN ('1st','Merge', '-') OR C.MAT_GRP_5 LIKE 'Middle%')         \n");
            strSqlString.Append("                           AND C.DELETE_FLAG = ' '          \n");
            strSqlString.Append("                       )         \n");
            strSqlString.Append("               )         \n");
            strSqlString.Append("         GROUP BY MAT_ID         \n");
            strSqlString.Append("       )                  \n");

            strSqlString.Append(")         \n");
            strSqlString.Append(", V_WEEK_PLAN AS (         \n");
            strSqlString.Append("SELECT MAT.MAT_ID         \n");
            strSqlString.Append("     , SUM(CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN W0 / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE W0 END) AS W0         \n");
            strSqlString.Append("     , SUM(CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN W1 / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE W1 END) AS W1         \n");
            strSqlString.Append("  FROM MWIPMATDEF MAT         \n");
            strSqlString.Append("     , (         \n");
            strSqlString.Append("        SELECT MAT_ID         \n");
            strSqlString.Append("             , SUM(DECODE(PLAN_WEEK, V_CAL.YEAR_CUR_WEEK, WW_QTY, 0)) AS W0         \n");
            strSqlString.Append("             , SUM(DECODE(PLAN_WEEK, V_CAL.YEAR_NEXT_WEEK, WW_QTY, 0)) AS W1         \n");
            strSqlString.Append("          FROM RWIPPLNWEK, V_CAL         \n");
            strSqlString.Append("         WHERE 1=1         \n");
            strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'         \n");
            strSqlString.Append("           AND GUBUN = '3'         \n");
            strSqlString.Append("           AND PLAN_WEEK BETWEEN V_CAL.YEAR_CUR_WEEK AND V_CAL.YEAR_NEXT_WEEK         \n");
            strSqlString.Append("         GROUP BY MAT_ID         \n");
            strSqlString.Append("       ) PLN         \n");
            strSqlString.Append(" WHERE 1=1         \n");
            strSqlString.Append("   AND MAT.MAT_ID = PLN.MAT_ID         \n");
            strSqlString.Append("   AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'         \n");
            strSqlString.Append("   AND MAT.DELETE_FLAG = ' '         \n");
            strSqlString.Append("   AND MAT.MAT_TYPE = 'FG'         \n");
            strSqlString.Append(" GROUP BY MAT.MAT_ID         \n");
            strSqlString.Append(")         \n");
            strSqlString.Append("SELECT DECODE(MAT_GRP_10, 'Z TOTAL', 'TOTAL', MAT_GRP_10) MAT_GRP_10         \n");
            strSqlString.Append("     , DECODE(MAT_GRP_10, 'Z TOTAL', ' ', MAT_GRP_6)  MAT_GRP_6         \n");
            strSqlString.Append("     , DECODE(MAT_GRP_10, 'Z TOTAL', ' ', MAT_CMF_11) MAT_CMF_11         \n");
            if (ckbKpcs.Checked)
            {
                strSqlString.Append("     , ROUND(SUM(WIP)/1000,0)            \n");
                strSqlString.Append("     , ROUND(SUM(\"W0_REMAIN\")/1000,0)  \n");
                strSqlString.Append("     , ROUND(SUM(W0)/1000,0)             \n");
                strSqlString.Append("     , ROUND(SUM(W1)/1000,0)             \n");
            }
            else
            {
                strSqlString.Append("     , SUM(WIP)         \n");
                strSqlString.Append("     , SUM(\"W0_REMAIN\")         \n");
                strSqlString.Append("     , SUM(W0)         \n");
                strSqlString.Append("     , SUM(W1)         \n");
            }
            strSqlString.Append("  FROM (SELECT DISTINCT SEQ,         \n");
            strSqlString.Append("               DECODE(SEQ.SEQ, 2, 'Z TOTAL', MAT.MAT_GRP_10) MAT_GRP_10         \n");
            strSqlString.Append("             , MAT.MAT_GRP_6         \n");
            strSqlString.Append("             , MAT.MAT_CMF_11         \n");
            strSqlString.Append("             , ROUND(V_WIP.TTL,0) WIP         \n");
            strSqlString.Append("             , ROUND(NVL(V_WEEK_PLAN.W0,0) - NVL(V_AO.WEEK_AO,0) - NVL(V_WIP.TTL,0),0) AS \"W0_REMAIN\"         \n");
            strSqlString.Append("             , ROUND(V_WEEK_PLAN.W0,0) AS W0         \n");
            strSqlString.Append("             , ROUND(V_WEEK_PLAN.W1,0) AS W1         \n");
            strSqlString.Append("          FROM MWIPMATDEF MAT          \n");
            strSqlString.Append("             , (SELECT DISTINCT MATCODE, NVL(PROD.MAT_ID, PARTNUMBER) PARTNUMBER, RESV_FLAG_1, RESV_FIELD_2, DELFLAG         \n");
            strSqlString.Append("                  FROM CWIPBOMDEF BOM         \n");
            strSqlString.Append("                     , HRTDMCPROUT@RPTTOMES PROD         \n");
            strSqlString.Append("                 WHERE 1 = 1         \n");
            strSqlString.Append("                   AND BOM.PARTNUMBER = PROD.MAT_KEY) BOM            \n");
            strSqlString.Append("             , V_WIP         \n");
            strSqlString.Append("             , V_AO         \n");
            strSqlString.Append("             , V_WEEK_PLAN         \n");
            strSqlString.Append("             , (SELECT LEVEL AS SEQ FROM DUAL CONNECT BY LEVEL <= 2) SEQ         \n");
            strSqlString.Append("         WHERE MAT.MAT_ID = BOM.PARTNUMBER         \n");
            strSqlString.Append("           AND BOM.RESV_FLAG_1 = 'Y'          \n");
            strSqlString.Append("           AND DELFLAG = ' '         \n");
            strSqlString.Append("           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'         \n");
            strSqlString.Append("           AND REPLACE(BOM.MATCODE, '-O', '') LIKE '" + matCode + "'         \n");
            strSqlString.Append("           AND MAT.MAT_ID =  V_WIP.MAT_ID(+)           \n");
            strSqlString.Append("           AND MAT.MAT_ID = V_AO.MAT_ID(+)         \n");
            strSqlString.Append("           AND MAT.MAT_ID = V_WEEK_PLAN.MAT_ID(+) \n");
            strSqlString.Append("           AND BOM.RESV_FIELD_2 = 'PB'          \n");
            strSqlString.Append("           AND MAT.MAT_GRP_10 IN ('EMCP', 'EMMC')         \n");
            strSqlString.Append("           AND MAT.VENDOR_ID <> ' '                           \n");
            strSqlString.Append("           AND MAT.VENDOR_ID NOT LIKE 'PMSE000%'          \n");
            strSqlString.Append("           AND MAT.MAT_ID LIKE '" + txtSearchProduct.Text + "'        \n");

            #region 상세조회
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
            #endregion

            strSqlString.Append("       )                  \n");
            strSqlString.Append(" GROUP BY DECODE(MAT_GRP_10, 'Z TOTAL', 'TOTAL', MAT_GRP_10), DECODE(MAT_GRP_10, 'Z TOTAL', ' ', MAT_GRP_6) ,DECODE(MAT_GRP_10, 'Z TOTAL', ' ', MAT_CMF_11)         \n");
            strSqlString.Append(" ORDER BY DECODE(MAT_GRP_10, 'Z TOTAL', 'TOTAL', MAT_GRP_10), MAT_GRP_6, MAT_CMF_11         \n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion

        #region MakeSqlStringPopupWIKWIP
        private string MakeSqlStringPopupWIKWIP(string matCode)
        {
            StringBuilder strSqlString = new StringBuilder();

            // 시간 관련 셋팅
            string sToday = DateTime.Now.ToString("yyyyMMdd");
            Boolean isToday = false;

            // 선택한 날짜가 오늘인지 체크
            if (sToday == cdvDate.SelectedValue())
            {
                isToday = true;
            }
            else
            {
                isToday = false;
            }

            DateTime dt = new DateTime(cdvDate.Value.Year, cdvDate.Value.Month, 1);

            string sSelectedDate = cdvDate.Value.ToString("yyyyMMdd");

            // 쿼리
            if (ckbKpcs.Checked)
            {
                strSqlString.Append("SELECT ROUND(SUM(QTY)/1000,0)         \n");
                strSqlString.Append("     , ROUND(SUM(DECODE(OPER,'STOCK', QTY))/1000,0) AS STOCK         \n");
                strSqlString.Append("     , ROUND(SUM(DECODE(OPER,'WIP', QTY))/1000,0) AS WIP         \n");
                strSqlString.Append("     , ROUND(SUM(DECODE(OPER,'SHIP', QTY))/1000,0) AS STOCK         \n");
            }
            else
            {
                strSqlString.Append("SELECT SUM(QTY)         \n");
                strSqlString.Append("     , SUM(DECODE(OPER,'STOCK', QTY)) AS STOCK         \n");
                strSqlString.Append("     , SUM(DECODE(OPER,'WIP', QTY)) AS WIP         \n");
                strSqlString.Append("     , SUM(DECODE(OPER,'SHIP', QTY)) AS STOCK         \n");
            }
            strSqlString.Append("  FROM (SELECT OPER, SUM(QTY) QTY         \n");
            strSqlString.Append("          FROM ((SELECT 'WIP' AS OPER, '0' QTY FROM DUAL UNION ALL SELECT 'STOCK' AS OPER, '0' QTY FROM DUAL UNION ALL SELECT 'SHIP' AS OPER, '0' QTY FROM DUAL)         \n");
            strSqlString.Append("                UNION ALL            \n");
            strSqlString.Append("                SELECT A.OPER, A.LOT_QTY         \n");
            strSqlString.Append("                  FROM ISTMWIKWIP@RPTTOMES A         \n");
            strSqlString.Append("                 WHERE 1=1          \n");
            if (isToday)
            {
                strSqlString.Append("                   AND CUTOFF_DT = '" + sSelectedDate + "' || TO_CHAR(SYSDATE, 'HH24')" + "          \n");
            }
            else
            {
                strSqlString.Append("                   AND A.CUTOFF_DT = '" + sSelectedDate + "22'         \n");
            }
            strSqlString.Append("                   AND REPLACE(A.MAT_ID, '-O', '') = '" + matCode + "')         \n");
            strSqlString.Append("         GROUP BY OPER         \n");
            strSqlString.Append("        )         \n");


            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion

        #region MakeSqlStringPopupMatPlanDate
        private string MakeSqlStringPopupMatPlanDate(DateTime dtPlanDate)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT SYS_DATE " + "\n");
            strSqlString.Append("  FROM MWIPCALDEF " + "\n");
            strSqlString.Append(" WHERE CALENDAR_ID = 'OTD' " + "\n");
            strSqlString.Append("   AND (SYS_YEAR = '" + dtPlanDate.ToString("yyyy") + "' AND LPAD(SYS_MONTH,2,'0') = '" + dtPlanDate.ToString("MM") + "' OR SYS_YEAR = '" + dtPlanDate.AddMonths(1).ToString("yyyy") + "' AND LPAD(SYS_MONTH,2,'0') = '" + dtPlanDate.AddMonths(1).ToString("MM") + "') " + "\n");
            strSqlString.Append(" ORDER BY SYS_DATE ASC " + "\n");

            return strSqlString.ToString();
        }
        #endregion

        #region MakeSqlStringPopupMatPlan
        /// <summary>
        /// 입고 계획을 위한 쿼리 생성
        /// </summary>
        private string MakeSqlStringPopupMatPlan(DataTable dtDateList, DateTime dtPlandate, string strMatCode)
        {
            string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값

            if (ckbKpcs.Checked == true)
                sKpcsValue = "1000";
            else
                sKpcsValue = "1";

            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("WITH OUTPUT AS ( " + "\n");
            strSqlString.Append("                SELECT BU_DATE " + "\n");
            strSqlString.Append("                     , MAT_ID " + "\n");
            strSqlString.Append("                     , VENDOR_CODE " + "\n");
            strSqlString.Append("                     , SUM(NVL(QUANTITY,0)) AS QUANTITY " + "\n");
            strSqlString.Append("                 FROM CWMSLOTHIS@RPTTOMES " + "\n");
            strSqlString.Append("                WHERE MOVEMENT_CODE IN ('A01','A02','A31') " + "\n");
            strSqlString.Append("                  AND BU_DATE BETWEEN '" + dtDateList.Rows[0][0].ToString() + "' AND '" + DateTime.Now.ToString("yyyyMMdd") + "' " + "\n");
            strSqlString.Append("                  AND RESV_FIELD_3 = ' ' " + "\n");
            strSqlString.Append("                GROUP BY BU_DATE, MAT_ID, VENDOR_CODE " + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("   , PLAN AS ( " + "\n");
            strSqlString.Append("              SELECT FACTORY " + "\n");
            strSqlString.Append("                   , CUSTOMER " + "\n");
            strSqlString.Append("                   , MAT_ID " + "\n");
            strSqlString.Append("                   , PRODUCT " + "\n");
            strSqlString.Append("                   , PKG " + "\n");
            strSqlString.Append("                   , VENDOR_CODE " + "\n");
            strSqlString.Append("                   , SUPPLY " + "\n");
            strSqlString.Append("                   , RN AS DAY_SEQ " + "\n");
            strSqlString.Append("                   , TO_CHAR(TO_DATE(SUBSTR(PLAN_DATE,1,6)||'01', 'YYYYMMDD') + RN - 1, 'YYYYMMDD') AS PLAN_DATE " + "\n");
            strSqlString.Append("                   , CASE " + "\n");

            for (int i = 0; i < dtDateList.Rows.Count; i++)
            {
                strSqlString.Append(string.Format("            WHEN RN = {0}  THEN D_{0} " + "\n", i + 1, i + 1));
            }

            strSqlString.Append("       ELSE 0 END PLN_QTY " + "\n");
            strSqlString.Append("  FROM CMATPLNINP@RPTTOMES " + "\n");
            strSqlString.Append("     , (SELECT ROWNUM RN FROM DUAL CONNECT BY LEVEL <= 62) SEQ " + "\n");
            strSqlString.Append(" WHERE PLAN_DATE <> ' ' " + "\n");
            strSqlString.Append(") " + "\n");
            strSqlString.Append("SELECT \"코드\", \"업체\", GUBUN " + "\n");

            for (int i = 0; i < dtDateList.Rows.Count; i++)
            {
                strSqlString.Append(string.Format("                 , ROUND(SUM(\"{0}\")/" + sKpcsValue + ",0) AS D{1} " + "\n", dtDateList.Rows[i][0].ToString(), i + 1));
            }

            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT \"코드\" " + "\n");
            strSqlString.Append("             , \"업체\" " + "\n");
            strSqlString.Append("             , DECODE( RN, 1, '계획', 2, '실적', 3, '누계차') AS GUBUN " + "\n");

            for (int i = 0; i < dtDateList.Rows.Count; i++)
            {
                strSqlString.Append("             , CASE WHEN \"날짜\" = '" + dtDateList.Rows[i][0] + "' AND RN = 1 THEN \"계획수량\"  " + "\n");
                strSqlString.Append("                    WHEN \"날짜\" = '" + dtDateList.Rows[i][0] + "' AND RN = 2 THEN \"실적수량\"  " + "\n");
                strSqlString.Append("                    WHEN \"날짜\" = '" + dtDateList.Rows[i][0] + "' AND RN = 3 THEN \"누계차\" ELSE 0 END AS \"" + dtDateList.Rows[i][0] + "\" " + "\n");
            }

            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT PLAN.MAT_ID \"코드\" " + "\n");
            strSqlString.Append("                     , PLAN.SUPPLY \"업체\" " + "\n");
            strSqlString.Append("                     , PLAN.PLAN_DATE \"날짜\" " + "\n");
            strSqlString.Append("                     , PLAN.PLN_QTY \"계획수량\" " + "\n");
            strSqlString.Append("                     , OUTPUT.QUANTITY \"실적수량\" " + "\n");
            strSqlString.Append("                     , SUM(PLAN.PLN_QTY) OVER(PARTITION BY PLAN.MAT_ID, PLAN.SUPPLY ORDER BY PLAN.PLAN_DATE) CUM_SUM_PLAN " + "\n");
            strSqlString.Append("                     , SUM(NVL(OUTPUT.QUANTITY,0)) OVER(PARTITION BY PLAN.MAT_ID, PLAN.SUPPLY ORDER BY PLAN.PLAN_DATE) CUM_SUM_QUANTITY " + "\n");
            strSqlString.Append("                     , SUM(NVL(OUTPUT.QUANTITY,0)) OVER(PARTITION BY PLAN.MAT_ID, PLAN.SUPPLY ORDER BY PLAN.PLAN_DATE) - SUM(PLAN.PLN_QTY) OVER(PARTITION BY PLAN.MAT_ID, PLAN.SUPPLY ORDER BY PLAN.PLAN_DATE) \"누계차\" " + "\n");
            strSqlString.Append("                  FROM PLAN, OUTPUT " + "\n");
            strSqlString.Append("                 WHERE PLAN.MAT_ID = OUTPUT.MAT_ID(+) " + "\n");
            strSqlString.Append("                   AND PLAN.VENDOR_CODE = OUTPUT.VENDOR_CODE(+) " + "\n");
            strSqlString.Append("                   AND PLAN.PLAN_DATE = OUTPUT.BU_DATE(+) " + "\n");
            strSqlString.Append("                   AND PLAN.MAT_ID = '" + strMatCode + "' " + "\n");
            strSqlString.Append("                   AND PLAN_DATE BETWEEN '" + dtPlandate.ToString("yyyyMMdd") + "' AND '" + dtPlandate.AddDays(30).ToString("yyyyMMdd") + "' " + "\n");

            strSqlString.Append("               ) A " + "\n");
            strSqlString.Append("             , (SELECT ROWNUM RN FROM DUAL CONNECT BY LEVEL <= 3) SEQ " + "\n");
            strSqlString.Append("        ) " + "\n");
            strSqlString.Append(" GROUP BY GROUPING SETS ( (\"코드\", \"업체\", GUBUN), (GUBUN) ) " + "\n");
            strSqlString.Append(" ORDER BY DECODE(\"업체\", NULL, '1', \"업체\"), DECODE(GUBUN, '계획', 1, '실적', 2, '누계차',3, 4)         " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion

        #endregion

        #region Event Handler

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

        private void rbtBrief_CheckedChanged(object sender, EventArgs e)
        {
            //cdvStep.Visible = !(rbtBrief.Checked);
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            DataTable dtPop = null;
            DateTime dtp = new DateTime();
            string matCode;
            if (e.RowHeader || e.ColumnHeader) // 헤더 클릭 이벤트면 SKIP
            {
                return;
            }
            try
            {
                if (e.Row >= 0 && (e.Column == 0 || e.Column == 17 || e.Column == 21))
                {
                    matCode = spdData.Sheets[0].Cells[e.Row, 0].Text.Trim();

                    this.Refresh();
                    if (e.Column == 0) // MATCODE 클릭 시
                    {
                        LoadingPopUp.LoadIngPopUpShow(this);
                        dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringPopupMatCode(matCode));
                    }
                    else if (e.Column == 17) // 외주재고 클릭 시
                    {
                        if (!spdData.Sheets[0].Cells[e.Row, 17].Text.Trim().Equals("") && !spdData.Sheets[0].Cells[e.Row, 17].Text.Trim().Equals("0"))
                        {
                            LoadingPopUp.LoadIngPopUpShow(this);
                            dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringPopupWIKWIP(matCode));
                        }
                    }
                    //else if (e.Column == 17) // 당주 + 차주 PCB 부족 클릭 시
                    //{
                        //; // 2013-09-04 관련 데이터가 아직 setup안되어 있음. 차 후 데이터 세팅 후 작성
                    //}
                    else if (e.Column == 21) // 발주잔량 클릭 시
                    {
                        DataTable dtPlanDate = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", "SELECT MIN(PLAN_DATE) AS PLAN_DATE FROM CMATPLNINP@RPTTOMES WHERE MAT_ID = '" + matCode + "'");
                        string strPlanDate = "";

                        if (dtPlanDate.Rows.Count == 0 || dtPlanDate.Rows[0][0].ToString() == "")
                            strPlanDate = DateTime.Now.ToString("yyyyMM") + "01";
                        else
                            strPlanDate = dtPlanDate.Rows[0][0].ToString();

                        dtp = Convert.ToDateTime(strPlanDate.Substring(0, 4) + "-" + strPlanDate.Substring(4, 2) + "-" + strPlanDate.Substring(6, 2));
                        DataTable dtPlanList = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringPopupMatPlanDate(dtp));

                        dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringPopupMatPlan(dtPlanList, dtp, matCode));
                    }

                    if (dtPop != null && dtPop.Rows.Count > 0)
                    {
                        LoadingPopUp.LoadingPopUpHidden();
                        if (e.Column == 0) // MATCODE 클릭 시
                        {
                            System.Windows.Forms.Form frm = new MAT070504_P1(matCode, dtPop, weekList);
                            frm.ShowDialog();
                        }
                        else if (e.Column == 17) // 외주재고 클릭 시
                        {
                            if (!spdData.Sheets[0].Cells[e.Row, 17].Text.Trim().Equals("") && !spdData.Sheets[0].Cells[e.Row, 17].Text.Trim().Equals("0"))
                            {
                                System.Windows.Forms.Form frm = new MAT070504_P2(matCode, dtPop);
                                frm.ShowDialog();
                            }
                        }
                        //else if (e.Column == 17) // 당주 + 차주 PCB 부족 클릭 시
                        //{
                            //; // 2013-09-04 관련 데이터가 아직 setup안되어 있음. 차 후 데이터 세팅 후 작성
                        //}
                        else if (e.Column == 21) //발주잔량 클릭 시
                        {
                            System.Windows.Forms.Form frm = new MAT070503_P3("Warehousing plan", dtPop, matCode, dtp);
                            frm.ShowDialog();
                        }
                    }
                    else
                    {
                        LoadingPopUp.LoadingPopUpHidden();
                    }
                }
            }
            catch (Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(ex.Message); ;
            }
        }
        #endregion

        /// <summary>
        /// 원부자재 입고 계획이 있을 시 셀을 빨강으로..
        /// </summary>
        private void SetPlanCellColor()
        {
            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", "SELECT DISTINCT MAT_ID FROM CMATPLNINP@RPTTOMES WHERE PLAN_DATE <> ' '");

            int matcodeIdx = 0;

            for (int i = 0; i < spdData.ActiveSheet.Columns.Count; i++)
            {
                if ("MATCODE" == spdData.ActiveSheet.GetColumnLabel(0, i))
                {
                    matcodeIdx = i;
                    break;
                }
                else
                {
                    matcodeIdx = -1;
                }
            }

            if (matcodeIdx != -1)
            {
                for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    string matCode = Convert.ToString(spdData.ActiveSheet.Cells[i, matcodeIdx].Value);

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (matCode == dt.Rows[j][0].ToString())
                        {
                            spdData.ActiveSheet.Cells[i, 21].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(100)), ((System.Byte)(100)));
                            break;
                        }
                    }
                }
            }


        }
    }
}

