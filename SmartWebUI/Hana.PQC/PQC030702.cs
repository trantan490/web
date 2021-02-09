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

namespace Hana.PQC
{
    /// <summary>
    /// 클  래  스: PQC030702<br/>
    /// 클래스요약: 수입검사 불량 항목<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2009-12-14<br/>
    /// 상세  설명: 수입검사 불량 항목 상위 5 화면<br/>
    /// 2010-03-09-임종우 : 접수 구분 조회 추가
    /// 2010-04-09-임종우 : 불량 항목 TOP 5 에서 전체 항목 다 보이도록 변경.
    /// 2011-02-24-임종우 : Wafer 수입검사 데이터 표시 요청 (송희석 요청)
    /// 2015-01-21-임종우 : 업체 GCM 정보 H_VENDOR -> VENDOR 정보로 변경
    /// </summary>
    public partial class PQC030702 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        #region " PQC030702 : Program Initial "

        // 전역변수 선언
        string stime = string.Empty;
        string etime = string.Empty;
        string sVendor = string.Empty;
        string sMatType = string.Empty;
        string sModel = string.Empty;
        string sDesc = string.Empty;
        string sQcType = string.Empty;

        public PQC030702()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            SortInit();
            GridColumnInit();
            GridColumnInit2();
            udcChartFX1.RPT_1_ChartInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.cdvVendor.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvMatType.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvModel.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvDesc.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvRcvType.sFactory = GlobalVariable.gsAssyDefaultFactory;
        }

        #endregion


        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            return true;
        }

        #endregion


        #region " GridColumnInit : Sheet Title 설정 "

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            try
            {
                spdData.RPT_ColumnInit();


                spdData.RPT_AddBasicColumn("Defect name", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Defect quantity", 0, 1, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("Share", 0, 2, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Double2, 60);
                //spdData.RPT_AddBasicColumn("Standard", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);

                //spdData.RPT_AddBasicColumn("investigation subject", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                //spdData.RPT_AddBasicColumn("Number", 1, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                //spdData.RPT_AddBasicColumn("LOT", 1, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                //spdData.RPT_AddBasicColumn("UNIT", 1, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                //spdData.RPT_MerageHeaderColumnSpan(0, 4, 3);

                //spdData.RPT_AddBasicColumn("Pass rate (%)", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                //spdData.RPT_AddBasicColumn("LOT", 1, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 50);
                //spdData.RPT_AddBasicColumn("UNIT", 1, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 50);
                //spdData.RPT_MerageHeaderColumnSpan(0, 7, 2);

                //spdData.RPT_AddBasicColumn("Inspection quantity", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                //spdData.RPT_AddBasicColumn("LOT", 1, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                //spdData.RPT_AddBasicColumn("UNIT", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                //spdData.RPT_MerageHeaderColumnSpan(0, 9, 2);

                //spdData.RPT_AddBasicColumn("Defect quantity", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                //spdData.RPT_AddBasicColumn("LOT", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                //spdData.RPT_AddBasicColumn("UNIT", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                //spdData.RPT_MerageHeaderColumnSpan(0, 11, 2);

                //spdData.RPT_AddBasicColumn("Defective rate(%)", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                //spdData.RPT_AddBasicColumn("LOT", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 50);
                //spdData.RPT_AddBasicColumn("UNIT", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 50);
                //spdData.RPT_MerageHeaderColumnSpan(0, 13, 2);

                //spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                
                // Group항목이 있을경우 반드시 선언해줄것.
                //spdData.RPT_ColumnConfigFromTable(btnSort);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        private void GridColumnInit2()
        {
            try
            {
                spdData2.RPT_ColumnInit();

                spdData2.RPT_AddBasicColumn("account", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData2.RPT_AddBasicColumn("IQC NO", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 160);
                spdData2.RPT_AddBasicColumn("Item Code", 0, 2, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                spdData2.RPT_AddBasicColumn("Material classification", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                spdData2.RPT_AddBasicColumn("Model", 0, 4, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
                spdData2.RPT_AddBasicColumn("Standard", 0, 5, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                spdData2.RPT_AddBasicColumn("Defect quantity", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                spdData2.RPT_AddBasicColumn("Inspection date", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        #endregion


        #region " SortInit : Group By 설정 "

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            //((udcTableForm)(this.btnSort.BindingForm)).Clear();
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Vendor description", "VENDOR", "VENDOR", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Material classification", "MAT_TYPE", "MAT_TYPE", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "MODEL", "MODEL", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Standard", "MAT_DESC", "MAT_DESC", false);
        }

        #endregion


        #region " MakeSqlString : Sql Query문 "

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;

            //추가
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;

            stime = cdvFromToDate.Start_Tran_Time;
            etime = cdvFromToDate.End_Tran_Time;
            sVendor = cdvVendor.SelectedValueToQueryString;
            sMatType = cdvMatType.SelectedValueToQueryString;
            sModel = cdvModel.SelectedValueToQueryString;
            sDesc = cdvDesc.SelectedValueToQueryString;
            sQcType = cdvQCType.Text;

            // 쿼리
            strSqlString.Append("SELECT LOSS_CHAR " + "\n");            
            strSqlString.Append("     , LOSS_QTY " + "\n");
            strSqlString.Append("     , ROUND(LOSS_QTY/TOT_QTY*100, 2) AS PER " + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT LSM.LOSS_CHAR " + "\n");
            strSqlString.Append("             , SUM(LSM.LOSS_QTY) AS LOSS_QTY " + "\n");
            strSqlString.Append("             , (SELECT SUM(LOSS_QTY) FROM CIQCEDCLSM@RPTTOMES WHERE TRAN_TIME BETWEEN '" + stime + "' AND '" + etime + "') AS TOT_QTY " + "\n");
            strSqlString.Append("          FROM CIQCEDCLSM@RPTTOMES LSM" + "\n");
            strSqlString.Append("             , MWIPMATDEF MMAT " + "\n");
            strSqlString.Append("             , CWIPMATDEF@RPTTOMES CMAT " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND LSM.FACTORY = MMAT.FACTORY " + "\n");
            strSqlString.Append("           AND LSM.FACTORY = CMAT.FACTORY(+) " + "\n");
            strSqlString.Append("           AND LSM.MAT_ID = MMAT.MAT_ID " + "\n");
            strSqlString.Append("           AND LSM.MAT_ID = CMAT.MAT_ID(+) " + "\n");
            strSqlString.Append("           AND MMAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("           AND CMAT.DELETE_FLAG(+) = ' '" + "\n");
            strSqlString.Append("           AND LSM.TRAN_TIME BETWEEN '" + stime + "' AND '" + etime + "'" + "\n");
            strSqlString.Append("           AND LSM.VENDOR " + sVendor + "\n");
            strSqlString.Append("           AND LSM.MAT_TYPE " + sMatType + "\n");
            strSqlString.Append("           AND CMAT.MODEL(+) " + sModel + "\n");
            strSqlString.Append("           AND MMAT.MAT_DESC " + sDesc + "\n");

            if (sQcType != "ALL")
            {
                strSqlString.Append("           AND LSM.TRAN_CODE = '" + sQcType + "'" + "\n");
            }

            // 2010-03-09-임종우 : 접수 구분 조회 추가
            if (cdvRcvType.Text != "ALL" && cdvRcvType.Text != "")
            {
                strSqlString.Append("           AND LSM.IQC_TYPE " + cdvRcvType.SelectedValueToQueryString + "\n");
            }

            strSqlString.Append("         GROUP BY LSM.LOSS_CHAR " + "\n");
            strSqlString.Append("         ORDER BY LOSS_QTY DESC " + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");            
            //strSqlString.Append("   AND ROWNUM < 6 " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private string MakeSqlString2(string schar_id)
        {
            StringBuilder strSqlString = new StringBuilder();

            // Query문으로 데이터를 검색한다.
            strSqlString.Append("SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME IN ('VENDOR', 'H_CUSTOMER') AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND KEY_1 = LSM.VENDOR) AS VENDOR " + "\n");
            strSqlString.Append("     , LSM.IQC_NO " + "\n");
            strSqlString.Append("     , LSM.MAT_ID " + "\n");
            strSqlString.Append("     , LSM.MAT_TYPE " + "\n");
            strSqlString.Append("     , CMAT.MODEL " + "\n");
            strSqlString.Append("     , MMAT.MAT_DESC " + "\n");
            strSqlString.Append("     , LSM.LOSS_QTY " + "\n");
            strSqlString.Append("     , LSM.TRAN_TIME " + "\n");
            strSqlString.Append("  FROM CIQCEDCLSM@RPTTOMES LSM " + "\n");
            strSqlString.Append("     , MWIPMATDEF MMAT " + "\n");
            strSqlString.Append("     , CWIPMATDEF@RPTTOMES CMAT " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND LSM.FACTORY = MMAT.FACTORY " + "\n");
            strSqlString.Append("   AND LSM.FACTORY = CMAT.FACTORY(+) " + "\n");
            strSqlString.Append("   AND LSM.MAT_ID = MMAT.MAT_ID " + "\n");
            strSqlString.Append("   AND LSM.MAT_ID = CMAT.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND MMAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("   AND CMAT.DELETE_FLAG(+) = ' '" + "\n");
            strSqlString.Append("   AND LSM.LOSS_CHAR = '" + schar_id + "'" + "\n");
            strSqlString.Append("   AND LSM.TRAN_TIME BETWEEN '" + stime + "' AND '" + etime + "'" + "\n");
            strSqlString.Append("   AND LSM.VENDOR " + sVendor + "\n");
            strSqlString.Append("   AND LSM.MAT_TYPE " + sMatType + "\n");
            strSqlString.Append("   AND CMAT.MODEL(+) " + sModel + "\n");
            strSqlString.Append("   AND MMAT.MAT_DESC " + sDesc + "\n");

            if (sQcType != "ALL")
            {
                strSqlString.Append("   AND LSM.TRAN_CODE = '" + sQcType + "'" + "\n");
            }

            // 2010-03-09-임종우 : 접수 구분 조회 추가
            if (cdvRcvType.Text != "ALL" && cdvRcvType.Text != "")
            {
                strSqlString.Append("   AND LSM.IQC_TYPE " + cdvRcvType.SelectedValueToQueryString + "\n");
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion


        #region " MakeChart : Chart 처리 "

        /// <summary>
        ///  Chart 생성
        /// </summary>
        private void ShowChart(DataTable dt)
        {
            // 차트 설정
            udcChartFX1.RPT_1_ChartInit();
            udcChartFX1.RPT_2_ClearData();

            udcChartFX1.AxisY.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisY2.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisX.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);

            //DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

            if (dt == null || dt.Rows.Count < 1)
                return;

            udcChartFX1.DataSource = dt;
            udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;

            udcChartFX1.Series[1].YAxis = SoftwareFX.ChartFX.YAxis.Secondary;
            udcChartFX1.Series[1].Gallery = SoftwareFX.ChartFX.Gallery.Lines;

            udcChartFX1.LegendBox = false;
            udcChartFX1.PointLabels = true;
            udcChartFX1.Chart3D = false;
            udcChartFX1.MultipleColors = false;
            udcChartFX1.AxisX.Title.Text = "Defect graph";
            udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
            
            udcChartFX1.RecalcScale();
            //udcChartFX1.AxisY.DataFormat.Decimals = 0;
            udcChartFX1.AxisY2.DataFormat.Decimals = 2;

            //udcChartFX1.RPT_3_OpenData(1, dtChart.Rows.Count);
            //int[] total_rows = new Int32[dtChart.Rows.Count];
            ////int[] columnsHeader = new Int32[dtChart.Rows.Count + 1];

            //for (int i = 0; i < dtChart.Rows.Count; i++)
            //{
            //    total_rows[i] = 0 + i;

            //}
            //double max1 = udcChartFX1.RPT_4_AddData(dtChart, total_rows, new int[] { columnCount + 1 }, SeriseType.Rows, DataTypes.Initeger);


            //udcChartFX1.RPT_5_CloseData();

            ////각 Serise별로 다른 타입을 사용할 경우
            //udcChartFX1.RPT_6_SetGallery(ChartType.Line, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            //udcChartFX1.Series[0].Color = System.Drawing.Color.Black;

            //udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(dtChart);

            //udcChartFX1.PointLabels = true;
            //udcChartFX1.AxisY.Gridlines = false;
            //udcChartFX1.AxisY.DataFormat.Decimals = 2;
        }

        #endregion


        #region " Button Event "
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private void btnView_Click(object sender, EventArgs e)
        {

            DataTable dt = null;

            if (CheckField() == false) return;

            LoadingPopUp.LoadIngPopUpShow(this);

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                // 검색중 화면 표시
             //   LoadingPopUp.LoadIngPopUpShow(this);
                this.Refresh();

                GridColumnInit();
                GridColumnInit2();
                udcChartFX1.RPT_1_ChartInit();

                // Query문으로 데이터를 검색한다.

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                spdData.DataSource = dt;

                //spdData.isShowZero = true;

                //// Sub Total
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 1, null, null);

                //// Total
                spdData.RPT_FillDataSelectiveCells("Total", 0, 1, 0, 1, true, Align.Center, VerticalAlign.Center);

                ////4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                ShowChart(dt);
                //dt.Dispose();

            }
            catch (Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //Cursor.Current = Cursors.WaitCursor;
            //try
            //{
            //    spdData.ExportExcel();
            //}
            //catch (Exception ex)
            //{
            //    CmnFunction.ShowMsgBox(ex.Message);
            //}
            //finally
            //{
            //    Cursor.Current = Cursors.Default;
            //}

            ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1, this.lblTitle.Text, " ^ ", " ^ ");
        }

        #endregion

        private void cdvDesc_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DISTINCT MAT_DESC Code, '' Data" + "\n";
            strQuery += "  FROM MWIPMATDEF " + "\n";
            strQuery += " WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n";
            strQuery += "   AND MAT_TYPE <> 'FG' " + "\n";
            strQuery += "   AND MAT_TYPE " + cdvMatType.SelectedValueToQueryString + "\n";
            strQuery += "   AND DELETE_FLAG = ' ' " + "\n";
            strQuery += "ORDER BY MAT_DESC " + "\n";

            cdvDesc.sDynamicQuery = strQuery;
        }

        private void cdvModel_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DISTINCT MODEL Code, '' Data" + "\n";
            strQuery += "  FROM ( " + "\n";
            strQuery += "         SELECT CDEF.MODEL " + "\n";            
            strQuery += "           FROM MWIPMATDEF MDEF, CWIPMATDEF@RPTTOMES CDEF" + "\n";
            strQuery += "          WHERE MDEF.FACTORY = CDEF.FACTORY " + "\n";
            strQuery += "            AND MDEF.MAT_ID = CDEF.MAT_ID " + "\n";
            strQuery += "            AND MDEF.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n";            
            strQuery += "            AND MDEF.MAT_TYPE " + cdvMatType.SelectedValueToQueryString + "\n";
            strQuery += "            AND MDEF.DELETE_FLAG = ' ' " + "\n";
            strQuery += "            AND CDEF.DELETE_FLAG = ' ' " + "\n";
            strQuery += "       ) " + "\n";
            strQuery += "ORDER BY MODEL " + "\n";

            cdvModel.sDynamicQuery = strQuery;
        }

        private void PQC030702_Load(object sender, EventArgs e)
        {
            // 테이블레이아웃 챠트부분 셀 병합
            tableLayoutPanel1.SetColumnSpan(udcChartFX1, 2);
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            DataTable dt = null;
            
            try
            {
                GridColumnInit2();
                                
                string schar_id = spdData.ActiveSheet.Cells[e.Row, 0].Value.ToString();

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(schar_id));

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                spdData2.DataSource = dt;

                spdData2.RPT_AutoFit(false);
            }
            catch (Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
                Cursor.Current = Cursors.Default;
            }
        }

        private void cdvRcvType_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DECODE (ROWNUM, 1, A, 2, B, 3, C) AS Code, '' Data" + "\n";
            strQuery += "  FROM ( " + "\n";
            strQuery += "         SELECT 1 " + "\n";
            strQuery += "           FROM DUAL" + "\n";
            strQuery += "        CONNECT BY LEVEL <=3 " + "\n";
            strQuery += "       ) " + "\n";
            strQuery += "     , ( " + "\n";
            strQuery += "         SELECT '양산' AS A " + "\n";
            strQuery += "              , 'QUAL' AS B " + "\n";
            strQuery += "              , 'ER' AS C " + "\n";
            strQuery += "           FROM DUAL" + "\n";
            strQuery += "       ) " + "\n";

            cdvRcvType.sDynamicQuery = strQuery;
        }

        // 2011-09-21-배수민 : VENDOR와 CUSTOMER 함께 보여주기, GCM테이블이 아닌 쿼리 이용 (QI파트 송희석S 요청)
        private void cdvVendor_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DISTINCT VENDOR AS Code " + "\n";
            strQuery += "     , DECODE(ORDER_ID, 'FG', C.DATA_1, B.DATA_1) AS Data " + "\n";
            strQuery += "  FROM CIQCBATSTS A " + "\n";
            strQuery += "     , (SELECT KEY_1, DATA_1 " + "\n";
            strQuery += "          FROM MGCMTBLDAT " + "\n";
            strQuery += "         WHERE TABLE_NAME = 'VENDOR' " + "\n";
            strQuery += "           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' ) B " + "\n";
            strQuery += "     , (SELECT KEY_1, DATA_1 " + "\n";
            strQuery += "          FROM MGCMTBLDAT " + "\n";
            strQuery += "         WHERE TABLE_NAME = 'H_CUSTOMER' " + "\n";
            strQuery += "           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' ) C " + "\n";
            strQuery += " WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n";
            strQuery += "   AND A.VENDOR = B.KEY_1(+) " + "\n";
            strQuery += "   AND A.VENDOR = C.KEY_1(+)  " + "\n";
            strQuery += "   AND A.CREATE_TIME BETWEEN '" + cdvFromToDate.Start_Tran_Time + "' AND '" + cdvFromToDate.End_Tran_Time + "' " + "\n";
            strQuery += "   AND A.ORDER_ID " + cdvMatType.SelectedValueToQueryString + "\n";
            strQuery += "ORDER BY DECODE(LENGTH(VENDOR), '2', 2, 1) ASC " + "\n";

            cdvVendor.sDynamicQuery = strQuery;

            // 2011-09-21-임종우 : 리스트 초기화 후 다시 담기 위해...약간 이상하지만 예상으로 데이터는 해당 이벤트 종료시점에 담기는 거 같음.
            // 전에 리스트가 한개라도 존재하면 리셋, 한개도 존재하지 않으면 "ALL" 처리
            if (cdvVendor.ValueItems.Count > 0)
            {
                cdvVendor.ResetText();
            }
            else
            {
                // 이 부분 처리 안하면 다음번 부터 데이터 안 들어감.
                cdvVendor.Text = "ALL";
            }
        }
    }
}
