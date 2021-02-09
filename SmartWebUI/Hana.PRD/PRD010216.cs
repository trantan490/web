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
    public partial class PRD010216 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {        
        /// <summary>
        /// 클  래  스: PRD010216<br/>
        /// 클래스요약: SOP Monitoring<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2013-03-16<br/>
        /// 상세  설명: SOP Monitoring (전 고객사)<br/>
        /// 변경  내용: <br/>
        /// 2013-03-19-임종우 : Major_Code, SAP_Code 추가 & Part_No 검색 조건 추가 (임태성 요청)
        /// 2013-03-21-임종우 : 계획 값 유무에 의한 표시 추가, D0 잔량에 대한 음영표시 추가, 주간 잔량 추가, 재공 로직 수정 (임태성 요청)
        /// 2013-03-28-임종우 : D0 계획 값 로직 변경 = 당일계획 + (전일까지의 누계계획 - 전일까지의 누계실적) (임태성 요청)
        /// 2013-04-16-임종우 : DA 기준 Capa 추가, 일별 출하 계획 추가, DA1~DA4 & WB1~WB4 공정의 Capa 추가 (임태성 요청)
        /// 2013-04-23-임종우 : 주차 Remain 표시 시 -값을 0으로 표시 되는 체크 박스 추가 (김성업 요청)
        /// 2013-04-29-임종우 : 차주 계획 쿼리 수정 - 차주가 존재 여부에 따라 무조건 차주 계획 사용 또는 미사용으로...(개선 사항)
        /// 2013-07-08-김민우 : Remain 값의 경우 WF Input … Mold 순에서  역순으로 변경 요청(임태성K 요청)
        /// 2013-07-08-김민우 : HX의 경우 06 기준으로 실적 값 변경(임태성K 요청)
        /// 2013-08-20-임종우 : 초과달성 선택 시 D0_PLAN 실적 감안하는 로직으로 원복 (김민우가 왜 막았는지 모르겠음....)
        /// 2013-08-26-임종우 : 주차별 계획, 실행계획(월계획), 마감예상(월rev 계획) 데이터 추가 (김권수 요청)
        ///                     계획 테이블 변경 CWIPPLNWEK_N -> RWIPPLNWEK
        /// 2013-08-27-임종우 : 재공 부분 COMP 로직 추가 (임태성 요청)
        /// 2013-08-30-임종우 : 월간 기준 추가(실행계획, 누계실적, 잔량) (임태성 요청)
        /// 2013-09-11-임종우 : KPCS 기능 추가(백성호 요청)
        /// 2013-09-23-임종우 : 가압오븐 공정 (A053%) DA 공정 재공에 포함 (임태성 요청)
        /// 2013-10-11-김민우 : Daily 06시 기준의 재공 확인 필요 (당일도 06시 기준의 재공/실적 확인 필요함) (임태성K 요청)
        ///                     현재 주간 계획 대비 잔량으로 나오는데… 일간 출하계획 대비 공정별 잔량도 나올 수 있도록 추가 필요함.
        ///                     COB 제외
        /// 2013-10-16-김민우 : Daily 06시 기준의 실적 로직 변경(김권수D 요청)                    
        ///                     과거	14일 조회	HX	기존: 14일 06시 ~ 실시간 누계실적	변경: 14일 06시 시점의 실적 = 0
		///                                        그외	기존: 14일 06시 ~ 실시간 누계실적	변경: 13일 22시~14일 06시 까지의 실적
        ///                     현재	15일 조회	HX	기존: 15일 06시 ~ 실시간 누계실적	변경: 15일 06시 시점의 실적 = 0
		///                                        그외	기존: 15일 06시 ~ 실시간 누계실적	변경: 14일 22시~15일 06시 까지의 실적
        /// 2013-10-21-임종우 : SE 제외 나머지 업체에 대해 WB 재공에 Gate(A0801~A0804) Merge 재공 표시, 마지막 차수의 Gate 재공은 Gate 재공에 표시
        /// 2013-12-01-김민우 : WIP DA1 - A0250,A0333,A3040 추가         
        /// 2013-12-04-임종우 : CAPA - DA, DA1 에 A0333 추가
        /// 2013-12-06-임종우 : 삼성 메모리의 경우 싱글, 스택 제품 구분없이 무조건 'SEK_________-___' || SUBSTR(MAT_ID, -3) 으로 표시 (임태성 요청)
        /// 2013-12-12-임종우 : WIP DA1 - A0310 추가
        /// 2013-12-24-임종우 : 설비 효율 통일화 -> DA : 70%, WB : 75% (임태성 요청)
        /// 2013-12-26-임종우 : 재공 TIN 공정을 M/K 와 CURE 사이로 변경 (김권수 요청)
        /// 2014-06-20-임종우 : 월간 실행 계획 변경 - 기존 ORI_PLAN 에서 생산관리 업로드 RESV_FIELD1 계획으로 변경 (임태성 요청)
        /// 2014-09-02-임종우 : HX 제품 재공 -> Process 상에 A0015가 있는 제품의 경우 [(A0000~A0015) / Auto loss + A0016 이후 재공의 합] (임태성K 요청)
        ///                                     Process 상에 A0395가 있는 제품의 경우 [(A0000~A0395) / Auto loss + A0396 이후 재공의 합]
        ///                                     Process 상에 A0015, A0395 둘다 있는 제품 [(A0000~A0015) / Auto loss] + [(A0016~A035) / (Auto loss/2)] + A0396 이후 재공의 합
        /// 2014-10-13-임종우 : WIP DA1 - A0370, A0380 공정 추가 (김권수D 요청)
        /// 2014-10-28-임종우 : C200 코드의 설비 제외 (임태성K 요청)
        /// 2014-10-29-임종우 : 실적에서 반품 입고 수량 차감시킴 - RSUMFACMOV -> VSUMWIPOUT 으로 변경 (임태성K 요청)
        /// 2014-11-06-임종우 : B199 코드의 설비 제외 (임태성K 요청)
        /// 2015-01-09-임종우 : WIP AVI 공정에서 DC1(A1790), DC2(A1795) 공정 분리 (김성업D 요청)
        /// 2015-02-17-임종우 : 9단 STACK 제품까지 표시 되도록 추가 (임태성K 요청)
        /// 2015-02-25-임종우 : PVI 재공 - A2030 공정 추가 (임태성K 요청)
        /// 2015-03-05-임종우 : HOLD, NON HOLD 검색 기능 추가 (김성업D 요청)
        ///                     PLASMA, BAKE 추가 (김성업D 요청)
        /// 2015-07-06-임종우 : 설비 대수 집계 시 DISPATCH 기준 정보가 'Y" 인 설비만 집계 (임태성K 요청)
        /// 2015-08-10-임종우 : PLASMA 재공 - A0900 공정 추가 (김성업D 요청)
        /// 2015-09-15-임종우 : C200, B199 설비 제외시 해당코드로 'Down' 된 설비만 제외 (김보람 요청)
        /// 2016-02-18-임종우 : PLASMA 공정 - A0910, A0920, A0930 공정 추가 (임태성K 요청)    
        /// 2016-02-25-임종우 : MOLD 공정 - A0980 공정 추가 (임태성K 요청)
        /// 2016-07-12-임종우 : CAPA 효율 GlobalVariable 로 선언하여 변경함.
        /// 2016-12-27-임종우 : 주 계획 - Memory(OTD:토~금), SLSI(SE:월~일) 기준 분리 (최연희D 요청)
        /// 2017-07-31-임종우 : QC GATE 재공 - A2300 공정 추가 (임태성C 요청)
        /// 2018-01-22-임종우 : SubTotal, GrandTotal 백분율 구하기 Function 변경
        /// 2018-10-18-임종우 : WIP D1 - A0395 공정 추가 (김성업과장 요청)
        /// 2019-03-12-임종우 : 당일실적, 주간실적에서 Return 실적 제외 SHP_QTY_1 > 0 (박형순대리 요청)
        /// </summary>
        GlobalVariable.FindWeek FindWeek_SOP_A = new GlobalVariable.FindWeek();
        GlobalVariable.FindWeek FindWeek_SOP_SE = new GlobalVariable.FindWeek();

        static string[] DateArray = new string[5];
        static string[] DateArray2 = new string[5];

        public PRD010216()
        {
            InitializeComponent();
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();

            cboHolddiv.SelectedIndex = 0;
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
            GetWorkDay();
            spdData.RPT_ColumnInit();
            int autoRow = 0;
            
            try
            {
                int[] idxBox;
                spdData.RPT_AddBasicColumn("Customer", 0, autoRow, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Major Code", 0, ++autoRow, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Family", 0, ++autoRow, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Package", 0, ++autoRow, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 0, ++autoRow, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type2", 0, ++autoRow, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD Count", 0, ++autoRow, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Density", 0, ++autoRow, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Generation", 0, ++autoRow, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Pin Type", 0, ++autoRow, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Product", 0, ++autoRow, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70); ;
                spdData.RPT_AddBasicColumn("Cust Device", 0, ++autoRow, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("SAP Code", 0, ++autoRow, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("PKG Code", 0, ++autoRow, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);

                //if (cdvWeek.Text == "당주+차주")
                if (cdvWeek.SelectedIndex == 1)
                {
                    spdData.RPT_AddBasicColumn("WW" + FindWeek_SOP_A.ThisWeek.Substring(4) + " ~ WW" + FindWeek_SOP_A.NextWeek.Substring(4), 0, ++autoRow, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("WW" + FindWeek_SOP_A.ThisWeek.Substring(4), 0, ++autoRow, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                }

                spdData.RPT_AddBasicColumn("plan", 1, autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Cumulative Performance", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                idxBox = spanHelper("plan", "residual quantity");
                spdData.RPT_MerageHeaderColumnSpan(0, idxBox[0], idxBox[1]);

                //if (cdvWeek.Text == "일간")
                if (cdvWeek.SelectedIndex == 2)
                {
                    spdData.RPT_AddBasicColumn("Remain on the day", 0, ++autoRow, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("WW" + FindWeek_SOP_A.ThisWeek.Substring(4) + " Remain", 0, ++autoRow, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                }



                spdData.RPT_AddBasicColumn("Mold", 1, autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("BG", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Input", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WF", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                idxBox = spanHelper("Mold", "WF");
                spdData.RPT_MerageHeaderColumnSpan(0, idxBox[0], idxBox[1]);

                spdData.RPT_AddBasicColumn("Capa", 0, ++autoRow, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("DA standard", 1, autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB Standard", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                idxBox = spanHelper("DA standard", "WB Standard");
                spdData.RPT_MerageHeaderColumnSpan(0, idxBox[0], idxBox[1]);

                spdData.RPT_AddBasicColumn(DateArray[0].ToString(), 0, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("plan", 1, autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("actual", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                idxBox = spanHelper("plan", "actual");
                spdData.RPT_MerageHeaderColumnSpan(0, idxBox[0], idxBox[1]);

                spdData.RPT_AddBasicColumn("WIP", 0, ++autoRow, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("HMKA3", 1, autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("QC_GATE", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("PVI", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("BAKE", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("AVI", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DC2", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DC1", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("T/F", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("MARK2", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("SIG", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("P&P", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("AOI", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("SBA", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("OSP", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("FLIPPER", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("TRIM", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("MARK", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TIN", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("CURE", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("MOLD", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("PLASMA", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("GATE", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB9", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA9", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB8", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA8", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB7", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA7", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB6", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA6", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB5", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA5", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB4", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA4", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB3", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA3", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB2", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA2", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB1", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA1", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("S/P", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("SAW", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("B/G", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("HMKA2", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                idxBox = spanHelper("HMKA3", "HMKA2");
                spdData.RPT_MerageHeaderColumnSpan(0, idxBox[0], idxBox[1]);

                spdData.RPT_AddBasicColumn("Shipping plan", 0, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(DateArray[0].ToString(), 1, autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(DateArray[1].ToString(), 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(DateArray[2].ToString(), 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(DateArray[3].ToString(), 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(DateArray[4].ToString(), 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                idxBox = spanHelper(DateArray[0].ToString(), DateArray[4].ToString());
                spdData.RPT_MerageHeaderColumnSpan(0, idxBox[0], idxBox[1]);

                spdData.RPT_AddBasicColumn("Equipment Arrange Capa Status", 0, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("DA1", 1, autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB1", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA2", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB2", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA3", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB3", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA4", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB4", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA5", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB5", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA6", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB6", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA7", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB7", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA8", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB8", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA9", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB9", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                idxBox = spanHelper("DA1", "WB9");
                spdData.RPT_MerageHeaderColumnSpan(0, idxBox[0], idxBox[1]);

                spdData.RPT_AddBasicColumn("Plan by work week", 0, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("WW" + FindWeek_SOP_A.NextWeek.Substring(4, 2), 1, autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WW" + FindWeek_SOP_A.W2_Week.Substring(4, 2), 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WW" + FindWeek_SOP_A.W3_Week.Substring(4, 2), 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WW" + FindWeek_SOP_A.W4_Week.Substring(4, 2), 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                idxBox = spanHelper("WW" + FindWeek_SOP_A.NextWeek.Substring(4, 2), "WW" + FindWeek_SOP_A.W4_Week.Substring(4, 2));
                spdData.RPT_MerageHeaderColumnSpan(0, idxBox[0], idxBox[1]);

                spdData.RPT_AddBasicColumn("monthly", 0, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Action Plan", 1, autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Cumulative Performance", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                idxBox = spanHelper("Action Plan", "residual quantity");
                spdData.RPT_MerageHeaderColumnSpan(0, idxBox[0], idxBox[1]);

                spdData.RPT_AddBasicColumn("SOP deadline standard", 0, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Closing forecast", 1, autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Achievement rate", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                idxBox = spanHelper("Closing forecast", "Achievement rate");
                spdData.RPT_MerageHeaderColumnSpan(0, idxBox[0], idxBox[1]);
                

                for (int i = 0; i <= 13; i++)
                {
                    spdData.RPT_MerageHeaderRowSpan(0, i, 2);
                }
                
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT.MAT_GRP_1", "MAT.MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", "CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Major Code", "MAT.MAT_GRP_9", "MAT.MAT_GRP_9", "MAT.MAT_GRP_9", "MAT_GRP_9", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2", "MAT_GRP_2", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3", "MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4", "MAT_GRP_4", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", "MAT_GRP_6", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10", "MAT_CMF_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.CONV_MAT_ID", "MAT.MAT_ID", "MAT.CONV_MAT_ID", "CONV_MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "MAT.MAT_CMF_7", "MAT.MAT_CMF_7", "MAT.MAT_CMF_7", "MAT_CMF_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SAP Code", "MAT.SAP_CODE", "MAT.SAP_CODE", "MAT.SAP_CODE", "SAP_CODE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG Code", "MAT.MAT_CMF_11", "MAT.MAT_CMF_11", "MAT.MAT_CMF_11", "MAT_CMF_11", false);
          
        }
        #endregion


        #region 시간관련 함수
        private void GetWorkDay()
        {
            DateTime Now = cdvDate.Value;
            FindWeek_SOP_A = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");
            FindWeek_SOP_SE = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "SE");

            for (int i = 0; i < 5; i++)
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
            string QueryCond4;
            string Yesterday;
            string Yesterdaybf;
            string Today;
            string TodayAdd;
            string sWeek;
            string sMonth;
            string sStartDate;
            string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값            

            Today = cdvDate.Value.ToString("yyyyMMdd");
            Yesterday = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");
            Yesterdaybf = cdvDate.Value.AddDays(-2).ToString("yyyyMMdd");
            sMonth = cdvDate.Value.ToString("yyyyMM");
            TodayAdd = cdvDate.Value.AddDays(+1).ToString("yyyyMMdd");

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            // kpcs 선택에 의한 분모 값 저장 한다.
            if (ckbKpcs.Checked == true)
            {     
                sKpcsValue = "1000";            
            }
            else
            {
                sKpcsValue = "1";
            }

            //if (cdvWeek.Text == "당주+차주")
            if (cdvWeek.SelectedIndex == 1)
            {
                sWeek = "NVL(WEEK_TTL, 0)"; 
            }
            else
            {
                sWeek = "NVL(WEEK1_PLAN, 0)";
            }
            
            // 조회월과 조회주차의 시작일이 같은 달이면 시작은 조회월의 1일자로 하고, 다르면(주차시작일이 작으면) 주차 시작일을 시작일로 한다.
            if (sMonth == FindWeek_SOP_A.StartDay_ThisWeek.Substring(0, 6))
            {
                sStartDate = sMonth + "01";
            }
            else
            {
                sStartDate = FindWeek_SOP_A.StartDay_ThisWeek;
            }
            
            //if (cdvWeek.Text == "일간")
            if (cdvWeek.SelectedIndex == 2)
            {
                strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond4);
                strSqlString.Append("     , WEEK_PLAN, SHP_WEEK, WEEK_DEF" + "\n");
                strSqlString.Append("     , (D0_PLAN2 - SHP_TODAY - W_MOLD)  AS REMAIN_MOLD" + "\n");
                strSqlString.Append("     , (D0_PLAN2 - SHP_TODAY - W_WB)  AS REMAIN_WB" + "\n");
                strSqlString.Append("     , (D0_PLAN2 - SHP_TODAY - W_DA)  AS REMAIN_DA" + "\n");
                strSqlString.Append("     , (D0_PLAN2 - SHP_TODAY - W_BG)  AS REMAIN_BG" + "\n");
                strSqlString.Append("     , (D0_PLAN2 - SHP_TODAY - W_ISSUE)  AS REMAIN_INPUT" + "\n");
                strSqlString.Append("     , (D0_PLAN2 - SHP_TODAY - TOTAL)  AS REMAIN_WF" + "\n");
                strSqlString.Append("     , DA_CAPA, WB_CAPA, D0_PLAN, D0_DEF, SHP_TODAY, HMK3A, QC_GATE, PVI, BAKE, AVI, DC2, DC1, TF, MK2, SIG" + "\n");
                strSqlString.Append("     , PP, AOI, SBA, OSP, FLIPPER" + "\n");
                strSqlString.Append("     , TRIM, MK, TIN, CURE, MOLD, PLASMA, GATE" + "\n");
                strSqlString.Append("     , WB9, DA9, WB8, DA8, WB7, DA7, WB6, DA6, WB5, DA5, WB4, DA4, WB3, DA3, WB2, DA2, WB1, DA1, SP, SAW, BG, HMK2A" + "\n");
                strSqlString.Append("     , D0_PLAN, D1_PLAN, D2_PLAN, D3_PLAN, D4_PLAN, DA1_CAPA, WB1_CAPA, DA2_CAPA, WB2_CAPA, DA3_CAPA, WB3_CAPA, DA4_CAPA, WB4_CAPA, DA5_CAPA, WB5_CAPA" + "\n");
                strSqlString.Append("     , DA6_CAPA, WB6_CAPA, DA7_CAPA, WB7_CAPA, DA8_CAPA, WB8_CAPA, DA9_CAPA, WB9_CAPA, WEEK2_PLAN, WEEK3_PLAN, WEEK4_PLAN, WEEK5_PLAN, ORI_PLAN, SHP_MONTH, MONTH_DEF, REV_PLAN, PER" + "\n");
            }
            else
            {
                strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond4);
                strSqlString.Append("     , WEEK_PLAN, SHP_WEEK, WEEK_DEF, REMAIN_MOLD, REMAIN_WB, REMAIN_DA, REMAIN_BG, REMAIN_INPUT, REMAIN_WF, DA_CAPA, WB_CAPA" + "\n");
                strSqlString.Append("     , D0_PLAN2, D0_DEF, SHP_TODAY, HMK3A, QC_GATE, PVI, BAKE, AVI, DC2, DC1, TF, MK2, SIG" + "\n");
                strSqlString.Append("     , PP, AOI, SBA, OSP, FLIPPER" + "\n");
                strSqlString.Append("     , TRIM, MK, TIN, CURE, MOLD, PLASMA, GATE" + "\n");
                strSqlString.Append("     , WB9, DA9, WB8, DA8, WB7, DA7, WB6, DA6, WB5, DA5, WB4, DA4, WB3, DA3, WB2, DA2, WB1, DA1, SP, SAW, BG, HMK2A" + "\n");
                strSqlString.Append("     , D0_PLAN, D1_PLAN, D2_PLAN, D3_PLAN, D4_PLAN, DA1_CAPA, WB1_CAPA, DA2_CAPA, WB2_CAPA, DA3_CAPA, WB3_CAPA, DA4_CAPA, WB4_CAPA, DA5_CAPA, WB5_CAPA" + "\n");
                strSqlString.Append("     , DA6_CAPA, WB6_CAPA, DA7_CAPA, WB7_CAPA, DA8_CAPA, WB8_CAPA, DA9_CAPA, WB9_CAPA, WEEK2_PLAN, WEEK3_PLAN, WEEK4_PLAN, WEEK5_PLAN, ORI_PLAN, SHP_MONTH, MONTH_DEF, REV_PLAN, PER" + "\n");
            }
            strSqlString.Append("  FROM(" + "\n");
            
            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond3);
            strSqlString.Append("     , ROUND(SUM(" +  sWeek + ") / " + sKpcsValue + ", 0) AS WEEK_PLAN" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(SHP_WEEK, 0)) / " + sKpcsValue + ", 0) AS SHP_WEEK" + "\n");

            if (ckbZero.Checked == true)
            {
                strSqlString.Append("     , CASE WHEN SUM(" + sWeek + " - NVL(SHP_WEEK, 0)) < 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE ROUND(SUM(" + sWeek + " - NVL(SHP_WEEK, 0)) / " + sKpcsValue + ", 0) END AS WEEK_DEF" + "\n");


                strSqlString.Append("     , CASE WHEN SUM(" + sWeek + " - NVL(SHP_WEEK, 0) - NVL(W_MOLD, 0)) < 0 THEN 0 " + "\n");
                strSqlString.Append("            ELSE ROUND(SUM(" + sWeek + " - NVL(SHP_WEEK, 0) - NVL(W_MOLD, 0)) / " + sKpcsValue + ", 0) END AS REMAIN_MOLD" + "\n");
                strSqlString.Append("     , CASE WHEN SUM(" + sWeek + " - NVL(SHP_WEEK, 0) - NVL(W_WB, 0)) < 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE ROUND(SUM(" + sWeek + " - NVL(SHP_WEEK, 0) - NVL(W_WB, 0)) / " + sKpcsValue + ", 0) END AS REMAIN_WB" + "\n");
                strSqlString.Append("     , CASE WHEN SUM(" + sWeek + " - NVL(SHP_WEEK, 0) - NVL(W_DA, 0)) < 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE ROUND(SUM(" + sWeek + " - NVL(SHP_WEEK, 0) - NVL(W_DA, 0)) / " + sKpcsValue + ", 0) END AS REMAIN_DA" + "\n");
                strSqlString.Append("     , CASE WHEN SUM(" + sWeek + " - NVL(SHP_WEEK, 0) - NVL(W_BG, 0)) < 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE ROUND(SUM(" + sWeek + " - NVL(SHP_WEEK, 0) - NVL(W_BG, 0)) / " + sKpcsValue + ", 0) END AS REMAIN_BG" + "\n");
                strSqlString.Append("     , CASE WHEN SUM(" + sWeek + " - NVL(SHP_WEEK, 0) - NVL(W_ISSUE, 0)) < 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE ROUND(SUM(" + sWeek + " - NVL(SHP_WEEK, 0) - NVL(W_ISSUE, 0)) / " + sKpcsValue + ", 0) END AS REMAIN_INPUT" + "\n");
                strSqlString.Append("     , CASE WHEN SUM(" + sWeek + " - NVL(SHP_WEEK, 0) - NVL(TOTAL, 0)) < 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE ROUND(SUM(" + sWeek + " - NVL(SHP_WEEK, 0) - NVL(TOTAL, 0)) / " + sKpcsValue + ", 0) END AS REMAIN_WF" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(DA_CAPA, 0)) / " + sKpcsValue + ", 0) AS DA_CAPA" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WB_CAPA, 0)) / " + sKpcsValue + ", 0) AS WB_CAPA" + "\n");
                strSqlString.Append("     , CASE WHEN SUM(NVL(D0_PLAN, 0) + (NVL(WEEK1_TTL, 0) - NVL(SHP_WEEK_TTL, 0))) < 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE ROUND(SUM(NVL(D0_PLAN, 0) + (NVL(WEEK1_TTL, 0) - NVL(SHP_WEEK_TTL, 0))) / " + sKpcsValue + ", 0) END AS D0_PLAN2" + "\n");
                strSqlString.Append("     , CASE WHEN SUM(NVL(D0_PLAN, 0) + (NVL(WEEK1_TTL, 0) - NVL(SHP_WEEK_TTL, 0)) - NVL(SHP_TODAY, 0)) < 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE ROUND(SUM(NVL(D0_PLAN, 0) + (NVL(WEEK1_TTL, 0) - NVL(SHP_WEEK_TTL, 0)) - NVL(SHP_TODAY, 0)) / " + sKpcsValue + ", 0) END AS D0_DEF" + "\n");
            }
            else
            {
                strSqlString.Append("     , ROUND(SUM(" + sWeek + " - NVL(SHP_WEEK, 0)) / " + sKpcsValue + ", 0) AS WEEK_DEF" + "\n");
                strSqlString.Append("     , ROUND(SUM(" + sWeek + " - NVL(SHP_WEEK, 0) - NVL(W_MOLD, 0)) / " + sKpcsValue + ", 0) AS REMAIN_MOLD " + "\n");
                strSqlString.Append("     , ROUND(SUM(" + sWeek + " - NVL(SHP_WEEK, 0) - NVL(W_WB, 0)) / " + sKpcsValue + ", 0) AS REMAIN_WB" + "\n");
                strSqlString.Append("     , ROUND(SUM(" + sWeek + " - NVL(SHP_WEEK, 0) - NVL(W_DA, 0)) / " + sKpcsValue + ", 0) AS REMAIN_DA" + "\n");
                strSqlString.Append("     , ROUND(SUM(" + sWeek + " - NVL(SHP_WEEK, 0) - NVL(W_BG, 0)) / " + sKpcsValue + ", 0) AS REMAIN_BG" + "\n");
                strSqlString.Append("     , ROUND(SUM(" + sWeek + " - NVL(SHP_WEEK, 0) - NVL(W_ISSUE, 0)) / " + sKpcsValue + ", 0) AS REMAIN_INPUT" + "\n");
                strSqlString.Append("     , ROUND(SUM(" + sWeek + " - NVL(SHP_WEEK, 0) - NVL(TOTAL, 0)) / " + sKpcsValue + ", 0) AS REMAIN_WF" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(DA_CAPA, 0)) / " + sKpcsValue + ", 0) AS DA_CAPA" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WB_CAPA, 0)) / " + sKpcsValue + ", 0) AS WB_CAPA" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(D0_PLAN, 0) + (NVL(WEEK1_TTL, 0) - NVL(SHP_WEEK_TTL, 0))) / " + sKpcsValue + ", 0) AS D0_PLAN2" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(D0_PLAN, 0) + (NVL(WEEK1_TTL, 0) - NVL(SHP_WEEK_TTL, 0)) - NVL(SHP_TODAY, 0)) / " + sKpcsValue + ", 0) AS D0_DEF" + "\n");
            }
            strSqlString.Append("     , ROUND(SUM(NVL(SHP_TODAY, 0)) / " + sKpcsValue + ", 0) AS SHP_TODAY" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.HMK3A, 0)) / " + sKpcsValue + ", 0) AS HMK3A" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.QC_GATE, 0)) / " + sKpcsValue + ", 0) AS QC_GATE" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.PVI, 0)) / " + sKpcsValue + ", 0) AS PVI" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.BAKE,0)) / " + sKpcsValue + ", 0) AS BAKE" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.AVI,0)) / " + sKpcsValue + ", 0) AS AVI" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.DC2,0)) / " + sKpcsValue + ", 0) AS DC2" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.DC1,0)) / " + sKpcsValue + ", 0) AS DC1" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.TF,0)) / " + sKpcsValue + ", 0) AS TF" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.MK2,0)) / " + sKpcsValue + ", 0) AS MK2" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.SIG,0)) / " + sKpcsValue + ", 0) AS SIG" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.SBA,0)) / " + sKpcsValue + ", 0) AS SBA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.OSP,0)) / " + sKpcsValue + ", 0) AS OSP" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.AOI,0)) / " + sKpcsValue + ", 0) AS AOI" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.FLIPPER,0)) / " + sKpcsValue + ", 0) AS FLIPPER" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.PP,0)) / " + sKpcsValue + ", 0) AS PP" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.TIN,0)) / " + sKpcsValue + ", 0) AS TIN" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.TRIM,0)) / " + sKpcsValue + ", 0) AS TRIM" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.MK,0)) / " + sKpcsValue + ", 0) AS MK" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.CURE,0)) / " + sKpcsValue + ", 0) AS CURE" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.MOLD,0)) / " + sKpcsValue + ", 0) AS MOLD" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.PLASMA,0)) / " + sKpcsValue + ", 0) AS PLASMA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.GATE,0)) / " + sKpcsValue + ", 0) AS GATE" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.WB9,0)) / " + sKpcsValue + ", 0) AS WB9" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.DA9,0)) / " + sKpcsValue + ", 0) AS DA9" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.WB8,0)) / " + sKpcsValue + ", 0) AS WB8" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.DA8,0)) / " + sKpcsValue + ", 0) AS DA8" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.WB7,0)) / " + sKpcsValue + ", 0) AS WB7" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.DA7,0)) / " + sKpcsValue + ", 0) AS DA7" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.WB6,0)) / " + sKpcsValue + ", 0) AS WB6" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.DA6,0)) / " + sKpcsValue + ", 0) AS DA6" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.WB5,0)) / " + sKpcsValue + ", 0) AS WB5" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.DA5,0)) / " + sKpcsValue + ", 0) AS DA5" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.WB4,0)) / " + sKpcsValue + ", 0) AS WB4" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.DA4,0)) / " + sKpcsValue + ", 0) AS DA4" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.WB3,0)) / " + sKpcsValue + ", 0) AS WB3" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.DA3,0)) / " + sKpcsValue + ", 0) AS DA3" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.WB2,0)) / " + sKpcsValue + ", 0) AS WB2" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.DA2,0)) / " + sKpcsValue + ", 0) AS DA2" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.WB1,0)) / " + sKpcsValue + ", 0) AS WB1" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.DA1,0)) / " + sKpcsValue + ", 0) AS DA1" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.SP,0)) / " + sKpcsValue + ", 0) AS SP" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.SAW,0)) / " + sKpcsValue + ", 0) AS SAW" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.BG,0)) / " + sKpcsValue + ", 0) AS BG" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.HMK2A,0)) / " + sKpcsValue + ", 0) AS HMK2A" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(D0_PLAN, 0) + (NVL(WEEK1_TTL, 0) - NVL(SHP_WEEK_TTL, 0))) / " + sKpcsValue + ", 0) AS D0_PLAN" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(D1_PLAN, 0)) / " + sKpcsValue + ", 0) AS D1_PLAN" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(D2_PLAN, 0)) / " + sKpcsValue + ", 0) AS D2_PLAN" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(D3_PLAN, 0)) / " + sKpcsValue + ", 0) AS D3_PLAN" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(D4_PLAN, 0)) / " + sKpcsValue + ", 0) AS D4_PLAN" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(DA1_CAPA, 0)) / " + sKpcsValue + ", 0) AS DA1_CAPA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WB1_CAPA, 0)) / " + sKpcsValue + ", 0) AS WB1_CAPA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(DA2_CAPA, 0)) / " + sKpcsValue + ", 0) AS DA2_CAPA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WB2_CAPA, 0)) / " + sKpcsValue + ", 0) AS WB2_CAPA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(DA3_CAPA, 0)) / " + sKpcsValue + ", 0) AS DA3_CAPA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WB3_CAPA, 0)) / " + sKpcsValue + ", 0) AS WB3_CAPA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(DA4_CAPA, 0)) / " + sKpcsValue + ", 0) AS DA4_CAPA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WB4_CAPA, 0)) / " + sKpcsValue + ", 0) AS WB4_CAPA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(DA5_CAPA, 0)) / " + sKpcsValue + ", 0) AS DA5_CAPA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WB5_CAPA, 0)) / " + sKpcsValue + ", 0) AS WB5_CAPA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(DA6_CAPA, 0)) / " + sKpcsValue + ", 0) AS DA6_CAPA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WB6_CAPA, 0)) / " + sKpcsValue + ", 0) AS WB6_CAPA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(DA7_CAPA, 0)) / " + sKpcsValue + ", 0) AS DA7_CAPA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WB7_CAPA, 0)) / " + sKpcsValue + ", 0) AS WB7_CAPA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(DA8_CAPA, 0)) / " + sKpcsValue + ", 0) AS DA8_CAPA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WB8_CAPA, 0)) / " + sKpcsValue + ", 0) AS WB8_CAPA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(DA9_CAPA, 0)) / " + sKpcsValue + ", 0) AS DA9_CAPA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WB9_CAPA, 0)) / " + sKpcsValue + ", 0) AS WB9_CAPA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WEEK2_PLAN, 0)) / " + sKpcsValue + ", 0) AS WEEK2_PLAN" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WEEK3_PLAN, 0)) / " + sKpcsValue + ", 0) AS WEEK3_PLAN" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WEEK4_PLAN, 0)) / " + sKpcsValue + ", 0) AS WEEK4_PLAN" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WEEK5_PLAN, 0)) / " + sKpcsValue + ", 0) AS WEEK5_PLAN" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(ORI_PLAN, 0)) / " + sKpcsValue + ", 0) AS ORI_PLAN" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(SHP_MONTH, 0)) / " + sKpcsValue + ", 0) AS SHP_MONTH" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(ORI_PLAN, 0) - NVL(SHP_MONTH, 0)) / " + sKpcsValue + ", 0) AS MONTH_DEF" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(REV_PLAN, 0)) / " + sKpcsValue + ", 0) AS REV_PLAN" + "\n");
            strSqlString.Append("     , CASE WHEN SUM(NVL(ORI_PLAN, 0)) = 0 THEN 0" + "\n");
            strSqlString.Append("            ELSE ROUND(SUM(NVL(REV_PLAN, 0)) / SUM(NVL(ORI_PLAN, 0)) * 100, 0)" + "\n");
            strSqlString.Append("       END PER" + "\n");

            strSqlString.Append("     , ROUND(SUM(NVL(W_MOLD, 0)) / " + sKpcsValue + ", 0) AS W_MOLD " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(W_WB, 0)) / " + sKpcsValue + ", 0) AS W_WB" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(W_DA, 0)) / " + sKpcsValue + ", 0) AS W_DA" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(W_BG, 0)) / " + sKpcsValue + ", 0) AS W_BG" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(W_ISSUE, 0)) / " + sKpcsValue + ", 0) AS W_ISSUE" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(TOTAL, 0)) / " + sKpcsValue + ", 0) AS TOTAL" + "\n");
            

            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6" + "\n");
            strSqlString.Append("             , MAT_GRP_7, MAT_GRP_8, MAT_GRP_9, MAT_GRP_10, MAT_CMF_7, MAT_CMF_10, MAT_ID, MAT_CMF_11" + "\n");
            strSqlString.Append("             , CASE WHEN MAT_GRP_9 = 'MEMORY' AND MAT_GRP_1 = 'HX' AND INSTR(MAT_ID, '_') = 0 THEN SUBSTR(MAT_ID, 1, 12) || '%' || SUBSTR(MAT_ID, -3)" + "\n");
            strSqlString.Append("                    WHEN MAT_GRP_9 = 'MEMORY' AND MAT_GRP_1 = 'HX' AND INSTR(MAT_ID, '_') > 0 THEN SUBSTR(MAT_ID, 1, INSTR(MAT_ID, '_')) || '%' || SUBSTR(MAT_ID, -3)" + "\n");
            //strSqlString.Append("                    WHEN MAT_GRP_9 = 'MEMORY' AND MAT_GRP_1 = 'SE' AND MAT_GRP_4 IN ('-','FD','FU') THEN SUBSTR(MAT_ID, 1, INSTR(MAT_ID, '-')) || '___' || SUBSTR(MAT_ID, -3)" + "\n");
            strSqlString.Append("                    WHEN MAT_GRP_9 = 'MEMORY' AND MAT_GRP_1 = 'SE' THEN 'SEK_________-___' || SUBSTR(MAT_ID, -3)" + "\n");
            strSqlString.Append("                    ELSE MAT_ID" + "\n");
            strSqlString.Append("               END CONV_MAT_ID" + "\n");
            strSqlString.Append("             , VENDOR_ID AS SAP_CODE" + "\n");
            strSqlString.Append("          FROM MWIPMATDEF" + "\n");
            strSqlString.Append("         WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("           AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("           AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND (MAT_GRP_5 IN ('1st','Merge', '-') OR MAT_GRP_5 LIKE 'Middle%') " + "\n");
            strSqlString.Append("           AND MAT_GRP_3 <> 'COB' " + "\n");

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

            strSqlString.Append("           AND MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");
            strSqlString.Append("       ) MAT " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT FACTORY, MAT_ID " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D0_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D0_QTY + D1_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D0_QTY + D1_QTY + D2_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D0_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D0_QTY + D1_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D0_QTY + D1_QTY + D2_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY " + "\n");
            strSqlString.Append("                        ELSE 0" + "\n");
            strSqlString.Append("                   END) AS WEEK1_TTL " + "\n");
            strSqlString.Append("             , SUM(W1_QTY) AS WEEK1_PLAN " + "\n");
            strSqlString.Append("             , SUM(W2_QTY) AS WEEK2_PLAN " + "\n");
            strSqlString.Append("             , SUM(W3_QTY) AS WEEK3_PLAN " + "\n");
            strSqlString.Append("             , SUM(W4_QTY) AS WEEK4_PLAN " + "\n");
            strSqlString.Append("             , SUM(W5_QTY) AS WEEK5_PLAN " + "\n");
            strSqlString.Append("             , SUM(NVL(W1_QTY,0) + NVL(W2_QTY,0)) AS WEEK_TTL " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D0_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D1_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D2_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D3_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D4_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D5_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D6_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D0_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D1_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D2_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D3_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D4_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D5_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D6_QTY " + "\n");
            strSqlString.Append("                        ELSE 0" + "\n");
            strSqlString.Append("                   END) AS D0_PLAN " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D1_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D2_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D3_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D4_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D5_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D6_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D7_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D1_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D2_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D3_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D4_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D5_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D6_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D7_QTY " + "\n");
            strSqlString.Append("                        ELSE 0" + "\n");
            strSqlString.Append("                   END) AS D1_PLAN " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D2_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D3_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D4_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D5_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D6_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D7_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D8_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D2_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D3_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D4_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D5_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D6_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D7_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D8_QTY " + "\n");
            strSqlString.Append("                        ELSE 0" + "\n");
            strSqlString.Append("                   END) AS D2_PLAN " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D3_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D4_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D5_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D6_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D7_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D8_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D9_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D3_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D4_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D5_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D6_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D7_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D8_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D9_QTY " + "\n");
            strSqlString.Append("                        ELSE 0" + "\n");
            strSqlString.Append("                   END) AS D3_PLAN " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D4_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D5_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D6_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D7_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D8_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D9_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D10_QTY" + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D4_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D5_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D6_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D7_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D8_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D9_QTY " + "\n");
            strSqlString.Append("                        WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D10_QTY " + "\n");
            strSqlString.Append("                        ELSE 0" + "\n");
            strSqlString.Append("                   END) AS D4_PLAN " + "\n");            
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT FACTORY, MAT_ID, 'OTD' AS CALENDAR_ID " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D0_QTY, 0)) AS D0_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D1_QTY, 0)) AS D1_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D2_QTY, 0)) AS D2_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D3_QTY, 0)) AS D3_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D4_QTY, 0)) AS D4_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D5_QTY, 0)) AS D5_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D6_QTY, 0)) AS D6_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D0_QTY, 0)) AS D7_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D1_QTY, 0)) AS D8_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D2_QTY, 0)) AS D9_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D3_QTY, 0)) AS D10_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D4_QTY, 0)) AS D11_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D5_QTY, 0)) AS D12_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D6_QTY, 0)) AS D13_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', WW_QTY, 0)) AS W1_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', WW_QTY, 0)) AS W2_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.W2_Week + "', WW_QTY, 0)) AS W3_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.W3_Week + "', WW_QTY, 0)) AS W4_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.W4_Week + "', WW_QTY, 0)) AS W5_QTY " + "\n");
            strSqlString.Append("                  FROM RWIPPLNWEK " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND GUBUN = '3' " + "\n");
            strSqlString.Append("                   AND MAT_ID NOT LIKE 'SES%' " + "\n");
            strSqlString.Append("                   AND PLAN_WEEK IN ('" + FindWeek_SOP_A.ThisWeek + "','" + FindWeek_SOP_A.NextWeek + "','" + FindWeek_SOP_A.W2_Week + "','" + FindWeek_SOP_A.W3_Week + "','" + FindWeek_SOP_A.W4_Week + "')" + "\n");            
            strSqlString.Append("                 GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                 UNION ALL " + "\n");
            strSqlString.Append("                SELECT FACTORY, MAT_ID, 'SE' AS CALENDAR_ID " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.ThisWeek + "', D0_QTY, 0)) AS D0_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.ThisWeek + "', D1_QTY, 0)) AS D1_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.ThisWeek + "', D2_QTY, 0)) AS D2_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.ThisWeek + "', D3_QTY, 0)) AS D3_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.ThisWeek + "', D4_QTY, 0)) AS D4_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.ThisWeek + "', D5_QTY, 0)) AS D5_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.ThisWeek + "', D6_QTY, 0)) AS D6_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.NextWeek + "', D0_QTY, 0)) AS D7_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.NextWeek + "', D1_QTY, 0)) AS D8_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.NextWeek + "', D2_QTY, 0)) AS D9_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.NextWeek + "', D3_QTY, 0)) AS D10_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.NextWeek + "', D4_QTY, 0)) AS D11_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.NextWeek + "', D5_QTY, 0)) AS D12_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.NextWeek + "', D6_QTY, 0)) AS D13_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.ThisWeek + "', WW_QTY, 0)) AS W1_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.NextWeek + "', WW_QTY, 0)) AS W2_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.W2_Week + "', WW_QTY, 0)) AS W3_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.W3_Week + "', WW_QTY, 0)) AS W4_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.W4_Week + "', WW_QTY, 0)) AS W5_QTY " + "\n");
            strSqlString.Append("                  FROM RWIPPLNWEK " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND GUBUN = '3' " + "\n");
            strSqlString.Append("                   AND MAT_ID LIKE 'SES%' " + "\n");
            strSqlString.Append("                   AND PLAN_WEEK IN ('" + FindWeek_SOP_SE.ThisWeek + "','" + FindWeek_SOP_SE.NextWeek + "','" + FindWeek_SOP_SE.W2_Week + "','" + FindWeek_SOP_SE.W3_Week + "','" + FindWeek_SOP_SE.W4_Week + "')" + "\n");
            strSqlString.Append("                 GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("         GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("       ) PLN " + "\n");

            if (ckb06.Checked == true)
            {
                /*
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT MAT_ID " + "\n");
                strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + Today + "', NVL(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1, 0), 0)) AS SHP_TODAY " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Today + "' THEN NVL(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1, 0) END) AS SHP_WEEK " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1, 0) END) AS SHP_WEEK_TTL " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN WORK_DATE BETWEEN '" + sMonth + "01' AND '" + Today + "' THEN NVL(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1, 0) END) AS SHP_MONTH " + "\n");
                strSqlString.Append("          FROM CSUMFACMOV" + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");
                strSqlString.Append("           AND CM_KEY_1  = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("           AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("           AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("           AND FACTORY <> 'RETURN' " + "\n");
                strSqlString.Append("           AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND WORK_DATE BETWEEN '" + sStartDate + "' AND '" + Today + "' " + "\n");
                strSqlString.Append("         GROUP BY MAT_ID " + "\n");
                strSqlString.Append("       ) SHP " + "\n");
                */
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT MAT_ID " + "\n");
                strSqlString.Append("             , SUM(SHP_TODAY) AS SHP_TODAY " + "\n");
                strSqlString.Append("             , SUM(SHP_WEEK) AS SHP_WEEK" + "\n");
                strSqlString.Append("             , SUM(SHP_WEEK_TTL) AS SHP_WEEK_TTL" + "\n");
                strSqlString.Append("             , SUM(SHP_MONTH) AS SHP_MONTH" + "\n");
                strSqlString.Append("          FROM ( " + "\n");
                strSqlString.Append("                SELECT MAT_ID " + "\n");
                strSqlString.Append("                     , 0 AS SHP_TODAY " + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN SHP_QTY_1 > 0 AND MAT_ID LIKE 'SES%' AND WORK_DATE BETWEEN '" + FindWeek_SOP_SE.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(SHP_QTY_1, 0)  " + "\n");
                strSqlString.Append("                                WHEN SHP_QTY_1 > 0 AND MAT_ID NOT LIKE 'SES%' AND WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(SHP_QTY_1, 0)  " + "\n");
                strSqlString.Append("                                ELSE 0 END) AS SHP_WEEK  " + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN SHP_QTY_1 > 0 AND MAT_ID LIKE 'SES%' AND WORK_DATE BETWEEN '" + FindWeek_SOP_SE.StartDay_ThisWeek + "' AND '" + Yesterdaybf + "' THEN NVL(SHP_QTY_1, 0)  " + "\n");
                strSqlString.Append("                                WHEN SHP_QTY_1 > 0 AND MAT_ID NOT LIKE 'SES%' AND WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Yesterdaybf + "' THEN NVL(SHP_QTY_1, 0) " + "\n");
                strSqlString.Append("                                ELSE 0 END) AS SHP_WEEK_TTL  " + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + sMonth + "01' AND '" + Yesterday + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_MONTH " + "\n");
                strSqlString.Append("                  FROM VSUMWIPOUT" + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
                strSqlString.Append("                   AND FACTORY  = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                   AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                   AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                   AND MAT_ID NOT LIKE 'HX%' " + "\n");
                //strSqlString.Append("                   AND WORK_DATE BETWEEN '" + sStartDate + "' AND '" + Today + "' " + "\n");
                strSqlString.Append("                   AND WORK_DATE BETWEEN LEAST('" + sMonth + "01', '" + FindWeek_SOP_A.StartDay_ThisWeek + "', '" + FindWeek_SOP_SE.StartDay_ThisWeek + "') AND '" + Today + "' " + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
                strSqlString.Append("                 UNION ALL " + "\n");
                strSqlString.Append("                SELECT MAT_ID " + "\n");
                strSqlString.Append("                     , SUM(QTY_1) AS SHP_TODAY " + "\n");
                strSqlString.Append("                     , SUM(QTY_1) AS SHP_WEEK " + "\n");
                strSqlString.Append("                     , 0 AS SHP_WEEK_TTL " + "\n");
                strSqlString.Append("                     , SUM(QTY_1) AS SHP_MONTH " + "\n");
                strSqlString.Append("                  FROM RWIPLOTHIS " + "\n");
                strSqlString.Append("                 WHERE OLD_FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                   AND TRAN_TIME BETWEEN '" + Yesterday + "220000' AND '" + Today + "060000' " + "\n");
                strSqlString.Append("                   AND TRAN_CODE = 'SHIP' " + "\n");
                strSqlString.Append("                   AND HIST_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("                   AND MAT_ID NOT LIKE 'HX%' " + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
                strSqlString.Append("                 UNION ALL " + "\n");
                strSqlString.Append("                SELECT MAT_ID " + "\n");
                strSqlString.Append("                     , 0 AS SHP_TODAY " + "\n");
                strSqlString.Append("                     , 0 AS SHP_WEEK " + "\n");
                strSqlString.Append("                     , SUM(QTY_1) AS SHP_WEEK_TTL " + "\n");
                strSqlString.Append("                     , 0 AS SHP_MONTH " + "\n");
                strSqlString.Append("                  FROM RWIPLOTHIS " + "\n");
                strSqlString.Append("                 WHERE OLD_FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                   AND TRAN_TIME BETWEEN '" + Yesterdaybf + "220000' AND '" + Yesterday + "060000' " + "\n");
                strSqlString.Append("                   AND TRAN_CODE = 'SHIP' " + "\n");
                strSqlString.Append("                   AND HIST_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("                   AND MAT_ID NOT LIKE 'HX%' " + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
                strSqlString.Append("                 UNION ALL " + "\n");
                strSqlString.Append("                SELECT MAT_ID " + "\n");
                //strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + Yesterday + "', NVL(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1, 0), 0)) AS SHP_TODAY " + "\n");
                strSqlString.Append("                     , 0 AS SHP_TODAY " + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN SHP_QTY_1 > 0 AND WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(SHP_QTY_1, 0) ELSE 0 END) AS SHP_WEEK " + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN SHP_QTY_1 > 0 AND WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(SHP_QTY_1, 0) ELSE 0 END) AS SHP_WEEK_TTL " + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + sMonth + "01' AND '" + Yesterday + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_MONTH " + "\n");
                strSqlString.Append("                  FROM VSUMWIPOUT_06" + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
                strSqlString.Append("                   AND FACTORY  = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                   AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                   AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                   AND MAT_ID LIKE 'HX%' " + "\n");
                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + sStartDate + "' AND '" + Yesterday + "' " + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID " + "\n");                
                strSqlString.Append("               )  " + "\n");
                strSqlString.Append("         GROUP BY MAT_ID " + "\n");
                strSqlString.Append("       ) SHP " + "\n");
            }
            else
            {
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT MAT_ID " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN SHP_QTY_1 > 0 AND WORK_DATE = '" + Today + "' THEN NVL(SHP_QTY_1, 0) ELSE 0 END) AS SHP_TODAY " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN SHP_QTY_1 > 0 AND MAT_ID LIKE 'SES%' AND WORK_DATE BETWEEN '" + FindWeek_SOP_SE.StartDay_ThisWeek + "' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0)  " + "\n");
                strSqlString.Append("                        WHEN SHP_QTY_1 > 0 AND MAT_ID NOT LIKE 'SES%' AND WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0) " + "\n");
                strSqlString.Append("                        ELSE 0 END) AS SHP_WEEK  " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN SHP_QTY_1 > 0 AND MAT_ID LIKE 'SES%' AND WORK_DATE BETWEEN '" + FindWeek_SOP_SE.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(SHP_QTY_1, 0) " + "\n");
                strSqlString.Append("                        WHEN SHP_QTY_1 > 0 AND MAT_ID NOT LIKE 'SES%' AND WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(SHP_QTY_1, 0) " + "\n");
                strSqlString.Append("                        ELSE 0 END) AS SHP_WEEK_TTL  " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN WORK_DATE BETWEEN '" + sMonth + "01' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_MONTH  " + "\n");
                strSqlString.Append("          FROM VSUMWIPOUT" + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");
                strSqlString.Append("           AND FACTORY  = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("           AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("           AND CM_KEY_3 LIKE 'P%' " + "\n");                
                strSqlString.Append("           AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND MAT_ID NOT LIKE 'HX%' " + "\n");
                //strSqlString.Append("           AND WORK_DATE BETWEEN '" + sStartDate + "' AND '" + Today + "' " + "\n");
                strSqlString.Append("           AND WORK_DATE BETWEEN LEAST('" + sMonth + "01', '" + FindWeek_SOP_A.StartDay_ThisWeek + "', '" + FindWeek_SOP_SE.StartDay_ThisWeek + "') AND '" + Today + "' " + "\n");
                strSqlString.Append("         GROUP BY MAT_ID " + "\n");
                strSqlString.Append("         UNION ALL " + "\n");
                strSqlString.Append("        SELECT MAT_ID " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN SHP_QTY_1 > 0 AND WORK_DATE = '" + Today + "' THEN NVL(SHP_QTY_1, 0) ELSE 0 END) AS SHP_TODAY " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN SHP_QTY_1 > 0 AND WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0) ELSE 0 END) AS SHP_WEEK " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN SHP_QTY_1 > 0 AND WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(SHP_QTY_1, 0) ELSE 0 END) AS SHP_WEEK_TTL " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN WORK_DATE BETWEEN '" + sMonth + "01' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_MONTH " + "\n");
                strSqlString.Append("          FROM VSUMWIPOUT_06" + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");
                strSqlString.Append("           AND FACTORY  = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("           AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("           AND CM_KEY_3 LIKE 'P%' " + "\n");                
                strSqlString.Append("           AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND MAT_ID LIKE 'HX%' " + "\n");
                strSqlString.Append("           AND WORK_DATE BETWEEN '" + sStartDate + "' AND '" + Today + "' " + "\n");
                strSqlString.Append("         GROUP BY MAT_ID " + "\n");
                strSqlString.Append("       ) SHP " + "\n");
            }


            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT_ID" + "\n");
            strSqlString.Append("             , SUM(QTY) TOTAL" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK2A', QTY, 0)) AS HMK2A" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'B/G', QTY, 0)) AS BG" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SAW', QTY, 0)) AS SAW" + "\n");            
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'S/P', QTY, 0)) AS SP" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A0250','A0310','A0333','A0340','A0401','A0501', 'A0400', 'A0500', 'A0530', 'A0531', 'A0370', 'A0380', 'A0395') THEN QTY " + "\n");
            strSqlString.Append("                        WHEN OPER_GRP_1 = 'SMT' THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) AS DA1 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A0402','A0502', 'A0532') THEN QTY ELSE 0 END) AS DA2 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A0403','A0503', 'A0533') THEN QTY ELSE 0 END) AS DA3 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A0404','A0504', 'A0534') THEN QTY ELSE 0 END) AS DA4 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A0405','A0505', 'A0535') THEN QTY ELSE 0 END) AS DA5 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A0406','A0506', 'A0536') THEN QTY ELSE 0 END) AS DA6 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A0407','A0507', 'A0537') THEN QTY ELSE 0 END) AS DA7 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A0408','A0508', 'A0538') THEN QTY ELSE 0 END) AS DA8 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A0409','A0509', 'A0539') THEN QTY ELSE 0 END) AS DA9 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A0551', 'A0601', 'A0550', 'A0600') THEN QTY" + "\n");
            strSqlString.Append("                        WHEN OPER = 'A0801' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN QTY " + "\n");
            strSqlString.Append("                        WHEN OPER = 'A0801' AND MAT_GRP_1 <> 'SE' THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) AS WB1 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A0552', 'A0602') THEN QTY" + "\n");
            strSqlString.Append("                        WHEN OPER = 'A0802' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN QTY " + "\n");
            strSqlString.Append("                        WHEN OPER = 'A0802' AND MAT_GRP_1 <> 'SE' THEN DECODE(SUBSTR(MAT_GRP_4,-1), '2', 0, QTY) " + "\n");
            strSqlString.Append("                        ELSE 0 END) AS WB2 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A0553', 'A0603') THEN QTY" + "\n");
            strSqlString.Append("                        WHEN OPER = 'A0803' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN QTY  " + "\n");
            strSqlString.Append("                        WHEN OPER = 'A0803' AND MAT_GRP_1 <> 'SE' THEN DECODE(SUBSTR(MAT_GRP_4,-1), '2', 0, '3', 0, QTY) " + "\n");
            strSqlString.Append("                        ELSE 0 END) AS WB3 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A0554', 'A0604') THEN QTY" + "\n");
            strSqlString.Append("                        WHEN OPER = 'A0804' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN QTY " + "\n");
            strSqlString.Append("                        WHEN OPER = 'A0804' AND MAT_GRP_1 <> 'SE' THEN DECODE(SUBSTR(MAT_GRP_4,-1), '2', 0, '3', 0, '4', 0, QTY) " + "\n");
            strSqlString.Append("                        ELSE 0 END) AS WB4 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A0555', 'A0605') THEN QTY" + "\n");
            strSqlString.Append("                        WHEN OPER = 'A0805' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN QTY " + "\n");
            strSqlString.Append("                        WHEN OPER = 'A0805' AND MAT_GRP_1 <> 'SE' THEN DECODE(SUBSTR(MAT_GRP_4,-1), '2', 0, '3', 0, '4', 0, '5', 0, QTY) " + "\n");
            strSqlString.Append("                        ELSE 0 END) AS WB5 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A0556', 'A0606') THEN QTY" + "\n");
            strSqlString.Append("                        WHEN OPER = 'A0806' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN QTY " + "\n");
            strSqlString.Append("                        WHEN OPER = 'A0806' AND MAT_GRP_1 <> 'SE' THEN DECODE(SUBSTR(MAT_GRP_4,-1), '2', 0, '3', 0, '4', 0, '5', 0, '6', 0, QTY) " + "\n");
            strSqlString.Append("                        ELSE 0 END) AS WB6 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A0557', 'A0607') THEN QTY" + "\n");
            strSqlString.Append("                        WHEN OPER = 'A0807' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN QTY " + "\n");
            strSqlString.Append("                        WHEN OPER = 'A0807' AND MAT_GRP_1 <> 'SE' THEN DECODE(SUBSTR(MAT_GRP_4,-1), '2', 0, '3', 0, '4', 0, '5', 0, '6', 0, '7', 0, QTY) " + "\n");
            strSqlString.Append("                        ELSE 0 END) AS WB7 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A0558', 'A0608') THEN QTY" + "\n");
            strSqlString.Append("                        WHEN OPER = 'A0808' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN QTY " + "\n");
            strSqlString.Append("                        WHEN OPER = 'A0808' AND MAT_GRP_1 <> 'SE' THEN DECODE(SUBSTR(MAT_GRP_4,-1), '2', 0, '3', 0, '4', 0, '5', 0, '6', 0, '7', 0, '8', 0, QTY) " + "\n");
            strSqlString.Append("                        ELSE 0 END) AS WB8 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A0559', 'A0609') THEN QTY" + "\n");
            strSqlString.Append("                        WHEN OPER = 'A0809' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN QTY " + "\n");
            strSqlString.Append("                        WHEN OPER = 'A0809' AND MAT_GRP_1 <> 'SE' THEN DECODE(SUBSTR(MAT_GRP_4,-1), '2', 0, '3', 0, '4', 0, '5', 0, '6', 0, '7', 0, '8', 0, '9', 0, QTY) " + "\n");
            strSqlString.Append("                        ELSE 0 END) AS WB9 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER_GRP_1 = 'GATE' AND MAT_GRP_5 = '-' THEN QTY " + "\n");
            strSqlString.Append("                        WHEN OPER_GRP_1 = 'GATE' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 = 'Merge' THEN QTY " + "\n");
            strSqlString.Append("                        WHEN OPER_GRP_1 = 'GATE' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) = SUBSTR(OPER, -1) THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) AS GATE " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A0950','A0900', 'A0910', 'A0920', 'A0930') THEN QTY ELSE 0 END) AS PLASMA " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A1000','A0980') THEN QTY ELSE 0 END) AS MOLD " + "\n");            
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'CURE', QTY, 0)) AS CURE" + "\n");
            //strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'M/K', QTY, 0)) AS MK" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A1150', QTY, 0)) AS MK" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A1500', QTY, 0)) AS MK2" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'TRIM', QTY, 0)) AS TRIM" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'TIN', QTY, 0)) AS TIN" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A1240', QTY, 0)) AS FLIPPER" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A1260', QTY, 0)) AS OSP" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A1300', QTY, 0)) AS SBA" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A1370', QTY, 0)) AS AOI" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A1380', QTY, 0)) AS PP" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A1900', QTY, 0)) AS TF" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER!='A1900' AND OPER_GRP_1 = 'SIG' THEN QTY ELSE 0 END) AS SIG" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A1790', QTY, 0)) AS DC1" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A1795', QTY, 0)) AS DC2" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER_GRP_1 = 'AVI' AND OPER NOT IN ('A1790', 'A1795') THEN QTY ELSE 0 END) AS AVI" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER = 'A2030' THEN QTY ELSE 0 END) AS BAKE" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A2050') THEN QTY ELSE 0 END) AS PVI" + "\n");            
            strSqlString.Append("             , SUM(CASE WHEN OPER IN ('A2100','A2300') THEN QTY ELSE 0 END) AS QC_GATE " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK3A', QTY, 0)) AS HMK3A" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK2A', 0, QTY)) AS W_ISSUE" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER_GRP_1 IN ('HMK2A', 'B/G') THEN 0 ELSE QTY END) AS W_BG" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'SMT', 'S/P', 'D/A') THEN 0 " + "\n");
            strSqlString.Append("                        WHEN OPER_GRP_1 = 'W/B' AND MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> 'Merge' THEN 0 " + "\n");
            strSqlString.Append("                        ELSE QTY END) AS W_DA " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'SMT', 'S/P', 'D/A', 'W/B') THEN 0 ELSE QTY END) AS W_WB" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'SMT', 'S/P', 'D/A', 'W/B', 'GATE', 'MOLD') THEN 0 ELSE QTY END) AS W_MOLD" + "\n");
            strSqlString.Append("          FROM (" + "\n");  
            strSqlString.Append("                SELECT A.MAT_ID, A.OPER, B.OPER_GRP_1, C.MAT_GRP_1, C.MAT_GRP_4, C.MAT_GRP_5, QTY_1" + "\n");
            strSqlString.Append("                     , COMP_CNT, HX_COMP_MIN, HX_COMP_MAX " + "\n");
            strSqlString.Append("                     , CASE WHEN MAT_GRP_1 = 'HX' AND HX_COMP_MIN IS NOT NULL" + "\n");
            strSqlString.Append("                                 THEN (CASE WHEN HX_COMP_MIN <> HX_COMP_MAX AND A.OPER > HX_COMP_MIN AND A.OPER <= HX_COMP_MAX THEN QTY_1 / NVL(COMP_CNT / 2,1)" + "\n");
            strSqlString.Append("                                            WHEN A.OPER <= HX_COMP_MAX THEN QTY_1 / NVL(COMP_CNT,1)" + "\n");
            strSqlString.Append("                                            ELSE QTY_1 END)" + "\n");
            strSqlString.Append("                            WHEN A.OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1) " + "\n");
            strSqlString.Append("                            ELSE QTY_1 " + "\n");
            strSqlString.Append("                       END QTY " + "\n");
            
            // 금일조회 기준 (06시 제외)
            if (DateTime.Now.ToString("yyyyMMdd") == Today && ckb06.Checked == false)
            {
                strSqlString.Append("                  FROM RWIPLOTSTS A " + "\n");
                strSqlString.Append("                     , MWIPOPRDEF B" + "\n");
                strSqlString.Append("                     , VWIPMATDEF C" + "\n");             
                strSqlString.Append("                 WHERE 1=1 " + "\n");
            }
            else
            {
                strSqlString.Append("                  FROM RWIPLOTSTS_BOH A " + "\n");
                strSqlString.Append("                     , MWIPOPRDEF B" + "\n");
                strSqlString.Append("                     , VWIPMATDEF C" + "\n");              

                //06시 기준
                if (ckb06.Checked == true)
                {
                    strSqlString.Append("                 WHERE CUTOFF_DT = '" + Today + "06' " + "\n");
                }
                else
                {
                    strSqlString.Append("                 WHERE ((CUTOFF_DT = '" + Today + "22' AND C.MAT_ID NOT LIKE 'HX%')" + "\n");
                    strSqlString.Append("                       OR (CUTOFF_DT = '" + TodayAdd + "06' AND C.MAT_ID LIKE 'HX%')) " + "\n");
                }
            }
            
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                   AND A.FACTORY = C.FACTORY " + "\n");
            strSqlString.Append("                   AND A.OPER = B.OPER" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = C.MAT_ID" + "\n");
            strSqlString.Append("                   AND A.FACTORY  = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                   AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND A.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                   AND A.LOT_CMF_5 LIKE 'P%' " + "\n");
            strSqlString.Append("                   AND C.DELETE_FLAG = ' ' " + "\n");    

            if (cboHolddiv.Text == "Hold")
                strSqlString.AppendFormat("                   AND A.HOLD_FLAG = 'Y' " + "\n");
            else if (cboHolddiv.Text == "Non Hold")
                strSqlString.AppendFormat("                   AND A.HOLD_FLAG = ' ' " + "\n");

            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) WIP  " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT.MAT_ID" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN GUBUN ='DA' AND MAT.MAT_GRP_5 IN ('Merge', '-') THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_da + " * RES.RAS_CNT, 0)) ELSE 0 END) AS DA_CAPA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN GUBUN ='WB' AND MAT.MAT_GRP_5 IN ('Merge', '-') THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_wb + " * RES.RAS_CNT, 0)) ELSE 0 END) AS WB_CAPA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN RES.OPER IN ('A0333','A0400', 'A0401') THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_da + " * RES.RAS_CNT, 0)) ELSE 0 END) AS DA1_CAPA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN RES.OPER = 'A0402' THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_da + " * RES.RAS_CNT, 0)) ELSE 0 END) AS DA2_CAPA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN RES.OPER = 'A0403' THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_da + " * RES.RAS_CNT, 0)) ELSE 0 END) AS DA3_CAPA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN RES.OPER = 'A0404' THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_da + " * RES.RAS_CNT, 0)) ELSE 0 END) AS DA4_CAPA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN RES.OPER = 'A0405' THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_da + " * RES.RAS_CNT, 0)) ELSE 0 END) AS DA5_CAPA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN RES.OPER = 'A0406' THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_da + " * RES.RAS_CNT, 0)) ELSE 0 END) AS DA6_CAPA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN RES.OPER = 'A0407' THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_da + " * RES.RAS_CNT, 0)) ELSE 0 END) AS DA7_CAPA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN RES.OPER = 'A0408' THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_da + " * RES.RAS_CNT, 0)) ELSE 0 END) AS DA8_CAPA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN RES.OPER = 'A0409' THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_da + " * RES.RAS_CNT, 0)) ELSE 0 END) AS DA9_CAPA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN RES.OPER IN ('A0600', 'A0601') THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_wb + " * RES.RAS_CNT, 0)) ELSE 0 END) AS WB1_CAPA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN RES.OPER = 'A0602' THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_wb + " * RES.RAS_CNT, 0)) ELSE 0 END) AS WB2_CAPA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN RES.OPER = 'A0603' THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_wb + " * RES.RAS_CNT, 0)) ELSE 0 END) AS WB3_CAPA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN RES.OPER = 'A0604' THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_wb + " * RES.RAS_CNT, 0)) ELSE 0 END) AS WB4_CAPA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN RES.OPER = 'A0605' THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_wb + " * RES.RAS_CNT, 0)) ELSE 0 END) AS WB5_CAPA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN RES.OPER = 'A0606' THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_wb + " * RES.RAS_CNT, 0)) ELSE 0 END) AS WB6_CAPA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN RES.OPER = 'A0607' THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_wb + " * RES.RAS_CNT, 0)) ELSE 0 END) AS WB7_CAPA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN RES.OPER = 'A0608' THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_wb + " * RES.RAS_CNT, 0)) ELSE 0 END) AS WB8_CAPA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN RES.OPER = 'A0609' THEN TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * " + GlobalVariable.gdPer_wb + " * RES.RAS_CNT, 0)) ELSE 0 END) AS WB9_CAPA" + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT FACTORY, RES_STS_2, RES_STS_8 AS OPER " + "\n");
            strSqlString.Append("                     , CASE WHEN RES_STS_8 LIKE 'A06%' THEN 'WB' ELSE 'DA' END AS GUBUN " + "\n");
            strSqlString.Append("                     , RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, COUNT(RES_ID) AS RAS_CNT " + "\n");
            strSqlString.Append("                  FROM MRASRESDEF " + "\n");
            strSqlString.Append("                 WHERE FACTORY  = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                   AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("                   AND RES_CMF_7 = 'Y' " + "\n");
            strSqlString.Append("                   AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND (RES_STS_8 LIKE 'A04%' OR RES_STS_8 LIKE 'A06%' OR RES_STS_8 = 'A0333')" + "\n");
            //strSqlString.Append("                   AND RES_STS_1 NOT IN ('C200', 'B199') " + "\n");
            strSqlString.Append("                   AND (RES_STS_1 NOT IN ('C200', 'B199') OR RES_UP_DOWN_FLAG = 'U') " + "\n");
            strSqlString.Append("                 GROUP BY FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7 " + "\n");
            strSqlString.Append("               ) RES " + "\n");
            strSqlString.Append("             , CRASUPHDEF UPH " + "\n");
            strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
            strSqlString.Append("         WHERE 1 = 1 " + "\n");
            strSqlString.Append("           AND RES.FACTORY = UPH.FACTORY(+) " + "\n");
            strSqlString.Append("           AND RES.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("           AND RES.OPER = UPH.OPER(+) " + "\n");
            strSqlString.Append("           AND RES.RES_MODEL = UPH.RES_MODEL(+) " + "\n");
            strSqlString.Append("           AND RES.UPEH_GRP = UPH.UPEH_GRP(+) " + "\n");
            strSqlString.Append("           AND RES.RES_STS_2 = UPH.MAT_ID(+) " + "\n");
            strSqlString.Append("           AND RES.RES_STS_2 = MAT.MAT_ID " + "\n");
            strSqlString.Append("           AND MAT.DELETE_FLAG = ' ' " + "\n");            
            strSqlString.Append("         GROUP BY MAT.MAT_ID " + "\n");
            strSqlString.Append("       ) CAP  " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            // 2014-06-20-임종우 : 월간 실행 계획 변경 - 기존 ORI_PLAN 에서 생산관리 업로드 RESV_FIELD1 계획으로 변경 (임태성 요청)
            //strSqlString.Append("             , PLAN_QTY_ASSY AS ORI_PLAN " + "\n");
            strSqlString.Append("             , DECODE(RESV_FIELD1, ' ', 0, RESV_FIELD1) AS ORI_PLAN " + "\n");
            strSqlString.Append("             , DECODE(RESV_FIELD3, ' ', 0, RESV_FIELD3) AS REV_PLAN " + "\n");
            strSqlString.Append("          FROM CWIPPLNMON " + "\n");
            strSqlString.Append("         WHERE FACTORY  = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("           AND PLAN_MONTH = '" + sMonth + "' " + "\n");
            strSqlString.Append("           AND PLAN_QTY_ASSY + DECODE(RESV_FIELD3, ' ', 0, RESV_FIELD3) > 0 " + "\n");
            strSqlString.Append("       ) MON  " + "\n");
            strSqlString.Append(" WHERE 1 = 1  " + "\n");
            strSqlString.Append("   AND MAT.MAT_ID NOT IN (SELECT MAT_ID FROM MWIPMATDEF WHERE FIRST_FLOW = 'A-BANK' AND DELETE_FLAG = ' ') " + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = PLN.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = SHP.MAT_ID(+) " + "\n");            
            strSqlString.Append("   AND MAT.MAT_ID = WIP.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = CAP.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = MON.MAT_ID(+) " + "\n");
            strSqlString.AppendFormat(" GROUP BY {0} " + "\n", QueryCond1);
            strSqlString.Append("HAVING SUM(" + sWeek + " + NVL(SHP_WEEK, 0) + NVL(TOTAL, 0) + NVL(DA_CAPA, 0) + NVL(WB_CAPA, 0) + NVL(ORI_PLAN, 0) + NVL(REV_PLAN, 0) + NVL(SHP_MONTH, 0)) > 0" + "\n");

            if (ckbPlan.Checked == true)
            {
                strSqlString.Append("   AND SUM(" + sWeek + ") > 0" + "\n");
            }

            strSqlString.AppendFormat(" ORDER BY {0}" + "\n", QueryCond1);

            strSqlString.AppendFormat(" )" + "\n", QueryCond1);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        //2020-09-07-이희석 쉬운유지보수를 위해 추가
        //같은컬럼명이 나와도 그전에 Span처리 되었다면 그 값은 사라진다.
        //시작해야되는 row 1의 첫 컬럼과 마지막컬럼 쓰기
        #region 자동 spanHelper
        private int[] spanHelper(String firstCol, String lastCol)
        {
            int[] idxBox = new int[2];
                for (int i = 0; i < spdData.ActiveSheet.ColumnCount; i++)
                {
                        //첫번째 컬럼 인덱스
                        if (firstCol == spdData.ActiveSheet.GetColumnLabel(1, i))
                        {
                            idxBox[0] = i;
                        }
                        //마지막 컬럼 인덱스
                        else if (lastCol == spdData.ActiveSheet.GetColumnLabel(1, i))
                        {
                            idxBox[1] = i - idxBox[0] + 1;
                        }
                }
            
            return idxBox;
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
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 14, null, null, btnSort);

                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 14, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);
                //spdData.DataSource = dt;
                
                // 당일 계획에 따른 재공 색깔 표시 (목표>= 재공 : red, 목표<재공 : blue)
                for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    int sum = 0;
                    int value = 0;
                    int iEndColumn = 65;
                    int iTarget = 0;

                    if (spdData.ActiveSheet.Cells[i, 25].BackColor.IsEmpty) // subtotal 부분 제외시키기 위함.
                    {
                        iTarget = Convert.ToInt32(spdData.ActiveSheet.Cells[i, 25].Value);

                        if (iTarget > 0) // 목표값 0인것 제외
                        {
                            for (int y = 27; y <= iEndColumn; y++) // 공정 컬럼번호 (순서는 반대로)
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

                spdData.RPT_SetPerSubTotalAndGrandTotal(1, 96, 93, 97);                

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

            if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                cdvWeek.Enabled = true;
            else cdvWeek.Enabled = false;
        }
        #endregion
    }
}
