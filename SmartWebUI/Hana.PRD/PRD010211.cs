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
    public partial class PRD010211 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {        
        /// <summary>
        /// 클  래  스: PRD010211<br/>
        /// 클래스요약: FINISH 공정 생산일보<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2011-04-04<br/>
        /// 상세  설명: FINISH 공정 생산일보 조회<br/>
        /// 변경  내용: <br/>
        /// 2011-11-21-임종우 : 월 계획 제품 중복 오류 수정
        /// 2011-12-26-임종우 : MWIPCALDEF 의 작년,올해 마지막 주차 겹치는 에러 발생으로 SYS_YEAR -> PLAN_YEAR 으로 변경
        /// 2016-10-20-임종우 : Renewal 작업 진행 (한성희C 요청)
        /// </summary>

        string[] dayArry = new string[2];
        string[] dayArry1 = new string[2];

        GlobalVariable.FindWeek FindWeek = new GlobalVariable.FindWeek();
        DataTable dtOper = null;

        public PRD010211()
        {
            InitializeComponent();
            cdvDate.Value = DateTime.Now;
            SortInit();
            cboTimeBase.SelectedIndex = 0;
            GridColumnInit();            
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
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
            FindWeek = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");

            dtOper = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2());

            spdData.RPT_ColumnInit();

            LabelTextChange();

            try
            {                
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Major Code", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Family", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD Count", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Type1", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Type2", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 7, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Density", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Generation", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Product", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Cust Device", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PKG CODE", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Monthly plan", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                                        
                spdData.RPT_AddBasicColumn("Cumulative Performance", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Progress rate", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("a daily goal", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(cdvDate.Value.ToString("MM-dd"), 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("plan", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("actual", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 18, 3);

                int headerCount = 21;

                spdData.RPT_AddBasicColumn("Status of work", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);                

                for (int i = 0; i < dtOper.Rows.Count; i++)
                {
                    spdData.RPT_AddBasicColumn(dtOper.Rows[i][0].ToString(), 1, headerCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    headerCount++;
                }

                spdData.RPT_AddBasicColumn("sum", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                
                spdData.RPT_MerageHeaderColumnSpan(0, 21, dtOper.Rows.Count + 1);

                spdData.RPT_AddBasicColumn("Previous day's goal", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderRowSpan(0, spdData.ActiveSheet.Columns.Count-1, 2);

                spdData.RPT_AddBasicColumn("The previous day's performance", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderRowSpan(0, spdData.ActiveSheet.Columns.Count - 1, 2);

                spdData.RPT_AddBasicColumn("Performance difference", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderRowSpan(0, spdData.ActiveSheet.Columns.Count - 1, 2);

                spdData.RPT_AddBasicColumn("GOAL", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderRowSpan(0, spdData.ActiveSheet.Columns.Count - 1, 2);

                spdData.RPT_AddBasicColumn("Today's goal", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderRowSpan(0, spdData.ActiveSheet.Columns.Count - 1, 2);

                spdData.RPT_AddBasicColumn("Remarks(Reasons for disruptions)", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderRowSpan(0, spdData.ActiveSheet.Columns.Count - 1, 2);

                spdData.RPT_AddBasicColumn("HOLD", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderRowSpan(0, spdData.ActiveSheet.Columns.Count - 1, 2);

                spdData.RPT_AddBasicColumn("Expected Results", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderRowSpan(0, spdData.ActiveSheet.Columns.Count - 1, 2);

                spdData.RPT_AddBasicColumn("CHIP", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderRowSpan(0, spdData.ActiveSheet.Columns.Count - 1, 2);

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
                spdData.RPT_MerageHeaderRowSpan(0, 13, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 14, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 15, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 16, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 17, 2);                
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT.MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "MAT.MAT_GRP_9", "MAT.MAT_GRP_9 AS MAJOR_CODE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2 AS FAMILY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3 AS PACKAGE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6 AS \"LD COUNT\"", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4 AS TYPE1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5 AS TYPE2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10 AS PIN_TYPE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7 AS DENSITY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8 AS GENERATION", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT.MAT_ID", "MAT.MAT_ID AS PRODUCT", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST DEVICE", "MAT.MAT_CMF_7", "MAT.MAT_CMF_7 AS CUST_DEVICE", false);
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
            string start_day;
            string start_day2;
            string today;
            string yesterday;
            string year;
            string month;
            string sKpcsValue;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            // kpcs 선택에 의한 분모 값 저장 한다.
            if (ckbKpcs.Checked == true)
            {
                sKpcsValue = "1000";
            }
            else
            {
                sKpcsValue = "1";
            }

            string getStartDate = cdvDate.Value.ToString("yyyy-MM") + "-01";
            
            start_day = cdvDate.Value.ToString("yyyyMM") + "01";
            today = cdvDate.Value.ToString("yyyyMMdd");
            yesterday = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");
            year = cdvDate.Value.ToString("yyyy");
            month = cdvDate.Value.ToString("yyyyMM");

            // 2일 이후에만 기준월의 시작일을 가지고 사용함.
            if (today.Substring(6, 2) == "01")
            {
                start_day2 = dayArry1[0];
            }
            else
            {
                start_day2 = start_day;
            }

            // 지난주차의 마지막일 가져오기
            DataTable dt = null;
            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1(year, today));
            string Lastweek_lastday = dt.Rows[0][0].ToString();
            
            strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond2);
            strSqlString.Append("     , SUBSTR(MAT.MAT_ID, LENGTH(MAT.MAT_ID) - 2) AS PKG_CODE" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(MON_PLN.MON_PLAN,0)) / " + sKpcsValue + ",0) AS MON_PLAN" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(MON_AO.SHP_MONTH,0)) / " + sKpcsValue + ",0) AS SHP_MONTH" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(MON_PLN.MON_PLAN,0) - NVL(MON_AO.SHP_MONTH,0)) / " + sKpcsValue + ",0) AS MON_DEF" + "\n");
            strSqlString.Append("     , ROUND(DECODE(SUM(NVL(MON_PLN.MON_PLAN,0)), 0, 0, SUM(NVL(MON_AO.SHP_MONTH,0)) / SUM(NVL(MON_PLN.MON_PLAN,0)) * 100), 0) AS JINDO" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(MON_PLN.MON_PLAN,0)) / " + lblRemain.Text + " / " + sKpcsValue + " ,0) AS TARGET" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WEEK_PLN.D0_PLAN, 0) + (NVL(WEEK_PLN.WEEK1_TTL, 0) - NVL(MON_AO.SHP_WEEK_TTL, 0))) / " + sKpcsValue + ", 0) AS PLAN_TODAY" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WEEK_PLN.D0_PLAN, 0) + (NVL(WEEK_PLN.WEEK1_TTL, 0) - NVL(MON_AO.SHP_WEEK_TTL, 0)) - NVL(MON_AO.SHP_TODAY, 0)) / " + sKpcsValue + ", 0) AS DEF_TODAY" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(MON_AO.SHP_TODAY, 0)) / " + sKpcsValue + ", 0) AS SHP_TODAY" + "\n");            

            for (int i = 0; i < dtOper.Rows.Count; i++)
            {
                strSqlString.Append("     , ROUND(SUM(NVL(WIP." + dtOper.Rows[i][0].ToString() + ",0)) / " + sKpcsValue + ",0) AS " + dtOper.Rows[i][0].ToString() + "\n");    
            }

            strSqlString.Append("     , ROUND(SUM(NVL(WIP.WIP_TTL,0)) / " + sKpcsValue + ",0) AS WIP_TOTAL" + "\n");
            strSqlString.Append("     , '' AS ETC0     " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(MON_AO.SHP_YESTERDAY,0)) / " + sKpcsValue + ",0) AS SHP_YESTERDAY" + "\n");
            strSqlString.Append("     , '' AS ETC1" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(MON_PLN.MON_PLAN,0) - NVL(MON_AO.SHP_MONTH,0)) / " + lblRemain.Text + " / " + sKpcsValue + ",0) AS GOAL" + "\n");
            strSqlString.Append("     , '' AS ETC2" + "\n");
            strSqlString.Append("     , '' AS ETC3" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.HOLD_WIP,0)) / " + sKpcsValue + ",0) AS HOLD_WIP" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.WIP_TTL,0) - NVL(WIP.HOLD_WIP,0)) / " + sKpcsValue + ",0) AS FORECAST_SHP" + "\n");
            strSqlString.Append("     , ROUND(SUM((NVL(WIP.WIP_TTL,0) - NVL(WIP.HOLD_WIP,0)) * NVL(SUBSTR(MAT.MAT_GRP_4, 3),1)) / " + sKpcsValue + ",0) AS CHIP" + "\n");
            strSqlString.Append("  FROM MWIPMATDEF MAT " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT_ID, SUM(RESV_FIELD1) AS MON_PLAN " + "\n");
            strSqlString.Append("          FROM CWIPPLNMON " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND PLAN_MONTH = '" + month + "' " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("       ) MON_PLN  " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN TO_CHAR(TO_DATE('" + today + "', 'YYYYMMDD'), 'D') = 1 THEN D0_QTY" + "\n");
            strSqlString.Append("                        WHEN TO_CHAR(TO_DATE('" + today + "', 'YYYYMMDD'), 'D') = 2 THEN D0_QTY + D1_QTY" + "\n");
            strSqlString.Append("                        WHEN TO_CHAR(TO_DATE('" + today + "', 'YYYYMMDD'), 'D') = 3 THEN D0_QTY + D1_QTY + D2_QTY" + "\n");
            strSqlString.Append("                        WHEN TO_CHAR(TO_DATE('" + today + "', 'YYYYMMDD'), 'D') = 4 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY" + "\n");
            strSqlString.Append("                        WHEN TO_CHAR(TO_DATE('" + today + "', 'YYYYMMDD'), 'D') = 5 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY" + "\n");
            strSqlString.Append("                        WHEN TO_CHAR(TO_DATE('" + today + "', 'YYYYMMDD'), 'D') = 6 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY" + "\n");
            strSqlString.Append("                        ELSE 0" + "\n");
            strSqlString.Append("                   END) AS WEEK1_TTL " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN TO_CHAR(TO_DATE('" + today + "', 'YYYYMMDD'), 'D') = 7 THEN D0_QTY" + "\n");
            strSqlString.Append("                        WHEN TO_CHAR(TO_DATE('" + today + "', 'YYYYMMDD'), 'D') = 1 THEN D1_QTY" + "\n");
            strSqlString.Append("                        WHEN TO_CHAR(TO_DATE('" + today + "', 'YYYYMMDD'), 'D') = 2 THEN D2_QTY" + "\n");
            strSqlString.Append("                        WHEN TO_CHAR(TO_DATE('" + today + "', 'YYYYMMDD'), 'D') = 3 THEN D3_QTY" + "\n");
            strSqlString.Append("                        WHEN TO_CHAR(TO_DATE('" + today + "', 'YYYYMMDD'), 'D') = 4 THEN D4_QTY" + "\n");
            strSqlString.Append("                        WHEN TO_CHAR(TO_DATE('" + today + "', 'YYYYMMDD'), 'D') = 5 THEN D5_QTY" + "\n");
            strSqlString.Append("                        WHEN TO_CHAR(TO_DATE('" + today + "', 'YYYYMMDD'), 'D') = 6 THEN D6_QTY" + "\n");
            strSqlString.Append("                        ELSE 0" + "\n");
            strSqlString.Append("                   END) AS D0_PLAN " + "\n");            
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT MAT_ID " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D0_QTY, 0)) AS D0_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D1_QTY, 0)) AS D1_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D2_QTY, 0)) AS D2_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D3_QTY, 0)) AS D3_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D4_QTY, 0)) AS D4_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D5_QTY, 0)) AS D5_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', D6_QTY, 0)) AS D6_QTY " + "\n");            
            strSqlString.Append("                  FROM RWIPPLNWEK " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND GUBUN = '3' " + "\n");
            strSqlString.Append("                   AND PLAN_WEEK = '" + FindWeek.ThisWeek + "'" + "\n");            
            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("       ) WEEK_PLN " + "\n");
            strSqlString.Append("     , (       " + "\n");
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + yesterday + "', NVL(SHP_QTY_1, 0), 0)) AS SHP_YESTERDAY " + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + today + "', NVL(SHP_QTY_1, 0), 0)) AS SHP_TODAY " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + yesterday + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_WEEK_TTL " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE BETWEEN '" + start_day + "' AND '" + today + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_MONTH " + "\n");
            strSqlString.Append("          FROM VSUMWIPOUT" + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND FACTORY  = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("           AND CM_KEY_2 = 'PROD' " + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("           AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }
            
            strSqlString.Append("           AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND MAT_ID NOT LIKE 'HX%' " + "\n");
            strSqlString.Append("           AND WORK_DATE BETWEEN LEAST('" + FindWeek.StartDay_ThisWeek + "', '" + start_day + "', '" + yesterday + "') AND '" + today + "' " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("         UNION ALL " + "\n");
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + yesterday + "', NVL(SHP_QTY_1, 0), 0)) AS SHP_YESTERDAY " + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + today + "', NVL(SHP_QTY_1, 0), 0)) AS SHP_TODAY " + "\n");            
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + yesterday + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_WEEK_TTL " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE BETWEEN '" + start_day + "' AND '" + today + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_MONTH " + "\n");
            strSqlString.Append("          FROM VSUMWIPOUT_06" + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND FACTORY  = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("           AND CM_KEY_2 = 'PROD' " + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("           AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }
                        
            strSqlString.Append("           AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND MAT_ID LIKE 'HX%' " + "\n");
            strSqlString.Append("           AND WORK_DATE BETWEEN LEAST('" + FindWeek.StartDay_ThisWeek + "', '" + start_day + "', '" + yesterday + "') AND '" + today + "' " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID " + "\n");            
            strSqlString.Append("       ) MON_AO        " + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID" + "\n");

            for (int i = 0; i < dtOper.Rows.Count; i++)
            {
                strSqlString.Append("             , SUM(DECODE(OPER, '" + dtOper.Rows[i][0].ToString() + "', QTY_1, 0)) AS " + dtOper.Rows[i][0].ToString() + "\n");                
            }

            strSqlString.Append("             , SUM(QTY_1) AS WIP_TTL" + "\n");
            strSqlString.Append("             , SUM(DECODE(HOLD_FLAG, 'Y', QTY_1, 0)) AS HOLD_WIP" + "\n");

            //if (cboTimeBase.Text == "현재")
            if (cboTimeBase.SelectedIndex == 0)
            {
                strSqlString.Append("          FROM RWIPLOTSTS" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
            }
            else
            {
                strSqlString.Append("          FROM RWIPLOTSTS_BOH" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                //strSqlString.Append("           AND CUTOFF_DT = '" + today + cboTimeBase.Text.Replace("시", "") + "'" + "\n");
                strSqlString.Append("           AND CUTOFF_DT = '" + today + cboTimeBase.Text.Substring(0, 2) + "'" + "\n");
            }

            strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("           AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("           AND OPER BETWEEN 'A0950' AND 'AZ010'" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("           AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            if (cboHolddiv.Text == "Hold")
                strSqlString.AppendFormat("           AND HOLD_FLAG = 'Y' " + "\n");
            else if (cboHolddiv.Text == "Non Hold")
                strSqlString.AppendFormat("           AND HOLD_FLAG = ' ' " + "\n");

            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) WIP" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = MON_PLN.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = WEEK_PLN.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = MON_AO.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("   AND MAT.MAT_TYPE= 'FG' " + "\n");
            strSqlString.Append("   AND MAT.DELETE_FLAG <> 'Y' " + "\n");

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                strSqlString.AppendFormat("   AND MAT.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);

            strSqlString.AppendFormat(" GROUP BY {0}, SUBSTR(MAT.MAT_ID, LENGTH(MAT.MAT_ID) - 2)" + "\n", QueryCond1);
            strSqlString.Append(" HAVING (" + "\n");
            strSqlString.Append("         SUM(NVL(MON_AO.SHP_MONTH,0)) + SUM(NVL(MON_AO.SHP_WEEK_TTL,0)) + SUM(NVL(MON_AO.SHP_YESTERDAY,0)) + " + "\n");
            strSqlString.Append("         SUM(NVL(WIP.WIP_TTL,0)) + " + "\n");
            strSqlString.Append("         SUM(NVL(MON_PLN.MON_PLAN,0))  + SUM(NVL(WEEK_PLN.D0_PLAN,0)) + SUM(NVL(WEEK_PLN.WEEK1_TTL,0))" + "\n");
            strSqlString.Append("         ) <> 0" + "\n");
            strSqlString.AppendFormat(" ORDER BY DECODE(MAT.MAT_GRP_1, 'SE', 1, 'HX', 2, 3), {0}", QueryCond1 + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        // 지난 주차의 마지막일 가져오기
        private string MakeSqlString1(string year, string date)
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

        private string MakeSqlString2()   // 재공이 있는 공정을 순서대로 가져옴
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT DISTINCT OPER" + "\n");
            strSqlString.Append("  FROM RWIPLOTSTS " + "\n");
            strSqlString.Append(" WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("   AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("   AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("   AND OPER BETWEEN 'A0950' AND 'AZ010'" + "\n");
            strSqlString.Append(" ORDER BY OPER " + "\n");
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

                if (ckbSubTotal.Checked == true)
                {
                    //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                    int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(1);

                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 13, null, null, btnSort);
                    //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                    //Total부분 셀머지
                    spdData.RPT_FillDataSelectiveCells("Total", 0, 13, 0, 1, true, Align.Center, VerticalAlign.Center);
                }
                else
                {
                    spdData.DataSource = dt;

                    for (int i = 0; i <= 12; i++)
                    {
                        spdData.ActiveSheet.Columns[i].BackColor = Color.LemonChiffon;
                    }
                }

                spdData.RPT_AutoFit(false);

                spdData.RPT_SetPerSubTotalAndGrandTotal(0, 14, 13, 16);                

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
                string sNowDate = null;

                sNowDate = DateTime.Now.ToString();
                StringBuilder Condition = new StringBuilder();
                
                Condition.AppendFormat("기준일자: {0}        진도율: {1}        잔여일수: {2}        LOT TYPE : {3}        조회시간 : {4}", lblToday.Text, lblJindo.Text, lblRemain.Text, cdvLotType.Text, sNowDate);
                
                ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);
                //spdData.ExportExcel();            
            }
        }

        /// <summary>
        /// Factory 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            //this.SetFactory(cdvFactory.txtValue);
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }

        /// <summary>
        /// 7. 상단 Lebel 표시
        /// </summary>
        private void LabelTextChange()
        {               
            string strToday = cdvDate.Value.ToString("yyyyMMdd");

            string getStartDate = cdvDate.Value.ToString("yyyy-MM") + "-01";   
            string strEndday = Convert.ToDateTime(getStartDate).AddMonths(1).AddDays(-1).ToString("yyyyMMdd");

            double jindo = Convert.ToDouble(strToday.Substring(6, 2)) / Convert.ToDouble(strEndday.Substring(6, 2)) * 100;

            int remain = Convert.ToInt32(strEndday.Substring(6, 2)) - Convert.ToInt32(strToday.Substring(6, 2));

            // 해당월의 마지막날은 잔여일을 1로 계산한다.
            if (remain == 0)
            {
                remain = 1;
            }

            // 진도율 소수점 1째자리 까지 표시
            decimal jindoPer = Math.Round(Convert.ToDecimal(jindo),1);

            lblToday.Text = cdvDate.Value.ToString("yyyy-MM-dd");
            lblJindo.Text = jindoPer.ToString() + "%";
            lblRemain.Text = remain.ToString();

            dayArry[0] = cdvDate.Value.AddDays(-1).ToString("MM.dd");
            dayArry[1] = cdvDate.Value.ToString("MM.dd");

            dayArry1[0] = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");
            dayArry1[1] = cdvDate.Value.ToString("yyyyMMdd");            
        }
        #endregion
    }       
}
