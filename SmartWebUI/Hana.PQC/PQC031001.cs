using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.UI.Controls;

namespace Hana.PQC
{
    /// <summary>
    /// 클  래  스: PQC031001<br/>
    /// 클래스요약: 오븐Monitoring<br/>
    /// 작  성  자: ZON 박봉준<br/>
    /// 최초작성일: 2009-08-13<br/>
    /// 상세  설명: 오븐Monitoring을 Chart로 보여준다.<br/>
    /// 변경  내용: [2009.11.17 정중희]  <br/>
    /// </summary>
    /// 
    public partial class PQC031001 : Miracom.SmartWeb.UI.Controls.udcCUSReportOven001
    {
        #region Variable Definition

        private String title1 = "";
        private String Pattern1 = "";
        private String Mindate1 = "";

        // 2010-08-11-안시홍 : 4개의 그래프를 1개로 설정하므로 주석처리
        //private String title2 = "";
        //private String Pattern2 = "";
        //private String Mindate2 = "";
        //private String title3 = "";
        //private String Pattern3 = "";
        //private String Mindate3 = "";
        //private String title4 = "";
        //private String Pattern4 = "";
        //private String Mindate4 = "";

        // SpreadSheet Cell 선택 컬럼 정보
        private static int s_selected_cell_col = 0;

        // Radio Button 선택 정보
        private static string s_oven_res = "";

        // 2010-08-07-안시홍 : 초기 Refresh Time 정보
        private int intTime = 300;

        #endregion

        #region Function Definition

        public PQC031001()
        {
            InitializeComponent();
            GridColumnInit_Status(); //헤더 한줄짜리 
            GridColumnInit_List();

            // 챠트 초기화
            udcChartFX1.RPT_1_ChartInit();
            //udcChartFX2.RPT_1_ChartInit();
            //udcChartFX3.RPT_1_ChartInit();
            //udcChartFX4.RPT_1_ChartInit();
        }

        private void GridColumnInit_Status()
        {
            try
            {
                udcFarPointStatus.RPT_ColumnInit();
                udcFarPointStatus.RPT_AddBasicColumn("number", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 40);
                udcFarPointStatus.RPT_AddBasicColumn("PATTERN", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);     //2010-08-09-안시홍 : Recipe 명 보이게 설정.
                udcFarPointStatus.RPT_AddBasicColumn("Status", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                udcFarPointStatus.RPT_AddBasicColumn("start", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                udcFarPointStatus.RPT_AddBasicColumn("time", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);       //2010-08-09-안시홍 : 온도시간 보이게 설정.
                udcFarPointStatus.RPT_AddBasicColumn("Lot수", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 40);
                udcFarPointStatus.RPT_AddBasicColumn("Lot_ID", 0, 6, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 40);
                udcFarPointStatus.RPT_AddBasicColumn("Time", 0, 7, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 40);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        private void GridColumnInit_List()
        {
            try
            {
                udcFarPointList.RPT_ColumnInit();
                udcFarPointList.RPT_AddBasicColumn("number", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 40);
                udcFarPointList.RPT_AddBasicColumn("LotID", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
                udcFarPointList.RPT_AddBasicColumn("Product", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                udcFarPointList.RPT_AddBasicColumn("IN QTY", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }


        private string OvenResSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("SELECT DISTINCT RES_GRP_3" + "\n");
            strSqlString.AppendFormat("FROM MRASRESDEF" + "\n");
            strSqlString.AppendFormat("WHERE 1=1" + "\n");
            strSqlString.AppendFormat("AND RES_PROC_MODE='FULL AUTO'" + "\n");
            strSqlString.AppendFormat("AND RES_GRP_3 <> 'WIRE BOND'" + "\n");
            strSqlString.AppendFormat("ORDER BY RES_GRP_3" + "\n");

            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            return strSqlString.ToString();

        }

        // 2010-08-08-안시홍 : 쿼리 수정
        private string StatusSqlString(string OvenRes)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("SELECT A.RES_ID, A.PATTERN_NO, STATUS, START_TIME, TEMP_DATE, CNT, A.LOT_ID, MIN(TEMP_TIME)" + "\n");
            strSqlString.AppendFormat("FROM (" + "\n");
            strSqlString.AppendFormat("SELECT RES_ID, PATTERN_NO, DECODE(CNT, 0, 'WAIT','PROC') AS STATUS, START_TIME, TEMP_DATE, DECODE(CNT,0, '', CNT) AS CNT, LOT_ID" + "\n");
            strSqlString.AppendFormat("     FROM (" + "\n");
            strSqlString.AppendFormat("         SELECT RES_ID, MAX(PATTERN_NO) AS PATTERN_NO, MAX(START_TIME) AS START_TIME, MAX(TEMP_DATE) AS TEMP_DATE, MAX(CNT) AS CNT, MAX(LOT_ID) AS LOT_ID" + "\n");
            strSqlString.AppendFormat("             FROM(" + "\n");
            strSqlString.AppendFormat("                 SELECT /*+ INDEX(A MWIPLOTSTS_IDX_6) */ A.START_RES_ID AS RES_ID, PATTERN_NO" + "\n");
            strSqlString.AppendFormat("                     , substr(MIN(A.LAST_TRAN_TIME),5,4)||' '||substr(MIN(A.LAST_TRAN_TIME),9, 2)||':'||substr(MIN(A.LAST_TRAN_TIME),11, 2) AS START_TIME" + "\n");
            strSqlString.AppendFormat("                     , MIN(A.LAST_TRAN_TIME) AS TEMP_DATE, COUNT(A.LOT_ID) AS CNT, MIN(A.LOT_ID) AS LOT_ID" + "\n");
            strSqlString.AppendFormat("                     FROM MWIPLOTSTS@RPTTOMES A, MWIPMATPTN@RPTTOMES B, MWIPLOTHIS@RPTTOMES C" + "\n");
            strSqlString.AppendFormat("                 WHERE A.FACTORY = B.FACTORY" + "\n");
            strSqlString.AppendFormat("                     AND B.FACTORY = C.FACTORY" + "\n");
            strSqlString.AppendFormat("                     AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.AppendFormat("                     AND B.MAT_ID = C.MAT_ID" + "\n");
            strSqlString.AppendFormat("                     AND A.OPER = B.OPER" + "\n");
            strSqlString.AppendFormat("                     AND B.OPER = C.OPER" + "\n");
            strSqlString.AppendFormat("                     AND A.LOT_ID = C.LOT_ID" + "\n");
            strSqlString.AppendFormat("                     AND C.TRAN_CMF_4 > ' '" + "\n");
            strSqlString.AppendFormat("                     AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("                     AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.AppendFormat("                     AND A.LOT_STATUS ='PROC'" + "\n");
            strSqlString.AppendFormat("                     AND A.LAST_TRAN_CODE = 'START'" + "\n");
            strSqlString.AppendFormat("                     AND A.OPER IN (SELECT ATTR_KEY" + "\n");
            strSqlString.AppendFormat("                                     FROM MATRNAMSTS@RPTTOMES" + "\n");
            strSqlString.AppendFormat("                                     WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("                                         AND ATTR_TYPE = 'OPER'" + "\n");
            strSqlString.AppendFormat("                                         AND ATTR_NAME = 'PATTERN_CHECK'" + "\n");
            strSqlString.AppendFormat("                                         AND ATTR_VALUE = 'Y')" + "\n");
            strSqlString.AppendFormat("                     AND A.START_RES_ID IN (SELECT RES_ID" + "\n");
            strSqlString.AppendFormat("                                             FROM MRASRESDEF" + "\n");
            strSqlString.AppendFormat("                                             WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("                                             AND RES_GRP_3 = '" + OvenRes + "'" + "\n");
            strSqlString.AppendFormat("                                             AND RES_PROC_MODE = 'FULL AUTO')" + "\n");
            strSqlString.AppendFormat("                     AND B.PATTERN_NO > ' '" + "\n");
            strSqlString.AppendFormat("                 GROUP BY A.START_RES_ID, PATTERN_NO" + "\n");
            // 2010-08-24-안시홍 : 진행중(PROC)만 보이게 한다.
            //strSqlString.AppendFormat("                 UNION ALL" + "\n");
            //strSqlString.AppendFormat("                 SELECT RES_ID, '' AS PATTERN_NO, '' AS START_TIME, '' AS TEMP_DATE, 0 AS CNT, '' AS LOT_ID" + "\n");
            //strSqlString.AppendFormat("                     FROM MRASRESDEF" + "\n");
            //strSqlString.AppendFormat("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            //strSqlString.AppendFormat("                     AND RES_GRP_3 = '" + OvenRes + "'" + "\n");
            //strSqlString.AppendFormat("                     AND RES_PROC_MODE = 'FULL AUTO'" + "\n");
            strSqlString.AppendFormat("            )" + "\n");
            strSqlString.AppendFormat("             GROUP BY RES_ID" + "\n");
            strSqlString.AppendFormat(")" + "\n");
            strSqlString.AppendFormat(") A," + "\n");
            strSqlString.AppendFormat("MRASLOTOVN@RPTTOMES B" + "\n");
            strSqlString.AppendFormat("WHERE 1=1" + "\n");
            strSqlString.AppendFormat("AND A.LOT_ID =B.LOT_ID(+)" + "\n");
            strSqlString.AppendFormat("AND A.RES_ID= B.RES_ID(+)" + "\n");
            strSqlString.AppendFormat("GROUP BY A.RES_ID, A.PATTERN_NO, STATUS, START_TIME, TEMP_DATE, CNT, A.LOT_ID" + "\n");
            strSqlString.AppendFormat("ORDER BY STATUS, RES_ID" + "\n");




            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            return strSqlString.ToString();
        }

        private string LotListSqlString(string strresID)
        {
            StringBuilder strSqlString = new StringBuilder();

            string toDate = DateTime.Now.ToString("yyyy-MM-dd");
            string sFrom = DateTime.Parse(toDate).AddDays(-1).ToString("yyyyMMdd");
            string sTo = DateTime.Parse(toDate).ToString("yyyyMMdd");


            strSqlString.AppendFormat("SELECT A.START_RES_ID, A.LOT_ID, A.MAT_ID , A.QTY_1" + "\n");
            strSqlString.AppendFormat("     FROM RWIPLOTSTS A, MWIPMATPTN@RPTTOMES B" + "\n");
            strSqlString.AppendFormat("WHERE A.FACTORY = B.FACTORY" + "\n");
            strSqlString.AppendFormat("     AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.AppendFormat("     AND A.OPER = B.OPER" + "\n");
            strSqlString.AppendFormat("     AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("     AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.AppendFormat("     AND A.LOT_STATUS = 'PROC'" + "\n");
            strSqlString.AppendFormat("     AND A.LAST_TRAN_CODE = 'START'" + "\n");
            strSqlString.AppendFormat("     AND A.OPER IN (SELECT ATTR_KEY" + "\n");
            strSqlString.AppendFormat("                     FROM MATRNAMSTS" + "\n");
            strSqlString.AppendFormat("                     WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("                         AND ATTR_TYPE = 'OPER'" + "\n");
            strSqlString.AppendFormat("                         AND ATTR_NAME = 'PATTERN_CHECK'" + "\n");
            strSqlString.AppendFormat("                         AND ATTR_VALUE = 'Y')" + "\n");
            strSqlString.AppendFormat("     AND A.START_RES_ID IN ('" + strresID + "')" + "\n");
            strSqlString.AppendFormat("     AND B.PATTERN_NO <> ' '" + "\n");
            strSqlString.AppendFormat("ORDER BY A.LOT_ID" + "\n");


            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            return strSqlString.ToString();
        }

        private string DataChart(string Pattern, string Mindate)
        {
            /************************************************
             * comment : 실제온도에 대한 쿼리를 실행한다.
             * 
             * created by :
             * 
             * modified by : Si-hong An (2010-08-27-금요일)
             ************************************************/
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT OPER, MARK, REAL_DATA" + "\n");
            strSqlString.Append("   FROM(" + "\n");
            strSqlString.Append("       SELECT ROWNUM NUM, OPER, MARK" + "\n");
            strSqlString.Append("           FROM(" + "\n");
            strSqlString.Append("               SELECT OPER, DECODE(MARK, '', LAG(MARK) OVER (ORDER BY ROWNUM), MARK) AS MARK" + "\n");
            strSqlString.Append("                   FROM( " + "\n");
            strSqlString.Append("                       SELECT OPER, NVL(ROUND(FROM_TEMP+(TO_TEMP-FROM_TEMP)/TIME_GAP*(OPER-FROM_TIME)),TO_TEMP) AS MARK" + "\n");
            strSqlString.Append("                           FROM (" + "\n");
            strSqlString.Append("                               SELECT NVL(LAG(TIME)OVER(ORDER BY SEQUENCE),0) FROM_TIME, TIME TO_TIME, TIME_GAP, NVL(LAG(TEMP)OVER(ORDER BY SEQUENCE),25) FROM_TEMP, TEMP TO_TEMP" + "\n");
            strSqlString.Append("                                   FROM (" + "\n");
            strSqlString.Append("                                       SELECT SEQUENCE, SUM(TIME) OVER (ORDER BY SEQUENCE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS TIME, TIME TIME_GAP,  TEMP" + "\n");
            strSqlString.Append("                                           FROM MRASOVNDEF@RPTTOMES" + "\n");
            strSqlString.Append("                                           WHERE 1=1" + "\n");
            strSqlString.Append("                                               AND PATTERN_NO = '" + Pattern + "'" + "\n");
            strSqlString.Append("                                   ) " + "\n");
            strSqlString.Append("                           ) DEF," + "\n");
            strSqlString.Append("                           (" + "\n");
            strSqlString.Append("                               SELECT OPER" + "\n");
            strSqlString.Append("                                   FROM (SELECT 0 AS OPER FROM DUAL UNION ALL" + "\n");
            strSqlString.Append("                                   SELECT LEVEL AS OPER FROM DUAL WHERE LEVEL >= 0 CONNECT BY LEVEL <= (" + "\n");
            strSqlString.Append("                                       SELECT MAX(TIME) AS TIME" + "\n");
            strSqlString.Append("                                           FROM(" + "\n");
            strSqlString.Append("                                               SELECT SUM(TIME)OVER(ORDER BY SEQUENCE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS TIME" + "\n");
            strSqlString.Append("                                                   FROM MRASOVNDEF@RPTTOMES" + "\n");
            strSqlString.Append("                                                   WHERE 1=1" + "\n");
            strSqlString.Append("                                                       AND PATTERN_NO = '" + Pattern + "'" + "\n");
            strSqlString.Append("                                           )" + "\n");
            strSqlString.Append("                                       )" + "\n");
            strSqlString.Append("                                   )" + "\n");
            strSqlString.Append("                           ) TIME" + "\n");
            strSqlString.Append("                           WHERE (TIME.OPER >= DEF.FROM_TIME(+) and TIME.OPER < DEF.TO_TIME(+))" + "\n");
            strSqlString.Append("                   )" + "\n");
            strSqlString.Append("                   ORDER BY OPER" + "\n");
            strSqlString.Append("           )" + "\n");
            strSqlString.Append("   ) A," + "\n");
            strSqlString.Append("   (" + "\n");
            strSqlString.Append("       SELECT ROWNUM NUM, REAL_DATA" + "\n");
            strSqlString.Append("           FROM (" + "\n");
            strSqlString.Append("               SELECT TEMP AS REAL_DATA" + "\n");
            strSqlString.Append("                   FROM MRASLOTOVN@RPTTOMES" + "\n");
            strSqlString.Append("                   WHERE 1=1" + "\n");
            strSqlString.Append("                       AND LOT_ID = (" + "\n");
            strSqlString.Append("                           SELECT MAX(LOT_ID) FROM MRASLOTOVN@RPTTOMES" + "\n");
            strSqlString.Append("                               WHERE 1=1" + "\n");
            strSqlString.Append("                                   AND TEMP_TIME = '" + Mindate + "'" + "\n");
            strSqlString.Append("                                   AND RES_ID = '" + title1 + "'" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                       AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                       AND RES_ID = '" + title1 + "'" + "\n");
            strSqlString.Append("                   ORDER BY REAL_DATA ASC" + "\n");
            strSqlString.Append("           )" + "\n");
            strSqlString.Append("   ) B" + "\n");
            strSqlString.Append("   WHERE A.NUM = B.NUM(+)" + "\n");
            strSqlString.Append("   ORDER BY OPER" + "\n");


            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            return strSqlString.ToString();
        }

        //private string DataChart_Mark(string Pattern, string Mindate)
        //{
        //    /************************************************
        //     * comment : 기준온도에 대한 쿼리를 실행한다.
        //     * 
        //     * created by :
        //     * 
        //     * modified by : Si-hong An (2010-08-27-금요일)
        //     ************************************************/
        //    StringBuilder strSqlString = new StringBuilder();

        //    strSqlString.AppendFormat("SELECT OPER, DECODE(MARK, '', LAG(MARK) OVER (ORDER BY ROWNUM), MARK) AS MARK" + "\n");
        //    strSqlString.AppendFormat("     FROM(" + "\n");
        //    strSqlString.AppendFormat("           SELECT OPER, NVL(ROUND(FROM_TEMP+(TO_TEMP-FROM_TEMP)/TIME_GAP*(OPER-FROM_TIME)),TO_TEMP) AS MARK" + "\n");
        //    strSqlString.AppendFormat("           FROM (" + "\n");
        //    strSqlString.AppendFormat("                 SELECT NVL(LAG(TIME)OVER(ORDER BY SEQUENCE),0) FROM_TIME, TIME TO_TIME, TIME_GAP, NVL(LAG(TEMP)OVER(ORDER BY SEQUENCE),25) FROM_TEMP, TEMP TO_TEMP" + "\n");
        //    strSqlString.AppendFormat("                 FROM (" + "\n");
        //    strSqlString.AppendFormat("                         SELECT SEQUENCE, SUM(TIME)OVER(ORDER BY SEQUENCE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS TIME, TIME TIME_GAP,  TEMP" + "\n");
        //    strSqlString.AppendFormat("                         FROM MRASOVNDEF@RPTTOMES" + "\n");
        //    strSqlString.AppendFormat("                         WHERE 1=1" + "\n");
        //    strSqlString.AppendFormat("                             AND PATTERN_NO ='" + Pattern + "'" + "\n");
        //    strSqlString.AppendFormat("                      ) " + "\n");
        //    strSqlString.AppendFormat("           ) DEF," + "\n");
        //    strSqlString.AppendFormat("           (" + "\n");
        //    strSqlString.AppendFormat("           SELECT OPER FROM (" + "\n");
        //    strSqlString.AppendFormat("                 SELECT 0 AS OPER FROM DUAL UNION ALL" + "\n");
        //    strSqlString.AppendFormat("                 SELECT LEVEL AS OPER FROM DUAL WHERE LEVEL >= 0 connect by level <= (" + "\n");
        //    strSqlString.AppendFormat("                     SELECT MAX(TIME) AS TIME" + "\n");
        //    strSqlString.AppendFormat("                     FROM(" + "\n");
        //    strSqlString.AppendFormat("                         SELECT SUM(TIME)OVER(ORDER BY SEQUENCE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS TIME" + "\n");
        //    strSqlString.AppendFormat("                         FROM MRASOVNDEF@RPTTOMES" + "\n");
        //    strSqlString.AppendFormat("                         WHERE 1=1" + "\n");
        //    strSqlString.AppendFormat("                             AND PATTERN_NO='" + Pattern + "'" + "\n");
        //    strSqlString.AppendFormat("                         )" + "\n");
        //    strSqlString.AppendFormat("                 )" + "\n");
        //    strSqlString.AppendFormat("           )" + "\n");
        //    strSqlString.AppendFormat("     ) TIME" + "\n");
        //    strSqlString.AppendFormat("     WHERE (TIME.OPER >= DEF.FROM_TIME(+) and TIME.OPER < DEF.TO_TIME(+))" + "\n");
        //    strSqlString.AppendFormat(" )" + "\n");
        //    strSqlString.AppendFormat("ORDER BY OPER" + "\n");

        //    System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
        //    return strSqlString.ToString();
        //}

        //private string DataChart_Time(string Pattern, string Mindate)
        //{
        //    /************************************************
        //     * comment : 시간에 대한 쿼리를 실행한다.
        //     * 
        //     * created by : Si-hong An (2010-08-27-금요일)
        //     * 
        //     * modified by : Si-hong An (2010-08-27-금요일)
        //     ************************************************/
        //    StringBuilder strSqlString = new StringBuilder();

        //    strSqlString.Append("SELECT OPER " + "\n");
        //    strSqlString.Append("    FROM (SELECT 0 AS OPER FROM DUAL UNION ALL" + "\n");
        //    strSqlString.Append("    SELECT LEVEL AS OPER " + "\n");
        //    strSqlString.Append("        FROM DUAL " + "\n");
        //    strSqlString.Append("        WHERE LEVEL >= 0 connect by level <= (" + "\n");
        //    strSqlString.Append("            SELECT MAX(TIME) AS TIME" + "\n");
        //    strSqlString.Append("                FROM(" + "\n");
        //    strSqlString.Append("                    SELECT SUM(TIME) OVER (ORDER BY SEQUENCE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS TIME" + "\n");
        //    strSqlString.Append("                        FROM MRASOVNDEF@RPTTOMES" + "\n");
        //    strSqlString.Append("                        WHERE 1=1" + "\n");
        //    strSqlString.Append("                            AND PATTERN_NO='" + Pattern + "'" + "\n");
        //    strSqlString.Append("                )" + "\n");
        //    strSqlString.Append("        )" + "\n");
        //    strSqlString.Append("		)" + "\n");

        //    System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
        //    return strSqlString.ToString();
        //}

        private string LineChart(string Mindate)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("         SELECT MAX(OPER)" + "\n");
            strSqlString.AppendFormat("         FROM(" + "\n");
            strSqlString.AppendFormat("             SELECT (TRUNC(MOD((TO_DATE(TEMP_TIME,'YYYYMMDDHH24MISS')-TO_DATE('" + Mindate + "','YYYYMMDDHH24MISS')),1)*24)*60) +" + "\n");
            strSqlString.AppendFormat("                 (TRUNC(MOD((TO_DATE(TEMP_TIME,'YYYYMMDDHH24MISS')-TO_DATE('" + Mindate + "','YYYYMMDDHH24MISS'))*24,1)*60)) AS OPER" + "\n");
            strSqlString.AppendFormat("             FROM MRASLOTOVN@RPTTOMES" + "\n");
            strSqlString.AppendFormat("             WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                 AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("                 AND LOT_ID = (" + "\n");
            strSqlString.AppendFormat("                     SELECT MAX(LOT_ID) FROM MRASLOTOVN@RPTTOMES" + "\n");
            strSqlString.AppendFormat("                     WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                         AND TEMP_TIME ='" + Mindate + "'" + "\n");
            strSqlString.AppendFormat("                         AND RES_ID = '" + title1 + "'" + "\n");             // 2010-08-24-안시홍 : 속도와 정확성을 위해 추가.
            strSqlString.AppendFormat("                 )" + "\n");
            strSqlString.AppendFormat("                 AND RES_ID = '" + title1 + "'" + "\n");                     // 2010-08-24-안시홍 : 속도와 정확성을 위해 추가.
            strSqlString.AppendFormat("             )" + "\n");

            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            return strSqlString.ToString();
        }

        private void ChartShow(string Pattern, string Mindate, Object obj)
        {
            /************************************************
             * comment : OVEN CHART를 설정한다
             * 
             * created by :
             * 
             * modified by : Si-hong An (2010-08-27-금요일)
             ************************************************/
            udcChartFX CHART = (udcChartFX)obj;

            DataTable Line_dt = null;
            DataTable Data_dt = null;
//            DataTable Data_dt_Mark = null;

            Data_dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", DataChart(Pattern, Mindate));
//            Data_dt_Mark = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", DataChart_Mark(Pattern, Mindate));
            Line_dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", LineChart(Mindate));

            int columnCount = 0;
            double max1 = 0;
            double max_temp = 0;

            //int columnCount_Mark = 0;
            //double max2 = 0;
            //double max_temp_Mark = 0;

            // 차트 설정
            CHART.RPT_1_ChartInit();
            CHART.RPT_2_ClearData();
            CHART.RPT_3_OpenData(1, Data_dt.Rows.Count);
//            CHART.RPT_3_OpenData(1, Data_dt_Mark.Rows.Count);           // 2010-08-27-안시홍

            // contion attribute 를 이용한 0 point label hidden
            SoftwareFX.ChartFX.ConditionalAttributes contion = CHART.ConditionalAttributes[1];
            contion.Condition.From = 0;
            contion.Condition.To = 0;
            contion.PointLabels = false;

            int[] tat_columns = new Int32[Data_dt.Rows.Count];
            int[] columnsHeader = new Int32[Data_dt.Rows.Count];
            int[] dt_columns = new Int32[Data_dt.Rows.Count];

            //int[] tat_columns_Mark = new Int32[Data_dt_Mark.Rows.Count];
            //int[] columnsHeader_Mark = new Int32[Data_dt_Mark.Rows.Count];
            //int[] dt_columns_Mark = new Int32[Data_dt_Mark.Rows.Count];

            for (int i = 0; i < dt_columns.Length; i++)
            {
                tat_columns[i] = 0 + i;
                dt_columns[i] = 0 + i;
                columnsHeader[i] = 0 + i;
            }
/*
            for (int i = 0; i < dt_columns_Mark.Length; i++)
            {
                tat_columns_Mark[i] = 0 + i;
                dt_columns_Mark[i] = 0 + i;
                columnsHeader_Mark[i] = 0 + i;
            }

            max2 = CHART.RPT_4_AddData(Data_dt_Mark, tat_columns_Mark, new int[] { columnCount_Mark + 1 }, SeriseType.Column);
            max_temp_Mark = max2;
            //max2 = CHART.RPT_4_AddData(Data_dt_Mark, dt_columns_Mark, new int[] { columnCount + 1 }, SeriseType.Column);
            //if (max2 > max_temp_Mark)
            //{
            //    max_temp_Mark = max2;
            //}
            //max2 = max_temp_Mark;
*/
            max1 = CHART.RPT_4_AddData(Data_dt, tat_columns, new int[] { columnCount + 1 }, SeriseType.Column);
            max_temp = max1;
            max1 = CHART.RPT_4_AddData(Data_dt, dt_columns, new int[] { columnCount + 2 }, SeriseType.Column);
            if (max1 > max_temp)
            {
                max_temp = max1;
            }
            max1 = max_temp;

            CHART.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            CHART.RPT_6_SetGallery(ChartType.ThinLine, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            CHART.RPT_6_SetGallery(ChartType.ThinLine, 1, 1, "", AsixType.Y2, DataTypes.Initeger, max1 * 1.2);
            CHART.Series[0].Color = System.Drawing.Color.Red;
            CHART.Series[1].Color = System.Drawing.Color.Blue;
            CHART.Series[0].LineWidth = 2;                                              //2010-08-06-안시홍 : 굵은 그래프 선(RED)으로 변경
            CHART.Series[1].LineWidth = 2;                                              //2010-08-06-안시홍 : 굵은 그래프 선(BLUE)으로 변경

            //경과시간 세로Line 
            CHART.ConstantLines[0].Value = Convert.ToInt16(Line_dt.Rows[0][0].ToString());
            CHART.ConstantLines[0].Axis = SoftwareFX.ChartFX.AxisItem.X;
            CHART.ConstantLines[0].Width = 2;                                           //2010-08-09-안시홍 : 굵은 세로 그래프 선(GREEN)으로 변경.(설비팀 요청)
            CHART.ConstantLines[0].Font = new System.Drawing.Font("Tahoma", 15.25F);    //2010-08-10-안시홍 : 글자 굵게 변경.(설비팀 요청)
            CHART.ConstantLines[0].Text = Line_dt.Rows[0][0].ToString() + "분";
            CHART.ConstantLines[0].Color = System.Drawing.Color.Green;

            CHART.RPT_7_SetXAsixTitleUsingSpreadHeader(Data_dt);
            CHART.AxisY2.Gridlines = true;
            CHART.Highlight.PointAttributes.PointLabelColor = System.Drawing.Color.Transparent;

            /* 2010-08-10-안시홍 : 새로 추가한 부분 */
            /*****************************************************************************************/
            udcChartFX1.AxisX.Title.Text = "Minutes";
            udcChartFX1.AxisY.Title.Text = "Temperature (℃)";
            udcChartFX1.AxisX.Font = new System.Drawing.Font("Tahoma", 9.25F);          // 가로축 글자 굵게 변경
            udcChartFX1.AxisY.Font = new System.Drawing.Font("Tahoma", 9.25F);          // 세로축 글자 굵게 변경
            udcChartFX1.AxisY2.Font = new System.Drawing.Font("Tahoma", 0.03F);         // 오른쪽 세로축

            udcChartFX1.AxisX.Step = 10;
            udcChartFX1.AxisY.Step = 10;
            udcChartFX1.AxisY2.Step = 5;

            // 2010-08-30-안시홍 : 30분 간격의 세로 구분선 추가
            udcChartFX1.AxisX.MinorGridlines = true;
            udcChartFX1.AxisX.MinorStep = 30;
            //udcChartFX1.AxisX.MinorGrid.Color = System.Drawing.Color.LightYellow;

            udcChartFX1.AxisX.Min = 0;
            udcChartFX1.AxisY.Min = 20;
            udcChartFX1.AxisY.Max = 210;
            udcChartFX1.AxisY2.Min = 20;
            udcChartFX1.AxisY2.Max = 210;

            // 범례 표시
            udcChartFX1.SerLeg[0] = "기준온도";
            udcChartFX1.SerLeg[1] = "현재온도";
            udcChartFX1.SerLegBox = true;
            udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
            /*****************************************************************************************/
        }

        private void Status_Grade(DataTable Status_dt)
        {
            if (Status_dt.Rows.Count == 0)
            {
                Status_dt.Dispose();
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                return;
            }

            udcFarPointStatus.DataSource = Status_dt;

            udcFarPointStatus.isShowZero = true;

            udcFarPointStatus.RPT_AutoFit(false);
        }


        private void LotList_Grade(DataTable LotList_dt)
        {
            if (LotList_dt.Rows.Count == 0)
            {
                LotList_dt.Dispose();
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                return;
            }

            udcFarPointList.DataSource = LotList_dt;

            udcFarPointList.isShowZero = true;

            udcFarPointList.RPT_AutoFit(false);
        }

        #region Comments
        ///////////////////////////////////////////////////////////////////////////////
        //
        // * Function ID:    OvenListChart
        //
        // * Description
        //    
        // * Special Logic
        //         
        // * Modification History
        //   ----------  --------------  ----------------------------------------------
        //   [DATE    ]  [AUTHOR      ]  [DESCRIPTION                                 ]
        //   ----------  --------------  ----------------------------------------------
        //   2009/11/17  JJH             Initialize
        //
        ///////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Oven Chart들을 보여줍니다.
        /// </summary> 

        /* 원래 코드
        private void OvenListChart(string OvenRes)
        {
            DataTable Status_dt = null;

            Status_dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", StatusSqlString(OvenRes));

            Status_Grade(Status_dt);

            // 초기화
            label3.Text = ""; label3.Hide();
            label4.Text = ""; label4.Hide();
            label5.Text = ""; label5.Hide();
            label6.Text = ""; label6.Hide();

            udcChartFX1.Hide();
            udcChartFX2.Hide();
            udcChartFX3.Hide();
            udcChartFX4.Hide();

            // 루프돌며 컨트롤 Showing
            for (int cnt_0 = 1, cnt_3 = 3, i = s_selected_cell_col; i < Math.Min(s_selected_cell_col + 4, Status_dt.Rows.Count); i++, cnt_0++, cnt_3++)
            {
                if (Status_dt.Rows[i][2].ToString().Equals("PROC"))
                {
                    if (!Status_dt.Rows[i][7].ToString().Equals(""))
                    {
                        Control[] lbls = this.Controls.Find(string.Format("label{0}", cnt_3), true);
                        lbls[0].Text = "오븐번호:" + Status_dt.Rows[i][0].ToString();
                        Control[] charts = this.Controls.Find(string.Format("udcChartFX{0}", cnt_0), true);
                        ChartShow(Status_dt.Rows[i][1].ToString(), Status_dt.Rows[i][7].ToString(), (udcChartFX)charts[0]);

                        switch (lbls[0].Name.ToString())
                        {
                            case "label3":
                                title1 = Status_dt.Rows[i][0].ToString();
                                Pattern1 = Status_dt.Rows[i][1].ToString();
                                Mindate1 = Status_dt.Rows[i][7].ToString();
                                break;

                            case "label4":
                                title2 = Status_dt.Rows[i][0].ToString();
                                Pattern2 = Status_dt.Rows[i][1].ToString();
                                Mindate2 = Status_dt.Rows[i][7].ToString();
                                break;

                            case "label5":
                                title3 = Status_dt.Rows[i][0].ToString();
                                Pattern3 = Status_dt.Rows[i][1].ToString();
                                Mindate3 = Status_dt.Rows[i][7].ToString();
                                break;

                            case "label6":
                                title4 = Status_dt.Rows[i][0].ToString();
                                Pattern4 = Status_dt.Rows[i][1].ToString();
                                Mindate4 = Status_dt.Rows[i][7].ToString();
                                break;
                        }

                        lbls[0].Show();
                        charts[0].Show();
                    }
                }
            }

            return;
        }
        */
        #endregion

        private void OvenListChart(string OvenRes)
        {
            /************************************************
             * comment : 공정을 선택하면 OVEN CHART를 보여준다.
             * 
             * created by :
             * 
             * modified by : Si-hong An (2010-08-24-화요일)
             ************************************************/
            DataTable Status_dt = null;

            Status_dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", StatusSqlString(OvenRes));

            Status_Grade(Status_dt);

            for (int i = s_selected_cell_col; i < Math.Min(s_selected_cell_col + 1, Status_dt.Rows.Count); i++)
            {
                if (Status_dt.Rows[i][2].ToString().Equals("PROC"))
                {
                    if (!Status_dt.Rows[i][7].ToString().Equals(""))
                    {
                        Control[] lbls = this.Controls.Find(string.Format("label3", 3), true);
                        lbls[0].Text = "오븐번호:" + Status_dt.Rows[i][0].ToString();
                        Control[] charts = this.Controls.Find(string.Format("udcChartFX1", 0), true);
                        //ChartShow(Status_dt.Rows[i][1].ToString(), Status_dt.Rows[i][7].ToString(), (udcChartFX)charts[0]);

                        title1 = Status_dt.Rows[i][0].ToString();
                        Pattern1 = Status_dt.Rows[i][1].ToString();
                        Mindate1 = Status_dt.Rows[i][7].ToString();

                        // 2010-08-24-안시홍 : RES_ID를 가져오기 위해 실행 순서 교체
                        ChartShow(Status_dt.Rows[i][1].ToString(), Status_dt.Rows[i][7].ToString(), (udcChartFX)charts[0]);

                        lbls[0].Show();
                        charts[0].Show();
                    }
                }
            }
            return;
        }

        #endregion

        #region Event Handler

        private void PQC0000_Load(object sender, EventArgs e)
        {
            DataTable OvenRes_dt = null;
            DataTable LotList_dt = null;

            OvenRes_dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", OvenResSqlString());

            //오븐 공정
            for (int i = 0; i < OvenRes_dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    //radioButton1.Visible = true;
                    radioButton1.Text = OvenRes_dt.Rows[i][0].ToString();
                }
                else if (i == 1)
                {
                    //radioButton2.Visible = true;
                    radioButton2.Text = OvenRes_dt.Rows[i][0].ToString();
                }
                else if (i == 2)
                {
                    //radioButton3.Visible = true;
                    radioButton3.Text = OvenRes_dt.Rows[i][0].ToString();
                }
            }
            // 임시 기준정보가 잘못 입력 되있어 'CURE OVEN'을 Check
            // 2010-08-24-안시홍 : OVEN 설비만 체크하므로 주석처리.
            //if (radioButton1.Text.Equals("BACK LAP"))
            //{
            //    radioButton1.Checked = true;
            //    s_oven_res = radioButton1.Text;
            //}
            //else if (radioButton2.Text.Equals("CURE OVEN"))
            //{
            //    radioButton2.Checked = true;
            //    s_oven_res = radioButton2.Text;
            //}
            //else if (radioButton3.Text.Equals("PLASMA"))
            //{
            //    radioButton3.Checked = true;
            //    s_oven_res = radioButton3.Text;
            //}

            // 2010-08-24-안시홍 : OVEN 설비만 보여주기 위해 s_oven_res 설정
            s_oven_res = "CURE OVEN";

            //오븐의 PROC 상태와 chart를 보여준다.

            OvenListChart(s_oven_res);

            string strresID = udcFarPointStatus.ActiveSheet.Cells[0, 0].Value.ToString();
            LotList_dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", LotListSqlString(strresID));
            LotList_Grade(LotList_dt);

            timer1.Interval = 1000; // 2010-08-07-안시홍 : 1초 간격으로 체크
            timer1.Enabled = true;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                if (timer1 != null)
                {
                    timer1.Stop();
                }

                this.OnCloseLayoutForm();
                this.Dispose();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("PQC031001.btnClose_Click()" + "\r\n" + ex.Message);
            }
        }

        private void udcFarPointStatus_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            DataTable LotList_dt = null;

            // 2010-08-09-안시홍 : 셀 배경색을 초기화 한다.(설비팀 요청)
            for (int i = 0; i <= udcFarPointStatus.ActiveSheet.RowCount - 1; i++)
            {
                for (int j = 0; j <= udcFarPointStatus.ActiveSheet.ColumnCount - 1; j++)
                {
                    udcFarPointStatus.ActiveSheet.Cells[i, j].BackColor = Color.Empty;
                }
            }

            if (e.Column == 0)
            {
                string strresID = udcFarPointStatus.ActiveSheet.Cells[e.Row, 0].Value.ToString();
                LotList_dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", LotListSqlString(strresID));
                if (LotList_dt.Rows.Count != 0)
                {
                    LotList_Grade(LotList_dt);
                    s_selected_cell_col = e.Row;

                    OvenListChart(s_oven_res);
                }
            }
            // 2010-08-09-안시홍 : 셀 선택하면 배경색 표시한다.(설비팀 요청)
            udcFarPointStatus.ActiveSheet.Cells[e.Row, e.Column].BackColor = Color.LawnGreen;
        }

        #region Comments
        ///////////////////////////////////////////////////////////////////////////////
        //
        // * Function ID:    timer1_Tick
        //
        // * Description
        //    
        // * Special Logic
        //         
        // * Modification History
        //   ----------  --------------  ----------------------------------------------
        //   [DATE    ]  [AUTHOR      ]  [DESCRIPTION                                 ]
        //   ----------  --------------  ----------------------------------------------
        //   2009/11/17  JJH             Initialize
        //
        ///////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Timer Event를 처리합니다.
        /// </summary> 

        /* 원래 코드
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                s_oven_res = radioButton1.Text;
            }
            else if (radioButton2.Checked)
            {
                s_oven_res = radioButton2.Text;
            }
            else if (radioButton3.Checked)
            {
                s_oven_res = radioButton3.Text;
            }

            OvenListChart(s_oven_res);
        }
        */
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            /************************************************
             * comment : Refresh Time을 설정한다.
             * 
             * created by : 
             * 
             * modified by : Si-hong An (2010-08-07-토요일)
             ************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (intTime > 0)
                {
                    intTime -= 1;
                }
                else
                {

                    //if (cboTimer.Text == "1분")
                    if (cboTimer.SelectedIndex == 0)
                    {
                        intTime = 60;
                    }
                    //else if (cboTimer.Text == "3분")
                    else if (cboTimer.SelectedIndex == 1)
                    {
                        intTime = 180;
                    }
                    //else if (cboTimer.Text == "5분")
                    else if (cboTimer.SelectedIndex == 2)
                    {
                        intTime = 300;
                    }
                    //else if (cboTimer.Text == "10분")
                    else if (cboTimer.SelectedIndex == 3)
                    {
                        intTime = 600;
                    }
                    else
                    {
                        intTime = 300;
                    }

                    OvenListChart(s_oven_res);

                }
                lblRefresh.Text = "Refresh 남은 시간 : " + intTime.ToString() + " 초";
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            /************************************************
             * comment : Refresh Time을 설정하고 초기화 한다.
             * 
             * created by : Si-hong An (2010-08-07-토요일)
             * 
             * modified by : Si-hong An (2010-08-23-월요일)
             ************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                //if (cboTimer.Text == "1분")
                if (cboTimer.SelectedIndex == 0)
                {
                    lblRefresh.Text = "Refresh time remaining: 60 seconds";
                    intTime = 60;
                }
                //else if (cboTimer.Text == "3분")
                else if (cboTimer.SelectedIndex == 1)
                {
                    lblRefresh.Text = "Refresh time remaining: 180 seconds";
                    intTime = 180;
                }
                //else if (cboTimer.Text == "5분")
                else if (cboTimer.SelectedIndex == 2)
                {
                    lblRefresh.Text = "Refresh time remaining: 300 seconds";
                    intTime = 300;
                }
                //else if (cboTimer.Text == "10분")
                else if (cboTimer.SelectedIndex == 3)
                {
                    lblRefresh.Text = "Refresh time remaining: 600 seconds";
                    intTime = 600;
                }
                else
                {
                    lblRefresh.Text = "Refresh time remaining: 300 seconds";
                    intTime = 300;
                }
                OvenListChart(s_oven_res);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        // Chart를 DoubleClick했을때 popup창을 띄운다.
        private void udcChartFX1_DoubleClick(object sender, SoftwareFX.ChartFX.MouseEventArgsX e)
        {

            System.Diagnostics.Debug.WriteLine("오븐번호:" + title1);
            System.Diagnostics.Debug.WriteLine("Pattern:" + Pattern1);
            System.Diagnostics.Debug.WriteLine("DATE:" + Mindate1);
            if (!Pattern1.Equals(""))
            {
                System.Windows.Forms.Form frm = new PQC031001_P1(title1, Pattern1, Mindate1, title1);
                frm.ShowDialog();

            }
            else
            {
                CmnFunction.ShowMsgBox("No Data Chart");
            }
        }

        #region Comments
        ///////////////////////////////////////////////////////////////////////////////
        //
        // * Function ID:    OvenListCheckedChanged
        //
        // * Description
        //    
        // * Special Logic
        //         
        // * Modification History
        //   ----------  --------------  ----------------------------------------------
        //   [DATE    ]  [AUTHOR      ]  [DESCRIPTION                                 ]
        //   ----------  --------------  ----------------------------------------------
        //   2009/11/17  JJH             Initialize
        //
        ///////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Radio button Click Event를 처리합니다.
        /// </summary> 
        #endregion
        private void RadioBtnClick(object sender, EventArgs e)
        {
            // sender의 텍스트를 담아준다.
            s_selected_cell_col = 0;
            s_oven_res = ((RadioButton)sender).Text;
            OvenListChart(s_oven_res);
        }

        #endregion

    }
}