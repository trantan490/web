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
    public partial class PRD010212 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        string[] dayArry = new string[2];
        string[] dayArry1 = new string[2];

        /// <summary>
        /// 클  래  스: PRD010212<br/>
        /// 클래스요약: TEST 공정 생산일보<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2011-04-14<br/>
        /// 상세  설명: TEST 공정 생산일보 조회<br/>
        /// 변경  내용: <br/>
        /// 2011-04-25-임종우 : 업체 제외 - >'HA','HT','PI','TO','XR'
        ///                   : PKG  제외 -> SE COB, HM USB, HM Blue tooth (김주인 요청)
        /// 2011-05-25-임종우 : H72 HOLD CODE 재공 제외 (권순찬 요청)
        /// 2011-11-21-임종우 : 월 계획 제품 중복 오류 수정
        /// 2011-12-26-임종우 : MWIPCALDEF 의 작년,올해 마지막 주차 겹치는 에러 발생으로 SYS_YEAR -> PLAN_YEAR 으로 변경
        /// 2012-09-11-임종우 : 신규 포멧의 생산 일보로 변경 (권문석 요청)

        /// </summary>
        public PRD010212()
        {
            InitializeComponent();
            cdvDate.Value = DateTime.Now;
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
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Major Code", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Family", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);                
                spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD Count", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Type1", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Type2", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 7, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Density", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Generation", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Product", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Cust Device", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("Standard information", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("T/T", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("PARA", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Theory UPEH", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("UPEH Performance", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 12, 4);

                spdData.RPT_AddBasicColumn("Monthly plan", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Monthly Plan Rev", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("Monthly Production Status", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("actual", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Progress rate", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 18, 2);

                spdData.RPT_AddBasicColumn("a daily goal", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("Performance Status by Operation", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Semi-finished product status", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[0], 2, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[1], 2, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(1, 21, 2);

                spdData.RPT_AddBasicColumn("TEST OUT", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[0], 2, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[1], 2, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(1, 23, 2);

                spdData.RPT_AddBasicColumn("TEST HOLD", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[0], 2, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[1], 2, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(1, 25, 2);

                spdData.RPT_AddBasicColumn("LIS OUT", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[0], 2, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[1], 2, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(1, 27, 2);

                spdData.RPT_AddBasicColumn("TnR OUT", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[0], 2, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[1], 2, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(1, 29, 2);

                spdData.RPT_AddBasicColumn("SHIP", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[0], 2, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[1], 2, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(1, 31, 2);

                spdData.RPT_MerageHeaderColumnSpan(0, 21, 12);

                spdData.RPT_AddBasicColumn("Weekly SOP", 0, 33, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("plan", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("warehousing", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 33, 3);

                spdData.RPT_AddBasicColumn("RTF Status", 0, 36, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("plan", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("warehousing", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 36, 3);

                spdData.RPT_AddBasicColumn("Daily SOP", 0, 39, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("plan", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("warehousing", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 39, 3);

                spdData.RPT_AddBasicColumn("ASSY WIP", 0, 42, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Mold", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Finish", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                
                spdData.RPT_MerageHeaderColumnSpan(0, 42, 2);
                                
                spdData.RPT_AddBasicColumn("재공현황 (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + " 기준)", 0, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("STOCK", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TEST", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("BAKE", 1, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("LIS", 1, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TnR", 1, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("PACKING", 1, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("HMK4T", 1, 50, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TTL", 1, 51, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("FGS", 1, 52, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 44, 9);               

                spdData.RPT_MerageHeaderRowSpan(0, 0, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 6, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 7, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 8, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 9, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 10, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 11, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 16, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 17, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 20, 3);
                spdData.RPT_MerageHeaderRowSpan(1, 12, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 13, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 14, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 15, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 18, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 19, 2);

                for (int i = 32; i <= 52; i++)
                {
                    spdData.RPT_MerageHeaderRowSpan(1, i, 2);
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT.MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "MAT.MAT_GRP_9", "MAT.MAT_GRP_9 AS MAJOR_CODE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2 AS FAMILY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3 AS PACKAGE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6 AS LD_COUNT", false);
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
            string w_start_day;
            string m_start_day;
            string w_end_day;
            string Lastweek_lastday;
            string m_end_day;            
            string wed_day;
            string thu_day;
            string sKcpkValue;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;            
            
            // 2일 이후에만 기준월의 시작일을 가지고 사용함.
            //if (today.Substring(6, 2) == "01")
            //{
            //    start_day2 = dayArry1[0];
            //}
            //else
            //{
            //    start_day2 = start_day;
            //}

            // 일자 가져오기
            DataTable dt = null;
            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());
            w_start_day = dt.Rows[0][0].ToString();
            w_end_day = dt.Rows[0][1].ToString();
            wed_day = dt.Rows[0][2].ToString();
            thu_day = dt.Rows[0][3].ToString();
            m_start_day = dt.Rows[0][4].ToString();
            m_end_day = dt.Rows[0][5].ToString();
            Lastweek_lastday = dt.Rows[0][6].ToString();

            // 2012-08-09-임종우 : kcpk 선택에 의한 분모 값 저장 한다.
            if (ckbKpcs.Checked == true)
            {
                sKcpkValue = "1000";
            }
            else
            {
                sKcpkValue = "1";
            }
                        
            strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond2);
            strSqlString.Append("     , ATR.TEST_TIME, ATR.PARA_CNT " + "\n");
            strSqlString.Append("     , '' AS UPEH_MASTER " + "\n");
            strSqlString.Append("     , ROUND(SUM(MOV.TOUT_D1) / " + sKcpkValue + ", 0) AS UPEH_REAL " + "\n");
            strSqlString.Append("     , ROUND(SUM(PLN.ORI_PLAN) / " + sKcpkValue + ", 0) AS ORI_PLAN" + "\n");
            strSqlString.Append("     , ROUND(SUM(PLN.REV_PLAN) / " + sKcpkValue + ", 0) AS REV_PLAN" + "\n");
            strSqlString.Append("     , ROUND(SUM(SHP.SHP_MON) / " + sKcpkValue + ", 0) AS SHP_MON" + "\n");
            strSqlString.Append("     , CASE WHEN SUM(NVL(PLN.REV_PLAN,0)) = 0 OR SUM(NVL(SHP.SHP_MON,0)) = 0 THEN 0 " + "\n");
            strSqlString.Append("            ELSE ROUND((SUM(NVL(SHP.SHP_MON,0)) / SUM(NVL(PLN.REV_PLAN,0)))*100,1) " + "\n");
            strSqlString.Append("       END AS JINDO " + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(PLN.REV_PLAN,0)) - SUM(NVL(SHP.SHP_MON,0))) / " + lblRemain.Text + " / " + sKcpkValue + ", 0) AS TARGET" + "\n");
            strSqlString.Append("     , ROUND(SUM(MOV.RCV_D0) / " + sKcpkValue + ",0) AS RCV_D0" + "\n");
            strSqlString.Append("     , ROUND(SUM(MOV.RCV_D1) / " + sKcpkValue + ",0) AS RCV_D1" + "\n");
            strSqlString.Append("     , ROUND(SUM(MOV.TOUT_D0) / " + sKcpkValue + ",0) AS TOUT_D0" + "\n");
            strSqlString.Append("     , ROUND(SUM(MOV.TOUT_D1) / " + sKcpkValue + ",0) AS TOUT_D1" + "\n");
            strSqlString.Append("     , ROUND(SUM(HLD.HLD_D0) / " + sKcpkValue + ",0) AS HLD_D0" + "\n");
            strSqlString.Append("     , ROUND(SUM(WIP.HLD_D1) / " + sKcpkValue + ",0) AS HLD_D1" + "\n");
            strSqlString.Append("     , ROUND(SUM(MOV.LIS_D0) / " + sKcpkValue + ",0) AS LIS_D0" + "\n");
            strSqlString.Append("     , ROUND(SUM(MOV.LIS_D1) / " + sKcpkValue + ",0) AS LIS_D1" + "\n");
            strSqlString.Append("     , ROUND(SUM(MOV.TNR_D0) / " + sKcpkValue + ",0) AS TNR_D0" + "\n");
            strSqlString.Append("     , ROUND(SUM(MOV.TNR_D1) / " + sKcpkValue + ",0) AS TNR_D1" + "\n");
            strSqlString.Append("     , ROUND(SUM(SHP.SHP_D0) / " + sKcpkValue + ",0) AS SHP_D0" + "\n");
            strSqlString.Append("     , ROUND(SUM(SHP.SHP_D1) / " + sKcpkValue + ",0) AS SHP_D1" + "\n");
            strSqlString.Append("     , ROUND(SUM(GCM.PLN_WEEKLY) / " + sKcpkValue + ",0) AS PLN_WEEKLY" + "\n");
            strSqlString.Append("     , ROUND(SUM(SHP.SHP_WEEK) / " + sKcpkValue + ",0) AS SHP_WEEK" + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(GCM.PLN_WEEKLY,0)) - SUM(NVL(SHP.SHP_WEEK ,0))) / " + sKcpkValue + ",0) AS WEEKLY_DEF" + "\n");
            strSqlString.Append("     , ROUND(SUM(GCM.PLN_RTF) / " + sKcpkValue + ",0) AS PLN_RTF" + "\n");
            strSqlString.Append("     , ROUND(SUM(SHP.SHP_RTF) / " + sKcpkValue + ",0) AS SHP_RTF" + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(GCM.PLN_RTF,0)) - SUM(NVL(SHP.SHP_RTF ,0))) / " + sKcpkValue + ",0) AS RTF_DEF" + "\n");
            strSqlString.Append("     , ROUND(SUM(SOP.PLAN_QTY) / " + sKcpkValue + ",0) AS PLN_DAILY" + "\n");
            strSqlString.Append("     , ROUND(SUM(SHP.SHP_D1) / " + sKcpkValue + ",0) AS SHP_DAILY" + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(SOP.PLAN_QTY,0)) - SUM(NVL(SHP.SHP_D1 ,0))) / " + sKcpkValue + ",0) AS DAILY_DEF" + "\n");
            strSqlString.Append("     , ROUND(SUM(WIP.MOLD) / " + sKcpkValue + ",0) AS MOLD" + "\n");
            strSqlString.Append("     , ROUND(SUM(WIP.FINISH) / " + sKcpkValue + ",0) AS FINISH" + "\n");
            strSqlString.Append("     , ROUND(SUM(WIP.STOCK) / " + sKcpkValue + ",0) AS STOCK" + "\n");
            strSqlString.Append("     , ROUND(SUM(WIP.TEST) / " + sKcpkValue + ",0) AS TEST" + "\n");
            strSqlString.Append("     , ROUND(SUM(WIP.BAKE) / " + sKcpkValue + ",0) AS BAKE" + "\n");
            strSqlString.Append("     , ROUND(SUM(WIP.LIS) / " + sKcpkValue + ",0) AS LIS" + "\n");
            strSqlString.Append("     , ROUND(SUM(WIP.TNR) / " + sKcpkValue + ",0) AS TNR" + "\n");
            strSqlString.Append("     , ROUND(SUM(WIP.PACK) / " + sKcpkValue + ",0) AS PACK" + "\n");
            strSqlString.Append("     , ROUND(SUM(WIP.HMK4T) / " + sKcpkValue + ",0) AS HMK4T" + "\n");
            strSqlString.Append("     , ROUND(SUM(WIP.TTL) / " + sKcpkValue + ",0) AS TTL" + "\n");
            strSqlString.Append("     , ROUND(SUM(WIP.FGS) / " + sKcpkValue + ",0) AS FGS" + "\n");
            strSqlString.Append("  FROM MWIPMATDEF MAT " + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT FACTORY, MAT_ID, ORI_PLAN, REV_PLAN " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT FACTORY,MAT_ID,SUM(PLAN_QTY_TEST) AS ORI_PLAN, SUM(RESV_FIELD2) AS REV_PLAN " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_TEST) AS PLAN_QTY_TEST, SUM(TO_NUMBER(DECODE(RESV_FIELD2,' ',0,RESV_FIELD2))) AS RESV_FIELD2  " + "\n");
            strSqlString.Append("                          FROM CWIPPLNMON " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND PLAN_MONTH = TO_CHAR(SYSDATE, 'YYYYMM') " + "\n");
            strSqlString.Append("                         GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                         UNION ALL " + "\n");
            strSqlString.Append("                        SELECT A.FACTORY, A.MAT_ID, 0 AS PLAN_QTY_TEST, SUM(A.PLAN_QTY) AS RESV_FIELD2 " + "\n");
            strSqlString.Append("                          FROM ( " + "\n");
            strSqlString.Append("                                SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
            strSqlString.Append("                                  FROM CWIPPLNDAY " + "\n");
            strSqlString.Append("                                 WHERE 1=1 " + "\n");
            strSqlString.Append("                                   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("                                   AND PLAN_DAY BETWEEN '" + m_start_day + "' AND '" + m_end_day + "'\n");
            strSqlString.Append("                                   AND IN_OUT_FLAG = 'IN' " + "\n");
            strSqlString.Append("                                   AND CLASS = 'SLIS' " + "\n");
            strSqlString.Append("                                 GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                                 UNION ALL " + "\n");
            strSqlString.Append("                                SELECT FACTORY, MAT_ID, SUM(SHP_QTY_1) PLAN_QTY " + "\n");
            strSqlString.Append("                                  FROM VSUMWIPSHP " + "\n");
            strSqlString.Append("                                 WHERE 1=1 " + "\n");
            strSqlString.Append("                                   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("                                   AND CM_KEY_2 = 'PROD' " + "\n");
            strSqlString.Append("                                   AND CM_KEY_3 LIKE 'P%' " + "\n");
            strSqlString.Append("                                   AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + m_start_day + "' AND '" + Lastweek_lastday + "'\n");
            strSqlString.Append("                                 GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                               ) A " + "\n");
            strSqlString.Append("                             , MWIPMATDEF B " + "\n");
            strSqlString.Append("                         WHERE 1=1  " + "\n");
            strSqlString.Append("                           AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("                           AND B.MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                           AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
            strSqlString.Append("                         GROUP BY A.FACTORY, A.MAT_ID " + "\n");
            strSqlString.Append("                       ) " + "\n");
            strSqlString.Append("                 GROUP BY FACTORY, MAT_ID  " + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("         WHERE ORI_PLAN + REV_PLAN > 0 " + "\n");
            strSqlString.Append("       ) PLN" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT * " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT ATTR_KEY AS MAT_ID " + "\n");
            strSqlString.Append("                     , CASE WHEN ATTR_NAME = 'TEST_TIME_16PARA' THEN '16' " + "\n");
            strSqlString.Append("                            WHEN ATTR_NAME = 'TEST_TIME_OCTA' THEN '8' " + "\n");
            strSqlString.Append("                            WHEN ATTR_NAME IN ('TEST_TIME_QUAD', 'TEST_TIME_QUAD_2') THEN '4' " + "\n");
            strSqlString.Append("                            WHEN ATTR_NAME = 'TEST_TIME_DUAL' THEN '2' " + "\n");
            strSqlString.Append("                            ELSE '1' " + "\n");
            strSqlString.Append("                       END AS PARA_CNT " + "\n");
            strSqlString.Append("                     , ATTR_VALUE AS TEST_TIME " + "\n");
            strSqlString.Append("                     , RANK() OVER(PARTITION BY ATTR_KEY ORDER BY ATTR_NAME DESC) AS RNK " + "\n");
            strSqlString.Append("                  FROM MATRNAMSTS " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND ATTR_TYPE = 'MAT_TEST_PGM'" + "\n");
            strSqlString.Append("                   AND ATTR_NAME LIKE 'TEST_TIME%'" + "\n");
            strSqlString.Append("                   AND ATTR_VALUE <> ' '" + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("         WHERE RNK = 1 " + "\n");
            strSqlString.Append("       ) ATR " + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE = '" + dayArry1[0] + "' THEN SHP_QTY_1 ELSE 0 END) SHP_D0 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE = '" + dayArry1[1] + "' THEN SHP_QTY_1 ELSE 0 END) SHP_D1 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE BETWEEN '" + w_start_day + "' AND '" + w_end_day + "' THEN SHP_QTY_1 ELSE 0 END) SHP_WEEK " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE BETWEEN '" + wed_day + "' AND '" + thu_day + "' THEN SHP_QTY_1 ELSE 0 END) SHP_RTF " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE BETWEEN '" + m_start_day + "' AND '" + m_end_day + "' THEN SHP_QTY_1 ELSE 0 END) SHP_MON " + "\n");            
            strSqlString.Append("          FROM VSUMWIPSHP " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND CM_KEY_1 = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("           AND CM_KEY_2 = 'PROD' " + "\n");

            if (Convert.ToInt32(w_start_day) < Convert.ToInt32(m_start_day))
            {
                strSqlString.Append("           AND WORK_DATE BETWEEN '" + w_start_day + "' AND '" + m_end_day + "'" + "\n");
            }
            else
            {
                strSqlString.Append("           AND WORK_DATE BETWEEN '" + m_start_day + "' AND '" + m_end_day + "'" + "\n");
            }

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("           AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) SHP" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE = '" + dayArry1[0] + "' AND OPER = 'T0000' THEN (S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1) ELSE 0 END) AS RCV_D0 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE = '" + dayArry1[1] + "' AND OPER = 'T0000' THEN (S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1) ELSE 0 END) AS RCV_D1 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE = '" + dayArry1[0] + "' AND OPER IN ('T0100', 'T0400') THEN (S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1) ELSE 0 END) AS TOUT_D0 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE = '" + dayArry1[1] + "' AND OPER IN ('T0100', 'T0400') THEN (S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1) ELSE 0 END) AS TOUT_D1 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE = '" + dayArry1[0] + "' AND OPER = 'T1100' THEN (S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1) ELSE 0 END) AS LIS_D0 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE = '" + dayArry1[1] + "' AND OPER = 'T1100' THEN (S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1) ELSE 0 END) AS LIS_D1 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE = '" + dayArry1[0] + "' AND OPER = 'T1200' THEN (S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1) ELSE 0 END) AS TNR_D0 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE = '" + dayArry1[1] + "' AND OPER = 'T1200' THEN (S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1) ELSE 0 END) AS TNR_D1 " + "\n");
            strSqlString.Append("          FROM RSUMWIPMOV" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND FACTORY ='" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND OPER IN ('T0000', 'T0100', 'T0400', 'T1100', 'T1200') " + "\n");
            strSqlString.Append("           AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND WORK_DATE BETWEEN '" + dayArry1[0] + "' AND '" + dayArry1[1] + "'" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("           AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) MOV" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT A.MAT_ID" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND A.OPER_CMF_3 = 'MOLD' THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) AS MOLD " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND A.OPER_CMF_3 = 'FINISH' THEN QTY" + "\n");
            strSqlString.Append("                        ELSE 0 END) AS FINISH" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.OPER = 'T0000' THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) AS STOCK" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.OPER IN ('T0100', 'T0400', 'T1040') THEN QTY" + "\n");
            strSqlString.Append("                        ELSE 0 END) AS TEST" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.OPER = 'T1080' THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) AS BAKE" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.OPER = '1100' THEN QTY" + "\n");
            strSqlString.Append("                        ELSE 0 END) AS LIS" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.OPER = 'T1200' THEN QTY" + "\n");
            strSqlString.Append("                        ELSE 0 END) AS TNR" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.OPER IN ('T1260', 'T1300') THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) AS PACK" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.OPER = 'TZ010' THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) AS HMK4T" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.OPER <> 'T1150' THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) AS TTL" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = 'FGS' THEN QTY" + "\n");
            strSqlString.Append("                        ELSE 0 END) AS FGS" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.OPER IN ('T0100', 'T0400') AND HOLD_FLAG = 'Y' THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) AS HLD_D1" + "\n");            
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                 SELECT A.MAT_ID, A.FACTORY, A.OPER, B.OPER_GRP_1, B.OPER_CMF_3, A.QTY_1 AS QTY, HOLD_FLAG " + "\n");
            strSqlString.Append("                   FROM RWIPLOTSTS A " + "\n");
            strSqlString.Append("                      , MWIPOPRDEF B " + "\n");
            strSqlString.Append("                 WHERE A.FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "', 'FGS')" + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                   AND A.OPER = B.OPER " + "\n");
            strSqlString.Append("                   AND A.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                   AND A.LOT_DEL_FLAG = ' '" + "\n");            

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("               ) A" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) WIP" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID, SUM(QTY_1) AS HLD_D0" + "\n");
            strSqlString.Append("          FROM RWIPLOTSTS_BOH" + "\n");
            strSqlString.Append("         WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("           AND OPER IN ('T0100', 'T0400')" + "\n");
            strSqlString.Append("           AND HOLD_FLAG = 'Y'" + "\n");
            strSqlString.Append("           AND CUTOFF_DT = '" + dayArry1[0] + "22'" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("           AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) HLD" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID, PLAN_QTY" + "\n");
            strSqlString.Append("          FROM CWIPPLNDAY" + "\n");
            strSqlString.Append("         WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND PLAN_DAY = '" + dayArry1[1] + "'" + "\n");
            strSqlString.Append("           AND IN_OUT_FLAG = 'IN'" + "\n");
            strSqlString.Append("           AND CLASS = 'SLIS'" + "\n");
            strSqlString.Append("       ) SOP" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT KEY_1 AS MAT_ID, DATA_1 AS PLN_WEEKLY, DATA_2 AS PLN_RTF" + "\n");
            strSqlString.Append("          FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("         WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND TABLE_NAME = 'H_TEST_SOP_PLAN'" + "\n");            
            strSqlString.Append("       ) GCM" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND MAT.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("   AND MAT.FACTORY = PLN.FACTORY(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = ATR.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = MOV.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = HLD.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = SOP.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = GCM.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_TYPE= 'FG' " + "\n");
            strSqlString.Append("   AND MAT.DELETE_FLAG = ' ' " + "\n");

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

            strSqlString.AppendFormat(" GROUP BY {0}, ATR.TEST_TIME, ATR.PARA_CNT" + "\n", QueryCond1);
            strSqlString.Append(" HAVING (" + "\n");
            strSqlString.Append("         SUM(NVL(PLN.ORI_PLAN,0)) + SUM(NVL(PLN.REV_PLAN,0)) + SUM(NVL(SHP.SHP_MON,0)) + SUM(NVL(MOV.RCV_D0,0)) + SUM(NVL(MOV.RCV_D1,0)) +" + "\n");
            strSqlString.Append("         SUM(NVL(MOV.TOUT_D0,0)) + SUM(NVL(MOV.TOUT_D1,0)) + SUM(NVL(HLD.HLD_D0,0)) + SUM(NVL(WIP.HLD_D1,0)) + SUM(NVL(MOV.LIS_D0,0)) +" + "\n");
            strSqlString.Append("         SUM(NVL(MOV.LIS_D1,0)) + SUM(NVL(MOV.TNR_D0,0)) + SUM(NVL(MOV.TNR_D1,0)) + SUM(NVL(SHP.SHP_D0,0)) + SUM(NVL(SHP.SHP_D1,0)) +" + "\n");
            strSqlString.Append("         SUM(NVL(GCM.PLN_WEEKLY,0)) + SUM(NVL(SHP.SHP_WEEK,0)) + SUM(NVL(GCM.PLN_RTF,0)) + SUM(NVL(SOP.PLAN_QTY,0)) + SUM(NVL(WIP.MOLD,0)) +" + "\n");
            strSqlString.Append("         SUM(NVL(WIP.FINISH,0)) + SUM(NVL(WIP.STOCK,0)) + SUM(NVL(WIP.TEST,0)) + SUM(NVL(WIP.BAKE,0)) + SUM(NVL(WIP.LIS,0)) +" + "\n");
            strSqlString.Append("         SUM(NVL(WIP.TNR,0)) + SUM(NVL(WIP.PACK,0)) + SUM(NVL(WIP.HMK4T,0)) + SUM(NVL(WIP.FGS,0))" + "\n");
            strSqlString.Append("         ) <> 0" + "\n");
            strSqlString.AppendFormat(" ORDER BY {0}", QueryCond1 + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        // 일자 가져오기
        private string MakeSqlString1()
        {
            StringBuilder sqlString = new StringBuilder();

            sqlString.Append("SELECT TO_CHAR(TRUNC(SYSDATE, 'IW'), 'YYYYMMDD') AS D1 " + "\n");      // 금주 시작일 (월요일)
            sqlString.Append("     , TO_CHAR(TRUNC(SYSDATE, 'IW') + 6, 'YYYYMMDD') AS D2 " + "\n");  // 금주 종료일 (일요일)
            sqlString.Append("     , TO_CHAR(TRUNC(SYSDATE, 'IW') + 2, 'YYYYMMDD') AS D3 " + "\n");  // 금주 수요일
            sqlString.Append("     , TO_CHAR(TRUNC(SYSDATE, 'IW') + 3, 'YYYYMMDD') AS D4 " + "\n");  // 금주 목요일
            sqlString.Append("     , TO_CHAR(TRUNC(SYSDATE, 'MM'), 'YYYYMMDD') AS D5 " + "\n");      // 금월 시작일(1일)
            sqlString.Append("     , TO_CHAR(TRUNC(LAST_DAY(SYSDATE)), 'YYYYMMDD') AS D6 " + "\n");  // 금월 종료일
            sqlString.Append("     , TO_CHAR(TRUNC(SYSDATE, 'IW') + 13, 'YYYYMMDD') AS D7 " + "\n");  // 차주 종료일 (일요일)
            sqlString.Append("  FROM DUAL " + "\n");

            return sqlString.ToString();
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
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 12, null, null, btnSort);
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함
                //spdData.Sheets[0].FrozenColumnCount = 3;                

                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 12, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);

                spdData.RPT_SetPerSubTotalAndGrandTotal(0, 13, 12, 14);

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
                //string sNowDate = null;

                //sNowDate = DateTime.Now.ToString();
                //StringBuilder Condition = new StringBuilder();
                
                //Condition.AppendFormat("기준일자: {0}        진도율: {1}        잔여일수: {2}        LOT TYPE : {3}        조회시간 : {4}", lblToday.Text, lblJindo.Text, lblRemain.Text, cdvLotType.Text, sNowDate);
                
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
