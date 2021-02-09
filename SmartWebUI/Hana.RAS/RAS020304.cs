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

namespace Hana.RAS
{
    /// <summary>
    /// 클  래  스: RAS020304<br/>
    /// 클래스요약: Trend TOP 10<br/>
    /// 작  성  자: 미라콤 양형석<br/>
    /// 최초작성일: 2009-01-07<br/>
    /// 상세  설명: Trend TOP 10<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class RAS020304 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public RAS020304()
        {
            InitializeComponent();
            udcFromToDate.AutoBinding();
            udcFromToDate.DaySelector.SelectedValue = "MONTH";
            SortInit();
            GridColumnInit();
            udcChartFX1.RPT_1_ChartInit();  //차트 초기화. 
        }

        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            //spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            //spdData.ActiveSheet.RowHeader.ColumnCount = 0;
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("Team in charge", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Part", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("EQP Type", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Maker", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Model", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Equipment name", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);

            spdData.RPT_AddBasicColumn("Code", 0, 6, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Code name", 0, 7, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Item", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);

            spdData.RPT_AddDynamicColumn(udcFromToDate, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Team in charge", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_1), '-') AS TEAM", "TEAM", "RES.RES_GRP_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Part", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_2), '-') AS PART", "PART", "RES.RES_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("EQP Type", "RES.RES_GRP_3 AS EQP_TYPE", "EQP_TYPE", "RES_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Maker", "RES.RES_GRP_5 AS MAKER", "MAKER", "RES.RES_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "RES.RES_GRP_6 AS MODEL", "MODEL", "RES.RES_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment name", "RES.RES_ID AS RES_ID", "RES_ID", "RES.RES_ID", false);
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            string strMtbaAlias = string.Empty;
            string strRunTimeAlias = string.Empty;
            string strCntAlias = string.Empty;

            string strCutOff = string.Empty;
            string strJoupTime = string.Empty;
            string strMTBA = string.Empty;
            string strRunTime = string.Empty;
            string strCnt = string.Empty;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            switch (udcFromToDate.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    strCutOff = "D";
                    strJoupTime = "1440";
                    break;
                case "WEEK":
                    strCutOff = "W";
                    strJoupTime = "10080"; // 7 * 1440
                    break;
                case "MONTH":
                    strCutOff = "M";
                    strJoupTime = "(TO_CHAR(LAST_DAY(TO_DATE(GET_WORK_DATE(DOWN_DATE, 'M'), 'YYYYMM')), 'DD') * 1440)";
                    break;
            }

            #region " udcDurationPlus에서 정확한 조회시간을 받아오기 : strFromDate, strToDate "

            string strFromDate = udcFromToDate.ExactFromDate;
            string strToDate = udcFromToDate.ExactToDate;

            #endregion

            #region " DECODE 쿼리 스트링 구하기 : strMTBA, strCnt "

            for (int i = 0; i < udcFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                strMtbaAlias += ", MTBA_" + i.ToString();
                strRunTimeAlias += ", RUN_TIME_" + i.ToString();
                strCntAlias += ", CNT_" + i.ToString();
            }

            strMTBA = udcFromToDate.getDecodeQuery("MAX(DECODE(ALM.CUTOFF_DT", "ROUND(DECODE(ALM.CNT, 0, 0, NVL(RUN_TIME, (TO_DATE('" + strToDate + "', 'YYYYMMDDHH24MISS') - TO_DATE('" + strFromDate + "', 'YYYYMMDDHH24MISS'))*1440)/ALM.CNT), 4), 0))", "MTBA_").Replace(", MAX(", "             , MAX(");
            strRunTime = udcFromToDate.getDecodeQuery("MAX(DECODE(ALM.CUTOFF_DT", "NVL(RUN_TIME, " + ((strCutOff!="M")?strJoupTime:strJoupTime.Replace("GET_WORK_DATE(DOWN_DATE, 'M')", "ALM.CUTOFF_DT")) + "), 0))", "RUN_TIME_").Replace(", MAX(", "             , MAX(");
            strCnt = udcFromToDate.getDecodeQuery("MAX(DECODE(ALM.CUTOFF_DT", "ALM.CNT, 0))", "CNT_").Replace(", MAX(", "             , MAX(");

            #endregion

            #region " MAIN 쿼리 "
            strSqlString.Append("SELECT " + QueryCond2 + " " + "\n");
            strSqlString.Append("     , ALM_ID " + "\n");
            strSqlString.Append("     , ALM_DESC " + "\n");
            strSqlString.Append("     , ' ' GUBUN " + "\n");
            strSqlString.Append("     " + strMtbaAlias + "\n");
            strSqlString.Append("     " + strRunTimeAlias + "\n");
            strSqlString.Append("     " + strCntAlias + "\n");
            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT " + QueryCond1 + " " + "\n");
            strSqlString.Append("             , ALM.ALM_ID " + "\n");
            strSqlString.Append("             , ALM.ALM_DESC " + "\n");
            strSqlString.Append("             , ROW_NUMBER() OVER(ORDER BY ALM.CNT DESC) RNK " + "\n");
            strSqlString.Append(strMTBA);
            strSqlString.Append(strRunTime);
            strSqlString.Append(strCnt);
            strSqlString.Append("          FROM (  " + "\n");
            strSqlString.Append("                    SELECT GET_WORK_DATE(TRAN_TIME, '" + strCutOff + "') CUTOFF_DT, FACTORY, RES_ID, ALARM_ID ALM_ID, ALARM_DESC ALM_DESC, COUNT(*) CNT   " + "\n");
            strSqlString.Append("                      FROM CRASALMHIS  " + "\n");
            strSqlString.Append("                     WHERE 1=1  " + "\n");
            strSqlString.Append("                       AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  " + "\n");
            strSqlString.Append("                       AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                       AND CLEAR_TIME > 0  " + "\n");
            strSqlString.Append("                     GROUP BY GET_WORK_DATE(TRAN_TIME, '" + strCutOff + "'), FACTORY, RES_ID, ALARM_ID, ALARM_DESC " + "\n");
            strSqlString.Append("               ) ALM  " + "\n");
            strSqlString.Append("             , (  " + "\n");
            strSqlString.Append("                    SELECT GET_WORK_DATE(DOWN_DATE, '" + strCutOff + "') CUTOFF_DT, FACTORY, RES_ID, ROUND(" + strJoupTime + " - SUM(CASE WHEN TIME_SUM>86400 THEN 0 ELSE TIME_SUM/60 END),0) RUN_TIME " + "\n");
            strSqlString.Append("                      FROM CSUMRESDWH  " + "\n");
            strSqlString.Append("                     WHERE 1=1 " + "\n");
            strSqlString.Append("                       AND DOWN_DATE BETWEEN GET_WORK_DATE('" + strFromDate + "', 'D') AND GET_WORK_DATE('" + strToDate + "', 'D') " + "\n");
            strSqlString.Append("                       AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                     GROUP BY GET_WORK_DATE(DOWN_DATE, '" + strCutOff + "'), FACTORY, RES_ID " + "\n");
            strSqlString.Append("               ) DWH " + "\n");
            strSqlString.Append("             , MRASRESDEF RES    " + "\n");
            strSqlString.Append("         WHERE 1=1  " + "\n");
            strSqlString.Append("           AND ALM.CUTOFF_DT = DWH.CUTOFF_DT(+) " + "\n");
            strSqlString.Append("           AND ALM.FACTORY = DWH.FACTORY(+)  " + "\n");
            strSqlString.Append("           AND ALM.RES_ID = DWH.RES_ID(+) " + "\n");
            strSqlString.Append("           AND ALM.FACTORY = RES.FACTORY " + "\n");
            strSqlString.Append("           AND ALM.RES_ID = RES.RES_ID " + "\n");

            #region " RAS 상세 조회 "
            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

            strSqlString.Append("   AND RES.RES_TYPE NOT IN ('DUMMY')" + "\n");
            #endregion

            strSqlString.Append("         GROUP BY RES.FACTORY, " + QueryCond3 + ", ALM.ALM_ID, ALM_DESC, ALM.CNT " + "\n");
            strSqlString.Append("       ) " + "\n");
            strSqlString.Append(" WHERE RNK < 11 " + "\n");
            strSqlString.Append(" ORDER BY RNK " + "\n");
            #endregion

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        /// <summary>
        /// 5. Chart 생성
        /// </summary>
        /// <param name="DT">Chart를 생성할 데이터 테이블</param>
        private void MakeChart(DataTable DT)
        {

        }

        /// <summary>
        /// 그룹별 체크박스가 체크되어 있을 때 스프레드의 특정 Row들을 ChartFX로 보여주는 메서드
        /// </summary>
        /// <param name="nRowGroupUnitCount">스프레드에서 그룹으로 피봇되어 있는 행들의 개수</param>
        /// <param name="nTargetRowPos">해당 그룹에서의 행 위치</param>
        /// <param name="nStartRow">위치 계산을 시작할 처음 행 위치</param>
        /// <param name="nStartColumn">값들이 시작되는 처음 열 위치</param>
        /// <param name="nLegendColumn">시리즈의 범례로 지정할 칼럼 위치</param>
        /// <param name="nStep">값들이 포함된 열의 개수</param>
        private void ShowChartByGroup(int nRowGroupUnitCount, int nTargetRowPos, int nStartRow, int nStartColumn, int nLegendColumn, int nStep)
        {
            DataTable dt = (DataTable)spdData.DataSource;

            // 차트 설정
            udcChartFX1.RPT_1_ChartInit();
            udcChartFX1.RPT_2_ClearData();

            // Validation
            if (dt.Rows.Count < nRowGroupUnitCount)
                return;
            if ((dt.Rows.Count - nStartRow) % nRowGroupUnitCount > 0)
                return;
            if (nRowGroupUnitCount < 1 || nStartColumn < 1 || nStep < 1)
                return;

            // 시리즈 개수 구하기
            int nSeries = (dt.Rows.Count - nStartRow) / nRowGroupUnitCount;

            // 차트 초기화
            udcChartFX1.RPT_3_OpenData(nSeries, nStep);

            // 칼럼 헤더 초기화
            int[] columnsHeader = new Int32[nStep];
            for (int i = 0; i < columnsHeader.Length; i++)
            {
                columnsHeader[i] = nStartColumn + i;
            }

            // 범례 칼럼 실제 위치 구하기
            for (int i = nLegendColumn; i >= 0; i--)
            {
                if (spdData.ActiveSheet.Columns[i].Visible == true)
                {
                    nLegendColumn = i;
                    break;
                }
            }

            // 시리즈별로 차트에 데이터 입력
            double temp = 0;
            double max = 0;
            int nRowCount = 0;
            int[] value_columns = new Int32[nStep];
            string[] arrStrLegend = new string[nSeries];
            for (int i = nStartRow + nTargetRowPos; i < dt.Rows.Count; i += nRowGroupUnitCount)
            {
                value_columns = new Int32[nStep];
                for (int j = 0; j < value_columns.Length; j++)
                {
                    value_columns[j] = nStartColumn + j;
                }

                temp = udcChartFX1.RPT_4_AddData(spdData, new int[] { i }, value_columns, SeriseType.Rows);
                max = (max < temp) ? temp : max;

                arrStrLegend[nRowCount] = dt.Rows[i][nLegendColumn].ToString();
                nRowCount++;
            }
            udcChartFX1.RPT_5_CloseData();

            //각 Serise별로 동일한 타입을 사용할 경우
            udcChartFX1.RPT_6_SetGallery(ChartType.Line, " ", AsixType.Y, DataTypes.Initeger, max * 1.2);

            // x 축 텍스트 입력
            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);

            // 시리즈 범례
            udcChartFX1.RPT_8_SetSeriseLegend(arrStrLegend, SoftwareFX.ChartFX.Docked.Top);

            // 기타 설정
            udcChartFX1.PointLabels = false;
            udcChartFX1.Series[0].PointLabelColor = Color.Blue;
            udcChartFX1.Series[1].PointLabelColor = Color.Green;
            udcChartFX1.Series[0].LineWidth = 2;
            udcChartFX1.RecalcScale();
            //udcChartFX1.AxisY.ScaleUnit = 0.01;
        }

        #endregion

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (CheckField() == false) return;

            GridColumnInit();

            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                this.Refresh();

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //by John
                //1.Griid 합계 표시
                spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 0, 9, 9, 3, udcFromToDate.SubtractBetweenFromToDate + 1);

                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 8, 0, 3, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit4
                spdData.RPT_AutoFit(false);

                spdData.RPT_FillColumnData(8, new string[] { "MTBA", "Uptime", "Number" });

                SetSpreadExpression();

                ShowChartByGroup(3, 2, 3, 9, 6, udcFromToDate.SubtractBetweenFromToDate + 1);
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

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ");
            spdData.ExportExcel();           
        }

        #region 기타
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            //cdvProduct.sFactory = cdvFactory.txtValue;
        }

        /// <summary>
        /// 스프레드의 특정 Row에 CellType을 다르게 지정하는 메서드입니다.
        /// </summary>
        private void SetSpreadExpression()
        {
            int nStartValueColumn = 9;      // 값들이 시작되는 열 위치
            int nStartRow = 0;              // 값들이 시작되는 행 위치
            int nRowCountPerUnit = 3;       // 피봇으로 묶였을 때 하나의 그룹당 행 개수, 피봇이 아니면 1
            int nRowOffset = 2;             // 해당 그룹에서의 행 위치

            // Cell Type
            FarPoint.Win.Spread.CellType.NumberCellType ctNumber = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType ctDouble = new FarPoint.Win.Spread.CellType.NumberCellType();
            ctNumber.DecimalPlaces = 0;
            ctDouble.DecimalPlaces = 2;

            Double value = 0;
            for (int i = nStartRow + nRowOffset; i < spdData.ActiveSheet.Rows.Count; )
            {
                for (int j = nStartValueColumn; j < spdData.ActiveSheet.Columns.Count; j++)
                {
                    spdData.ActiveSheet.Cells[i, j].CellType = ctNumber;

                    // 수식
                    value = Convert.ToDouble(spdData.ActiveSheet.Cells[i - 1, j].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[i, j].Value);
                    if (!double.IsNaN(value) && !double.IsInfinity(value))
                    {
                        if (value == 0)
                        {
                            spdData.ActiveSheet.Cells[i, j].Text = "";
                        }
                        else
                        {
                            spdData.ActiveSheet.Cells[i - 2, j].CellType = ctDouble;
                            spdData.ActiveSheet.Cells[i - 2, j].Value = value;
                        }
                    }
                }
                i += nRowCountPerUnit;
            }
        }
        #endregion
    }
}