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
    public partial class PRD010202 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        private String Lastweek_lastday = null;     // 지난주차 마지막일
        private String Thisweek_startday = null;    // 금주차 시작일
        private String Thisweek_lastday = null;     // 금주차 마지막일
        private String Thisweek = null;             // 금주 주차
        private String Nextweek = null;             // 차주 주차

        /// <summary>
        /// 클  래  스: PRD010202<br/>
        /// 클래스요약: TEST 진도 관리<br/>
        /// 작  성  자: 미라콤 김민규<br/>
        /// 최초작성일: 2008-12-05<br/>
        /// 상세  설명: TEST 진도 관리<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// 2010-11-05-임종우 : T0400 실적도 전일 TEST IN/OUT 실적에 반영함. (김권수 요청)
        /// 2011-05-19-임종우 : HMKT1일 경우 H72(장기정체) HOLD 재공 제외 (김권수 요청)
        /// 2011-07-25-임종우 : 과거 월 계획 표시 되도록 수정
        /// 2011-11-21-임종우 : 월 계획 제품 중복 오류 수정
        /// 2011-12-26-임종우 : MWIPCALDEF 의 작년,올해 마지막 주차 겹치는 에러 발생으로 SYS_YEAR -> PLAN_YEAR 으로 변경
        /// 2012-08-14-임종우 : 주간 조회 기능 추가 (임태성 요청)
        /// 2012-09-19-임종우 : HMKE1의 실적 표시 되도록 수정 (박민정 요청)
        /// 2013-01-07-김민우 : SAP_CODE 추가, BASE_MAT_ID만 (김동인 요청)
        /// 2013-06-07-임종우 : SE 제품 월 계획 REV 값을 기존 OMS 데이터에서 월 계획 UPLOAD 데이터로 변경 함 (김은석 요청)
        /// 2013-09-02-임종우 : HMKE1의 계획 표시 되도록 수정 (박민정 요청)
        /// 2013-10-14-김민우 : LOT TYPE ALL, P%, E% 구분으로변경
        /// 2016-09-07-임종우 : HMKT1, HMKE1 분리 (임태성K 요청)
        /// 2020-03-03-김미경 : COB 제외 기능 추가 (이승희D 요청)
        /// </summary>
        public PRD010202()
        {
            InitializeComponent();
            SortInit();
            GridColumnInit();
            this.SetFactory(GlobalVariable.gsTestDefaultFactory);
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            cdvFactory.Text = GlobalVariable.gsTestDefaultFactory;

            cdvDate.Value = DateTime.Now;
        }

        #region 유효성 검사 및 초기화

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
            MakeSqlString2(cdvDate.SelectedValue().Substring(0, 4), cdvDate.SelectedValue());

            try
            {
                if (ckbWeek.Checked == true)
                {
                    #region 주 계획, kpcs 체크
                    if (ckbKpcs.Checked == true)
                    {
                        spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("LD Count", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 200);
                        spdData.RPT_AddBasicColumn("Cust Device", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 200);
                        spdData.RPT_AddBasicColumn("SAP_CODE", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);

                        spdData.RPT_AddBasicColumn("weekly", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 80);                        
                        spdData.RPT_AddBasicColumn("WW30", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);                        
                        spdData.RPT_AddBasicColumn("SHIP", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);                        
                        spdData.RPT_AddBasicColumn("Progress rate", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_AddBasicColumn("Shortage (OUT)", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 90);
                        spdData.RPT_AddBasicColumn("Shortage (IN)", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_MerageHeaderColumnSpan(0, 12, 5);

                        spdData.RPT_AddBasicColumn("WW31", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);

                        spdData.RPT_AddBasicColumn("a daily goal", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("OUT", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);

                        spdData.RPT_AddBasicColumn("The previous day's performance", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Recv", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("T-IN", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("T-OUT", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("FGS", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 19, 5);

                        spdData.RPT_AddBasicColumn("the day's performance", 0, 24, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Recv", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_AddBasicColumn("FGS", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_AddBasicColumn("R-OUT", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_MerageHeaderColumnSpan(0, 24, 4);

                        spdData.RPT_AddBasicColumn("TEST WIP", 0, 28, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("TOTAL", 1, 28, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_AddBasicColumn("HMK4T", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("P/K", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("TnR", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("V/I", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("Bake", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("QA2", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("O/S, HT", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("CAS", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("QA1", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("HOLD", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("F/T", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("WAIT", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("HMK3T", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 28, 14);

                        spdData.RPT_AddBasicColumn("ASSY WIP", 0, 42, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("A-TTL", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("HMK3A", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("FINISH", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("BOND", 1, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 42, 5);

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
                        spdData.RPT_MerageHeaderRowSpan(0, 11, 2);
                        spdData.RPT_MerageHeaderRowSpan(0, 16, 2);

                        spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
                    }
                    #endregion
                    #region 주 계획, kpcs 체크 안함
                    else
                    {

                        spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("LD Count", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 130);
                        spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 180);
                        spdData.RPT_AddBasicColumn("Cust Device", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 180);
                        spdData.RPT_AddBasicColumn("SAP_CODE", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);

                        spdData.RPT_AddBasicColumn("weekly", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("WW30", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);                        
                        spdData.RPT_AddBasicColumn("Progress rate", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_AddBasicColumn("Shortage (OUT)", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 90);
                        spdData.RPT_AddBasicColumn("Shortage (IN)", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_MerageHeaderColumnSpan(0, 12, 5);

                        spdData.RPT_AddBasicColumn("WW31", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                        spdData.RPT_AddBasicColumn("a daily goal", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("OUT", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                        spdData.RPT_AddBasicColumn("The previous day's performance", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Recv", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("T-IN", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("T-OUT", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("FGS", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 19, 5);

                        spdData.RPT_AddBasicColumn("the day's performance", 0, 24, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Recv", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("FGS", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("R-OUT", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_MerageHeaderColumnSpan(0, 24, 4);

                        spdData.RPT_AddBasicColumn("TEST WIP", 0, 28, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("TOTAL", 1, 28, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("HMK4T", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("P/K", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TnR", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("V/I", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Bake", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("QA2", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("O/S, HT", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("CAS", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("QA1", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HOLD", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("F/T", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WAIT", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMK3T", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 28, 14);

                        spdData.RPT_AddBasicColumn("ASSY WIP", 0, 42, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("A-TTL", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMK3A", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("FINISH", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("BOND", 1, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 42, 5);
                        
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
                        spdData.RPT_MerageHeaderRowSpan(0, 11, 2);
                        spdData.RPT_MerageHeaderRowSpan(0, 16, 2);

                        spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
                    }
                    #endregion
                }
                else
                {
                    #region 월 계획, kpcs 체크
                    if (ckbKpcs.Checked == true)
                    {
                        spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("LD Count", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 200);
                        spdData.RPT_AddBasicColumn("Cust Device", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 200);
                        spdData.RPT_AddBasicColumn("SAP_CODE", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);

                        spdData.RPT_AddBasicColumn("monthly", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 80);
                        if (ckbRev.Checked == false)
                        {
                            spdData.RPT_AddBasicColumn("Monthly plan", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                            spdData.RPT_AddBasicColumn("Monthly Plan Rev", 1, 13, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                            spdData.RPT_AddBasicColumn("Monthly plan difference", 1, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn("Monthly plan", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                            spdData.RPT_AddBasicColumn("Monthly Plan Rev", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                            spdData.RPT_AddBasicColumn("Monthly plan difference", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        }
                        spdData.RPT_AddBasicColumn("SHIP", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_AddBasicColumn("FGS", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_AddBasicColumn("Progress rate", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_AddBasicColumn("Month shortage (OUT)", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 90);
                        spdData.RPT_AddBasicColumn("Month shortage (IN)", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_MerageHeaderColumnSpan(0, 12, 8);

                        spdData.RPT_AddBasicColumn("a daily goal", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("OUT", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);

                        spdData.RPT_AddBasicColumn("Weekly goal", 0, 21, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("OUT", 1, 21, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);


                        spdData.RPT_AddBasicColumn("the day's performance", 0, 22, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Recv", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_AddBasicColumn("FGS", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_AddBasicColumn("R-OUT", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_MerageHeaderColumnSpan(0, 22, 4);

                        spdData.RPT_AddBasicColumn("FGS", 0, 26, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_AddBasicColumn("FGS", 1, 26, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_MerageHeaderRowSpan(0, 26, 2);

                        spdData.RPT_AddBasicColumn("TEST WIP", 0, 27, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("TOTAL", 1, 27, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_AddBasicColumn("HMK4T", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("P/K", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("TnR", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("V/I", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("Bake", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("QA2", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("O/S, HT", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("CAS", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("QA1", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("HOLD", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("F/T", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("WAIT", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("HMK3T", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 27, 14);

                        spdData.RPT_AddBasicColumn("ASSY WIP", 0, 41, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("A-TTL", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("HMK3A", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("FINISH", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("BOND", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 41, 5);

                        spdData.RPT_AddBasicColumn("Total WIP", 0, 46, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Total WIP", 1, 46, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_MerageHeaderRowSpan(0, 46, 2);

                        spdData.RPT_AddBasicColumn("The previous day's performance", 0, 47, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Recv", 1, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("T-IN", 1, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("T-OUT", 1, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 50, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("FGS", 1, 51, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 47, 5);

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
                        spdData.RPT_MerageHeaderRowSpan(0, 11, 2);

                        spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
                    }
                    #endregion
                    #region 월 계획, kpcs 체크 안함
                    else
                    {

                        spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("LD Count", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 130);
                        spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 180);
                        spdData.RPT_AddBasicColumn("Cust Device", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 180);
                        spdData.RPT_AddBasicColumn("SAP_CODE", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);

                        spdData.RPT_AddBasicColumn("monthly", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        if (ckbRev.Checked == false)
                        {
                            spdData.RPT_AddBasicColumn("Monthly plan", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                            spdData.RPT_AddBasicColumn("Monthly Plan Rev", 1, 13, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                            spdData.RPT_AddBasicColumn("Monthly plan difference", 1, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn("Monthly plan", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                            spdData.RPT_AddBasicColumn("Monthly Plan Rev", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                            spdData.RPT_AddBasicColumn("Monthly plan difference", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        }

                        spdData.RPT_AddBasicColumn("SHIP", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("FGS", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("Progress rate", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        spdData.RPT_AddBasicColumn("Month shortage (OUT)", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 90);
                        spdData.RPT_AddBasicColumn("Month shortage (IN)", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_MerageHeaderColumnSpan(0, 12, 8);

                        spdData.RPT_AddBasicColumn("a daily goal", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("OUT", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                        spdData.RPT_AddBasicColumn("Weekly goal", 0, 21, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("OUT", 1, 21, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                        spdData.RPT_AddBasicColumn("the day's performance", 0, 22, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Recv", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("FGS", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("R-OUT", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_MerageHeaderColumnSpan(0, 22, 4);

                        spdData.RPT_AddBasicColumn("FGS", 0, 26, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("FGS", 1, 26, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
                        spdData.RPT_MerageHeaderRowSpan(0, 26, 2);

                        spdData.RPT_AddBasicColumn("TEST WIP", 0, 27, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("TOTAL", 1, 27, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("HMK4T", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("P/K", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TnR", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("V/I", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Bake", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("QA2", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("O/S, HT", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("CAS", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("QA1", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HOLD", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("F/T", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WAIT", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMK3T", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 27, 14);

                        spdData.RPT_AddBasicColumn("ASSY WIP", 0, 41, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("A-TTL", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMK3A", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("FINISH", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("BOND", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 41, 5);

                        spdData.RPT_AddBasicColumn("Total WIP", 0, 46, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Total WIP", 1, 46, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
                        spdData.RPT_MerageHeaderRowSpan(0, 46, 2);

                        spdData.RPT_AddBasicColumn("The previous day's performance", 0, 47, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Recv", 1, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("T-IN", 1, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("T-OUT", 1, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 50, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("FGS", 1, 51, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 47, 5);


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
                        spdData.RPT_MerageHeaderRowSpan(0, 11, 2);

                        spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
                    }
                    #endregion
                }
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "A.MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "A.MAT_GRP_2", "A.MAT_GRP_2 AS FAMILY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "A.MAT_GRP_6", "A.MAT_GRP_6 AS \"LD COUNT\"", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "A.MAT_GRP_3", "A.MAT_GRP_3 AS PACKAGE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "A.MAT_GRP_4", "A.MAT_GRP_4 AS TYPE1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "A.MAT_GRP_5", "A.MAT_GRP_5 AS TYPE2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "A.MAT_GRP_7", "A.MAT_GRP_7 AS DENSITY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "A.MAT_GRP_8", "A.MAT_GRP_8 AS GENERATION", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "A.MAT_CMF_10", "A.MAT_CMF_10 AS PIN_TYPE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.MAT_ID", "A.MAT_ID AS PRODUCT", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST DEVICE", "A.MAT_CMF_7", "A.MAT_CMF_7 AS CUST_DEVICE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SAP CODE", "A.BASE_MAT_ID", "A.BASE_MAT_ID", true);
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
            string date;
            string month;
            string year;
            string lastMonth;
            string start_date;
            string yesterday;            
            bool lotType = true;
            string sKcpkValue;         // Kcpk 구분에 의한 나누기 분모 값 

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            date = cdvDate.SelectedValue();

            DateTime Select_date;
            Select_date = DateTime.Parse(cdvDate.Text.ToString());

            year = Select_date.ToString("yyyy");
            month = Select_date.ToString("yyyyMM");
            start_date = month + "01";
            yesterday = Select_date.AddDays(-1).ToString("yyyyMMdd");
            lastMonth = Select_date.AddMonths(-1).ToString("yyyyMM"); //지난달

            // 지난달 마지막일 구하기
            DataTable dt1 = null;
            string Last_Month_Last_day = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + lastMonth + "', 'YYYYMM')),'YYYYMMDD') FROM DUAL)";
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", Last_Month_Last_day);
            Last_Month_Last_day = dt1.Rows[0][0].ToString();

            // 달의 마지막일 구하기
            DataTable dt2 = null;
            string last_day = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + month + "', 'YYYYMM')),'YYYYMMDD') FROM DUAL)";
            dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", last_day);
            last_day = dt2.Rows[0][0].ToString();
                      
            // LotType 선택시 P Type 있는지 확인, 있으면 월계획 뿌려줌.
            if (cdvLotType.Text == "P%")
            {
                lotType = true;
            }

            // 2012-08-16-임종우 : kcpk 선택에 의한 분모 값 저장 한다.
            if (ckbKpcs.Checked == true)
            {
                sKcpkValue = "1000";
            }
            else
            {
                sKcpkValue = "1";
            }

            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond2);

            #region 주 계획
            if (ckbWeek.Checked == true)
            {
                strSqlString.Append("     , ROUND(SUM(NVL(A.WEEK1_PLAN,0))/" + sKcpkValue + ", 1) " + "\n");                
                strSqlString.Append("     , ROUND(SUM(NVL(SHIP_WEEK,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , CASE WHEN SUM(NVL(A.WEEK1_PLAN,0)) = 0 OR SUM(NVL(SHIP_WEEK,0)) = 0 THEN 0 " + "\n");
                strSqlString.Append("            ELSE ROUND((SUM(NVL(SHIP_WEEK,0))/ SUM(NVL(A.WEEK1_PLAN,0)))*100,1) " + "\n");
                strSqlString.Append("       END AS JINDO " + "\n");
                strSqlString.Append("     , ROUND(( SUM(NVL(A.WEEK1_PLAN,0)) - SUM(NVL(SHIP_WEEK,0)) )/" + sKcpkValue + ", 1) AS LACK_OUT       " + "\n");
                strSqlString.Append("     , ROUND(( SUM(NVL(A.WEEK1_PLAN,0)) - ( SUM(NVL(SHIP_WEEK,0)) + SUM(NVL(CUR_WIP,0)) ) )/" + sKcpkValue + ", 1) AS LACK_IN " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.WEEK2_PLAN,0))/" + sKcpkValue + ", 1) " + "\n");

                if (lblRemain2.Text != "0")
                {
                    strSqlString.Append("     , ROUND(( SUM(NVL(A.WEEK1_PLAN,0)) - SUM(NVL(SHIP_WEEK,0)) ) /" + Convert.ToUInt32(lblRemain2.Text) + "/" + sKcpkValue + ", 1) AS TARGET " + "\n");
                }
                else
                {
                    strSqlString.Append("     ,0 AS TARGET " + "\n");
                }

                strSqlString.Append("     , ROUND(SUM(NVL(A.RCV_YESTERDAY,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(D.T_IN,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(D.T_OUT,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.SHIP_YESTERDAY,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(RCV_FGS_YESTERDAY,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.RCV_DAY,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.SHIP_DAY,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(RCV_FGS_TODAY,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(E.RTN_DAY,0))/" + sKcpkValue + ", 1) " + "\n");                
                strSqlString.Append("     , ROUND((SUM(NVL(B.V0,0)) + SUM(NVL(B.V1,0)) + SUM(NVL(B.V2,0)) + SUM(NVL(B.V3,0)) + SUM(NVL(B.V4,0)) + SUM(NVL(B.V5,0)) + SUM(NVL(B.V6,0)) ");
                strSqlString.Append("       + SUM(NVL(B.V7,0)) + SUM(NVL(B.V8,0)) + SUM(NVL(B.V9,0)) + SUM(NVL(B.V10,0)) + SUM(NVL(B.V11,0)) + SUM(NVL(B.V12,0))) /" + sKcpkValue + ", 1) AS TOTLA " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V12,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V11,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V10,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V9,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V8,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V7,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V6,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V5,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V4,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V3,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V2,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V1,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V0,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V13,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V14,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V15,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V16,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V17,0))/" + sKcpkValue + ", 1) " + "\n");                
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT A.MAT_GRP_1, A.MAT_GRP_2, A.MAT_GRP_3, A.MAT_GRP_4, A.MAT_GRP_5, A.MAT_GRP_6, A.MAT_GRP_7, A.MAT_GRP_8, A.MAT_CMF_10, A.MAT_ID, A.MAT_CMF_7,BASE_MAT_ID " + "\n");
                strSqlString.Append("             , B.WEEK1_PLAN " + "\n");
                strSqlString.Append("             , B.WEEK2_PLAN " + "\n");
                strSqlString.Append("             , C.SHIP_WEEK " + "\n");
                strSqlString.Append("             , F.SHIP_DAY " + "\n");
                strSqlString.Append("             , C.SHIP_YESTERDAY " + "\n");
                strSqlString.Append("             , D.CUR_WIP " + "\n");
                strSqlString.Append("             , E.RCV_YESTERDAY " + "\n");
                strSqlString.Append("             , E.RCV_DAY " + "\n");
                strSqlString.Append("             , D.CUR_FGS " + "\n");
                strSqlString.Append("          FROM (          " + "\n");
                strSqlString.Append("                SELECT DISTINCT MAT_ID, MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, MAT_GRP_9, MAT_GRP_10, MAT_CMF_7, MAT_CMF_10, BASE_MAT_ID  " + "\n");
                strSqlString.Append("                  FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                 WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");
                //strSqlString.Append("                 WHERE FACTORY IN ('" + GlobalVariable.gsTestDefaultFactory + "', 'HMKE1') " + "\n");
                strSqlString.Append("                   AND DELETE_FLAG = ' ' " + "\n");
                strSqlString.Append("                   AND MAT_TYPE = 'FG' " + "\n");

                if (ckbCOB.Checked == true)
                    strSqlString.Append("           AND MAT_GRP_3 <> 'COB' " + "\n");

                strSqlString.Append("               ) A " + "\n");
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + Thisweek + "', PLAN_QTY, 0)) AS WEEK1_PLAN " + "\n");
                strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + Nextweek + "', PLAN_QTY, 0)) AS WEEK2_PLAN " + "\n");
                strSqlString.Append("                  FROM CWIPPLNWEK@RPTTOMES " + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                //strSqlString.Append("                   AND FACTORY IN ('" + GlobalVariable.gsTestDefaultFactory + "', 'HMKE1') " + "\n");
                strSqlString.Append("                   AND PLAN_WEEK IN ('" + Thisweek + "', '" + Nextweek + "') " + "\n");
                strSqlString.Append("                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("               ) B " + "\n");
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT MAT_ID " + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE,'" + Lastweek_lastday + "', 0, NVL(SHP_QTY_1, 0))) AS SHIP_WEEK " + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE,'" + yesterday + "', NVL(SHP_QTY_1, 0), 0)) AS SHIP_YESTERDAY " + "\n");
                strSqlString.Append("                  FROM ( " + "\n");

                if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
                {
                    strSqlString.Append("                        SELECT MAT_ID, WORK_DATE, CM_KEY_3, SHP_QTY_1 " + "\n");
                    strSqlString.Append("                          FROM VSUMWIPSHP " + "\n");
                    strSqlString.Append("                         WHERE 1=1 " + "\n");
                    strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                           AND CM_KEY_1 = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                           AND LOT_TYPE = 'W'" + "\n");
                    strSqlString.Append("                           AND CM_KEY_2 = 'PROD' " + "\n");
                    strSqlString.Append("                           AND WORK_DATE BETWEEN '" + Lastweek_lastday + "' AND '" + date + "'" + "\n");
                }
                else
                {
                    //strSqlString.Append("                        UNION ALL " + "\n");
                    strSqlString.Append("                        SELECT MAT_ID, WORK_DATE, CM_KEY_3, (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) AS SHP_QTY_1 " + "\n");
                    strSqlString.Append("                          FROM RSUMFACMOV " + "\n");
                    strSqlString.Append("                         WHERE 1=1" + "\n");
                    strSqlString.Append("                           AND WORK_DATE BETWEEN '" + Lastweek_lastday + "' AND '" + date + "'" + "\n");
                    strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                           AND CM_KEY_1 = 'HMKE1'" + "\n");
                }

                strSqlString.Append("                       ) " + "\n");
                
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                 WHERE CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) C " + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    strSqlString.Append("             , ( " + "\n");
                    strSqlString.Append("                SELECT MAT_ID " + "\n");
                    strSqlString.Append("                     , SUM(CASE WHEN FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' THEN QTY_1 " + "\n");
                    strSqlString.Append("                                ELSE 0 END) AS CUR_WIP   " + "\n");
                    strSqlString.Append("                     , SUM(CASE WHEN FACTORY = 'FGS' THEN QTY_1 " + "\n");
                    strSqlString.Append("                                ELSE 0 END) AS CUR_FGS " + "\n");
                    strSqlString.Append("                  FROM RWIPLOTSTS  " + "\n");
                    strSqlString.Append("                 WHERE FACTORY  IN ('" + cdvFactory.Text + "', 'FGS') " + "\n");
                    //strSqlString.Append("                 WHERE FACTORY  IN ('" + GlobalVariable.gsTestDefaultFactory + "', 'FGS') " + "\n");
                    strSqlString.Append("                   AND MAT_VER = 1 " + "\n");
                    strSqlString.Append("                   AND LOT_DEL_FLAG = ' ' " + "\n");
                    strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND HOLD_CODE <> 'H72' " + "\n");
                    
                    if (cdvLotType.Text != "ALL")
                    {
                        strSqlString.Append("                   AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                    }

                    strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
                    strSqlString.Append("               ) D " + "\n");
                }
                else
                {
                    strSqlString.Append("             , ( " + "\n");
                    strSqlString.Append("                SELECT MAT_ID " + "\n");
                    strSqlString.Append("                     , SUM(CASE WHEN FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' THEN QTY_1 " + "\n");
                    strSqlString.Append("                                ELSE 0 END) AS CUR_WIP   " + "\n");
                    strSqlString.Append("                     , SUM(CASE WHEN FACTORY = 'FGS' THEN QTY_1 " + "\n");
                    strSqlString.Append("                                ELSE 0 END) AS CUR_FGS " + "\n");
                    strSqlString.Append("                  FROM RWIPLOTSTS_BOH  " + "\n");
                    strSqlString.Append("                 WHERE FACTORY  IN ('" + cdvFactory.Text + "', 'FGS') " + "\n");
                    //strSqlString.Append("                 WHERE FACTORY IN ('" + GlobalVariable.gsTestDefaultFactory + "', 'FGS') " + "\n");
                    strSqlString.Append("                   AND MAT_VER = 1  " + "\n");
                    strSqlString.Append("                   AND LOT_DEL_FLAG = ' ' " + "\n");
                    strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND HOLD_CODE <> 'H72' " + "\n");
                    strSqlString.Append("                   AND CUTOFF_DT = '" + cdvDate.SelectedValue() + "22'" + "\n");
                   
                    if (cdvLotType.Text != "ALL")
                    {
                        strSqlString.Append("                   AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                    }
                    strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
                    strSqlString.Append("               ) D " + "\n");
                }

                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT MAT_ID " + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE = '" + yesterday + "' THEN NVL(S1_OPER_IN_QTY_1,0) + NVL(S2_OPER_IN_QTY_1,0) + NVL(S3_OPER_IN_QTY_1,0)" + "\n");
                strSqlString.Append("                                ELSE 0 " + "\n");
                strSqlString.Append("                           END) AS RCV_YESTERDAY" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE = '" + date + "' THEN NVL(S1_OPER_IN_QTY_1,0) + NVL(S2_OPER_IN_QTY_1,0) + NVL(S3_OPER_IN_QTY_1,0)" + "\n");
                strSqlString.Append("                                ELSE 0 " + "\n");
                strSqlString.Append("                           END) RCV_DAY " + "\n");
                strSqlString.Append("                  FROM RSUMWIPMOV " + "\n");
                strSqlString.Append("                 WHERE 1 = 1     " + "\n");
                strSqlString.Append("                   AND MAT_VER=1  " + "\n");
                strSqlString.Append("                   AND FACTORY  = '" + cdvFactory.Text + "' " + "\n");
                //strSqlString.Append("                   AND FACTORY  = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND OPER = 'T0000'" + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                   AND CM_KEY_2 = 'PROD'" + "\n");
                
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.AppendFormat("                   AND WORK_DATE BETWEEN '{0}' AND '{1}'" + "\n", yesterday, date);
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) E" + "\n");

                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT MAT_ID " + "\n");
                strSqlString.Append("                     , SUM(SHP_QTY_1) SHIP_DAY " + "\n");
                strSqlString.Append("                  FROM ( " + "\n");

                if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
                {
                    strSqlString.Append("                        SELECT MAT_ID, CM_KEY_3, SHP_QTY_1 " + "\n");
                    strSqlString.Append("                          FROM VSUMWIPSHP_ONLY " + "\n");
                    strSqlString.Append("                         WHERE 1=1 " + "\n");
                    strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                           AND CM_KEY_1 = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                           AND LOT_TYPE = 'W'" + "\n");
                    strSqlString.Append("                           AND CM_KEY_2 = 'PROD' " + "\n");
                    strSqlString.Append("                           AND WORK_DATE =  '" + date + "'  " + "\n");
                }
                else
                {
                    //strSqlString.Append("                         UNION ALL " + "\n");
                    strSqlString.Append("                        SELECT MAT_ID, CM_KEY_3, (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) AS SHP_QTY_1 " + "\n");
                    strSqlString.Append("                          FROM RSUMFACMOV " + "\n");
                    strSqlString.Append("                         WHERE 1=1 " + "\n");
                    strSqlString.Append("                           AND WORK_DATE =  '" + date + "'  " + "\n");
                    strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                           AND CM_KEY_1 = 'HMKE1'" + "\n");
                }

                strSqlString.Append("                       ) " + "\n");
              
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                 WHERE CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) F" + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");                
                strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND A.MAT_ID = C.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND A.MAT_ID = D.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND A.MAT_ID = E.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND A.MAT_ID = F.MAT_ID(+) " + "\n");

                //상세 조회에 따른 SQL문 생성                        
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

                strSqlString.Append("       ) A " + "\n");
            }
            #endregion
            #region 월 계획
            else
            {
                if (lotType == false) // lotType 이 E Type이면 계획 안보여줌.
                {
                    strSqlString.Append("     , 0 " + "\n");
                    strSqlString.Append("     , 0 " + "\n");
                    strSqlString.Append("     , 0 " + "\n");
                }
                else // lotType이 P Type 또는 ALL 일 경우 계획 보여줌.
                {
                    if (ckbRev.Checked == false)
                    {
                        strSqlString.Append("     , ROUND(SUM(NVL(A.PLAN_QTY_TEST,0))/" + sKcpkValue + ", 1) " + "\n");
                        strSqlString.Append("     , 0 " + "\n");
                        strSqlString.Append("     , 0 " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("     , ROUND(SUM(NVL(A.ORI_PLAN,0))/" + sKcpkValue + ", 1) " + "\n");
                        strSqlString.Append("     , ROUND(SUM(NVL(A.PLAN_QTY_TEST,0))/" + sKcpkValue + ", 1) " + "\n");
                        strSqlString.Append("     , ROUND(SUM(NVL(A.PLAN_QTY_TEST,0))/" + sKcpkValue + ", 1) - ROUND(SUM(NVL(A.ORI_PLAN,0))/" + sKcpkValue + ", 1) " + "\n");
                    }
                }

                strSqlString.Append("     , ROUND(SUM(NVL(SHIP_MON,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(FGS_MONTH,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , CASE WHEN SUM(NVL(A.PLAN_QTY_TEST,0)) = 0 OR SUM(NVL(SHIP_MON,0)) = 0 THEN 0 " + "\n");
                strSqlString.Append("            ELSE ROUND((SUM(NVL(SHIP_MON,0))/ SUM(NVL(A.PLAN_QTY_TEST,0)))*100,1) " + "\n");
                strSqlString.Append("       END AS JINDO " + "\n");
                strSqlString.Append("     , ROUND(( SUM(NVL(A.PLAN_QTY_TEST,0)) - SUM(NVL(SHIP_MON,0)) )/" + sKcpkValue + ", 1) AS LACK_OUT       " + "\n");
                strSqlString.Append("     , ROUND(( SUM(NVL(A.PLAN_QTY_TEST,0)) - ( SUM(NVL(SHIP_MON,0)) + SUM(NVL(CUR_WIP,0)) ) )/" + sKcpkValue + ", 1) AS LACK_IN " + "\n");

                if (lblRemain2.Text != "0")
                {
                    strSqlString.Append("     , ROUND(( SUM(NVL(A.PLAN_QTY_TEST,0)) - SUM(NVL(SHIP_MON,0)) ) /" + Convert.ToUInt32(lblRemain2.Text) + "/" + sKcpkValue + ", 1) AS TARGET " + "\n");
                    if (Convert.ToUInt32(lblRemain2.Text) >= 7)
                    {
                        strSqlString.Append("     , ROUND(( SUM(NVL(A.PLAN_QTY_TEST,0)) - SUM(NVL(SHIP_MON,0)) ) /" + Convert.ToUInt32(lblRemain2.Text) + "/" + sKcpkValue + ", 1) * 7 AS TARGET " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("     , ROUND(( SUM(NVL(A.PLAN_QTY_TEST,0)) - SUM(NVL(SHIP_MON,0)) ) /" + sKcpkValue + ", 1) AS TARGET " + "\n");
                    }
                }
                else
                {
                    strSqlString.Append("     ,0 AS TARGET " + "\n");
                    strSqlString.Append("     ,0 AS TARGET " + "\n");
                }

                strSqlString.Append("     , ROUND(SUM(NVL(A.RCV_DAY,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.SHIP_DAY,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(RCV_FGS_TODAY,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(E.RTN_DAY,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(CUR_FGS,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND((SUM(NVL(B.V0,0)) + SUM(NVL(B.V1,0)) + SUM(NVL(B.V2,0)) + SUM(NVL(B.V3,0)) + SUM(NVL(B.V4,0)) + SUM(NVL(B.V5,0)) + SUM(NVL(B.V6,0)) ");
                strSqlString.Append("       + SUM(NVL(B.V7,0)) + SUM(NVL(B.V8,0)) + SUM(NVL(B.V9,0)) + SUM(NVL(B.V10,0)) + SUM(NVL(B.V11,0)) + SUM(NVL(B.V12,0))) /" + sKcpkValue + ", 1) AS TOTLA " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V12,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V11,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V10,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V9,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V8,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V7,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V6,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V5,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V4,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V3,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V2,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V1,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V0,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V13,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V14,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V15,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V16,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V17,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND((SUM(NVL(B.V0,0)) + SUM(NVL(B.V1,0)) + SUM(NVL(B.V2,0)) + SUM(NVL(B.V3,0)) + SUM(NVL(B.V4,0)) + SUM(NVL(B.V5,0)) + SUM(NVL(B.V6,0)) + SUM(NVL(B.V7,0)) + SUM(NVL(B.V8,0)) + SUM(NVL(B.V9,0)) ");
                strSqlString.Append("       + SUM(NVL(B.V10,0)) + SUM(NVL(B.V11,0)) + SUM(NVL(B.V12,0)) + SUM(NVL(CUR_FGS,0)) + SUM(NVL(B.V13,0)))/" + sKcpkValue + ",1) AS WIP_TOTAL" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.RCV_YESTERDAY,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(D.T_IN,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(D.T_OUT,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.SHIP_YESTERDAY,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(RCV_FGS_YESTERDAY,0))/" + sKcpkValue + ", 1) " + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT A.MAT_GRP_1, A.MAT_GRP_2, A.MAT_GRP_3, A.MAT_GRP_4, A.MAT_GRP_5, A.MAT_GRP_6, A.MAT_GRP_7, A.MAT_GRP_8, A.MAT_CMF_10, A.MAT_ID, A.MAT_CMF_7,BASE_MAT_ID " + "\n");

                if (ckbRev.Checked == false)
                {
                    strSqlString.Append("             , B.PLAN_QTY_TEST " + "\n");
                }
                else
                {
                    //김민우
                    strSqlString.Append("             , B.RESV_FIELD2 AS PLAN_QTY_TEST" + "\n");
                    strSqlString.Append("             , B.PLAN_QTY_TEST AS ORI_PLAN" + "\n");
                }

                strSqlString.Append("             , C.SHIP_MON " + "\n");
                strSqlString.Append("             , F.SHIP_DAY " + "\n");
                strSqlString.Append("             , C.SHIP_YESTERDAY " + "\n");
                strSqlString.Append("             , D.CUR_WIP " + "\n");
                strSqlString.Append("             , E.RCV_YESTERDAY " + "\n");
                strSqlString.Append("             , E.RCV_DAY " + "\n");
                strSqlString.Append("             , D.CUR_FGS " + "\n");
                strSqlString.Append("          FROM (          " + "\n");
                strSqlString.Append("                SELECT DISTINCT MAT_ID, MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, MAT_GRP_9, MAT_GRP_10, MAT_CMF_7, MAT_CMF_10, BASE_MAT_ID  " + "\n");
                strSqlString.Append("                  FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                 WHERE FACTORY = '" + cdvFactory.Text + "' " + "\n");
                //strSqlString.Append("                 WHERE FACTORY IN ('" + GlobalVariable.gsTestDefaultFactory + "', 'HMKE1') " + "\n");
                strSqlString.Append("                   AND DELETE_FLAG = ' ' " + "\n");
                strSqlString.Append("                   AND MAT_TYPE = 'FG' " + "\n");

                if (ckbCOB.Checked == true)
                    strSqlString.Append("           AND MAT_GRP_3 <> 'COB' " + "\n");

                strSqlString.Append("               ) A " + "\n");

                //월 계획 SLIS 제품은 MP계획과 동일하게
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT FACTORY,MAT_ID,PLAN_QTY_TEST,PLAN_MONTH, RESV_FIELD2  " + "\n");
                strSqlString.Append("                  FROM (  " + "\n");
                strSqlString.Append("                        SELECT FACTORY,MAT_ID,SUM(PLAN_QTY_TEST) AS PLAN_QTY_TEST,PLAN_MONTH, SUM(RESV_FIELD2) AS RESV_FIELD2  " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_TEST) AS PLAN_QTY_TEST, PLAN_MONTH, SUM(TO_NUMBER(DECODE(RESV_FIELD2,' ',0,RESV_FIELD2))) AS RESV_FIELD2 " + "\n"); //김민우
                strSqlString.Append("                                  FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                //strSqlString.Append("                                   AND FACTORY IN ('" + GlobalVariable.gsTestDefaultFactory + "', 'HMKE1') " + "\n");
                strSqlString.Append("                                 GROUP BY FACTORY, MAT_ID, PLAN_MONTH " + "\n");

                // 2013-06-07-임종우 : SE 제품 월 계획 REV 값을 기존 OMS 데이터에서 월 계획 UPLOAD 데이터로 변경 함 (김은석 요청)
                //strSqlString.Append("                                UNION ALL " + "\n");
                //strSqlString.Append("                                SELECT A.FACTORY, A.MAT_ID, 0 AS PLAN_QTY_TEST , '" + month + "' AS PLAN_MONTH, SUM(A.PLAN_QTY) AS RESV_FIELD2  " + "\n");
                //strSqlString.Append("                                  FROM ( " + "\n");

                //// 월계획 금일이면 기존대로
                //if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                //{
                //    strSqlString.Append("                                        SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                //    strSqlString.Append("                                          FROM CWIPPLNDAY " + "\n");
                //    strSqlString.Append("                                         WHERE 1=1 " + "\n");
                //    strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                //    strSqlString.Append("                                           AND PLAN_DAY BETWEEN '" + start_date + "' AND '" + last_day + "'\n");
                //    strSqlString.Append("                                           AND IN_OUT_FLAG = 'IN' " + "\n");
                //    strSqlString.Append("                                           AND CLASS = 'SLIS' " + "\n");
                //    strSqlString.Append("                                         GROUP BY FACTORY, MAT_ID " + "\n");
                //}
                //else// 금일이 아니면 스냅샷 떠놓은 테이블에서 가져옴.
                //{
                //    strSqlString.Append("                                        SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                //    strSqlString.Append("                                          FROM CWIPPLNSNP@RPTTOMES " + "\n");
                //    strSqlString.Append("                                         WHERE 1=1 " + "\n");
                //    strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                //    strSqlString.Append("                                           AND SNAPSHOT_DAY = '" + cdvDate.SelectedValue() + "'" + "\n");
                //    strSqlString.Append("                                           AND PLAN_DAY BETWEEN '" + start_date + "' AND '" + last_day + "'\n");
                //    strSqlString.Append("                                           AND IN_OUT_FLAG = 'IN' " + "\n");
                //    strSqlString.Append("                                           AND CLASS = 'SLIS' " + "\n");
                //    strSqlString.Append("                                         GROUP BY FACTORY, MAT_ID " + "\n");
                //}

                //strSqlString.Append("                                         UNION ALL " + "\n");
                //strSqlString.Append("                                        SELECT FACTORY, MAT_ID " + "\n");
                //strSqlString.Append("                                             , SUM(SHP_QTY_1) PLAN_QTY " + "\n");
                //strSqlString.Append("                                          FROM VSUMWIPSHP " + "\n");
                //strSqlString.Append("                                         WHERE 1=1 " + "\n");
                //strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                ////strSqlString.Append("                                         AND CM_KEY_1 = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                //strSqlString.Append("                                           AND CM_KEY_2 = 'PROD' " + "\n");
                //strSqlString.Append("                                           AND CM_KEY_3 LIKE 'P%' " + "\n");
                //strSqlString.Append("                                           AND LOT_TYPE = 'W' " + "\n");
                //strSqlString.Append("                                           AND WORK_DATE BETWEEN '" + start_date + "' AND '" + Lastweek_lastday + "'\n");
                //strSqlString.Append("                                         GROUP BY FACTORY, MAT_ID " + "\n");
                //strSqlString.Append("                                       ) A" + "\n");
                //strSqlString.Append("                                     , MWIPMATDEF B " + "\n");
                //strSqlString.Append("                                 WHERE 1=1  " + "\n");
                //strSqlString.Append("                                   AND A.FACTORY = B.FACTORY " + "\n");
                //strSqlString.Append("                                   AND A.MAT_ID = B.MAT_ID " + "\n");
                //strSqlString.Append("                                   AND B.MAT_GRP_1 = 'SE' " + "\n");
                //strSqlString.Append("                                   AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
                //strSqlString.Append("                                 GROUP BY A.FACTORY, A.MAT_ID " + "\n");
                strSqlString.Append("                               ) " + "\n");
                strSqlString.Append("                         GROUP BY FACTORY, MAT_ID,PLAN_MONTH  " + "\n");
                strSqlString.Append("                       ) " + "\n");
                strSqlString.Append("               ) B " + "\n");
                strSqlString.Append("             , ( " + "\n");

                strSqlString.Append("                SELECT MAT_ID " + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "' THEN SHP_QTY_1" + "\n");
                strSqlString.Append("                                ELSE 0 END) SHIP_MON " + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE = '" + yesterday + "' THEN SHP_QTY_1" + "\n");
                strSqlString.Append("                                ELSE 0 END) SHIP_YESTERDAY " + "\n");
                strSqlString.Append("                  FROM ( " + "\n");

                if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
                {
                    strSqlString.Append("                        SELECT MAT_ID, WORK_DATE, CM_KEY_3, SHP_QTY_1 " + "\n");
                    strSqlString.Append("                          FROM VSUMWIPSHP " + "\n");
                    strSqlString.Append("                         WHERE 1=1 " + "\n");
                    strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                           AND CM_KEY_1 = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                           AND LOT_TYPE = 'W'" + "\n");
                    strSqlString.Append("                           AND CM_KEY_2 = 'PROD' " + "\n");
                    strSqlString.Append("                           AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + date + "'" + "\n");
                }
                else
                {
                    //strSqlString.Append("                        UNION ALL " + "\n");
                    strSqlString.Append("                        SELECT MAT_ID, WORK_DATE, CM_KEY_3, (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) AS SHP_QTY_1 " + "\n");
                    strSqlString.Append("                          FROM RSUMFACMOV " + "\n");
                    strSqlString.Append("                         WHERE 1=1 " + "\n");
                    strSqlString.Append("                           AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + date + "'" + "\n");
                    strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                           AND CM_KEY_1 = 'HMKE1'" + "\n");
                }

                strSqlString.Append("                       ) " + "\n");
               
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                 WHERE CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }
                
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) C " + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    strSqlString.Append("             , ( " + "\n");
                    strSqlString.Append("                SELECT MAT_ID " + "\n");
                    strSqlString.Append("                     , SUM(CASE WHEN FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' THEN QTY_1 " + "\n");
                    strSqlString.Append("                                ELSE 0 END) AS CUR_WIP   " + "\n");
                    strSqlString.Append("                     , SUM(CASE WHEN FACTORY = 'FGS' THEN QTY_1 " + "\n");
                    strSqlString.Append("                                ELSE 0 END) AS CUR_FGS " + "\n");
                    strSqlString.Append("                  FROM RWIPLOTSTS  " + "\n");
                    strSqlString.Append("                 WHERE FACTORY IN ('" + cdvFactory.Text + "', 'FGS') " + "\n");
                    //strSqlString.Append("                 WHERE FACTORY  IN ('" + GlobalVariable.gsTestDefaultFactory + "', 'FGS') " + "\n");
                    strSqlString.Append("                   AND MAT_VER = 1 " + "\n");
                    strSqlString.Append("                   AND LOT_DEL_FLAG = ' ' " + "\n");
                    strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND HOLD_CODE <> 'H72' " + "\n");
                    
                    if (cdvLotType.Text != "ALL")
                    {
                        strSqlString.Append("                   AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                    }

                    strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
                    strSqlString.Append("               ) D " + "\n");
                }
                else
                {
                    strSqlString.Append("             , ( " + "\n");
                    strSqlString.Append("                SELECT MAT_ID " + "\n");
                    strSqlString.Append("                     , SUM(CASE WHEN FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' THEN QTY_1 " + "\n");
                    strSqlString.Append("                                ELSE 0 END) AS CUR_WIP   " + "\n");
                    strSqlString.Append("                     , SUM(CASE WHEN FACTORY = 'FGS' THEN QTY_1 " + "\n");
                    strSqlString.Append("                                ELSE 0 END) AS CUR_FGS " + "\n");
                    strSqlString.Append("                  FROM RWIPLOTSTS_BOH  " + "\n");
                    strSqlString.Append("                 WHERE FACTORY IN ('" + cdvFactory.Text + "', 'FGS') " + "\n");
                    //strSqlString.Append("                 WHERE FACTORY IN ('" + GlobalVariable.gsTestDefaultFactory + "', 'FGS') " + "\n");
                    strSqlString.Append("                   AND MAT_VER = 1  " + "\n");
                    strSqlString.Append("                   AND LOT_DEL_FLAG = ' ' " + "\n");
                    strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND HOLD_CODE <> 'H72' " + "\n");
                    strSqlString.Append("                   AND CUTOFF_DT = '" + cdvDate.SelectedValue() + "22'" + "\n");
                   
                    if (cdvLotType.Text != "ALL")
                    {
                        strSqlString.Append("                   AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                    }

                    strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
                    strSqlString.Append("               ) D " + "\n");
                }

                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT MAT_ID " + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE = '" + yesterday + "' THEN NVL(S1_OPER_IN_QTY_1,0) + NVL(S2_OPER_IN_QTY_1,0) + NVL(S3_OPER_IN_QTY_1,0)" + "\n");
                strSqlString.Append("                                ELSE 0 " + "\n");
                strSqlString.Append("                           END) AS RCV_YESTERDAY" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE = '" + date + "' THEN NVL(S1_OPER_IN_QTY_1,0) + NVL(S2_OPER_IN_QTY_1,0) + NVL(S3_OPER_IN_QTY_1,0)" + "\n");
                strSqlString.Append("                                ELSE 0 " + "\n");
                strSqlString.Append("                           END) RCV_DAY " + "\n");
                strSqlString.Append("                  FROM RSUMWIPMOV " + "\n");
                strSqlString.Append("                 WHERE 1 = 1 " + "\n");
                strSqlString.Append("                   AND MAT_VER=1  " + "\n");
                strSqlString.Append("                   AND FACTORY  = '" + cdvFactory.Text + "' " + "\n");
                //strSqlString.Append("                   AND FACTORY  = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND OPER IN ('T0000')" + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                   AND CM_KEY_2 = 'PROD'" + "\n");
                
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.AppendFormat("                   AND WORK_DATE BETWEEN '{0}' AND '{1}'" + "\n", yesterday, date);
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) E" + "\n");

                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT MAT_ID " + "\n");
                strSqlString.Append("                     , SUM(SHP_QTY_1) SHIP_DAY " + "\n");
                strSqlString.Append("                  FROM ( " + "\n");

                if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
                {
                    strSqlString.Append("                        SELECT MAT_ID, CM_KEY_3, SHP_QTY_1 " + "\n");
                    strSqlString.Append("                          FROM VSUMWIPSHP_ONLY " + "\n");
                    strSqlString.Append("                         WHERE 1=1 " + "\n");
                    strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                           AND CM_KEY_1 = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                           AND LOT_TYPE = 'W'" + "\n");
                    strSqlString.Append("                           AND CM_KEY_2 = 'PROD' " + "\n");
                    strSqlString.Append("                           AND WORK_DATE =  '" + date + "'  " + "\n");
                }
                else
                {
                    //strSqlString.Append("                         UNION ALL " + "\n");
                    strSqlString.Append("                        SELECT MAT_ID, CM_KEY_3, (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) AS SHP_QTY_1 " + "\n");
                    strSqlString.Append("                          FROM RSUMFACMOV " + "\n");
                    strSqlString.Append("                         WHERE 1=1 " + "\n");
                    strSqlString.Append("                           AND WORK_DATE =  '" + date + "'  " + "\n");
                    strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                           AND CM_KEY_1 = 'HMKE1'" + "\n");
                }

                strSqlString.Append("                       ) " + "\n");
                
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                 WHERE CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) F" + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");
                strSqlString.AppendFormat("           AND B.PLAN_MONTH(+) = '{0}' " + "\n", date.Substring(0, 6));
                strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND A.MAT_ID = C.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND A.MAT_ID = D.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND A.MAT_ID = E.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND A.MAT_ID = F.MAT_ID(+) " + "\n");

                //상세 조회에 따른 SQL문 생성                        
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

                strSqlString.Append("       ) A " + "\n");
            }
            #endregion

            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT A.MAT_ID  " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.OPER_GRP_1 = 'HMK3T' AND HOLD_CODE <> 'H72' THEN QTY" + "\n");            
            strSqlString.Append("                        ELSE 0 END) V0 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN LOT_STATUS = 'WAIT' AND HOLD_FLAG = ' ' AND OPER_GRP_1 = 'TEST' THEN QTY " + "\n");            
            strSqlString.Append("                        ELSE 0 END) V1" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN LOT_STATUS = 'PROC' AND HOLD_FLAG = ' ' AND OPER_GRP_1 = 'TEST' THEN QTY " + "\n");            
            strSqlString.Append("                        ELSE 0 END) V2 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN HOLD_FLAG = 'Y' AND OPER_GRP_1 = 'TEST' AND HOLD_CODE <> 'H72' THEN QTY " + "\n");            
            strSqlString.Append("                        ELSE 0 END) V3 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.OPER_GRP_1 = 'QA1' AND HOLD_CODE <> 'H72' THEN QTY " + "\n");            
            strSqlString.Append("                        ELSE 0 END) V4  " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.OPER_GRP_1 = 'CAS' AND HOLD_CODE <> 'H72' THEN QTY " + "\n");            
            strSqlString.Append("                        ELSE 0 END) V5  " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.OPER_GRP_1 = 'OS' AND HOLD_CODE <> 'H72' THEN QTY " + "\n");            
            strSqlString.Append("                        ELSE 0 END) V6  " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.OPER_GRP_1 = 'QA2' AND HOLD_CODE <> 'H72' THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) V7  " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.OPER_GRP_1 = 'BAKE' AND HOLD_CODE <> 'H72' THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) V8  " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.OPER_GRP_1 = 'V/I' AND HOLD_CODE <> 'H72' THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) V9  " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.OPER_GRP_1 = 'TnR' AND HOLD_CODE <> 'H72' THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) V10 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.OPER_GRP_1 = 'P/K' AND HOLD_CODE <> 'H72' THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) V11  " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.OPER_GRP_1 = 'HMK4T' AND HOLD_CODE <> 'H72' THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) V12 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND A.OPER_CMF_3 in( 'HMK3', 'FINISH', 'MOLD', 'BOND' ) THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) V13 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND A.OPER_CMF_3 = 'HMK3' THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) V14 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND A.OPER_CMF_3 = 'FINISH' THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) V15 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND A.OPER_CMF_3 = 'MOLD' THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) V16 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND A.OPER_CMF_3 = 'BOND' THEN QTY " + "\n");
            strSqlString.Append("                        ELSE 0 END) V17          " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT A.MAT_ID, A.FACTORY, B.OPER_GRP_1, B.OPER_GRP_7, B.OPER_CMF_3, SUM(A.QTY_1)AS QTY, HOLD_FLAG, HOLD_CODE, LOT_STATUS " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("                  FROM RWIPLOTSTS A " + "\n");
            }
            else
            {
                strSqlString.Append("                  FROM RWIPLOTSTS_BOH A " + "\n");
            }

            strSqlString.Append("                     , MWIPOPRDEF B     " + "\n");
            strSqlString.Append("                 WHERE A.FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + cdvFactory.Text + "') " + "\n");
            //strSqlString.Append("                 WHERE A.FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY  " + "\n");
            strSqlString.Append("                   AND A.OPER = B.OPER  " + "\n");
            strSqlString.Append("                   AND A.MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND A.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
            
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            if (DateTime.Now.ToString("yyyyMMdd") != cdvDate.SelectedValue())
            {
                strSqlString.Append("                   AND A.CUTOFF_DT = '" + cdvDate.SelectedValue() + "22'" + "\n");
            }

            strSqlString.Append("                 GROUP BY A.MAT_ID, A.FACTORY, B.OPER_GRP_1, B.OPER_GRP_7, B.OPER_CMF_3, HOLD_FLAG, HOLD_CODE, LOT_STATUS  " + "\n");
            strSqlString.Append("               ) A " + "\n");            
            strSqlString.Append("         GROUP BY A.MAT_ID " + "\n");
            strSqlString.Append("       ) B " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE = '" + yesterday + "' THEN S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1" + "\n");            
            strSqlString.Append("                        ELSE 0 END) RCV_FGS_YESTERDAY         " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE = '" + date + "' THEN S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1" + "\n");            
            strSqlString.Append("                        ELSE 0 END) RCV_FGS_TODAY   " + "\n");
            strSqlString.AppendFormat("             , SUM(CASE WHEN WORK_DATE BETWEEN '{0}' AND '{1}' THEN S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1" + "\n", start_date, date);            
            strSqlString.Append("                        ELSE 0 END) FGS_MONTH " + "\n");
            strSqlString.Append("          FROM RSUMFACMOV " + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND CM_KEY_1 = 'FGS'" + "\n");
            strSqlString.Append("           AND CM_KEY_2 = 'PROD'" + "\n");
            strSqlString.Append("           AND LOT_TYPE = 'W'" + "\n");
            
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("           AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("           AND FACTORY IN ('CUSTOMER', 'FGS') " + "\n");
            strSqlString.Append("           AND MAT_VER = 1" + "\n");
            strSqlString.Append("           AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + date + "'" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) C " + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("             , SUM( S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1 ) AS T_IN" + "\n");
            strSqlString.Append("             , SUM( S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ) AS T_OUT" + "\n");
            strSqlString.Append("          FROM RSUMWIPMOV" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            //strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND MAT_VER = 1" + "\n");            
            strSqlString.Append("           AND OPER IN ('T0100', 'T0400')" + "\n");
            strSqlString.Append("           AND CM_KEY_2 = 'PROD'   " + "\n");
            
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("           AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("           AND WORK_DATE = '" + yesterday + "'" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("       ) D" + "\n");

            strSqlString.Append("     , (" + "\n");
            if (DateTime.Now.ToString("yyyyMMdd") != cdvDate.SelectedValue())   // 과거 시점일때 RTN_DAY
            {
                strSqlString.Append("        SELECT MAT_ID " + "\n");
                strSqlString.Append("             , SUM(S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1+S4_FAC_IN_QTY_1) AS RTN_DAY    " + "\n");
                strSqlString.Append("          FROM RSUMFACMOV " + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                //strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND FACTORY = CM_KEY_1 " + "\n");
                strSqlString.Append("           AND OPER = 'TZ010' " + "\n");
                strSqlString.Append("           AND CM_KEY_1 <> 'FGS'" + "\n");
                strSqlString.Append("           AND WORK_DATE = '" + date + "'    " + "\n");
                
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("           AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            }
            else        // 금일 시점의 RTN_DAY
            {
                strSqlString.Append("        SELECT MAT_ID" + "\n");
                strSqlString.Append("             , SUM(QTY_1) AS RTN_DAY" + "\n");
                strSqlString.Append("          FROM RWIPLOTHIS" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("           AND OLD_FACTORY = '" + cdvFactory.Text + "'" + "\n");
                //strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                //strSqlString.Append("           AND OLD_FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("           AND OWNER_CODE = 'PROD'" + "\n");
                strSqlString.Append("           AND OPER = 'TZ010' " + "\n");
                strSqlString.Append("           AND CREATE_CODE = 'RETN'   " + "\n");
                strSqlString.AppendFormat("           AND TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", yesterday + "220000", date + "215959");
                strSqlString.Append("           AND HIST_DEL_FLAG = ' '" + "\n");
             
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("           AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            }

            strSqlString.Append("       ) E " + "\n");
            strSqlString.Append(" WHERE 1 = 1 " + "\n");
            strSqlString.Append("   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND A.MAT_ID = C.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND A.MAT_ID = D.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND A.MAT_ID = E.MAT_ID(+)" + "\n");

            strSqlString.AppendFormat(" GROUP BY {0}" + "\n", QueryCond1);

            strSqlString.Append("HAVING (" + "\n");

            if (ckbWeek.Checked == true)
            {
                strSqlString.Append("        SUM(NVL(A.WEEK1_PLAN,0)) +  " + "\n");
                strSqlString.Append("        SUM(NVL(A.WEEK2_PLAN,0)) +  " + "\n");
                strSqlString.Append("        SUM(NVL(A.SHIP_WEEK,0)) + " + "\n");
            }
            else
            {
                strSqlString.Append("        SUM(NVL(A.PLAN_QTY_TEST,0)) + " + "\n");
                strSqlString.Append("        SUM(NVL(SHIP_MON, 0)) +" + "\n");
                strSqlString.Append("        SUM(NVL(FGS_MONTH, 0)) + " + "\n");
                strSqlString.Append("        SUM(NVL(CUR_FGS, 0)) +" + "\n");
            }
                        
            strSqlString.Append("        SUM(NVL(A.RCV_DAY, 0)) + " + "\n");
            strSqlString.Append("        SUM(NVL(A.SHIP_DAY, 0)) + " + "\n");
            strSqlString.Append("        SUM(NVL(RCV_FGS_TODAY, 0)) + " + "\n");
            strSqlString.Append("        SUM(NVL(E.RTN_DAY, 0)) + " + "\n");
            strSqlString.Append("        SUM (NVL(B.V0,0) + NVL(B.V1,0) + NVL(B.V2,0) + NVL(B.V3,0) + NVL(B.V4,0) + NVL(B.V5,0) + NVL(B.V6,0) + NVL(B.V7,0) + NVL(B.V8,0) +" + "\n");
            strSqlString.Append("        NVL(B.V9,0) + NVL(B.V10,0) + NVL(B.V11,0) + NVL(B.V12,0) +  NVL(B.V13,0) ) +" + "\n");            
            strSqlString.Append("        SUM(NVL(A.RCV_YESTERDAY, 0)) +" + "\n");
            strSqlString.Append("        SUM(NVL(D.T_IN, 0)) +" + "\n");
            strSqlString.Append("        SUM(NVL(D.T_OUT, 0)) + " + "\n");
            strSqlString.Append("        SUM(NVL(RCV_FGS_YESTERDAY, 0)) + " + "\n");
            strSqlString.Append("        SUM(NVL(A.SHIP_YESTERDAY, 0))" + "\n");
            strSqlString.Append("       )  > 0" + "\n");


            strSqlString.AppendFormat(" ORDER BY {0}" + "\n", QueryCond1);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private string MakeSqlString1()
        {
            StringBuilder strSqlString = new StringBuilder();

            string getDate = cdvDate.SelectedValue();

            strSqlString.Append("        SELECT  " + "\n");
            strSqlString.Append("            LAST_DAY('" + getDate + "') AS \"SYSLSI\", " + "\n");
            strSqlString.Append("            LAST_DAY('" + getDate + "') AS \"MEMO\",  " + "\n");
            //strSqlString.Append("            LAST_DAY('" + getDate + "') - TO_DATE('" + getDate + "') AS REMAIN1,  " + "\n");
            //strSqlString.Append("            LAST_DAY('" + getDate + "') - TO_DATE('" + getDate + "')  AS REMAIN2,  " + "\n");

            // 금일 포함하여 잔여일 계산
            strSqlString.Append("            LAST_DAY('" + getDate + "') - TO_DATE('" + getDate + "') +1 AS REMAIN1,  " + "\n");
            strSqlString.Append("            LAST_DAY('" + getDate + "') - TO_DATE('" + getDate + "') + 1 AS REMAIN2,  " + "\n");

            strSqlString.Append("            SYSDATE AS TODAY  " + "\n");
            strSqlString.Append("        FROM DUAL " + "\n");

            return strSqlString.ToString();
        }

        // 지난 주차의 마지막일 가져오기
        private void MakeSqlString2(string year, string date)
        {
            DataTable dt = null;
            StringBuilder sqlString = new StringBuilder();

            sqlString.Append("SELECT LAST_WEEK_END, THIS_WEEK_START, THIS_WEEK_END, THIS_WEEK " + "\n");
            sqlString.Append("     , (SELECT PLAN_YEAR||PLAN_WEEK FROM MWIPCALDEF WHERE CALENDAR_ID = 'SE' AND SYS_DATE = A.NEXT_WEEK_START) AS NEXT_WEEK " + "\n");
            sqlString.Append("  FROM (" + "\n");
            sqlString.Append("        SELECT TO_CHAR(TO_DATE(MIN(SYS_DATE), 'YYYYMMDD')-1, 'YYYYMMDD') AS LAST_WEEK_END " + "\n");
            sqlString.Append("             , MIN(SYS_DATE) AS THIS_WEEK_START" + "\n");
            sqlString.Append("             , MAX(SYS_DATE) AS THIS_WEEK_END" + "\n");
            sqlString.Append("             , MAX(PLAN_YEAR||PLAN_WEEK) AS THIS_WEEK " + "\n");
            sqlString.Append("             , TO_CHAR(TO_DATE(MAX(SYS_DATE), 'YYYYMMDD')+1, 'YYYYMMDD') AS NEXT_WEEK_START  " + "\n");
            sqlString.Append("          FROM MWIPCALDEF " + "\n");
            sqlString.Append("         WHERE 1=1" + "\n");
            sqlString.Append("           AND CALENDAR_ID = 'SE'" + "\n");
            sqlString.Append("           AND PLAN_YEAR = '" + year + "'\n");
            sqlString.Append("           AND PLAN_WEEK = (" + "\n");
            sqlString.Append("                            SELECT PLAN_WEEK " + "\n");
            sqlString.Append("                              FROM MWIPCALDEF " + "\n");
            sqlString.Append("                             WHERE 1=1 " + "\n");
            sqlString.Append("                               AND CALENDAR_ID='SE' " + "\n");
            sqlString.Append("                               AND SYS_DATE= '" + date + "'" + "\n");
            sqlString.Append("                           )" + "\n");
            sqlString.Append("       ) A" + "\n");

            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", sqlString.ToString());
            Lastweek_lastday = dt.Rows[0][0].ToString();
            Thisweek_startday = dt.Rows[0][1].ToString();
            Thisweek_lastday = dt.Rows[0][2].ToString();
            Thisweek = dt.Rows[0][3].ToString();
            Nextweek = dt.Rows[0][4].ToString();
        }

        #endregion


        #region EVENT 처리
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            DataTable dt2 = null;

            if (CheckField() == false) return;

            GridColumnInit();
            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());
                LabelTextChange(dt2);
                dt2.Dispose();

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

                //표구성에따른 항목 Display
                //spdData.RPT_ColumnConfigFromTable(btnSort);
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 11, null, null, btnSort);

                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 1, true, Align.Center, VerticalAlign.Center);

                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 2, null, null);
                //spdData.Sheets[0].FrozenColumnCount = 10;

                //spdData.RPT_AutoFit(false); // 2011-09-01-김민우: 이 아이 넣으면 컬럼 visible이 안 먹는다..

                #region 진도율 Total 구하기
                // YIELD 부분의  TOTAL값 및 SUB TOTAL을 계산하지 않고 직접 계산 
                string subtotal = null;

                for (int i = 0; i < spdData.ActiveSheet.Columns.Count; i++)
                {
                    if (spdData.ActiveSheet.Columns[i].Label == "Progress rate")
                    {

                        if (ckbWeek.Checked == true)
                        {
                            if (Convert.ToInt32(spdData.ActiveSheet.Cells[0, i - 1].Value) == 0 || Convert.ToInt32(spdData.ActiveSheet.Cells[0, i - 2].Value) == 0)
                            {
                                spdData.ActiveSheet.Cells[0, i].Value = 0;
                            }
                            else
                            {
                                spdData.ActiveSheet.Cells[0, i].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 1].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 2].Value)) * 100).ToString();
                            }

                            for (int j = 0; j < spdData.ActiveSheet.Rows.Count; j++)
                            {
                                for (int k = 0; k < sub + 1; k++)
                                {
                                    if (spdData.ActiveSheet.Cells[j, k].Value != null)
                                        subtotal = spdData.ActiveSheet.Cells[j, k].Value.ToString();

                                    subtotal.Trim();
                                    if (subtotal.Length > 5)
                                    {
                                        if (subtotal.Substring(subtotal.Length - 5, 5) == "Total")
                                        {
                                            if (Convert.ToInt32(spdData.ActiveSheet.Cells[j, i - 2].Value) != 0)
                                            {
                                                spdData.ActiveSheet.Cells[j, i].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[j, i - 1].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[j, i - 2].Value)) * 100).ToString();
                                            }
                                            else
                                            {
                                                spdData.ActiveSheet.Cells[j, i].Value = 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(spdData.ActiveSheet.Cells[0, i - 4].Value) == 0 || Convert.ToInt32(spdData.ActiveSheet.Cells[0, i - 2].Value) == 0)
                            {
                                spdData.ActiveSheet.Cells[0, i].Value = 0;
                            }
                            else
                            {
                                spdData.ActiveSheet.Cells[0, i].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 2].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 4].Value)) * 100).ToString();
                            }

                            for (int j = 0; j < spdData.ActiveSheet.Rows.Count; j++)
                            {
                                for (int k = 0; k < sub + 1; k++)
                                {
                                    if (spdData.ActiveSheet.Cells[j, k].Value != null)
                                        subtotal = spdData.ActiveSheet.Cells[j, k].Value.ToString();

                                    subtotal.Trim();
                                    if (subtotal.Length > 5)
                                    {
                                        if (subtotal.Substring(subtotal.Length - 5, 5) == "Total")
                                        {
                                            if (Convert.ToInt32(spdData.ActiveSheet.Cells[j, i - 4].Value) != 0)
                                            {
                                                spdData.ActiveSheet.Cells[j, i].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[j, i - 2].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[j, i - 4].Value)) * 100).ToString();
                                            }
                                            else
                                            {
                                                spdData.ActiveSheet.Cells[j, i].Value = 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

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
                Condition.AppendFormat("기준일자: {0}        Lot Type: {1} " + "\n", cdvDate.Text, cdvLotType.Text);
                Condition.AppendFormat("금일실적기준: {0}    SYSLSI: {1}     월마감: {2}     진도율: {3}    잔여일수: " + lblRemain.Text.ToString() + "     ", lblToday.Text.ToString(), lblSyslsi.Text.ToString(), lblMagam.Text.ToString(), lblJindo.Text.ToString());
                Condition.Append(" ||   ");
                Condition.AppendFormat("전일실적기준: {0}    MEMO:   {1}     월마감: {2}     진도율: {3}    잔여일수: " + lblRemain2.Text.ToString(), lblYesterday.Text.ToString(), lblMemo.Text.ToString(), lblMagam2.Text.ToString(), lblJindo2.Text.ToString());

                ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);
                //spdData.ExportExcel();           
            }
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }

        /// <summary>
        /// 7. 상단 Lebel 표시
        /// </summary>
        private void LabelTextChange(DataTable dt)
        {
            string getYesterday = cdvDate.Value.AddDays(-1).ToString("yyyy-MM-dd");
            string getDate = cdvDate.Value.ToString("yyyy-MM-dd");
            string getStartDate = cdvDate.Value.ToString("yyyyMM") + "01";
            string strDate = cdvDate.Value.ToString("yyyyMMdd");

            // TEST 진도는 LSI,MEMORY 모두 해당월 말일 기준으로 현재일 계산함.
            string magam = dt.Rows[0][1].ToString().Substring(8, 2);
            //string today = dt.Rows[0][4].ToString().Substring(8, 2);
            string selectday = strDate.Substring(6, 2);
            double jindo = (Convert.ToDouble(selectday)) / Convert.ToDouble(magam) * 100;
            //double jindo = (Convert.ToDouble(selectday) - 1) / Convert.ToDouble(magam) * 100;

            // 진도율 소수점 1째자리 까지 표시 (2009.08.17 임종우)
            Decimal jindoPer = Math.Round(Convert.ToDecimal(jindo), 1);
            //int jindoPer = Convert.ToInt32(jindo);

            lblYesterday.Text = getYesterday + " 22:00";

            //금일조회일 경우 조회조건은 REALTIME
            if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
            {
                strDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm");
                lblToday.Text = strDate;

            }
            else
            {
                // 금일 실적 기준 00:00 -> 22:00 으로 변경 (2009.08.17 임종우)
                lblToday.Text = getDate + " 22:00";
            }

            lblSyslsi.Text = getStartDate;
            lblMemo.Text = getStartDate;
            lblMagam.Text = dt.Rows[0][0].ToString().Substring(0, 10);
            lblMagam2.Text = dt.Rows[0][1].ToString().Substring(0, 10);
            lblJindo.Text = jindoPer.ToString() + "%";
            lblJindo2.Text = jindoPer.ToString() + "%";
            lblRemain.Text = dt.Rows[0][2].ToString();
            lblRemain2.Text = dt.Rows[0][3].ToString();
        }

        #endregion

        private void cdvFactory_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT FACTORY AS Code, FAC_DESC AS Data" + "\n";
            strQuery += "  FROM MWIPFACDEF " + "\n";
            strQuery += " WHERE FACTORY IN ('" + GlobalVariable.gsTestDefaultFactory + "', 'HMKE1') " + "\n";
            strQuery += "   AND FAC_GRP_5 = 'Y' " + "\n";
            strQuery += " ORDER BY FAC_GRP_4 DESC" + "\n";

            cdvFactory.sDynamicQuery = strQuery;
        }


    }
}
