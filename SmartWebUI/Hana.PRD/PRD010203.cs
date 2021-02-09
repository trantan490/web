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
    public partial class PRD010203 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010203<br/>
        /// 클래스요약: SOP 관리<br/>
        /// 작  성  자: 미라콤 김민규<br/>
        /// 최초작성일: 2008-12-05<br/>
        /// 상세  설명: SOP 관리<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// 2010-08-30-임종우 : 금일 목표 대비 재공 색상 표시(임태성 요청)
        /// 2010-09-07-김민우 : 월현황, 투입 보여지는 여부 Check박스 구현, 월현황, 투입 항목이 보여지지 않을땐 금주까지 틀 고정
        /// 2010-09-30-임종우 : HMKT1 조회시 당일실적 / A-TTL 사이에 RE_MAIN 추가 (권문석 요청)
        /// 2010-09-30-임종우 : HMKT1 조회시 ASSY 재공 표시 부분에 기존 BOND를 D/A와 W/B으로 분리 (권문석 요청)
        /// 2011-01-17-김민우 : HMKT1 조회시 ASSY 재공 표시 부분에 기존 D/A를 D/B와 D/A으로 분리 (권문석 요청)
        /// 2011-04-25-임종우 : 이름 변경 (MP관리 -> SOP 관리)
        /// 2011-04-27-임종우 : 금주 계획 로직 변경 -> 1. 월,화 : 월~일 계획    2. 수~일 : 월요일 실적 + 화~일 계획
        /// 2011-06-21-임종우 : CHIP 부족, 투입 필요 추가 (김문한 요청)
        /// 2011-11-22-임종우 : 주 계획 로직 변경 - 해당 주차에 현재일 기준 전일까지의 실적 + 현재일 이후 계획 값으로 변경 (삼성 강진욱)
        /// 2011-12-22-임종우 : PRODUCT 검색 기능 추가 (권문석 요청)
        /// 2011-12-26-임종우 : MWIPCALDEF 의 작년,올해 마지막 주차 겹치는 에러 발생으로 SYS_YEAR -> PLAN_YEAR 으로 변경
        /// 2012-03-26-임종우 : ASSY 투입계획 부분 추가(권문석 요청)
        /// 2012-04-19-임종우 : ASSY 투입실적(ISSUE) 부분 추가 (김은석 요청)
        /// 2012-08-10-임종우 : 일별 목표는 일별 계획 - 일별 실적으로 변경. 실적이 더 많으면 -표시 되도록.. (김문한K 요청)
        /// 2012-08-23-임종우 : COB는 D0, 투입실적, 실적잔량, D1에 한해 Net Die 이용하여 WEFER 장 수 표현 가능하도록 수정 (김문한K 요청)
        /// </summary>

        static string[] DateArray = new string[8];
        static string[] DateArray2 = new string[8];
        static string Now_date = string.Empty;
        static string This_Week = string.Empty;
        static string Next_Week = string.Empty;
        static string Secon_Week = string.Empty;
        static string Third_Week = string.Empty;
        static string yesterday_Week = string.Empty;
        static string Next_Year = string.Empty;
        static string Secon_Year = string.Empty;
        static string yesterday_Year = string.Empty;


        DataTable Oper_Group = null;
        DataTable LowDayofWeek = null;
        // DataTable HighDayofWeek = null;

        public PRD010203()
        {
            InitializeComponent();
            SortInit();
            GridColumnInit();

            udcWIPCondition1.Text = "SE";
            udcWIPCondition1.Enabled = false;
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
            GetDayofWeek();
            spdData.RPT_ColumnInit();            

            try
            {
                spdData.RPT_AddBasicColumn("Product", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Customer", 1, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Family", 1, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Package", 1, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 1, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type2", 1, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD Count", 1, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Density", 1, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Generation", 1, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Pin Type", 1, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Product", 1, 9, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70); ;
                spdData.RPT_AddBasicColumn("Cust Device", 1, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 0, 11);
                if (ckbVisible.Checked == false)
                {

                    spdData.RPT_AddBasicColumn("Monthly status", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Monthly plan", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("월SHIP", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    if (cdvFactory.txtValue != "" && cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
                    {
                        spdData.RPT_AddBasicColumn("월T/O", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("월A/O", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }

                    spdData.RPT_AddBasicColumn("Month shortage", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Progress rate", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 11, 5);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Monthly status", 0, 11, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Monthly plan", 1, 11, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("월SHIP", 1, 12, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    if (cdvFactory.txtValue != "" && cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
                    {
                        spdData.RPT_AddBasicColumn("월T/O", 1, 13, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("월A/O", 1, 13, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }

                    spdData.RPT_AddBasicColumn("Month shortage", 1, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Progress rate", 1, 15, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 11, 5);

                }


                spdData.RPT_AddBasicColumn("금주(WW" + This_Week + ")", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("plan", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("actual", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                
                spdData.RPT_AddBasicColumn("Weekly shortage", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Lack of CHIP", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Input required", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 16, 5);

                spdData.RPT_AddBasicColumn("Next week (WW" + Next_Week + ")", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("plan", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                if (ckbVisible.Checked == false)
                {
                    spdData.RPT_AddBasicColumn("input", 0, 22, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Possible quantity", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("rank 0", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("rank 1", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("rank 2", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("rank 3", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("D0", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Input Performance", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Performance remaining volume", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("D1", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 22, 9);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("input", 0, 22, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Possible quantity", 1, 22, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("rank 0", 1, 23, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("rank 1", 1, 24, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("rank 2", 1, 25, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("rank 3", 1, 26, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("D0", 1, 27, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Input Performance", 1, 28, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Performance remaining volume", 1, 29, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("D1", 1, 30, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 22, 9);
                }
                spdData.RPT_AddBasicColumn("WIP", 0, 31, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("TOTAL", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                if (cdvFactory.txtValue != "")
                {
                    if (cdvFactory.txtValue.Equals(GlobalVariable.gsTestDefaultFactory))
                    {
                        Oper_Group = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2("DESC")); // 해당 FACTORY의 공정 그룹을 가져옴(공정순서 거꾸로..)
                    }
                    else
                    {
                        Oper_Group = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2("ASC")); // 해당 FACTORY의 공정 그룹을 가져옴
                    }
                    
                    for (int i = 0; i < Oper_Group.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(Oper_Group.Rows[i][0].ToString(), 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                    spdData.RPT_MerageHeaderColumnSpan(0, 31, Oper_Group.Rows.Count+1);

                    spdData.RPT_AddBasicColumn("the day's performance", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("the day's performance", 1, spdData.ActiveSheet.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    spdData.RPT_MerageHeaderRowSpan(0, spdData.ActiveSheet.Columns.Count - 1, 2);
                                        
                    if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
                    {
                        spdData.RPT_AddBasicColumn("RE_MAIN", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderRowSpan(0, spdData.ActiveSheet.Columns.Count - 1, 2);

                        spdData.RPT_AddBasicColumn("ASSY WIP", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("A-TTL", 1, spdData.ActiveSheet.Columns.Count - 1, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("HMK3A", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("FINISH", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("MOLD", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("W/B", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        
                        spdData.RPT_AddBasicColumn("D/A", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("D/B", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.Columns.Count - 7, 7);

                    }

                    spdData.RPT_AddBasicColumn(DateArray[0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("plan", 1, spdData.ActiveSheet.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("goal", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("actual", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.Columns.Count - 3, 3);

                    spdData.RPT_AddBasicColumn(DateArray[1].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("plan", 1, spdData.ActiveSheet.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("goal", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("actual", 1, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.Columns.Count - 3, 3);

                    spdData.RPT_AddBasicColumn(DateArray[2].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("plan", 1, spdData.ActiveSheet.Columns.Count-1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    spdData.RPT_AddBasicColumn(DateArray[3].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("plan", 1, spdData.ActiveSheet.Columns.Count-1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    spdData.RPT_AddBasicColumn(DateArray[4].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("plan", 1, spdData.ActiveSheet.Columns.Count-1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    spdData.RPT_AddBasicColumn(DateArray[5].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("plan", 1, spdData.ActiveSheet.Columns.Count-1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    spdData.RPT_AddBasicColumn(DateArray[6].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("plan", 1, spdData.ActiveSheet.Columns.Count-1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    spdData.RPT_AddBasicColumn(DateArray[7].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("plan", 1, spdData.ActiveSheet.Columns.Count-1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "A.MAT_GRP_1", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "A.MAT_GRP_2", "MAT_GRP_2", "A.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "A.MAT_GRP_3", "MAT_GRP_3", "A.MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "A.MAT_GRP_4", "MAT_GRP_4", "A.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "A.MAT_GRP_5", "MAT_GRP_5", "A.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "A.MAT_GRP_6", "MAT_GRP_6", "A.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "A.MAT_GRP_7", "MAT_GRP_7", "A.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "A.MAT_GRP_8", "MAT_GRP_8", "A.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "A.MAT_CMF_10", "MAT_CMF_10", "A.MAT_CMF_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "A.MAT_ID", "MAT_ID", "A.MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "A.MAT_CMF_7", "MAT_CMF_7", "A.MAT_CMF_7", false);
        }
        #endregion


        #region 시간관련 함수
        private void GetWorkWeek()
        {
            DataTable dt = new DataTable();
            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());

            This_Week = dt.Rows[0][0].ToString();
            Next_Week = dt.Rows[1][0].ToString();
            Secon_Week = dt.Rows[2][0].ToString(); // (현재일+1일)의 주차구함
            yesterday_Week = dt.Rows[3][0].ToString(); // (현재일 -1일)의 주차구함
            Next_Year = dt.Rows[1][1].ToString(); // 다음주차의 해당 년도 구함
            Secon_Year = dt.Rows[2][1].ToString(); // (현재일+1일) 주차의 해당 년도 구함.
            yesterday_Year = dt.Rows[3][1].ToString(); // (현재일-1일)의 주차의 해당 년도 구함.

            if(This_Week.Length == 1)
            {
                This_Week = "0" + This_Week;
            }

            if(Next_Week.Length == 1)
            {
                Next_Week = "0" + Next_Week;
            }

            if (Secon_Week.Length == 1)
            {
                Secon_Week = "0" + Secon_Week;
            }

            if (Third_Week.Length == 1)
            {
                Third_Week = "0" + Third_Week;
            }

            if (yesterday_Week.Length == 1)
            {
                yesterday_Week = "0" + yesterday_Week;
            }

            dt.Dispose();
        }

        private void GetDayofWeek()
        {
            DateTime Now = DateTime.Now;
            Now_date = Now.ToString("yyyyMMdd");
            GetWorkWeek();
            GetLowDayofWeek();

            Now = Now.AddDays(-1);
            for (int i = 0; i < 8; i++)
            {
                DateArray[i] = Now.ToString("MM-dd") + "(" + Now.ToLongDateString().Substring(Now.ToLongDateString().Length - 3, 1) + ")";
                DateArray2[i] = Now.ToString("yyyyMMdd");
                Now = Now.AddDays(1);
            }
        }

        private void GetLowDayofWeek() // 해당주차내에서 기준일보다 작은일들,큰일들을 구함.
        {
            string Year;

            DateTime Select_date;
            Select_date = DateTime.Now;
            Year = Select_date.ToString("yyyy");

            // 기준일보다 작은일
            LowDayofWeek = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString4(Year, This_Week));

            // 기준일보다 큰일
            //HighDayofWeek = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString4(Year, Secon_Week, 2));
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
            string startday;
            string yesterday;
            string tomorrow;
            string month;
            string Today;
            string Today_1; // 금일(형식:yyyyMMdd)
            string Year;
            string lastMonth;
            string lastweek_lastday;
                        
            DateTime Select_date;
            Select_date = DateTime.Now;

            Today = Select_date.ToString("yyyyMMddhhmmss");
            Today_1 = Select_date.ToString("yyyyMMdd");
            Year = Select_date.ToString("yyyy");
            month = Select_date.ToString("yyyyMM");
            startday = month + "01"; 
            yesterday = Select_date.AddDays(-1).ToString("yyyyMMdd");
            tomorrow = Select_date.AddDays(+1).ToString("yyyyMMdd");
            lastMonth = Select_date.AddMonths(-1).ToString("yyyyMM"); // 지난달

            // 달의 마지막일 구하기
            DataTable dt1 = null;
            string lastday = DateTime.Now.ToString("yyyyMM").ToString();
            string ToDate = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + lastday + "', 'YYYYMM')), 'YYYYMMDD') FROM DUAL)";
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", ToDate);
            lastday = dt1.Rows[0][0].ToString(); // 달의 마지막날

            // 지난달 마지막일 구하기
            DataTable dt2 = null;
            string Last_Month_Last_day = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + lastMonth + "', 'YYYYMM')),'YYYYMMDD') FROM DUAL)";
            dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", Last_Month_Last_day);
            Last_Month_Last_day = dt2.Rows[0][0].ToString();

            dt1 = null;
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString3(Year, This_Week, Next_Week, Secon_Week, Next_Year, Secon_Year, yesterday_Week, yesterday_Year));
            string This_Week_First_Day = dt1.Rows[0][0].ToString();
            string This_Week_Last_Day = dt1.Rows[0][1].ToString(); // 금주 마지막 날
            string Next_Week_Last_Day = dt1.Rows[0][2].ToString();
            string Secon_Week_Last_Day = dt1.Rows[0][3].ToString(); // 현재일 + 1일 주차의 마지막날 구함
            string Third_Week_First_Day = dt1.Rows[0][4].ToString(); // (현재일+1일 주차) + 1주차 의 시작일
            string Third_Week_Last_Day = dt1.Rows[0][5].ToString(); // (현재일+1일 주차) + 1주차 의 마지막일
            string Yesterday_Week_First_Day = dt1.Rows[0][6].ToString(); // (현재일-1일 주차) 시작일
            string Yesterday_Week_Last_Day = dt1.Rows[0][7].ToString(); // (현재일-1일 주차) 마지막일

            // 2011-04-25-임종우 : 차주 시작일 추가
            string Next_Week_First_Day = dt1.Rows[0][8].ToString(); // 차주 시작일

            dt1.Dispose();

            lastweek_lastday = This_Week_First_Day.Insert(4, "-");
            lastweek_lastday = lastweek_lastday.Insert(7, "-");
            lastweek_lastday = DateTime.Parse(lastweek_lastday).AddDays(-1).ToString("yyyyMMdd");


            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            

            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond3);            
            strSqlString.Append("     , SUM(NVL(A.MON_PLAN,0)) AS MON_PLAN" + "\n");
            strSqlString.Append("     , SUM(NVL(A.SHIP_MON,0)) AS SHIP_MON" + "\n");
            strSqlString.Append("     , SUM(NVL(A.ASSY_MON,0)) AS ASSY_MON" + "\n");
            strSqlString.Append("     , SUM(NVL(A.LACK_MON,0)) AS LACK_MON" + "\n");
            strSqlString.Append("     , SUM(NVL(A.JINDO,0)) AS JINDO" + "\n");

            // 2011-11-22-임종우 : 금주 계획 로직 변경 -> 1. 월 : 월~일 계획    2. 화~일 : 전일 까지 실적 + 금일 이후 계획
            strSqlString.Append("     , DECODE(TO_CHAR(SYSDATE, 'D'), 2, SUM(NVL(C.PLAN_W1,0)), SUM(NVL(C.PLAN_W1_2,0)) + SUM(NVL(A.SHIP_THISWEEK_2,0))) AS PLAN_W1" + "\n");
            strSqlString.Append("     , SUM(NVL(A.SHIP_THISWEEK,0)) AS SHIP_THISWEEK" + "\n");
            strSqlString.Append("     , (DECODE(TO_CHAR(SYSDATE, 'D'), 2, SUM(NVL(C.PLAN_W1,0)), SUM(NVL(C.PLAN_W1_2,0)) + SUM(NVL(A.SHIP_THISWEEK_2,0)))) - SUM(NVL(A.SHIP_THISWEEK,0)) AS LACK_WEEK" + "\n");

            // 2011-06-21-임종우 : CHIP 부족, 투입 필요 추가 (김문한 요청)
            if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                strSqlString.Append("     , CASE WHEN (DECODE(TO_CHAR(SYSDATE, 'D'), 2, SUM(NVL(C.PLAN_W1,0)), SUM(NVL(C.PLAN_W1_2,0)) + SUM(NVL(A.SHIP_THISWEEK_2,0)))) - SUM(NVL(A.SHIP_THISWEEK,0)) - SUM(NVL(B.TOTAL,0)) <= 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE (DECODE(TO_CHAR(SYSDATE, 'D'), 2, SUM(NVL(C.PLAN_W1,0)), SUM(NVL(C.PLAN_W1_2,0)) + SUM(NVL(A.SHIP_THISWEEK_2,0)))) - SUM(NVL(A.SHIP_THISWEEK,0)) - SUM(NVL(B.TOTAL,0)) " + "\n");
                strSqlString.Append("       END AS LACK_CHIP " + "\n");
                strSqlString.Append("     , CASE WHEN (DECODE(TO_CHAR(SYSDATE, 'D'), 2, SUM(NVL(C.PLAN_W1,0)), SUM(NVL(C.PLAN_W1_2,0)) + SUM(NVL(A.SHIP_THISWEEK_2,0)))) - SUM(NVL(A.SHIP_THISWEEK,0)) - SUM(NVL(B.TOTAL,0)) + SUM(NVL(B.V0,0)) <= 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE (DECODE(TO_CHAR(SYSDATE, 'D'), 2, SUM(NVL(C.PLAN_W1,0)), SUM(NVL(C.PLAN_W1_2,0)) + SUM(NVL(A.SHIP_THISWEEK_2,0)))) - SUM(NVL(A.SHIP_THISWEEK,0)) - SUM(NVL(B.TOTAL,0)) + SUM(NVL(B.V0,0)) " + "\n");
                strSqlString.Append("       END AS LACK_IN " + "\n");
            }
            else
            {
                strSqlString.Append("     , CASE WHEN (DECODE(TO_CHAR(SYSDATE, 'D'), 2, SUM(NVL(C.PLAN_W1,0)), SUM(NVL(C.PLAN_W1_2,0)) + SUM(NVL(A.SHIP_THISWEEK_2,0)))) - SUM(NVL(A.SHIP_THISWEEK,0)) - SUM(NVL(B.TOTAL,0)) - SUM(NVL(J.TOTAL,0)) <= 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE (DECODE(TO_CHAR(SYSDATE, 'D'), 2, SUM(NVL(C.PLAN_W1,0)), SUM(NVL(C.PLAN_W1_2,0)) + SUM(NVL(A.SHIP_THISWEEK_2,0)))) - SUM(NVL(A.SHIP_THISWEEK,0)) - SUM(NVL(B.TOTAL,0)) - SUM(NVL(J.TOTAL,0)) " + "\n");
                strSqlString.Append("       END AS LACK_CHIP " + "\n");
                strSqlString.Append("     , CASE WHEN (DECODE(TO_CHAR(SYSDATE, 'D'), 2, SUM(NVL(C.PLAN_W1,0)), SUM(NVL(C.PLAN_W1_2,0)) + SUM(NVL(A.SHIP_THISWEEK_2,0)))) - SUM(NVL(A.SHIP_THISWEEK,0)) - SUM(NVL(B.TOTAL,0)) - SUM(NVL(J.TOTAL,0)) + SUM(NVL(J.V0,0)) <= 0 THEN 0" + "\n");
                strSqlString.Append("            ELSE (DECODE(TO_CHAR(SYSDATE, 'D'), 2, SUM(NVL(C.PLAN_W1,0)), SUM(NVL(C.PLAN_W1_2,0)) + SUM(NVL(A.SHIP_THISWEEK_2,0)))) - SUM(NVL(A.SHIP_THISWEEK,0)) - SUM(NVL(B.TOTAL,0)) - SUM(NVL(J.TOTAL,0)) + SUM(NVL(J.V0,0)) " + "\n");
                strSqlString.Append("       END AS LACK_IN " + "\n");
            }

            strSqlString.Append("     , SUM(NVL(C.PLAN_W2,0)) AS PLAN_W2" + "\n");
            strSqlString.Append("     , SUM(CASE WHEN ( ( NVL(C.PLAN_W1,0) - NVL(A.SHIP_THISWEEK_2,0) ) + NVL(C.PLAN_W2,0) - NVL(B.TOTAL_WIP,0) ) > 0" + "\n");
            strSqlString.Append("                THEN ( " + "\n");

            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory) // TEST 경우 재공을 거꾸로 표시 함. 그래서 창고(HMK3T)가 맨 뒷부분이므로 공정테이블의 count 를 세어 마지막거를 표현함.
            {                
                strSqlString.Append("                      CASE WHEN ( (NVL(C.PLAN_W1,0) - NVL(A.SHIP_THISWEEK_2,0) ) + NVL(C.PLAN_W2,0) - NVL(B.TOTAL_WIP,0) ) > NVL(B.V" + (Oper_Group.Rows.Count - 1) + ",0)" + "\n");
                strSqlString.Append("                           THEN NVL(B.V" + (Oper_Group.Rows.Count-1) + ",0)" + "\n");
            }
            else
            {
                strSqlString.Append("                      CASE WHEN ( (NVL(C.PLAN_W1,0) - NVL(A.SHIP_THISWEEK_2,0) ) + NVL(C.PLAN_W2,0) - NVL(B.TOTAL_WIP,0) ) > NVL(B.V0,0)" + "\n");
                strSqlString.Append("                           THEN NVL(B.V0,0)" + "\n");
            }

            strSqlString.Append("                           ELSE DECODE(A.MAT_GRP_3,'COB',NVL((NVL(C.PLAN_W1,0) - NVL(A.SHIP_THISWEEK_2,0) ) + NVL(C.PLAN_W2,0) - NVL(B.TOTAL_WIP,0), 0)," + "\n");
            strSqlString.Append("                                         (" + "\n");

            // ((계획-실적-WIP) * 102%) > HMK2,HMK3의 재공값 보다 클경우 HMK2,HMK3의 재공값 표시 ---> 2009.04.22 수정(한종태요청)
            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("                                          CASE WHEN NVL((NVL(C.PLAN_W1,0) - NVL(A.SHIP_THISWEEK_2,0) ) + NVL(C.PLAN_W2,0) - NVL(B.TOTAL_WIP,0), 0) * 1.02 > NVL(B.V" + (Oper_Group.Rows.Count - 1) + ",0)" + "\n");
                strSqlString.Append("                                               THEN NVL(B.V" + (Oper_Group.Rows.Count - 1) + ",0)" + "\n");
            }
            else
            {
                strSqlString.Append("                                          CASE WHEN NVL((NVL(C.PLAN_W1,0) - NVL(A.SHIP_THISWEEK_2,0) ) + NVL(C.PLAN_W2,0) - NVL(B.TOTAL_WIP,0), 0) * 1.02 > NVL(B.V0,0)" + "\n");
                strSqlString.Append("                                               THEN NVL(B.V0,0)" + "\n");
            }

            strSqlString.Append("                                               ELSE NVL((NVL(C.PLAN_W1,0) - NVL(A.SHIP_THISWEEK_2,0) ) + NVL(C.PLAN_W2,0) - NVL(B.TOTAL_WIP,0), 0) * 1.02" + "\n");
            strSqlString.Append("                                          END " + "\n");
            strSqlString.Append("                                         )" + "\n");
            strSqlString.Append("                                      )" + "\n");
            strSqlString.Append("                      END" + "\n");            
            strSqlString.Append("                     )" + "\n");
            strSqlString.Append("           END) POSSIBLE_QTY" + "\n");
            strSqlString.Append("     , SUM(NVL(D.NUMBER0,0)) AS NUMBER0" + "\n");
            strSqlString.Append("     , SUM(NVL(E.NUMBER1,0)) AS NUMBER1" + "\n");
            strSqlString.Append("     , SUM(NVL(F.NUMBER2,0)) AS NUMBER2" + "\n");
            strSqlString.Append("     , SUM(NVL(G.NUMBER3,0)) AS NUMBER3" + "\n");

            if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                if (ckbWafer.Checked == true)
                {
                    strSqlString.Append("     , SUM(CASE WHEN A.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(NVL(K.D0,0)/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13)),0) " + "\n");
                    strSqlString.Append("                ELSE NVL(K.D0,0)" + "\n");
                    strSqlString.Append("           END) AS D0" + "\n");
                    strSqlString.Append("     , SUM(CASE WHEN A.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(NVL(L.ISSUE_QTY,0)/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13)),0) " + "\n");
                    strSqlString.Append("                ELSE NVL(L.ISSUE_QTY,0)" + "\n");
                    strSqlString.Append("           END) AS ISSUE_QTY" + "\n");
                    strSqlString.Append("     , SUM(CASE WHEN A.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND((NVL(L.ISSUE_QTY,0) - NVL(K.D0,0))/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13)),0) " + "\n");
                    strSqlString.Append("                ELSE NVL(L.ISSUE_QTY,0) - NVL(K.D0,0)" + "\n");
                    strSqlString.Append("           END) AS IN_DEF" + "\n");
                    strSqlString.Append("     , SUM(CASE WHEN A.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(NVL(K.D1,0)/TO_NUMBER(DECODE(A.MAT_CMF_13,' ',1,A.MAT_CMF_13)),0) " + "\n");
                    strSqlString.Append("                ELSE NVL(K.D1,0)" + "\n");
                    strSqlString.Append("           END) AS D1" + "\n");
                }
                else
                {
                    strSqlString.Append("     , SUM(NVL(K.D0,0)) AS D0" + "\n");
                    strSqlString.Append("     , SUM(NVL(L.ISSUE_QTY,0)) AS ISSUE_QTY" + "\n");
                    strSqlString.Append("     , SUM(NVL(L.ISSUE_QTY,0)) - SUM(NVL(K.D0,0)) AS IN_DEF" + "\n");
                    strSqlString.Append("     , SUM(NVL(K.D1,0)) AS D1" + "\n");
                }
            }
            else
            {
                strSqlString.Append("     , 0" + "\n");
                strSqlString.Append("     , 0" + "\n");
                strSqlString.Append("     , 0" + "\n");
                strSqlString.Append("     , 0" + "\n");
            }

            strSqlString.Append("     , SUM(NVL(B.TOTAL,0)) AS TOTAL" + "\n");

            for (int i = 0; i < Oper_Group.Rows.Count;i++)
            {
                strSqlString.AppendFormat("     , SUM(NVL(B.V{0},0)) AS V{1}" + "\n", i, i);
            }

            strSqlString.Append("     , SUM(NVL(A.SHIP_TODAY,0)) AS SHIP_TODAY" + "\n");

            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("     , (DECODE(TO_CHAR(SYSDATE, 'D'), 2, SUM(NVL(C.PLAN_W1,0)), SUM(NVL(C.PLAN_W1_2,0)) + SUM(NVL(A.SHIP_THISWEEK_2,0)))) - SUM(NVL(A.SHIP_THISWEEK,0)) - SUM(NVL(B.TOTAL,0)) AS RE_MAIN " + "\n");
                strSqlString.Append("     , SUM(NVL(J.TOTAL,0)) " + "\n");
                strSqlString.Append("     , SUM(NVL(J.V4,0)) " + "\n");
                strSqlString.Append("     , SUM(NVL(J.V3,0)) " + "\n");
                strSqlString.Append("     , SUM(NVL(J.V2,0)) " + "\n");
                strSqlString.Append("     , SUM(NVL(J.V1,0)) " + "\n");
                // 2011-01-17 김민우: D/A 공정, DIE BANK와 DIE BANK 이후 ~ D/A로 구분 
                strSqlString.Append("     , SUM(NVL(J.V5,0)) " + "\n");
                strSqlString.Append("     , SUM(NVL(J.V0,0)) " + "\n");
            }

            strSqlString.Append("     , SUM(NVL(C.PLAN1,0)) AS PLAN1" + "\n");
            strSqlString.Append("     , SUM(NVL(H.GOAL_WEEK,0)) AS GOAL_WEEK" + "\n");
            strSqlString.Append("     , SUM(NVL(A.SHIP_YESTERDAY,0)) AS SHIP_YESTERDAY" + "\n");
            strSqlString.Append("     , SUM(NVL(C.PLAN2,0)) AS PLAN2" + "\n");

            // 2012-08-10-임종우 : 일별 목표는 일별 계획 - 일별 실적으로 변경. 실적이 더 많으면 -표시 되도록.. (김문한K 요청)
            strSqlString.Append("     , SUM(NVL(C.PLAN2,0)) - SUM(NVL(A.SHIP_TODAY,0)) AS GOAL_TODAY" + "\n");

            //strSqlString.Append("     , SUM(CASE WHEN (NVL(I.THIS_WEEK_PLAN,0) - NVL(A.SHIP_THISWEEK,0)) > 0 " + "\n");
            //strSqlString.Append("                THEN (NVL(I.THIS_WEEK_PLAN,0) - NVL(A.SHIP_THISWEEK,0)) " + "\n");
            //strSqlString.Append("                ELSE 0 END) GOAL_TODAY_WEEK " + "\n");
            strSqlString.Append("     , SUM(NVL(A.SHIP_TODAY,0)) AS SHIP_TODAY" + "\n");
            strSqlString.Append("     , SUM(NVL(C.PLAN3,0)) AS PLAN3" + "\n");
            strSqlString.Append("     , SUM(NVL(C.PLAN4,0)) AS PLAN4" + "\n");
            strSqlString.Append("     , SUM(NVL(C.PLAN5,0)) AS PLAN5" + "\n");
            strSqlString.Append("     , SUM(NVL(C.PLAN6,0)) AS PLAN6" + "\n");
            strSqlString.Append("     , SUM(NVL(C.PLAN7,0)) AS PLAN7" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT A.MAT_GRP_1, A.MAT_GRP_2, A.MAT_GRP_3, A.MAT_GRP_4, A.MAT_GRP_5, A.MAT_GRP_6, A.MAT_GRP_7, A.MAT_GRP_8, A.MAT_CMF_10, A.MAT_ID, A.MAT_CMF_7, A.MAT_CMF_13" + "\n");
            strSqlString.Append("             , SUM(B.PLAN_QTY) MON_PLAN" + "\n");

            if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                strSqlString.Append("             , SUM(C.ASSY_MON) ASSY_MON" + "\n");
            }
            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory) // TEST 경우 TO는 SHIP과 동일
            {
                strSqlString.Append("             , SUM(D.SHIP_MON) ASSY_MON" + "\n");
            }

            strSqlString.Append("             , SUM(C.ASSY_MONDAY) ASSY_MONDAY " + "\n");            
            strSqlString.Append("             , SUM(D.SHIP_MON) SHIP_MON " + "\n");            

            if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                strSqlString.Append("             , SUM(B.PLAN_QTY - NVL(C.ASSY_MON,0)) AS LACK_MON " + "\n");
                strSqlString.Append("             , ROUND(DECODE(SUM(NVL(B.PLAN_QTY,0)), 0, 0, SUM(NVL(C.ASSY_MON,0))/SUM(NVL(B.PLAN_QTY,0))*100 ),1) AS JINDO" + "\n");
            }
            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory) // TEST 경우 SHIP값으로..
            {
                strSqlString.Append("             , SUM(B.PLAN_QTY -  NVL(D.SHIP_MON,0)) AS LACK_MON                     " + "\n");
                strSqlString.Append("             , ROUND(DECODE(SUM(NVL(B.PLAN_QTY,0)), 0, 0, SUM(NVL(D.SHIP_MON,0))/SUM(NVL(B.PLAN_QTY,0))*100 ),1) AS JINDO" + "\n");
            }

            strSqlString.Append("             , SUM(C.ASSY_TODAY) SHIP_TODAY" + "\n");
            strSqlString.Append("             , SUM(C.ASSY_YESTERDAY) SHIP_YESTERDAY" + "\n");
            strSqlString.Append("             , SUM(C.ASSY_WEEK) SHIP_THISWEEK" + "\n");
            strSqlString.Append("             , SUM(C.ASSY_WEEK_2) SHIP_THISWEEK_2" + "\n");
            
            // 삼성 S-LSI 만 가져옴
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT *  " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF     " + "\n");
            strSqlString.Append("                 WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND MAT_GRP_9 = 'S-LSI' " + "\n");
            strSqlString.Append("               ) A " + "\n");
                        
            // 삼성 계획
            strSqlString.Append("             , (" + "\n");

            if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                strSqlString.Append("                SELECT MAT_ID, SUM(PLAN_QTY) AS PLAN_QTY " + "\n");
                strSqlString.Append("                  FROM ( " + "\n");
                strSqlString.Append("                        SELECT MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                          FROM CWIPPLNDAY " + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                strSqlString.Append("                           AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.AppendFormat("                           AND PLAN_DAY BETWEEN '{0}' AND '{1}'" + "\n", startday, lastday);
                strSqlString.Append("                           AND IN_OUT_FLAG = 'OUT'" + "\n");
                strSqlString.Append("                           AND CLASS = 'ASSY'" + "\n");
                strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
                strSqlString.Append("                         UNION ALL" + "\n");
                strSqlString.Append("                        SELECT MAT_ID " + "\n");
                strSqlString.Append("                             , SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                          FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                         WHERE 1=1 " + "\n");
                strSqlString.Append("                           AND CM_KEY_1 " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.Append("                           AND CM_KEY_2 = 'PROD'" + "\n");
                strSqlString.Append("                           AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                           AND LOT_TYPE = 'W' " + "\n");
                strSqlString.AppendFormat("                           AND WORK_DATE BETWEEN '{0}' AND '{1}'" + "\n", startday, lastweek_lastday);
                strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
                strSqlString.Append("                       ) " + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
            }

            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("                SELECT MAT_ID, SUM(PLAN_QTY) AS PLAN_QTY " + "\n");
                strSqlString.Append("                  FROM ( " + "\n");
                strSqlString.Append("                        SELECT MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                          FROM CWIPPLNDAY " + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                strSqlString.Append("                           AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.AppendFormat("                           AND PLAN_DAY BETWEEN '{0}' AND '{1}'" + "\n", startday, lastday);
                strSqlString.Append("                           AND IN_OUT_FLAG = 'IN'" + "\n");
                strSqlString.Append("                           AND CLASS = 'SLIS'" + "\n");
                strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
                strSqlString.Append("                         UNION ALL" + "\n");
                strSqlString.Append("                        SELECT MAT_ID " + "\n");
                strSqlString.Append("                             , SUM(SHP_QTY_1) PLAN_QTY " + "\n");
                strSqlString.Append("                          FROM VSUMWIPSHP " + "\n");
                strSqlString.Append("                         WHERE 1=1 " + "\n");
                strSqlString.Append("                           AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.Append("                           AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                           AND CM_KEY_2 = 'PROD'" + "\n");
                strSqlString.Append("                           AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.AppendFormat("                           AND WORK_DATE BETWEEN '{0}' AND '{1}'" + "\n", startday, lastweek_lastday);
                strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
                strSqlString.Append("                       ) " + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
            }

            strSqlString.Append("               ) B " + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_ID " + "\n");
            strSqlString.Append("                     , SUM(DECODE(WORK_DATE, TO_CHAR(SYSDATE + 2/24, 'YYYYMMDD'), NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0), 0)) AS ASSY_TODAY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + yesterday + "', NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0), 0)) AS ASSY_YESTERDAY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + This_Week_First_Day + "', NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0), 0)) AS ASSY_MONDAY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(WORK_MONTH, '" + month + "' , NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0), 0)) AS ASSY_MON " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + This_Week_First_Day + "' AND '" + This_Week_Last_Day + "' THEN NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0) " + "\n");
            strSqlString.Append("                                ELSE 0 " + "\n");
            strSqlString.Append("                       END) AS ASSY_WEEK " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + This_Week_First_Day + "' AND '" + yesterday + "' THEN NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0) " + "\n");
            strSqlString.Append("                                ELSE 0 " + "\n");
            strSqlString.Append("                       END) AS ASSY_WEEK_2 " + "\n");            
            strSqlString.Append("                  FROM RSUMFACMOV " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND CM_KEY_1 " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND CM_KEY_2 = 'PROD' " + "\n");
            strSqlString.Append("                   AND CM_KEY_3 LIKE 'P%' " + "\n");

            if (Convert.ToInt32(This_Week_First_Day) < Convert.ToInt32(Last_Month_Last_day))
            {
                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + This_Week_First_Day + "' AND '" + Today_1 + "'" + "\n");
            }
            else
            {
                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + Today_1 + "'" + "\n");
            }

            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("               ) C" + "\n");
            
            // 월 SHIP
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_ID " + "\n");
            strSqlString.Append("                     , SUM(SHP_QTY_1) AS SHIP_MON" + "\n");
            strSqlString.Append("                  FROM VSUMWIPSHP" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND CM_KEY_3 LIKE 'P%' " + "\n");
            strSqlString.Append("                   AND WORK_DATE BETWEEN '" + startday + "' AND '" + Today.Substring(0, 8) + "'" + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
            strSqlString.Append("               ) D" + "\n");            
            strSqlString.Append("         WHERE 1 = 1 " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+) " + "\n");
            strSqlString.Append("           AND A.MAT_ID = C.MAT_ID(+)  " + "\n");
            strSqlString.Append("           AND A.MAT_ID = D.MAT_ID(+) " + "\n");            
            strSqlString.Append("           AND A.MAT_GRP_1 = 'SE'" + "\n");
            strSqlString.Append("         GROUP BY A.MAT_GRP_1, A.MAT_GRP_2, A.MAT_GRP_3, A.MAT_GRP_4, A.MAT_GRP_5, A.MAT_GRP_6, A.MAT_GRP_7, A.MAT_GRP_8, A.MAT_CMF_10, A.MAT_ID, A.MAT_CMF_7, A.MAT_CMF_13" + "\n");
            strSqlString.Append("       ) A " + "\n");            
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("             , SUM(QTY) TOTAL " + "\n");

            if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
            {
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK2A',0,QTY)) TOTAL_WIP" + "\n");
            }

            if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK3T',0,QTY)) TOTAL_WIP" + "\n");
            }

            for (int i = 0; i < Oper_Group.Rows.Count; i++ )  // 공정 그룹의 재공을 가져옴
            {
                strSqlString.AppendFormat("             , SUM(DECODE(OPER_GRP_1, '{0}', QTY, 0)) V{1} " + "\n", Oper_Group.Rows[i][0].ToString(), i);
            }

            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT A.MAT_ID " + "\n");
            strSqlString.Append("                     , B.OPER_GRP_1 " + "\n");
            strSqlString.Append("                     , SUM(A.QTY_1)AS QTY  " + "\n");
            strSqlString.Append("                  FROM RWIPLOTSTS A " + "\n");
            strSqlString.Append("                     , MWIPOPRDEF B " + "\n");
            strSqlString.Append("                 WHERE 1 = 1 " + "\n");
            strSqlString.Append("                   AND A.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                   AND A.OPER = B.OPER " + "\n");
            strSqlString.Append("                   AND A.MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND A.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                   AND LOT_CMF_5 LIKE 'P%' " + "\n");
            strSqlString.Append("                 GROUP BY A.MAT_ID, B.OPER_GRP_1   " + "\n");
            strSqlString.Append("               )  " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("       ) B " + "\n");
            
            // 일별 세부 계획, 금주,차주 계획
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT FACTORY, MAT_ID" + "\n");
            strSqlString.Append("             , SUM(PLAN1)AS PLAN1" + "\n");
            strSqlString.Append("             , SUM(PLAN2)AS PLAN2" + "\n");
            strSqlString.Append("             , SUM(PLAN3)AS PLAN3" + "\n");
            strSqlString.Append("             , SUM(PLAN4)AS PLAN4" + "\n");
            strSqlString.Append("             , SUM(PLAN5)AS PLAN5" + "\n");
            strSqlString.Append("             , SUM(PLAN6)AS PLAN6" + "\n");
            strSqlString.Append("             , SUM(PLAN7)AS PLAN7" + "\n");
            strSqlString.Append("             , SUM(PLAN_W1)AS PLAN_W1" + "\n");
            strSqlString.Append("             , SUM(PLAN_W1_2)AS PLAN_W1_2" + "\n");
            strSqlString.Append("             , SUM(PLAN_W2)AS PLAN_W2" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT FACTORY, MAT_ID" + "\n");


            for (int i = 0; i < DateArray2.Length - 1; i++)
            {                
                strSqlString.AppendFormat("                     , DECODE(PLAN_DAY, '{0}', PLAN_QTY, 0) AS PLAN{1}" + "\n", DateArray2[i].ToString(), (i + 1));
            }

            strSqlString.Append("                     , CASE WHEN PLAN_DAY BETWEEN '" + This_Week_First_Day + "' AND '" + This_Week_Last_Day + "' THEN PLAN_QTY " + "\n");
            strSqlString.Append("                            ELSE 0 " + "\n");
            strSqlString.Append("                       END AS PLAN_W1 " + "\n");

            // 2011-11-22-임종우 : 금일 이후 금주 계획(금일 포함)
            strSqlString.Append("                     , CASE WHEN PLAN_DAY BETWEEN '" + Today_1 + "' AND '" + This_Week_Last_Day + "' THEN PLAN_QTY " + "\n");
            strSqlString.Append("                            ELSE 0 " + "\n");
            strSqlString.Append("                       END AS PLAN_W1_2 " + "\n");
            strSqlString.Append("                     , CASE WHEN PLAN_DAY BETWEEN '" + Next_Week_First_Day + "' AND '" + Next_Week_Last_Day + "' THEN PLAN_QTY " + "\n");
            strSqlString.Append("                            ELSE 0 " + "\n");
            strSqlString.Append("                       END AS PLAN_W2 " + "\n");            
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT B.*" + "\n");
            strSqlString.Append("                          FROM (" + "\n");           
            strSqlString.Append("                                SELECT * " + "\n");
            strSqlString.Append("                                  FROM CWIPPLNDAY " + "\n");
            strSqlString.Append("                                 WHERE 1=1" + "\n");
            strSqlString.Append("                                   AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                                   AND PLAN_DAY BETWEEN '" + This_Week_First_Day + "' AND '" + Next_Week_Last_Day + "'" + "\n");            

            if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                strSqlString.Append("                                   AND IN_OUT_FLAG = 'OUT'" + "\n");
                strSqlString.Append("                                   AND CLASS = 'ASSY'" + "\n");
            }

            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("                                   AND IN_OUT_FLAG = 'IN'" + "\n");
                strSqlString.Append("                                   AND CLASS = 'SLIS'" + "\n");
            }

            strSqlString.Append("                               ) B" + "\n");            
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY FACTORY, MAT_ID" + "\n");
            strSqlString.Append("       ) C  " + "\n");
            
            // 0순위 값 구함.
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT A.FACTORY, A.MAT_ID " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN (NVL(A.PLAN_QTY,0) - NVL(B.SHIP_QTY,0)) > 0 " + "\n");
            strSqlString.Append("                        THEN (NVL(A.PLAN_QTY,0) - NVL(B.SHIP_QTY,0)) " + "\n");
            strSqlString.Append("                        ELSE 0 END) NUMBER0 " + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT FACTORY, MAT_ID,SUM(PLAN_QTY) AS PLAN_QTY " + "\n");
            strSqlString.Append("                  FROM CWIPPLNDAY " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND PLAN_DAY BETWEEN '" + LowDayofWeek.Rows[0][0].ToString() + "' AND '" + LowDayofWeek.Rows[0][1].ToString() + "'\n");

            if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                strSqlString.Append("                   AND IN_OUT_FLAG = 'OUT'" + "\n");
                strSqlString.Append("                   AND CLASS = 'ASSY'" + "\n");
            }

            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("                   AND IN_OUT_FLAG = 'IN'" + "\n");
                strSqlString.Append("                   AND CLASS = 'SLIS'" + "\n");
            }

            strSqlString.Append("                 GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("               ) A " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT MAT_ID, SUM(CLS_QTY_1) AS SHIP_QTY " + "\n");
            strSqlString.Append("                  FROM VSUMWIPCLS " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                   AND CM_KEY_2 = 'PROD' " + "\n");
            strSqlString.Append("                   AND CM_KEY_3 LIKE 'P%' " + "\n");
            strSqlString.Append("                   AND WORK_DATE BETWEEN '" + LowDayofWeek.Rows[0][0].ToString() + "' AND '" + LowDayofWeek.Rows[0][1].ToString() + "'\n");

            if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                strSqlString.Append("                   AND OPER IN ('AZ010') " + "\n");
            }
            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("                   AND OPER IN ('TZ010') " + "\n");
            }

            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("               ) B " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND A.MAT_ID=B.MAT_ID(+) " + "\n");
            strSqlString.Append("         GROUP BY A.FACTORY, A.MAT_ID " + "\n");
            strSqlString.Append("       ) D  " + "\n");

            // 1순위 값 구함.
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT A.FACTORY, A.MAT_ID " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN (NVL(A.PLAN_QTY,0) - NVL(B.SHIP_QTY,0)) > 0 " + "\n");
            strSqlString.Append("                        THEN (NVL(A.PLAN_QTY,0) - NVL(B.SHIP_QTY,0)) " + "\n");
            strSqlString.Append("                        ELSE 0 END) NUMBER1 " + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT FACTORY, MAT_ID,SUM(PLAN_QTY) AS PLAN_QTY " + "\n");
            strSqlString.Append("                  FROM CWIPPLNDAY " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND PLAN_DAY = '" + Now_date + "'" + "\n");


            if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                strSqlString.Append("                   AND IN_OUT_FLAG = 'OUT'" + "\n");
                strSqlString.Append("                   AND CLASS = 'ASSY'" + "\n");
            }

            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("                   AND IN_OUT_FLAG = 'IN'" + "\n");
                strSqlString.Append("                   AND CLASS = 'SLIS'" + "\n");
            }

            strSqlString.Append("                 GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("               ) A  " + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_ID, SUM(CLS_QTY_1) AS SHIP_QTY " + "\n");
            strSqlString.Append("                  FROM VSUMWIPCLS " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                   AND CM_KEY_2 = 'PROD' " + "\n");
            strSqlString.Append("                   AND CM_KEY_3 LIKE 'P%' " + "\n");
            strSqlString.Append("                   AND WORK_DATE = '" + Now_date + "'" + "\n");

            if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                strSqlString.Append("                   AND OPER IN ('AZ009', 'AZ010') " + "\n");
            }
            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("                   AND OPER IN ('TZ010') " + "\n");
            }

            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("               ) B " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND A.MAT_ID=B.MAT_ID(+) " + "\n");
            strSqlString.Append("         GROUP BY A.FACTORY, A.MAT_ID " + "\n");
            strSqlString.Append("       ) E  " + "\n");

            // 2순위 값 구함.
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT A.FACTORY, A.MAT_ID " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN (NVL(A.PLAN_QTY,0) - NVL(B.SHIP_QTY,0)) > 0 " + "\n");
            strSqlString.Append("                        THEN (NVL(A.PLAN_QTY,0) - NVL(B.SHIP_QTY,0)) " + "\n");
            strSqlString.Append("                        ELSE 0 END) NUMBER2 " + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT FACTORY, MAT_ID,SUM(PLAN_QTY) AS PLAN_QTY " + "\n");
            strSqlString.Append("                  FROM CWIPPLNDAY " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND PLAN_DAY BETWEEN '" + tomorrow + "' AND '" + Secon_Week_Last_Day + "'\n");

            if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                strSqlString.Append("                   AND IN_OUT_FLAG = 'OUT'" + "\n");
                strSqlString.Append("                   AND CLASS = 'ASSY'" + "\n");
            }

            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("                   AND IN_OUT_FLAG = 'IN'" + "\n");
                strSqlString.Append("                   AND CLASS = 'SLIS'" + "\n");
            }

            strSqlString.Append("                 GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("               ) A  " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT MAT_ID, SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS SHIP_QTY " + "\n");
            strSqlString.Append("                  FROM RSUMFACMOV " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND CM_KEY_1 " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND CM_KEY_2 = 'PROD' " + "\n");
            strSqlString.Append("                   AND CM_KEY_3 LIKE 'P%' " + "\n");
            strSqlString.Append("                   AND WORK_DATE BETWEEN '" + tomorrow + "' AND '" + Secon_Week_Last_Day + "'\n");
            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("               ) B " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND A.MAT_ID=B.MAT_ID(+) " + "\n");
            strSqlString.Append("         GROUP BY A.FACTORY, A.MAT_ID " + "\n");
            strSqlString.Append("       ) F  " + "\n");

            // 3순위 값 구함.
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT FACTORY, MAT_ID,SUM(PLAN_QTY) AS NUMBER3 " + "\n");
            strSqlString.Append("          FROM CWIPPLNDAY" + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("           AND PLAN_DAY BETWEEN '" + Third_Week_First_Day + "' AND '" + Third_Week_Last_Day + "'\n");            

            if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                strSqlString.Append("           AND IN_OUT_FLAG = 'OUT'" + "\n");
                strSqlString.Append("           AND CLASS = 'ASSY'" + "\n");
            }

            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("           AND IN_OUT_FLAG = 'IN'" + "\n");
                strSqlString.Append("           AND CLASS = 'SLIS'" + "\n");
            }

            strSqlString.Append("         GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("       ) G  " + "\n");

            // 현재일-1일 의 목표값 ( (현재일-1일 주차의 시작일 ~ 어제)의 계획 - (현재일-1일 주차의 시작일 ~ 어제)의 실적 )
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT A.FACTORY, A.MAT_ID " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN (NVL(A.PLAN_QTY,0) - NVL(B.WEEK_SHIP_QTY,0)) > 0 " + "\n");
            strSqlString.Append("                        THEN (NVL(A.PLAN_QTY,0) - NVL(B.WEEK_SHIP_QTY,0)) " + "\n");
            strSqlString.Append("                        ELSE 0 END) GOAL_WEEK " + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT FACTORY, MAT_ID,SUM(PLAN_QTY) AS PLAN_QTY " + "\n");
            strSqlString.Append("                  FROM CWIPPLNDAY " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND PLAN_DAY BETWEEN '" + Yesterday_Week_First_Day + "' AND '" + yesterday + "'\n");

            if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                strSqlString.Append("                   AND IN_OUT_FLAG = 'OUT'" + "\n");
                strSqlString.Append("                   AND CLASS = 'ASSY'" + "\n");
            }

            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("                   AND IN_OUT_FLAG = 'IN'" + "\n");
                strSqlString.Append("                   AND CLASS = 'SLIS'" + "\n");
            }

            strSqlString.Append("                 GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("               ) A  " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT MAT_ID " + "\n"); 
            strSqlString.Append("                     , SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS WEEK_SHIP_QTY " + "\n");
            strSqlString.Append("                  FROM RSUMFACMOV " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND CM_KEY_1 " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND CM_KEY_2 = 'PROD' " + "\n");
            strSqlString.Append("                   AND CM_KEY_3 LIKE 'P%' " + "\n");
            strSqlString.Append("                   AND WORK_DATE BETWEEN '" + Yesterday_Week_First_Day + "' AND '" + yesterday + "'\n");            
            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("               ) B " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND A.MAT_ID=B.MAT_ID(+) " + "\n");
            strSqlString.Append("         GROUP BY A.FACTORY, A.MAT_ID " + "\n");
            strSqlString.Append("       ) H " + "\n");

            // 금주 시작~현재일까지의 계획
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT FACTORY, MAT_ID,SUM(PLAN_QTY) AS THIS_WEEK_PLAN " + "\n");
            strSqlString.Append("          FROM CWIPPLNDAY" + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("           AND PLAN_DAY BETWEEN '" + This_Week_First_Day + "' AND '" + Today_1 + "'\n");            
            
            if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                strSqlString.Append("           AND IN_OUT_FLAG = 'OUT'" + "\n");
                strSqlString.Append("           AND CLASS = 'ASSY'" + "\n");
            }

            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("           AND IN_OUT_FLAG = 'IN'" + "\n");
                strSqlString.Append("           AND CLASS = 'SLIS'" + "\n");
            }

            strSqlString.Append("         GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("       ) I  " + "\n");

            // HMKT 조회시 ASSY 재공 표시 부분
            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT MAT_ID" + "\n");
                strSqlString.Append("             , SUM(QTY) TOTAL" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN OPER_GRP_1 IN ('HMK2A')" + "\n");
                strSqlString.Append("                        THEN QTY" + "\n");
                strSqlString.Append("                        ELSE 0 END) V0" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN OPER_GRP_1 IN ('SMT','W/B')" + "\n");
                strSqlString.Append("                        THEN QTY" + "\n");
                strSqlString.Append("                        ELSE 0 END) V1" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN OPER_GRP_1 IN ('GATE','MOLD')" + "\n");
                strSqlString.Append("                        THEN QTY" + "\n");
                strSqlString.Append("                        ELSE 0 END) V2" + "\n");
                strSqlString.Append("             , SUM(CASE WHEN OPER_GRP_1 IN ('CURE','M/K','TRIM','TIN','S/B/A','SIG','AVI','V/I')" + "\n");
                strSqlString.Append("                        THEN QTY" + "\n");
                strSqlString.Append("                        ELSE 0 END) V3" + "\n");
                strSqlString.Append("             , SUM(DECODE(OPER_GRP_1,'HMK3A',QTY,0)) AS V4" + "\n");
                
                //2011-01-17 김민우
                strSqlString.Append("             , SUM(CASE WHEN OPER_GRP_1 IN ('B/G','SAW','S/P','D/A')" + "\n");
                strSqlString.Append("                        THEN QTY" + "\n");
                strSqlString.Append("                        ELSE 0 END) V5" + "\n");

                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT A.MAT_ID,B.OPER_GRP_1,SUM(A.QTY_1) AS QTY" + "\n");
                strSqlString.Append("                  FROM RWIPLOTSTS A" + "\n");
                strSqlString.Append("                     , MWIPOPRDEF B" + "\n");
                strSqlString.Append("                 WHERE A.FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                   AND A.FACTORY=B.FACTORY" + "\n");
                strSqlString.Append("                   AND A.OPER=B.OPER" + "\n");
                strSqlString.Append("                   AND A.MAT_VER=1" + "\n");
                strSqlString.Append("                   AND A.LOT_DEL_FLAG=' '" + "\n");
                strSqlString.Append("                   AND A.LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                   AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
                strSqlString.Append("                 GROUP BY A.MAT_ID,B.OPER_GRP_1" + "\n");
                strSqlString.Append("               )" + "\n");
                strSqlString.Append("         GROUP BY MAT_ID" + "\n");
                strSqlString.Append("       ) J  " + "\n");

            }

            // 2012-03-26-임종우 : ASSY 투입계획 부분 추가(권문석 요청)
            if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT FACTORY, MAT_ID " + "\n");
                strSqlString.Append("             , SUM(DECODE(PLAN_DAY, '" + Today_1 + "', PLAN_QTY, 0)) AS D0" + "\n");
                strSqlString.Append("             , SUM(DECODE(PLAN_DAY, '" + Today_1 + "', 0, PLAN_QTY)) AS D1" + "\n");
                strSqlString.Append("          FROM CWIPPLNDAY" + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");
                strSqlString.Append("           AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.Append("           AND PLAN_DAY BETWEEN '" + Today_1 + "' AND TO_DATE('" + Today_1 + "','YYYYMMDD')+1 " + "\n");
                strSqlString.Append("           AND IN_OUT_FLAG = 'IN'" + "\n");
                strSqlString.Append("           AND CLASS = 'ASSY'" + "\n");
                strSqlString.Append("         GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("       ) K  " + "\n");
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT MAT_ID " + "\n");
                strSqlString.Append("             , SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1) AS ISSUE_QTY" + "\n");
                strSqlString.Append("          FROM RSUMWIPMOV" + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");
                strSqlString.Append("           AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.Append("           AND WORK_DATE = '" + Today_1 + "'" + "\n");
                strSqlString.Append("           AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND OPER = 'A0000' " + "\n");
                strSqlString.Append("           AND MAT_ID LIKE 'SES%' " + "\n");
                strSqlString.Append("         GROUP BY MAT_ID " + "\n");
                strSqlString.Append("       ) L  " + "\n");
            }

            strSqlString.Append(" WHERE 1 = 1  " + "\n");
            strSqlString.Append("   AND A.MAT_ID = B.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND A.MAT_ID = C.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND A.MAT_ID = D.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND A.MAT_ID = E.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND A.MAT_ID = F.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND A.MAT_ID = G.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND A.MAT_ID = H.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND A.MAT_ID = I.MAT_ID(+) " + "\n");            

            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("   AND A.MAT_ID = J.MAT_ID(+) " + "\n");
            }
            else
            {
                strSqlString.Append("   AND A.MAT_ID = K.MAT_ID(+) " + "\n");
                strSqlString.Append("   AND A.MAT_ID = L.MAT_ID(+) " + "\n");
            }

             //상세 조회에 따른 SQL문 생성                                    
            strSqlString.Append("   AND A.MAT_GRP_1 = 'SE'" + "\n");

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

            strSqlString.Append("   AND A.MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");

            strSqlString.AppendFormat(" GROUP BY {0} " + "\n", QueryCond1);
            strSqlString.Append("HAVING SUM(NVL(A.MON_PLAN,0))" + "\n");
            strSqlString.Append("     + SUM(NVL(A.SHIP_MON,0))" + "\n");
            strSqlString.Append("     + SUM(NVL(A.ASSY_MON,0))" + "\n");
            strSqlString.Append("     + SUM(NVL(A.LACK_MON,0))" + "\n");
            strSqlString.Append("     + SUM(NVL(A.JINDO,0)) " + "\n");
            strSqlString.Append("     + SUM(NVL(C.PLAN_W1,0))" + "\n");
            strSqlString.Append("     + SUM(NVL(A.SHIP_THISWEEK,0))" + "\n");
            strSqlString.Append("     + SUM(NVL(C.PLAN_W2,0))" + "\n");
            strSqlString.Append("     + SUM(NVL(B.TOTAL,0))   " + "\n");
            strSqlString.Append("     + SUM(NVL(C.PLAN1,0))" + "\n");
            strSqlString.Append("     + SUM(NVL(C.PLAN2,0))" + "\n");
            strSqlString.Append("     + SUM(NVL(C.PLAN3,0))" + "\n");
            strSqlString.Append("     + SUM(NVL(C.PLAN4,0))" + "\n");
            strSqlString.Append("     + SUM(NVL(C.PLAN5,0))" + "\n");
            strSqlString.Append("     + SUM(NVL(C.PLAN6,0))" + "\n");
            strSqlString.Append("     + SUM(NVL(C.PLAN7,0))" + "\n");
            strSqlString.Append("     > 0 " + "\n");
            strSqlString.AppendFormat(" ORDER BY {0}" + "\n", QueryCond1);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private string MakeSqlString1() // 현재 주차와 다음주차 그리고 (현재일+1)의 주차, (현재일-1)의 주차를 가져옴
        {
            StringBuilder strSqlString = new StringBuilder();
            strSqlString.Append("SELECT PLAN_WEEK,PLAN_YEAR,SEQ " + "\n");
            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("       SELECT PLAN_WEEK,PLAN_YEAR,1 AS SEQ " + "\n");
            strSqlString.Append("         FROM MWIPCALDEF  " + "\n");
            strSqlString.Append("        WHERE CALENDAR_ID = 'SE' " + "\n");
            strSqlString.Append("          AND SYS_DATE = TO_CHAR(TO_DATE('" + Now_date + "', 'YYYYMMDD'), 'YYYYMMDD') " + "\n");
            strSqlString.Append("    UNION ALL " + "\n");
            strSqlString.Append("       SELECT PLAN_WEEK,PLAN_YEAR,2 AS SEQ " + "\n");
            strSqlString.Append("         FROM MWIPCALDEF  " + "\n");
            strSqlString.Append("        WHERE CALENDAR_ID = 'SE' " + "\n");
            strSqlString.Append("          AND SYS_DATE = TO_CHAR(TO_DATE('" + Now_date + "', 'YYYYMMDD')+7, 'YYYYMMDD') " + "\n");
            strSqlString.Append("    UNION ALL " + "\n");
            strSqlString.Append("       SELECT PLAN_WEEK,PLAN_YEAR,3 AS SEQ " + "\n");
            strSqlString.Append("         FROM MWIPCALDEF  " + "\n");
            strSqlString.Append("        WHERE CALENDAR_ID = 'SE' " + "\n");
            strSqlString.Append("          AND SYS_DATE = TO_CHAR(TO_DATE('" + Now_date + "', 'YYYYMMDD')+1, 'YYYYMMDD') " + "\n");
            strSqlString.Append("    UNION ALL " + "\n");
            strSqlString.Append("       SELECT PLAN_WEEK,PLAN_YEAR,4 AS SEQ " + "\n");
            strSqlString.Append("         FROM MWIPCALDEF  " + "\n");
            strSqlString.Append("        WHERE CALENDAR_ID = 'SE' " + "\n");
            strSqlString.Append("          AND SYS_DATE = TO_CHAR(TO_DATE('" + Now_date + "', 'YYYYMMDD')-1, 'YYYYMMDD') " + "\n");
            strSqlString.Append("        ) " + "\n");
            strSqlString.Append("ORDER BY SEQ" + "\n");
                        
            return strSqlString.ToString();
        }


        private string MakeSqlString2(string type)   // 공정그룹을 순서대로 가져옴
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT OPER_GRP_1" + "\n");
            strSqlString.Append("  FROM MWIPOPRDEF " + "\n");
            strSqlString.Append(" WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("   AND OPER_CMF_4 <> ' '    " + "\n");                        
            strSqlString.Append(" GROUP BY OPER_GRP_1" + "\n");
            strSqlString.Append(" ORDER BY TO_NUMBER(MIN(OPER_CMF_4)) " + type + "\n");
            return strSqlString.ToString();           
        }

        private string MakeSqlString3(string year, string from, string to, string secon, string toYear, string seconYear, string yesterday_Week, string yesterday_Year)   // 현재 주차의 첫째날,마지막날과 다음주차의 마지막날의 가져옴
        {
            StringBuilder strSqlString = new StringBuilder();
            strSqlString.Append("   SELECT This_Week_First_Day, This_Week_Last_Day, Next_Week_Last_Day, Secon_Week_Last_Day,  " + "\n");
            strSqlString.Append("          TO_CHAR(TO_DATE(Secon_Week_Last_Day,'YYYYMMDD')+1,'YYYYMMDD') AS Third_Week_First_Day,  " + "\n");
            strSqlString.Append("          TO_CHAR(TO_DATE(Secon_Week_Last_Day,'YYYYMMDD')+7,'YYYYMMDD') AS Third_Week_Last_Day,  " + "\n");
            strSqlString.Append("          yesterday_Week_First_Day, yesterday_Week_Last_Day, Next_Week_First_Day " + "\n");
            strSqlString.Append("   FROM  " + "\n");
            strSqlString.Append("   (" + "\n");
            strSqlString.Append("        SELECT MIN(A.SYS_DATE) AS This_Week_First_Day,  " + "\n");
            strSqlString.Append("               MAX(A.SYS_DATE) AS This_Week_Last_Day, " + "\n");
            strSqlString.Append("               MIN(B.SYS_DATE) AS Next_Week_First_Day, " + "\n");
            strSqlString.Append("               MAX(B.SYS_DATE) AS Next_Week_Last_Day, " + "\n");
            strSqlString.Append("               MAX(C.SYS_DATE) AS Secon_Week_Last_Day, " + "\n");
            strSqlString.Append("               MIN(D.SYS_DATE) AS yesterday_Week_First_Day, " + "\n");
            strSqlString.Append("               MAX(D.SYS_DATE) AS yesterday_Week_Last_Day " + "\n");
            strSqlString.Append("        FROM" + "\n");
            strSqlString.Append("        (" + "\n");
            strSqlString.Append("        SELECT * " + "\n");
            strSqlString.Append("          FROM MWIPCALDEF " + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND CALENDAR_ID = 'SE'" + "\n");
            strSqlString.Append("           AND PLAN_YEAR = TO_CHAR(TRUNC(SYSDATE,'IW')  ,'YYYY')" + "\n");
            strSqlString.AppendFormat("           AND PLAN_WEEK = '{0}'" + "\n", from);
            strSqlString.Append("         ORDER BY SYS_DATE" + "\n");
            strSqlString.Append("         ) A" + "\n");
            strSqlString.Append("       , (" + "\n");
            strSqlString.Append("        SELECT * " + "\n");
            strSqlString.Append("          FROM MWIPCALDEF " + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND CALENDAR_ID = 'SE'" + "\n");
            strSqlString.AppendFormat("           AND PLAN_YEAR = '{0}'" + "\n", toYear);
            strSqlString.AppendFormat("           AND PLAN_WEEK = '{0}'" + "\n", to);
            strSqlString.Append("         ORDER BY SYS_DATE" + "\n");
            strSqlString.Append("         ) B" + "\n");
            strSqlString.Append("       , (" + "\n");
            strSqlString.Append("        SELECT * " + "\n");
            strSqlString.Append("          FROM MWIPCALDEF " + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND CALENDAR_ID = 'SE'" + "\n");
            strSqlString.AppendFormat("           AND PLAN_YEAR = '{0}'" + "\n", Secon_Year);
            strSqlString.AppendFormat("           AND PLAN_WEEK = '{0}'" + "\n", secon);
            strSqlString.Append("         ORDER BY SYS_DATE" + "\n");
            strSqlString.Append("         ) C" + "\n");
            strSqlString.Append("       , (" + "\n");
            strSqlString.Append("        SELECT * " + "\n");
            strSqlString.Append("          FROM MWIPCALDEF " + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND CALENDAR_ID = 'SE'" + "\n");
            strSqlString.AppendFormat("           AND PLAN_YEAR = '{0}'" + "\n", yesterday_Year);
            strSqlString.AppendFormat("           AND PLAN_WEEK = '{0}'" + "\n", yesterday_Week);
            strSqlString.Append("         ORDER BY SYS_DATE" + "\n");
            strSqlString.Append("         ) D" + "\n");
            strSqlString.Append("   )" + "\n");

            return strSqlString.ToString();
        }

        private string MakeSqlString4(string year, string week) // 해당주차내에서 기준일보다 작은일 가져옴.
        {
            StringBuilder strSqlString = new StringBuilder();
            strSqlString.Append("       SELECT MIN(SYS_DATE), MAX(SYS_DATE) " + "\n");
            strSqlString.Append("         FROM MWIPCALDEF " + "\n");
            strSqlString.Append("        WHERE 1=1 " + "\n");
            strSqlString.Append("          AND CALENDAR_ID = 'SE' " + "\n");
            strSqlString.AppendFormat("          AND PLAN_YEAR = '{0}'" + "\n", year);
            strSqlString.AppendFormat("          AND PLAN_WEEK = '{0}'" + "\n", week);

            //if (type == 1) // 기준일보다 작은일들 구함.
            //{
                strSqlString.Append("          AND SYS_DATE < TO_CHAR(TO_DATE('" + Now_date + "', 'YYYYMMDD'), 'YYYYMMDD') " + "\n");
            //}
            //else // 기준일보다 큰일들을 구함.
            //{
            //    strSqlString.Append("          AND SYS_DATE > TO_CHAR(TO_DATE('" + Now_date + "', 'YYYYMMDD'), 'YYYYMMDD' " + "\n");
            //}

            strSqlString.Append("        ORDER BY SYS_DATE" + "\n");

            return strSqlString.ToString();
        }

        #endregion


        #region EVENT 처리
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            DataTable dt1 = null;

            StringBuilder strSqlString = new StringBuilder();
            
            // 계획값 기준 시간 가져오기(Plan Revision Time)
            //strSqlString.Append("SELECT DISTINCT(RESV_FIELD5) FROM CWIPPLNDAY");
            strSqlString.Append("SELECT TO_DATE(RESV_FIELD5,'YYYY-MM-DD HH24MISS') FROM CWIPPLNDAY");
  
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
            lblPlanTime.Text = dt1.Rows[0][0].ToString();

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

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 11, null, null, btnSort);

                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 1, true, Align.Center, VerticalAlign.Center);

                //// 칼럼 고정(필요하다면..)
                //if (ckbVisible.Checked == false)
                //{
                //    spdData.Sheets[0].FrozenColumnCount = 10;
                //}
                //else
                //{
                //    spdData.Sheets[0].FrozenColumnCount = 21;
                //}
                spdData.RPT_AutoFit(false);

                // YIELD 부분의  TOTAL값 및 SUB TOTAL을 계산하지 않고 직접 계산 
                string subtotal = null;

                for (int i = 0; i < spdData.ActiveSheet.Columns.Count; i++)
                {
                    if (spdData.ActiveSheet.Columns[i].Label == "Progress rate")
                    {
                        spdData.ActiveSheet.Cells[0, i].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 2].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 4].Value)) * 100).ToString();

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

                // 컬럼 변경시 전부 변경하여야 함.
                // 당일 계획에 따른 재공 색깔 표시 (목표>= 재공 : red, 목표<재공 : blue)
                if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
                {
                    for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                    {
                        int sum = 0;
                        int value = 0;

                        if (spdData.ActiveSheet.Cells[i, 55].BackColor.IsEmpty) // subtotal 부분 제외시키기 위함.
                        {
                            if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, 55].Value) != 0) // 목표값 0인것 제외
                            {
                                for (int y = 32; y < 43; y++) // 공정 컬럼번호 (순서는 반대로)
                                {
                                    value = Convert.ToInt32(spdData.ActiveSheet.Cells[i, y].Value);
                                    sum += value;

                                    if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, 55].Value) > sum)
                                    {
                                        spdData.ActiveSheet.Cells[i, y].ForeColor = Color.Red;

                                        /// 2010-08-30-임종우 : 금일 목표 대비 재공 색상 표시(임태성 요청)
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

                    //spdData.ActiveSheet.Cells[2, 47].BackColor = Color.Blue;
                    //spdData.ActiveSheet.Cells[2, 36].BackColor = Color.Yellow;
                    //spdData.ActiveSheet.Cells[2, 26].BackColor = Color.Red;
                }
                else
                {
                    for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                    {
                        int sum = 0;
                        int value = 0;

                        if (spdData.ActiveSheet.Cells[i, 55].BackColor.IsEmpty)
                        {
                            if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, 55].Value) != 0)
                            {
                                for (int y = 49; y > 31; y--)
                                {
                                    value = Convert.ToInt32(spdData.ActiveSheet.Cells[i, y].Value);
                                    sum += value;

                                    if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, 55].Value) > sum)
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
                    //spdData.ActiveSheet.Cells[2, 47].BackColor = Color.Blue;
                    //spdData.ActiveSheet.Cells[2, 41].BackColor = Color.Yellow;
                    //spdData.ActiveSheet.Cells[2, 26].BackColor = Color.Red;
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

                Condition.Append("Plan Revision Time : " + lblPlanTime.Text);

                if (cdvFactory.txtValue.Equals(GlobalVariable.gsAssyDefaultFactory))
                {
                    ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text + "(ASSY)", Condition.ToString(), null, true);
                    //(데이타, 제목(타이틀), 좌측문구, 우측문구, 자동사이즈조정)
                }
                else
                {
                    ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text + "(TEST)", Condition.ToString(), null, true);
                }
            }
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            //spdData.ExportExcel();            
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);            
            //cdvOper.sFactory = cdvFactory.txtValue;
        }
        #endregion
    }
}
