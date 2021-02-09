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
    public partial class PRD011007 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {        
        List<string> selectDate  = new List<string>();
     
        /// <summary>
        /// 클  래  스: PRD011007<br/>
        /// 클래스요약: WAFER 입고<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2013-09-03<br/>
        /// 상세  설명: WAFER 입고(임태성 요청) - 기존 THE 개발화면 제거하고 신규로 만듬<br/>
        /// 변경  내용: <br/>   
        /// 2013-09-05-임종우 : COB 의 경우 (수량 = 장수), (매출 = 칩수 * 4.8) 로 계산 한다. (임태성 요청)
        ///                   : CUST_TYPE 추가 (임태성 요청)
        /// 2013-09-12-임종우 : REV 월 계획 기존 생산관리에서 업로드 하는 것으로 변경 (임태성 요청)
        /// 2013-10-29-임종우 : PIN TYPE 추가 (김권수 요청)
        /// 2014-08-11-임종우 : 실적 -> DHL 제품 - 3차 기준, JZH 제품 - 1/2 로 계산 (임태성K 요청)
        ///                     재공 -> DHL 제품 - 3차 기준
        /// 2014-09-03-임종우 : 매출액2 검색 기능 추가 - Turn Key 제품은 ASSY, TEST 단가 합해서 계산한다 (임태성K 요청)
        /// 2014-11-04-임종우 : 삼성 단가 미등록된 제품의 경우 동일한 PKG_CODE의 평균 단가로 계산한다 (임태성K 요청)
        /// 2015-01-08-임종우 : SEKY%로 시작하는 제품 2차를 MAIN 으로..(임태성K 요청)
        /// 2015-02-16-임종우 : DQZ 제품 - 3차 기준 (임태성K 요청)
        /// 2015-06-01-임종우 : DQA, DRA 제품 - 3차 기준 (김성업D 요청)
        /// 2015-07-16-임종우 : 재공 -> Hynix HX_VERSION = 'A-376' 제품 - 5차, Merge 기준 (임태성K 요청)
        ///                     실적 -> Hynix HX_VERSION = 'A-376' 제품 - 5차기준
        /// 2015-08-28-임종우 : DND 제품 - 3차 기준 (임태성K 요청)
        /// 2015-09-11-임종우 : DWG 제품 - 3차 기준 (임태성K 요청)
        /// 2015-10-05-임종우 : 3차 기준 제품은 Middle 은 제외하고 Middle1,2,3,.....만 포함 (임태성K 요청)
        /// 2015-10-16-임종우 : DUT 제품 - 3차 기준 (임태성K 요청)
        /// 2016-01-13-임종우 : HAU 제품 - 3차 기준 (오영식K 요청)
        /// 2016-10-12-임종우 : Hynix 제품 06시 기준으로 분리 (임태성K 요청)
        /// 2016-10-20-임종우 : HF8 제품 - 3차 기준 (김성업D 요청)
        /// 2016-11-14-임종우 : HDM 제품 - 3차 기준 (김성업D 요청)
        /// 2016-12-12-임종우 : HDR 제품 - 3차 기준 (김성업D 요청)
        /// 2017-02-06-임종우 : 재공 -> Hynix HX_VERSION = 'A-445' 제품 - 6차, Merge 기준 (임태성K 요청)
        ///                     실적 -> Hynix HX_VERSION = 'A-445' 제품 - 6차기준
        /// 2017-05-02-임종우 : HEN 제품 - 3차 기준 (김성업K 요청)
        /// 2017-07-24-임종우 : HQA 제품 - 3차 기준 (이원하 요청)
        /// 2017-10-25-임종우 : HQC 제품 - 3차 기준 (김성업K 요청)
        /// 2018-06-04-임종우 : Hynix 제품 Partial Wafer Dummy - PW 제외 (박형순대리 요청)
        /// 2018-06-08-임종우 : 재공 -> Hynix HX_VERSION = 'A-524' 제품 - 7차, Merge 기준 (박형순대리 요청)
        ///                     실적 -> Hynix HX_VERSION = 'A-524' 제품 - 7차기준 
        /// 2018-07-04-임종우 : 재공 -> Hynix HX_VERSION = 'A-525' 제품 - 6차, Merge 기준 (박형순대리 요청)
        ///                     실적 -> Hynix HX_VERSION = 'A-524' 제품 - 6차기준 
        /// 2020-04-14-김미경 : 매출액, 매출액(Turkey) 옵션 삭제 - 이승희 D
        /// </summary>        
        public PRD011007()
        {
            InitializeComponent();
            SortInit();           

            cdvDate.Value = DateTime.Now;
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;    
            GridColumnInit();

            lblNumericSum.Text = "";
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        ///
        /// </summary>        
        private Boolean CheckField()
        {
            if (!(cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory || cdvFactory.Text == GlobalVariable.gsTestDefaultFactory))
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        /// <summary 2. 헤더 생성> 
        ///
        /// </summary>
        private void GridColumnInit()
        {
            selectDate.Clear();            
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 2;

            int colIndex = 14;
            DateTime stDate = Convert.ToDateTime(cdvDate.Value.ToString("yyyy-MM") + "-01");
            DateTime edDate = Convert.ToDateTime(cdvDate.Value.ToString("yyyy-MM-dd"));

            try
            {
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("TYPE", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Vendor description", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Major", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PKG", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Lead", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PKG Code", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Action Plan", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Rev plan", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("BOH", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Wafer warehousing quantity", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Available Quantity", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("rate of progress", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Execution", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("Rev", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 12, 2);

                spdData.RPT_AddBasicColumn("Warehousing Performance", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);               

                for (DateTime d = stDate; d <= edDate; d = d.AddDays(1))
                {                    
                    selectDate.Add(d.ToString("yyyyMMdd"));
                    spdData.RPT_AddBasicColumn(d.ToString("MM.dd"), 1, colIndex++, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                                        
                }

                spdData.RPT_MerageHeaderColumnSpan(0, 14, selectDate.Count);

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
                spdData.RPT_MerageHeaderRowSpan(0, 10, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 11, 2);

                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
            }

            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        /// <summary 3. SortInit>
        /// 
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE", "CUST_TYPE", "CUST_TYPE", "DECODE(CUST_TYPE, 'SEC', 1, 'Hynix', 2, 'Fabless', 3, 4)", "CUST_TYPE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", "MAT_GRP_1", "CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "MAT_GRP_9", "MAT_GRP_9", "MAT_GRP_9", "MAT_GRP_9", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT_GRP_10", "MAT_GRP_10", "MAT_GRP_10", "MAT_GRP_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lead", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG Code", "MAT_CMF_11", "MAT_CMF_11", "MAT_CMF_11", "MAT_CMF_11", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT_CMF_10", "MAT_CMF_10", "MAT_CMF_10", "MAT_CMF_10", false);
        }
        #endregion
        
        #region SQL 쿼리 Build
        /// <summary 4. SQL 쿼리 Build>
        /// 
        /// </summary>
        /// <returns> strSqlString </returns>
      
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string QueryCond4 = null;            
            
            string start_date;
            string today;            
            string month;                     
            string sSumQuery1 = null;
            string sSumQuery2 = null;
            string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값
        
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            // kpcs 선택에 의한 분모 값 저장 한다.
            if (ckbKpcs.Checked == true)
            {
                if (rdbQuantity.Checked == true)
                {
                    sKpcsValue = "1000";
                }
                else
                {
                    sKpcsValue = "1000000";                    
                }
            }
            else
            {
                sKpcsValue = "1";
            }

            month = cdvDate.Value.ToString("yyyyMM");
            start_date = month + "01";
            today = cdvDate.SelectedValue();

            for (int i = 0; i < selectDate.Count; i++)
            {
                sSumQuery1 += "     , ROUND(SUM(RCV_" + i + " * PRICE) / " + sKpcsValue + ", 0)  AS RCV_" + i + "\n";
                sSumQuery2 += "                     , SUM(DECODE(WORK_DATE, '" + selectDate[i] + "', QTY / NVL(COMP_CNT,1), 0)) AS RCV_" + i + "\n";                
            }

            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond2);
            strSqlString.Append("     , ROUND(SUM(ORI_PLAN * PRICE) / " + sKpcsValue + ", 0) AS ORI_PLAN" + "\n");
            strSqlString.Append("     , ROUND(SUM(REV_PLAN * PRICE) / " + sKpcsValue + ", 0) AS REV_PLAN" + "\n");
            strSqlString.Append("     , ROUND(SUM(BOH_QTY * PRICE) / " + sKpcsValue + ", 0) AS BOH_QTY" + "\n");
            strSqlString.Append("     , ROUND(SUM(RCV_QTY * PRICE) / " + sKpcsValue + ", 0) AS RCV_QTY" + "\n");
            strSqlString.Append("     , ROUND(SUM((NVL(BOH_QTY,0) + NVL(RCV_QTY,0)) * PRICE) / " + sKpcsValue + ", 0) AS WSH_QTY" + "\n");
            strSqlString.Append("     , ROUND(DECODE(SUM(ORI_PLAN), 0, 0, ROUND(SUM(NVL(BOH_QTY,0) + NVL(RCV_QTY,0)) / SUM(ORI_PLAN) * 100,1)), 0) AS ORI_PER" + "\n");
            strSqlString.Append("     , ROUND(DECODE(SUM(REV_PLAN), 0, 0, ROUND(SUM(NVL(BOH_QTY,0) + NVL(RCV_QTY,0)) / SUM(REV_PLAN) * 100,1)), 0) AS REV_PER" + "\n");
            strSqlString.Append(sSumQuery1);            
            strSqlString.Append("  FROM (" + "\n");

            if (rdbQuantity.Checked == true)
            {
                strSqlString.Append("        SELECT MAT.*" + "\n");
                strSqlString.Append("             , NVL((SELECT DATA_10 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND  KEY_1 = MAT_GRP_1), '-') AS CUST_TYPE" + "\n");
                strSqlString.Append("             , CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(PLN.ORI_PLAN/DECODE(MAT_CMF_13,' ', 1, '-', 1, MAT_CMF_13),0) ELSE PLN.ORI_PLAN END AS ORI_PLAN" + "\n");
                strSqlString.Append("             , CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(PLN.REV_PLAN/DECODE(MAT_CMF_13,' ', 1, '-', 1, MAT_CMF_13),0) ELSE PLN.REV_PLAN END AS REV_PLAN" + "\n");
                strSqlString.Append("             , CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(BOH.BOH_QTY/DECODE(MAT_CMF_13,' ', 1, '-', 1, MAT_CMF_13),0)" + "\n");
                strSqlString.Append("                    WHEN MAT_CMF_11 IN ('DHL', 'DQZ', 'DQA', 'DRA', 'DND', 'DWG', 'DUT', 'HAU', 'HF8', 'HDM', 'HDR', 'HEN', 'HQA', 'HQC') THEN (CASE WHEN MAT_GRP_5 IN ('3rd','Merge') THEN BOH.BOH_QTY" + "\n");
                strSqlString.Append("                                                                                             WHEN MAT_GRP_5 LIKE 'Middle%' AND MAT_GRP_5 <> 'Middle' THEN BOH.BOH_QTY" + "\n");
                strSqlString.Append("                                                                                             ELSE 0 END)" + "\n");
                strSqlString.Append("                    WHEN MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND (MAT_ID LIKE 'SEKS%' OR MAT_ID LIKE 'SEKY%'))" + "\n");
                strSqlString.Append("                         THEN (CASE WHEN MAT_GRP_5 IN ('2nd','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN BOH.BOH_QTY ELSE 0 END)" + "\n");
                strSqlString.Append("                    WHEN MAT_GRP_1 = 'HX' AND MESMGR.F_GET_ATTR_VALUE@RPTTOMES(MAT.MAT_ID, 'MAT_ETC', 'HX_VERSION') = 'A-376'" + "\n");
                strSqlString.Append("                         THEN (CASE WHEN MAT_GRP_5 IN ('5th','Merge') THEN BOH.BOH_QTY ELSE 0 END)" + "\n");
                strSqlString.Append("                    WHEN MAT_GRP_1 = 'HX' AND MESMGR.F_GET_ATTR_VALUE@RPTTOMES(MAT.MAT_ID, 'MAT_ETC', 'HX_VERSION') IN ('A-445', 'A-525')" + "\n");
                strSqlString.Append("                         THEN (CASE WHEN MAT_GRP_5 IN ('6th','Merge') THEN BOH.BOH_QTY ELSE 0 END)" + "\n");
                strSqlString.Append("                    WHEN MAT_GRP_1 = 'HX' AND MESMGR.F_GET_ATTR_VALUE@RPTTOMES(MAT.MAT_ID, 'MAT_ETC', 'HX_VERSION') = 'A-524'" + "\n");
                strSqlString.Append("                         THEN (CASE WHEN MAT_GRP_5 IN ('7th','Merge') THEN BOH.BOH_QTY ELSE 0 END)" + "\n");
                strSqlString.Append("                    WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' " + "\n");
                strSqlString.Append("                         THEN (CASE WHEN MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN BOH.BOH_QTY ELSE 0 END)" + "\n");
                strSqlString.Append("                    ELSE BOH.BOH_QTY" + "\n");
                strSqlString.Append("               END BOH_QTY" + "\n");;
                strSqlString.Append("             , RCV.*" + "\n");
                strSqlString.Append("             , 1 AS PRICE" + "\n");
            }
            else
            {
                strSqlString.Append("        SELECT MAT.*" + "\n");
                strSqlString.Append("             , NVL((SELECT DATA_10 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND  KEY_1 = MAT_GRP_1), '-') AS CUST_TYPE" + "\n");
                strSqlString.Append("             , NVL(PLN.ORI_PLAN,0) AS ORI_PLAN" + "\n");
                strSqlString.Append("             , NVL(PLN.REV_PLAN,0) AS REV_PLAN" + "\n");
                strSqlString.Append("             , CASE WHEN MAT_CMF_11 IN ('DHL', 'DQZ', 'DQA', 'DRA', 'DND', 'DWG', 'DUT', 'HAU', 'HF8', 'HDM', 'HDR', 'HEN', 'HQA', 'HQC') THEN (CASE WHEN MAT_GRP_5 IN ('3rd','Merge') THEN BOH.BOH_QTY" + "\n");
                strSqlString.Append("                                                                                             WHEN MAT_GRP_5 LIKE 'Middle%' AND MAT_GRP_5 <> 'Middle' THEN BOH.BOH_QTY" + "\n");
                strSqlString.Append("                                                                                             ELSE 0 END)" + "\n");
                strSqlString.Append("                    WHEN MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND (MAT_ID LIKE 'SEKS%' OR MAT_ID LIKE 'SEKY%'))" + "\n");
                strSqlString.Append("                         THEN (CASE WHEN MAT_GRP_5 IN ('2nd','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN BOH.BOH_QTY ELSE 0 END)" + "\n");
                strSqlString.Append("                    WHEN MAT_GRP_1 = 'HX' AND MESMGR.F_GET_ATTR_VALUE@RPTTOMES(MAT.MAT_ID, 'MAT_ETC', 'HX_VERSION') = 'A-376'" + "\n");
                strSqlString.Append("                         THEN (CASE WHEN MAT_GRP_5 IN ('5th','Merge') THEN BOH.BOH_QTY ELSE 0 END)" + "\n");
                strSqlString.Append("                    WHEN MAT_GRP_1 = 'HX' AND MESMGR.F_GET_ATTR_VALUE@RPTTOMES(MAT.MAT_ID, 'MAT_ETC', 'HX_VERSION') IN ('A-445', 'A-525')" + "\n");
                strSqlString.Append("                         THEN (CASE WHEN MAT_GRP_5 IN ('6th','Merge') THEN BOH.BOH_QTY ELSE 0 END)" + "\n");
                strSqlString.Append("                    WHEN MAT_GRP_1 = 'HX' AND MESMGR.F_GET_ATTR_VALUE@RPTTOMES(MAT.MAT_ID, 'MAT_ETC', 'HX_VERSION') = 'A-524'" + "\n");
                strSqlString.Append("                         THEN (CASE WHEN MAT_GRP_5 IN ('7th','Merge') THEN BOH.BOH_QTY ELSE 0 END)" + "\n");
                strSqlString.Append("                    WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' " + "\n");
                strSqlString.Append("                         THEN (CASE WHEN MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN BOH.BOH_QTY ELSE 0 END)" + "\n");
                strSqlString.Append("                    ELSE BOH.BOH_QTY" + "\n");
                strSqlString.Append("               END BOH_QTY" + "\n");
                strSqlString.Append("             , RCV.*" + "\n");
                strSqlString.Append("             , CASE WHEN MAT_GRP_3 = 'COB' THEN 4.8" + "\n");
                strSqlString.Append("                    WHEN PRICE IS NULL OR PRICE = 1 THEN PKG_PRICE" + "\n");
                strSqlString.Append("                    ELSE PRICE " + "\n");
                strSqlString.Append("               END PRICE" + "\n");
            }

            strSqlString.Append("          FROM MWIPMATDEF MAT" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");
            strSqlString.Append("                     , SUM(PLAN_QTY_ASSY) AS ORI_PLAN " + "\n");
            strSqlString.Append("                     , SUM(DECODE(RESV_FIELD1, ' ', 0, RESV_FIELD1)) AS REV_PLAN" + "\n");
            strSqlString.Append("                  FROM CWIPPLNMON" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND PLAN_MONTH = '" + month + "' " + "\n");
            strSqlString.Append("                   AND PLAN_QTY_ASSY + DECODE(RESV_FIELD1, ' ', 0, RESV_FIELD1) > 0 " + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
            strSqlString.Append("               ) PLN" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN MESMGR.F_GET_ATTR_VALUE@RPTTOMES(MAT_ID, 'MAT_ETC', 'HX_VERSION') IN ('A-376', 'A-445')" + "\n");
            strSqlString.Append("                                     THEN (CASE WHEN OPER <= 'A0015' THEN QTY_1 / NVL(COMP_CNT,1) ELSE QTY_1 END)" + "\n");
            strSqlString.Append("                                ELSE (CASE WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1) ELSE QTY_1 END)" + "\n");
            strSqlString.Append("                           END) AS BOH_QTY" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID" + "\n");
            strSqlString.Append("                             , OPER" + "\n");
            strSqlString.Append("                             , EOH_QTY_1 AS QTY_1" + "\n");
            strSqlString.Append("                             , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') AND KEY_1 = A.MAT_ID) AS COMP_CNT " + "\n");
            strSqlString.Append("                          FROM RSUMWIPEOH A" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND WORK_DATE = TO_CHAR(TO_DATE('" + month + "' || '01')-1, 'YYYYMMDD')" + "\n");
            strSqlString.Append("                           AND SHIFT = '3'" + "\n");
            strSqlString.Append("                           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                           AND MAT_ID NOT LIKE 'HX%' " + "\n");
            strSqlString.Append("                           AND CM_KEY_3 LIKE 'P%' " + "\n");
            strSqlString.Append("                         UNION ALL " + "\n");
            strSqlString.Append("                        SELECT MAT_ID" + "\n");
            strSqlString.Append("                             , OPER" + "\n");
            strSqlString.Append("                             , EOH_QTY_1 AS QTY_1" + "\n");
            strSqlString.Append("                             , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') AND KEY_1 = A.MAT_ID) AS COMP_CNT " + "\n");
            strSqlString.Append("                          FROM RSUMWIPEOH A" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND WORK_DATE = '" + start_date + "'" + "\n");
            strSqlString.Append("                           AND SHIFT = '1'" + "\n");
            strSqlString.Append("                           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                           AND MAT_ID LIKE 'HX%' " + "\n");
            strSqlString.Append("                           AND CM_KEY_3 LIKE 'P%' " + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
            strSqlString.Append("               ) BOH" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_ID AS PRODUCT " + "\n");
            strSqlString.Append("                     , SUM(QTY / NVL(COMP_CNT,1)) AS RCV_QTY" + "\n");
            strSqlString.Append(sSumQuery2);            
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT A.WORK_DATE, A.MAT_ID" + "\n");
            strSqlString.Append("                             , CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(RCV_QTY_1/DECODE(MAT_CMF_13,' ', 1, '-', 1, MAT_CMF_13),0)" + "\n");
            strSqlString.Append("                                    WHEN MAT_CMF_11 = 'JZH' THEN ROUND(RCV_QTY_1 / 2,0)" + "\n");
            strSqlString.Append("                                    WHEN MAT_CMF_11 IN ('DHL', 'DQZ', 'DQA', 'DRA', 'DND', 'DWG', 'DUT', 'HAU', 'HF8', 'HDM', 'HDR', 'HEN', 'HQA', 'HQC') THEN DECODE(MAT_GRP_5, '3rd', RCV_QTY_1, 0)" + "\n");
            strSqlString.Append("                                    WHEN MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND (MAT_ID LIKE 'SEKS%' OR MAT_ID LIKE 'SEKY%'))" + "\n");
            strSqlString.Append("                                         THEN DECODE(MAT_GRP_5, '2nd', RCV_QTY_1, 0)" + "\n");
            strSqlString.Append("                                    WHEN MAT_GRP_1 = 'HX' AND MESMGR.F_GET_ATTR_VALUE@RPTTOMES(A.MAT_ID, 'MAT_ETC', 'HX_VERSION') = 'A-376'" + "\n");
            strSqlString.Append("                                         THEN DECODE(MAT_GRP_5, '5th', RCV_QTY_1, 0)" + "\n");
            strSqlString.Append("                                    WHEN MAT_GRP_1 = 'HX' AND MESMGR.F_GET_ATTR_VALUE@RPTTOMES(A.MAT_ID, 'MAT_ETC', 'HX_VERSION') IN ('A-445', 'A-525')" + "\n");
            strSqlString.Append("                                         THEN DECODE(MAT_GRP_5, '6th', RCV_QTY_1, 0)" + "\n");
            strSqlString.Append("                                    WHEN MAT_GRP_1 = 'HX' AND MESMGR.F_GET_ATTR_VALUE@RPTTOMES(A.MAT_ID, 'MAT_ETC', 'HX_VERSION') = 'A-524'" + "\n");
            strSqlString.Append("                                         THEN DECODE(MAT_GRP_5, '7th', RCV_QTY_1, 0)" + "\n");
            strSqlString.Append("                                    WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN MAT_GRP_5 IN ('1st','Merge') THEN RCV_QTY_1 ELSE 0 END" + "\n");
            strSqlString.Append("                                    ELSE RCV_QTY_1" + "\n");
            strSqlString.Append("                                END QTY" + "\n");
            strSqlString.Append("                             , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') AND KEY_1 = A.MAT_ID) AS COMP_CNT " + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT A.*" + "\n");
            strSqlString.Append("                                     , B.WORK_DATE, B.RCV_QTY_1, B.RCV_QTY_2" + "\n");
            strSqlString.Append("                                  FROM MWIPMATDEF A" + "\n");
            strSqlString.Append("                                     , (" + "\n");
            strSqlString.Append("                                        SELECT *" + "\n");
            strSqlString.Append("                                          FROM VSUMWIPRCV " + "\n");
            strSqlString.Append("                                         WHERE 1=1" + "\n");
            strSqlString.Append("                                           AND WORK_DATE BETWEEN '" + start_date + "' AND '" + today + "'" + "\n");
            strSqlString.Append("                                           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                           AND CM_KEY_2 = 'PROD'   " + "\n");
            strSqlString.Append("                                           AND CM_KEY_3 LIKE 'P%'" + "\n");
            strSqlString.Append("                                           AND MAT_ID NOT LIKE 'HX%'" + "\n");
            strSqlString.Append("                                         UNION ALL" + "\n");
            strSqlString.Append("                                        SELECT *" + "\n");
            strSqlString.Append("                                          FROM VSUMWIPRCV_HX " + "\n");
            strSqlString.Append("                                         WHERE 1=1" + "\n");
            strSqlString.Append("                                           AND WORK_DATE BETWEEN '" + start_date + "' AND '" + today + "'" + "\n");
            strSqlString.Append("                                           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                           AND CM_KEY_2 = 'PROD'   " + "\n");
            strSqlString.Append("                                           AND CM_KEY_3 LIKE 'P%'" + "\n");
            strSqlString.Append("                                           AND MAT_ID LIKE 'HX%'" + "\n");
            strSqlString.Append("                                       ) B" + "\n");
            strSqlString.Append("                                 WHERE 1=1" + "\n");
            strSqlString.Append("                                   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                                   AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                               ) A" + "\n");            
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
            strSqlString.Append("               ) RCV" + "\n");

            if (rdbQuantity.Checked == true)
            {
                strSqlString.Append("         WHERE 1=1" + "\n");
            }
            else if (rbdSaled.Checked == true)
            {
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID AS PRODUCT, MAX(PRICE) AS PRICE" + "\n");
                strSqlString.Append("                  FROM (" + "\n");
                strSqlString.Append("                        SELECT MAT_ID, DECODE(MAT_KEY, PRODUCT, PRICE, 1) AS PRICE" + "\n");
                strSqlString.Append("                          FROM HRTDMCPROUT@RPTTOMES A" + "\n");
                strSqlString.Append("                             , (" + "\n");
                strSqlString.Append("                                SELECT PRODUCT, PRICE" + "\n");
                strSqlString.Append("                                  FROM RPRIMATDAT" + "\n");
                strSqlString.Append("                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                   AND SUBSTR(ITEM_CD,10,2) = 'A0'  " + "\n");
                strSqlString.Append("                               ) B " + "\n");
                strSqlString.Append("                         WHERE MAT_KEY = PRODUCT(+) " + "\n");
                strSqlString.Append("                           AND MAT_ID IS NOT NULL " + "\n");
                strSqlString.Append("                         UNION " + "\n");
                strSqlString.Append("                        SELECT PRODUCT, PRICE " + "\n");
                strSqlString.Append("                          FROM RPRIMATDAT " + "\n");
                strSqlString.Append("                         WHERE 1=1 " + "\n");
                strSqlString.Append("                           AND SUBSTR(ITEM_CD,10,2) = 'A0' " + "\n");
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) PRI" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT B.MAT_GRP_1 AS CUST_CODE, B.MAT_CMF_11 AS PKG_CODE, ROUND(AVG(PRICE), 0) AS PKG_PRICE" + "\n");                
                strSqlString.Append("                  FROM RPRIMATDAT A" + "\n");
                strSqlString.Append("                     , MWIPMATDEF B" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND A.PRODUCT = B.MAT_ID" + "\n");
                strSqlString.Append("                   AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND B.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                   AND SUBSTR(ITEM_CD,10,2) = 'A0' " + "\n");
                strSqlString.Append("                   AND NVL(A.PRICE,0) > 0 " + "\n");
                strSqlString.Append("                   AND A.PRODUCT LIKE 'SE%' " + "\n");                
                strSqlString.Append("                 GROUP BY MAT_GRP_1, MAT_CMF_11" + "\n");
                strSqlString.Append("               ) PRI2" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PRI.PRODUCT(+)" + "\n");
                strSqlString.Append("           AND MAT.MAT_CMF_11 = PRI2.PKG_CODE(+)" + "\n");
                strSqlString.Append("           AND MAT.MAT_GRP_1 = PRI2.CUST_CODE(+)" + "\n");
            }
            else
            {
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID AS PRODUCT, MAX(PRICE) AS PRICE" + "\n");
                strSqlString.Append("                  FROM (" + "\n");
                strSqlString.Append("                        SELECT MAT_ID, DECODE(MAT_KEY, PRODUCT, PRICE, 1) AS PRICE" + "\n");
                strSqlString.Append("                          FROM HRTDMCPROUT@RPTTOMES A" + "\n");
                strSqlString.Append("                             , (" + "\n");
                strSqlString.Append("                                SELECT PRODUCT, NVL(A_PRICE,0) + NVL(T_PRICE,0) AS PRICE" + "\n");
                strSqlString.Append("                                  FROM (" + "\n");
                strSqlString.Append("                                        SELECT PRODUCT, PRICE AS A_PRICE " + "\n");
                strSqlString.Append("                                             , (SELECT MAX(PRICE) FROM RPRIMATDAT WHERE SUBSTR(ITEM_CD,10,2) = '0T' AND PRODUCT = A.PRODUCT) AS T_PRICE " + "\n");
                strSqlString.Append("                                          FROM RPRIMATDAT A" + "\n");
                strSqlString.Append("                                         WHERE 1=1 " + "\n");
                strSqlString.Append("                                           AND SUBSTR(ITEM_CD,10,2) = 'A0' " + "\n");
                strSqlString.Append("                                           AND NVL(A.PRICE,0) > 0 " + "\n");
                strSqlString.Append("                                       )" + "\n");                
                strSqlString.Append("                               ) B " + "\n");
                strSqlString.Append("                         WHERE MAT_KEY = PRODUCT(+) " + "\n");
                strSqlString.Append("                           AND MAT_ID IS NOT NULL " + "\n");
                strSqlString.Append("                         UNION " + "\n");
                strSqlString.Append("                        SELECT PRODUCT, NVL(A_PRICE,0) + NVL(T_PRICE,0) AS PRICE" + "\n");
                strSqlString.Append("                          FROM (" + "\n");
                strSqlString.Append("                                SELECT PRODUCT, PRICE AS A_PRICE " + "\n");
                strSqlString.Append("                                     , (SELECT MAX(PRICE) FROM RPRIMATDAT WHERE SUBSTR(ITEM_CD,10,2) = '0T' AND PRODUCT = A.PRODUCT) AS T_PRICE " + "\n");
                strSqlString.Append("                                  FROM RPRIMATDAT A" + "\n");
                strSqlString.Append("                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                   AND SUBSTR(ITEM_CD,10,2) = 'A0' " + "\n");
                strSqlString.Append("                                   AND NVL(A.PRICE,0) > 0 " + "\n");
                strSqlString.Append("                               )" + "\n");              
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) PRI" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_GRP_1 AS CUST_CODE, MAT_CMF_11 AS PKG_CODE, ROUND(NVL(AVG(A_PRICE),0) + NVL(AVG(T_PRICE),0),0) AS PKG_PRICE" + "\n");
                strSqlString.Append("                  FROM (" + "\n");
                strSqlString.Append("                        SELECT B.MAT_GRP_1, B.MAT_CMF_11, PRICE AS A_PRICE" + "\n");
                strSqlString.Append("                             , (SELECT MAX(PRICE) FROM RPRIMATDAT WHERE SUBSTR(ITEM_CD,10,2) = '0T' AND PRODUCT = A.PRODUCT) AS T_PRICE" + "\n");
                strSqlString.Append("                          FROM RPRIMATDAT A" + "\n");
                strSqlString.Append("                             , MWIPMATDEF B" + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                strSqlString.Append("                           AND A.PRODUCT = B.MAT_ID" + "\n");
                strSqlString.Append("                           AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                           AND B.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                           AND SUBSTR(ITEM_CD,10,2) = 'A0' " + "\n");
                strSqlString.Append("                           AND NVL(A.PRICE,0) > 0 " + "\n");
                strSqlString.Append("                           AND A.PRODUCT LIKE 'SE%' " + "\n");
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("                 GROUP BY MAT_GRP_1, MAT_CMF_11" + "\n");
                strSqlString.Append("               ) PRI2" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PRI.PRODUCT(+)" + "\n");
                strSqlString.Append("           AND MAT.MAT_CMF_11 = PRI2.PKG_CODE(+)" + "\n");
                strSqlString.Append("           AND MAT.MAT_GRP_1 = PRI2.CUST_CODE(+)" + "\n");
            }

            strSqlString.Append("           AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = BOH.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = RCV.PRODUCT(+)" + "\n");
            strSqlString.Append("           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("           AND MAT.MAT_TYPE = 'FG'" + "\n");
            // 2018-06-05-임종우 : Hynix Partial Wafer Dummy - PW 제외 (박형순대리 요청)
            strSqlString.Append("           AND MAT.MAT_GRP_2 NOT IN ('-', 'PW')" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            //상세 조회에 따른 SQL문 생성                        
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

            strSqlString.Append("       ) " + "\n");
            strSqlString.AppendFormat(" GROUP BY {0} " + "\n", QueryCond1);            
            strSqlString.Append(" HAVING SUM(NVL(ORI_PLAN,0)) + SUM(NVL(REV_PLAN,0)) + SUM(NVL(BOH_QTY,0)) + SUM(NVL(RCV_QTY,0)) > 0" + "\n");
            strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond3);
            
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion
              

        #region EVENT 처리
        /// <summary 5. View>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;         
            if (CheckField() == false) return;                      
            
            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);              
                this.Refresh();
                GridColumnInit();

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());                                

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                ////그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 7, null, null, btnSort);
                spdData.RPT_FillDataSelectiveCells("Total", 0, 7, 0, 1, true, Align.Center, VerticalAlign.Center);

                SetAvgVertical01(1, 12);
                SetAvgVertical02(1, 13);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);
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

        /// <summary 6. Excel Export>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            // Excel 바로 보이기
            ExcelHelper.Instance.subMakeExcel(spdData, null, this.lblTitle.Text, null, null);
            //spdData.ExportExcel();           
        }
        #endregion

        private void PRD011007_Load(object sender, EventArgs e)
        {
            //기본적으로 Detail이 보이도록..
            //pnlWIPDetail.Visible = true;
        }


        private void rdbQuantity_Click(object sender, EventArgs e)
        {
            ckbKpcs.Text = "Kcps";
        }

        private void rbdSaled_Click(object sender, EventArgs e)
        {
            ckbKpcs.Text = "one million won";
        }

        private void rdbTurnkey_Click(object sender, EventArgs e)
        {
            ckbKpcs.Text = "one million won";
        }

        /// <summary>
        /// 2013.06.07 손광섭
        /// AVG 구하기.. SubTotal, GrandTotal 구할때 특정 컬럼 내용들로 직접 구할때..
        /// </summary>
        /// <param name="nSampleNormalRowPos"></param>
        /// <param name="nColPos"></param>
        public void SetAvgVertical01(int nSampleNormalRowPos, int nColPos)
        {
            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;
            double PlanCnt = 0;
            double ShipCnt = 0;

            PlanCnt = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 7].Value);
            ShipCnt = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 11].Value);
            if (PlanCnt > 0) spdData.ActiveSheet.Cells[0, nColPos].Value = Convert.ToDecimal((ShipCnt / PlanCnt) * 100);

            for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                if (spdData.ActiveSheet.Cells[i, nColPos].BackColor != color)
                {
                    PlanCnt = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 7].Value);
                    ShipCnt = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 11].Value);
                    if (PlanCnt > 0) spdData.ActiveSheet.Cells[i, nColPos].Value = Convert.ToDecimal((ShipCnt / PlanCnt) * 100);
                }
            }
        }

        /// <summary>
        /// 2013.06.07 손광섭
        /// AVG 구하기.. SubTotal, GrandTotal 구할때 특정 컬럼 내용들로 직접 구할때..
        /// </summary>
        /// <param name="nSampleNormalRowPos"></param>
        /// <param name="nColPos"></param>
        public void SetAvgVertical02(int nSampleNormalRowPos, int nColPos)
        {
            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;
            double PlanCnt = 0;
            double ShipCnt = 0;

            PlanCnt = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 8].Value);
            ShipCnt = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 11].Value);
            if (PlanCnt > 0) spdData.ActiveSheet.Cells[0, nColPos].Value = Convert.ToDecimal((ShipCnt / PlanCnt) * 100);

            for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                if (spdData.ActiveSheet.Cells[i, nColPos].BackColor != color)
                {
                    PlanCnt = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 8].Value);
                    ShipCnt = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 11].Value);
                    if (PlanCnt > 0) spdData.ActiveSheet.Cells[i, nColPos].Value = Convert.ToDecimal((ShipCnt / PlanCnt) * 100);
                }
            }
        }

        #region "Imitate Excel Sum"
        private bool IsNumeric(string value)
        {
            value = value.Trim();
            value = value.Replace(".", "");

            foreach (char cData in value)
            {
                if (false == Char.IsNumber(cData))
                {
                    return false;
                }
            }
            return true;
        }

        private void spdData_KeyUp(object sender, KeyEventArgs e)
        {
            if (spdData.ActiveSheet.RowCount <= 0) return;

            FarPoint.Win.Spread.Model.CellRange crRange;

            try
            {
                crRange = spdData.ActiveSheet.GetSelection(0);
                spdData.ActiveSheet.GetClip(crRange.Row, crRange.Column, crRange.RowCount, crRange.ColumnCount);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "You didn't make a selection!!");
                return;
            }

            double dblSum = 0; long lngCnt = 0;
            for (int ii = crRange.Row; ii < crRange.Row + crRange.RowCount; ii++)
            {
                for (int kk = crRange.Column; kk < crRange.Column + crRange.ColumnCount; kk++)
                {
                    string strTmpValue = spdData.ActiveSheet.Cells[ii, kk].Text;
                    strTmpValue = strTmpValue.Replace(",", "");
                    if (strTmpValue.Trim() == "") strTmpValue = "0";

                    if (this.IsNumeric(strTmpValue))
                    {
                        dblSum += double.Parse(strTmpValue);
                    }
                    else
                    {
                        lblNumericSum.Text = "Characters included.";
                        return;
                    }
                    lngCnt++;
                }
            }

            if (lngCnt > 0 && dblSum != 0)
                lblNumericSum.Text = "개수: " + lngCnt.ToString("#,###").Trim() + "    합계: " + dblSum.ToString("#,###").Trim();
            else
                lblNumericSum.Text = "";
        }

        private void spdData_MouseUp(object sender, MouseEventArgs e)
        {
            spdData_KeyUp(sender, new KeyEventArgs(Keys.Return));
        }
        #endregion        
    }
}
