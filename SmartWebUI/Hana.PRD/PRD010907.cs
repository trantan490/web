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
    public partial class PRD010907 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {        
        private DataTable dtWeek1 = new DataTable();
        private DataTable dtWeek2 = new DataTable();
        /// <summary>
        /// 클  래  스: PRD010907<br/>
        /// 클래스요약: PLAN 조회<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2013-07-22<br/>
        /// 상세  설명: PLAN 조회<br/>
        /// 변경  내용: <br/> 
        /// 2013-07-31-임종우 : 월간 조회 시 미래 조회 가능하도록 수정
        /// 2013-08-03-임종우 : 매출 환산, Wire Count 환산, Chip 환산 로직 추가 (임태성 요청)
        /// 2013-09-09-임종우 : 주간 실적 표시 (김권수 요청)
        /// 2013-09-11-임종우 : 업체 Type, SAP CODE 추가 요청 (임태성 요청)
        /// 2013-10-01-임종우 : TEST Plan 조회 가능하도록 수정 (임태성 요청)
        /// 2013-10-07-임종우 : TEST 월 계획 REV 값은 Summary 값으로 대체
        /// 2014-01-01-임종우 : 월에 해당하는 주차 가져올때 PLAN_YEAR, PLAN_MONTH 대신 SYS_YEAR, SYS_MONTH 으로 변경
        /// 2014-04-21-임종우 : 입고반품은 실적에서 제외- 테이블 변경 RSUMFACMOV -> VSUMWIPOUT (임태성 요청)
        /// 2015-10-14-임종우 : 월계획 조회시 Rev Plan 표시 되도록 추가 (임태성K 요청)
        /// 2016-08-10-임종우 : 제조에서는 매출이 안보이도록 수정 (임태성K 요청)
        /// 2016-12-05-임종우 : 주간 계획 영업계획 -> Fix 계획으로 변경 (임태성K 요청)
        /// 2016-12-06-임종우 : 10단 칩 이상 스택 수 오류 수정
        /// 2017-07-27-임종우 : 중복되는 단가 최신정보로 가져오도록 변경 (임태성C 요청)
        /// 2018-07-16-임종우 : HRTDMCPROUT -> RWIPMCPBOM 테이블로 변경
        /// 2018-09-12-임종우 : 월계획과 주계획 등록 파트가 달라서 계획 값 표시 안되는 오류 수정
        /// 2020-04-14-김미경 : 단가에 환율 반영 (이승희 D 요청)
        /// 2020-04-28-김미경 : 제품 TYPE(STACK) 추가 반영 (이승희 D)
        /// </summary>
        public PRD010907()
        {
            InitializeComponent();
            cdvFromToDate.DaySelector.SelectedValue = "MONTH";
            cdvFromToDate.AutoBindingUserSetting(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));            
            cdvFromToDate.ToYearMonth.Visible = false;
            cdvFromToDate.FromDate.Visible = false;
            cdvFromToDate.ToDate.Visible = false;

            SortInit();
            GridColumnInit();
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;

            // 2016-08-10-임종우 : 제조에서는 해당화면이 보이돼 매출을 확인 할 수 없도록 수정
            if (GlobalVariable.gsUserGroup == "OPERATOR_GERNERAL" || GlobalVariable.gsUserGroup == "QA MANAGER")
            {
                cdvType.Items.RemoveAt(1);
                //cdvType.Items.Remove("매출");
            }
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "DAY")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD045", GlobalVariable.gcLanguage));
                return false;
            }

            if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory && (cdvType.Text == "Wire" || cdvType.Text == "Chip"))
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD060", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            GetWorkWeek();
            spdData.RPT_ColumnInit();
            
            try
            {
                spdData.RPT_ColumnInit();

                if (cdvFactory.Text.Trim() != "HMKB1")
                {
                    spdData.RPT_AddBasicColumn("TYPE", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("CUSTOMER", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("MAJOR CODE", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("FAMILY", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("PACKAGE", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("TYPE1", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("TYPE2", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LD COUNT", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PKG CODE", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("DENSITY", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("GENERATION", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PIN TYPE", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PRODUCT", 0, 12, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SAP CODE", 0, 13, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("TYPE", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("CUSTOMER", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Bumping Type", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Operation Flow", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Layer classification", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("PKG Type", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RDL Plating", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Final Bump", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Sub. Material", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PRODUCT", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Size", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Thickness", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Flat Type", 0, 12, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SAP CODE", 0, 13, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                }

                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.ToString("MM") + "월", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Action Plan", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Monthly plan (REV standard)", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 100);
                    spdData.RPT_AddBasicColumn("Monthly plan (based on SOP)", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 100);
                    spdData.RPT_MerageHeaderColumnSpan(0, 14, 3);

                    spdData.RPT_AddBasicColumn("PLAN", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);

                    for (int i = 0; i < dtWeek1.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn("WW" + dtWeek1.Rows[i][0].ToString().Substring(4, 2), 1, i + 17, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 50);
                    }

                    spdData.RPT_MerageHeaderColumnSpan(0, 17, dtWeek1.Rows.Count);

                    int iColumn = spdData.ActiveSheet.Columns.Count;

                    spdData.RPT_AddBasicColumn("actual", 0, iColumn, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);

                    for (int i = 0; i < dtWeek2.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn("WW" + dtWeek2.Rows[i][0].ToString().Substring(4, 2), 1, i + iColumn, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 50);
                    }

                    spdData.RPT_AddBasicColumn("PLAN", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.AddMonths(1).ToString("MM") + "월", 1, spdData.ActiveSheet.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 50);
                    spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.AddMonths(2).ToString("MM") + "월", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 50);
                    spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.Columns.Count - 2, 2);

                    spdData.RPT_MerageHeaderColumnSpan(0, iColumn, dtWeek2.Rows.Count);
                }
                else
                {
                    spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                    {
                        spdData.RPT_MerageHeaderRowSpan(0, i + 14, 2);
                    }
                }

                for (int i = 0; i <= 13; i++)
                {
                    spdData.RPT_MerageHeaderRowSpan(0, i, 2);
                }

                //spdData.RPT_ColumnConfigFromTable(btnSort);
                spdData.RPT_ColumnConfigFromTable_New(btnSort);
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
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();

            if (cdvFactory.Text.Trim() != "HMKB1")
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE", "CUST_TYPE", "CUST_TYPE", "DECODE(CUST_TYPE, 'SEC', 1, 'Hynix', 2, 'Fabless', 3, 4)", "CUST_TYPE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", "MAT_GRP_1", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "MAT_GRP_9", "MAT_GRP_9 AS MAJOR_CODE", "MAT_GRP_9", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT_GRP_2", "MAT_GRP_2 AS FAMILY", "MAT_GRP_2", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT_GRP_10", "MAT_GRP_10 AS PACKAGE", "MAT_GRP_10", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT_GRP_4", "MAT_GRP_4 AS TYPE1", "MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT_GRP_5", "MAT_GRP_5 AS TYPE2", "MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT_GRP_6", "MAT_GRP_6 AS LD_COUNT", "MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "MAT_CMF_11", "MAT_CMF_11 AS PKG_CODE", "MAT_CMF_11", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT_GRP_7", "MAT_GRP_7 AS DENSITY", "MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT_GRP_8", "MAT_GRP_8 AS GENERATION", "MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT_CMF_10", "MAT_CMF_10 AS PIN_TYPE", "MAT_CMF_10", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT_ID", "MAT_ID AS PRODUCT", "MAT_ID", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SAP CODE", "VENDOR_ID", "VENDOR_ID AS SAP_CODE", "VENDOR_ID", false);
            }
            else
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE", "CUST_TYPE", "CUST_TYPE", "DECODE(CUST_TYPE, 'SEC', 1, 'Hynix', 2, 'Fabless', 3, 4)", "CUST_TYPE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", "MAT_GRP_1", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("BUMPING TYPE", "MAT_GRP_2", "MAT_GRP_2 AS BUMPING_TYPE", "MAT_GRP_2", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PROCESS FLOW", "MAT_GRP_3", "MAT_GRP_3 AS PROCESS_FLOW", "MAT_GRP_3", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "MAT_GRP_4", "MAT_GRP_4 AS LAYER", "MAT_GRP_4", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG TYPE", "MAT_GRP_5", "MAT_GRP_5 AS PKG_TYPE", "MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL PLATING", "MAT_GRP_6", "MAT_GRP_6 AS RDL_PLATING", "MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FINAL BUMP", "MAT_GRP_7", "MAT_GRP_7 AS FINAL_BUMP", "MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SUB. MATERIAL", "MAT_GRP_8", "MAT_GRP_8 AS SUB_MATERIAL", "MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT_ID", "MAT_ID AS PRODUCT", "MAT_ID", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SIZE", "MAT_CMF_14", "MAT_CMF_14 AS WF_SIZE", "MAT_CMF_14", false); // \"SIZE\"
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("THICKNESS", "MAT_CMF_2", "MAT_CMF_2 AS THICKNESS", "MAT_CMF_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FLAT TYPE", "MAT_CMF_3", "MAT_CMF_3 AS FLAT_TYPE", "MAT_CMF_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SAP CODE", "VENDOR_ID", "VENDOR_ID AS SAP_CODE", "VENDOR_ID", false);
            }
        }
        #endregion

        #region 시간관련 함수
        private void GetWorkWeek()
        {            
            dtWeek1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1("PLAN"));
            dtWeek2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1("SHIP"));

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
            GlobalVariable.FindWeek FindWeek = new GlobalVariable.FindWeek();

            string QueryCond1;
            string QueryCond2;
            string QueryCond3;
            string sToday;
            string sHavingQuery = null;            
            string sSumQuery = null;
            string sSumQuery1 = null;
            string sCaseQuery = null;
            string sCaseQuery1 = null;
            string strDecodeQuery = null;
            string strDecodeQuery1 = null;
            string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            
            // kpcs 선택에 의한 분모 값 저장 한다.
            if (ckbKpcs.Checked == true)
            {
                if (cdvType.Text == LanguageFunction.FindLanguage("Sales", 0))
                {
                    sKpcsValue = "1000000";
                }
                else
                {
                    sKpcsValue = "1000";
                }
            }
            else
            {
                sKpcsValue = "1";
            }
                        
            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                #region 월간 조회
                String sPlanMonth = cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM");
                String sPlanMonth_M1 = cdvFromToDate.FromYearMonth.Value.AddMonths(1).ToString("yyyyMM");
                String sPlanMonth_M2 = cdvFromToDate.FromYearMonth.Value.AddMonths(2).ToString("yyyyMM");

                // 기준일 구하기
                if (DateTime.Now.ToString("yyyyMM") == sPlanMonth)
                {
                    sToday = DateTime.Now.ToString("yyyyMMdd"); // 현재일
                }
                else if (Convert.ToInt32(DateTime.Now.ToString("yyyyMM")) < Convert.ToInt32(sPlanMonth))
                {
                    sToday = sPlanMonth + "01"; // 미래일
                }
                else
                {
                    sToday = cdvFromToDate.FromYearMonth.Value.AddMonths(1).AddDays(-1).ToString("yyyyMMdd"); // 조회 월의 마지막 일
                }

                // 기준 주차 구하기
                FindWeek = CmnFunction.GetWeekInfo(sToday, "OTD");

                for (int i = 0; i < dtWeek1.Rows.Count; i++)
                {
                    sHavingQuery += " + NVL(W" + i + ",0)";
                    sSumQuery += "     , ROUND(SUM(W" + i + " * CONVERT_QTY) /" + sKpcsValue + ",0) AS W" + i + "\n";
                    sCaseQuery += "             , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN W" + i + " / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE W" + i + " END AS W" + i + "\n";
                }

                for (int i = 0; i < dtWeek2.Rows.Count; i++)
                {
                    sHavingQuery += " + NVL(SHP_W" + i + ",0)";
                    sSumQuery1 += "     , ROUND(SUM(SHP_W" + i + " * CONVERT_QTY) /" + sKpcsValue + ",0) AS SHP_W" + i + "\n";
                    sCaseQuery1 += "             , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN SHP_W" + i + " / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE SHP_W" + i + " END AS SHP_W" + i + "\n";
                }

                //strDecodeQuery = CmnFunction.GetDecodeQueryStringFromDataTable("                     , SUM(DECODE(PLAN_WEEK, ", dtWeek1, ", REV_QTY, 0)) AS W", false);                
                strDecodeQuery = CmnFunction.GetDecodeQueryStringFromDataTable("                     , SUM(CASE WHEN PLAN_MONTH = '" + sPlanMonth + "' AND PLAN_WEEK = ", dtWeek1, " THEN REV_QTY ELSE 0 END) AS W", false);
                strDecodeQuery1 = CmnFunction.GetDecodeQueryStringFromDataTable("                     , SUM(DECODE(PLAN_WEEK, ", dtWeek2, ", SHP_QTY, 0)) AS SHP_W", false);

                strSqlString.Append("WITH DT AS" + "\n");
                strSqlString.Append("(" + "\n");
                strSqlString.Append(" SELECT B.PLAN_MONTH, A.PLAN_WEEK, A.GUBUN, B.CKD_WEEK, A.MAT_ID, A.WW_QTY, B.CNT" + "\n");
                strSqlString.Append("      , ROUND((A.WW_QTY / 7) * B.CNT, 0) AS REV_QTY" + "\n");
                strSqlString.Append("   FROM RWIPPLNWEK A" + "\n");
                strSqlString.Append("      , (" + "\n");
                strSqlString.Append("         SELECT MAX(TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_MONTH,2,'0')) PLAN_MONTH" + "\n");
                strSqlString.Append("              , MAX(TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_WEEK,2,'0')) PLAN_WEEK" + "\n");
                strSqlString.Append("              , COUNT(*) AS CNT" + "\n");
                strSqlString.Append("              , (SELECT MAX(TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_WEEK,2,'0')) PLAN_WEEK FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = '" + sToday + "') AS CKD_WEEK" + "\n");
                strSqlString.Append("           FROM MWIPCALDEF" + "\n");
                strSqlString.Append("          WHERE 1=1" + "\n");
                strSqlString.Append("            AND CALENDAR_ID = 'OTD'" + "\n");
                strSqlString.Append("            AND SYS_YEAR||LPAD(SYS_MONTH,2,'0') IN ('" + sPlanMonth + "','" + sPlanMonth_M1 + "','" + sPlanMonth_M2 + "')" + "\n");
                strSqlString.Append("          GROUP BY PLAN_MONTH, PLAN_WEEK" + "\n");
                strSqlString.Append("        ) B" + "\n");
                strSqlString.Append("  WHERE 1=1" + "\n");
                strSqlString.Append("    AND A.PLAN_WEEK = B.PLAN_WEEK" + "\n");
                strSqlString.Append("    AND A.FACTORY = '" + cdvFactory.Text + "' " + "\n");                
                strSqlString.Append(")" + "\n");
                strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond2);
                strSqlString.Append("     , ROUND(SUM(MON_PLN * CONVERT_QTY) /" + sKpcsValue + ",0) AS MON_PLN" + "\n");
                strSqlString.Append("     , ROUND(SUM(REV_PLN * CONVERT_QTY) /" + sKpcsValue + ",0) AS REV_PLN" + "\n");

                // 2017-01-17-임종우 : 미래 월계획은 주간계획을 모두 SUM 한 값으로 대체한다.
                if (Convert.ToInt32(DateTime.Now.ToString("yyyyMM")) < Convert.ToInt32(sPlanMonth))
                {
                    strSqlString.Append("     , ROUND(SUM(M0 * CONVERT_QTY) /" + sKpcsValue + ",0) AS SOP_PLN" + "\n");
                }
                else
                {
                    strSqlString.Append("     , ROUND(SUM(SOP_PLN * CONVERT_QTY) /" + sKpcsValue + ",0) AS SOP_PLN" + "\n");
                }

                strSqlString.Append(sSumQuery);
                strSqlString.Append(sSumQuery1);
                strSqlString.Append("     , ROUND(SUM(M1 * CONVERT_QTY) /" + sKpcsValue + ",0) AS M1" + "\n");
                strSqlString.Append("     , ROUND(SUM(M2 * CONVERT_QTY) /" + sKpcsValue + ",0) AS M2" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT MAT.* " + "\n");
                strSqlString.Append("             , NVL((SELECT DATA_10 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND  KEY_1 = MAT_GRP_1), '-') AS CUST_TYPE " + "\n");
                strSqlString.Append("             , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN M_PLN.MON_PLN / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE M_PLN.MON_PLN END AS MON_PLN" + "\n");
                strSqlString.Append("             , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN M_PLN.REV_PLN / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE M_PLN.REV_PLN END AS REV_PLN" + "\n");
                strSqlString.Append("             , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN M_PLN.SOP_PLN / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE M_PLN.SOP_PLN END AS SOP_PLN" + "\n");
                strSqlString.Append(sCaseQuery);
                strSqlString.Append(sCaseQuery1);
                strSqlString.Append("             , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN M0 / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE M0 END AS M0" + "\n");
                strSqlString.Append("             , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN M1 / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE M1 END AS M1" + "\n");
                strSqlString.Append("             , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN M2 / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE M2 END AS M2" + "\n");

                if (cdvType.Text == "Wire")
                {
                    strSqlString.Append("             , CASE WHEN CONVERT_QTY IS NOT NULL THEN CONVERT_QTY" + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN MAT_GRP_6 IN ('-', '0') THEN 1 ELSE TO_NUMBER(MAT_GRP_6) END" + "\n");
                    strSqlString.Append("               END AS CONVERT_QTY" + "\n");
                }
                else if (cdvType.Text == "Chip")
                {
                    strSqlString.Append("             , CASE WHEN MAT.MAT_GRP_4 IN ('-', 'FD', 'FU') THEN '1'" + "\n");                 
                    strSqlString.Append("                    WHEN MAT.MAT_GRP_4 IN ('DDP') THEN '2'" + "\n");
                    strSqlString.Append("                    WHEN MAT.MAT_GRP_4 IN ('QDP') THEN '4'" + "\n");
                    strSqlString.Append("                    WHEN MAT.MAT_GRP_4 IN ('ODP') THEN '8'" + "\n");
                    strSqlString.Append("                    ELSE REGEXP_REPLACE(MAT.MAT_GRP_4, '[^[:digit:]]')" + "\n");
                    //strSqlString.Append("                    ELSE SUBSTR(MAT.MAT_GRP_4, 3)" + "\n");
                    strSqlString.Append("               END AS CONVERT_QTY" + "\n");
                }
                else if (cdvType.Text == LanguageFunction.FindLanguage("Sales", 0))
                {
                    strSqlString.Append("             , CONVERT_QTY" + "\n");
                }
                else
                {
                    strSqlString.Append("             , 1 AS CONVERT_QTY" + "\n");
                }

                strSqlString.Append("          FROM MWIPMATDEF MAT" + "\n");
                strSqlString.Append("             , (" + "\n");

                if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                {
                    strSqlString.Append("                SELECT MAT_ID" + "\n");
                    strSqlString.Append("                     , SUM(PLAN_QTY_ASSY) AS MON_PLN" + "\n");
                    strSqlString.Append("                     , SUM(DECODE(RESV_FIELD1, ' ', 0, RESV_FIELD1)) AS REV_PLN" + "\n");
                    strSqlString.Append("                     , SUM(DECODE(RESV_FIELD3, ' ', 0, RESV_FIELD3)) AS SOP_PLN" + "\n");
                }
                else if (cdvFactory.Text == "HMKB1")
                {
                    strSqlString.Append("                SELECT MAT_ID" + "\n");
                    strSqlString.Append("                     , SUM(DECODE(RESV_FIELD7, ' ', 0, RESV_FIELD7)) AS MON_PLN" + "\n");
                    strSqlString.Append("                     , SUM(DECODE(RESV_FIELD8, ' ', 0, RESV_FIELD8)) AS REV_PLN" + "\n");
                    strSqlString.Append("                     , SUM(DECODE(RESV_FIELD8, ' ', 0, RESV_FIELD8)) AS SOP_PLN" + "\n");                      
                }
                else
                {
                    strSqlString.Append("                SELECT MAT_ID" + "\n");
                    strSqlString.Append("                     , SUM(PLAN_QTY_TEST) AS MON_PLN" + "\n");
                    strSqlString.Append("                     , SUM(DECODE(RESV_FIELD2, ' ', 0, RESV_FIELD2)) AS REV_PLN" + "\n");
                    strSqlString.Append("                     , SUM(DECODE(RESV_FIELD4, ' ', 0, RESV_FIELD4)) AS SOP_PLN" + "\n");                    
                }

                strSqlString.Append("                  FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
                if (cdvFactory.Text == "HMKB1")
                {
                    strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND (RESV_FIELD7 > '0' OR RESV_FIELD8 > '0') " + "\n");
                }
                else
                {
                    strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                }
                strSqlString.Append("                   AND PLAN_MONTH = '" + sPlanMonth + "'" + "\n");                
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) M_PLN" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append(strDecodeQuery1);
                strSqlString.Append("                  FROM (" + "\n");
                //strSqlString.Append("                        SELECT TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_WEEK,2,'0') AS PLAN_WEEK, MAT_ID" + "\n");
                //strSqlString.Append("                             , SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) AS SHP_QTY" + "\n");
                //strSqlString.Append("                          FROM RSUMFACMOV A" + "\n");
                //strSqlString.Append("                             , MWIPCALDEF B " + "\n");
                //strSqlString.Append("                         WHERE 1=1 " + "\n");
                //strSqlString.Append("                           AND A.WORK_DATE = B.SYS_DATE(+) " + "\n");
                //strSqlString.Append("                           AND A.WORK_DATE LIKE '" + sPlanMonth + "%'" + "\n");
                //strSqlString.Append("                           AND A.LOT_TYPE = 'W' " + "\n");
                //strSqlString.Append("                           AND A.CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                //strSqlString.Append("                           AND A.CM_KEY_2 = 'PROD' " + "\n");
                //strSqlString.Append("                           AND A.CM_KEY_3 LIKE 'P%'" + "\n");
                //strSqlString.Append("                           AND A.FACTORY NOT IN ('RETURN')" + "\n");
                //strSqlString.Append("                           AND B.CALENDAR_ID(+) = 'OTD'" + "\n");
                strSqlString.Append("                        SELECT TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_WEEK,2,'0') AS PLAN_WEEK, MAT_ID" + "\n");
                strSqlString.Append("                             , SUM(SHP_QTY_1) AS SHP_QTY" + "\n");
                strSqlString.Append("                          FROM VSUMWIPOUT A" + "\n");
                strSqlString.Append("                             , MWIPCALDEF B " + "\n");
                strSqlString.Append("                         WHERE 1=1 " + "\n");
                strSqlString.Append("                           AND A.WORK_DATE = B.SYS_DATE(+) " + "\n");
                strSqlString.Append("                           AND A.WORK_DATE LIKE '" + sPlanMonth + "%'" + "\n");
                strSqlString.Append("                           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                           AND A.FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                           AND A.CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                           AND A.CM_KEY_3 LIKE 'P%'" + "\n");                
                strSqlString.Append("                           AND B.CALENDAR_ID(+) = 'OTD'" + "\n");

                if (DateTime.Now.ToString("yyyyMM") == sPlanMonth)
                {
                    // Modified By James Kwon 2015/06/25
                    //strSqlString.Append("                           AND B.PLAN_YEAR||LPAD(B.PLAN_WEEK,2,'0') < '" + FindWeek.ThisWeek + "'" + "\n");
                    strSqlString.Append("                           AND B.PLAN_YEAR||LPAD(B.PLAN_WEEK,2,'0') <= '" + FindWeek.ThisWeek + "'" + "\n");
                    strSqlString.Append("                         GROUP BY TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_WEEK,2,'0'), MAT_ID " + "\n");
                    strSqlString.Append("                         UNION ALL " + "\n");
                    strSqlString.Append("                        SELECT PLAN_WEEK, MAT_ID, SUM(REV_QTY) " + "\n");
                    strSqlString.Append("                          FROM DT " + "\n");
                    strSqlString.Append("                         WHERE GUBUN = '3' " + "\n");
                    strSqlString.Append("                           AND PLAN_WEEK = '" + FindWeek.ThisWeek + "'" + "\n");
                    strSqlString.Append("                           AND PLAN_MONTH = '" + sPlanMonth + "'" + "\n");
                    strSqlString.Append("                         GROUP BY PLAN_WEEK, MAT_ID " + "\n");
                }
                else
                {
                    strSqlString.Append("                         GROUP BY TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_WEEK,2,'0'), MAT_ID " + "\n");
                }

                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) SHP" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append(strDecodeQuery);
                strSqlString.Append("                     , SUM(CASE WHEN PLAN_WEEK >= CKD_WEEK THEN REV_QTY ELSE 0 END) AS W_LAST" + "\n");
                strSqlString.Append("                     , SUM(DECODE(PLAN_MONTH, '" + sPlanMonth + "', REV_QTY, 0)) AS M0" + "\n");
                strSqlString.Append("                     , SUM(DECODE(PLAN_MONTH, '" + sPlanMonth_M1 + "', REV_QTY, 0)) AS M1" + "\n");
                strSqlString.Append("                     , SUM(DECODE(PLAN_MONTH, '" + sPlanMonth_M2 + "', REV_QTY, 0)) AS M2" + "\n");
                strSqlString.Append("                  FROM (" + "\n");
                strSqlString.Append("                        SELECT * FROM DT WHERE GUBUN = '3'" + "\n");
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) W_PLN" + "\n");

                if (cdvType.Text == "Wire")
                {
                    strSqlString.Append("             , (" + "\n");
                    strSqlString.Append("                SELECT MCP_TO_PART AS MAT_ID" + "\n");
                    strSqlString.Append("                     , SUM(NVL(WIRE_CNT, MAT_GRP_6)) AS CONVERT_QTY" + "\n");
                    strSqlString.Append("                  FROM (" + "\n");
                    strSqlString.Append("                        SELECT A.MCP_TO_PART" + "\n");
                    strSqlString.Append("                             , (SELECT TCD_CMF_2 FROM CWIPTCDDEF@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND OPER LIKE 'A060%' AND TCD_CMF_2 <> ' ' AND SET_FLAG = 'Y' AND MAT_ID = A.MAT_ID) AS WIRE_CNT" + "\n");
                    strSqlString.Append("                             , B.* " + "\n");
                    strSqlString.Append("                          FROM RWIPMCPBOM A " + "\n");
                    strSqlString.Append("                             , MWIPMATDEF B " + "\n");
                    strSqlString.Append("                         WHERE 1=1 " + "\n");
                    strSqlString.Append("                           AND A.FACTORY = B.FACTORY " + "\n");
                    strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID " + "\n");
                    strSqlString.Append("                           AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                           AND B.DELETE_FLAG = ' ' " + "\n");
                    strSqlString.Append("                           AND (MAT_GRP_5 = '1' OR MAT_GRP_5 LIKE 'Middle%' OR MAT_GRP_5 = 'Merge') " + "\n");
                    strSqlString.Append("                       )" + "\n");
                    strSqlString.Append("                 GROUP BY MCP_TO_PART" + "\n");
                    strSqlString.Append("                 UNION ALL" + "\n");
                    strSqlString.Append("                SELECT MAT_ID , TO_NUMBER(TCD_CMF_2) AS CONVERT_QTY" + "\n");
                    strSqlString.Append("                  FROM CWIPTCDDEF@RPTTOMES A" + "\n");
                    strSqlString.Append("                 WHERE 1=1 " + "\n");
                    strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND OPER = 'A0600' " + "\n");
                    strSqlString.Append("                   AND SET_FLAG = 'Y' " + "\n");
                    strSqlString.Append("                   AND TCD_CMF_2 <> ' ' " + "\n");
                    strSqlString.Append("               ) COV" + "\n");
                    strSqlString.Append("         WHERE 1=1" + "\n");
                    strSqlString.Append("           AND MAT.MAT_ID = COV.MAT_ID(+)" + "\n");
                }
                else if (cdvType.Text == LanguageFunction.FindLanguage("Sales", 0))
                {
                    strSqlString.Append("             , (" + "\n");
                    strSqlString.Append("                SELECT PRODUCT AS MAT_ID" + "\n");
                    strSqlString.Append("                     , DECODE(MAX(CRNC_UNIT), 'USD', MAX(PRICE * STD_RATE) KEEP(DENSE_RANK FIRST ORDER BY UPDT_DT DESC), MAX(PRICE) KEEP(DENSE_RANK FIRST ORDER BY UPDT_DT DESC)) AS CONVERT_QTY" + "\n");
                    strSqlString.Append("                  FROM RPRIMATDAT" + "\n");
                    strSqlString.Append("                     , (SELECT STD_RATE" + "\n");
                    strSqlString.Append("                          FROM RDAYECRDAT" + "\n");
                    strSqlString.Append("                         WHERE SUBSTR(REPLACE(APPRL_DT, '-', ''), 0, 6) = '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "' " + "\n");
                    strSqlString.Append("                        )" + "\n");
                    strSqlString.Append("                 WHERE 1=1" + "\n");

                    if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                    {
                        strSqlString.Append("                   AND SUBSTR(ITEM_CD, 10, 2) = 'A0'" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("                   AND SUBSTR(ITEM_CD, 10, 2) = '0T'" + "\n");
                    }

                    strSqlString.Append("                 GROUP BY PRODUCT" + "\n");
                    strSqlString.Append("               ) COV" + "\n");
                    strSqlString.Append("         WHERE 1=1" + "\n");
                    strSqlString.Append("           AND MAT.MAT_ID = COV.MAT_ID(+)" + "\n");
                }
                else
                {
                    strSqlString.Append("         WHERE 1=1" + "\n");
                }
                                
                strSqlString.Append("           AND MAT.MAT_ID = M_PLN.MAT_ID(+)" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = W_PLN.MAT_ID(+)" + "\n");
                strSqlString.Append("           AND MAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("           AND MAT.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("           AND MAT.MAT_TYPE = 'FG'" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (cdvFactory.Text.Trim() != "HMKB1")
                {
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

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
                }
                else
                {
                    if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                    if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                    if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                    if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                    if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                    if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                    if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                    if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                    if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                    if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                    if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                    if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                }
                #endregion

                strSqlString.Append("       )" + "\n");
                strSqlString.AppendFormat(" GROUP BY {0} " + "\n", QueryCond1);
                strSqlString.Append("HAVING SUM(NVL(MON_PLN,0) + NVL(REV_PLN,0) + NVL(SOP_PLN,0) + " + sHavingQuery + " + NVL(M0,0) + NVL(M1,0) + NVL(M2,0)) > 0" + "\n");
                strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond3);
            #endregion
            }
            else
            {
                #region 주간 조회
                string sFrom = cdvFromToDate.FromWeek.SelectedValue.ToString();
                string sTo = cdvFromToDate.ToWeek.SelectedValue.ToString();
                string[] selectDate = new string[cdvFromToDate.SubtractBetweenFromToDate + 1];
                selectDate = cdvFromToDate.getSelectDate();

                for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                {
                    sSumQuery += "     , ROUND(SUM(W" + i + " * CONVERT_QTY) /" + sKpcsValue + ",0) AS W" + i + "\n";
                    sCaseQuery += "             , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN W" + i + " / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE W" + i + " END AS W" + i + "\n";
                    strDecodeQuery += "                     , SUM(DECODE(PLAN_WEEK, '" + selectDate[i].ToString() + "', WW_QTY, 0)) AS W" + i + "\n";
                }

                strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond2);
                strSqlString.Append(sSumQuery);
                strSqlString.Append("  FROM (" + "\n");                
                strSqlString.Append("        SELECT MAT.*" + "\n");
                strSqlString.Append("             , NVL((SELECT DATA_10 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND  KEY_1 = MAT_GRP_1), '-') AS CUST_TYPE" + "\n");
                strSqlString.Append(sCaseQuery);

                if (cdvType.Text == "Wire")
                {
                    strSqlString.Append("             , CASE WHEN CONVERT_QTY IS NOT NULL THEN CONVERT_QTY" + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN MAT.MAT_GRP_6 IN ('-', '0') THEN 1 ELSE TO_NUMBER(MAT.MAT_GRP_6) END" + "\n");
                    strSqlString.Append("               END AS CONVERT_QTY" + "\n");
                }
                else if (cdvType.Text == "Chip")
                {
                    strSqlString.Append("             , CASE WHEN MAT.MAT_GRP_4 IN ('-', 'FD', 'FU') THEN '1'" + "\n");
                    strSqlString.Append("                    WHEN MAT.MAT_GRP_4 IN ('DDP') THEN '2'" + "\n");
                    strSqlString.Append("                    WHEN MAT.MAT_GRP_4 IN ('QDP') THEN '4'" + "\n");
                    strSqlString.Append("                    WHEN MAT.MAT_GRP_4 IN ('ODP') THEN '8'" + "\n");
                    strSqlString.Append("                    ELSE REGEXP_REPLACE(MAT.MAT_GRP_4, '[^[:digit:]]')" + "\n");
                    //strSqlString.Append("                    ELSE SUBSTR(MAT.MAT_GRP_4, 3)" + "\n");
                    strSqlString.Append("               END AS CONVERT_QTY" + "\n");
                }
                else if (cdvType.Text == LanguageFunction.FindLanguage("Sales", 0))
                {
                    strSqlString.Append("             , CONVERT_QTY" + "\n");
                }
                else
                {
                    strSqlString.Append("             , 1 AS CONVERT_QTY" + "\n");
                }

                strSqlString.Append("          FROM MWIPMATDEF MAT" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append(strDecodeQuery);                
                strSqlString.Append("                  FROM RWIPPLNWEK" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                   AND GUBUN = '3' " + "\n");
                strSqlString.Append("                   AND PLAN_WEEK BETWEEN '" + sFrom + "' AND '" + sTo + "'" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) PLN" + "\n");

                if (cdvType.Text == "Wire")
                {
                    strSqlString.Append("             , (" + "\n");
                    strSqlString.Append("                SELECT MCP_TO_PART AS MAT_ID" + "\n");
                    strSqlString.Append("                     , SUM(NVL(WIRE_CNT, MAT_GRP_6)) AS CONVERT_QTY" + "\n");
                    strSqlString.Append("                  FROM (" + "\n");
                    strSqlString.Append("                        SELECT A.MCP_TO_PART" + "\n");
                    strSqlString.Append("                             , (SELECT TCD_CMF_2 FROM CWIPTCDDEF@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND OPER LIKE 'A060%' AND TCD_CMF_2 <> ' ' AND SET_FLAG = 'Y' AND MAT_ID = A.MAT_ID) AS WIRE_CNT" + "\n");
                    strSqlString.Append("                             , B.* " + "\n");
                    strSqlString.Append("                          FROM RWIPMCPBOM A " + "\n");
                    strSqlString.Append("                             , MWIPMATDEF B " + "\n");
                    strSqlString.Append("                         WHERE 1=1 " + "\n");
                    strSqlString.Append("                           AND A.FACTORY = B.FACTORY " + "\n");
                    strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID " + "\n");
                    strSqlString.Append("                           AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                           AND B.DELETE_FLAG = ' ' " + "\n");
                    strSqlString.Append("                           AND (MAT_GRP_5 = '1' OR MAT_GRP_5 LIKE 'Middle%' OR MAT_GRP_5 = 'Merge')  " + "\n");
                    strSqlString.Append("                       )" + "\n");
                    strSqlString.Append("                 GROUP BY MCP_TO_PART" + "\n");
                    strSqlString.Append("                 UNION ALL" + "\n");
                    strSqlString.Append("                SELECT MAT_ID , TO_NUMBER(TCD_CMF_2) AS CONVERT_QTY" + "\n");
                    strSqlString.Append("                  FROM CWIPTCDDEF@RPTTOMES A" + "\n");
                    strSqlString.Append("                 WHERE 1=1 " + "\n");
                    strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND OPER = 'A0600' " + "\n");
                    strSqlString.Append("                   AND SET_FLAG = 'Y' " + "\n");
                    strSqlString.Append("                   AND TCD_CMF_2 <> ' ' " + "\n");
                    strSqlString.Append("               ) COV" + "\n");
                    strSqlString.Append("         WHERE 1=1" + "\n");
                    strSqlString.Append("           AND MAT.MAT_ID = COV.MAT_ID(+)" + "\n");
                }
                else if (cdvType.Text == LanguageFunction.FindLanguage("Sales", 0))
                {
                    strSqlString.Append("             , (" + "\n");
                    strSqlString.Append("                SELECT PRODUCT AS MAT_ID" + "\n");
                    strSqlString.Append("                     , DECODE(CRNC_UNIT, 'USD', PRICE * STD_RATE, PRICE) AS CONVERT_QTY" + "\n");
                    strSqlString.Append("                  FROM RPRIMATDAT" + "\n");
                    strSqlString.Append("                     , (SELECT STD_RATE" + "\n");
                    strSqlString.Append("                          FROM RDAYECRDAT" + "\n");
                    strSqlString.Append("                         WHERE SUBSTR(REPLACE(APPRL_DT, '-', ''), 0, 6) = '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "' " + "\n");
                    strSqlString.Append("                        )" + "\n");
                    strSqlString.Append("                 WHERE 1=1" + "\n");

                    if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                    {
                        strSqlString.Append("                   AND SUBSTR(ITEM_CD, 10, 2) = 'A0'" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("                   AND SUBSTR(ITEM_CD, 10, 2) = '0T'" + "\n");
                    }
                    
                    strSqlString.Append("               ) COV" + "\n");
                    strSqlString.Append("         WHERE 1=1" + "\n");
                    strSqlString.Append("           AND MAT.MAT_ID = COV.MAT_ID(+)" + "\n");
                }
                else
                {
                    strSqlString.Append("         WHERE 1=1" + "\n");
                }
                
                strSqlString.Append("           AND MAT.MAT_ID = PLN.MAT_ID" + "\n");
                strSqlString.Append("           AND MAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("           AND MAT.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("           AND MAT.MAT_TYPE = 'FG'" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (cdvFactory.Text.Trim() != "HMKB1")
                {
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

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
                }
                else
                {
                    if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                    if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                    if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                    if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                    if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                    if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                    if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                    if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                    if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                    if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                    if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                    if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                        strSqlString.AppendFormat("           AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                }
                #endregion

                strSqlString.Append("       )" + "\n");
                strSqlString.AppendFormat(" GROUP BY {0} " + "\n", QueryCond1);
                strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond3);
                #endregion
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private string MakeSqlString1(string type) // 검색 월에 해당하는 주차 리스트 가져오기
        {
            StringBuilder strSqlString = new StringBuilder();
            
            String sPlanMonth = cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM");

            string sToday = null;

            // 기준일 구하기
            if (DateTime.Now.ToString("yyyyMM") == sPlanMonth)
            {
                sToday = DateTime.Now.ToString("yyyyMMdd"); // 현재일
            }
            else if (Convert.ToInt32(DateTime.Now.ToString("yyyyMM")) < Convert.ToInt32(sPlanMonth))
            {
                sToday = sPlanMonth + "01"; // 미래일
            }
            else
            {
                sToday = cdvFromToDate.FromYearMonth.Value.AddMonths(1).AddDays(-1).ToString("yyyyMMdd"); // 조회 월의 마지막 일
            }

            if (type == "PLAN")
            {
                strSqlString.Append("SELECT DISTINCT PLAN_YEAR||LPAD(PLAN_WEEK,2,'0') AS PLAN_WEEK " + "\n");
                strSqlString.Append("  FROM MWIPCALDEF" + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND CALENDAR_ID = 'OTD' " + "\n");
                strSqlString.Append("   AND SYS_YEAR||LPAD(SYS_MONTH,2,'0') = '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "' " + "\n");
                strSqlString.Append(" ORDER BY PLAN_WEEK" + "\n");
            }
            else
            {
                strSqlString.Append("SELECT DISTINCT PLAN_YEAR||LPAD(PLAN_WEEK,2,'0') AS PLAN_WEEK " + "\n");
                strSqlString.Append("  FROM MWIPCALDEF" + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND CALENDAR_ID = 'OTD' " + "\n");
                strSqlString.Append("   AND SYS_YEAR||LPAD(SYS_MONTH,2,'0') = '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "' " + "\n");
                strSqlString.Append("   AND SYS_DATE <= '" + sToday + "' " + "\n");
                strSqlString.Append(" ORDER BY PLAN_WEEK" + "\n");
            }

            return strSqlString.ToString();
        }

        #endregion


        #region EVENT 처리
        /// <summary>
        /// 6. View 버튼 Action
        /// </summary>        ///         
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            if (CheckField() == false) return;

            GridColumnInit();
            spdData_Sheet1.RowCount = 0;

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
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 14, null, null, btnSort);
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 14, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);                

                //spdData.DataSource = dt;
                
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
                //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null, true);
                spdData.ExportExcel();            
            }
        }

        /// <summary>
        /// Factory 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);

            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                BaseFormType = eBaseFormType.BUMP_BASE;
                pnlBUMPDetail.Visible = false;

                cdvType.Items.Clear();
                cdvType.Items.Add(LanguageFunction.FindLanguage("basics", 0));
                cdvType.Items.Add(LanguageFunction.FindLanguage("Sales", 0));
                cdvType.SelectedIndex = 0;

                ckbKpcs.Checked = false;
            }
            else
            {
                BaseFormType = eBaseFormType.WIP_BASE;
                pnlWIPDetail.Visible = false;

                cdvType.Items.Clear();
                cdvType.Items.Add(LanguageFunction.FindLanguage("basics", 0));
                cdvType.Items.Add(LanguageFunction.FindLanguage("Sales", 0));
                cdvType.Items.Add("Wire");
                cdvType.Items.Add("Chip");
                cdvType.SelectedIndex = 0;
            }

            SortInit();
            GridColumnInit();
        }

        #endregion

        private void cdvType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cdvType.Text == LanguageFunction.FindLanguage("Sales", 0))
            {
                ckbKpcs.Text = LanguageFunction.FindLanguage("one million won", 0);
            }
            else
            {
                ckbKpcs.Text = "Kpcs";
            }           
        }
    }
}
