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

namespace Hana.MAT
{
    public partial class MAT070401 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: MAT070401<br/>
        /// 클래스요약: 당월 주요 원부자재 소요량 산출<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2010-07-29<br/>
        /// 상세  설명: 당월 주요 원부자재 소요량 산출<br/>
        /// 변경  내용: <br/>
        /// 2011-11-21-임종우 : 월 계획 제품 중복 오류 수정
        /// 2011-12-08-김민우 : 창고재고 “ 값 = “ 원부자재 재공 현황 “ 화면의 “ 창고재고 + 현장창고재고 “ 값으로 수정 요청 
        ///                     자재코드 기준 조회 시 잔량 값 계산 안됨
        /// 2011-12-12-김민우 : 예상소진일 항목 추가 (재고TTL/ 일필요량)
        /// 2011-12-12-김민우 : 소요량 항목 계산값 수정 ( 월계획/Usage --> 월계획 * Usage )
        /// 2020-03-03-김미경 : usage 수정 =: A0000~A0399 공정의 원부자재에 한해 원부자재 사용량(Usage) = Usage * LOSS_QTY(H_HX_AUTO_LOSS, H_SEC_AUTO_LOSS ADAPT_OPER 'A0395'의 LOSS_QTY) - 이승희 D

        #region " MAT070401 : Program Initial "

        public MAT070401()
        {
            InitializeComponent();            
            SortInit();
            GridColumnInit();
            cdvDate.Value = DateTime.Now;            
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.cdvMatCode.sFactory = GlobalVariable.gsAssyDefaultFactory;
        }

        #endregion
        

        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (String.IsNullOrEmpty(cdvMatType.Text))
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD003", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        #endregion


        #region " GridColumnInit : Sheet Title 설정 "

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {            
            try
            {
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD Count", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);                
                spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 200);
                spdData.RPT_AddBasicColumn("Cust Device", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Material code", 0, 11, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);

                spdData.RPT_AddBasicColumn("Monthly plan", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("STEP", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);                
                spdData.RPT_AddBasicColumn("Item name", 0, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 200);
                spdData.RPT_AddBasicColumn("Unit", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("usage", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("Required amount ", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("Use Q'ty", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("Daily requirement", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("Average usage over last 3 days", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);

                spdData.RPT_AddBasicColumn("Estimated exhaustion date", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);                


                spdData.RPT_AddBasicColumn("Inventory status", 0, 23, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("Warehouse stock", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("On-site inventory", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("TTL", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 23, 3);

                for (int i = 0; i <= 22; i++)
                {
                    spdData.RPT_MerageHeaderRowSpan(0, i, 2);                 
                }

                // Group항목이 있을경우 반드시 선언해줄것.
                spdData.RPT_ColumnConfigFromTable(btnSort);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        #endregion


        #region " SortInit : Group By 설정 "

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            try
            {                
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS MAT_GRP_1", "MAT.MAT_GRP_1", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT_GRP_2", "MAT_GRP_2", "MAT.MAT_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT_GRP_6", "MAT_GRP_6", "MAT.MAT_GRP_6", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT_GRP_3", "MAT_GRP_3", "MAT.MAT_GRP_3", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT_GRP_4", "MAT_GRP_4", "MAT.MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT_GRP_5", "MAT_GRP_5", "MAT.MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT_GRP_7", "MAT_GRP_7", "MAT.MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT_GRP_8", "MAT_GRP_8", "MAT.MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT_CMF_10", "MAT_CMF_10", "MAT.MAT_CMF_10", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT_ID", "MAT_ID", "MAT.MAT_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST DEVICE", "MAT_CMF_7", "MAT_CMF_7", "MAT.MAT_CMF_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Material code", "MATCODE", "MATCODE", "MAT.MATCODE", true);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        #endregion

        

        #region " MakeSqlString : Sql Query문 "

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string stToday = null;
            string stMonth = null;
            
            StringBuilder strSqlString = new StringBuilder();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
                        
            stToday = cdvDate.Value.ToString("yyyyMMdd"); // 현재 조회일
            stMonth = cdvDate.Value.ToString("yyyyMM");

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
                        
            strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond2);
            strSqlString.Append("     , SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY" + "\n");
            strSqlString.Append("     , STEPID, DESCRIPT, UNIT" + "\n");
            strSqlString.Append("     , MAX(CASE WHEN STEPID BETWEEN 'A0000' AND 'A0399' THEN LOSS_QTY * USAGE ELSE USAGE END) USAGE" + "\n");
            strSqlString.Append("     , ROUND(SUM(SO_QTY), 1) AS SO_QTY" + "\n");



            strSqlString.Append("     , ROUND(SUM(DECODE(MAT_TYPE, 'TE', TE_USE_QTY, M_USE_QTY_1)), 1) AS USE_QTY" + "\n");

            strSqlString.Append("     , ROUND(CASE WHEN SUM(NVL(SO_QTY,0)) = 0 THEN 0" + "\n");
            strSqlString.Append("                      WHEN SUM(NVL(SO_QTY,0)) - SUM(NVL(DECODE(MAT_TYPE, 'TE', TE_USE_QTY, M_USE_QTY_1),0)) < 0 THEN 0" + "\n");
            strSqlString.Append("                      ELSE SUM(NVL(SO_QTY,0)) - SUM(NVL(DECODE(MAT_TYPE, 'TE', TE_USE_QTY, M_USE_QTY_1),0)) " + "\n");
            strSqlString.Append("                 END, 1) AS REMAIN_QTY" + "\n");
            strSqlString.Append("     , ROUND((CASE WHEN SUM(NVL(SO_QTY,0)) = 0 THEN 0" + "\n");
            strSqlString.Append("                       WHEN SUM(NVL(SO_QTY,0)) - SUM(NVL(DECODE(MAT_TYPE, 'TE', TE_USE_QTY, M_USE_QTY_1),0)) < 0 THEN 0" + "\n");
            strSqlString.Append("                       ELSE SUM(NVL(SO_QTY,0)) - SUM(NVL(DECODE(MAT_TYPE, 'TE', TE_USE_QTY, M_USE_QTY_1),0)) " + "\n");
            strSqlString.Append("                  END / (SELECT LAST_DAY(TO_DATE('" + stToday + "', 'YYYYMMDD')) - TO_DATE('" + stToday + "', 'YYYYMMDD') LASTDAY FROM DUAL)), 1) AS DAY_QTY" + "\n");
            
            
            /*
            strSqlString.Append("     , ROUND(SUM(CASE WHEN NVL(SO_QTY,0) = 0 THEN 0" + "\n");
            strSqlString.Append("                      WHEN NVL(SO_QTY,0) - NVL(DECODE(MAT_TYPE, 'TE', TE_USE_QTY, M_USE_QTY_1),0) < 0 THEN 0" + "\n");
            strSqlString.Append("                      ELSE NVL(SO_QTY,0) - NVL(DECODE(MAT_TYPE, 'TE', TE_USE_QTY, M_USE_QTY_1),0) " + "\n");
            strSqlString.Append("                 END), 1) AS REMAIN_QTY" + "\n");
            strSqlString.Append("     , ROUND(SUM((CASE WHEN NVL(SO_QTY,0) = 0 THEN 0" + "\n");
            strSqlString.Append("                       WHEN NVL(SO_QTY,0) - NVL(DECODE(MAT_TYPE, 'TE', TE_USE_QTY, M_USE_QTY_1),0) < 0 THEN 0" + "\n");
            strSqlString.Append("                       ELSE NVL(SO_QTY,0) - NVL(DECODE(MAT_TYPE, 'TE', TE_USE_QTY, M_USE_QTY_1),0) " + "\n");
            strSqlString.Append("                  END) / (SELECT LAST_DAY(TO_DATE('" + stToday + "', 'YYYYMMDD')) - TO_DATE('" + stToday + "', 'YYYYMMDD') LASTDAY FROM DUAL)), 1) AS DAY_QTY" + "\n");
           */
            
            strSqlString.Append("     , ROUND(SUM(AVG_USE_QTY), 1) AS AVG_USE_QTY" + "\n");
            //1212

            strSqlString.Append("     , ROUND(DECODE(ROUND((CASE WHEN SUM(NVL(SO_QTY,0)) = 0 THEN 0" + "\n");
            strSqlString.Append("                       WHEN SUM(NVL(SO_QTY,0)) - SUM(NVL(DECODE(MAT_TYPE, 'TE', TE_USE_QTY, M_USE_QTY_1),0)) < 0 THEN 0" + "\n");
            strSqlString.Append("                       ELSE SUM(NVL(SO_QTY,0)) - SUM(NVL(DECODE(MAT_TYPE, 'TE', TE_USE_QTY, M_USE_QTY_1),0)) " + "\n");
            strSqlString.Append("                       END / (SELECT LAST_DAY(TO_DATE('" + stToday + "', 'YYYYMMDD')) - TO_DATE('" + stToday + "', 'YYYYMMDD') LASTDAY FROM DUAL)), 1),0,0, " + "\n");
            strSqlString.Append("                   ROUND(INV_QTY + L_IN, 1) / ROUND((CASE WHEN SUM(NVL(SO_QTY,0)) = 0 THEN 0" + "\n");
            strSqlString.Append("                                                          WHEN SUM(NVL(SO_QTY,0)) - SUM(NVL(DECODE(MAT_TYPE, 'TE', TE_USE_QTY, M_USE_QTY_1),0)) < 0 THEN 0" + "\n");
            strSqlString.Append("                                                          ELSE SUM(NVL(SO_QTY,0)) - SUM(NVL(DECODE(MAT_TYPE, 'TE', TE_USE_QTY, M_USE_QTY_1),0)) " + "\n");
            strSqlString.Append("                                                          END / (SELECT LAST_DAY(TO_DATE('" + stToday + "', 'YYYYMMDD')) - TO_DATE('" + stToday + "', 'YYYYMMDD') LASTDAY FROM DUAL)), 1)),1) AS EXPIRY_DAY" + "\n");




  

            strSqlString.Append("     , ROUND(INV_QTY, 1) AS INV_QTY" + "\n");
            strSqlString.Append("     , ROUND(L_IN, 1) AS L_IN " + "\n");
            strSqlString.Append("     , ROUND(INV_QTY + L_IN, 1) AS TTL" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.AppendFormat("        SELECT {0}" + "\n", QueryCond3);            
            strSqlString.Append("             , PLAN_QTY_ASSY, STEPID, DESCRIPT, UNIT" + "\n");
            strSqlString.Append("             , PLAN_QTY_ASSY * (CASE WHEN MAT_TYPE <> 'TE' THEN DECODE(UNIT_QTY, 0, 1, UNIT_QTY / PAR_BASE_QTY)" + "\n");
            strSqlString.Append("                                     ELSE DECODE(NVL(MAT_CMF_13, ' '), ' ', 1, TO_NUMBER(REPLACE(MAT_CMF_13,',','')))" + "\n");
            strSqlString.Append("                                END) AS SO_QTY" + "\n");
            strSqlString.Append("             , CASE WHEN MAT_TYPE <> 'TE' THEN UNIT_QTY / PAR_BASE_QTY" + "\n");
            strSqlString.Append("                    ELSE DECODE(NVL(MAT_CMF_13, ' '), ' ', 0, TO_NUMBER(REPLACE(MAT_CMF_13,',','')))" + "\n");
            strSqlString.Append("               END AS USAGE                    " + "\n");
            strSqlString.Append("             , MAT_CMF_13, MAT_TYPE" + "\n");
            strSqlString.Append("             , CASE WHEN MAT_TYPE <> 'TE' THEN 0" + "\n");
            strSqlString.Append("                    ELSE (BG_QTY / DECODE(NVL(MAT_CMF_13, ' '), ' ', 1, TO_NUMBER(REPLACE(MAT_CMF_13,',',''))))" + "\n");
            strSqlString.Append("               END AS TE_USE_QTY" + "\n");
            strSqlString.Append("             , M_USE_QTY_1, ROUND(AVG_USE_QTY,2) AS AVG_USE_QTY, INV_QTY, L_IN" + "\n");
            strSqlString.Append("             , NVL(GCM.DATA_1, 1) AS LOSS_QTY" + "\n");
            strSqlString.Append("          FROM (  " + "\n");
            strSqlString.Append("                SELECT A.MAT_GRP_1, A.MAT_GRP_2, A.MAT_GRP_3, A.MAT_GRP_4, A.MAT_GRP_5, A.MAT_GRP_6, A.MAT_GRP_7, A.MAT_GRP_8, A.MAT_GRP_9, A.MAT_CMF_7, A.MAT_CMF_10, A.MAT_CMF_13, A.MAT_ID  " + "\n");
            strSqlString.Append("                     , B.STEPID, B.MATCODE, B.DESCRIPT, B.PAR_BASE_QTY, B.UNIT_QTY, B.UNIT, B.RESV_FIELD_2 AS MAT_TYPE " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF@RPTTOMES A  " + "\n");
            strSqlString.Append("                     , CWIPBOMDEF B  " + "\n");
            strSqlString.Append("                 WHERE 1=1  " + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.PARTNUMBER(+)  " + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                   AND A.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND A.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND B.RESV_FLAG_1(+) = 'Y'  " + "\n");
            strSqlString.Append("                   AND B.RESV_FIELD_2(+) <> 'WW'           " + "\n");
            strSqlString.Append("               ) MAT" + "\n");
            strSqlString.Append("             , (   " + "\n");
            strSqlString.Append("                 SELECT FACTORY,MAT_ID,PLAN_QTY_ASSY,PLAN_MONTH   " + "\n");
            strSqlString.Append("                   FROM (   " + "\n");
            strSqlString.Append("                          SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, PLAN_MONTH   " + "\n");
            strSqlString.Append("                            FROM CWIPPLNMON   " + "\n");
            strSqlString.Append("                           WHERE 1=1   " + "\n");
            strSqlString.Append("                             AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'   " + "\n");
            strSqlString.Append("                             AND MAT_ID NOT LIKE 'SES%'   " + "\n");
            strSqlString.Append("                           GROUP BY FACTORY, MAT_ID, PLAN_MONTH " + "\n");
            strSqlString.Append("                          UNION ALL   " + "\n");
            strSqlString.Append("                          SELECT FACTORY, MAT_ID, SUM(PLAN_QTY) AS PLAN_QTY_ASSY , '" + stMonth + "' AS PLAN_MONTH   " + "\n");
            strSqlString.Append("                            FROM (   " + "\n");
            strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
            strSqlString.Append("                                     FROM CWIPPLNDAY " + "\n");
            strSqlString.Append("                                    WHERE 1=1 " + "\n");
            strSqlString.Append("                                      AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                      AND PLAN_DAY BETWEEN TO_CHAR(TRUNC(TO_DATE('" + stToday + "', 'YYYYMMDD'), 'MM'), 'YYYYMMDD') AND TO_CHAR(TRUNC(LAST_DAY(TO_DATE('" + stToday + "', 'YYYYMMDD'))), 'YYYYMMDD')  " + "\n");
            strSqlString.Append("                                      AND IN_OUT_FLAG = 'OUT' " + "\n");
            strSqlString.Append("                                      AND CLASS = 'ASSY' " + "\n");
            strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                                   UNION ALL " + "\n");
            strSqlString.Append("                                   SELECT CM_KEY_1 AS FACTORY, MAT_ID" + "\n");
            strSqlString.Append("                                        , SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS PLAN_QTY   " + "\n");
            strSqlString.Append("                                     FROM RSUMFACMOV " + "\n");
            strSqlString.Append("                                    WHERE 1=1 " + "\n");
            strSqlString.Append("                                      AND WORK_DATE BETWEEN TO_CHAR(TRUNC(TO_DATE('" + stToday + "', 'YYYYMMDD'), 'MM'), 'YYYYMMDD') AND TO_CHAR(TRUNC(TO_DATE('" + stToday + "', 'YYYYMMDD'), 'IW') - 1, 'YYYYMMDD')" + "\n");
            strSqlString.Append("                                      AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                                      AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                      AND CM_KEY_3 LIKE 'P%' " + "\n");
            strSqlString.Append("                                      AND MAT_ID LIKE 'SES%' " + "\n");
            strSqlString.Append("                                   GROUP BY CM_KEY_1, MAT_ID " + "\n");
            strSqlString.Append("                                 )  " + "\n");
            strSqlString.Append("                           WHERE 1=1   " + "\n");
            strSqlString.Append("                          GROUP BY FACTORY, MAT_ID   " + "\n");
            strSqlString.Append("                        )  " + "\n");
            strSqlString.Append("                  WHERE PLAN_MONTH = '" + stMonth + "'  " + "\n");
            strSqlString.Append("                    AND PLAN_QTY_ASSY > 0  " + "\n");
            strSqlString.Append("               ) PLN  " + "\n");
            strSqlString.Append("             , (  " + "\n");
            strSqlString.Append("                SELECT P_MAT_ID, M_MAT_ID, OPER, SUM(M_USE_QTY_1) AS M_USE_QTY_1   " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN TO_CHAR(TO_DATE('" + stToday + "', 'YYYYMMDD') - 3, 'YYYYMMDD') AND TO_CHAR(TO_DATE('" + stToday + "', 'YYYYMMDD') - 1, 'YYYYMMDD') THEN M_USE_QTY_1  " + "\n");
            strSqlString.Append("                                ELSE 0  " + "\n");
            strSqlString.Append("                           END) / 3 AS AVG_USE_QTY " + "\n");
            strSqlString.Append("                  FROM RSUMMATMOV  " + "\n");
            strSqlString.Append("                 WHERE 1=1  " + "\n");
            strSqlString.Append("                   AND WORK_DATE BETWEEN  TO_CHAR(TRUNC(TO_DATE('" + stToday + "', 'YYYYMMDD'), 'MM'), 'YYYYMMDD') AND TO_CHAR(TRUNC(LAST_DAY(TO_DATE('" + stToday + "', 'YYYYMMDD'))), 'YYYYMMDD')  " + "\n");
            strSqlString.Append("                 GROUP BY P_MAT_ID, M_MAT_ID, OPER " + "\n");
            strSqlString.Append("               ) BOM  " + "\n");
            strSqlString.Append("             , (  " + "\n");
            strSqlString.Append("                SELECT MAT_ID, SUM(INV_QTY) AS INV_QTY, SUM(L_IN) AS L_IN  " + "\n");
            strSqlString.Append("                  FROM (  " + "\n");
            strSqlString.Append("                        SELECT ITEM_CD AS MAT_ID, SUM(INV_QTY) AS INV_QTY, 0 AS L_IN  " + "\n");
            strSqlString.Append("                          FROM IERPWMSSTS@RPTTOMES " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            //2011-12-08-김민우 : 창고재고 “ 값 = “ 원부자재 재공 현황 “ 화면의 “ 창고재고 + 현장창고재고 “ 값으로 수정 요청 
            //                    기존 AND WH_CD IN ('MH01', 'B10')  변경 AND WH_CD IN ('MH01')
            strSqlString.Append("                           AND WH_CD IN ('MH01', 'B10')             " + "\n");
            strSqlString.Append("                           AND INV_QTY > 0 " + "\n");
            strSqlString.Append("                         GROUP BY ITEM_CD " + "\n");
            strSqlString.Append("                         UNION ALL  " + "\n");
            strSqlString.Append("                        SELECT MAT_ID, 0, SUM(QTY_1) AS QTY_1  " + "\n");
            strSqlString.Append("                          FROM RWIPLOTSTS  " + "\n");
            strSqlString.Append("                         WHERE 1=1  " + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                           AND OPER LIKE 'V%'     " + "\n");
            strSqlString.Append("                           AND LOT_DEL_FLAG = ' '  " + "\n");
            strSqlString.Append("                           AND LOT_TYPE <> 'W'  " + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID  " + "\n");
            strSqlString.Append("                       )  " + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID  " + "\n");
            strSqlString.Append("               ) WIP  " + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");
            strSqlString.Append("                     , SUM(S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1) AS BG_QTY             " + "\n");
            strSqlString.Append("                  FROM RSUMWIPMOV" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND WORK_DATE BETWEEN TO_CHAR(TRUNC(TO_DATE('" + stToday + "', 'YYYYMMDD'), 'MM'), 'YYYYMMDD') AND TO_CHAR(TRUNC(LAST_DAY(TO_DATE('" + stToday + "', 'YYYYMMDD'))), 'YYYYMMDD')" + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                   AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "'           " + "\n");
            strSqlString.Append("                   AND OPER = 'A0040'" + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
            strSqlString.Append("               ) BGE" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT KEY_1, DATA_1" + "\n");
            strSqlString.Append("                  FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND TABLE_NAME IN ('H_SEC_AUTO_LOSS', 'H_HX_AUTO_LOSS')" + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND DATA_4 = 'A0395'" + "\n");
            strSqlString.Append("               ) GCM" + "\n");
            strSqlString.Append("         WHERE 1=1  " + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = BOM.P_MAT_ID(+)  " + "\n");
            strSqlString.Append("           AND MAT.MATCODE = BOM.M_MAT_ID(+)  " + "\n");
            strSqlString.Append("           AND MAT.STEPID = BOM.OPER(+)  " + "\n");
            strSqlString.Append("           AND MAT.MATCODE = WIP.MAT_ID(+) " + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = BGE.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND MAT.MATCODE = GCM.KEY_1(+) " + "\n");
            strSqlString.Append("           AND (MAT.MAT_TYPE <> 'TE' OR (MAT.MAT_TYPE = 'TE' AND MAT.STEPID = 'A0040'))" + "\n");
            //strSqlString.Append("           AND NVL(PLAN_QTY_ASSY,0) + NVL(M_USE_QTY_1,0) + NVL(INV_QTY,0) + NVL(L_IN,0) + NVL(BG_QTY,0) > 0" + "\n");
            strSqlString.Append("           AND MAT.MAT_TYPE = '" + cdvMatType.Text + "'" + "\n");

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                strSqlString.AppendFormat("           AND MAT.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);

            if (cdvMatCode.Text != "ALL" && cdvMatCode.Text != "")
                strSqlString.AppendFormat("           AND MAT.MATCODE {0} " + "\n", cdvMatCode.SelectedValueToQueryString);


            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

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
            #endregion

            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" WHERE 1=1  " + "\n");
            strSqlString.Append("   AND NVL(PLAN_QTY_ASSY,0) + NVL(SO_QTY,0) + NVL(TE_USE_QTY,0) + NVL(M_USE_QTY_1,0) > 0" + "\n");
            strSqlString.AppendFormat(" GROUP BY {0}, STEPID, DESCRIPT, UNIT, USAGE, INV_QTY, L_IN " + "\n", QueryCond1);
            strSqlString.AppendFormat(" ORDER BY {0}, STEPID " + "\n", QueryCond1);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region " Event 처리 "


        #region " btnView_Click : View버튼을 선택했을 때 "

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

                spdData.DataSource = dt;
                ////그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                ////표구성에따른 항목 Display
                ////spdData.RPT_ColumnConfigFromTable(btnSort);
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 11, null, null, btnSort);

                ////Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 1, true, Align.Center, VerticalAlign.Center);

                ////int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 2, null, null);
                ////spdData.Sheets[0].FrozenColumnCount = 10;
                ////spdData.RPT_AutoFit(false);

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

        #endregion


        #region " btnExcelExport_Click : Excel로 내보내기 "

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null, true);
            spdData.ExportExcel();
        }

        #endregion

        #endregion

        private void cdvMatCode_ValueButtonPress(object sender, EventArgs e)
        {
            string strquery = string.Empty;
            strquery = "SELECT MAT_ID, MAT_TYPE FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_TYPE = '" + cdvMatType.Text.Trim() + "' AND DELETE_FLAG = ' ' ORDER BY MAT_ID";

            cdvMatCode.sDynamicQuery = strquery;

        }

        private void cdvMatType_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            cdvMatCode.Text = "";
        }

    }
}
