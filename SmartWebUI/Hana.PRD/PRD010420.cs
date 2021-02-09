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
    public partial class PRD010420 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        string[] dayArry = new string[3];
        string[] dayArry2 = new string[3];
        decimal jindoPer;
        GlobalVariable.FindWeek FindWeek = new GlobalVariable.FindWeek();

        int nowCulumn = 0;//현재일 재공 컬럼
        /// <summary>
        /// 클  래  스: PRD010420<br/>
        /// 클래스요약: WIP TREND<br/>
        /// 작  성  자: 김태호<br/>
        /// 최초작성일: 2013-07-31<br/>
        /// 상세  설명: WIP TREND<br/>
        /// 변경  내용: <br/> 
        /// 2013-12-26-임종우 : 공정 그룹 검색 기능 멀티 선택으로 변경 (김권수 요청)
        ///                     SetAvgVertical3(1, 48, false) 이 부분 계산시 예외 처리 오류 부분 수정 
        /// 2014-04-07-임종우 : 06시 조회시 당일 재공 중복 오류 수정
        /// 2014-08-29-임종우 : PKG_CODE 가 DHL 이면 3rd, Middle%, Merge 재공 가져오기 (임태성K 요청)
        /// 2015-09-14-임종우 : 재공 기준 변경 - '-', '1st', 'Middle%', 'Merge' 로 변경 (임태성K 요청)
        /// 2015-09-24-임종우 : 고객사 명 하드 코딩 되어 있는것을 기준정보로 변경 (임태성K 요청)
        /// 2018-01-22-임종우 : SubTotal, GrandTotal 백분율 구하기 Function 변경
        /// </summary>
        public PRD010420()
        {
            InitializeComponent();
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            cboTimeBase.SelectedIndex = 1;
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

            String ss = DateTime.Now.ToString("MM-dd");
            FindWeek = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");


            try
            {
                if (cdvFactory.Text.Trim() != "HMKB1")
                {
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Major Code", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("LD Count", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Pin Type", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PKG Code", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Product", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Process group", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

                    spdData.RPT_AddBasicColumn("Monthly plan", 0, 9, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Monthly Plan Rev", 0, 10, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Monthly plan difference", 0, 11, Visibles.False, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);

                    spdData.RPT_AddBasicColumn("goal", 0, 12, Visibles.False, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);

                    spdData.RPT_AddBasicColumn("Production Status", 0, 13, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("actual", 1, 13, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Progress rate (%)", 1, 14, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Double1, 90);
                    spdData.RPT_AddBasicColumn("residual quantity", 1, 15, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);

                    spdData.RPT_MerageHeaderColumnSpan(0, 13, 3);

                    spdData.RPT_AddBasicColumn("Proper WIP", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);


                    if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                    {
                        spdData.RPT_AddBasicColumn("재공현황 (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + " 기준)", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    }
                    else
                    {
                        if (cboTimeBase.SelectedIndex == 1)
                        {
                            spdData.RPT_AddBasicColumn("재공현황 (" + cdvDate.Value.ToString("yy.MM.dd") + " 22:00 기준)", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn("재공현황 (" + cdvDate.Value.ToString("yy.MM.dd") + " 06:00 기준)", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                        }
                    }

                    //선택된 달의 일수에 맞게 컬럼 표시
                    for (int i = 1; i <= 31; i++)
                    {
                        if (i <= Convert.ToInt32(cdvDate.Value.ToString("dd")))
                        {
                            spdData.RPT_AddBasicColumn(string.Format("{0}.{1:D2}", cdvDate.Value.ToString("MM"), i), 1, 17 + i - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                            nowCulumn = 17 + i - 1;
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn(string.Format("{0}.{1:D2}", cdvDate.Value.ToString("MM"), i), 1, 17 + i - 1, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        }
                    }

                    spdData.RPT_MerageHeaderColumnSpan(0, 17, 31);

                    spdData.RPT_AddBasicColumn("WIP days", 0, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                    spdData.RPT_AddBasicColumn("Standard work days", 0, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);

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
                    spdData.RPT_MerageHeaderRowSpan(0, 15, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 16, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 48, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 49, 2);
                }
                else  // Bump Factory
                {
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Bumping Type", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Process Flow", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Layer classification", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Pkg Type", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("RDL Plating", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Final Bumo", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SUB. Material", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Product", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Size", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Thickness", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Flat Type", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Process group", 0, 12, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

                    spdData.RPT_AddBasicColumn("Monthly plan", 0, 13, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Monthly Plan Rev", 0, 14, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Monthly plan difference", 0, 15, Visibles.False, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);

                    spdData.RPT_AddBasicColumn("goal", 0, 16, Visibles.False, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);

                    spdData.RPT_AddBasicColumn("Production Status", 0, 17, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("actual", 1, 17, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Progress rate (%)", 1, 18, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Double1, 90);
                    spdData.RPT_AddBasicColumn("residual quantity", 1, 19, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);

                    spdData.RPT_MerageHeaderColumnSpan(0, 17, 3);

                    spdData.RPT_AddBasicColumn("Proper WIP", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);


                    if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                    {
                        spdData.RPT_AddBasicColumn("재공현황 (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + " 기준)", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    }
                    else
                    {
                        if (cboTimeBase.SelectedIndex == 1)
                        {
                            spdData.RPT_AddBasicColumn("재공현황 (" + cdvDate.Value.ToString("yy.MM.dd") + " 22:00 기준)", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn("재공현황 (" + cdvDate.Value.ToString("yy.MM.dd") + " 06:00 기준)", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                        }
                    }

                    //선택된 달의 일수에 맞게 컬럼 표시
                    for (int i = 1; i <= 31; i++)
                    {
                        if (i <= Convert.ToInt32(cdvDate.Value.ToString("dd")))
                        {
                            spdData.RPT_AddBasicColumn(string.Format("{0}.{1:D2}", cdvDate.Value.ToString("MM"), i), 1, 21 + i - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                            nowCulumn = 21 + i - 1;
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn(string.Format("{0}.{1:D2}", cdvDate.Value.ToString("MM"), i), 1, 21 + i - 1, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        }
                    }

                    spdData.RPT_MerageHeaderColumnSpan(0, 21, 31);

                    spdData.RPT_AddBasicColumn("WIP days", 0, 52, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                    spdData.RPT_AddBasicColumn("Standard work days", 0, 53, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);

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
                    spdData.RPT_MerageHeaderRowSpan(0, 13, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 14, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 15, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 19, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 20, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 52, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, 53, 2);
                }

                //spdData.RPT_ColumnConfigFromTable(btnSort);
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

            if (cdvFactory.Text.Trim() != "HMKB1")
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "CUSTOMER", "(CASE WHEN MAT.MAT_GRP_1 = 'SE' THEN 'SAMSUNG' WHEN MAT.MAT_GRP_1 = 'HX' THEN 'SK Hynix' ELSE 'Fabless' END) AS CUSTOMER", "(CASE WHEN MAT.MAT_GRP_1 = 'SE' THEN 'SAMSUNG' WHEN MAT.MAT_GRP_1 = 'HX' THEN 'SK Hynix' ELSE 'Fabless'  END)", "DECODE(CUSTOMER, '', 1, 'SAMSUNG', 2, 'SK Hynix', 3, 'Fabless', 4, 5), CUSTOMER", "DECODE(CUSTOMER,'SEC','SEC','HYNIX','HYNIX','','', 'Fabless')", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "MAJOR", "MAT.MAT_GRP_9 AS MAJOR", "MAT.MAT_GRP_9", "MAJOR", "MAJOR", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "PKG", "MAT.MAT_GRP_10 AS PKG", "MAT.MAT_GRP_10", "PKG", "PKG", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "TYPE1", "MAT.MAT_GRP_4 AS TYPE1", "MAT.MAT_GRP_4", "TYPE1", "TYPE1", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "LD_COUNT", "MAT.MAT_GRP_6 AS LD_COUNT", "MAT.MAT_GRP_6", "LD_COUNT", "LD_COUNT", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "PIN_TYPE", "MAT.MAT_CMF_10 AS PIN_TYPE", "MAT.MAT_CMF_10", "PIN_TYPE", "PIN_TYPE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "PKG_CODE", "MAT.MAT_CMF_11 AS PKG_CODE", "MAT.MAT_CMF_11", "PKG_CODE", "PKG_CODE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "PRODUCT", "MAT.MAT_ID AS PRODUCT", "MAT.MAT_ID", "PRODUCT", "PRODUCT", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Process group", "OPER_GRP", "' '", "' '", "' '", "OPER_GRP", false);
            }
            else
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "CUSTOMER", "(CASE WHEN MAT.MAT_GRP_1 = 'SE' THEN 'SAMSUNG' WHEN MAT.MAT_GRP_1 = 'HX' THEN 'SK Hynix' ELSE 'Fabless' END) AS CUSTOMER", "(CASE WHEN MAT.MAT_GRP_1 = 'SE' THEN 'SAMSUNG' WHEN MAT.MAT_GRP_1 = 'HX' THEN 'SK Hynix' ELSE 'Fabless'  END)", "DECODE(CUSTOMER, '', 1, 'SAMSUNG', 2, 'SK Hynix', 3, 'Fabless', 4, 5), CUSTOMER", "DECODE(CUSTOMER,'SEC','SEC','HYNIX','HYNIX','','', 'Fabless')", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("BUMPING TYPE", "BUMPING_TYPE", "MAT_GRP_2 AS BUMPING_TYPE", "MAT.MAT_GRP_2", "BUMPING_TYPE", "BUMPING_TYPE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PROCESS FLOW", "PROCESS_FLOW", "MAT_GRP_3 AS PROCESS_FLOW", "MAT.MAT_GRP_3", "PROCESS_FLOW", "PROCESS_FLOW", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "LAYER", "MAT_GRP_4 AS LAYER", "MAT.MAT_GRP_4", "LAYER", "LAYER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG TYPE", "PKG_TYPE", "MAT_GRP_5 AS PKG_TYPE", "MAT.MAT_GRP_5", "PKG_TYPE", "PKG_TYPE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL PLATING", "RDL_PLATING", "MAT_GRP_6 AS RDL_PLATING", "MAT.MAT_GRP_6", "RDL_PLATING", "RDL_PLATING", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FINAL BUMP", "FINAL_BUMP", "MAT_GRP_7 AS FINAL_BUMP", "MAT.MAT_GRP_7", "FINAL_BUMP", "FINAL_BUMP", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SUB. MATERIAL", "SUB_MATERIAL", "MAT_GRP_8 AS SUB_MATERIAL", "MAT.MAT_GRP_8", "SUB_MATERIAL", "SUB_MATERIAL", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "PRODUCT", "MAT.MAT_ID AS PRODUCT", "MAT.MAT_ID", "PRODUCT", "PRODUCT", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SIZE", "WF_SIZE", "MAT_CMF_14 AS WF_SIZE", "MAT.MAT_CMF_14", "WF_SIZE", "WF_SIZE", false); // \"SIZE\"
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("THICKNESS", "THICKNESS", "MAT_CMF_2 AS THICKNESS", "MAT.MAT_CMF_2", "THICKNESS", "THICKNESS", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FLAT TYPE", "FLAT_TYPE", "MAT_CMF_3 AS FLAT_TYPE", "MAT.MAT_CMF_3", "FLAT_TYPE", "FLAT_TYPE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Process group", "OPER_GRP", "' '", "' '", "' '", "OPER_GRP", false);
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
            
            string start_date;
            string yesterday;
            string date;
            string month;
            string year;
            string lastMonth;            
            bool IsOperGroup = false; //공정그룹이 조건그룹에 포함되었는지 여부

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;
            

            #region 공정그룹 여부 판단
            
            string[] groupList = QueryCond1.Split(',');

            for (int i = 0; i < groupList.Length; i++)
            {
                if ("OPER_GRP" == groupList[i].Trim().ToString())
                {
                    IsOperGroup = true;
                    break;
                }
            }
            #endregion 공정그룹 여부 판단
            //int remain = Convert.ToInt32(lblLastDay.Text.Substring(0,2)) - Convert.ToInt32(lblToday.Text.Substring(0,2)) + 1;

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
            
            string strKpcs = "";

            if (ckbKpcs.Checked == false)
                strKpcs = "1";
            else
                strKpcs = "1000";

            string hour = "";

            //if (cboTimeBase.Text == "06시")
            if (cboTimeBase.SelectedIndex == 0)
            {
                hour = "06";
            }
            else
            {
                hour = "22";
            }

            strSqlString.Append("SELECT " + QueryCond1 + "\n");

            #region Bump 이외 공장 Query
            if (cdvFactory.Text.Trim() != "HMKB1")
            {
                strSqlString.Append("     , ROUND(MAX(ORI_PLAN)/" + strKpcs + ",0) AS \"월계획\"  " + "\n");
                strSqlString.Append("     , ROUND(MAX(MON_PLAN)/" + strKpcs + ",0) AS \"월계획Rev\"  " + "\n");
                strSqlString.Append("     , ROUND((MAX(MON_PLAN) - MAX(ORI_PLAN))/" + strKpcs + ",0) AS \"월계획 차이\"  " + "\n");
                strSqlString.Append("     , ROUND(MAX(TARGET_MON)/1000,0), ROUND(MAX(ASSY_MON)/" + strKpcs + ",0)  " + "\n");
                strSqlString.Append("     , DECODE(MAX(MON_PLAN), 0, 0, ROUND((MAX(ASSY_MON)/MAX(MON_PLAN))*100, 1)) JINDO " + "\n");
                strSqlString.Append("     , ROUND((MAX(MON_PLAN)-MAX(ASSY_MON))/" + strKpcs + ",0) AS \"잔량\"  " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(OPT_WIP,0))/" + strKpcs + ",0) AS OPT_WIP " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(DAY01,0))/" + strKpcs + ",0) AS DAY01, ROUND(SUM(NVL(DAY02,0))/" + strKpcs + ",0) AS DAY02, ROUND(SUM(NVL(DAY03,0))/" + strKpcs + ",0) AS DAY03, ROUND(SUM(NVL(DAY04,0))/" + strKpcs + ",0) AS DAY04, ROUND(SUM(NVL(DAY05,0))/" + strKpcs + ",0) AS DAY05, ROUND(SUM(NVL(DAY06,0))/" + strKpcs + ",0) AS DAY06, ROUND(SUM(NVL(DAY07,0))/" + strKpcs + ",0) AS DAY07, ROUND(SUM(NVL(DAY08,0))/" + strKpcs + ",0) AS DAY08, ROUND(SUM(NVL(DAY09,0))/" + strKpcs + ",0) AS DAY09, ROUND(SUM(NVL(DAY10,0))/" + strKpcs + ",0) AS DAY10   " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(DAY11,0))/" + strKpcs + ",0) AS DAY11, ROUND(SUM(NVL(DAY12,0))/" + strKpcs + ",0) AS DAY12, ROUND(SUM(NVL(DAY13,0))/" + strKpcs + ",0) AS DAY13, ROUND(SUM(NVL(DAY14,0))/" + strKpcs + ",0) AS DAY14, ROUND(SUM(NVL(DAY15,0))/" + strKpcs + ",0) AS DAY15, ROUND(SUM(NVL(DAY16,0))/" + strKpcs + ",0) AS DAY16, ROUND(SUM(NVL(DAY17,0))/" + strKpcs + ",0) AS DAY17, ROUND(SUM(NVL(DAY18,0))/" + strKpcs + ",0) AS DAY18, ROUND(SUM(NVL(DAY19,0))/" + strKpcs + ",0) AS DAY19, ROUND(SUM(NVL(DAY20,0))/" + strKpcs + ",0) AS DAY20   " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(DAY21,0))/" + strKpcs + ",0) AS DAY21, ROUND(SUM(NVL(DAY22,0))/" + strKpcs + ",0) AS DAY22, ROUND(SUM(NVL(DAY23,0))/" + strKpcs + ",0) AS DAY23, ROUND(SUM(NVL(DAY24,0))/" + strKpcs + ",0) AS DAY24, ROUND(SUM(NVL(DAY25,0))/" + strKpcs + ",0) AS DAY25, ROUND(SUM(NVL(DAY26,0))/" + strKpcs + ",0) AS DAY26, ROUND(SUM(NVL(DAY27,0))/" + strKpcs + ",0) AS DAY27, ROUND(SUM(NVL(DAY28,0))/" + strKpcs + ",0) AS DAY28, ROUND(SUM(NVL(DAY29,0))/" + strKpcs + ",0) AS DAY29, ROUND(SUM(NVL(DAY30,0))/" + strKpcs + ",0) AS DAY30, ROUND(SUM(NVL(DAY31,0))/" + strKpcs + ",0) AS DAY31 " + "\n");

                strSqlString.Append("     , ROUND(MAX(WIP_DAY),1) AS WIP_DAY " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(TAT,0)),2) AS TAT " + "\n");

                strSqlString.Append("\n");
                strSqlString.Append("  FROM ( " + "\n");


                strSqlString.Append("        SELECT " + QueryCond2 + ", MAT.OPER_GRP AS OPER_GRP" + "\n");

                strSqlString.Append("             , SUM(NVL(M.ORI_PLAN,0)) AS ORI_PLAN " + "\n");
                strSqlString.Append("             , SUM(NVL(M.MON_PLAN,0)) AS MON_PLAN " + "\n");
                strSqlString.Append("             , SUM(NVL(M.TARGET_MON,0)) AS TARGET_MON " + "\n");
                strSqlString.Append("             , SUM(NVL(M.ASSY_MON,0)) AS ASSY_MON " + "\n");
                strSqlString.Append("             , SUM(NVL(M.ASSY_MON_SHIP,0)) AS ASSY_MON_SHIP " + "\n");

                if (ckbRemain.Checked == false)
                {
                    strSqlString.Append("             , CASE WHEN SUM(NVL(M.MON_PLAN,0))=0 THEN 0 " + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN MAT.OPER_GRP='STOCK' THEN DECODE(MIN(T.STOCK_TAT),0, 0, ((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.STOCK_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='SAW' THEN DECODE(MIN(T.SAW_TAT),0, 0, ((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.SAW_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='DA' THEN DECODE(MIN(T.DA_TAT),0, 0, ((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.DA_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='WB' THEN DECODE(MIN(T.WB_TAT),0, 0, ((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.WB_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='MOLD' THEN DECODE(MIN(T.MOLD_TAT),0, 0, ((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.MOLD_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='FINISH' THEN DECODE(MIN(T.FINISH_TAT),0, 0, ((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.FINISH_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='HMK3A' THEN DECODE(MIN(T.HMK3A_TAT),0, 0, ((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.HMK3A_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              ELSE 0 " + "\n");
                    strSqlString.Append("                          END " + "\n");
                    strSqlString.Append("                END OPT_WIP " + "\n");
                }
                else
                {
                    strSqlString.Append("             , CASE WHEN SUM(NVL(MON_PLAN,0))=0 THEN 0 " + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN MAT.OPER_GRP='STOCK' THEN DECODE(MIN(T.STOCK_TAT),0, 0, ((SUM(NVL(MON_PLAN,0) - NVL(ASSY_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.STOCK_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='SAW' THEN DECODE(MIN(T.SAW_TAT),0, 0, ((SUM(NVL(MON_PLAN,0) - NVL(ASSY_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.SAW_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='DA' THEN DECODE(MIN(T.DA_TAT),0, 0, ((SUM(NVL(MON_PLAN,0) - NVL(ASSY_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.DA_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='WB' THEN DECODE(MIN(T.WB_TAT),0, 0, ((SUM(NVL(MON_PLAN,0) - NVL(ASSY_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.WB_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='MOLD' THEN DECODE(MIN(T.MOLD_TAT),0, 0, ((SUM(NVL(MON_PLAN,0) - NVL(ASSY_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.MOLD_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='FINISH' THEN DECODE(MIN(T.FINISH_TAT),0, 0, ((SUM(NVL(MON_PLAN,0) - NVL(ASSY_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.FINISH_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='HMK3A' THEN DECODE(MIN(T.HMK3A_TAT),0, 0, ((SUM(NVL(MON_PLAN,0) - NVL(ASSY_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.HMK3A_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                          END " + "\n");
                    strSqlString.Append("                END OPT_WIP " + "\n");
                }

                strSqlString.Append("             , SUM(NVL(A.DAY01,0)) AS DAY01, SUM(NVL(A.DAY02,0)) AS DAY02, SUM(NVL(A.DAY03,0)) AS DAY03, SUM(NVL(A.DAY04,0)) AS DAY04, SUM(NVL(A.DAY05,0)) AS DAY05, SUM(NVL(A.DAY06,0)) AS DAY06, SUM(NVL(A.DAY07,0)) AS DAY07, SUM(NVL(A.DAY08,0)) AS DAY08, SUM(NVL(A.DAY09,0)) AS DAY09, SUM(NVL(A.DAY10,0)) AS DAY10 " + "\n");
                strSqlString.Append("             , SUM(NVL(A.DAY11,0)) AS DAY11, SUM(NVL(A.DAY12,0)) AS DAY12, SUM(NVL(A.DAY13,0)) AS DAY13, SUM(NVL(A.DAY14,0)) AS DAY14, SUM(NVL(A.DAY15,0)) AS DAY15, SUM(NVL(A.DAY16,0)) AS DAY16, SUM(NVL(A.DAY17,0)) AS DAY17, SUM(NVL(A.DAY18,0)) AS DAY18, SUM(NVL(A.DAY19,0)) AS DAY19, SUM(NVL(A.DAY20,0)) AS DAY20 " + "\n");
                strSqlString.Append("             , SUM(NVL(A.DAY21,0)) AS DAY21, SUM(NVL(A.DAY22,0)) AS DAY22, SUM(NVL(A.DAY23,0)) AS DAY23, SUM(NVL(A.DAY24,0)) AS DAY24, SUM(NVL(A.DAY25,0)) AS DAY25, SUM(NVL(A.DAY26,0)) AS DAY26, SUM(NVL(A.DAY27,0)) AS DAY27, SUM(NVL(A.DAY28,0)) AS DAY28, SUM(NVL(A.DAY29,0)) AS DAY29, SUM(NVL(A.DAY30,0)) AS DAY30, SUM(NVL(A.DAY31,0)) AS DAY31 " + "\n");

                if (ckbRemain.Checked == false)
                {
                    strSqlString.Append("             , CASE WHEN SUM(NVL(M.MON_PLAN,0))=0 THEN 0 " + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN MAT.OPER_GRP='STOCK' THEN DECODE(MIN(T.STOCK_TAT),0, 0, SUM(NVL(A.DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.STOCK_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='SAW' THEN DECODE(MIN(T.SAW_TAT),0, 0, SUM(NVL(A.DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.SAW_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='DA' THEN DECODE(MIN(T.DA_TAT),0, 0, SUM(NVL(A.DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.DA_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='WB' THEN DECODE(MIN(T.WB_TAT),0, 0, SUM(NVL(A.DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.WB_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='MOLD' THEN DECODE(MIN(T.MOLD_TAT),0, 0, SUM(NVL(A.DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.MOLD_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='FINISH' THEN DECODE(MIN(T.FINISH_TAT),0, 0, SUM(NVL(A.DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.FINISH_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='HMK3A' THEN DECODE(MIN(T.HMK3A_TAT),0, 0, SUM(NVL(A.DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.HMK3A_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              ELSE 0 " + "\n");
                    strSqlString.Append("                          END " + "\n");
                    strSqlString.Append("                END WIP_DAY " + "\n");
                }
                else
                {
                    strSqlString.Append("             , CASE WHEN SUM(NVL(MON_PLAN,0) - NVL(ASSY_MON_SHIP,0))=0 THEN 0 " + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN MAT.OPER_GRP='STOCK' THEN DECODE(MIN(T.STOCK_TAT),0, 0, SUM(NVL(DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(MON_PLAN,0) - NVL(ASSY_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.STOCK_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='SAW' THEN  DECODE(MIN(T.SAW_TAT),0, 0, SUM(NVL(DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(MON_PLAN,0) - NVL(ASSY_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.SAW_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='DA' THEN  DECODE(MIN(T.DA_TAT),0, 0, SUM(NVL(DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(MON_PLAN,0) - NVL(ASSY_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.DA_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='WB' THEN  DECODE(MIN(T.WB_TAT),0, 0, SUM(NVL(DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(MON_PLAN,0) - NVL(ASSY_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.WB_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='MOLD' THEN  DECODE(MIN(T.MOLD_TAT),0, 0, SUM(NVL(DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(MON_PLAN,0) - NVL(ASSY_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.MOLD_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='FINISH' THEN  DECODE(MIN(T.FINISH_TAT),0, 0, SUM(NVL(DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(MON_PLAN,0) - NVL(ASSY_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.FINISH_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='HMK3A' THEN  DECODE(MIN(T.HMK3A_TAT),0, 0, SUM(NVL(DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(MON_PLAN,0) - NVL(ASSY_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.HMK3A_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                          END " + "\n");
                    strSqlString.Append("                END WIP_DAY " + "\n");
                }

                strSqlString.Append("             , MIN(CASE WHEN MAT.OPER_GRP='STOCK' THEN T.STOCK_TAT " + "\n");
                strSqlString.Append("                        WHEN MAT.OPER_GRP='SAW' THEN T.SAW_TAT " + "\n");
                strSqlString.Append("                        WHEN MAT.OPER_GRP='DA' THEN T.DA_TAT " + "\n");
                strSqlString.Append("                        WHEN MAT.OPER_GRP='WB' THEN T.WB_TAT " + "\n");
                strSqlString.Append("                        WHEN MAT.OPER_GRP='MOLD' THEN T.MOLD_TAT " + "\n");
                strSqlString.Append("                        WHEN MAT.OPER_GRP='FINISH' THEN T.FINISH_TAT " + "\n");
                strSqlString.Append("                        WHEN MAT.OPER_GRP='HMK3A' THEN T.HMK3A_TAT " + "\n");
                strSqlString.Append("                        ELSE 0 " + "\n");
                strSqlString.Append("                    END) TAT " + "\n");

                strSqlString.Append("      FROM ( " + "\n");
                strSqlString.Append("            SELECT MAT.*, OPER.OPER_GRP " + "\n");
                strSqlString.Append("              FROM MWIPMATDEF MAT " + "\n");
                strSqlString.Append("                 , ( " + "\n");
                strSqlString.Append("                    SELECT DISTINCT A.FACTORY, DECODE(A.OPER_GRP_1, 'HMK3A', 'HMK3A', 'M/K', 'FINISH', 'V/I', 'FINISH', 'TRIM', 'FINISH', 'S/B/A', 'FINISH', 'TIN', 'FINISH', 'SIG', 'FINISH', 'AVI', 'FINISH', 'MOLD', 'MOLD', 'CURE', 'MOLD', 'W/B', 'WB', 'GATE', 'WB', 'D/A', 'DA', 'SMT', 'DA', 'S/P', 'DA', 'B/G', 'SAW', 'SAW', 'SAW', 'HMK2A', 'STOCK') OPER_GRP " + "\n");
                strSqlString.Append("                      FROM  MWIPOPRDEF A " + "\n");
                strSqlString.Append("                     WHERE 1 = 1 " + "\n");
                strSqlString.Append("                       AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                       AND OPER_GRP_1 IN ('HMK3A', 'M/K', 'V/I', 'TRIM', 'S/B/A', 'TIN', 'SIG', 'AVI', 'MOLD', 'CURE', 'W/B', 'GATE', 'D/A', 'SMT', 'S/P', 'B/G', 'SAW', 'HMK2A') " + "\n");
                strSqlString.Append("                     GROUP BY A.FACTORY, A.OPER_GRP_1 " + "\n");
                strSqlString.Append("                    ) OPER " + "\n");
                strSqlString.Append("             WHERE 1 = 1 " + "\n");
                strSqlString.Append("               AND OPER_GRP <> ' ' " + "\n");
                strSqlString.Append("               AND MAT.VENDOR_ID <> ' ' " + "\n");
                strSqlString.Append("               AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("               AND MAT.DELETE_FLAG = ' ' " + "\n");
                strSqlString.Append("           ) MAT " + "\n");
                strSqlString.Append("         , (  " + "\n");
                strSqlString.Append("            SELECT MAT_ID, OPER_GRP " + "\n");

                for (int i = 1; i <= 31; i++)
                {
                    strSqlString.Append(string.Format("                     , SUM(NVL(DECODE(CUTOFF_DAY,'{0}{1:D2}', QTY),0)) AS DAY{2:D2} \n", month, i, i));
                }


                strSqlString.Append("                  FROM ( " + "\n");
                strSqlString.Append("                        SELECT MAT_ID, CUTOFF_DAY, OPER_GRP " + "\n");
                strSqlString.Append("                             , SUM(NVL(DECODE(OPER_GRP, 'STOCK', STOCK, 'SAW', SAW, 'DA', DA, 'WB', WB, 'MOLD', MOLD, 'FINISH', FINISH, 'HMK3A', HMK3A),0)) AS QTY " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                SELECT A.MAT_ID \n");
                strSqlString.Append("                                     , F.CUTOFF_DAY AS CUTOFF_DAY, DECODE(OPER_GRP_1, 'HMK3A', 'HMK3A', 'M/K', 'FINISH', 'V/I', 'FINISH', 'TRIM', 'FINISH', 'S/B/A', 'FINISH', 'TIN', 'FINISH', 'SIG', 'FINISH', 'AVI', 'FINISH', 'MOLD', 'MOLD', 'CURE', 'MOLD', 'W/B', 'WB', 'GATE', 'WB', 'D/A', 'DA', 'SMT', 'DA', 'S/P', 'DA', 'B/G', 'SAW', 'SAW', 'SAW', 'HMK2A', 'STOCK') AS OPER_GRP " + "\n");
                strSqlString.Append("                                     , SUM(NVL(F.V0,0)) AS STOCK  " + "\n");
                strSqlString.Append("                                     , SUM(NVL(F.V1,0)+NVL(F.V2,0)) AS SAW" + "\n");
                strSqlString.Append("                                     , SUM(NVL(F.V3,0)+NVL(F.V4,0)) AS DA" + "\n");
                strSqlString.Append("                                     , SUM(NVL(F.V5,0)+NVL(F.V16,0)) AS WB" + "\n");
                strSqlString.Append("                                     , SUM(NVL(F.V6,0)+NVL(F.V7,0)) AS MOLD" + "\n");
                strSqlString.Append("                                     , SUM(NVL(F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14,0)) AS FINISH" + "\n");
                strSqlString.Append("                                     , SUM(NVL(F.V15,0)) AS HMK3A  " + "\n");
                strSqlString.Append("                                  FROM ( " + "\n");
                strSqlString.Append("                                        SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8 " + "\n");
                strSqlString.Append("                                             , DECODE(MAT.MAT_GRP_1,'SE',MAT.MAT_GRP_9,' ') AS MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7, MAT.MAT_CMF_13, MAT.MAT_CMF_11  " + "\n");

                strSqlString.Append("                                          FROM MWIPMATDEF MAT " + "\n");

                //월 계획 SLIS 제품은 MP계획과 동일하게            
                strSqlString.Append("                                             , ( " + "\n");
                strSqlString.Append("                                                SELECT FACTORY,MAT_ID,PLAN_QTY_ASSY,PLAN_MONTH, RESV_FIELD1 " + "\n");
                strSqlString.Append("                                                  FROM ( " + "\n");
                strSqlString.Append("                                                        SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, PLAN_MONTH, SUM(RESV_FIELD1) AS RESV_FIELD1  " + "\n");
                strSqlString.Append("                                                          FROM ( " + "\n");
                strSqlString.Append("                                                                SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, PLAN_MONTH, SUM(TO_NUMBER(DECODE(RESV_FIELD1,' ',0,RESV_FIELD1))) AS RESV_FIELD1 " + "\n");
                strSqlString.Append("                                                                  FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                                                 GROUP BY FACTORY, MAT_ID, PLAN_MONTH " + "\n");
                strSqlString.Append("                                                               ) " + "\n");
                strSqlString.Append("                                                         GROUP BY FACTORY, MAT_ID,PLAN_MONTH " + "\n");
                strSqlString.Append("                                                       ) " + "\n");
                strSqlString.Append("                                               ) PLAN " + "\n");
                strSqlString.Append("                                         WHERE 1 = 1 " + "\n");
                strSqlString.Append("                                           AND MAT.FACTORY =PLAN.FACTORY(+) " + "\n");
                strSqlString.Append("                                           AND MAT.MAT_ID = PLAN.MAT_ID(+) " + "\n");

                strSqlString.Append("                                           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                           AND PLAN.PLAN_MONTH(+) = '" + month + "' " + "\n");
                strSqlString.Append("                                           AND MAT.MAT_TYPE= 'FG' " + "\n");
                strSqlString.Append("                                           AND MAT.DELETE_FLAG <> 'Y' " + "\n");
                strSqlString.Append("                                         GROUP BY MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7, MAT.MAT_CMF_13, MAT.MAT_CMF_11" + "\n");
                strSqlString.Append("                                       ) A  " + "\n");
                strSqlString.Append("                                       " + "\n");

                strSqlString.Append("                                     , ( " + "\n");
                strSqlString.Append("                                        SELECT MAT_ID, OPER_GRP_1, CUTOFF_DAY " + "\n");
                strSqlString.Append("                                             , ROUND(SUM(DECODE(OPER_GRP_1, 'HMK2A', QTY, 0))) AS V0 " + "\n");
                strSqlString.Append("                                             , ROUND(SUM(DECODE(OPER_GRP_1, 'B/G', QTY, 0))) AS V1 " + "\n");
                strSqlString.Append("                                             , ROUND(SUM(DECODE(OPER_GRP_1, 'SAW', QTY, 0))) AS V2 " + "\n");
                strSqlString.Append("                                             , ROUND(SUM(DECODE(OPER_GRP_1, 'S/P', QTY, 0))) AS V3 " + "\n");
                strSqlString.Append("                                             , ROUND(SUM(DECODE(OPER_GRP_1, 'D/A', QTY, 0))) AS V4 " + "\n");
                strSqlString.Append("                                             , ROUND(SUM(DECODE(OPER_GRP_1, 'W/B', QTY, 0))) AS V5 " + "\n");
                strSqlString.Append("                                             , ROUND(SUM(DECODE(OPER_GRP_1, 'MOLD', QTY, 0))) AS V6 " + "\n");
                strSqlString.Append("                                             , ROUND(SUM(DECODE(OPER_GRP_1, 'CURE', QTY, 0))) AS V7 " + "\n");
                strSqlString.Append("                                             , ROUND(SUM(DECODE(OPER_GRP_1, 'M/K', QTY, 0))) AS V8 " + "\n");
                strSqlString.Append("                                             , ROUND(SUM(DECODE(OPER_GRP_1, 'TRIM', QTY, 0))) AS V9 " + "\n");
                strSqlString.Append("                                             , ROUND(SUM(DECODE(OPER_GRP_1, 'TIN', QTY, 0))) AS V10" + "\n");
                strSqlString.Append("                                             , ROUND(SUM(DECODE(OPER_GRP_1, 'S/B/A', QTY, 0))) AS V11 " + "\n");
                strSqlString.Append("                                             , ROUND(SUM(DECODE(OPER_GRP_1, 'SIG', QTY, 0))) AS V12 " + "\n");
                strSqlString.Append("                                             , ROUND(SUM(DECODE(OPER_GRP_1, 'AVI', QTY, 0))) AS V13 " + "\n");
                strSqlString.Append("                                             , ROUND(SUM(DECODE(OPER_GRP_1, 'V/I', QTY, 0))) AS V14 " + "\n");
                strSqlString.Append("                                             , ROUND(SUM(DECODE(OPER_GRP_1, 'HMK3A', QTY, 0))) AS V15 " + "\n");
                strSqlString.Append("                                             , ROUND(SUM(DECODE(OPER_GRP_1, 'GATE', QTY, 0))) AS V16 " + "\n");
                strSqlString.Append("                                          FROM (  " + "\n");
                strSqlString.Append("                                                SELECT MAT_ID, OPER_GRP_1, SUBSTR(CUTOFF_DT, 1, 8) AS CUTOFF_DAY " + "\n");
                strSqlString.Append("                                                     , SUM(CASE WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1) " + "\n");
                strSqlString.Append("                                                                ELSE QTY_1 " + "\n");
                strSqlString.Append("                                                           END) QTY " + "\n");
                strSqlString.Append("                                                  FROM (" + "\n");
                strSqlString.Append("                                                        SELECT A.CUTOFF_DT, A.FACTORY, A.MAT_ID, B.OPER_GRP_1, B.OPER " + "\n");
                strSqlString.Append("                                                             , CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN A.QTY_1 / C.NET_DIE ELSE A.QTY_1 END AS QTY_1 " + "\n");
                strSqlString.Append("                                                             , C.COMP_CNT " + "\n");
                strSqlString.Append("                                                          FROM (" + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == Select_date.ToString("yyyyMMdd"))
                {                    
                    strSqlString.Append("                                                                SELECT '" + Select_date.ToString("yyyyMMdd") + hour + "' AS CUTOFF_DT, A.*, 0 AS REJECT_QTY, 0 AS CV_QTY " + "\n");
                    strSqlString.Append("                                                                  FROM RWIPLOTSTS A " + "\n");
                    strSqlString.Append("                                                                 UNION ALL " + "\n");
                    strSqlString.Append("                                                                SELECT * " + "\n");
                    strSqlString.Append("                                                                  FROM RWIPLOTSTS_BOH B " + "\n");
                    strSqlString.Append("                                                                 WHERE CUTOFF_DT IN (");

                    for (int i = 1; i < Convert.ToInt32(Select_date.ToString("dd")); i++)
                    {
                        if (i != 1)
                            strSqlString.Append(",");

                        strSqlString.Append(string.Format("'{0}{1:D2}{2}'", month, i, hour));
                    }

                    strSqlString.Append(") \n");

                }
                else
                {
                    strSqlString.Append("                                                                SELECT * " + "\n");
                    strSqlString.Append("                                                                  FROM RWIPLOTSTS_BOH" + "\n");
                    strSqlString.Append("                                                                 WHERE CUTOFF_DT IN (");

                    for (int i = 1; i <= Convert.ToInt32(Select_date.ToString("dd")); i++)
                    {
                        if (i != 1)
                            strSqlString.Append(",");

                        strSqlString.Append(string.Format("'{0}{1:D2}{2}'", month, i, hour));
                    }

                    strSqlString.Append(") \n");

                }
                                
                strSqlString.Append("                                                               ) A " + "\n");
                strSqlString.Append("                                                               , MWIPOPRDEF B " + "\n");
                strSqlString.Append("                                                               , VWIPMATDEF C " + "\n");
                strSqlString.Append("                                                           WHERE 1 = 1 " + "\n");
                strSqlString.Append("                                                             AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                                                             AND A.FACTORY = C.FACTORY " + "\n");
                strSqlString.Append("                                                             AND A.OPER = B.OPER " + "\n");
                strSqlString.Append("                                                             AND A.MAT_ID = C.MAT_ID " + "\n");

                int lastday = Convert.ToInt32(last_day.Substring(6, 2));

                strSqlString.Append("                                                             AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                                             AND A.MAT_VER = 1 " + "\n");
                strSqlString.Append("                                                             AND A.LOT_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("                                                             AND A.LOT_TYPE = 'W' " + "\n");

                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                {
                    strSqlString.AppendFormat("                                                             AND A.LOT_CMF_5 LIKE '{0}'" + "\n", txtSearchProduct.Text);
                }

                strSqlString.Append("                                                             AND C.DELETE_FLAG <> 'Y' " + "\n");
                strSqlString.Append("                                                             AND C.MAT_GRP_2 <> '-'  " + "\n");
                strSqlString.Append("                                                             AND REGEXP_LIKE(C.MAT_GRP_5, 'Middle*|Merge|1st|-') " + "\n");
                strSqlString.Append("                                                       ) " + "\n");
                strSqlString.Append("                                                 GROUP BY MAT_ID, OPER_GRP_1, SUBSTR(CUTOFF_DT, 1, 8)   " + "\n");
                strSqlString.Append("                                               )  " + "\n");
                strSqlString.Append("                                         GROUP BY MAT_ID ,OPER_GRP_1, CUTOFF_DAY  " + "\n");
                strSqlString.Append("                                       ) F " + "\n");
                strSqlString.Append("                                 WHERE 1 = 1 " + "\n");
                strSqlString.Append("                                   AND A.MAT_ID = F.MAT_ID(+)" + "\n");
                strSqlString.AppendFormat("                                   AND A.MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");
                strSqlString.Append("                                 GROUP BY A.MAT_ID ,F.CUTOFF_DAY, DECODE(OPER_GRP_1, 'HMK3A', 'HMK3A', 'M/K', 'FINISH', 'V/I', 'FINISH', 'TRIM', 'FINISH', 'S/B/A', 'FINISH', 'TIN', 'FINISH', 'SIG', 'FINISH', 'AVI', 'FINISH', 'MOLD', 'MOLD', 'CURE', 'MOLD', 'W/B', 'WB', 'GATE', 'WB', 'D/A', 'DA', 'SMT', 'DA', 'S/P', 'DA', 'B/G', 'SAW', 'SAW', 'SAW', 'HMK2A', 'STOCK')" + "\n");

                strSqlString.Append("                                HAVING (" + "\n");
                strSqlString.Append("                                        NVL(SUM(F.V0+F.V1+F.V2+F.V3+F.V4+F.V5+F.V6+F.V7+F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14+F.V15+F.V16), 0)  " + "\n");
                strSqlString.Append("                                       ) <> 0" + "\n");
                strSqlString.Append("                               ) " + "\n");
                strSqlString.Append("                         GROUP BY MAT_ID, CUTOFF_DAY, OPER_GRP " + "\n");
                strSqlString.Append("                       ) " + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID, OPER_GRP " + "\n");
                strSqlString.Append("               ) A " + "\n");

                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT A.MAT_ID " + "\n");
                strSqlString.Append("                     , SUM(NVL(A.MON_PLAN,0)) AS MON_PLAN " + "\n");
                strSqlString.Append("                     , SUM(NVL(A.ORI_PLAN,0)) AS ORI_PLAN " + "\n");
                strSqlString.Append("                     , ROUND(((SUM(NVL(A.MON_PLAN,0)) * " + jindoPer + ") / 100),0) AS TARGET_MON " + "\n");
                strSqlString.Append("                     , SUM(NVL(A.ASSY_MON,0)) AS ASSY_MON " + "\n");
                strSqlString.Append("                     , SUM(NVL(A.ASSY_MON_SHIP,0)) AS ASSY_MON_SHIP " + "\n");
                strSqlString.Append("                     , SUM(NVL(A.ASSY_MON,0)) - ROUND(((SUM(NVL(A.MON_PLAN,0)) * " + jindoPer + ") / 100),1) AS DEF   " + "\n");

                if (lblRemain.Text != "0 day")  // 잔여일이 0 일이 아닐때
                {
                    strSqlString.Append("                     , ROUND(((SUM(NVL(A.MON_PLAN,0)) - SUM(NVL(A.ASSY_MON,0))) /" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "), 1) AS TARGET_DAY " + "\n");
                }
                else  // 잔여일이 0 일 일대
                {
                    strSqlString.Append("                     , 0 AS TARGET_DAY " + "\n");
                }

                strSqlString.Append("                  FROM ( " + "\n");
                strSqlString.Append("                        SELECT MAT.MAT_ID " + "\n");
                strSqlString.Append("                             , CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SUM(PLAN.RESV_FIELD1)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0)" + "\n");
                strSqlString.Append("                                    ELSE SUM(PLAN.RESV_FIELD1)" + "\n");
                strSqlString.Append("                                END MON_PLAN" + "\n");
                strSqlString.Append("                             , CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SUM(PLAN.PLAN_QTY_ASSY)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0)" + "\n");
                strSqlString.Append("                                    ELSE SUM(PLAN.PLAN_QTY_ASSY)" + "\n");
                strSqlString.Append("                                END ORI_PLAN" + "\n");
                strSqlString.Append("                             , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(SUM(MON_AO.ASSY_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0)" + "\n");
                strSqlString.Append("                                    ELSE SUM(MON_AO.ASSY_MON)" + "\n");
                strSqlString.Append("                                END ASSY_MON" + "\n");
                strSqlString.Append("                             , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(SUM(MON_AO_SHIP.ASSY_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0)" + "\n");
                strSqlString.Append("                                    ELSE SUM(MON_AO_SHIP.ASSY_MON)" + "\n");
                strSqlString.Append("                                END ASSY_MON_SHIP" + "\n");
                strSqlString.Append("                          FROM MWIPMATDEF MAT " + "\n");
                strSqlString.Append("                             , ( " + "\n");
                strSqlString.Append("                                SELECT FACTORY,MAT_ID,PLAN_QTY_ASSY,PLAN_MONTH, RESV_FIELD1 " + "\n");
                strSqlString.Append("                                  FROM ( " + "\n");
                strSqlString.Append("                                        SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, PLAN_MONTH, SUM(RESV_FIELD1) AS RESV_FIELD1  " + "\n");
                strSqlString.Append("                                          FROM ( " + "\n");
                strSqlString.Append("                                                SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, PLAN_MONTH, SUM(TO_NUMBER(DECODE(RESV_FIELD1,' ',0,RESV_FIELD1))) AS RESV_FIELD1 " + "\n");
                strSqlString.Append("                                                  FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                                 GROUP BY FACTORY, MAT_ID, PLAN_MONTH " + "\n");
                strSqlString.Append("                                               ) " + "\n");
                strSqlString.Append("                                         GROUP BY FACTORY, MAT_ID,PLAN_MONTH " + "\n");
                strSqlString.Append("                                       ) " + "\n");
                strSqlString.Append("                               ) PLAN " + "\n");
                strSqlString.Append("                             , ( " + "\n");
                strSqlString.Append("                                SELECT MAT_ID " + "\n");

                // 매월 1일, 2일 경우 3일자 가져온 데이터로 WORK_DATE 사용하며 월 A/O 값에서 전월 데이터 뺀다.
                //if (date.Substring(6, 2).Equals("01"))
                //{
                //    strSqlString.Append("                                     , SUM(DECODE(SUBSTR(WORK_DATE,0,6),'" + month + "', NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0),0)) AS ASSY_MON  " + "\n");
                //    strSqlString.Append("                                  FROM RSUMFACMOV " + "\n");
                //    strSqlString.Append("                                 WHERE 1=1 " + "\n");
                //    strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + dayArry2[0] + "' AND '" + date + "'" + "\n");
                //}
                //else
                //{
                strSqlString.Append("                                     , SUM(DECODE(WORK_DATE,'" + Last_Month_Last_day + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                strSqlString.Append("                                  FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + date + "'" + "\n");
                //}
                strSqlString.Append("                                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                   AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                   AND CM_KEY_2 = 'PROD' " + "\n");

                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                {
                    strSqlString.AppendFormat("                                   AND CM_KEY_3 LIKE '{0}'" + "\n", txtSearchProduct.Text);
                }

                strSqlString.Append("                                   AND FACTORY NOT IN ('RETURN') " + "\n");
                strSqlString.Append("                                 GROUP BY MAT_ID " + "\n");
                strSqlString.Append("                               ) MON_AO " + "\n");
                strSqlString.Append("                             , ( " + "\n");
                strSqlString.Append("                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(WORK_DATE,'" + Last_Month_Last_day + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                strSqlString.Append("                                  FROM CSUMFACMOV " + "\n");
                strSqlString.Append("                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + date + "'" + "\n");
                strSqlString.Append("                                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                   AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                   AND CM_KEY_2 = 'PROD' " + "\n");
                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                {
                    strSqlString.AppendFormat("                                   AND CM_KEY_3 LIKE '{0}'" + "\n", txtSearchProduct.Text);
                }
                strSqlString.Append("                                   AND FACTORY NOT IN ('RETURN') " + "\n");
                strSqlString.Append("                                 GROUP BY MAT_ID " + "\n");
                strSqlString.Append("                               ) MON_AO_HX " + "\n");
                strSqlString.Append("                             , ( " + "\n");
                strSqlString.Append("                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(WORK_DATE,'" + Last_Month_Last_day + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                strSqlString.Append("                                  FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                                 WHERE 1=1 " + "\n");
                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd"))
                {
                    strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + cdvDate.Value.AddDays(-1).ToString("yyyyMMdd") + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + date + "'" + "\n");
                }
                strSqlString.Append("                                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                   AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                   AND CM_KEY_2 = 'PROD' " + "\n");

                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                {
                    strSqlString.AppendFormat("                                   AND CM_KEY_3 LIKE '{0}'" + "\n", txtSearchProduct.Text);
                }

                strSqlString.Append("                                   AND FACTORY NOT IN ('RETURN') " + "\n");
                strSqlString.Append("                                 GROUP BY MAT_ID " + "\n");
                strSqlString.Append("                               ) MON_AO_SHIP " + "\n");
                strSqlString.Append("                             , ( " + "\n");
                strSqlString.Append("                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(WORK_DATE,'" + Last_Month_Last_day + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                strSqlString.Append("                                  FROM CSUMFACMOV " + "\n");
                strSqlString.Append("                                 WHERE 1=1 " + "\n");
                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd"))
                {
                    strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + cdvDate.Value.AddDays(-1).ToString("yyyyMMdd") + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + date + "'" + "\n");
                }
                strSqlString.Append("                                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                   AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                   AND CM_KEY_2 = 'PROD' " + "\n");
                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                {
                    strSqlString.AppendFormat("                                   AND CM_KEY_3 LIKE '{0}'" + "\n", txtSearchProduct.Text);
                }
                strSqlString.Append("                                   AND FACTORY NOT IN ('RETURN') " + "\n");
                strSqlString.Append("                                 GROUP BY MAT_ID " + "\n");
                strSqlString.Append("                               ) MON_AO_HX_SHIP " + "\n");
                strSqlString.Append("                         WHERE 1 = 1 " + "\n");
                strSqlString.Append("                           AND MAT.FACTORY =PLAN.FACTORY(+) " + "\n");
                strSqlString.Append("                           AND MAT.MAT_ID = PLAN.MAT_ID(+) " + "\n");
                strSqlString.Append("                           AND MAT.MAT_ID = MON_AO.MAT_ID(+)  " + "\n");
                strSqlString.Append("                           AND MAT.MAT_ID = MON_AO_HX.MAT_ID(+) " + "\n");
                strSqlString.Append("                           AND MAT.MAT_ID = MON_AO_SHIP.MAT_ID(+)  " + "\n");
                strSqlString.Append("                           AND MAT.MAT_ID = MON_AO_HX_SHIP.MAT_ID(+) " + "\n");
                strSqlString.Append("                           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                           AND PLAN.PLAN_MONTH(+) = '" + month + "' " + "\n");
                strSqlString.Append("                           AND MAT.MAT_TYPE= 'FG' " + "\n");
                strSqlString.Append("                           AND MAT.DELETE_FLAG <> 'Y' " + "\n");
                strSqlString.Append("                         GROUP BY MAT.MAT_ID, MAT.MAT_GRP_3, MAT.MAT_CMF_13 " + "\n");
                strSqlString.Append("                       ) A  " + "\n");
                strSqlString.Append("                 GROUP BY A.MAT_ID " + "\n");
                strSqlString.Append("               ) M " + "\n");
                strSqlString.Append("             , (  " + "\n");
                strSqlString.Append("                SELECT SAP_CODE, MIN(STOCK_TAT) AS STOCK_TAT, MIN(HMK3A_TAT) AS HMK3A_TAT, MIN(SAW_TAT) AS SAW_TAT, MIN(DA_TAT) AS DA_TAT, MIN(WB_TAT) AS WB_TAT, MIN(MOLD_TAT) AS MOLD_TAT, MIN(FINISH_TAT) AS FINISH_TAT, MIN(TOTAL_TAT) AS TAT  " + "\n");
                strSqlString.Append("                  FROM (  " + "\n");
                strSqlString.Append("                        SELECT " + QueryCond1 + ", SAP_CODE  " + "\n");
                strSqlString.Append("                             , MAX(STOCK_TAT) STOCK_TAT, MAX(HMK3A_TAT) HMK3A_TAT, MAX(SAW_TAT) SAW_TAT, MAX(DA_TAT) DA_TAT, MAX(WB_TAT) WB_TAT, MAX(MOLD_TAT) MOLD_TAT, MAX(FINISH_TAT) FINISH_TAT, MAX(SAW_TAT)+MAX(DA_TAT)+MAX(WB_TAT)+MAX(MOLD_TAT)+MAX(FINISH_TAT) AS TOTAL_TAT  " + "\n");
                strSqlString.Append("                             , RANK() OVER (PARTITION BY " + QueryCond1 + ", SAP_CODE ORDER BY MAX(SAW_TAT)+MAX(DA_TAT)+MAX(WB_TAT)+MAX(MOLD_TAT)+MAX(FINISH_TAT), SAP_CODE) AS RANK  " + "\n");
                strSqlString.Append("                          FROM (  " + "\n");
                strSqlString.Append("                                SELECT MAT.MAT_ID " + "\n");
                strSqlString.Append("                                     , " + QueryCond2 + ", OPER_GRP " + "\n");
                strSqlString.Append("                                     , MAT.VENDOR_ID  " + "\n");
                strSqlString.Append("                                     , OPER.SAP_CODE  " + "\n");
                strSqlString.Append("                                     , NVL(DECODE(OPER_GRP, 'STOCK',SUM(OPER.TAT)),0) STOCK_TAT " + "\n");
                strSqlString.Append("                                     , NVL(DECODE(OPER_GRP, 'SAW',SUM(OPER.TAT)),0) SAW_TAT  " + "\n");
                strSqlString.Append("                                     , NVL(DECODE(OPER_GRP, 'DA',SUM(OPER.TAT)),0) DA_TAT  " + "\n");
                strSqlString.Append("                                     , NVL(DECODE(OPER_GRP, 'WB',SUM(OPER.TAT)),0) WB_TAT  " + "\n");
                strSqlString.Append("                                     , NVL(DECODE(OPER_GRP, 'MOLD',SUM(OPER.TAT)),0) MOLD_TAT  " + "\n");
                strSqlString.Append("                                     , NVL(DECODE(OPER_GRP, 'FINISH',SUM(OPER.TAT)),0) FINISH_TAT   " + "\n");
                strSqlString.Append("                                     , NVL(DECODE(OPER_GRP, 'HMK3A',SUM(OPER.TAT)),0) HMK3A_TAT " + "\n");
                strSqlString.Append("                                  FROM MWIPMATDEF MAT   " + "\n");
                strSqlString.Append("                                     , (   " + "\n");
                strSqlString.Append("                                        SELECT A.FACTORY, A.OPER, A.SAP_CODE, DECODE(B.OPER_GRP_1, 'HMK3A', 'HMK3A', 'M/K', 'FINISH', 'V/I', 'FINISH', 'TRIM', 'FINISH', 'S/B/A', 'FINISH', 'TIN', 'FINISH', 'SIG', 'FINISH', 'AVI', 'FINISH', 'MOLD', 'MOLD', 'CURE', 'MOLD', 'W/B', 'WB', 'GATE', 'WB', 'D/A', 'DA', 'SMT', 'DA', 'S/P', 'DA', 'B/G', 'SAW', 'SAW', 'SAW', 'HMK2A', 'STOCK') OPER_GRP, SUM(A.TAT_DAY+A.TAT_DAY_WAIT) AS TAT   " + "\n");
                strSqlString.Append("                                          FROM CWIPSAPTAT@RPTTOMES A   " + "\n");
                strSqlString.Append("                                             , MWIPOPRDEF B   " + "\n");
                strSqlString.Append("                                         WHERE 1 = 1   " + "\n");
                strSqlString.Append("                                           AND A.RESV_FIELD_2 = ' '   " + "\n");
                strSqlString.Append("                                           AND A.FACTORY = B.FACTORY   " + "\n");
                strSqlString.Append("                                           AND A.OPER = B.OPER   " + "\n");
                strSqlString.Append("                                           AND OPER_GRP_1 IN ('HMK3A', 'M/K', 'V/I', 'TRIM', 'S/B/A', 'TIN', 'SIG', 'AVI', 'MOLD', 'CURE', 'W/B', 'GATE', 'D/A', 'SMT', 'S/P', 'B/G', 'SAW', 'HMK2A')   " + "\n");
                strSqlString.Append("                                         GROUP BY A.FACTORY, A.OPER, A.SAP_CODE, B.OPER_GRP_1   " + "\n");
                strSqlString.Append("                                       ) OPER  " + "\n");
                strSqlString.Append("                                 WHERE 1 = 1   " + "\n");
                strSqlString.Append("                                   AND MAT.VENDOR_ID = OPER.SAP_CODE(+)  " + "\n");
                strSqlString.Append("                                   AND OPER_GRP <> ' '   " + "\n");
                strSqlString.Append("                                 GROUP BY  MAT.MAT_ID, " + QueryCond3 + ", MAT.VENDOR_ID, OPER.SAP_CODE, OPER_GRP  " + "\n");
                strSqlString.Append("                               )   " + "\n");
                strSqlString.Append("                         GROUP BY " + QueryCond1 + ", SAP_CODE " + "\n");
                strSqlString.Append("                       )  " + "\n");
                strSqlString.Append("                 WHERE RANK = 1  " + "\n");
                strSqlString.Append("                 GROUP BY SAP_CODE " + "\n");
                strSqlString.Append("               ) T " + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");

                //상세 조회에 따른 SQL문 생성                        
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

                if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);

                if (cdvOperGroup.Text != "ALL")
                {
                    strSqlString.Append("           AND MAT.OPER_GRP " + cdvOperGroup.SelectedValueToQueryString + "\n");
                }

                strSqlString.Append("           AND MAT.MAT_ID = A.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.OPER_GRP = A.OPER_GRP(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = M.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.VENDOR_ID = T.SAP_CODE(+) " + "\n");
                strSqlString.Append("         GROUP BY " + QueryCond3 + ", MAT.OPER_GRP " + "\n");

                strSqlString.Append("   )  " + "\n");
                strSqlString.Append(" GROUP BY " + QueryCond1 + " " + "\n");
                strSqlString.Append("HAVING ( " + "\n");
                strSqlString.Append("        SUM(NVL(ORI_PLAN,0) + NVL(MON_PLAN,0) + NVL(TARGET_MON,0) + NVL(ASSY_MON,0))  +  " + "\n");
                strSqlString.Append("        SUM(NVL(DAY01,0) + NVL(DAY02,0) + NVL(DAY03,0) + NVL(DAY04,0) + NVL(DAY05,0) + NVL(DAY06,0) + NVL(DAY07,0) + NVL(DAY08,0) + NVL(DAY09,0) + NVL(DAY10,0) +  " + "\n");
                strSqlString.Append("            NVL(DAY11,0) + NVL(DAY12,0) + NVL(DAY13,0) + NVL(DAY14,0) + NVL(DAY15,0) + NVL(DAY16,0) + NVL(DAY17,0) + NVL(DAY18,0) + NVL(DAY19,0) + NVL(DAY20,0) +  " + "\n");
                strSqlString.Append("            NVL(DAY21,0) + NVL(DAY22,0) + NVL(DAY23,0) + NVL(DAY24,0) + NVL(DAY25,0) + NVL(DAY26,0) + NVL(DAY27,0) + NVL(DAY28,0) + NVL(DAY29,0) + NVL(DAY30,0) + NVL(DAY31,0)) " + "\n");
                strSqlString.Append("       ) <> 0 " + "\n");

                if (IsOperGroup)
                {
                    strSqlString.Append(" ORDER BY " + QueryCond4 + ", DECODE(OPER_GRP, 'STOCK', 1, 'SAW', 2, 'DA', 3, 'WB', 4, 'MOLD', 5, 'FINISH', 6, 'HMK3A', 7) " + "\n");
                }
                else
                {
                    strSqlString.Append(" ORDER BY " + QueryCond4 + " " + "\n");
                }
            }
            #endregion
            #region Bump 공장 Query
            else
            {
                strSqlString.Append("     , ROUND(MAX(ORI_PLAN)/" + strKpcs + ",0) AS \"월계획\"  " + "\n");
                strSqlString.Append("     , ROUND(MAX(MON_PLAN)/" + strKpcs + ",0) AS \"월계획Rev\"  " + "\n");
                strSqlString.Append("     , ROUND((MAX(MON_PLAN) - MAX(ORI_PLAN))/" + strKpcs + ",0) AS \"월계획 차이\"  " + "\n");
                strSqlString.Append("     , ROUND(MAX(TARGET_MON)/1000,0), ROUND(MAX(BUMP_MON)/" + strKpcs + ",0)  " + "\n");
                strSqlString.Append("     , DECODE(MAX(MON_PLAN), 0, 0, ROUND((MAX(BUMP_MON)/MAX(MON_PLAN))*100, 1)) JINDO " + "\n");
                strSqlString.Append("     , ROUND((MAX(MON_PLAN)-MAX(BUMP_MON))/" + strKpcs + ",0) AS \"잔량\"  " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(OPT_WIP,0))/" + strKpcs + ",0) AS OPT_WIP " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(DAY01,0))/" + strKpcs + ",0) AS DAY01, ROUND(SUM(NVL(DAY02,0))/" + strKpcs + ",0) AS DAY02, ROUND(SUM(NVL(DAY03,0))/" + strKpcs + ",0) AS DAY03, ROUND(SUM(NVL(DAY04,0))/" + strKpcs + ",0) AS DAY04, ROUND(SUM(NVL(DAY05,0))/" + strKpcs + ",0) AS DAY05, ROUND(SUM(NVL(DAY06,0))/" + strKpcs + ",0) AS DAY06, ROUND(SUM(NVL(DAY07,0))/" + strKpcs + ",0) AS DAY07, ROUND(SUM(NVL(DAY08,0))/" + strKpcs + ",0) AS DAY08, ROUND(SUM(NVL(DAY09,0))/" + strKpcs + ",0) AS DAY09, ROUND(SUM(NVL(DAY10,0))/" + strKpcs + ",0) AS DAY10   " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(DAY11,0))/" + strKpcs + ",0) AS DAY11, ROUND(SUM(NVL(DAY12,0))/" + strKpcs + ",0) AS DAY12, ROUND(SUM(NVL(DAY13,0))/" + strKpcs + ",0) AS DAY13, ROUND(SUM(NVL(DAY14,0))/" + strKpcs + ",0) AS DAY14, ROUND(SUM(NVL(DAY15,0))/" + strKpcs + ",0) AS DAY15, ROUND(SUM(NVL(DAY16,0))/" + strKpcs + ",0) AS DAY16, ROUND(SUM(NVL(DAY17,0))/" + strKpcs + ",0) AS DAY17, ROUND(SUM(NVL(DAY18,0))/" + strKpcs + ",0) AS DAY18, ROUND(SUM(NVL(DAY19,0))/" + strKpcs + ",0) AS DAY19, ROUND(SUM(NVL(DAY20,0))/" + strKpcs + ",0) AS DAY20   " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(DAY21,0))/" + strKpcs + ",0) AS DAY21, ROUND(SUM(NVL(DAY22,0))/" + strKpcs + ",0) AS DAY22, ROUND(SUM(NVL(DAY23,0))/" + strKpcs + ",0) AS DAY23, ROUND(SUM(NVL(DAY24,0))/" + strKpcs + ",0) AS DAY24, ROUND(SUM(NVL(DAY25,0))/" + strKpcs + ",0) AS DAY25, ROUND(SUM(NVL(DAY26,0))/" + strKpcs + ",0) AS DAY26, ROUND(SUM(NVL(DAY27,0))/" + strKpcs + ",0) AS DAY27, ROUND(SUM(NVL(DAY28,0))/" + strKpcs + ",0) AS DAY28, ROUND(SUM(NVL(DAY29,0))/" + strKpcs + ",0) AS DAY29, ROUND(SUM(NVL(DAY30,0))/" + strKpcs + ",0) AS DAY30, ROUND(SUM(NVL(DAY31,0))/" + strKpcs + ",0) AS DAY31 " + "\n");

                strSqlString.Append("     , ROUND(MAX(WIP_DAY),1) AS WIP_DAY " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(TAT,0)),2) AS TAT " + "\n");

                strSqlString.Append("\n");
                strSqlString.Append("  FROM ( " + "\n");


                strSqlString.Append("        SELECT " + QueryCond2 + ", MAT.OPER_GRP AS OPER_GRP" + "\n");

                strSqlString.Append("             , SUM(NVL(M.ORI_PLAN,0)) AS ORI_PLAN " + "\n");
                strSqlString.Append("             , SUM(NVL(M.MON_PLAN,0)) AS MON_PLAN " + "\n");
                strSqlString.Append("             , SUM(NVL(M.TARGET_MON,0)) AS TARGET_MON " + "\n");
                strSqlString.Append("             , SUM(NVL(M.BUMP_MON,0)) AS BUMP_MON " + "\n");
                strSqlString.Append("             , SUM(NVL(M.BUMP_MON_SHIP,0)) AS BUMP_MON_SHIP " + "\n");

                if (ckbRemain.Checked == false)
                {
                    strSqlString.Append("             , CASE WHEN SUM(NVL(M.MON_PLAN,0))=0 THEN 0 " + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN MAT.OPER_GRP='STOCK' THEN DECODE(MIN(T.STOCK_TAT),0, 0, ((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.STOCK_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='RCF' THEN DECODE(MIN(T.RCF_TAT),0, 0, ((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.RCF_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='RDL1' THEN DECODE(MIN(T.RDL1_TAT),0, 0, ((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.RDL1_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='PSV1' THEN DECODE(MIN(T.PSV1_TAT),0, 0, ((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.PSV1_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='RDL2' THEN DECODE(MIN(T.RDL2_TAT),0, 0, ((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.RDL2_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='PSV2' THEN DECODE(MIN(T.PSV2_TAT),0, 0, ((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.PSV2_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='RDL3' THEN DECODE(MIN(T.RDL3_TAT),0, 0, ((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.RDL3_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='PSV3' THEN DECODE(MIN(T.PSV3_TAT),0, 0, ((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.PSV3_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='BUMP' THEN DECODE(MIN(T.BUMP_TAT),0, 0, ((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.BUMP_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='AVI' THEN DECODE(MIN(T.AVI_TAT),0, 0, ((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.AVI_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='HMK3B' THEN DECODE(MIN(T.HMK3B_TAT),0, 0, ((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.HMK3B_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              ELSE 0 " + "\n");
                    strSqlString.Append("                          END " + "\n");
                    strSqlString.Append("                END OPT_WIP " + "\n");
                }
                else
                {
                    strSqlString.Append("             , CASE WHEN SUM(NVL(MON_PLAN,0))=0 THEN 0 " + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN MAT.OPER_GRP='STOCK' THEN DECODE(MIN(T.STOCK_TAT),0, 0, ((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.STOCK_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='RCF' THEN DECODE(MIN(T.RCF_TAT),0, 0, ((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.RCF_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='RDL1' THEN DECODE(MIN(T.RDL1_TAT),0, 0, ((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.RDL1_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='PSV1' THEN DECODE(MIN(T.PSV1_TAT),0, 0, ((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.PSV1_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='RDL2' THEN DECODE(MIN(T.RDL2_TAT),0, 0, ((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.RDL2_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='PSV2' THEN DECODE(MIN(T.PSV2_TAT),0, 0, ((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.PSV2_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='RDL3' THEN DECODE(MIN(T.RDL3_TAT),0, 0, ((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.RDL3_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='PSV3' THEN DECODE(MIN(T.PSV3_TAT),0, 0, ((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.PSV3_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='BUMP' THEN DECODE(MIN(T.BUMP_TAT),0, 0, ((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.BUMP_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='AVI' THEN DECODE(MIN(T.AVI_TAT),0, 0, ((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.AVI_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='HMK3B' THEN DECODE(MIN(T.HMK3B_TAT),0, 0, ((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.HMK3B_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                          END " + "\n");
                    strSqlString.Append("                END OPT_WIP " + "\n");
                }

                strSqlString.Append("             , SUM(NVL(A.DAY01,0)) AS DAY01, SUM(NVL(A.DAY02,0)) AS DAY02, SUM(NVL(A.DAY03,0)) AS DAY03, SUM(NVL(A.DAY04,0)) AS DAY04, SUM(NVL(A.DAY05,0)) AS DAY05, SUM(NVL(A.DAY06,0)) AS DAY06, SUM(NVL(A.DAY07,0)) AS DAY07, SUM(NVL(A.DAY08,0)) AS DAY08, SUM(NVL(A.DAY09,0)) AS DAY09, SUM(NVL(A.DAY10,0)) AS DAY10 " + "\n");
                strSqlString.Append("             , SUM(NVL(A.DAY11,0)) AS DAY11, SUM(NVL(A.DAY12,0)) AS DAY12, SUM(NVL(A.DAY13,0)) AS DAY13, SUM(NVL(A.DAY14,0)) AS DAY14, SUM(NVL(A.DAY15,0)) AS DAY15, SUM(NVL(A.DAY16,0)) AS DAY16, SUM(NVL(A.DAY17,0)) AS DAY17, SUM(NVL(A.DAY18,0)) AS DAY18, SUM(NVL(A.DAY19,0)) AS DAY19, SUM(NVL(A.DAY20,0)) AS DAY20 " + "\n");
                strSqlString.Append("             , SUM(NVL(A.DAY21,0)) AS DAY21, SUM(NVL(A.DAY22,0)) AS DAY22, SUM(NVL(A.DAY23,0)) AS DAY23, SUM(NVL(A.DAY24,0)) AS DAY24, SUM(NVL(A.DAY25,0)) AS DAY25, SUM(NVL(A.DAY26,0)) AS DAY26, SUM(NVL(A.DAY27,0)) AS DAY27, SUM(NVL(A.DAY28,0)) AS DAY28, SUM(NVL(A.DAY29,0)) AS DAY29, SUM(NVL(A.DAY30,0)) AS DAY30, SUM(NVL(A.DAY31,0)) AS DAY31 " + "\n");

                if (ckbRemain.Checked == false)
                {
                    strSqlString.Append("             , CASE WHEN SUM(NVL(M.MON_PLAN,0))=0 THEN 0 " + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN MAT.OPER_GRP='STOCK' THEN DECODE(MIN(T.STOCK_TAT),0, 0, SUM(NVL(A.DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.STOCK_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='RCF' THEN DECODE(MIN(T.RCF_TAT),0, 0, SUM(NVL(A.DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.RCF_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='RDL1' THEN DECODE(MIN(T.RDL1_TAT),0, 0, SUM(NVL(A.DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.RDL1_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='PSV1' THEN DECODE(MIN(T.PSV1_TAT),0, 0, SUM(NVL(A.DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.PSV1_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='RDL2' THEN DECODE(MIN(T.RDL2_TAT),0, 0, SUM(NVL(A.DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.RDL2_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='PSV2' THEN DECODE(MIN(T.PSV2_TAT),0, 0, SUM(NVL(A.DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.PSV2_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='RDL3' THEN DECODE(MIN(T.RDL3_TAT),0, 0, SUM(NVL(A.DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.RDL3_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='PSV3' THEN DECODE(MIN(T.PSV3_TAT),0, 0, SUM(NVL(A.DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.PSV3_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='BUMP' THEN DECODE(MIN(T.BUMP_TAT),0, 0, SUM(NVL(A.DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.BUMP_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='AVI' THEN DECODE(MIN(T.AVI_TAT),0, 0, SUM(NVL(A.DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.AVI_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='HMK3B' THEN DECODE(MIN(T.HMK3B_TAT),0, 0, SUM(NVL(A.DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(M.MON_PLAN,0))/" + last_day.Substring(6, 2) + ")*MIN(T.HMK3B_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              ELSE 0 " + "\n");
                    strSqlString.Append("                          END " + "\n");
                    strSqlString.Append("                END WIP_DAY " + "\n");
                }
                else
                {
                    strSqlString.Append("             , CASE WHEN SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0))=0 THEN 0 " + "\n");
                    strSqlString.Append("                    ELSE CASE WHEN MAT.OPER_GRP='STOCK' THEN DECODE(MIN(T.STOCK_TAT),0, 0, SUM(NVL(DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.STOCK_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='RCF' THEN  DECODE(MIN(T.RCF_TAT),0, 0, SUM(NVL(DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.RCF_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='RDL1' THEN  DECODE(MIN(T.RDL1_TAT),0, 0, SUM(NVL(DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.RDL1_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='PSV1' THEN  DECODE(MIN(T.PSV1_TAT),0, 0, SUM(NVL(DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.PSV1_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='RDL2' THEN  DECODE(MIN(T.RDL2_TAT),0, 0, SUM(NVL(DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.RDL2_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='PSV2' THEN  DECODE(MIN(T.PSV2_TAT),0, 0, SUM(NVL(DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.PSV2_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='RDL3' THEN  DECODE(MIN(T.RDL3_TAT),0, 0, SUM(NVL(DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.RDL3_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='PSV3' THEN  DECODE(MIN(T.PSV3_TAT),0, 0, SUM(NVL(DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.PSV3_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='BUMP' THEN  DECODE(MIN(T.BUMP_TAT),0, 0, SUM(NVL(DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.BUMP_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='AVI' THEN  DECODE(MIN(T.AVI_TAT),0, 0, SUM(NVL(DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.AVI_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                              WHEN MAT.OPER_GRP='HMK3B' THEN  DECODE(MIN(T.HMK3B_TAT),0, 0, SUM(NVL(DAY" + string.Format("{0}", DateTime.Now.ToString("dd")) + ",0))/((SUM(NVL(MON_PLAN,0) - NVL(BUMP_MON_SHIP,0)))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "*MIN(T.HMK3B_TAT)*1.1)) " + "\n");
                    strSqlString.Append("                          END " + "\n");
                    strSqlString.Append("                END WIP_DAY " + "\n");
                }

                strSqlString.Append("             , MIN(CASE WHEN MAT.OPER_GRP='STOCK' THEN T.STOCK_TAT " + "\n");
                strSqlString.Append("                        WHEN MAT.OPER_GRP='RCF' THEN T.RCF_TAT " + "\n");
                strSqlString.Append("                        WHEN MAT.OPER_GRP='RDL1' THEN T.RDL1_TAT " + "\n");
                strSqlString.Append("                        WHEN MAT.OPER_GRP='PSV1' THEN T.PSV1_TAT " + "\n");
                strSqlString.Append("                        WHEN MAT.OPER_GRP='RDL2' THEN T.RDL2_TAT " + "\n");
                strSqlString.Append("                        WHEN MAT.OPER_GRP='PSV2' THEN T.PSV2_TAT " + "\n");
                strSqlString.Append("                        WHEN MAT.OPER_GRP='RDL3' THEN T.RDL3_TAT " + "\n");
                strSqlString.Append("                        WHEN MAT.OPER_GRP='PSV3' THEN T.PSV3_TAT " + "\n");
                strSqlString.Append("                        WHEN MAT.OPER_GRP='BUMP' THEN T.BUMP_TAT " + "\n");
                strSqlString.Append("                        WHEN MAT.OPER_GRP='AVI' THEN T.AVI_TAT " + "\n");
                strSqlString.Append("                        WHEN MAT.OPER_GRP='HMK3B' THEN T.HMK3B_TAT " + "\n");
                strSqlString.Append("                        ELSE 0 " + "\n");
                strSqlString.Append("                    END) TAT " + "\n");

                strSqlString.Append("      FROM ( " + "\n");
                strSqlString.Append("            SELECT MAT.*, OPER.OPER_GRP " + "\n");
                strSqlString.Append("              FROM MWIPMATDEF MAT " + "\n");
                strSqlString.Append("                 , ( " + "\n");
                strSqlString.Append("                     SELECT DISTINCT A.FACTORY, DECODE(A.OPER_GRP_1, 'OGI', 'HMK3B', 'PACKING', 'HMK3B', 'HMK3B', 'HMK3B', 'HMK2B', 'STOCK', 'IQC', 'STOCK', 'I-STOCK', 'STOCK', 'RCF_PHOTO', 'RCF', 'RDL1_SPUTTER', 'RDL1', 'RDL1_PHOTO', 'RDL1', 'RDL1_PLAT', 'RDL1', 'RDL1_ETCH', 'RDL1', 'PSV1_PHOTO', 'PSV1', 'RDL2_SPUTTER', 'RDL2', 'RDL2_PHOTO', 'RDL2', 'RDL2_PLAT', 'RDL2', 'RDL2_ETCH', 'RDL2', 'PSV2_PHOTO', 'PSV2', 'RDL3_SPUTTER', 'RDL3', 'RDL3_PHOTO', 'RDL3', 'RDL3_PLAT', 'RDL3', 'RDL3_ETCH', 'RDL3', 'PSV3_PHOTO', 'PSV3', 'BUMP_SPUTTER', 'BUMP', 'BUMP_PHOTO', 'BUMP', 'BUMP_CU_PLAT', 'BUMP', 'BUMP_SNAG_PLAT', 'BUMP', 'BUMP_ETCH', 'BUMP', 'BUMP_BALL_MOUNT', 'BUMP', 'BUMP_REFLOW', 'BUMP', 'FINAL_INSP', 'BUMP', 'SORT', 'AVI', 'AVI', 'AVI') OPER_GRP " + "\n");
                strSqlString.Append("                       FROM  MWIPOPRDEF A " + "\n");
                strSqlString.Append("                      WHERE 1 = 1 " + "\n");
                strSqlString.Append("                        AND FACTORY = '" + cdvFactory.Text.Trim() + "' " + "\n");
                strSqlString.Append("                        AND OPER_GRP_1 IN ('HMK2B','IQC','I-STOCK','RCF_PHOTO','RDL1_SPUTTER','RDL1_PHOTO','RDL1_PLAT','RDL1_ETCH','PSV1_PHOTO','RDL2_SPUTTER','RDL2_PHOTO','RDL2_PLAT','RDL2_ETCH','PSV2_PHOTO','RDL3_SPUTTER','RDL3_PHOTO','RDL3_PLAT','RDL3_ETCH','PSV3_PHOTO','BUMP_SPUTTER','BUMP_PHOTO','BUMP_CU_PLAT','BUMP_SNAG_PLAT','BUMP_ETCH','BUMP_BALL_MOUNT','BUMP_REFLOW','FINAL_INSP','SORT','AVI','OGI','PACKING','HMK3B') " + "\n");
                strSqlString.Append("                      GROUP BY A.FACTORY, A.OPER_GRP_1 " + "\n");
                strSqlString.Append("                    ) OPER " + "\n");
                strSqlString.Append("              WHERE 1 = 1 " + "\n");
                strSqlString.Append("                AND OPER_GRP <> ' ' " + "\n");
                strSqlString.Append("                AND MAT.VENDOR_ID <> ' ' " + "\n");
                strSqlString.Append("                AND MAT.FACTORY = '" + cdvFactory.Text.Trim() + "' " + "\n");
                strSqlString.Append("                AND MAT.DELETE_FLAG = ' ' " + "\n");
                strSqlString.Append("           ) MAT " + "\n");
                strSqlString.Append("         , (  " + "\n");
                strSqlString.Append("            SELECT MAT_ID, OPER_GRP " + "\n");

                for (int i = 1; i <= 31; i++)
                {
                    strSqlString.Append(string.Format("                     , SUM(NVL(DECODE(CUTOFF_DAY,'{0}{1:D2}', QTY),0)) AS DAY{2:D2} \n", month, i, i));
                }

                strSqlString.Append("                  FROM ( " + "\n");
                strSqlString.Append("                        SELECT MAT_ID, CUTOFF_DAY, OPER_GRP " + "\n");
                strSqlString.Append("                             , SUM(NVL(DECODE(OPER_GRP, 'STOCK', STOCK, 'RCF', RCF, 'RDL1', RDL1, 'PSV1', PSV1, 'RDL2', RDL2, 'PSV2', PSV2, 'RDL3', RDL3, 'PSV3', PSV3, 'BUMP', BUMP, 'AVI', AVI, 'HMK3B', HMK3B),0)) AS QTY " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                SELECT A.MAT_ID \n");
                strSqlString.Append("                                     , F.CUTOFF_DAY AS CUTOFF_DAY, DECODE(OPER_GRP_1, 'OGI', 'HMK3B', 'PACKING', 'HMK3B', 'HMK3B', 'HMK3B', 'HMK2B', 'STOCK', 'IQC', 'STOCK', 'I-STOCK', 'STOCK', 'RCF_PHOTO', 'RCF', 'RDL1_SPUTTER', 'RDL1', 'RDL1_PHOTO', 'RDL1', 'RDL1_PLAT', 'RDL1', 'RDL1_ETCH', 'RDL1', 'PSV1_PHOTO', 'PSV1', 'RDL2_SPUTTER', 'RDL2', 'RDL2_PHOTO', 'RDL2', 'RDL2_PLAT', 'RDL2', 'RDL2_ETCH', 'RDL2', 'PSV2_PHOTO', 'PSV2', 'RDL3_SPUTTER', 'RDL3', 'RDL3_PHOTO', 'RDL3', 'RDL3_PLAT', 'RDL3', 'RDL3_ETCH', 'RDL3', 'PSV3_PHOTO', 'PSV3', 'BUMP_SPUTTER', 'BUMP', 'BUMP_PHOTO', 'BUMP', 'BUMP_CU_PLAT', 'BUMP', 'BUMP_SNAG_PLAT', 'BUMP', 'BUMP_ETCH', 'BUMP', 'BUMP_BALL_MOUNT', 'BUMP', 'BUMP_REFLOW', 'BUMP', 'FINAL_INSP', 'BUMP', 'SORT', 'AVI', 'AVI', 'AVI') AS OPER_GRP " + "\n");
                strSqlString.Append("                                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 = 'DHL' THEN CASE WHEN MAT_GRP_5 IN ('3rd','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V0,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + cdvFactory.Text.Trim() + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN CASE WHEN A.MAT_GRP_5 IN ('2nd','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V0,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V0,0) ELSE 0 END  " + "\n");
                strSqlString.Append("                                                     ELSE NVL(F.V0,0)+NVL(F.V1,0)+NVL(F.V2,0) " + "\n");
                strSqlString.Append("                                                 END),0) " + "\n");
                strSqlString.Append("                                          ) AS STOCK " + "\n");
                strSqlString.Append("                                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 = 'DHL' THEN CASE WHEN MAT_GRP_5 IN ('3rd','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V1,0)+NVL(F.V2,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + cdvFactory.Text.Trim() + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V1,0)+NVL(F.V2,0), 'Merge', NVL(F.V1,0)+NVL(F.V2,0), 0) " + "\n");
                strSqlString.Append("                                                     WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V1,0)+NVL(F.V2,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     ELSE NVL(F.V3,0) " + "\n");
                strSqlString.Append("                                                END),0) " + "\n");
                strSqlString.Append("                                          ) AS RCF " + "\n");
                strSqlString.Append("                                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 = 'DHL' THEN CASE WHEN MAT_GRP_5 IN ('3rd','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V3,0)+NVL(F.V4,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + cdvFactory.Text.Trim() + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V3,0)+NVL(F.V4,0), 'Merge', NVL(F.V3,0)+NVL(F.V4,0), 0) " + "\n");
                strSqlString.Append("                                                     WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V3,0)+NVL(F.V4,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     ELSE NVL(F.V4,0)+NVL(F.V5,0)+NVL(F.V6,0)+NVL(F.V7,0) " + "\n");
                strSqlString.Append("                                                 END),0) " + "\n");
                strSqlString.Append("                                          ) AS RDL1 " + "\n");
                strSqlString.Append("                                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 = 'DHL' THEN CASE WHEN MAT_GRP_5 IN ('3rd','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V5+F.V16,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + cdvFactory.Text.Trim() + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V5+F.V16,0), 'Merge', NVL(F.V5+F.V16,0), 0) " + "\n");
                strSqlString.Append("                                                     WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V5+F.V16,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     ELSE NVL(F.V8,0) " + "\n");
                strSqlString.Append("                                                 END),0) " + "\n");
                strSqlString.Append("                                          ) AS PSV1 " + "\n");
                strSqlString.Append("                                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 = 'DHL' THEN CASE WHEN MAT_GRP_5 IN ('3rd','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V6+F.V7,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + cdvFactory.Text.Trim() + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V6+F.V7,0), 'Merge', NVL(F.V6+F.V7,0), 0) " + "\n");
                strSqlString.Append("                                                     WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V6+F.V7,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     ELSE NVL(F.V9,0)+NVL(F.V10,0)+NVL(F.V11,0)+NVL(F.V12,0) " + "\n");
                strSqlString.Append("                                                 END),0) " + "\n");
                strSqlString.Append("                                          ) AS RDL2 " + "\n");
                strSqlString.Append("                                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 = 'DHL' THEN CASE WHEN MAT_GRP_5 IN ('3rd','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V6+F.V7,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + cdvFactory.Text.Trim() + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V6+F.V7,0), 'Merge', NVL(F.V6+F.V7,0), 0) " + "\n");
                strSqlString.Append("                                                     WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V6+F.V7,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     ELSE NVL(F.V13,0) " + "\n");
                strSqlString.Append("                                                 END),0) " + "\n");
                strSqlString.Append("                                          ) AS PSV2 " + "\n");
                strSqlString.Append("                                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 = 'DHL' THEN CASE WHEN MAT_GRP_5 IN ('3rd','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V6+F.V7,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + cdvFactory.Text.Trim() + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V6+F.V7,0), 'Merge', NVL(F.V6+F.V7,0), 0) " + "\n");
                strSqlString.Append("                                                     WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V6+F.V7,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     ELSE NVL(F.V14,0)+NVL(F.V15,0)+NVL(F.V16,0)+NVL(F.V17,0) " + "\n");
                strSqlString.Append("                                                 END),0) " + "\n");
                strSqlString.Append("                                          ) AS RDL3 " + "\n");
                strSqlString.Append("                                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 = 'DHL' THEN CASE WHEN MAT_GRP_5 IN ('3rd','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V6+F.V7,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + cdvFactory.Text.Trim() + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V6+F.V7,0), 'Merge', NVL(F.V6+F.V7,0), 0) " + "\n");
                strSqlString.Append("                                                     WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V6+F.V7,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     ELSE NVL(F.V18,0) " + "\n");
                strSqlString.Append("                                                 END),0) " + "\n");
                strSqlString.Append("                                          ) AS PSV3 " + "\n");
                strSqlString.Append("                                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 = 'DHL' THEN CASE WHEN MAT_GRP_5 IN ('3rd','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + cdvFactory.Text.Trim() + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14,0), 'Merge', NVL(F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14,0), 0) " + "\n");
                strSqlString.Append("                                                     WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     ELSE NVL(F.V19,0)+NVL(F.V20,0)+NVL(F.V21,0)+NVL(F.V22,0)+NVL(F.V23,0)+NVL(F.V24,0)+NVL(F.V25,0)+NVL(F.V26,0) " + "\n");
                strSqlString.Append("                                                 END),0) " + "\n");
                strSqlString.Append("                                          ) AS BUMP " + "\n");
                strSqlString.Append("                                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 = 'DHL' THEN CASE WHEN MAT_GRP_5 IN ('3rd','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + cdvFactory.Text.Trim() + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14,0), 'Merge', NVL(F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14,0), 0) " + "\n");
                strSqlString.Append("                                                     WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     ELSE NVL(F.V27,0)+NVL(F.V28,0) " + "\n");
                strSqlString.Append("                                                 END),0) " + "\n");
                strSqlString.Append("                                          ) AS AVI " + "\n");
                strSqlString.Append("                                     , SUM(NVL((CASE WHEN A.MAT_CMF_11 = 'DHL' THEN CASE WHEN MAT_GRP_5 IN ('3rd','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V15,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + cdvFactory.Text.Trim() + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(F.V15,0), 'Merge', NVL(F.V15,0), 0) " + "\n");
                strSqlString.Append("                                                     WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V15,0) ELSE 0 END " + "\n");
                strSqlString.Append("                                                     ELSE NVL(F.V29,0)+NVL(F.V30,0)+NVL(F.V31,0) " + "\n");
                strSqlString.Append("                                                 END),0) " + "\n");
                strSqlString.Append("                                          ) AS HMK3B " + "\n");
                strSqlString.Append("                                           " + "\n");

                strSqlString.Append("                                  FROM ( " + "\n");
                strSqlString.Append("                                        SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8 " + "\n");
                strSqlString.Append("                                             , DECODE(MAT.MAT_GRP_1,'SE',MAT.MAT_GRP_9,' ') AS MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7, MAT.MAT_CMF_13, MAT.MAT_CMF_11  " + "\n");

                strSqlString.Append("                                          FROM MWIPMATDEF MAT " + "\n");

                //월 계획 SLIS 제품은 MP계획과 동일하게            
                strSqlString.Append("                                             , ( " + "\n");
                strSqlString.Append("                                                SELECT FACTORY,MAT_ID,PLAN_QTY_BUMP,PLAN_MONTH, RESV_FIELD1 " + "\n");
                strSqlString.Append("                                                  FROM ( " + "\n");
                strSqlString.Append("                                                        SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_BUMP) AS PLAN_QTY_BUMP, PLAN_MONTH, SUM(RESV_FIELD1) AS RESV_FIELD1  " + "\n");
                strSqlString.Append("                                                          FROM ( " + "\n");
                strSqlString.Append("                                                                SELECT 'HMKB1' AS FACTORY, MAT_ID, SUM(TO_NUMBER(DECODE(RESV_FIELD7,' ',0,RESV_FIELD7))) AS PLAN_QTY_BUMP, PLAN_MONTH, SUM(TO_NUMBER(DECODE(RESV_FIELD8,' ',0,RESV_FIELD8))) AS RESV_FIELD1 " + "\n");
                strSqlString.Append("                                                                  FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                                                   AND ((RESV_FIELD7 <> ' ' AND RESV_FIELD7 <> '0') OR (RESV_FIELD8 <> ' ' AND RESV_FIELD8 <> '0')) " + "\n");
                strSqlString.Append("                                                                 GROUP BY FACTORY, MAT_ID, PLAN_MONTH " + "\n");
                strSqlString.Append("                                                               ) " + "\n");
                strSqlString.Append("                                                         GROUP BY FACTORY, MAT_ID,PLAN_MONTH " + "\n");
                strSqlString.Append("                                                       ) " + "\n");
                strSqlString.Append("                                               ) PLAN " + "\n");
                strSqlString.Append("                                         WHERE 1 = 1 " + "\n");
                strSqlString.Append("                                           AND MAT.FACTORY =PLAN.FACTORY(+) " + "\n");
                strSqlString.Append("                                           AND MAT.MAT_ID = PLAN.MAT_ID(+) " + "\n");

                strSqlString.Append("                                           AND MAT.FACTORY = '" + cdvFactory.Text.Trim() + "' " + "\n");
                strSqlString.Append("                                           AND PLAN.PLAN_MONTH(+) = '" + month + "' " + "\n");
                strSqlString.Append("                                           AND MAT.MAT_TYPE= 'FG' " + "\n");
                strSqlString.Append("                                           AND MAT.DELETE_FLAG <> 'Y' " + "\n");
                strSqlString.Append("                                         GROUP BY MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7, MAT.MAT_CMF_13, MAT.MAT_CMF_11" + "\n");
                strSqlString.Append("                                       ) A  " + "\n");
                strSqlString.Append("                                       " + "\n");

                strSqlString.Append("                                     , ( " + "\n");
                strSqlString.Append("                                        SELECT LOT.MAT_ID, OPER_GRP_1, MAT.MAT_GRP_3, LOT.CUTOFF_DAY " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'HMK2B', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V0 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'IQC', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V1 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'I-STOCK', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V2 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'RCF_PHOTO', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V3 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'RDL1_SPUTTER', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V4 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'RDL1_PHOTO', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V5 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'RDL1_PLAT', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V6 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'RDL1_ETCH', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V7 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'PSV1_PHOTO', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V8 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'RDL2_SPUTTER', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V9 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'RDL2_PHOTO', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V10 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'RDL2_PLAT', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V11 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'RDL2_ETCH', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V12 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'PSV2_PHOTO', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V13 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'RDL3_SPUTTER', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V14 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'RDL3_PHOTO', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V15 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'RDL3_PLAT', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V16 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'RDL3_ETCH', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V17 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'PSV3_PHOTO', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V18 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'BUMP_SPUTTER', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V19 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'BUMP_PHOTO', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V20 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'BUMP_CU_PLAT', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V21 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'BUMP_SNAG_PLAT', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V22 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'BUMP_ETCH', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V23 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'BUMP_BALL_MOUNT', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V24 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'BUMP_REFLOW', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V25 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'FINAL_INSP', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V26 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'SORT', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V27 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'AVI', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V28 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'OGI', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V29 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'PACKING', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V30 " + "\n");
                strSqlString.Append("                                             , SUM(DECODE(OPER_GRP_1, 'HMK3B', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V31 " + "\n");
                strSqlString.Append("                                          FROM (  " + "\n");
                strSqlString.Append("                                                SELECT FACTORY, MAT_ID, OPER_GRP_1, SUBSTR(CUTOFF_DT, 1, 8) AS CUTOFF_DAY " + "\n");
                strSqlString.Append("                                                     , SUM(CASE WHEN OPER <= 'BZ999' THEN QTY_1 / NVL(COMP_CNT,1) " + "\n");   /// ???????? A0395
                strSqlString.Append("                                                                ELSE QTY_1 " + "\n");
                strSqlString.Append("                                                            END) QTY " + "\n");
                strSqlString.Append("                                                  FROM (" + "\n");
                strSqlString.Append("                                                        SELECT A.CUTOFF_DT, A.FACTORY, A.MAT_ID, B.OPER_GRP_1, B.OPER, A.QTY_1 " + "\n");
                strSqlString.Append("                                                             , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + cdvFactory.Text.Trim() + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') AND KEY_1 = A.MAT_ID) AS COMP_CNT " + "\n");
                strSqlString.Append("                                                          FROM (" + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == Select_date.ToString("yyyyMMdd"))
                {
                    strSqlString.Append("                                                                  SELECT '" + Select_date.ToString("yyyyMMdd") + hour + "' AS CUTOFF_DT, A.*, 0 AS REJECT_QTY, 0 AS CV_QTY " + "\n");
                    strSqlString.Append("                                                                    FROM RWIPLOTSTS A " + "\n");
                    strSqlString.Append("                                                                   UNION ALL " + "\n");
                    strSqlString.Append("                                                                  SELECT * " + "\n");
                    strSqlString.Append("                                                                    FROM RWIPLOTSTS_BOH B " + "\n");
                    strSqlString.Append("                                                                   WHERE CUTOFF_DT IN (");

                    for (int i = 1; i < Convert.ToInt32(Select_date.ToString("dd")); i++)
                    {
                        if (i != 1)
                            strSqlString.Append(",");

                        strSqlString.Append(string.Format("'{0}{1:D2}{2}'", month, i, hour));
                    }

                    strSqlString.Append(") \n");

                }
                else
                {
                    strSqlString.Append("                                                                  SELECT * " + "\n");
                    strSqlString.Append("                                                                    FROM RWIPLOTSTS_BOH" + "\n");
                    strSqlString.Append("                                                                   WHERE CUTOFF_DT IN (");

                    for (int i = 1; i <= Convert.ToInt32(Select_date.ToString("dd")); i++)
                    {
                        if (i != 1)
                            strSqlString.Append(",");

                        strSqlString.Append(string.Format("'{0}{1:D2}{2}'", month, i, hour));
                    }

                    strSqlString.Append(") \n");

                }

                strSqlString.Append("                                                                 ) A " + "\n");
                strSqlString.Append("                                                               , MWIPOPRDEF B " + "\n");
                strSqlString.Append("                                                           WHERE 1 = 1 " + "\n");
                strSqlString.Append("                                                             AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                                                             AND A.OPER = B.OPER(+) " + "\n");

                int lastday = Convert.ToInt32(last_day.Substring(6, 2));

                strSqlString.Append("                                                             AND A.FACTORY = '" + cdvFactory.Text.Trim() + "' " + "\n");
                strSqlString.Append("                                                             AND A.MAT_VER = 1 " + "\n");
                strSqlString.Append("                                                             AND A.LOT_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("                                                             AND A.LOT_TYPE = 'W' " + "\n");

                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                {
                    strSqlString.AppendFormat("                                                       AND A.LOT_CMF_5 LIKE '{0}'" + "\n", txtSearchProduct.Text);
                }
                strSqlString.Append("                                                       ) " + "\n");
                strSqlString.Append("                                                 GROUP BY FACTORY, MAT_ID, OPER_GRP_1, SUBSTR(CUTOFF_DT, 1, 8)   " + "\n");
                strSqlString.Append("                                               ) LOT " + "\n");
                strSqlString.Append("                                             , MWIPMATDEF MAT " + "\n");
                strSqlString.Append("                                         WHERE 1 = 1 " + "\n");
                strSqlString.Append("                                           AND LOT.FACTORY = MAT.FACTORY " + "\n");
                strSqlString.Append("                                           AND LOT.MAT_ID = MAT.MAT_ID " + "\n");
                strSqlString.Append("                                           AND MAT.DELETE_FLAG <> 'Y' " + "\n");
                strSqlString.Append("                                           AND MAT.MAT_GRP_2 <> '-' " + "\n");
                strSqlString.Append("                                         GROUP BY LOT.MAT_ID ,OPER_GRP_1, MAT.MAT_GRP_3, LOT.CUTOFF_DAY  " + "\n");
                strSqlString.Append("                                       ) F " + "\n");
                strSqlString.Append("                                     , (" + "\n");
                strSqlString.Append("                                        SELECT KEY_1,DATA_1" + "\n");
                strSqlString.Append("                                          FROM MGCMTBLDAT " + "\n");
                strSqlString.Append("                                         WHERE FACTORY = '" + cdvFactory.Text.Trim() + "' " + "\n");
                strSqlString.Append("                                           AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')" + "\n");
                strSqlString.Append("                                       ) G" + "\n");
                strSqlString.Append("                                 WHERE 1 = 1 " + "\n");
                strSqlString.Append("                                   AND A.MAT_ID = F.MAT_ID(+)" + "\n");
                strSqlString.Append("                                   AND A.MAT_ID = G.KEY_1(+)" + "\n");
                strSqlString.AppendFormat("                                   AND A.MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");
                strSqlString.Append("                                 GROUP BY A.MAT_ID ,F.CUTOFF_DAY, DECODE(OPER_GRP_1, 'OGI', 'HMK3B', 'PACKING', 'HMK3B', 'HMK3B', 'HMK3B', 'HMK2B', 'STOCK', 'IQC', 'STOCK', 'I-STOCK', 'STOCK', 'RCF_PHOTO', 'RCF', 'RDL1_SPUTTER', 'RDL1', 'RDL1_PHOTO', 'RDL1', 'RDL1_PLAT', 'RDL1', 'RDL1_ETCH', 'RDL1', 'PSV1_PHOTO', 'PSV1', 'RDL2_SPUTTER', 'RDL2', 'RDL2_PHOTO', 'RDL2', 'RDL2_PLAT', 'RDL2', 'RDL2_ETCH', 'RDL2', 'PSV2_PHOTO', 'PSV2', 'RDL3_SPUTTER', 'RDL3', 'RDL3_PHOTO', 'RDL3', 'RDL3_PLAT', 'RDL3', 'RDL3_ETCH', 'RDL3', 'PSV3_PHOTO', 'PSV3', 'BUMP_SPUTTER', 'BUMP', 'BUMP_PHOTO', 'BUMP', 'BUMP_CU_PLAT', 'BUMP', 'BUMP_SNAG_PLAT', 'BUMP', 'BUMP_ETCH', 'BUMP', 'BUMP_BALL_MOUNT', 'BUMP', 'BUMP_REFLOW', 'BUMP', 'FINAL_INSP', 'BUMP', 'SORT', 'AVI', 'AVI', 'AVI')" + "\n");

                strSqlString.Append("                                HAVING (" + "\n");
                strSqlString.Append("                                        NVL(SUM(F.V0+F.V1+F.V2+F.V3+F.V4+F.V5+F.V6+F.V7+F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14+F.V15+F.V16+F.V17+F.V18+F.V19+F.V20+F.V21+F.V22+F.V23+F.V24+F.V25+F.V26+F.V27+F.V28+F.V29+F.V30+F.V31), 0)  " + "\n");
                strSqlString.Append("                                       ) <> 0" + "\n");
                strSqlString.Append("                               ) " + "\n");
                strSqlString.Append("                         GROUP BY MAT_ID, CUTOFF_DAY, OPER_GRP " + "\n");
                strSqlString.Append("                       ) " + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID, OPER_GRP " + "\n");
                strSqlString.Append("               ) A " + "\n");

                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT A.MAT_ID " + "\n");
                strSqlString.Append("                     , SUM(NVL(A.MON_PLAN,0)) AS MON_PLAN " + "\n");
                strSqlString.Append("                     , SUM(NVL(A.ORI_PLAN,0)) AS ORI_PLAN " + "\n");
                strSqlString.Append("                     , ROUND(((SUM(NVL(A.MON_PLAN,0)) * " + jindoPer + ") / 100),0) AS TARGET_MON " + "\n");
                strSqlString.Append("                     , SUM(NVL(A.BUMP_MON,0)) AS BUMP_MON " + "\n");
                strSqlString.Append("                     , SUM(NVL(A.BUMP_MON_SHIP,0)) AS BUMP_MON_SHIP " + "\n");
                strSqlString.Append("                     , SUM(NVL(A.BUMP_MON,0)) - ROUND(((SUM(NVL(A.MON_PLAN,0)) * " + jindoPer + ") / 100),1) AS DEF   " + "\n");

                if (lblRemain.Text != "0 day")  // 잔여일이 0 일이 아닐때
                {
                    strSqlString.Append("                     , ROUND(((SUM(NVL(A.MON_PLAN,0)) - SUM(NVL(A.BUMP_MON,0))) /" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "), 1) AS TARGET_DAY " + "\n");
                }
                else  // 잔여일이 0 일 일대
                {
                    strSqlString.Append("                     , 0 AS TARGET_DAY " + "\n");
                }

                strSqlString.Append("                  FROM ( " + "\n");
                strSqlString.Append("                        SELECT MAT.MAT_ID " + "\n");
                strSqlString.Append("                             , CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SUM(PLAN.RESV_FIELD1)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0)" + "\n");
                strSqlString.Append("                                    ELSE SUM(PLAN.RESV_FIELD1)" + "\n");
                strSqlString.Append("                                END MON_PLAN" + "\n");
                strSqlString.Append("                             , CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SUM(PLAN.PLAN_QTY_BUMP)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0)" + "\n");
                strSqlString.Append("                                    ELSE SUM(PLAN.PLAN_QTY_BUMP)" + "\n");
                strSqlString.Append("                                END ORI_PLAN" + "\n");
                strSqlString.Append("                             , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(SUM(MON_BO.BUMP_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0)" + "\n");
                strSqlString.Append("                                    ELSE SUM(MON_BO.BUMP_MON)" + "\n");
                strSqlString.Append("                                END BUMP_MON" + "\n");
                strSqlString.Append("                             , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(SUM(MON_BO_SHIP.BUMP_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0)" + "\n");
                strSqlString.Append("                                    ELSE SUM(MON_BO_SHIP.BUMP_MON)" + "\n");
                strSqlString.Append("                                END BUMP_MON_SHIP" + "\n");
                strSqlString.Append("                          FROM MWIPMATDEF MAT " + "\n");
                strSqlString.Append("                             , ( " + "\n");
                strSqlString.Append("                                SELECT FACTORY,MAT_ID,PLAN_QTY_BUMP,PLAN_MONTH, RESV_FIELD1 " + "\n");
                strSqlString.Append("                                  FROM ( " + "\n");
                strSqlString.Append("                                        SELECT 'HMKB1' AS FACTORY, MAT_ID, SUM(PLAN_QTY_BUMP) AS PLAN_QTY_BUMP, PLAN_MONTH, SUM(RESV_FIELD1) AS RESV_FIELD1  " + "\n");
                strSqlString.Append("                                          FROM ( " + "\n");
                strSqlString.Append("                                                SELECT 'HMKB1' AS FACTORY, MAT_ID, SUM(TO_NUMBER(DECODE(RESV_FIELD7,' ',0,RESV_FIELD7))) AS PLAN_QTY_BUMP, PLAN_MONTH, SUM(TO_NUMBER(DECODE(RESV_FIELD8,' ',0,RESV_FIELD8))) AS RESV_FIELD1 " + "\n");
                strSqlString.Append("                                                  FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                                   AND ((RESV_FIELD7 <> ' ' AND RESV_FIELD7 <> '0') OR (RESV_FIELD8 <> ' ' AND RESV_FIELD8 <> '0')) " + "\n");
                strSqlString.Append("                                                 GROUP BY FACTORY, MAT_ID, PLAN_MONTH " + "\n");
                strSqlString.Append("                                               ) " + "\n");
                strSqlString.Append("                                         GROUP BY FACTORY, MAT_ID,PLAN_MONTH " + "\n");
                strSqlString.Append("                                       ) " + "\n");
                strSqlString.Append("                               ) PLAN " + "\n");
                strSqlString.Append("                             , ( " + "\n");
                strSqlString.Append("                                SELECT MAT_ID " + "\n");

                // 매월 1일, 2일 경우 3일자 가져온 데이터로 WORK_DATE 사용하며 월 A/O 값에서 전월 데이터 뺀다.
                //if (date.Substring(6, 2).Equals("01"))
                //{
                //    strSqlString.Append("                                     , SUM(DECODE(SUBSTR(WORK_DATE,0,6),'" + month + "', NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0),0)) AS BUMP_MON  " + "\n");
                //    strSqlString.Append("                                  FROM RSUMFACMOV " + "\n");
                //    strSqlString.Append("                                 WHERE 1=1 " + "\n");
                //    strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + dayArry2[0] + "' AND '" + date + "'" + "\n");
                //}
                //else
                //{
                strSqlString.Append("                                     , SUM(DECODE(WORK_DATE,'" + Last_Month_Last_day + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS BUMP_MON " + "\n");
                strSqlString.Append("                                  FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + date + "'" + "\n");
                //}
                strSqlString.Append("                                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                   AND CM_KEY_1 = '" + cdvFactory.Text.Trim() + "' " + "\n");
                strSqlString.Append("                                   AND CM_KEY_2 = 'PROD' " + "\n");

                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                {
                    strSqlString.AppendFormat("                                   AND CM_KEY_3 LIKE '{0}'" + "\n", txtSearchProduct.Text);
                }

                strSqlString.Append("                                   AND FACTORY NOT IN ('RETURN') " + "\n");
                strSqlString.Append("                                 GROUP BY MAT_ID " + "\n");
                strSqlString.Append("                               ) MON_BO " + "\n");
                strSqlString.Append("                             , ( " + "\n");
                strSqlString.Append("                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(WORK_DATE,'" + Last_Month_Last_day + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS BUMP_MON " + "\n");
                strSqlString.Append("                                  FROM CSUMFACMOV " + "\n");
                strSqlString.Append("                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + date + "'" + "\n");
                strSqlString.Append("                                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                   AND CM_KEY_1 = '" + cdvFactory.Text.Trim() + "' " + "\n");
                strSqlString.Append("                                   AND CM_KEY_2 = 'PROD' " + "\n");
                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                {
                    strSqlString.AppendFormat("                                   AND CM_KEY_3 LIKE '{0}'" + "\n", txtSearchProduct.Text);
                }
                strSqlString.Append("                                   AND FACTORY NOT IN ('RETURN') " + "\n");
                strSqlString.Append("                                 GROUP BY MAT_ID " + "\n");
                strSqlString.Append("                               ) MON_BO_HX " + "\n");
                strSqlString.Append("                             , ( " + "\n");
                strSqlString.Append("                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(WORK_DATE,'" + Last_Month_Last_day + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS BUMP_MON " + "\n");
                strSqlString.Append("                                  FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                                 WHERE 1=1 " + "\n");
                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd"))
                {
                    strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + cdvDate.Value.AddDays(-1).ToString("yyyyMMdd") + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + date + "'" + "\n");
                }
                strSqlString.Append("                                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                   AND CM_KEY_1 = '" + cdvFactory.Text.Trim() + "' " + "\n");
                strSqlString.Append("                                   AND CM_KEY_2 = 'PROD' " + "\n");

                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                {
                    strSqlString.AppendFormat("                                   AND CM_KEY_3 LIKE '{0}'" + "\n", txtSearchProduct.Text);
                }

                strSqlString.Append("                                   AND FACTORY NOT IN ('RETURN') " + "\n");
                strSqlString.Append("                                 GROUP BY MAT_ID " + "\n");
                strSqlString.Append("                               ) MON_BO_SHIP " + "\n");
                strSqlString.Append("                             , ( " + "\n");
                strSqlString.Append("                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                     , SUM(DECODE(WORK_DATE,'" + Last_Month_Last_day + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS BUMP_MON " + "\n");
                strSqlString.Append("                                  FROM CSUMFACMOV " + "\n");
                strSqlString.Append("                                 WHERE 1=1 " + "\n");
                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd"))
                {
                    strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + cdvDate.Value.AddDays(-1).ToString("yyyyMMdd") + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + date + "'" + "\n");
                }
                strSqlString.Append("                                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                   AND CM_KEY_1 = '" + cdvFactory.Text.Trim() + "' " + "\n");
                strSqlString.Append("                                   AND CM_KEY_2 = 'PROD' " + "\n");
                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                {
                    strSqlString.AppendFormat("                                   AND CM_KEY_3 LIKE '{0}'" + "\n", txtSearchProduct.Text);
                }
                strSqlString.Append("                                   AND FACTORY NOT IN ('RETURN') " + "\n");
                strSqlString.Append("                                 GROUP BY MAT_ID " + "\n");
                strSqlString.Append("                               ) MON_BO_HX_SHIP " + "\n");
                strSqlString.Append("                         WHERE 1 = 1 " + "\n");
                strSqlString.Append("                           AND MAT.FACTORY =PLAN.FACTORY(+) " + "\n");
                strSqlString.Append("                           AND MAT.MAT_ID = PLAN.MAT_ID(+) " + "\n");
                strSqlString.Append("                           AND MAT.MAT_ID = MON_BO.MAT_ID(+)  " + "\n");
                strSqlString.Append("                           AND MAT.MAT_ID = MON_BO_HX.MAT_ID(+) " + "\n");
                strSqlString.Append("                           AND MAT.MAT_ID = MON_BO_SHIP.MAT_ID(+)  " + "\n");
                strSqlString.Append("                           AND MAT.MAT_ID = MON_BO_HX_SHIP.MAT_ID(+) " + "\n");
                strSqlString.Append("                           AND MAT.FACTORY = '" + cdvFactory.Text.Trim() + "' " + "\n");
                strSqlString.Append("                           AND PLAN.PLAN_MONTH(+) = '" + month + "' " + "\n");
                strSqlString.Append("                           AND MAT.MAT_TYPE= 'FG' " + "\n");
                strSqlString.Append("                           AND MAT.DELETE_FLAG <> 'Y' " + "\n");
                strSqlString.Append("                         GROUP BY MAT.MAT_ID, MAT.MAT_GRP_3, MAT.MAT_CMF_13 " + "\n");
                strSqlString.Append("                       ) A  " + "\n");
                strSqlString.Append("                     , (" + "\n");
                strSqlString.Append("                        SELECT KEY_1,DATA_1" + "\n");
                strSqlString.Append("                          FROM MGCMTBLDAT " + "\n");
                strSqlString.Append("                         WHERE FACTORY = '" + cdvFactory.Text.Trim() + "' " + "\n");
                strSqlString.Append("                           AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')" + "\n");
                strSqlString.Append("                       ) G" + "\n");
                strSqlString.Append("                 WHERE 1 = 1 " + "\n");
                strSqlString.Append("                   AND A.MAT_ID = G.KEY_1(+)" + "\n");
                strSqlString.Append("                 GROUP BY A.MAT_ID " + "\n");
                strSqlString.Append("               ) M " + "\n");
                strSqlString.Append("             , (  " + "\n");
                strSqlString.Append("                SELECT SAP_CODE, MIN(STOCK_TAT) AS STOCK_TAT, MIN(HMK3B_TAT) AS HMK3B_TAT, MIN(RCF_TAT) RCF_TAT, MIN(RDL1_TAT) RDL1_TAT, MIN(PSV1_TAT) PSV1_TAT, MIN(RDL2_TAT) RDL2_TAT, MIN(PSV2_TAT) PSV2_TAT, MIN(RDL3_TAT) RDL3_TAT, MIN(PSV3_TAT) PSV3_TAT, MIN(BUMP_TAT) BUMP_TAT, MIN(AVI_TAT) AVI_TAT, MIN(TOTAL_TAT) AS TAT  " + "\n");
                strSqlString.Append("                  FROM (  " + "\n");
                strSqlString.Append("                        SELECT " + QueryCond1 + ", SAP_CODE  " + "\n");
                strSqlString.Append("                             , MAX(STOCK_TAT) STOCK_TAT, MAX(HMK3B_TAT) HMK3B_TAT, MAX(RCF_TAT) RCF_TAT, MAX(RDL1_TAT) RDL1_TAT, MAX(PSV1_TAT) PSV1_TAT, MAX(RDL2_TAT) RDL2_TAT, MAX(PSV2_TAT) PSV2_TAT, MAX(RDL3_TAT) RDL3_TAT, MAX(PSV3_TAT) PSV3_TAT, MAX(BUMP_TAT) BUMP_TAT, MAX(AVI_TAT) AVI_TAT, MAX(RCF_TAT)+MAX(RDL1_TAT)+MAX(PSV1_TAT)+MAX(RDL2_TAT)+MAX(PSV2_TAT)+MAX(RDL3_TAT)+MAX(PSV3_TAT)+MAX(BUMP_TAT)+MAX(AVI_TAT) AS TOTAL_TAT  " + "\n");
                strSqlString.Append("                             , RANK() OVER (PARTITION BY " + QueryCond1 + ", SAP_CODE ORDER BY MAX(RCF_TAT)+MAX(RDL1_TAT)+MAX(PSV1_TAT)+MAX(RDL2_TAT)+MAX(PSV2_TAT)+MAX(RDL3_TAT)+MAX(PSV3_TAT)+MAX(BUMP_TAT), SAP_CODE) AS RANK  " + "\n");
                strSqlString.Append("                          FROM (  " + "\n");
                strSqlString.Append("                                SELECT MAT.MAT_ID " + "\n");
                strSqlString.Append("                                     , " + QueryCond2 + ", OPER_GRP " + "\n");
                strSqlString.Append("                                     , MAT.VENDOR_ID  " + "\n");
                strSqlString.Append("                                     , OPER.SAP_CODE  " + "\n");
                strSqlString.Append("                                     , NVL(DECODE(OPER_GRP, 'STOCK',SUM(OPER.TAT)),0) STOCK_TAT " + "\n");
                strSqlString.Append("                                     , NVL(DECODE(OPER_GRP, 'RCF',SUM(OPER.TAT)),0) RCF_TAT  " + "\n");
                strSqlString.Append("                                     , NVL(DECODE(OPER_GRP, 'RDL1',SUM(OPER.TAT)),0) RDL1_TAT  " + "\n");
                strSqlString.Append("                                     , NVL(DECODE(OPER_GRP, 'PSV1',SUM(OPER.TAT)),0) PSV1_TAT  " + "\n");
                strSqlString.Append("                                     , NVL(DECODE(OPER_GRP, 'RDL2',SUM(OPER.TAT)),0) RDL2_TAT  " + "\n");
                strSqlString.Append("                                     , NVL(DECODE(OPER_GRP, 'PSV2',SUM(OPER.TAT)),0) PSV2_TAT  " + "\n");
                strSqlString.Append("                                     , NVL(DECODE(OPER_GRP, 'RDL3',SUM(OPER.TAT)),0) RDL3_TAT  " + "\n");
                strSqlString.Append("                                     , NVL(DECODE(OPER_GRP, 'PSV3',SUM(OPER.TAT)),0) PSV3_TAT  " + "\n");
                strSqlString.Append("                                     , NVL(DECODE(OPER_GRP, 'BUMP',SUM(OPER.TAT)),0) BUMP_TAT  " + "\n");
                strSqlString.Append("                                     , NVL(DECODE(OPER_GRP, 'AVI',SUM(OPER.TAT)),0) AVI_TAT   " + "\n");
                strSqlString.Append("                                     , NVL(DECODE(OPER_GRP, 'HMK3B',SUM(OPER.TAT)),0) HMK3B_TAT " + "\n");
                strSqlString.Append("                                  FROM MWIPMATDEF MAT   " + "\n");
                strSqlString.Append("                                     , (   " + "\n");
                strSqlString.Append("                                        SELECT A.FACTORY, A.OPER, A.SAP_CODE, DECODE(B.OPER_GRP_1, 'OGI', 'HMK3B', 'PACKING', 'HMK3B', 'HMK3B', 'HMK3B', 'HMK2B', 'STOCK', 'IQC', 'STOCK', 'I-STOCK', 'STOCK', 'RCF_PHOTO', 'RCF', 'RDL1_SPUTTER', 'RDL1', 'RDL1_PHOTO', 'RDL1', 'RDL1_PLAT', 'RDL1', 'RDL1_ETCH', 'RDL1', 'PSV1_PHOTO', 'PSV1', 'RDL2_SPUTTER', 'RDL2', 'RDL2_PHOTO', 'RDL2', 'RDL2_PLAT', 'RDL2', 'RDL2_ETCH', 'RDL2', 'PSV2_PHOTO', 'PSV2', 'RDL3_SPUTTER', 'RDL3', 'RDL3_PHOTO', 'RDL3', 'RDL3_PLAT', 'RDL3', 'RDL3_ETCH', 'RDL3', 'PSV3_PHOTO', 'PSV3', 'BUMP_SPUTTER', 'BUMP', 'BUMP_PHOTO', 'BUMP', 'BUMP_CU_PLAT', 'BUMP', 'BUMP_SNAG_PLAT', 'BUMP', 'BUMP_ETCH', 'BUMP', 'BUMP_BALL_MOUNT', 'BUMP', 'BUMP_REFLOW', 'BUMP', 'FINAL_INSP', 'BUMP', 'SORT', 'AVI', 'AVI', 'AVI') OPER_GRP, SUM(A.TAT_DAY+A.TAT_DAY_WAIT) AS TAT   " + "\n");
                strSqlString.Append("                                          FROM CWIPSAPTAT@RPTTOMES A   " + "\n");
                strSqlString.Append("                                             , MWIPOPRDEF B   " + "\n");
                strSqlString.Append("                                         WHERE 1 = 1   " + "\n");
                strSqlString.Append("                                           AND A.RESV_FIELD_2 = ' '   " + "\n");
                strSqlString.Append("                                           AND A.FACTORY = B.FACTORY   " + "\n");
                strSqlString.Append("                                           AND A.FACTORY = '" + cdvFactory.Text.Trim() + "' " + "\n");
                strSqlString.Append("                                           AND A.OPER = B.OPER   " + "\n");
                strSqlString.Append("                                           AND OPER_GRP_1 IN ('HMK2B','IQC','I-STOCK','RCF_PHOTO','RDL1_SPUTTER','RDL1_PHOTO','RDL1_PLAT','RDL1_ETCH','PSV1_PHOTO','RDL2_SPUTTER','RDL2_PHOTO','RDL2_PLAT','RDL2_ETCH','PSV2_PHOTO','RDL3_SPUTTER','RDL3_PHOTO','RDL3_PLAT','RDL3_ETCH','PSV3_PHOTO','BUMP_SPUTTER','BUMP_PHOTO','BUMP_CU_PLAT','BUMP_SNAG_PLAT','BUMP_ETCH','BUMP_BALL_MOUNT','BUMP_REFLOW','FINAL_INSP','SORT','AVI','OGI','PACKING','HMK3B')   " + "\n");
                strSqlString.Append("                                         GROUP BY A.FACTORY, A.OPER, A.SAP_CODE, B.OPER_GRP_1   " + "\n");
                strSqlString.Append("                                       ) OPER  " + "\n");
                strSqlString.Append("                                 WHERE 1 = 1   " + "\n");
                strSqlString.Append("                                   AND MAT.VENDOR_ID = OPER.SAP_CODE(+)  " + "\n");
                strSqlString.Append("                                   AND OPER_GRP <> ' '   " + "\n");
                strSqlString.Append("                                 GROUP BY  MAT.MAT_ID, " + QueryCond3 + ", MAT.VENDOR_ID, OPER.SAP_CODE, OPER_GRP  " + "\n");
                strSqlString.Append("                               )   " + "\n");
                strSqlString.Append("                         GROUP BY " + QueryCond1 + ", SAP_CODE " + "\n");
                strSqlString.Append("                       )  " + "\n");
                strSqlString.Append("                 WHERE RANK = 1  " + "\n");
                strSqlString.Append("                 GROUP BY SAP_CODE " + "\n");
                strSqlString.Append("               ) T " + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");

                //상세 조회에 따른 SQL문 생성  
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

                if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);

                if (cdvOperGroup.Text != "ALL")
                {
                    strSqlString.Append("           AND MAT.OPER_GRP " + cdvOperGroup.SelectedValueToQueryString + "\n");
                }

                strSqlString.Append("           AND MAT.MAT_ID = A.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.OPER_GRP = A.OPER_GRP(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = M.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.VENDOR_ID = T.SAP_CODE(+) " + "\n");
                strSqlString.Append("         GROUP BY " + QueryCond3 + ", MAT.OPER_GRP " + "\n");

                strSqlString.Append("   )  " + "\n");
                strSqlString.Append(" GROUP BY " + QueryCond1 + " " + "\n");
                strSqlString.Append("HAVING ( " + "\n");
                strSqlString.Append("        SUM(NVL(ORI_PLAN,0) + NVL(MON_PLAN,0) + NVL(TARGET_MON,0) + NVL(BUMP_MON,0))  +  " + "\n");
                strSqlString.Append("        SUM(NVL(DAY01,0) + NVL(DAY02,0) + NVL(DAY03,0) + NVL(DAY04,0) + NVL(DAY05,0) + NVL(DAY06,0) + NVL(DAY07,0) + NVL(DAY08,0) + NVL(DAY09,0) + NVL(DAY10,0) +  " + "\n");
                strSqlString.Append("            NVL(DAY11,0) + NVL(DAY12,0) + NVL(DAY13,0) + NVL(DAY14,0) + NVL(DAY15,0) + NVL(DAY16,0) + NVL(DAY17,0) + NVL(DAY18,0) + NVL(DAY19,0) + NVL(DAY20,0) +  " + "\n");
                strSqlString.Append("            NVL(DAY21,0) + NVL(DAY22,0) + NVL(DAY23,0) + NVL(DAY24,0) + NVL(DAY25,0) + NVL(DAY26,0) + NVL(DAY27,0) + NVL(DAY28,0) + NVL(DAY29,0) + NVL(DAY30,0) + NVL(DAY31,0)) " + "\n");
                strSqlString.Append("       ) <> 0 " + "\n");

                if (IsOperGroup)
                {
                    strSqlString.Append(" ORDER BY " + QueryCond4 + ", DECODE(OPER_GRP, 'STOCK', 1, 'RCF', 2, 'RDL1', 3, 'PSV1', 4, 'RDL2', 5, 'PSV2', 6, 'RDL3', 7, 'PSV3', 8, 'BUMP', 9, 'AVI', 10, 'HMK3B', 11) " + "\n");
                }
                else
                {
                    strSqlString.Append(" ORDER BY " + QueryCond4 + " " + "\n");
                }
            }
            #endregion

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
        private void btnView_Click(object sender, EventArgs e)/////////////////////
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
                int nGroupCount = ((udcTableForm)(this.btnSort.BindingForm)).GetSelectedCount();

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 9, null, null, btnSort);
                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 9, 0, 1, true, Align.Center, VerticalAlign.Center);

                if (IsOperGroup())
                    HideColumn();
                    
                //spdData.RPT_AutoFit(false);

                if (cdvFactory.Text.Trim() != "HMKB1")
                {                    
                    spdData.RPT_SetPerSubTotalAndGrandTotal(1, 13, 9, 14);
                    spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 49, nGroupCount, false);                     
                    SetAvgVertical3(1, 48, false);
                }
                else
                {                    
                    spdData.RPT_SetPerSubTotalAndGrandTotal(1, 17, 13, 18);
                    spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 53, nGroupCount, false); 
                    SetAvgVertical3(1, 52, false);
                }
                
                CellColorChange();                

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
        /// 공정그룹 유무 확인
        /// </summary>
        /// <returns></returns>
        bool IsOperGroup()
        {
            bool rtnVal = false;

            for (int i = 0; i < spdData.ActiveSheet.ColumnHeader.Columns.Count; i++)
            {
                if ("Process group" == spdData.ActiveSheet.ColumnHeader.Columns[i].Label.ToString())
                {
                    rtnVal = true;
                    break;
                }
            }
            return rtnVal;
        }

        /// <summary>
        /// 공정그룹이 있을 경우 월계획, 컬럼 숨김
        /// </summary>
        void HideColumn()
        {
            if (cdvFactory.Text.Trim() != "HMKB1")
            {
                spdData.ActiveSheet.ColumnHeader.Columns[9].Visible = false;    //월계획
                spdData.ActiveSheet.ColumnHeader.Columns[10].Visible = false;   //월계획Rev
                spdData.ActiveSheet.ColumnHeader.Columns[13].Visible = false;   //실적
                spdData.ActiveSheet.ColumnHeader.Columns[14].Visible = false;   //진도율
                spdData.ActiveSheet.ColumnHeader.Columns[15].Visible = false;   //잔량
            }
            else
            {
                spdData.ActiveSheet.ColumnHeader.Columns[13].Visible = false;    //월계획
                spdData.ActiveSheet.ColumnHeader.Columns[14].Visible = false;   //월계획Rev
                spdData.ActiveSheet.ColumnHeader.Columns[17].Visible = false;   //실적
                spdData.ActiveSheet.ColumnHeader.Columns[18].Visible = false;   //진도율
                spdData.ActiveSheet.ColumnHeader.Columns[19].Visible = false;   //잔량
            }
        }

        /// <summary>
        /// 적정재공 이상으로 재공 보유 시 음영 표시 
        /// </summary>
        private void CellColorChange()
        {
            Color colorOver = System.Drawing.Color.FromArgb(((System.Byte)(242)), ((System.Byte)(220)), ((System.Byte)(219)));

            if (cdvFactory.Text.Trim() != "HMKB1")
            {
                if (ckbKpcs.Checked == false)
                {
                    int jeok = 0;//적정재공
                    int wip = 0;  //재공
                    int cntDay = Convert.ToInt32(cdvDate.Value.ToString("dd"));

                    for (int row = 0; row < spdData.ActiveSheet.RowCount; row++)
                    {

                        bool result = Int32.TryParse(spdData.ActiveSheet.GetText(row, 16).Replace(",", ""), out jeok);

                        for (int j = 17; j < 17 + cntDay; j++)
                        {
                            result = Int32.TryParse(spdData.ActiveSheet.GetText(row, j).Replace(",", ""), out wip);

                            if (wip > jeok)
                            {
                                spdData.ActiveSheet.Cells[row, j].BackColor = colorOver;
                            }
                        }
                    }
                }
                else
                {
                    Double jeok = 0.0f;//적정재공
                    Double wip = 0.0f;  //재공
                    int cntDay = Convert.ToInt32(cdvDate.Value.ToString("dd"));

                    for (int row = 0; row < spdData.ActiveSheet.RowCount; row++)
                    {

                        bool result = Double.TryParse(spdData.ActiveSheet.GetText(row, 16).Replace(",", ""), out jeok);

                        for (int j = 17; j < 17 + cntDay; j++)
                        {
                            result = Double.TryParse(spdData.ActiveSheet.GetText(row, j).Replace(",", ""), out wip);

                            if (wip > jeok)
                            {
                                spdData.ActiveSheet.Cells[row, j].BackColor = colorOver;
                            }
                        }
                    }
                }
            }
            else
            {
                if (ckbKpcs.Checked == false)
                {
                    int jeok = 0;//적정재공
                    int wip = 0;  //재공
                    int cntDay = Convert.ToInt32(cdvDate.Value.ToString("dd"));

                    for (int row = 0; row < spdData.ActiveSheet.RowCount; row++)
                    {

                        bool result = Int32.TryParse(spdData.ActiveSheet.GetText(row, 20).Replace(",", ""), out jeok);

                        for (int j = 21; j < 21 + cntDay; j++)
                        {
                            result = Int32.TryParse(spdData.ActiveSheet.GetText(row, j).Replace(",", ""), out wip);

                            if (wip > jeok)
                            {
                                spdData.ActiveSheet.Cells[row, j].BackColor = colorOver;
                            }
                        }
                    }
                }
                else
                {
                    Double jeok = 0.0f;//적정재공
                    Double wip = 0.0f;  //재공
                    int cntDay = Convert.ToInt32(cdvDate.Value.ToString("dd"));

                    for (int row = 0; row < spdData.ActiveSheet.RowCount; row++)
                    {

                        bool result = Double.TryParse(spdData.ActiveSheet.GetText(row, 20).Replace(",", ""), out jeok);

                        for (int j = 21; j < 21 + cntDay; j++)
                        {
                            result = Double.TryParse(spdData.ActiveSheet.GetText(row, j).Replace(",", ""), out wip);

                            if (wip > jeok)
                            {
                                spdData.ActiveSheet.Cells[row, j].BackColor = colorOver;
                            }
                        }
                    }
                }
            }
        }       

        /// <summary>
        /// 재공일수 구하기 (subtotal, grandtotal 의 현재공과 적정재공으로 재공일수를 구한다)
        /// </summary>
        /// <param name="nSampleNormalRowPos"></param>
        /// <param name="nColPos"></param>
        /// <param name="bWithNull"></param>
        public void SetAvgVertical3(int nSampleNormalRowPos, int nColPos, bool bWithNull)
        {
            if (cdvFactory.Text.Trim() != "HMKB1")
            {
                for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    if (!bWithNull && (spdData.ActiveSheet.Cells[i, nColPos].Value == null || spdData.ActiveSheet.Cells[i, nColPos].Value.ToString().Trim() == "" || spdData.ActiveSheet.Cells[i, nColPos].Value.ToString() == "0"))
                    {
                        continue;
                    }
                    else if (spdData.ActiveSheet.Cells[i, 16].Value == null || spdData.ActiveSheet.Cells[i, 16].Value.ToString().Trim() == "" || spdData.ActiveSheet.Cells[i, 16].Value.ToString() == "0")
                    {
                        continue;
                    }
                    else
                    {
                        spdData.ActiveSheet.Cells[i, nColPos].Value = Math.Round(Convert.ToDouble(spdData.ActiveSheet.Cells[i, nowCulumn].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[i, 16].Value), 1);
                    }
                }
            }
            else
            {
                for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    if (!bWithNull && (spdData.ActiveSheet.Cells[i, nColPos].Value == null || spdData.ActiveSheet.Cells[i, nColPos].Value.ToString().Trim() == "" || spdData.ActiveSheet.Cells[i, nColPos].Value.ToString() == "0"))
                    {
                        continue;
                    }
                    else if (spdData.ActiveSheet.Cells[i, 20].Value == null || spdData.ActiveSheet.Cells[i, 20].Value.ToString().Trim() == "" || spdData.ActiveSheet.Cells[i, 20].Value.ToString() == "0")
                    {
                        continue;
                    }
                    else
                    {
                        spdData.ActiveSheet.Cells[i, nColPos].Value = Math.Round(Convert.ToDouble(spdData.ActiveSheet.Cells[i, nowCulumn].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[i, 20].Value), 1);
                    }
                }
            }
            return;
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
                Condition.AppendFormat("기준일자: {0}     today: {1}      workday: {2}     remain: {3}      표준진도율: {4} " + "\n", cdvDate.Text, lblToday.Text.ToString(), lblLastDay.Text.ToString(), lblRemain.Text.ToString(), lblJindo.Text.ToString());
                //Condition.AppendFormat("today: {0}    workday: {1}     표준진도율: {2} " + lblToday.Text.ToString() , lblLastDay.Text.ToString(), lblJindo.Text.ToString());
                Condition.Append("        단위 : PKG (pcs) , COB (wafer) ");
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
            this.SetFactory(cdvFactory.txtValue);
            //cdvLotType.sFactory = cdvFactory.txtValue;
            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                BaseFormType = eBaseFormType.BUMP_BASE;
                pnlBUMPDetail.Visible = false;

                ckbKpcs.Checked = false;
            }
            else
            {
                BaseFormType = eBaseFormType.WIP_BASE;
                pnlWIPDetail.Visible = false;
            }

            SortInit();
            GridColumnInit();
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

            // 진도율 소수점 1째자리 까지 표시
            jindoPer = Math.Round(Convert.ToDecimal(jindo), 1);

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

        }
        #endregion

        private void ckbssss_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void cdvOperGroup_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            if (cdvFactory.Text.Trim() != "HMKB1")
            {
                strQuery += "SELECT DECODE(ROWNUM, 1, A, 2, B, 3, C, 4, D, 5, E, 6, F, 7, G) AS Code, '' AS Data" + "\n";
                strQuery += "  FROM (SELECT 1 FROM DUAL CONNECT BY LEVEL <= 7) " + "\n";
                strQuery += "     , ( " + "\n";
                strQuery += "        SELECT 'STOCK' AS A" + "\n";
                strQuery += "             , 'SAW' AS B, 'DA' AS C " + "\n";
                strQuery += "             , 'WB' AS D, 'MOLD' AS E " + "\n";
                strQuery += "             , 'FINISH' AS F, 'HMK3A' AS G " + "\n";
                strQuery += "           FROM DUAL " + "\n";
                strQuery += "       ) " + "\n";
            }
            else
            {
                strQuery += "SELECT DECODE(ROWNUM, 1, A, 2, B, 3, C, 4, D, 5, E, 6, F, 7, G, 8, H, 9, I, 10, J, 11, K) AS Code, '' AS Data" + "\n";
                strQuery += "  FROM (SELECT 1 FROM DUAL CONNECT BY LEVEL <= 11) " + "\n";
                strQuery += "     , ( " + "\n";
                strQuery += "        SELECT 'STOCK' AS A" + "\n";
                strQuery += "             , 'RCF' AS B, 'RDL1' AS C " + "\n";
                strQuery += "             , 'PSV1' AS D, 'RDL2' AS E " + "\n";
                strQuery += "             , 'PSV2' AS F, 'RDL3' AS G " + "\n";
                strQuery += "             , 'PSV3' AS H, 'BUMP' AS I " + "\n";
                strQuery += "             , 'AVI' AS J, 'HMK3B' AS K " + "\n";
                strQuery += "           FROM DUAL " + "\n";
                strQuery += "       ) " + "\n";
            }

            cdvOperGroup.sDynamicQuery = strQuery;
        }
    }
}
