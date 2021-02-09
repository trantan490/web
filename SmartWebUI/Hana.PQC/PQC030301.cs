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
    public partial class PQC030301 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {


        /// <summary>
        /// 클  래  스: PQC030301<br/>
        /// 클래스요약: DP공정 불량 현황 <br/>
        /// 작  성  자: 김민우<br/>
        /// 최초작성일: 2012-05-02<br/>
        /// 상세  설명: DP공정 불량 현황<br/>
        /// 변경  내용: <br/> 
        /// 2011-05-16-김민우 : 
        
        private String[] dayArry = new String[7];
        private DataTable dtLoss = null;

        /// </summary>
        public PQC030301()
        {
            InitializeComponent();
            //SortInit();

            dtLoss = new DataTable(); // Loss Code Table 
            GetLossDT(); // Loss Table 가져오기

            GridColumnInit();
            cdvFromToDate.AutoBinding();
            cdvFromToDate.DaySelector.SelectedValue = "DAY";
            cdvFromToDate.DaySelector.Visible = false;
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

            //String ss = DateTime.Now.ToString("MM-dd");
            //string[] strDate = cdvFromToDate.getSelectDate();


            try
            {
                // 전체 공정
                spdData.RPT_AddBasicColumn("DATE", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("IN_QTY", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("OUT_QTY", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("YIELD", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.Double3, 50);
                spdData.RPT_AddBasicColumn("LOSS", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Defective rate", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.Double3, 50);
                spdData.RPT_AddBasicColumn("LOT COUNT", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("LOSS LOT COUNT", 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("LRR", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.Double2, 50);

                for (int i = 0; i < dtLoss.Rows.Count; i++)
                {
                    spdData.RPT_AddBasicColumn(dtLoss.Rows[i]["KEY_1"].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                }
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

            GetLossDT(); // Loss Table 가져오기

            // Decode 반복문 셋팅
            string strDecode = string.Empty;
            string strDecodeTitle = string.Empty;

            for (int i = 0; i < dtLoss.Rows.Count; i++)
            {
                strDecode += "             , SUM(DECODE(LOSS_CODE,'" + dtLoss.Rows[i]["KEY_1"].ToString() + "',QTY,0))  LOSS" + dtLoss.Rows[i]["KEY_1"].ToString() + "\n";
                strDecodeTitle += "     , LOSS" + dtLoss.Rows[i]["KEY_1"].ToString() + "\n";
            }
            string FromDate = Convert.ToDateTime(cdvFromToDate.FromDate.Text).AddDays(-1).ToString("yyyyMMdd");
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            
            strSqlString.Append("SELECT A.WORK_DATE, A.IN_QTY, (A.IN_QTY - NVL(TOTAL_LOSS,0)) AS OUT_QTY" + "\n");
            strSqlString.Append("     , ROUND(((A.IN_QTY - NVL(TOTAL_LOSS,0)) / A.IN_QTY) * 100,3) AS YIELD" + "\n");
            strSqlString.Append("     , NVL(TOTAL_LOSS,0)" + "\n");
            strSqlString.Append("     , 100 - ROUND(((A.IN_QTY - NVL(TOTAL_LOSS,0)) / A.IN_QTY) * 100,3) AS LOSS_RATE" + "\n");
            strSqlString.Append("     , LOT_COUNT " + "\n");
            strSqlString.Append("     , LOSS_LOT_COUNT " + "\n");
            strSqlString.Append("     , ROUND((LOSS_LOT_COUNT/LOT_COUNT) * 100,2) AS LRR " + "\n");
            strSqlString.Append(strDecodeTitle + "\n");
            strSqlString.Append("  FROM (SELECT TO_CHAR(TO_DATE(TRAN_TIME, 'YYYYMMDDHH24MISS') + 120/1440, 'YYYYMMDD') AS WORK_DATE" + "\n");
            strSqlString.Append("             , SUM(CREATE_QTY_1) AS IN_QTY" + "\n");
            strSqlString.Append("             , COUNT(LOT_ID) AS LOT_COUNT" + "\n");
            strSqlString.Append("          FROM RWIPLOTHIS HIS, MWIPMATDEF MAT" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND HIS.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND HIS.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("           AND HIS.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("           AND TRAN_TIME BETWEEN '" + FromDate + "220000" + "' AND '" + cdvFromToDate.ToDate.Text.Replace("-", "") + "215959" + "'" + "\n");
            strSqlString.Append("           AND OPER = 'A0400'" + "\n");
            strSqlString.Append("           AND TRAN_CODE = 'END'" + "\n");
            strSqlString.Append("           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("           AND MAT.MAT_VER = 1" + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

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
            #endregion



            strSqlString.Append("         GROUP BY TO_CHAR(TO_DATE(TRAN_TIME, 'YYYYMMDDHH24MISS') + 120/1440,'YYYYMMDD') ) A," + "\n");
            strSqlString.Append("       (SELECT TO_CHAR(TO_DATE(WORK_DATE, 'YYYYMMDDHH24MISS') + 120/1440, 'YYYYMMDD') AS WORK_DATE" + "\n");
            strSqlString.Append(strDecode + "\n");
            strSqlString.Append("             ,SUM(QTY) AS TOTAL_LOSS" + "\n");
            strSqlString.Append("             , SUM(LOSS_LOT_COUNT) AS LOSS_LOT_COUNT" + "\n");
            strSqlString.Append("          FROM (SELECT A.*" + "\n");
            strSqlString.Append("                  FROM (SELECT B.TRAN_TIME AS WORK_DATE, LOSS_CODE, SUM(NVL(LOSS_QTY, 0)) AS QTY" + "\n");
            strSqlString.Append("                             , COUNT(A.LOT_ID) AS LOSS_LOT_COUNT" + "\n");
            strSqlString.Append("                          FROM RWIPLOTLSM A ," + "\n");
            strSqlString.Append("                              (SELECT TRAN_TIME, LOT_ID, CREATE_QTY_1 AS IN_QTY" + "\n");
            strSqlString.Append("                                 FROM RWIPLOTHIS HIS, MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                                WHERE 1=1" + "\n");
            strSqlString.Append("                                  AND HIS.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                  AND HIS.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("                                  AND HIS.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                                  AND TRAN_TIME BETWEEN '" + FromDate + "220000" + "' AND '" + cdvFromToDate.ToDate.Text.Replace("-", "") + "215959" + "'" + "\n");
            strSqlString.Append("                                  AND OPER = 'A0400'" + "\n");
            strSqlString.Append("                                  AND TRAN_CODE = 'END'" + "\n");
            strSqlString.Append("                                  AND MAT.MAT_VER = 1" + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                                  AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                                  AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                                  AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                                  AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                                  AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                                  AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                                  AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                                  AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            #endregion



            strSqlString.Append("                                  AND LOT_TYPE = 'W' ) B" + "\n");
            strSqlString.Append("                         WHERE A.LOT_ID(+) = B.LOT_ID" + "\n");
            strSqlString.Append("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                        GROUP BY B.TRAN_TIME, LOSS_CODE" + "\n");
            strSqlString.Append("                       ) A," + "\n");
            strSqlString.Append("                       (SELECT KEY_1 AS LOSS_CODE" + "\n");
            strSqlString.Append("                          FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND TABLE_NAME='LOSS_CODE'" + "\n");
            strSqlString.Append("                           AND DATA_3='Y'" + "\n");
            strSqlString.Append("                           AND DATA_5 = 'SAW' ) B" + "\n");

            strSqlString.Append("                 WHERE A.LOSS_CODE = B.LOSS_CODE )" + "\n");
            strSqlString.Append("         GROUP BY TO_CHAR(TO_DATE(WORK_DATE, 'YYYYMMDDHH24MISS') + 120/1440,'YYYYMMDD') ) B" + "\n");
            strSqlString.Append(" WHERE A.WORK_DATE = B.WORK_DATE(+)" + "\n");
            strSqlString.Append("ORDER BY A.WORK_DATE" + "\n");
          
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region Loss Code 가져오기

        /// <summary>
        /// Loss Code 가져오기
        /// </summary>
        private void GetLossDT()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT  KEY_1,DATA_3,DATA_4  " + "\n");
            strSqlString.Append("FROM    MGCMTBLDAT  " + "\n");
            strSqlString.Append("WHERE   1=1 " + "\n");
            //strSqlString.Append("        AND FACTORY = '" + cdvFactory.txtValue + "' " + "\n");
            strSqlString.Append("        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("        AND TABLE_NAME='LOSS_CODE' " + "\n");
            strSqlString.Append("        AND DATA_3='Y'  " + "\n");
            strSqlString.Append("        AND DATA_5 = 'SAW'  " + "\n");
            strSqlString.Append("ORDER BY TO_NUMBER(DATA_4) " + "\n");
            

            dtLoss = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
        }

        #endregion


        #region EVENT 처리
        /// <summary>
        /// 6. View 버튼 Action
        /// </summary>        ///         
        private void btnView_Click(object sender, EventArgs e)
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
               
               int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 1, null, null, btnSort);
               //Total부분 셀머지
               spdData.RPT_FillDataSelectiveCells("TOTAL", 0, 1, 0, 1, true, Align.Center, VerticalAlign.Center);
               spdData.ActiveSheet.Cells[0, 3].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, 2].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 1].Value)) * 100;
               spdData.ActiveSheet.Cells[0, 5].Value = (100 - Convert.ToDouble(spdData.ActiveSheet.Cells[0, 3].Value));
               spdData.ActiveSheet.Cells[0, 8].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, 7].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 6].Value)) * 100;
               
             
               dt.Dispose();
               
               //Chart 생성(LOSS 수량, 수율)
               if (spdData.ActiveSheet.RowCount > 0)
                     fnMakeChart2(spdData, spdData.ActiveSheet.RowCount);
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

        //공정 미선택 시 LOSS 수량과 YIELD 차트
        private void fnMakeChart2(Miracom.SmartWeb.UI.Controls.udcFarPoint SS, int iselrow)
        {
            /****************************************************
             * Comment : SS의 데이터를 Chart에 표시한다.
             * 
             * Created By : min-woo kim(2010-10-20)
             * 
             * Modified By : min-woo kim(2010-10-20)
             ****************************************************/
            int[] ich_mm = new int[iselrow-1]; int[] icols_mm = new int[iselrow-1]; int[] irows_mm = new int[iselrow-1]; string[] stitle_mm = new string[1];
            int[] yield_rows = new int[iselrow - 1]; 


            int icol = 0, irow = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                cf02.RPT_1_ChartInit();
                cf02.RPT_2_ClearData();
              

                cf02.AxisX.Staggered = false;
                for (icol = 0; icol < ich_mm.Length; icol++)
                {
                   icols_mm[icol] = icol + 1;
                    
                }
                for (irow = 0; irow < irows_mm.Length; irow++)
                {
                    irows_mm[irow] = irow + 1;
                    yield_rows[irow] = irow + 1;
                
                }
                String legendDescLoss = "LOSS [단위 : pcs]";
                String legendDescYield = "YIELD [단위 :%]";
                cf02.RPT_3_OpenData(2, icols_mm.Length);
                double max1 = cf02.RPT_4_AddData(SS, irows_mm, new int[] { 4 }, SeriseType.Column);
                double max2 = cf02.RPT_4_AddData(SS, irows_mm, new int[] { 3 }, SeriseType.Column);
                cf02.RPT_5_CloseData();
                cf02.RPT_6_SetGallery(ChartType.Bar, 0, 1, legendDescLoss, AsixType.Y, DataTypes.Initeger, max1 * 1.2);
                cf02.RPT_6_SetGallery(ChartType.Line, 1, 1, legendDescYield, AsixType.Y2, DataTypes.Initeger, max2 * 1.2);
                cf02.Series[0].PointLabels = true;
                cf02.Series[1].PointLabels = true;
                cf02.RPT_7_SetXAsixTitleUsingSpread(SS, irows_mm, 0);
                cf02.RPT_8_SetSeriseLegend(new string[] { "LOSS","YIELD"}, SoftwareFX.ChartFX.Docked.Top);
                cf02.AxisY.Gridlines = true;
                cf02.AxisY2.Gridlines = true;
                cf02.AxisY.DataFormat.Decimals = 0; 
                cf02.AxisY2.DataFormat.Decimals = 3;

                cf02.AxisY2.Step = 1;
                cf02.AxisY2.Max = 100;
                cf02.AxisY2.Min = 97;
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

        // 불량율 차트
        private void fnMakeChart(Miracom.SmartWeb.UI.Controls.udcFarPoint SS, int iselrow)
        {
            /****************************************************
             * Comment : SS의 데이터를 Chart에 표시한다.
             * 
             * Created By : min-woo kim(2010-10-20)
             * 
             * Modified By : min-woo kim(2010-10-20)
             ****************************************************/
            int[] ich_mm = new int[6]; int[] icols_mm = new int[dtLoss.Rows.Count]; int[] irows_mm = new int[1]; string[] stitle_mm = new string[1];
            int[] ich_ww = new int[iselrow - 1]; int[] icols_ww = new int[iselrow - 1]; int[] irows_ww = new int[iselrow]; string[] stitle_ww = new string[iselrow];
            
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                cf01.RPT_1_ChartInit();
                cf01.RPT_2_ClearData();

                cf01.AxisX.Title.Text = "Defect Status";
                cf01.Gallery = SoftwareFX.ChartFX.Gallery.Pie;

                cf01.LeftGap = 1;
                cf01.RightGap = 1;
                cf01.TopGap = 1;
                cf01.BottomGap = 1;

                cf01.AxisY.DataFormat.Format = 0;
                cf01.AxisX.Staggered = false;
                cf01.PointLabels = true;


                for (int i = 0; i < dtLoss.Rows.Count; i++)
                {
                    icols_mm[i] = 9+i;
                }
                
                irows_mm[0] = iselrow;
                stitle_mm[0] = SS.Sheets[0].Cells[iselrow, 0].Text;

                cf01.RPT_3_OpenData(irows_mm.Length, icols_mm.Length);
                cf01.RPT_4_AddData(SS, irows_mm, icols_mm, SeriseType.Rows);
                cf01.RPT_5_CloseData();

                //cf01.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 1, 6);

                //cf01.Legend[0] = "LOSS_123";// SS.Sheets[0].Cells[0, 7].Text;

                for (int i = 0; i < dtLoss.Rows.Count; i++)
                {
                    cf01.Legend[i] = SS.Sheets[0].Columns[9+i].Label;
                }
                cf01.RPT_8_SetSeriseLegend(stitle_mm, SoftwareFX.ChartFX.Docked.Top);
                cf01.AxisY.DataFormat.Decimals = 0;
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

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            ExcelHelper.Instance.subMakeExcel(spdData, cf01, this.lblTitle.Text, null, null);
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

        // 그리드에서 셀 선택시 해당 셀에 대한 Pie 차트
        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            /****************************************************
			 * Comment : SS의 Cell을 선택하면
			 * 
			 * Created By : bee-jae jung(2010-06-05-토요일)
			 * 
			 * Modified By : bee-jae jung(2010-06-05-토요일)
			 ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (e.Row >= 1)
                {
                    fnMakeChart(spdData, e.Row);
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
    }
}
