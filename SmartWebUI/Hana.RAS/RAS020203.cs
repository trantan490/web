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
    /// 클  래  스: RAS020203<br/>
    /// 클래스요약: Trend 가동률 상세<br/>
    /// 작  성  자: 미라콤 양형석<br/>
    /// 최초작성일: 2009-01-19<br/>
    /// 상세  설명: Trend 가동률 상세<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class RAS020203 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public RAS020203()
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


            spdData.RPT_AddBasicColumn("Team in charge", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Part", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("EQP Type", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Maker", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Model", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Equipment name", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("classification", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Item", 0, 7, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 80);

            spdData.RPT_AddBasicColumn("CUM", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

            spdData.RPT_AddDynamicColumn(udcFromToDate, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
           
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Team in charge", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_1), '-') AS TEAM", "AND RES_GRP_1 = RES.RES_GRP_1 ", "RES.RES_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Part", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_2), '-') AS PART", "AND RES_GRP_2 = RES.RES_GRP_2 ", "RES.RES_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("EQP Type", "RES.RES_GRP_3 AS EQP_TYPE", "AND RES_GRP_3 = RES.RES_GRP_3 ", "RES.RES_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Maker", "RES.RES_GRP_5 AS MAKER", "AND RES_GRP_5 = RES.RES_GRP_5 ", "RES.RES_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "RES.RES_GRP_6 AS MODEL", "AND RES_GRP_6 = RES.RES_GRP_6 ", "RES.RES_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment name", "RES.RES_ID AS RES", "AND RES_ID = RES.RES_ID ", "RES.RES_ID", false);
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

            string strRatioTotalPerformance = string.Empty;
            string strRatioPerformance = string.Empty;
            string strRatioTime = string.Empty;
            string strRatioBuha = string.Empty;
            string strTimeJoup = string.Empty;
            string strTimeValueRun = string.Empty;
            string strTimeRun = string.Empty;
            string strTimeLoss = string.Empty;
            string strQty = string.Empty;
            string strCntBoyuRes = string.Empty;
            string strCntRunnedRes = string.Empty;
            string strCntIdleRes = string.Empty;

            string strRatioBm = string.Empty;
            string strRatioRunDown = string.Empty;
            string strRatioPm = string.Empty;
            string strRatioDevChg = string.Empty;
            string strRatioEngDown = string.Empty;
            string strRatioSuperDown = string.Empty;
            string strRatioUtilityDown = string.Empty;
            string strRatioETC = string.Empty;

            string strTimeBm = string.Empty;
            string strTimeRunDown = string.Empty;
            string strTimePm = string.Empty;
            string strTimeDevChg = string.Empty;
            string strTimeEngDown = string.Empty;
            string strTimeSuperDown = string.Empty;
            string strTimeUtilityDown = string.Empty;
            string strTimeETC = string.Empty;

            string strCntBm = string.Empty;
            string strCntRunDown = string.Empty;
            string strCntPm = string.Empty;
            string strCntDevChg = string.Empty;
            string strCntEngDown = string.Empty;
            string strCntSuperDown = string.Empty;
            string strCntUtilityDown = string.Empty;
            string strCntETC = string.Empty;

            string strSqlBoyuResCnt = string.Empty;

            string strFromDate = string.Empty;
            string strToDate = string.Empty;

            #region " UdcDurationPlus 각 단계의 값 가져오기 "

            string[] arrStep = udcFromToDate.getSelectDate();

            #endregion

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQuery;
            QueryCond3 = tableForm.SelectedValue3ToQuery;


            #region " udcDurationPlus에서 정확한 조회시간을 받아오기 : strFromDate, strToDate "
            strFromDate = udcFromToDate.ExactFromDate;
            strToDate = udcFromToDate.ExactToDate;
            #endregion

            #region " DECODE 쿼리 스트링 구하기 "

            #region " CUM "
            int nStep = udcFromToDate.SubtractBetweenFromToDate + 1;

            strRatioTotalPerformance += "     , TRUNC((DECODE( AVG(UPEH), 0, 0, SUM(QTY)/AVG(UPEH)*60 / (SUM(JOUP_TIME) - SUM(LOSS_TIME)/60)) * ((SUM(JOUP_TIME) - SUM(LOSS_TIME)/60) / SUM(JOUP_TIME)))*100,2) RES_PERFORMANCE_CUM" + "\n";
            strRatioPerformance += "     , TRUNC(DECODE( AVG(UPEH), 0, 0, SUM(QTY)/AVG(UPEH)*60 / (SUM(JOUP_TIME) - SUM(LOSS_TIME)/60)*100),2) RATIO_PERFORMANCE_CUM" + "\n";
            strRatioTime += "     , TRUNC((SUM(JOUP_TIME) - SUM(LOSS_TIME)/60) / SUM(JOUP_TIME) * 100, 2) RATIO_TIME_RUN_CUM" + "\n";
            strRatioBuha += "     , TRUNC((SUM(JOUP_TIME) - SUM(CASE WHEN DOWN_EVENT_ID='RUN_DOWN' THEN LOSS_TIME ELSE 0 END)/60) / SUM(JOUP_TIME)*100, 2) RATIO_BUHA_CUM" + "\n";

            strTimeJoup += "     , TRUNC(SUM(JOUP_TIME/" + nStep + ")) JOUP_TIME_CUM" + "\n";
            strTimeValueRun += "     , TRUNC(DECODE(AVG(UPEH), 0, 0, SUM(QTY)/AVG(UPEH)*60/" + nStep + ")) VALUE_RUN_TIME_CUM" + "\n";
            strTimeRun += "     , TRUNC((SUM(JOUP_TIME) - SUM(LOSS_TIME)/60)/" + nStep + ") RUN_TIME_CUM" + "\n";
            strTimeLoss += "     , TRUNC(SUM(LOSS_TIME)/60/" + nStep + ") LOSS_TIME_CUM" + "\n";
            strQty += "     , TRUNC(SUM(QTY2)/" + nStep + ") QTY2_CUM" + "\n";

            strCntBoyuRes += "     , COUNT(DISTINCT RES_ID) BOYU_RES_CNT_CUM" + "\n";
            strCntRunnedRes += "     , COUNT(DISTINCT CASE WHEN QTY > 0 THEN RES_ID ELSE NULL END) RUNNED_RES_CNT_CUM" + "\n";
            strCntIdleRes += "     , COUNT(DISTINCT CASE WHEN QTY < 1 THEN RES_ID ELSE NULL END) IDLE_RES_CNT_CUM" + "\n";

            strRatioBm += "     , DECODE(SUM(LOSS_TIME)/60, SUM(JOUP_TIME), 0, TRUNC((SUM(CASE WHEN DOWN_EVENT_ID='BM' THEN LOSS_TIME ELSE 0 END)/60/(SUM(JOUP_TIME) - SUM(LOSS_TIME)/60))*100,2)) RATIO_BM_CUM" + "\n";
            strRatioRunDown += "     , TRUNC(SUM(CASE WHEN DOWN_EVENT_ID='RUN_DOWN' THEN LOSS_TIME ELSE 0 END)/60 / SUM(JOUP_TIME) * 100,2) RATIO_RUN_DOWN_CUM" + "\n";
            strRatioPm += "     , DECODE(SUM(LOSS_TIME)/60, SUM(JOUP_TIME), 0, TRUNC((SUM(CASE WHEN DOWN_EVENT_ID='PM' THEN LOSS_TIME ELSE 0 END)/60/(SUM(JOUP_TIME) - SUM(LOSS_TIME)/60))*100,2)) RATIO_PM_CUM" + "\n";
            strRatioDevChg += "     , DECODE(SUM(LOSS_TIME)/60, SUM(JOUP_TIME), 0, TRUNC((SUM(CASE WHEN DOWN_EVENT_ID='DEV_CHG' THEN LOSS_TIME ELSE 0 END)/60/(SUM(JOUP_TIME) - SUM(LOSS_TIME)/60))*100,2)) RATIO_DEV_CHG_CUM" + "\n";
            strRatioEngDown += "     , DECODE(SUM(LOSS_TIME)/60, SUM(JOUP_TIME), 0, TRUNC((SUM(CASE WHEN DOWN_EVENT_ID='ENG_DOWN' THEN LOSS_TIME ELSE 0 END)/60/(SUM(JOUP_TIME) - SUM(LOSS_TIME)/60))*100,2)) RATIO_ENG_DOWN_CUM" + "\n";
            strRatioSuperDown += "     , DECODE(SUM(LOSS_TIME)/60, SUM(JOUP_TIME), 0, TRUNC((SUM(CASE WHEN DOWN_EVENT_ID='SUPER_DOWN' THEN LOSS_TIME ELSE 0 END)/60/(SUM(JOUP_TIME) - SUM(LOSS_TIME)/60))*100,2)) RATIO_SUPER_DOWN_CUM" + "\n";
            strRatioUtilityDown += "     , DECODE(SUM(LOSS_TIME)/60, SUM(JOUP_TIME), 0, TRUNC((SUM(CASE WHEN DOWN_EVENT_ID='UTILITY_DOWN' THEN LOSS_TIME ELSE 0 END)/60/(SUM(JOUP_TIME) - SUM(LOSS_TIME)/60))*100,2)) RATIO_UTILITY_DOWN_CUM" + "\n";
            strRatioETC += "     , DECODE(SUM(LOSS_TIME)/60, SUM(JOUP_TIME), 0, TRUNC((SUM(CASE WHEN DOWN_EVENT_ID='ETC_DOWN' THEN LOSS_TIME ELSE 0 END)/60/(SUM(JOUP_TIME) - SUM(LOSS_TIME)/60))*100,2)) RATIO_ETC_DOWN_CUM" + "\n";

            strTimeBm += "     , TRUNC(SUM(CASE WHEN DOWN_EVENT_ID='BM' THEN LOSS_TIME ELSE 0 END)/60/" + nStep + ") TIME_BM_CUM" + "\n";
            strTimeRunDown += "     , TRUNC(SUM(CASE WHEN DOWN_EVENT_ID='RUN_DOWN' THEN LOSS_TIME ELSE 0 END)/60/" + nStep + ") TIME_RUN_DOWN_CUM" + "\n";
            strTimePm += "     , TRUNC(SUM(CASE WHEN DOWN_EVENT_ID='PM' THEN LOSS_TIME ELSE 0 END)/60/" + nStep + ") TIME_PM_CUM" + "\n";
            strTimeDevChg += "     , TRUNC(SUM(CASE WHEN DOWN_EVENT_ID='DEV_CHG' THEN LOSS_TIME ELSE 0 END)/60/" + nStep + ") TIME_DEV_CHG_CUM" + "\n";
            strTimeEngDown += "     , TRUNC(SUM(CASE WHEN DOWN_EVENT_ID='ENG_DOWN' THEN LOSS_TIME ELSE 0 END)/60/" + nStep + ") TIME_ENG_DOWN_CUM" + "\n";
            strTimeSuperDown += "     , TRUNC(SUM(CASE WHEN DOWN_EVENT_ID='SUPER_DOWN' THEN LOSS_TIME ELSE 0 END)/60/" + nStep + ") TIME_SUPER_DOWN_CUM" + "\n";
            strTimeUtilityDown += "     , TRUNC(SUM(CASE WHEN DOWN_EVENT_ID='UTILITY_DOWN' THEN LOSS_TIME ELSE 0 END)/60/" + nStep + ") TIME_UTILITY_DOWN_CUM" + "\n";
            strTimeETC += "     , TRUNC(SUM(CASE WHEN DOWN_EVENT_ID='ETC_DOWN' THEN LOSS_TIME ELSE 0 END)/60/" + nStep + ") TIME_ETC_CUM" + "\n";

            strCntBm += "     , TRUNC(SUM(CASE WHEN DOWN_EVENT_ID='BM' THEN DOWN_CNT ELSE 0 END)/" + nStep + ") CNT_BM_CUM" + "\n";
            strCntRunDown += "     , TRUNC(SUM(CASE WHEN DOWN_EVENT_ID='RUN_DOWN' THEN DOWN_CNT ELSE 0 END)/" + nStep + ") CNT_RUN_DOWN_CUM" + "\n";
            strCntPm += "     , TRUNC(SUM(CASE WHEN DOWN_EVENT_ID='PM' THEN DOWN_CNT ELSE 0 END)/" + nStep + ") CNT_PM_CUM" + "\n";
            strCntDevChg += "     , TRUNC(SUM(CASE WHEN DOWN_EVENT_ID='DEV_CHG' THEN DOWN_CNT ELSE 0 END)/" + nStep + ") CNT_DEV_CHG_CUM" + "\n";
            strCntEngDown += "     , TRUNC(SUM(CASE WHEN DOWN_EVENT_ID='ENG_DOWN' THEN DOWN_CNT ELSE 0 END)/" + nStep + ") CNT_ENG_DOWN_CUM" + "\n";
            strCntSuperDown += "     , TRUNC(SUM(CASE WHEN DOWN_EVENT_ID='SUPER_DOWN' THEN DOWN_CNT ELSE 0 END)/" + nStep + ") CNT_SUPER_DOWN_CUM" + "\n";
            strCntUtilityDown += "     , TRUNC(SUM(CASE WHEN DOWN_EVENT_ID='UTILITY_DOWN' THEN DOWN_CNT ELSE 0 END)/" + nStep + ") CNT_UTILITY_DOWN_CUM" + "\n";
            strCntETC += "     , TRUNC(SUM(CASE WHEN DOWN_EVENT_ID='ETC_DOWN' THEN DOWN_CNT ELSE 0 END)/" + nStep + ") CNT_ETC_CUM" + "\n";

            #endregion

            for (int i=0; i<udcFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                strRatioTotalPerformance += "     , ROUND((DECODE( AVG(DECODE(CUTOFF_DT, '" + arrStep[i] + "', UPEH, 0)), 0, 0, SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', QTY, 0))/AVG(DECODE(CUTOFF_DT, '" + arrStep[i] + "', UPEH, 0))*60 / (SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)) - SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', LOSS_TIME, 0))/60)) * ((SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)) - SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', LOSS_TIME, 0))/60) / SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0))))*100,2) RES_PERFORMANCE_" + i.ToString() + " " + "\n";
                strRatioPerformance += "     , ROUND(DECODE( AVG(DECODE(CUTOFF_DT, '" + arrStep[i] + "', UPEH, 0)), 0, 0, SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', QTY, 0))/AVG(DECODE(CUTOFF_DT, '" + arrStep[i] + "', UPEH, 0))*60 / (SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)) - SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', LOSS_TIME, 0))/60)*100),2) RATIO_PERFORMANCE_" + i.ToString() + "   " + "\n";
                strRatioTime += "     , ROUND((SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)) - SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', LOSS_TIME, 0))/60) / SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)) * 100, 2) RATIO_TIME_RUN_" + i.ToString() + "    " + "\n";
                strRatioBuha += "     , ROUND((SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)) - SUM(CASE WHEN CUTOFF_DT = '" + arrStep[i] + "' AND DOWN_EVENT_ID='RUN_DOWN' THEN LOSS_TIME ELSE 0 END)/60) / SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0))*100, 2) RATIO_BUHA_" + i.ToString() + "    " + "\n";

                strTimeJoup += "     , SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)) JOUP_TIME_" + i.ToString() + "   " + "\n";
                strTimeValueRun += "     , ROUND(DECODE(AVG(DECODE(CUTOFF_DT, '" + arrStep[i] + "', UPEH, 0)), 0, 0, SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', QTY, 0))/AVG(DECODE(CUTOFF_DT, '" + arrStep[i] + "', UPEH, 0))*60)) VALUE_RUN_TIME_" + i.ToString() + "    " + "\n";
                strTimeRun += "     , ROUND(SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)) - SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', LOSS_TIME, 0))/60) RUN_TIME_" + i.ToString() + "    " + "\n";
                strTimeLoss += "     , ROUND(SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', LOSS_TIME, 0))/60) LOSS_TIME_" + i.ToString() + "   " + "\n";
                strQty += "     , SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', QTY2, 0)) QTY2_" + i.ToString() + "  " + "\n";

                #region 주석처리 : 기간별 장비보유대수 구하기 과거버전
                //strSqlBoyuResCnt = "     , MAX(DECODE(CUTOFF_DT,  '" + arrStep[i] + "', ( " + "\n";
                //strSqlBoyuResCnt += "                                         SELECT COUNT(*) CNT " + "\n";
                //strSqlBoyuResCnt += "                                           FROM MRASRESDEF " + "\n";
                //strSqlBoyuResCnt += "                                          WHERE 1=1 " + "\n";
                //strSqlBoyuResCnt += "                                            AND FACTORY = RES.FACTORY " + "\n";
                //strSqlBoyuResCnt += "                                            " + QueryCond2.Replace(",", "") + "\n";
                //strSqlBoyuResCnt += "                                            AND RES_TYPE NOT IN ('DUMMY') " + "\n";
                //switch (udcFromToDate.DaySelector.SelectedValue.ToString())
                //{
                //    case "DAY":
                //        strSqlBoyuResCnt += "                                            AND SUBSTR(CREATE_TIME, 1, 8) <= '" + arrStep[i] + "' " + "\n";
                //        strSqlBoyuResCnt += "                                            AND (DELETE_TIME = ' ' OR (DELETE_TIME > ' ' AND SUBSTR(DELETE_TIME, 1, 8) > '" + arrStep[i] + "')) " + "\n";
                //        break;
                //    case "WEEK":
                //        strSqlBoyuResCnt += "                                            AND GET_WORK_DATE(CREATE_TIME, 'W') <= '" + arrStep[i] + "' " + "\n";
                //        strSqlBoyuResCnt += "                                            AND (DELETE_TIME = ' ' OR (DELETE_TIME > ' ' AND GET_WORK_DATE(DELETE_TIME, 'W') > '" + arrStep[i] + "')) " + "\n";
                //        break;
                //    case "MONTH":
                //        strSqlBoyuResCnt += "                                            AND SUBSTR(CREATE_TIME, 1, 6) <= '" + arrStep[i] + "' " + "\n";
                //        strSqlBoyuResCnt += "                                            AND (DELETE_TIME = ' ' OR (DELETE_TIME > ' ' AND SUBSTR(DELETE_TIME, 1, 6) > '" + arrStep[i] + "')) " + "\n";
                //        break;
                //}
                //strSqlBoyuResCnt += "                                            AND RES.RES_GRP_1 > '-'  " + "\n"; // 팀이 '-'인 것 제외
                //strSqlBoyuResCnt += "                                        ), 0)) BOYU_RES_CNT_" + i.ToString() + "\n";

                //strCntBoyuRes += strSqlBoyuResCnt;
                #endregion

                strCntBoyuRes += "     , COUNT(DISTINCT CASE WHEN CUTOFF_DT = '" + arrStep[i] + "' THEN RES_ID ELSE NULL END) BOYU_RES_CNT_" + i.ToString() + "  " + "\n";
                strCntRunnedRes += "     , COUNT(DISTINCT CASE WHEN CUTOFF_DT = '" + arrStep[i] + "' AND QTY > 0 THEN RES_ID ELSE NULL END) RUNNED_RES_CNT_" + i.ToString() + "  " + "\n";
                strCntIdleRes += "     , COUNT(DISTINCT CASE WHEN CUTOFF_DT = '" + arrStep[i] + "' AND QTY < 1 THEN RES_ID ELSE NULL END) IDLE_RES_CNT_" + i.ToString() + " \n";

                strRatioBm += "     , DECODE(SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', LOSS_TIME, 0))/60, SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)), 0, ROUND((SUM(CASE WHEN CUTOFF_DT='" + arrStep[i] + "' AND DOWN_EVENT_ID='BM' THEN LOSS_TIME ELSE 0 END)/60)/(SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)) - SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', LOSS_TIME, 0))/60)*100,2)) RATIO_BM_" + i.ToString() + "    " + "\n";
                strRatioRunDown += "     , ROUND(SUM(CASE WHEN CUTOFF_DT='" + arrStep[i] + "' AND DOWN_EVENT_ID='RUN_DOWN' THEN LOSS_TIME ELSE 0 END)/60 / SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)) * 100,2) RATIO_RUN_DOWN_" + i.ToString() + "      " + "\n";
                strRatioPm += "     , DECODE(SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', LOSS_TIME, 0))/60, SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)), 0, ROUND((SUM(CASE WHEN CUTOFF_DT='" + arrStep[i] + "' AND DOWN_EVENT_ID='PM' THEN LOSS_TIME ELSE 0 END)/60) / (SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)) - SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', LOSS_TIME, 0))/60) * 100,2)) RATIO_PM_" + i.ToString() + "      " + "\n";
                strRatioDevChg += "     , DECODE(SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', LOSS_TIME, 0))/60, SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)), 0, ROUND((SUM(CASE WHEN CUTOFF_DT='" + arrStep[i] + "' AND DOWN_EVENT_ID='DEV_CHG' THEN LOSS_TIME ELSE 0 END)/60) / (SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)) - SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', LOSS_TIME, 0))/60) * 100,2)) RATIO_DEV_CHG_" + i.ToString() + "      " + "\n";
                strRatioEngDown += "     , DECODE(SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', LOSS_TIME, 0))/60, SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)), 0, ROUND((SUM(CASE WHEN CUTOFF_DT='" + arrStep[i] + "' AND DOWN_EVENT_ID='ENG_DOWN' THEN LOSS_TIME ELSE 0 END)/60) / (SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)) - SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', LOSS_TIME, 0))/60) * 100,2)) RATIO_ENG_DOWN_" + i.ToString() + "      " + "\n";
                strRatioSuperDown += "     , DECODE(SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', LOSS_TIME, 0))/60, SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)), 0, ROUND((SUM(CASE WHEN CUTOFF_DT='" + arrStep[i] + "' AND DOWN_EVENT_ID='SUPER_DOWN' THEN LOSS_TIME ELSE 0 END)/60) / (SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)) - SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', LOSS_TIME, 0))/60) * 100,2)) RATIO_SUPER_DOWN_" + i.ToString() + "      " + "\n";
                strRatioUtilityDown += "     , DECODE(SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', LOSS_TIME, 0))/60, SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)), 0, ROUND((SUM(CASE WHEN CUTOFF_DT='" + arrStep[i] + "' AND DOWN_EVENT_ID='UTLITY_DOWN' THEN LOSS_TIME ELSE 0 END)/60) / (SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)) - SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', LOSS_TIME, 0))/60) * 100,2)) RATIO_UTILITY_DOWN_" + i.ToString() + "      " + "\n";
                strRatioETC += "     , DECODE(SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', LOSS_TIME, 0))/60, SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)), 0, ROUND((SUM(CASE WHEN CUTOFF_DT='" + arrStep[i] + "' AND DOWN_EVENT_ID='ETC_DOWN' THEN LOSS_TIME ELSE 0 END)/60) / (SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', JOUP_TIME, 0)) - SUM(DECODE(CUTOFF_DT, '" + arrStep[i] + "', LOSS_TIME, 0))/60) * 100,2)) RATIO_ETC_" + i.ToString() + "      " + "\n";

                strTimeBm += "     , ROUND(SUM(CASE WHEN CUTOFF_DT = '" + arrStep[i] + "' AND DOWN_EVENT_ID='BM' THEN LOSS_TIME ELSE 0 END)/60) TIME_BM_" + i.ToString() + "    " + "\n";
                strTimeRunDown += "     , ROUND(SUM(CASE WHEN CUTOFF_DT = '" + arrStep[i] + "' AND DOWN_EVENT_ID='RUN_DOWN' THEN LOSS_TIME ELSE 0 END)/60) TIME_RUN_DOWN_" + i.ToString() + "      " + "\n";
                strTimePm += "     , ROUND(SUM(CASE WHEN CUTOFF_DT = '" + arrStep[i] + "' AND DOWN_EVENT_ID='PM' THEN LOSS_TIME ELSE 0 END)/60) TIME_PM_" + i.ToString() + "      " + "\n";
                strTimeDevChg += "     , ROUND(SUM(CASE WHEN CUTOFF_DT = '" + arrStep[i] + "' AND DOWN_EVENT_ID='DEV_CHG' THEN LOSS_TIME ELSE 0 END)/60) TIME_DEV_CHG_" + i.ToString() + "      " + "\n";
                strTimeEngDown += "     , ROUND(SUM(CASE WHEN CUTOFF_DT = '" + arrStep[i] + "' AND DOWN_EVENT_ID='ENG_DOWN' THEN LOSS_TIME ELSE 0 END)/60) TIME_ENG_DOWN_" + i.ToString() + "      " + "\n";
                strTimeSuperDown += "     , ROUND(SUM(CASE WHEN CUTOFF_DT = '" + arrStep[i] + "' AND DOWN_EVENT_ID='SUPER_DOWN' THEN LOSS_TIME ELSE 0 END)/60) TIME_SUPER_DOWN_" + i.ToString() + "      " + "\n";
                strTimeUtilityDown += "     , ROUND(SUM(CASE WHEN CUTOFF_DT = '" + arrStep[i] + "' AND DOWN_EVENT_ID='UTILITY_DOWN' THEN LOSS_TIME ELSE 0 END)/60) TIME_UTILITY_DOWN_" + i.ToString() + "      " + "\n";
                strTimeETC += "     , ROUND(SUM(CASE WHEN CUTOFF_DT = '" + arrStep[i] + "' AND DOWN_EVENT_ID='ETC_DOWN' THEN LOSS_TIME ELSE 0 END)/60) TIME_ETC_" + i.ToString() + "      " + "\n";

                strCntBm += "     , SUM(CASE WHEN CUTOFF_DT = '" + arrStep[i] + "' AND DOWN_EVENT_ID='BM' THEN DOWN_CNT ELSE 0 END) CNT_BM_" + i.ToString() + "  " + "\n";
                strCntRunDown += "     , SUM(CASE WHEN CUTOFF_DT = '" + arrStep[i] + "' AND DOWN_EVENT_ID='RUN_DOWN' THEN DOWN_CNT ELSE 0 END) CNT_RUN_DOWN_" + i.ToString() + "      " + "\n";
                strCntPm += "     , SUM(CASE WHEN CUTOFF_DT = '" + arrStep[i] + "' AND DOWN_EVENT_ID='PM' THEN DOWN_CNT ELSE 0 END) CNT_PM_" + i.ToString() + "      " + "\n";
                strCntDevChg += "     , SUM(CASE WHEN CUTOFF_DT = '" + arrStep[i] + "' AND DOWN_EVENT_ID='DEV_CHG' THEN DOWN_CNT ELSE 0 END) CNT_DEV_CHG_" + i.ToString() + "      " + "\n";
                strCntEngDown += "     , SUM(CASE WHEN CUTOFF_DT = '" + arrStep[i] + "' AND DOWN_EVENT_ID='ENG_DOWN' THEN DOWN_CNT ELSE 0 END) CNT_ENG_DOWN_" + i.ToString() + "      " + "\n";
                strCntSuperDown += "     , SUM(CASE WHEN CUTOFF_DT = '" + arrStep[i] + "' AND DOWN_EVENT_ID='SUPER_DOWN' THEN DOWN_CNT ELSE 0 END) CNT_SUPER_DOWN_" + i.ToString() + "      " + "\n";
                strCntUtilityDown += "     , SUM(CASE WHEN CUTOFF_DT = '" + arrStep[i] + "' AND DOWN_EVENT_ID='UTILITY_DOWN' THEN DOWN_CNT ELSE 0 END) CNT_UTILITY_DOWN_" + i.ToString() + "      " + "\n";
                strCntETC += "     , SUM(CASE WHEN CUTOFF_DT = '" + arrStep[i] + "' AND DOWN_EVENT_ID='ETC_DOWN' THEN DOWN_CNT ELSE 0 END) CNT_ETC_" + i.ToString() + "            " + "\n";

            }
            #endregion

            #region " MAIN 쿼리 "

            strSqlString.Append("SELECT " + QueryCond1 + " " + "\n");
            strSqlString.Append("     , ' ' GUBUN_1 " + "\n");
            strSqlString.Append("     , ' ' GUBUN_2 " + "\n");
            // CUM 생략되어있음!
            strSqlString.Append(strRatioTotalPerformance);
            strSqlString.Append(strRatioPerformance);
            strSqlString.Append(strRatioTime);
            strSqlString.Append(strRatioBuha);

            strSqlString.Append(strTimeJoup);

            strSqlString.Append(strTimeValueRun);
            strSqlString.Append(strTimeRun);
            strSqlString.Append(strTimeLoss);

            strSqlString.Append(strQty);

            strSqlString.Append(strCntBoyuRes); 
            strSqlString.Append(strCntRunnedRes);
            strSqlString.Append(strCntIdleRes);
            strSqlString.Append(strRatioBm);
            strSqlString.Append(strRatioRunDown);
            strSqlString.Append(strRatioPm);
            strSqlString.Append(strRatioDevChg);
            strSqlString.Append(strRatioEngDown);
            strSqlString.Append(strRatioSuperDown);
            strSqlString.Append(strRatioUtilityDown);
            strSqlString.Append(strRatioETC);

            strSqlString.Append(strTimeBm);
            strSqlString.Append(strTimeRunDown);
            strSqlString.Append(strTimePm);
            strSqlString.Append(strTimeDevChg);
            strSqlString.Append(strTimeEngDown);
            strSqlString.Append(strTimeSuperDown);
            strSqlString.Append(strTimeUtilityDown);
            strSqlString.Append(strTimeETC);

            strSqlString.Append(strCntBm);
            strSqlString.Append(strCntRunDown);
            strSqlString.Append(strCntPm);
            strSqlString.Append(strCntDevChg);
            strSqlString.Append(strCntEngDown);
            strSqlString.Append(strCntSuperDown);
            strSqlString.Append(strCntUtilityDown);
            strSqlString.Append(strCntETC);
            strSqlString.Append("  FROM      " + "\n");
            strSqlString.Append("       ( " + "\n");
            strSqlString.Append("        SELECT RES.CUTOFF_DT, RES.FACTORY, RES.RES_GRP_1, RES.RES_GRP_2, RES.RES_GRP_3, RES.RES_GRP_5, RES.RES_GRP_6, RES.RES_ID " + "\n");
            strSqlString.Append("         " + "\n");
            strSqlString.Append("             , JOUP_TIME " + "\n");
            strSqlString.Append("             , RES.DOWN_EVENT_ID " + "\n");
            strSqlString.Append("             , RES.LOSS_TIME " + "\n");
            strSqlString.Append("             , RES.DOWN_CNT DOWN_CNT " + "\n");
            strSqlString.Append("             , NVL(MOV.UPEH,0) UPEH " + "\n");
            //strSqlString.Append("             , NVL(MOV.RUNNED_RES_CNT, 0) RUNNED_RES_CNT " + "\n");
            strSqlString.Append("             , NVL(MOV.QTY, 0) QTY " + "\n");
            strSqlString.Append("             , NVL(MOV.QTY2,0) QTY2              " + "\n");
            strSqlString.Append("          FROM    " + "\n");
            strSqlString.Append("               (  " + "\n");
            strSqlString.Append("                SELECT RES.CUTOFF_DT, RES.FACTORY, RES.RES_GRP_1, RES.RES_GRP_2, RES.RES_GRP_3, RES.RES_GRP_5, RES.RES_GRP_6, RES.RES_ID " + "\n");
            
            switch(udcFromToDate.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    strSqlString.Append("                     , 1440 JOUP_TIME " + "\n");
                    break;
                case "WEEK":
                    strSqlString.Append("                     , 10080 JOUP_TIME " + "\n"); // 7 * 1440
                    break;
                case "MONTH":
                    strSqlString.Append("                     , TO_CHAR(LAST_DAY(TO_DATE(RES.CUTOFF_DT, 'YYYYMM')), 'DD') * 1440 JOUP_TIME " + "\n");
                    break;
            }

            strSqlString.Append("                     , NVL(DWH.DOWN_EVENT_ID, ' ') DOWN_EVENT_ID " + "\n");
            strSqlString.Append("                     , NVL(SUM(DWH.TIME_SUM),0) LOSS_TIME " + "\n");
            strSqlString.Append("                     , DECODE(SUM(DWH.DOWN_COUNT),0,1, NVL(SUM(DWH.DOWN_COUNT),0))  DOWN_CNT " + "\n");
            strSqlString.Append("                  FROM  " + "\n");
            strSqlString.Append("                       (            " + "\n");
            strSqlString.Append("                        SELECT CUTOFF_DT, RES.FACTORY, RES.RES_GRP_1, RES.RES_GRP_2, RES.RES_GRP_3, RES.RES_GRP_5, RES.RES_GRP_6, RES.RES_ID, ROW_NUMBER() OVER(PARTITION BY CUTOFF_DT ORDER BY CUTOFF_DT) RN " + "\n");
            strSqlString.Append("                          FROM ( " + "\n");
            
            switch(udcFromToDate.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    strSqlString.Append("                                SELECT UNIQUE SYS_DATE CUTOFF_DT " + "\n");
                    strSqlString.Append("                                  FROM MWIPCALDEF " + "\n");
                    strSqlString.Append("                                 WHERE 1=1 " + "\n");
                    strSqlString.Append("                                   AND CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("                                   AND SYS_DATE > SUBSTR('" +  strFromDate + "', 1, 8) " + "\n");
                    strSqlString.Append("                                   AND SYS_DATE <= SUBSTR('" +  strToDate + "', 1, 8) " + "\n");

                    break;
                case "WEEK":
                    strSqlString.Append("                                SELECT UNIQUE GET_WORK_DATE(SYS_DATE, 'W') CUTOFF_DT " + "\n");
                    strSqlString.Append("                                  FROM MWIPCALDEF " + "\n");
                    strSqlString.Append("                                 WHERE 1=1 " + "\n");
                    strSqlString.Append("                                   AND CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("                                   AND SYS_DATE > SUBSTR('" +  strFromDate + "', 1, 8) " + "\n");
                    strSqlString.Append("                                   AND SYS_DATE <= SUBSTR('" +  strToDate + "', 1, 8) " + "\n");
                    break;
                case "MONTH":
                    strSqlString.Append("                                SELECT UNIQUE SUBSTR(SYS_DATE, 1, 6) CUTOFF_DT " + "\n");
                    strSqlString.Append("                                  FROM MWIPCALDEF " + "\n");
                    strSqlString.Append("                                 WHERE 1=1 " + "\n");
                    strSqlString.Append("                                   AND CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("                                   AND SUBSTR(SYS_DATE, 1, 6) > SUBSTR('" +  strFromDate + "', 1, 6) " + "\n");
                    strSqlString.Append("                                   AND SUBSTR(SYS_DATE, 1, 6) <= SUBSTR('" +  strToDate + "', 1, 6) " + "\n");
                    break;
            }

            strSqlString.Append("                               ) CAL " + "\n");
            strSqlString.Append("                             , MRASRESDEF RES " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND RES.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            
            #region " RAS 상세 조회 "
            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("                           AND RES.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("                           AND RES.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("                           AND RES.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("                           AND RES.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("                           AND RES.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("                           AND RES.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("                           AND RES.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

            strSqlString.Append("                           AND RES.RES_TYPE NOT IN ('DUMMY')" + "\n");
            
            // 요청사항 - FLAG Y인 장비만
            strSqlString.Append("                           AND RES.RES_GRP_3 IN ( SELECT KEY_1 FROM MGCMTBLDAT WHERE TABLE_NAME ='H_EQP_GRP' AND DATA_2 = 'Y' AND FACTORY " + cdvFactory.SelectedValueToQueryString + ")" + "\n");

            #endregion

            switch(udcFromToDate.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    strSqlString.Append("                           AND SUBSTR(RES.CREATE_TIME, 1, 6) < CAL.CUTOFF_DT  " + "\n");
                    strSqlString.Append("                           AND (RES.DELETE_TIME = ' ' OR (RES.DELETE_TIME > ' ' AND SUBSTR(RES.DELETE_TIME, 1, 6) > CAL.CUTOFF_DT)) " + "\n");
                    break;
                case "WEEK":
                    strSqlString.Append("                           AND GET_WORK_DATE(RES.CREATE_TIME, 'W') < CAL.CUTOFF_DT  " + "\n");
                    strSqlString.Append("                           AND (RES.DELETE_TIME = ' ' OR (RES.DELETE_TIME > ' ' AND GET_WORK_DATE(RES.DELETE_TIME, 'W') > CAL.CUTOFF_DT)) " + "\n");
                    break;
                case "MONTH":
                    strSqlString.Append("                           AND SUBSTR(RES.CREATE_TIME, 1, 6) < CAL.CUTOFF_DT  " + "\n");
                    strSqlString.Append("                           AND (RES.DELETE_TIME = ' ' OR (RES.DELETE_TIME > ' ' AND SUBSTR(RES.DELETE_TIME, 1, 6) > CAL.CUTOFF_DT)) " + "\n");
                    break;
            }

            strSqlString.Append("                         GROUP BY CUTOFF_DT, RES.FACTORY, RES.RES_GRP_1, RES.RES_GRP_2, RES.RES_GRP_3, RES.RES_GRP_5, RES.RES_GRP_6, RES.RES_ID " + "\n");
            strSqlString.Append("                       ) RES " + "\n");
            strSqlString.Append("                     , CSUMRESDWH DWH  " + "\n");
            strSqlString.Append("                 WHERE 1=1            " + "\n");
            strSqlString.Append("                   AND RES.FACTORY = DWH.FACTORY(+) " + "\n");
            strSqlString.Append("                   AND RES.RES_ID = DWH.RES_ID(+) " + "\n");

            switch(udcFromToDate.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    strSqlString.Append("                   AND RES.CUTOFF_DT = DWH.DOWN_DATE(+) " + "\n");
                    break;
                case "WEEK":
                    strSqlString.Append("                   AND RES.CUTOFF_DT = GET_WORK_DATE(DWH.DOWN_DATE(+), 'W') " + "\n");
                    break;
                case "MONTH":
                    strSqlString.Append("                   AND RES.CUTOFF_DT = SUBSTR(DWH.DOWN_DATE(+), 1, 6) " + "\n");
                    break;
            }

            strSqlString.Append("                   AND DWH.DOWN_DATE(+) BETWEEN TO_CHAR(TO_DATE('" + strFromDate + "', 'YYYYMMDDHH24MISS')+1, 'YYYYMMDD') AND GET_WORK_DATE('" + strToDate + "', 'D') " + "\n");
            strSqlString.Append("                   AND DWH.TIME_SUM(+) <= 86400   " + "\n");
            strSqlString.Append("                   AND RES.RES_GRP_1 > '-'  " + "\n");
            strSqlString.Append("                 GROUP BY RES.CUTOFF_DT, RES.FACTORY, RES.RES_GRP_1, RES.RES_GRP_2, RES.RES_GRP_3, RES.RES_GRP_5, RES.RES_GRP_6, RES.RES_ID, DWH.DOWN_EVENT_ID   " + "\n");
            strSqlString.Append("               ) RES     " + "\n");
            strSqlString.Append("             , (   " + "\n");

            switch(udcFromToDate.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    strSqlString.Append("                SELECT SUBSTR(WORK_DATE, 1, 8) CUTOFF_DT " + "\n");
                    break;
                case "WEEK":
                    strSqlString.Append("                SELECT GET_WORK_DATE(WORK_DATE, 'W') CUTOFF_DT " + "\n");
                    break;
                case "MONTH":
                    strSqlString.Append("                SELECT SUBSTR(WORK_DATE, 1, 6) CUTOFF_DT " + "\n");
                    break;
            }
            
            strSqlString.Append("                     , MOV.FACTORY, MOV.RES_ID " + "\n");
            //strSqlString.Append("                     , COUNT(*) OVER(PARTITION BY WORK_DATE) RUNNED_RES_CNT " + "\n");
            strSqlString.Append("                     , AVG(UPEH) UPEH, SUM(MOV.QTY) QTY " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN 1=1 " + "\n");
            
            #region " WIP 상세 조회 "
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                                 AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                                 AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                                 AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                                 AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                                 AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                                 AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                                 AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                                 AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            #endregion

            strSqlString.Append("                                THEN QTY " + "\n");
            strSqlString.Append("                                ELSE 0 " + "\n");
            strSqlString.Append("                           END) QTY2 " + "\n");
            strSqlString.Append("                  FROM (    " + "\n");
            strSqlString.Append("                        SELECT WORK_DATE, FACTORY, RES_GRP_1, RES_GRP_2, RES_GRP_3, RES_GRP_5, RES_GRP_6, RES_ID, MAT_ID, SUM(RESV_FIELD_1) UPEH, SUM(END_QTY_1) QTY    " + "\n");
            strSqlString.Append("                          FROM RSUMRESMOV     " + "\n");
            strSqlString.Append("                         WHERE 1=1    " + "\n");
            strSqlString.Append("                           AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                           AND WORK_DATE BETWEEN TO_CHAR(TO_DATE('" + strFromDate + "', 'YYYYMMDDHH24MISS')+1, 'YYYYMMDD') AND GET_WORK_DATE('" + strToDate + "', 'D') " + "\n");
            strSqlString.Append("                         GROUP BY WORK_DATE, FACTORY, RES_GRP_1, RES_GRP_2, RES_GRP_3, RES_GRP_5, RES_GRP_6, RES_ID, MAT_ID   " + "\n");
            strSqlString.Append("                       ) MOV   " + "\n");
            strSqlString.Append("                     , MWIPMATDEF MAT  " + "\n");
            strSqlString.Append("                 WHERE 1=1  " + "\n");
            strSqlString.Append("                   AND MOV.FACTORY = MAT.FACTORY  " + "\n");
            strSqlString.Append("                   AND MOV.MAT_ID = MAT.MAT_ID  " + "\n");
            strSqlString.Append("                   AND MAT.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND MAT.MAT_VER = 1  " + "\n");

            switch (udcFromToDate.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    strSqlString.Append("                 GROUP BY SUBSTR(WORK_DATE, 1, 8), MOV.FACTORY, MOV.RES_GRP_1, MOV.RES_GRP_2, MOV.RES_GRP_3, MOV.RES_GRP_5, MOV.RES_GRP_6, MOV.RES_ID   " + "\n");
                    break;
                case "WEEK":
                    strSqlString.Append("                 GROUP BY GET_WORK_DATE(WORK_DATE, 'W'), MOV.FACTORY, MOV.RES_GRP_1, MOV.RES_GRP_2, MOV.RES_GRP_3, MOV.RES_GRP_5, MOV.RES_GRP_6, MOV.RES_ID   " + "\n");
                    break;
                case "MONTH":
                    strSqlString.Append("                 GROUP BY SUBSTR(WORK_DATE, 1, 6), MOV.FACTORY, MOV.RES_GRP_1, MOV.RES_GRP_2, MOV.RES_GRP_3, MOV.RES_GRP_5, MOV.RES_GRP_6, MOV.RES_ID   " + "\n");
                    break;
            }

            strSqlString.Append("               ) MOV   " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND RES.CUTOFF_DT = MOV.CUTOFF_DT(+) " + "\n");
            strSqlString.Append("           AND RES.FACTORY = MOV.FACTORY(+)   " + "\n");
            strSqlString.Append("           AND RES.RES_ID = MOV.RES_ID(+)   " + "\n");
            strSqlString.Append("       ) RES " + "\n");
            strSqlString.Append(" GROUP BY RES.FACTORY, " + QueryCond3 + "\n");
            strSqlString.Append(" ORDER BY RES.FACTORY, " + QueryCond3 + "\n");

            #endregion

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
                        
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

            udcChartFX1.RPT_3_OpenData(3, udcFromToDate.SubtractBetweenFromToDate+2);
            int[] arrTotal = new Int32[udcFromToDate.SubtractBetweenFromToDate + 2];
            int[] arrPerf = new Int32[udcFromToDate.SubtractBetweenFromToDate + 2];
            int[] arrTime = new Int32[udcFromToDate.SubtractBetweenFromToDate + 2];

            for (int i = 0; i < arrTotal.Length; i++)
            {
                arrTotal[i] = 8 + i;
                arrPerf[i] = 8 + i;
                arrTime[i] = 8 + i;
            }

            double total = udcChartFX1.RPT_4_AddData(spdData, new int[] { 0 }, arrTotal, SeriseType.Rows);
            double perf = udcChartFX1.RPT_4_AddData(spdData, new int[] { 1 }, arrPerf, SeriseType.Rows);
            double time = udcChartFX1.RPT_4_AddData(spdData, new int[] { 2 }, arrTime, SeriseType.Rows);

            double max = double.MinValue;
            max = total>perf?total:perf;
            max = max>time?max:time;

            udcChartFX1.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            //udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 0, 1, "건수", AsixType.Y, DataTypes.Initeger, maxCnt * 1.2);
            //udcChartFX1.RPT_6_SetGallery(ChartType.Line, 1, 1, "누적점유", AsixType.Y2, DataTypes.Initeger, maxAddedRatio);

            //각 Serise별로 동일한 타입을 사용할 경우
            udcChartFX1.RPT_6_SetGallery(ChartType.Line, "[단위 : %]", AsixType.Y, DataTypes.Initeger, max*1.2);

            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, arrTotal);
            udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "종합", "성능", "시간" }, SoftwareFX.ChartFX.Docked.Top);

            // 기타 설정
            udcChartFX1.PointLabels = true;
            udcChartFX1.Series[0].PointLabelColor = Color.Black;
            udcChartFX1.Series[1].PointLabelColor = Color.Black;
            udcChartFX1.Series[2].PointLabelColor = Color.Black;
            udcChartFX1.RecalcScale();
        }

        private bool CheckEquipment()
        {
            DataTable dtTemp = null;
            string strLastDateZeroResCnt = string.Empty;
            string strSqlBoyuResCnt = string.Empty;

            string[] arrStep = udcFromToDate.getSelectDate();

            for (int i = 0; i < udcFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                strSqlBoyuResCnt = "SELECT COUNT(*) CNT " + "\n";
                strSqlBoyuResCnt += "  FROM MRASRESDEF RES " + "\n";
                strSqlBoyuResCnt += " WHERE 1=1 " + "\n";
                strSqlBoyuResCnt += "   AND FACTORY " + cdvFactory.SelectedValueToQueryString + " \n";

                #region " RAS 상세 조회 "
                if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                    strSqlBoyuResCnt += string.Format("   AND RES.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

                if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                    strSqlBoyuResCnt += string.Format("   AND RES.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

                if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                    strSqlBoyuResCnt += string.Format("   AND RES.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

                if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                    strSqlBoyuResCnt += string.Format("   AND RES.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

                if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                    strSqlBoyuResCnt += string.Format("   AND RES.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

                if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    strSqlBoyuResCnt += string.Format("   AND RES.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

                if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                    strSqlBoyuResCnt += string.Format("   AND RES.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

                strSqlBoyuResCnt += "   AND RES.RES_TYPE NOT IN ('DUMMY')" + "\n";
                
                // 요청사항 - FLAG Y인 장비만
                strSqlBoyuResCnt += "   AND RES.RES_GRP_3 IN ( SELECT KEY_1 FROM MGCMTBLDAT WHERE TABLE_NAME ='H_EQP_GRP' AND DATA_2 = 'Y' AND FACTORY " + cdvFactory.SelectedValueToQueryString + ")" + "\n";
                #endregion

                switch (udcFromToDate.DaySelector.SelectedValue.ToString())
                {
                    case "DAY":
                        strSqlBoyuResCnt += "   AND SUBSTR(CREATE_TIME, 1, 8) < '" + arrStep[i] + "' " + "\n";
                        strSqlBoyuResCnt += "   AND (DELETE_TIME = ' ' OR (DELETE_TIME > ' ' AND SUBSTR(DELETE_TIME, 1, 8) > '" + arrStep[i] + "')) " + "\n";
                        break;
                    case "WEEK":
                        strSqlBoyuResCnt += "   AND GET_WORK_DATE(CREATE_TIME, 'W') < '" + arrStep[i] + "' " + "\n";
                        strSqlBoyuResCnt += "   AND (DELETE_TIME = ' ' OR (DELETE_TIME > ' ' AND GET_WORK_DATE(DELETE_TIME, 'W') > '" + arrStep[i] + "')) " + "\n";
                        break;
                    case "MONTH":
                        strSqlBoyuResCnt += "   AND SUBSTR(CREATE_TIME, 1, 6) < '" + arrStep[i] + "' " + "\n";
                        strSqlBoyuResCnt += "   AND (DELETE_TIME = ' ' OR (DELETE_TIME > ' ' AND SUBSTR(DELETE_TIME, 1, 6) > '" + arrStep[i] + "')) " + "\n";
                        break;
                }

                dtTemp = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlBoyuResCnt);
                if (dtTemp.Rows[0]["CNT"].ToString() == "0")
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
        
        #region " EVENT HANDLER "

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            if (CheckField() == false) return;

            if(CheckEquipment() == false) 
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD030", GlobalVariable.gcLanguage));
                return;
            }

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
                
                spdData.DataSource = dt;
                spdData.RPT_DivideRows(8, 36, udcFromToDate.SubtractBetweenFromToDate + 2);

                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                //3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 4, 0, 9, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit4
                spdData.RPT_AutoFit(false);

                FarPoint.Win.Spread.CellType.RichTextCellType ct = new FarPoint.Win.Spread.CellType.RichTextCellType();
                ct.Multiline = true;
                ct.WordWrap = true;

                spdData.ActiveSheet.Columns[6].CellType = ct;

                spdData.RPT_FillColumnData(6, new string[] { 
                    "efficiency"
                    , "efficiency"
                    , "efficiency"
                    , "efficiency"
                    , "Operating hours during working hours"
                    , "Operating hours during working hours"
                    , "Operating hours during working hours"
                    , "Operating hours during working hours"
                    , "Performance"
                    , "Equipment Status"
                    , "Equipment Status"
                    , "Equipment Status"
                    , "Time Loss (Ratio)"
                    , "Time Loss (Ratio)"
                    , "Time Loss (Ratio)"
                    , "Time Loss (Ratio)"
                    , "Time Loss (Ratio)"
                    , "Time Loss (Ratio)"
                    , "Time Loss (Ratio)"
                    , "Time Loss (Ratio)"
                    , "Time Loss (Time)"
                    , "Time Loss (Time)"
                    , "Time Loss (Time)"
                    , "Time Loss (Time)"
                    , "Time Loss (Time)"
                    , "Time Loss (Time)"
                    , "Time Loss (Time)"
                    , "Time Loss (Time)"
                    , "Time Loss (Count)"
                    , "Time Loss (Count)"
                    , "Time Loss (Count)"
                    , "Time Loss (Count)"
                    , "Time Loss (Count)"
                    , "Time Loss (Count)"
                    , "Time Loss (Count)"
                    , "Time Loss (Count)" });

                spdData.RPT_FillColumnData(7, new string[] { 
                    "Synthesis"
                    , "Performance"
                    , "time"
                    , "load"
                    , "Operating hours"
                    , "value operation"
                    , "Uptime"
                    , "Stop time"
                    , "Production rate"
                    , "A holding equipment"
                    , "operating equipment"
                    , "non-operation equipment"
                    , "breakdown"
                    , "Run Down"
                    , "Preventive Check"
                    , "Replacement of product"
                    , "Eng Down"
                    , "Super Down"
                    , "Utility Down"
                    , "Quality failure"
                    , "breakdown"
                    , "Run Down"
                    , "Preventive Check"
                    , "Replacement of product"
                    , "Eng Down"
                    , "Super Down"
                    , "Utility Down"
                    , "Quality failure"
                    , "breakdown"
                    , "Run Down"
                    , "Preventive Check"
                    , "Replacement of product"
                    , "Eng Down"
                    , "Super Down"
                    , "Utility Down"
                    , "Quality failure" });

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

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
        }

        #endregion
    }
}