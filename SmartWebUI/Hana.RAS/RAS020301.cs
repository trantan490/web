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
    /// 클  래  스: RAS020301<br/>
    /// 클래스요약: Trend 순간정지<br/>
    /// 작  성  자: 미라콤 김민규<br/>
    /// 최초작성일: 2008-12-30<br/>
    /// 상세  설명: Trend 순간정지<br/>
    /// 변경  내용: <br/>
    /// 2010-03-15-임종우 : DPMO 삭제 함 (권순태C 요청), Excel Export시 Chart 출력 되도록 수정.
    /// 2010-03-18-임종우 : TOTAL 추가함.
    /// </summary>
    /// 

    public partial class RAS020301 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
      
        public RAS020301()
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
            spdData.RPT_AddBasicColumn("Part", 0, 1, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Operation", 0, 2, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Maker", 0, 3, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);           
            spdData.RPT_AddBasicColumn("Model", 0, 4, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Equipment", 0, 5, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("classification", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("TOTAL", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
            
            spdData.RPT_AddDynamicColumn(cdvFromTo, 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()        
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Team in charge", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = A.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES_GRP_1), '-') AS TEAM", "B.RES_GRP_1", "RES_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Part", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES_GRP_2), '-') AS PART", "B.RES_GRP_2", "RES_GRP_2", false);
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
            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            int Between = cdvFromTo.SubtractBetweenFromToDate+1;
            
            string[] selectDate1 = new string[cdvFromTo.SubtractBetweenFromToDate+1];
            selectDate1 = cdvFromTo.getSelectDate();

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

            strSqlString.AppendFormat("  SELECT {0}, ' ', ' '  " + "\n", QueryCond1);

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("       , SUM(ERR_CNT" + i + ") ERR_C" + i + "\n");
            }

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("       , SUM(MTBA"+ i +") MTBA" + i + "\n");
            }

            // 2010-03-15-임종우 : DPMO 삭제 함 (권순태C 요청)
            //for (int i = 0; i < Between; i++)
            //{
            //    strSqlString.Append("       , SUM(DPMO" + i + ") DPM" + i + "\n");
            //}

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("       , SUM(QTY" + i + ") QTY" + i + "\n");
            }

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("       , ROUND(SUM(RUN_TIME" + i + "),0) RUN_TIME" + i + "\n");
            }

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("       , SUM(RES_QTY" + i + ") RES_QTY" + i + "\n");
            }

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("       , SUM(RES_QTY_DAY" + i +") RES_QTY_DAY" + i + "\n");
            }
           
            ///////////////////////////////////////////////////////////////////////
            strSqlString.AppendFormat("    FROM (SELECT FACTORY, {0}   " + "\n", QueryCond3);

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("               , SUM(CNT" + i +") ERR_CNT" + i + "\n");
            }

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("               , CASE SUM(CNT"+i+") WHEN 0 THEN 0 ELSE ROUND(SUM(RUN_TIME"+i+")/SUM(CNT"+i+"),0) END MTBA" +i+ "\n");
            }

            // 2010-03-15-임종우 : DPMO 삭제 함 (권순태C 요청)
            //for (int i = 0; i < Between; i++)
            //{
            //    strSqlString.Append("               , CASE SUM(QTY0) WHEN 0 THEN 0 ELSE ROUND(SUM(CNT0) / SUM(QTY0) * 1000000 ,0) END DPMO" + i + "\n");
            //}

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("               , SUM(QTY"+i+") QTY" +i + "\n");
            }

            for (int i = 0; i < Between; i++)
            {
                strSqlString.Append("               , SUM(RUN_TIME"+i+") RUN_TIME" +i+ "\n");
            }

            
            if( strDate == "WORK_MONTH")
            {
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("               , CASE WORK_MONTH WHEN '"+selectDate1[i]+"' THEN COUNT(*) ELSE 0 END AS RES_QTY" + i + "\n");
                }

                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("               , CASE WORK_MONTH WHEN '" + selectDate1[i] + "' THEN ROUND(COUNT(*)/TO_CHAR(LAST_DAY(TO_DATE('" + selectDate1[i] + "' , 'YYYYMM')), 'DD'), 0) ELSE 0 END AS RES_QTY_DAY" + i + "\n");
                }            
            }
            else if( strDate == "WORK_WEEK")
            {
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("               , CASE WORK_WEEK WHEN '" + selectDate1[i] + "' THEN COUNT(*) ELSE 0 END AS RES_QTY" + i + "\n");
                }

                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("               , CASE WORK_WEEK WHEN '" + selectDate1[i] + "' THEN ROUND(COUNT(*)/7, 0) ELSE 0 END AS RES_QTY_DAY" + i + "\n");
                }  
            }   
            else if( strDate == "WORK_DATE")     
            {
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("               , CASE WORK_DATE WHEN '" + selectDate1[i] + "' THEN COUNT(*) ELSE 0 END AS RES_QTY" + i + "\n");
                }

                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("               , CASE WORK_DATE WHEN '" + selectDate1[i] + "' THEN ROUND(COUNT(*), 0) ELSE 0 END AS RES_QTY_DAY" + i + "\n");
                }  
            }
            
            strSqlString.Append("           FROM ( " + "\n");
            strSqlString.Append("                SELECT RES_GRP_1, RES_GRP_2, RES_GRP_3, RES_GRP_4, RES_GRP_5, RES_GRP_6, RES_GRP_7, RES_GRP_8  " + "\n");
            strSqlString.Append("                     , FACTORY, RES_ID  " + "\n");
            strSqlString.Append("                     , " + strDate+ "\n");

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
            }
            else if ( strDate == "WORK_WEEK")
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
            }
            else if( strDate == "WORK_DATE")
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
            }                    
            
            strSqlString.Append("                  FROM (         " + "\n");
            strSqlString.Append("                    SELECT RES_GRP_1, RES_GRP_2, RES_GRP_3, RES_GRP_4, RES_GRP_5, RES_GRP_6, RES_GRP_7, RES_GRP_8  " + "\n");
            strSqlString.Append("                         , A.RES_ID, A.FACTORY       " + "\n");
            strSqlString.Append("                         , A.CNT                  " + "\n");
            strSqlString.Append("                         , NVL(DWH.TIME_SUM, 0) TIME_SUM  " + "\n");
            strSqlString.Append("                         , NVL(QTY.QTY, 0) QTY                          " + "\n");
            strSqlString.Append("                         , A." + strDate  + "\n");
            strSqlString.Append("                      FROM (                     " + "\n");
            strSqlString.Append("                            SELECT RES.RES_GRP_1, RES.RES_GRP_2, RES.RES_GRP_3, RES.RES_GRP_4, RES.RES_GRP_5, RES.RES_GRP_6, RES.RES_GRP_7  " + "\n");
            strSqlString.Append("                                 , RES.RES_GRP_8, ALM.FACTORY, ALM.RES_ID                         " + "\n");
            strSqlString.Append("                                 , SUM(ALM.CNT) CNT  " + "\n");
            strSqlString.Append("                                 , ALM." + strDate + "\n");
            strSqlString.Append("                              FROM (    " + "\n");
            strSqlString.Append("                                    SELECT FACTORY, RES_ID, COUNT(*) CNT    " + "\n");

            if (strDate == "WORK_DATE")
            {                
                strSqlString.Append("                                         , GET_WORK_DATE(TRAN_TIME, 'D') AS WORK_DATE  " + "\n");
            }
            else if (strDate == "WORK_WEEK")
            {
                strSqlString.Append("                                         , GET_WORK_DATE(TRAN_TIME, 'W') AS WORK_WEEK   " + "\n");
            }
            else if (strDate == "WORK_MONTH")
            {
                strSqlString.Append("                                         , GET_WORK_DATE(TRAN_TIME, 'M') AS WORK_MONTH  " + "\n");
            }

            strSqlString.Append("                                      FROM CRASALMHIS   " + "\n");
            strSqlString.Append("                                     WHERE 1=1   " + "\n");
            strSqlString.Append("                                       AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'   " + "\n");
            strSqlString.Append("                                       AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                                       AND CLEAR_TIME > 0  " + "\n");

            if (strDate == "WORK_DATE")
            {
                strSqlString.Append("                                     GROUP BY FACTORY, RES_ID, GET_WORK_DATE(TRAN_TIME, 'D')   " + "\n");                
            }
            else if (strDate == "WORK_WEEK")
            {
                strSqlString.Append("                                     GROUP BY FACTORY, RES_ID, GET_WORK_DATE(TRAN_TIME, 'W')   " + "\n");                
            }
            else if (strDate == "WORK_MONTH")
            {
                strSqlString.Append("                                     GROUP BY FACTORY, RES_ID, GET_WORK_DATE(TRAN_TIME, 'M')   " + "\n");                
            }
                        
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

            strSqlString.Append("                             GROUP BY ALM.FACTORY, ALM.RES_ID, ALM." + strDate  + ", RES.RES_GRP_1, RES.RES_GRP_2  " + "\n");
            strSqlString.Append("                                    , RES.RES_GRP_3, RES.RES_GRP_4, RES.RES_GRP_5, RES.RES_GRP_6, RES.RES_GRP_7, RES.RES_GRP_8                                                                                   " + "\n");
            strSqlString.Append("                           ) A       " + "\n");
            strSqlString.Append("                         , (   " + "\n");
            strSqlString.Append("                            SELECT FACTORY, RES_ID, TIME_SUM, DOWN_DATE " + "\n");
            strSqlString.Append("                              FROM CSUMRESDWH   " + "\n");
            strSqlString.Append("                             WHERE 1=1  " + "\n");
            strSqlString.Append("                               AND DOWN_DATE BETWEEN '" + strFromDate + "' AND '" + strToDate + "'   " + "\n");
            strSqlString.Append("                               AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                             GROUP BY FACTORY, RES_ID, TIME_SUM, DOWN_DATE " + "\n");
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
            strSqlString.Append("                     GROUP BY A.RES_ID, A.CNT, DWH.TIME_SUM, QTY.QTY, A." + strDate  + ", A.FACTORY  " + "\n");
            strSqlString.Append("                            , RES_GRP_1, RES_GRP_2, RES_GRP_3, RES_GRP_4, RES_GRP_5, RES_GRP_6, RES_GRP_7, RES_GRP_8 " + "\n");
            strSqlString.Append("                    )  " + "\n");
            strSqlString.Append("                GROUP BY RES_GRP_1, RES_GRP_2, RES_GRP_3, RES_GRP_4, RES_GRP_5, RES_GRP_6, RES_GRP_7, RES_GRP_8 " + "\n");
            strSqlString.Append("                       , "+ strDate +", FACTORY, RES_ID " + "\n");
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
                columnsHeader[i] = 8 + i;
                Stop_Columns[i] = 8 + i;
                Mtba_Columns[i] = 8 + i;
            }

            int count = 0;
            int sortCount = 0;

            for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i = i + 8)
            {
                count++;
            }

            // 범례 칼럼 실제 위치 구하기
            for (int i = 8; i >= 0; i--)
            {
                if (spdData.ActiveSheet.Columns[i].Visible == true)
                {
                    sortCount++;
                }
            }

            int[] Rows1 = new int[count];
            int[] Rows2 = new int[count];
            string[] exam = new string[count * 2];

            for (int i = 0, j = 0; i < spdData.ActiveSheet.Rows.Count; i = i + 8)
            {
                Rows1[j] = i;
                exam[j] = spdData.ActiveSheet.Cells[i, sortCount-1].Text + " 건수";
                j++;
            }

            for (int i = 1, j = 0; i < spdData.ActiveSheet.Rows.Count; i = i + 8)
            {
                Rows2[j] = i;
                exam[j + count] = spdData.ActiveSheet.Cells[i, sortCount-1].Text + " MTBA";
                j++;
            }

            //순간정지 건수
            double Moment = udcChartFX1.RPT_4_AddData(spdData, Rows1, Stop_Columns, SeriseType.Rows);

            //MTBA
            double Mtba = udcChartFX1.RPT_4_AddData(spdData, Rows2, Mtba_Columns, SeriseType.Rows);

            udcChartFX1.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 0, count, "순간정지 건수", AsixType.Y, DataTypes.Initeger, Moment * 1.3);
            udcChartFX1.RPT_6_SetGallery(ChartType.Line, count, count, "MTBA", AsixType.Y2, DataTypes.Initeger, Mtba * 1.1);
            

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
        }

        #endregion

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            int sum = 0;
            
            if (CheckField() == false) return;
            GridColumnInit();
            spdData_Sheet1.RowCount = 0;

            try
            {
                this.Refresh();
                LoadingPopUp.LoadIngPopUpShow(this);

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
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 10, null, null, btnSort);
                //토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용


                //int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 0, 0, 8, 5, 1, btnSort);

                spdData.DataSource = dt;

                // 2010-03-15-임종우 : DPMO 삭제 함 (권순태C 요청)
                spdData.RPT_DivideRows(8, 6, cdvFromTo.SubtractBetweenFromToDate + 1);
                //spdData.RPT_DivideRows(7,  7, cdvFromTo.SubtractBetweenFromToDate+1);
                
                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                //3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 1, true, Align.Center, VerticalAlign.Center);


                //4. Column Auto Fit4
                spdData.RPT_AutoFit(false);

                // 2010-03-15-임종우 : DPMO 삭제 함 (권순태C 요청)
                spdData.RPT_FillColumnData(6, new string[] { "number of cases", "MTBA", "output", "Uptime", "the number of operations", "Number of days/equipment" });               

                //if (chkGroup.Checked)
                //    ShowChartByGroup(4, 0, 0, 15, 13, udcFromToDate.SubtractBetweenFromToDate + 1);
                //else

                string stValue = null;
                double dMtba = 0;
                double dQty = 0;

                //2010-03-18-임종우 : TOTAL Colum 값 구하기.
                for (int y = 0; y < spdData.ActiveSheet.Rows.Count; y++)
                {
                    for (int x = 8; x < spdData.ActiveSheet.Columns.Count; x++)
                    {
                        if (spdData.ActiveSheet.Cells[y, x].Text != "")
                        {
                            stValue = spdData.ActiveSheet.Cells[y, x].Text.Replace(",", ""); // 1,006 -> 1006 으로 변환
                            sum += Convert.ToInt32(stValue);
                        }

                        stValue = null;
                    }

                    spdData.ActiveSheet.Cells[y, 7].Text = sum.ToString();

                    sum = 0;
                }

                //2010-03-18-임종우 : TOTAL MTBA(총 가동시간/총 정지 건수), TOTAL 일/대당 건수(총 정지 건수/총 가동댓수) 구하기
                for (int y = 0; y < spdData.ActiveSheet.Rows.Count; y++)
                {
                    if (spdData.ActiveSheet.Cells[y, 6].Text.Equals("MTBA"))
                    {
                        dMtba = Convert.ToDouble(spdData.ActiveSheet.Cells[y + 2, 7].Text) / Convert.ToDouble(spdData.ActiveSheet.Cells[y - 1, 7].Text);
                        spdData.ActiveSheet.Cells[y,7].Text = dMtba.ToString("##.##");
                    }
                    else if (spdData.ActiveSheet.Cells[y, 6].Text.Equals("일/대당 건수"))
                    {
                        dQty = Convert.ToDouble(spdData.ActiveSheet.Cells[y -5, 7].Text) / Convert.ToDouble(spdData.ActiveSheet.Cells[y - 1, 7].Text);
                        spdData.ActiveSheet.Cells[y, 7].Text = dQty.ToString("##.##");
                    }

                    dMtba = 0;
                    dQty = 0;
                }

                for (int i = 0; i <= 7; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LemonChiffon;
                }

                ShowChart(0);
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
            //2010-03-15-임종우 : Excel Export 시 Chart 표시 되도록 수정.
            ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1, this.lblTitle.Text, " ^ ", " ^ ");
            //spdData.ExportExcel();            
        }

        #region 기타
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            cdvLoss.sFactory = cdvFactory.txtValue;
        }
        #endregion

    }
}