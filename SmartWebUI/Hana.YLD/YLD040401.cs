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
    public partial class YLD040401 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: YLD040401<br/>
        /// 클래스요약: 수율 팀별<br/>
        /// 작  성  자: 미라콤 김민규<br/>
        /// 최초작성일: 2008-10-08<br/>
        /// 상세  설명: 수율 팀별 데이터를 조회한다.<br/>
        /// 변경  내용: <br/>
        /// </summary>
        public YLD040401()
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Depart", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME='H_DEPARTMENT' AND KEY_1 = OPER_GRP_2 AND ROWNUM=1), '-') AS TEAM", "OPER_GRP_2", "OPER_GRP_2", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') as Customer", "MAT_GRP_1", "MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT_ID", "MAT_ID", "A.MAT_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lot ID", "LOT_ID", "LOT_ID", "A.LOT_ID", false);
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
            spdData.RPT_AddBasicColumn("Depart", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Customer", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Family", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("LD Count", 0, 6, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Density", 0, 7, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Generation", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Lot Id", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddDynamicColumn(cdvFromTo, 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 60);
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
                spdData_Sheet1.RowCount = 0;
                this.Refresh();
                LoadingPopUp.LoadIngPopUpShow(this);
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                ////by John (한줄짜리)
                ////1.Griid 합계 표시            
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 10, null, null, btnSort);
                
                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 1, true, Align.Center, VerticalAlign.Center);

                ////4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                // yield TOTAL부분 직접계산
                for (int i = 0; i < cdvFromTo.SubtractBetweenFromToDate + 1; i++)
                {
                    SetAvgVertical(1, i + 11, false);
                }

                ShowChart(0);              // 챠트 그리기  

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

        #endregion

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
            List<object[]> data = new List<object[]>();

            // Sub Total 행을 개별 Series로 뽑아내기
            for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                if (spdData.ActiveSheet.Cells[i, 0].Value.ToString().IndexOf("Sub Total") > 0)
                {
                    object[] series = new object[step + 1];
                    series[0] = spdData.ActiveSheet.Cells[i, 0].Value.ToString().Replace(" Sub Total", "");
                    for (int j = 0; j < step; j++)
                    {
                        series[j + 1] = spdData.ActiveSheet.Cells[i, j + 11].Value;
                    }
                    data.Add(series);
                }
            }

            // Legend로 쓸 Date 정보 가져오기
            string[] arrLegend = cdvFromTo.getSelectDate();

            // Chart Series, Legend 갯수 초기화
            udcChartFX1.RPT_3_OpenData(data.Count, step);

            // Chart Value 셋팅
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < step; j++)
                {
                    udcChartFX1.Legend[j] = arrLegend[j];
                    udcChartFX1.Value[i, j] = Convert.ToDouble(data[i][j + 1]);
                }

                // Series Visual 셋팅
                udcChartFX1.SerLeg[i] = data[i][0].ToString();
                udcChartFX1.Series[i].Gallery = SoftwareFX.ChartFX.Gallery.Lines;
                udcChartFX1.Series[i].LineWidth = 2;
            }
            udcChartFX1.RPT_5_CloseData();

            // Y 축 Scale 조정
            udcChartFX1.AxisY.Max = 100.05;
            udcChartFX1.AxisY.Min = 99.5;

            // 기타 설정
            udcChartFX1.SerLegBox = true;
            udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
            udcChartFX1.PointLabels = true;
            udcChartFX1.AxisY.LabelsFormat.Decimals = 3;
            udcChartFX1.AxisY.DataFormat.Decimals = 3;
        }
        #endregion

        #region MakeSqlString
        /// <summary>
        /// SQL 쿼리 Build
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string[] FromTo = new string[cdvFromTo.SubtractBetweenFromToDate + 1];
            string strFromDate = cdvFromTo.ExactFromDate;
            string strToDate = cdvFromTo.ExactToDate;
            string strDate = string.Empty;
            string option = null;
            string sFrom = null;
            string sTo = null;

            int Between = cdvFromTo.SubtractBetweenFromToDate + 1;

            FromTo = cdvFromTo.getSelectDate();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            switch (cdvFromTo.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    sFrom = cdvFromTo.FromDate.Text.Replace("-", "");
                    sTo = cdvFromTo.ToDate.Text.Replace("-", "");
                    strDate = "WORK_DATE";
                    option = "D";
                    break;
                case "WEEK":
                    sFrom = cdvFromTo.FromWeek.SelectedValue.ToString();
                    sTo = cdvFromTo.ToWeek.SelectedValue.ToString();
                    strDate = "WORK_WEEK";
                    option = "W";
                    break;
                case "MONTH":
                    sFrom = cdvFromTo.FromYearMonth.Value.ToString("yyyyMM");
                    sTo = cdvFromTo.ToYearMonth.Value.ToString("yyyMM");
                    strDate = "WORK_MONTH";
                    option = "M";
                    break;
                default:
                    sFrom = cdvFromTo.FromDate.Text.Replace("-", "");
                    sTo = cdvFromTo.ToDate.Text.Replace("-", "");
                    strDate = "WORK_DATE";
                    option = "D";
                    break;
            }

            strSqlString.AppendFormat("   SELECT {0}" + "\n", QueryCond1);

            for (int i = 0; i < Between; i++)
            {
                //strSqlString.AppendFormat("        , SUM(IN_QTY{0}) AS IN_QTY{0}" + "\n", i.ToString());
                //strSqlString.AppendFormat("        , SUM(OUT_QTY{0}) AS OUT_QTY{0}" + "\n", i.ToString());
                strSqlString.AppendFormat("        , SUM(YIELD{0}) AS YIELD{0} " + "\n", i.ToString());
            }

            strSqlString.Append("     FROM (" + "\n");
            strSqlString.AppendFormat("          SELECT {0}, {1}" + "\n", QueryCond2, strDate);

            for (int i = 0; i < Between; i++)
            {
                //strSqlString.AppendFormat("               , SUM(DECODE({0}, '{1}', IN_QTY, 0)) AS IN_QTY{2} " + "\n", strDate, FromTo[i].ToString(), i.ToString());
                //strSqlString.AppendFormat("               , SUM(DECODE({0}, '{1}', OUT_QTY, 0)) AS OUT_QTY{2} " + "\n", strDate, FromTo[i].ToString(), i.ToString());
                strSqlString.AppendFormat("               , ROUND(DECODE({0}, '{1}', DECODE(SUM(IN_QTY), 0, 0, (SUM(OUT_QTY)/SUM(IN_QTY)*100)), 0), 3) AS YIELD{2}  " + "\n", strDate, FromTo[i].ToString(), i.ToString());
            }

            strSqlString.Append("            FROM (            " + "\n");
            strSqlString.AppendFormat("                 SELECT {0}, {1}" + "\n", QueryCond2, strDate);
            strSqlString.Append("                      , SUM(IN_QTY) AS IN_QTY, SUM(OUT_QTY) AS OUT_QTY " + "\n");
            strSqlString.Append("                   FROM (" + "\n");
            strSqlString.AppendFormat("                        SELECT {0}" + "\n", QueryCond3);
            strSqlString.AppendFormat("                             , GET_WORK_DATE(END_TIME, '{0}') AS {1}" + "\n", option, strDate);
            strSqlString.AppendFormat("                             , SUM(CASE WHEN OPER_IN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
            strSqlString.Append("                                        THEN OPER_IN_QTY_1  " + "\n");
            strSqlString.Append("                                        ELSE 0" + "\n");
            strSqlString.Append("                               END) AS IN_QTY" + "\n");
            strSqlString.AppendFormat("                             , SUM(CASE WHEN END_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
            strSqlString.Append("                                        THEN END_QTY_1  " + "\n");
            strSqlString.Append("                                        ELSE 0" + "\n");
            strSqlString.Append("                               END) AS OUT_QTY  " + "\n");
            strSqlString.Append("                          FROM RSUMWIPLTH A     " + "\n");
            strSqlString.Append("                             , MWIPMATDEF B" + "\n");
            strSqlString.Append("                             , MWIPOPRDEF C" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND A.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                           AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                           AND A.FACTORY = C.FACTORY" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                           AND A.OPER = C.OPER  " + "\n");
            strSqlString.Append("                           AND B.MAT_VER = 1                    " + "\n");
            strSqlString.AppendFormat("                           AND A.OPER IN ({0})" + "\n", cdvOper.getInQuery());
            strSqlString.AppendFormat("                           AND A.OPER_IN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
            strSqlString.AppendFormat("                           AND A.END_TIME BETWEEN '{0}' AND '{1}'  " + "\n", strFromDate, strToDate);

            if(cdvDepart.txtValue != "ALL" && cdvDepart.txtValue != "")
            {
                strSqlString.Append("                           AND C.OPER_GRP_2 " + cdvDepart.SelectedValueToQueryString + "\n");
            }

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            strSqlString.AppendFormat("                      GROUP BY {0}, END_TIME " + "\n", QueryCond3);
            strSqlString.Append("                        )" + "\n");
            strSqlString.Append("                  WHERE IN_QTY <> 0 AND OUT_QTY <> 0 " + "\n");
            strSqlString.AppendFormat("               GROUP BY {0}, {1}" + "\n", QueryCond2, strDate);
            strSqlString.Append("                 )" + "\n");
            strSqlString.AppendFormat("             GROUP BY {0}, {1}" + "\n", QueryCond2, strDate);
            strSqlString.Append("          )" + "\n");
            strSqlString.AppendFormat("         GROUP BY {0} " + "\n", QueryCond2);
            strSqlString.AppendFormat("         ORDER BY {0} " + "\n", QueryCond2);
            
            // System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
            
            return strSqlString.ToString();
        }

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
            this.cdvOper.sFactory = cdvFactory.txtValue;
            this.cdvDepart.sFactory = cdvFactory.txtValue;                            
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

