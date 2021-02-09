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
    public partial class PQC030706 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {        
        private string sBeforeDay = string.Empty;

        #region " PQC030706 : Program Initial "

        /// <summary>
        /// 클  래  스: PQC030706<br/>
        /// 클래스요약: 원자재 재발율 현황<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2010-12-02<br/>
        /// 상세  설명: 원자재 재발율 현황<br/>
        /// 2015-01-21-임종우 : 업체 GCM 정보 H_VENDOR -> VENDOR 정보로 변경
        /// </summary>
        /// 
        public PQC030706()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();            
            SortInit();

            cdvFromToDate.DaySelector.SelectedValue = "MONTH";
            cdvFromToDate.DaySelector.Visible = false;
            cdvFromToDate.AutoBindingUserSetting(DateTime.Now.Year + "-01", DateTime.Now.Year + "-12");

            GridColumnInit();
            udcChartFX1.RPT_1_ChartInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.cdvVendor.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvMatType.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvModel.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvDesc.sFactory = GlobalVariable.gsAssyDefaultFactory;
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
                
                spdData.RPT_AddBasicColumn("account", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Material classification", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 2, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 60);

                // 쿼리에서 HAVING 절 사용하기 위해 컬럼 추가.. 표시는 안됨. 0보다 큰 데이터만 가져오기 위해 사용함.
                spdData.RPT_AddBasicColumn("TOTAL", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
               
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
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("account", "VENDOR", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME='VENDOR' AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND KEY_1 = VENDOR) AS VENDOR", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Material classification", "MAT_TYPE", "MAT_TYPE", true);            
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
            string QueryCond2 = null;
            string strDecode = null;

            sBeforeDay = cboDay.Text;

            //추가
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            // 쿼리

            strDecode += cdvFromToDate.getDecodeQuery("SUM(DECODE(T_DAY", "RE_CNT/TOTAL * 100, 0))", "M").Replace(", SUM", "     , SUM");

            strSqlString.Append("SELECT " + QueryCond2 + " \n");
            strSqlString.Append(strDecode);
            strSqlString.Append("     , SUM(RE_CNT/TOTAL) AS TOT " + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT VENDOR, MAT_TYPE, T_DAY, COUNT(*) AS TOTAL, SUM(DECODE(RE_CNT, 0, 0, 1)) AS RE_CNT" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT DAT.VENDOR, DAT.MAT_ID, DAT.MAT_TYPE, DAT.LOSS_CHAR, DAT.T_DAY" + "\n");

            if (rdb1.Checked == true) // 수입검사
            {
                strSqlString.Append("                     , (" + "\n");
                strSqlString.Append("                        SELECT COUNT(*) " + "\n");
                strSqlString.Append("                          FROM CIQCEDCLSM@RPTTOMES" + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                strSqlString.Append("                           AND TRAN_TIME BETWEEN DAT.S_DAY AND DAT.E_DAY" + "\n");
                strSqlString.Append("                           AND LOSS_CHAR = DAT.LOSS_CHAR" + "\n");
                strSqlString.Append("                           AND VENDOR = DAT.VENDOR" + "\n");
                strSqlString.Append("                           AND MAT_ID = DAT.MAT_ID" + "\n");
                strSqlString.Append("                       ) AS RE_CNT" + "\n");
                strSqlString.Append("                  FROM (" + "\n");
                strSqlString.Append("                        SELECT VENDOR, MAT_ID, MAT_TYPE, LOSS_CHAR, SUBSTR(TRAN_TIME, 1, 6) AS T_DAY" + "\n");
                strSqlString.Append("                             , TO_CHAR(ADD_MONTHS(TO_DATE(SUBSTR(TRAN_TIME, 1, 6), 'YYYYMM'), -" + cboDay.Text + "), 'YYYYMM') || '01000000' AS S_DAY" + "\n");
                strSqlString.Append("                             , TO_CHAR(ADD_MONTHS(TO_DATE(SUBSTR(TRAN_TIME, 1, 6), 'YYYYMM'), -1), 'YYYYMM') || '31235959' AS E_DAY" + "\n");
                strSqlString.Append("                             , COUNT(*)" + "\n");
                strSqlString.Append("                          FROM CIQCEDCLSM@RPTTOMES" + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                strSqlString.Append("                           AND TRAN_TIME BETWEEN '" + cdvFromToDate.HmFromDay + "000000' AND '" + cdvFromToDate.HmToDay + "235959'" + "\n");
                strSqlString.Append("                         GROUP BY VENDOR, MAT_ID, MAT_TYPE, LOSS_CHAR, SUBSTR(TRAN_TIME, 1, 6)" + "\n");
                strSqlString.Append("                       ) DAT" + "\n");
            }
            else // 공정검사
            {
                strSqlString.Append("                     , (" + "\n");
                strSqlString.Append("                        SELECT COUNT(*) " + "\n");
                strSqlString.Append("                          FROM CQCMNCRDAT@RPTTOMES" + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                strSqlString.Append("                           AND TRAN_TIME BETWEEN DAT.S_DAY AND DAT.E_DAY" + "\n");
                strSqlString.Append("                           AND RESV_CMF_2 = DAT.LOSS_CHAR" + "\n");
                strSqlString.Append("                           AND VENDOR = DAT.VENDOR" + "\n");
                strSqlString.Append("                           AND RESV_CMF_3 = DAT.MAT_ID" + "\n");
                strSqlString.Append("                       ) AS RE_CNT" + "\n");
                strSqlString.Append("                  FROM (" + "\n");
                strSqlString.Append("                        SELECT VENDOR, RESV_CMF_3 AS MAT_ID, MAT_TYPE, RESV_CMF_2 AS LOSS_CHAR, SUBSTR(TRAN_TIME, 1, 6) AS T_DAY" + "\n");
                strSqlString.Append("                             , TO_CHAR(ADD_MONTHS(TO_DATE(SUBSTR(TRAN_TIME, 1, 6), 'YYYYMM'), -" + cboDay.Text + "), 'YYYYMM') || '01000000' AS S_DAY" + "\n");
                strSqlString.Append("                             , TO_CHAR(ADD_MONTHS(TO_DATE(SUBSTR(TRAN_TIME, 1, 6), 'YYYYMM'), -1), 'YYYYMM') || '31235959' AS E_DAY" + "\n");
                strSqlString.Append("                             , COUNT(*)" + "\n");
                strSqlString.Append("                          FROM CQCMNCRDAT@RPTTOMES" + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                strSqlString.Append("                           AND TRAN_TIME BETWEEN '" + cdvFromToDate.HmFromDay + "000000' AND '" + cdvFromToDate.HmToDay + "235959'" + "\n");
                strSqlString.Append("                           AND HIST_DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                           AND NCR_TYPE = '공정불량NCR'" + "\n");
                strSqlString.Append("                           AND RESV_CMF_1 <> ' '" + "\n");
                strSqlString.Append("                         GROUP BY VENDOR, RESV_CMF_3, MAT_TYPE, RESV_CMF_2, SUBSTR(TRAN_TIME, 1, 6)" + "\n");
                strSqlString.Append("                       ) DAT" + "\n");
            }

            strSqlString.Append("                 GROUP BY DAT.VENDOR, DAT.MAT_ID, DAT.MAT_TYPE, DAT.LOSS_CHAR, DAT.T_DAY, DAT.S_DAY ,DAT.E_DAY" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY VENDOR, MAT_TYPE, T_DAY" + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");

            // 업체 조회
            if (cdvVendor.Text != "ALL" && cdvVendor.Text != "")
            {
                strSqlString.Append("   AND VENDOR " + cdvVendor.SelectedValueToQueryString + "\n");
            }

            // 제품타입 조회
            if (cdvMatType.Text != "ALL" && cdvMatType.Text != "")
            {
                strSqlString.Append("   AND MAT_TYPE " + cdvMatType.SelectedValueToQueryString + "\n");
            }

            strSqlString.Append(" GROUP BY " + QueryCond1 + "\n");
            strSqlString.Append(" HAVING SUM(RE_CNT/TOTAL) > 0 " + "\n");
            strSqlString.Append(" ORDER BY " + QueryCond1 + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion


        #region "POP용 쿼리"
        private string MakeSqlStringForPopup(string sVendor, string sMatType, string sBeforeDay, string sDay)
        {
            StringBuilder strSqlString = new StringBuilder();

            string sFromday = "20" + sDay.Replace(".", "");

            strSqlString.Append("SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME='VENDOR' AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND KEY_1 = VENDOR) AS VENDOR" + " \n");
            strSqlString.Append("     , MAT_TYPE, MAT_ID, LOSS_CHAR, DECODE(RE_CNT, 0, '', '재발') AS RE_ERROR" + " \n");
            strSqlString.Append("  FROM (" + "\n");            
            strSqlString.Append("        SELECT DAT.VENDOR, DAT.MAT_ID, DAT.MAT_TYPE, DAT.LOSS_CHAR, DAT.T_DAY" + "\n");

            if (rdb1.Checked == true)
            {
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT COUNT(*) " + "\n");
                strSqlString.Append("                  FROM CIQCEDCLSM@RPTTOMES" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND TRAN_TIME BETWEEN DAT.S_DAY AND DAT.E_DAY" + "\n");
                strSqlString.Append("                   AND LOSS_CHAR = DAT.LOSS_CHAR" + "\n");
                strSqlString.Append("                   AND VENDOR = DAT.VENDOR" + "\n");
                strSqlString.Append("                   AND MAT_ID = DAT.MAT_ID" + "\n");
                strSqlString.Append("               ) AS RE_CNT" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT VENDOR, MAT_ID, MAT_TYPE, LOSS_CHAR, SUBSTR(TRAN_TIME, 1, 6) AS T_DAY" + "\n");
                strSqlString.Append("                     , TO_CHAR(ADD_MONTHS(TO_DATE(SUBSTR(TRAN_TIME, 1, 6), 'YYYYMM'), -" + sBeforeDay + "), 'YYYYMM') || '01000000' AS S_DAY" + "\n");
                strSqlString.Append("                     , TO_CHAR(ADD_MONTHS(TO_DATE(SUBSTR(TRAN_TIME, 1, 6), 'YYYYMM'), -1), 'YYYYMM') || '31235959' AS E_DAY" + "\n");
                strSqlString.Append("                     , COUNT(*)" + "\n");
                strSqlString.Append("                  FROM CIQCEDCLSM@RPTTOMES" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND TRAN_TIME LIKE '" + sFromday + "%'" + "\n");
                strSqlString.Append("                   AND VENDOR = (SELECT KEY_1 FROM MGCMTBLDAT WHERE TABLE_NAME='VENDOR' AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND DATA_1 = '" + sVendor + "')" + "\n");
                strSqlString.Append("                   AND MAT_TYPE = '" + sMatType + "'" + "\n");
                strSqlString.Append("                 GROUP BY VENDOR, MAT_ID, MAT_TYPE, LOSS_CHAR, SUBSTR(TRAN_TIME, 1, 6)" + "\n");
                strSqlString.Append("               ) DAT" + "\n");
            }
            else
            {
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT COUNT(*) " + "\n");
                strSqlString.Append("                  FROM CQCMNCRDAT@RPTTOMES" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND TRAN_TIME BETWEEN DAT.S_DAY AND DAT.E_DAY" + "\n");
                strSqlString.Append("                   AND RESV_CMF_2 = DAT.LOSS_CHAR" + "\n");
                strSqlString.Append("                   AND VENDOR = DAT.VENDOR" + "\n");
                strSqlString.Append("                   AND RESV_CMF_3 = DAT.MAT_ID" + "\n");
                strSqlString.Append("               ) AS RE_CNT" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT VENDOR, RESV_CMF_3 AS MAT_ID, MAT_TYPE, RESV_CMF_2 AS LOSS_CHAR, SUBSTR(TRAN_TIME, 1, 6) AS T_DAY" + "\n");
                strSqlString.Append("                     , TO_CHAR(ADD_MONTHS(TO_DATE(SUBSTR(TRAN_TIME, 1, 6), 'YYYYMM'), -" + sBeforeDay + "), 'YYYYMM') || '01000000' AS S_DAY" + "\n");
                strSqlString.Append("                     , TO_CHAR(ADD_MONTHS(TO_DATE(SUBSTR(TRAN_TIME, 1, 6), 'YYYYMM'), -1), 'YYYYMM') || '31235959' AS E_DAY" + "\n");
                strSqlString.Append("                     , COUNT(*)" + "\n");
                strSqlString.Append("                  FROM CQCMNCRDAT@RPTTOMES" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND TRAN_TIME LIKE '" + sFromday + "%'" + "\n");
                strSqlString.Append("                   AND HIST_DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                   AND NCR_TYPE = '공정불량NCR'" + "\n");
                strSqlString.Append("                   AND RESV_CMF_1 <> ' '" + "\n");
                strSqlString.Append("                   AND VENDOR = (SELECT KEY_1 FROM MGCMTBLDAT WHERE TABLE_NAME='VENDOR' AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND DATA_1 = '" + sVendor + "')" + "\n");
                strSqlString.Append("                   AND MAT_TYPE = '" + sMatType + "'" + "\n");
                strSqlString.Append("                 GROUP BY VENDOR, RESV_CMF_3, MAT_TYPE, RESV_CMF_2, SUBSTR(TRAN_TIME, 1, 6)" + "\n");
                strSqlString.Append("               ) DAT" + "\n");
            }

            strSqlString.Append("         GROUP BY DAT.VENDOR, DAT.MAT_ID, DAT.MAT_TYPE, DAT.LOSS_CHAR, DAT.T_DAY, DAT.S_DAY ,DAT.E_DAY" + "\n");
            strSqlString.Append("       )" + "\n");            

            return strSqlString.ToString();
        }
        #endregion

        #region ShowChart

        private void ShowChart(int irow, string sVender)
        {
            int chcnt = 2;            
            int iColumnsCount = cdvFromToDate.SubtractBetweenFromToDate + 1;

            // 차트 설정
            udcChartFX1.RPT_2_ClearData();
            udcChartFX1.RPT_3_OpenData(1, iColumnsCount);
            int[] columns = new Int32[iColumnsCount];
            int[] columnsHeader = new Int32[iColumnsCount];

            for (int i = 0; i < columns.Length; i++)
            {                
                columns[i] = chcnt;
                columnsHeader[i] = chcnt;
                chcnt++;
            }
                        
            double max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { irow }, columns, SeriseType.Rows);

            udcChartFX1.RPT_5_CloseData();

            udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.1);

            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);

            udcChartFX1.PointLabels = true;
            udcChartFX1.AxisY.Gridlines = false;
            udcChartFX1.AxisY.DataFormat.Decimals = 0;

            string[] strDate = cdvFromToDate.getSelectDate();
            // x축 Label 표시 부분
            for (int i = 0; i <= cdvFromToDate.SubtractBetweenFromToDate; i++)
            {
                udcChartFX1.Legend[i] = strDate[i];
            }

            udcChartFX1.SerLegBox = true;
            udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
            udcChartFX1.SerLeg[0] = sVender;



            //udcChartFX1.AxisX.Staggered = false;    
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
                this.Refresh();

                GridColumnInit();
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
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 2, null, null);

                //// Total
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 2, 0, 1, true, Align.Center, VerticalAlign.Center);

                ////4. Column Auto Fit
                spdData.RPT_AutoFit(false);

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
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                spdData.ExportExcel();
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

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {      
            // 챠트
            if (spdData.ActiveSheet.Columns[e.Column].Label == "account" || spdData.ActiveSheet.Columns[e.Column].Label == "Material classification")
            {
                string strVendor = spdData.ActiveSheet.Cells[e.Row, 0].Text;
                                
                ShowChart(e.Row, strVendor);                                
            }
            else // 상세 팝업
            {
                if (spdData.ActiveSheet.Cells[e.Row, e.Column].Text != "")
                {

                    String sVendor = spdData.ActiveSheet.Cells[e.Row, 0].Text;
                    String sMatType = spdData.ActiveSheet.Cells[e.Row, 1].Text;
                    String sDay = spdData.ActiveSheet.Columns[e.Column].Label;

                    DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForPopup(sVendor, sMatType, sBeforeDay, sDay));

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        System.Windows.Forms.Form frm = new PQC030706_P1(dt);
                        frm.ShowDialog();
                    }
                }
            }            
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
            strQuery += "   AND A.CREATE_TIME BETWEEN '" + cdvFromToDate.HmFromDay + "' AND '" + cdvFromToDate.HmToDay + "' " + "\n";
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
