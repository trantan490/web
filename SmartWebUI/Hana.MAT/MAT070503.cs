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
    public partial class MAT070503 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: MAT070503<br/>
        /// 클래스요약: 원부자재 투입 점검<br/>
        /// 작  성  자: 김태호<br/>
        /// 최초작성일: 2010-08-16<br/>
        /// 상세  설명: 원부자재 투입 점검<br/>
        /// 변경  내용: <br/>//
        /// 2013-12-24-임종우 : 컬럼명 수정 : SMT -> 자사 SMT, 외주 -> 외주 SMT (김권수 요청)
        /// 2014-02-27-임종우 : 공정투입 대기 자재는 공정창고에 포함되도록 수정 (김권수D 요청)
        /// 2014-03-10-임종우 : 재고 수량 중복 오류 수정
        /// 2014-04-25-임종우 : 보유현황, 발주 잔량 POPUP 창 VENDOR 정보 추가 (임태성K 요청)
        /// 2014-04-25-임종우 : 보유현황 쿼리 튜닝
        /// 2014-07-01-임종우 : 발주 현황 상세 팝업 데이터 오류 수정
        /// 2014-08-18-임종우 : 발주 현황 상세 팝업에서 누계차는 업데이트한 날짜 기준 부터 계산 되도록...
        ///                   : 단 해당 데이터 복사 할 경우에는 계획, 실적 데이터는 기존 처럼 1일부터 나오도록 (임태성K 요청)
        /// 2015-05-27-임종우 : 자재 타입 RC 추가 (최연희D 요청)
        /// 2015-09-24-임종우 : 고객사 명 하드 코딩 되어 있는것을 기준정보로 변경 (임태성K 요청)
        /// 2019-01-30-임종우 : 창고 재고 과거 데이터 테이블 변경 ZHMMT111@SAPREAL -> CWMSLOTSTS_BOH
        /// 2019-10-25-임종우 : HRTDMCPROUT@RPTTOMES -> RWIPMCPBOM 변경
        /// 2020-03-03-김미경 : usage 수정 =: A0000~A0399 공정의 원부자재에 한해 원부자재 사용량(Usage) = Usage * LOSS_QTY(H_HX_AUTO_LOSS, H_SEC_AUTO_LOSS ADAPT_OPER 'A0395'의 LOSS_QTY) - 이승희 D
        #region " MAT070503 : Program Initial "

        private DataTable dtWeek = new DataTable();
        private DataTable dtWeekFday = new DataTable();
        private DataTable dtWeekList = new DataTable();

        string[] dayArry = new string[3];
        string[] dayArry2 = new string[3];

        GlobalVariable.FindWeek FindWeek = new GlobalVariable.FindWeek();
        
        private int iViewWeekPlanCount = 8;
        
        private int ClickPos1 = 0;        
        private int ClickPos5 = 0;


        public MAT070503()
        {
            InitializeComponent();

            rdbWeek.Checked = true;
            cdvDate.Value = DateTime.Now;
            SortInit();
            GridColumnInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;           
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

            return true;
        }

        #endregion

        #region " GridColumnInit : Sheet Title 설정 "

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            GetWorkWeek();
            GetFirstDayWeek();
            GetWeekList();

            // 지난달 마지막일 구하기
            string ss = DateTime.Now.ToString("MM-dd");
            FindWeek = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");

            string selectWeek = dtWeek.Rows[0][0].ToString();
            string selectMonth = cdvDate.Value.ToString("MM");

            try
            {
                spdData.RPT_ColumnInit();

                if (ckbDetail.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    //spdData.RPT_AddBasicColumn("Major Code", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PKG Code", 0, 6, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Pin Type", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

                    spdData.RPT_AddBasicColumn("Material type", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("MATCODE", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Item name", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 250);
                    spdData.RPT_AddBasicColumn("SAP CODE", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                    spdData.RPT_AddBasicColumn("Unit", 0, 12, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("STEP", 0, 13, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("USAGE", 0, 14, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.String, 50);

                    spdData.RPT_AddBasicColumn("WIP against excess or deficiency", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("STOCK WIP", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Quantity available in consideration of warehouse", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Overs and shorts ", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Need to wear raw materials", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 15, 4);

                    if (rdbMonth.Checked == true)
                    {
                        spdData.RPT_AddBasicColumn(selectMonth + " " + "월 생산계획 대비 잔량", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn(selectMonth + " " + "월 계획", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    else if (rdbWeek.Checked == true)
                    {
                        spdData.RPT_AddBasicColumn("WW" + dtWeekList.Rows[0][0].ToString().Substring(4, 2) + " 생산계획 대비 잔량", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("WW" + dtWeekList.Rows[0][0].ToString().Substring(4, 2) + " 계획", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("Work Completion quantity", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Process standard residual quantity", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                    spdData.RPT_AddBasicColumn("Raw materials required", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                    spdData.RPT_AddBasicColumn("Overs and shorts ", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 19, 5);

                    spdData.RPT_AddBasicColumn("Status of possession", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("TOTAL", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Own SMT", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Line", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Outsourcing SMT", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("On-site warehouse", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Warehouse (purchase)", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 24, 6);

                    spdData.RPT_AddBasicColumn("Remaining Order", 0, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                    spdData.RPT_AddBasicColumn("Plan by work week", 0, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                    for (int i = 0; i < 8; i++)
                    {
                        if (i == 0)
                            spdData.RPT_AddBasicColumn(dtWeekList.Rows[i][0].ToString().Substring(4, 2) + "주 잔량", 1, 31 + i, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        else
                            spdData.RPT_AddBasicColumn(dtWeekList.Rows[i][0].ToString().Substring(4, 2) + "주차", 1, 31 + i, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }

                    spdData.RPT_MerageHeaderColumnSpan(0, 31, 8);

                    spdData.RPT_AddBasicColumn("over and short quantity by work week", 0, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                    for (int i = 0; i < 8; i++)
                    {
                        spdData.RPT_AddBasicColumn(dtWeekList.Rows[i][0].ToString().Substring(4, 2) + "주차", 1, 39 + i, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }

                    spdData.RPT_MerageHeaderColumnSpan(0, 39, 8);

                    spdData.RPT_AddBasicColumn("1", 0, 47, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 10);
                    spdData.RPT_AddBasicColumn("2", 1, 47, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 10);
                    spdData.RPT_AddBasicColumn("3", 2, 47, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 10);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    //spdData.RPT_AddBasicColumn("Major Code", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PKG Code", 0, 6, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Pin Type", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

                    spdData.RPT_AddBasicColumn("Material type", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("MATCODE", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Item name", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 250);
                    spdData.RPT_AddBasicColumn("SAP CODE", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                    spdData.RPT_AddBasicColumn("Unit", 0, 12, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("STEP", 0, 13, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("USAGE", 0, 14, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.String, 50);

                    spdData.RPT_AddBasicColumn("WIP against excess or deficiency", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("STOCK WIP", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Quantity available in consideration of warehouse", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Overs and shorts ", 1, 17, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Need to wear raw materials", 1, 18, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 15, 4);

                    if (rdbMonth.Checked == true)
                    {
                        spdData.RPT_AddBasicColumn(selectMonth + " 월 생산계획 대비 잔량", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn(selectMonth + " 월 계획", 1, 19, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    else if (rdbWeek.Checked == true)
                    {
                        spdData.RPT_AddBasicColumn("WW" + selectWeek + " 생산계획 대비 잔량", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("WW" + selectWeek + " 계획", 1, 19, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("Work Completion quantity", 1, 20, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Process standard residual quantity", 1, 21, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                    spdData.RPT_AddBasicColumn("Raw materials required", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Overs and shorts ", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 19, 5);

                    spdData.RPT_AddBasicColumn("Status of possession", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("TOTAL", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SMT", 1, 25, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Line", 1, 26, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("outsourcing", 1, 27, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("On-site warehouse", 1, 28, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Warehouse (purchase)", 1, 29, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 24, 6);

                    spdData.RPT_AddBasicColumn("Remaining Order", 0, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                    spdData.RPT_AddBasicColumn("Plan by work week", 0, 31, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                    for (int i = 0; i < 8; i++)
                    {
                        if (i == 0)
                            spdData.RPT_AddBasicColumn(dtWeekList.Rows[i][0].ToString().Substring(4, 2) + "주 잔량", 1, 31 + i, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        else
                            spdData.RPT_AddBasicColumn(dtWeekList.Rows[i][0].ToString().Substring(4, 2) + "주차", 1, 31 + i, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }

                    spdData.RPT_MerageHeaderColumnSpan(0, 31, 8);

                    spdData.RPT_AddBasicColumn("over and short quantity by work week", 0, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                    for (int i = 0; i < 8; i++)
                    {
                        spdData.RPT_AddBasicColumn(dtWeekList.Rows[i][0].ToString().Substring(4, 2) + "주차", 1, 39 + i, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }

                    spdData.RPT_MerageHeaderColumnSpan(0, 39, 8);

                    spdData.RPT_AddBasicColumn("1", 0, 47, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 10);
                    spdData.RPT_AddBasicColumn("2", 1, 47, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 10);
                    spdData.RPT_AddBasicColumn("3", 2, 47, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 10);
                }

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
                spdData.RPT_MerageHeaderRowSpan(0, 13, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 14, 3);

                spdData.RPT_MerageHeaderRowSpan(1, 15, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 16, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 17, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 18, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 19, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 20, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 21, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 22, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 23, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 24, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 25, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 26, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 27, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 28, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 29, 2);

                spdData.RPT_MerageHeaderRowSpan(0, 30, 3);

                spdData.RPT_MerageHeaderRowSpan(1, 31, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 32, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 33, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 34, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 35, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 36, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 37, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 38, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 39, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 40, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 41, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 42, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 43, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 44, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 45, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 46, 2);

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
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAT TYPE", "MATTYPE", "A.MATTYPE", "A.MATTYPE", "MATTYPE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "(SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = CUSTOMER) AS CUSTOMER", "MAT.MAT_GRP_1 AS CUSTOMER", "MAT.MAT_GRP_1", "DECODE(A.CUSTOMER, 'SE', 1, 'HX', 2, 'IM', 3, 'FC', 4, 'IG', 5, 6),CUSTOMER", true);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "MAJOR_CODE", " MAT_GRP_9", "MAT.MAT_GRP_9", "", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "FAMILY", "MAT.MAT_GRP_2 AS FAMILY", "MAT.MAT_GRP_2", "FAMILY", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "PACKAGE", "MAT_GRP_10 AS PACKAGE", "MAT.MAT_GRP_10", "PACKAGE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "Type1", "MAT.MAT_GRP_4 AS Type1", "MAT.MAT_GRP_4", "Type1", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "Type2", "MAT.MAT_GRP_5 AS Type2", "MAT.MAT_GRP_5", "Type2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "LD_COUNT", "MAT.MAT_GRP_6 AS LD_COUNT", "MAT.MAT_GRP_6", "LD_COUNT", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "PKG_CODE", "MAT.MAT_CMF_11 AS PKG_CODE", "MAT.MAT_CMF_11", "PKG_CODE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "PIN_TYPE", "MAT.MAT_CMF_10 AS PIN_TYPE", "MAT.MAT_CMF_10", "PIN_TYPE", false);

                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MATCODE", "MATCODE", "REPLACE(A.MATCODE, '-O','') AS MATCODE", "REPLACE(A.MATCODE, '-O','')", "MATCODE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DESCRIPT", "DESCRIPT", "A.DESCRIPT AS DESCRIPT", "A.DESCRIPT", "DESCRIPT", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SAP CODE", "SAPCODE", "MAT.VENDOR_ID AS SAPCODE", "MAT.VENDOR_ID", "SAPCODE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("단위", "UNIT", "A.UNIT", "A.UNIT", "UNIT", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("STEP", "OPER", "A.OPER", "A.OPER", "OPER", true);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        #endregion

        #region " MakeSqlString : Sql Query문 "

        #region 시간관련 함수
        private void GetWorkWeek()
        {
            dtWeek = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());

        }
        private void GetFirstDayWeek()
        {
            dtWeekFday = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2("MIN", cdvDate.Value.ToString("yyyy"), dtWeek.Rows[0][0].ToString()));
        }
        private void GetWeekList()
        {
            dtWeekList = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString3());
        }

        #endregion

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
            string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값
            string Today;
            string sMonth;
            string start_date;
            string date;
            string month;
            string year;
            string lastMonth;

            Today = cdvDate.Value.ToString("yyyyMMdd");
            sMonth = cdvDate.Value.ToString("yyyyMM");

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;


            string selectDate = cdvDate.Value.ToString("yyyyMMdd"); //선택한 날짜

            date = cdvDate.SelectedValue();

            DateTime Select_date;
            Select_date = DateTime.Parse(cdvDate.Text.ToString());

            year = Select_date.ToString("yyyy");
            month = Select_date.ToString("yyyyMM");
            start_date = month + "01";
            lastMonth = Select_date.AddMonths(-1).ToString("yyyyMM"); // 지난달

            // 지난달 마지막일 구하기
            DataTable dt1 = null;
            string Last_Month_Last_day = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + lastMonth + "', 'YYYYMM')),'YYYYMMDD') FROM DUAL)";
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", Last_Month_Last_day);
            Last_Month_Last_day = dt1.Rows[0][0].ToString();

            // 지난주차의 마지막일 가져오기
            dt1 = null;
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString4(year, Select_date.ToString("yyyyMMdd")));
            string Lastweek_lastday = dt1.Rows[0][0].ToString();


            dayArry[0] = cdvDate.Value.AddDays(-2).ToString("MM.dd");
            dayArry[1] = cdvDate.Value.AddDays(-1).ToString("MM.dd");
            dayArry[2] = cdvDate.Value.ToString("MM.dd");

            dayArry2[0] = cdvDate.Value.AddDays(-2).ToString("yyyyMMdd");
            dayArry2[1] = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");
            dayArry2[2] = cdvDate.Value.ToString("yyyyMMdd");

            #region 상세 조회에 따른 SQL문 생성

            if (ckbKpcs.Checked == true)
                sKpcsValue = "1000";
            else
                sKpcsValue = "1";

            if (rdbMonth.Checked == true)
            {
                #region 월간 조회

                #endregion 월간 조회
            }
            else if (rdbWeek.Checked == true)
            {
                #region 주간 조회

                #endregion 주간 조회
            }

            if (rdbMonth.Checked == true)
            {
                strSqlString.Append("SELECT " + QueryCond1 + " " + "\n");
                strSqlString.Append("     , RTRIM(TO_CHAR(USAGE, 'FM9990D9999'), '.') AS USAGE " + "\n");

            
                strSqlString.Append("     , ROUND(WIP_STOCK/" + sKpcsValue + ",0) AS WIP_STOCK " + "\n");
                strSqlString.Append("     , ROUND(((MAT_TTL/USAGE*1000)-REV_QTY)/" + sKpcsValue + ",0) AS CMM " + "\n");
                strSqlString.Append("     , ROUND((((MAT_TTL/USAGE*1000)-REV_QTY)-WIP_STOCK)/" + sKpcsValue + ",0) AS \"재공대비과부족\" " + "\n");
                strSqlString.Append("     , CASE WHEN ((MAT_TTL/USAGE*1000)-REV_QTY)-WIP_STOCK<0 THEN ROUND((ABS(((MAT_TTL/USAGE*1000)-REV_QTY)-WIP_STOCK)/USAGE*1000)/" + sKpcsValue + ",1) ELSE 0 END \"원부자재입고필요\" " + "\n");
                strSqlString.Append("     , ROUND(SOP_PLN_MON/" + sKpcsValue + ",0) AS SOP_PLN " + "\n");
                strSqlString.Append("     , ROUND(ASSY_MON/" + sKpcsValue + ",0) AS ASSY_MON " + "\n");
                strSqlString.Append("     , ROUND((SOP_PLN_MON-ASSY_MON)/" + sKpcsValue + ",1) AS \"공정기준잔량\" " + "\n");
                strSqlString.Append("     , ROUND(((SOP_PLN_MON-ASSY_MON)*USAGE/1000)/" + sKpcsValue + ",1) AS \"원부자재필요\" " + "\n");
                strSqlString.Append("     , ROUND((MAT_TTL-((SOP_PLN_MON-ASSY_MON)*USAGE/1000))/" + sKpcsValue + ",0) AS \"계획대비과부족\" " + "\n");
                strSqlString.Append("     , ROUND(MAT_TTL/" + sKpcsValue + ",0) AS MAT_TTL " + "\n");
                strSqlString.Append("     , ROUND(MAT_SMT_IN/" + sKpcsValue + ",0) AS MAT_SMT_IN " + "\n");
                strSqlString.Append("     , ROUND(MAT_L_IN/" + sKpcsValue + ",0) AS MAT_L_IN " + "\n");
                strSqlString.Append("     , ROUND(WIK_WIP/" + sKpcsValue + ",0) AS WIK_WIP " + "\n");
                strSqlString.Append("     , ROUND(MAT_INV_L_QTY/" + sKpcsValue + ",0) AS MAT_INV_L_QTY " + "\n");
                strSqlString.Append("     , ROUND(MAT_INV_QTY/" + sKpcsValue + ",0) AS MAT_INV_QTY " + "\n");
                strSqlString.Append("     , ROUND(ORDER_QTY/" + sKpcsValue + ",0) AS ORDER_QTY " + "\n");
            }
            else if (rdbWeek.Checked == true)
            {
                strSqlString.Append("SELECT " + QueryCond1 + " " + "\n");
                strSqlString.Append("     , RTRIM(TO_CHAR(USAGE, 'FM9990D9999'), '.') AS USAGE " + "\n");
                strSqlString.Append("     , ROUND(WIP_STOCK/" + sKpcsValue + ",0) AS WIP_STOCK " + "\n");
                strSqlString.Append("     , ROUND(((MAT_TTL/USAGE*1000)-REV_QTY)/" + sKpcsValue + ",0) AS CMM " + "\n");
                strSqlString.Append("     , ROUND((((MAT_TTL/USAGE*1000)-REV_QTY)-WIP_STOCK)/" + sKpcsValue + ",0) AS \"재공대비과부족\" " + "\n");
                strSqlString.Append("     , CASE WHEN ((MAT_TTL/USAGE*1000)-REV_QTY)-WIP_STOCK<0 THEN ROUND((ABS(((MAT_TTL/USAGE*1000)-REV_QTY)-WIP_STOCK)*USAGE/1000)/" + sKpcsValue + ",1) ELSE 0 END \"원부자재입고필요\" " + "\n");
                strSqlString.Append("     , ROUND(SOP_PLN_WEEK/" + sKpcsValue + ",0) AS SOP_PLN" + "\n");
                strSqlString.Append("     , ROUND(ASSY_WEEK/" + sKpcsValue + ",0) AS ASSY_WEEK " + "\n");
                strSqlString.Append("     , ROUND((SOP_PLN_WEEK-ASSY_WEEK)/" + sKpcsValue + ",1) AS \"공정기준잔량\" " + "\n");
                strSqlString.Append("     , ROUND(((SOP_PLN_WEEK-ASSY_WEEK)*USAGE/1000)/" + sKpcsValue + ",1) AS \"원부자재필요\" " + "\n");
                strSqlString.Append("     , ROUND((MAT_TTL-((SOP_PLN_WEEK-ASSY_WEEK)*USAGE/1000))/" + sKpcsValue + ",0) AS \"계획대비과부족\" " + "\n");
                strSqlString.Append("     , ROUND(MAT_TTL/" + sKpcsValue + ",0) AS MAT_TTL " + "\n");
                strSqlString.Append("     , ROUND(MAT_SMT_IN/" + sKpcsValue + ",0) AS MAT_SMT_IN " + "\n");
                strSqlString.Append("     , ROUND(MAT_L_IN/" + sKpcsValue + ",0) AS MAT_L_IN " + "\n");
                strSqlString.Append("     , ROUND(WIK_WIP/" + sKpcsValue + ",0) AS WIK_WIP " + "\n");
                strSqlString.Append("     , ROUND(MAT_INV_L_QTY/" + sKpcsValue + ",0) AS MAT_INV_L_QTY " + "\n");
                strSqlString.Append("     , ROUND(MAT_INV_QTY/" + sKpcsValue + ",0) AS MAT_INV_QTY " + "\n");
                strSqlString.Append("     , ROUND(ORDER_QTY/" + sKpcsValue + ",0) AS ORDER_QTY " + "\n");                
            }

            strSqlString.Append("     , ROUND((SOP_PLN_WEEK-ASSY_WEEK)/" + sKpcsValue + ",0) AS W0 " + "\n");

            for (int i = 1; i < 8; i++)
                strSqlString.Append("     , ROUND(W" + i + "/" + sKpcsValue + ",0) AS W" + i + " " + "\n");

            strSqlString.Append("     , ROUND((MAT_TTL-((SOP_PLN_WEEK-ASSY_WEEK)*USAGE/1000))/" + sKpcsValue + ",0) AS MW0 " + "\n");
            strSqlString.Append("     , ROUND((MAT_TTL-((SOP_PLN_WEEK-ASSY_WEEK)*USAGE/1000) - (W1*USAGE/1000))/" + sKpcsValue + ",0) AS MW1 " + "\n");
            strSqlString.Append("     , ROUND((MAT_TTL-((SOP_PLN_WEEK-ASSY_WEEK)*USAGE/1000) - (W1*USAGE/1000) - (W2*USAGE/1000))/" + sKpcsValue + ",0) AS MW2 " + "\n");
            strSqlString.Append("     , ROUND((MAT_TTL-((SOP_PLN_WEEK-ASSY_WEEK)*USAGE/1000) - (W1*USAGE/1000) - (W2*USAGE/1000) - (W3*USAGE/1000))/" + sKpcsValue + ",0) AS MW3 " + "\n");
            strSqlString.Append("     , ROUND((MAT_TTL-((SOP_PLN_WEEK-ASSY_WEEK)*USAGE/1000) - (W1*USAGE/1000) - (W2*USAGE/1000) - (W3*USAGE/1000) - (W4*USAGE/1000))/" + sKpcsValue + ",0) AS MW4 " + "\n");
            strSqlString.Append("     , ROUND((MAT_TTL-((SOP_PLN_WEEK-ASSY_WEEK)*USAGE/1000) - (W1*USAGE/1000) - (W2*USAGE/1000) - (W3*USAGE/1000) - (W4*USAGE/1000) - (W5*USAGE/1000))/" + sKpcsValue + ",0) AS MW5 " + "\n");
            strSqlString.Append("     , ROUND((MAT_TTL-((SOP_PLN_WEEK-ASSY_WEEK)*USAGE/1000) - (W1*USAGE/1000) - (W2*USAGE/1000) - (W3*USAGE/1000) - (W4*USAGE/1000) - (W5*USAGE/1000) - (W6*USAGE/1000))/" + sKpcsValue + ",0) AS MW6 " + "\n");
            strSqlString.Append("     , ROUND((MAT_TTL-((SOP_PLN_WEEK-ASSY_WEEK)*USAGE/1000) - (W1*USAGE/1000) - (W2*USAGE/1000) - (W3*USAGE/1000) - (W4*USAGE/1000) - (W5*USAGE/1000) - (W6*USAGE/1000) - (W7*USAGE/1000))/" + sKpcsValue + ",0) AS MW7 " + "\n");



            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT " + QueryCond2 + "\n");
            strSqlString.Append("             , SUM(A.SOP_PLN_MON) AS SOP_PLN_MON " + "\n");
            strSqlString.Append("             , SUM(A.SOP_PLN_WEEK) AS SOP_PLN_WEEK " + "\n");
            strSqlString.Append("             , SUM(A.WEEK_AO+A.WIP_STOCK_NEXT) AS ASSY_WEEK " + "\n");
            strSqlString.Append("             , SUM(A.ASSY_MON+A.WIP_STOCK_NEXT) AS ASSY_MON " + "\n");
            strSqlString.Append("             , SUM(A.WIP_STOCK) AS WIP_STOCK " + "\n");
            strSqlString.Append("             , SUM(A.REV_QTY) AS REV_QTY " + "\n");
            strSqlString.Append("             , MAX(CASE WHEN OPER BETWEEN 'A0000' AND 'A0399' THEN LOSS_QTY * A.USAGE ELSE A.USAGE END) USAGE " + "\n");
            strSqlString.Append("             , MAX(A.TTL) AS MAT_TTL " + "\n");
            strSqlString.Append("             , MAX(A.SMT_IN) AS MAT_SMT_IN " + "\n");
            strSqlString.Append("             , MAX(A.L_IN) AS MAT_L_IN " + "\n");
            strSqlString.Append("             , MAX(A.WIK_WIP) AS WIK_WIP " + "\n");
            strSqlString.Append("             , MAX(A.INV_L_QTY) AS MAT_INV_L_QTY " + "\n");
            strSqlString.Append("             , MAX(A.INV_QTY) AS MAT_INV_QTY " + "\n");
            strSqlString.Append("             , MAX(A.ORDER_QTY) AS ORDER_QTY " + "\n");
            strSqlString.Append("             , MAX(A.WIP_STOCK-A.IN_QTY) AS  IN_QTY " + "\n");

            for (int i = 0; i < iViewWeekPlanCount; i++)
                strSqlString.Append("             , SUM(A.W" + i + ") AS W" + i + " " + "\n");

            strSqlString.Append("        FROM MWIPMATDEF MAT " + "\n");
            strSqlString.Append("           ,  ( " + "\n");
            strSqlString.Append("              SELECT MAT.MAT_ID " + "\n");
            strSqlString.Append("                   , C.MATTYPE " + "\n");
            strSqlString.Append("                   , C.MATCODE " + "\n");
            strSqlString.Append("                   , C.DESCRIPT " + "\n");
            strSqlString.Append("                   , C.OPER " + "\n");
            strSqlString.Append("                   , C.UNIT " + "\n");
            strSqlString.Append("                   , MAX(A.SOP_PLN_MON) AS  SOP_PLN_MON " + "\n");
            strSqlString.Append("                   , MAX(A.SOP_PLN_WEEK) AS  SOP_PLN_WEEK " + "\n");
            strSqlString.Append("                   , MAX(USAGE) AS USAGE " + "\n");
            strSqlString.Append("                   , MAX(A.WEEK_AO) AS WEEK_AO " + "\n");
            strSqlString.Append("                   , MAX(A.ASSY_MON) AS ASSY_MON " + "\n");
            strSqlString.Append("                   , SUM(NVL((CASE WHEN MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' THEN CASE WHEN MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%' THEN CASE WHEN B.OPER = 'A0000' THEN NVL(B.VO,0) ELSE 0 END ELSE 0 END  " + "\n");
            strSqlString.Append("                               ELSE CASE WHEN B.OPER = 'A0000' THEN NVL(B.VO,0) ELSE 0 END " + "\n");
            strSqlString.Append("                           END),0)) AS WIP_STOCK " + "\n");
            strSqlString.Append("                   , SUM(NVL((CASE WHEN MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' THEN CASE WHEN MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%' THEN CASE WHEN (B.OPER BETWEEN C.OPER AND 'AZ010') AND B.OPER <> C.OPER THEN NVL(B.VO,0) ELSE 0 END ELSE 0 END  " + "\n");
            strSqlString.Append("                               ELSE CASE WHEN (B.OPER BETWEEN C.OPER AND 'AZ010') AND B.OPER <> C.OPER THEN NVL(B.VO,0) ELSE 0 END  " + "\n");
            strSqlString.Append("                           END),0)) AS WIP_STOCK_NEXT " + "\n");
            strSqlString.Append("                   , SUM(NVL((CASE WHEN MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' THEN CASE WHEN MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%' THEN CASE WHEN (B.OPER BETWEEN 'A0000' AND C.OPER) AND B.OPER <> 'A0000' THEN NVL(B.VO,0) ELSE 0 END ELSE 0 END  " + "\n");
            strSqlString.Append("                               ELSE CASE WHEN (B.OPER BETWEEN 'A0000' AND C.OPER) AND B.OPER <> 'A0000' THEN NVL(B.VO,0) ELSE 0 END  " + "\n");
            strSqlString.Append("                           END),0)) AS REV_QTY " + "\n");
            strSqlString.Append("                   , MAX(C.TTL) AS TTL " + "\n");
            strSqlString.Append("                   , MAX(C.SMT_IN) AS SMT_IN " + "\n");
            strSqlString.Append("                   , MAX(C.L_IN) AS L_IN " + "\n");
            strSqlString.Append("                   , MAX(C.WIK_WIP) AS WIK_WIP " + "\n");
            strSqlString.Append("                   , MAX(C.INV_L_QTY) AS INV_L_QTY " + "\n");
            strSqlString.Append("                   , MAX(C.INV_QTY) AS INV_QTY " + "\n");
            strSqlString.Append("                   , MAX(C.ORDER_QTY) AS ORDER_QTY " + "\n");
            strSqlString.Append("                   , MAX(C.IN_QTY) AS IN_QTY " + "\n");

            for (int i = 0; i < iViewWeekPlanCount; i++)
                strSqlString.Append("                   , MAX(A.W" + i + ") AS W" + i + " " + "\n");
            strSqlString.Append("                   , MAX(LOSS_QTY) AS LOSS_QTY " + "\n");

            strSqlString.Append("                FROM MWIPMATDEF MAT " + "\n");
            strSqlString.Append("                   , ( " + "\n");
            strSqlString.Append("                      SELECT MAT.MAT_ID   " + "\n");
            strSqlString.Append("                           , MAX(CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(NVL(PLAN.RESV_FIELD1,0)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0) ELSE NVL(PLAN.RESV_FIELD1,0) END) AS SOP_PLN_MON " + "\n");
            strSqlString.Append("                           , MAX(CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN NVL(W_PLN.W0,0) / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE NVL(W_PLN.W0,0) END) AS SOP_PLN_WEEK  " + "\n");
            strSqlString.Append("                           , MAX(CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(NVL(WEEK_AO.WEEK_AO,0)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0) ELSE NVL(WEEK_AO.WEEK_AO,0) END) WEEK_AO " + "\n");
            strSqlString.Append("                           , MAX(CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(NVL(MON_AO.ASSY_MON,0)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0) ELSE NVL(MON_AO.ASSY_MON,0) END) ASSY_MON " + "\n");

            for (int i = 0; i < 8; i++)
                strSqlString.Append("                           , MAX(CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN NVL(W" + i + ",0) / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE NVL(W" + i + ",0) END) AS W" + i + " " + "\n");

            strSqlString.Append("                           , MAX(W_PLN.W_LAST) AS W_LAST " + "\n");
            strSqlString.Append("                        FROM MWIPMATDEF MAT  " + "\n");
            strSqlString.Append("                           , (    " + "\n");
            strSqlString.Append("                              SELECT MAT_ID, SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) AS SHP_QTY  " + "\n");
            strSqlString.Append("                                FROM RSUMFACMOV   " + "\n");
            strSqlString.Append("                               WHERE 1=1   " + "\n");
            strSqlString.Append("                                 AND WORK_DATE BETWEEN '" + dtWeekFday.Rows[0][0] + "' AND '" + Today + "' " + "\n");
            strSqlString.Append("                                 AND LOT_TYPE = 'W'   " + "\n");
            strSqlString.Append("                                 AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "'   " + "\n");
            strSqlString.Append("                                 AND CM_KEY_2 = 'PROD'   " + "\n");

            if (cdvLotType.SelectedItem.ToString() != "ALL")
                strSqlString.Append("                                 AND CM_KEY_3 LIKE '" + cdvLotType.SelectedItem.ToString() + "' " + "\n");
            else
                strSqlString.Append("                                 AND CM_KEY_3 LIKE '%' " + "\n");

            strSqlString.Append("                                 AND FACTORY NOT IN ('RETURN')  " + "\n");
            strSqlString.Append("                               GROUP BY MAT_ID     " + "\n");
            strSqlString.Append("                             ) SHP   " + "\n");




            strSqlString.Append("                          , ( " + "\n");
            strSqlString.Append("                             SELECT MAT_ID " + "\n");

            if (date.Substring(6, 2).Equals("01"))
            {
                strSqlString.Append("                                  , SUM(DECODE(SUBSTR(WORK_DATE,0,6),'" + month + "', NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0),0)) AS ASSY_MON  " + "\n");
                strSqlString.Append("                               FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                              WHERE 1=1 " + "\n");
                strSqlString.Append("                                AND WORK_DATE BETWEEN '" + dayArry2[0] + "' AND '" + date + "'" + "\n");
            }
            else
            {
                strSqlString.Append("                                  , SUM(DECODE(WORK_DATE,'" + Last_Month_Last_day + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                strSqlString.Append("                               FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                               WHERE 1=1 " + "\n");
                strSqlString.Append("                                 AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + date + "'" + "\n");
            }
            strSqlString.Append("                                 AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                                 AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                 AND CM_KEY_2 = 'PROD' " + "\n");

            if (cdvLotType.SelectedItem.ToString() != "ALL")
                strSqlString.Append("                                 AND CM_KEY_3 LIKE '" + cdvLotType.SelectedItem.ToString() + "' " + "\n");

            strSqlString.Append("                                 AND FACTORY NOT IN ('RETURN') " + "\n");
            strSqlString.Append("                               GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                             ) MON_AO " + "\n");
            strSqlString.Append("                           , ( " + "\n");
            strSqlString.Append("                              SELECT MAT_ID " + "\n");
            strSqlString.Append("                                   , SUM(NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0)) AS WEEK_AO  " + "\n");
            strSqlString.Append("                                FROM RSUMFACMOV " + "\n");
            strSqlString.Append("                               WHERE 1=1 " + "\n");
            strSqlString.Append("                                 AND WORK_DATE BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + FindWeek.EndDay_ThisWeek + "'" + "\n");
            strSqlString.Append("                                 AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                                 AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                 AND CM_KEY_2 = 'PROD' " + "\n");
            if (cdvLotType.SelectedItem.ToString() != "ALL")
                strSqlString.Append("                                 AND CM_KEY_3 LIKE '" + cdvLotType.SelectedItem.ToString() + "' " + "\n");


            strSqlString.Append("                                 AND FACTORY NOT IN ('RETURN') " + "\n");
            strSqlString.Append("                               GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                             ) WEEK_AO " + "\n");
            strSqlString.Append("                           , (  " + "\n");
            strSqlString.Append("                              SELECT FACTORY,MAT_ID,PLAN_QTY_ASSY,PLAN_MONTH, RESV_FIELD1  " + "\n");
            strSqlString.Append("                                FROM (  " + "\n");
            strSqlString.Append("                                      SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, PLAN_MONTH, SUM(RESV_FIELD1) AS RESV_FIELD1   " + "\n");
            strSqlString.Append("                                        FROM (  " + "\n");
            strSqlString.Append("                                              SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, PLAN_MONTH, SUM(TO_NUMBER(DECODE(RESV_FIELD1,' ',0,RESV_FIELD1))) AS RESV_FIELD1  " + "\n");
            strSqlString.Append("                                                FROM CWIPPLNMON  " + "\n");
            strSqlString.Append("                                               WHERE 1=1  " + "\n");
            strSqlString.Append("                                                 AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                                               GROUP BY FACTORY, MAT_ID, PLAN_MONTH  " + "\n");
            strSqlString.Append("                                             )  " + "\n");
            strSqlString.Append("                                       GROUP BY FACTORY, MAT_ID,PLAN_MONTH  " + "\n");
            strSqlString.Append("                                     )  " + "\n");
            strSqlString.Append("                                WHERE PLAN_MONTH = '" + cdvDate.Value.ToString("yyyyMM") + "' " + "\n");

            strSqlString.Append("                             ) PLAN " + "\n");



            strSqlString.Append("                           , (   " + "\n");
            strSqlString.Append("                              SELECT MAT_ID   " + "\n");

            if (rdbMonth.Checked == true)
            {
                for (int i = 0; i < 8; i++)
                {
                    strSqlString.Append("                                   , SUM(DECODE(PLAN_WEEK, '" + dtWeekList.Rows[i][0].ToString() + "', REV_QTY, 0)) AS W" + i + "  " + "\n");
                }
            }
            else if (rdbWeek.Checked == true)
            {
                for (int i = 0; i < 8; i++)
                {
                    strSqlString.Append("                                   , SUM(DECODE(PLAN_WEEK, '" + dtWeekList.Rows[i][0].ToString() + "', WW_QTY, 0)) AS W" + i + "  " + "\n");
                }
            }

            strSqlString.Append("                                   , SUM(CASE WHEN PLAN_WEEK >= CKD_S_WEEK AND PLAN_WEEK <= CKD_L_WEEK THEN REV_QTY ELSE 0 END) AS W_LAST   " + "\n");
            strSqlString.Append("                                FROM (   " + "\n");
            strSqlString.Append("                                      SELECT A.PLAN_WEEK, B.CKD_S_WEEK, B.CKD_L_WEEK, A.MAT_ID, A.WW_QTY, B.CNT   " + "\n");
            strSqlString.Append("                                           , ROUND((A.WW_QTY / 7) * B.CNT, 0) AS REV_QTY   " + "\n");
            strSqlString.Append("                                        FROM RWIPPLNWEK A   " + "\n");
            strSqlString.Append("                                           , (   " + "\n");

            strSqlString.Append("                                              SELECT MAX(TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_WEEK,2,'0')) PLAN_WEEK, COUNT(*) AS CNT  " + "\n");
            strSqlString.Append("                                                   , (SELECT MAX(TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_WEEK,2,'0')) PLAN_WEEK FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = '" + dtWeekFday.Rows[0][0].ToString() + "') AS CKD_S_WEEK " + "\n");
            strSqlString.Append("                                                   , (SELECT MAX(TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_WEEK,2,'0')) PLAN_WEEK FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = '" + Today + "') AS CKD_L_WEEK " + "\n");
            strSqlString.Append("                                                FROM MWIPCALDEF  " + "\n");
            strSqlString.Append("                                               WHERE 1=1  " + "\n");
            strSqlString.Append("                                                 AND CALENDAR_ID = 'OTD'  " + "\n");
            strSqlString.Append("                                                 AND PLAN_YEAR||LPAD(PLAN_MONTH,2,'0') IN('" + cdvDate.Value.ToString("yyyyMM") + "','" + cdvDate.Value.AddMonths(1).ToString("yyyyMM") + "','" + cdvDate.Value.AddMonths(2).ToString("yyyyMM") + "') " + "\n");
            strSqlString.Append("                                               GROUP BY PLAN_WEEK  " + "\n");
            strSqlString.Append("                                             ) B   " + "\n");
            strSqlString.Append("                                       WHERE 1=1   " + "\n");
            strSqlString.Append("                                         AND A.PLAN_WEEK = B.PLAN_WEEK   " + "\n");
            strSqlString.Append("                                         AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'     " + "\n");
            strSqlString.Append("                                         AND A.GUBUN = '3'    " + "\n");
            strSqlString.Append("                                     )   " + "\n");
            strSqlString.Append("                               GROUP BY MAT_ID   " + "\n");
            strSqlString.Append("                             ) W_PLN   " + "\n");
            strSqlString.Append("                       WHERE 1=1  " + "\n");
            strSqlString.Append("                         AND MAT.MAT_ID = PLAN.MAT_ID(+) " + "\n");
            strSqlString.Append("                         AND MAT.MAT_ID = SHP.MAT_ID(+)  " + "\n");
            strSqlString.Append("                         AND MAT.MAT_ID = W_PLN.MAT_ID(+)  " + "\n");
            strSqlString.Append("                         AND MAT.MAT_ID = MON_AO.MAT_ID(+)  " + "\n");
            strSqlString.Append("                         AND MAT.MAT_ID = WEEK_AO.MAT_ID(+)  " + "\n");
            strSqlString.Append("                         AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                         AND MAT.DELETE_FLAG = ' '  " + "\n");
            strSqlString.Append("                         AND MAT.MAT_TYPE = 'FG'  " + "\n");
            strSqlString.Append("                         AND MAT.MAT_ID LIKE '%'  " + "\n");
            strSqlString.Append("                       GROUP BY MAT.MAT_ID " + "\n");
            strSqlString.Append("                     ) A " + "\n");
            strSqlString.Append("                   , ( " + "\n");
            strSqlString.Append("                      SELECT LOT.MAT_ID, MAT.MAT_GRP_3, LOT.OPER_GRP_1, LOT.OPER " + "\n");
            strSqlString.Append("                           , DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY) AS VO " + "\n");
            strSqlString.Append("                        FROM ( " + "\n");
            strSqlString.Append("                              SELECT FACTORY, MAT_ID, OPER_GRP_1, OPER " + "\n");
            strSqlString.Append("                                   , SUM(CASE WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1) " + "\n");
            strSqlString.Append("                                              ELSE QTY_1 " + "\n");
            strSqlString.Append("                                         END) QTY " + "\n");
            strSqlString.Append("                                FROM ( " + "\n");
            strSqlString.Append("                                      SELECT A.FACTORY, A.MAT_ID, B.OPER_GRP_1, B.OPER, A.QTY_1 " + "\n");
            strSqlString.Append("                                           , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') AND KEY_1 = A.MAT_ID) AS COMP_CNT " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("                                        FROM RWIPLOTSTS A " + "\n");
                strSqlString.Append("                                           , MWIPOPRDEF B " + "\n");
                strSqlString.Append("                                       WHERE 1 = 1 " + "\n");
            }
            else
            {
                strSqlString.Append("                                        FROM RWIPLOTSTS_BOH A " + "\n");
                strSqlString.Append("                                           , MWIPOPRDEF B " + "\n");
                strSqlString.Append("                                       WHERE 1 = 1 " + "\n");
                strSqlString.Append("                                         AND A.CUTOFF_DT = '" + cdvDate.SelectedValue() + "' || '22' " + "\n");
            }
            strSqlString.Append("                                         AND A.FACTORY = B.FACTORY(+) " + "\n");
            strSqlString.Append("                                         AND A.OPER = B.OPER(+) " + "\n");
            strSqlString.Append("                                         AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                         AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                                         AND A.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                                         AND B.OPER_GRP_1 NOT IN (' ', '-') " + "\n");
            strSqlString.Append("                                         AND B.OPER BETWEEN 'A0000' AND 'AZ010' " + "\n");

            if (cdvLotType.SelectedItem.ToString() != "ALL")
                strSqlString.Append("                                         AND A.LOT_CMF_5 LIKE '" + cdvLotType.SelectedItem.ToString() + "' " + "\n");
            else
                strSqlString.Append("                                         AND A.LOT_CMF_5 LIKE '%' " + "\n");

            strSqlString.Append("                                     ) " + "\n");
            strSqlString.Append("                               GROUP BY FACTORY, MAT_ID, OPER_GRP_1, OPER " + "\n");
            strSqlString.Append("                               ORDER BY MAT_ID, OPER, OPER_GRP_1 " + "\n");
            strSqlString.Append("                             ) LOT " + "\n");
            strSqlString.Append("                           , MWIPMATDEF MAT " + "\n");
            strSqlString.Append("                       WHERE 1 = 1 " + "\n");
            strSqlString.Append("                         AND LOT.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("                         AND LOT.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("                         AND MAT.DELETE_FLAG <> 'Y' " + "\n");
            strSqlString.Append("                         AND MAT.MAT_GRP_2 <> '-' " + "\n");
            //strSqlString.Append("                       GROUP BY LOT.MAT_ID ,MAT.MAT_GRP_3, LOT.OPER_GRP_1, LOT.OPER " + "\n");
            strSqlString.Append("                       ORDER BY LOT.OPER, LOT.OPER_GRP_1 " + "\n");
            strSqlString.Append("                     ) B " + "\n");

            strSqlString.Append("                   , ( " + "\n");
            strSqlString.Append("                      SELECT MAT.MAT_ID    " + "\n");
            strSqlString.Append("                           , SMM.MAT_TYPE AS MATTYPE   " + "\n");
            strSqlString.Append("                           , MAX(NVL(SMM.UNIT_QTY,0)) AS USAGE   " + "\n");
            strSqlString.Append("                           , SMM.MATCODE AS MATCODE " + "\n");
            strSqlString.Append("                           , SMM.DESCRIPT AS DESCRIPT " + "\n");
            strSqlString.Append("                           , SMM.OPER AS OPER  " + "\n");
            strSqlString.Append("                           , SMM.UNIT_1 AS UNIT  " + "\n");
            strSqlString.Append("                           , MAX(WIP_MAT.TTL) AS TTL " + "\n");
            strSqlString.Append("                           , MAX(WIP_MAT.SMT_IN) AS SMT_IN " + "\n");
            strSqlString.Append("                           , MAX(WIP_MAT.L_IN) AS L_IN " + "\n");
            strSqlString.Append("                           , MAX(WIP_MAT.WIK_WIP) AS WIK_WIP " + "\n");
            strSqlString.Append("                           , MAX(WIP_MAT.INV_L_QTY) AS INV_L_QTY " + "\n");
            strSqlString.Append("                           , MAX(WIP_MAT.INV_QTY) AS INV_QTY " + "\n");
            strSqlString.Append("                           , MAX(WIP_MAT.ORDER_QTY) AS ORDER_QTY " + "\n");
            strSqlString.Append("                           , MAX(WIP_MAT.IN_QTY) AS IN_QTY " + "\n");
            strSqlString.Append("                           , MAX(LOSS_QTY) AS LOSS_QTY " + "\n");

            strSqlString.Append("                        FROM MWIPMATDEF MAT  " + "\n");
            //strSqlString.Append("                           , (    " + "\n");
            //strSqlString.Append("                                SELECT SMM.*, MAT.UNIT_1 " + "\n");
            //strSqlString.Append("                                FROM MWIPMATDEF MAT " + "\n");
            //strSqlString.Append("                                   , RSUMWIPMAT SMM " + "\n");
            //strSqlString.Append("                               WHERE 1=1  " + "\n");
            //strSqlString.Append("                                 AND MAT.MAT_ID = SMM.MATCODE  " + "\n");
            //strSqlString.Append("                                 AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            //strSqlString.Append("                                 AND MAT.DELETE_FLAG = ' ' " + "\n");

            //if (cdvMatType.Text != "ALL" && cdvMatType.Text != "")
            //    strSqlString.AppendFormat("                                   AND SMM.MAT_TYPE " + cdvMatType.SelectedValueToQueryString + "\n");

            //strSqlString.Append("                             ) SMM  " + "\n");

            strSqlString.Append("                           , (   " + "\n");
            strSqlString.Append("                              SELECT DISTINCT NVL(P.MAT_ID,B.PARTNUMBER) PARTNUMBER, B.MATCODE, B.DESCRIPT, B.RESV_FIELD_2 AS MAT_TYPE, B.UNIT AS UNIT_1, B.UNIT_QTY, B.STEPID AS OPER " + "\n");
            strSqlString.Append("                                FROM CWIPMATDEF@RPTTOMES A  " + "\n");
            strSqlString.Append("                                   , CWIPBOMDEF B  " + "\n");
            strSqlString.Append("                                   , RWIPMCPBOM P    " + "\n");
            strSqlString.Append("                               WHERE 1=1  " + "\n");
            strSqlString.Append("                                 AND A.MAT_ID = B.MATCODE  " + "\n");
            strSqlString.Append("                                 AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                                 AND B.RESV_FIELD_2 IN ('CW','GW','MC','SB','SW','TE', 'LF', 'PB', 'RC')  " + "\n");
            strSqlString.Append("                                 AND B.RESV_FIELD_2 <> ' '  " + "\n");
            strSqlString.Append("                                 AND B.RESV_FLAG_1 = 'Y'  " + "\n");
            strSqlString.Append("                                 AND B.STEPID <> 'A0300'  " + "\n");
            strSqlString.Append("                                 AND B.MATCODE NOT LIKE '%-O'  " + "\n");
            strSqlString.Append("                                 AND B.PARTNUMBER = P.MCP_TO_PART(+) " + "\n");
            if (cdvMatType.Text != "ALL" && cdvMatType.Text != "")
                strSqlString.AppendFormat("                                 AND B.RESV_FIELD_2 " + cdvMatType.SelectedValueToQueryString + "\n");
            strSqlString.Append("                               ORDER BY MATCODE " + "\n");
            strSqlString.Append("                             ) SMM " + "\n");
            strSqlString.Append("                           , (  " + "\n");
            strSqlString.Append("                              SELECT REPLACE(A.MAT_ID, '-O', '') AS MAT_ID " + "\n");
            strSqlString.Append("                                   , SUM(NVL(B.INV_QTY,0)) + SUM(NVL(B.INV_L_QTY,0))+SUM(NVL(C.QTY_SMT,0))+SUM(NVL(C.QTY_TTL,0))+SUM(NVL(E.WIK_WIP,0)) AS TTL " + "\n");
            strSqlString.Append("                                   , SUM(NVL(C.QTY_SMT,0)) AS SMT_IN  " + "\n");
            strSqlString.Append("                                   , SUM(NVL(C.QTY_TTL,0)) AS L_IN  " + "\n");
            strSqlString.Append("                                   , SUM(NVL(E.WIK_WIP,0)) AS WIK_WIP  " + "\n");
            strSqlString.Append("                                   , SUM(NVL(B.INV_L_QTY,0)) AS INV_L_QTY  " + "\n");
            strSqlString.Append("                                   , SUM(NVL(B.INV_QTY,0)) AS INV_QTY  " + "\n");
            strSqlString.Append("                                   , SUM(NVL(D.ORDER_QTY,0)) AS ORDER_QTY  " + "\n");
            strSqlString.Append("                                   , SUM(NVL(C.IN_QTY,0)) AS IN_QTY  " + "\n");
            strSqlString.Append("                                   , MAX(NVL(G.DATA_1, 1)) AS LOSS_QTY  " + "\n");
            strSqlString.Append("                                FROM MWIPMATDEF A     " + "\n");
            strSqlString.Append("                                   , (     " + "\n");
            strSqlString.Append("                                      SELECT MAT_ID " + "\n");
            strSqlString.Append("                                           , SUM(INV_QTY) AS INV_QTY " + "\n");
            strSqlString.Append("                                           , SUM(INV_L_QTY) AS INV_L_QTY " + "\n");
            strSqlString.Append("                                        FROM ( " + "\n");
            strSqlString.Append("                                              SELECT MAT_ID  " + "\n");
            strSqlString.Append("                                                   , SUM(DECODE(STORAGE_LOCATION, '1000', QUANTITY, '1003', QUANTITY, 0)) AS INV_QTY  " + "\n");
            strSqlString.Append("                                                   , SUM(DECODE(STORAGE_LOCATION, '1001', QUANTITY, 0)) AS INV_L_QTY    " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {   
                strSqlString.Append("                                                FROM CWMSLOTSTS@RPTTOMES " + "\n");
                strSqlString.Append("                                               WHERE 1=1 " + "\n");                
            }
            else
            {
                strSqlString.Append("                                                FROM CWMSLOTSTS_BOH@RPTTOMES " + "\n");
                strSqlString.Append("                                               WHERE CUTOFF_DT = '" + cdvDate.SelectedValue() + "22'" + "\n"); 
            }

            strSqlString.Append("                                                 AND QUANTITY> 0 " + "\n");
            strSqlString.Append("                                                 AND STORAGE_LOCATION IN ('1000', '1001', '1003') " + "\n");
            strSqlString.Append("                                               GROUP BY MAT_ID  " + "\n");
            strSqlString.Append("                                               UNION ALL " + "\n");

            // 2015-01-30-임종우 : 하기 내용의 의미 해석 - WMS 에서는 넘기고 아직 공정으로는 투입하기 전의 상태 재공
            strSqlString.Append("                                              SELECT MAT_ID, 0 AS INV_QTY, SUM(QTY_1) AS INV_L_QTY " + "\n");
            strSqlString.Append("                                                FROM CWIPMATSLP@RPTTOMES " + "\n");
            strSqlString.Append("                                               WHERE 1=1 " + "\n");
            strSqlString.Append("                                                 AND RECV_FLAG = ' ' " + "\n");
            strSqlString.Append("                                                 AND IN_TIME BETWEEN '" + cdvDate.Value.AddDays(-2).ToString("yyyyMMdd") + "000000' AND '" + cdvDate.SelectedValue() + "235959' " + "\n");
            strSqlString.Append("                                               GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                                             ) " + "\n");
            strSqlString.Append("                                       GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                                     ) B     " + "\n");
            strSqlString.Append("                                   , (     " + "\n");
            strSqlString.Append("                                      SELECT MAT_ID  " + "\n");
            strSqlString.Append("                                           , SUM(CASE WHEN B.LOT_ID IS NOT NULL THEN 1   " + "\n");
            strSqlString.Append("                                                      ELSE 0   " + "\n");
            strSqlString.Append("                                                 END) AS IN_QTY   " + "\n");
            strSqlString.Append("                                           , COUNT(*) AS LOT_TTL   " + "\n");
            strSqlString.Append("                                           , SUM(CASE WHEN OPER LIKE 'M%' AND OPER <= 'M0330' THEN QTY_1 ELSE 0 END) AS QTY_SMT  " + "\n");
            strSqlString.Append("                                           , SUM(CASE WHEN OPER LIKE 'A%' OR OPER > 'M0330' THEN QTY_1 ELSE 0 END) AS QTY_TTL  " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("                                        FROM RWIPLOTSTS A   " + "\n");
                strSqlString.Append("                                           , CRASRESMAT B   " + "\n");
                strSqlString.Append("                                       WHERE 1=1    " + "\n");
            }
            else
            {
                strSqlString.Append("                                        FROM RWIPLOTSTS_BOH A " + "\n");
                strSqlString.Append("                                           , CRASRESMAT B " + "\n");
                strSqlString.Append("                                       WHERE 1=1  " + "\n");
                strSqlString.Append("                                         AND A.CUTOFF_DT = '" + cdvDate.SelectedValue() + "' || '22' " + "\n");
            }

            strSqlString.Append("                                         AND A.FACTORY = B.FACTORY(+)   " + "\n");
            strSqlString.Append("                                         AND A.LOT_ID = B.LOT_ID(+)   " + "\n");
            strSqlString.Append("                                         AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                                         AND A.LOT_TYPE != 'W'  " + "\n");
            strSqlString.Append("                                         AND A.LOT_DEL_FLAG = ' '  " + "\n");
            strSqlString.Append("                                         AND A.LOT_CMF_2 = '-'   " + "\n");
            strSqlString.Append("                                         AND A.LOT_CMF_9 != ' '   " + "\n");
            strSqlString.Append("                                         AND A.QTY_1 > 0   " + "\n");
            strSqlString.Append("                                         AND A.OPER NOT IN  ('00001', '00002', 'V0000')   " + "\n");
            strSqlString.Append("                                       GROUP BY A.MAT_ID    " + "\n");
            strSqlString.Append("                                     ) C     " + "\n");
            strSqlString.Append("                                   , (     " + "\n");
            strSqlString.Append("                                      SELECT MAT_ID, SUM(ORDER_QUAN) AS ORDER_QTY     " + "\n");
            strSqlString.Append("                                        FROM RSAPORDQNT     " + "\n");
            strSqlString.Append("                                       WHERE 1=1     " + "\n");
            strSqlString.Append("                                         AND CREATE_TIME = TO_CHAR(SYSDATE, 'YYYYMMDD')     " + "\n");
            strSqlString.Append("                                       GROUP BY MAT_ID     " + "\n");
            strSqlString.Append("                                     ) D " + "\n");
            strSqlString.Append("                                   , ( " + "\n");
            strSqlString.Append("                                      SELECT MAT_ID, SUM(LOT_QTY) AS WIK_WIP " + "\n");
            strSqlString.Append("                                        FROM ISTMWIKWIP@RPTTOMES " + "\n");
            strSqlString.Append("                                       WHERE 1=1 " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("                                         AND CUTOFF_DT = '" + cdvDate.SelectedValue() + "' || TO_CHAR(SYSDATE, 'HH24')" + "\n");
            }
            else
            {
                strSqlString.Append("                                         AND CUTOFF_DT = '" + cdvDate.SelectedValue() + "' || '22'" + "\n");
            }

            strSqlString.Append("                                       GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                                     ) E " + "\n");
            strSqlString.Append("                                   , (     " + "\n");
            strSqlString.Append("                                      SELECT KEY_1, DATA_1 " + "\n");
            strSqlString.Append("                                        FROM MGCMTBLDAT " + "\n");
            strSqlString.Append("                                       WHERE 1=1     " + "\n");
            strSqlString.Append("                                         AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                         AND TABLE_NAME IN ('H_SEC_AUTO_LOSS', 'H_HX_AUTO_LOSS') " + "\n");
            strSqlString.Append("                                         AND DATA_4 = 'A0395' " + "\n");
            strSqlString.Append("                                     ) G " + "\n");
            strSqlString.Append("                               WHERE 1=1 " + "\n");
            strSqlString.Append("                                 AND A.MAT_ID = B.MAT_ID(+) " + "\n");
            strSqlString.Append("                                 AND A.MAT_ID = C.MAT_ID(+) " + "\n");
            strSqlString.Append("                                 AND A.MAT_ID = D.MAT_ID(+) " + "\n");
            strSqlString.Append("                                 AND A.MAT_ID = E.MAT_ID(+) " + "\n");
            strSqlString.Append("                                 AND A.MAT_ID = G.KEY_1(+) " + "\n");
            strSqlString.Append("                                 AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                               GROUP BY REPLACE(A.MAT_ID, '-O', '') " + "\n");
            strSqlString.Append("                               HAVING SUM(NVL(B.INV_QTY,0)) + SUM(NVL(C.QTY_SMT,0)) + SUM(NVL(C.QTY_TTL,0)) + SUM(NVL(B.INV_L_QTY,0)) + SUM(NVL(D.ORDER_QTY,0)) + SUM(NVL(E.WIK_WIP,0)) + SUM(NVL(C.IN_QTY,0)) > 0 " + "\n");
            strSqlString.Append("                             ) WIP_MAT " + "\n");
            strSqlString.Append("                       WHERE 1=1 " + "\n");
            strSqlString.Append("                         AND MAT.MAT_ID = SMM.PARTNUMBER(+) " + "\n");
            strSqlString.Append("                         AND SMM.MATCODE = WIP_MAT.MAT_ID(+) " + "\n");
            strSqlString.Append("                         AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                         AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                         AND MAT.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                         AND MAT.MAT_ID LIKE '%' " + "\n");
            strSqlString.Append("                         AND SMM.UNIT_QTY > 0 " + "\n");
            strSqlString.Append("                       GROUP BY MAT.MAT_ID, SMM.MAT_TYPE, SMM.DESCRIPT, SMM.MATCODE, SMM.DESCRIPT, SMM.OPER, SMM.UNIT_1 " + "\n");
            strSqlString.Append("                     ) C " + "\n");
            strSqlString.Append("               WHERE 1=1 " + "\n");
            strSqlString.Append("                 AND MAT.MAT_ID = A.MAT_ID(+) " + "\n");
            strSqlString.Append("                 AND MAT.MAT_ID = B.MAT_ID(+) " + "\n");
            strSqlString.Append("                 AND MAT.MAT_ID = C.MAT_ID(+) " + "\n");
            strSqlString.Append("                 AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                 AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                 AND MAT.VENDOR_ID <> ' ' " + "\n");
            strSqlString.Append("               GROUP BY MAT.MAT_ID, C.MATTYPE, C.MATCODE, C.DESCRIPT, C.OPER, C.UNIT " + "\n");
            strSqlString.Append("             ) A " + "\n");
            strSqlString.Append("        WHERE 1=1  " + "\n");
            strSqlString.Append("          AND MAT.MAT_ID = A.MAT_ID(+) " + "\n");
            strSqlString.Append("          AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("          AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("          AND A.MATCODE <> ' ' " + "\n");

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("          AND MAT.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("          AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("          AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("          AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("          AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("          AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("          AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("          AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("          AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            if (txtMatCode.Text.Trim() != "%" && txtMatCode.Text.Trim() != "")
                strSqlString.AppendFormat("          AND A.MATCODE LIKE '{0}'" + "\n", txtMatCode.Text);

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                strSqlString.AppendFormat("          AND MAT.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);

            strSqlString.Append("        GROUP BY " + QueryCond3 + "\n");

            if (rdbMonth.Checked == true)
            {
                strSqlString.Append("        HAVING ( " + "\n");
                strSqlString.Append("                SUM(NVL(SOP_PLN_MON,0))+SUM(NVL(SOP_PLN_WEEK,0))+SUM(NVL(ASSY_MON,0))+SUM(NVL(WIP_STOCK,0))+SUM(NVL(REV_QTY,0))+MAX(NVL(TTL,0)) " + "\n");
                strSqlString.Append("               +SUM(NVL(W0,0))+SUM(NVL(W1,0))+SUM(NVL(W2,0))+SUM(NVL(W3,0))+SUM(NVL(W4,0))+SUM(NVL(W5,0))+SUM(NVL(W6,0))+SUM(NVL(W7,0)) " + "\n");
                strSqlString.Append("               ) > 0 " + "\n");
            }
            else if (rdbWeek.Checked == true)
            {
                strSqlString.Append("        HAVING ( " + "\n");
                strSqlString.Append("                SUM(NVL(SOP_PLN_MON,0))+SUM(NVL(SOP_PLN_WEEK,0))+SUM(NVL(WEEK_AO,0))+SUM(NVL(WIP_STOCK,0))+SUM(NVL(REV_QTY,0))+MAX(NVL(TTL,0)) " + "\n");
                strSqlString.Append("               +SUM(NVL(W0,0))+SUM(NVL(W1,0))+SUM(NVL(W2,0))+SUM(NVL(W3,0))+SUM(NVL(W4,0))+SUM(NVL(W5,0))+SUM(NVL(W6,0))+SUM(NVL(W7,0)) " + "\n");
                strSqlString.Append("               ) > 0 " + "\n");
            }

            strSqlString.Append("       ) A " + "\n");
            strSqlString.Append(" WHERE 1=1  " + "\n");

            if (rdbMonth.Checked == true)
            {
                strSqlString.Append("   AND NVL(WIP_STOCK,0)+NVL(SOP_PLN_MON,0)+NVL(SOP_PLN_WEEK,0)+NVL(ASSY_MON,0)+NVL(REV_QTY,0)+NVL(MAT_TTL,0)+NVL(MAT_L_IN,0)+NVL(WIK_WIP,0)+NVL(MAT_INV_L_QTY,0)+NVL(MAT_INV_QTY,0)+NVL(ORDER_QTY,0)+NVL(W1,0)+NVL(W2,0)+NVL(W3,0)+NVL(W4,0)+NVL(W5,0)+NVL(W6,0)+NVL(W7,0) > 0 " + "\n");
            }
            else if (rdbWeek.Checked == true)
            {
                strSqlString.Append("   AND NVL(WIP_STOCK,0)+NVL(SOP_PLN_MON,0)+NVL(SOP_PLN_WEEK,0)+NVL(ASSY_WEEK,0)+NVL(REV_QTY,0)+NVL(MAT_TTL,0)+NVL(MAT_L_IN,0)+NVL(WIK_WIP,0)+NVL(MAT_INV_L_QTY,0)+NVL(MAT_INV_QTY,0)+NVL(ORDER_QTY,0)+NVL(W1,0)+NVL(W2,0)+NVL(W3,0)+NVL(W4,0)+NVL(W5,0)+NVL(W6,0)+NVL(W7,0) > 0 " + "\n");

            }

            if (ckbOverShort.Checked == true)
            {
                if (rdbMonth.Checked == true)
                {
                    strSqlString.Append("   AND ( ROUND((((MAT_TTL/USAGE*1000)-REV_QTY)-WIP_STOCK)/" + sKpcsValue + ",1) < 0 OR ROUND((MAT_TTL-((SOP_PLN_MON-ASSY_MON)*USAGE/1000))/" + sKpcsValue + ",1) < 0 ) " + "\n");
                }
                else if (rdbWeek.Checked == true)
                {
                    strSqlString.Append("   AND ( ROUND((((MAT_TTL/USAGE*1000)-REV_QTY)-WIP_STOCK)/" + sKpcsValue + ",1) < 0 OR ROUND((MAT_TTL-((SOP_PLN_WEEK-ASSY_WEEK)*USAGE/1000))/" + sKpcsValue + ",1) < 0 )" + "\n");
                }


            }
            strSqlString.Append(" ORDER BY " + QueryCond4 + "\n");


            #endregion

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        // 검색 월에 해당하는 주차 리스트 가져오기
        private string MakeSqlString1()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT DISTINCT LPAD(PLAN_WEEK,2,'0') AS PLAN_WEEK " + "\n");
            strSqlString.Append("  FROM MWIPCALDEF " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND CALENDAR_ID = 'OTD' " + "\n");
            strSqlString.Append("   AND SYS_YEAR = '" + cdvDate.Value.ToString("yyyy") + "' " + "\n");
            strSqlString.Append("   AND SYS_MONTH = '" + cdvDate.Value.ToString("MM") + "' " + "\n");
            strSqlString.Append("   AND SYS_DAY = '" + cdvDate.Value.ToString("dd") + "' " + "\n");
            strSqlString.Append(" ORDER BY PLAN_WEEK " + "\n");

            return strSqlString.ToString();
        }

        // 지난 주차의 마지막일 가져오기
        private string MakeSqlString4(string year, string date)
        {
            StringBuilder sqlString = new StringBuilder();

            sqlString.Append("SELECT MIN(SYS_DATE-1) " + "\n");
            sqlString.Append("  FROM MWIPCALDEF " + "\n");
            sqlString.Append(" WHERE 1=1" + "\n");
            sqlString.Append("   AND CALENDAR_ID='SE'" + "\n");
            sqlString.Append("   AND PLAN_YEAR='" + year + "'\n");
            sqlString.Append("   AND PLAN_WEEK=(" + "\n");
            sqlString.Append("                  SELECT PLAN_WEEK " + "\n");
            sqlString.Append("                    FROM MWIPCALDEF " + "\n");
            sqlString.Append("                   WHERE 1=1 " + "\n");
            sqlString.Append("                     AND CALENDAR_ID='SE' " + "\n");
            sqlString.Append("                     AND SYS_DATE=TO_CHAR(TO_DATE('" + date + "','YYYYMMDD'),'YYYYMMDD')" + "\n");
            sqlString.Append("                 )" + "\n");

            return sqlString.ToString();
        }

        // 주차의 시작일, 마지막일 가져오기
        private string MakeSqlString2(string fn, string year, string week)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT " + fn + "(SYS_DATE) " + "\n");
            strSqlString.Append("  FROM MWIPCALDEF " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND CALENDAR_ID='OTD' " + "\n");
            strSqlString.Append("   AND PLAN_YEAR='" + year + "' " + "\n");
            strSqlString.Append("   AND PLAN_WEEK='" + week + "' " + "\n");


            return strSqlString.ToString();
        }

        //8주차 월 리스트
        private string MakeSqlString3()
        {
            StringBuilder strSqlString = new StringBuilder();
            strSqlString.Append("SELECT DISTINCT PLAN_YEAR|| LPAD(PLAN_WEEK,2,'0') " + "\n");
            strSqlString.Append("  FROM MWIPCALDEF " + "\n");
            strSqlString.Append(" WHERE CALENDAR_ID = 'OTD' " + "\n");
            strSqlString.Append("   AND SYS_YEAR || LPAD(SYS_MONTH,2,'0') IN ('" + cdvDate.Value.ToString("yyyyMM") + "', '" + cdvDate.Value.AddMonths(1).ToString("yyyyMM") + "' ,'" + cdvDate.Value.AddMonths(2).ToString("yyyyMM") + "') " + "\n");
            strSqlString.Append("   AND SYS_DATE >= '" + cdvDate.Value.ToString("yyyyMMdd") + "' " + "\n");
            strSqlString.Append(" ORDER BY 1 " + "\n");
            return strSqlString.ToString();
        }

        /// <summary>
        /// 원부자재기준의 유효기간별 수량을 구하기 위한 쿼리 생성
        /// </summary>
        /// <param name="strMatCode"></param>
        /// <param name="strProduct"></param>
        /// <returns></returns>
        private string MakeSqlStringForPopup_1(string gubun, string strMatCode)
        {
            string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값

            if (ckbKpcs.Checked == true)
                sKpcsValue = "1000";
            else
                sKpcsValue = "1";

            StringBuilder strSqlString = new StringBuilder();

            if ("TTL" == gubun)
            {
                strSqlString.Append("SELECT MAT_ID " + "\n");
                strSqlString.Append("     , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'VENDOR' AND KEY_1 = VENDOR_CODE) AS VENDOR_CODE " + "\n");
                strSqlString.Append("     , ROUND(SUM(TTL)/" + sKpcsValue + ",0) AS TTL " + "\n");
                strSqlString.Append("     , ROUND(SUM(MONTHS4)/" + sKpcsValue + ",0) AS MONTHS4 " + "\n");
                strSqlString.Append("     , ROUND(SUM(MONTHS3)/" + sKpcsValue + ",0) AS MONTHS3 " + "\n");
                strSqlString.Append("     , ROUND(SUM(MONTHS2)/" + sKpcsValue + ",0) AS MONTHS2 " + "\n");
                strSqlString.Append("     , ROUND(SUM(MONTHS1)/" + sKpcsValue + ",0) AS MONTHS1 " + "\n");
                strSqlString.Append("     , ROUND(SUM(MONTHS0)/" + sKpcsValue + ",0) AS MONTHS0 " + "\n");
                strSqlString.Append("  FROM ( " + "\n");                
                strSqlString.Append("        SELECT REPLACE(MAT_ID, '-O', '') AS MAT_ID " + "\n");
                strSqlString.Append("             , VENDOR_CODE " + "\n");
                strSqlString.Append("             , SUM(NVL(QTY_TTL,0)) AS TTL " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN TO_CHAR(ADD_MONTHS(SYSDATE,3), 'YYYYMMDDHH24MISS') < EPN_DATE THEN QTY_TTL ELSE 0 END) AS MONTHS4 " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN EPN_DATE <= TO_CHAR(ADD_MONTHS(SYSDATE,3), 'YYYYMMDDHH24MISS') AND TO_CHAR(ADD_MONTHS(SYSDATE,2), 'YYYYMMDDHH24MISS') < EPN_DATE THEN QTY_TTL ELSE 0 END) AS MONTHS3 " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN EPN_DATE <= TO_CHAR(ADD_MONTHS(SYSDATE,2), 'YYYYMMDDHH24MISS') AND TO_CHAR(ADD_MONTHS(SYSDATE,1), 'YYYYMMDDHH24MISS') < EPN_DATE THEN QTY_TTL ELSE 0 END) AS MONTHS2 " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN EPN_DATE <= TO_CHAR(ADD_MONTHS(SYSDATE,1), 'YYYYMMDDHH24MISS') AND TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS') < EPN_DATE THEN QTY_TTL ELSE 0 END) AS MONTHS1 " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN EPN_DATE <= TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS') THEN QTY_TTL ELSE 0 END) AS MONTHS0   " + "\n");                
                strSqlString.Append("          FROM ( " + "\n");
                strSqlString.Append("                SELECT MAT_ID, LOT_CMF_16 AS EPN_DATE, LOT_CMF_20 AS VENDOR_CODE, QTY_1 AS QTY_TTL " + "\n");                
                strSqlString.Append("                  FROM RWIPLOTSTS A " + "\n");
                strSqlString.Append("                     , CRASRESMAT B " + "\n");
                strSqlString.Append("                 WHERE 1=1  " + "\n");
                strSqlString.Append("                   AND A.FACTORY = B.FACTORY(+)   " + "\n");
                strSqlString.Append("                   AND A.LOT_ID = B.LOT_ID(+)   " + "\n");
                strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
                strSqlString.Append("                   AND A.LOT_TYPE != 'W'  " + "\n");
                strSqlString.Append("                   AND A.LOT_DEL_FLAG = ' '  " + "\n");
                strSqlString.Append("                   AND A.LOT_CMF_2 = '-'   " + "\n");
                strSqlString.Append("                   AND A.LOT_CMF_9 != ' '   " + "\n");
                strSqlString.Append("                   AND A.QTY_1 > 0   " + "\n");
                strSqlString.Append("                   AND A.OPER NOT IN  ('00001', '00002', 'V0000')   " + "\n");
                strSqlString.Append("                   AND (A.MAT_ID = '" + strMatCode + "' OR  A.MAT_ID = '" + strMatCode + "-O') " + "\n");                
                strSqlString.Append("                 UNION ALL " + "\n");
                strSqlString.Append("                SELECT MAT_ID, EPN_DATE, VENDOR_CODE, QUANTITY AS QTY_TTL " + "\n");                
                strSqlString.Append("                  FROM CWMSLOTSTS@RPTTOMES   " + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
                strSqlString.Append("                   AND QUANTITY> 0   " + "\n");
                strSqlString.Append("                   AND STORAGE_LOCATION IN ('1000', '1001', '1003')   " + "\n");
                strSqlString.Append("                   AND (MAT_ID = '" + strMatCode + "' OR  MAT_ID = '" + strMatCode + "-O') " + "\n");
                strSqlString.Append("               ) " + "\n");
                strSqlString.Append("         GROUP BY REPLACE(MAT_ID, '-O', ''), VENDOR_CODE " + "\n");                
                strSqlString.Append("       ) " + "\n");
                strSqlString.Append(" GROUP BY MAT_ID, VENDOR_CODE " + "\n");
            }
            else
            {
                if ("INV_QTY" == gubun) //창고(구매)
                {
                    strSqlString.Append("SELECT REPLACE(A.MAT_ID, '-O', '') AS MAT_ID " + "\n");
                    strSqlString.Append("     , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'VENDOR' AND KEY_1 = VENDOR_CODE) AS VENDOR_CODE " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.INV_QTY,0))/" + sKpcsValue + ",0) AS INV_QTY " + "\n");
                    strSqlString.Append("     , ROUND(SUM(CASE  " + "\n");
                    strSqlString.Append("       WHEN TO_CHAR(ADD_MONTHS(SYSDATE,3), 'YYYYMMDD') < B.EPN_DATE THEN B.INV_QTY ELSE 0 " + "\n");
                    strSqlString.Append("        END)/" + sKpcsValue + ",0) AS MONTHS4  " + "\n");
                    strSqlString.Append("     , ROUND(SUM(CASE  " + "\n");
                    strSqlString.Append("           WHEN B.EPN_DATE <= TO_CHAR(ADD_MONTHS(SYSDATE,3), 'YYYYMMDD') AND TO_CHAR(ADD_MONTHS(SYSDATE,2), 'YYYYMMDD') < B.EPN_DATE THEN B.INV_QTY ELSE 0 " + "\n");
                    strSqlString.Append("            END)/" + sKpcsValue + ",0) AS MONTHS3  " + "\n");
                    strSqlString.Append("     , ROUND(SUM(CASE  " + "\n");
                    strSqlString.Append("           WHEN B.EPN_DATE <= TO_CHAR(ADD_MONTHS(SYSDATE,2), 'YYYYMMDD') AND TO_CHAR(ADD_MONTHS(SYSDATE,1), 'YYYYMMDD') < B.EPN_DATE THEN B.INV_QTY ELSE 0 " + "\n");
                    strSqlString.Append("            END)/" + sKpcsValue + ",0) AS MONTHS2  " + "\n");
                    strSqlString.Append("     , ROUND(SUM(CASE  " + "\n");
                    strSqlString.Append("           WHEN B.EPN_DATE <= TO_CHAR(ADD_MONTHS(SYSDATE,1), 'YYYYMMDD') AND TO_CHAR(SYSDATE, 'YYYYMMDD') < B.EPN_DATE THEN B.INV_QTY ELSE 0 " + "\n");
                    strSqlString.Append("            END)/" + sKpcsValue + ",0) AS MONTHS1  " + "\n");
                    strSqlString.Append("     , ROUND(SUM(CASE  " + "\n");
                    strSqlString.Append("           WHEN B.EPN_DATE <= TO_CHAR(SYSDATE, 'YYYYMMDD') THEN B.INV_QTY ELSE 0 " + "\n");
                    strSqlString.Append("            END)/" + sKpcsValue + ",0) AS MONTHS0  " + "\n");
                    strSqlString.Append("  FROM MWIPMATDEF A  " + "\n");
                    strSqlString.Append("     , (  " + "\n");
                    strSqlString.Append("        SELECT MAT_ID, EPN_DATE, VENDOR_CODE " + "\n");
                    strSqlString.Append("             , SUM(DECODE(STORAGE_LOCATION, '1000', QUANTITY, '1003', QUANTITY, 0)) AS INV_QTY  " + "\n");
                    strSqlString.Append("             , SUM(DECODE(STORAGE_LOCATION, '1001', QUANTITY, 0)) AS INV_L_QTY " + "\n");
                    strSqlString.Append("          FROM CWMSLOTSTS@RPTTOMES  " + "\n");
                    strSqlString.Append("         WHERE 1=1  " + "\n");
                    strSqlString.Append("           AND QUANTITY> 0  " + "\n");
                    strSqlString.Append("           AND STORAGE_LOCATION IN ('1000', '1001', '1003')  " + "\n");
                    strSqlString.Append("         GROUP BY MAT_ID, EPN_DATE, VENDOR_CODE " + "\n");
                    strSqlString.Append("       ) B  " + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    strSqlString.Append("   AND A.MAT_ID = B.MAT_ID(+) " + "\n");
                    strSqlString.Append("   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("   AND (A.MAT_ID = '" + strMatCode + "' OR  A.MAT_ID = '" + strMatCode + "-O') " + "\n");
                    strSqlString.Append(" GROUP BY REPLACE(A.MAT_ID, '-O', ''), VENDOR_CODE " + "\n");

                }
                else if ("INV_L_QTY" == gubun) //현장창고
                {
                    strSqlString.Append("SELECT REPLACE(A.MAT_ID, '-O', '') AS MAT_ID " + "\n");
                    strSqlString.Append("     , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'VENDOR' AND KEY_1 = VENDOR_CODE) AS VENDOR_CODE " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(B.INV_L_QTY,0))/" + sKpcsValue + ",0) AS INV_L_QTY " + "\n");
                    strSqlString.Append("         , ROUND(SUM(CASE  " + "\n");
                    strSqlString.Append("           WHEN TO_CHAR(ADD_MONTHS(SYSDATE,3), 'YYYYMMDD') < B.EPN_DATE THEN B.INV_L_QTY ELSE 0 " + "\n");
                    strSqlString.Append("            END)/" + sKpcsValue + ",0) AS MONTHS4  " + "\n");
                    strSqlString.Append("         , ROUND(SUM(CASE  " + "\n");
                    strSqlString.Append("               WHEN B.EPN_DATE <= TO_CHAR(ADD_MONTHS(SYSDATE,3), 'YYYYMMDD') AND TO_CHAR(ADD_MONTHS(SYSDATE,2), 'YYYYMMDD') < B.EPN_DATE THEN B.INV_L_QTY ELSE 0 " + "\n");
                    strSqlString.Append("                END)/" + sKpcsValue + ",0) AS MONTHS3  " + "\n");
                    strSqlString.Append("         , ROUND(SUM(CASE  " + "\n");
                    strSqlString.Append("               WHEN B.EPN_DATE <= TO_CHAR(ADD_MONTHS(SYSDATE,2), 'YYYYMMDD') AND TO_CHAR(ADD_MONTHS(SYSDATE,1), 'YYYYMMDD') < B.EPN_DATE THEN B.INV_L_QTY ELSE 0 " + "\n");
                    strSqlString.Append("                END)/" + sKpcsValue + ",0) AS MONTHS2  " + "\n");
                    strSqlString.Append("         , ROUND(SUM(CASE  " + "\n");
                    strSqlString.Append("               WHEN B.EPN_DATE <= TO_CHAR(ADD_MONTHS(SYSDATE,1), 'YYYYMMDD') AND TO_CHAR(SYSDATE, 'YYYYMMDD') < B.EPN_DATE THEN B.INV_L_QTY ELSE 0 " + "\n");
                    strSqlString.Append("                END)/" + sKpcsValue + ",0) AS MONTHS1  " + "\n");
                    strSqlString.Append("         , ROUND(SUM(CASE  " + "\n");
                    strSqlString.Append("               WHEN B.EPN_DATE <= TO_CHAR(SYSDATE, 'YYYYMMDD') THEN B.INV_L_QTY ELSE 0 " + "\n");
                    strSqlString.Append("                END)/" + sKpcsValue + ",0) AS MONTHS0  " + "\n");
                    strSqlString.Append("  FROM MWIPMATDEF A  " + "\n");
                    strSqlString.Append("     , (  " + "\n");
                    strSqlString.Append("        SELECT MAT_ID, EPN_DATE, VENDOR_CODE " + "\n");
                    strSqlString.Append("             , SUM(DECODE(STORAGE_LOCATION, '1000', QUANTITY, '1003', QUANTITY, 0)) AS INV_QTY  " + "\n");
                    strSqlString.Append("             , SUM(DECODE(STORAGE_LOCATION, '1001', QUANTITY, 0)) AS INV_L_QTY " + "\n");
                    strSqlString.Append("          FROM CWMSLOTSTS@RPTTOMES  " + "\n");
                    strSqlString.Append("         WHERE 1=1  " + "\n");
                    strSqlString.Append("           AND QUANTITY> 0  " + "\n");
                    strSqlString.Append("           AND STORAGE_LOCATION IN ('1000', '1001', '1003')  " + "\n");
                    strSqlString.Append("         GROUP BY MAT_ID, EPN_DATE, VENDOR_CODE " + "\n");
                    strSqlString.Append("       ) B  " + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    strSqlString.Append("   AND A.MAT_ID = B.MAT_ID(+) " + "\n");
                    strSqlString.Append("   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("   AND (A.MAT_ID = '" + strMatCode + "' OR  A.MAT_ID = '" + strMatCode + "-O') " + "\n");
                    strSqlString.Append(" GROUP BY REPLACE(A.MAT_ID, '-O', ''), VENDOR_CODE " + "\n");

                }
                else if ("SMT_IN" == gubun) //SMT
                {
                    strSqlString.Append("SELECT REPLACE(A.MAT_ID, '-O', '') AS MAT_ID " + "\n");
                    strSqlString.Append("     , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'VENDOR' AND KEY_1 = VENDOR_CODE) AS VENDOR_CODE " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(C.QTY_SMT,0))/" + sKpcsValue + ",0) AS L_IN  " + "\n");
                    strSqlString.Append("         , ROUND(SUM(CASE  " + "\n");
                    strSqlString.Append("           WHEN TO_CHAR(ADD_MONTHS(SYSDATE,3), 'YYYYMMDDHH24MISS') < C.LOT_CMF_16 THEN C.QTY_SMT ELSE 0 " + "\n");
                    strSqlString.Append("            END)/" + sKpcsValue + ",0) AS MONTHS4  " + "\n");
                    strSqlString.Append("         , ROUND(SUM(CASE  " + "\n");
                    strSqlString.Append("               WHEN C.LOT_CMF_16 <= TO_CHAR(ADD_MONTHS(SYSDATE,3), 'YYYYMMDDHH24MISS') AND TO_CHAR(ADD_MONTHS(SYSDATE,2), 'YYYYMMDDHH24MISS') < C.LOT_CMF_16 THEN C.QTY_SMT ELSE 0 " + "\n");
                    strSqlString.Append("                END)/" + sKpcsValue + ",0) AS MONTHS3  " + "\n");
                    strSqlString.Append("         , ROUND(SUM(CASE  " + "\n");
                    strSqlString.Append("               WHEN C.LOT_CMF_16 <= TO_CHAR(ADD_MONTHS(SYSDATE,2), 'YYYYMMDDHH24MISS') AND TO_CHAR(ADD_MONTHS(SYSDATE,1), 'YYYYMMDDHH24MISS') < C.LOT_CMF_16 THEN C.QTY_SMT ELSE 0 " + "\n");
                    strSqlString.Append("                END)/" + sKpcsValue + ",0) AS MONTHS2  " + "\n");
                    strSqlString.Append("         , ROUND(SUM(CASE  " + "\n");
                    strSqlString.Append("               WHEN C.LOT_CMF_16 <= TO_CHAR(ADD_MONTHS(SYSDATE,1), 'YYYYMMDDHH24MISS') AND TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS') < C.LOT_CMF_16 THEN C.QTY_SMT ELSE 0 " + "\n");
                    strSqlString.Append("                END)/" + sKpcsValue + ",0) AS MONTHS1  " + "\n");
                    strSqlString.Append("         , ROUND(SUM(CASE  " + "\n");
                    strSqlString.Append("               WHEN C.LOT_CMF_16 <= TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS') THEN C.QTY_SMT ELSE 0 " + "\n");
                    strSqlString.Append("                END)/" + sKpcsValue + ",0) AS MONTHS0  " + "\n");
                    strSqlString.Append("  FROM MWIPMATDEF A  " + "\n");
                    strSqlString.Append("     , (  " + "\n");
                    strSqlString.Append("        SELECT MAT_ID, LOT_CMF_16, LOT_CMF_20 AS VENDOR_CODE " + "\n");
                    strSqlString.Append("             , SUM(CASE WHEN OPER LIKE 'M%' AND OPER <= 'M0330' THEN QTY_1 ELSE 0 END) AS QTY_SMT  " + "\n");
                    strSqlString.Append("          FROM RWIPLOTSTS A  " + "\n");
                    strSqlString.Append("             , CRASRESMAT B  " + "\n");
                    strSqlString.Append("         WHERE 1=1   " + "\n");
                    strSqlString.Append("           AND A.FACTORY = B.FACTORY(+)  " + "\n");
                    strSqlString.Append("           AND A.LOT_ID = B.LOT_ID(+)  " + "\n");
                    strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("           AND A.LOT_TYPE != 'W' " + "\n");
                    strSqlString.Append("           AND A.LOT_DEL_FLAG = ' ' " + "\n");
                    strSqlString.Append("           AND A.LOT_CMF_2 = '-'  " + "\n");
                    strSqlString.Append("           AND A.LOT_CMF_9 != ' '  " + "\n");
                    strSqlString.Append("           AND A.QTY_1 > 0  " + "\n");
                    strSqlString.Append("           AND A.OPER NOT IN  ('00001', '00002', 'V0000')  " + "\n");
                    strSqlString.Append("         GROUP BY A.MAT_ID, LOT_CMF_16, LOT_CMF_20 " + "\n");
                    strSqlString.Append("       ) C  " + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    strSqlString.Append("   AND A.MAT_ID = C.MAT_ID(+) " + "\n");
                    strSqlString.Append("   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("   AND (A.MAT_ID = '" + strMatCode + "' OR  A.MAT_ID = '" + strMatCode + "-O') " + "\n");
                    strSqlString.Append(" GROUP BY REPLACE(A.MAT_ID, '-O', ''), VENDOR_CODE " + "\n");

                }
                else if ("L_IN" == gubun) //LINE
                {
                    strSqlString.Append("SELECT REPLACE(A.MAT_ID, '-O', '') AS MAT_ID " + "\n");
                    strSqlString.Append("     , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'VENDOR' AND KEY_1 = VENDOR_CODE) AS VENDOR_CODE " + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(C.QTY_TTL,0))/" + sKpcsValue + ",0) AS L_IN  " + "\n");
                    strSqlString.Append("         , ROUND(SUM(CASE  " + "\n");
                    strSqlString.Append("           WHEN TO_CHAR(ADD_MONTHS(SYSDATE,3), 'YYYYMMDDHH24MISS') < C.LOT_CMF_16 THEN C.QTY_TTL ELSE 0 " + "\n");
                    strSqlString.Append("            END)/" + sKpcsValue + ",0) AS MONTHS4  " + "\n");
                    strSqlString.Append("         , ROUND(SUM(CASE  " + "\n");
                    strSqlString.Append("               WHEN C.LOT_CMF_16 <= TO_CHAR(ADD_MONTHS(SYSDATE,3), 'YYYYMMDDHH24MISS') AND TO_CHAR(ADD_MONTHS(SYSDATE,2), 'YYYYMMDDHH24MISS') < C.LOT_CMF_16 THEN C.QTY_TTL ELSE 0 " + "\n");
                    strSqlString.Append("                END)/" + sKpcsValue + ",0) AS MONTHS3  " + "\n");
                    strSqlString.Append("         , ROUND(SUM(CASE  " + "\n");
                    strSqlString.Append("               WHEN C.LOT_CMF_16 <= TO_CHAR(ADD_MONTHS(SYSDATE,2), 'YYYYMMDDHH24MISS') AND TO_CHAR(ADD_MONTHS(SYSDATE,1), 'YYYYMMDDHH24MISS') < C.LOT_CMF_16 THEN C.QTY_TTL ELSE 0 " + "\n");
                    strSqlString.Append("                END)/" + sKpcsValue + ",0) AS MONTHS2  " + "\n");
                    strSqlString.Append("         , ROUND(SUM(CASE  " + "\n");
                    strSqlString.Append("               WHEN C.LOT_CMF_16 <= TO_CHAR(ADD_MONTHS(SYSDATE,1), 'YYYYMMDDHH24MISS') AND TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS') < C.LOT_CMF_16 THEN C.QTY_TTL ELSE 0 " + "\n");
                    strSqlString.Append("                END)/" + sKpcsValue + ",0) AS MONTHS1  " + "\n");
                    strSqlString.Append("         , ROUND(SUM(CASE  " + "\n");
                    strSqlString.Append("               WHEN C.LOT_CMF_16 <= TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS') THEN C.QTY_TTL ELSE 0 " + "\n");
                    strSqlString.Append("                END)/" + sKpcsValue + ",0) AS MONTHS0  " + "\n");
                    strSqlString.Append("  FROM MWIPMATDEF A  " + "\n");
                    strSqlString.Append("     , (  " + "\n");
                    strSqlString.Append("        SELECT MAT_ID, LOT_CMF_16, LOT_CMF_20 AS VENDOR_CODE " + "\n");
                    strSqlString.Append("             , SUM(CASE WHEN OPER LIKE 'A%' OR OPER > 'M0330' THEN QTY_1 ELSE 0 END) AS QTY_TTL " + "\n");
                    strSqlString.Append("          FROM RWIPLOTSTS A  " + "\n");
                    strSqlString.Append("             , CRASRESMAT B  " + "\n");
                    strSqlString.Append("         WHERE 1=1   " + "\n");
                    strSqlString.Append("           AND A.FACTORY = B.FACTORY(+)  " + "\n");
                    strSqlString.Append("           AND A.LOT_ID = B.LOT_ID(+)  " + "\n");
                    strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("           AND A.LOT_TYPE != 'W' " + "\n");
                    strSqlString.Append("           AND A.LOT_DEL_FLAG = ' ' " + "\n");
                    strSqlString.Append("           AND A.LOT_CMF_2 = '-'  " + "\n");
                    strSqlString.Append("           AND A.LOT_CMF_9 != ' '  " + "\n");
                    strSqlString.Append("           AND A.QTY_1 > 0  " + "\n");
                    strSqlString.Append("           AND A.OPER NOT IN  ('00001', '00002', 'V0000')  " + "\n");
                    strSqlString.Append("         GROUP BY A.MAT_ID, LOT_CMF_16, LOT_CMF_20 " + "\n");
                    strSqlString.Append("       ) C  " + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    strSqlString.Append("   AND A.MAT_ID = C.MAT_ID(+) " + "\n");
                    strSqlString.Append("   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("   AND (A.MAT_ID = '" + strMatCode + "' OR  A.MAT_ID = '" + strMatCode + "-O') " + "\n");
                    strSqlString.Append(" GROUP BY REPLACE(A.MAT_ID, '-O', ''), VENDOR_CODE " + "\n");

                }
                else if ("WIK_WIP" == gubun) //외주
                {
                    strSqlString.Append("SELECT REPLACE(A.MAT_ID, '-O', '') AS MAT_ID, ' ' " + "\n");
                    strSqlString.Append("     , ROUND(SUM(E.WIK_WIP)/" + sKpcsValue + ",0) AS WIK_WIP  " + "\n");
                    strSqlString.Append("  FROM MWIPMATDEF A  " + "\n");
                    strSqlString.Append("     , (  " + "\n");
                    strSqlString.Append("        SELECT MAT_ID, SUM(LOT_QTY) AS WIK_WIP  " + "\n");
                    strSqlString.Append("          FROM ISTMWIKWIP@RPTTOMES  " + "\n");
                    strSqlString.Append("         WHERE 1=1  " + "\n");
                    strSqlString.Append("           AND CUTOFF_DT = '" + DateTime.Now.ToString("yyyyMMdd") + "' || TO_CHAR(SYSDATE, 'HH24') " + "\n");
                    strSqlString.Append("         GROUP BY MAT_ID  " + "\n");
                    strSqlString.Append("       ) E  " + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    strSqlString.Append("   AND A.MAT_ID = E.MAT_ID(+) " + "\n");
                    strSqlString.Append("   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("   AND (A.MAT_ID = '" + strMatCode + "' OR  A.MAT_ID = '" + strMatCode + "-O') " + "\n");
                    strSqlString.Append(" GROUP BY REPLACE(A.MAT_ID, '-O', '') AS MAT_ID " + "\n");
                }
            }


            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        /// <summary>
        /// 계획대비 입고 현황을 위한 쿼리 생성
        /// </summary>
        /// <param name="strMatCode"></param>
        /// <param name="strProduct"></param>
        /// <returns></returns>
        private string MakeSqlStringForPopup_2(string strMatCode, string date_1, string date_2)
        {
            StringBuilder strSqlString = new StringBuilder();

            string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값

            if (ckbKpcs.Checked == true)
                sKpcsValue = "1000";
            else
                sKpcsValue = "1";

            strSqlString.Append("SELECT MATCODE, FLD_NM " + "\n");
            strSqlString.Append("     , ROUND((NVL(D0,0)+NVL(D1,0)+NVL(D2,0)+NVL(D3,0)+NVL(D4,0)+NVL(D5,0)+NVL(D6,0))/" + sKpcsValue + ",0) AS TTL " + "\n");
            strSqlString.Append("     , ROUND(NVL(D6,0)/" + sKpcsValue + ",0) AS D6 " + "\n");
            strSqlString.Append("     , ROUND(NVL(D5,0)/" + sKpcsValue + ",0) AS D5 " + "\n");
            strSqlString.Append("     , ROUND(NVL(D4,0)/" + sKpcsValue + ",0) AS D4 " + "\n");
            strSqlString.Append("     , ROUND(NVL(D3,0)/" + sKpcsValue + ",0) AS D3 " + "\n");
            strSqlString.Append("     , ROUND(NVL(D2,0)/" + sKpcsValue + ",0) AS D2 " + "\n");
            strSqlString.Append("     , ROUND(NVL(D1,0)/" + sKpcsValue + ",0) AS D1 " + "\n");
            strSqlString.Append("     , ROUND(NVL(D0,0)/" + sKpcsValue + ",0) AS D0 " + "\n");
            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT '" + strMatCode + "' AS MATCODE, FLD_NM  " + "\n");
            strSqlString.Append("             , SUM(DECODE(PRD, '" + cdvDate.Value.AddDays(-6).ToString("yyyyMMdd") + "', PVAL)) D0  " + "\n");
            strSqlString.Append("             , SUM(DECODE(PRD, '" + cdvDate.Value.AddDays(-5).ToString("yyyyMMdd") + "', PVAL)) D1  " + "\n");
            strSqlString.Append("             , SUM(DECODE(PRD, '" + cdvDate.Value.AddDays(-4).ToString("yyyyMMdd") + "', PVAL)) D2  " + "\n");
            strSqlString.Append("             , SUM(DECODE(PRD, '" + cdvDate.Value.AddDays(-3).ToString("yyyyMMdd") + "', PVAL)) D3  " + "\n");
            strSqlString.Append("             , SUM(DECODE(PRD, '" + cdvDate.Value.AddDays(-2).ToString("yyyyMMdd") + "', PVAL)) D4  " + "\n");
            strSqlString.Append("             , SUM(DECODE(PRD, '" + cdvDate.Value.AddDays(-1).ToString("yyyyMMdd") + "', PVAL)) D5  " + "\n");
            strSqlString.Append("             , SUM(DECODE(PRD, '" + cdvDate.Value.ToString("yyyyMMdd") + "', PVAL)) D6  " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT ROWNUM RNUM " + "\n");
            strSqlString.Append("                     , DT.PRD " + "\n");
            strSqlString.Append("                     , MOD (ROWNUM - 1, 3) + 1 DT_SN " + "\n");
            strSqlString.Append("                     , DECODE (MOD (ROWNUM - 1, 3) + 1, 1, '계획', 2, '실적', 3, '누계과부족') FLD_NM " + "\n");
            strSqlString.Append("                     , DECODE (MOD (ROWNUM - 1, 3) + 1, 1, I1, 2, I2, 3, I3) PVAL " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT DT.PRD, DT.I1, DT.I2, DT.I3 " + "\n");
            strSqlString.Append("                          FROM ( " + "\n");
            strSqlString.Append("                                SELECT PRD " + "\n");
            strSqlString.Append("                                     , SUM(NVL(I1,0)) I1 " + "\n");
            strSqlString.Append("                                     , SUM(NVL(I2,0)) I2 " + "\n");
            strSqlString.Append("                                     , SUM(NVL(I2,0)-NVL(I1,0)) I3 " + "\n");
            strSqlString.Append("                                  FROM ( " + "\n");
            strSqlString.Append("                                        SELECT D.KEY_1 PRD " + "\n");
            strSqlString.Append("                                             , NVL(TO_NUMBER(D.DATA_1),0) I1 " + "\n");
            strSqlString.Append("                                             , 0 I2 " + "\n");
            strSqlString.Append("                                          FROM MGCMTBLDAT D " + "\n");
            strSqlString.Append("                                         WHERE 1=1 " + "\n");
            strSqlString.Append("                                           AND D.TABLE_NAME = 'H_MAT_INPUT_PLAN' " + "\n");
            strSqlString.Append("                                           AND D.KEY_2 = '" + strMatCode + "'  " + "\n");
            strSqlString.Append("                                           AND D.KEY_1 BETWEEN '" + cdvDate.Value.AddDays(-6).ToString("yyyyMMdd") + "' AND '" + cdvDate.Value.ToString("yyyyMMdd") + "'  " + "\n");
            strSqlString.Append("                                        UNION ALL " + "\n");
            strSqlString.Append("                                        SELECT R.WORK_DATE PRD " + "\n");
            strSqlString.Append("                                             , 0 I1 " + "\n");
            strSqlString.Append("                                             , SUM(NVL(R.S1_END_QTY_1,0)+NVL(R.S2_END_QTY_1,0)+NVL(R.S3_END_QTY_1,0)+NVL(R.S1_END_RWK_QTY_1,0) + NVL(R.S2_END_RWK_QTY_1,0) + NVL(R.S3_END_RWK_QTY_1,0)) AS I2 " + "\n");
            strSqlString.Append("                                          FROM RSUMWIPMOV R " + "\n");
            strSqlString.Append("                                         WHERE 1=1 " + "\n");
            strSqlString.Append("                                           AND R.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                           AND R.WORK_DATE BETWEEN '" + cdvDate.Value.AddDays(-6).ToString("yyyyMMdd") + "' AND '" + cdvDate.Value.ToString("yyyyMMdd") + "' " + "\n");
            strSqlString.Append("                                           AND R.OPER ='V0000' " + "\n");

            if (cdvLotType.SelectedItem.ToString() != "ALL")
                strSqlString.Append("                                           AND R.CM_KEY_3 LIKE '" + cdvLotType.SelectedItem.ToString() + "' " + "\n");

            strSqlString.Append("                                           AND R.MAT_ID = '" + strMatCode + "' " + "\n");
            strSqlString.Append("                                         GROUP BY R.WORK_DATE " + "\n");
            strSqlString.Append("                                       ) " + "\n");
            strSqlString.Append("                                 GROUP BY PRD " + "\n");
            strSqlString.Append("                                 ORDER BY PRD ASC " + "\n");
            strSqlString.Append("                               ) DT " + "\n");
            strSqlString.Append("                             , ( SELECT 1 " + "\n");
            strSqlString.Append("                                   FROM DUAL " + "\n");
            strSqlString.Append("                                CONNECT BY LEVEL <= 3 " + "\n");
            strSqlString.Append("                               ) " + "\n");
            strSqlString.Append("                         ORDER BY DT.PRD ASC " + "\n");
            strSqlString.Append("                        ) DT " + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("         GROUP BY FLD_NM " + "\n");
            strSqlString.Append("       ) " + "\n");




            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        /// <summary>
        /// 입고 계획을 위한 쿼리 생성
        /// </summary>
        private string MakeSqlStringForPopup_3(DataTable dtDateList, DateTime dtPlandate, string strMatCode)
        {
            string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값

            if (ckbKpcs.Checked == true)
                sKpcsValue = "1000";
            else
                sKpcsValue = "1";

            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("WITH OUTPUT AS ( " + "\n");
            strSqlString.Append("                SELECT BU_DATE " + "\n");
            strSqlString.Append("                     , MAT_ID " + "\n");
            strSqlString.Append("                     , VENDOR_CODE " + "\n");
            strSqlString.Append("                     , SUM(NVL(QUANTITY,0)) AS QUANTITY " + "\n");
            strSqlString.Append("                 FROM CWMSLOTHIS@RPTTOMES " + "\n");
            strSqlString.Append("                WHERE MOVEMENT_CODE IN ('A01','A02','A31') " + "\n");
            strSqlString.Append("                  AND BU_DATE BETWEEN '" + dtDateList.Rows[0][0].ToString() + "' AND '" + DateTime.Now.ToString("yyyyMMdd") + "' " + "\n");
            strSqlString.Append("                  AND RESV_FIELD_3 = ' ' " + "\n");
            strSqlString.Append("                GROUP BY BU_DATE, MAT_ID, VENDOR_CODE " + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("   , PLAN AS ( " + "\n");
            strSqlString.Append("              SELECT FACTORY " + "\n");
            strSqlString.Append("                   , CUSTOMER " + "\n");
            strSqlString.Append("                   , MAT_ID " + "\n");
            strSqlString.Append("                   , PRODUCT " + "\n");
            strSqlString.Append("                   , PKG " + "\n");
            strSqlString.Append("                   , VENDOR_CODE " + "\n");
            strSqlString.Append("                   , SUPPLY " + "\n");
            strSqlString.Append("                   , RN AS DAY_SEQ " + "\n");
            strSqlString.Append("                   , TO_CHAR(TO_DATE(SUBSTR(PLAN_DATE,1,6)||'01', 'YYYYMMDD') + RN - 1, 'YYYYMMDD') AS PLAN_DATE " + "\n");
            strSqlString.Append("                   , CASE " + "\n");

            for (int i = 0; i < dtDateList.Rows.Count; i++)
            {
                strSqlString.Append(string.Format("            WHEN RN = {0}  THEN D_{0} " + "\n", i + 1, i + 1));
            }

            strSqlString.Append("       ELSE 0 END PLN_QTY " + "\n");
            strSqlString.Append("  FROM CMATPLNINP@RPTTOMES " + "\n");
            strSqlString.Append("     , (SELECT ROWNUM RN FROM DUAL CONNECT BY LEVEL <= 62) SEQ " + "\n");
            strSqlString.Append(" WHERE PLAN_DATE <> ' ' " + "\n");
            strSqlString.Append("   AND MAT_ID = '" + strMatCode + "' " + "\n");
            strSqlString.Append("   AND PLAN_DATE BETWEEN '" + dtPlandate.ToString("yyyyMMdd") + "' AND '" + dtPlandate.AddDays(30).ToString("yyyyMMdd") + "' " + "\n");
            strSqlString.Append(") " + "\n");
            strSqlString.Append("SELECT \"코드\", \"업체\", GUBUN " + "\n");

            for (int i = 0; i < dtDateList.Rows.Count; i++)
            {
                strSqlString.Append(string.Format("                 , ROUND(SUM(\"{0}\")/" + sKpcsValue + ",0) AS D{1} " + "\n", dtDateList.Rows[i][0].ToString(), i + 1));
            }

            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT \"코드\" " + "\n");
            strSqlString.Append("             , \"업체\" " + "\n");
            strSqlString.Append("             , DECODE( RN, 1, '계획', 2, '실적', 3, '누계차') AS GUBUN " + "\n");

            for (int i = 0; i < dtDateList.Rows.Count; i++)
            {
                strSqlString.Append("             , CASE WHEN \"날짜\" = '" + dtDateList.Rows[i][0] + "' AND RN = 1 THEN \"계획수량\"  " + "\n");
                strSqlString.Append("                    WHEN \"날짜\" = '" + dtDateList.Rows[i][0] + "' AND RN = 2 THEN \"실적수량\"  " + "\n");
                strSqlString.Append("                    WHEN \"날짜\" = '" + dtDateList.Rows[i][0] + "' AND RN = 3 THEN \"누계차\" ELSE 0 END AS \"" + dtDateList.Rows[i][0] + "\" " + "\n");
            }

            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT PLAN.MAT_ID \"코드\" " + "\n");
            strSqlString.Append("                     , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'VENDOR' AND KEY_1 = PLAN.VENDOR_CODE) AS \"업체\" " + "\n");
            strSqlString.Append("                     , PLAN.PLAN_DATE \"날짜\" " + "\n");
            strSqlString.Append("                     , PLAN.PLN_QTY \"계획수량\" " + "\n");
            strSqlString.Append("                     , OUTPUT.QUANTITY \"실적수량\" " + "\n");

            //2014-08-18-임종우 : 발주 현황 상세 팝업에서 누계차는 업데이트한 날짜 기준 부터 계산 되도록...
            strSqlString.Append("                     , SUM(CASE WHEN PLAN.PLAN_DATE < '" + dtPlandate.ToString("yyyyMMdd") + "' THEN 0 ELSE PLAN.PLN_QTY END) OVER(PARTITION BY PLAN.MAT_ID, PLAN.VENDOR_CODE ORDER BY PLAN.PLAN_DATE) CUM_SUM_PLAN " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN PLAN.PLAN_DATE < '" + dtPlandate.ToString("yyyyMMdd") + "' THEN 0 ELSE NVL(OUTPUT.QUANTITY,0) END) OVER(PARTITION BY PLAN.MAT_ID, PLAN.VENDOR_CODE ORDER BY PLAN.PLAN_DATE) CUM_SUM_QUANTITY " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN PLAN.PLAN_DATE < '" + dtPlandate.ToString("yyyyMMdd") + "' THEN 0 ELSE NVL(OUTPUT.QUANTITY,0) END) OVER(PARTITION BY PLAN.MAT_ID, PLAN.VENDOR_CODE ORDER BY PLAN.PLAN_DATE) - SUM(CASE WHEN PLAN.PLAN_DATE < '" + dtPlandate.ToString("yyyyMMdd") + "' THEN 0 ELSE PLAN.PLN_QTY END) OVER(PARTITION BY PLAN.MAT_ID, PLAN.VENDOR_CODE ORDER BY PLAN.PLAN_DATE) \"누계차\" " + "\n");
            //strSqlString.Append("                     , SUM(PLAN.PLN_QTY) OVER(PARTITION BY PLAN.MAT_ID, PLAN.VENDOR_CODE ORDER BY PLAN.PLAN_DATE) CUM_SUM_PLAN " + "\n");
            //strSqlString.Append("                     , SUM(NVL(OUTPUT.QUANTITY,0)) OVER(PARTITION BY PLAN.MAT_ID, PLAN.VENDOR_CODE ORDER BY PLAN.PLAN_DATE) CUM_SUM_QUANTITY " + "\n");
            //strSqlString.Append("                     , SUM(NVL(OUTPUT.QUANTITY,0)) OVER(PARTITION BY PLAN.MAT_ID, PLAN.VENDOR_CODE ORDER BY PLAN.PLAN_DATE) - SUM(PLAN.PLN_QTY) OVER(PARTITION BY PLAN.MAT_ID, PLAN.VENDOR_CODE ORDER BY PLAN.PLAN_DATE) \"누계차\" " + "\n");
            strSqlString.Append("                  FROM PLAN, OUTPUT " + "\n");
            strSqlString.Append("                 WHERE PLAN.MAT_ID = OUTPUT.MAT_ID(+) " + "\n");
            strSqlString.Append("                   AND PLAN.VENDOR_CODE = OUTPUT.VENDOR_CODE(+) " + "\n");
            strSqlString.Append("                   AND PLAN.PLAN_DATE = OUTPUT.BU_DATE(+) " + "\n");
            strSqlString.Append("               ) A " + "\n");
            strSqlString.Append("             , (SELECT ROWNUM RN FROM DUAL CONNECT BY LEVEL <= 3) SEQ " + "\n");
            strSqlString.Append("        ) " + "\n");
            strSqlString.Append(" GROUP BY GROUPING SETS ( (\"코드\", \"업체\", GUBUN), (GUBUN) ) " + "\n");
            strSqlString.Append(" ORDER BY DECODE(\"업체\", NULL, 1, 2), \"업체\", DECODE(GUBUN, '계획', 1, '실적', 2, '누계차',3, 4)         " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        /// <summary>
        /// SAP 코드 목록을 위한 쿼리 생성
        /// </summary>
        /// <param name="strMatCode"></param>
        /// <returns></returns>
        private string MakeSqlStringForPopup_4(string strMatCode)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT DISTINCT '" + strMatCode + "' AS MATCODE, VENDOR_ID AS SAP_CODE " + "\n");
            strSqlString.Append("  FROM MWIPMATDEF " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND MAT_ID IN ( " + "\n");
            strSqlString.Append("                  SELECT DISTINCT B.PARTNUMBER " + "\n");
            strSqlString.Append("                    FROM CWIPMATDEF@RPTTOMES A " + "\n");
            strSqlString.Append("                       , CWIPBOMDEF B " + "\n");
            strSqlString.Append("                   WHERE 1=1 " + "\n");
            strSqlString.Append("                     AND A.MAT_ID = B.MATCODE " + "\n");
            strSqlString.Append("                     AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                     AND B.RESV_FIELD_2 <> ' ' " + "\n");
            strSqlString.Append("                     AND B.RESV_FLAG_1 = 'Y' " + "\n");
            strSqlString.Append("                     AND B.STEPID <> 'A0300' " + "\n");
            strSqlString.Append("                     AND B.MATCODE = '" + strMatCode + "' " + "\n");
            strSqlString.Append("                 ) " + "\n");
            strSqlString.Append("   AND VENDOR_ID <> ' ' " + "\n");
            strSqlString.Append("   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("   AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append(" ORDER BY VENDOR_ID " + "\n");


            return strSqlString.ToString();
        }

        private string MakeSqlStringForPopup_5(DateTime dtPlanDate)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT SYS_DATE " + "\n");
            strSqlString.Append("  FROM MWIPCALDEF " + "\n");
            strSqlString.Append(" WHERE CALENDAR_ID = 'OTD' " + "\n");
            strSqlString.Append("   AND (SYS_YEAR = '" + dtPlanDate.ToString("yyyy") + "' AND LPAD(SYS_MONTH,2,'0') = '" + dtPlanDate.ToString("MM") + "' OR SYS_YEAR = '" + dtPlanDate.AddMonths(1).ToString("yyyy") + "' AND LPAD(SYS_MONTH,2,'0') = '" + dtPlanDate.AddMonths(1).ToString("MM") + "') " + "\n");
            strSqlString.Append(" ORDER BY SYS_DATE ASC " + "\n");

            return strSqlString.ToString();
        }

        #endregion

        #region " Event 처리 "


        #region " btnView_Click : View버튼을 선택했을 때 "

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            DataTable dt1 = null;

            StringBuilder strSqlString = new StringBuilder();

            // 마지막 SUMMARY 시간 가져오기
            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("SELECT DISTINCT TO_DATE(UPDATE_TIME,'YYYY-MM-DD HH24MISS') FROM RSUMWIPMAT");

                dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
                lblUpdateTime.Text = dt1.Rows[0][0].ToString();
            }
            else
            {
                lblUpdateTime.Text = cdvDate.Value.ToString("yyyy-MM-dd") + " (22시 기준)";
            }

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
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                //표구성에따른 항목 Display
                //spdData.RPT_ColumnConfigFromTable(btnSort);
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 15, null, null, btnSort);

                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 15, 0, 1, true, Align.Center, VerticalAlign.Center);

                //발주잔량에서 입고계획이 있는 자재는 셀을 빨강으로..
                SetPlanCellColor();

                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 2, null, null);
                //spdData.Sheets[0].FrozenColumnCount = 10;
                //spdData.RPT_AutoFit(false);

                //for (int i = 0; i < 5; i++)
                //{
                //    SetMaxVertical(ClickPos5+i);
                //}


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
        /// 원부자재 입고 계획이 있을 시 셀을 빨강으로..
        /// </summary>
        private void SetPlanCellColor()
        {
            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", "SELECT DISTINCT MAT_ID FROM CMATPLNINP@RPTTOMES WHERE PLAN_DATE <> ' '");

            int matcodeIdx = 0;

            for (int i = 0; i < spdData.ActiveSheet.Columns.Count; i++)
            {
                if ("MATCODE" == spdData.ActiveSheet.GetColumnLabel(0, i))
                {
                    matcodeIdx = i;
                    break;
                }
                else
                {
                    matcodeIdx = -1;
                }
            }

            if(matcodeIdx != -1)
            {
                for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    string matCode = Convert.ToString(spdData.ActiveSheet.Cells[i, matcodeIdx].Value);

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (matCode == dt.Rows[j][0].ToString())
                        {
                            spdData.ActiveSheet.Cells[i, 30].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(100)), ((System.Byte)(100)));
                            break;
                        }
                    }
                }
            }

            
        }

        /// <summary>
        /// 원부자재 보유 현황 subtotal (sum 하지 않도록 함)
        /// </summary>
        /// <param name="nSampleNormalRowPos"></param>
        /// <param name="nColPos"></param>
        /// <param name="bWithNull"></param>
        public void SetMaxVertical(int nColPos)
        {
            int subTotal = 0;
            int subTotTotal = 0;
            int grdTotal = 0;

            //int checkValue = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(1);

            Color valColor = spdData.ActiveSheet.Cells[1, nColPos].BackColor;
            Color chkColor = spdData.ActiveSheet.Cells[1, 0].BackColor;

            for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                //if (spdData.ActiveSheet.Cells[i, 0].BackColor == chkColor)
                //{
                if (spdData.ActiveSheet.Cells[i, nColPos].BackColor == valColor)
                {
                    subTotal = Convert.ToInt32(spdData.ActiveSheet.Cells[i, nColPos].Value);
                }
                else
                {
                    spdData.ActiveSheet.Cells[i, nColPos].Value = subTotal;

                    grdTotal += subTotal;
                    subTotTotal += subTotal;
                    subTotal = 0;
                }

            }

            spdData.ActiveSheet.Cells[0, nColPos].Value = grdTotal;
            /*
            // GrandTotal 구함.
            if (totalDivide != 0)
                spdData.ActiveSheet.Cells[0, nColPos].Value = totalSum / totalDivide;

            else
            {
                // subTotal이 하나도 없을때 즉 Raw Data만 있을때 GrandTotal 구함.
                if (divide != 0)
                {
                    spdData.ActiveSheet.Cells[0, nColPos].Value = sum / divide;
                }
            }
            */
        }

        //private void SetMatFieldColor()
        //{
        //    //22, 25, 28
        //    for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i++)
        //    {
        //        if (spdData.ActiveSheet.Cells[i, 22].Value < 0)
        //        {
        //            spdData.ActiveSheet.Cells[i, 22].Value + spdData.ActiveSheet.Cells[i, 25].Value
        //        }
        //    }
        //}


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

        #endregion

        private void cdvMatType_ValueButtonPress(object sender, EventArgs e)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT KEY_1 AS Code, DATA_1 AS Data " + "\n");
            strSqlString.Append("  FROM MGCMTBLDAT " + "\n");
            strSqlString.Append(" WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("   AND TABLE_NAME = 'MATERIAL_TYPE' " + "\n");
            strSqlString.Append("   AND KEY_1 IN ('TE','PB','LF','GW','SW','CW','MC', 'SB', 'RC') " + "\n");
            strSqlString.Append(" ORDER BY KEY_1 " + "\n");

            cdvMatType.sDynamicQuery = strSqlString.ToString();
        }

        /// <summary>
        /// 스프레드를 클릭했을 때 자재코드에 해당하는 정보를 팝업 화면에 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader == true) return;

            int iClickPos = ClickPos5;
            int tot_1 = 24;    //total
            int tot_2 = 25;    //SMT
            int tot_3 = 26;    //현장
            int tot_4 = 27;    //외주
            int tot_5 = 28;    //현장창고
            int tot_6 = 29;    //창고(구매)

            int matcodeIdx = 0; //자재코드 컬럼인덱스
            bool bMatcode = false;
                        
            string strMatCode = "";            
            string strTotal = spdData.ActiveSheet.GetText(e.Row, ClickPos1);


            for (int i = 0; i < spdData.ActiveSheet.Columns.Count; i++)
            {
                if ("MATCODE" == spdData.ActiveSheet.GetColumnLabel(0, i))
                {
                    matcodeIdx = i;
                    strMatCode = spdData.ActiveSheet.GetText(e.Row, matcodeIdx);
                    bMatcode = spdData.ActiveSheet.Cells[e.Row, matcodeIdx].Column.Visible;
                    break;
                }
                else
                {
                    matcodeIdx = -1;
                    strMatCode = "";
                }
            }

            // PART NO 를 클릭 했을 때 새로운 창으로 실행
            if (e.Column == tot_1 || e.Column == tot_2 || e.Column == tot_3 || e.Column == tot_5 || e.Column == tot_6)
            {
                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue()) //현재 재공일 경우만 유효기간 팝업 생성
                {
                    DataTable dt = null;

                    if (matcodeIdx != -1 && bMatcode)
                    {
                        if (e.Column == tot_1)//total
                        {
                            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForPopup_1("TTL", strMatCode));
                        }
                        else if (e.Column == tot_2)//SMT
                        {
                            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForPopup_1("SMT_IN", strMatCode));
                        }
                        else if (e.Column == tot_3)//LINE
                        {
                            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForPopup_1("L_IN", strMatCode));
                        }
                        else if (e.Column == tot_4)//외주
                        {
                            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForPopup_1("WIK_WIP", strMatCode));
                        }
                        else if (e.Column == tot_5)//현장창고
                        {
                            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForPopup_1("INV_L_QTY", strMatCode));
                        }
                        else if (e.Column == tot_6)//창고(구매)
                        {
                            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForPopup_1("INV_QTY", strMatCode));
                        }


                        if (dt != null && dt.Rows.Count > 0)
                        {
                            System.Windows.Forms.Form frm = new MAT070503_P1("Quantity by Expiration Date", dt, strTotal.Equals("") ? "0" : strTotal);
                            frm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("해당 하는 자재코드의 세부 정보가 없습니다.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("자재코드를 그룹에서 선택해야 합니다.");
                    }
                }
            }
            else if (e.Column == matcodeIdx)
            {
                
            }
            else if (e.Column == 30) //발주 잔량 클릭
            {
                //strMatCode = "R160001060";
                DataTable dtPlanDate = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", "SELECT MAX(PLAN_DATE) AS PLAN_DATE FROM CMATPLNINP@RPTTOMES WHERE MAT_ID = '" + strMatCode + "'");
                string strPlanDate = "";

                if (dtPlanDate.Rows.Count == 0 || dtPlanDate.Rows[0][0].ToString() == "")
                    strPlanDate = DateTime.Now.ToString("yyyyMM") + "01";
                else
                    strPlanDate = dtPlanDate.Rows[0][0].ToString();

                DateTime dtp = Convert.ToDateTime(strPlanDate.Substring(0, 4) + "-" + strPlanDate.Substring(4, 2) + "-" + strPlanDate.Substring(6, 2));
                DataTable dtPlanList = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForPopup_5(dtp));

                //입고 계획
                if (matcodeIdx != -1 && bMatcode)
                {
                    DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForPopup_3(dtPlanList, dtp, strMatCode));

                    if (dt.Rows.Count != 0)
                    {
                        System.Windows.Forms.Form frm = new MAT070503_P3("Warehousing plan", dt, strMatCode, dtp);
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("해당 하는 자재코드의 세부 정보가 없습니다.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("자재코드를 그룹에서 선택해야 합니다.");
                }
            }
        }


    }
}

