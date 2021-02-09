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
    public partial class PRD010806 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010806<br/>
        /// 클래스요약: 제품별 환산 기준정보<br/>
        /// 작  성  자: 임종우<br/>
        /// 최초작성일: 2016-07-21<br/>
        /// 상세  설명: 제품별 환산 기준정보(최인남상무 요청)<br/>
        /// 변경  내용: <br/>     
        /// 2016-08-02-임종우 : UPEH 정보 추가 (최인남상무 요청)
        /// </summary>
        public PRD010806()
        {
            InitializeComponent();                                   
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
            
            try
            {
                spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("MAJOR CODE", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PACKAGE2", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD COUNT", 0, 3, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PKG CODE", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("FAMILY", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("TYPE1", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("TYPE2", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                
                spdData.RPT_AddBasicColumn("COMP QTY", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("NET DIE", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WIRE CNT", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("단가", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("UPH", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PRE_BG", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("STEALTH_SAW", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("BG", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("SAW", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 13, 4);

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
                spdData.RPT_MerageHeaderRowSpan(0, 12, 2);

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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT.MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "MAT.MAT_GRP_9", "MAT.MAT_GRP_9 AS MAJOR", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT.MAT_GRP_10", "MAT.MAT_GRP_10 AS PKG", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6 AS LD_COUNT", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT.MAT_ID", "MAT.MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "MAT.MAT_CMF_11", "MAT.MAT_CMF_11 AS PKG_CODE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2 AS FAMILY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4 AS TYPE1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5 AS TYPE2", true);           
            
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
            string sPriceType;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;


            if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                sPriceType = "A0";
            else
                sPriceType = "0T";

            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond2);
            strSqlString.Append("     , COMP_CNT" + "\n");
            strSqlString.Append("     , NET_DIE" + "\n");
            strSqlString.Append("     , DECODE(WIRE_CNT, NULL, MAT_GRP_6, WIRE_CNT) AS WIRE_CNT" + "\n");
            strSqlString.Append("     , PRICE" + "\n");
            strSqlString.Append("     , PRE_BG, STEALTH_SAW, BG, SAW" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT MAT_ID, MAT_GRP_1, MAT_GRP_2, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6, MAT_GRP_9, MAT_GRP_10, MAT_CMF_11" + "\n");
            strSqlString.Append("             , MAX(COMP_CNT) AS COMP_CNT, MAX(NET_DIE) AS NET_DIE, MAX(PRICE) AS PRICE" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT CASE WHEN A.MAT_GRP_1 = 'HX' THEN A.MAT_CMF_10" + "\n");
            strSqlString.Append("                            WHEN A.MAT_ID LIKE 'SEK%' THEN 'SEK_________-___' || SUBSTR(A.MAT_ID, -3)" + "\n");
            strSqlString.Append("                            ELSE A.MAT_ID" + "\n");
            strSqlString.Append("                       END MAT_ID" + "\n");
            strSqlString.Append("                     , A.MAT_GRP_1" + "\n");
            strSqlString.Append("                     , A.MAT_GRP_2" + "\n");            
            strSqlString.Append("                     , A.MAT_GRP_4" + "\n");
            strSqlString.Append("                     , A.MAT_GRP_5" + "\n");
            strSqlString.Append("                     , A.MAT_GRP_6" + "\n");
            strSqlString.Append("                     , A.MAT_GRP_9" + "\n");
            strSqlString.Append("                     , A.MAT_GRP_10 " + "\n");
            strSqlString.Append("                     , A.MAT_CMF_11 " + "\n");
            strSqlString.Append("                     , A.COMP_CNT" + "\n");
            strSqlString.Append("                     , A.NET_DIE " + "\n");
            strSqlString.Append("                     , CASE WHEN B.PRICE IS NULL OR B.PRICE = 1 THEN C.PKG_PRICE" + "\n");
            strSqlString.Append("                            ELSE B.PRICE" + "\n");
            strSqlString.Append("                       END PRICE" + "\n");
            strSqlString.Append("                  FROM VWIPMATDEF A" + "\n");
            strSqlString.Append("                     , (        " + "\n");
            strSqlString.Append("                        SELECT MAT_ID, MAX(PRICE) AS PRICE" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT MAT_ID, DECODE(MCP_TO_PART, PRODUCT, PRICE, 1) AS PRICE" + "\n");
            strSqlString.Append("                                  FROM RWIPMCPBOM A" + "\n");
            strSqlString.Append("                                     , (" + "\n");
            strSqlString.Append("                                        SELECT PRODUCT, PRICE" + "\n");
            strSqlString.Append("                                          FROM RPRIMATDAT" + "\n");
            strSqlString.Append("                                         WHERE 1=1 " + "\n");
            strSqlString.Append("                                           AND SUBSTR(ITEM_CD,10,2) = '" + sPriceType + "'  " + "\n");
            strSqlString.Append("                                       ) B " + "\n");
            strSqlString.Append("                                 WHERE MCP_TO_PART = PRODUCT(+) " + "\n");
            strSqlString.Append("                                   AND MAT_ID IS NOT NULL " + "\n");
            strSqlString.Append("                                 UNION " + "\n");
            strSqlString.Append("                                SELECT PRODUCT, PRICE " + "\n");
            strSqlString.Append("                                  FROM RPRIMATDAT " + "\n");
            strSqlString.Append("                                 WHERE 1=1 " + "\n");
            strSqlString.Append("                                   AND SUBSTR(ITEM_CD,10,2) = '" + sPriceType + "' " + "\n");
            strSqlString.Append("                               )" + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                       ) B" + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT B.MAT_GRP_1 AS CUST_CODE, B.MAT_CMF_11 AS PKG_CODE, ROUND(AVG(PRICE), 0) AS PKG_PRICE" + "\n");
            strSqlString.Append("                          FROM RPRIMATDAT A" + "\n");
            strSqlString.Append("                             , MWIPMATDEF B" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND A.PRODUCT = B.MAT_ID" + "\n");
            strSqlString.Append("                           AND B.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                           AND B.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND SUBSTR(ITEM_CD,10,2) = '" + sPriceType + "' " + "\n");
            strSqlString.Append("                           AND NVL(A.PRICE,0) > 0 " + "\n");
            strSqlString.Append("                           AND A.PRODUCT LIKE 'SE%' " + "\n");
            strSqlString.Append("                         GROUP BY MAT_GRP_1, MAT_CMF_11" + "\n");
            strSqlString.Append("                       ) C" + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("                   AND A.MAT_GRP_1 = C.CUST_CODE(+)" + "\n");
            strSqlString.Append("                   AND A.MAT_CMF_11 = C.PKG_CODE(+)" + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                   AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND A.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                   AND A.MAT_GRP_2 <> '-'" + "\n");
            strSqlString.Append("                   AND A.MAT_GRP_5 NOT LIKE 'Middle%'" + "\n");
            strSqlString.Append("                   AND A.MAT_GRP_5 <> 'Merge'" + "\n");

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);
            }

            #region 상세조회
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
            #endregion

            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID, MAT_GRP_1, MAT_GRP_2, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6, MAT_GRP_9, MAT_GRP_10, MAT_CMF_11" + "\n");
            strSqlString.Append("       ) MAT" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID" + "\n");
            strSqlString.Append("             , SUM(REV_PLN) AS REV_PLN" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT CASE WHEN B.MAT_GRP_1 = 'HX' THEN B.MAT_CMF_10" + "\n");
            strSqlString.Append("                            WHEN B.MAT_ID LIKE 'SEK%' THEN 'SEK_________-___' || SUBSTR(B.MAT_ID, -3)" + "\n");
            strSqlString.Append("                            ELSE B.MAT_ID" + "\n");
            strSqlString.Append("                       END MAT_ID " + "\n");

            if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
            {
                strSqlString.Append("                     , TO_NUMBER(DECODE(RESV_FIELD1,' ',0,RESV_FIELD1)) AS REV_PLN" + "\n");
            }
            else
            {
                strSqlString.Append("                     , TO_NUMBER(DECODE(RESV_FIELD2,' ',0,RESV_FIELD2)) AS REV_PLN" + "\n");
            }

            strSqlString.Append("                  FROM CWIPPLNMON A" + "\n");
            strSqlString.Append("                     , MWIPMATDEF B" + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                   AND A.PLAN_MONTH = TO_CHAR(SYSDATE, 'YYYYMM')" + "\n");
            strSqlString.Append("                   AND B.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("         WHERE REV_PLN > 0" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID  " + "\n");
            strSqlString.Append("       ) PLN" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID, STACK, MAX(WIRE_CNT) AS WIRE_CNT" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT CASE WHEN B.MAT_GRP_1 = 'HX' THEN B.MAT_CMF_10" + "\n");
            strSqlString.Append("                            WHEN B.MAT_ID LIKE 'SEK%' THEN 'SEK_________-___' || SUBSTR(B.MAT_ID, -3)" + "\n");
            strSqlString.Append("                            ELSE B.MAT_ID" + "\n");
            strSqlString.Append("                       END MAT_ID" + "\n");
            strSqlString.Append("                     , CASE WHEN A.OPER = 'A0600' THEN B.MAT_GRP_5" + "\n");
            strSqlString.Append("                            WHEN A.OPER = 'A0601' THEN '1st'" + "\n");
            strSqlString.Append("                            WHEN A.OPER = 'A0602' THEN '2nd'" + "\n");
            strSqlString.Append("                            WHEN A.OPER = 'A0603' THEN '3rd'" + "\n");
            strSqlString.Append("                            WHEN A.OPER = 'A0604' THEN '4th'" + "\n");
            strSqlString.Append("                            WHEN A.OPER = 'A0605' THEN '5th'" + "\n");
            strSqlString.Append("                            WHEN A.OPER = 'A0606' THEN '6th'" + "\n");
            strSqlString.Append("                            WHEN A.OPER = 'A0607' THEN '7th'" + "\n");
            strSqlString.Append("                            WHEN A.OPER = 'A0608' THEN '8th'" + "\n");
            strSqlString.Append("                            WHEN A.OPER = 'A0609' THEN '9th'" + "\n");
            strSqlString.Append("                            WHEN A.OPER = 'A060A' THEN '10th'" + "\n");
            strSqlString.Append("                            WHEN A.OPER = 'A060B' THEN '11th'" + "\n");
            strSqlString.Append("                            WHEN A.OPER = 'A060C' THEN '12th'" + "\n");
            strSqlString.Append("                            WHEN A.OPER = 'A060D' THEN '13th'" + "\n");
            strSqlString.Append("                            WHEN A.OPER = 'A060E' THEN '14th'" + "\n");
            strSqlString.Append("                            WHEN A.OPER = 'A060F' THEN '15th'" + "\n");
            strSqlString.Append("                            WHEN A.OPER = 'A060G' THEN '16th'" + "\n");
            strSqlString.Append("                            WHEN A.OPER = 'A060H' THEN '17th'" + "\n");
            strSqlString.Append("                            WHEN A.OPER = 'A060I' THEN '18th'" + "\n");
            strSqlString.Append("                            WHEN A.OPER = 'A060J' THEN '19th'" + "\n");
            strSqlString.Append("                            WHEN A.OPER = 'A060K' THEN '20th'" + "\n");
            strSqlString.Append("                            ELSE ''" + "\n");
            strSqlString.Append("                       END STACK" + "\n");
            strSqlString.Append("                     , A.TCD_CMF_2 AS WIRE_CNT" + "\n");
            strSqlString.Append("                  FROM CWIPTCDDEF@RPTTOMES A" + "\n");
            strSqlString.Append("                     , MWIPMATDEF B" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID   " + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                   AND A.OPER LIKE 'A060%' " + "\n");
            strSqlString.Append("                   AND A.SET_FLAG = 'Y' " + "\n");
            strSqlString.Append("                   AND A.TCD_CMF_2 <> ' '    " + "\n");
            strSqlString.Append("                   AND B.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID, STACK " + "\n");
            strSqlString.Append("       ) WIR" + "\n");            
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID, MAT_GRP_5" + "\n");
            strSqlString.Append("             , ROUND(AVG(DECODE(OPER, 'A0030', UPEH)),1) AS PRE_BG" + "\n");
            strSqlString.Append("             , ROUND(AVG(DECODE(OPER, 'A0033', UPEH)),1) AS STEALTH_SAW" + "\n");
            strSqlString.Append("             , ROUND(AVG(DECODE(OPER, 'A0040', UPEH)),1) AS BG" + "\n");
            strSqlString.Append("             , ROUND(AVG(DECODE(OPER, 'A0200', UPEH)),1) AS SAW " + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT CASE WHEN B.MAT_GRP_1 = 'HX' THEN B.MAT_CMF_10" + "\n");
            strSqlString.Append("                            WHEN B.MAT_ID LIKE 'SEK%' THEN 'SEK_________-___' || SUBSTR(B.MAT_ID, -3)" + "\n");
            strSqlString.Append("                            ELSE B.MAT_ID" + "\n");
            strSqlString.Append("                       END MAT_ID" + "\n");
            strSqlString.Append("                     , A.OPER, A.RES_MODEL, A.UPEH" + "\n");
            strSqlString.Append("                     , B.MAT_GRP_5" + "\n");
            strSqlString.Append("                  FROM CRASUPHDEF A" + "\n");
            strSqlString.Append("                     , VWIPMATDEF B" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID           " + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND B.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND B.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                   AND A.OPER IN ('A0030', 'A0033', 'A0040', 'A0200')" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND OPER IN ('A0030', 'A0033', 'A0040', 'A0200')" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID, MAT_GRP_5" + "\n");
            strSqlString.Append("       ) UPH" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");

            if (ckbPlan.Checked == true)
            {
                strSqlString.Append("   AND MAT.MAT_ID = PLN.MAT_ID" + "\n");
            }
            else
            {
                strSqlString.Append("   AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
            }
            
            strSqlString.Append("   AND MAT.MAT_ID = WIR.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = UPH.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_GRP_5 = WIR.STACK(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_GRP_5 = UPH.MAT_GRP_5(+)" + "\n");

            strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond1);            

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

                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 13, 13, null, null, btnSort);                
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함
        
                //Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 5, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.DataSource = dt;
                spdData.RPT_AutoFit(false);

                for (int i = 0; i <= 8; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LightYellow;
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

        
        #endregion

    }       
}
