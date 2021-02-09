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
    public partial class PRD011015 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD011015<br/>
        /// 클래스요약: 계획잔량 모니터링<br/>
        /// 작  성  자: 에이스텍 김태호<br/>
        /// 최초작성일: 2013-11-01<br/>
        /// 상세  설명: 계획잔량 모니터링<br/>
        /// 2013-11-25-임종우 : TYPE2 Group 활성화시 재공 누락되는 문제 수정
        /// 2013-12-03-임종우 : 재공 부분 수정 -> BG - A0033 포함, SAW - A0033 제외, DA 1 - A0250 포함 (임태성 요청)
        /// 2013-12-05-임종우 : 재공 부분 수정 -> SP - A0340 제외, DA 1 - A0333, A0340 포함 / 실적 부분 수정 -> DA 1 - A0333 포함 / A0333 공정도 색상 표시에 DA1 속하도록 수정 (임태성 요청)
        /// 2014-01-06-임종우 : 설비대수, CAPA 현황, D2 잔량, 차주 잔량 추가 (임태성 요청)
        ///                     재공 부분에 음영처리 부분 글씨는 보이도록 수정 (김권수 요청)
        /// 2014-01-08-임종우 : 상세 팝업창 추가
        /// 2014-01-15-임종우 : 공정별 실적부분 HX 는 06시 기준으로 변경 & GATE 마지막 차수 로직 추가 & 미입고 잔량 추가 (김권수 요청)   
        /// 2014-01-22-임종우 : COB 제외 체크박스 추가, 실적(MIX, 22시, 06시) 검색 기능 추가 (김권수 요청)
        /// 2014-02-03-임종우 : 과거 조회 시 설비 대수 과거 데이터 나오도록 수정
        /// 2014-02-19-임종우 : A0370, A0380 공정의 재공은 DA1 재공에 포함 (김권수 요청)
        /// 2014-05-07-임종우 : 금일 시간별 조회 기능 추가 (임태성 요청)
        /// 2014-06-09-임종우 : 재공 팝업창에 Part 정보 추가 (김성업 요청)
        ///                     과거 일자 검색시 재공도 과거 기준으로 되도록 수정 (임태성 요청)
        /// 2014-06-26-임종우 : LOT TYPE 검색 기능 추가 (임태성K 요청)
        /// 2014-07-07-임종우 : 계획 유무에 의한 검색 기능 추가 (임태성K 요청)
        ///                   : 스냅샵 데이터 당일뿐만 아니라 하루 전일 데이터도 검색 가능하도록 변경 (임태성K 요청)
        /// 2014-09-02-임종우 : HX 제품 재공 -> Process 상에 A0015가 있는 제품의 경우 [(A0000~A0015) / Auto loss + A0016 이후 재공의 합] (임태성K 요청)
        ///                                     Process 상에 A0395가 있는 제품의 경우 [(A0000~A0395) / Auto loss + A0396 이후 재공의 합]
        ///                                     Process 상에 A0015, A0395 둘다 있는 제품 [(A0000~A0015) / Auto loss] + [(A0016~A035) / (Auto loss/2)] + A0396 이후 재공의 합
        ///                     A0500 ~ A0509 공정 재공 기존 WB -> DA 공정에 포함 (임태성K 요청)
        /// 2014-09-03-임종우 : A0530 ~ A0539 공정 재공 기존 WB -> DA 공정에 포함 (임태성K 요청)
        /// 2014-09-11-임종우 : A1770, A1790, A1795 공정 AVI 공정 재공에 포함 (임태성K 요청)
        /// 2014-09-16-임종우 : A0310 공정 DA1 재공에 포함 (임태성K 요청)
        /// 2014-09-16-임종우 : DA Cure 공정 옵션에 의해 DA or WB 공정에 포함 되도록 변경 (임태성K 요청)
        /// 2014-10-13-임종우 : 2일 이상 과거 데이터 조회 시 재공은 06시 선택시 06시 조회 그 이외는 22시 조회 되도록 변경 (임태성K 요청)
        /// 2014-10-28-임종우 : C200 코드의 설비 제외 (임태성K 요청)
        /// 2014-10-29-임종우 : 실적에서 반품 입고 수량 차감시킴 - RSUMFACMOV -> VSUMWIPOUT 으로 변경 (임태성K 요청)
        /// 2014-11-06-임종우 : B199 코드의 설비 제외 (임태성K 요청)
        /// 2014-11-14-임종우 : CAPA 효율 변경 - WB : 75% -> 67.5%, 그 외 : 70% -> 63% (임태성K 요청)
        /// 2014-11-18-임종우 : SAW 재공 - A0045 공정 추가 (백성호 요청)
        /// 2014-11-19-임종우 : CAPA 효율 변경 - WB : 67.5% -> 75%, 그 외 : 63% -> 70% (임태성K 요청)
        /// 2014-12-05-임종우 : SAW 재공 - A0215 공정 추가 (백성호 요청)
        ///                     A0980 공정 재공 기존 CURE -> MOLD 공정으로 변경 (임태성K 요청)
        /// 2015-02-16-임종우 : 9단 STACK 제품까지 표시 되도록 추가 (임태성K 요청)
        /// 2015-02-25-임종우 : PVI 재공 - A2030 공정 추가 (임태성K 요청)
        /// 2015-03-09-임종우 : C_MOLD 제품 검색 기능 추가 (김성업D 요청)
        /// 2015-03-23-임종우 : 설비 대수 상세 팝업 추가 (김성업D 요청)
        /// 2015-03-24-임종우 : DA1 재공 - A0337 공정 추가 (조형진 요청)
        /// 2015-04-07-임종우 : 설비 팝업창에 설비 STATUS 부분 추가 (임태성K 요청)
        /// 2015-06-22-임종우 : HX EMCP 제품의 경우 재공 로직은 삼성로직과 동일하게 한다 (백성호 요청)
        /// 2015-07-03-임종우 : 설비 대수 집계 시 DISPATCH 기준 정보가 'Y" 인 설비만 집계 (임태성K 요청)   
        /// 2015-08-13-임종우 : 삼성메모리 마감일 표시 (김성업D 요청)
        /// 2015-09-10-임종우 : HX 중간 Gate가 없어짐에따라 기존 DA에 포함되던로직을 모두 Gate 에 포함한다 (백성호 요청)
        /// 2015-09-15-임종우 : C200, B199 설비 제외시 해당코드로 'Down' 된 설비만 제외 (김보람 요청)
        /// 2015-09-24-임종우 : 고객사 명 하드 코딩 되어 있는것을 기준정보로 변경 (임태성K 요청)
        /// 2015-11-09-임종우 : PVI 재공 - A2020 공정 추가 (김권수D 요청)
        /// 2016-02-18-임종우 : MOLD 공정 - A0910, A0920, A0930 공정 추가 (임태성K 요청)
        /// 2016-02-25-임종우 : MOLD 실적&설비 - A0910 공정 추가 (임태성K 요청)
        /// 2016-03-11-임종우 : 일자 표시 부분에 시간, 분 까지 표시 되도록 수정 (임태성K 요청)
        /// 2016-03-14-임종우 : 일자 표시 부분 다시 이전으로 원복함 (임태성K 요청)
        /// 2016-05-24-임종우 : C-MOLD 로직 Part 단위로 확대 함. (임태성K 요청)
        /// 2016-05-30-임종우 : SBA 재공 - A1380 공정 추가 (임태성K 요청)
        /// 2016-06-07-임종우 : MK 재공 - A1020 공정 추가 (임태성K 요청)
        /// 2016-07-12-임종우 : CAPA 효율 GlobalVariable 로 선언하여 변경함.
        /// 2016-11-04-임종우 : SIG 재공 - A1810, A1815 공정 추가 (임태성K 요청)
        /// 2016-12-16-임종우 : 주 계획 - Memory(OTD:토~금), SLSI(SE:월~일) 기준 분리 (최연희D 요청)
        /// 2017-07-31-임종우 : QC GATE 재공 - A2300 공정 추가 (임태성C 요청)
        /// 2017-09-19-임종우 : SIG 신규공정 추가 - A1820, A1825, A1830 (임태성C 요청)
        /// 2017-10-10-임종우 : SIG 신규공정 추가 - A1460 (임태성C 요청)
        /// 2017-12-19-임종우 : CUST Device, Sales Code 그룹정보 추가 (임태성C 요청)
        ///                   : Gate 신규공정 추가 - A0700 (황선미D 요청)
        /// 2018-01-31-임종우 : QC GATE 재공 - A2350 공정 추가 (임태성C 요청)
        /// 2018-06-27-임종우 : GATE 재공 - A0713 공정 추가 (황선미대리 요청)
        /// 2018-07-16-임종우 : SBA 재공 - A1370 공정 추가 (임태성차장 요청)
        /// 2018-10-10-임종우 : 재공부분 - 기존 하드코딩부터 제거하고 재공공정그룹2 기준으로 변경
        /// 2019-03-12-임종우 : 당일실적, 주간실적에서 Return 실적 제외 SHP_QTY_1 > 0 (박형순대리 요청)
        /// 2020-07-18-이희석 : Flipchip 관점 추가 (방해원 과장 요청)
        /// 2020-08-07-이희석 : DP 세분화 관점 추가 (방해원 과장 요청)
        /// 2020-09-22-임종우 : V2 로직 분리 - 월 실적 DR 고객사는 전월 26일 ~ 금월 25일 기준으로 변경 (김성업과장 요청)
        /// 2020-09-29-임종우 : DR 25일 까지는 전월 26 ~ 금월 25 / 26일 부터는 금월 26 ~ 차월 25일
        /// 2020-10-12-김미경 : H_PKG_2D_CMOLD "%" 기능 추가
        /// 2020-11-23-임종우 : 월 실적 DR 고객사 1일 ~ 말일 기준으로 다시 변경 (김성업과장 요청)
        /// </summary>
        GlobalVariable.FindWeek FindWeek_SOP_A = new GlobalVariable.FindWeek();
        GlobalVariable.FindWeek FindWeek_SOP_SE = new GlobalVariable.FindWeek();

        public PRD011015()
        {
            InitializeComponent();
            
            SortInit();
            cdvDate.Value = DateTime.Now;
            cboTimeBase.SelectedIndex = 0;
            GridColumnInit();
            
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(cdvFactory.txtValue);
            //cdvFactory.Enabled = false;

            cdvGroup.sDynamicQuery = "SELECT DECODE(SEQ, 1, '설비대수', 2, 'CAPA현황', 3, '당일 실적', 4, 'D0 잔량', 5, 'D1 잔량', 6, 'D2 잔량', 7, '당주 잔량', 8, '차주 잔량', 9, '월간 잔량') AS Code, ' ' AS Data FROM DUAL, (SELECT LEVEL SEQ FROM DUAL CONNECT BY LEVEL<=9)";

            cdvGroup.Text = "ALL";
            
        }
        #region " Constant Definition "

        #endregion

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
            GetWorkDay();
            spdData.RPT_ColumnInit();
            Dictionary<string, Visibles> WipVisibles = GetWipVisibleList();

            try
            {
                if (cdvFactory.Text.Trim() == "HMKB1")
                {
                    #region Bump
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Bumping Type", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Operation Flow", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Layer classification", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PKG Type", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("RDL Plating", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Final Bump", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Sub. Material", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Size", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Thickness", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Flat Type", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Wafer Orientation", 0, 11, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("PRODUCT", 0, 12, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);


                    spdData.RPT_AddBasicColumn(cdvDate.Value.ToString("MM-dd"), 0, 13, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 60);                    
                    spdData.RPT_AddBasicColumn("plan", 1, 13, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("actual", 1, 14, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("residual quantity", 1, 15, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_MerageHeaderColumnSpan(0, 13, 3);                    

                    spdData.RPT_AddBasicColumn("Classification", 0, 16, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 60);
                    
                    spdData.RPT_AddBasicColumn("WIP", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("HMK3B", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                    spdData.RPT_AddBasicColumn("PACKING", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                    spdData.RPT_AddBasicColumn("OGI", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("AVI", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("SORT", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    
                    spdData.RPT_AddBasicColumn("FINAL_INSP", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("BUMP_REFLOW", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("BUMP_BALL_MOUNT", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                    spdData.RPT_AddBasicColumn("BUMP_ETCH", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Ti Etch", 2, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Cu Etch", 2, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("PR Strip", 2, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_MerageHeaderColumnSpan(1, 25, 3);   //BUMP_ETCH

                    spdData.RPT_AddBasicColumn("BUMP_SNAG_PLAT", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Sn/Ag Plating", 2, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Ni Plating", 2, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_MerageHeaderColumnSpan(1, 28, 2);   //BUMP_SNAG_PLAT

                    spdData.RPT_AddBasicColumn("BUMP_CU_PLAT", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Cu Plating", 2, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                    spdData.RPT_AddBasicColumn("BUMP_PHOTO", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Descum", 2, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Measuring Insp", 2, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Develop", 2, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Exposure", 2, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("PR Coating", 2, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_MerageHeaderColumnSpan(1, 31, 5);   //BUMP_PHOTO

                    spdData.RPT_AddBasicColumn("BUMP_SPUTTER", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Sputter", 2, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                    spdData.RPT_AddBasicColumn("PSV3_PHOTO", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Descum", 2, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);                    
                    spdData.RPT_AddBasicColumn("Develop Insp", 2, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Develop", 2, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Exposure", 2, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("PI Coating", 2, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_MerageHeaderColumnSpan(1, 37, 5);   //PSV3_PHOTO

                    spdData.RPT_AddBasicColumn("RDL3_ETCH", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Descum", 2, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Ti Etch", 2, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Cu Etch", 2, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("PR Strip", 2, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_MerageHeaderColumnSpan(1, 42, 4);   //RDL3_ETCH

                    spdData.RPT_AddBasicColumn("RDL3_PLAT", 1, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Cu Plating", 2, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                    spdData.RPT_AddBasicColumn("RDL3_PHOTO", 1, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Descum", 2, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Measuring Insp", 2, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Develop", 2, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Exposure", 2, 50, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("PR Coating", 2, 51, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_MerageHeaderColumnSpan(1, 46, 5);   //RDL3_PHOTO

                    spdData.RPT_AddBasicColumn("RDL3_SPUTTER", 1, 52, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Sputter", 2, 52, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                    spdData.RPT_AddBasicColumn("PSV2_PHOTO", 1, 53, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Descum", 2, 53, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Cure", 2, 54, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Develop Insp", 2, 55, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Develop", 2, 56, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Exposure", 2, 57, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("PI Coating", 2, 58, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_MerageHeaderColumnSpan(1, 53, 6);   //PSV2_PHOTO

                    spdData.RPT_AddBasicColumn("RDL2_ETCH", 1, 59, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Descum", 2, 59, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Ti Etch", 2, 60, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Cu Etch", 2, 61, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("PR Strip", 2, 62, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_MerageHeaderColumnSpan(1, 59, 4);   //RDL2_ETCH

                    spdData.RPT_AddBasicColumn("RDL2_PLAT", 1, 63, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Cu Plating", 2, 63, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                    spdData.RPT_AddBasicColumn("RDL2_PHOTO", 1, 64, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Descum", 2, 64, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Measuring Insp", 2, 65, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Develop", 2, 66, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Exposure", 2, 67, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("PR Coating", 2, 68, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_MerageHeaderColumnSpan(1, 64, 5);   //RDL2_PHOTO

                    spdData.RPT_AddBasicColumn("RDL2_SPUTTER", 1, 69, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Sputter", 2, 69, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                    spdData.RPT_AddBasicColumn("PSV1_PHOTO", 1, 70, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Descum", 2, 70, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Cure", 2, 71, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Develop Insp", 2, 72, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Develop", 2, 73, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Exposure", 2, 74, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("PI Coating", 2, 75, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_MerageHeaderColumnSpan(1, 70, 6);   //PSV1_PHOTO

                    spdData.RPT_AddBasicColumn("RDL1_ETCH", 1, 76, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Descum", 2, 76, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Ti Etch", 2, 77, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Cu Etch", 2, 78, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("PR Strip", 2, 79, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_MerageHeaderColumnSpan(1, 76, 4);   //RDL1_ETCH

                    spdData.RPT_AddBasicColumn("RDL1_PLAT", 1, 80, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Cu Plating", 2, 80, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                    spdData.RPT_AddBasicColumn("RDL1_PHOTO", 1, 81, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Descum", 2, 84, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Measuring Insp", 2, 82, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Develp", 2, 83, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Exposure", 2, 84, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("PR Coating", 2, 85, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_MerageHeaderColumnSpan(1, 81, 5);   //RDL1_PHOTO

                    spdData.RPT_AddBasicColumn("RDL1_SPUTTER", 1, 86, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Sputter", 2, 86, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                    spdData.RPT_AddBasicColumn("RCF_PHOTO", 1, 87, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Descum", 2, 87, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Cure", 2, 88, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Develop Insp", 2, 89, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Develop", 2, 90, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("Exposure", 2, 91, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("PI Coating", 2, 92, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_MerageHeaderColumnSpan(1, 87, 6);   //RCF_PHOTO

                    spdData.RPT_AddBasicColumn("I-STOCK", 1, 93, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                    spdData.RPT_AddBasicColumn("IQC", 1, 94, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                    spdData.RPT_AddBasicColumn("HMK2B", 1, 95, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                    spdData.RPT_MerageHeaderColumnSpan(0, 17, 79);  //WIP
                                        
                    spdData.RPT_AddBasicColumn("Unreceived remaining volume", 0, 96, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

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
                    spdData.RPT_MerageHeaderRowSpan(0, 12, 3);

                    spdData.RPT_MerageHeaderRowSpan(0, 16, 3);
                    spdData.RPT_MerageHeaderRowSpan(0, 96, 3);
                    
                    spdData.RPT_MerageHeaderRowSpan(1, 13, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 14, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 15, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 17, 2);  //HMK3B
                    spdData.RPT_MerageHeaderRowSpan(1, 18, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 19, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 20, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 21, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 22, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 23, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 24, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 93, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 94, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 95, 2);
                    #endregion
                }
                else
                {
                    #region Assy
                    int autoRow = 0;
                    int[] idxBox= new int[2];
                    spdData.RPT_AddBasicColumn("Customer", 0, autoRow, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("MAJOR", 0, ++autoRow, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PKG", 0, ++autoRow, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("LD_COUNT", 0, ++autoRow, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PKG_CODE", 0, ++autoRow, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("FAMILY", 0, ++autoRow, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PRODUCT", 0, ++autoRow, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 180);
                    spdData.RPT_AddBasicColumn("TYPE_1", 0, ++autoRow, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("TYPE_2", 0, ++autoRow, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("DENSITY", 0, ++autoRow, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("GENERATION", 0, ++autoRow, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("PIN_TYPE", 0, ++autoRow, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("CUST_DEVICE", 0, ++autoRow, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("SALES_CODE", 0, ++autoRow, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn(" ", 0, ++autoRow, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn(" ", 0, ++autoRow, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);//autoRow=15                    

                    spdData.RPT_AddBasicColumn(cdvDate.Value.ToString("MM-dd"), 0, ++autoRow, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 60);

                    //if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue() && cboTimeBase.Text == "현재")
                    //{                        .
                    //    spdData.RPT_AddBasicColumn(DateTime.Now.ToString("yy.MM.dd HH:mm"), 0, 12, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 60);
                    //}
                    //else if ((DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd") || DateTime.Now.AddDays(-1).ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd")) && cboTimeBase.Text != "현재")
                    //{
                    //    spdData.RPT_AddBasicColumn(cdvDate.Value.ToString("yy.MM.dd") + " " + cboTimeBase.Text.Replace("시", "") + ":00", 0, 12, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 60);
                    //}
                    //else
                    //{
                    //    spdData.RPT_AddBasicColumn(cdvDate.Value.ToString("yy.MM.dd") + " 22:00", 0, 12, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 60);                        
                    //}

                    spdData.RPT_AddBasicColumn("plan", 1, autoRow, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("actual", 1, ++autoRow, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("residual quantity", 1, ++autoRow, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 60);
                    idxBox = spanHelper("plan","residual quantity");
                    spdData.RPT_MerageHeaderColumnSpan(0, idxBox[0], idxBox[1]);

                    spdData.RPT_AddBasicColumn("Classification", 0, ++autoRow, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);

                    spdData.RPT_AddBasicColumn("WIP", 0, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("HMKA3", 1, autoRow, WipVisibles["HMKA3"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("QC_GATE", 1, ++autoRow, WipVisibles["QC_GATE"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("PVI", 1, ++autoRow, WipVisibles["PVI"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("AVI", 1, ++autoRow, WipVisibles["AVI"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    //SBA 공정 세분화 이강훈 직장 요청
                    if (cdvType.SelectedIndex == 7)
                    {
                        spdData.RPT_AddBasicColumn("DC TEST", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("MARK2", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("SIG", 1, ++autoRow, WipVisibles["SIG"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("P&P", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("AOI", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("SBA", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("FILPPER", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("TIN", 1, ++autoRow, WipVisibles["TIN"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("TRIM", 1, ++autoRow, WipVisibles["TRIM"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("MARK", 1, ++autoRow, WipVisibles["MARK"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("CURE", 1, ++autoRow, WipVisibles["CURE"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("MOLD", 1, ++autoRow, WipVisibles["MOLD"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    }
                    else
                    {
                        if (cdvType.SelectedIndex == 3)
                        {
                            spdData.RPT_AddBasicColumn("SIG", 1, ++autoRow, WipVisibles["SIG"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                            spdData.RPT_AddBasicColumn("MARK", 1, ++autoRow, WipVisibles["MARK"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                            spdData.RPT_AddBasicColumn("SBA", 1, ++autoRow, WipVisibles["SBA"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                            spdData.RPT_AddBasicColumn("TIN", 1, ++autoRow, WipVisibles["TIN"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                            spdData.RPT_AddBasicColumn("TRIM", 1, ++autoRow, WipVisibles["TRIM"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                            spdData.RPT_AddBasicColumn("CURE", 1, ++autoRow, WipVisibles["CURE"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                            spdData.RPT_AddBasicColumn("MOLD", 1, ++autoRow, WipVisibles["MOLD"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        }else{
                            spdData.RPT_AddBasicColumn("SIG", 1, ++autoRow, WipVisibles["SIG"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                            spdData.RPT_AddBasicColumn("SBA", 1, ++autoRow, WipVisibles["SBA"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                            spdData.RPT_AddBasicColumn("TIN", 1, ++autoRow, WipVisibles["TIN"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                            spdData.RPT_AddBasicColumn("TRIM", 1, ++autoRow, WipVisibles["TRIM"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                            spdData.RPT_AddBasicColumn("MARK", 1, ++autoRow, WipVisibles["MARK"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                            spdData.RPT_AddBasicColumn("CURE", 1, ++autoRow, WipVisibles["CURE"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                            spdData.RPT_AddBasicColumn("MOLD", 1, ++autoRow, WipVisibles["MOLD"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        }
                    }
                    

                    
                    //Flipchip 관점 (추가한 DP관점으로 바꿀수있는 컬럼개수 MAX 15개 고정)
                    if (cdvType.SelectedIndex == 3)
                    {
                        spdData.RPT_AddBasicColumn("HSA CURE", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("H/S/A", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("QC GATE 3", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("BTM SMT", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("Flipper", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("U/F CURE", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("U/F", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PLASMA 1", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PREBAKE 2", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DEFLUX", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("C/A", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("UV", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("DA4", 1, ++autoRow, WipVisibles["DA4"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("WB3", 1, ++autoRow, WipVisibles["WB3"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("DA3", 1, ++autoRow, WipVisibles["DA3"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    }
                    //DP세부 관점 ( 4 : 종합 , 5 : Normal , 6 : WLCSP )
                    else if (cdvType.SelectedIndex == 4 || cdvType.SelectedIndex == 5 || cdvType.SelectedIndex == 6)
                    {
                        spdData.RPT_AddBasicColumn("COB_AVI", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("UV", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("W/E", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("SAW", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("L/G", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("T/M", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("MARKING", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("CURE", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("COATING", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("DETACH", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("B/G", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("STEALTH", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("PRE_B/G", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("L/N", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("DIE_BANK", 1, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("GATE", 1, ++autoRow, WipVisibles["GATE"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("WB9", 1, ++autoRow, WipVisibles["WB9"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("DA9", 1, ++autoRow, WipVisibles["DA9"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("WB8", 1, ++autoRow, WipVisibles["WB8"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("DA8", 1, ++autoRow, WipVisibles["DA8"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("WB7", 1, ++autoRow, WipVisibles["WB7"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("DA7", 1, ++autoRow, WipVisibles["DA7"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("WB6", 1, ++autoRow, WipVisibles["WB6"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("DA6", 1, ++autoRow, WipVisibles["DA6"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("WB5", 1, ++autoRow, WipVisibles["WB5"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("DA5", 1, ++autoRow, WipVisibles["DA5"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("WB4", 1, ++autoRow, WipVisibles["WB4"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("DA4", 1, ++autoRow, WipVisibles["DA4"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("WB3", 1, ++autoRow, WipVisibles["WB3"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        spdData.RPT_AddBasicColumn("DA3", 1, ++autoRow, WipVisibles["DA3"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    }
                    spdData.RPT_AddBasicColumn("WB2", 1, ++autoRow, WipVisibles["WB2"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("DA2", 1, ++autoRow, WipVisibles["DA2"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("WB1", 1, ++autoRow, WipVisibles["WB1"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("DA1", 1, ++autoRow, WipVisibles["DA1"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("SP", 1, ++autoRow, WipVisibles["SP"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("SAW", 1, ++autoRow, WipVisibles["SAW"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("BG", 1, ++autoRow, WipVisibles["BG"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    spdData.RPT_AddBasicColumn("HMKA2", 1, ++autoRow, WipVisibles["HMKA2"], Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                    idxBox = spanHelper("HMKA3", "HMKA2");
                    spdData.RPT_MerageHeaderColumnSpan(0, idxBox[0], idxBox[1]);

                    spdData.RPT_AddBasicColumn("Unreceived remaining volume", 0, ++autoRow, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    for (int i = 0; i <= 15; i++)
                    {
                        spdData.RPT_MerageHeaderRowSpan(0, i, 2);
                    }
                    spdData.RPT_MerageHeaderRowSpan(0, 19, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, autoRow, 2);
                    #endregion
                }
                //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
                spdData.RPT_ColumnConfigFromTable_New(btnSort);
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
            if (cdvFactory.txtValue.Equals("HMKB1"))
            {
                ((udcTableForm)(this.btnSort.BindingForm)).Clear();                
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = 'HMKB1' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1) AS CUSTOMER", "MAT.MAT_GRP_1", "MAT_GRP_1", "DECODE(MAT_GRP_1, 'SE', 1, 'HX', 2, 'IM', 3, 'FC', 4, 'IG', 5, 6), CUSTOMER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("BUMPING TYPE", "MAT_GRP_2 AS BUMPING_TYPE", "MAT.MAT_GRP_2", "MAT_GRP_2", "BUMPING_TYPE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PROCESS FLOW", "MAT_GRP_3 AS PROCESS_FLOW", "MAT.MAT_GRP_3", "MAT_GRP_3", "PROCESS_FLOW", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "MAT_GRP_4 AS LAYER", "MAT.MAT_GRP_4", "MAT_GRP_4", "LAYER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG TYPE", "MAT_GRP_5 AS PKG_TYPE", "MAT.MAT_GRP_5", "MAT_GRP_5", "PKG_TYPE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL PLATING", "MAT_GRP_6 AS RDL_PLATING", "MAT.MAT_GRP_6", "MAT_GRP_6", "RDL_PLATING", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FINAL BUMP", "MAT_GRP_7 AS FINAL_BUMP", "MAT.MAT_GRP_7", "MAT_GRP_7", "FINAL_BUMP", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SUB. MATERIAL", "MAT_GRP_8 AS SUB_MATERIAL", "MAT.MAT_GRP_8", "MAT_GRP_8", "SUB_MATERIAL", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SIZE", "MAT_CMF_14 AS WF_SIZE", "MAT.MAT_CMF_14", "MAT_CMF_14", "WF_SIZE", false); // \"SIZE\"
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("THICKNESS", "MAT_CMF_2 AS THICKNESS", "MAT.MAT_CMF_2", "MAT_CMF_2", "THICKNESS", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FLAT TYPE", "MAT_CMF_3 AS FLAT_TYPE", "MAT.MAT_CMF_3", "MAT_CMF_3", "FLAT_TYPE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("WAFER ORIENTATION", "MAT_CMF_4 AS WAFER_ORIENTATION", "MAT.MAT_CMF_4", "MAT_CMF_4", "WAFER_ORIENTATION", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "CONV_MAT_ID AS PRODUCT", "CONV_MAT_ID", "CONV_MAT_ID", "PRODUCT", false);
            }
            else
            {
                ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1) AS CUSTOMER", "MAT.MAT_GRP_1", "MAT_GRP_1", "DECODE(MAT_GRP_1, 'SE', 1, 'HX', 2, 'IM', 3, 'FC', 4, 'IG', 5, 6), CUSTOMER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR", "MAT_GRP_9 AS MAJOR", "MAT.MAT_GRP_9", "MAT_GRP_9", "MAJOR", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG", "MAT_GRP_10 AS PKG", "MAT.MAT_GRP_10", "MAT_GRP_10", "PKG", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD_COUNT", "MAT_GRP_6 AS LD_COUNT", "MAT.MAT_GRP_6", "MAT_GRP_6", "LD_COUNT", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG_CODE", "MAT_CMF_11 AS PKG_CODE", "MAT.MAT_CMF_11", "MAT_CMF_11", "PKG_CODE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT_GRP_2 AS FAMILY", "MAT.MAT_GRP_2", "MAT_GRP_2", "FAMILY", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "CONV_MAT_ID AS PRODUCT", "CONV_MAT_ID", "CONV_MAT_ID", "PRODUCT", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE_1", "MAT_GRP_4 AS TYPE_1", "MAT.MAT_GRP_4", "MAT_GRP_4", "TYPE_1", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE_2", "MAT_GRP_5 AS TYPE_2", "MAT.MAT_GRP_5", "MAT_GRP_5", "TYPE_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT_GRP_7 AS DENSITY", "MAT.MAT_GRP_7", "MAT_GRP_7", "DENSITY", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT_GRP_8 AS GENERATION", "MAT.MAT_GRP_8", "MAT_GRP_8", "GENERATION", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "MAT_CMF_10 AS PIN_TYPE", "MAT.MAT_CMF_10", "MAT_CMF_10", "PIN_TYPE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST_DEVICE", "MAT_CMF_7 AS CUST_DEVICE", "MAT.MAT_CMF_7", "MAT_CMF_7", "CUST_DEVICE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SALES_CODE", "MAT_CMF_8 AS SALES_CODE", "MAT.MAT_CMF_8", "MAT_CMF_8", "SALES_CODE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("", "", "", "", "", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("", "", "", "", "", false);
            }
        }


        //2020-09-07-이희석 쉬운유지보수를 위해 추가
        //같은컬럼명이 나와도 그전에 Span처리 되었다면 그 값은 사라진다.
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

        #region 시간관련 함수
        private void GetWorkDay()
        {
            DateTime Now = cdvDate.Value;
            FindWeek_SOP_A = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");
            FindWeek_SOP_SE = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "SE");
        }
        #endregion

        #region 재공표시 기준 설정
        private Dictionary<string, Visibles> GetWipVisibleList()
        {
            Dictionary<string, Visibles> WipVisible = new Dictionary<string, Visibles>();

          
                WipVisible.Add("GATE", Visibles.True);
                WipVisible.Add("WB9", Visibles.True);
                WipVisible.Add("DA9", Visibles.True);
                WipVisible.Add("WB8", Visibles.True);
                WipVisible.Add("DA8", Visibles.True);
                WipVisible.Add("WB7", Visibles.True);
                WipVisible.Add("DA7", Visibles.True);
                WipVisible.Add("WB6", Visibles.True);
                WipVisible.Add("DA6", Visibles.True);
                WipVisible.Add("WB5", Visibles.True);
                WipVisible.Add("DA5", Visibles.True);
                WipVisible.Add("WB4", Visibles.True);
            
            WipVisible.Add("HMKA3", Visibles.True);
            WipVisible.Add("QC_GATE", Visibles.True);
            WipVisible.Add("PVI", Visibles.True);
            WipVisible.Add("AVI", Visibles.True);
            WipVisible.Add("SIG", Visibles.True);
            WipVisible.Add("SBA", Visibles.True);
            WipVisible.Add("TIN", Visibles.True);
            WipVisible.Add("TRIM", Visibles.True);
            WipVisible.Add("MARK", Visibles.True);
            WipVisible.Add("CURE", Visibles.True);
            WipVisible.Add("MOLD", Visibles.True);
            WipVisible.Add("DA4", Visibles.True);
            WipVisible.Add("WB3", Visibles.True);
            WipVisible.Add("DA3", Visibles.True);
            WipVisible.Add("WB2", Visibles.True);
            WipVisible.Add("DA2", Visibles.True);
            WipVisible.Add("WB1", Visibles.True);
            WipVisible.Add("DA1", Visibles.True);
            WipVisible.Add("SP", Visibles.True);
            WipVisible.Add("SAW", Visibles.True);
            WipVisible.Add("BG", Visibles.True);
            WipVisible.Add("HMKA2", Visibles.True);

            //if (cdvType.Text == "Mold 관점")
            if (cdvType.SelectedIndex == 1)
            {
                WipVisible["HMKA3"] = Visibles.False;
                WipVisible["QC_GATE"] = Visibles.False;
                WipVisible["PVI"] = Visibles.False;
                WipVisible["AVI"] = Visibles.False;
                WipVisible["SIG"] = Visibles.False;
                WipVisible["SBA"] = Visibles.False;
                WipVisible["TIN"] = Visibles.False;
                WipVisible["TRIM"] = Visibles.False;
                WipVisible["MARK"] = Visibles.False;
                WipVisible["CURE"] = Visibles.True;
                WipVisible["MOLD"] = Visibles.True;
                WipVisible["GATE"] = Visibles.True;
                WipVisible["WB9"] = Visibles.True;
                WipVisible["DA9"] = Visibles.True;
                WipVisible["WB8"] = Visibles.True;
                WipVisible["DA8"] = Visibles.True;
                WipVisible["WB7"] = Visibles.True;
                WipVisible["DA7"] = Visibles.True;
                WipVisible["WB6"] = Visibles.True;
                WipVisible["DA6"] = Visibles.True;
                WipVisible["WB5"] = Visibles.True;
                WipVisible["DA5"] = Visibles.True;
                WipVisible["WB4"] = Visibles.True;
                WipVisible["DA4"] = Visibles.True;
                WipVisible["WB3"] = Visibles.True;
                WipVisible["DA3"] = Visibles.True;
                WipVisible["WB2"] = Visibles.True;
                WipVisible["DA2"] = Visibles.True;
                WipVisible["WB1"] = Visibles.True;
                WipVisible["DA1"] = Visibles.True;
                WipVisible["SP"] = Visibles.True;
                WipVisible["SAW"] = Visibles.True;
                WipVisible["BG"] = Visibles.True;
                WipVisible["HMKA2"] = Visibles.True;
            }
            //else if (cdvType.Text == "Front 관점")
            else if (cdvType.SelectedIndex == 2)
            {
                WipVisible["HMKA3"] = Visibles.False;
                WipVisible["QC_GATE"] = Visibles.False;
                WipVisible["PVI"] = Visibles.False;
                WipVisible["AVI"] = Visibles.False;
                WipVisible["SIG"] = Visibles.False;
                WipVisible["SBA"] = Visibles.False;
                WipVisible["TIN"] = Visibles.False;
                WipVisible["TRIM"] = Visibles.False;
                WipVisible["MARK"] = Visibles.False;
                WipVisible["CURE"] = Visibles.False;
                WipVisible["MOLD"] = Visibles.False;
                WipVisible["GATE"] = Visibles.True;
                WipVisible["WB9"] = Visibles.True;
                WipVisible["DA9"] = Visibles.True;
                WipVisible["WB8"] = Visibles.True;
                WipVisible["DA8"] = Visibles.True;
                WipVisible["WB7"] = Visibles.True;
                WipVisible["DA7"] = Visibles.True;
                WipVisible["WB6"] = Visibles.True;
                WipVisible["DA6"] = Visibles.True;
                WipVisible["WB5"] = Visibles.True;
                WipVisible["DA5"] = Visibles.True;
                WipVisible["WB4"] = Visibles.True;
                WipVisible["DA4"] = Visibles.True;
                WipVisible["WB3"] = Visibles.True;
                WipVisible["DA3"] = Visibles.True;
                WipVisible["WB2"] = Visibles.True;
                WipVisible["DA2"] = Visibles.True;
                WipVisible["WB1"] = Visibles.True;
                WipVisible["DA1"] = Visibles.True;
                WipVisible["SP"] = Visibles.True;
                WipVisible["SAW"] = Visibles.True;
                WipVisible["BG"] = Visibles.True;
                WipVisible["HMKA2"] = Visibles.True;
            }
            else if (cdvType.SelectedIndex == 3)
            {
                WipVisible["HMKA3"] = Visibles.True;
                WipVisible["QC_GATE"] = Visibles.True;
                WipVisible["PVI"] = Visibles.True;
                WipVisible["AVI"] = Visibles.True;
                WipVisible["SIG"] = Visibles.True;
                WipVisible["SBA"] = Visibles.True;
                WipVisible["TIN"] = Visibles.False;
                WipVisible["TRIM"] = Visibles.False;
                WipVisible["MARK"] = Visibles.True;
                WipVisible["CURE"] = Visibles.True;
                WipVisible["MOLD"] = Visibles.True;
                WipVisible["DA4"] = Visibles.False;
                WipVisible["WB3"] = Visibles.False;
                WipVisible["DA3"] = Visibles.False;
                WipVisible["WB2"] = Visibles.False;
                WipVisible["DA2"] = Visibles.False;
                WipVisible["WB1"] = Visibles.False;
                WipVisible["DA1"] = Visibles.False;
                WipVisible["SP"] = Visibles.False;
                WipVisible["SAW"] = Visibles.True;
                WipVisible["BG"] = Visibles.True;
                WipVisible["HMKA2"] = Visibles.True;
            }
            //1191214 이희석 DP 종합 관점으로 인한 조건 추가
            else if (cdvType.SelectedIndex == 4 || cdvType.SelectedIndex == 5 || cdvType.SelectedIndex == 6)
            {
                WipVisible["HMKA3"] = Visibles.False;
                WipVisible["QC_GATE"] = Visibles.False;
                WipVisible["PVI"] = Visibles.False;
                WipVisible["AVI"] = Visibles.False;
                WipVisible["SIG"] = Visibles.False;
                WipVisible["SBA"] = Visibles.False;
                WipVisible["TIN"] = Visibles.False;
                WipVisible["TRIM"] = Visibles.False;
                WipVisible["MARK"] = Visibles.False;
                WipVisible["CURE"] = Visibles.False;
                WipVisible["MOLD"] = Visibles.False;
                WipVisible["DA4"] = Visibles.False;
                WipVisible["WB3"] = Visibles.False;
                WipVisible["DA3"] = Visibles.False;
                WipVisible["WB2"] = Visibles.False;
                WipVisible["DA2"] = Visibles.False;
                WipVisible["WB1"] = Visibles.False;
                WipVisible["DA1"] = Visibles.False;
                WipVisible["SP"] = Visibles.False;
                WipVisible["SAW"] = Visibles.False;
                WipVisible["BG"] = Visibles.False;
                WipVisible["HMKA2"] = Visibles.False;
            }
            else
            {
                WipVisible["HMKA3"] = Visibles.True;
                WipVisible["QC_GATE"] = Visibles.True;
                WipVisible["PVI"] = Visibles.True;
                WipVisible["AVI"] = Visibles.True;
                WipVisible["SIG"] = Visibles.True;
                WipVisible["SBA"] = Visibles.True;
                WipVisible["TIN"] = Visibles.True;
                WipVisible["TRIM"] = Visibles.True;
                WipVisible["MARK"] = Visibles.True;
                WipVisible["CURE"] = Visibles.True;
                WipVisible["MOLD"] = Visibles.True;
                WipVisible["GATE"] = Visibles.True;
                WipVisible["WB9"] = Visibles.True;
                WipVisible["DA9"] = Visibles.True;
                WipVisible["WB8"] = Visibles.True;
                WipVisible["DA8"] = Visibles.True;
                WipVisible["WB7"] = Visibles.True;
                WipVisible["DA7"] = Visibles.True;
                WipVisible["WB6"] = Visibles.True;
                WipVisible["DA6"] = Visibles.True;
                WipVisible["WB5"] = Visibles.True;
                WipVisible["DA5"] = Visibles.True;
                WipVisible["WB4"] = Visibles.True;
                WipVisible["DA4"] = Visibles.True;
                WipVisible["WB3"] = Visibles.True;
                WipVisible["DA3"] = Visibles.True;
                WipVisible["WB2"] = Visibles.True;
                WipVisible["DA2"] = Visibles.True;
                WipVisible["WB1"] = Visibles.True;
                WipVisible["DA1"] = Visibles.True;
                WipVisible["SP"] = Visibles.True;
                WipVisible["SAW"] = Visibles.True;
                WipVisible["BG"] = Visibles.True;
                WipVisible["HMKA2"] = Visibles.True;
            }
            

            return WipVisible;
        }
        #endregion

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string QueryCond4 = null;

            string Yesterday;
            string Yesterdaybf;
            string Today;
            string sMonth;
            string sStartDate;
            string sMonth_dr;
            string sStartDate_dr;
            string sEndDate_dr;
            string sTable_fac;
            string sTable_wip;
            string strKpcs = "1000";

            Today = cdvDate.Value.ToString("yyyyMMdd");
            Yesterday = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");
            Yesterdaybf = cdvDate.Value.AddDays(-2).ToString("yyyyMMdd");
            sMonth = cdvDate.Value.ToString("yyyyMM");

            // 2020-09-29-임종우 : 25일 까지는 전월 26 ~ 금월 25 / 26일 부터는 금월 26 ~ 차월 25일
            if (cdvDate.Value.Day <= 25)
            {
                sStartDate_dr = cdvDate.Value.AddMonths(-1).ToString("yyyyMM") + "26";
                sEndDate_dr = sMonth + "25";
                sMonth_dr = sMonth;
            }
            else
            {
                sStartDate_dr = cdvDate.Value.ToString("yyyyMM") + "26";
                sEndDate_dr = cdvDate.Value.AddMonths(1).ToString("yyyyMM") + "25";
                sMonth_dr = cdvDate.Value.AddMonths(1).ToString("yyyyMM");
            }

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            // 조회월과 조회주차의 시작일이 같은 달이면 시작은 조회월의 1일자로 하고, 다르면(주차시작일이 작으면) 주차 시작일을 시작일로 한다.
            if (sMonth == FindWeek_SOP_A.StartDay_ThisWeek.Substring(0, 6))
            {
                sStartDate = sMonth + "01";
            }
            else
            {
                sStartDate = FindWeek_SOP_A.StartDay_ThisWeek;
            }

            if (ckbKpcs.Checked == true)
            {
                strKpcs = "DECODE(GUBUN, '설비대수', 1, 1000)";
            }
            else
            {
                strKpcs = "1";
            }

            //if (cdvTime.Text == "22시")
            if (cdvTime.SelectedIndex == 1)
            {
                sTable_wip = "RSUMWIPMOV";
                sTable_fac = "VSUMWIPOUT";
            }
            else
            {
                sTable_wip = "CSUMWIPMOV";
                sTable_fac = "VSUMWIPOUT_06";
            }
                        
            strSqlString.Append("SELECT " + QueryCond1 + " " + "\n");
            strSqlString.Append("     , ROUND(SUM(PLAN)/" + strKpcs + ",0) AS PLAN " + "\n");
            strSqlString.Append("     , ROUND(SUM(SHP)/" + strKpcs + ",0) AS SHP " + "\n");
            strSqlString.Append("     , ROUND(SUM(DEF)/" + strKpcs + ",0) AS DEF " + "\n");
            strSqlString.Append("     , GUBUN " + "\n");
            strSqlString.Append("     , ROUND(SUM(HMK3A)/" + strKpcs + ",0) AS HMK3A " + "\n");
            strSqlString.Append("     , ROUND(SUM(QC_GATE)/" + strKpcs + ",0) AS QC_GATE " + "\n");
            strSqlString.Append("     , ROUND(SUM(PVI)/" + strKpcs + ",0) AS PVI " + "\n");
            strSqlString.Append("     , ROUND(SUM(AVI)/" + strKpcs + ",0) AS AVI " + "\n");
            
            if (cdvType.SelectedIndex == 7)
            {
                strSqlString.Append("     , ROUND(SUM(DC)/" + strKpcs + ",0) AS DC " + "\n");
                strSqlString.Append("     , ROUND(SUM(MK2)/" + strKpcs + ",0) AS MK2 " + "\n");
                strSqlString.Append("     , ROUND(SUM(SIG)/" + strKpcs + ",0) AS SIG " + "\n");
                strSqlString.Append("     , ROUND(SUM(PP)/" + strKpcs + ",0) AS PP " + "\n");
                strSqlString.Append("     , ROUND(SUM(AOI)/" + strKpcs + ",0) AS AOI " + "\n");
                strSqlString.Append("     , ROUND(SUM(SBA)/" + strKpcs + ",0) AS SBA " + "\n");
                strSqlString.Append("     , ROUND(SUM(FLIPPER)/" + strKpcs + ",0) AS FLIPPER " + "\n");
            }
            else 
            {
                if (cdvType.SelectedIndex == 3)
                {
                    strSqlString.Append("     , ROUND(SUM(SIG)/" + strKpcs + ",0) AS SIG " + "\n");
                    strSqlString.Append("     , ROUND(SUM(MK)/" + strKpcs + ",0) AS MK " + "\n");
                    strSqlString.Append("     , ROUND(SUM(SBA)/" + strKpcs + ",0) AS SBA " + "\n");
                    strSqlString.Append("     , ROUND(SUM(TIN)/" + strKpcs + ",0) AS TIN " + "\n");
                    strSqlString.Append("     , ROUND(SUM(TRIM)/" + strKpcs + ",0) AS TRIM " + "\n");
                    strSqlString.Append("     , ROUND(SUM(CURE)/" + strKpcs + ",0) AS CURE " + "\n");
                    strSqlString.Append("     , ROUND(SUM(MOLD)/" + strKpcs + ",0) AS MOLD " + "\n");
                }
                else
                {
                    strSqlString.Append("     , ROUND(SUM(SIG)/" + strKpcs + ",0) AS SIG " + "\n");
                    strSqlString.Append("     , ROUND(SUM(SBA)/" + strKpcs + ",0) AS SBA " + "\n");
                    strSqlString.Append("     , ROUND(SUM(TIN)/" + strKpcs + ",0) AS TIN " + "\n");
                    strSqlString.Append("     , ROUND(SUM(TRIM)/" + strKpcs + ",0) AS TRIM " + "\n");
                    strSqlString.Append("     , ROUND(SUM(MK)/" + strKpcs + ",0) AS MK " + "\n");
                    strSqlString.Append("     , ROUND(SUM(CURE)/" + strKpcs + ",0) AS CURE " + "\n");
                    strSqlString.Append("     , ROUND(SUM(MOLD)/" + strKpcs + ",0) AS MOLD " + "\n");
                }
            }
            
            //1191214 이희석 F-CHIP 관점으로 인한 데이터 조건 추가
            if (cdvType.SelectedIndex == 3)
            {
                strSqlString.Append("     , ROUND(SUM(HSA_CURE)/" + strKpcs + ",0) AS \"HS/A CURE\" " + "\n");
                strSqlString.Append("     , ROUND(SUM(HS_Attach)/" + strKpcs + ",0) AS \"H/S Attach\" " + "\n");
                strSqlString.Append("     , ROUND(SUM(QC_GATE_3)/" + strKpcs + ",0) AS \"QC GATE 3\" " + "\n");
                strSqlString.Append("     , ROUND(SUM(BTM_SMT)/" + strKpcs + ",0) AS \"BTM SMT\" " + "\n");
                strSqlString.Append("     , ROUND(SUM(FC_Flipper)/" + strKpcs + ",0) AS \"FC Flipper\" " + "\n");
                strSqlString.Append("     , ROUND(SUM(UF_CURE)/" + strKpcs + ",0) AS \"U/F CURE\" " + "\n");
                strSqlString.Append("     , ROUND(SUM(UNDERFILL)/" + strKpcs + ",0) AS UNDERFILL " + "\n");
                strSqlString.Append("     , ROUND(SUM(PLASMA_1)/" + strKpcs + ",0) AS \"PLASMA 1\" " + "\n");
                strSqlString.Append("     , ROUND(SUM(PREBAKE_2)/" + strKpcs + ",0) AS \"PREBAKE 2\" " + "\n");
                strSqlString.Append("     , ROUND(SUM(DEFLUX)/" + strKpcs + ",0) AS DEFLUX " + "\n");
                strSqlString.Append("     , ROUND(SUM(CHIP_ATTACH)/" + strKpcs + ",0) AS \"CHIP ATTACH\" " + "\n");
                strSqlString.Append("     , ROUND(SUM(UV_EXPOSE_2)/" + strKpcs + ",0) AS \"UV EXPOSE 2\" " + "\n");
                strSqlString.Append("     , ROUND(SUM(DA4)/" + strKpcs + ",0) AS D4 " + "\n");
                strSqlString.Append("     , ROUND(SUM(WB3)/" + strKpcs + ",0) AS W3 " + "\n");
                strSqlString.Append("     , ROUND(SUM(DA3)/" + strKpcs + ",0) AS D3 " + "\n");
                strSqlString.Append("     , ROUND(SUM(WB2)/" + strKpcs + ",0) AS W2 " + "\n");
                strSqlString.Append("     , ROUND(SUM(DA2)/" + strKpcs + ",0) AS D2 " + "\n");
                strSqlString.Append("     , ROUND(SUM(WB1)/" + strKpcs + ",0) AS W1 " + "\n");
                strSqlString.Append("     , ROUND(SUM(DA1)/" + strKpcs + ",0) AS D1 " + "\n");
                strSqlString.Append("     , ROUND(SUM(SP)/" + strKpcs + ",0) AS SP " + "\n");
                strSqlString.Append("     , ROUND(SUM(SAW)/" + strKpcs + ",0) AS SAW " + "\n");
                strSqlString.Append("     , ROUND(SUM(BG)/" + strKpcs + ",0) AS BG " + "\n");
                strSqlString.Append("     , ROUND(SUM(HMK2A)/" + strKpcs + ",0) AS HMK2A " + "\n");
            }
            else if (cdvType.SelectedIndex == 4 || cdvType.SelectedIndex == 5 || cdvType.SelectedIndex == 6)
            {
                strSqlString.Append("     , ROUND(SUM(COB_AVI)/" + strKpcs + ",0) AS COB_AVI " + "\n");
                strSqlString.Append("     , ROUND(SUM(UV)/" + strKpcs + ",0) AS UV " + "\n");
                strSqlString.Append("     , ROUND(SUM(WE)/" + strKpcs + ",0) AS WE " + "\n");
                strSqlString.Append("     , ROUND(SUM(SAWING)/" + strKpcs + ",0) AS SAWING " + "\n");
                strSqlString.Append("     , ROUND(SUM(LG)/" + strKpcs + ",0) AS LG " + "\n");
                strSqlString.Append("     , ROUND(SUM(TAPE_MOUNT)/" + strKpcs + ",0) AS TAPE_MOUNT " + "\n");
                strSqlString.Append("     , ROUND(SUM(BS_MARKING)/" + strKpcs + ",0) AS BS_MARKING " + "\n");
                strSqlString.Append("     , ROUND(SUM(COATINGTCURE)/" + strKpcs + ",0) AS COATINGTCURE " + "\n");
                strSqlString.Append("     , ROUND(SUM(BSCOATING)/" + strKpcs + ",0) AS BSCOATING " + "\n");
                strSqlString.Append("     , ROUND(SUM(DETACH)/" + strKpcs + ",0) AS DETACH " + "\n");
                strSqlString.Append("     , ROUND(SUM(BG)/" + strKpcs + ",0) AS BG " + "\n");
                strSqlString.Append("     , ROUND(SUM(STEALTH)/" + strKpcs + ",0) AS STEALTH " + "\n");
                strSqlString.Append("     , ROUND(SUM(PRE_BG)/" + strKpcs + ",0) AS PRE_BG" + "\n");
                strSqlString.Append("     , ROUND(SUM(LN)/" + strKpcs + ",0) AS LN " + "\n");
                strSqlString.Append("     , ROUND(SUM(DIE_BANK)/" + strKpcs + ",0) AS DIE_BANK " + "\n");
                //1191214 이희석 : 밑에 컬럼들은 전체 수량과 순서를 맞추기 위해 의미없는 값 추가
                strSqlString.Append("     , '' " + "\n");
                strSqlString.Append("     , '' " + "\n");
                strSqlString.Append("     , '' " + "\n");
                strSqlString.Append("     , '' " + "\n");
                strSqlString.Append("     , '' " + "\n");
                strSqlString.Append("     , '' " + "\n");
                strSqlString.Append("     , '' " + "\n");
                strSqlString.Append("     , '' " + "\n");
            }
            else
            {
                strSqlString.Append("     , ROUND(SUM(GATE)/" + strKpcs + ",0) AS GATE " + "\n");
                strSqlString.Append("     , ROUND(SUM(WB9)/" + strKpcs + ",0) AS WB9 " + "\n");
                strSqlString.Append("     , ROUND(SUM(DA9)/" + strKpcs + ",0) AS DA9 " + "\n");
                strSqlString.Append("     , ROUND(SUM(WB8)/" + strKpcs + ",0) AS WB8 " + "\n");
                strSqlString.Append("     , ROUND(SUM(DA8)/" + strKpcs + ",0) AS DA8 " + "\n");
                strSqlString.Append("     , ROUND(SUM(WB7)/" + strKpcs + ",0) AS WB7 " + "\n");
                strSqlString.Append("     , ROUND(SUM(DA7)/" + strKpcs + ",0) AS DA7 " + "\n");
                strSqlString.Append("     , ROUND(SUM(WB6)/" + strKpcs + ",0) AS WB6 " + "\n");
                strSqlString.Append("     , ROUND(SUM(DA6)/" + strKpcs + ",0) AS DA6 " + "\n");
                strSqlString.Append("     , ROUND(SUM(WB5)/" + strKpcs + ",0) AS WB5 " + "\n");
                strSqlString.Append("     , ROUND(SUM(DA5)/" + strKpcs + ",0) AS DA5 " + "\n");
                strSqlString.Append("     , ROUND(SUM(WB4)/" + strKpcs + ",0) AS WB4 " + "\n");
                strSqlString.Append("     , ROUND(SUM(DA4)/" + strKpcs + ",0) AS DA4 " + "\n");
                strSqlString.Append("     , ROUND(SUM(WB3)/" + strKpcs + ",0) AS WB3 " + "\n");
                strSqlString.Append("     , ROUND(SUM(DA3)/" + strKpcs + ",0) AS DA3 " + "\n");
                strSqlString.Append("     , ROUND(SUM(WB2)/" + strKpcs + ",0) AS WB2 " + "\n");
                strSqlString.Append("     , ROUND(SUM(DA2)/" + strKpcs + ",0) AS DA2 " + "\n");
                strSqlString.Append("     , ROUND(SUM(WB1)/" + strKpcs + ",0) AS WB1 " + "\n");
                strSqlString.Append("     , ROUND(SUM(DA1)/" + strKpcs + ",0) AS DA1 " + "\n");
                strSqlString.Append("     , ROUND(SUM(SP)/" + strKpcs + ",0) AS SP " + "\n");
                strSqlString.Append("     , ROUND(SUM(SAW)/" + strKpcs + ",0) AS SAW " + "\n");
                strSqlString.Append("     , ROUND(SUM(BG)/" + strKpcs + ",0) AS BG " + "\n");
                strSqlString.Append("     , ROUND(SUM(HMK2A)/" + strKpcs + ",0) AS HMK2A " + "\n");
            }
            
            strSqlString.Append("     , ROUND(SUM(WIP_DEF)/" + strKpcs + ",0) AS WIP_DEF " + "\n");
            strSqlString.Append("  FROM (  " + "\n");
            strSqlString.Append("        SELECT " + QueryCond3 + ", GUBUN " + "\n");
            strSqlString.Append("             , MAX(PLAN) AS PLAN " + "\n");
            strSqlString.Append("             , MAX(SHP) AS SHP " + "\n");
            strSqlString.Append("             , MAX(DEF) AS DEF " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK3A', SUM_DEF, 0)) AS HMK3A " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'QC_GATE', SUM_DEF, 0)) AS QC_GATE " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PVI', SUM_DEF, 0)) AS PVI " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'AVI', SUM_DEF, 0)) AS AVI " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SIG', SUM_DEF, 0)) AS SIG " + "\n");
            if (cdvType.SelectedIndex == 7)
            {
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DC', SUM_DEF, 0)) AS DC " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'MK2', SUM_DEF, 0)) AS MK2 " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'AOI', SUM_DEF, 0)) AS AOI " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PP', SUM_DEF, 0)) AS PP " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'FLIPPER', SUM_DEF, 0)) AS FLIPPER " + "\n");
            }
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SBA', SUM_DEF, 0)) AS SBA " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'TIN', SUM_DEF, 0)) AS TIN " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'TRIM', SUM_DEF, 0)) AS TRIM " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'MK', SUM_DEF, 0)) AS MK " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'CURE', SUM_DEF, 0)) AS CURE " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'MOLD', SUM_DEF, 0)) AS MOLD " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'GATE', SUM_DEF, 0)) AS GATE " + "\n");


            //1191214 이희석 F-CHIP 관점으로 인한 데이터 조건 추가
            if (cdvType.SelectedIndex == 3)
            {
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'UV EXPOSE 2', SUM_DEF, 0)) AS UV_EXPOSE_2 " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'CHIP ATTACH', SUM_DEF, 0)) AS CHIP_ATTACH " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DEFLUX', SUM_DEF, 0)) AS DEFLUX " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PREBAKE 2', SUM_DEF, 0)) AS PREBAKE_2 " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PLASMA 1', SUM_DEF, 0)) AS PLASMA_1 " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'UNDERFILL', SUM_DEF, 0)) AS UNDERFILL " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'U/F CURE', SUM_DEF, 0)) AS UF_CURE " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'FC Flipper', SUM_DEF, 0)) AS FC_Flipper " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BTM SMT', SUM_DEF, 0)) AS BTM_SMT " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'QC GATE 3', SUM_DEF, 0)) AS QC_GATE_3 " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'H/S Attach', SUM_DEF, 0)) AS HS_Attach " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HS/A CURE', SUM_DEF, 0)) AS HSA_CURE " + "\n");
            }
            //1191214 이희석 DP 종합 관점 조건 추가
            else if (cdvType.SelectedIndex == 4 || cdvType.SelectedIndex == 5 || cdvType.SelectedIndex == 6)
            {
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DIE_BANK', SUM_DEF, 0)) AS DIE_BANK " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'LN', SUM_DEF, 0)) AS LN " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PRE_BG', SUM_DEF, 0)) AS PRE_BG" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'STEALTH', SUM_DEF, 0)) AS STEALTH" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DETACH', SUM_DEF, 0)) AS DETACH " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BSCOATING', SUM_DEF, 0)) AS BSCOATING " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'COATINGTCURE', SUM_DEF, 0)) AS COATINGTCURE " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BS_MARKING', SUM_DEF, 0)) AS BS_MARKING " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'TAPE_MOUNT', SUM_DEF, 0)) AS TAPE_MOUNT " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'LG', SUM_DEF, 0)) AS LG " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SAWING', SUM_DEF, 0)) AS SAWING " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'WE', SUM_DEF, 0)) AS WE " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'UV', SUM_DEF, 0)) AS UV " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'COB_AVI', SUM_DEF, 0)) AS COB_AVI " + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'EXTRA', SUM_DEF, 0)) AS EXTRA " + "\n");
            }

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'WB9', SUM_DEF, 0)) AS WB9 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DA9', SUM_DEF, 0)) AS DA9 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'WB8', SUM_DEF, 0)) AS WB8 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DA8', SUM_DEF, 0)) AS DA8 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'WB7', SUM_DEF, 0)) AS WB7 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DA7', SUM_DEF, 0)) AS DA7 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'WB6', SUM_DEF, 0)) AS WB6 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DA6', SUM_DEF, 0)) AS DA6 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'WB5', SUM_DEF, 0)) AS WB5 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DA5', SUM_DEF, 0)) AS DA5 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'WB4', SUM_DEF, 0)) AS WB4 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DA4', SUM_DEF, 0)) AS DA4 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'WB3', SUM_DEF, 0)) AS WB3 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DA3', SUM_DEF, 0)) AS DA3 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'WB2', SUM_DEF, 0)) AS WB2 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DA2', SUM_DEF, 0)) AS DA2 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'WB1', SUM_DEF, 0)) AS WB1 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DA1', SUM_DEF, 0)) AS DA1 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SP', SUM_DEF, 0)) AS SP " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SAW', SUM_DEF, 0)) AS SAW " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BG', SUM_DEF, 0)) AS BG " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK2A',SUM_DEF, 0)) AS HMK2A " + "\n");
            strSqlString.Append("             , MAX(WIP_TTL) AS WIP_TTL " + "\n");
            strSqlString.Append("             , MAX(DEF) - MAX(WIP_TTL) AS WIP_DEF " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT " + QueryCond3 + " " + "\n");
            strSqlString.Append("                     , OPER_GRP_1 " + "\n");
            strSqlString.Append("                     , DECODE(SEQ, 1, 'WIP', 2, '설비대수', 3, 'CAPA현황', 4, '당일 실적', 5, 'D0 잔량', 6, 'D1 잔량', 7, 'D2 잔량', 8, '당주 잔량', 9, '차주 잔량', 10, '월간 잔량') AS GUBUN " + "\n");
            strSqlString.Append("                     , SUM(DECODE(SEQ, 1, 0, 2, 0, 3, 0, 4, 0, 5, D0_PLAN, 6, D1_PLAN, 7, D2_PLAN, 8, WEEK_PLAN, 9, WEEK2_PLAN, 10, MON_PLAN)) AS PLAN  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(SEQ, 1, 0, 2, 0, 3, 0, 4, 0, 5, SHP_TODAY, 6, SHP_D1, 7, SHP_D2, 8, SHP_WEEK, 9, SHP2_WEEK, 10, SHP_MONTH)) AS SHP " + "\n");
            strSqlString.Append("                     , SUM(DECODE(SEQ, 1, 0, 2, 0, 3, 0, 4, 0, 5, D0_DEF, 6, D1_DEF, 7, D2_DEF, 8, WEEK_DEF, 9, WEEK2_DEF, 10, MONTH_DEF)) AS DEF " + "\n");
            strSqlString.Append("                     , SUM(DECODE(SEQ, 1, WIP_QTY, 2, RES_CNT, 3, RES_CAPA, 4, ASSY_END_QTY, 5, D0_SUM_QUANTITY, 6, D1_SUM_QUANTITY, 7, D2_SUM_QUANTITY, 8, WEEK_SUM_QUANTITY, 9, WEEK2_SUM_QUANTITY, 10, MONTH_SUM_QUANTITY)) AS SUM_DEF " + "\n");
            strSqlString.Append("                     , MAX(DECODE(SEQ, 1, 0, 2, 0, 3, 0, 4, 0, WIP_TTL)) AS WIP_TTL " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT " + QueryCond3 + ", OPER_GRP_1 " + "\n");
            strSqlString.Append("                             , SUM(NVL(D0_PLAN,0)) AS D0_PLAN, SUM(NVL(SHP_TODAY,0)) AS SHP_TODAY, SUM(NVL(D0_DEF,0)) AS D0_DEF " + "\n");
            strSqlString.Append("                             , SUM(NVL(D1_PLAN,0)) AS D1_PLAN, SUM(NVL(SHP_D1,0)) AS SHP_D1, SUM(NVL(D1_DEF,0)) AS D1_DEF " + "\n");
            strSqlString.Append("                             , SUM(NVL(D2_PLAN,0)) AS D2_PLAN, SUM(NVL(SHP_D2,0)) AS SHP_D2, SUM(NVL(D2_DEF,0)) AS D2_DEF " + "\n");
            strSqlString.Append("                             , SUM(NVL(WEEK_PLAN,0)) AS WEEK_PLAN, SUM(NVL(SHP_WEEK,0)) AS SHP_WEEK, SUM(NVL(WEEK_DEF,0)) AS WEEK_DEF " + "\n");
            strSqlString.Append("                             , SUM(NVL(WEEK2_PLAN,0)) AS WEEK2_PLAN, SUM(NVL(SHP2_WEEK,0)) AS SHP2_WEEK, SUM(NVL(WEEK2_DEF,0)) AS WEEK2_DEF " + "\n");
            strSqlString.Append("                             , SUM(NVL(MON_PLAN,0)) AS MON_PLAN, SUM(NVL(SHP_MONTH,0)) AS SHP_MONTH, SUM(NVL(MONTH_DEF,0)) AS MONTH_DEF " + "\n");
            strSqlString.Append("                             , SUM(NVL(ASSY_END_QTY,0)) AS ASSY_END_QTY " + "\n");
            strSqlString.Append("                             , SUM(NVL(WIP_QTY,0)) AS WIP_QTY " + "\n");
            strSqlString.Append("                             , SUM(SUM(WIP_QTY)) OVER(PARTITION BY " + QueryCond3 + ") AS WIP_TTL " + "\n");
            strSqlString.Append("                             , SUM(RES_CNT) AS RES_CNT " + "\n");
            strSqlString.Append("                             , SUM(RES_CAPA) AS RES_CAPA " + "\n");
            strSqlString.Append("                             , SUM(NVL(D0_DEF-WIP_SUM_QUANTITY+WIP_QTY,0)) AS D0_SUM_QUANTITY " + "\n");
            strSqlString.Append("                             , SUM(NVL(D1_DEF-WIP_SUM_QUANTITY+WIP_QTY,0)) AS D1_SUM_QUANTITY " + "\n");
            strSqlString.Append("                             , SUM(NVL(D2_DEF-WIP_SUM_QUANTITY+WIP_QTY,0)) AS D2_SUM_QUANTITY " + "\n");
            strSqlString.Append("                             , SUM(NVL(WEEK_DEF-WIP_SUM_QUANTITY+WIP_QTY,0)) AS WEEK_SUM_QUANTITY " + "\n");
            strSqlString.Append("                             , SUM(NVL(WEEK2_DEF-WIP_SUM_QUANTITY+WIP_QTY,0)) AS WEEK2_SUM_QUANTITY " + "\n");
            strSqlString.Append("                             , SUM(NVL(MONTH_DEF-WIP_SUM_QUANTITY+WIP_QTY,0)) AS MONTH_SUM_QUANTITY " + "\n");
            strSqlString.Append("                          FROM ( " + "\n");
            strSqlString.Append("                          SELECT * FROM ( " + "\n");
            strSqlString.Append("                                SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_CMF_10, MAT.MAT_CMF_7, MAT.MAT_CMF_8, MAT.MAT_CMF_11, MAT.MAT_ID, MAT.CONV_MAT_ID, MAT.OPER_GRP_1 " + "\n");
            strSqlString.Append("                                     , A.D0_PLAN, A.SHP_TODAY, A.D0_DEF " + "\n");
            strSqlString.Append("                                     , A.D1_PLAN, A.SHP_D1, A.D1_DEF " + "\n");
            strSqlString.Append("                                     , A.D2_PLAN, A.SHP_D2, A.D2_DEF " + "\n");
            strSqlString.Append("                                     , A.WEEK_PLAN, A.SHP_WEEK, A.WEEK_DEF " + "\n");
            strSqlString.Append("                                     , A.WEEK2_PLAN, A.SHP2_WEEK, A.WEEK2_DEF " + "\n");
            strSqlString.Append("                                     , A.MON_PLAN, A.SHP_MONTH, A.MONTH_DEF " + "\n");
            strSqlString.Append("                                     , A.D0_ORI_PLAN " + "\n");
            strSqlString.Append("                                     , NVL(B.ASSY_END_QTY,0) AS ASSY_END_QTY " + "\n");
            strSqlString.Append("                                     , MAX(NVL(B.ASSY_END_QTY,0)) OVER(PARTITION BY " + QueryCond2 + ") AS CHK_ASSY_END_QTY " + "\n");
            strSqlString.Append("                                     , NVL(WIP.QTY,0) AS WIP_QTY " + "\n");
            strSqlString.Append("                                     , RES_CNT " + "\n");
            strSqlString.Append("                                     , RES_CAPA " + "\n");
            
            //1191214 이희석 F-CHIP 관점으로 인한 데이터 조건 추가
            if (cdvType.SelectedIndex == 3)
            {
                strSqlString.Append("                                     , SUM(NVL(WIP.QTY,0)) OVER(PARTITION BY A.MAT_ID ORDER BY MAT.MAT_ID, DECODE(MAT.OPER_GRP_1, 'HMK3A', 1, 'QC_GATE', 2, 'PVI', 3, 'AVI', 4, 'SIG', 5, 'SBA', 6, 'TIN', 7, 'TRIM', 8, 'MK', 9, 'CURE', 10, 'MOLD', 11, 'GATE', 12 " + "\n");
                strSqlString.Append("                                                                                                                                , 'HS/A CURE', 13, 'H/S Attach', 14, 'QC GATE 3', 15, 'BTM SMT', 16, 'FC Flipper', 17, 'U/F CURE', 18, 'UNDERFILL', 19, 'PLASMA 1', 20, 'PREBAKE 2', 21, 'DEFLUX', 22, 'CHIP ATTACH', 23, 'UV EXPOSE 2', 24 , 'WB3', 25, 'DA3', 26, 'WB2', 27, 'DA2', 28" + "\n");
                strSqlString.Append("                                                                                                                                , 'WB1', 29, 'DA1', 30, 'SP', 31, 'SAW', 32, 'BG', 33, 'HMK2A',34, 35)) AS WIP_SUM_QUANTITY " + "\n");
            }
            //1191214 이희석 DP 종합 관점으로 인한 데이터 조건 추가
            else if (cdvType.SelectedIndex == 4 || cdvType.SelectedIndex == 5 || cdvType.SelectedIndex == 6)
            {
                strSqlString.Append("                                     , SUM(NVL(WIP.QTY,0)) OVER(PARTITION BY A.MAT_ID ORDER BY MAT.MAT_ID, DECODE(MAT.OPER_GRP_1, 'EXTRA',1,'COB_AVI', 2, 'UV', 3,'WE' , 4,'SAWING' , 5,  'LG', 6, 'TAPE_MOUNT', 7, 'BS_MARKING', 8, 'COATINGTCURE', 9, 'BSCOATING', 10, 'DETACH', 11,'BG', 12, 'STEALTH', 13, 'PRE_BG', 14, 'LN', 15,'DIE_BANK',16,17)) AS WIP_SUM_QUANTITY" + "\n");
                                                                                                                                
            }
            else if (cdvType.SelectedIndex == 7)
            {
                strSqlString.Append("                                     , SUM(NVL(WIP.QTY,0)) OVER(PARTITION BY A.MAT_ID ORDER BY MAT.MAT_ID, DECODE(MAT.OPER_GRP_1, 'HMK3A', 1, 'QC_GATE', 2, 'PVI', 3, 'AVI', 4, 'DC', 5, 'MK2', 6, 'SIG', 7, 'PP', 8, 'AOI', 9, 'SBA', 10, 'FLIPPER', 11, 'TIN', 12, 'TRIM', 13, 'MK', 14, 'CURE', 15, 'MOLD', 16, 'GATE', 17 " + "\n");
                strSqlString.Append("                                                                                                                                , 'WB9', 18, 'DA9', 19, 'WB8', 20, 'DA8', 21, 'WB7', 22, 'DA7', 23, 'WB6', 24, 'DA6', 25, 'WB5', 26, 'DA5', 27, 'WB4', 28, 'DA4', 29, 'WB3', 30, 'DA3', 31, 'WB2', 32, 'DA2', 33" + "\n");
                strSqlString.Append("                                                                                                                                , 'WB1', 34, 'DA1', 35, 'SP', 36, 'SAW', 37, 'BG', 38, 'HMK2A',39, 40)) AS WIP_SUM_QUANTITY " + "\n");
            }
            else
            {
                strSqlString.Append("                                     , SUM(NVL(WIP.QTY,0)) OVER(PARTITION BY A.MAT_ID ORDER BY MAT.MAT_ID, DECODE(MAT.OPER_GRP_1, 'HMK3A', 1, 'QC_GATE', 2, 'PVI', 3, 'AVI', 4, 'SIG', 5, 'SBA', 6, 'TIN', 7, 'TRIM', 8, 'MK', 9, 'CURE', 10, 'MOLD', 11, 'GATE', 12 " + "\n");
                strSqlString.Append("                                                                                                                                , 'WB9', 13, 'DA9', 14, 'WB8', 15, 'DA8', 16, 'WB7', 17, 'DA7', 18, 'WB6', 19, 'DA6', 20, 'WB5', 21, 'DA5', 22, 'WB4', 23, 'DA4', 24, 'WB3', 25, 'DA3', 26, 'WB2', 27, 'DA2', 28" + "\n");
                strSqlString.Append("                                                                                                                                , 'WB1', 29, 'DA1', 30, 'SP', 31, 'SAW', 32, 'BG', 33, 'HMK2A',34, 35)) AS WIP_SUM_QUANTITY " + "\n");

            }
            
            strSqlString.Append("                                  FROM ( " + "\n");
            strSqlString.Append("                                        SELECT MAT_GRP_1, MAT_GRP_2, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, MAT_GRP_9, MAT_GRP_10, MAT_CMF_10, MAT_CMF_7, MAT_CMF_8, MAT_CMF_11, MAT_ID, OPER_GRP_1 " + "\n");
            strSqlString.Append("                                             , CASE WHEN MAT_GRP_1 = 'SE' AND MAT_GRP_9 = 'MEMORY' THEN 'SEK_________-___' || SUBSTR(MAT_ID, -3) " + "\n");
            strSqlString.Append("                                                    WHEN MAT_GRP_1 = 'HX' THEN MAT_CMF_10 " + "\n");
            strSqlString.Append("                                                    ELSE MAT_ID " + "\n");
            strSqlString.Append("                                                END CONV_MAT_ID " + "\n");
            strSqlString.Append("                                          FROM ( " + "\n");
            strSqlString.Append("                                                SELECT * " + "\n");
            strSqlString.Append("                                                  FROM (" + "\n");
            strSqlString.Append("                                                        SELECT A.* " + "\n");

            if (ckbCmold.Checked == true)
            {
                strSqlString.Append("                                                             , (SELECT /*+INDEX_DESC(MGCMTBLDAT MGCMTBLDAT2_PK)*/ KEY_3 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_PKG_2D_CMOLD' AND KEY_1 = 'C-MOLD'AND KEY_2 = A.MAT_GRP_1 AND KEY_3 = A.MAT_CMF_11 AND (KEY_4 = '%' OR A.MAT_ID LIKE KEY_4) AND ROWNUM = 1) AS KEY_3 " + "\n");
            }

            strSqlString.Append("                                                          FROM MWIPMATDEF A" + "\n");
            strSqlString.Append("                                                         WHERE 1=1 " + "\n");
            strSqlString.Append("                                                           AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                                                           AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                                                           AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                                                           AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

            if (ckbCOB.Checked == true)
            {
                strSqlString.Append("                                                           AND MAT_GRP_10 NOT LIKE 'COB%' " + "\n");
            }
            //1191214 이희석 F-CHIP 관점으로 인한 데이터 조건 추가
            if (cdvType.SelectedIndex == 3)
            {
                strSqlString.Append("                                                           AND (MAT_GRP_3 IN ('FCBGA', 'FCFBGA', 'FCBGAH') OR MAT_GRP_10 ='UMCP')" + "\n");
                strSqlString.Append("                                                           AND LAST_FLOW IN (SELECT FLOW FROM MWIPFLWOPR WHERE OPER='A0333')" + "\n");
                strSqlString.Append("                                                           AND LAST_FLOW NOT IN (SELECT FLOW FROM MWIPFLWOPR WHERE OPER LIKE 'A040_')" + "\n");
            }
            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                                                           AND MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                                                           AND MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                                                           AND MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                                                           AND MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                                                           AND MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                                                           AND MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                                                           AND MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                                                           AND MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                                                           AND MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion
            
            strSqlString.Append("                                                       ) " + "\n");

            if (ckbCmold.Checked == true)
            {
                strSqlString.Append("                                                 WHERE KEY_3 IS NOT NULL " + "\n");
            }

            strSqlString.Append("                                               ) " + "\n");
            strSqlString.Append("                                             , ( " + "\n");
            //1191214 이희석 수정  F-CHIP 관점으로 인한 데이터 조건 추가
            if (cdvType.SelectedIndex == 3)
            {
                strSqlString.Append("                                                SELECT DECODE(LEVEL, 1, 'HMK3A', 2, 'QC_GATE', 3, 'PVI', 4, 'AVI', 5, 'SIG', 6, 'SBA', 7, 'TIN', 8, 'TRIM', 9, 'MK', 10, 'CURE', 11, 'MOLD', 12, 'HS/A CURE' " + "\n");
                strSqlString.Append("                                                                   , 13, 'H/S Attach', 14, 'QC GATE 3', 15, 'BTM SMT', 16, 'FC Flipper', 17, 'U/F CURE', 18, 'UNDERFILL', 19, 'PLASMA 1', 20, 'PREBAKE 2', 21, 'DEFLUX', 22, 'CHIP ATTACH', 23, 'UV EXPOSE 2', 24, 'DA4'" + "\n");
                strSqlString.Append("                                                                   , 25, 'WB3', 26, 'DA3', 27, 'WB2', 28, 'DA2', 29, 'WB1', 30, 'DA1', 31, 'SP', 32, 'SAW', 33, 'BG', 34, 'HMK2A' " + "\n");
                strSqlString.Append("                                                             ) OPER_GRP_1 " + "\n");
                strSqlString.Append("                                                  FROM DUAL CONNECT BY LEVEL <= 34 " + "\n");
            }
            else if (cdvType.SelectedIndex == 4 || cdvType.SelectedIndex == 5 || cdvType.SelectedIndex == 6)
            {
                strSqlString.Append("                                                SELECT DECODE(LEVEL,1, 'EXTRA',2, 'COB_AVI', 3,'UV' , 4, 'WE', 5, 'SAWING' , 6,'LG' , 7,'TAPE_MOUNT' , 8, 'BS_MARKING', 9, 'COATINGTCURE', 10, 'BSCOATING', 11, 'DETACH', 12, 'BG', 13, 'STEALTH', 14, 'PRE_BG', 15, 'LN', 16,'DIE_BANK' " + "\n");
                strSqlString.Append("                                                             ) OPER_GRP_1 " + "\n");
                strSqlString.Append("                                                  FROM DUAL CONNECT BY LEVEL <= 16 " + "\n");
            }
            else if (cdvType.SelectedIndex == 7)
            {
                strSqlString.Append("                                                SELECT DECODE(LEVEL, 1, 'HMK3A', 2, 'QC_GATE', 3, 'PVI', 4, 'AVI', 5, 'DC', 6, 'MK2', 7,'SIG', 8, 'PP', 9, 'AOI', 10, 'SBA', 11, 'FLIPPER', 12, 'TIN', 13, 'TRIM', 14, 'MK', 15, 'CURE', 16, 'MOLD', 17, 'GATE' " + "\n");
                strSqlString.Append("                                                                   , 18, 'WB9', 19, 'DA9', 20, 'WB8', 21, 'DA8', 22, 'WB7', 23, 'DA7', 24, 'WB6', 25, 'DA6', 26, 'WB5', 27, 'DA5', 28, 'WB4', 29, 'DA4'" + "\n");
                strSqlString.Append("                                                                   , 30, 'WB3', 31, 'DA3', 32, 'WB2', 33, 'DA2', 34, 'WB1', 35, 'DA1', 36, 'SP', 37, 'SAW', 38, 'BG', 39, 'HMK2A' " + "\n");
                strSqlString.Append("                                                             ) OPER_GRP_1 " + "\n");
                strSqlString.Append("                                                  FROM DUAL CONNECT BY LEVEL <= 39 " + "\n");
            }
            else
            {
                strSqlString.Append("                                                SELECT DECODE(LEVEL, 1, 'HMK3A', 2, 'QC_GATE', 3, 'PVI', 4, 'AVI', 5, 'SIG', 6, 'SBA', 7, 'TIN', 8, 'TRIM', 9, 'MK', 10, 'CURE', 11, 'MOLD', 12, 'GATE' " + "\n");
                strSqlString.Append("                                                                   , 13, 'WB9', 14, 'DA9', 15, 'WB8', 16, 'DA8', 17, 'WB7', 18, 'DA7', 19, 'WB6', 20, 'DA6', 21, 'WB5', 22, 'DA5', 23, 'WB4', 24, 'DA4'" + "\n");
                strSqlString.Append("                                                                   , 25, 'WB3', 26, 'DA3', 27, 'WB2', 28, 'DA2', 29, 'WB1', 30, 'DA1', 31, 'SP', 32, 'SAW', 33, 'BG', 34, 'HMK2A' " + "\n");
                strSqlString.Append("                                                             ) OPER_GRP_1 " + "\n");
                strSqlString.Append("                                                  FROM DUAL CONNECT BY LEVEL <= 34 " + "\n");
            }
            

            strSqlString.Append("                                               ) " + "\n");                      
            strSqlString.Append("                                       ) MAT " + "\n");
            strSqlString.Append("                                     , ( " + "\n");
            strSqlString.Append("                                        SELECT MAT.MAT_ID " + "\n");
            strSqlString.Append("                                             , SUM(NVL(D0_PLAN, 0) + (NVL(WEEK1_TTL, 0) - NVL(SHP_WEEK_TTL, 0))) AS D0_PLAN " + "\n");
            strSqlString.Append("                                             , SUM(NVL(D0_PLAN, 0)) AS D0_ORI_PLAN " + "\n");
            strSqlString.Append("                                             , SUM(NVL(SHP_TODAY, 0)) AS SHP_TODAY " + "\n");
            strSqlString.Append("                                             , SUM(NVL(D0_PLAN, 0) + (NVL(WEEK1_TTL, 0) - NVL(SHP_WEEK_TTL, 0)) - NVL(SHP_TODAY, 0)) AS D0_DEF " + "\n");
            strSqlString.Append("                                             , SUM(NVL(D1_PLAN, 0)) AS D1_PLAN " + "\n");
            strSqlString.Append("                                             , 0 AS SHP_D1 " + "\n");
            strSqlString.Append("                                             , SUM(NVL(D1_PLAN, 0) + NVL(D0_PLAN, 0) + (NVL(WEEK1_TTL, 0) - NVL(SHP_WEEK_TTL, 0)) - NVL(SHP_TODAY, 0)) AS D1_DEF " + "\n");
            strSqlString.Append("                                             , SUM(NVL(D2_PLAN, 0)) AS D2_PLAN " + "\n");
            strSqlString.Append("                                             , 0 AS SHP_D2 " + "\n");
            strSqlString.Append("                                             , SUM(NVL(D2_PLAN, 0) + NVL(D1_PLAN, 0) + NVL(D0_PLAN, 0) + (NVL(WEEK1_TTL, 0) - NVL(SHP_WEEK_TTL, 0)) - NVL(SHP_TODAY, 0)) AS D2_DEF " + "\n");
            strSqlString.Append("                                             , SUM(NVL(WEEK1_PLAN, 0)) AS WEEK_PLAN " + "\n");
            strSqlString.Append("                                             , SUM(NVL(SHP_WEEK, 0)) AS SHP_WEEK " + "\n");
            strSqlString.Append("                                             , SUM(NVL(WEEK1_PLAN, 0) - NVL(SHP_WEEK, 0)) AS WEEK_DEF " + "\n");
            strSqlString.Append("                                             , SUM(NVL(WEEK2_PLAN, 0)) AS WEEK2_PLAN " + "\n");
            strSqlString.Append("                                             , 0 AS SHP2_WEEK " + "\n");
            strSqlString.Append("                                             , SUM(NVL(WEEK2_PLAN, 0) + NVL(WEEK1_PLAN, 0) - NVL(SHP_WEEK, 0)) AS WEEK2_DEF " + "\n");
            strSqlString.Append("                                             , SUM(NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(MON.RESV_FIELD1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0) " + "\n");
            strSqlString.Append("                                                    ELSE MON.RESV_FIELD1 " + "\n");
            strSqlString.Append("                                                END,0)) MON_PLAN " + "\n");
            strSqlString.Append("                                             , SUM(NVL(SHP_MONTH, 0)) AS SHP_MONTH " + "\n");
            strSqlString.Append("                                             , SUM(NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(NVL(MON.RESV_FIELD1,0)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0)-NVL(SHP_MONTH, 0) " + "\n");
            strSqlString.Append("                                                    ELSE NVL(MON.RESV_FIELD1, 0)-NVL(SHP_MONTH, 0) " + "\n");
            strSqlString.Append("                                                END,0)) MONTH_DEF " + "\n");
            strSqlString.Append("                                          FROM MWIPMATDEF MAT " + "\n");
            strSqlString.Append("                                             , ( " + "\n");
            strSqlString.Append("                                                SELECT FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                                                     , SUM(CASE WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D0_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D0_QTY + D1_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D0_QTY + D1_QTY + D2_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D0_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D0_QTY + D1_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D0_QTY + D1_QTY + D2_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY " + "\n");
            strSqlString.Append("                                                                ELSE 0 " + "\n");
            strSqlString.Append("                                                           END) AS WEEK1_TTL " + "\n");
            strSqlString.Append("                                                     , SUM(W1_QTY) AS WEEK1_PLAN " + "\n");
            strSqlString.Append("                                                     , SUM(W2_QTY) AS WEEK2_PLAN " + "\n");
            strSqlString.Append("                                                     , SUM(NVL(W1_QTY,0) + NVL(W2_QTY,0)) AS WEEK_TTL " + "\n");
            strSqlString.Append("                                                     , SUM(CASE WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D0_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D1_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D2_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D3_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D4_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D5_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D6_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D0_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D1_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D2_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D3_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D4_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D5_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D6_QTY " + "\n");
            strSqlString.Append("                                                                ELSE 0 " + "\n");
            strSqlString.Append("                                                           END) AS D0_PLAN " + "\n");
            strSqlString.Append("                                                     , SUM(CASE WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D1_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D2_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D3_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D4_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D5_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D6_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D7_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D1_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D2_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D3_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D4_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D5_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D6_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D7_QTY " + "\n");
            strSqlString.Append("                                                                ELSE 0 " + "\n");
            strSqlString.Append("                                                           END) AS D1_PLAN " + "\n");
            strSqlString.Append("                                                     , SUM(CASE WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D2_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D3_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D4_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D5_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D6_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D7_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'OTD' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D8_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D2_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D3_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D4_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D5_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D6_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D7_QTY " + "\n");
            strSqlString.Append("                                                                WHEN CALENDAR_ID = 'SE' AND TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D8_QTY " + "\n");
            strSqlString.Append("                                                                ELSE 0 " + "\n");
            strSqlString.Append("                                                           END) AS D2_PLAN " + "\n");
            strSqlString.Append("                                                  FROM ( " + "\n");
            strSqlString.Append("                                                        SELECT FACTORY, MAT_ID, 'OTD' AS CALENDAR_ID " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D0_QTY, 0)) AS D0_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D1_QTY, 0)) AS D1_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D2_QTY, 0)) AS D2_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D3_QTY, 0)) AS D3_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D4_QTY, 0)) AS D4_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D5_QTY, 0)) AS D5_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D6_QTY, 0)) AS D6_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D0_QTY, 0)) AS D7_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D1_QTY, 0)) AS D8_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D2_QTY, 0)) AS D9_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D3_QTY, 0)) AS D10_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D4_QTY, 0)) AS D11_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D5_QTY, 0)) AS D12_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D6_QTY, 0)) AS D13_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', WW_QTY, 0)) AS W1_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', WW_QTY, 0)) AS W2_QTY  " + "\n");
            strSqlString.Append("                                                          FROM RWIPPLNWEK  " + "\n");
            strSqlString.Append("                                                         WHERE 1=1  " + "\n");
            strSqlString.Append("                                                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                                                           AND GUBUN = '3'  " + "\n");
            strSqlString.Append("                                                           AND MAT_ID NOT LIKE 'SES%' " + "\n");
            strSqlString.Append("                                                           AND PLAN_WEEK IN ('" + FindWeek_SOP_A.ThisWeek + "','" + FindWeek_SOP_A.NextWeek + "') " + "\n");
            strSqlString.Append("                                                         GROUP BY FACTORY, MAT_ID  " + "\n");
            strSqlString.Append("                                                         UNION ALL " + "\n");
            strSqlString.Append("                                                        SELECT FACTORY, MAT_ID, 'SE' AS CALENDAR_ID  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.ThisWeek + "', D0_QTY, 0)) AS D0_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.ThisWeek + "', D1_QTY, 0)) AS D1_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.ThisWeek + "', D2_QTY, 0)) AS D2_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.ThisWeek + "', D3_QTY, 0)) AS D3_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.ThisWeek + "', D4_QTY, 0)) AS D4_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.ThisWeek + "', D5_QTY, 0)) AS D5_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.ThisWeek + "', D6_QTY, 0)) AS D6_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.NextWeek + "', D0_QTY, 0)) AS D7_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.NextWeek + "', D1_QTY, 0)) AS D8_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.NextWeek + "', D2_QTY, 0)) AS D9_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.NextWeek + "', D3_QTY, 0)) AS D10_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.NextWeek + "', D4_QTY, 0)) AS D11_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.NextWeek + "', D5_QTY, 0)) AS D12_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.NextWeek + "', D6_QTY, 0)) AS D13_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.ThisWeek + "', WW_QTY, 0)) AS W1_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_SE.NextWeek + "', WW_QTY, 0)) AS W2_QTY  " + "\n");
            strSqlString.Append("                                                          FROM RWIPPLNWEK  " + "\n");
            strSqlString.Append("                                                         WHERE 1=1  " + "\n");
            strSqlString.Append("                                                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                                                           AND GUBUN = '3'  " + "\n");
            strSqlString.Append("                                                           AND MAT_ID LIKE 'SES%' " + "\n");
            strSqlString.Append("                                                           AND PLAN_WEEK IN ('" + FindWeek_SOP_SE.ThisWeek + "','" + FindWeek_SOP_SE.NextWeek + "') " + "\n");
            strSqlString.Append("                                                         GROUP BY FACTORY, MAT_ID  " + "\n");
            strSqlString.Append("                                                       )  " + "\n");
            strSqlString.Append("                                                 GROUP BY FACTORY, MAT_ID  " + "\n");
            strSqlString.Append("                                               ) PLN " + "\n");
            strSqlString.Append("                                             , (  " + "\n");
            strSqlString.Append("                                                SELECT FACTORY,MAT_ID, RESV_FIELD1  " + "\n");
            strSqlString.Append("                                                  FROM (  " + "\n");
            strSqlString.Append("                                                        SELECT FACTORY, MAT_ID, SUM(RESV_FIELD1) AS RESV_FIELD1   " + "\n");
            strSqlString.Append("                                                          FROM (  " + "\n");

            // 2020-09-29-임종우 : V2 로직 분리 - 월 계힉 DR 고객사는 26일 기준으로 차월 계획 가져옴 (김성업과장 요청)
            #region HMK (MON Plan)
            if (GlobalVariable.gsGlovalSite == "K1")
            {
                strSqlString.Append("                                                                SELECT FACTORY, MAT_ID, SUM(TO_NUMBER(DECODE(RESV_FIELD1,' ',0,RESV_FIELD1))) AS RESV_FIELD1  " + "\n");
                strSqlString.Append("                                                                  FROM CWIPPLNMON  " + "\n");
                strSqlString.Append("                                                                 WHERE 1=1  " + "\n");
                strSqlString.Append("                                                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
                strSqlString.Append("                                                                   AND PLAN_MONTH = '" + sMonth + "' " + "\n");
                strSqlString.Append("                                                                 GROUP BY FACTORY, MAT_ID " + "\n");
            }
            #endregion

            #region HMV2 (MON Plan)
            else
            {
                strSqlString.Append("                                                                SELECT FACTORY, MAT_ID, SUM(TO_NUMBER(DECODE(RESV_FIELD1,' ',0,RESV_FIELD1))) AS RESV_FIELD1  " + "\n");
                strSqlString.Append("                                                                  FROM CWIPPLNMON  " + "\n");
                strSqlString.Append("                                                                 WHERE 1=1  " + "\n");
                strSqlString.Append("                                                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
                strSqlString.Append("                                                                   AND PLAN_MONTH = '" + sMonth + "' " + "\n");
                //strSqlString.Append("                                                                   AND MAT_ID NOT LIKE 'DR%' " + "\n");
                strSqlString.Append("                                                                 GROUP BY FACTORY, MAT_ID " + "\n");
                //strSqlString.Append("                                                                 UNION ALL " + "\n");
                //strSqlString.Append("                                                                SELECT FACTORY, MAT_ID, SUM(TO_NUMBER(DECODE(RESV_FIELD1,' ',0,RESV_FIELD1))) AS RESV_FIELD1  " + "\n");
                //strSqlString.Append("                                                                  FROM CWIPPLNMON  " + "\n");
                //strSqlString.Append("                                                                 WHERE 1=1  " + "\n");
                //strSqlString.Append("                                                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
                //strSqlString.Append("                                                                   AND PLAN_MONTH = '" + sMonth_dr + "' " + "\n");
                //strSqlString.Append("                                                                   AND MAT_ID LIKE 'DR%' " + "\n");
                //strSqlString.Append("                                                                 GROUP BY FACTORY, MAT_ID " + "\n");
            }
            strSqlString.Append("                                                               )  " + "\n");
            strSqlString.Append("                                                         GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                                                       ) " + "\n");
            strSqlString.Append("                                               ) MON " + "\n");
            strSqlString.Append("                                             , (  " + "\n");
            #endregion

            // 2014-05-07-임종우 : 금일일자이면서 시간조회가 현재가 아니면 스냅샷 테이블 데이터 조회
            // 2014-06-27-임종우 : 어제일자도 스냅샷 테이블 데이터 조회 가능하도록 변경
            #region 금일, 어제 시간별 스냅샷 데이터
            //if ((DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd") || DateTime.Now.AddDays(-1).ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd")) && cboTimeBase.Text != "현재")
            if ((DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd") || DateTime.Now.AddDays(-1).ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd")) && cboTimeBase.SelectedIndex != 0)
            {
                strSqlString.Append("                                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                     , SUM(TODAY_SHP) AS SHP_TODAY " + "\n");
                strSqlString.Append("                                                     , SUM(WEEK_SHP) AS SHP_WEEK " + "\n");
                strSqlString.Append("                                                     , SUM(WEEK_Y_SHP) AS SHP_WEEK_TTL " + "\n");
                strSqlString.Append("                                                     , SUM(MON_SHP) AS SHP_MONTH " + "\n");
                strSqlString.Append("                                                  FROM RSHPTIMTMP " + "\n");
                strSqlString.Append("                                                 WHERE 1=1  " + "\n");
                strSqlString.Append("                                                   AND WORK_DATE = '" + Today + "'" + "\n");
                //strSqlString.Append("                                                   AND WORK_TIME = '" + cboTimeBase.Text.Replace("시", "") + "'" + "\n");
                strSqlString.Append("                                                   AND WORK_TIME = '" + cboTimeBase.Text.Substring(0, 2) + "'" + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                                                   AND LOT_TYPE LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                                                 GROUP BY MAT_ID " + "\n");
            }
            #endregion

            #region 기존 데이터
            else
            {
                //if (cdvTime.Text == "MIX")
                if (cdvTime.SelectedIndex == 0)
                {
                    // 2020-09-22-임종우 : V2 로직 분리 - 월 실적 DR 고객사는 전월 26일 ~ 금월 25일 기준으로 변경 (김성업과장 요청)
                    #region HMK (SHP)
                    if (GlobalVariable.gsGlovalSite == "K1")
                    {
                        strSqlString.Append("                                                SELECT MAT_ID  " + "\n");
                        strSqlString.Append("                                                     , SUM(CASE WHEN SHP_QTY_1 > 0 AND WORK_DATE = '" + Today + "' THEN NVL(SHP_QTY_1, 0) ELSE 0 END) AS SHP_TODAY  " + "\n");
                        strSqlString.Append("                                                     , SUM(CASE WHEN SHP_QTY_1 > 0 AND MAT_ID LIKE 'SES%' AND WORK_DATE BETWEEN '" + FindWeek_SOP_SE.StartDay_ThisWeek + "' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0)  " + "\n");
                        strSqlString.Append("                                                                WHEN SHP_QTY_1 > 0 AND MAT_ID NOT LIKE 'SES%' AND WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0) " + "\n");
                        strSqlString.Append("                                                                ELSE 0 END) AS SHP_WEEK  " + "\n");
                        strSqlString.Append("                                                     , SUM(CASE WHEN SHP_QTY_1 > 0 AND MAT_ID LIKE 'SES%' AND WORK_DATE BETWEEN '" + FindWeek_SOP_SE.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(SHP_QTY_1, 0)  " + "\n");
                        strSqlString.Append("                                                                WHEN SHP_QTY_1 > 0 AND MAT_ID NOT LIKE 'SES%' AND WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(SHP_QTY_1, 0)  " + "\n");
                        strSqlString.Append("                                                                ELSE 0 END) AS SHP_WEEK_TTL  " + "\n");
                        strSqlString.Append("                                                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + sMonth + "01' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_MONTH  " + "\n");
                        strSqlString.Append("                                                  FROM VSUMWIPOUT " + "\n");
                        strSqlString.Append("                                                 WHERE 1=1  " + "\n");
                        strSqlString.Append("                                                   AND FACTORY  = '" + cdvFactory.Text + "'  " + "\n");
                        strSqlString.Append("                                                   AND LOT_TYPE = 'W'  " + "\n");
                        strSqlString.Append("                                                   AND CM_KEY_2 = 'PROD'  " + "\n");

                        if (cdvLotType.Text != "ALL")
                        {
                            strSqlString.Append("                                                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                        }


                        strSqlString.Append("                                                   AND MAT_ID NOT LIKE 'HX%'  " + "\n");
                        strSqlString.Append("                                                   AND WORK_DATE BETWEEN LEAST('" + sMonth + "01', '" + FindWeek_SOP_A.StartDay_ThisWeek + "', '" + FindWeek_SOP_SE.StartDay_ThisWeek + "') AND '" + Today + "' " + "\n");
                        strSqlString.Append("                                                 GROUP BY MAT_ID  " + "\n");
                        strSqlString.Append("                                                 UNION ALL  " + "\n");
                        strSqlString.Append("                                                SELECT MAT_ID  " + "\n");
                        strSqlString.Append("                                                     , SUM(CASE WHEN SHP_QTY_1 > 0 AND WORK_DATE = '" + Today + "' THEN NVL(SHP_QTY_1, 0) ELSE 0 END) AS SHP_TODAY  " + "\n");
                        strSqlString.Append("                                                     , SUM(CASE WHEN SHP_QTY_1 > 0 AND WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0) ELSE 0 END) AS SHP_WEEK  " + "\n");
                        strSqlString.Append("                                                     , SUM(CASE WHEN SHP_QTY_1 > 0 AND WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(SHP_QTY_1, 0) ELSE 0 END) AS SHP_WEEK_TTL  " + "\n");
                        strSqlString.Append("                                                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + sMonth + "01' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_MONTH  " + "\n");
                        strSqlString.Append("                                                  FROM VSUMWIPOUT_06 " + "\n");
                        strSqlString.Append("                                                 WHERE 1=1  " + "\n");
                        strSqlString.Append("                                                   AND FACTORY  = '" + cdvFactory.Text + "'  " + "\n");
                        strSqlString.Append("                                                   AND LOT_TYPE = 'W'  " + "\n");
                        strSqlString.Append("                                                   AND CM_KEY_2 = 'PROD'  " + "\n");

                        if (cdvLotType.Text != "ALL")
                        {
                            strSqlString.Append("                                                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                        }

                        strSqlString.Append("                                                   AND MAT_ID LIKE 'HX%'  " + "\n");
                        strSqlString.Append("                                                   AND WORK_DATE BETWEEN '" + sStartDate + "' AND '" + Today + "' " + "\n");
                        strSqlString.Append("                                                 GROUP BY MAT_ID " + "\n");
                    }
                    #endregion

                    #region HMV2 (SHP)
                    else
                    {
                        strSqlString.Append("                                                SELECT MAT_ID  " + "\n");
                        strSqlString.Append("                                                     , SUM(CASE WHEN SHP_QTY_1 > 0 AND WORK_DATE = '" + Today + "' THEN NVL(SHP_QTY_1, 0) ELSE 0 END) AS SHP_TODAY  " + "\n");
                        strSqlString.Append("                                                     , SUM(CASE WHEN SHP_QTY_1 > 0 AND MAT_ID LIKE 'SES%' AND WORK_DATE BETWEEN '" + FindWeek_SOP_SE.StartDay_ThisWeek + "' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0)  " + "\n");
                        strSqlString.Append("                                                                WHEN SHP_QTY_1 > 0 AND MAT_ID NOT LIKE 'SES%' AND WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0) " + "\n");
                        strSqlString.Append("                                                                ELSE 0 END) AS SHP_WEEK  " + "\n");
                        strSqlString.Append("                                                     , SUM(CASE WHEN SHP_QTY_1 > 0 AND MAT_ID LIKE 'SES%' AND WORK_DATE BETWEEN '" + FindWeek_SOP_SE.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(SHP_QTY_1, 0)  " + "\n");
                        strSqlString.Append("                                                                WHEN SHP_QTY_1 > 0 AND MAT_ID NOT LIKE 'SES%' AND WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(SHP_QTY_1, 0)  " + "\n");
                        strSqlString.Append("                                                                ELSE 0 END) AS SHP_WEEK_TTL  " + "\n");
                        strSqlString.Append("                                                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + sMonth + "01' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_MONTH  " + "\n");
                        strSqlString.Append("                                                  FROM VSUMWIPOUT " + "\n");
                        strSqlString.Append("                                                 WHERE 1=1  " + "\n");
                        strSqlString.Append("                                                   AND FACTORY  = '" + cdvFactory.Text + "'  " + "\n");
                        strSqlString.Append("                                                   AND LOT_TYPE = 'W'  " + "\n");
                        strSqlString.Append("                                                   AND CM_KEY_2 = 'PROD'  " + "\n");

                        if (cdvLotType.Text != "ALL")
                        {
                            strSqlString.Append("                                                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                        }


                        //strSqlString.Append("                                                   AND MAT_ID NOT LIKE 'DR%'  " + "\n");
                        strSqlString.Append("                                                   AND WORK_DATE BETWEEN LEAST('" + sMonth + "01', '" + FindWeek_SOP_A.StartDay_ThisWeek + "', '" + FindWeek_SOP_SE.StartDay_ThisWeek + "') AND '" + Today + "' " + "\n");
                        strSqlString.Append("                                                 GROUP BY MAT_ID  " + "\n");
                        //strSqlString.Append("                                                 UNION ALL  " + "\n");
                        //strSqlString.Append("                                                SELECT MAT_ID  " + "\n");
                        //strSqlString.Append("                                                     , SUM(CASE WHEN SHP_QTY_1 > 0 AND WORK_DATE = '" + Today + "' THEN NVL(SHP_QTY_1, 0) ELSE 0 END) AS SHP_TODAY  " + "\n");
                        //strSqlString.Append("                                                     , SUM(CASE WHEN SHP_QTY_1 > 0 AND WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0) ELSE 0 END) AS SHP_WEEK  " + "\n");
                        //strSqlString.Append("                                                     , SUM(CASE WHEN SHP_QTY_1 > 0 AND WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(SHP_QTY_1, 0) ELSE 0 END) AS SHP_WEEK_TTL  " + "\n");
                        //strSqlString.Append("                                                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + sStartDate_dr + "' AND LEAST('" + Today + "','" + sEndDate_dr + "') THEN NVL(SHP_QTY_1, 0) END) AS SHP_MONTH  " + "\n");
                        //strSqlString.Append("                                                  FROM VSUMWIPOUT " + "\n");
                        //strSqlString.Append("                                                 WHERE 1=1  " + "\n");
                        //strSqlString.Append("                                                   AND FACTORY  = '" + cdvFactory.Text + "'  " + "\n");
                        //strSqlString.Append("                                                   AND LOT_TYPE = 'W'  " + "\n");
                        //strSqlString.Append("                                                   AND CM_KEY_2 = 'PROD'  " + "\n");

                        //if (cdvLotType.Text != "ALL")
                        //{
                        //    strSqlString.Append("                                                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                        //}

                        //strSqlString.Append("                                                   AND MAT_ID LIKE 'DR%'  " + "\n");
                        //strSqlString.Append("                                                   AND WORK_DATE BETWEEN LEAST('" + sStartDate_dr + "', '" + FindWeek_SOP_A.StartDay_ThisWeek + "') AND '" + Today + "' " + "\n");
                        //strSqlString.Append("                                                 GROUP BY MAT_ID " + "\n");
                    }
                    #endregion
                }
                else
                {
                    strSqlString.Append("                                                SELECT MAT_ID  " + "\n");
                    strSqlString.Append("                                                     , SUM(CASE WHEN SHP_QTY_1 > 0 AND WORK_DATE = '" + Today + "' THEN NVL(SHP_QTY_1, 0) ELSE 0 END) AS SHP_TODAY  " + "\n");
                    strSqlString.Append("                                                     , SUM(CASE WHEN SHP_QTY_1 > 0 AND MAT_ID LIKE 'SES%' AND WORK_DATE BETWEEN '" + FindWeek_SOP_SE.StartDay_ThisWeek + "' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0)  " + "\n");
                    strSqlString.Append("                                                                WHEN SHP_QTY_1 > 0 AND MAT_ID NOT LIKE 'SES%' AND WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0)  " + "\n");
                    strSqlString.Append("                                                                ELSE 0 END) AS SHP_WEEK  " + "\n");
                    strSqlString.Append("                                                     , SUM(CASE WHEN SHP_QTY_1 > 0 AND MAT_ID LIKE 'SES%' AND WORK_DATE BETWEEN '" + FindWeek_SOP_SE.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(SHP_QTY_1, 0)  " + "\n");
                    strSqlString.Append("                                                                WHEN SHP_QTY_1 > 0 AND MAT_ID NOT LIKE 'SES%' AND WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(SHP_QTY_1, 0) " + "\n");
                    strSqlString.Append("                                                                ELSE 0 END) AS SHP_WEEK_TTL " + "\n");
                    strSqlString.Append("                                                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + sMonth + "01' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_MONTH  " + "\n");
                    strSqlString.Append("                                                  FROM " + sTable_fac + "\n");
                    strSqlString.Append("                                                 WHERE 1=1  " + "\n");
                    strSqlString.Append("                                                   AND FACTORY  = '" + cdvFactory.Text + "'  " + "\n");
                    strSqlString.Append("                                                   AND CM_KEY_2 = 'PROD'  " + "\n");
                    strSqlString.Append("                                                   AND LOT_TYPE = 'W'  " + "\n");

                    if (cdvLotType.Text != "ALL")
                    {
                        strSqlString.Append("                                                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                    }
                                        
                    strSqlString.Append("                                                   AND WORK_DATE BETWEEN LEAST('" + sMonth + "01', '" + FindWeek_SOP_A.StartDay_ThisWeek + "', '" + FindWeek_SOP_SE.StartDay_ThisWeek + "') AND '" + Today + "' " + "\n");
                    strSqlString.Append("                                                 GROUP BY MAT_ID  " + "\n");
                }
            }
            #endregion

            strSqlString.Append("                                               ) SHP " + "\n");
            strSqlString.Append("                                         WHERE 1=1 " + "\n");
            strSqlString.Append("                                           AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                                           AND MAT.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                                           AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                                           AND MAT.MAT_ID = PLN.MAT_ID(+) " + "\n");
            strSqlString.Append("                                           AND MAT.MAT_ID = MON.MAT_ID(+) " + "\n");
            strSqlString.Append("                                           AND MAT.MAT_ID = SHP.MAT_ID(+) " + "\n");
            strSqlString.Append("                                         GROUP BY MAT.MAT_ID " + "\n");
            strSqlString.Append("                                         ORDER BY MAT.MAT_ID " + "\n");
            strSqlString.Append("                                       ) A " + "\n");
            strSqlString.Append("                                     , ( " + "\n");

            #region 금일, 어제 시간별 스냅샷 데이터
            //if ((DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd") || DateTime.Now.AddDays(-1).ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd")) && cboTimeBase.Text != "현재")
            if ((DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd") || DateTime.Now.AddDays(-1).ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd")) && cboTimeBase.SelectedIndex != 0)
            {
                strSqlString.Append("                                        SELECT MAT_ID " + "\n");
                strSqlString.Append("                                             , OPER_GRP AS OPER_GRP_1 " + "\n");
                strSqlString.Append("                                             , SUM(QTY) AS ASSY_END_QTY " + "\n");
                strSqlString.Append("                                          FROM RWIPTIMTMP " + "\n");
                strSqlString.Append("                                         WHERE 1=1  " + "\n");
                strSqlString.Append("                                           AND WORK_DATE = '" + Today + "'" + "\n");
                //strSqlString.Append("                                           AND WORK_TIME = '" + cboTimeBase.Text.Replace("시", "") + "'" + "\n");
                strSqlString.Append("                                           AND WORK_TIME = '" + cboTimeBase.Text.Substring(0, 2) + "'" + "\n");
                strSqlString.Append("                                           AND GUBUN = 'OUT' " + "\n");
                strSqlString.Append("                                           AND FUNCTION_NAME = 'PRD011015' " + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                                           AND LOT_TYPE LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                                         GROUP BY MAT_ID, OPER_GRP " + "\n");
            }
            #endregion

            #region 기존 데이터
            else
            {
                strSqlString.Append("                                        SELECT MAT_ID, OPER_GRP_1 " + "\n");
                strSqlString.Append("                                             , SUM(ASSY_END_QTY) AS ASSY_END_QTY " + "\n");
                strSqlString.Append("                                          FROM ( " + "\n");
                strSqlString.Append("                                                SELECT B.MAT_ID " + "\n");
                //strSqlString.Append("                                                                    WHEN OPER IN ('A0340', 'A0350', 'A0360', 'A0390', 'A0397', 'A0395') THEN 'SP' " + "\n");
                //1191214 이희석 F-CHIP 관점으로 인한 데이터 조건 추가
                if (cdvType.SelectedIndex == 3)
                {
                    strSqlString.Append("                                                     , CASE WHEN OPER IN ('A0000', 'A000N') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'HMK2A' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0040') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'BG' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0200', 'A0230') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'SAW' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0400', 'A0401') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA1' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0402') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA2' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0403') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA3' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0404') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA4' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0405') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA5' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0406') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA6' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0407') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA7' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0408') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA8' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0409') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA9' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0250') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'UV EXPOSE 2' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0333') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'CHIP ATTACH' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0337') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DEFLUX' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0340') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'PREBAKE 2' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0350') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'PLASMA 1' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0370') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'UNDERFILL' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0380') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'U/F CURE' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0381') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'FC Flipper' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0391') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'BTM SMT' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0800') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'QC GATE 3' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0910') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'H/S Attach' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0920') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'HS/A CURE' " + "\n");
                }
                else if (cdvType.SelectedIndex == 4)
                {
                    strSqlString.Append("                                                     , CASE WHEN OPER IN ('A0000','A000N') THEN 'DIE_BANK'  " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0005','A0010','A0012','A0013','A0015','A0020') THEN 'LN'  " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0023','A0025','A0030') THEN 'PRE_BG'  " + "\n");
                    strSqlString.Append("                                                            WHEN OPER ='A0033' THEN 'STEALTH'  " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0040','A0041','A0042','A0045') THEN 'BG'  " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0050','A0055') THEN 'DETACH'  " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0060','A0065') THEN 'BSCOATING' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER ='A0070' THEN 'COATINGTCURE' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER ='A0080' THEN 'BS_MARKING' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0090','A0095','A0100') THEN 'TAPE_MOUNT' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0110','A0120','A0130','A0140','A0150','A0160','A0161','A0165','A0170') THEN 'LG' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0175','A0176','A0180','A0190','A0200','A0201','A0202','A0210','A0215','A0220','A0300') THEN 'SAWING' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0230','A0240') THEN 'WE'  " + "\n");
                    strSqlString.Append("                                                            WHEN OPER = 'A0250' THEN 'UV' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0270','A0290','A0295') THEN 'COB_AVI' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER BETWEEN 'A0290' AND 'AZ010' THEN 'EXTRA'" + "\n");
                }
                else if (cdvType.SelectedIndex == 5)
                {
                    strSqlString.Append("                                                     , CASE WHEN OPER IN ('A0000','A000N') THEN 'DIE_BANK'  " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0005','A0010','A0012','A0013','A0015','A0020') THEN 'LN'  " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0023','A0025','A0030') THEN 'PRE_BG'  " + "\n");
                    strSqlString.Append("                                                            WHEN OPER ='A0033' THEN 'STEALTH'  " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0040','A0041','A0042','A0045') THEN 'BG'  " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0050','A0055','A0060','A0065','A0070','A0080','A0090','A0095','A0100','A0110','A0120','A0130','A0140','A0150','A0160','A0161','A0165','A0170') THEN 'LG' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0175','A0176','A0180','A0190','A0200','A0201','A0202','A0210','A0215','A0220','A0300') THEN 'SAWING' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0230','A0240') THEN 'WE'  " + "\n");
                    strSqlString.Append("                                                            WHEN OPER = 'A0250' THEN 'UV' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0270','A0290','A0295') THEN 'COB_AVI' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER BETWEEN 'A0290' AND 'AZ010' THEN 'EXTRA'" + "\n");
                }
                else if (cdvType.SelectedIndex == 6)
                {
                    strSqlString.Append("                                                     , CASE WHEN OPER IN ('A0000','A000N') THEN 'DIE_BANK'  " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0005','A0010','A0012','A0013','A0015','A0020') THEN 'LN'  " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0023','A0025','A0030','A0033','A0040','A0041','A0042','A0045') THEN 'BG'  " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0050','A0055') THEN 'DETACH'  " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0060','A0065') THEN 'BSCOATING' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER ='A0070' THEN 'COATINGTCURE' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER ='A0080' THEN 'BS_MARKING' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0090','A0095','A0100') THEN 'TAPE_MOUNT' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0110','A0120','A0130','A0140','A0150','A0160','A0161','A0165','A0170','A0175','A0176','A0180','A0190','A0200','A0201','A0202','A0210','A0215','A0220','A0230','A0240','A0300') THEN 'SAWING' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER = 'A0250' THEN 'UV' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0270','A0290','A0295') THEN 'COB_AVI' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER BETWEEN 'A0290' AND 'AZ010' THEN 'EXTRA'" + "\n");
                }
                else
                {
                    strSqlString.Append("                                                     , CASE WHEN OPER IN ('A0000', 'A000N') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'HMK2A' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0040') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'BG' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0200', 'A0230') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'SAW' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0400', 'A0401', 'A0333') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA1' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0402') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA2' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0403') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA3' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0404') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA4' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0405') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA5' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0406') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA6' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0407') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA7' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0408') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA8' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A0409') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA9' " + "\n");
                }
                

                strSqlString.Append("                                                            WHEN OPER IN ('A0600','A0601') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'WB1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('A0602') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'WB2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('A0603') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'WB3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('A0604') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'WB4' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('A0605') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'WB5' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('A0606') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'WB6' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('A0607') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'WB7' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('A0608') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'WB8' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('A0609') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'WB9' " + "\n");
                //1191214 이희석 F-CHIP 관점으로 인한 데이터 조건 추가
                if (cdvType.SelectedIndex == 3)
                {
                    strSqlString.Append("                                                            WHEN OPER IN ('A0801', 'A0802', 'A0803', 'A0804', 'A0805', 'A0806', 'A0807', 'A0808', 'A0809') AND MAT_GRP_5 IN ('-', 'Merge') THEN 'GATE' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A1000') THEN 'MOLD' " + "\n");
                }
                else
                {
                    strSqlString.Append("                                                            WHEN OPER IN ('A0800', 'A0801', 'A0802', 'A0803', 'A0804', 'A0805', 'A0806', 'A0807', 'A0808', 'A0809') AND MAT_GRP_5 IN ('-', 'Merge') THEN 'GATE' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A1000', 'A0910') THEN 'MOLD' " + "\n");
                }
                strSqlString.Append("                                                            WHEN OPER IN ('A1100') THEN 'CURE' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('A1200') THEN 'TRIM' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('A1450') THEN 'TIN' " + "\n");
                if (cdvType.SelectedIndex == 7)
                {
                    strSqlString.Append("                                                            WHEN OPER IN ('A1790') THEN 'DC' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A1500') THEN 'MK2' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A1240') THEN 'FLIPPER' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A1300') THEN 'SBA' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A1370') THEN 'AOI' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A1380') THEN 'PP' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A1150') THEN 'MK' " + "\n");
                }
                else
                {
                    strSqlString.Append("                                                            WHEN OPER IN ('A1150', 'A1500') THEN 'MK' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER IN ('A1300') THEN 'SBA' " + "\n");
                }
                
                strSqlString.Append("                                                            WHEN OPER IN ('A1750', 'A1800', 'A1900', 'A1825') THEN 'SIG' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('A2000') THEN 'AVI' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('A2050') THEN 'PVI' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('A2100') THEN 'QC_GATE' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('AZ010') THEN 'HMK3A' " + "\n");
                strSqlString.Append("                                                            ELSE ' ' " + "\n");
                strSqlString.Append("                                                        END OPER_GRP_1 " + "\n");
                strSqlString.Append("                                                     , (CASE WHEN OPER IN ('AZ010','SHIP','TZ010','F0000','EZ010', 'SZ010') THEN SHIP_QTY_1 ELSE END_QTY_1 END) ASSY_END_QTY " + "\n");
                strSqlString.Append("                                                  FROM ( " + "\n");
                strSqlString.Append("                                                        SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                strSqlString.Append("                                                             , SUM(END_LOT) AS END_LOT " + "\n");
                strSqlString.Append("                                                             , SUM(END_QTY_1) AS END_QTY_1 " + "\n");
                strSqlString.Append("                                                             , SUM(END_QTY_2) AS END_QTY_2 " + "\n");
                strSqlString.Append("                                                             , SUM(CASE WHEN SHIP_QTY_1 > 0 THEN SHIP_QTY_1 ELSE 0 END) AS SHIP_QTY_1 " + "\n");
                strSqlString.Append("                                                             , SUM(CASE WHEN SHIP_QTY_2 > 0 THEN SHIP_QTY_2 ELSE 0 END) AS SHIP_QTY_2 " + "\n");
                strSqlString.Append("                                                          FROM ( " + "\n");

                //if (cdvTime.Text == "MIX")
                if (cdvTime.SelectedIndex == 0)
                {
                    strSqlString.Append("                                                                SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                    strSqlString.Append("                                                                     , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_LOT+S2_OPER_IN_LOT+S3_OPER_IN_LOT),SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT)) END_LOT " + "\n");
                    strSqlString.Append("                                                                     , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_1+S2_OPER_IN_QTY_1+S3_OPER_IN_QTY_1),SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1)) END_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_2+S2_OPER_IN_QTY_2+S3_OPER_IN_QTY_2),SUM(S1_END_QTY_2+S2_END_QTY_2+S3_END_QTY_2+S1_END_RWK_QTY_2 + S2_END_RWK_QTY_2 + S3_END_RWK_QTY_2)) END_QTY_2 " + "\n");
                    strSqlString.Append("                                                                     , 0 SHIP_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , 0 SHIP_QTY_2 " + "\n");
                    strSqlString.Append("                                                                  FROM RSUMWIPMOV " + "\n");
                    strSqlString.Append("                                                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                                                                   AND OPER NOT IN ('AZ010') " + "\n");
                    strSqlString.Append("                                                                   AND MAT_ID NOT LIKE 'HX%' " + "\n");
                    strSqlString.Append("                                                                 GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                    strSqlString.Append("                                                                 UNION ALL " + "\n");
                    strSqlString.Append("                                                                SELECT FACTORY, MAT_ID " + "\n");
                    strSqlString.Append("                                                                     , 'AZ010' AS OPER " + "\n");
                    strSqlString.Append("                                                                     , LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                    strSqlString.Append("                                                                     , 0 END_LOT " + "\n");
                    strSqlString.Append("                                                                     , 0 END_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , 0 END_QTY_2 " + "\n");
                    strSqlString.Append("                                                                     , SUM(SHP_QTY_1) SHIP_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , SUM(SHP_QTY_2) SHIP_QTY_2 " + "\n");
                    strSqlString.Append("                                                                  FROM VSUMWIPOUT " + "\n");
                    strSqlString.Append("                                                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                                                                   AND MAT_ID NOT LIKE 'HX%' " + "\n");
                    strSqlString.Append("                                                                 GROUP BY FACTORY, MAT_ID, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                    strSqlString.Append("                                                                 UNION ALL " + "\n");
                    strSqlString.Append("                                                                SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                    strSqlString.Append("                                                                     , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_LOT+S2_OPER_IN_LOT+S3_OPER_IN_LOT),SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT)) END_LOT " + "\n");
                    strSqlString.Append("                                                                     , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_1+S2_OPER_IN_QTY_1+S3_OPER_IN_QTY_1),SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1)) END_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_2+S2_OPER_IN_QTY_2+S3_OPER_IN_QTY_2),SUM(S1_END_QTY_2+S2_END_QTY_2+S3_END_QTY_2+S1_END_RWK_QTY_2 + S2_END_RWK_QTY_2 + S3_END_RWK_QTY_2)) END_QTY_2 " + "\n");
                    strSqlString.Append("                                                                     , 0 SHIP_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , 0 SHIP_QTY_2 " + "\n");
                    strSqlString.Append("                                                                  FROM CSUMWIPMOV " + "\n");
                    strSqlString.Append("                                                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                                                                   AND OPER NOT IN ('AZ010') " + "\n");
                    strSqlString.Append("                                                                   AND MAT_ID LIKE 'HX%' " + "\n");
                    strSqlString.Append("                                                                 GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                    strSqlString.Append("                                                                 UNION ALL " + "\n");
                    strSqlString.Append("                                                                SELECT FACTORY, MAT_ID " + "\n");
                    strSqlString.Append("                                                                     , 'AZ010' AS OPER " + "\n");
                    strSqlString.Append("                                                                     , LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                    strSqlString.Append("                                                                     , 0 END_LOT " + "\n");
                    strSqlString.Append("                                                                     , 0 END_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , 0 END_QTY_2 " + "\n");
                    strSqlString.Append("                                                                     , SUM(SHP_QTY_1) SHIP_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , SUM(SHP_QTY_2) SHIP_QTY_2 " + "\n");
                    strSqlString.Append("                                                                  FROM VSUMWIPOUT_06 " + "\n");
                    strSqlString.Append("                                                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                                                                   AND MAT_ID LIKE 'HX%' " + "\n");
                    strSqlString.Append("                                                                 GROUP BY FACTORY, MAT_ID, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                }
                else
                {
                    strSqlString.Append("                                                                SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                    strSqlString.Append("                                                                     , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_LOT+S2_OPER_IN_LOT+S3_OPER_IN_LOT),SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT)) END_LOT " + "\n");
                    strSqlString.Append("                                                                     , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_1+S2_OPER_IN_QTY_1+S3_OPER_IN_QTY_1),SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1)) END_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_2+S2_OPER_IN_QTY_2+S3_OPER_IN_QTY_2),SUM(S1_END_QTY_2+S2_END_QTY_2+S3_END_QTY_2+S1_END_RWK_QTY_2 + S2_END_RWK_QTY_2 + S3_END_RWK_QTY_2)) END_QTY_2 " + "\n");
                    strSqlString.Append("                                                                     , 0 SHIP_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , 0 SHIP_QTY_2 " + "\n");
                    strSqlString.Append("                                                                  FROM " + sTable_wip + "\n");
                    strSqlString.Append("                                                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                                                                   AND OPER NOT IN ('AZ010') " + "\n");
                    strSqlString.Append("                                                                 GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                    strSqlString.Append("                                                                 UNION ALL " + "\n");
                    strSqlString.Append("                                                                SELECT FACTORY, MAT_ID " + "\n");
                    strSqlString.Append("                                                                     , 'AZ010' AS  OPER " + "\n");
                    strSqlString.Append("                                                                     , LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                    strSqlString.Append("                                                                     , 0 END_LOT " + "\n");
                    strSqlString.Append("                                                                     , 0 END_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , 0 END_QTY_2 " + "\n");
                    strSqlString.Append("                                                                     , SUM(SHP_QTY_1) SHIP_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , SUM(SHP_QTY_2) SHIP_QTY_2 " + "\n");
                    strSqlString.Append("                                                                  FROM " + sTable_fac + "\n");
                    strSqlString.Append("                                                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                                                                 GROUP BY FACTORY, MAT_ID, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                }
                strSqlString.Append("                                                               ) " + "\n");
                strSqlString.Append("                                                         GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                strSqlString.Append("                                                       ) A " + "\n");
                strSqlString.Append("                                                     , MWIPMATDEF B " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                   AND A.FACTORY  = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                                   AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                                                   AND A.MAT_ID = B.MAT_ID " + "\n");                
                strSqlString.Append("                                                   AND B.MAT_TYPE = 'FG' " + "\n");
                strSqlString.Append("                                                   AND A.MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                                                   AND A.CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }
                                
                strSqlString.Append("                                                   AND A.OPER NOT IN ('00001','00002') " + "\n");
                strSqlString.Append("                                                   AND A.WORK_DATE = '" + Today + "' " + "\n");
                strSqlString.Append("                                                 ) " + "\n");
                strSqlString.Append("                                         GROUP BY MAT_ID, OPER_GRP_1 " + "\n");
                strSqlString.Append("                                        HAVING SUM(ASSY_END_QTY) > 0 " + "\n");
            }
            #endregion

            strSqlString.Append("                                       ) B " + "\n");
            strSqlString.Append("                                     , ( " + "\n");

            #region 금일, 어제 시간별 스냅샷 데이터
            //if ((DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd") || DateTime.Now.AddDays(-1).ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd")) && cboTimeBase.Text != "현재")
            if ((DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd") || DateTime.Now.AddDays(-1).ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd")) && cboTimeBase.SelectedIndex != 0)
            {
                strSqlString.Append("                                        SELECT MAT_ID " + "\n");
                strSqlString.Append("                                             , OPER_GRP AS OPER_GRP_1 " + "\n");
                strSqlString.Append("                                             , SUM(QTY) AS QTY " + "\n");
                strSqlString.Append("                                          FROM RWIPTIMTMP " + "\n");
                strSqlString.Append("                                         WHERE 1=1  " + "\n");
                strSqlString.Append("                                           AND WORK_DATE = '" + Today + "'" + "\n");
                //strSqlString.Append("                                           AND WORK_TIME = '" + cboTimeBase.Text.Replace("시", "") + "'" + "\n");
                strSqlString.Append("                                           AND WORK_TIME = '" + cboTimeBase.Text.Substring(0, 2) + "'" + "\n");
                strSqlString.Append("                                           AND GUBUN = 'WIP' " + "\n");
                strSqlString.Append("                                           AND FUNCTION_NAME = 'PRD011015' " + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                                           AND LOT_TYPE LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                                         GROUP BY MAT_ID, OPER_GRP " + "\n");
            }
            #endregion

            #region 기존 데이터
            else
            {
                strSqlString.Append("                                        SELECT MAT_ID, OPER_GRP_1, SUM(NVL(QTY,0)) QTY " + "\n");
                strSqlString.Append("                                          FROM (  " + "\n");
                strSqlString.Append("                                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                     , QTY " + "\n");
                strSqlString.Append("                                                     , CASE WHEN OPER_GRP_8 = 'HMK2A' THEN 'HMK2A' " + "\n");
                strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'B/G' THEN 'BG' " + "\n");
                strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'SAW' THEN 'SAW' " + "\n");
                strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'S/P' THEN 'SP' " + "\n");
                //1191214 이희석 F-CHIP 관점으로 인한 데이터 조건 추가
                if (cdvType.SelectedIndex == 3)
                {
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'UV EXPOSE 2' THEN 'UV EXPOSE 2' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'CHIP ATTACH' THEN 'CHIP ATTACH' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'DEFLUX' THEN 'DEFLUX' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'PREBAKE 2' THEN 'PREBAKE 2' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'PLASMA 1' THEN 'PLASMA 1' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'UNDERFILL' THEN 'UNDERFILL' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'U/F CURE' THEN 'U/F CURE' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'FC Flipper' THEN 'FC Flipper' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'BTM SMT' THEN 'BTM SMT' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'QC GATE 3' THEN 'QC GATE 3' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'H/S Attach' THEN 'H/S Attach' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'HS/A CURE' THEN 'HS/A CURE' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'HS/A CURE' THEN 'HS/A CURE' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'HS/A CURE' THEN 'HS/A CURE' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'HS/A CURE' THEN 'HS/A CURE' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'HS/A CURE' THEN 'HS/A CURE' " + "\n");

                }
                else if (cdvType.SelectedIndex == 4)
                {
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'DIE_BANK' THEN 'DIE_BANK' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'LN' THEN 'LN' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'PRE_BG' THEN 'PRE_BG' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'STEALTH' THEN 'STEALTH' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'BG' THEN 'BG' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'DETACH' THEN 'DETACH' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'BSCOATING' THEN 'BSCOATING' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'COATINGTCURE' THEN 'COATINGTCURE' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'BS_MARKING' THEN 'BS_MARKING' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'TAPE_MOUNT' THEN 'TAPE_MOUNT' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'LG' THEN 'LG' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'SAWING' THEN 'SAWING' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'WE' THEN 'WE' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'UV' THEN 'UV' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'COB_AVI' THEN 'COB_AVI' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'EXTRA' THEN 'EXTRA' " + "\n");

                }
                else if (cdvType.SelectedIndex == 5)
                {
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'DIE_BANK' THEN 'DIE_BANK' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'LN' THEN 'LN' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'PRE_BG' THEN 'PRE_BG' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'STEALTH' THEN 'STEALTH' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'BG' THEN 'BG' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'LG' THEN 'LG' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'SAWING' THEN 'SAWING' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'WE' THEN 'WE' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'UV' THEN 'UV' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'COB_AVI' THEN 'COB_AVI' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'EXTRA' THEN 'EXTRA' " + "\n");
                }
                else if (cdvType.SelectedIndex == 6)
                {
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'DIE_BANK' THEN 'DIE_BANK' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'LN' THEN 'LN' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'BG' THEN 'BG' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'DETACH' THEN 'DETACH' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'BSCOATING' THEN 'BSCOATING' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'COATINGTCURE' THEN 'COATINGTCURE' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'BS_MARKING' THEN 'BS_MARKING' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'TAPE_MOUNT' THEN 'TAPE_MOUNT' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'SAWING' THEN 'SAWING' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'UV' THEN 'UV' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'COB_AVI' THEN 'COB_AVI' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'EXTRA' THEN 'EXTRA' " + "\n");
                }

                    // 2014-09-16-임종우 : Cure 공정은 선택에 해당 공정에 포함 되도록 변경
                    if (cdvCure.Text == "DA")
                    {
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 IN ('D/A1', 'D/A1 CURE') THEN 'DA1' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 IN ('D/A2', 'D/A2 CURE') THEN 'DA2' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0801' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA2' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0801' AND MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA2' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 IN ('D/A3', 'D/A3 CURE') THEN 'DA3' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0802' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA3' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0802' AND MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA3' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 IN ('D/A4', 'D/A4 CURE') THEN 'DA4' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0803' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA4' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0803' AND MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA4' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 IN ('D/A5', 'D/A5 CURE') THEN 'DA5' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0804' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA5' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0804' AND MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA5' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 IN ('D/A6', 'D/A6 CURE') THEN 'DA6' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0805' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA6' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0805' AND MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA6' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 IN ('D/A7', 'D/A7 CURE') THEN 'DA7' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0806' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA7' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0806' AND MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA7' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 IN ('D/A8', 'D/A8 CURE') THEN 'DA8' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0807' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA8' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0807' AND MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA8' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 IN ('D/A9', 'D/A9 CURE') THEN 'DA9' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0808' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA9' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0808' AND MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA9' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'W/B1' THEN 'WB1' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'W/B2' THEN 'WB2' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'W/B3' THEN 'WB3' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'W/B4' THEN 'WB4' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'W/B5' THEN 'WB5' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'W/B6' THEN 'WB6' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'W/B7' THEN 'WB7' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'W/B8' THEN 'WB8' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'W/B9' THEN 'WB9' " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'D/A1' THEN 'DA1' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'D/A2' THEN 'DA2' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0801' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA2' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0801' AND MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA2' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'D/A3' THEN 'DA3' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0802' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA3' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0802' AND MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA3' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'D/A4' THEN 'DA4' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0803' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA4' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0803' AND MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA4' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'D/A5' THEN 'DA5' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0804' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA5' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0804' AND MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA5' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'D/A6' THEN 'DA6' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0805' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA6' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0805' AND MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA6' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'D/A7' THEN 'DA7' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0806' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA7' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0806' AND MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA7' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'D/A8' THEN 'DA8' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0807' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA8' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0807' AND MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA8' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'D/A9' THEN 'DA9' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0808' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA9' " + "\n");
                        //strSqlString.Append("                                                            WHEN OPER = 'A0808' AND MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA9' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 IN ('W/B1', 'D/A1 CURE') THEN 'WB1' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 IN ('W/B2', 'D/A2 CURE') THEN 'WB2' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 IN ('W/B3', 'D/A3 CURE') THEN 'WB3' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 IN ('W/B4', 'D/A4 CURE') THEN 'WB4' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 IN ('W/B5', 'D/A5 CURE') THEN 'WB5' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 IN ('W/B6', 'D/A6 CURE') THEN 'WB6' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 IN ('W/B7', 'D/A7 CURE') THEN 'WB7' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 IN ('W/B8', 'D/A8 CURE') THEN 'WB8' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 IN ('W/B9', 'D/A9 CURE') THEN 'WB9' " + "\n");
                    }

                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'GATE' AND MAT_GRP_5 = '-' THEN 'GATE' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'GATE' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 = 'Merge' THEN 'GATE' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'GATE' AND MAT_GRP_1 = 'HX' THEN 'GATE' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'GATE' AND MAT_GRP_1 NOT IN ('SE','HX') AND SUBSTR(MAT_GRP_4,-1) = SUBSTR(OPER, -1) THEN 'GATE' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'MOLD' THEN 'MOLD' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'CURE' THEN 'CURE' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'M/K' THEN 'MK' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'TRIM' THEN 'TRIM' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'TIN' THEN 'TIN' " + "\n");
                    if (cdvType.SelectedIndex == 7)
                    {
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'M/K2' THEN 'MK2' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'SBA' THEN 'SBA' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'DC' THEN 'DC' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'AOI' THEN 'AOI' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'PP' THEN 'PP' " + "\n");
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'FLIPPER' THEN 'FLIPPER' " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'S/B/A' THEN 'SBA' " + "\n");
                    }
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'SIG' THEN 'SIG' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'AVI' THEN 'AVI' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'V/I' THEN 'PVI' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'QC GATE' THEN 'QC_GATE' " + "\n");
                    strSqlString.Append("                                                            WHEN OPER_GRP_8 = 'HMK3A' THEN 'HMK3A' " + "\n");



                    strSqlString.Append("                                                            ELSE ' ' " + "\n");
                

                strSqlString.Append("                                                        END OPER_GRP_1 " + "\n");
                strSqlString.Append("                                                  FROM ( " + "\n");
                strSqlString.Append("                                                        SELECT MAT_ID, OPER, OPER_GRP_8, MAT_GRP_1, MAT_GRP_4, MAT_GRP_5, MAT_GRP_10 " + "\n");
                //strSqlString.Append("                                                             , CASE WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1) ELSE QTY_1 END AS QTY " + "\n");
                // 2014-09-02-임종우 : HX 제품 재공 로직 변경 A0015, A0395 타는 제품의 경우 해당 공정까지 COMP 로직
                strSqlString.Append("                                                             , CASE WHEN MAT_GRP_1 = 'HX' AND HX_COMP_MIN IS NOT NULL" + "\n");
                strSqlString.Append("                                                                         THEN (CASE WHEN HX_COMP_MIN <> HX_COMP_MAX AND OPER > HX_COMP_MIN AND OPER <= HX_COMP_MAX THEN QTY_1 / NVL(COMP_CNT / 2,1)" + "\n");
                strSqlString.Append("                                                                         WHEN OPER <= HX_COMP_MAX THEN QTY_1 / NVL(COMP_CNT,1)" + "\n");
                strSqlString.Append("                                                                         ELSE QTY_1 END)" + "\n");
                strSqlString.Append("                                                                    WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1) " + "\n");
                strSqlString.Append("                                                                    ELSE QTY_1 " + "\n");
                strSqlString.Append("                                                               END QTY " + "\n");
                strSqlString.Append("                                                          FROM ( " + "\n");
                strSqlString.Append("                                                                SELECT A.MAT_ID, B.OPER, " + "\n");
                //1191214 이희석 F-CHIP 관점으로 인한 데이터 조건 추가
                if (cdvType.SelectedIndex == 3)
                {
                    strSqlString.Append("                                                                CASE WHEN B.OPER ='A0250' THEN 'UV EXPOSE 2' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER ='A0333' THEN 'CHIP ATTACH' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER ='A0337' THEN 'DEFLUX' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER ='A0340' THEN 'PREBAKE 2' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER ='A0350' THEN 'PLASMA 1' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER ='A0370' THEN 'UNDERFILL' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER ='A0380' THEN 'U/F CURE' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER ='A0381' THEN 'FC Flipper' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER ='A0391' THEN 'BTM SMT' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER ='A0800' THEN 'QC GATE 3' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER ='A0910' THEN 'H/S Attach' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER ='A0920' THEN 'HS/A CURE' " + "\n");
                    strSqlString.Append("                                                                     ELSE B.OPER_GRP_8 END AS OPER_GRP_8, C.MAT_GRP_1, C.MAT_GRP_4, C.MAT_GRP_5, C.MAT_GRP_10 " + "\n");
                }
                else if (cdvType.SelectedIndex == 4)
                {
                    strSqlString.Append("                                                                CASE WHEN B.OPER ='A0000' THEN 'DIE_BANK'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0005','A0010','A0012','A0013','A0015','A0020') THEN 'LN' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0023','A0025','A0030') THEN 'PRE_BG' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER ='A0033' THEN 'STEALTH'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0040','A0041','A0042','A0045') THEN 'BG'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0050','A0055') THEN 'DETACH' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0060','A0065') THEN 'BSCOATING' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER ='A0070' THEN 'COATINGTCURE'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER ='A0080' THEN 'BS_MARKING' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0090','A0095','A0100') THEN 'TAPE_MOUNT'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0110','A0120','A0130','A0140','A0150','A0160','A0161','A0165','A0170') THEN 'LG'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0175','A0176','A0180','A0190','A0200','A0201','A0202','A0210','A0215','A0220','A0300') THEN 'SAWING'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER ='A0080' THEN 'BS_MARKING' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0230','A0240') THEN 'WE'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER = 'A0250' THEN 'UV'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0270','A0290','A0295') THEN 'COB_AVI'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER BETWEEN 'A0290' AND 'AZ010' THEN 'EXTRA'  " + "\n");
                    strSqlString.Append("                                                                     ELSE B.OPER_GRP_8 END AS OPER_GRP_8, C.MAT_GRP_1, C.MAT_GRP_4, C.MAT_GRP_5, C.MAT_GRP_10 " + "\n");
                }
                else if (cdvType.SelectedIndex == 5)
                {
                    strSqlString.Append("                                                                CASE WHEN B.OPER ='A0000' THEN 'DIE_BANK'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0005','A0010','A0012','A0013','A0015','A0020') THEN 'LN' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0023','A0025','A0030') THEN 'PRE_BG' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER ='A0033' THEN 'STEALTH'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0040','A0041','A0042','A0045') THEN 'BG'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0050','A0055','A0060','A0065','A0070','A0080','A0090','A0095','A0100','A0110','A0120','A0130','A0140','A0150','A0160','A0161','A0165','A0170') THEN 'LG'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0175','A0176','A0180','A0190','A0200','A0201','A0202','A0210','A0215','A0220','A0300') THEN 'SAWING'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER ='A0080' THEN 'BS_MARKING' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0230','A0240') THEN 'WE'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER = 'A0250' THEN 'UV'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0270','A0290','A0295') THEN 'COB_AVI'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER BETWEEN 'A0290' AND 'AZ010' THEN 'EXTRA'  " + "\n");
                    strSqlString.Append("                                                                     ELSE B.OPER_GRP_8 END AS OPER_GRP_8, C.MAT_GRP_1, C.MAT_GRP_4, C.MAT_GRP_5, C.MAT_GRP_10 " + "\n");
                }
                else if (cdvType.SelectedIndex == 6)
                {
                    strSqlString.Append("                                                                CASE WHEN B.OPER ='A0000' THEN 'DIE_BANK'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0005','A0010','A0012','A0013','A0015','A0020') THEN 'LN' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0023','A0025','A0030','A0033','A0040','A0041','A0042','A0045') THEN 'BG'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0050','A0055') THEN 'DETACH' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0060','A0065') THEN 'BSCOATING' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER ='A0070' THEN 'COATINGTCURE'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER ='A0080' THEN 'BS_MARKING' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0090','A0095','A0100') THEN 'TAPE_MOUNT'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0110','A0120','A0130','A0140','A0150','A0160','A0161','A0165','A0170','A0175','A0176','A0180','A0190','A0200','A0201','A0202','A0210','A0215','A0220','A0230','A0240','A0300') THEN 'SAWING'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER ='A0080' THEN 'BS_MARKING' " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER = 'A0250' THEN 'UV'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER IN ('A0270','A0290','A0295') THEN 'COB_AVI'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER BETWEEN 'A0290' AND 'AZ010' THEN 'EXTRA'  " + "\n");
                    strSqlString.Append("                                                                     ELSE B.OPER_GRP_8 END AS OPER_GRP_8, C.MAT_GRP_1, C.MAT_GRP_4, C.MAT_GRP_5, C.MAT_GRP_10 " + "\n");
                }
                else if (cdvType.SelectedIndex == 7)
                {
                    strSqlString.Append("                                                                CASE WHEN B.OPER ='A1240' THEN 'FLIPPER'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER = 'A1790' THEN 'DC'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER = 'A1300' THEN 'SBA'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER = 'A1370' THEN 'AOI'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER = 'A1380' THEN 'PP'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER = 'A1150' THEN 'M/K'  " + "\n");
                    strSqlString.Append("                                                                     WHEN B.OPER = 'A1500' THEN 'M/K2'  " + "\n");
                    strSqlString.Append("                                                                     ELSE B.OPER_GRP_8 END AS OPER_GRP_8, C.MAT_GRP_1, C.MAT_GRP_4, C.MAT_GRP_5, C.MAT_GRP_10 " + "\n");
                }
                else
                {
                    strSqlString.Append("                                                                B.OPER_GRP_8, C.MAT_GRP_1, C.MAT_GRP_4, C.MAT_GRP_5, C.MAT_GRP_10 " + "\n");
                }
                strSqlString.Append("                                                                     , CASE WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN QTY_1 ELSE 0 END " + "\n");
                strSqlString.Append("                                                                            WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(QTY_1/TO_NUMBER(DECODE(MAT_CMF_13, ' ', 1, '-', 1, MAT_CMF_13)),0) " + "\n");
                strSqlString.Append("                                                                            ELSE QTY_1 " + "\n");
                strSqlString.Append("                                                                       END AS QTY_1 " + "\n");
                strSqlString.Append("                                                                     , COMP_CNT  " + "\n");
                strSqlString.Append("                                                                     , HX_COMP_MIN, HX_COMP_MAX " + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd"))
                {
                    strSqlString.Append("                                                                  FROM RWIPLOTSTS A   " + "\n");
                    strSqlString.Append("                                                                     , MWIPOPRDEF B   " + "\n");
                    strSqlString.Append("                                                                     , VWIPMATDEF C " + "\n");
                    strSqlString.Append("                                                                 WHERE 1 = 1  " + "\n");
                }
                else
                {
                    strSqlString.Append("                                                                  FROM RWIPLOTSTS_BOH A   " + "\n");
                    strSqlString.Append("                                                                     , MWIPOPRDEF B   " + "\n");
                    strSqlString.Append("                                                                     , VWIPMATDEF C " + "\n");
                    strSqlString.Append("                                                                 WHERE 1 = 1  " + "\n");

                    // 2014-10-13-임종우 : 06시를 선택시 06시 조회 그 이외는 22시 조회
                    //if (cboTimeBase.Text == "06시")
                    if (cboTimeBase.SelectedIndex== 6)
                    {
                        strSqlString.Append("                                                                   AND A.CUTOFF_DT = '" + cdvDate.Value.ToString("yyyyMMdd") + "06' " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("                                                                   AND A.CUTOFF_DT = '" + cdvDate.Value.ToString("yyyyMMdd") + "22' " + "\n");
                    }
                }
                strSqlString.Append("                                                                   AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                                                                   AND A.FACTORY = C.FACTORY " + "\n");
                strSqlString.Append("                                                                   AND A.OPER = B.OPER " + "\n");
                strSqlString.Append("                                                                   AND A.MAT_ID = C.MAT_ID " + "\n");
                strSqlString.Append("                                                                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'   " + "\n");
                strSqlString.Append("                                                                   AND A.LOT_TYPE = 'W'  " + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                                                                   AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                                                                   AND A.LOT_DEL_FLAG = ' '  " + "\n");
                strSqlString.Append("                                                                   AND C.MAT_GRP_2 <> '-' " + "\n");
                strSqlString.Append("                                                                   AND C.DELETE_FLAG = ' '  " + "\n");
                //1 -> None Hold / 2 -> Hold
                if (holdFlag.SelectedIndex == 1)
                {
                    strSqlString.Append("                                                                   AND A.HOLD_FLAG = ' '  " + "\n");
                }
                else if (holdFlag.SelectedIndex == 2)
                {
                    strSqlString.Append("                                                                   AND A.HOLD_FLAG = 'Y'  " + "\n");
                }

                strSqlString.Append("                                                               ) " + "\n");
                strSqlString.Append("                                                       ) " + "\n");
                strSqlString.Append("                                               ) " + "\n");
                strSqlString.Append("                                         WHERE 1=1 " + "\n");
                strSqlString.Append("                                           AND OPER_GRP_1 <> ' ' " + "\n");
                strSqlString.Append("                                         GROUP BY MAT_ID, OPER_GRP_1 " + "\n");
                strSqlString.Append("                                        HAVING SUM(NVL(QTY,0)) > 0 " + "\n");
            }
            #endregion

            strSqlString.Append("                                       ) WIP " + "\n");
            strSqlString.Append("                                     , ( " + "\n");
            strSqlString.Append("                                        SELECT A.RES_STS_2 AS MAT_ID " + "\n");
            strSqlString.Append("                                             , A.OPER_GRP_1 " + "\n");
            strSqlString.Append("                                             , SUM(A.RES_CNT) AS RES_CNT " + "\n");
            strSqlString.Append("                                             , SUM(TRUNC(NVL(NVL(B.UPEH,0) * 24 * A.PERCENT * A.RES_CNT, 0))) AS RES_CAPA " + "\n");
            strSqlString.Append("                                          FROM ( " + "\n");
            strSqlString.Append("                                                SELECT FACTORY, RES_STS_2, RES_STS_8 AS OPER, RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, COUNT(RES_ID) AS RES_CNT " + "\n");
            //strSqlString.Append("                                                     , DECODE(SUBSTR(RES_STS_8, 1, 3), 'A06', 0.75, 0.7 ) AS PERCENT " + "\n");
            strSqlString.Append("                                                     , CASE WHEN RES_STS_8 LIKE 'A06%' THEN " + GlobalVariable.gdPer_wb + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 LIKE 'A04%' THEN " + GlobalVariable.gdPer_da + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 = 'A0333' THEN " +GlobalVariable.gdPer_da + "\n");
            strSqlString.Append("                                                            ELSE " + GlobalVariable.gdPer_etc + "\n");
            strSqlString.Append("                                                       END PERCENT " + "\n");
            strSqlString.Append("                                                     , CASE WHEN RES_STS_8 IN ('A0040') THEN 'BG' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0200', 'A0230') THEN 'SAW' " + "\n");

            //1191214 이희석 F-CHIP 관점으로 인한 데이터 조건 추가
            if (cdvType.SelectedIndex == 3)
            {
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0400', 'A0401') THEN 'DA1' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0402') THEN 'DA2' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0403') THEN 'DA3' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0404') THEN 'DA4' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0405') THEN 'DA5' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0406') THEN 'DA6' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0407') THEN 'DA7' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0408') THEN 'DA8' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0409') THEN 'DA9' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0250') THEN 'UV EXPOSE 2'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0333') THEN 'CHIP ATTACH'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0337') THEN 'DEFLUX' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0340') THEN 'PREBAKE 2' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0350') THEN 'PLASMA 1' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0370') THEN 'UNDERFILL' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0380') THEN 'U/F CURE' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0381') THEN 'FC Flipper' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0391') THEN 'BTM SMT' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0800') THEN 'QC GATE 3' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0910') THEN 'H/S Attach' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0920') THEN 'HS/A CURE'  " + "\n");
            }
            else if (cdvType.SelectedIndex == 4)
            {
                strSqlString.Append("                                                            WHEN RES_STS_8 = 'A0000' THEN 'DIE_BANK'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0005','A0010','A0012','A0013','A0015','A0020') THEN 'LN' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0023','A0025','A0030') THEN 'PRE_BG'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 = 'A0033' THEN 'STEALTH' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0040','A0041','A0042','A0045') THEN 'BG' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0050','A0055') THEN 'DETACH' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0060','A0065') THEN 'BSCOATING'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 = 'A0070' THEN 'COATINGTCURE' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 = 'A0080' THEN 'BS_MARKING'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0090','A0095','A0100') THEN 'TAPE_MOUNT'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0110','A0120','A0130','A0140','A0150','A0160','A0161','A0165','A0170') THEN 'LG'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0175','A0176','A0180','A0190','A0200','A0201','A0202','A0210','A0215','A0220','A0300') THEN 'SAWING'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0230','A0240') THEN 'WE'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 = 'A0250' THEN 'UV'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0270','A0290','A0295') THEN 'COB_AVI' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 BETWEEN 'A0290' AND 'AZ010' THEN 'EXTRA' " + "\n");
            }
            else if (cdvType.SelectedIndex == 5)
            {
                strSqlString.Append("                                                            WHEN RES_STS_8 = 'A0000' THEN 'DIE_BANK'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0005','A0010','A0012','A0013','A0015','A0020') THEN 'LN' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0023','A0025','A0030') THEN 'PRE_BG'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 = 'A0033' THEN 'STEALTH' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0040','A0041','A0042','A0045') THEN 'BG' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0050','A0055','A0060','A0065','A0070','A0080','A0090','A0095','A0100','A0110','A0120','A0130','A0140','A0150','A0160','A0161','A0165','A0170') THEN 'LG'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0175','A0176','A0180','A0190','A0200','A0201','A0202','A0210','A0215','A0220','A0300') THEN 'SAWING'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0230','A0240') THEN 'WE'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 = 'A0250' THEN 'UV'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0270','A0290','A0295') THEN 'COB_AVI' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 BETWEEN 'A0290' AND 'AZ010' THEN 'EXTRA' " + "\n");
            }
            else if (cdvType.SelectedIndex == 6)
            {
                strSqlString.Append("                                                            WHEN RES_STS_8 = 'A0000' THEN 'DIE_BANK'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0005','A0010','A0012','A0013','A0015','A0020') THEN 'LN' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0023','A0025','A0030','A0033','A0040','A0041','A0042','A0045') THEN 'BG' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0050','A0055') THEN 'DETACH' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0060','A0065') THEN 'BSCOATING'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 = 'A0070' THEN 'COATINGTCURE' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 = 'A0080' THEN 'BS_MARKING'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0090','A0095','A0100') THEN 'TAPE_MOUNT'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0110','A0120','A0130','A0140','A0150','A0160','A0161','A0165','A0170','A0175','A0176','A0180','A0190','A0200','A0201','A0202','A0210','A0215','A0220','A0230','A0240','A0300') THEN 'SAWING'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 = 'A0250' THEN 'UV'  " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0270','A0290','A0295') THEN 'COB_AVI' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 BETWEEN 'A0290' AND 'AZ010' THEN 'EXTRA' " + "\n");
            }
            else
            {
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0400', 'A0401', 'A0333') THEN 'DA1' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0402') THEN 'DA2' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0403') THEN 'DA3' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0404') THEN 'DA4' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0405') THEN 'DA5' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0406') THEN 'DA6' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0407') THEN 'DA7' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0408') THEN 'DA8' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0409') THEN 'DA9' " + "\n");
            }
            
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0600','A0601') THEN 'WB1' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0602') THEN 'WB2' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0603') THEN 'WB3' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0604') THEN 'WB4' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0605') THEN 'WB5' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0606') THEN 'WB6' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0607') THEN 'WB7' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0608') THEN 'WB8' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0609') THEN 'WB9' " + "\n");
            //1191214 이희석 F-CHIP 관점으로 인한 데이터 조건 추가
            if (cdvType.SelectedIndex == 3)
            {
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0801', 'A0802', 'A0803', 'A0804', 'A0805', 'A0806', 'A0807', 'A0808', 'A0809') THEN 'GATE' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A1000') THEN 'MOLD' " + "\n");
            }
            else
            {
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A0800', 'A0801', 'A0802', 'A0803', 'A0804', 'A0805', 'A0806', 'A0807', 'A0808', 'A0809') THEN 'GATE' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A1000', 'A0910') THEN 'MOLD' " + "\n");
            }
            
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A1100') THEN 'CURE' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A1200') THEN 'TRIM' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A1450') THEN 'TIN' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A1300') THEN 'SBA' " + "\n");
            if (cdvType.SelectedIndex == 7)
            {
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A1240') THEN 'FLIPPER' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A1370') THEN 'AOI' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A1380') THEN 'PP' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A1150') THEN 'MK' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A1790') THEN 'DC' " + "\n");
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A1500') THEN 'MK2' " + "\n");
            }
            else
            {
                strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A1150', 'A1500') THEN 'MK' " + "\n");
            }
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A1750', 'A1800', 'A1900', 'A1825') THEN 'SIG' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A2000') THEN 'AVI' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A2050') THEN 'PVI' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('A2100') THEN 'QC_GATE' " + "\n");
            strSqlString.Append("                                                            ELSE ' ' " + "\n");
            strSqlString.Append("                                                       END OPER_GRP_1 " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                                                  FROM MRASRESDEF " + "\n");
                strSqlString.Append("                                                 WHERE 1 = 1  " + "\n");
            }
            else
            {
                strSqlString.Append("                                                  FROM MRASRESDEF_BOH " + "\n");
                strSqlString.Append("                                                 WHERE 1 = 1  " + "\n");
                strSqlString.Append("                                                   AND CUTOFF_DT = '" + cdvDate.Value.ToString("yyyyMMdd") + "22' " + "\n");
            }

            strSqlString.Append("                                                   AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                                   AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("                                                   AND RES_CMF_7 = 'Y' " + "\n");
            strSqlString.Append("                                                   AND DELETE_FLAG = ' ' " + "\n");
             //1191214 이희석 F-CHIP 관점으로 인한 데이터 조건 추가
            if (cdvType.SelectedIndex == 3)
            {
                strSqlString.Append("                                                   AND (RES_TYPE='EQUIPMENT' OR RES_TYPE='LINE') " + "\n");
            }
            else
            {
                strSqlString.Append("                                                   AND RES_TYPE='EQUIPMENT' " + "\n");
            }
            //strSqlString.Append("                                                   AND RES_STS_1 NOT IN ('C200', 'B199') " + "\n");
            strSqlString.Append("                                                   AND (RES_STS_1 NOT IN ('C200', 'B199', 'B800') OR RES_UP_DOWN_FLAG = 'U') " + "\n");
            strSqlString.Append("                                                 GROUP BY FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7  " + "\n");
            strSqlString.Append("                                               ) A " + "\n");
            strSqlString.Append("                                             , CRASUPHDEF B " + "\n");
            strSqlString.Append("                                         WHERE 1=1 " + "\n");
            strSqlString.Append("                                           AND A.FACTORY = B.FACTORY(+) " + "\n");
            strSqlString.Append("                                           AND A.OPER = B.OPER(+) " + "\n");
            strSqlString.Append("                                           AND A.RES_MODEL = B.RES_MODEL(+) " + "\n");
            strSqlString.Append("                                           AND A.UPEH_GRP = B.UPEH_GRP(+) " + "\n");
            strSqlString.Append("                                           AND A.RES_STS_2 = B.MAT_ID(+) " + "\n");
            strSqlString.Append("                                           AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                           AND A.OPER NOT IN ('00001','00002') " + "\n");
            strSqlString.Append("                                           AND A.OPER_GRP_1 <> ' ' " + "\n");
            strSqlString.Append("                                         GROUP BY A.RES_STS_2, A.OPER_GRP_1 " + "\n");
            strSqlString.Append("                                       ) RES " + "\n");
            strSqlString.Append("                                 WHERE 1=1 " + "\n");

            // 2014-07-07-임종우 : 계획 유무에 의한 검색 기능 추가
            if (ckdPlan.Checked == true)
            {
                strSqlString.Append("                                   AND MAT.MAT_ID = A.MAT_ID " + "\n");
            }
            else
            {
                strSqlString.Append("                                   AND MAT.MAT_ID = A.MAT_ID(+) " + "\n");
            }

            strSqlString.Append("                                   AND MAT.MAT_ID = B.MAT_ID(+) " + "\n");
            strSqlString.Append("                                   AND MAT.MAT_ID = WIP.MAT_ID(+) " + "\n");
            strSqlString.Append("                                   AND MAT.MAT_ID = RES.MAT_ID(+) " + "\n");
            strSqlString.Append("                                   AND MAT.OPER_GRP_1 = B.OPER_GRP_1(+) " + "\n");
            strSqlString.Append("                                   AND MAT.OPER_GRP_1 = WIP.OPER_GRP_1(+) " + "\n");
            strSqlString.Append("                                   AND MAT.OPER_GRP_1 = RES.OPER_GRP_1(+) " + "\n");
            strSqlString.Append("                                   ) " + "\n");
            strSqlString.Append("                                   WHERE 1=1" + "\n");
            strSqlString.Append("                                   AND MAT_ID NOT IN (SELECT MAT_ID FROM MWIPMATDEF WHERE FIRST_FLOW = 'A-BANK' AND DELETE_FLAG = ' ') " + "\n");
            //strSqlString.Append("                                 ORDER BY A.MAT_ID, DECODE(MAT.OPER_GRP_1, 'HMK3A', 1, 'QC_GATE', 2, 'PVI', 3, 'AVI', 4, 'SIG', 5, 'SBA', 6, 'TIN', 7, 'TRIM', 8, 'MK', 9, 'CURE', 10 " + "\n");
            //strSqlString.Append("                                                                       , 'MOLD', 11, 'GATE', 12, 'WB5', 13, 'DA5', 14, 'WB4', 15, 'DA4', 16, 'WB3', 17, 'DA3', 18, 'WB2', 19, 'DA2', 20 " + "\n");
            //strSqlString.Append("                                                                       , 'WB1', 21, 'DA1', 22, 'SP', 23, 'SAW', 24, 'BG', 25, 'HMK2A',26, 27) " + "\n");
            strSqlString.Append("                               ) " + "\n");
            strSqlString.Append("                         GROUP BY " + QueryCond3 + ", OPER_GRP_1 " + "\n");
            strSqlString.Append("                        HAVING ( " + "\n");
            strSqlString.Append("                                SUM(NVL(D0_ORI_PLAN,0))+SUM(NVL(D1_PLAN,0))+SUM(NVL(D2_PLAN,0))+SUM(NVL(WEEK_PLAN,0))+SUM(NVL(WEEK2_PLAN,0))+SUM(NVL(MON_PLAN,0))+SUM(NVL(WIP_QTY,0)) " + "\n");
            strSqlString.Append("                              + SUM(NVL(D0_PLAN,0))+SUM(NVL(SHP_WEEK,0))+SUM(NVL(SHP_MONTH,0))+SUM(NVL(ASSY_END_QTY,0)) " + "\n");

            //if (cdvGroup.Text == "ALL")
            //{
            //    strSqlString.Append("                                SUM(NVL(D0_ORI_PLAN,0))+SUM(NVL(D1_PLAN,0))+SUM(NVL(D2_PLAN,0))+SUM(NVL(WEEK_PLAN,0))+SUM(NVL(WEEK2_PLAN,0))+SUM(NVL(MON_PLAN,0))+SUM(NVL(WIP_QTY,0)) " + "\n");
            //}
            //else if (cdvGroup.Text == "당일 실적")
            //{
            //    strSqlString.Append("                                SUM(NVL(CHK_ASSY_END_QTY,0))+SUM(NVL(WIP_QTY,0))  " + "\n");
            //}
            //else
            //{
            //    string[] condList = cdvGroup.Text.Split(',');
            //    string strMakeCond = "";

            //    for (int i = 0; i < condList.Length; i++)
            //    {
            //        if ("금일 잔량" == condList[i].Trim())
            //        {
            //            strMakeCond += "SUM(NVL(D0_ORI_PLAN,0))+";
            //        }
            //        else if ("명일 잔량" == condList[i].Trim())
            //        {
            //            strMakeCond += "SUM(NVL(D1_PLAN,0))+";
            //        }
            //        else if ("주간 잔량" == condList[i].Trim())
            //        {
            //            strMakeCond += "SUM(NVL(WEEK_PLAN,0))+";
            //        }
            //        else if ("월간 잔량" == condList[i].Trim())
            //        {
            //            strMakeCond += "SUM(NVL(MON_PLAN,0))+";
            //        }
            //    }

            //    strMakeCond = strMakeCond.Trim('+');

            //    strSqlString.Append("                                " + strMakeCond + "+SUM(NVL(WIP_QTY,0)) " + "\n");
            //}

            strSqlString.Append("                               ) > 0 " + "\n");
            strSqlString.Append("                      ) " + "\n");
            strSqlString.Append("                    , (SELECT LEVEL AS SEQ FROM DUAL CONNECT BY LEVEL <= 10) " + "\n");
            strSqlString.Append("                 GROUP BY " + QueryCond3 + ", OPER_GRP_1, DECODE(SEQ, 1, 'WIP', 2, '설비대수', 3, 'CAPA현황', 4, '당일 실적', 5, 'D0 잔량', 6, 'D1 잔량', 7, 'D2 잔량', 8, '당주 잔량', 9, '차주 잔량', 10, '월간 잔량') " + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("         GROUP BY  " + QueryCond3 + ", GUBUN " + "\n");
            strSqlString.Append("       ) A " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");

            if (cdvGroup.Text != "ALL")
            {
                strSqlString.Append("   AND (GUBUN = 'WIP' OR GUBUN " + cdvGroup.SelectedValueToQueryString + ") " + "\n");
            }

            strSqlString.Append(" GROUP BY " + QueryCond3 + ", GUBUN " + "\n");
            strSqlString.Append(" ORDER BY " + QueryCond4 + ", DECODE(GUBUN, 'WIP', 1, '설비대수', 2, 'CAPA현황', 3, '당일 실적', 4, 'D0 잔량', 5, 'D1 잔량', 6, 'D2 잔량', 7, '당주 잔량', 8, '차주 잔량', 9, '월간 잔량', 10) " + "\n");
            

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
 
            return strSqlString.ToString();
        }

        /// <summary>
        /// 설정된 그룹별 공정 정보
        /// </summary>
        /// <returns></returns>
        private string MakeSqlStringStepInfo()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string QueryCond4 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            strSqlString.Append("SELECT " + QueryCond1 + " " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'HMK3A', 'T', '', 'F', 'F')) AS HMK3A " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'QC_GATE', 'T', '', 'F', 'F')) AS QC_GATE " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'PVI', 'T', '', 'F', 'F')) AS PVI " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'AVI', 'T', '', 'F', 'F')) AS AVI " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'SIG', 'T', '', 'F', 'F')) AS SIG " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'SBA', 'T', '', 'F', 'F')) AS SBA " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'TIN', 'T', '', 'F', 'F')) AS TIN " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'TRIM', 'T', '', 'F', 'F')) AS TRIM " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'MK', 'T', '', 'F', 'F')) AS MK " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'CURE', 'T', '', 'F', 'F')) AS CURE " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'MOLD', 'T', '', 'F', 'F')) AS MOLD " + "\n");
            //1191214 이희석 F-CHIP 관점으로 인한 데이터 조건 추가
            if (cdvType.SelectedIndex == 3)
            {
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'UV', 'T', '', 'F', 'F')) AS UV " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'C/A', 'T', '', 'F', 'F')) AS \"C/A\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DEFLUX', 'T', '', 'F', 'F')) AS DEFLUX " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'PREBAKE 2', 'T', '', 'F', 'F')) AS \"PREBAKE 2\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'PLASMA 1', 'T', '', 'F', 'F')) AS \"PLASMA 1\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'U/F', 'T', '', 'F', 'F')) AS \"U/F\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'U/F CURE', 'T', '', 'F', 'F')) AS \"U/F CURE\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'Flipper', 'T', '', 'F', 'F')) AS Flipper " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'BTM SMT', 'T', '', 'F', 'F')) AS \"BTM SMT\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'QC GATE 3', 'T', '', 'F', 'F')) AS \"QC GATE 3\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'H/S/A', 'T', '', 'F', 'F')) AS \"H/S/A\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'HSA CURE', 'T', '', 'F', 'F')) AS \"HSA CURE\" " + "\n");
            }
            else if (cdvType.SelectedIndex == 4)
            {
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DIE_BANK', 'T', '', 'F', 'F')) AS DIE_BANK " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'L/N', 'T', '', 'F', 'F')) AS \"L/N\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'PRE_B/G', 'T', '', 'F', 'F')) AS \"PRE_B/G\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'STEALTH', 'T', '', 'F', 'F')) AS STEALTH " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'BG', 'T', '', 'F', 'F')) AS BG " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DETACH', 'T', '', 'F', 'F')) AS DETACH " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'BS/COATING', 'T', '', 'F', 'F')) AS \"BS/COATING\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'COATING/T/CURE', 'T', '', 'F', 'F')) AS \"COATING/T/CURE\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'B/S_MARKING', 'T', '', 'F', 'F')) AS \"B/S_MARKING\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'TAPE_MOUNT', 'T', '', 'F', 'F')) AS TAPE_MOUNT " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'LG', 'T', '', 'F', 'F')) AS LG " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'SAWING', 'T', '', 'F', 'F')) AS SAW " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'W/E', 'T', '', 'F', 'F')) AS \"W/E\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'UV', 'T', '', 'F', 'F')) AS UV " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'COB_AVI', 'T', '', 'F', 'F')) AS COB_AVI " + "\n");
            }
            else if (cdvType.SelectedIndex == 5)
            {
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DIE_BANK', 'T', '', 'F', 'F')) AS DIE_BANK " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'L/N', 'T', '', 'F', 'F')) AS \"L/N\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'PRE_B/G', 'T', '', 'F', 'F')) AS \"PRE_B/G\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'STEALTH', 'T', '', 'F', 'F')) AS STEALTH " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'BG', 'T', '', 'F', 'F')) AS BG " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'LG', 'T', '', 'F', 'F')) AS LG " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'SAWING', 'T', '', 'F', 'F')) AS SAW " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'W/E', 'T', '', 'F', 'F')) AS \"W/E\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'UV', 'T', '', 'F', 'F')) AS UV " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'COB_AVI', 'T', '', 'F', 'F')) AS COB_AVI " + "\n");
            }
            else if (cdvType.SelectedIndex == 6)
            {
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DIE_BANK', 'T', '', 'F', 'F')) AS DIE_BANK " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'L/N', 'T', '', 'F', 'F')) AS \"L/N\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'BG', 'T', '', 'F', 'F')) AS BG " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DETACH', 'T', '', 'F', 'F')) AS DETACH " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'BS/COATING', 'T', '', 'F', 'F')) AS \"BS/COATING\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'COATING/T/CURE', 'T', '', 'F', 'F')) AS \"COATING/T/CURE\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'B/S_MARKING', 'T', '', 'F', 'F')) AS \"B/S_MARKING\" " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'TAPE_MOUNT', 'T', '', 'F', 'F')) AS TAPE_MOUNT " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'SAWING', 'T', '', 'F', 'F')) AS SAW " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'UV', 'T', '', 'F', 'F')) AS UV " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'COB_AVI', 'T', '', 'F', 'F')) AS COB_AVI " + "\n");
            }
            else
            {
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'GATE', 'T', '', 'F', 'F')) AS GATE " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'WB9', 'T', '', 'F', 'F')) AS WB9 " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DA9', 'T', '', 'F', 'F')) AS DA9 " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'WB8', 'T', '', 'F', 'F')) AS WB8 " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DA8', 'T', '', 'F', 'F')) AS DA8 " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'WB7', 'T', '', 'F', 'F')) AS WB7 " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DA7', 'T', '', 'F', 'F')) AS DA7 " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'WB6', 'T', '', 'F', 'F')) AS WB6 " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DA6', 'T', '', 'F', 'F')) AS DA6 " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'WB5', 'T', '', 'F', 'F')) AS WB5 " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DA5', 'T', '', 'F', 'F')) AS DA5 " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'WB4', 'T', '', 'F', 'F')) AS WB4 " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DA4', 'T', '', 'F', 'F')) AS DA4 " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'WB3', 'T', '', 'F', 'F')) AS WB3 " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DA3', 'T', '', 'F', 'F')) AS DA3  " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'WB2', 'T', '', 'F', 'F')) AS WB2 " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DA2', 'T', '', 'F', 'F')) AS DA2 " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'WB1', 'T', '', 'F', 'F')) AS WB1 " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DA1', 'T', '', 'F', 'F')) AS DA1 " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'SP', 'T', '', 'F', 'F')) AS SP " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'SAW', 'T', '', 'F', 'F')) AS SAW " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'BG', 'T', '', 'F', 'F')) AS BG " + "\n");
                strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'HMK2A','T', '', 'F', 'F')) AS HMK2A " + "\n");
            }
            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT MAT.FACTORY, " + QueryCond2.Replace(", CONV_MAT_ID", "") + ", MAT.MAT_ID, MAT.DELETE_FLAG " + "\n");
            strSqlString.Append("             , CASE WHEN MAT.MAT_GRP_1 = 'SE' AND MAT.MAT_GRP_9 = 'MEMORY' THEN 'SEK_________-___' || SUBSTR(MAT.MAT_ID, -3) " + "\n");
            strSqlString.Append("                                                                    WHEN MAT.MAT_GRP_1 = 'HX' THEN MAT.MAT_CMF_10 " + "\n");
            strSqlString.Append("                                                                    ELSE MAT_ID " + "\n");
            strSqlString.Append("                                                               END CONV_MAT_ID " + "\n");
            strSqlString.Append("          FROM MWIPMATDEF MAT " + "\n");
            strSqlString.Append("       ) MAT " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT A.MAT_ID " + "\n");
            //1191214 이희석 F-CHIP 관점으로 인한 데이터 조건 추가
            if (cdvType.SelectedIndex == 3)
            {
                strSqlString.Append("                     ,CASE WHEN B.OPER ='A0250' THEN 'UV' " + "\n");
                strSqlString.Append("                           WHEN B.OPER ='A0333' THEN 'C/A' " + "\n");
                strSqlString.Append("                           WHEN B.OPER ='A0337' THEN 'DEFLUX' " + "\n");
                strSqlString.Append("                           WHEN B.OPER ='A0340' THEN 'PREBAKE 2' " + "\n");
                strSqlString.Append("                           WHEN B.OPER ='A0350' THEN 'PLASMA 1' " + "\n");
                strSqlString.Append("                           WHEN B.OPER ='A0370' THEN 'U/F' " + "\n");
                strSqlString.Append("                           WHEN B.OPER ='A0380' THEN 'U/F CURE' " + "\n");
                strSqlString.Append("                           WHEN B.OPER ='A0381' THEN 'Flipper' " + "\n");
                strSqlString.Append("                           WHEN B.OPER ='A0391' THEN 'BTM SMT' " + "\n");
                strSqlString.Append("                           WHEN B.OPER ='A0800' THEN 'QC GATE 3' " + "\n");
                strSqlString.Append("                           WHEN B.OPER ='A0910' THEN 'H/S/A' " + "\n");
                strSqlString.Append("                           WHEN B.OPER ='A0920' THEN 'HSA CURE' " + "\n");
            }
            else if (cdvType.SelectedIndex == 4)
            {
                strSqlString.Append("                    , CASE WHEN B.OPER IN ('A0000','A000N') THEN 'DIE_BANK'   " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0005','A0010','A0012','A0013','A0015','A0020') THEN 'L/N'   " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0023','A0025','A0030') THEN 'PRE_B/G'  " + "\n");
                strSqlString.Append("                           WHEN B.OPER ='A0033' THEN 'STEALTH'   " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0040','A0041','A0042','A0045') THEN 'BG'   " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0050','A0055') THEN 'DETACH'   " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0060','A0065') THEN 'BS/COATING'  " + "\n");
                strSqlString.Append("                           WHEN B.OPER ='A0070' THEN 'COATING/T/CURE' " + "\n");
                strSqlString.Append("                           WHEN B.OPER ='A0080' THEN 'B/S_MARKING'" + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0090','A0095','A0100') THEN 'TAPE_MOUNT'  " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0110','A0120','A0130','A0140','A0150','A0160','A0161','A0165','A0170') THEN 'LG' " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0175','A0176','A0180','A0190','A0200','A0201','A0202','A0210','A0215','A0220','A0300') THEN 'SAWING' " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0230','A0240') THEN 'W/E'  " + "\n");
                strSqlString.Append("                           WHEN B.OPER = 'A0250' THEN 'UV' " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0270','A0290','A0295') THEN 'COB_AVI' " + "\n");
            }
            else if (cdvType.SelectedIndex == 5)
            {
                strSqlString.Append("                    , CASE WHEN B.OPER IN ('A0000','A000N') THEN 'DIE_BANK'   " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0005','A0010','A0012','A0013','A0015','A0020') THEN 'L/N'   " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0023','A0025','A0030') THEN 'PRE_B/G'  " + "\n");
                strSqlString.Append("                           WHEN B.OPER ='A0033' THEN 'STEALTH'   " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0040','A0041','A0042','A0045') THEN 'BG'   " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0050','A0055','A0060','A0065','A0070','A0080','A0090','A0095','A0100','A0110','A0120','A0130','A0140','A0150','A0160','A0161','A0165','A0170') THEN 'LG' " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0175','A0176','A0180','A0190','A0200','A0201','A0202','A0210','A0215','A0220','A0300') THEN 'SAWING' " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0230','A0240') THEN 'W/E'  " + "\n");
                strSqlString.Append("                           WHEN B.OPER = 'A0250' THEN 'UV' " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0270','A0290','A0295') THEN 'COB_AVI' " + "\n");
            }
            else if (cdvType.SelectedIndex == 6)
            {
                strSqlString.Append("                    , CASE WHEN B.OPER IN ('A0000','A000N') THEN 'DIE_BANK'   " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0005','A0010','A0012','A0013','A0015','A0020') THEN 'L/N'   " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0023','A0025','A0030','A0033','A0040','A0041','A0042','A0045') THEN 'BG'   " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0050','A0055') THEN 'DETACH'   " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0060','A0065') THEN 'BS/COATING'  " + "\n");
                strSqlString.Append("                           WHEN B.OPER ='A0070' THEN 'COATING/T/CURE' " + "\n");
                strSqlString.Append("                           WHEN B.OPER ='A0080' THEN 'B/S_MARKING'" + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0090','A0095','A0100') THEN 'TAPE_MOUNT'  " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0110','A0120','A0130','A0140','A0150','A0160','A0161','A0165','A0170','A0175','A0176','A0180','A0190','A0200','A0201','A0202','A0210','A0215','A0220','A0230','A0240','A0300') THEN 'SAWING' " + "\n");
                strSqlString.Append("                           WHEN B.OPER = 'A0250' THEN 'UV' " + "\n");
                strSqlString.Append("                           WHEN B.OPER IN ('A0270','A0290','A0295') THEN 'COB_AVI' " + "\n");
            }
            else
            {
                strSqlString.Append("                     , CASE WHEN B.OPER IN ('A0400', 'A0401', 'A0333') THEN 'DA1' " + "\n");
                strSqlString.Append("                            WHEN B.OPER IN ('A0402') THEN 'DA2' " + "\n");
                strSqlString.Append("                            WHEN B.OPER IN ('A0403') THEN 'DA3' " + "\n");
                strSqlString.Append("                            WHEN B.OPER IN ('A0404') THEN 'DA4' " + "\n");
                strSqlString.Append("                            WHEN B.OPER IN ('A0405') THEN 'DA5' " + "\n");
                strSqlString.Append("                            WHEN B.OPER IN ('A0406') THEN 'DA6' " + "\n");
                strSqlString.Append("                            WHEN B.OPER IN ('A0407') THEN 'DA7' " + "\n");
                strSqlString.Append("                            WHEN B.OPER IN ('A0408') THEN 'DA8' " + "\n");
                strSqlString.Append("                            WHEN B.OPER IN ('A0409') THEN 'DA9' " + "\n");
                strSqlString.Append("                            WHEN B.OPER IN ('A0600', 'A0601') THEN 'WB1' " + "\n");
                strSqlString.Append("                            WHEN B.OPER IN ('A0602') THEN 'WB2' " + "\n");
                strSqlString.Append("                            WHEN B.OPER IN ('A0603') THEN 'WB3' " + "\n");
                strSqlString.Append("                            WHEN B.OPER IN ('A0604') THEN 'WB4' " + "\n");
                strSqlString.Append("                            WHEN B.OPER IN ('A0605') THEN 'WB5' " + "\n");
                strSqlString.Append("                            WHEN B.OPER IN ('A0606') THEN 'WB6' " + "\n");
                strSqlString.Append("                            WHEN B.OPER IN ('A0607') THEN 'WB7' " + "\n");
                strSqlString.Append("                            WHEN B.OPER IN ('A0608') THEN 'WB8' " + "\n");
                strSqlString.Append("                            WHEN B.OPER IN ('A0609') THEN 'WB9' " + "\n");
            }
            strSqlString.Append("                            ELSE ' ' " + "\n");
            strSqlString.Append("                        END OPER_GRP_1 " + "\n");
            strSqlString.Append("          FROM MWIPMATDEF A " + "\n");
            strSqlString.Append("             , MWIPFLWOPR@RPTTOMES B " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.FIRST_FLOW = B.FLOW " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("           AND A.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND A.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("           AND A.MAT_GRP_5 IN ('-', 'Merge') " + "\n");

            strSqlString.Append("           AND A.MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion

            strSqlString.Append("       ) A " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("   AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = A.MAT_ID  " + "\n");
            strSqlString.Append(" GROUP BY " + QueryCond2 + " " + "\n");
            strSqlString.Append(" ORDER BY " + QueryCond4 + " " + "\n");

            return strSqlString.ToString();
        }

        /// <summary>
        /// Bump 조회 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlStringBump()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string QueryCond4 = null;

            string Yesterday;
            string Yesterdaybf;
            string Today;
            string sMonth;
            string sStartDate;
            string sTable_fac;
            string sTable_wip;
            string strKpcs = "1000";

            Today = cdvDate.Value.ToString("yyyyMMdd");
            Yesterday = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");
            Yesterdaybf = cdvDate.Value.AddDays(-2).ToString("yyyyMMdd");
            sMonth = cdvDate.Value.ToString("yyyyMM");

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            // 조회월과 조회주차의 시작일이 같은 달이면 시작은 조회월의 1일자로 하고, 다르면(주차시작일이 작으면) 주차 시작일을 시작일로 한다.
            if (sMonth == FindWeek_SOP_A.StartDay_ThisWeek.Substring(0, 6))
            {
                sStartDate = sMonth + "01";
            }
            else
            {
                sStartDate = FindWeek_SOP_A.StartDay_ThisWeek;
            }

            if (ckbKpcs.Checked == true)
            {
                strKpcs = "DECODE(GUBUN, '설비대수', 1, 1000)";
            }
            else
            {
                strKpcs = "1";
            }

            //if (cdvTime.Text == "22시")
            if (cdvTime.SelectedIndex == 1)
            {
                sTable_wip = "RSUMWIPMOV";
                sTable_fac = "VSUMWIPOUT";
            }
            else
            {
                sTable_wip = "CSUMWIPMOV";
                sTable_fac = "VSUMWIPOUT_06";
            }

            strSqlString.Append("SELECT " + QueryCond1 + " " + "\n");
            strSqlString.Append("     , ROUND(SUM(PLAN)/" + strKpcs + ",0) AS PLAN " + "\n");
            strSqlString.Append("     , ROUND(SUM(SHP)/" + strKpcs + ",0) AS SHP " + "\n");
            strSqlString.Append("     , ROUND(SUM(DEF)/" + strKpcs + ",0) AS DEF " + "\n");
            strSqlString.Append("     , GUBUN " + "\n");

            strSqlString.Append("     , ROUND(SUM(완제품창고)/" + strKpcs + ",0) AS 완제품창고 " + "\n");
            strSqlString.Append("     , ROUND(SUM(PACKING)/" + strKpcs + ",0) AS PACKING " + "\n");
            strSqlString.Append("     , ROUND(SUM(OGI)/" + strKpcs + ",0) AS OGI " + "\n");
            strSqlString.Append("     , ROUND(SUM(AVI)/" + strKpcs + ",0) AS AVI " + "\n");
            strSqlString.Append("     , ROUND(SUM(SORT1)/" + strKpcs + ",0) AS SORT1 " + "\n");
            strSqlString.Append("     , ROUND(SUM(FINAL_INSP)/" + strKpcs + ",0) AS FINAL_INSP " + "\n");
            strSqlString.Append("     , ROUND(SUM(REFLOW_BUMP)/" + strKpcs + ",0) AS REFLOW_BUMP " + "\n");
            strSqlString.Append("     , ROUND(SUM(BALL_MOUNT_BUMP)/" + strKpcs + ",0) AS BALL_MOUNT_BUMP " + "\n");
            strSqlString.Append("     , ROUND(SUM(TI_ETCH_BUMP)/" + strKpcs + ",0) AS TI_ETCH_BUMP " + "\n");
            strSqlString.Append("     , ROUND(SUM(CU_ETCH_BUMP)/" + strKpcs + ",0) AS CU_ETCH_BUMP " + "\n");
            strSqlString.Append("     , ROUND(SUM(PR_STRIP_BUMP)/" + strKpcs + ",0) AS PR_STRIP_BUMP " + "\n");

            strSqlString.Append("     , ROUND(SUM(SN_AG_PLATING_BUMP)/" + strKpcs + ",0) AS SN_AG_PLATING_BUMP " + "\n");
            strSqlString.Append("     , ROUND(SUM(NI_PLATING_BUMP)/" + strKpcs + ",0) AS NI_PLATING_BUMP " + "\n");

            strSqlString.Append("     , ROUND(SUM(CU_PLATING_BUMP)/" + strKpcs + ",0) AS CU_PLATING_BUMP " + "\n");

            //bump photo
            strSqlString.Append("     , ROUND(SUM(DESCUM_BUMP)/" + strKpcs + ",0) AS DESCUM_BUMP " + "\n");
            strSqlString.Append("     , ROUND(SUM(MEASURING_INSP_BUMP)/" + strKpcs + ",0) AS MEASURING_INSP_BUMP " + "\n");
            strSqlString.Append("     , ROUND(SUM(DEVELOP_BUMP)/" + strKpcs + ",0) AS DEVELOP_BUMP " + "\n");
            strSqlString.Append("     , ROUND(SUM(EXPOSURE_BUMP)/" + strKpcs + ",0) AS EXPOSURE_BUMP " + "\n");
            strSqlString.Append("     , ROUND(SUM(PR_COATING_BUMP)/" + strKpcs + ",0) AS PR_COATING_BUMP " + "\n");

            strSqlString.Append("     , ROUND(SUM(SPUTTER_BUMP)/" + strKpcs + ",0) AS SPUTTER_BUMP " + "\n");

            strSqlString.Append("     , ROUND(SUM(DESCUM_PSV3)/" + strKpcs + ",0) AS DESCUM_PSV3 " + "\n");
            strSqlString.Append("     , ROUND(SUM(DEVELOP_INSP_PSV3)/" + strKpcs + ",0) AS DEVELOP_INSP_PSV3 " + "\n");
            strSqlString.Append("     , ROUND(SUM(DEVELOP_PSV3)/" + strKpcs + ",0) AS DEVELOP_PSV3 " + "\n");
            strSqlString.Append("     , ROUND(SUM(EXPOSURE_PSV3)/" + strKpcs + ",0) AS EXPOSURE_PSV3 " + "\n");
            strSqlString.Append("     , ROUND(SUM(PI_COATING_PSV3)/" + strKpcs + ",0) AS PI_COATING_PSV3 " + "\n");

            strSqlString.Append("     , ROUND(SUM(DESCUM_RDL3E)/" + strKpcs + ",0) AS DESCUM_RDL3E " + "\n");
            strSqlString.Append("     , ROUND(SUM(TI_ETCH_RDL3)/" + strKpcs + ",0) AS TI_ETCH_RDL3 " + "\n");
            strSqlString.Append("     , ROUND(SUM(CU_ETCH_RDL3)/" + strKpcs + ",0) AS CU_ETCH_RDL3 " + "\n");
            strSqlString.Append("     , ROUND(SUM(PR_STRIP_RDL3)/" + strKpcs + ",0) AS PR_STRIP_RDL3 " + "\n");

            strSqlString.Append("     , ROUND(SUM(CU_PLATING_RDL3)/" + strKpcs + ",0) AS CU_PLATING_RDL3 " + "\n");

            strSqlString.Append("     , ROUND(SUM(DESCUM_RDL3P)/" + strKpcs + ",0) AS DESCUM_RDL3P " + "\n");
            strSqlString.Append("     , ROUND(SUM(MEASURING_INSP_RDL3)/" + strKpcs + ",0) AS MEASURING_INSP_RDL3 " + "\n");
            strSqlString.Append("     , ROUND(SUM(DEVELOP_RDL3)/" + strKpcs + ",0) AS DEVELOP_RDL3 " + "\n");
            strSqlString.Append("     , ROUND(SUM(EXPOSURE_RDL3)/" + strKpcs + ",0) AS EXPOSURE_RDL3 " + "\n");
            strSqlString.Append("     , ROUND(SUM(PR_COATING_RDL3)/" + strKpcs + ",0) AS PR_COATING_RDL3 " + "\n");

            strSqlString.Append("     , ROUND(SUM(SPUTTER_RDL3)/" + strKpcs + ",0) AS SPUTTER_RDL3 " + "\n");

            strSqlString.Append("     , ROUND(SUM(DESCUM_PSV2)/" + strKpcs + ",0) AS DESCUM_PSV2 " + "\n");
            strSqlString.Append("     , ROUND(SUM(CURE_PSV2)/" + strKpcs + ",0) AS CURE_PSV2 " + "\n");
            strSqlString.Append("     , ROUND(SUM(DEVELOP_INSP_PSV2)/" + strKpcs + ",0) AS DEVELOP_INSP_PSV2 " + "\n");
            strSqlString.Append("     , ROUND(SUM(DEVELOP_PSV2)/" + strKpcs + ",0) AS DEVELOP_PSV2 " + "\n");
            strSqlString.Append("     , ROUND(SUM(EXPOSURE_PSV2)/" + strKpcs + ",0) AS EXPOSURE_PSV2 " + "\n");
            strSqlString.Append("     , ROUND(SUM(PI_COATING_PSV2)/" + strKpcs + ",0) AS PI_COATING_PSV2 " + "\n");

            strSqlString.Append("     , ROUND(SUM(DESCUM_RDL2E)/" + strKpcs + ",0) AS DESCUM_RDL2E " + "\n");
            strSqlString.Append("     , ROUND(SUM(TI_ETCH_RDL2)/" + strKpcs + ",0) AS TI_ETCH_RDL2 " + "\n");
            strSqlString.Append("     , ROUND(SUM(CU_ETCH_RDL2)/" + strKpcs + ",0) AS CU_ETCH_RDL2 " + "\n");
            strSqlString.Append("     , ROUND(SUM(PR_STRIP_RDL2)/" + strKpcs + ",0) AS PR_STRIP_RDL2 " + "\n");

            strSqlString.Append("     , ROUND(SUM(CU_PLATING_RDL2)/" + strKpcs + ",0) AS CU_PLATING_RDL2 " + "\n");

            strSqlString.Append("     , ROUND(SUM(DESCUM_RDL2P)/" + strKpcs + ",0) AS DESCUM_RDL2P " + "\n");
            strSqlString.Append("     , ROUND(SUM(MEASURING_INSP_RDL2)/" + strKpcs + ",0) AS MEASURING_INSP_RDL2 " + "\n");
            strSqlString.Append("     , ROUND(SUM(DEVELOP_RDL2)/" + strKpcs + ",0) AS DEVELOP_RDL2 " + "\n");
            strSqlString.Append("     , ROUND(SUM(EXPOSURE_RDL2)/" + strKpcs + ",0) AS EXPOSURE_RDL2 " + "\n");
            strSqlString.Append("     , ROUND(SUM(PR_COATING_RDL2)/" + strKpcs + ",0) AS PR_COATING_RDL2 " + "\n");

            strSqlString.Append("     , ROUND(SUM(SPUTTER_RDL2)/" + strKpcs + ",0) AS SPUTTER_RDL2 " + "\n");

            strSqlString.Append("     , ROUND(SUM(DESCUM_PSV1)/" + strKpcs + ",0) AS DESCUM_PSV1 " + "\n");
            strSqlString.Append("     , ROUND(SUM(CURE_PSV1)/" + strKpcs + ",0) AS CURE_PSV1 " + "\n");
            strSqlString.Append("     , ROUND(SUM(DEVELOP_INSP_PSV1)/" + strKpcs + ",0) AS DEVELOP_INSP_PSV1 " + "\n");
            strSqlString.Append("     , ROUND(SUM(DEVELOP_PSV1)/" + strKpcs + ",0) AS DEVELOP_PSV1 " + "\n");
            strSqlString.Append("     , ROUND(SUM(EXPOSURE_PSV1)/" + strKpcs + ",0) AS EXPOSURE_PSV1 " + "\n");
            strSqlString.Append("     , ROUND(SUM(PI_COATING_PSV1)/" + strKpcs + ",0) AS PI_COATING_PSV1 " + "\n");

            strSqlString.Append("     , ROUND(SUM(DESCUM_RDL1E)/" + strKpcs + ",0) AS DESCUM_RDL1E " + "\n");
            strSqlString.Append("     , ROUND(SUM(TI_ETCH_RDL1)/" + strKpcs + ",0) AS TI_ETCH_RDL1 " + "\n");
            strSqlString.Append("     , ROUND(SUM(CU_ETCH_RDL1)/" + strKpcs + ",0) AS CU_ETCH_RDL1 " + "\n");
            strSqlString.Append("     , ROUND(SUM(PR_STRIP_RDL1)/" + strKpcs + ",0) AS PR_STRIP_RDL1 " + "\n");

            strSqlString.Append("     , ROUND(SUM(CU_PLATING_RDL1)/" + strKpcs + ",0) AS CU_PLATING_RDL1 " + "\n");

            strSqlString.Append("     , ROUND(SUM(DESCUM_RDL1P)/" + strKpcs + ",0) AS DESCUM_RDL1P " + "\n");
            strSqlString.Append("     , ROUND(SUM(MEASURING_INSP_RDL1)/" + strKpcs + ",0) AS MEASURING_INSP_RDL1 " + "\n");
            strSqlString.Append("     , ROUND(SUM(DEVELOP_RDL1)/" + strKpcs + ",0) AS DEVELOP_RDL1 " + "\n");
            strSqlString.Append("     , ROUND(SUM(EXPOSURE_RDL1)/" + strKpcs + ",0) AS EXPOSURE_RDL1 " + "\n");
            strSqlString.Append("     , ROUND(SUM(PR_COATING_RDL1)/" + strKpcs + ",0) AS PR_COATING_RDL1 " + "\n");

            strSqlString.Append("     , ROUND(SUM(SPUTTER_RDL1)/" + strKpcs + ",0) AS SPUTTER_RDL1 " + "\n");

            strSqlString.Append("     , ROUND(SUM(DESCUM_RCF)/" + strKpcs + ",0) AS DESCUM_RCF " + "\n");
            strSqlString.Append("     , ROUND(SUM(CURE_RCF)/" + strKpcs + ",0) AS CURE_RCF " + "\n");
            strSqlString.Append("     , ROUND(SUM(DEVELOP_INSP_RCF)/" + strKpcs + ",0) AS DEVELOP_INSP_RCF " + "\n");
            strSqlString.Append("     , ROUND(SUM(DEVELOP_RCF)/" + strKpcs + ",0) AS DEVELOP_RCF " + "\n");
            strSqlString.Append("     , ROUND(SUM(EXPOSURE_RCF)/" + strKpcs + ",0) AS EXPOSURE_RCF " + "\n");
            strSqlString.Append("     , ROUND(SUM(PI_COATING_RCF)/" + strKpcs + ",0) AS PI_COATING_RCF " + "\n");

            strSqlString.Append("     , ROUND(SUM(I_STOCK)/" + strKpcs + ",0) AS I_STOCK " + "\n");
            strSqlString.Append("     , ROUND(SUM(IQC)/" + strKpcs + ",0) AS IQC " + "\n");
            strSqlString.Append("     , ROUND(SUM(HMK2B)/" + strKpcs + ",0) AS HMK2B " + "\n");

            strSqlString.Append("     , ROUND(SUM(WIP_DEF)/" + strKpcs + ",0) AS WIP_DEF " + "\n");
            strSqlString.Append("  FROM (  " + "\n");
            strSqlString.Append("        SELECT " + QueryCond3 + ", GUBUN " + "\n");
            strSqlString.Append("             , MAX(PLAN) AS PLAN " + "\n");
            strSqlString.Append("             , MAX(SHP) AS SHP " + "\n");
            strSqlString.Append("             , MAX(DEF) AS DEF " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, '완제품창고', SUM_DEF, 0)) AS 완제품창고 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PACKING', SUM_DEF, 0)) AS PACKING " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'OGI', SUM_DEF, 0)) AS OGI " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'AVI', SUM_DEF, 0)) AS AVI " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SORT1', SUM_DEF, 0)) AS SORT1 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'FINAL_INSP', SUM_DEF, 0)) AS FINAL_INSP " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'REFLOW_BUMP', SUM_DEF, 0)) AS REFLOW_BUMP " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BALL_MOUNT_BUMP', SUM_DEF, 0)) AS BALL_MOUNT_BUMP " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'TI_ETCH_BUMP', SUM_DEF, 0)) AS TI_ETCH_BUMP " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'CU_ETCH_BUMP', SUM_DEF, 0)) AS CU_ETCH_BUMP " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PR_STRIP_BUMP', SUM_DEF, 0)) AS PR_STRIP_BUMP " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SN_AG_PLATING_BUMP', SUM_DEF, 0)) AS SN_AG_PLATING_BUMP " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'NI_PLATING_BUMP', SUM_DEF, 0)) AS NI_PLATING_BUMP " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'CU_PLATING_BUMP', SUM_DEF, 0)) AS CU_PLATING_BUMP " + "\n");

            //bump photo
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DESCUM_BUMP', SUM_DEF, 0)) AS DESCUM_BUMP " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'MEASURING_INSP_BUMP', SUM_DEF, 0)) AS MEASURING_INSP_BUMP " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DEVELOP_BUMP', SUM_DEF, 0)) AS DEVELOP_BUMP " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'EXPOSURE_BUMP', SUM_DEF, 0)) AS EXPOSURE_BUMP " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PR_COATING_BUMP', SUM_DEF, 0)) AS PR_COATING_BUMP " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SPUTTER_BUMP', SUM_DEF, 0)) AS SPUTTER_BUMP " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DESCUM_PSV3', SUM_DEF, 0)) AS DESCUM_PSV3 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DEVELOP_INSP_PSV3', SUM_DEF, 0)) AS DEVELOP_INSP_PSV3 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DEVELOP_PSV3', SUM_DEF, 0)) AS DEVELOP_PSV3 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'EXPOSURE_PSV3', SUM_DEF, 0)) AS EXPOSURE_PSV3 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PI_COATING_PSV3', SUM_DEF, 0)) AS PI_COATING_PSV3 " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DESCUM_RDL3E', SUM_DEF, 0)) AS DESCUM_RDL3E " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'TI_ETCH_RDL3', SUM_DEF, 0)) AS TI_ETCH_RDL3 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'CU_ETCH_RDL3', SUM_DEF, 0)) AS CU_ETCH_RDL3 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PR_STRIP_RDL3', SUM_DEF, 0)) AS PR_STRIP_RDL3 " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'CU_PLATING_RDL3', SUM_DEF, 0)) AS CU_PLATING_RDL3 " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DESCUM_RDL3P', SUM_DEF, 0)) AS DESCUM_RDL3P " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'MEASURING_INSP_RDL3', SUM_DEF, 0)) AS MEASURING_INSP_RDL3 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DEVELOP_RDL3', SUM_DEF, 0)) AS DEVELOP_RDL3 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'EXPOSURE_RDL3', SUM_DEF, 0)) AS EXPOSURE_RDL3 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PR_COATING_RDL3', SUM_DEF, 0)) AS PR_COATING_RDL3 " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SPUTTER_RDL3', SUM_DEF, 0)) AS SPUTTER_RDL3 " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DESCUM_PSV2', SUM_DEF, 0)) AS DESCUM_PSV2 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'CURE_PSV2', SUM_DEF, 0)) AS CURE_PSV2 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DEVELOP_INSP_PSV2', SUM_DEF, 0)) AS DEVELOP_INSP_PSV2 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DEVELOP_PSV2', SUM_DEF, 0)) AS DEVELOP_PSV2 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'EXPOSURE_PSV2', SUM_DEF, 0)) AS EXPOSURE_PSV2 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PI_COATING_PSV2', SUM_DEF, 0)) AS PI_COATING_PSV2 " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DESCUM_RDL2E', SUM_DEF, 0)) AS DESCUM_RDL2E " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'TI_ETCH_RDL2', SUM_DEF, 0)) AS TI_ETCH_RDL2 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'CU_ETCH_RDL2', SUM_DEF, 0)) AS CU_ETCH_RDL2 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PR_STRIP_RDL2', SUM_DEF, 0)) AS PR_STRIP_RDL2 " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'CU_PLATING_RDL2', SUM_DEF, 0)) AS CU_PLATING_RDL2 " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DESCUM_RDL2P', SUM_DEF, 0)) AS DESCUM_RDL2P " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DEVELOP_RDL2', SUM_DEF, 0)) AS DEVELOP_RDL2 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'MEASURING_INSP_RDL2', SUM_DEF, 0)) AS MEASURING_INSP_RDL2 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'EXPOSURE_RDL2', SUM_DEF, 0)) AS EXPOSURE_RDL2 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PR_COATING_RDL2', SUM_DEF, 0)) AS PR_COATING_RDL2 " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SPUTTER_RDL2', SUM_DEF, 0)) AS SPUTTER_RDL2 " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DESCUM_PSV1', SUM_DEF, 0)) AS DESCUM_PSV1 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'CURE_PSV1', SUM_DEF, 0)) AS CURE_PSV1 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DEVELOP_INSP_PSV1', SUM_DEF, 0)) AS DEVELOP_INSP_PSV1 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DEVELOP_PSV1', SUM_DEF, 0)) AS DEVELOP_PSV1 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'EXPOSURE_PSV1', SUM_DEF, 0)) AS EXPOSURE_PSV1 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PI_COATING_PSV1', SUM_DEF, 0)) AS PI_COATING_PSV1 " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DESCUM_RDL1E', SUM_DEF, 0)) AS DESCUM_RDL1E " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'TI_ETCH_RDL1', SUM_DEF, 0)) AS TI_ETCH_RDL1 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'CU_ETCH_RDL1', SUM_DEF, 0)) AS CU_ETCH_RDL1 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PR_STRIP_RDL1', SUM_DEF, 0)) AS PR_STRIP_RDL1 " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'CU_PLATING_RDL1', SUM_DEF, 0)) AS CU_PLATING_RDL1 " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DESCUM_RDL1P', SUM_DEF, 0)) AS DESCUM_RDL1P " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'MEASURING_INSP_RDL1', SUM_DEF, 0)) AS MEASURING_INSP_RDL1 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DEVELOP_RDL1', SUM_DEF, 0)) AS DEVELOP_RDL1 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'EXPOSURE_RDL1', SUM_DEF, 0)) AS EXPOSURE_RDL1 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PR_COATING_RDL1', SUM_DEF, 0)) AS PR_COATING_RDL1 " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SPUTTER_RDL1', SUM_DEF, 0)) AS SPUTTER_RDL1 " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DESCUM_RCF', SUM_DEF, 0)) AS DESCUM_RCF " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'CURE_RCF', SUM_DEF, 0)) AS CURE_RCF " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DEVELOP_INSP_RCF', SUM_DEF, 0)) AS DEVELOP_INSP_RCF " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'DEVELOP_RCF', SUM_DEF, 0)) AS DEVELOP_RCF " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'EXPOSURE_RCF', SUM_DEF, 0)) AS EXPOSURE_RCF " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PI_COATING_RCF', SUM_DEF, 0)) AS PI_COATING_RCF " + "\n");

            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'I-STOCK', SUM_DEF, 0)) AS I_STOCK " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'IQC', SUM_DEF, 0)) AS IQC " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK2B', SUM_DEF, 0)) AS HMK2B " + "\n");
            strSqlString.Append("             , MAX(WIP_TTL) AS WIP_TTL " + "\n");
            strSqlString.Append("             , MAX(DEF) - MAX(WIP_TTL) AS WIP_DEF " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT " + QueryCond3 + " " + "\n");
            strSqlString.Append("                     , OPER_GRP_1 " + "\n");
            strSqlString.Append("                     , DECODE(SEQ, 1, 'WIP', 2, '설비대수', 3, 'CAPA현황', 4, '당일 실적', 5, 'D0 잔량', 6, 'D1 잔량', 7, 'D2 잔량', 8, '당주 잔량', 9, '차주 잔량', 10, '월간 잔량') AS GUBUN " + "\n");
            strSqlString.Append("                     , SUM(DECODE(SEQ, 1, 0, 2, 0, 3, 0, 4, 0, 5, D0_PLAN, 6, D1_PLAN, 7, D2_PLAN, 8, WEEK_PLAN, 9, WEEK2_PLAN, 10, MON_PLAN)) AS PLAN  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(SEQ, 1, 0, 2, 0, 3, 0, 4, 0, 5, SHP_TODAY, 6, SHP_D1, 7, SHP_D2, 8, SHP_WEEK, 9, SHP2_WEEK, 10, SHP_MONTH)) AS SHP " + "\n");
            strSqlString.Append("                     , SUM(DECODE(SEQ, 1, 0, 2, 0, 3, 0, 4, 0, 5, D0_DEF, 6, D1_DEF, 7, D2_DEF, 8, WEEK_DEF, 9, WEEK2_DEF, 10, MONTH_DEF)) AS DEF " + "\n");
            
            strSqlString.Append("                     , SUM(DECODE(SEQ, 1, WIP_QTY, 2, RES_CNT, 3, RES_CAPA, 4, BUMP_END_QTY, 5, D0_SUM_QUANTITY, 6, D1_SUM_QUANTITY, 7, D2_SUM_QUANTITY, 8, WEEK_SUM_QUANTITY, 9, WEEK2_SUM_QUANTITY, 10, MONTH_SUM_QUANTITY)) AS SUM_DEF " + "\n");
            strSqlString.Append("                     , MAX(DECODE(SEQ, 1, 0, 2, 0, 3, 0, 4, 0, WIP_TTL)) AS WIP_TTL " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT " + QueryCond3 + ", OPER_GRP_1 " + "\n");
            strSqlString.Append("                             , SUM(NVL(D0_PLAN,0)) AS D0_PLAN, SUM(NVL(SHP_TODAY,0)) AS SHP_TODAY, SUM(NVL(D0_DEF,0)) AS D0_DEF " + "\n");
            strSqlString.Append("                             , SUM(NVL(D1_PLAN,0)) AS D1_PLAN, SUM(NVL(SHP_D1,0)) AS SHP_D1, SUM(NVL(D1_DEF,0)) AS D1_DEF " + "\n");
            strSqlString.Append("                             , SUM(NVL(D2_PLAN,0)) AS D2_PLAN, SUM(NVL(SHP_D2,0)) AS SHP_D2, SUM(NVL(D2_DEF,0)) AS D2_DEF " + "\n");
            strSqlString.Append("                             , SUM(NVL(WEEK_PLAN,0)) AS WEEK_PLAN, SUM(NVL(SHP_WEEK,0)) AS SHP_WEEK, SUM(NVL(WEEK_DEF,0)) AS WEEK_DEF " + "\n");
            strSqlString.Append("                             , SUM(NVL(WEEK2_PLAN,0)) AS WEEK2_PLAN, SUM(NVL(SHP2_WEEK,0)) AS SHP2_WEEK, SUM(NVL(WEEK2_DEF,0)) AS WEEK2_DEF " + "\n");
            strSqlString.Append("                             , SUM(NVL(MON_PLAN,0)) AS MON_PLAN, SUM(NVL(SHP_MONTH,0)) AS SHP_MONTH, SUM(NVL(MONTH_DEF,0)) AS MONTH_DEF " + "\n");            
            strSqlString.Append("                             , SUM(NVL(BUMP_END_QTY,0)) AS BUMP_END_QTY " + "\n");
            strSqlString.Append("                             , SUM(NVL(WIP_QTY,0)) AS WIP_QTY " + "\n");
            strSqlString.Append("                             , SUM(SUM(WIP_QTY)) OVER(PARTITION BY " + QueryCond3 + ") AS WIP_TTL " + "\n");
            strSqlString.Append("                             , SUM(RES_CNT) AS RES_CNT " + "\n");
            strSqlString.Append("                             , SUM(RES_CAPA) AS RES_CAPA " + "\n");
            strSqlString.Append("                             , SUM(NVL(D0_DEF-WIP_SUM_QUANTITY+WIP_QTY,0)) AS D0_SUM_QUANTITY " + "\n");
            strSqlString.Append("                             , SUM(NVL(D1_DEF-WIP_SUM_QUANTITY+WIP_QTY,0)) AS D1_SUM_QUANTITY " + "\n");
            strSqlString.Append("                             , SUM(NVL(D2_DEF-WIP_SUM_QUANTITY+WIP_QTY,0)) AS D2_SUM_QUANTITY " + "\n");
            strSqlString.Append("                             , SUM(NVL(WEEK_DEF-WIP_SUM_QUANTITY+WIP_QTY,0)) AS WEEK_SUM_QUANTITY " + "\n");
            strSqlString.Append("                             , SUM(NVL(WEEK2_DEF-WIP_SUM_QUANTITY+WIP_QTY,0)) AS WEEK2_SUM_QUANTITY " + "\n");
            strSqlString.Append("                             , SUM(NVL(MONTH_DEF-WIP_SUM_QUANTITY+WIP_QTY,0)) AS MONTH_SUM_QUANTITY " + "\n");
            strSqlString.Append("                          FROM ( " + "\n");

            strSqlString.Append("                                SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_CMF_14, MAT.MAT_CMF_2, MAT.MAT_CMF_3, MAT.MAT_CMF_4, MAT.MAT_ID, MAT.CONV_MAT_ID, MAT.OPER_GRP_1 " + "\n");
            
            strSqlString.Append("                                     , A.D0_PLAN, A.SHP_TODAY, A.D0_DEF " + "\n");
            strSqlString.Append("                                     , A.D1_PLAN, A.SHP_D1, A.D1_DEF " + "\n");
            strSqlString.Append("                                     , A.D2_PLAN, A.SHP_D2, A.D2_DEF " + "\n");
            strSqlString.Append("                                     , A.WEEK_PLAN, A.SHP_WEEK, A.WEEK_DEF " + "\n");
            strSqlString.Append("                                     , A.WEEK2_PLAN, A.SHP2_WEEK, A.WEEK2_DEF " + "\n");
            strSqlString.Append("                                     , A.MON_PLAN, A.SHP_MONTH, A.MONTH_DEF " + "\n");
            strSqlString.Append("                                     , A.D0_ORI_PLAN " + "\n");
            strSqlString.Append("                                     , NVL(B.BUMP_END_QTY,0) AS BUMP_END_QTY " + "\n");
            strSqlString.Append("                                     , MAX(NVL(B.BUMP_END_QTY,0)) OVER(PARTITION BY " + QueryCond2 + ") AS CHK_ASSY_END_QTY " + "\n");

            strSqlString.Append("                                     , NVL(WIP.QTY,0) AS WIP_QTY " + "\n");
            strSqlString.Append("                                     , RES_CNT " + "\n");
            strSqlString.Append("                                     , RES_CAPA " + "\n");
            strSqlString.Append("                                     , SUM(NVL(WIP.QTY,0)) OVER(PARTITION BY A.MAT_ID ORDER BY MAT.MAT_ID, DECODE(MAT.OPER_GRP_1, '완제품창고', 1, 'PACKING', 2, 'OGI', 3, 'AVI', 4, 'SORT1', 5, 'FINAL_INSP', 6, 'REFLOW_BUMP', 7, 'BALL_MOUNT_BUMP', 8, 'TI_ETCH_BUMP', 9, 'CU_ETCH_BUMP', 10, 'PR_STRIP_BUMP', 11, 'SN_AG_PLATING_BUMP', 12 " + "\n");
            strSqlString.Append("                                                                                                                                , 'NI_PLATING_BUMP', 13, 'CU_PLATING_BUMP', 14, 'DESCUM_BUMP', 15, 'MEASURING_INSP_BUMP', 16, 'DEVELOP_BUMP', 17, 'EXPOSURE_BUMP', 18, 'PR_COATING_BUMP', 19, 'SPUTTER_BUMP', 20, 'DESCUM_PSV3', 21, 'DEVELOP_INSP_PSV3', 22, 'DEVELOP_PSV3', 23, 'EXPOSURE_PSV3', 24 " + "\n");
            strSqlString.Append("                                                                                                                                , 'PI_COATING_PSV3', 25, 'DESCUM_RDL3E', 26, 'TI_ETCH_RDL3', 27, 'CU_ETCH_RDL3', 28, 'PR_STRIP_RDL3', 29, 'CU_PLATING_RDL3', 30, 'DESCUM_RDL3P', 31, 'MEASURING_INSP_RDL3', 32, 'DEVELOP_RDL3', 33, 'EXPOSURE_RDL3', 34, 'PR_COATING_RDL3', 35, 'SPUTTER_RDL3', 36 " + "\n");
            strSqlString.Append("                                                                                                                                , 'DESCUM_PSV2', 37, 'CURE_PSV2', 38, 'DEVELOP_INSP_PSV2', 39, 'DEVELOP_PSV2', 40, 'EXPOSURE_PSV2', 41, 'PI_COATING_PSV2', 42, 'DESCUM_RDL2E', 43, 'TI_ETCH_RDL2', 44, 'CU_ETCH_RDL2', 45, 'PR_STRIP_RDL2', 46, 'CU_PLATING_RDL2', 47, 'DESCUM_RDL2P', 48 " + "\n");
            strSqlString.Append("                                                                                                                                , 'MEASURING_INSP_RDL2', 49, 'DEVELOP_RDL2', 50, 'EXPOSURE_RDL2', 51, 'PR_COATING_RDL2', 52, 'SPUTTER_RDL2', 53, 'DESCUM_PSV1', 54, 'CURE_PSV1', 55, 'DEVELOP_INSP_PSV1', 56, 'DEVELOP_PSV1', 57, 'EXPOSURE_PSV1', 58, 'PI_COATING_PSV1', 59, 'DESCUM_RDL1E', 60 " + "\n");
            strSqlString.Append("                                                                                                                                , 'TI_ETCH_RDL1', 61, 'CU_ETCH_RDL1', 62, 'PR_STRIP_RDL1', 63, 'CU_PLATING_RDL1', 64, 'DESCUM_RDL1P', 65, 'MEASURING_INSP_RDL1', 66, 'DEVELOP_RDL1', 67, 'EXPOSURE_RDL1', 68, 'PR_COATING_RDL1', 69, 'SPUTTER_RDL1', 70, 'DESCUM_RCF', 71, 'CURE_RCF', 72 " + "\n");
            strSqlString.Append("                                                                                                                                , 'DEVELOP_INSP_RCF', 73, 'DEVELOP_RCF', 74, 'EXPOSURE_RCF', 75, 'PI_COATING_RCF', 76, 'I_STOCK', 77, 'IQC', 78, 'HMK2B', 79, 80)) AS WIP_SUM_QUANTITY " + "\n");
            strSqlString.Append("                                  FROM ( " + "\n");

            strSqlString.Append("                                        SELECT MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, MAT_CMF_14, MAT_CMF_2, MAT_CMF_3, MAT_CMF_4, MAT_ID, OPER_GRP_1 " + "\n");
            
            //strSqlString.Append("                                             , CASE WHEN MAT_GRP_1 = 'SE' AND MAT_GRP_9 = 'MEMORY' THEN 'SEK_________-___' || SUBSTR(MAT_ID, -3) " + "\n");
            //strSqlString.Append("                                                    WHEN MAT_GRP_1 = 'HX' THEN MAT_CMF_10 " + "\n");
            //strSqlString.Append("                                                    ELSE MAT_ID " + "\n");
            //strSqlString.Append("                                                END CONV_MAT_ID " + "\n");
            strSqlString.Append("                                             , MAT_ID AS CONV_MAT_ID " + "\n");


            strSqlString.Append("                                          FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                                             , ( " + "\n");

            strSqlString.Append("                                                SELECT DECODE(LEVEL, 1, '완제품창고', 2, 'PACKING', 3, 'OGI', 4, 'AVI', 5, 'SORT1', 6, 'FINAL_INSP', 7, 'REFLOW_BUMP', 8, 'BALL_MOUNT_BUMP', 9, 'TI_ETCH_BUMP', 10, 'CU_ETCH_BUMP', 11, 'PR_STRIP_BUMP', 12, 'SN_AG_PLATING_BUMP' " + "\n");
            strSqlString.Append("                                                                   , 13, 'NI_PLATING_BUMP', 14, 'CU_PLATING_BUMP', 15, 'DESCUM_BUMP', 16, 'MEASURING_INSP_BUMP', 17, 'DEVELOP_BUMP', 18, 'EXPOSURE_BUMP', 19, 'PR_COATING_BUMP', 20, 'SPUTTER_BUMP', 21, 'DESCUM_PSV3', 22, 'DEVELOP_INSP_PSV3', 23, 'DEVELOP_PSV3', 24, 'EXPOSURE_PSV3' " + "\n");
            strSqlString.Append("                                                                   , 25, 'PI_COATING_PSV3', 26, 'DESCUM_RDL3E', 27, 'TI_ETCH_RDL3', 28, 'CU_ETCH_RDL3', 29, 'PR_STRIP_RDL3', 30, 'CU_PLATING_RDL3', 31, 'DESCUM_RDL3P', 32, 'MEASURING_INSP_RDL3', 33, 'DEVELOP_RDL3', 34, 'EXPOSURE_RDL3', 35, 'PR_COATING_RDL3', 36, 'SPUTTER_RDL3' " + "\n");
            strSqlString.Append("                                                                   , 37, 'DESCUM_PSV2', 38, 'CURE_PSV2', 39, 'DEVELOP_INSP_PSV2', 40, 'DEVELOP_PSV2', 41, 'EXPOSURE_PSV2', 42, 'PI_COATING_PSV2', 43, 'DESCUM_RDL2E', 44, 'TI_ETCH_RDL2', 45, 'CU_ETCH_RDL2', 46, 'PR_STRIP_RDL2', 47, 'CU_PLATING_RDL2', 48, 'DESCUM_RDL2P' " + "\n");
            strSqlString.Append("                                                                   , 49, 'MEASURING_INSP_RDL2', 50, 'DEVELOP_RDL2', 51, 'EXPOSURE_RDL2', 52, 'PR_COATING_RDL2', 53, 'SPUTTER_RDL2', 54, 'DESCUM_PSV1', 55, 'CURE_PSV1', 56, 'DEVELOP_INSP_PSV1', 57, 'DEVELOP_PSV1', 58, 'EXPOSURE_PSV1', 59, 'PI_COATING_PSV1', 60, 'DESCUM_RDL1E' " + "\n");
            strSqlString.Append("                                                                   , 61, 'TI_ETCH_RDL1', 62, 'CU_ETCH_RDL1', 63, 'PR_STRIP_RDL1', 64, 'CU_PLATING_RDL1', 65, 'DESCUM_RDL1P', 66, 'MEASURING_INSP_RDL1', 67, 'DEVELOP_RDL1', 68, 'EXPOSURE_RDL1', 69, 'PR_COATING_RDL1', 70, 'SPUTTER_RDL1', 71, 'DESCUM_RCF', 72, 'CURE_RCF' " + "\n");
            strSqlString.Append("                                                                   , 73, 'DEVELOP_INSP_RCF', 74, 'DEVELOP_RCF', 75, 'EXPOSURE_RCF', 76, 'PI_COATING_RCF', 77, 'I_STOCK', 78, 'IQC', 79, 'HMK2B' " + "\n");
                        
            strSqlString.Append("                                                             ) OPER_GRP_1 " + "\n");

            strSqlString.Append("                                                  FROM DUAL CONNECT BY LEVEL <= 79 " + "\n");
            strSqlString.Append("                                               ) " + "\n");
            strSqlString.Append("                                          WHERE 1=1 " + "\n");
            strSqlString.Append("                                            AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                                            AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                                            AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                                            AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

            //if (ckbCOB.Checked == true)
            //{
            //    strSqlString.Append("                                            AND MAT_GRP_10 <> 'COB' " + "\n");
            //}

            #region 상세 조회에 따른 SQL문 생성
            if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

            if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

            if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

            if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

            if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

            if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

            if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

            if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

            if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

            if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

            if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

            if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                strSqlString.AppendFormat("                                            AND MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                        
            #endregion

            strSqlString.Append("                                       ) MAT " + "\n");
            strSqlString.Append("                                     , ( " + "\n");
            strSqlString.Append("                                        SELECT MAT.MAT_ID " + "\n");
            strSqlString.Append("                                             , SUM(NVL(D0_PLAN, 0) + (NVL(WEEK1_TTL, 0) - NVL(SHP_WEEK_TTL, 0))) AS D0_PLAN " + "\n");
            strSqlString.Append("                                             , SUM(NVL(D0_PLAN, 0)) AS D0_ORI_PLAN " + "\n");
            strSqlString.Append("                                             , SUM(NVL(SHP_TODAY, 0)) AS SHP_TODAY " + "\n");
            strSqlString.Append("                                             , SUM(NVL(D0_PLAN, 0) + (NVL(WEEK1_TTL, 0) - NVL(SHP_WEEK_TTL, 0)) - NVL(SHP_TODAY, 0)) AS D0_DEF " + "\n");
            strSqlString.Append("                                             , SUM(NVL(D1_PLAN, 0)) AS D1_PLAN " + "\n");
            strSqlString.Append("                                             , 0 AS SHP_D1 " + "\n");
            strSqlString.Append("                                             , SUM(NVL(D1_PLAN, 0) + NVL(D0_PLAN, 0) + (NVL(WEEK1_TTL, 0) - NVL(SHP_WEEK_TTL, 0)) - NVL(SHP_TODAY, 0)) AS D1_DEF " + "\n");
            strSqlString.Append("                                             , SUM(NVL(D2_PLAN, 0)) AS D2_PLAN " + "\n");
            strSqlString.Append("                                             , 0 AS SHP_D2 " + "\n");
            strSqlString.Append("                                             , SUM(NVL(D2_PLAN, 0) + NVL(D1_PLAN, 0) + NVL(D0_PLAN, 0) + (NVL(WEEK1_TTL, 0) - NVL(SHP_WEEK_TTL, 0)) - NVL(SHP_TODAY, 0)) AS D2_DEF " + "\n");
            strSqlString.Append("                                             , SUM(NVL(WEEK1_PLAN, 0)) AS WEEK_PLAN " + "\n");
            strSqlString.Append("                                             , SUM(NVL(SHP_WEEK, 0)) AS SHP_WEEK " + "\n");
            strSqlString.Append("                                             , SUM(NVL(WEEK1_PLAN, 0) - NVL(SHP_WEEK, 0)) AS WEEK_DEF " + "\n");
            strSqlString.Append("                                             , SUM(NVL(WEEK2_PLAN, 0)) AS WEEK2_PLAN " + "\n");
            strSqlString.Append("                                             , 0 AS SHP2_WEEK " + "\n");
            strSqlString.Append("                                             , SUM(NVL(WEEK2_PLAN, 0) + NVL(WEEK1_PLAN, 0) - NVL(SHP_WEEK, 0)) AS WEEK2_DEF " + "\n");

            //MAT.MAT_GRP_3 IN ('COB') ? >> 삭제 / MON.RESV_FIELD1 컬럼수정 
            //strSqlString.Append("                                             , SUM(NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(MON.RESV_FIELD8/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0) " + "\n");
            //strSqlString.Append("                                                    ELSE MON.RESV_FIELD8 " + "\n");
            //strSqlString.Append("                                                END,0)) MON_PLAN " + "\n");
            strSqlString.Append("                                             , SUM(NVL(MON.RESV_FIELD8,0)) AS MON_PLAN  " + "\n");
            
            strSqlString.Append("                                             , SUM(NVL(SHP_MONTH, 0)) AS SHP_MONTH " + "\n");

            //strSqlString.Append("                                             , SUM(NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(NVL(MON.RESV_FIELD8,0)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0)-NVL(SHP_MONTH, 0) " + "\n");
            //strSqlString.Append("                                                    ELSE NVL(MON.RESV_FIELD8, 0)-NVL(SHP_MONTH, 0) " + "\n");  //RESV_FIELD1
            //strSqlString.Append("                                                END,0)) MONTH_DEF " + "\n");
            strSqlString.Append("                                             , SUM(NVL(NVL(MON.RESV_FIELD8, 0)-NVL(SHP_MONTH, 0) ,0)) AS MONTH_DEF  " + "\n");

            strSqlString.Append("                                          FROM MWIPMATDEF MAT " + "\n");
            strSqlString.Append("                                             , ( " + "\n");
            strSqlString.Append("                                                SELECT FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                                                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D0_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D0_QTY + D1_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D0_QTY + D1_QTY + D2_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY " + "\n");
            strSqlString.Append("                                                                ELSE 0 " + "\n");
            strSqlString.Append("                                                           END) AS WEEK1_TTL " + "\n");
            strSqlString.Append("                                                     , SUM(W1_QTY) AS WEEK1_PLAN " + "\n");
            strSqlString.Append("                                                     , SUM(W2_QTY) AS WEEK2_PLAN " + "\n");
            strSqlString.Append("                                                     , SUM(NVL(W1_QTY,0) + NVL(W2_QTY,0)) AS WEEK_TTL " + "\n");
            strSqlString.Append("                                                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D0_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D1_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D2_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D3_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D4_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D5_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D6_QTY " + "\n");
            strSqlString.Append("                                                                ELSE 0 " + "\n");
            strSqlString.Append("                                                           END) AS D0_PLAN " + "\n");
            strSqlString.Append("                                                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D1_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D2_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D3_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D4_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D5_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D6_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D7_QTY " + "\n");
            strSqlString.Append("                                                                ELSE 0 " + "\n");
            strSqlString.Append("                                                           END) AS D1_PLAN " + "\n");
            strSqlString.Append("                                                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 7 THEN D2_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 1 THEN D3_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 2 THEN D4_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 3 THEN D5_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 4 THEN D6_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 5 THEN D7_QTY " + "\n");
            strSqlString.Append("                                                                WHEN TO_CHAR(TO_DATE('" + Today + "', 'YYYYMMDD'), 'D') = 6 THEN D8_QTY " + "\n");
            strSqlString.Append("                                                                ELSE 0 " + "\n");
            strSqlString.Append("                                                           END) AS D2_PLAN " + "\n");
            strSqlString.Append("                                                  FROM ( " + "\n");
            strSqlString.Append("                                                        SELECT FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D0_QTY, 0)) AS D0_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D1_QTY, 0)) AS D1_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D2_QTY, 0)) AS D2_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D3_QTY, 0)) AS D3_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D4_QTY, 0)) AS D4_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D5_QTY, 0)) AS D5_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D6_QTY, 0)) AS D6_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D0_QTY, 0)) AS D7_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D1_QTY, 0)) AS D8_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D2_QTY, 0)) AS D9_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D3_QTY, 0)) AS D10_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D4_QTY, 0)) AS D11_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D5_QTY, 0)) AS D12_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D6_QTY, 0)) AS D13_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', WW_QTY, 0)) AS W1_QTY  " + "\n");
            strSqlString.Append("                                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', WW_QTY, 0)) AS W2_QTY  " + "\n");
            
            strSqlString.Append("                                                          FROM RWIPPLNWEK  " + "\n");
            strSqlString.Append("                                                         WHERE 1=1  " + "\n");
            strSqlString.Append("                                                           AND FACTORY  "+ cdvFactory.SelectedValueToQueryString + "\n");
            //strSqlString.Append("                                                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                                                           AND GUBUN = '3'  " + "\n");
            strSqlString.Append("                                                           AND PLAN_WEEK IN ('" + FindWeek_SOP_A.ThisWeek + "','" + FindWeek_SOP_A.NextWeek + "') " + "\n");
            strSqlString.Append("                                                         GROUP BY FACTORY, MAT_ID  " + "\n");
            strSqlString.Append("                                                       )  " + "\n");
            strSqlString.Append("                                                 GROUP BY FACTORY, MAT_ID  " + "\n");
            strSqlString.Append("                                               ) PLN " + "\n");
            strSqlString.Append("                                             , (  " + "\n");

            strSqlString.Append("                                                SELECT FACTORY,MAT_ID, RESV_FIELD8  " + "\n");
            strSqlString.Append("                                                  FROM (  " + "\n");                        
            strSqlString.Append("                                                        SELECT FACTORY, MAT_ID, SUM(RESV_FIELD8) AS RESV_FIELD8   " + "\n");
            strSqlString.Append("                                                          FROM (  " + "\n");
            strSqlString.Append("                                                                SELECT FACTORY, MAT_ID, SUM(TO_NUMBER(DECODE(RESV_FIELD8,' ',0,RESV_FIELD8))) AS RESV_FIELD8  " + "\n");
            strSqlString.Append("                                                                  FROM CWIPPLNMON  " + "\n");
            strSqlString.Append("                                                                 WHERE 1=1  " + "\n");
            strSqlString.Append("                                                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                                                                   AND PLAN_MONTH = '" + sMonth + "' " + "\n");
            strSqlString.Append("                                                                   AND (TO_NUMBER(TRIM(RESV_FIELD7)) > 0 OR TO_NUMBER(TRIM(RESV_FIELD8)) > 0)  " + "\n");            
            strSqlString.Append("                                                                 GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                                                               )  " + "\n");
            strSqlString.Append("                                                         GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                                                       ) " + "\n");
            strSqlString.Append("                                               ) MON " + "\n");
            strSqlString.Append("                                             , (  " + "\n");

            // 2014-05-07-임종우 : 금일일자이면서 시간조회가 현재가 아니면 스냅샷 테이블 데이터 조회
            // 2014-06-27-임종우 : 어제일자도 스냅샷 테이블 데이터 조회 가능하도록 변경
            #region 금일, 어제 시간별 스냅샷 데이터
            //if ((DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd") || DateTime.Now.AddDays(-1).ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd")) && cboTimeBase.Text != "현재")
            if ((DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd") || DateTime.Now.AddDays(-1).ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd")) && cboTimeBase.SelectedIndex != 0)
            {
                strSqlString.Append("                                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                     , SUM(TODAY_SHP) AS SHP_TODAY " + "\n");
                strSqlString.Append("                                                     , SUM(WEEK_SHP) AS SHP_WEEK " + "\n");
                strSqlString.Append("                                                     , SUM(WEEK_Y_SHP) AS SHP_WEEK_TTL " + "\n");
                strSqlString.Append("                                                     , SUM(MON_SHP) AS SHP_MONTH " + "\n");
                strSqlString.Append("                                                  FROM RSHPTIMTMP " + "\n");
                strSqlString.Append("                                                 WHERE 1=1  " + "\n");
                strSqlString.Append("                                                   AND WORK_DATE = '" + Today + "'" + "\n");
                //strSqlString.Append("                                                   AND WORK_TIME = '" + cboTimeBase.Text.Replace("시", "") + "'" + "\n");
                strSqlString.Append("                                                   AND WORK_TIME = '" + cboTimeBase.Text.Substring(0, 2) + "'" + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                                                   AND LOT_TYPE LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                                                 GROUP BY MAT_ID " + "\n");
            }
            #endregion

            #region 기존 데이터
            else
            {
                //if (cdvTime.Text == "MIX")
                if (cdvTime.SelectedIndex == 0)
                {
                    strSqlString.Append("                                                SELECT MAT_ID  " + "\n");
                    strSqlString.Append("                                                     , SUM(DECODE(WORK_DATE, '" + Today + "', NVL(SHP_QTY_1, 0), 0)) AS SHP_TODAY  " + "\n");
                    strSqlString.Append("                                                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_WEEK  " + "\n");
                    strSqlString.Append("                                                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_WEEK_TTL  " + "\n");
                    strSqlString.Append("                                                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + sMonth + "01' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_MONTH  " + "\n");
                    strSqlString.Append("                                                  FROM VSUMWIPOUT " + "\n");
                    strSqlString.Append("                                                 WHERE 1=1  " + "\n");
                    strSqlString.Append("                                                   AND FACTORY  = '" + cdvFactory.Text + "'  " + "\n");
                    strSqlString.Append("                                                   AND LOT_TYPE = 'W'  " + "\n");
                    strSqlString.Append("                                                   AND CM_KEY_2 = 'PROD'  " + "\n");

                    if (cdvLotType.Text != "ALL")
                    {
                        strSqlString.Append("                                                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                    }


                    strSqlString.Append("                                                   AND MAT_ID NOT LIKE 'HX%'  " + "\n");
                    strSqlString.Append("                                                   AND WORK_DATE BETWEEN '" + sStartDate + "' AND '" + Today + "'  " + "\n");
                    strSqlString.Append("                                                 GROUP BY MAT_ID  " + "\n");
                    strSqlString.Append("                                                 UNION ALL  " + "\n");
                    strSqlString.Append("                                                SELECT MAT_ID  " + "\n");
                    strSqlString.Append("                                                     , SUM(DECODE(WORK_DATE, '" + Today + "', NVL(SHP_QTY_1, 0), 0)) AS SHP_TODAY  " + "\n");
                    strSqlString.Append("                                                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_WEEK  " + "\n");
                    strSqlString.Append("                                                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_WEEK_TTL  " + "\n");
                    strSqlString.Append("                                                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + sMonth + "01' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_MONTH  " + "\n");
                    strSqlString.Append("                                                  FROM VSUMWIPOUT_06 " + "\n");
                    strSqlString.Append("                                                 WHERE 1=1  " + "\n");
                    strSqlString.Append("                                                   AND FACTORY  = '" + cdvFactory.Text + "'  " + "\n");
                    strSqlString.Append("                                                   AND LOT_TYPE = 'W'  " + "\n");
                    strSqlString.Append("                                                   AND CM_KEY_2 = 'PROD'  " + "\n");

                    if (cdvLotType.Text != "ALL")
                    {
                        strSqlString.Append("                                                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                    }

                    strSqlString.Append("                                                   AND MAT_ID LIKE 'HX%'  " + "\n");
                    strSqlString.Append("                                                   AND WORK_DATE BETWEEN '" + sStartDate + "' AND '" + Today + "' " + "\n");
                    strSqlString.Append("                                                 GROUP BY MAT_ID " + "\n");
                }
                else
                {
                    strSqlString.Append("                                                SELECT MAT_ID  " + "\n");
                    strSqlString.Append("                                                     , SUM(DECODE(WORK_DATE, '" + Today + "', NVL(SHP_QTY_1, 0), 0)) AS SHP_TODAY  " + "\n");
                    strSqlString.Append("                                                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_WEEK  " + "\n");
                    strSqlString.Append("                                                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Yesterday + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_WEEK_TTL  " + "\n");
                    strSqlString.Append("                                                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + sMonth + "01' AND '" + Today + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_MONTH  " + "\n");
                    strSqlString.Append("                                                  FROM " + sTable_fac + "\n");
                    strSqlString.Append("                                                 WHERE 1=1  " + "\n");
                    strSqlString.Append("                                                   AND FACTORY  = '" + cdvFactory.Text + "'  " + "\n");
                    strSqlString.Append("                                                   AND CM_KEY_2 = 'PROD'  " + "\n");
                    strSqlString.Append("                                                   AND LOT_TYPE = 'W'  " + "\n");

                    if (cdvLotType.Text != "ALL")
                    {
                        strSqlString.Append("                                                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                    }

                    strSqlString.Append("                                                   AND WORK_DATE BETWEEN '" + sStartDate + "' AND '" + Today + "'  " + "\n");
                    strSqlString.Append("                                                 GROUP BY MAT_ID  " + "\n");
                }
            }
            #endregion

            strSqlString.Append("                                               ) SHP " + "\n");
            strSqlString.Append("                                         WHERE 1=1 " + "\n");
            strSqlString.Append("                                           AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                                           AND MAT.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                                           AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                                           AND MAT.MAT_ID = PLN.MAT_ID(+) " + "\n");
            strSqlString.Append("                                           AND MAT.MAT_ID = MON.MAT_ID(+) " + "\n");
            strSqlString.Append("                                           AND MAT.MAT_ID = SHP.MAT_ID(+) " + "\n");
            strSqlString.Append("                                         GROUP BY MAT.MAT_ID " + "\n");
            strSqlString.Append("                                         ORDER BY MAT.MAT_ID " + "\n");
            strSqlString.Append("                                       ) A " + "\n");
            strSqlString.Append("                                     , ( " + "\n");

            #region 금일, 어제 시간별 스냅샷 데이터
            //if ((DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd") || DateTime.Now.AddDays(-1).ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd")) && cboTimeBase.Text != "현재")
            if ((DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd") || DateTime.Now.AddDays(-1).ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd")) && cboTimeBase.SelectedIndex != 0)
            {
                strSqlString.Append("                                        SELECT MAT_ID " + "\n");
                strSqlString.Append("                                             , OPER_GRP AS OPER_GRP_1 " + "\n");
                strSqlString.Append("                                             , SUM(QTY) AS BUMP_END_QTY " + "\n");
                strSqlString.Append("                                          FROM RWIPTIMTMP " + "\n");
                strSqlString.Append("                                         WHERE 1=1  " + "\n");
                strSqlString.Append("                                           AND WORK_DATE = '" + Today + "'" + "\n");
                //strSqlString.Append("                                           AND WORK_TIME = '" + cboTimeBase.Text.Replace("시", "") + "'" + "\n");
                strSqlString.Append("                                           AND WORK_TIME = '" + cboTimeBase.Text.Substring(0, 2) + "'" + "\n");
                strSqlString.Append("                                           AND GUBUN = 'OUT' " + "\n");
                strSqlString.Append("                                           AND FUNCTION_NAME = 'PRD011015' " + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                                           AND LOT_TYPE LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                                         GROUP BY MAT_ID, OPER_GRP " + "\n");
            }
            #endregion

            #region 기존 데이터
            else
            {
                strSqlString.Append("                                        SELECT MAT_ID, OPER_GRP_1 " + "\n");
                //이름변경 BUMP_END_QTY
                strSqlString.Append("                                             , SUM(BUMP_END_QTY) AS BUMP_END_QTY " + "\n");
                strSqlString.Append("                                          FROM ( " + "\n");
                strSqlString.Append("                                                SELECT B.MAT_ID " + "\n");
                strSqlString.Append("                                                     , CASE WHEN OPER IN ('B0000') THEN 'HMK2B' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B0400') THEN 'IQC' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B0500') THEN 'I_STOCK' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B1100') THEN 'PI_COATING_RCF' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1200') THEN 'EXPOSURE_RCF' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1250') THEN 'DEVELOP_RCF' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1300') THEN 'DEVELOP_INSP_RCF' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1350') THEN 'CURE_RCF' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1450') THEN 'DESCUM_RCF' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1500') THEN 'SPUTTER_RDL1' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B1650') THEN 'PR_COATING_RDL1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1750') THEN 'EXPOSURE_RDL1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1800') THEN 'DEVELOP_RDL1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1850') THEN 'MEASURING_INSP_RDL1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1900') THEN 'DESCUM_RDL1P' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B2050') THEN 'CU_PLATING_RDL1' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B2150') THEN 'PR_STRIP_RDL1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B2250') THEN 'CU_ETCH_RDL1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B2350') THEN 'TI_ETCH_RDL1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B2450') THEN 'DESCUM_RDL1E' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B2650') THEN 'PI_COATING_PSV1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B2750') THEN 'EXPOSURE_PSV1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B2800') THEN 'DEVELOP_PSV1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B2850') THEN 'DEVELOP_INSP_PSV1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B2900') THEN 'CURE_PSV1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B3000') THEN 'DESCUM_PSV1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B3100') THEN 'SPUTTER_RDL2' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B3250') THEN 'PR_COATING_RDL2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B3350') THEN 'EXPOSURE_RDL2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B3400') THEN 'DEVELOP_RDL2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B3450') THEN 'MEASURING_INSP_RDL2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B3500') THEN 'DESCUM_RDL2P' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B3650') THEN 'CU_PLATING_RDL2' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B3750') THEN 'PR_STRIP_RDL2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B3850') THEN 'CU_ETCH_RDL2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B3950') THEN 'TI_ETCH_RDL2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B4050') THEN 'DESCUM_RDL2E' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B4250') THEN 'PI_COATING_PSV2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B4350') THEN 'EXPOSURE_PSV2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B4400') THEN 'DEVELOP_PSV2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B4450') THEN 'DEVELOP_INSP_PSV2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B4500') THEN 'CURE_PSV2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B4600') THEN 'DESCUM_PSV2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B4700') THEN 'SPUTTER_RDL3' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B4850') THEN 'PR_COATING_RDL3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B4950') THEN 'EXPOSURE_RDL3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B5000') THEN 'DEVELOP_RDL3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B5050') THEN 'MEASURING_INSP_RDL3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B5100') THEN 'DESCUM_RDL3P' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B5250') THEN 'CU_PLATING_RDL3' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B5350') THEN 'PR_STRIP_RDL3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B5450') THEN 'CU_ETCH_RDL3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B5550') THEN 'TI_ETCH_RDL3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B5650') THEN 'DESCUM_RDL3E' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B5850') THEN 'PI_COATING_PSV3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B5950') THEN 'EXPOSURE_PSV3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B6000') THEN 'DEVELOP_PSV3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B6050') THEN 'DEVELOP_INSP_PSV3' " + "\n");                
                strSqlString.Append("                                                            WHEN OPER IN ('B6200') THEN 'DESCUM_PSV3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B6300') THEN 'SPUTTER_BUMP' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B6450') THEN 'PR_COATING_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B6550') THEN 'EXPOSURE_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B6650') THEN 'DEVELOP_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B6700') THEN 'MEASURING_INSP_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B6750') THEN 'DESCUM_BUMP' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B6900') THEN 'CU_PLATING_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B7050') THEN 'NI_PLATING_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B7100') THEN 'SN_AG_PLATING_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B7200') THEN 'PR_STRIP_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B7300') THEN 'CU_ETCH_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B7400') THEN 'TI_ETCH_BUMP' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B7500') THEN 'BALL_MOUNT_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B7600') THEN 'REFLOW_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B7750') THEN 'FINAL_INSP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B7800') THEN 'SORT1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B7900') THEN 'AVI' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B9000') THEN 'OGI' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B9100') THEN 'PACKING' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('BZ010') THEN '완제품창고' " + "\n");

                //strSqlString.Append("                                                     , CASE WHEN OPER IN ('A0000', 'A000N') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'HMK2A' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0040') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'BG' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0200', 'A0230') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'SAW' " + "\n");
                ////strSqlString.Append("                                                                    WHEN OPER IN ('A0340', 'A0350', 'A0360', 'A0390', 'A0397', 'A0395') THEN 'SP' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0400', 'A0401', 'A0333') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA1' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0402') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA2' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0403') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA3' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0404') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA4' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0405') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA5' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0406') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA6' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0407') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA7' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0408') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA8' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0409') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'DA9' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0600','A0601') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'WB1' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0602') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'WB2' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0603') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'WB3' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0604') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'WB4' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0605') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'WB5' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0606') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'WB6' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0607') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'WB7' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0608') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'WB8' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0609') AND (MAT_GRP_5 IN ('-', '1st', 'Merge') OR MAT_GRP_5 LIKE 'Middle%') THEN 'WB9' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0800', 'A0801', 'A0802', 'A0803', 'A0804', 'A0805', 'A0806', 'A0807', 'A0808', 'A0809') AND MAT_GRP_5 IN ('-', 'Merge') THEN 'GATE' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A1000') THEN 'MOLD' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A1100') THEN 'CURE' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A1150', 'A1500') THEN 'MK' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A1200') THEN 'TRIM' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A1450') THEN 'TIN' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A1300') THEN 'SBA' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A1750', 'A1800', 'A1900') THEN 'SIG' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A2000') THEN 'AVI' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A2050') THEN 'PVI' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A2100') THEN 'QC_GATE' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('AZ010') THEN 'HMK3A' " + "\n");
                strSqlString.Append("                                                            ELSE ' ' " + "\n");
                strSqlString.Append("                                                        END OPER_GRP_1 " + "\n");
                strSqlString.Append("                                                     , (CASE WHEN OPER IN ('AZ010','SHIP','TZ010','F0000','EZ010', 'SZ010', 'BZ010') THEN SHIP_QTY_1 ELSE END_QTY_1 END) BUMP_END_QTY " + "\n");    //ASSY_END_QTY
                strSqlString.Append("                                                  FROM ( " + "\n");
                strSqlString.Append("                                                        SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                strSqlString.Append("                                                             , SUM(END_LOT) AS END_LOT " + "\n");
                strSqlString.Append("                                                             , SUM(END_QTY_1) AS END_QTY_1 " + "\n");
                strSqlString.Append("                                                             , SUM(END_QTY_2) AS END_QTY_2 " + "\n");
                strSqlString.Append("                                                             , SUM(SHIP_QTY_1) AS SHIP_QTY_1 " + "\n");
                strSqlString.Append("                                                             , SUM(SHIP_QTY_2) AS SHIP_QTY_2 " + "\n");
                strSqlString.Append("                                                          FROM ( " + "\n");

                //if (cdvTime.Text == "MIX")
                if (cdvTime.SelectedIndex == 0)
                {
                    strSqlString.Append("                                                                SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                    strSqlString.Append("                                                                     , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_LOT+S2_OPER_IN_LOT+S3_OPER_IN_LOT),SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT)) END_LOT " + "\n");
                    strSqlString.Append("                                                                     , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_1+S2_OPER_IN_QTY_1+S3_OPER_IN_QTY_1),SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1)) END_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_2+S2_OPER_IN_QTY_2+S3_OPER_IN_QTY_2),SUM(S1_END_QTY_2+S2_END_QTY_2+S3_END_QTY_2+S1_END_RWK_QTY_2 + S2_END_RWK_QTY_2 + S3_END_RWK_QTY_2)) END_QTY_2 " + "\n");
                    strSqlString.Append("                                                                     , 0 SHIP_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , 0 SHIP_QTY_2 " + "\n");
                    strSqlString.Append("                                                                  FROM RSUMWIPMOV " + "\n");
                    strSqlString.Append("                                                                 WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                    //strSqlString.Append("                                                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                                                                   AND OPER NOT IN ('BZ010') " + "\n");
                    strSqlString.Append("                                                                   AND MAT_ID NOT LIKE 'HX%' " + "\n");
                    strSqlString.Append("                                                                 GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                    strSqlString.Append("                                                                 UNION ALL " + "\n");
                    strSqlString.Append("                                                                SELECT FACTORY, MAT_ID " + "\n");
                    strSqlString.Append("                                                                     , 'BZ010' AS OPER " + "\n");
                    strSqlString.Append("                                                                     , LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                    strSqlString.Append("                                                                     , 0 END_LOT " + "\n");
                    strSqlString.Append("                                                                     , 0 END_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , 0 END_QTY_2 " + "\n");
                    strSqlString.Append("                                                                     , SUM(SHP_QTY_1) SHIP_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , SUM(SHP_QTY_2) SHIP_QTY_2 " + "\n");
                    strSqlString.Append("                                                                  FROM VSUMWIPOUT " + "\n");
                    strSqlString.Append("                                                                 WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                    //strSqlString.Append("                                                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                                                                   AND MAT_ID NOT LIKE 'HX%' " + "\n");
                    strSqlString.Append("                                                                 GROUP BY FACTORY, MAT_ID, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                    strSqlString.Append("                                                                 UNION ALL " + "\n");
                    strSqlString.Append("                                                                SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                    strSqlString.Append("                                                                     , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_LOT+S2_OPER_IN_LOT+S3_OPER_IN_LOT),SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT)) END_LOT " + "\n");
                    strSqlString.Append("                                                                     , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_1+S2_OPER_IN_QTY_1+S3_OPER_IN_QTY_1),SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1)) END_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_2+S2_OPER_IN_QTY_2+S3_OPER_IN_QTY_2),SUM(S1_END_QTY_2+S2_END_QTY_2+S3_END_QTY_2+S1_END_RWK_QTY_2 + S2_END_RWK_QTY_2 + S3_END_RWK_QTY_2)) END_QTY_2 " + "\n");
                    strSqlString.Append("                                                                     , 0 SHIP_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , 0 SHIP_QTY_2 " + "\n");
                    strSqlString.Append("                                                                  FROM CSUMWIPMOV " + "\n");
                    strSqlString.Append("                                                                 WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                    //strSqlString.Append("                                                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                                                                   AND OPER NOT IN ('BZ010') " + "\n");
                    strSqlString.Append("                                                                   AND MAT_ID LIKE 'HX%' " + "\n");
                    strSqlString.Append("                                                                 GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                    strSqlString.Append("                                                                 UNION ALL " + "\n");
                    strSqlString.Append("                                                                SELECT FACTORY, MAT_ID " + "\n");
                    strSqlString.Append("                                                                     , 'BZ010' AS OPER " + "\n");
                    strSqlString.Append("                                                                     , LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                    strSqlString.Append("                                                                     , 0 END_LOT " + "\n");
                    strSqlString.Append("                                                                     , 0 END_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , 0 END_QTY_2 " + "\n");
                    strSqlString.Append("                                                                     , SUM(SHP_QTY_1) SHIP_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , SUM(SHP_QTY_2) SHIP_QTY_2 " + "\n");
                    strSqlString.Append("                                                                  FROM VSUMWIPOUT_06 " + "\n");
                    strSqlString.Append("                                                                 WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                    //strSqlString.Append("                                                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                                                                   AND MAT_ID LIKE 'HX%' " + "\n");
                    strSqlString.Append("                                                                 GROUP BY FACTORY, MAT_ID, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                }
                else
                {
                    strSqlString.Append("                                                                SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                    strSqlString.Append("                                                                     , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_LOT+S2_OPER_IN_LOT+S3_OPER_IN_LOT),SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT)) END_LOT " + "\n");
                    strSqlString.Append("                                                                     , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_1+S2_OPER_IN_QTY_1+S3_OPER_IN_QTY_1),SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1)) END_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_2+S2_OPER_IN_QTY_2+S3_OPER_IN_QTY_2),SUM(S1_END_QTY_2+S2_END_QTY_2+S3_END_QTY_2+S1_END_RWK_QTY_2 + S2_END_RWK_QTY_2 + S3_END_RWK_QTY_2)) END_QTY_2 " + "\n");
                    strSqlString.Append("                                                                     , 0 SHIP_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , 0 SHIP_QTY_2 " + "\n");
                    strSqlString.Append("                                                                  FROM " + sTable_wip + "\n");
                    strSqlString.Append("                                                                 WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                    //strSqlString.Append("                                                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                                                                   AND OPER NOT IN ('BZ010') " + "\n");
                    strSqlString.Append("                                                                 GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                    strSqlString.Append("                                                                 UNION ALL " + "\n");
                    strSqlString.Append("                                                                SELECT FACTORY, MAT_ID " + "\n");
                    strSqlString.Append("                                                                     , 'BZ010' AS  OPER " + "\n");
                    strSqlString.Append("                                                                     , LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                    strSqlString.Append("                                                                     , 0 END_LOT " + "\n");
                    strSqlString.Append("                                                                     , 0 END_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , 0 END_QTY_2 " + "\n");
                    strSqlString.Append("                                                                     , SUM(SHP_QTY_1) SHIP_QTY_1 " + "\n");
                    strSqlString.Append("                                                                     , SUM(SHP_QTY_2) SHIP_QTY_2 " + "\n");
                    strSqlString.Append("                                                                  FROM " + sTable_fac + "\n");
                    strSqlString.Append("                                                                 WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                    //strSqlString.Append("                                                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                                                                 GROUP BY FACTORY, MAT_ID, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                }
                strSqlString.Append("                                                               ) " + "\n");
                strSqlString.Append("                                                         GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
                strSqlString.Append("                                                       ) A " + "\n");
                strSqlString.Append("                                                     , MWIPMATDEF B " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                   AND A.FACTORY  = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                                   AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                                                   AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("                                                   AND B.MAT_TYPE = 'FG' " + "\n");
                strSqlString.Append("                                                   AND A.MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                                                   AND A.CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                                                   AND A.OPER NOT IN ('00001','00002') " + "\n");
                strSqlString.Append("                                                   AND A.WORK_DATE = '" + Today + "' " + "\n");
                strSqlString.Append("                                                 ) " + "\n");
                strSqlString.Append("                                         GROUP BY MAT_ID, OPER_GRP_1 " + "\n");
                strSqlString.Append("                                        HAVING SUM(BUMP_END_QTY) > 0 " + "\n");
            }
            #endregion

            strSqlString.Append("                                       ) B " + "\n");
            strSqlString.Append("                                     , ( " + "\n");

            #region 금일, 어제 시간별 스냅샷 데이터
            //if ((DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd") || DateTime.Now.AddDays(-1).ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd")) && cboTimeBase.Text != "현재")
            if ((DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd") || DateTime.Now.AddDays(-1).ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd")) && cboTimeBase.SelectedIndex != 0)
            {
                strSqlString.Append("                                        SELECT MAT_ID " + "\n");
                strSqlString.Append("                                             , OPER_GRP AS OPER_GRP_1 " + "\n");
                strSqlString.Append("                                             , SUM(QTY) AS QTY " + "\n");
                strSqlString.Append("                                          FROM RWIPTIMTMP " + "\n");
                strSqlString.Append("                                         WHERE 1=1  " + "\n");
                strSqlString.Append("                                           AND WORK_DATE = '" + Today + "'" + "\n");
                //strSqlString.Append("                                           AND WORK_TIME = '" + cboTimeBase.Text.Replace("시", "") + "'" + "\n");
                strSqlString.Append("                                           AND WORK_TIME = '" + cboTimeBase.Text.Substring(0, 2) + "'" + "\n");
                strSqlString.Append("                                           AND GUBUN = 'WIP' " + "\n");
                strSqlString.Append("                                           AND FUNCTION_NAME = 'PRD011015' " + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                                           AND LOT_TYPE LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                                         GROUP BY MAT_ID, OPER_GRP " + "\n");
            }
            #endregion

            #region 기존 데이터
            else
            {
                strSqlString.Append("                                        SELECT MAT_ID, OPER_GRP_1, SUM(NVL(QTY,0)) QTY " + "\n");
                strSqlString.Append("                                          FROM (  " + "\n");
                strSqlString.Append("                                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                     , QTY " + "\n");
                strSqlString.Append("                                                     , CASE WHEN OPER IN ('B0000') THEN 'HMK2B' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B0400') THEN 'IQC' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B0500') THEN 'I_STOCK' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B1000', 'B1050', 'B1100') THEN 'PI_COATING_RCF' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1150', 'B1200') THEN 'EXPOSURE_RCF' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1250') THEN 'DEVELOP_RCF' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1300') THEN 'DEVELOP_INSP_RCF' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1350') THEN 'CURE_RCF' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1400', 'B1450') THEN 'DESCUM_RCF' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1500') THEN 'SPUTTER_RDL1' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B1550', 'B1600', 'B1650') THEN 'PR_COATING_RDL1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1700', 'B1750') THEN 'EXPOSURE_RDL1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1800') THEN 'DEVELOP_RDL1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1850') THEN 'MEASURING_INSP_RDL1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1900') THEN 'DESCUM_RDL1P' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B1950', 'B2000', 'B2050') THEN 'CU_PLATING_RDL1' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B2100', 'B2150') THEN 'PR_STRIP_RDL1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B2200', 'B2250') THEN 'CU_ETCH_RDL1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B2300', 'B2350') THEN 'TI_ETCH_RDL1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B2400', 'B2450') THEN 'DESCUM_RDL1E' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B2500', 'B2550', 'B2600', 'B2650') THEN 'PI_COATING_PSV1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B2700', 'B2750') THEN 'EXPOSURE_PSV1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B2800') THEN 'DEVELOP_PSV1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B2850') THEN 'DEVELOP_INSP_PSV1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B2900') THEN 'CURE_PSV1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B2950', 'B3000') THEN 'DESCUM_PSV1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B3050', 'B3100') THEN 'SPUTTER_RDL2' " + "\n");
                                
                strSqlString.Append("                                                            WHEN OPER IN ('B3150', 'B3200', 'B3250') THEN 'PR_COATING_RDL2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B3300', 'B3350') THEN 'EXPOSURE_RDL2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B3400') THEN 'DEVELOP_RDL2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B3450') THEN 'MEASURING_INSP_RDL2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B3500') THEN 'DESCUM_RDL2P' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B3550', 'B3600', 'B3650') THEN 'CU_PLATING_RDL2' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B3700', 'B3750') THEN 'PR_STRIP_RDL2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B3800', 'B3850') THEN 'CU_ETCH_RDL2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B3900', 'B3950') THEN 'TI_ETCH_RDL2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B4000', 'B4050') THEN 'DESCUM_RDL2E' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B4100', 'B4150', 'B4200', 'B4250') THEN 'PI_COATING_PSV2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B4300', 'B4350') THEN 'EXPOSURE_PSV2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B4400') THEN 'DEVELOP_PSV2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B4450') THEN 'DEVELOP_INSP_PSV2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B4500') THEN 'CURE_PSV2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B4550', 'B4600') THEN 'DESCUM_PSV2' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B4650', 'B4700') THEN 'SPUTTER_RDL3' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B4750', 'B4800', 'B4850') THEN 'PR_COATING_RDL3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B4900', 'B4950') THEN 'EXPOSURE_RDL3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B5000') THEN 'DEVELOP_RDL3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B5050') THEN 'MEASURING_INSP_RDL3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B5100') THEN 'DESCUM_RDL3P' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B5150', 'B5200', 'B5250') THEN 'CU_PLATING_RDL3' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B5300', 'B5350') THEN 'PR_STRIP_RDL3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B5400', 'B5450') THEN 'CU_ETCH_RDL3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B5500', 'B5550') THEN 'TI_ETCH_RDL3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B5600', 'B5650') THEN 'DESCUM_RDL3E' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B5700', 'B5750', 'B5800', 'B5850') THEN 'PI_COATING_PSV3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B5900', 'B5950') THEN 'EXPOSURE_PSV3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B6000') THEN 'DEVELOP_PSV3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B6050') THEN 'DEVELOP_INSP_PSV3' " + "\n");                
                strSqlString.Append("                                                            WHEN OPER IN ('B6100', 'B6150', 'B6200') THEN 'DESCUM_PSV3' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B6250', 'B6300') THEN 'SPUTTER_BUMP' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B6350', 'B6400', 'B6450') THEN 'PR_COATING_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B6500', 'B6550', 'B6600') THEN 'EXPOSURE_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B6650') THEN 'DEVELOP_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B6700') THEN 'MEASURING_INSP_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B6750') THEN 'DESCUM_BUMP' " + "\n");

                strSqlString.Append("                                                            WHEN OPER IN ('B6800', 'B6850', 'B6900') THEN 'CU_PLATING_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B6950', 'B7000', 'B7050') THEN 'NI_PLATING_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B7100') THEN 'SN_AG_PLATING_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B7150', 'B7200') THEN 'PR_STRIP_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B7250', 'B7300') THEN 'CU_ETCH_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B7350', 'B7400') THEN 'TI_ETCH_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B7450', 'B7500') THEN 'BALL_MOUNT_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B7550', 'B7600') THEN 'REFLOW_BUMP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B7650', 'B7700', 'B7750') THEN 'FINAL_INSP' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B7800') THEN 'SORT1' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B7850', 'B7900') THEN 'AVI' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B9000') THEN 'OGI' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('B9100') THEN 'PACKING' " + "\n");
                strSqlString.Append("                                                            WHEN OPER IN ('BZ010') THEN '완제품창고' " + "\n");

                // 2014-09-16-임종우 : Cure 공정은 선택에 해당 공정에 포함 되도록 변경
                //if (cdvCure.Text == "DA")
                //{
                    //strSqlString.Append("                                                            WHEN OPER IN ('A0400', 'A0401', 'A0250', 'A0333', 'A0340', 'A0370', 'A0380', 'A0500', 'A0501', 'A0530', 'A0531', 'A0310') THEN 'DA1' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER IN ('A0402', 'A0502', 'A0532') THEN 'DA2' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER = 'A0801' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA2' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER = 'A0801' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA2' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER IN ('A0403', 'A0503', 'A0533') THEN 'DA3' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER = 'A0802' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA3' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER = 'A0802' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA3' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER IN ('A0404', 'A0504', 'A0534') THEN 'DA4' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER = 'A0803' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA4' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER = 'A0803' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA4' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER IN ('A0405', 'A0505', 'A0535') THEN 'DA5' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER = 'A0804' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA5' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER = 'A0804' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA5' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER IN ('A0406', 'A0506', 'A0536') THEN 'DA6' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER = 'A0805' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA6' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER = 'A0805' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA6' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER IN ('A0407', 'A0507', 'A0537') THEN 'DA7' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER = 'A0806' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA7' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER = 'A0806' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA7' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER IN ('A0408', 'A0508', 'A0538') THEN 'DA8' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER = 'A0807' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA8' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER = 'A0807' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA8' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER IN ('A0409', 'A0509', 'A0539') THEN 'DA9' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER = 'A0808' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA9' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER = 'A0808' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA9' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER IN ('A0550', 'A0551', 'A0600','A0601') THEN 'WB1' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER IN ('A0552', 'A0602') THEN 'WB2' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER IN ('A0553', 'A0603') THEN 'WB3' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER IN ('A0554', 'A0604') THEN 'WB4' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER IN ('A0555', 'A0605') THEN 'WB5' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER IN ('A0556', 'A0606') THEN 'WB6' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER IN ('A0557', 'A0607') THEN 'WB7' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER IN ('A0558', 'A0608') THEN 'WB8' " + "\n");
                    //strSqlString.Append("                                                            WHEN OPER IN ('A0559', 'A0609') THEN 'WB9' " + "\n");
                //}
                //else
                //{
                //    strSqlString.Append("                                                            WHEN OPER IN ('A0400', 'A0401', 'A0250', 'A0333', 'A0340', 'A0370', 'A0380', 'A0310') THEN 'DA1' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER IN ('A0402') THEN 'DA2' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER = 'A0801' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA2' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER = 'A0801' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA2' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER IN ('A0403') THEN 'DA3' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER = 'A0802' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA3' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER = 'A0802' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA3' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER IN ('A0404') THEN 'DA4' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER = 'A0803' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA4' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER = 'A0803' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA4' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER IN ('A0405') THEN 'DA5' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER = 'A0804' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA5' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER = 'A0804' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA5' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER IN ('A0406') THEN 'DA6' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER = 'A0805' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA6' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER = 'A0805' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA6' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER IN ('A0407') THEN 'DA7' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER = 'A0806' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA7' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER = 'A0806' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA7' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER IN ('A0408') THEN 'DA8' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER = 'A0807' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA8' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER = 'A0807' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA8' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER IN ('A0409') THEN 'DA9' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER = 'A0808' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN 'DA9' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER = 'A0808' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN 'DA9' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER IN ('A0550', 'A0551', 'A0600','A0601', 'A0500', 'A0501', 'A0530', 'A0531') THEN 'WB1' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER IN ('A0552', 'A0602', 'A0502', 'A0532') THEN 'WB2' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER IN ('A0553', 'A0603', 'A0503', 'A0533') THEN 'WB3' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER IN ('A0554', 'A0604', 'A0504', 'A0534') THEN 'WB4' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER IN ('A0555', 'A0605', 'A0505', 'A0535') THEN 'WB5' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER IN ('A0556', 'A0606', 'A0506', 'A0536') THEN 'WB6' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER IN ('A0557', 'A0607', 'A0507', 'A0537') THEN 'WB7' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER IN ('A0558', 'A0608', 'A0508', 'A0538') THEN 'WB8' " + "\n");
                //    strSqlString.Append("                                                            WHEN OPER IN ('A0559', 'A0609', 'A0509', 'A0539') THEN 'WB9' " + "\n");
                //}

                //strSqlString.Append("                                                            WHEN OPER IN ('A0800', 'A0801', 'A0802', 'A0803', 'A0804', 'A0805', 'A0806', 'A0807', 'A0808', 'A0809') AND MAT_GRP_5 = '-' THEN 'GATE' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0800', 'A0801', 'A0802', 'A0803', 'A0804', 'A0805', 'A0806', 'A0807', 'A0808', 'A0809') AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 = 'Merge' THEN 'GATE' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0800', 'A0801', 'A0802', 'A0803', 'A0804', 'A0805', 'A0806', 'A0807', 'A0808', 'A0809') AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) = SUBSTR(OPER, -1) THEN 'GATE' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A0970', 'A0900', 'A0950', 'A1000', 'A0980') THEN 'MOLD' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A1170', 'A1180', 'A1100', 'A1230', 'A1950') THEN 'CURE' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A1110', 'A1150', 'A1500') THEN 'MK' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A1200') THEN 'TRIM' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A1470', 'A1450', 'A1440', 'A1050', 'A1420') THEN 'TIN' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A1270', 'A1250', 'A1300', 'A1260', 'A1350', 'A1240') THEN 'SBA' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A1720', 'A1750', 'A1800', 'A1900', 'A1760', 'A1740', 'A1765') THEN 'SIG' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A2000', 'A1040', 'A1770', 'A1790', 'A1795') THEN 'AVI' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A2050') THEN 'PVI' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('A2100') THEN 'QC_GATE' " + "\n");
                //strSqlString.Append("                                                            WHEN OPER IN ('AZ010') THEN 'HMK3A' " + "\n");
                strSqlString.Append("                                                            ELSE ' ' " + "\n");
                strSqlString.Append("                                                        END OPER_GRP_1 " + "\n");
                strSqlString.Append("                                                  FROM ( " + "\n");
                strSqlString.Append("                                                        SELECT MAT_ID, OPER, OPER_GRP_1, MAT_GRP_1, MAT_GRP_4, MAT_GRP_5 " + "\n");
                
                // 단순 QTY_1 사용  ????
                // 2014-09-02-임종우 : HX 제품 재공 로직 변경 A0015, A0395 타는 제품의 경우 해당 공정까지 COMP 로직
                //strSqlString.Append("                                                             , CASE WHEN MAT_GRP_1 = 'HX' AND HX_COMP_MIN IS NOT NULL" + "\n");
                //strSqlString.Append("                                                                         THEN (CASE WHEN HX_COMP_MIN <> HX_COMP_MAX AND OPER > HX_COMP_MIN AND OPER <= HX_COMP_MAX THEN QTY_1 / NVL(COMP_CNT / 2,1)" + "\n");
                //strSqlString.Append("                                                                         WHEN OPER <= HX_COMP_MAX THEN QTY_1 / NVL(COMP_CNT,1)" + "\n");
                //strSqlString.Append("                                                                         ELSE QTY_1 END)" + "\n");
                //strSqlString.Append("                                                                    WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1) " + "\n");
                //strSqlString.Append("                                                                    ELSE QTY_1 " + "\n");
                //strSqlString.Append("                                                               END QTY " + "\n");
                strSqlString.Append("                                                               , QTY_1 AS QTY " + "\n");

                strSqlString.Append("                                                          FROM ( " + "\n");
                strSqlString.Append("                                                                SELECT A.MAT_ID, B.OPER, B.OPER_GRP_1, C.MAT_GRP_1, C.MAT_GRP_4, C.MAT_GRP_5 " + "\n");
                
                //strSqlString.Append("                                                                     , CASE WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN QTY_1 ELSE 0 END " + "\n");
                //strSqlString.Append("                                                                            WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(QTY_1/TO_NUMBER(DECODE(MAT_CMF_13, ' ', 1, '-', 1, MAT_CMF_13)),0) " + "\n");
                //strSqlString.Append("                                                                            ELSE QTY_1 " + "\n");
                //strSqlString.Append("                                                                       END AS QTY_1 " + "\n");                
                strSqlString.Append("                                                                       , QTY_1  " + "\n");
                                
                //strSqlString.Append("                                                                     , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') AND KEY_1 = A.MAT_ID) AS COMP_CNT  " + "\n");
                //strSqlString.Append("                                                                     , HX_COMP_MIN, HX_COMP_MAX " + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd"))
                {
                    strSqlString.Append("                                                                  FROM RWIPLOTSTS A   " + "\n");
                    strSqlString.Append("                                                                     , MWIPOPRDEF B   " + "\n");
                    strSqlString.Append("                                                                     , MWIPMATDEF C " + "\n");

                    // COMP 로직 필요X
                    //strSqlString.Append("                                                                     , (SELECT FLOW, MIN(OPER) AS HX_COMP_MIN, MAX(OPER) AS HX_COMP_MAX" + "\n");
                    //strSqlString.Append("                                                                          FROM MWIPFLWOPR@RPTTOMES" + "\n");
                    //strSqlString.Append("                                                                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    //strSqlString.Append("                                                                           AND OPER IN ('A0015','A0395')" + "\n");
                    //strSqlString.Append("                                                                         GROUP BY FLOW" + "\n");
                    //strSqlString.Append("                                                                       ) D" + "\n");
                    strSqlString.Append("                                                                 WHERE 1 = 1  " + "\n");
                }
                else
                {
                    strSqlString.Append("                                                                  FROM RWIPLOTSTS_BOH A   " + "\n");
                    strSqlString.Append("                                                                     , MWIPOPRDEF B   " + "\n");
                    strSqlString.Append("                                                                     , MWIPMATDEF C " + "\n");

                    // COMP 로직 필요X
                    //strSqlString.Append("                                                                     , (SELECT FLOW, MIN(OPER) AS HX_COMP_MIN, MAX(OPER) AS HX_COMP_MAX" + "\n");
                    //strSqlString.Append("                                                                          FROM MWIPFLWOPR@RPTTOMES" + "\n");
                    //strSqlString.Append("                                                                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    //strSqlString.Append("                                                                           AND OPER IN ('A0015','A0395')" + "\n");
                    //strSqlString.Append("                                                                         GROUP BY FLOW" + "\n");
                    //strSqlString.Append("                                                                       ) D" + "\n");
                    strSqlString.Append("                                                                 WHERE 1 = 1  " + "\n");

                    // 2014-10-13-임종우 : 06시를 선택시 06시 조회 그 이외는 22시 조회
                    //if (cboTimeBase.Text == "06시")
                    if (cboTimeBase.SelectedIndex == 6)
                    {
                        strSqlString.Append("                                                                   AND A.CUTOFF_DT = '" + cdvDate.Value.ToString("yyyyMMdd") + "06' " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("                                                                   AND A.CUTOFF_DT = '" + cdvDate.Value.ToString("yyyyMMdd") + "22' " + "\n");
                    }
                }
                strSqlString.Append("                                                                   AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                                                                   AND A.FACTORY = C.FACTORY " + "\n");
                strSqlString.Append("                                                                   AND A.OPER = B.OPER " + "\n");
                strSqlString.Append("                                                                   AND A.MAT_ID = C.MAT_ID " + "\n");
                //strSqlString.Append("                                                                   AND A.FLOW = D.FLOW(+) " + "\n");
                strSqlString.Append("                                                                   AND A.FACTORY  "+ cdvFactory.SelectedValueToQueryString + "\n");
                //strSqlString.Append("                                                                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'   " + "\n");
                strSqlString.Append("                                                                   AND A.LOT_TYPE = 'W'  " + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                                                                   AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                                                                   AND A.LOT_DEL_FLAG = ' '  " + "\n");
                strSqlString.Append("                                                                   AND C.MAT_GRP_2 <> '-' " + "\n");
                strSqlString.Append("                                                                   AND C.DELETE_FLAG = ' '  " + "\n");
                strSqlString.Append("                                                               ) " + "\n");
                strSqlString.Append("                                                       ) " + "\n");
                strSqlString.Append("                                               ) " + "\n");
                strSqlString.Append("                                         WHERE 1=1 " + "\n");
                strSqlString.Append("                                           AND OPER_GRP_1 <> ' ' " + "\n");
                strSqlString.Append("                                         GROUP BY MAT_ID, OPER_GRP_1 " + "\n");
                strSqlString.Append("                                        HAVING SUM(NVL(QTY,0)) > 0 " + "\n");
            }
            #endregion

            strSqlString.Append("                                       ) WIP " + "\n");
            strSqlString.Append("                                     , ( " + "\n");
            strSqlString.Append("                                        SELECT A.RES_STS_2 AS MAT_ID " + "\n");
            strSqlString.Append("                                             , A.OPER_GRP_1 " + "\n");
            strSqlString.Append("                                             , SUM(A.RES_CNT) AS RES_CNT " + "\n");
            strSqlString.Append("                                             , SUM(TRUNC(NVL(NVL(B.UPEH,0) * 24 * A.PERCENT * A.RES_CNT, 0))) AS RES_CAPA " + "\n");
            strSqlString.Append("                                          FROM ( " + "\n");
            strSqlString.Append("                                                SELECT FACTORY, RES_STS_2, RES_STS_8 AS OPER, RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, COUNT(RES_ID) AS RES_CNT " + "\n");
            strSqlString.Append("                                                     , 0.75 AS PERCENT " + "\n");
            //strSqlString.Append("                                                     , DECODE(SUBSTR(RES_STS_8, 1, 3), 'A06', 0.75, 0.7 ) AS PERCENT " + "\n");
                        
            strSqlString.Append("                                                     , CASE WHEN RES_STS_8 IN ('B0000') THEN 'HMK2B' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B0400') THEN 'IQC' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B0500') THEN 'I_STOCK' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B1100') THEN 'PI_COATING_RCF' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B1200') THEN 'EXPOSURE_RCF' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B1250') THEN 'DEVELOP_RCF' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B1300') THEN 'DEVELOP_INSP_RCF' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B1350') THEN 'CURE_RCF' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B1450') THEN 'DESCUM_RCF' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B1500') THEN 'SPUTTER_RDL1' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B1650') THEN 'PR_COATING_RDL1' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B1750') THEN 'EXPOSURE_RDL1' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B1800') THEN 'DEVELOP_RDL1' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B1850') THEN 'MEASURING_INSP_RDL1' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B1900') THEN 'DESCUM_RDL1P' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B2050') THEN 'CU_PLATING_RDL1' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B2150') THEN 'PR_STRIP_RDL1' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B2250') THEN 'CU_ETCH_RDL1' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B2350') THEN 'TI_ETCH_RDL1' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B2450') THEN 'DESCUM_RDL1E' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B2650') THEN 'PI_COATING_PSV1' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B2750') THEN 'EXPOSURE_PSV1' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B2800') THEN 'DEVELOP_PSV1' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B2850') THEN 'DEVELOP_INSP_PSV1' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B2900') THEN 'CURE_PSV1' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B3000') THEN 'DESCUM_PSV1' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B3100') THEN 'SPUTTER_RDL2' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B3250') THEN 'PR_COATING_RDL2' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B3350') THEN 'EXPOSURE_RDL2' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B3400') THEN 'DEVELOP_RDL2' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B3450') THEN 'MEASURING_INSP_RDL2' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B3500') THEN 'DESCUM_RDL2P' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B3650') THEN 'CU_PLATING_RDL2' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B3750') THEN 'PR_STRIP_RDL2' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B3850') THEN 'CU_ETCH_RDL2' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B3950') THEN 'TI_ETCH_RDL2' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B4050') THEN 'DESCUM_RDL2E' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B4250') THEN 'PI_COATING_PSV2' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B4350') THEN 'EXPOSURE_PSV2' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B4400') THEN 'DEVELOP_PSV2' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B4450') THEN 'DEVELOP_INSP_PSV2' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B4500') THEN 'CURE_PSV2' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B4600') THEN 'DESCUM_PSV2' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B4700') THEN 'SPUTTER_RDL3' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B4850') THEN 'PR_COATING_RDL3' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B4950') THEN 'EXPOSURE_RDL3' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B5000') THEN 'DEVELOP_RDL3' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B5050') THEN 'MEASURING_INSP_RDL3' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B5100') THEN 'DESCUM_RDL3P' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B5250') THEN 'CU_PLATING_RDL3' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B5350') THEN 'PR_STRIP_RDL3' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B5450') THEN 'CU_ETCH_RDL3' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B5550') THEN 'TI_ETCH_RDL3' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B5650') THEN 'DESCUM_RDL3E' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B5850') THEN 'PI_COATING_PSV3' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B5950') THEN 'EXPOSURE_PSV3' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B6000') THEN 'DEVELOP_PSV3' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B6050') THEN 'DEVELOP_INSP_PSV3' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B6200') THEN 'DESCUM_PSV3' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B6300') THEN 'SPUTTER_BUMP' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B6450') THEN 'PR_COATING_BUMP' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B6550') THEN 'EXPOSURE_BUMP' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B6650') THEN 'DEVELOP_BUMP' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B6700') THEN 'MEASURING_INSP_BUMP' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B6750') THEN 'DESCUM_BUMP' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B6900') THEN 'CU_PLATING_BUMP' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B7050') THEN 'NI_PLATING_BUMP' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B7100') THEN 'SN_AG_PLATING_BUMP' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B7200') THEN 'PR_STRIP_BUMP' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B7300') THEN 'CU_ETCH_BUMP' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B7400') THEN 'TI_ETCH_BUMP' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B7500') THEN 'BALL_MOUNT_BUMP' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B7600') THEN 'REFLOW_BUMP' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B7750') THEN 'FINAL_INSP' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B7800') THEN 'SORT1' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B7900') THEN 'AVI' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B9000') THEN 'OGI' " + "\n");
            strSqlString.Append("                                                            WHEN RES_STS_8 IN ('B9100') THEN 'PACKING' " + "\n");
            //strSqlString.Append("                                                            WHEN RES_STS_8 IN ('BZ010') THEN '완제품창고' " + "\n");

            strSqlString.Append("                                                            ELSE ' ' " + "\n");
            strSqlString.Append("                                                       END OPER_GRP_1 " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                                                  FROM MRASRESDEF " + "\n");
                strSqlString.Append("                                                 WHERE 1 = 1  " + "\n");
            }
            else
            {
                strSqlString.Append("                                                  FROM MRASRESDEF_BOH " + "\n");
                strSqlString.Append("                                                 WHERE 1 = 1  " + "\n");
                strSqlString.Append("                                                   AND CUTOFF_DT = '" + cdvDate.Value.ToString("yyyyMMdd") + "22' " + "\n");
            }

            strSqlString.Append("                                                   AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            //strSqlString.Append("                                                   AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                        
            strSqlString.Append("                                                   AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("                                                   AND RES_CMF_7 = 'Y' " + "\n");
            strSqlString.Append("                                                   AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                                                   AND RES_TYPE='EQUIPMENT' " + "\n");
            //strSqlString.Append("                                                   AND RES_STS_1 NOT IN ('C200', 'B199') " + "\n");        //Bump에서는 주석처리

            strSqlString.Append("                                                 GROUP BY FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7  " + "\n");
            strSqlString.Append("                                               ) A " + "\n");
            strSqlString.Append("                                             , CRASUPHDEF B " + "\n");
            strSqlString.Append("                                         WHERE 1=1 " + "\n");
            strSqlString.Append("                                           AND A.FACTORY = B.FACTORY(+) " + "\n");
            strSqlString.Append("                                           AND A.OPER = B.OPER(+) " + "\n");
            strSqlString.Append("                                           AND A.RES_MODEL = B.RES_MODEL(+) " + "\n");
            strSqlString.Append("                                           AND A.UPEH_GRP = B.UPEH_GRP(+) " + "\n");
            strSqlString.Append("                                           AND A.RES_STS_2 = B.MAT_ID(+) " + "\n");
            strSqlString.Append("                                           AND A.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            //strSqlString.Append("                                           AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                           AND A.OPER NOT IN ('00001','00002') " + "\n");
            strSqlString.Append("                                           AND A.OPER_GRP_1 <> ' ' " + "\n");
            strSqlString.Append("                                         GROUP BY A.RES_STS_2, A.OPER_GRP_1 " + "\n");
            strSqlString.Append("                                       ) RES " + "\n");
            strSqlString.Append("                                 WHERE 1=1 " + "\n");

            // 2014-07-07-임종우 : 계획 유무에 의한 검색 기능 추가
            if (ckdPlan.Checked == true)
            {
                strSqlString.Append("                                   AND MAT.MAT_ID = A.MAT_ID " + "\n");
            }
            else
            {
                strSqlString.Append("                                   AND MAT.MAT_ID = A.MAT_ID(+) " + "\n");
            }

            strSqlString.Append("                                   AND MAT.MAT_ID = B.MAT_ID(+) " + "\n");
            strSqlString.Append("                                   AND MAT.MAT_ID = WIP.MAT_ID(+) " + "\n");
            strSqlString.Append("                                   AND MAT.MAT_ID = RES.MAT_ID(+) " + "\n");
            strSqlString.Append("                                   AND MAT.OPER_GRP_1 = B.OPER_GRP_1(+) " + "\n");
            strSqlString.Append("                                   AND MAT.OPER_GRP_1 = WIP.OPER_GRP_1(+) " + "\n");
            strSqlString.Append("                                   AND MAT.OPER_GRP_1 = RES.OPER_GRP_1(+) " + "\n");
            
            strSqlString.Append("                               ) " + "\n");
            strSqlString.Append("                         GROUP BY " + QueryCond3 + ", OPER_GRP_1 " + "\n");
            strSqlString.Append("                        HAVING ( " + "\n");
            if (cdvTime.SelectedIndex == 4 || cdvTime.SelectedIndex == 5 || cdvTime.SelectedIndex == 6)
            {
                strSqlString.Append("                                SUM(NVL(D0_ORI_PLAN,0))+SUM(NVL(D1_PLAN,0))+SUM(NVL(D2_PLAN,0))+SUM(NVL(WEEK_PLAN,0))+SUM(NVL(WEEK2_PLAN,0)) " + "\n");
            }
            else
            {
                strSqlString.Append("                                SUM(NVL(D0_ORI_PLAN,0))+SUM(NVL(D1_PLAN,0))+SUM(NVL(D2_PLAN,0))+SUM(NVL(WEEK_PLAN,0))+SUM(NVL(WEEK2_PLAN,0))+SUM(NVL(MON_PLAN,0))+SUM(NVL(WIP_QTY,0)) " + "\n");
            }

            //if (cdvGroup.Text == "ALL")
            //{
            //    strSqlString.Append("                                SUM(NVL(D0_ORI_PLAN,0))+SUM(NVL(D1_PLAN,0))+SUM(NVL(D2_PLAN,0))+SUM(NVL(WEEK_PLAN,0))+SUM(NVL(WEEK2_PLAN,0))+SUM(NVL(MON_PLAN,0))+SUM(NVL(WIP_QTY,0)) " + "\n");
            //}
            //else if (cdvGroup.Text == "당일 실적")
            //{
            //    strSqlString.Append("                                SUM(NVL(CHK_ASSY_END_QTY,0))+SUM(NVL(WIP_QTY,0))  " + "\n");
            //}
            //else
            //{
            //    string[] condList = cdvGroup.Text.Split(',');
            //    string strMakeCond = "";

            //    for (int i = 0; i < condList.Length; i++)
            //    {
            //        if ("금일 잔량" == condList[i].Trim())
            //        {
            //            strMakeCond += "SUM(NVL(D0_ORI_PLAN,0))+";
            //        }
            //        else if ("명일 잔량" == condList[i].Trim())
            //        {
            //            strMakeCond += "SUM(NVL(D1_PLAN,0))+";
            //        }
            //        else if ("주간 잔량" == condList[i].Trim())
            //        {
            //            strMakeCond += "SUM(NVL(WEEK_PLAN,0))+";
            //        }
            //        else if ("월간 잔량" == condList[i].Trim())
            //        {
            //            strMakeCond += "SUM(NVL(MON_PLAN,0))+";
            //        }
            //    }

            //    strMakeCond = strMakeCond.Trim('+');

            //    strSqlString.Append("                                " + strMakeCond + "+SUM(NVL(WIP_QTY,0)) " + "\n");
            //}

            strSqlString.Append("                               ) > 0 " + "\n");
            strSqlString.Append("                      ) " + "\n");
            strSqlString.Append("                    , (SELECT LEVEL AS SEQ FROM DUAL CONNECT BY LEVEL <= 10) " + "\n");
            strSqlString.Append("                 GROUP BY " + QueryCond3 + ", OPER_GRP_1, DECODE(SEQ, 1, 'WIP', 2, '설비대수', 3, 'CAPA현황', 4, '당일 실적', 5, 'D0 잔량', 6, 'D1 잔량', 7, 'D2 잔량', 8, '당주 잔량', 9, '차주 잔량', 10, '월간 잔량') " + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("         GROUP BY  " + QueryCond3 + ", GUBUN " + "\n");
            strSqlString.Append("       ) A " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");

            if (cdvGroup.Text != "ALL")
            {
                strSqlString.Append("   AND (GUBUN = 'WIP' OR GUBUN " + cdvGroup.SelectedValueToQueryString + ") " + "\n");
            }

            strSqlString.Append(" GROUP BY " + QueryCond3 + ", GUBUN " + "\n");
            strSqlString.Append(" ORDER BY " + QueryCond4 + ", DECODE(GUBUN, 'WIP', 1, '설비대수', 2, 'CAPA현황', 3, '당일 실적', 4, 'D0 잔량', 5, 'D1 잔량', 6, 'D2 잔량', 7, '당주 잔량', 8, '차주 잔량', 9, '월간 잔량', 10) " + "\n");


            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }


        /// <summary>
        /// 설정된 그룹별 공정 정보_BUMP
        /// </summary>
        /// <returns></returns>
        private string MakeSqlStringStepInfoBump()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string QueryCond4 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            strSqlString.Append("SELECT " + QueryCond1 + " " + "\n");                        
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, '완제품창고', 'T', '', 'F', 'F')) AS 완제품창고 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'PACKING', 'T', '', 'F', 'F')) AS PACKING " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'OGI', 'T', '', 'F', 'F')) AS OGI " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'AVI', 'T', '', 'F', 'F')) AS AVI " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'SORT1', 'T', '', 'F', 'F')) AS SORT1 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'FINAL_INSP', 'T', '', 'F', 'F')) AS FINAL_INSP " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'REFLOW_BUMP', 'T', '', 'F', 'F')) AS REFLOW_BUMP " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'BALL_MOUNT_BUMP', 'T', '', 'F', 'F')) AS BALL_MOUNT_BUMP " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'TI_ETCH_BUMP', 'T', '', 'F', 'F')) AS TI_ETCH_BUMP " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'CU_ETCH_BUMP', 'T', '', 'F', 'F')) AS CU_ETCH_BUMP " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'PR_STRIP_BUMP', 'T', '', 'F', 'F')) AS PR_STRIP_BUMP " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'SN_AG_PLATING_BUMP', 'T', '', 'F', 'F')) AS SN_AG_PLATING_BUMP " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'NI_PLATING_BUMP', 'T', '', 'F', 'F')) AS NI_PLATING_BUMP " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'CU_PLATING_BUMP', 'T', '', 'F', 'F')) AS CU_PLATING_BUMP " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DESCUM_BUMP', 'T', '', 'F', 'F')) AS DESCUM_BUMP " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'MEASURING_INSP_BUMP', 'T', '', 'F', 'F')) AS MEASURING_INSP_BUMP " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DEVELOP_BUMP', 'T', '', 'F', 'F')) AS DEVELOP_BUMP " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'EXPOSURE_BUMP', 'T', '', 'F', 'F')) AS EXPOSURE_BUMP " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'PR_COATING_BUMP', 'T', '', 'F', 'F')) AS PR_COATING_BUMP " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'SPUTTER_BUMP', 'T', '', 'F', 'F')) AS SPUTTER_BUMP " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DESCUM_PSV3', 'T', '', 'F', 'F')) AS DESCUM_PSV3  " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DEVELOP_INSP_PSV3', 'T', '', 'F', 'F')) AS DEVELOP_INSP_PSV3 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DEVELOP_PSV3', 'T', '', 'F', 'F')) AS DEVELOP_PSV3 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'EXPOSURE_PSV3', 'T', '', 'F', 'F')) AS EXPOSURE_PSV3 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'PI_COATING_PSV3', 'T', '', 'F', 'F')) AS PI_COATING_PSV3 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DESCUM_RDL3E', 'T', '', 'F', 'F')) AS DESCUM_RDL3E " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'TI_ETCH_RDL3', 'T', '', 'F', 'F')) AS TI_ETCH_RDL3 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'CU_ETCH_RDL3', 'T', '', 'F', 'F')) AS CU_ETCH_RDL3 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'PR_STRIP_RDL3', 'T', '', 'F', 'F')) AS PR_STRIP_RDL3 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'CU_PLATING_RDL3', 'T', '', 'F', 'F')) AS CU_PLATING_RDL3 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DESCUM_RDL3P', 'T', '', 'F', 'F')) AS DESCUM_RDL3P " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'MEASURING_INSP_RDL3', 'T', '', 'F', 'F')) AS MEASURING_INSP_RDL3 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DEVELOP_RDL3', 'T', '', 'F', 'F')) AS DEVELOP_RDL3 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'EXPOSURE_RDL3', 'T', '', 'F', 'F')) AS EXPOSURE_RDL3 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'PR_COATING_RDL3', 'T', '', 'F', 'F')) AS PR_COATING_RDL3 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'SPUTTER_RDL3', 'T', '', 'F', 'F')) AS SPUTTER_RDL3 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DESCUM_PSV2', 'T', '', 'F', 'F')) AS DESCUM_PSV2  " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'CURE_PSV2', 'T', '', 'F', 'F')) AS CURE_PSV2 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DEVELOP_INSP_PSV2', 'T', '', 'F', 'F')) AS DEVELOP_INSP_PSV2 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DEVELOP_PSV2', 'T', '', 'F', 'F')) AS DEVELOP_PSV2 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'EXPOSURE_PSV2', 'T', '', 'F', 'F')) AS EXPOSURE_PSV2 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'PI_COATING_PSV2', 'T', '', 'F', 'F')) AS PI_COATING_PSV2 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DESCUM_RDL2E', 'T', '', 'F', 'F')) AS DESCUM_RDL2E " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'TI_ETCH_RDL2', 'T', '', 'F', 'F')) AS TI_ETCH_RDL2 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'CU_ETCH_RDL2', 'T', '', 'F', 'F')) AS CU_ETCH_RDL2 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'PR_STRIP_RDL2', 'T', '', 'F', 'F')) AS PR_STRIP_RDL2 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'CU_PLATING_RDL2', 'T', '', 'F', 'F')) AS CU_PLATING_RDL2 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DESCUM_RDL2P', 'T', '', 'F', 'F')) AS DESCUM_RDL2P " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'MEASURING_INSP_RDL2', 'T', '', 'F', 'F')) AS MEASURING_INSP_RDL2 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DEVELOP_RDL2', 'T', '', 'F', 'F')) AS DEVELOP_RDL2 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'EXPOSURE_RDL2', 'T', '', 'F', 'F')) AS EXPOSURE_RDL2 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'PR_COATING_RDL2', 'T', '', 'F', 'F')) AS PR_COATING_RDL2 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'SPUTTER_RDL2', 'T', '', 'F', 'F')) AS SPUTTER_RDL2 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DESCUM_PSV1', 'T', '', 'F', 'F')) AS DESCUM_PSV1  " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'CURE_PSV1', 'T', '', 'F', 'F')) AS CURE_PSV1 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DEVELOP_INSP_PSV1', 'T', '', 'F', 'F')) AS DEVELOP_INSP_PSV1 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DEVELOP_PSV1', 'T', '', 'F', 'F')) AS DEVELOP_PSV1 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'EXPOSURE_PSV1', 'T', '', 'F', 'F')) AS EXPOSURE_PSV1 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'PI_COATING_PSV1', 'T', '', 'F', 'F')) AS PI_COATING_PSV1 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DESCUM_RDL1E', 'T', '', 'F', 'F')) AS DESCUM_RDL1E " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'TI_ETCH_RDL1', 'T', '', 'F', 'F')) AS TI_ETCH_RDL1 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'CU_ETCH_RDL1', 'T', '', 'F', 'F')) AS CU_ETCH_RDL1 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'PR_STRIP_RDL1', 'T', '', 'F', 'F')) AS PR_STRIP_RDL1 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'CU_PLATING_RDL1', 'T', '', 'F', 'F')) AS CU_PLATING_RDL1 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DESCUM_RDL1P', 'T', '', 'F', 'F')) AS DESCUM_RDL1P " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'MEASURING_INSP_RDL1', 'T', '', 'F', 'F')) AS MEASURING_INSP_RDL1 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DEVELOP_RDL1', 'T', '', 'F', 'F')) AS DEVELOP_RDL1 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'EXPOSURE_RDL1', 'T', '', 'F', 'F')) AS EXPOSURE_RDL1 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'PR_COATING_RDL1', 'T', '', 'F', 'F')) AS PR_COATING_RDL1 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'SPUTTER_RDL1', 'T', '', 'F', 'F')) AS SPUTTER_RDL1 " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DESCUM_RCF', 'T', '', 'F', 'F')) AS DESCUM_RCF " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'CURE_RCF', 'T', '', 'F', 'F')) AS CURE_RCF " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DEVELOP_INSP_RCF', 'T', '', 'F', 'F')) AS DEVELOP_INSP_RCF " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'DEVELOP_RCF', 'T', '', 'F', 'F')) AS DEVELOP_RCF " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'EXPOSURE_RCF', 'T', '', 'F', 'F')) AS EXPOSURE_RCF " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'PI_COATING_RCF', 'T', '', 'F', 'F')) AS PI_COATING_RCF " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'I-STOCK', 'T', '', 'F', 'F')) AS I_STOCK " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'IQC', 'T', '', 'F', 'F')) AS QC_GATE " + "\n");
            strSqlString.Append("     , MAX(DECODE(OPER_GRP_1, 'HMK2B', 'T', '', 'F', 'F')) AS HMK2B " + "\n");

            strSqlString.Append("  FROM ( " + "\n");
            
            
            strSqlString.Append("        SELECT MAT.FACTORY, " + QueryCond2.Replace(", CONV_MAT_ID", "") + ", MAT.MAT_ID, MAT.DELETE_FLAG " + "\n");
            //strSqlString.Append("             , CASE WHEN MAT.MAT_GRP_1 = 'SE' AND MAT.MAT_GRP_9 = 'MEMORY' THEN 'SEK_________-___' || SUBSTR(MAT.MAT_ID, -3) " + "\n");
            //strSqlString.Append("                                                                    WHEN MAT.MAT_GRP_1 = 'HX' THEN MAT.MAT_CMF_10 " + "\n");
            //strSqlString.Append("                                                                    ELSE MAT_ID " + "\n");
            //strSqlString.Append("                                                               END CONV_MAT_ID " + "\n");
            strSqlString.Append("               , MAT_ID AS CONV_MAT_ID " + "\n");

            strSqlString.Append("          FROM MWIPMATDEF MAT " + "\n");
            strSqlString.Append("       ) MAT " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT A.MAT_ID " + "\n");
                                
            strSqlString.Append("                     , CASE WHEN B.OPER IN ('B1000', 'B1050', 'B1100') THEN 'PI_COATING_RCF' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B1150', 'B1200') THEN 'EXPOSURE_RCF' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B1250') THEN 'DEVELOP_RCF' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B1300') THEN 'DEVELOP_INSP_RCF' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B1350') THEN 'CURE_RCF' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B1400', 'B1450') THEN 'DESCUM_RCF' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B1500') THEN 'SPUTTER_RDL1' " + "\n");

            strSqlString.Append("                            WHEN B.OPER IN ('B1550', 'B1600', 'B1650') THEN 'PR_COATING_RDL1' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B1700', 'B1750') THEN 'EXPOSURE_RDL1' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B1800') THEN 'DEVELOP_RDL1' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B1850') THEN 'MEASURING_INSP_RDL1' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B1900') THEN 'DESCUM_RDL1P' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B1950', 'B2000', 'B2050') THEN 'CU_PLATING_RDL1' " + "\n");

            strSqlString.Append("                            WHEN B.OPER IN ('B2100', 'B2150') THEN 'PR_STRIP_RDL1' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B2200', 'B2250') THEN 'CU_ETCH_RDL1' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B2300', 'B2350') THEN 'TI_ETCH_RDL1' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B2400', 'B2450') THEN 'DESCUM_RDL1E' " + "\n");

            strSqlString.Append("                            WHEN B.OPER IN ('B2500', 'B2550', 'B2600', 'B2650') THEN 'PI_COATING_PSV1' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B2700', 'B2750') THEN 'EXPOSURE_PSV1' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B2800') THEN 'DEVELOP_PSV1' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B2850') THEN 'DEVELOP_INSP_PSV1' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B2900') THEN 'CURE_PSV1' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B2950', 'B3000') THEN 'DESCUM_PSV1' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B3050', 'B3100') THEN 'SPUTTER_RDL2' " + "\n");

            strSqlString.Append("                            WHEN B.OPER IN ('B3150', 'B3200', 'B3250') THEN 'PR_COATING_RDL2' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B3300', 'B3350') THEN 'EXPOSURE_RDL2' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B3400') THEN 'DEVELOP_RDL2' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B3450') THEN 'MEASURING_INSP_RDL2' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B3500') THEN 'DESCUM_RDL2P' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B3550', 'B3600', 'B3650') THEN 'CU_PLATING_RDL2' " + "\n");

            strSqlString.Append("                            WHEN B.OPER IN ('B3700', 'B3750') THEN 'PR_STRIP_RDL2' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B3800', 'B3850') THEN 'CU_ETCH_RDL2' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B3900', 'B3950') THEN 'TI_ETCH_RDL2' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B4000', 'B4050') THEN 'DESCUM_RDL2E' " + "\n");

            strSqlString.Append("                            WHEN B.OPER IN ('B4100', 'B4150', 'B4200', 'B4250') THEN 'PI_COATING_PSV2' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B4300', 'B4350') THEN 'EXPOSURE_PSV2' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B4400') THEN 'DEVELOP_PSV2' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B4450') THEN 'DEVELOP_INSP_PSV2' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B4500') THEN 'CURE_PSV2' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B4550', 'B4600') THEN 'DESCUM_PSV2' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B4650', 'B4700') THEN 'SPUTTER_RDL3' " + "\n");

            strSqlString.Append("                            WHEN B.OPER IN ('B4750', 'B4800', 'B4850') THEN 'PR_COATING_RDL3' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B4900', 'B4950') THEN 'EXPOSURE_RDL3' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B5000') THEN 'DEVELOP_RDL3' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B5050') THEN 'MEASURING_INSP_RDL3' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B5100') THEN 'DESCUM_RDL3P' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B5150', 'B5200', 'B5250') THEN 'CU_PLATING_RDL3' " + "\n");

            strSqlString.Append("                            WHEN B.OPER IN ('B5300', 'B5350') THEN 'PR_STRIP_RDL3' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B5400', 'B5450') THEN 'CU_ETCH_RDL3' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B5500', 'B5550') THEN 'TI_ETCH_RDL3' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B5600', 'B5650') THEN 'DESCUM_RDL3E' " + "\n");

            strSqlString.Append("                            WHEN B.OPER IN ('B5700', 'B5750', 'B5800', 'B5850') THEN 'PI_COATING_PSV3' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B5900', 'B5950') THEN 'EXPOSURE_PSV3' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B6000') THEN 'DEVELOP_PSV3' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B6050') THEN 'DEVELOP_INSP_PSV3' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B6100', 'B6150', 'B6200') THEN 'DESCUM_PSV3' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B6250', 'B6300') THEN 'SPUTTER_BUMP' " + "\n");

            strSqlString.Append("                            WHEN B.OPER IN ('B6350', 'B6400', 'B6450') THEN 'PR_COATING_BUMP' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B6500', 'B6550', 'B6600') THEN 'EXPOSURE_BUMP' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B6650') THEN 'DEVELOP_BUMP' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B6700') THEN 'MEASURING_INSP_BUMP' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B6750') THEN 'DESCUM_BUMP' " + "\n");

            strSqlString.Append("                            WHEN B.OPER IN ('B6800', 'B6850', 'B6900') THEN 'CU_PLATING_BUMP' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B6950', 'B7000', 'B7050') THEN 'NI_PLATING_BUMP' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B7100') THEN 'SN_AG_PLATING_BUMP' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B7150', 'B7200') THEN 'PR_STRIP_BUMP' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B7250', 'B7300') THEN 'CU_ETCH_BUMP' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B7350', 'B7400') THEN 'TI_ETCH_BUMP' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B7450', 'B7500') THEN 'BALL_MOUNT_BUMP' " + "\n");
            strSqlString.Append("                            WHEN B.OPER IN ('B7550', 'B7600') THEN 'REFLOW_BUMP' " + "\n");

            strSqlString.Append("                            ELSE ' ' " + "\n");
            strSqlString.Append("                        END OPER_GRP_1 " + "\n");
            strSqlString.Append("          FROM MWIPMATDEF A " + "\n");
            strSqlString.Append("             , MWIPFLWOPR@RPTTOMES B " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.FIRST_FLOW = B.FLOW " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("           AND A.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND A.MAT_TYPE = 'FG' " + "\n");
            
            // Bump에서는 조건 제외
            //strSqlString.Append("           AND A.MAT_GRP_5 IN ('-', 'Merge') " + "\n");

            strSqlString.Append("           AND A.MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

            if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

            if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

            if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

            if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

            if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

            if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

            if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

            if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

            if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

            if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

            if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);

            #endregion

            strSqlString.Append("       ) A " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("   AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = A.MAT_ID  " + "\n");
            strSqlString.Append(" GROUP BY " + QueryCond2 + " " + "\n");
            strSqlString.Append(" ORDER BY " + QueryCond4 + " " + "\n");

            return strSqlString.ToString();
        }

        #endregion

        #region EventHandler

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (CheckField() == false) return;
            GridColumnInit();
            spdData_Sheet1.RowCount = 0;
            
            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);

                if (cdvFactory.txtValue.Equals("HMKB1"))
                    dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringBump());
                else
                    dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());
                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }
                
                /*
                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                
                //spdData.DataSource = dt;
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 14, null, null, btnSort);

                spdData.RPT_FillDataSelectiveCells("Total", 0, 14, 0, 1, true, Align.Center, VerticalAlign.Center);
                //spdData.Sheets[0].FrozenColumnCount = 3;
                */
                spdData.DataSource = dt;
                //spdData.RPT_AutoFit(false);

                //dt.Dispose();
                if (cdvFactory.txtValue.Equals("HMKB1"))
                    SetOperCellStyleBump();
                else
                    SetOperCellStyle();
                SetTodayDefYn();

                if (ckdHideOper.Checked == true)
                {
                    ckdHideOper_CheckedChanged(null, null);
                }
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
        /// 그룹의 해당되지 않는 공정의 셀은 검은색으로 변환 - Bump 공정에 맞게 수정할것
        /// </summary>
        private void SetOperCellStyleBump()
        {
            int groupCnt = 0;
            int startCol = 0;
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            string[] list = tableForm.SelectedValue2ToQueryContainNull.Split(',');
            int[] groupList = new int[12];


            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] != "  ' '")
                {
                    groupList[groupCnt] = i;
                    groupCnt++;
                }
            }

            for (int i = 0; i < spdData.ActiveSheet.ColumnCount; i++)
            {
                if ("BUMP_REFLOW" == spdData.ActiveSheet.GetColumnLabel(1, i))
                {
                    startCol = i;
                    break;
                }
            }

            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringStepInfoBump());

            for (int i = dt.Columns.Count - 1; i >= 0; i--)
            {
                if (dt.Columns[i].ColumnName.IndexOf("''") != -1)
                    dt.Columns.RemoveAt(i);
            }

            //Font ff = new Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(200)));
            Color colorStep = System.Drawing.Color.FromArgb(((System.Byte)(200)), ((System.Byte)(200)), ((System.Byte)(200)));
            Color colorWip = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(251)), ((System.Byte)(170)));
            
            for (int i = 0; i < spdData.ActiveSheet.RowCount; i++)
            {
                //구분 컬럼이 Wip 일 때 셀을 노랗게 변환
                if ("WIP" == spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                {
                    for (int colCnt = 13; colCnt < 97; colCnt++)
                    {
                        spdData.ActiveSheet.Cells[i, colCnt].BackColor = colorWip;
                    }
                }

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    for (int k = 0; k < groupCnt; k++)
                    {
                        if (spdData.ActiveSheet.Cells[i, groupList[k]].Text == dt.Rows[j][k].ToString())
                        {
                            if (groupCnt - 1 == k)
                            {
                                //spdData.ActiveSheet.Cells[i, startCol].BackColor
                                if (dt.Rows[j]["REFLOW_BUMP"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol].Value = 0;
                                }
                                if (dt.Rows[j]["BALL_MOUNT_BUMP"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 1].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 1].Value = 0;
                                }
                                if (dt.Rows[j]["TI_ETCH_BUMP"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 2].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 2].Value = 0;
                                }
                                if (dt.Rows[j]["CU_ETCH_BUMP"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 3].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 3].Value = 0;
                                }
                                if (dt.Rows[j]["PR_STRIP_BUMP"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 4].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 4].Value = 0;
                                }
                                if (dt.Rows[j]["SN_AG_PLATING_BUMP"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 5].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 5].Value = 0;
                                }
                                if (dt.Rows[j]["NI_PLATING_BUMP"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 6].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 6].Value = 0;
                                }
                                if (dt.Rows[j]["CU_PLATING_BUMP"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 7].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 7].Value = 0;
                                }
                                if (dt.Rows[j]["DESCUM_BUMP"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 8].BackColor = colorStep;

                                    // 2014-01-08-임종우 : WIP 부분은 음영 처리 제외
                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 8].Value = 0;

                                }
                                if (dt.Rows[j]["MEASURING_INSP_BUMP"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 9].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 9].Value = 0;
                                }
                                
                                if (dt.Rows[j]["DEVELOP_BUMP"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 10].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 10].Value = 0;
                                }
                                if (dt.Rows[j]["EXPOSURE_BUMP"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 11].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 11].Value = 0;
                                }
                                if (dt.Rows[j]["PR_COATING_BUMP"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 12].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 12].Value = 0;
                                }
                                if (dt.Rows[j]["SPUTTER_BUMP"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 13].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 13].Value = 0;
                                }
                                if (dt.Rows[j]["DESCUM_PSV3"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 14].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 14].Value = 0;
                                }
                                if (dt.Rows[j]["DEVELOP_INSP_PSV3"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 15].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 15].Value = 0;
                                }
                                if (dt.Rows[j]["DEVELOP_PSV3"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 16].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 16].Value = 0;
                                }
                                if (dt.Rows[j]["EXPOSURE_PSV3"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 17].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 17].Value = 0;
                                }

                                if (dt.Rows[j]["PI_COATING_PSV3"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 18].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 15].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 18].Value = 0;
                                }
                                if (dt.Rows[j]["DESCUM_RDL3E"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 19].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 19].Value = 0;
                                }
                                if (dt.Rows[j]["TI_ETCH_RDL3"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 20].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 20].Value = 0;
                                }

                                if (dt.Rows[j]["CU_ETCH_RDL3"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 21].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 21].Value = 0;
                                }
                                if (dt.Rows[j]["PR_STRIP_RDL3"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 22].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 22].Value = 0;
                                }
                                if (dt.Rows[j]["CU_PLATING_RDL3"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 23].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 23].Value = 0;
                                }
                                if (dt.Rows[j]["DESCUM_RDL3P"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 24].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 24].Value = 0;
                                }
                                if (dt.Rows[j]["MEASURING_INSP_RDL3"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 25].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 25].Value = 0;
                                }
                                if (dt.Rows[j]["DEVELOP_RDL3"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 26].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 26].Value = 0;
                                }
                                if (dt.Rows[j]["EXPOSURE_RDL3"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 27].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 27].Value = 0;
                                }
                                if (dt.Rows[j]["PR_COATING_RDL3"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 28].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 28].Value = 0;
                                }
                                if (dt.Rows[j]["SPUTTER_RDL3"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 29].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 29].Value = 0;
                                }
                                if (dt.Rows[j]["DESCUM_PSV2"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 30].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 30].Value = 0;
                                }
                                if (dt.Rows[j]["CURE_PSV2"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 31].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 31].Value = 0;
                                }
                                if (dt.Rows[j]["DEVELOP_INSP_PSV2"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 32].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 32].Value = 0;
                                }
                                if (dt.Rows[j]["DEVELOP_PSV2"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 33].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 33].Value = 0;
                                }
                                if (dt.Rows[j]["EXPOSURE_PSV2"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 34].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 34].Value = 0;
                                }
                                if (dt.Rows[j]["PI_COATING_PSV2"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 35].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 35].Value = 0;
                                }
                                if (dt.Rows[j]["DESCUM_RDL2E"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 36].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 36].Value = 0;
                                }
                                if (dt.Rows[j]["TI_ETCH_RDL2"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 37].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 37].Value = 0;
                                }
                                if (dt.Rows[j]["CU_ETCH_RDL2"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 38].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 38].Value = 0;
                                }
                                if (dt.Rows[j]["PR_STRIP_RDL2"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 39].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 39].Value = 0;
                                }
                                if (dt.Rows[j]["CU_PLATING_RDL2"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 40].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 40].Value = 0;
                                }
                                if (dt.Rows[j]["DESCUM_RDL2P"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 41].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 41].Value = 0;
                                }
                                if (dt.Rows[j]["MEASURING_INSP_RDL2"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 42].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 42].Value = 0;
                                }
                                if (dt.Rows[j]["DEVELOP_RDL2"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 43].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 43].Value = 0;
                                }
                                if (dt.Rows[j]["EXPOSURE_RDL2"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 44].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 44].Value = 0;
                                }
                                if (dt.Rows[j]["PR_COATING_RDL2"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 45].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 45].Value = 0;
                                }
                                if (dt.Rows[j]["SPUTTER_RDL2"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 46].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 46].Value = 0;
                                }
                                if (dt.Rows[j]["DESCUM_PSV1"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 47].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 47].Value = 0;
                                }
                                if (dt.Rows[j]["CURE_PSV1"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 48].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 48].Value = 0;
                                }
                                if (dt.Rows[j]["DEVELOP_INSP_PSV1"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 49].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 49].Value = 0;
                                }
                                if (dt.Rows[j]["DEVELOP_PSV1"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 50].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 50].Value = 0;
                                }
                                if (dt.Rows[j]["EXPOSURE_PSV1"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 51].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 51].Value = 0;
                                }
                                if (dt.Rows[j]["PI_COATING_PSV1"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 52].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 52].Value = 0;
                                }
                                if (dt.Rows[j]["DESCUM_RDL1E"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 53].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 53].Value = 0;
                                }
                                if (dt.Rows[j]["TI_ETCH_RDL1"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 54].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 54].Value = 0;
                                }
                                if (dt.Rows[j]["CU_ETCH_RDL1"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 55].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 55].Value = 0;
                                }
                                if (dt.Rows[j]["PR_STRIP_RDL1"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 56].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 56].Value = 0;
                                }
                                if (dt.Rows[j]["CU_PLATING_RDL1"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 57].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 57].Value = 0;
                                }
                                if (dt.Rows[j]["DESCUM_RDL1P"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 58].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 58].Value = 0;
                                }
                                if (dt.Rows[j]["MEASURING_INSP_RDL1"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 59].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 59].Value = 0;
                                }
                                if (dt.Rows[j]["DEVELOP_RDL1"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 60].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 60].Value = 0;
                                }
                                if (dt.Rows[j]["EXPOSURE_RDL1"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 61].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 61].Value = 0;
                                }
                                if (dt.Rows[j]["PR_COATING_RDL1"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 62].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 62].Value = 0;
                                }
                                if (dt.Rows[j]["SPUTTER_RDL1"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 63].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 63].Value = 0;
                                }
                                if (dt.Rows[j]["DESCUM_RCF"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 64].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 64].Value = 0;
                                }
                                if (dt.Rows[j]["CURE_RCF"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 65].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 65].Value = 0;
                                }
                                if (dt.Rows[j]["DEVELOP_INSP_RCF"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 66].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 66].Value = 0;
                                }
                                if (dt.Rows[j]["DEVELOP_RCF"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 67].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 67].Value = 0;
                                }
                                if (dt.Rows[j]["EXPOSURE_RCF"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 68].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 68].Value = 0;
                                }
                                if (dt.Rows[j]["PI_COATING_RCF"].ToString() == "F")
                                {
                                    spdData.ActiveSheet.Cells[i, startCol + 69].BackColor = colorStep;

                                    if ("WIP" != spdData.ActiveSheet.Cells[i, 16].Value.ToString())
                                        spdData.ActiveSheet.Cells[i, startCol + 69].Value = 0;
                                }

                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 그룹의 해당되지 않는 공정의 셀은 검은색으로 변환
        /// </summary>
        private void SetOperCellStyle()
        {
            int groupCnt = 0;
            int startCol = 0;
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            

            string[] list = tableForm.SelectedValue2ToQueryContainNull.Split(',');
            int[] groupList = new int[16];


            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] != "  ' '")
                {
                    groupList[groupCnt] = i;
                    groupCnt++;
                }
            }
            for (int i = 0; i < spdData.ActiveSheet.ColumnCount; i++)
            {

                //1191214 이희석 F-CHIP 관점으로 인한 데이터 조건 추가
                if (cdvType.SelectedIndex == 3)
                {
                    if ("HSA CURE" == spdData.ActiveSheet.GetColumnLabel(1, i))
                    {
                        startCol = i;
                        break;
                    }
                }
                else if (cdvType.SelectedIndex == 4 || cdvType.SelectedIndex == 5 || cdvType.SelectedIndex == 6)
                {
                    if ("COB_AVI" == spdData.ActiveSheet.GetColumnLabel(1, i))
                    {
                        startCol = i;
                        break;
                    }
                }
                else
                {
                    if ("WB9" == spdData.ActiveSheet.GetColumnLabel(1, i))
                    {
                        startCol = i;
                        break;
                    }
                }

            }
            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringStepInfo());

            for (int i = dt.Columns.Count - 1; i >= 0; i--)
            {
                if (dt.Columns[i].ColumnName.IndexOf("''") != -1)
                    dt.Columns.RemoveAt(i);
            }

            //Font ff = new Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(200)));
            Color colorStep = System.Drawing.Color.FromArgb(((System.Byte)(200)), ((System.Byte)(200)), ((System.Byte)(200)));
            Color colorWip = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(251)), ((System.Byte)(170)));

            
            for (int i = 0; i < spdData.ActiveSheet.RowCount; i++)
            {

                //구분 컬럼이 Wip 일 때 셀을 노랗게 변환
                if ("WIP" == spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                {
                    for (int colCnt = 16; colCnt < spdData.ActiveSheet.ColumnCount; colCnt++)
                    {
                        spdData.ActiveSheet.Cells[i, colCnt].BackColor = colorWip;
                    }
                }


                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    

                    for (int k = 0; k < groupCnt; k++)
                    {                                                                               
                        
                        if (spdData.ActiveSheet.Cells[i, groupList[k]].Text == dt.Rows[j][k].ToString() )
                        {
                            if (groupCnt - 1 == k)
                            {                             
                                //1191214 이희석 F-CHIP 관점으로 인한 데이터 조건 추가
                                if (cdvType.SelectedIndex == 3)
                                {
                                    if (dt.Rows[j]["HSA CURE"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol].Value = 0;

                                    }
                                    if (dt.Rows[j]["H/S/A"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 1].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 1].Value = 0;
                                    }
                                    if (dt.Rows[j]["QC GATE 3"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 2].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 2].Value = 0;

                                    }
                                    if (dt.Rows[j]["BTM SMT"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 3].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 3].Value = 0;
                                    }
                                    if (dt.Rows[j]["Flipper"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 4].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 4].Value = 0;

                                    }
                                    if (dt.Rows[j]["U/F CURE"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 5].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 5].Value = 0;
                                    }
                                    if (dt.Rows[j]["U/F"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 6].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 6].Value = 0;

                                    }
                                    if (dt.Rows[j]["PLASMA 1"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 7].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 7].Value = 0;
                                    }
                                    if (dt.Rows[j]["PREBAKE 2"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 8].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 8].Value = 0;

                                    }
                                    if (dt.Rows[j]["DEFLUX"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 9].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 9].Value = 0;
                                    }
                                    if (dt.Rows[j]["C/A"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 10].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 10].Value = 0;
                                    }
                                    if (dt.Rows[j]["UV"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 11].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 11].Value = 0;
                                    }
                                }
                                else if (cdvType.SelectedIndex == 4)//DP 종합 음영처리
                                {
                                    if (dt.Rows[j]["COB_AVI"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol].Value = 0;

                                    }
                                    if (dt.Rows[j]["UV"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol+1].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 1].Value = 0;

                                    }
                                    if (dt.Rows[j]["W/E"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 2].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 2].Value = 0;

                                    }
                                    if (dt.Rows[j]["SAW"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 3].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 3].Value = 0;
                                    }
                                    if (dt.Rows[j]["LG"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 4].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 4].Value = 0;

                                    }
                                    if (dt.Rows[j]["TAPE_MOUNT"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 5].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 5].Value = 0;
                                    }
                                    if (dt.Rows[j]["B/S_MARKING"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 6].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 6].Value = 0;

                                    }
                                    if (dt.Rows[j]["COATING/T/CURE"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 7].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 7].Value = 0;
                                    }
                                    if (dt.Rows[j]["BS/COATING"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 8].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 8].Value = 0;

                                    }
                                    if (dt.Rows[j]["DETACH"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 9].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 9].Value = 0;

                                    }
                                    if (dt.Rows[j]["BG"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 10].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 10].Value = 0;

                                    }
                                    if (dt.Rows[j]["STEALTH"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 11].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 11].Value = 0;

                                    }
                                    if (dt.Rows[j]["PRE_B/G"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 12].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 12].Value = 0;

                                    }
                                    if (dt.Rows[j]["L/N"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 13].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 13].Value = 0;

                                    }
                                    if (dt.Rows[j]["DIE_BANK"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 14].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 14].Value = 0;

                                    }
                                }
                                else if (cdvType.SelectedIndex == 5)//DP Normal 관점 음영처리
                                {
                                    if (dt.Rows[j]["COB_AVI"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol].Value = 0;

                                    }
                                    if (dt.Rows[j]["UV"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 1].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 1].Value = 0;

                                    }
                                    if (dt.Rows[j]["W/E"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 2].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 2].Value = 0;

                                    }
                                    if (dt.Rows[j]["SAW"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 3].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 3].Value = 0;
                                    }
                                    if (dt.Rows[j]["LG"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 4].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 4].Value = 0;

                                    }
                                    if (dt.Rows[j]["BG"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 5].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 5].Value = 0;

                                    }
                                    if (dt.Rows[j]["STEALTH"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 6].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 6].Value = 0;

                                    }
                                    if (dt.Rows[j]["PRE_B/G"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 7].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 7].Value = 0;

                                    }
                                    if (dt.Rows[j]["L/N"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 8].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 8].Value = 0;

                                    }
                                    if (dt.Rows[j]["DIE_BANK"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 9].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 9].Value = 0;

                                    }
                                }
                                else if (cdvType.SelectedIndex == 6)//DP WLCSP 관점 음영 처리
                                {
                                    if (dt.Rows[j]["COB_AVI"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol].Value = 0;

                                    }
                                    if (dt.Rows[j]["UV"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 1].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 1].Value = 0;

                                    }
                                    if (dt.Rows[j]["SAW"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 2].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 2].Value = 0;
                                    }
                                    if (dt.Rows[j]["TAPE_MOUNT"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 3].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 3].Value = 0;
                                    }
                                    if (dt.Rows[j]["B/S_MARKING"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 4].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 4].Value = 0;

                                    }
                                    if (dt.Rows[j]["COATING/T/CURE"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 5].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 5].Value = 0;
                                    }
                                    if (dt.Rows[j]["BS/COATING"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 6].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 6].Value = 0;

                                    }
                                    if (dt.Rows[j]["DETACH"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 7].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 7].Value = 0;

                                    }
                                    if (dt.Rows[j]["BG"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 8].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 8].Value = 0;

                                    }
                                    if (dt.Rows[j]["L/N"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 9].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 9].Value = 0;

                                    }
                                    if (dt.Rows[j]["DIE_BANK"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 10].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 10].Value = 0;

                                    }
                                }
                                else
                                {

                                    //spdData.ActiveSheet.Cells[i, startCol].BackColor
                                    if (dt.Rows[j]["WB9"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol].Value = 0;

                                    }
                                    if (dt.Rows[j]["DA9"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 1].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 1].Value = 0;
                                    }
                                    if (dt.Rows[j]["WB8"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 2].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 2].Value = 0;

                                    }
                                    if (dt.Rows[j]["DA8"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 3].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 3].Value = 0;
                                    }
                                    if (dt.Rows[j]["WB7"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 4].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 4].Value = 0;

                                    }
                                    if (dt.Rows[j]["DA7"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 5].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 5].Value = 0;
                                    }
                                    if (dt.Rows[j]["WB6"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 6].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 6].Value = 0;

                                    }
                                    if (dt.Rows[j]["DA6"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 7].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 7].Value = 0;
                                    }
                                    if (dt.Rows[j]["WB5"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 8].BackColor = colorStep;

                                        // 2014-01-08-임종우 : WIP 부분은 음영 처리 제외
                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 8].Value = 0;

                                    }
                                    if (dt.Rows[j]["DA5"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 9].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 9].Value = 0;
                                    }
                                    if (dt.Rows[j]["WB4"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 10].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 10].Value = 0;
                                    }
                                    if (dt.Rows[j]["DA4"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 11].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 11].Value = 0;
                                    }
                                    if (dt.Rows[j]["WB3"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 12].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 12].Value = 0;
                                    }
                                    if (dt.Rows[j]["DA3"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 13].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 13].Value = 0;
                                    }
                                    if (dt.Rows[j]["WB2"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 14].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 14].Value = 0;
                                    }
                                    if (dt.Rows[j]["DA2"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 15].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 15].Value = 0;
                                    }
                                    if (dt.Rows[j]["WB1"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 16].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 16].Value = 0;
                                    }
                                    if (dt.Rows[j]["DA1"].ToString() == "F")
                                    {
                                        spdData.ActiveSheet.Cells[i, startCol + 17].BackColor = colorStep;

                                        if ("WIP" != spdData.ActiveSheet.Cells[i, 19].Value.ToString())
                                            spdData.ActiveSheet.Cells[i, startCol + 17].Value = 0;
                                    }
                                }
                                
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }


        }

        /// <summary>
        /// 금일 잔량이 남아있는 경우 PKG_CODE 컬럼색 변환
        /// </summary>
        private void SetTodayDefYn()
        {
            if (cdvGroup.Text != "ALL" && cdvGroup.Text.IndexOf("금일 잔량") == -1)
                return;

            int pkgColPos = -1;
            int customerColPos = -1;

            for (int i = 0; i < spdData.ActiveSheet.Columns.Count; i++)
            {
                if ("PKG_CODE" == spdData.ActiveSheet.GetColumnLabel(0, i).ToString())
                {
                    pkgColPos = i;
                    break;
                }
            }

            
            if (pkgColPos == -1)
                return;

            if(!spdData.ActiveSheet.ColumnHeader.Columns[pkgColPos].Visible)
                return;

            Color color = System.Drawing.Color.FromArgb(((System.Byte)(200)), ((System.Byte)(100)), ((System.Byte)(100)));
            DateTime nowTime = DateTime.Now;
            DateTime moldTime = new DateTime(Convert.ToInt32(cdvDate.Value.ToString("yyyy")), Convert.ToInt32(cdvDate.Value.ToString("MM")), Convert.ToInt32(cdvDate.Value.ToString("dd")), 12, 59, 59);
            DateTime wbTime = new DateTime(Convert.ToInt32(cdvDate.Value.ToString("yyyy")), Convert.ToInt32(cdvDate.Value.ToString("MM")), Convert.ToInt32(cdvDate.Value.ToString("dd")), 5, 59, 59);
            DateTime moldTimeHX = new DateTime(Convert.ToInt32(cdvDate.Value.ToString("yyyy")), Convert.ToInt32(cdvDate.Value.ToString("MM")), Convert.ToInt32(cdvDate.Value.ToString("dd")), 17, 59, 59);
            DateTime wbTimeHX = new DateTime(Convert.ToInt32(cdvDate.Value.ToString("yyyy")), Convert.ToInt32(cdvDate.Value.ToString("MM")), Convert.ToInt32(cdvDate.Value.ToString("dd")), 10, 59, 59);

            int convRowPos = -1;

            if (cdvGroup.Text == "ALL" || cdvGroup.Text.IndexOf(LanguageFunction.FindLanguage("the day's performance", 0)) != -1)
            {
                convRowPos--;
            }

            for (int i = 0; i < spdData.ActiveSheet.Columns.Count; i++)
            {
                if ("CUSTOMER" == spdData.ActiveSheet.GetColumnLabel(0, i).ToString().ToUpper())
                {
                    customerColPos = i;
                    break;
                }
            }
            

            if (nowTime > moldTime)
            {
                for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    if ("HYNIX" != spdData.ActiveSheet.Cells[i, customerColPos].Value.ToString())
                    {
                        //if ("금일 잔량" == spdData.ActiveSheet.Cells[i, 15].Value.ToString())
                        //{
                        //    if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, 25].Value) > 0)
                        //    {
                        //        spdData.ActiveSheet.Cells[i + convRowPos, pkgColPos].BackColor = color;
                        //    }
                        //}
                    }
                }
            }

            if (nowTime > wbTime)
            {
                for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    if ("HYNIX" != spdData.ActiveSheet.Cells[i, customerColPos].Value.ToString())
                    {
                        //if ("금일 잔량" == spdData.ActiveSheet.Cells[i, 15].Value.ToString())
                        //{
                        //    if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, 27].Value) > 0)
                        //    {
                        //        spdData.ActiveSheet.Cells[i + convRowPos, pkgColPos].BackColor = color;
                        //    }
                        //}
                    }
                }
            }

            if (nowTime > moldTimeHX)
            {
                for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    if ("HYNIX" == spdData.ActiveSheet.Cells[i, customerColPos].Value.ToString())
                    {
                        //if ("금일 잔량" == spdData.ActiveSheet.Cells[i, 15].Value.ToString())
                        //{
                        //    if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, 25].Value) > 0)
                        //    {
                        //        spdData.ActiveSheet.Cells[i + convRowPos, pkgColPos].BackColor = color;
                        //    }
                        //}
                    }
                }
            }

            if (nowTime > wbTimeHX)
            {
                for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    if ("HYNIX" == spdData.ActiveSheet.Cells[i, customerColPos].Value.ToString())
                    {
                        //if ("금일 잔량" == spdData.ActiveSheet.Cells[i, 15].Value.ToString())
                        //{
                        //    if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, 27].Value) > 0)
                        //    {
                        //        spdData.ActiveSheet.Cells[i + convRowPos, pkgColPos].BackColor = color;
                        //    }
                        //}
                    }
                }
            }

        }

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

        #endregion

        private void cdvGroup_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            if (cdvGroup.SelectCount == 0)
                cdvGroup.Text = "ALL";
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            try
            {
                string[] dataArry;
                string sOper = null;
                int iGubun = 19;

                if (cdvFactory.txtValue.Equals("HMKB1"))
                {
                    iGubun = iGubun - 3;
                    dataArry = new string[13];
                }
                else
                {
                    dataArry = new string[16];
                }


                // WIP 클릭 시 팝업 창 띄움
                if ((spdData.ActiveSheet.Cells[e.Row, iGubun].Value.ToString() == "WIP" || spdData.ActiveSheet.Cells[e.Row, iGubun].Value.ToString() == "설비대수") && e.Column > iGubun && spdData.ActiveSheet.Cells[e.Row, e.Column].Text != "")
                {
                    // Group 정보 가져와서 담기... 상세 팝업에서 조건값으로 사용하기 위해
                    if (cdvFactory.txtValue.Equals("HMKB1"))
                    {
                        for (int i = 0; i <= 12; i++)
                        {
                            if (spdData.ActiveSheet.Columns[i].Label == "Customer")
                                dataArry[0] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "BUMPING_TYPE")
                                dataArry[1] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "PROCESS_FLOW")
                                dataArry[2] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "LAYER")
                                dataArry[3] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "PKG_TYPE")
                                dataArry[4] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "RDL_PLATING")
                                dataArry[5] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "FINAL_BUMP")
                                dataArry[6] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "SUB_MATERIAL")
                                dataArry[7] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "WF_SIZE")
                                dataArry[8] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "THICKNESS")
                                dataArry[9] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "FLAT_TYPE")
                                dataArry[10] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "WAFER_ORIENTATION")
                                dataArry[11] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "PRODUCT")
                                dataArry[12] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();
                        }
                    }
                    else
                    {
                        for (int i = 0; i <= 15; i++)
                        {
                            if (spdData.ActiveSheet.Columns[i].Label == "Customer")
                                dataArry[0] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "MAJOR")
                                dataArry[1] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "PKG")
                                dataArry[2] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "LD_COUNT")
                                dataArry[3] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "PKG_CODE")
                                dataArry[4] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "FAMILY")
                                dataArry[5] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "PRODUCT")
                                dataArry[6] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "TYPE_1")
                                dataArry[7] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "TYPE_2")
                                dataArry[8] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "DENSITY")
                                dataArry[9] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "GENERATION")
                                dataArry[10] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "PIN_TYPE")
                                dataArry[11] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "CUST_DEVICE")
                                dataArry[12] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "SALES_CODE")
                                dataArry[13] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();
                        }
                    }

                    sOper = spdData.ActiveSheet.Columns[e.Column].Label;

                    if (spdData.ActiveSheet.Cells[e.Row, iGubun].Value.ToString() == "WIP")
                    {
                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetailWip(sOper, dataArry));

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            System.Windows.Forms.Form frm = new PRD011015_P1("", dt);
                            frm.ShowDialog();
                        }
                    }
                    else
                    {
                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetailRas(sOper, dataArry));

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            System.Windows.Forms.Form frm = new PRD011015_P2("", dt);
                            frm.ShowDialog();
                        }
                    }
                }
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

        private string MakeSqlDetailRas(string sOper, string[] dataArry)
        {
            string Today = null;

            StringBuilder strSqlString = new StringBuilder();

            Today = cdvDate.Value.ToString("yyyyMMdd");

            if (cdvFactory.txtValue.Equals("HMKB1"))
            {
                #region BUMP
                strSqlString.Append("SELECT A.RES_MODEL" + "\n");
                strSqlString.Append("     , SUM(A.RES_CNT) AS RES_CNT" + "\n");
                strSqlString.Append("     , MAX(B.UPEH) AS UPEH" + "\n");
                strSqlString.Append("     , SUM(TRUNC(NVL(NVL(B.UPEH,0) * 24 * A.PERCENT * A.RES_CNT, 0))) AS RES_CAPA" + "\n");
                strSqlString.Append("     , SUM(RUN_CNT) AS RUN_CNT" + "\n");
                strSqlString.Append("     , SUM(WAIT_CNT) AS WAIT_CNT" + "\n");                
                strSqlString.Append("     , SUM(RES_DOWN_CNT) AS RES_DOWN_CNT" + "\n");
                strSqlString.Append("     , SUM(DEV_CHANG_CNT) AS DEV_CHANG_CNT" + "\n");
                strSqlString.Append("  FROM ( " + "\n");
                strSqlString.Append("        SELECT RAS.FACTORY, RES_STS_2, RES_STS_8 AS OPER, RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, COUNT(RES_ID) AS RES_CNT " + "\n");
                //strSqlString.Append("             , DECODE(SUBSTR(RES_STS_8, 1, 3), 'A06', 0.75, 0.7 ) AS PERCENT " + "\n");
                strSqlString.Append("             , 0.75 AS PERCENT " + "\n");

                //strSqlString.Append("           , CASE WHEN RES_STS_8 IN ('B0000') THEN 'HMK2B' " + "\n");
                strSqlString.Append("             , CASE WHEN RES_STS_8 IN ('B0400') THEN 'IQC' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B0500') THEN 'I_STOCK' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B1100') THEN 'PI_COATING_RCF' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B1200') THEN 'EXPOSURE_RCF' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B1250') THEN 'DEVELOP_RCF' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B1300') THEN 'DEVELOP_INSP_RCF' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B1350') THEN 'CURE_RCF' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B1450') THEN 'DESCUM_RCF' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B1500') THEN 'SPUTTER_RDL1' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B1650') THEN 'PR_COATING_RDL1' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B1750') THEN 'EXPOSURE_RDL1' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B1800') THEN 'DEVELOP_RDL1' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B1850') THEN 'MEASURING_INSP_RDL1' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B1900') THEN 'DESCUM_RDL1P' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B2050') THEN 'CU_PLATING_RDL1' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B2150') THEN 'PR_STRIP_RDL1' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B2250') THEN 'CU_ETCH_RDL1' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B2350') THEN 'TI_ETCH_RDL1' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B2450') THEN 'DESCUM_RDL1E' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B2650') THEN 'PI_COATING_PSV1' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B2750') THEN 'EXPOSURE_PSV1' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B2800') THEN 'DEVELOP_PSV1' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B2850') THEN 'DEVELOP_INSP_PSV1' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B2900') THEN 'CURE_PSV1' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B3000') THEN 'DESCUM_PSV1' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B3100') THEN 'SPUTTER_RDL2' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B3250') THEN 'PR_COATING_RDL2' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B3350') THEN 'EXPOSURE_RDL2' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B3400') THEN 'DEVELOP_RDL2' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B3450') THEN 'MEASURING_INSP_RDL2' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B3500') THEN 'DESCUM_RDL2P' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B3650') THEN 'CU_PLATING_RDL2' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B3750') THEN 'PR_STRIP_RDL2' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B3850') THEN 'CU_ETCH_RDL2' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B3950') THEN 'TI_ETCH_RDL2' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B4050') THEN 'DESCUM_RDL2E' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B4250') THEN 'PI_COATING_PSV2' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B4350') THEN 'EXPOSURE_PSV2' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B4400') THEN 'DEVELOP_PSV2' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B4450') THEN 'DEVELOP_INSP_PSV2' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B4500') THEN 'CURE_PSV2' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B4600') THEN 'DESCUM_PSV2' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B4700') THEN 'SPUTTER_RDL3' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B4850') THEN 'PR_COATING_RDL3' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B4950') THEN 'EXPOSURE_RDL3' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B5000') THEN 'DEVELOP_RDL3' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B5050') THEN 'MEASURING_INSP_RDL3' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B5100') THEN 'DESCUM_RDL3P' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B5250') THEN 'CU_PLATING_RDL3' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B5350') THEN 'PR_STRIP_RDL3' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B5450') THEN 'CU_ETCH_RDL3' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B5550') THEN 'TI_ETCH_RDL3' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B5650') THEN 'DESCUM_RDL3E' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B5850') THEN 'PI_COATING_PSV3' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B5950') THEN 'EXPOSURE_PSV3' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B6000') THEN 'DEVELOP_PSV3' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B6050') THEN 'DEVELOP_INSP_PSV3' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B6200') THEN 'DESCUM_PSV3' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B6300') THEN 'SPUTTER_BUMP' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B6450') THEN 'PR_COATING_BUMP' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B6550') THEN 'EXPOSURE_BUMP' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B6650') THEN 'DEVELOP_BUMP' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B6700') THEN 'MEASURING_INSP_BUMP' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B6750') THEN 'DESCUM_BUMP' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B6900') THEN 'CU_PLATING_BUMP' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B7050') THEN 'NI_PLATING_BUMP' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B7100') THEN 'SN_AG_PLATING_BUMP' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B7200') THEN 'PR_STRIP_BUMP' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B7300') THEN 'CU_ETCH_BUMP' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B7400') THEN 'TI_ETCH_BUMP' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B7500') THEN 'BALL_MOUNT_BUMP' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B7600') THEN 'REFLOW_BUMP' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B7750') THEN 'FINAL_INSP' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B7800') THEN 'SORT1' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B7900') THEN 'AVI' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B9000') THEN 'OGI' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('B9100') THEN 'PACKING' " + "\n");
                //strSqlString.Append("                  WHEN RES_STS_8 IN ('BZ010') THEN '완제품창고' " + "\n");

                strSqlString.Append("                    ELSE ' ' " + "\n");
                strSqlString.Append("               END OPER_GRP_1 " + "\n");
                strSqlString.Append("             , SUM(DECODE(RES_UP_DOWN_FLAG, 'U', DECODE(NVL(LOT.START_RES_ID, '-'), '-', 1, 0), 0)) AS WAIT_CNT " + "\n");
                strSqlString.Append("             , SUM(DECODE(RES_UP_DOWN_FLAG, 'U', DECODE(NVL(LOT.START_RES_ID, '-'), '-', 0, 1), 0)) AS RUN_CNT " + "\n");
                strSqlString.Append("             , SUM(DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(NVL(SUBSTR(RES_STS_1, 1, 1), '-'), 'D', 0, 1), 0)) AS RES_DOWN_CNT " + "\n");
                strSqlString.Append("             , SUM(DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(NVL(SUBSTR(RES_STS_1, 1, 1), '-'), 'D', 1, 0), 0)) AS DEV_CHANG_CNT " + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == Today)
                {
                    strSqlString.Append("          FROM MRASRESDEF RAS" + "\n");
                    strSqlString.Append("             , (" + "\n");
                    strSqlString.Append("                SELECT DISTINCT FACTORY, START_RES_ID" + "\n");
                    strSqlString.Append("                  FROM MWIPLOTSTS" + "\n");
                    strSqlString.Append("                 WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                    //strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND LOT_DEL_FLAG = ' '" + "\n");
                    strSqlString.Append("                   AND LOT_CMF_5 LIKE 'P%'" + "\n");
                    strSqlString.Append("                   AND LOT_STATUS = 'PROC'" + "\n");
                    strSqlString.Append("               ) LOT" + "\n");
                    strSqlString.Append("         WHERE 1=1  " + "\n");
                }
                else
                {
                    strSqlString.Append("          FROM MRASRESDEF_BOH RAS" + "\n");
                    strSqlString.Append("             , (" + "\n");
                    strSqlString.Append("                SELECT DISTINCT FACTORY, START_RES_ID" + "\n");
                    strSqlString.Append("                  FROM MWIPLOTSTS_BOH" + "\n");
                    strSqlString.Append("                 WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                    //strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                   AND CUTOFF_DT = '" + Today + "22' " + "\n");
                    strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND LOT_DEL_FLAG = ' '" + "\n");
                    strSqlString.Append("                   AND LOT_CMF_5 LIKE 'P%'" + "\n");
                    strSqlString.Append("                   AND LOT_STATUS = 'PROC'" + "\n");
                    strSqlString.Append("               ) LOT" + "\n");
                    strSqlString.Append("         WHERE 1=1  " + "\n");
                    strSqlString.Append("           AND RAS.CUTOFF_DT = '" + Today + "22' " + "\n");
                }

                strSqlString.Append("           AND RAS.FACTORY = LOT.FACTORY(+) " + "\n");
                strSqlString.Append("           AND RAS.RES_ID = LOT.START_RES_ID(+) " + "\n");
                strSqlString.Append("           AND RAS.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                //strSqlString.Append("           AND RAS.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND RES_CMF_9 = 'Y' " + "\n");
                strSqlString.Append("           AND RES_CMF_7 = 'Y' " + "\n");
                strSqlString.Append("           AND DELETE_FLAG = ' ' " + "\n");
                strSqlString.Append("           AND RES_TYPE='EQUIPMENT' " + "\n");
                //strSqlString.Append("           AND RES_STS_1 NOT IN ('C200', 'B199') " + "\n");
                strSqlString.Append("         GROUP BY RAS.FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7  " + "\n");
                strSqlString.Append("       ) A " + "\n");
                strSqlString.Append("     , CRASUPHDEF B " + "\n");
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY "+ cdvFactory.SelectedValueToQueryString +" AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1) AS CUSTOMER" + "\n");
                strSqlString.Append("             , A.* " + "\n");
                strSqlString.Append("          FROM MWIPMATDEF A " + "\n");
                strSqlString.Append("       ) C " + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND A.FACTORY = B.FACTORY(+) " + "\n");
                strSqlString.Append("   AND A.FACTORY = C.FACTORY(+) " + "\n");
                strSqlString.Append("   AND A.OPER = B.OPER(+) " + "\n");
                strSqlString.Append("   AND A.RES_MODEL = B.RES_MODEL(+) " + "\n");
                strSqlString.Append("   AND A.UPEH_GRP = B.UPEH_GRP(+) " + "\n");
                strSqlString.Append("   AND A.RES_STS_2 = B.MAT_ID(+) " + "\n");
                strSqlString.Append("   AND A.RES_STS_2 = C.MAT_ID(+) " + "\n");
                strSqlString.Append("   AND A.FACTORY  " + cdvFactory.SelectedValueToQueryString + "\n");
                //strSqlString.Append("   AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("   AND A.OPER NOT IN ('00001','00002') " + "\n");
                strSqlString.Append("   AND A.OPER_GRP_1 = '" + sOper + "'" + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (dataArry[0] != " " && dataArry[0] != null)
                    strSqlString.AppendFormat("   AND C.CUSTOMER = '" + dataArry[0] + "'" + "\n");

                if (dataArry[1] != " " && dataArry[1] != null)
                    strSqlString.AppendFormat("   AND C.MAT_GRP_2 = '" + dataArry[1] + "'" + "\n");

                if (dataArry[2] != " " && dataArry[2] != null)
                    strSqlString.AppendFormat("   AND C.MAT_GRP_3 = '" + dataArry[2] + "'" + "\n");

                if (dataArry[3] != " " && dataArry[3] != null)
                    strSqlString.AppendFormat("   AND C.MAT_GRP_4 = '" + dataArry[3] + "'" + "\n");

                if (dataArry[4] != " " && dataArry[4] != null)
                    strSqlString.AppendFormat("   AND C.MAT_GRP_5 = '" + dataArry[4] + "'" + "\n");

                if (dataArry[5] != " " && dataArry[5] != null)
                    strSqlString.AppendFormat("   AND C.MAT_GRP_6 = '" + dataArry[5] + "'" + "\n");

                if (dataArry[6] != " " && dataArry[6] != null)
                    strSqlString.AppendFormat("   AND C.MAT_GRP_7 = '" + dataArry[6] + "'" + "\n");

                if (dataArry[7] != " " && dataArry[7] != null)
                    strSqlString.AppendFormat("   AND C.MAT_GRP_8 = '" + dataArry[7] + "'" + "\n");

                if (dataArry[8] != " " && dataArry[8] != null)
                    strSqlString.AppendFormat("   AND C.MAT_CMF_14 = '" + dataArry[8] + "'" + "\n");

                if (dataArry[9] != " " && dataArry[9] != null)
                    strSqlString.AppendFormat("   AND C.MAT_CMF_2 = '" + dataArry[9] + "'" + "\n");

                if (dataArry[10] != " " && dataArry[10] != null)
                    strSqlString.AppendFormat("   AND C.MAT_CMF_3 = '" + dataArry[10] + "'" + "\n");

                if (dataArry[11] != " " && dataArry[11] != null)
                    strSqlString.AppendFormat("   AND C.MAT_CMF_4 = '" + dataArry[11] + "'" + "\n");

                if (dataArry[12] != " " && dataArry[12] != null)
                    strSqlString.AppendFormat("   AND C.MAT_ID = '" + dataArry[12] + "'" + "\n");

                #endregion

                strSqlString.Append(" GROUP BY A.RES_MODEL " + "\n");
                strSqlString.Append(" ORDER BY A.RES_MODEL " + "\n");

                #endregion
            }
            else
            {
                #region ASSY
                strSqlString.Append("SELECT A.RES_MODEL" + "\n");
                strSqlString.Append("     , SUM(A.RES_CNT) AS RES_CNT" + "\n");
                strSqlString.Append("     , MAX(B.UPEH) AS UPEH" + "\n");
                strSqlString.Append("     , SUM(TRUNC(NVL(NVL(B.UPEH,0) * 24 * A.PERCENT * A.RES_CNT, 0))) AS RES_CAPA" + "\n");
                strSqlString.Append("     , SUM(RUN_CNT) AS RUN_CNT" + "\n");
                strSqlString.Append("     , SUM(WAIT_CNT) AS WAIT_CNT" + "\n");                
                strSqlString.Append("     , SUM(RES_DOWN_CNT) AS RES_DOWN_CNT" + "\n");
                strSqlString.Append("     , SUM(DEV_CHANG_CNT) AS DEV_CHANG_CNT" + "\n");
                strSqlString.Append("  FROM ( " + "\n");
                strSqlString.Append("        SELECT RAS.FACTORY, RES_STS_2, RES_STS_8 AS OPER, RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, COUNT(RES_ID) AS RES_CNT " + "\n");
                //strSqlString.Append("             , DECODE(SUBSTR(RES_STS_8, 1, 3), 'A06', 0.75, 0.7 ) AS PERCENT " + "\n");
                strSqlString.Append("             , CASE WHEN RES_STS_8 LIKE 'A06%' THEN " + GlobalVariable.gdPer_wb + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 LIKE 'A04%' THEN " + GlobalVariable.gdPer_da + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 = 'A0333' THEN " + GlobalVariable.gdPer_da + "\n");
                strSqlString.Append("                    ELSE " + GlobalVariable.gdPer_etc + "\n");
                strSqlString.Append("               END PERCENT " + "\n");
                strSqlString.Append("             , CASE WHEN RES_STS_8 IN ('A0040') THEN 'BG' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A0200', 'A0230') THEN 'SAW' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A0400', 'A0401', 'A0333') THEN 'DA1' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A0402') THEN 'DA2' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A0403') THEN 'DA3' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A0404') THEN 'DA4' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A0405') THEN 'DA5' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A0406') THEN 'DA6' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A0407') THEN 'DA7' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A0408') THEN 'DA8' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A0409') THEN 'DA9' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A0600','A0601') THEN 'WB1' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A0602') THEN 'WB2' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A0603') THEN 'WB3' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A0604') THEN 'WB4' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A0605') THEN 'WB5' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A0606') THEN 'WB6' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A0607') THEN 'WB7' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A0608') THEN 'WB8' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A0609') THEN 'WB9' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A0800', 'A0801', 'A0802', 'A0803', 'A0804', 'A0805', 'A0806', 'A0807', 'A0808', 'A0809') THEN 'GATE' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A1000', 'A0910') THEN 'MOLD' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A1100') THEN 'CURE' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A1150', 'A1500') THEN 'MK' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A1200') THEN 'TRIM' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A1450') THEN 'TIN' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A1300') THEN 'SBA' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A1750', 'A1800', 'A1900', 'A1825') THEN 'SIG' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A2000') THEN 'AVI' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A2050') THEN 'PVI' " + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 IN ('A2100') THEN 'QC_GATE' " + "\n");
                strSqlString.Append("                    ELSE ' ' " + "\n");
                strSqlString.Append("               END OPER_GRP_1 " + "\n");
                strSqlString.Append("             , SUM(DECODE(RES_UP_DOWN_FLAG, 'U', DECODE(NVL(LOT.START_RES_ID, '-'), '-', 1, 0), 0)) AS WAIT_CNT " + "\n");
                strSqlString.Append("             , SUM(DECODE(RES_UP_DOWN_FLAG, 'U', DECODE(NVL(LOT.START_RES_ID, '-'), '-', 0, 1), 0)) AS RUN_CNT " + "\n");
                strSqlString.Append("             , SUM(DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(NVL(SUBSTR(RES_STS_1, 1, 1), '-'), 'D', 0, 1), 0)) AS RES_DOWN_CNT " + "\n");
                strSqlString.Append("             , SUM(DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(NVL(SUBSTR(RES_STS_1, 1, 1), '-'), 'D', 1, 0), 0)) AS DEV_CHANG_CNT " + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == Today)
                {
                    strSqlString.Append("          FROM MRASRESDEF RAS" + "\n");
                    strSqlString.Append("             , (" + "\n");
                    strSqlString.Append("                SELECT DISTINCT FACTORY, START_RES_ID" + "\n");
                    strSqlString.Append("                  FROM MWIPLOTSTS" + "\n");
                    strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND LOT_DEL_FLAG = ' '" + "\n");
                    strSqlString.Append("                   AND LOT_CMF_5 LIKE 'P%'" + "\n");
                    strSqlString.Append("                   AND LOT_STATUS = 'PROC'" + "\n");
                    strSqlString.Append("               ) LOT" + "\n");
                    strSqlString.Append("         WHERE 1=1  " + "\n");
                }
                else
                {
                    strSqlString.Append("          FROM MRASRESDEF_BOH RAS" + "\n");
                    strSqlString.Append("             , (" + "\n");
                    strSqlString.Append("                SELECT DISTINCT FACTORY, START_RES_ID" + "\n");
                    strSqlString.Append("                  FROM MWIPLOTSTS_BOH" + "\n");
                    strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                   AND CUTOFF_DT = '" + Today + "22' " + "\n");
                    strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND LOT_DEL_FLAG = ' '" + "\n");
                    strSqlString.Append("                   AND LOT_CMF_5 LIKE 'P%'" + "\n");
                    strSqlString.Append("                   AND LOT_STATUS = 'PROC'" + "\n");
                    strSqlString.Append("               ) LOT" + "\n");
                    strSqlString.Append("         WHERE 1=1  " + "\n");
                    strSqlString.Append("           AND RAS.CUTOFF_DT = '" + Today + "22' " + "\n");
                }

                strSqlString.Append("           AND RAS.FACTORY = LOT.FACTORY(+) " + "\n");
                strSqlString.Append("           AND RAS.RES_ID = LOT.START_RES_ID(+) " + "\n");
                strSqlString.Append("           AND RAS.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND RES_CMF_9 = 'Y' " + "\n");
                strSqlString.Append("           AND RES_CMF_7 = 'Y' " + "\n");
                strSqlString.Append("           AND DELETE_FLAG = ' ' " + "\n");
                strSqlString.Append("           AND RES_TYPE='EQUIPMENT' " + "\n");
                //strSqlString.Append("           AND RES_STS_1 NOT IN ('C200', 'B199') " + "\n");
                strSqlString.Append("           AND (RES_STS_1 NOT IN ('C200', 'B199', 'B800') OR RES_UP_DOWN_FLAG = 'U') " + "\n");
                strSqlString.Append("         GROUP BY RAS.FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7  " + "\n");
                strSqlString.Append("       ) A " + "\n");
                strSqlString.Append("     , CRASUPHDEF B " + "\n");
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1) AS CUSTOMER" + "\n");
                strSqlString.Append("             , A.* " + "\n");
                strSqlString.Append("          FROM MWIPMATDEF A " + "\n");
                strSqlString.Append("       ) C " + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND A.FACTORY = B.FACTORY(+) " + "\n");
                strSqlString.Append("   AND A.FACTORY = C.FACTORY(+) " + "\n");
                strSqlString.Append("   AND A.OPER = B.OPER(+) " + "\n");
                strSqlString.Append("   AND A.RES_MODEL = B.RES_MODEL(+) " + "\n");
                strSqlString.Append("   AND A.UPEH_GRP = B.UPEH_GRP(+) " + "\n");
                strSqlString.Append("   AND A.RES_STS_2 = B.MAT_ID(+) " + "\n");
                strSqlString.Append("   AND A.RES_STS_2 = C.MAT_ID(+) " + "\n");
                strSqlString.Append("   AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("   AND A.OPER NOT IN ('00001','00002') " + "\n");
                strSqlString.Append("   AND A.OPER_GRP_1 = '" + sOper + "'" + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (dataArry[0] != " " && dataArry[0] != null)
                    strSqlString.AppendFormat("   AND C.CUSTOMER = '" + dataArry[0] + "'" + "\n");

                if (dataArry[1] != " " && dataArry[1] != null)
                    strSqlString.AppendFormat("   AND C.MAT_GRP_9 = '" + dataArry[1] + "'" + "\n");

                if (dataArry[2] != " " && dataArry[2] != null)
                    strSqlString.AppendFormat("   AND C.MAT_GRP_10 = '" + dataArry[2] + "'" + "\n");

                if (dataArry[3] != " " && dataArry[3] != null)
                    strSqlString.AppendFormat("   AND C.MAT_GRP_6 = '" + dataArry[3] + "'" + "\n");

                if (dataArry[4] != " " && dataArry[4] != null)
                    strSqlString.AppendFormat("   AND C.MAT_CMF_11 = '" + dataArry[4] + "'" + "\n");

                if (dataArry[5] != " " && dataArry[5] != null)
                    strSqlString.AppendFormat("   AND C.MAT_GRP_2 = '" + dataArry[5] + "'" + "\n");

                if (dataArry[6] != " " && dataArry[6] != null)
                    strSqlString.AppendFormat("   AND C.MAT_ID = '" + dataArry[6] + "'" + "\n");

                if (dataArry[7] != " " && dataArry[7] != null)
                    strSqlString.AppendFormat("   AND C.MAT_GRP_4 = '" + dataArry[7] + "'" + "\n");

                if (dataArry[8] != " " && dataArry[8] != null)
                    strSqlString.AppendFormat("   AND C.MAT_GRP_5 = '" + dataArry[8] + "'" + "\n");

                if (dataArry[9] != " " && dataArry[9] != null)
                    strSqlString.AppendFormat("   AND C.MAT_GRP_7 = '" + dataArry[9] + "'" + "\n");

                if (dataArry[10] != " " && dataArry[10] != null)
                    strSqlString.AppendFormat("   AND C.MAT_GRP_8 = '" + dataArry[10] + "'" + "\n");

                if (dataArry[11] != " " && dataArry[11] != null)
                    strSqlString.AppendFormat("   AND C.MAT_CMF_10 = '" + dataArry[11] + "'" + "\n");

                if (dataArry[12] != " " && dataArry[12] != null)
                    strSqlString.AppendFormat("   AND C.MAT_CMF_7 = '" + dataArry[12] + "'" + "\n");

                if (dataArry[13] != " " && dataArry[13] != null)
                    strSqlString.AppendFormat("   AND C.MAT_CMF_8 = '" + dataArry[13] + "'" + "\n");

                #endregion

                strSqlString.Append(" GROUP BY A.RES_MODEL " + "\n");
                strSqlString.Append(" ORDER BY A.RES_MODEL " + "\n");
                #endregion
            }
           
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private string MakeSqlDetailWip(string sOper, string[] dataArry)
        {
            string Today = null;

            StringBuilder strSqlString = new StringBuilder();

            Today = cdvDate.Value.ToString("yyyyMMdd");

            if (cdvFactory.txtValue.Equals("HMKB1"))
            {
                #region BUMP
                strSqlString.Append("SELECT OPER" + "\n");
                strSqlString.Append("     , MAT_ID" + "\n");
                strSqlString.Append("     , LOT_ID" + "\n");
                strSqlString.Append("     , QTY_1" + "\n");
                strSqlString.Append("     , DECODE(HOLD_FLAG, 'Y', 'HOLD', DECODE(LOT_STATUS, 'PROC', 'RUN', 'WAIT')) AS STATUS" + "\n");
                strSqlString.Append("     , START_RES_ID" + "\n");
                strSqlString.Append("     , RES_MODEL" + "\n");
                strSqlString.Append("     , SUB_AREA_ID" + "\n");
                strSqlString.Append("     , CHIP_CNT" + "\n");
                strSqlString.Append("     , UPEH" + "\n");
                strSqlString.Append("     , TO_CHAR(SYSDATE + ((NVL(QTY_1,0) - NVL(CHIP_CNT,0)) / UPEH / 24), 'YYYY/MM/DD HH24:MI:SS') AS SCH_END_TIME" + "\n");
                strSqlString.Append("     , TO_CHAR(TO_DATE(OPER_IN_TIME, 'YYYYMMDDHH24MISS'), 'YYYY/MM/DD HH24:MI:SS') AS OPER_IN_TIME" + "\n");
                strSqlString.Append("     , DECODE(START_TIME, ' ', ' ', TO_CHAR(TO_DATE(START_TIME, 'YYYYMMDDHH24MISS'), 'YYYY/MM/DD HH24:MI:SS')) AS START_TIME" + "\n");
                strSqlString.Append("     , TRUNC(SYSDATE-TO_DATE(OPER_IN_TIME,'YYYYMMDDHH24MISS')) || '일 ' ||" + "\n");
                strSqlString.Append("       TRUNC(MOD((SYSDATE-TO_DATE(OPER_IN_TIME,'YYYYMMDDHH24MISS')),1)*24)|| '시간 ' ||" + "\n");
                strSqlString.Append("       TRUNC(MOD((SYSDATE-TO_DATE(OPER_IN_TIME,'YYYYMMDDHH24MISS'))*24,1)*60)|| '분 '" + "\n");
                strSqlString.Append("       AS TIME_INTERVAL" + "\n");
                strSqlString.Append("     , LAST_COMMENT" + "\n");
                strSqlString.Append("  FROM ( " + "\n");
                strSqlString.Append("        SELECT A.*" + "\n");
                strSqlString.Append("             , (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + " AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1) AS CUSTOMER" + "\n");
                //strSqlString.Append("             , B.MAT_GRP_2, B.MAT_GRP_4, B.MAT_GRP_5, B.MAT_GRP_6, B.MAT_GRP_7, B.MAT_GRP_8, B.MAT_GRP_9, B.MAT_GRP_10, B.MAT_CMF_10, B.MAT_CMF_11" + "\n");
                strSqlString.Append("             , B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_4, B.MAT_GRP_5, B.MAT_GRP_6, B.MAT_GRP_7, B.MAT_GRP_8, B.MAT_CMF_14, B.MAT_CMF_2, B.MAT_CMF_3, B.MAT_CMF_4" + "\n");


                strSqlString.Append("             , CASE WHEN A.OPER IN ('B0000') THEN 'HMK2B' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B0400') THEN 'IQC' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B0500') THEN 'I_STOCK' " + "\n");

                strSqlString.Append("                    WHEN A.OPER IN ('B1000', 'B1050', 'B1100') THEN 'PI_COATING_RCF' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B1150', 'B1200') THEN 'EXPOSURE_RCF' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B1250') THEN 'DEVELOP_RCF' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B1300') THEN 'DEVELOP_INSP_RCF' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B1350') THEN 'CURE_RCF' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B1400', 'B1450') THEN 'DESCUM_RCF' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B1500') THEN 'SPUTTER_RDL1' " + "\n");

                strSqlString.Append("                    WHEN A.OPER IN ('B1550', 'B1600', 'B1650') THEN 'PR_COATING_RDL1' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B1700', 'B1750') THEN 'EXPOSURE_RDL1' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B1800') THEN 'DEVELOP_RDL1' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B1850') THEN 'MEASURING_INSP_RDL1' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B1900') THEN 'DESCUM_RDL1P' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B1950', 'B2000', 'B2050') THEN 'CU_PLATING_RDL1' " + "\n");

                strSqlString.Append("                    WHEN A.OPER IN ('B2100', 'B2150') THEN 'PR_STRIP_RDL1' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B2200', 'B2250') THEN 'CU_ETCH_RDL1' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B2300', 'B2350') THEN 'TI_ETCH_RDL1' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B2400', 'B2450') THEN 'DESCUM_RDL1E' " + "\n");

                strSqlString.Append("                    WHEN A.OPER IN ('B2500', 'B2550', 'B2600', 'B2650') THEN 'PI_COATING_PSV1' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B2700', 'B2750') THEN 'EXPOSURE_PSV1' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B2800') THEN 'DEVELOP_PSV1' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B2850') THEN 'DEVELOP_INSP_PSV1' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B2900') THEN 'CURE_PSV1' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B2950', 'B3000') THEN 'DESCUM_PSV1' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B3050', 'B3100') THEN 'SPUTTER_RDL2' " + "\n");

                strSqlString.Append("                    WHEN A.OPER IN ('B3150', 'B3200', 'B3250') THEN 'PR_COATING_RDL2' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B3300', 'B3350') THEN 'EXPOSURE_RDL2' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B3400') THEN 'DEVELOP_RDL2' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B3450') THEN 'MEASURING_INSP_RDL2' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B3500') THEN 'DESCUM_RDL2P' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B3550', 'B3600', 'B3650') THEN 'CU_PLATING_RDL2' " + "\n");

                strSqlString.Append("                    WHEN A.OPER IN ('B3700', 'B3750') THEN 'PR_STRIP_RDL2' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B3800', 'B3850') THEN 'CU_ETCH_RDL2' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B3900', 'B3950') THEN 'TI_ETCH_RDL2' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B4000', 'B4050') THEN 'DESCUM_RDL2E' " + "\n");

                strSqlString.Append("                    WHEN A.OPER IN ('B4100', 'B4150', 'B4200', 'B4250') THEN 'PI_COATING_PSV2' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B4300', 'B4350') THEN 'EXPOSURE_PSV2' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B4400') THEN 'DEVELOP_PSV2' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B4450') THEN 'DEVELOP_INSP_PSV2' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B4500') THEN 'CURE_PSV2' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B4550', 'B4600') THEN 'DESCUM_PSV2' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B4650', 'B4700') THEN 'SPUTTER_RDL3' " + "\n");

                strSqlString.Append("                    WHEN A.OPER IN ('B4750', 'B4800', 'B4850') THEN 'PR_COATING_RDL3' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B4900', 'B4950') THEN 'EXPOSURE_RDL3' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B5000') THEN 'DEVELOP_RDL3' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B5050') THEN 'MEASURING_INSP_RDL3' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B5100') THEN 'DESCUM_RDL3P' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B5150', 'B5200', 'B5250') THEN 'CU_PLATING_RDL3' " + "\n");

                strSqlString.Append("                    WHEN A.OPER IN ('B5300', 'B5350') THEN 'PR_STRIP_RDL3' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B5400', 'B5450') THEN 'CU_ETCH_RDL3' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B5500', 'B5550') THEN 'TI_ETCH_RDL3' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B5600', 'B5650') THEN 'DESCUM_RDL3E' " + "\n");

                strSqlString.Append("                    WHEN A.OPER IN ('B5700', 'B5750', 'B5800', 'B5850') THEN 'PI_COATING_PSV3' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B5900', 'B5950') THEN 'EXPOSURE_PSV3' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B6000') THEN 'DEVELOP_PSV3' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B6050') THEN 'DEVELOP_INSP_PSV3' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B6100', 'B6150', 'B6200') THEN 'DESCUM_PSV3' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B6250', 'B6300') THEN 'SPUTTER_BUMP' " + "\n");

                strSqlString.Append("                    WHEN A.OPER IN ('B6350', 'B6400', 'B6450') THEN 'PR_COATING_BUMP' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B6500', 'B6550', 'B6600') THEN 'EXPOSURE_BUMP' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B6650') THEN 'DEVELOP_BUMP' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B6700') THEN 'MEASURING_INSP_BUMP' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B6750') THEN 'DESCUM_BUMP' " + "\n");

                strSqlString.Append("                    WHEN A.OPER IN ('B6800', 'B6850', 'B6900') THEN 'CU_PLATING_BUMP' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B6950', 'B7000', 'B7050') THEN 'NI_PLATING_BUMP' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B7100') THEN 'SN_AG_PLATING_BUMP' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B7150', 'B7200') THEN 'PR_STRIP_BUMP' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B7250', 'B7300') THEN 'CU_ETCH_BUMP' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B7350', 'B7400') THEN 'TI_ETCH_BUMP' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B7450', 'B7500') THEN 'BALL_MOUNT_BUMP' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B7550', 'B7600') THEN 'REFLOW_BUMP' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B7650', 'B7700', 'B7750') THEN 'FINAL_INSP' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B7800') THEN 'SORT1' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B7850', 'B7900') THEN 'AVI' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B9000') THEN 'OGI' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('B9100') THEN 'PACKING' " + "\n");
                strSqlString.Append("                    WHEN A.OPER IN ('BZ010') THEN '완제품창고' " + "\n");

                strSqlString.Append("                    ELSE ' ' " + "\n");
                strSqlString.Append("                END OPER_GRP" + "\n");
                strSqlString.Append("             , (SELECT CHIP_CNT FROM MRESCNTHIS@RPTTOMES WHERE LOT_ID = A.LOT_ID AND OPER = A.OPER AND LINK != 'A') AS CHIP_CNT" + "\n");
                strSqlString.Append("             , C.*" + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == Today)
                {
                    strSqlString.Append("          FROM RWIPLOTSTS A " + "\n");
                    strSqlString.Append("             , MWIPMATDEF B " + "\n");
                    strSqlString.Append("             , (" + "\n");
                    strSqlString.Append("                SELECT RES_ID, RES_GRP_6 AS RES_MODEL, SUB_AREA_ID, UPEH" + "\n");
                    strSqlString.Append("                  FROM MRASRESDEF RES" + "\n");
                    strSqlString.Append("                     , CRASUPHDEF UPH" + "\n");
                    strSqlString.Append("                 WHERE 1=1" + "\n");
                    strSqlString.Append("                   AND RES.FACTORY = UPH.FACTORY(+)" + "\n");
                    strSqlString.Append("                   AND RES.RES_STS_8 = UPH.OPER(+)" + "\n");
                    strSqlString.Append("                   AND RES.RES_GRP_6 = UPH.RES_MODEL(+) " + "\n");
                    strSqlString.Append("                   AND RES.RES_GRP_7 = UPH.UPEH_GRP(+) " + "\n");
                    strSqlString.Append("                   AND RES.RES_STS_2 = UPH.MAT_ID(+)" + "\n");
                    strSqlString.Append("                   AND RES.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                    //strSqlString.Append("                   AND RES.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND RES.RES_STS_8 NOT IN ('00001','00002')" + "\n");
                    strSqlString.Append("                   AND RES.RES_CMF_9 = 'Y' " + "\n");
                    strSqlString.Append("                   AND RES.RES_CMF_7 = 'Y' " + "\n");
                    strSqlString.Append("                   AND RES.DELETE_FLAG = ' ' " + "\n");
                    strSqlString.Append("                   AND RES.RES_TYPE='EQUIPMENT' " + "\n");
                    strSqlString.Append("               ) C" + "\n");
                    strSqlString.Append("         WHERE 1 = 1  " + "\n");
                }
                else
                {
                    strSqlString.Append("          FROM RWIPLOTSTS_BOH A " + "\n");
                    strSqlString.Append("             , MWIPMATDEF B " + "\n");
                    strSqlString.Append("             , (" + "\n");
                    strSqlString.Append("                SELECT RES_ID, RES_GRP_6 AS RES_MODEL, SUB_AREA_ID, UPEH" + "\n");

                    strSqlString.Append("                  FROM MRASRESDEF_BOH RES" + "\n");
                    strSqlString.Append("                     , CRASUPHDEF UPH" + "\n");
                    strSqlString.Append("                 WHERE 1=1 " + "\n");
                    strSqlString.Append("                   AND RES.CUTOFF_DT = '" + Today + "22' " + "\n");
                    strSqlString.Append("                   AND RES.FACTORY = UPH.FACTORY(+)" + "\n");
                    strSqlString.Append("                   AND RES.RES_STS_8 = UPH.OPER(+)" + "\n");
                    strSqlString.Append("                   AND RES.RES_GRP_6 = UPH.RES_MODEL(+) " + "\n");
                    strSqlString.Append("                   AND RES.RES_GRP_7 = UPH.UPEH_GRP(+) " + "\n");
                    strSqlString.Append("                   AND RES.RES_STS_2 = UPH.MAT_ID(+)" + "\n");
                    strSqlString.Append("                   AND RES.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                    //strSqlString.Append("                   AND RES.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND RES.RES_STS_8 NOT IN ('00001','00002')" + "\n");
                    strSqlString.Append("                   AND RES.RES_CMF_9 = 'Y' " + "\n");
                    strSqlString.Append("                   AND RES.RES_CMF_7 = 'Y' " + "\n");
                    strSqlString.Append("                   AND RES.DELETE_FLAG = ' ' " + "\n");
                    strSqlString.Append("                   AND RES.RES_TYPE='EQUIPMENT' " + "\n");
                    strSqlString.Append("               ) C" + "\n");
                    strSqlString.Append("         WHERE A.CUTOFF_DT = '" + Today + "22' " + "\n");
                }

                strSqlString.Append("           AND A.FACTORY = B.FACTORY" + "\n");
                strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("           AND A.START_RES_ID = C.RES_ID(+)" + "\n");
                strSqlString.Append("           AND A.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                //strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'   " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W'  " + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("           AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("           AND A.LOT_DEL_FLAG = ' '  " + "\n");
                strSqlString.Append("           AND B.MAT_GRP_2 <> '-' " + "\n");
                strSqlString.Append("           AND B.DELETE_FLAG = ' '   " + "\n");
                strSqlString.Append("         ORDER BY OPER, LOT_ID" + "\n");
                strSqlString.Append("       )" + "\n");
                strSqlString.Append("   WHERE 1=1" + "\n");
                strSqlString.Append("     AND OPER_GRP = '" + sOper + "'" + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (dataArry[0] != " " && dataArry[0] != null)
                    strSqlString.AppendFormat("   AND CUSTOMER = '" + dataArry[0] + "'" + "\n");

                if (dataArry[1] != " " && dataArry[1] != null)
                    strSqlString.AppendFormat("   AND MAT_GRP_2 = '" + dataArry[1] + "'" + "\n");

                if (dataArry[2] != " " && dataArry[2] != null)
                    strSqlString.AppendFormat("   AND MAT_GRP_3 = '" + dataArry[2] + "'" + "\n");

                if (dataArry[3] != " " && dataArry[3] != null)
                    strSqlString.AppendFormat("   AND MAT_GRP_4 = '" + dataArry[3] + "'" + "\n");

                if (dataArry[4] != " " && dataArry[4] != null)
                    strSqlString.AppendFormat("   AND MAT_GRP_5 = '" + dataArry[4] + "'" + "\n");

                if (dataArry[5] != " " && dataArry[5] != null)
                    strSqlString.AppendFormat("   AND MAT_GRP_6 = '" + dataArry[5] + "'" + "\n");

                if (dataArry[6] != " " && dataArry[6] != null)
                    strSqlString.AppendFormat("   AND MAT_GRP_7 = '" + dataArry[6] + "'" + "\n");

                if (dataArry[7] != " " && dataArry[7] != null)
                    strSqlString.AppendFormat("   AND MAT_GRP_8 = '" + dataArry[7] + "'" + "\n");

                if (dataArry[8] != " " && dataArry[8] != null)
                    strSqlString.AppendFormat("   AND MAT_CMF_14 = '" + dataArry[8] + "'" + "\n");

                if (dataArry[9] != " " && dataArry[9] != null)
                    strSqlString.AppendFormat("   AND MAT_CMF_2 = '" + dataArry[9] + "'" + "\n");

                if (dataArry[10] != " " && dataArry[10] != null)
                    strSqlString.AppendFormat("   AND MAT_CMF_3 = '" + dataArry[10] + "'" + "\n");

                if (dataArry[11] != " " && dataArry[11] != null)
                    strSqlString.AppendFormat("   AND MAT_CMF_4 = '" + dataArry[11] + "'" + "\n");

                if (dataArry[12] != " " && dataArry[12] != null)
                    strSqlString.AppendFormat("   AND MAT_ID = '" + dataArry[12] + "'" + "\n");


                #endregion

                strSqlString.Append(" ORDER BY OPER, SCH_END_TIME, OPER_IN_TIME" + "\n");
                #endregion
            }
            else
            {
                #region ASSY
                strSqlString.Append("SELECT OPER" + "\n");
                strSqlString.Append("     , MAT_ID" + "\n");
                strSqlString.Append("     , LOT_ID" + "\n");
                strSqlString.Append("     , QTY_1" + "\n");
                strSqlString.Append("     , DECODE(HOLD_FLAG, 'Y', 'HOLD', DECODE(LOT_STATUS, 'PROC', 'RUN', 'WAIT')) AS STATUS" + "\n");
                strSqlString.Append("     , START_RES_ID" + "\n");
                strSqlString.Append("     , RES_MODEL" + "\n");
                strSqlString.Append("     , SUB_AREA_ID" + "\n");
                strSqlString.Append("     , CHIP_CNT" + "\n");
                strSqlString.Append("     , UPEH" + "\n");
                strSqlString.Append("     , TO_CHAR(SYSDATE + ((NVL(QTY_1,0) - NVL(CHIP_CNT,0)) / UPEH / 24), 'YYYY/MM/DD HH24:MI:SS') AS SCH_END_TIME" + "\n");
                strSqlString.Append("     , TO_CHAR(TO_DATE(OPER_IN_TIME, 'YYYYMMDDHH24MISS'), 'YYYY/MM/DD HH24:MI:SS') AS OPER_IN_TIME" + "\n");
                strSqlString.Append("     , DECODE(START_TIME, ' ', ' ', TO_CHAR(TO_DATE(START_TIME, 'YYYYMMDDHH24MISS'), 'YYYY/MM/DD HH24:MI:SS')) AS START_TIME" + "\n");
                strSqlString.Append("     , TRUNC(SYSDATE-TO_DATE(OPER_IN_TIME,'YYYYMMDDHH24MISS')) || '일 ' ||" + "\n");
                strSqlString.Append("       TRUNC(MOD((SYSDATE-TO_DATE(OPER_IN_TIME,'YYYYMMDDHH24MISS')),1)*24)|| '시간 ' ||" + "\n");
                strSqlString.Append("       TRUNC(MOD((SYSDATE-TO_DATE(OPER_IN_TIME,'YYYYMMDDHH24MISS'))*24,1)*60)|| '분 '" + "\n");
                strSqlString.Append("       AS TIME_INTERVAL" + "\n");
                strSqlString.Append("     , LAST_COMMENT" + "\n");
                strSqlString.Append("  FROM ( " + "\n");
                strSqlString.Append("        SELECT A.*" + "\n");                
                strSqlString.Append("             , (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1) AS CUSTOMER" + "\n");
                strSqlString.Append("             , B.MAT_GRP_2, B.MAT_GRP_4, B.MAT_GRP_5, B.MAT_GRP_6, B.MAT_GRP_7, B.MAT_GRP_8, B.MAT_GRP_9, B.MAT_GRP_10, B.MAT_CMF_10, B.MAT_CMF_7, B.MAT_CMF_8, B.MAT_CMF_11" + "\n");
                //strSqlString.Append("             , CASE WHEN A.OPER IN ('A0000', 'A000N') THEN 'HMK2A' " + "\n");
                //strSqlString.Append("                    WHEN A.OPER IN ('A0020', 'A0040', 'A0025', 'A0050', 'A0070', 'A0080', 'A0010', 'A0041', 'A0030', 'A0015', 'A0033') THEN 'BG' " + "\n");
                //strSqlString.Append("                    WHEN A.OPER IN ('A0100', 'A0150', 'A0200', 'A0300', 'A0170', 'A0180', 'A0270', 'A0230', 'A0045', 'A0215') THEN 'SAW' " + "\n");
                //strSqlString.Append("                    WHEN A.OPER IN ('A0350', 'A0360', 'A0390', 'A0397', 'A0395') THEN 'SP' " + "\n");

                strSqlString.Append("             , CASE WHEN OPER_GRP_8 = 'HMK2A' THEN 'HMK2A' " + "\n");
                strSqlString.Append("                    WHEN OPER_GRP_8 = 'B/G' THEN 'BG' " + "\n");
                strSqlString.Append("                    WHEN OPER_GRP_8 = 'SAW' THEN 'SAW' " + "\n");
                strSqlString.Append("                    WHEN OPER_GRP_8 = 'S/P' THEN 'SP' " + "\n");



                if (cdvCure.Text == "DA")
                {
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0400', 'A0401', 'A0250', 'A0333', 'A0340', 'A0370', 'A0380', 'A0500', 'A0501', 'A0530', 'A0531', 'A0337') THEN 'DA1' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0402', 'A0502', 'A0532') THEN 'DA2' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0801' AND B.MAT_GRP_1 = 'SE' AND B.MAT_GRP_5 <> 'Merge' THEN 'DA2' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0801' AND B.MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(B.MAT_GRP_4,-1) <> SUBSTR(A.OPER, -1) THEN 'DA2' " + "\n");                    
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0403', 'A0503', 'A0533') THEN 'DA3' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0802' AND B.MAT_GRP_1 = 'SE' AND B.MAT_GRP_5 <> 'Merge' THEN 'DA3' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0802' AND B.MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(B.MAT_GRP_4,-1) <> SUBSTR(A.OPER, -1) THEN 'DA3' " + "\n");                    
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0404', 'A0504', 'A0534') THEN 'DA4' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0803' AND B.MAT_GRP_1 = 'SE' AND B.MAT_GRP_5 <> 'Merge' THEN 'DA4' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0803' AND B.MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(B.MAT_GRP_4,-1) <> SUBSTR(A.OPER, -1) THEN 'DA4' " + "\n");                    
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0405', 'A0505', 'A0535') THEN 'DA5' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0804' AND B.MAT_GRP_1 = 'SE' AND B.MAT_GRP_5 <> 'Merge' THEN 'DA5' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0804' AND B.MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(B.MAT_GRP_4,-1) <> SUBSTR(A.OPER, -1) THEN 'DA5' " + "\n");                    
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0406', 'A0506', 'A0536') THEN 'DA6' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0805' AND B.MAT_GRP_1 = 'SE' AND B.MAT_GRP_5 <> 'Merge' THEN 'DA6' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0805' AND B.MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(B.MAT_GRP_4,-1) <> SUBSTR(A.OPER, -1) THEN 'DA6' " + "\n");                    
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0407', 'A0507', 'A0537') THEN 'DA7' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0806' AND B.MAT_GRP_1 = 'SE' AND B.MAT_GRP_5 <> 'Merge' THEN 'DA7' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0806' AND B.MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(B.MAT_GRP_4,-1) <> SUBSTR(A.OPER, -1) THEN 'DA7' " + "\n");                    
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0408', 'A0508', 'A0538') THEN 'DA8' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0807' AND B.MAT_GRP_1 = 'SE' AND B.MAT_GRP_5 <> 'Merge' THEN 'DA8' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0807' AND B.MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(B.MAT_GRP_4,-1) <> SUBSTR(A.OPER, -1) THEN 'DA8' " + "\n");                    
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0409', 'A0509', 'A0539') THEN 'DA9' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0808' AND B.MAT_GRP_1 = 'SE' AND B.MAT_GRP_5 <> 'Merge' THEN 'DA9' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0808' AND B.MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(B.MAT_GRP_4,-1) <> SUBSTR(A.OPER, -1) THEN 'DA9' " + "\n");                    
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0550', 'A0551', 'A0600','A0601') THEN 'WB1' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0552', 'A0602') THEN 'WB2' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0553', 'A0603') THEN 'WB3' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0554', 'A0604') THEN 'WB4' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0555', 'A0605') THEN 'WB5' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0556', 'A0606') THEN 'WB6' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0557', 'A0607') THEN 'WB7' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0558', 'A0608') THEN 'WB8' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0559', 'A0609') THEN 'WB9' " + "\n");

                    strSqlString.Append("                    WHEN OPER_GRP_8 IN ('D/A1', 'D/A1 CURE') THEN 'DA1' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 IN ('D/A2', 'D/A2 CURE') THEN 'DA2' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 IN ('D/A3', 'D/A3 CURE') THEN 'DA3' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 IN ('D/A4', 'D/A4 CURE') THEN 'DA4' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 IN ('D/A5', 'D/A5 CURE') THEN 'DA5' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 IN ('D/A6', 'D/A6 CURE') THEN 'DA6' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 IN ('D/A7', 'D/A7 CURE') THEN 'DA7' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 IN ('D/A8', 'D/A8 CURE') THEN 'DA8' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 IN ('D/A9', 'D/A9 CURE') THEN 'DA9' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 = 'W/B1' THEN 'WB1' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 = 'W/B2' THEN 'WB2' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 = 'W/B3' THEN 'WB3' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 = 'W/B4' THEN 'WB4' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 = 'W/B5' THEN 'WB5' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 = 'W/B6' THEN 'WB6' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 = 'W/B7' THEN 'WB7' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 = 'W/B8' THEN 'WB8' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 = 'W/B9' THEN 'WB9' " + "\n");
                }
                else
                {
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0400', 'A0401', 'A0250', 'A0333', 'A0340', 'A0370', 'A0380', 'A0337') THEN 'DA1' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0402') THEN 'DA2' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0801' AND B.MAT_GRP_1 = 'SE' AND B.MAT_GRP_5 <> 'Merge' THEN 'DA2' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0801' AND B.MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(B.MAT_GRP_4,-1) <> SUBSTR(A.OPER, -1) THEN 'DA2' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0403') THEN 'DA3' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0802' AND B.MAT_GRP_1 = 'SE' AND B.MAT_GRP_5 <> 'Merge' THEN 'DA3' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0802' AND B.MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(B.MAT_GRP_4,-1) <> SUBSTR(A.OPER, -1) THEN 'DA3' " + "\n");                    
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0404') THEN 'DA4' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0803' AND B.MAT_GRP_1 = 'SE' AND B.MAT_GRP_5 <> 'Merge' THEN 'DA4' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0803' AND B.MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(B.MAT_GRP_4,-1) <> SUBSTR(A.OPER, -1) THEN 'DA4' " + "\n");                   
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0405') THEN 'DA5' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0804' AND B.MAT_GRP_1 = 'SE' AND B.MAT_GRP_5 <> 'Merge' THEN 'DA5' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0804' AND B.MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(B.MAT_GRP_4,-1) <> SUBSTR(A.OPER, -1) THEN 'DA5' " + "\n");                   
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0406') THEN 'DA6' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0805' AND B.MAT_GRP_1 = 'SE' AND B.MAT_GRP_5 <> 'Merge' THEN 'DA6' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0805' AND B.MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(B.MAT_GRP_4,-1) <> SUBSTR(A.OPER, -1) THEN 'DA6' " + "\n");                    
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0407') THEN 'DA7' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0806' AND B.MAT_GRP_1 = 'SE' AND B.MAT_GRP_5 <> 'Merge' THEN 'DA7' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0806' AND B.MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(B.MAT_GRP_4,-1) <> SUBSTR(A.OPER, -1) THEN 'DA7' " + "\n");                    
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0408') THEN 'DA8' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0807' AND B.MAT_GRP_1 = 'SE' AND B.MAT_GRP_5 <> 'Merge' THEN 'DA8' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0807' AND B.MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(B.MAT_GRP_4,-1) <> SUBSTR(A.OPER, -1) THEN 'DA8' " + "\n");                    
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0409') THEN 'DA9' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0808' AND B.MAT_GRP_1 = 'SE' AND B.MAT_GRP_5 <> 'Merge' THEN 'DA9' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER = 'A0808' AND B.MAT_GRP_1 NOT IN ('SE', 'HX') AND SUBSTR(B.MAT_GRP_4,-1) <> SUBSTR(A.OPER, -1) THEN 'DA9' " + "\n");         
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0550', 'A0551', 'A0600','A0601', 'A0500', 'A0501', 'A0530', 'A0531') THEN 'WB1' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0552', 'A0602', 'A0502', 'A0532') THEN 'WB2' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0553', 'A0603', 'A0503', 'A0533') THEN 'WB3' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0554', 'A0604', 'A0504', 'A0534') THEN 'WB4' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0555', 'A0605', 'A0505', 'A0535') THEN 'WB5' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0556', 'A0606', 'A0506', 'A0536') THEN 'WB6' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0557', 'A0607', 'A0507', 'A0537') THEN 'WB7' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0558', 'A0608', 'A0508', 'A0538') THEN 'WB8' " + "\n");
                    //strSqlString.Append("                    WHEN A.OPER IN ('A0559', 'A0609', 'A0509', 'A0539') THEN 'WB9' " + "\n");

                    strSqlString.Append("                    WHEN OPER_GRP_8 = 'D/A1' THEN 'DA1' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 = 'D/A2' THEN 'DA2' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 = 'D/A3' THEN 'DA3' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 = 'D/A4' THEN 'DA4' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 = 'D/A5' THEN 'DA5' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 = 'D/A6' THEN 'DA6' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 = 'D/A7' THEN 'DA7' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 = 'D/A8' THEN 'DA8' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 = 'D/A9' THEN 'DA9' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 IN ('W/B1', 'D/A1 CURE') THEN 'WB1' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 IN ('W/B2', 'D/A2 CURE') THEN 'WB2' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 IN ('W/B3', 'D/A3 CURE') THEN 'WB3' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 IN ('W/B4', 'D/A4 CURE') THEN 'WB4' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 IN ('W/B5', 'D/A5 CURE') THEN 'WB5' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 IN ('W/B6', 'D/A6 CURE') THEN 'WB6' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 IN ('W/B7', 'D/A7 CURE') THEN 'WB7' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 IN ('W/B8', 'D/A8 CURE') THEN 'WB8' " + "\n");
                    strSqlString.Append("                    WHEN OPER_GRP_8 IN ('W/B9', 'D/A9 CURE') THEN 'WB9' " + "\n");

                }

                strSqlString.Append("                    WHEN OPER_GRP_8 = 'GATE' AND MAT_GRP_5 = '-' THEN 'GATE' " + "\n");
                strSqlString.Append("                    WHEN OPER_GRP_8 = 'GATE' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 = 'Merge' THEN 'GATE' " + "\n");
                strSqlString.Append("                    WHEN OPER_GRP_8 = 'GATE' AND MAT_GRP_1 = 'HX' THEN 'GATE' " + "\n");
                strSqlString.Append("                    WHEN OPER_GRP_8 = 'GATE' AND MAT_GRP_1 NOT IN ('SE','HX') AND SUBSTR(MAT_GRP_4,-1) = SUBSTR(A.OPER, -1) THEN 'GATE' " + "\n");
                strSqlString.Append("                    WHEN OPER_GRP_8 = 'MOLD' THEN 'MOLD' " + "\n");
                strSqlString.Append("                    WHEN OPER_GRP_8 = 'CURE' THEN 'CURE' " + "\n");
                strSqlString.Append("                    WHEN OPER_GRP_8 = 'M/K' THEN 'MK' " + "\n");
                strSqlString.Append("                    WHEN OPER_GRP_8 = 'TRIM' THEN 'TRIM' " + "\n");
                strSqlString.Append("                    WHEN OPER_GRP_8 = 'TIN' THEN 'TIN' " + "\n");
                strSqlString.Append("                    WHEN OPER_GRP_8 = 'S/B/A' THEN 'SBA' " + "\n");
                strSqlString.Append("                    WHEN OPER_GRP_8 = 'SIG' THEN 'SIG' " + "\n");
                strSqlString.Append("                    WHEN OPER_GRP_8 = 'AVI' THEN 'AVI' " + "\n");
                strSqlString.Append("                    WHEN OPER_GRP_8 = 'V/I' THEN 'PVI' " + "\n");
                strSqlString.Append("                    WHEN OPER_GRP_8 = 'QC GATE' THEN 'QC_GATE' " + "\n");
                strSqlString.Append("                    WHEN OPER_GRP_8 = 'HMK3A' THEN 'HMK3A' " + "\n");
                strSqlString.Append("                    ELSE ' ' " + "\n");


                //strSqlString.Append("                    WHEN A.OPER IN ('A0800', 'A0801', 'A0802', 'A0803', 'A0804', 'A0805', 'A0806', 'A0807', 'A0808', 'A0809', 'A0700', 'A0713') AND B.MAT_GRP_5 = '-' THEN 'GATE' " + "\n");
                //strSqlString.Append("                    WHEN A.OPER IN ('A0800', 'A0801', 'A0802', 'A0803', 'A0804', 'A0805', 'A0806', 'A0807', 'A0808', 'A0809', 'A0700', 'A0713') AND B.MAT_GRP_1 = 'SE' AND B.MAT_GRP_5 = 'Merge' THEN 'GATE' " + "\n");
                //strSqlString.Append("                    WHEN A.OPER IN ('A0800', 'A0801', 'A0802', 'A0803', 'A0804', 'A0805', 'A0806', 'A0807', 'A0808', 'A0809', 'A0700', 'A0713') AND B.MAT_GRP_1 = 'HX' THEN 'GATE' " + "\n");
                //strSqlString.Append("                    WHEN A.OPER IN ('A0800', 'A0801', 'A0802', 'A0803', 'A0804', 'A0805', 'A0806', 'A0807', 'A0808', 'A0809', 'A0700', 'A0713') AND B.MAT_GRP_1 NOT IN ('SE','HX') AND SUBSTR(B.MAT_GRP_4,-1) = SUBSTR(A.OPER, -1) THEN 'GATE' " + "\n");
                //strSqlString.Append("                    WHEN A.OPER IN ('A0970', 'A0900', 'A0950', 'A1000', 'A0980', 'A0910', 'A0920', 'A0930') THEN 'MOLD' " + "\n");
                //strSqlString.Append("                    WHEN A.OPER IN ('A1170', 'A1180', 'A1100', 'A1230', 'A1950') THEN 'CURE' " + "\n");
                //strSqlString.Append("                    WHEN A.OPER IN ('A1110', 'A1150', 'A1500', 'A1020') THEN 'MK' " + "\n");
                //strSqlString.Append("                    WHEN A.OPER IN ('A1200') THEN 'TRIM' " + "\n");
                //strSqlString.Append("                    WHEN A.OPER IN ('A1470', 'A1450', 'A1440', 'A1050', 'A1420') THEN 'TIN' " + "\n");
                //strSqlString.Append("                    WHEN A.OPER IN ('A1270', 'A1250', 'A1300', 'A1260', 'A1350', 'A1240', 'A1380') THEN 'SBA' " + "\n");
                //strSqlString.Append("                    WHEN A.OPER IN ('A1720', 'A1750', 'A1800', 'A1900', 'A1760', 'A1740', 'A1765', 'A1810', 'A1815', 'A1820', 'A1825', 'A1830', 'A1460') THEN 'SIG' " + "\n");                
                //strSqlString.Append("                    WHEN A.OPER IN ('A2000', 'A1040', 'A1770', 'A1790', 'A1795') THEN 'AVI' " + "\n");
                //strSqlString.Append("                    WHEN A.OPER IN ('A2030', 'A2050', 'A2020') THEN 'PVI' " + "\n");
                //strSqlString.Append("                    WHEN A.OPER IN ('A2100', 'A2300', 'A2350') THEN 'QC_GATE' " + "\n");
                //strSqlString.Append("                    WHEN A.OPER IN ('AZ010') THEN 'HMKA3' " + "\n");
                //strSqlString.Append("                    ELSE ' ' " + "\n");
                strSqlString.Append("                END OPER_GRP" + "\n");
                strSqlString.Append("             , (SELECT CHIP_CNT FROM MRESCNTHIS@RPTTOMES WHERE LOT_ID = A.LOT_ID AND OPER = A.OPER AND LINK != 'A') AS CHIP_CNT" + "\n");
                strSqlString.Append("             , C.*" + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == Today)
                {
                    strSqlString.Append("          FROM RWIPLOTSTS A " + "\n");
                    strSqlString.Append("             , MWIPMATDEF B " + "\n");
                    strSqlString.Append("             , (" + "\n");
                    strSqlString.Append("                SELECT RES_ID, RES_GRP_6 AS RES_MODEL, SUB_AREA_ID, UPEH" + "\n");
                    strSqlString.Append("                  FROM MRASRESDEF RES" + "\n");
                    strSqlString.Append("                     , CRASUPHDEF UPH" + "\n");
                    strSqlString.Append("                 WHERE 1=1" + "\n");
                    strSqlString.Append("                   AND RES.FACTORY = UPH.FACTORY(+)" + "\n");
                    strSqlString.Append("                   AND RES.RES_STS_8 = UPH.OPER(+)" + "\n");
                    strSqlString.Append("                   AND RES.RES_GRP_6 = UPH.RES_MODEL(+) " + "\n");
                    strSqlString.Append("                   AND RES.RES_GRP_7 = UPH.UPEH_GRP(+) " + "\n");
                    strSqlString.Append("                   AND RES.RES_STS_2 = UPH.MAT_ID(+)" + "\n");
                    strSqlString.Append("                   AND RES.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND RES.RES_STS_8 NOT IN ('00001','00002')" + "\n");
                    strSqlString.Append("                   AND RES.RES_CMF_9 = 'Y' " + "\n");
                    strSqlString.Append("                   AND RES.RES_CMF_7 = 'Y' " + "\n");
                    strSqlString.Append("                   AND RES.DELETE_FLAG = ' ' " + "\n");
                    strSqlString.Append("                   AND RES.RES_TYPE='EQUIPMENT' " + "\n");
                    strSqlString.Append("               ) C" + "\n");
                    strSqlString.Append("             , MWIPOPRDEF D " + "\n");
                    strSqlString.Append("         WHERE 1 = 1  " + "\n");
                }
                else
                {
                    strSqlString.Append("          FROM RWIPLOTSTS_BOH A " + "\n");
                    strSqlString.Append("             , MWIPMATDEF B " + "\n");
                    strSqlString.Append("             , (" + "\n");
                    strSqlString.Append("                SELECT RES_ID, RES_GRP_6 AS RES_MODEL, SUB_AREA_ID, UPEH" + "\n");

                    strSqlString.Append("                  FROM MRASRESDEF_BOH RES" + "\n");
                    strSqlString.Append("                     , CRASUPHDEF UPH" + "\n");
                    strSqlString.Append("                 WHERE 1=1 " + "\n");
                    strSqlString.Append("                   AND RES.CUTOFF_DT = '" + Today + "22' " + "\n");
                    strSqlString.Append("                   AND RES.FACTORY = UPH.FACTORY(+)" + "\n");
                    strSqlString.Append("                   AND RES.RES_STS_8 = UPH.OPER(+)" + "\n");
                    strSqlString.Append("                   AND RES.RES_GRP_6 = UPH.RES_MODEL(+) " + "\n");
                    strSqlString.Append("                   AND RES.RES_GRP_7 = UPH.UPEH_GRP(+) " + "\n");
                    strSqlString.Append("                   AND RES.RES_STS_2 = UPH.MAT_ID(+)" + "\n");
                    strSqlString.Append("                   AND RES.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND RES.RES_STS_8 NOT IN ('00001','00002')" + "\n");
                    strSqlString.Append("                   AND RES.RES_CMF_9 = 'Y' " + "\n");
                    strSqlString.Append("                   AND RES.RES_CMF_7 = 'Y' " + "\n");
                    strSqlString.Append("                   AND RES.DELETE_FLAG = ' ' " + "\n");
                    strSqlString.Append("                   AND RES.RES_TYPE='EQUIPMENT' " + "\n");
                    strSqlString.Append("               ) C" + "\n");
                    strSqlString.Append("             , MWIPOPRDEF D " + "\n");
                    strSqlString.Append("         WHERE A.CUTOFF_DT = '" + Today + "22' " + "\n");
                }

                strSqlString.Append("           AND A.FACTORY = B.FACTORY" + "\n");
                strSqlString.Append("           AND A.FACTORY = D.FACTORY" + "\n");
                strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("           AND A.OPER = D.OPER " + "\n");
                strSqlString.Append("           AND A.START_RES_ID = C.RES_ID(+)" + "\n");
                strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'   " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W'  " + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("           AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("           AND A.LOT_DEL_FLAG = ' '  " + "\n");
                strSqlString.Append("           AND B.MAT_GRP_2 <> '-' " + "\n");
                strSqlString.Append("           AND B.DELETE_FLAG = ' '   " + "\n");
                strSqlString.Append("         ORDER BY A.OPER, LOT_ID" + "\n");
                strSqlString.Append("       )" + "\n");
                strSqlString.Append("   WHERE 1=1" + "\n");
                strSqlString.Append("     AND OPER_GRP = '" + sOper + "'" + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (dataArry[0] != " " && dataArry[0] != null)
                    strSqlString.AppendFormat("   AND CUSTOMER = '" + dataArry[0] + "'" + "\n");

                if (dataArry[1] != " " && dataArry[1] != null)
                    strSqlString.AppendFormat("   AND MAT_GRP_9 = '" + dataArry[1] + "'" + "\n");

                if (dataArry[2] != " " && dataArry[2] != null)
                    strSqlString.AppendFormat("   AND MAT_GRP_10 = '" + dataArry[2] + "'" + "\n");

                if (dataArry[3] != " " && dataArry[3] != null)
                    strSqlString.AppendFormat("   AND MAT_GRP_6 = '" + dataArry[3] + "'" + "\n");

                if (dataArry[4] != " " && dataArry[4] != null)
                    strSqlString.AppendFormat("   AND MAT_CMF_11 = '" + dataArry[4] + "'" + "\n");

                if (dataArry[5] != " " && dataArry[5] != null)
                    strSqlString.AppendFormat("   AND MAT_GRP_2 = '" + dataArry[5] + "'" + "\n");

                if (dataArry[6] != " " && dataArry[6] != null)
                    strSqlString.AppendFormat("   AND MAT_ID = '" + dataArry[6] + "'" + "\n");

                if (dataArry[7] != " " && dataArry[7] != null)
                    strSqlString.AppendFormat("   AND MAT_GRP_4 = '" + dataArry[7] + "'" + "\n");

                if (dataArry[8] != " " && dataArry[8] != null)
                    strSqlString.AppendFormat("   AND MAT_GRP_5 = '" + dataArry[8] + "'" + "\n");

                if (dataArry[9] != " " && dataArry[9] != null)
                    strSqlString.AppendFormat("   AND MAT_GRP_7 = '" + dataArry[9] + "'" + "\n");

                if (dataArry[10] != " " && dataArry[10] != null)
                    strSqlString.AppendFormat("   AND MAT_GRP_8 = '" + dataArry[10] + "'" + "\n");

                if (dataArry[11] != " " && dataArry[11] != null)
                    strSqlString.AppendFormat("   AND MAT_CMF_10 = '" + dataArry[11] + "'" + "\n");

                if (dataArry[12] != " " && dataArry[12] != null)
                    strSqlString.AppendFormat("   AND MAT_CMF_7 = '" + dataArry[12] + "'" + "\n");

                if (dataArry[13] != " " && dataArry[13] != null)
                    strSqlString.AppendFormat("   AND MAT_CMF_8 = '" + dataArry[13] + "'" + "\n");

                #endregion

                strSqlString.Append(" ORDER BY OPER, SCH_END_TIME, OPER_IN_TIME" + "\n");

                #endregion
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private void cboTimeBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cboTimeBase.Text == "현재")
            if (cboTimeBase.SelectedIndex == 0)
            {
                cdvTime.Enabled = true;
                cdvType.Enabled = true;
            }
            else
            {
                cdvTime.Enabled = false;
                cdvType.Enabled = false;

                cdvTime.SelectedIndex = 0;
                cdvType.SelectedIndex = 0;
            }
        }

        private void cdvDate_ValueChanged(object sender, EventArgs e)
        {
            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd"))
            {                
                cdvCure.Enabled = true;
            }
            else
            {
                cdvCure.Text = "DA";
                cdvCure.Enabled = false;
            }
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {

            if (cdvFactory.txtValue.Equals("HMKB1"))
            {
                BaseFormType = eBaseFormType.BUMP_BASE;
                pnlBUMPDetail.Visible = false;

                cdvType.Enabled = false;
                cdvCure.Enabled = false;
                ckbCOB.Enabled = false;
                ckbCmold.Enabled = false;

                label4.Visible = false;
                label5.Visible = false;

                cdvType.SelectedIndex = 0;
                cdvCure.Text = "DA";
                ckbCOB.Checked = true;

                ckbKpcs.Checked = false;
            }
            else
            {
                BaseFormType = eBaseFormType.WIP_BASE;
                pnlWIPDetail.Visible = false;

                cdvType.Enabled = true;
                cdvCure.Enabled = true;
                ckbCOB.Enabled = true;
                ckbCmold.Enabled = true;

                label4.Visible = true;
                label5.Visible = true;
            }

            SortInit();
        }

        private void ckdHideOper_CheckedChanged(object sender, EventArgs e)
        {
            Color colorStep = System.Drawing.Color.FromArgb(((System.Byte)(200)), ((System.Byte)(200)), ((System.Byte)(200)));
            bool bOperHide = true;
            int iOperPos = 0;
            int i = 0;
            int j = 0;
            
            if (cdvFactory.txtValue.Equals("HMKB1"))
            {
                iOperPos = 16;
            }
            else
            {
                iOperPos = 19;
            }

            if (ckdHideOper.Checked)
            {
                for (j = iOperPos; j < spdData_Sheet1.ColumnCount; j++)
                {
                    bOperHide = true;

                    for (i = 0; i < spdData_Sheet1.RowCount; i++)
                    {
                        if (spdData_Sheet1.Cells[i, j].BackColor != colorStep)
                        {
                            bOperHide = false;
                            break;
                        }
                    }

                    if (bOperHide == true)
                    {
                        spdData_Sheet1.Columns[j].Visible = false;
                    }
                }
            }
            else
            {
                for (j = iOperPos; j < spdData_Sheet1.ColumnCount; j++)
                {
                    spdData_Sheet1.Columns[j].Visible = true;
                }
            }
            
                

        }

        private void cdvType_ValueMemberChanged(object sender, EventArgs e)
        {
            if (cdvType.SelectedIndex == 7)
            {
                label19.Visible = true;
                holdFlag.Visible = true;
            }
            else
            {
                label19.Visible = false;
                holdFlag.Visible = false;
            }
        }




    }
}
