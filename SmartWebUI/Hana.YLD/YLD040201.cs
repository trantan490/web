using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.UI.Controls;

namespace Hana.YLD
{
    public partial class YLD040201 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: YLD040201<br/>
        /// 클래스요약: 수율 Trend<br/>
        /// 작  성  자: 미라콤 김태순<br/>
        /// 최초작성일: 2008-10-08<br/>
        /// 상세  설명: 수율 항목별 데이터를 트렌드 차트로 조회한다.<br/>
        /// 변경  내용: <br/>
        /// </summary>
        public YLD040201()
        {
            InitializeComponent();
            cdvFromTo.AutoBinding();

            SortInit();
            GridColumnInit(); //헤더 한줄짜리 

            udcChartFX1.RPT_1_ChartInit();  //차트 초기화.
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS MAT_GRP_1", "MAT_GRP_1", "MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT_ID", "MAT_ID", "SHIP.MAT_ID", false);
        }

        #endregion

        #region 한줄헤더생성

        /// <summary>
        /// 한줄헤더생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Product", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddDynamicColumn(cdvFromTo, 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 60);

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
        }

        #endregion

        #region 조회

        /// <summary>
        /// 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            if (!CheckField()) return;

            DataTable dt = null;

            GridColumnInit();

            try
            {
                //LoadingPopUp.LoadIngPopUpShow(this);
                spdData_Sheet1.RowCount = 0;
                this.Refresh();
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //// 두줄짜리이상 샘플코드(부분합계 제공 안됨-총 합계만 가능.)
                //spdData.DataSource = dt;

                ////표구성에따른 항목 Display
                //spdData.RPT_ColumnConfigFromTable(btnSort);

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 9, null, null, btnSort);

                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 9, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                for (int i = 0; i < cdvFromTo.SubtractBetweenFromToDate + 1; i++)
                {
                    SetAvgVertical(1, i + 9, false);
                }

             
                //Chart 생성
                if (spdData.ActiveSheet.RowCount > 0)
                    ShowChart(0);
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

        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            if (cdvOper.FromText.Trim().Length == 0 || cdvOper.ToText.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        #endregion

        #region MakeSqlString
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string[] selectDate1 = new string[cdvFromTo.SubtractBetweenFromToDate + 1];
            string strFromDate = cdvFromTo.ExactFromDate;
            string strToDate = cdvFromTo.ExactToDate;
            string strDate = string.Empty;
            string sFrom = null;
            string sTo = null;
            string option = null;

            int Between = cdvFromTo.SubtractBetweenFromToDate + 1;

            selectDate1 = cdvFromTo.getSelectDate();

            switch (cdvFromTo.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    strDate = "WORK_DATE";
                    sFrom = cdvFromTo.FromDate.Text.Replace("-", "");
                    sTo = cdvFromTo.ToDate.Text.Replace("-", "");
                    option = "D";
                    break;
                case "WEEK":
                    strDate = "WORK_WEEK";
                    sFrom = cdvFromTo.FromWeek.SelectedValue.ToString();
                    sTo = cdvFromTo.ToWeek.SelectedValue.ToString();
                    option = "W";
                    break;
                case "MONTH":
                    strDate = "WORK_MONTH";
                    sFrom = cdvFromTo.FromYearMonth.Value.ToString("yyyyMM");
                    sTo = cdvFromTo.ToYearMonth.Value.ToString("yyyMM");
                    option = "M";
                    break;
                default:
                    strDate = "WORK_DATE";
                    sFrom = cdvFromTo.FromDate.Text.Replace("-", "");
                    sTo = cdvFromTo.ToDate.Text.Replace("-", "");
                    option = "D";
                    break;
            }

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond1);

            for (int i = 0; i < Between; i++)
            {
                strSqlString.AppendFormat("     , SUM(YIELD{0}) " + "\n", i.ToString());
            }

            strSqlString.Append(" FROM ( " + "\n");
            strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond2);

            for (int i = 0; i < Between; i++)
            {
                strSqlString.AppendFormat("     , ROUND(DECODE({0}, '{1}', DECODE(SUM(IN_QTY), 0, 0, (SUM(OUT_QTY)/SUM(IN_QTY)*100)), 0), 3) AS YIELD{2}" + "\n", strDate, selectDate1[i].ToString(), i.ToString());
            }

            strSqlString.Append("  FROM (" + "\n");
            strSqlString.AppendFormat("        SELECT {0}, LOT_ID" + "\n", QueryCond2);
            strSqlString.AppendFormat("             , GET_WORK_DATE(SHIP_TIME, '{0}') AS {1}, SUM(IN_QTY) AS IN_QTY, SUM(OUT_QTY) AS OUT_QTY " + "\n", option, strDate);
            strSqlString.Append("          FROM (              " + "\n");
            strSqlString.AppendFormat("                    SELECT {0}, LTH.LOT_ID " + "\n", QueryCond3);
            strSqlString.Append("                         , SHIP_TIME" + "\n");
            strSqlString.AppendFormat("                         , SUM(CASE WHEN OPER = '{0}' THEN OPER_IN_QTY_1 ELSE 0 END) AS IN_QTY" + "\n", cdvOper.FromText);
            strSqlString.AppendFormat("                         , SUM(CASE WHEN OPER = '{0}' THEN END_QTY_1 ELSE 0 END) AS OUT_QTY" + "\n", cdvOper.ToText);
            strSqlString.Append("                      FROM RSUMOUTLTH LTH" + "\n");
            strSqlString.Append("                         , MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                         , (" + "\n");
            strSqlString.Append("                             SELECT B.MAT_ID, B.LOT_ID, B.FACTORY, A.TRANS_TIME SHIP_TIME                    " + "\n");
            strSqlString.Append("                               FROM CWIPSHPINF A " + "\n");
            strSqlString.Append("                                  , CWIPSHPLOT B " + "\n");
            strSqlString.Append("                              WHERE 1 = 1 " + "\n");
            strSqlString.Append("                                AND A.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                                AND A.SHP_FLAG = 'Y' " + "\n");
            strSqlString.Append("                                AND A.SHP_FAC ='CUSTOMER'" + "\n");
            strSqlString.AppendFormat("                                AND A.TRANS_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
            strSqlString.Append("                                AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                                AND A.INVOICE_NO = B.INVOICE_NO" + "\n");
            strSqlString.Append("                           GROUP BY B.MAT_ID, B.LOT_ID, B.FACTORY, A.TRANS_TIME " + "\n");
            strSqlString.Append("                           ) SHIP " + "\n");
            strSqlString.Append("                     WHERE 1=1 " + "\n");
            strSqlString.Append("                       AND SHIP.LOT_ID = LTH.LOT_ID(+) " + "\n");
            strSqlString.Append("                       AND SHIP.FACTORY = LTH.FACTORY(+) " + "\n");
            strSqlString.Append("                       AND SHIP.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("                       AND SHIP.MAT_ID = MAT.MAT_ID  " + "\n");
            strSqlString.Append("                       AND MAT.MAT_VER = 1 " + "\n");
            strSqlString.Append("                       AND LTH.MAT_VER(+) = 1  " + "\n");
            strSqlString.AppendFormat("                       AND LTH.OPER IN ('{0}', '{1}')  " + "\n", cdvOper.FromText, cdvOper.ToText);
            strSqlString.Append("                       AND LTH.CM_FIELD_2(+) = 'PROD' " + "\n");


            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                       AND MAT.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                       AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                       AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                       AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                       AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                       AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                       AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                       AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            strSqlString.AppendFormat("                  GROUP BY {0}, LTH.LOT_ID, SHIP_TIME" + "\n", QueryCond3);
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND IN_QTY <> 0 " + "\n");
            strSqlString.Append("           AND OUT_QTY <> 0" + "\n");
            strSqlString.AppendFormat("         GROUP BY {0}, GET_WORK_DATE(SHIP_TIME, '{1}'), LOT_ID" + "\n", QueryCond2, option);
            strSqlString.Append("   )" + "\n");
            strSqlString.AppendFormat("GROUP BY {0}, {1} " + "\n", QueryCond2, strDate);
            strSqlString.Append(" ) " + "\n");
            strSqlString.AppendFormat("GROUP BY {0} " + "\n", QueryCond2);
            strSqlString.AppendFormat("ORDER BY {0} " + "\n", QueryCond2);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region ShowChart

        private void ShowChart(int rowCount)
        {
            // 차트 설정
            udcChartFX1.RPT_1_ChartInit();
            udcChartFX1.RPT_2_ClearData();

            // Assertion
            if (((DataTable)spdData.DataSource).Rows.Count < 1)
                return;

            int step = cdvFromTo.SubtractBetweenFromToDate + 1;
            if (step < 1)
                return;

            // Chart에 바인딩할 자료
            List <object []> data = new List<object []>();

            // TOTAL Series 먼저 추가
            object[] total = new object[step + 1];
            total[0] = "Total";
            for (int j = 0; j < step; j++)
                total[j + 1] = spdData.ActiveSheet.Cells[0, j + 9].Value;
            data.Add(total);

            // Sub Total 행을 개별 Series로 뽑아내기
            for(int i=0; i<spdData.ActiveSheet.Rows.Count; i++)
            {
                if(spdData.ActiveSheet.Cells[i, 0].Value.ToString().IndexOf("Sub Total") > 0)
                {
                    object[] series = new object[step + 1];
                    series[0] = spdData.ActiveSheet.Cells[i, 0].Value.ToString().Replace(" Sub Total", "");
                    for(int j=0; j<step; j++)
                    {
                        series[j + 1] = spdData.ActiveSheet.Cells[i, j + 9].Value;
                    }
                    data.Add(series);
                }
            }

            // Legend로 쓸 Date 정보 가져오기
            string [] arrLegend = cdvFromTo.getSelectDate();

            // Chart Series, Legend 갯수 초기화
            udcChartFX1.RPT_3_OpenData(data.Count, step);

            // Chart Value 셋팅
            for (int i = 0; i < data.Count; i++)
            {
                for(int j=0; j<step; j++)
                {
                    udcChartFX1.Legend[j] = arrLegend[j];
                    udcChartFX1.Value[i, j] = Convert.ToDouble(data[i][j+1]);
                }

                // Series Visual 셋팅
                udcChartFX1.SerLeg[i] = data[i][0].ToString();
                udcChartFX1.Series[i].Gallery = SoftwareFX.ChartFX.Gallery.Lines;
                udcChartFX1.Series[i].LineWidth = 2;
            }
            udcChartFX1.RPT_5_CloseData();

            // Y 축 Scale 조정
            udcChartFX1.AxisY.Max = 102;
            udcChartFX1.AxisY.Min = 96;

            // 기타 설정
            udcChartFX1.SerLegBox = true;
            udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
            udcChartFX1.PointLabels = true;
            udcChartFX1.AxisY.LabelsFormat.Decimals = 3;
            udcChartFX1.AxisY.DataFormat.Decimals = 3;
        }
        #endregion

        #endregion

        #region ToExcel

        /// <summary>
        /// ToExcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();            
        }

        #endregion

        #region cdvFactory_ValueSelectedItemChanged

        /// <summary>
        /// cdvFactory_ValueSelectedItemChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            cdvOper.sFactory = cdvFactory.txtValue;
        }

        #endregion

        public void SetAvgVertical(int nSampleNormalRowPos, int nColPos, bool bWithNull)
        {
            double sum = 0;
            double totalSum = 0;

            double divide = 0;
            double totalDivide = 0;

            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;

            for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                if (spdData.ActiveSheet.Cells[i, nColPos].BackColor == color)
                {
                    sum += Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos].Value);

                    if (!bWithNull && (spdData.ActiveSheet.Cells[i, nColPos].Value == null || spdData.ActiveSheet.Cells[i, nColPos].Value.ToString().Trim() == ""))
                        continue;

                    divide += 1;
                }
                else
                {
                    if (divide != 0)
                        spdData.ActiveSheet.Cells[i, nColPos].Value = sum / divide;

                    totalSum += sum;
                    totalDivide += divide;

                    sum = 0;
                    divide = 0;
                }
            }
            if (totalDivide != 0)
                spdData.ActiveSheet.Cells[0, nColPos].Value = totalSum / totalDivide;

            return;
        }

    }        
}
