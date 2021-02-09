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
    public partial class MAT070506 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        /// <summary>
        /// 클  래  스: MAT070506<br/>
        /// 클래스요약: SMT 진도 관리_NEW<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2016-01-25<br/>
        /// 상세  설명: SMT 진도 관리(요청자 : 임태성 과장)<br/>
        /// 변경  내용: <br/>
        /// 2016-03-02-임종우 : Grand Total 추가 (박형순 요청)
        /// 2016-04-05-임종우 : 실적에 DA 1차 이후 재공 포함 (임태성K 요청)
        /// 2016-07-12-임종우 : CAPA 효율 GlobalVariable 로 선언하여 변경함.
        /// 2016-12-13-임종우 : 10단 칩 이상 스택 수 오류 수정
        /// 2019-02-07-임종우 : 창고 재고 과거 데이터 테이블 변경 ZHMMT111@SAPREAL -> CWMSLOTSTS_BOH
        /// 2020-04-28-김미경 : 제품 TYPE(STACK) 추가 반영 (이승희 D)
        /// </summary>
        public MAT070506()
        {
            InitializeComponent();

            cdvDate.Value = DateTime.Now;

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.udcWIPCondition1.sFactory = GlobalVariable.gsAssyDefaultFactory;
            cdvFactory.Enabled = false;
            SortInit();
            GridColumnInit();            
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer base", "MAT.CUSTOMER", "A.MAT_CODE_DESC", "MAT.MAT_CODE_DESC", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "MAT.PKG_CODE", "A.MAT_CMF_11", "MAT.MAT_CMF_11", true);            
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Material code", "MAT.MATCODE", "A.MAT_CODE", "MAT.MAT_CODE", true);            
        }

        #endregion

        #region 한줄헤더생성

        /// <summary>
        /// 헤더생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {  
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Customer base", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PKG CODE", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Material code", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("SMT Allocation", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);

            spdData.RPT_AddBasicColumn("PKG WIP", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);            
            spdData.RPT_AddBasicColumn("STOCK", 1, 4, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("DP", 1, 5, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("DA 1st", 1, 6, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("TTL", 1, 7, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_MerageHeaderColumnSpan(0, 4, 4);

            spdData.RPT_AddBasicColumn("SMT PCB", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);            
            spdData.RPT_AddBasicColumn("DA CAPA", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Uptime compared to CAPA", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Hours Available Compared to Daily Target", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("BarePCB", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("warehouse", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Operation warehouse", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("outsourcing", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("SMT waiting", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("TTL", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_MerageHeaderColumnSpan(0, 12, 5);

            spdData.RPT_AddBasicColumn("SMT Performance", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("today", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("the day before", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_MerageHeaderColumnSpan(0, 17, 2);
            
            spdData.RPT_AddBasicColumn("This week D / A 1st", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("plan", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("actual", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("residual quantity", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("a daily goal", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("SMT remaining", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_MerageHeaderColumnSpan(0, 19, 5);

            spdData.RPT_AddBasicColumn("this week + next week D / A 1st", 0, 24, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("plan", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("actual", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("residual quantity", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("a daily goal", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("SMT remaining", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_MerageHeaderColumnSpan(0, 24, 5);

            spdData.RPT_AddBasicColumn("Monthly D / A 1st", 0, 29, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("plan", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("actual", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("residual quantity", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("a daily goal", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("SMT remaining", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_MerageHeaderColumnSpan(0, 29, 5);            

            spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 8, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 9, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 10, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 11, 2);

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
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 0, null, null, btnSort);

                //3. Total부분 셀머지

                spdData.RPT_FillDataSelectiveCells("Total", 0, 4, 0, 1, true, Align.Center, VerticalAlign.Center);

                                
                //spdData.DataSource = dt;

                spdData.RPT_AutoFit(false);

                //  1. column header
                for (int i = 0; i <= 3; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LemonChiffon;
                }

                for (int y = 0; y < spdData_Sheet1.RowCount; y++)
                {
                    if (Convert.ToInt32(spdData.Sheets[0].Cells[y, 10].Value) <= 6)
                    {
                        spdData.ActiveSheet.Cells[y, 10].ForeColor = Color.Red;
                        spdData.ActiveSheet.Cells[y, 10].BackColor = Color.Pink;                        
                    }

                    if (Convert.ToInt32(spdData.Sheets[0].Cells[y, 11].Value) <= 6)
                    {
                        spdData.ActiveSheet.Cells[y, 11].ForeColor = Color.Red;
                        spdData.ActiveSheet.Cells[y, 11].BackColor = Color.Pink; 
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
            string strKpcs = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            GlobalVariable.FindWeek FindWeek = new GlobalVariable.FindWeek();
            FindWeek = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");
            // 시간 관련 셋팅
            string sToday = cdvDate.SelectedValue();
            string sYesterday = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");
            string sStartWeekDay = FindWeek.StartDay_ThisWeek;
            string sEndWeekDay = FindWeek.EndDay_ThisWeek;
            string sThisWeek = FindWeek.ThisWeek;
            string sNextWeek = FindWeek.NextWeek;
            string sMonth = cdvDate.Value.ToString("yyyyMM");
            string sStartMonthDay = cdvDate.Value.ToString("yyyyMM") + "01";

            if (ckbKpcs.Checked == true)
            {
                strKpcs = "1000";
            }
            else
            {
                strKpcs = "1";
            }
            
            // 쿼리
            strSqlString.Append("SELECT " + QueryCond1 + "\n");
            strSqlString.Append("     , RAS.RES_ID " + "\n");
            strSqlString.Append("     , ROUND(DAT.WIP_STOCK / " + strKpcs + ", 0) AS WIP_STOCK" + "\n");
            strSqlString.Append("     , ROUND(DAT.WIP_DP / " + strKpcs + ", 0) AS WIP_DP" + "\n");
            strSqlString.Append("     , ROUND(DAT.WIP_DA / " + strKpcs + ", 0) AS WIP_DA" + "\n");
            strSqlString.Append("     , ROUND(DAT.WIP_TTL / " + strKpcs + ", 0) AS WIP_TTL" + "\n");
            strSqlString.Append("     , ROUND(WMS.SMT / " + strKpcs + ", 0) AS SMT" + "\n");            
            strSqlString.Append("     , DAT.DA_CAPA" + "\n");
            strSqlString.Append("     , ROUND(CASE WHEN NVL(DAT.DA_CAPA,0) = 0 THEN 0" + "\n");
            strSqlString.Append("                  ELSE (WMS.SMT / DAT.DA_CAPA) * 24" + "\n");
            strSqlString.Append("             END, 0) AS SMTPCB_CAPA" + "\n");
            strSqlString.Append("     , ROUND(CASE WHEN NVL(DAT.W1_TARGET,0) = 0 THEN 0" + "\n");
            strSqlString.Append("                  ELSE (WMS.SMT / DAT.W1_TARGET) * 24" + "\n");
            strSqlString.Append("             END, 0) AS SMTPCB_TARGET" + "\n");            
            strSqlString.Append("     , ROUND(WMS.INV_QTY / " + strKpcs + ", 0) AS INV_QTY" + "\n");
            strSqlString.Append("     , ROUND(WMS.INV_L_QTY / " + strKpcs + ", 0) AS INV_L_QTY" + "\n");            
            strSqlString.Append("     , ROUND(WMS.WIK_WIP / " + strKpcs + ", 0) AS WIK_WIP" + "\n");
            strSqlString.Append("     , ROUND(WMS.SMT_WAIT / " + strKpcs + ", 0) AS SMT_WAIT" + "\n");
            strSqlString.Append("     , ROUND((NVL(WMS.INV_QTY,0) + NVL(WMS.INV_L_QTY,0) + NVL(WMS.WIK_WIP,0) + NVL(WMS.SMT_WAIT,0)) / " + strKpcs + ", 0) AS SMT_WIP_TTL" + "\n");            
            strSqlString.Append("     , ROUND(S_OUT.T_SMT_OUT / " + strKpcs + ", 0) AS T_SMT_OUT" + "\n");
            strSqlString.Append("     , ROUND(S_OUT.Y_SMT_OUT / " + strKpcs + ", 0) AS Y_SMT_OUT" + "\n");
            strSqlString.Append("     , ROUND(DAT.W0_PLAN / " + strKpcs + ", 0) AS W0_PLAN" + "\n");
            strSqlString.Append("     , ROUND(DAT.W0_AO / " + strKpcs + ", 0) AS W0_AO" + "\n");
            strSqlString.Append("     , ROUND(DAT.W0_DEF / " + strKpcs + ", 0) AS W0_DEF" + "\n");
            strSqlString.Append("     , ROUND(DAT.W0_TARGET / " + strKpcs + ", 0) AS W0_TARGET" + "\n");
            strSqlString.Append("     , ROUND((NVL(DAT.W0_DEF,0) - NVL(WMS.SMT,0)) / " + strKpcs + ", 0) AS W0_SMT_DEF" + "\n");
            strSqlString.Append("     , ROUND(DAT.W1_PLAN / " + strKpcs + ", 0) AS W1_PLAN" + "\n");
            strSqlString.Append("     , ROUND(DAT.W1_AO / " + strKpcs + ", 0) AS W1_AO" + "\n");
            strSqlString.Append("     , ROUND(DAT.W1_DEF / " + strKpcs + ", 0) AS W1_DEF" + "\n");
            strSqlString.Append("     , ROUND(DAT.W1_TARGET / " + strKpcs + ", 0) AS W1_TARGET" + "\n");
            strSqlString.Append("     , ROUND((NVL(DAT.W1_DEF,0) - NVL(WMS.SMT,0)) / " + strKpcs + ", 0) AS W1_SMT_DEF" + "\n");
            strSqlString.Append("     , ROUND(DAT.MON_PLAN / " + strKpcs + ", 0) AS MON_PLAN" + "\n");
            strSqlString.Append("     , ROUND(DAT.MONTH_AO / " + strKpcs + ", 0) AS MONTH_AO" + "\n");
            strSqlString.Append("     , ROUND(DAT.MON_DEF / " + strKpcs + ", 0) AS MON_DEF" + "\n");
            strSqlString.Append("     , ROUND(DAT.MON_TARGET / " + strKpcs + ", 0) AS MON_TARGET" + "\n");
            strSqlString.Append("     , ROUND((NVL(DAT.MON_DEF,0) - NVL(WMS.SMT,0)) / " + strKpcs + ", 0) AS MON_SMT_DEF" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT MATCODE, WM_CONCAT(MAT_CMF_11) AS PKG_CODE, WM_CONCAT(MAT_GRP_1) AS CUSTOMER" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT MATCODE, MAT_GRP_1, WM_CONCAT(MAT_CMF_11) AS MAT_CMF_11" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT DISTINCT REPLACE(A.MATCODE, '-O', '') AS MATCODE, B.MAT_CMF_11, B.MAT_GRP_1" + "\n");
            strSqlString.Append("                          FROM CWIPBOMDEF A" + "\n");
            strSqlString.Append("                             , MWIPMATDEF B" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND A.PARTNUMBER = B.MAT_ID " + "\n");
            strSqlString.Append("                           AND A.STEPID = 'A0300'" + "\n");
            strSqlString.Append("                           AND A.RESV_FIELD_2 = 'PB'" + "\n");
            strSqlString.Append("                           AND A.RESV_FLAG_1 = 'Y' " + "\n");
            strSqlString.Append("                           AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND B.DELETE_FLAG = ' '" + "\n");

            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                strSqlString.Append("                           AND B.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            if (txtMatCode.Text.Trim() != "%" && txtMatCode.Text.Trim() != "")
                strSqlString.Append("                           AND A.MATCODE LIKE '" + txtMatCode.Text + "'" + "\n");

            #region 상세 조회에 따른 SQL문 생성

            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion

            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY MATCODE, MAT_GRP_1" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY MATCODE" + "\n");
            strSqlString.Append("       ) MAT" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MATCODE" + "\n");
            strSqlString.Append("             , SUM(WIP_STOCK) AS WIP_STOCK" + "\n");
            strSqlString.Append("             , SUM(WIP_DP) AS WIP_DP" + "\n");
            strSqlString.Append("             , SUM(WIP_DA) AS WIP_DA" + "\n");
            strSqlString.Append("             , SUM(WIP_STOCK + WIP_DP + WIP_DA) AS WIP_TTL" + "\n");
            strSqlString.Append("             , SUM(DA_CAPA) AS DA_CAPA" + "\n");
            strSqlString.Append("             , SUM(W0_PLAN) AS W0_PLAN" + "\n");
            strSqlString.Append("             , SUM(WEEK_AO) AS W0_AO" + "\n");
            strSqlString.Append("             , SUM(W0_DEF) AS W0_DEF" + "\n");
            strSqlString.Append("             , SUM(ROUND(W0_TARGET,0)) AS W0_TARGET" + "\n");
            strSqlString.Append("             , SUM(W1_PLAN) AS W1_PLAN" + "\n");
            strSqlString.Append("             , SUM(WEEK_AO) AS W1_AO" + "\n");
            strSqlString.Append("             , SUM(W1_DEF) AS W1_DEF" + "\n");
            strSqlString.Append("             , SUM(ROUND(W1_TARGET,0)) AS W1_TARGET" + "\n");
            strSqlString.Append("             , SUM(MON_PLAN) AS MON_PLAN" + "\n");
            strSqlString.Append("             , SUM(MONTH_AO) AS MONTH_AO" + "\n");
            strSqlString.Append("             , SUM(MON_DEF) AS MON_DEF" + "\n");
            strSqlString.Append("             , SUM(ROUND(MON_TARGET,0)) AS MON_TARGET" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT A.MAT_ID, A.MATCODE, A.STACK, A.W0_DEF_DAY, A.W1_DEF_DAY, A.MON_DEF_DAY" + "\n");
            strSqlString.Append("                     , NVL(B.W0_PLAN,0) AS W0_PLAN" + "\n");
            strSqlString.Append("                     , NVL(B.W1_PLAN,0) AS W1_PLAN" + "\n");
            strSqlString.Append("                     , NVL(C.MON_PLAN,0) AS MON_PLAN" + "\n");            
            strSqlString.Append("                     , NVL(D.WIP_STOCK,0) AS WIP_STOCK" + "\n");
            strSqlString.Append("                     , NVL(D.WIP_DP,0) AS WIP_DP" + "\n");
            strSqlString.Append("                     , NVL(D.WIP_DA,0) AS WIP_DA" + "\n");            
            strSqlString.Append("                     , NVL(D.WIP_DA_AFTER,0) AS WIP_DA_AFTER" + "\n");
            strSqlString.Append("                     , NVL(E.WEEK_AO,0) + NVL(D.WIP_DA_AFTER,0) AS WEEK_AO" + "\n");
            strSqlString.Append("                     , NVL(E.MONTH_AO,0) + NVL(D.WIP_DA_AFTER,0) AS MONTH_AO" + "\n");
            strSqlString.Append("                     , NVL(B.W0_PLAN,0) - (NVL(E.WEEK_AO,0) + NVL(D.WIP_DA_AFTER,0)) AS W0_DEF" + "\n");
            strSqlString.Append("                     , CASE WHEN NVL(B.W0_PLAN,0) - (NVL(E.WEEK_AO,0) + NVL(D.WIP_DA_AFTER,0)) <= 0 THEN 0" + "\n");
            strSqlString.Append("                            ELSE (NVL(B.W0_PLAN,0) - (NVL(E.WEEK_AO,0) + NVL(D.WIP_DA_AFTER,0))) / A.W0_DEF_DAY " + "\n");
            strSqlString.Append("                       END AS W0_TARGET" + "\n");
            strSqlString.Append("                     , NVL(B.W0_PLAN,0) + NVL(B.W1_PLAN,0) - (NVL(E.WEEK_AO,0) + NVL(D.WIP_DA_AFTER,0)) AS W1_DEF" + "\n");
            strSqlString.Append("                     , CASE WHEN NVL(B.W0_PLAN,0) + NVL(B.W1_PLAN,0) - (NVL(E.WEEK_AO,0) + NVL(D.WIP_DA_AFTER,0)) <= 0 THEN 0" + "\n");
            strSqlString.Append("                            ELSE (NVL(B.W0_PLAN,0) + NVL(B.W1_PLAN,0) - (NVL(E.WEEK_AO,0) + NVL(D.WIP_DA_AFTER,0))) / A.W1_DEF_DAY " + "\n");
            strSqlString.Append("                       END AS W1_TARGET" + "\n");
            strSqlString.Append("                     , NVL(C.MON_PLAN,0) - (NVL(E.MONTH_AO,0) + NVL(D.WIP_DA_AFTER,0)) AS MON_DEF" + "\n");
            strSqlString.Append("                     , CASE WHEN NVL(C.MON_PLAN,0) - (NVL(E.MONTH_AO,0) + NVL(D.WIP_DA_AFTER,0)) <= 0 THEN 0" + "\n");
            strSqlString.Append("                            ELSE (NVL(C.MON_PLAN,0) - (NVL(E.MONTH_AO,0) + NVL(D.WIP_DA_AFTER,0))) / A.MON_DEF_DAY " + "\n");
            strSqlString.Append("                       END AS MON_TARGET" + "\n");
            strSqlString.Append("                     , NVL(F.DA_CAPA,0) AS DA_CAPA " + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID, MATCODE, STACK " + "\n");
            strSqlString.Append("                             , CASE WHEN W0_CUTOFF - (STACK + 1) < 1 THEN 1 ELSE W0_CUTOFF - (STACK + 1) END AS W0_DEF_DAY" + "\n");
            strSqlString.Append("                             , CASE WHEN (W0_CUTOFF + 7) - (STACK + 1) < 1 THEN 1 ELSE (W0_CUTOFF + 7) - (STACK + 1) END AS W1_DEF_DAY " + "\n");
            strSqlString.Append("                             , CASE WHEN MON_CUTOFF - (STACK + 1) < 1 THEN 1 ELSE MON_CUTOFF - (STACK + 1) END AS MON_DEF_DAY" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT MAT_ID, MATCODE, STACK" + "\n");
            strSqlString.Append("                                     , CASE TO_CHAR(TO_DATE('" + sToday + "', 'YYYYMMDD'), 'DY') WHEN '월' THEN 4" + "\n");
            strSqlString.Append("                                                                                           WHEN '화' THEN 3" + "\n");
            strSqlString.Append("                                                                                           WHEN '수' THEN 2" + "\n");
            strSqlString.Append("                                                                                           WHEN '목' THEN 1" + "\n");
            strSqlString.Append("                                                                                           WHEN '금' THEN 0" + "\n");
            strSqlString.Append("                                                                                           WHEN '토' THEN 7" + "\n");
            strSqlString.Append("                                                                                           WHEN '일' THEN 6" + "\n");
            strSqlString.Append("                                       END AS W0_CUTOFF" + "\n");
            strSqlString.Append("                                     , LAST_DAY(TO_DATE('" + sToday + "', 'YYYYMMDD')) - TO_DATE('" + sToday + "', 'YYYYMMDD') AS MON_CUTOFF " + "\n");
            strSqlString.Append("                                  FROM (" + "\n");
            strSqlString.Append("                                        SELECT DISTINCT PARTNUMBER AS MAT_ID, REPLACE(MATCODE, '-O', '') AS MATCODE" + "\n");
            strSqlString.Append("                                            , CASE WHEN B.MAT_GRP_4 IN ('-', 'FD', 'FU') THEN '1'" + "\n");
            strSqlString.Append("                                                   WHEN B.MAT_GRP_4 IN ('DDP') THEN '2'" + "\n");
            strSqlString.Append("                                                   WHEN B.MAT_GRP_4 IN ('QDP') THEN '4'" + "\n");
            strSqlString.Append("                                                   WHEN B.MAT_GRP_4 IN ('ODP') THEN '8'" + "\n");
            strSqlString.Append("                                                   ELSE REGEXP_REPLACE(B.MAT_GRP_4, '[^[:digit:]]')" + "\n");
            strSqlString.Append("                                               END AS STACK" + "\n");
            //strSqlString.Append("                                             , CASE WHEN B.MAT_GRP_4 IN ('-','FD','FU') THEN '2'" + "\n");            
            //strSqlString.Append("                                                    ELSE SUBSTR(MAT_GRP_4, 3)" + "\n");
            //strSqlString.Append("                                               END AS STACK  " + "\n");
            strSqlString.Append("                                             , MAT_GRP_4" + "\n");
            strSqlString.Append("                                          FROM CWIPBOMDEF A" + "\n");
            strSqlString.Append("                                             , MWIPMATDEF B" + "\n");
            strSqlString.Append("                                         WHERE 1=1" + "\n");
            strSqlString.Append("                                           AND A.PARTNUMBER = B.MAT_ID   " + "\n");
            strSqlString.Append("                                           AND A.STEPID = 'A0300'" + "\n");
            strSqlString.Append("                                           AND A.RESV_FIELD_2 = 'PB'" + "\n");
            strSqlString.Append("                                           AND A.RESV_FLAG_1 = 'Y' " + "\n");
            strSqlString.Append("                                           AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                           AND B.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                       )" + "\n");
            strSqlString.Append("                               )" + "\n");
            strSqlString.Append("                       ) A    " + "\n");
            strSqlString.Append("                     , ( " + "\n");
            strSqlString.Append("                        SELECT MAT_ID" + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + sThisWeek + "', WW_QTY, 0)) AS W0_PLAN" + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + sNextWeek + "', WW_QTY, 0)) AS W1_PLAN" + "\n");
            strSqlString.Append("                          FROM RWIPPLNWEK " + "\n");
            strSqlString.Append("                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND PLAN_WEEK IN ('" + sThisWeek + "', '" +sNextWeek + "')" + "\n");
            strSqlString.Append("                           AND GUBUN = '3' " + "\n");
            strSqlString.Append("                        GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                       ) B " + "\n");
            strSqlString.Append("                     , (          " + "\n");
            strSqlString.Append("                        SELECT MAT_ID, SUM(TO_NUMBER(DECODE(RESV_FIELD1,' ',0,RESV_FIELD1))) AS MON_PLAN " + "\n");
            strSqlString.Append("                          FROM CWIPPLNMON " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND PLAN_MONTH = '" + sMonth + "'" + "\n");
            strSqlString.Append("                           AND TO_NUMBER(DECODE(RESV_FIELD1,' ',0,RESV_FIELD1)) > 0 " + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID  " + "\n");
            strSqlString.Append("                       ) C" + "\n");
            strSqlString.Append("                     , ( " + "\n");
            strSqlString.Append("                        SELECT MAT_ID" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 = '1st' AND OPER = 'A0000' THEN QTY_1 ELSE 0 END) AS WIP_STOCK" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 = '1st' AND OPER BETWEEN 'A0001' AND 'A0399' THEN QTY_1 ELSE 0 END) AS WIP_DP" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 = '1st' AND OPER IN ('A0400', 'A0401') THEN QTY_1 ELSE 0 END) AS WIP_DA" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER >= 'A0402' THEN QTY_1 ELSE 0 END) AS WIP_DA_AFTER" + "\n");
            strSqlString.Append("                          FROM ( " + "\n");
            strSqlString.Append("                                SELECT A.MAT_ID, OPER, B.MAT_GRP_5 " + "\n");
            strSqlString.Append("                                     , CASE WHEN B.MAT_GRP_1 = 'HX' AND B.HX_COMP_MIN IS NOT NULL" + "\n");
            strSqlString.Append("                                                 THEN (CASE WHEN HX_COMP_MIN <> HX_COMP_MAX AND OPER > HX_COMP_MIN AND OPER <= HX_COMP_MAX THEN QTY_1 / NVL(COMP_CNT / 2,1)" + "\n");
            strSqlString.Append("                                                            WHEN OPER <= HX_COMP_MAX THEN QTY_1 / NVL(COMP_CNT,1)" + "\n");
            strSqlString.Append("                                                            ELSE QTY_1 END)" + "\n");
            strSqlString.Append("                                            WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1) " + "\n");
            strSqlString.Append("                                            ELSE QTY_1 " + "\n");
            strSqlString.Append("                                       END QTY_1 " + "\n");
            strSqlString.Append("                                     , COMP_CNT  " + "\n");
            strSqlString.Append("                                     , HX_COMP_MIN, HX_COMP_MAX " + "\n");

            // 금일조회 기준
            if (DateTime.Now.ToString("yyyyMMdd") == sToday)
            {
                strSqlString.Append("                                  FROM RWIPLOTSTS A " + "\n");
                strSqlString.Append("                                     , VWIPMATDEF B" + "\n");
                strSqlString.Append("                                 WHERE 1=1 " + "\n");
            }
            else// 금일이 아니면 스냅샷 떠놓은 테이블에서 가져옴.
            {
                strSqlString.Append("                                  FROM RWIPLOTSTS_BOH A" + "\n");
                strSqlString.Append("                                     , VWIPMATDEF B" + "\n");
                strSqlString.Append("                                 WHERE CUTOFF_DT = '" + sToday + "22' " + "\n");
            }
            
            strSqlString.Append("                                   AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                                   AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("                                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                                   AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                                   AND A.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                                   AND B.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                                   AND B.MAT_GRP_2 <> '-' " + "\n");
            strSqlString.Append("                                   AND REGEXP_LIKE(B.MAT_GRP_5, 'Middle*|Merge|1st|-') " + "\n");            

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                                   AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                               ) " + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                       ) D" + "\n");
            strSqlString.Append("                     , ( " + "\n");
            strSqlString.Append("                        SELECT MAT_ID " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE BETWEEN '" + sStartWeekDay + "' AND '" + sEndWeekDay + "' THEN (S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1) END) AS WEEK_AO  " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE BETWEEN '" + sStartMonthDay + "' AND '" + sToday + "' THEN (S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1) END) AS MONTH_AO  " + "\n");
            strSqlString.Append("                          FROM RSUMFACMOV " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND WORK_DATE BETWEEN LEAST('" + sStartWeekDay + "', '" + sStartMonthDay + "') AND '" + sToday + "'" + "\n");
            strSqlString.Append("                           AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                           AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND CM_KEY_2 = 'PROD' " + "\n");            

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                           AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                           AND FACTORY NOT IN ('RETURN') " + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("                       ) E " + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT MAT.MAT_ID" + "\n");
            strSqlString.Append("                             , SUM(TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_da + " * RES.RAS_CNT, 0))) AS DA_CAPA" + "\n");
            strSqlString.Append("                          FROM ( " + "\n");
            strSqlString.Append("                                SELECT FACTORY, RES_STS_2, RES_STS_8 AS OPER " + "\n");
            strSqlString.Append("                                     , RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, COUNT(RES_ID) AS RAS_CNT " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == sToday)
            {
                strSqlString.Append("                                  FROM MRASRESDEF " + "\n");
                strSqlString.Append("                                 WHERE 1=1" + "\n");
            }
            else
            {
                strSqlString.Append("                                  FROM MRASRESDEF_BOH " + "\n");
                strSqlString.Append("                                 WHERE CUTOFF_DT = '" + sToday + "'" + "\n");
            }
            
            strSqlString.Append("                                   AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                   AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("                                   AND RES_CMF_7 = 'Y' " + "\n");
            strSqlString.Append("                                   AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                                   AND (RES_STS_8 LIKE 'A04%' OR RES_STS_8 = 'A0333')" + "\n");
            strSqlString.Append("                                   AND (RES_STS_1 NOT IN ('C200', 'B199') OR RES_UP_DOWN_FLAG = 'U') " + "\n");
            strSqlString.Append("                                 GROUP BY FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7 " + "\n");
            strSqlString.Append("                               ) RES " + "\n");
            strSqlString.Append("                             , CRASUPHDEF UPH " + "\n");
            strSqlString.Append("                             , MWIPMATDEF MAT " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND RES.FACTORY = UPH.FACTORY(+) " + "\n");
            strSqlString.Append("                           AND RES.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("                           AND RES.OPER = UPH.OPER(+) " + "\n");
            strSqlString.Append("                           AND RES.RES_MODEL = UPH.RES_MODEL(+) " + "\n");
            strSqlString.Append("                           AND RES.UPEH_GRP = UPH.UPEH_GRP(+) " + "\n");
            strSqlString.Append("                           AND RES.RES_STS_2 = UPH.MAT_ID(+) " + "\n");
            strSqlString.Append("                           AND RES.RES_STS_2 = MAT.MAT_ID " + "\n");
            strSqlString.Append("                           AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                           AND MAT.MAT_GRP_5 IN ('-', '1st') " + "\n");
            strSqlString.Append("                         GROUP BY MAT.MAT_ID " + "\n");
            strSqlString.Append("                       ) F" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = C.MAT_ID(+)" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = D.MAT_ID(+)" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = E.MAT_ID(+)" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = F.MAT_ID(+)" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY MATCODE " + "\n");
            strSqlString.Append("       ) DAT" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT RES_STS_2 AS MATCODE, WM_CONCAT(RES_ID) AS RES_ID" + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == sToday)
            {
                strSqlString.Append("          FROM MRASRESDEF" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
            }
            else
            {
                strSqlString.Append("          FROM MRASRESDEF_BOH " + "\n");
                strSqlString.Append("         WHERE CUTOFF_DT = '" + sToday + "'" + "\n");
            }

            strSqlString.Append("           AND RES_STS_8 = 'A0330'" + "\n");
            strSqlString.Append("           AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("           AND RES_CMF_7 = 'Y' " + "\n");
            strSqlString.Append("           AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND RES_STS_2 IN (" + "\n");
            strSqlString.Append("                             SELECT DISTINCT REPLACE(MATCODE, '-O', '') AS MATCODE" + "\n");
            strSqlString.Append("                               FROM CWIPBOMDEF" + "\n");
            strSqlString.Append("                              WHERE 1=1" + "\n");
            strSqlString.Append("                                AND STEPID = 'A0300'" + "\n");
            strSqlString.Append("                                AND RESV_FIELD_2 = 'PB'" + "\n");
            strSqlString.Append("                                AND RESV_FLAG_1 = 'Y'" + "\n");
            strSqlString.Append("                            )   " + "\n");
            strSqlString.Append("         GROUP BY RES_STS_2" + "\n");
            strSqlString.Append("       ) RAS" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MATCODE" + "\n");
            strSqlString.Append("             , SUM(SMT) AS SMT" + "\n");
            strSqlString.Append("             , SUM(SMT_WAIT) AS SMT_WAIT" + "\n");
            strSqlString.Append("             , SUM(INV_QTY) AS INV_QTY" + "\n");
            strSqlString.Append("             , SUM(INV_L_QTY) AS INV_L_QTY" + "\n");
            strSqlString.Append("             , SUM(WIK_WIP) AS WIK_WIP" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT MATCODE" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN GUBUN = 'SMT' THEN QTY_1 ELSE 0 END) SMT" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN GUBUN = 'SMT_WAIT' THEN QTY_1 ELSE 0 END) SMT_WAIT" + "\n");
            strSqlString.Append("                     , 0 AS INV_QTY, 0 AS INV_L_QTY, 0 AS WIK_WIP" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT REPLACE(MAT_ID, '-O', '') AS MATCODE, OPER" + "\n");
            strSqlString.Append("                             , CASE WHEN OPER <= 'M0330' THEN 'SMT_WAIT' ELSE 'SMT' END AS GUBUN" + "\n");
            strSqlString.Append("                             , QTY_1" + "\n");

            // 금일조회 기준
            if (DateTime.Now.ToString("yyyyMMdd") == sToday)
            {
                strSqlString.Append("                          FROM RWIPLOTSTS " + "\n");
                strSqlString.Append("                         WHERE 1=1 " + "\n");
            }
            else// 금일이 아니면 스냅샷 떠놓은 테이블에서 가져옴.
            {
                strSqlString.Append("                          FROM RWIPLOTSTS_BOH" + "\n");
                strSqlString.Append("                         WHERE CUTOFF_DT = '" + sToday + "22' " + "\n");

            }
        
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND LOT_TYPE = 'P'" + "\n");
            strSqlString.Append("                           AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND LOT_CMF_2 = '-'" + "\n");
            strSqlString.Append("                           AND LOT_CMF_9 != ' '" + "\n");
            strSqlString.Append("                           AND QTY_1 > 0" + "\n");
            strSqlString.Append("                           AND OPER <>  'V0000'" + "\n");
            strSqlString.Append("                           AND (OPER LIKE 'M%' OR OPER LIKE 'V%')" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY MATCODE" + "\n");
            strSqlString.Append("                 UNION ALL" + "\n");
            strSqlString.Append("                SELECT MATCODE" + "\n");
            strSqlString.Append("                     , 0, 0" + "\n");
            strSqlString.Append("                     , SUM(INV_QTY) AS INV_QTY" + "\n");
            strSqlString.Append("                     , SUM(INV_L_QTY) AS INV_L_QTY" + "\n");
            strSqlString.Append("                     , 0" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID AS MATCODE" + "\n");
            strSqlString.Append("                             , SUM(DECODE(STORAGE_LOCATION, '1000', QUANTITY, '1003', QUANTITY, 0)) AS INV_QTY" + "\n");
            strSqlString.Append("                             , SUM(DECODE(STORAGE_LOCATION, '1001', QUANTITY, 0)) AS INV_L_QTY" + "\n");

            // 금일 조회
            if (DateTime.Now.ToString("yyyyMMdd") == sToday)
            {                
                strSqlString.Append("                          FROM CWMSLOTSTS@RPTTOMES" + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");                
            }
            else
            {
                strSqlString.Append("                          FROM CWMSLOTSTS_BOH@RPTTOMES " + "\n");
                strSqlString.Append("                         WHERE CUTOFF_DT = '" + sToday + "22'" + "\n");                
            }

            strSqlString.Append("                           AND QUANTITY> 0" + "\n");
            strSqlString.Append("                           AND STORAGE_LOCATION IN ('1000', '1001', '1003')" + "\n");
            strSqlString.Append("                           AND MAT_ID LIKE 'R16%'" + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                         UNION ALL " + "\n");
            strSqlString.Append("                        SELECT MAT_ID, 0 AS INV_QTY, SUM(QTY_1) AS INV_L_QTY " + "\n");
            strSqlString.Append("                          FROM CWIPMATSLP@RPTTOMES " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND RECV_FLAG = ' ' " + "\n");
            strSqlString.Append("                           AND MAT_ID LIKE 'R16%' " + "\n");
            strSqlString.Append("                           AND IN_TIME BETWEEN '" + cdvDate.Value.AddDays(-2).ToString("yyyyMMdd") + "000000' AND '" + sToday + "235959' " + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY MATCODE" + "\n");
            strSqlString.Append("                 UNION ALL" + "\n");
            strSqlString.Append("                SELECT REPLACE(MAT_ID, '-O', '') AS MATCODE" + "\n");
            strSqlString.Append("                     , 0, 0, 0, 0" + "\n");
            strSqlString.Append("                     , SUM(LOT_QTY) AS WIK_WIP " + "\n");
            strSqlString.Append("                  FROM ISTMWIKWIP@RPTTOMES" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == sToday)
            {
                strSqlString.Append("                   AND CUTOFF_DT = '" + sToday + "' || TO_CHAR(SYSDATE, 'HH24')" + "\n");
            }
            else
            {
                strSqlString.Append("                   AND CUTOFF_DT = '" + sToday + "22'" + "\n");
            }
                        
            strSqlString.Append("                 GROUP BY REPLACE(MAT_ID, '-O', '')" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY MATCODE" + "\n");
            strSqlString.Append("       ) WMS" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID AS MATCODE" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + sYesterday + "', S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1, 0)) AS Y_SMT_OUT" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + sToday + "', S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1, 0)) AS T_SMT_OUT" + "\n");
            strSqlString.Append("          FROM RSUMWIPMOV" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND WORK_DATE IN ('" + sYesterday + "', '" + sToday + "')" + "\n");
            strSqlString.Append("           AND OPER = 'M0330'" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) S_OUT" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND MAT.MATCODE = DAT.MATCODE(+)" + "\n");
            strSqlString.Append("   AND MAT.MATCODE = RAS.MATCODE(+)" + "\n");
            strSqlString.Append("   AND MAT.MATCODE = WMS.MATCODE(+)" + "\n");
            strSqlString.Append("   AND MAT.MATCODE = S_OUT.MATCODE(+)" + "\n");
            strSqlString.Append("   AND NVL(DAT.WIP_TTL,0) + NVL(WMS.SMT,0) + NVL(DAT.DA_CAPA,0) + NVL(WMS.INV_QTY,0) + NVL(WMS.SMT_WAIT,0) + " + "\n");
            strSqlString.Append("       NVL(WMS.INV_L_QTY,0) + NVL(WMS.WIK_WIP,0) + NVL(S_OUT.Y_SMT_OUT,0) + NVL(S_OUT.T_SMT_OUT,0) + NVL(DAT.W0_PLAN,0) + " + "\n");
            strSqlString.Append("       NVL(DAT.W0_AO,0) + NVL(DAT.W1_PLAN,0) + NVL(DAT.MON_PLAN,0) + NVL(DAT.MONTH_AO,0) > 0" + "\n");             
            strSqlString.Append(" ORDER BY " + QueryCond1 + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region MakeSqlStringPopupSmtWip
        private string MakeSqlStringPopupSmtWip(string matCode, string sSMT)
        {
            StringBuilder strSqlString = new StringBuilder();
            
            // 쿼리
            strSqlString.Append("SELECT A.MAT_ID, A.LOT_ID, B.MAT_DESC, A.OPER, A.QTY_1, A.LOT_STATUS, A.LOT_CMF_5 " + "\n");
            strSqlString.Append("     , (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'VENDOR' AND FACTORY = A.FACTORY AND KEY_1 = A.LOT_CMF_20) AS VENDOR_NAME " + "\n");
            strSqlString.Append("     , A.LOT_CMF_20 AS VENDOR_CODE " + "\n");
            strSqlString.Append("     , TRUNC(SYSDATE-TO_DATE(A.LAST_TRAN_TIME,'YYYYMMDDHH24MISS')) || '일 ' || " + "\n");
            strSqlString.Append("       TRUNC(MOD((SYSDATE-TO_DATE(A.LAST_TRAN_TIME,'YYYYMMDDHH24MISS')),1)*24)|| '시간 ' || " + "\n");
            strSqlString.Append("       TRUNC(MOD((SYSDATE-TO_DATE(A.LAST_TRAN_TIME,'YYYYMMDDHH24MISS'))*24,1)*60)|| '분 ' " + "\n");
            strSqlString.Append("       AS TRAN_INTERVAL " + "\n");
            strSqlString.Append("     , TRUNC(SYSDATE-TO_DATE(A.CREATE_TIME,'YYYYMMDDHH24MISS')) || '일 ' || " + "\n");
            strSqlString.Append("       TRUNC(MOD((SYSDATE-TO_DATE(A.CREATE_TIME,'YYYYMMDDHH24MISS')),1)*24)|| '시간 ' || " + "\n");
            strSqlString.Append("       TRUNC(MOD((SYSDATE-TO_DATE(A.CREATE_TIME,'YYYYMMDDHH24MISS'))*24,1)*60)|| '분 ' " + "\n");
            strSqlString.Append("       AS CREATE_INTERVAL " + "\n");
            strSqlString.Append("     , A.HOLD_FLAG, A.HOLD_CODE " + "\n");


            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {                
                strSqlString.Append("  FROM RWIPLOTSTS A " + "\n");
                strSqlString.Append("     , MWIPMATDEF B " + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
            }
            else
            {
                strSqlString.Append("  FROM RWIPLOTSTS_BOH A " + "\n");
                strSqlString.Append("     , MWIPMATDEF B " + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND A.CUTOFF_DT = '" + cdvDate.SelectedValue() + "22'" + "\n");
            }

            strSqlString.Append("   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("   AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("   AND A.LOT_TYPE = 'P'" + "\n");
            strSqlString.Append("   AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("   AND A.LOT_CMF_2 = '-'" + "\n");
            strSqlString.Append("   AND A.LOT_CMF_9 != ' '" + "\n");
            strSqlString.Append("   AND A.QTY_1 > 0" + "\n");
            strSqlString.Append("   AND A.OPER <>  'V0000'" + "\n");
            strSqlString.Append("   AND (A.OPER LIKE 'M%' OR A.OPER LIKE 'V%')" + "\n");
            strSqlString.Append("   AND B.DELETE_FLAG = ' '" + "\n");            
            strSqlString.Append("   AND A.MAT_ID LIKE '" + matCode + "%'" + "\n");

            if (sSMT == "SMT_END")
            {
                strSqlString.Append("   AND A.OPER > 'M0330'" + "\n");
            }
            else
            {
                strSqlString.Append("   AND A.OPER <= 'M0330'" + "\n");
            }

            strSqlString.Append(" ORDER BY A.MAT_ID, A.OPER, A.LOT_ID" + "\n");

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
            spdData.ExportExcel();
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            DataTable dtPop = null;            
            string matCode = null;

            if (e.RowHeader || e.ColumnHeader) // 헤더 클릭 이벤트면 SKIP
            {
                return;
            }

            try
            {
                // 자재 코드 담기
                for (int i = 0; i <= 3; i++)
                {
                    if (spdData.ActiveSheet.Columns[i].Label == "Material code")
                        matCode = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();
                }

                if (e.Column == 8 || e.Column == 14 || e.Column == 15)
                {
                    this.Refresh();
                    if (e.Column == 8) // SMT 완료 클릭 시
                    {
                        if (!spdData.Sheets[0].Cells[e.Row, 8].Text.Trim().Equals("") && !spdData.Sheets[0].Cells[e.Row, 8].Text.Trim().Equals("0"))
                        {
                            LoadingPopUp.LoadIngPopUpShow(this);
                            dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringPopupSmtWip(matCode, "SMT_END"));
                        }
                    }
                    else if (e.Column == 15) // SMT 대기 클릭 시
                    {
                        if (!spdData.Sheets[0].Cells[e.Row, 15].Text.Trim().Equals("") && !spdData.Sheets[0].Cells[e.Row, 15].Text.Trim().Equals("0"))
                        {
                            LoadingPopUp.LoadIngPopUpShow(this);
                            dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringPopupSmtWip(matCode, "SMT_WAIT"));
                        }
                    }
                    else if (e.Column == 14) // 외주재고 클릭 시
                    {
                        if (!spdData.Sheets[0].Cells[e.Row, 14].Text.Trim().Equals("") && !spdData.Sheets[0].Cells[e.Row, 14].Text.Trim().Equals("0"))
                        {
                            LoadingPopUp.LoadIngPopUpShow(this);
                            dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringPopupWIKWIP(matCode));
                        }
                    }
                    

                    if (dtPop != null && dtPop.Rows.Count > 0)
                    {
                        LoadingPopUp.LoadingPopUpHidden();

                        if (e.Column == 8 || e.Column == 15) // SMT 완료, SMT 대기 클릭 시
                        {
                            System.Windows.Forms.Form frm = new MAT070505_P1(matCode, dtPop);
                            frm.ShowDialog();
                        }
                        else if (e.Column == 14) // 외주재고 클릭 시
                        {
                            if (!spdData.Sheets[0].Cells[e.Row, 23].Text.Trim().Equals("") && !spdData.Sheets[0].Cells[e.Row, 23].Text.Trim().Equals("0"))
                            {
                                System.Windows.Forms.Form frm = new MAT070504_P2(matCode, dtPop);
                                frm.ShowDialog();
                            }
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
    }
}

