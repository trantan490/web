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
    public partial class PRD010215 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010215<br/>
        /// 클래스요약: 삼성 S-LSI SOP 관리(NEW)<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2012-12-18<br/>
        /// 상세  설명: 삼성 S-LSI SOP 관리 신규 개발<br/>
        /// 변경  내용: <br/>
        /// 2012-01-02-임종우 : 실적에서 Return 제외
        /// 2012-01-15-임종우 : RTF 데이터는 월~목 데이터만 표시, ASSY SOP 기준을 토~금으로 변경, TEST 공정별 잔량 추가 (김은석 요청)
        /// 2013-01-24-임종우 : TEST RTF 계획데이터는 생관에서 등록한 데이터 사용 (김은석 요청)
        /// 2013-03-08-임종우 : 음영표시 기준을 금일 잔량 -> 금주 잔량으로 변경 (김은석 요청)
        /// 2014-12-12-임종우 : RTF 미입고 잔량 추가 (임태성K 요청)
        /// 2015-12-04-임종우 : 투입계획 누락 표기 되는 내용 수정
        /// </summary>
        GlobalVariable.FindWeek FindWeek_SOP_A = new GlobalVariable.FindWeek();
        GlobalVariable.FindWeek FindWeek_SOP_T = new GlobalVariable.FindWeek();
        GlobalVariable.FindWeek FindWeek_RTF = new GlobalVariable.FindWeek();
        
        static string[] DateArray = new string[8];
        static string[] DateArray2 = new string[8];

        public PRD010215()
        {
            InitializeComponent();
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();

            udcWIPCondition1.Text = "SE";
            udcWIPCondition1.Enabled = false;
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
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Classification", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Product", 0, 10, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70); ;
                spdData.RPT_AddBasicColumn("Cust Device", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

                if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                {
                    spdData.RPT_AddBasicColumn("TESTER", 0, 12, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("TESTER", 0, 12, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                }

                spdData.RPT_AddBasicColumn("Next week (WW" + FindWeek_SOP_A.NextWeek.Substring(4) + ")", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("plan", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("금주(WW" + FindWeek_SOP_A.ThisWeek.Substring(4) + ")", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("plan", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("actual", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                
                spdData.RPT_AddBasicColumn("residual quantity", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                
                spdData.RPT_MerageHeaderColumnSpan(0, 14, 3);

                spdData.RPT_AddBasicColumn("금주(WW" + FindWeek_RTF.ThisWeek.Substring(4) + ") RTF", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("plan", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("actual", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 17, 3);

                spdData.RPT_AddBasicColumn("today", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("plan", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("actual", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 20, 3);

                spdData.RPT_AddBasicColumn("D1 plan", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                {
                    spdData.RPT_AddBasicColumn("Residual quantity by operation", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("This week's F / T residual quantity", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                }

                spdData.RPT_AddBasicColumn("WIP", 0, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("TOTAL", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                if (cdvFactory.txtValue != "")
                {
                    if (cdvFactory.txtValue.Equals(GlobalVariable.gsAssyDefaultFactory))
                    {
                        spdData.RPT_AddBasicColumn("HMK3A", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("V/I", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("S/B/A", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TIN", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("M/K", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("S/P", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SMT", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Back Side Coating", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMK2A", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Unreceived remaining volume", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 25, 21);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("HMK4T", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("P/K", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("T&R", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("V/I", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("BAKE", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("QA2", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TEST", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMK3T", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Unreceived remaining volume", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RTF AO residual quantity", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 25, 11);
                    }

                    spdData.RPT_AddBasicColumn("plan", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(DateArray[1].ToString(), 1, spdData.ActiveSheet.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(DateArray[2].ToString(), 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(DateArray[3].ToString(), 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(DateArray[4].ToString(), 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(DateArray[5].ToString(), 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(DateArray[6].ToString(), 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(DateArray[7].ToString(), 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.Columns.Count - 7, 7);
                }

                if (cdvFactory.txtValue.Equals(GlobalVariable.gsAssyDefaultFactory))
                {
                    spdData.RPT_AddBasicColumn("WAFER input plan", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("D0", 1, spdData.ActiveSheet.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("actual", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("residual quantity", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("D1", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.Columns.Count - 4, 4);
                }

                for (int i = 0; i <= 12; i++)
                {
                    spdData.RPT_MerageHeaderRowSpan(0, i, 2);
                }

                spdData.RPT_MerageHeaderRowSpan(0, 23, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 24, 2);

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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT.MAT_GRP_1", "MAT.MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Classification", "' '", "TYPE", "NVL((SELECT DISTINCT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_TURNKEY_PRODUCT' AND KEY_1 = MAT.MAT_ID), ' ') AS TYPE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID", "MAT.MAT_ID", "MAT.MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "MAT.MAT_CMF_7", "MAT.MAT_CMF_7", "MAT.MAT_CMF_7", false);            
        }
        #endregion


        #region 시간관련 함수
        private void GetWorkDay()
        {
            DateTime Now = cdvDate.Value;
            FindWeek_SOP_A = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");
            FindWeek_SOP_T = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "SE");
            FindWeek_RTF = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "QC");

            for (int i = 0; i < 8; i++)
            {
                DateArray[i] = Now.ToString("MM-dd");
                DateArray2[i] = Now.ToString("yyyyMMdd");
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
            string sGroup;
                        
            Today = cdvDate.Value.ToString("yyyyMMdd");
            Yesterday = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            // 그룹선택에 따른 쿼리 (ASSY 만 사용)
            if (cdvGroup.Text == "WAFER")
                sGroup = "NVL(WIP.DEF_WAFER,0)";
            else if (cdvGroup.Text == "DA")
                sGroup = "NVL(WIP.DEF_DA,0)";
            else if (cdvGroup.Text == "WB")
                sGroup = "NVL(WIP.DEF_WB,0)";
            else
                sGroup = "NVL(WIP.DEF_MOLD,0)";

            #region ASSY
            if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
            {
                strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond3);
                strSqlString.Append("     , ''" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN_W2,0)) AS PLAN_W2" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN_W1,0)) AS PLAN_W1" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.SHP_SOP_WEEK,0)) AS SHP_SOP_WEEK" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN_W1,0) - NVL(SHP.SHP_SOP_WEEK,0)) AS SOP_DEF" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN_RTF,0)) AS PLAN_RTF" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.SHP_RTF_WEEK,0)) AS SHP_RTF_WEEK" + "\n");
                strSqlString.Append("     , SUM(NVL(PLAN_RTF,0) - NVL(SHP.SHP_RTF_WEEK,0)) AS RTF_DEF" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN1,0)) AS PLAN1" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.SHP_TODAY,0)) AS SHP_TODAY" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN1,0) - NVL(SHP.SHP_TODAY,0)) AS D1_DEF" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN2,0)) AS PLAN2" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN_W1,0) - (NVL(SHP.SHP_SOP_WEEK,0) + " + sGroup + ")) AS OPER_DEF" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.TOTAL,0)) AS TOTAL" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.HMK3A,0)) AS HMK3A" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.VI,0)) AS VI" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.AVI,0)) AS AVI" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.SIG,0)) AS SIG" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.SBA,0)) AS SBA" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.TIN,0)) AS TIN" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.TRIM,0)) AS TRIM" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.MK,0)) AS MK" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.CURE,0)) AS CURE" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.MOLD,0)) AS MOLD" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.GATE,0)) AS GATE" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.WB,0)) AS WB" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.SP,0)) AS SP" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.SMT,0)) AS SMT" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.SAW,0)) AS SAW" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.BG,0)) AS BG" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.DA,0)) AS DA" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.BSC,0)) AS BSC" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.HMK2A,0)) AS HMK2A" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN_W1,0) - (NVL(SHP.SHP_SOP_WEEK,0) + NVL(WIP.TOTAL,0))) AS INPUT_DEF" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN2,0)) AS PLAN2" + "\n");                
                strSqlString.Append("     , SUM(NVL(PLN.PLAN3,0)) AS PLAN3" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN4,0)) AS PLAN4" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN5,0)) AS PLAN5" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN6,0)) AS PLAN6" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN7,0)) AS PLAN7" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN8,0)) AS PLAN8" + "\n");
                strSqlString.Append("     , SUM(NVL(OMS.D0,0)) AS IN_D0" + "\n");
                strSqlString.Append("     , SUM(NVL(ISS.ISSUE_QTY,0)) AS ISSUE_QTY" + "\n");
                strSqlString.Append("     , SUM(NVL(OMS.D0,0)) - SUM(NVL(ISS.ISSUE_QTY,0)) AS ISSUE_DEF" + "\n");
                strSqlString.Append("     , SUM(NVL(OMS.D1,0)) AS IN_D1" + "\n");
                strSqlString.Append("  FROM MWIPMATDEF MAT" + "\n");
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT FACTORY, MAT_ID" + "\n");
                strSqlString.Append("             , SUM(DECODE(PLAN_DATE, '" + DateArray2[0].ToString() + "', QTY_1, 0)) AS PLAN1" + "\n");
                strSqlString.Append("             , SUM(DECODE(PLAN_DATE, '" + DateArray2[1].ToString() + "', QTY_1, 0)) AS PLAN2 " + "\n");
                strSqlString.Append("             , SUM(DECODE(PLAN_DATE, '" + DateArray2[2].ToString() + "', QTY_1, 0)) AS PLAN3 " + "\n");
                strSqlString.Append("             , SUM(DECODE(PLAN_DATE, '" + DateArray2[3].ToString() + "', QTY_1, 0)) AS PLAN4 " + "\n");
                strSqlString.Append("             , SUM(DECODE(PLAN_DATE, '" + DateArray2[4].ToString() + "', QTY_1, 0)) AS PLAN5" + "\n");
                strSqlString.Append("             , SUM(DECODE(PLAN_DATE, '" + DateArray2[5].ToString() + "', QTY_1, 0)) AS PLAN6" + "\n");
                strSqlString.Append("             , SUM(DECODE(PLAN_DATE, '" + DateArray2[6].ToString() + "', QTY_1, 0)) AS PLAN7" + "\n");
                strSqlString.Append("             , SUM(DECODE(PLAN_DATE, '" + DateArray2[7].ToString() + "', QTY_1, 0)) AS PLAN8" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN PLAN_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + FindWeek_SOP_A.EndDay_ThisWeek + "' THEN QTY_1 ELSE 0 END) AS PLAN_W1" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN PLAN_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_NextWeek + "' AND '" + FindWeek_SOP_A.EndDay_NextWeek + "' THEN QTY_1 ELSE 0 END) AS PLAN_W2" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') IN ('6','7','1') THEN 0" + "\n");
                strSqlString.Append("                        WHEN PLAN_DATE BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + FindWeek_RTF.EndDay_ThisWeek + "' AND WW_RTF <> '-' THEN QTY_1 " + "\n");
                strSqlString.Append("                        ELSE 0 " + "\n");
                strSqlString.Append("                   END) AS PLAN_RTF" + "\n");
                strSqlString.Append("          FROM CPLNSOPDAY@RPTTOMES " + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");
                strSqlString.Append("           AND FACTORY  = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("           AND PLAN_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + FindWeek_SOP_A.EndDay_NextWeek + "' " + "\n");
                strSqlString.Append("         GROUP BY FACTORY, MAT_ID" + "\n");
                strSqlString.Append("       ) PLN " + "\n");
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT MAT_ID " + "\n");
                strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + Today + "', NVL(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1, 0), 0)) AS SHP_TODAY " + "\n");                
                strSqlString.Append("             , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Today + "' THEN NVL(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1, 0) ELSE 0 END) AS SHP_SOP_WEEK " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') IN ('6','7','1') THEN 0" + "\n");
                strSqlString.Append("                        WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + Today + "' THEN NVL(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1, 0) " + "\n");
                strSqlString.Append("                        ELSE 0 " + "\n");
                strSqlString.Append("                   END) AS SHP_RTF_WEEK" + "\n");
                strSqlString.Append("          FROM RSUMFACMOV" + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");
                strSqlString.Append("           AND CM_KEY_1  = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("           AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("           AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("           AND MAT_ID LIKE 'SES%' " + "\n");
                strSqlString.Append("           AND FACTORY <> 'RETURN' " + "\n");
                strSqlString.Append("           AND WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Today + "' " + "\n");
                strSqlString.Append("         GROUP BY MAT_ID " + "\n");
                strSqlString.Append("       ) SHP " + "\n");
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT MAT_ID" + "\n");
                strSqlString.Append("             , SUM(QTY) TOTAL" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK2A', QTY, 0)) AS HMK2A" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'Back Side Coating', QTY, 0)) AS BSC" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'D/A', QTY, 0)) AS DA" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'B/G', QTY, 0)) AS BG" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SAW', QTY, 0)) AS SAW" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SMT', QTY, 0)) AS SMT" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'S/P', QTY, 0)) AS SP" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'W/B', QTY, 0)) AS WB" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'GATE', QTY, 0)) AS GATE" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'MOLD', QTY, 0)) AS MOLD" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'CURE', QTY, 0)) AS CURE" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'M/K', QTY, 0)) AS MK" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'TRIM', QTY, 0)) AS TRIM" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'TIN', QTY, 0)) AS TIN" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'S/B/A', QTY, 0)) AS SBA" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SIG', QTY, 0)) AS SIG" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'AVI', QTY, 0)) AS AVI" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'V/I', QTY, 0)) AS VI" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK3A', QTY, 0)) AS HMK3A" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK2A', 0, QTY)) AS DEF_WAFER" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN OPER_GRP_1 NOT IN ('HMK2A','Back Side Coating','D/A','B/G','SAW','SMT','S/P') THEN QTY END) AS DEF_DA" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN OPER_GRP_1 NOT IN ('HMK2A','Back Side Coating','D/A','B/G','SAW','SMT','S/P','W/B') THEN QTY END) AS DEF_WB" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN OPER_GRP_1 NOT IN ('HMK2A','Back Side Coating','D/A','B/G','SAW','SMT','S/P','W/B','GATE','MOLD') THEN QTY END) AS DEF_MOLD" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT A.MAT_ID" + "\n");
                strSqlString.Append("                     , B.OPER_GRP_1 " + "\n");
                strSqlString.Append("                     , SUM(A.QTY_1)AS QTY " + "\n");

                // 금일조회 기준
                if (DateTime.Now.ToString("yyyyMMdd") == Today)
                {
                    strSqlString.Append("                  FROM RWIPLOTSTS A " + "\n");
                    strSqlString.Append("                     , MWIPOPRDEF B" + "\n");
                    strSqlString.Append("                 WHERE 1=1 " + "\n");
                }
                else// 금일이 아니면 스냅샷 떠놓은 테이블에서 가져옴.
                {
                    strSqlString.Append("                  FROM RWIPLOTSTS_BOH A " + "\n");
                    strSqlString.Append("                     , MWIPOPRDEF B" + "\n");
                    strSqlString.Append("                 WHERE CUTOFF_DT = '" + Today + "22' " + "\n");
                }

                strSqlString.Append("                   AND A.FACTORY  = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                   AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                   AND A.OPER = B.OPER" + "\n");
                strSqlString.Append("                   AND A.LOT_DEL_FLAG = ' '" + "\n");
                strSqlString.Append("                   AND A.LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                   AND LOT_CMF_5 LIKE 'P%' " + "\n");
                strSqlString.Append("                   AND A.MAT_ID LIKE 'SES%'" + "\n");
                strSqlString.Append("                 GROUP BY A.MAT_ID, B.OPER_GRP_1" + "\n");
                strSqlString.Append("               )" + "\n");
                strSqlString.Append("         GROUP BY MAT_ID" + "\n");
                strSqlString.Append("       ) WIP  " + "\n");
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT FACTORY, MAT_ID " + "\n");
                strSqlString.Append("             , SUM(DECODE(PLAN_DAY, '" + Today + "', PLAN_QTY, 0)) AS D0" + "\n");
                strSqlString.Append("             , SUM(DECODE(PLAN_DAY, '" + Today + "', 0, PLAN_QTY)) AS D1" + "\n");

                // 월계획 금일이면 기존대로
                if (DateTime.Now.ToString("yyyyMMdd") == Today)
                {
                    strSqlString.Append("          FROM CWIPPLNDAY " + "\n");
                    strSqlString.Append("         WHERE 1=1 " + "\n");
                }
                else// 금일이 아니면 스냅샷 떠놓은 테이블에서 가져옴.
                {
                    strSqlString.Append("          FROM CWIPPLNSNP@RPTTOMES " + "\n");
                    strSqlString.Append("         WHERE 1=1 " + "\n");
                    strSqlString.Append("           AND SNAPSHOT_DAY = '" + Today + "'" + "\n");

                }
                
                strSqlString.Append("           AND FACTORY  = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("           AND PLAN_DAY BETWEEN '" + Today + "' AND TO_DATE('" + Today + "','YYYYMMDD')+1 " + "\n");
                strSqlString.Append("           AND IN_OUT_FLAG = 'IN'" + "\n");
                strSqlString.Append("           AND CLASS = 'ASSY'" + "\n");
                strSqlString.Append("         GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("       ) OMS  " + "\n");
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT MAT_ID, SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1) AS ISSUE_QTY " + "\n");
                strSqlString.Append("          FROM RSUMWIPMOV" + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");
                strSqlString.Append("           AND FACTORY  = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("           AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("           AND MAT_ID LIKE 'SES%' " + "\n");
                strSqlString.Append("           AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND OPER = 'A0000' " + "\n");
                strSqlString.Append("           AND WORK_DATE = '" + Today + "' " + "\n");
                strSqlString.Append("         GROUP BY MAT_ID " + "\n");
                strSqlString.Append("       ) ISS  " + "\n");
                strSqlString.Append(" WHERE 1 = 1  " + "\n");
                strSqlString.Append("   AND MAT.MAT_ID = PLN.MAT_ID(+) " + "\n");
                strSqlString.Append("   AND MAT.MAT_ID = SHP.MAT_ID(+) " + "\n");
                strSqlString.Append("   AND MAT.MAT_ID = WIP.MAT_ID(+) " + "\n");
                strSqlString.Append("   AND MAT.MAT_ID = OMS.MAT_ID(+) " + "\n");
                strSqlString.Append("   AND MAT.MAT_ID = ISS.MAT_ID(+) " + "\n");
                strSqlString.Append("   AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("   AND MAT.MAT_ID LIKE 'SES%' " + "\n");
                strSqlString.Append("   AND MAT.MAT_ID NOT LIKE '%UW_' " + "\n");
                strSqlString.Append("   AND MAT.DELETE_FLAG = ' ' " + "\n");

                //상세 조회에 따른 SQL문 생성                                    
                strSqlString.Append("   AND MAT.MAT_GRP_1 = 'SE'" + "\n");

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

                strSqlString.Append("   AND MAT.MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");

                strSqlString.AppendFormat(" GROUP BY {0} " + "\n", QueryCond1);
                strSqlString.Append("HAVING SUM(NVL(PLN.PLAN_W1,0)+NVL(PLN.PLAN_W2,0)+NVL(SHP.SHP_SOP_WEEK,0)+NVL(WIP.TOTAL,0)+NVL(OMS.D0,0)+NVL(OMS.D1,0)+NVL(ISS.ISSUE_QTY,0)) > 0" + "\n");
                strSqlString.AppendFormat(" ORDER BY {0}" + "\n", QueryCond2);
            }
            #endregion
            #region TEST
            else
            {
                strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond3);
                strSqlString.Append("     , ''" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN_W2,0)) AS PLAN_W2" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN_W1,0) + NVL(SHP.SHP_SOP_WEEK_1,0)) AS PLAN_W1" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.SHP_SOP_WEEK_2,0)) AS SHP_SOP_WEEK_2" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN_W1,0) - NVL(SHP.SHP_TODAY,0)) AS SOP_DEF" + "\n");
                strSqlString.Append("     , SUM(NVL(RTF.PLAN_RTF,0)) AS PLAN_RTF" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.SHP_RTF_WEEK,0)) AS SHP_RTF_WEEK_2" + "\n");
                strSqlString.Append("     , SUM(NVL(RTF.PLAN_RTF,0) - NVL(SHP.SHP_RTF_WEEK,0)) AS RTF_DEF" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN1,0)) AS PLAN1" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.SHP_TODAY,0)) AS SHP_TODAY" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN1,0) - NVL(SHP.SHP_TODAY,0)) AS D1_DEF" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN2,0)) AS PLAN2" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN_W1,0) - NVL(SHP.SHP_TODAY,0) - NVL(WIP.TOTAL_2,0)) AS OPER_DEF" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.TOTAL,0)) AS TOTAL" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.HMK4T,0)) AS HMK4T" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.PK,0)) AS PK" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.TNR,0)) AS TNR" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.VI,0)) AS VI" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.BAKE,0)) AS BAKE" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.QA2,0)) AS QA2" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.TEST,0)) AS TEST" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.HMK3T,0)) AS HMK3T" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN_W1,0) - (NVL(SHP.SHP_TODAY,0) + NVL(WIP.TOTAL,0))) AS INPUT_DEF" + "\n");
                strSqlString.Append("     , SUM(NVL(RTF.PLAN_RTF,0) - (NVL(SHP.SHP_RTF_WEEK,0) + NVL(WIP.TOTAL,0))) AS RTF_INPUT_DEF" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN2,0)) AS PLAN2" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN3,0)) AS PLAN3" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN4,0)) AS PLAN4" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN5,0)) AS PLAN5" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN6,0)) AS PLAN6" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN7,0)) AS PLAN7" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.PLAN8,0)) AS PLAN8" + "\n");
                strSqlString.Append("  FROM MWIPMATDEF MAT" + "\n");
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT FACTORY, MAT_ID" + "\n");
                strSqlString.Append("             , SUM(DECODE(PLAN_DAY, '" + DateArray2[0].ToString() + "', PLAN_QTY, 0)) AS PLAN1" + "\n");
                strSqlString.Append("             , SUM(DECODE(PLAN_DAY, '" + DateArray2[1].ToString() + "', PLAN_QTY, 0)) AS PLAN2 " + "\n");
                strSqlString.Append("             , SUM(DECODE(PLAN_DAY, '" + DateArray2[2].ToString() + "', PLAN_QTY, 0)) AS PLAN3 " + "\n");
                strSqlString.Append("             , SUM(DECODE(PLAN_DAY, '" + DateArray2[3].ToString() + "', PLAN_QTY, 0)) AS PLAN4 " + "\n");
                strSqlString.Append("             , SUM(DECODE(PLAN_DAY, '" + DateArray2[4].ToString() + "', PLAN_QTY, 0)) AS PLAN5" + "\n");
                strSqlString.Append("             , SUM(DECODE(PLAN_DAY, '" + DateArray2[5].ToString() + "', PLAN_QTY, 0)) AS PLAN6" + "\n");
                strSqlString.Append("             , SUM(DECODE(PLAN_DAY, '" + DateArray2[6].ToString() + "', PLAN_QTY, 0)) AS PLAN7" + "\n");
                strSqlString.Append("             , SUM(DECODE(PLAN_DAY, '" + DateArray2[7].ToString() + "', PLAN_QTY, 0)) AS PLAN8" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN PLAN_DAY BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + FindWeek_SOP_T.EndDay_ThisWeek + "' THEN PLAN_QTY ELSE 0 END) AS PLAN_W1" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN PLAN_DAY BETWEEN '" + FindWeek_SOP_T.StartDay_NextWeek + "' AND '" + FindWeek_SOP_T.EndDay_NextWeek + "' THEN PLAN_QTY ELSE 0 END) AS PLAN_W2" + "\n");
                //strSqlString.Append("             , SUM(CASE WHEN PLAN_DAY BETWEEN '" + FindWeek_RTF.StartDay_ThisWeek + "' AND '" + FindWeek_RTF.EndDay_ThisWeek + "' THEN PLAN_QTY ELSE 0 END) AS PLAN_RTF" + "\n");

                // 월계획 금일이면 기존대로
                if (DateTime.Now.ToString("yyyyMMdd") == Today)
                {
                    strSqlString.Append("          FROM CWIPPLNDAY " + "\n");
                    strSqlString.Append("         WHERE 1=1 " + "\n");
                }
                else// 금일이 아니면 스냅샷 떠놓은 테이블에서 가져옴.
                {
                    strSqlString.Append("          FROM CWIPPLNSNP@RPTTOMES " + "\n");
                    strSqlString.Append("         WHERE 1=1 " + "\n");
                    strSqlString.Append("           AND SNAPSHOT_DAY = '" + Today + "'" + "\n");

                }
                strSqlString.Append("           AND FACTORY  = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("           AND PLAN_DAY BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + FindWeek_SOP_T.EndDay_NextWeek + "' " + "\n");
                strSqlString.Append("           AND IN_OUT_FLAG = 'IN' " + "\n");
                strSqlString.Append("           AND CLASS = 'SLIS' " + "\n");
                strSqlString.Append("         GROUP BY FACTORY, MAT_ID" + "\n");
                strSqlString.Append("       ) PLN " + "\n");
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT MAT_ID" + "\n");                
                strSqlString.Append("             , SUM(CASE WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') IN ('6','7','1') THEN 0" + "\n");
                strSqlString.Append("                        WHEN PLAN_DATE BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + FindWeek_RTF.EndDay_ThisWeek + "' AND WW_RTF <> '-' THEN QTY_1 " + "\n");
                strSqlString.Append("                        ELSE 0 " + "\n");
                strSqlString.Append("                   END) AS PLAN_RTF" + "\n");
                strSqlString.Append("          FROM CPLNSOPDAY@RPTTOMES " + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");
                strSqlString.Append("           AND FACTORY  = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("           AND PLAN_DATE BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + FindWeek_SOP_T.EndDay_ThisWeek + "' " + "\n");
                strSqlString.Append("         GROUP BY FACTORY, MAT_ID" + "\n");
                strSqlString.Append("       ) RTF " + "\n");
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT MAT_ID " + "\n");
                strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + Today + "', NVL(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1, 0), 0)) AS SHP_TODAY " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1, 0) ELSE 0 END) AS SHP_SOP_WEEK_1 " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + Today + "' THEN NVL(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1, 0) ELSE 0 END) AS SHP_SOP_WEEK_2 " + "\n");
                //strSqlString.Append("             , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_RTF.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1, 0) ELSE 0 END) AS SHP_RTF_WEEK_1 " + "\n");
                //strSqlString.Append("             , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_RTF.StartDay_ThisWeek + "' AND '" + Today + "' THEN NVL(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1, 0) ELSE 0 END) AS SHP_RTF_WEEK_2 " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') IN ('6','7','1') THEN 0" + "\n");
                strSqlString.Append("                        WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + Today + "' THEN NVL(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1, 0) " + "\n");
                strSqlString.Append("                        ELSE 0 " + "\n");
                strSqlString.Append("                   END) AS SHP_RTF_WEEK" + "\n");
                strSqlString.Append("          FROM RSUMFACMOV" + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");
                strSqlString.Append("           AND CM_KEY_1  = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("           AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("           AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("           AND MAT_ID LIKE 'SES%' " + "\n");
                strSqlString.Append("           AND FACTORY <> 'RETURN' " + "\n");
                strSqlString.Append("           AND WORK_DATE BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + Today + "' " + "\n");
                strSqlString.Append("         GROUP BY MAT_ID " + "\n");
                strSqlString.Append("       ) SHP " + "\n");
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT MAT_ID" + "\n");
                strSqlString.Append("             , SUM(QTY) TOTAL" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK4T', QTY, 0)) AS HMK4T" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'P/K', QTY, 0)) AS PK" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'TnR', QTY, 0)) AS TNR" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'V/I', QTY, 0)) AS VI" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BAKE', QTY, 0)) AS BAKE" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'QA2', QTY, 0)) AS QA2" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'TEST', QTY, 0)) AS TEST" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK3T', QTY, 0)) AS HMK3T" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN OPER_GRP_1 IN ('HMK3T', 'TEST') THEN 0 ELSE QTY END) AS TOTAL_2" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT A.MAT_ID" + "\n");
                strSqlString.Append("                     , B.OPER_GRP_1 " + "\n");
                strSqlString.Append("                     , SUM(A.QTY_1) AS QTY " + "\n");

                // 금일조회 기준
                if (DateTime.Now.ToString("yyyyMMdd") == Today)
                {
                    strSqlString.Append("                  FROM RWIPLOTSTS A " + "\n");
                    strSqlString.Append("                     , MWIPOPRDEF B" + "\n");
                    strSqlString.Append("                 WHERE 1=1 " + "\n");
                }
                else// 금일이 아니면 스냅샷 떠놓은 테이블에서 가져옴.
                {
                    strSqlString.Append("                  FROM RWIPLOTSTS_BOH A " + "\n");
                    strSqlString.Append("                     , MWIPOPRDEF B" + "\n");
                    strSqlString.Append("                 WHERE CUTOFF_DT = '" + Today + "22' " + "\n");
                }

                strSqlString.Append("                   AND A.FACTORY  = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                   AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                   AND A.OPER = B.OPER" + "\n");
                strSqlString.Append("                   AND A.LOT_DEL_FLAG = ' '" + "\n");
                strSqlString.Append("                   AND A.LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                   AND LOT_CMF_5 LIKE 'P%' " + "\n");
                strSqlString.Append("                   AND A.MAT_ID LIKE 'SES%'" + "\n");
                strSqlString.Append("                 GROUP BY A.MAT_ID, B.OPER_GRP_1" + "\n");
                strSqlString.Append("               )" + "\n");
                strSqlString.Append("         GROUP BY MAT_ID" + "\n");
                strSqlString.Append("       ) WIP  " + "\n");
                strSqlString.Append(" WHERE 1 = 1  " + "\n");
                strSqlString.Append("   AND MAT.MAT_ID = PLN.MAT_ID(+) " + "\n");
                strSqlString.Append("   AND MAT.MAT_ID = SHP.MAT_ID(+) " + "\n");
                strSqlString.Append("   AND MAT.MAT_ID = WIP.MAT_ID(+) " + "\n");
                strSqlString.Append("   AND MAT.MAT_ID = RTF.MAT_ID(+) " + "\n");
                strSqlString.Append("   AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("   AND MAT.MAT_ID LIKE 'SES%' " + "\n");
                strSqlString.Append("   AND MAT.MAT_ID NOT LIKE '%UW_' " + "\n");
                strSqlString.Append("   AND MAT.DELETE_FLAG = ' ' " + "\n");

                //상세 조회에 따른 SQL문 생성                                    
                strSqlString.Append("   AND MAT.MAT_GRP_1 = 'SE'" + "\n");

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

                strSqlString.Append("   AND MAT.MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");

                strSqlString.AppendFormat(" GROUP BY {0} " + "\n", QueryCond1);
                strSqlString.Append("HAVING SUM(NVL(PLN.PLAN_W1,0)+NVL(PLN.PLAN_W2,0)+NVL(SHP.SHP_SOP_WEEK_1,0)+NVL(SHP.SHP_SOP_WEEK_2,0)+NVL(WIP.TOTAL,0)) > 0" + "\n");
                strSqlString.AppendFormat(" ORDER BY {0}" + "\n", QueryCond2);
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
            DataTable dt1 = null;            

            StringBuilder strSqlString = new StringBuilder();
            
            // 계획값 기준 시간 가져오기(Plan Revision Time)
            strSqlString.Append("SELECT TO_DATE(RESV_FIELD5,'YYYY-MM-DD HH24MISS') FROM CWIPPLNDAY");
  
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
            lblPlanTime.Text = dt1.Rows[0][0].ToString();

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
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 13, null, null, btnSort);

                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 13, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);
                //spdData.DataSource = dt;
                
                
                // 당일 계획에 따른 재공 색깔 표시 (목표>= 재공 : red, 목표<재공 : blue)
                for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    int sum = 0;
                    int value = 0;
                    int iEndColumn = 0;
                    int iTarget = 0;

                    // Factory 별 WIP 마지막 공정 셋팅
                    if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                        iEndColumn = 44;
                    else 
                        iEndColumn = 33;

                    if (spdData.ActiveSheet.Cells[i, 16].BackColor.IsEmpty) // subtotal 부분 제외시키기 위함.
                    {
                        iTarget = Convert.ToInt32(spdData.ActiveSheet.Cells[i, 16].Value);

                        if (iTarget > 0) // 목표값 0인것 제외
                        {
                            for (int y = 26; y <= iEndColumn; y++) // 공정 컬럼번호 (순서는 반대로)
                            {
                                value = Convert.ToInt32(spdData.ActiveSheet.Cells[i, y].Value);
                                sum += value;

                                if (iTarget > sum)
                                {
                                    spdData.ActiveSheet.Cells[i, y].ForeColor = Color.Red;                                                                                
                                    spdData.ActiveSheet.Cells[i, y].BackColor = Color.Pink;
                                }
                                else
                                {
                                    spdData.ActiveSheet.Cells[i, y].ForeColor = Color.Blue;
                                    spdData.ActiveSheet.Cells[i, y].BackColor = Color.LightGreen;
                                    break;
                                }
                            }
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

                Condition.Append("Plan Revision Time : " + lblPlanTime.Text);

                if (cdvFactory.txtValue.Equals(GlobalVariable.gsAssyDefaultFactory))
                {
                    ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text + "(ASSY)", Condition.ToString(), null, true);
                    //(데이타, 제목(타이틀), 좌측문구, 우측문구, 자동사이즈조정)
                }
                else
                {
                    ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text + "(TEST)", Condition.ToString(), null, true);
                }
            }
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            //spdData.ExportExcel();            
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);

            if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                cdvGroup.Enabled = true;
            else cdvGroup.Enabled = false;
        }
        #endregion
    }
}
