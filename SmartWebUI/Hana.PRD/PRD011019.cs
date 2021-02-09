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
    public partial class PRD011019 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD011019<br/>
        /// 클래스요약: BIP 모니터링<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2015-09-07<br/>
        /// 상세  설명: BIP 모니터링(김권수D 요청)<br/>  
        /// 2015-09-15-임종우 : C200, B199 설비 제외시 해당코드로 'Down' 된 설비만 제외 (김보람 요청)
        /// 2019-01-28-임종우 : 창고 재고 과거 데이터 테이블 변경 ZHMMT111@SAPREAL -> CWMSLOTSTS_BOH
        /// </summary>
        GlobalVariable.FindWeek FindWeek_SOP_A = new GlobalVariable.FindWeek();

        public PRD011019()
        {
            InitializeComponent();
            
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();
            GridColumnInit2();
            
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(cdvFactory.txtValue);
            cdvFactory.Enabled = false;

            cdvGroup.sDynamicQuery = "SELECT DECODE(SEQ, 1, '설비대수', 2, 'CAPA현황', 3, '당일 실적', 4, 'D0 잔량', 5, 'D1 잔량', 6, 'D2 잔량', 7, '당주 잔량', 8, '차주 잔량', 9, '월간 잔량') AS Code, ' ' AS Data FROM DUAL, (SELECT LEVEL SEQ FROM DUAL CONNECT BY LEVEL<=9)";

            cdvGroup.Text = "ALL";
            cboTimeBase.SelectedIndex = 0;
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
                spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);                
                spdData.RPT_AddBasicColumn("PKG", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("LD_COUNT", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("PKG_CODE", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("PIN_TYPE", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);                

                spdData.RPT_AddBasicColumn(cdvDate.Value.ToString("MM-dd"), 0, 6, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("plan", 1, 6, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("actual", 1, 7, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("residual quantity", 1, 8, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_MerageHeaderColumnSpan(0, 6, 3);

                spdData.RPT_AddBasicColumn("Classification", 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
                                
                spdData.RPT_AddBasicColumn("HMK3", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("P/K", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("QC GATE", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PVI", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("WIRE ENCAP", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);                
                spdData.RPT_AddBasicColumn("WB", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PCO2 (room temperature)", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("DA", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("CORNER SAW", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("UNIT MOUNT", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("UV", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("SAW", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("RE MOUNT", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PCO1 (room temperature)", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("SHEET LAMI", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PCO (high temperature)", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("ROLL TRANSFER", 0, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("BG", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("L/N", 0, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("EDGE TRIM", 0, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("STOCK", 0, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                
                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 2);

                for (int i = 9; i <= 30; i++)
                {
                    spdData.RPT_MerageHeaderRowSpan(0, i, 2);
                }

                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        private void GridColumnInit2()
        {
            try
            {
                spdData2.RPT_ColumnInit();

                spdData2.RPT_AddBasicColumn("Operation", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData2.RPT_AddBasicColumn("Operation Name", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
                spdData2.RPT_AddBasicColumn("Item Code", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData2.RPT_AddBasicColumn("Item Description", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);
                spdData2.RPT_AddBasicColumn("Unit", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);

                spdData2.RPT_AddBasicColumn("Inventory status", 0, 5, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);
                spdData2.RPT_AddBasicColumn("Warehouse (S) Qty", 1, 5, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData2.RPT_AddBasicColumn("Warehouse (L) Qty", 1, 6, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData2.RPT_AddBasicColumn("On-site Qty", 1, 7, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData2.RPT_AddBasicColumn("Total", 1, 8, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData2.RPT_AddBasicColumn("Suspension (1004)", 1, 9, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData2.RPT_MerageHeaderColumnSpan(0, 5, 5);

                spdData2.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData2.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData2.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData2.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData2.RPT_MerageHeaderRowSpan(0, 4, 2);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT_GRP_1", "MAT.MAT_GRP_1", "MAT_GRP_1", "CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG", "MAT_GRP_3", "MAT.MAT_GRP_3", "MAT_GRP_3", "PKG", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD_COUNT", "MAT_GRP_", "MAT.MAT_GRP_6", "MAT_GRP_6", "LD_COUNT", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG_CODE", "MAT_CMF_11", "MAT.MAT_CMF_11", "MAT_CMF_11", "PKG_CODE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT_ID", "MAT.MAT_ID", "MAT_ID", "PRODUCT", true);
            
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT_GRP_2 AS FAMILY", "MAT.MAT_GRP_2", "MAT_GRP_2", "FAMILY", false);
            
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE_1", "MAT_GRP_4 AS TYPE_1", "MAT.MAT_GRP_4", "MAT_GRP_4", "TYPE_1", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE_2", "MAT_GRP_5 AS TYPE_2", "MAT.MAT_GRP_5", "MAT_GRP_5", "TYPE_2", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT_GRP_7 AS DENSITY", "MAT.MAT_GRP_7", "MAT_GRP_7", "DENSITY", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT_GRP_8 AS GENERATION", "MAT.MAT_GRP_8", "MAT_GRP_8", "GENERATION", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "MAT_CMF_10", "MAT.MAT_CMF_10", "MAT_CMF_10", "PIN_TYPE", false);
        }

        #region 시간관련 함수
        private void GetWorkDay()
        {
            DateTime Now = cdvDate.Value;
            FindWeek_SOP_A = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");
        }
        #endregion
        

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string QueryCond4 = null;

            string Yesterday;
            string Yesterdaybf;
            string Today;
            string sMonth;
            string sStartDate;

            string strKpcs = "1000";

            Today = cdvDate.Value.ToString("yyyyMMdd");
            Yesterday = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");
            Yesterdaybf = cdvDate.Value.AddDays(-2).ToString("yyyyMMdd");
            sMonth = cdvDate.Value.ToString("yyyyMM");

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            // 조회월과 조회주차의 시작일이 같은 달이면 시작은 조회월의 1일자로 하고, 다르면(주차시작일이 작으면) 주차 시작일을 시작일로 한다.
            if (sMonth == FindWeek_SOP_A.StartDay_ThisWeek.Substring(0, 6))
            {
                sStartDate = sMonth + "01";
            }
            else
            {
                sStartDate = FindWeek_SOP_A.StartDay_ThisWeek;
            }

            if (ckbKpcs.Checked == true)
            {
                strKpcs = "DECODE(GUBUN, '설비대수', 1, 1000)";
            }
            else
            {
                strKpcs = "1";
            }

       
            strSqlString.Append("SELECT " + QueryCond1 + "\n");
            strSqlString.Append("     , ROUND(SUM(PLAN_QTY)/" + strKpcs + ",0) AS PLAN_QTY " + "\n");
            strSqlString.Append("     , ROUND(SUM(SHP_QTY)/" + strKpcs + ",0) AS SHP_QTY " + "\n");
            strSqlString.Append("     , ROUND(SUM(DEF_QTY)/" + strKpcs + ",0) AS DEF_QTY " + "\n");
            strSqlString.Append("     , GUBUN " + "\n");
            strSqlString.Append("     , ROUND(SUM(HMKA3)/" + strKpcs + ",0) AS HMKA3 " + "\n");
            strSqlString.Append("     , ROUND(SUM(PK)/" + strKpcs + ",0) AS PK " + "\n");
            strSqlString.Append("     , ROUND(SUM(QC_GATE)/" + strKpcs + ",0) AS QC_GATE " + "\n");
            strSqlString.Append("     , ROUND(SUM(PVI)/" + strKpcs + ",0) AS PVI " + "\n");
            strSqlString.Append("     , ROUND(SUM(WIRE_ENCAP)/" + strKpcs + ",0) AS WIRE_ENCAP " + "\n");
            strSqlString.Append("     , ROUND(SUM(WB)/" + strKpcs + ",0) AS WB " + "\n");
            strSqlString.Append("     , ROUND(SUM(PCO2)/" + strKpcs + ",0) AS PCO2 " + "\n");
            strSqlString.Append("     , ROUND(SUM(DA)/" + strKpcs + ",0) AS DA " + "\n");
            strSqlString.Append("     , ROUND(SUM(CORNER_SAW)/" + strKpcs + ",0) AS CORNER_SAW " + "\n");
            strSqlString.Append("     , ROUND(SUM(UNIT_MOUNT)/" + strKpcs + ",0) AS UNIT_MOUNT " + "\n");
            strSqlString.Append("     , ROUND(SUM(UV)/" + strKpcs + ",0) AS UV " + "\n");
            strSqlString.Append("     , ROUND(SUM(SAW)/" + strKpcs + ",0) AS SAW " + "\n");
            strSqlString.Append("     , ROUND(SUM(RE_MOUNT)/" + strKpcs + ",0) AS RE_MOUNT " + "\n");
            strSqlString.Append("     , ROUND(SUM(PCO1)/" + strKpcs + ",0) AS PCO1 " + "\n");
            strSqlString.Append("     , ROUND(SUM(SHEET_LAMI)/" + strKpcs + ",0) AS SHEET_LAMI " + "\n");
            strSqlString.Append("     , ROUND(SUM(PCO)/" + strKpcs + ",0) AS PCO " + "\n");
            strSqlString.Append("     , ROUND(SUM(ROLL_TRANSFER)/" + strKpcs + ",0) AS ROLL_TRANSFER " + "\n");
            strSqlString.Append("     , ROUND(SUM(BG)/" + strKpcs + ",0) AS BG " + "\n");
            strSqlString.Append("     , ROUND(SUM(LAMI)/" + strKpcs + ",0) AS LAMI " + "\n");
            strSqlString.Append("     , ROUND(SUM(EDGE_TRIM)/" + strKpcs + ",0) AS EDGE_TRIM " + "\n");
            strSqlString.Append("     , ROUND(SUM(STOCK)/" + strKpcs + ",0) AS STOCK " + "\n");
            strSqlString.Append("  FROM (  " + "\n");
            strSqlString.Append("        SELECT " + QueryCond1 + ", GUBUN " + "\n");
            strSqlString.Append("             , MAX(PLAN_QTY) AS PLAN_QTY " + "\n");
            strSqlString.Append("             , MAX(SHP_QTY) AS SHP_QTY " + "\n");
            strSqlString.Append("             , MAX(DEF_QTY) AS DEF_QTY " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'AZ010', SUM_DEF, 0)) AS HMKA3" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A2300', SUM_DEF, 0)) AS PK" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A2100', SUM_DEF, 0)) AS QC_GATE" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A2050', SUM_DEF, 0)) AS PVI" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0930', SUM_DEF, 0)) AS WIRE_ENCAP" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0600', SUM_DEF, 0)) AS WB" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0530', SUM_DEF, 0)) AS PCO2" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0400', SUM_DEF, 0)) AS DA" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0260', SUM_DEF, 0)) AS CORNER_SAW" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0240', SUM_DEF, 0)) AS UNIT_MOUNT" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0250', SUM_DEF, 0)) AS UV" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0200', SUM_DEF, 0)) AS SAW" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0220', SUM_DEF, 0)) AS RE_MOUNT" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0165', SUM_DEF, 0)) AS PCO1" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0160', SUM_DEF, 0)) AS SHEET_LAMI" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0095', SUM_DEF, 0)) AS PCO" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0090', SUM_DEF, 0)) AS ROLL_TRANSFER" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0040', SUM_DEF, 0)) AS BG" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0020', SUM_DEF, 0)) AS LAMI" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0013', SUM_DEF, 0)) AS EDGE_TRIM" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0000', SUM_DEF, 0)) AS STOCK" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT " + QueryCond1 + ", OPER" + "\n");
            strSqlString.Append("                     , DECODE(SEQ, 1, 'WIP', 2, '설비대수', 3, 'CAPA현황', 4, '당일 실적', 5, 'D0 잔량', 6, 'D1 잔량', 7, 'D2 잔량', 8, '당주 잔량', 9, '차주 잔량', 10, '월간 잔량') AS GUBUN " + "\n");
            strSqlString.Append("                     , SUM(DECODE(SEQ, 1, 0, 2, 0, 3, 0, 4, 0, 5, D0_PLAN, 6, D1_PLAN, 7, D2_PLAN, 8, WEEK_PLAN, 9, WEEK2_PLAN, 10, MON_PLAN)) AS PLAN_QTY  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(SEQ, 1, 0, 2, 0, 3, 0, 4, 0, 5, D0_SHP, 6, D1_SHP, 7, D2_SHP, 8, WEEK_SHP, 9, WEEK2_SHP, 10, MON_SHP)) AS SHP_QTY" + "\n");
            strSqlString.Append("                     , SUM(DECODE(SEQ, 1, 0, 2, 0, 3, 0, 4, 0, 5, D0_DEF, 6, D1_DEF, 7, D2_DEF, 8, WEEK_DEF, 9, WEEK2_DEF, 10, MON_DEF)) AS DEF_QTY" + "\n");
            strSqlString.Append("                     , SUM(DECODE(SEQ, 1, WIP_QTY, 2, RES_CNT, 3, RES_CAPA, 4, END_QTY, 5, D0_SUM_QTY, 6, D1_SUM_QTY, 7, D2_SUM_QTY, 8, WEEK_SUM_QTY, 9, WEEK2_SUM_QTY, 10, MON_SUM_QTY)) AS SUM_DEF" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT " + QueryCond1 + ", OPER" + "\n");
            strSqlString.Append("                             , SUM(D0_PLAN) AS D0_PLAN, SUM(D0_SHP) AS D0_SHP, SUM(D0_DEF) AS D0_DEF" + "\n");
            strSqlString.Append("                             , SUM(D1_PLAN) AS D1_PLAN, SUM(D1_SHP) AS D1_SHP, SUM(D1_DEF) AS D1_DEF" + "\n");
            strSqlString.Append("                             , SUM(D2_PLAN) AS D2_PLAN, SUM(D2_SHP) AS D2_SHP, SUM(D2_DEF) AS D2_DEF" + "\n");
            strSqlString.Append("                             , SUM(WEEK_PLAN) AS WEEK_PLAN, SUM(WEEK_SHP) AS WEEK_SHP, SUM(WEEK_DEF) AS WEEK_DEF" + "\n");
            strSqlString.Append("                             , SUM(WEEK2_PLAN) AS WEEK2_PLAN, SUM(WEEK2_SHP) AS WEEK2_SHP, SUM(WEEK2_DEF) AS WEEK2_DEF" + "\n");
            strSqlString.Append("                             , SUM(MON_PLAN) AS MON_PLAN, SUM(MON_SHP) AS MON_SHP, SUM(MON_DEF) AS MON_DEF" + "\n");
            strSqlString.Append("                             , SUM(END_QTY) AS END_QTY, SUM(WIP_QTY) AS WIP_QTY, SUM(RES_CNT) AS RES_CNT, SUM(RES_CAPA) AS RES_CAPA" + "\n");
            strSqlString.Append("                             , SUM(D0_DEF-WIP_SUM_QTY+WIP_QTY) AS D0_SUM_QTY" + "\n");
            strSqlString.Append("                             , SUM(D1_DEF-WIP_SUM_QTY+WIP_QTY) AS D1_SUM_QTY " + "\n");
            strSqlString.Append("                             , SUM(D2_DEF-WIP_SUM_QTY+WIP_QTY) AS D2_SUM_QTY" + "\n");
            strSqlString.Append("                             , SUM(WEEK_DEF-WIP_SUM_QTY+WIP_QTY) AS WEEK_SUM_QTY" + "\n");
            strSqlString.Append("                             , SUM(WEEK2_DEF-WIP_SUM_QTY+WIP_QTY) AS WEEK2_SUM_QTY" + "\n");
            strSqlString.Append("                             , SUM(MON_DEF-WIP_SUM_QTY+WIP_QTY) AS MON_SUM_QTY" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_3, MAT.MAT_CMF_8, MAT.MAT_ID, MAT.OPER" + "\n");
            strSqlString.Append("                                     , NVL(PLN.D0_PLAN,0) AS D0_PLAN, NVL(PLN.D0_SHP,0) AS D0_SHP, NVL(PLN.D0_DEF,0) AS D0_DEF, NVL(PLN.D1_PLAN,0) AS D1_PLAN, NVL(PLN.D1_SHP,0) AS D1_SHP, NVL(PLN.D1_DEF,0) AS D1_DEF" + "\n");
            strSqlString.Append("                                     , NVL(PLN.D2_PLAN,0) AS D2_PLAN, NVL(PLN.D2_SHP,0) AS D2_SHP, NVL(PLN.D2_DEF,0) AS D2_DEF, NVL(PLN.WEEK_PLAN,0) AS WEEK_PLAN, NVL(PLN.WEEK_SHP,0) AS WEEK_SHP, NVL(PLN.WEEK_DEF,0) AS WEEK_DEF" + "\n");            
            strSqlString.Append("                                     , NVL(PLN.WEEK2_PLAN,0) AS WEEK2_PLAN, NVL(PLN.WEEK2_SHP,0) AS WEEK2_SHP, NVL(PLN.WEEK2_DEF,0) AS WEEK2_DEF, NVL(PLN.MON_PLAN,0) AS MON_PLAN, NVL(PLN.MON_SHP,0) AS MON_SHP, NVL(PLN.MON_DEF,0) AS MON_DEF" + "\n");
            strSqlString.Append("                                     , NVL(SHP.END_QTY,0) AS END_QTY, NVL(WIP.WIP_QTY,0) AS WIP_QTY, NVL(RAS.RES_CNT,0) AS RES_CNT, NVL(RAS.RES_CAPA,0) AS RES_CAPA" + "\n");
            strSqlString.Append("                                     , SUM(NVL(WIP.WIP_QTY,0)) OVER(PARTITION BY MAT.MAT_ID ORDER BY MAT.MAT_ID, DECODE(MAT.OPER, 'AZ010', 1, 'A2300', 2, 'A2100', 3, 'A2050', 4, 'A0930', 5, 'A0600', 6, 'A0530', 7" + "\n");
            strSqlString.Append("                                                                                                                                , 'A0400', 8, 'A0260', 9, 'A0240', 10, 'A0250', 11, 'A0200', 12, 'A0220', 13, 'A0165', 14 " + "\n");
            strSqlString.Append("                                                                                                                                , 'A0160', 15, 'A0095', 16, 'A0090', 17, 'A0040', 18, 'A0020', 19, 'A0013', 20, 'A0000', 21)) AS WIP_SUM_QTY" + "\n");
            strSqlString.Append("                                  FROM (" + "\n");
            strSqlString.Append("                                        SELECT DISTINCT MAT_GRP_1, MAT_GRP_3, MAT_CMF_8, MAT_ID, OPER" + "\n");            
            strSqlString.Append("                                          FROM VWIPMATDEF A" + "\n");
            strSqlString.Append("                                             , ( " + "\n");
            strSqlString.Append("                                                SELECT DECODE(LEVEL, 1, 'A0000', 2, 'A0013', 3, 'A0020', 4, 'A0040', 5, 'A0090', 6, 'A0095', 7, 'A0160'" + "\n");
            strSqlString.Append("                                                                   , 8, 'A0165', 9, 'A0220', 10, 'A0200', 11, 'A0250', 12, 'A0240', 13, 'A0260', 14, 'A0400' " + "\n");
            strSqlString.Append("                                                                   , 15, 'A0530', 16, 'A0600', 17, 'A0930', 18, 'A2050', 19, 'A2100', 20, 'A2300', 21, 'AZ010'" + "\n");            
            strSqlString.Append("                                                             ) OPER " + "\n");
            strSqlString.Append("                                                  FROM DUAL CONNECT BY LEVEL <= 21 " + "\n");
            strSqlString.Append("                                               ) " + "\n");
            strSqlString.Append("                                         WHERE 1=1 " + "\n");
            strSqlString.Append("                                           AND MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                                           AND DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                           AND MAT_GRP_10 = 'BIP'" + "\n");

            strSqlString.Append("                                           AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion

            strSqlString.Append("                                       ) MAT" + "\n");
            strSqlString.Append("                                     , (" + "\n");
            strSqlString.Append("                                        SELECT A.MAT_ID " + "\n");
            strSqlString.Append("                                             , SUM(NVL(D0_PLAN, 0) + (NVL(WEEK1_TTL, 0) - NVL(WEEK_SHP_TTL, 0))) AS D0_PLAN" + "\n");
            strSqlString.Append("                                             , SUM(NVL(D0_SHP, 0)) AS D0_SHP " + "\n");
            strSqlString.Append("                                             , SUM(NVL(D0_PLAN, 0) + (NVL(WEEK1_TTL, 0) - NVL(WEEK_SHP_TTL, 0)) - NVL(D0_SHP, 0)) AS D0_DEF " + "\n");
            strSqlString.Append("                                             , SUM(NVL(D1_PLAN, 0)) AS D1_PLAN " + "\n");
            strSqlString.Append("                                             , 0 AS D1_SHP" + "\n");
            strSqlString.Append("                                             , SUM(NVL(D1_PLAN, 0) + NVL(D0_PLAN, 0) + (NVL(WEEK1_TTL, 0) - NVL(WEEK_SHP_TTL, 0)) - NVL(D0_SHP, 0)) AS D1_DEF " + "\n");
            strSqlString.Append("                                             , SUM(NVL(D2_PLAN, 0)) AS D2_PLAN " + "\n");
            strSqlString.Append("                                             , 0 AS D2_SHP" + "\n");
            strSqlString.Append("                                             , SUM(NVL(D2_PLAN, 0) + NVL(D1_PLAN, 0) + NVL(D0_PLAN, 0) + (NVL(WEEK1_TTL, 0) - NVL(WEEK_SHP_TTL, 0)) - NVL(D0_SHP, 0)) AS D2_DEF" + "\n");
            strSqlString.Append("                                             , SUM(NVL(WEEK1_PLAN, 0)) AS WEEK_PLAN " + "\n");
            strSqlString.Append("                                             , SUM(NVL(WEEK_SHP, 0)) AS WEEK_SHP" + "\n");
            strSqlString.Append("                                             , SUM(NVL(WEEK1_PLAN, 0) - NVL(WEEK_SHP, 0)) AS WEEK_DEF " + "\n");
            strSqlString.Append("                                             , SUM(NVL(WEEK2_PLAN, 0)) AS WEEK2_PLAN " + "\n");
            strSqlString.Append("                                             , 0 AS WEEK2_SHP" + "\n");
            strSqlString.Append("                                             , SUM(NVL(WEEK2_PLAN, 0) + NVL(WEEK1_PLAN, 0) - NVL(WEEK_SHP, 0)) AS WEEK2_DEF " + "\n");
            strSqlString.Append("                                             , SUM(NVL(C.RESV_FIELD1,0)) MON_PLAN " + "\n");
            strSqlString.Append("                                             , SUM(NVL(MON_SHP, 0)) AS MON_SHP" + "\n");
            strSqlString.Append("                                             , SUM(NVL(C.RESV_FIELD1, 0)-NVL(MON_SHP, 0)) MON_DEF " + "\n");
            strSqlString.Append("                                          FROM (" + "\n");
            strSqlString.Append("                                                SELECT DISTINCT MAT_GRP_1, MAT_GRP_3, MAT_CMF_8, MAT_ID" + "\n");
            strSqlString.Append("                                                  FROM MWIPMATDEF" + "\n");
            strSqlString.Append("                                                 WHERE 1=1" + "\n");
            strSqlString.Append("                                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                                   AND MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                                                   AND DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                                   AND MAT_GRP_10 = 'BIP'" + "\n");
            strSqlString.Append("                                               ) A" + "\n");
            strSqlString.Append("                                             , ( " + "\n");
            strSqlString.Append("                                                SELECT MAT_ID " + "\n");
            strSqlString.Append("                                                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D0_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D0_QTY + D1_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D0_QTY + D1_QTY + D2_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY " + "\n");
            strSqlString.Append("                                                                ELSE 0 " + "\n");
            strSqlString.Append("                                                           END) AS WEEK1_TTL " + "\n");
            strSqlString.Append("                                                     , SUM(W1_QTY) AS WEEK1_PLAN " + "\n");
            strSqlString.Append("                                                     , SUM(W2_QTY) AS WEEK2_PLAN " + "\n");
            strSqlString.Append("                                                     , SUM(NVL(W1_QTY,0) + NVL(W2_QTY,0)) AS WEEK_TTL " + "\n");
            strSqlString.Append("                                                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D0_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D1_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D2_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D3_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D4_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D5_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D6_QTY " + "\n");
            strSqlString.Append("                                                                ELSE 0 " + "\n");
            strSqlString.Append("                                                           END) AS D0_PLAN " + "\n");
            strSqlString.Append("                                                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D1_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D2_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D3_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D4_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D5_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D6_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D7_QTY " + "\n");
            strSqlString.Append("                                                                ELSE 0 " + "\n");
            strSqlString.Append("                                                           END) AS D1_PLAN " + "\n");
            strSqlString.Append("                                                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D2_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D3_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D4_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D5_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D6_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D7_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D8_QTY " + "\n");
            strSqlString.Append("                                                                ELSE 0 " + "\n");
            strSqlString.Append("                                                           END) AS D2_PLAN " + "\n");
            strSqlString.Append("                                                  FROM ( " + "\n");
            strSqlString.Append("                                                        SELECT FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D0_QTY, 0)) AS D0_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D1_QTY, 0)) AS D1_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D2_QTY, 0)) AS D2_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D3_QTY, 0)) AS D3_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D4_QTY, 0)) AS D4_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D5_QTY, 0)) AS D5_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D6_QTY, 0)) AS D6_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D0_QTY, 0)) AS D7_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D1_QTY, 0)) AS D8_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D2_QTY, 0)) AS D9_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D3_QTY, 0)) AS D10_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D4_QTY, 0)) AS D11_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D5_QTY, 0)) AS D12_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D6_QTY, 0)) AS D13_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', WW_QTY, 0)) AS W1_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', WW_QTY, 0)) AS W2_QTY  " + "\n");
            strSqlString.Append("                                                          FROM RWIPPLNWEK  " + "\n");
            strSqlString.Append("                                                         WHERE 1=1  " + "\n");          
            strSqlString.Append("                                                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");         
            strSqlString.Append("                                                           AND GUBUN = '3'  " + "\n");
            strSqlString.Append("                                                           AND PLAN_WEEK IN ('" + FindWeek_SOP_A.ThisWeek + "','" + FindWeek_SOP_A.NextWeek + "') " + "\n");
            strSqlString.Append("                                                         GROUP BY FACTORY, MAT_ID  " + "\n");
            strSqlString.Append("                                                       )  " + "\n");
            strSqlString.Append("                                                 GROUP BY MAT_ID  " + "\n");
            strSqlString.Append("                                               ) B " + "\n");
            strSqlString.Append("                                             , (  " + "\n");
            strSqlString.Append("                                                SELECT MAT_ID " + "\n");
            strSqlString.Append("                                                     , SUM(TO_NUMBER(DECODE(RESV_FIELD1,' ',0,RESV_FIELD1))) AS RESV_FIELD1 " + "\n");
            strSqlString.Append("                                                  FROM CWIPPLNMON  " + "\n");
            strSqlString.Append("                                                 WHERE 1=1  " + "\n");
            strSqlString.Append("                                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                                                   AND PLAN_MONTH = '" + sMonth + "' " + "\n");            
            strSqlString.Append("                                                 GROUP BY MAT_ID " + "\n");            
            strSqlString.Append("                                               ) C " + "\n");  
            strSqlString.Append("                                             , (  " + "\n");
            strSqlString.Append("                                                SELECT MAT_ID  " + "\n");
            strSqlString.Append("                                                     , SUM(DECODE(WORK_DATE, '" + Today + "', S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1, 0)) AS D0_SHP" + "\n");
            strSqlString.Append("                                                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Today + "' THEN S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1 ELSE 0 END) AS WEEK_SHP" + "\n");
            strSqlString.Append("                                                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1 ELSE 0 END) AS WEEK_SHP_TTL  " + "\n");
            strSqlString.Append("                                                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + sMonth + "01' AND '" + Today + "' THEN S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1 ELSE 0 END) AS MON_SHP" + "\n");
            strSqlString.Append("                                                  FROM RSUMFACMOV" + "\n");
            strSqlString.Append("                                                 WHERE 1=1" + "\n");
            strSqlString.Append("                                                   AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                                   AND FACTORY = 'CUSTOMER' " + "\n");
            strSqlString.Append("                                                   AND CM_KEY_3 LIKE 'P%'" + "\n");
            strSqlString.Append("                                                   AND WORK_DATE BETWEEN '" + sStartDate + "' AND '" + Today + "'" + "\n");
            strSqlString.Append("                                                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                                               ) D " + "\n");
            strSqlString.Append("                                         WHERE 1=1    " + "\n");

            if (ckdPlan.Checked == true)
            {
                strSqlString.Append("                                           AND A.MAT_ID = B.MAT_ID " + "\n");
            }
            else
            {
                strSqlString.Append("                                           AND A.MAT_ID = B.MAT_ID(+) " + "\n");
            }

            strSqlString.Append("                                           AND A.MAT_ID = C.MAT_ID(+) " + "\n");
            strSqlString.Append("                                           AND A.MAT_ID = D.MAT_ID(+) " + "\n");
            strSqlString.Append("                                         GROUP BY A.MAT_ID  " + "\n");
            strSqlString.Append("                                       ) PLN" + "\n");
            strSqlString.Append("                                     , (" + "\n");
            strSqlString.Append("                                        SELECT MAT_ID" + "\n");
            strSqlString.Append("                                             , OPER " + "\n");
            strSqlString.Append("                                             , SUM(CASE WHEN OPER = 'AZ010' THEN SHIP_QTY_1 ELSE END_QTY_1 END) AS END_QTY" + "\n");
            strSqlString.Append("                                          FROM ( " + "\n");
            strSqlString.Append("                                                SELECT MAT_ID, OPER, CM_KEY_3" + "\n");
            strSqlString.Append("                                                     , (S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1) END_QTY_1" + "\n");
            strSqlString.Append("                                                     , 0 SHIP_QTY_1                             " + "\n");
            strSqlString.Append("                                                  FROM RSUMWIPMOV " + "\n");
            strSqlString.Append("                                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                                   AND OPER <> 'AZ010'" + "\n");
            strSqlString.Append("                                                   AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                                                   AND WORK_DATE = '" + Today + "'" + "\n");
            strSqlString.Append("                                                 UNION ALL " + "\n");
            strSqlString.Append("                                                SELECT MAT_ID" + "\n");
            strSqlString.Append("                                                     , 'AZ010' AS OPER" + "\n");            
            strSqlString.Append("                                                     , CM_KEY_3" + "\n");
            strSqlString.Append("                                                     , 0 END_QTY_1" + "\n");
            strSqlString.Append("                                                     , SHP_QTY_1 AS SHIP_QTY_1" + "\n");
            strSqlString.Append("                                                  FROM VSUMWIPOUT" + "\n");
            strSqlString.Append("                                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                                   AND WORK_DATE = '" + Today + "'" + "\n");
            strSqlString.Append("                                               ) " + "\n");

            if (cdvLotType.Text != "ALL")
            {                
                strSqlString.Append("                                         WHERE CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                                         GROUP BY MAT_ID, OPER" + "\n");
            strSqlString.Append("                                       ) SHP" + "\n");
            strSqlString.Append("                                     , (" + "\n");
            strSqlString.Append("                                        SELECT MAT_ID, OPER, SUM(QTY_1) AS WIP_QTY" + "\n");
            strSqlString.Append("                                          FROM (" + "\n");
            strSqlString.Append("                                                SELECT MAT_ID" + "\n");
            strSqlString.Append("                                                     , CASE WHEN OPER = 'A0300' THEN 'A0250'" + "\n");
            strSqlString.Append("                                                            WHEN OPER = 'A0550' THEN 'A0600'" + "\n");
            strSqlString.Append("                                                            WHEN OPER = 'A0800' THEN 'A0930'" + "\n");
            strSqlString.Append("                                                            ELSE OPER" + "\n");
            strSqlString.Append("                                                       END OPER" + "\n");
            strSqlString.Append("                                                     , QTY_1" + "\n");
            
            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                                                  FROM RWIPLOTSTS" + "\n");
                strSqlString.Append("                                                 WHERE 1=1  " + "\n");
            }
            else
            {
                strSqlString.Append("                                                  FROM RWIPLOTSTS_BOH" + "\n");
                strSqlString.Append("                                                 WHERE CUTOFF_DT = '" + cdvDate.Value.ToString("yyyyMMdd") + "22'" + "\n");
            }

            strSqlString.Append("                                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                                   AND LOT_TYPE = 'W'  " + "\n");
            strSqlString.Append("                                                   AND LOT_DEL_FLAG = ' ' " + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                                                   AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                                               )" + "\n");
            strSqlString.Append("                                         GROUP BY MAT_ID, OPER" + "\n");
            strSqlString.Append("                                       ) WIP" + "\n");
            strSqlString.Append("                                     , (" + "\n");
            strSqlString.Append("                                        SELECT A.MAT_ID " + "\n");
            strSqlString.Append("                                             , A.OPER" + "\n");
            strSqlString.Append("                                             , SUM(A.RES_CNT) AS RES_CNT " + "\n");
            strSqlString.Append("                                             , SUM(TRUNC(NVL(NVL(B.UPEH,0) * 24 * A.PERCENT * A.RES_CNT, 0))) AS RES_CAPA " + "\n");
            strSqlString.Append("                                          FROM ( " + "\n");
            strSqlString.Append("                                                SELECT FACTORY, RES_STS_2 AS MAT_ID " + "\n");
            strSqlString.Append("                                                     , CASE WHEN RES_STS_8 = 'A0300' THEN 'A0250'" + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 = 'A0550' THEN 'A0600'" + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 = 'A0800' THEN 'A0930'" + "\n");
            strSqlString.Append("                                                            ELSE RES_STS_8" + "\n");
            strSqlString.Append("                                                       END OPER" + "\n");
            strSqlString.Append("                                                     , RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, COUNT(RES_ID) AS RES_CNT" + "\n");
            strSqlString.Append("                                                     , DECODE(SUBSTR(RES_STS_8, 1, 3), 'A06', 0.75, 0.7 ) AS PERCENT" + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                                                  FROM MRASRESDEF " + "\n");
                strSqlString.Append("                                                 WHERE 1=1  " + "\n");
            }
            else
            {
                strSqlString.Append("                                                  FROM MRASRESDEF_BOH " + "\n");
                strSqlString.Append("                                                 WHERE 1=1  " + "\n");
                strSqlString.Append("                                                   AND CUTOFF_DT = '" + cdvDate.Value.ToString("yyyyMMdd") + "22' " + "\n");
            }            

            strSqlString.Append("                                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                                   AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("                                                   AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                                                   AND RES_TYPE='EQUIPMENT' " + "\n");
            //strSqlString.Append("                                                   AND RES_STS_1 NOT IN ('C200', 'B199') " + "\n");
            strSqlString.Append("                                                   AND (RES_STS_1 NOT IN ('C200', 'B199') OR RES_UP_DOWN_FLAG = 'U') " + "\n");
            strSqlString.Append("                                                 GROUP BY FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7  " + "\n");
            strSqlString.Append("                                               ) A " + "\n");
            strSqlString.Append("                                             , CRASUPHDEF B " + "\n");
            strSqlString.Append("                                         WHERE 1=1 " + "\n");
            strSqlString.Append("                                           AND A.FACTORY = B.FACTORY(+) " + "\n");
            strSqlString.Append("                                           AND A.OPER = B.OPER(+) " + "\n");
            strSqlString.Append("                                           AND A.RES_MODEL = B.RES_MODEL(+) " + "\n");
            strSqlString.Append("                                           AND A.UPEH_GRP = B.UPEH_GRP(+) " + "\n");
            strSqlString.Append("                                           AND A.MAT_ID = B.MAT_ID(+)    " + "\n");
            strSqlString.Append("                                           AND A.OPER NOT IN ('00001','00002', ' ')" + "\n");
            strSqlString.Append("                                         GROUP BY A.MAT_ID, A.OPER" + "\n");
            strSqlString.Append("                                       ) RAS" + "\n");
            strSqlString.Append("                                 WHERE 1=1" + "\n");

            if (ckdPlan.Checked == true)
            {
                strSqlString.Append("                                   AND MAT.MAT_ID = PLN.MAT_ID" + "\n");
            }
            else
            {
                strSqlString.Append("                                   AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
            }

            
            strSqlString.Append("                                   AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
            strSqlString.Append("                                   AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");
            strSqlString.Append("                                   AND MAT.MAT_ID = RAS.MAT_ID(+)" + "\n");
            strSqlString.Append("                                   AND MAT.OPER = SHP.OPER(+)" + "\n");
            strSqlString.Append("                                   AND MAT.OPER = WIP.OPER(+)" + "\n");
            strSqlString.Append("                                   AND MAT.OPER = RAS.OPER(+)" + "\n");
            strSqlString.Append("                               )" + "\n");
            strSqlString.Append("                         GROUP BY " + QueryCond1 + ", OPER " + "\n");
            strSqlString.Append("                         HAVING SUM(WEEK_PLAN)+SUM(WEEK2_PLAN)+SUM(MON_PLAN)+SUM(WEEK_SHP)+SUM(MON_SHP)+SUM(END_QTY)+SUM(WIP_QTY) > 0" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                     , (SELECT LEVEL AS SEQ FROM DUAL CONNECT BY LEVEL <= 10)" + "\n");
            strSqlString.Append("                 GROUP BY " + QueryCond1 + ", OPER, DECODE(SEQ, 1, 'WIP', 2, '설비대수', 3, 'CAPA현황', 4, '당일 실적', 5, 'D0 잔량', 6, 'D1 잔량', 7, 'D2 잔량', 8, '당주 잔량', 9, '차주 잔량', 10, '월간 잔량')" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY " + QueryCond1 + ", GUBUN" + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");

            if (cdvGroup.Text != "ALL")
            {
                strSqlString.Append("   AND (GUBUN = 'WIP' OR GUBUN " + cdvGroup.SelectedValueToQueryString + ") " + "\n");
            }

            strSqlString.Append(" GROUP BY " + QueryCond1 + ", GUBUN " + "\n");
            strSqlString.Append(" ORDER BY " + QueryCond1 + ", DECODE(GUBUN, 'WIP', 1, '설비대수', 2, 'CAPA현황', 3, '당일 실적', 4, 'D0 잔량', 5, 'D1 잔량', 6, 'D2 잔량', 7, '당주 잔량', 8, '차주 잔량', 9, '월간 잔량', 10) " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        /// <summary>
        /// 설정된 그룹별 공정 정보
        /// </summary>
        /// <returns></returns>
        private string MakeSqlStringStepInfo()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string QueryCond4 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            strSqlString.Append("SELECT " + QueryCond1 + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'AZ010', 'T', 'F')) AS HMKA3" + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'A2300', 'T', 'F')) AS PK" + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'A2100', 'T', 'F')) AS QC_GATE" + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'A2050', 'T', 'F')) AS PVI" + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'A0930', 'T', 'F')) AS WIRE_ENCAP" + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'A0600', 'T', 'F')) AS WB" + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'A0530', 'T', 'F')) AS PCO2" + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'A0400', 'T', 'F')) AS DA" + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'A0260', 'T', 'F')) AS CORNER_SAW" + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'A0240', 'T', 'F')) AS UNIT_MOUNT" + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'A0250', 'T', 'F')) AS UV" + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'A0200', 'T', 'F')) AS SAW" + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'A0220', 'T', 'F')) AS RE_MOUNT" + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'A0165', 'T', 'F')) AS PCO1" + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'A0160', 'T', 'F')) AS SHEET_LAMI" + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'A0095', 'T', 'F')) AS PCO" + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'A0090', 'T', 'F')) AS ROLL_TRANSFER" + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'A0040', 'T', 'F')) AS BG" + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'A0020', 'T', 'F')) AS LAMI" + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'A0013', 'T', 'F')) AS EDGE_TRIM" + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER, 'A0000', 'T', 'F')) AS STOCK" + "\n");
            strSqlString.Append("  FROM MWIPMATDEF A " + "\n");
            strSqlString.Append("     , MWIPFLWOPR@RPTTOMES B " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("   AND A.FIRST_FLOW = B.FLOW" + "\n");
            strSqlString.Append("   AND A.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("   AND A.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("   AND A.MAT_GRP_10 = 'BIP'" + "\n");
            strSqlString.Append("   AND A.MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

            #region 상세 조회에 따른 SQL문 생성
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
            #endregion

            strSqlString.Append(" GROUP BY " + QueryCond1 + "\n");
            strSqlString.Append(" ORDER BY " + QueryCond1 + "\n");

            return strSqlString.ToString();
        }

        #endregion

        #region EventHandler

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            DataTable dt2 = null;

            if (CheckField() == false) return;

            GridColumnInit();
            GridColumnInit2();
            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());
                dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                /*
                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                
                //spdData.DataSource = dt;
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 14, null, null, btnSort);

                spdData.RPT_FillDataSelectiveCells("Total", 0, 14, 0, 1, true, Align.Center, VerticalAlign.Center);
                //spdData.Sheets[0].FrozenColumnCount = 3;
                */
                spdData.DataSource = dt;
                spdData.RPT_AutoFit(false);

                spdData2.DataSource = dt2;
                spdData2.RPT_AutoFit(false);

                dt.Dispose();
                dt2.Dispose();

                SetOperCellStyle();
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

        private string MakeSqlString2()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT A.STEPID, A.OPER_DESC, A.MAT_ID, A.MAT_DESC, A.UNIT_1" + "\n");
            strSqlString.Append("     , B.INV_QTY, B.INV_L_QTY" + "\n");
            strSqlString.Append("     , C.WIP_QTY" + "\n");
            strSqlString.Append("     , NVL(B.INV_QTY,0) + NVL(B.INV_L_QTY,0) + NVL(C.WIP_QTY,0) AS TTL_QTY" + "\n");
            strSqlString.Append("     , B.QTY_1004" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT DISTINCT A.STEPID, C.OPER_DESC, B.MAT_ID, B.MAT_DESC, B.UNIT_1" + "\n");
            strSqlString.Append("          FROM CWIPBOMDEF A" + "\n");
            strSqlString.Append("             , MWIPMATDEF B" + "\n");
            strSqlString.Append("             , MWIPOPRDEF C" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND A.MATCODE = B.MAT_ID" + "\n");
            strSqlString.Append("           AND A.STEPID = C.OPER" + "\n");
            strSqlString.Append("           AND A.RESV_FLAG_1 = 'Y'" + "\n");
            strSqlString.Append("           AND A.STEPID IN ('A0013','A0090','A0160','A0400','A0600','A0930')" + "\n");
            strSqlString.Append("           AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'          " + "\n");
            strSqlString.Append("           AND C.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND B.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("           AND A.PARTNUMBER IN (SELECT MAT_ID FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_10 = 'BIP' AND DELETE_FLAG = ' ' AND MAT_ID LIKE '" + txtSearchProduct.Text + "')" + "\n");
            strSqlString.Append("       ) A" + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("             , SUM(INV_QTY) AS INV_QTY " + "\n");
            strSqlString.Append("             , SUM(INV_L_QTY) AS INV_L_QTY " + "\n");
            strSqlString.Append("             , SUM(QTY_1004) AS QTY_1004" + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT MAT_ID " + "\n");
            strSqlString.Append("                     , SUM(DECODE(STORAGE_LOCATION, '1000', QUANTITY, '1003', QUANTITY, 0)) AS INV_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(STORAGE_LOCATION, '1001', QUANTITY, 0)) AS INV_L_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(STORAGE_LOCATION, '1004', QUANTITY, 0)) AS QTY_1004" + "\n");
        
            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {                
                strSqlString.Append("                  FROM CWMSLOTSTS@RPTTOMES " + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");             
            }
            else
            {
                strSqlString.Append("                  FROM CWMSLOTSTS_BOH@RPTTOMES " + "\n");
                strSqlString.Append("                 WHERE CUTOFF_DT = '" + cdvDate.SelectedValue() + "22'" + "\n");                 
            }
            strSqlString.Append("                   AND QUANTITY> 0 " + "\n");
            strSqlString.Append("                   AND STORAGE_LOCATION IN ('1000', '1001', '1003', '1004') " + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                 UNION ALL " + "\n");
            strSqlString.Append("                SELECT MAT_ID, 0 AS INV_QTY, SUM(QTY_1) AS INV_L_QTY, 0 AS QTY_1004" + "\n");
            strSqlString.Append("                  FROM CWIPMATSLP@RPTTOMES " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND RECV_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND IN_TIME BETWEEN '" + cdvDate.Value.AddDays(-2).ToString("yyyyMMdd") + "000000' AND '" + cdvDate.SelectedValue() + "235959' " + "\n");            
            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("       ) B " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT_ID" + "\n");
            strSqlString.Append("             , SUM(QTY_1) AS WIP_QTY " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("          FROM RWIPLOTSTS " + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
            }
            else
            {
                strSqlString.Append("          FROM RWIPLOTSTS_BOH" + "\n");                
                strSqlString.Append("         WHERE 1=1  " + "\n");
                strSqlString.Append("           AND CUTOFF_DT = '" + cdvDate.SelectedValue() + "' || '22' " + "\n");
            }
                        
            strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND LOT_TYPE != 'W'" + "\n");
            strSqlString.Append("           AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("           AND LOT_CMF_2 = '-' " + "\n");
            strSqlString.Append("           AND LOT_CMF_9 != ' ' " + "\n");
            strSqlString.Append("           AND QTY_1 > 0 " + "\n");
            strSqlString.Append("           AND OPER NOT IN  ('00001', '00002', 'V0000') " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID  " + "\n");
            strSqlString.Append("       ) C " + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND A.MAT_ID = C.MAT_ID(+)" + "\n");
            strSqlString.Append(" ORDER BY STEPID, MAT_ID" + "\n");

            return strSqlString.ToString();
        }

        /// <summary>
        /// 그룹의 해당되지 않는 공정의 셀은 검은색으로 변환
        /// </summary>
        private void SetOperCellStyle()
        {
            string sGroupData1 = null;
            string sGroupData2 = null;
            string sDayofWeek = cdvDate.Value.DayOfWeek.ToString();

            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringStepInfo());

            Font ff = new Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(200)));
            Color colorStep = System.Drawing.Color.FromArgb(((System.Byte)(200)), ((System.Byte)(200)), ((System.Byte)(200)));
            Color colorWip = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(251)), ((System.Byte)(170)));

            
            for (int i = 0; i < spdData.ActiveSheet.RowCount; i++)
            {
                sGroupData1 = spdData.ActiveSheet.Cells[i, 0].Value.ToString() + "/" + spdData.ActiveSheet.Cells[i, 1].Value.ToString() + "/" + spdData.ActiveSheet.Cells[i, 2].Value.ToString() + "/" + spdData.ActiveSheet.Cells[i, 3].Value.ToString() + "/";

                //구분 컬럼이 Wip 일 때 셀을 노랗게 변환
                if ("WIP" == spdData.ActiveSheet.Cells[i, 9].Value.ToString())
                {
                    for (int colCnt = 6; colCnt <= 30; colCnt++)
                    {
                        spdData.ActiveSheet.Cells[i, colCnt].BackColor = colorWip;
                    }
                }

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    sGroupData2 = dt.Rows[j][0].ToString() + "/" + dt.Rows[j][1].ToString() + "/" + dt.Rows[j][2].ToString() + "/" + dt.Rows[j][3].ToString() + "/";

                    if (sGroupData1 == sGroupData2)
                    {
                        for (int y = 1; y <= 21; y++)
                        {
                            if (dt.Rows[j]["HMKA3"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 10].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 10].Value = 0;
                            }

                            if (dt.Rows[j]["PK"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 11].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 11].Value = 0;
                            }

                            if (dt.Rows[j]["QC_GATE"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 12].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 12].Value = 0;
                            }

                            if (dt.Rows[j]["PVI"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 13].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 13].Value = 0;
                            }

                            if (dt.Rows[j]["WIRE_ENCAP"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 14].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 14].Value = 0;
                            }

                            if (dt.Rows[j]["WB"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 15].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 15].Value = 0;
                            }

                            if (dt.Rows[j]["PCO2"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 16].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 16].Value = 0;
                            }

                            if (dt.Rows[j]["DA"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 17].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 17].Value = 0;
                            }

                            if (dt.Rows[j]["CORNER_SAW"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 18].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 18].Value = 0;
                            }

                            if (dt.Rows[j]["UNIT_MOUNT"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 19].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 19].Value = 0;
                            }

                            if (dt.Rows[j]["UV"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 20].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 20].Value = 0;
                            }

                            if (dt.Rows[j]["SAW"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 21].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 21].Value = 0;
                            }

                            if (dt.Rows[j]["RE_MOUNT"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 22].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 22].Value = 0;
                            }

                            if (dt.Rows[j]["PCO1"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 23].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 23].Value = 0;
                            }

                            if (dt.Rows[j]["SHEET_LAMI"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 24].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 24].Value = 0;
                            }

                            if (dt.Rows[j]["PCO"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 25].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 25].Value = 0;
                            }

                            if (dt.Rows[j]["ROLL_TRANSFER"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 26].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 26].Value = 0;
                            }

                            if (dt.Rows[j]["BG"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 27].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 27].Value = 0;
                            }

                            if (dt.Rows[j]["LAMI"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 28].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 28].Value = 0;
                            }

                            if (dt.Rows[j]["EDGE_TRIM"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 29].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 29].Value = 0;
                            }

                            if (dt.Rows[j]["STOCK"].ToString() == "F")
                            {
                                spdData.ActiveSheet.Cells[i, 30].BackColor = colorStep;
                                spdData.ActiveSheet.Cells[i, 30].Value = 0;
                            }                           
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
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();            
        }

        #endregion

        private void cdvGroup_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            if (cdvGroup.SelectCount == 0)
                cdvGroup.Text = "ALL";
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string[] dataArry = new string[6];
            string sOper = null;


            // WIP 클릭 시 팝업 창 띄움
            if (spdData.ActiveSheet.Cells[e.Row, 9].Value.ToString() == "WIP" && e.Column > 9 && spdData.ActiveSheet.Cells[e.Row, e.Column].Text != "")
            {
                // Group 정보 가져와서 담기... 상세 팝업에서 조건값으로 사용하기 위해
                for (int i = 0; i <= 5; i++)
                {
                    if (spdData.ActiveSheet.Columns[i].Label == "CUSTOMER")
                        dataArry[0] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "PKG")
                        dataArry[1] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "LD_COUNT")
                        dataArry[2] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "PKG_CODE")
                        dataArry[3] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "PRODUCT")
                        dataArry[4] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "PIN_TYPE")
                        dataArry[5] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();
                   
                }

                sOper = spdData.ActiveSheet.Columns[e.Column].Label;

                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetailWip(sOper, dataArry));

                if (dt != null && dt.Rows.Count > 0)
                {
                    System.Windows.Forms.Form frm = new PRD011019_P1("", dt);
                    frm.ShowDialog();
                }

            }

            else
            {
                return;
            }
        }

        //private string MakeSqlDetailRas(string sOper, string[] dataArry)
        //{
        //    string Today = null;

        //    StringBuilder strSqlString = new StringBuilder();

        //    Today = cdvDate.Value.ToString("yyyyMMdd");

        //    strSqlString.Append("SELECT A.RES_MODEL" + "\n");
        //    strSqlString.Append("     , SUM(A.RES_CNT) AS RES_CNT" + "\n");
        //    strSqlString.Append("     , MAX(B.UPEH) AS UPEH" + "\n");
        //    strSqlString.Append("     , SUM(TRUNC(NVL(NVL(B.UPEH,0) * 24 * A.PERCENT * A.RES_CNT, 0))) AS RES_CAPA" + "\n");
        //    strSqlString.Append("     , SUM(WAIT_CNT) AS WAIT_CNT" + "\n");
        //    strSqlString.Append("     , SUM(RUN_CNT) AS RUN_CNT" + "\n");
        //    strSqlString.Append("     , SUM(RES_DOWN_CNT) AS RES_DOWN_CNT" + "\n");
        //    strSqlString.Append("     , SUM(DEV_CHANG_CNT) AS DEV_CHANG_CNT" + "\n");
        //    strSqlString.Append("  FROM ( " + "\n");
        //    strSqlString.Append("        SELECT RAS.FACTORY, RES_STS_2, RES_STS_8 AS OPER, RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, COUNT(RES_ID) AS RES_CNT " + "\n");
        //    strSqlString.Append("             , DECODE(SUBSTR(RES_STS_8, 1, 3), 'A06', 0.75, 0.7 ) AS PERCENT " + "\n");
        //    strSqlString.Append("             , CASE WHEN RES_STS_8 IN ('A0040') THEN 'BG' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A0200', 'A0230') THEN 'SAW' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A0400', 'A0401', 'A0333') THEN 'DA1' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A0402') THEN 'DA2' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A0403') THEN 'DA3' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A0404') THEN 'DA4' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A0405') THEN 'DA5' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A0406') THEN 'DA6' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A0407') THEN 'DA7' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A0408') THEN 'DA8' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A0409') THEN 'DA9' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A0600','A0601') THEN 'WB1' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A0602') THEN 'WB2' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A0603') THEN 'WB3' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A0604') THEN 'WB4' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A0605') THEN 'WB5' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A0606') THEN 'WB6' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A0607') THEN 'WB7' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A0608') THEN 'WB8' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A0609') THEN 'WB9' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A0800', 'A0801', 'A0802', 'A0803', 'A0804', 'A0805', 'A0806', 'A0807', 'A0808', 'A0809') THEN 'GATE' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A1000') THEN 'MOLD' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A1100') THEN 'CURE' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A1150', 'A1500') THEN 'MK' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A1200') THEN 'TRIM' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A1450') THEN 'TIN' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A1300') THEN 'SBA' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A1750', 'A1800', 'A1900') THEN 'SIG' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A2000') THEN 'AVI' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A2050') THEN 'PVI' " + "\n");
        //    strSqlString.Append("                    WHEN RES_STS_8 IN ('A2100') THEN 'QC_GATE' " + "\n");
        //    strSqlString.Append("                    ELSE ' ' " + "\n");
        //    strSqlString.Append("               END OPER_GRP_1 " + "\n");
        //    strSqlString.Append("             , SUM(DECODE(RES_UP_DOWN_FLAG, 'U', DECODE(NVL(LOT.START_RES_ID, '-'), '-', 1, 0), 0)) AS WAIT_CNT " + "\n");
        //    strSqlString.Append("             , SUM(DECODE(RES_UP_DOWN_FLAG, 'U', DECODE(NVL(LOT.START_RES_ID, '-'), '-', 0, 1), 0)) AS RUN_CNT " + "\n");
        //    strSqlString.Append("             , SUM(DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(NVL(SUBSTR(RES_STS_1, 1, 1), '-'), 'D', 0, 1), 0)) AS RES_DOWN_CNT " + "\n");
        //    strSqlString.Append("             , SUM(DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(NVL(SUBSTR(RES_STS_1, 1, 1), '-'), 'D', 1, 0), 0)) AS DEV_CHANG_CNT " + "\n");

        //    if (DateTime.Now.ToString("yyyyMMdd") == Today)
        //    {
        //        strSqlString.Append("          FROM MRASRESDEF RAS" + "\n");
        //        strSqlString.Append("             , (" + "\n");
        //        strSqlString.Append("                SELECT DISTINCT FACTORY, START_RES_ID" + "\n");
        //        strSqlString.Append("                  FROM MWIPLOTSTS" + "\n");
        //        strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
        //        strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
        //        strSqlString.Append("                   AND LOT_DEL_FLAG = ' '" + "\n");
        //        strSqlString.Append("                   AND LOT_CMF_5 LIKE 'P%'" + "\n");
        //        strSqlString.Append("                   AND LOT_STATUS = 'PROC'" + "\n");
        //        strSqlString.Append("               ) LOT" + "\n");
        //        strSqlString.Append("         WHERE 1=1  " + "\n");
        //    }
        //    else
        //    {
        //        strSqlString.Append("          FROM MRASRESDEF_BOH RAS" + "\n");
        //        strSqlString.Append("             , (" + "\n");
        //        strSqlString.Append("                SELECT DISTINCT FACTORY, START_RES_ID" + "\n");
        //        strSqlString.Append("                  FROM MWIPLOTSTS_BOH" + "\n");
        //        strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
        //        strSqlString.Append("                   AND CUTOFF_DT = '" + Today + "22' " + "\n");
        //        strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
        //        strSqlString.Append("                   AND LOT_DEL_FLAG = ' '" + "\n");
        //        strSqlString.Append("                   AND LOT_CMF_5 LIKE 'P%'" + "\n");
        //        strSqlString.Append("                   AND LOT_STATUS = 'PROC'" + "\n");
        //        strSqlString.Append("               ) LOT" + "\n");
        //        strSqlString.Append("         WHERE 1=1  " + "\n");
        //        strSqlString.Append("           AND RAS.CUTOFF_DT = '" + Today + "22' " + "\n");
        //    }

        //    strSqlString.Append("           AND RAS.FACTORY = LOT.FACTORY(+) " + "\n");
        //    strSqlString.Append("           AND RAS.RES_ID = LOT.START_RES_ID(+) " + "\n");
        //    strSqlString.Append("           AND RAS.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
        //    strSqlString.Append("           AND RES_CMF_9 = 'Y' " + "\n");
        //    strSqlString.Append("           AND DELETE_FLAG = ' ' " + "\n");
        //    strSqlString.Append("           AND RES_TYPE='EQUIPMENT' " + "\n");
        //    strSqlString.Append("           AND RES_STS_1 NOT IN ('C200', 'B199') " + "\n");
        //    strSqlString.Append("         GROUP BY RAS.FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7  " + "\n");
        //    strSqlString.Append("       ) A " + "\n");
        //    strSqlString.Append("     , CRASUPHDEF B " + "\n");
        //    strSqlString.Append("     , ( " + "\n");
        //    strSqlString.Append("        SELECT DECODE(MAT_GRP_1,'SE','SEC','HX','HYNIX','IM','iML','FC','FCI','IG','IMAGIS' , (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1)) AS CUSTOMER" + "\n");
        //    strSqlString.Append("             , A.* " + "\n");
        //    strSqlString.Append("          FROM MWIPMATDEF A " + "\n");
        //    strSqlString.Append("       ) C " + "\n");
        //    strSqlString.Append(" WHERE 1=1 " + "\n");
        //    strSqlString.Append("   AND A.FACTORY = B.FACTORY(+) " + "\n");
        //    strSqlString.Append("   AND A.FACTORY = C.FACTORY(+) " + "\n");
        //    strSqlString.Append("   AND A.OPER = B.OPER(+) " + "\n");
        //    strSqlString.Append("   AND A.RES_MODEL = B.RES_MODEL(+) " + "\n");
        //    strSqlString.Append("   AND A.UPEH_GRP = B.UPEH_GRP(+) " + "\n");
        //    strSqlString.Append("   AND A.RES_STS_2 = B.MAT_ID(+) " + "\n");
        //    strSqlString.Append("   AND A.RES_STS_2 = C.MAT_ID(+) " + "\n");
        //    strSqlString.Append("   AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
        //    strSqlString.Append("   AND A.OPER NOT IN ('00001','00002') " + "\n");
        //    strSqlString.Append("   AND A.OPER_GRP_1 = '" + sOper + "'" + "\n");

        //    #region 상세 조회에 따른 SQL문 생성
        //    if (dataArry[0] != " " && dataArry[0] != null)
        //        strSqlString.AppendFormat("   AND C.CUSTOMER = '" + dataArry[0] + "'" + "\n");

        //    if (dataArry[1] != " " && dataArry[1] != null)
        //        strSqlString.AppendFormat("   AND C.MAT_GRP_9 = '" + dataArry[1] + "'" + "\n");

        //    if (dataArry[2] != " " && dataArry[2] != null)
        //        strSqlString.AppendFormat("   AND C.MAT_GRP_10 = '" + dataArry[2] + "'" + "\n");

        //    if (dataArry[3] != " " && dataArry[3] != null)
        //        strSqlString.AppendFormat("   AND C.MAT_GRP_6 = '" + dataArry[3] + "'" + "\n");

        //    if (dataArry[4] != " " && dataArry[4] != null)
        //        strSqlString.AppendFormat("   AND C.MAT_CMF_11 = '" + dataArry[4] + "'" + "\n");

        //    if (dataArry[5] != " " && dataArry[5] != null)
        //        strSqlString.AppendFormat("   AND C.MAT_GRP_2 = '" + dataArry[5] + "'" + "\n");

        //    if (dataArry[6] != " " && dataArry[6] != null)
        //        strSqlString.AppendFormat("   AND C.MAT_ID = '" + dataArry[6] + "'" + "\n");

        //    if (dataArry[7] != " " && dataArry[7] != null)
        //        strSqlString.AppendFormat("   AND C.MAT_GRP_4 = '" + dataArry[7] + "'" + "\n");

        //    if (dataArry[7] != " " && dataArry[8] != null)
        //        strSqlString.AppendFormat("   AND C.MAT_GRP_5 = '" + dataArry[8] + "'" + "\n");

        //    if (dataArry[7] != " " && dataArry[9] != null)
        //        strSqlString.AppendFormat("   AND C.MAT_GRP_7 = '" + dataArry[9] + "'" + "\n");

        //    if (dataArry[7] != " " && dataArry[10] != null)
        //        strSqlString.AppendFormat("   AND C.MAT_GRP_8 = '" + dataArry[10] + "'" + "\n");

        //    if (dataArry[7] != " " && dataArry[11] != null)
        //        strSqlString.AppendFormat("   AND C.MAT_CMF_10 = '" + dataArry[11] + "'" + "\n");

        //    #endregion

        //    strSqlString.Append(" GROUP BY A.RES_MODEL " + "\n");
        //    strSqlString.Append(" ORDER BY A.RES_MODEL " + "\n");

           
        //    if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
        //    {
        //        System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
        //    }

        //    return strSqlString.ToString();
        //}

        private string MakeSqlDetailWip(string sOper, string[] dataArry)
        {
            string Today = null;

            StringBuilder strSqlString = new StringBuilder();

            Today = cdvDate.Value.ToString("yyyyMMdd");

            strSqlString.Append("SELECT OPER, LOT_CMF_4, LOT_ID, QTY_1, STATUS, HOLD_CODE, HOLD_DESC, TIME_INTERVAL, LAST_COMMENT" + "\n");            
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT A.OPER" + "\n");
            strSqlString.Append("             , A.LOT_CMF_4" + "\n");
            strSqlString.Append("             , A.LOT_ID" + "\n");
            strSqlString.Append("             , A.QTY_1" + "\n");
            strSqlString.Append("             , DECODE(HOLD_FLAG, 'Y', 'HOLD', DECODE(LOT_STATUS, 'PROC', 'RUN', 'WAIT')) AS STATUS" + "\n");
            strSqlString.Append("             , A.HOLD_CODE" + "\n");
            strSqlString.Append("             , (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'HOLD_CODE' AND FACTORY = A.FACTORY AND KEY_1 = A.HOLD_CODE) AS HOLD_DESC" + "\n");
            strSqlString.Append("             , TRUNC(SYSDATE-TO_DATE(OPER_IN_TIME,'YYYYMMDDHH24MISS')) || '일 ' ||" + "\n");
            strSqlString.Append("               TRUNC(MOD((SYSDATE-TO_DATE(OPER_IN_TIME,'YYYYMMDDHH24MISS')),1)*24)|| '시간 ' ||" + "\n");
            strSqlString.Append("               TRUNC(MOD((SYSDATE-TO_DATE(OPER_IN_TIME,'YYYYMMDDHH24MISS'))*24,1)*60)|| '분 '" + "\n");
            strSqlString.Append("               AS TIME_INTERVAL" + "\n");
            strSqlString.Append("             , LAST_COMMENT" + "\n");
            strSqlString.Append("             , CASE WHEN OPER = 'AZ010' THEN 'HMKA3'" + "\n");
            strSqlString.Append("                    WHEN OPER = 'A2300' THEN 'PK'" + "\n");
            strSqlString.Append("                    WHEN OPER = 'A2100' THEN 'QC_GATE'" + "\n");
            strSqlString.Append("                    WHEN OPER = 'A2050' THEN 'PVI'" + "\n");
            strSqlString.Append("                    WHEN OPER IN ('A0930', 'A0800') THEN 'WIRE_ENCAP'" + "\n");
            strSqlString.Append("                    WHEN OPER IN ('A0600', 'A0550') THEN 'WB'" + "\n");
            strSqlString.Append("                    WHEN OPER = 'A0530' THEN 'PCO2'" + "\n");
            strSqlString.Append("                    WHEN OPER = 'A0400' THEN 'DA'" + "\n");
            strSqlString.Append("                    WHEN OPER = 'A0260' THEN 'CORNER_SAW'" + "\n");
            strSqlString.Append("                    WHEN OPER = 'A0240' THEN 'UNIT_MOUNT'" + "\n");
            strSqlString.Append("                    WHEN OPER IN ('A0250', 'A0300') THEN 'UV'" + "\n");
            strSqlString.Append("                    WHEN OPER = 'A0200' THEN 'SAW'" + "\n");
            strSqlString.Append("                    WHEN OPER = 'A0220' THEN 'RE_MOUNT'" + "\n");
            strSqlString.Append("                    WHEN OPER = 'A0165' THEN 'PCO1'" + "\n");
            strSqlString.Append("                    WHEN OPER = 'A0160' THEN 'SHEET_LAMI'" + "\n");
            strSqlString.Append("                    WHEN OPER = 'A0095' THEN 'PCO'" + "\n");
            strSqlString.Append("                    WHEN OPER = 'A0090' THEN 'ROLL_TRANSFER'" + "\n");
            strSqlString.Append("                    WHEN OPER = 'A0040' THEN 'BG'" + "\n");
            strSqlString.Append("                    WHEN OPER = 'A0020' THEN 'LAMI'" + "\n");
            strSqlString.Append("                    WHEN OPER = 'A0013' THEN 'EDGE_TRIM'" + "\n");
            strSqlString.Append("                    WHEN OPER = 'A0000' THEN 'STOCK'" + "\n");
            strSqlString.Append("                    WHEN OPER = 'E0100' THEN 'P-TEST'" + "\n");
            strSqlString.Append("                    WHEN OPER = 'E0000' THEN 'P-STOCK'" + "\n");
            strSqlString.Append("               END OPER_GRP" + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == Today)
            {
                strSqlString.Append("          FROM RWIPLOTSTS A" + "\n");
                strSqlString.Append("             , MWIPMATDEF B" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
            }
            else
            {
                strSqlString.Append("          FROM RWIPLOTSTS A" + "\n");
                strSqlString.Append("             , MWIPMATDEF B" + "\n");
                strSqlString.Append("         WHERE A.CUTOFF_DT = '" + Today + "22'" + "\n");
            }

            strSqlString.Append("           AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID  " + "\n");            
            strSqlString.Append("           AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("           AND A.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("           AND B.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (dataArry[0] != " " && dataArry[0] != null)
                strSqlString.AppendFormat("           AND B.MAT_GRP_1 = '" + dataArry[0] + "'" + "\n");

            if (dataArry[1] != " " && dataArry[1] != null)
                strSqlString.AppendFormat("           AND B.MAT_GRP_10 = '" + dataArry[1] + "'" + "\n");

            if (dataArry[2] != " " && dataArry[2] != null)
                strSqlString.AppendFormat("           AND B.MAT_GRP_6 = '" + dataArry[2] + "'" + "\n");

            if (dataArry[3] != " " && dataArry[3] != null)
                strSqlString.AppendFormat("           AND B.MAT_CMF_11 = '" + dataArry[3] + "'" + "\n");

            if (dataArry[4] != " " && dataArry[4] != null)
                strSqlString.AppendFormat("           AND B.MAT_ID = '" + dataArry[4] + "'" + "\n");

            if (dataArry[5] != " " && dataArry[5] != null)
                strSqlString.AppendFormat("           AND B.MAT_CMF_10 = '" + dataArry[5] + "'" + "\n");
            #endregion

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("           AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" WHERE OPER_GRP = '" + sOper + "'" + "\n");
            strSqlString.Append(" ORDER BY TIME_INTERVAL DESC, LOT_CMF_4" + "\n");

 
            

            //strSqlString.Append(" ORDER BY OPER, SCH_END_TIME, OPER_IN_TIME" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        
    }
}
