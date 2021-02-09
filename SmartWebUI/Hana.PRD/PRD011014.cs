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
    public partial class PRD011014 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD011014<br/>
        /// 클래스요약: 제품별가용설비군 조회<br/>
        /// 작  성  자: 에이스텍 김태호<br/>
        /// 최초작성일: 2013-10-29<br/>
        /// 상세  설명: 제품별가용설비군 조회<br/>
        /// 2015-09-25-임종우 : 고객사 명 하드 코딩 되어 있는것을 기준정보로 변경 (임태성K 요청)
        /// </summary>
        public PRD011014()
        {
            InitializeComponent();
            
            SortInit();
            GridColumnInit();
            
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(cdvFactory.txtValue);
            SetStep();
            cdvFactory.Enabled = false;
            cdvStep.Text = "ALL";
        }
        #region " Constant Definition "

        #endregion

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

            if (cdvStep.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
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
                spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("FAMILY", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("PKG", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PKG_CODE", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("SAP_CODE", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("TYPE_1", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("TYPE_2", 0, 7, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("STEP", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("LD_COUNT", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("DENSITY", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("GENERATION", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MAJOR", 0, 12, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("PIN_TYPE", 0, 13, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);

                spdData.RPT_AddBasicColumn("Available Model", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                spdData.RPT_AddBasicColumn("rank 1", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("Model", 2, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Production rate", 2, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("UPEH", 2, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                spdData.RPT_MerageHeaderColumnSpan(1, 14, 3);

                spdData.RPT_AddBasicColumn("rank 2", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("Model", 2, 17, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Production rate", 2, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("UPEH", 2, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                spdData.RPT_MerageHeaderColumnSpan(1, 17, 3);

                spdData.RPT_AddBasicColumn("rank 3", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("Model", 2, 20, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Production rate", 2, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("UPEH", 2, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                spdData.RPT_MerageHeaderColumnSpan(1, 20, 3);

                spdData.RPT_AddBasicColumn("rank 4", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("Model", 2, 23, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Production rate", 2, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("UPEH", 2, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                
                spdData.RPT_MerageHeaderColumnSpan(1, 23, 3);

                spdData.RPT_AddBasicColumn("rank 5", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("Model", 2, 26, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Production rate", 2, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("UPEH", 2, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                spdData.RPT_MerageHeaderColumnSpan(1, 26, 3);

                spdData.RPT_MerageHeaderColumnSpan(0, 14, 15);

                spdData.RPT_AddBasicColumn("Equipment compatibility", 0, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("확보", 2, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("unobtained", 2, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("Compatibility rate", 2, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                spdData.RPT_MerageHeaderColumnSpan(0, 29, 3);


                spdData.RPT_MerageHeaderRowSpan(0, 0, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 6, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 7, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 8, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 9, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 10, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 11, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 12, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 13, 3);

                spdData.RPT_MerageHeaderRowSpan(0, 29, 2);

                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "(SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1) AS CUSTOMER", "MAT.MAT_GRP_1", "MAT.MAT_GRP_1", "DECODE(MAT.MAT_GRP_1, 'SE', 1, 'HX', 2, 'IM', 3, 'FC', 4, 'IG', 5, 6), MAT.MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT_GRP_2 AS FAMILY", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG", "MAT_GRP_10 AS PKG", "MAT.MAT_GRP_10", "MAT.MAT_GRP_10", "MAT.MAT_GRP_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT_ID AS PRODUCT", "MAT.MAT_ID", "MAT.MAT_ID", "MAT.MAT_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG_CODE", "MAT_CMF_11 AS PKG_CODE", "MAT.MAT_CMF_11", "MAT.MAT_CMF_11", "MAT.MAT_CMF_11", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SAP_CODE", "BASE_MAT_ID AS SAP_CODE", "MAT.BASE_MAT_ID", "MAT.BASE_MAT_ID", "MAT.BASE_MAT_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE_1", "MAT_GRP_4 AS TYPE_1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE_2", "MAT_GRP_5 AS TYPE_2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5", "DECODE(MAT.MAT_GRP_5, '-', 1, '1st', 2, 'Middle', 3, 'Middle1', 4, 'Middle 1', 5, 'Middle2', 6, 'Middle 2', 7, 'Merge', 8), MAT.MAT_GRP_5", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("STEP", "OPER AS STEP", "A.OPER", "A.OPER", "A.OPER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD_COUNT", "MAT_GRP_6 AS LD_COUNT", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT_GRP_7 AS DENSITY", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT_GRP_8 AS GENERATION", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR", "MAT_GRP_9 AS MAJOR", "MAT.MAT_GRP_9", "MAT.MAT_GRP_9", "MAT.MAT_GRP_9", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "MAT_CMF_10 AS PIN_TYPE", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10", false);
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
            string QueryCond4 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            strSqlString.Append("SELECT " + QueryCond1 + " " + "\n");
            strSqlString.Append("     , RES_MODEL_S1, DECODE(SHP_MON_S1, 0, 0, ROUND(SHP_MON_S1/SHP_MON_TTL*100,2)) AS SHP_MON_S1, ROUND(UPH_S1, 0) AS UPH_S1 " + "\n");
            strSqlString.Append("     , RES_MODEL_S2, DECODE(SHP_MON_S2, 0, 0, ROUND(SHP_MON_S2/SHP_MON_TTL*100,2)) AS SHP_MON_S2, ROUND(UPH_S2, 0) AS UPH_S2 " + "\n");
            strSqlString.Append("     , RES_MODEL_S3, DECODE(SHP_MON_S3, 0, 0, ROUND(SHP_MON_S3/SHP_MON_TTL*100,2)) AS SHP_MON_S3, ROUND(UPH_S3, 0) AS UPH_S3 " + "\n");
            strSqlString.Append("     , RES_MODEL_S4, DECODE(SHP_MON_S4, 0, 0, ROUND(SHP_MON_S4/SHP_MON_TTL*100,2)) AS SHP_MON_S4, ROUND(UPH_S4, 0) AS UPH_S4 " + "\n");
            strSqlString.Append("     , RES_MODEL_S5, DECODE(SHP_MON_S5, 0, 0, ROUND(SHP_MON_S5/SHP_MON_TTL*100,2)) AS SHP_MON_S5, ROUND(UPH_S5, 0) AS UPH_S5 " + "\n");
            strSqlString.Append("     , RNAK AS SEC " + "\n");
            strSqlString.Append("     , AMDL - RNAK AS NSEC " + "\n");
            strSqlString.Append("     , DECODE(RNAK, 0, 0, ROUND(RNAK/AMDL*100,2)) AS RATE_COMP " + "\n");
            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT " + QueryCond2 + " " + "\n");
            strSqlString.Append("             , MAX(RANK)-1 AS RNAK " + "\n");
            strSqlString.Append("             , MAX(B.AMDL) AS AMDL " + "\n");
            strSqlString.Append("             , MAX(DECODE(RANK, 1, A.RES_MODEL, ' ')) AS RES_MODEL_TTL, SUM(DECODE(RANK, 1, A.SHP_MON, 0)) AS SHP_MON_TTL, AVG(DISTINCT DECODE(RANK, 1, A.UPH, NULL)) AS UPH_TTL " + "\n");
            strSqlString.Append("             , MAX(DECODE(RANK, 2, A.RES_MODEL, ' ')) AS RES_MODEL_S1, SUM(DECODE(RANK, 2, A.SHP_MON, 0)) AS SHP_MON_S1, AVG(DISTINCT DECODE(RANK, 2, A.UPH, NULL)) AS UPH_S1 " + "\n");
            strSqlString.Append("             , MAX(DECODE(RANK, 3, A.RES_MODEL, ' ')) AS RES_MODEL_S2, SUM(DECODE(RANK, 3, A.SHP_MON, 0)) AS SHP_MON_S2, AVG(DISTINCT DECODE(RANK, 3, A.UPH, NULL)) AS UPH_S2 " + "\n");
            strSqlString.Append("             , MAX(DECODE(RANK, 4, A.RES_MODEL, ' ')) AS RES_MODEL_S3, SUM(DECODE(RANK, 4, A.SHP_MON, 0)) AS SHP_MON_S3, AVG(DISTINCT DECODE(RANK, 4, A.UPH, NULL)) AS UPH_S3 " + "\n");
            strSqlString.Append("             , MAX(DECODE(RANK, 5, A.RES_MODEL, ' ')) AS RES_MODEL_S4, SUM(DECODE(RANK, 5, A.SHP_MON, 0)) AS SHP_MON_S4, AVG(DISTINCT DECODE(RANK, 5, A.UPH, NULL)) AS UPH_S4 " + "\n");
            strSqlString.Append("             , MAX(DECODE(RANK, 6, A.RES_MODEL, ' ')) AS RES_MODEL_S5, SUM(DECODE(RANK, 6, A.SHP_MON, 0)) AS SHP_MON_S5, AVG(DISTINCT DECODE(RANK, 6, A.UPH, NULL)) AS UPH_S5 " + "\n");
            strSqlString.Append("          FROM MWIPMATDEF MAT " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT SHP.MAT_ID, SHP.OPER, SHP.RES_MODEL " + "\n");
            strSqlString.Append("                     , SUM(NVL(SHP.QTY,0)) AS SHP_MON " + "\n");
            strSqlString.Append("                     , SUM(NVL(UPH.UPEH,0)) AS UPH " + "\n");
            strSqlString.Append("                     , ROW_NUMBER() OVER (PARTITION BY SHP.MAT_ID, SHP.OPER ORDER BY SHP.MAT_ID, SHP.OPER, DECODE(SHP.RES_MODEL, '', 1, 2), SUM(NVL(SHP.QTY,0)) DESC, SUM(NVL(UPH.UPEH,0)) DESC ) AS RANK   " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT RES_MODEL, MAT_ID, OPER, SUM(MES_PROC_QTY) AS QTY " + "\n");
            strSqlString.Append("                          FROM TSM_EQUIP_PROC_RESULT@RPTTOFA " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND WORK_MONTH = '" + DateTime.Now.AddMonths(-1).ToString("yyyyMM") + "' " + "\n");
            strSqlString.Append("                         GROUP BY RES_MODEL, MAT_ID, OPER " + "\n");
            strSqlString.Append("                       ) SHP " + "\n");
            strSqlString.Append("                     , CRASUPHDEF UPH " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND UPH.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                   AND SHP.MAT_ID = UPH.MAT_ID(+) " + "\n");
            strSqlString.Append("                   AND SHP.OPER = UPH.OPER(+) " + "\n");
            strSqlString.Append("                   AND SHP.RES_MODEL = UPH.RES_MODEL(+) " + "\n");
            strSqlString.Append("                   AND SHP.OPER " + cdvStep.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND SHP.MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");
            strSqlString.Append("                 GROUP BY GROUPING SETS ((SHP.MAT_ID, SHP.OPER, SHP.RES_MODEL), (SHP.MAT_ID, SHP.OPER)) " + "\n");
            strSqlString.Append("               ) A " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT OPER, COUNT(OPER) AS AMDL " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT OPER, RES_MODEL " + "\n");
            strSqlString.Append("                          FROM TSM_EQUIP_PROC_RESULT@RPTTOFA " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND WORK_MONTH = '" + DateTime.Now.AddMonths(-1).ToString("yyyyMM") + "' " + "\n");
            strSqlString.Append("                           AND OPER " + cdvStep.SelectedValueToQueryString + "\n");
            strSqlString.Append("                         GROUP BY OPER, RES_MODEL " + "\n");
            strSqlString.Append("                         ORDER BY OPER " + "\n");
            strSqlString.Append("                       ) " + "\n");
            strSqlString.Append("                 GROUP BY OPER " + "\n");
            strSqlString.Append("               ) B " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("           AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND MAT.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = A.MAT_ID " + "\n");
            strSqlString.Append("           AND A.OPER = B.OPER(+) " + "\n");

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

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion

            strSqlString.Append("         GROUP BY " + QueryCond3 + " " + "\n");
            strSqlString.Append("         ORDER BY " + QueryCond4 + " " + "\n");
            strSqlString.Append("       ) " + "\n");


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

        #endregion

        #region EventHandler

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (CheckField() == false) return;

            GridColumnInit();
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

                /*
                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                
                //spdData.DataSource = dt;
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 14, null, null, btnSort);

                spdData.RPT_FillDataSelectiveCells("Total", 0, 14, 0, 1, true, Align.Center, VerticalAlign.Center);
                //spdData.Sheets[0].FrozenColumnCount = 3;
                */
                spdData.DataSource = dt;
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
            }
        }

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();            
        }

        private void SetStep()
        {

            this.SetFactory(cdvFactory.txtValue);
            cdvStep.sFactory = cdvFactory.txtValue;

            cdvStep.SetChangedFlag(true);
            cdvStep.Text = "";
            string strQuery = string.Empty;

            strQuery += "SELECT DISTINCT OPR.OPER CODE, OPR.OPER_DESC DATA" + "\n";
            strQuery += "  FROM MRASRESMFO RES, MWIPOPRDEF OPR " + "\n";
            strQuery += " WHERE RES.FACTORY='" + cdvFactory.txtValue + "' " + "\n";
            strQuery += "   AND REL_LEVEL='R' " + "\n";

            if (cdvFactory.txtValue.Equals(GlobalVariable.gsAssyDefaultFactory))
            {
                strQuery += "   AND RES.OPER NOT IN ('A0100','A0150','A0250','A0320','A0330','A0340','A0350','A0370','A0380','A0390','A0500','A0501',";
                strQuery += "'A0502','A0503','A0504','A0505','A0506','A0507','A0508','A0509','A0550','A0950','A1100','A1110','A1180','A1230','A1720','A1950', 'A2050', 'A2100')" + "\n";
                strQuery += "   AND OPR.OPER LIKE 'A%'" + "\n";
            }
            else
            {
                strQuery += "   AND RES.OPER IN ('T0100','T0650','T0900','T1040','T1080','T1200')" + "\n";
            }

            strQuery += "   AND RES.FACTORY=OPR.FACTORY " + "\n";
            strQuery += "   AND RES.OPER=OPR.OPER " + "\n";
            strQuery += "ORDER BY OPR.OPER " + "\n";

            if (cdvFactory.txtValue != "")
                cdvStep.sDynamicQuery = strQuery;
            else
                cdvStep.sDynamicQuery = "";
        }

        #endregion

    }
}
