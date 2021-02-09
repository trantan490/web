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
    public partial class PRD010206 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        string[] dayArry = new string[5];
        string[] dayArry1 = new string[5];
        int iShip_Remain = 0;
        /// <summary>
        /// 클  래  스: PRD010206<br/>
        /// 클래스요약: BG ~ SAW 공정 생산일보<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2010-08-31<br/>
        /// 상세  설명: BG ~ SAW 공정 생산일보 조회<br/>
        /// 변경  내용: <br/>
        /// 2010-09-06-임종우 : CHIP 입고 수량 HYNIX는 06시 기준, 그 외는 22시 기준으로 변경 함. (김문한K 요청)
        /// 2010-09-10-임종우 : 월간 계획 & 월 누적기준 두개의 데이터가 존재 하는 것만 표시 되도록 체크 박스 추가(김문한K 요청)
        /// 2010-09-16-임종우 : LOT_TYPE 기본으로 P%으로 표시 (김문한K 요청)
        /// 2010-09-28-임종우 : KPCS 선택시에도 소수점 안나오도록 수정 (김문한K 요청)
        /// 2010-10-01-임종우 : 부진율 = 표준 진도율 - 진도율 -> 단 부진율이 0보다 작은면(진도율이 클경우) 0으로 표시 (김문한K 요청)
        /// 2010-10-18-임종우 : 재공 표시 부분에 D/A 공정 추가함(A0300 + OPER_GRP_1 이 D/A 인 것). 단 Total 수량에서는 D/A 수량 제외 (김문한K 요청)
        /// 2011-01-17-임종우 : D/A 재공에 공정 재공 그룹 S/P 수량 추가함 (김문한K 요청)
        /// 2011-03-17-임종우 : MAJOR CODE 추가 (김문한 요청)
        /// 2011-05-11-임종우 : 실적 진도율, 일필요량 추가 (김문한 요청)
        /// 2011-09-06-배수민 : 일필요량, 일평균, D/A재공, W/B재공 로직 추가 & 기존 로직에 해당공정명 추가 및 변경 (김문한K 요청)
        /// 2011-09-08-배수민 : SAW실적 일필요량 기존 로직 변경 (김문한K 요청)
        /// 2011-12-26-임종우 : MWIPCALDEF 의 작년,올해 마지막 주차 겹치는 에러 발생으로 SYS_YEAR -> PLAN_YEAR 으로 변경
        /// 2012-02-01-김민우 : MRASRESSTS의 공정정보 RES_STS_9에서 RES_STS_8로 변경
        /// </summary>
        public PRD010206()
        {
            InitializeComponent();
            cdvDate.Value = DateTime.Now.AddDays(-1);
            SortInit();
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

            LabelTextChange();

            try
            {
                //if (ckbKpcs.Checked == false)
                //{
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Major Code", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Family", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("LD Count", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 7, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Density", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Generation", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Product", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Cust Device", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Monthly plan", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("Monthly CHIP warehousing", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("cumulative total", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Daily requirement", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Progress rate", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("Sluggish rate", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 90);
                spdData.RPT_MerageHeaderColumnSpan(0, 13, 5);

                spdData.RPT_AddBasicColumn("CHIP warehousing", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(dayArry[0], 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[1], 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[2], 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[3], 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[4], 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Daily average", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 18, 6);

                spdData.RPT_AddBasicColumn("SAW performance", 0, 24, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(dayArry[0], 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[1], 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[2], 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[3], 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[4], 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TOTAL", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Daily requirement", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Daily average", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Progress rate", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 24, 9);

                spdData.RPT_AddBasicColumn("WIP", 0, 33, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("STOCK", 1, 33, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("L/N", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("B/G", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("L/G", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70); //
                spdData.RPT_AddBasicColumn("SAW", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("GATE", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TOTAL", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                //spdData.RPT_AddBasicColumn("D/A", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 33, 7);

                //2011-09-06-배수민
                spdData.RPT_AddBasicColumn("D/A WIP", 0, 40, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("D/A1", 1, 40, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A2", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A3", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TOTAL", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 40, 4);

                //2011-09-06-배수민
                spdData.RPT_AddBasicColumn("W/B WIP", 0, 44, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("W/B1", 1, 44, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W/B2", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W/B3", 1, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TOTAL", 1, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 44, 4);

                spdData.RPT_AddBasicColumn("Equipment Status", 0, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("SAW", 1, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 48, 2);

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
                spdData.RPT_MerageHeaderRowSpan(0, 12, 2);
                spdData.RPT_ColumnConfigFromTable(btnSort);
                //}
                //else
                //{
                //    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                //    spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                //    spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                //    spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                //    spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                //    spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                //    spdData.RPT_AddBasicColumn("Pin Type", 0, 6, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                //    spdData.RPT_AddBasicColumn("Density", 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                //    spdData.RPT_AddBasicColumn("Generation", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);                    
                //    spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                //    spdData.RPT_AddBasicColumn("Cust Device", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                //    spdData.RPT_AddBasicColumn("Monthly plan", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                //    spdData.RPT_AddBasicColumn("Monthly CHIP warehousing", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                //    spdData.RPT_AddBasicColumn("cumulative total", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_AddBasicColumn("residual quantity", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_AddBasicColumn("Daily requirement", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_AddBasicColumn("Progress rate", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_AddBasicColumn("Sluggish rate", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 90);
                //    spdData.RPT_MerageHeaderColumnSpan(0, 12, 5);

                //    spdData.RPT_AddBasicColumn("CHIP warehousing", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                //    spdData.RPT_AddBasicColumn(dayArry[0], 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_AddBasicColumn(dayArry[1], 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_AddBasicColumn(dayArry[2], 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_AddBasicColumn(dayArry[3], 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_AddBasicColumn(dayArry[4], 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_AddBasicColumn("Daily average", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_MerageHeaderColumnSpan(0, 17, 6);

                //    spdData.RPT_AddBasicColumn("SAW performance", 0, 23, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                //    spdData.RPT_AddBasicColumn(dayArry[0], 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_AddBasicColumn(dayArry[1], 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_AddBasicColumn(dayArry[2], 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_AddBasicColumn(dayArry[3], 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_AddBasicColumn(dayArry[4], 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_AddBasicColumn("TOTAL", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_MerageHeaderColumnSpan(0, 23, 6);

                //    spdData.RPT_AddBasicColumn("WIP", 0, 29, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                //    spdData.RPT_AddBasicColumn("STOCK", 1, 29, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_AddBasicColumn("L/N", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_AddBasicColumn("B/G", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_AddBasicColumn("SAW", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_AddBasicColumn("U/V", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_AddBasicColumn("TOTAL", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                //    spdData.RPT_MerageHeaderColumnSpan(0, 29, 6);

                //    spdData.RPT_AddBasicColumn("Equipment Status", 0, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                //    spdData.RPT_AddBasicColumn("SAW", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                //    spdData.RPT_MerageHeaderColumnSpan(0, 35, 2);

                //    spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                //    spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                //    spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                //    spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                //    spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                //    spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
                //    spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
                //    spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
                //    spdData.RPT_MerageHeaderRowSpan(0, 8, 2);
                //    spdData.RPT_MerageHeaderRowSpan(0, 9, 2);
                //    spdData.RPT_MerageHeaderRowSpan(0, 10, 2);
                //    spdData.RPT_MerageHeaderRowSpan(0, 11, 2);                    
                //    spdData.RPT_ColumnConfigFromTable(btnSort);  
                //}

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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "PLN.MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = PLN.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "PLN.MAT_GRP_9", "PLN.MAT_GRP_9 AS MAJOR_CODE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "PLN.MAT_GRP_2", "PLN.MAT_GRP_2 AS FAMILY", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "PLN.MAT_GRP_3", "PLN.MAT_GRP_3 AS PACKAGE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "PLN.MAT_GRP_4", "PLN.MAT_GRP_4 AS TYPE1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "PLN.MAT_GRP_5", "PLN.MAT_GRP_5 AS TYPE2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "PLN.MAT_GRP_6", "PLN.MAT_GRP_6 AS \"LD COUNT\"", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "PLN.MAT_CMF_10", "PLN.MAT_CMF_10 AS PIN_TYPE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "PLN.MAT_GRP_7", "PLN.MAT_GRP_7 AS DENSITY", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "PLN.MAT_GRP_8", "PLN.MAT_GRP_8 AS GENERATION", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "PLN.MAT_ID", "PLN.MAT_ID AS PRODUCT", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST DEVICE", "PLN.MAT_CMF_7", "PLN.MAT_CMF_7 AS CUST_DEVICE", false);
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
            string start_day;
            string end_day;
            string today;
            string year;
            string month;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            string getStartDate = cdvDate.Value.ToString("yyyy-MM") + "-01";

            start_day = cdvDate.Value.ToString("yyyyMM") + "01";
            end_day = Convert.ToDateTime(getStartDate).AddMonths(1).AddDays(-1).ToString("yyyyMMdd");
            today = cdvDate.Value.ToString("yyyyMMdd");
            year = cdvDate.Value.ToString("yyyy");
            month = cdvDate.Value.ToString("yyyyMM");

            // 지난주차의 마지막일 가져오기
            DataTable dt = null;
            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1(year, today));
            string Lastweek_lastday = dt.Rows[0][0].ToString();

            strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond2);

            if (ckbKpcs.Checked == true)
            {
                strSqlString.Append("     , ROUND(SUM(NVL(PLN.MON_PLAN,0)) / 1000, 1) AS MON" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(RCV.MON_RCV,0)) / 1000, 1) AS MON_IN" + "\n");
                strSqlString.Append("     , ROUND((SUM(NVL(PLN.MON_PLAN,0)) - SUM(NVL(RCV.MON_RCV,0))) / 1000, 1) AS DEF" + "\n");
                strSqlString.Append("     , ROUND(((SUM(NVL(PLN.MON_PLAN,0)) - SUM(NVL(RCV.MON_RCV,0))) / " + lblRemain.Text + ") / 1000, 1) AS TARGET" + "\n");
                strSqlString.Append("     , ROUND(DECODE(SUM(NVL(PLN.MON_PLAN,0)), 0, 0, SUM(NVL(RCV.MON_RCV,0)) / SUM(NVL(PLN.MON_PLAN,0)) * 100), 2) AS JINDO" + "\n");

                // 2010-10-01-임종우 : 부진율 = 표준 진도율 - 진도율 -> 단 부진율이 0보다 작은면(진도율이 클경우) 0으로 표시 (김문한K 요청)
                //strSqlString.Append("     , ROUND(DECODE(SUM(NVL(PLN.MON_PLAN,0)), 0, 0, SUM(NVL(RCV.MON_RCV,0)) / SUM(NVL(PLN.MON_PLAN,0)) * 100 -" + lblJindo.Text.Replace("%", "") + "), 2) AS DEF_PER" + "\n");
                strSqlString.Append("     , CASE WHEN ROUND(DECODE(SUM(NVL(PLN.MON_PLAN,0)), 0, 0, " + lblJindo.Text.Replace("%", "") + " - (SUM(NVL(RCV.MON_RCV,0)) / SUM(NVL(PLN.MON_PLAN,0)) * 100)), 2) < 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE ROUND(DECODE(SUM(NVL(PLN.MON_PLAN,0)), 0, 0, " + lblJindo.Text.Replace("%", "") + " - (SUM(NVL(RCV.MON_RCV,0)) / SUM(NVL(PLN.MON_PLAN,0)) * 100)), 2)" + "\n");
                strSqlString.Append("       END AS DEF_PER" + "\n");

                strSqlString.Append("     , ROUND(SUM(NVL(RCV.RCV0,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(RCV.RCV1,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(RCV.RCV2,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(RCV.RCV3,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(RCV.RCV4,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(RCV.MON_RCV_AVG,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.SHP0,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.SHP1,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.SHP2,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.SHP3,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.SHP4,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.MON_SHP,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(((SUM(NVL(PLN.MON_PLAN,0)) - (SUM(NVL(RCV_SHP.MON_SHP,0)) + SUM(NVL(WIP_BOH.QTY_1,0))) ) / " + iShip_Remain + ") / 1000, 1) AS SHP_TARGET" + "\n"); //일필요량
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.MON_SHP_AVG,0)) / 1000, 0)" + "\n"); //SAW실적 일평균
                strSqlString.Append("     , ROUND(DECODE(SUM(NVL(PLN.MON_PLAN,0)), 0, 0, SUM(NVL(SHP.MON_SHP,0)) / SUM(NVL(PLN.MON_PLAN,0)) * 100), 2) AS SHP_JINDO" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.STOCK,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.LN,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.BG,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.LG,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.SAW,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.GATE,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.TOTAL,0)) / 1000, 0)" + "\n");

                strSqlString.Append("     , ROUND(SUM(NVL(WIP_DA.DA_1,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP_DA.DA_2,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP_DA.DA_3,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP_DA.TOTAL,0)) / 1000, 0)" + "\n");

                strSqlString.Append("     , ROUND(SUM(NVL(WIP_WB.WB_1,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP_WB.WB_2,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP_WB.WB_3,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP_WB.TOTAL,0)) / 1000, 0)" + "\n");
            }
            else
            {
                strSqlString.Append("     , SUM(NVL(PLN.MON_PLAN,0)) AS MON" + "\n");
                strSqlString.Append("     , SUM(NVL(RCV.MON_RCV,0)) AS MON_IN" + "\n");
                strSqlString.Append("     , SUM(NVL(PLN.MON_PLAN,0)) - SUM(NVL(RCV.MON_RCV,0)) AS DEF" + "\n");
                strSqlString.Append("     , ROUND((SUM(NVL(PLN.MON_PLAN,0)) - SUM(NVL(RCV.MON_RCV,0))) / " + lblRemain.Text + ", 0) AS TARGET" + "\n");
                strSqlString.Append("     , ROUND(DECODE(SUM(NVL(PLN.MON_PLAN,0)), 0, 0, SUM(NVL(RCV.MON_RCV,0)) / SUM(NVL(PLN.MON_PLAN,0)) * 100), 2) AS JINDO" + "\n");

                // 2010-10-01-임종우 : 부진율 = 표준 진도율 - 진도율 -> 단 부진율이 0보다 작은면(진도율이 클경우) 0으로 표시 (김문한K 요청)
                //strSqlString.Append("     , ROUND(DECODE(SUM(NVL(PLN.MON_PLAN,0)), 0, 0, SUM(NVL(RCV.MON_RCV,0)) / SUM(NVL(PLN.MON_PLAN,0)) * 100 -" + lblJindo.Text.Replace("%", "") + "), 2) AS DEF_PER" + "\n");
                strSqlString.Append("     , CASE WHEN ROUND(DECODE(SUM(NVL(PLN.MON_PLAN,0)), 0, 0, " + lblJindo.Text.Replace("%", "") + " - (SUM(NVL(RCV.MON_RCV,0)) / SUM(NVL(PLN.MON_PLAN,0)) * 100)), 2) < 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE ROUND(DECODE(SUM(NVL(PLN.MON_PLAN,0)), 0, 0, " + lblJindo.Text.Replace("%", "") + " - (SUM(NVL(RCV.MON_RCV,0)) / SUM(NVL(PLN.MON_PLAN,0)) * 100)), 2)" + "\n");
                strSqlString.Append("       END AS DEF_PER" + "\n");

                strSqlString.Append("     , SUM(NVL(RCV.RCV0,0))" + "\n");
                strSqlString.Append("     , SUM(NVL(RCV.RCV1,0))" + "\n");
                strSqlString.Append("     , SUM(NVL(RCV.RCV2,0))" + "\n");
                strSqlString.Append("     , SUM(NVL(RCV.RCV3,0))" + "\n");
                strSqlString.Append("     , SUM(NVL(RCV.RCV4,0))" + "\n");
                strSqlString.Append("     , SUM(NVL(RCV.MON_RCV_AVG,0))" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.SHP0,0))" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.SHP1,0))" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.SHP2,0))" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.SHP3,0))" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.SHP4,0))" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.MON_SHP,0))" + "\n");
                strSqlString.Append("     , ROUND(((SUM(NVL(PLN.MON_PLAN,0)) - (SUM(NVL(RCV_SHP.MON_SHP,0)) + SUM(NVL(WIP_BOH.QTY_1,0))) ) / " + iShip_Remain + ") / 1000, 1) AS SHP_TARGET" + "\n"); //일필요량
                strSqlString.Append("     , ROUND(DECODE(SUM(NVL(PLN.MON_PLAN,0)), 0, 0, SUM(NVL(SHP.MON_SHP,0)) / SUM(NVL(PLN.MON_PLAN,0)) * 100), 2) AS SHP_JINDO" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.STOCK,0))" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.LN,0))" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.BG,0))" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.LG,0))" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.SAW,0))" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.GATE,0))" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.TOTAL,0))" + "\n");

                strSqlString.Append("     , ROUND(SUM(NVL(WIP_DA.DA_1,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP_DA.DA_2,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP_DA.DA_3,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP_DA.TOTAL,0)) / 1000, 0)" + "\n");

                strSqlString.Append("     , ROUND(SUM(NVL(WIP_WB.WB_1,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP_WB.WB_2,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP_WB.WB_3,0)) / 1000, 0)" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP_WB.TOTAL,0)) / 1000, 0)" + "\n");
            }

            strSqlString.Append("     , SUM(NVL(RAS.QTY_1,0))" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_GRP_9, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7 " + "\n");
            strSqlString.Append("             , SUM(PLAN.PLAN_QTY_ASSY) AS MON_PLAN" + "\n");
            strSqlString.Append("          FROM MWIPMATDEF MAT " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                 SELECT FACTORY,MAT_ID,PLAN_QTY_ASSY,PLAN_MONTH " + "\n");
            strSqlString.Append("                   FROM ( " + "\n");
            strSqlString.Append("                          SELECT FACTORY, MAT_ID, PLAN_QTY_ASSY, PLAN_MONTH " + "\n");
            strSqlString.Append("                            FROM CWIPPLNMON " + "\n");
            strSqlString.Append("                           WHERE 1=1 " + "\n");
            strSqlString.Append("                             AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                             AND MAT_ID NOT IN ( " + "\n");
            strSqlString.Append("                                                SELECT MAT_ID " + "\n");
            strSqlString.Append("                                                  FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                                                 WHERE 1=1 " + "\n");
            strSqlString.Append("                                                   AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                                                   AND MAT_GRP_9 = 'S-LSI' " + "\n");
            strSqlString.Append("                                               ) " + "\n");
            strSqlString.Append("                          UNION ALL " + "\n");
            strSqlString.Append("                          SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_ASSY , '" + month + "' AS PLAN_MONTH " + "\n");
            strSqlString.Append("                            FROM ( " + "\n");
            strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
            strSqlString.Append("                                     FROM CWIPPLNDAY " + "\n");
            strSqlString.Append("                                    WHERE 1=1 " + "\n");
            strSqlString.Append("                                      AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + start_day + "' AND '" + end_day + "'\n");
            strSqlString.Append("                                      AND IN_OUT_FLAG = 'OUT' " + "\n");
            strSqlString.Append("                                      AND CLASS = 'ASSY' " + "\n");
            strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                                   UNION ALL " + "\n");
            strSqlString.Append("                                   SELECT CM_KEY_1 AS FACTORY, MAT_ID, " + "\n");
            strSqlString.Append("                                          SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS PLAN_QTY " + "\n");
            strSqlString.Append("                                     FROM RSUMFACMOV " + "\n");
            strSqlString.Append("                                    WHERE 1=1 " + "\n");
            strSqlString.Append("                                      AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                      AND CM_KEY_2 = 'PROD' " + "\n");
            strSqlString.Append("                                      AND CM_KEY_3 LIKE 'P%' " + "\n");
            strSqlString.Append("                                      AND WORK_DATE BETWEEN '" + start_day + "' AND '" + Lastweek_lastday + "'\n");
            strSqlString.Append("                                   GROUP BY CM_KEY_1, MAT_ID " + "\n");
            strSqlString.Append("                                 ) A," + "\n");
            strSqlString.Append("                                 MWIPMATDEF B " + "\n");
            strSqlString.Append("                           WHERE 1=1  " + "\n");
            strSqlString.Append("                             AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                             AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("                             AND B.MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                             AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
            strSqlString.Append("                          GROUP BY A.FACTORY, A.MAT_ID " + "\n");
            strSqlString.Append("                        ) " + "\n");
            strSqlString.Append("               ) PLAN     " + "\n");
            strSqlString.Append("         WHERE 1 = 1 " + "\n");
            strSqlString.Append("           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND PLAN.PLAN_MONTH(+) = '" + month + "' " + "\n");
            strSqlString.Append("           AND MAT.MAT_TYPE= 'FG' " + "\n");
            strSqlString.Append("           AND MAT.DELETE_FLAG <> 'Y' " + "\n");
            strSqlString.Append("           AND MAT.FACTORY =PLAN.FACTORY(+) " + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = PLAN.MAT_ID(+) " + "\n");
            strSqlString.Append("         GROUP BY MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_GRP_9, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7, MAT.MAT_CMF_13" + "\n");
            strSqlString.Append("       ) PLN" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("         SELECT MAT_ID        " + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_DATE, '" + dayArry1[0] + "', S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1+S4_FAC_IN_QTY_1, 0)) AS RCV0" + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_DATE, '" + dayArry1[1] + "', S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1+S4_FAC_IN_QTY_1, 0)) AS RCV1" + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_DATE, '" + dayArry1[2] + "', S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1+S4_FAC_IN_QTY_1, 0)) AS RCV2" + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_DATE, '" + dayArry1[3] + "', S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1+S4_FAC_IN_QTY_1, 0)) AS RCV3" + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_DATE, '" + dayArry1[4] + "', S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1+S4_FAC_IN_QTY_1, 0)) AS RCV4" + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_MONTH, '" + month + "', DECODE(WORK_DATE, '" + dayArry1[4] + "', 0, S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1+S4_FAC_IN_QTY_1), 0)) AS MON_RCV" + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_MONTH, '" + month + "', DECODE(WORK_DATE, '" + dayArry1[4] + "', 0, S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1+S4_FAC_OUT_QTY_1), 0)) AS MON_SHP" + "\n");
            strSqlString.Append("              , ROUND(SUM(DECODE(WORK_MONTH, '" + month + "', DECODE(WORK_DATE, '" + dayArry1[4] + "', 0, S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1+S4_FAC_IN_QTY_1), 0)) / " + Convert.ToInt32(cdvDate.Value.ToString("dd")) + ", 0) AS MON_RCV_AVG" + "\n");
            strSqlString.Append("           FROM RSUMFACMOV" + "\n");
            strSqlString.Append("          WHERE 1=1" + "\n");
            strSqlString.Append("            AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("            AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("            AND OPER = 'A0000'" + "\n");

            // 2010-09-06-임종우 : CHIP 입고 수량 HYNIX 외 다른 업체는 22시 기준으로..
            strSqlString.Append("            AND MAT_ID NOT LIKE 'HX%'" + "\n");


            // 5일 이후에만 기준월의 시작일을 가지고 사용함.
            if (today.Substring(6, 2) == "01" || today.Substring(6, 2) == "02" || today.Substring(6, 2) == "03" || today.Substring(6, 2) == "04")
            {
                strSqlString.Append("            AND WORK_DATE BETWEEN '" + dayArry1[0] + "' AND '" + dayArry1[4] + "'" + "\n");
            }
            else
            {
                strSqlString.Append("            AND WORK_DATE BETWEEN '" + start_day + "' AND '" + dayArry1[4] + "'" + "\n");
            }

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("            AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("          GROUP BY MAT_ID" + "\n");
            strSqlString.Append("         UNION ALL" + "\n");
            strSqlString.Append("         SELECT MAT_ID        " + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_DATE, '" + dayArry1[0] + "', S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1+S4_FAC_IN_QTY_1, 0)) AS RCV0" + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_DATE, '" + dayArry1[1] + "', S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1+S4_FAC_IN_QTY_1, 0)) AS RCV1" + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_DATE, '" + dayArry1[2] + "', S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1+S4_FAC_IN_QTY_1, 0)) AS RCV2" + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_DATE, '" + dayArry1[3] + "', S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1+S4_FAC_IN_QTY_1, 0)) AS RCV3" + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_DATE, '" + dayArry1[4] + "', S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1+S4_FAC_IN_QTY_1, 0)) AS RCV4" + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_MONTH, '" + month + "', DECODE(WORK_DATE, '" + dayArry1[4] + "', 0, S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1+S4_FAC_IN_QTY_1), 0)) AS MON_RCV" + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_MONTH, '" + month + "', DECODE(WORK_DATE, '" + dayArry1[4] + "', 0, S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1+S4_FAC_OUT_QTY_1), 0)) AS MON_SHP" + "\n");
            strSqlString.Append("              , ROUND(SUM(DECODE(WORK_MONTH, '" + month + "', DECODE(WORK_DATE, '" + dayArry1[4] + "', 0, S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1+S4_FAC_IN_QTY_1), 0)) / " + Convert.ToInt32(cdvDate.Value.ToString("dd")) + ", 0) AS MON_RCV_AVG" + "\n");
            strSqlString.Append("           FROM CSUMFACMOV" + "\n");
            strSqlString.Append("          WHERE 1=1" + "\n");
            strSqlString.Append("            AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("            AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("            AND OPER = 'A0000'" + "\n");

            // 2010-09-06-임종우 : CHIP 입고 수량 HYNIX는 06시 기준으로..
            strSqlString.Append("            AND MAT_ID LIKE 'HX%'" + "\n");

            // 5일 이후에만 기준월의 시작일을 가지고 사용함.
            if (today.Substring(6, 2) == "01" || today.Substring(6, 2) == "02" || today.Substring(6, 2) == "03" || today.Substring(6, 2) == "04")
            {
                strSqlString.Append("            AND WORK_DATE BETWEEN '" + dayArry1[0] + "' AND '" + dayArry1[4] + "'" + "\n");
            }
            else
            {
                strSqlString.Append("            AND WORK_DATE BETWEEN '" + start_day + "' AND '" + dayArry1[4] + "'" + "\n");
            }

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("            AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("          GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) RCV" + "\n");

            // 2011-09-09-임종우 : A/O 는 무조건 22시 기준으로.. (김문한 요청)
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("         SELECT MAT_ID        " + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_MONTH, '" + month + "', DECODE(WORK_DATE, '" + dayArry1[4] + "', 0, S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1+S4_FAC_OUT_QTY_1), 0)) AS MON_SHP" + "\n");
            strSqlString.Append("           FROM RSUMFACMOV" + "\n");
            strSqlString.Append("          WHERE 1=1" + "\n");            
            
            // 5일 이후에만 기준월의 시작일을 가지고 사용함.
            if (today.Substring(6, 2) == "01" || today.Substring(6, 2) == "02" || today.Substring(6, 2) == "03" || today.Substring(6, 2) == "04")
            {
                strSqlString.Append("            AND WORK_DATE BETWEEN '" + dayArry1[0] + "' AND '" + dayArry1[4] + "'" + "\n");
            }
            else
            {
                strSqlString.Append("            AND WORK_DATE BETWEEN '" + start_day + "' AND '" + dayArry1[4] + "'" + "\n");
            }

            strSqlString.Append("            AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("            AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");            

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("            AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("          GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) RCV_SHP" + "\n");

            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("         SELECT MAT_ID" + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_DATE, '" + dayArry1[0] + "', S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1+S2_END_RWK_QTY_1+S3_END_RWK_QTY_1, 0)) AS SHP0" + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_DATE, '" + dayArry1[1] + "', S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1+S2_END_RWK_QTY_1+S3_END_RWK_QTY_1, 0)) AS SHP1" + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_DATE, '" + dayArry1[2] + "', S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1+S2_END_RWK_QTY_1+S3_END_RWK_QTY_1, 0)) AS SHP2" + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_DATE, '" + dayArry1[3] + "', S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1+S2_END_RWK_QTY_1+S3_END_RWK_QTY_1, 0)) AS SHP3" + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_DATE, '" + dayArry1[4] + "', S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1+S2_END_RWK_QTY_1+S3_END_RWK_QTY_1, 0)) AS SHP4" + "\n");
            strSqlString.Append("              , SUM(DECODE(WORK_MONTH, '" + month + "', DECODE(WORK_DATE, '" + dayArry1[4] + "', 0, S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1+S2_END_RWK_QTY_1+S3_END_RWK_QTY_1), 0)) AS MON_SHP" + "\n");
            strSqlString.Append("              , ROUND(SUM(DECODE(WORK_MONTH, '" + month + "', DECODE(WORK_DATE, '" + dayArry1[4] + "', 0, S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1+S2_END_RWK_QTY_1+S3_END_RWK_QTY_1), 0)) / " + Convert.ToInt32(cdvDate.Value.ToString("dd")) + ", 0) AS MON_SHP_AVG" + "\n"); 
            strSqlString.Append("           FROM RSUMWIPMOV" + "\n");
            strSqlString.Append("          WHERE 1=1" + "\n");
            strSqlString.Append("            AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("            AND OPER = 'A0200'" + "\n");

            // 5일 이후에만 기준월의 시작일을 가지고 사용함.
            if (today.Substring(6, 2) == "01" || today.Substring(6, 2) == "02" || today.Substring(6, 2) == "03" || today.Substring(6, 2) == "04")
            {
                strSqlString.Append("            AND WORK_DATE BETWEEN '" + dayArry1[0] + "' AND '" + dayArry1[4] + "'" + "\n");
            }
            else
            {
                strSqlString.Append("            AND WORK_DATE BETWEEN '" + start_day + "' AND '" + dayArry1[4] + "'" + "\n");
            }

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("            AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("          GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) SHP" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("         SELECT MAT_ID, STOCK, LN, BG, LG, SAW, GATE, STOCK+LN+BG+LG+SAW+GATE AS TOTAL" + "\n");
            strSqlString.Append("           FROM (" + "\n");
            strSqlString.Append("                 SELECT LOT.MAT_ID" + "\n");
            strSqlString.Append("                      , SUM(DECODE(LOT.OPER, 'A0000', QTY_1, 0)) AS STOCK" + "\n");
            strSqlString.Append("                      , SUM(DECODE(LOT.OPER, 'A0020', QTY_1, 0)) AS LN" + "\n");
            strSqlString.Append("                      , SUM(CASE WHEN LOT.OPER BETWEEN 'A0040' AND 'A0100' THEN QTY_1" + "\n");
            strSqlString.Append("                                 ELSE 0" + "\n");
            strSqlString.Append("                        END) AS BG" + "\n");
            strSqlString.Append("                      , SUM(DECODE(LOT.OPER, 'A0170', QTY_1, 0)) AS LG" + "\n");
            strSqlString.Append("                      , SUM(DECODE(LOT.OPER, 'A0200', QTY_1, 0)) AS SAW" + "\n");
            //strSqlString.Append("                      , SUM(CASE WHEN LOT.OPER BETWEEN 'A0170' AND 'A0200' THEN QTY_1" + "\n");
            //strSqlString.Append("                                 ELSE 0" + "\n");
            //strSqlString.Append("                        END) AS SAW" + "\n");
            strSqlString.Append("                      , SUM(DECODE(LOT.OPER, 'A0300', QTY_1, 0)) AS GATE" + "\n");

            // 2010-10-18-임종우 : DA 공정(A0300 + OPER_GRP_1이 D/A 인 것) 재공 표시 (김문한K 요청)
            // 2011-01-17-임종우 : DA 공정(A0300 + OPER_GRP_1이 D/A + OPER_GRP_1이 S/P 인 것)
            //strSqlString.Append("                      , SUM(CASE WHEN LOT.OPER = 'A0300' THEN QTY_1" + "\n");
            //strSqlString.Append("                                 WHEN OPR.OPER_GRP_1 = 'D/A' THEN QTY_1" + "\n");
            //strSqlString.Append("                                 WHEN OPR.OPER_GRP_1 = 'S/P' THEN QTY_1" + "\n");
            //strSqlString.Append("                                 ELSE 0" + "\n");
            //strSqlString.Append("                        END) AS DA" + "\n");
            strSqlString.Append("                   FROM RWIPLOTSTS LOT" + "\n");
            strSqlString.Append("                      , MWIPOPRDEF OPR" + "\n");
            strSqlString.Append("                  WHERE 1=1" + "\n");
            strSqlString.Append("                    AND LOT.FACTORY = OPR.FACTORY" + "\n");
            strSqlString.Append("                    AND LOT.OPER = OPR.OPER" + "\n");
            strSqlString.Append("                    AND LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                    AND LOT.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                    AND LOT.LOT_DEL_FLAG = ' '" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                    AND LOT.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                  GROUP BY LOT.MAT_ID" + "\n");
            strSqlString.Append("                )" + "\n");
            strSqlString.Append("          WHERE STOCK+LN+BG+LG+SAW+GATE > 0" + "\n");
            strSqlString.Append("        ) WIP" + "\n");

            //2011-09-08-배수민 WIP_BOH
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("         SELECT MAT_ID, QTY_1 " + "\n");
            strSqlString.Append("           FROM (" + "\n");
            strSqlString.Append("                 SELECT MAT_ID" + "\n");
            strSqlString.Append("                      , SUM(QTY_1) AS QTY_1 " + "\n");
            strSqlString.Append("                   FROM RWIPLOTSTS_BOH " + "\n");
            strSqlString.Append("                  WHERE 1=1" + "\n");
            strSqlString.Append("                    AND CUTOFF_DT = '" + today + "22' " + "\n");
            strSqlString.Append("                    AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                    AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                    AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                    AND OPER > 'A0200'" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                    AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                  GROUP BY MAT_ID" + "\n");
            strSqlString.Append("                )" + "\n");
            strSqlString.Append("          WHERE QTY_1 > 0" + "\n");
            strSqlString.Append("        ) WIP_BOH" + "\n");

            //2011-09-06-배수민 WIP_DA
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("         SELECT MAT_ID, DA_1, DA_2, DA_3, DA_1+DA_2+DA_3 AS TOTAL" + "\n");
            strSqlString.Append("           FROM (" + "\n");
            strSqlString.Append("                 SELECT LOT.MAT_ID" + "\n");
            strSqlString.Append("                      , SUM(DECODE(LOT.OPER, 'A0250', QTY_1, 0)) " + "\n");
            strSqlString.Append("                      + SUM(CASE WHEN LOT.OPER BETWEEN 'A0340' AND 'A0401' THEN QTY_1" + "\n");
            strSqlString.Append("                                 ELSE 0" + "\n");
            strSqlString.Append("                        END) AS DA_1" + "\n");
            strSqlString.Append("                      , SUM(DECODE(LOT.OPER, 'A0402', QTY_1, 0)) + SUM(DECODE(LOT.OPER, 'A0501', QTY_1, 0)) AS DA_2" + "\n");
            strSqlString.Append("                      , SUM(DECODE(LOT.OPER, 'A0403', QTY_1, 0)) + SUM(DECODE(LOT.OPER, 'A0502', QTY_1, 0)) AS DA_3" + "\n");
            strSqlString.Append("                   FROM RWIPLOTSTS LOT" + "\n");
            strSqlString.Append("                      , MWIPOPRDEF OPR" + "\n");
            strSqlString.Append("                  WHERE 1=1" + "\n");
            strSqlString.Append("                    AND LOT.FACTORY = OPR.FACTORY" + "\n");
            strSqlString.Append("                    AND LOT.OPER = OPR.OPER" + "\n");
            strSqlString.Append("                    AND LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                    AND LOT.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                    AND LOT.LOT_DEL_FLAG = ' '" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                    AND LOT.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                  GROUP BY LOT.MAT_ID" + "\n");
            strSqlString.Append("                )" + "\n");
            strSqlString.Append("          WHERE DA_1+DA_2+DA_3 > 0" + "\n");
            strSqlString.Append("        ) WIP_DA" + "\n");

            //2011-09-06-배수민 : WIP_WB 
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("         SELECT MAT_ID, WB_1, WB_2, WB_3, WB_1+WB_2+WB_3 AS TOTAL" + "\n");
            strSqlString.Append("           FROM (" + "\n");
            strSqlString.Append("                 SELECT LOT.MAT_ID" + "\n");
            strSqlString.Append("                      , SUM(DECODE(LOT.OPER, 'A0550', QTY_1, 0)) + SUM(DECODE(LOT.OPER, 'A0551', QTY_1, 0)) " + "\n");
            strSqlString.Append("                      + SUM(DECODE(LOT.OPER, 'A0600', QTY_1, 0)) + SUM(DECODE(LOT.OPER, 'A0601', QTY_1, 0)) " + "\n");
            strSqlString.Append("                      + SUM(DECODE(LOT.OPER, 'A0800', QTY_1, 0)) + SUM(DECODE(LOT.OPER, 'A0801', QTY_1, 0)) AS WB_1 " + "\n");

            strSqlString.Append("                      , SUM(DECODE(LOT.OPER, 'A0552', QTY_1, 0)) + SUM(DECODE(LOT.OPER, 'A0602', QTY_1, 0)) " + "\n");
            strSqlString.Append("                      + SUM(DECODE(LOT.OPER, 'A0802', QTY_1, 0)) AS WB_2 " + "\n");

            strSqlString.Append("                      , SUM(CASE WHEN LOT.OPER BETWEEN 'A0553' AND 'A0559' THEN QTY_1" + "\n");
            strSqlString.Append("                                 ELSE 0" + "\n");
            strSqlString.Append("                        END) " + "\n");
            strSqlString.Append("                      + SUM(CASE WHEN LOT.OPER BETWEEN 'A0603' AND 'A0609' THEN QTY_1" + "\n");
            strSqlString.Append("                                 ELSE 0" + "\n");
            strSqlString.Append("                        END) " + "\n");
            strSqlString.Append("                      + SUM(CASE WHEN LOT.OPER BETWEEN 'A0803' AND 'A0809' THEN QTY_1" + "\n");
            strSqlString.Append("                                 ELSE 0" + "\n");
            strSqlString.Append("                        END) AS WB_3" + "\n");
           
            strSqlString.Append("                   FROM RWIPLOTSTS LOT" + "\n");
            strSqlString.Append("                      , MWIPOPRDEF OPR" + "\n");
            strSqlString.Append("                  WHERE 1=1" + "\n");
            strSqlString.Append("                    AND LOT.FACTORY = OPR.FACTORY" + "\n");
            strSqlString.Append("                    AND LOT.OPER = OPR.OPER" + "\n");
            strSqlString.Append("                    AND LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                    AND LOT.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                    AND LOT.LOT_DEL_FLAG = ' '" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                    AND LOT.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                  GROUP BY LOT.MAT_ID" + "\n");
            strSqlString.Append("                )" + "\n");
            strSqlString.Append("          WHERE WB_1+WB_2+WB_3 > 0" + "\n");
            strSqlString.Append("        ) WIP_WB" + "\n");


            strSqlString.Append("      , (" + "\n");
            strSqlString.Append("          SELECT RES_STS_2 AS MAT_ID, COUNT(*) AS QTY_1" + "\n");
            strSqlString.Append("           FROM MRASRESDEF" + "\n");
            strSqlString.Append("          WHERE 1=1" + "\n");
            strSqlString.Append("            AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("            AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("            AND DELETE_FLAG = ' ' " + "\n");
            //2012-02-01-김민우 : MRASRESSTS의 공정정보 RES_STS_9에서 RES_STS_8로 변경
            strSqlString.Append("            AND RES_STS_8 IN ('A0170', 'A0180',  'A0200')" + "\n");
            strSqlString.Append("          GROUP BY RES_STS_2" + "\n");
            strSqlString.Append("        ) RAS" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND PLN.MAT_ID = RCV.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND PLN.MAT_ID = RCV_SHP.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND PLN.MAT_ID = SHP.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND PLN.MAT_ID = WIP.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND PLN.MAT_ID = WIP_BOH.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND PLN.MAT_ID = RAS.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND PLN.MAT_ID = WIP_DA.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND PLN.MAT_ID = WIP_WB.MAT_ID(+)" + "\n");

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND PLN.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND PLN.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND PLN.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND PLN.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND PLN.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND PLN.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND PLN.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND PLN.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND PLN.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.AppendFormat(" GROUP BY {0}" + "\n", QueryCond1);

            // 2010-09-10-임종우 : 월간 계획 & 월 누적기준 두개의 데이터가 존재 하는 것만 표시 되도록 체크 박스 추가(김문한K 요청)
            if (ckbMon.Checked == true)
            {
                strSqlString.Append(" HAVING NVL(SUM(PLN.MON_PLAN), 0) + NVL(SUM(RCV.MON_RCV), 0) > 0" + "\n");
            }
            else
            {
                strSqlString.Append(" HAVING NVL(SUM(PLN.MON_PLAN), 0) + NVL(SUM(RCV.MON_RCV), 0) + NVL(SUM(RCV.RCV0+RCV.RCV1+RCV.RCV2+RCV.RCV3+RCV.RCV4), 0)" + "\n");
                strSqlString.Append("       + NVL(SUM(SHP.SHP0+SHP.SHP1+SHP.SHP2+SHP.SHP3+SHP.SHP4), 0) + NVL(SUM(SHP.MON_SHP), 0)" + "\n");
                strSqlString.Append("       + NVL(SUM(WIP.TOTAL), 0) + NVL(SUM(RAS.QTY_1), 0) > 0" + "\n");
            }

            strSqlString.AppendFormat(" ORDER BY DECODE(PLN.MAT_GRP_1, 'SE', 1, 'HX', 2, 'AB', 3, 'FC', 4, 'IG', 5, 'IM', 6, 7), {0}" + "\n", QueryCond1);

            //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            //{
            //    System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            //}

            return strSqlString.ToString();
        }

        // 지난 주차의 마지막일 가져오기
        private string MakeSqlString1(string year, string date)
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
                //spdData.RPT_AutoFit(false);

                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 12, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);

                SetAvgVertical2(0, 16, "IN");
                SetAvgVertical2(0, 32, "OUT"); // subTotal에 특정컬럼만 평균값으로 나타내기                 

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
        /// 2010-09-03-임종우 : AVG 구하기.. SubTotal, GrandTotal 구할때 특정 컬럼 내용들로 직접 구할때..        
        /// 
        /// </summary>
        /// <param name="nSampleNormalRowPos"></param>
        /// <param name="nColPos"></param>
        public void SetAvgVertical2(int nSampleNormalRowPos, int nColPos, string type)
        {
            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;
            double iassyMon = 0;
            double itargetMon = 0;

            if (type == "IN")
            {
                iassyMon = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 13].Value);
                itargetMon = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 12].Value);

                // 분모값이 0이 아닐경우에만 계산한다..
                if (itargetMon != 0)
                {
                    spdData.ActiveSheet.Cells[0, nColPos].Value = (iassyMon / itargetMon) * 100;

                    double ijindo_stand = Convert.ToDouble(lblJindo.Text.Replace("%", ""));
                    double ijindo = Convert.ToDouble(spdData.ActiveSheet.Cells[0, nColPos].Value);

                    ijindo = ijindo_stand - ijindo;

                    if (ijindo > 0)
                    {
                        spdData.ActiveSheet.Cells[0, nColPos + 1].Value = ijindo.ToString();
                    }
                    else
                    {
                        spdData.ActiveSheet.Cells[0, nColPos + 1].Value = "0";
                    }

                    for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                    {
                        if (spdData.ActiveSheet.Cells[i, nColPos].BackColor != color)
                        {
                            iassyMon = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 13].Value);
                            itargetMon = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 12].Value);

                            if (itargetMon != 0)
                            {
                                spdData.ActiveSheet.Cells[i, nColPos].Value = iassyMon / itargetMon * 100;

                                double ijindo_sub = Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos].Value);
                                ijindo_sub = ijindo_stand - ijindo_sub;

                                if (ijindo_sub > 0)
                                {
                                    spdData.ActiveSheet.Cells[i, nColPos + 1].Value = ijindo_sub.ToString();
                                }
                                else
                                {
                                    spdData.ActiveSheet.Cells[i, nColPos + 1].Value = "0";
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                iassyMon = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 29].Value);
                itargetMon = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 12].Value);

                // 분모값이 0이 아닐경우에만 계산한다..
                if (itargetMon != 0)
                {
                    spdData.ActiveSheet.Cells[0, nColPos].Value = (iassyMon / itargetMon) * 100;

                    for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                    {
                        if (spdData.ActiveSheet.Cells[i, nColPos].BackColor != color)
                        {
                            iassyMon = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 29].Value);
                            itargetMon = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 12].Value);

                            if (itargetMon != 0)
                            {
                                spdData.ActiveSheet.Cells[i, nColPos].Value = iassyMon / itargetMon * 100;
                            }
                        }
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
            if (spdData.ActiveSheet.Rows.Count > 0)
            {
                string sNowDate = null;

                sNowDate = DateTime.Now.ToString();
                StringBuilder Condition = new StringBuilder();

                Condition.AppendFormat("기준일자: {0}        진도율: {1}        잔여일수: {2}        LOT TYPE : {3}        조회시간 : {4}", lblToday.Text, lblJindo.Text, lblRemain.Text, cdvLotType.Text, sNowDate);
                //Condition.AppendFormat("금일실적기준: {0}    SYSLSI: {1}     월마감: {2}     진도율: {3}    잔여일수: " + lblRemain.Text.ToString() + "    ", lblToday.Text.ToString(), lblSyslsi.Text.ToString(), lblMagam.Text.ToString(), lblJindo.Text.ToString());
                //Condition.Append(" ||   ");
                //Condition.AppendFormat("전일실적기준: {0}    MEMO:   {1}     월마감: {2}     진도율: {3}    잔여일수: " + lblRemain2.Text.ToString(), lblYesterday.Text.ToString(), lblMemo.Text.ToString(), lblMagam2.Text.ToString(), lblJindo2.Text.ToString());

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
            //this.SetFactory(cdvFactory.txtValue);
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }

        /// <summary>
        /// 7. 상단 Lebel 표시
        /// </summary>
        private void LabelTextChange()
        {
            string strTomorrow = cdvDate.Value.AddDays(1).ToString("yyyyMMdd");
            string strToday = cdvDate.Value.ToString("yyyyMMdd");

            string getStartDate = cdvDate.Value.ToString("yyyy-MM") + "-01";
            string strEndday = Convert.ToDateTime(getStartDate).AddMonths(1).AddDays(-1).ToString("yyyyMMdd");

            // 2011-09-15-배수민: strEndday에서도 -3일 (김문한K 요청)
            double jindo = Convert.ToDouble(strToday.Substring(6, 2)) / (Convert.ToDouble(strEndday.Substring(6, 2)) - 3) * 100;

            int remain = Convert.ToInt32(strEndday.Substring(6, 2)) - Convert.ToInt32(strToday.Substring(6, 2));

            // 2011-05-11-임종우 : 실적계산용 잔여일은 당일 포함하기에 당일을 제외 한다.
            iShip_Remain = remain - 3; //2011-09-03-배수민 : -1에서 -3으로 수정 (김문한K 요청)

            // 2010-10-04-임종우 : 해당월의 마지막날은 잔여일을 1로 계산한다.
            if (remain == 0)
            {
                remain = 1;
            }

            if (iShip_Remain <= 0)
            {
                iShip_Remain = 1;
            }

            // 진도율 소수점 1째자리 까지 표시
            decimal jindoPer = Math.Round(Convert.ToDecimal(jindo), 1);

            lblToday.Text = cdvDate.Value.ToString("yyyy-MM-dd");
            lblJindo.Text = jindoPer.ToString() + "%";

            // 2011-09-15-배수민: remain에서도 -3일 (김문한K 요청)
            lblRemain.Text = iShip_Remain.ToString(); //remain.ToString();

            dayArry[0] = cdvDate.Value.AddDays(-3).ToString("MM.dd");
            dayArry[1] = cdvDate.Value.AddDays(-2).ToString("MM.dd");
            dayArry[2] = cdvDate.Value.AddDays(-1).ToString("MM.dd");
            dayArry[3] = cdvDate.Value.ToString("MM.dd");
            dayArry[4] = cdvDate.Value.AddDays(1).ToString("MM.dd");


            dayArry1[0] = cdvDate.Value.AddDays(-3).ToString("yyyyMMdd");
            dayArry1[1] = cdvDate.Value.AddDays(-2).ToString("yyyyMMdd");
            dayArry1[2] = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");
            dayArry1[3] = cdvDate.Value.ToString("yyyyMMdd");
            dayArry1[4] = cdvDate.Value.AddDays(1).ToString("yyyyMMdd");
        }
        #endregion
    }
}
