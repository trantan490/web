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

namespace Hana.YLD
{
    public partial class YLD040703 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {       
        /// <summary>
        /// 클  래  스: YLD040703<br/>
        /// 클래스요약: DC TREND(Scatter) <br/>
        /// 작  성  자: 김민우<br/>
        /// 최초작성일: 2010-10-04<br/>
        /// 상세  설명: DC TREND(Scatter)<br/>
        /// 변경  내용: <br/> 
        
        /// </summary>
        public YLD040703()
        {
            InitializeComponent();
            SortInit();
          
            GridColumnInit();
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

            String ss = DateTime.Now.ToString("MM-dd");
            string[] strDate = cdvFromToDate.getSelectDate();


            try
            {
                spdData.RPT_AddBasicColumn("DATE", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LOT_ID", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("DEVICE", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 160);
                spdData.RPT_AddBasicColumn("FAMILY", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("MD", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("DEN", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("TECH", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("ORG", 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("DC_IN_QTY", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DC_OUT_OTY", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.Number, 75);
                spdData.RPT_AddBasicColumn("DC_YLD", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.Double2, 70);
                spdData.RPT_AddBasicColumn("TIME", 0, 11, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("SS", 0, 12, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                /*
                for (int i = 0; i <= cdvFromToDate.SubtractBetweenFromToDate; i++)
                {
                  spdData.RPT_AddBasicColumn(strDate[i], 0, i + 1, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.Double2, 70);
                }
                */
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

            string QueryCond1;
            string QueryCond2;
           
            string[] strDate = cdvFromToDate.getSelectDate();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            strSqlString.Append("SELECT RECEIVE_DATE" + "\n");
            strSqlString.Append("     , LOT_ID" + "\n");
            strSqlString.Append("     , MAT_ID" + "\n");
            strSqlString.Append("     , FAMILY" + "\n");
            strSqlString.Append("     , MD" + "\n");
            strSqlString.Append("     , DEN" + "\n");
            strSqlString.Append("     , TECH" + "\n");
            strSqlString.Append("     , ORG" + "\n");
            strSqlString.Append("     , IN_QTY" + "\n");
            strSqlString.Append("     , OUT_QTY" + "\n");
            strSqlString.Append("     , YIELD" + "\n");
            strSqlString.Append("     , A.CREATE_TIME" + "\n");
            strSqlString.Append("     , RECEIVE_DATE||' - '||LOT_ID" + "\n");
            strSqlString.Append("  FROM IGTMLOTDCW@RPTTOMES A , MWIPCALDEF B " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND A.RECEIVE_DATE = B.SYS_DATE " + "\n");
            strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
            strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
            strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
            strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

            
            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                strSqlString.Append("           AND SUBSTR(RECEIVE_DATE,0,6) BETWEEN '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "' AND '" + cdvFromToDate.ToYearMonth.Value.ToString("yyyyMM") + "'" + "\n");
            }
            else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                strSqlString.Append("           AND B.SYS_YEAR||B.PLAN_WEEK BETWEEN '" + cdvFromToDate.HmFromWeek + "' AND '" + cdvFromToDate.HmToWeek + "'" + "\n");
            }
            else
            {
                strSqlString.Append("   AND RECEIVE_DATE >= " + cdvFromToDate.FromDate.Text.Replace("-", "") + "\n");
                strSqlString.Append("   AND RECEIVE_DATE <= " + cdvFromToDate.ToDate.Text.Replace("-", "") + "\n");
            }
          
            strSqlString.Append("   AND YIELD <= " + txtYield.Text + "\n");
           
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
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



            if (cdvCustomer.Text.Equals("") || cdvCustomer.Text.Equals("ALL"))
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD038", GlobalVariable.gcLanguage));
                return;
            }

            if (txtYield.Text.Equals("") || CmnFunction.CheckNumeric(txtYield.Text) == false)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD078", GlobalVariable.gcLanguage));
                return;
            }

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                if (txtSearchProduct.Text.Length < 3 && txtLotID.Text.Length < 3)
                {

                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD077", GlobalVariable.gcLanguage));
                    return;

                }
            }
            else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "DAY")
            {

            }
            else
            {
                if (txtSearchProduct.Text.Length < 3 && txtLotID.Text.Length < 3)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD077", GlobalVariable.gcLanguage));
                    return;
                }


            }

            GridColumnInit();
            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    udcChartFX1.RPT_1_ChartInit();
                    udcChartFX1.RPT_2_ClearData();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }


                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 1, null, null, btnSort);
                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("TOTAL", 0, 1, 0, 1, true, Align.Center, VerticalAlign.Center);

                //5. TAT 값 평균값 구하기(GrandTotal부분만 구함. SubTotal은 없음)
                double sum = 0;
                double avr = 0;
                double div = 0;

                for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++) // GrandTotal 부분 제외하기 위해 1부터 시작
                {
                    if (spdData.ActiveSheet.Cells[i, 10].Value != null)
                    {
                        sum += Convert.ToDouble(spdData.ActiveSheet.Cells[i, 10].Value);
                        div++;
                    }
                }
                avr = sum / div;
                spdData.ActiveSheet.Cells[0, 10].Value = Convert.ToDouble(avr.ToString());

                for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    if (Convert.ToDouble(spdData.ActiveSheet.Cells[i, 10].Value) < 99.00)
                    {
                        //spdData.ActiveSheet.Cells[i, 5].BackColor = Color.Red;
                        spdData.ActiveSheet.Rows[i].BackColor = Color.Red;
                    }
                }

                dt.Dispose();
                
                //Chart 생성
                if (spdData.ActiveSheet.RowCount > 0)
                {
                    fnMakeChart(spdData, spdData.ActiveSheet.RowCount);
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
            }
        }

        // 지난 주차의 마지막일 가져오기
        private string MakeSqlString2(string year, string date)
        {
            StringBuilder sqlString = new StringBuilder();

            sqlString.Append("SELECT MIN(SYS_DATE-1) " + "\n");
            sqlString.Append("  FROM MWIPCALDEF " + "\n");
            sqlString.Append(" WHERE 1=1" + "\n");
            sqlString.Append("   AND CALENDAR_ID='SE'" + "\n");
            sqlString.Append("   AND SYS_YEAR='" + year + "'\n");
            sqlString.Append("   AND PLAN_WEEK=(" + "\n");
            sqlString.Append("                  SELECT PLAN_WEEK " + "\n");
            sqlString.Append("                    FROM MWIPCALDEF " + "\n");
            sqlString.Append("                   WHERE 1=1 " + "\n");
            sqlString.Append("                     AND CALENDAR_ID='SE' " + "\n");
            sqlString.Append("                     AND SYS_DATE=TO_CHAR(TO_DATE('" + date + "','YYYYMMDD'),'YYYYMMDD')" + "\n");
            sqlString.Append("                 )" + "\n");

            return sqlString.ToString();
        }

        private void fnMakeChart(Miracom.SmartWeb.UI.Controls.udcFarPoint SS, int iselrow)
        {
            /****************************************************
             * Comment : SS의 데이터를 Chart에 표시한다.
             * 
             * Created By : min-woo kim(2010-10-05-화요일)
             * 
             * Modified By : min-woo kim(2010-10-05-화요일)
             ****************************************************/
            int[] ich_mm = new int[iselrow]; int[] icols_mm = new int[iselrow]; int[] irows_mm = new int[iselrow-1]; string[] stitle_mm = new string[iselrow];

            int icol = 0, irow = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                udcChartFX1.RPT_1_ChartInit();
                udcChartFX1.RPT_2_ClearData();
                udcChartFX1.AxisY.Font = new System.Drawing.Font("Tahoma", 8.25F);
                udcChartFX1.AxisX.Font = new System.Drawing.Font("Tahoma", 8.25F);
                udcChartFX1.AxisX.Title.Text = "DC TREND";
                udcChartFX1.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 13.25F);
                udcChartFX1.AxisY.Title.Text = "yield";

                udcChartFX1.AxisX.Staggered = false;
                for (icol = 0; icol < ich_mm.Length; icol++)
                {
                    ich_mm[icol] = icol +1;
                    icols_mm[icol] = icol +1;
                }
                for (irow = 0; irow < irows_mm.Length; irow++)
                {
                    irows_mm[irow] = irow +1;
                    //stitle_mm[irow] = SS.Sheets[0].Cells[irow, 0].Text;
                }

                udcChartFX1.RPT_3_OpenData(1, icols_mm.Length-1);
                double max1 = udcChartFX1.RPT_4_AddData(SS, irows_mm, new int[] { 10 }, SeriseType.Column);
                //double max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { 2 }, icols_mm, SeriseType.Rows);
                udcChartFX1.RPT_5_CloseData();
                //udcChartFX1.RPT_6_SetGallery(ChartType.Line, 0, 1, "", AsixType.Y, DataTypes.Double2, 100);
                //udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, 1);
                udcChartFX1.RPT_7_SetXAsixTitleUsingSpread(SS, irows_mm, 12);
                //udcChartFX1.RPT_8_SetSeriseLegend(stitle_mm, SoftwareFX.ChartFX.Docked.Bottom);
              
                udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Scatter;
                udcChartFX1.MarkerShape = SoftwareFX.ChartFX.MarkerShape.Circle;
                udcChartFX1.MarkerSize = 1;
                udcChartFX1.AxisY.Max = Convert.ToDouble(txtYield.Text);
                udcChartFX1.AxisY.Min = Convert.ToDouble(txtYield.Text) - 1 ;
                udcChartFX1.AxisY.Step = 0.1;
                udcChartFX1.AxisY.LabelsFormat.Decimals = 2;
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
            if (spdData.ActiveSheet.Rows.Count > 0)
            {
                StringBuilder Condition = new StringBuilder();
                ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);
                //spdData.ExportExcel();            
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
        #endregion

    }
}
        