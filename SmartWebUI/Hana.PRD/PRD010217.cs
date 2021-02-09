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
    public partial class PRD010217 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010217<br/>
        /// 클래스요약: 투입 작업 지시서<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2013-06-28<br/>
        /// 상세  설명: 투입 작업 지시서 (이영찬 요청)<br/>
        /// 변경  내용: <br/>
        /// 2013-07-09-임종우 : 당주 SOP잔량 + 차주 계획으로 투입목록 생성 (이영찬 요청)
        /// 2013-07-16-임종우 : SOP 잔량 -> SOP 잔량 - 당일 실적으로 변경 (임태성 요청)
        ///                     kpcs 옵션 추가
        /// 2013-08-26-임종우 : 계획 테이블 변경 CWIPPLNWEK_N -> RWIPPLNWEK
        /// </summary>
        public PRD010217()
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

            try
            {
                spdData.RPT_AddBasicColumn("Input rank", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PART NO", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("SOP remaining", 0, 2, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Input goal", 0, 3, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Input Performance", 0, 4, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Residual amount", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Remaining quantity (Wafer)", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("STOCK WIP", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA CAPA", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("L/N ~ D/A WIP", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number,70);
                spdData.RPT_AddBasicColumn("percentage of work-in-operation securement", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Standard Goal", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("a daily goal", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("SOP Progress Rate", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Arrage ranking", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);                
                spdData.RPT_AddBasicColumn("Input unit (wafer) ", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("PCB/LF", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                
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
            string sYesterday = dt.Rows[0][0].ToString();
            string sToday = dt.Rows[0][1].ToString();
            string sThisWeek = dt.Rows[0][2].ToString();
            string sNextWeek = dt.Rows[1][2].ToString();
                        


            strSqlString.Append("SELECT TO_CHAR(RNK_IN) AS RNK_IN" + "\n");
            strSqlString.Append("     , PART" + "\n");
            strSqlString.Append("     , NVL(SOP, 0) - NVL(ISSUE, 0) AS SOP" + "\n");
            strSqlString.Append("     , IN_TARGET" + "\n");
            strSqlString.Append("     , ISSUE" + "\n");
            strSqlString.Append("     , DEF_1" + "\n");
            strSqlString.Append("     , DEF_2" + "\n");
            strSqlString.Append("     , STOCK" + "\n");
            strSqlString.Append("     , CAPA_DA" + "\n");
            strSqlString.Append("     , LN_DA" + "\n");
            strSqlString.Append("     , ROUND(WIP_PER, 0) AS WIP_PER" + "\n");
            strSqlString.Append("     , PLAN_QTY" + "\n");
            strSqlString.Append("     , DAY_TARGET" + "\n");
            strSqlString.Append("     , ROUND(SOP_PER, 0) AS SOP_PER" + "\n");
            strSqlString.Append("     , ROW_NUMBER() OVER (ORDER BY CASE WHEN SOP_PER <= 80 THEN 1 ELSE 2 END, WIP_PER) AS RNK" + "\n");
            strSqlString.Append("     , CASE WHEN DEF_2 > 40 THEN 40 " + "\n");
            strSqlString.Append("            WHEN DEF_2 < 0 THEN 0" + "\n");
            strSqlString.Append("            ELSE DEF_2 " + "\n");
            strSqlString.Append("       END AS IN_UNIT" + "\n");
            strSqlString.Append("     , MAT_TTL" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT A.PART" + "\n");

            if (ckbKpcs.Checked == true)
            {
                strSqlString.Append("             , ROUND(NVL(SOP,0) / 1000,0) AS SOP" + "\n");
                strSqlString.Append("             , ROUND(NVL(DAY_TARGET,0) / 1000,0) AS IN_TARGET" + "\n");
                strSqlString.Append("             , ROUND(NVL(ISSUE,0) / 1000,0) AS ISSUE" + "\n");
                strSqlString.Append("             , ROUND((NVL(DAY_TARGET,0) - NVL(ISSUE,0)) / 1000,0) AS DEF_1" + "\n");
                strSqlString.Append("             , ROUND((NVL(DAY_TARGET,0) - NVL(ISSUE,0)) / NET_DIE * 0.9, 0) AS DEF_2" + "\n");
                strSqlString.Append("             , ROUND(NVL(STOCK,0) / 1000,0) AS STOCK" + "\n");
                strSqlString.Append("             , ROUND(NVL(CAPA_DA,0) / 1000,0) AS CAPA_DA" + "\n");
                strSqlString.Append("             , ROUND(NVL(LN_DA,0) / 1000,0) AS LN_DA" + "\n");
                strSqlString.Append("             , ROUND(LN_DA / CAPA_DA * 100, 0) AS WIP_PER " + "\n");
                strSqlString.Append("             , ROUND(NVL(PLAN_QTY,0) / 1000,0) AS PLAN_QTY" + "\n");
                strSqlString.Append("             , ROUND(NVL(DAY_TARGET,0) / 1000,0) AS DAY_TARGET" + "\n");
                strSqlString.Append("             , CASE WHEN DAY_TARGET IS NULL THEN NULL" + "\n");
                strSqlString.Append("                    WHEN DAY_TARGET = 0 THEN 0" + "\n");
                strSqlString.Append("                    ELSE NVL(PLAN_QTY,0) / DAY_TARGET" + "\n");
                strSqlString.Append("               END AS SOP_PER     " + "\n");
                strSqlString.Append("             , ROW_NUMBER() OVER(ORDER BY C.EX_TIME) AS RNK_IN " + "\n");
                strSqlString.Append("             , ROUND(NVL(D.MAT_TTL,0) / 1000,0) AS MAT_TTL" + "\n");                
            }
            else
            {
                strSqlString.Append("             , SOP, DAY_TARGET AS IN_TARGET, ISSUE" + "\n");
                strSqlString.Append("             , ROUND((NVL(DAY_TARGET,0) - NVL(ISSUE,0)),0) AS DEF_1" + "\n");
                strSqlString.Append("             , ROUND((NVL(DAY_TARGET,0) - NVL(ISSUE,0)) / NET_DIE * 0.9, 0) AS DEF_2" + "\n");
                strSqlString.Append("             , STOCK, CAPA_DA, LN_DA" + "\n");
                strSqlString.Append("             , ROUND(LN_DA / CAPA_DA * 100, 0) AS WIP_PER   " + "\n");
                strSqlString.Append("             , PLAN_QTY" + "\n");
                strSqlString.Append("             , DAY_TARGET" + "\n");
                strSqlString.Append("             , CASE WHEN DAY_TARGET IS NULL THEN NULL" + "\n");
                strSqlString.Append("                    WHEN DAY_TARGET = 0 THEN 0" + "\n");
                strSqlString.Append("                    ELSE NVL(PLAN_QTY,0) / DAY_TARGET" + "\n");
                strSqlString.Append("               END AS SOP_PER     " + "\n");
                strSqlString.Append("             , ROW_NUMBER() OVER(ORDER BY C.EX_TIME) AS RNK_IN " + "\n");
                strSqlString.Append("             , D.MAT_TTL" + "\n");
            }

            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT INP.PART, INP.INPUT, SOP.SOP, SOP.DAY_TARGET, TO_NUMBER(DECODE(NET_DIE,' ',1,NET_DIE)) AS NET_DIE" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT PART_NO AS PART" + "\n");
            strSqlString.Append("                             , SUM(QTY_1) AS INPUT" + "\n");
            strSqlString.Append("                             , MAX(MAT_CMF_13) AS NET_DIE" + "\n");
            strSqlString.Append("                          FROM RWIPINPDAT A" + "\n");
            strSqlString.Append("                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND MAT_GRP_3 <> 'COB'" + "\n");
            strSqlString.Append("                         GROUP BY PART_NO" + "\n");
            strSqlString.Append("                       ) INP" + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT PART " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN CUSTOMER = 'SE' AND GUBUN = ' ' THEN INPUT_SE " + "\n");
            strSqlString.Append("                                        WHEN CUSTOMER <> 'SE' AND GUBUN = ' ' THEN INPUT_ETC " + "\n");
            strSqlString.Append("                                        ELSE 0 END " + "\n");
            strSqlString.Append("                                  ) AS SOP " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN CUSTOMER = 'SE' AND GUBUN = 'TARGET' THEN INPUT_SE " + "\n");
            strSqlString.Append("                                        WHEN CUSTOMER <> 'SE' AND GUBUN = 'TARGET' THEN INPUT_ETC " + "\n");
            strSqlString.Append("                                        ELSE 0 END " + "\n");
            strSqlString.Append("                                  ) AS DAY_TARGET " + "\n");            
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT CUSTOMER, RESV_FIELD_2 AS PART " + "\n");
            strSqlString.Append("                                     , RESV_FIELD_1 AS GUBUN " + "\n");
            strSqlString.Append("                                     , CASE WHEN MAX(INPUT) = 0 THEN MIN(INPUT) ELSE MAX(INPUT) END AS INPUT_SE  " + "\n");
            strSqlString.Append("                                     , SUM(INPUT) AS INPUT_ETC " + "\n");
            strSqlString.Append("                                  FROM RSUMOPRREM" + "\n");
            strSqlString.Append("                                 WHERE 1=1" + "\n");
            strSqlString.Append("                                   AND WORK_DATE = '" + sToday + "'" + "\n");
            strSqlString.Append("                                   AND INPUT <> 0" + "\n");
            strSqlString.Append("                                 GROUP BY CUSTOMER, RESV_FIELD_2, RESV_FIELD_1" + "\n");
            strSqlString.Append("                               )" + "\n");
            strSqlString.Append("                         GROUP BY PART" + "\n");
            strSqlString.Append("                       ) SOP" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND INP.PART = SOP.PART(+)" + "\n");
            strSqlString.Append("               ) A" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT CASE WHEN MAT_GRP_1 = 'SE' THEN MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 " + "\n");
            strSqlString.Append("                            ELSE MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_GRP_7 || ' ' || MAT_GRP_8 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 " + "\n");
            strSqlString.Append("                       END AS PART" + "\n");
            strSqlString.Append("                     , SUM(WIP.STOCK) AS STOCK" + "\n");
            strSqlString.Append("                     , SUM(CAP.CAPA_DA) AS CAPA_DA" + "\n");
            strSqlString.Append("                     , SUM(WIP.LN_DA) AS LN_DA" + "\n");
            strSqlString.Append("                     , SUM(ISS.ISSUE) AS ISSUE" + "\n");
            strSqlString.Append("                     , ROUND(SUM(PLN.PLAN_QTY) / 7, 0) AS PLAN_QTY" + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT A.MAT_ID" + "\n");
            strSqlString.Append("                             , SUM(DECODE(OPER, 'A0000', QTY_1, 0)) AS STOCK" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN MAT_GRP_5 = '-' AND OPER BETWEEN 'A0020' AND 'A0400' THEN QTY_1" + "\n");
            strSqlString.Append("                                        WHEN MAT_GRP_5 = '1st' AND OPER BETWEEN 'A0020' AND 'A0401' THEN QTY_1" + "\n");
            strSqlString.Append("                                        WHEN MAT_GRP_5 IN ('2nd', '3rd', '4th', '5th') AND OPER <> 'A0000' THEN QTY_1" + "\n");
            strSqlString.Append("                                        ELSE 0" + "\n");
            strSqlString.Append("                                   END) AS LN_DA     " + "\n");
            strSqlString.Append("                          FROM MWIPMATDEF A" + "\n");
            strSqlString.Append("                             , RWIPLOTSTS B" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID   " + "\n");
            strSqlString.Append("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND A.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                           AND B.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND B.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                         GROUP BY A.MAT_ID" + "\n");
            strSqlString.Append("                       ) WIP" + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID, SUM(QTY_1) AS ISSUE" + "\n");
            strSqlString.Append("                          FROM CWIPLOTEND" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND TRAN_TIME BETWEEN '" + sYesterday + "220000' AND '" + sToday + "215959' " + "\n");
            strSqlString.Append("                           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                           AND OLD_OPER = 'A0000'" + "\n");
            strSqlString.Append("                           AND HIST_DEL_FLAG = ' '           " + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("                       ) ISS" + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT RES.MAT_ID     " + "\n");
            strSqlString.Append("                             , SUM(NVL(UPH.UPEH,0) * 24 * 0.75 * RES.EQP_CNT) AS CAPA_DA   " + "\n");
            strSqlString.Append("                          FROM (  " + "\n");
            strSqlString.Append("                                SELECT FACTORY, RES_STS_2 AS MAT_ID, RES_STS_8 AS OPER, RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, COUNT(RES_ID) AS EQP_CNT " + "\n");
            strSqlString.Append("                                  FROM MRASRESDEF " + "\n");
            strSqlString.Append("                                 WHERE 1 = 1  " + "\n");
            strSqlString.Append("                                   AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                   AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("                                   AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                                   AND RES_STS_8 LIKE 'A040%'" + "\n");
            strSqlString.Append("                                 GROUP BY FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7 " + "\n");
            strSqlString.Append("                               ) RES " + "\n");
            strSqlString.Append("                             , CRASUPHDEF UPH      " + "\n");
            strSqlString.Append("                        WHERE 1 = 1 " + "\n");
            strSqlString.Append("                          AND RES.FACTORY = UPH.FACTORY(+)   " + "\n");
            strSqlString.Append("                          AND RES.MAT_ID = UPH.MAT_ID(+)  " + "\n");
            strSqlString.Append("                          AND RES.OPER = UPH.OPER(+) " + "\n");
            strSqlString.Append("                          AND RES.RES_MODEL = UPH.RES_MODEL(+) " + "\n");
            strSqlString.Append("                          AND RES.UPEH_GRP = UPH.UPEH_GRP(+)   " + "\n");
            strSqlString.Append("                        GROUP BY RES.MAT_ID" + "\n");
            strSqlString.Append("                        HAVING SUM(NVL(UPH.UPEH,0) * 24 * 0.75 * RES.EQP_CNT) > 0" + "\n");
            strSqlString.Append("                       ) CAP" + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID, W1_QTY, W2_QTY" + "\n");
            strSqlString.Append("                             , CASE WHEN TO_CHAR(TO_DATE('" + sToday + "', 'YYYYMMDD'), 'D') IN ('3','4','5','6') THEN W2_QTY" + "\n");
            strSqlString.Append("                                    ELSE W1_QTY" + "\n");
            strSqlString.Append("                               END PLAN_QTY" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + sThisWeek + "', WW_QTY, 0)) AS W1_QTY " + "\n");
            strSqlString.Append("                                     , SUM(DECODE(PLAN_WEEK, '" + sNextWeek + "', WW_QTY, 0)) AS W2_QTY " + "\n");
            strSqlString.Append("                                  FROM RWIPPLNWEK" + "\n");
            strSqlString.Append("                                 WHERE 1=1 " + "\n");
            strSqlString.Append("                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                   AND GUBUN = '3' " + "\n");
            strSqlString.Append("                                   AND PLAN_WEEK IN ('" + sThisWeek + "','" + sNextWeek + "')" + "\n");            
            strSqlString.Append("                                 GROUP BY FACTORY, MAT_ID  " + "\n");
            strSqlString.Append("                               ) A     " + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND W1_QTY + W2_QTY > 0" + "\n");
            strSqlString.Append("                       ) PLN" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");
            strSqlString.Append("                   AND MAT.MAT_ID = ISS.MAT_ID(+)" + "\n");
            strSqlString.Append("                   AND MAT.MAT_ID = CAP.MAT_ID(+)" + "\n");
            strSqlString.Append("                   AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
            strSqlString.Append("                   AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND MAT.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                   AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                 GROUP BY CASE WHEN MAT_GRP_1 = 'SE' THEN MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 " + "\n");
            strSqlString.Append("                               ELSE MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_GRP_7 || ' ' || MAT_GRP_8 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 " + "\n");
            strSqlString.Append("                          END" + "\n");
            strSqlString.Append("               ) B" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT A.PART" + "\n");
            strSqlString.Append("                     , B.WIP_QTY" + "\n");
            strSqlString.Append("                     , ROUND(NVL(B.WIP_QTY,0) / A.UPEH / A.CNT,1) AS EX_TIME     " + "\n");
            strSqlString.Append("                     , A.UPEH" + "\n");
            strSqlString.Append("                     , A.CNT" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT CASE WHEN MAT_GRP_1 = 'SE' THEN MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 " + "\n");
            strSqlString.Append("                                    ELSE MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_GRP_7 || ' ' || MAT_GRP_8 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 " + "\n");
            strSqlString.Append("                               END AS PART " + "\n");
            strSqlString.Append("                             , MAX(CASE WHEN Z.UPEH IS NOT NULL THEN Z.UPEH" + "\n");
            strSqlString.Append("                                        ELSE (SELECT UPEH FROM CRESMSTUPH@RPTTOMES WHERE FACTORY = A.FACTORY AND OPER = 'A0040' AND MODEL = A.RES_GRP_6 AND UPEH_GROUP = A.RES_GRP_7)" + "\n");
            strSqlString.Append("                                   END) UPEH" + "\n");
            strSqlString.Append("                             , SUM(SUM(1)) OVER(PARTITION BY CASE WHEN MAT_GRP_1 = 'SE' THEN MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5" + "\n");
            strSqlString.Append("                                                                  ELSE MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_GRP_7 || ' ' || MAT_GRP_8 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5" + "\n");
            strSqlString.Append("                                                             END) AS CNT" + "\n");
            strSqlString.Append("                          FROM MRASRESDEF A" + "\n");
            strSqlString.Append("                             , MWIPMATDEF B" + "\n");
            strSqlString.Append("                             , CRASUPHDEF Z              " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                           AND A.FACTORY = Z.FACTORY(+) " + "\n");
            strSqlString.Append("                           AND A.RES_STS_2 = B.MAT_ID " + "\n");
            strSqlString.Append("                           AND A.RES_STS_2 = Z.MAT_ID(+)           " + "\n");
            strSqlString.Append("                           AND A.RES_GRP_6 = Z.RES_MODEL(+) " + "\n");
            strSqlString.Append("                           AND A.RES_GRP_7 = Z.UPEH_GRP(+) " + "\n");
            strSqlString.Append("                           AND Z.OPER(+) = 'A0040' " + "\n");
            strSqlString.Append("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND A.RES_ID LIKE 'BB%' " + "\n");
            strSqlString.Append("                           AND A.RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("                           AND A.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                         GROUP BY CASE WHEN MAT_GRP_1 = 'SE' THEN MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5  " + "\n");
            strSqlString.Append("                                       ELSE MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_GRP_7 || ' ' || MAT_GRP_8 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5   " + "\n");
            strSqlString.Append("                                  END" + "\n");
            strSqlString.Append("                       ) A " + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT CASE WHEN MAT_GRP_1 = 'SE' THEN MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 " + "\n");
            strSqlString.Append("                                    ELSE MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_GRP_7 || ' ' || MAT_GRP_8 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 " + "\n");
            strSqlString.Append("                               END AS PART " + "\n");
            strSqlString.Append("                             , SUM(B.WIP_QTY) AS WIP_QTY" + "\n");
            strSqlString.Append("                          FROM MWIPMATDEF A " + "\n");
            strSqlString.Append("                             , ( " + "\n");
            strSqlString.Append("                                SELECT MAT_ID, SUM(QTY_1) AS WIP_QTY" + "\n");
            strSqlString.Append("                                  FROM RWIPLOTSTS " + "\n");
            strSqlString.Append("                                 WHERE 1=1 " + "\n");
            strSqlString.Append("                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                   AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                                   AND LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                                   AND OPER BETWEEN 'A0020' AND 'A0040'" + "\n");
            strSqlString.Append("                                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                               ) B " + "\n");
            strSqlString.Append("                         WHERE 1 = 1 " + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                         GROUP BY CASE WHEN MAT_GRP_1 = 'SE' THEN MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5  " + "\n");
            strSqlString.Append("                                       ELSE MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_GRP_7 || ' ' || MAT_GRP_8 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5   " + "\n");
            strSqlString.Append("                                  END " + "\n");
            strSqlString.Append("                        HAVING NVL(SUM(B.WIP_QTY),0) > 0 " + "\n");
            strSqlString.Append("                       ) B" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND A.PART = B.PART(+) " + "\n");
            strSqlString.Append("               ) C" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT BOM.PART" + "\n");
            strSqlString.Append("                     , SUM(NVL(CWIP.INV_QTY,0)) + SUM(NVL(CWIP.INV_L_QTY,0)) + SUM(NVL(WIP.QTY_TTL,0)) AS MAT_TTL" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT DISTINCT PART, MATCODE" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT PARTNUMBER, OPER, A.MAT_TYPE, MATCODE, DESCRIPT, PAR_BASE_QTY, UNIT_QTY, EDIT_DT     " + "\n");
            strSqlString.Append("                                     , DENSE_RANK() OVER(PARTITION BY PARTNUMBER, OPER, A.MAT_TYPE ORDER BY EDIT_DT DESC) AS RNK" + "\n");
            strSqlString.Append("                                     , CASE WHEN MAT_GRP_1 = 'SE' THEN MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 " + "\n");
            strSqlString.Append("                                            ELSE MAT_GRP_1 || ' ' || MAT_GRP_6 || ' ' || MAT_GRP_10 || ' ' || MAT_GRP_7 || ' ' || MAT_GRP_8 || ' ' || MAT_CMF_11 || ' ' || MAT_GRP_5 " + "\n");
            strSqlString.Append("                                       END AS PART" + "\n");
            strSqlString.Append("                                  FROM (" + "\n");
            strSqlString.Append("                                        SELECT PARTNUMBER, STEPID AS OPER, RESV_FIELD_2 AS MAT_TYPE, MATCODE, DESCRIPT, PAR_BASE_QTY, UNIT_QTY" + "\n");
            strSqlString.Append("                                             , DECODE(EDIT_DT, ' ', CREATE_DT, EDIT_DT) AS EDIT_DT" + "\n");
            strSqlString.Append("                                          FROM CWIPBOMDEF" + "\n");
            strSqlString.Append("                                         WHERE 1=1    " + "\n");
            strSqlString.Append("                                           AND RESV_FLAG_1 = 'Y'" + "\n");
            strSqlString.Append("                                           AND RESV_FIELD_2 IN ('PB','LF') " + "\n");
            strSqlString.Append("                                       ) A" + "\n");
            strSqlString.Append("                                     , MWIPMATDEF B" + "\n");
            strSqlString.Append("                                 WHERE 1=1" + "\n");
            strSqlString.Append("                                   AND A.PARTNUMBER = B.MAT_ID" + "\n");
            strSqlString.Append("                                   AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                   AND B.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                               )" + "\n");
            strSqlString.Append("                         WHERE RNK = 1" + "\n");
            strSqlString.Append("                       ) BOM " + "\n");
            strSqlString.Append("                     , ( " + "\n");
            strSqlString.Append("                        SELECT MAT_ID " + "\n");
            strSqlString.Append("                             , SUM(DECODE(STORAGE_LOCATION, '1000', QUANTITY, 0)) AS INV_QTY " + "\n");
            strSqlString.Append("                             , SUM(DECODE(STORAGE_LOCATION, '1001', QUANTITY, 0)) AS INV_L_QTY " + "\n");
            strSqlString.Append("                          FROM CWMSLOTSTS@RPTTOMES " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND QUANTITY> 0 " + "\n");
            strSqlString.Append("                           AND STORAGE_LOCATION IN ('1000', '1001') " + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                       ) CWIP " + "\n");
            strSqlString.Append("                     , ( " + "\n");
            strSqlString.Append("                        SELECT MAT_ID" + "\n");
            strSqlString.Append("                             , SUM(QTY_1) AS QTY_TTL " + "\n");
            strSqlString.Append("                          FROM RWIPLOTSTS " + "\n");
            strSqlString.Append("                         WHERE 1=1  " + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND LOT_TYPE != 'W'" + "\n");
            strSqlString.Append("                           AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND LOT_CMF_2 = '-' " + "\n");
            strSqlString.Append("                           AND LOT_CMF_9 != ' ' " + "\n");
            strSqlString.Append("                           AND QTY_1 > 0 " + "\n");
            strSqlString.Append("                           AND OPER NOT IN  ('00001', '00002', 'V0000') " + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID  " + "\n");
            strSqlString.Append("                       ) WIP " + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND BOM.MATCODE = CWIP.MAT_ID(+)" + "\n");
            strSqlString.Append("                   AND BOM.MATCODE = WIP.MAT_ID(+)         " + "\n");
            strSqlString.Append("                 GROUP BY BOM.PART" + "\n");
            strSqlString.Append("                 HAVING SUM(NVL(CWIP.INV_QTY,0)) + SUM(NVL(CWIP.INV_L_QTY,0)) + SUM(NVL(WIP.QTY_TTL,0)) > 0" + "\n");
            strSqlString.Append("               ) D" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND A.PART = B.PART(+)" + "\n");
            strSqlString.Append("           AND A.PART = C.PART(+)" + "\n");
            strSqlString.Append("           AND A.PART = D.PART(+)" + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" ORDER BY TO_NUMBER(RNK_IN ) " + "\n");


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

            sqlString.Append("SELECT TO_CHAR(SYSDATE -1 + 2/24, 'YYYYMMDD') AS Y_DAY, SYS_DATE, PLAN_YEAR||LPAD(PLAN_WEEK,2,'0') AS PLAN_WEEK  " + "\n");
            sqlString.Append("  FROM MWIPCALDEF" + "\n");
            sqlString.Append(" WHERE CALENDAR_ID = 'OTD'" + "\n");
            sqlString.Append("   AND SYS_DATE IN (TO_CHAR(SYSDATE + 2/24, 'YYYYMMDD'), TO_CHAR(SYSDATE + 7 + 2/24, 'YYYYMMDD'))" + "\n");

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

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 2, null, null);
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함
                
                   //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 2, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);

                //spdData.DataSource = dt;

                spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 10, 0, false);
                spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 13, 0, false);             

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
            //this.SetFactory(cdvFactory.txtValue);
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }
                
        #endregion
    }
}
