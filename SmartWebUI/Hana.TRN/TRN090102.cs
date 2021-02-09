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
 * comment : EIS - 재공이동현황
 *
 * created by : bee-jae jung (2010-08-30-월요일)
 *
 * modified by : bee-jae jung (2010-11-29-월요일)
 ****************************************************/

namespace Hana.TRN
{
    public partial class TRN090102 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        #region " TRN090102 : Program Initial "

        public TRN090102()
        {
            InitializeComponent();
			fnSSInitial(SS01);
			fnSSSortInit();

            // 2010-11-30-정비재 : Factory의 초기값을 설정한다.
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            // 2010-11-30-정비재 : 초기값은 K 단위로 사용한다.
            chkKpcs.Checked = true;
            // 2010-11-30-정비재 : 일자별의 검색기간의 초기값을 설정한다.
            cdvFromToDate.FromDate.Value = DateTime.Now.AddMonths(-1);  
            cdvFromToDate.ToDate.Value = DateTime.Now;
            // 2010-11-30-정비재 : 월별의 검색기간의 초기값을 설정한다.
            cdvFromToDate.FromYearMonth.Value = DateTime.Now.AddMonths(-12);
            cdvFromToDate.ToYearMonth.Value = DateTime.Now;
        }

        #endregion
        

        #region " Common Function "

		private void fnSSInitial(Miracom.SmartWeb.UI.Controls.udcFarPoint SS)
		{
            /****************************************************
             * Comment : SS의 Header를 설정한다.
             * 
             * Created By : bee-jae jung(2010-08-30-월요일)
             * 
             * Modified By : bee-jae jung(2010-11-29-월요일)
             ****************************************************/
            int iIdx = 0;
			try
			{
                Cursor.Current = Cursors.WaitCursor;

                SS.RPT_ColumnInit();

                if (rbSearch01.Checked == true || rbSearch02.Checked == true)
                {
                    SS.RPT_AddBasicColumn("Customer", 0, iIdx + 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                    SS.RPT_AddBasicColumn("PACKAGE", 0, iIdx + 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                    if (rbSearch01.Checked == true)
                    {
                        // 2010-11-29-정비재 : 일자별
                        SS.RPT_AddBasicColumn("WORK_DATE", 0, iIdx + 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                    }
                    else if(rbSearch02.Checked == true)
                    {
                        // 2010-11-29-정비재 : 월별
                        SS.RPT_AddBasicColumn("WORK_MONTH", 0, iIdx + 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                    }
                    SS.RPT_AddBasicColumn("RETURN_LOT", 0, iIdx + 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    SS.RPT_AddBasicColumn("RETURN_QTY", 0, iIdx + 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    SS.RPT_AddBasicColumn("RECEIVE_LOT", 0, iIdx + 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    SS.RPT_AddBasicColumn("RECEIVE_QTY", 0, iIdx + 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    SS.RPT_AddBasicColumn("ISSUE_LOT", 0, iIdx + 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    SS.RPT_AddBasicColumn("ISSUE_QTY", 0, iIdx + 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    SS.RPT_AddBasicColumn("WIP_LOT", 0, iIdx + 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    SS.RPT_AddBasicColumn("WIP_QTY", 0, iIdx + 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    SS.RPT_AddBasicColumn("SHIP_LOT", 0, iIdx + 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    SS.RPT_AddBasicColumn("SHIP_QTY", 0, iIdx + 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                }
                else if (rbSearch03.Checked == true || rbSearch04.Checked == true)
                {
                    if (rbSearch03.Checked == true)
                    {
                        // 2010-11-29-정비재 : 일자별
                        SS.RPT_AddBasicColumn("WORK_DATE", 0, iIdx + 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                    }
                    else if (rbSearch04.Checked == true)
                    {
                        // 2010-11-29-정비재 : 월별
                        SS.RPT_AddBasicColumn("WORK_MONTH", 0, iIdx + 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                    }
                    SS.RPT_AddBasicColumn("RETURN_LOT", 0, iIdx + 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    SS.RPT_AddBasicColumn("RETURN_QTY", 0, iIdx + 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    SS.RPT_AddBasicColumn("RECEIVE_LOT", 0, iIdx + 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    SS.RPT_AddBasicColumn("RECEIVE_QTY", 0, iIdx + 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    SS.RPT_AddBasicColumn("ISSUE_LOT", 0, iIdx + 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    SS.RPT_AddBasicColumn("ISSUE_QTY", 0, iIdx + 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    SS.RPT_AddBasicColumn("WIP_LOT", 0, iIdx + 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    SS.RPT_AddBasicColumn("WIP_QTY", 0, iIdx + 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    SS.RPT_AddBasicColumn("SHIP_LOT", 0, iIdx + 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    SS.RPT_AddBasicColumn("SHIP_QTY", 0, iIdx + 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                }
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
             * Created By : bee-jae jung(2010-08-30-월요일)
             * 
             * Modified By : bee-jae jung(2010-11-29-월요일)
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
             * Created By : bee-jae jung(2010-08-30-월요일)
             * 
             * Modified By : bee-jae jung(2010-11-29-월요일)
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
             * Created By : bee-jae jung(2010-08-30-월요일)
             * 
             * Modified By : bee-jae jung(2010-11-29-월요일)
             ****************************************************/
            String Qry = "";
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                LoadingPopUp.LoadIngPopUpShow(this);

                if (fnBusinessRule() == false)
                {
                    return false;
                }
                
                if (rbSearch01.Checked == true || rbSearch02.Checked == true)
                {
                    if (rbSearch01.Checked == true)
                    {
                        // 2010-11-29-정비재 : 일자별 데이터를 검색한다.
                        Qry = "SELECT CUSTOMER AS CUSTOMER"
                            + "     , PACKAGE AS PACKAGE"
                            + "     , TO_CHAR(TO_DATE(WORK_DATE, 'YYYYMMDDHH24'), 'YYYY-MM-DD') AS WORK_DATE";
                    }
                    else if (rbSearch02.Checked == true)
                    {
                        // 2010-11-29-정비재 : 월별 데이터를 검색한다.
                        Qry = "SELECT CUSTOMER AS CUSTOMER"
                            + "     , PACKAGE AS PACKAGE"
                            + "     , TO_CHAR(TO_DATE(WORK_MONTH, 'YYYYMM'), 'YYYY-MM') AS WORK_MONTH";
                    }

                    if (chkKpcs.Checked == true)
                    {
                        Qry += "     , SUM(RETURN_LOT) AS RETURN_LOT"
                            + "     , SUM(RETURN_QTY_1) / 1000 AS RETURN_QTY"
                            + "     , SUM(RECEIVE_LOT) AS RECEIVE_LOT"
                            + "     , SUM(RECEIVE_QTY_1) / 1000 AS RECEIVE_QTY"
                            + "     , SUM(ISSUE_LOT) AS ISSUE_LOT"
                            + "     , SUM(ISSUE_QTY_1) / 1000 AS ISSUE_QTY"
                            + "     , SUM(WIP_LOT) AS WIP_LOT"
                            + "     , SUM(WIP_QTY_1) / 1000 AS WIP_QTY"
                            + "     , SUM(SHIP_LOT) AS SHIP_LOT"
                            + "     , SUM(SHIP_QTY_1) / 1000 AS SHIP_QTY"
                            + "  FROM REISWIPMOV"
                            + " WHERE FACTORY = '" + cdvFactory.Text + "'"
                            + "   AND CUSTOMER LIKE '" + cdvCustomer.Text + "%'"
                            + "   AND PACKAGE LIKE '" + cdvPackage.Text + "%'";
                    }
                    else
                    {
                        Qry += "     , SUM(RETURN_LOT) AS RETURN_LOT"
                            + "     , SUM(RETURN_QTY_1) AS RETURN_QTY"
                            + "     , SUM(RECEIVE_LOT) AS RECEIVE_LOT"
                            + "     , SUM(RECEIVE_QTY_1) AS RECEIVE_QTY"
                            + "     , SUM(ISSUE_LOT) AS ISSUE_LOT"
                            + "     , SUM(ISSUE_QTY_1) AS ISSUE_QTY"
                            + "     , SUM(WIP_LOT) AS WIP_LOT"
                            + "     , SUM(WIP_QTY_1) AS WIP_QTY"
                            + "     , SUM(SHIP_LOT) AS SHIP_LOT"
                            + "     , SUM(SHIP_QTY_1) AS SHIP_QTY"
                            + "  FROM REISWIPMOV"
                            + " WHERE FACTORY = '" + cdvFactory.Text + "'"
                            + "   AND CUSTOMER LIKE '" + cdvCustomer.Text + "%'"
                            + "   AND PACKAGE LIKE '" + cdvPackage.Text + "%'";
                    }

                    if (rbSearch01.Checked == true)
                    {
                        // 2010-11-29-정비재 : 일자별 데이터를 검색한다.
                        Qry += "   AND WORK_DATE >= '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' || '22'"
                            + "   AND WORK_DATE <= '" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' || '22'"
                            + " GROUP BY CUSTOMER, PACKAGE, WORK_DATE"
                            + " ORDER BY CUSTOMER ASC, PACKAGE ASC, WORK_DATE DESC";
                    }
                    else if (rbSearch02.Checked == true)
                    {
                        // 2010-11-29-정비재 : 월별 데이터를 검색한다.
                        Qry += "   AND WORK_DATE >= '" + cdvFromToDate.FromYearMonth.Text.Replace("-", "") + "01' || '22'"
                            + "   AND WORK_DATE <= '" + cdvFromToDate.ToYearMonth.Text.Replace("-", "") + "31' || '22'"
                            + " GROUP BY CUSTOMER, PACKAGE, WORK_MONTH"
                            + " ORDER BY CUSTOMER ASC, PACKAGE ASC, WORK_MONTH DESC";
                    }
                }
                else if (rbSearch03.Checked == true || rbSearch04.Checked == true)
                {
                    if (rbSearch03.Checked == true)
                    {
                        // 2010-11-29-정비재 : 일자별 데이터를 검색한다.
                        Qry = "SELECT TO_CHAR(TO_DATE(WORK_DATE, 'YYYYMMDDHH24'), 'YYYY-MM-DD') AS WORK_DATE";
                    }
                    else if (rbSearch04.Checked == true)
                    {
                        // 2010-11-29-정비재 : 월별 데이터를 검색한다.
                        Qry = "SELECT TO_CHAR(TO_DATE(WORK_MONTH, 'YYYYMM'), 'YYYY-MM') AS WORK_MONTH";
                    }

                    if (chkKpcs.Checked == true)
                    {
                        Qry += "     , SUM(RETURN_LOT) AS RETURN_LOT"
                            + "     , SUM(RETURN_QTY_1) / 1000 AS RETURN_QTY"
                            + "     , SUM(RECEIVE_LOT) AS RECEIVE_LOT"
                            + "     , SUM(RECEIVE_QTY_1) / 1000 AS RECEIVE_QTY"
                            + "     , SUM(ISSUE_LOT) AS ISSUE_LOT"
                            + "     , SUM(ISSUE_QTY_1) / 1000 AS ISSUE_QTY"
                            + "     , SUM(WIP_LOT) AS WIP_LOT"
                            + "     , SUM(WIP_QTY_1) / 1000 AS WIP_QTY"
                            + "     , SUM(SHIP_LOT) AS SHIP_LOT"
                            + "     , SUM(SHIP_QTY_1) / 1000 AS SHIP_QTY"
                            + "  FROM REISWIPMOV"
                            + " WHERE FACTORY = '" + cdvFactory.Text + "'"
                            + "   AND CUSTOMER LIKE '" + cdvCustomer.Text + "%'"
                            + "   AND PACKAGE LIKE '" + cdvPackage.Text + "%'";
                    }
                    else
                    {
                        Qry += "     , SUM(RETURN_LOT) AS RETURN_LOT"
                            + "     , SUM(RETURN_QTY_1) AS RETURN_QTY"
                            + "     , SUM(RECEIVE_LOT) AS RECEIVE_LOT"
                            + "     , SUM(RECEIVE_QTY_1) AS RECEIVE_QTY"
                            + "     , SUM(ISSUE_LOT) AS ISSUE_LOT"
                            + "     , SUM(ISSUE_QTY_1) AS ISSUE_QTY"
                            + "     , SUM(WIP_LOT) AS WIP_LOT"
                            + "     , SUM(WIP_QTY_1) AS WIP_QTY"
                            + "     , SUM(SHIP_LOT) AS SHIP_LOT"
                            + "     , SUM(SHIP_QTY_1) AS SHIP_QTY"
                            + "  FROM REISWIPMOV"
                            + " WHERE FACTORY = '" + cdvFactory.Text + "'"
                            + "   AND CUSTOMER LIKE '" + cdvCustomer.Text + "%'"
                            + "   AND PACKAGE LIKE '" + cdvPackage.Text + "%'";
                    }
                    
                    if (rbSearch03.Checked == true)
                    {
                        // 2010-11-29-정비재 : 일자별 데이터를 검색한다.
                        Qry += "   AND WORK_DATE >= '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' || '22'"
                            + "   AND WORK_DATE <= '" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' || '22'"
                            + " GROUP BY WORK_DATE"
                            + " ORDER BY WORK_DATE DESC";
                    }
                    else if (rbSearch04.Checked == true)
                    {
                        // 2010-11-29-정비재 : 월별 데이터를 검색한다.
                        Qry += "   AND WORK_DATE >= '" + cdvFromToDate.FromYearMonth.Text.Replace("-", "") + "01' || '22'"
                            + "   AND WORK_DATE <= '" + cdvFromToDate.ToYearMonth.Text.Replace("-", "") + "31' || '22'"
                            + " GROUP BY WORK_MONTH"
                            + " ORDER BY WORK_MONTH DESC";
                    }
                }

                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", Qry);

                if (dt.Rows.Count <= 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return false;
                }

                SS01.DataSource = dt;
                SS01.RPT_AutoFit(false);

                // 2010-11-29-정비재 : 검색된 내용을 chart에 Display한다.
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
             * Comment : SS의 데이터를 Chart에 표시한다.
             * 
             * Created By : bee-jae jung(2010-08-30-월요일)
             * 
             * Modified By : bee-jae jung(2010-11-29-월요일)
             ****************************************************/
            int iRow = 0;
            double dMaxY = 0, dMaxY_Temp = 0, dMaxY2 = 0, dMaxY2_Temp = 0;
            try
			{
                Cursor.Current = Cursors.WaitCursor;

                // 2010-11-29-정비재 : SS에 데이터가 없으면 종료한다.
                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                // 2010-11-29-정비재 : Chart에서 Ship수량을 빼고 Chart를 그리기 위하여
                int[] iRowDataY1 = new int[SS.Sheets[0].RowCount];
                int[] iRowDataY2 = new int[SS.Sheets[0].RowCount];
                // 2010-09-01-정비재 : Qty 및 Time에 대한 Bar Chart를 Display한다.
                for (iRow = 0; iRow < iRowDataY1.Length; iRow++)
                {
                    iRowDataY1[iRow] = (iRowDataY1.Length - 1) - iRow;
                    iRowDataY2[iRow] = (iRowDataY1.Length - 1) - iRow;
                }

                String[] strSeries = new String[] { "RETURN_LOT", "RETURN_QTY", "RECEIVE_LOT", "RECEIVE_QTY"
                                                  , "ISSUE_LOT", "ISSUE_QTY", "WIP_LOT", "WIP_QTY"
                                                  , "SHIP_LOT", "SHIP_QTY" }; 
                
                // STEP 1 : Chart를 이미지를 초기화 함
                udcMSChart1.RPT_1_ChartInit();
                // STEP 2 : Chart를 Display하는 데이터를 초기화 함.
                udcMSChart1.RPT_2_ClearData();
                // STEP 3 : Chart를 그리기 위하여 Open
                udcMSChart1.RPT_3_OpenData(strSeries.Length, iRowDataY1.Length);

                // 2015-12-02-정비재 : 검색조건에 맞는 chart를 그린다.
                if (rbSearch01.Checked == true || rbSearch02.Checked == true)
                {
                    // STEP 4 : 입력된 데이터로 Chart를 Display한다.
                    // 2010-11-29-정비재 : BarChart 그리기 및 BarChart의 최대값 계산
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 3 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 5 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 7 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 9 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 11 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);

                    // 2010-11-29-정비재 : LineChart 그리기 및 LineChart의 최대값 계산
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 4 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 6 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 8 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 10 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 12 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);

                    // STEP 5 : Chart를 닫음
                    udcMSChart1.RPT_5_CloseData();

                    // STEP 6 : Chart를 Display한다.
                    // 2010-11-29-정비재 : BarChart를 그림
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 0, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 1, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 2, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 3, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 4, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);

                    // 2010-09-01-정비재 : LineChart를 그림
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 5, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 6, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 7, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 8, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 9, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                }
                else if (rbSearch03.Checked == true || rbSearch04.Checked == true)
                {
                    // STEP 4 : 입력된 데이터로 Chart를 Display한다.
                    // 2010-11-30-정비재 : BarChart 그리기 및 BarChart의 최대값 계산
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 1 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 3 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 5 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 7 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);
                    dMaxY_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY1, new int[] { 9 }, SeriseType.Column);
                    dMaxY = (dMaxY_Temp > dMaxY == true ? dMaxY_Temp : dMaxY);

                    // 2010-11-30-정비재 : LineChart 그리기 및 LineChart의 최대값 계산
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 2 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 4 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 6 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 8 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);
                    dMaxY2_Temp = udcMSChart1.RPT_4_AddData(SS, iRowDataY2, new int[] { 10 }, SeriseType.Column);
                    dMaxY2 = (dMaxY2_Temp > dMaxY2 == true ? dMaxY2_Temp : dMaxY2);

                    // STEP 5 : Chart를 닫음
                    udcMSChart1.RPT_5_CloseData();

                    // STEP 6 : Chart를 Display한다.
                    // 2010-11-30-정비재 : BarChart를 그림
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 0, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 1, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 2, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 3, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 4, 1, "LOT 건 수", AsixType.Y, DataTypes.Initeger, dMaxY * 1.1);

                    // 2010-09-01-정비재 : LineChart를 그림
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 5, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 6, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 7, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 8, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                    udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 9, 1, "QTY(수량)", AsixType.Y2, DataTypes.Initeger, dMaxY2 * 1.1);
                }

                // STEP 7 : Chart의 X축의 Title을 설정한다.
                udcMSChart1.RPT_7_SetXAsixTitleUsingSpread(SS, iRowDataY1, strSeries.Length);
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

        private void rbSearch_CheckedChanged(object sender, EventArgs e)
        {
            /****************************************************
             * comment : 일/월을 선택하면 발생되는 이벤트
             * 
             * created by : bee-jae jung(2010-11-29-월요일)
             * 
             * modified by : bee-jae jung(2010-11-30-화요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (rbSearch01.Checked == true || rbSearch03.Checked == true)
                {
                    cdvFromToDate.DaySelector.Text = "일"; 
                }
                else if (rbSearch02.Checked == true || rbSearch04.Checked == true)
                {
                    cdvFromToDate.DaySelector.Text = "월";
                }

                // 2010-11-30-정비재 : Chart를 초기화 한다.
                udcMSChart1.RPT_1_ChartInit();
                udcMSChart1.RPT_2_ClearData();
                // 2010-08-30-정비재 : Sheet Control을 초기화 한다.
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
             * created by : bee-jae jung(2010-08-30-월요일)
             * 
             * modified by : bee-jae jung(2010-11-29-월요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                udcMSChart1.RPT_1_ChartInit();
                udcMSChart1.RPT_2_ClearData();

				fnSSInitial(SS01);                  // 2010-08-30-정비재 : Sheet Control을 초기화 한다.
                fnDataFind();                       // 2010-11-29-정비재 : 검색조건에 일치하는 데이터를 검색한다.
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
             * Created By : bee-jae jung(2010-08-30-월요일)
             * 
             * Modified By : bee-jae jung(2010-08-30-월요일)
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

        #endregion        

       
    }
}
