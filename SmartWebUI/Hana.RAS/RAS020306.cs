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

namespace Hana.RAS
{
    /// <summary>
    /// 클  래  스: RAS020306<br/>
    /// 클래스요약: 순간정지 설비별 점유율<br/>
    /// 작  성  자: 미라콤 양형석<br/>
    /// 최초작성일: 2009-01-06<br/>
    /// 상세  설명: 순간정지 TOP 10<br/>
    /// 변경  내용: <br/>
    /// 2010-03-16-임종우 : DPMD, 생산량, RATIO, 누적점유, CHART 삭제(권순태C 요청)
    /// 2010-03-16-임종우 : TOP10 설비별 순간정지 TOP10 LIST 표시
    /// </summary>
    public partial class RAS020306 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public RAS020306()
        {
            InitializeComponent();
            udcFromToDate.AutoBinding();
            udcFromToDate.DaySelector.SelectedValue = "MONTH";
            SortInit();
            GridColumnInit();
            udcChartFX1.RPT_1_ChartInit();  //차트 초기화. 
        }

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

            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            //spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            //spdData.ActiveSheet.RowHeader.ColumnCount = 0;
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("Team in charge", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Part", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("EQP Type", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Maker", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);

            spdData.RPT_AddBasicColumn("classification", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 100);
            for (int i = 0; i < 10; i++)
            {
                spdData.RPT_AddBasicColumn((i+1).ToString(), 0, 5+i, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
            }
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Team in charge", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_1), '-') AS TEAM", "TEAM", "RES.RES_GRP_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Part", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_2), '-') AS PART", "PART", "RES.RES_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("EQP Type", "RES.RES_GRP_3 AS EQP_TYPE", "EQP_TYPE", "RES_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Maker", "RES.RES_GRP_5 AS MAKER", "MAKER", "RES.RES_GRP_5", false);
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

            string strResID = string.Empty;
            string strCnt = string.Empty;
            string strModel = string.Empty;
            string strMTBA = string.Empty;
            string strDPMO = string.Empty;
            string strRunTime = string.Empty;
            string strQty = string.Empty;
            string strRatio = string.Empty;
            string strAddedRatio = string.Empty;
            string strNo1 = string.Empty;
            string strNo2 = string.Empty;
            string strNo3 = string.Empty;
            string strNo4 = string.Empty;
            string strNo5 = string.Empty;
            string strNo6 = string.Empty;
            string strNo7 = string.Empty;
            string strNo8 = string.Empty;
            string strNo9 = string.Empty;
            string strNo10 = string.Empty;

            string strPrevfix = string.Empty;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;


            #region " udcDurationPlus에서 정확한 조회시간을 받아오기 : strFromDate, strToDate "
            string strFromDate = udcFromToDate.ExactFromDate;
            string strToDate = udcFromToDate.ExactToDate;
            #endregion

            #region " DECODE 쿼리 스트링 구하기 "
            strPrevfix = "     , MAX(DECODE(RNK, ";
            strResID     = CmnFunction.GetDecodeQueryString(strPrevfix, ", RES_ID, ' ')) RES_ID_", 10);
            strCnt       = CmnFunction.GetDecodeQueryString(strPrevfix, ", CNT, 0)) CNT_", 10);
            strModel     = CmnFunction.GetDecodeQueryString(strPrevfix, ", MODEL, ' ')) MODEL_", 10);
            strMTBA      = CmnFunction.GetDecodeQueryString(strPrevfix, ", MTBA, 0)) MTBA_", 10);
            strDPMO      = CmnFunction.GetDecodeQueryString(strPrevfix, ", DPMO, 0)) DPMO_", 10);
            strRunTime   = CmnFunction.GetDecodeQueryString(strPrevfix, ", RUN_TIME, 0)) RUN_TIME_", 10);
            strQty       = CmnFunction.GetDecodeQueryString(strPrevfix, ", QTY, 0)) QTY_", 10);
            strRatio     = CmnFunction.GetDecodeQueryString(strPrevfix, ", RATIO, 0)) RATIO_", 10);
            strAddedRatio = CmnFunction.GetDecodeQueryString(strPrevfix, ", ADDED_RATIO, 0)) ADDED_RATIO_", 10);

            //2010-03-16-임종우 : 순간정지 TOP10 LIST 추가
            strNo1 = CmnFunction.GetDecodeQueryString(strPrevfix, ", NO1, 0)) NO1_", 10);
            strNo2 = CmnFunction.GetDecodeQueryString(strPrevfix, ", NO2, 0)) NO2_", 10);
            strNo3 = CmnFunction.GetDecodeQueryString(strPrevfix, ", NO3, 0)) NO3_", 10);
            strNo4 = CmnFunction.GetDecodeQueryString(strPrevfix, ", NO4, 0)) NO4_", 10);
            strNo5 = CmnFunction.GetDecodeQueryString(strPrevfix, ", NO5, 0)) NO5_", 10);
            strNo6 = CmnFunction.GetDecodeQueryString(strPrevfix, ", NO6, 0)) NO6_", 10);
            strNo7 = CmnFunction.GetDecodeQueryString(strPrevfix, ", NO7, 0)) NO7_", 10);
            strNo8 = CmnFunction.GetDecodeQueryString(strPrevfix, ", NO8, 0)) NO8_", 10);
            strNo9 = CmnFunction.GetDecodeQueryString(strPrevfix, ", NO9, 0)) NO9_", 10);
            strNo10 = CmnFunction.GetDecodeQueryString(strPrevfix, ", NO10, 0)) NO10_", 10);
            #endregion

            #region " 조회조건에 해당하는 총 순간정지건수 합계 구하기 : TOT_CNT "

            string TOT_CNT = string.Empty;
            string RAS_LIST = string.Empty;

            DataTable dt = null;
            DataTable dt2 = null;
            
            strSqlString.Append("SELECT NVL(SUM(ALM.CNT),0) TOT_CNT  " + "\n");
            strSqlString.Append("  FROM (   " + "\n");
            strSqlString.Append("            SELECT FACTORY, RES_ID, COUNT(*) CNT   " + "\n");
            strSqlString.Append("              FROM CRASALMHIS  " + "\n");
            strSqlString.Append("             WHERE 1=1  " + "\n");
            strSqlString.Append("               AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "' " + "\n");
            strSqlString.Append("               AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("               AND CLEAR_TIME > 0  " + "\n");
            strSqlString.Append("             GROUP BY FACTORY, RES_ID  " + "\n");
            strSqlString.Append("       ) ALM " + "\n");
            strSqlString.Append("     , MRASRESDEF RES   " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND ALM.FACTORY = RES.FACTORY " + "\n");
            strSqlString.Append("   AND ALM.RES_ID = RES.RES_ID " + "\n");

            #region " RAS 상세 조회 "
            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

            strSqlString.Append("   AND RES.RES_TYPE NOT IN ('DUMMY')" + "\n");
            #endregion

            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
            if (dt != null)
                TOT_CNT = dt.Rows[0][0].ToString();

            strSqlString.Remove(0, strSqlString.Length);

            // 2010-03-16-임종우 : 순간정지 Top10 설비 LIST 가져오기
            dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringRas());

            for (int y = 0; y < dt2.Rows.Count; y++)
            {
                RAS_LIST += "'" + dt2.Rows[y][0].ToString() + "',";
            }

            RAS_LIST = RAS_LIST.Substring(0, RAS_LIST.Length - 1);
            #endregion

            #region " MAIN 쿼리 "
            strSqlString.Append("SELECT " + QueryCond2 + " " + "\n");
            strSqlString.Append("     , ' ' GUBUN " + "\n");
            strSqlString.Append(strResID);
            strSqlString.Append(strCnt);
            strSqlString.Append(strModel);
            strSqlString.Append(strMTBA);
            //strSqlString.Append(strDPMO);
            strSqlString.Append(strRunTime);
            //strSqlString.Append(strQty);
            //strSqlString.Append(strRatio);
            //strSqlString.Append(strAddedRatio);
            strSqlString.Append(strNo1);
            strSqlString.Append(strNo2);
            strSqlString.Append(strNo3);
            strSqlString.Append(strNo4);
            strSqlString.Append(strNo5);
            strSqlString.Append(strNo6);
            strSqlString.Append(strNo7);
            strSqlString.Append(strNo8);
            strSqlString.Append(strNo9);
            strSqlString.Append(strNo10);
            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT " + QueryCond1 + " " + "\n");
            strSqlString.Append("             , TO_CHAR(A.RNK) RNK " + "\n");
            strSqlString.Append("             , A.RES_ID " + "\n");
            strSqlString.Append("             , RES.RES_GRP_6 MODEL " + "\n");
            strSqlString.Append("             , A.CNT  " + "\n");
            strSqlString.Append("             , ROUND(DECODE(A.CNT, 0, 0, NVL(DWH.RUN_TIME, (TO_DATE('" + strToDate + "', 'YYYYMMDDHH24MISS') - TO_DATE('" + strFromDate + "', 'YYYYMMDDHH24MISS'))*1440)/A.CNT), 4) MTBA " + "\n");
            //strSqlString.Append("             , ROUND(DECODE(NVL(QTY.QTY, 0), 0, 0, DECODE(QTY.QTY, 0, 0, A.CNT/QTY.QTY))*1000000, 4) DPMO " + "\n");
            strSqlString.Append("             , ROUND(NVL(DWH.RUN_TIME, (TO_DATE('" + strToDate + "', 'YYYYMMDDHH24MISS') - TO_DATE('" + strFromDate + "', 'YYYYMMDDHH24MISS'))*1440),4) RUN_TIME " + "\n");
            //strSqlString.Append("             , NVL(QTY.QTY, 0) QTY " + "\n");
            //strSqlString.Append("             , ROUND((RATIO_TO_REPORT(A.CNT) OVER())*100, 2) RATIO  " + "\n");
            //strSqlString.Append("             , ROUND(SUM(B.CNT)/" + TOT_CNT + "*100, 2) ADDED_RATIO  " + "\n");
            strSqlString.Append("             , B.NO1, B.NO2, B.NO3, B.NO4, B.NO5, B.NO6, B.NO7, B.NO8, B.NO9, B.NO10 " + "\n");
            strSqlString.Append("          FROM (  " + "\n");
            strSqlString.Append("                    SELECT FACTORY, RNK, RES_ID, CNT    " + "\n");
            strSqlString.Append("                      FROM (   " + "\n");
            strSqlString.Append("                            SELECT ALM.FACTORY " + "\n");
            strSqlString.Append("                                 , ROW_NUMBER() OVER(ORDER BY SUM(ALM.CNT) DESC) RNK " + "\n");
            strSqlString.Append("                                 , ALM.RES_ID   " + "\n");
            strSqlString.Append("                                 , SUM(ALM.CNT) CNT  " + "\n");
            strSqlString.Append("                              FROM (   " + "\n");
            strSqlString.Append("                                    SELECT FACTORY, RES_ID, COUNT(*) CNT   " + "\n");
            strSqlString.Append("                                      FROM CRASALMHIS  " + "\n");
            strSqlString.Append("                                     WHERE 1=1  " + "\n");
            strSqlString.Append("                                       AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "' " + "\n");
            strSqlString.Append("                                       AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                                       AND CLEAR_TIME > 0  " + "\n");
            strSqlString.Append("                                     GROUP BY FACTORY, RES_ID  " + "\n");
            strSqlString.Append("                                   ) ALM   " + "\n");
            strSqlString.Append("                             WHERE 1=1    " + "\n");
            strSqlString.Append("                             GROUP BY ALM.FACTORY, ALM.RES_ID " + "\n");
            strSqlString.Append("                           )   " + "\n");
            strSqlString.Append("                     WHERE RNK < 11  " + "\n");
            strSqlString.Append("               ) A  " + "\n");
            //strSqlString.Append("             , (  " + "\n");
            //strSqlString.Append("                    SELECT FACTORY, RNK, RES_ID, CNT    " + "\n");
            //strSqlString.Append("                      FROM (   " + "\n");
            //strSqlString.Append("                            SELECT ALM.FACTORY " + "\n");
            //strSqlString.Append("                                 , ROW_NUMBER() OVER(ORDER BY SUM(ALM.CNT) DESC) RNK " + "\n");
            //strSqlString.Append("                                 , ALM.RES_ID   " + "\n");
            //strSqlString.Append("                                 , SUM(ALM.CNT) CNT  " + "\n");
            //strSqlString.Append("                              FROM (   " + "\n");
            //strSqlString.Append("                                    SELECT FACTORY, RES_ID, COUNT(*) CNT   " + "\n");
            //strSqlString.Append("                                      FROM CRASALMHIS  " + "\n");
            //strSqlString.Append("                                     WHERE 1=1  " + "\n");
            //strSqlString.Append("                                       AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "' " + "\n");
            //strSqlString.Append("                                       AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            //strSqlString.Append("                                     GROUP BY FACTORY, RES_ID  " + "\n");
            //strSqlString.Append("                                   ) ALM   " + "\n");
            //strSqlString.Append("                             WHERE 1=1    " + "\n");
            //strSqlString.Append("                             GROUP BY ALM.FACTORY, ALM.RES_ID " + "\n");
            //strSqlString.Append("                           )   " + "\n");
            //strSqlString.Append("                     WHERE RNK < 11  " + "\n");
            //strSqlString.Append("               ) B  " + "\n");
            strSqlString.Append("             , (  " + "\n");
            strSqlString.Append("                 SELECT FACTORY, RES_ID" + "\n");
            strSqlString.Append("                     , MAX(DECODE(RNK, 1, ALARM_DESC)) NO1" + "\n");
            strSqlString.Append("                     , MAX(DECODE(RNK, 2, ALARM_DESC)) NO2" + "\n");
            strSqlString.Append("                     , MAX(DECODE(RNK, 3, ALARM_DESC)) NO3" + "\n");
            strSqlString.Append("                     , MAX(DECODE(RNK, 4, ALARM_DESC)) NO4" + "\n");
            strSqlString.Append("                     , MAX(DECODE(RNK, 5, ALARM_DESC)) NO5" + "\n");
            strSqlString.Append("                     , MAX(DECODE(RNK, 6, ALARM_DESC)) NO6" + "\n");
            strSqlString.Append("                     , MAX(DECODE(RNK, 7, ALARM_DESC)) NO7" + "\n");
            strSqlString.Append("                     , MAX(DECODE(RNK, 8, ALARM_DESC)) NO8" + "\n");
            strSqlString.Append("                     , MAX(DECODE(RNK, 9, ALARM_DESC)) NO9" + "\n");
            strSqlString.Append("                     , MAX(DECODE(RNK, 10, ALARM_DESC)) NO10" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT FACTORY, RNK, RES_ID, ALARM_ID, ALARM_DESC, CNT" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT FACTORY, ROW_NUMBER() OVER(PARTITION BY RES_ID ORDER BY CNT DESC) RNK, RES_ID, ALARM_ID, ALARM_DESC, CNT" + "\n");
            strSqlString.Append("                                  FROM (" + "\n");
            strSqlString.Append("                                        SELECT FACTORY, RES_ID, ALARM_ID, ALARM_DESC, COUNT(*) CNT" + "\n");
            strSqlString.Append("                                          FROM CRASALMHIS  " + "\n");
            strSqlString.Append("                                         WHERE 1=1  " + "\n");
            strSqlString.Append("                                           AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "' " + "\n");
            strSqlString.Append("                                           AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                                           AND RES_ID IN (" + RAS_LIST + ")" + "\n");
            strSqlString.Append("                                           AND CLEAR_TIME > 0  " + "\n");
            strSqlString.Append("                                         GROUP BY FACTORY, RES_ID, ALARM_ID, ALARM_DESC" + "\n");
            strSqlString.Append("                                       )" + "\n");
            strSqlString.Append("                               )" + "\n");
            strSqlString.Append("                         WHERE RNK < 11" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY FACTORY, RES_ID" + "\n");
            strSqlString.Append("               ) B  " + "\n");
            strSqlString.Append("             , (  " + "\n");
            strSqlString.Append("                    SELECT FACTORY, RES_ID, (TO_DATE('" + strToDate + "', 'YYYYMMDDHH24MISS') - TO_DATE('" + strFromDate + "', 'YYYYMMDDHH24MISS'))*1440 - SUM(TIME_SUM)/60 RUN_TIME " + "\n");
            strSqlString.Append("                      FROM CSUMRESDWH  " + "\n");
            strSqlString.Append("                     WHERE 1=1 " + "\n");
            strSqlString.Append("                       AND DOWN_DATE BETWEEN GET_WORK_DATE('" + strFromDate + "', 'D') AND GET_WORK_DATE('" + strToDate + "', 'D') " + "\n");
            strSqlString.Append("                       AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                     GROUP BY FACTORY, RES_ID " + "\n");
            strSqlString.Append("               ) DWH  " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                    SELECT FACTORY, RES_ID, SUM(END_QTY_1) QTY " + "\n");
            strSqlString.Append("                      FROM RSUMRESMOV " + "\n");
            strSqlString.Append("                     WHERE 1=1 " + "\n");
            strSqlString.Append("                       AND WORK_DATE BETWEEN '" + strFromDate.Substring(0, 8) + "' AND '" + strToDate.Substring(0, 8) + "' " + "\n");
            strSqlString.Append("                       AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                       AND MAT_VER = 1 " + "\n");
            strSqlString.Append("                     GROUP BY FACTORY, RES_ID " + "\n");
            strSqlString.Append("               ) QTY " + "\n");
            strSqlString.Append("             , MRASRESDEF RES     " + "\n");
            strSqlString.Append("         WHERE 1=1  " + "\n");
            //strSqlString.Append("           AND A.RNK > B.RNK(+) - 1  " + "\n");
            strSqlString.Append("           AND A.FACTORY = B.FACTORY(+)  " + "\n");
            strSqlString.Append("           AND A.RES_ID = B.RES_ID(+)  " + "\n");
            strSqlString.Append("           AND A.FACTORY = DWH.FACTORY(+)  " + "\n");
            strSqlString.Append("           AND A.RES_ID = DWH.RES_ID(+)  " + "\n");
            strSqlString.Append("           AND A.FACTORY = QTY.FACTORY(+) " + "\n");
            strSqlString.Append("           AND A.RES_ID = QTY.RES_ID(+) " + "\n");
            strSqlString.Append("           AND A.FACTORY = RES.FACTORY " + "\n");
            strSqlString.Append("           AND A.RES_ID = RES.RES_ID " + "\n");

            #region " RAS 상세 조회 "
            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

            strSqlString.Append("           AND RES.RES_TYPE NOT IN ('DUMMY')" + "\n");
            #endregion

            //strSqlString.Append("         GROUP BY RES.FACTORY, " + QueryCond3 + ", A.RNK, A.RES_ID, RES.RES_GRP_6, A.CNT, DWH.RUN_TIME, QTY.QTY " + "\n");
            strSqlString.Append("         GROUP BY RES.FACTORY, " + QueryCond3 + ", A.RNK, A.RES_ID, RES.RES_GRP_6, A.CNT, DWH.RUN_TIME, B.NO1, B.NO2, B.NO3, B.NO4, B.NO5, B.NO6, B.NO7, B.NO8, B.NO9, B.NO10" + "\n");
            strSqlString.Append("       ) " + "\n");
            strSqlString.Append(" GROUP BY " + QueryCond2 + " " + "\n");
            strSqlString.Append(" ORDER BY " + QueryCond2 + " " + "\n");
            #endregion

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        // 2010-03-16-임종우 : 순간정지 TOP10 설비 LIST 가져오기
        private string MakeSqlStringRas()
        {

            StringBuilder strSqlString = new StringBuilder();

            #region " udcDurationPlus에서 정확한 조회시간을 받아오기 : strFromDate, strToDate "
            string strFromDate = udcFromToDate.ExactFromDate;
            string strToDate = udcFromToDate.ExactToDate;
            #endregion

            strSqlString.Append("SELECT RES_ID " + "\n");
            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT ALM.FACTORY  " + "\n");
            strSqlString.Append("             , ROW_NUMBER() OVER(ORDER BY SUM(ALM.CNT) DESC) RNK " + "\n");
            strSqlString.Append("             , ALM.RES_ID " + "\n");
            strSqlString.Append("             , SUM(ALM.CNT) CNT " + "\n");
            strSqlString.Append("          FROM (   " + "\n");
            strSqlString.Append("                SELECT HIS.FACTORY, HIS.RES_ID, COUNT(*) CNT " + "\n");
            strSqlString.Append("                  FROM CRASALMHIS HIS " + "\n");
            strSqlString.Append("                     , MRASRESDEF RES " + "\n");
            strSqlString.Append("                 WHERE 1=1  " + "\n");
            strSqlString.Append("                   AND HIS.TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "' " + "\n");
            strSqlString.Append("                   AND HIS.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND HIS.FACTORY = RES.FACTORY " + "\n");
            strSqlString.Append("                   AND HIS.RES_ID = RES.RES_ID " + "\n");
            strSqlString.Append("                   AND HIS.CLEAR_TIME > 0  " + "\n");

            #region " RAS 상세 조회 "
            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("                   AND RES.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("                   AND RES.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("                   AND RES.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("                   AND RES.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("                   AND RES.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("                   AND RES.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("                   AND RES.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

            strSqlString.Append("                   AND RES.RES_TYPE NOT IN ('DUMMY')" + "\n");
            #endregion

            strSqlString.Append("                 GROUP BY HIS.FACTORY, HIS.RES_ID " + "\n");
            strSqlString.Append("               ) ALM " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("         GROUP BY ALM.FACTORY, ALM.RES_ID " + "\n");
            strSqlString.Append("       ) " + "\n");
            strSqlString.Append(" WHERE RNK < 11 " + "\n");

            return strSqlString.ToString();
        }

        /// <summary>
        /// 5. Chart 생성
        /// </summary>
        /// <param name="DT">Chart를 생성할 데이터 테이블</param>
        private void MakeChart(DataTable DT)
        {

        }

        private void ShowChart(int rowCount)
        {
            // 차트 설정
            udcChartFX1.RPT_1_ChartInit();
            udcChartFX1.RPT_2_ClearData();

            if (((DataTable)spdData.DataSource).Rows.Count < 9)
                return;

            udcChartFX1.RPT_3_OpenData(2, 10);
            int[] cnt_columns = new Int32[10];
            int[] addedRatio_columns = new Int32[10];
            int[] columnsHeader = new Int32[10];

            for (int i = 0; i < cnt_columns.Length; i++)
            {
                columnsHeader[i] = 5 + i;
                cnt_columns[i] = 5 + i;
                addedRatio_columns[i] = 5 + i;
            }

            double maxCnt = udcChartFX1.RPT_4_AddData(spdData, new int[] { 1 }, cnt_columns, SeriseType.Rows);
            double maxAddedRatio = udcChartFX1.RPT_4_AddData(spdData, new int[] { 8 }, cnt_columns, SeriseType.Rows);

            udcChartFX1.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 0, 1, "건수", AsixType.Y, DataTypes.Initeger, maxCnt * 1.2);
            udcChartFX1.RPT_6_SetGallery(ChartType.Line, 1, 1, "누적점유", AsixType.Y2, DataTypes.Initeger, maxAddedRatio);

            //각 Serise별로 동일한 타입을 사용할 경우
            //udcChartFX1.RPT_6_SetGallery(ChartType.Bar, "[단위 : sls]", AsixType.Y, DataTypes.Initeger, yield * 1.2);

            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "건수", "누적점유" }, SoftwareFX.ChartFX.Docked.Top);

            // 기타 설정
            udcChartFX1.PointLabels = false;
            udcChartFX1.Series[0].PointLabelColor = Color.Blue;
            udcChartFX1.Series[1].PointLabelColor = Color.Green;
            udcChartFX1.Series[1].LineWidth = 2;
            udcChartFX1.AxisY2.Max = 100;
            udcChartFX1.AxisY2.LabelsFormat.Decimals = 2;
            udcChartFX1.AxisY2.DataFormat.Format = SoftwareFX.ChartFX.AxisFormat.Number;
            udcChartFX1.AxisY2.DataFormat.Decimals = 4;
            udcChartFX1.RecalcScale();
        }

        #endregion

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

                //by John
                //1.Griid 합계 표시
                if(dt.Rows.Count > 1)
                {
                    spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 0, 5, 5, 9, 10);
                }
                else
                {
                    spdData.DataSource = dt;
                    //spdData.RPT_DivideRows(5, 9, 10);

                    spdData.RPT_DivideRows(5, 15, 10);
                }

                // 누적점유율이 0이면 Null값 주기
                //for (int i = 5; i < 15; i++)
                //{
                //    if (Convert.ToDouble(spdData.ActiveSheet.Cells[8, i].Value) == 0)
                //        spdData.ActiveSheet.Cells[8, i].Value = null;
                //}

                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 4, 0, 9, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit4
                spdData.RPT_AutoFit(false);               

                // 2010-03-16-임종우 : 설비별 순간정지 TOP10 LIST 가져오기, 필요없는 정보 삭제, CHART 삭제(권순태C 요청)
                spdData.RPT_FillColumnData(4, new string[] { "Equipment name", "Number", "Model", "MTBA", "Uptime", "Momentary stop_Rank1", "Momentary stop_Rank2", "Momentary stop_Rank3", "Momentary stop_Rank4", "Momentary stop_Rank5", "Momentary stop_Rank6", "Momentary stop_Rank7", "Momentary stop_Rank8", "Momentary stop_Rank9", "Momentary stop_Rank10" });
                //ShowChart(0);

                spdData.ActiveSheet.Rows[0].BackColor = Color.LemonChiffon;                
                spdData.ActiveSheet.Columns[4].BackColor = Color.LemonChiffon;
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
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ");
            spdData.ExportExcel();         
        }

        #region 기타
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            //cdvProduct.sFactory = cdvFactory.txtValue;
        }

        #endregion
    }
}