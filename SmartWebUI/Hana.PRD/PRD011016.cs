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
    public partial class PRD011016 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {        
        private DataTable dtWeek1 = new DataTable();
        private DataTable dtWeek2 = new DataTable();
        /// <summary>
        /// 클  래  스: PRD011016<br/>
        /// 클래스요약: Capa simulation (TEST)<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2013-11-13<br/>
        /// 상세  설명: Capa simulation (TEST)<br/>
        /// 변경  내용: <br/> 
        /// 2013-11-15-임종우 : 그룹 정보 추가 (박민정 요청)
        /// 2013-11-27-임종우 : 운휴설비 제외, H_MACHINE_INFO -> H_CAPA_MACHINE_INFO 변경 (박민정 요청)
        /// 2013-12-05-임종우 : 가동댓수/운휴댓수 분리, TYPE 추가 (박민정 요청)
        /// 2013-12-06-임종우 : HANDLER 설비 기준 변경 - HANDLER_ID 의 6자리 -> MODEL 의 6자리로 (박민정 요청)
        /// 2013-12-06-임종우 : 공정 / 그룹 / type / 설비 순서로 표시 (임태성 요청)
        /// 2013-12-09-임종우 : HMKE1 의 제품도 표시 되도록 수정
        /// 2014-03-10-임종우 : Step 추가, Product 클릭 시 상세 팝업 창 추가 (박민정 요청)
        /// 2014-03-11-임종우 : Product 클릭 시 상세 팝업은 그룹정보가 Tester 일때만 표시 되도록 수정 (박민정 요청)
        /// 2014-04-02-임종우 : 성능효율부분 사용자 입력 데이터로 계산 되도록 추가 (박민정 요청)
        /// 2014-04-08-임종우 : Product 조회 시 해당 제품만 나오도록 수정 (박민정 요청)
        /// 2014-04-08-임종우 : 팝업창에 성능효율 표시 (박민정 요청)
        /// </summary>
        public PRD011016()
        {
            InitializeComponent();
            cdvFromToDate.DaySelector.SelectedValue = "WEEK";
            cdvFromToDate.AutoBindingUserSetting(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));            
            cdvFromToDate.ToYearMonth.Visible = false;
            cdvFromToDate.FromDate.Visible = false;
            cdvFromToDate.ToDate.Visible = false;

            SortInit();
            GridColumnInit();
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(GlobalVariable.gsTestDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsTestDefaultFactory;
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

            if (txtPer.Text.Equals("") || CmnFunction.CheckNumeric(txtPer.Text) == false)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD067", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {            
            spdData.RPT_ColumnInit();
            
            try
            {
                spdData.RPT_AddBasicColumn("Operation", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("group", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("TYPE", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Equipment", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("STEP", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("FAMILY", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PACKAGE", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LD COUNT", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PKG CODE", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("SAP CODE", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("the number of operations", 0, 11, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("Number of suspension of the service ", 0, 12, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 60);

                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    if (ckbDetail.Checked == true)
                    {
                        spdData.RPT_AddBasicColumn("the required number of times", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.ToString("MM") + "월", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.AddMonths(1).ToString("MM") + "월", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.AddMonths(2).ToString("MM") + "월", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 13, 3);

                        spdData.RPT_AddBasicColumn("an excessive shortage of resources", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.ToString("MM") + "월", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.AddMonths(1).ToString("MM") + "월", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.AddMonths(2).ToString("MM") + "월", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 16, 3);

                        spdData.RPT_AddBasicColumn("Monthly plan", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.ToString("MM") + "월", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.AddMonths(1).ToString("MM") + "월", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.AddMonths(2).ToString("MM") + "월", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 19, 3);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("the required number of times", 0, 13, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.ToString("MM") + "월", 1, 13, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.AddMonths(1).ToString("MM") + "월", 1, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.AddMonths(2).ToString("MM") + "월", 1, 15, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 13, 3);

                        spdData.RPT_AddBasicColumn("an excessive shortage of resources", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.ToString("MM") + "월", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.AddMonths(1).ToString("MM") + "월", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.AddMonths(2).ToString("MM") + "월", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 16, 3);

                        spdData.RPT_AddBasicColumn("Monthly plan", 0, 19, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.ToString("MM") + "월", 1, 19, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.AddMonths(1).ToString("MM") + "월", 1, 20, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn(cdvFromToDate.FromYearMonth.Value.AddMonths(2).ToString("MM") + "월", 1, 21, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 19, 3);
                    }
                }
                else
                {
                    if (ckbDetail.Checked == true)
                    {
                        spdData.RPT_AddBasicColumn("the required number of times", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                        spdData.RPT_AddDynamicColumn(cdvFromToDate, 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 13, cdvFromToDate.SubtractBetweenFromToDate + 1);

                        spdData.RPT_AddBasicColumn("an excessive shortage of resources", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                        spdData.RPT_AddDynamicColumn(cdvFromToDate, 1, spdData.ActiveSheet.ColumnCount - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnCount - (cdvFromToDate.SubtractBetweenFromToDate + 1), cdvFromToDate.SubtractBetweenFromToDate + 1);

                        spdData.RPT_AddBasicColumn("Master plan", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                        spdData.RPT_AddDynamicColumn(cdvFromToDate, 1, spdData.ActiveSheet.ColumnCount - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnCount - (cdvFromToDate.SubtractBetweenFromToDate + 1), cdvFromToDate.SubtractBetweenFromToDate + 1);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("the required number of times", 0, 13, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                        spdData.RPT_AddDynamicColumn(cdvFromToDate, 1, 13, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 13, cdvFromToDate.SubtractBetweenFromToDate + 1);

                        spdData.RPT_AddBasicColumn("an excessive shortage of resources", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                        spdData.RPT_AddDynamicColumn(cdvFromToDate, 1, spdData.ActiveSheet.ColumnCount - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnCount - (cdvFromToDate.SubtractBetweenFromToDate + 1), cdvFromToDate.SubtractBetweenFromToDate + 1);

                        spdData.RPT_AddBasicColumn("Master plan", 0, spdData.ActiveSheet.ColumnCount, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                        spdData.RPT_AddDynamicColumn(cdvFromToDate, 1, spdData.ActiveSheet.ColumnCount - 1, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnCount - (cdvFromToDate.SubtractBetweenFromToDate + 1), cdvFromToDate.SubtractBetweenFromToDate + 1);
                    }
                }

                for (int i = 0; i <= 12; i++)
                {
                    spdData.RPT_MerageHeaderRowSpan(0, i, 2);
                }

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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation", "KEY_1", "KEY_1 AS OPER_GRP", "DECODE(KEY_1, 'TEST', 1, 'AUTO VISUAL', 2, 3)", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("group", "KEY_2", "KEY_2 AS GP", "KEY_2 DESC", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE", "KEY_4", "KEY_4 AS TYPE", "KEY_4", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment", "KEY_3", "KEY_3 AS SYS", "KEY_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("STEP", "OPER", "OPER", "OPER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT_GRP_2", "MAT_GRP_2 AS FAMILY", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT_GRP_3", "MAT_GRP_3 AS PACKAGE", "MAT_GRP_3", false);            
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT_GRP_6", "MAT_GRP_6 AS LD_COUNT", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "MAT_CMF_11", "MAT_CMF_11 AS PKG_CODE", "MAT_CMF_11", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT_ID", "MAT_ID AS PRODUCT", "MAT_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SAP CODE", "BASE_MAT_ID", "BASE_MAT_ID AS SAP_CODE", "BASE_MAT_ID", false);            
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
            string strSumQuery1 = null;
            string strSumQuery2 = null;
            string strSumQuery3 = null;
            string strSumQuery4 = null;
            string strSumQuery5 = null;
            string strSumQuery6 = null;
            string strRoundQuery1 = null;
            string strRoundQuery2 = null;
            string strRepeatQuery1 = null;
            string sPer = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            // 성능효율 부분
            if (txtPer.Text == "0")
            {
                sPer = "DATA_2";
            }
            else
            {
                sPer = txtPer.Text;
            }

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                #region 월간 조회
                string strMonth1 = cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM");
                string strMonth2 = cdvFromToDate.FromYearMonth.Value.AddMonths(1).ToString("yyyyMM");
                string strMonth3 = cdvFromToDate.FromYearMonth.Value.AddMonths(2).ToString("yyyyMM");
  
                strSqlString.Append("WITH DT AS" + "\n");
                strSqlString.Append("(" + "\n");
                strSqlString.Append("SELECT MAT_GRP_2, MAT_GRP_3, MAT_GRP_6, BASE_MAT_ID, MAT_CMF_11, MAT_ID" + "\n");
                strSqlString.Append("     , SUM(DECODE(PLAN_MONTH, '" + strMonth1 + "', REV_QTY, 0)) AS PLAN_0" + "\n");
                strSqlString.Append("     , SUM(DECODE(PLAN_MONTH, '" + strMonth2 + "', REV_QTY, 0)) AS PLAN_1" + "\n");
                strSqlString.Append("     , SUM(DECODE(PLAN_MONTH, '" + strMonth3 + "', REV_QTY, 0)) AS PLAN_2" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT C.PLAN_MONTH, A.PLAN_WEEK, A.GUBUN, A.WW_QTY, C.CNT" + "\n");
                strSqlString.Append("             , B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_6, B.BASE_MAT_ID, B.MAT_CMF_11, B.MAT_ID" + "\n");
                strSqlString.Append("             , ROUND((A.WW_QTY / 7) * C.CNT, 0) AS REV_QTY" + "\n");
                strSqlString.Append("          FROM RWIPPLNWEK A" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT DISTINCT MAT_GRP_2, MAT_GRP_3, MAT_GRP_6, BASE_MAT_ID, MAT_CMF_11, MAT_ID" + "\n");
                strSqlString.Append("                  FROM MWIPMATDEF" + "\n");
                strSqlString.Append("                 WHERE FACTORY IN ('" + GlobalVariable.gsTestDefaultFactory + "', 'HMKE1')" + "\n");
                strSqlString.Append("                   AND DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                   AND MAT_TYPE = 'FG' " + "\n");

                //상세 조회에 따른 SQL문 생성                        
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("                   AND MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("                   AND MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("                   AND MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("                   AND MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("                   AND MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("                   AND MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("                   AND MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("                   AND MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("                   AND MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

                strSqlString.Append("               ) B" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT PLAN_YEAR||LPAD(PLAN_MONTH,2,'0') AS PLAN_MONTH" + "\n");
                strSqlString.Append("                     , PLAN_YEAR||LPAD(PLAN_WEEK,2,'0') AS PLAN_WEEK" + "\n");
                strSqlString.Append("                     , COUNT(*) AS CNT " + "\n");
                strSqlString.Append("                  FROM MWIPCALDEF " + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND CALENDAR_ID = 'OTD'" + "\n");
                strSqlString.Append("                   AND PLAN_YEAR||LPAD(PLAN_MONTH,2,'0') IN ('" + strMonth1 + "', '" + strMonth2 + "', '" + strMonth3 + "')" + "\n");
                strSqlString.Append("                 GROUP BY PLAN_YEAR||LPAD(PLAN_MONTH,2,'0'), PLAN_YEAR||LPAD(PLAN_WEEK,2,'0') " + "\n");
                strSqlString.Append("               ) C" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");                
                strSqlString.Append("           AND A.MAT_ID = B.MAT_ID" + "\n");
                strSqlString.Append("           AND A.PLAN_WEEK = C.PLAN_WEEK" + "\n");
                strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND A.GUBUN = '3'" + "\n");                
                strSqlString.Append("           AND A.MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");                
                strSqlString.Append("       )" + "\n");
                strSqlString.Append(" GROUP BY MAT_GRP_2, MAT_GRP_3, MAT_GRP_6, BASE_MAT_ID, MAT_CMF_11, MAT_ID" + "\n");                
                strSqlString.Append(")" + "\n");
                strSqlString.AppendFormat("SELECT {0}, RES_Y_CNT, RES_N_CNT " + "\n", QueryCond2);
                strSqlString.Append("     , ROUND(SUM(NEED_0), 1) AS NEED_0" + "\n");
                strSqlString.Append("     , ROUND(SUM(NEED_1), 1) AS NEED_1" + "\n");
                strSqlString.Append("     , ROUND(SUM(NEED_2), 1) AS NEED_2" + "\n");
                strSqlString.Append("     , ROUND(RES_Y_CNT - SUM(NEED_0), 1) AS DEF_0" + "\n");
                strSqlString.Append("     , ROUND(RES_Y_CNT - SUM(NEED_1), 1) AS DEF_1" + "\n");
                strSqlString.Append("     , ROUND(RES_Y_CNT - SUM(NEED_2), 1) AS DEF_2" + "\n");
                strSqlString.Append("     , SUM(PLAN_0) AS PLAN_0" + "\n");
                strSqlString.Append("     , SUM(PLAN_1) AS PLAN_1" + "\n");
                strSqlString.Append("     , SUM(PLAN_2) AS PLAN_2" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT A.KEY_1" + "\n");
                strSqlString.Append("             , A.KEY_2" + "\n");
                strSqlString.Append("             , A.KEY_3" + "\n");
                strSqlString.Append("             , A.KEY_4" + "\n");
                strSqlString.Append("             , SUM(DECODE(A.KEY_2, 'HANDLER', HDR_Y_CNT, RES_Y_CNT)) AS RES_Y_CNT" + "\n");
                strSqlString.Append("             , SUM(DECODE(A.KEY_2, 'HANDLER', HDR_N_CNT, RES_N_CNT)) AS RES_N_CNT" + "\n");
                strSqlString.Append("          FROM MGCMTBLDAT A" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT 'RES_GRP_6' AS GUBUN, RES_GRP_6 AS DATA" + "\n");
                strSqlString.Append("                     , SUM(DECODE(RES_CMF_9, 'Y', 1, 0)) AS RES_Y_CNT" + "\n");
                strSqlString.Append("                     , SUM(DECODE(RES_CMF_9, 'N', 1, 0)) AS RES_N_CNT" + "\n");
                strSqlString.Append("                  FROM MRASRESDEF" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND DELETE_FLAG = ' ' " + "\n");                
                strSqlString.Append("                 GROUP BY RES_GRP_6 " + "\n");
                strSqlString.Append("                 UNION ALL" + "\n");
                strSqlString.Append("                SELECT 'RES_GRP_3' AS GUBUN, RES_GRP_3" + "\n");
                strSqlString.Append("                     , SUM(DECODE(RES_CMF_9, 'Y', 1, 0)) AS RES_Y_CNT" + "\n");
                strSqlString.Append("                     , SUM(DECODE(RES_CMF_9, 'N', 1, 0)) AS RES_N_CNT" + "\n");
                strSqlString.Append("                  FROM MRASRESDEF" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                 GROUP BY RES_GRP_3 " + "\n");
                strSqlString.Append("                 UNION ALL" + "\n");
                strSqlString.Append("                SELECT 'SUB_AREA_ID' AS GUBUN, SUB_AREA_ID" + "\n");
                strSqlString.Append("                     , SUM(DECODE(RES_CMF_9, 'Y', 1, 0)) AS RES_Y_CNT" + "\n");
                strSqlString.Append("                     , SUM(DECODE(RES_CMF_9, 'N', 1, 0)) AS RES_N_CNT" + "\n");
                strSqlString.Append("                  FROM MRASRESDEF" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                 GROUP BY SUB_AREA_ID " + "\n");
                strSqlString.Append("               ) B" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT SUBSTR(MODEL, 1, 6) AS HDR_ID" + "\n");
                strSqlString.Append("                     , SUM(DECODE(PROGRAM, 'Y', 1, 0)) AS HDR_Y_CNT" + "\n");
                strSqlString.Append("                     , SUM(DECODE(PROGRAM, 'N', 1, 0)) AS HDR_N_CNT" + "\n");
                strSqlString.Append("                  FROM CRASHDRDEF@RPTTOMES" + "\n");
                strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("                 GROUP BY SUBSTR(MODEL, 1, 6)" + "\n");
                strSqlString.Append("               ) C" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND A.DATA_1 = B.GUBUN(+)" + "\n");
                strSqlString.Append("           AND A.KEY_3 = C.HDR_ID(+)" + "\n");
                strSqlString.Append("           AND B.DATA(+) LIKE A.DATA_2" + "\n");
                strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND A.TABLE_NAME = 'H_CAPA_MACHINE_INFO'" + "\n");
                strSqlString.Append("         GROUP BY A.KEY_1, A.KEY_2, A.KEY_3, A.KEY_4" + "\n");
                strSqlString.Append("       ) A" + "\n");
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT KEY_3 AS GUBUN, KEY_2 AS OPER" + "\n");
                strSqlString.Append("             , B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_6, B.BASE_MAT_ID, B.MAT_CMF_11, B.MAT_ID" + "\n");
                strSqlString.Append("             , SUM((B.PLAN_0 * DATA_7) / ((86400 / (NVL(DATA_4,0) + NVL(DATA_5,0)) * (DATA_1/100) * (" + sPer + "/100) * (DATA_3/100) * KEY_4) * 7)) AS NEED_0" + "\n");
                strSqlString.Append("             , SUM((B.PLAN_1 * DATA_7) / ((86400 / (NVL(DATA_4,0) + NVL(DATA_5,0)) * (DATA_1/100) * (" + sPer + "/100) * (DATA_3/100) * KEY_4) * 7)) AS NEED_1" + "\n");
                strSqlString.Append("             , SUM((B.PLAN_2 * DATA_7) / ((86400 / (NVL(DATA_4,0) + NVL(DATA_5,0)) * (DATA_1/100) * (" + sPer + "/100) * (DATA_3/100) * KEY_4) * 7)) AS NEED_2" + "\n");
                strSqlString.Append("             , SUM((B.PLAN_0 * DATA_7)) AS PLAN_0" + "\n");
                strSqlString.Append("             , SUM((B.PLAN_1 * DATA_7)) AS PLAN_1" + "\n");
                strSqlString.Append("             , SUM((B.PLAN_2 * DATA_7)) AS PLAN_2" + "\n");
                strSqlString.Append("          FROM MGCMTBLDAT A" + "\n");
                strSqlString.Append("             , DT B" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND A.KEY_1 = B.MAT_ID" + "\n");
                strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND A.TABLE_NAME = 'H_CAPA_SIM_INFO'" + "\n");
                strSqlString.Append("         GROUP BY KEY_3, KEY_2, B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_6, B.BASE_MAT_ID, B.MAT_CMF_11, B.MAT_ID" + "\n");
                strSqlString.Append("         UNION ALL" + "\n");
                strSqlString.Append("        SELECT DATA_8 AS GUBUN, KEY_2 AS OPER" + "\n");
                strSqlString.Append("             , B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_6, B.BASE_MAT_ID, B.MAT_CMF_11, B.MAT_ID" + "\n");
                strSqlString.Append("             , SUM((B.PLAN_0 * DATA_7) / ((86400 / (NVL(DATA_4,0) + NVL(DATA_5,0)) * (DATA_1/100) * (" + sPer + "/100) * (DATA_3/100) * KEY_4) * 7)) AS NEED_0" + "\n");
                strSqlString.Append("             , SUM((B.PLAN_1 * DATA_7) / ((86400 / (NVL(DATA_4,0) + NVL(DATA_5,0)) * (DATA_1/100) * (" + sPer + "/100) * (DATA_3/100) * KEY_4) * 7)) AS NEED_1" + "\n");
                strSqlString.Append("             , SUM((B.PLAN_2 * DATA_7) / ((86400 / (NVL(DATA_4,0) + NVL(DATA_5,0)) * (DATA_1/100) * (" + sPer + "/100) * (DATA_3/100) * KEY_4) * 7)) AS NEED_2" + "\n");
                strSqlString.Append("             , SUM((B.PLAN_0 * DATA_7)) AS PLAN_0" + "\n");
                strSqlString.Append("             , SUM((B.PLAN_1 * DATA_7)) AS PLAN_1" + "\n");
                strSqlString.Append("             , SUM((B.PLAN_2 * DATA_7)) AS PLAN_2" + "\n");
                strSqlString.Append("          FROM MGCMTBLDAT A" + "\n");
                strSqlString.Append("             , DT B" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND A.KEY_1 = B.MAT_ID" + "\n");
                strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND A.TABLE_NAME = 'H_CAPA_SIM_INFO'" + "\n");
                strSqlString.Append("         GROUP BY DATA_8, KEY_2, B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_6, B.BASE_MAT_ID, B.MAT_CMF_11, B.MAT_ID" + "\n");
                strSqlString.Append("         UNION ALL" + "\n");
                strSqlString.Append("        SELECT 'AUTO VISUAL' AS GUBUN, '' AS OPER  " + "\n");
                strSqlString.Append("             , B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_6, B.BASE_MAT_ID, B.MAT_CMF_11, B.MAT_ID" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN C.FLOW IS NULL THEN 0 ELSE PLAN_0 / (80000 * 7) END) NEED_0" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN C.FLOW IS NULL THEN 0 ELSE PLAN_1 / (80000 * 7) END) NEED_1" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN C.FLOW IS NULL THEN 0 ELSE PLAN_2 / (80000 * 7) END) NEED_2" + "\n");
                strSqlString.Append("             , SUM(PLAN_0) AS PLAN_0" + "\n");
                strSqlString.Append("             , SUM(PLAN_1) AS PLAN_1" + "\n");
                strSqlString.Append("             , SUM(PLAN_2) AS PLAN_2" + "\n");
                strSqlString.Append("          FROM DT A" + "\n");
                strSqlString.Append("             , MWIPMATDEF B" + "\n");
                strSqlString.Append("             , (SELECT DISTINCT FLOW FROM MWIPFLWOPR@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND OPER ='T1100') C " + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND A.MAT_ID = B.MAT_ID" + "\n");
                strSqlString.Append("           AND B.FIRST_FLOW = C.FLOW " + "\n");
                strSqlString.Append("           AND B.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND B.MAT_TYPE = 'FG'" + "\n");
                strSqlString.Append("           AND B.DELETE_FLAG = ' ' " + "\n");
                strSqlString.Append("         GROUP BY B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_6, B.BASE_MAT_ID, B.MAT_CMF_11, B.MAT_ID" + "\n");
                strSqlString.Append("         UNION ALL" + "\n");
                strSqlString.Append("        SELECT 'TAPE & REEL' AS GUBUN, '' AS OPER  " + "\n");
                strSqlString.Append("             , B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_6, B.BASE_MAT_ID, B.MAT_CMF_11, B.MAT_ID" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN C.FLOW IS NULL THEN 0 ELSE PLAN_0 / (100000 * 7) END) NEED_0" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN C.FLOW IS NULL THEN 0 ELSE PLAN_1 / (100000 * 7) END) NEED_1" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN C.FLOW IS NULL THEN 0 ELSE PLAN_2 / (100000 * 7) END) NEED_2" + "\n");
                strSqlString.Append("             , SUM(PLAN_0) AS PLAN_0" + "\n");
                strSqlString.Append("             , SUM(PLAN_1) AS PLAN_1" + "\n");
                strSqlString.Append("             , SUM(PLAN_2) AS PLAN_2" + "\n");
                strSqlString.Append("          FROM DT A" + "\n");
                strSqlString.Append("             , MWIPMATDEF B" + "\n");
                strSqlString.Append("             , (SELECT DISTINCT FLOW FROM MWIPFLWOPR@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND OPER ='T1200') C " + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND A.MAT_ID = B.MAT_ID" + "\n");
                strSqlString.Append("           AND B.FIRST_FLOW = C.FLOW   " + "\n");
                strSqlString.Append("           AND B.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND B.MAT_TYPE = 'FG'" + "\n");
                strSqlString.Append("           AND B.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("         GROUP BY B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_6, B.BASE_MAT_ID, B.MAT_CMF_11, B.MAT_ID" + "\n");
                strSqlString.Append("       ) B" + "\n");
                strSqlString.Append(" WHERE 1=1" + "\n");
                strSqlString.Append("   AND A.KEY_3 = B.GUBUN(+)" + "\n");

                if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                    strSqlString.Append("   AND B.MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");

                strSqlString.AppendFormat(" GROUP BY {0}, RES_Y_CNT, RES_N_CNT " + "\n", QueryCond1);
                strSqlString.AppendFormat(" ORDER BY {0}, RES_Y_CNT, RES_N_CNT " + "\n", QueryCond3);                
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
                    strSumQuery1 += "     , SUM(DECODE(PLAN_WEEK, '" + selectDate[i].ToString() + "', WW_QTY, 0)) AS PLAN_" + i + "\n";
                    strRoundQuery1 += "     , ROUND(SUM(NEED_" + i + "), 1) AS NEED_" + i + "\n";
                    strRoundQuery2 += "     , ROUND(RES_Y_CNT - SUM(NEED_" + i + "), 1) AS DEF_" + i + "\n";
                    strRepeatQuery1 += "     , SUM(PLAN_" + i + ") AS PLAN_" + i + "\n";
                    strSumQuery2 += "             , SUM((B.PLAN_" + i + " * DATA_7) / ((86400 / (NVL(DATA_4,0) + NVL(DATA_5,0)) * (DATA_1/100) * (" + sPer + "/100) * (DATA_3/100) * KEY_4) * 7)) AS NEED_" + i + "\n";
                    strSumQuery3 += "             , SUM((B.PLAN_" + i + " * DATA_7)) AS PLAN_" + i + "\n";
                    strSumQuery4 += "             , SUM(CASE WHEN C.FLOW IS NULL THEN 0 ELSE PLAN_" + i + " / (80000 * 7) END) NEED_" + i + "\n";
                    strSumQuery5 += "             , SUM(CASE WHEN C.FLOW IS NULL THEN 0 ELSE PLAN_" + i + " / (100000 * 7) END) NEED_" + i + "\n";
                    strSumQuery6 += "             , SUM(PLAN_" + i + ") AS PLAN_" + i + "\n";
                }

                strSqlString.Append("WITH DT AS" + "\n");
                strSqlString.Append("(" + "\n");
                strSqlString.Append("SELECT B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_6, B.BASE_MAT_ID, B.MAT_CMF_11, B.MAT_ID" + "\n");
                strSqlString.Append(strSumQuery1);
                strSqlString.Append("  FROM RWIPPLNWEK A" + "\n");
                strSqlString.Append("     , (" + "\n");
                strSqlString.Append("        SELECT DISTINCT MAT_GRP_2, MAT_GRP_3, MAT_GRP_6, BASE_MAT_ID, MAT_CMF_11, MAT_ID" + "\n");
                strSqlString.Append("          FROM MWIPMATDEF" + "\n");
                strSqlString.Append("         WHERE FACTORY IN ('" + GlobalVariable.gsTestDefaultFactory + "', 'HMKE1')" + "\n");
                strSqlString.Append("           AND DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("           AND MAT_TYPE = 'FG' " + "\n");

                //상세 조회에 따른 SQL문 생성                        
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("           AND MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("           AND MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("           AND MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("           AND MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("           AND MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("           AND MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("           AND MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("           AND MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("           AND MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

                strSqlString.Append("       ) B" + "\n");
                strSqlString.Append(" WHERE 1=1" + "\n");                
                strSqlString.Append("   AND A.MAT_ID = B.MAT_ID" + "\n");
                strSqlString.Append("   AND A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("   AND A.PLAN_WEEK BETWEEN '" + sFrom + "' AND '" + sTo + "'" + "\n");
                strSqlString.Append("   AND A.GUBUN = '3' " + "\n");                
                strSqlString.Append("   AND A.MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");
                strSqlString.Append(" GROUP BY B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_6, B.BASE_MAT_ID, B.MAT_CMF_11, B.MAT_ID" + "\n");
                strSqlString.Append(")" + "\n");
                strSqlString.AppendFormat("SELECT {0}, RES_Y_CNT, RES_N_CNT " + "\n", QueryCond2);
                strSqlString.Append(strRoundQuery1);                
                strSqlString.Append(strRoundQuery2);
                strSqlString.Append(strRepeatQuery1);
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT A.KEY_1" + "\n");
                strSqlString.Append("             , A.KEY_2" + "\n");
                strSqlString.Append("             , A.KEY_3" + "\n");
                strSqlString.Append("             , A.KEY_4" + "\n");
                strSqlString.Append("             , SUM(DECODE(A.KEY_2, 'HANDLER', HDR_Y_CNT, RES_Y_CNT)) AS RES_Y_CNT" + "\n");
                strSqlString.Append("             , SUM(DECODE(A.KEY_2, 'HANDLER', HDR_N_CNT, RES_N_CNT)) AS RES_N_CNT" + "\n");
                strSqlString.Append("          FROM MGCMTBLDAT A" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT 'RES_GRP_6' AS GUBUN, RES_GRP_6 AS DATA" + "\n");
                strSqlString.Append("                     , SUM(DECODE(RES_CMF_9, 'Y', 1, 0)) AS RES_Y_CNT" + "\n");
                strSqlString.Append("                     , SUM(DECODE(RES_CMF_9, 'N', 1, 0)) AS RES_N_CNT" + "\n");
                strSqlString.Append("                  FROM MRASRESDEF" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                 GROUP BY RES_GRP_6 " + "\n");
                strSqlString.Append("                 UNION ALL" + "\n");
                strSqlString.Append("                SELECT 'RES_GRP_3' AS GUBUN, RES_GRP_3" + "\n");
                strSqlString.Append("                     , SUM(DECODE(RES_CMF_9, 'Y', 1, 0)) AS RES_Y_CNT" + "\n");
                strSqlString.Append("                     , SUM(DECODE(RES_CMF_9, 'N', 1, 0)) AS RES_N_CNT" + "\n");
                strSqlString.Append("                  FROM MRASRESDEF" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                 GROUP BY RES_GRP_3 " + "\n");
                strSqlString.Append("                 UNION ALL" + "\n");
                strSqlString.Append("                SELECT 'SUB_AREA_ID' AS GUBUN, SUB_AREA_ID" + "\n");
                strSqlString.Append("                     , SUM(DECODE(RES_CMF_9, 'Y', 1, 0)) AS RES_Y_CNT" + "\n");
                strSqlString.Append("                     , SUM(DECODE(RES_CMF_9, 'N', 1, 0)) AS RES_N_CNT" + "\n");
                strSqlString.Append("                  FROM MRASRESDEF" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                 GROUP BY SUB_AREA_ID " + "\n");
                strSqlString.Append("               ) B" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT SUBSTR(MODEL, 1, 6) AS HDR_ID" + "\n");
                strSqlString.Append("                     , SUM(DECODE(PROGRAM, 'Y', 1, 0)) AS HDR_Y_CNT" + "\n");
                strSqlString.Append("                     , SUM(DECODE(PROGRAM, 'N', 1, 0)) AS HDR_N_CNT" + "\n");
                strSqlString.Append("                  FROM CRASHDRDEF@RPTTOMES" + "\n");
                strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("                 GROUP BY SUBSTR(MODEL, 1, 6)" + "\n");
                strSqlString.Append("               ) C" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND A.DATA_1 = B.GUBUN(+)" + "\n");
                strSqlString.Append("           AND A.KEY_3 = C.HDR_ID(+)" + "\n");
                strSqlString.Append("           AND B.DATA(+) LIKE A.DATA_2" + "\n");
                strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND A.TABLE_NAME = 'H_CAPA_MACHINE_INFO'" + "\n");
                strSqlString.Append("         GROUP BY A.KEY_1, A.KEY_2, A.KEY_3, A.KEY_4" + "\n");
                strSqlString.Append("       ) A" + "\n");
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT KEY_3 AS GUBUN, KEY_2 AS OPER" + "\n");
                strSqlString.Append("             , B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_6, B.BASE_MAT_ID, B.MAT_CMF_11, B.MAT_ID" + "\n");
                strSqlString.Append(strSumQuery2);
                strSqlString.Append(strSumQuery3);
                strSqlString.Append("          FROM MGCMTBLDAT A" + "\n");
                strSqlString.Append("             , DT B" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND A.KEY_1 = B.MAT_ID" + "\n");
                strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND A.TABLE_NAME = 'H_CAPA_SIM_INFO'" + "\n");
                strSqlString.Append("         GROUP BY KEY_3, KEY_2, B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_6, B.BASE_MAT_ID, B.MAT_CMF_11, B.MAT_ID" + "\n");
                strSqlString.Append("         UNION ALL" + "\n");
                strSqlString.Append("        SELECT DATA_8 AS GUBUN, KEY_2 AS OPER" + "\n");
                strSqlString.Append("             , B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_6, B.BASE_MAT_ID, B.MAT_CMF_11, B.MAT_ID" + "\n");
                strSqlString.Append(strSumQuery2);
                strSqlString.Append(strSumQuery3);
                strSqlString.Append("          FROM MGCMTBLDAT A" + "\n");
                strSqlString.Append("             , DT B" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND A.KEY_1 = B.MAT_ID" + "\n");
                strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND A.TABLE_NAME = 'H_CAPA_SIM_INFO'" + "\n");
                strSqlString.Append("         GROUP BY DATA_8, KEY_2, B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_6, B.BASE_MAT_ID, B.MAT_CMF_11, B.MAT_ID" + "\n");
                strSqlString.Append("         UNION ALL" + "\n");
                strSqlString.Append("        SELECT 'AUTO VISUAL' AS GUBUN, '' AS OPER " + "\n");
                strSqlString.Append("             , B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_6, B.BASE_MAT_ID, B.MAT_CMF_11, B.MAT_ID" + "\n");
                strSqlString.Append(strSumQuery4);
                strSqlString.Append(strSumQuery6);
                strSqlString.Append("          FROM DT A" + "\n");
                strSqlString.Append("             , MWIPMATDEF B" + "\n");
                strSqlString.Append("             , (SELECT DISTINCT FLOW FROM MWIPFLWOPR@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND OPER ='T1100') C " + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND A.MAT_ID = B.MAT_ID" + "\n");
                strSqlString.Append("           AND B.FIRST_FLOW = C.FLOW " + "\n");
                strSqlString.Append("           AND B.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND B.MAT_TYPE = 'FG'" + "\n");
                strSqlString.Append("           AND B.DELETE_FLAG = ' ' " + "\n");
                strSqlString.Append("         GROUP BY B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_6, B.BASE_MAT_ID, B.MAT_CMF_11, B.MAT_ID" + "\n");
                strSqlString.Append("         UNION ALL" + "\n");
                strSqlString.Append("        SELECT 'TAPE & REEL' AS GUBUN, '' AS OPER " + "\n");
                strSqlString.Append("             , B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_6, B.BASE_MAT_ID, B.MAT_CMF_11, B.MAT_ID" + "\n");
                strSqlString.Append(strSumQuery5);
                strSqlString.Append(strSumQuery6);
                strSqlString.Append("          FROM DT A" + "\n");
                strSqlString.Append("             , MWIPMATDEF B" + "\n");
                strSqlString.Append("             , (SELECT DISTINCT FLOW FROM MWIPFLWOPR@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND OPER ='T1200') C " + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND A.MAT_ID = B.MAT_ID" + "\n");
                strSqlString.Append("           AND B.FIRST_FLOW = C.FLOW   " + "\n");
                strSqlString.Append("           AND B.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND B.MAT_TYPE = 'FG'" + "\n");
                strSqlString.Append("           AND B.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("         GROUP BY B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_6, B.BASE_MAT_ID, B.MAT_CMF_11, B.MAT_ID" + "\n");
                strSqlString.Append("       ) B" + "\n");
                strSqlString.Append(" WHERE 1=1" + "\n");
                strSqlString.Append("   AND A.KEY_3 = B.GUBUN(+)" + "\n");

                if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                    strSqlString.Append("   AND B.MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");

                strSqlString.AppendFormat(" GROUP BY {0}, RES_Y_CNT, RES_N_CNT " + "\n", QueryCond1);
                strSqlString.AppendFormat(" ORDER BY {0}, RES_Y_CNT, RES_N_CNT " + "\n", QueryCond3);                
                #endregion
            }

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
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 14, null, null, btnSort);
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                //Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 14, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.DataSource = dt;

                for (int i = 0; i <= 9; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LightYellow;
                }

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
            //this.SetFactory(cdvFactory.txtValue);
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }

        #endregion

        private void btnProduct_Click(object sender, EventArgs e)
        {
          
            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForPopup());

            if (dt != null && dt.Rows.Count > 0)
            {
                System.Windows.Forms.Form frm = new PRD011016_P1(dt);
                frm.ShowDialog();
            }            
            else
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD068", GlobalVariable.gcLanguage));
                return;
            }
        }

        private string MakeSqlStringForPopup()
        {
            StringBuilder strSqlString = new StringBuilder();

            string strMonth1 = null;
            string strMonth2 = null;
            string strMonth3 = null;
            string sFrom = null;
            string sTo = null;

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                strMonth1 = cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM");
                strMonth2 = cdvFromToDate.FromYearMonth.Value.AddMonths(1).ToString("yyyyMM");
                strMonth3 = cdvFromToDate.FromYearMonth.Value.AddMonths(2).ToString("yyyyMM");
            }
            else
            {
                sFrom = cdvFromToDate.FromWeek.SelectedValue.ToString();
                sTo = cdvFromToDate.ToWeek.SelectedValue.ToString();
            }           

            strSqlString.Append("SELECT MAT_ID" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT A.MAT_ID, B.KEY_1" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT DISTINCT MAT_ID" + "\n");
            strSqlString.Append("                  FROM RWIPPLNWEK     " + "\n");
            strSqlString.Append("                 WHERE 1=1   " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                strSqlString.Append("                   AND PLAN_WEEK IN (SELECT DISTINCT PLAN_YEAR||LPAD(PLAN_WEEK,2,'0') FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND PLAN_YEAR||LPAD(PLAN_MONTH,2,'0') IN ('" + strMonth1 + "', '" + strMonth2 + "', '" + strMonth3 + "'))" + "\n");
            }
            else
            {
                strSqlString.Append("                   AND PLAN_WEEK BETWEEN '" + sFrom + "' AND '" + sTo + "'" + "\n");                
            }

            strSqlString.Append("                   AND GUBUN = '3'  " + "\n");
            strSqlString.Append("               ) A" + "\n");
            strSqlString.Append("             , MGCMTBLDAT B     " + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.KEY_1(+)" + "\n");
            strSqlString.Append("           AND B.FACTORY(+) = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND B.TABLE_NAME(+) = 'H_CAPA_SIM_INFO'" + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" WHERE KEY_1 IS NULL" + "\n");
            strSqlString.Append(" ORDER BY MAT_ID" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
            
            return strSqlString.ToString();
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sProduct = null;            

            // 그룹정보가 아니고 HOLD RATE 가 아니면서 Null 값이 아닌경우 클릭 시 팝업 창 띄움.
            if (e.Column == 9 && spdData.ActiveSheet.Cells[e.Row, e.Column].Text != "" && spdData.ActiveSheet.Cells[e.Row, 1].Text == "TESTER")
            {
                sProduct = spdData.ActiveSheet.Cells[e.Row, e.Column].Text;

                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetail(sProduct));

                if (dt != null && dt.Rows.Count > 0)
                {
                    System.Windows.Forms.Form frm = new PRD011016_P2("", dt);
                    frm.ShowDialog();
                }
            }
            else
            {
                return;
            }
        }

        private string MakeSqlDetail(string sProduct)
        {
            StringBuilder strSqlString = new StringBuilder();

            string sPer = null;

            // 성능효율 부분
            if (txtPer.Text == "0")
            {
                sPer = "DATA_2";
            }
            else
            {
                sPer = txtPer.Text;
            }

            strSqlString.Append("SELECT KEY_3 AS SYS" + "\n");
            strSqlString.Append("     , DATA_8 AS HANDLER" + "\n");
            strSqlString.Append("     , DATA_4 AS TEST_TIME" + "\n");
            strSqlString.Append("     , DATA_5 AS INDEX_TIME" + "\n");
            strSqlString.Append("     , DATA_1 AS YIELD" + "\n");
            strSqlString.Append("     , " + sPer + " AS PERFORMANCE" + "\n");
            strSqlString.Append("     , KEY_4 AS PARA " + "\n");
            strSqlString.Append("     , ROUND(86400 / (NVL(DATA_4,0) + NVL(DATA_5,0)) * (DATA_1/100) * (" + sPer + "/100) * (DATA_3/100) * KEY_4,0) AS CAPA " + "\n");
            strSqlString.Append("  FROM MGCMTBLDAT" + "\n");         
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("   AND TABLE_NAME = 'H_CAPA_SIM_INFO'" + "\n");
            strSqlString.Append("   AND KEY_1 = '" + sProduct + "'" + "\n");
            strSqlString.Append(" ORDER BY KEY_3, DATA_8, DATA_4, DATA_5, DATA_1, KEY_4" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
                        
            return strSqlString.ToString();
        }


    }
}
