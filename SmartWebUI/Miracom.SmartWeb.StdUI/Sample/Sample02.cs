using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI.Controls;

namespace Miracom.SmartWeb.UI
{
    public partial class Sample02 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public Sample02()
        {
            InitializeComponent();
            udcDurationDate1.AutoBinding();
            udcDurationDate2.AutoBinding();

            SortInit();  
            GridColumnInit(); //해더 한줄짜리 샘플

            udcChartFX1.RPT_1_ChartInit();  //차트 초기화.
            
        }

        private Boolean CheckField()
        {
            //Boolean Check = false;

            //if (cdvFactory.Text.TrimEnd() == "")
            //{
            //    MessageBox.Show(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage), "STD1110");
            //    Check = false;
            //}
            //else { Check = true; }

            return true;
        }

        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            //string QueryCond3 = null;
            string sFrom = null;
            string sTo = null;
            string sGroupBy = null;
            //string sHeader = null;
            string bbbb = null;
            string cccc = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
                        
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
             
            strSqlString.AppendFormat(" SELECT {0}, STEPSEQ, 'X' GUBUN, SUM(TOTAL) TOTAL " + "\n", QueryCond2);

            bbbb = udcDurationDate1.getRepeatQuery("SUM(V", ")", "VAL");            
            strSqlString.AppendFormat(" {0} ", bbbb);
            strSqlString.Append("             , ROUND(AVG(TOTAL),3) AVG, SUM(TOTAL1) TOTAL1 " + "\n");

            cccc = udcDurationDate1.getRepeatQuery("SUM(C", ")", "CNT");
            strSqlString.AppendFormat(" {0} ", cccc);
            strSqlString.Append("             , ROUND(AVG(TOTAL1),3) AVG1" + "\n");

            strSqlString.AppendFormat(" FROM ( SELECT {0} " + "\n", QueryCond2);
            strSqlString.Append("             , STEPSEQ, QTY TOTAL" + "\n");
            
            //기간별 조회 SQL문 생성
            if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "DAY")
            {
                bbbb = udcDurationDate1.getDecodeQuery("DECODE(CUTOFF_DATE", "QTY,0)", "V");
                cccc = udcDurationDate1.getDecodeQuery("DECODE(CUTOFF_DATE", "CNT,0)", "C");
            }
            else if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                bbbb = udcDurationDate1.getDecodeQuery("DECODE(CUTOFF_WEEK", "QTY,0)", "V");
                cccc = udcDurationDate1.getDecodeQuery("DECODE(CUTOFF_WEEK", "CNT,0)", "C");
            }
            else
            {
                bbbb = udcDurationDate1.getDecodeQuery("DECODE(CUTOFF_MON", "QTY,0)", "V");
                cccc = udcDurationDate1.getDecodeQuery("DECODE(CUTOFF_MON", "CNT,0)", "C");
            }

            strSqlString.AppendFormat(" {0} ", bbbb);

            strSqlString.Append("             , CNT TOTAL1" + "\n");
            strSqlString.AppendFormat(" {0} ", cccc);

            strSqlString.AppendFormat(" FROM ( SELECT {0} " + "\n", QueryCond1);

            strSqlString.Append("             , A.STEPSEQ" + "\n");      

            //기간별 조회 SQL문 생성
            if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "DAY")
            {
                strSqlString.Append("             , A.CUTOFF_DATE" + "\n");                  
            }
            else if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                strSqlString.Append("             , A.CUTOFF_WEEK" + "\n");  
            }
            else
            {
                strSqlString.Append("             , A.CUTOFF_MON" + "\n");
            }

            strSqlString.Append("             , ROUND(SUM(A.OUT_QTY)/1000,2) QTY" + "\n");
            strSqlString.Append("             , COUNT(A.OUT_QTY) CNT" + "\n");
            strSqlString.Append("             FROM RPT_DEMO A ");
            strSqlString.AppendFormat(" WHERE A.FACTORY = '{0}' " + "\n", CmnFunction.Trim(cdvFactory.Text));
            strSqlString.AppendFormat("   AND A.STEPSEQ {0} " + "\n", cdvOper.SelectedValueToQueryString);

            //기간별 조회 SQL문 생성
            if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "DAY")
            {
                sFrom = udcDurationDate1.FromDate.Text.Replace("-","");
                sTo = udcDurationDate1.ToDate.Text.Replace("-", "");

                strSqlString.AppendFormat("   AND A.CUTOFF_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom,sTo);
                sGroupBy = "A.CUTOFF_DATE";
            }
            else if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                sFrom = udcDurationDate1.FromWeek.SelectedValue.ToString();
                sTo = udcDurationDate1.ToWeek.SelectedValue.ToString();

                strSqlString.AppendFormat("   AND A.CUTOFF_WEEK BETWEEN '{0}' AND '{1}' " + "\n", sFrom,sTo);
                sGroupBy = "A.CUTOFF_WEEK";
            }
            else
            {
                sFrom = udcDurationDate1.FromYearMonth.Value.ToString("yyyyMM");
                sTo = udcDurationDate1.ToYearMonth.Value.ToString("yyyMM");

                strSqlString.AppendFormat("   AND A.CUTOFF_MON BETWEEN '{0}' AND '{1}' " + "\n", sFrom,sTo);
                sGroupBy = "A.CUTOFF_MON";
            }

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text !="ALL" && udcWIPCondition1.Text != "" )
                strSqlString.AppendFormat("   AND A.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

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

            strSqlString.AppendFormat("    GROUP BY {0}, A.STEPSEQ" + "\n", QueryCond2.ToString());
            strSqlString.AppendFormat("     , {0} ))" + "\n", sGroupBy.ToString());
            strSqlString.AppendFormat("    GROUP BY {0}, STEPSEQ" + "\n", QueryCond2.ToString());
            strSqlString.AppendFormat("    ORDER BY {0}, STEPSEQ" + "\n", QueryCond2.ToString());
            return strSqlString.ToString();
        }

        //private void MakeChart(DataTable DT)
        //{
        //    ultraChart1.Visible = true;

        //    DataTable Table = new DataTable();
        //    Table = RptUChart.GetData(DT, 0, 3, 3);

        //    RptUChart.UChartOption.Legend.ShowLegend(ultraChart1, "Right");
        //    RptUChart.UChartOption.UChartTitle(ultraChart1, "Sample ");
        //    RptUChart.UChartOption.DataPointLabel(ultraChart1, true, "ColumnChart", "<DATA_VALUE:0>");
        //    RptUChart.ShowUChart(ultraChart1, Table, "ColumnChart");
        //    RptUChart.UChartOption.AxisItemFormat(ultraChart1, "", "<DATA_VALUE:0.0>");
        //    ultraChart1.Tooltips.Format = Infragistics.UltraChart.Shared.Styles.TooltipStyle.None;

        //    DT.Dispose();
        //}

        private void ShowChart(int rowCount)
        {
            // 차트 설정
            udcChartFX1.RPT_2_ClearData();
            udcChartFX1.RPT_3_OpenData(2, udcDurationDate1.SubtractBetweenFromToDate + 1);
            int[] wip_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];
            int[] tat_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];
            int[] columnsHeader = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];
             
            for (int i = 0; i < wip_columns.Length; i++)
            {
                columnsHeader[i] = 11 + i;
                wip_columns[i] = 11 + i;
                tat_columns[i] = 11 + i;
            }
            
            //MOVE
            double max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount+0 }, wip_columns, SeriseType.Rows);

            //TAT
            double max2 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount+1 }, wip_columns, SeriseType.Rows);
            
            udcChartFX1.RPT_5_CloseData();
            
            //각 Serise별로 다른 타입을 사용할 경우
            //udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 0, 1, "WIP [단위 : sls]", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            //udcChartFX1.RPT_6_SetGallery(ChartType.Line, 1, 1, "TAT [단위 : day]", AsixType.Y2, DataTypes.Initeger, max2 * 1.2);

            //각 Serise별로 동일한 타입을 사용할 경우
            udcChartFX1.RPT_6_SetGallery(ChartType.Bar,  "[단위 : sls]", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
   
            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "WIP","TAT"}, SoftwareFX.ChartFX.Docked.Top);
        }
        
        private void Sample02_Load(object sender, EventArgs e)
        {
         
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = null;

                //CheckField();

                GridColumnInit();

                spdData_Sheet1.RowCount = 0;
                this.Refresh();

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt == null) return;

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 7, 10, null, null, btnSort);

                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 1, true, Align.Center, VerticalAlign.Center);


                ////4. Column Auto Fit
                //spdData.RPT_AutoFit(false);
                ////--------------------------------------------

                //// 두줄짜리이상 샘플코드(부분합계 제공 안됨-총 합계만 가능.)
                spdData.DataSource = dt;

                ////표구성에따른 항목 Display
                //spdData.RPT_ColumnConfigFromTable(btnSort);

                ////최상단에 합계 표시
                //spdData.RPT_AddTotalRow2( 11);

                ////2행단위로 Sheet 재구성(구분부터 이므로 11개임)
                //spdData.RPT_DivideRows(11, 2, udcDurationDate1.SubtractBetweenFromToDate + 3);

                ////구분항목 값 생성(구분이 들어가는 위치임. 0부터 시작)
                //spdData.RPT_FillColumnData(10, new string[] { "Move", "TAT" });
                //spdData.ActiveSheet.DataAutoSizeColumns = false;

                ////색상 바꾸고(0부터 구분까지 이므로 11개임)
                //spdData.RPT_AddRowsColor(RowColorType.General, 2, 0,  11, true);

                ////최 상위 셀 Merge(0부터 공정까지 셀머지 : 10개임)
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 2, true, Align.Center, VerticalAlign.Center);

                ////2줄단위로 포멧 생성(Total부터 : 위치는 11임)
                //spdData.RPT_RepeatableRowCellTypeChange(11, new Formatter[] { Formatter.Number, Formatter.Number});

                ////Column Auto Fit
                //spdData.RPT_AutoFit(false);
                ////--------------------------------------

                //by John (두줄짜리이상 + 부분합계.)
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 7, 10, 10, 2, udcDurationDate1.SubtractBetweenFromToDate + 3, btnSort);

                //구분항목 값 생성(구분이 들어가는 위치임. 0부터 시작)
                spdData.RPT_FillColumnData(10, new string[] { "Move", "TAT" });

                //2. 칼럼 고정(필요하다면..)
                spdData.Sheets[0].FrozenColumnCount = 9;

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 2, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);
                //--------------------------------------------

                //Chart 생성
                //if (spdData.ActiveSheet.RowCount > 0)
                //ShowChart(0);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1, this.lblTitle.Text, "조회조건 1^조회조건 2", "조회조건 3^조회조건4");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);            
            this.udcCUSFromToCondition1.sFactory = cdvFactory.txtValue;
        }

        //한줄짜리 해더 샘플
        private void GridColumnInit()
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;

            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Customer"  , 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Family"    , 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Package"   , 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type1"     , 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Type2"     , 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("LD Count"  , 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Density"   , 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Product"   , 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Operation", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Classification", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Total", 0, 11, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);            
            spdData.RPT_AddDynamicColumn(udcDurationDate1, 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False,Formatter.Number , 60);
            spdData.RPT_AddBasicColumn("Average", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.Number, 70);

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.

            spdData.ActiveSheet.RowCount = 5;
        }

        //두줄짜리 해더 샘플
        private void GridColumnInit1()
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Product", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("LOT QTY", 1, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("WF QTY", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("S-TAT", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 60);
            spdData.RPT_AddBasicColumn("TIME", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("LMOV", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

            spdData.RPT_AddDynamicColumn(udcDurationDate1, new string[] { "LOT QTY", "WF QTY", "S-TAT", "TIME", "LOT MOV" },
                0, 14, Visibles.True , Frozen.False, Align.Right, Merge.False, new Formatter[] { Formatter.Number, Formatter.Number, Formatter.Double3, Formatter.Number, Formatter.Number }, 60);

            spdData.RPT_MerageHeaderColumnSpan("AVG", 0, 9, 5);
            spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 8, 2);

            spdData.ActiveSheet.RowCount = 5;

        }

        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "A.MAT_GRP_1", "MAT_GRP_1", "MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "A.MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "A.MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "A.MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "A.MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "A.MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "A.MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "A.MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "A.PRODUCT", "PRODUCT", "PRODUCT", true );                      
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GridColumnInit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
         //   GridColumnInit1();
            cdvFactory.AutoBinding();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sTest = null;
            string sTest1 = null;
            string sTest2 = null;
            string sTest3 = null;

            sTest = udcCUSFromToCondition1.getDecodeQuery("DECODE(OPER", "QTY,0)", "V");
            sTest1 = udcCUSFromToCondition1.getDecodeQuery("DECODE(OPER", "kkk,0)", "V");
            sTest2 = udcCUSFromToCondition1.getDecodeQuery("DECODE(OPER", "ddd,0)", "V");
            sTest3 = udcCUSFromToCondition1.getDecodeQuery("DECODE(OPER", "rrr,0)", "V");

            sTest1 = udcCUSFromToCondition1.getMultyRepeatQuery(" DECODE(SUM(IN?), 0, 0, TRUNC(SUM(OUT?)/SUM(IN?)*100,3)) ", "Val");

            sTest2 = udcCUSFromToCondition1.getRepeatQuery("SUM(ASSY_END_QTY", ")", "VAL");
        }

        private void spdData_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;

            int i = 0;

            i = e.Row / 2;

            ShowChart(i*2);
        }

        private void cdvOper_Load(object sender, EventArgs e)
        {

        }

        private void cdvFactory_Load(object sender, EventArgs e)
        {

        }
    }
}

