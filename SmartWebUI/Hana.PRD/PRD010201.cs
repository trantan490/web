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
    public partial class PRD010201 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        GlobalVariable.FindWeek FindWeek = new GlobalVariable.FindWeek();
        /// <summary>
        /// 클  래  스: PRD010201<br/>
        /// 클래스요약: ASSY 진도 관리<br/>
        /// 작  성  자: 미라콤 김민규<br/>
        /// 최초작성일: 2008-12-05<br/>
        /// 상세  설명: ASSY 진도 관리<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// 2011-08-11-배수민 : Chip Q'ty, Wafer slice Q'ty 2가지 조건으로 검색할수 있도록 추가 요청 (생산관리 김동인D)
        /// 2011-09-19-김민우 : 월계획 Rev 조건 추가
        /// 2011-11-21-임종우 : 월 계획 제품 중복 오류 수정
        /// 2011-12-26-임종우 : MWIPCALDEF 의 작년,올해 마지막 주차 겹치는 에러 발생으로 SYS_YEAR -> PLAN_YEAR 으로 변경
        /// 2012-08-13-임종우 : 주간 조회 기능 추가 (임태성 요청)
        /// 2013-01-07-김민우 : SAP_CODE 추가, BASE_MAT_ID만 (김동인 요청)
        /// 2013-01-17-임종우 : 주차 정보 가져오는 로직 CmnFunction.GetWeekInfo 으로 변경
        /// 2013-01-31-임종우 : 주간 계획 테이블 변경 CWIPPLNWEK -> CWIPPLNWEK_N, 주차 기준 월~일(SE)에서 토~금(OTD) 로 변경 (임태성 요청)
        /// 2013-06-07-임종우 : SE 제품 월 계획 REV 값을 기존 OMS 데이터에서 월 계획 UPLOAD 데이터로 변경 함 (김은석 요청)
        /// 2013-08-26-임종우 : 계획 테이블 변경 CWIPPLNWEK_N -> RWIPPLNWEK
        /// 2013-10-14-김민우 : LOT TYPE ALL, P%, E% 구분으로변경
        /// 2019-06-25-임종우 : PVI 재공 - QC_GATE 재공 공정그룹 추가 (임태성차장 요청)
        /// </summary>
        public PRD010201()
        {
            InitializeComponent();                                   
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();            
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
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
                if (ckbWeek.Checked == false)
                {
                    #region 월계획, kpcs 체크 안함
                    if (ckbKpcs.Checked == false)
                    {
                        spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 200);
                        spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 200);
                        spdData.RPT_AddBasicColumn("Cust Device", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("SAP_CODE", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);

                        spdData.RPT_AddBasicColumn("monthly", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        if (ckbRev.Checked == false)
                        {
                            spdData.RPT_AddBasicColumn("Monthly plan", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                            spdData.RPT_AddBasicColumn("Monthly Plan Rev", 1, 13, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                            spdData.RPT_AddBasicColumn("Monthly plan difference", 1, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn("Monthly plan", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                            spdData.RPT_AddBasicColumn("Monthly Plan Rev", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                            spdData.RPT_AddBasicColumn("Monthly plan difference", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        }
                        spdData.RPT_AddBasicColumn("A/O", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Progress rate", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("Month shortage (A/0)", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 90);
                        spdData.RPT_AddBasicColumn("Month shortage (IN)", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 12, 8);

                        spdData.RPT_AddBasicColumn("a daily goal", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("A/O", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);


                        spdData.RPT_AddBasicColumn("the day's performance", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("일AC-T", 1, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("R-OUT", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 21, 3);

                        spdData.RPT_AddBasicColumn("ASSY WIP", 0, 24, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TOTAL", 1, 24, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("S/P", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMKA3", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 24, 18);

                        spdData.RPT_AddBasicColumn("The previous day's performance", 0, 42, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Recv", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        //spdData.RPT_AddBasicColumn("MARK", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        //spdData.RPT_AddBasicColumn("SBA", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("A/O", 1, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 50, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 42, 9);

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
                        spdData.RPT_ColumnConfigFromTable(btnSort);
                    }
                    #endregion
                    #region 월계획, kpcs 체크
                    else
                    {
                        spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 200);
                        spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 200);
                        spdData.RPT_AddBasicColumn("Cust Device", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("SAP_CODE", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);

                        spdData.RPT_AddBasicColumn("monthly", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
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
                        spdData.RPT_AddBasicColumn("A/O", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("Progress rate", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("Month shortage (A/O)", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 90);
                        spdData.RPT_AddBasicColumn("Month shortage (IN)", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 12, 8);

                        spdData.RPT_AddBasicColumn("a daily goal", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("A/O", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);


                        spdData.RPT_AddBasicColumn("the day's performance", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("일AC-T", 1, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("R-OUT", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 21, 3);

                        spdData.RPT_AddBasicColumn("ASSY WIP", 0, 24, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("TOTAL", 1, 24, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("S/P", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("HMKA3", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 24, 18);

                        spdData.RPT_AddBasicColumn("The previous day's performance", 0, 42, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("Recv", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        //spdData.RPT_AddBasicColumn("MARK", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        //spdData.RPT_AddBasicColumn("SBA", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("A/O", 1, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 50, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 42, 9);

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
                        spdData.RPT_ColumnConfigFromTable(btnSort);
                    }
                    #endregion
                }
                else
                {
                    #region 주계획, kpcs 체크 안함
                    if (ckbKpcs.Checked == false)
                    {
                        spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 200);
                        spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 200);
                        spdData.RPT_AddBasicColumn("Cust Device", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("SAP_CODE", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);

                        spdData.RPT_AddBasicColumn("weekly", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WW" + FindWeek.ThisWeek.Substring(4,2), 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("WW" + FindWeek.NextWeek.Substring(4,2), 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                        
                        spdData.RPT_AddBasicColumn("A/O", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Progress rate", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("Shortage (A / 0)", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 90);
                        spdData.RPT_AddBasicColumn("Shortage (IN)", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 12, 7);

                        spdData.RPT_AddBasicColumn("a daily goal", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("A/O", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                        spdData.RPT_AddBasicColumn("The previous day's performance", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Recv", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                        
                        spdData.RPT_AddBasicColumn("PLT", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                        
                        spdData.RPT_AddBasicColumn("SIG", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("A/O", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 20, 9);

                        spdData.RPT_AddBasicColumn("ASSY WIP", 0, 29, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TOTAL", 1, 29, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("S/P", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMKA3", 1, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 29, 18);

                        spdData.RPT_AddBasicColumn("the day's performance", 0, 47, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("일AC-T", 1, 47, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("R-OUT", 1, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 47, 3);

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
                        spdData.RPT_ColumnConfigFromTable(btnSort);
                    }
                    #endregion
                    #region 주계획, kpcs 체크
                    else
                    {
                        spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                        spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 200);
                        spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 200);
                        spdData.RPT_AddBasicColumn("Cust Device", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("SAP_CODE", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);

                        spdData.RPT_AddBasicColumn("weekly", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("WW" + FindWeek.ThisWeek.Substring(4, 2), 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("WW" + FindWeek.NextWeek.Substring(4, 2), 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("A/O", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("Progress rate", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("Shortage (A / 0)", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 90);
                        spdData.RPT_AddBasicColumn("Shortage (IN)", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 12, 7);

                        spdData.RPT_AddBasicColumn("a daily goal", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("A/O", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);

                        spdData.RPT_AddBasicColumn("The previous day's performance", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("Recv", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("A/O", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 20, 9);

                        spdData.RPT_AddBasicColumn("ASSY WIP", 0, 29, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("TOTAL", 1, 29, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("S/P", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("HMKA3", 1, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 29, 18);

                        spdData.RPT_AddBasicColumn("the day's performance", 0, 47, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("일AC-T", 1, 47, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SHIP", 1, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("R-OUT", 1, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, 47, 3);

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
                        spdData.RPT_ColumnConfigFromTable(btnSort);
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "A.MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", "MIN(FAC_SEQ)", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "A.MAT_GRP_2", "A.MAT_GRP_2 AS FAMILY", "MIN(FAB_SEQ) AS FAMILY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "A.MAT_GRP_3", "A.MAT_GRP_3 AS PACKAGE", false);            
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "A.MAT_GRP_4", "A.MAT_GRP_4 AS TYPE1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "A.MAT_GRP_5", "A.MAT_GRP_5 AS TYPE2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "A.MAT_GRP_6", "A.MAT_GRP_6 AS \"LD COUNT\"", false);
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
            string start_date;         // 금월 시작일
            string yesterday;          // 조회일 기준 어제일자
            string date;               // 기준일
            string month;              // 기준월
            string year;               // 기준년
            string lastMonth;          // 지난달
            //string[] arrLotType;
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
            lastMonth = Select_date.AddMonths(-1).ToString("yyyyMM"); // 지난달

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

            /*
            // LotType 선택시 P Type 있는지 확인, 있으면 월계획 뿌려줌.
            if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
            {
                arrLotType = cdvLotType.txtValue.Split(',');
                lotType = false;

                for (int i = 0; i < arrLotType.Length; i++)
                {
                    if (arrLotType[i].Trim().Substring(0, 1).Equals("P"))
                    {
                        lotType = true;
                    }
                }
            }
            */
            // LotType 선택시 P Type 있는지 확인, 있으면 월계획 뿌려줌.
            if (cdvLotType.Text == "P%")
            {
                lotType = true;
            }

            // 2012-08-09-임종우 : kcpk 선택에 의한 분모 값 저장 한다.
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
                #region Wafer Qty
                if (cdvQty.Text == "Wafer_Qty") //수정 ㄱㄱ 
                {        
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.WEEK1_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.WEEK1_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.WEEK1_PLAN),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.WEEK2_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.WEEK2_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.WEEK2_PLAN),0))/" + sKcpkValue + ", 1) " + "\n");                    
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.ASSY_WEEK/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.ASSY_WEEK/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.ASSY_WEEK),0))/" + sKcpkValue + ", 1)          " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.SHIP_WEEK/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.SHIP_WEEK/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.SHIP_WEEK),0))/" + sKcpkValue + ", 1) " + "\n");                  
                    strSqlString.Append("     , ROUND(NVL(DECODE(SUM(A.WEEK1_PLAN), 0, 0, ROUND((SUM(A.ASSY_WEEK)/SUM(A.WEEK1_PLAN))*100, 1)),0),1) AS JINDO" + "\n");
                    strSqlString.Append("     , ROUND((SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.WEEK1_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.WEEK1_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.WEEK1_PLAN),0)) - SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.ASSY_WEEK/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.ASSY_WEEK/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.ASSY_WEEK),0)) )/" + sKcpkValue + ", 1) AS LACK   " + "\n");
                    strSqlString.Append("     , ROUND(NVL((SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.WEEK1_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.WEEK1_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.WEEK1_PLAN),0)) - SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.ASSY_WEEK/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.ASSY_WEEK/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.ASSY_WEEK),0)) - SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V0+B.V1+B.V2+B.V3+B.V4+B.V5+B.V6+B.V7+B.V8+B.V9+B.V10+B.V11+B.V12+B.V13+B.V14+B.V15+B.V16/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V0+B.V1+B.V2+B.V3+B.V4+B.V5+B.V6+B.V7+B.V8+B.V9+B.V10+B.V11+B.V12+B.V13+B.V14+B.V15+B.V16/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V0+B.V1+B.V2+B.V3+B.V4+B.V5+B.V6+B.V7+B.V8+B.V9+B.V10+B.V11+B.V12+B.V13+B.V14+B.V15+B.V16),0))) ,0)/" + sKcpkValue + ",1) AS LACK_IN   " + "\n");

                    if (lblRemain2.Text != "0")
                    {
                        strSqlString.AppendFormat("     , ROUND((SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.WEEK1_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.WEEK1_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.WEEK1_PLAN),0)) - SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.ASSY_WEEK/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.ASSY_WEEK/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.ASSY_WEEK),0)) )/{0} /" + sKcpkValue + ", 1) AS TARGET" + "\n", Convert.ToUInt32(lblRemain2.Text));
                    }
                    else
                    {
                        strSqlString.Append("     , 0 AS TARGET " + "\n");
                    }

                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.RCV_YESTERDAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.RCV_YESTERDAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.RCV_YESTERDAY),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(C.V0/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(C.V0/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), C.V0),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(C.V1/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(C.V1/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), C.V1),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(C.V2/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(C.V2/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), C.V2),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(C.V3/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(C.V3/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), C.V3),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(C.V5/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(C.V5/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), C.V5),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(C.V7/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(C.V7/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), C.V7),0))/" + sKcpkValue + ", 1)   " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.ASSY_YESTERDAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.ASSY_YESTERDAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.ASSY_YESTERDAY),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.SHIP_YESTERDAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.SHIP_YESTERDAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.SHIP_YESTERDAY),0))/" + sKcpkValue + ", 1)  " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V0+B.V1+B.V2+B.V3+B.V4+B.V5+B.V6+B.V7+B.V8+B.V9+B.V10+B.V11+B.V12+B.V13+B.V14+B.V15+B.V16/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN', ROUND(B.V0+B.V1+B.V2+B.V3+B.V4+B.V5+B.V6+B.V7+B.V8+B.V9+B.V10+B.V11+B.V12+B.V13+B.V14+B.V15+B.V16/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V0+B.V1+B.V2+B.V3+B.V4+B.V5+B.V6+B.V7+B.V8+B.V9+B.V10+B.V11+B.V12+B.V13+B.V14+B.V15+B.V16),0))/" + sKcpkValue + ", 1) AS TOTAL  " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V0/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V0/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V0),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V1/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V1/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V1),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V2/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V2/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V2),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V3/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V3/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V3),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V4/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V4/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V4),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V5/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V5/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V5),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V6/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V6/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V6),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V7/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V7/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V7),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V8/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V8/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V8),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V9/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V9/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V9),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V10/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V10/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V10),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V11/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V11/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V11),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V12/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V12/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V12),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V13/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V13/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V13),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V14/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V14/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V14),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V15/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V15/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V15),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V16/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V16/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V16),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(F.AC_T_DAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(F.AC_T_DAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), F.AC_T_DAY),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(E.SHIP_DAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(E.SHIP_DAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), E.SHIP_DAY),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(D.RTN_DAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(D.RTN_DAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), D.RTN_DAY),0))/" + sKcpkValue + ", 1) " + "\n");
                }
                #endregion
                #region Chip Qty
                else
                {
                    strSqlString.Append("     , ROUND(SUM(NVL(A.WEEK1_PLAN,0)) /" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(A.WEEK2_PLAN,0)) /" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(A.ASSY_WEEK,0))/" + sKcpkValue + ", 1)          " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(A.SHIP_WEEK,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(NVL(DECODE( SUM(A.WEEK1_PLAN), 0, 0, ROUND((SUM(A.ASSY_WEEK)/SUM(A.WEEK1_PLAN))*100, 1)),0),1) AS JINDO" + "\n");
                    strSqlString.Append("     , ROUND((SUM(NVL(A.WEEK1_PLAN,0)) - SUM(NVL(A.ASSY_WEEK,0)) )/" + sKcpkValue + ", 1) AS LACK   " + "\n");
                    strSqlString.Append("     , ROUND(NVL((SUM(NVL(A.WEEK1_PLAN,0)) - SUM(NVL(A.ASSY_WEEK,0)) - SUM(NVL(B.V0+B.V1+B.V2+B.V3+B.V4+B.V5+B.V6+B.V7+B.V8+B.V9+B.V10+B.V11+B.V12+B.V13+B.V14+B.V15+B.V16,0))) ,0)/" + sKcpkValue + ",1) AS LACK_IN   " + "\n");

                    if (lblRemain2.Text != "0")
                    {
                        strSqlString.AppendFormat("     , ROUND((SUM(NVL(A.WEEK1_PLAN,0)) - SUM(NVL(A.ASSY_WEEK,0)) )/{0} /" + sKcpkValue + ", 1) AS TARGET" + "\n", Convert.ToUInt32(lblRemain2.Text));
                    }
                    else
                    {
                        strSqlString.Append("     , 0 AS TARGET " + "\n");
                    }
                                        
                    strSqlString.Append("     , ROUND(SUM(NVL(A.RCV_YESTERDAY,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(C.V0,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(C.V1,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(C.V2,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(C.V3,0))/" + sKcpkValue + ", 1) " + "\n");                    
                    strSqlString.Append("     , ROUND(SUM(NVL(C.V5,0))/" + sKcpkValue + ", 1) " + "\n");                    
                    strSqlString.Append("     , ROUND(SUM(NVL(C.V7,0))/" + sKcpkValue + ", 1)   " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(A.ASSY_YESTERDAY,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(A.SHIP_YESTERDAY,0))/" + sKcpkValue + ", 1)  " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V0+B.V1+B.V2+B.V3+B.V4+B.V5+B.V6+B.V7+B.V8+B.V9+B.V10+B.V11+B.V12+B.V13+B.V14+B.V15+B.V16,0))/" + sKcpkValue + ", 1) AS TOTAL  " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V0,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V1,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V2,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V3,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V4,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V5,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V6,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V7,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V8,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V9,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V10,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V11,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V12,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V13,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V14,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V15,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V16,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(F.AC_T_DAY,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(E.SHIP_DAY,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(D.RTN_DAY,0))/" + sKcpkValue + ", 1) " + "\n");                    
                }
                #endregion

                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT A.MAT_GRP_1, A.MAT_GRP_2, A.MAT_GRP_3, A.MAT_GRP_4, A.MAT_GRP_5, A.MAT_GRP_6, A.MAT_GRP_7, A.MAT_GRP_8, A.MAT_GRP_9, A.MAT_CMF_10, A.MAT_ID, A.MAT_CMF_7, A.MAT_CMF_13,BASE_MAT_ID" + "\n");
                strSqlString.Append("             , SUM(B.WEEK1_PLAN) WEEK1_PLAN" + "\n");
                strSqlString.Append("             , SUM(B.WEEK2_PLAN) WEEK2_PLAN" + "\n");
                strSqlString.Append("             , SUM(C.ASSY_WEEK) ASSY_WEEK" + "\n");
                strSqlString.Append("             , SUM(C.ASSY_YESTERDAY) ASSY_YESTERDAY" + "\n");
                strSqlString.Append("             , SUM(D.SHIP_WEEK) SHIP_WEEK " + "\n");
                strSqlString.Append("             , SUM(D.SHIP_YESTERDAY) SHIP_YESTERDAY " + "\n");
                strSqlString.Append("             , SUM(F.RCV_DAY) RCV_DAY" + "\n");
                strSqlString.Append("             , SUM(F.RCV_YESTERDAY) RCV_YESTERDAY" + "\n");
                strSqlString.Append("          FROM MWIPMATDEF A " + "\n");
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.ThisWeek + "', WW_QTY)) AS WEEK1_PLAN " + "\n");
                strSqlString.Append("                     , SUM(DECODE(PLAN_WEEK, '" + FindWeek.NextWeek + "', WW_QTY)) AS WEEK2_PLAN " + "\n");
                strSqlString.Append("                  FROM RWIPPLNWEK " + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND GUBUN = '3' " + "\n");
                strSqlString.Append("                   AND PLAN_WEEK IN ('" + FindWeek.ThisWeek + "','" + FindWeek.NextWeek + "')" + "\n");
                strSqlString.Append("                 GROUP BY FACTORY, MAT_ID " + "\n");                
                strSqlString.Append("               ) B " + "\n");

                // 월A/O, 전일A/O 값 -->예) 4/1일 조회기준 - 전월 말일부터 ~ 금일까지 가져옴. 전일: 3/31일(전월말일), 4월합: 3/31(전원말일)을 제외하고 모두 SUM 
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID " + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + FindWeek.EndDay_LastWeek + "', 0, NVL(CLS_QTY_1,0))) AS ASSY_WEEK " + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + yesterday + "', NVL(CLS_QTY_1,0), 0)) AS ASSY_YESTERDAY " + "\n");
                strSqlString.Append("                  FROM VSUMWIPCLS " + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND MAT_VER = 1 " + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                   AND CM_KEY_2 = 'PROD'" + "\n");
                /*
                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                   AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }
                */
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + FindWeek.EndDay_LastWeek + "' AND '" + date + "' \n");
                strSqlString.Append("                   AND OPER IN ('AZ009', 'AZ010') " + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
                strSqlString.Append("               ) C" + "\n");
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT MAT_ID " + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE,'" + FindWeek.EndDay_LastWeek + "', 0, NVL(SHP_QTY_1, 0))) AS SHIP_WEEK " + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE,'" + yesterday + "', NVL(SHP_QTY_1, 0), 0)) AS SHIP_YESTERDAY " + "\n");
                strSqlString.Append("                  FROM VSUMWIPSHP " + "\n");
                strSqlString.Append("                 WHERE 1 = 1 " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND CM_KEY_2 = 'PROD'" + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + FindWeek.EndDay_LastWeek + "' AND '" + date + "'" + "\n");
                /*
                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                   AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }
                */
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) D " + "\n");
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + yesterday + "', NVL(RCV_QTY_1,0), 0)) AS RCV_YESTERDAY" + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + date + "', NVL(RCV_QTY_1,0), 0)) AS RCV_DAY      " + "\n");
                strSqlString.Append("                  FROM VSUMWIPRCV " + "\n");
                strSqlString.Append("                 WHERE 1 = 1 " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + yesterday + "' AND '" + date + "'" + "\n");
                /*
                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                   AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }
                */
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) F " + "\n");

                strSqlString.Append("         WHERE 1 = 1 " + "\n");
                strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND A.MAT_TYPE= 'FG' " + "\n");
                strSqlString.Append("           AND A.FACTORY =B.FACTORY(+) " + "\n");
                strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND A.MAT_ID = C.MAT_ID(+)  " + "\n");
                strSqlString.Append("           AND A.MAT_ID = D.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND A.MAT_ID = F.MAT_ID(+)    " + "\n");
                strSqlString.Append("         GROUP BY A.MAT_GRP_1, A.MAT_GRP_2, A.MAT_GRP_3, A.MAT_GRP_4, A.MAT_GRP_5, A.MAT_GRP_6, A.MAT_GRP_7, A.MAT_GRP_8, A.MAT_GRP_9, A.MAT_CMF_10, A.MAT_ID, A.MAT_CMF_7, A.MAT_CMF_13, BASE_MAT_ID" + "\n");
                strSqlString.Append("         HAVING A.MAT_GRP_2 NOT IN ('PW','PK')    " + "\n");
                strSqlString.Append("       ) A  " + "\n");
            }
            #endregion
            #region 월 계획
            else
            {
                #region Wafer Qty
                if (cdvQty.Text == "Wafer_Qty") //수정 ㄱㄱ 
                { 
                    if (lotType == false) // lotType 이E Type이면계획안보여줌.
                    {
                        strSqlString.Append("     , 0 " + "\n");
                        strSqlString.Append("     , 0 " + "\n");
                        strSqlString.Append("     , 0 " + "\n");
                    }
                    else // lotType이P Type 또는ALL 일경우계획보여줌.
                    {
                        if (ckbRev.Checked == false)
                        {
                            strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.MON_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.MON_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.MON_PLAN),0))/" + sKcpkValue + ", 1) " + "\n");
                            strSqlString.Append("     , 0 " + "\n");
                            strSqlString.Append("     , 0 " + "\n");
                        }
                        else
                        {
                            strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.ORI_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.ORI_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.ORI_PLAN),0))/" + sKcpkValue + ", 1) " + "\n");
                            strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.MON_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.MON_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.MON_PLAN),0))/" + sKcpkValue + ", 1) " + "\n");
                            strSqlString.Append("     , ROUND((SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.MON_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.MON_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.MON_PLAN),0)) - SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.ORI_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.ORI_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.ORI_PLAN),0))) /" + sKcpkValue + ", 1) " + "\n");
                        }
                    }

                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.ASSY_MON/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.ASSY_MON/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.ASSY_MON),0))/" + sKcpkValue + ", 1)          " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.SHIP_MON/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.SHIP_MON/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.SHIP_MON),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(NVL(DECODE( SUM(A.MON_PLAN), 0, 0, ROUND((SUM(A.ASSY_MON)/SUM(A.MON_PLAN))*100, 1)),0),1) AS JINDO" + "\n");
                    strSqlString.Append("     , ROUND(( SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.MON_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.MON_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.MON_PLAN),0)) - SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.ASSY_MON/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.ASSY_MON/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.ASSY_MON),0)) )/" + sKcpkValue + ", 1) AS LACK   " + "\n");
                    strSqlString.Append("     , ROUND(NVL( (SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.MON_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.MON_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.MON_PLAN),0)) - SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.ASSY_MON/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.ASSY_MON/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.ASSY_MON),0)) - SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V0+B.V1+B.V2+B.V3+B.V4+B.V5+B.V6+B.V7+B.V8+B.V9+B.V10+B.V11+B.V12+B.V13+B.V14+B.V15+B.V16/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V0+B.V1+B.V2+B.V3+B.V4+B.V5+B.V6+B.V7+B.V8+B.V9+B.V10+B.V11+B.V12+B.V13+B.V14+B.V15+B.V16/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V0+B.V1+B.V2+B.V3+B.V4+B.V5+B.V6+B.V7+B.V8+B.V9+B.V10+B.V11+B.V12+B.V13+B.V14+B.V15+B.V16),0))) ,0)/" + sKcpkValue + ",1) AS LACK_IN   " + "\n");

                    if (lblRemain2.Text != "0")
                    {
                        strSqlString.AppendFormat("     , ROUND( ( SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.MON_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.MON_PLAN/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.MON_PLAN),0)) - SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.ASSY_MON/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.ASSY_MON/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.ASSY_MON),0)) )/{0} /" + sKcpkValue + ", 1) AS TARGET" + "\n", Convert.ToUInt32(lblRemain2.Text));
                    }
                    else
                    {
                        strSqlString.Append("     , 0 AS TARGET " + "\n");
                    }

                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(F.AC_T_DAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(F.AC_T_DAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), F.AC_T_DAY),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(E.SHIP_DAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(E.SHIP_DAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), E.SHIP_DAY),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(D.RTN_DAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(D.RTN_DAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), D.RTN_DAY),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V0+B.V1+B.V2+B.V3+B.V4+B.V5+B.V6+B.V7+B.V8+B.V9+B.V10+B.V11+B.V12+B.V13+B.V14+B.V15+B.V16/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN', ROUND(B.V0+B.V1+B.V2+B.V3+B.V4+B.V5+B.V6+B.V7+B.V8+B.V9+B.V10+B.V11+B.V12+B.V13+B.V14+B.V15+B.V16/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V0+B.V1+B.V2+B.V3+B.V4+B.V5+B.V6+B.V7+B.V8+B.V9+B.V10+B.V11+B.V12+B.V13+B.V14+B.V15+B.V16),0))/" + sKcpkValue + ", 1) AS TOTAL  " + "\n");

                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V0/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V0/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V0),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V1/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V1/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V1),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V2/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V2/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V2),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V3/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V3/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V3),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V4/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V4/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V4),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V5/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V5/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V5),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V6/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V6/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V6),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V7/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V7/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V7),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V8/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V8/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V8),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V9/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V9/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V9),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V10/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V10/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V10),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V11/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V11/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V11),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V12/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V12/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V12),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V13/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V13/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V13),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V14/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V14/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V14),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V15/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V15/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V15),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(B.V16/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(B.V16/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), B.V16),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.RCV_YESTERDAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.RCV_YESTERDAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.RCV_YESTERDAY),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(C.V0/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(C.V0/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), C.V0),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(C.V1/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(C.V1/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), C.V1),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(C.V2/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(C.V2/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), C.V2),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(C.V3/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(C.V3/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), C.V3),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(C.V5/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(C.V5/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), C.V5),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(C.V7/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(C.V7/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), C.V7),0))/" + sKcpkValue + ", 1)   " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.ASSY_YESTERDAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.ASSY_YESTERDAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.ASSY_YESTERDAY),0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(DECODE(A.MAT_GRP_3, 'COB', ROUND(A.SHIP_YESTERDAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), 'BGN',ROUND(A.SHIP_YESTERDAY/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13))), A.SHIP_YESTERDAY),0))/" + sKcpkValue + ", 1)  " + "\n");                    
                }
                #endregion
                #region Chip Qty
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
                            strSqlString.Append("     , ROUND(SUM(NVL(A.MON_PLAN,0))/" + sKcpkValue + ", 1) " + "\n");
                            strSqlString.Append("     , 0 " + "\n");
                            strSqlString.Append("     , 0 " + "\n");
                        }
                        else
                        {
                            strSqlString.Append("     , ROUND(SUM(NVL(A.ORI_PLAN,0))/" + sKcpkValue + ", 1) " + "\n");
                            strSqlString.Append("     , ROUND(SUM(NVL(A.MON_PLAN,0))/" + sKcpkValue + ", 1) " + "\n");
                            strSqlString.Append("     , ROUND((SUM(NVL(A.MON_PLAN,0)) - SUM(NVL(A.ORI_PLAN,0)))/" + sKcpkValue + ", 1) " + "\n");
                        }
                    }

                    strSqlString.Append("     , ROUND(SUM(NVL(A.ASSY_MON,0))/" + sKcpkValue + ", 1)          " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(A.SHIP_MON,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(NVL(DECODE( SUM(A.MON_PLAN), 0, 0, ROUND((SUM(A.ASSY_MON)/SUM(A.MON_PLAN))*100, 1)),0),1) AS JINDO" + "\n");
                    strSqlString.Append("     , ROUND(( SUM(NVL(A.MON_PLAN,0)) - SUM(NVL(A.ASSY_MON,0)) )/" + sKcpkValue + ", 1) AS LACK   " + "\n");
                    strSqlString.Append("     , ROUND(NVL( (SUM(NVL(A.MON_PLAN,0)) - SUM(NVL(A.ASSY_MON,0)) - SUM(NVL(B.V0+B.V1+B.V2+B.V3+B.V4+B.V5+B.V6+B.V7+B.V8+B.V9+B.V10+B.V11+B.V12+B.V13+B.V14+B.V15+B.V16,0))) ,0)/" + sKcpkValue + ",1) AS LACK_IN   " + "\n");

                    if (lblRemain2.Text != "0")
                    {
                        strSqlString.AppendFormat("     , ROUND( ( SUM(NVL(A.MON_PLAN,0)) - SUM(NVL(A.ASSY_MON,0)) )/{0} /" + sKcpkValue + ", 1) AS TARGET" + "\n", Convert.ToUInt32(lblRemain2.Text));
                    }
                    else
                    {
                        strSqlString.Append("     , 0 AS TARGET " + "\n");
                    }

                    strSqlString.Append("     , ROUND(SUM(NVL(F.AC_T_DAY,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(E.SHIP_DAY,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(D.RTN_DAY,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V0+B.V1+B.V2+B.V3+B.V4+B.V5+B.V6+B.V7+B.V8+B.V9+B.V10+B.V11+B.V12+B.V13+B.V14+B.V15+B.V16,0))/" + sKcpkValue + ", 1) AS TOTAL  " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V0,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V1,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V2,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V3,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V4,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V5,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V6,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V7,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V8,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V9,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V10,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V11,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V12,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V13,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V14,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V15,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.V16,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(A.RCV_YESTERDAY,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(C.V0,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(C.V1,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(C.V2,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(C.V3,0))/" + sKcpkValue + ", 1) " + "\n");
                    //strSqlString.Append("     , ROUND(SUM(NVL(C.V4,0))/1000, 1) " + "\n");                
                    strSqlString.Append("     , ROUND(SUM(NVL(C.V5,0))/" + sKcpkValue + ", 1) " + "\n");
                    //strSqlString.Append("     , ROUND(SUM(NVL(C.V6,0))/1000, 1)   " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(C.V7,0))/" + sKcpkValue + ", 1)   " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(A.ASSY_YESTERDAY,0))/" + sKcpkValue + ", 1) " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(A.SHIP_YESTERDAY,0))/" + sKcpkValue + ", 1)  " + "\n");                    
                }
                #endregion

                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT A.MAT_GRP_1, A.MAT_GRP_2, A.MAT_GRP_3, A.MAT_GRP_4, A.MAT_GRP_5, A.MAT_GRP_6, A.MAT_GRP_7, A.MAT_GRP_8, A.MAT_GRP_9, A.MAT_CMF_10, A.MAT_ID, A.MAT_CMF_7, A.MAT_CMF_13,BASE_MAT_ID" + "\n");
                strSqlString.Append("             , SUM(B.RESV_FIELD1) MON_PLAN" + "\n");
                strSqlString.Append("             , SUM(B.PLAN_QTY_ASSY) ORI_PLAN" + "\n");
                //strSqlString.Append("             , SUM(C_1.ASSY_MON) ASSY_MON" + "\n");
                strSqlString.Append("             , SUM(C.ASSY_MON) ASSY_MON" + "\n");
                strSqlString.Append("             , SUM(C.ASSY_DAY) ASSY_DAY" + "\n");
                strSqlString.Append("             , SUM(C.ASSY_YESTERDAY) ASSY_YESTERDAY" + "\n");
                strSqlString.Append("             , SUM(D.SHIP_MON) SHIP_MON " + "\n");
                //strSqlString.Append("             , SUM(D.SHIP_DAY) SHIP_DAY " + "\n");
                strSqlString.Append("             , SUM(D.SHIP_YESTERDAY) SHIP_YESTERDAY " + "\n");
                strSqlString.Append("             , SUM(E.CUR_WIP) AS CUR_WIP " + "\n");
                strSqlString.Append("             , SUM(F.RCV_DAY) RCV_DAY" + "\n");
                strSqlString.Append("             , SUM(F.RCV_YESTERDAY) RCV_YESTERDAY" + "\n");
                strSqlString.Append("          FROM MWIPMATDEF A " + "\n");
                //strSqlString.Append("             , CWIPPLNMON B " + "\n");

                //월 계획 SLIS 제품은 MP계획과 동일하게
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT FACTORY,MAT_ID,PLAN_QTY_ASSY,PLAN_MONTH, RESV_FIELD1 " + "\n");
                strSqlString.Append("                  FROM ( " + "\n");
                strSqlString.Append("                        SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, PLAN_MONTH, SUM(RESV_FIELD1) AS RESV_FIELD1  " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, PLAN_MONTH, SUM(TO_NUMBER(DECODE(RESV_FIELD1,' ',0,RESV_FIELD1))) AS RESV_FIELD1 " + "\n"); // 김민우 : 변경 계획 값(RESV_FIELD1)
                strSqlString.Append("                                  FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                /*
                strSqlString.Append("                                   AND MAT_ID NOT IN ( " + "\n");
                strSqlString.Append("                                                      SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                        FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                                       WHERE 1=1 " + "\n");
                strSqlString.Append("                                                         AND MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                                                         AND MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                                                     ) " + "\n");
                */
                strSqlString.Append("                                 GROUP BY FACTORY, MAT_ID, PLAN_MONTH " + "\n");

                // 2013-06-07-임종우 : SE 제품 월 계획 REV 값을 기존 OMS 데이터에서 월 계획 UPLOAD 데이터로 변경 함 (김은석 요청)
                //strSqlString.Append("                                UNION ALL " + "\n");
                //strSqlString.Append("                                SELECT A.FACTORY, A.MAT_ID, 0 AS PLAN_QTY_ASSY , '" + month + "' AS PLAN_MONTH, SUM(A.PLAN_QTY) AS RESV_FIELD1 " + "\n");
                //strSqlString.Append("                                  FROM ( " + "\n");
                //// 월계획 금일이면 기존대로
                //if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                //{
                //    strSqlString.Append("                                        SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                //    strSqlString.Append("                                          FROM CWIPPLNDAY " + "\n");
                //    strSqlString.Append("                                         WHERE 1=1 " + "\n");
                //    strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                //    strSqlString.Append("                                           AND PLAN_DAY BETWEEN '" + start_date + "' AND '" + last_day + "'\n");
                //    strSqlString.Append("                                           AND IN_OUT_FLAG = 'OUT' " + "\n");
                //    strSqlString.Append("                                           AND CLASS = 'ASSY' " + "\n");
                //    strSqlString.Append("                                         GROUP BY FACTORY, MAT_ID " + "\n");
                //}
                //else// 금일이 아니면 스냅샷 떠놓은 테이블에서 가져옴.
                //{
                //    strSqlString.Append("                                        SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                //    strSqlString.Append("                                          FROM CWIPPLNSNP@RPTTOMES " + "\n");
                //    strSqlString.Append("                                         WHERE 1=1 " + "\n");
                //    strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                //    strSqlString.Append("                                           AND SNAPSHOT_DAY = '" + cdvDate.SelectedValue() + "'" + "\n");
                //    strSqlString.Append("                                           AND PLAN_DAY BETWEEN '" + start_date + "' AND '" + last_day + "'\n");
                //    strSqlString.Append("                                           AND IN_OUT_FLAG = 'OUT' " + "\n");
                //    strSqlString.Append("                                           AND CLASS = 'ASSY' " + "\n");
                //    strSqlString.Append("                                        GROUP BY FACTORY, MAT_ID " + "\n");
                //}

                //strSqlString.Append("                                        UNION ALL " + "\n");
                //strSqlString.Append("                                        SELECT CM_KEY_1 AS FACTORY, MAT_ID " + "\n");
                //strSqlString.Append("                                             , SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS PLAN_QTY " + "\n");
                //strSqlString.Append("                                          FROM RSUMFACMOV " + "\n");
                //strSqlString.Append("                                         WHERE 1=1 " + "\n");
                //strSqlString.Append("                                           AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                //strSqlString.Append("                                           AND CM_KEY_2 = 'PROD' " + "\n");
                //strSqlString.Append("                                           AND CM_KEY_3 LIKE 'P%' " + "\n");
                //strSqlString.Append("                                           AND WORK_DATE BETWEEN '" + start_date + "' AND '" + FindWeek.EndDay_LastWeek + "'\n");
                //strSqlString.Append("                                         GROUP BY CM_KEY_1, MAT_ID " + "\n");
                //strSqlString.Append("                                       ) A" + "\n");
                //strSqlString.Append("                                     , MWIPMATDEF B " + "\n");
                //strSqlString.Append("                                 WHERE 1=1  " + "\n");
                //strSqlString.Append("                                   AND A.FACTORY = B.FACTORY " + "\n");
                //strSqlString.Append("                                   AND A.MAT_ID = B.MAT_ID " + "\n");
                //strSqlString.Append("                                   AND B.MAT_GRP_1 = 'SE' " + "\n");
                //strSqlString.Append("                                   AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
                //strSqlString.Append("                                 GROUP BY A.FACTORY, A.MAT_ID " + "\n");
                strSqlString.Append("                               ) " + "\n");
                strSqlString.Append("                         GROUP BY FACTORY, MAT_ID,PLAN_MONTH " + "\n");

                strSqlString.Append("                       ) " + "\n");
                strSqlString.Append("               ) B " + "\n");

                // 월A/O, 전일A/O 값 -->예) 4/1일 조회기준 - 전월 말일부터 ~ 금일까지 가져옴. 전일: 3/31일(전월말일), 4월합: 3/31(전원말일)을 제외하고 모두 SUM 
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT MAT_ID " + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + Last_Month_Last_day + "', 0, NVL(CLS_QTY_1,0))) AS ASSY_MON " + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + date + "', NVL(CLS_QTY_1,0), 0)) AS ASSY_DAY " + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + yesterday + "', NVL(CLS_QTY_1,0), 0)) AS ASSY_YESTERDAY " + "\n");
                strSqlString.Append("                  FROM VSUMWIPCLS " + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND MAT_VER = 1 " + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                   AND CM_KEY_2 = 'PROD'" + "\n");
                /*
                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                   AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }
                */
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + date + "' \n");
                strSqlString.Append("                   AND OPER IN ('AZ009', 'AZ010') " + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
                strSqlString.Append("               ) C" + "\n");

                // 2009-12-23-임종우 월A/O 값 Return 수량 제외 표시 로 인하여 해당 쿼리 주석처리 함.(임태성 대리 요청)
                //strSqlString.Append("             , ( " + "\n");
                //strSqlString.Append("               SELECT MAT_ID " + "\n");
                //strSqlString.Append("                    , SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS ASSY_MON " + "\n");
                //strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                //strSqlString.Append("                WHERE 1=1 " + "\n");
                //strSqlString.Append("                  AND CM_KEY_1='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                //strSqlString.Append("                  AND CM_KEY_2='PROD'" + "\n");

                //if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                //{
                //    strSqlString.Append("                  AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                //}

                //strSqlString.Append("                  AND WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "'\n");
                //strSqlString.Append("                GROUP BY MAT_ID " + "\n");
                //strSqlString.Append("               ) C_1 " + "\n");
                // 2009-12-23-임종우 주처 처리 끝

                strSqlString.Append("             , ( " + "\n");

                // 월SHIP, 전일SHIP 값 -->예) 4/1일 조회기준 - 전월 말일부터 ~ 금일까지 가져옴. 전일: 3/31일(전월말일), 4월합: 3/31(전원말일)을 제외하고 모두 SUM 
                strSqlString.Append("                SELECT MAT_ID, SUM(SHIP_MON) SHIP_MON, SUM(SHIP_YESTERDAY) SHIP_YESTERDAY " + "\n");
                strSqlString.Append("                  FROM ( " + "\n");
                strSqlString.Append("                        SELECT MAT_ID " + "\n");
                strSqlString.Append("                             , SUM(DECODE(WORK_DATE,'" + Last_Month_Last_day + "', 0, NVL(SHP_QTY_1, 0))) AS SHIP_MON " + "\n");
                //strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE = '" + date + "'" + "\n");
                //strSqlString.Append("                                        THEN NVL(SHP_QTY_1, 0)  ELSE 0 END) SHIP_DAY " + "\n");            
                strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE = '" + yesterday + "'" + "\n");
                strSqlString.Append("                                        THEN NVL(SHP_QTY_1, 0)  ELSE 0 END) SHIP_YESTERDAY " + "\n");
                strSqlString.Append("                          FROM VSUMWIPSHP " + "\n");
                strSqlString.Append("                         WHERE 1 = 1 " + "\n");
                strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                           AND CM_KEY_2 = 'PROD'" + "\n");
                strSqlString.Append("                           AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                           AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + date + "'" + "\n");
                /*
                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                           AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }
                */
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                           AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }
                strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) D " + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    strSqlString.Append("             , ( " + "\n");
                    strSqlString.Append("                SELECT MAT_ID, SUM(QTY_1) AS CUR_WIP " + "\n");
                    strSqlString.Append("                  FROM RWIPLOTSTS " + "\n");
                    strSqlString.Append("                 WHERE 1 = 1 " + "\n");
                    strSqlString.Append("                   AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND MAT_VER = 1 " + "\n");
                    strSqlString.Append("                   AND LOT_DEL_FLAG = ' ' " + "\n");
                    strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                    /*
                    if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                    {
                        strSqlString.Append("                   AND LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
                    }
                    */
                    if (cdvLotType.Text != "ALL")
                    {
                        strSqlString.Append("                   AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                    }


                    strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
                    strSqlString.Append("               ) E " + "\n");
                }
                else
                {
                    strSqlString.Append("             , ( " + "\n");
                    strSqlString.Append("                SELECT MAT_ID, SUM(QTY_1) AS CUR_WIP " + "\n");
                    strSqlString.Append("                  FROM RWIPLOTSTS_BOH " + "\n");
                    strSqlString.Append("                 WHERE 1 = 1 " + "\n");
                    strSqlString.Append("                   AND FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND MAT_VER = 1 " + "\n");
                    strSqlString.Append("                   AND LOT_DEL_FLAG = ' ' " + "\n");
                    strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                    /*
                    if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                    {
                        strSqlString.Append("                   AND LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
                    }
                    */
                    if (cdvLotType.Text != "ALL")
                    {
                        strSqlString.Append("                   AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                    }

                    strSqlString.Append("                   AND CUTOFF_DT = '" + cdvDate.SelectedValue() + "22'" + "\n");
                    strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
                    strSqlString.Append("               ) E " + "\n");
                }

                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + yesterday + "', NVL(RCV_QTY_1,0), 0)) AS RCV_YESTERDAY" + "\n");
                strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + date + "', NVL(RCV_QTY_1,0), 0)) AS RCV_DAY      " + "\n");
                strSqlString.Append("                  FROM VSUMWIPRCV " + "\n");
                strSqlString.Append("                 WHERE 1 = 1 " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + yesterday + "' AND '" + date + "'" + "\n");
                /*
                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                   AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }
                */
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) F " + "\n");

                strSqlString.Append("         WHERE 1 = 1 " + "\n");
                strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND B.PLAN_MONTH(+) = '" + month + "' " + "\n");
                strSqlString.Append("           AND A.MAT_TYPE= 'FG' " + "\n");
                strSqlString.Append("           AND A.FACTORY =B.FACTORY(+) " + "\n");
                strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND A.MAT_ID = C.MAT_ID(+)  " + "\n");
                //strSqlString.Append("           AND A.MAT_ID = C_1.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND A.MAT_ID = D.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND A.MAT_ID = E.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND A.MAT_ID = F.MAT_ID(+)    " + "\n");
                strSqlString.Append("         GROUP BY A.MAT_GRP_1, A.MAT_GRP_2, A.MAT_GRP_3, A.MAT_GRP_4, A.MAT_GRP_5, A.MAT_GRP_6, A.MAT_GRP_7, A.MAT_GRP_8, A.MAT_GRP_9, A.MAT_CMF_10, A.MAT_ID, A.MAT_CMF_7, A.MAT_CMF_13, BASE_MAT_ID" + "\n");
                strSqlString.Append("         HAVING A.MAT_GRP_2 NOT IN ('PW','PK')    " + "\n");
                strSqlString.Append("       ) A  " + "\n");
            }
            #endregion

            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK2A', QTY, 0)) V0 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'B/G', QTY, 0)) V1 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SAW', QTY, 0)) V2 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'S/P', QTY, 0)) V3 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'D/A', QTY, 0)) V4 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'W/B', QTY, 0)) V5 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'GATE', QTY, 0)) V6 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'MOLD', QTY, 0)) V7 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'CURE', QTY, 0)) V8 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'M/K', QTY, 0)) V9 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'TRIM', QTY, 0)) V10 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'TIN', QTY, 0)) V11" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'S/B/A', QTY, 0)) V12 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SIG', QTY, 0)) V13 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'AVI', QTY, 0)) V14 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'V/I', QTY, 'QC GATE', QTY, 0)) V15 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK3A', QTY, 0)) V16 " + "\n");
            strSqlString.Append("          FROM (  " + "\n");
            strSqlString.Append("                SELECT A.MAT_ID, B.OPER_GRP_1 " + "\n");
            strSqlString.Append("                     , SUM(A.QTY_1) QTY  " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") != cdvDate.SelectedValue())
            {                
                strSqlString.Append("                  FROM RWIPLOTSTS_BOH A " + "\n");
                strSqlString.Append("                     , MWIPOPRDEF B " + "\n");
                strSqlString.Append("                 WHERE 1 = 1 " + "\n");                
                strSqlString.Append("                   AND A.CUTOFF_DT = '" + cdvDate.SelectedValue() +"22'     " + "\n");                                               
            }
            else if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("                  FROM RWIPLOTSTS A  " + "\n");
                strSqlString.Append("                     , MWIPOPRDEF B  " + "\n");
                strSqlString.Append("                 WHERE 1 = 1 " + "\n");               
            }

            strSqlString.Append("                   AND A.FACTORY = B.FACTORY(+)  " + "\n");
            strSqlString.Append("                   AND A.OPER = B.OPER(+)  " + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                   AND A.MAT_VER = 1  " + "\n");
            strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                   AND A.LOT_DEL_FLAG = ' ' " + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                 GROUP BY A.MAT_ID, B.OPER_GRP_1   " + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID  " + "\n");
            strSqlString.Append("       ) B   " + "\n");
            strSqlString.Append("     , (  " + "\n");
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_7, 'D/A', QTY, 0)) V0 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_7, 'W/B',   QTY, 0)) V1 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_7, 'MOLD',   QTY, 0)) V2 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_7, 'TRIM',   QTY, 0)) V3 " + "\n");
            //strSqlString.Append("             , SUM(DECODE(OPER_GRP_7, 'M/K',   QTY, 0)) V4 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_7, 'TIN',  QTY, 0)) V5  " + "\n");
            //strSqlString.Append("             , SUM(DECODE(OPER_GRP_7, 'S/B/A',  QTY, 0)) V6 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_7, 'SIG',  QTY, 0)) V7 " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT A.MAT_ID, B.OPER_GRP_7" + "\n");
            strSqlString.Append("                     , SUM(A.S1_END_QTY_1 + A.S2_END_QTY_1 + A.S3_END_QTY_1 + A.S1_END_RWK_QTY_1 + A.S2_END_RWK_QTY_1 + A.S3_END_RWK_QTY_1 ) QTY " + "\n");
            strSqlString.Append("                  FROM RSUMWIPMOV A " + "\n");
            strSqlString.Append("                     , MWIPOPRDEF B " + "\n");
            strSqlString.Append("                 WHERE 1 = 1 " + "\n");
            strSqlString.Append("                   AND A.WORK_DATE = '"+ yesterday +"' " + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND A.MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY " + "\n");
         
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND A.CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                   AND A.OPER = B.OPER " + "\n");
            strSqlString.Append("                 GROUP BY A.MAT_ID, B.OPER_GRP_7 " + "\n");            
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("       ) C" + "\n");
            strSqlString.Append("     , (" + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") != cdvDate.SelectedValue())   // 과거 시점일때 RTN_DAY
            {
                //strSqlString.Append("      SELECT MAT_ID" + "\n");
                //strSqlString.Append("           , SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1+S4_FAC_OUT_QTY_1) AS RTN_DAY" + "\n");
                //strSqlString.Append("        FROM RSUMFACMOV " + "\n");
                //strSqlString.Append("       WHERE 1=1 " + "\n");
                //strSqlString.Append("         AND FACTORY= 'RETURN' " + "\n");
                //strSqlString.Append("         AND MAT_VER = 1" + "\n");
                //strSqlString.Append("         AND LOT_TYPE = 'W'" + "\n");
                //strSqlString.Append("         AND CM_KEY_2 = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                //strSqlString.Append("         AND CM_KEY_2 = 'PROD'" + "\n");
                //strSqlString.Append("         AND WORK_DATE = '" + date + "'" + "\n");

                //if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                //{
                //    strSqlString.Append("         AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                //}

                //strSqlString.Append("    GROUP BY MAT_ID " + "\n");

                strSqlString.Append("        SELECT MAT_ID                   " + "\n");
                strSqlString.Append("             , SUM(S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1+S4_FAC_IN_QTY_1) AS RTN_DAY    " + "\n");
                strSqlString.Append("          FROM RSUMFACMOV " + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND FACTORY = CM_KEY_1 " + "\n");
                strSqlString.Append("           AND OPER IN ('AZ010','AZ009') " + "\n");
                strSqlString.Append("           AND CM_KEY_1 <> 'FGS'" + "\n");
                strSqlString.Append("           AND WORK_DATE = '"+ date +"'    " + "\n");
                
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("           AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }


                strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            }
            else        // 금일 시점의 RTN_DAY
            {
                //strSqlString.Append("      SELECT MAT_ID, SUM(QTY_1) AS RTN_DAY " + "\n");
                //strSqlString.Append("        FROM RWIPLOTSTS" + "\n");
                //strSqlString.Append("       WHERE 1=1" + "\n");
                //strSqlString.Append("         AND FACTORY = 'RETURN'" + "\n");
                //strSqlString.Append("         AND MAT_VER = 1" + "\n");
                //strSqlString.Append("         AND LOT_TYPE = 'W'" + "\n");
                //strSqlString.Append("         AND OWNER_CODE = 'PROD'" + "\n");
                //strSqlString.Append("         AND LAST_TRAN_CODE = 'SHIP'" + "\n");
                //strSqlString.Append("         AND LOT_DEL_FLAG = 'Y'" + "\n");
                //strSqlString.Append("         AND GET_WORK_DATE(LAST_TRAN_TIME, 'D') = '" + cdvDate.Text.Replace("-", "")  +"'" + "\n");

                //if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                //{
                //    strSqlString.Append("         AND LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
                //}

                //strSqlString.Append("    GROUP BY MAT_ID " + "\n");

                strSqlString.Append("        SELECT MAT_ID" + "\n");
                strSqlString.Append("             , SUM(QTY_1) AS RTN_DAY" + "\n");
                strSqlString.Append("          FROM RWIPLOTHIS" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND OLD_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("           AND OWNER_CODE = 'PROD'" + "\n");
                strSqlString.Append("           AND OPER IN ('AZ010', 'AZ009')  " + "\n");
                strSqlString.Append("           AND CREATE_CODE = 'RETN'   " + "\n");
                strSqlString.AppendFormat("           AND TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", yesterday + "220000", date + "215959");
                strSqlString.Append("           AND HIST_DEL_FLAG = ' '" + "\n");
                
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("           AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                }


                strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            }
            strSqlString.Append("       ) D           " + "\n");

            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("            , SUM(SHP_QTY_1) AS SHIP_DAY " + "\n");
            strSqlString.Append("         FROM VSUMWIPSHP_ONLY " + "\n");
            strSqlString.Append("        WHERE 1 = 1 " + "\n");
            strSqlString.Append("          AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("          AND CM_KEY_2 = 'PROD'" + "\n");
            strSqlString.Append("          AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("          AND WORK_DATE = '"+  date +"'" + "\n");
            strSqlString.Append("        GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) E                  " + "\n");

            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("             , SUM(SHP_QTY_1) AS AC_T_DAY " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT WORK_DATE " + "\n");
            strSqlString.Append("                     , MAT_ID " + "\n");
            strSqlString.Append("                     , LOT_TYPE " + "\n");
            strSqlString.Append("                     , CM_KEY_2 " + "\n");
            strSqlString.Append("                     , S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1+S4_FAC_OUT_QTY_1 AS SHP_QTY_1 " + "\n");
            strSqlString.Append("                  FROM RSUMFACMOV " + "\n");
            strSqlString.Append("                 WHERE FACTORY='" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND CM_KEY_1='" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");            
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("         WHERE 1 = 1 " + "\n");
            strSqlString.Append("           AND CM_KEY_2 = 'PROD'" + "\n");
            strSqlString.Append("           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("           AND WORK_DATE = '" + date + "'" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) F                  " + "\n");
            strSqlString.Append(" WHERE 1 = 1 " + "\n");
            strSqlString.Append("   AND A.MAT_ID NOT IN (SELECT MAT_ID FROM MWIPMATDEF WHERE FIRST_FLOW = 'A-BANK' AND DELETE_FLAG = ' ')" + "\n");
            strSqlString.Append("   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND A.MAT_ID = C.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND A.MAT_ID = D.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND A.MAT_ID = E.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND A.MAT_ID = F.MAT_ID(+)" + "\n");

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.AppendFormat(" GROUP BY {0} " + "\n", QueryCond1);

            strSqlString.Append("HAVING (" + "\n");
            
            if (ckbWeek.Checked == true)
            {
                strSqlString.Append("        NVL(SUM(A.WEEK1_PLAN), 0)+  " + "\n");
                strSqlString.Append("        NVL(SUM(A.WEEK2_PLAN), 0)+  " + "\n");                
                strSqlString.Append("        NVL(SUM(A.ASSY_WEEK), 0)+          " + "\n");
                strSqlString.Append("        NVL(SUM(A.SHIP_WEEK), 0)+ " + "\n");                               
            }
            else
            {
                strSqlString.Append("        NVL(SUM(A.MON_PLAN), 0)+  " + "\n");
                if (ckbRev.Checked == true)
                {
                    strSqlString.Append("        NVL(SUM(A.ORI_PLAN), 0)+  " + "\n");
                } 

                strSqlString.Append("        NVL(SUM(A.ASSY_MON), 0)+          " + "\n");
                strSqlString.Append("        NVL(SUM(A.SHIP_MON), 0)+ " + "\n");            
                strSqlString.Append("        NVL(SUM(A.ASSY_DAY), 0)+ " + "\n");
            }

            strSqlString.Append("        NVL(SUM(A.RCV_DAY), 0)+ " + "\n");                
            strSqlString.Append("        NVL(SUM(E.SHIP_DAY), 0)+ " + "\n");            
            strSqlString.Append("        NVL(SUM(B.V0+B.V1+B.V2+B.V3+B.V4+B.V5+B.V6+B.V7+B.V8+B.V9+B.V10+B.V11+B.V12+B.V13+B.V14+B.V15+B.V16), 0)+  " + "\n");
            strSqlString.Append("        NVL(SUM(B.V0), 0)+ NVL(SUM(B.V1), 0)+ NVL(SUM(B.V2), 0)+ NVL(SUM(B.V3), 0)+ NVL(SUM(B.V4), 0)+ NVL(SUM(B.V5), 0)+" + "\n");
            strSqlString.Append("        NVL(SUM(B.V6), 0)+ NVL(SUM(B.V7), 0)+ NVL(SUM(B.V8), 0)+ NVL(SUM(B.V9), 0)+ NVL(SUM(B.V10), 0)+ NVL(SUM(B.V11), 0)+ " + "\n");
            strSqlString.Append("        NVL(SUM(B.V12+B.V13), 0)+ NVL(SUM(B.V14), 0)+ NVL(SUM(B.V15), 0)+ NVL(SUM(B.V16), 0)+  " + "\n");
            strSqlString.Append("        NVL(SUM(D.RTN_DAY), 0)+ " + "\n");
            strSqlString.Append("        NVL(SUM(C.V0), 0)+ NVL(SUM(C.V1), 0)+ NVL(SUM(C.V2), 0)+ NVL(SUM(C.V3), 0)+ " + "\n");
            strSqlString.Append("        NVL(SUM(C.V5), 0)+ NVL(SUM(C.V7), 0)+ " + "\n"); // NVL(SUM(C.V6), 0) NVL(SUM(C.V4), 0) 삭제
            strSqlString.Append("        NVL(SUM(A.ASSY_YESTERDAY), 0)+ " + "\n");
            strSqlString.Append("        NVL(SUM(A.SHIP_YESTERDAY), 0)" + "\n");
            strSqlString.Append("       ) <> 0" + "\n");

            strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond1);

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
            strSqlString.Append("            LAST_DAY('" + getDate + "')-2 AS \"SYSLSI\", " + "\n");
            strSqlString.Append("            LAST_DAY('" + getDate + "') AS \"MEMO\",  " + "\n");
            //strSqlString.Append("            LAST_DAY('" + getDate + "')- 2 - TO_DATE('" + getDate + "')  AS REMAIN1,  " + "\n");
            //strSqlString.Append("            LAST_DAY('" + getDate + "') - TO_DATE('" + getDate + "')  AS REMAIN2,  " + "\n");

            //금일 포함해서 잔여일 계산
            strSqlString.Append("            LAST_DAY('" + getDate + "')- 2 - TO_DATE('" + getDate + "') +1  AS REMAIN1,  " + "\n");
            strSqlString.Append("            LAST_DAY('" + getDate + "') - TO_DATE('" + getDate + "')+1  AS REMAIN2,  " + "\n");

            strSqlString.Append("            SYSDATE AS TODAY  " + "\n");
            strSqlString.Append("        FROM DUAL " + "\n");

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
            DataTable dt2 = null;

            if (CheckField() == false) return;

            GridColumnInit();
            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());
                LabelTextChange(dt2);
                
                if (dt2 == null) return;

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

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 11, null, null, btnSort);                
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함
                //spdData.Sheets[0].FrozenColumnCount = 3;
                //spdData.RPT_AutoFit(false);

                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 1, true, Align.Center, VerticalAlign.Center);                                

                #region 진도율 Total 구하기
                //YIELD 부분의  TOTAL값 및 SUB TOTAL을 계산하지 않고 직접 계산 
                
                string subtotal = null;

                for (int i = 0; i < spdData.ActiveSheet.Columns.Count; i++)
                {
                    if (spdData.ActiveSheet.Columns[i].Label == "Progress rate")
                    {
                        if (ckbRev.Checked == true)
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
                                            if (Convert.ToInt32(spdData.ActiveSheet.Cells[j, i - 4].Value) == 0 || Convert.ToInt32(spdData.ActiveSheet.Cells[j, i - 2].Value) == 0)
                                            {
                                                spdData.ActiveSheet.Cells[j, i].Value = 0;
                                            }
                                            else
                                            {
                                                spdData.ActiveSheet.Cells[j, i].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[j, i - 2].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[j, i - 4].Value)) * 100).ToString();
                                            }
                                        }
                                    } // if (subtotal.Length > 5)
                                } //for (int k = sub + 1; k > 0; k--)
                            } //for (int j = 0; j < spdData.ActiveSheet.Rows.Count; j++)
                        }
                        else
                        {
                            if (Convert.ToInt32(spdData.ActiveSheet.Cells[0, i - 5].Value) == 0 || Convert.ToInt32(spdData.ActiveSheet.Cells[0, i - 2].Value) == 0)
                            {
                                spdData.ActiveSheet.Cells[0, i].Value = 0;
                            }
                            else
                            {
                                spdData.ActiveSheet.Cells[0, i].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 2].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 5].Value)) * 100).ToString();
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
                                            if (Convert.ToInt32(spdData.ActiveSheet.Cells[j, i - 5].Value) == 0 || Convert.ToInt32(spdData.ActiveSheet.Cells[j, i - 2].Value) == 0)
                                            {
                                                spdData.ActiveSheet.Cells[j, i].Value = 0;
                                            }
                                            else
                                            {
                                                spdData.ActiveSheet.Cells[j, i].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[j, i - 2].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[j, i - 5].Value)) * 100).ToString();
                                            }
                                        }
                                    } // if (subtotal.Length > 5)
                                } //for (int k = sub + 1; k > 0; k--)
                            } //for (int j = 0; j < spdData.ActiveSheet.Rows.Count; j++)

                        }
                    } // if (spdData.ActiveSheet.Columns[i].Label == "Progress rate")
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
                Condition.AppendFormat("금일실적기준: {0}    SYSLSI: {1}     월마감: {2}     진도율: {3}    잔여일수: " + lblRemain.Text.ToString() + "    ", lblToday.Text.ToString(), lblSyslsi.Text.ToString(), lblMagam.Text.ToString(), lblJindo.Text.ToString());
                Condition.Append(" ||   ");
                Condition.AppendFormat("전일실적기준: {0}    MEMO:   {1}     월마감: {2}     진도율: {3}    잔여일수: " + lblRemain2.Text.ToString(), lblYesterday.Text.ToString(), lblMemo.Text.ToString(), lblMagam2.Text.ToString(), lblJindo2.Text.ToString());

                ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);
                //spdData.ExportExcel();            
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

            // ASSY 진도는 LSI=해당월 말일 - 2일 기준,MEMORY 해당월 말일 기준으로 현재일 계산함.
            string magam1 = dt.Rows[0][0].ToString().Substring(8, 2);
            string magam2 = dt.Rows[0][1].ToString().Substring(8, 2);
            //string today = dt.Rows[0][4].ToString().Substring(8, 2);
            string selectday = strDate.Substring(6, 2);

            double jindo = (Convert.ToDouble(selectday)) / Convert.ToDouble(magam1) * 100;
            double jindo2 = (Convert.ToDouble(selectday)) / Convert.ToDouble(magam2) * 100;

            //double jindo = (Convert.ToDouble(selectday)-1) / Convert.ToDouble(magam1) * 100;
            //double jindo2 = (Convert.ToDouble(selectday)-1) / Convert.ToDouble(magam2) * 100

            // 진도율 소수점 1째자리 까지 표시 (2009.08.17 임종우)
            decimal jindoPer = Math.Round(Convert.ToDecimal(jindo),1);
            decimal jindoPer2 = Math.Round(Convert.ToDecimal(jindo2), 1);
            //int jindoPer = Convert.ToInt32(jindo);
            //int jindoPer2 = Convert.ToInt32(jindo2);

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
            lblJindo2.Text = jindoPer2.ToString() + "%";
            lblRemain.Text = dt.Rows[0][2].ToString();
            lblRemain2.Text = dt.Rows[0][3].ToString();
        }
        #endregion

       
    }       
}
