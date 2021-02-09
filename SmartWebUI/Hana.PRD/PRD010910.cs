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
    public partial class PRD010910 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010910<br/>
        /// 클래스요약: Plan Summary<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2014-07-11<br/>
        /// 상세  설명: Plan Summary<br/>
        /// 변경  내용: <br/>
        /// 2014-10-17-임종우 : FLOW 정보 SAP 정보로 변경(임태성K 요청) - 특정구간에 WITH 문 사용하면 Wire 수량만 데이터 집계가 이상하여 풀어서 사용함... 원인은 모르겠음.
        /// 2014-11-03-임종우 : 특정 구간 WITH문 풀어서 사용 -> 전체 구간 WITH문 풀어서 사용.
        /// 2014-11-06-임종우 : SAW 공정 기준 A0200으로 변경 (임태성K 요청)
        /// 2016-01-28-임종우 : C-MOLD 기준 MAT_ID 까지 비교하도록 수정 (김성업D 요청)
        /// 2016-12-06-임종우 : 10단 칩 이상 스택 수 오류 수정
        /// 2017-10-16-임종우 : MCP Combine rule 테이블 변경(HRTDMCPROUT -> RWIPMCPBOM) / SAP Flow 정보 로직 수정
        /// 2018-05-02-임종우 : SAP FLOW 정보 사용시 싱글 CHIP에 대한 로직 추가
        /// 2020-04-28-김미경 : 제품 TYPE(STACK) 추가 반영 (이승희 D)
        /// 2020-10-12-김미경 : H_PKG_2D_CMOLD "%" 기능 추가
        /// 2020-10-15-임종우 : C-MOLD로직은 본사만 적용
        /// </summary>
        public PRD010910()
        {
            InitializeComponent();

            cdvFromToDate.AutoBinding();
            cdvFromToDate.DaySelector.SelectedValue = "MONTH";
            SortInit();
            GridColumnInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory; 
        }


        #region 유효성검사 및 초기화
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
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Category 1", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Category 2", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Category 3", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);            
            spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 75);            

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {           
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "B.MAT_GRP_1", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER","MAT_GRP_1", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "B.MAT_GRP_9", "MAT_GRP_9", "MAT_GRP_9", "MAT_GRP_9", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "B.MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "B.MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "B.MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "B.MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "B.MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "B.MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "B.MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "B.MAT_CMF_10", "MAT_CMF_10", "MAT_CMF_10", "MAT_CMF_10", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "B.MAT_ID", "MAT_ID", "MAT_ID", "MAT_ID", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("STEP", "A.OPER", "OPER", "OPER", "DECODE(OPER,'AISSUE','A0001','TISSUE','T0001','EISSUE','E0001',OPER)", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST_DEVICE", "B.MAT_CMF_7", "MAT_CMF_7", "MAT_CMF_7", "MAT_CMF_7", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("SAP_CODE", "BASE_MAT_ID,VENDOR_MAT_ID", "BASE_MAT_ID,VENDOR_MAT_ID", "CASE WHEN OPER <= 'A0200' THEN VENDOR_MAT_ID ELSE BASE_MAT_ID END SAP_CODE", "BASE_MAT_ID,VENDOR_MAT_ID", true);
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

            //string QueryCond1 = null;
            //string QueryCond2 = null;

            string strDate = string.Empty;
            string sFrom = string.Empty;
            string sTo = string.Empty;
            
            string sSelectQty = string.Empty;

            string strDecode = string.Empty;
            string sKpcsValue = string.Empty;

            string[] selectDate = new string[cdvFromToDate.SubtractBetweenFromToDate + 1];
            selectDate = cdvFromToDate.getSelectDate();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            //QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            //QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            // kpcs 선택에 의한 분모 값 저장 한다.
            if (ckbKpcs.Checked == true)
            {
                sKpcsValue = "1000";
            }
            else
            {
                sKpcsValue = "1";
            }
            
            switch (cdvFromToDate.DaySelector.SelectedValue.ToString())
            {
                case "WEEK":
                    sFrom = cdvFromToDate.FromWeek.SelectedValue.ToString();
                    sTo = cdvFromToDate.ToWeek.SelectedValue.ToString();
                    strDate = "주간";
                    break;
                case "MONTH":
                    sFrom = cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM");
                    sTo = cdvFromToDate.ToYearMonth.Value.ToString("yyyMM");
                    strDate = "월간";
                    break;
                default:
                    sFrom = cdvFromToDate.FromDate.Text.Replace("-", "");
                    sTo = cdvFromToDate.ToDate.Text.Replace("-", "");
                    strDate = "WORK_DATE";
                    break;
            }

            if (ckbConv.Checked == true)
            {
                strDecode += cdvFromToDate.getDecodeQuery("SUM(DECODE(PLAN_DATE", "CONV_QTY, 0))", "V").Replace(", SUM(DECODE(", "                     , SUM(DECODE(");
            }
            else
            {
                strDecode += cdvFromToDate.getDecodeQuery("SUM(DECODE(PLAN_DATE", "PLAN_QTY, 0))", "V").Replace(", SUM(DECODE(", "                     , SUM(DECODE(");
            }

            // 2014-10-17-임종우 : 특정구간에 WITH 문 사용하면 Wire 수량만 데이터 집계가 이상하여 풀어서 사용함... 원인은 모르겠음.
            StringBuilder ss1 = new StringBuilder();
            
            ss1.Append("(" + "\n");            
            ss1.Append("                                        SELECT A.PLAN_DATE, A.MAT_KEY, A.MAT_ID, TO_NUMBER(PLAN_QTY) AS PLAN_QTY" + "\n");
            ss1.Append("                                             , B.MAT_GRP_1, B.MAT_GRP_2, B.MAT_GRP_4, B.MAT_GRP_5" + "\n");
            ss1.Append("                                             , TO_NUMBER(DECODE(B.MAT_GRP_6, '-', 1, ' ', 1, B.MAT_GRP_6)) AS MAT_GRP_6" + "\n");
            ss1.Append("                                             , B.MAT_CMF_11, TO_NUMBER(DECODE(B.MAT_CMF_13,' ',1,B.MAT_CMF_13)) AS MAT_CMF_13" + "\n");
            ss1.Append("                                             , CASE WHEN B.MAT_GRP_4 IN ('-', 'FD', 'FU') THEN '1'" + "\n");
            ss1.Append("                                                    WHEN B.MAT_GRP_4 IN ('DDP') THEN '2'" + "\n");
            ss1.Append("                                                    WHEN B.MAT_GRP_4 IN ('QDP') THEN '4'" + "\n");
            ss1.Append("                                                    WHEN B.MAT_GRP_4 IN ('ODP') THEN '8'" + "\n");
            ss1.Append("                                                    ELSE REGEXP_REPLACE(B.MAT_GRP_4, '[^[:digit:]]')" + "\n");
            ss1.Append("                                               END AS STACK" + "\n");
            //ss1.Append("                                             , CASE WHEN B.MAT_GRP_4 IN ('-', 'FD', 'FU') THEN '1'" + "\n");
            //ss1.Append("                                                    ELSE SUBSTR(B.MAT_GRP_4, 3) " + "\n");
            //ss1.Append("                                               END AS STACK" + "\n");
            ss1.Append("                                             , B.OPER" + "\n");
            ss1.Append("                                          FROM (" + "\n");

            if (strDate == "주간")
            {                
                ss1.Append("                                                SELECT DISTINCT A.PLAN_WEEK AS PLAN_DATE" + "\n");
                ss1.Append("                                                     , CASE WHEN B.MAT_ID IS NULL THEN A.MAT_ID ELSE B.MCP_TO_PART END AS MAT_KEY" + "\n");
                ss1.Append("                                                     , CASE WHEN B.MAT_ID IS NULL THEN A.MAT_ID ELSE B.MAT_ID END AS MAT_ID" + "\n");
                ss1.Append("                                                     , A.WW_QTY AS PLAN_QTY" + "\n");
                ss1.Append("                                                  FROM RWIPPLNWEK A" + "\n");
                ss1.Append("                                                     , RWIPMCPBOM B" + "\n");
                ss1.Append("                                                 WHERE 1=1   " + "\n");
                ss1.Append("                                                   AND A.MAT_ID = B.MCP_TO_PART(+)" + "\n");
                ss1.Append("                                                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'   " + "\n");
                ss1.Append("                                                   AND A.GUBUN = '3'  " + "\n");
                ss1.Append("                                                   AND A.PLAN_WEEK BETWEEN '" + sFrom + "' AND '" + sTo + "'" + "\n");
            }
            else
            {
                ss1.Append("                                                SELECT DISTINCT A.PLAN_MONTH AS PLAN_DATE" + "\n");
                ss1.Append("                                                     , CASE WHEN B.MAT_ID IS NULL THEN A.MAT_ID ELSE B.MCP_TO_PART END AS MAT_KEY" + "\n");
                ss1.Append("                                                     , CASE WHEN B.MAT_ID IS NULL THEN A.MAT_ID ELSE B.MAT_ID END AS MAT_ID" + "\n");
                ss1.Append("                                                     , A.RESV_FIELD1 AS PLAN_QTY" + "\n");
                ss1.Append("                                                  FROM CWIPPLNMON A" + "\n");
                ss1.Append("                                                     , RWIPMCPBOM B" + "\n");
                ss1.Append("                                                 WHERE 1=1   " + "\n");
                ss1.Append("                                                   AND A.MAT_ID = B.MCP_TO_PART(+)" + "\n");
                ss1.Append("                                                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'   " + "\n");
                ss1.Append("                                                   AND A.PLAN_MONTH BETWEEN '" + sFrom + "' AND '" + sTo + "'" + "\n");
                ss1.Append("                                                   AND DECODE(A.RESV_FIELD1, ' ', 0, A.RESV_FIELD1) > 0" + "\n");
            }
            
            ss1.Append("                                               ) A" + "\n");
            ss1.Append("                                             , (" + "\n");
            ss1.Append("                                                SELECT DISTINCT MAT_ID, MAT_GRP_1, MAT_GRP_2, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6, MAT_CMF_11, MAT_CMF_13, KTSCH AS OPER" + "\n");
            ss1.Append("                                                  FROM MWIPMATDEF MAT" + "\n");

            if (GlobalVariable.gsGlovalSite == "K1")
            {
                ss1.Append("                                                     , ZHPPT120@SAPREAL SAP" + "\n");
            }
            else
            {
                ss1.Append("                                                     ,( SELECT FLOW PLNNR,' ' ARBPL,OPER KTSCH FROM MWIPFLWOPR@RPTTOMES  WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "') SAP" + "\n");
            }
            
            ss1.Append("                                                 WHERE 1=1" + "\n");
            //ss1.Append("                                                   AND DECODE(MAT.MAT_GRP_5, 'Merge', MAT.FIRST_FLOW || '20', '-', MAT.FIRST_FLOW || '20', MAT.FIRST_FLOW || '10') = SAP.PLNNR" + "\n");
            ss1.Append("                                                   AND MAT.FIRST_FLOW = SUBSTR(PLNNR, 1, 4) " + "\n");
            ss1.Append("                                                   AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            ss1.Append("                                                   AND MAT.DELETE_FLAG = ' '" + "\n");
            ss1.Append("                                                   AND MAT.MAT_TYPE = 'FG'" + "\n");
            ss1.Append("                                                   AND SAP.ARBPL != 'DUMMY'" + "\n");
            ss1.Append("                                                   AND SAP.KTSCH LIKE 'A%'" + "\n");

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                                                   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                                                   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                                                   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                                                   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                                                   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                                                   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                                                   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                                                   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                                                   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);            

            ss1.Append("                                               ) B   " + "\n");            
            ss1.Append("                                         WHERE 1=1" + "\n");
            ss1.Append("                                           AND A.MAT_ID = B.MAT_ID" + "\n");
            ss1.Append("                                           AND (B.OPER IN ('A0010', 'A0200', 'A0030', 'A0040', 'A0012', 'A0033', 'A0170', 'A0180', 'A0230', 'A1250', 'A1260', 'A1300', 'A1750', 'A1000')" + "\n");
            ss1.Append("                                               OR B.OPER LIKE 'A06%')   " + "\n");

            // Product
            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
            {
                ss1.AppendFormat("                                           AND A.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
            }
                        
            ss1.Append("                                       )");            

            //strSqlString.Append("WITH DT AS" + "\n");
            //strSqlString.Append("(" + "\n");
            //strSqlString.Append("SELECT A.PLAN_DATE, A.MAT_KEY, A.MAT_ID, TO_NUMBER(PLAN_QTY) AS PLAN_QTY" + "\n");
            //strSqlString.Append("     , MAT_GRP_1, MAT_GRP_2, MAT_GRP_4, MAT_GRP_5" + "\n");
            //strSqlString.Append("     , TO_NUMBER(DECODE(MAT_GRP_6, '-', 1, ' ', 1, MAT_GRP_6)) AS MAT_GRP_6" + "\n");
            //strSqlString.Append("     , MAT_CMF_11, TO_NUMBER(DECODE(MAT_CMF_13,' ',1,MAT_CMF_13)) AS MAT_CMF_13, FIRST_FLOW" + "\n");
            //strSqlString.Append("     , CASE WHEN MAT_GRP_4 IN ('-', 'FD', 'FU') THEN '1'" + "\n");
            //strSqlString.Append("            ELSE SUBSTR(MAT_GRP_4, -1) " + "\n");
            //strSqlString.Append("       END AS STACK" + "\n");
            //strSqlString.Append("     , C.OPER" + "\n");
            //strSqlString.Append("  FROM (" + "\n");

            //if (strDate == "주간")
            //{
            //    strSqlString.Append("        SELECT DISTINCT A.PLAN_WEEK AS PLAN_DATE" + "\n");
            //    strSqlString.Append("             , CASE WHEN B.MAT_ID IS NULL THEN A.MAT_ID ELSE B.MAT_KEY END AS MAT_KEY" + "\n");
            //    strSqlString.Append("             , CASE WHEN B.MAT_ID IS NULL THEN A.MAT_ID ELSE B.MAT_ID END AS MAT_ID" + "\n");
            //    strSqlString.Append("             , A.WW_QTY AS PLAN_QTY" + "\n");
            //    strSqlString.Append("          FROM RWIPPLNWEK A" + "\n");
            //    strSqlString.Append("             , HRTDMCPROUT@RPTTOMES B" + "\n");
            //    strSqlString.Append("         WHERE 1=1   " + "\n");
            //    strSqlString.Append("           AND A.MAT_ID = B.MAT_KEY(+)" + "\n");
            //    strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'   " + "\n");
            //    strSqlString.Append("           AND A.GUBUN = '3'  " + "\n");
            //    strSqlString.Append("           AND A.PLAN_WEEK BETWEEN '" + sFrom + "' AND '" + sTo + "'" + "\n");
            //}
            //else
            //{
            //    strSqlString.Append("        SELECT DISTINCT A.PLAN_MONTH AS PLAN_DATE" + "\n");
            //    strSqlString.Append("             , CASE WHEN B.MAT_ID IS NULL THEN A.MAT_ID ELSE B.MAT_KEY END AS MAT_KEY" + "\n");
            //    strSqlString.Append("             , CASE WHEN B.MAT_ID IS NULL THEN A.MAT_ID ELSE B.MAT_ID END AS MAT_ID" + "\n");
            //    strSqlString.Append("             , A.RESV_FIELD1 AS PLAN_QTY" + "\n");
            //    strSqlString.Append("          FROM CWIPPLNMON A" + "\n");
            //    strSqlString.Append("             , HRTDMCPROUT@RPTTOMES B" + "\n");
            //    strSqlString.Append("         WHERE 1=1   " + "\n");
            //    strSqlString.Append("           AND A.MAT_ID = B.MAT_KEY(+)" + "\n");
            //    strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'   " + "\n");
            //    strSqlString.Append("           AND A.PLAN_MONTH BETWEEN '" + sFrom + "' AND '" + sTo + "'" + "\n");
            //    strSqlString.Append("           AND DECODE(A.RESV_FIELD1, ' ', 0, A.RESV_FIELD1) > 0" + "\n");
            //}

            //strSqlString.Append("       ) A" + "\n");
            //strSqlString.Append("     , MWIPMATDEF B" + "\n");
            //strSqlString.Append("     , (" + "\n");
            //strSqlString.Append("        SELECT DISTINCT SUBSTR(PLNNR, 1, 4) AS FLOW, KTSCH AS OPER" + "\n");
            //strSqlString.Append("          FROM ZHPPT120@SAPREAL" + "\n");
            //strSqlString.Append("         WHERE 1=1" + "\n");
            //strSqlString.Append("           AND ARBPL != 'DUMMY'" + "\n");
            //strSqlString.Append("       ) C   " + "\n");
            ////strSqlString.Append("     , MWIPFLWOPR@RPTTOMES C " + "\n");
            //strSqlString.Append(" WHERE 1=1" + "\n");
            //strSqlString.Append("   AND A.MAT_ID = B.MAT_ID" + "\n");
            ////strSqlString.Append("   AND B.FACTORY = C.FACTORY" + "\n");
            //strSqlString.Append("   AND B.FIRST_FLOW = C.FLOW   " + "\n");
            //strSqlString.Append("   AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            //strSqlString.Append("   AND B.DELETE_FLAG = ' '" + "\n");
            //strSqlString.Append("   AND (C.OPER IN ('A0010', 'A0020', 'A0030', 'A0040', 'A0012', 'A0033', 'A0170', 'A0180', 'A0230', 'A1250', 'A1260', 'A1300', 'A1750', 'A1000')" + "\n");
            //strSqlString.Append("       OR C.OPER LIKE 'A06%')   " + "\n");

            //// Product
            //if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
            //{
            //    strSqlString.AppendFormat("   AND B.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
            //}

            ////상세 조회에 따른 SQL문 생성                        
            //if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
            //    strSqlString.AppendFormat("   AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            //if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
            //    strSqlString.AppendFormat("   AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            //if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
            //    strSqlString.AppendFormat("   AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            //if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
            //    strSqlString.AppendFormat("   AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            //if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
            //    strSqlString.AppendFormat("   AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            //if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
            //    strSqlString.AppendFormat("   AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            //if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
            //    strSqlString.AppendFormat("   AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            //if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
            //    strSqlString.AppendFormat("   AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            //if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
            //    strSqlString.AppendFormat("   AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                        
            //strSqlString.Append("), MD AS" + "\n");
            strSqlString.Append("WITH MD AS" + "\n");
            strSqlString.Append("(" + "\n");
            strSqlString.Append("SELECT *" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT '계획' AS GUBUN1 FROM DUAL UNION" + "\n");
            strSqlString.Append("        SELECT '일목표' AS GUBUN1 FROM DUAL" + "\n");
            strSqlString.Append("       ) A" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT 'Wafer' AS GUBUN2, 'Half cut' AS GUBUN3 FROM DUAL UNION" + "\n");
            strSqlString.Append("        SELECT 'Wafer' AS GUBUN2, 'B/G' AS GUBUN3 FROM DUAL UNION" + "\n");
            strSqlString.Append("        SELECT 'Wafer' AS GUBUN2, 'Pre_G' AS GUBUN3 FROM DUAL UNION" + "\n");
            strSqlString.Append("        SELECT 'Wafer' AS GUBUN2, 'Saw' AS GUBUN3 FROM DUAL UNION" + "\n");
            strSqlString.Append("        SELECT 'Wafer' AS GUBUN2, 'Stealth saw' AS GUBUN3 FROM DUAL UNION" + "\n");
            strSqlString.Append("        SELECT 'Wafer' AS GUBUN2, 'Laser G/V' AS GUBUN3 FROM DUAL UNION" + "\n");
            strSqlString.Append("        SELECT 'Wafer' AS GUBUN2, 'DAF Cut' AS GUBUN3 FROM DUAL UNION" + "\n");
            strSqlString.Append("        SELECT 'Wafer' AS GUBUN2, 'DDS' AS GUBUN3 FROM DUAL UNION" + "\n");
            strSqlString.Append("        SELECT 'CHIP수' AS GUBUN2, 'Epoxy' AS GUBUN3 FROM DUAL UNION" + "\n");
            strSqlString.Append("        SELECT 'CHIP수' AS GUBUN2, 'BOC' AS GUBUN3 FROM DUAL UNION" + "\n");
            strSqlString.Append("        SELECT 'CHIP수' AS GUBUN2, 'Others' AS GUBUN3 FROM DUAL UNION" + "\n");
            strSqlString.Append("        SELECT 'Wire수' AS GUBUN2, ' ' AS GUBUN3 FROM DUAL UNION" + "\n");
            strSqlString.Append("        SELECT 'PKG수' AS GUBUN2, 'C-Mold' AS GUBUN3 FROM DUAL UNION" + "\n");
            strSqlString.Append("        SELECT 'PKG수' AS GUBUN2, 'ORP' AS GUBUN3 FROM DUAL UNION" + "\n");
            strSqlString.Append("        SELECT 'PKG수' AS GUBUN2, 'SBA' AS GUBUN3 FROM DUAL UNION" + "\n");
            strSqlString.Append("        SELECT 'PKG수' AS GUBUN2, 'SST' AS GUBUN3 FROM DUAL UNION" + "\n");
            strSqlString.Append("        SELECT '95mm' AS GUBUN2, 'Mold' AS GUBUN3 FROM DUAL UNION" + "\n");
            strSqlString.Append("        SELECT '95mm' AS GUBUN2, 'SBA' AS GUBUN3 FROM DUAL UNION" + "\n");
            strSqlString.Append("        SELECT '95mm' AS GUBUN2, 'SST' AS GUBUN3 FROM DUAL" + "\n");
            strSqlString.Append("       ) B      " + "\n");
            strSqlString.Append(")" + "\n");
            strSqlString.Append("SELECT MDL.GUBUN1, MDL.GUBUN2, MDL.GUBUN3" + "\n");

            for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                if (strDate == "주간")
                {
                    strSqlString.AppendFormat("     , ROUND(CASE WHEN MDL.GUBUN1 = '계획' THEN V{0} ELSE V{0} / 7 END / " + sKpcsValue + ", 0) V{0}" + "\n", i.ToString());
                }
                else
                {
                    strSqlString.AppendFormat("     , ROUND(CASE WHEN MDL.GUBUN1 = '계획' THEN V{0} ELSE V{0} / TO_CHAR(LAST_DAY(TO_DATE('{1}', 'YYYYMM')), 'DD') END / " + sKpcsValue + ", 0) V{0}" + "\n", i.ToString(), selectDate[i]);
                }
            }
            
            strSqlString.Append("  FROM MD MDL" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT *" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT '계획' AS GUBUN1 FROM DUAL UNION" + "\n");
            strSqlString.Append("                SELECT '일목표' AS GUBUN1 FROM DUAL" + "\n");
            strSqlString.Append("               ) A" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT GUBUN2, GUBUN3" + "\n");
            strSqlString.Append(strDecode);            
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT PLAN_DATE " + "\n");
            strSqlString.Append("                             , CASE WHEN OPER IN ('C-Mold', 'ORP', 'SBA', 'SST') THEN 'PKG수'" + "\n");
            strSqlString.Append("                                    ELSE 'Wafer' " + "\n");
            strSqlString.Append("                               END AS GUBUN2 " + "\n");
            strSqlString.Append("                             , OPER AS GUBUN3" + "\n");
            strSqlString.Append("                             , CASE WHEN OPER IN ('C-Mold', 'ORP', 'SBA', 'SST') THEN PLAN_QTY" + "\n");
            strSqlString.Append("                                    ELSE PLAN_QTY * NVL(COMP_CNT,1) " + "\n");
            strSqlString.Append("                               END AS PLAN_QTY" + "\n");
            strSqlString.Append("                             , ROUND(CASE WHEN OPER IN ('C-Mold', 'ORP', 'SBA', 'SST') THEN PLAN_QTY" + "\n");
            strSqlString.Append("                                          ELSE PLAN_QTY * NVL(COMP_CNT,1) / (MAT_CMF_13 * 0.85) " + "\n");
            strSqlString.Append("                                     END, 0) AS CONV_QTY" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT PLAN_DATE, MAT_ID" + "\n");
            strSqlString.Append("                                     , CASE WHEN OPER = 'A0010' THEN 'Half cut'" + "\n");
            strSqlString.Append("                                            WHEN OPER = 'A0040' THEN 'B/G'" + "\n");
            strSqlString.Append("                                            WHEN OPER = 'A0030' THEN 'Pre_G'" + "\n");
            strSqlString.Append("                                            WHEN OPER = 'A0200' THEN 'Saw'" + "\n");
            strSqlString.Append("                                            WHEN OPER IN ('A0012', 'A0033') THEN 'Stealth saw'" + "\n");
            strSqlString.Append("                                            WHEN OPER = 'A0170' THEN 'Laser G/V'" + "\n");
            strSqlString.Append("                                            WHEN OPER = 'A0180' THEN 'DAF Cut'" + "\n");
            strSqlString.Append("                                            WHEN OPER = 'A0230' THEN 'DDS'" + "\n");
            strSqlString.Append("                                            WHEN OPER = 'A1000' THEN 'C-Mold'" + "\n");
            strSqlString.Append("                                            WHEN OPER IN ('A1250', 'A1260') THEN 'ORP'" + "\n");
            strSqlString.Append("                                            WHEN OPER = 'A1300' THEN 'SBA'" + "\n");
            strSqlString.Append("                                            WHEN OPER = 'A1750' THEN 'SST'" + "\n");
            strSqlString.Append("                                            ELSE ''" + "\n");
            strSqlString.Append("                                       END OPER" + "\n");
            strSqlString.Append("                                     , PLAN_QTY" + "\n");
            strSqlString.Append("                                     , MAT_CMF_13" + "\n");
            strSqlString.Append("                                     , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') AND KEY_1 = A.MAT_ID) AS COMP_CNT" + "\n");
            strSqlString.Append("                                     , (SELECT /*+INDEX_DESC(MGCMTBLDAT MGCMTBLDAT2_PK)*/ KEY_3 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_PKG_2D_CMOLD' AND KEY_1 = 'C-MOLD' AND KEY_2 = A.MAT_GRP_1 AND KEY_3 = A.MAT_CMF_11 AND (KEY_4 = '%' OR A.MAT_ID LIKE KEY_4) AND ROWNUM = 1) AS C_MOLD" + "\n");
            //strSqlString.Append("                                  FROM DT A" + "\n");
            strSqlString.Append("                                  FROM " + ss1.ToString() + " A" + "\n");
            strSqlString.Append("                                 WHERE 1=1" + "\n");
            strSqlString.Append("                                   AND OPER IN ('A0010', 'A0200', 'A0030', 'A0040', 'A0012', 'A0033', 'A0170', 'A0180', 'A0230', 'A1000', 'A1250', 'A1260', 'A1300', 'A1750')" + "\n");
            strSqlString.Append("                               )" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");

            // 2020-10-15-임종우 : C-MOLD로직은 본사만 적용
            if (GlobalVariable.gsGlovalSite == "K1")
            {
                strSqlString.Append("                           AND (OPER <> 'C-Mold' OR (OPER = 'C-Mold' AND C_MOLD IS NOT NULL))" + "\n");
            }

            strSqlString.Append("                         UNION ALL" + "\n");
            strSqlString.Append("                        SELECT PLAN_DATE" + "\n");
            strSqlString.Append("                             , 'CHIP수' AS GUBUN2" + "\n");
            strSqlString.Append("                             , FAMILY AS GUBUN3" + "\n");
            strSqlString.Append("                             , SUM(PLAN_QTY * STACK) AS PLAN_QTY" + "\n");
            strSqlString.Append("                             , SUM(PLAN_QTY * STACK) AS CONV_QTY" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT DISTINCT A.PLAN_DATE, A.MAT_KEY, A.STACK, A.PLAN_QTY " + "\n");
            strSqlString.Append("                                     , (SELECT DATA_2 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_FAMILY' AND KEY_1 = A.MAT_GRP_2) AS FAMILY" + "\n");
            //strSqlString.Append("                                  FROM DT A" + "\n");
            strSqlString.Append("                                  FROM " + ss1.ToString() + " A" + "\n");
            strSqlString.Append("                               )" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND FAMILY IN ('Epoxy', 'BOC', 'Others')" + "\n");
            strSqlString.Append("                         GROUP BY PLAN_DATE, FAMILY" + "\n");
            strSqlString.Append("                         UNION ALL" + "\n");
            strSqlString.Append("                        SELECT PLAN_DATE" + "\n");
            strSqlString.Append("                             , 'Wire수' AS GUBUN2     " + "\n");
            strSqlString.Append("                             , ' ' AS GUBUN3" + "\n");
            strSqlString.Append("                             , PLAN_QTY" + "\n");
            strSqlString.Append("                             , PLAN_QTY * NVL(WIRE_QTY, MAT_GRP_6) AS CONV_QTY" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT PLAN_DATE" + "\n");
            strSqlString.Append("                                     , OPER" + "\n");
            strSqlString.Append("                                     , PLAN_QTY" + "\n");
            strSqlString.Append("                                     , MAT_ID" + "\n");
            strSqlString.Append("                                     , MAT_GRP_6" + "\n");
            strSqlString.Append("                                     , (SELECT MAX(TO_NUMBER(TCD_CMF_2)) FROM CWIPTCDDEF@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND SET_FLAG = 'Y' AND TCD_CMF_2 <> ' ' AND MAT_ID = A.MAT_ID AND OPER = A.OPER) AS WIRE_QTY" + "\n");
            //strSqlString.Append("                                  FROM DT A" + "\n");
            strSqlString.Append("                                  FROM " + ss1.ToString() + " A" + "\n");
            strSqlString.Append("                                 WHERE 1=1" + "\n");
            strSqlString.Append("                                   AND OPER LIKE 'A06%'" + "\n");
            strSqlString.Append("                               )" + "\n");
            strSqlString.Append("                         UNION ALL" + "\n");
            strSqlString.Append("                        SELECT PLAN_DATE" + "\n");
            strSqlString.Append("                             , '95mm' AS GUBUN2" + "\n");
            strSqlString.Append("                             , CASE WHEN OPER = 'A1000' THEN 'Mold'" + "\n");
            strSqlString.Append("                                    WHEN OPER = 'A1300' THEN 'SBA'" + "\n");
            strSqlString.Append("                                    ELSE 'SST'" + "\n");
            strSqlString.Append("                               END GUBUN3" + "\n");
            strSqlString.Append("                             , SUM(PLAN_QTY) AS PLAN_QTY" + "\n");
            strSqlString.Append("                             , SUM(PLAN_QTY) AS CONV_QTY" + "\n");
            //strSqlString.Append("                          FROM DT A" + "\n");
            strSqlString.Append("                          FROM " + ss1.ToString() + " A" + "\n");
            strSqlString.Append("                             , (" + "\n");
            strSqlString.Append("                                SELECT * " + "\n");
            strSqlString.Append("                                  FROM MATRNAMSTS@RPTTOMES " + "\n");
            strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                   AND ATTR_TYPE = 'MAT_ETC'" + "\n");
            strSqlString.Append("                                   AND ATTR_NAME = 'MAGAZINE_TYPE' " + "\n");
            strSqlString.Append("                                   AND ATTR_VALUE = '95'" + "\n");
            strSqlString.Append("                               ) B" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.ATTR_KEY" + "\n");
            strSqlString.Append("                           AND A.OPER IN ('A1000', 'A1300', 'A1750')" + "\n");
            strSqlString.Append("                         GROUP BY PLAN_DATE, OPER" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY GUBUN2, GUBUN3" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("       ) PLN" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND MDL.GUBUN1 = PLN.GUBUN1(+)   " + "\n");
            strSqlString.Append("   AND MDL.GUBUN2 = PLN.GUBUN2(+)   " + "\n");
            strSqlString.Append("   AND MDL.GUBUN3 = PLN.GUBUN3(+)   " + "\n");
            strSqlString.Append(" ORDER BY GUBUN1, DECODE(GUBUN2, 'Wafer', 1, 'CHIP수', 2, 'Wire수', 3, 'PKG수', 4, 5), GUBUN3" + "\n");
            
            
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
            
            return strSqlString.ToString();
        }
           
        #endregion


        #region EVENT 처리
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

                ////그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                ////by John
                ////1.Griid 합계 표시
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 12, null, null, btnSort);

                ////int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 7, 10, null, null, btnSort);
                ////                  토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함


                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                ////3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 13, 0, 1, true, Align.Center, VerticalAlign.Center);


                ////4. Column Auto Fit
                //spdData.RPT_AutoFit(false);

                spdData.DataSource = dt;

                for (int i = 0; i <= 2; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LemonChiffon;
                }

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
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();            
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);            
        }
        #endregion
    }
}