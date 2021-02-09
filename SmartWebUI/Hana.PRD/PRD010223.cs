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
    public partial class PRD010223 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        string[] dayArry = new string[3];
        string[] dayArry2 = new string[3];        
        GlobalVariable.FindWeek FindWeek = new GlobalVariable.FindWeek();

        /// <summary>
        /// 클  래  스: PRD010223<br/>
        /// 클래스요약: 투입단 관리<br/>
        /// 작  성  자: 임종우<br/>
        /// 최초작성일: 2014-07-28<br/>
        /// 상세  설명: 투입단 관리(임태성K 요청)<br/>
        /// 변경  내용: <br/>  
        /// 2014-08-11-임종우 : DA CAPA 대비 DA 재공이 50% 이하이면 음영표시 (임태성K 요청)
        /// 2014-08-19-임종우 : 재공, 실적 COMP 제품 로직 반영 A0395 공정 까지.. (임태성K 요청)
        /// 2014-08-20-임종우 : DA Capa 효율 75% -> 70%로 변경 (임태성K 요청)
        ///                   : 환산 시 Wafer 장수를 Wafer 수율로직 -> 실제 Wafer 장수(QTY_2) 로 변경 (임태성K 요청)
        /// 2014-08-21-임종우 : 상세 팝업창에 DA WIP 재공 추가 (임태성K 요청)
        /// 2014-09-15-임종우 : 상세 팝업창 - Middle & Merge 재공추가, Stock~Gate 공정 재공 추가
        /// 2014-09-23-임종우 : 싱글 칩도 제조 2그룹 차수별 재공현황 데이터 나오도록 수정, 상세 팝업창에 WIP Total 추가 (임태성K 요청)
        /// 2014-09-25-임종우 : A0250 공정 재공 SAW -> DA 포함 되도록 변경 (김권수D 요청)
        /// 2014-10-21-임종우 : DA WIP 로직 변경 -> 1차 A0401까지, 2차~3차 Middle, Merge 추가 (임태성K 요청)
        /// 2014-10-30-임종우 : MAIN_WIP 추가, SEKY% CHIP 종류 "S" 로 표시 (임태성K 요청)
        /// 2014-11-03-임종우 : MAIN_WIP 에 WB 추가 (임태성K 요청)
        /// 2015-02-23-임종우 : 9단 STACK 제품까지 표시 되도록 추가 (임태성K 요청)
        /// 2015-03-04-임종우 : Stealth Saw 데이터 추가 (임태성K 요청)
        /// 2015-03-09-임종우 : Stealth Saw 부분 수정 (임태성K 요청)
        ///                     환산시에도 QTY_1을 사용으로 변경.. NetDie로 나누고 수율 90%설정 (임태성K 요청)
        /// 2015-06-01-임종우 : A0400 공정 중복 되는 오류 수정
        /// 2015-06-08-임종우 : SAW 실적 - A0200, A0230 두 공정 모두 존재하는 FLOW 는 A0230 실적만 집계한다. (임태성K 요청)
        /// 2015-06-22-임종우 : 제조2그룹 차수별재공에서 A0250 공정 제외 (임태성K 요청)
        /// 2015-06-26-임종우 : WIP_BACK 재공에서 싱글칩 추가 및 A0809까지 재공 제외 함 - 제조2그룹 재공이랑 중복으로 인해 (임태성K 요청)
        /// 2015-07-06-임종우 : 설비 대수 집계 시 DISPATCH 기준 정보가 'Y" 인 설비만 집계 (임태성K 요청)
        /// 2016-06-03-임종우 : 잔량 데이터 3주차, 4주차 계획 대비 잔량 추가 (임태성K 요청)
        /// 2016-07-04-임종우 : Comp Qty, Wire Count 표시 (최인남상무 요청)
        /// 2016-07-11-임종우 : 전체고객사 조회 되도록 수정 (임태성K 요청)
        /// 2018-06-08-임종우 : Hynix 단종 제품 A-445 제외 (박형순대리 요청)
        /// </summary>
        public PRD010223()
        {
            InitializeComponent();                                   
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;    
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
                        
            FindWeek = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");
            
            try
            {  
                spdData.RPT_AddBasicColumn("Customer base", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Customer", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Product", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PKG", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("차수", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Chip type", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("DA CAPA", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("MAIN WIP", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA", 2, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB", 2, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 7, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 7, 2);

                spdData.RPT_AddBasicColumn("DA WIP", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Manufacturing Group 2 WIP Status by degree", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA WIP KIT Shortage Status", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("UNKIT Related Status", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("SAW Status", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 120);
                spdData.RPT_AddBasicColumn("WIP", 2, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Today's performance", 2, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("The previous day's performance", 2, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("B/G requirement", 2, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(1, 12, 4);

                spdData.RPT_AddBasicColumn("B/G Status", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WIP", 2, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Today's performance", 2, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("The previous day's performance", 2, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Input required", 2, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(1, 16, 4);

                spdData.RPT_AddBasicColumn("Wafer Stock", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("WIP", 2, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Today's performance", 2, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(1, 20, 2);
                spdData.RPT_MerageHeaderColumnSpan(0, 12, 10);

                spdData.RPT_AddBasicColumn("Input residual", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("weekly", 2, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("next week", 2, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W3", 2, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W4", 2, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("monthly", 2, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 22, 5);
                spdData.RPT_MerageHeaderRowSpan(0, 22, 2);

                spdData.RPT_AddBasicColumn("WF warehousing remaining", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("weekly", 2, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("next week", 2, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W3", 2, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W4", 2, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("monthly", 2, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 27, 5);
                spdData.RPT_MerageHeaderRowSpan(0, 27, 2);

                spdData.RPT_AddBasicColumn("Stealth Saw", 0, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("application", 2, 32, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("UPH", 2, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 32, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 32, 2);

                spdData.RPT_AddBasicColumn("WIP", 0, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("Stealth", 2, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Pre BG", 2, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Lami", 2, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Stock", 2, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TTL", 2, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 34, 5);
                spdData.RPT_MerageHeaderRowSpan(0, 34, 2);

                spdData.RPT_AddBasicColumn("Stealth Saw Remaining", 0, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("weekly", 2, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("next week", 2, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("monthly", 2, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 39, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 39, 2);

                spdData.RPT_AddBasicColumn("DP reference information", 0, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("Thick", 2, 42, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Tape", 2, 43, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("NetDie", 2, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 42, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 42, 2);

                spdData.RPT_AddBasicColumn("Comp Qty", 0, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Wire Count", 0, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_MerageHeaderRowSpan(0, 0, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 6, 3);                
                spdData.RPT_MerageHeaderRowSpan(0, 9, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 10, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 11, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 45, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 46, 3);

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
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "DECODE(A.CUSTOMER,'SEC','SEC','HYNIX','HYNIX','iML','iML','FCI','FCI','IMAGIS','IMAGIS' , (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.CUSTOMER)) AS CUSTOMER", "(CASE WHEN A.MAT_GRP_1 = 'SE' THEN 'SEC' WHEN A.MAT_GRP_1 = 'HX' THEN 'HYNIX'  WHEN A.MAT_GRP_1 = 'IM' THEN 'iML'  WHEN A.MAT_GRP_1 = 'FC' THEN 'FCI' WHEN A.MAT_GRP_1 = 'IG' THEN 'IMAGIS' ELSE A.MAT_GRP_1 END)", "(CASE WHEN A.MAT_GRP_1 = 'SE' THEN 'SEC' WHEN A.MAT_GRP_1 = 'HX' THEN 'HYNIX' WHEN A.MAT_GRP_1 = 'IM' THEN 'iML' WHEN A.MAT_GRP_1 = 'FC' THEN 'FCI' WHEN A.MAT_GRP_1 = 'IG' THEN 'IMAGIS' ELSE A.MAT_GRP_1 END) CUSTOMER", "(CASE WHEN MAT_GRP_1 = 'SE' THEN 'SEC' WHEN MAT_GRP_1 = 'HX' THEN 'HYNIX'  WHEN MAT_GRP_1 = 'IM' THEN 'iML'  WHEN MAT_GRP_1 = 'FC' THEN 'FCI' WHEN MAT_GRP_1 = 'IG' THEN 'IMAGIS' ELSE MAT_GRP_1 END)", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "A.MAJOR", "A.MAT_GRP_9", "A.MAT_GRP_9 AS MAJOR","MAT_GRP_9", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "A.PKG", "A.MAT_GRP_10", "A.MAT_GRP_10 AS PKG","MAT_GRP_10", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "A.TYPE1", "A.MAT_GRP_4","A.MAT_GRP_4 AS TYPE1","MAT_GRP_4", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "A.TYPE2", "A.MAT_GRP_5", "A.MAT_GRP_5 AS TYPE2", "MAT_GRP_5", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "A.LD_COUNT", "A.MAT_GRP_6", "A.MAT_GRP_6 AS LD_COUNT","MAT_GRP_6", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "A.PKG_CODE", "A.MAT_CMF_11", "A.MAT_CMF_11 AS PKG_CODE","MAT_CMF_11", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "A.DENSITY", "A.MAT_GRP_7", "A.MAT_GRP_7 AS DENSITY", "MAT_GRP_7", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "A.GENERATION", "A.MAT_GRP_8", "A.MAT_GRP_8 AS GENERATION", "MAT_GRP_8", false); 
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "A.PIN_TYPE", "A.MAT_CMF_10", "A.MAT_CMF_10 AS PIN_TYPE","MAT_CMF_10", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.MAT_ID", "A.MAT_ID", "A.MAT_ID", "MAT_ID", false);            
 
        }
        #endregion

        #region 시간관련 함수
        private void GetWorkDay()
        {
            FindWeek = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");
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
            string start_date;
            string yesterday;
            string date;
            string month;
            string year;
            string lastMonth;
            string sKpcsValue;
            string sQty_Type;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            

            date = cdvDate.SelectedValue();

            DateTime Select_date;
            Select_date = DateTime.Parse(cdvDate.Text.ToString());

            year = Select_date.ToString("yyyy");
            month = Select_date.ToString("yyyyMM");
            start_date = month + "01";
            yesterday = Select_date.AddDays(-1).ToString("yyyyMMdd");
            lastMonth = Select_date.AddMonths(-1).ToString("yyyyMM"); // 지난달

            // 2015-03-09-임종우 : 환산시에도 QTY_1을 사용으로 변경.. NetDie로 나누고 수율 90%설정 (임태성K 요청)
            if (ckbConv.Checked == true)
            {
                sQty_Type = "QTY_1";
            }
            else
            {
                sQty_Type = "QTY_1";
            }

            if (ckbKpcs.Checked == true)
            {
                sKpcsValue = "1000";
            }
            else
            {
                sKpcsValue = "1";
            }
                        
            strSqlString.Append("SELECT CUST, MAT_GRP_1, MAT_GRP_10, MAT_CMF_11, MAT_GRP_5, CHIP" + "\n");

            if (ckbConv.Checked == true)
            {
                strSqlString.Append("     , ROUND(CAPA / " + sKpcsValue + ", 0) AS CAPA" + "\n");
                strSqlString.Append("     , ROUND(MAIN_DA / (NET_DIE * 0.9), 0) AS MAIN_DA" + "\n");
                strSqlString.Append("     , ROUND(MAIN_WB / (NET_DIE * 0.9), 0) AS MAIN_WB" + "\n");
                strSqlString.Append("     , ROUND((WIP_DA + WIP_MERGE) / (NET_DIE * 0.9), 0) AS WIP_DA" + "\n");
                strSqlString.Append("     , ROUND(WIP_2GROUP / (NET_DIE * 0.9), 0) AS WIP_2GROUP" + "\n");
                strSqlString.Append("     , ROUND((WIP_2GROUP - MAX(WIP_2GROUP) OVER(PARTITION BY MAT_GRP_1, MAT_GRP_10, MAT_CMF_11)) / (NET_DIE * 0.9), 0) AS DA_WIP_KIT" + "\n");
                strSqlString.Append("     , ROUND(WIP_SAW / (NET_DIE * 0.9), 0) AS WIP_SAW" + "\n");
                strSqlString.Append("     , ROUND(T_OUT_SAW / (NET_DIE * 0.9), 0) AS T_OUT_SAW" + "\n");
                strSqlString.Append("     , ROUND(Y_OUT_SAW / (NET_DIE * 0.9), 0) AS Y_OUT_SAW" + "\n");
                strSqlString.Append("     , ROUND((-(WIP_2GROUP - MAX(WIP_2GROUP) OVER(PARTITION BY MAT_GRP_1, MAT_GRP_10, MAT_CMF_11)) - WIP_SAW) / (NET_DIE * 0.9), 0) AS NEED_BG" + "\n");
                strSqlString.Append("     , ROUND(WIP_BG / (NET_DIE * 0.9), 0) AS WIP_BG" + "\n");
                strSqlString.Append("     , ROUND(T_OUT_BG / (NET_DIE * 0.9), 0) AS T_OUT_BG" + "\n");
                strSqlString.Append("     , ROUND(Y_OUT_BG / (NET_DIE * 0.9), 0) AS Y_OUT_BG" + "\n");
                strSqlString.Append("     , ROUND((-(WIP_2GROUP - MAX(WIP_2GROUP) OVER(PARTITION BY MAT_GRP_1, MAT_GRP_10, MAT_CMF_11)) - WIP_SAW - WIP_BG) / (NET_DIE * 0.9), 0) AS NEED_INPUT" + "\n");
                strSqlString.Append("     , ROUND(WIP_STOCK / (NET_DIE * 0.9), 0) AS WIP_STOCK" + "\n");
                strSqlString.Append("     , ROUND(T_OUT_STOCK / (NET_DIE * 0.9), 0) AS T_OUT_STOCK" + "\n");
                strSqlString.Append("     , ROUND((PLN_WEEK1 - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK)) / (NET_DIE * 0.9), 0) AS T_WEEK_IN_DEF" + "\n");
                strSqlString.Append("     , ROUND(((PLN_WEEK1 + PLN_WEEK2) - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK)) / (NET_DIE * 0.9), 0) AS N_WEEK_IN_DEF" + "\n");
                strSqlString.Append("     , ROUND(((PLN_WEEK1 + PLN_WEEK2 + PLN_WEEK3) - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK)) / (NET_DIE * 0.9), 0) AS NN_WEEK_IN_DEF" + "\n");
                strSqlString.Append("     , ROUND(((PLN_WEEK1 + PLN_WEEK2 + PLN_WEEK3 + PLN_WEEK4) - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK)) / (NET_DIE * 0.9), 0) AS NNN_WEEK_IN_DEF" + "\n");
                strSqlString.Append("     , ROUND((PLN_MONTH - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_MONTH)) / (NET_DIE * 0.9), 0) AS MONTH_IN_DEF" + "\n");
                strSqlString.Append("     , ROUND((((PLN_WEEK1 - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK)) - WIP_STOCK)) / (NET_DIE * 0.9), 0) AS T_WEEK_IN_WF" + "\n");
                strSqlString.Append("     , ROUND(((((PLN_WEEK1 + PLN_WEEK2) - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK)) - WIP_STOCK)) / (NET_DIE * 0.9), 0) AS N_WEEK_IN_WF" + "\n");
                strSqlString.Append("     , ROUND(((((PLN_WEEK1 + PLN_WEEK2 + PLN_WEEK3) - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK)) - WIP_STOCK)) / (NET_DIE * 0.9), 0) AS NN_WEEK_IN_WF" + "\n");
                strSqlString.Append("     , ROUND(((((PLN_WEEK1 + PLN_WEEK2 + PLN_WEEK3 + PLN_WEEK4) - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK)) - WIP_STOCK)) / (NET_DIE * 0.9), 0) AS NNN_WEEK_IN_WF" + "\n");
                strSqlString.Append("     , ROUND((((PLN_MONTH - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_MONTH)) - WIP_STOCK)) / (NET_DIE * 0.9), 0) AS MONTH_IN_WF" + "\n");
                strSqlString.Append("     , OPER_STEALTH" + "\n");
                strSqlString.Append("     , ROUND(UPH_STEALTH / (NET_DIE * 0.9), 0) AS UPH_STEALTH" + "\n");
                strSqlString.Append("     , ROUND(WIP_STEALTH / (NET_DIE * 0.9), 0) AS WIP_STEALTH" + "\n");
                strSqlString.Append("     , ROUND(WIP_PREBG / (NET_DIE * 0.9), 0) AS WIP_PREBG" + "\n");
                strSqlString.Append("     , ROUND(WIP_LAMI / (NET_DIE * 0.9), 0) AS WIP_LAMI" + "\n");
                strSqlString.Append("     , ROUND(WIP_STOCK / (NET_DIE * 0.9), 0) AS WIP_STOCK" + "\n");
                strSqlString.Append("     , ROUND((WIP_STOCK + WIP_LAMI + WIP_PREBG + WIP_STEALTH) / (NET_DIE * 0.9), 0) AS WIP_TTL" + "\n");
                strSqlString.Append("     , CASE WHEN OPER_STEALTH = 'O' THEN ROUND((PLN_WEEK1 - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK) + WIP_STEALTH + WIP_PREBG + WIP_LAMI) / (NET_DIE * 0.9), 0)" + "\n");
                strSqlString.Append("            ELSE 0" + "\n");
                strSqlString.Append("       END AS T_WEEK_STEALTH_IN_DEF" + "\n");
                strSqlString.Append("     , CASE WHEN OPER_STEALTH = 'O' THEN ROUND(((PLN_WEEK1 + PLN_WEEK2) - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK) + WIP_STEALTH + WIP_PREBG + WIP_LAMI) / (NET_DIE * 0.9), 0)" + "\n");
                strSqlString.Append("            ELSE 0" + "\n");
                strSqlString.Append("       END  AS N_WEEK_STEALTH_IN_DEF" + "\n");
                strSqlString.Append("     , CASE WHEN OPER_STEALTH = 'O' THEN ROUND((PLN_MONTH - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_MONTH) + WIP_STEALTH + WIP_PREBG + WIP_LAMI) / (NET_DIE * 0.9), 0)" + "\n");
                strSqlString.Append("            ELSE 0" + "\n");
                strSqlString.Append("       END  AS MONTH_STEALTH_IN_DEF" + "\n");
                strSqlString.Append("     , BG_1, WM_TYPE1, NET_DIE, COMP_CNT, WIRE_CNT" + "\n");


                //strSqlString.Append("     , ROUND(CAPA / " + sKpcsValue + ", 0) AS CAPA" + "\n");
                //strSqlString.Append("     , ROUND(MAIN_DA, 0) AS MAIN_DA" + "\n");
                //strSqlString.Append("     , ROUND(MAIN_WB, 0) AS MAIN_WB" + "\n");
                //strSqlString.Append("     , ROUND(WIP_DA + WIP_MERGE, 0) AS WIP_DA" + "\n");
                //strSqlString.Append("     , ROUND(WIP_2GROUP, 0) AS WIP_2GROUP" + "\n");
                //strSqlString.Append("     , ROUND(WIP_2GROUP - MAX(WIP_2GROUP) OVER(PARTITION BY MAT_GRP_1, MAT_GRP_10, MAT_CMF_11), 0) AS DA_WIP_KIT" + "\n");
                //strSqlString.Append("     , ROUND(WIP_SAW, 0) AS WIP_SAW" + "\n");
                //strSqlString.Append("     , ROUND(T_OUT_SAW, 0) AS T_OUT_SAW" + "\n");
                //strSqlString.Append("     , ROUND(Y_OUT_SAW, 0) AS Y_OUT_SAW" + "\n");                
                //strSqlString.Append("     , ROUND(-(WIP_2GROUP - MAX(WIP_2GROUP) OVER(PARTITION BY MAT_GRP_1, MAT_GRP_10, MAT_CMF_11)) - WIP_SAW, 0) AS NEED_BG" + "\n");
                //strSqlString.Append("     , ROUND(WIP_BG, 0) AS WIP_BG" + "\n");
                //strSqlString.Append("     , ROUND(T_OUT_BG, 0) AS T_OUT_BG" + "\n");
                //strSqlString.Append("     , ROUND(Y_OUT_BG, 0) AS Y_OUT_BG" + "\n");                
                //strSqlString.Append("     , ROUND(-(WIP_2GROUP - MAX(WIP_2GROUP) OVER(PARTITION BY MAT_GRP_1, MAT_GRP_10, MAT_CMF_11)) - WIP_SAW - WIP_BG, 0) AS NEED_INPUT" + "\n");
                //strSqlString.Append("     , ROUND(WIP_STOCK, 0) AS WIP_STOCK" + "\n");
                //strSqlString.Append("     , ROUND(T_OUT_STOCK, 0) AS T_OUT_STOCK" + "\n");
                //strSqlString.Append("     , ROUND(PLN_WEEK1 - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK), 0) AS T_WEEK_IN_DEF" + "\n");
                //strSqlString.Append("     , ROUND((PLN_WEEK1 + PLN_WEEK2) - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK), 0) AS N_WEEK_IN_DEF" + "\n");
                //strSqlString.Append("     , ROUND(PLN_MONTH - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_MONTH), 0) AS MONTH_IN_DEF" + "\n");
                //strSqlString.Append("     , ROUND(((PLN_WEEK1 - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK)) - WIP_STOCK), 0) AS T_WEEK_IN_WF" + "\n");
                //strSqlString.Append("     , ROUND((((PLN_WEEK1 + PLN_WEEK2) - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK)) - WIP_STOCK), 0) AS N_WEEK_IN_WF" + "\n");
                //strSqlString.Append("     , ROUND(((PLN_MONTH - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_MONTH)) - WIP_STOCK), 0) AS MONTH_IN_WF" + "\n");
                //strSqlString.Append("     , OPER_STEALTH" + "\n");
                //strSqlString.Append("     , ROUND(UPH_STEALTH, 0) AS UPH_STEALTH" + "\n");
                //strSqlString.Append("     , ROUND(WIP_STEALTH, 0) AS WIP_STEALTH" + "\n");
                //strSqlString.Append("     , ROUND(WIP_PREBG, 0) AS WIP_PREBG" + "\n");
                //strSqlString.Append("     , ROUND(WIP_LAMI, 0) AS WIP_LAMI" + "\n");
                //strSqlString.Append("     , ROUND(WIP_STOCK, 0) AS WIP_STOCK" + "\n");
                //strSqlString.Append("     , ROUND(WIP_STOCK + WIP_LAMI + WIP_PREBG + WIP_STEALTH, 0) AS WIP_TTL" + "\n");
                //strSqlString.Append("     , CASE WHEN OPER_STEALTH = 'O' THEN ROUND(PLN_WEEK1 - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK) + WIP_STEALTH + WIP_PREBG + WIP_LAMI, 0)" + "\n");
                //strSqlString.Append("            ELSE 0" + "\n");
                //strSqlString.Append("       END AS T_WEEK_STEALTH_IN_DEF" + "\n");
                //strSqlString.Append("     , CASE WHEN OPER_STEALTH = 'O' THEN ROUND((PLN_WEEK1 + PLN_WEEK2) - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK) + WIP_STEALTH + WIP_PREBG + WIP_LAMI, 0)" + "\n");
                //strSqlString.Append("            ELSE 0" + "\n");
                //strSqlString.Append("       END  AS N_WEEK_STEALTH_IN_DEF" + "\n");
                //strSqlString.Append("     , CASE WHEN OPER_STEALTH = 'O' THEN ROUND(PLN_MONTH - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_MONTH) + WIP_STEALTH + WIP_PREBG + WIP_LAMI, 0)" + "\n");
                //strSqlString.Append("            ELSE 0" + "\n");
                //strSqlString.Append("       END  AS MONTH_STEALTH_IN_DEF" + "\n");
                //strSqlString.Append("     , BG_1, WM_TYPE1, NET_DIE" + "\n");                
            }
            else
            {
                strSqlString.Append("     , ROUND(CAPA / " + sKpcsValue + ", 0) AS CAPA" + "\n");
                strSqlString.Append("     , ROUND(MAIN_DA / " + sKpcsValue + ", 0) AS MAIN_DA" + "\n");
                strSqlString.Append("     , ROUND(MAIN_WB / " + sKpcsValue + ", 0) AS MAIN_WB" + "\n");
                strSqlString.Append("     , ROUND((WIP_DA + WIP_MERGE) / " + sKpcsValue + ", 0) AS WIP_DA" + "\n");
                strSqlString.Append("     , ROUND(WIP_2GROUP / " + sKpcsValue + ", 0) AS WIP_2GROUP" + "\n");
                strSqlString.Append("     , ROUND((WIP_2GROUP - MAX(WIP_2GROUP) OVER(PARTITION BY MAT_GRP_1, MAT_GRP_10, MAT_CMF_11)) / " + sKpcsValue + ", 0) AS DA_WIP_KIT" + "\n");
                strSqlString.Append("     , ROUND(WIP_SAW / " + sKpcsValue + ", 0) AS WIP_SAW" + "\n");
                strSqlString.Append("     , ROUND(T_OUT_SAW / " + sKpcsValue + ", 0) AS T_OUT_SAW" + "\n");
                strSqlString.Append("     , ROUND(Y_OUT_SAW / " + sKpcsValue + ", 0) AS Y_OUT_SAW" + "\n");                
                strSqlString.Append("     , ROUND((-(WIP_2GROUP - MAX(WIP_2GROUP) OVER(PARTITION BY MAT_GRP_1, MAT_GRP_10, MAT_CMF_11)) - WIP_SAW) / " + sKpcsValue + ", 0) AS NEED_BG" + "\n");
                strSqlString.Append("     , ROUND(WIP_BG / " + sKpcsValue + ", 0) AS WIP_BG" + "\n");
                strSqlString.Append("     , ROUND(T_OUT_BG / " + sKpcsValue + ", 0) AS T_OUT_BG" + "\n");
                strSqlString.Append("     , ROUND(Y_OUT_BG / " + sKpcsValue + ", 0) AS Y_OUT_BG" + "\n");                
                strSqlString.Append("     , ROUND((-(WIP_2GROUP - MAX(WIP_2GROUP) OVER(PARTITION BY MAT_GRP_1, MAT_GRP_10, MAT_CMF_11)) - WIP_SAW - WIP_BG) / " + sKpcsValue + ", 0) AS NEED_INPUT" + "\n");
                strSqlString.Append("     , ROUND(WIP_STOCK / " + sKpcsValue + ", 0) AS WIP_STOCK" + "\n");
                strSqlString.Append("     , ROUND(T_OUT_STOCK / " + sKpcsValue + ", 0) AS T_OUT_STOCK" + "\n");
                strSqlString.Append("     , ROUND((PLN_WEEK1 - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK)) / " + sKpcsValue + ", 0) AS T_WEEK_IN_DEF" + "\n");
                strSqlString.Append("     , ROUND(((PLN_WEEK1 + PLN_WEEK2) - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK)) / " + sKpcsValue + ", 0) AS N_WEEK_IN_DEF" + "\n");
                strSqlString.Append("     , ROUND(((PLN_WEEK1 + PLN_WEEK2 + PLN_WEEK3) - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK)) / " + sKpcsValue + ", 0) AS NN_WEEK_IN_DEF" + "\n");
                strSqlString.Append("     , ROUND(((PLN_WEEK1 + PLN_WEEK2 + PLN_WEEK3 + PLN_WEEK4) - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK)) / " + sKpcsValue + ", 0) AS NNN_WEEK_IN_DEF" + "\n");
                strSqlString.Append("     , ROUND((PLN_MONTH - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_MONTH)) / " + sKpcsValue + ", 0) AS MONTH_IN_DEF" + "\n");
                strSqlString.Append("     , ROUND((((PLN_WEEK1 - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK)) - WIP_STOCK)) / " + sKpcsValue + ", 0) AS T_WEEK_IN_WF" + "\n");
                strSqlString.Append("     , ROUND(((((PLN_WEEK1 + PLN_WEEK2) - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK)) - WIP_STOCK)) / " + sKpcsValue + ", 0) AS N_WEEK_IN_WF" + "\n");
                strSqlString.Append("     , ROUND(((((PLN_WEEK1 + PLN_WEEK2 + PLN_WEEK3) - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK)) - WIP_STOCK)) / " + sKpcsValue + ", 0) AS NN_WEEK_IN_WF" + "\n");
                strSqlString.Append("     , ROUND(((((PLN_WEEK1 + PLN_WEEK2 + PLN_WEEK3 + PLN_WEEK4) - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK)) - WIP_STOCK)) / " + sKpcsValue + ", 0) AS NNN_WEEK_IN_WF" + "\n");
                strSqlString.Append("     , ROUND((((PLN_MONTH - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_MONTH)) - WIP_STOCK)) / " + sKpcsValue + ", 0) AS MONTH_IN_WF" + "\n");
                strSqlString.Append("     , OPER_STEALTH" + "\n");
                strSqlString.Append("     , ROUND(UPH_STEALTH / " + sKpcsValue + ", 0) AS UPH_STEALTH" + "\n");
                strSqlString.Append("     , ROUND(WIP_STEALTH / " + sKpcsValue + ", 0) AS WIP_STEALTH" + "\n");
                strSqlString.Append("     , ROUND(WIP_PREBG / " + sKpcsValue + ", 0) AS WIP_PREBG" + "\n");
                strSqlString.Append("     , ROUND(WIP_LAMI / " + sKpcsValue + ", 0) AS WIP_LAMI" + "\n");
                strSqlString.Append("     , ROUND(WIP_STOCK / " + sKpcsValue + ", 0) AS WIP_STOCK" + "\n");
                strSqlString.Append("     , ROUND((WIP_STOCK + WIP_LAMI + WIP_PREBG + WIP_STEALTH) / " + sKpcsValue + ", 0) AS WIP_TTL" + "\n");
                strSqlString.Append("     , CASE WHEN OPER_STEALTH = 'O' THEN ROUND((PLN_WEEK1 - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK) + WIP_STEALTH + WIP_PREBG + WIP_LAMI) / " + sKpcsValue + ", 0)" + "\n");
                strSqlString.Append("            ELSE 0" + "\n");
                strSqlString.Append("       END AS T_WEEK_STEALTH_IN_DEF" + "\n");
                strSqlString.Append("     , CASE WHEN OPER_STEALTH = 'O' THEN ROUND(((PLN_WEEK1 + PLN_WEEK2) - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_WEEK) + WIP_STEALTH + WIP_PREBG + WIP_LAMI) / " + sKpcsValue + ", 0)" + "\n");
                strSqlString.Append("            ELSE 0" + "\n");
                strSqlString.Append("       END  AS N_WEEK_STEALTH_IN_DEF" + "\n");
                strSqlString.Append("     , CASE WHEN OPER_STEALTH = 'O' THEN ROUND((PLN_MONTH - (WIP_FRONT + WIP_BACK + WIP_2GROUP + AO_MONTH) + WIP_STEALTH + WIP_PREBG + WIP_LAMI) / " + sKpcsValue + ", 0)" + "\n");
                strSqlString.Append("            ELSE 0" + "\n");
                strSqlString.Append("       END AS MONTH_STEALTH_IN_DEF" + "\n");
                strSqlString.Append("     , BG_1, WM_TYPE1, NET_DIE, COMP_CNT, WIRE_CNT" + "\n");
            }

            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT A.MAT_GRP_1" + "\n");
            strSqlString.Append("             , A.MAT_GRP_10" + "\n");
            strSqlString.Append("             , A.MAT_CMF_11" + "\n");
            strSqlString.Append("             , A.MAT_GRP_5" + "\n");
            strSqlString.Append("             , A.CUST" + "\n");
            strSqlString.Append("             , A.CHIP" + "\n");
            strSqlString.Append("             , A.NET_DIE" + "\n");
            strSqlString.Append("             , NVL(D.CAPA,0) AS CAPA" + "\n");
            strSqlString.Append("             , A.WIP_DA_WB, B.WIP_MID_MER, B.WIP_MID_MER_3, B.WIP_MID_MER_4, B.WIP_MID_MER_5" + "\n");
            strSqlString.Append("             , CASE WHEN A.MAT_GRP_5 IN ('-', '1st') THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER,0)" + "\n");
            strSqlString.Append("                    WHEN A.MAT_GRP_5 = '2nd' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER,0)" + "\n");
            strSqlString.Append("                    WHEN A.MAT_GRP_5 = '3rd' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER_3,0)" + "\n");
            strSqlString.Append("                    WHEN A.MAT_GRP_5 = '4th' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER_4,0)" + "\n");
            strSqlString.Append("                    WHEN A.MAT_GRP_5 = '5th' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER_5,0)" + "\n");
            strSqlString.Append("                    WHEN A.MAT_GRP_5 = '6th' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER_6,0)" + "\n");
            strSqlString.Append("                    WHEN A.MAT_GRP_5 = '7th' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER_7,0)" + "\n");
            strSqlString.Append("                    WHEN A.MAT_GRP_5 = '8th' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER_8,0)" + "\n");
            strSqlString.Append("                    WHEN A.MAT_GRP_5 = '9th' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER_9,0)" + "\n");
            strSqlString.Append("                    ELSE 0" + "\n");
            strSqlString.Append("               END WIP_2GROUP" + "\n");
            strSqlString.Append("             , NVL(A.WIP_SAW,0) AS WIP_SAW" + "\n");
            strSqlString.Append("             , NVL(A.Y_OUT_SAW,0) AS Y_OUT_SAW" + "\n");
            strSqlString.Append("             , NVL(A.T_OUT_SAW,0) AS T_OUT_SAW" + "\n");
            strSqlString.Append("             , NVL(A.WIP_BG,0) AS WIP_BG" + "\n");
            strSqlString.Append("             , NVL(A.Y_OUT_BG,0) AS Y_OUT_BG" + "\n");
            strSqlString.Append("             , NVL(A.T_OUT_BG,0) AS T_OUT_BG" + "\n");
            strSqlString.Append("             , NVL(A.WIP_STOCK,0) AS WIP_STOCK" + "\n");
            strSqlString.Append("             , NVL(A.T_OUT_STOCK,0) AS T_OUT_STOCK" + "\n");
            strSqlString.Append("             , NVL(E.PLN_WEEK1,0) AS PLN_WEEK1" + "\n");
            strSqlString.Append("             , NVL(E.PLN_WEEK2,0) AS PLN_WEEK2" + "\n");
            strSqlString.Append("             , NVL(E.PLN_WEEK3,0) AS PLN_WEEK3" + "\n");
            strSqlString.Append("             , NVL(E.PLN_WEEK4,0) AS PLN_WEEK4" + "\n");
            strSqlString.Append("             , NVL(E.PLN_MONTH,0) AS PLN_MONTH" + "\n");
            strSqlString.Append("             , NVL(A.WIP_FRONT,0) AS WIP_FRONT" + "\n");
            strSqlString.Append("             , NVL(B.WIP_BACK,0) AS WIP_BACK" + "\n");
            strSqlString.Append("             , NVL(C.AO_WEEK,0) AS AO_WEEK" + "\n");
            strSqlString.Append("             , NVL(C.AO_MONTH,0) AS AO_MONTH" + "\n");
            strSqlString.Append("             , NVL(A.WIP_DA,0) AS WIP_DA" + "\n");
            strSqlString.Append("             , NVL(F.WIP_MERGE,0) AS WIP_MERGE" + "\n");
            strSqlString.Append("             , NVL(G.MAIN_DA,0) AS MAIN_DA" + "\n");
            strSqlString.Append("             , NVL(G.MAIN_WB,0) AS MAIN_WB" + "\n");
            strSqlString.Append("             , A.OPER_STEALTH" + "\n");
            strSqlString.Append("             , NVL(A.UPH_STEALTH,0) AS UPH_STEALTH" + "\n");
            strSqlString.Append("             , NVL(A.WIP_LAMI,0) AS WIP_LAMI" + "\n");
            strSqlString.Append("             , NVL(A.WIP_PREBG,0) AS WIP_PREBG" + "\n");
            strSqlString.Append("             , NVL(A.WIP_STEALTH,0) AS WIP_STEALTH" + "\n");
            strSqlString.Append("             , A.BG_1" + "\n");
            strSqlString.Append("             , A.WM_TYPE1" + "\n");
            strSqlString.Append("             , A.COMP_CNT" + "\n");
            strSqlString.Append("             , A.WIRE_CNT" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT MAT_GRP_1, MAT_GRP_10, MAT_CMF_11, MAT_GRP_5, MAX(NET_DIE) AS NET_DIE" + "\n");
            strSqlString.Append("                     , MAX(CASE WHEN MAT.MAT_ID LIKE 'SEK%' THEN 'SEK'" + "\n");
            strSqlString.Append("                                WHEN MAT.MAT_ID LIKE 'SES%' THEN 'SES'" + "\n");
            strSqlString.Append("                                WHEN MAT.MAT_ID LIKE 'HX%' THEN 'HX'" + "\n");
            strSqlString.Append("                                ELSE 'ETC'" + "\n");
            strSqlString.Append("                           END) CUST" + "\n");
            strSqlString.Append("                     , MAX(CASE WHEN MAT.MAT_ID LIKE 'SEKS3%' THEN SUBSTR(MAT.MAT_ID, INSTR(MAT.MAT_ID, '-')-3, 3)" + "\n");
            strSqlString.Append("                                WHEN MAT.MAT_ID LIKE 'SEK9%' THEN 'N'" + "\n");
            strSqlString.Append("                                WHEN MAT.MAT_ID LIKE 'SEKY%' THEN 'S'" + "\n");
            strSqlString.Append("                                WHEN MAT.MAT_ID LIKE 'SEK%' THEN 'D'" + "\n");
            strSqlString.Append("                                ELSE ' '" + "\n");
            strSqlString.Append("                           END) CHIP" + "\n");            

            // 2014-08-20-임종우 : 환산 시 Comp 로직 제외하기 위해 추가
            #region 환산
            if (ckbConv.Checked == true)
            {
                strSqlString.Append("                     , SUM(WIP_STOCK) AS WIP_STOCK" + "\n");
                strSqlString.Append("                     , SUM(WIP_BG) AS WIP_BG" + "\n");
                strSqlString.Append("                     , SUM(WIP_SAW) AS WIP_SAW" + "\n");
                strSqlString.Append("                     , SUM(WIP_LAMI) AS WIP_LAMI" + "\n");
                strSqlString.Append("                     , SUM(WIP_PREBG) AS WIP_PREBG" + "\n");
                strSqlString.Append("                     , SUM(WIP_STEALTH) AS WIP_STEALTH" + "\n");
                strSqlString.Append("                     , SUM(WIP_DA_WB) AS WIP_DA_WB" + "\n");
                strSqlString.Append("                     , SUM(WIP_FRONT1) + SUM(WIP_FRONT2) AS WIP_FRONT" + "\n");                
                strSqlString.Append("                     , CASE WHEN MAT_GRP_5 = '1st' THEN SUM(WIP_DA1) + SUM(WIP_DA2)" + "\n");
                strSqlString.Append("                            ELSE SUM(WIP_DA1) + SUM(WIP_DA2) + SUM(WIP_DA3)" + "\n");
                strSqlString.Append("                       END AS WIP_DA" + "\n");
                strSqlString.Append("                     , SUM(T_OUT_STOCK) AS T_OUT_STOCK" + "\n");
                strSqlString.Append("                     , SUM(Y_OUT_BG) AS Y_OUT_BG" + "\n");
                strSqlString.Append("                     , SUM(T_OUT_BG) AS T_OUT_BG" + "\n");
                strSqlString.Append("                     , SUM(Y_OUT_SAW) AS Y_OUT_SAW" + "\n");
                strSqlString.Append("                     , SUM(T_OUT_SAW) AS T_OUT_SAW" + "\n");            
            }
            #endregion
            #region 기본
            else
            {
                strSqlString.Append("                     , SUM(WIP_STOCK / COMP_CNT) AS WIP_STOCK" + "\n");
                strSqlString.Append("                     , SUM(WIP_BG / COMP_CNT) AS WIP_BG" + "\n");
                strSqlString.Append("                     , SUM(WIP_SAW / COMP_CNT) AS WIP_SAW" + "\n");
                strSqlString.Append("                     , SUM(WIP_LAMI / COMP_CNT) AS WIP_LAMI" + "\n");
                strSqlString.Append("                     , SUM(WIP_PREBG / COMP_CNT) AS WIP_PREBG" + "\n");
                strSqlString.Append("                     , SUM(WIP_STEALTH / COMP_CNT) AS WIP_STEALTH" + "\n");
                strSqlString.Append("                     , SUM(WIP_DA_WB) AS WIP_DA_WB" + "\n");
                strSqlString.Append("                     , SUM(WIP_FRONT1 / COMP_CNT) + SUM(WIP_FRONT2) AS WIP_FRONT" + "\n");
                //strSqlString.Append("                     , SUM(WIP_DA1 / COMP_CNT) + SUM(WIP_DA2) AS WIP_DA" + "\n");
                strSqlString.Append("                     , CASE WHEN MAT_GRP_5 = '1st' THEN SUM(WIP_DA1 / COMP_CNT) + SUM(WIP_DA2)" + "\n");
                strSqlString.Append("                            ELSE SUM(WIP_DA1 / COMP_CNT) + SUM(WIP_DA2) + SUM(WIP_DA3)" + "\n");
                strSqlString.Append("                       END AS WIP_DA" + "\n");
                strSqlString.Append("                     , SUM(T_OUT_STOCK / COMP_CNT) AS T_OUT_STOCK" + "\n");
                strSqlString.Append("                     , SUM(Y_OUT_BG / COMP_CNT) AS Y_OUT_BG" + "\n");
                strSqlString.Append("                     , SUM(T_OUT_BG / COMP_CNT) AS T_OUT_BG" + "\n");
                strSqlString.Append("                     , SUM(Y_OUT_SAW / COMP_CNT) AS Y_OUT_SAW" + "\n");
                strSqlString.Append("                     , SUM(T_OUT_SAW / COMP_CNT) AS T_OUT_SAW" + "\n");              
            }
            #endregion
            strSqlString.Append("                     , MAX(OPER_STEALTH) AS OPER_STEALTH" + "\n");
            strSqlString.Append("                     , AVG(UPH_STEALTH) AS UPH_STEALTH" + "\n");
            strSqlString.Append("                     , MAX(BG_1) AS BG_1" + "\n");
            strSqlString.Append("                     , MAX(WM_TYPE1) AS WM_TYPE1" + "\n");
            strSqlString.Append("                     , MAX(COMP_CNT) AS COMP_CNT" + "\n");
            strSqlString.Append("                     , MAX(WIRE_CNT) AS WIRE_CNT" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT A.*" + "\n");
            strSqlString.Append("                             , (SELECT DECODE(OPER, 'A0033', 'O') FROM MWIPFLWOPR@RPTTOMES WHERE FACTORY = A.FACTORY AND FLOW = A.FIRST_FLOW AND OPER = 'A0033') AS OPER_STEALTH" + "\n");
            strSqlString.Append("                             , (SELECT AVG(UPEH) FROM CRASUPHDEF WHERE FACTORY = A.FACTORY AND MAT_ID = A.MAT_ID AND OPER = 'A0033') AS UPH_STEALTH" + "\n");
            strSqlString.Append("                             , B.BG_1" + "\n");
            strSqlString.Append("                             , B.WM_TYPE1" + "\n"); 
            strSqlString.Append("                             , (SELECT MAX(TCD_CMF_2) FROM CWIPTCDDEF@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND OPER LIKE 'A060%' AND SET_FLAG = 'Y' AND TCD_CMF_2 <> ' ' AND MAT_ID = A.MAT_ID) AS WIRE_CNT" + "\n");                
            strSqlString.Append("                          FROM VWIPMATDEF A" + "\n");
            strSqlString.Append("                             , CLOTCRDDAT@RPTTOMES B" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND A.FACTORY = B.FACTORY(+)" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND A.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                           AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND A.MAT_GRP_5 <> 'Merge'" + "\n");
            strSqlString.Append("                           AND A.MAT_GRP_5 NOT LIKE 'Middle%'" + "\n");

            if (cdvCust.Text == "Etc")
            {
                strSqlString.Append("                           AND A.MAT_GRP_1 NOT IN ('SE','HX') " + "\n");
            }
            else if (cdvCust.Text == "HX")
            {
                strSqlString.Append("                           AND A.MAT_ID LIKE 'HX%'" + "\n");
                strSqlString.Append("                           AND MESMGR.F_GET_ATTR_VALUE@RPTTOMES(A.MAT_ID, 'MAT_ETC', 'HX_VERSION') <> 'A-445'" + "\n");
            }
            else
            {
                strSqlString.Append("                           AND A.MAT_ID LIKE '" + cdvCust.Text + "%'" + "\n");
            }

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("                           AND A.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);
            }

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                           AND A.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                           AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                           AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                           AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                           AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                           AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                           AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                           AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                           AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion

            strSqlString.Append("                       ) MAT" + "\n");                
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID" + "\n");
            strSqlString.Append("                             , SUM(DECODE(OPER, 'A0000', " + sQty_Type + ", 0)) AS WIP_STOCK" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER BETWEEN 'A0001' AND 'A0040' THEN " + sQty_Type + " ELSE 0 END) AS WIP_BG" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER BETWEEN 'A0041' AND 'A0300' AND OPER <> 'A0250' THEN " + sQty_Type + " ELSE 0 END) AS WIP_SAW" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER BETWEEN 'A0400' AND 'A0809' THEN " + sQty_Type + " ELSE 0 END) AS WIP_DA_WB" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER BETWEEN 'A0020' AND 'A0395' THEN " + sQty_Type + " ELSE 0 END) AS WIP_FRONT1" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER BETWEEN 'A0396' AND 'A0399' THEN " + sQty_Type + " ELSE 0 END) AS WIP_FRONT2" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER BETWEEN 'A0301' AND 'A0395' OR OPER = 'A0250' THEN " + sQty_Type + " ELSE 0 END) AS WIP_DA1" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER BETWEEN 'A0396' AND 'A0401' THEN " + sQty_Type + " ELSE 0 END) AS WIP_DA2" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER BETWEEN 'A0402' AND 'A0409' THEN " + sQty_Type + " ELSE 0 END) AS WIP_DA3" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER BETWEEN 'A0020' AND 'A0029' THEN " + sQty_Type + " ELSE 0 END) AS WIP_LAMI" + "\n");
            strSqlString.Append("                             , SUM(DECODE(OPER, 'A0030', " + sQty_Type + ", 0)) AS WIP_PREBG" + "\n");
            strSqlString.Append("                             , SUM(DECODE(OPER, 'A0033', " + sQty_Type + ", 0)) AS WIP_STEALTH" + "\n");

            if (date == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                          FROM RWIPLOTSTS " + "\n");
                strSqlString.Append("                         WHERE 1=1  " + "\n");
            }
            else
            {
                strSqlString.Append("                          FROM RWIPLOTSTS_BOH " + "\n");
                strSqlString.Append("                         WHERE 1=1  " + "\n");
                strSqlString.Append("                           AND CUTOFF_DT = '" + date + "22' " + "\n");
            }

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                           AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("                       ) WIP" + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE = '" + yesterday + "' AND OPER = 'A0040' THEN END_QTY ELSE 0 END) AS Y_OUT_BG" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE = '" + date + "' AND OPER = 'A0040' THEN END_QTY ELSE 0 END) AS T_OUT_BG" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE = '" + yesterday + "' AND OPER IN ('A0200', 'A0230') THEN END_QTY ELSE 0 END) AS Y_OUT_SAW" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE = '" + date + "' AND OPER IN ('A0200', 'A0230') THEN END_QTY ELSE 0 END) AS T_OUT_SAW" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT WORK_DATE, MAT_ID, OPER, B.FLOW" + "\n");
            strSqlString.Append("                                     , CASE WHEN B.FLOW IS NOT NULL AND OPER = 'A0200' THEN 0" + "\n");
            strSqlString.Append("                                            ELSE (S1_END_" + sQty_Type + "+S2_END_" + sQty_Type + "+S3_END_" + sQty_Type + ")" + "\n");
            strSqlString.Append("                                       END AS END_QTY " + "\n");
            strSqlString.Append("                                  FROM RSUMWIPMOV A" + "\n");
            strSqlString.Append("                                     , (" + "\n");
            strSqlString.Append("                                        SELECT FLOW, COUNT(*) AS CNT" + "\n");
            strSqlString.Append("                                          FROM MWIPFLWOPR@RPTTOMES" + "\n");
            strSqlString.Append("                                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                           AND OPER IN ('A0200', 'A0230')" + "\n");
            strSqlString.Append("                                         GROUP BY FLOW" + "\n");
            strSqlString.Append("                                        HAVING COUNT(*) > 1" + "\n");
            strSqlString.Append("                                       ) B" + "\n");
            strSqlString.Append("                                 WHERE 1=1" + "\n");
            strSqlString.Append("                                   AND A.FLOW = B.FLOW(+)" + "\n");
            strSqlString.Append("                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                   AND WORK_DATE IN ('" + yesterday + "', '" + date + "')" + "\n");
            strSqlString.Append("                                   AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                                   AND OPER IN ('A0040', 'A0200', 'A0230')" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                               )" + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID" + "\n");            
            strSqlString.Append("                       ) SHP   " + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID" + "\n");
            strSqlString.Append("                             , SUM(RCV_" + sQty_Type + ") AS T_OUT_STOCK" + "\n");
            strSqlString.Append("                          FROM VSUMWIPRCV " + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND WORK_DATE = '" + date + "'" + "\n");
            strSqlString.Append("                           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND CM_KEY_2 = 'PROD'   " + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                           AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("                       ) RCV " + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");                
            strSqlString.Append("                   AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");
            strSqlString.Append("                   AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
            strSqlString.Append("                   AND MAT.MAT_ID = RCV.MAT_ID(+)" + "\n");
            strSqlString.Append("                 GROUP BY MAT_GRP_1, MAT_GRP_10, MAT_CMF_11, MAT_GRP_5" + "\n");
            strSqlString.Append("               ) A" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT A.MAT_GRP_1, A.MAT_GRP_10, A.MAT_CMF_11 " + "\n");
            strSqlString.Append("                     , MAX(CASE WHEN A.MAT_ID LIKE 'SEK%' THEN 'SEK'" + "\n");
            strSqlString.Append("                                WHEN A.MAT_ID LIKE 'SES%' THEN 'SES'" + "\n");
            strSqlString.Append("                                WHEN A.MAT_ID LIKE 'HX%' THEN 'HX'" + "\n");
            strSqlString.Append("                                ELSE 'ETC'" + "\n");
            strSqlString.Append("                           END) CUST" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER BETWEEN 'A0400' AND 'A0609' AND MAT_GRP_5 NOT IN ('-') THEN " + sQty_Type + " ELSE 0 END) AS WIP_MID_MER" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER BETWEEN 'A0400' AND 'A0609' AND MAT_GRP_5 NOT IN ('-', 'Middle') THEN " + sQty_Type + " ELSE 0 END) AS WIP_MID_MER_3" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER BETWEEN 'A0400' AND 'A0609' AND MAT_GRP_5 NOT IN ('-', 'Middle', 'Middle 1') THEN " + sQty_Type + " ELSE 0 END) AS WIP_MID_MER_4" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER BETWEEN 'A0400' AND 'A0609' AND MAT_GRP_5 NOT IN ('-', 'Middle', 'Middle 1', 'Middle 2') THEN " + sQty_Type + " ELSE 0 END) AS WIP_MID_MER_5" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER BETWEEN 'A0400' AND 'A0609' AND MAT_GRP_5 NOT IN ('-', 'Middle', 'Middle 1', 'Middle 2', 'Middle 3') THEN " + sQty_Type + " ELSE 0 END) AS WIP_MID_MER_6" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER BETWEEN 'A0400' AND 'A0609' AND MAT_GRP_5 NOT IN ('-', 'Middle', 'Middle 1', 'Middle 2', 'Middle 3', 'Middle 4') THEN " + sQty_Type + " ELSE 0 END) AS WIP_MID_MER_7" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER BETWEEN 'A0400' AND 'A0609' AND MAT_GRP_5 NOT IN ('-', 'Middle', 'Middle 1', 'Middle 2', 'Middle 3', 'Middle 4', 'Middle 5') THEN " + sQty_Type + " ELSE 0 END) AS WIP_MID_MER_8" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER BETWEEN 'A0400' AND 'A0609' AND MAT_GRP_5 NOT IN ('-', 'Middle', 'Middle 1', 'Middle 2', 'Middle 3', 'Middle 4', 'Middle 5', 'Middle 6') THEN " + sQty_Type + " ELSE 0 END) AS WIP_MID_MER_9" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER BETWEEN 'A0400' AND 'A0609' THEN 0" + "\n");            
            strSqlString.Append("                                WHEN OPER = 'A0800' THEN 0" + "\n");            
            //strSqlString.Append("                                WHEN OPER LIKE 'A08%' AND SUBSTR(MAT_GRP_4, -1) = SUBSTR(OPER, -1) THEN " + sQty_Type + "" + "\n");
            strSqlString.Append("                                WHEN OPER BETWEEN 'A0801' AND 'A0809' AND SUBSTR(MAT_GRP_4, -1) = SUBSTR(OPER, -1) THEN " + sQty_Type + "" + "\n");
            strSqlString.Append("                                ELSE " + sQty_Type + "" + "\n");
            strSqlString.Append("                           END) AS WIP_BACK" + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF A" + "\n");

            if (date == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                     , RWIPLOTSTS B" + "\n");
                strSqlString.Append("                 WHERE 1=1  " + "\n");
            }
            else
            {
                strSqlString.Append("                     , RWIPLOTSTS_BOH B" + "\n");
                strSqlString.Append("                 WHERE 1=1  " + "\n");
                strSqlString.Append("                   AND B.CUTOFF_DT = '" + date + "22' " + "\n");
            }

            strSqlString.Append("                   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                   AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND B.OPER BETWEEN 'A0400' AND 'AZ010'" + "\n");
            strSqlString.Append("                   AND B.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                   AND B.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND REGEXP_LIKE(A.MAT_GRP_5, 'Middle*|Merge|-')" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND B.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                 GROUP BY A.MAT_GRP_1, A.MAT_GRP_10, A.MAT_CMF_11 " + "\n");
            strSqlString.Append("               ) B" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT A.MAT_GRP_1, A.MAT_GRP_10, A.MAT_CMF_11 " + "\n");
            strSqlString.Append("                     , MAX(CASE WHEN A.MAT_ID LIKE 'SEK%' THEN 'SEK'" + "\n");
            strSqlString.Append("                                WHEN A.MAT_ID LIKE 'SES%' THEN 'SES'" + "\n");
            strSqlString.Append("                                WHEN A.MAT_ID LIKE 'HX%' THEN 'HX'" + "\n");
            strSqlString.Append("                                ELSE 'ETC'" + "\n");
            strSqlString.Append("                           END) CUST" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + date + "' THEN (S1_FAC_OUT_" + sQty_Type + "+S2_FAC_OUT_" + sQty_Type + "+S3_FAC_OUT_" + sQty_Type + ") ELSE 0 END) AS AO_WEEK" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "' THEN (S1_FAC_OUT_" + sQty_Type + "+S2_FAC_OUT_" + sQty_Type + "+S3_FAC_OUT_" + sQty_Type + ") ELSE 0 END) AS AO_MONTH" + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF A" + "\n");
            strSqlString.Append("                     , RSUMFACMOV B" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.CM_KEY_1" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID" + "\n");

            if (Convert.ToInt32(FindWeek.StartDay_ThisWeek) < Convert.ToInt32(start_date))
            {
                strSqlString.Append("                   AND B.WORK_DATE BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + date + "'" + "\n");
            }
            else
            {
                strSqlString.Append("                   AND B.WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "'" + "\n");
            }

            strSqlString.Append("                   AND B.FACTORY <> 'RETURN'" + "\n");
            strSqlString.Append("                   AND B.CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND B.LOT_TYPE = 'W'   " + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND B.CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                   AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                 GROUP BY A.MAT_GRP_1, A.MAT_GRP_10, A.MAT_CMF_11 " + "\n");
            strSqlString.Append("               ) C" + "\n");
            strSqlString.Append("             , (                " + "\n");
            strSqlString.Append("                SELECT MAT_GRP_1, MAT_GRP_10, MAT_CMF_11" + "\n");
            strSqlString.Append("                     , MAX(CASE WHEN RES.MAT_ID LIKE 'SEK%' THEN 'SEK'" + "\n");
            strSqlString.Append("                                WHEN RES.MAT_ID LIKE 'SES%' THEN 'SES'" + "\n");
            strSqlString.Append("                                WHEN RES.MAT_ID LIKE 'HX%' THEN 'HX'" + "\n");
            strSqlString.Append("                                ELSE 'ETC'" + "\n");
            strSqlString.Append("                           END) CUST" + "\n");
            strSqlString.Append("                     , STACK" + "\n");
            strSqlString.Append("                     , SUM(TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.7 * RES.RAS_CNT, 0))) AS CAPA" + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT B.FACTORY, B.RES_STS_2 AS MAT_ID" + "\n");
            strSqlString.Append("                             , A.MAT_GRP_1, A.MAT_GRP_10, A.MAT_CMF_11" + "\n");
            strSqlString.Append("                             , B.RES_STS_8 AS OPER" + "\n");
            strSqlString.Append("                             , CASE WHEN B.RES_STS_8 IN ('A0400','A0401') THEN '1st'" + "\n");
            strSqlString.Append("                                    WHEN B.RES_STS_8 = 'A0402' THEN '2nd'" + "\n");
            strSqlString.Append("                                    WHEN B.RES_STS_8 = 'A0403' THEN '3rd'" + "\n");
            strSqlString.Append("                                    WHEN B.RES_STS_8 = 'A0404' THEN '4th'" + "\n");
            strSqlString.Append("                                    WHEN B.RES_STS_8 = 'A0405' THEN '5th'" + "\n");
            strSqlString.Append("                                    WHEN B.RES_STS_8 = 'A0406' THEN '6th'" + "\n");
            strSqlString.Append("                                    WHEN B.RES_STS_8 = 'A0407' THEN '7th'" + "\n");
            strSqlString.Append("                                    WHEN B.RES_STS_8 = 'A0408' THEN '8th'" + "\n");
            strSqlString.Append("                                    WHEN B.RES_STS_8 = 'A0409' THEN '9th'" + "\n");
            strSqlString.Append("                                    ELSE ''" + "\n");
            strSqlString.Append("                               END STACK" + "\n");
            strSqlString.Append("                             , B.RES_GRP_6 AS RES_MODEL, B.RES_GRP_7 AS UPEH_GRP, COUNT(B.RES_ID) AS RAS_CNT " + "\n");

            if (date == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                          FROM MWIPMATDEF A" + "\n");
                strSqlString.Append("                             , MRASRESDEF B  " + "\n");
                strSqlString.Append("                         WHERE 1=1  " + "\n");
            }
            else
            {
                strSqlString.Append("                          FROM MWIPMATDEF A" + "\n");
                strSqlString.Append("                             , MRASRESDEF_BOH B  " + "\n");
                strSqlString.Append("                         WHERE 1=1  " + "\n");
                strSqlString.Append("                           AND B.CUTOFF_DT = '" + date + "22' " + "\n");
            }

            strSqlString.Append("                           AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.RES_STS_2 " + "\n");
            strSqlString.Append("                           AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND B.RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("                           AND B.RES_CMF_7 = 'Y' " + "\n");
            strSqlString.Append("                           AND B.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND B.RES_STS_8 LIKE 'A04%'" + "\n");
            strSqlString.Append("                         GROUP BY B.FACTORY, B.RES_STS_2, A.MAT_GRP_1, A.MAT_GRP_10, A.MAT_CMF_11, B.RES_STS_8, B.RES_GRP_6, B.RES_GRP_7 " + "\n");
            strSqlString.Append("                       ) RES " + "\n");
            strSqlString.Append("                     , CRASUPHDEF UPH" + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND RES.FACTORY = UPH.FACTORY(+) " + "\n");
            strSqlString.Append("                   AND RES.OPER = UPH.OPER(+) " + "\n");
            strSqlString.Append("                   AND RES.RES_MODEL = UPH.RES_MODEL(+) " + "\n");
            strSqlString.Append("                   AND RES.UPEH_GRP = UPH.UPEH_GRP(+) " + "\n");
            strSqlString.Append("                   AND RES.MAT_ID = UPH.MAT_ID(+)" + "\n");
            strSqlString.Append("                 GROUP BY MAT_GRP_1, MAT_GRP_10, MAT_CMF_11, STACK" + "\n");
            strSqlString.Append("               ) D" + "\n");
            strSqlString.Append("             , (  " + "\n");
            strSqlString.Append("                SELECT A.MAT_GRP_1, A.MAT_GRP_10, A.MAT_CMF_11" + "\n");
            strSqlString.Append("                     , MAX(CASE WHEN A.MAT_ID LIKE 'SEK%' THEN 'SEK'" + "\n");
            strSqlString.Append("                                WHEN A.MAT_ID LIKE 'SES%' THEN 'SES'" + "\n");
            strSqlString.Append("                                WHEN A.MAT_ID LIKE 'HX%' THEN 'HX'" + "\n");
            strSqlString.Append("                                ELSE 'ETC'" + "\n");
            strSqlString.Append("                           END) CUST" + "\n");

            //if (ckbConv.Checked == true)
            //{
            //    strSqlString.Append("                     , SUM(PLN_WEEK1 / (TO_NUMBER(DECODE(MAT_CMF_13,' ',1,'-',1,MAT_CMF_13)) * 0.85)) AS PLN_WEEK1" + "\n");
            //    strSqlString.Append("                     , SUM(PLN_WEEK2 / (TO_NUMBER(DECODE(MAT_CMF_13,' ',1,'-',1,MAT_CMF_13)) * 0.85)) AS PLN_WEEK2" + "\n");
            //    strSqlString.Append("                     , SUM(PLN_MONTH / (TO_NUMBER(DECODE(MAT_CMF_13,' ',1,'-',1,MAT_CMF_13)) * 0.85)) AS PLN_MONTH" + "\n");
            //}
            //else
            //{
                strSqlString.Append("                     , SUM(PLN_WEEK1) AS PLN_WEEK1" + "\n");
                strSqlString.Append("                     , SUM(PLN_WEEK2) AS PLN_WEEK2" + "\n");
                strSqlString.Append("                     , SUM(PLN_WEEK3) AS PLN_WEEK3" + "\n");
                strSqlString.Append("                     , SUM(PLN_WEEK4) AS PLN_WEEK4" + "\n");
                strSqlString.Append("                     , SUM(PLN_MONTH) AS PLN_MONTH" + "\n");
            //}

            strSqlString.Append("                  FROM MWIPMATDEF A " + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID" + "\n");
            strSqlString.Append("                             , DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', WW_QTY, 0) AS PLN_WEEK1" + "\n");
            strSqlString.Append("                             , DECODE(PLAN_WEEK, '" + FindWeek.NextWeek + "', WW_QTY, 0) AS PLN_WEEK2" + "\n");
            strSqlString.Append("                             , DECODE(PLAN_WEEK, '" + FindWeek.W2_Week + "', WW_QTY, 0) AS PLN_WEEK3" + "\n");
            strSqlString.Append("                             , DECODE(PLAN_WEEK, '" + FindWeek.W3_Week + "', WW_QTY, 0) AS PLN_WEEK4" + "\n");
            strSqlString.Append("                             , 0 AS PLN_MONTH" + "\n");
            strSqlString.Append("                          FROM RWIPPLNWEK" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND PLAN_WEEK IN ('" + FindWeek.ThisWeek + "','" + FindWeek.NextWeek + "','" + FindWeek.W2_Week + "','" + FindWeek.W3_Week + "')" + "\n");
            strSqlString.Append("                           AND GUBUN = '3' " + "\n");
            strSqlString.Append("                         UNION ALL" + "\n");
            strSqlString.Append("                        SELECT MAT_ID" + "\n");
            strSqlString.Append("                             , 0 AS PLN_WEEK1, 0 AS PLN_WEEK2, 0 AS PLN_WEEK3, 0 AS PLN_WEEK4" + "\n");
            strSqlString.Append("                             , TO_NUMBER(RESV_FIELD1) AS PLN_MONTH" + "\n");
            strSqlString.Append("                          FROM CWIPPLNMON" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND PLAN_MONTH = '" + month + "'" + "\n");
            strSqlString.Append("                           AND DECODE(RESV_FIELD1, ' ', 0, RESV_FIELD1) > 0" + "\n");
            strSqlString.Append("                       ) B" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                 GROUP BY A.MAT_GRP_1, A.MAT_GRP_10, A.MAT_CMF_11" + "\n");
            strSqlString.Append("               ) E" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT A.MAT_GRP_1, A.MAT_GRP_10, A.MAT_CMF_11 " + "\n");
            strSqlString.Append("                     , MAX(CASE WHEN A.MAT_ID LIKE 'SEK%' THEN 'SEK'" + "\n");
            strSqlString.Append("                                WHEN A.MAT_ID LIKE 'SES%' THEN 'SES'" + "\n");
            strSqlString.Append("                                WHEN A.MAT_ID LIKE 'HX%' THEN 'HX'" + "\n");
            strSqlString.Append("                                ELSE 'ETC'" + "\n");
            strSqlString.Append("                           END) CUST" + "\n");
            strSqlString.Append("                     , CASE WHEN OPER = 'A0402' THEN '2nd'" + "\n");
            strSqlString.Append("                            WHEN OPER = 'A0403' THEN '3rd'" + "\n");
            strSqlString.Append("                            WHEN OPER = 'A0404' THEN '4th'" + "\n");
            strSqlString.Append("                            WHEN OPER = 'A0405' THEN '5th'" + "\n");
            strSqlString.Append("                            WHEN OPER = 'A0406' THEN '6th'" + "\n");
            strSqlString.Append("                            WHEN OPER = 'A0407' THEN '7th'" + "\n");
            strSqlString.Append("                            WHEN OPER = 'A0408' THEN '8th'" + "\n");
            strSqlString.Append("                            WHEN OPER = 'A0409' THEN '9th'" + "\n");
            strSqlString.Append("                            ELSE ''" + "\n");
            strSqlString.Append("                       END MAT_GRP_5" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0402' AND MAT_GRP_5 IN ('Middle','Merge') THEN " + sQty_Type + "\n");
            strSqlString.Append("                                WHEN OPER = 'A0403' AND MAT_GRP_5 IN ('Middle 1','Merge') THEN " + sQty_Type + "\n");
            strSqlString.Append("                                WHEN OPER = 'A0404' AND MAT_GRP_5 IN ('Middle 2','Merge') THEN " + sQty_Type + "\n");
            strSqlString.Append("                                WHEN OPER = 'A0405' AND MAT_GRP_5 IN ('Middle 3','Merge') THEN " + sQty_Type + "\n");
            strSqlString.Append("                                WHEN OPER = 'A0406' AND MAT_GRP_5 IN ('Middle 4','Merge') THEN " + sQty_Type + "\n");
            strSqlString.Append("                                WHEN OPER = 'A0407' AND MAT_GRP_5 IN ('Middle 5','Merge') THEN " + sQty_Type + "\n");
            strSqlString.Append("                                WHEN OPER = 'A0408' AND MAT_GRP_5 IN ('Middle 6','Merge') THEN " + sQty_Type + "\n");
            strSqlString.Append("                                WHEN OPER = 'A0409' AND MAT_GRP_5 IN ('Middle 7','Merge') THEN " + sQty_Type + "\n");
            strSqlString.Append("                                ELSE 0 " + "\n");
            strSqlString.Append("                           END) AS WIP_MERGE " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF A" + "\n");

            if (date == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                     , RWIPLOTSTS B" + "\n");
                strSqlString.Append("                 WHERE 1=1  " + "\n");
            }
            else
            {
                strSqlString.Append("                     , RWIPLOTSTS_BOH B" + "\n");
                strSqlString.Append("                 WHERE 1=1  " + "\n");
                strSqlString.Append("                   AND B.CUTOFF_DT = '" + date + "22' " + "\n");
            }

            strSqlString.Append("                   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                   AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND B.OPER BETWEEN 'A0402' AND 'A0409'" + "\n");
            strSqlString.Append("                   AND B.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                   AND B.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND REGEXP_LIKE(A.MAT_GRP_5, 'Middle*|Merge')" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND B.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                 GROUP BY A.MAT_GRP_1, A.MAT_GRP_10, A.MAT_CMF_11, B.OPER " + "\n");
            strSqlString.Append("               ) F" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_GRP_1, MAT_GRP_10, MAT_CMF_11, CUST, MAT_GRP_5 " + "\n");
            strSqlString.Append("                     , SUM(MAIN_DA) AS MAIN_DA" + "\n");
            strSqlString.Append("                     , SUM(MAIN_WB) AS MAIN_WB" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT B.MAT_GRP_1, B.MAT_GRP_10, B.MAT_CMF_11, OPER" + "\n");
            strSqlString.Append("                             , CASE WHEN A.MAT_ID LIKE 'SEK%' THEN 'SEK'" + "\n");
            strSqlString.Append("                                    WHEN A.MAT_ID LIKE 'SES%' THEN 'SES'" + "\n");
            strSqlString.Append("                                    WHEN A.MAT_ID LIKE 'HX%' THEN 'HX'" + "\n");
            strSqlString.Append("                                    ELSE 'ETC'" + "\n");
            strSqlString.Append("                               END CUST" + "\n");
            strSqlString.Append("                             , CASE WHEN OPER IN ('A0400', 'A0401', 'A0250', 'A0333', 'A0340', 'A0370', 'A0380', 'A0310') THEN '1st' " + "\n");
            strSqlString.Append("                                    WHEN OPER IN ('A0550', 'A0551', 'A0600','A0601', 'A0500', 'A0501', 'A0530', 'A0531') THEN '1st' " + "\n");
            strSqlString.Append("                                    WHEN OPER IN ('A0402') THEN '2nd' " + "\n");
            strSqlString.Append("                                    WHEN OPER = 'A0801' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN '2nd' " + "\n");
            strSqlString.Append("                                    WHEN OPER = 'A0801' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN '2nd' " + "\n");
            strSqlString.Append("                                    WHEN OPER IN ('A0552', 'A0602', 'A0502', 'A0532') THEN '2nd' " + "\n");
            strSqlString.Append("                                    WHEN OPER IN ('A0403') THEN '3rd' " + "\n");
            strSqlString.Append("                                    WHEN OPER = 'A0802' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN '3rd' " + "\n");
            strSqlString.Append("                                    WHEN OPER = 'A0802' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN '3rd' " + "\n");
            strSqlString.Append("                                    WHEN OPER IN ('A0553', 'A0603', 'A0503', 'A0533') THEN '3rd' " + "\n");
            strSqlString.Append("                                    WHEN OPER IN ('A0404') THEN '4th' " + "\n");
            strSqlString.Append("                                    WHEN OPER = 'A0803' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN '4th' " + "\n");
            strSqlString.Append("                                    WHEN OPER = 'A0803' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN '4th' " + "\n");
            strSqlString.Append("                                    WHEN OPER IN ('A0554', 'A0604', 'A0504', 'A0534') THEN '4th' " + "\n");
            strSqlString.Append("                                    WHEN OPER IN ('A0405') THEN '5th' " + "\n");
            strSqlString.Append("                                    WHEN OPER = 'A0804' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN '5th' " + "\n");
            strSqlString.Append("                                    WHEN OPER = 'A0804' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN '5th'" + "\n");
            strSqlString.Append("                                    WHEN OPER IN ('A0555', 'A0605', 'A0505', 'A0535') THEN '5th'" + "\n");
            strSqlString.Append("                                    WHEN OPER IN ('A0406') THEN '6th' " + "\n");
            strSqlString.Append("                                    WHEN OPER = 'A0805' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN '6th' " + "\n");
            strSqlString.Append("                                    WHEN OPER = 'A0805' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN '6th'" + "\n");
            strSqlString.Append("                                    WHEN OPER IN ('A0556', 'A0606', 'A0506', 'A0536') THEN '6th'" + "\n");
            strSqlString.Append("                                    WHEN OPER IN ('A0407') THEN '7th' " + "\n");
            strSqlString.Append("                                    WHEN OPER = 'A0806' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN '7th' " + "\n");
            strSqlString.Append("                                    WHEN OPER = 'A0806' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN '7th'" + "\n");
            strSqlString.Append("                                    WHEN OPER IN ('A0557', 'A0607', 'A0507', 'A0537') THEN '7th'" + "\n");
            strSqlString.Append("                                    WHEN OPER IN ('A0408') THEN '8th' " + "\n");
            strSqlString.Append("                                    WHEN OPER = 'A0807' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN '8th' " + "\n");
            strSqlString.Append("                                    WHEN OPER = 'A0807' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN '8th'" + "\n");
            strSqlString.Append("                                    WHEN OPER IN ('A0558', 'A0608', 'A0508', 'A0538') THEN '8th'" + "\n");
            strSqlString.Append("                                    WHEN OPER IN ('A0409') THEN '9th' " + "\n");
            strSqlString.Append("                                    WHEN OPER = 'A0808' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN '9th' " + "\n");
            strSqlString.Append("                                    WHEN OPER = 'A0808' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN '9th'" + "\n");
            strSqlString.Append("                                    WHEN OPER IN ('A0559', 'A0609', 'A0509', 'A0539') THEN '9th'" + "\n");
            strSqlString.Append("                                    ELSE ' ' " + "\n");
            strSqlString.Append("                                END MAT_GRP_5 " + "\n");
            strSqlString.Append("                             , CASE WHEN OPER NOT LIKE 'A05%' AND OPER NOT LIKE 'A06%' THEN (" + "\n");
            strSqlString.Append("                                         CASE WHEN MAT_GRP_1 = 'HX' AND HX_COMP_MIN IS NOT NULL" + "\n");
            strSqlString.Append("                                                   THEN (CASE WHEN HX_COMP_MIN <> HX_COMP_MAX AND OPER > HX_COMP_MIN AND OPER <= HX_COMP_MAX THEN QTY_1 / NVL(COMP_CNT / 2,1)" + "\n");
            strSqlString.Append("                                                              WHEN OPER <= HX_COMP_MAX THEN QTY_1 / NVL(COMP_CNT,1)" + "\n");
            strSqlString.Append("                                                              ELSE QTY_1 END)" + "\n");
            strSqlString.Append("                                              WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1) " + "\n");
            strSqlString.Append("                                              ELSE QTY_1 " + "\n");
            strSqlString.Append("                                         END " + "\n");
            strSqlString.Append("                                        ) " + "\n");
            strSqlString.Append("                                    ELSE 0 " + "\n");
            strSqlString.Append("                               END AS MAIN_DA " + "\n");
            strSqlString.Append("                             , CASE WHEN OPER LIKE 'A05%' OR OPER LIKE 'A06%' THEN QTY_1 ELSE 0 END AS MAIN_WB " + "\n");

            if (date == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                          FROM RWIPLOTSTS A" + "\n");
                strSqlString.Append("                             , VWIPMATDEF B " + "\n");
                strSqlString.Append("                         WHERE 1=1  " + "\n");
            }
            else
            {
                strSqlString.Append("                          FROM RWIPLOTSTS_BOH A" + "\n");
                strSqlString.Append("                             , VWIPMATDEF B " + "\n");
                strSqlString.Append("                         WHERE 1=1  " + "\n");
                strSqlString.Append("                           AND A.CUTOFF_DT = '" + date + "22' " + "\n");
            }

            strSqlString.Append("                           AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND A.LOT_TYPE = 'W'  " + "\n");
            strSqlString.Append("                           AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
            strSqlString.Append("                           AND A.LOT_DEL_FLAG = ' '  " + "\n");
            strSqlString.Append("                           AND B.MAT_GRP_2 <> '-' " + "\n");
            strSqlString.Append("                           AND B.DELETE_FLAG = ' '  " + "\n");
            strSqlString.Append("                           AND REGEXP_LIKE(B.MAT_GRP_5, '1st|Middle*|Merge') " + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                           AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                       ) " + "\n");
            strSqlString.Append("                 WHERE MAT_GRP_5 <> ' '" + "\n");
            strSqlString.Append("                 GROUP BY MAT_GRP_1, MAT_GRP_10, MAT_CMF_11, CUST, MAT_GRP_5" + "\n");
            strSqlString.Append("               ) G" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND A.MAT_GRP_1 = B.MAT_GRP_1(+)" + "\n");
            strSqlString.Append("           AND A.MAT_GRP_1 = C.MAT_GRP_1(+)" + "\n");
            strSqlString.Append("           AND A.MAT_GRP_1 = D.MAT_GRP_1(+)" + "\n");
            strSqlString.Append("           AND A.MAT_GRP_1 = E.MAT_GRP_1(+)" + "\n");
            strSqlString.Append("           AND A.MAT_GRP_1 = F.MAT_GRP_1(+)" + "\n");
            strSqlString.Append("           AND A.MAT_GRP_1 = G.MAT_GRP_1(+)" + "\n");
            strSqlString.Append("           AND A.MAT_GRP_10 = B.MAT_GRP_10(+)" + "\n");
            strSqlString.Append("           AND A.MAT_GRP_10 = C.MAT_GRP_10(+)" + "\n");
            strSqlString.Append("           AND A.MAT_GRP_10 = D.MAT_GRP_10(+)" + "\n");
            strSqlString.Append("           AND A.MAT_GRP_10 = E.MAT_GRP_10(+)" + "\n");
            strSqlString.Append("           AND A.MAT_GRP_10 = F.MAT_GRP_10(+)" + "\n");
            strSqlString.Append("           AND A.MAT_GRP_10 = G.MAT_GRP_10(+)" + "\n");
            strSqlString.Append("           AND A.MAT_CMF_11 = B.MAT_CMF_11(+)" + "\n");
            strSqlString.Append("           AND A.MAT_CMF_11 = C.MAT_CMF_11(+)" + "\n");
            strSqlString.Append("           AND A.MAT_CMF_11 = D.MAT_CMF_11(+)" + "\n");
            strSqlString.Append("           AND A.MAT_CMF_11 = E.MAT_CMF_11(+)" + "\n");
            strSqlString.Append("           AND A.MAT_CMF_11 = F.MAT_CMF_11(+)" + "\n");
            strSqlString.Append("           AND A.MAT_CMF_11 = G.MAT_CMF_11(+)" + "\n");
            strSqlString.Append("           AND A.CUST = B.CUST(+)" + "\n");
            strSqlString.Append("           AND A.CUST = C.CUST(+)" + "\n");
            strSqlString.Append("           AND A.CUST = D.CUST(+)" + "\n");
            strSqlString.Append("           AND A.CUST = E.CUST(+)" + "\n");
            strSqlString.Append("           AND A.CUST = F.CUST(+)" + "\n");
            strSqlString.Append("           AND A.CUST = G.CUST(+)" + "\n");
            strSqlString.Append("           AND A.MAT_GRP_5 = D.STACK(+)" + "\n");
            strSqlString.Append("           AND A.MAT_GRP_5 = F.MAT_GRP_5(+)" + "\n");
            strSqlString.Append("           AND A.MAT_GRP_5 = G.MAT_GRP_5(+)" + "\n");

            if (cdvCust.Text != "Etc")
            {
                strSqlString.Append("           AND A.CUST = '" + cdvCust.Text + "'" + "\n");
            }
            else
            {
                strSqlString.Append("           AND A.CUST NOT IN ('SEK','SES','HX')" + "\n");
            }

            strSqlString.Append("           AND NVL(D.CAPA,0) + NVL(B.WIP_MID_MER,0) + NVL(E.PLN_WEEK1,0) + NVL(E.PLN_WEEK2,0) + NVL(E.PLN_WEEK3,0) + NVL(E.PLN_WEEK4,0) + NVL(E.PLN_MONTH,0) +" + "\n");
            strSqlString.Append("               NVL(WIP_STOCK,0) + NVL(WIP_BG,0) + NVL(WIP_SAW,0) + NVL(WIP_LAMI,0) + NVL(WIP_PREBG,0) + NVL(WIP_STEALTH,0) + NVL(WIP_DA_WB,0) + NVL(WIP_FRONT,0) + NVL(B.WIP_BACK,0) +" + "\n");
            strSqlString.Append("               NVL(T_OUT_STOCK,0) + NVL(Y_OUT_BG,0) + NVL(T_OUT_BG,0) + NVL(Y_OUT_SAW,0) + NVL(T_OUT_SAW,0) + NVL(C.AO_WEEK,0) + NVL(C.AO_MONTH,0) + NVL(G.MAIN_DA,0) + NVL(G.MAIN_WB,0) > 0" + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" ORDER BY CUST, MAT_GRP_1, MAT_GRP_10, MAT_CMF_11, MAT_GRP_5" + "\n");            

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
            
            return strSqlString.ToString();
        }

        private string MakeSqlDetail(string sCust, string sPkg)
        {
            StringBuilder strSqlString = new StringBuilder();
            string sKpcsValue;
            string sQty_Type;


            if (ckbConv.Checked == true)
            {
                sQty_Type = "QTY_1";
            }
            else
            {
                sQty_Type = "QTY_1";
            }

            if (ckbKpcs.Checked == true && ckbConv.Checked == false)
            {
                sKpcsValue = "1000";
            }
            else
            {
                sKpcsValue = "1";
            }

            strSqlString.Append("WITH DT AS" + "\n");
            strSqlString.Append("(" + "\n");
            strSqlString.Append("SELECT DISTINCT DECODE(B.MAT_KEY, NULL, A.MAT_ID, B.MAT_KEY) AS MAT_KEY" + "\n");
            strSqlString.Append("     , A.MAT_ID, A.MAT_GRP_10, A.MAT_GRP_4, A.MAT_GRP_5, TO_NUMBER(DECODE(MAT_CMF_13,' ',1,'-',1,MAT_CMF_13)) AS NET_DIE" + "\n");
            strSqlString.Append("  FROM MWIPMATDEF A" + "\n");
            strSqlString.Append("     , HRTDMCPROUT@RPTTOMES B" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("   AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("   AND A.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("   AND A.MAT_GRP_1 = '" + sCust + "'" + "\n");
            strSqlString.Append("   AND A.MAT_CMF_11 = '" + sPkg + "'" + "\n");
            strSqlString.Append("   AND MESMGR.F_GET_ATTR_VALUE@RPTTOMES(A.MAT_ID, 'MAT_ETC', 'HX_VERSION') <> 'A-445'" + "\n");
            strSqlString.Append(")" + "\n");

            if (ckbConv.Checked == true)
            {
                strSqlString.Append("SELECT MAT_KEY, MAT_ID, MAT_GRP_5 " + "\n");
                strSqlString.Append("     , ROUND(WIP_DA / (NET_DIE * 0.9), 0) AS WIP_DA" + "\n");
                strSqlString.Append("     , ROUND(WIP_MERGE / (NET_DIE * 0.9), 0) AS WIP_MERGE" + "\n");
                strSqlString.Append("     , ROUND((NVL(WIP_DA,0) + NVL(WIP_MERGE,0)) / (NET_DIE * 0.9), 0) AS TTL" + "\n");
                strSqlString.Append("     , ROUND(WIP_2GROUP / (NET_DIE * 0.9), 0) AS WIP_2GROUP" + "\n");
                strSqlString.Append("     , ROUND((WIP_2GROUP - MAX(WIP_2GROUP) OVER(PARTITION BY MAT_KEY)) / (NET_DIE * 0.9), 0) AS DA_WIP_KIT" + "\n");
                strSqlString.Append("     , ROUND(WIP_TTL / (NET_DIE * 0.9), 0) AS WIP_TTL" + "\n");
                strSqlString.Append("     , ROUND(UV / (NET_DIE * 0.9), 0) AS UV" + "\n");
                strSqlString.Append("     , ROUND(GATE / (NET_DIE * 0.9), 0) AS GATE" + "\n");
                strSqlString.Append("     , ROUND(DDS / (NET_DIE * 0.9), 0) AS DDS" + "\n");
                strSqlString.Append("     , ROUND(SAW / (NET_DIE * 0.9), 0) AS SAW" + "\n");
                strSqlString.Append("     , ROUND(BG / (NET_DIE * 0.9), 0) AS BG" + "\n");
                strSqlString.Append("     , ROUND(STEALTH / (NET_DIE * 0.9), 0) AS STEALTH" + "\n");
                strSqlString.Append("     , ROUND(PRE_GR / (NET_DIE * 0.9), 0) AS PRE_GR" + "\n");
                strSqlString.Append("     , ROUND(LAMI / (NET_DIE * 0.9), 0) AS LAMI" + "\n");
                strSqlString.Append("     , ROUND(STOCK / (NET_DIE * 0.9), 0) AS STOCK" + "\n");
            }
            else
            {            
                strSqlString.Append("SELECT MAT_KEY, MAT_ID, MAT_GRP_5 " + "\n");
                strSqlString.Append("     , ROUND(WIP_DA / " + sKpcsValue + ", 0) AS WIP_DA" + "\n");
                strSqlString.Append("     , ROUND(WIP_MERGE / " + sKpcsValue + ", 0) AS WIP_MERGE" + "\n");
                strSqlString.Append("     , ROUND((NVL(WIP_DA,0) + NVL(WIP_MERGE,0)) / " + sKpcsValue + ", 0) AS TTL" + "\n");
                strSqlString.Append("     , ROUND(WIP_2GROUP / " + sKpcsValue + ", 0) AS WIP_2GROUP" + "\n");
                strSqlString.Append("     , ROUND((WIP_2GROUP - MAX(WIP_2GROUP) OVER(PARTITION BY MAT_KEY)) / " + sKpcsValue + ", 0) AS DA_WIP_KIT" + "\n");
                strSqlString.Append("     , ROUND(WIP_TTL / " + sKpcsValue + ", 0) AS WIP_TTL" + "\n");
                strSqlString.Append("     , ROUND(UV / " + sKpcsValue + ", 0) AS UV" + "\n");
                strSqlString.Append("     , ROUND(GATE / " + sKpcsValue + ", 0) AS GATE" + "\n");
                strSqlString.Append("     , ROUND(DDS / " + sKpcsValue + ", 0) AS DDS" + "\n");
                strSqlString.Append("     , ROUND(SAW / " + sKpcsValue + ", 0) AS SAW" + "\n");
                strSqlString.Append("     , ROUND(BG / " + sKpcsValue + ", 0) AS BG" + "\n");
                strSqlString.Append("     , ROUND(STEALTH / " + sKpcsValue + ", 0) AS STEALTH" + "\n");
                strSqlString.Append("     , ROUND(PRE_GR / " + sKpcsValue + ", 0) AS PRE_GR" + "\n");
                strSqlString.Append("     , ROUND(LAMI / " + sKpcsValue + ", 0) AS LAMI" + "\n");
                strSqlString.Append("     , ROUND(STOCK / " + sKpcsValue + ", 0) AS STOCK" + "\n");
            }

            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT A.MAT_KEY, A.MAT_ID, A.MAT_GRP_5, A.NET_DIE, A.WIP_DA" + "\n");
            strSqlString.Append("             , CASE WHEN A.MAT_GRP_5 = '1st' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER,0)" + "\n");
            strSqlString.Append("                    WHEN A.MAT_GRP_5 = '2nd' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER,0)" + "\n");
            strSqlString.Append("                    WHEN A.MAT_GRP_5 = '3rd' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER_3,0)" + "\n");
            strSqlString.Append("                    WHEN A.MAT_GRP_5 = '4th' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER_4,0)" + "\n");
            strSqlString.Append("                    WHEN A.MAT_GRP_5 = '5th' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER_5,0)" + "\n");
            strSqlString.Append("                    WHEN A.MAT_GRP_5 = '6th' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER_6,0)" + "\n");
            strSqlString.Append("                    WHEN A.MAT_GRP_5 = '7th' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER_7,0)" + "\n");
            strSqlString.Append("                    WHEN A.MAT_GRP_5 = '8th' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER_8,0)" + "\n");
            strSqlString.Append("                    WHEN A.MAT_GRP_5 = '9th' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER_9,0)" + "\n");
            strSqlString.Append("                    ELSE 0" + "\n");
            strSqlString.Append("               END WIP_2GROUP" + "\n");
            strSqlString.Append("             , C.WIP_MERGE" + "\n");
            strSqlString.Append("             , UV, GATE, DDS, SAW, BG" + "\n");
            strSqlString.Append("             , STEALTH, PRE_GR, LAMI, STOCK" + "\n");
            strSqlString.Append("             , NVL(UV,0) + NVL(GATE,0) + NVL(DDS,0) + NVL(SAW,0) + NVL(BG,0) +" + "\n");
            strSqlString.Append("               NVL(STEALTH,0) + NVL(PRE_GR,0) + NVL(LAMI,0) + NVL(STOCK,0) AS WIP_TTL" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT MAT.*" + "\n");
            strSqlString.Append("                     , CASE WHEN MAT.MAT_GRP_5 = '1st' THEN WIP_DA1" + "\n");
            strSqlString.Append("                            ELSE NVL(WIP_DA1,0) + NVL(WIP_DA2,0)" + "\n");
            strSqlString.Append("                       END WIP_DA" + "\n");
            strSqlString.Append("                     , WIP_DA_WB, UV, GATE, DDS, SAW, BG" + "\n");
            strSqlString.Append("                     , STEALTH, PRE_GR, LAMI, STOCK" + "\n");
            strSqlString.Append("                  FROM DT MAT" + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID" + "\n");

            if (ckbConv.Checked == true)
            {
                strSqlString.Append("                             , SUM(CASE WHEN OPER BETWEEN 'A0301' AND 'A0401' THEN " + sQty_Type + " ELSE 0 END) AS WIP_DA1" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER BETWEEN 'A0402' AND 'A0409' THEN " + sQty_Type + " ELSE 0 END) AS WIP_DA2" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER = 'A0000' THEN " + sQty_Type + " ELSE 0 END) AS STOCK" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER = 'A0020' THEN " + sQty_Type + " ELSE 0 END) AS LAMI" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER = 'A0030' THEN " + sQty_Type + " ELSE 0 END) AS PRE_GR" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER = 'A0033' THEN " + sQty_Type + " ELSE 0 END) AS STEALTH" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER = 'A0040' THEN " + sQty_Type + " ELSE 0 END) AS BG" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER = 'A0200' THEN " + sQty_Type + " ELSE 0 END) AS SAW" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER = 'A0230' THEN " + sQty_Type + " ELSE 0 END) AS DDS" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER = 'A0300' THEN " + sQty_Type + " ELSE 0 END) AS GATE" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER = 'A0250' THEN " + sQty_Type + " ELSE 0 END) AS UV" + "\n"); 
            }
            else
            {
                strSqlString.Append("                             , SUM(CASE WHEN OPER BETWEEN 'A0301' AND 'A0395' THEN " + sQty_Type + " / NVL(DATA_1, 1)" + "\n");
                strSqlString.Append("                                        WHEN OPER BETWEEN 'A0396' AND 'A0401' THEN " + sQty_Type + "" + "\n");
                strSqlString.Append("                                        ELSE 0" + "\n");
                strSqlString.Append("                                   END) AS WIP_DA1" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER BETWEEN 'A0402' AND 'A0409' THEN " + sQty_Type + " ELSE 0 END) AS WIP_DA2" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER = 'A0000' THEN " + sQty_Type + " / NVL(DATA_1, 1) ELSE 0 END) AS STOCK" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER = 'A0020' THEN " + sQty_Type + " / NVL(DATA_1, 1) ELSE 0 END) AS LAMI" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER = 'A0030' THEN " + sQty_Type + " / NVL(DATA_1, 1) ELSE 0 END) AS PRE_GR" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER = 'A0033' THEN " + sQty_Type + " / NVL(DATA_1, 1) ELSE 0 END) AS STEALTH" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER = 'A0040' THEN " + sQty_Type + " / NVL(DATA_1, 1) ELSE 0 END) AS BG" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER = 'A0200' THEN " + sQty_Type + " / NVL(DATA_1, 1) ELSE 0 END) AS SAW" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER = 'A0230' THEN " + sQty_Type + " / NVL(DATA_1, 1) ELSE 0 END) AS DDS" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER = 'A0300' THEN " + sQty_Type + " / NVL(DATA_1, 1) ELSE 0 END) AS GATE" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER = 'A0250' THEN " + sQty_Type + " / NVL(DATA_1, 1) ELSE 0 END) AS UV" + "\n"); 
            }
            
            strSqlString.Append("                             , SUM(CASE WHEN OPER BETWEEN 'A0401' AND 'A0609' THEN " + sQty_Type + " ELSE 0 END) AS WIP_DA_WB" + "\n");

            if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                          FROM RWIPLOTSTS A" + "\n");
                strSqlString.Append("                             , (SELECT KEY_1, DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')) B" + "\n");
                strSqlString.Append("                         WHERE 1=1  " + "\n");
            }
            else
            {
                strSqlString.Append("                          FROM RWIPLOTSTS_BOH A" + "\n");
                strSqlString.Append("                             , (SELECT KEY_1, DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')) B" + "\n");
                strSqlString.Append("                         WHERE 1=1  " + "\n");
                strSqlString.Append("                           AND A.CUTOFF_DT = '" + cdvDate.SelectedValue() + "22' " + "\n");
            }

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                           AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                           AND A.MAT_ID = B.KEY_1(+)" + "\n");
            strSqlString.Append("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND A.OPER BETWEEN 'A0000' AND 'A0609'" + "\n");
            strSqlString.Append("                           AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND A.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("                       ) WIP" + "\n");
            strSqlString.Append("                 WHERE 1=1   " + "\n");
            strSqlString.Append("                   AND MAT.MAT_ID = WIP.MAT_ID(+)   " + "\n");
            strSqlString.Append("                   AND MAT.MAT_GRP_5 <> 'Merge'" + "\n");
            strSqlString.Append("                   AND MAT.MAT_GRP_5 NOT LIKE 'Middle%'" + "\n");
            strSqlString.Append("               ) A" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT.MAT_KEY" + "\n");
            strSqlString.Append("                     , SUM(QTY) AS WIP_MID_MER" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN MAT.MAT_GRP_5 NOT IN ('Middle') THEN QTY ELSE 0 END) AS WIP_MID_MER_3" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN MAT.MAT_GRP_5 NOT IN ('Middle', 'Middle 1') THEN QTY ELSE 0 END) AS WIP_MID_MER_4" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN MAT.MAT_GRP_5 NOT IN ('Middle', 'Middle 1', 'Middle 2') THEN QTY ELSE 0 END) AS WIP_MID_MER_5 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN MAT.MAT_GRP_5 NOT IN ('Middle', 'Middle 1', 'Middle 2', 'Middle 3') THEN QTY ELSE 0 END) AS WIP_MID_MER_6 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN MAT.MAT_GRP_5 NOT IN ('Middle', 'Middle 1', 'Middle 2', 'Middle 3', 'Middle 4') THEN QTY ELSE 0 END) AS WIP_MID_MER_7 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN MAT.MAT_GRP_5 NOT IN ('Middle', 'Middle 1', 'Middle 2', 'Middle 3', 'Middle 4', 'Middle 5') THEN QTY ELSE 0 END) AS WIP_MID_MER_8 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN MAT.MAT_GRP_5 NOT IN ('Middle', 'Middle 1', 'Middle 2', 'Middle 3', 'Middle 4', 'Middle 5', 'Middle 6') THEN QTY ELSE 0 END) AS WIP_MID_MER_9 " + "\n");
            strSqlString.Append("                  FROM DT MAT" + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID" + "\n");
            strSqlString.Append("                             , SUM(" + sQty_Type + ") AS QTY" + "\n");            

            if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                          FROM RWIPLOTSTS" + "\n");
                strSqlString.Append("                         WHERE 1=1  " + "\n");
            }
            else
            {
                strSqlString.Append("                          FROM RWIPLOTSTS_BOH" + "\n");
                strSqlString.Append("                         WHERE 1=1  " + "\n");
                strSqlString.Append("                           AND CUTOFF_DT = '" + cdvDate.SelectedValue() + "22' " + "\n");
            }

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                           AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }
                        
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND OPER BETWEEN 'A0401' AND 'A0609'" + "\n");
            strSqlString.Append("                           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                           AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("                       ) WIP" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND MAT.MAT_ID = WIP.MAT_ID" + "\n");
            strSqlString.Append("                   AND REGEXP_LIKE(MAT.MAT_GRP_5, 'Middle*|Merge')" + "\n");
            strSqlString.Append("                 GROUP BY MAT.MAT_KEY" + "\n");
            strSqlString.Append("               ) B" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT.MAT_KEY" + "\n");
            strSqlString.Append("                     , CASE WHEN WIP.OPER = 'A0401' THEN '1st'" + "\n");
            strSqlString.Append("                            WHEN WIP.OPER = 'A0402' THEN '2nd'" + "\n");
            strSqlString.Append("                            WHEN WIP.OPER = 'A0403' THEN '3rd'" + "\n");
            strSqlString.Append("                            WHEN WIP.OPER = 'A0404' THEN '4th' " + "\n");
            strSqlString.Append("                            WHEN WIP.OPER = 'A0405' THEN '5th' " + "\n");
            strSqlString.Append("                            WHEN WIP.OPER = 'A0406' THEN '6th' " + "\n");
            strSqlString.Append("                            WHEN WIP.OPER = 'A0407' THEN '7th' " + "\n");
            strSqlString.Append("                            WHEN WIP.OPER = 'A0408' THEN '8th' " + "\n");
            strSqlString.Append("                            WHEN WIP.OPER = 'A0409' THEN '9th' " + "\n");
            strSqlString.Append("                            ELSE '' " + "\n");
            strSqlString.Append("                       END MAT_GRP_5 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN WIP.OPER = 'A0401' THEN 0" + "\n");
            strSqlString.Append("                                WHEN WIP.OPER = 'A0402' AND MAT_GRP_5 IN ('Middle','Merge') THEN " + sQty_Type + "" + "\n");
            strSqlString.Append("                                WHEN WIP.OPER = 'A0403' AND MAT_GRP_5 IN ('Middle 1','Merge') THEN " + sQty_Type + "" + "\n");
            strSqlString.Append("                                WHEN WIP.OPER = 'A0404' AND MAT_GRP_5 IN ('Middle 2','Merge') THEN " + sQty_Type + "" + "\n");
            strSqlString.Append("                                WHEN WIP.OPER = 'A0405' AND MAT_GRP_5 IN ('Middle 3','Merge') THEN " + sQty_Type + " " + "\n");
            strSqlString.Append("                                WHEN WIP.OPER = 'A0406' AND MAT_GRP_5 IN ('Middle 4','Merge') THEN " + sQty_Type + " " + "\n");
            strSqlString.Append("                                WHEN WIP.OPER = 'A0407' AND MAT_GRP_5 IN ('Middle 5','Merge') THEN " + sQty_Type + " " + "\n");
            strSqlString.Append("                                WHEN WIP.OPER = 'A0408' AND MAT_GRP_5 IN ('Middle 6','Merge') THEN " + sQty_Type + " " + "\n");
            strSqlString.Append("                                WHEN WIP.OPER = 'A0409' AND MAT_GRP_5 IN ('Middle 7','Merge') THEN " + sQty_Type + " " + "\n");
            strSqlString.Append("                                ELSE 0 " + "\n");
            strSqlString.Append("                           END) AS WIP_MERGE " + "\n");            
            strSqlString.Append("                  FROM DT MAT" + "\n");
            
            if (cdvDate.SelectedValue() == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                     , RWIPLOTSTS WIP" + "\n");
                strSqlString.Append("                 WHERE 1=1  " + "\n");
            }
            else
            {
                strSqlString.Append("                     , RWIPLOTSTS_BOH WIP" + "\n");
                strSqlString.Append("                 WHERE 1=1  " + "\n");
                strSqlString.Append("                   AND WIP.CUTOFF_DT = '" + cdvDate.SelectedValue() + "22' " + "\n");
            }

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND WIP.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                   AND MAT.MAT_ID = WIP.MAT_ID" + "\n");
            strSqlString.Append("                   AND WIP.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND WIP.OPER BETWEEN 'A0401' AND 'A0409'" + "\n");
            strSqlString.Append("                   AND WIP.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                   AND WIP.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND REGEXP_LIKE(MAT.MAT_GRP_5, 'Middle*|Merge')" + "\n");
            strSqlString.Append("                 GROUP BY MAT.MAT_KEY, WIP.OPER" + "\n");            
            strSqlString.Append("               ) C" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND A.MAT_KEY = B.MAT_KEY(+)" + "\n");
            strSqlString.Append("           AND A.MAT_KEY = C.MAT_KEY(+)" + "\n");
            strSqlString.Append("           AND A.MAT_GRP_5 = C.MAT_GRP_5(+)" + "\n");
            strSqlString.Append("       ) A" + "\n");
            strSqlString.Append(" WHERE WIP_2GROUP > 0" + "\n");
            strSqlString.Append(" ORDER BY MAT_KEY, MAT_GRP_5, MAT_ID" + "\n");

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
            double iDAWip = 0;
            double iDACapa = 0;
            double fDaPer = 0;

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

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 4, 5, null, null, btnSort);                
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함
        
                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 5, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);

                Color color = spdData.ActiveSheet.Cells[1, 0].BackColor;

                // PKG CODE 부분 제외하고 Sub Total, Grand Total 삭제 하기...
                // DA CAPA 대비 DA 재공이 50% 이하이면 음영표시
                for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    if (spdData.ActiveSheet.Cells[i, 0].BackColor != color)
                    {
                        spdData.ActiveSheet.Rows[i].Remove();
                        i = i - 1;
                    }

                    if (spdData.ActiveSheet.Cells[i, 1].BackColor != color)
                    {
                        spdData.ActiveSheet.Rows[i].Remove();
                        i = i - 1;
                    }

                    if (spdData.ActiveSheet.Cells[i, 2].BackColor != color)
                    {
                        spdData.ActiveSheet.Rows[i].Remove();
                        i = i - 1;
                    }

                    // PKG CODE 기준의 SubTotal 은 표시 하되 안에 데이터는 지운다.
                    if (spdData.ActiveSheet.Cells[i, 3].BackColor != color)
                    {
                        //spdData.ActiveSheet.Rows[i].ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));
                        for (int y = 3; y < spdData.ActiveSheet.ColumnCount; y++)
                        {
                            spdData.ActiveSheet.Cells[i, y].Text = "";
                        }
                    }

                    iDAWip = Convert.ToInt32(spdData.ActiveSheet.Cells[i, 9].Value);
                    iDACapa = Convert.ToInt32(spdData.ActiveSheet.Cells[i, 6].Value);

                    // DA CAPA 대비 DA 재공이 50% 이하이면 음영표시
                    if (iDAWip != 0 && iDACapa != 0)
                    {
                        fDaPer = iDAWip / iDACapa * 100;

                        if (fDaPer < 50)
                        {
                            spdData.ActiveSheet.Cells[i, 9].BackColor = Color.Red;
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
            this.SetFactory(cdvFactory.txtValue);            
        }

        
        #endregion

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == 3)
            {
                Color BackColor = spdData.ActiveSheet.Cells[1, 3].BackColor;

                // subTotal 을 제외한 나머지 부분 클릭시 실행되도록...
                if (spdData.ActiveSheet.Cells[e.Row, 3].BackColor == BackColor)
                {
                    string sCust = spdData.ActiveSheet.Cells[e.Row, 1].Value.ToString();
                    string sPkg = spdData.ActiveSheet.Cells[e.Row, 3].Value.ToString();

                    // 로딩 창 시작
                    LoadingPopUp.LoadIngPopUpShow(this);

                    DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetail(sCust, sPkg));

                    // 로딩 창 종료
                    LoadingPopUp.LoadingPopUpHidden();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        System.Windows.Forms.Form frm = new PRD010223_P1("", dt);

                        frm.ShowDialog();
                    }

                }
                else
                {
                    return;
                }
            }
        }
    }       
}
