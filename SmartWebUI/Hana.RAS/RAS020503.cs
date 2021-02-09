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


namespace Hana.Ras
{
    /// <summary>
    /// 클  래  스: RAS020503<br/>
    /// 클래스요약: MTTC 조회<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2011-09-30<br/>
    /// 상세  설명: MTTC 조회<br/>
    /// 변경  내용: 초기 개발 시 MTTC 전용으로 개발 하였지만 차 후 전체 정지 코드 확대를 대비해 설계 함.<br/>
    /// 2012-04-25-임종우 : [생산 - D1%, 품질 - D4%, 설비 - 그외] CNT, TIME, 대기 TIME 부분 추가 (곽창호 요청)
    /// 2013-01-11-임종우 : D1% ~ D9%로 데이터 상세 하게 분리 (김상천 요청)
    /// </summary>
    /// 

    public partial class RAS020503 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public RAS020503()
        {
            InitializeComponent();
            cdvFromTo.DaySelector.SelectedValue = "DAY";
            SortInit();  
            GridColumnInit(); //해더 한줄짜리 샘플
            cdvFromTo.AutoBinding();
        }

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
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;  //컬럼 완전 초기화
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("date", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Team in charge", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("파트", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("EQP TYPE", 0, 3, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Equipment", 0, 4, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("number of cases", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("time", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("MTTC", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("waiting time", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("Case Details ", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("D1", 1, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D2", 1, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D3", 1, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D4", 1, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D5", 1, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D6", 1, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D7", 1, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D8", 1, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D9", 1, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_MerageHeaderColumnSpan(0, 9, 9);

            spdData.RPT_AddBasicColumn("Time details ", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("D1", 1, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D2", 1, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D3", 1, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D4", 1, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D5", 1, 22, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D6", 1, 23, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D7", 1, 24, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D8", 1, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D9", 1, 26, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_MerageHeaderColumnSpan(0, 18, 9);

            spdData.RPT_AddBasicColumn("Wait time details ", 0, 27, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("D1", 1, 27, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D2", 1, 28, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D3", 1, 29, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D4", 1, 30, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D5", 1, 31, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D6", 1, 32, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D7", 1, 33, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D8", 1, 34, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D9", 1, 35, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_MerageHeaderColumnSpan(0, 27, 9);

            spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 8, 2);

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("date", "WORK_DATE", "", "", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Team in charge", "DEPART", "", "", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Part", "PART", "", "", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("EQP TYPE", "RES_GRP_3", "", "", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment", "RES_ID", "", "", true);            
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();
                        
            string strDate = string.Empty;

            int Between = cdvFromTo.SubtractBetweenFromToDate + 1;

            string[] selectDate1 = new string[cdvFromTo.SubtractBetweenFromToDate + 1];
            selectDate1 = cdvFromTo.getSelectDate();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            string strFromDate = cdvFromTo.ExactFromDate;
            string strToDate = cdvFromTo.ExactToDate;                     

            strSqlString.AppendFormat("SELECT " + QueryCond1 + " \n");
            strSqlString.AppendFormat("     , SUM(CNT) AS CNT" + "\n");
            strSqlString.AppendFormat("     , SUM(TTL_TIME) AS TTL_TIME" + "\n");
            strSqlString.AppendFormat("     , ROUND(SUM(DECODE(CNT, 0, 0, TTL_TIME / CNT)), 0) AS MTTC" + "\n");
            strSqlString.AppendFormat("     , SUM(WAIT_TIME) AS WAIT_TIME" + "\n");
            strSqlString.AppendFormat("     , SUM(D1_CNT) AS D1_CNT" + "\n");
            strSqlString.AppendFormat("     , SUM(D2_CNT) AS D2_CNT" + "\n");
            strSqlString.AppendFormat("     , SUM(D3_CNT) AS D3_CNT" + "\n");
            strSqlString.AppendFormat("     , SUM(D4_CNT) AS D4_CNT" + "\n");
            strSqlString.AppendFormat("     , SUM(D5_CNT) AS D5_CNT" + "\n");
            strSqlString.AppendFormat("     , SUM(D6_CNT) AS D6_CNT" + "\n");
            strSqlString.AppendFormat("     , SUM(D7_CNT) AS D7_CNT" + "\n");
            strSqlString.AppendFormat("     , SUM(D8_CNT) AS D8_CNT" + "\n");
            strSqlString.AppendFormat("     , SUM(D9_CNT) AS D9_CNT" + "\n");
            strSqlString.AppendFormat("     , SUM(D1_TIME) AS D1_TIME" + "\n");
            strSqlString.AppendFormat("     , SUM(D2_TIME) AS D2_TIME" + "\n");
            strSqlString.AppendFormat("     , SUM(D3_TIME) AS D3_TIME" + "\n");
            strSqlString.AppendFormat("     , SUM(D4_TIME) AS D4_TIME" + "\n");
            strSqlString.AppendFormat("     , SUM(D5_TIME) AS D5_TIME" + "\n");
            strSqlString.AppendFormat("     , SUM(D6_TIME) AS D6_TIME" + "\n");
            strSqlString.AppendFormat("     , SUM(D7_TIME) AS D7_TIME" + "\n");
            strSqlString.AppendFormat("     , SUM(D8_TIME) AS D8_TIME" + "\n");
            strSqlString.AppendFormat("     , SUM(D9_TIME) AS D9_TIME" + "\n");
            strSqlString.AppendFormat("     , SUM(D1_WAIT_TIME) AS D1_WAIT_TIME" + "\n");
            strSqlString.AppendFormat("     , SUM(D2_WAIT_TIME) AS D2_WAIT_TIME" + "\n");
            strSqlString.AppendFormat("     , SUM(D3_WAIT_TIME) AS D3_WAIT_TIME" + "\n");
            strSqlString.AppendFormat("     , SUM(D4_WAIT_TIME) AS D4_WAIT_TIME" + "\n");
            strSqlString.AppendFormat("     , SUM(D5_WAIT_TIME) AS D5_WAIT_TIME" + "\n");
            strSqlString.AppendFormat("     , SUM(D6_WAIT_TIME) AS D6_WAIT_TIME" + "\n");
            strSqlString.AppendFormat("     , SUM(D7_WAIT_TIME) AS D7_WAIT_TIME" + "\n");
            strSqlString.AppendFormat("     , SUM(D8_WAIT_TIME) AS D8_WAIT_TIME" + "\n");
            strSqlString.AppendFormat("     , SUM(D9_WAIT_TIME) AS D9_WAIT_TIME" + "\n");
            strSqlString.AppendFormat("  FROM (" + "\n");
            strSqlString.AppendFormat("        SELECT WORK_DATE" + "\n");
            strSqlString.AppendFormat("             , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = A.FACTORY AND TABLE_NAME='H_DEPARTMENT' AND KEY_1 = A.RES_GRP_1) AS DEPART" + "\n");
            strSqlString.AppendFormat("             , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = A.FACTORY AND TABLE_NAME='H_DEPARTMENT' AND KEY_1 = A.RES_GRP_2) AS PART" + "\n");
            strSqlString.AppendFormat("             , RES_GRP_3" + "\n");
            strSqlString.AppendFormat("             , RES_ID" + "\n");            
            strSqlString.AppendFormat("             , SUM(DECODE(MAINT_STEP, 'D_WAIT', FREQUENCY, 0)) AS CNT" + "\n");            
            strSqlString.AppendFormat("             , ROUND(SUM(TIME_SUM) / 60, 0) AS TTL_TIME" + "\n");            
            strSqlString.AppendFormat("             , ROUND(SUM(DECODE(MAINT_STEP, 'D_WAIT', TIME_SUM, 0)) / 60, 0) AS WAIT_TIME " + "\n");
            strSqlString.AppendFormat("             , SUM(CASE WHEN GROUP_CODE = 'D1' AND MAINT_STEP = 'D_WAIT' THEN FREQUENCY ELSE 0 END) AS D1_CNT " + "\n");
            strSqlString.AppendFormat("             , SUM(CASE WHEN GROUP_CODE = 'D2' AND MAINT_STEP = 'D_WAIT' THEN FREQUENCY ELSE 0 END) AS D2_CNT " + "\n");
            strSqlString.AppendFormat("             , SUM(CASE WHEN GROUP_CODE = 'D3' AND MAINT_STEP = 'D_WAIT' THEN FREQUENCY ELSE 0 END) AS D3_CNT " + "\n");
            strSqlString.AppendFormat("             , SUM(CASE WHEN GROUP_CODE = 'D4' AND MAINT_STEP = 'D_WAIT' THEN FREQUENCY ELSE 0 END) AS D4_CNT " + "\n");
            strSqlString.AppendFormat("             , SUM(CASE WHEN GROUP_CODE = 'D5' AND MAINT_STEP = 'D_WAIT' THEN FREQUENCY ELSE 0 END) AS D5_CNT " + "\n");
            strSqlString.AppendFormat("             , SUM(CASE WHEN GROUP_CODE = 'D6' AND MAINT_STEP = 'D_WAIT' THEN FREQUENCY ELSE 0 END) AS D6_CNT " + "\n");
            strSqlString.AppendFormat("             , SUM(CASE WHEN GROUP_CODE = 'D7' AND MAINT_STEP = 'D_WAIT' THEN FREQUENCY ELSE 0 END) AS D7_CNT " + "\n");
            strSqlString.AppendFormat("             , SUM(CASE WHEN GROUP_CODE = 'D8' AND MAINT_STEP = 'D_WAIT' THEN FREQUENCY ELSE 0 END) AS D8_CNT " + "\n");
            strSqlString.AppendFormat("             , SUM(CASE WHEN GROUP_CODE = 'D9' AND MAINT_STEP = 'D_WAIT' THEN FREQUENCY ELSE 0 END) AS D9_CNT " + "\n");
            strSqlString.AppendFormat("             , ROUND(SUM(CASE WHEN GROUP_CODE = 'D1' THEN TIME_SUM ELSE 0 END) / 60, 0) AS D1_TIME " + "\n");
            strSqlString.AppendFormat("             , ROUND(SUM(CASE WHEN GROUP_CODE = 'D2' THEN TIME_SUM ELSE 0 END) / 60, 0) AS D2_TIME " + "\n");
            strSqlString.AppendFormat("             , ROUND(SUM(CASE WHEN GROUP_CODE = 'D3' THEN TIME_SUM ELSE 0 END) / 60, 0) AS D3_TIME " + "\n");
            strSqlString.AppendFormat("             , ROUND(SUM(CASE WHEN GROUP_CODE = 'D4' THEN TIME_SUM ELSE 0 END) / 60, 0) AS D4_TIME " + "\n");
            strSqlString.AppendFormat("             , ROUND(SUM(CASE WHEN GROUP_CODE = 'D5' THEN TIME_SUM ELSE 0 END) / 60, 0) AS D5_TIME " + "\n");
            strSqlString.AppendFormat("             , ROUND(SUM(CASE WHEN GROUP_CODE = 'D6' THEN TIME_SUM ELSE 0 END) / 60, 0) AS D6_TIME " + "\n");
            strSqlString.AppendFormat("             , ROUND(SUM(CASE WHEN GROUP_CODE = 'D7' THEN TIME_SUM ELSE 0 END) / 60, 0) AS D7_TIME " + "\n");
            strSqlString.AppendFormat("             , ROUND(SUM(CASE WHEN GROUP_CODE = 'D8' THEN TIME_SUM ELSE 0 END) / 60, 0) AS D8_TIME " + "\n");
            strSqlString.AppendFormat("             , ROUND(SUM(CASE WHEN GROUP_CODE = 'D9' THEN TIME_SUM ELSE 0 END) / 60, 0) AS D9_TIME " + "\n");
            strSqlString.AppendFormat("             , ROUND(SUM(CASE WHEN GROUP_CODE = 'D1' AND MAINT_STEP = 'D_WAIT' THEN TIME_SUM ELSE 0 END) / 60, 0) AS D1_WAIT_TIME " + "\n");
            strSqlString.AppendFormat("             , ROUND(SUM(CASE WHEN GROUP_CODE = 'D2' AND MAINT_STEP = 'D_WAIT' THEN TIME_SUM ELSE 0 END) / 60, 0) AS D2_WAIT_TIME " + "\n");
            strSqlString.AppendFormat("             , ROUND(SUM(CASE WHEN GROUP_CODE = 'D3' AND MAINT_STEP = 'D_WAIT' THEN TIME_SUM ELSE 0 END) / 60, 0) AS D3_WAIT_TIME  " + "\n");
            strSqlString.AppendFormat("             , ROUND(SUM(CASE WHEN GROUP_CODE = 'D4' AND MAINT_STEP = 'D_WAIT' THEN TIME_SUM ELSE 0 END) / 60, 0) AS D4_WAIT_TIME  " + "\n");
            strSqlString.AppendFormat("             , ROUND(SUM(CASE WHEN GROUP_CODE = 'D5' AND MAINT_STEP = 'D_WAIT' THEN TIME_SUM ELSE 0 END) / 60, 0) AS D5_WAIT_TIME  " + "\n");
            strSqlString.AppendFormat("             , ROUND(SUM(CASE WHEN GROUP_CODE = 'D6' AND MAINT_STEP = 'D_WAIT' THEN TIME_SUM ELSE 0 END) / 60, 0) AS D6_WAIT_TIME  " + "\n");
            strSqlString.AppendFormat("             , ROUND(SUM(CASE WHEN GROUP_CODE = 'D7' AND MAINT_STEP = 'D_WAIT' THEN TIME_SUM ELSE 0 END) / 60, 0) AS D7_WAIT_TIME  " + "\n");
            strSqlString.AppendFormat("             , ROUND(SUM(CASE WHEN GROUP_CODE = 'D8' AND MAINT_STEP = 'D_WAIT' THEN TIME_SUM ELSE 0 END) / 60, 0) AS D8_WAIT_TIME  " + "\n");
            strSqlString.AppendFormat("             , ROUND(SUM(CASE WHEN GROUP_CODE = 'D9' AND MAINT_STEP = 'D_WAIT' THEN TIME_SUM ELSE 0 END) / 60, 0) AS D9_WAIT_TIME  " + "\n");
            strSqlString.AppendFormat("          FROM (" + "\n");
            strSqlString.AppendFormat("                SELECT MNT.*, RES.RES_GRP_1, RES.RES_GRP_2, RES.RES_GRP_3" + "\n");
            strSqlString.AppendFormat("                     , SUBSTR(MNT.MAINT_CODE, 1, 2) AS GROUP_CODE" + "\n");            
            strSqlString.AppendFormat("                  FROM CSUMRESMNT@RPTTOMES MNT" + "\n");
            strSqlString.AppendFormat("                     , MRASRESDEF RES" + "\n");
            strSqlString.AppendFormat("                 WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                   AND MNT.FACTORY = RES.FACTORY" + "\n");
            strSqlString.AppendFormat("                   AND MNT.RES_ID = RES.RES_ID" + "\n");
            strSqlString.AppendFormat("                   AND RES.RES_CMF_20 < '" + strFromDate + "'" + "\n");
            strSqlString.AppendFormat("                   AND WORK_DATE BETWEEN '" + cdvFromTo.HmFromDay + "' AND '" + cdvFromTo.HmToDay + "'" + "\n");
            strSqlString.AppendFormat("                   AND MNT.FACTORY = '" + cdvFactory.Text + "'" + " \n");
            strSqlString.AppendFormat("                   AND MAINT_CODE LIKE 'D%'" + "\n");

            if (cdvDownCodeDetail.Text != "ALL" && cdvDownCodeDetail.Text != "")
                strSqlString.AppendFormat("                   AND MAINT_CODE " + cdvDownCodeDetail.SelectedValueToQueryString + "\n");

            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("                   AND RES.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("                   AND RES.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("                   AND RES.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("                   AND RES.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("                   AND RES.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("                   AND RES.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            strSqlString.AppendFormat("               ) A" + "\n");
            strSqlString.AppendFormat("         GROUP BY WORK_DATE, FACTORY, RES_GRP_1, RES_GRP_2, RES_GRP_3, RES_ID" + "\n");
            strSqlString.AppendFormat("       )" + "\n");
            strSqlString.AppendFormat(" GROUP BY " + QueryCond1 + "\n");
            strSqlString.AppendFormat(" ORDER BY " + QueryCond1 + "\n");
            

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
            
            return strSqlString.ToString();
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
                this.Refresh();
                LoadingPopUp.LoadIngPopUpShow(this);

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 4, null, null);

                //4. Column Auto Fit4
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

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ");
            spdData.ExportExcel();
        }

        #region 기타
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            cdvDownCodeDetail.sFactory = cdvFactory.txtValue;
        }
        #endregion
        
        private void cdvDownCodeDetail_ValueButtonPress(object sender, EventArgs e)
        {
            cdvDownCodeDetail.sDynamicQuery = "SELECT DISTINCT KEY_2,DATA_1 FROM CWIPSITDEF WHERE FACTORY = '" + cdvFactory.Text + "' AND TABLE_NAME = 'MAINT_CODE' AND LENGTH(KEY_2) = 4 AND KEY_2 LIKE 'D%' ORDER BY KEY_2";
        }

    }
}