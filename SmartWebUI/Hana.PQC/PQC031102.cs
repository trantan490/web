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
    /// 클  래  스: PQC031102<br/>
    /// 클래스요약: 내부심사 & 인증심사 관리<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2010-08-24<br/>
    /// 상세  설명: 내부심사 & 인증심사 데이터를 조회한다.<br/>
    /// 변경  내용: <br/>    
    
    /// </summary>
    public partial class PQC031102 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        #region " PQC031102 : Program Initial "

        public PQC031102()
        {
            InitializeComponent();
            SortInit();

            cdvFromToDate.AutoBinding();
            cdvFromToDate.DaySelector.MaxDropDownItems = 2;
            cdvFromToDate.DaySelector.SelectedValue = "MONTH";
            
            GridColumnInit();
            udcChartFX1.RPT_1_ChartInit();
            udcChartFX2.RPT_1_ChartInit();
            udcChartFX3.RPT_1_ChartInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
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
            int j = 1;

            try
            {
                spdData.RPT_ColumnInit();
                //spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
                string[] strDate = cdvFromToDate.getSelectDate();

                spdData.RPT_AddBasicColumn("department", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 60);
                                
                for (int i = 0; i <= cdvFromToDate.SubtractBetweenFromToDate; i++)
                {
                    spdData.RPT_AddBasicColumn(strDate[i], 0, j, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);                    
                                            
                    j = j + 1;
                }                
                
                // Group항목이 있을경우 반드시 선언해줄것.
                spdData.RPT_ColumnConfigFromTable(btnSort);
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
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Classification", "VENDOR", "VENDOR", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation Name", "MAT_TYPE", "MAT_TYPE", true);
            
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
            string[] strDate = cdvFromToDate.getSelectDate();

            //추가
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;

            // 쿼리
            strSqlString.Append("SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_DEPARTMENT' AND KEY_1 = RESP_DEPARTMENT) AS RESP_DEPARTMENT" + "\n");

            for (int i = 0; i <= cdvFromToDate.SubtractBetweenFromToDate; i++)
            {
                strSqlString.Append("     , SUM(DECODE(CHECK_DAY, '" + strDate[i] + "', TOTAL_QTY, 0)) AS A" + i + "\n");                
            }
                        
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT RESP_DEPARTMENT" + "\n");

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {                
                strSqlString.Append("             , GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'M') AS CHECK_DAY" + "\n");
            }
            else
            {
                strSqlString.Append("             , GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'W') AS CHECK_DAY" + "\n");
            }

            strSqlString.Append("             , COUNT(*) AS TOTAL_QTY     " + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT ROW_NUMBER() OVER(PARTITION BY DOC_NO ORDER BY VERSION DESC) AS NUM" + "\n");
            strSqlString.Append("                     , A.*" + "\n");
            strSqlString.Append("                  FROM CQCMSCRDAT@RPTTOMES A" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");

            if (rbt1.Checked == true)
            {
                strSqlString.Append("                   AND DOC_NO LIKE 'HAI%'" + "\n");
            }
            else
            {
                strSqlString.Append("                   AND DOC_NO LIKE 'HAA%'" + "\n");
            }
            
            strSqlString.Append("                   AND HIST_DELETE_FLAG = ' '" + "\n");

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                strSqlString.Append("                   AND GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'M') BETWEEN '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "' AND '" + cdvFromToDate.ToYearMonth.Value.ToString("yyyyMM") + "'" + "\n");
            }
            else
            {
                strSqlString.Append("                   AND GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'W') BETWEEN '" + cdvFromToDate.HmFromWeek + "' AND '" + cdvFromToDate.HmToWeek + "'" + "\n");
            }
            
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND NUM = 1" + "\n");
            strSqlString.Append("           AND RESP_DEPARTMENT " + cdvDep.SelectedValueToQueryString + "\n");

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                strSqlString.Append("         GROUP BY RESP_DEPARTMENT, GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'M')" + "\n");                
            }
            else
            {
                strSqlString.Append("         GROUP BY RESP_DEPARTMENT, GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'W')" + "\n");                
            }
            
            strSqlString.Append("       ) " + "\n");
            strSqlString.Append(" GROUP BY RESP_DEPARTMENT" + "\n");
            strSqlString.Append(" ORDER BY RESP_DEPARTMENT" + "\n");            

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
            return strSqlString.ToString();
        }


        #endregion

        #region CHART 쿼리문
        private string MakeChartSqlString(int type)
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string[] strDate = cdvFromToDate.getSelectDate();

            //추가
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;

            // 쿼리
            if (cdvFromToDate.DaySelector.SelectedValue.Equals("MONTH"))
            {
                strSqlString.Append("SELECT GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'M') AS CHECK_DAY" + "\n");
            }
            else
            {
                strSqlString.Append("SELECT GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'W') AS CHECK_DAY" + "\n");
            }
            
            if (type == 1) // 미조치 현황
            {
                strSqlString.Append("     , SUM(DECODE(REG_STEP, '품질팀-승인', 1, 0)) AS \"조치\"" + "\n");
                strSqlString.Append("     , SUM(DECODE(REG_STEP, '품질팀-승인', 0, 1)) AS \"미조치\"" + "\n");
            }
            else // 재발생 현황
            {
                strSqlString.Append("     , SUM(DECODE(REG_TYPE, '신규', 1, 0)) AS \"신규\"" + "\n");
                strSqlString.Append("     , SUM(DECODE(REG_TYPE, '신규', 0, 1)) AS \"재발생\"" + "\n");
            }
            
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT ROW_NUMBER() OVER(PARTITION BY DOC_NO ORDER BY VERSION DESC) AS NUM" + "\n");
            strSqlString.Append("             , A.*" + "\n");
            strSqlString.Append("          FROM CQCMSCRDAT@RPTTOMES A" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");

            if (rbt1.Checked == true) // 내부심사
            {
                strSqlString.Append("           AND DOC_NO LIKE 'HAI%'" + "\n");
            }
            else  // 인증심사
            {
                strSqlString.Append("           AND DOC_NO LIKE 'HAA%'" + "\n");
            }

            strSqlString.Append("           AND HIST_DELETE_FLAG = ' ' " + "\n");

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                strSqlString.Append("           AND GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'M') BETWEEN '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "' AND '" + cdvFromToDate.ToYearMonth.Value.ToString("yyyyMM") + "'" + "\n");
            }
            else
            {
                strSqlString.Append("           AND GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'W') BETWEEN '" + cdvFromToDate.HmFromWeek + "' AND '" + cdvFromToDate.HmToWeek + "'" + "\n");
            }
                        
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND NUM = 1" + "\n");
            strSqlString.Append("   AND RESP_DEPARTMENT " + cdvDep.SelectedValueToQueryString + "\n");

            if (cdvFromToDate.DaySelector.SelectedValue.Equals("MONTH"))
            {
                strSqlString.Append(" GROUP BY GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'M')" + "\n");                
            }
            else
            {
                strSqlString.Append(" GROUP BY GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'W')" + "\n");                
            }
                        
            strSqlString.Append(" ORDER BY CHECK_DAY" + "\n");
           
            //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            //{
            //    System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            //}

            return strSqlString.ToString();
        }


        #endregion

        #region " MakeChart : Chart 처리 "

        /// <summary>
        ///  Chart 생성
        /// </summary>
        private void ShowChart1()
        {
            int chcnt = 1;
            int cnt = 0;
            int iColumnsCount = cdvFromToDate.SubtractBetweenFromToDate + 1;

            // 차트 설정
            udcChartFX1.RPT_2_ClearData();
            udcChartFX1.RPT_3_OpenData(1, iColumnsCount);
            int[] w_columns = new Int32[iColumnsCount];
            int[] columnsHeader = new Int32[iColumnsCount];

            for (int i = 0; i < w_columns.Length; i++)
            {
                w_columns[i] = chcnt;
                columnsHeader[i] = chcnt;
                chcnt += 1;           
            }

            double max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { cnt }, w_columns, SeriseType.Rows);

            udcChartFX1.RPT_5_CloseData();

            udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);

            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);

            udcChartFX1.PointLabels = true;
            udcChartFX1.AxisY.Gridlines = false;
            udcChartFX1.AxisY.DataFormat.Decimals = 0;
            udcChartFX1.SerLegBox = true;
            udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;

            if (rbt1.Checked == true)
            {
                udcChartFX1.SerLeg[0] = "내부심사 FINDING ITEM";
            }
            else
            {
                udcChartFX1.SerLeg[0] = "인증심사 FINDING ITEM";
            }

            udcChartFX1.AxisX.Staggered = false;
            
        }

        /// <summary>
        ///  Chart 생성
        /// </summary>
        private void ShowChart2()
        {            
            udcChartFX2.RPT_2_ClearData();

            //udcChartFX2.AxisY.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            //udcChartFX2.AxisY2.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            //udcChartFX2.AxisX.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);

            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeChartSqlString(1));

            if (dt == null || dt.Rows.Count < 1)
                return;

            udcChartFX2.DataSource = dt;
            udcChartFX2.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
            udcChartFX2.Stacked = SoftwareFX.ChartFX.Stacked.Normal;
            udcChartFX2.SerLegBox = true;
            udcChartFX2.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
            udcChartFX2.LegendBox = false;
            udcChartFX2.PointLabels = true;
            udcChartFX2.AxisX.Staggered = false;
            udcChartFX2.AxisY.Gridlines = false;

            udcChartFX2.Series[0].PointLabelColor = Color.Red;
            udcChartFX2.Series[1].PointLabelColor = Color.Blue;
                        
            udcChartFX2.AxisY.Max = udcChartFX2.AxisY.Max * 1.5;

        }

        /// <summary>
        ///  Chart 생성
        /// </summary>
        private void ShowChart3()
        {
            udcChartFX3.RPT_2_ClearData();

            //udcChartFX2.AxisY.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            //udcChartFX2.AxisY2.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            //udcChartFX2.AxisX.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);

            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeChartSqlString(2));

            if (dt == null || dt.Rows.Count < 1)
                return;

            udcChartFX3.DataSource = dt;
            udcChartFX3.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
            udcChartFX3.Stacked = SoftwareFX.ChartFX.Stacked.Normal;
            udcChartFX3.SerLegBox = true;
            udcChartFX3.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
            udcChartFX3.LegendBox = false;
            udcChartFX3.PointLabels = true;
            udcChartFX3.AxisX.Staggered = false;
            udcChartFX3.AxisY.Gridlines = false;

            udcChartFX3.Series[0].PointLabelColor = Color.Red;
            udcChartFX3.Series[1].PointLabelColor = Color.Blue;

            udcChartFX3.AxisY.Max = udcChartFX3.AxisY.Max * 1.5;

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

            GridColumnInit();
            udcChartFX1.RPT_1_ChartInit();
            udcChartFX2.RPT_1_ChartInit();
            udcChartFX3.RPT_1_ChartInit();

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

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 1, null, null, btnSort);
                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("TOTAL", 0, 1, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);

                dt.Dispose();
                                
                // Chart 생성
                if (spdData.ActiveSheet.RowCount > 0)
                {
                    ShowChart1();
                    ShowChart2();
                    ShowChart3();
                }

                  
                
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

        private void cdvDep_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT KEY_1 AS CODE, DATA_1 AS DATA" + "\n";
            strQuery += "  FROM MGCMTBLDAT " + "\n";
            strQuery += " WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n";
            strQuery += "   AND TABLE_NAME = 'H_DEPARTMENT' " + "\n";
            strQuery += " ORDER BY DATA " + "\n";
            
            cdvDep.sDynamicQuery = strQuery;
        }
    }
}
