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
    public partial class PRD010805 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {           
        string[] selectDate = null;
        /// <summary>
        /// 클  래  스: PRD010805<br/>
        /// 클래스요약: 공정별 필요 인력 조회<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2014-09-26<br/>
        /// 상세  설명: 공정별 필요 인력 조회<br/>
        /// 변경  내용: <br/>         
        /// 2016-12-13-임종우 : 10단 칩 이상 스택 수 오류 수정
        /// 2018-09-05-임종우 : 근무조(4) -> 근무조(3) 으로 변경 (배진우차장 요청)
        ///                   : HRTDMCPROUT -> RWIPMCPBOM 으로 변경
        /// 2020-04-28-김미경 : 제품 TYPE(STACK) 추가 반영 (이승희 D)
        /// </summary>
        public PRD010805()
        {
            InitializeComponent();
            cdvFromToDate.DaySelector.SelectedValue = "MONTH";
            cdvFromToDate.AutoBindingUserSetting(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));                        
            cdvFromToDate.FromDate.Visible = false;
            cdvFromToDate.ToDate.Visible = false;

            SortInit();
            GridColumnInit();
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
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
            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "DAY")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD045", GlobalVariable.gcLanguage));
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
            selectDate = cdvFromToDate.getSelectDate();

            try
            {
                spdData.RPT_AddBasicColumn("Team in charge", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("파트", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Operation", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Operation Description", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("UPH", 0, 4, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("the number per person", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("UPOH", 0, 6, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("OP_EFFICE", 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Operation Yield", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);

                for (int i = 0; i < selectDate.Length; i++)
                {
                    spdData.RPT_AddBasicColumn(selectDate[i].ToString(), 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Time required per person", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 100);
                    spdData.RPT_AddBasicColumn("Necessary manpower", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnHeader.Columns.Count - 2, 2);                    
                }


                for (int i = 0; i <= 8; i++)
                {
                    spdData.RPT_MerageHeaderRowSpan(0, i, 2);
                }

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
            string sStart = null;
            string sEnd = null;
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                sStart = cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM");
                sEnd = cdvFromToDate.ToYearMonth.Value.ToString("yyyyMM");
            }
            else
            {
                sStart = cdvFromToDate.HmFromWeek;
                sEnd = cdvFromToDate.HmToWeek;
            }

            strSqlString.Append("SELECT A.KEY_1, A.KEY_2, A.OPER, A.OPER_DESC" + "\n");
            strSqlString.Append("     , ROUND(AVG(A.UPEH), 0) AS UPEH" + "\n");
            strSqlString.Append("     , A.USER_CAPA" + "\n");
            strSqlString.Append("     , ROUND(AVG(A.UPOH), 0) AS UPOH" + "\n");
            strSqlString.Append("     , A.OP_EFFICE, A.YIELD" + "\n");

            for (int i = 0; i < selectDate.Length; i++)
            {
                strSqlString.Append("     , ROUND(SUM(CASE WHEN B.PLAN_DATE = '" + selectDate[i].ToString() + "' THEN A.K_TIME * B.PLAN_QTY ELSE 0 END), 2) AS HUMAN_TIME_" + i + "\n");
                strSqlString.Append("     , ROUND(SUM(CASE WHEN B.PLAN_DATE = '" + selectDate[i].ToString() + "' THEN (A.K_TIME * B.PLAN_QTY) / B.WORK_DAY / 24 * 3 ELSE 0 END), 2) AS HUMAN_NEED_" + i + "\n");
            }
       
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT KEY_1, KEY_2, KEY_3 AS OPER, DATA_1 AS OPER_DESC, DATA_3 AS USER_CAPA" + "\n");
            strSqlString.Append("             , DATA_4 AS OP_EFFICE, DATA_5 AS YIELD" + "\n");
            strSqlString.Append("             , B.MAT_GRP_1, B.MAT_GRP_5, B.PKG_CODE" + "\n");
            strSqlString.Append("             , AVG(B.UPEH) AS UPEH, AVG(B.UPEH * DATA_3) AS UPOH" + "\n");
            strSqlString.Append("             , AVG(CASE WHEN DATA_3 = ' ' OR DATA_4 = ' ' OR DATA_5 = ' ' THEN 0" + "\n");
            strSqlString.Append("                        WHEN B.UPEH IS NULL OR B.UPEH = 0 THEN 1000 / (B.OPER_AVG_UPEH * DATA_3 * (DATA_4/100) * (DATA_5/100)) * 1.15" + "\n");
            strSqlString.Append("                        ELSE 1000 / (B.UPEH * DATA_3 * (DATA_4/100) * (DATA_5/100)) * 1.15" + "\n");
            strSqlString.Append("                   END) AS K_TIME" + "\n");
            strSqlString.Append("             , AVG(B.OPER_AVG_UPEH) AS OPER_AVG_UPEH" + "\n");
            strSqlString.Append("          FROM MGCMTBLDAT A" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT A.FACTORY, A.MAT_ID, A.OPER, A.UPEH, B.MAT_GRP_1, B.MAT_GRP_5, B.MAT_CMF_11" + "\n");
            strSqlString.Append("                     , CASE WHEN B.MAT_GRP_1 = 'HX' THEN B.MAT_ID ELSE B.MAT_CMF_11 END AS PKG_CODE" + "\n");
            strSqlString.Append("                     , AVG(A.UPEH) OVER(PARTITION BY A.OPER) AS OPER_AVG_UPEH" + "\n");
            strSqlString.Append("                  FROM CRASUPHDEF A" + "\n");
            strSqlString.Append("                     , MWIPMATDEF B" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                   AND B.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND B.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("               ) B" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND A.FACTORY = B.FACTORY(+)" + "\n");
            strSqlString.Append("           AND A.KEY_3 = B.OPER(+)" + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("           AND A.TABLE_NAME = 'H_RPT_HUMAN'" + "\n");
            strSqlString.Append("           AND A.DATA_2 = 'Y'" + "\n");
            strSqlString.Append("         GROUP BY KEY_1, KEY_2, KEY_3, DATA_1, DATA_3, DATA_4, DATA_5, B.MAT_GRP_1, B.MAT_GRP_5, PKG_CODE" + "\n");
            strSqlString.Append("       ) A" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT PLAN_DATE, PKG_CODE, OPER, MAT_GRP_1, MAT_GRP_5, WORK_DAY" + "\n");
            strSqlString.Append("             , SUM(PLAN_QTY) AS PLAN_QTY" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT B.PLAN_DATE, B.MAT_KEY, B.MAT_ID, C.OPER, TO_NUMBER(PLAN_QTY) AS PLAN_QTY" + "\n");
            strSqlString.Append("                     , CASE WHEN A.MAT_GRP_1 = 'HX' THEN A.MAT_ID ELSE A.MAT_CMF_11 END AS PKG_CODE" + "\n");
            strSqlString.Append("                     , MAT_GRP_1, MAT_GRP_2, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6, MAT_CMF_11, FIRST_FLOW" + "\n");
            strSqlString.Append("                     , NET_DIE, COMP_CNT, HX_COMP_MIN, HX_COMP_MAX" + "\n");
            strSqlString.Append("                     , CASE WHEN MAT_GRP_4 IN ('-', 'FD', 'FU') THEN '1'" + "\n");
            strSqlString.Append("                            WHEN MAT_GRP_4 IN ('DDP') THEN '2'" + "\n");
            strSqlString.Append("                            WHEN MAT_GRP_4 IN ('QDP') THEN '4'" + "\n");
            strSqlString.Append("                            WHEN MAT_GRP_4 IN ('ODP') THEN '8'" + "\n");
            strSqlString.Append("                            ELSE REGEXP_REPLACE(MAT_GRP_4, '[^[:digit:]]')" + "\n");
            strSqlString.Append("                       END AS STACK" + "\n");
            //strSqlString.Append("                     , CASE WHEN MAT_GRP_4 IN ('-', 'FD', 'FU') THEN '1'" + "\n");
            //strSqlString.Append("                            ELSE SUBSTR(MAT_GRP_4, 3) " + "\n");
            //strSqlString.Append("                       END AS STACK" + "\n");        

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                strSqlString.Append("                     , TO_CHAR(LAST_DAY(TO_DATE(B.PLAN_DATE, 'YYYYMM')), 'DD') AS WORK_DAY" + "\n");
                strSqlString.Append("                  FROM VWIPMATDEF A" + "\n");
                strSqlString.Append("                     , (" + "\n");
                strSqlString.Append("                        SELECT DISTINCT A.PLAN_MONTH AS PLAN_DATE" + "\n");
                strSqlString.Append("                             , CASE WHEN B.MAT_ID IS NULL THEN A.MAT_ID ELSE B.MCP_TO_PART END AS MAT_KEY" + "\n");
                strSqlString.Append("                             , CASE WHEN B.MAT_ID IS NULL THEN A.MAT_ID ELSE B.MAT_ID END AS MAT_ID" + "\n");
                strSqlString.Append("                             , A.RESV_FIELD1 / 1000 AS PLAN_QTY" + "\n");
                strSqlString.Append("                          FROM CWIPPLNMON A" + "\n");
                strSqlString.Append("                             , RWIPMCPBOM B" + "\n");
                strSqlString.Append("                         WHERE 1=1   " + "\n");
                strSqlString.Append("                           AND A.FACTORY = B.FACTORY(+)" + "\n");
                strSqlString.Append("                           AND A.MAT_ID = B.MCP_TO_PART(+)" + "\n");
                strSqlString.Append("                           AND A.FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                           AND A.PLAN_MONTH BETWEEN '" + sStart + "' AND '" + sEnd + "'" + "\n");
                strSqlString.Append("                           AND DECODE(A.RESV_FIELD1, ' ', 0, A.RESV_FIELD1) > 0  " + "\n");
            }
            else
            {
                strSqlString.Append("                     , 7 AS WORK_DAY" + "\n");
                strSqlString.Append("                  FROM VWIPMATDEF A" + "\n");
                strSqlString.Append("                     , (" + "\n");
                strSqlString.Append("                        SELECT DISTINCT A.PLAN_WEEK AS PLAN_DATE" + "\n");
                strSqlString.Append("                             , CASE WHEN B.MAT_ID IS NULL THEN A.MAT_ID ELSE B.MCP_TO_PART END AS MAT_KEY" + "\n");
                strSqlString.Append("                             , CASE WHEN B.MAT_ID IS NULL THEN A.MAT_ID ELSE B.MAT_ID END AS MAT_ID" + "\n");
                strSqlString.Append("                             , A.WW_QTY / 1000 AS PLAN_QTY" + "\n");
                strSqlString.Append("                          FROM RWIPPLNWEK A" + "\n");
                strSqlString.Append("                             , RWIPMCPBOM B" + "\n");
                strSqlString.Append("                         WHERE 1=1   " + "\n");
                strSqlString.Append("                           AND A.FACTORY = B.FACTORY(+)" + "\n");
                strSqlString.Append("                           AND A.MAT_ID = B.MCP_TO_PART(+)" + "\n");
                strSqlString.Append("                           AND A.FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                           AND A.PLAN_WEEK BETWEEN '" + sStart + "' AND '" + sEnd + "'" + "\n");
                strSqlString.Append("                           AND A.GUBUN = '3'  " + "\n");
            }
            
            strSqlString.Append("                       ) B " + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT DISTINCT SUBSTR(PLNNR, 1, 4) AS FLOW, KTSCH AS OPER" + "\n");
            strSqlString.Append("                          FROM ZHPPT120@SAPREAL" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND ARBPL != 'DUMMY'" + "\n");
            strSqlString.Append("                       ) C   " + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                   AND A.FIRST_FLOW = C.FLOW(+)" + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                   AND A.DELETE_FLAG = ' ' " + "\n");

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                   AND A.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                   AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                   AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                   AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                   AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                   AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                   AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                   AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                   AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY PLAN_DATE, PKG_CODE, OPER, MAT_GRP_1, MAT_GRP_5, WORK_DAY" + "\n");
            strSqlString.Append("       ) B" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND A.PKG_CODE = B.PKG_CODE" + "\n");
            strSqlString.Append("   AND A.OPER = B.OPER" + "\n");
            strSqlString.Append("   AND A.MAT_GRP_1 = B.MAT_GRP_1" + "\n");
            strSqlString.Append("   AND A.MAT_GRP_5 = B.MAT_GRP_5" + "\n");


            if (cdvTeam.Text != "ALL" && cdvTeam.Text != "")
            {
                strSqlString.Append("   AND A.KEY_1 " + cdvTeam.SelectedValueToQueryString + "\n");
            }

            if (cdvPart.Text != "ALL" && cdvPart.Text != "")
            {
                strSqlString.Append("   AND A.KEY_2 " + cdvPart.SelectedValueToQueryString + "\n");
            }

            if (cdvOper.Text != "ALL" && cdvOper.Text != "")
            {
                strSqlString.Append("   AND A.OPER " + cdvOper.SelectedValueToQueryString + "\n");
            }

            strSqlString.Append("GROUP BY A.KEY_1, A.KEY_2, A.OPER, A.OPER_DESC, A.USER_CAPA, A.OP_EFFICE, A.YIELD" + "\n");
            strSqlString.Append("ORDER BY A.KEY_1, A.KEY_2, A.OPER, A.OPER_DESC, A.USER_CAPA, A.OP_EFFICE, A.YIELD" + "\n");

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
                strSqlString.Append("   AND TABLE_NAME = 'H_RPT_HUMAN' " + "\n");
                strSqlString.Append(" ORDER BY Code" + "\n");
            }
            else if (type == "PART")
            {
                strSqlString.Append("SELECT DISTINCT KEY_2 AS Code, '' AS Data " + "\n");
                strSqlString.Append("  FROM MGCMTBLDAT" + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("   AND TABLE_NAME = 'H_RPT_HUMAN' " + "\n");
                strSqlString.Append(" ORDER BY Code" + "\n");
            }
            else
            {
                strSqlString.Append("SELECT DISTINCT KEY_3 AS Code, DATA_1 AS Data " + "\n");
                strSqlString.Append("  FROM MGCMTBLDAT" + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("   AND TABLE_NAME = 'H_RPT_HUMAN' " + "\n");
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

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 2, 9, null, null, btnSort);
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 4, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);                

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
            //this.SetFactory(cdvFactory.txtValue);
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
