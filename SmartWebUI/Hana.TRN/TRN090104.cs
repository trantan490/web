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

/****************************************************
 * comment : EIS - 월/년별 공정별 실적현황
 * 
 * created by : bee-jae jung (2011-03-28-월요일)
 *
 * modified by : bee-jae jung (2011-03-28-월요일)
 ****************************************************/

namespace Hana.TRN
{
    public partial class TRN090104 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        #region " TRN090104 : Program Initial "

        public TRN090104()
        {
            InitializeComponent();
            fnSSInitial(SS01);

            // 2010-11-30-정비재 : Factory의 초기값을 설정한다.
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            // 2010-11-30-정비재 : 초기값은 K 단위로 사용한다.
            chkKpcs.Checked = true;
            // 2010-11-30-정비재 : 일자별의 검색기간의 초기값을 설정한다.
            cdvDate.Text = DateTime.Now.ToString("yyyy-MM");
        }

        #endregion
        

        #region " Common Function "

		private void fnSSInitial(Miracom.SmartWeb.UI.Controls.udcFarPoint SS)
		{
            /****************************************************
             * Comment : SS의 Header를 설정한다.
             * 
             * Created By : bee-jae jung(2011-03-28-월요일)
             * 
             * Modified By : bee-jae jung(2011-03-28-월요일)
             ****************************************************/
            int iIdx = 0;
			try
			{
                Cursor.Current = Cursors.WaitCursor;
                
                SS.RPT_ColumnInit();

                SS.RPT_AddBasicColumn("classification", 0, iIdx + 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                if (rbS01.Checked == true)
                {
                    SS.RPT_AddBasicColumn("Last month (M-1)", 0, iIdx + 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("Current month (M)", 0, iIdx + 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 100);
                }
                else if (rbS02.Checked == true)
                {
                    SS.RPT_AddBasicColumn("Last month (M-2)", 0, iIdx + 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("Last month (M-1)", 0, iIdx + 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("Current month (M)", 0, iIdx + 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 100);
                }
                else if (rbS03.Checked == true)
                {
                    SS.RPT_AddBasicColumn("Last month (M-3)", 0, iIdx + 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("Last month (M-2)", 0, iIdx + 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("Last month (M-1)", 0, iIdx + 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("Current month (M)", 0, iIdx + 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 100);
                }
                else if (rbS04.Checked == true)
                {
                    SS.RPT_AddBasicColumn("Last month (M-4)", 0, iIdx + 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("Last month (M-3)", 0, iIdx + 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("Last month (M-2)", 0, iIdx + 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("Last month (M-1)", 0, iIdx + 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("Current month (M)", 0, iIdx + 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 100);
                }
                else if (rbS05.Checked == true)
                {
                    SS.RPT_AddBasicColumn("Last month (M-5)", 0, iIdx + 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("Last month (M-4)", 0, iIdx + 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("Last month (M-3)", 0, iIdx + 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("Last month (M-2)", 0, iIdx + 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("Last month (M-1)", 0, iIdx + 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 100);
                    SS.RPT_AddBasicColumn("Current month (M)", 0, iIdx + 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 100);
                }

                fnSSSortInit();
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox(ex.Message);
				return;
			}
			finally
			{
                Cursor.Current = Cursors.Default;
			}
		}

		private void fnSSSortInit()
		{
            /****************************************************
             * Comment : SS의 데이터의 정렬규칙을 설정하다.
             * 
             * Created By : bee-jae jung(2011-03-28-월요일)
             * 
             * Modified By : bee-jae jung(2011-03-28-월요일)
             ****************************************************/
            try
			{
                Cursor.Current = Cursors.WaitCursor;
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox(ex.Message);
				return;
			}
			finally
			{
                Cursor.Current = Cursors.Default;
			}
		}

        private Boolean fnBusinessRule()
        {
            /****************************************************
             * Comment : 데이터 처리를 위한 데이터 검증을 한다.
             * 
             * Created By : bee-jae jung(2011-03-28-월요일)
             * 
             * Modified By : bee-jae jung(2011-03-28-월요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private Boolean fnDataFind()
        {
            /****************************************************
             * Comment : DataBase에 저장된 데이터를 조회한다.
             * 
             * Created By : bee-jae jung(2011-03-28-월요일)
             * 
             * Modified By : bee-jae jung(2011-03-28-월요일)
             ****************************************************/
            String Qry = "";
            Int32 iMonth = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                LoadingPopUp.LoadIngPopUpShow(this);

                // 2011-03-29-정비재 : Spread를 초기화 한다.
                fnSSInitial(SS01);

                if (fnBusinessRule() == false)
                {
                    return false;
                }

                // 2011-03-29-정비재 : 기준일자 기준으로 몇개월 전 데이터까지 볼것인지 결정한다.
                if (rbS01.Checked == true)
                {
                    iMonth = -1;
                }
                else if (rbS02.Checked == true)
                {
                    iMonth = -2;
                }
                else if (rbS03.Checked == true)
                {
                    iMonth = -3;
                }
                else if (rbS04.Checked == true)
                {
                    iMonth = -4;
                }
                else if (rbS05.Checked == true)
                {
                    iMonth = -5;
                }
                
                Qry = "SELECT TO_CHAR(TO_DATE(WORK_MONTH, 'YYYYMM'), 'YYYY-MM') AS WORK_MONTH \n"
                    + "     , SUM(FGS_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS FGS_QTY \n"
                    + "     , SUM(PACKING_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS PACKING_QTY \n"
                    + "     , SUM(FINALTEST_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS FINALTEST_QTY \n"
                    + "     , SUM(TEST_ISSUE_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS TEST_ISSUE_QTY \n"
                    + "     , SUM(PVI_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS PVI_QTY \n"
                    + "     , SUM(MOLDING_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS MOLDING_QTY \n"
                    + "     , SUM(WIREBONDING_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS WIREBONDING_QTY \n"
                    + "     , SUM(DIEATTACH_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS DIEATTACH_QTY \n"
                    + "     , SUM(SAWING_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS SAWING_QTY \n"
                    + "     , SUM(BACKGRIND_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS BACKGRIND_QTY \n"
                    + "     , SUM(ASSY_ISSUE_QTY) " + (chkKpcs.Checked == true ? "/ 1000" : "") + " AS ASSY_ISSUE_QTY \n"
                    + "  FROM REISWIPEND \n"
                    + " WHERE CUSTOMER LIKE '" + cdvCustomer.Text + "%' \n"
                    + "   AND PACKAGE LIKE '" + cdvPackage.Text + "%' \n"
                    + "   AND WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), " + iMonth.ToString() + "), 'YYYYMM') \n"
                    + "   AND WORK_MONTH <= '" + cdvDate.Value.ToString("yyyyMM") + "' \n"
                    + " GROUP BY WORK_MONTH \n"
                    + " ORDER BY WORK_MONTH ASC";

                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", Qry);

                if (dt.Rows.Count <= 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return false;
                }

                // 2011-03-29-정비재 : DataTable의 내용을 SS에 표시한다.
                SS01.Sheets[0].RowCount = dt.Columns.Count - 1;
                // 2011-03-29-정비재 : 각 항목의 Title을 설정한다.
                SS01.Sheets[0].Cells[0, 0].Value = "FGS_QTY";
                SS01.Sheets[0].Cells[1, 0].Value = "PACKING_QTY";
                SS01.Sheets[0].Cells[2, 0].Value = "FINALTEST_QTY";
                SS01.Sheets[0].Cells[3, 0].Value = "TEST_ISSUE_QTY";
                SS01.Sheets[0].Cells[4, 0].Value = "PVI_QTY";
                SS01.Sheets[0].Cells[5, 0].Value = "MOLDING_QTY";
                SS01.Sheets[0].Cells[6, 0].Value = "WIREBONDING_QTY";
                SS01.Sheets[0].Cells[7, 0].Value = "DIEATTACH_QTY";
                SS01.Sheets[0].Cells[8, 0].Value = "SAWING_QTY";
                SS01.Sheets[0].Cells[9, 0].Value = "BACKGRIND_QTY";
                SS01.Sheets[0].Cells[10, 0].Value = "ASSY_ISSUE_QTY";
                for (int iRow = 0; iRow < dt.Rows.Count; iRow++)
                {
                    for (int iCol = 1; iCol < dt.Columns.Count; iCol++)
                    {
                        SS01.Sheets[0].Cells[iCol - 1, iRow + 1].Value = dt.Rows[iRow][iCol].ToString();
                    }
                }
                
                fnMakeChart(SS01);
                                                
                return true;
            }
            catch (Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
                Cursor.Current = Cursors.Default;
            }
        }

        private void fnMakeChart(Miracom.SmartWeb.UI.Controls.udcFarPoint SS)
		{
            /****************************************************
             * Comment : DataTable의 데이터를 Chart에 표시한다.
             * 
             * Created By : bee-jae jung(2011-03-28-월요일)
             * 
             * Modified By : bee-jae jung(2011-03-28-월요일)
             ****************************************************/
            int iRow = 0;
            int[] iRowDataY1;
            double dMaxY = 0, dMaxY_Temp = 0;
            String[] strSeries = new String[] { "당월(M)" };
			try
			{
                Cursor.Current = Cursors.WaitCursor;

                // 2011-03-28-정비재 : DT에 데이터가 없으면 종료한다.
                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                // 2011-03-28-정비재 : Chart에서 사용할 Data Row의 배열을 선언한다.
                iRowDataY1 = new int[SS.Sheets[0].RowCount];
                // STEP 3 : Bar Chart에 대한 데이터를 배열에 입력한다.
                for (iRow = 0; iRow < iRowDataY1.Length; iRow++)
                {
                    iRowDataY1[iRow] = (iRowDataY1.Length - 1) - iRow;
                }
                
                // 2015-12-02-정비재 : chart에 사용할 series title을 설정한다.
                if (rbS01.Checked == true)
                {
                    strSeries = new String[] { "전월(M-1)", "당월(M)" };
                }
                else if (rbS02.Checked == true)
                {
                    strSeries = new String[] { "전월(M-2)", "전월(M-1)", "당월(M)" };
                }
                else if (rbS03.Checked == true)
                {
                    strSeries = new String[] { "전월(M-3)", "전월(M-2)", "전월(M-1)", "당월(M)" };
                }
                else if (rbS04.Checked == true)
                {
                    strSeries = new String[] { "전월(M-4)", "전월(M-3)", "전월(M-2)", "전월(M-1)", "당월(M)" };
                }
                else if (rbS05.Checked == true)
                {
                    strSeries = new String[] { "전월(M-5)", "전월(M-4)", "전월(M-3)", "전월(M-2)", "전월(M-1)", "당월(M)" };
                }

                // STEP 1 : Chart를 이미지를 초기화 함
                udcMSChart1.RPT_1_ChartInit();
                // STEP 2 : Chart를 Display하는 데이터를 초기화 함.
                udcMSChart1.RPT_2_ClearData();
                // STEP 3 : Chart를 그리기 위하여 Open
                udcMSChart1.RPT_3_OpenData(strSeries.Length, iRowDataY1.Length);
                // STEP 4 : BarChart 그리기 및 BarChart의 최대값 계산
                if (rbS01.Checked == true)
                {
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 1 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 2 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                }
                else if (rbS02.Checked == true)
                {
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 1 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 2 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 3 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                }
                else if (rbS03.Checked == true)
                {
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 1 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 2 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 3 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 4 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                }
                else if (rbS04.Checked == true)
                {
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 1 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 2 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 3 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 4 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 5 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                }
                else if (rbS05.Checked == true)
                {
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 1 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 2 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 3 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 4 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 5 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 6 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                }
                // STEP 5 : Chart를 닫음
                udcMSChart1.RPT_5_CloseData();
                // STEP 6 : Chart를 Display한다.
                udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 0, 1, "", AsixType.Y, DataTypes.Initeger, dMaxY * 1.05);
                // STEP 7 : Chart의 X축의 Title을 설정한다.
                udcMSChart1.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, strSeries.Length);
                // STEP 8 : Chart의 series값으로 표시한다.
                udcMSChart1.RPT_8_SetSeriseLegend(strSeries, System.Windows.Forms.DataVisualization.Charting.Docking.Top);
			}
			catch (Exception ex)
			{
                LoadingPopUp.LoadingPopUpHidden();
				CmnFunction.ShowMsgBox(ex.Message);
				return;
			}
			finally
			{
                Cursor.Current = Cursors.Default;
			}
		}

        #endregion


        #region " Form Event "

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            /****************************************************
             * comment : Radio Button을 클릭하면
             * 
             * created by : bee-jae jung(2011-03-29-화요일)
             * 
             * modified by : bee-jae jung(2011-03-29-화요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                udcMSChart1.RPT_1_ChartInit();
                udcMSChart1.RPT_2_ClearData();
                
                fnSSInitial(SS01);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            /****************************************************
             * comment : View Button을 클릭하면
             * 
             * created by : bee-jae jung(2011-03-28-월요일)
             * 
             * modified by : bee-jae jung(2011-03-28-월요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                udcMSChart1.RPT_1_ChartInit();
                udcMSChart1.RPT_2_ClearData();

                fnDataFind();               // 2010-11-29-정비재 : 검색조건에 일치하는 데이터를 검색한다.
            }
            catch (Exception ex)
            {
				CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion        

       
    }
}
