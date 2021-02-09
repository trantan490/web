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
    /// 클  래  스: RAS020404<br/>
    /// 클래스요약: Trend Top 10<br/>
    /// 작  성  자: 미라콤 양형석<br/>
    /// 최초작성일: 2008-12-17<br/>
    /// 상세  설명: Trend Top 10<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class RAS020404 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public RAS020404()
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
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.ActiveSheet.RowHeader.ColumnCount = 0;
            spdData.RPT_ColumnInit();


            spdData.RPT_AddBasicColumn("Team in charge", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("파트", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("EQP Type", 0, 2, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Maker", 0, 3, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Model", 0, 4, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Equipment name", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);

            spdData.RPT_AddBasicColumn("Customer", 0, 6, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Family", 0, 7, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Package", 0, 8, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type1", 0, 9, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type2", 0, 10, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LD Count", 0, 11, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Density", 0, 12, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Generation", 0, 13, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Loss Code", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LOss Desc", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Classification", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);

            spdData.RPT_AddDynamicColumn(udcFromToDate, 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);


            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Team in charge", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_1), '-') AS TEAM", "TEAM", "RES.RES_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Part", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_2), '-') AS PART", "PART", "RES.RES_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("EQP Type", "RES.RES_GRP_3 AS EQP_TYPE", "EQP_TYPE", "RES_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Maker", "RES.RES_GRP_5 AS MAKER", "MAKER", "RES.RES_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "RES.RES_GRP_6 AS MODEL", "MODEL", "RES.RES_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment name", "RES.RES_ID AS RES_ID", "RES_ID", "RES.RES_ID", false);

            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_CUSTOMER' AND ROWNUM=1 AND KEY_1 = MAT.MAT_GRP_1), '-') AS CUSTOMER", "CUSTOMER", "MAT.MAT_GRP_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2 as Family", "Family", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3 as Package", "Package", "MAT.MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4 as Type1", "Type1", "MAT.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5 as Type2", "Type2", "MAT.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6 as LD_Count", "LD_Count", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7 as Density", "Density", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8 as Generation", "Generation", "MAT.MAT_GRP_8", false);
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

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            ////////////////////////////////////////////////////
            // udcDurationPlus에서 정확한 조회시간을 받아오기
            string strFromDate = udcFromToDate.ExactFromDate;
            string strToDate = udcFromToDate.ExactToDate;
            ////////////////////////////////////////////////////

            string strYield = string.Empty;
            string strIn = string.Empty;
            string strOut = string.Empty;
            string strLoss = string.Empty;
            for (int i = 0; i < udcFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                //strYield += "     , DECODE(OUT_" + i.ToString() + ", 0, 0, TRUNC(OUT_" + i.ToString() + "/IN_" + i.ToString() + "*100,3)) YIELD_" + i.ToString() + "\n";
                strYield += "     , 0 YIELD_" + i.ToString() + "\n";
                strIn += "     , IN_" + i.ToString() + "\n";
                strOut += "     , OUT_" + i.ToString() + "\n";
                strLoss += "     , LOSS_" + i.ToString() + "\n";
            }   

            string strDecode = string.Empty;
            strDecode += udcFromToDate.getDecodeQuery("SUM(DECODE(CUTOFF_DT", "IN_QTY, 0))", "IN_").Replace(", SUM(", "              , SUM(");
            strDecode += udcFromToDate.getDecodeQuery("SUM(DECODE(CUTOFF_DT", "OUT_QTY, 0))", "OUT_").Replace(", SUM(", "              , SUM(");
            strDecode += udcFromToDate.getDecodeQuery("SUM(DECODE(CUTOFF_DT", "LOSS_QTY, 0))", "LOSS_").Replace(", SUM(", "              , SUM(");


            strSqlString.Append("SELECT " + QueryCond2 + "\n");
            strSqlString.Append("     , LOSS_CODE " + "\n");
            strSqlString.Append("     , LOSS_DESC " + "\n");
            strSqlString.Append("     , ' ' GUBUN " + "\n");
            strSqlString.Append(strYield + strIn + strOut + strLoss);
            //strSqlString.Append(strYield + strLoss);
            strSqlString.Append("  FROM (  " + "\n");
            strSqlString.Append("         SELECT " + QueryCond1 + "\n");
            strSqlString.Append("                 , LOT.CODE LOSS_CODE " + "\n");
            strSqlString.Append("                 , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'LOSS_CODE' AND KEY_1 = LOT.CODE AND ROWNUM = 1), '-') LOSS_DESC " + "\n");
            strSqlString.Append(strDecode);
            strSqlString.Append("           FROM ( " + "\n");
            switch(udcFromToDate.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    strSqlString.Append("                 SELECT SUBSTR(OUT_TIME, 1, 8) CUTOFF_DT " + "\n");
                    break;
                case "WEEK":
                    strSqlString.Append("                 SELECT GET_WORK_DATE(OUT_TIME, 'W') CUTOFF_DT " + "\n");
                    break;
                case "MONTH":
                    strSqlString.Append("                 SELECT SUBSTR(OUT_TIME, 1, 6) CUTOFF_DT " + "\n");
                    break;
                default :
                    strSqlString.Append("                 SELECT SUBSTR(OUT_TIME, 1, 8) CUTOFF_DT " + "\n");
                    break;
            }
            strSqlString.Append("                      , PREV.FACTORY, PREV.RES_ID, PREV.MAT_ID, PREV.LOT_ID  " + "\n");
            strSqlString.Append("                      , PREV.RES_HIST_SEQ PREV_SEQ, POST.RES_HIST_SEQ POST_SEQ " + "\n");
            strSqlString.Append("                      , LOSS.CODE " + "\n");
            strSqlString.Append("                      , IN_QTY, (IN_QTY-LOSS.QTY ) OUT_QTY " + "\n");
            strSqlString.Append("                      , LOSS.QTY LOSS_QTY " + "\n");
            strSqlString.Append("                      , IN_TIME, OUT_TIME " + "\n");
            strSqlString.Append("                      , ROW_NUMBER() OVER(ORDER BY LOSS.QTY DESC) LOSS_QTY_RANK " + "\n");

            strSqlString.Append("                   FROM ( " + "\n");
            strSqlString.Append("                          SELECT GET_WORK_DATE(TRAN_TIME, 'D') TRAN_TIME, FACTORY, RES_ID, MAT_ID, LOT_ID, RES_HIST_SEQ, OPER, QTY_1 IN_QTY, TRAN_TIME IN_TIME " + "\n");
            strSqlString.Append("                            FROM MRASRESLTH " + "\n");
            strSqlString.Append("                           WHERE 1=1 " + "\n");
            strSqlString.Append("                             AND EVENT_ID = 'START_LOT' " + "\n");
            strSqlString.Append("                             AND LOT_HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                             AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "' " + "\n");
            strSqlString.Append("                        ) PREV " + "\n");
            strSqlString.Append("                      , ( " + "\n");
            strSqlString.Append("                          SELECT FACTORY, RES_ID, MAT_ID, LOT_ID, RES_HIST_SEQ, OPER, QTY_1 OUT_QTY, TRAN_TIME OUT_TIME " + "\n");
            strSqlString.Append("                            FROM MRASRESLTH " + "\n");
            strSqlString.Append("                           WHERE 1=1 " + "\n");
            strSqlString.Append("                             AND EVENT_ID = 'END_LOT' " + "\n");
            strSqlString.Append("                             AND LOT_HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                             AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "' " + "\n");
            strSqlString.Append("                        ) POST " + "\n");
            strSqlString.Append("                      , (  " + "\n");
            strSqlString.Append("                          SELECT LOSS_CODE CODE, LOSS_QTY QTY, FACTORY, RES_ID, MAT_ID, LOT_ID, TRAN_TIME, HIST_SEQ  " + "\n");
            strSqlString.Append("                            FROM RWIPLOTLSM      " + "\n");
            strSqlString.Append("                           WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                             AND MAT_VER = 1    " + "\n");
            strSqlString.Append("                             AND HIST_DEL_FLAG = ' '  " + "\n");
            strSqlString.Append("                        ) LOSS " + "\n");

            strSqlString.Append("                  WHERE 1=1 " + "\n");
            strSqlString.Append("                    AND PREV.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                    AND PREV.FACTORY = POST.FACTORY  " + "\n");
            strSqlString.Append("                    AND PREV.RES_ID = POST.RES_ID  " + "\n");
            strSqlString.Append("                    AND PREV.MAT_ID = POST.MAT_ID  " + "\n");
            strSqlString.Append("                    AND PREV.LOT_ID = POST.LOT_ID  " + "\n");
            strSqlString.Append("                    AND PREV.RES_HIST_SEQ = (POST.RES_HIST_SEQ - 1)  " + "\n");
            strSqlString.Append("                    AND PREV.IN_TIME <> POST.OUT_TIME  " + "\n");
            strSqlString.Append("                    AND PREV.FACTORY = LOSS.FACTORY  " + "\n");
            strSqlString.Append("                    AND PREV.LOT_ID = LOSS.LOT_ID  " + "\n");
            strSqlString.Append("                    AND PREV.MAT_ID = LOSS.MAT_ID  " + "\n");
            strSqlString.Append("                    AND PREV.RES_ID = LOSS.RES_ID  " + "\n");
            strSqlString.Append("                    AND LOSS.TRAN_TIME BETWEEN PREV.IN_TIME AND POST.OUT_TIME " + "\n");
            strSqlString.Append("                    AND PREV.IN_QTY <> 0 AND POST.OUT_QTY <> 0 " + "\n");
            strSqlString.Append("             ) LOT " + "\n");
            strSqlString.Append("           , MRASRESDEF RES  " + "\n");
            strSqlString.Append("           , MWIPMATDEF MAT " + "\n");
            strSqlString.Append("       WHERE 1=1 " + "\n");
            strSqlString.Append("         AND LOT.FACTORY = RES.FACTORY  " + "\n");
            strSqlString.Append("         AND LOT.RES_ID = RES.RES_ID  " + "\n");
            strSqlString.Append("         AND LOT.FACTORY = MAT.FACTORY  " + "\n");
            strSqlString.Append("         AND LOT.MAT_ID = MAT.MAT_ID  " + "\n");
            strSqlString.Append("         AND LOT.LOSS_QTY_RANK < 11  " + "\n");

            #region " WIP 상세 조회 "
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("         AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("         AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("         AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("         AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("         AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("         AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("         AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("         AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            #endregion

            #region " RAS 상세 조회 "
            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("         AND RES.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("         AND RES.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("         AND RES.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("         AND RES.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("         AND RES.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("         AND RES.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("         AND RES.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

            strSqlString.Append("         AND RES.RES_TYPE NOT IN ('DUMMY')" + "\n");
            #endregion

            strSqlString.Append("       GROUP BY RES.FACTORY, " + QueryCond3 + ", LOT.CODE, LOT.LOSS_QTY_RANK " + "\n");
            strSqlString.Append("       ORDER BY LOT.LOSS_QTY_RANK, RES.FACTORY, " + QueryCond3 + ", LOT.CODE " + "\n");
            strSqlString.Append("  ) " + "\n");

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

        private void ShowChart(int rowCount)
        {
            // 차트 설정
            udcChartFX1.RPT_1_ChartInit();
            udcChartFX1.RPT_2_ClearData();

            if (((DataTable)spdData.DataSource).Rows.Count < 4)
                return;

            udcChartFX1.RPT_3_OpenData(2, udcFromToDate.SubtractBetweenFromToDate + 1);
            int[] yield_columns = new Int32[udcFromToDate.SubtractBetweenFromToDate + 1];
            int[] loss_columns = new Int32[udcFromToDate.SubtractBetweenFromToDate + 1];
            int[] columnsHeader = new Int32[udcFromToDate.SubtractBetweenFromToDate + 1];

            for (int i = 0; i < yield_columns.Length; i++)
            {
                columnsHeader[i] = 17 + i;
                yield_columns[i] = 17 + i;
                loss_columns[i] = 17 + i;
            }

            //YIELD
            double yield = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 0 }, yield_columns, SeriseType.Rows);

            //LOSS
            double loss = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 3 }, yield_columns, SeriseType.Rows);

            udcChartFX1.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            udcChartFX1.RPT_6_SetGallery(ChartType.Line, 0, 1, "YIELD [단위 : %]", AsixType.Y, DataTypes.Initeger, yield * 1.1);
            udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 1, 1, "LOSS [단위 : Qty]", AsixType.Y2, DataTypes.Initeger, loss * 1.3);

            //각 Serise별로 동일한 타입을 사용할 경우
            //udcChartFX1.RPT_6_SetGallery(ChartType.Bar, "[단위 : sls]", AsixType.Y, DataTypes.Initeger, yield * 1.2);

            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "YIELD", "LOSS" }, SoftwareFX.ChartFX.Docked.Top);
            
            // 기타 설정
            udcChartFX1.PointLabels = true;
            udcChartFX1.Series[0].PointLabelColor = Color.Blue;
            udcChartFX1.Series[1].PointLabelColor = Color.Green;
            udcChartFX1.Series[0].LineWidth = 2;
            udcChartFX1.RightGap = 10;
            udcChartFX1.AxisY.Max = 100.02;
            udcChartFX1.AxisY.Min = 99.95;
            udcChartFX1.AxisY.Step = 0.01;
            udcChartFX1.AxisY.LabelsFormat.Decimals = 3;
            udcChartFX1.AxisY2.DataFormat.Format = SoftwareFX.ChartFX.AxisFormat.Number;
            udcChartFX1.AxisY2.DataFormat.Decimals = 0;
            udcChartFX1.RecalcScale();
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
            if((dt.Rows.Count-nStartRow)%nRowGroupUnitCount > 0)
                return;
            if(nRowGroupUnitCount<1 || nStartColumn <1 || nStep<1)
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
            for(int i=nLegendColumn; i>=0; i--)
            {
                if(spdData.ActiveSheet.Columns[i].Visible == true)
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
            for(int i=nStartRow+nTargetRowPos; i<dt.Rows.Count; i+=nRowGroupUnitCount)
            {
                value_columns = new Int32[nStep];
                for (int j = 0; j < value_columns.Length; j++)
                {
                    value_columns[j] = nStartColumn + j;
                }                

                temp = udcChartFX1.RPT_4_AddData(spdData, new int[] {i}, value_columns, SeriseType.Rows);
                max = (max < temp) ? temp : max;

                arrStrLegend[nRowCount] = dt.Rows[i][nLegendColumn].ToString();
                nRowCount++; 
            }
            udcChartFX1.RPT_5_CloseData();

            //각 Serise별로 동일한 타입을 사용할 경우
            udcChartFX1.RPT_6_SetGallery(ChartType.Line, "YIELD [단위 : %]", AsixType.Y, DataTypes.Initeger, max * 1.2);

            // x 축 텍스트 입력
            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);

            // 시리즈 범례
            udcChartFX1.RPT_8_SetSeriseLegend(arrStrLegend, SoftwareFX.ChartFX.Docked.Top);

            // 기타 설정
            udcChartFX1.PointLabels = true;
            udcChartFX1.Series[0].PointLabelColor = Color.Blue;
            udcChartFX1.Series[1].PointLabelColor = Color.Green;
            udcChartFX1.Series[0].LineWidth = 2;
            udcChartFX1.RightGap = 10;
            udcChartFX1.AxisY.Max = 100.02;
            udcChartFX1.AxisY.Min = 99.95;
            udcChartFX1.AxisY.Step = 0.01;
            udcChartFX1.AxisY.LabelsFormat.Decimals = 3;
            udcChartFX1.AxisY2.DataFormat.Format = SoftwareFX.ChartFX.AxisFormat.Number;
            udcChartFX1.AxisY2.DataFormat.Decimals = 0;
            udcChartFX1.RecalcScale();
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
                int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 1, 17, 17, 4, udcFromToDate.SubtractBetweenFromToDate+1, btnSort);
                //spdData.DataSource = dt;

                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 16, 0, 4, true, Align.Center, VerticalAlign.Center);


                //4. Column Auto Fit4
                spdData.RPT_AutoFit(false);

                spdData.RPT_FillColumnData(16, new string[] { "Yield", "In", "Out", "Loss" });

                // Yield 셀 타입 변경
                SetSpreadExpression();

                if (chkGroup.Checked)
                    ShowChartByGroup(4, 0, 0, 17, 14, udcFromToDate.SubtractBetweenFromToDate + 1);
                else
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
            int nStartValueColumn = 17;     // 값들이 시작되는 열 위치
            int nStartRow = 0;              // 값들이 시작되는 행 위치
            int nRowCountPerUnit = 4;       // 피봇으로 묶였을 때 하나의 그룹당 행 개수, 피봇이 아니면 1
            int nRowOffset = 0;             // 해당 그룹에서의 행 위치

            Double value = 0;

            for (int i = nStartRow + nRowOffset; i < spdData.ActiveSheet.Rows.Count; )
            {
                for (int j = nStartValueColumn; j < spdData.ActiveSheet.Columns.Count; j++)
                {
                    // Cell Type
                    FarPoint.Win.Spread.CellType.NumberCellType ct = new FarPoint.Win.Spread.CellType.NumberCellType();
                    ct.DecimalPlaces = 3;
                    
                    // 수식
                    value = (Convert.ToDouble(spdData.ActiveSheet.Cells[i + 1, j].Value) - Convert.ToDouble(spdData.ActiveSheet.Cells[i + 3, j].Value)) / Convert.ToDouble(spdData.ActiveSheet.Cells[i + 1, j].Value);
                    if (!double.IsNaN(value) && !double.IsInfinity(value))
                    {
                        if(value == 0)
                        {
                            spdData.ActiveSheet.Cells[i, j].Text = "";
                        }
                        else
                        {
                            spdData.ActiveSheet.Cells[i, j].CellType = ct;
                            spdData.ActiveSheet.Cells[i, j].Value = value * 100;
                        }
                    }
                }
                i += nRowCountPerUnit;
            }
        }
        #endregion
    }
}