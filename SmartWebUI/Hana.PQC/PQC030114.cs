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
    /// 클  래  스: PQC030114<br/>
    /// 클래스요약: <br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2010-06-04<br/>
    /// 상세  설명: 기존 부적합현황 대신 신규로 화면 개발<br/>
    /// 변경  내용: <br/>
    /// </summary>
    /// 

    public partial class PQC030114 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public PQC030114()
        {
            InitializeComponent();
            SortInit();
            GridColumnInit();
            cdvFromTo.AutoBinding(); 
            //this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            //cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;


            udcChartFX1.RPT_1_ChartInit();  //차트 초기화. 

            //cboGraph.SelectedIndexChanged -= cboGraph_SelectedIndexChanged;
            //cboGraph.SelectedIndex = 0;
            //cboGraph.SelectedIndexChanged += cboGraph_SelectedIndexChanged;
        }

        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.Trim().Length == 0)
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

            try
            {
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("Operation", 0, 10, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot No.", 0, 11, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Seq", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Lot Qty", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Inspection quantity", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Defect quantity", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Defective rate" + "\n" + "(ppm)", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Defect name", 0, 17, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Equipment number", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Judgment result", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Purpose of inspection", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Inspector", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Inspection time", 0, 22, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Shift", 0, 23, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("COMMENT", 0, 24, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);

                // Group항목이 있을경우 반드시 선언해줄것.
                spdData.RPT_ColumnConfigFromTable(btnSort);
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT.MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2 AS FAMILY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3 AS PACKAGE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4 AS TYPE1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5 AS TYPE2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6 AS \"LD COUNT\"", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7 AS DENSITY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8 AS GENERATION", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10 AS PIN_TYPE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT.MAT_ID", "MAT.MAT_ID AS PRODUCT", true);
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        #region Spread 생성을 위한 쿼리
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();
            
            string strFromDate = cdvFromTo.ExactFromDate;
            string strToDate = cdvFromTo.ExactToDate;
            
            string QueryCond1 = null;
            string QueryCond2 = null;
            
            //추가
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            
            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond2);
            strSqlString.Append("     , DAT.QC_OPER " + "\n");            
            strSqlString.Append("     , DAT.LOT_ID " + "\n");
            strSqlString.Append("     , DAT.QC_HIST_SEQ " + "\n");
            strSqlString.Append("     , HIS.QTY_1 " + "\n");
            strSqlString.Append("     , DAT.SAMPLE_QTY " + "\n");
            strSqlString.Append("     , LOS.DEFECT_QTY_1 " + "\n");            
            strSqlString.Append("     , ROUND((LOS.DEFECT_QTY_1 / DAT.SAMPLE_QTY) * 1000000, 2) AS LOSS_PER " + "\n");
            strSqlString.Append("     , LOS.DEFECT_CODE " + "\n");
            strSqlString.Append("     , DAT.RES_ID " + "\n");
            strSqlString.Append("     , DECODE(DAT.QC_FLAG, 'Y', 'PASS', 'FAIL') AS QC_FLAG " + "\n");
            strSqlString.Append("     , DAT.QC_TYPE " + "\n");
            strSqlString.Append("     , (SELECT USER_DESC || '(' || USER_ID || ')' FROM RWEBUSRDEF WHERE USER_ID = DAT.QC_OPERATOR) AS QC_USER " + "\n");
            strSqlString.Append("     , DAT.TRAN_TIME " + "\n");
            strSqlString.Append("     , DAT.SHIFT " + "\n");
            strSqlString.Append("     , DAT.QC_COMMENT " + "\n");
            strSqlString.Append("  FROM CPQCLOTHIS DAT " + "\n");
            strSqlString.Append("     , CPQCLOTDFT LOS " + "\n");
            strSqlString.Append("     , RWIPLOTHIS HIS " + "\n");
            strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND DAT.TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
            strSqlString.Append("   AND DAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("   AND DAT.HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("   AND DAT.FACTORY = LOS.FACTORY(+) " + "\n");
            strSqlString.Append("   AND DAT.LOT_ID = LOS.LOT_ID(+) " + "\n");
            strSqlString.Append("   AND DAT.QC_HIST_SEQ = LOS.QC_HIST_SEQ(+) " + "\n");
            strSqlString.Append("   AND DAT.FACTORY = HIS.FACTORY " + "\n");
            strSqlString.Append("   AND DAT.LOT_ID = HIS.LOT_ID " + "\n");
            strSqlString.Append("   AND DAT.HIST_SEQ = HIS.HIST_SEQ " + "\n");
            strSqlString.Append("   AND DAT.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("   AND DAT.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("   AND DAT.QC_TYPE " + cdvType.SelectedValueToQueryString + "\n");
            strSqlString.Append("   AND DAT.OPER " + cdvOper.SelectedValueToQueryString + "\n");

            if (cboResult.SelectedIndex == 1)
                strSqlString.Append("   AND DAT.QC_FLAG = 'Y' " + "\n");
            
            if (cboResult.SelectedIndex == 2)
                strSqlString.Append("   AND DAT.QC_FLAG = 'N' " + "\n");

            if (cboWorkGroup.SelectedIndex > 0)
                strSqlString.Append("   AND DAT.SHIFT = '" + cboWorkGroup.SelectedItem + "' " + "\n");

           // strSqlString.Append("   AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
           // strSqlString.Append("   AND STS.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");            
           

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            #endregion

            strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond1);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region Chart 생성을 위한 쿼리
        private string MakeChartSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();
            
            string strFromDate = cdvFromTo.ExactFromDate;
            string strToDate = cdvFromTo.ExactToDate;

            //switch (cboGraph.SelectedIndex)
            //{
            //    case 0: // 1. 부적합 종류별  발생 건 & TAT
            //        {
            //            #region Chart No.1
            //            strSqlString.Append("SELECT ABR_TYPE, COUNT(ABR_TYPE) AS CNT, ROUND(AVG(TAT), 2) AS TAT " + "\n");
            //            strSqlString.Append("  FROM ( " + "\n");
            //            strSqlString.Append("        SELECT SUBSTR(ABR_NO,1,3) AS ABR_TYPE " + "\n");
            //            strSqlString.Append("             , TRUNC(DECODE(CLOSE_TIME, ' ', SYSDATE, TO_DATE(CLOSE_TIME, 'YYYYMMDDHH24MISS')) - TO_DATE(STEP10_TIME, 'YYYYMMDDHH24MISS'), 2) AS TAT" + "\n");
            //            strSqlString.Append("          FROM CABRLOTSTS@RPTTOMES STS " + "\n");
            //            strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
            //            strSqlString.Append(" WHERE 1=1 " + "\n");
            //            strSqlString.Append("   AND STS.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            //            strSqlString.Append("   AND STS.FACTORY = MAT.FACTORY " + "\n");
            //            strSqlString.Append("   AND STS.MAT_ID = MAT.MAT_ID " + "\n");
            //            strSqlString.Append("   AND STEP10_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
            //            strSqlString.Append("   AND SUBSTR(STS.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("   AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("   AND STS.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("   AND STS.OPER_1 IN (" + cdvOper.getInQuery() + ")" + "\n");

            //            if (!ckbEndData.Checked)
            //            {
            //                strSqlString.Append("   AND STS.CLOSE_FLAG <> 'Y' " + "\n");
            //            }

            //            #region 상세 조회에 따른 SQL문 생성
            //            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            //            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            //            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            //            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            //            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            //            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            //            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            //            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            //            #endregion

            //            strSqlString.Append("       )" + "\n");
            //            strSqlString.Append(" GROUP BY ABR_TYPE " + "\n");
            //            #endregion
            //        }
            //        break;
            //    case 1: // 2. 기간 별 부적합 발생 건
            //        {
            //            #region Chart No.2
            //            strSqlString.Append("SELECT TRAN_TIME, COUNT(TRAN_TIME) AS CNT " + "\n");
            //            strSqlString.Append("  FROM ( " + "\n");

            //            if (cdvFromTo.DaySelector.SelectedValue.Equals("DAY"))
            //            {
            //                strSqlString.Append("        SELECT  GET_WORK_DATE(STEP10_TIME,'D') AS TRAN_TIME " + "\n");
            //            }
            //            else if (cdvFromTo.DaySelector.SelectedValue.Equals("WEEK"))
            //            {
            //                strSqlString.Append("        SELECT  GET_WORK_DATE(STEP10_TIME,'W') AS TRAN_TIME " + "\n");
            //            }
            //            else
            //            {
            //                strSqlString.Append("        SELECT  GET_WORK_DATE(STEP10_TIME,'M') AS TRAN_TIME " + "\n");
            //            }

            //            strSqlString.Append("          FROM CABRLOTSTS@RPTTOMES STS " + "\n");
            //            strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
            //            strSqlString.Append(" WHERE 1=1 " + "\n");
            //            strSqlString.Append("   AND STS.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            //            strSqlString.Append("   AND STS.FACTORY = MAT.FACTORY " + "\n");
            //            strSqlString.Append("   AND STS.MAT_ID = MAT.MAT_ID " + "\n");
            //            strSqlString.Append("   AND STEP10_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
            //            strSqlString.Append("   AND SUBSTR(STS.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("   AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("   AND STS.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("   AND STS.OPER_1 IN (" + cdvOper.getInQuery() + ")" + "\n");

            //            if (!ckbEndData.Checked)
            //            {
            //                strSqlString.Append("   AND STS.CLOSE_FLAG <> 'Y' " + "\n");
            //            }

            //            #region 상세 조회에 따른 SQL문 생성
            //            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            //            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            //            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            //            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            //            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            //            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            //            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            //            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            //            #endregion

            //            strSqlString.Append("       )" + "\n");
            //            strSqlString.Append(" GROUP BY TRAN_TIME " + "\n");
            //            #endregion
            //        }
            //        break;
            //    case 2: // 3. 불량별 파레토
            //        {
            //            #region Chart No.3
            //            strSqlString.Append("SELECT DEFECT_CODE, QTY " + "\n");
            //            strSqlString.Append("     , ROUND(QTY_S/QTY_A,4)*100 AS ADDED_RATIO  " + "\n");
            //            strSqlString.Append("  FROM ( " + "\n");
            //            strSqlString.Append("        SELECT DEFECT_CODE, QTY, SUM(QTY) OVER(ORDER BY QTY DESC, DEFECT_CODE) AS QTY_S, SUM(QTY) OVER() AS QTY_A " + "\n");
            //            strSqlString.Append("          FROM ( " + "\n");
            //            strSqlString.Append("                SELECT (SELECT DESC_1 FROM CABRDFTDEF@RPTTOMES WHERE DEL_FLAG=' ' AND FACTORY = STS.FACTORY AND DEFECT_CODE = STS.DEFECT_CODE) AS DEFECT_CODE " + "\n");
            //            strSqlString.Append("                     , COUNT(STS.DEFECT_CODE) AS QTY " + "\n");
            //            strSqlString.Append("                  FROM CABRLOTSTS@RPTTOMES STS " + "\n");
            //            strSqlString.Append("                     , MWIPMATDEF MAT " + "\n");
            //            strSqlString.Append("                 WHERE 1=1 " + "\n");
            //            strSqlString.Append("                   AND STS.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            //            strSqlString.Append("                   AND STS.FACTORY = MAT.FACTORY " + "\n");
            //            strSqlString.Append("                   AND STS.MAT_ID = MAT.MAT_ID " + "\n");
            //            strSqlString.Append("                   AND STEP10_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
            //            strSqlString.Append("                   AND SUBSTR(STS.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("                   AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("                   AND STS.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("                   AND STS.OPER_1 IN (" + cdvOper.getInQuery() + ")" + "\n");

            //            if (!ckbEndData.Checked)
            //            {
            //                strSqlString.Append("                   AND STS.CLOSE_FLAG <> 'Y' " + "\n");
            //            }

            //            #region 상세 조회에 따른 SQL문 생성
            //            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
            //                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            //            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
            //                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            //            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
            //                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            //            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
            //                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            //            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
            //                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            //            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
            //                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            //            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
            //                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            //            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
            //                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            //            #endregion

            //            strSqlString.Append("                 GROUP BY STS.FACTORY, STS.DEFECT_CODE " + "\n");                        
            //            strSqlString.Append("               )" + "\n");                        
            //            strSqlString.Append("       )" + "\n");
            //            strSqlString.Append(" ORDER BY QTY_S " + "\n");
            //            #endregion
            //        }
            //        break;
            //    case 3: // 4. 불량별 부적합 발생 건 & 생산량
            //        {                        
            //            #region Chart No.4
            //            String stColumnName = String.Empty;
            //            DataTable loss_code = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeLossCodeSqlString());
            //            String strDecodeQuery = CmnFunction.GetDecodeQueryStringFromDataTable("     , SUM(DECODE(DEFECT_CODE, ", loss_code, ", 1, 0)) AS ", true);

            //            for (int i = 0; i < loss_code.Rows.Count; i++)
            //            {
            //                stColumnName += "     , \"" + loss_code.Rows[i][0] + "\" " + "\n";
            //            }

            //            strSqlString.Append("SELECT TRAN_TIME, SHP_QTY  " + "\n");
            //            strSqlString.Append(stColumnName);
            //            strSqlString.Append("  FROM ( " + "\n");
            //            strSqlString.Append("        SELECT TRAN_TIME " + "\n");
            //            strSqlString.Append(strDecodeQuery);
            //            strSqlString.Append("          FROM ( " + "\n");

            //            if (cdvFromTo.DaySelector.SelectedValue.Equals("DAY"))
            //            {
            //                strSqlString.Append("                SELECT GET_WORK_DATE(STEP10_TIME,'D') AS TRAN_TIME " + "\n");
            //            }
            //            else if (cdvFromTo.DaySelector.SelectedValue.Equals("WEEK"))
            //            {
            //                strSqlString.Append("                SELECT GET_WORK_DATE(STEP10_TIME,'W') AS TRAN_TIME " + "\n");
            //            }
            //            else
            //            {
            //                strSqlString.Append("                SELECT GET_WORK_DATE(STEP10_TIME,'M') AS TRAN_TIME " + "\n");
            //            }

            //            strSqlString.Append("                     , (SELECT DESC_1 FROM CABRDFTDEF@RPTTOMES WHERE DEL_FLAG=' ' AND FACTORY = STS.FACTORY AND DEFECT_CODE = STS.DEFECT_CODE) AS DEFECT_CODE " + "\n");
            //            strSqlString.Append("                  FROM CABRLOTSTS@RPTTOMES STS " + "\n");
            //            strSqlString.Append("                     , MWIPMATDEF MAT " + "\n");
            //            strSqlString.Append("                 WHERE 1=1 " + "\n");
            //            strSqlString.Append("                   AND STS.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            //            strSqlString.Append("                   AND STS.FACTORY = MAT.FACTORY " + "\n");
            //            strSqlString.Append("                   AND STS.MAT_ID = MAT.MAT_ID " + "\n");
            //            strSqlString.Append("                   AND STEP10_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
            //            strSqlString.Append("                   AND SUBSTR(STS.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("                   AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("                   AND STS.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("                   AND STS.OPER_1 IN (" + cdvOper.getInQuery() + ")" + "\n");

            //            if (!ckbEndData.Checked)
            //            {
            //                strSqlString.Append("                   AND STS.CLOSE_FLAG <> 'Y' " + "\n");
            //            }

            //            #region 상세 조회에 따른 SQL문 생성
            //            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
            //                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            //            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
            //                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            //            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
            //                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            //            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
            //                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            //            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
            //                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            //            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
            //                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            //            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
            //                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            //            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
            //                strSqlString.AppendFormat("                   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            //            #endregion

            //            strSqlString.Append("               ) " + "\n");
            //            strSqlString.Append("         GROUP BY TRAN_TIME " + "\n");
            //            strSqlString.Append("       ) A" + "\n");
            //            strSqlString.Append("     , (" + "\n");
            //            strSqlString.Append("        SELECT WORK_DATE, SUM(SHP_QTY_1) AS SHP_QTY " + "\n");
            //            strSqlString.Append("          FROM VSUMWIPSHP LOT " + "\n");
            //            strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
            //            strSqlString.Append("         WHERE LOT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            //            strSqlString.Append("           AND LOT.FACTORY = MAT.FACTORY " + "\n");
            //            strSqlString.Append("           AND LOT.MAT_ID = MAT.MAT_ID " + "\n");
            //            strSqlString.Append("           AND WORK_DATE BETWEEN '" + cdvFromTo.HmFromDay + "' AND '" + cdvFromTo.HmToDay + "'" + "\n");
            //            strSqlString.Append("           AND LOT_TYPE = 'W' " + "\n");
            //            strSqlString.Append("         GROUP BY WORK_DATE " + "\n");
            //            strSqlString.Append("       ) B" + "\n");
            //            strSqlString.Append(" WHERE A.TRAN_TIME = B.WORK_DATE " + "\n");
            //            #endregion
            //        }
            //        break;
            //    case 4: // 5. 불량별 부적합 제품 발생 건
            //        {
            //            #region Chart No.5
            //            DataTable loss_code = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeLossCodeSqlString());
            //            String strDecodeQuery = CmnFunction.GetDecodeQueryStringFromDataTable("     , SUM(DECODE(DEFECT_CODE, ", loss_code, ", 1, 0)) AS ", true);

            //            strSqlString.Append("SELECT MAT_ID  " + "\n");
            //            strSqlString.Append(strDecodeQuery);
            //            strSqlString.Append("  FROM ( " + "\n");
            //            strSqlString.Append("        SELECT STS.MAT_ID " + "\n");                        
            //            strSqlString.Append("             , (SELECT DESC_1 FROM CABRDFTDEF@RPTTOMES WHERE DEL_FLAG=' ' AND FACTORY = STS.FACTORY AND DEFECT_CODE = STS.DEFECT_CODE) AS DEFECT_CODE " + "\n");
            //            strSqlString.Append("          FROM CABRLOTSTS@RPTTOMES STS " + "\n");
            //            strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
            //            strSqlString.Append("         WHERE 1=1 " + "\n");
            //            strSqlString.Append("           AND STS.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            //            strSqlString.Append("           AND STS.FACTORY = MAT.FACTORY " + "\n");
            //            strSqlString.Append("           AND STS.MAT_ID = MAT.MAT_ID " + "\n");
            //            strSqlString.Append("           AND STEP10_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
            //            strSqlString.Append("           AND SUBSTR(STS.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("           AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("           AND STS.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("           AND STS.OPER_1 IN (" + cdvOper.getInQuery() + ")" + "\n");

            //            if (!ckbEndData.Checked)
            //            {
            //                strSqlString.Append("           AND STS.CLOSE_FLAG <> 'Y' " + "\n");
            //            }

            //            #region 상세 조회에 따른 SQL문 생성
            //            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
            //                strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            //            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
            //                strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            //            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
            //                strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            //            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
            //                strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            //            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
            //                strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            //            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
            //                strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            //            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
            //                strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            //            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
            //                strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            //            #endregion

            //            strSqlString.Append("       ) " + "\n");
            //            strSqlString.Append(" GROUP BY MAT_ID " + "\n");
            //            #endregion
            //        }
            //        break;
            //    case 5: // 6. STEP 별 부적합 발생 건
            //        {
            //            #region Chart No.6
            //            strSqlString.Append("SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_ABR_STEP' AND FACTORY = STS.FACTORY AND KEY_1 = STS.CUR_STEP) AS CUR_STEP " + "\n");
            //            strSqlString.Append("     , COUNT(CUR_STEP) AS CNT " + "\n");                        
            //            strSqlString.Append("  FROM CABRLOTSTS@RPTTOMES STS " + "\n");
            //            strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
            //            strSqlString.Append(" WHERE 1=1 " + "\n");
            //            strSqlString.Append("   AND STS.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            //            strSqlString.Append("   AND STS.FACTORY = MAT.FACTORY " + "\n");
            //            strSqlString.Append("   AND STS.MAT_ID = MAT.MAT_ID " + "\n");
            //            strSqlString.Append("   AND STEP10_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
            //            strSqlString.Append("   AND SUBSTR(STS.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("   AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("   AND STS.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("   AND STS.OPER_1 IN (" + cdvOper.getInQuery() + ")" + "\n");

            //            if (!ckbEndData.Checked)
            //            {
            //                strSqlString.Append("   AND STS.CLOSE_FLAG <> 'Y' " + "\n");
            //            }

            //            #region 상세 조회에 따른 SQL문 생성
            //            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            //            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            //            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            //            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            //            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            //            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            //            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            //            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            //            #endregion
                                                
            //            strSqlString.Append(" GROUP BY STS.FACTORY, CUR_STEP " + "\n");
            //            #endregion
            //        }
            //        break;
            //    case 6: // 7. 작업자별 부적합 발생 건
            //        {
            //            #region Chart No.7
            //            strSqlString.Append("SELECT (SELECT USER_DESC || '(' || USER_ID || ')' FROM RWEBUSRDEF WHERE USER_ID = STS.QA_OPER) AS OPER_USER " + "\n");
            //            strSqlString.Append("     , COUNT(QA_OPER) AS CNT " + "\n");
            //            strSqlString.Append("  FROM CABRLOTSTS@RPTTOMES STS " + "\n");
            //            strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
            //            strSqlString.Append(" WHERE 1=1 " + "\n");
            //            strSqlString.Append("   AND STS.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            //            strSqlString.Append("   AND STS.FACTORY = MAT.FACTORY " + "\n");
            //            strSqlString.Append("   AND STS.MAT_ID = MAT.MAT_ID " + "\n");
            //            strSqlString.Append("   AND STEP10_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
            //            strSqlString.Append("   AND SUBSTR(STS.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("   AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("   AND STS.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
            //            strSqlString.Append("   AND STS.OPER_1 IN (" + cdvOper.getInQuery() + ")" + "\n");

            //            if (!ckbEndData.Checked)
            //            {
            //                strSqlString.Append("   AND STS.CLOSE_FLAG <> 'Y' " + "\n");
            //            }

            //            #region 상세 조회에 따른 SQL문 생성
            //            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            //            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            //            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            //            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            //            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            //            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            //            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            //            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
            //                strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            //            #endregion

            //            strSqlString.Append(" GROUP BY QA_OPER " + "\n");
            //            #endregion
            //        }
            //        break;
            //}

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region 조회된 데이터중 불량명만을 구하기 위한 쿼리
        private string MakeLossCodeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT DISTINCT (SELECT DESC_1 FROM CABRDFTDEF@RPTTOMES WHERE DEL_FLAG=' ' AND FACTORY = STS.FACTORY AND DEFECT_CODE = STS.DEFECT_CODE) AS DEFECT_CODE " + "\n");
            strSqlString.Append("  FROM CABRLOTSTS@RPTTOMES STS " + "\n");
            strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND STS.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("   AND STS.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("   AND STS.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("   AND STEP10_TIME BETWEEN '" + cdvFromTo.ExactFromDate + "' AND '" + cdvFromTo.ExactToDate + "'" + "\n");
            //strSqlString.Append("   AND SUBSTR(STS.ABR_NO, 0, 3) " + cdvCp.SelectedValueToQueryString + "\n");
            //strSqlString.Append("   AND STS.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");
            //strSqlString.Append("   AND STS.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
            //strSqlString.Append("   AND STS.OPER_1 IN (" + cdvOper.getInQuery() + ")" + "\n");


            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            #endregion

            return strSqlString.ToString();
        }
        #endregion

        /// <summary>
        /// 5. Chart 생성
        /// </summary>
        /// <param name="rowCount">Row Number</param>
        private void ShowChart()
        {            
            // 차트 설정
            udcChartFX1.RPT_1_ChartInit();
            udcChartFX1.RPT_2_ClearData();

            udcChartFX1.AxisY.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisY2.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisX.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);

            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeChartSqlString());
            
            if (dt == null || dt.Rows.Count < 1)
                return;

            switch (cboGraph.SelectedIndex)
            {
                case 0: // 1. 부적합 종류별  발생 건 & TAT
                    {
                        #region Chart No.1
                        udcChartFX1.DataSource = dt;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;

                        udcChartFX1.Series[1].YAxis = SoftwareFX.ChartFX.YAxis.Secondary;
                        udcChartFX1.Series[1].Gallery = SoftwareFX.ChartFX.Gallery.Lines;

                        udcChartFX1.LegendBox = false;
                        udcChartFX1.PointLabels = true;
                        udcChartFX1.Chart3D = false;
                        udcChartFX1.MultipleColors = false;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        udcChartFX1.SerLegBox = true;
                        udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        udcChartFX1.AxisY2.DataFormat.Decimals = 2;
                        udcChartFX1.PointLabelColor = Color.Blue;
                        udcChartFX1.Series[1].PointLabelColor = Color.Red;
                        udcChartFX1.RecalcScale();
                        #endregion
                    }
                    break;

                case 1: // 2. 기간 별 부적합 발생 건
                    {
                        #region Chart No.2
                        int rowCount = dt.Rows.Count;

                        udcChartFX1.RPT_3_OpenData(1, rowCount);
                        int[] cnt_rows = new Int32[rowCount];

                        for (int i = 0; i < cnt_rows.Length; i++)
                        {
                            cnt_rows[i] = i;
                        }

                        // 건수
                        double cnt = udcChartFX1.RPT_4_AddData(dt, cnt_rows, new int[] { 1 }, SeriseType.Column);

                        udcChartFX1.RPT_5_CloseData();

                        //각 Serise별로 다른 타입을 사용할 경우
                        udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 0, 1, "Number", AsixType.Y, DataTypes.Initeger, cnt * 1.2);
                        udcChartFX1.RPT_7_SetXAsixTitleUsingDataTable(dt, 0);
                        udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "Number" }, SoftwareFX.ChartFX.Docked.Top);

                        // 기타 설정
                        udcChartFX1.PointLabels = true;
                        udcChartFX1.Series[1].LineWidth = 2;
                        udcChartFX1.AxisY.LabelsFormat.Decimals = 0;
                        udcChartFX1.AxisY.DataFormat.Decimals = 0;
                        #endregion
                    }
                    break;

                case 2: // 3. 불량별 파레토
                    {
                        #region Chart No.3
                        int rowCount = dt.Rows.Count;

                        udcChartFX1.RPT_3_OpenData(2, rowCount);
                        int[] cnt_rows = new Int32[rowCount];

                        for (int i = 0; i < cnt_rows.Length; i++)
                        {
                            cnt_rows[i] = i;
                        }

                        // 건수
                        double cnt = udcChartFX1.RPT_4_AddData(dt, cnt_rows, new int[] { 1 }, SeriseType.Column);

                        // 누적점유율
                        double percent = udcChartFX1.RPT_4_AddData(dt, cnt_rows, new int[] { 2 }, SeriseType.Column);

                        udcChartFX1.RPT_5_CloseData();

                        //각 Serise별로 다른 타입을 사용할 경우
                        udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 0, 1, "Number", AsixType.Y, DataTypes.Initeger, cnt * 1.2);
                        udcChartFX1.RPT_6_SetGallery(ChartType.Line, 1, 1, "Cumulative occupation", AsixType.Y2, DataTypes.Initeger, percent);

                        //각 Serise별로 동일한 타입을 사용할 경우
                        //udcChartFX1.RPT_6_SetGallery(ChartType.Bar, "[단위 : sls]", AsixType.Y, DataTypes.Initeger, yield * 1.2);

                        udcChartFX1.RPT_7_SetXAsixTitleUsingDataTable(dt, 0);
                        udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "Number", "Cumulative occupation" }, SoftwareFX.ChartFX.Docked.Top);

                        // 기타 설정
                        udcChartFX1.PointLabels = false;
                        udcChartFX1.Series[1].LineWidth = 2;
                        udcChartFX1.AxisY.LabelsFormat.Decimals = 0;
                        udcChartFX1.AxisY.DataFormat.Decimals = 0;
                        udcChartFX1.AxisY2.DataFormat.Format = SoftwareFX.ChartFX.AxisFormat.Number;
                        udcChartFX1.AxisY2.DataFormat.Decimals = 2;
                        udcChartFX1.AxisX.LabelAngle = 90;
                        udcChartFX1.AxisX.Staggered = false;
                        #endregion
                    }
                    break;

                case 3: // 4. 불량별 부적합 발생 건 & 생산량
                    {
                        #region Chart No.4
                        double cnt = 0;
                        double max = 0;
                        double max_temp = 0;
                        int arrCnt = 0;

                        int rowCount = dt.Rows.Count;

                        udcChartFX1.RPT_3_OpenData(2, rowCount);
                        string[] LegBox = new string[dt.Columns.Count - 1]; //데이터 테이블의 2째 컬럼헤더 부터 사용하기에 -1을 한다.
                        int[] loss_rows = new Int32[rowCount];
                        int[] tot_rows = new Int32[rowCount];

                        for (int i = 0; i < loss_rows.Length; i++)
                        {
                            loss_rows[i] = i;
                            tot_rows[i] = i;
                        }

                        // 데이터 테이블의 컬럼헤더를 저장한다. LegBox 표시 값으로 사용하려...
                        for (int x = 1; x < dt.Columns.Count; x++)
                        {
                            LegBox[arrCnt] = dt.Columns[x].ToString();

                            arrCnt++;
                        }

                        // 생산 수량 표시
                        cnt = udcChartFX1.RPT_4_AddData(dt, tot_rows, new int[] { 1 }, SeriseType.Column);

                        for (int i = 2; i < dt.Columns.Count; i++)
                        {
                            // 각 불량 수 표시
                            max = udcChartFX1.RPT_4_AddData(dt, loss_rows, new int[] { i }, SeriseType.Column);

                            if (max > max_temp)
                            {
                                max_temp = max;
                            }
                        }                        
                        max = max_temp;
                        
                        udcChartFX1.RPT_5_CloseData();

                        udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 0, 1, "output", AsixType.Y2, DataTypes.Initeger, cnt * 1.2);

                        for (int i = 1; i < dt.Columns.Count; i++)
                        {
                            udcChartFX1.RPT_6_SetGallery(ChartType.Line, i, 1, "", AsixType.Y, DataTypes.Initeger, max * 1.2);
                        }

                        udcChartFX1.RPT_7_SetXAsixTitleUsingDataTable(dt, 0);
                        udcChartFX1.RPT_8_SetSeriseLegend(LegBox, SoftwareFX.ChartFX.Docked.Right);

                        // 기타 설정
                        udcChartFX1.PointLabels = false;                        
                        udcChartFX1.AxisY.LabelsFormat.Decimals = 0;
                        udcChartFX1.AxisY.DataFormat.Decimals = 0;
                        udcChartFX1.AxisY2.DataFormat.Format = 0;
                        udcChartFX1.AxisY2.DataFormat.Decimals = 0;
                        #endregion
                    }
                    break;
                case 4: // 5. 불량별 부적합 제품 발생 건
                    {
                        #region Chart No.5
                        double max = 0;
                        double max_temp = 0;
                        int arrCnt = 0;

                        int rowCount = dt.Rows.Count;

                        udcChartFX1.RPT_3_OpenData(1, rowCount);
                        string[] LegBox = new string[dt.Columns.Count - 1]; //데이터 테이블의 2째 컬럼헤더 부터 사용하기에 -1을 한다.
                        int[] loss_rows = new Int32[rowCount];

                        for (int i = 0; i < loss_rows.Length; i++)
                        {
                            loss_rows[i] = i;
                        }

                        // 데이터 테이블의 컬럼헤더를 저장한다. LegBox 표시 값으로 사용하려...
                        for (int x = 1; x < dt.Columns.Count; x++)
                        {
                            LegBox[arrCnt] = dt.Columns[x].ToString();

                            arrCnt++;
                        }

                        for (int i = 1; i < dt.Columns.Count; i++)
                        {
                            // 각 불량 수 표시
                            max = udcChartFX1.RPT_4_AddData(dt, loss_rows, new int[] { i }, SeriseType.Column);

                            if (max > max_temp)
                            {
                                max_temp = max;
                            }
                        }
                        max = max_temp;

                        udcChartFX1.RPT_5_CloseData();

                        for (int i = 0; i < dt.Columns.Count - 1; i++)
                        {
                            udcChartFX1.RPT_6_SetGallery(ChartType.Line, i, 1, "", AsixType.Y, DataTypes.Initeger, max * 1.2);
                        }

                        udcChartFX1.RPT_7_SetXAsixTitleUsingDataTable(dt, 0);
                        udcChartFX1.RPT_8_SetSeriseLegend(LegBox, SoftwareFX.ChartFX.Docked.Right);

                        // 기타 설정
                        udcChartFX1.PointLabels = false;
                        udcChartFX1.AxisY.LabelsFormat.Decimals = 0;
                        udcChartFX1.AxisY.DataFormat.Decimals = 0;
                        udcChartFX1.AxisY2.DataFormat.Format = 0;
                        udcChartFX1.AxisY2.DataFormat.Decimals = 0;
                        udcChartFX1.AxisX.LabelAngle = 90;
                        udcChartFX1.AxisX.Staggered = false;
                        #endregion
                    }
                    break;
                case 5: // 6. STEP 별 부적합 발생 건
                    {
                        #region Chart No.6
                        int rowCount = dt.Rows.Count;

                        udcChartFX1.RPT_3_OpenData(1, rowCount);
                        int[] cnt_rows = new Int32[rowCount];

                        for (int i = 0; i < cnt_rows.Length; i++)
                        {
                            cnt_rows[i] = i;
                        }

                        // 건수
                        double cnt = udcChartFX1.RPT_4_AddData(dt, cnt_rows, new int[] { 1 }, SeriseType.Column);

                        udcChartFX1.RPT_5_CloseData();

                        //각 Serise별로 다른 타입을 사용할 경우
                        udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 0, 1, "Number", AsixType.Y, DataTypes.Initeger, cnt * 1.2);
                        udcChartFX1.RPT_7_SetXAsixTitleUsingDataTable(dt, 0);
                        udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "Number" }, SoftwareFX.ChartFX.Docked.Top);

                        // 기타 설정
                        udcChartFX1.PointLabels = true;
                        udcChartFX1.Series[1].LineWidth = 2;
                        udcChartFX1.AxisY.LabelsFormat.Decimals = 0;
                        udcChartFX1.AxisY.DataFormat.Decimals = 0;
                        #endregion
                    }
                    break;
                case 6: // 7. 작업자별 부적합 발생 건
                    {
                        #region Chart No.7
                        int rowCount = dt.Rows.Count;

                        udcChartFX1.RPT_3_OpenData(1, rowCount);
                        int[] cnt_rows = new Int32[rowCount];

                        for (int i = 0; i < cnt_rows.Length; i++)
                        {
                            cnt_rows[i] = i;
                        }

                        // 건수
                        double cnt = udcChartFX1.RPT_4_AddData(dt, cnt_rows, new int[] { 1 }, SeriseType.Column);

                        udcChartFX1.RPT_5_CloseData();

                        //각 Serise별로 다른 타입을 사용할 경우
                        udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 0, 1, "Number", AsixType.Y, DataTypes.Initeger, cnt * 1.2);
                        udcChartFX1.RPT_7_SetXAsixTitleUsingDataTable(dt, 0);
                        udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "Number" }, SoftwareFX.ChartFX.Docked.Top);

                        // 기타 설정
                        udcChartFX1.PointLabels = true;
                        udcChartFX1.Series[1].LineWidth = 2;
                        udcChartFX1.AxisY.LabelsFormat.Decimals = 0;
                        udcChartFX1.AxisY.DataFormat.Decimals = 0;
                        udcChartFX1.AxisX.LabelAngle = 90;
                        udcChartFX1.AxisX.Staggered = false;
                        #endregion
                    }
                    break;
            }

            //udcChartFX1.AxisX.Title.Text = "Nonconformance type";

        }

        #endregion

        /// <summary>
        /// 버튼 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Button Click Event

        private void btnView_Click(object sender, EventArgs e)
        {
            if (CheckField() == false) return;

            LoadingPopUp.LoadIngPopUpShow(this);

            DataTable dt = null;
            GridColumnInit();

            try
            {
                this.Refresh();
                //LoadingPopUp.LoadIngPopUpShow(this);
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    udcChartFX1.RPT_1_ChartInit();
                    udcChartFX1.RPT_2_ClearData();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 10, null, null, btnSort);

                //Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.DataSource = dt;
                spdData.RPT_AutoFit(false);

                //ShowChart();
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
            ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1, this.lblTitle.Text, null, null);
            //spdData.ExportExcel();
        }

        #endregion

        private void cdvLoss_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DEFECT_CODE AS Code, DESC_1 AS Data" + "\n";
            strQuery += "  FROM CABRDFTDEF@RPTTOMES " + "\n";
            strQuery += " WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n";
            strQuery += " ORDER BY DEFECT_CODE" + "\n";

          //  cdvLoss.sDynamicQuery = strQuery;
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);

           // cdvLoss.sFactory = cdvFactory.txtValue;
            cdvOper.sFactory = cdvFactory.txtValue;
            cdvType.sFactory = cdvFactory.txtValue;
        }

        private void cdvType_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT KEY_1 AS CODE, ' ' AS DATA" + "\n";
            strQuery += "  FROM MGCMTBLDAT " + "\n";
            strQuery += " WHERE TABLE_NAME = 'ABRLOT_PURPOSE' " + "\n";
            strQuery += "   AND FACTORY = '" + cdvFactory.Text + "'" + "\n";

            cdvType.sDynamicQuery = strQuery;
        }

    }
}