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
    public partial class MAT070505 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        /// <summary>
        /// 클  래  스: MAT070505<br/>
        /// 클래스요약: SMT 진도 관리<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2014-02-13<br/>
        /// 상세  설명: SMT 진도 관리(요청자 : 김권수 대리)<br/>
        /// 변경  내용: <br/>
        /// 2014-02-27-임종우 : 공정투입 대기 자재는 공정창고에 포함되도록 수정 (김권수D 요청)
        /// 2014-02-27-임종우 : AUTO LOSS 로직 추가 (김권수D 요청)
        /// 2015-03-18-임종우 : SMT 완료 부분 세분화 표시 -> SMT 완료, 실적, 401이후 재공 (박우순 요청)
        /// 2015-04-02-임종우 : 2주계획 -> 4주계획 표시 (임태성K 요청)
        /// 2015-04-15-임종우 : SMT 로직 수정 - SMT완료 : M0330 이후재공 + V0000제외한 V%공정, SMT WAIT : M0330 까지의 재공 (박우순 요청)
        /// 2015-07-06-임종우 : 설비 대수 집계 시 DISPATCH 기준 정보가 'Y" 인 설비만 집계 (임태성K 요청)
        /// 2015-11-13-임종우 : 데이터 중복 오류 수정
        /// 2016-07-12-임종우 : CAPA 효율 GlobalVariable 로 선언하여 변경함.
        /// 2019-02-07-임종우 : 창고 재고 과거 데이터 테이블 변경 ZHMMT111@SAPREAL -> CWMSLOTSTS_BOH
        /// 2019-07-05-임종우 : HMBJ Flow 추가 (박형순대리 요청)
        /// 2020-09-24-임종우 : V2 PCB Code 추가 - G16%
        /// 2020-11-11-임종우 : 제품 자릿수 제한 해제. (김성업과장 요청)
        /// </summary>
        private string[] DateArray = new string[4];
        private string[] DateArray2 = new string[4];
        private string[] weekList = new string[5];


        public MAT070505()
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Material code", "MAT_CODE", "A.MAT_CODE", "MAT.MAT_CODE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Material name", "MAT_CODE_DESC", "A.MAT_CODE_DESC", "MAT.MAT_CODE_DESC", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SAP CODE", "VENDOR_ID", "A.VENDOR_ID", "MAT.VENDOR_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "MAT_CMF_11", "A.MAT_CMF_11", "MAT.MAT_CMF_11", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT_ID", "A.MAT_ID", "MAT.MAT_ID", false);
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
            GetDayArray();
  
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Material code", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Material name", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("SAP CODE", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PKG CODE", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("PRODUCT", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 50);

            spdData.RPT_AddBasicColumn("WW" + weekList[0].Substring(4, 2) , 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("plan", 1, 5, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("SMT completed", 1, 6, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("After A401", 1, 7, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("SHIP", 1, 8, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("SMT remaining", 1, 9, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_MerageHeaderColumnSpan(0, 5, 5);

            spdData.RPT_AddBasicColumn("WW" + weekList[1].Substring(4, 2), 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("SMT cumulative remaining volume", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("WW" + weekList[2].Substring(4, 2), 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("SMT cumulative remaining volume", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("SMT Performance", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("the day", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("the day before", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_MerageHeaderColumnSpan(0, 12, 2);

            spdData.RPT_AddBasicColumn("SMT a daily goal", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("DA", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("CAPA", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("WIP", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_MerageHeaderColumnSpan(0, 15, 2);

            spdData.RPT_AddBasicColumn("Shortage of SMT in comparison to DA WIP", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("PCB exhaustion estimated time", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("the day work Priority", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("Inventory status", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("TTL", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("SMT complete", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("SMT wait", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("outsourcing", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Operation warehouse", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("warehouse", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_MerageHeaderColumnSpan(0, 20, 6);

            spdData.RPT_AddBasicColumn("SMT Guide PLAN", 0, 26, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn(DateArray[0].ToString(), 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn(DateArray[1].ToString(), 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn(DateArray[2].ToString(), 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn(DateArray[3].ToString(), 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_MerageHeaderColumnSpan(0, 26, 4);
                        
            spdData.RPT_AddBasicColumn("Remaining Order", 0, 30, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("WW" + weekList[1].Substring(4, 2), 0, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("WW" + weekList[2].Substring(4, 2), 0, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("WW" + weekList[3].Substring(4, 2), 0, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("WW" + weekList[4].Substring(4, 2), 0, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

            spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 14, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 17, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 18, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 19, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 30, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 31, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 32, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 33, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 34, 2);

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
        }

        private void GetDayArray()
        {
            StringBuilder strSqlString = new StringBuilder();
            DataTable dt = null;

            DateTime Now = DateTime.Now;

            Now = cdvDate.Value;

            for (int i = 0; i <= 3; i++)
            {
                DateArray[i] = Now.ToString("MM-dd") + "(" + Now.ToLongDateString().Substring(Now.ToLongDateString().Length - 3, 1) + ")";
                DateArray2[i] = Now.ToString("yyyyMMdd");
                Now = Now.AddDays(1);
            }

            strSqlString.Append("SELECT PLAN_YEAR || LPAD(PLAN_WEEK,2,0) AS WEEK" + "\n");
            strSqlString.Append("  FROM MWIPCALDEF" + "\n");
            strSqlString.Append(" WHERE CALENDAR_ID = 'OTD'" + "\n");
            strSqlString.Append("   AND SYS_DATE IN ('" + cdvDate.Value.ToString("yyyyMMdd") + "', '" + cdvDate.Value.AddDays(7).ToString("yyyyMMdd") + "', '" + cdvDate.Value.AddDays(14).ToString("yyyyMMdd") + "', '" + cdvDate.Value.AddDays(21).ToString("yyyyMMdd") + "', '" + cdvDate.Value.AddDays(28).ToString("yyyyMMdd") + "')" + "\n");
            strSqlString.Append(" ORDER BY PLAN_YEAR || LPAD(PLAN_WEEK,2,0)" + "\n");
            

            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());

            for (int i = 0; i <= 4; i++)
            {
                weekList[i] = dt.Rows[i][0].ToString();
            }
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
                spdData.DataSource = dt;

                spdData.RPT_AutoFit(false);

                //  1. column header
                for (int i = 0; i <= 4; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LemonChiffon;
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
            string sStartDay = FindWeek.StartDay_ThisWeek;
            string sEndDay = FindWeek.EndDay_ThisWeek;
            string sYesterday = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");

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
            strSqlString.Append("     , ROUND(SUM(PLN_W1) / " + strKpcs + ", 0) AS PLN_W1" + "\n");
            //strSqlString.Append("     , ROUND(SUM(SMT_END) / " + strKpcs + ", 0) AS SMT_END" + "\n");
            strSqlString.Append("     , ROUND(SUM(SMT) / " + strKpcs + ", 0) AS SMT_END" + "\n");
            strSqlString.Append("     , ROUND(SUM(AO_QTY) / " + strKpcs + ", 0) AS AO_QTY" + "\n");
            strSqlString.Append("     , ROUND(SUM(AFTER_DA1) / " + strKpcs + ", 0) AS AFTER_DA1" + "\n");
            strSqlString.Append("     , ROUND(SUM(SMT_DEF) / " + strKpcs + ", 0) AS SMT_DEF" + "\n");
            strSqlString.Append("     , ROUND(SUM(SMT_DEF2) / " + strKpcs + ", 0) AS SMT_DEF2" + "\n");
            strSqlString.Append("     , ROUND(SUM(SMT_DEF3) / " + strKpcs + ", 0) AS SMT_DEF3" + "\n");            
            strSqlString.Append("     , ROUND(SUM(SMT_OUT_TODAY) / " + strKpcs + ", 0) AS SMT_OUT_TODAY" + "\n");
            strSqlString.Append("     , ROUND(SUM(SMT_OUT_YESTERDAY) / " + strKpcs + ", 0) AS SMT_OUT_YESTERDAY" + "\n");
            strSqlString.Append("     , ROUND(SUM(SMT_TARGET) / " + strKpcs + ", 0) AS SMT_TARGET" + "\n");
            strSqlString.Append("     , ROUND(SUM(DA1_CAPA) / " + strKpcs + ", 0) AS DA1_CAPA" + "\n");
            strSqlString.Append("     , ROUND(SUM(BEFORE_DA1) / " + strKpcs + ", 0) AS BEFORE_DA1" + "\n");
            strSqlString.Append("     , ROUND(SUM(DA_SMT) / " + strKpcs + ", 0) AS DA_SMT" + "\n");
            strSqlString.Append("     , ROUND(SUM(PCB_SCH) / " + strKpcs + ", 0) AS PCB_SCH" + "\n");
            strSqlString.Append("     , CASE WHEN SUM(PCB_SCH) = 0 THEN 0" + "\n");
            strSqlString.Append("            ELSE ROW_NUMBER() OVER(ORDER BY DECODE(SUM(PCB_SCH),0, 999, SUM(PCB_SCH)))" + "\n");
            strSqlString.Append("       END AS RNK" + "\n");
            strSqlString.Append("     , ROUND(SUM(TTL_WIP) / " + strKpcs + ", 0) AS TTL_WIP" + "\n");
            strSqlString.Append("     , ROUND(SUM(SMT) / " + strKpcs + ", 0) AS SMT" + "\n");
            strSqlString.Append("     , ROUND(SUM(SMT_WAIT) / " + strKpcs + ", 0) AS SMT_WAIT" + "\n");
            strSqlString.Append("     , ROUND(SUM(WIK_WIP) / " + strKpcs + ", 0) AS WIK_WIP" + "\n");
            strSqlString.Append("     , ROUND(SUM(INV_L_QTY) / " + strKpcs + ", 0) AS INV_L_QTY" + "\n");
            strSqlString.Append("     , ROUND(SUM(INV_QTY) / " + strKpcs + ", 0) AS INV_QTY" + "\n");
            strSqlString.Append("     , ROUND(CASE WHEN SUM(SMT_TARGET) > SUM(DA1_CAPA) THEN (CASE WHEN SUM(TTL_WAIT) > SUM(SMT_TARGET) THEN SUM(SMT_TARGET) ELSE SUM(TTL_WAIT) END)" + "\n");
            strSqlString.Append("                  ELSE (CASE WHEN SUM(TTL_WAIT) > SUM(DA1_CAPA) THEN SUM(DA1_CAPA) ELSE SUM(TTL_WAIT) END)" + "\n");
            strSqlString.Append("       END / " + strKpcs + ", 0) AS GUIDE_D1" + "\n");
            strSqlString.Append("     , ROUND(CASE WHEN SUM(SMT_TARGET) > SUM(DA1_CAPA) THEN (CASE WHEN SUM(TTL_WAIT) > (SUM(SMT_TARGET) * 2) THEN SUM(SMT_TARGET) " + "\n");
            strSqlString.Append("                                                                  WHEN SUM(TTL_WAIT) - SUM(SMT_TARGET) <= 0 THEN 0" + "\n");
            strSqlString.Append("                                                                  ELSE SUM(TTL_WAIT) - SUM(SMT_TARGET) END)" + "\n");
            strSqlString.Append("                  ELSE (CASE WHEN SUM(TTL_WAIT) > (SUM(DA1_CAPA) * 2) THEN SUM(DA1_CAPA) " + "\n");
            strSqlString.Append("                             WHEN SUM(TTL_WAIT) - SUM(DA1_CAPA) <= 0 THEN 0" + "\n");
            strSqlString.Append("                             ELSE SUM(TTL_WAIT) - SUM(DA1_CAPA) END)" + "\n");
            strSqlString.Append("       END / " + strKpcs + ", 0) AS GUIDE_D2" + "\n");
            strSqlString.Append("     , ROUND(CASE WHEN SUM(SMT_TARGET) > SUM(DA1_CAPA) THEN (CASE WHEN SUM(TTL_WAIT) > (SUM(SMT_TARGET) * 3) THEN SUM(SMT_TARGET) " + "\n");
            strSqlString.Append("                                                                  WHEN SUM(TTL_WAIT) - (SUM(SMT_TARGET) * 2) <= 0 THEN 0" + "\n");
            strSqlString.Append("                                                                  ELSE SUM(TTL_WAIT) - (SUM(SMT_TARGET) * 2) END)" + "\n");
            strSqlString.Append("                  ELSE (CASE WHEN SUM(TTL_WAIT) > (SUM(DA1_CAPA) * 3) THEN SUM(DA1_CAPA) " + "\n");
            strSqlString.Append("                             WHEN SUM(TTL_WAIT) - (SUM(DA1_CAPA) * 2) <= 0 THEN 0" + "\n");
            strSqlString.Append("                             ELSE SUM(TTL_WAIT) - (SUM(DA1_CAPA) * 2) END)" + "\n");
            strSqlString.Append("       END / " + strKpcs + ", 0) AS GUIDE_D3" + "\n");
            strSqlString.Append("     , ROUND(CASE WHEN SUM(SMT_TARGET) > SUM(DA1_CAPA) THEN (CASE WHEN SUM(TTL_WAIT) > (SUM(SMT_TARGET) * 4) THEN SUM(SMT_TARGET) " + "\n");
            strSqlString.Append("                                                                  WHEN SUM(TTL_WAIT) - (SUM(SMT_TARGET) * 3) <= 0 THEN 0" + "\n");
            strSqlString.Append("                                                                  ELSE SUM(TTL_WAIT) - (SUM(SMT_TARGET) * 3) END)" + "\n");
            strSqlString.Append("                  ELSE (CASE WHEN SUM(TTL_WAIT) > (SUM(DA1_CAPA) * 4) THEN SUM(DA1_CAPA) " + "\n");
            strSqlString.Append("                             WHEN SUM(TTL_WAIT) - (SUM(DA1_CAPA) * 3) <= 0 THEN 0" + "\n");
            strSqlString.Append("                             ELSE SUM(TTL_WAIT) - (SUM(DA1_CAPA) * 3) END)" + "\n");
            strSqlString.Append("       END / " + strKpcs + ", 0) AS GUIDE_D4" + "\n");
            strSqlString.Append("     , ROUND(SUM(ORDER_QTY) / " + strKpcs + ", 0) AS ORDER_QTY" + "\n");
            strSqlString.Append("     , ROUND(SUM(PLN_W2) / " + strKpcs + ", 0) AS PLN_W2" + "\n");
            strSqlString.Append("     , ROUND(SUM(PLN_W3) / " + strKpcs + ", 0) AS PLN_W3" + "\n");
            strSqlString.Append("     , ROUND(SUM(PLN_W4) / " + strKpcs + ", 0) AS PLN_W4" + "\n");
            strSqlString.Append("     , ROUND(SUM(PLN_W5) / " + strKpcs + ", 0) AS PLN_W5" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT " + QueryCond3 + "\n");            
            strSqlString.Append("             , SUM(NVL(MAT.PLN_W1,0)) AS PLN_W1" + "\n");
            strSqlString.Append("             , SUM(NVL(MAT.AO_QTY,0) + NVL(MAT.AFTER_DA1,0) + NVL(WIP.SMT,0)) AS SMT_END" + "\n");
            strSqlString.Append("             , SUM(NVL(MAT.PLN_W1,0) - (NVL(MAT.AO_QTY,0) + NVL(MAT.AFTER_DA1,0) + NVL(WIP.SMT,0))) AS SMT_DEF" + "\n");
            strSqlString.Append("             , SUM((NVL(MAT.PLN_W1,0) + NVL(MAT.PLN_W2,0)) - (NVL(MAT.AO_QTY,0) + NVL(MAT.AFTER_DA1,0) + NVL(WIP.SMT,0))) AS SMT_DEF2" + "\n");
            strSqlString.Append("             , SUM((NVL(MAT.PLN_W1,0) + NVL(MAT.PLN_W2,0) + NVL(MAT.PLN_W3,0)) - (NVL(MAT.AO_QTY,0) + NVL(MAT.AFTER_DA1,0) + NVL(WIP.SMT,0))) AS SMT_DEF3" + "\n");
            strSqlString.Append("             , SUM(NVL(MOV.SMT_OUT_YESTERDAY,0)) AS SMT_OUT_YESTERDAY" + "\n");
            strSqlString.Append("             , SUM(NVL(MOV.SMT_OUT_TODAY,0)) AS SMT_OUT_TODAY" + "\n");
            strSqlString.Append("             , ROUND(CASE TO_CHAR(TO_DATE('" + sToday + "', 'YYYYMMDD'), 'DY') WHEN '월' THEN SUM(NVL(MAT.PLN_W1,0) - (NVL(MAT.AO_QTY,0) + NVL(MAT.AFTER_DA1,0) + NVL(WIP.SMT,0)))" + "\n");
            strSqlString.Append("                                                                         WHEN '화' THEN SUM((NVL(MAT.PLN_W1,0) + NVL(MAT.PLN_W2,0)) - (NVL(MAT.AO_QTY,0) + NVL(MAT.AFTER_DA1,0) + NVL(WIP.SMT,0))) / 7" + "\n");
            strSqlString.Append("                                                                         WHEN '수' THEN SUM((NVL(MAT.PLN_W1,0) + NVL(MAT.PLN_W2,0)) - (NVL(MAT.AO_QTY,0) + NVL(MAT.AFTER_DA1,0) + NVL(WIP.SMT,0))) / 6" + "\n");
            strSqlString.Append("                                                                         WHEN '목' THEN SUM((NVL(MAT.PLN_W1,0) + NVL(MAT.PLN_W2,0)) - (NVL(MAT.AO_QTY,0) + NVL(MAT.AFTER_DA1,0) + NVL(WIP.SMT,0))) / 5" + "\n");
            strSqlString.Append("                                                                         WHEN '금' THEN SUM((NVL(MAT.PLN_W1,0) + NVL(MAT.PLN_W2,0)) - (NVL(MAT.AO_QTY,0) + NVL(MAT.AFTER_DA1,0) + NVL(WIP.SMT,0))) / 4" + "\n");
            strSqlString.Append("                                                                         WHEN '토' THEN SUM(NVL(MAT.PLN_W1,0) - (NVL(MAT.AO_QTY,0) + NVL(MAT.AFTER_DA1,0) + NVL(WIP.SMT,0))) / 3" + "\n");
            strSqlString.Append("                                                                         ELSE SUM(NVL(MAT.PLN_W1,0) - (NVL(MAT.AO_QTY,0) + NVL(MAT.AFTER_DA1,0) + NVL(WIP.SMT,0))) / 2" + "\n");
            strSqlString.Append("               END, 0) AS SMT_TARGET " + "\n");
            strSqlString.Append("             , SUM(NVL(MAT.DA1_CAPA,0)) AS DA1_CAPA" + "\n");
            strSqlString.Append("             , SUM(NVL(MAT.BEFORE_DA1,0)) AS BEFORE_DA1" + "\n");
            strSqlString.Append("             , SUM(NVL(WIP.SMT,0) - NVL(MAT.BEFORE_DA1,0)) AS DA_SMT" + "\n");
            strSqlString.Append("             , CASE WHEN SUM(NVL(MAT.DA1_CAPA,0)) = 0 AND SUM(NVL(MAT.BEFORE_DA1,0)) > 0 THEN 72" + "\n");
            strSqlString.Append("                    WHEN SUM(NVL(MAT.DA1_CAPA,0)) = 0 THEN 0" + "\n");
            strSqlString.Append("                    ELSE ROUND(SUM(NVL(WIP.SMT,0)) / SUM(NVL(MAT.DA1_CAPA,0)) * 24, 2)" + "\n");
            strSqlString.Append("               END AS PCB_SCH" + "\n");
            strSqlString.Append("             , SUM(NVL(WIP.SMT,0) + NVL(WIP.SMT_WAIT,0) + NVL(WIK.WIK_WIP,0) + NVL(WMS.INV_L_QTY,0) + NVL(WMS.INV_QTY,0)) AS TTL_WIP" + "\n");
            strSqlString.Append("             , SUM(NVL(WIP.SMT_WAIT,0) + NVL(WMS.INV_L_QTY,0) + NVL(WMS.INV_QTY,0)) AS TTL_WAIT" + "\n");            
            strSqlString.Append("             , SUM(NVL(WIP.SMT_WAIT,0)) AS SMT_WAIT" + "\n");
            strSqlString.Append("             , SUM(NVL(WIK.WIK_WIP,0)) AS WIK_WIP" + "\n");
            strSqlString.Append("             , SUM(NVL(WMS.INV_L_QTY,0)) AS INV_L_QTY" + "\n");
            strSqlString.Append("             , SUM(NVL(WMS.INV_QTY,0)) AS INV_QTY" + "\n");
            strSqlString.Append("             , SUM(NVL(ORD.ORDER_QTY,0)) AS ORDER_QTY" + "\n");
            strSqlString.Append("             , SUM(NVL(MAT.PLN_W2,0)) AS PLN_W2" + "\n");
            strSqlString.Append("             , SUM(NVL(MAT.PLN_W3,0)) AS PLN_W3" + "\n");
            strSqlString.Append("             , SUM(NVL(MAT.PLN_W4,0)) AS PLN_W4" + "\n");
            strSqlString.Append("             , SUM(NVL(MAT.PLN_W5,0)) AS PLN_W5" + "\n");
            strSqlString.Append("             , SUM(NVL(WIP.SMT,0)) AS SMT" + "\n");
            strSqlString.Append("             , SUM(NVL(MAT.AO_QTY,0)) AS AO_QTY" + "\n");
            strSqlString.Append("             , SUM(NVL(MAT.AFTER_DA1,0)) AS AFTER_DA1" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT " + QueryCond2 + "\n");
            strSqlString.Append("                     , SUM(NVL(B.PLN_W1,0)) AS PLN_W1" + "\n");
            strSqlString.Append("                     , SUM(NVL(B.PLN_W2,0)) AS PLN_W2" + "\n");
            strSqlString.Append("                     , SUM(NVL(B.PLN_W3,0)) AS PLN_W3" + "\n");
            strSqlString.Append("                     , SUM(NVL(B.PLN_W4,0)) AS PLN_W4" + "\n");
            strSqlString.Append("                     , SUM(NVL(B.PLN_W5,0)) AS PLN_W5" + "\n");
            strSqlString.Append("                     , SUM(NVL(C.AO_QTY,0)) AS AO_QTY" + "\n");
            strSqlString.Append("                     , SUM(NVL(D.AFTER_DA1,0)) AS AFTER_DA1" + "\n");
            strSqlString.Append("                     , SUM(NVL(D.BEFORE_DA1,0)) AS BEFORE_DA1" + "\n");
            strSqlString.Append("                     , SUM(NVL(E.DA1_CAPA,0)) AS DA1_CAPA " + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT DISTINCT MAT_CODE, MAT_CODE_DESC, B.VENDOR_ID, B.MAT_CMF_11, B.MAT_ID" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT DISTINCT A.MAT_ID AS MAT_CODE, A.MAT_DESC AS MAT_CODE_DESC, B.PARTNUMBER" + "\n");
            strSqlString.Append("                                  FROM MWIPMATDEF A" + "\n");
            strSqlString.Append("                                     , CWIPBOMDEF B" + "\n");
            strSqlString.Append("                                 WHERE 1=1" + "\n");
            strSqlString.Append("                                   AND A.MAT_ID = B.MATCODE" + "\n");
            strSqlString.Append("                                   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                                   AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                   AND A.FIRST_FLOW IN ('HMAI','HMAJ','HMAR','HMBF', 'HMBJ')" + "\n"); //2014-09-11 장한별'HMAR'추가
            strSqlString.Append("                                   AND B.RESV_FLAG_1 = 'Y'" + "\n");
            strSqlString.Append("                                   AND B.RESV_FIELD_2 = 'PB'" + "\n");
            //strSqlString.Append("                                   AND LENGTH(A.MAT_ID) = 10" + "\n");            
            strSqlString.Append("                               ) A" + "\n");
            strSqlString.Append("                             , MWIPMATDEF B" + "\n");
            strSqlString.Append("                         WHERE A.PARTNUMBER = B.MAT_ID" + "\n");
            strSqlString.Append("                           AND B.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                           AND B.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND REGEXP_LIKE(B.MAT_GRP_5, '-|1st|Merge|Middle*')" + "\n");

            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                strSqlString.Append("                           AND B.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            if (txtMatCode.Text.Trim() != "%" && txtMatCode.Text.Trim() != "")
                strSqlString.Append("                           AND A.MAT_CODE LIKE '" + txtMatCode.Text + "'" + "\n");

            //상세 조회에 따른 SQL문 생성
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
            
            strSqlString.Append("                       ) A" + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID" + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + weekList[0] + "', WW_QTY, 0)) AS PLN_W1" + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + weekList[1] + "', WW_QTY, 0)) AS PLN_W2" + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + weekList[2] + "', WW_QTY, 0)) AS PLN_W3" + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + weekList[3] + "', WW_QTY, 0)) AS PLN_W4" + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + weekList[4] + "', WW_QTY, 0)) AS PLN_W5" + "\n");
            strSqlString.Append("                          FROM RWIPPLNWEK" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                           AND GUBUN = '3'" + "\n");
            strSqlString.Append("                           AND PLAN_WEEK BETWEEN '" + weekList[0] + "' AND '" + weekList[4] + "'" + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("                       ) B" + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID " + "\n");
            strSqlString.Append("                             , SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) AS AO_QTY" + "\n");
            strSqlString.Append("                          FROM RSUMFACMOV" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND CM_KEY_1 = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                           AND WORK_DATE BETWEEN '" + sStartDay + "' AND '" + sEndDay + "'" + "\n");
            strSqlString.Append("                           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                           AND FACTORY <> 'RETURN'" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                           AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("                       ) C" + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER > 'A0401' THEN QTY_1 ELSE 0 END) AS AFTER_DA1" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1)" + "\n");
            strSqlString.Append("                                        WHEN OPER <= 'A0401' THEN QTY_1" + "\n");
            strSqlString.Append("                                        ELSE 0 " + "\n");
            strSqlString.Append("                                   END) AS BEFORE_DA1 " + "\n");
            strSqlString.Append("                          FROM ( " + "\n");
            strSqlString.Append("                                SELECT MAT_ID, OPER, QTY_1 " + "\n");
            strSqlString.Append("                                     , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') AND KEY_1 = A.MAT_ID) AS COMP_CNT " + "\n");

            // 금일조회 기준
            if (DateTime.Now.ToString("yyyyMMdd") == sToday)
            {                
                strSqlString.Append("                                  FROM RWIPLOTSTS A" + "\n");
                strSqlString.Append("                                 WHERE 1=1   " + "\n");
            }
            else// 금일이 아니면 스냅샷 떠놓은 테이블에서 가져옴.
            {
                strSqlString.Append("                                  FROM RWIPLOTSTS_BOH A" + "\n");
                strSqlString.Append("                                 WHERE CUTOFF_DT = '" + sToday + "22' " + "\n");                
            }

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                                   AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                                   AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                                   AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                                   AND OPER <> 'A0000'" + "\n");
            strSqlString.Append("                               ) " + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("                       ) D" + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT RES.MAT_ID" + "\n");
            strSqlString.Append("                             , SUM(TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_da + " * RES.RAS_CNT, 0))) AS DA1_CAPA" + "\n");
            strSqlString.Append("                          FROM ( " + "\n");
            strSqlString.Append("                                SELECT FACTORY, RES_STS_2 AS MAT_ID, RES_STS_8 AS OPER                      " + "\n");
            strSqlString.Append("                                     , RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, COUNT(RES_ID) AS RAS_CNT " + "\n");
                        
            if (DateTime.Now.ToString("yyyyMMdd") == sToday)
            {
                strSqlString.Append("                                  FROM MRASRESDEF " + "\n");
                strSqlString.Append("                                 WHERE 1 = 1  " + "\n");
            }
            else
            {
                strSqlString.Append("                                  FROM MRASRESDEF_BOH " + "\n");
                strSqlString.Append("                                 WHERE 1 = 1  " + "\n");
                strSqlString.Append("                                   AND CUTOFF_DT = '" + sToday + "'" + "\n");
            }

            strSqlString.Append("                                   AND FACTORY  = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                                   AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("                                   AND RES_CMF_7 = 'Y' " + "\n");
            strSqlString.Append("                                   AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                                   AND RES_STS_8 IN ('A0333', 'A0400', 'A0401')" + "\n");
            strSqlString.Append("                                 GROUP BY FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7 " + "\n");
            strSqlString.Append("                               ) RES " + "\n");
            strSqlString.Append("                             , CRASUPHDEF UPH " + "\n");
            strSqlString.Append("                         WHERE 1 = 1 " + "\n");
            strSqlString.Append("                           AND RES.FACTORY = UPH.FACTORY(+) " + "\n");
            strSqlString.Append("                           AND RES.OPER = UPH.OPER(+) " + "\n");
            strSqlString.Append("                           AND RES.RES_MODEL = UPH.RES_MODEL(+) " + "\n");
            strSqlString.Append("                           AND RES.UPEH_GRP = UPH.UPEH_GRP(+) " + "\n");
            strSqlString.Append("                           AND RES.MAT_ID = UPH.MAT_ID(+)" + "\n");
            strSqlString.Append("                         GROUP BY RES.MAT_ID " + "\n");
            strSqlString.Append("                       ) E" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = C.MAT_ID(+)" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = D.MAT_ID(+)" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = E.MAT_ID(+)" + "\n");
            strSqlString.Append("                 GROUP BY " + QueryCond2 + "\n");
            strSqlString.Append("               ) MAT" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT REPLACE(MAT_ID, '-O', '') AS MAT_CODE" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN GUBUN = 'SMT' THEN QTY_1 ELSE 0 END) SMT" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN GUBUN = 'SMT_WAIT' THEN QTY_1 ELSE 0 END) SMT_WAIT" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID, OPER" + "\n");
            strSqlString.Append("                             , CASE WHEN OPER <= 'M0330' THEN 'SMT_WAIT' ELSE 'SMT' END AS GUBUN" + "\n");
            strSqlString.Append("                             , QTY_1" + "\n");

            // 금일조회 기준
            if (DateTime.Now.ToString("yyyyMMdd") == sToday)
            {
                strSqlString.Append("                          FROM RWIPLOTSTS" + "\n");
                strSqlString.Append("                         WHERE 1=1   " + "\n");
            }
            else// 금일이 아니면 스냅샷 떠놓은 테이블에서 가져옴.
            {
                strSqlString.Append("                          FROM RWIPLOTSTS_BOH" + "\n");
                strSqlString.Append("                         WHERE CUTOFF_DT = '" + sToday + "22' " + "\n");
            }

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                           AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                           AND LOT_TYPE = 'P'" + "\n");
            strSqlString.Append("                           AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND LOT_CMF_2 = '-'" + "\n");
            strSqlString.Append("                           AND LOT_CMF_9 != ' '" + "\n");
            strSqlString.Append("                           AND QTY_1 > 0" + "\n");
            strSqlString.Append("                           AND OPER <>  'V0000'" + "\n");
            strSqlString.Append("                           AND (OPER LIKE 'M%' OR OPER LIKE 'V%')" + "\n");
            //strSqlString.Append("                           AND OPER NOT IN  ('00001', '00002', 'V0000', 'A2300', 'AZ010')" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY REPLACE(MAT_ID, '-O', '')" + "\n");
            strSqlString.Append("               ) WIP" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_CODE" + "\n");
            strSqlString.Append("                     , SUM(INV_QTY) AS INV_QTY" + "\n");
            strSqlString.Append("                     , SUM(INV_L_QTY) AS INV_L_QTY" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID AS MAT_CODE" + "\n");
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
            strSqlString.Append("                           AND (MAT_ID LIKE 'R16%' OR MAT_ID LIKE 'G16%')" + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                         UNION ALL " + "\n");
            strSqlString.Append("                        SELECT MAT_ID, 0 AS INV_QTY, SUM(QTY_1) AS INV_L_QTY " + "\n");
            strSqlString.Append("                          FROM CWIPMATSLP@RPTTOMES " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND RECV_FLAG = ' ' " + "\n");
            strSqlString.Append("                           AND (MAT_ID LIKE 'R16%' OR MAT_ID LIKE 'G16%') " + "\n");
            strSqlString.Append("                           AND IN_TIME BETWEEN '" + cdvDate.Value.AddDays(-2).ToString("yyyyMMdd") + "000000' AND '" + sToday + "235959' " + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY MAT_CODE " + "\n");
            strSqlString.Append("               ) WMS" + "\n");
            strSqlString.Append("             , (          " + "\n");
            strSqlString.Append("                SELECT REPLACE(MAT_ID, '-O', '') AS MAT_CODE" + "\n");
            strSqlString.Append("                     , SUM(LOT_QTY) AS WIK_WIP           " + "\n");
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
            strSqlString.Append("               ) WIK" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT REPLACE(MATCODE, '-O', '') AS MAT_CODE" + "\n");
            strSqlString.Append("                     , SUM(ORDER_QTY) AS ORDER_QTY" + "\n");
            strSqlString.Append("                  FROM RSUMWIPMAT" + "\n");
            strSqlString.Append("                 WHERE MAT_TYPE = 'PB'" + "\n");
            strSqlString.Append("                 GROUP BY REPLACE(MATCODE, '-O', '')  " + "\n");
            strSqlString.Append("               ) ORD" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_ID AS MAT_CODE" + "\n");
            strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + sYesterday + "', S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1, 0)) AS SMT_OUT_YESTERDAY" + "\n");
            strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + sToday + "', S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1, 0)) AS SMT_OUT_TODAY" + "\n");
            strSqlString.Append("                  FROM RSUMWIPMOV" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                   AND WORK_DATE IN ('" + sYesterday + "','" + sToday + "')" + "\n");
            strSqlString.Append("                   AND OPER = 'M0330'" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
            strSqlString.Append("               ) MOV" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");            
            strSqlString.Append("           AND MAT.MAT_CODE = WIP.MAT_CODE(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_CODE = WMS.MAT_CODE(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_CODE = WIK.MAT_CODE(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_CODE = ORD.MAT_CODE(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_CODE = MOV.MAT_CODE(+)" + "\n");
            strSqlString.Append("         GROUP BY " + QueryCond3 + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" GROUP BY " + QueryCond1 + "\n");
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
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            DataTable dtPop = null;
            DateTime dtp = new DateTime();
            string matCode = null;            

            if (e.RowHeader || e.ColumnHeader) // 헤더 클릭 이벤트면 SKIP
            {
                return;
            }
            
            try
            {
                // 자재 코드 담기
                for (int i = 0; i <= 4; i++)
                {
                    if (spdData.ActiveSheet.Columns[i].Label == "Material code")
                        matCode = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();
                }

                if (e.Column == 21 || e.Column == 22 || e.Column == 23 || e.Column == 30)
                {                    
                    this.Refresh();
                    if (e.Column == 21) // SMT 완료 클릭 시
                    {
                        LoadingPopUp.LoadIngPopUpShow(this);

                        dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringPopupSmtWip(matCode, "SMT_END"));
                    }
                    else if (e.Column == 22) // SMT 대기 클릭 시
                    {
                        LoadingPopUp.LoadIngPopUpShow(this);

                        dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringPopupSmtWip(matCode, "SMT_WAIT"));
                    }
                    else if (e.Column == 23) // 외주재고 클릭 시
                    {
                        if (!spdData.Sheets[0].Cells[e.Row, 23].Text.Trim().Equals("") && !spdData.Sheets[0].Cells[e.Row, 23].Text.Trim().Equals("0"))
                        {
                            LoadingPopUp.LoadIngPopUpShow(this);
                            dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringPopupWIKWIP(matCode));
                        }
                    }
                    else if (e.Column == 30) // 발주잔량 클릭 시
                    {
                        LoadingPopUp.LoadIngPopUpShow(this);

                        DataTable dtPlanDate = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", "SELECT MAX(PLAN_DATE) AS PLAN_DATE FROM CMATPLNINP@RPTTOMES WHERE MAT_ID = '" + matCode + "'");
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

                        if (e.Column == 21 || e.Column == 22) // SMT 완료, SMT 대기 클릭 시
                        {
                            System.Windows.Forms.Form frm = new MAT070505_P1(matCode, dtPop);
                            frm.ShowDialog();
                        }
                        else if (e.Column == 23) // 외주재고 클릭 시
                        {
                            if (!spdData.Sheets[0].Cells[e.Row, 23].Text.Trim().Equals("") && !spdData.Sheets[0].Cells[e.Row, 23].Text.Trim().Equals("0"))
                            {
                                System.Windows.Forms.Form frm = new MAT070504_P2(matCode, dtPop);
                                frm.ShowDialog();
                            }
                        }
                        else if (e.Column == 30) //발주잔량 클릭 시
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

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {

        }

    }
}

