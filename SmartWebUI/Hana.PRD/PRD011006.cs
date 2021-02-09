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
    public partial class PRD011006 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        string[] dayArry = new string[3];
        string[] dayArry2 = new string[3];
        decimal jindoPer;
        GlobalVariable.FindWeek FindWeek_SOP_A = new GlobalVariable.FindWeek();
        GlobalVariable.FindWeek FindWeek_SOP_T = new GlobalVariable.FindWeek();
        /// <summary>
        /// 클  래  스: PRD011006<br/>
        /// 클래스요약: 일일 생산매출 UI<br/>
        /// 작  성  자: 손광섭<br/>
        /// 최초작성일: 2013-05-31<br/>
        /// 상세  설명: <br/>
        /// 변경  내용: <br/> 
        /// 2013-08-27-임종우 : 매출액 선택하여 조회시 누계실적에 당일실적 누락되는 오류 수정
        /// 2013-09-17-임종우 : STACK 제품 체크 조회 시 오류 부분 수정
        /// 2013-09-30-임종우 : STACK 제품 체크 조회 시 DA2, DA3 ... 조회 오류 수정, 매출액 KPCS 오류 수정
        /// 2013-10-22-임종우 : 권한에 따른 매출액 표시 기능 추가 (임태성 요청)
        /// 2013-10-25-임종우 : 기존 THE 개발자가 만들어 놓은 쿼리가 너무 복잡하게 되어 있어 신규 쿼리 개발 함(ASSY 부분)
        /// 2013-10-31-임종우 : DA/WB Final 재공 로직에 Gate 마지막 차수 로직 수정 (김권수 요청)
        /// 2013-12-01-김민우 : STACK 제품 조건 조회 시 DA1차 재공에 A0333,A0340 재공 포함 (임태성K 요청)
        ///                     D/A, WB final 조건 조회시 DA1차 재공에 A0250, A0333 , A0340 재공 포함할 것
        /// 2014-01-08-임종우 : TEST FACTORY 조회시 HMK3 재공에 T0000 재공 포함 (임태성 요청)
        /// 2014-02-25-임종우 : HMK3 공정 분리 -> HMK3A, HMK3T (김권수 요청)
        /// 2014-02-27-임종우 : TEST Factory  조회 시 월간 누계실적은 Assy 와 동일하게 금일 포함하도록..(김권수 요청)
        /// 2014-04-02-임종우 : SMT 재공 DA 포함 (임태성 요청)
        /// 2014-05-12-임종우 : Hynix 제품 실적은 06시 기준으로.. (임태성 요청)
        /// 2014-08-11-임종우 : 실적 -> DHL 제품 - 3차 기준, JZH 제품 - 1/2 로 계산 (임태성K 요청)
        ///                     재공 -> DHL 제품 - 3차 기준
        /// 2014-09-02-임종우 : HX 제품 재공 -> Operation 상에 A0015가 있는 제품의 경우 [(A0000~A0015) / Auto loss + A0016 이후 재공의 합] (임태성K 요청)
        ///                                     Operation 상에 A0395가 있는 제품의 경우 [(A0000~A0395) / Auto loss + A0396 이후 재공의 합]
        ///                                     Operation 상에 A0015, A0395 둘다 있는 제품 [(A0000~A0015) / Auto loss] + [(A0016~A035) / (Auto loss/2)] + A0396 이후 재공의 합
        /// 2014-09-03-임종우 : 매출액2 검색 기능 추가 - Turn Key 제품은 ASSY, TEST 단가 합해서 계산한다 (임태성K 요청)
        /// 2014-09-11-임종우 : D0계획, D1계획, D0 잔량, 공정별 실적 추가 (임태성K 요청)
        /// 2014-09-12-임종우 : MWIPMATDEF -> VWIPMATDEF 로 변경 함
        /// 2014-10-22-임종우 : 재공, 실적 -> SEKY% 로 시작하는 1st PKG_CODE - 2차 기준 (임태성K 요청)
        /// 2014-10-23-임종우 : DA/WB Final 조회 시 상세 실적 표시되도록 수정 (임태성K 요청)
        /// 2014-10-28-임종우 : DA/WB Final -> Detail 명칭으로 변경 (임태성K 요청)
        /// 2014-11-04-임종우 : 삼성 단가 미등록된 제품의 경우 동일한 PKG_CODE의 평균 단가로 계산한다 (임태성K 요청)
        /// 2015-02-16-임종우 : DQZ 제품 - 3차 기준 (임태성K 요청)
        /// 2015-02-17-임종우 : 9단 STACK 제품까지 표시 되도록 추가 (임태성K 요청)
        /// 2015-02-25-임종우 : PVI 재공 - A2030 공정 추가 (임태성K 요청)
        /// 2015-07-13-임종우 : DQA, DRA 제품 - 3차 기준 (임태성K 요청)
        /// 2015-07-16-임종우 : 재공 -> Hynix HX_VERSION = 'A-376' 제품 - 5차, Merge 기준 (임태성K 요청)
        /// 2015-08-28-임종우 : DND 제품 - 3차 기준 (임태성K 요청)
        /// 2015-09-11-임종우 : DWG 제품 - 3차 기준 (임태성K 요청)
        /// 2015-09-21-임종우 : 고객사 명 하드 코딩 되어 있는것을 기준정보로 변경 (임태성K 요청)
        /// 2015-10-16-임종우 : DUT 제품 - 3차 기준 (임태성K 요청)
        /// 2016-01-13-임종우 : HAU 제품 - 3차 기준 (오영식K 요청)
        /// 2016-10-20-임종우 : HF8 제품 - 3차 기준 (김성업D 요청)
        /// 2016-11-14-임종우 : HDM 제품 - 3차 기준 (김성업D 요청)
        /// 2016-12-12-임종우 : HDR 제품 - 3차 기준 (김성업D 요청)
        /// 2017-02-06-임종우 : 재공 -> Hynix HX_VERSION = 'A-445' 제품 - 6차, Merge 기준 (임태성K 요청)
        /// 2017-05-02-임종우 : HEN 제품 - 3차 기준 (김성업K 요청)
        /// 2017-07-24-임종우 : HQA 제품 - 3차 기준 (이원하 요청)
        /// 2017-10-25-임종우 : HQC 제품 - 3차 기준 (김성업K 요청)
        /// 2018-01-12-임종우 : Close 실적 추가 (임태성C 요청) & 백분율 Function 으로 변경
        /// 2018-06-11-임종우 : HRTDMCPROUT@RPTTOMES -> RWIPMCPBOM 으로 변경
        ///                     PO 미발행 HOLD CODE 제외 - H71, H54 (김성업과장 요청)
        /// 2018-06-27-임종우 : 재공 -> Hynix HX_VERSION = 'A-524' 제품 - 7차, Merge 기준 (박형순대리 요청)
        /// 2018-07-04-임종우 : 재공 -> Hynix HX_VERSION = 'A-525' 제품 - 6차, Merge 기준 (박형순대리 요청)
        /// 2019-05-15-임종우 : FINISH 재공 - A2100 공정 추가 (김성업과장 요청)
        /// 2020-04-14-김미경 : 단가에 환율 반영 / 삼성만(KRW) 해둔 쿼리에선 환율 적용 제외 (이승희 D 요청)
        /// 2020-11-28-임종우 : 환율정보 HMK, HMV2 분리
        /// </summary>
        public PRD011006()
        {
            InitializeComponent();                                   
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvLotType.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;

            lblNumericSum.Text = "";

            // 2013-10-22-임종우 : 제조에서는 해당화면이 보이돼 매출을 확인 할 수 없도록 수정
            if (GlobalVariable.gsUserGroup == "OPERATOR_GERNERAL" || GlobalVariable.gsUserGroup == "QA MANAGER")
            {
                rdbSales.Visible = false;
                rdbTurnkey.Visible = false;
            }
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (!(cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory || cdvFactory.Text == GlobalVariable.gsTestDefaultFactory || cdvFactory.Text == "HMKB1"))
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
            spdData.RPT_ColumnInit();
            LabelTextChange();            
            String ss = DateTime.Now.ToString("MM-dd");

            try
            {
                #region Bump
                if (cdvFactory.txtValue.Equals("HMKB1"))
                {
                    spdData.RPT_ColumnInit();
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Bumping Type", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Operation Flow", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Layer classification", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Size", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("월간(" + dayArry[2] + ")", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Action Plan", 1, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Rev plan", 1, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Cumulative shipment", 1, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Progress rate 1", 1, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("Progress rate 2", 1, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 5, 5);

                    spdData.RPT_AddBasicColumn("D1 plan", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("D0 plan", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                    spdData.RPT_AddBasicColumn("Daily shipment", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("(" + dayArry[2] + ")", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    spdData.RPT_AddBasicColumn("D0 remaining", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                    spdData.RPT_AddBasicColumn("WIP", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);

                    //if (cdvType.Text == "기본")
                    if (cdvType.SelectedIndex == 0)
                    {
                        spdData.RPT_AddBasicColumn("HMK3B", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PACKING", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("OQC", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("FINAL_INSP", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Bump", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PSV 3", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL 3", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PSV 2", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL 2", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PSV 1", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL 1", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RCF", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Incoming", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 14, 14);

                        spdData.RPT_AddBasicColumn("WAFER", 0, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("warehousing", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                        spdData.RPT_AddBasicColumn("actual", 0, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PACKING", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("FINAL_INSP", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("BUMP", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PSV3", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL3", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PSV2", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL2", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PSV1", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL1", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RCF", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 29, 11);
                    }
                    //else if (cdvType.Text == "Detail")
                    else if (cdvType.SelectedIndex == 2)
                    {
                        spdData.RPT_AddBasicColumn("HMK3B", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PACKING", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("OGI", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SORT", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("FINAL_INSP", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("BUMP_REFLOW", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("BUMP_BALL_MOUNT", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("BUMP_ETCH", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("BUMP_SNAG_PLAT", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("BUMP_CU_PLAT", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("BUMP_PHOTO", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("BUMP_SPUTTER", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PSV3_PHOTO", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL3_ETCH", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL3_PLAT", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL3_PHOTO", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL3_SPUTTER", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PSV2_PHOTO", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL2_ETCH", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL2_PLAT", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL2_PHOTO", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL2_SPUTTER", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PSV1_PHOTO", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL1_ETCH", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL1_PLAT", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL1_PHOTO", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL1_SPUTTER", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RCF_PHOTO", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("I-STOCK", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("IQC", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMK2B", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 14, 32);

                        spdData.RPT_AddBasicColumn("WAFER", 0, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("warehousing", 1, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                        spdData.RPT_AddBasicColumn("actual", 0, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("P/K", 1, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("FINAL_INSP", 1, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("BUMP", 1, 50, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PSV3", 1, 51, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL3", 1, 52, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PSV2", 1, 53, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL2", 1, 54, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PSV1", 1, 55, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RDL1", 1, 56, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("RCF", 1, 57, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 47, 11);
                    }

                }
                #endregion
                #region 그 외
                else
                {
                    spdData.RPT_ColumnInit();
                    spdData.RPT_AddBasicColumn("Vendor description", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Major", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PKG", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Lead", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PKG Code", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PRODUCT", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                    spdData.RPT_AddBasicColumn("PIN TYPE", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);

                    spdData.RPT_AddBasicColumn("월간(" + dayArry[2] + ")", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Action Plan", 1, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Rev plan", 1, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Cumulative shipment", 1, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Progress rate 1", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("Progress rate 2", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 7, 5);

                    spdData.RPT_AddBasicColumn("D1 plan", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("D0 plan", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                    spdData.RPT_AddBasicColumn("Daily shipment", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("(" + dayArry[2] + ")", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    spdData.RPT_AddBasicColumn("D0 remaining", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                    spdData.RPT_AddBasicColumn("WIP", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                    // spdData.RPT_AddBasicColumn("TTL", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
                    {
                        spdData.RPT_AddBasicColumn("HMK4", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("EOL", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TEST", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMK3T", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMK3A", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("FINISH", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("FRONT", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMK2", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TTL", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 16, 10);

                        spdData.RPT_AddBasicColumn("Semi-manufactures", 0, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("warehousing", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                    //공장 == HMKA1 && Stack제품 == 미선택
                    //else if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory && cdvType.Text == "기본")
                    else if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory && cdvType.SelectedIndex == 0)
                    {
                        spdData.RPT_AddBasicColumn("HMK3", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("FINISH", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DP", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("STOCK", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TTL", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 16, 8);

                        spdData.RPT_AddBasicColumn("WAFER", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("warehousing", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                    //공장 == HMKA1 && Stack제품 == 선택
                    //else if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory && cdvType.Text == "Stack 제품")
                    else if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory && cdvType.SelectedIndex == 1)
                    {
                        spdData.RPT_AddBasicColumn("HMK3", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("FINISH", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB9", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA9", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB8", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA8", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB7", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA7", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB6", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA6", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB5", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA5", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB4", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA4", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB3", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA3", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB2", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA2", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB1", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA1", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DP", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("STOCK", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TTL", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 16, 24);

                        spdData.RPT_AddBasicColumn("WAFER", 0, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("warehousing", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("HMK3", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("QC_GATE", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TIN", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB9", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA9", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB8", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA8", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB7", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA7", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB6", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA6", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB5", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA5", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB4", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA4", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB3", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA3", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB2", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA2", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB1", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA1", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SP", 1, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("BG", 1, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("STOCK", 1, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TTL", 1, 50, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 16, 35);

                        spdData.RPT_AddBasicColumn("WAFER", 0, 51, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("warehousing", 1, 51, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }


                    if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
                    {
                        spdData.RPT_AddBasicColumn("actual", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TEST", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnHeader.Columns.Count - 1, 1);
                    }
                    //else if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory && cdvType.Text == "Detail")
                    else if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory && cdvType.SelectedIndex == 2)
                    {
                        spdData.RPT_AddBasicColumn("actual", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMKA3", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("CLOSE", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB9", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA9", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB8", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA8", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB7", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA7", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB6", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA6", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB5", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA5", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB4", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA4", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB3", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA3", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB2", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA2", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB1", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA1", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DDS", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("BG", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("STEALTH", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PRE", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("LAMI", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("ISSUE", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnHeader.Columns.Count - 30, 30);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("actual", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("CLOSE", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WB", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DA", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("DP", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("ISSUE", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnHeader.Columns.Count - 6, 6);
                    }
                }
                #endregion

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 12, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 13, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 15, 2);

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
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();

            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1 AS CUSTOMER", "MAT_GRP_1", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Bumping Type", "MAT_GRP_2 AS BUMPING_TYPE", "MAT_GRP_2", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation Flow", "MAT_GRP_3 AS PROCESS_FLOW", "MAT_GRP_3", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "MAT_GRP_4 AS LAYER", "MAT_GRP_4", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Size", "MAT_CMF_14 AS WF_SIZE", "MAT_CMF_14", true);
            }
            else
            {
                //5,6 번째 파라미터는 MakeString03()에서 사용 
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "A.MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.MAT_GRP_1) CUSTOMER", "A.MAT_GRP_1,  A.MAT_GRP_9 ,  A.MAT_GRP_10", "CUSTOMER", "MAT.MAT_GRP_1", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "A.MAT_GRP_9", "A.MAT_GRP_9", "A.MAT_GRP_9", "MAT_GRP_9", "MAT.MAT_GRP_9", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "A.MAT_GRP_10", "A.MAT_GRP_10", "A.MAT_GRP_10", "MAT_GRP_10", "MAT.MAT_GRP_10", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lead", "A.MAT_GRP_6", "A.MAT_GRP_6", "A.MAT_GRP_6", "MAT_GRP_6", "MAT.MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG Code", "A.MAT_CMF_11", "A.MAT_CMF_11", "A.MAT_CMF_11", "MAT_CMF_11", "MAT.MAT_CMF_11", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.CONV_MAT_ID", "A.CONV_MAT_ID", "A.CONV_MAT_ID", "CONV_MAT_ID", "MAT.CONV_MAT_ID", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "A.MAT_CMF_10", "A.MAT_CMF_10", "A.MAT_CMF_10", "MAT_CMF_10", "MAT.MAT_CMF_10", false);
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
            string QueryCond1NotNull;
            string QueryCond2NotNull;
            string QueryCond3NotNull;
            string QueryCond4NotNull;
            string start_date;
            string end_date;
            string yesterday;            
            string month;            
            string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값
            string sExchange;
   
            // kpcs 선택에 의한 분모 값 저장 한다.
            if (ckbKpcs.Checked == true)
            {
                if (rdbQuantity.Checked == true )
                {
                    sKpcsValue = "1000";
                }
                else
                {
                    sKpcsValue = "1000000";                    
                }
            }
            else
            {
                sKpcsValue = "1";
            }

            if (GlobalVariable.gsGlovalSite == "V2")
            {
                sExchange = "RDAYECRDAT@HANARPT";
            }
            else
            {
                sExchange = "RDAYECRDAT";
            }

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            QueryCond1NotNull = tableForm.SelectedValueToQuery;
            QueryCond2NotNull = tableForm.SelectedValue2ToQuery;
            QueryCond3NotNull = tableForm.SelectedValue3ToQuery;
            QueryCond4NotNull = tableForm.SelectedValue4ToQuery;
            
            DateTime Select_date;
            Select_date = DateTime.Parse(cdvDate.Text.ToString());

            month = Select_date.ToString("yyyyMM");            
            end_date = Select_date.ToString("yyyyMMdd");
            yesterday = Select_date.AddDays(-1).ToString("yyyyMMdd");

            // 조회월과 조회주차의 시작일이 같은 달이면 시작은 조회월의 1일자로 하고, 다르면(주차시작일이 작으면) 주차 시작일을 시작일로 한다.
            if (month == FindWeek_SOP_A.StartDay_ThisWeek.Substring(0, 6))
            {
                start_date = month + "01";
            }
            else
            {
                start_date = FindWeek_SOP_A.StartDay_ThisWeek;
            }

            #region ASSY
            if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
            {
                strSqlString.Append("SELECT " + QueryCond2 + "\n");

                #region 기본
                //if (cdvType.Text == "기본")
                if (cdvType.SelectedIndex == 0)
                {
                    strSqlString.Append("     , ROUND(SUM(ORI_PLN) / " + sKpcsValue + ", 0) AS ORI_PLN" + "\n");
                    strSqlString.Append("     , ROUND(SUM(REV_PLN) / " + sKpcsValue + ", 0) AS REV_PLN" + "\n");
                    strSqlString.Append("     , ROUND(SUM(SHP_TTL) / " + sKpcsValue + ", 0) AS SHP_TTL" + "\n");
                    strSqlString.Append("     , ROUND(DECODE(SUM(ORI_PLN), 0, 0, SUM(SHP_TTL) / SUM(ORI_PLN) * 100), 1) AS JINDO_1" + "\n");
                    strSqlString.Append("     , ROUND(DECODE(SUM(REV_PLN), 0, 0, SUM(SHP_TTL) / SUM(REV_PLN) * 100), 1) AS JINDO_2" + "\n");
                    strSqlString.Append("     , ROUND(SUM(D1_PLAN) / " + sKpcsValue + ", 0) AS D1_PLAN" + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(D0_PLAN,0) - NVL(SHP_WEEK,0)) / " + sKpcsValue + ", 0) AS D0_PLAN" + "\n");                    
                    strSqlString.Append("     , ROUND(SUM(SHP_TODAY) / " + sKpcsValue + ", 0) AS SHP_TODAY" + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(D0_PLAN,0) - NVL(SHP_WEEK,0) - NVL(SHP_TODAY,0)) / " + sKpcsValue + ", 0) AS D0_DEF" + "\n");

                    strSqlString.Append("     , ROUND(SUM(HMK3A) / " + sKpcsValue + ", 0) AS HMK3A" + "\n");
                    strSqlString.Append("     , ROUND(SUM(MK + TRIM + TIN + SBA + SIG + AVI + VI + QC_GATE) / " + sKpcsValue + ", 0) AS FINISH" + "\n");
                    strSqlString.Append("     , ROUND(SUM(CURE + MOLD) / " + sKpcsValue + ", 0) AS MOLD" + "\n");
                    strSqlString.Append("     , ROUND(SUM(WB + GATE) / " + sKpcsValue + ", 0) AS WB" + "\n");
                    strSqlString.Append("     , ROUND(SUM(SP + DA + SMT) / " + sKpcsValue + ", 0) AS DA" + "\n");
                    strSqlString.Append("     , ROUND(SUM(BG + SAW) / " + sKpcsValue + ", 0) AS DP" + "\n");
                    strSqlString.Append("     , ROUND(SUM(STOCK) / " + sKpcsValue + ", 0) AS STOCK" + "\n");
                    strSqlString.Append("     , ROUND(SUM(HMK3A + QC_GATE + VI + AVI + SIG + SBA + TIN + TRIM + MK + CURE + MOLD + GATE + WB + DA + SP + SAW + BG + STOCK) / " + sKpcsValue + ", 0) AS WIP_TTL " + "\n");

                    strSqlString.Append("     , ROUND(SUM(RCV_QTY) / " + sKpcsValue + ", 0) AS RCV_QTY" + "\n");

                    strSqlString.Append("     , ROUND(SUM(OUT_CLOSE) / " + sKpcsValue + ", 0) AS OUT_CLOSE" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_MOLD) / " + sKpcsValue + ", 0) AS OUT_MOLD" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_WB) / " + sKpcsValue + ", 0) AS OUT_WB" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_DA) / " + sKpcsValue + ", 0) AS OUT_DA" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_DP) / " + sKpcsValue + ", 0) AS OUT_DP" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_STOCK) / " + sKpcsValue + ", 0) AS OUT_STOCK" + "\n");
                }
                #endregion

                #region Stack 제품
                //else if (cdvType.Text == "Stack 제품")
                else if (cdvType.SelectedIndex == 1)
                {
                    strSqlString.Append("     , ROUND(SUM(ORI_PLN) / " + sKpcsValue + ", 0) AS ORI_PLN" + "\n");
                    strSqlString.Append("     , ROUND(SUM(REV_PLN) / " + sKpcsValue + ", 0) AS REV_PLN" + "\n");
                    strSqlString.Append("     , ROUND(SUM(SHP_TTL) / " + sKpcsValue + ", 0) AS SHP_TTL" + "\n");
                    strSqlString.Append("     , ROUND(DECODE(SUM(ORI_PLN), 0, 0, SUM(SHP_TTL) / SUM(ORI_PLN) * 100), 1) AS JINDO_1" + "\n");
                    strSqlString.Append("     , ROUND(DECODE(SUM(REV_PLN), 0, 0, SUM(SHP_TTL) / SUM(REV_PLN) * 100), 1) AS JINDO_2" + "\n");
                    strSqlString.Append("     , ROUND(SUM(D1_PLAN) / " + sKpcsValue + ", 0) AS D1_PLAN" + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(D0_PLAN,0) - NVL(SHP_WEEK,0)) / " + sKpcsValue + ", 0) AS D0_PLAN" + "\n");                    
                    strSqlString.Append("     , ROUND(SUM(SHP_TODAY) / " + sKpcsValue + ", 0) AS SHP_TODAY" + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(D0_PLAN,0) - NVL(SHP_WEEK,0) - NVL(SHP_TODAY,0)) / " + sKpcsValue + ", 0) AS D0_DEF" + "\n");
                    strSqlString.Append("     , ROUND(SUM(HMK3A) / " + sKpcsValue + ", 0) AS HMK3A" + "\n");
                    strSqlString.Append("     , ROUND(SUM(MK + TRIM + TIN + SBA + SIG + AVI + VI + QC_GATE) / " + sKpcsValue + ", 0) AS FINISH" + "\n");
                    strSqlString.Append("     , ROUND(SUM(CURE + MOLD) / " + sKpcsValue + ", 0) AS MOLD" + "\n");
                    strSqlString.Append("     , ROUND(SUM(S_WB9) / " + sKpcsValue + ", 0) AS S_WB9" + "\n");
                    strSqlString.Append("     , ROUND(SUM(S_DA9) / " + sKpcsValue + ", 0) AS S_DA9" + "\n");
                    strSqlString.Append("     , ROUND(SUM(S_WB8) / " + sKpcsValue + ", 0) AS S_WB8" + "\n");
                    strSqlString.Append("     , ROUND(SUM(S_DA8) / " + sKpcsValue + ", 0) AS S_DA8" + "\n");
                    strSqlString.Append("     , ROUND(SUM(S_WB7) / " + sKpcsValue + ", 0) AS S_WB7" + "\n");
                    strSqlString.Append("     , ROUND(SUM(S_DA7) / " + sKpcsValue + ", 0) AS S_DA7" + "\n");
                    strSqlString.Append("     , ROUND(SUM(S_WB6) / " + sKpcsValue + ", 0) AS S_WB6" + "\n");
                    strSqlString.Append("     , ROUND(SUM(S_DA6) / " + sKpcsValue + ", 0) AS S_DA6" + "\n");
                    strSqlString.Append("     , ROUND(SUM(S_WB5) / " + sKpcsValue + ", 0) AS S_WB5" + "\n");
                    strSqlString.Append("     , ROUND(SUM(S_DA5) / " + sKpcsValue + ", 0) AS S_DA5" + "\n");
                    strSqlString.Append("     , ROUND(SUM(S_WB4) / " + sKpcsValue + ", 0) AS S_WB4" + "\n");
                    strSqlString.Append("     , ROUND(SUM(S_DA4) / " + sKpcsValue + ", 0) AS S_DA4" + "\n");
                    strSqlString.Append("     , ROUND(SUM(S_WB3) / " + sKpcsValue + ", 0) AS S_WB3" + "\n");
                    strSqlString.Append("     , ROUND(SUM(S_DA3) / " + sKpcsValue + ", 0) AS S_DA3" + "\n");
                    strSqlString.Append("     , ROUND(SUM(S_WB2) / " + sKpcsValue + ", 0) AS S_WB2" + "\n");
                    strSqlString.Append("     , ROUND(SUM(S_DA2) / " + sKpcsValue + ", 0) AS S_DA2" + "\n");
                    strSqlString.Append("     , ROUND(SUM(S_WB1) / " + sKpcsValue + ", 0) AS S_WB1" + "\n");
                    strSqlString.Append("     , ROUND(SUM(SP + S_DA1 + SMT) / " + sKpcsValue + ", 0) AS S_DA1" + "\n");
                    strSqlString.Append("     , ROUND(SUM(BG + SAW) / " + sKpcsValue + ", 0) AS DP" + "\n");
                    strSqlString.Append("     , ROUND(SUM(STOCK) / " + sKpcsValue + ", 0) AS STOCK" + "\n");
                    strSqlString.Append("     , ROUND(SUM(HMK3A + QC_GATE + VI + AVI + SIG + SBA + TIN + TRIM + MK + CURE + MOLD + S_WB5 + S_DA5 + " + "\n");
                    strSqlString.Append("                 S_WB4 + S_DA4 + S_WB3 + S_DA3 + S_WB2 + S_DA2 + S_WB1 + S_DA1 + SP + SAW + BG + STOCK) / " + sKpcsValue + ", 0) AS WIP_TTL " + "\n");
                    strSqlString.Append("     , ROUND(SUM(RCV_QTY) / " + sKpcsValue + ", 0) AS RCV_QTY" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_CLOSE) / " + sKpcsValue + ", 0) AS OUT_CLOSE" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_MOLD) / " + sKpcsValue + ", 0) AS OUT_MOLD" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_WB) / " + sKpcsValue + ", 0) AS OUT_WB" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_DA) / " + sKpcsValue + ", 0) AS OUT_DA" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_DP) / " + sKpcsValue + ", 0) AS OUT_DP" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_STOCK) / " + sKpcsValue + ", 0) AS OUT_STOCK" + "\n");
                }
                #endregion

                #region Detail
                else
                {
                    strSqlString.Append("     , ROUND(SUM(ORI_PLN) / " + sKpcsValue + ", 0) AS ORI_PLN" + "\n");
                    strSqlString.Append("     , ROUND(SUM(REV_PLN) / " + sKpcsValue + ", 0) AS REV_PLN" + "\n");
                    strSqlString.Append("     , ROUND(SUM(SHP_TTL) / " + sKpcsValue + ", 0) AS SHP_TTL" + "\n");
                    strSqlString.Append("     , ROUND(DECODE(SUM(ORI_PLN), 0, 0, SUM(SHP_TTL) / SUM(ORI_PLN) * 100), 1) AS JINDO_1" + "\n");
                    strSqlString.Append("     , ROUND(DECODE(SUM(REV_PLN), 0, 0, SUM(SHP_TTL) / SUM(REV_PLN) * 100), 1) AS JINDO_2" + "\n");
                    strSqlString.Append("     , ROUND(SUM(D1_PLAN) / " + sKpcsValue + ", 0) AS D1_PLAN" + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(D0_PLAN,0) - NVL(SHP_WEEK,0)) / " + sKpcsValue + ", 0) AS D0_PLAN" + "\n");                    
                    strSqlString.Append("     , ROUND(SUM(SHP_TODAY) / " + sKpcsValue + ", 0) AS SHP_TODAY" + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(D0_PLAN,0) - NVL(SHP_WEEK,0) - NVL(SHP_TODAY,0)) / " + sKpcsValue + ", 0) AS D0_DEF" + "\n");

                    strSqlString.Append("     , ROUND(SUM(HMK3A) / " + sKpcsValue + ", 0) AS HMK3A" + "\n");
                    strSqlString.Append("     , ROUND(SUM(QC_GATE) / " + sKpcsValue + ", 0) AS QC_GATE" + "\n");
                    strSqlString.Append("     , ROUND(SUM(PVI) / " + sKpcsValue + ", 0) AS PVI" + "\n");
                    strSqlString.Append("     , ROUND(SUM(AVI) / " + sKpcsValue + ", 0) AS AVI" + "\n");
                    strSqlString.Append("     , ROUND(SUM(SIG) / " + sKpcsValue + ", 0) AS SIG" + "\n");
                    strSqlString.Append("     , ROUND(SUM(SBA) / " + sKpcsValue + ", 0) AS SBA" + "\n");
                    strSqlString.Append("     , ROUND(SUM(TIN) / " + sKpcsValue + ", 0) AS TIN" + "\n");
                    strSqlString.Append("     , ROUND(SUM(TRIM) / " + sKpcsValue + ", 0) AS TRIM" + "\n");
                    strSqlString.Append("     , ROUND(SUM(MK) / " + sKpcsValue + ", 0) AS MK" + "\n");
                    strSqlString.Append("     , ROUND(SUM(CURE) / " + sKpcsValue + ", 0) AS CURE" + "\n");
                    strSqlString.Append("     , ROUND(SUM(MOLD) / " + sKpcsValue + ", 0) AS MOLD" + "\n");
                    strSqlString.Append("     , ROUND(SUM(F_GATE) / " + sKpcsValue + ", 0) AS F_GATE" + "\n");
                    strSqlString.Append("     , ROUND(SUM(F_WB9) / " + sKpcsValue + ", 0) AS F_WB9" + "\n");
                    strSqlString.Append("     , ROUND(SUM(F_DA9) / " + sKpcsValue + ", 0) AS F_DA9" + "\n");
                    strSqlString.Append("     , ROUND(SUM(F_WB8) / " + sKpcsValue + ", 0) AS F_WB8" + "\n");
                    strSqlString.Append("     , ROUND(SUM(F_DA8) / " + sKpcsValue + ", 0) AS F_DA8" + "\n");
                    strSqlString.Append("     , ROUND(SUM(F_WB7) / " + sKpcsValue + ", 0) AS F_WB7" + "\n");
                    strSqlString.Append("     , ROUND(SUM(F_DA7) / " + sKpcsValue + ", 0) AS F_DA7" + "\n");
                    strSqlString.Append("     , ROUND(SUM(F_WB6) / " + sKpcsValue + ", 0) AS F_WB6" + "\n");
                    strSqlString.Append("     , ROUND(SUM(F_DA6) / " + sKpcsValue + ", 0) AS F_DA6" + "\n");
                    strSqlString.Append("     , ROUND(SUM(F_WB5) / " + sKpcsValue + ", 0) AS F_WB5" + "\n");
                    strSqlString.Append("     , ROUND(SUM(F_DA5) / " + sKpcsValue + ", 0) AS F_DA5" + "\n");
                    strSqlString.Append("     , ROUND(SUM(F_WB4) / " + sKpcsValue + ", 0) AS F_WB4" + "\n");
                    strSqlString.Append("     , ROUND(SUM(F_DA4) / " + sKpcsValue + ", 0) AS F_DA4" + "\n");
                    strSqlString.Append("     , ROUND(SUM(F_WB3) / " + sKpcsValue + ", 0) AS F_WB3" + "\n");
                    strSqlString.Append("     , ROUND(SUM(F_DA3) / " + sKpcsValue + ", 0) AS F_DA3" + "\n");
                    strSqlString.Append("     , ROUND(SUM(F_WB2) / " + sKpcsValue + ", 0) AS F_WB2" + "\n");
                    strSqlString.Append("     , ROUND(SUM(F_DA2) / " + sKpcsValue + ", 0) AS F_DA2" + "\n");
                    strSqlString.Append("     , ROUND(SUM(F_WB1) / " + sKpcsValue + ", 0) AS F_WB1" + "\n");
                    strSqlString.Append("     , ROUND(SUM(F_DA1 + SMT) / " + sKpcsValue + ", 0) AS F_DA1" + "\n");
                    strSqlString.Append("     , ROUND(SUM(SP) / " + sKpcsValue + ", 0) AS SP" + "\n");
                    strSqlString.Append("     , ROUND(SUM(SAW) / " + sKpcsValue + ", 0) AS SAW" + "\n");
                    strSqlString.Append("     , ROUND(SUM(BG) / " + sKpcsValue + ", 0) AS BG" + "\n");
                    strSqlString.Append("     , ROUND(SUM(STOCK) / " + sKpcsValue + ", 0) AS STOCK" + "\n");
                    strSqlString.Append("     , ROUND(SUM(HMK3A + QC_GATE + PVI + AVI + SIG + SBA + TIN + TRIM + MK + CURE + MOLD + F_GATE + F_WB5 + F_DA5 + " + "\n");
                    strSqlString.Append("                 F_WB4 + F_DA4 + F_WB3 + F_DA3 + F_WB2 + F_DA2 + F_WB1 + F_DA1 + SP + SAW + BG + STOCK) / " + sKpcsValue + ", 0) AS WIP_TTL " + "\n");

                    strSqlString.Append("     , ROUND(SUM(RCV_QTY) / " + sKpcsValue + ", 0) AS RCV_QTY" + "\n");

                    strSqlString.Append("     , ROUND(SUM(OUT_HMKA3) / " + sKpcsValue + ", 0) AS OUT_HMKA3" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_CLOSE) / " + sKpcsValue + ", 0) AS OUT_CLOSE" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_SIG) / " + sKpcsValue + ", 0) AS OUT_SIG" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_MK) / " + sKpcsValue + ", 0) AS OUT_MK" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_MOLD) / " + sKpcsValue + ", 0) AS OUT_MOLD" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_WB9) / " + sKpcsValue + ", 0) AS OUT_WB9" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_DA9) / " + sKpcsValue + ", 0) AS OUT_DA9" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_WB8) / " + sKpcsValue + ", 0) AS OUT_WB8" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_DA8) / " + sKpcsValue + ", 0) AS OUT_DA8" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_WB7) / " + sKpcsValue + ", 0) AS OUT_WB7" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_DA7) / " + sKpcsValue + ", 0) AS OUT_DA7" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_WB6) / " + sKpcsValue + ", 0) AS OUT_WB6" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_DA6) / " + sKpcsValue + ", 0) AS OUT_DA6" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_WB5) / " + sKpcsValue + ", 0) AS OUT_WB5" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_DA5) / " + sKpcsValue + ", 0) AS OUT_DA5" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_WB4) / " + sKpcsValue + ", 0) AS OUT_WB4" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_DA4) / " + sKpcsValue + ", 0) AS OUT_DA4" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_WB3) / " + sKpcsValue + ", 0) AS OUT_WB3" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_DA3) / " + sKpcsValue + ", 0) AS OUT_DA3" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_WB2) / " + sKpcsValue + ", 0) AS OUT_WB2" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_DA2) / " + sKpcsValue + ", 0) AS OUT_DA2" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_WB1) / " + sKpcsValue + ", 0) AS OUT_WB1" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_DA1) / " + sKpcsValue + ", 0) AS OUT_DA1" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_DDS) / " + sKpcsValue + ", 0) AS OUT_DDS" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_SAW) / " + sKpcsValue + ", 0) AS OUT_SAW" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_BG) / " + sKpcsValue + ", 0) AS OUT_BG" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_STEALTH) / " + sKpcsValue + ", 0) AS OUT_STEALTH" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_PRI) / " + sKpcsValue + ", 0) AS OUT_PRI" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_LAMI) / " + sKpcsValue + ", 0) AS OUT_LAMI" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OUT_STOCK) / " + sKpcsValue + ", 0) AS OUT_STOCK" + "\n");
                }
                #endregion

                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT * FROM " + "\n");
                strSqlString.Append("        ( " + "\n");
                strSqlString.Append("        SELECT MAT.*" + "\n");
                strSqlString.Append("             , NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(PLN.ORI_PLN/MAT.NET_DIE,0) ELSE PLN.ORI_PLN END, 0) * PRICE AS ORI_PLN" + "\n");
                strSqlString.Append("             , NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(PLN.REV_PLN/MAT.NET_DIE,0) ELSE PLN.REV_PLN END, 0) * PRICE AS REV_PLN" + "\n");
                strSqlString.Append("             , NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SHP.SHP_TTL/MAT.NET_DIE,0) ELSE SHP.SHP_TTL END, 0) * PRICE AS SHP_TTL" + "\n");
                strSqlString.Append("             , NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(PLN.D0_PLAN/MAT.NET_DIE,0) ELSE PLN.D0_PLAN END, 0) * PRICE AS D0_PLAN" + "\n");
                strSqlString.Append("             , NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(PLN.D1_PLAN/MAT.NET_DIE,0) ELSE PLN.D1_PLAN END, 0) * PRICE AS D1_PLAN" + "\n");
                strSqlString.Append("             , NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SHP.SHP_TODAY/MAT.NET_DIE,0) ELSE SHP.SHP_TODAY END, 0) * PRICE AS SHP_TODAY " + "\n");
                strSqlString.Append("             , NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SHP.SHP_WEEK/MAT.NET_DIE,0) ELSE SHP.SHP_WEEK END, 0) * PRICE AS SHP_WEEK " + "\n");

                strSqlString.Append("             , NVL(STOCK,0) * PRICE AS STOCK, NVL(BG,0) * PRICE AS BG, NVL(SAW,0) * PRICE AS SAW, NVL(SP,0) * PRICE AS SP, NVL(SMT,0) * PRICE AS SMT, NVL(DA,0) * PRICE AS DA" + "\n");
                strSqlString.Append("             , NVL(S_DA1,0) * PRICE AS S_DA1, NVL(S_DA2,0) * PRICE AS S_DA2, NVL(S_DA3,0) * PRICE AS S_DA3, NVL(S_DA4,0) * PRICE AS S_DA4, NVL(S_DA5,0) * PRICE AS S_DA5" + "\n");
                strSqlString.Append("             , NVL(S_DA6,0) * PRICE AS S_DA6, NVL(S_DA7,0) * PRICE AS S_DA7, NVL(S_DA8,0) * PRICE AS S_DA8, NVL(S_DA9,0) * PRICE AS S_DA9" + "\n");
                strSqlString.Append("             , NVL(F_DA1,0) * PRICE AS F_DA1, NVL(F_DA2,0) * PRICE AS F_DA2, NVL(F_DA3,0) * PRICE AS F_DA3, NVL(F_DA4,0) * PRICE AS F_DA4, NVL(F_DA5,0) * PRICE AS F_DA5" + "\n");
                strSqlString.Append("             , NVL(F_DA6,0) * PRICE AS F_DA6, NVL(F_DA7,0) * PRICE AS F_DA7, NVL(F_DA8,0) * PRICE AS F_DA8, NVL(F_DA9,0) * PRICE AS F_DA9" + "\n");
                strSqlString.Append("             , NVL(WB,0) * PRICE AS WB, NVL(S_WB1,0) * PRICE AS S_WB1, NVL(S_WB2,0) * PRICE AS S_WB2, NVL(S_WB3,0) * PRICE AS S_WB3, NVL(S_WB4,0) * PRICE AS S_WB4" + "\n");
                strSqlString.Append("             , NVL(S_WB5,0) * PRICE AS S_WB5, NVL(S_WB6,0) * PRICE AS S_WB6, NVL(S_WB7,0) * PRICE AS S_WB7, NVL(S_WB8,0) * PRICE AS S_WB8, NVL(S_WB9,0) * PRICE AS S_WB9" + "\n");
                strSqlString.Append("             , NVL(F_WB1,0) * PRICE AS F_WB1, NVL(F_WB2,0) * PRICE AS F_WB2, NVL(F_WB3,0) * PRICE AS F_WB3, NVL(F_WB4,0) * PRICE AS F_WB4" + "\n");
                strSqlString.Append("             , NVL(F_WB5,0) * PRICE AS F_WB5, NVL(F_WB6,0) * PRICE AS F_WB6, NVL(F_WB7,0) * PRICE AS F_WB7, NVL(F_WB8,0) * PRICE AS F_WB8, NVL(F_WB9,0) * PRICE AS F_WB9" + "\n");
                strSqlString.Append("             , NVL(GATE,0) * PRICE AS GATE, NVL(F_GATE,0) * PRICE  AS F_GATE, NVL(MOLD,0) * PRICE AS MOLD, NVL(CURE,0) * PRICE AS CURE" + "\n");
                strSqlString.Append("             , NVL(MK,0) * PRICE AS MK, NVL(TRIM,0) * PRICE AS TRIM, NVL(TIN,0) * PRICE AS TIN, NVL(SBA,0) * PRICE AS SBA, NVL(SIG,0) * PRICE AS SIG" + "\n");
                strSqlString.Append("             , NVL(AVI,0) * PRICE AS AVI, NVL(VI,0) * PRICE AS VI, NVL(PVI,0) * PRICE AS PVI, NVL(QC_GATE,0) * PRICE AS QC_GATE, NVL(HMK3A,0) * PRICE AS HMK3A" + "\n");

                strSqlString.Append("             , NVL(CASE WHEN MAT.MAT_CMF_11 = 'JZH' THEN NVL(RCV.RCV_QTY,0)/MAT.COMP_CNT/2" + "\n");
                strSqlString.Append("                        WHEN MAT.MAT_CMF_11 IN ('DHL', 'DQZ', 'DRA', 'DQA', 'DND', 'DWG', 'DUT', 'HAU', 'HF8', 'HDM', 'HDR', 'HEN', 'HQA', 'HQC') THEN DECODE(MAT.MAT_GRP_5, '3rd', NVL(RCV.RCV_QTY,0)/MAT.COMP_CNT, 0)" + "\n");
                strSqlString.Append("                        WHEN MAT.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND (MAT_ID LIKE 'SEKS%' OR MAT_ID LIKE 'SEKY%')) THEN DECODE(MAT.MAT_GRP_5, '2nd', NVL(RCV.RCV_QTY,0)/MAT.COMP_CNT, 0)" + "\n");
                strSqlString.Append("                        WHEN MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' THEN CASE WHEN MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%' THEN NVL(RCV.RCV_QTY,0)/MAT.COMP_CNT ELSE 0 END" + "\n");
                strSqlString.Append("                        WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(RCV.RCV_QTY/NET_DIE,0)" + "\n");
                strSqlString.Append("                        ELSE NVL(RCV.RCV_QTY,0)/MAT.COMP_CNT" + "\n");
                strSqlString.Append("                   END,0) * PRICE AS RCV_QTY" + "\n");                

                strSqlString.Append("             , NVL(MOV.OUT_WB,0) * PRICE AS OUT_WB" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_DA,0) * PRICE AS OUT_DA" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_DP,0) * PRICE AS OUT_DP" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_HMKA3,0) * PRICE AS OUT_HMKA3" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_CLOSE,0) * PRICE AS OUT_CLOSE" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_SIG,0) * PRICE AS OUT_SIG" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_MK,0) * PRICE AS OUT_MK" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_MOLD,0) * PRICE AS OUT_MOLD" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_WB9,0) * PRICE AS OUT_WB9" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_DA9,0) * PRICE AS OUT_DA9" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_WB8,0) * PRICE AS OUT_WB8" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_DA8,0) * PRICE AS OUT_DA8" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_WB7,0) * PRICE AS OUT_WB7" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_DA7,0) * PRICE AS OUT_DA7" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_WB6,0) * PRICE AS OUT_WB6" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_DA6,0) * PRICE AS OUT_DA6" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_WB5,0) * PRICE AS OUT_WB5" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_DA5,0) * PRICE AS OUT_DA5" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_WB4,0) * PRICE AS OUT_WB4" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_DA4,0) * PRICE AS OUT_DA4" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_WB3,0) * PRICE AS OUT_WB3" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_DA3,0) * PRICE AS OUT_DA3" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_WB2,0) * PRICE AS OUT_WB2" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_DA2,0) * PRICE AS OUT_DA2" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_WB1,0) * PRICE AS OUT_WB1" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_DA1,0) * PRICE AS OUT_DA1" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_DDS,0) * PRICE AS OUT_DDS" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_SAW,0) * PRICE AS OUT_SAW" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_BG,0) * PRICE AS OUT_BG" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_STEALTH,0) * PRICE AS OUT_STEALTH" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_PRI,0) * PRICE AS OUT_PRI" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_LAMI,0) * PRICE AS OUT_LAMI" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_STOCK,0) * PRICE AS OUT_STOCK" + "\n");

                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT A.MAT_ID, MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5" + "\n");
                strSqlString.Append("                     , MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, MAT_GRP_9, MAT_GRP_10, MAT_CMF_10, MAT_CMF_11" + "\n");
                strSqlString.Append("                     , NET_DIE, COMP_CNT, HX_COMP_MIN, HX_COMP_MAX " + "\n");
                strSqlString.Append("                     , CASE WHEN MAT_GRP_1 = 'SE' AND MAT_GRP_9 = 'MEMORY' THEN 'SEK_________-___' || SUBSTR(A.MAT_ID, -3) " + "\n");
                strSqlString.Append("                            WHEN MAT_GRP_1 = 'HX' THEN MAT_CMF_10 " + "\n");
                strSqlString.Append("                            ELSE A.MAT_ID " + "\n");
                strSqlString.Append("                       END CONV_MAT_ID " + "\n");

                if (rdbSales.Checked == true)
                {
                    //strSqlString.Append("                     , PRICE" + "\n");
                    strSqlString.Append("                     , CASE WHEN PRICE IS NULL OR PRICE = 1 THEN PKG_PRICE" + "\n");
                    strSqlString.Append("                            ELSE PRICE" + "\n");
                    strSqlString.Append("                       END PRICE" + "\n");
                    strSqlString.Append("                  FROM VWIPMATDEF A" + "\n");
                    strSqlString.Append("                     , (" + "\n");
                    strSqlString.Append("                        SELECT MAT_ID, MAX(PRICE) AS PRICE" + "\n");
                    strSqlString.Append("                          FROM (" + "\n");
                    strSqlString.Append("                                SELECT MAT_ID, DECODE(MCP_TO_PART, PRODUCT, PRICE, 1) AS PRICE" + "\n");
                    strSqlString.Append("                                  FROM RWIPMCPBOM A" + "\n");
                    strSqlString.Append("                                     , (" + "\n");
                    strSqlString.Append("                                        SELECT PRODUCT " + "\n");
                    strSqlString.Append("                                             , DECODE(CRNC_UNIT, 'USD', PRICE * STD_RATE, PRICE) AS PRICE " + "\n");
                    strSqlString.Append("                                          FROM RPRIMATDAT" + "\n");
                    strSqlString.Append("                                             , (SELECT STD_RATE" + "\n");
                    strSqlString.Append("                                                  FROM " + sExchange + "\n");
                    strSqlString.Append("                                                 WHERE SUBSTR(REPLACE(APPRL_DT, '-', ''), 0, 6) = '" + month + "' " + "\n");
                    strSqlString.Append("                                                )" + "\n");
                    strSqlString.Append("                                         WHERE 1=1 " + "\n");
                    strSqlString.Append("                                           AND SUBSTR(ITEM_CD,10,2) = 'A0'  " + "\n");
                    strSqlString.Append("                                       ) B " + "\n");
                    strSqlString.Append("                                 WHERE MCP_TO_PART = PRODUCT(+) " + "\n");
                    strSqlString.Append("                                   AND MAT_ID IS NOT NULL " + "\n");
                    strSqlString.Append("                                 UNION " + "\n");
                    strSqlString.Append("                                SELECT PRODUCT " + "\n");
                    strSqlString.Append("                                     , DECODE(CRNC_UNIT, 'USD', PRICE * STD_RATE, PRICE) AS PRICE " + "\n");
                    strSqlString.Append("                                  FROM RPRIMATDAT " + "\n");
                    strSqlString.Append("                                     , (SELECT STD_RATE" + "\n");
                    strSqlString.Append("                                          FROM " + sExchange + "\n");
                    strSqlString.Append("                                          WHERE SUBSTR(REPLACE(APPRL_DT, '-', ''), 0, 6) = '" + month + "' " + "\n");
                    strSqlString.Append("                                       )" + "\n");
                    strSqlString.Append("                                 WHERE 1=1 " + "\n");
                    strSqlString.Append("                                   AND SUBSTR(ITEM_CD,10,2) = 'A0' " + "\n");
                    strSqlString.Append("                               )" + "\n");
                    strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("                       ) B" + "\n");
                    strSqlString.Append("                     , (" + "\n");
                    strSqlString.Append("                        SELECT B.MAT_GRP_1 AS CUST_CODE, B.MAT_CMF_11 AS PKG_CODE, ROUND(AVG(PRICE), 0) AS PKG_PRICE" + "\n");
                    strSqlString.Append("                          FROM RPRIMATDAT A" + "\n");
                    strSqlString.Append("                             , MWIPMATDEF B" + "\n");
                    strSqlString.Append("                         WHERE 1=1" + "\n");
                    strSqlString.Append("                           AND A.PRODUCT = B.MAT_ID" + "\n");
                    strSqlString.Append("                           AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                           AND B.DELETE_FLAG = ' '" + "\n");
                    strSqlString.Append("                           AND SUBSTR(ITEM_CD,10,2) = 'A0' " + "\n");
                    strSqlString.Append("                           AND NVL(A.PRICE,0) > 0 " + "\n");
                    strSqlString.Append("                           AND A.PRODUCT LIKE 'SE%' " + "\n");
                    strSqlString.Append("                         GROUP BY MAT_GRP_1, MAT_CMF_11" + "\n");
                    strSqlString.Append("                       ) C" + "\n");
                    strSqlString.Append("                 WHERE 1=1" + "\n");
                    strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
                    strSqlString.Append("                   AND A.MAT_GRP_1 = C.CUST_CODE(+)" + "\n");
                    strSqlString.Append("                   AND A.MAT_CMF_11 = C.PKG_CODE(+)" + "\n");
                }
                else if (rdbTurnkey.Checked == true)
                {
                    //strSqlString.Append("                     , PRICE" + "\n");
                    strSqlString.Append("                     , CASE WHEN PRICE IS NULL OR PRICE = 1 THEN PKG_PRICE" + "\n");
                    strSqlString.Append("                            ELSE PRICE" + "\n");
                    strSqlString.Append("                       END PRICE" + "\n");
                    strSqlString.Append("                  FROM VWIPMATDEF A" + "\n");
                    strSqlString.Append("                     , (" + "\n");
                    strSqlString.Append("                        SELECT MAT_ID, MAX(PRICE) AS PRICE" + "\n");
                    strSqlString.Append("                          FROM (" + "\n");
                    strSqlString.Append("                                SELECT MAT_ID, DECODE(MCP_TO_PART, PRODUCT, PRICE, 1) AS PRICE" + "\n");
                    strSqlString.Append("                                  FROM RWIPMCPBOM A" + "\n");
                    strSqlString.Append("                                     , (" + "\n");
                    strSqlString.Append("                                        SELECT PRODUCT, NVL(A_PRICE,0) + NVL(T_PRICE,0) AS PRICE" + "\n");
                    strSqlString.Append("                                          FROM (" + "\n");
                    strSqlString.Append("                                                SELECT PRODUCT, DECODE(CRNC_UNIT, 'USD', PRICE * STD_RATE, PRICE) AS A_PRICE " + "\n");
                    strSqlString.Append("                                                     , (SELECT DECODE(MAX(CRNC_UNIT), 'USD', MAX(PRICE * STD_RATE), MAX(PRICE)) FROM RPRIMATDAT WHERE SUBSTR(ITEM_CD,10,2) = '0T' AND PRODUCT = A.PRODUCT) AS T_PRICE " + "\n");
                    strSqlString.Append("                                                  FROM RPRIMATDAT A" + "\n");
                    strSqlString.Append("                                                     , (SELECT STD_RATE" + "\n");
                    strSqlString.Append("                                                          FROM " + sExchange + "\n");
                    strSqlString.Append("                                                         WHERE SUBSTR(REPLACE(APPRL_DT, '-', ''), 0, 6) = '" + month + "' " + "\n");
                    strSqlString.Append("                                                       )" + "\n");
                    strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                    strSqlString.Append("                                                   AND SUBSTR(ITEM_CD,10,2) = 'A0' " + "\n");
                    strSqlString.Append("                                                   AND NVL(A.PRICE,0) > 0 " + "\n");
                    strSqlString.Append("                                               )" + "\n");
                    strSqlString.Append("                                       ) B " + "\n");
                    strSqlString.Append("                                 WHERE MCP_TO_PART = PRODUCT(+) " + "\n");
                    strSqlString.Append("                                   AND MAT_ID IS NOT NULL " + "\n");
                    strSqlString.Append("                                 UNION " + "\n");
                    strSqlString.Append("                                SELECT PRODUCT, NVL(A_PRICE,0) + NVL(T_PRICE,0) AS PRICE" + "\n");
                    strSqlString.Append("                                  FROM (" + "\n");
                    strSqlString.Append("                                        SELECT PRODUCT, DECODE(CRNC_UNIT, 'USD', PRICE * STD_RATE, PRICE) AS A_PRICE " + "\n");
                    strSqlString.Append("                                             , (SELECT DECODE(MAX(CRNC_UNIT), 'USD', MAX(PRICE * STD_RATE), MAX(PRICE)) FROM RPRIMATDAT WHERE SUBSTR(ITEM_CD,10,2) = '0T' AND PRODUCT = A.PRODUCT) AS T_PRICE " + "\n");
                    strSqlString.Append("                                          FROM RPRIMATDAT A" + "\n");
                    strSqlString.Append("                                             , (SELECT STD_RATE" + "\n");
                    strSqlString.Append("                                                  FROM " + sExchange + "\n");
                    strSqlString.Append("                                                 WHERE SUBSTR(REPLACE(APPRL_DT, '-', ''), 0, 6) = '" + month + "' " + "\n");
                    strSqlString.Append("                                               )" + "\n");
                    strSqlString.Append("                                         WHERE 1=1 " + "\n");
                    strSqlString.Append("                                           AND SUBSTR(ITEM_CD,10,2) = 'A0' " + "\n");
                    strSqlString.Append("                                           AND NVL(A.PRICE,0) > 0 " + "\n");
                    strSqlString.Append("                                       )" + "\n");                    
                    strSqlString.Append("                               )" + "\n");
                    strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("                       ) B" + "\n");
                    strSqlString.Append("                     , (" + "\n");
                    strSqlString.Append("                        SELECT MAT_GRP_1 AS CUST_CODE, MAT_CMF_11 AS PKG_CODE, ROUND(NVL(AVG(A_PRICE),0) + NVL(AVG(T_PRICE),0),0) AS PKG_PRICE" + "\n");
                    strSqlString.Append("                          FROM (" + "\n");
                    strSqlString.Append("                                SELECT B.MAT_GRP_1, B.MAT_CMF_11, PRICE AS A_PRICE" + "\n");
                    strSqlString.Append("                                     , (SELECT MAX(PRICE) FROM RPRIMATDAT WHERE SUBSTR(ITEM_CD,10,2) = '0T' AND PRODUCT = A.PRODUCT) AS T_PRICE" + "\n");
                    strSqlString.Append("                                  FROM RPRIMATDAT A" + "\n");
                    strSqlString.Append("                                     , MWIPMATDEF B" + "\n");
                    strSqlString.Append("                                 WHERE 1=1" + "\n");
                    strSqlString.Append("                                   AND A.PRODUCT = B.MAT_ID" + "\n");
                    strSqlString.Append("                                   AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                                   AND B.DELETE_FLAG = ' '" + "\n");
                    strSqlString.Append("                                   AND SUBSTR(ITEM_CD,10,2) = 'A0' " + "\n");
                    strSqlString.Append("                                   AND NVL(A.PRICE,0) > 0 " + "\n");
                    strSqlString.Append("                                   AND A.PRODUCT LIKE 'SE%' " + "\n");
                    strSqlString.Append("                               )" + "\n");
                    strSqlString.Append("                         GROUP BY MAT_GRP_1, MAT_CMF_11" + "\n");
                    strSqlString.Append("                       ) C" + "\n");
                    strSqlString.Append("                 WHERE 1=1" + "\n");
                    strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
                    strSqlString.Append("                   AND A.MAT_GRP_1 = C.CUST_CODE(+)" + "\n");
                    strSqlString.Append("                   AND A.MAT_CMF_11 = C.PKG_CODE(+)" + "\n");
                }
                else
                {
                    strSqlString.Append("                     , 1 AS PRICE" + "\n");
                    strSqlString.Append("                  FROM VWIPMATDEF A" + "\n");
                    strSqlString.Append("                 WHERE 1=1" + "\n");
                }

                strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND A.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                   AND A.MAT_TYPE = 'FG' " + "\n");
                strSqlString.Append("                   AND A.MAT_GRP_2 <> 'PW' " + "\n");
                strSqlString.Append("               ) MAT" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID " + "\n");
                strSqlString.Append("                     , SUM(ORI_PLN) AS ORI_PLN " + "\n");
                strSqlString.Append("                     , SUM(REV_PLN) AS REV_PLN " + "\n");
                strSqlString.Append("                     , SUM(D0_PLAN) AS D0_PLAN " + "\n");
                strSqlString.Append("                     , SUM(D1_PLAN) AS D1_PLAN " + "\n");
                strSqlString.Append("                  FROM ( " + "\n");
                strSqlString.Append("                        SELECT MAT_ID, SUM(PLAN_QTY_ASSY) AS ORI_PLN, SUM(RESV_FIELD1) AS REV_PLN, 0 AS D0_PLAN, 0 AS D1_PLAN" + "\n");
                strSqlString.Append("                          FROM (" + "\n");
                strSqlString.Append("                                SELECT MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, SUM(TO_NUMBER(DECODE(RESV_FIELD1,' ',0,RESV_FIELD1))) AS RESV_FIELD1" + "\n");
                strSqlString.Append("                                  FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                                   AND PLAN_MONTH = '" + month + "'" + "\n");
                strSqlString.Append("                                 GROUP BY MAT_ID " + "\n");
                strSqlString.Append("                               ) " + "\n");
                strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
                strSqlString.Append("                        HAVING SUM(PLAN_QTY_ASSY + RESV_FIELD1) > 0" + "\n");
                strSqlString.Append("                         UNION ALL" + "\n");
                strSqlString.Append("                        SELECT MAT_ID, 0, 0" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 7 THEN D0_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 1 THEN D0_QTY + D1_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 2 THEN D0_QTY + D1_QTY + D2_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 3 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 4 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 5 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 6 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY + D6_QTY " + "\n");
                strSqlString.Append("                                        ELSE 0 " + "\n");
                strSqlString.Append("                                   END) AS D0_PLAN " + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 7 THEN D1_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 1 THEN D2_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 2 THEN D3_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 3 THEN D4_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 4 THEN D5_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 5 THEN D6_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 6 THEN D7_QTY " + "\n");
                strSqlString.Append("                                        ELSE 0 " + "\n");
                strSqlString.Append("                                   END) AS D1_PLAN " + "\n");
                strSqlString.Append("                          FROM (" + "\n");
                strSqlString.Append("                                SELECT FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D0_QTY, 0)) AS D0_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D1_QTY, 0)) AS D1_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D2_QTY, 0)) AS D2_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D3_QTY, 0)) AS D3_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D4_QTY, 0)) AS D4_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D5_QTY, 0)) AS D5_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D6_QTY, 0)) AS D6_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D0_QTY, 0)) AS D7_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D1_QTY, 0)) AS D8_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D2_QTY, 0)) AS D9_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D3_QTY, 0)) AS D10_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D4_QTY, 0)) AS D11_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D5_QTY, 0)) AS D12_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D6_QTY, 0)) AS D13_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', WW_QTY, 0)) AS W1_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', WW_QTY, 0)) AS W2_QTY  " + "\n");
                strSqlString.Append("                                  FROM RWIPPLNWEK  " + "\n");
                strSqlString.Append("                                 WHERE 1=1  " + "\n");
                strSqlString.Append("                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
                strSqlString.Append("                                   AND GUBUN = '3'  " + "\n");
                strSqlString.Append("                                   AND PLAN_WEEK IN ('" + FindWeek_SOP_A.ThisWeek + "','" + FindWeek_SOP_A.NextWeek + "') " + "\n");
                strSqlString.Append("                                 GROUP BY FACTORY, MAT_ID  " + "\n");
                strSqlString.Append("                               )  " + "\n");
                strSqlString.Append("                         GROUP BY MAT_ID  " + "\n");
                strSqlString.Append("                       ) " + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID  " + "\n");
                strSqlString.Append("               ) PLN" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + month + "01' AND '" + end_date + "' THEN SHP_QTY_1 END) AS SHP_TTL " + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + end_date + "', SHP_QTY_1, 0)) AS SHP_TODAY" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + yesterday + "' THEN SHP_QTY_1 END) AS SHP_WEEK " + "\n");
                strSqlString.Append("                  FROM VSUMWIPOUT" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + start_date + "' AND '" + end_date + "' " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                   AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                   AND CM_KEY_3 LIKE 'P%'" + "\n");
                strSqlString.Append("                   AND MAT_ID NOT LIKE 'HX%'" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
                strSqlString.Append("                 UNION ALL " + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + month + "01' AND '" + end_date + "' THEN SHP_QTY_1 END) AS SHP_TTL " + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + end_date + "', SHP_QTY_1, 0)) AS SHP_TODAY" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + yesterday + "' THEN SHP_QTY_1 END) AS SHP_WEEK " + "\n");
                strSqlString.Append("                  FROM VSUMWIPOUT_06" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + start_date + "' AND '" + end_date + "' " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                   AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                   AND CM_KEY_3 LIKE 'P%'" + "\n");
                strSqlString.Append("                   AND MAT_ID LIKE 'HX%'" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID " + "\n");                
                strSqlString.Append("               ) SHP" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID, SUM(RCV_QTY_1) AS RCV_QTY" + "\n");
                strSqlString.Append("                  FROM VSUMWIPRCV" + "\n");
                strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND WORK_DATE = '" + end_date + "'" + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                   AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) RCV" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append("                     , SUM(QTY) AS WIP_TTL" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'HMK2A', QTY, 0)) AS STOCK" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'B/G', QTY, 0)) AS BG" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'SAW', QTY, 0)) AS SAW" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'S/P', QTY, 0)) AS SP " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'SMT', QTY, 0)) AS SMT " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'D/A', QTY, 0)) AS DA" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0250', 'A0305', 'A0306', 'A0310', 'A0333', 'A0340', 'A0400', 'A0401', 'A0500', 'A0501', 'A0530', 'A0531') THEN QTY ELSE 0 END) AS S_DA1" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0402', 'A0502', 'A0532') THEN QTY ELSE 0 END) AS S_DA2" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0403', 'A0503', 'A0533') THEN QTY ELSE 0 END) AS S_DA3" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0404', 'A0504', 'A0534') THEN QTY ELSE 0 END) AS S_DA4" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0405', 'A0505', 'A0535') THEN QTY ELSE 0 END) AS S_DA5" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0406', 'A0506', 'A0536') THEN QTY ELSE 0 END) AS S_DA6" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0407', 'A0507', 'A0537') THEN QTY ELSE 0 END) AS S_DA7" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0408', 'A0508', 'A0538') THEN QTY ELSE 0 END) AS S_DA8" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0409', 'A0509', 'A0539') THEN QTY ELSE 0 END) AS S_DA9" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0250','A0333','A0340','A0400', 'A0401') THEN QTY ELSE 0 END) AS F_DA1" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0402') THEN QTY " + "\n");
                strSqlString.Append("                                WHEN OPER = 'A0801' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN QTY " + "\n");
                strSqlString.Append("                                WHEN OPER = 'A0801' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN QTY " + "\n");
                strSqlString.Append("                                ELSE 0 END) AS F_DA2 " + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0403') THEN QTY " + "\n");
                strSqlString.Append("                                WHEN OPER = 'A0802' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN QTY " + "\n");
                strSqlString.Append("                                WHEN OPER = 'A0802' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN QTY " + "\n");
                strSqlString.Append("                                ELSE 0 END) AS F_DA3 " + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0404') THEN QTY " + "\n");
                strSqlString.Append("                                WHEN OPER = 'A0803' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN QTY " + "\n");
                strSqlString.Append("                                WHEN OPER = 'A0803' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN QTY " + "\n");
                strSqlString.Append("                                ELSE 0 END) AS F_DA4 " + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0405') THEN QTY " + "\n");
                strSqlString.Append("                                WHEN OPER = 'A0804' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN QTY " + "\n");
                strSqlString.Append("                                WHEN OPER = 'A0804' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN QTY " + "\n");
                strSqlString.Append("                                ELSE 0 END) AS F_DA5 " + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0406') THEN QTY " + "\n");
                strSqlString.Append("                                WHEN OPER = 'A0805' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN QTY " + "\n");
                strSqlString.Append("                                WHEN OPER = 'A0805' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN QTY " + "\n");
                strSqlString.Append("                                ELSE 0 END) AS F_DA6 " + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0407') THEN QTY " + "\n");
                strSqlString.Append("                                WHEN OPER = 'A0806' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN QTY " + "\n");
                strSqlString.Append("                                WHEN OPER = 'A0806' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN QTY " + "\n");
                strSqlString.Append("                                ELSE 0 END) AS F_DA7 " + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0408') THEN QTY " + "\n");
                strSqlString.Append("                                WHEN OPER = 'A0807' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN QTY " + "\n");
                strSqlString.Append("                                WHEN OPER = 'A0807' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN QTY " + "\n");
                strSqlString.Append("                                ELSE 0 END) AS F_DA8 " + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0409') THEN QTY " + "\n");
                strSqlString.Append("                                WHEN OPER = 'A0808' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 <> 'Merge' THEN QTY " + "\n");
                strSqlString.Append("                                WHEN OPER = 'A0808' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) <> SUBSTR(OPER, -1) THEN QTY " + "\n");
                strSqlString.Append("                                ELSE 0 END) AS F_DA9 " + "\n"); 
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'W/B', QTY, 0)) AS WB" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0550', 'A0551', 'A0600','A0601', 'A0800', 'A0801') THEN QTY ELSE 0 END) AS S_WB1" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0552', 'A0602', 'A0802') THEN QTY ELSE 0 END) AS S_WB2" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0553', 'A0603', 'A0803') THEN QTY ELSE 0 END) AS S_WB3" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0554', 'A0604', 'A0804') THEN QTY ELSE 0 END) AS S_WB4" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0555', 'A0605', 'A0805') THEN QTY ELSE 0 END) AS S_WB5" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0556', 'A0606', 'A0806') THEN QTY ELSE 0 END) AS S_WB6" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0557', 'A0607', 'A0807') THEN QTY ELSE 0 END) AS S_WB7" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0558', 'A0608', 'A0808') THEN QTY ELSE 0 END) AS S_WB8" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0559', 'A0609', 'A0809') THEN QTY ELSE 0 END) AS S_WB9" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0550', 'A0551', 'A0600','A0601', 'A0500', 'A0501', 'A0530', 'A0531') THEN QTY ELSE 0 END) AS F_WB1" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0552', 'A0602', 'A0502', 'A0532') THEN QTY ELSE 0 END) AS F_WB2" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0553', 'A0603', 'A0503', 'A0533') THEN QTY ELSE 0 END) AS F_WB3" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0554', 'A0604', 'A0504', 'A0534') THEN QTY ELSE 0 END) AS F_WB4" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0555', 'A0605', 'A0505', 'A0535') THEN QTY ELSE 0 END) AS F_WB5" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0556', 'A0606', 'A0506', 'A0536') THEN QTY ELSE 0 END) AS F_WB6" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0557', 'A0607', 'A0507', 'A0537') THEN QTY ELSE 0 END) AS F_WB7" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0558', 'A0608', 'A0508', 'A0538') THEN QTY ELSE 0 END) AS F_WB8" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0559', 'A0609', 'A0509', 'A0539') THEN QTY ELSE 0 END) AS F_WB9" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'GATE', QTY, 0)) AS GATE" + "\n");                
                strSqlString.Append("                     , SUM(CASE WHEN OPER_GRP_1 = 'GATE' AND MAT_GRP_5 = '-' THEN QTY " + "\n");
                strSqlString.Append("                                WHEN OPER_GRP_1 = 'GATE' AND MAT_GRP_1 = 'SE' AND MAT_GRP_5 = 'Merge' THEN QTY" + "\n");
                strSqlString.Append("                                WHEN OPER_GRP_1 = 'GATE' AND MAT_GRP_1 <> 'SE' AND SUBSTR(MAT_GRP_4,-1) = SUBSTR(OPER, -1) THEN QTY  " + "\n");
                strSqlString.Append("                                ELSE 0 END) AS F_GATE " + "\n"); 
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'MOLD', QTY, 0)) AS MOLD" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'CURE', QTY, 0)) AS CURE" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'M/K', QTY, 0)) AS MK" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'TRIM', QTY, 0)) AS TRIM" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'TIN', QTY, 0)) AS TIN" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'S/B/A', QTY, 0)) AS SBA" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'SIG', QTY, 0)) AS SIG" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'AVI', QTY, 0)) AS AVI" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'V/I', QTY, 0)) AS VI" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A2030', 'A2050') THEN QTY ELSE 0 END) AS PVI" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A2100', 'A2350') THEN QTY ELSE 0 END) AS QC_GATE" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'HMK3A', QTY, 0)) AS HMK3A" + "\n");
                strSqlString.Append("                  FROM (" + "\n");
                strSqlString.Append("                        SELECT MAT_ID, OPER, OPER_GRP_1, MAT_GRP_1, MAT_GRP_4, MAT_GRP_5" + "\n");
                strSqlString.Append("                             , CASE WHEN HX_COMP_MIN IS NOT NULL" + "\n");
                strSqlString.Append("                                         THEN (CASE WHEN HX_COMP_MIN <> HX_COMP_MAX AND OPER > HX_COMP_MIN AND OPER <= HX_COMP_MAX THEN QTY_1 / NVL(COMP_CNT / 2,1)" + "\n");
                strSqlString.Append("                                                    WHEN OPER <= HX_COMP_MAX THEN QTY_1 / NVL(COMP_CNT,1)" + "\n");
                strSqlString.Append("                                                    ELSE QTY_1 END)" + "\n");
                strSqlString.Append("                                    WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1) " + "\n");
                strSqlString.Append("                                    ELSE QTY_1 " + "\n");
                strSqlString.Append("                               END QTY " + "\n");
                //strSqlString.Append("                             , CASE WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1) ELSE QTY_1 END AS QTY" + "\n");
                strSqlString.Append("                          FROM (" + "\n");
                strSqlString.Append("                                SELECT A.MAT_ID, B.OPER, B.OPER_GRP_1, C.MAT_GRP_1, C.MAT_GRP_4, C.MAT_GRP_5" + "\n");
                strSqlString.Append("                                     , CASE WHEN MAT_CMF_11 IN ('DHL', 'DQZ', 'DRA', 'DQA', 'DND', 'DWG', 'DUT', 'HAU', 'HF8', 'HDM', 'HDR', 'HEN', 'HQA', 'HQC') THEN (CASE WHEN MAT_GRP_5 IN ('3rd','Merge') THEN QTY_1" + "\n");
                strSqlString.Append("                                                                               WHEN MAT_GRP_5 LIKE 'Middle%' AND MAT_GRP_5 <> 'Middle' THEN QTY_1" + "\n");
                strSqlString.Append("                                                                               ELSE 0 END)" + "\n");
                strSqlString.Append("                                            WHEN MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND (MAT_ID LIKE 'SEKS%' OR MAT_ID LIKE 'SEKY%')) THEN CASE WHEN MAT_GRP_5 IN ('2nd','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN QTY_1 ELSE 0 END" + "\n");
                strSqlString.Append("                                            WHEN MAT_GRP_1 = 'HX' AND MESMGR.F_GET_ATTR_VALUE@RPTTOMES(A.MAT_ID, 'MAT_ETC', 'HX_VERSION') = 'A-376' THEN CASE WHEN MAT_GRP_5 IN ('5th','Merge') THEN QTY_1 ELSE 0 END" + "\n");
                strSqlString.Append("                                            WHEN MAT_GRP_1 = 'HX' AND MESMGR.F_GET_ATTR_VALUE@RPTTOMES(A.MAT_ID, 'MAT_ETC', 'HX_VERSION') IN ('A-445', 'A-525') THEN CASE WHEN MAT_GRP_5 IN ('6th','Merge') THEN QTY_1 ELSE 0 END" + "\n");
                strSqlString.Append("                                            WHEN MAT_GRP_1 = 'HX' AND MESMGR.F_GET_ATTR_VALUE@RPTTOMES(A.MAT_ID, 'MAT_ETC', 'HX_VERSION') = 'A-524' THEN CASE WHEN MAT_GRP_5 IN ('7th','Merge') THEN QTY_1 ELSE 0 END" + "\n");
                strSqlString.Append("                                            WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN QTY_1 ELSE 0 END" + "\n");
                strSqlString.Append("                                            WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(QTY_1/C.NET_DIE,0)" + "\n");
                strSqlString.Append("                                            ELSE QTY_1" + "\n");
                strSqlString.Append("                                       END AS QTY_1" + "\n");                
                strSqlString.Append("                                     , C.COMP_CNT, C.HX_COMP_MIN, C.HX_COMP_MAX " + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == end_date)
                {
                    strSqlString.Append("                                  FROM RWIPLOTSTS A  " + "\n");
                    strSqlString.Append("                                     , MWIPOPRDEF B  " + "\n");
                    strSqlString.Append("                                     , VWIPMATDEF C" + "\n");
                    strSqlString.Append("                                 WHERE 1 = 1 " + "\n");
                }
                else
                {
                    strSqlString.Append("                                  FROM RWIPLOTSTS_BOH A  " + "\n");
                    strSqlString.Append("                                     , MWIPOPRDEF B  " + "\n");
                    strSqlString.Append("                                     , VWIPMATDEF C" + "\n");
                    strSqlString.Append("                                 WHERE A.CUTOFF_DT = '" + end_date + "22' " + "\n");
                }

                strSqlString.Append("                                   AND A.FACTORY = B.FACTORY" + "\n");
                strSqlString.Append("                                   AND A.FACTORY = C.FACTORY" + "\n");
                strSqlString.Append("                                   AND A.OPER = B.OPER" + "\n");
                strSqlString.Append("                                   AND A.MAT_ID = C.MAT_ID" + "\n");
                strSqlString.Append("                                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
                strSqlString.Append("                                   AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                   AND A.LOT_CMF_5 LIKE 'P%' " + "\n");
                strSqlString.Append("                                   AND A.LOT_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("                                   AND C.MAT_GRP_2 <> '-'" + "\n");
                strSqlString.Append("                                   AND C.DELETE_FLAG = ' ' " + "\n");
                strSqlString.Append("                                   AND A.HOLD_CODE NOT IN ('H71','H54')" + "\n");
                strSqlString.Append("                               )" + "\n");
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) WIP" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");                
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0200', 'A0230') THEN QTY ELSE 0 END) AS OUT_DP" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER LIKE 'A04%' THEN (CASE WHEN MAT_GRP_1 <> 'SE' AND MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' " + "\n");
                strSqlString.Append("                                                                      THEN DECODE(SUBSTR(MAT_GRP_4,-1), SUBSTR(OPER, -1), QTY, 0)" + "\n");
                strSqlString.Append("                                                                 ELSE QTY END)" + "\n");
                strSqlString.Append("                                WHEN OPER = 'A0333' THEN QTY" + "\n");
                strSqlString.Append("                                ELSE 0" + "\n");
                strSqlString.Append("                           END) AS OUT_DA" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER LIKE 'A06%' THEN (CASE WHEN MAT_GRP_1 <> 'SE' AND MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' " + "\n");
                strSqlString.Append("                                                                      THEN DECODE(SUBSTR(MAT_GRP_4,-1), SUBSTR(OPER, -1), QTY, 0)" + "\n");
                strSqlString.Append("                                                                 ELSE QTY END)" + "\n");
                strSqlString.Append("                                 ELSE 0" + "\n");
                strSqlString.Append("                           END) AS OUT_WB" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0000' THEN QTY ELSE 0 END) AS OUT_STOCK" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0020' THEN QTY ELSE 0 END) AS OUT_LAMI" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0030' THEN QTY ELSE 0 END) AS OUT_PRI" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0033' THEN QTY ELSE 0 END) AS OUT_STEALTH" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0040' THEN QTY ELSE 0 END) AS OUT_BG" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0200' THEN QTY ELSE 0 END) AS OUT_SAW" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0230' THEN QTY ELSE 0 END) AS OUT_DDS" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0400', 'A0401', 'A0333') THEN QTY ELSE 0 END) AS OUT_DA1" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0600', 'A0601') THEN QTY ELSE 0 END) AS OUT_WB1" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0402' THEN QTY ELSE 0 END) AS OUT_DA2" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0602' THEN QTY ELSE 0 END) AS OUT_WB2" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0403' THEN QTY ELSE 0 END) AS OUT_DA3" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0603' THEN QTY ELSE 0 END) AS OUT_WB3" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0404' THEN QTY ELSE 0 END) AS OUT_DA4" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0604' THEN QTY ELSE 0 END) AS OUT_WB4" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0405' THEN QTY ELSE 0 END) AS OUT_DA5" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0605' THEN QTY ELSE 0 END) AS OUT_WB5" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0406' THEN QTY ELSE 0 END) AS OUT_DA6" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0606' THEN QTY ELSE 0 END) AS OUT_WB6" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0407' THEN QTY ELSE 0 END) AS OUT_DA7" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0607' THEN QTY ELSE 0 END) AS OUT_WB7" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0408' THEN QTY ELSE 0 END) AS OUT_DA8" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0608' THEN QTY ELSE 0 END) AS OUT_WB8" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0409' THEN QTY ELSE 0 END) AS OUT_DA9" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0609' THEN QTY ELSE 0 END) AS OUT_WB9" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A1000' THEN QTY ELSE 0 END) AS OUT_MOLD " + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A1150', 'A1500') THEN QTY ELSE 0 END) AS OUT_MK" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A1750', 'A1800', 'A1900') THEN QTY ELSE 0 END) AS OUT_SIG" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A2100' THEN QTY ELSE 0 END) AS OUT_CLOSE " + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'AZ010' THEN QTY ELSE 0 END) AS OUT_HMKA3 " + "\n");
                strSqlString.Append("                  FROM (    " + "\n");
                strSqlString.Append("                        SELECT MAT_ID, OPER, MAT_GRP_1, MAT_GRP_4, MAT_GRP_5" + "\n");
                strSqlString.Append("                             , CASE WHEN HX_COMP_MIN IS NOT NULL" + "\n");
                strSqlString.Append("                                         THEN (CASE WHEN HX_COMP_MIN <> HX_COMP_MAX AND OPER > HX_COMP_MIN AND OPER <= HX_COMP_MAX THEN QTY_1 / NVL(COMP_CNT / 2,1)" + "\n");
                strSqlString.Append("                                                    WHEN OPER <= HX_COMP_MAX THEN QTY_1 / NVL(COMP_CNT,1)" + "\n");
                strSqlString.Append("                                                    ELSE QTY_1 END)" + "\n");
                strSqlString.Append("                                    WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1) " + "\n");
                strSqlString.Append("                                    ELSE QTY_1 " + "\n");
                strSqlString.Append("                               END QTY" + "\n");
                strSqlString.Append("                          FROM (" + "\n");
                strSqlString.Append("                                SELECT MAT_ID, OPER, MAT_GRP_1, MAT_GRP_4, MAT_GRP_5, MAT_GRP_9, MAT_CMF_11" + "\n");
                strSqlString.Append("                                     , CASE WHEN MAT_CMF_11 IN ('DHL', 'DQZ', 'DRA', 'DQA', 'DND', 'DWG', 'DUT', 'HAU', 'HF8', 'HDM', 'HDR', 'HEN', 'HQA', 'HQC') THEN (CASE WHEN MAT_GRP_5 IN ('3rd','Merge') THEN END_QTY ELSE 0 END)" + "\n");
                strSqlString.Append("                                            WHEN MAT_ID LIKE 'SEK%' AND MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND (MAT_ID LIKE 'SEKS%' OR MAT_ID LIKE 'SEKY%'))" + "\n");
                strSqlString.Append("                                                 THEN (CASE WHEN MAT_GRP_5 IN ('2nd','Merge') THEN END_QTY ELSE 0 END)" + "\n");
                strSqlString.Append("                                            WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-'" + "\n");
                strSqlString.Append("                                                 THEN (CASE WHEN MAT_GRP_5 IN ('1st','Merge') THEN END_QTY ELSE 0 END)" + "\n");
                strSqlString.Append("                                            WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(END_QTY/NET_DIE,0)" + "\n");
                strSqlString.Append("                                            ELSE END_QTY" + "\n");
                strSqlString.Append("                                       END QTY_1 " + "\n");
                strSqlString.Append("                                     , COMP_CNT, HX_COMP_MIN, HX_COMP_MAX " + "\n");
                strSqlString.Append("                                  FROM ( " + "\n");
                strSqlString.Append("                                        SELECT A.OPER" + "\n");
                strSqlString.Append("                                             , CASE WHEN OPER = 'AZ010' THEN S1_MOVE_QTY_1+S2_MOVE_QTY_1+S3_MOVE_QTY_1" + "\n");
                strSqlString.Append("                                                    ELSE S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1" + "\n");
                strSqlString.Append("                                               END END_QTY" + "\n");
                strSqlString.Append("                                             , B.*" + "\n");
                strSqlString.Append("                                          FROM RSUMWIPMOV A" + "\n");
                strSqlString.Append("                                             , VWIPMATDEF B" + "\n");
                strSqlString.Append("                                         WHERE 1=1" + "\n");
                strSqlString.Append("                                           AND A.FACTORY = B.FACTORY" + "\n");
                strSqlString.Append("                                           AND A.MAT_ID = B.MAT_ID" + "\n");
                strSqlString.Append("                                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                                           AND A.WORK_DATE = '" + end_date + "'" + "\n");
                strSqlString.Append("                                           AND A.LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                                           AND A.CM_KEY_3 LIKE 'P%'   " + "\n");
                strSqlString.Append("                                           AND B.MAT_GRP_5 NOT LIKE 'Middle%'" + "\n");

                //if (cdvType.Text != "Detail")
                if (cdvType.SelectedIndex != 2)
                {
                    strSqlString.Append("                                           AND REGEXP_LIKE(A.OPER, 'A0000|A0200|A0230|A0333|A040*|A060*|A1000|A2100')" + "\n");
                    strSqlString.Append("                                           AND ((A.OPER >= 'A0400' AND B.MAT_GRP_5 IN ('-', 'Merge')) OR (A.OPER < 'A0400'))" + "\n");
                }

                strSqlString.Append("                                       ) " + "\n");
                strSqlString.Append("                               )" + "\n");
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) MOV" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = RCV.MAT_ID(+)" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = MOV.MAT_ID(+)" + "\n");

                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                {
                    strSqlString.AppendFormat("           AND MAT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
                }

                #region 상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                #endregion

                strSqlString.Append("           AND NVL(ORI_PLN,0)+NVL(REV_PLN,0)+NVL(SHP_TTL,0)+NVL(RCV_QTY,0)+NVL(WIP_TTL,0)+NVL(D0_PLAN,0)+NVL(D1_PLAN,0)+NVL(SHP_WEEK,0) <> 0" + "\n");
                strSqlString.Append("           ) " + "\n");
                strSqlString.Append("           WHERE 1=1" + "\n");
                strSqlString.Append("           AND MAT_ID NOT IN (SELECT MAT_ID FROM MWIPMATDEF WHERE FIRST_FLOW = 'A-BANK' AND DELETE_FLAG = ' ')" + "\n");
                strSqlString.Append("       ) A" + "\n");
                strSqlString.Append(" GROUP BY " + QueryCond1 + "\n");
                strSqlString.Append(" ORDER BY DECODE(A.MAT_GRP_1, 'SE', 1, 'HX', 2, 'IM', 3, 'FC', 4, 'IG', 5,6), " + QueryCond4NotNull + "\n");
            }
            #endregion

            #region TEST -> 사용 안함
            else
            {
                strSqlString.Append("SELECT " + QueryCond2 + "\n");
                strSqlString.Append("     , ROUND(SUM(ORI_PLN) / " + sKpcsValue + ", 0) AS ORI_PLN" + "\n");
                strSqlString.Append("     , ROUND(SUM(REV_PLN) / " + sKpcsValue + ", 0) AS REV_PLN" + "\n");
                strSqlString.Append("     , ROUND(SUM(SHP_TTL) / " + sKpcsValue + ", 0) AS SHP_TTL" + "\n");
                strSqlString.Append("     , ROUND(DECODE(SUM(ORI_PLN), 0, 0, SUM(SHP_TTL) / SUM(ORI_PLN) * 100), 1) AS JINDO_1" + "\n");
                strSqlString.Append("     , ROUND(DECODE(SUM(REV_PLN), 0, 0, SUM(SHP_TTL) / SUM(REV_PLN) * 100), 1) AS JINDO_2" + "\n");
                strSqlString.Append("     , ROUND(SUM(SHP_TODAY) / " + sKpcsValue + ", 0) AS SHP_TODAY" + "\n");
                strSqlString.Append("     , ROUND(SUM(HMK4T) / " + sKpcsValue + ", 0) AS HMK4T" + "\n");
                strSqlString.Append("     , ROUND(SUM(QA2 + BAKE + VI + TNR + PK) / " + sKpcsValue + ", 0) AS EOL" + "\n");
                strSqlString.Append("     , ROUND(SUM(TEST + QA1 + CAS + OS) / " + sKpcsValue + ", 0) AS TEST" + "\n");
                strSqlString.Append("     , ROUND(SUM(HMK3) / " + sKpcsValue + ", 0) AS HMK3" + "\n");
                strSqlString.Append("     , ROUND(SUM(FINISH) / " + sKpcsValue + ", 0) AS FINISH" + "\n");
                strSqlString.Append("     , ROUND(SUM(MOLD) / " + sKpcsValue + ", 0) AS MOLD" + "\n");
                strSqlString.Append("     , ROUND(SUM(FRONT) / " + sKpcsValue + ", 0) AS FRONT" + "\n");
                strSqlString.Append("     , ROUND(SUM(HMK2) / " + sKpcsValue + ", 0) AS HMK2" + "\n");
                strSqlString.Append("     , ROUND(SUM(TEST + QA1 + CAS + OS + QA2 + BAKE + VI + TNR + PK +HMK4T) / " + sKpcsValue + ", 0) AS WIP_TTL " + "\n");
                strSqlString.Append("     , ROUND(SUM(RCV_QTY) / " + sKpcsValue + ", 0) AS RCV_QTY" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT MAT.*" + "\n");
                strSqlString.Append("             , NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(PLN.ORI_PLN/MAT.MAT_CMF_13,0) ELSE PLN.ORI_PLN END, 0) * PRICE AS ORI_PLN" + "\n");
                strSqlString.Append("             , NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(PLN.REV_PLN/MAT.MAT_CMF_13,0) ELSE PLN.REV_PLN END, 0) * PRICE AS REV_PLN" + "\n");
                strSqlString.Append("             , NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SHP.SHP_TTL/MAT.MAT_CMF_13,0) ELSE SHP.SHP_TTL END, 0) * PRICE AS SHP_TTL" + "\n");
                strSqlString.Append("             , NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SHP.SHP_TODAY/MAT.MAT_CMF_13,0) ELSE SHP.SHP_TODAY END, 0) * PRICE AS SHP_TODAY " + "\n");
                strSqlString.Append("             , NVL(HMK2,0) * PRICE AS HMK2, NVL(FRONT,0) * PRICE AS FRONT, NVL(MOLD,0) * PRICE AS MOLD, NVL(FINISH,0) * PRICE AS FINISH, NVL(HMK3,0) * PRICE AS HMK3" + "\n");
                strSqlString.Append("             , NVL(TEST,0) * PRICE AS TEST, NVL(QA1,0) * PRICE AS QA1, NVL(CAS,0) * PRICE AS CAS, NVL(OS,0) * PRICE AS OS, NVL(QA2,0) * PRICE AS QA2" + "\n");
                strSqlString.Append("             , NVL(BAKE,0) * PRICE AS BAKE, NVL(VI,0) * PRICE AS VI, NVL(TNR,0) * PRICE AS TNR, NVL(PK,0) * PRICE AS PK, NVL(HMK4T,0) * PRICE AS HMK4T" + "\n");
                strSqlString.Append("             , NVL(CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(RCV.RCV_QTY/MAT_CMF_13,0)" + "\n");                
                strSqlString.Append("                        ELSE NVL(RCV.RCV_QTY,0)" + "\n");
                strSqlString.Append("                   END,0) * PRICE AS RCV_QTY" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT A.MAT_ID, MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5" + "\n");
                strSqlString.Append("                     , MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, MAT_GRP_9, MAT_GRP_10, MAT_CMF_11" + "\n");
                strSqlString.Append("                     , TO_NUMBER(DECODE(MAT_CMF_13, ' ', 1, '-', 1, MAT_CMF_13)) AS MAT_CMF_13 " + "\n");

                if (rdbSales.Checked == true)
                {
                    strSqlString.Append("                     , PRICE" + "\n");
                    strSqlString.Append("                  FROM MWIPMATDEF A" + "\n");
                    strSqlString.Append("                     , (" + "\n");
                    strSqlString.Append("                        SELECT MAT_ID, MAX(PRICE) AS PRICE" + "\n");
                    strSqlString.Append("                          FROM (" + "\n");
                    strSqlString.Append("                                SELECT MAT_ID, DECODE(MCP_TO_PART, PRODUCT, PRICE, 1) AS PRICE" + "\n");
                    strSqlString.Append("                                  FROM RWIPMCPBOM A" + "\n");
                    strSqlString.Append("                                     , (" + "\n");
                    strSqlString.Append("                                        SELECT PRODUCT, DECODE(CRNC_UNIT, 'USD', PRICE * STD_RATE, PRICE) AS PRICE " + "\n");
                    strSqlString.Append("                                          FROM RPRIMATDAT" + "\n");
                    strSqlString.Append("                                             , (SELECT STD_RATE" + "\n");
                    strSqlString.Append("                                                  FROM " + sExchange + "\n");
                    strSqlString.Append("                                                 WHERE SUBSTR(REPLACE(APPRL_DT, '-', ''), 0, 6) = '" + month + "' " + "\n");
                    strSqlString.Append("                                               )" + "\n");
                    strSqlString.Append("                                         WHERE 1=1 " + "\n");
                    strSqlString.Append("                                           AND SUBSTR(ITEM_CD,10,2) = '0T' " + "\n");
                    strSqlString.Append("                                       ) B " + "\n");
                    strSqlString.Append("                                 WHERE MCP_TO_PART = PRODUCT(+) " + "\n");
                    strSqlString.Append("                                   AND MAT_ID IS NOT NULL " + "\n");
                    strSqlString.Append("                                 UNION " + "\n");
                    strSqlString.Append("                                SELECT PRODUCT, DECODE(CRNC_UNIT, 'USD', PRICE * STD_RATE, PRICE) AS PRICE " + "\n");
                    strSqlString.Append("                                  FROM RPRIMATDAT " + "\n");
                    strSqlString.Append("                                     , (SELECT STD_RATE" + "\n");
                    strSqlString.Append("                                          FROM " + sExchange + "\n");
                    strSqlString.Append("                                         WHERE SUBSTR(REPLACE(APPRL_DT, '-', ''), 0, 6) = '" + month + "' " + "\n");
                    strSqlString.Append("                                       )" + "\n");
                    strSqlString.Append("                                 WHERE 1=1 " + "\n");
                    strSqlString.Append("                                   AND SUBSTR(ITEM_CD,10,2) = '0T' " + "\n");
                    strSqlString.Append("                               )" + "\n");
                    strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("                       ) B" + "\n");
                    strSqlString.Append("                 WHERE 1=1" + "\n");
                    strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
                }
                else
                {
                    strSqlString.Append("                     , 1 AS PRICE" + "\n");
                    strSqlString.Append("                  FROM MWIPMATDEF A" + "\n");
                    strSqlString.Append("                 WHERE 1=1" + "\n");
                }

                strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND A.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                   AND A.MAT_TYPE = 'FG' " + "\n");
                strSqlString.Append("               ) MAT" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID, SUM(PLAN_QTY_TEST) AS ORI_PLN, SUM(TO_NUMBER(DECODE(RESV_FIELD2,' ',0,RESV_FIELD2))) AS REV_PLN" + "\n");
                strSqlString.Append("                  FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND PLAN_MONTH = '" + month + "'" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID " + "\n");                
                strSqlString.Append("               ) PLN" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append("                     , SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1) AS SHP_TTL" + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE,'" + end_date + "', S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1, 0)) AS SHP_TODAY" + "\n");
                strSqlString.Append("                  FROM RSUMFACMOV" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + start_date + "' AND '" + end_date + "' " + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                   AND CM_KEY_1 = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND CM_KEY_2 = 'PROD'" + "\n");                
                strSqlString.Append("                   AND FACTORY NOT IN ('RETURN')" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
                strSqlString.Append("               ) SHP" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID, SUM(RCV_QTY_1) AS RCV_QTY" + "\n");
                strSqlString.Append("                  FROM VSUMWIPRCV" + "\n");
                strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND WORK_DATE = '" + end_date + "'" + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");                
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) RCV" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append("                     , SUM(QTY_1) AS WIP_TTL" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'TEST', QTY_1, 0)) AS TEST " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'QA1', QTY_1, 0)) AS QA1" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'CAS', QTY_1, 0)) AS CAS" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'OS', QTY_1, 0)) AS OS " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'QA2', QTY_1, 0)) AS QA2" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'BAKE', QTY_1, 0)) AS BAKE" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'V/I', QTY_1, 0)) AS VI" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'TnR', QTY_1, 0)) AS TNR" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'P/K', QTY_1, 0)) AS PK" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'HMK4T', QTY_1, 0)) AS HMK4T" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_CMF_3, 'HMK3', QTY_1, 0)) AS HMK3" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_CMF_3, 'FINISH', QTY_1, 0)) AS FINISH" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_CMF_3, 'MOLD', QTY_1, 0)) AS MOLD" + "\n");                            
                strSqlString.Append("                     , SUM(CASE WHEN OPER_CMF_3 = 'BOND' AND OPER <> 'A0000' THEN QTY_1 ELSE 0 END) AS FRONT" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER, 'A0000', QTY_1, 0)) AS HMK2 " + "\n");
                strSqlString.Append("                  FROM (" + "\n");
                strSqlString.Append("                        SELECT A.MAT_ID, B.OPER, B.OPER_GRP_1, B.OPER_CMF_3" + "\n");
                strSqlString.Append("                             , CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(QTY_1/TO_NUMBER(DECODE(MAT_CMF_13, ' ', 1, '-', 1, MAT_CMF_13)),0)" + "\n");                
                strSqlString.Append("                                    ELSE QTY_1" + "\n");
                strSqlString.Append("                               END AS QTY_1" + "\n");                

                if (DateTime.Now.ToString("yyyyMMdd") == end_date)
                {
                    strSqlString.Append("                          FROM RWIPLOTSTS A  " + "\n");
                    strSqlString.Append("                             , MWIPOPRDEF B  " + "\n");
                    strSqlString.Append("                             , MWIPMATDEF C" + "\n");
                    strSqlString.Append("                         WHERE 1 = 1 " + "\n");
                }
                else
                {
                    strSqlString.Append("                          FROM RWIPLOTSTS_BOH A  " + "\n");
                    strSqlString.Append("                             , MWIPOPRDEF B  " + "\n");
                    strSqlString.Append("                             , MWIPMATDEF C" + "\n");
                    strSqlString.Append("                         WHERE A.CUTOFF_DT = '" + end_date + "22' " + "\n");
                }
                
                strSqlString.Append("                           AND A.FACTORY = B.FACTORY" + "\n");
                strSqlString.Append("                           AND A.FACTORY = C.FACTORY" + "\n");
                strSqlString.Append("                           AND A.OPER = B.OPER" + "\n");
                strSqlString.Append("                           AND A.MAT_ID = C.MAT_ID" + "\n");
                strSqlString.Append("                           AND A.FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
                strSqlString.Append("                           AND A.LOT_TYPE = 'W' " + "\n");
                //strSqlString.Append("                           AND A.LOT_CMF_5 LIKE 'P%' " + "\n");
                strSqlString.Append("                           AND A.LOT_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("                           AND C.MAT_GRP_2 <> '-'" + "\n");
                strSqlString.Append("                           AND C.DELETE_FLAG = ' ' " + "\n");                
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) WIP" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = RCV.MAT_ID(+)" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");                

                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                {
                    strSqlString.AppendFormat("           AND MAT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
                }

                #region 상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                #endregion

                strSqlString.Append("           AND NVL(ORI_PLN,0)+NVL(REV_PLN,0)+NVL(SHP_TTL,0)+NVL(RCV_QTY,0)+NVL(WIP_TTL,0) <> 0" + "\n");
                strSqlString.Append("       ) A" + "\n");
                strSqlString.Append(" GROUP BY " + QueryCond1 + "\n");
                strSqlString.Append(" ORDER BY DECODE(CUSTOMER, 'SEC', 1, 'HYNIX', 2, 'iML', 3, 'FCI', 4, 'IMAGIS', 5,6), " + QueryCond4NotNull + "\n");
            }
            #endregion

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }


        // Bump
        private string MakeSqlStringBump()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1;
            string QueryCond2;
            string QueryCond3;
            string QueryCond4;
            string QueryCond1NotNull;
            string QueryCond2NotNull;
            string QueryCond3NotNull;
            string QueryCond4NotNull;
            string start_date;
            string end_date;
            string yesterday;            
            string month;            
            string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값

   
            // kpcs 선택에 의한 분모 값 저장 한다.
            if (ckbKpcs.Checked == true)
            {
                if (rdbQuantity.Checked == true )
                {
                    sKpcsValue = "1000";
                }
                else
                {
                    sKpcsValue = "1000000";                    
                }
            }
            else
            {
                sKpcsValue = "1";
            }

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            QueryCond1NotNull = tableForm.SelectedValueToQuery;
            QueryCond2NotNull = tableForm.SelectedValue2ToQuery;
            QueryCond3NotNull = tableForm.SelectedValue3ToQuery;
            QueryCond4NotNull = tableForm.SelectedValue4ToQuery;
            
            DateTime Select_date;
            Select_date = DateTime.Parse(cdvDate.Text.ToString());

            month = Select_date.ToString("yyyyMM");            
            end_date = Select_date.ToString("yyyyMMdd");
            yesterday = Select_date.AddDays(-1).ToString("yyyyMMdd");

            // 조회월과 조회주차의 시작일이 같은 달이면 시작은 조회월의 1일자로 하고, 다르면(주차시작일이 작으면) 주차 시작일을 시작일로 한다.
            if (month == FindWeek_SOP_A.StartDay_ThisWeek.Substring(0, 6))
            {
                start_date = month + "01";
            }
            else
            {
                start_date = FindWeek_SOP_A.StartDay_ThisWeek;
            }

            #region BUMP
            if (cdvFactory.Text == "HMKB1")
            {
                strSqlString.Append("SELECT " + QueryCond2 + "\n");

                strSqlString.Append("     , ROUND(SUM(ORI_PLN) / " + sKpcsValue + ", 0) AS ORI_PLN" + "\n");
                strSqlString.Append("     , ROUND(SUM(REV_PLN) / " + sKpcsValue + ", 0) AS REV_PLN" + "\n");
                strSqlString.Append("     , ROUND(SUM(SHP_TTL) / " + sKpcsValue + ", 0) AS SHP_TTL" + "\n");
                strSqlString.Append("     , ROUND(DECODE(SUM(ORI_PLN), 0, 0, SUM(SHP_TTL) / SUM(ORI_PLN) * 100), 1) AS JINDO_1" + "\n");
                strSqlString.Append("     , ROUND(DECODE(SUM(REV_PLN), 0, 0, SUM(SHP_TTL) / SUM(REV_PLN) * 100), 1) AS JINDO_2" + "\n");
                strSqlString.Append("     , ROUND(SUM(D1_PLAN) / " + sKpcsValue + ", 0) AS D1_PLAN" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(D0_PLAN,0) - NVL(SHP_WEEK,0)) / " + sKpcsValue + ", 0) AS D0_PLAN" + "\n");
                strSqlString.Append("     , ROUND(SUM(SHP_TODAY) / " + sKpcsValue + ", 0) AS SHP_TODAY" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(D0_PLAN,0) - NVL(SHP_WEEK,0) - NVL(SHP_TODAY,0)) / " + sKpcsValue + ", 0) AS D0_DEF" + "\n");
                    
                #region 기본
                //if (cdvType.Text == "기본")
                if (cdvType.SelectedIndex == 0)
                {
                    strSqlString.Append("     , ROUND(SUM(HMK3B) / " + sKpcsValue + ", 0) AS HMK3B" + "\n");
                    strSqlString.Append("     , ROUND(SUM(PACKING) / " + sKpcsValue + ", 0) AS PACKING" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OGI) / " + sKpcsValue + ", 0) AS OQC" + "\n");
                    strSqlString.Append("     , ROUND(SUM(SORT + AVI) / " + sKpcsValue + ", 0) AS AVI" + "\n");
                    strSqlString.Append("     , ROUND(SUM(FINAL_INSP) / " + sKpcsValue + ", 0) AS FINAL_INSP" + "\n");
                    strSqlString.Append("     , ROUND(SUM(BUMP_SPUTTER + BUMP_PHOTO + BUMP_CU_PLAT + BUMP_SNAG_PLAT + BUMP_ETCH + BUMP_BALL_MOUNT + BUMP_REFLOW) / " + sKpcsValue + ", 0) AS Bump" + "\n");
                    strSqlString.Append("     , ROUND(SUM(PSV3_PHOTO) / " + sKpcsValue + ", 0) AS PSV_3" + "\n");
                    strSqlString.Append("     , ROUND(SUM(RDL3_SPUTTER + RDL3_PHOTO + RDL3_PLAT + RDL3_ETCH) / " + sKpcsValue + ", 0) AS RDL_3" + "\n");
                    strSqlString.Append("     , ROUND(SUM(PSV2_PHOTO) / " + sKpcsValue + ", 0) AS PSV_2" + "\n");
                    strSqlString.Append("     , ROUND(SUM(RDL2_SPUTTER + RDL2_PHOTO + RDL2_PLAT + RDL2_ETCH) / " + sKpcsValue + ", 0) AS RDL_2" + "\n");
                    strSqlString.Append("     , ROUND(SUM(PSV1_PHOTO) / " + sKpcsValue + ", 0) AS PSV_1" + "\n");
                    strSqlString.Append("     , ROUND(SUM(RDL1_SPUTTER + RDL1_PHOTO + RDL1_PLAT + RDL1_ETCH) / " + sKpcsValue + ", 0) AS RDL_1" + "\n");
                    strSqlString.Append("     , ROUND(SUM(RCF_PHOTO) / " + sKpcsValue + ", 0) AS RCF" + "\n");
                    strSqlString.Append("     , ROUND(SUM(HMK2B + IQC + I_STOCK) / " + sKpcsValue + ", 0) AS INCOMING" + "\n");
                }
                #endregion

                #region Detail
                //else if (cdvType.Text == "Detail")
                else if (cdvType.SelectedIndex == 2)
                {
                    strSqlString.Append("     , ROUND(SUM(HMK3B) / " + sKpcsValue + ", 0) AS HMK3B" + "\n");
                    strSqlString.Append("     , ROUND(SUM(PACKING) / " + sKpcsValue + ", 0) AS PACKING" + "\n");
                    strSqlString.Append("     , ROUND(SUM(OGI) / " + sKpcsValue + ", 0) AS OGI" + "\n");
                    strSqlString.Append("     , ROUND(SUM(AVI) / " + sKpcsValue + ", 0) AS AVI" + "\n");
                    strSqlString.Append("     , ROUND(SUM(SORT) / " + sKpcsValue + ", 0) AS SORT" + "\n");
                    strSqlString.Append("     , ROUND(SUM(FINAL_INSP) / " + sKpcsValue + ", 0) AS FINAL_INSP" + "\n");
                    strSqlString.Append("     , ROUND(SUM(BUMP_REFLOW) / " + sKpcsValue + ", 0) AS BUMP_REFLOW" + "\n");
                    strSqlString.Append("     , ROUND(SUM(BUMP_BALL_MOUNT) / " + sKpcsValue + ", 0) AS BUMP_BALL_MOUNT" + "\n");
                    strSqlString.Append("     , ROUND(SUM(BUMP_ETCH) / " + sKpcsValue + ", 0) AS BUMP_ETCH" + "\n");
                    strSqlString.Append("     , ROUND(SUM(BUMP_SNAG_PLAT) / " + sKpcsValue + ", 0) AS BUMP_SNAG_PLAT" + "\n");
                    strSqlString.Append("     , ROUND(SUM(BUMP_CU_PLAT) / " + sKpcsValue + ", 0) AS BUMP_CU_PLAT" + "\n");
                    strSqlString.Append("     , ROUND(SUM(BUMP_PHOTO) / " + sKpcsValue + ", 0) AS BUMP_PHOTO" + "\n");
                    strSqlString.Append("     , ROUND(SUM(BUMP_SPUTTER) / " + sKpcsValue + ", 0) AS BUMP_SPUTTER" + "\n");
                    strSqlString.Append("     , ROUND(SUM(PSV3_PHOTO) / " + sKpcsValue + ", 0) AS PSV3_PHOTO" + "\n");
                    strSqlString.Append("     , ROUND(SUM(RDL3_ETCH) / " + sKpcsValue + ", 0) AS RDL3_ETCH" + "\n");
                    strSqlString.Append("     , ROUND(SUM(RDL3_PLAT) / " + sKpcsValue + ", 0) AS RDL3_PLAT" + "\n");
                    strSqlString.Append("     , ROUND(SUM(RDL3_PHOTO) / " + sKpcsValue + ", 0) AS RDL3_PHOTO" + "\n");
                    strSqlString.Append("     , ROUND(SUM(RDL3_SPUTTER) / " + sKpcsValue + ", 0) AS RDL3_SPUTTER" + "\n");
                    strSqlString.Append("     , ROUND(SUM(PSV2_PHOTO) / " + sKpcsValue + ", 0) AS PSV2_PHOTO" + "\n");
                    strSqlString.Append("     , ROUND(SUM(RDL2_ETCH) / " + sKpcsValue + ", 0) AS RDL2_ETCH" + "\n");
                    strSqlString.Append("     , ROUND(SUM(RDL2_PLAT) / " + sKpcsValue + ", 0) AS RDL2_PLAT" + "\n");
                    strSqlString.Append("     , ROUND(SUM(RDL2_PHOTO) / " + sKpcsValue + ", 0) AS RDL2_PHOTO" + "\n");
                    strSqlString.Append("     , ROUND(SUM(RDL2_SPUTTER) / " + sKpcsValue + ", 0) AS RDL2_SPUTTER" + "\n");
                    strSqlString.Append("     , ROUND(SUM(PSV1_PHOTO) / " + sKpcsValue + ", 0) AS PSV1_PHOTO" + "\n");
                    strSqlString.Append("     , ROUND(SUM(RDL1_ETCH) / " + sKpcsValue + ", 0) AS RDL1_ETCH" + "\n");
                    strSqlString.Append("     , ROUND(SUM(RDL1_PLAT) / " + sKpcsValue + ", 0) AS RDL1_PLAT" + "\n");
                    strSqlString.Append("     , ROUND(SUM(RDL1_PHOTO) / " + sKpcsValue + ", 0) AS RDL1_PHOTO" + "\n");
                    strSqlString.Append("     , ROUND(SUM(RDL1_SPUTTER) / " + sKpcsValue + ", 0) AS RDL1_SPUTTER" + "\n");
                    strSqlString.Append("     , ROUND(SUM(RCF_PHOTO) / " + sKpcsValue + ", 0) AS RCF_PHOTO" + "\n");
                    strSqlString.Append("     , ROUND(SUM(I_STOCK) / " + sKpcsValue + ", 0) AS I_STOCK" + "\n");
                    strSqlString.Append("     , ROUND(SUM(IQC) / " + sKpcsValue + ", 0) AS IQC" + "\n");
                    strSqlString.Append("     , ROUND(SUM(HMK2B) / " + sKpcsValue + ", 0) AS HMK2B" + "\n");
                }
                #endregion

                strSqlString.Append("     , ROUND(SUM(RCV_QTY) / " + sKpcsValue + ", 0) AS RCV_QTY" + "\n");

                strSqlString.Append("     , ROUND(SUM(OUT_PK) / " + sKpcsValue + ", 0) AS OUT_PK" + "\n");
                strSqlString.Append("     , ROUND(SUM(OUT_AVI) / " + sKpcsValue + ", 0) AS OUT_AVI" + "\n");
                strSqlString.Append("     , ROUND(SUM(OUT_FINAL_INSP) / " + sKpcsValue + ", 0) AS OUT_FINAL_INSP" + "\n");
                strSqlString.Append("     , ROUND(SUM(OUT_BUMP) / " + sKpcsValue + ", 0) AS OUT_BUMP" + "\n");
                strSqlString.Append("     , ROUND(SUM(OUT_PSV3) / " + sKpcsValue + ", 0) AS OUT_PSV3" + "\n");
                strSqlString.Append("     , ROUND(SUM(OUT_RDL3) / " + sKpcsValue + ", 0) AS OUT_RDL3" + "\n");
                strSqlString.Append("     , ROUND(SUM(OUT_PSV2) / " + sKpcsValue + ", 0) AS OUT_PSV2" + "\n");
                strSqlString.Append("     , ROUND(SUM(OUT_RDL2) / " + sKpcsValue + ", 0) AS OUT_RDL2" + "\n");
                strSqlString.Append("     , ROUND(SUM(OUT_PSV1) / " + sKpcsValue + ", 0) AS OUT_PSV1" + "\n");
                strSqlString.Append("     , ROUND(SUM(OUT_RDL1) / " + sKpcsValue + ", 0) AS OUT_RDL1" + "\n");
                strSqlString.Append("     , ROUND(SUM(OUT_RCF) / " + sKpcsValue + ", 0) AS OUT_RCF" + "\n");

                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT MAT.*" + "\n");

                strSqlString.Append("             , NVL(PLN.ORI_PLN, 0) * PRICE AS ORI_PLN" + "\n");
                strSqlString.Append("             , NVL(PLN.REV_PLN, 0) * PRICE AS REV_PLN" + "\n");
                strSqlString.Append("             , NVL(SHP.SHP_TTL, 0) * PRICE AS SHP_TTL" + "\n");
                strSqlString.Append("             , NVL(PLN.D0_PLAN, 0) * PRICE AS D0_PLAN" + "\n");
                strSqlString.Append("             , NVL(PLN.D1_PLAN, 0) * PRICE AS D1_PLAN" + "\n");
                strSqlString.Append("             , NVL(SHP.SHP_TODAY, 0) * PRICE AS SHP_TODAY " + "\n");
                strSqlString.Append("             , NVL(SHP.SHP_WEEK, 0) * PRICE AS SHP_WEEK " + "\n");

                strSqlString.Append("             , NVL(HMK2B,0) * PRICE AS HMK2B" + "\n");
                strSqlString.Append("             , NVL(IQC,0) * PRICE AS IQC" + "\n");
                strSqlString.Append("             , NVL(I_STOCK,0) * PRICE AS I_STOCK" + "\n");
                strSqlString.Append("             , NVL(RCF_PHOTO,0) * PRICE AS RCF_PHOTO" + "\n");
                strSqlString.Append("             , NVL(RDL1_SPUTTER,0) * PRICE AS RDL1_SPUTTER" + "\n");
                strSqlString.Append("             , NVL(RDL1_PHOTO,0) * PRICE AS RDL1_PHOTO" + "\n");
                strSqlString.Append("             , NVL(RDL1_PLAT,0) * PRICE AS RDL1_PLAT" + "\n");
                strSqlString.Append("             , NVL(RDL1_ETCH,0) * PRICE AS RDL1_ETCH" + "\n");
                strSqlString.Append("             , NVL(PSV1_PHOTO,0) * PRICE AS PSV1_PHOTO" + "\n");
                strSqlString.Append("             , NVL(RDL2_SPUTTER,0) * PRICE AS RDL2_SPUTTER" + "\n");
                strSqlString.Append("             , NVL(RDL2_PHOTO,0) * PRICE AS RDL2_PHOTO" + "\n");
                strSqlString.Append("             , NVL(RDL2_PLAT,0) * PRICE AS RDL2_PLAT" + "\n");
                strSqlString.Append("             , NVL(RDL2_ETCH,0) * PRICE AS RDL2_ETCH" + "\n");
                strSqlString.Append("             , NVL(PSV2_PHOTO,0) * PRICE AS PSV2_PHOTO" + "\n");
                strSqlString.Append("             , NVL(RDL3_SPUTTER,0) * PRICE AS RDL3_SPUTTER" + "\n");
                strSqlString.Append("             , NVL(RDL3_PHOTO,0) * PRICE AS RDL3_PHOTO" + "\n");
                strSqlString.Append("             , NVL(RDL3_PLAT,0) * PRICE AS RDL3_PLAT" + "\n");
                strSqlString.Append("             , NVL(RDL3_ETCH,0) * PRICE AS RDL3_ETCH" + "\n");
                strSqlString.Append("             , NVL(PSV3_PHOTO,0) * PRICE AS PSV3_PHOTO" + "\n");
                strSqlString.Append("             , NVL(BUMP_SPUTTER,0) * PRICE AS BUMP_SPUTTER" + "\n");
                strSqlString.Append("             , NVL(BUMP_PHOTO,0) * PRICE AS BUMP_PHOTO" + "\n");
                strSqlString.Append("             , NVL(BUMP_CU_PLAT,0) * PRICE AS BUMP_CU_PLAT" + "\n");
                strSqlString.Append("             , NVL(BUMP_SNAG_PLAT,0) * PRICE AS BUMP_SNAG_PLAT" + "\n");
                strSqlString.Append("             , NVL(BUMP_ETCH,0) * PRICE AS BUMP_ETCH" + "\n");
                strSqlString.Append("             , NVL(BUMP_BALL_MOUNT,0) * PRICE AS BUMP_BALL_MOUNT" + "\n");
                strSqlString.Append("             , NVL(BUMP_REFLOW,0) * PRICE AS BUMP_REFLOW" + "\n");
                strSqlString.Append("             , NVL(FINAL_INSP,0) * PRICE AS FINAL_INSP" + "\n");
                strSqlString.Append("             , NVL(SORT,0) * PRICE AS SORT" + "\n");
                strSqlString.Append("             , NVL(AVI,0) * PRICE AS AVI" + "\n");
                strSqlString.Append("             , NVL(OGI,0) * PRICE AS OGI" + "\n");
                strSqlString.Append("             , NVL(PACKING,0) * PRICE AS PACKING" + "\n");
                strSqlString.Append("             , NVL(HMK3B,0) * PRICE AS HMK3B" + "\n");

                strSqlString.Append("             , NVL(RCV.RCV_QTY,0) * PRICE AS RCV_QTY" + "\n");

                strSqlString.Append("             , NVL(MOV.OUT_PK,0) * PRICE AS OUT_PK" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_AVI,0) * PRICE AS OUT_AVI" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_FINAL_INSP,0) * PRICE AS OUT_FINAL_INSP" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_BUMP,0) * PRICE AS OUT_BUMP" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_PSV3,0) * PRICE AS OUT_PSV3" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_RDL3,0) * PRICE AS OUT_RDL3" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_PSV2,0) * PRICE AS OUT_PSV2" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_RDL2,0) * PRICE AS OUT_RDL2" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_PSV1,0) * PRICE AS OUT_PSV1" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_RDL1,0) * PRICE AS OUT_RDL1" + "\n");
                strSqlString.Append("             , NVL(MOV.OUT_RCF,0) * PRICE AS OUT_RCF" + "\n");

                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT A.MAT_ID, MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5" + "\n");
                strSqlString.Append("                     , MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, MAT_GRP_9, MAT_GRP_10, MAT_CMF_11, MAT_CMF_14" + "\n");

                if (rdbSales.Checked == true)
                {
                    strSqlString.Append("                     , NVL(PRICE,0) AS PRICE" + "\n");
                    strSqlString.Append("                  FROM VWIPMATDEF A" + "\n");                                                         
                    strSqlString.Append("                     , (" + "\n");
                    strSqlString.Append("                        SELECT MAT_ID, MAX(PRICE) AS PRICE" + "\n");
                    strSqlString.Append("                          FROM (" + "\n");

                    strSqlString.Append("                                SELECT B.MAT_ID, A.PRODUCT, DECODE(A.CRNC_UNIT, 'USD', A.PRICE * C.STD_RATE, A.PRICE) AS PRICE " + "\n");
                    strSqlString.Append("                                  FROM RPRIMATDAT A " + "\n");                                           // ?????
                    strSqlString.Append("                                     , MWIPMATDEF B " + "\n");
                    strSqlString.Append("                                     , (SELECT STD_RATE" + "\n");
                    strSqlString.Append("                                          FROM RDAYECRDAT" + "\n");
                    strSqlString.Append("                                         WHERE SUBSTR(REPLACE(APPRL_DT, '-', ''), 0, 6) = '" + month + "' " + "\n");
                    strSqlString.Append("                                       ) C " + "\n");
                    strSqlString.Append("                                 WHERE 1=1 " + "\n");
                    strSqlString.Append("                                   AND A.PRODUCT = B.MAT_ID " + "\n");
                    //- A1 : 'A0' / B1 : '' : 아직 결정 된것이 없음 !!!!! ?????
                    strSqlString.Append("                                   AND SUBSTR(A.ITEM_CD,10,2) = 'A0' " + "\n");                          // ?????
                    strSqlString.Append("                                   AND NVL(A.PRICE,0) > 0 " + "\n");
                    strSqlString.Append("                                   AND B.FACTORY = '" + cdvFactory.Text + "' " + "\n");
                    strSqlString.Append("                                   AND B.DELETE_FLAG = ' ' " + "\n");
                    //-
                    strSqlString.Append("                               )" + "\n");
                    strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("                       ) B" + "\n");

                    strSqlString.Append("                 WHERE 1=1" + "\n");
                    strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
                }
                else
                {
                    strSqlString.Append("                     , 1 AS PRICE" + "\n");
                    strSqlString.Append("                  FROM VWIPMATDEF A" + "\n");                                                          // ?????
                    strSqlString.Append("                 WHERE 1=1" + "\n");
                }

                strSqlString.Append("                   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                   AND A.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                   AND A.MAT_TYPE = 'FG' " + "\n");                                                        // FG : 일반 생산 제품 / FG 외 : 원부자재
                strSqlString.Append("               ) MAT" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID " + "\n");
                strSqlString.Append("                     , SUM(ORI_PLN) AS ORI_PLN " + "\n");
                strSqlString.Append("                     , SUM(REV_PLN) AS REV_PLN " + "\n");
                strSqlString.Append("                     , SUM(D0_PLAN) AS D0_PLAN " + "\n");
                strSqlString.Append("                     , SUM(D1_PLAN) AS D1_PLAN " + "\n");
                strSqlString.Append("                  FROM ( " + "\n");
                strSqlString.Append("                        SELECT MAT_ID, SUM(PLAN_QTY_BUMP) AS ORI_PLN, SUM(RESV_FIELD8) AS REV_PLN, 0 AS D0_PLAN, 0 AS D1_PLAN" + "\n");
                strSqlString.Append("                          FROM (" + "\n");
                strSqlString.Append("                                SELECT MAT_ID, SUM(TO_NUMBER(DECODE(RESV_FIELD7,' ',0,RESV_FIELD7))) AS PLAN_QTY_BUMP, SUM(TO_NUMBER(DECODE(RESV_FIELD8,' ',0,RESV_FIELD8))) AS RESV_FIELD8" + "\n");
                strSqlString.Append("                                  FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                                   AND PLAN_MONTH = '" + month + "'" + "\n");
                strSqlString.Append("                                   AND (TO_NUMBER(DECODE(RESV_FIELD7,' ',0,RESV_FIELD7)) > 0 OR TO_NUMBER(DECODE(RESV_FIELD8,' ',0,RESV_FIELD8)) > 0)" + "\n");
                strSqlString.Append("                                 GROUP BY MAT_ID " + "\n");
                strSqlString.Append("                               ) " + "\n");
                strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
                strSqlString.Append("                        HAVING SUM(PLAN_QTY_BUMP + RESV_FIELD8) > 0" + "\n");
                strSqlString.Append("                         UNION ALL" + "\n");
                strSqlString.Append("                        SELECT MAT_ID, 0, 0" + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 7 THEN D0_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 1 THEN D0_QTY + D1_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 2 THEN D0_QTY + D1_QTY + D2_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 3 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 4 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 5 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 6 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY + D6_QTY " + "\n");
                strSqlString.Append("                                        ELSE 0 " + "\n");
                strSqlString.Append("                                   END) AS D0_PLAN " + "\n");
                strSqlString.Append("                             , SUM(CASE WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 7 THEN D1_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 1 THEN D2_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 2 THEN D3_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 3 THEN D4_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 4 THEN D5_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 5 THEN D6_QTY " + "\n");
                strSqlString.Append("                                        WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 6 THEN D7_QTY " + "\n");
                strSqlString.Append("                                        ELSE 0 " + "\n");
                strSqlString.Append("                                   END) AS D1_PLAN " + "\n");
                strSqlString.Append("                          FROM (" + "\n");
                strSqlString.Append("                                SELECT FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D0_QTY, 0)) AS D0_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D1_QTY, 0)) AS D1_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D2_QTY, 0)) AS D2_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D3_QTY, 0)) AS D3_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D4_QTY, 0)) AS D4_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D5_QTY, 0)) AS D5_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D6_QTY, 0)) AS D6_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D0_QTY, 0)) AS D7_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D1_QTY, 0)) AS D8_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D2_QTY, 0)) AS D9_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D3_QTY, 0)) AS D10_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D4_QTY, 0)) AS D11_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D5_QTY, 0)) AS D12_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D6_QTY, 0)) AS D13_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', WW_QTY, 0)) AS W1_QTY  " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', WW_QTY, 0)) AS W2_QTY  " + "\n");
                strSqlString.Append("                                  FROM RWIPPLNWEK  " + "\n");
                strSqlString.Append("                                 WHERE 1=1  " + "\n");
                strSqlString.Append("                                   AND FACTORY = '" + cdvFactory.Text + "'  " + "\n");
                strSqlString.Append("                                   AND GUBUN = '3'  " + "\n");                                                                             // 0 : 영업계획 / 3 : 픽스 계획(생산 계획)
                strSqlString.Append("                                   AND PLAN_WEEK IN ('" + FindWeek_SOP_A.ThisWeek + "','" + FindWeek_SOP_A.NextWeek + "') " + "\n");
                strSqlString.Append("                                 GROUP BY FACTORY, MAT_ID  " + "\n");
                strSqlString.Append("                               )  " + "\n");
                strSqlString.Append("                         GROUP BY MAT_ID  " + "\n");
                strSqlString.Append("                       ) " + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID  " + "\n");
                strSqlString.Append("               ) PLN" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + month + "01' AND '" + end_date + "' THEN SHP_QTY_1 END) AS SHP_TTL " + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + end_date + "', SHP_QTY_1, 0)) AS SHP_TODAY" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + yesterday + "' THEN SHP_QTY_1 END) AS SHP_WEEK " + "\n");
                strSqlString.Append("                  FROM VSUMWIPOUT" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + start_date + "' AND '" + end_date + "' " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");                               // W : 일반 생산 품목
                strSqlString.Append("                   AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                   AND CM_KEY_3 LIKE 'P%'" + "\n");
                strSqlString.Append("                   AND MAT_ID NOT LIKE 'HX%'" + "\n");                         // HX가 아닌 업체는 22시 기준
                strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
                strSqlString.Append("                 UNION ALL " + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + month + "01' AND '" + end_date + "' THEN SHP_QTY_1 END) AS SHP_TTL " + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + end_date + "', SHP_QTY_1, 0)) AS SHP_TODAY" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + yesterday + "' THEN SHP_QTY_1 END) AS SHP_WEEK " + "\n");
                strSqlString.Append("                  FROM VSUMWIPOUT_06" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + start_date + "' AND '" + end_date + "' " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                   AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                   AND CM_KEY_3 LIKE 'P%'" + "\n");
                strSqlString.Append("                   AND MAT_ID LIKE 'HX%'" + "\n");                     // HX는 06시 기준
                strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
                strSqlString.Append("               ) SHP" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID, SUM(RCV_QTY_1) AS RCV_QTY" + "\n");
                strSqlString.Append("                  FROM VSUMWIPRCV" + "\n");
                strSqlString.Append("                 WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");           // B0000공정에서 Create 되면
                strSqlString.Append("                   AND WORK_DATE = '" + end_date + "'" + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                   AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) RCV" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append("                     , SUM(QTY) AS WIP_TTL" + "\n");

                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'HMK2B', QTY, 0)) AS HMK2B" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'IQC', QTY, 0)) AS IQC" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'I-STOCK', QTY, 0)) AS I_STOCK" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'RCF_PHOTO', QTY, 0)) AS RCF_PHOTO" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'RDL1_SPUTTER', QTY, 0)) AS RDL1_SPUTTER" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'RDL1_PHOTO', QTY, 0)) AS RDL1_PHOTO" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'RDL1_PLAT', QTY, 0)) AS RDL1_PLAT" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'RDL1_ETCH', QTY, 0)) AS RDL1_ETCH" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'PSV1_PHOTO', QTY, 0)) AS PSV1_PHOTO" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'RDL2_SPUTTER', QTY, 0)) AS RDL2_SPUTTER" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'RDL2_PHOTO', QTY, 0)) AS RDL2_PHOTO" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'RDL2_PLAT', QTY, 0)) AS RDL2_PLAT" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'RDL2_ETCH', QTY, 0)) AS RDL2_ETCH" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'PSV2_PHOTO', QTY, 0)) AS PSV2_PHOTO" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'RDL3_SPUTTER', QTY, 0)) AS RDL3_SPUTTER" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'RDL3_PHOTO', QTY, 0)) AS RDL3_PHOTO" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'RDL3_PLAT', QTY, 0)) AS RDL3_PLAT" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'RDL3_ETCH', QTY, 0)) AS RDL3_ETCH" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'PSV3_PHOTO', QTY, 0)) AS PSV3_PHOTO" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'BUMP_SPUTTER', QTY, 0)) AS BUMP_SPUTTER" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'BUMP_PHOTO', QTY, 0)) AS BUMP_PHOTO" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'BUMP_CU_PLAT', QTY, 0)) AS BUMP_CU_PLAT" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'BUMP_SNAG_PLAT', QTY, 0)) AS BUMP_SNAG_PLAT" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'BUMP_ETCH', QTY, 0)) AS BUMP_ETCH" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'BUMP_BALL_MOUNT', QTY, 0)) AS BUMP_BALL_MOUNT" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'BUMP_REFLOW', QTY, 0)) AS BUMP_REFLOW" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'FINAL_INSP', QTY, 0)) AS FINAL_INSP" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'SORT', QTY, 0)) AS SORT" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'AVI', QTY, 0)) AS AVI" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'OGI', QTY, 0)) AS OGI" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'PACKING', QTY, 0)) AS PACKING" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'HMK3B', QTY, 0)) AS HMK3B" + "\n");

                strSqlString.Append("                  FROM (" + "\n");
                strSqlString.Append("                        SELECT MAT_ID, OPER, OPER_GRP_1, MAT_GRP_1, MAT_GRP_4, MAT_GRP_5" + "\n");
                strSqlString.Append("                             , QTY_1 AS QTY " + "\n");
                strSqlString.Append("                          FROM (" + "\n");
                strSqlString.Append("                                SELECT A.MAT_ID, B.OPER, B.OPER_GRP_1, C.MAT_GRP_1, C.MAT_GRP_4, C.MAT_GRP_5" + "\n");
                strSqlString.Append("                                     , QTY_1 AS QTY_1" + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == end_date)
                {
                    strSqlString.Append("                                  FROM RWIPLOTSTS A  " + "\n");
                    strSqlString.Append("                                     , MWIPOPRDEF B  " + "\n");
                    strSqlString.Append("                                     , VWIPMATDEF C" + "\n");
                    strSqlString.Append("                                 WHERE 1 = 1 " + "\n");
                }
                else
                {
                    strSqlString.Append("                                  FROM RWIPLOTSTS_BOH A  " + "\n"); 
                    strSqlString.Append("                                     , MWIPOPRDEF B  " + "\n");
                    strSqlString.Append("                                     , VWIPMATDEF C" + "\n");  
                    strSqlString.Append("                                 WHERE A.CUTOFF_DT = '" + end_date + "22' " + "\n");
                }

                strSqlString.Append("                                   AND A.FACTORY = B.FACTORY" + "\n");
                strSqlString.Append("                                   AND A.FACTORY = C.FACTORY" + "\n");
                strSqlString.Append("                                   AND A.OPER = B.OPER" + "\n");
                strSqlString.Append("                                   AND A.MAT_ID = C.MAT_ID" + "\n");
                strSqlString.Append("                                   AND A.FACTORY = '" + cdvFactory.Text + "'  " + "\n");
                strSqlString.Append("                                   AND A.LOT_TYPE = 'W' " + "\n");       
                strSqlString.Append("                                   AND A.LOT_CMF_5 LIKE 'P%' " + "\n");  
                strSqlString.Append("                                   AND A.LOT_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("                                   AND C.DELETE_FLAG = ' ' " + "\n");
                strSqlString.Append("                               )" + "\n");
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) WIP" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'B9100' THEN QTY ELSE 0 END) AS OUT_PK" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'B7900' THEN QTY ELSE 0 END) AS OUT_AVI" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'B7750' THEN QTY ELSE 0 END) AS OUT_FINAL_INSP" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'B7700' THEN QTY ELSE 0 END) AS OUT_BUMP" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'B6250' THEN QTY ELSE 0 END) AS OUT_PSV3" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'B5800' THEN QTY ELSE 0 END) AS OUT_RDL3" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'B4650' THEN QTY ELSE 0 END) AS OUT_PSV2" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'B4200' THEN QTY ELSE 0 END) AS OUT_RDL2" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'B3050' THEN QTY ELSE 0 END) AS OUT_PSV1" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'B2600' THEN QTY ELSE 0 END) AS OUT_RDL1" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN OPER = 'B1450' THEN QTY ELSE 0 END) AS OUT_RCF" + "\n");
                strSqlString.Append("                  FROM (    " + "\n");
                strSqlString.Append("                        SELECT MAT_ID, OPER, MAT_GRP_1, MAT_GRP_4, MAT_GRP_5" + "\n");
                strSqlString.Append("                             , QTY_1 AS QTY" + "\n");
                strSqlString.Append("                          FROM (" + "\n");
                strSqlString.Append("                                SELECT MAT_ID, OPER, MAT_GRP_1, MAT_GRP_4, MAT_GRP_5, MAT_GRP_9, MAT_CMF_11" + "\n");
                strSqlString.Append("                                     , END_QTY AS QTY_1 " + "\n");
                strSqlString.Append("                                  FROM ( " + "\n");
                strSqlString.Append("                                        SELECT A.OPER" + "\n");
                strSqlString.Append("                                             , CASE WHEN OPER = 'BZ010' THEN S1_MOVE_QTY_1 + S2_MOVE_QTY_1 + S3_MOVE_QTY_1" + "\n");
                strSqlString.Append("                                                    ELSE S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1" + "\n");
                strSqlString.Append("                                               END END_QTY" + "\n");
                strSqlString.Append("                                             , B.*" + "\n");
                strSqlString.Append("                                          FROM RSUMWIPMOV A" + "\n");
                strSqlString.Append("                                             , VWIPMATDEF B" + "\n");
                strSqlString.Append("                                         WHERE 1=1" + "\n");
                strSqlString.Append("                                           AND A.FACTORY = B.FACTORY" + "\n");
                strSqlString.Append("                                           AND A.MAT_ID = B.MAT_ID" + "\n");
                strSqlString.Append("                                           AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                                           AND A.WORK_DATE = '" + end_date + "'" + "\n");
                strSqlString.Append("                                           AND A.LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                                           AND A.CM_KEY_3 LIKE 'P%'   " + "\n");
                strSqlString.Append("                                       ) " + "\n");
                strSqlString.Append("                               )" + "\n");
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) MOV" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = RCV.MAT_ID(+)" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = MOV.MAT_ID(+)" + "\n");

                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                {
                    strSqlString.AppendFormat("           AND MAT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
                }

                #region 상세 조회에 따른 SQL문 생성
                if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                #endregion

                strSqlString.Append("           AND NVL(ORI_PLN,0)+NVL(REV_PLN,0)+NVL(SHP_TTL,0)+NVL(RCV_QTY,0)+NVL(WIP_TTL,0)+NVL(D0_PLAN,0)+NVL(D1_PLAN,0)+NVL(SHP_WEEK,0) <> 0" + "\n");
                strSqlString.Append("       ) A" + "\n");

                strSqlString.Append(" GROUP BY " + QueryCond2 + "\n");
                strSqlString.Append(" ORDER BY DECODE(MAT_GRP_1, 'SEC', 1, 'HYNIX', 2, 'iML', 3, 'FCI', 4, 'IMAGIS', 5,6), " + QueryCond2 + "\n");

            }
            #endregion

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }


        // 2013-06-03-손광섭
        // 공장 == HMK1일때 쿼리
        private string MakeSqlString03()
        {
            StringBuilder strSqlString = new StringBuilder();            
            
            string QueryCond1;
            string QueryCond2;
            string QueryCond3;
            string QueryCond4;
            string QueryCond5;            
            string start_date;
            string yesterday;
            string end_date;
            string month;
            string year;            
            string sKpcsValue;
            string sExchange;

            // kpcs 선택에 의한 분모 값 저장 한다.
            if (ckbKpcs.Checked == true)
            {
                if (rdbQuantity.Checked == true)
                {
                    sKpcsValue = "1000";
                }
                else
                {
                    sKpcsValue = "1000000";
                }
            }
            else
            {
                sKpcsValue = "1";
            }

            if (GlobalVariable.gsGlovalSite == "V2")
            {
                sExchange = "RDAYECRDAT@HANARPT";
            }
            else
            {
                sExchange = "RDAYECRDAT";
            }

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;
            QueryCond5 = tableForm.SelectedValue5ToQueryContainNull;
                                            
            DateTime Select_date;
            Select_date = DateTime.Parse(cdvDate.Text.ToString());

            year = Select_date.ToString("yyyy");
            month = Select_date.ToString("yyyyMM");
            end_date = Select_date.ToString("yyyyMMdd");
            yesterday = Select_date.AddDays(-1).ToString("yyyyMMdd");                                   

            // 조회월과 조회주차의 시작일이 같은 달이면 시작은 조회월의 1일자로 하고, 다르면(주차시작일이 작으면) 주차 시작일을 시작일로 한다.
            if (month == FindWeek_SOP_T.StartDay_ThisWeek.Substring(0, 6))
            {
                start_date = month + "01";
            }
            else
            {
                start_date = FindWeek_SOP_T.StartDay_ThisWeek;
            }

            strSqlString.Append("SELECT " + QueryCond2 + "\n");
            strSqlString.Append("     , ROUND(SUM(ORI_PLAN) / " + sKpcsValue + ", 0) AS ORI_PLAN" + "\n");
            strSqlString.Append("     , ROUND(SUM(MON_PLAN) / " + sKpcsValue + ", 0) AS MON_PLAN" + "\n");
            strSqlString.Append("     , ROUND(SUM(THIS_MON_SHIP) / " + sKpcsValue + ", 0) AS THIS_MON_SHIP" + "\n");
            strSqlString.Append("     , ROUND(DECODE(SUM(ORI_PLAN), 0, 0, SUM(THIS_MON_SHIP) / SUM(ORI_PLAN) * 100), 1) AS JINDO_1" + "\n");
            strSqlString.Append("     , ROUND(DECODE(SUM(MON_PLAN), 0, 0, SUM(THIS_MON_SHIP) / SUM(MON_PLAN) * 100), 1) AS JINDO_2" + "\n");
            strSqlString.Append("     , ROUND(SUM(D1_PLAN) / " + sKpcsValue + ", 0) AS D1_PLAN" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(D0_PLAN,0) - NVL(SHP_WEEK,0)) / " + sKpcsValue + ", 0) AS D0_PLAN" + "\n");            
            strSqlString.Append("     , ROUND(SUM(TO_DAY_SHP) / " + sKpcsValue + ", 0) AS TO_DAY_SHP" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(D0_PLAN,0) - NVL(SHP_WEEK,0) - NVL(TO_DAY_SHP,0)) / " + sKpcsValue + ", 0) AS D0_DEF" + "\n");
            strSqlString.Append("     , ROUND(SUM(HMK4T) / " + sKpcsValue + ", 0) AS HMK4T" + "\n");
            strSqlString.Append("     , ROUND(SUM(TEST_END) / " + sKpcsValue + ", 0) AS TEST_END" + "\n");
            strSqlString.Append("     , ROUND(SUM(TEST) / " + sKpcsValue + ", 0) AS TEST" + "\n");
            strSqlString.Append("     , ROUND(SUM(HMK3T) / " + sKpcsValue + ", 0) AS HMK3T" + "\n");
            strSqlString.Append("     , ROUND(SUM(HMK3A) / " + sKpcsValue + ", 0) AS HMK3A" + "\n");
            strSqlString.Append("     , ROUND(SUM(FINISH) / " + sKpcsValue + ", 0) AS FINISH" + "\n");
            strSqlString.Append("     , ROUND(SUM(MOLD) / " + sKpcsValue + ", 0) AS MOLD" + "\n");
            strSqlString.Append("     , ROUND(SUM(FRONT) / " + sKpcsValue + ", 0) AS FRONT " + "\n");
            strSqlString.Append("     , ROUND(SUM(HMK2) / " + sKpcsValue + ", 0) AS HMK2" + "\n");
            strSqlString.Append("     , ROUND(SUM(TTL) / " + sKpcsValue + ", 0) AS TTL" + "\n");
            strSqlString.Append("     , ROUND(SUM(RCV2) / " + sKpcsValue + ", 0) AS RCV2" + "\n");
            strSqlString.Append("     , ROUND(SUM(OUT_TEST) / " + sKpcsValue + ", 0) AS OUT_TEST" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT " + QueryCond1 + "\n");
            //strSqlString.Append("        SELECT DECODE(A.MAT_GRP_1,'SE','SEC','HX','HYNIX','IM','iML','FC','FCI','IG','IMAGIS' , (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.MAT_GRP_1)) CUSTOMER, A.MAT_GRP_9 MAT_GRP_9, A.MAT_GRP_3 AS MAT_GRP_10,A.MAT_GRP_6, A.MAT_CMF_11" + "\n");

            if (rdbQuantity.Checked == true)
            {
                strSqlString.Append("             , ROUND(SUM(NVL(PLAN.ORI_PLAN ,0)) ) AS ORI_PLAN" + "\n");
                strSqlString.Append("             , ROUND(SUM(NVL(PLAN.MON_PLAN,0)) ) AS MON_PLAN" + "\n");
                strSqlString.Append("             , SUM(NVL(PLAN.D0_PLAN,0)) AS D0_PLAN" + "\n");
                strSqlString.Append("             , SUM(NVL(PLAN.D1_PLAN,0)) AS D1_PLAN" + "\n");
                strSqlString.Append("             , SUM(NVL(PLAN.THIS_MON_SHIP,0))  THIS_MON_SHIP" + "\n");
                strSqlString.Append("             , SUM(NVL(PLAN.TO_DAY_SHP,0))  TO_DAY_SHP" + "\n");
                strSqlString.Append("             , SUM(NVL(PLAN.SHP_WEEK,0)) AS SHP_WEEK" + "\n");
                strSqlString.Append("             , SUM(NVL(TEST_WIP.V12,0)) + SUM(NVL(TEST_WIP.V7+TEST_WIP.V8+TEST_WIP.V9+TEST_WIP.V10+TEST_WIP.V11,0)) + SUM(NVL(TEST_WIP.V1+TEST_WIP.V2+TEST_WIP.V3+TEST_WIP.V4+TEST_WIP.V5+TEST_WIP.V6,0)) + SUM(NVL(TEST_WIP.V0,0)) TTL" + "\n");
                strSqlString.Append("             , SUM(NVL(TEST_WIP.V12,0)) HMK4T" + "\n");
                strSqlString.Append("             , SUM(NVL(TEST_WIP.V7+TEST_WIP.V8+TEST_WIP.V9+TEST_WIP.V10+TEST_WIP.V11,0)) TEST_END" + "\n");
                strSqlString.Append("             , SUM(NVL(TEST_WIP.V1+TEST_WIP.V2+TEST_WIP.V3+TEST_WIP.V4+TEST_WIP.V5+TEST_WIP.V6,0)) TEST" + "\n");
                strSqlString.Append("             , SUM(NVL(TEST_WIP.V0,0)) HMK3T " + "\n");
                strSqlString.Append("             , SUM(NVL(ASSY_WIP.HMK3, 0)) HMK3A " + "\n");
                strSqlString.Append("             , SUM(NVL(ASSY_WIP.FINISH, 0)) FINISH" + "\n");
                strSqlString.Append("             , SUM(NVL(ASSY_WIP.MOLD, 0)) MOLD" + "\n");
                strSqlString.Append("             , SUM(NVL(ASSY_WIP.FRONT, 0)) FRONT" + "\n");
                strSqlString.Append("             , SUM(NVL(ASSY_WIP.HMK2, 0)) HMK2" + "\n");
                strSqlString.Append("             , SUM(NVL(PLAN.WAFER_IN_QTY,0)) RCV2" + "\n");
                strSqlString.Append("             , SUM(NVL(MOV.QTY,0)) OUT_TEST" + "\n");
            }
            else
            {
                strSqlString.Append("             , ROUND(SUM(NVL(PLAN.ORI_PLAN ,0) * NVL( H.PRICE, 0 )) ) AS ORI_PLAN" + "\n");
                strSqlString.Append("             , ROUND(SUM(NVL(PLAN.MON_PLAN,0) * NVL( H.PRICE, 0 ) ) ) AS MON_PLAN" + "\n");
                strSqlString.Append("             , SUM(NVL(PLAN.D0_PLAN,0) * NVL(H.PRICE, 0)) AS D0_PLAN" + "\n");
                strSqlString.Append("             , SUM(NVL(PLAN.D1_PLAN,0) * NVL(H.PRICE, 0)) AS D1_PLAN" + "\n");
                strSqlString.Append("             , SUM(NVL(PLAN.THIS_MON_SHIP,0) * NVL( H.PRICE, 0 ) )  THIS_MON_SHIP" + "\n");
                strSqlString.Append("             , SUM(NVL(PLAN.TO_DAY_SHP,0) * NVL( H.PRICE, 0 ) )  TO_DAY_SHP" + "\n");
                strSqlString.Append("             , SUM(NVL(PLAN.SHP_WEEK,0) * NVL(H.PRICE, 0)) AS SHP_WEEK" + "\n");
                strSqlString.Append("             , SUM(NVL(TEST_WIP.V12,0) * NVL( H.PRICE, 0 ) ) + SUM(NVL(TEST_WIP.V7+TEST_WIP.V8+TEST_WIP.V9+TEST_WIP.V10+TEST_WIP.V11,0) * NVL( H.PRICE, 0 ) ) + SUM(NVL(TEST_WIP.V1+TEST_WIP.V2+TEST_WIP.V3+TEST_WIP.V4+TEST_WIP.V5+TEST_WIP.V6,0) * NVL( H.PRICE, 0 ) ) + SUM(NVL(TEST_WIP.V0,0) * NVL( H.PRICE, 0 ) ) TTL" + "\n");
                strSqlString.Append("             , SUM(NVL(TEST_WIP.V12,0) * NVL( H.PRICE, 0 ) ) HMK4T" + "\n");
                strSqlString.Append("             , SUM(NVL(TEST_WIP.V7+TEST_WIP.V8+TEST_WIP.V9+TEST_WIP.V10+TEST_WIP.V11,0) * NVL( H.PRICE, 0 ) ) TEST_END" + "\n");
                strSqlString.Append("             , SUM(NVL(TEST_WIP.V1+TEST_WIP.V2+TEST_WIP.V3+TEST_WIP.V4+TEST_WIP.V5+TEST_WIP.V6,0) * NVL( H.PRICE, 0 ) ) TEST" + "\n");
                strSqlString.Append("             , SUM(NVL(TEST_WIP.V0,0) * NVL( H.PRICE, 0 ) ) HMK3T" + "\n");
                strSqlString.Append("             , SUM(NVL(ASSY_WIP.HMK3, 0) * NVL( H.PRICE, 0 ) ) HMK3A" + "\n");
                strSqlString.Append("             , SUM(NVL(ASSY_WIP.FINISH, 0) * NVL( H.PRICE, 0 ) ) FINISH" + "\n");
                strSqlString.Append("             , SUM(NVL(ASSY_WIP.MOLD, 0) * NVL( H.PRICE, 0 ) ) MOLD" + "\n");
                strSqlString.Append("             , SUM(NVL(ASSY_WIP.FRONT, 0) * NVL( H.PRICE, 0 ) ) FRONT" + "\n");
                strSqlString.Append("             , SUM(NVL(ASSY_WIP.HMK2, 0) * NVL( H.PRICE, 0 ) ) HMK2" + "\n");
                strSqlString.Append("             , SUM(NVL(PLAN.WAFER_IN_QTY,0) * NVL( H.PRICE, 0 ) ) RCV2" + "\n");
                strSqlString.Append("             , SUM(NVL(MOV.QTY,0) * NVL(H.PRICE, 0)) OUT_TEST" + "\n");
            }

            strSqlString.Append("          FROM MWIPMATDEF A" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8" + "\n");
            strSqlString.Append("                     , DECODE(MAT.MAT_GRP_1,'SE',MAT.MAT_GRP_9,' ') AS MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7, MAT.MAT_CMF_13, MAT.MAT_CMF_11" + "\n");
            strSqlString.Append("                     , CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SUM(PLAN.RESV_FIELD2)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0)" + "\n");
            strSqlString.Append("                            ELSE SUM(PLAN.RESV_FIELD2)" + "\n");
            strSqlString.Append("                       END MON_PLAN" + "\n");
            strSqlString.Append("                     , CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SUM(PLAN.PLAN_QTY_TEST)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1, MAT.MAT_CMF_13)),0)" + "\n");
            strSqlString.Append("                            ELSE SUM(PLAN.PLAN_QTY_TEST)" + "\n");
            strSqlString.Append("                       END ORI_PLAN" + "\n");
            strSqlString.Append("                     , CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SUM(PLAN.D0_PLAN)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1, MAT.MAT_CMF_13)),0)" + "\n");
            strSqlString.Append("                            ELSE SUM(PLAN.D0_PLAN)" + "\n");
            strSqlString.Append("                       END D0_PLAN" + "\n");
            strSqlString.Append("                     , CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SUM(PLAN.D1_PLAN)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1, MAT.MAT_CMF_13)),0)" + "\n");
            strSqlString.Append("                            ELSE SUM(PLAN.D1_PLAN)" + "\n");
            strSqlString.Append("                       END D1_PLAN" + "\n");
            strSqlString.Append("                     , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(SUM(SHIP.THIS_MON_SHIP)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1, MAT.MAT_CMF_13)),0)" + "\n");
            strSqlString.Append("                            ELSE SUM(SHIP.THIS_MON_SHIP)" + "\n");
            strSqlString.Append("                       END THIS_MON_SHIP" + "\n");
            strSqlString.Append("                     , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(SUM(SHIP.TO_DAY_SHP)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1, MAT.MAT_CMF_13)),0)" + "\n");
            strSqlString.Append("                            ELSE SUM(SHIP.TO_DAY_SHP)" + "\n");
            strSqlString.Append("                       END TO_DAY_SHP" + "\n");
            strSqlString.Append("                     , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(SUM(SHIP.SHP_WEEK)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1, MAT.MAT_CMF_13)),0)" + "\n");
            strSqlString.Append("                            ELSE SUM(SHIP.SHP_WEEK)" + "\n");
            strSqlString.Append("                       END SHP_WEEK" + "\n");
            strSqlString.Append("                     , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(SUM(ST_IN.QTY)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1, MAT.MAT_CMF_13)),0)" + "\n");
            strSqlString.Append("                            ELSE SUM(ST_IN.QTY)" + "\n");
            strSqlString.Append("                       END WAFER_IN_QTY " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID " + "\n");
            strSqlString.Append("                             , SUM(PLAN_QTY_TEST) AS PLAN_QTY_TEST " + "\n");
            strSqlString.Append("                             , SUM(RESV_FIELD2) AS RESV_FIELD2 " + "\n");
            strSqlString.Append("                             , SUM(D0_PLAN) AS D0_PLAN " + "\n");
            strSqlString.Append("                             , SUM(D1_PLAN) AS D1_PLAN " + "\n");
            strSqlString.Append("                          FROM ( " + "\n");
            strSqlString.Append("                                SELECT MAT_ID, SUM(PLAN_QTY_TEST) AS PLAN_QTY_TEST, SUM(RESV_FIELD2) AS RESV_FIELD2, 0 AS D0_PLAN, 0 AS D1_PLAN" + "\n");
            strSqlString.Append("                                  FROM (" + "\n");
            strSqlString.Append("                                        SELECT MAT_ID, SUM(PLAN_QTY_TEST) AS PLAN_QTY_TEST, SUM(TO_NUMBER(DECODE(RESV_FIELD2,' ',0,RESV_FIELD2))) AS RESV_FIELD2" + "\n");
            strSqlString.Append("                                          FROM CWIPPLNMON " + "\n");
            strSqlString.Append("                                         WHERE 1=1 " + "\n");
            strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("                                           AND PLAN_MONTH = '" + month + "'  " + "\n");
            strSqlString.Append("                                         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                                       ) " + "\n");
            strSqlString.Append("                                 GROUP BY MAT_ID" + "\n");
            strSqlString.Append("                                HAVING SUM(PLAN_QTY_TEST + RESV_FIELD2) > 0" + "\n");
            strSqlString.Append("                                 UNION ALL" + "\n");
            strSqlString.Append("                                SELECT MAT_ID, 0, 0" + "\n");
            strSqlString.Append("                                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 2 THEN D0_QTY " + "\n");
            strSqlString.Append("                                                WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 3 THEN D0_QTY + D1_QTY " + "\n");
            strSqlString.Append("                                                WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 4 THEN D0_QTY + D1_QTY + D2_QTY " + "\n");
            strSqlString.Append("                                                WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 5 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY " + "\n");
            strSqlString.Append("                                                WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 6 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY " + "\n");
            strSqlString.Append("                                                WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 7 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY " + "\n");
            strSqlString.Append("                                                WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 1 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY + D6_QTY " + "\n");
            strSqlString.Append("                                                ELSE 0 " + "\n");
            strSqlString.Append("                                       END) AS D0_PLAN " + "\n");
            strSqlString.Append("                                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 2 THEN D1_QTY " + "\n");
            strSqlString.Append("                                                WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 3 THEN D2_QTY " + "\n");
            strSqlString.Append("                                                WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 4 THEN D3_QTY " + "\n");
            strSqlString.Append("                                                WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 5 THEN D4_QTY " + "\n");
            strSqlString.Append("                                                WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 6 THEN D5_QTY " + "\n");
            strSqlString.Append("                                                WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 7 THEN D6_QTY " + "\n");
            strSqlString.Append("                                                WHEN TO_CHAR(TO_DATE('" + end_date + "', 'YYYYMMDD'), 'D') = 1 THEN D7_QTY " + "\n");
            strSqlString.Append("                                                ELSE 0 " + "\n");
            strSqlString.Append("                                       END) AS D1_PLAN " + "\n");
            strSqlString.Append("                                  FROM (" + "\n");
            strSqlString.Append("                                        SELECT FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.ThisWeek + "', D0_QTY, 0)) AS D0_QTY  " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.ThisWeek + "', D1_QTY, 0)) AS D1_QTY  " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.ThisWeek + "', D2_QTY, 0)) AS D2_QTY  " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.ThisWeek + "', D3_QTY, 0)) AS D3_QTY  " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.ThisWeek + "', D4_QTY, 0)) AS D4_QTY  " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.ThisWeek + "', D5_QTY, 0)) AS D5_QTY  " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.ThisWeek + "', D6_QTY, 0)) AS D6_QTY  " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.NextWeek + "', D0_QTY, 0)) AS D7_QTY  " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.NextWeek + "', D1_QTY, 0)) AS D8_QTY  " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.NextWeek + "', D2_QTY, 0)) AS D9_QTY  " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.NextWeek + "', D3_QTY, 0)) AS D10_QTY  " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.NextWeek + "', D4_QTY, 0)) AS D11_QTY  " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.NextWeek + "', D5_QTY, 0)) AS D12_QTY  " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.NextWeek + "', D6_QTY, 0)) AS D13_QTY  " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.ThisWeek + "', WW_QTY, 0)) AS W1_QTY  " + "\n");
            strSqlString.Append("                                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.NextWeek + "', WW_QTY, 0)) AS W2_QTY  " + "\n");
            strSqlString.Append("                                          FROM RWIPPLNWEK  " + "\n");
            strSqlString.Append("                                         WHERE 1=1  " + "\n");
            strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                                           AND GUBUN = '3'  " + "\n");
            strSqlString.Append("                                           AND PLAN_WEEK IN ('" + FindWeek_SOP_T.ThisWeek + "','" + FindWeek_SOP_T.NextWeek + "') " + "\n");
            strSqlString.Append("                                         GROUP BY FACTORY, MAT_ID  " + "\n");
            strSqlString.Append("                                       )  " + "\n");
            strSqlString.Append("                                 GROUP BY MAT_ID  " + "\n");
            strSqlString.Append("                               ) " + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID  " + "\n");
            strSqlString.Append("                       ) PLAN" + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID" + "\n");            
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE BETWEEN '" + month + "01' AND '" + end_date + "' THEN SHP_QTY_1 END) AS THIS_MON_SHIP " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE = '" + end_date + "' THEN  SHP_QTY_1  ELSE  0 END ) TO_DAY_SHP" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + yesterday + "' THEN SHP_QTY_1 END) AS SHP_WEEK " + "\n");
            strSqlString.Append("                          FROM VSUMWIPSHP" + "\n");
            strSqlString.Append("                         WHERE WORK_DATE BETWEEN '" + start_date + "' AND '" + end_date + "' " + "\n");
            strSqlString.Append("                           AND FACTORY  IN ('" + GlobalVariable.gsTestDefaultFactory + "')" + "\n");
            strSqlString.Append("                           AND CM_KEY_1 IN ('" + GlobalVariable.gsTestDefaultFactory + "')" + "\n");
            strSqlString.Append("                           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                           AND CM_KEY_2 = 'PROD'" + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("                       ) SHIP" + "\n");
            strSqlString.Append("                     , ( " + "\n");
            strSqlString.Append("                        SELECT MAT_ID, SUM(QTY_1) QTY " + "\n");
            strSqlString.Append("                          FROM RWIPLOTHIS" + "\n");
            strSqlString.Append("                         WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND OPER = 'T0000'" + "\n");                        
            strSqlString.Append("                           AND TRAN_TIME BETWEEN '" + yesterday + "220000' AND '" + end_date + "215959'" + "\n");
            strSqlString.Append("                           AND TRAN_CODE = 'CREATE'" + "\n");
            strSqlString.Append("                           AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                         GROUP BY FACTORY,MAT_ID" + "\n");
            strSqlString.Append("                       ) ST_IN" + "\n");
            strSqlString.Append("                 WHERE MAT.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND MAT.MAT_TYPE= 'FG'" + "\n");
            strSqlString.Append("                   AND MAT.DELETE_FLAG <> 'Y'" + "\n");            
            strSqlString.Append("                   AND MAT.MAT_ID = PLAN.MAT_ID(+)" + "\n");
            strSqlString.Append("                   AND MAT.MAT_ID = SHIP.MAT_ID(+)" + "\n");
            strSqlString.Append("                   AND MAT.MAT_ID = ST_IN.MAT_ID(+)" + "\n");
            strSqlString.Append("                 GROUP BY MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_GRP_9, MAT.MAT_GRP_10" + "\n");
            strSqlString.Append("                        , MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7, MAT.MAT_CMF_13, MAT.MAT_CMF_11" + "\n");
            strSqlString.Append("               ) PLAN" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT A.MAT_ID, MAT.MAT_GRP_3" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'HMK3T' , DECODE(MAT.MAT_GRP_3 ,'COB',ROUND(A.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),A.QTY_1), 0)) V0" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'TEST'  , DECODE(LOT_STATUS, 'WAIT', DECODE(HOLD_FLAG, ' ', DECODE(MAT.MAT_GRP_3,'COB',ROUND(A.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),A.QTY_1),0),0), 0)) V1" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'TEST'  , DECODE(LOT_STATUS, 'PROC', DECODE(HOLD_FLAG, ' ', DECODE(MAT.MAT_GRP_3,'COB',ROUND(A.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),A.QTY_1),0),0), 0)) V2" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'TEST'  , DECODE(HOLD_FLAG, 'Y', DECODE(MAT.MAT_GRP_3,'COB',ROUND(A.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),A.QTY_1),0), 0)) V3" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'QA1'   , DECODE(MAT.MAT_GRP_3,'COB',ROUND(A.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),A.QTY_1), 0)) V4" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'CAS'   , DECODE(MAT.MAT_GRP_3,'COB',ROUND(A.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),A.QTY_1), 0)) V5" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'OS'    , DECODE(MAT.MAT_GRP_3,'COB',ROUND(A.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),A.QTY_1), 0)) V6" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'QA2'   , DECODE(MAT.MAT_GRP_3,'COB',ROUND(A.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),A.QTY_1), 0)) V7" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'BAKE'  , DECODE(MAT.MAT_GRP_3,'COB',ROUND(A.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),A.QTY_1), 0)) V8" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'V/I'   , DECODE(MAT.MAT_GRP_3,'COB',ROUND(A.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),A.QTY_1), 0)) V9" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'TnR'   , DECODE(MAT.MAT_GRP_3,'COB',ROUND(A.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),A.QTY_1), 0)) V10" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'P/K'   , DECODE(MAT.MAT_GRP_3,'COB',ROUND(A.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),A.QTY_1), 0)) V11" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'HMK4T' , DECODE(MAT.MAT_GRP_3,'COB',ROUND(A.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),A.QTY_1), 0)) V12" + "\n");
            
            if (DateTime.Now.ToString("yyyyMMdd") != cdvDate.SelectedValue())
            {
                strSqlString.Append("                  FROM RWIPLOTSTS_BOH A, MWIPOPRDEF B, MWIPMATDEF MAT" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND A.CUTOFF_DT =  '" + cdvDate.SelectedValue() + "22'" + "\n");
            }

            else if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("                  FROM RWIPLOTSTS A, MWIPOPRDEF B, MWIPMATDEF MAT" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
            }

            strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND A.MAT_VER = 1" + "\n");
            strSqlString.Append("                   AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND A.LOT_CMF_5 LIKE '" + txtLotType.Text + "' " + "\n");
            strSqlString.Append("                   AND A.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                   AND A.OPER = B.OPER" + "\n");
            strSqlString.Append("                   AND A.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                   AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            strSqlString.Append("                   AND MAT.MAT_GRP_2 <> '-'" + "\n");
            strSqlString.Append("                 GROUP BY A.MAT_ID, MAT.MAT_GRP_3" + "\n");
            strSqlString.Append("               ) TEST_WIP" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT A.MAT_ID" + "\n");
            strSqlString.Append("                     , SUM( DECODE(B.OPER_CMF_3, 'HMK3',  DECODE(MAT.MAT_GRP_3,'COB',ROUND(A.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),A.QTY_1), 0) ) HMK3" + "\n");
            strSqlString.Append("                     , SUM( DECODE(B.OPER_CMF_3, 'FINISH',  DECODE(MAT.MAT_GRP_3,'COB',ROUND(A.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),A.QTY_1), 0) ) FINISH" + "\n");
            strSqlString.Append("                     , SUM( DECODE(B.OPER_CMF_3, 'MOLD',  DECODE(MAT.MAT_GRP_3,'COB',ROUND(A.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),A.QTY_1), 0) ) MOLD" + "\n");
            strSqlString.Append("                     , SUM( DECODE(B.OPER_CMF_3, 'BOND',  DECODE(MAT.MAT_GRP_3,'COB',ROUND(A.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),A.QTY_1), 0) ) BOND" + "\n");
            strSqlString.Append("                     , SUM( DECODE(B.OPER_CMF_3, 'BOND', DECODE(A.OPER, 'A0000', 0,  DECODE(MAT.MAT_GRP_3,'COB',ROUND(A.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),A.QTY_1), 0) )  ) FRONT" + "\n");
            strSqlString.Append("                     , SUM( DECODE(B.OPER_CMF_3, 'BOND', DECODE(A.OPER, 'A0000',  DECODE(MAT.MAT_GRP_3,'COB',ROUND(A.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),A.QTY_1), 0), 0 )  ) HMK2" + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") != cdvDate.SelectedValue())
            {
                strSqlString.Append("                  FROM RWIPLOTSTS_BOH A, MWIPOPRDEF B, MWIPMATDEF MAT" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND A.CUTOFF_DT =  '" + cdvDate.SelectedValue() + "22'" + "\n");                
            }

            else if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("                  FROM  RWIPLOTSTS A, MWIPOPRDEF B, MWIPMATDEF MAT" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");                
            }

            strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND A.MAT_VER = 1" + "\n");
            strSqlString.Append("                   AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND A.LOT_CMF_5 LIKE '" + txtLotType.Text + "' " + "\n");
            strSqlString.Append("                   AND A.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                   AND A.OPER = B.OPER" + "\n");
            strSqlString.Append("                   AND A.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                   AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            strSqlString.Append("                   AND MAT.MAT_GRP_2 <> '-'" + "\n");
            strSqlString.Append("                 GROUP BY A.MAT_ID" + "\n");
            strSqlString.Append("               ) ASSY_WIP" + "\n");

            if (rdbTurnkey.Checked == true)
            {
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT MAT_ID, MAX(PRICE) PRICE" + "\n");
                strSqlString.Append("                  FROM ( " + "\n");
                strSqlString.Append("                        SELECT PRODUCT AS MAT_ID, NVL(A_PRICE,0) + NVL(T_PRICE,0) AS PRICE" + "\n");
                strSqlString.Append("                          FROM (" + "\n");
                strSqlString.Append("                                SELECT PRODUCT, DECODE(CRNC_UNIT, 'USD', PRICE * STD_RATE, PRICE) AS T_PRICE" + "\n");
                strSqlString.Append("                                     , (SELECT DECODE(MAX(CRNC_UNIT), 'USD', MAX(PRICE * STD_RATE), MAX(PRICE)) FROM RPRIMATDAT WHERE SUBSTR(ITEM_CD,10,2) = 'A0' AND PRODUCT = A.PRODUCT) AS A_PRICE" + "\n");
                strSqlString.Append("                                  FROM RPRIMATDAT A" + "\n");
                strSqlString.Append("                                     , (SELECT STD_RATE" + "\n");
                strSqlString.Append("                                          FROM " + sExchange +"\n");
                strSqlString.Append("                                         WHERE SUBSTR(REPLACE(APPRL_DT, '-', ''), 0, 6) = '" + month + "' " + "\n");
                strSqlString.Append("                                       )" + "\n");
                strSqlString.Append("                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                   AND SUBSTR(ITEM_CD,10,2) = '0T' " + "\n");
                strSqlString.Append("                                   AND NVL(A.PRICE,0) > 0 " + "\n");
                strSqlString.Append("                               )" + "\n");
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) H" + "\n");
            }
            else
            {
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID, MAX(PRICE) PRICE" + "\n");
                strSqlString.Append("                  FROM (" + "\n");
                strSqlString.Append("                        SELECT PRODUCT AS MAT_ID, DECODE(CRNC_UNIT, 'USD', PRICE * STD_RATE, PRICE) AS PRICE" + "\n");
                strSqlString.Append("                          FROM RPRIMATDAT" + "\n");
                strSqlString.Append("                             , (SELECT STD_RATE" + "\n");
                strSqlString.Append("                                  FROM " + sExchange + "\n");
                strSqlString.Append("                                 WHERE SUBSTR(REPLACE(APPRL_DT, '-', ''), 0, 6) = '" + month + "' " + "\n");
                strSqlString.Append("                               )" + "\n");
                strSqlString.Append("                         WHERE SUBSTR(ITEM_CD,10,2) = '0T'" + "\n");
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) H" + "\n");
            }

            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT MAT_ID " + "\n");
            strSqlString.Append("                     , SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1) AS QTY " + "\n");
            strSqlString.Append("                  FROM RSUMWIPMOV " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND OPER IN ('T0100', 'T0400') " + "\n");
            strSqlString.Append("                   AND WORK_DATE = '" + end_date + "' " + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                   AND CM_KEY_3 LIKE 'P%' " + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("               ) MOV" + "\n");
            strSqlString.Append("         WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND A.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("           AND A.DELETE_FLAG <> 'Y'" + "\n");
            strSqlString.Append("           AND A.MAT_ID = PLAN.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND A.MAT_ID = TEST_WIP.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND A.MAT_ID = ASSY_WIP.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND A.MAT_ID = H.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND A.MAT_ID = MOV.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND A.MAT_ID like '" + txtSearchProduct.Text + "' " + "\n");
                     
            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

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

            strSqlString.Append("         GROUP BY  A.MAT_GRP_1,  A.MAT_GRP_9 ,  A.MAT_GRP_10, A.MAT_GRP_9, A.MAT_GRP_3, A.MAT_GRP_6, A.MAT_CMF_11" + "\n");
            strSqlString.Append("       ) A" + "\n");
            strSqlString.Append(" WHERE (" + "\n");
            strSqlString.Append("        NVL(MON_PLAN, 0)+" + "\n");
            strSqlString.Append("        NVL(ORI_PLAN, 0)+" + "\n");
            strSqlString.Append("        NVL(THIS_MON_SHIP, 0)+" + "\n");
            strSqlString.Append("        NVL(TO_DAY_SHP, 0)+ NVL(TTL, 0)+ NVL(HMK4T, 0)+" + "\n");
            strSqlString.Append("        NVL(TEST_END, 0)+ NVL(TEST, 0)+ NVL(HMK3T, 0)+ NVL(HMK3A, 0)+" + "\n");
            strSqlString.Append("        NVL(FINISH, 0)+ NVL(MOLD, 0)+ NVL(FRONT, 0)+" + "\n");
            strSqlString.Append("        NVL(HMK2, 0)+ NVL(RCV2, 0) + NVL(D0_PLAN,0) + NVL(D1_PLAN,0)  ) > 0 AND MAT_GRP_10 <> 'COB'" + "\n");
            strSqlString.Append(" GROUP BY  " + QueryCond1 + "\n");
            strSqlString.Append(" ORDER BY DECODE(MAT_GRP_1, 'SE', 1, 'HX', 2, 'IM', 3, 'FC', 4, 'IG', 5,6), " + QueryCond4 + "\n");


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
            LabelTextChange();
            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);

                if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                {
                    dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());
                }
                else if (cdvFactory.Text == "HMKB1")
                {
                    dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringBump());
                }
                else
                {
                    dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString03());
                }

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                ////그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                if (cdvFactory.Text == "HMKB1")
                {
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 5, null, null, btnSort);            // 월간 앞
                    spdData.RPT_FillDataSelectiveCells("Total", 0, 5, 0, 1, true, Align.Center, VerticalAlign.Center);      // 월간 앞

                    spdData.RPT_SetPerSubTotalAndGrandTotal(1, 7, 5, 8);
                    spdData.RPT_SetPerSubTotalAndGrandTotal(1, 7, 6, 9);                    
                }
                else
                {
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 7, null, null, btnSort);            // 월간 앞
                    spdData.RPT_FillDataSelectiveCells("Total", 0, 7, 0, 1, true, Align.Center, VerticalAlign.Center);      // 월간 앞

                    spdData.RPT_SetPerSubTotalAndGrandTotal(1, 9, 7, 10);
                    spdData.RPT_SetPerSubTotalAndGrandTotal(1, 9, 8, 11);                   
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
                //Condition.AppendFormat("기준일자: {0}     today: {1}      workday: {2}     remain: {3}      표준진도율: {4} " + "\n", cdvDate.Text, lblToday.Text.ToString(), lblLastDay.Text.ToString(), lblRemain.Text.ToString(), lblJindo.Text.ToString());
                //Condition.Append("        단위 : PKG (pcs) , COB (매) ");

                ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);
            }
        }

        /// <summary>
        /// Factory 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            if (cdvFactory.txtValue.Equals("HMKB1"))
            {
                BaseFormType = eBaseFormType.BUMP_BASE;
                pnlBUMPDetail.Visible = false;

                rdbQuantity.Checked = true;
                rdbTurnkey.Enabled = false;

                cdvType.Items.Clear();
                cdvType.Items.Add("basics");
                cdvType.Items.Add("Detail");
                cdvType.SelectedIndex = 0;

                ckbKpcs.Checked = false;
            }
            else
            {
                BaseFormType = eBaseFormType.WIP_BASE;
                pnlWIPDetail.Visible = false;

                rdbTurnkey.Enabled = true;

                cdvType.Items.Clear();
                cdvType.Items.Add("basics");
                cdvType.Items.Add("Stack Product");
                cdvType.Items.Add("Detail");
                cdvType.SelectedIndex = 0;
            }

            this.SetFactory(cdvFactory.txtValue);

            SortInit();
        }

        /// <summary>
        /// 7. 상단 Lebel 표시
        /// </summary>
        private void LabelTextChange()
        {
            int remain = 0;

            DateTime getStartDate = Convert.ToDateTime(cdvDate.Value.ToString("yyyy-MM") + "-01");
            string getEndDate = getStartDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");
            string strDate = cdvDate.Value.ToString("yyyyMMdd");
            
            // ASSY 진도는 LSI=해당월 말일 - 2일 기준,MEMORY 해당월 말일 기준으로 현재일 계산함.
            string selectday = strDate.Substring(6, 2);
            string lastday = getEndDate.Substring(6, 2);

            double jindo = (Convert.ToDouble(selectday)) / Convert.ToDouble(lastday) * 100;

            // 진도율 소수점 1째자리 까지 표시 (2009.08.17 임종우)
            jindoPer = Math.Round(Convert.ToDecimal(jindo),1);

            dayArry[0] = cdvDate.Value.AddDays(-2).ToString("MM.dd");
            dayArry[1] = cdvDate.Value.AddDays(-1).ToString("MM.dd");
            dayArry[2] = cdvDate.Value.ToString("MM.dd");

            dayArry2[0] = cdvDate.Value.AddDays(-2).ToString("yyyyMMdd");
            dayArry2[1] = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");
            dayArry2[2] = cdvDate.Value.ToString("yyyyMMdd");

            lblToday.Text = selectday + " day";
            lblLastDay.Text = lastday + " day";

            // 금일 조회일 경우 잔여일에 금일 포함함.
            if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
            {
                remain = (Convert.ToInt32(lastday) - Convert.ToInt32(selectday) + 1);
            }
            else
            {
                remain = (Convert.ToInt32(lastday) - Convert.ToInt32(selectday));
            }

            lblRemain.Text = remain.ToString() + " day";             
            lblJindo.Text = jindoPer.ToString() + "%";

            FindWeek_SOP_A = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");
            FindWeek_SOP_T = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "SE");

        }
        #endregion

        private void rdbQuantity_Click(object sender, EventArgs e)
        {
            ckbKpcs.Text = "Kcps";
        }

        private void rdbSales_Click(object sender, EventArgs e)
        {
            ckbKpcs.Text = "one million won";
        }

        private void rdbTurnkey_Click(object sender, EventArgs e)
        {
            ckbKpcs.Text = "one million won";
        }

        #region "Imitate Excel Sum"
        private bool IsNumeric(string value)
        {
            value = value.Trim();
            value = value.Replace(".", "");

            foreach (char cData in value)
            {
                if (false == Char.IsNumber(cData))
                {
                    return false;
                }
            }
            return true;
        }

        private void spdData_KeyUp(object sender, KeyEventArgs e)
        {
            if (spdData.ActiveSheet.RowCount <= 0) return;

            FarPoint.Win.Spread.Model.CellRange crRange;

            try
            {
                crRange = spdData.ActiveSheet.GetSelection(0);
                spdData.ActiveSheet.GetClip(crRange.Row, crRange.Column, crRange.RowCount, crRange.ColumnCount);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "You didn't make a selection!!");
                return;
            }

            double dblSum = 0; long lngCnt = 0;
            for (int ii = crRange.Row; ii < crRange.Row + crRange.RowCount; ii++)
            {
                for (int kk = crRange.Column; kk < crRange.Column + crRange.ColumnCount; kk++)
                {
                    string strTmpValue = spdData.ActiveSheet.Cells[ii, kk].Text;
                    strTmpValue = strTmpValue.Replace(",", "");
                    if (strTmpValue.Trim() == "") strTmpValue = "0";

                    if (this.IsNumeric(strTmpValue))
                    {
                        dblSum += double.Parse(strTmpValue);
                    }
                    else
                    {
                        lblNumericSum.Text = "Characters included.";
                        return;
                    }
                    lngCnt++;
                }
            }

            if (lngCnt > 0 && dblSum != 0)
                lblNumericSum.Text = "개수: " + lngCnt.ToString("#,###").Trim() + "    합계: " + dblSum.ToString("#,###").Trim();
            else
                lblNumericSum.Text = "";
        }


        private void spdData_MouseUp(object sender, MouseEventArgs e)
        {
            spdData_KeyUp(sender, new KeyEventArgs(Keys.Return));
        }

        #endregion        
    }       
}
