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
    /// 클  래  스: RAS020405<br/>
    /// 클래스요약: YIELD 불량 코드별<br/>
    /// 작  성  자: 미라콤 김민규<br/>
    /// 최초작성일: 2008-12-17<br/>
    /// 상세  설명: YIELD 불량 코드별<br/>
    /// 변경  내용: <br/>
    /// </summary>
    /// 
    public partial class RAS020405 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        DataTable global_loss_code = null;

        public RAS020405()
        {
            InitializeComponent();
            cdvFromTo.AutoBinding();
            cdvFromTo.DaySelector.SelectedValue = "MONTH";
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
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("Team in charge", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Part", 0, 1, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Operation", 0, 2, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Maker", 0, 3, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Model", 0, 4, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Equipment", 0, 5, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);            

            spdData.RPT_AddBasicColumn("Customer", 0, 6, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Family", 0, 7, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Package", 0, 8, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type1", 0, 9, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type2", 0, 10, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LD Count", 0, 11, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Density", 0, 12, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Generation", 0, 13, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);

            if (udcRASCondition3.Text != "")
            {
                spdData.RPT_AddBasicColumn(udcRASCondition3.Text, 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
            }
            else
            {
                spdData.RPT_AddBasicColumn("Code", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
            }


            if (cdvFactory.txtValue != "")
            {
                global_loss_code = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());

                for (int i = 0; i < global_loss_code.Rows.Count; i++)
                {
                    spdData.RPT_AddBasicColumn(global_loss_code.Rows[i][0].ToString(), 0, (15 + i), Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                }
            }

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Team in charge", "RES_GRP_1", "B.RES_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = A.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES_GRP_1), '-') AS TEAM", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Part", "RES_GRP_2", "B.RES_GRP_2", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = A.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES_GRP_2), '-') AS PART", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation", "RES_GRP_3", "B.RES_GRP_3", "RES_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Maker", "RES_GRP_5", "B.RES_GRP_5", "RES_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "RES_GRP_6", "B.RES_GRP_6", "RES_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment", "RES_ID", "B.RES_ID", "RES_ID", false);            

            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "B.MAT_GRP_1", "MAT_GRP_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "B.MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "B.MAT_GRP_3", "MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "B.MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "B.MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "B.MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "B.MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "B.MAT_GRP_8", "MAT_GRP_8", false);
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

            string strFromDate = cdvFromTo.FromDate.Value.ToString("yyyyMMdd");
            string strToDate = cdvFromTo.ToDate.Value.ToString("yyyyMMdd");

            strSqlString.AppendFormat("   SELECT {0}  " + "\n", QueryCond3);
            strSqlString.Append("        , ' ' as n " + "\n");
            //for (int i = 0; i < global_loss_code.Rows.Count; i++)
            //{
            //    strSqlString.Append("        , MAX(CODE" + i + ")\n");
            //}
            for (int i = 0; i < global_loss_code.Rows.Count; i++)
            {
                strSqlString.Append("        , MAX(NAME" + i + ")\n");
            }
            for (int i = 0; i < global_loss_code.Rows.Count; i++)
            {
                strSqlString.Append("        , SUM(YIELD" + i + ")\n");
            }
            for (int i = 0; i < global_loss_code.Rows.Count; i++)
            {
                strSqlString.Append("        , SUM(QTY" + i + ")   " + "\n");
            }
            strSqlString.Append("     FROM ( " + "\n");


            strSqlString.AppendFormat("   SELECT {0}  " + "\n", QueryCond2);
            strSqlString.Append("        , ' ' as n, A.FACTORY " + "\n");

            for (int i = 0; i < global_loss_code.Rows.Count; i++)
            {
                strSqlString.Append("        , MAX(A.CODE" + i + ") CODE" + i + "\n");
            }

            for (int i = 0; i < global_loss_code.Rows.Count; i++)
            {
                strSqlString.Append("        , MAX(A.NAME" + i + ") NAME" + i + "\n");
            }

            //            strSqlString.Append("        , B.MAT_ID, B.LOT_ID, B.RES_ID         " + "\n");

            for (int i = 0; i < global_loss_code.Rows.Count; i++)
            {
                strSqlString.Append("        , CASE WHEN C.LOSS_CODE = '" + global_loss_code.Rows[i][0].ToString() + "' " + "\n");
                strSqlString.Append("                   THEN TRUNC(((SUM(B.END_QTY_1)-SUM(C.LOSS_QTY))/SUM(B.END_QTY_1))*100, 3)" + "\n");
                strSqlString.Append("                   ELSE 0 END YIELD" + i + "\n");
            }

            for (int i = 0; i < global_loss_code.Rows.Count; i++)
            {
                strSqlString.Append("        , SUM(B.END_QTY_1) QTY" + i + "\n");
            }

            strSqlString.Append("     FROM (SELECT FACTORY   " + "\n");

            for (int i = 0; i < global_loss_code.Rows.Count; i++)
            {
                strSqlString.Append("                , MAX(DECODE(KEY_1, '" + global_loss_code.Rows[i][0].ToString() + "', KEY_1, ' '))CODE" + i + "\n");
                strSqlString.Append("                , MAX(DECODE(KEY_1, '" + global_loss_code.Rows[i][0].ToString() + "', DATA_1, ' '))NAME" + i + "\n");
            }

            strSqlString.Append("             FROM (SELECT FACTORY, KEY_1, DATA_1    " + "\n");
            strSqlString.Append("                     FROM MGCMTBLDAT    " + "\n");
            strSqlString.Append("                    WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                      AND TABLE_NAME = 'LOSS_CODE'   " + "\n");
            strSqlString.Append("                  )    " + "\n");
            strSqlString.Append("         GROUP BY FACTORY   " + "\n");
            strSqlString.Append("          ) A   " + "\n");
            strSqlString.Append("        , (SELECT A.FACTORY, A.MAT_ID, A.LOT_ID    " + "\n");
            strSqlString.Append("                , C.RES_ID, B.MAT_GRP_1, B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_4, B.MAT_GRP_5, B.MAT_GRP_6, B.MAT_GRP_7, B.MAT_GRP_8   " + "\n");
            strSqlString.Append("                , C.RES_GRP_1, C.RES_GRP_2, C.RES_GRP_3, C.RES_GRP_4, C.RES_GRP_5, C.RES_GRP_6, C.RES_GRP_7, C.RES_GRP_8   " + "\n");
            strSqlString.Append("                , A.END_QTY_1   " + "\n");
            strSqlString.Append("             FROM (SELECT *     " + "\n");
            strSqlString.Append("                     FROM RSUMWIPLTH     " + "\n");
            strSqlString.Append("                  ) A   " + "\n");
            strSqlString.Append("                  , MWIPMATDEF B   " + "\n");
            strSqlString.Append("                  , MRASRESDEF C   " + "\n");
            strSqlString.Append("              WHERE 1 = 1   " + "\n");
            strSqlString.Append("                AND A.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                AND A.FACTORY = B.FACTORY(+)   " + "\n");
            strSqlString.Append("                AND A.FACTORY = C.FACTORY(+)   " + "\n");
            strSqlString.Append("                AND A.MAT_ID = B.MAT_ID(+)   " + "\n");
            strSqlString.Append("                AND B.MAT_VER(+) = 1         " + "\n");
            strSqlString.Append("                AND A.END_RES_ID = C.RES_ID(+)   " + "\n");
            strSqlString.Append("                AND A.END_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");

            // ---- WIP 상세 조회 ---- //
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            // ---- RES 상세 조회 ---- //             
            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("                AND C.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("                AND C.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("                AND C.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("                AND C.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("                AND C.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("                AND C.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("                AND C.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

            strSqlString.Append("          ) B   " + "\n");
            strSqlString.Append("        , (SELECT *   " + "\n");
            strSqlString.Append("             FROM RWIPLOTLSM   " + "\n");
            strSqlString.Append("            WHERE HIST_DEL_FLAG = ' '   " + "\n");
            strSqlString.Append("              AND MAT_VER = 1   " + "\n");
            strSqlString.Append("          ) C               " + "\n");
            strSqlString.Append("    WHERE 1 = 1   " + "\n");
            strSqlString.Append("      AND B.FACTORY = A.FACTORY(+)   " + "\n");
            strSqlString.Append("      AND B.LOT_ID = C.LOT_ID(+)   " + "\n");
            strSqlString.Append("      AND B.RES_ID = C.RES_ID(+)   " + "\n");
            strSqlString.Append("      AND B.RES_ID IS NOT NULL " + "\n");

            strSqlString.AppendFormat("GROUP BY {0}, C.LOSS_CODE, A.FACTORY  " + "\n", QueryCond2);
            strSqlString.Append( ") A " + "\n");
            strSqlString.AppendFormat(" GROUP BY {0}, A.FACTORY " + "\n", QueryCond1 );

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private string MakeSqlString1()             // 선택 공정에 존재하는 LOSS_CODE를 가져옴 ( 컬럼헤더 생성 )
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string sFrom = string.Empty;
            string sTo = string.Empty;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;


            strSqlString.Append("        SELECT DISTINCT LOSS_CODE  " + "\n");
            strSqlString.Append("          FROM RWIPLOTLSM A  " + "\n");
            strSqlString.Append("             , MRASRESDEF B  " + "\n");
            strSqlString.Append("         WHERE A.FACTORY  " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("           AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.RES_ID = B.RES_ID " + "\n");
            strSqlString.Append("           AND B.RES_TYPE NOT IN ('DUMMY')" + "\n");
            
            // ---- RES 상세 조회 ---- //             
            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("           AND B.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("           AND B.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("           AND B.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("           AND B.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("           AND B.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("           AND B.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("           AND B.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

            strSqlString.Append("           AND HIST_DEL_FLAG=' '  " + "\n");

            //기간 선택시 SQL 조건문 생성
            if (cdvFromTo.DaySelector.SelectedValue.ToString() == "DAY")
            {
                sFrom = cdvFromTo.FromDate.Text.Replace("-", "");
                sTo = cdvFromTo.ToDate.Text.Replace("-", "");
                strSqlString.AppendFormat("           AND GET_WORK_DATE(TRAN_TIME, 'D') BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);

            }
            else if (cdvFromTo.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                sFrom = cdvFromTo.FromWeek.SelectedValue.ToString();
                sTo = cdvFromTo.ToWeek.SelectedValue.ToString();
                strSqlString.AppendFormat("           AND GET_WORK_DATE(TRAN_TIME, 'W') BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
            }
            else
            {
                sFrom = cdvFromTo.FromYearMonth.Value.ToString("yyyyMM");
                sTo = cdvFromTo.ToYearMonth.Value.ToString("yyyyMM");
                strSqlString.AppendFormat("           AND GET_WORK_DATE(TRAN_TIME, 'M') BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
            }
            strSqlString.Append("      ORDER BY LOSS_CODE  " + "\n");

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

            if (((DataTable)spdData.DataSource).Rows.Count == 0)
                return;

            udcChartFX1.RPT_3_OpenData(2, global_loss_code.Rows.Count);
            int[] yield_columns = new Int32[global_loss_code.Rows.Count];
            int[] loss_columns = new Int32[global_loss_code.Rows.Count];
            int[] columnsHeader = new Int32[global_loss_code.Rows.Count];

            for (int i = 0; i < yield_columns.Length; i++)
            {
                columnsHeader[i] = 15 + i;
                yield_columns[i] = 15 + i;
                loss_columns[i] = 15 + i;
            }

            //YIELD
            double yield = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 1 }, yield_columns, SeriseType.Rows);

            //LOSS
            double loss = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 2 }, yield_columns, SeriseType.Rows);

            udcChartFX1.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            udcChartFX1.RPT_6_SetGallery(ChartType.Line, 0, 1, "YIELD [단위 : %]", AsixType.Y, DataTypes.Initeger, yield * 1.1);
            udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 1, 1, "생산량 [단위 : Qty]", AsixType.Y2, DataTypes.Initeger, loss * 1.3);

            //각 Serise별로 동일한 타입을 사용할 경우
            //udcChartFX1.RPT_6_SetGallery(ChartType.Bar, "[단위 : sls]", AsixType.Y, DataTypes.Initeger, yield * 1.2);

            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "YIELD", "생산량" }, SoftwareFX.ChartFX.Docked.Top);

            // 기타 설정
            udcChartFX1.PointLabels = true;
            udcChartFX1.Series[0].PointLabelColor = Color.Blue;
            udcChartFX1.Series[1].PointLabelColor = Color.Green;
            udcChartFX1.Series[0].LineWidth = 2;
            udcChartFX1.RightGap = 10;
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
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 10, null, null, btnSort);
                //토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용


                //int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 0, 0, 17, 4, global_loss_code.Rows.Count, btnSort);

                spdData.DataSource = dt;
                spdData.RPT_DivideRows(15, 3, global_loss_code.Rows.Count);



                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                //3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 1, true, Align.Center, VerticalAlign.Center);


                //4. Column Auto Fit4
                spdData.RPT_AutoFit(false);

                spdData.RPT_FillColumnData(14, new string[] { "Defect name", "Yield", "output" });

                //if (chkGroup.Checked)
                //    ShowChartByGroup(4, 0, 0, 15, 13, udcFromToDate.SubtractBetweenFromToDate + 1);
                //else
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
            cdvProduct.sFactory = cdvFactory.txtValue;
        }
        #endregion
    }
}