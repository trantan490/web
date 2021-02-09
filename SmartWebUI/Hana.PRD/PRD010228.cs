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
    public partial class PRD010228 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        decimal jindoPer;
        int iLastday;
        int iToday = 0;
        double iRemain = 0;
        /// <summary>
        /// 클  래  스: PRD010228<br/>
        /// 클래스요약: TEST 월간 진도관리<br/>
        /// 작  성  자: 김미경<br/>
        /// 최초작성일: 2020-03-02<br/>
        /// 상세  설명: TEST 월간 진도관리(이승희 D 요청)<br/>
        /// 변경  내용: <br/>     
        /// 2020-04-28-김미경 : 제품 TYPE(STACK) 추가 반영 (이승희 D) 
        /// 2020-05-04-김미경 : T/O 실적에 TZ010 WIP 재공 반영 (이승희 D)
        /// </summary>
        public PRD010228()
        {
            InitializeComponent();
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();
            this.cdvFactory.sFactory = GlobalVariable.gsTestDefaultFactory;
            this.SetFactory(GlobalVariable.gsTestDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsTestDefaultFactory;
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();

            LabelTextChange();

            try
            {
                spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("MAJOR CODE", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);                
                spdData.RPT_AddBasicColumn("PACKAGE", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("TYPE1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("TYPE2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("LD COUNT", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("PKG CODE", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("DENSITY", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("GENERATION", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);

                spdData.RPT_AddBasicColumn("Monthly plan", 0, 11, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("T/O", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("actual", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Progress difference", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 12, 2);

                spdData.RPT_AddBasicColumn("Semi-manufactures", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("actual", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Progress difference", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 14, 2);

                spdData.RPT_AddBasicColumn("WIP", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WIP days", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("a daily goal", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("actual", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Semi-manufactures", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                
                spdData.RPT_AddBasicColumn("Test-Out", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 90);                
                spdData.RPT_AddBasicColumn("SHIP", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 19, 3);

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 8, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 9, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 10, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 11, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 16, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 17, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 18, 2);
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "(SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.MAT_GRP_1) AS CUSTOMER", "A.MAT_GRP_1", "CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "A.MAT_GRP_9 AS MAJOR", "A.MAT_GRP_9", "A.MAT_GRP_9", true);           
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "A.MAT_GRP_10 AS PKG", "A.MAT_GRP_10", "A.MAT_GRP_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "A.MAT_GRP_4 AS TYPE1", "A.MAT_GRP_4", "A.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "A.MAT_GRP_5 AS TYPE2", "A.MAT_GRP_5", "A.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "A.MAT_GRP_6 AS LD_COUNT", "A.MAT_GRP_6", "A.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "A.MAT_CMF_11 AS PKG_CODE", "A.MAT_CMF_11", "A.MAT_CMF_11", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "A.MAT_GRP_7 AS DENSITY", "A.MAT_GRP_7", "A.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "A.MAT_GRP_8 AS GENERATION", "A.MAT_GRP_8", "A.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "A.MAT_CMF_10 AS PIN_TYPE", "A.MAT_CMF_10", "A.MAT_CMF_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.MAT_ID", "A.MAT_ID", "A.MAT_ID", false);
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
            string QueryCond3;
            string start_date;
            string date;
            string month;
            string year;
            string sKcpkValue;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            date = cdvDate.SelectedValue();

            DateTime Select_date;
            Select_date = DateTime.Parse(cdvDate.Text.ToString());

            year = Select_date.ToString("yyyy");
            month = Select_date.ToString("yyyyMM");
            start_date = month + "01";

            if (ckbKpcs.Checked == true)
            {
                sKcpkValue = "1000";
            }
            else
            {
                sKcpkValue = "1";
            }

            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond1);
            strSqlString.Append("     , ROUND(SUM(MON_PLN) / " + sKcpkValue + ", 0) AS MON_PLN" + "\n");
            strSqlString.Append("     , ROUND(SUM(SHP_TTL) / " + sKcpkValue + ", 0) AS SHP_TTL" + "\n");
            strSqlString.Append("     , CASE WHEN SUM(MON_PLN) = 0 THEN 0" + "\n");
            strSqlString.Append("            ELSE ROUND((SUM(SHP_TTL) / (SUM(MON_PLN) / " + iLastday + ")) - " + iToday + ", 1)" + "\n");
            strSqlString.Append("       END AS SHP_JINDO" + "\n");
            strSqlString.Append("     , ROUND((SUM(ONLY_SHP) + SUM(WIP_TTL)) / " + sKcpkValue + ", 0) AS RCV_TTL" + "\n");
            strSqlString.Append("     , CASE WHEN SUM(MON_PLN) = 0 THEN 0" + "\n");
            strSqlString.Append("            ELSE ROUND(( ((SUM(SHP_TTL) + SUM(WIP_TTL)) / SUM(MON_PLN)) - ((" + iToday + " + 2) / " + iLastday + ") ) * " + iLastday + ", 1)" + "\n");
            strSqlString.Append("       END AS RCV_JINDO" + "\n");           
            strSqlString.Append("     , ROUND(SUM(WIP_TTL) / " + sKcpkValue + ", 0) AS WIP_TTL" + "\n");
            strSqlString.Append("     , CASE WHEN SUM(MON_PLN) = 0 THEN 0" + "\n");
            strSqlString.Append("            ELSE ROUND(SUM(WIP_TTL) / (SUM(MON_PLN) / " + iLastday + "), 1)" + "\n");
            strSqlString.Append("       END AS WIP_DAY" + "\n");

            if (iRemain != 0)  // 잔여일이 0 일이 아닐때
            {
                strSqlString.Append("     , ROUND((SUM(MON_PLN) - SUM(SHP_TTL)) / " + iRemain + " / " + sKcpkValue + ", 0) AS TARGET_QTY" + "\n");
            }
            else  // 잔여일이 0 일 일대
            {
                strSqlString.Append("     , 0 AS TARGET_QTY " + "\n");
            }

            strSqlString.Append("     , ROUND(SUM(RCV_TODAY) / " + sKcpkValue + ", 0) AS RCV_TODAY" + "\n");           
            strSqlString.Append("     , ROUND(SUM(T_OUT_TODAY) / " + sKcpkValue + ", 0) AS T_OUT_TODAY" + "\n");
            strSqlString.Append("     , ROUND(SUM(SHP_TODAY) / " + sKcpkValue + ", 0) AS SHP_TODAY" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT MAT.MAT_ID, MAT_GRP_1 AS CUSTOMER" + "\n");
            strSqlString.Append("             , MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5" + "\n");
            strSqlString.Append("             , MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, MAT_GRP_9, MAT_GRP_10, MAT_CMF_10, MAT_CMF_11" + "\n");
            strSqlString.Append("             , NVL(PLN.MON_PLN, 0) * PRICE AS MON_PLN" + "\n");
            //strSqlString.Append("             , NVL(SHP.SHP_TTL, 0) * PRICE AS SHP_TTL" + "\n");
            strSqlString.Append("             , (NVL(SHP.SHP_TTL, 0) + DECODE(SUBSTR('" + date + "', -2), '01', 0, NVL(WIP.WIP_TZ010, 0))) * PRICE AS SHP_TTL" + "\n");
            strSqlString.Append("             , (NVL(SHP.SHP_TTL, 0)) * PRICE AS ONLY_SHP" + "\n"); //2020-09-22-이희석 : 생산운영그룹(이승희 대리) 요청으로 수정
            strSqlString.Append("             , NVL(WIP.WIP_TTL,0) * PRICE AS WIP_TTL" + "\n");
            strSqlString.Append("             , NVL(RCV_TODAY,0) * PRICE AS RCV_TODAY" + "\n");
            strSqlString.Append("             , NVL(T_OUT_TODAY, 0) * PRICE AS T_OUT_TODAY" + "\n");
            strSqlString.Append("             , NVL(SHP.SHP_TODAY, 0) * PRICE AS SHP_TODAY" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT A.MAT_ID, MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5" + "\n");
            strSqlString.Append("                     , MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, MAT_GRP_9, MAT_GRP_10, MAT_CMF_10, MAT_CMF_11" + "\n");

            if (ckbConv.Checked == true)
            {
                strSqlString.Append("                   , CASE WHEN MAT_GRP_4 IN ('-', 'FD', 'FU') THEN '1'" + "\n");
                strSqlString.Append("                          WHEN MAT_GRP_4 IN ('DDP') THEN '2'" + "\n");
                strSqlString.Append("                          WHEN MAT_GRP_4 IN ('QDP') THEN '4'" + "\n");
                strSqlString.Append("                          WHEN MAT_GRP_4 IN ('ODP') THEN '8'" + "\n");
                strSqlString.Append("                          ELSE REGEXP_REPLACE(MAT_GRP_4, '[^[:digit:]]')" + "\n");
                strSqlString.Append("                      END AS PRICE" + "\n");
                //strSqlString.Append("                     , CASE WHEN MAT_GRP_4 IN ('-', 'FD', 'FU') THEN '1' ELSE SUBSTR(MAT_GRP_4, 3) END AS PRICE " + "\n");
            }
            else
            {
                strSqlString.Append("                     , 1 AS PRICE" + "\n");
            }

            strSqlString.Append("                  FROM VWIPMATDEF A" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND A.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND A.MAT_GRP_2 NOT IN ('-') " + "\n");
            strSqlString.Append("                   AND A.MAT_GRP_3 NOT IN ('COB') " + "\n");

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
            {
                strSqlString.Append("                   AND A.MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");
            }

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                   AND A.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

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
            #endregion

            strSqlString.Append("               ) MAT" + "\n");
            strSqlString.Append("             , (                " + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");

            if (ckbRevPlan.Checked == true)
            {
                strSqlString.Append("                     , SUM(RESV_FIELD2) AS MON_PLN" + "\n");
            }
            else
            {
                strSqlString.Append("                     , SUM(PLAN_QTY_TEST) AS MON_PLN" + "\n");
            }

            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID, SUM(PLAN_QTY_TEST) AS PLAN_QTY_TEST, SUM(TO_NUMBER(DECODE(RESV_FIELD2,' ',0,RESV_FIELD2))) AS RESV_FIELD2" + "\n");
            strSqlString.Append("                          FROM CWIPPLNMON " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND PLAN_MONTH = '" + month + "'" + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                       ) " + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
            strSqlString.Append("               ) PLN" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");
            strSqlString.Append("                     , SUM(SHP_QTY_1) AS SHP_TTL " + "\n");
            strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + date + "', SHP_QTY_1, 0)) AS SHP_TODAY" + "\n");
            strSqlString.Append("                  FROM VSUMWIPOUT" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "' " + "\n");
            strSqlString.Append("                   AND FACTORY IN ('" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                   AND CM_KEY_2 = 'PROD' " + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                 GROUP BY MAT_ID" + "\n");          
            strSqlString.Append("               ) SHP" + "\n");           
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");
            strSqlString.Append("                     , SUM(QTY) AS WIP_TTL" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'TZ010', QTY, 0)) AS WIP_TZ010" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID, QTY_1 AS QTY, OPER" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT A.MAT_ID, A.OPER, B.OPER_GRP_1, C.MAT_GRP_1, C.MAT_GRP_4, C.MAT_GRP_5" + "\n");
            strSqlString.Append("                                     , QTY_1" + "\n");            
            strSqlString.Append("                                     , A.HOLD_CODE " + "\n");
            strSqlString.Append("                                     , CASE WHEN A.HOLD_CODE = 'H72' AND A.OPER = 'T0000' THEN 'Y' ELSE 'N' END AS CK_STOCK_H72 " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("                                  FROM RWIPLOTSTS A  " + "\n");
                strSqlString.Append("                                     , MWIPOPRDEF B  " + "\n");
                strSqlString.Append("                                     , VWIPMATDEF C" + "\n");
                strSqlString.Append("                                 WHERE 1 = 1 " + "\n");
            }
            else
            {
                strSqlString.Append("                                  FROM RWIPLOTSTS_BOH A " + "\n");
                strSqlString.Append("                                     , MWIPOPRDEF B " + "\n");
                strSqlString.Append("                                     , VWIPMATDEF C " + "\n");
                strSqlString.Append("                                 WHERE A.CUTOFF_DT = '" + cdvDate.SelectedValue() + "22' " + "\n");
            }

            strSqlString.Append("                                   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                                   AND A.FACTORY = C.FACTORY" + "\n");
            strSqlString.Append("                                   AND A.OPER = B.OPER" + "\n");
            strSqlString.Append("                                   AND A.MAT_ID = C.MAT_ID" + "\n");
            strSqlString.Append("                                   AND A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'  " + "\n");
            strSqlString.Append("                                   AND A.LOT_TYPE = 'W' " + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                                   AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "' " + "\n");
            }

            strSqlString.Append("                                   AND A.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                                   AND C.MAT_GRP_2 <> '-'" + "\n");
            strSqlString.Append("                                   AND C.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                   AND A.HOLD_CODE NOT IN ('H71','H54') " + "\n");
            strSqlString.Append("                               )" + "\n");
            strSqlString.Append("                         WHERE CK_STOCK_H72 = 'N'" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
            strSqlString.Append("               ) WIP" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER = 'T0000' THEN S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1 ELSE 0 END) RCV_TODAY" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN T0100_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER = 'T0960' THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END)" + "\n");
            strSqlString.Append("                                WHEN T0500_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER = 'T0960' THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END)" + "\n");
            strSqlString.Append("                                WHEN OPER IN ('T0100', 'T0400', 'T0500', 'T0960', 'S0960') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1" + "\n");
            strSqlString.Append("                                ELSE 0" + "\n");           
            strSqlString.Append("                             END) T_OUT_TODAY" + "\n");          
            strSqlString.Append("                  FROM (    " + "\n");
            strSqlString.Append("                        SELECT (SELECT COUNT(*) FROM MWIPFLWOPR@RPTTOMES FLW WHERE 1=1 AND FLW.OPER = 'T0100' AND FLW.FLOW = A.FLOW) AS T0100_CNT" + "\n");
            strSqlString.Append("                             , (SELECT COUNT(*) FROM MWIPFLWOPR@RPTTOMES FLW WHERE 1=1 AND FLW.OPER = 'T0500' AND FLW.FLOW = A.FLOW) AS T0500_CNT" + "\n");
            strSqlString.Append("                             , (SELECT COUNT(*) FROM MWIPFLWOPR@RPTTOMES FLW WHERE 1=1 AND FLW.OPER = 'T0960' AND FLW.FLOW = A.FLOW) AS T0960_CNT" + "\n");
            strSqlString.Append("                             , A.OPER AS OPER" + "\n");
            strSqlString.Append("                             , A.S1_OPER_IN_QTY_1, A.S2_OPER_IN_QTY_1, A.S3_OPER_IN_QTY_1" + "\n");
            strSqlString.Append("                             , A.S1_END_QTY_1, A.S2_END_QTY_1, A.S3_END_QTY_1" + "\n");
            strSqlString.Append("                             , A.S1_END_RWK_QTY_1, A.S2_END_RWK_QTY_1, A.S3_END_RWK_QTY_1" + "\n");
            strSqlString.Append("                             , B.*" + "\n");
            strSqlString.Append("                          FROM RSUMWIPMOV A" + "\n");
            strSqlString.Append("                             , VWIPMATDEF B" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                           AND A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND A.WORK_DATE = '" + date + "'" + "\n");
            strSqlString.Append("                           AND A.LOT_TYPE = 'W'" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                        AND A.CM_KEY_3 LIKE '" + cdvLotType.Text + "' " + "\n");
            }

            strSqlString.Append("                            AND REGEXP_LIKE(A.OPER, 'T0000|T0100|T0400|T0500|T0960|S0960')" + "\n");        
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
            strSqlString.Append("               ) MOV" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = MOV.MAT_ID(+)" + "\n");
            strSqlString.Append("       ) A" + "\n");
            strSqlString.Append(" GROUP BY " + QueryCond2 + "\n");
            strSqlString.Append(" HAVING SUM(MON_PLN) + SUM(SHP_TTL) + SUM(WIP_TTL) + SUM(RCV_TODAY) + SUM(T_OUT_TODAY) + SUM(SHP_TODAY) <> 0" + "\n");
            strSqlString.Append(" ORDER BY DECODE(MAT_GRP_1, 'SE', 1, 'IM', 2, 'FC', 3, 'AB', 4, 5), " + QueryCond3 + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion


        #region EVENT 처리
        /// <summary>
        /// 6. View 버튼 Action
        /// </summary>
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            if (CheckField() == false) return;

            GridColumnInit();
            LabelTextChange();

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
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 11, null, null, btnSort);
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);

                // Grand TTL, Sub TTL 진도차 값 공식으로 구하기
                SetAvgVertical(1, 13, 15, 17);

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
            this.SetFactory(cdvFactory.txtValue);
        }

        /// <summary>
        /// 7. 상단 Lebel 표시
        /// </summary>
        private void LabelTextChange()
        {
            DateTime getStartDate = Convert.ToDateTime(cdvDate.Value.ToString("yyyy-MM") + "-01");
            DateTime baseTime = DateTime.ParseExact(DateTime.Now.ToString("yyyyMMdd") + "220000", "yyyyMMddHHmmss", null);
            TimeSpan diff = baseTime - DateTime.Now;

            string getEndDate = getStartDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");
            string strDate = cdvDate.Value.ToString("yyyyMMdd");
            string sToday = strDate.Substring(6, 2);
            string sLastday = getEndDate.Substring(6, 2);

            iToday = Convert.ToInt32(sToday);
            iLastday = Convert.ToInt32(sLastday);

            double jindo = (Convert.ToDouble(sToday)) / Convert.ToDouble(sLastday) * 100;

            // 진도율 소수점 1째자리 까지 표시 (2009.08.17 임종우)
            jindoPer = Math.Round(Convert.ToDecimal(jindo), 1);

            lblToday.Text = sToday + " day";
            lblLastDay.Text = sLastday + " day";

            // 금일 조회일 경우 잔여일에 금일 포함함.
            if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
            {
                iRemain = (iLastday - iToday) + (diff.TotalHours / 24);  // 2019-09-19-임종우 : 금일의 경우 잔여일 계산 시 시간 단위까지 계산되도록 수정 (임태성차장 요청)
            }
            else
            {
                iRemain = (iLastday - iToday);
            }

            lblRemain.Text = Math.Round(iRemain, 2).ToString() + " day";
            lblJindo.Text = jindoPer.ToString() + "%";

        }

        /// <summary>
        /// Grand TTL, Sub TTL 진도차, 재공일수 직접 계산
        /// </summary>
        /// <param name="nSampleNormalRowPos"></param>
        /// <param name="nColPosTO"></param>
        /// <param name="nColPosSemi_Mat"></param>
        public void SetAvgVertical(int nSampleNormalRowPos, int nColPosTO, int nColPosSemi_Mat, int nColPosWipDay)
        {
            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPosTO].BackColor;
            double iMonPlan = 0;
            double iAO = 0;
            double iWF = 0;
            double iWip = 0;

            iMonPlan = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 11].Value);
            iAO = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 12].Value);
            iWF = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 14].Value);
            iWip = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 16].Value);

            // 분모값이 0이 아닐경우에만 계산한다..
            if (iMonPlan != 0)
            {
                spdData.ActiveSheet.Cells[0, nColPosTO].Value = (iAO / (iMonPlan / iLastday)) - iToday;
                spdData.ActiveSheet.Cells[0, nColPosSemi_Mat].Value = ((iWF / iMonPlan) - ((Convert.ToDouble(iToday) + 2) / Convert.ToDouble(iLastday))) * iLastday;
                spdData.ActiveSheet.Cells[0, nColPosWipDay].Value = iWip / (iMonPlan / iLastday);

                for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    if (spdData.ActiveSheet.Cells[i, nColPosTO].BackColor != color)
                    {
                        iMonPlan = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 11].Value);
                        iAO = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 12].Value);
                        iWF = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 14].Value);
                        iWip = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 16].Value);

                        if (iMonPlan != 0)
                        {
                            spdData.ActiveSheet.Cells[i, nColPosTO].Value = (iAO / (iMonPlan / iLastday)) - iToday;
                            // 스프레드에 표시 된 데이터는 이미 (실적 + 재공) 이 계산 된 상태 이므로 해당 실적 데이터만 사용 한다.
                            spdData.ActiveSheet.Cells[i, nColPosSemi_Mat].Value = ((iWF / iMonPlan) - ((Convert.ToDouble(iToday) + 2) / Convert.ToDouble(iLastday))) * iLastday;
                            spdData.ActiveSheet.Cells[i, nColPosWipDay].Value = iWip / (iMonPlan / iLastday);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
