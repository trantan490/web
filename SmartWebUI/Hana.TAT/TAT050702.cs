using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.UI.Controls;

namespace Hana.TAT
{
    public partial class TAT050702 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        private String stToday = null;
        private String stYesterday = null; 
       
        private Int32 itoday = 0;
        private Int32 ilastday = 0;

        /// <summary>
        /// 클  래  스: TAT050702<br/>
        /// 클래스요약: 생산 목표 (LIPAS)<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2010-07-09<br/>
        /// 상세  설명: TAT 목표 달성을 위한 공정별 Daily OUT 목표를 조회 한다.<br/>
        /// 변경  내용: <br/>
        /// 2010-07-22-임종우 : 상세 POPUP 창에 OUT_TARGET_TIME 추가 (임태성 요청)
        /// 2010-11-04-임종우 : 기존 고객사 기준과 내부 기준으로 나누어 신규로 내부 기준 화면 추가 요청 (김동인 요청)
        /// 2010-11-04-임종우 : 내부 기준 - Hynix 데이터는 06시 기준으로 표시 (김동인 요청)        
        /// 2010-12-17-임종우 : 내부 기준 - 월별 실적 Hynix 데이터는 06시 기준으로 표시 (김동인 요청)
        /// 2010-12-20-임종우 : 내부 기준 - receive, issue, D/A Start 기준 조회 되도록 표시 (임태성 요청)
        /// 2010-12-23-임종우 : 전 고객사 관리함. 목표 TAT의 고객사가 '-' 인 것은 기타 고객사로 처리 함 (임태성 요청)
        /// 2010-12-24-임종우 : 내부 기준 - 월 계획 표시 또는 미표시 (임태성 요청)
        /// 2011-11-21-임종우 : 월 계획 제품 중복 오류 수정
        /// 2012-11-05-임종우 : 재공 기준 DA1~DA4, WB1~WB4로 변경 (김동인 요청)
        /// </summary>
        public TAT050702()
        {
            InitializeComponent();

            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit(); //헤더 한줄짜리 
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1", "MAT.MAT_GRP_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID", "MAT.MAT_ID", true);
        }

        #endregion

        #region 한줄헤더생성

        /// <summary>
        /// 한줄헤더생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {
            try
            {
                string stD0 = cdvDate.Value.ToString("MM.dd");
                string stD1 = cdvDate.Value.AddDays(1).ToString("MM.dd");
                string stD2 = cdvDate.Value.AddDays(2).ToString("MM.dd");

                //spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
                spdData.RPT_ColumnInit();

                // 2010-11-05-임종우 : 고객사 기준 과 내부 기준으로 나눔.
                if (rdbCus.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Lead", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("PIN TYPE", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);

                    spdData.RPT_AddBasicColumn(stD0 + " 현재", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("plan", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("actual", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Difference", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 10, 3);

                    spdData.RPT_AddBasicColumn("3day Out Plan", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn(stD0, 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn(stD1, 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn(stD2, 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 13, 3);

                    if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                    {
                        spdData.RPT_AddBasicColumn("재공현황 (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + " 기준)", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("재공현황 (" + cdvDate.Value.ToString("yy.MM.dd") + " 22:00 기준)", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    }

                    spdData.RPT_AddBasicColumn("STOCK", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("SAW", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("DA", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("WB", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("GATE", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("MOLD", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("FINISH", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("TTL", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 16, 8);

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

                }
                else
                {
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Lead", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("PIN TYPE", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);

                    spdData.RPT_AddBasicColumn(stD0 + " 현재", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("plan", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("actual", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("Difference", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 10, 3);
                                        
                    if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                    {
                        spdData.RPT_AddBasicColumn("재공현황 (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + " 기준)", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("재공현황 (" + cdvDate.Value.ToString("yy.MM.dd") + " 22:00 기준)", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    }

                    spdData.RPT_AddBasicColumn("TTL", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    if (cboGroup.Text == "SAW")
                    {
                        spdData.RPT_AddBasicColumn("HMKA3", 1, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 15, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 16, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 17, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 18, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 19, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 20, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 21, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 22, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 23, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 24, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B4", 1, 25, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A4", 1, 26, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B3", 1, 27, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A3", 1, 28, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B2", 1, 29, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A2", 1, 30, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B1", 1, 31, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 32, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A1", 1, 33, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 34, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("S/P", 1, 35, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                    else if (cboGroup.Text == "D/A")
                    {
                        spdData.RPT_AddBasicColumn("HMKA3", 1, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 15, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 16, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 17, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 18, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 19, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 20, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 21, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 22, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 23, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 24, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B4", 1, 25, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A4", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B3", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A3", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B2", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A2", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B1", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A1", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                        
                        spdData.RPT_AddBasicColumn("S/P", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                    else if (cboGroup.Text == "W/B")
                    {
                        spdData.RPT_AddBasicColumn("HMKA3", 1, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 15, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 16, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 17, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 18, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 19, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 20, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 21, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 22, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 23, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B4", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A4", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B3", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A3", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B2", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A2", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B1", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A1", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("S/P", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                    else if (cboGroup.Text == "MOLD")
                    {
                        spdData.RPT_AddBasicColumn("HMKA3", 1, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 15, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 16, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 17, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 18, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 19, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 20, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 21, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B4", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A4", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B3", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A3", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B2", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A2", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B1", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A1", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                        
                        spdData.RPT_AddBasicColumn("S/P", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("HMKA3", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PVI", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("AVI", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SIG", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SBA", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("PLT", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("TRIM", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MARK", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("CURE", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("GATE", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B4", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A4", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B3", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A3", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B2", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A2", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B1", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A1", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/A", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("S/P", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("SAW", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("B/G", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMKA2", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                                        
                    spdData.RPT_MerageHeaderColumnSpan(0, 13, 26);

                    spdData.RPT_AddBasicColumn("3day Out Plan", 0, 39, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn(stD0, 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn(stD1, 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn(stD2, 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, 39, 3);

                    // 월계획 표시 또는 미표시
                    if (ckbMon.Checked == true)
                    {
                        spdData.RPT_AddBasicColumn("Monthly plan", 0, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("output", 0, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("Progress rate", 0, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                        spdData.RPT_AddBasicColumn("a daily goal", 0, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("W/F over-and-short", 0, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("Monthly plan", 0, 42, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("output", 0, 43, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("Progress rate", 0, 44, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                        spdData.RPT_AddBasicColumn("a daily goal", 0, 45, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("W/F over-and-short", 0, 46, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }

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
                    spdData.RPT_MerageHeaderRowSpan(0, 42, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 43, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 44, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 45, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 46, 2);
                }

                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        #endregion

        #region 조회

        /// <summary>
        /// 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            int iOperStart = 0;
            int iOperEnd = 0;

            if (!CheckField()) return;

            DataTable dt = null;

           
            //PKG 선택한 공정 가져오기
            //sPkgList = udcWIPCondition3.getSelectedItems();

            GridColumnInit();

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                spdData_Sheet1.RowCount = 0;
                this.Refresh();

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }


                ////표구성에따른 항목 Display
                spdData.RPT_ColumnConfigFromTable(btnSort);

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 10, null, null, btnSort);


                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);
                //--------------------------------------------

                //spdData.ActiveSheet.Rows[0].BackColor = Color.LemonChiffon;
                //spdData.ActiveSheet.Rows[1].BackColor = Color.Aqua;
                //spdData.ActiveSheet.Rows[2].BackColor = Color.Aqua;

                // 내부기준 일때만...
                if (rdbHana.Checked == true)
                {
                    // 2010-12-24-임종우 : 진도율 직접 계산하여 표시 (월 계획 표시할때만..)
                    if (ckbMon.Checked == true)
                    {
                        SetAvgVertical(1, 44);
                    }

                    // 공정의 시작 컬럼과 끝나는 컬럼을 찾기 위해...
                    if (cboGroup.Text == "SAW")
                    {
                        iOperStart = 36;
                    }
                    else if (cboGroup.Text == "D/A")
                    {
                        iOperStart = 34;
                    }
                    else if (cboGroup.Text == "W/B")
                    {
                        iOperStart = 25;
                    }
                    else if (cboGroup.Text == "MOLD")
                    {
                        iOperStart = 22;
                    }
                    else
                    {
                        iOperStart = 14;
                    }

                    iOperEnd = 38;

                    // 당일계획에 대한 재공 음영 표시 추가
                    for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                    {
                        int sum = 0;
                        int value = 0;

                        if (spdData.ActiveSheet.Cells[i, 38].BackColor.IsEmpty) // subtotal 부분 제외시키기 위함.
                        {
                            if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, 12].Value) > 0 && Convert.ToInt32(spdData.ActiveSheet.Cells[i, 13].Value) > 0) // 당일 차이 값과 재공Total 이 0 이상인 것만...
                            {
                                for (int y = iOperStart; y <= iOperEnd; y++) // 공정 컬럼번호
                                {
                                    value = Convert.ToInt32(spdData.ActiveSheet.Cells[i, y].Value);
                                    sum += value;

                                    if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, 12].Value) > sum)
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
        #endregion

        /// <summary>
        /// 2010-12-15-임종우
        /// AVG 구하기.. SubTotal, GrandTotal 구할때 특정 컬럼 내용들로 직접 구할때..
        /// </summary>
        /// <param name="nSampleNormalRowPos"></param>
        /// <param name="nColPos"></param>
        public void SetAvgVertical(int nSampleNormalRowPos, int nColPos)
        {
            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;
            double iassyMon = 0;
            double itargetMon = 0;
            double dayPer = Convert.ToDouble(itoday) / Convert.ToDouble(ilastday);

            
            iassyMon = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 43].Value);
            itargetMon = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 42].Value);

            if (itargetMon > 0)
            {
                double jindo = Math.Round((iassyMon / itargetMon) / (dayPer) * 100, 2);


                spdData.ActiveSheet.Cells[0, nColPos].Value = jindo;

                for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    if (spdData.ActiveSheet.Cells[i, nColPos].BackColor != color)
                    {
                        iassyMon = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 43].Value);
                        itargetMon = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 42].Value);

                        if (itargetMon > 0)
                        {
                            spdData.ActiveSheet.Cells[i, nColPos].Value = Math.Round((iassyMon / itargetMon) / (dayPer) * 100, 2);
                        }
                    }
                }
            }

        }

        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            //if (udcWIPCondition1.Text == "ALL" || udcWIPCondition1.Text == "")
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD033", GlobalVariable.gcLanguage));
            //    return false;
            //}

            return true;
        }

        #endregion

        #region MakeSqlString
        private string MakeSqlString()
        {
            string QueryCond1 = null;
            string QueryCond2 = null;

            string strDecode = string.Empty;
            string strDecode2 = string.Empty;
            string year = string.Empty;
            string month = string.Empty;
            string start_date = string.Empty;            
            
            DataTable dt1 = null;
            DataTable dt2 = null;
            StringBuilder strSqlString = new StringBuilder();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            //------ 현재 조회일과 전일 가져오기 -----------
            stToday = Convert.ToDateTime(cdvDate.Text).ToString("yyyyMMdd");
            stYesterday = Convert.ToDateTime(cdvDate.Text).AddDays(-1).ToString("yyyyMMdd");

            year = cdvDate.Value.ToString("yyyy");
            month = cdvDate.Value.ToString("yyyyMM");
            start_date = month + "01";

            // 달의 마지막일 구하기            
            string last_day = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + month + "', 'YYYYMM')),'YYYYMMDD') FROM DUAL)";
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", last_day);
            last_day = dt1.Rows[0][0].ToString();
            
            itoday = Convert.ToInt32(cdvDate.Value.ToString("dd"));
            ilastday = Convert.ToInt32(last_day.Substring(6, 2));
            int idefday = ilastday - itoday + 1; // 조회일 포함하여 잔여일 계산 함.
            
            // 지난주차의 마지막일 가져오기            
            dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(year, stToday));
            string Lastweek_lastday = dt2.Rows[0][0].ToString();

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond1);

            #region 고객사 기준
            if (rdbCus.Checked == true)
            {
                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    strSqlString.Append("     , SUM(NVL(SHP.END_QTY_1,0)) + SUM(NVL(PLN.D0,0))" + "\n");
                    strSqlString.Append("     , SUM(NVL(SHP.END_QTY_1,0)) AS END_QTY" + "\n");
                    strSqlString.Append("     , (SUM(NVL(SHP.END_QTY_1,0)) + SUM(NVL(PLN.D0,0))) - SUM(NVL(SHP.END_QTY_1,0)) AS DEF_QTY" + "\n");
                    strSqlString.Append("     , SUM(NVL(SHP.END_QTY_1,0)) + SUM(NVL(PLN.D0,0)) AS D0" + "\n");
                }
                else
                {
                    strSqlString.Append("     , SUM(NVL(PLN.D0,0))" + "\n");
                    strSqlString.Append("     , SUM(NVL(SHP.END_QTY_1,0)) AS END_QTY" + "\n");
                    strSqlString.Append("     , SUM(NVL(PLN.D0,0)) - SUM(NVL(SHP.END_QTY_1,0)) AS DEF_QTY" + "\n");
                    strSqlString.Append("     , SUM(NVL(PLN.D0,0)) AS D0" + "\n");
                }
                            
                strSqlString.Append("     , SUM(PLN.D1)" + "\n");
                strSqlString.Append("     , SUM(PLN.D2)" + "\n");
                strSqlString.Append("     , SUM(WIP.STOCK)" + "\n");
                strSqlString.Append("     , SUM(WIP.SAW)" + "\n");
                strSqlString.Append("     , SUM(WIP.DA)" + "\n");
                strSqlString.Append("     , SUM(WIP.WB)" + "\n");
                strSqlString.Append("     , SUM(WIP.GATE)" + "\n");
                strSqlString.Append("     , SUM(WIP.MOLD)" + "\n");
                strSqlString.Append("     , SUM(WIP.FINISH)" + "\n");
                strSqlString.Append("     , SUM(WIP.TTL)" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT MAT_GRP_1" + "\n");
                strSqlString.Append("             , MAT_GRP_3" + "\n");
                strSqlString.Append("             , MAT_ID" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN DATA_2 <= T0 THEN QTY_1" + "\n");
                strSqlString.Append("                        ELSE 0" + "\n");
                strSqlString.Append("                   END) D0" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN T0 < DATA_2 AND DATA_2 <= T1 THEN QTY_1" + "\n");
                strSqlString.Append("                        ELSE 0" + "\n");
                strSqlString.Append("                   END) D1" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN T1 < DATA_2 AND DATA_2 <= T2 THEN QTY_1" + "\n");
                strSqlString.Append("                        ELSE 0" + "\n");
                strSqlString.Append("                   END) D2" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT A.MAT_GRP_1" + "\n");
                strSqlString.Append("                     , A.MAT_GRP_3" + "\n");
                strSqlString.Append("                     , A.MAT_ID" + "\n");
                strSqlString.Append("                     , A.LOT_ID" + "\n");
                strSqlString.Append("                     , A.OPER" + "\n");
                strSqlString.Append("                     , A.QTY_1" + "\n");
                strSqlString.Append("                     , A.DATA_2" + "\n");
                strSqlString.Append("                     , B.OPER_GRP_1" + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    strSqlString.Append("                     , (SYSDATE - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 1) - (SYSDATE - TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS')) AS T0" + "\n");
                    strSqlString.Append("                     , (SYSDATE - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 2) - (SYSDATE - TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS')) AS T1" + "\n");
                    strSqlString.Append("                     , (SYSDATE - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 3) - (SYSDATE - TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS')) AS T2" + "\n");                    
                }
                else
                {
                    strSqlString.Append("                     , TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS') - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 1 AS T0" + "\n");
                    strSqlString.Append("                     , TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS') - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 2 AS T1" + "\n");
                    strSqlString.Append("                     , TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS') - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 3 AS T2" + "\n");                    
                }

                strSqlString.Append("                  FROM ( " + "\n");
                strSqlString.Append("                        SELECT A.LOT_ID " + "\n");
                strSqlString.Append("                             , A.FACTORY " + "\n");
                strSqlString.Append("                             , A.MAT_ID " + "\n");
                strSqlString.Append("                             , A.OPER " + "\n");
                strSqlString.Append("                             , A.QTY_1 " + "\n");
                strSqlString.Append("                             , A.LOT_CMF_5 " + "\n");
                strSqlString.Append("                             , A.LOT_CMF_14 " + "\n");
                strSqlString.Append("                             , A.RESV_FIELD_1 " + "\n");
                strSqlString.Append("                             , A.MAT_GRP_1 " + "\n");
                strSqlString.Append("                             , A.MAT_GRP_3 " + "\n");
                strSqlString.Append("                             , CASE WHEN NVL(B.DATA_2, 0) <> 0 THEN B.DATA_2  " + "\n");
                strSqlString.Append("                                    ELSE C.DATA_2 " + "\n");
                strSqlString.Append("                               END AS DATA_2 " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                SELECT STS.LOT_ID " + "\n");
                strSqlString.Append("                                     , STS.FACTORY " + "\n");
                strSqlString.Append("                                     , STS.MAT_ID " + "\n");
                strSqlString.Append("                                     , STS.OPER " + "\n");
                strSqlString.Append("                                     , STS.QTY_1 " + "\n");
                strSqlString.Append("                                     , STS.LOT_CMF_5 " + "\n");
                strSqlString.Append("                                     , STS.LOT_CMF_14 " + "\n");
                strSqlString.Append("                                     , STS.RESV_FIELD_1 " + "\n");
                strSqlString.Append("                                     , MAT.MAT_GRP_1 " + "\n");
                strSqlString.Append("                                     , MAT.MAT_GRP_3 " + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    strSqlString.Append("                                  FROM RWIPLOTSTS STS" + "\n");
                }
                else
                {
                    strSqlString.Append("                                  FROM RWIPLOTSTS_BOH STS" + "\n");
                }

                strSqlString.Append("                                     , (" + "\n");
                strSqlString.Append("                                        SELECT FACTORY " + "\n");
                strSqlString.Append("                                             , MAT_ID " + "\n");
                strSqlString.Append("                                             , MAT_GRP_1 " + "\n");
                strSqlString.Append("                                             , MAT_GRP_3 " + "\n");
                strSqlString.Append("                                          FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                       ) MAT " + "\n");
                strSqlString.Append("                                 WHERE STS.FACTORY = MAT.FACTORY" + "\n");
                strSqlString.Append("                                   AND STS.MAT_ID = MAT.MAT_ID" + "\n");
                strSqlString.Append("                                   AND STS.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                                   AND STS.LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                                   AND STS.LOT_DEL_FLAG = ' '" + "\n");

                if (cbLotType.Text != "ALL")
                {
                    strSqlString.Append("                                   AND STS.LOT_CMF_5 LIKE '" + cbLotType.Text + "'" + "\n");
                }  

                // 과거 일자 이면 BOH 테이블 조회
                if (DateTime.Now.ToString("yyyyMMdd") != cdvDate.SelectedValue())
                {
                    strSqlString.Append("                                   AND CUTOFF_DT = '" + stYesterday + "22'" + "\n");
                }

                strSqlString.Append("                               ) A " + "\n");

                // 고객사가 존재하는 TAT 값 가져오기
                strSqlString.Append("                             , (" + "\n");
                strSqlString.Append("                                SELECT FACTORY, KEY_2, KEY_3, SUM(TO_NUMBER(DATA_2)) AS DATA_2" + "\n");
                strSqlString.Append("                                  FROM MGCMTBLDAT" + "\n");
                strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                   AND TABLE_NAME = 'H_RPT_TAT_MAINOBJECT'" + "\n");
                strSqlString.Append("                                   AND KEY_1 <= '" + stToday + "'" + "\n");
                strSqlString.Append("                                   AND DATA_1 >= '" + stToday + "'" + "\n");
                strSqlString.Append("                                   AND KEY_2 <> '-' " + "\n");

                // 선택한 공정그룹까지의 TAT 목표 SUM 값을 가져오기 위해..
                if (cboGroup.Text == "SAW")
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW')" + "\n");
                }
                else if (cboGroup.Text == "D/A")
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW', 'D/A')" + "\n");
                }
                else if (cboGroup.Text == "W/B")
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B')" + "\n");
                }
                else if (cboGroup.Text == "GATE")
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B', 'GATE')" + "\n");
                }
                else if (cboGroup.Text == "MOLD")
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B', 'GATE', 'MOLD')" + "\n");
                }
                else
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B', 'GATE', 'MOLD', 'FINISH')" + "\n");
                }

                strSqlString.Append("                                 GROUP BY FACTORY, KEY_2, KEY_3" + "\n");
                strSqlString.Append("                               ) B " + "\n");

                // 고객사 없는 공통 TAT 목표 값 가져오기
                strSqlString.Append("                             , (" + "\n");
                strSqlString.Append("                                SELECT FACTORY, KEY_2, KEY_3, SUM(TO_NUMBER(DATA_2)) AS DATA_2" + "\n");
                strSqlString.Append("                                  FROM MGCMTBLDAT" + "\n");
                strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                   AND TABLE_NAME = 'H_RPT_TAT_MAINOBJECT'" + "\n");
                strSqlString.Append("                                   AND KEY_1 <= '" + stToday + "'" + "\n");
                strSqlString.Append("                                   AND DATA_1 >= '" + stToday + "'" + "\n");
                strSqlString.Append("                                   AND KEY_2 = '-' " + "\n");

                // 선택한 공정그룹까지의 TAT 목표 SUM 값을 가져오기 위해..
                if (cboGroup.Text == "SAW")
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW')" + "\n");
                }
                else if (cboGroup.Text == "D/A")
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW', 'D/A')" + "\n");
                }
                else if (cboGroup.Text == "W/B")
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B')" + "\n");
                }
                else if (cboGroup.Text == "GATE")
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B', 'GATE')" + "\n");
                }
                else if (cboGroup.Text == "MOLD")
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B', 'GATE', 'MOLD')" + "\n");
                }
                else
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B', 'GATE', 'MOLD', 'FINISH')" + "\n");
                }

                strSqlString.Append("                                 GROUP BY FACTORY, KEY_2, KEY_3" + "\n");
                strSqlString.Append("                               ) C " + "\n");
                strSqlString.Append("                         WHERE A.FACTORY = B.FACTORY(+) " + "\n");
                strSqlString.Append("                           AND A.MAT_GRP_1 = B.KEY_2(+) " + "\n");
                strSqlString.Append("                           AND A.MAT_GRP_3 = B.KEY_3(+) " + "\n");
                strSqlString.Append("                           AND A.FACTORY = C.FACTORY(+)" + "\n");
                strSqlString.Append("                           AND A.MAT_GRP_3 = C.KEY_3(+)" + "\n");
                strSqlString.Append("                       ) A " + "\n");
                strSqlString.Append("                     , MWIPOPRDEF B" + "\n");
                strSqlString.Append("                 WHERE A.FACTORY = B.FACTORY" + "\n");
                strSqlString.Append("                   AND A.OPER = B.OPER" + "\n");
                strSqlString.Append("                   AND A.DATA_2 > 0" + "\n");                              
                
                if (cboGroup.Text == "SAW")
                {
                    strSqlString.Append("                   AND B.OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'S/P')" + "\n");
                }
                else if (cboGroup.Text == "D/A")
                {
                    strSqlString.Append("                   AND B.OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'S/P', 'D/A')" + "\n");
                }
                else if (cboGroup.Text == "W/B")
                {
                    strSqlString.Append("                   AND B.OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'S/P', 'D/A', 'W/B')" + "\n");
                }
                //else if (cboGroup.Text == "GATE")
                //{
                //    strSqlString.Append("                   AND OPR.OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'S/P', 'D/A', 'W/B', 'GATE')" + "\n");
                //}
                else if (cboGroup.Text == "MOLD")
                {
                    strSqlString.Append("                   AND B.OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'S/P', 'D/A', 'W/B', 'GATE', 'MOLD','CURE')" + "\n");
                }
                else
                {
                    strSqlString.Append("                   AND B.OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'S/P', 'D/A', 'W/B', 'GATE', 'MOLD','CURE', 'M/K','TRIM','TIN','S/B/A','SIG','AVI','V/I','HMK3A')" + "\n");
                }
                
                strSqlString.Append("               )" + "\n");
                strSqlString.Append("         GROUP BY MAT_GRP_1, MAT_GRP_3, MAT_ID" + "\n");
                strSqlString.Append("       ) PLN" + "\n");
                strSqlString.Append("     , (" + "\n");
                strSqlString.Append("        SELECT A.MAT_ID " + "\n");
                strSqlString.Append("             , SUM(NVL(A.V0,0)) AS STOCK " + "\n");
                strSqlString.Append("             , SUM(NVL(A.V1+A.V2+A.V3,0)) AS SAW " + "\n");
                strSqlString.Append("             , SUM(NVL(A.V4,0)) AS DA " + "\n");
                strSqlString.Append("             , SUM(NVL(A.V5,0)) AS WB " + "\n");
                strSqlString.Append("             , SUM(NVL(A.V16,0)) AS GATE " + "\n");
                strSqlString.Append("             , SUM(NVL(A.V6+A.V7,0)) AS MOLD " + "\n");
                strSqlString.Append("             , SUM(NVL(A.V8+A.V9+A.V10+A.V11+A.V12+A.V13+A.V14+A.V15,0)) AS FINISH " + "\n");
                strSqlString.Append("             , SUM(NVL(A.V0+A.V1+A.V2+A.V3+A.V4+A.V5+A.V6+A.V7+A.V8+A.V9+A.V10+A.V11+A.V12+A.V13+A.V14+A.V15+A.V16,0)) AS TTL " + "\n");
                strSqlString.Append("          FROM ( " + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'HMK2A', QTY, 0)) V0 " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'B/G', QTY, 0)) V1 " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'SAW', QTY, 0)) V2 " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'S/P', QTY, 0)) V3 " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'D/A', QTY, 0)) V4 " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'W/B', QTY, 0)) V5 " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'MOLD', QTY, 0)) V6 " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'CURE', QTY, 0)) V7 " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'M/K', QTY, 0)) V8 " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'TRIM', QTY, 0)) V9 " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'TIN', QTY, 0)) V10" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'S/B/A', QTY, 0)) V11 " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'SIG', QTY, 0)) V12 " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'AVI', QTY, 0)) V13 " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'V/I', QTY, 0)) V14 " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'HMK3A', QTY, 0)) V15  " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'GATE', QTY, 0)) V16" + "\n");
                strSqlString.Append("                  FROM (  " + "\n");
                strSqlString.Append("                        SELECT A.FACTORY, A.MAT_ID, B.OPER_GRP_1 " + "\n");
                strSqlString.Append("                             , SUM(A.QTY_1) QTY  " + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    strSqlString.Append("                          FROM RWIPLOTSTS A  " + "\n");
                    strSqlString.Append("                             , MWIPOPRDEF B  " + "\n");
                    strSqlString.Append("                         WHERE 1 = 1                      " + "\n");
                }
                else
                {
                    strSqlString.Append("                          FROM RWIPLOTSTS_BOH A  " + "\n");
                    strSqlString.Append("                             , MWIPOPRDEF B  " + "\n");
                    strSqlString.Append("                         WHERE 1 = 1                      " + "\n");
                    strSqlString.Append("                           AND A.CUTOFF_DT = '" + stToday + "22' " + "\n");
                }

                strSqlString.Append("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                           AND A.LOT_TYPE = 'W'  " + "\n");
                strSqlString.Append("                           AND A.LOT_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("                           AND B.OPER_GRP_1 <> '-'" + "\n");

                //2011-01-19-임종우 : A2100(PVI)공정 -> A2050(PVI) 변경 됨으로 인해 수정 함.
                strSqlString.Append("                           AND A.OPER NOT IN ('A000N')" + "\n");
                //strSqlString.Append("                           AND A.OPER IN ('A0000', 'A000N', 'A0020', 'A0040', 'A0100', 'A0150', 'A0170', 'A0180', 'A0200', 'A0250', 'A0270', 'A0300', 'A0320', 'A0330', 'A0340', 'A0350', 'A0360', 'A0370', 'A0380', 'A0390', 'A0400', 'A0401', 'A0500', 'A0501', 'A0540', 'A0550', 'A0551', 'A0600', 'A0620', 'A0630', 'A0650', 'A0670', 'A0800', 'A0402', 'A0502', 'A0601', 'A0801', 'A0552', 'A0602', 'A0802', 'A0403', 'A0503', 'A0553', 'A0603', 'A0803', 'A0404', 'A0504', 'A0554', 'A0604', 'A0804', 'A0405', 'A0505', 'A0555', 'A0605', 'A0805', 'A0406', 'A0506', 'A0556', 'A0606', 'A0806', 'A0407', 'A0507', 'A0557', 'A0607', 'A0807', 'A0408', 'A0508', 'A0558', 'A0608', 'A0808', 'A0409', 'A0509', 'A0559', 'A0609', 'A0809', 'A0950', 'A0970', 'A1000', 'A1070', 'A1100', 'A1110', 'A1120', 'A1130', 'A1150', 'A1600', 'A1650', 'A1170', 'A1180', 'A1200', 'A1230', 'A1250', 'A1260', 'A1300', 'A1350', 'A1450', 'A1470', 'A1500', 'A1720', 'A1750', 'A1770', 'A1800', 'A1900', 'A1950', 'A2000', 'A2100', 'A2300', 'AZ009', 'AZ010')" + "\n");
                strSqlString.Append("                           AND A.FACTORY = B.FACTORY(+)  " + "\n");
                strSqlString.Append("                           AND A.OPER = B.OPER(+)  " + "\n");

                if (cbLotType.Text != "ALL")
                {
                    strSqlString.Append("                           AND A.LOT_CMF_5 LIKE '" + cbLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                         GROUP BY A.FACTORY, A.MAT_ID, B.OPER_GRP_1 " + "\n");
                strSqlString.Append("                       ) " + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) A" + "\n");
                strSqlString.Append("         WHERE 1 = 1 " + "\n");
                strSqlString.Append("         GROUP BY A.MAT_ID" + "\n");
                strSqlString.Append("       ) WIP" + "\n");
                strSqlString.Append("     , (" + "\n");
                strSqlString.Append("        SELECT MAT_ID" + "\n");
                strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + stToday + "', (CASE WHEN OPER IN ('AZ010','SHIP','TZ010','F0000','EZ010', 'F0000') THEN SHIP_QTY_1 ELSE END_QTY_1 END),0)) END_QTY_1" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3" + "\n");
                strSqlString.Append("                     , SUM(END_QTY_1) AS END_QTY_1" + "\n");
                strSqlString.Append("                     , SUM(SHIP_QTY_1) AS SHIP_QTY_1" + "\n");
                strSqlString.Append("                  FROM (" + "\n");
                strSqlString.Append("                        SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3" + "\n");
                strSqlString.Append("                             , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_1+S2_OPER_IN_QTY_1+S3_OPER_IN_QTY_1),SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1)) END_QTY_1" + "\n");
                strSqlString.Append("                             , 0 SHIP_QTY_1" + "\n");
                strSqlString.Append("                          FROM RSUMWIPMOV " + "\n");
                strSqlString.Append("                         WHERE WORK_DATE = '" + stToday + "'" + "\n");
                strSqlString.Append("                           AND OPER NOT IN ('AZ010','TZ010','F0000','EZ010', 'F0000')" + "\n");

                if (cbLotType.Text != "ALL")
                {
                    strSqlString.Append("                           AND CM_KEY_3 LIKE '" + cbLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                         GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3" + "\n");
                strSqlString.Append("                         UNION ALL" + "\n");
                strSqlString.Append("                        SELECT CM_KEY_1 AS FACTORY, MAT_ID" + "\n");
                strSqlString.Append("                             , DECODE(CM_KEY_1,'" + GlobalVariable.gsAssyDefaultFactory + "','AZ010','" + GlobalVariable.gsTestDefaultFactory + "','TZ010','FGS','F0000','HMKE1','EZ010') OPER" + "\n");
                strSqlString.Append("                             , LOT_TYPE, MAT_VER,  WORK_DATE,CM_KEY_3" + "\n");
                strSqlString.Append("                             , 0 END_QTY_1" + "\n");
                strSqlString.Append("                             , SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) SHIP_QTY_1" + "\n");
                strSqlString.Append("                          FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                         WHERE FACTORY NOT IN ('RETURN')" + "\n");
                strSqlString.Append("                           AND WORK_DATE = '" + stToday + "'" + "\n");

                if (cbLotType.Text != "ALL")
                {
                    strSqlString.Append("                           AND CM_KEY_3 LIKE '" + cbLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                         GROUP BY CM_KEY_1, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3" + "\n");
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("                 GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3" + "\n");
                strSqlString.Append("               ) " + "\n");
                strSqlString.Append("        WHERE 1=1 " + "\n");
                strSqlString.Append("          AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'              " + "\n");
                strSqlString.Append("          AND MAT_VER = 1 " + "\n");

                // 선택한 공정그룹의 대표공정을 가지고 실적을 가져옴.
                if (cboGroup.Text == "SAW")
                {
                    strSqlString.Append("          AND OPER = 'A0300' " + "\n");
                }
                else if (cboGroup.Text == "D/A")
                {
                    strSqlString.Append("          AND OPER IN ('A0400','A0402')" + "\n");
                }
                else if (cboGroup.Text == "W/B")
                {
                    strSqlString.Append("          AND OPER IN ('A0600','A0602')" + "\n");
                }
                //else if (cboGroup.Text == "GATE")
                //{
                //    strSqlString.Append("          AND OPER IN ('A0800','A0802')" + "\n");
                //}
                else if (cboGroup.Text == "MOLD")
                {
                    strSqlString.Append("          AND OPER IN ('A1100','A1180','A1230')" + "\n");
                }
                else
                {
                    strSqlString.Append("          AND OPER IN ('AZ010')" + "\n");
                }

                strSqlString.Append("          AND OPER NOT IN ('00001','00002') " + "\n");
                strSqlString.Append("        GROUP BY MAT_ID" + "\n");
                strSqlString.Append("       ) SHP" + "\n");                
                strSqlString.Append("       , MWIPMATDEF MAT" + "\n");
                strSqlString.Append(" WHERE 1=1" + "\n");
                strSqlString.Append("   AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
                strSqlString.Append("   AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");
                strSqlString.Append("   AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
                strSqlString.Append("   AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");


                #region 상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                #endregion

                strSqlString.AppendFormat(" GROUP BY {0}" + "\n", QueryCond2);
                strSqlString.Append(" HAVING ( " + "\n");
                strSqlString.Append("         NVL(SUM(SHP.END_QTY_1),0) + NVL(SUM(PLN.D0),0) + NVL(SUM(PLN.D1),0) + NVL(SUM(PLN.D2),0) + NVL(SUM(WIP.STOCK),0) +  " + "\n");
                strSqlString.Append("         NVL(SUM(WIP.SAW),0) + NVL(SUM(WIP.DA),0) + NVL(SUM(WIP.WB),0) + NVL(SUM(WIP.GATE),0) + NVL(SUM(WIP.MOLD),0) + NVL(SUM(WIP.FINISH),0) + NVL(SUM(WIP.TTL),0) " + "\n");
                strSqlString.Append("        ) <> 0 " + "\n");
                strSqlString.AppendFormat(" ORDER BY {0}" + "\n", QueryCond2);
            }
            #endregion
            #region 내부기준
            else
            {
                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    strSqlString.Append("     , SUM(NVL(SHP.END_QTY_1,0)) + SUM(NVL(PLN.D0,0))" + "\n");
                    strSqlString.Append("     , SUM(NVL(SHP.END_QTY_1,0)) AS END_QTY" + "\n");
                    strSqlString.Append("     , (SUM(NVL(SHP.END_QTY_1,0)) + SUM(NVL(PLN.D0,0))) - SUM(NVL(SHP.END_QTY_1,0)) AS DEF_QTY" + "\n");                    
                }
                else
                {
                    strSqlString.Append("     , SUM(NVL(PLN.D0,0))" + "\n");
                    strSqlString.Append("     , SUM(NVL(SHP.END_QTY_1,0)) AS END_QTY" + "\n");
                    strSqlString.Append("     , SUM(NVL(PLN.D0,0)) - SUM(NVL(SHP.END_QTY_1,0)) AS DEF_QTY" + "\n");                    
                }

                strSqlString.Append("     , SUM(WIP.WIP_SUM)" + "\n");                
                strSqlString.Append("     , SUM(WIP.HMK3A)" + "\n");
                strSqlString.Append("     , SUM(WIP.VI)" + "\n");
                strSqlString.Append("     , SUM(WIP.AVI)" + "\n");
                strSqlString.Append("     , SUM(WIP.SIG)" + "\n");
                strSqlString.Append("     , SUM(WIP.SBA)" + "\n");
                strSqlString.Append("     , SUM(WIP.TIN)" + "\n");
                strSqlString.Append("     , SUM(WIP.TRIM)" + "\n");
                strSqlString.Append("     , SUM(WIP.MK)" + "\n");
                strSqlString.Append("     , SUM(WIP.CURE)" + "\n");
                strSqlString.Append("     , SUM(WIP.MOLD)" + "\n");
                strSqlString.Append("     , SUM(WIP.GATE)" + "\n");
                strSqlString.Append("     , SUM(WIP.WB4)" + "\n");
                strSqlString.Append("     , SUM(WIP.DA4)" + "\n");
                strSqlString.Append("     , SUM(WIP.WB3)" + "\n");
                strSqlString.Append("     , SUM(WIP.DA3)" + "\n");
                strSqlString.Append("     , SUM(WIP.WB2)" + "\n");
                strSqlString.Append("     , SUM(WIP.DA2)" + "\n");
                strSqlString.Append("     , SUM(WIP.WB1)" + "\n");
                strSqlString.Append("     , SUM(WIP.WB)" + "\n");
                strSqlString.Append("     , SUM(WIP.DA1)" + "\n");
                strSqlString.Append("     , SUM(WIP.DA)" + "\n");
                strSqlString.Append("     , SUM(WIP.SP)" + "\n");
                strSqlString.Append("     , SUM(WIP.SAW)" + "\n");
                strSqlString.Append("     , SUM(WIP.BG)" + "\n");
                strSqlString.Append("     , SUM(WIP.HMK2A)" + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    strSqlString.Append("     , SUM(NVL(SHP.END_QTY_1,0)) + SUM(NVL(PLN.D0,0)) AS D0" + "\n");
                }
                else
                {
                    strSqlString.Append("     , SUM(NVL(PLN.D0,0)) AS D0" + "\n");
                }

                strSqlString.Append("     , SUM(PLN.D1)" + "\n");
                strSqlString.Append("     , SUM(PLN.D2)" + "\n");
                strSqlString.Append("     , SUM(ETC.MON_PLAN)" + "\n");
                strSqlString.Append("     , SUM(ETC.SHIP_MON)" + "\n");                
                strSqlString.Append("     , NVL(DECODE( SUM(ETC.MON_PLAN), 0, 0, ROUND((SUM(ETC.SHIP_MON)/SUM(ETC.MON_PLAN)) / (" + itoday + "/" + ilastday + ") * 100, 2)),0) AS JINDO" + "\n");
                strSqlString.Append("     , ROUND(SUM(ETC.TARGET) / " + idefday + " , 0)" + "\n");
                strSqlString.Append("     , SUM(NVL(ETC.MON_PLAN, 0) - NVL(ETC.SHIP_MON,0) - NVL(WIP.WIP_TTL,0))" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT MAT_GRP_1" + "\n");
                strSqlString.Append("             , MAT_GRP_3" + "\n");
                strSqlString.Append("             , MAT_ID" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN DATA_2 <= T0 THEN QTY_1" + "\n");
                strSqlString.Append("                        ELSE 0" + "\n");
                strSqlString.Append("                   END) D0" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN T0 < DATA_2 AND DATA_2 <= T1 THEN QTY_1" + "\n");
                strSqlString.Append("                        ELSE 0" + "\n");
                strSqlString.Append("                   END) D1" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN T1 < DATA_2 AND DATA_2 <= T2 THEN QTY_1" + "\n");
                strSqlString.Append("                        ELSE 0" + "\n");
                strSqlString.Append("                   END) D2" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT A.MAT_GRP_1" + "\n");
                strSqlString.Append("                     , A.MAT_GRP_3" + "\n");
                strSqlString.Append("                     , A.MAT_ID" + "\n");
                strSqlString.Append("                     , A.LOT_ID" + "\n");
                strSqlString.Append("                     , A.OPER" + "\n");
                strSqlString.Append("                     , A.QTY_1" + "\n");
                strSqlString.Append("                     , A.DATA_2" + "\n");
                strSqlString.Append("                     , B.OPER_GRP_1" + "\n");

                // 내부기준이며 Issue 기준 일때..
                //if (rdbHana.Checked == true && cboType.Text == "Issue 기준")
                if (rdbHana.Checked == true && cboType.SelectedIndex == 1)
                {
                    if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                    {
                        strSqlString.Append("                     , (SYSDATE - TO_DATE(A.RESV_FIELD_1, 'YYYYMMDDHH24MISS') + 1) - DECODE(A.MAT_GRP_1, 'HX', (SYSDATE - TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS')), (SYSDATE - TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS'))) AS T0" + "\n");
                        strSqlString.Append("                     , (SYSDATE - TO_DATE(A.RESV_FIELD_1, 'YYYYMMDDHH24MISS') + 2) - DECODE(A.MAT_GRP_1, 'HX', (SYSDATE - TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS')), (SYSDATE - TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS'))) AS T1" + "\n");
                        strSqlString.Append("                     , (SYSDATE - TO_DATE(A.RESV_FIELD_1, 'YYYYMMDDHH24MISS') + 3) - DECODE(A.MAT_GRP_1, 'HX', (SYSDATE - TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS')), (SYSDATE - TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS'))) AS T2" + "\n");                        
                    }
                    else
                    {
                        strSqlString.Append("                     , DECODE(A.MAT_GRP_1, 'HX', TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS'), TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(A.RESV_FIELD_1, 'YYYYMMDDHH24MISS') + 1 AS T0" + "\n");
                        strSqlString.Append("                     , DECODE(A.MAT_GRP_1, 'HX', TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS'), TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(A.RESV_FIELD_1, 'YYYYMMDDHH24MISS') + 2 AS T1" + "\n");
                        strSqlString.Append("                     , DECODE(A.MAT_GRP_1, 'HX', TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS'), TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(A.RESV_FIELD_1, 'YYYYMMDDHH24MISS') + 3 AS T2" + "\n");                        
                    }
                }
                else
                {
                    if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                    {
                        strSqlString.Append("                     , (SYSDATE - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 1) - DECODE(A.MAT_GRP_1, 'HX', (SYSDATE - TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS')), (SYSDATE - TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS'))) AS T0" + "\n");
                        strSqlString.Append("                     , (SYSDATE - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 2) - DECODE(A.MAT_GRP_1, 'HX', (SYSDATE - TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS')), (SYSDATE - TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS'))) AS T1" + "\n");
                        strSqlString.Append("                     , (SYSDATE - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 3) - DECODE(A.MAT_GRP_1, 'HX', (SYSDATE - TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS')), (SYSDATE - TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS'))) AS T2" + "\n");                     
                    }
                    else
                    {
                        strSqlString.Append("                     , DECODE(A.MAT_GRP_1, 'HX', TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS'), TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 1 AS T0" + "\n");
                        strSqlString.Append("                     , DECODE(A.MAT_GRP_1, 'HX', TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS'), TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 2 AS T1" + "\n");
                        strSqlString.Append("                     , DECODE(A.MAT_GRP_1, 'HX', TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS'), TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 3 AS T2" + "\n");                     
                    }
                }

                strSqlString.Append("                  FROM ( " + "\n");
                strSqlString.Append("                        SELECT A.LOT_ID " + "\n");
                strSqlString.Append("                             , A.FACTORY " + "\n");
                strSqlString.Append("                             , A.MAT_ID " + "\n");
                strSqlString.Append("                             , A.OPER " + "\n");
                strSqlString.Append("                             , A.QTY_1 " + "\n");
                strSqlString.Append("                             , A.LOT_CMF_5 " + "\n");
                strSqlString.Append("                             , A.LOT_CMF_14 " + "\n");
                strSqlString.Append("                             , A.RESV_FIELD_1 " + "\n");
                strSqlString.Append("                             , A.MAT_GRP_1 " + "\n");
                strSqlString.Append("                             , A.MAT_GRP_3 " + "\n");
                strSqlString.Append("                             , CASE WHEN NVL(B.DATA_2, 0) <> 0 THEN B.DATA_2  " + "\n");
                strSqlString.Append("                                    ELSE C.DATA_2 " + "\n");
                strSqlString.Append("                               END AS DATA_2 " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                SELECT STS.LOT_ID " + "\n");
                strSqlString.Append("                                     , STS.FACTORY " + "\n");
                strSqlString.Append("                                     , STS.MAT_ID " + "\n");
                strSqlString.Append("                                     , STS.OPER " + "\n");
                strSqlString.Append("                                     , STS.QTY_1 " + "\n");
                strSqlString.Append("                                     , STS.LOT_CMF_5 " + "\n");
                strSqlString.Append("                                     , STS.LOT_CMF_14 " + "\n");
                strSqlString.Append("                                     , STS.RESV_FIELD_1 " + "\n");
                strSqlString.Append("                                     , MAT.MAT_GRP_1 " + "\n");
                strSqlString.Append("                                     , MAT.MAT_GRP_3 " + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    strSqlString.Append("                                  FROM RWIPLOTSTS STS" + "\n");
                }
                else
                {
                    strSqlString.Append("                                  FROM RWIPLOTSTS_BOH STS" + "\n");
                }

                strSqlString.Append("                                     , (" + "\n");
                strSqlString.Append("                                        SELECT FACTORY " + "\n");
                strSqlString.Append("                                             , MAT_ID " + "\n");
                strSqlString.Append("                                             , MAT_GRP_1 " + "\n");
                strSqlString.Append("                                             , MAT_GRP_3 " + "\n");
                strSqlString.Append("                                          FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                       ) MAT " + "\n");
                strSqlString.Append("                                 WHERE STS.FACTORY = MAT.FACTORY" + "\n");
                strSqlString.Append("                                   AND STS.MAT_ID = MAT.MAT_ID" + "\n");
                strSqlString.Append("                                   AND STS.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                                   AND STS.LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                                   AND STS.LOT_DEL_FLAG = ' '" + "\n");

                // 내부기준이며 Issue 기준 일때.. ISSUE 시간 없는 것은 제외
                //if (rdbHana.Checked == true && cboType.Text == "Issue 기준")
                if (rdbHana.Checked == true && cboType.SelectedIndex == 1)
                {
                    strSqlString.Append("                                   AND STS.RESV_FIELD_1 <> ' ' " + "\n");
                }

                if (cbLotType.Text != "ALL")
                {
                    strSqlString.Append("                                   AND STS.LOT_CMF_5 LIKE '" + cbLotType.Text + "'" + "\n");
                }

                // 과거 일자 이면 BOH 테이블 조회
                if (DateTime.Now.ToString("yyyyMMdd") != cdvDate.SelectedValue())
                {
                    strSqlString.Append("                                   AND CUTOFF_DT = '" + stYesterday + "22'" + "\n");
                }

                strSqlString.Append("                               ) A " + "\n");

                // 고객사가 존재하는 TAT 값 가져오기
                strSqlString.Append("                             , (" + "\n");
                strSqlString.Append("                                SELECT FACTORY, KEY_2, KEY_3, SUM(TO_NUMBER(DATA_2)) AS DATA_2" + "\n");
                strSqlString.Append("                                  FROM MGCMTBLDAT" + "\n");
                strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                   AND TABLE_NAME = 'H_RPT_TAT_HANA'" + "\n");
                strSqlString.Append("                                   AND KEY_1 <= '" + stToday + "'" + "\n");
                strSqlString.Append("                                   AND DATA_1 >= '" + stToday + "'" + "\n");
                strSqlString.Append("                                   AND KEY_2 <> '-' " + "\n");

                // 선택한 공정그룹까지의 TAT 목표 SUM 값을 가져오기 위해..
                if (cboGroup.Text == "SAW")
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW')" + "\n");
                }
                else if (cboGroup.Text == "D/A")
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW', 'D/A')" + "\n");
                }
                else if (cboGroup.Text == "W/B")
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B')" + "\n");
                }
                else if (cboGroup.Text == "GATE")
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B', 'GATE')" + "\n");
                }
                else if (cboGroup.Text == "MOLD")
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B', 'GATE', 'MOLD')" + "\n");
                }
                else
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B', 'GATE', 'MOLD', 'FINISH')" + "\n");
                }

                strSqlString.Append("                                 GROUP BY FACTORY, KEY_2, KEY_3" + "\n");
                strSqlString.Append("                               ) B " + "\n");

                // 고객사 없는 공통 TAT 목표 값 가져오기
                strSqlString.Append("                             , (" + "\n");
                strSqlString.Append("                                SELECT FACTORY, KEY_2, KEY_3, SUM(TO_NUMBER(DATA_2)) AS DATA_2" + "\n");
                strSqlString.Append("                                  FROM MGCMTBLDAT" + "\n");
                strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                   AND TABLE_NAME = 'H_RPT_TAT_HANA'" + "\n");
                strSqlString.Append("                                   AND KEY_1 <= '" + stToday + "'" + "\n");
                strSqlString.Append("                                   AND DATA_1 >= '" + stToday + "'" + "\n");
                strSqlString.Append("                                   AND KEY_2 = '-' " + "\n");

                // 선택한 공정그룹까지의 TAT 목표 SUM 값을 가져오기 위해..
                if (cboGroup.Text == "SAW")
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW')" + "\n");
                }
                else if (cboGroup.Text == "D/A")
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW', 'D/A')" + "\n");
                }
                else if (cboGroup.Text == "W/B")
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B')" + "\n");
                }
                else if (cboGroup.Text == "GATE")
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B', 'GATE')" + "\n");
                }
                else if (cboGroup.Text == "MOLD")
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B', 'GATE', 'MOLD')" + "\n");
                }
                else
                {
                    strSqlString.Append("                                   AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B', 'GATE', 'MOLD', 'FINISH')" + "\n");
                }

                strSqlString.Append("                                 GROUP BY FACTORY, KEY_2, KEY_3" + "\n");
                strSqlString.Append("                               ) C " + "\n");
                strSqlString.Append("                         WHERE A.FACTORY = B.FACTORY(+) " + "\n");
                strSqlString.Append("                           AND A.MAT_GRP_1 = B.KEY_2(+) " + "\n");
                strSqlString.Append("                           AND A.MAT_GRP_3 = B.KEY_3(+) " + "\n");
                strSqlString.Append("                           AND A.FACTORY = C.FACTORY(+)" + "\n");
                strSqlString.Append("                           AND A.MAT_GRP_3 = C.KEY_3(+)" + "\n");
                strSqlString.Append("                       ) A " + "\n");
                strSqlString.Append("                     , MWIPOPRDEF B" + "\n");
                strSqlString.Append("                 WHERE A.FACTORY = B.FACTORY" + "\n");
                strSqlString.Append("                   AND A.OPER = B.OPER" + "\n");
                strSqlString.Append("                   AND A.DATA_2 > 0" + "\n");

                if (cboGroup.Text == "SAW")
                {
                    strSqlString.Append("                   AND B.OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'S/P')" + "\n");
                }
                else if (cboGroup.Text == "D/A")
                {
                    strSqlString.Append("                   AND B.OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'S/P', 'D/A')" + "\n");
                }
                else if (cboGroup.Text == "W/B")
                {
                    strSqlString.Append("                   AND B.OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'S/P', 'D/A', 'W/B')" + "\n");
                }
                //else if (cboGroup.Text == "GATE")
                //{
                //    strSqlString.Append("                   AND OPR.OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'S/P', 'D/A', 'W/B', 'GATE')" + "\n");
                //}
                else if (cboGroup.Text == "MOLD")
                {
                    strSqlString.Append("                   AND B.OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'S/P', 'D/A', 'W/B', 'GATE', 'MOLD','CURE')" + "\n");
                }
                else
                {
                    strSqlString.Append("                   AND B.OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'S/P', 'D/A', 'W/B', 'GATE', 'MOLD','CURE', 'M/K','TRIM','TIN','S/B/A','SIG','AVI','V/I','HMK3A')" + "\n");
                }

                strSqlString.Append("               )" + "\n");
                strSqlString.Append("         GROUP BY MAT_GRP_1, MAT_GRP_3, MAT_ID" + "\n");
                strSqlString.Append("       ) PLN" + "\n");
                strSqlString.Append("     , (" + "\n");
                strSqlString.Append("        SELECT MAT_ID " + "\n");
                strSqlString.Append("             , CASE WHEN '" + cboGroup.Text + "' = 'SAW' THEN HMK2A + BG + SAW " + "\n");
                strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'DA' THEN HMK2A + BG + SAW + SP + DA + DA1 + WB1 + DA2 + WB2 + DA3+ WB3+ DA4 " + "\n");
                strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'WB' THEN HMK2A + BG + SAW + SP + DA + WB + GATE + DA1 + WB1 + DA2 + WB2 + DA3+ WB3+ DA4 + WB4" + "\n");
                strSqlString.Append("                    WHEN '" + cboGroup.Text + "' = 'MOLD' THEN HMK2A + BG + SAW + SP + DA + WB + GATE + MOLD + CURE + DA1 + WB1 + DA2 + WB2 + DA3 + WB3 + DA4 + WB4 " + "\n");
                strSqlString.Append("                    ELSE TOTAL " + "\n");
                strSqlString.Append("                END AS WIP_SUM " + "\n");
                strSqlString.Append("             , TOTAL AS WIP_TTL " + "\n");
                strSqlString.Append("             , HMK2A, BG, SAW, SP, DA, WB, GATE, MOLD, CURE, MK, TRIM, TIN, SBA, SIG, AVI, VI, HMK3A, DA1, WB1, DA2, WB2, DA3, WB3, DA4, WB4 " + "\n");
                strSqlString.Append("          FROM ( " + "\n");
                strSqlString.Append("                SELECT MAT_ID" + "\n");
                strSqlString.Append("                     , SUM(NVL(QTY, 0)) TOTAL " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'HMK2A', QTY, 0)) AS HMK2A " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'B/G', QTY, 0)) AS BG " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'SAW', QTY, 0)) AS SAW " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'S/P', QTY, 0)) AS SP " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'D/A', QTY, 0)) AS DA " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'W/B', QTY, 0)) AS WB " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'GATE', QTY, 0)) AS GATE" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'MOLD', QTY, 0)) AS MOLD " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'CURE', QTY, 0)) AS CURE " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'M/K', QTY, 0)) AS MK " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'TRIM', QTY, 0)) AS TRIM " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'TIN', QTY, 0)) AS TIN" + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'S/B/A', QTY, 0)) AS SBA " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'SIG', QTY, 0)) AS SIG " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'AVI', QTY, 0)) AS AVI " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'V/I', QTY, 0)) AS VI " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'HMK3A', QTY, 0)) AS HMK3A  " + "\n");

                // 2012-11-05-임종우 : 재공 기준 DA1~DA4, WB1~WB4로 변경 
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'D/A1', QTY, 0)) AS DA1  " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'W/B1', QTY, 0)) AS WB1  " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'D/A2', QTY, 0)) AS DA2  " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'W/B2', QTY, 0)) AS WB2  " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'D/A3', QTY, 0)) AS DA3  " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'W/B3', QTY, 0)) AS WB3  " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'D/A4', QTY, 0)) AS DA4  " + "\n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP, 'W/B4', QTY, 0)) AS WB4  " + "\n");
                strSqlString.Append("                  FROM (  " + "\n");
                strSqlString.Append("                        SELECT A.FACTORY, A.MAT_ID, B.OPER_GRP_1 " + "\n");
                strSqlString.Append("                             , CASE WHEN B.OPER_GRP_1 = 'D/A' THEN (CASE WHEN A.OPER IN ('A0401','A0501','A0531') THEN 'D/A1'  " + "\n");
                strSqlString.Append("                                                                         WHEN A.OPER IN ('A0402','A0502','A0532') THEN 'D/A2'" + "\n");
                strSqlString.Append("                                                                         WHEN A.OPER IN ('A0403','A0503','A0533') THEN 'D/A3'" + "\n");
                strSqlString.Append("                                                                         WHEN A.OPER IN ('A0404','A0504','A0534') THEN 'D/A4'" + "\n");
                strSqlString.Append("                                                                         ELSE 'D/A'" + "\n");
                strSqlString.Append("                                                                    END)" + "\n");
                strSqlString.Append("                                    WHEN B.OPER_GRP_1 = 'W/B' THEN (CASE WHEN A.OPER IN ('A0551','A0601') THEN 'W/B1'  " + "\n");
                strSqlString.Append("                                                                         WHEN A.OPER IN ('A0552','A0602') THEN 'W/B2'" + "\n");
                strSqlString.Append("                                                                         WHEN A.OPER IN ('A0553','A0603') THEN 'W/B3'" + "\n");
                strSqlString.Append("                                                                         WHEN A.OPER IN ('A0554','A0604') THEN 'W/B4'" + "\n");
                strSqlString.Append("                                                                         ELSE 'W/B'" + "\n");
                strSqlString.Append("                                                                    END)" + "\n");
                strSqlString.Append("                                    ELSE B.OPER_GRP_1" + "\n");
                strSqlString.Append("                               END AS OPER_GRP" + "\n");
                strSqlString.Append("                             , A.QTY_1 AS QTY  " + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    strSqlString.Append("                          FROM RWIPLOTSTS A  " + "\n");
                    strSqlString.Append("                             , MWIPOPRDEF B  " + "\n");
                    strSqlString.Append("                         WHERE 1 = 1                      " + "\n");
                }
                else
                {
                    strSqlString.Append("                          FROM RWIPLOTSTS_BOH A  " + "\n");
                    strSqlString.Append("                             , MWIPOPRDEF B  " + "\n");
                    strSqlString.Append("                         WHERE 1 = 1                      " + "\n");
                    strSqlString.Append("                           AND A.CUTOFF_DT = '" + stToday + "22' " + "\n");
                }

                strSqlString.Append("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                           AND A.LOT_TYPE = 'W'  " + "\n");
                strSqlString.Append("                           AND A.LOT_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("                           AND B.OPER_GRP_1 <> '-'" + "\n");
                //2011-01-19-임종우 : A2100(PVI)공정 -> A2050(PVI) 변경 됨으로 인해 수정 함.
                strSqlString.Append("                           AND A.OPER NOT IN ('A000N')" + "\n");
                //strSqlString.Append("                           AND A.OPER IN ('A0000', 'A000N', 'A0020', 'A0040', 'A0100', 'A0150', 'A0170', 'A0180', 'A0200', 'A0250', 'A0270', 'A0300', 'A0320', 'A0330', 'A0340', 'A0350', 'A0360', 'A0370', 'A0380', 'A0390', 'A0400', 'A0401', 'A0500', 'A0501', 'A0540', 'A0550', 'A0551', 'A0600', 'A0620', 'A0630', 'A0650', 'A0670', 'A0800', 'A0402', 'A0502', 'A0601', 'A0801', 'A0552', 'A0602', 'A0802', 'A0403', 'A0503', 'A0553', 'A0603', 'A0803', 'A0404', 'A0504', 'A0554', 'A0604', 'A0804', 'A0405', 'A0505', 'A0555', 'A0605', 'A0805', 'A0406', 'A0506', 'A0556', 'A0606', 'A0806', 'A0407', 'A0507', 'A0557', 'A0607', 'A0807', 'A0408', 'A0508', 'A0558', 'A0608', 'A0808', 'A0409', 'A0509', 'A0559', 'A0609', 'A0809', 'A0950', 'A0970', 'A1000', 'A1070', 'A1100', 'A1110', 'A1120', 'A1130', 'A1150', 'A1600', 'A1650', 'A1170', 'A1180', 'A1200', 'A1230', 'A1250', 'A1260', 'A1300', 'A1350', 'A1450', 'A1470', 'A1500', 'A1720', 'A1750', 'A1770', 'A1800', 'A1900', 'A1950', 'A2000', 'A2100', 'A2300', 'AZ009', 'AZ010')" + "\n");
                strSqlString.Append("                           AND A.FACTORY = B.FACTORY(+)  " + "\n");
                strSqlString.Append("                           AND A.OPER = B.OPER(+)  " + "\n");

                if (cbLotType.Text != "ALL")
                {
                    strSqlString.Append("                           AND A.LOT_CMF_5 LIKE '" + cbLotType.Text + "'" + "\n");
                }
                
                strSqlString.Append("                       ) " + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) " + "\n");                
                strSqlString.Append("       ) WIP" + "\n");
                strSqlString.Append("     , (" + "\n");
                strSqlString.Append("        SELECT MAT_ID" + "\n");
                strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + stToday + "', (CASE WHEN OPER IN ('AZ010','SHIP','TZ010','F0000','EZ010', 'F0000') THEN SHIP_QTY_1 ELSE END_QTY_1 END),0)) END_QTY_1" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3" + "\n");
                strSqlString.Append("                     , SUM(END_QTY_1) AS END_QTY_1" + "\n");
                strSqlString.Append("                     , SUM(SHIP_QTY_1) AS SHIP_QTY_1" + "\n");
                strSqlString.Append("                  FROM (" + "\n");
                strSqlString.Append("                        SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3" + "\n");
                strSqlString.Append("                             , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_1+S2_OPER_IN_QTY_1+S3_OPER_IN_QTY_1),SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1)) END_QTY_1" + "\n");
                strSqlString.Append("                             , 0 SHIP_QTY_1" + "\n");
                strSqlString.Append("                          FROM RSUMWIPMOV " + "\n");
                strSqlString.Append("                         WHERE WORK_DATE = '" + stToday + "'" + "\n");
                strSqlString.Append("                           AND OPER NOT IN ('AZ010','TZ010','F0000','EZ010', 'F0000')" + "\n");
                strSqlString.Append("                           AND MAT_ID NOT LIKE 'HX%'" + "\n");

                if (cbLotType.Text != "ALL")
                {
                    strSqlString.Append("                           AND CM_KEY_3 LIKE '" + cbLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                         GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3" + "\n");
                strSqlString.Append("                         UNION ALL" + "\n");
                strSqlString.Append("                        SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3" + "\n");
                strSqlString.Append("                             , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_1+S2_OPER_IN_QTY_1+S3_OPER_IN_QTY_1),SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1)) END_QTY_1" + "\n");
                strSqlString.Append("                             , 0 SHIP_QTY_1" + "\n");
                strSqlString.Append("                          FROM CSUMWIPMOV " + "\n");
                strSqlString.Append("                         WHERE WORK_DATE = '" + stToday + "'" + "\n");
                strSqlString.Append("                           AND OPER NOT IN ('AZ010','TZ010','F0000','EZ010', 'F0000')" + "\n");
                strSqlString.Append("                           AND MAT_ID LIKE 'HX%'" + "\n");

                if (cbLotType.Text != "ALL")
                {
                    strSqlString.Append("                           AND CM_KEY_3 LIKE '" + cbLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                         GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3" + "\n");
                strSqlString.Append("                         UNION ALL" + "\n");
                strSqlString.Append("                        SELECT CM_KEY_1 AS FACTORY, MAT_ID" + "\n");
                strSqlString.Append("                             , DECODE(CM_KEY_1,'" + GlobalVariable.gsAssyDefaultFactory + "','AZ010','" + GlobalVariable.gsTestDefaultFactory + "','TZ010','FGS','F0000','HMKE1','EZ010') OPER" + "\n");
                strSqlString.Append("                             , LOT_TYPE, MAT_VER,  WORK_DATE,CM_KEY_3" + "\n");
                strSqlString.Append("                             , 0 END_QTY_1" + "\n");
                strSqlString.Append("                             , SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) SHIP_QTY_1" + "\n");
                strSqlString.Append("                          FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                         WHERE FACTORY NOT IN ('RETURN')" + "\n");
                strSqlString.Append("                           AND WORK_DATE = '" + stToday + "'" + "\n");
                strSqlString.Append("                           AND MAT_ID NOT LIKE 'HX%'" + "\n");

                if (cbLotType.Text != "ALL")
                {
                    strSqlString.Append("                           AND CM_KEY_3 LIKE '" + cbLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                         GROUP BY CM_KEY_1, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3" + "\n");
                strSqlString.Append("                         UNION ALL" + "\n");
                strSqlString.Append("                        SELECT CM_KEY_1 AS FACTORY, MAT_ID" + "\n");
                strSqlString.Append("                             , DECODE(CM_KEY_1,'" + GlobalVariable.gsAssyDefaultFactory + "','AZ010','" + GlobalVariable.gsTestDefaultFactory + "','TZ010','FGS','F0000','HMKE1','EZ010') OPER" + "\n");
                strSqlString.Append("                             , LOT_TYPE, MAT_VER,  WORK_DATE,CM_KEY_3" + "\n");
                strSqlString.Append("                             , 0 END_QTY_1" + "\n");
                strSqlString.Append("                             , SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) SHIP_QTY_1" + "\n");
                strSqlString.Append("                          FROM CSUMFACMOV " + "\n");
                strSqlString.Append("                         WHERE FACTORY NOT IN ('RETURN')" + "\n");
                strSqlString.Append("                           AND WORK_DATE = '" + stToday + "'" + "\n");
                strSqlString.Append("                           AND MAT_ID LIKE 'HX%'" + "\n");

                if (cbLotType.Text != "ALL")
                {
                    strSqlString.Append("                           AND CM_KEY_3 LIKE '" + cbLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                         GROUP BY CM_KEY_1, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3" + "\n");
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("                 GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3" + "\n");
                strSqlString.Append("               ) " + "\n");
                strSqlString.Append("        WHERE 1=1 " + "\n");
                strSqlString.Append("          AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'              " + "\n");
                strSqlString.Append("          AND MAT_VER = 1 " + "\n");

                // 선택한 공정그룹의 대표공정을 가지고 실적을 가져옴.
                if (cboGroup.Text == "SAW")
                {
                    strSqlString.Append("          AND OPER = 'A0300' " + "\n");
                }
                else if (cboGroup.Text == "D/A")
                {
                    strSqlString.Append("          AND OPER IN ('A0400','A0402')" + "\n");
                }
                else if (cboGroup.Text == "W/B")
                {
                    strSqlString.Append("          AND OPER IN ('A0600','A0602')" + "\n");
                }
                //else if (cboGroup.Text == "GATE")
                //{
                //    strSqlString.Append("          AND OPER IN ('A0800','A0802')" + "\n");
                //}
                else if (cboGroup.Text == "MOLD")
                {
                    strSqlString.Append("          AND OPER IN ('A1100','A1180','A1230')" + "\n");
                }
                else
                {
                    strSqlString.Append("          AND OPER IN ('AZ010')" + "\n");
                }

                strSqlString.Append("          AND OPER NOT IN ('00001','00002') " + "\n");
                strSqlString.Append("        GROUP BY MAT_ID" + "\n");
                strSqlString.Append("       ) SHP" + "\n");
                strSqlString.Append("     , (" + "\n");
                strSqlString.Append("         SELECT A.MAT_ID" + "\n");
                strSqlString.Append("             , SUM(B.PLAN_QTY_ASSY) MON_PLAN" + "\n");
                strSqlString.Append("             , SUM(C.SHIP_MON) SHIP_MON " + "\n");
                //strSqlString.Append("             , SUM(CASE WHEN NVL(C.SHIP_MON, 0) = 0 OR NVL(B.PLAN_QTY_ASSY,0) = 0 THEN 0" + "\n");
                //strSqlString.Append("                        ELSE (NVL(C.SHIP_MON,0) / NVL(B.PLAN_QTY_ASSY,0)) / (" + itoday + "/" + ilastday + ") * 100 " + "\n");
                //strSqlString.Append("                   END) AS JINDO" + "\n");
                strSqlString.Append("             , SUM(NVL(B.PLAN_QTY_ASSY,0) - NVL(C.SHIP_MON,0)) AS TARGET" + "\n");                
                //strSqlString.Append("             , SUM(CASE WHEN NVL(B.PLAN_QTY_ASSY,0) - NVL(C.SHIP_MON,0) < 0 THEN 0" + "\n");
                //strSqlString.Append("                        ELSE (NVL(B.PLAN_QTY_ASSY,0) - NVL(C.SHIP_MON,0)) / " + idefday + "\n");
                //strSqlString.Append("                   END) AS TARGET" + "\n");
                strSqlString.Append("          FROM MWIPMATDEF A " + "\n");
                //월 계획 SLIS 제품은 MP계획과 동일하게
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                 SELECT FACTORY,MAT_ID,PLAN_QTY_ASSY,PLAN_MONTH " + "\n");
                strSqlString.Append("                   FROM ( " + "\n");
                strSqlString.Append("                          SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, PLAN_MONTH " + "\n");
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
                strSqlString.Append("                           GROUP BY FACTORY, MAT_ID, PLAN_MONTH " + "\n");
                strSqlString.Append("                          UNION ALL " + "\n");
                strSqlString.Append("                          SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_ASSY , '" + cdvDate.SelectedValue().Substring(0, 6) + "' AS PLAN_MONTH " + "\n");
                strSqlString.Append("                            FROM ( " + "\n");

                // 월계획 금일이면 기존대로
                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                    strSqlString.Append("                                     FROM CWIPPLNDAY " + "\n");
                    strSqlString.Append("                                    WHERE 1=1 " + "\n");
                    strSqlString.Append("                                      AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + start_date + "' AND '" + last_day + "'\n");
                    strSqlString.Append("                                      AND IN_OUT_FLAG = 'OUT' " + "\n");
                    strSqlString.Append("                                      AND CLASS = 'ASSY' " + "\n");
                    strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                }
                else// 금일이 아니면 스냅샷 떠놓은 테이블에서 가져옴.
                {
                    strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                    strSqlString.Append("                                     FROM CWIPPLNSNP@RPTTOMES " + "\n");
                    strSqlString.Append("                                    WHERE 1=1 " + "\n");
                    strSqlString.Append("                                      AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                                      AND SNAPSHOT_DAY = '" + cdvDate.SelectedValue() + "'" + "\n");
                    strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + start_date + "' AND '" + last_day + "'\n");
                    strSqlString.Append("                                      AND IN_OUT_FLAG = 'OUT' " + "\n");
                    strSqlString.Append("                                      AND CLASS = 'ASSY' " + "\n");
                    strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                }

                strSqlString.Append("                                   UNION ALL " + "\n");
                strSqlString.Append("                                   SELECT CM_KEY_1 AS FACTORY, MAT_ID, " + "\n");
                strSqlString.Append("                                          SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                                      AND WORK_DATE BETWEEN '" + start_date + "' AND '" + Lastweek_lastday + "'\n");
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
                strSqlString.Append("               ) B " + "\n");
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT MAT_ID " + "\n");
                strSqlString.Append("                     , SUM(NVL(SHP_QTY_1, 0)) AS SHIP_MON " + "\n");
                strSqlString.Append("                  FROM VSUMWIPSHP " + "\n");
                strSqlString.Append("                 WHERE 1 = 1 " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND CM_KEY_2 = 'PROD'" + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                   AND WORK_MONTH = '" + month + "'" + "\n");
                strSqlString.Append("                   AND MAT_ID NOT LIKE 'HX%'" + "\n");

                if (cbLotType.Text != "ALL")
                {
                    strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cbLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("                 UNION ALL " + "\n");
                strSqlString.Append("                SELECT MAT_ID " + "\n");
                strSqlString.Append("                     , SUM(NVL(SHP_QTY_1, 0)) AS SHIP_MON " + "\n");
                strSqlString.Append("                  FROM VSUMWIPSHP_06 " + "\n");
                strSqlString.Append("                 WHERE 1 = 1 " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND CM_KEY_2 = 'PROD'" + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                   AND WORK_MONTH = '" + month + "'" + "\n");
                strSqlString.Append("                   AND MAT_ID LIKE 'HX%'" + "\n");

                if (cbLotType.Text != "ALL")
                {
                    strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cbLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
                strSqlString.Append("               ) C" + "\n");
                strSqlString.Append("         WHERE 1 = 1 " + "\n");
                strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND B.PLAN_MONTH(+) = '" + month + "' " + "\n");
                strSqlString.Append("           AND A.MAT_TYPE= 'FG' " + "\n");
                strSqlString.Append("           AND A.FACTORY =B.FACTORY(+) " + "\n");
                strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND A.MAT_ID = C.MAT_ID(+) " + "\n");
                strSqlString.Append("         GROUP BY A.MAT_ID" + "\n");
                strSqlString.Append("        HAVING NVL(SUM(B.PLAN_QTY_ASSY), 0) + NVL(SUM(C.SHIP_MON),0) > 0" + "\n");
                strSqlString.Append("       ) ETC" + "\n");
                strSqlString.Append("       , MWIPMATDEF MAT" + "\n");
                strSqlString.Append(" WHERE 1=1" + "\n");
                strSqlString.Append("   AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
                strSqlString.Append("   AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");
                strSqlString.Append("   AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
                strSqlString.Append("   AND MAT.MAT_ID = ETC.MAT_ID(+)" + "\n");
                strSqlString.Append("   AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                #endregion

                strSqlString.AppendFormat(" GROUP BY {0}" + "\n", QueryCond2);
                strSqlString.Append(" HAVING ( " + "\n");
                strSqlString.Append("         NVL(SUM(SHP.END_QTY_1),0) + NVL(SUM(PLN.D0),0) + NVL(SUM(PLN.D1),0) + NVL(SUM(PLN.D2),0) + " + "\n");
                strSqlString.Append("         NVL(SUM(WIP.WIP_SUM),0) + NVL(SUM(ETC.MON_PLAN),0) + NVL(SUM(ETC.SHIP_MON),0) " + "\n");
                strSqlString.Append("        ) <> 0 " + "\n");
                strSqlString.AppendFormat(" ORDER BY {0}" + "\n", QueryCond2);
            }
            #endregion

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        // 지난 주차의 마지막일 가져오기
        private string MakeSqlString2(string year, string date)
        {
            StringBuilder sqlString = new StringBuilder();

            sqlString.Append("SELECT MIN(SYS_DATE-1) " + "\n");
            sqlString.Append("  FROM MWIPCALDEF " + "\n");
            sqlString.Append(" WHERE 1=1" + "\n");
            sqlString.Append("   AND CALENDAR_ID='SE'" + "\n");
            sqlString.Append("   AND SYS_YEAR='" + year + "'\n");
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

           

        #region ToExcel

        /// <summary>
        /// ToExcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null);
        }

        #endregion

        #region 3day Plan Cell을 클릭 했을 경우의 팝업창
        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //string stType = spdData.ActiveSheet.Cells[e.Row, e.Column].Column.Label.ToString();

            Color BackColor = spdData.ActiveSheet.Cells[1, 13].BackColor;

            string stType = null;
            string[] dataArry = new string[10];
            int iD0 = 0;

            if (rdbCus.Checked == true)
            {
                if (e.Column == 13)
                    stType = "D0";
                if (e.Column == 14)
                    stType = "D1";
                if (e.Column == 15)
                    stType = "D2";

                iD0 = 13;
            }
            else
            {
                if (e.Column == 31)
                    stType = "D0";
                if (e.Column == 32)
                    stType = "D1";
                if (e.Column == 33)
                    stType = "D2";

                iD0 = 31;
            }

            if (stType == "D0" || stType == "D1" || stType == "D2")
            {
                // GrandTotal 과 SubTotal 제외하기 위해
                if (e.Row != 0 && spdData.ActiveSheet.Cells[e.Row, iD0].BackColor == BackColor)                
                {
                    // Group 정보 가져와서 담기... 상세 팝업에서 조건값으로 사용하기 위해
                    for (int i = 0; i < 10; i++)
                    {
                        dataArry[i] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();
                    }

                    // 고객사 명을 고객사 코드로 변경하기 위해..
                    if (dataArry[0] != " ")
                    {
                        DataTable dtCustomer = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlCustomer(dataArry[0].ToString()));

                        dataArry[0] = dtCustomer.Rows[0][0].ToString();
                    }

                    DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetail(stType, dataArry));

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        System.Windows.Forms.Form frm = new TAT050702_P1("", dt);
                        frm.ShowDialog();
                    }
                }
            }
            else
            {
                return;
            }
        }
        #endregion

        #region MakeSqlCustomer
        //고객사 명으로 고객사 코드 가져오기 위해..
        private string MakeSqlCustomer(string customerName)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT KEY_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND DATA_1 = '" + customerName + "' AND ROWNUM=1" + "\n");

            return strSqlString.ToString();

        }
        #endregion

        #region MakeSqlDetail
        // 상세 팝업창 쿼리
        private string MakeSqlDetail(string stType, string[] dataArry)
        {
            string sTableName = null;

            if (rdbCus.Checked == true)
            {
                sTableName = "H_RPT_TAT_MAINOBJECT";
            }
            else
            {
                sTableName = "H_RPT_TAT_HANA";
            }

            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT ROW_NUMBER() OVER(ORDER BY STS.OPER, STS.LOT_ID) AS NO" + "\n");
            strSqlString.Append("     , (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1" + "\n");
            strSqlString.Append("     , MAT_GRP_3" + "\n");
            strSqlString.Append("     , STS.MAT_ID" + "\n");
            strSqlString.Append("     , STS.LOT_ID" + "\n");
            strSqlString.Append("     , STS.LOT_CMF_5" + "\n");
            strSqlString.Append("     , STS.QTY_1" + "\n");
            strSqlString.Append("     , DECODE(STS.OPER, ' ', 'SHIP', STS.OPER) AS OPER" + "\n");
            strSqlString.Append("     , DECODE(HOLD_FLAG, 'Y', 'HOLD', DECODE(STS.QTY_1, 0, 'SPLIT', DECODE(LOT_STATUS, 'PROC', 'RUN', 'WAIT'))) AS STATUS " + "\n");
            strSqlString.Append("     , STS.START_RES_ID" + "\n");
            strSqlString.Append("     , ROUND(SYSDATE - TO_DATE(STS.LOT_CMF_14, 'YYYYMMDDHH24MISS'), 2) AS TAT " + "\n");
            strSqlString.Append("     , OUT_TARGET_TIME " + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT A.MAT_GRP_1" + "\n");
            strSqlString.Append("             , A.MAT_GRP_3" + "\n");
            strSqlString.Append("             , A.MAT_ID" + "\n");
            strSqlString.Append("             , A.LOT_ID" + "\n");
            strSqlString.Append("             , A.OPER" + "\n");
            strSqlString.Append("             , A.QTY_1" + "\n");
            strSqlString.Append("             , A.DATA_2" + "\n");            
            strSqlString.Append("             , B.OPER_GRP_1" + "\n");
            strSqlString.Append("             , TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + A.DATA_2 AS OUT_TARGET_TIME " + "\n");

            if (rdbCus.Checked == true)
            {
                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    strSqlString.Append("             , (SYSDATE - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 1) - (SYSDATE - TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS')) AS D0" + "\n");
                    strSqlString.Append("             , (SYSDATE - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 2) - (SYSDATE - TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS')) AS D1" + "\n");
                    strSqlString.Append("             , (SYSDATE - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 3) - (SYSDATE - TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS')) AS D2" + "\n");                    
                }
                else
                {
                    strSqlString.Append("             , TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS') - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 1 AS D0" + "\n");
                    strSqlString.Append("             , TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS') - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 2 AS D1" + "\n");
                    strSqlString.Append("             , TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS') - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 3 AS D2" + "\n");
                }
            }
            else
            {
                //if (cboType.Text == "Issue 기준")
                if (cboType.SelectedIndex == 1)
                {
                    if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                    {
                        strSqlString.Append("             , (SYSDATE - TO_DATE(A.RESV_FIELD_1, 'YYYYMMDDHH24MISS') + 1) - DECODE(A.MAT_GRP_1, 'HX', (SYSDATE - TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS')), (SYSDATE - TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS'))) AS D0" + "\n");
                        strSqlString.Append("             , (SYSDATE - TO_DATE(A.RESV_FIELD_1, 'YYYYMMDDHH24MISS') + 2) - DECODE(A.MAT_GRP_1, 'HX', (SYSDATE - TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS')), (SYSDATE - TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS'))) AS D1" + "\n");
                        strSqlString.Append("             , (SYSDATE - TO_DATE(A.RESV_FIELD_1, 'YYYYMMDDHH24MISS') + 3) - DECODE(A.MAT_GRP_1, 'HX', (SYSDATE - TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS')), (SYSDATE - TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS'))) AS D2" + "\n");                        
                    }
                    else
                    {
                        strSqlString.Append("             , DECODE(A.MAT_GRP_1, 'HX', TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS'), TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(A.RESV_FIELD_1, 'YYYYMMDDHH24MISS') + 1 AS D0" + "\n");
                        strSqlString.Append("             , DECODE(A.MAT_GRP_1, 'HX', TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS'), TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(A.RESV_FIELD_1, 'YYYYMMDDHH24MISS') + 2 AS D1" + "\n");
                        strSqlString.Append("             , DECODE(A.MAT_GRP_1, 'HX', TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS'), TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(A.RESV_FIELD_1, 'YYYYMMDDHH24MISS') + 3 AS D2" + "\n");                        
                    }
                }
                else
                {
                    if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                    {
                        strSqlString.Append("             , (SYSDATE - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 1) - DECODE(A.MAT_GRP_1, 'HX', (SYSDATE - TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS')), (SYSDATE - TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS'))) AS D0" + "\n");
                        strSqlString.Append("             , (SYSDATE - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 2) - DECODE(A.MAT_GRP_1, 'HX', (SYSDATE - TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS')), (SYSDATE - TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS'))) AS D1" + "\n");
                        strSqlString.Append("             , (SYSDATE - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 3) - DECODE(A.MAT_GRP_1, 'HX', (SYSDATE - TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS')), (SYSDATE - TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS'))) AS D2" + "\n");                        
                    }
                    else
                    {
                        strSqlString.Append("             , DECODE(A.MAT_GRP_1, 'HX', TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS'), TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 1 AS D0" + "\n");
                        strSqlString.Append("             , DECODE(A.MAT_GRP_1, 'HX', TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS'), TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 2 AS D1" + "\n");
                        strSqlString.Append("             , DECODE(A.MAT_GRP_1, 'HX', TO_DATE('" + stToday + "060000', 'YYYYMMDDHH24MISS'), TO_DATE('" + stYesterday + "220000', 'YYYYMMDDHH24MISS')) - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS') + 3 AS D2" + "\n");                     
                    }
                }
            }

            strSqlString.Append("          FROM ( " + "\n");            
            strSqlString.Append("                SELECT A.LOT_ID " + "\n");
            strSqlString.Append("                     , A.FACTORY " + "\n");
            strSqlString.Append("                     , A.MAT_ID " + "\n");
            strSqlString.Append("                     , A.OPER " + "\n");
            strSqlString.Append("                     , A.QTY_1 " + "\n");
            strSqlString.Append("                     , A.LOT_CMF_5 " + "\n");
            strSqlString.Append("                     , A.LOT_CMF_14 " + "\n");
            strSqlString.Append("                     , A.RESV_FIELD_1 " + "\n");
            strSqlString.Append("                     , A.MAT_GRP_1 " + "\n");
            strSqlString.Append("                     , A.MAT_GRP_3 " + "\n");
            strSqlString.Append("                     , CASE WHEN NVL(B.DATA_2, 0) <> 0 THEN B.DATA_2  " + "\n");
            strSqlString.Append("                            ELSE C.DATA_2 " + "\n");
            strSqlString.Append("                       END AS DATA_2 " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT STS.LOT_ID " + "\n");
            strSqlString.Append("                             , STS.FACTORY " + "\n");
            strSqlString.Append("                             , STS.MAT_ID " + "\n");
            strSqlString.Append("                             , STS.OPER " + "\n");
            strSqlString.Append("                             , STS.QTY_1 " + "\n");
            strSqlString.Append("                             , STS.LOT_CMF_5 " + "\n");
            strSqlString.Append("                             , STS.LOT_CMF_14 " + "\n");
            strSqlString.Append("                             , STS.RESV_FIELD_1 " + "\n");
            strSqlString.Append("                             , MAT.MAT_GRP_1 " + "\n");
            strSqlString.Append("                             , MAT.MAT_GRP_3 " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {                
                strSqlString.Append("                          FROM RWIPLOTSTS STS" + "\n");
            }
            else
            {
                strSqlString.Append("                          FROM RWIPLOTSTS_BOH STS" + "\n");
            }
            
            strSqlString.Append("                             , (" + "\n");
            strSqlString.Append("                                SELECT FACTORY " + "\n");
            strSqlString.Append("                                     , MAT_ID " + "\n");
            strSqlString.Append("                                     , MAT_GRP_1 " + "\n");
            strSqlString.Append("                                     , MAT_GRP_3 " + "\n");
            strSqlString.Append("                                  FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (dataArry[0] != " ")
                strSqlString.AppendFormat("                                   AND MAT_GRP_1 = '" + dataArry[0] + "'" + "\n");

            if (dataArry[1] != " ")
                strSqlString.AppendFormat("                                   AND MAT_GRP_2 = '" + dataArry[1] + "'" + "\n");

            if (dataArry[2] != " ")
                strSqlString.AppendFormat("                                   AND MAT_GRP_3 = '" + dataArry[2] + "'" + "\n");

            if (dataArry[3] != " ")
                strSqlString.AppendFormat("                                   AND MAT_GRP_4 = '" + dataArry[3] + "'" + "\n");

            if (dataArry[4] != " ")
                strSqlString.AppendFormat("                                   AND MAT_GRP_5 = '" + dataArry[4] + "'" + "\n");

            if (dataArry[5] != " ")
                strSqlString.AppendFormat("                                   AND MAT_GRP_6 = '" + dataArry[5] + "'" + "\n");

            if (dataArry[6] != " ")
                strSqlString.AppendFormat("                                   AND MAT_GRP_7 = '" + dataArry[6] + "'" + "\n");

            if (dataArry[7] != " ")
                strSqlString.AppendFormat("                                   AND MAT_GRP_8 = '" + dataArry[7] + "'" + "\n");

            if (dataArry[8] != " ")
                strSqlString.AppendFormat("                                   AND MAT_CMF_10 = '" + dataArry[8] + "'" + "\n");

            if (dataArry[9] != " ")
                strSqlString.AppendFormat("                                   AND MAT_ID = '" + dataArry[9] + "'" + "\n");
            #endregion

            strSqlString.Append("                               ) MAT " + "\n");
            strSqlString.Append("                         WHERE STS.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("                           AND STS.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                           AND STS.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND STS.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                           AND STS.LOT_DEL_FLAG = ' '" + "\n");

            // 내부기준이며 Issue 기준 일때.. ISSUE 시간 없는 것은 제외
            //if (rdbHana.Checked == true && cboType.Text == "Issue 기준")
            if (rdbHana.Checked == true && cboType.SelectedIndex == 1)
            {
                strSqlString.Append("                           AND STS.RESV_FIELD_1 <> ' ' " + "\n");
            }

            if (cbLotType.Text != "ALL")
            {
                strSqlString.Append("                           AND STS.LOT_CMF_5 LIKE '" + cbLotType.Text + "'" + "\n");
            }

            // 과거 일자 이면 BOH 테이블 조회
            if (DateTime.Now.ToString("yyyyMMdd") != cdvDate.SelectedValue())
            {
                strSqlString.Append("                           AND CUTOFF_DT = '" + stYesterday + "22'" + "\n");
            }
            
            strSqlString.Append("                       ) A " + "\n");

            // 고객사가 존재하는 TAT 값 가져오기
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT FACTORY, KEY_2, KEY_3, SUM(TO_NUMBER(DATA_2)) AS DATA_2" + "\n");
            strSqlString.Append("                          FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND TABLE_NAME = '" + sTableName + "'" + "\n");
            strSqlString.Append("                           AND KEY_1 <= '" + stToday + "'" + "\n");
            strSqlString.Append("                           AND DATA_1 >= '" + stToday + "'" + "\n");
            strSqlString.Append("                           AND KEY_2 <> '-' " + "\n");

            // 선택한 공정그룹까지의 TAT 목표 SUM 값을 가져오기 위해..
            if (cboGroup.Text == "SAW")
            {
                strSqlString.Append("                           AND KEY_4 IN ('HMK2A','SAW')" + "\n");
            }
            else if (cboGroup.Text == "D/A")
            {
                strSqlString.Append("                           AND KEY_4 IN ('HMK2A','SAW', 'D/A')" + "\n");
            }
            else if (cboGroup.Text == "W/B")
            {
                strSqlString.Append("                           AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B')" + "\n");
            }
            else if (cboGroup.Text == "GATE")
            {
                strSqlString.Append("                           AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B', 'GATE')" + "\n");
            }
            else if (cboGroup.Text == "MOLD")
            {
                strSqlString.Append("                           AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B', 'GATE', 'MOLD')" + "\n");
            }
            else
            {
                strSqlString.Append("                           AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B', 'GATE', 'MOLD', 'FINISH')" + "\n");
            }

            strSqlString.Append("                         GROUP BY FACTORY, KEY_2, KEY_3" + "\n");
            strSqlString.Append("                       ) B " + "\n");

            // 고객사 없는 공통 TAT 목표 값 가져오기
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT FACTORY, KEY_2, KEY_3, SUM(TO_NUMBER(DATA_2)) AS DATA_2" + "\n");
            strSqlString.Append("                          FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND TABLE_NAME = '" + sTableName + "'" + "\n");
            strSqlString.Append("                           AND KEY_1 <= '" + stToday + "'" + "\n");
            strSqlString.Append("                           AND DATA_1 >= '" + stToday + "'" + "\n");
            strSqlString.Append("                           AND KEY_2 = '-' " + "\n");

            // 선택한 공정그룹까지의 TAT 목표 SUM 값을 가져오기 위해..
            if (cboGroup.Text == "SAW")
            {
                strSqlString.Append("                           AND KEY_4 IN ('HMK2A','SAW')" + "\n");
            }
            else if (cboGroup.Text == "D/A")
            {
                strSqlString.Append("                           AND KEY_4 IN ('HMK2A','SAW', 'D/A')" + "\n");
            }
            else if (cboGroup.Text == "W/B")
            {
                strSqlString.Append("                           AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B')" + "\n");
            }
            else if (cboGroup.Text == "GATE")
            {
                strSqlString.Append("                           AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B', 'GATE')" + "\n");
            }
            else if (cboGroup.Text == "MOLD")
            {
                strSqlString.Append("                           AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B', 'GATE', 'MOLD')" + "\n");
            }
            else
            {
                strSqlString.Append("                           AND KEY_4 IN ('HMK2A','SAW', 'D/A', 'W/B', 'GATE', 'MOLD', 'FINISH')" + "\n");
            }

            strSqlString.Append("                         GROUP BY FACTORY, KEY_2, KEY_3" + "\n");
            strSqlString.Append("                       ) C " + "\n");
            strSqlString.Append("                 WHERE A.FACTORY = B.FACTORY(+) " + "\n");
            strSqlString.Append("                   AND A.MAT_GRP_1 = B.KEY_2(+) " + "\n");
            strSqlString.Append("                   AND A.MAT_GRP_3 = B.KEY_3(+) " + "\n");
            strSqlString.Append("                   AND A.FACTORY = C.FACTORY(+)" + "\n");
            strSqlString.Append("                   AND A.MAT_GRP_3 = C.KEY_3(+)" + "\n");            
            strSqlString.Append("               ) A " + "\n");
            strSqlString.Append("               , MWIPOPRDEF B" + "\n");
            strSqlString.Append("           WHERE A.FACTORY = B.FACTORY" + "\n");            
            strSqlString.Append("             AND A.OPER = B.OPER" + "\n");
            strSqlString.Append("             AND A.DATA_2 > 0" + "\n");

            if (cboGroup.Text == "SAW")
            {
                strSqlString.Append("             AND B.OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'S/P')" + "\n");
            }
            else if (cboGroup.Text == "D/A")
            {
                strSqlString.Append("             AND B.OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'S/P', 'D/A')" + "\n");
            }
            else if (cboGroup.Text == "W/B")
            {
                strSqlString.Append("             AND B.OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'S/P', 'D/A', 'W/B')" + "\n");
            }
            //else if (cboGroup.Text == "GATE")
            //{
            //    strSqlString.Append("                   AND OPR.OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'S/P', 'D/A', 'W/B', 'GATE')" + "\n");
            //}
            else if (cboGroup.Text == "MOLD")
            {
                strSqlString.Append("             AND B.OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'S/P', 'D/A', 'W/B', 'GATE', 'MOLD','CURE')" + "\n");
            }
            else
            {
                strSqlString.Append("             AND B.OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'S/P', 'D/A', 'W/B', 'GATE', 'MOLD','CURE', 'M/K','TRIM','TIN','S/B/A','SIG','AVI','V/I','HMK3A')" + "\n");
            }

            strSqlString.Append("         ) HIS" + "\n");            
            strSqlString.Append("       , RWIPLOTSTS STS " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");

            if (stType == "D0")
            {
                strSqlString.Append("   AND DATA_2 <= " + stType + "\n");
            }
            else if (stType == "D1")
            {
                strSqlString.Append("   AND DATA_2 > D0 " + "\n");
                strSqlString.Append("   AND DATA_2 <= D1" + "\n");
            }
            else
            {
                strSqlString.Append("   AND DATA_2 > D1 " + "\n");
                strSqlString.Append("   AND DATA_2 <= D2" + "\n");
            }

            strSqlString.Append("   AND HIS.LOT_ID = STS.LOT_ID " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion MakeSqlDetail

        private void TAT050702_Load(object sender, EventArgs e)
        {
            //기본적으로 Detail이 보이도록..
            //pnlWIPDetail.Visible = true;
        }

        private void rdbCus_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCus.Checked == true)
            {
                cboType.Visible = false;
                ckbMon.Visible = false;
            }
            else
            {
                cboType.Visible = true;
                ckbMon.Visible = true;
            }
        }      
    }
}