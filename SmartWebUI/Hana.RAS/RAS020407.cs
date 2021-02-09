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


namespace Hana.PRD
{
    public partial class RAS020407 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: RAS020407<br/>
        /// 클래스요약: 출하제품 이력br/>
        /// 작  성  자: 미라콤 김민규<br/>
        /// 최초작성일: 2008-12-05<br/>
        /// 상세  설명: 출하제품 이력<br/>
        /// 변경  내용: <br/>
        /// </summary>
        /// 
        static DataTable dt_loss_code = null;

        public RAS020407()
        {
            InitializeComponent();
            cdvFromTo.yesterday_flag = true;
            cdvFromTo.AutoBinding();
            SortInit();
            GridColumnInit();
        }


        #region 유효성 검사 및 초기화
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
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("date", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Work Week", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Product", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Lot ID", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Customer", 0, 4, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Family", 0, 5, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Package", 0, 6, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type1", 0, 7, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type2", 0, 8, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LD Count", 0, 9, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Density", 0, 10, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Generation", 0, 11, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);

            spdData.RPT_AddBasicColumn("Equipment name", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Team in charge", 0, 13, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("파트", 0, 14, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Operation", 0, 15, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Maker", 0, 16, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Model", 0, 17, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);

            spdData.RPT_AddBasicColumn("InPut", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("OutPut", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("Loss", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 50);
            spdData.RPT_AddBasicColumn("yield", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("working time", 0, 22, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("In Time", 0, 23, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Out Time", 0, 24, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);

            if (cdvFactory.Text != "")
            {
                dt_loss_code = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());

                if (dt_loss_code.Rows.Count != 0)
                {
                    for (int i = 0; i < dt_loss_code.Rows.Count; i++)
                    {
                        // 미리 구한 Loss 코드별로 칼럼 동적 생성 및 Select 절에 추가
                        spdData.RPT_AddBasicColumn(dt_loss_code.Rows[i]["CODE"].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                    }
                }
            }
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            string strDate = "TO_CHAR(TO_DATE(C.WORK_DATE, 'YYYY-MM-DD'), 'YYYY-MM-DD')AS DAY";
            string team = "MAX(NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = A.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = A.RES_GRP_1), '-')) AS TEAM";
            string part = "MAX(NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = A.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = A.RES_GRP_2), '-')) AS PART";

            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("date", strDate, "C.WORK_DATE", strDate, true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Work Week", "C.WORK_WEEK", "C.WORK_WEEK", "C.WORK_WEEK", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "A.MAT_ID", "A.MAT_ID", "A.MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lot ID", "A.LOT_ID", "A.LOT_ID", "A.LOT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "MAT_GRP_1", "MAT_GRP_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);

            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment name", "A.RES_ID", "A.RES_ID", "A.RES_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Team in charge", "A.RES_GRP_1", "A.RES_GRP_1", team, false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Part", "A.RES_GRP_2", "A.RES_GRP_2", part, false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation", "A.RES_GRP_3", "A.RES_GRP_3", "A.RES_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Maker", "A.RES_GRP_5", "A.RES_GRP_5", "A.RES_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "A.RES_GRP_6", "A.RES_GRP_6", "A.RES_GRP_6", false);
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

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string sFrom = string.Empty;
            string sTo = string.Empty;
            string bbbb = string.Empty;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            string strFromDate = cdvFromTo.ExactFromDate;
            string strToDate = cdvFromTo.ExactToDate;

            strSqlString.AppendFormat("  SELECT {0} " + "\n", QueryCond3);
            strSqlString.Append("       , A.OPER_IN_QTY_1, A.END_QTY_1, SUM(NVL(LOSS_QTY,0)) AS LOSS_QTY  " + "\n");
            strSqlString.Append("       , CONCAT(DECODE(OPER_IN_QTY_1, 0, 0, TRUNC((END_QTY_1/OPER_IN_QTY_1)*100,3)), '%') AS YIELD  " + "\n");
            strSqlString.Append("       , A.PROC_TIME, A.OPER_IN_TIME, A.END_TIME  " + "\n");

            for (int i = 0; i < dt_loss_code.Rows.Count; i++)
            {
                strSqlString.Append("     , SUM(DECODE(B.LOSS_CODE, '" + dt_loss_code.Rows[i]["CODE"].ToString() + "', B.LOSS_QTY, 0)) AS LOSS_" + i.ToString() + " \n");
            }

            strSqlString.Append("    FROM (SELECT A.LOT_ID, A.MAT_ID, C.RES_ID, A.FACTORY " + "\n");
            strSqlString.Append("               , B.MAT_GRP_1, B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_4, B.MAT_GRP_5, B.MAT_GRP_6, B.MAT_GRP_7, B.MAT_GRP_8 " + "\n");
            strSqlString.Append("               , C.RES_GRP_1, C.RES_GRP_2, C.RES_GRP_3, C.RES_GRP_4, C.RES_GRP_5, C.RES_GRP_6, C.RES_GRP_7, C.RES_GRP_8  " + "\n");
            strSqlString.Append("               , A.OPER_IN_QTY_1, A.END_QTY_1, A.PROC_TIME, A.OPER_IN_TIME, A.END_TIME  " + "\n");
            strSqlString.Append("            FROM (SELECT *    " + "\n");
            strSqlString.Append("                    FROM RSUMWIPLTH  " + "\n");
            strSqlString.Append("                    WHERE LOT_ID IN " + "\n");
            strSqlString.Append("                                 (SELECT LOT_ID" + "\n");
            strSqlString.Append("                                    FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("                                    WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                                        AND OLD_FACTORY {0} " + "\n", cdvFactory.SelectedValueToQueryString);
            strSqlString.Append("                                        AND HIST_DEL_FLAG=' ' " + "\n");
            strSqlString.Append("                                        AND TRAN_CODE='SHIP'" + "\n");
            strSqlString.Append("                                        AND FACTORY <> 'RETURN' " + "\n");
            
            string from = string.Empty;
            string to = string.Empty;

            from = cdvFromTo.getFromTranTime();
            to = cdvFromTo.getToTranTime();

            strSqlString.AppendFormat("                                        AND TRAN_TIME BETWEEN '{0}' AND '{1}' )" + "\n", from, to);


            strSqlString.Append("                 ) A  " + "\n");
            strSqlString.Append("                 , MWIPMATDEF B  " + "\n");
            strSqlString.Append("                 , MRASRESDEF C  " + "\n");
            strSqlString.Append("             WHERE 1 = 1  " + "\n");
            strSqlString.Append("               AND A.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("               AND A.FACTORY = B.FACTORY(+)  " + "\n");
            strSqlString.Append("               AND A.FACTORY = C.FACTORY(+)  " + "\n");
            strSqlString.Append("               AND A.MAT_ID = B.MAT_ID(+)  " + "\n");
            strSqlString.Append("               AND B.MAT_VER(+) = 1        " + "\n");
            strSqlString.Append("               AND A.END_RES_ID = C.RES_ID(+) " + "\n");
            strSqlString.Append("               AND C.RES_TYPE NOT IN ('DUMMY')" + "\n");

            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("               AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("               AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("               AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("               AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("               AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("               AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("               AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("               AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            // ---- RES 상세 조회             
            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("               AND C.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("               AND C.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("               AND C.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("               AND C.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("               AND C.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("               AND C.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("               AND C.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

            strSqlString.Append("         ) A " + "\n");
            strSqlString.Append("       , (   " + "\n");
            strSqlString.Append("         SELECT *    " + "\n");
            strSqlString.Append("           FROM RWIPLOTLSM    " + "\n");
            strSqlString.Append("          WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("            AND MAT_VER = 1    " + "\n");
            strSqlString.Append("         ) B           " + "\n");

            strSqlString.Append("       , (SELECT LOT_ID, " + "\n");
            strSqlString.Append("                 GET_WORK_DATE(TRAN_TIME,'D') AS WORK_DATE, " + "\n");
            strSqlString.Append("                 GET_WORK_DATE(TRAN_TIME,'M') AS WORK_MONTH, " + "\n");
            strSqlString.Append("                 GET_WORK_DATE(TRAN_TIME,'W') AS WORK_WEEK, " + "\n");
            strSqlString.Append("                 TRAN_TIME" + "\n");
            strSqlString.Append("            FROM RWIPLOTHIS" + "\n");
            strSqlString.Append("            WHERE 1=1" + "\n");
            strSqlString.Append("                AND OLD_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                AND HIST_DEL_FLAG=' ' " + "\n");
            strSqlString.Append("                AND TRAN_CODE='SHIP'" + "\n");
            strSqlString.Append("                AND FACTORY  <> 'RETURN' " + "\n");
            strSqlString.AppendFormat("                AND TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", from, to);

            strSqlString.Append("         ) C    " + "\n");
            strSqlString.Append("   WHERE 1 = 1    " + "\n");
            strSqlString.Append("     AND A.LOT_ID = B.LOT_ID(+)                             " + "\n");
            strSqlString.Append("     AND A.LOT_ID = C.LOT_ID(+)       " + "\n");
            strSqlString.Append("     AND A.RES_ID = B.RES_ID(+)  " + "\n");
            strSqlString.Append("     AND A.RES_ID <> ' '   " + "\n");
            strSqlString.Append("     AND B.TRAN_TIME(+) BETWEEN A.OPER_IN_TIME AND A.END_TIME  " + "\n");
            strSqlString.AppendFormat("GROUP BY {0}, A.OPER_IN_QTY_1, A.END_QTY_1 " + "\n", QueryCond2);
            strSqlString.Append("       , A.PROC_TIME, A.OPER_IN_TIME, A.END_TIME  " + "\n");
            strSqlString.AppendFormat("ORDER BY {0}, A.OPER_IN_QTY_1, A.END_QTY_1 " + "\n", QueryCond2);
            strSqlString.Append("       , A.PROC_TIME, A.OPER_IN_TIME, A.END_TIME  " + "\n");

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

            string strFromDate = cdvFromTo.ExactFromDate;
            string strToDate = cdvFromTo.ExactToDate;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;

            strSqlString.Append("SELECT DISTINCT(A.LOSS_CODE) CODE " + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("       SELECT *    " + "\n");
            strSqlString.Append("         FROM MWIPLOTLSM  " + "\n");
            strSqlString.Append("        WHERE LOT_ID IN (SELECT LOT_ID           " + "\n");
            strSqlString.Append(                           "FROM RWIPLOTHIS " + "\n");
            strSqlString.Append("                          WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                            AND OLD_FACTORY {0} " + "\n", cdvFactory.SelectedValueToQueryString);
            strSqlString.Append("                            AND HIST_DEL_FLAG=' ' " + "\n");
            strSqlString.Append("                            AND TRAN_CODE='SHIP'" + "\n");
            strSqlString.Append("                            AND FACTORY <> 'RETURN' " + "\n");

            string from = string.Empty;
            string to = string.Empty;

            from = cdvFromTo.getFromTranTime();
            to = cdvFromTo.getToTranTime();

            strSqlString.AppendFormat("                            AND TRAN_TIME BETWEEN '{0}' AND '{1}' )" + "\n", from, to);

            strSqlString.Append("       ) A  " + "\n");
            strSqlString.Append("     , MWIPMATDEF B  " + "\n");
            strSqlString.Append("     , MRASRESDEF C  " + "\n");
            strSqlString.Append(" WHERE 1 = 1  " + "\n");
            strSqlString.AppendFormat(" AND A.FACTORY {0} " + "\n", cdvFactory.SelectedValueToQueryString);
            strSqlString.Append("   AND A.FACTORY = B.FACTORY(+)  " + "\n");
            strSqlString.Append("   AND A.FACTORY = C.FACTORY(+)  " + "\n");
            strSqlString.Append("   AND A.MAT_ID = B.MAT_ID(+)  " + "\n");
            strSqlString.Append("   AND B.MAT_VER(+) = 1        " + "\n");
            strSqlString.Append("   AND A.RES_ID = C.RES_ID(+) " + "\n");
            strSqlString.Append("   AND C.RES_TYPE NOT IN ('DUMMY')" + "\n");

            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("               AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("               AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("               AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("               AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("               AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("               AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("               AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("               AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

             //---- RES 상세 조회             
            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("               AND C.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("               AND C.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("               AND C.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("               AND C.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("               AND C.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("               AND C.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("               AND C.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

            strSqlString.Append("ORDER BY A.LOSS_CODE" + "\n");

            //System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            return strSqlString.ToString();
        }

        #endregion


        #region EVENT 처리
        private void btnView_Click(object sender, EventArgs e)
        {
            //DurationTime_Check();

            DataTable dt = null;
            if (CheckField() == false) return;

            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                this.Refresh();

                GridColumnInit();
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //spdData.DataSource = dt;

                //by John
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 17, null, null, btnSort);
                //토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 11;

                //by John (두줄짜리이상 + 부분합계.)
                //1.Griid 합계 표시
                //int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 1, 9, 10, 4, udcCUSFromToCondition1.CountFromToValue, btnSort);
                //int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 1, 9, 10, 4, udcCUSCondition1.SelectCount, btnSort);
                //// 서브토탈시작할 컬럼, 서브토탈 몇개쓸껀지, 몇 컬럼까지 서브토탈 ~ 까지 안함, 자동채움시작컬럼 전컬럼, 디바이드 로우수                                                                                        

                //구분항목 값 생성(구분이 들어가는 위치임. 0부터 시작)                

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 17, 0, 1, true, Align.Center, VerticalAlign.Center);

                //spdData.RPT_FillColumnData(9, new string[] {"IN", "OUT", "EOH", "BOH" });

                //4. Column Auto Fit
                //spdData.RPT_AutoFit(true);

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
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null);
            spdData.ExportExcel();            
        }
        #endregion
    }
}
