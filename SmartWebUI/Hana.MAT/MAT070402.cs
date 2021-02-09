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

namespace Hana.MAT
{
    public partial class MAT070402 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: MAT070402<br/>
        /// 클래스요약: 원부자재 소요량 조회<br/>
        /// 작  성  자: 임종우<br/>
        /// 최초작성일: 2018-03-08<br/>
        /// 상세  설명: 원부자재 소요량 조회(이승희D 요청)<br/>
        /// 변경  내용: <br/>//
        /// 2018-03-16-임종우 : 점검필요 항목만 조회 기능 추가 - 과부족 3주차가 마이너스 값인 것 (이승희D 요청)
        ///                   : GROUP 옵션 활성화 - PRODUCT, 자재유형, MATCODE (이승희D 요청)
        /// 2018-11-01-임종우 : GROUP 옵션 Oper 까지 추가, 자재타입 검색 기능 전체 자재로 수정 (임태성차장 요청)    
        /// 2018-11-02-임종우 : 예상일 변경 - Summary Data 사용 -> 조회 시 직접 계산하여 사용 (임태성차장 요청)
        /// 2018-11-22-임종우 : 현재재고, 발주량 제품코드 기준으로 중복 집계 안되도록 수정 (임태성차장 요청)
        ///                   : Group 정보 Matcode 필수, Usage 정보 추가 (임태성차장 요청)
        /// 2018-11-26-임종우 : 예상소진일 자재재고 중복 집계되는 부분 수정, 과부족 조회 시 직접 계산하도록 변경 (임태성차장 요청)
        /// 2018-12-03-임종우 : PIN_TYPE 추가 (박형순대리 요청)
        /// 2019-11-20-김미경 : 현보유재고를 "On-site inventory", "Warehouse stock"로 나눔
        ///                   : 안전재고 Warning - 표준 납기(일) * 일필요량 * 50% 이 창고 재고와 비교 (PCB, RC 소자는 현보유재고와 비교)
        ///                   : 발주량 Warning - 표준납기(일) 를 주로 변환 -> 변환 된 주차를 기준으로 해당 주차 이후 과부족 주차가 –값이면서 발주량이 해당 과부족보다 작으면 Warning 표시
        /// 2019-11-21-김미경 : 발주량 셀 클릭 시, SAP 발주현황 Display   
        ///                   : 안전재고/ 발주량 Warning 인 항목만 보는 check 박스 생성
        /// 2019-11-27-김미경 : 안전재고 Warning / 발주량 Warning 안보이도록 처리 && Warning 항목에 대해서 재고 MistyRose 색상으로 셀배경 표현 
        ///                   : 표준납기 0W 인 경우에는 로직 내에서 4W로 Defalut 계산해준다. (화면에는 0W 그대로 표현하고 보유재고, 발주량 Warning 4w로 계산) - 이동욱 D 요청
        /// 2019-12-04-김미경 : 발주량에 발주 잔량 보이도록 수정 - 이동욱 D 요청
        /// 2020-01-28-김미경 : 팝업창의 발주잔량 오류로 쿼리 수정
        /// 2020-03-03-김미경 : usage 수정 =: A0000~A0399 공정의 원부자재에 한해 원부자재 사용량(Usage) = Usage * LOSS_QTY(H_HX_AUTO_LOSS, H_SEC_AUTO_LOSS ADAPT_OPER 'A0395'의 LOSS_QTY) - 이승희 D
        /// 2020-08-24-임종우 : 단위 추가 (김성업K 요청)
        #region " MAT070402 : Program Initial "

        GlobalVariable.FindWeek FindWeek = new GlobalVariable.FindWeek();
        
        public MAT070402()
        {
            InitializeComponent();
                        
            cdvDate.Value = DateTime.Now;
            SortInit();
            GridColumnInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
        }

        #endregion

        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (String.IsNullOrEmpty(cdvMatType.Text))
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD003", GlobalVariable.gcLanguage));
                return false;
            }

            if (((udcTableForm)(this.btnSort.BindingForm)).SelectedValueToQueryContainNull.Contains("MATCODE") == false)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD080", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        #endregion

        #region " GridColumnInit : Sheet Title 설정 "

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            FindWeek = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");       

            try
            {
                spdData.RPT_ColumnInit();

                spdData.RPT_AddBasicColumn("PRODUCT", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("Material type", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("MATCODE", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("categories", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 250);
                spdData.RPT_AddBasicColumn("Process group", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Operation", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Unit", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("USAGE", 0, 8, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("Remaining amount compared to production plan", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn(FindWeek.ThisWeek.Substring(4, 2) + "WW", 1, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn(FindWeek.NextWeek.Substring(4, 2) + "WW", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn(FindWeek.W2_Week.Substring(4, 2) + "WW", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn(FindWeek.W3_Week.Substring(4, 2) + "WW", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn(FindWeek.W4_Week.Substring(4, 2) + "WW", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn(FindWeek.W5_Week.Substring(4, 2) + "WW", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_MerageHeaderColumnSpan(0, 9, 6);

                spdData.RPT_AddBasicColumn("Material Acquisition Status", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("On-site inventory", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Warehouse stock", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Safety stock warning", 1, 17, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("Daily requirement", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Estimated exhaustion date", 1, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Material delivery date", 1, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Order quantity", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Order Warning", 1, 22, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);  
                spdData.RPT_MerageHeaderColumnSpan(0, 15, 8);

                spdData.RPT_AddBasicColumn("over and short quantity by work week", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn(FindWeek.ThisWeek.Substring(4, 2) + "WW", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn(FindWeek.NextWeek.Substring(4, 2) + "WW", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn(FindWeek.W2_Week.Substring(4, 2) + "WW", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn(FindWeek.W3_Week.Substring(4, 2) + "WW", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn(FindWeek.W4_Week.Substring(4, 2) + "WW", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn(FindWeek.W5_Week.Substring(4, 2) + "WW", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);                
                spdData.RPT_MerageHeaderColumnSpan(0, 23, 6);

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 8, 2);

                spdData.RPT_ColumnConfigFromTable(btnSort);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        #endregion

        #region " SortInit : Group By 설정 "

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            try
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.MAT_ID", "A.MAT_ID", "MAT_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "B.MAT_CMF_10 AS PIN_TYPE", "B.MAT_CMF_10", "PIN_TYPE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Material type", "A.MAT_TYPE", "A.MAT_TYPE", "MAT_TYPE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MATCODE", "A.MATCODE", "A.MATCODE", "MATCODE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("categories", "A.MAT_DESC", "A.MAT_DESC", "MAT_DESC", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Process group", "A.MAT_OPER", "A.MAT_OPER", "MAT_OPER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation", "A.OPER", "A.OPER", "OPER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Unit", "(SELECT UNIT_1 FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_ID = A.MATCODE) AS UNIT_1", "' '", "UNIT_1", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("USAGE", "CASE WHEN OPER BETWEEN 'A0000' AND 'A0399' THEN MAX(NVL(UNIT_QTY, 0)) * MAX(NVL(GCM.DATA_1, 1)) ELSE MAX(NVL(UNIT_QTY, 0)) END USAGE", "1", "USAGE", true);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "Type1", "MAT.MAT_GRP_4 AS Type1", "MAT.MAT_GRP_4", "Type1", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "Type2", "MAT.MAT_GRP_5 AS Type2", "MAT.MAT_GRP_5", "Type2", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "LD_COUNT", "MAT.MAT_GRP_6 AS LD_COUNT", "MAT.MAT_GRP_6", "LD_COUNT", true);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "PKG_CODE", "MAT.MAT_CMF_11 AS PKG_CODE", "MAT.MAT_CMF_11", "PKG_CODE", true);                
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        #endregion

        #region " MakeSqlString : Sql Query문 "

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

            string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값
                                                
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            // 2019-11-20-김미경 : QueryCond3 추가
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
                        
            if (ckbKpcs.Checked == true)
                sKpcsValue = "1000";
            else
                sKpcsValue = "1";

            strSqlString.Append("SELECT " + QueryCond3 + "\n");
            strSqlString.Append("       , REMAIN_W0, REMAIN_W1, REMAIN_W2, REMAIN_W3, REMAIN_W4, REMAIN_W5 \n");
            strSqlString.Append("       , MAT_L, MAT_INV_QTY, SAFTY_WARNING, MAT_NEED, MAT_FORECAST_DAY, DELIVERY_WEEK, MAT_ORDER_QTY, ORDER_WARNING \n");
            strSqlString.Append("       , SHORTAGE_W0, SHORTAGE_W1, SHORTAGE_W2, SHORTAGE_W3, SHORTAGE_W4, SHORTAGE_W5 \n");
            strSqlString.Append("  FROM (SELECT " + QueryCond3 + "\n");
            strSqlString.Append("             , REMAIN_W0, REMAIN_W1, REMAIN_W2, REMAIN_W3, REMAIN_W4, REMAIN_W5 \n");
            strSqlString.Append("             , MAT_L, MAT_INV_QTY \n");
            //strSqlString.Append("             , CASE WHEN MAT_TYPE = 'PB' AND DELIVERY_DATE * MAT_NEED * 0.5 > MAT_TTL THEN 'Y' \n");
            //strSqlString.Append("                    WHEN MAT_TYPE <> 'PB' AND DELIVERY_DATE * MAT_NEED * 0.5 > MAT_INV_QTY THEN 'Y' \n");
            strSqlString.Append("             , CASE WHEN MAT_TYPE = 'PB' AND DECODE(DELIVERY_DATE, 0, 4, DELIVERY_DATE) * MAT_NEED * 0.5 > MAT_TTL THEN 'Y' \n");
            strSqlString.Append("                    WHEN MAT_TYPE = 'RC' AND DECODE(DELIVERY_DATE, 0, 4, DELIVERY_DATE) * MAT_NEED * 0.5 > MAT_TTL THEN 'Y' \n");
            strSqlString.Append("                    WHEN DECODE(DELIVERY_DATE, 0, 4, DELIVERY_DATE) * MAT_NEED * 0.5 > MAT_INV_QTY THEN 'Y' \n");
            strSqlString.Append("                    ELSE 'N' \n");
            strSqlString.Append("               END SAFTY_WARNING \n");
            strSqlString.Append("             , MAT_NEED, MAT_FORECAST_DAY \n");
            strSqlString.Append("             , CEIL(DELIVERY_DATE / 7) || 'W' AS DELIVERY_WEEK \n");
            strSqlString.Append("             , MAT_ORDER_QTY \n");
            strSqlString.Append("             , CASE WHEN CEIL(DELIVERY_DATE / 7) = 0 AND SHORTAGE_W4 < 0 AND ABS(SHORTAGE_W4) > MAT_ORDER_QTY THEN 'Y' \n");
            strSqlString.Append("                    WHEN CEIL(DELIVERY_DATE / 7) = 1 AND SHORTAGE_W1 < 0 AND ABS(SHORTAGE_W1) > MAT_ORDER_QTY THEN 'Y' \n");
            strSqlString.Append("                    WHEN CEIL(DELIVERY_DATE / 7) = 2 AND SHORTAGE_W2 < 0 AND ABS(SHORTAGE_W2) > MAT_ORDER_QTY THEN 'Y' \n");
            strSqlString.Append("                    WHEN CEIL(DELIVERY_DATE / 7) = 3 AND SHORTAGE_W3 < 0 AND ABS(SHORTAGE_W3) > MAT_ORDER_QTY THEN 'Y' \n");
            strSqlString.Append("                    WHEN CEIL(DELIVERY_DATE / 7) = 4 AND SHORTAGE_W4 < 0 AND ABS(SHORTAGE_W4) > MAT_ORDER_QTY THEN 'Y' \n");
            strSqlString.Append("                    WHEN CEIL(DELIVERY_DATE / 7) = 5 AND SHORTAGE_W5 < 0 AND ABS(SHORTAGE_W5) > MAT_ORDER_QTY THEN 'Y' \n");
            strSqlString.Append("                    WHEN CEIL(DELIVERY_DATE / 7) = 6 AND SHORTAGE_W6 < 0 AND ABS(SHORTAGE_W6) > MAT_ORDER_QTY THEN 'Y' \n");
            strSqlString.Append("                    WHEN CEIL(DELIVERY_DATE / 7) = 7 AND SHORTAGE_W7 < 0 AND ABS(SHORTAGE_W7) > MAT_ORDER_QTY THEN 'Y' \n");
            strSqlString.Append("                    WHEN CEIL(DELIVERY_DATE / 7) = 8 AND SHORTAGE_W8 < 0 AND ABS(SHORTAGE_W8) > MAT_ORDER_QTY THEN 'Y' \n");
            strSqlString.Append("                    WHEN CEIL(DELIVERY_DATE / 7) = 9 AND SHORTAGE_W9 < 0 AND ABS(SHORTAGE_W9) > MAT_ORDER_QTY THEN 'Y' \n");
            strSqlString.Append("                    WHEN CEIL(DELIVERY_DATE / 7) = 10 AND SHORTAGE_W10 < 0 AND ABS(SHORTAGE_W10) > MAT_ORDER_QTY THEN 'Y' \n");
            strSqlString.Append("                    WHEN CEIL(DELIVERY_DATE / 7) = 11 AND SHORTAGE_W11 < 0 AND ABS(SHORTAGE_W11) > MAT_ORDER_QTY THEN 'Y' \n");
            strSqlString.Append("                    ELSE 'N' \n");
            strSqlString.Append("               END ORDER_WARNING  \n");
            strSqlString.Append("             , SHORTAGE_W0, SHORTAGE_W1, SHORTAGE_W2, SHORTAGE_W3, SHORTAGE_W4, SHORTAGE_W5 \n");
            strSqlString.Append("         FROM (SELECT " + QueryCond1 + "\n");  
            strSqlString.Append("                   , SUM(ROUND(REMAIN_W0,0)) AS REMAIN_W0 " + "\n");
            strSqlString.Append("                   , SUM(ROUND(REMAIN_W1,0)) AS REMAIN_W1 " + "\n");
            strSqlString.Append("                   , SUM(ROUND(REMAIN_W2,0)) AS REMAIN_W2 " + "\n");
            strSqlString.Append("                   , SUM(ROUND(REMAIN_W3,0)) AS REMAIN_W3 " + "\n");
            strSqlString.Append("                   , SUM(ROUND(REMAIN_W4,0)) AS REMAIN_W4 " + "\n");
            strSqlString.Append("                   , SUM(ROUND(REMAIN_W5,0)) AS REMAIN_W5 " + "\n");
            strSqlString.Append("                   , MAX(TRUNC(MAT_SMT_IN + MAT_L_IN + MAT_WIK_WIP + MAT_INV_L_QTY)) AS MAT_L" + "\n");
            strSqlString.Append("                   , MAX(TRUNC(MAT_INV_QTY)) AS MAT_INV_QTY" + "\n");
            strSqlString.Append("                   , MAX(TRUNC(MAT_TTL)) AS MAT_TTL" + "\n");
            strSqlString.Append("                   , SUM(TRUNC(MAT_NEED)) AS MAT_NEED" + "\n");
            //strSqlString.Append("     , MAX(MAT_FORECAST_DAY) AS MAT_FORECAST_DAY " + "\n");
            strSqlString.Append("                   , TO_CHAR(CASE WHEN SUM(TRUNC(MAT_NEED)) = 0 THEN TO_DATE('" + cdvDate.SelectedValue() + "', 'YYYYMMDD')" + "\n");
            strSqlString.Append("                                  WHEN (MAX(TRUNC(MAT_TTL)) / SUM(TRUNC(MAT_NEED))) >= 60 THEN TO_DATE('" + cdvDate.SelectedValue() + "', 'YYYYMMDD') + 60" + "\n");
            strSqlString.Append("                                  ELSE (MAX(TRUNC(MAT_TTL)) / SUM(TRUNC(MAT_NEED))) + TO_DATE('" + cdvDate.SelectedValue() + "', 'YYYYMMDD')" + "\n");
            strSqlString.Append("                             END, 'YY/MM/DD') MAT_FORECAST_DAY" + "\n");
            strSqlString.Append("                   , MAX(TRUNC(MAT_ORDER_QTY)) AS MAT_ORDER_QTY" + "\n");
            strSqlString.Append("                   , MAX(TRUNC(MAT_TTL)) - SUM(ROUND(REMAIN_W0,0)) AS SHORTAGE_W0" + "\n");
            strSqlString.Append("                   , MAX(TRUNC(MAT_TTL)) - SUM(ROUND(REMAIN_W0 + REMAIN_W1,0)) AS SHORTAGE_W1" + "\n");
            strSqlString.Append("                   , MAX(TRUNC(MAT_TTL)) - SUM(ROUND(REMAIN_W0 + REMAIN_W1 + REMAIN_W2,0)) AS SHORTAGE_W2" + "\n");
            strSqlString.Append("                   , MAX(TRUNC(MAT_TTL)) - SUM(ROUND(REMAIN_W0 + REMAIN_W1 + REMAIN_W2 + REMAIN_W3,0)) AS SHORTAGE_W3" + "\n");
            strSqlString.Append("                   , MAX(TRUNC(MAT_TTL)) - SUM(ROUND(REMAIN_W0 + REMAIN_W1 + REMAIN_W2 + REMAIN_W3 + REMAIN_W4,0)) AS SHORTAGE_W4" + "\n");
            strSqlString.Append("                   , MAX(TRUNC(MAT_TTL)) - SUM(ROUND(REMAIN_W0 + REMAIN_W1 + REMAIN_W2 + REMAIN_W3 + REMAIN_W4 + REMAIN_W5,0)) AS SHORTAGE_W5" + "\n");
            strSqlString.Append("                   , MAX(TRUNC(MAT_TTL)) - SUM(ROUND(REMAIN_W0 + REMAIN_W1 + REMAIN_W2 + REMAIN_W3 + REMAIN_W4 + REMAIN_W5 + REMAIN_W6,0)) AS SHORTAGE_W6" + "\n");
            strSqlString.Append("                   , MAX(TRUNC(MAT_TTL)) - SUM(ROUND(REMAIN_W0 + REMAIN_W1 + REMAIN_W2 + REMAIN_W3 + REMAIN_W4 + REMAIN_W5 + REMAIN_W6 + REMAIN_W7,0)) AS SHORTAGE_W7" + "\n");
            strSqlString.Append("                   , MAX(TRUNC(MAT_TTL)) - SUM(ROUND(REMAIN_W0 + REMAIN_W1 + REMAIN_W2 + REMAIN_W3 + REMAIN_W4 + REMAIN_W5 + REMAIN_W6 + REMAIN_W7 + REMAIN_W8,0)) AS SHORTAGE_W8" + "\n");
            strSqlString.Append("                   , MAX(TRUNC(MAT_TTL)) - SUM(ROUND(REMAIN_W0 + REMAIN_W1 + REMAIN_W2 + REMAIN_W3 + REMAIN_W4 + REMAIN_W5 + REMAIN_W6 + REMAIN_W7 + REMAIN_W8 + REMAIN_W9,0)) AS SHORTAGE_W9" + "\n");
            strSqlString.Append("                   , MAX(TRUNC(MAT_TTL)) - SUM(ROUND(REMAIN_W0 + REMAIN_W1 + REMAIN_W2 + REMAIN_W3 + REMAIN_W4 + REMAIN_W5 + REMAIN_W6 + REMAIN_W7 + REMAIN_W8 + REMAIN_W9 + REMAIN_W10,0)) AS SHORTAGE_W10" + "\n");
            strSqlString.Append("                   , MAX(TRUNC(MAT_TTL)) - SUM(ROUND(REMAIN_W0 + REMAIN_W1 + REMAIN_W2 + REMAIN_W3 + REMAIN_W4 + REMAIN_W5 + REMAIN_W6 + REMAIN_W7 + REMAIN_W8 + REMAIN_W9 + REMAIN_W10 + REMAIN_W11,0)) AS SHORTAGE_W11" + "\n");
            strSqlString.Append("                   , MAX(NVL(A.DELIVERY_DATE, 0)) AS DELIVERY_DATE" + "\n");
            //strSqlString.Append("     , SUM(ROUND(SHORTAGE_W0,0)) AS SHORTAGE_W0" + "\n");
            //strSqlString.Append("     , SUM(ROUND(SHORTAGE_W1,0)) AS SHORTAGE_W1" + "\n");
            //strSqlString.Append("     , SUM(ROUND(SHORTAGE_W2,0)) AS SHORTAGE_W2" + "\n");
            //strSqlString.Append("     , SUM(ROUND(SHORTAGE_W3,0)) AS SHORTAGE_W3" + "\n");
            //strSqlString.Append("     , SUM(ROUND(SHORTAGE_W4,0)) AS SHORTAGE_W4" + "\n");
            //strSqlString.Append("     , SUM(ROUND(SHORTAGE_W5,0)) AS SHORTAGE_W5" + "\n");
            strSqlString.Append("                FROM RSUMMATMRP A " + "\n");
            strSqlString.Append("                   , MWIPMATDEF B " + "\n");
            strSqlString.Append("                   , (SELECT KEY_1, DATA_1 FROM MGCMTBLDAT WHERE 1=1 AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS', 'H_HX_AUTO_LOSS') AND DATA_4 = 'A0395') GCM" + "\n");
            strSqlString.Append("               WHERE 1=1 " + "\n");
            strSqlString.Append("                 AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                 AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("                 AND A.MAT_ID = GCM.KEY_1(+)" + "\n");
            strSqlString.Append("                 AND A.WORK_DATE = '" + cdvDate.SelectedValue() + "'" + "\n");

            if (txtMatCode.Text.Trim() != "%" && txtMatCode.Text.Trim() != "")
                strSqlString.AppendFormat("                 AND A.MATCODE LIKE '{0}'" + "\n", txtMatCode.Text);

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                strSqlString.AppendFormat("                 AND A.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);

            if (cdvMatType.Text != "ALL" && cdvMatType.Text != "")
                strSqlString.AppendFormat("                 AND A.MAT_TYPE {0}" + "\n", cdvMatType.SelectedValueToQueryString);            

            #region 상세 조회에 따른 SQL문 생성
                                 
            //if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
            //    strSqlString.AppendFormat("          AND MAT.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            //if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
            //    strSqlString.AppendFormat("          AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            //if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
            //    strSqlString.AppendFormat("          AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            //if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
            //    strSqlString.AppendFormat("          AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            //if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
            //    strSqlString.AppendFormat("          AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            //if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
            //    strSqlString.AppendFormat("          AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            //if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
            //    strSqlString.AppendFormat("          AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            //if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
            //    strSqlString.AppendFormat("          AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            //if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
            //    strSqlString.AppendFormat("          AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);            

            #endregion

            strSqlString.Append("            GROUP BY " + QueryCond2 + "\n");
            
            if (ckbCheck.Checked == true)
            {
                //strSqlString.Append("   AND SHORTAGE_W2 < 0" + "\n");
                strSqlString.Append("          HAVING MAX(TRUNC(MAT_TTL)) - SUM(ROUND(REMAIN_W0 + REMAIN_W1 + REMAIN_W2,0)) < 0" + "\n");
            }

            strSqlString.Append("            ORDER BY " + QueryCond2 + ")) \n");
            strSqlString.Append(" WHERE 1 = 1" + "\n");

            if (ckbSafety_Warning.Checked == true)
            {
                strSqlString.Append("   AND SAFTY_WARNING = 'Y'" + "\n");               
            }

            if (ckbOrder_Warning.Checked == true)
            {
                strSqlString.Append("   AND ORDER_WARNING = 'Y'" + "\n");
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region "popup 창 쿼리"

        private string MakeSqlDetail(string s_Sub_MatId)
        {
            StringBuilder strSqlString = new StringBuilder();

            // 2019-11-21-김미경 : MATNR(자재코드) / TXZ01(자재명) / LOEKZ(삭제 플래그) / MENGE(오더수량) / MEINS(단위) / ELIKZ(납품 완료 플래그) / PRDAT(오더일) / EINDT(납품일)
            strSqlString.Append("  SELECT ORD.MATNR, ORD.TXZ01, ORD.EBELN,  ORD.MENGE \n");
            strSqlString.Append("       , ORD.MENGE - SUM(NVL(IN_ORD.IN_QTY, 0)) + SUM(NVL(OUT_ORD.OUT_QTY, 0)) AS REMAIN \n");
            strSqlString.Append("       , ORD.MEINS \n");
            strSqlString.Append("       , TO_CHAR(TO_DATE(ORD.PRDAT, 'YYYYMMDD'), 'YYYY.MM.DD') AS PRDAT \n");
            strSqlString.Append("       , TO_CHAR(TO_DATE(ORD.EINDT, 'YYYYMMDD'), 'YYYY.MM.DD') AS EINDT \n");
            strSqlString.Append("    FROM (SELECT *" + "\n");
            strSqlString.Append("            FROM ZHMMV003@SAPREAL" + "\n");
            strSqlString.Append("           WHERE MATNR = '" + s_Sub_MatId + "'" + "\n");
            strSqlString.Append("             AND LOEKZ = ' '" + "\n");
            strSqlString.Append("             AND ELIKZ = ' '" + ") ORD \n");
            strSqlString.Append("       , (SELECT MANDT, EBELN, EBELP, BEWTP, BWART, SUM(NVL(MENGE, 0)) IN_QTY" + "\n");
            strSqlString.Append("            FROM ZHMMV004@SAPREAL" + "\n");
            strSqlString.Append("           WHERE BEWTP = 'E'" + "\n");
            strSqlString.Append("             AND BWART IN ('101')" + "\n");
            strSqlString.Append("             GROUP BY MANDT, EBELN, EBELP, BEWTP, BWART) IN_ORD" + "\n");
            strSqlString.Append("       , (SELECT MANDT, EBELN, EBELP, BEWTP, BWART, SUM(NVL(MENGE, 0)) OUT_QTY" + "\n");
            strSqlString.Append("            FROM ZHMMV004@SAPREAL" + "\n");
            strSqlString.Append("           WHERE BEWTP = 'E'" + "\n");
            strSqlString.Append("             AND BWART IN ('102', '122', '123', '161')" + "\n");
            strSqlString.Append("             GROUP BY MANDT, EBELN, EBELP, BEWTP, BWART) OUT_ORD" + "\n");
            strSqlString.Append("   WHERE ORD.EBELN = IN_ORD.EBELN(+)" + "\n");
            strSqlString.Append("     AND ORD.EBELP = IN_ORD.EBELP(+)" + "\n");
            strSqlString.Append("     AND ORD.EBELN = OUT_ORD.EBELN(+)" + "\n");
            strSqlString.Append("     AND ORD.EBELP = OUT_ORD.EBELP(+)" + "\n");
            strSqlString.Append("GROUP BY ORD.MANDT, ORD.EBELN, ORD.EBELP, ORD.TXZ01, ORD.MATNR, ORD.MENGE, ORD.MEINS, ORD.PRDAT, ORD.EINDT" + "\n");
            strSqlString.Append("ORDER BY EBELN" + "\n");
            
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }         

            return strSqlString.ToString();
        }

        #endregion

        #region " Event 처리 "


        #region " btnView_Click : View버튼을 선택했을 때 "

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;           

            StringBuilder strSqlString = new StringBuilder();
                  
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

                spdData.DataSource = dt;
                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                //표구성에따른 항목 Display
                //spdData.RPT_ColumnConfigFromTable(btnSort);
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 5, 5, null, null, btnSort);

                //Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 5, 0, 1, true, Align.Center, VerticalAlign.Center);

                for (int i = 0; i <= 8; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LemonChiffon;
                }

                // 2019-11-27-김미경 : 안전재고 Warning 'Y' 이면 창고재고 MistyRose / 발주량 Warning 'Y' 이면 발주량 MistyRose 표시
                for (int y = 0; y < spdData_Sheet1.RowCount; y++)
                {
                    if (Convert.ToChar(spdData.Sheets[0].Cells[y, 17].Value) == 'Y')
                    {
                        spdData.ActiveSheet.Cells[y, 16].BackColor = Color.MistyRose;                        
                    }

                    if (Convert.ToChar(spdData.Sheets[0].Cells[y, 22].Value) == 'Y')
                    {
                        spdData.ActiveSheet.Cells[y, 21].BackColor = Color.MistyRose;                        
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

        #endregion


        #region " btnExcelExport_Click : Excel로 내보내기 "

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null, true);
            spdData.ExportExcel();
        }

        #endregion


        #region " spdData_CellClick : 셀 클릭 "

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        { 
            if (e.Column == 21)
            {
                string s_Sub_MatId = spdData.ActiveSheet.Cells[e.Row, 3].Value.ToString();
                int iTarget = Convert.ToInt32(spdData.ActiveSheet.Cells[e.Row, 21].Value);

                // 로딩 창 시작
                LoadingPopUp.LoadIngPopUpShow(this);

                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetail(s_Sub_MatId));

                // 로딩 창 종료
                LoadingPopUp.LoadingPopUpHidden();

                if (dt != null && dt.Rows.Count > 0)
                {
                    System.Windows.Forms.Form frm = new MAT070402_P1("", dt, iTarget);                   
                    frm.ShowDialog();
                }
            }
            else
            {
                return;
            }           
        }

        #endregion              

        #endregion

    }
}

