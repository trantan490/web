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
    public partial class PQC030302 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        string[] sQcType = new string[] { "부적합처리", "CCS(변경점)", "GATE", "PPQ", "시양산(LVM)", "설비인증", "정비검사", "변경점검사", "부적합검사", "간이신뢰성", "DOE(ER)", "순회검사", "도금수입검사" };
        /// <summary>
        /// 클  래  스: PQC030302<br/>
        /// 클래스요약: 공정별 품질 인력 산출<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2014-12-03<br/>
        /// 상세  설명: 공정별 품질 인력 산출(최재주D)<br/>
        /// 변경  내용: <br/>         
        /// </summary>
        public PQC030302()
        {
            InitializeComponent();
            //cdvFromToDate.DaySelector.SelectedValue = "MONTH";
            cdvFromToDate.AutoBindingUserSetting(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));  

            SortInit();
            GridColumnInit();

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;             
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            //if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "DAY")
            //{
            //    CmnFunction.ShowMsgBox("월간 / 주간 조회만 가능합니다.");
            //    return false;
            //}

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
                if (cdvFactory.Text.Trim() == "HMKB1")
                {                    
                    spdData.RPT_AddBasicColumn("Operation", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Operation Description", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                    spdData.RPT_AddBasicColumn("BF TOTAL", 0, 2, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Double2, 70);

                    for (int i = 0; i < sQcType.Length; i++)
                    {
                        spdData.RPT_AddBasicColumn(sQcType[i].ToString(), 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Number", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("time", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("BF", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnHeader.Columns.Count - 3, 3);
                    }

                    spdData.RPT_AddBasicColumn("registration date", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("registrant", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);

                    for (int i = 0; i <= 2; i++)
                    {
                        spdData.RPT_MerageHeaderRowSpan(0, i, 2);
                    }

                    spdData.RPT_MerageHeaderRowSpan(0, (sQcType.Length * 3) + 3, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, (sQcType.Length * 3) + 4, 2);

                    spdData.RPT_ColumnConfigFromTable(btnSort);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Team in charge", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("파트", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Operation", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Operation Description", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                    spdData.RPT_AddBasicColumn("BF TOTAL", 0, 4, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Double2, 70);

                    for (int i = 0; i < sQcType.Length; i++)
                    {
                        spdData.RPT_AddBasicColumn(sQcType[i].ToString(), 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Number", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("time", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("BF", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);
                        spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnHeader.Columns.Count - 3, 3);
                    }

                    spdData.RPT_AddBasicColumn("registration date", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("registrant", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);

                    for (int i = 0; i <= 4; i++)
                    {
                        spdData.RPT_MerageHeaderRowSpan(0, i, 2);
                    }

                    spdData.RPT_MerageHeaderRowSpan(0, (sQcType.Length * 3) + 5, 2);
                    spdData.RPT_MerageHeaderRowSpan(0, (sQcType.Length * 3) + 6, 2);

                    spdData.RPT_ColumnConfigFromTable(btnSort);
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
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE", "CUST_TYPE", "CUST_TYPE", "DECODE(CUST_TYPE, 'SEC', 1, 'Hynix', 2, 'Fabless', 3, 4)", "CUST_TYPE", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", "MAT_GRP_1", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "MAT_GRP_9", "MAT_GRP_9 AS MAJOR_CODE", "MAT_GRP_9", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT_GRP_2", "MAT_GRP_2 AS FAMILY", "MAT_GRP_2", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT_GRP_10", "MAT_GRP_10 AS PACKAGE", "MAT_GRP_10", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT_GRP_4", "MAT_GRP_4 AS TYPE1", "MAT_GRP_4", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT_GRP_5", "MAT_GRP_5 AS TYPE2", "MAT_GRP_5", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT_GRP_6", "MAT_GRP_6 AS LD_COUNT", "MAT_GRP_6", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "MAT_CMF_11", "MAT_CMF_11 AS PKG_CODE", "MAT_CMF_11", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT_GRP_7", "MAT_GRP_7 AS DENSITY", "MAT_GRP_7", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT_GRP_8", "MAT_GRP_8 AS GENERATION", "MAT_GRP_8", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT_CMF_10", "MAT_CMF_10 AS PIN_TYPE", "MAT_CMF_10", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT_ID", "MAT_ID AS PRODUCT", "MAT_ID", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("SAP CODE", "VENDOR_ID", "VENDOR_ID AS SAP_CODE", "VENDOR_ID", false);            
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
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                strSqlString.Append("SELECT A.OPER, A.OPER_DESC, 0 AS TTL_BF" + "\n");

                for (int i = 1; i <= sQcType.Length; i++)
                {
                    strSqlString.AppendFormat("     , B.QC_{0}, A.QC_TYPE_{0}, ROUND((B.QC_{0} * A.QC_TYPE_{0}) / (27000 * 3), 2) AS QC_TTL_{0}" + "\n", i);
                }

                strSqlString.Append("     , A.UPDATE_TIME" + "\n");
                strSqlString.Append("     , (SELECT USER_DESC || '(' || USER_ID || ')' FROM RWEBUSRDEF WHERE USER_ID = A.UPDATE_USER_ID) AS UPDATE_USER_ID" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT KEY_1 AS OPER, KEY_2 AS OPER_DESC" + "\n");
                strSqlString.Append("             , DECODE(KEY_3, ' ', 0, KEY_3) AS QC_TYPE_1, DECODE(KEY_4, ' ', 0, KEY_4) AS QC_TYPE_2" + "\n");
                strSqlString.Append("             , DECODE(KEY_5, ' ', 0, KEY_5) AS QC_TYPE_3, DECODE(KEY_6, ' ', 0, KEY_6) AS QC_TYPE_4" + "\n");
                strSqlString.Append("             , DECODE(KEY_7, ' ', 0, KEY_7) AS QC_TYPE_5, DECODE(KEY_8, ' ', 0, KEY_8) AS QC_TYPE_6" + "\n");
                strSqlString.Append("             , DECODE(KEY_9, ' ', 0, KEY_9) AS QC_TYPE_7, DECODE(KEY_10, ' ', 0, KEY_10) AS QC_TYPE_8" + "\n");
                strSqlString.Append("             , DECODE(DATA_1, ' ', 0, DATA_1) AS QC_TYPE_9, DECODE(DATA_2, ' ', 0, DATA_2) AS QC_TYPE_10" + "\n");
                strSqlString.Append("             , DECODE(DATA_3, ' ', 0, DATA_3) AS QC_TYPE_11, DECODE(DATA_4, ' ', 0, DATA_4) AS QC_TYPE_12" + "\n");
                strSqlString.Append("             , DECODE(DATA_5, ' ', 0, DATA_5) AS QC_TYPE_13" + "\n");
                strSqlString.Append("             , DECODE(UPDATE_TIME, ' ', CREATE_TIME, UPDATE_TIME) AS UPDATE_TIME" + "\n");
                strSqlString.Append("             , DECODE(UPDATE_USER_ID, ' ', CREATE_USER_ID, UPDATE_USER_ID) AS UPDATE_USER_ID" + "\n");
                strSqlString.Append("          FROM MGCMTBLDAT" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("           AND TABLE_NAME = 'H_RPT_HUMAN_QC'" + "\n");
                strSqlString.Append("       ) A" + "\n");

                strSqlString.Append("     , (" + "\n");
                strSqlString.Append("        SELECT OPER" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, '부적합처리', 1, 0)) AS QC_1" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, 'CCS(변경점)', 1, 0)) AS QC_2" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, 'GATE', 1, 0)) AS QC_3" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, 'PPQ', 1, 0)) AS QC_4" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, '시양산(LVM)', 1, 0)) AS QC_5" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, '설비인증', 1, 0)) AS QC_6" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, '정비검사', 1, 0)) AS QC_7" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, 'DOE', 1, 0)) AS QC_8" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, '순회검사', 1, 0)) AS QC_9" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, '변경점검사', 1, 0)) AS QC_10" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, '부적합검사', 1, 0)) AS QC_11" + "\n");                
                strSqlString.Append("             , SUM(1) AS TTL_CNT" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT MAT_ID, OPER, QC_TYPE" + "\n");
                strSqlString.Append("                  FROM CPQCLOTHIS@RPTTOMES" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                   AND HIST_DEL_FLAG = ' '" + "\n");
                strSqlString.Append("                   AND TRAN_TIME BETWEEN '" + cdvFromToDate.ExactFromDate + "' AND '" + cdvFromToDate.ExactToDate + "' " + "\n");                               
                strSqlString.Append("                 UNION ALL" + "\n");
                strSqlString.Append("                SELECT MAT_ID, OPER, '부적합처리' AS QC_TYPE " + "\n");
                strSqlString.Append("                  FROM CQCMABNMNG@RPTTOMES" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");                                
                strSqlString.Append("                   AND CREATE_TIME BETWEEN '" + cdvFromToDate.ExactFromDate + "' AND '" + cdvFromToDate.ExactToDate + "' " + "\n");
                strSqlString.Append("               )" + "\n");
                strSqlString.Append("         WHERE QC_TYPE IN ('부적합처리','CCS(변경점)','GATE','PPQ','시양산(LVM)','설비인증','정비검사','DOE','순회검사','변경점검사','부적합검사')" + "\n");

                if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                    strSqlString.AppendFormat("           AND MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);

                strSqlString.Append("         GROUP BY OPER" + "\n");
                strSqlString.Append("       ) B" + "\n");
                strSqlString.Append(" WHERE 1=1" + "\n");
                strSqlString.Append("   AND A.OPER = B.OPER(+)" + "\n");

                if (cdvOper.Text != "ALL" && cdvOper.Text != "")
                {
                    strSqlString.Append("   AND A.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                }

                strSqlString.Append("ORDER BY A.OPER, A.OPER_DESC" + "\n");
            }
            else
            {
                strSqlString.Append("SELECT A.TEAM, A.PART, A.OPER, A.OPER_DESC, 0 AS TTL_BF" + "\n");

                for (int i = 1; i <= sQcType.Length; i++)
                {
                    strSqlString.AppendFormat("     , B.QC_{0}, A.QC_TYPE_{0}, ROUND((B.QC_{0} * A.QC_TYPE_{0}) / (27000 * 3), 2) AS QC_TTL_{0}" + "\n", i);
                }

                strSqlString.Append("     , A.UPDATE_TIME" + "\n");
                strSqlString.Append("     , (SELECT USER_DESC || '(' || USER_ID || ')' FROM RWEBUSRDEF WHERE USER_ID = A.UPDATE_USER_ID) AS UPDATE_USER_ID" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT KEY_1 AS TEAM, KEY_2 AS PART, KEY_3 AS OPER, KEY_4 AS OPER_DESC" + "\n");
                strSqlString.Append("             , DECODE(KEY_5, ' ', 0, KEY_5) AS QC_TYPE_1, DECODE(KEY_6, ' ', 0, KEY_6) AS QC_TYPE_2" + "\n");
                strSqlString.Append("             , DECODE(KEY_7, ' ', 0, KEY_7) AS QC_TYPE_3, DECODE(KEY_8, ' ', 0, KEY_8) AS QC_TYPE_4" + "\n");
                strSqlString.Append("             , DECODE(KEY_9, ' ', 0, KEY_9) AS QC_TYPE_5, DECODE(KEY_10, ' ', 0, KEY_10) AS QC_TYPE_6" + "\n");
                strSqlString.Append("             , DECODE(DATA_1, ' ', 0, DATA_1) AS QC_TYPE_7, DECODE(DATA_2, ' ', 0, DATA_2) AS QC_TYPE_8" + "\n");
                strSqlString.Append("             , DECODE(DATA_3, ' ', 0, DATA_3) AS QC_TYPE_9, DECODE(DATA_4, ' ', 0, DATA_4) AS QC_TYPE_10" + "\n");
                strSqlString.Append("             , DECODE(DATA_5, ' ', 0, DATA_5) AS QC_TYPE_11, DECODE(DATA_6, ' ', 0, DATA_6) AS QC_TYPE_12" + "\n");
                strSqlString.Append("             , DECODE(DATA_7, ' ', 0, DATA_7) AS QC_TYPE_13" + "\n");
                strSqlString.Append("             , DECODE(UPDATE_TIME, ' ', CREATE_TIME, UPDATE_TIME) AS UPDATE_TIME" + "\n");
                strSqlString.Append("             , DECODE(UPDATE_USER_ID, ' ', CREATE_USER_ID, UPDATE_USER_ID) AS UPDATE_USER_ID" + "\n");
                strSqlString.Append("          FROM MGCMTBLDAT" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("           AND TABLE_NAME = 'H_RPT_HUMAN_QC'" + "\n");
                strSqlString.Append("       ) A" + "\n");
                strSqlString.Append("     , (" + "\n");
                strSqlString.Append("        SELECT OPER" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, '부적합처리', 1, 0)) AS QC_1" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, 'CCS(변경점)', 1, 0)) AS QC_2" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, 'GATE', 1, 0)) AS QC_3" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, 'PPQ', 1, 0)) AS QC_4" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, '시양산(LVM)', 1, 0)) AS QC_5" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, '설비인증', 1, 0)) AS QC_6" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, '정비검사', 1, 0)) AS QC_7" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, '변경점검사', 1, 0)) AS QC_8" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, '부적합검사', 1, 0)) AS QC_9" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, '간이신뢰성', 1, 0)) AS QC_10" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, 'DOE(ER)', 1, 0)) AS QC_11" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, '순회검사', 1, 0)) AS QC_12" + "\n");
                strSqlString.Append("             , SUM(DECODE(QC_TYPE, '도금수입검사', 1, 0)) AS QC_13" + "\n");
                strSqlString.Append("             , SUM(1) AS TTL_CNT" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT MAT_ID, OPER, QC_TYPE" + "\n");
                strSqlString.Append("                  FROM CPQCLOTHIS@RPTTOMES" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                   AND HIST_DEL_FLAG = ' '" + "\n");
                strSqlString.Append("                   AND TRAN_TIME BETWEEN '" + cdvFromToDate.ExactFromDate + "' AND '" + cdvFromToDate.ExactToDate + "' " + "\n");
                strSqlString.Append("                   AND CMF_9 = 'VISUAL'" + "\n");
                strSqlString.Append("                 UNION ALL" + "\n");
                strSqlString.Append("                SELECT MAT_ID, OPER_1, '부적합처리' AS QC_TYPE " + "\n");
                strSqlString.Append("                  FROM CABRLOTSTS@RPTTOMES" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                   AND DEFECT_CODE <> 'Q033'" + "\n");
                strSqlString.Append("                   AND STEP10_TIME BETWEEN '" + cdvFromToDate.ExactFromDate + "' AND '" + cdvFromToDate.ExactToDate + "' " + "\n");
                strSqlString.Append("               )" + "\n");
                strSqlString.Append("         WHERE QC_TYPE IN ('부적합처리','CCS(변경점)','GATE','PPQ','시양산(LVM)','설비인증','정비검사','변경점검사','부적합검사','간이신뢰성','DOE(ER)','순회검사','도금수입검사')" + "\n");

                if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                    strSqlString.AppendFormat("           AND MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);

                strSqlString.Append("         GROUP BY OPER" + "\n");
                strSqlString.Append("       ) B" + "\n");
                strSqlString.Append(" WHERE 1=1" + "\n");
                strSqlString.Append("   AND A.OPER = B.OPER(+)" + "\n");

                if (cdvTeam.Text != "ALL" && cdvTeam.Text != "")
                {
                    strSqlString.Append("   AND A.TEAM " + cdvTeam.SelectedValueToQueryString + "\n");
                }

                if (cdvPart.Text != "ALL" && cdvPart.Text != "")
                {
                    strSqlString.Append("   AND A.PART " + cdvPart.SelectedValueToQueryString + "\n");
                }

                if (cdvOper.Text != "ALL" && cdvOper.Text != "")
                {
                    strSqlString.Append("   AND A.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                }

                strSqlString.Append("ORDER BY A.PART, A.OPER, A.OPER_DESC" + "\n");
            }
                        
            //상세 조회에 따른 SQL문 생성                        
            //if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
            //    strSqlString.AppendFormat("                   AND A.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            //if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
            //    strSqlString.AppendFormat("                   AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            //if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
            //    strSqlString.AppendFormat("                   AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            //if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
            //    strSqlString.AppendFormat("                   AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            //if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
            //    strSqlString.AppendFormat("                   AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            //if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
            //    strSqlString.AppendFormat("                   AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            //if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
            //    strSqlString.AppendFormat("                   AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            //if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
            //    strSqlString.AppendFormat("                   AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            //if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
            //    strSqlString.AppendFormat("                   AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
                
        private string MakeSqlString1(string type) // 검색 조건 LIST
        {
            StringBuilder strSqlString = new StringBuilder();
                        
            if (type == "TEAM")
            {
                strSqlString.Append("SELECT DISTINCT KEY_1 AS Code, '' AS Data " + "\n");
                strSqlString.Append("  FROM MGCMTBLDAT" + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("   AND TABLE_NAME = 'H_RPT_HUMAN_QC' " + "\n");
                strSqlString.Append(" ORDER BY Code" + "\n");
            }
            else if (type == "PART")
            {
                strSqlString.Append("SELECT DISTINCT KEY_2 AS Code, '' AS Data " + "\n");
                strSqlString.Append("  FROM MGCMTBLDAT" + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("   AND TABLE_NAME = 'H_RPT_HUMAN_QC' " + "\n");
                strSqlString.Append(" ORDER BY Code" + "\n");
            }
            else
            {
                if (cdvFactory.Text.Trim() == "HMKB1")
                {
                    strSqlString.Append("SELECT DISTINCT KEY_1 AS Code, KEY_2 AS Data " + "\n");
                }
                else
                {
                    strSqlString.Append("SELECT DISTINCT KEY_3 AS Code, KEY_4 AS Data " + "\n");
                }
                
                strSqlString.Append("  FROM MGCMTBLDAT" + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("   AND TABLE_NAME = 'H_RPT_HUMAN_QC' " + "\n");
                strSqlString.Append(" ORDER BY Code" + "\n");
                
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

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                if (cdvFactory.Text.Trim() == "HMKB1")
                {
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 2, null, null);

                    //Total부분 셀머지
                    spdData.RPT_FillDataSelectiveCells("Total", 0, 2, 0, 1, true, Align.Center, VerticalAlign.Center);

                    spdData.RPT_AutoFit(false);

                    int iBFTTLCol = 2;

                    // BF TTL
                    for (int r = 0; r < spdData.ActiveSheet.Rows.Count; r++)
                    {
                        double iBFTTL = 0;

                        for (int i = iBFTTLCol + 3; i <= (sQcType.Length * 3) + iBFTTLCol; i = i + 3)
                        {
                            iBFTTL = iBFTTL + Convert.ToDouble(spdData.ActiveSheet.Cells[r, i].Value);
                        }

                        if (iBFTTL == 0)
                        {
                            spdData.ActiveSheet.Cells[r, iBFTTLCol].Value = null;
                        }
                        else
                        {
                            spdData.ActiveSheet.Cells[r, iBFTTLCol].Value = iBFTTL;
                        }
                    }
                }
                else
                {
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 2, 4, null, null, btnSort);
                    //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                    //Total부분 셀머지
                    spdData.RPT_FillDataSelectiveCells("Total", 0, 4, 0, 1, true, Align.Center, VerticalAlign.Center);

                    spdData.RPT_AutoFit(false);

                    int iBFTTLCol = 4;

                    // BF TTL
                    for (int r = 0; r < spdData.ActiveSheet.Rows.Count; r++)
                    {
                        double iBFTTL = 0;

                        for (int i = iBFTTLCol + 3; i <= (sQcType.Length * 3) + iBFTTLCol; i = i + 3)
                        {
                            iBFTTL = iBFTTL + Convert.ToDouble(spdData.ActiveSheet.Cells[r, i].Value);
                        }

                        if (iBFTTL == 0)
                        {
                            spdData.ActiveSheet.Cells[r, iBFTTLCol].Value = null;
                        }
                        else
                        {
                            spdData.ActiveSheet.Cells[r, iBFTTLCol].Value = iBFTTL;
                        }
                    }
                }

                //spdData.DataSource = dt;
                
                dt.Dispose();
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
            if (spdData.ActiveSheet.Rows.Count > 0)
            {
                //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null, true);
                spdData.ExportExcel();            
            }
        }

        /// <summary>
        /// Factory 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                cdvTeam.Visible = false;
                cdvPart.Visible = false;
                cdvOper.Location = new System.Drawing.Point(204, 2);

                sQcType = new string[] { "부적합처리", "CCS(변경점)", "GATE", "PPQ", "시양산(LVM)", "설비인증", "정비검사", "DOE", "순회검사", "변경점검사", "부적합검사" };
            }
            else
            {
                cdvTeam.Visible = true;
                cdvPart.Visible = true;
                cdvOper.Location = new System.Drawing.Point(604, 2);
                
                sQcType = new string[] { "부적합처리", "CCS(변경점)", "GATE", "PPQ", "시양산(LVM)", "설비인증", "정비검사", "변경점검사", "부적합검사", "간이신뢰성", "DOE(ER)", "순회검사", "도금수입검사" };
            }

            this.SetFactory(cdvFactory.txtValue);
            cdvOper.sFactory = cdvFactory.txtValue;
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }

        #endregion

        private void cdvTeam_ValueButtonPress(object sender, EventArgs e)
        {
            cdvTeam.sDynamicQuery = MakeSqlString1("TEAM");
        }

        private void cdvPart_ValueButtonPress(object sender, EventArgs e)
        {
            cdvPart.sDynamicQuery = MakeSqlString1("PART");
        }

        private void cdvOper_ValueButtonPress(object sender, EventArgs e)
        {
            cdvOper.sDynamicQuery = MakeSqlString1("OPER");
        }

    }
}
