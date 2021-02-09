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

namespace Hana.YLD
{
    public partial class YLD040501 : Miracom.SmartWeb.UI.Controls.udcCUSReport002        
    {
        /// <summary>
        /// 클  래  스: YLD040501<br/>
        /// 클래스요약: 수율 불량코드<br/>
        /// 작  성  자: 미라콤 김민규<br/>
        /// 최초작성일: 2008-10-08<br/>
        /// 상세  설명: 수율 고객사별 불량코드 데이터를 조회한다.<br/>
        /// 변경  내용: <br/>
        /// </summary>                
        public YLD040501()
        {
            InitializeComponent();
            udcDurationDate1.AutoBinding();
            SortInit();       //Sort 리스트 초기화     
            GridColumnInit(); //헤더 한줄짜리                         
            
            menu_select.SelectedIndex = 0; // Viewoption 기본값
            cboPcs.SelectedIndex = 0;      // 단위 기본값
        }

        #region SQL쿼리문 생성       
        /// <summary>
        ///  SQL 쿼리 Build
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            string sFrom = null;
            string sTo = null;
            string bbbb = null;            
            string duration_option = null;

            if(udcDurationDate1.DaySelector.SelectedValue.ToString() == "DAY")
            {                   
                duration_option = "WORK_DAY";   // *****  RWIPLOTLCD 뷰의 WORK_DATE 필드명이 WORK_DAY입니다 ***** //
            }
            else if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "WEEK")
            {                   
                duration_option = "WORK_WEEK";
            }
            else
            {                   
                duration_option = "WORK_MON";
            }
                        
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;




            strSqlString.AppendFormat("     SELECT {0}, LOSS_CODE" + "\n", QueryCond3);

            if (cboPcs.SelectedIndex == 0)   //PPM
            {
                bbbb = udcDurationDate1.getRepeatQuery("          DECODE(SUM(V", "), 0, 0, TRUNC(SUM(V", ")/MAX(C", ")*1000000,3)) VAL");               
            }
            else if(cboPcs.SelectedIndex == 1) //EA
            {
                bbbb = udcDurationDate1.getRepeatQuery("          SUM(V", ")", " VAL");                
            }
            else if(cboPcs.SelectedIndex == 2) //MPPM
            {
                bbbb = udcDurationDate1.getRepeatQuery("          DECODE(SUM(V", "), 0, 0, TRUNC(SUM(V", ")/MAX(C", ")*1000000000,3)) VAL");
            }
            
            strSqlString.AppendFormat(" {0} ", bbbb);
            strSqlString.AppendFormat("       FROM (SELECT {0}, LOSS_CODE, LOSS_QTY " + duration_option + "\n", QueryCond2);                             
            
            bbbb = udcDurationDate1.getDecodeQuery("                   DECODE(" + duration_option, "LOSS_QTY,0)", "V");          
            strSqlString.AppendFormat("{0} ", bbbb);

            bbbb = udcDurationDate1.getDecodeQuery("                   DECODE(" + duration_option, "OLD_QTY,1)", "C");
            strSqlString.AppendFormat("{0} ", bbbb);

            
            strSqlString.Append("                         FROM RWIPLOTLCD  " + "\n");
            strSqlString.Append("                        WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                          AND LOSS_CODE " + cdvLossCode.SelectedValueToQueryString + "\n");
            strSqlString.Append("                          AND OPER BETWEEN '" + cdvOper.FromText +"' AND '" + cdvOper.ToText + "' " + "\n");            

            //기간 선택시 SQL 조건문 생성
            if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "DAY")
            {
                sFrom = udcDurationDate1.FromDate.Text.Replace("-", "");
                sTo = udcDurationDate1.ToDate.Text.Replace("-", "");
                strSqlString.AppendFormat("                 AND WORK_DAY BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);

            }
            else if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                sFrom = udcDurationDate1.FromWeek.SelectedValue.ToString();
                sTo = udcDurationDate1.ToWeek.SelectedValue.ToString();
                strSqlString.AppendFormat("                 AND WORK_WEEK BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
            }
            else
            {
                sFrom = udcDurationDate1.FromYearMonth.Value.ToString("yyyyMM");
                sTo = udcDurationDate1.ToYearMonth.Value.ToString("yyyyMM");
                strSqlString.AppendFormat("                 AND WORK_MON BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
            }

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                 AND MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                 AND MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                 AND MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                 AND MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                 AND MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                 AND MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                 AND MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                 AND MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            
            if (menu_select.SelectedIndex == 1)   // 팀별 VIEWOPTION을 선택 했을경우 OPER_GRP_2이 존재하는 ROW만 선택
            {
                strSqlString.Append("                 AND OPER_GRP_2 " + cdvDepart.SelectedValueToQueryString + "\n");
            }
            strSqlString.AppendFormat("                     GROUP BY {0}, LOSS_CODE, LOSS_QTY, OLD_QTY ," + duration_option, QueryCond2 + "\n");            
            strSqlString.Append("             ) " + "\n");

            strSqlString.AppendFormat("   GROUP BY {0}, LOSS_CODE" + "\n", QueryCond2);
            strSqlString.AppendFormat("   ORDER BY {0}, LOSS_CODE ASC " + "\n", QueryCond2);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();

        }

        #endregion

        #region 기본 Chart 생성
        /// <summary>
        /// 챠트 생성
        /// </summary>
        /// <param name="rowCount"></param>
        private void ShowChart(int rowCount)
        {            
            string unit = null;
            
            // 차트 설정
            udcChartFX1.RPT_2_ClearData();
            udcChartFX1.RPT_3_OpenData(1, udcDurationDate1.SubtractBetweenFromToDate+1);
            int[] wip_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate+1];
            int[] tat_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate+1];
            int[] columnsHeader = new Int32[udcDurationDate1.SubtractBetweenFromToDate+1];
                     
            if (cboPcs.SelectedIndex == 0)
            {
                unit = "[단위 : PPM]";
            }
            else if (cboPcs.SelectedIndex == 1)
            {
                unit = "[단위 : EA]";
            }
            else
            {
                unit = "[단위 : MPPM]";
            }
            
            for (int i = 0;  i < wip_columns.Length ; i++)
            {
                columnsHeader[i] = 11 + i;
                wip_columns[i] = 11 + i;
                tat_columns[i] = 11 + i;              
            }
            
            double max1 = 0;
            double max_temp = 0;
            int max_rownum = 0;
            int[] rows = new Int32[spdData.ActiveSheet.Rows.Count];

            for( int j=0 ; j < spdData.ActiveSheet.Rows.Count ; j++ )
            {

                max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { j }, wip_columns, SeriseType.Rows);
                if( max1 > max_temp)
                {
                    max_temp = max1;
                    max_rownum = j;
                }
                rows[j] = j;
            }

            max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { max_rownum }, wip_columns, SeriseType.Rows);
            
            udcChartFX1.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            //udcChartFX1.RPT_6_SetGallery(ChartType.Line, 0, 1, "[단위 : EA]", AsixType.Y, DataTypes.Initeger, max1 * 1);
            //udcChartFX1.RPT_6_SetGallery(ChartType.Area, 1, 1, "END [단위 : EA]", AsixType.Y2, DataTypes.Initeger, max2 * 1.5);

            //각 Serise별로 동일한 타입을 사용할 경우
            udcChartFX1.RPT_6_SetGallery(ChartType.Line, unit, AsixType.Y, DataTypes.PercentDouble2, max1 * 1.5);

            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            udcChartFX1.RPT_8_SetSeriseLegend(spdData, rows , 10, SoftwareFX.ChartFX.Docked.Right);                        
            udcChartFX1.PointLabels = true;
            udcChartFX1.AxisY.Gridlines = true;
            udcChartFX1.AxisY.AdjustScale();
            udcChartFX1.LegendBox = false;
            udcChartFX1.SerLegBox = false;
            
        }
        #endregion
        
        #region Viewoption중 Depart(팀)를 더블 클릭했을경우 해당 ROW의 Bar형태 그래프 보여줌 
        /// <summary>
        /// Depart 옵션의 Chart
        /// </summary>
        /// <param name="rowCount"></param>
        private void ShowChart_Selected(int rowCount)
        {            
            string unit = null;

            udcChartFX1.RPT_2_ClearData();
            udcChartFX1.RPT_3_OpenData(1, udcDurationDate1.SubtractBetweenFromToDate + 1);
            int[] wip_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];
            int[] tat_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];
            int[] columnsHeader = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];

            if (cboPcs.SelectedIndex == 0)      // 단위 분류
            {
                unit = "[단위 : PPM]";
            }
            else if (cboPcs.SelectedIndex == 1)
            {
                unit = "[단위 : EA]";
            }
            else
            {
                unit = "[단위 : MPPM]";
            }

            for (int i = 0; i < wip_columns.Length; i++)  // Viewoption에 따라 컬럼수 정의
            {
                columnsHeader[i] = 11 + i;
                wip_columns[i] = 11 + i;
                tat_columns[i] = 11 + i;               
            }

            double max1 = 0;
            int[] rows = new Int32[1];
            rows[0] = rowCount;

            max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount }, wip_columns, SeriseType.Rows);

            udcChartFX1.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            //udcChartFX1.RPT_6_SetGallery(ChartType.Line, 0, 1, "[단위 : EA]", AsixType.Y, DataTypes.Initeger, max1 * 1);
            //udcChartFX1.RPT_6_SetGallery(ChartType.Area, 1, 1, "END [단위 : EA]", AsixType.Y2, DataTypes.Initeger, max2 * 1.5);

            //각 Serise별로 동일한 타입을 사용할 경우

            if (menu_select.SelectedIndex == 1)
            {
                udcChartFX1.RPT_6_SetGallery(ChartType.Bar, unit, AsixType.Y, DataTypes.Initeger, max1 * 1.5);
            }
            else
            {
                udcChartFX1.RPT_6_SetGallery(ChartType.Line, unit, AsixType.Y, DataTypes.Initeger, max1 * 1.5);
            }
            

            //udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            udcChartFX1.RPT_8_SetSeriseLegend(spdData, rows, (10), SoftwareFX.ChartFX.Docked.Top);    // 그래프 설명
            //udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "WIP" }, SoftwareFX.ChartFX.Docked.Top);
            udcChartFX1.PointLabels = true;         // 라인 그래프 일경우 정점에 수치 표시
            udcChartFX1.AxisY.Gridlines = true;     // Y축 그리드 표시
            udcChartFX1.LegendBox = false;
            udcChartFX1.SerLegBox = false;
        }
        #endregion

        //Sample02 정의
        //private void Sample02_Load(object sender, EventArgs e)
        //{
            
        //}

        //ListBox 값 체크
        /// <summary>
        /// Check Condition
        /// </summary>
        /// <returns></returns>
        private Boolean Check_select()
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }
            if (cdvOper.FromText.TrimEnd() == "" || cdvOper.ToText.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
                return false;
            }
            
            if(menu_select.SelectedIndex  == -1)
            {                   
                MessageBox.Show("View Option 항목을 선택하십시요.", "알림");
                return false;
            }

            if(cboPcs.SelectedIndex == -1)
            {
                MessageBox.Show("PCS 항목을 선택하십시요.", "알림");
                return false;
            }
            return true;
        }

        #region 바인딩된 Data를 스프레드에 뿌린뒤 Chart함수 호출
        /// <summary>
        /// View 버튼 EVENT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            if (!CheckField()) return;

            udcChartFX1.RPT_2_ClearData();
            int Column_reCacluate = 0;

            if(Check_select() == false)
            {
                return;            
            }
                        
            GridColumnInit();           
            DataTable dt = null;

            try
            {
                spdData_Sheet1.RowCount = 0;  // 스프레드 초기화
                this.Refresh();
                LoadingPopUp.LoadIngPopUpShow(this);
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 9, null, null, btnSort);
                //                                         ( 서브토탈 시작 컬럼 위치, 서브토탈개수, 토탈안낼 컬럼위치(0 부터 x까지)


                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total부분 셀머지
                if (menu_select.SelectedIndex == 1 || menu_select.SelectedIndex == 2)
                {
                    Column_reCacluate = 10;    // Viewoption에 따른 컬럼 카운트값 초기화
                }                
                else
                {
                    Column_reCacluate = 9;
                }
                spdData.RPT_FillDataSelectiveCells("Total", 0, Column_reCacluate, 0, 1, true, Align.Center, VerticalAlign.Center);

                ////4. Column Auto Fit
                spdData.RPT_AutoFit(false);
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
                       
            //Chart 생성
            if (spdData.ActiveSheet.RowCount > 0)
                ShowChart(0);            
        }
        #endregion

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

            if (cdvOper.FromText.Trim().Length == 0 || cdvOper.ToText.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        #endregion

        #region Excel 파일로 Export
        /// <summary>
        /// EXCEL EXPORT
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

        //Factory 변경시 각각의 상속받은 Class의 Factory값들도 함께 변경
        /// <summary>
        /// Factory Change EVENT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            this.cdvOper.sFactory = cdvFactory.txtValue;
            this.cdvLossCode.sFactory = cdvFactory.txtValue;
            this.cdvDepart.sFactory = cdvFactory.txtValue;
        }
        

        #region 컬럼 헤더 생성
        /// <summary>
        /// INIT ColumnHeader
        /// </summary>
        private void GridColumnInit()
        {   
            if (menu_select.SelectedIndex == -1 || menu_select.SelectedIndex == 0)
            {  
                spdData.RPT_ColumnInit();

                spdData.RPT_AddBasicColumn("Depart", 0, 0, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Step", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);

                spdData.RPT_AddBasicColumn("Customer", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Family", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Package", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type2", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Lead Count", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Density", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Generation", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Scrap Code", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddDynamicColumn(udcDurationDate1, 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                spdData.RPT_ColumnConfigFromTable_New(btnSort); //Group항목이 있을경우 반드시 선언해줄것                   
            }
            else if(menu_select.SelectedIndex == 1)
            {
                spdData.RPT_ColumnInit();

                spdData.RPT_AddBasicColumn("Depart", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Step", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);

                spdData.RPT_AddBasicColumn("Customer", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Family", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Package", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type2", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Lead Count", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Density", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Generation", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Scrap Code", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddDynamicColumn(udcDurationDate1, 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                spdData.RPT_ColumnConfigFromTable_New(btnSort); //Group항목이 있을경우 반드시 선언해줄것                   


            }
            else if(menu_select.SelectedIndex == 2)
            {
                spdData.RPT_ColumnInit();

                spdData.RPT_AddBasicColumn("Depart", 0, 0, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Step", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);

                spdData.RPT_AddBasicColumn("Customer", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Family", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Package", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type2", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Lead Count", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Density", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Generation", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Scrap Code", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddDynamicColumn(udcDurationDate1, 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                spdData.RPT_ColumnConfigFromTable_New(btnSort); //Group항목이 있을경우 반드시 선언해줄것     
            }
        }
        #endregion

        #region Sort List 초기화
        /// <summary>
        /// Init Sort List
        /// </summary>
        private void SortInit()        
        {
            if (menu_select.SelectedIndex == -1 || menu_select.SelectedIndex == 0)
            {
                ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Depart", "A.OPER_GRP_2", "OPER_GRP_2", "OPER_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Step", "A.OPER", "OPER", "OPER", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "A.MAT_GRP_1", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "A.MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "A.MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "A.MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "A.MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "A.MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "A.MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "A.MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
            }
            else if(menu_select.SelectedIndex == 1)
            {
                ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Depart", "A.OPER_GRP_2", "OPER_GRP_2", "OPER_GRP_2", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Step", "A.OPER", "OPER", "OPER", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "A.MAT_GRP_1", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "A.MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "A.MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "A.MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "A.MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "A.MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "A.MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "A.MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
            }
            else if(menu_select.SelectedIndex == 2)
            {
                ((udcTableForm)(this.btnSort.BindingForm)).Clear();                
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Depart", "A.OPER_GRP_2", "OPER_GRP_2", "OPER_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Step", "A.OPER", "OPER", "OPER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "A.MAT_GRP_1", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "A.MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "A.MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "A.MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "A.MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "A.MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "A.MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "A.MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
            }             
        }
        #endregion

        #region Cell을 더블클릭 했을 경우의 이벤트 처리
        /// <summary>
        /// Double Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spdData_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;

            int i = 0;
            int j = 0;

            j = e.Column;

            if (j == 0 && menu_select.SelectedIndex == 1)
            {
                this._Form_delegate("YLD040201");   // 새로운 Tablayout 창 추가             
            }
            else
            {
                i = e.Row;
                ShowChart_Selected(i);              // 챠트 그리기
            }
        }
        #endregion
                        
        private void menu_select_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (menu_select.SelectedIndex == 1)
            {
                cdvDepart.Enabled = true;
            }
            else
            {
                cdvDepart.Enabled = false;
            }
            //spdData.RPT_ColumnInit();
          //spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            SortInit();            
            //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.           
        }
                
        #region 마우스 오른쪽 버튼 클릭시 ContextMenu 보이기  ( 사용금지)
        //private void spdData_Cell_RightClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        //{
            
        //    if(e.Button == MouseButtons.Right)
        //    {
        //        if(e.ColumnHeader)
        //        {
        //            return;
        //        }                    
                
        //        int i = 0;
        //        i = e.Column;                
        //        spdData.ActiveSheet.SetActiveCell(e.Row, e.Column);  // 해달 셀을 Active시킴                

        //        string Column_value = e.Column.ToString();

        //        Point p = new Point();
        //        p.X = e.X;
        //        p.Y = e.Y;
                
        //        Show_Contextmenu(Column_value, p);
                
        //    }
        //    else
        //    {

        //    }
        //}
                
        //private void Show_Contextmenu(string column_vlaue, Point p)
        //{            
        //    EventHandler eh = new EventHandler(spd_menuOnClick);
        
        //    MenuItem[] spd = 
        //    { 
        //    new MenuItem("LOT조회",eh),
        //    new MenuItem("History조회",eh),
        //    new MenuItem("아이템1",eh),
        //    new MenuItem("아이템2",eh)    
        //    };
        //    ContextMenu spd_menu = new ContextMenu(spd);            
        //    spd_menu.Show(this.spdData, p);                       
        //}

        //private void spd_menuOnClick(object obj, EventArgs ea1)
        //{
        //    MenuItem mi = (MenuItem)obj;

        //    string menu_name = mi.Text;

        //    switch (menu_name)
        //    {
        //        case "LOT조회":
        //            MessageBox.Show("1번 메뉴 클릭");
        //            break;

        //        case "History조회":
        //            MessageBox.Show("2번 메뉴 클릭");
        //            break;

        //        case "아이템1":
        //            MessageBox.Show("3번 메뉴 클릭");
        //            break;

        //        case "아이템2":
        //            MessageBox.Show("4번 메뉴 클릭");
        //            break;
        //    }                 
        //}       
        #endregion  
    }       
}
    
