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


namespace Hana.Ras
{
    /// <summary>
    /// 클  래  스: RAS020303<br/>
    /// 클래스요약: Trend 순간정지 분석<br/>
    /// 작  성  자: 미라콤 김민규<br/>
    /// 최초작성일: 2008-12-30<br/>
    /// 상세  설명: Trend 순간정지 분석<br/>
    /// 변경  내용: <br/>
    /// </summary>
    /// 

    public partial class RAS020303 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
      
        public RAS020303()
        {
            InitializeComponent();         
            SortInit();  
            GridColumnInit(); //해더 한줄짜리 샘플
            cdvFromTo.AutoBinding();
            udcChartFX1.RPT_1_ChartInit();  //차트 초기화.           
            //cdvFromTo.DaySelector.SelectedIndex = 2;
            //cdvFromTo.DaySelector.Enabled = false;            
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
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;  //컬럼 완전 초기화
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("Team in charge", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Part", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Operation", 0, 2, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Maker", 0, 3, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);            
            spdData.RPT_AddBasicColumn("Model", 0, 4, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Equipment", 0, 5, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("classification", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            
            spdData.RPT_AddDynamicColumn(cdvFromTo, 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Team in charge", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = A.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES_GRP_1), '-') AS TEAM", "B.RES_GRP_1", "RES_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Part", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES_GRP_2), '-') AS PART", "B.RES_GRP_2", "RES_GRP_2", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation", "RES_GRP_3", "B.RES_GRP_3", "RES_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Maker", "RES_GRP_5", "B.RES_GRP_5", "RES_GRP_5", false);            
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "RES_GRP_6", "B.RES_GRP_6", "RES_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment", "RES_ID", "B.RES_ID", "RES_ID", false);            
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();
                        
            string strDate = string.Empty;

            int Between = cdvFromTo.SubtractBetweenFromToDate + 1;

            string[] selectDate1 = new string[cdvFromTo.SubtractBetweenFromToDate + 1];
            selectDate1 = cdvFromTo.getSelectDate();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            string strFromDate = cdvFromTo.ExactFromDate;
            string strToDate = cdvFromTo.ExactToDate;

            switch (cdvFromTo.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    strDate = "WORK_DATE";
                    break;
                case "WEEK":
                    strDate = "WORK_WEEK";
                    break;
                case "MONTH":
                    strDate = "WORK_MONTH";
                    break;
                default:
                    strDate = "WORK_DATE";
                    break;
            }

            strSqlString.AppendFormat("  SELECT {0}, ' '  " + "\n", QueryCond1);

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("       , SUM(ERR_CNT" + i + ") ERR_C" + i + "\n");
            }

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("       , SUM(BM" + i + ") BM" + i + "\n");
            }

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("       , SUM(ETC" + i + ") ETC" + i + "\n");
            }

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("       , SUM(DPMO" + i + ") DPMO" + i + "\n");
            }

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("       , SUM(MTBA" + i + ") MTBA" + i + "\n");
            }

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("       , SUM(BM_MTBF" + i + ") BM_MTBF" + i + "\n");
            }

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("       , SUM(ETC_MTBF" + i + ") ETC_MTBF" + i + "\n");
            }            

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("       , ROUND(SUM(RUN_TIME" + i + "),0) RUN_TIME" + i + "\n");
            }

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("       , SUM(QTY" + i + ") QTY" + i + "\n");
            }
           

            ///////////////////////////////////////////////////////////////////////
            strSqlString.AppendFormat("    FROM (SELECT FACTORY, {0}   " + "\n", QueryCond3);

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("               , SUM(CNT" + i + ") ERR_CNT" + i + "\n");
            }

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("               , CASE SUM(CNT" + i + ") WHEN 0 THEN 0 ELSE ROUND(SUM(RUN_TIME" + i + ")/SUM(CNT" + i + "),0) END MTBA" + i + "\n");
            }

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("               , CASE SUM(BM" + i + ") WHEN 0 THEN 0 ELSE ROUND(SUM(RUN_TIME" + i + ")/SUM(BM" + i + "),0) END BM_MTBF" + i + "\n");
            }

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("               , CASE SUM(ETC" + i + ") WHEN 0 THEN 0 ELSE ROUND(SUM(RUN_TIME" + i + ")/SUM(ETC" + i + "),0) END ETC_MTBF" + i + "\n");
            }



            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("               , CASE SUM(QTY0) WHEN 0 THEN 0 ELSE ROUND(SUM(CNT0) / SUM(QTY0) * 1000000 ,0) END DPMO" + i + "\n");
            }

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("               , SUM(QTY" + i + ") QTY" + i + "\n");
            }

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("               , SUM(RUN_TIME" + i + ") RUN_TIME" + i + "\n");
            }
            
            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("               , SUM(BM" + i + ") BM" + i + "\n");
            }
            
            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("               , SUM(ETC" + i + ") ETC" + i + "\n");
            }
                       

            strSqlString.Append("           FROM ( " + "\n");
            strSqlString.Append("                SELECT RES_GRP_1, RES_GRP_2, RES_GRP_3, RES_GRP_4, RES_GRP_5, RES_GRP_6, RES_GRP_7, RES_GRP_8  " + "\n");
            strSqlString.Append("                     , FACTORY                        " + "\n");
            strSqlString.Append("                     , " + strDate + "\n");

            if (strDate == "WORK_MONTH")
            {
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                     , SUM(DECODE(WORK_MONTH, '" + selectDate1[i] + "', CNT, 0)) AS CNT" + i + "\n");
                }
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                     , SUM(DECODE(WORK_MONTH, '" + selectDate1[i] + "', TO_CHAR(LAST_DAY(TO_DATE('" + selectDate1[i] + "' , 'YYYYMM')), 'DD')*1440 - TIME_SUM/60, 0)) AS RUN_TIME" + i + "\n");
                }
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                     , SUM(DECODE(WORK_MONTH, '" + selectDate1[i] + "', QTY, 0)) AS QTY" + i + "\n");
                }
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                     , SUM(DECODE(WORK_MONTH, '" + selectDate1[i] + "', BM, 0)) AS BM" + i + "\n");
                }
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                     , SUM(DECODE(WORK_MONTH, '" + selectDate1[i] + "', ETC, 0)) AS ETC" + i + "\n");
                }

            }
            else if (strDate == "WORK_WEEK")
            {
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                     , SUM(DECODE(WORK_WEEK, '" + selectDate1[i] + "', CNT, 0)) AS CNT" + i + "\n");
                }
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                     , SUM(DECODE(WORK_WEEK, '" + selectDate1[i] + "', 7*1440 - TIME_SUM/60, 0)) AS RUN_TIME" + i + "\n");
                }
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                     , SUM(DECODE(WORK_WEEK, '" + selectDate1[i] + "', QTY, 0)) AS QTY" + i + "\n");
                }
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                     , SUM(DECODE(WORK_WEEK, '" + selectDate1[i] + "', BM, 0)) AS BM" + i + "\n");
                }
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                     , SUM(DECODE(WORK_WEEK, '" + selectDate1[i] + "', ETC, 0)) AS ETC" + i + "\n");
                }

            }
            else if (strDate == "WORK_DATE")
            {
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + selectDate1[i] + "', CNT, 0)) AS CNT" + i + "\n");
                }
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + selectDate1[i] + "', 1440 - TIME_SUM/60, 0)) AS RUN_TIME" + i + "\n");
                }
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + selectDate1[i] + "', QTY, 0)) AS QTY" + i + "\n");
                }
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + selectDate1[i] + "', BM, 0)) AS BM" + i + "\n");
                }
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + selectDate1[i] + "', ETC, 0)) AS ETC" + i + "\n");
                }
            }

            strSqlString.Append("                  FROM (         " + "\n");
            strSqlString.Append("                    SELECT RES_GRP_1, RES_GRP_2, RES_GRP_3, RES_GRP_4, RES_GRP_5, RES_GRP_6, RES_GRP_7, RES_GRP_8  " + "\n");
            strSqlString.Append("                         , A.RES_ID, A.FACTORY       " + "\n");
            strSqlString.Append("                         , A.CNT                  " + "\n");
            strSqlString.Append("                         , NVL(DWH.TIME_SUM, 0) TIME_SUM  " + "\n");
            strSqlString.Append("                         , NVL(QTY.QTY, 0) QTY    " + "\n");
            strSqlString.Append("                         , DECODE(DOWN_EVENT_ID, 'BM', DOWN_COUNT, 0) BM     " + "\n");
            strSqlString.Append("                         , DECODE(DOWN_EVENT_ID, 'ETC_DOWN', DOWN_COUNT, 0) ETC " + "\n");
            strSqlString.Append("                         , A." + strDate + "\n");
            strSqlString.Append("                      FROM (                     " + "\n");
            strSqlString.Append("                            SELECT RES.RES_GRP_1, RES.RES_GRP_2, RES.RES_GRP_3, RES.RES_GRP_4, RES.RES_GRP_5, RES.RES_GRP_6, RES.RES_GRP_7  " + "\n");
            strSqlString.Append("                                 , RES.RES_GRP_8, ALM.FACTORY, ALM.RES_ID                         " + "\n");
            strSqlString.Append("                                 , SUM(ALM.CNT) CNT  " + "\n");
            strSqlString.Append("                                 , GET_WORK_DATE(ALM.TRAN_TIME, 'D') AS WORK_DATE  " + "\n");
            strSqlString.Append("                                 , GET_WORK_DATE(ALM.TRAN_TIME, 'W') AS WORK_WEEK   " + "\n");
            strSqlString.Append("                                 , GET_WORK_DATE(ALM.TRAN_TIME, 'M') AS WORK_MONTH  " + "\n");
            strSqlString.Append("                              FROM (    " + "\n");
            strSqlString.Append("                                    SELECT FACTORY, RES_ID, COUNT(*) CNT, TRAN_TIME    " + "\n");
            strSqlString.Append("                                      FROM CRASALMHIS   " + "\n");
            strSqlString.Append("                                     WHERE 1=1   " + "\n");
            strSqlString.Append("                                       AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'   " + "\n");
            strSqlString.Append("                                       AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                                       AND CLEAR_TIME > 0  " + "\n");
            strSqlString.Append("                                     GROUP BY FACTORY, RES_ID, TRAN_TIME   " + "\n");
            strSqlString.Append("                                   ) ALM   " + "\n");
            strSqlString.Append("                                   , MRASRESDEF RES   " + "\n");
            strSqlString.Append("                             WHERE 1=1    " + "\n");
            strSqlString.Append("                               AND ALM.FACTORY = RES.FACTORY    " + "\n");
            strSqlString.Append("                               AND ALM.RES_ID = RES.RES_ID    " + "\n");
            strSqlString.Append("                               AND RES.RES_TYPE NOT IN ('DUMMY') " + "\n");

            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("                               AND RES.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("                               AND RES.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("                               AND RES.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("                               AND RES.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("                               AND RES.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("                               AND RES.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("                               AND RES.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

            strSqlString.Append("                             GROUP BY ALM.FACTORY, ' ',  ' ',  ' ',  ' ',  ' ', ALM.RES_ID, ALM.TRAN_TIME, RES.RES_GRP_1, RES.RES_GRP_2  " + "\n");
            strSqlString.Append("                                    , RES.RES_GRP_3, RES.RES_GRP_4, RES.RES_GRP_5, RES.RES_GRP_6, RES.RES_GRP_7, RES.RES_GRP_8                                                                                   " + "\n");
            strSqlString.Append("                           ) A       " + "\n");
            strSqlString.Append("                         , (   " + "\n");
            strSqlString.Append("                            SELECT FACTORY, RES_ID, TIME_SUM, DOWN_DATE, DOWN_EVENT_ID, DOWN_COUNT " + "\n");
            strSqlString.Append("                              FROM CSUMRESDWH   " + "\n");
            strSqlString.Append("                             WHERE 1=1  " + "\n");
            strSqlString.Append("                               AND DOWN_DATE BETWEEN '" + strFromDate + "' AND '" + strToDate + "'   " + "\n");
            strSqlString.Append("                               AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                             GROUP BY FACTORY, RES_ID, TIME_SUM, DOWN_DATE, DOWN_EVENT_ID, DOWN_COUNT " + "\n");
            strSqlString.Append("                           ) DWH   " + "\n");
            strSqlString.Append("                         , (  " + "\n");
            strSqlString.Append("                            SELECT FACTORY, RES_ID, END_QTY_1 as QTY, WORK_DATE  " + "\n");
            strSqlString.Append("                              FROM RSUMRESMOV  " + "\n");
            strSqlString.Append("                             WHERE 1=1  " + "\n");
            strSqlString.Append("                               AND WORK_DATE BETWEEN '" + strFromDate + "' AND '" + strToDate + "'   " + "\n");
            strSqlString.Append("                               AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                               AND MAT_VER = 1  " + "\n");
            strSqlString.Append("                             GROUP BY FACTORY, RES_ID, END_QTY_1, WORK_DATE  " + "\n");
            strSqlString.Append("                           ) QTY                                  " + "\n");
            strSqlString.Append("                     WHERE 1=1      " + "\n");
            strSqlString.Append("                       AND A.FACTORY = DWH.FACTORY(+)   " + "\n");
            strSqlString.Append("                       AND A.RES_ID = DWH.RES_ID(+)   " + "\n");
            strSqlString.Append("                       AND A.FACTORY = QTY.FACTORY(+)  " + "\n");
            strSqlString.Append("                       AND A.RES_ID = QTY.RES_ID(+)     " + "\n");
            strSqlString.Append("                       AND A.WORK_DATE = QTY.WORK_DATE(+) " + "\n");
            strSqlString.Append("                       AND A.WORK_DATE = DWH.DOWN_DATE(+)                     " + "\n");
            strSqlString.Append("                     GROUP BY A.RES_ID, A.CNT, DWH.TIME_SUM, QTY.QTY, A.WORK_DATE, A.WORK_WEEK, A.WORK_MONTH, A.FACTORY, DOWN_EVENT_ID, DOWN_COUNT  " + "\n");
            strSqlString.Append("                            , RES_GRP_1, RES_GRP_2, RES_GRP_3, RES_GRP_4, RES_GRP_5, RES_GRP_6, RES_GRP_7, RES_GRP_8 " + "\n");
            strSqlString.Append("                    )  " + "\n");
            strSqlString.Append("                GROUP BY RES_GRP_1, RES_GRP_2, RES_GRP_3, RES_GRP_4, RES_GRP_5, RES_GRP_6, RES_GRP_7, RES_GRP_8 " + "\n");
            strSqlString.Append("                       , " + strDate + ", FACTORY " + "\n");
            strSqlString.Append("                )   " + "\n");
            strSqlString.AppendFormat("             GROUP BY {0}, FACTORY, " + strDate + "\n", QueryCond3);
            strSqlString.Append("              ) A " + "\n");
            strSqlString.AppendFormat("          GROUP BY FACTORY, {0} " + "\n", QueryCond3);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }        

        /// <summary>
        /// 5. Chart 생성
        /// </summary>
        /// <param name="rowCount">Row Number</param>
        private void ShowChart(int rowCount)
        {           
            udcChartFX1.RPT_1_ChartInit();
            udcChartFX1.RPT_2_ClearData();

            if (((DataTable)spdData.DataSource).Rows.Count == 0)
                return;
            
            udcChartFX1.RPT_3_OpenData(2, cdvFromTo.SubtractBetweenFromToDate + 1);
            int[] Stop_Columns = new Int32[cdvFromTo.SubtractBetweenFromToDate + 1];
            int[] Mtba_Columns = new Int32[cdvFromTo.SubtractBetweenFromToDate + 1];
            int[] columnsHeader = new Int32[cdvFromTo.SubtractBetweenFromToDate + 1];

            for (int i = 0; i < Stop_Columns.Length; i++)
            {
                columnsHeader[i] = 7 + i;
                Stop_Columns[i] = 7 + i;
                Mtba_Columns[i] = 7 + i;
            }

            //int count = 0;
            int sortCount = 0;
            
            //for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i = i + 9)
            //{
            //    count++;
            //}

            // 범례 칼럼 실제 위치 구하기
            for (int i = 7; i >= 0; i--)
            {
                if (spdData.ActiveSheet.Columns[i].Visible == true)
                {
                    sortCount++;
                }
            }
                        
            //int[] Rows1 = new int[count];
            //int[] Rows2 = new int[count];
            string[] exam = new string[] {"가동시간", "MTBA", "설비고장 MTBF", "품질고장 MTBF"};  

            //for(int i=0,j=0; i< spdData.ActiveSheet.Rows.Count;i=i+9)
            //{
            //    Rows1[j] = i;
            //    exam[j] = spdData.ActiveSheet.Cells[i,sortCount-1].Text ;
            //    j++;
            //}
            
            //for (int i=1,j=0; i< spdData.ActiveSheet.Rows.Count; i=i+9)
            //{
            //    Rows2[j] = i;               
            //    exam[j + count] = spdData.ActiveSheet.Cells[i, sortCount-1].Text ;                
            //    j++;
            //}

            //가동시간
            double Moment = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 6 }, Stop_Columns, SeriseType.Rows);

            //MTBF
            double Mtbf = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount+3, rowCount+4, rowCount+5 }, Mtba_Columns, SeriseType.Rows);
            
            udcChartFX1.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 0, 1, "가동시간", AsixType.Y, DataTypes.Initeger, Moment * 1.3);
            udcChartFX1.RPT_6_SetGallery(ChartType.Line, 1, 4, "MTBF", AsixType.Y2, DataTypes.Initeger, Mtbf * 1.2);
            
            

            //각 Serise별로 동일한 타입을 사용할 경우
            //udcChartFX1.RPT_6_SetGallery(ChartType.Bar, "[단위 : sls]", AsixType.Y, DataTypes.Initeger, yield * 1.2);

            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            
            udcChartFX1.RPT_8_SetSeriseLegend(exam, SoftwareFX.ChartFX.Docked.Top);

            // 기타 설정
            udcChartFX1.PointLabels = true;
            udcChartFX1.Series[1].PointLabelColor = Color.Red;
            udcChartFX1.Series[0].PointLabelColor = Color.Blue;            
            udcChartFX1.Series[0].LineWidth = 2;                        
            udcChartFX1.RightGap = 10;
            udcChartFX1.AxisY.DataFormat.Decimals = 0;
            udcChartFX1.Titles[0].Text = spdData.ActiveSheet.Cells[rowCount, sortCount - 1].Text;
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
                this.Refresh();
                //LoadingPopUp.LoadIngPopUpShow(this);

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    udcChartFX1.RPT_1_ChartInit();
                    udcChartFX1.RPT_2_ClearData();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));                    
                    return;
                }

                //by John
                //1.Griid 합계 표시
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 10, null, null, btnSort);
                //토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용


                int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 0, 7, 7, 9, cdvFromTo.SubtractBetweenFromToDate+1, btnSort);

                //spdData.DataSource = dt;
                //spdData.RPT_DivideRows(9,  7, cdvFromTo.SubtractBetweenFromToDate+1);

                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 6, 0, 9, true, Align.Center, VerticalAlign.Center);


                //4. Column Auto Fit4
                spdData.RPT_AutoFit(false);

                spdData.RPT_FillColumnData(6, new string[] { "Number of momentary stops", "Number of equipment failures", "Number of quality failures", "DPMO", "MTBA", "Average equipment failure interval", "Quality average failure interval", "Uptime", "output" });

                ShowChart(1);                
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
           // ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ");
            spdData.ExportExcel();           
        }

        #region 기타
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            cdvLoss.sFactory = cdvFactory.txtValue;    
        }
        #endregion

        #region Cell을 더블클릭 했을 경우의 이벤트 처리
        /// <summary>
        /// Double Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spdData_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;

            int i = 0;
            int j = 0;

            int get_row = 0;
            get_row = e.Row / 9;
                       
            j = e.Column;
            i = e.Row;
            ShowChart(get_row*9+1);              // 챠트 그리기            
        }
        #endregion
    }
}