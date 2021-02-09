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
 * comment : EIS - 월별 매출현황
 *
 * created by : bee-jae jung (2016-06-15-수요일)
 *
 * modified by : bee-jae jung (2016-06-15-수요일)
 ****************************************************/

namespace Hana.TRN
{
    public partial class TRN090106 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        #region " TRN090106 : Program Initial "

        public TRN090106()
        {
            InitializeComponent();
			fnSSInitial(SS01);
			fnSSSortInit();

            // 2016-06-15-정비재 : Factory의 초기값을 설정한다.
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
			// 2016-06-15-정비재 : 초기값은 K 단위로 사용한다.
            chkKpcs.Checked = true;
			cdvFromToDate.DaySelector.Text = "월";
			// 2016-06-15-정비재 : 월별의 검색기간의 초기값을 설정한다.
            cdvFromToDate.FromYearMonth.Value = DateTime.Now;
        }

        #endregion
        

        #region " Common Function "

		private void fnSSInitial(Miracom.SmartWeb.UI.Controls.udcFarPoint SS)
		{
            /****************************************************
             * Comment : Spread Sheet의 Header를 설정한다.
             * 
             * Created By : bee-jae jung(2016-06-15-수요일)
             * 
             * Modified By : bee-jae jung(2016-06-15-수요일)
             ****************************************************/
            int iIdx = 0;
			try
			{
				Cursor.Current = Cursors.WaitCursor;

                SS.RPT_ColumnInit();
				SS.RPT_AddBasicColumn("WORK_MONTH", 0, iIdx + 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100); 
				SS.RPT_AddBasicColumn("CUSTOMER", 0, iIdx + 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
				SS.RPT_AddBasicColumn("PACKAGE", 0, iIdx + 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
				SS.RPT_AddBasicColumn("RETURN_QTY_1", 0, iIdx + 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
				SS.RPT_AddBasicColumn("RETURN_PRICE_1", 0, iIdx + 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
				SS.RPT_AddBasicColumn("RECEIVE_QTY_1", 0, iIdx + 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
				SS.RPT_AddBasicColumn("RECEIVE_PRICE_1", 0, iIdx + 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
				SS.RPT_AddBasicColumn("ISSUE_QTY_1", 0, iIdx + 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
				SS.RPT_AddBasicColumn("ISSUE_PRICE_1", 0, iIdx + 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
				SS.RPT_AddBasicColumn("WIP_QTY_1", 0, iIdx + 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
				SS.RPT_AddBasicColumn("WIP_PRICE_1", 0, iIdx + 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
				SS.RPT_AddBasicColumn("SHIP_QTY_1", 0, iIdx + 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
				SS.RPT_AddBasicColumn("SHIP_PRICE_1", 0, iIdx + 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
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
			 * Created By : bee-jae jung(2016-06-15-수요일)
			 * 
			 * Modified By : bee-jae jung(2016-06-15-수요일)
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
			 * Comment : SS의 데이터를 Chart에 표시한다.
			 * 
			 * Created By : bee-jae jung(2016-06-15-수요일)
			 * 
			 * Modified By : bee-jae jung(2016-06-15-수요일)
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
			 * Created By : bee-jae jung(2016-06-15-수요일)
			 * 
			 * Modified By : bee-jae jung(2016-06-16-목요일)
			 ****************************************************/
			String sPackage = "", sParameter = "";
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                LoadingPopUp.LoadIngPopUpShow(this);

                if (fnBusinessRule() == false)
                {
                    return false;
                }

				// 2016-06-16-정비재 : Sheet / Listview를 초기화 한다.
				CmnInitFunction.ClearList(SS01, true);
				
				// 2016-06-16-정비재 : oracle package(procedure)를 이용하여 데이터를 조회한다.
				sPackage = "PACKAGE_TRN090106.PROC_TRN090106";
				sParameter = "P_WORK_MONTH:" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM")
						+ "│ P_CUSTOMER:" + (cdvCustomer.Text == "ALL" ? "%" : cdvCustomer.Text) + "%"
						+ "│ P_PACKAGE:" + (cdvPackage.Text == "ALL" ? "%" : cdvPackage.Text) + "%";

				DataTable dt = CmnFunction.oComm.fnExecutePackage("DYNAMIC", sPackage, sParameter);

				if (dt.Rows.Count <= 0)
				{
					dt.Dispose();
					LoadingPopUp.LoadingPopUpHidden();
					CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
					return false;
				}

                SS01.DataSource = dt;
                SS01.RPT_AutoFit(false);
                
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

        private void fnMakeChart(Miracom.SmartWeb.UI.Controls.udcFarPoint SS, Int32 iCellRowCount, String sCustomer)
		{
			/****************************************************
			 * Comment : SS의 데이터를 Chart에 표시한다.
			 * 
			 * Created By : bee-jae jung(2016-06-15-수요일)
			 * 
			 * Modified By : bee-jae jung(2016-06-15-수요일)
			 ****************************************************/
			Int32 iRow = 0, iSelectRowCount = 0;
			Int32[] iSelectRow_MM, iSelectCol_MM;
			String[] sSelectRow_MM;
			Double dChartMax = 0;
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				// 2016-06-20-정비재 : SS에 데이터가 없으면 종료한다.
                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

				// 2016-06-20-정비재 : 선택한 cell을 chart에 표시하기 위하여 row count를 계산한다.
				for (iRow = 0; iRow < iCellRowCount; iRow++)
				{
					if (SS.Sheets[0].Cells[iRow, 1].Text == sCustomer)
					{
						iSelectRowCount++;
					}
				}

				// 2016-06-20-정비재 : 선택한 고객사의 package수 만큼 데이터 배열을 생성한다.
				iSelectRow_MM = new Int32[iSelectRowCount];
				sSelectRow_MM = new String[iSelectRowCount];
				
				// 2016-06-20-정비재 : 선택한 고객사의 package이름을 배열에 입력한다.
				iSelectRowCount = 0;
				for (iRow = 0; iRow < iCellRowCount; iRow++)
				{
					if (SS.Sheets[0].Cells[iRow, 1].Text == sCustomer)
					{
						iSelectRow_MM[iSelectRowCount] = iRow;
						sSelectRow_MM[iSelectRowCount] = SS.Sheets[0].Cells[iRow, 2].Text;
						iSelectRowCount++;
					}
				}

				// 2016-06-20-정비재 : 선택한 고객사의 package에 대한 수량, 금액정보를 배열에 저장한다.
				iSelectCol_MM = new Int32[4];
				if (rbS01.Checked == true)
				{
					iSelectCol_MM = new Int32[] { 3, 5, 7, 9, 11 };
				}
				else
				{
					iSelectCol_MM = new Int32[] { 4, 6, 8, 10, 12 };
				}
				
				// 2016-06-20-정비재 : 변수에 저장된 데이터를 이용하여 chart에 표시한다.
				udcMSChart1.RPT_1_ChartInit();
				udcMSChart1.RPT_2_ClearData();
				udcMSChart1.ChartAreas[0].AxisX.Title = sCustomer;
				udcMSChart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Tahoma", 10.25F);
				udcMSChart1.ChartAreas[0].AxisY.Title = "매출현황";
				udcMSChart1.RPT_3_OpenData(iSelectRow_MM.Length, iSelectCol_MM.Length);
				dChartMax = udcMSChart1.RPT_4_AddData(SS, iSelectRow_MM, iSelectCol_MM, SeriseType.Rows);
				udcMSChart1.RPT_5_CloseData();
				udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 0, 1, "", AsixType.Y, DataTypes.Initeger, dChartMax * 1.05);
				udcMSChart1.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, 2);
				udcMSChart1.RPT_8_SetSeriseLegend(sSelectRow_MM, System.Windows.Forms.DataVisualization.Charting.Docking.Top);
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

        private void btnView_Click(object sender, EventArgs e)
        {
			/****************************************************
			 * comment : View Button을 클릭하면
			 * 
			 * created by : bee-jae jung(2016-06-15-수요일)
			 * 
			 * modified by : bee-jae jung(2016-06-15-수요일)
			 ****************************************************/
			try
            {
				Cursor.Current = Cursors.WaitCursor;

                udcMSChart1.RPT_1_ChartInit();
                udcMSChart1.RPT_2_ClearData();

				fnSSInitial(SS01);                  // 2016-06-15-정비재 : Sheet Control을 초기화 한다.
				fnDataFind();                       // 2016-06-15-정비재 : 검색조건에 일치하는 데이터를 검색한다.
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

		private void RadioButton_CheckedChanged(object sender, EventArgs e)
		{
			/****************************************************
			 * comment : Radio Button을 클릭하면
			 * 
			 * created by : bee-jae jung(2016-06-20-월요일)
			 * 
			 * modified by : bee-jae jung(2016-06-20-월요일)
			 ****************************************************/
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				// 2016-06-20-정비재 : chart를 초기화 한다.
				udcMSChart1.RPT_1_ChartInit();
				udcMSChart1.RPT_2_ClearData();

				// 2016-06-20-정비재 : 데이터를 조회한다.
				fnDataFind();
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

		private void btnExcelExport_Click(object sender, EventArgs e)
		{
			/****************************************************
			 * Comment : Excel Export Button을 클릭하면
			 * 
			 * Created By : bee-jae jung(2016-06-15-수요일)
			 * 
			 * Modified By : bee-jae jung(2016-06-15-수요일)
			 ****************************************************/
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				SS01.ExportExcel();
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

		private void SS01_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
		{
			/****************************************************
			 * Comment : Spread Sheet를 클릭하면
			 * 
			 * Created By : bee-jae jung(2016-06-20-월요일)
			 * 
			 * Modified By : bee-jae jung(2016-06-20-월요일)
			 ****************************************************/
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				// 2016-06-20-정비재 : Sheet에서 선택한 항목의 정보를 chart를 표시한다.
				fnMakeChart(SS01, SS01.ActiveSheet.RowCount, SS01.ActiveSheet.Cells[e.Row, 1].Value.ToString());
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
