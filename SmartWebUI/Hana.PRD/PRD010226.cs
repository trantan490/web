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
    public partial class PRD010226 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        decimal jindoPer;
        int iLastday;
        int iToday = 0;
        double iRemain = 0;        
        /// <summary>
        /// 클  래  스: PRD010226<br/>
        /// 클래스요약: ASSY 월간 진도관리<br/>
        /// 작  성  자: 임종우<br/>
        /// 최초작성일: 2018-04-02<br/>
        /// 상세  설명: ASSY 월간 진도관리(김성업K 요청)<br/>
        /// 변경  내용: <br/> 
        /// 2018-04-05-임종우 : Stack 정보 5로 고정 (김성업K 요청)
        /// 2018-04-09-임종우 : Rev 계획 Check Box 추가 (김성업K 요청)
        /// 2018-04-25-임종우 : 환산 Check Box 추가, Ship 실적 추가 (김성업과장 요청)
        /// 2018-05-15-임종우 : WF 진도차 로직 수정 -> (( WF 실적 / 월계획) - ((경과일 + 5일) / Workday )) * work day  (임태성차장 요청)
        /// 2018-06-11-임종우 : PO 미발행 HOLD CODE 제외 - H71, H54 (김성업과장 요청)
        ///                   : 재공 -> Hynix HX_VERSION = 'A-524' 제품 - 7차, Merge 기준 (박형순대리 요청)        
        /// 2018-07-04-임종우 : 재공 -> Hynix HX_VERSION = 'A-525' 제품 - 6차, Merge 기준 (박형순대리 요청)
        ///                   : Hynix 실적 06시 기준으로 변경 (박형순대리 요청)
        /// 2018-10-01-임종우 : LOT TYPE 검색 기능 추가 (임태성차장 요청)
        /// 2018-12-28-임종우 : Stock 재공 H72 Hold 제외 (김성업과장 요청)
        /// 2019-09-19-임종우 : 금일의 경우 잔여일 계산 시 시간 단위까지 계산되도록 수정 (임태성차장 요청)
        /// 2020-04-28-김미경 : 제품 TYPE(STACK) 추가 반영 (이승희 D)
        /// 2020-05-04-김미경 : A/O 실적에 AZ010 WIP 재공 반영 (이승희 D)
        /// 2020-05-04-김미경 : COB 예외 CHECK BOX 생성 (이승희 D)
        /// 2020-09-10-임종우 : V2 로직 분리 - 월 실적 DR 고객사는 전월 26일 ~ 금월 25일 기준으로 변경 (김성업과장 요청)
        /// 2020-11-23-임종우 : 월 실적 DR 고객사 1일 ~ 말일 기준으로 다시 변경 (김성업과장 요청)
        /// </summary>
        public PRD010226()
        {
            InitializeComponent();                                   
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;    
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            //if (cdvFactory.Text.TrimEnd() == "")
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
            //    return false;
            //}

            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();

            LabelTextChange();
            
            try
            {                
                spdData.RPT_AddBasicColumn("CUST TYPE", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("MAJOR CODE", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("CUSTOMER", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);                
                spdData.RPT_AddBasicColumn("PACKAGE", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("TYPE1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("TYPE2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("LD COUNT", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("PKG CODE", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("DENSITY", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("GENERATION", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);                    
                                        
                spdData.RPT_AddBasicColumn("Monthly plan", 0, 12, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);                
                    
                spdData.RPT_AddBasicColumn("A/O", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("actual", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Progress difference", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 13, 2);

                spdData.RPT_AddBasicColumn("WF", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("actual", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Progress difference", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 15, 2);

                spdData.RPT_AddBasicColumn("WIP", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WIP days", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("a daily goal", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("actual", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("WF", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DP", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("WB", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("CLOSE", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("SHIP", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 20, 6);                    

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
                spdData.RPT_MerageHeaderRowSpan(0, 12, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 17, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 18, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 19, 2);
                spdData.RPT_ColumnConfigFromTable(btnSort);
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST TYPE", "A.CUST_TYPE", "A.CUST_TYPE", "DECODE(CUST_TYPE, 'SEC', 1, 'Hynix', 2, 'Fabless', 3, 4)", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "A.MAT_GRP_9 AS MAJOR", "A.MAT_GRP_9", "A.MAT_GRP_9", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "(SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.MAT_GRP_1) AS CUSTOMER", "A.MAT_GRP_1", "DECODE(A.MAT_GRP_1, 'SE', 1, 'HX', 2, 3),CUSTOMER", false);            
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "A.MAT_GRP_10 AS PKG", "A.MAT_GRP_10", "A.MAT_GRP_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "A.MAT_GRP_4 AS TYPE1", "A.MAT_GRP_4", "A.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "A.MAT_GRP_5 AS TYPE2", "A.MAT_GRP_5", "A.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "A.MAT_GRP_6 AS LD_COUNT", "A.MAT_GRP_6", "A.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "A.MAT_CMF_11 AS PKG_CODE", "A.MAT_CMF_11", "A.MAT_CMF_11", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "A.MAT_GRP_7 AS DENSITY", "A.MAT_GRP_7", "A.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "A.MAT_GRP_8 AS GENERATION", "A.MAT_GRP_8", "A.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "A.MAT_CMF_10 AS PIN_TYPE", "A.MAT_CMF_10", "A.MAT_CMF_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.CONV_MAT_ID AS MAT_ID", "A.CONV_MAT_ID", "A.CONV_MAT_ID", false);            
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
            string start_date;
            string start_date_dr;
            string end_date_dr;
            string date;
            string month;
            string year;
            string sKcpkValue;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;            
            
            date = cdvDate.SelectedValue();

            DateTime Select_date;
            Select_date = DateTime.Parse(cdvDate.Text.ToString());

            year = Select_date.ToString("yyyy");
            month = Select_date.ToString("yyyyMM");
            start_date = month + "01";

            start_date_dr = Select_date.AddMonths(-1).ToString("yyyyMM") + "26";
            end_date_dr = month + "25";

            if (ckbKpcs.Checked == true)
            {
                sKcpkValue = "1000";
            }
            else
            {
                sKcpkValue = "1";
            }
                        
            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond1);
            strSqlString.Append("     , ROUND(SUM(MON_PLN) / " + sKcpkValue + ", 0) AS MON_PLN" + "\n");
            strSqlString.Append("     , ROUND(SUM(SHP_TTL) / " + sKcpkValue + ", 0) AS SHP_TTL" + "\n");
            strSqlString.Append("     , CASE WHEN SUM(MON_PLN) = 0 THEN 0" + "\n");
            strSqlString.Append("            ELSE ROUND((SUM(SHP_TTL) / (SUM(MON_PLN) / " + iLastday + ")) - " + iToday + ", 1)" + "\n");
            strSqlString.Append("       END AS SHP_JINDO" + "\n");

            if (QueryCond2.IndexOf("CONV_MAT_ID") != -1 & QueryCond2.IndexOf("MAT_GRP_10") !=-1)
            {
                strSqlString.Append("     , CASE WHEN MAT_GRP_10 = 'EMCP' AND INSTR(A.CONV_MAT_ID, '1ST') != 0 THEN NULL" + "\n");
                strSqlString.Append("       ELSE ROUND((SUM(ONLY_SHP) + SUM(WIP_TTL)) / " + sKcpkValue + ", 0) END AS RCV_TTL" + "\n");
            }
            else
            {
                strSqlString.Append("      ,ROUND((SUM(ONLY_SHP) + SUM(WIP_TTL)) / " + sKcpkValue + ", 0) AS RCV_TTL" + "\n");
            }
            
            strSqlString.Append("     , CASE WHEN SUM(MON_PLN) = 0 THEN 0" + "\n");
            strSqlString.Append("            ELSE ROUND(( ((SUM(SHP_TTL) + SUM(WIP_TTL)) / SUM(MON_PLN)) - ((" + iToday + " + 5) / " + iLastday + ") ) * " + iLastday + ", 1)" + "\n");
            strSqlString.Append("       END AS RCV_JINDO" + "\n");
            //strSqlString.Append("     , CASE WHEN SUM(MON_PLN) = 0 THEN 0" + "\n");
            //strSqlString.Append("            ELSE ROUND(((SUM(SHP_TTL) + SUM(WIP_TTL)) / (SUM(MON_PLN) / (" + iLastday + "- 5))) - " + iToday + ", 1)" + "\n");
            //strSqlString.Append("       END AS RCV_JINDO" + "\n");
            strSqlString.Append("     , ROUND(SUM(WIP_TTL) / " + sKcpkValue + ", 0) AS WIP_TTL" + "\n");
            strSqlString.Append("     , CASE WHEN SUM(MON_PLN) = 0 THEN 0" + "\n");
            strSqlString.Append("            ELSE ROUND(SUM(WIP_TTL) / (SUM(MON_PLN) / " + iLastday + "), 1)" + "\n");
            strSqlString.Append("       END AS WIP_DAY" + "\n");

            if (iRemain != 0)  // 잔여일이 0 일이 아닐때
            {
                strSqlString.Append("     , ROUND((SUM(MON_PLN) - SUM(SHP_TTL)) / " + iRemain + " / " + sKcpkValue + ", 0) AS TARGET_QTY" + "\n");
            }
            else  // 잔여일이 0 일 일대
            {
                strSqlString.Append("     , 0 AS TARGET_QTY " + "\n");
            }

            strSqlString.Append("     , ROUND(SUM(RCV_TODAY) / " + sKcpkValue + ", 0) AS RCV_TODAY" + "\n");
            strSqlString.Append("     , ROUND(SUM(OUT_DP) / " + sKcpkValue + ", 0) AS OUT_DP" + "\n");
            strSqlString.Append("     , ROUND(SUM(OUT_DA) / " + sKcpkValue + ", 0) AS OUT_DA" + "\n");
            strSqlString.Append("     , ROUND(SUM(OUT_WB) / " + sKcpkValue + ", 0) AS OUT_WB" + "\n");
            strSqlString.Append("     , ROUND(SUM(OUT_CLOSE) / " + sKcpkValue + ", 0) AS OUT_CLOSE" + "\n");
            strSqlString.Append("     , ROUND(SUM(SHP_TODAY) / " + sKcpkValue + ", 0) AS SHP_TODAY" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT NVL((SELECT DATA_10 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND  KEY_1 = MAT_GRP_1), '-') AS CUST_TYPE" + "\n");
            strSqlString.Append("             , MAT.CONV_MAT_ID, MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5" + "\n");
            strSqlString.Append("             , MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, MAT_GRP_9, MAT_GRP_10, MAT_CMF_10, MAT_CMF_11" + "\n");
            //strSqlString.Append("             , CASE WHEN MAT_GRP_4 IN ('-','FD','FU') THEN 5" + "\n");
            //strSqlString.Append("                    WHEN SUBSTR(MAT_GRP_4, 3) <= 4 THEN 5" + "\n");
            //strSqlString.Append("                    ELSE SUBSTR(MAT_GRP_4, 3) + 1" + "\n");
            //strSqlString.Append("               END AS STACK" + "\n");
            strSqlString.Append("             , NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(PLN.MON_PLN/MAT.NET_DIE,0) ELSE PLN.MON_PLN END, 0) * PRICE AS MON_PLN" + "\n");
            //strSqlString.Append("             , NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SHP.SHP_TTL/MAT.NET_DIE,0) ELSE SHP.SHP_TTL END, 0) * PRICE AS SHP_TTL " + "\n");     
            strSqlString.Append("             , (NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SHP.SHP_TTL/MAT.NET_DIE,0) ELSE SHP.SHP_TTL END, 0) + DECODE(SUBSTR('" + date + "', -2), '01', 0, NVL(WIP.WIP_AZ010, 0))) * PRICE AS SHP_TTL " + "\n");
            strSqlString.Append("             , NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SHP.SHP_TTL/MAT.NET_DIE,0) ELSE SHP.SHP_TTL END, 0) * PRICE AS ONLY_SHP" + "\n");//2020-09-22-이희석 : 생산운영그룹(이승희 대리) 요청으로 수정
            strSqlString.Append("             , NVL(CASE WHEN MAT.MAT_CMF_11 = 'JZH' THEN NVL(RCV.RCV_TTL,0)/MAT.COMP_CNT/2" + "\n");
            strSqlString.Append("                        WHEN MAT.MAT_CMF_11 IN ('DHL', 'DQZ', 'DRA', 'DQA', 'DND', 'DWG', 'DUT', 'HAU', 'HF8', 'HDM', 'HDR', 'HEN', 'HQA', 'HQC') THEN DECODE(MAT.MAT_GRP_5, '3rd', NVL(RCV.RCV_TTL,0)/MAT.COMP_CNT, 0)" + "\n");
            strSqlString.Append("                        WHEN MAT.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND (MAT_ID LIKE 'SEKS%' OR MAT_ID LIKE 'SEKY%')) THEN DECODE(MAT.MAT_GRP_5, '2nd', NVL(RCV.RCV_TTL,0)/MAT.COMP_CNT, 0)" + "\n");
            strSqlString.Append("                        WHEN MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' THEN CASE WHEN MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%' THEN NVL(RCV.RCV_TTL,0)/MAT.COMP_CNT ELSE 0 END" + "\n");
            strSqlString.Append("                        WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(RCV.RCV_TTL/NET_DIE,0)" + "\n");
            strSqlString.Append("                        ELSE NVL(RCV.RCV_TTL,0)/MAT.COMP_CNT" + "\n");
            strSqlString.Append("                   END,0) * PRICE AS RCV_TTL" + "\n");
            strSqlString.Append("             , NVL(CASE WHEN MAT.MAT_CMF_11 = 'JZH' THEN NVL(RCV.RCV_TODAY,0)/MAT.COMP_CNT/2" + "\n");
            strSqlString.Append("                        WHEN MAT.MAT_CMF_11 IN ('DHL', 'DQZ', 'DRA', 'DQA', 'DND', 'DWG', 'DUT', 'HAU', 'HF8', 'HDM', 'HDR', 'HEN', 'HQA', 'HQC') THEN DECODE(MAT.MAT_GRP_5, '3rd', NVL(RCV.RCV_TODAY,0)/MAT.COMP_CNT, 0)" + "\n");
            strSqlString.Append("                        WHEN MAT.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND (MAT_ID LIKE 'SEKS%' OR MAT_ID LIKE 'SEKY%')) THEN DECODE(MAT.MAT_GRP_5, '2nd', NVL(RCV.RCV_TODAY,0)/MAT.COMP_CNT, 0)" + "\n");
            strSqlString.Append("                        WHEN MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' THEN CASE WHEN MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%' THEN NVL(RCV.RCV_TODAY,0)/MAT.COMP_CNT ELSE 0 END" + "\n");
            strSqlString.Append("                        WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(RCV.RCV_TODAY/NET_DIE,0)" + "\n");
            strSqlString.Append("                        ELSE NVL(RCV.RCV_TODAY,0)/MAT.COMP_CNT" + "\n");
            strSqlString.Append("                   END,0) * PRICE AS RCV_TODAY" + "\n");
            strSqlString.Append("             , NVL(WIP.WIP_TTL,0) * PRICE AS WIP_TTL" + "\n");
            strSqlString.Append("             , NVL(WIP.WIP_AZ010,0) * PRICE AS WIP_AZ010" + "\n");
            strSqlString.Append("             , NVL(OUT_DP,0) * PRICE AS OUT_DP" + "\n");
            strSqlString.Append("             , NVL(OUT_DA,0) * PRICE AS OUT_DA" + "\n");
            strSqlString.Append("             , NVL(OUT_WB,0) * PRICE AS OUT_WB" + "\n");
            strSqlString.Append("             , NVL(OUT_CLOSE,0) * PRICE AS OUT_CLOSE" + "\n");
            strSqlString.Append("             , NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SHP.SHP_TODAY/MAT.NET_DIE,0) ELSE SHP.SHP_TODAY END, 0) * PRICE AS SHP_TODAY " + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT A.MAT_ID, MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5" + "\n");
            strSqlString.Append("                     , MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, MAT_GRP_9, MAT_GRP_10, MAT_CMF_10, MAT_CMF_11" + "\n");
            strSqlString.Append("                     , NET_DIE, COMP_CNT, HX_COMP_MIN, HX_COMP_MAX " + "\n");
            strSqlString.Append("                     , CASE WHEN MAT_GRP_1 = 'SE' AND MAT_GRP_9 = 'MEMORY' THEN 'SEK_________-___' || SUBSTR(A.MAT_ID, -3) " + "\n");
            strSqlString.Append("                            WHEN MAT_GRP_1 = 'HX' THEN MAT_CMF_10 " + "\n");
            strSqlString.Append("                            ELSE A.MAT_ID " + "\n");
            strSqlString.Append("                       END CONV_MAT_ID " + "\n");

            if (ckbConv.Checked == true)
            {
                strSqlString.Append("                       , CASE WHEN MAT_GRP_4 IN ('-', 'FD', 'FU') THEN '1'" + "\n");
                strSqlString.Append("                              WHEN MAT_GRP_4 IN ('DDP') THEN '2'" + "\n");
                strSqlString.Append("                              WHEN MAT_GRP_4 IN ('QDP') THEN '4'" + "\n");
                strSqlString.Append("                              WHEN MAT_GRP_4 IN ('ODP') THEN '8'" + "\n");
                strSqlString.Append("                              ELSE REGEXP_REPLACE(MAT_GRP_4, '[^[:digit:]]')" + "\n");
                strSqlString.Append("                         END AS PRICE" + "\n");
                //strSqlString.Append("                     , CASE WHEN MAT_GRP_4 IN ('-','FD','FU') THEN '1' ELSE SUBSTR(MAT_GRP_4, 3) END AS PRICE " + "\n");
            }
            else
            {
                strSqlString.Append("                     , 1 AS PRICE" + "\n");
            }
            
            strSqlString.Append("                  FROM VWIPMATDEF A" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND A.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND A.MAT_GRP_2 NOT IN ('-', 'PW') " + "\n");

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
            {
                strSqlString.Append("                   AND A.MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");
            }

            if (ckbCOB.Checked == true)
                strSqlString.Append("                   AND A.MAT_GRP_3 <> 'COB' " + "\n");

            #region 상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                   AND A.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

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
            #endregion

            strSqlString.Append("               ) MAT" + "\n");
            strSqlString.Append("             , (                " + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");

            if (ckbRevPlan.Checked == true)
            {
                strSqlString.Append("                     , SUM(RESV_FIELD1) AS MON_PLN" + "\n");
            }
            else
            {
                strSqlString.Append("                     , SUM(PLAN_QTY_ASSY) AS MON_PLN" + "\n");
            }

            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, SUM(TO_NUMBER(DECODE(RESV_FIELD1,' ',0,RESV_FIELD1))) AS RESV_FIELD1" + "\n");
            strSqlString.Append("                          FROM CWIPPLNMON " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND PLAN_MONTH = '" + month + "'" + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                       ) " + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID" + "\n");            
            strSqlString.Append("               ) PLN" + "\n");

            // 2020-09-10-임종우 : V2 로직 분리 - 월 실적 DR 고객사는 전월 26일 ~ 금월 25일 기준으로 변경 (김성업과장 요청)
            #region HMK (SHP, RCV)
            if (GlobalVariable.gsGlovalSite == "K1")
            {
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append("                     , SUM(SHP_QTY_1) AS SHP_TTL " + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + date + "', SHP_QTY_1, 0)) AS SHP_TODAY" + "\n");
                strSqlString.Append("                  FROM VSUMWIPOUT" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "' " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                   AND CM_KEY_2 = 'PROD' " + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                   AND MAT_ID NOT LIKE 'HX%'" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("                 UNION ALL" + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append("                     , SUM(SHP_QTY_1) AS SHP_TTL " + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + date + "', SHP_QTY_1, 0)) AS SHP_TODAY" + "\n");
                strSqlString.Append("                  FROM VSUMWIPOUT_06" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "' " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                   AND CM_KEY_2 = 'PROD' " + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                   AND MAT_ID LIKE 'HX%'" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) SHP" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append("                     , SUM(RCV_QTY_1) AS RCV_TTL" + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + date + "', RCV_QTY_1, 0)) AS RCV_TODAY" + "\n");
                strSqlString.Append("                  FROM VSUMWIPRCV" + "\n");
                strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "'" + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "' " + "\n");
                }

                strSqlString.Append("                   AND MAT_ID NOT LIKE 'HX%'" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("                 UNION ALL" + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append("                     , SUM(RCV_QTY_1) AS RCV_TTL" + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + date + "', RCV_QTY_1, 0)) AS RCV_TODAY" + "\n");
                strSqlString.Append("                  FROM VSUMWIPRCV_HX" + "\n");
                strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "'" + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "' " + "\n");
                }

                strSqlString.Append("                   AND MAT_ID LIKE 'HX%'" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) RCV" + "\n");
            }
            #endregion

            #region HMV2 (SHP, RCV)
            else
            {
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append("                     , SUM(SHP_QTY_1) AS SHP_TTL " + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + date + "', SHP_QTY_1, 0)) AS SHP_TODAY" + "\n");
                strSqlString.Append("                  FROM VSUMWIPOUT" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "' " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                   AND CM_KEY_2 = 'PROD' " + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                //strSqlString.Append("                   AND MAT_ID NOT LIKE 'DR%'" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                //strSqlString.Append("                 UNION ALL" + "\n");
                //strSqlString.Append("                SELECT MAT_ID" + "\n");
                //strSqlString.Append("                     , SUM(SHP_QTY_1) AS SHP_TTL " + "\n");
                //strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + date + "', SHP_QTY_1, 0)) AS SHP_TODAY" + "\n");
                //strSqlString.Append("                  FROM VSUMWIPOUT" + "\n");
                //strSqlString.Append("                 WHERE 1=1" + "\n");
                //strSqlString.Append("                   AND WORK_DATE BETWEEN '" + start_date_dr + "' AND LEAST('" + date + "', '" + end_date_dr + "') " + "\n");
                //strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                //strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                //strSqlString.Append("                   AND CM_KEY_2 = 'PROD' " + "\n");

                //if (cdvLotType.Text != "ALL")
                //{
                //    strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                //}

                //strSqlString.Append("                   AND MAT_ID LIKE 'DR%'" + "\n");
                //strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) SHP" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append("                     , SUM(RCV_QTY_1) AS RCV_TTL" + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + date + "', RCV_QTY_1, 0)) AS RCV_TODAY" + "\n");
                strSqlString.Append("                  FROM VSUMWIPRCV" + "\n");
                strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "'" + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "' " + "\n");
                }

                //strSqlString.Append("                   AND MAT_ID NOT LIKE 'DR%'" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                //strSqlString.Append("                 UNION ALL" + "\n");
                //strSqlString.Append("                SELECT MAT_ID" + "\n");
                //strSqlString.Append("                     , SUM(RCV_QTY_1) AS RCV_TTL" + "\n");
                //strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + date + "', RCV_QTY_1, 0)) AS RCV_TODAY" + "\n");
                //strSqlString.Append("                  FROM VSUMWIPRCV" + "\n");
                //strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                //strSqlString.Append("                   AND WORK_DATE BETWEEN '" + start_date_dr + "' AND LEAST('" + date + "', '" + end_date_dr + "') " + "\n");                
                //strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");

                //if (cdvLotType.Text != "ALL")
                //{
                //    strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "' " + "\n");
                //}

                //strSqlString.Append("                   AND MAT_ID LIKE 'DR%'" + "\n");
                //strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) RCV" + "\n");
            }
            #endregion

            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");
            strSqlString.Append("                     , SUM(QTY) AS WIP_TTL" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'AZ010', QTY, 0)) AS WIP_AZ010" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID, OPER, OPER_GRP_1, MAT_GRP_1, MAT_GRP_4, MAT_GRP_5" + "\n");
            strSqlString.Append("                             , CASE WHEN HX_COMP_MIN IS NOT NULL" + "\n");
            strSqlString.Append("                                         THEN (CASE WHEN HX_COMP_MIN <> HX_COMP_MAX AND OPER > HX_COMP_MIN AND OPER <= HX_COMP_MAX THEN QTY_1 / NVL(COMP_CNT / 2,1)" + "\n");
            strSqlString.Append("                                                    WHEN OPER <= HX_COMP_MAX THEN QTY_1 / NVL(COMP_CNT,1)" + "\n");
            strSqlString.Append("                                                    ELSE QTY_1 END)" + "\n");
            strSqlString.Append("                                    WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1) " + "\n");
            strSqlString.Append("                                    ELSE QTY_1 " + "\n");
            strSqlString.Append("                               END QTY " + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT A.MAT_ID, B.OPER, B.OPER_GRP_1, C.MAT_GRP_1, C.MAT_GRP_4, C.MAT_GRP_5" + "\n");
            strSqlString.Append("                                     , CASE WHEN MAT_CMF_11 IN ('DHL', 'DQZ', 'DRA', 'DQA', 'DND', 'DWG', 'DUT', 'HAU', 'HF8', 'HDM', 'HDR', 'HEN', 'HQA', 'HQC') THEN (CASE WHEN MAT_GRP_5 IN ('3rd','Merge') THEN QTY_1" + "\n");
            strSqlString.Append("                                                                               WHEN MAT_GRP_5 LIKE 'Middle%' AND MAT_GRP_5 <> 'Middle' THEN QTY_1" + "\n");
            strSqlString.Append("                                                                               ELSE 0 END)" + "\n");
            strSqlString.Append("                                            WHEN MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND (MAT_ID LIKE 'SEKS%' OR MAT_ID LIKE 'SEKY%')) THEN CASE WHEN MAT_GRP_5 IN ('2nd','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN QTY_1 ELSE 0 END" + "\n");
            strSqlString.Append("                                            WHEN MAT_GRP_1 = 'HX' AND MESMGR.F_GET_ATTR_VALUE@RPTTOMES(A.MAT_ID, 'MAT_ETC', 'HX_VERSION') = 'A-376' THEN CASE WHEN MAT_GRP_5 IN ('5th','Merge') THEN QTY_1 ELSE 0 END" + "\n");
            strSqlString.Append("                                            WHEN MAT_GRP_1 = 'HX' AND MESMGR.F_GET_ATTR_VALUE@RPTTOMES(A.MAT_ID, 'MAT_ETC', 'HX_VERSION') IN ('A-445', 'A-525') THEN CASE WHEN MAT_GRP_5 IN ('6th','Merge') THEN QTY_1 ELSE 0 END" + "\n");
            strSqlString.Append("                                            WHEN MAT_GRP_1 = 'HX' AND MESMGR.F_GET_ATTR_VALUE@RPTTOMES(A.MAT_ID, 'MAT_ETC', 'HX_VERSION') = 'A-524' THEN CASE WHEN MAT_GRP_5 IN ('7th','Merge') THEN QTY_1 ELSE 0 END" + "\n");            
            strSqlString.Append("                                            WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN QTY_1 ELSE 0 END" + "\n");
            strSqlString.Append("                                            WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(QTY_1/C.NET_DIE,0)" + "\n");
            strSqlString.Append("                                            ELSE QTY_1" + "\n");
            strSqlString.Append("                                       END AS QTY_1" + "\n");
            strSqlString.Append("                                     , C.COMP_CNT, C.HX_COMP_MIN, C.HX_COMP_MAX " + "\n");
            strSqlString.Append("                                     , A.HOLD_CODE " + "\n");
            strSqlString.Append("                                     , CASE WHEN A.HOLD_CODE = 'H72' AND A.OPER = 'A0000' THEN 'Y' ELSE 'N' END AS CK_STOCK_H72 " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("                                  FROM RWIPLOTSTS A  " + "\n");
                strSqlString.Append("                                     , MWIPOPRDEF B  " + "\n");
                strSqlString.Append("                                     , VWIPMATDEF C" + "\n");
                strSqlString.Append("                                 WHERE 1 = 1 " + "\n");
            }
            else
            {
                strSqlString.Append("                                  FROM RWIPLOTSTS_BOH A " + "\n");
                strSqlString.Append("                                     , MWIPOPRDEF B " + "\n");
                strSqlString.Append("                                     , VWIPMATDEF C " + "\n");
                strSqlString.Append("                                 WHERE A.CUTOFF_DT = '" + cdvDate.SelectedValue() + "22' " + "\n");
            }

            strSqlString.Append("                                   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                                   AND A.FACTORY = C.FACTORY" + "\n");
            strSqlString.Append("                                   AND A.OPER = B.OPER" + "\n");
            strSqlString.Append("                                   AND A.MAT_ID = C.MAT_ID" + "\n");
            strSqlString.Append("                                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                                   AND A.LOT_TYPE = 'W' " + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                                   AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "' " + "\n");
            }

            strSqlString.Append("                                   AND A.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                                   AND C.MAT_GRP_2 <> '-'" + "\n");
            strSqlString.Append("                                   AND C.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                   AND A.HOLD_CODE NOT IN ('H71','H54') " + "\n");
            strSqlString.Append("                               )" + "\n");
            strSqlString.Append("                         WHERE CK_STOCK_H72 = 'N'" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
            strSqlString.Append("               ) WIP" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0200', 'A0230') THEN QTY ELSE 0 END) AS OUT_DP" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER LIKE 'A04%' THEN QTY" + "\n");
            strSqlString.Append("                                WHEN OPER = 'A0333' THEN QTY" + "\n");
            strSqlString.Append("                                ELSE 0" + "\n");
            strSqlString.Append("                           END) AS OUT_DA                     " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER LIKE 'A06%' THEN QTY ELSE 0 END) AS OUT_WB" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN MAT_GRP_3='WLCSP' THEN (CASE WHEN OPER = 'A0300' THEN QTY ELSE 0 END) " + "\n");
            strSqlString.Append("                           ELSE (CASE WHEN OPER = 'A2100' THEN QTY ELSE 0 END) END) AS OUT_CLOSE " + "\n");
            strSqlString.Append("                  FROM (    " + "\n");
            strSqlString.Append("                        SELECT MAT_ID, OPER, MAT_GRP_1, MAT_GRP_4, MAT_GRP_5, MAT_GRP_3" + "\n");
            strSqlString.Append("                             , CASE WHEN HX_COMP_MIN IS NOT NULL" + "\n");
            strSqlString.Append("                                         THEN (CASE WHEN HX_COMP_MIN <> HX_COMP_MAX AND OPER > HX_COMP_MIN AND OPER <= HX_COMP_MAX THEN QTY_1 / NVL(COMP_CNT / 2,1)" + "\n");
            strSqlString.Append("                                                    WHEN OPER <= HX_COMP_MAX THEN QTY_1 / NVL(COMP_CNT,1)" + "\n");
            strSqlString.Append("                                                    ELSE QTY_1 END)" + "\n");
            strSqlString.Append("                                    WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1) " + "\n");
            strSqlString.Append("                                    ELSE QTY_1 " + "\n");
            strSqlString.Append("                               END QTY" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT MAT_ID, OPER, MAT_GRP_1, MAT_GRP_4, MAT_GRP_5, MAT_GRP_9, MAT_CMF_11, MAT_GRP_3" + "\n");
            strSqlString.Append("                                     , CASE WHEN MAT_CMF_11 IN ('DHL', 'DQZ', 'DRA', 'DQA', 'DND', 'DWG', 'DUT', 'HAU', 'HF8', 'HDM', 'HDR', 'HEN', 'HQA', 'HQC') THEN (CASE WHEN MAT_GRP_5 IN ('3rd','Merge') THEN END_QTY ELSE 0 END)" + "\n");
            strSqlString.Append("                                            WHEN MAT_ID LIKE 'SEK%' AND MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND (MAT_ID LIKE 'SEKS%' OR MAT_ID LIKE 'SEKY%'))" + "\n");
            strSqlString.Append("                                                 THEN (CASE WHEN MAT_GRP_5 IN ('2nd','Merge') THEN END_QTY ELSE 0 END)" + "\n");
            strSqlString.Append("                                            WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-'" + "\n");
            strSqlString.Append("                                                 THEN (CASE WHEN MAT_GRP_5 IN ('1st','Merge') THEN END_QTY ELSE 0 END)" + "\n");
            strSqlString.Append("                                            WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(END_QTY/NET_DIE,0)" + "\n");
            strSqlString.Append("                                            ELSE END_QTY" + "\n");
            strSqlString.Append("                                       END QTY_1 " + "\n");
            strSqlString.Append("                                     , COMP_CNT, HX_COMP_MIN, HX_COMP_MAX " + "\n");
            strSqlString.Append("                                  FROM ( " + "\n");
            strSqlString.Append("                                        SELECT A.OPER" + "\n");
            strSqlString.Append("                                             , (S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1) AS END_QTY" + "\n");
            strSqlString.Append("                                             , B.*" + "\n");
            strSqlString.Append("                                          FROM RSUMWIPMOV A" + "\n");
            strSqlString.Append("                                             , VWIPMATDEF B" + "\n");
            strSqlString.Append("                                         WHERE 1=1" + "\n");
            strSqlString.Append("                                           AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                                           AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                           AND A.WORK_DATE = '" + date + "'" + "\n");
            strSqlString.Append("                                           AND A.LOT_TYPE = 'W'" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                                           AND A.CM_KEY_3 LIKE '" + cdvLotType.Text + "' " + "\n");
            }

            strSqlString.Append("                                           AND B.MAT_GRP_5 NOT LIKE 'Middle%'" + "\n");
            strSqlString.Append("                                           AND REGEXP_LIKE(A.OPER, 'A0200|A0230|A0300|A0333|A040*|A060*|A2100')" + "\n");
            strSqlString.Append("                                           AND ((A.OPER >= 'A0400' AND B.MAT_GRP_5 IN ('-', 'Merge')) OR (A.OPER < 'A0400'))" + "\n");
            strSqlString.Append("                                           AND A.MAT_ID NOT LIKE 'HX%'" + "\n");
            strSqlString.Append("                                         UNION ALL" + "\n");
            strSqlString.Append("                                        SELECT A.OPER" + "\n");
            strSqlString.Append("                                             , (S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1) AS END_QTY" + "\n");
            strSqlString.Append("                                             , B.*" + "\n");
            strSqlString.Append("                                          FROM RSUMWIPMOV A" + "\n");
            strSqlString.Append("                                             , VWIPMATDEF B" + "\n");
            strSqlString.Append("                                         WHERE 1=1" + "\n");
            strSqlString.Append("                                           AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                                           AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                           AND A.WORK_DATE = '" + date + "'" + "\n");
            strSqlString.Append("                                           AND A.LOT_TYPE = 'W'" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                                           AND A.CM_KEY_3 LIKE '" + cdvLotType.Text + "' " + "\n");
            }

            strSqlString.Append("                                           AND B.MAT_GRP_5 NOT LIKE 'Middle%'" + "\n");
            strSqlString.Append("                                           AND REGEXP_LIKE(A.OPER, 'A0200|A0230|A0333|A040*|A060*|A2100')" + "\n");
            strSqlString.Append("                                           AND ((A.OPER >= 'A0400' AND B.MAT_GRP_5 IN ('-', 'Merge')) OR (A.OPER < 'A0400'))" + "\n");
            strSqlString.Append("                                           AND A.MAT_ID LIKE 'HX%'" + "\n");
            strSqlString.Append("                                       ) " + "\n");
            strSqlString.Append("                               )" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
            strSqlString.Append("               ) MOV" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID NOT IN (SELECT MAT_ID FROM MWIPMATDEF WHERE FIRST_FLOW = 'A-BANK' AND DELETE_FLAG = ' ')" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = RCV.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = MOV.MAT_ID(+)" + "\n");            
            strSqlString.Append("       ) A" + "\n");
            strSqlString.Append(" GROUP BY " + QueryCond2 + "\n");
            strSqlString.Append(" HAVING SUM(MON_PLN) + SUM(SHP_TTL) + SUM(WIP_TTL) + SUM(RCV_TODAY) + SUM(OUT_DP) + SUM(OUT_DA) + SUM(OUT_WB) + SUM(OUT_CLOSE) + SUM(SHP_TODAY) <> 0" + "\n");
            strSqlString.Append(" ORDER BY " + QueryCond3 + "\n");
            
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
            
            return strSqlString.ToString();
        }

        #endregion


        #region EVENT 처리
        /// <summary>
        /// 6. View 버튼 Action
        /// </summary>
        private void btnView_Click(object sender, EventArgs e)
        {                       
            DataTable dt = null;

            if (CheckField() == false) return;

            GridColumnInit();
            LabelTextChange();

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

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 12, null, null, btnSort);                
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함
                                
                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 12, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);

                // Grand TTL, Sub TTL 진도차 값 공식으로 구하기
                SetAvgVertical(1, 14, 16, 18);        
       
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
                //StringBuilder Condition = new StringBuilder();
                //Condition.AppendFormat("기준일자: {0}     today: {1}      workday: {2}     remain: {3}      표준진도율: {4} " + "\n", cdvDate.Text, lblToday.Text.ToString(), lblLastDay.Text.ToString(), lblRemain.Text.ToString(), lblJindo.Text.ToString());               

                //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);
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
        }

        /// <summary>
        /// 7. 상단 Lebel 표시
        /// </summary>
        private void LabelTextChange()
        {
            DateTime getStartDate = Convert.ToDateTime(cdvDate.Value.ToString("yyyy-MM") + "-01");
            DateTime baseTime = DateTime.ParseExact(DateTime.Now.ToString("yyyyMMdd") + "220000", "yyyyMMddHHmmss", null);
            TimeSpan diff = baseTime - DateTime.Now;

            string getEndDate = getStartDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");            
            string strDate = cdvDate.Value.ToString("yyyyMMdd");            
            string sToday = strDate.Substring(6, 2);
            string sLastday = getEndDate.Substring(6, 2);

            iToday = Convert.ToInt32(sToday);
            iLastday = Convert.ToInt32(sLastday);

            double jindo = (Convert.ToDouble(sToday)) / Convert.ToDouble(sLastday) * 100;

            // 진도율 소수점 1째자리 까지 표시 (2009.08.17 임종우)
            jindoPer = Math.Round(Convert.ToDecimal(jindo),1);

            lblToday.Text = sToday + " day";
            lblLastDay.Text = sLastday + " day";

            // 금일 조회일 경우 잔여일에 금일 포함함.
            if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
            {
                iRemain = (iLastday - iToday) + (diff.TotalHours / 24);  // 2019-09-19-임종우 : 금일의 경우 잔여일 계산 시 시간 단위까지 계산되도록 수정 (임태성차장 요청)
            }
            else
            {
                iRemain = (iLastday - iToday);
            }

            lblRemain.Text = Math.Round(iRemain,2).ToString() + " day";             
            lblJindo.Text = jindoPer.ToString() + "%";

        }

        /// <summary>
        /// Grand TTL, Sub TTL 진도차, 재공일수 직접 계산
        /// </summary>
        /// <param name="nSampleNormalRowPos"></param>
        /// <param name="nColPosAO"></param>
        /// <param name="nColPosWF"></param>
        public void SetAvgVertical(int nSampleNormalRowPos, int nColPosAO, int nColPosWF, int nColPosWipDay)
        {
            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPosAO].BackColor;
            double iMonPlan = 0;
            double iAO = 0;
            double iWF = 0;
            double iWip = 0;

            iMonPlan = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 12].Value);
            iAO = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 13].Value);
            iWF = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 15].Value);
            iWip = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 17].Value);

            // 분모값이 0이 아닐경우에만 계산한다..
            if (iMonPlan != 0)
            {
                spdData.ActiveSheet.Cells[0, nColPosAO].Value = (iAO / (iMonPlan / iLastday)) - iToday;
                spdData.ActiveSheet.Cells[0, nColPosWF].Value = ((iWF / iMonPlan) - ((Convert.ToDouble(iToday) + 5) / Convert.ToDouble(iLastday))) * iLastday;
                spdData.ActiveSheet.Cells[0, nColPosWipDay].Value = iWip / (iMonPlan / iLastday);
                
                for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    if (spdData.ActiveSheet.Cells[i, nColPosAO].BackColor != color)
                    {
                        iMonPlan = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 12].Value);
                        iAO = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 13].Value);
                        iWF = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 15].Value);
                        iWip = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 17].Value);

                        if (iMonPlan != 0)
                        {
                            spdData.ActiveSheet.Cells[i, nColPosAO].Value = (iAO / (iMonPlan / iLastday)) - iToday;
                            // 스프레드에 표시 된 데이터는 이미 (실적 + 재공) 이 계산 된 상태 이므로 해당 실적 데이터만 사용 한다.
                            spdData.ActiveSheet.Cells[i, nColPosWF].Value = ((iWF / iMonPlan) - ((Convert.ToDouble(iToday) + 5) / Convert.ToDouble(iLastday))) * iLastday;
                            spdData.ActiveSheet.Cells[i, nColPosWipDay].Value = iWip / (iMonPlan / iLastday);
                        }
                    }
                }
            }
        }
        #endregion
    }       
}
