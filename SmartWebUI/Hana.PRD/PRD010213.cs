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
    public partial class PRD010213 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010213<br/>
        /// 클래스요약: 삼성 COB SOP 관리<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2012-10-30<br/>
        /// 상세  설명: COB 관리<br/>
        /// 변경  내용: <br/>
        /// 2013-09-06-임종우 : 재공 LOT LIST 중 누적 정체 시간에 따른 출하 LOT 선정 POP UP 추가 (최홍범 요청)
        /// 2013-10-23-임종우 : INPUT 계획 D2, D3 추가 (김은석 요청)
        /// 2014-07-02-임종우 : Wafer 장수 표현 시 Net Die * 85% 수율 반영하여 계산 할 것 (김권수D 요청)
        ///                   : 금주 RTF 계획, RTF 잔량 표시 (김권수D 요청)
        ///                   : D1 변경 계획 부분 추가 (김권수D 요청)
        /// 2014-07-03-장한별 : D1 변경 계획 부분 문구수정 , D1잔량 로직 수정 : 기존 D1계획-실적 ->DZ계획 + D1계획 - 실적 (김권수D 요청)
        ///                   : 웨이퍼 장수 수율 85%->95% 변경 (김권수D 요청)
        /// 2015-02-09-임종우 : PIN TYPE 선택시 오류 부분 수정
        /// 2015-02-24-임종우 : AVI 공정 재공 데이터 추가 표시 (조형진 요청)
        /// 2015-05-15-임종우 : Package -> Package2 로 변경 (임태성K 요청)
        /// 2015-07-28-임종우 : OMS 계획 존재하나 제품기준정보가 없는 제품 표시 (김권수D 요청)
        /// 2018-08-29-임종우 : Net Die 정보 추가 (최연희대리 요청)
        /// </summary>
        static string[] DateArray = new string[8];
        static string[] DateArray2 = new string[8];
        GlobalVariable.FindWeek FindWeek = new GlobalVariable.FindWeek();
        GlobalVariable.FindWeek FindWeek_RTF = new GlobalVariable.FindWeek();

        public PRD010213()
        {
            InitializeComponent();
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();

            this.SetFactory(GlobalVariable.gsTestDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsTestDefaultFactory;

            //udcWIPCondition1.Text = "SE";
            //udcWIPCondition1.Enabled = false;
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
            GetDayArray();
            spdData.RPT_ColumnInit();
            spdData2.RPT_ColumnInit();

            FindWeek = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "SE");
            FindWeek_RTF = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "QC");

            try
            {
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LD Count", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 130);
                spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 180);
                spdData.RPT_AddBasicColumn("Cust Device", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 180);

                spdData.RPT_AddBasicColumn("Next week (WW" + FindWeek.NextWeek.Substring(4,2) + ")", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("RTF", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("SOP", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                                
                spdData.RPT_MerageHeaderColumnSpan(0, 11, 2);

                spdData.RPT_AddBasicColumn("금주(WW" + FindWeek.ThisWeek.Substring(4,2) + ")", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("RTF", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("SOP", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("actual", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("RTF remaining", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("COB remaining amount", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_MerageHeaderColumnSpan(0, 13, 5);

                spdData.RPT_AddBasicColumn("CHIP remaining amount", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("RTF", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("SOP", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_MerageHeaderColumnSpan(0, 18, 2);

                spdData.RPT_AddBasicColumn("INPUT", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);                
                spdData.RPT_AddBasicColumn("D0", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("actual", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D1", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D2", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D3", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 20, 6);
                
                spdData.RPT_AddBasicColumn("WIP", 0, 26, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("TOTAL", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("STOCK", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("L/N", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("B/G", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("LABEL_PRINT", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                spdData.RPT_AddBasicColumn("SAW", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("UV", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("AVI", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("PVI", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("HMK3", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("PACKING", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("HMK4", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 26, 12);

                spdData.RPT_AddBasicColumn(DateArray[0].ToString(), 0, 38, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("plan", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("actual", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 38, 3);

                spdData.RPT_AddBasicColumn(DateArray[1].ToString(), 0, 41, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("plan", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(DateArray[2].ToString(), 0, 42, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("plan", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(DateArray[3].ToString(), 0, 43, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("plan", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(DateArray[4].ToString(), 0, 44, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("plan", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(DateArray[5].ToString(), 0, 45, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("plan", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(DateArray[6].ToString(), 0, 46, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("plan", 1, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("D1 change plan", 0, 47, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("plan", 1, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("the day's performance", 1, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D0 + D1 remaining", 1, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 47, 3);

                spdData.RPT_AddBasicColumn("Net Die", 0, 50, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

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
                spdData.RPT_MerageHeaderRowSpan(0, 50, 2);

                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.


                spdData2.RPT_AddBasicColumn("Notice", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 180);
                spdData2.RPT_AddBasicColumn("FACTORY", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData2.RPT_AddBasicColumn("MAT_ID", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 180);
                spdData2.RPT_AddBasicColumn("PLAN_DAY", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData2.RPT_AddBasicColumn("PLAN_QTY", 0, 4, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);                

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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT.MAT_GRP_1", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2", "MAT_GRP_2", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6", "MAT_GRP_6", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_10", "MAT_GRP_10", "MAT.MAT_GRP_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4", "MAT_GRP_4", "MAT.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5", "MAT_GRP_5", "MAT.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7", "MAT_GRP_7", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8", "MAT_GRP_8", "MAT.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10", "MAT_CMF_10", "MAT.MAT_CMF_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID", "MAT_ID", "MAT.MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "MAT.MAT_CMF_7", "MAT_CMF_7", "MAT.MAT_CMF_7", false);
        }
        #endregion


        #region 금일부터 이후 6일의 일자 가져오기
        private void GetDayArray()
        {
            DateTime Now = DateTime.Now;
            Now = cdvDate.Value;            
                        
            for (int i = 0; i < 7; i++)
            {
                DateArray[i] = Now.ToString("MM-dd") + "(" + Now.ToLongDateString().Substring(Now.ToLongDateString().Length - 3, 1) + ")";
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
            string Today;            
            
            Today = cdvDate.Value.ToString("yyyyMMdd");                    

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;            

            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond3);

            if (ckbWafer.Checked == false)
            {
                strSqlString.Append("     , 0 AS RTF_W2" + "\n");
                strSqlString.Append("     , SUM(NVL(TEST_PLAN_W2, 0)) AS SOP_W2" + "\n");
                strSqlString.Append("     , SUM(NVL(PLAN_RTF, 0)) AS RTF_W1 " + "\n");
                strSqlString.Append("     , SUM(NVL(TEST_PLAN_W1, 0) + (NVL(SHP_WEEK, 0) - NVL(SHP_D0, 0))) AS SOP_W1" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP_WEEK, 0)) AS SHP_WEEK" + "\n");
                strSqlString.Append("     , SUM(NVL(PLAN_RTF, 0) - NVL(SHP_WEEK, 0)) AS RTF_DEF" + "\n");
                strSqlString.Append("     , SUM(NVL(TEST_PLAN_W1, 0) - NVL(SHP_D0, 0)) AS SOP_DEF" + "\n");
                strSqlString.Append("     , 0 AS RTF_CHIP" + "\n");
                strSqlString.Append("     , SUM(CASE WHEN NVL(TEST_PLAN_W1, 0) - NVL(SHP_D0, 0) - NVL(TOTAL, 0) < 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE NVL(TEST_PLAN_W1, 0) - NVL(SHP_D0, 0) - NVL(TOTAL, 0) " + "\n");
                strSqlString.Append("       END) AS SOP_CHIP" + "\n");
                strSqlString.Append("     , SUM(NVL(ASSY_PLAN_D0, 0)) AS ASSY_PLAN_D0" + "\n");
                strSqlString.Append("     , SUM(NVL(ISSUE_QTY, 0)) AS ISSUE_QTY" + "\n");
                strSqlString.Append("     , SUM(NVL(ISSUE_QTY, 0) - NVL(ASSY_PLAN_D0, 0)) AS IN_DEF" + "\n");
                strSqlString.Append("     , SUM(NVL(ASSY_PLAN_D1, 0)) AS ASSY_PLAN_D1" + "\n");
                strSqlString.Append("     , SUM(NVL(ASSY_PLAN_D2, 0)) AS ASSY_PLAN_D2" + "\n");
                strSqlString.Append("     , SUM(NVL(ASSY_PLAN_D3, 0)) AS ASSY_PLAN_D3" + "\n");
                strSqlString.Append("     , SUM(NVL(TOTAL, 0)) AS TOTAL" + "\n");
                strSqlString.Append("     , SUM(NVL(STOCK, 0)) AS STOCK" + "\n");
                strSqlString.Append("     , SUM(NVL(LN, 0)) AS LN" + "\n");
                strSqlString.Append("     , SUM(NVL(BG, 0)) AS BG" + "\n");
                strSqlString.Append("     , SUM(NVL(LABEL, 0)) AS LABEL" + "\n");
                strSqlString.Append("     , SUM(NVL(SAW, 0)) AS SAW" + "\n");
                strSqlString.Append("     , SUM(NVL(UV, 0)) AS UV" + "\n");
                strSqlString.Append("     , SUM(NVL(AVI, 0)) AS AVI" + "\n");
                strSqlString.Append("     , SUM(NVL(PVI, 0)) AS PVI" + "\n");
                strSqlString.Append("     , SUM(NVL(HMK3, 0)) AS HMK3" + "\n");
                strSqlString.Append("     , SUM(NVL(PACKING, 0)) AS PACKING" + "\n");
                strSqlString.Append("     , SUM(NVL(HMK4, 0)) AS HMK4" + "\n");
                strSqlString.Append("     , SUM(NVL(TEST_PLAN_D0, 0)) AS TEST_PLAN_D0" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP_D0, 0)) AS SHP_D0" + "\n");
                strSqlString.Append("     , SUM(NVL(TEST_PLAN_D0, 0) - NVL(SHP_D0, 0)) AS TODAY_DEF" + "\n");
                strSqlString.Append("     , SUM(NVL(TEST_PLAN_D1, 0)) AS TEST_PLAN_D1" + "\n");
                strSqlString.Append("     , SUM(NVL(TEST_PLAN_D2, 0)) AS TEST_PLAN_D2" + "\n");
                strSqlString.Append("     , SUM(NVL(TEST_PLAN_D3, 0)) AS TEST_PLAN_D3" + "\n");
                strSqlString.Append("     , SUM(NVL(TEST_PLAN_D4, 0)) AS TEST_PLAN_D4" + "\n");
                strSqlString.Append("     , SUM(NVL(TEST_PLAN_D5, 0)) AS TEST_PLAN_D5" + "\n");
                strSqlString.Append("     , SUM(NVL(TEST_PLAN_D6, 0)) AS TEST_PLAN_D6" + "\n");
                strSqlString.Append("     , SUM(NVL(REV_PLAN, 0)) AS REV_PLAN" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP_D0, 0)) AS SHP_D0" + "\n");
                strSqlString.Append("     , SUM(NVL(TEST_PLAN_D0, 0) + NVL(REV_PLAN, 0) - NVL(SHP_D0, 0)) AS REV_DEF" + "\n");
            }
            else
            {
                strSqlString.Append("     , 0 AS RTF_W2" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(TEST_PLAN_W2,0) / NET_DIE)) AS SOP_W2" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(PLAN_RTF, 0) / NET_DIE)) AS RTF_W1" + "\n");
                strSqlString.Append("     , SUM(CEIL((NVL(TEST_PLAN_W1, 0) + (NVL(SHP_WEEK, 0) - NVL(SHP_D0, 0))) / NET_DIE)) AS SOP_W1" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(SHP_WEEK, 0) / NET_DIE)) AS SHP_WEEK" + "\n");
                strSqlString.Append("     , SUM(CEIL((NVL(PLAN_RTF, 0) - NVL(SHP_WEEK, 0)) / NET_DIE)) AS RTF_DEF" + "\n");
                strSqlString.Append("     , SUM(CEIL((NVL(TEST_PLAN_W1, 0) - NVL(SHP_D0, 0)) / NET_DIE)) AS SOP_DEF" + "\n");
                strSqlString.Append("     , 0 AS RTF_CHIP" + "\n");
                strSqlString.Append("     , SUM(CASE WHEN NVL(TEST_PLAN_W1, 0) - NVL(SHP_D0, 0) - NVL(TOTAL, 0) < 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE CEIL((NVL(TEST_PLAN_W1, 0) - NVL(SHP_D0, 0) - NVL(TOTAL, 0)) / NET_DIE) " + "\n");
                strSqlString.Append("       END) AS SOP_CHIP" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(ASSY_PLAN_D0, 0) / NET_DIE)) AS ASSY_PLAN_D0" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(ISSUE_QTY, 0) / NET_DIE)) AS ISSUE_QTY" + "\n");
                strSqlString.Append("     , SUM(CEIL((NVL(ISSUE_QTY, 0) - NVL(ASSY_PLAN_D0, 0)) / NET_DIE)) AS IN_DEF" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(ASSY_PLAN_D1, 0) / NET_DIE)) AS ASSY_PLAN_D1" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(ASSY_PLAN_D2, 0) / NET_DIE)) AS ASSY_PLAN_D2" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(ASSY_PLAN_D3, 0) / NET_DIE)) AS ASSY_PLAN_D3" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(TOTAL, 0) / NET_DIE)) AS TOTAL" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(STOCK, 0) / NET_DIE)) AS STOCK" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(LN, 0) / NET_DIE)) AS LN" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(BG, 0) / NET_DIE)) AS BG" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(LABEL, 0) / NET_DIE)) AS LABEL" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(SAW, 0) / NET_DIE)) AS SAW" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(UV, 0) / NET_DIE)) AS UV" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(AVI, 0) / NET_DIE)) AS AVI" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(PVI, 0) / NET_DIE)) AS PVI" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(HMK3, 0) / NET_DIE)) AS HMK3" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(PACKING, 0) / NET_DIE)) AS PACKING" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(HMK4, 0) / NET_DIE)) AS HMK4" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(TEST_PLAN_D0, 0) / NET_DIE)) AS TEST_PLAN_D0" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(SHP_D0, 0) / NET_DIE)) AS SHP_D0" + "\n");
                strSqlString.Append("     , SUM(CEIL((NVL(TEST_PLAN_D0, 0) - NVL(SHP_D0, 0)) / NET_DIE)) AS TODAY_DEF" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(TEST_PLAN_D1, 0) / NET_DIE)) AS TEST_PLAN_D1" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(TEST_PLAN_D2, 0) / NET_DIE)) AS TEST_PLAN_D2" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(TEST_PLAN_D3, 0) / NET_DIE)) AS TEST_PLAN_D3" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(TEST_PLAN_D4, 0) / NET_DIE)) AS TEST_PLAN_D4" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(TEST_PLAN_D5, 0) / NET_DIE)) AS TEST_PLAN_D5" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(TEST_PLAN_D6, 0) / NET_DIE)) AS TEST_PLAN_D6" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(REV_PLAN, 0) / NET_DIE)) AS REV_PLAN" + "\n");
                strSqlString.Append("     , SUM(CEIL(NVL(SHP_D0, 0) / NET_DIE)) AS SHP_D0" + "\n");
                strSqlString.Append("     , SUM(CEIL((NVL(TEST_PLAN_D0, 0) + NVL(REV_PLAN, 0) - NVL(SHP_D0, 0)) / NET_DIE)) AS REV_DEF" + "\n");
            }

            strSqlString.Append("     , MAX(MAT.MAT_CMF_13) AS MAT_CMF_13" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT A.*" + "\n");
            strSqlString.Append("             , TO_NUMBER(DECODE(A.MAT_CMF_13,' ', 1, A.MAT_CMF_13)) * 0.95 AS NET_DIE" + "\n");
            strSqlString.Append("          FROM MWIPMATDEF A" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND MAT_ID LIKE '%UW_'" + "\n");
            strSqlString.Append("           AND DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("       ) MAT" + "\n");            
            // 실적
            strSqlString.Append("     , (" + "\n");            
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + Today + "', S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0)) AS SHP_D0" + "\n");
            strSqlString.Append("             , SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS SHP_WEEK" + "\n");
            strSqlString.Append("          FROM RSUMFACMOV " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND CM_KEY_1  = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND CM_KEY_2 = 'PROD' " + "\n");
            strSqlString.Append("           AND CM_KEY_3 LIKE 'P%' " + "\n");
            strSqlString.Append("           AND MAT_ID LIKE '%UW_'" + "\n");
            strSqlString.Append("           AND WORK_DATE BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + Today + "'   " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("       ) SHP" + "\n");
            // ISSUE
            strSqlString.Append("     , (" + "\n");            
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("             , SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1) AS ISSUE_QTY" + "\n");
            strSqlString.Append("          FROM RSUMWIPMOV" + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND WORK_DATE = '" + Today + "'" + "\n");
            strSqlString.Append("           AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND OPER = 'A0000' " + "\n");
            strSqlString.Append("           AND MAT_ID LIKE '%UW_' " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("       ) ISS" + "\n");
            // 계획
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND PLAN_DAY = '" + DateArray2[0].ToString() + "' THEN PLAN_QTY ELSE 0 END) AS ASSY_PLAN_D0" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND PLAN_DAY = '" + DateArray2[1].ToString() + "' THEN PLAN_QTY ELSE 0 END) AS ASSY_PLAN_D1" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND PLAN_DAY = '" + DateArray2[2].ToString() + "' THEN PLAN_QTY ELSE 0 END) AS ASSY_PLAN_D2" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND PLAN_DAY = '" + DateArray2[3].ToString() + "' THEN PLAN_QTY ELSE 0 END) AS ASSY_PLAN_D3" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND PLAN_DAY = '" + DateArray2[0].ToString() + "' THEN PLAN_QTY ELSE 0 END) AS TEST_PLAN_D0" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND PLAN_DAY = '" + DateArray2[1].ToString() + "' THEN PLAN_QTY ELSE 0 END) AS TEST_PLAN_D1" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND PLAN_DAY = '" + DateArray2[2].ToString() + "' THEN PLAN_QTY ELSE 0 END) AS TEST_PLAN_D2" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND PLAN_DAY = '" + DateArray2[3].ToString() + "' THEN PLAN_QTY ELSE 0 END) AS TEST_PLAN_D3" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND PLAN_DAY = '" + DateArray2[4].ToString() + "' THEN PLAN_QTY ELSE 0 END) AS TEST_PLAN_D4" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND PLAN_DAY = '" + DateArray2[5].ToString() + "' THEN PLAN_QTY ELSE 0 END) AS TEST_PLAN_D5" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND PLAN_DAY = '" + DateArray2[6].ToString() + "' THEN PLAN_QTY ELSE 0 END) AS TEST_PLAN_D6     " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND PLAN_DAY BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + FindWeek.EndDay_ThisWeek + "' THEN PLAN_QTY ELSE 0 END) AS TEST_PLAN_W1 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND PLAN_DAY BETWEEN '" + FindWeek.StartDay_NextWeek + "' AND '" + FindWeek.EndDay_NextWeek + "' THEN PLAN_QTY ELSE 0 END) AS TEST_PLAN_W2      " + "\n");

            // 금일조회 기준
            if (DateTime.Now.ToString("yyyyMMdd") == Today)
            {
                strSqlString.Append("          FROM CWIPPLNDAY " + "\n");
                strSqlString.Append("         WHERE 1=1   " + "\n");                
            }
            else// 금일이 아니면 스냅샷 떠놓은 테이블에서 가져옴.
            {
                strSqlString.Append("          FROM CWIPPLNSNP@RPTTOMES " + "\n");
                strSqlString.Append("         WHERE SNAPSHOT_DAY = '" + Today + "'" + "\n");                
            }

            strSqlString.Append("           AND PLAN_DAY BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + FindWeek.EndDay_NextWeek + "'" + "\n");
            strSqlString.Append("           AND IN_OUT_FLAG = 'IN'   " + "\n");
            strSqlString.Append("           AND MAT_ID LIKE '%UW_'" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) PLN" + "\n");
            // 재공
            strSqlString.Append("     , (" + "\n");            
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("             , SUM(QTY_1) TOTAL " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0000', QTY_1, 0)) STOCK" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0020', QTY_1, 0)) LN" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0040', QTY_1, 0)) BG" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0140', QTY_1, 0)) LABEL" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0200', QTY_1, 0)) SAW" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0250', QTY_1, 0)) UV" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0270', QTY_1, 0)) AVI" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A2050', QTY_1, 'A2100', QTY_1, 'A2150', QTY_1, 0)) PVI" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'T0000', QTY_1, 0)) HMK3" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'T1300', QTY_1, 0)) PACKING" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'TZ010', QTY_1, 0)) HMK4 " + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT *" + "\n");

            // 금일조회 기준
            if (DateTime.Now.ToString("yyyyMMdd") == Today)
            {
                strSqlString.Append("                  FROM RWIPLOTSTS" + "\n");
                strSqlString.Append("                 WHERE 1=1   " + "\n");
            }
            else// 금일이 아니면 스냅샷 떠놓은 테이블에서 가져옴.
            {
                strSqlString.Append("                  FROM RWIPLOTSTS_BOH" + "\n");
                strSqlString.Append("                 WHERE CUTOFF_DT = '" + Today + "22' " + "\n");
            }
                                    
            strSqlString.Append("                   AND FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "')" + "\n");
            strSqlString.Append("                   AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                   AND LOT_CMF_5 LIKE 'P%'" + "\n");
            strSqlString.Append("                   AND MAT_ID LIKE '%UW_'" + "\n");
            strSqlString.Append("               )  " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("       ) WIP" + "\n");
            // RTF
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') IN ('6','7','1') THEN 0 " + "\n");
            strSqlString.Append("                        WHEN PLAN_DATE BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + FindWeek_RTF.EndDay_ThisWeek + "' AND WW_RTF <> '-' THEN QTY_1 " + "\n");
            strSqlString.Append("                        ELSE 0 " + "\n");
            strSqlString.Append("                   END) AS PLAN_RTF " + "\n");
            strSqlString.Append("          FROM CPLNSOPDAY@RPTTOMES " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND FACTORY  = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND PLAN_DATE BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + FindWeek.EndDay_ThisWeek + "' " + "\n");
            strSqlString.Append("           AND MAT_ID LIKE '%UW_' " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("       ) RTF" + "\n");
            // REV
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT KEY_2 AS MAT_ID " + "\n");
            strSqlString.Append("             , SUM(DATA_1) AS REV_PLAN " + "\n");
            strSqlString.Append("          FROM MGCMTBLDAT " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND TABLE_NAME = 'H_RPT_D1_REV_PLAN' " + "\n");
            strSqlString.Append("           AND KEY_1 = '" + Today + "' " + "\n");
            strSqlString.Append("           AND KEY_2 LIKE '%UW_' " + "\n");
            strSqlString.Append("         GROUP BY KEY_2 " + "\n");
            strSqlString.Append("       ) REV" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");            
            strSqlString.Append("   AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = ISS.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = RTF.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = REV.MAT_ID(+)" + "\n");

            //상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

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

            strSqlString.Append("   AND MAT.MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");
            strSqlString.AppendFormat(" GROUP BY {0}" + "\n", QueryCond1);            
            strSqlString.Append(" HAVING SUM(NVL(TEST_PLAN_W2,0) + NVL(TEST_PLAN_W1,0) + NVL(SHP_WEEK,0) + NVL(SHP_D0,0) + NVL(TOTAL,0)" + "\n");
            strSqlString.Append("          + NVL(ASSY_PLAN_D0, 0) + NVL(ASSY_PLAN_D1, 0) + NVL(ASSY_PLAN_D2, 0) + NVL(ASSY_PLAN_D3, 0) + NVL(ISSUE_QTY, 0) + NVL(TEST_PLAN_D0, 0)" + "\n");
            strSqlString.Append("          + NVL(TEST_PLAN_D1, 0) + NVL(TEST_PLAN_D2, 0) + NVL(TEST_PLAN_D3, 0) + NVL(TEST_PLAN_D4, 0)" + "\n");
            strSqlString.Append("          + NVL(TEST_PLAN_D5, 0) + NVL(TEST_PLAN_D6, 0)) > 0" + "\n");
            strSqlString.AppendFormat(" ORDER BY {0}" + "\n", QueryCond1);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }               

        #endregion

        #region popup 창 쿼리
        private string MakeSqlDetail(string sMatId)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT MAT_ID, OPER, LOT_ID, QTY_1" + "\n");
            strSqlString.Append("     , CASE WHEN RESV_FIELD_1 = ' ' THEN '0일 0시간 0분'" + "\n");
            strSqlString.Append("            ELSE (TRUNC(SYSDATE-TO_DATE(RESV_FIELD_1,'YYYYMMDDHH24MISS')) || '일 ' ||" + "\n");
            strSqlString.Append("            TRUNC(MOD((SYSDATE-TO_DATE(RESV_FIELD_1,'YYYYMMDDHH24MISS')),1)*24)|| '시간 ' ||" + "\n");
            strSqlString.Append("            TRUNC(MOD((SYSDATE-TO_DATE(RESV_FIELD_1,'YYYYMMDDHH24MISS'))*24,1)*60)|| '분 ' )" + "\n");
            strSqlString.Append("       END AS TIME_INTERVAL" + "\n");
            strSqlString.Append("     , SUM(QTY_1) OVER(PARTITION BY MAT_ID ORDER BY OPER DESC, RESV_FIELD_1, QTY_1 ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS SUM_QTY" + "\n");
            strSqlString.Append("  FROM RWIPLOTSTS" + "\n");
            strSqlString.Append(" WHERE FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "')" + "\n");
            strSqlString.Append("   AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("   AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("   AND LOT_CMF_5 LIKE 'P%'" + "\n");
            strSqlString.Append("   AND HOLD_FLAG = ' '" + "\n");
            strSqlString.Append("   AND MAT_ID = '" + sMatId + "'" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion

        #region 제품기준정보 미등록 조회 쿼리
        private string MakeSqlProduct()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT '제품기준정보 미등록 제품' AS NOTICE, A.FACTORY, A.MAT_ID, A.PLAN_DAY, A.PLAN_QTY" + "\n");
            strSqlString.Append("  FROM CWIPPLNDAY A" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT *" + "\n");
            strSqlString.Append("          FROM MWIPMATDEF" + "\n");
            strSqlString.Append("         WHERE MAT_ID LIKE '%UW_' " + "\n");
            strSqlString.Append("           AND DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("       ) B" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND A.FACTORY = B.FACTORY(+)" + "\n");
            strSqlString.Append("   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND A.PLAN_DAY BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + FindWeek.EndDay_NextWeek + "'" + "\n");
            strSqlString.Append("   AND A.IN_OUT_FLAG = 'IN' " + "\n");
            strSqlString.Append("   AND A.MAT_ID LIKE '%UW_'" + "\n");
            strSqlString.Append("   AND B.MAT_ID IS NULL" + "\n");

            //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            //{
            //    System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            //}

            return strSqlString.ToString();
        }
        #endregion

        #region EVENT 처리
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            DataTable dt1 = null;
            DataTable dt2 = null;

            StringBuilder strSqlString = new StringBuilder();
            
            // 계획값 기준 시간 가져오기(Plan Revision Time)
            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("SELECT TO_DATE(RESV_FIELD5,'YYYY-MM-DD HH24MISS') FROM CWIPPLNDAY");
            }
            else
            {
                strSqlString.Append("SELECT TO_DATE(RESV_FIELD5,'YYYY-MM-DD HH24MISS') FROM CWIPPLNSNP@RPTTOMES WHERE SNAPSHOT_DAY ='" + cdvDate.SelectedValue() + "'");
            }
            

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

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 11, null, null, btnSort);

                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 1, true, Align.Center, VerticalAlign.Center);
                                
                spdData.RPT_AutoFit(false);

                // 당일 잔량 대비 재공 음영표시
                for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    int sum = 0;
                    int value = 0;

                    if (spdData.ActiveSheet.Cells[i, 39].BackColor.IsEmpty)
                    {
                        if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, 39].Value) > 0)
                        {
                            for (int y = 36; y >= 27; y--)
                            {
                                value = Convert.ToInt32(spdData.ActiveSheet.Cells[i, y].Value);
                                sum += value;

                                if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, 39].Value) > sum)
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

                dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlProduct());
                spdData2.DataSource = dt2;

                dt1.Dispose();
                dt.Dispose();
                dt2.Dispose();

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


                ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);

            }
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            //spdData.ExportExcel();            
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);         
        }
        #endregion

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {            
            Color BackColor = spdData.ActiveSheet.Cells[1, 39].BackColor;

            // subTotal 을 제외한 나머지 부분 클릭시 실행되도록...
            if (spdData.ActiveSheet.Cells[e.Row, 39].BackColor == BackColor)
            {
                string sMatId = spdData.ActiveSheet.Cells[e.Row, 9].Value.ToString();
                int iTarget = Convert.ToInt32(spdData.ActiveSheet.Cells[e.Row, 39].Value);

                // 잔량 이면서 Null 값이 아닌경우 클릭 시 팝업 창 띄움.
                if (e.Column == 39 && iTarget > 0)
                {
                    // 로딩 창 시작
                    LoadingPopUp.LoadIngPopUpShow(this);

                    DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetail(sMatId));

                    // 로딩 창 종료
                    LoadingPopUp.LoadingPopUpHidden();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        System.Windows.Forms.Form frm = new PRD010213_P1("", dt, iTarget);

                        frm.ShowDialog();
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }
}
