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
    public partial class PRD010220 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010220<br/>
        /// 클래스요약: 생산진척현황_TEST<br/>
        /// 작  성  자: 김태호<br/>
        /// 최초작성일: 2013-09-17<br/>
        /// 상세  설명: 생산진척현황_TEST<br/>
        /// 변경  내용: <br/>    
        /// 2013-12-17-임종우 : 주계획은 전체 주계획으로 변경, 일별계획 기준 : 토(D0)~금(D6) -> 월(D0)~일(D6) 로 변경 (김권수 요청)
        /// 2014-02-25-임종우 : 계획 값 중복오류 수정 - SAP 계획에서 삼성은 제외 함.
        /// 2014-02-26-임종우 : 3Day 출하 계획 -> 5Day 출하 계획으로 변경 (박민정 요청)
        /// 2014-04-02-임종우 : 주 계획 데이터 로직 수정 (박민정 요청)
        /// 2014-04-18-임종우 : 반품입고 부분 월 누계 실적에서 제외 (임태성 요청)
        /// 2014-05-28-임종우 : S-LSI, FABLESS 검색 기능 추가 (박민정 요청)
        /// 2014-06-17-임종우 : MACOM, FCI MAT_ID 변경, D1 추라 (박민정 요청)
        ///                   : D1 계획도 실적 반영한 계획으로 변경, 단 삼성은 OMS 계획만 표시 (박민정 요청)
        /// 2014-06-25-임종우 : D1 계획에 실적은 금일 실적 포함 되도록... (박민정 요청)
        /// 2014-07-02-임종우 : Freescale 의 경우 Part ID의 앞 5자리 + 뒤 3자리 조합하여 MAT_ID 사용 (박민정D 요청)
        /// 2015-01-19-임종우 : 중복 데이터 제공. 그냥 최종단에서 Distinct 사용 함. (박민정D 요청)
        /// 2016-04-20-임종우 : HMKT1, HMKE1 분리 (임태성K 요청)
        /// 2018-01-22-임종우 : SubTotal, GrandTotal 백분율 구하기 Function 변경
        /// 2018-04-19-임종우 : D1 Plan 기존 실적 반영한 로직 삭제. 순수 계획 값만 표시 (임태성차장 요청)
        /// </summary>
        GlobalVariable.FindWeek FindWeek_SOP_T = new GlobalVariable.FindWeek();
        GlobalVariable.FindWeek FindWeek_RTF = new GlobalVariable.FindWeek();

        string[] dayArry = new string[3];
        string[] dayArry2 = new string[3];
        decimal jindoPer;

        string strKpcs = "";    //Kpcs
        Formatter frmt = Formatter.Number;
        Visibles ViMonth = Visibles.True;
        Visibles ViWeek = Visibles.True;

        
        public PRD010220()
        {
            InitializeComponent();
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(GlobalVariable.gsTestDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsTestDefaultFactory;
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            GetWorkDay();
            spdData.RPT_ColumnInit();

            LabelTextChange();

            DateTime Select_date;

            Select_date = cdvDate.Value;
 

            string strWeek = FindWeek_SOP_T.ThisWeek.Substring(4,2);

            if (rdbMonth.Checked == true)
            {
                ViMonth = Visibles.True;
                ViWeek = Visibles.False;
            }
            else
            {
                ViMonth = Visibles.False;
                ViWeek = Visibles.True;
            }

            frmt = Formatter.Number;

            try
            {
                spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("MAJOR_CODE", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("FAMILY", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PACKAGE", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("TYPE_1", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("TYPE_2", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("LD_COUNT", 0, 6, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("DENSITY", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("GENERATION", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PIN_TYPE", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("MAT_ID", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("PKG_CODE", 0, 12, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("SAP_CODE", 0, 13, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("Classification", 0, 14, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);

                if (ckbDetail.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("Monthly Planning Standard", 0, 15, ViMonth, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("Monthly plan", 1, 15, ViMonth, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("Monthly Plan Rev", 1, 16, ViMonth, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("Cumulative Performance", 1, 17, ViMonth, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("residual quantity", 1, 18, ViMonth, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("Progress rate", 1, 19, ViMonth, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("Semi-finished product acquisition rate", 1, 20, ViMonth, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("a daily goal", 1, 21, ViMonth, Frozen.False, Align.Right, Merge.False, frmt, 70);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Monthly Planning Standard", 0, 15, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("Monthly plan", 1, 15, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("Monthly Plan Rev", 1, 16, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("Cumulative Performance", 1, 17, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("residual quantity", 1, 18, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("Progress rate", 1, 19, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("Semi-finished product acquisition rate", 1, 20, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("a daily goal", 1, 21, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                }

                spdData.RPT_MerageHeaderColumnSpan(0, 15, 7);

                spdData.RPT_AddBasicColumn("월 REMAIN", 0, 22, ViMonth, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("P/K", 1, 22, ViMonth, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("TEST", 1, 23, ViMonth, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("INPUT", 1, 24, ViMonth, Frozen.False, Align.Right, Merge.False, frmt, 70);

                spdData.RPT_MerageHeaderColumnSpan(0, 22, 3);

                if (ckbDetail.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("WW" + strWeek, 0, 25, ViWeek, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("Master plan", 1, 25, ViWeek, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("Cumulative Performance", 1, 26, ViWeek, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("residual quantity", 1, 27, ViWeek, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("Progress rate", 1, 28, ViWeek, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("Semi-finished product acquisition rate", 1, 29, ViWeek, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("a daily goal", 1, 30, ViWeek, Frozen.False, Align.Right, Merge.False, frmt, 70);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("WW" + strWeek, 0, 25, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("Master plan", 1, 25, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("Cumulative Performance", 1, 26, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("residual quantity", 1, 27, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("Progress rate", 1, 28, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("Semi-finished product acquisition rate", 1, 29, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("a daily goal", 1, 30, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                }

                spdData.RPT_MerageHeaderColumnSpan(0, 25, 6);

                spdData.RPT_AddBasicColumn("WW" + strWeek + " REMAIN", 0, 31, ViWeek, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("P/K", 1, 31, ViWeek, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("TEST", 1, 32, ViWeek, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("INPUT", 1, 33, ViWeek, Frozen.False, Align.Right, Merge.False, frmt, 70);

                spdData.RPT_MerageHeaderColumnSpan(0, 31, 3);

                if (ckbDetail.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("RTF", 0, 34, ViWeek, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("plan", 1, 34, ViWeek, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("actual", 1, 35, ViWeek, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("residual quantity", 1, 36, ViWeek, Frozen.False, Align.Right, Merge.False, frmt, 70);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("RTF", 0, 34, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("plan", 1, 34, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("actual", 1, 35, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("residual quantity", 1, 36, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                }

                spdData.RPT_MerageHeaderColumnSpan(0, 34, 3);

                spdData.RPT_AddBasicColumn("TEST Status", 0, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("a daily goal", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("TEST ASSIGN", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("count", 2, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("CAPA", 2, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("Realistic", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("Today's forecast", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);

                spdData.RPT_MerageHeaderColumnSpan(0, 37, 5);
                spdData.RPT_MerageHeaderColumnSpan(1, 38, 2);

                spdData.RPT_AddBasicColumn(Select_date.ToString("dd") + "일 출하 계획", 0, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("plan", 2, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("actual", 2, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 2, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);

                spdData.RPT_MerageHeaderColumnSpan(0, 42, 3);

                spdData.RPT_AddBasicColumn(Select_date.AddDays(1).ToString("MM-dd"), 0, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);

                spdData.RPT_AddBasicColumn("TEST WIP", 0, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("HMK4T", 1, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("MVP", 1, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("HOLD", 1, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("TEST", 1, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("HMK3T", 1, 50, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn("TTL", 1, 51, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);

                spdData.RPT_MerageHeaderColumnSpan(0, 46, 6);

                if (ckbDetail.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("ASSY WIP", 0, 52, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("FINISH", 1, 52, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("MOLD", 1, 53, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("WB", 1, 54, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("DA", 1, 55, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("B-LAB", 1, 56, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("STOCK", 1, 57, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("TOTAL", 1, 58, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("ASSY WIP", 0, 52, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("FINISH", 1, 52, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("MOLD", 1, 53, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("WB", 1, 54, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("DA", 1, 55, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("B-LAB", 1, 56, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("STOCK", 1, 57, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn("TOTAL", 1, 58, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                }

                spdData.RPT_MerageHeaderColumnSpan(0, 52, 7);

                spdData.RPT_AddBasicColumn("5DAY SHIPMENT PLAN", 0, 59, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn(Select_date.AddDays(1).ToString("MM-dd"), 1, 59, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn(Select_date.AddDays(2).ToString("MM-dd"), 1, 60, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn(Select_date.AddDays(3).ToString("MM-dd"), 1, 61, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn(Select_date.AddDays(4).ToString("MM-dd"), 1, 62, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                spdData.RPT_AddBasicColumn(Select_date.AddDays(5).ToString("MM-dd"), 1, 63, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);

                spdData.RPT_MerageHeaderColumnSpan(0, 59, 5);

                if (ckbDetail.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("3DAY SHIP Performance", 0, 64, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn(Select_date.AddDays(-3).ToString("MM-dd"), 1, 64, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn(Select_date.AddDays(-2).ToString("MM-dd"), 1, 65, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn(Select_date.AddDays(-1).ToString("MM-dd"), 1, 66, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("3DAY SHIP Performance", 0, 64, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn(Select_date.AddDays(-3).ToString("MM-dd"), 1, 64, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn(Select_date.AddDays(-2).ToString("MM-dd"), 1, 65, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn(Select_date.AddDays(-1).ToString("MM-dd"), 1, 66, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                }

                spdData.RPT_MerageHeaderColumnSpan(0, 64, 3);

                if (ckbDetail.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("3DAY TEST Performance", 0, 67, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn(Select_date.AddDays(-3).ToString("MM-dd"), 1, 67, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn(Select_date.AddDays(-2).ToString("MM-dd"), 1, 68, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn(Select_date.AddDays(-1).ToString("MM-dd"), 1, 69, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("3DAY TEST Performance", 0, 67, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn(Select_date.AddDays(-3).ToString("MM-dd"), 1, 67, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn(Select_date.AddDays(-2).ToString("MM-dd"), 1, 68, Visibles.False, Frozen.False, Align.Right, Merge.False, frmt, 70);
                    spdData.RPT_AddBasicColumn(Select_date.AddDays(-1).ToString("MM-dd"), 1, 69, Visibles.True, Frozen.False, Align.Right, Merge.False, frmt, 70);
                }

                spdData.RPT_MerageHeaderColumnSpan(0, 67, 3);

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
                spdData.RPT_MerageHeaderRowSpan(1, 30, 2);

                spdData.RPT_MerageHeaderRowSpan(1, 31, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 32, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 33, 2);

                spdData.RPT_MerageHeaderRowSpan(1, 34, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 35, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 36, 2);

                spdData.RPT_MerageHeaderRowSpan(1, 37, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 40, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 41, 2);

                spdData.RPT_MerageHeaderRowSpan(0, 42, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 45, 3);

                spdData.RPT_MerageHeaderRowSpan(1, 46, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 47, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 48, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 49, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 50, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 51, 2);

                spdData.RPT_MerageHeaderRowSpan(1, 52, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 53, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 54, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 55, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 56, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 57, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 58, 2);

                spdData.RPT_MerageHeaderRowSpan(1, 59, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 60, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 61, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 62, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 63, 2);

                spdData.RPT_MerageHeaderRowSpan(1, 64, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 65, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 66, 2);

                spdData.RPT_MerageHeaderRowSpan(1, 67, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 68, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 69, 2);

                spdData.RPT_ColumnConfigFromTable(btnSort);
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "DECODE(MAT.MAT_GRP_1,'SE','SEC','AB','ABOV','IM','iML','FC','FCI' , (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1)) AS CUSTOMER", "MAT.MAT_GRP_1", "DECODE(CUSTOMER, 'SEC', 1, 'HYNIX', 2, 'iML', 3, 'FCI', 4, 'IMAGIS', 5, 6), CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR_CODE", "MAT.MAT_GRP_9 AS MAJOR_CODE", "MAT.MAT_GRP_9", "MAJOR_CODE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT.MAT_GRP_2 AS FAMILY", "MAT.MAT_GRP_2", "FAMILY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT.MAT_GRP_3 AS PACKAGE", "MAT.MAT_GRP_3", "PACKAGE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE_1", "MAT.MAT_GRP_4 AS TYPE_1", "MAT.MAT_GRP_4", "TYPE_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE_2", "MAT.MAT_GRP_5 AS TYPE_2", "MAT.MAT_GRP_5", "TYPE_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD_COUNT", "MAT.MAT_GRP_6 AS LD_COUNT", "MAT.MAT_GRP_6", "LD_COUNT", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT.MAT_GRP_7 AS DENSITY", "MAT.MAT_GRP_7", "DENSITY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT.MAT_GRP_8 AS GENERATION", "MAT.MAT_GRP_8", "GENERATION", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "MAT.MAT_CMF_10 AS PIN_TYPE", "MAT.MAT_CMF_10", "PIN_TYPE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT.MAT_ID AS PRODUCT", "MAT.MAT_ID", "PRODUCT", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAT_ID", "MAT.CONV_MAT_ID AS CUST_DEVICE", "MAT.CONV_MAT_ID", "CUST_DEVICE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG_CODE", "MAT.MAT_CMF_11 AS PKG_CODE", "MAT.MAT_CMF_11", "PKG_CODE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SAP_CODE", "MAT.BASE_MAT_ID AS SAP_CODE", "MAT.BASE_MAT_ID", "SAP_CODE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Classification", "DECODE(SUBSTR(MAT.BASE_MAT_ID, 9, 2), '30', 'TURN KEY', '40', '외주', ' ') AS \"구분\"", "SUBSTR(MAT.BASE_MAT_ID, 9, 2)", "DECODE(\"구분\", 'TURN KEY', 1, '외주', 2, 3)", true);
        }
        #endregion

        #region 시간관련 함수
        private void GetWorkDay()
        {
            DateTime Now = cdvDate.Value;            
            FindWeek_SOP_T = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "SE");
            FindWeek_RTF = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "QC");

            //for (int i = 0; i < 8; i++)
            //{
            //    DateArray[i] = Now.ToString("MM-dd");
            //    DateArray2[i] = Now.ToString("yyyyMMdd");
            //    Now = Now.AddDays(1);
            //}

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
            string start_date;
            string yesterday;
            string date;
            string month;
            string year;
            string lastMonth;            

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            int remain = Convert.ToInt32(lblLastDay.Text.Substring(0, 2)) - Convert.ToInt32(lblToday.Text.Substring(0, 2)) + 1;
                        
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

            // 지난주차의 마지막일 가져오기
            dt1 = null;
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(year, Select_date.ToString("yyyyMMdd")));
            string Lastweek_lastday = dt1.Rows[0][0].ToString();

            if (ckbKpcs.Checked == true)
            {
                strKpcs = "1000";
            }
            else
            {
                strKpcs = "1";
            }

            strSqlString.Append("SELECT DISTINCT " + QueryCond1 + " " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.ORI_PLAN,0))/" + strKpcs + ",1) AS ORI_PLAN " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.MON_PLAN,0))/" + strKpcs + ",1) AS MON_PLAN " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.SHIP_MON,0))/" + strKpcs + ",1) AS SHIP_MON " + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(PLN.MON_PLAN,0))-SUM(NVL(PLN.SHIP_MON,0)))/" + strKpcs + ",1) AS \"월잔량\" " + "\n");
            strSqlString.Append("     , DECODE(SUM(NVL(PLN.MON_PLAN,0)),0,0, ROUND((SUM(NVL(PLN.SHIP_MON,0))/SUM(NVL(PLN.MON_PLAN,0)))*100,1)) AS JINDO " + "\n");
            strSqlString.Append("     , DECODE(SUM(NVL(PLN.MON_PLAN,0)),0,0, ROUND(((SUM(NVL(PLN.SHIP_MON,0)) + SUM(NVL(TEST_WIP.V0+TEST_WIP.V1+TEST_WIP.V2+TEST_WIP.V3+TEST_WIP.V4+TEST_WIP.V5+TEST_WIP.V6+TEST_WIP.V7+TEST_WIP.V8+TEST_WIP.V9+TEST_WIP.V10+TEST_WIP.V11,0)))/SUM(NVL(PLN.MON_PLAN,0)))*100,1)) AS OBT " + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(PLN.MON_PLAN,0)) - SUM(NVL(PLN.T_SHIP_MON,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "/" + strKpcs + ", 1) AS TARGET_DAY " + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(PLN.MON_PLAN,0))-(SUM(NVL(PLN.SHIP_MON,0))+SUM(NVL(TEST_WIP.V12+TEST_WIP.V20,0))))/" + strKpcs + ",1) AS RE_PK " + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(PLN.MON_PLAN,0))-(SUM(NVL(PLN.SHIP_MON,0))+SUM(NVL(TEST_WIP.V13+TEST_WIP.V21,0))))/" + strKpcs + ",1) AS RE_TEST " + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(PLN.MON_PLAN,0))-(SUM(NVL(PLN.SHIP_MON,0))+SUM(NVL(TEST_WIP.V0+TEST_WIP.V1+TEST_WIP.V2+TEST_WIP.V3+TEST_WIP.V4+TEST_WIP.V5+TEST_WIP.V6+TEST_WIP.V7+TEST_WIP.V8+TEST_WIP.V9+TEST_WIP.V10+TEST_WIP.V11,0))))/" + strKpcs + ",1) AS RE_INPUT " + "\n");
            strSqlString.Append("      " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.WEEK_PLAN,0))/" + strKpcs + ",1) AS WEEK_PLAN " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.SHIP_WEEK,0))/" + strKpcs + ",1) AS SHIP_WEEK " + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(PLN.WEEK_PLAN,0))-SUM(NVL(PLN.SHIP_WEEK,0)))/" + strKpcs + ",1) AS \"주잔량\" " + "\n");
            strSqlString.Append("     , DECODE(SUM(NVL(PLN.WEEK_PLAN,0)),0,0, ROUND((SUM(NVL(PLN.SHIP_WEEK,0))/SUM(NVL(PLN.WEEK_PLAN,0)))*100,1)) AS W_JINDO " + "\n");
            strSqlString.Append("     , DECODE(SUM(NVL(PLN.WEEK_PLAN,0)),0,0, ROUND(((SUM(NVL(PLN.SHIP_WEEK,0)) + SUM(NVL(TEST_WIP.V0+TEST_WIP.V1+TEST_WIP.V2+TEST_WIP.V3+TEST_WIP.V4+TEST_WIP.V5+TEST_WIP.V6+TEST_WIP.V7+TEST_WIP.V8+TEST_WIP.V9+TEST_WIP.V10+TEST_WIP.V11,0)))/SUM(NVL(PLN.WEEK_PLAN,0)))*100,1)) AS W_OBT " + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(PLN.WEEK_PLAN,0)) - SUM(NVL(PLN.T_SHIP_WEEK,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "/" + strKpcs + ", 1) AS W_TARGET_DAY " + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(PLN.WEEK_PLAN,0))-(SUM(NVL(PLN.SHIP_WEEK,0))+SUM(NVL(TEST_WIP.V12+TEST_WIP.V20,0))))/" + strKpcs + ",1) AS W_RE_PK  " + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(PLN.WEEK_PLAN,0))-(SUM(NVL(PLN.SHIP_WEEK,0))+SUM(NVL(TEST_WIP.V13+TEST_WIP.V21,0))))/" + strKpcs + ",1) AS W_RE_TEST  " + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(PLN.WEEK_PLAN,0))-(SUM(NVL(PLN.SHIP_WEEK,0))+SUM(NVL(TEST_WIP.V0+TEST_WIP.V1+TEST_WIP.V2+TEST_WIP.V3+TEST_WIP.V4+TEST_WIP.V5+TEST_WIP.V6+TEST_WIP.V7+TEST_WIP.V8+TEST_WIP.V9+TEST_WIP.V10+TEST_WIP.V11,0))))/" + strKpcs + ",1) AS W_RE_INPUT " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.PLAN_RTF,0))/" + strKpcs + ",1) AS PLAN_RTF " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.SHP_RTF_WEEK,0))/" + strKpcs + ",1) AS SHP_RTF_WEEK " + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(PLN.PLAN_RTF,0) - NVL(PLN.SHP_RTF_WEEK,0)))/" + strKpcs + ",1) AS RTF_DEF " + "\n");
            strSqlString.Append("      " + "\n");
            strSqlString.Append("      " + "\n");

            if(rdbMonth.Checked == true)
                strSqlString.Append("     , ROUND((SUM(NVL(PLN.MON_PLAN,0))-SUM(NVL(PLN.T_SHIP_MON,0))-(SUM(NVL(T_TEST_WIP.V0,0))+SUM(NVL(T_TEST_WIP.V1,0))))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "/" + strKpcs + ",1) AS TEST_OPER_TARGET_DAY " + "\n");
            else
                strSqlString.Append("     , ROUND((SUM(NVL(PLN.WEEK_PLAN,0)+NVL(PLN.T_SHIP_WEEK,0))-SUM(NVL(PLN.T_SHIP_WEEK,0))-(SUM(NVL(T_TEST_WIP.V0,0))+SUM(NVL(T_TEST_WIP.V1,0))))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "/" + strKpcs + ",1) AS TEST_OPER_TARGET_DAY " + "\n");

            strSqlString.Append("     , '' TEST_ASSIGN_댓수  " + "\n");
            strSqlString.Append("     , '' TEST_ASSIGN_CAPA " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.OPER_AO_D0,0))/" + strKpcs + ",1) AS OPER_AO_D0 " + "\n");
            strSqlString.Append("     , '' 금일예상  " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.D0_PLAN, 0))/" + strKpcs + ",1) AS D0_PLAN " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.SHIP_TODAY,0))/" + strKpcs + ",1) AS D0_SHP " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.D0_PLAN, 0)-NVL(SHIP_TODAY, 0))/" + strKpcs + ",1) DO_DEF " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.D1_PLAN,0))/" + strKpcs + ",1) AS D1 " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(TEST_WIP.V9 + TEST_WIP.V19,0))/" + strKpcs + ",1) AS TEST_HMK4T " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(TEST_WIP.V1+TEST_WIP.V4+TEST_WIP.V5+TEST_WIP.V6+TEST_WIP.V7+TEST_WIP.V8+TEST_WIP.V18,0))/" + strKpcs + ",1) AS TEST_MVP " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(TEST_WIP.V11,0))/" + strKpcs + ",1) AS TEST_HOLD " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(TEST_WIP.V10+TEST_WIP.V2+TEST_WIP.V3+TEST_WIP.V17,0))/" + strKpcs + ",1) AS TEST_TEST " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(TEST_WIP.V0+TEST_WIP.V16,0))/" + strKpcs + ",1) AS TEST_HMK3T " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(TEST_WIP.V0+TEST_WIP.V1+TEST_WIP.V2+TEST_WIP.V3+TEST_WIP.V4+TEST_WIP.V5+TEST_WIP.V6+TEST_WIP.V7+TEST_WIP.V8+TEST_WIP.V9+TEST_WIP.V10+TEST_WIP.V11+TEST_WIP.V16+TEST_WIP.V17+TEST_WIP.V18+TEST_WIP.V19,0))/" + strKpcs + ",1) AS TEST_TTL " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(ASSY_WIP.V9+ASSY_WIP.V15+ASSY_WIP.V10+ASSY_WIP.V12+ASSY_WIP.V11+ASSY_WIP.V13+ASSY_WIP.V14+ASSY_WIP.V16,0))/" + strKpcs + ",1) AS ASSY_FINISH " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(ASSY_WIP.V7+ASSY_WIP.V8,0))/" + strKpcs + ",1) AS ASSY_MOLD " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(ASSY_WIP.V6+ASSY_WIP.V17,0))/" + strKpcs + ",1) AS ASSY_WB " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(ASSY_WIP.V5+ASSY_WIP.V4+ASSY_WIP.V3,0))/" + strKpcs + ",1) AS ASSY_DA " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(ASSY_WIP.V1+ASSY_WIP.V2,0))/" + strKpcs + ",1) AS ASSY_B_LAB " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(ASSY_WIP.V0,0))/" + strKpcs + ",1) AS ASSY_STOCK " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(ASSY_WIP.V0+ASSY_WIP.V1+ASSY_WIP.V2+ASSY_WIP.V3+ASSY_WIP.V4+ASSY_WIP.V5+ASSY_WIP.V6+ASSY_WIP.V7+ASSY_WIP.V8+ASSY_WIP.V9+ASSY_WIP.V10+ASSY_WIP.V11+ASSY_WIP.V12+ASSY_WIP.V13+ASSY_WIP.V14+ASSY_WIP.V15+ASSY_WIP.V16+ASSY_WIP.V17,0))/" + strKpcs + ",1) AS ASSY_TTL " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.D1_PLAN,0))/" + strKpcs + ",1) AS D1 " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.D2_PLAN,0))/" + strKpcs + ",1) AS D2 " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.D3_PLAN,0))/" + strKpcs + ",1) AS D3 " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.D4_PLAN,0))/" + strKpcs + ",1) AS D4 " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.D5_PLAN,0))/" + strKpcs + ",1) AS D5 " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.SHIP_D1,0))/" + strKpcs + ",1) AS SHIP_D1  " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.SHIP_D2,0))/" + strKpcs + ",1) AS SHIP_D2  " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.SHIP_D3,0))/" + strKpcs + ",1) AS SHIP_D3  " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.OPER_AO_D1,0))/" + strKpcs + ",1) AS OPER_AO_D1 " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.OPER_AO_D2,0))/" + strKpcs + ",1) AS OPER_AO_D2 " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.OPER_AO_D3,0))/" + strKpcs + ",1) AS OPER_AO_D3 " + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT A.* " + "\n");
            strSqlString.Append("             , CASE WHEN MAT_GRP_1 = 'FC' AND INSTR(MAT_ID, '-') = 0 THEN MAT_ID " + "\n");
            strSqlString.Append("                    WHEN MAT_GRP_1 = 'FC' THEN SUBSTR(MAT_ID, 1, INSTR(MAT_ID, '-')-1) || MAT_CMF_11 " + "\n");
            strSqlString.Append("                    WHEN MAT_GRP_1 = 'FS' THEN SUBSTR(MAT_ID, 1, 5) || SUBSTR(MAT_ID, -3) " + "\n");
            strSqlString.Append("                    ELSE MAT_CMF_7 " + "\n");
            strSqlString.Append("               END CONV_MAT_ID " + "\n");
            strSqlString.Append("          FROM MWIPMATDEF A " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND FACTORY = '" +cdvFactory.Text + "'" + "\n");
            strSqlString.Append("           AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");

            if (ckbCOB.Checked == true)
                strSqlString.Append("           AND MAT_GRP_3 <> 'COB' " + "\n");

            if (cdvGubun.Text == "S-LSI")
            {
                strSqlString.Append("           AND MAT_GRP_1 = 'SE' " + "\n");
            }
            else if (cdvGubun.Text == "FABLESS")
            {
                strSqlString.Append("           AND MAT_GRP_1 <> 'SE' " + "\n");
            }

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

            strSqlString.Append("       ) MAT " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7  " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN.RESV_FIELD2)/MAT.MAT_CMF_13,0), SUM(PLAN.RESV_FIELD2)) AS MON_PLAN " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN.PLAN_QTY_TEST)/MAT.MAT_CMF_13,0), SUM(PLAN.PLAN_QTY_TEST)) AS ORI_PLAN " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP.SHIP_MON)/MAT.MAT_CMF_13,0), SUM(SHP.SHIP_MON)) AS SHIP_MON " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP.SHIP_WEEK)/MAT.MAT_CMF_13,0), SUM(SHP.SHIP_WEEK)) AS SHIP_WEEK " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP.T_SHIP_MON)/MAT.MAT_CMF_13,0), SUM(SHP.T_SHIP_MON)) AS T_SHIP_MON " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP.T_SHIP_WEEK)/MAT.MAT_CMF_13,0), SUM(SHP.T_SHIP_WEEK)) AS T_SHIP_WEEK " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP.SHIP_TODAY)/MAT.MAT_CMF_13,0), SUM(SHP.SHIP_TODAY)) AS SHIP_TODAY " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP.SHIP_D1)/MAT.MAT_CMF_13,0), SUM(SHP.SHIP_D1)) AS SHIP_D1 " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP.SHIP_D2)/MAT.MAT_CMF_13,0), SUM(SHP.SHIP_D2)) AS SHIP_D2 " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP.SHIP_D3)/MAT.MAT_CMF_13,0), SUM(SHP.SHIP_D3)) AS SHIP_D3 " + "\n");
            //strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(W_PLN.WW)/MAT.MAT_CMF_13,0), SUM(W_PLN.WW)) AS WEEK_PLAN " + "\n");
            strSqlString.Append("             , CASE WHEN MAT.MAT_ID LIKE 'SE%' AND MAT.MAT_GRP_3 = 'COB' THEN ROUND(SUM(NVL(W_PLN.WW,0) + NVL(SHP.T_SHIP_WEEK,0))/MAT.MAT_CMF_13,0) " + "\n");
            strSqlString.Append("                    WHEN MAT.MAT_ID LIKE 'SE%' THEN SUM(NVL(W_PLN.WW,0) + NVL(SHP.T_SHIP_WEEK,0)) " + "\n");
            strSqlString.Append("                    ELSE SUM(NVL(W_PLN.WW,0)) " + "\n");
            strSqlString.Append("               END AS WEEK_PLAN " + "\n");

            strSqlString.Append("             , CASE WHEN MAT.MAT_GRP_1 <> 'SE' THEN SUM(NVL(W_PLN.D0,0) + NVL(W_PLN.T_WW,0) - NVL(SHP.T_SHIP_WEEK,0))" + "\n");
            strSqlString.Append("                    WHEN MAT.MAT_GRP_3 = 'COB' THEN ROUND(SUM(W_PLN.D0)/MAT.MAT_CMF_13,0) " + "\n");
            strSqlString.Append("                    ELSE SUM(W_PLN.D0) " + "\n");
            strSqlString.Append("               END D0_PLAN " + "\n");
            //strSqlString.Append("             , CASE WHEN MAT.MAT_GRP_1 <> 'SE' THEN SUM(NVL(W_PLN.D0,0) + NVL(W_PLN.D1,0) + NVL(W_PLN.T_WW,0) - NVL(SHP.T_SHIP_WEEK,0) - NVL(SHP.SHIP_TODAY,0))" + "\n");
            //strSqlString.Append("                    WHEN MAT.MAT_GRP_3 = 'COB' THEN ROUND(SUM(W_PLN.D1)/MAT.MAT_CMF_13,0) " + "\n");
            //strSqlString.Append("                    ELSE SUM(W_PLN.D1) " + "\n");
            //strSqlString.Append("               END D1_PLAN " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(W_PLN.D1)/MAT.MAT_CMF_13,0), SUM(W_PLN.D1)) AS D1_PLAN " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(W_PLN.D2)/MAT.MAT_CMF_13,0), SUM(W_PLN.D2)) AS D2_PLAN " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(W_PLN.D3)/MAT.MAT_CMF_13,0), SUM(W_PLN.D3)) AS D3_PLAN " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(W_PLN.D4)/MAT.MAT_CMF_13,0), SUM(W_PLN.D4)) AS D4_PLAN " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(W_PLN.D5)/MAT.MAT_CMF_13,0), SUM(W_PLN.D5)) AS D5_PLAN " + "\n");
            strSqlString.Append("             , SUM(NVL(RTF.PLAN_RTF,0)) AS PLAN_RTF " + "\n");
            strSqlString.Append("             , SUM(NVL(SHP.SHP_RTF_WEEK,0)) AS SHP_RTF_WEEK " + "\n");
            strSqlString.Append("             , SUM(NVL(OPER_AO.QTY_D0,0)) AS OPER_AO_D0 " + "\n");
            strSqlString.Append("             , SUM(NVL(OPER_AO.QTY_D1,0)) AS OPER_AO_D1 " + "\n");
            strSqlString.Append("             , SUM(NVL(OPER_AO.QTY_D2,0)) AS OPER_AO_D2 " + "\n");
            strSqlString.Append("             , SUM(NVL(OPER_AO.QTY_D3,0)) AS OPER_AO_D3 " + "\n");
            strSqlString.Append("          FROM (  " + "\n");
            strSqlString.Append("                SELECT DISTINCT MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, MAT_CMF_10, MAT_ID, MAT_CMF_7, TO_NUMBER(DECODE(MAT_CMF_13, ' ', 1, '-', 1, MAT_CMF_13)) AS MAT_CMF_13 " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF  " + "\n");
            strSqlString.Append("                 WHERE 1=1  " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                   AND MAT_TYPE= 'FG'  " + "\n");
            strSqlString.Append("                   AND DELETE_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("               ) MAT  " + "\n");
            strSqlString.Append("             , (  " + "\n");
            strSqlString.Append("                SELECT MAT_ID,PLAN_QTY_TEST,PLAN_MONTH, RESV_FIELD2   " + "\n");
            strSqlString.Append("                  FROM (   " + "\n");
            strSqlString.Append("                        SELECT MAT_ID,SUM(PLAN_QTY_TEST) AS PLAN_QTY_TEST,PLAN_MONTH, SUM(RESV_FIELD2) AS RESV_FIELD2   " + "\n");
            strSqlString.Append("                          FROM (  " + "\n");

            if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
                strSqlString.Append("                                SELECT MAT_ID, SUM(PLAN_QTY_TEST) AS PLAN_QTY_TEST, PLAN_MONTH, SUM(TO_NUMBER(DECODE(RESV_FIELD2,' ',0,RESV_FIELD2))) AS RESV_FIELD2  " + "\n");
            else
                strSqlString.Append("                                SELECT MAT_ID, SUM(TO_NUMBER(DECODE(RESV_FIELD5,' ',0,RESV_FIELD5))) AS PLAN_QTY_TEST, PLAN_MONTH, SUM(TO_NUMBER(DECODE(RESV_FIELD6,' ',0,RESV_FIELD6))) AS RESV_FIELD2  " + "\n");

            strSqlString.Append("                                  FROM CWIPPLNMON  " + "\n");
            strSqlString.Append("                                 WHERE 1=1  " + "\n");
            strSqlString.Append("                                   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");  // 어차피 ASSY데이터를 TEST도 동일하게 복사하기에 각 factory 구분은 그냥 컬럼으로 한다.
            strSqlString.Append("                                   AND PLAN_MONTH = '" + month + "'  " + "\n");
            strSqlString.Append("                                 GROUP BY MAT_ID, PLAN_MONTH  " + "\n");
            strSqlString.Append("                               )  " + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID,PLAN_MONTH   " + "\n");
            strSqlString.Append("                       )  " + "\n");
            strSqlString.Append("               ) PLAN  " + "\n");
            strSqlString.Append("             , (    " + "\n");
            strSqlString.Append("                SELECT FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 3 THEN D0_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 4 THEN D0_QTY + D1_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 5 THEN D0_QTY + D1_QTY + D2_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 6 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 7 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 1 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY" + "\n");
            strSqlString.Append("                                ELSE 0" + "\n");
            strSqlString.Append("                           END) AS T_WW " + "\n");
            strSqlString.Append("                     , SUM(WW) AS WW " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 2 THEN D0_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 3 THEN D1_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 4 THEN D2_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 5 THEN D3_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 6 THEN D4_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 7 THEN D5_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 1 THEN D6_QTY" + "\n");
            strSqlString.Append("                                ELSE 0" + "\n");
            strSqlString.Append("                           END) AS D0 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 2 THEN D1_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 3 THEN D2_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 4 THEN D3_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 5 THEN D4_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 6 THEN D5_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 7 THEN D6_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 1 THEN D7_QTY" + "\n");
            strSqlString.Append("                                ELSE 0" + "\n");
            strSqlString.Append("                           END) AS D1 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 2 THEN D2_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 3 THEN D3_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 4 THEN D4_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 5 THEN D5_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 6 THEN D6_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 7 THEN D7_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 1 THEN D8_QTY" + "\n");
            strSqlString.Append("                                ELSE 0" + "\n");
            strSqlString.Append("                           END) AS D2 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 2 THEN D3_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 3 THEN D4_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 4 THEN D5_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 5 THEN D6_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 6 THEN D7_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 7 THEN D8_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 1 THEN D9_QTY" + "\n");
            strSqlString.Append("                                ELSE 0" + "\n");
            strSqlString.Append("                           END) AS D3 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 2 THEN D4_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 3 THEN D5_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 4 THEN D6_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 5 THEN D7_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 6 THEN D8_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 7 THEN D9_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 1 THEN D10_QTY" + "\n");
            strSqlString.Append("                                ELSE 0" + "\n");
            strSqlString.Append("                           END) AS D4 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 2 THEN D5_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 3 THEN D6_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 4 THEN D7_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 5 THEN D8_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 6 THEN D9_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 7 THEN D10_QTY" + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 1 THEN D11_QTY" + "\n");
            strSqlString.Append("                                ELSE 0" + "\n");
            strSqlString.Append("                           END) AS D5 " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.ThisWeek + "', D0_QTY, 0)) AS D0_QTY " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.ThisWeek + "', D1_QTY, 0)) AS D1_QTY " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.ThisWeek + "', D2_QTY, 0)) AS D2_QTY " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.ThisWeek + "', D3_QTY, 0)) AS D3_QTY " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.ThisWeek + "', D4_QTY, 0)) AS D4_QTY " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.ThisWeek + "', D5_QTY, 0)) AS D5_QTY " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.ThisWeek + "', D6_QTY, 0)) AS D6_QTY " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.NextWeek + "', D0_QTY, 0)) AS D7_QTY " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.NextWeek + "', D1_QTY, 0)) AS D8_QTY " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.NextWeek + "', D2_QTY, 0)) AS D9_QTY " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.NextWeek + "', D3_QTY, 0)) AS D10_QTY " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.NextWeek + "', D4_QTY, 0)) AS D11_QTY " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.NextWeek + "', D5_QTY, 0)) AS D12_QTY " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.NextWeek + "', D6_QTY, 0)) AS D13_QTY " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_T.ThisWeek + "', WW_QTY, 0)) AS WW " + "\n");
            strSqlString.Append("                          FROM RWIPPLNWEK " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                           AND GUBUN = '3' " + "\n");
            strSqlString.Append("                           AND PLAN_WEEK IN ('" + FindWeek_SOP_T.ThisWeek + "','" + FindWeek_SOP_T.NextWeek + "')" + "\n");
            strSqlString.Append("                           AND MAT_ID NOT LIKE 'SE%' " + "\n");
            strSqlString.Append("                         GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                       ) " + "\n");
            strSqlString.Append("                GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                UNION ALL  " + "\n");            
            strSqlString.Append("                SELECT FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN PLAN_DAY BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + Select_date.AddDays(-1).ToString("yyyyMMdd") + "' THEN PLAN_QTY ELSE 0 END) AS T_WW " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN PLAN_DAY BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + FindWeek_SOP_T.EndDay_ThisWeek + "' THEN PLAN_QTY ELSE 0 END) AS WW " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_DAY, '" + Select_date.AddDays(0).ToString("yyyyMMdd") + "', PLAN_QTY, 0)) AS D0 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_DAY, '" + Select_date.AddDays(1).ToString("yyyyMMdd") + "', PLAN_QTY, 0)) AS D1 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_DAY, '" + Select_date.AddDays(2).ToString("yyyyMMdd") + "', PLAN_QTY, 0)) AS D2 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_DAY, '" + Select_date.AddDays(3).ToString("yyyyMMdd") + "', PLAN_QTY, 0)) AS D3 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_DAY, '" + Select_date.AddDays(4).ToString("yyyyMMdd") + "', PLAN_QTY, 0)) AS D4 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_DAY, '" + Select_date.AddDays(5).ToString("yyyyMMdd") + "', PLAN_QTY, 0)) AS D5 " + "\n");

            // 금일조회 기준
            if (DateTime.Now.ToString("yyyyMMdd") == date)
            {
                strSqlString.Append("                  FROM CWIPPLNDAY " + "\n");
                strSqlString.Append("                 WHERE 1=1   " + "\n");
            }
            else// 금일이 아니면 스냅샷 떠놓은 테이블에서 가져옴.
            {
                strSqlString.Append("                  FROM CWIPPLNSNP@RPTTOMES " + "\n");
                strSqlString.Append("                 WHERE SNAPSHOT_DAY = '" + date + "'" + "\n");
            }

            strSqlString.Append("                   AND FACTORY  = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                   AND PLAN_DAY BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + FindWeek_SOP_T.EndDay_NextWeek + "' " + "\n");
            strSqlString.Append("                   AND IN_OUT_FLAG = 'IN' " + "\n");
            strSqlString.Append("                   AND CLASS = 'SLIS' " + "\n");
            strSqlString.Append("                 GROUP BY FACTORY, MAT_ID " + "\n");
            
            strSqlString.Append("               ) W_PLN " + "\n");
            strSqlString.Append("             , (  " + "\n");
            strSqlString.Append("                SELECT MAT_ID  " + "\n");
            strSqlString.Append("                     , SUM(SHIP_D1) AS SHIP_D1 " + "\n");
            strSqlString.Append("                     , SUM(SHIP_D2) AS SHIP_D2 " + "\n");
            strSqlString.Append("                     , SUM(SHIP_D3) AS SHIP_D3 " + "\n");
            strSqlString.Append("                     , SUM(SHIP_TODAY) AS SHIP_TODAY " + "\n");
            strSqlString.Append("                     , SUM(SHIP_MON) AS SHIP_MON " + "\n");
            strSqlString.Append("                     , SUM(T_SHIP_MON) AS T_SHIP_MON " + "\n");
            strSqlString.Append("                     , SUM(SHIP_WEEK) AS SHIP_WEEK " + "\n");
            strSqlString.Append("                     , SUM(T_SHIP_WEEK) AS T_SHIP_WEEK " + "\n");
            strSqlString.Append("                     , SUM(SHP_RTF_WEEK) AS SHP_RTF_WEEK " + "\n");            
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT MAT_ID  " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE = '" + Select_date.AddDays(-3).ToString("yyyyMMdd") + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) SHIP_D1 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE = '" + Select_date.AddDays(-2).ToString("yyyyMMdd") + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) SHIP_D2 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE = '" + Select_date.AddDays(-1).ToString("yyyyMMdd") + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) SHIP_D3 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE = '" + date + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) SHIP_TODAY " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) SHIP_MON " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE BETWEEN '" + start_date + "' AND '" + Select_date.AddDays(-1).ToString("yyyyMMdd") + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) T_SHIP_MON " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + date + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) SHIP_WEEK " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + Select_date.AddDays(-1).ToString("yyyyMMdd") + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) T_SHIP_WEEK " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') IN ('6','7','1') THEN 0 " + "\n");
            strSqlString.Append("                                        WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + date + "' THEN NVL(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1, 0)  " + "\n");
            strSqlString.Append("                                        ELSE 0  " + "\n");
            strSqlString.Append("                                   END) AS SHP_RTF_WEEK " + "\n");
            strSqlString.Append("                          FROM RSUMFACMOV " + "\n");
            strSqlString.Append("                         WHERE 1=1  " + "\n");
            strSqlString.Append("                           AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                           AND CM_KEY_2 = 'PROD'  " + "\n");

            if (cdvLotType.SelectedItem.ToString() != "ALL")
                strSqlString.Append("                           AND CM_KEY_3 LIKE '" + cdvLotType.SelectedItem.ToString() + "' " + "\n");

            strSqlString.Append("                           AND FACTORY <> 'RETURN'  " + "\n");
            strSqlString.Append("                           AND ((WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "') OR (WORK_DATE BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + date + "'))" + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID  " + "\n");
            strSqlString.Append("                         UNION ALL  " + "\n");
            strSqlString.Append("                        SELECT MAT_ID " + "\n");
            strSqlString.Append("                             , 0, 0, 0, 0 " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "' THEN -(S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1) ELSE 0 END) SHIP_MON " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE BETWEEN '" + start_date + "' AND '" + Select_date.AddDays(-1).ToString("yyyyMMdd") + "' THEN -(S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1) ELSE 0 END) T_SHIP_MON " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + date + "' THEN -(S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1) ELSE 0 END) SHIP_WEEK " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + Select_date.AddDays(-1).ToString("yyyyMMdd") + "' THEN -(S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1) ELSE 0 END) T_SHIP_WEEK " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') IN ('6','7','1') THEN 0 " + "\n");
            strSqlString.Append("                                        WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + date + "' THEN -(S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1) " + "\n");
            strSqlString.Append("                                        ELSE 0  " + "\n");
            strSqlString.Append("                                   END) AS SHP_RTF_WEEK " + "\n");
            strSqlString.Append("                          FROM RSUMFACMOV " + "\n");
            strSqlString.Append("                         WHERE 1=1  " + "\n");
            strSqlString.Append("                           AND FACTORY = CM_KEY_1 " + "\n");
            strSqlString.Append("                           AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                           AND CM_KEY_2 = 'PROD' " + "\n");

            if (cdvLotType.SelectedItem.ToString() != "ALL")
                strSqlString.Append("                           AND CM_KEY_3 LIKE '" + cdvLotType.SelectedItem.ToString() + "' " + "\n");

            if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
                strSqlString.Append("                           AND OPER = 'TZ010' " + "\n");
            else
                strSqlString.Append("                           AND OPER = 'EZ010' " + "\n");

            //strSqlString.Append("                           AND OPER IN ('TZ010','EZ010') " + "\n");

            strSqlString.Append("                           AND ((WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "') OR (WORK_DATE BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + date + "'))" + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID  " + "\n");
            strSqlString.Append("                       )  " + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID  " + "\n");
            strSqlString.Append("               ) SHP  " + "\n");
            strSqlString.Append("             , (  " + "\n");
            strSqlString.Append("                SELECT MAT_ID " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + Select_date.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'D') IN ('6','7','1') THEN 0 " + "\n");
            strSqlString.Append("                                WHEN PLAN_DATE BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + FindWeek_RTF.EndDay_ThisWeek + "' AND WW_RTF <> '-' THEN QTY_1  " + "\n");
            strSqlString.Append("                                ELSE 0  " + "\n");
            strSqlString.Append("                           END) AS PLAN_RTF " + "\n");
            strSqlString.Append("                  FROM CPLNSOPDAY@RPTTOMES  " + "\n");
            strSqlString.Append("                 WHERE 1=1  " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                   AND PLAN_DATE BETWEEN '" + FindWeek_SOP_T.StartDay_ThisWeek + "' AND '" + FindWeek_SOP_T.EndDay_ThisWeek + "'  " + "\n");
            strSqlString.Append("                 GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("               ) RTF  " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT MAT_ID " + "\n");
            strSqlString.Append("                     , SUM(QTY_D0) AS QTY_D0 " + "\n");
            strSqlString.Append("                     , SUM(QTY_D1) AS QTY_D1 " + "\n");
            strSqlString.Append("                     , SUM(QTY_D2) AS QTY_D2 " + "\n");
            strSqlString.Append("                     , SUM(QTY_D3) AS QTY_D3 " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT MAT_ID, OPER, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
            strSqlString.Append("                             , DECODE(WORK_DATE, '" + Select_date.ToString("yyyyMMdd") + "', SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1),0) QTY_D0 " + "\n");
            strSqlString.Append("                             , DECODE(WORK_DATE, '" + Select_date.AddDays(-3).ToString("yyyyMMdd") + "', SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1),0) QTY_D1 " + "\n");
            strSqlString.Append("                             , DECODE(WORK_DATE, '" + Select_date.AddDays(-2).ToString("yyyyMMdd") + "', SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1),0) QTY_D2 " + "\n");
            strSqlString.Append("                             , DECODE(WORK_DATE, '" + Select_date.AddDays(-1).ToString("yyyyMMdd") + "', SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1),0) QTY_D3 " + "\n");
            strSqlString.Append("                          FROM RSUMWIPMOV  " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                           AND OPER IN ('T0100', 'T0400', 'E0100', 'E0300') " + "\n");
            strSqlString.Append("                           AND WORK_DATE IN ('" + Select_date.AddDays(-3).ToString("yyyyMMdd") + "', '" + Select_date.AddDays(-2).ToString("yyyyMMdd") + "', '" + Select_date.AddDays(-1).ToString("yyyyMMdd") + "', '" + Select_date.ToString("yyyyMMdd") + "') " + "\n");

            if (cdvLotType.SelectedItem.ToString() != "ALL")
                strSqlString.Append("                           AND CM_KEY_3 LIKE '" + cdvLotType.SelectedItem.ToString() + "' " + "\n");

            strSqlString.Append("                         GROUP BY MAT_ID, OPER, LOT_TYPE, WORK_DATE, CM_KEY_3 " + "\n");
            strSqlString.Append("                       ) " + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("               ) OPER_AO " + "\n");
            strSqlString.Append("         WHERE 1 = 1  " + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = PLAN.MAT_ID(+)  " + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = W_PLN.MAT_ID(+) " + "\n");            
            strSqlString.Append("           AND MAT.MAT_ID = SHP.MAT_ID(+)  " + "\n");            
            strSqlString.Append("           AND MAT.MAT_ID = RTF.MAT_ID(+) " + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = OPER_AO.MAT_ID(+) " + "\n");
            strSqlString.Append("         GROUP BY MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7, MAT.MAT_CMF_13 " + "\n");
            strSqlString.Append("       ) PLN " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT LOT.MAT_ID, MAT.MAT_GRP_3  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK3T', DECODE(LOT.HOLD_FLAG, 'Y', 0, DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY)), 0)) V0  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'QA1', DECODE(LOT.HOLD_FLAG, 'Y', 0, DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY)), 0)) V1  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'CAS',  DECODE(LOT.HOLD_FLAG, 'Y', 0, DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY)), 0)) V2  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'OS',  DECODE(LOT.HOLD_FLAG, 'Y', 0, DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY)), 0)) V3  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'QA2', DECODE(LOT.HOLD_FLAG, 'Y', 0, DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY)), 0)) V4  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BAKE', DECODE(LOT.HOLD_FLAG, 'Y', 0, DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY)), 0)) V5  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'V/I', DECODE(LOT.HOLD_FLAG, 'Y', 0, DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY)), 0)) V6  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'TnR', DECODE(LOT.HOLD_FLAG, 'Y', 0, DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY)), 0)) V7 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'P/K', DECODE(LOT.HOLD_FLAG, 'Y', 0, DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY)), 0)) V8  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK4T', DECODE(LOT.HOLD_FLAG, 'Y', 0, DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY)), 0)) V9 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'TEST', DECODE(LOT.HOLD_FLAG, 'Y', 0, DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY)), 0)) V10 " + "\n");
            strSqlString.Append("             , SUM(DECODE(LOT.HOLD_FLAG, 'Y', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V11 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN (LOT.OPER BETWEEN 'T1300' AND 'TZ010') AND LOT.OPER <> 'T1300' THEN DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY) ELSE 0 END) AS V12 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN (LOT.OPER BETWEEN 'T0100' AND 'TZ010') AND LOT.OPER NOT IN ('T0100', 'T0400') THEN DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY) ELSE 0 END) AS V13 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN (LOT.OPER BETWEEN 'T0000' AND 'TZ010') AND LOT.OPER <> 'T0000' THEN DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY) ELSE 0 END) AS V14 " + "\n");
            strSqlString.Append("             , SUM(NVL(CASE WHEN LOT.OPER IN ('T0100', 'T0400') THEN DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY) ELSE 0 END,0)) AS V15 " + "\n");
            strSqlString.Append("             , SUM(NVL(CASE WHEN LOT.OPER IN ('E0000') THEN DECODE(LOT.HOLD_FLAG, 'Y', 0, DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY)) ELSE 0 END,0)) AS V16 " + "\n");
            strSqlString.Append("             , SUM(NVL(CASE WHEN LOT.OPER IN ('E0100', 'E0300') THEN DECODE(LOT.HOLD_FLAG, 'Y', 0, DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY)) ELSE 0 END,0)) AS V17 " + "\n");
            strSqlString.Append("             , SUM(NVL(CASE WHEN LOT.OPER IN ('E0200','E0350','E0400','E0500','E0600') THEN DECODE(LOT.HOLD_FLAG, 'Y', 0, DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY)) ELSE 0 END,0)) AS V18 " + "\n");
            strSqlString.Append("             , SUM(NVL(CASE WHEN LOT.OPER IN ('EZ010') THEN DECODE(LOT.HOLD_FLAG, 'Y', 0, DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY)) ELSE 0 END,0)) AS V19 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN (LOT.OPER BETWEEN 'E0500' AND 'EZ010') AND LOT.OPER <> 'E0500' THEN DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY) ELSE 0 END) AS V20   " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN (LOT.OPER BETWEEN 'E0100' AND 'EZ010') AND LOT.OPER NOT IN ('E0100', 'E0300') THEN DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY) ELSE 0 END) AS V21  " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN (LOT.OPER BETWEEN 'E0000' AND 'EZ010') AND LOT.OPER <> 'E0000' THEN DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY) ELSE 0 END) AS V22  " + "\n");
            strSqlString.Append("             , SUM(NVL(CASE WHEN LOT.OPER IN ('E0100', 'E0300') THEN DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY) ELSE 0 END,0)) AS V23 " + "\n");
            strSqlString.Append("          FROM (   " + "\n");
            strSqlString.Append("                SELECT A.FACTORY, A.MAT_ID, B.OPER_GRP_1, B.OPER_GRP_7, B.OPER_CMF_3, B.OPER, SUM(A.QTY_1) QTY, HOLD_FLAG, LOT_STATUS  " + "\n");

            if (date == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                  FROM RWIPLOTSTS A  " + "\n");
                strSqlString.Append("                     , MWIPOPRDEF B  " + "\n");
                strSqlString.Append("                 WHERE 1 = 1  " + "\n");
            }
            else
            {
                strSqlString.Append("                  FROM RWIPLOTSTS_BOH A   " + "\n");
                strSqlString.Append("                     , MWIPOPRDEF B   " + "\n");
                strSqlString.Append("                 WHERE 1 = 1  " + "\n");
                strSqlString.Append("                   AND A.CUTOFF_DT = '" + date + "' || '22' " + "\n");
            }

            strSqlString.Append("                   AND A.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                   AND A.MAT_VER = 1  " + "\n");
            strSqlString.Append("                   AND A.LOT_DEL_FLAG = ' '  " + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY  " + "\n");
            strSqlString.Append("                   AND A.OPER = B.OPER  " + "\n");

            if (cdvLotType.SelectedItem.ToString() != "ALL")
                strSqlString.Append("                   AND A.LOT_CMF_5 LIKE '" + cdvLotType.SelectedItem.ToString() + "' " + "\n");

            strSqlString.Append("                 GROUP BY A.MAT_ID, A.FACTORY, B.OPER_GRP_1, B.OPER_GRP_7, B.OPER_CMF_3, B.OPER, HOLD_FLAG, LOT_STATUS  " + "\n");
            strSqlString.Append("               ) LOT  " + "\n");
            strSqlString.Append("             , MWIPMATDEF MAT  " + "\n");
            strSqlString.Append("         WHERE 1 = 1  " + "\n");
            strSqlString.Append("           AND LOT.FACTORY = MAT.FACTORY  " + "\n");
            strSqlString.Append("           AND LOT.MAT_ID = MAT.MAT_ID  " + "\n");
            strSqlString.Append("           AND MAT.DELETE_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("           AND MAT.MAT_GRP_2 <> '-'  " + "\n");
            strSqlString.Append("         GROUP BY LOT.MAT_ID ,MAT.MAT_GRP_3 " + "\n");
            strSqlString.Append("       ) TEST_WIP " + "\n");


            strSqlString.Append("     , (  " + "\n");
            strSqlString.Append("        SELECT LOT.MAT_ID, MAT.MAT_GRP_3    " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN (LOT.OPER BETWEEN 'T0100' AND 'TZ010') AND LOT.OPER NOT IN ('T0100', 'T0400') THEN DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY) ELSE 0 END) AS V0  " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN (LOT.OPER BETWEEN 'E0100' AND 'EZ010') AND LOT.OPER NOT IN ('E0100', 'E0300') THEN DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY) ELSE 0 END) AS V1 " + "\n");
            strSqlString.Append("          FROM (    " + "\n");
            strSqlString.Append("                SELECT A.FACTORY, A.MAT_ID, B.OPER_GRP_1, B.OPER_GRP_7, B.OPER_CMF_3, B.OPER, SUM(A.QTY_1) QTY, HOLD_FLAG, LOT_STATUS   " + "\n");
            strSqlString.Append("                  FROM RWIPLOTSTS_BOH A   " + "\n");
            strSqlString.Append("                     , MWIPOPRDEF B   " + "\n");
            strSqlString.Append("                 WHERE 1 = 1 " + "\n");
            strSqlString.Append("                   AND A.CUTOFF_DT = '" + Select_date.AddDays(-1).ToString("yyyyMMdd") + "' || '22' " + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                   AND A.MAT_VER = 1   " + "\n");
            strSqlString.Append("                   AND A.LOT_DEL_FLAG = ' '   " + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY   " + "\n");
            strSqlString.Append("                   AND A.OPER = B.OPER   " + "\n");

            if (cdvLotType.SelectedItem.ToString() != "ALL")
                strSqlString.Append("                   AND A.LOT_CMF_5 LIKE '" + cdvLotType.SelectedItem.ToString() + "' " + "\n");

            strSqlString.Append("                 GROUP BY A.MAT_ID, A.FACTORY, B.OPER_GRP_1, B.OPER_GRP_7, B.OPER_CMF_3, B.OPER, HOLD_FLAG, LOT_STATUS   " + "\n");
            strSqlString.Append("               ) LOT   " + "\n");
            strSqlString.Append("             , MWIPMATDEF MAT   " + "\n");
            strSqlString.Append("         WHERE 1 = 1   " + "\n");
            strSqlString.Append("           AND LOT.FACTORY = MAT.FACTORY   " + "\n");
            strSqlString.Append("           AND LOT.MAT_ID = MAT.MAT_ID   " + "\n");
            strSqlString.Append("           AND MAT.DELETE_FLAG <> 'Y'   " + "\n");
            strSqlString.Append("           AND MAT.MAT_GRP_2 <> '-'   " + "\n");
            strSqlString.Append("         GROUP BY LOT.MAT_ID ,MAT.MAT_GRP_3  " + "\n");
            strSqlString.Append("       ) T_TEST_WIP " + "\n");




            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT LOT.MAT_ID, MAT.MAT_GRP_3  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK2A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V0  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'B/G', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V1  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SAW', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V2  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V3 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SMT', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V4   " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'D/A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V5  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'W/B', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V6  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'MOLD', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V7  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'CURE', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V8  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'M/K', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V9 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'TRIM', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V10  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'TIN', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V11 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'S/B/A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V12  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SIG', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V13 " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'AVI', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V14  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'V/I', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V15  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK3A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V16  " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'GATE', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V17 " + "\n");
            strSqlString.Append("          FROM (   " + "\n");
            strSqlString.Append("                SELECT FACTORY, MAT_ID, OPER_GRP_1  " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1)  " + "\n");
            strSqlString.Append("                                ELSE QTY_1  " + "\n");
            strSqlString.Append("                           END) QTY  " + "\n");
            strSqlString.Append("                  FROM (  " + "\n");
            strSqlString.Append("                        SELECT A.FACTORY, A.MAT_ID, B.OPER_GRP_1, B.OPER, A.QTY_1  " + "\n");
            strSqlString.Append("                             , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') AND KEY_1 = A.MAT_ID) AS COMP_CNT   " + "\n");

            if (date == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                          FROM RWIPLOTSTS A   " + "\n");
                strSqlString.Append("                             , MWIPOPRDEF B   " + "\n");
                strSqlString.Append("                         WHERE 1 = 1  " + "\n");
            }
            else
            {
                strSqlString.Append("                          FROM RWIPLOTSTS_BOH A   " + "\n");
                strSqlString.Append("                             , MWIPOPRDEF B   " + "\n");
                strSqlString.Append("                         WHERE 1 = 1  " + "\n");
                strSqlString.Append("                           AND A.CUTOFF_DT = '" + date + "' || '22' " + "\n");
            }

            strSqlString.Append("                           AND A.FACTORY = B.FACTORY(+)   " + "\n");
            strSqlString.Append("                           AND A.OPER = B.OPER(+)   " + "\n");
            strSqlString.Append("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'   " + "\n");
            strSqlString.Append("                           AND A.LOT_TYPE = 'W'  " + "\n");
            strSqlString.Append("                           AND A.LOT_DEL_FLAG = ' '  " + "\n");

            if (cdvLotType.SelectedItem.ToString() != "ALL")
                strSqlString.Append("                           AND A.LOT_CMF_5 LIKE '" + cdvLotType.SelectedItem.ToString() + "' " + "\n");

            strSqlString.Append("                       )  " + "\n");
            strSqlString.Append("                 GROUP BY FACTORY, MAT_ID, OPER_GRP_1  " + "\n");
            strSqlString.Append("               ) LOT  " + "\n");
            strSqlString.Append("             , MWIPMATDEF MAT  " + "\n");
            strSqlString.Append("         WHERE 1 = 1  " + "\n");
            strSqlString.Append("           AND LOT.FACTORY = MAT.FACTORY  " + "\n");
            strSqlString.Append("           AND LOT.MAT_ID = MAT.MAT_ID  " + "\n");
            strSqlString.Append("           AND MAT.DELETE_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("           AND MAT.MAT_GRP_2 <> '-'  " + "\n");
            strSqlString.Append("         GROUP BY LOT.MAT_ID ,MAT.MAT_GRP_3 " + "\n");
            strSqlString.Append("       ) ASSY_WIP " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = PLN.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = TEST_WIP.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = ASSY_WIP.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = T_TEST_WIP.MAT_ID(+) " + "\n");
            strSqlString.Append(" GROUP BY " + QueryCond2 + " "+ "\n");
            strSqlString.Append("HAVING ( " + "\n");

            if(rdbMonth.Checked == true)
                strSqlString.Append("        SUM(NVL(PLN.MON_PLAN,0)) + SUM(NVL(PLN.SHIP_MON,0)) + " + "\n");
            else if(rdbWeek.Checked == true)
                strSqlString.Append("        SUM(NVL(PLN.WEEK_PLAN,0) + NVL(PLN.T_SHIP_WEEK,0)) + SUM(NVL(PLN.SHIP_WEEK,0)) + " + "\n");

            strSqlString.Append("        SUM(NVL(TEST_WIP.V0+TEST_WIP.V1+TEST_WIP.V2+TEST_WIP.V3+TEST_WIP.V4+TEST_WIP.V5+TEST_WIP.V6+TEST_WIP.V7+TEST_WIP.V8+TEST_WIP.V9+TEST_WIP.V10+TEST_WIP.V11+TEST_WIP.V16+TEST_WIP.V17+TEST_WIP.V18+TEST_WIP.V19,0)) + " + "\n");
            strSqlString.Append("        SUM(NVL(ASSY_WIP.V0+ASSY_WIP.V1+ASSY_WIP.V2+ASSY_WIP.V3+ASSY_WIP.V4+ASSY_WIP.V5+ASSY_WIP.V6+ASSY_WIP.V7+ASSY_WIP.V8+ASSY_WIP.V9+ASSY_WIP.V10+ASSY_WIP.V11+ASSY_WIP.V12+ASSY_WIP.V13+ASSY_WIP.V14+ASSY_WIP.V15+ASSY_WIP.V16+ASSY_WIP.V17,0)) " + "\n");
            strSqlString.Append("       ) <> 0 " + "\n");
            
            strSqlString.Append(" ORDER BY " + QueryCond3 + " " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private string MakeSqlStringForPopup_1()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT DECODE(MAT.MAT_GRP_1,'SE','SEC','AB','ABOV','IM','iML','FC','FCI' , (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1)) AS CUSTOMER, MAT.MAT_GRP_3 AS PACKAGE, MAT.MAT_GRP_6 AS LD_COUNT, MAT.MAT_ID AS PART, TEST_WIP.OPER AS STEP " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(TEST_WIP.LOT_QTY,0))/1,1) AS LOT_QTY " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(TEST_WIP.HOLD_QTY,0))/1,1) AS HOLD_QTY " + "\n");
            strSqlString.Append("  FROM MWIPMATDEF MAT " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT LOT.MAT_ID, LOT.OPER, MAT.MAT_GRP_3 " + "\n");
            strSqlString.Append("             , SUM(DECODE(LOT.HOLD_FLAG, 'Y', LOT_QTY, 0)) AS LOT_QTY " + "\n");
            strSqlString.Append("             , SUM(DECODE(LOT.HOLD_FLAG, 'Y', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) HOLD_QTY " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT A.FACTORY, A.MAT_ID, B.OPER_GRP_1, B.OPER_GRP_7, B.OPER_CMF_3, B.OPER, SUM(A.QTY_1) QTY, HOLD_FLAG, LOT_STATUS, COUNT(A.LOT_ID) AS LOT_QTY " + "\n");

            if (cdvDate.Value.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                          FROM RWIPLOTSTS A   " + "\n");
                strSqlString.Append("                             , MWIPOPRDEF B   " + "\n");
                strSqlString.Append("                         WHERE 1 = 1  " + "\n");
            }
            else
            {
                strSqlString.Append("                          FROM RWIPLOTSTS_BOH A   " + "\n");
                strSqlString.Append("                             , MWIPOPRDEF B   " + "\n");
                strSqlString.Append("                         WHERE 1 = 1  " + "\n");
                strSqlString.Append("                           AND A.CUTOFF_DT = '" + cdvDate.Value.ToString("yyyyMMdd") + "' || '22' " + "\n");
            }

            strSqlString.Append("                   AND A.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                   AND A.MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND A.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                   AND A.OPER = B.OPER " + "\n");

            if (cdvLotType.SelectedItem.ToString() != "ALL")
                strSqlString.Append("                   AND A.LOT_CMF_5 LIKE '" + cdvLotType.SelectedItem.ToString() + "' " + "\n");

            strSqlString.Append("                 GROUP BY A.MAT_ID, A.FACTORY, B.OPER_GRP_1, B.OPER_GRP_7, B.OPER_CMF_3, B.OPER, HOLD_FLAG, LOT_STATUS " + "\n");
            strSqlString.Append("               ) LOT " + "\n");
            strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND LOT.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("           AND LOT.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("           AND MAT.DELETE_FLAG <> 'Y' " + "\n");
            strSqlString.Append("           AND MAT.MAT_GRP_2 <> '-' " + "\n");
            strSqlString.Append("         GROUP BY LOT.MAT_ID, LOT.OPER, MAT.MAT_GRP_3 " + "\n");
            strSqlString.Append("       ) TEST_WIP " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = TEST_WIP.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("   AND MAT.DELETE_FLAG = ' ' " + "\n");

            strSqlString.AppendFormat("   AND MAT.MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");

            if (ckbCOB.Checked == true)
                strSqlString.Append("   AND MAT.MAT_GRP_3 <> 'COB' " + "\n");

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

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

            strSqlString.Append(" GROUP BY GROUPING SETS((MAT.MAT_GRP_1, MAT.MAT_GRP_3, MAT.MAT_GRP_6, MAT.MAT_ID, TEST_WIP.OPER), (MAT.MAT_GRP_1),()) " + "\n");
            strSqlString.Append("HAVING ( " + "\n");
            strSqlString.Append("        SUM(NVL(TEST_WIP.HOLD_QTY+TEST_WIP.LOT_QTY,0)) " + "\n");
            strSqlString.Append("       ) <> 0 " + "\n");
            strSqlString.Append(" ORDER BY DECODE(CUSTOMER, 'SEC', 1, 'HYNIX', 2, 'iML', 3, 'FCI', 4, 'IMAGIS', 5, 6), CUSTOMER, PACKAGE, LD_COUNT, PART, OPER " + "\n");



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

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 15, null, null, btnSort);

                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 15, 0, 1, true, Align.Center, VerticalAlign.Center);

                //spdData.RPT_AutoFit(false);

                spdData.RPT_SetPerSubTotalAndGrandTotal(1, 17, 16, 19);   //진도율 평균계산(월)                
                SetAvgVertical3(1, 20, 17, 51, 16);  //반제품확보율(월)
                spdData.RPT_SetPerSubTotalAndGrandTotal(1, 26, 25, 28);   //진도율 평균계산(주)                
                SetAvgVertical3(1, 29, 26, 51, 25);  //반제품확보율(주)

                SetColumnStyle(19);
                SetColumnStyle(20);
                SetColumnStyle(28);
                SetColumnStyle(29);

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
        /// AVG 구하기.. 반제품 확보율
        /// </summary>
        /// <param name="nSampleNormalRowPos"></param>
        /// <param name="nColPos"></param>
        /// <param name="nShipColPos"></param>
        /// <param name="nPlanColPos"></param>
        public void SetAvgVertical3(int nSampleNormalRowPos, int nColPos, int nShipColPos, int nWipColPos, int nPlanColPos)
        {
            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;
            double iShip = 0;
            double iWip = 0;
            double iPlan = 0;


            iShip = Convert.ToDouble(spdData.ActiveSheet.Cells[0, nShipColPos].Value);
            iWip = Convert.ToDouble(spdData.ActiveSheet.Cells[0, nWipColPos].Value);
            iPlan = Convert.ToDouble(spdData.ActiveSheet.Cells[0, nPlanColPos].Value);

            if (iPlan > 0)
            {
                spdData.ActiveSheet.Cells[0, nColPos].Value = Math.Round((iShip + iWip) / iPlan * 100, 1);
            }

            for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                if (spdData.ActiveSheet.Cells[i, nColPos].BackColor != color)
                {
                    iShip = Convert.ToDouble(spdData.ActiveSheet.Cells[i, nShipColPos].Value);
                    iWip = Convert.ToDouble(spdData.ActiveSheet.Cells[i, nWipColPos].Value);
                    iPlan = Convert.ToDouble(spdData.ActiveSheet.Cells[i, nPlanColPos].Value);

                    if (iPlan > 0)
                    {
                        spdData.ActiveSheet.Cells[i, nColPos].Value = Math.Round((iShip + iWip) / iPlan * 100, 1);
                    }
                }
            }

        }

        /// <summary>
        /// 표준진도율보다 낮은 경우 셀을 붉은색으로 변경
        /// </summary>
        /// <param name="nColPos"></param>
        private void SetColumnStyle(int nColPos)
        {
            //jindoPer
            for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                if (Convert.ToDecimal(spdData.ActiveSheet.Cells[i, nColPos].Value) < jindoPer && Convert.ToDecimal(spdData.ActiveSheet.Cells[i, nColPos].Value)>0)
                {
                    spdData.ActiveSheet.Cells[i, nColPos].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(129)), ((System.Byte)(129)));
                }
            }

        }

        /// <summary>
        /// 주기준 잔여일 구하기
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        private string GetWeekRemain(string strDate)
        {
            int remain = 0;
            string startDay = FindWeek_SOP_T.StartDay_ThisWeek;
            DateTime dt = DateTime.Parse(startDay.Substring(0,4)+"-"+startDay.Substring(4,2)+"-"+startDay.Substring(6,2));

            for (int i = 0; i < 7; i++)
            {
                if (dt.AddDays(i).ToString("yyyyMMdd") == strDate)
                {
                    remain = i;
                    break;
                }
            }

            remain = 7 - remain;

            return Convert.ToString(remain);
            
        }

        // 지난 주차의 마지막일 가져오기
        private string MakeSqlString2(string year, string date)
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
                Condition.AppendFormat("기준일자: {0}     today: {1}      workday: {2}     표준진도율: {3} " + "\n", cdvDate.Text, lblToday.Text.ToString(), lblLastDay.Text.ToString(), lblJindo.Text.ToString());

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

            string weekReamin = GetWeekRemain(strDate); //주기준 reamain

            double jindo = 0.0f;

            if (rdbMonth.Checked == true)
            {
                jindo = (Convert.ToDouble(selectday)) / Convert.ToDouble(lastday) * 100;
            }
            else
            {
                jindo = Convert.ToDouble(8-(Convert.ToDouble(weekReamin))) / Convert.ToDouble(7) * 100;
            }
            

            jindoPer = Math.Round(Convert.ToDecimal(jindo), 1);

            //금일조회일 경우 조회조건은 REALTIME
            if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
            {
                dayArry[0] = cdvDate.Value.AddDays(-3).ToString("MM.dd");
                dayArry[1] = cdvDate.Value.AddDays(-2).ToString("MM.dd");
                dayArry[2] = cdvDate.Value.AddDays(-1).ToString("MM.dd");

                dayArry2[0] = cdvDate.Value.AddDays(-3).ToString("yyyyMMdd");
                dayArry2[1] = cdvDate.Value.AddDays(-2).ToString("yyyyMMdd");
                dayArry2[2] = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");

            }
            else
            {
                dayArry[0] = cdvDate.Value.AddDays(-2).ToString("MM.dd");
                dayArry[1] = cdvDate.Value.AddDays(-1).ToString("MM.dd");
                dayArry[2] = cdvDate.Value.ToString("MM.dd");

                dayArry2[0] = cdvDate.Value.AddDays(-2).ToString("yyyyMMdd");
                dayArry2[1] = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");
                dayArry2[2] = cdvDate.Value.ToString("yyyyMMdd");
            }

            if (rdbMonth.Checked == true)
            {
                lblToday.Text = selectday + " day";
                lblLastDay.Text = lastday + " day";
            }
            else
            {
                lblToday.Text = selectday + " day";
                lblLastDay.Text = "7 day";
            }

            if (rdbMonth.Checked == true)
            {
                // 금일조회일 경우 잔여일에 금일 포함함.
                if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
                {
                    remain = (Convert.ToInt32(lastday) - Convert.ToInt32(selectday) + 1);
                }
                else
                {
                    remain = (Convert.ToInt32(lastday) - Convert.ToInt32(selectday));
                }
            }
            else
            {
                // 금일조회일 경우 잔여일에 금일 포함함.
                if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
                {
                    remain = Convert.ToInt32(weekReamin);
                }
                else
                {
                    remain = Convert.ToInt32(weekReamin)-1;
                }
            }
            lblRemain.Text = remain.ToString() + " day";
            lblJindo.Text = jindoPer.ToString() + "%";

        }

        /// <summary>
        /// spred cell 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader == true) return;

            if (e.Column == 48)
            {
                DataTable dt = null;

                if (e.Row == 0)//grand total 클릭
                {
                    dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForPopup_1());

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        System.Windows.Forms.Form frm = new PRD010220_P1("HOLD status", dt);
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("HOLD 현황 세부 정보가 없습니다.");
                    }
                }
                else
                {
                }
            }
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
