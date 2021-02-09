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

using System.Windows.Forms.DataVisualization.Charting;

namespace Hana.TRN
{
    public partial class PQC030705 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {


        /// <summary>
        /// 클  래  스: PQC030705<br/>
        /// 클래스요약: 수입검사원 불량 검출 현황 <br/>
        /// 작  성  자: 배수민<br/>
        /// 최초작성일: 2010-12-02<br/>
        /// 상세  설명: SHIP&LOSS TREND<br/>
        /// 변경  내용: <br/> 
        /// 2011-03-21-배수민 : 원자재, 웨이퍼 검사원 불량검출 현황 구분 (QI 이소의S 요청)
        /// 2011-09-15-배수민 : 실제 샘플링하여 검사한 Lot 수량을 조회할 수 있도록 샘플링 LOT column 추가 (QI 송희석S 요청)
        /// 2015-03-03-오득연 : ChartFX -> MS Chart로 변경

        private String[] dayArry = new String[7];

        /// </summary>
        public PQC030705()
        {
            InitializeComponent();
            SortInit();

            GridColumnInit();
            //chart1.Series.Clear();
            //chart2.Series.Clear();
            //InitChart();

            cdvFromToDate.AutoBinding();
            cdvFromToDate.DaySelector.SelectedValue = "DAY";
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
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
            spdData.RPT_ColumnInit();

            LabelTextChange();

            String ss = DateTime.Now.ToString("MM-dd");
            string[] strDate = cdvFromToDate.getSelectDate();

            try
            {
                spdData.RPT_AddBasicColumn("employee number", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 110);
                spdData.RPT_AddBasicColumn("name", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 110);
                spdData.RPT_AddBasicColumn("총 SHIPMENT", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 110);
                spdData.RPT_AddBasicColumn("Defect shipment", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 110);
                spdData.RPT_AddBasicColumn("Total inspection LOT", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 110);
                spdData.RPT_AddBasicColumn("Sampling LOT", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 110);
                spdData.RPT_AddBasicColumn("Total defect LOT", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 110);
                spdData.RPT_AddBasicColumn("Inspection rate", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double2, 110);
                spdData.RPT_AddBasicColumn("Defective rate", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double2, 110);

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
            //사용안함
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

            string FromDate = Convert.ToDateTime(cdvFromToDate.FromDate.Text).AddDays(-1).ToString("yyyyMMdd");

            string[] strDate = cdvFromToDate.getSelectDate();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                GetFirstDay();
                GetLastDay();
            }
           
            strSqlString.Append("  SELECT BAT.UPDATE_USER_ID " + "\n");
            strSqlString.Append("       , (SELECT USER_DESC FROM RWEBUSRDEF WHERE USER_ID = BAT.UPDATE_USER_ID) AS USER_DESC" + "\n");
            strSqlString.Append("       , COUNT(BAT.IQC_NO) AS TOT_SHIPMENT" + "\n");
            strSqlString.Append("       , SUM(DECODE(BAT.FINAL_DECISION, 'N', 1, 0)) AS LOSS_SHIPMENT" + "\n");
            strSqlString.Append("       , SUM(BAT.TOT_QTY_2) AS TOT_LOT_QTY" + "\n");
            strSqlString.Append("       , SUM(SML.SAMPLE_LOT_QTY) AS SAMPLE_LOT_QTY" + "\n");
            strSqlString.Append("       , SUM(HIS.LOSS_LOT_QTY) AS LOSS_LOT_QTY" + "\n");
            strSqlString.Append("       , ROUND(RATIO_TO_REPORT(COUNT(BAT.IQC_NO)) OVER () * 100, 2) AS RATIO" + "\n");
            strSqlString.Append("       , ROUND(SUM(DECODE(BAT.FINAL_DECISION, 'N', 1, 0)) / COUNT(BAT.IQC_NO) * 100, 2) AS LOSS_PER" + "\n");
            strSqlString.Append("    FROM CIQCBATSTS@RPTTOMES BAT" + "\n");
            strSqlString.Append("       , (" + "\n");
            strSqlString.Append("          SELECT IQC_NO, COUNT(*) AS LOSS_LOT_QTY" + "\n");
            strSqlString.Append("            FROM CIQCEDCHIS@RPTTOMES" + "\n");
            strSqlString.Append("           WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("             AND DEFECT_QTY > 0" + "\n");

            // 원자재, 웨이퍼 구분 (이소의S 요청)
            //if (cdvMatType.Text == "원부자재")
            if (cdvMatType.SelectedIndex == 0)
            {
                strSqlString.Append("             AND MAT_TYPE <> 'FG' " + "\n");
            }
            //else if (cdvMatType.Text == "Wafer")
            else if (cdvMatType.SelectedIndex == 1)
            {
                strSqlString.Append("             AND MAT_TYPE = 'FG' " + "\n");
            }

            strSqlString.Append("        GROUP BY IQC_NO" + "\n");
            strSqlString.Append("          ) HIS" + "\n");

            // 2011-09-15-배수민 : 
            strSqlString.Append("       , (" + "\n");
            strSqlString.Append("          SELECT IQC_NO, COUNT(*) AS SAMPLE_LOT_QTY" + "\n");
            strSqlString.Append("            FROM CIQCLOTSTS@RPTTOMES" + "\n");
            strSqlString.Append("           WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");

            // 원자재, 웨이퍼 구분 (이소의S 요청)
            //if (cdvMatType.Text == "원부자재")
            if (cdvMatType.SelectedIndex == 0)
            {
                strSqlString.Append("             AND MAT_TYPE <> 'FG' " + "\n");
            }
            //else if (cdvMatType.Text == "Wafer")
            else if (cdvMatType.SelectedIndex == 1)
            {
                strSqlString.Append("             AND MAT_TYPE = 'FG' " + "\n");
            }

            strSqlString.Append("        GROUP BY IQC_NO" + "\n");
            strSqlString.Append("          ) SML" + "\n");

            strSqlString.Append("      WHERE BAT.IQC_NO = HIS.IQC_NO(+)" + "\n");
            strSqlString.Append("        AND BAT.IQC_NO = SML.IQC_NO(+)" + "\n");

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                strSqlString.Append("        AND BAT.UPDATE_TIME BETWEEN '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "01000000" + "' AND '" + cdvFromToDate.ToYearMonth.Value.ToString("yyyyMM") + "31235959" + "'" + "\n");
            }
            else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                strSqlString.Append("        AND BAT.UPDATE_TIME BETWEEN '" + dayArry[0] + "000000" + "' AND '" + dayArry[1] + "235959" + "'" + "\n");
            }
            else
            {
                strSqlString.Append("        AND BAT.UPDATE_TIME BETWEEN '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "000000" + "' AND '" + cdvFromToDate.ToDate.Text.Replace("-", "") + "235959" + "'" + "\n");
            }

            strSqlString.Append("        AND BAT.FINAL_DECISION <> ' '" + "\n");

            //if (cdvMatType.Text == "원부자재")
            if (cdvMatType.SelectedIndex == 0)
            {
                strSqlString.Append("        AND LENGTH(BAT.VENDOR) <> 2 " + "\n");
            }
            //else if (cdvMatType.Text == "Wafer")
            else if (cdvMatType.SelectedIndex == 1)
            {
                strSqlString.Append("        AND LENGTH(BAT.VENDOR) = 2 " + "\n");
            }

            // 성적서 스킵 검사
            if (rbt2.Checked == true)
            {
                strSqlString.Append("       AND VIS_PASS_FLAG <> ' '" + "\n");
            }

            strSqlString.Append("   GROUP BY BAT.UPDATE_USER_ID" + "\n");
            strSqlString.Append("   ORDER BY COUNT(BAT.IQC_NO) DESC" + "\n");
            

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region 주차로 그 주차의 첫날, 마지막날 가져오기

        // 주차의 첫날
        private void GetFirstDay()
        {
            DataTable dt = null;
            StringBuilder strSqlString = new StringBuilder();

            // 22시 기준이라 금주 첫 시작일에서 -1일한 날의 215959부터가 시작 일
            //strSqlString.Append("SELECT TO_CHAR(TO_DATE(SYS_DATE)-1,'YYYYMMDD'), SYS_YEAR||PLAN_WEEK FROM MWIPCALDEF" + "\n");
            strSqlString.Append("  SELECT SYS_DATE, SYS_YEAR||PLAN_WEEK FROM MWIPCALDEF" + "\n");
            strSqlString.Append("   WHERE CALENDAR_ID = 'HM'" + "\n");
            strSqlString.Append("   AND SYS_YEAR||TRIM(TO_CHAR(PLAN_WEEK,'00')) = " + cdvFromToDate.HmToWeek + "\n");
            strSqlString.Append("ORDER BY SYS_DATE" + "\n");
            
            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
            dayArry[0] = dt.Rows[0][0].ToString(); // 금 주 시작일자
                              
        }

        // 주차의 마지막날
        private void GetLastDay()
        {
            DataTable dtLast = null;
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("  SELECT SYS_DATE, SYS_YEAR||PLAN_WEEK FROM MWIPCALDEF" + "\n");
            strSqlString.Append("   WHERE CALENDAR_ID = 'HM'" + "\n");
            strSqlString.Append("   AND SYS_YEAR||TRIM(TO_CHAR(PLAN_WEEK,'00')) = " + cdvFromToDate.HmToWeek + "\n");
            strSqlString.Append("ORDER BY SYS_DATE" + "\n");

            dtLast = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
            int count = dtLast.Rows.Count;
            dayArry[1] = dtLast.Rows[count - 1][0].ToString();// 금 주 마지막일자

        }


        #endregion


        #region EVENT 처리
        /// <summary>
        /// 6. View 버튼 Action
        /// </summary>        ///         
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            if (CheckField() == true)
            {
                GridColumnInit(); //데이터 부르러
                LabelTextChange();
                spdData_Sheet1.RowCount = 0;
                sheetView1.RowCount = 0;

                spdData.Visible = true;                
            }

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (spdData.Visible == true)
                {
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 1, null, null, btnSort);
                    
                    spdData.RPT_FillDataSelectiveCells("TOTAL", 0, 2, 0, 1, true, Align.Center, VerticalAlign.Center); //Total부분 셀 merge 
                }

                spdData.ActiveSheet.Cells[0, 7].Value = 100;
                spdData.ActiveSheet.Cells[0, 8].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, 3].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 2].Value)) * 100;


                dt.Dispose();

                if (spdData.ActiveSheet.RowCount > 0)

                    fnMakeChart(spdData, spdData.ActiveSheet.RowCount);

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


        private void InitChart()
        {
            Series series1 = null;
            Series series2 = null;
            Legend inspLegend = null;
            Legend failLegend = null;

            chart1.Series.Clear();
            chart2.Series.Clear();

            series1 = new Series();
            series2 = new Series();

            chart1.Series.Add(series1);
            chart2.Series.Add(series2);

            inspLegend = new Legend("Default");
            failLegend = new Legend("Default");

            chart1.Legends.Add(inspLegend);
            chart2.Legends.Add(failLegend);
            
            inspLegend.Docking = Docking.Top;
            inspLegend.Alignment = StringAlignment.Center;
            failLegend.Docking = Docking.Top;
            failLegend.Alignment = StringAlignment.Center;
            
            chart1.Series[0].Legend = "Default";
            chart2.Series[0].Legend = "Default";
            chart1.Series[0].IsValueShownAsLabel = true;
            chart2.Series[0].IsValueShownAsLabel = true;

            chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chart2.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
        }

        //기준날짜 선택 시 검사건, 불량건 수량 차트
        private void fnMakeChart(Miracom.SmartWeb.UI.Controls.udcFarPoint SS, int iselrow)
        {
            // SS의 데이터를 Chart에 표시

            int[] ich_mm = new int[iselrow]; int[] icols_mm = new int[iselrow ]; int[] irows_mm = new int[iselrow]; string[] stitle_mm = new string[1];
            int[] yield_rows = new int[iselrow]; 


            int icol = 0, irow = 0;
            try
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                //cf01.RPT_1_ChartInit();
                //cf02.RPT_1_ChartInit();
                //cf01.RPT_2_ClearData();
                //cf02.RPT_2_ClearData();

                udcMSChart1.RPT_1_ChartInit();
                udcMSChart2.RPT_1_ChartInit();
                
                udcMSChart1.RPT_2_ClearData();
                udcMSChart2.RPT_2_ClearData();
              
                //cf01.AxisX.Staggered = false; //cf01              // ms chart에서 기능 확인 필요
                
                for (icol = 0; icol < ich_mm.Length; icol++)
                {
                    icols_mm[icol] = icol ;                    
                }

                for (irow = 0; irow < irows_mm.Length; irow++)
                {
                    irows_mm[irow] = irow ;
                    yield_rows[irow] = irow ;
                }

                //cf02.AxisX.Staggered = false; //cf02
                for (icol = 0; icol < ich_mm.Length; icol++)
                {
                    icols_mm[icol] = icol ;

                }
                for (irow = 0; irow < irows_mm.Length; irow++)
                {
                    irows_mm[irow] = irow ;
                    yield_rows[irow] = irow ;
                }


                String legendDescShip = "";
                String legendDescLoss = "";

                //cf01.RPT_3_OpenData(1, icols_mm.Length);
                //cf02.RPT_3_OpenData(1, icols_mm.Length);

                //double max1 = cf01.RPT_4_AddData(SS, irows_mm, new int[] { 2 }, SeriseType.Column);
                //double max2 = cf02.RPT_4_AddData(SS, irows_mm, new int[] { 3 }, SeriseType.Column);
                
                //cf01.RPT_5_CloseData();
                //cf02.RPT_5_CloseData();

                //cf01.RPT_6_SetGallery(ChartType.Bar, 0, 1, legendDescShip, AsixType.Y, DataTypes.Initeger, max1 * 1.2); 
                //cf02.RPT_6_SetGallery(ChartType.Bar, 1, 1, legendDescLoss, AsixType.Y, DataTypes.Initeger, max2 * 1.2); //최대값 넉넉히

                //cf01.Series[0].PointLabels = true; 
                //cf02.Series[0].PointLabels = true; //포인트 라벨

                //cf01.RPT_7_SetXAsixTitleUsingSpread(SS, irows_mm, 1);
                //cf02.RPT_7_SetXAsixTitleUsingSpread(SS, irows_mm, 1); //x축 대표라벨

                //cf01.RPT_8_SetSeriseLegend(new string[] { "검사건" }, SoftwareFX.ChartFX.Docked.Top);
                //cf02.RPT_8_SetSeriseLegend(new string[] { "불량건" }, SoftwareFX.ChartFX.Docked.Top);

                //cf01.AxisY.Gridlines = true;
                //cf02.AxisY.Gridlines = true; //눈금라벨

                //cf01.AxisY.DataFormat.Decimals = 0;
                //cf02.AxisY.DataFormat.Decimals = 0;

                //cf01.AxisX.Font = new System.Drawing.Font("Tahoma", 8F);
                //cf02.AxisX.Font = new System.Drawing.Font("Tahoma", 8F); //글씨체,크키 조절
                                
                udcMSChart1.RPT_3_OpenData(1, icols_mm.Length);
                udcMSChart2.RPT_3_OpenData(1, icols_mm.Length);

                double max1 = udcMSChart1.RPT_4_AddData(SS, irows_mm, new int[] { 2 }, SeriseType.Column);
                double max2 = udcMSChart2.RPT_4_AddData(SS, irows_mm, new int[] { 3 }, SeriseType.Column);

                udcMSChart1.RPT_6_SetGallery(SeriesChartType.Column, 0, 1, legendDescShip, AsixType.Y, DataTypes.Initeger, max1 * 1.2);
                udcMSChart2.RPT_6_SetGallery(SeriesChartType.Column, 0, 1, legendDescLoss, AsixType.Y, DataTypes.Initeger, max2 * 1.2); //최대값 넉넉히

                udcMSChart1.Series[0].IsValueShownAsLabel = true;
                udcMSChart2.Series[0].IsValueShownAsLabel = true;   //포인트 라벨
                udcMSChart1.Series[0]["DrawingStyle"] = "Cylinder";
                udcMSChart2.Series[0]["DrawingStyle"] = "Cylinder";

                udcMSChart1.RPT_7_SetXAsixTitleUsingSpread(SS, irows_mm, 1);
                udcMSChart2.RPT_7_SetXAsixTitleUsingSpread(SS, irows_mm, 1); //x축 대표라벨

                udcMSChart1.RPT_8_SetSeriseLegend(new string[] { "검사건" }, System.Windows.Forms.DataVisualization.Charting.Docking.Top);
                udcMSChart2.RPT_8_SetSeriseLegend(new string[] { "불량건" }, System.Windows.Forms.DataVisualization.Charting.Docking.Top);

                udcMSChart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                udcMSChart2.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;

                udcMSChart1.Legends[0].Alignment = StringAlignment.Center;
                udcMSChart2.Legends[0].Alignment = StringAlignment.Center;


                //MS CHART
                //chart1.Series[0].Points.Clear();
                //chart2.Series[0].Points.Clear();
                //chart1.Series[0].LegendText = "검사건";
                //chart2.Series[0].LegendText = "불량건";
                //chart1.Series[0]["DrawingStyle"] = "Cylinder";
                //chart2.Series[0]["DrawingStyle"] = "Cylinder";
                //chart1.Series[0].ChartType = SeriesChartType.Column;
                //chart2.Series[0].ChartType = SeriesChartType.Column;

                //for (int a = 0; a < SS.Sheets[0].RowCount; a++ )
                //{
                //    string userDesc = SS.Sheets[0].Cells[a, 1].Value.ToString();    //"USER_DESC"
                //    int tot = Convert.ToInt32(SS.Sheets[0].Cells[a, 2].Value);      //"TOT_SHIPMENT" 검사건
                //    int loss = Convert.ToInt32(SS.Sheets[0].Cells[a, 3].Value);     //"LOSS_SHIPMENT" 불량건

                //    chart1.Series[0].Points.AddXY(userDesc, tot);
                //    chart2.Series[0].Points.AddXY(userDesc, loss);
                //}
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = Cursors.Default;
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
                //ExcelHelper.Instance.subMakeMsChartExcel(spdData, udcMSChart1, this.lblTitle.Text, null, null);

                ////StringBuilder Condition = new StringBuilder();
                ////ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);
                //ExcelHelper.Instance.subMakeExcel(spdData, cf01, this.lblTitle.Text, null, null);
                ////spdData.ExportExcel();            
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


        }
        #endregion

        //private void chart1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    System.Windows.Forms.DataVisualization.Charting.Chart iChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
        //    iChart = (System.Windows.Forms.DataVisualization.Charting.Chart)sender;

        //    try
        //    {
        //        HitTestResult result = iChart.HitTest(e.X, e.Y);
        //        string SeriesPoint = string.Empty;
        //        int seriesCnt = iChart.Series.Count;

        //        if (result != null)
        //        {
        //            if (seriesCnt > 1)
        //            {
        //                for (int p = 0; p < iChart.Series.Count; p++)
        //                {
        //                    iChart.Series[p].BorderWidth = 4;
        //                    iChart.Series[p].MarkerSize = 10;
        //                    iChart.Series[p].BorderDashStyle = ChartDashStyle.Solid;
        //                    iChart.Series[p].BorderColor = Color.Transparent;
        //                }
        //            }
        //            else
        //            {
        //                for (int p = 0; p < iChart.Series[0].Points.Count; p++)
        //                {
        //                    iChart.Series[0].Points[p].BorderWidth = 1;
        //                    iChart.Series[0].Points[p].MarkerSize = 10;
        //                    iChart.Series[0].Points[p].BorderDashStyle = ChartDashStyle.Solid;
        //                    iChart.Series[0].Points[p].BorderColor = Color.Transparent;
        //                }
        //            }
        //        }

        //        if (result.ChartElementType == ChartElementType.LegendItem)
        //        {
        //            SeriesPoint = result.Series.Name.ToString();
        //            DataPoint point2 = null;

        //            for (int p = 0; p < iChart.Series[0].Points.Count; p++)
        //            {
        //                point2 = iChart.Series[SeriesPoint].Points[p];
        //                point2.BorderWidth = 5;
        //                point2.BorderDashStyle = ChartDashStyle.Dot;
        //                point2.BorderColor = Color.Red;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LoadingPopUp.LoadingPopUpHidden();
        //        CmnFunction.ShowMsgBox(ex.Message);
        //    }
        //}

        //private void chart2_MouseMove(object sender, MouseEventArgs e)
        //{
        //    System.Windows.Forms.DataVisualization.Charting.Chart iChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
        //    iChart = (System.Windows.Forms.DataVisualization.Charting.Chart)sender;

        //    try
        //    {
        //        HitTestResult result = iChart.HitTest(e.X, e.Y);
        //        string SeriesPoint = string.Empty;
        //        int seriesCnt = iChart.Series.Count;

        //        if (result != null)
        //        {
        //            if (seriesCnt > 1)
        //            {
        //                for (int p = 0; p < iChart.Series.Count; p++)
        //                {
        //                    iChart.Series[p].BorderWidth = 4;
        //                    iChart.Series[p].MarkerSize = 10;
        //                    iChart.Series[p].BorderDashStyle = ChartDashStyle.Solid;
        //                    iChart.Series[p].BorderColor = Color.Transparent;
        //                }
        //            }
        //            else
        //            {
        //                for (int p = 0; p < iChart.Series[0].Points.Count; p++)
        //                {
        //                    iChart.Series[0].Points[p].BorderWidth = 1;
        //                    iChart.Series[0].Points[p].MarkerSize = 10;
        //                    iChart.Series[0].Points[p].BorderDashStyle = ChartDashStyle.Solid;
        //                    iChart.Series[0].Points[p].BorderColor = Color.Transparent;
        //                }
        //            }
        //        }                  

        //        if (result.ChartElementType == ChartElementType.LegendItem)
        //        {
        //            SeriesPoint = result.Series.Name.ToString();
        //            DataPoint point2 = null;

        //            for (int p = 0; p < iChart.Series[0].Points.Count; p++)
        //            {
        //                point2 = iChart.Series[SeriesPoint].Points[p];
        //                point2.BorderWidth = 5;
        //                point2.BorderDashStyle = ChartDashStyle.Dot;
        //                point2.BorderColor = Color.Red;
        //            }                   
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LoadingPopUp.LoadingPopUpHidden();
        //        CmnFunction.ShowMsgBox(ex.Message);
        //    }
        //}
        

    }
}
