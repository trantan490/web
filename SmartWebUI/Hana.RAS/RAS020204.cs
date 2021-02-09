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

using System.Windows.Forms.DataVisualization.Charting;

namespace Hana.Ras
{
    /// <summary>
    /// 클  래  스: RAS020204<br/>
    /// 클래스요약: Trend Code별<br/>
    /// 작  성  자: 미라콤 김민규<br/>
    /// 최초작성일: 2008-12-30<br/>
    /// 상세  설명: Trend Code별<br/>
    /// 변경  내용: <br/>
    /// 2015-06-30-임종우 : ChartFX -> MS Chart로 변경
    /// 2015-11-26-임종우 : 지정 설비 검색 기능 추가 (김보람K 요청)
    /// </summary>    

    public partial class RAS020204 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public RAS020204()
        {
            InitializeComponent();         
            SortInit();  
            GridColumnInit(); //해더 한줄짜리 샘플
            cdvFromTo.AutoBinding();
            udcMSChart1.RPT_1_ChartInit();  //차트 초기화.           
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

            if (cdvLoss.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("RAS001", GlobalVariable.gcLanguage));
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
            spdData.RPT_AddBasicColumn("Code", 0, 6, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("classification", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            
            spdData.RPT_AddDynamicColumn(cdvFromTo, 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);          
        
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Team in charge", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = A.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES_GRP_1), '-') AS TEAM", "RES_GRP_1", "TEAM", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Part", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = A.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES_GRP_2), '-') AS PART", "RES_GRP_2", "PART", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation", "RES_GRP_3", "RES_GRP_3", "RES_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Maker", "RES_GRP_5", "RES_GRP_5", "RES_GRP_5", false);            
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "RES_GRP_6", "RES_GRP_6", "RES_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment", "RES_ID", "RES_ID", "RES_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Code", "DOWN_NEW_STS_1", "DOWN_NEW_STS_1", "DOWN_NEW_STS_1", true);            
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

            strSqlString.AppendFormat("           SELECT {0} " + "\n", QueryCond1);
            strSqlString.Append("                , ' ' " + "\n");

            if (strDate == "WORK_MONTH")
            {
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                , SUM(DECODE(WORK_MONTH, '" + selectDate1[i] + "',   " + "\n");
                    strSqlString.Append("                                                   CASE WHEN UP_EVENT_ID = ' ' " + "\n");
                    strSqlString.Append("                                                        THEN ROUND((F_GET_DIFF_TIME('"+ strToDate +"', DOWN_TRAN_TIME, 'M')), 0) " + "\n");
                    strSqlString.Append("                                                        ELSE ROUND((F_GET_DIFF_TIME(UP_TRAN_TIME, DOWN_TRAN_TIME, 'M')), 0)                                                    " + "\n");
                    strSqlString.Append("                                                   END           " + "\n");
                    strSqlString.Append("                                                 , 0)) AS TIME"+ i + "\n");
                }

                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                , SUM(DECODE(WORK_MONTH, '" + selectDate1[i] + "', 1 , 0)) AS  COUNT"+ i + "\n");
                }                
            }
            else if(strDate == "WORK_WEEK")
            {
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                , SUM(DECODE(WORK_WEEK, '" + selectDate1[i] + "',   " + "\n");
                    strSqlString.Append("                                                   CASE WHEN UP_EVENT_ID = ' ' " + "\n");
                    strSqlString.Append("                                                        THEN ROUND((F_GET_DIFF_TIME('" + strToDate + "', DOWN_TRAN_TIME, 'M')), 0) " + "\n");
                    strSqlString.Append("                                                        ELSE ROUND(F_GET_DIFF_TIME(UP_TRAN_TIME, DOWN_TRAN_TIME, 'M')), 0)                                                    " + "\n");
                    strSqlString.Append("                                                   END           " + "\n");
                    strSqlString.Append("                                                 , 0)) AS TIME" + i + "\n");
                }

                for (int i = 0; i < Between;i++ )
                {
                    strSqlString.Append("                , SUM(DECODE(WORK_WEEK, '" + selectDate1[i] + "', 1 , 0)) AS  COUNT" + i + "\n");
                }
            }
            else if(strDate == "WORK_DATE")
            {
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                , SUM(DECODE(WORK_DATE, '" + selectDate1[i] + "',   " + "\n");
                    strSqlString.Append("                                                   CASE WHEN UP_EVENT_ID = ' ' " + "\n");
                    strSqlString.Append("                                                        THEN ROUND((F_GET_DIFF_TIME('" + strToDate + "', DOWN_TRAN_TIME, 'M')), 0) " + "\n");
                    strSqlString.Append("                                                        ELSE ROUND((F_GET_DIFF_TIME(UP_TRAN_TIME, DOWN_TRAN_TIME, 'M')), 0)                                                    " + "\n");
                    strSqlString.Append("                                                   END           " + "\n");
                    strSqlString.Append("                                                 , 0)) AS TIME" + i + "\n");
                }

                for (int i = 0; i < Between;i++ )
                {
                    strSqlString.Append("                , SUM(DECODE(WORK_DATE, '" + selectDate1[i] + "', 1 , 0)) AS  COUNT" + i + "\n");
                }
            }
            strSqlString.Append("             FROM ( " + "\n");
            strSqlString.Append("                  SELECT B.RES_GRP_1, B.RES_GRP_2, B.RES_GRP_3, B.RES_GRP_4, B.RES_GRP_5, B.RES_GRP_6, B.RES_GRP_7, B.RES_GRP_8 " + "\n");
            strSqlString.Append("                       , A.FACTORY, A.DOWN_NEW_STS_1, UP_EVENT_ID, UP_TRAN_TIME, A.RES_ID " + "\n");
            strSqlString.Append("                       , GET_WORK_DATE(A.DOWN_TRAN_TIME, 'M')AS WORK_MONTH, GET_WORK_DATE(A.DOWN_TRAN_TIME, 'W')AS WORK_WEEK, A.DOWN_TRAN_TIME " + "\n");
            strSqlString.Append("                       , GET_WORK_DATE(A.DOWN_TRAN_TIME, 'D')AS WORK_DATE " + "\n");
            strSqlString.Append("                    FROM CRASRESDWH A " + "\n");
            strSqlString.Append("                       , MRASRESDEF B " + "\n");
            strSqlString.Append("                   WHERE 1=1  " + "\n");
            strSqlString.Append("                     AND A.FACTORY  " + cdvFactory.SelectedValueToQueryString  + "\n");
            strSqlString.Append("                     AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                     AND A.RES_ID = B.RES_ID " + "\n");
            strSqlString.Append("                     AND A.DOWN_NEW_STS_1 LIKE '" + cdvLoss.txtValue + "%'" + "\n");
            strSqlString.Append("                     AND B.RES_GRP_3 IN ( SELECT KEY_1 FROM MGCMTBLDAT WHERE TABLE_NAME ='H_EQP_GRP' AND DATA_2 = 'Y' AND FACTORY " + cdvFactory.SelectedValueToQueryString + ")" + "\n");

            // 2015-11-26-임종우 : 지정설비 검색 조건 추가
            //if (cdvCare.Text == "Designated Equipment")
            if (cdvCare.SelectedIndex == 1)
            {
                strSqlString.AppendFormat("                     AND B.RES_ID IN (SELECT KEY_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_RES_CARE_LIST')  " + "\n");
            }
            //else if (cdvCare.Text == "Unspecified equipment")
            else if (cdvCare.SelectedIndex == 2)
            {
                strSqlString.AppendFormat("                     AND B.RES_ID NOT IN (SELECT KEY_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_RES_CARE_LIST')  " + "\n");
            }

            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("                         AND B.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("                         AND B.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("                         AND B.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("                         AND B.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("                         AND B.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("                         AND B.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("                         AND B.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);
            
            strSqlString.Append("                  ) A   " + "\n");
            strSqlString.Append("            WHERE 1=1 " + "\n");
            strSqlString.Append("              AND DOWN_TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "' " + "\n");
            strSqlString.AppendFormat("         GROUP BY {0}, FACTORY" + "\n", QueryCond2);
            strSqlString.AppendFormat("         ORDER BY {0}, FACTORY" + "\n", QueryCond3);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
            
            return strSqlString.ToString();
        }

        private string MakeSqlString1()
        {
            StringBuilder strSqlString = new StringBuilder();

            string strDate = string.Empty;
                      
            int Between = cdvFromTo.SubtractBetweenFromToDate + 1;  // From To 선택된 기간

            string[] selectDate1 = new string[cdvFromTo.SubtractBetweenFromToDate + 1];
            selectDate1 = cdvFromTo.getSelectDate();  // From To 선택된 기간의 모든 MONTH, WEEK, DATE를 가져옴

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            string strFromDate = cdvFromTo.ExactFromDate; // From 의 Tran_Time
            string strToDate = cdvFromTo.ExactToDate;     // To 의 Tran_Time


            //장비대수 구하기//
            StringBuilder strRes_Cnt = new StringBuilder();
            {
                strRes_Cnt.Append("SELECT COUNT(RES_ID) FROM MRASRESDEF WHERE DELETE_FLAG = ' ' ");
                strRes_Cnt.Append("    AND RES_GRP_3 IN ( SELECT KEY_1 FROM MGCMTBLDAT WHERE TABLE_NAME ='H_EQP_GRP' AND DATA_2 = 'Y' AND FACTORY " + cdvFactory.SelectedValueToQueryString + ")" + "\n");

                if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                    strRes_Cnt.AppendFormat("  AND RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

                if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                    strRes_Cnt.AppendFormat("  AND RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

                if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                    strRes_Cnt.AppendFormat("  AND RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

                if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                    strRes_Cnt.AppendFormat(" AND RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

                if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                    strRes_Cnt.AppendFormat("  AND RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

                if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    strRes_Cnt.AppendFormat("  AND RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

                if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                    strRes_Cnt.AppendFormat("  AND RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);
            }

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

            strSqlString.Append("       SELECT 'MTBF', 'MTBF', 'MTBF', 'MTBF', 'MTBF', 'MTBF', 'MTBF'  " + "\n");

            if (strDate == "WORK_MONTH")
            {
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("             ,SUM(CASE WHEN COUNT" + i + " = 0 \n");
                    strSqlString.Append("                       THEN 0  " + "\n");
                    strSqlString.Append("                       ELSE ROUND(DECODE(WORK_MONTH, '" + selectDate1[i] + "', TO_CHAR(LAST_DAY(TO_DATE('200811' , 'YYYYMM')), 'DD')*1440*(" + strRes_Cnt.ToString() + ") - TIME0/60, 0)/COUNT" + i + ", 3) END) MTBF" + i + "\n");
                }
            }
            else if (strDate == "WORK_WEEK")
            {
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("             ,SUM(CASE WHEN COUNT" + i + "= 0 \n");
                    strSqlString.Append("                       THEN 0  " + "\n");
                    strSqlString.Append("                       ELSE ROUND(DECODE(WORK_WEEK, '" + selectDate1[i] + "', (7*1440*(" + strRes_Cnt.ToString() + ") - TIME0)/60, 0)/COUNT" + i + ", 3) END) MTBF" + i + "\n");
                }
            }
            else if (strDate == "WORK_DATE")
            {
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("             ,SUM(CASE WHEN COUNT" + i + "= 0 \n");
                    strSqlString.Append("                       THEN 0  " + "\n");
                    strSqlString.Append("                       ELSE ROUND(DECODE(WORK_DATE, '" + selectDate1[i] + "', (1440*(" + strRes_Cnt.ToString() + ") - TIME0)/60, 0)/COUNT" + i + ", 3) END) MTBF" + i + "\n");
                }
            }



            strSqlString.Append("         FROM (SELECT RES_GRP_1, RES_GRP_2, RES_GRP_3, RES_GRP_5, RES_GRP_6  " + "\n");
            strSqlString.Append("                    , FACTORY, DOWN_NEW_STS_1, " + strDate + "\n");
            if (strDate == "WORK_MONTH")
            {
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                , SUM(DECODE(WORK_MONTH, '" + selectDate1[i] + "',   " + "\n");
                    strSqlString.Append("                                                   CASE WHEN UP_EVENT_ID =' ' " + "\n");
                    strSqlString.Append("                                                        THEN ROUND((F_GET_DIFF_TIME('" + strToDate + "', DOWN_TRAN_TIME, 'M')), 0) " + "\n");
                    //strSqlString.Append("                                                        THEN ROUND((F_GET_DIFF_TIME(TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS'), DOWN_TRAN_TIME, 'M')), 0) " + "\n");
                    strSqlString.Append("                                                        ELSE ROUND((F_GET_DIFF_TIME(UP_TRAN_TIME, DOWN_TRAN_TIME, 'M')), 0)  " + "\n");
                    strSqlString.Append("                                                   END           " + "\n");
                    strSqlString.Append("                                                 , 0)) AS TIME" + i + "\n");
                }

                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                , SUM(DECODE(WORK_MONTH, '" + selectDate1[i] + "', 1 , 0)) AS  COUNT" + i + "\n");
                }
            }
            else if (strDate == "WORK_WEEK")
            {
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                , SUM(DECODE(WORK_WEEK, '" + selectDate1[i] + "',   " + "\n");
                    strSqlString.Append("                                                   CASE WHEN UP_EVENT_ID = ' '  " + "\n");
                    strSqlString.Append("                                                        THEN ROUND((F_GET_DIFF_TIME('" + strToDate + "', DOWN_TRAN_TIME, 'M')), 0) " + "\n");
                    strSqlString.Append("                                                        ELSE ROUND((F_GET_DIFF_TIME(UP_TRAN_TIME, DOWN_TRAN_TIME, 'M')), 0)   " + "\n");
                    strSqlString.Append("                                                   END           " + "\n");
                    strSqlString.Append("                                                 , 0)) AS TIME" + i + "\n");
                }

                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                , SUM(DECODE(WORK_WEEK, '" + selectDate1[i] + "', 1 , 0)) AS  COUNT" + i + "\n");
                }
            }
            else if (strDate == "WORK_DATE")
            {
                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                , SUM(DECODE(WORK_DATE, '" + selectDate1[i] + "',   " + "\n");
                    strSqlString.Append("                                                   CASE WHEN UP_EVENT_ID = ' ' " + "\n");
                    strSqlString.Append("                                                        THEN ROUND((F_GET_DIFF_TIME('" + strToDate + "', DOWN_TRAN_TIME, 'M')), 0) " + "\n");
                    strSqlString.Append("                                                        ELSE ROUND((F_GET_DIFF_TIME(UP_TRAN_TIME, DOWN_TRAN_TIME, 'M')), 0)                                                    " + "\n");
                    strSqlString.Append("                                                   END           " + "\n");
                    strSqlString.Append("                                                 , 0)) AS TIME" + i + "\n");
                }

                for (int i = 0; i < Between; i++)
                {
                    strSqlString.Append("                , SUM(DECODE(WORK_DATE, '" + selectDate1[i] + "', 1 , 0)) AS  COUNT" + i + "\n");
                }
            }
            strSqlString.Append("                 FROM (  " + "\n");
            strSqlString.Append("                      SELECT B.RES_GRP_1, B.RES_GRP_2, B.RES_GRP_3, B.RES_GRP_5, B.RES_GRP_6  " + "\n");
            strSqlString.Append("                           , A.FACTORY, UP_EVENT_ID, UP_TRAN_TIME, DOWN_NEW_STS_1  " + "\n");
            strSqlString.Append("                           , GET_WORK_DATE(A.DOWN_TRAN_TIME, 'M')AS WORK_MONTH, GET_WORK_DATE(A.DOWN_TRAN_TIME, 'W')AS WORK_WEEK, A.DOWN_TRAN_TIME  " + "\n");
            strSqlString.Append("                           , GET_WORK_DATE(A.DOWN_TRAN_TIME, 'D')AS WORK_DATE " + "\n");
            strSqlString.Append("                        FROM CRASRESDWH A  " + "\n");
            strSqlString.Append("                           , MRASRESDEF B  " + "\n");
            strSqlString.Append("                       WHERE 1=1   " + "\n");
            strSqlString.Append("                         AND A.FACTORY   = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                         AND A.FACTORY = B.FACTORY  " + "\n");
            strSqlString.Append("                         AND A.RES_ID = B.RES_ID  " + "\n");
            strSqlString.Append("                         AND A.DOWN_NEW_STS_1 LIKE '" + cdvLoss.txtValue + "%' " + "\n");
            strSqlString.Append("                         AND B.RES_GRP_3 IN ( SELECT KEY_1 FROM MGCMTBLDAT WHERE TABLE_NAME ='H_EQP_GRP' AND DATA_2 = 'Y' AND FACTORY " + cdvFactory.SelectedValueToQueryString + ")" + "\n");

            // 2015-11-26-임종우 : 지정설비 검색 조건 추가
            //if (cdvCare.Text == "Designated Equipment")
            if (cdvCare.SelectedIndex == 1)
            {
                strSqlString.AppendFormat("                         AND B.RES_ID IN (SELECT KEY_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_RES_CARE_LIST')  " + "\n");
            }
            //else if (cdvCare.Text == "Unspecified equipment")
            else if (cdvCare.SelectedIndex == 2)
            {
                strSqlString.AppendFormat("                         AND B.RES_ID NOT IN (SELECT KEY_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_RES_CARE_LIST')  " + "\n");
            }
            
            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("                         AND B.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("                         AND B.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("                         AND B.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("                         AND B.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("                         AND B.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("                         AND B.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("                         AND B.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

            strSqlString.Append("                      )     " + "\n");
            strSqlString.Append("                WHERE 1=1  " + "\n");
            strSqlString.Append("                  AND DOWN_TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  " + "\n");
            strSqlString.Append("             GROUP BY RES_GRP_1, RES_GRP_2, RES_GRP_3, RES_GRP_5, RES_GRP_6 " + "\n");
            strSqlString.Append("                    , FACTORY, DOWN_NEW_STS_1, " + strDate + "\n");
            strSqlString.Append("             ORDER BY RES_GRP_1, RES_GRP_2" + "\n");
            strSqlString.Append("              ) A           " + "\n");
            
            return strSqlString.ToString();
        }

        /// <summary>
        /// 5. Chart 생성
        /// </summary>
        /// <param name="rowCount">Row Number</param>
        private void ShowChart(int rowCount)
        {           
            udcMSChart1.RPT_1_ChartInit();
            udcMSChart1.RPT_2_ClearData();

            if (((DataTable)spdData.DataSource).Rows.Count == 0)
                return;
            
            udcMSChart1.RPT_3_OpenData(2, cdvFromTo.SubtractBetweenFromToDate+1);
            int[] TIME_Columns = new Int32[cdvFromTo.SubtractBetweenFromToDate + 1];
            int[] MTBF_Columns = new Int32[cdvFromTo.SubtractBetweenFromToDate + 1];
            int[] columnsHeader = new Int32[cdvFromTo.SubtractBetweenFromToDate + 1];

            for (int i = 0; i < TIME_Columns.Length; i++)
            {
                columnsHeader[i] = 8 + i;
                TIME_Columns[i] = 8 + i;
                MTBF_Columns[i] = 8 + i;
            }

            //int count = 0;
            //int sortCount = 0;
            
            //for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i = i + 9)
            //{
            //    count++;
            //}

            // 범례 칼럼 실제 위치 구하기
            //for (int i = 7; i >= 0; i--)
            //{
            //    if (spdData.ActiveSheet.Columns[i].Visible == true)
            //    {
            //        sortCount++;
            //    }
            //}
                        
            //int[] Rows1 = new int[count];
            //int[] Rows2 = new int[count];
            string[] exam = new string[] {"MTBF", "고장시간"};  

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

            //실 UPEH
            double MTBF = udcMSChart1.RPT_4_AddData(spdData, new int[] {0}, MTBF_Columns, SeriseType.Rows);
            double TIME = udcMSChart1.RPT_4_AddData(spdData, new int[] {1}, TIME_Columns, SeriseType.Rows);

            //MTBF
           // double Mtbf = udcMSChart1.RPT_4_AddData(spdData, new int[] { rowCount+3, rowCount+4, rowCount+5 }, Mtba_Columns, SeriseType.Rows);
            
            udcMSChart1.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            udcMSChart1.RPT_6_SetGallery(SeriesChartType.Line, 0, 1, "MTBF", AsixType.Y, DataTypes.Initeger, MTBF * 1.3);
            udcMSChart1.RPT_6_SetGallery(SeriesChartType.Line, 1, 1, "고장시간", AsixType.Y2, DataTypes.Initeger, TIME * 1.3);
            
            //각 Serise별로 동일한 타입을 사용할 경우
            //udcMSChart1.RPT_6_SetGallery(ChartType.Line, "[단위 : ???]", AsixType.Y, DataTypes.Initeger,  * 1.2);

            udcMSChart1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);

            udcMSChart1.RPT_8_SetSeriseLegend(exam, System.Windows.Forms.DataVisualization.Charting.Docking.Top);

            // 기타 설정
            //udcMSChart1.PointLabels = true;
            //udcMSChart1.Series[0].PointLabelColor = Color.Blue;                        
            //udcMSChart1.RightGap = 10;
            //udcMSChart1.AxisY.DataFormat.Decimals = 0;            
        }

      
        #endregion

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            DataTable dt2 = null;
            if (CheckField() == false) return;
            GridColumnInit();
            spdData_Sheet1.RowCount = 0;             

            try
            {
                this.Refresh();
                LoadingPopUp.LoadIngPopUpShow(this);

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());
                dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    udcMSChart1.RPT_1_ChartInit();
                    udcMSChart1.RPT_2_ClearData();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 7, 8, 8, 2, cdvFromTo.SubtractBetweenFromToDate + 1, btnSort);

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 7, 0, 2, true, Align.Center, VerticalAlign.Center);


                //4. Column Auto Fit4
                spdData.RPT_AutoFit(false);

                spdData.RPT_FillColumnData(7, new string[] { "time", "Number" });

                {// --- Spread 최상단에 MTBF 표시 ---
                    // 0번 행에 1개의 ROW를 추가함
                    spdData.ActiveSheet.AddRows(0, 1);
                    spdData.ActiveSheet.AddSpanCell(0, 0, 1, 8);                    

                    // Spread에 추가한 ROW에 DATA Insert 
                    for (int i = 0; i < dt2.Columns.Count; i++)
                    {
                        spdData.ActiveSheet.Cells[0, i].Text = dt2.Rows[0][i].ToString();
                    }
                    // 강제로 Frozen Row와 Column을 설정
                    spdData.ActiveSheet.FrozenRowCount = 3;
                    spdData.ActiveSheet.FrozenColumnCount = 8;
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
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ");
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
        //private void spdData_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        //{
        //    if (e.ColumnHeader)
        //        return;

        //    int i = 0;
        //    int j = 0;

        //    int get_row = 0;
        //    get_row = e.Row / 9;
                       
        //    j = e.Column;
        //    i = e.Row;
        //    ShowChart(get_row*9+1);              // 챠트 그리기            
        //}
        #endregion
    }
}