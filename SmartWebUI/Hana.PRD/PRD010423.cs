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
    public partial class PRD010423 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010423<br/>
        /// 클래스요약: 주요 공정별 실적 모니터링<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2014-03-27<br/>
        /// 상세  설명: 주요 공정별 실적 모니터링<br/>
        /// 변경  내용: <br/>
        /// 2014-08-18-임종우 : C-MOLD 재공에 A0950 공정 포함 (김권수D 요청)
        /// 2014-08-25-임종우 : W/B, SST 공정 추가 & BG 재공, 실적에 A0030 공정 포함 (김권수D 요청)
        ///                   : 환산(EPOXY) 추가. 특정 설비의 생산량을 구해야되서 CSUMRASMOV 사용 (김권수D 요청)
        ///                       -> (단, LOT TYPE 구분자가 없기에 E% 포함 됨으로 약간의 수량 갭이 발생 됨, COMP 로직은 반영하지 않는다)
        /// 2014-08-28-임종우 : 그룹정보 TYPE2 추가, SAW 공정 추가 & 날짜 검색 From ~ To 로 변경 (김권수D 요청)
        ///                   : 환산 (EPOXY) 로직 변경. DA(Epoxy), DA(DFN)으로 분리 (임태성K 요청)
        ///                   : DA(Epoxy) - 재공, 실적 (GCM Family 정보에 Epoxy 인것), 설비 (Model정보가 ESEC2100SD, DB%, SPA% 인것)
        ///                   : DA(DFN) - 재공, 실적, 설비 (제품 기준정의 Family 정보가 DFN 인것)
        /// 2014-08-29-임종우 : 재공 부분 1st, Middle%, Merge 만 가져오기. (단. PKG_CODE 가 DHL은 3차 칩, SEKS%로 시작하는 1st 제품의 PKG_CODE 와 동일 하면 2차 칩 가져오기) (임태성K 요청)
        /// 2014-09-03-임종우 : 공정그룹 검색 기능 추가, CUST_TYPE 추가, 환산 시 WB 재공 : 재공 * Wire Count 로 변경 (김권수D 요청)
        ///                   : Kpcs 사용 안하고 고정값으로 표현(1. 기본[Kpcs 로 표현], 2. 환산 : Stealth Saw, BG, DDS, SAW[Wafer 장수로 표현], 그 외[Kpcs로 표현])        
        /// 2014-10-07-임종우 : Stock, Lami 공정 추가(임태성K 요청)
        /// 2014-10-31-임종우 : DA 재공의 경우 SEK% 는 A04% 공정 기존 1st -> 차수와 상관없이 COMP 로직 적용 (임태성K 요청)
        /// 2014-11-10-임종우 : PIN TYPE 추가, Shift 별 실적 조회 기능 추가(임태성K 요청)
        /// 2014-11-12-임종우 : DA 이전 재공은 모두 차수에 대해 재공 (임태성K 요청)
        /// 2015-03-16-임종우 : HMK3 공정 추가 (임태성K 요청)
        /// 2015-04-02-임종우 : Pre_Grind(A0030), BackGrind(A0040) 조회 체크 박스 추가 (백성호 요청)
        ///                   : LOT TYPE 검색 기능 추가 (백성호 요청)
        /// 2015-05-05-임종우 : EPOXY 설비 리스트 오류 부분 수정
        /// 2015-07-03-임종우 : 설비 대수 집계 시 DISPATCH 기준 정보가 'Y" 인 설비만 집계 (임태성K 요청)
        /// 2015-07-20-임종우 : DISPATCH 조건 검색 추가 - 과거 데이터 검색을 위해(임태성K 요청)
        /// 2015-09-10-임종우 : 재공 기준 1st, Middle%, Merge 만 가져오도록 변경 (임태성K 요청)
        /// 2016-01-28-임종우 : C-MOLD 기준 MAT_ID 까지 비교하도록 수정 (김성업D 요청)
        /// 2016-05-26-임종우 : C200, B199 설비 제외시 해당코드로 'Down' 된 설비만 제외 (김보람K 요청)
        /// 2016-06-29-임종우 : WIRE COUNT 정보 없을때 LEAD COUNT 로 대체하는 2차로직 추가 (임태성K 요청)
        /// 2016-07-04-임종우 : 재공 기준으로도 Comp Q'ty 반영 되도록 변경 (최인남상무 요청)
        /// 2016-07-12-임종우 : DA 재공 - A0250, WB 재공 - A050%, A053%, A055% 추가 (임태성K 요청)
        /// 2016-10-10-임종우 : COMP_QTY 삼성외에는 1st, Merge, Middle% 적용하도록 변경 (임태성K 요청)
        ///                   : Stock 실적은 Issue -> Receive 로 변경 (최인남상무 요청)
        /// 2017-02-28-임종우 : Cu wire 검색 기능 추가 (권순태이사 요청)
        /// 2017-09-13-임종우 : SST 신규공정 추가 - A1825 (임태성C 요청)
        /// 2018-09-21-임종우 : Saw Cut(m) 검색 기능 추가 (배진우차장 요청)
        /// 2019-10-16-임종우 : A050%, A053% 공정 WB -> DA 변경 (김성업과장 요청)
        /// 2020-08-24-임종우 : V2 로직 분리 (김성업과장 요청)
        /// 2020-10-12-김미경 : H_PKG_2D_CMOLD "%" 기능 추가
        /// </summary>
        //DataTable dtDay = null;

        public PRD010423()
        {
            InitializeComponent();
            SortInit();
            cdvFromToDate.AutoBinding();
            cdvFromToDate.DaySelector.Enabled = false;
            GridColumnInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
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
            //GetWorkDay();
            spdData.RPT_ColumnInit();           

            try
            {
                spdData.RPT_AddBasicColumn("공정", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("TARGET", 0, 1, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Customer", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Cust Type", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Major Code", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Package", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type2", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Product", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PKG CODE", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("구분", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);

                //for (int i = 0; i < dtDay.Rows.Count; i++)
                //{
                //    spdData.RPT_AddBasicColumn(dtDay.Rows[i][1].ToString() + "일", 0, 9 + i, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                //}

                spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("TOTAL", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("평균", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);               
                
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation", "A.OPER_GRP", "DECODE(A.OPER_GRP, 'STOCK', 1, 'LAMI', 2, 'Stealth Saw', 3, 'BG', 4, 'DDS', 5, 'SAW', 6, 'DA', 7, 'DA(Epoxy)', 8, 'DA(DFN)', 9, 'WB', 10, 'C-MOLD', 11, 'SST', 12, 13)", "A.OPER_GRP", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TARGET", "A.OPER_GRP", "A.OPER_GRP", "(SELECT TO_NUMBER(DATA_1) FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_OPER_GRP_TARGET' AND KEY_1 = A.OPER_GRP) AS TARGET", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "A.MAT_GRP_1", "A.MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Type", "A.CUST_TYPE", "A.CUST_TYPE", "A.CUST_TYPE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Major Code", "A.MAT_GRP_9", "A.MAT_GRP_9", "A.MAT_GRP_9", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "A.MAT_GRP_10", "A.MAT_GRP_10", "A.MAT_GRP_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "A.MAT_GRP_5", "A.MAT_GRP_5", "A.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "A.MAT_ID", "A.MAT_ID", "A.MAT_ID", false);  
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "A.MAT_CMF_11", "A.MAT_CMF_11", "A.MAT_CMF_11", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "A.MAT_CMF_10", "A.MAT_CMF_10", "A.MAT_CMF_10", false);
            
        }
        #endregion


        #region 시간관련 함수
        private void GetWorkDay()
        {            
            //string selectDay = cdvDate.SelectedValue();
            //string startDay = cdvDate.Value.ToString("yyyyMM") + "01";
                        
            //StringBuilder strSqlString = new StringBuilder();

            //strSqlString.Append("SELECT TO_CHAR(TO_DATE('" + startDay + "', 'YYYYMMDD') + ((ROWNUM-1)), 'YYYYMMDD') AS A" + "\n");
            //strSqlString.Append("     , TO_CHAR(TO_DATE('" + startDay + "', 'YYYYMMDD') + ((ROWNUM-1)), 'DD') AS B" + "\n");
            //strSqlString.Append("  FROM DUAL" + "\n");
            //strSqlString.Append("CONNECT BY LEVEL <= (TO_DATE('" + selectDay + "', 'YYYYMMDD') - TO_DATE('" + startDay + "', 'YYYYMMDD')) + 1");

            //dtDay = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());                       
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
            string sStartDay;
            string sEndDay;
            string sYesterday;
            //string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값
            string sSumQuery = null;
            string sWipQuery = null;
            string sEndQuery = null;
            string sExpQuery = null;
            string sResQuery = null;
            string sRunQuery = null;
            string sConvType = null;            
            string sdiff = null;
            string sShpQty1 = null;
            string sShpQty2 = null;
            string sShpQty3 = null;
            string sOperInQty1 = null;
            string sOperInQty2 = null;
            string sAddOper = null;

            sEndDay = cdvFromToDate.HmToDay;
            sYesterday = cdvFromToDate.ToDate.Value.AddDays(-1).ToString("yyyyMMdd");            
            sStartDay = cdvFromToDate.HmFromDay;            

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            DateTime baseTime = DateTime.ParseExact(DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "220000", "yyyyMMddHHmmss", null);
            TimeSpan diff = DateTime.Now - baseTime;            

            // kpcs 선택에 의한 분모 값 저장 한다.
            //if (ckbKpcs.Checked == true)
            //{
            //    sKpcsValue = "1000";
            //}
            //else
            //{
            //    sKpcsValue = "1";
            //}
                       
            if (ckbSawCut.Checked == false)
            {
                sAddOper += "|A0200";
            }
            
            if (rdbConvert.Checked == true || rdbEpoxy.Checked == true)
            {
                sConvType = "CONV_";
            }
            else
            {
                sConvType = "";
            }

            // 경과시간 계산, 현재 조회만 사용하기에 과거일자와 분리함.
            if (DateTime.Now.ToString("yyyyMMdd") == sEndDay)
            {
                sdiff = " / " + diff.TotalHours + " * 24";
            }
            else
            {
                sdiff = "";
            }

            // 선택한 Shift에 따른 쿼리문 생성
            if (cdvShift.Text != "ALL" && cdvShift.Text != "")
            {
                sShpQty1 = cdvShift.SelectedValueToQueryString.Replace("IN ", "").Replace("'", "").Replace(",", "+").Replace("GY", "S1_END_QTY_1").Replace("DY", "S2_END_QTY_1").Replace("SW", "S3_END_QTY_1");
                sShpQty2 = cdvShift.SelectedValueToQueryString.Replace("IN ", "").Replace("'", "").Replace(",", "+").Replace("GY", "S1_END_QTY_2").Replace("DY", "S2_END_QTY_2").Replace("SW", "S3_END_QTY_2");
                sShpQty3 = cdvShift.SelectedValueToQueryString.Replace("IN ", "").Replace("'", "").Replace(",", "+").Replace("GY", "S1_MOVE_QTY_1").Replace("DY", "S2_MOVE_QTY_1").Replace("SW", "S3_MOVE_QTY_1");
                sOperInQty1 = cdvShift.SelectedValueToQueryString.Replace("IN ", "").Replace("'", "").Replace(",", "+").Replace("GY", "S1_OPER_IN_QTY_1").Replace("DY", "S2_OPER_IN_QTY_1").Replace("SW", "S3_OPER_IN_QTY_1");
                sOperInQty2 = cdvShift.SelectedValueToQueryString.Replace("IN ", "").Replace("'", "").Replace(",", "+").Replace("GY", "S1_OPER_IN_QTY_2").Replace("DY", "S2_OPER_IN_QTY_2").Replace("SW", "S3_OPER_IN_QTY_2");
            }
            else
            {
                sShpQty1 = "(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1)";
                sShpQty2 = "(S1_END_QTY_2+S2_END_QTY_2+S3_END_QTY_2)";
                sShpQty3 = "(S1_MOVE_QTY_1+S2_MOVE_QTY_1+S3_MOVE_QTY_1)";
                sOperInQty1 = "(S1_OPER_IN_QTY_1+S2_OPER_IN_QTY_1+S3_OPER_IN_QTY_1)";
                sOperInQty2 = "(S1_OPER_IN_QTY_2+S2_OPER_IN_QTY_2+S3_OPER_IN_QTY_2)";
            }

            #region 반복 쿼리문 생성

            string[] strDate = cdvFromToDate.getSelectDate();
            for (int i = 0; i <= cdvFromToDate.SubtractBetweenFromToDate; i++)
            {
                // kpcs 사용 안하고 고정 값으로 표현 한다.
                if (rdbMain.Checked == true)
                {
                    sSumQuery += "     , ROUND(SUM(CASE WHEN B.GUBUN = '재공' THEN WIP_" + i + " / 1000 " + "\n";
                    sSumQuery += "                      WHEN B.GUBUN = '실적' THEN END_" + i + " / 1000 " + "\n";
                    sSumQuery += "                      WHEN B.GUBUN = '예상실적' THEN EXP_" + i + " / 1000 " + "\n";
                    sSumQuery += "                      WHEN B.GUBUN = '설비대수' THEN RES_" + i + "\n";
                    sSumQuery += "                      WHEN B.GUBUN = 'RUN' THEN RUN_" + i + "\n";
                    sSumQuery += "                 END), 0) AS D" + i + "\n";
                }
                else
                {
                    sSumQuery += "     , ROUND(SUM(CASE WHEN B.GUBUN = '재공' THEN (CASE WHEN A.OPER_GRP IN ('STOCK', 'LAMI', 'Stealth Saw', 'BG', 'DDS', 'SAW') THEN WIP_" + i + " ELSE WIP_" + i + " / 1000 END) " + "\n";
                    sSumQuery += "                      WHEN B.GUBUN = '실적' THEN (CASE WHEN A.OPER_GRP IN ('STOCK', 'LAMI', 'Stealth Saw', 'BG', 'DDS', 'SAW') THEN END_" + i + " ELSE END_" + i + " / 1000 END) " + "\n";
                    sSumQuery += "                      WHEN B.GUBUN = '예상실적' THEN (CASE WHEN A.OPER_GRP IN ('STOCK', 'LAMI', 'Stealth Saw', 'BG', 'DDS', 'SAW') THEN EXP_" + i + " ELSE EXP_" + i + " / 1000 END) " + "\n";
                    sSumQuery += "                      WHEN B.GUBUN = '설비대수' THEN RES_" + i + "\n";
                    sSumQuery += "                      WHEN B.GUBUN = 'RUN' THEN RUN_" + i + "\n";
                    sSumQuery += "                 END), 0) AS D" + i + "\n";
                }

                sEndQuery += "                     , SUM(DECODE(WORK_DATE, '" + strDate[i] + "', " + sConvType + "END_QTY, 0)) AS END_" + i + "\n";
                sWipQuery += "                     , SUM(DECODE(WORK_DATE, '" + strDate[i] + "', " + sConvType + "WIP_QTY, 0)) AS WIP_" + i + "\n";
                sResQuery += "                     , SUM(DECODE(WORK_DATE, '" + strDate[i] + "', RES_CNT, 0)) AS RES_" + i + "\n";
                sRunQuery += "                     , SUM(DECODE(WORK_DATE, '" + strDate[i] + "', RUN_CNT, 0)) AS RUN_" + i + "\n";
                
                // 금일 일자 이면 경과시간 로직 반영함
                if (DateTime.Now.ToString("yyyyMMdd") == strDate[i])
                {
                    sExpQuery += "                     , SUM(DECODE(WORK_DATE, '" + strDate[i] + "', " + sConvType + "END_QTY, 0))" + sdiff + " AS EXP_" + i + "\n";
                }
                else
                {
                    sExpQuery += "                     , SUM(DECODE(WORK_DATE, '" + strDate[i] + "', " + sConvType + "END_QTY, 0)) AS EXP_" + i + "\n";
                }
            }
            #endregion            

            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond3);
            strSqlString.Append("     , B.GUBUN" + "\n");
            strSqlString.Append(sSumQuery);

            if (rdbMain.Checked == true)
            {
                strSqlString.Append("     , ROUND(SUM(CASE WHEN B.GUBUN = '재공' THEN WIP_TTL / 1000" + "\n");
                strSqlString.Append("                      WHEN B.GUBUN = '실적' THEN END_TTL / 1000" + "\n");
                strSqlString.Append("                      WHEN B.GUBUN = '예상실적' THEN EXP_TTL / 1000" + "\n");
                strSqlString.Append("                      WHEN B.GUBUN = '설비대수' THEN RES_TTL" + "\n");
                strSqlString.Append("                      WHEN B.GUBUN = 'RUN' THEN RUN_TTL" + "\n");
                strSqlString.Append("                 END), 0) AS TTL" + "\n");
                strSqlString.Append("     , ROUND(SUM(CASE WHEN B.GUBUN = '재공' THEN WIP_TTL / 1000" + "\n");
                strSqlString.Append("                      WHEN B.GUBUN = '실적' THEN END_TTL / 1000" + "\n");
                strSqlString.Append("                      WHEN B.GUBUN = '예상실적' THEN EXP_TTL / 1000" + "\n");
                strSqlString.Append("                      WHEN B.GUBUN = '설비대수' THEN RES_TTL" + "\n");
                strSqlString.Append("                      WHEN B.GUBUN = 'RUN' THEN RUN_TTL" + "\n");
                strSqlString.Append("                 END) / " + strDate.Length + ", 0) AS AVG" + "\n");
            }
            else
            {
                strSqlString.Append("     , ROUND(SUM(CASE WHEN B.GUBUN = '재공' THEN (CASE WHEN A.OPER_GRP IN ('STOCK', 'LAMI', 'Stealth Saw', 'BG', 'DDS', 'SAW') THEN WIP_TTL ELSE WIP_TTL / 1000 END)" + "\n");
                strSqlString.Append("                      WHEN B.GUBUN = '실적' THEN (CASE WHEN A.OPER_GRP IN ('STOCK', 'LAMI', 'Stealth Saw', 'BG', 'DDS', 'SAW') THEN END_TTL ELSE END_TTL / 1000 END)" + "\n");
                strSqlString.Append("                      WHEN B.GUBUN = '예상실적' THEN (CASE WHEN A.OPER_GRP IN ('STOCK', 'LAMI', 'Stealth Saw', 'BG', 'DDS', 'SAW') THEN EXP_TTL ELSE EXP_TTL / 1000 END)" + "\n");
                strSqlString.Append("                      WHEN B.GUBUN = '설비대수' THEN RES_TTL" + "\n");
                strSqlString.Append("                      WHEN B.GUBUN = 'RUN' THEN RUN_TTL" + "\n");
                strSqlString.Append("                 END), 0) AS TTL" + "\n");
                strSqlString.Append("     , ROUND(SUM(CASE WHEN B.GUBUN = '재공' THEN (CASE WHEN A.OPER_GRP IN ('STOCK', 'LAMI', 'Stealth Saw', 'BG', 'DDS', 'SAW') THEN WIP_TTL ELSE WIP_TTL / 1000 END)" + "\n");
                strSqlString.Append("                      WHEN B.GUBUN = '실적' THEN (CASE WHEN A.OPER_GRP IN ('STOCK', 'LAMI', 'Stealth Saw', 'BG', 'DDS', 'SAW') THEN END_TTL ELSE END_TTL / 1000 END)" + "\n");
                strSqlString.Append("                      WHEN B.GUBUN = '예상실적' THEN (CASE WHEN A.OPER_GRP IN ('STOCK', 'LAMI', 'Stealth Saw', 'BG', 'DDS', 'SAW') THEN EXP_TTL ELSE EXP_TTL / 1000 END)" + "\n");
                strSqlString.Append("                      WHEN B.GUBUN = '설비대수' THEN RES_TTL" + "\n");
                strSqlString.Append("                      WHEN B.GUBUN = 'RUN' THEN RUN_TTL" + "\n");
                strSqlString.Append("                 END) / " + strDate.Length + ", 0) AS AVG" + "\n");
            }

            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT * FROM" + "\n");
            strSqlString.Append("        (" + "\n");
            strSqlString.Append("        SELECT *" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT *" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT A.*" + "\n");
            strSqlString.Append("                             , B.*" + "\n");
            strSqlString.Append("                             , NVL((SELECT DATA_10 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND  KEY_1 = B.MAT_GRP_1), '-') AS CUST_TYPE" + "\n");
            strSqlString.Append("                             , (SELECT /*+INDEX_DESC(MGCMTBLDAT MGCMTBLDAT2_PK)*/ KEY_3 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_PKG_2D_CMOLD' AND KEY_1 = 'C-MOLD' AND KEY_2 = B.MAT_GRP_1 AND KEY_3 = B.MAT_CMF_11 AND (KEY_4 = '%' OR B.MAT_ID LIKE KEY_4) AND ROWNUM = 1) AS KEY_3" + "\n");
            strSqlString.Append("                          FROM (" + "\n");

            if (rdbEpoxy.Checked == true)
            {
                strSqlString.Append("                                SELECT DECODE(LEVEL, 1, 'STOCK', 2, 'LAMI', 3, 'Stealth Saw', 4, 'BG', 5, 'DDS', 6, 'SAW', 7, 'DA(Epoxy)', 8, 'DA(DFN)', 9, 'WB', 10, 'C-MOLD', 11, 'SST', 12, 'HMK3') AS OPER_GRP" + "\n");
                strSqlString.Append("                                  FROM DUAL CONNECT BY LEVEL <= 12 " + "\n");
            }
            else
            {
                strSqlString.Append("                                SELECT DECODE(LEVEL, 1, 'STOCK', 2, 'LAMI', 3, 'Stealth Saw', 4, 'BG', 5, 'DDS', 6, 'SAW', 7, 'DA', 8, 'WB', 9, 'C-MOLD', 10, 'SST', 11, 'HMK3') AS OPER_GRP" + "\n");
                strSqlString.Append("                                  FROM DUAL CONNECT BY LEVEL <= 11 " + "\n");
            }

            strSqlString.Append("                               ) A " + "\n");
            strSqlString.Append("                             , MWIPMATDEF B " + "\n");
            strSqlString.Append("                         WHERE B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND B.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                           AND B.DELETE_FLAG = ' '" + "\n");

            // 2017-02-28-임종우 : Cu wire 검색 기능 추가 (권순태이사 요청)
            if (ckbCuWire.Checked == true)
            {
                strSqlString.Append("                           AND B.MAT_ID IN (SELECT DISTINCT PARTNUMBER FROM CWIPBOMDEF WHERE RESV_FIELD_2 = 'CW')" + "\n");
            }

            strSqlString.Append("                       )" + "\n");

            // 2020-08-24-임종우 : V2 해당 로직 제외
            if (GlobalVariable.gsGlovalSite == "K1")
            {
                strSqlString.Append("                 WHERE (OPER_GRP <> 'C-MOLD' OR KEY_3 IS NOT NULL)" + "\n");
            }

            if (cdvOperGroup.Text != "ALL")
            {
                strSqlString.Append("                   AND OPER_GRP " + cdvOperGroup.SelectedValueToQueryString + "\n");
            }

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                strSqlString.AppendFormat("                   AND MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);
            
            strSqlString.Append("               ) MAT" + "\n");

            #region 1.실적부분
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_ID AS END_MAT_ID" + "\n");
            strSqlString.Append("                     , OPER_GRP AS END_OPER_GRP" + "\n");
            strSqlString.Append("                     , SUM(" + sConvType + "END_QTY) AS END_TTL" + "\n");

            // 실시간 조회는 금일자 경과시간 로직에 의해 계산, 과거일자 조회는 그냥 과거 데이터
            if (DateTime.Now.ToString("yyyyMMdd") == sEndDay)
            {
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + sEndDay + "', 0, " + sConvType + "END_QTY)) + (SUM(DECODE(WORK_DATE, '" + sEndDay + "', " + sConvType + "END_QTY , 0)) / " + diff.TotalHours + " * 24) AS EXP_TTL" + "\n");
            }
            else
            {
                strSqlString.Append("                     , SUM(" + sConvType + "END_QTY) AS EXP_TTL" + "\n");
            }

            strSqlString.Append(sEndQuery);
            strSqlString.Append(sExpQuery);
            strSqlString.Append("                  FROM (" + "\n");
                        
            if (rdbEpoxy.Checked == true)
            {
                #region 1-1.실적-환산(Epoxy)
                strSqlString.Append("                        SELECT A.MAT_ID" + "\n");
                strSqlString.Append("                             , A.WORK_DATE" + "\n");
                strSqlString.Append("                             , CASE WHEN A.OPER = 'A0033' THEN 'Stealth Saw'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER IN ('A0040', 'A0030') THEN 'BG'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'A0230' THEN 'DDS'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'A1000' THEN 'C-MOLD'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER LIKE 'A060%' THEN 'WB'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER IN ('A1750', 'A1825') THEN 'SST'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'A0200' THEN 'SAW'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER LIKE 'A040%' AND B.MAT_GRP_2 = 'DFN' THEN 'DA(DFN)'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER LIKE 'A040%' AND E.DA_TYPE = 'Epoxy' THEN 'DA(Epoxy)'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'A0000' THEN 'STOCK'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'A0020' THEN 'LAMI'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'AZ010' THEN 'HMK3'" + "\n");
                strSqlString.Append("                                    ELSE ''" + "\n");
                strSqlString.Append("                               END OPER_GRP" + "\n");
                #endregion
            }

            else
            {
                #region 1-2.실적-기본&환산
                strSqlString.Append("                        SELECT A.MAT_ID" + "\n");
                strSqlString.Append("                             , A.WORK_DATE" + "\n");
                strSqlString.Append("                             , CASE WHEN A.OPER = 'A0033' THEN 'Stealth Saw'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER IN ('A0040', 'A0030') THEN 'BG'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'A0230' THEN 'DDS'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'A1000' THEN 'C-MOLD'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER LIKE 'A060%' THEN 'WB'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER IN ('A1750', 'A1825') THEN 'SST'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'A0200' THEN 'SAW'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'A0000' THEN 'STOCK'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'A0020' THEN 'LAMI'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'AZ010' THEN 'HMK3'" + "\n");
                strSqlString.Append("                                    ELSE 'DA'" + "\n");
                strSqlString.Append("                               END OPER_GRP" + "\n");
                #endregion
            }
                    
            strSqlString.Append("                             , CASE WHEN A.OPER = 'AZ010' THEN DECODE(B.MAT_GRP_3, 'COB', 0, 'BGN', 0, " + sShpQty3 + ")" + "\n");
            strSqlString.Append("                                    WHEN A.OPER = 'A0000' THEN " + sOperInQty1 + "\n");
            strSqlString.Append("                                    ELSE " + sShpQty1 + "\n");
            strSqlString.Append("                               END AS END_QTY" + "\n");
            strSqlString.Append("                             , CASE WHEN A.OPER = 'A0000' THEN " + sOperInQty2 + "\n");
            strSqlString.Append("                                    WHEN A.OPER IN ('A0033', 'A0040', 'A0230', 'A0030', 'A0200', 'A0020') THEN " + sShpQty2 + "\n");
            strSqlString.Append("                                    WHEN A.OPER LIKE 'A040%' AND B.MAT_ID LIKE 'SEK%' THEN " + sShpQty1 + " * NVL(C.DATA_1,1)" + "\n");
            strSqlString.Append("                                    WHEN A.OPER LIKE 'A040%' AND (B.MAT_GRP_5 IN ('1st', 'Merge') OR B.MAT_GRP_5 LIKE 'Middle%') THEN " + sShpQty1 + " * NVL(C.DATA_1,1)" + "\n");
            strSqlString.Append("                                    WHEN A.OPER LIKE 'A060%' THEN " + sShpQty1 + " * (CASE WHEN D.WIRE_CNT IS NOT NULL THEN D.WIRE_CNT" + "\n");
            strSqlString.Append("                                                                                           WHEN B.MAT_GRP_6 NOT IN ('-','0') THEN B.MAT_GRP_6" + "\n");
            strSqlString.Append("                                                                                           ELSE '1' END)" + "\n");
            strSqlString.Append("                                    WHEN A.OPER = 'AZ010' THEN DECODE(B.MAT_GRP_3, 'COB', 0, 'BGN', 0, " + sShpQty3 + ")" + "\n");
            strSqlString.Append("                                    ELSE " + sShpQty1 + "" + "\n");
            strSqlString.Append("                               END AS CONV_END_QTY" + "\n");
            strSqlString.Append("                          FROM RSUMWIPMOV A" + "\n");
            strSqlString.Append("                             , MWIPMATDEF B" + "\n");
            strSqlString.Append("                             , (" + "\n");
            strSqlString.Append("                                SELECT KEY_1 AS MAT_ID, DATA_1" + "\n");
            strSqlString.Append("                                  FROM MGCMTBLDAT " + "\n");
            strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                   AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')" + "\n");
            strSqlString.Append("                               ) C" + "\n");
            strSqlString.Append("                             , (" + "\n");
            strSqlString.Append("                                SELECT MAT_ID, OPER, TCD_CMF_2 AS WIRE_CNT" + "\n");
            strSqlString.Append("                                  FROM CWIPTCDDEF@RPTTOMES " + "\n");
            strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                   AND OPER LIKE 'A060%'" + "\n");
            strSqlString.Append("                                   AND SET_FLAG = 'Y'" + "\n");
            strSqlString.Append("                                   AND TCD_CMF_2 <> ' '" + "\n");
            strSqlString.Append("                               ) D" + "\n");
            strSqlString.Append("                             , (" + "\n");
            strSqlString.Append("                                SELECT KEY_1 AS MAT_GRP_2, DATA_2 AS DA_TYPE" + "\n");
            strSqlString.Append("                                  FROM MGCMTBLDAT " + "\n");
            strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                   AND TABLE_NAME = 'H_FAMILY'" + "\n");
            strSqlString.Append("                               ) E" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = C.MAT_ID(+)" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = D.MAT_ID(+)" + "\n");
            strSqlString.Append("                           AND A.OPER = D.OPER(+)" + "\n");
            strSqlString.Append("                           AND B.MAT_GRP_2 = E.MAT_GRP_2(+)" + "\n");
            strSqlString.Append("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND A.WORK_DATE BETWEEN '" + sStartDay + "' AND '" + sEndDay + "'" + "\n");
            strSqlString.Append("                           AND A.LOT_TYPE = 'W'" + "\n");                        
            strSqlString.Append("                           AND B.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND B.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                           AND A.FACTORY NOT IN ('RETURN') " + "\n");

            if (cdvType.Text != "ALL")
            {
                strSqlString.Append("                           AND A.CM_KEY_3 LIKE '" + cdvType.Text + "' " + "\n");
            }

            if (ckbPreG.Checked == true)
            {
                strSqlString.Append("                           AND REGEXP_LIKE(A.OPER, 'A040*|A0033|A0040|A0230|A1000|A060*|A1750|A0030|A0000|A0020|AZ010|A1825" + sAddOper +"')" + "\n");
            }            
            else
            {
                strSqlString.Append("                           AND REGEXP_LIKE(A.OPER, 'A040*|A0033|A0040|A0230|A1000|A060*|A1750|A0000|A0020|AZ010|A1825" + sAddOper + "')" + "\n");
            }

            if (ckbSawCut.Checked == true)
            {            
                strSqlString.Append("                         UNION ALL" + "\n");
                strSqlString.Append("                        SELECT MAT_ID, WORK_DATE, 'SAW' AS OPER_GRP, 0, SUM(CMF_FIELD04) + SUM(CMF_FIELD05) AS CONV_END_QTY" + "\n");
                strSqlString.Append("                          FROM (" + "\n");
                strSqlString.Append("                                SELECT MAT_ID" + "\n");
                strSqlString.Append("                                     , TO_CHAR(EVENT_TIME + 2/24,'YYYYMMDD') AS WORK_DATE" + "\n");
                strSqlString.Append("                                     , NVL(TRIM(CMF_FIELD04), 0) AS CMF_FIELD04, NVL(TRIM(CMF_FIELD05), 0) AS CMF_FIELD05" + "\n");
                strSqlString.Append("                                  FROM TDA_UNIT_EVENT@RPTTOFA" + "\n");
                strSqlString.Append("                                 WHERE 1=1" + "\n");                
                strSqlString.Append("                                   AND EVENT_ID = 'EAPBSS_UNIT_WF_END'" + "\n");
                strSqlString.Append("                                   AND EVENT_TIME BETWEEN TO_DATE('" + cdvFromToDate.ExactFromDate + "', 'YYYY/MM/DD HH24:MI:SS') AND TO_DATE('" + cdvFromToDate.ExactToDate + "', 'YYYY/MM/DD HH24:MI:SS')" + "\n");
                strSqlString.Append("                                   AND UNIT_ID = 'CUT'" + "\n");
                strSqlString.Append("                                   AND OPER = 'A0200'" + "\n");
                strSqlString.Append("                                   AND (CMF_FIELD04 IS NOT NULL OR CMF_FIELD05 IS NOT NULL)" + "\n");
                strSqlString.Append("                               )" + "\n");
                strSqlString.Append("                         GROUP BY MAT_ID, WORK_DATE" + "\n");
            }

            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID, OPER_GRP" + "\n");
            strSqlString.Append("               ) SHP" + "\n");
            #endregion

            #region 2.재공부분
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_ID AS WIP_MAT_ID" + "\n");
            strSqlString.Append("                     , OPER_GRP AS WIP_OPER_GRP" + "\n");            
            strSqlString.Append("                     , SUM(" + sConvType + "WIP_QTY) AS WIP_TTL" + "\n");
            strSqlString.Append(sWipQuery);            
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT A.MAT_ID, WORK_DATE" + "\n");            

            if (rdbEpoxy.Checked == true)
            {
                #region 2-1.재공-환산(Epoxy)
                strSqlString.Append("                             , CASE WHEN A.OPER = 'A0033' THEN 'Stealth Saw'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER IN ('A0040', 'A0030') THEN 'BG'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'A0230' THEN 'DDS'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER IN ('A1000', 'A0940', 'A0950') THEN 'C-MOLD'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER LIKE 'A060%' OR A.OPER LIKE 'A055%' THEN 'WB'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER IN ('A1750', 'A1825') THEN 'SST'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'A0200' THEN 'SAW'" + "\n");
                strSqlString.Append("                                    WHEN (A.OPER LIKE 'A040%' OR A.OPER = 'A0250') AND B.MAT_GRP_2 = 'DFN' THEN 'DA(DFN)'" + "\n");
                strSqlString.Append("                                    WHEN (A.OPER LIKE 'A040%' OR A.OPER = 'A0250') AND B.DA_TYPE = 'Epoxy' THEN 'DA(Epoxy)'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'A0000' THEN 'STOCK'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'A0020' THEN 'LAMI'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'AZ010' THEN 'HMK3'" + "\n");
                strSqlString.Append("                                    ELSE ''" + "\n");
                strSqlString.Append("                               END OPER_GRP" + "\n");
                #endregion
            }
            else
            {
                #region 2-2.재공-기본&환산
                strSqlString.Append("                             , CASE WHEN A.OPER = 'A0033' THEN 'Stealth Saw'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER IN ('A0040', 'A0030') THEN 'BG'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'A0230' THEN 'DDS'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER IN ('A1000', 'A0940', 'A0950') THEN 'C-MOLD'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER LIKE 'A060%' OR A.OPER LIKE 'A055%' THEN 'WB'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER IN ('A1750', 'A1825') THEN 'SST'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'A0200' THEN 'SAW'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'A0000' THEN 'STOCK'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'A0020' THEN 'LAMI'" + "\n");
                strSqlString.Append("                                    WHEN A.OPER = 'AZ010' THEN 'HMK3'" + "\n");
                strSqlString.Append("                                    ELSE 'DA'" + "\n");
                strSqlString.Append("                               END OPER_GRP" + "\n");
                #endregion
            }

            //strSqlString.Append("                             , WIP_QTY" + "\n");
            //strSqlString.Append("                             , CASE WHEN A.OPER IN ('A0033', 'A0040', 'A0030', 'A0230', 'A0200', 'A0000', 'A0020') THEN WIP_QTY_2" + "\n");
            //strSqlString.Append("                                    WHEN A.OPER LIKE 'A060%' THEN WIP_QTY * NVL(C.WIRE_CNT, 1)" + "\n");
            //strSqlString.Append("                                    ELSE WIP_QTY" + "\n");
            //strSqlString.Append("                               END AS CONV_WIP_QTY" + "\n");
            strSqlString.Append("                             , CASE WHEN A.OPER IN ('A0033', 'A0040', 'A0030', 'A0230', 'A0200', 'A0000', 'A0020') THEN WIP_QTY" + "\n");
            strSqlString.Append("                                    WHEN B.GUBUN = 'O' THEN WIP_QTY" + "\n");
            strSqlString.Append("                                    ELSE 0" + "\n");
            strSqlString.Append("                               END WIP_QTY" + "\n");
            strSqlString.Append("                             , CASE WHEN A.OPER IN ('A0033', 'A0040', 'A0030', 'A0230', 'A0200', 'A0000', 'A0020') THEN WIP_QTY_2" + "\n");
            strSqlString.Append("                                    WHEN A.OPER LIKE 'A040%' AND B.GUBUN = 'O' THEN WIP_QTY * COMP_CNT" + "\n");
            //strSqlString.Append("                                    WHEN A.OPER LIKE 'A060%' AND B.GUBUN = 'O' THEN WIP_QTY * NVL(C.WIRE_CNT, 1)" + "\n");
            strSqlString.Append("                                    WHEN A.OPER LIKE 'A060%' AND B.GUBUN = 'O' THEN WIP_QTY * (CASE WHEN C.WIRE_CNT IS NOT NULL THEN C.WIRE_CNT" + "\n");
            strSqlString.Append("                                                                                                    WHEN B.MAT_GRP_6 NOT IN ('-','0') THEN B.MAT_GRP_6" + "\n");
            strSqlString.Append("                                                                                                    ELSE '1' END)" + "\n");
            strSqlString.Append("                                    WHEN B.GUBUN = 'O' THEN WIP_QTY" + "\n");
            strSqlString.Append("                                    ELSE 0" + "\n");
            strSqlString.Append("                               END AS CONV_WIP_QTY" + "\n");
            strSqlString.Append("                          FROM (" + "\n");            
            
            if (DateTime.Now.ToString("yyyyMMdd") == sEndDay)
            {                
                strSqlString.Append("                                SELECT MAT_ID" + "\n");
                strSqlString.Append("                                     , '" + sEndDay + "' AS WORK_DATE" + "\n");
                strSqlString.Append("                                     , OPER" + "\n");                
                strSqlString.Append("                                     , QTY_1 AS WIP_QTY" + "\n");
                strSqlString.Append("                                     , QTY_2 AS WIP_QTY_2" + "\n");
                strSqlString.Append("                                  FROM RWIPLOTSTS" + "\n");
                strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                                   AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                                   AND LOT_DEL_FLAG = ' '" + "\n");

                if (ckbPreG.Checked == true)
                {
                    strSqlString.Append("                                   AND REGEXP_LIKE(OPER, 'A040*|A0033|A0040|A0230|A1000|A0940|A0950|A060*|A1750|A0030|A0200|A0000|A0020|AZ010|A0250|A050*|A053*|A055*|A1825') " + "\n");
                }
                else
                {
                    strSqlString.Append("                                   AND REGEXP_LIKE(OPER, 'A040*|A0033|A0040|A0230|A1000|A0940|A0950|A060*|A1750|A0200|A0000|A0020|AZ010|A0250|A050*|A053*|A055*|A1825') " + "\n");
                }

                if (cdvType.Text != "ALL")
                {
                    strSqlString.Append("                                   AND LOT_CMF_5 LIKE '" + cdvType.Text + "' " + "\n");
                }
                                
                strSqlString.Append("                                 UNION ALL" + "\n");
                strSqlString.Append("                                SELECT MAT_ID" + "\n");
                strSqlString.Append("                                     , WORK_DATE" + "\n");
                strSqlString.Append("                                     , OPER" + "\n");           
                strSqlString.Append("                                     , EOH_QTY_1 AS WIP_QTY" + "\n");
                strSqlString.Append("                                     , EOH_QTY_2 AS WIP_QTY_2" + "\n");
                strSqlString.Append("                                  FROM RSUMWIPEOH" + "\n");
                strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                                   AND LOT_TYPE = 'W'" + "\n");

                if (ckbPreG.Checked == true)
                {
                    strSqlString.Append("                                   AND REGEXP_LIKE(OPER, 'A040*|A0033|A0040|A0230|A1000|A0940|A0950|A060*|A1750|A0030|A0200|A0000|A0020|AZ010|A0250|A050*|A053*|A055*|A1825')" + "\n");
                }
                else
                {
                    strSqlString.Append("                                   AND REGEXP_LIKE(OPER, 'A040*|A0033|A0040|A0230|A1000|A0940|A0950|A060*|A1750|A0200|A0000|A0020|AZ010|A0250|A050*|A053*|A055*|A1825')" + "\n");
                }
                
                strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + sStartDay + "' AND '" + sYesterday + "'" + "\n");
                strSqlString.Append("                                   AND SHIFT = '3'" + "\n");

                if (cdvType.Text != "ALL")
                {
                    strSqlString.Append("                                   AND CM_KEY_3 LIKE '" + cdvType.Text + "' " + "\n");
                }                
            }
            else
            {
                strSqlString.Append("                                SELECT MAT_ID" + "\n");
                strSqlString.Append("                                     , WORK_DATE" + "\n");
                strSqlString.Append("                                     , OPER" + "\n");
                strSqlString.Append("                                     , EOH_QTY_1 AS WIP_QTY" + "\n");
                strSqlString.Append("                                     , EOH_QTY_2 AS WIP_QTY_2" + "\n");
                strSqlString.Append("                                  FROM RSUMWIPEOH" + "\n");
                strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                                   AND LOT_TYPE = 'W'" + "\n");

                if (ckbPreG.Checked == true)
                {
                    strSqlString.Append("                                   AND REGEXP_LIKE(OPER, 'A040*|A0033|A0040|A0230|A1000|A0940|A0950|A060*|A1750|A0030|A0200|A0000|A0020|AZ010|A0250|A050*|A053*|A055*|A1825')" + "\n");
                }
                else
                {
                    strSqlString.Append("                                   AND REGEXP_LIKE(OPER, 'A040*|A0033|A0040|A0230|A1000|A0940|A0950|A060*|A1750|A0200|A0000|A0020|AZ010|A0250|A050*|A053*|A055*|A1825')" + "\n");
                }
                
                strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + sStartDay + "' AND '" + sEndDay + "'" + "\n");
                strSqlString.Append("                                   AND SHIFT = '3'" + "\n");

                if (cdvType.Text != "ALL")
                {
                    strSqlString.Append("                                   AND CM_KEY_3 LIKE '" + cdvType.Text + "' " + "\n");
                }                
            }

            strSqlString.Append("                               ) A" + "\n");
            strSqlString.Append("                             , (" + "\n");
            strSqlString.Append("                                SELECT *" + "\n");
            strSqlString.Append("                                  FROM (" + "\n");
            strSqlString.Append("                                        SELECT MAT_ID, MAT_GRP_2, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6, MAT_CMF_11" + "\n");
            //strSqlString.Append("                                             , CASE WHEN MAT_CMF_11 = 'DHL' THEN (CASE WHEN MAT_GRP_5 IN ('3rd','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN 'O' ELSE '' END)" + "\n");
            //strSqlString.Append("                                                    WHEN MAT_ID LIKE 'SEK%' AND MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%')" + "\n");
            //strSqlString.Append("                                                         THEN (CASE WHEN MAT_GRP_5 IN ('2nd','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN 'O' ELSE '' END)" + "\n");
            //strSqlString.Append("                                                    WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-'" + "\n");
            //strSqlString.Append("                                                         THEN (CASE WHEN MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN 'O' ELSE '' END)" + "\n");
            //strSqlString.Append("                                                    ELSE 'O'" + "\n");
            //strSqlString.Append("                                               END GUBUN" + "\n");            
            strSqlString.Append("                                             , CASE WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-'" + "\n");
            strSqlString.Append("                                                         THEN (CASE WHEN MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN 'O' ELSE '' END)" + "\n");
            strSqlString.Append("                                                    WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 = '-' THEN '' " + "\n");
            strSqlString.Append("                                                    ELSE 'O'" + "\n");
            strSqlString.Append("                                               END GUBUN" + "\n");
            strSqlString.Append("                                             , (SELECT DATA_2 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_FAMILY' AND KEY_1 = MAT_GRP_2) AS DA_TYPE" + "\n");
            strSqlString.Append("                                             , COMP_CNT" + "\n");
            strSqlString.Append("                                          FROM VWIPMATDEF" + "\n");
            strSqlString.Append("                                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                           AND MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                                           AND DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                           AND MAT_GRP_2 <> '-'" + "\n");
            strSqlString.Append("                                       )" + "\n");
            //strSqlString.Append("                                 WHERE GUBUN = 'O'" + "\n");
            strSqlString.Append("                               ) B" + "\n");
            strSqlString.Append("                             , (" + "\n");
            strSqlString.Append("                                SELECT MAT_ID, OPER, TCD_CMF_2 AS WIRE_CNT" + "\n");
            strSqlString.Append("                                  FROM CWIPTCDDEF@RPTTOMES" + "\n");
            strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                   AND OPER LIKE 'A060%'" + "\n");
            strSqlString.Append("                                   AND SET_FLAG = 'Y'" + "\n");
            strSqlString.Append("                                   AND TCD_CMF_2 <> ' '" + "\n");
            strSqlString.Append("                               ) C" + "\n");
            strSqlString.Append("                         WHERE A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = C.MAT_ID(+)" + "\n");
            strSqlString.Append("                           AND A.OPER = C.OPER(+)" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID, OPER_GRP" + "\n");
            strSqlString.Append("               ) WIP" + "\n");
            #endregion

            #region 3.설비부분
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT RES_STS_2 AS RES_MAT_ID" + "\n");
            strSqlString.Append("                     , OPER_GRP AS RES_OPER_GRP " + "\n");
            strSqlString.Append("                     , SUM(RES_CNT) AS RES_TTL" + "\n");
            strSqlString.Append("                     , SUM(RUN_CNT) AS RUN_TTL" + "\n");
            strSqlString.Append(sResQuery);
            strSqlString.Append(sRunQuery);
            strSqlString.Append("                  FROM (" + "\n");

            strSqlString.Append("                        SELECT RES_STS_2" + "\n");
            strSqlString.Append("                             , WORK_DATE" + "\n");

            if (rdbEpoxy.Checked == true)
            {
                #region 3-1.설비-환산(Epoxy)
                strSqlString.Append("                             , CASE WHEN A.RES_GRP_3 = 'SDBG' THEN 'Stealth Saw'" + "\n");
                strSqlString.Append("                                    WHEN A.RES_GRP_3 = 'BACK LAP' THEN 'BG'" + "\n");
                strSqlString.Append("                                    WHEN A.RES_GRP_3 = 'WAFER EXPANDING' THEN 'DDS'" + "\n");
                strSqlString.Append("                                    WHEN A.RES_GRP_3 = 'MOLD' THEN 'C-MOLD'" + "\n");
                strSqlString.Append("                                    WHEN A.RES_GRP_3 = 'WIRE BOND' THEN 'WB'" + "\n");
                strSqlString.Append("                                    WHEN A.RES_GRP_3 = 'SAW SORTER' THEN 'SST'" + "\n");
                strSqlString.Append("                                    WHEN A.RES_GRP_3 = 'SAW' THEN 'SAW'" + "\n");
                strSqlString.Append("                                    WHEN A.RES_GRP_3 = 'DIE ATTACH' AND B.MAT_GRP_2 = 'DFN' THEN 'DA(DFN)'" + "\n");
                strSqlString.Append("                                    WHEN A.RES_GRP_3 = 'DIE ATTACH' AND REGEXP_LIKE(A.RES_GRP_6, 'ESEC2100SD|SDB-30UST|^DB*|^SPA*') THEN 'DA(Epoxy)'" + "\n");
                strSqlString.Append("                                    WHEN A.RES_GRP_3 = 'LAMINATION' THEN 'LAMI'" + "\n");
                strSqlString.Append("                                    ELSE 'DA'" + "\n");
                strSqlString.Append("                               END OPER_GRP" + "\n");
                #endregion
            }
            else
            {
                #region 3-2.설비-기본&환산
                strSqlString.Append("                             , CASE WHEN A.RES_GRP_3 = 'SDBG' THEN 'Stealth Saw'" + "\n");
                strSqlString.Append("                                    WHEN A.RES_GRP_3 = 'BACK LAP' THEN 'BG'" + "\n");
                strSqlString.Append("                                    WHEN A.RES_GRP_3 = 'WAFER EXPANDING' THEN 'DDS'" + "\n");
                strSqlString.Append("                                    WHEN A.RES_GRP_3 = 'MOLD' THEN 'C-MOLD'" + "\n");
                strSqlString.Append("                                    WHEN A.RES_GRP_3 = 'WIRE BOND' THEN 'WB'" + "\n");
                strSqlString.Append("                                    WHEN A.RES_GRP_3 = 'SAW SORTER' THEN 'SST'" + "\n");
                strSqlString.Append("                                    WHEN A.RES_GRP_3 = 'SAW' THEN 'SAW'" + "\n");
                strSqlString.Append("                                    WHEN A.RES_GRP_3 = 'LAMINATION' THEN 'LAMI'" + "\n");
                strSqlString.Append("                                    ELSE 'DA'" + "\n");
                strSqlString.Append("                               END OPER_GRP" + "\n");
                #endregion
            }

            strSqlString.Append("                             , RES_CNT" + "\n");
            strSqlString.Append("                             , RUN_CNT" + "\n");
            strSqlString.Append("                          FROM (" + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == sEndDay)
            {                
                strSqlString.Append("                                SELECT A.RES_STS_2" + "\n");
                strSqlString.Append("                                     , '" + sEndDay + "' AS WORK_DATE" + "\n");
                strSqlString.Append("                                     , A.RES_GRP_3" + "\n");
                strSqlString.Append("                                     , A.RES_GRP_6" + "\n");                
                strSqlString.Append("                                     , COUNT(A.RES_ID) AS RES_CNT" + "\n");
                strSqlString.Append("                                     , NVL(SUM(DECODE(RES_UP_DOWN_FLAG, 'U', DECODE(NVL(B.START_RES_ID, '-'), '-', 0, 1))), 0) AS RUN_CNT" + "\n");
                strSqlString.Append("                                  FROM MRASRESDEF A" + "\n");
                strSqlString.Append("                                     , (" + "\n");
                strSqlString.Append("                                        SELECT DISTINCT START_RES_ID" + "\n");
                strSqlString.Append("                                          FROM MWIPLOTSTS" + "\n");
                strSqlString.Append("                                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                                           AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                                           AND LOT_DEL_FLAG = ' '" + "\n");

                if (cdvType.Text != "ALL")
                {
                    strSqlString.Append("                                           AND LOT_CMF_5 LIKE '" + cdvType.Text + "' " + "\n");
                }

                if (ckbPreG.Checked == true)
                {
                    strSqlString.Append("                                           AND REGEXP_LIKE(OPER, 'A040*|A0033|A0040|A0230|A1000|A060*|A1750|A0030|A0200|A0020|A1825')" + "\n");
                }
                else
                {
                    strSqlString.Append("                                           AND REGEXP_LIKE(OPER, 'A040*|A0033|A0040|A0230|A1000|A060*|A1750|A0200|A0020|A1825')" + "\n");

                }
                
                strSqlString.Append("                                           AND LOT_STATUS = 'PROC'" + "\n");
                strSqlString.Append("                                       ) B" + "\n");
                strSqlString.Append("                                 WHERE 1=1" + "\n");
                strSqlString.Append("                                   AND A.RES_ID = B.START_RES_ID(+)" + "\n");
                strSqlString.Append("                                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                                   AND A.RES_CMF_9 = 'Y'" + "\n");

                if (ckbDispatch.Checked == true)
                {
                    strSqlString.Append("                                   AND A.RES_CMF_7 = 'Y'" + "\n");
                    strSqlString.Append("                                   AND (A.RES_STS_1 NOT IN ('C200', 'B199') OR A.RES_UP_DOWN_FLAG = 'U') " + "\n");
                }
                strSqlString.Append("                                   AND A.DELETE_FLAG  = ' '" + "\n");
                strSqlString.Append("                                   AND A.RES_TYPE  = 'EQUIPMENT'" + "\n");

                // 2020-08-24-임종우 : V2 로직 분리
                if (GlobalVariable.gsGlovalSite == "K1")
                {
                    strSqlString.Append("                                   AND (A.RES_GRP_3 IN ('SDBG','BACK LAP','WAFER EXPANDING','DIE ATTACH', 'WIRE BOND', 'SAW SORTER', 'SAW', 'LAMINATION') OR A.RES_ID IN ('M-A57','M-A59','M-A60'))" + "\n");
                }
                else
                {
                    strSqlString.Append("                                   AND A.RES_GRP_3 IN ('SDBG','BACK LAP','WAFER EXPANDING','DIE ATTACH', 'WIRE BOND', 'SAW SORTER', 'SAW', 'LAMINATION', 'MOLD')" + "\n");
                }

                strSqlString.Append("                                 GROUP BY A.RES_STS_2, A.RES_GRP_3, A.RES_GRP_6" + "\n");
                strSqlString.Append("                                 UNION ALL" + "\n");
                strSqlString.Append("                                SELECT RES_STS_2" + "\n");
                strSqlString.Append("                                     , SUBSTR(CUTOFF_DT, 1, 8) AS WORK_DATE" + "\n");
                strSqlString.Append("                                     , RES_GRP_3" + "\n");
                strSqlString.Append("                                     , RES_GRP_6" + "\n");
                strSqlString.Append("                                     , COUNT(RES_ID) AS RES_CNT" + "\n");
                strSqlString.Append("                                     , SUM(DECODE(RES_UP_DOWN_FLAG, 'U', 1, 0)) AS RUN_CNT" + "\n");
                strSqlString.Append("                                  FROM MRASRESDEF_BOH" + "\n");
                strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                                   AND CUTOFF_DT BETWEEN '" + sStartDay + "22' AND '" + sYesterday + "22'" + "\n");
                strSqlString.Append("                                   AND SUBSTR(CUTOFF_DT, -2) = '22'" + "\n");
                strSqlString.Append("                                   AND RES_CMF_9 = 'Y'" + "\n");

                if (ckbDispatch.Checked == true)
                {
                    strSqlString.Append("                                   AND RES_CMF_7 = 'Y'" + "\n");
                    strSqlString.Append("                                   AND (RES_STS_1 NOT IN ('C200', 'B199') OR RES_UP_DOWN_FLAG = 'U') " + "\n");
                }

                strSqlString.Append("                                   AND DELETE_FLAG  = ' '" + "\n");
                strSqlString.Append("                                   AND RES_TYPE  = 'EQUIPMENT'" + "\n");

                // 2020-08-24-임종우 : V2 로직 분리
                if (GlobalVariable.gsGlovalSite == "K1")
                {
                    strSqlString.Append("                                   AND (RES_GRP_3 IN ('SDBG','BACK LAP','WAFER EXPANDING','DIE ATTACH', 'WIRE BOND', 'SAW SORTER', 'SAW', 'LAMINATION') OR RES_ID IN ('M-A57','M-A59','M-A60'))" + "\n");
                }
                else
                {
                    strSqlString.Append("                                   AND RES_GRP_3 IN ('SDBG','BACK LAP','WAFER EXPANDING','DIE ATTACH', 'WIRE BOND', 'SAW SORTER', 'SAW', 'LAMINATION', 'MOLD')" + "\n");
                }
                                
                strSqlString.Append("                                 GROUP BY RES_STS_2, SUBSTR(CUTOFF_DT, 1, 8), RES_GRP_3, RES_GRP_6" + "\n");
            }
            else
            {
                strSqlString.Append("                                SELECT RES_STS_2" + "\n");
                strSqlString.Append("                                     , SUBSTR(CUTOFF_DT, 1, 8) AS WORK_DATE" + "\n");
                strSqlString.Append("                                     , RES_GRP_3" + "\n");
                strSqlString.Append("                                     , RES_GRP_6" + "\n");
                strSqlString.Append("                                     , COUNT(RES_ID) AS RES_CNT" + "\n");
                strSqlString.Append("                                     , SUM(DECODE(RES_UP_DOWN_FLAG, 'U', 1, 0)) AS RUN_CNT" + "\n");
                strSqlString.Append("                                  FROM MRASRESDEF_BOH" + "\n");
                strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                                   AND CUTOFF_DT BETWEEN '" + sStartDay + "22' AND '" + sEndDay + "22'" + "\n");
                strSqlString.Append("                                   AND SUBSTR(CUTOFF_DT, -2) = '22'" + "\n");
                strSqlString.Append("                                   AND RES_CMF_9 = 'Y'" + "\n");

                if (ckbDispatch.Checked == true)
                {
                    strSqlString.Append("                                   AND RES_CMF_7 = 'Y'" + "\n");
                    strSqlString.Append("                                   AND (RES_STS_1 NOT IN ('C200', 'B199') OR RES_UP_DOWN_FLAG = 'U') " + "\n");
                }

                strSqlString.Append("                                   AND DELETE_FLAG  = ' '" + "\n");
                strSqlString.Append("                                   AND RES_TYPE  = 'EQUIPMENT'" + "\n");

                // 2020-08-24-임종우 : V2 로직 분리
                if (GlobalVariable.gsGlovalSite == "K1")
                {
                    strSqlString.Append("                                   AND (RES_GRP_3 IN ('SDBG','BACK LAP','WAFER EXPANDING','DIE ATTACH', 'WIRE BOND', 'SAW SORTER', 'SAW', 'LAMINATION') OR RES_ID IN ('M-A57','M-A59','M-A60'))" + "\n");
                }
                else
                {
                    strSqlString.Append("                                   AND RES_GRP_3 IN ('SDBG','BACK LAP','WAFER EXPANDING','DIE ATTACH', 'WIRE BOND', 'SAW SORTER', 'SAW', 'LAMINATION', 'MOLD')" + "\n");
                }
                
                strSqlString.Append("                                 GROUP BY RES_STS_2, SUBSTR(CUTOFF_DT, 1, 8), RES_GRP_3, RES_GRP_6" + "\n");
            }
            
            strSqlString.Append("                               ) A" + "\n");

            if (rdbEpoxy.Checked == true)
            {                
                strSqlString.Append("                             , (" + "\n");
                strSqlString.Append("                                SELECT MAT_ID, MAT_GRP_2" + "\n");
                strSqlString.Append("                                     , (SELECT DATA_2 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_FAMILY' AND KEY_1 = MAT_GRP_2) AS DA_TYPE" + "\n");
                strSqlString.Append("                                  FROM MWIPMATDEF" + "\n");
                strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                                   AND MAT_TYPE = 'FG'" + "\n");
                strSqlString.Append("                               ) B" + "\n");
                strSqlString.Append("                 WHERE A.RES_STS_2 = B.MAT_ID(+)" + "\n");
            }
           
            strSqlString.Append("                       )" + "\n");                        
            strSqlString.Append("                 GROUP BY RES_STS_2, OPER_GRP" + "\n");
            strSqlString.Append("               ) RES" + "\n");
            #endregion

            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND MAT.OPER_GRP = SHP.END_OPER_GRP(+)" + "\n");
            strSqlString.Append("           AND MAT.OPER_GRP = WIP.WIP_OPER_GRP(+)" + "\n");
            strSqlString.Append("           AND MAT.OPER_GRP = RES.RES_OPER_GRP(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = SHP.END_MAT_ID(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = WIP.WIP_MAT_ID(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = RES.RES_MAT_ID(+)" + "\n");
            strSqlString.Append("           AND NVL(SHP.END_TTL,0) + NVL(WIP.WIP_TTL,0) + NVL(RES.RES_TTL,0) > 0" + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.Append("       WHERE 1=1" + "\n");
            strSqlString.Append("       AND MAT_ID NOT IN (SELECT MAT_ID FROM MWIPMATDEF WHERE FIRST_FLOW = 'A-BANK' AND DELETE_FLAG = ' ')" + "\n");
            strSqlString.Append("       ) A" + "\n");
            strSqlString.Append("     , (SELECT DECODE(LEVEL, 1, '재공', 2, '실적', 3, '예상실적', 4, '설비대수', 5, 'RUN') AS GUBUN FROM DUAL CONNECT BY LEVEL <= 5 ) B" + "\n");            
            strSqlString.AppendFormat(" GROUP BY {0}, B.GUBUN " + "\n", QueryCond1);
            strSqlString.AppendFormat(" ORDER BY {0}, DECODE(B.GUBUN, '재공', 1, '실적', 2, '예상실적', 3, '설비대수', 4, 5) " + "\n", QueryCond2);            

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
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 12, null, null, btnSort);

                //Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 12, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.DataSource = dt;                
                spdData.RPT_AutoFit(false);

                for (int i = 0; i <= 10; i++)
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

                ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);
                //(데이타, 제목(타이틀), 좌측문구, 우측문구, 자동사이즈조정)

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
            string sDate = null;
            string[] dataArry = new string[9];

            // 설비대수 클릭 시 팝업 창 띄움.
            if (e.Column > 10 && spdData.ActiveSheet.Cells[e.Row, 10].Text == "설비대수" && spdData.ActiveSheet.Columns[e.Column].Label != "TOTAL" && spdData.ActiveSheet.Columns[e.Column].Label != "평균")
            {                           
                // Group 정보 가져와서 담기... 상세 팝업에서 조건값으로 사용하기 위해
                for (int i = 0; i < 10; i++)
                {
                    if (spdData.ActiveSheet.Columns[i].Label == "공정")
                        dataArry[0] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "Customer")
                        dataArry[1] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "Major Code")
                        dataArry[2] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "Package")
                        dataArry[3] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "Type2")
                        dataArry[4] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "Product")
                        dataArry[5] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "PKG CODE")
                        dataArry[6] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "Cust Type")
                        dataArry[7] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "Pin Type")
                        dataArry[8] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();
          
                }

                // 공정 그룹을 공정 코드로 변경하기 위해..
                if (dataArry[0] != " ")
                {
                    if (dataArry[0] == "Stealth Saw")
                        dataArry[0] = "SDBG";

                    else if (dataArry[0] == "BG")
                        dataArry[0] = "BACK LAP";

                    else if (dataArry[0] == "DDS")
                        dataArry[0] = "WAFER EXPANDING";

                    else if (dataArry[0] == "C-MOLD")
                        dataArry[0] = "MOLD";

                    else if (dataArry[0] == "WB")
                        dataArry[0] = "WIRE BOND";

                    else if (dataArry[0] == "SST")
                        dataArry[0] = "SAW SORTER";

                    else if (dataArry[0] == "SAW")
                        dataArry[0] = "SAW";

                    else if (dataArry[0] == "DA")
                        dataArry[0] = "DIE ATTACH";

                    else if (dataArry[0] == "DA(DFN)")
                        dataArry[0] = "DA(DFN)";

                    else if (dataArry[0] == "LAMI")
                        dataArry[0] = "LAMINATION";

                    else
                        dataArry[0] = "DA(Epoxy)";

                }

                // 고객사 명을 고객사 코드로 변경하기 위해..
                if (dataArry[1] != " ")
                {
                    DataTable dtCustomer = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlCustomer(dataArry[1].ToString()));

                    dataArry[1] = dtCustomer.Rows[0][0].ToString();
                }

                // 클릭한 컬럼의 날짜 값 가져오기
                string[] strDate = cdvFromToDate.getSelectDate();
                sDate = strDate[e.Column-11];

                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetail(sDate, dataArry));

                if (dt != null && dt.Rows.Count > 0)
                {
                    System.Windows.Forms.Form frm = new PRD010423_P1("", dt);
                    frm.ShowDialog();
                }
                
            }
            else
            {
                return;
            }
        }

        //고객사 명으로 고객사 코드 가져오기 위해..
        private string MakeSqlCustomer(string customerName)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("SELECT KEY_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND DATA_1 = '" + customerName + "' AND ROWNUM=1" + "\n");

            return strSqlString.ToString();
        }

        #region MakeSqlDetail
        
        private string MakeSqlDetail(string sDate, string[] dataArry)
        {
            StringBuilder strSqlString = new StringBuilder();



            if (DateTime.Now.ToString("yyyyMMdd") == sDate)
            {
                strSqlString.Append("SELECT DECODE(A.RES_UP_DOWN_FLAG, 'U', 'UP', 'DOWN') AS STATE" + "\n");
                strSqlString.Append("     , A.RES_GRP_6 AS MODEL, A.RES_STS_2 AS MAT_ID, A.RES_ID, A.RES_STS_1 AS CODE" + "\n");
                strSqlString.Append("  FROM MRASRESDEF A" + "\n");
                strSqlString.Append("     , (" + "\n");
                strSqlString.Append("        SELECT M.* " + "\n");
                strSqlString.Append("             , NVL((SELECT DATA_10 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND  KEY_1 = M.MAT_GRP_1), '-') AS CUST_TYPE " + "\n");
                strSqlString.Append("          FROM MWIPMATDEF M " + "\n");
                strSqlString.Append("         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND DELETE_FLAG = ' ' " + "\n");
                strSqlString.Append("       ) B" + "\n");
                strSqlString.Append("     , (" + "\n");
                strSqlString.Append("        SELECT DISTINCT START_RES_ID" + "\n");
                strSqlString.Append("          FROM MWIPLOTSTS" + "\n");
                strSqlString.Append("         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("           AND LOT_DEL_FLAG = ' '" + "\n");

                if (cdvType.Text != "ALL")
                {
                    strSqlString.Append("           AND LOT_CMF_5 LIKE '" + cdvType.Text + "' " + "\n");
                }

                if (ckbPreG.Checked == true)
                {
                    strSqlString.Append("           AND REGEXP_LIKE(OPER, 'A040*|A0033|A0040|A0230|A1000|A060*|A1750|A0030|A0200|A0020|A1825')" + "\n");
                }
                else
                {
                    strSqlString.Append("           AND REGEXP_LIKE(OPER, 'A040*|A0033|A0040|A0230|A1000|A060*|A1750|A0200|A0020|A1825')" + "\n");
                }

                strSqlString.Append("           AND LOT_STATUS = 'PROC'" + "\n");
                strSqlString.Append("       ) C" + "\n");
                strSqlString.Append(" WHERE 1=1" + "\n");
                strSqlString.Append("   AND A.FACTORY = B.FACTORY(+)" + "\n");
                strSqlString.Append("   AND A.RES_STS_2 = B.MAT_ID(+)" + "\n");
                strSqlString.Append("   AND A.RES_ID = C.START_RES_ID(+)" + "\n");
                strSqlString.Append("   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("   AND A.RES_CMF_9 = 'Y'" + "\n");

                if (ckbDispatch.Checked == true)
                {
                    strSqlString.Append("   AND A.RES_CMF_7 = 'Y'" + "\n");
                    strSqlString.Append("   AND (A.RES_STS_1 NOT IN ('C200', 'B199') OR A.RES_UP_DOWN_FLAG = 'U') " + "\n");
                }

                strSqlString.Append("   AND A.DELETE_FLAG  = ' '" + "\n");
                strSqlString.Append("   AND A.RES_TYPE  = 'EQUIPMENT'" + "\n");

                // 2017-02-28-임종우 : Cu wire 검색 기능 추가 (권순태이사 요청)
                if (ckbCuWire.Checked == true)
                {
                    strSqlString.Append("   AND B.MAT_ID IN (SELECT DISTINCT PARTNUMBER FROM CWIPBOMDEF WHERE RESV_FIELD_2 = 'CW')" + "\n");
                }
            }
            else
            {
                strSqlString.Append("SELECT DECODE(A.RES_UP_DOWN_FLAG, 'U', 'UP', 'DOWN') AS STATE" + "\n");
                strSqlString.Append("     , A.RES_GRP_6 AS MODEL, A.RES_STS_2 AS MAT_ID, A.RES_ID, A.RES_STS_1 AS CODE" + "\n");
                strSqlString.Append("  FROM MRASRESDEF_BOH A" + "\n");
                strSqlString.Append("     , (" + "\n");
                strSqlString.Append("        SELECT M.* " + "\n");
                strSqlString.Append("             , NVL((SELECT DATA_10 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND  KEY_1 = M.MAT_GRP_1), '-') AS CUST_TYPE " + "\n");
                strSqlString.Append("          FROM MWIPMATDEF M " + "\n");
                strSqlString.Append("         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND DELETE_FLAG = ' ' " + "\n");
                strSqlString.Append("       ) B" + "\n");                
                strSqlString.Append(" WHERE 1=1" + "\n");
                strSqlString.Append("   AND A.FACTORY = B.FACTORY(+)" + "\n");
                strSqlString.Append("   AND A.RES_STS_2 = B.MAT_ID(+)" + "\n");                
                strSqlString.Append("   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("   AND A.RES_CMF_9 = 'Y'" + "\n");

                if (ckbDispatch.Checked == true)
                {
                    strSqlString.Append("   AND A.RES_CMF_7 = 'Y'" + "\n");
                    strSqlString.Append("   AND (A.RES_STS_1 NOT IN ('C200', 'B199') OR A.RES_UP_DOWN_FLAG = 'U') " + "\n");
                }

                strSqlString.Append("   AND A.DELETE_FLAG  = ' '" + "\n");
                strSqlString.Append("   AND A.RES_TYPE  = 'EQUIPMENT'" + "\n");
                strSqlString.Append("   AND A.CUTOFF_DT = '" + sDate + "22'" + "\n");

                // 2017-02-28-임종우 : Cu wire 검색 기능 추가 (권순태이사 요청)
                if (ckbCuWire.Checked == true)
                {
                    strSqlString.Append("   AND B.MAT_ID IN (SELECT DISTINCT PARTNUMBER FROM CWIPBOMDEF WHERE RESV_FIELD_2 = 'CW')" + "\n");
                }
            }

            #region 상세 조회에 따른 SQL문 생성
            if (dataArry[0] != " ")
            {
                if (dataArry[0] == "MOLD")
                {
                    strSqlString.AppendFormat("   AND A.RES_ID IN ('M-A57','M-A59','M-A60')" + "\n");
                }
                else if (dataArry[0] == "DA(DFN)")
                {
                    strSqlString.AppendFormat("   AND B.MAT_GRP_2 = 'DFN'" + "\n");
                    strSqlString.AppendFormat("   AND A.RES_GRP_3 = 'DIE ATTACH'" + "\n");
                }
                else if (dataArry[0] == "DA(Epoxy)")
                {
                    strSqlString.AppendFormat("   AND B.MAT_GRP_2 <> 'DFN'" + "\n");
                    strSqlString.AppendFormat("   AND REGEXP_LIKE(A.RES_GRP_6, 'ESEC2100SD|SDB-30UST|^DB*|^SPA*')" + "\n");
                    strSqlString.AppendFormat("   AND A.RES_GRP_3 = 'DIE ATTACH'" + "\n");
                }
                else
                {
                    strSqlString.AppendFormat("   AND A.RES_GRP_3 = '" + dataArry[0] + "'" + "\n");
                }                
            }

            if (dataArry[1] != " ")
                strSqlString.AppendFormat("   AND B.MAT_GRP_1 = '" + dataArry[1] + "'" + "\n");

            if (dataArry[2] != " ")
                strSqlString.AppendFormat("   AND B.MAT_GRP_9 = '" + dataArry[2] + "'" + "\n");

            if (dataArry[3] != " ")
                strSqlString.AppendFormat("   AND B.MAT_GRP_10 = '" + dataArry[3] + "'" + "\n");

            if (dataArry[4] != " ")
                strSqlString.AppendFormat("   AND B.MAT_GRP_5 = '" + dataArry[4] + "'" + "\n");

            if (dataArry[5] != " ")
                strSqlString.AppendFormat("   AND B.MAT_ID = '" + dataArry[5] + "'" + "\n");

            if (dataArry[6] != " ")
                strSqlString.AppendFormat("   AND B.MAT_CMF_11 = '" + dataArry[6] + "'" + "\n");

            if (dataArry[7] != " ")
                strSqlString.AppendFormat("   AND B.CUST_TYPE = '" + dataArry[7] + "'" + "\n");

            if (dataArry[8] != " ")
                strSqlString.AppendFormat("   AND B.MAT_CMF_10 = '" + dataArry[8] + "'" + "\n");
            

            #endregion

            strSqlString.Append(" ORDER BY STATE, RES_GRP_6" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();

        }
        #endregion

        private void cdvOperGroup_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DECODE(ROWNUM, 1, A, 2, B, 3, C, 4, D, 5, E, 6, F, 7, G, 8, H, 9, I, 10, J, 11, K, 12, L, 13, M) AS Code, '' AS Data" + "\n";
            strQuery += "  FROM (SELECT 1 FROM DUAL CONNECT BY LEVEL <= 13) " + "\n";
            strQuery += "     , ( " + "\n";
            strQuery += "        SELECT 'STOCK' AS A, 'LAMI' AS B, 'Stealth Saw' AS C" + "\n";
            strQuery += "             , 'BG' AS D, 'DDS' AS E " + "\n";
            strQuery += "             , 'SAW' AS F, 'DA' AS G " + "\n";
            strQuery += "             , 'DA(Epoxy)' AS H, 'DA(DFN)' AS I " + "\n";
            strQuery += "             , 'WB' AS J, 'C-MOLD' AS K " + "\n";
            strQuery += "             , 'SST' AS L, 'HMK3' AS M " + "\n";
            strQuery += "           FROM DUAL " + "\n";
            strQuery += "       ) " + "\n";

            cdvOperGroup.sDynamicQuery = strQuery;
        }

        private void cdvShift_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DECODE(ROWNUM, 1, A1, 2, B1, 3, C1) AS Code" + "\n";
            strQuery += "     , DECODE(ROWNUM, 1, A2, 2, B2, 3, C2) AS Data" + "\n";
            strQuery += "  FROM (SELECT 1 FROM DUAL CONNECT BY LEVEL <= 3) " + "\n";
            strQuery += "     , ( " + "\n";
            strQuery += "        SELECT 'GY' AS A1, 'DY' AS B1, 'SW' AS C1" + "\n";
            strQuery += "             , '22시~06시' AS A2, '06시~14시' AS B2, '14시~22시' AS C2 " + "\n";
            strQuery += "          FROM DUAL " + "\n";
            strQuery += "       ) " + "\n";

            cdvShift.sDynamicQuery = strQuery;
        }
    }
}

