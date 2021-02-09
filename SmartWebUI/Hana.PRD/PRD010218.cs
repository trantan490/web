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
    public partial class PRD010218 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        String s_Start = null;
        String s_End = null;
        /// <summary>
        /// 클  래  스: PRD010218<br/>
        /// 클래스요약: BG 작업 지시서<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2013-06-17<br/>
        /// 상세  설명: BG 작업 지시서 (이영찬 요청)<br/>
        /// 변경  내용: <br/>
        /// 2013-06-26-임종우 : 데이터 검증을 위한 실적, UPEH, 설비 CNT 추가 (임태성 요청)
        ///                     모델별 Sub Total & Grand Total 추가 (임태성 요청)
        /// 2013-06-27-임종우 : 제품 표시 방법 변경 (임태성 요청)
        ///                     삼성: 현재와 동일하게 유지
        ///                     Non-SEC : 업체코드 + Lead + PKG2 + Density + Generation + PKG Code + Type2  순으로 표시 
        /// 2013-07-16-임종우 : SOP 잔량 -> SOP 잔량 - 당일 실적으로 변경 (임태성 요청)
        ///                     kpcs 옵션 추가
        /// 2013-07-17-임종우 : 설비에 ARRANGE 되지 않은 재공 정보 스프레드 추가 (임태성 요청)
        /// </summary>
        public PRD010218()
        {
            InitializeComponent();
            cdvDate.Value = DateTime.Now.AddDays(-1);
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
            //if (cdvFactory.Text.TrimEnd() == "")
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
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
            spdData2.RPT_ColumnInit();

            try
            {
                spdData.RPT_AddBasicColumn("MODEL", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Equipment NO", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Performance (count)", 0, 2, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("actual", 0, 3, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Time utilization rate", 0, 4, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Double2, 70);
                spdData.RPT_AddBasicColumn("INCH", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("WHEEL 1", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("WHEEL 2", 0, 7, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("GDP", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("NCH", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("TAPE", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Operation PKG", 0, 11, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("Operation PKG WIP", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("SOP remaining", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                
                spdData.RPT_AddBasicColumn("a daily goal", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Residual amount", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Estimated time to complete", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 90);
                spdData.RPT_AddBasicColumn("Change planned", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("ARRAGE 1", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("ARRAGE 2", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("ARRAGE 3", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("Operation PKG Performance", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("UPEH", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Equipment number", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                
                spdData2.RPT_AddBasicColumn("PKG", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 150);
                spdData2.RPT_AddBasicColumn("WIP", 0, 1, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 90);
                spdData2.RPT_AddBasicColumn("SOP remaining", 0, 2, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 90);
                spdData2.RPT_AddBasicColumn("a daily goal", 0, 3, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 90);
                spdData2.RPT_AddBasicColumn("actual", 0, 4, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 90);
                spdData2.RPT_AddBasicColumn("Residual amount", 0, 5, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 90);                
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
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "PLN.MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = PLN.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "PLN.MAT_GRP_9", "PLN.MAT_GRP_9 AS MAJOR_CODE", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "PLN.MAT_GRP_2", "PLN.MAT_GRP_2 AS FAMILY", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "PLN.MAT_GRP_3", "PLN.MAT_GRP_3 AS PACKAGE", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "PLN.MAT_GRP_4", "PLN.MAT_GRP_4 AS TYPE1", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "PLN.MAT_GRP_5", "PLN.MAT_GRP_5 AS TYPE2", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "PLN.MAT_GRP_6", "PLN.MAT_GRP_6 AS \"LD COUNT\"", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "PLN.MAT_CMF_10", "PLN.MAT_CMF_10 AS PIN_TYPE", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "PLN.MAT_GRP_7", "PLN.MAT_GRP_7 AS DENSITY", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "PLN.MAT_GRP_8", "PLN.MAT_GRP_8 AS GENERATION", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "PLN.MAT_ID", "PLN.MAT_ID AS PRODUCT", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST DEVICE", "PLN.MAT_CMF_7", "PLN.MAT_CMF_7 AS CUST_DEVICE", false);
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
            
            // 지난주차의 마지막일 가져오기
            DataTable dt = null;
            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());
            s_Start = dt.Rows[0][0].ToString();
            s_End = dt.Rows[0][1].ToString();
            
            if (ckbKpcs.Checked == true)
            {
                strSqlString.Append("SELECT CASE WHEN A.RES_ID BETWEEN 'BB001' AND 'BB004' THEN 'TSK'" + "\n");
                strSqlString.Append("            WHEN A.RES_ID BETWEEN 'BB005' AND 'BB009' THEN '8760'" + "\n");
                strSqlString.Append("            ELSE '8761'" + "\n");
                strSqlString.Append("       END AS MODEL" + "\n");
                strSqlString.Append("     , A.RES_ID" + "\n");
                strSqlString.Append("     , A.RAS_QTY_2" + "\n");
                strSqlString.Append("     , ROUND(A.RAS_QTY_1 / 1000, 0) AS RAS_QTY_1" + "\n");
                strSqlString.Append("     , A.TIME_PER" + "\n");
                strSqlString.Append("     , A.WF_INCH" + "\n");
                strSqlString.Append("     , '' AS WHEEL_1" + "\n");
                strSqlString.Append("     , '' AS WHEEL_2" + "\n");
                strSqlString.Append("     , CASE WHEN A.GDP IS NOT NULL THEN 'GDP' END AS GDP" + "\n");
                strSqlString.Append("     , CASE WHEN A.NCH IS NOT NULL THEN 'NCH' END AS NCH" + "\n");
                strSqlString.Append("     , A.TAPE" + "\n");
                strSqlString.Append("     , A.PART" + "\n");
                strSqlString.Append("     , ROUND(B.WIP_QTY / 1000, 0) AS WIP_QTY" + "\n");
                strSqlString.Append("     , ROUND((NVL(C.SOP,0) - NVL(B.END_QTY,0)) / 1000, 0) AS SOP" + "\n");
                strSqlString.Append("     , ROUND(C.TARGET / 1000, 0) AS TARGET  " + "\n");
                strSqlString.Append("     , ROUND((NVL(C.TARGET,0) - NVL(B.END_QTY,0)) / 1000, 0) AS DEF" + "\n");
                strSqlString.Append("     , ROUND(NVL(B.WIP_QTY,0) / A.UPEH / A.CNT,1) AS EX_TIME" + "\n");
                strSqlString.Append("     , CASE WHEN NVL(B.BG_WAIT_QTY, 0) = 0 AND (SELECT COUNT(*) FROM RWIPINPDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND PART_NO = A.PART) = 0 THEN 'Change'" + "\n");
                strSqlString.Append("            ELSE '' END CHANGE" + "\n");
                strSqlString.Append("     , D.ARR_1" + "\n");
                strSqlString.Append("     , D.ARR_2" + "\n");
                strSqlString.Append("     , D.ARR_3" + "\n");
                strSqlString.Append("     , ROUND(B.END_QTY / 1000, 0) AS END_QTY" + "\n");
                strSqlString.Append("     , ROUND(A.UPEH / 1000, 0) AS UPEH" + "\n");
                strSqlString.Append("     , A.CNT" + "\n");
            }
            else
            {
                strSqlString.Append("SELECT CASE WHEN A.RES_ID BETWEEN 'BB001' AND 'BB004' THEN 'TSK'" + "\n");
                strSqlString.Append("            WHEN A.RES_ID BETWEEN 'BB005' AND 'BB009' THEN '8760'" + "\n");
                strSqlString.Append("            ELSE '8761'" + "\n");
                strSqlString.Append("       END AS MODEL" + "\n");
                strSqlString.Append("     , A.RES_ID" + "\n");
                strSqlString.Append("     , A.RAS_QTY_2" + "\n");
                strSqlString.Append("     , A.RAS_QTY_1" + "\n");
                strSqlString.Append("     , A.TIME_PER" + "\n");
                strSqlString.Append("     , A.WF_INCH" + "\n");
                strSqlString.Append("     , '' AS WHEEL_1" + "\n");
                strSqlString.Append("     , '' AS WHEEL_2" + "\n");
                strSqlString.Append("     , CASE WHEN A.GDP IS NOT NULL THEN 'GDP' END AS GDP" + "\n");
                strSqlString.Append("     , CASE WHEN A.NCH IS NOT NULL THEN 'NCH' END AS NCH" + "\n");
                strSqlString.Append("     , A.TAPE" + "\n");
                strSqlString.Append("     , A.PART" + "\n");
                strSqlString.Append("     , B.WIP_QTY" + "\n");
                strSqlString.Append("     , NVL(C.SOP,0) - NVL(B.END_QTY,0) AS SOP" + "\n");
                strSqlString.Append("     , C.TARGET" + "\n");
                strSqlString.Append("     , NVL(C.TARGET,0) - NVL(B.END_QTY,0) AS DEF" + "\n");
                strSqlString.Append("     , ROUND(NVL(B.WIP_QTY,0) / A.UPEH / A.CNT,1) AS EX_TIME" + "\n");
                strSqlString.Append("     , CASE WHEN NVL(B.BG_WAIT_QTY, 0) = 0 AND (SELECT COUNT(*) FROM RWIPINPDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND PART_NO = A.PART) = 0 THEN 'Change'" + "\n");
                strSqlString.Append("            ELSE '' END CHANGE" + "\n");
                strSqlString.Append("     , D.ARR_1" + "\n");
                strSqlString.Append("     , D.ARR_2" + "\n");
                strSqlString.Append("     , D.ARR_3" + "\n");
                strSqlString.Append("     , B.END_QTY" + "\n");
                strSqlString.Append("     , A.UPEH" + "\n");
                strSqlString.Append("     , A.CNT" + "\n");
            }

            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT A.RES_ID, A.RES_GRP_6, B.MAT_CMF_14 AS WF_INCH" + "\n");
            strSqlString.Append("             , C.RAS_QTY_2, C.RAS_QTY_1" + "\n");
            strSqlString.Append("             , D.TIME_PER" + "\n");
            strSqlString.Append("             , E.GDP, E.NCH" + "\n");
            strSqlString.Append("             , F.NORM AS TAPE" + "\n");
            strSqlString.Append("             , CASE WHEN MAT_GRP_1 = 'SE' THEN MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 " + "\n");
            strSqlString.Append("                    ELSE MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_GRP_7 || ' ' || MAT_GRP_8 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 " + "\n");
            strSqlString.Append("               END AS PART " + "\n");
            strSqlString.Append("             , MAX(CASE WHEN Z.UPEH IS NOT NULL THEN Z.UPEH" + "\n");
            strSqlString.Append("                        ELSE (SELECT UPEH FROM CRESMSTUPH@RPTTOMES WHERE FACTORY = A.FACTORY AND OPER = 'A0040' AND MODEL = A.RES_GRP_6 AND UPEH_GROUP = A.RES_GRP_7)" + "\n");
            strSqlString.Append("                   END) UPEH" + "\n");
            strSqlString.Append("             , SUM(SUM(1)) OVER(PARTITION BY CASE WHEN MAT_GRP_1 = 'SE' THEN MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5" + "\n");
            strSqlString.Append("                                                  ELSE MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_GRP_7 || ' ' || MAT_GRP_8 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5" + "\n");
            strSqlString.Append("                                             END) AS CNT" + "\n");
            strSqlString.Append("          FROM MRASRESDEF A" + "\n");
            strSqlString.Append("             , MWIPMATDEF B" + "\n");
            strSqlString.Append("             , CRASUPHDEF Z" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT END_RES_ID AS RES_ID, SUM(QTY_2) AS RAS_QTY_2, SUM(QTY_1) AS RAS_QTY_1 " + "\n");
            strSqlString.Append("                  FROM CWIPLOTEND " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND TRAN_TIME BETWEEN '" + s_Start + "220000' AND '" + s_End + "215959' " + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                   AND OLD_OPER = 'A0040' " + "\n");
            strSqlString.Append("                   AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                 GROUP BY END_RES_ID " + "\n");
            strSqlString.Append("               ) C " + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT RES_ID " + "\n");
            strSqlString.Append("                     , ROUND(DOWN_TIME, 2) AS DOWN_TIME " + "\n");
            strSqlString.Append("                     , ROUND(TTL_TIME, 2) AS TTL_TIME " + "\n");
            strSqlString.Append("                     , ROUND((TTL_TIME - DOWN_TIME) / TTL_TIME * 100, 2) AS TIME_PER " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT RES_ID, SUM(TIME_SUM) / 60 AS DOWN_TIME, (SYSDATE - (TRUNC(SYSDATE-1) + 22/24)) * 24 * 60 AS TTL_TIME " + "\n");
            strSqlString.Append("                          FROM CSUMRESMNT@RPTTOMES  " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND WORK_DATE = '" + s_End + "' " + "\n");
            strSqlString.Append("                           AND RES_ID LIKE 'BB%' " + "\n");
            strSqlString.Append("                         GROUP BY RES_ID " + "\n");
            strSqlString.Append("                       ) " + "\n");
            strSqlString.Append("               ) D " + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT RES_ID " + "\n");
            strSqlString.Append("                     , MAX(DECODE(TOOL_TYPE, 'GDP WHEEL', TOOL_ID)) AS GDP " + "\n");
            strSqlString.Append("                     , MAX(DECODE(TOOL_TYPE, 'NON CONTACT HAND', TOOL_ID)) AS NCH " + "\n");
            strSqlString.Append("                  FROM CWIPTOLSTS@RPTTOMES " + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND RES_ID LIKE 'BB%' " + "\n");            
            strSqlString.Append("                 GROUP BY RES_ID " + "\n");
            strSqlString.Append("               ) E " + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT A.RES_ID, MAX(C.NORM) AS NORM " + "\n");
            strSqlString.Append("                  FROM CRASRESMAT A " + "\n");
            strSqlString.Append("                     , RWIPLOTSTS B " + "\n");
            strSqlString.Append("                     , CWIPMATDEF@RPTTOMES C " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                   AND B.FACTORY = C.FACTORY " + "\n");
            strSqlString.Append("                   AND A.MAT_LOT_ID = B.LOT_ID " + "\n");
            strSqlString.Append("                   AND B.MAT_ID = C.MAT_ID " + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND A.RES_ID LIKE 'BB%'" + "\n");
            strSqlString.Append("                   AND B.LOT_TYPE = 'Z'" + "\n");
            strSqlString.Append("                   AND C.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                 GROUP BY A.RES_ID " + "\n");
            strSqlString.Append("               ) F " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.FACTORY = Z.FACTORY(+) " + "\n");
            strSqlString.Append("           AND A.RES_STS_2 = B.MAT_ID " + "\n");
            strSqlString.Append("           AND A.RES_STS_2 = Z.MAT_ID(+) " + "\n");
            strSqlString.Append("           AND A.RES_ID = C.RES_ID(+) " + "\n");
            strSqlString.Append("           AND A.RES_ID = D.RES_ID(+) " + "\n");
            strSqlString.Append("           AND A.RES_ID = E.RES_ID(+) " + "\n");
            strSqlString.Append("           AND A.RES_ID = F.RES_ID(+) " + "\n");
            strSqlString.Append("           AND A.RES_GRP_6 = Z.RES_MODEL(+) " + "\n");
            strSqlString.Append("           AND A.RES_GRP_7 = Z.UPEH_GRP(+) " + "\n");
            strSqlString.Append("           AND Z.OPER(+) = 'A0040' " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND A.RES_ID LIKE 'BB%' " + "\n");
            strSqlString.Append("           AND A.RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("           AND A.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("         GROUP BY A.RES_ID, A.RES_GRP_6, C.RAS_QTY_2, C.RAS_QTY_1, D.TIME_PER, E.GDP, E.NCH, F.NORM " + "\n");
            strSqlString.Append("                , CASE WHEN MAT_GRP_1 = 'SE' THEN MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5  " + "\n");
            strSqlString.Append("                       ELSE MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_GRP_7 || ' ' || MAT_GRP_8 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5   " + "\n");
            strSqlString.Append("                  END, B.MAT_CMF_14  " + "\n");
            strSqlString.Append("       ) A " + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT CASE WHEN MAT_GRP_1 = 'SE' THEN MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 " + "\n");
            strSqlString.Append("                    ELSE MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_GRP_7 || ' ' || MAT_GRP_8 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 " + "\n");
            strSqlString.Append("               END AS PART " + "\n");
            strSqlString.Append("             , SUM(B.WIP_QTY) AS WIP_QTY" + "\n");
            strSqlString.Append("             , SUM(B.BG_WAIT_QTY) AS BG_WAIT_QTY " + "\n");
            strSqlString.Append("             , SUM(C.END_QTY) AS END_QTY " + "\n");
            strSqlString.Append("          FROM MWIPMATDEF A " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT MAT_ID, SUM(QTY_1) AS WIP_QTY" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER = 'A0040' AND LOT_STATUS = 'WAIT' THEN QTY_1 " + "\n");
            strSqlString.Append("                                ELSE 0" + "\n");
            strSqlString.Append("                           END) BG_WAIT_QTY " + "\n");
            strSqlString.Append("                  FROM RWIPLOTSTS " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                   AND LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND OPER BETWEEN 'A0020' AND 'A0040'" + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("               ) B " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT MAT_ID, SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1) AS END_QTY " + "\n");
            strSqlString.Append("                  FROM RSUMWIPMOV " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND WORK_DATE = '" + s_End + "' " + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                   AND OPER = 'A0040' " + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("               ) C " + "\n");
            strSqlString.Append("         WHERE 1 = 1 " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND A.MAT_ID = C.MAT_ID(+) " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("         GROUP BY CASE WHEN MAT_GRP_1 = 'SE' THEN MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5  " + "\n");
            strSqlString.Append("                       ELSE MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_GRP_7 || ' ' || MAT_GRP_8 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5   " + "\n");
            strSqlString.Append("                  END " + "\n");
            strSqlString.Append("        HAVING NVL(SUM(B.WIP_QTY),0) + NVL(SUM(C.END_QTY),0) > 0 " + "\n");            
            strSqlString.Append("       ) B" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT PART " + "\n");
            strSqlString.Append("             , SUM(DECODE(GUBUN, ' ', BG, 0)) AS SOP " + "\n");
            strSqlString.Append("             , SUM(DECODE(GUBUN, 'TARGET', BG, 0)) AS TARGET " + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT RESV_FIELD_2 AS PART " + "\n");
            strSqlString.Append("                     , RESV_FIELD_1 AS GUBUN " + "\n");
            strSqlString.Append("                     , CASE WHEN MAX(BG) = 0 THEN MIN(BG) ELSE MAX(BG) END AS BG  " + "\n");            
            strSqlString.Append("                  FROM RSUMOPRREM" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND WORK_DATE = '" + s_End + "'" + "\n");
            strSqlString.Append("                 GROUP BY RESV_FIELD_2, RESV_FIELD_1" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY PART" + "\n");
            strSqlString.Append("       ) C" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT KEY_1 AS RES_ID" + "\n");
            strSqlString.Append("             , DATA_1 AS ARR_1" + "\n");
            strSqlString.Append("             , DATA_2 AS ARR_2" + "\n");
            strSqlString.Append("             , DATA_3 AS ARR_3" + "\n");
            strSqlString.Append("          FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND TABLE_NAME = 'H_BG_ARRAGE' " + "\n");
            strSqlString.Append("       ) D" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND A.PART = B.PART(+)" + "\n");
            strSqlString.Append("   AND A.PART = C.PART(+) " + "\n");
            strSqlString.Append("   AND A.RES_ID = D.RES_ID(+) " + "\n");
            strSqlString.Append(" ORDER BY RES_ID" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        // 지난 주차의 마지막일 가져오기
        private string MakeSqlString1()
        {
            StringBuilder sqlString = new StringBuilder();

            sqlString.Append("SELECT GET_WORK_DATE(TO_CHAR(SYSDATE-1, 'YYYYMMDDHH24MISS'), 'D') AS S_DAY " + "\n");
            sqlString.Append("     , GET_WORK_DATE(TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS'), 'D') AS E_DAY" + "\n");
            sqlString.Append("  FROM DUAL" + "\n");

            return sqlString.ToString();
        }

        // 설비 Arrange 되지 않은 재공 정보
        private string MakeSqlString2()
        {
            StringBuilder sqlString = new StringBuilder();

            if (ckbKpcs.Checked == true)
            {
                sqlString.Append("SELECT A.PART " + "\n");
                sqlString.Append("     , ROUND(A.WIP_QTY / 1000, 0) AS WIP_QTY" + "\n");
                sqlString.Append("     , ROUND(B.SOP / 1000, 0) AS SOP" + "\n");
                sqlString.Append("     , ROUND(B.TARGET / 1000, 0) AS TARGET" + "\n");
                sqlString.Append("     , ROUND(A.END_QTY / 1000, 0) AS END_QTY" + "\n");
                sqlString.Append("     , ROUND((NVL(B.TARGET, 0) - NVL(A.END_QTY, 0)) / 1000, 0) AS REMAIN" + "\n");
            }
            else
            {
                sqlString.Append("SELECT A.PART " + "\n");
                sqlString.Append("     , A.WIP_QTY" + "\n");
                sqlString.Append("     , B.SOP" + "\n");
                sqlString.Append("     , B.TARGET" + "\n");
                sqlString.Append("     , A.END_QTY" + "\n");
                sqlString.Append("     , NVL(B.TARGET, 0) - NVL(A.END_QTY, 0) AS REMAIN" + "\n");
            }

            sqlString.Append("  FROM (" + "\n");
            sqlString.Append("        SELECT CASE WHEN MAT_GRP_1 = 'SE' THEN MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 " + "\n");
            sqlString.Append("                    ELSE MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_GRP_7 || ' ' || MAT_GRP_8 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 " + "\n");
            sqlString.Append("               END AS PART " + "\n");
            sqlString.Append("             , SUM(WIP.WIP_QTY) AS WIP_QTY " + "\n");
            sqlString.Append("             , SUM(MOV.END_QTY) AS END_QTY " + "\n");
            sqlString.Append("          FROM MWIPMATDEF MAT " + "\n");
            sqlString.Append("             , (" + "\n");
            sqlString.Append("                SELECT MAT_ID, SUM(QTY_1) AS WIP_QTY" + "\n");
            sqlString.Append("                  FROM RWIPLOTSTS" + "\n");
            sqlString.Append("                 WHERE 1=1" + "\n");
            sqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
            sqlString.Append("                   AND LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                   AND OPER BETWEEN 'A0020' AND 'A0040'" + "\n");
            sqlString.Append("                   AND LOT_STATUS = 'WAIT'" + "\n");
            sqlString.Append("                 GROUP BY MAT_ID" + "\n");
            sqlString.Append("               ) WIP" + "\n");
            sqlString.Append("             , (" + "\n");
            sqlString.Append("                SELECT MAT_ID, SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1) AS END_QTY" + "\n");
            sqlString.Append("                  FROM RSUMWIPMOV" + "\n");
            sqlString.Append("                 WHERE 1=1" + "\n");
            sqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                   AND WORK_DATE = '" + s_End + "'" + "\n");
            sqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
            sqlString.Append("                   AND OPER = 'A0040'" + "\n");            
            sqlString.Append("                 GROUP BY MAT_ID" + "\n");
            sqlString.Append("               ) MOV" + "\n");
            sqlString.Append("         WHERE 1=1" + "\n");
            sqlString.Append("           AND MAT.MAT_ID = WIP.MAT_ID" + "\n");
            sqlString.Append("           AND MAT.MAT_ID = MOV.MAT_ID(+)" + "\n");
            sqlString.Append("           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("           AND MAT.MAT_TYPE = 'FG'" + "\n");
            sqlString.Append("           AND MAT.DELETE_FLAG = ' '" + "\n");
            sqlString.Append("         GROUP BY CASE WHEN MAT_GRP_1 = 'SE' THEN MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 " + "\n");
            sqlString.Append("                       ELSE MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_GRP_7 || ' ' || MAT_GRP_8 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 " + "\n");
            sqlString.Append("                  END" + "\n");
            sqlString.Append("       ) A" + "\n");
            sqlString.Append("     , (" + "\n");
            sqlString.Append("        SELECT PART" + "\n");
            sqlString.Append("             , SUM(DECODE(GUBUN, ' ', BG, 0)) AS SOP " + "\n");
            sqlString.Append("             , SUM(DECODE(GUBUN, 'TARGET', BG, 0)) AS TARGET  " + "\n");
            sqlString.Append("          FROM ( " + "\n");
            sqlString.Append("                SELECT RESV_FIELD_2 AS PART " + "\n");
            sqlString.Append("                     , RESV_FIELD_1 AS GUBUN " + "\n");
            sqlString.Append("                     , CASE WHEN MAX(BG) = 0 THEN MIN(BG) ELSE MAX(BG) END AS BG  " + "\n");
            sqlString.Append("                  FROM RSUMOPRREM " + "\n");
            sqlString.Append("                 WHERE 1=1 " + "\n");
            sqlString.Append("                   AND WORK_DATE = '" + s_End + "' " + "\n");
            sqlString.Append("                 GROUP BY RESV_FIELD_2, RESV_FIELD_1" + "\n");
            sqlString.Append("               ) " + "\n");
            sqlString.Append("         GROUP BY PART " + "\n");
            sqlString.Append("       ) B" + "\n");
            sqlString.Append(" WHERE 1=1" + "\n");
            sqlString.Append("   AND A.PART = B.PART(+)" + "\n");
            sqlString.Append(" ORDER BY PART" + "\n");

            return sqlString.ToString();
        }

        #endregion


        #region EVENT 처리
        /// <summary>
        /// 6. View 버튼 Action
        /// </summary>        ///         
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            DataTable dt2 = null;

            if (CheckField() == false) return;

            GridColumnInit();
            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());
                dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2() );

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                if (dt2.Rows.Count == 0)
                {
                    dt2.Dispose();                    
                }

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 1, null, null, btnSort);
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함
                
                   //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 1, 0, 1, true, Align.Center, VerticalAlign.Center);

                
                int[] rowType2 = spdData2.RPT_DataBindingWithSubTotal(dt2, 0, 0, 1, null, null, btnSort);                                              
                spdData2.RPT_FillDataSelectiveCells("Total", 0, 1, 0, 1, true, Align.Center, VerticalAlign.Center);

                //spdData.DataSource = dt;

                //spdData.ActiveSheet.Columns[0].BackColor = Color.LightYellow;
                //spdData.ActiveSheet.Columns[1].BackColor = Color.LightYellow;

                spdData.RPT_AutoFit(false);
                spdData2.RPT_AutoFit(false);

                spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 4, 0, false);

                dt.Dispose();
                dt2.Dispose();
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
            //this.SetFactory(cdvFactory.txtValue);
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }
                
        #endregion
    }
}
