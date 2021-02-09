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
    /// 클  래  스: RAS020210<br/>
    /// 클래스요약: UPEH 실적<br/>
    /// 작  성  자: 미라콤 김민규<br/>
    /// 최초작성일: 2008-12-30<br/>
    /// 상세  설명: UPEH 실적<br/>
    /// 변경  내용: <br/>
    /// </summary>
    /// 

    public partial class RAS020210 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        static DataTable dt2 = null;
        static bool b_load_flag = false;
        
        public RAS020210()
        {
            InitializeComponent();         
            SortInit();  
            GridColumnInit(); //해더 한줄짜리 샘플
            cdvFromTo.AutoBinding();
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
            
            if (udcRASCondition6.Text.TrimEnd() == "ALL" || udcRASCondition6.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD028", GlobalVariable.gcLanguage));
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
            spdData.RPT_AddBasicColumn("UPEH", 0, 5, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("classification", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);

            try            
            {
                if (cdvFactory.txtValue != "")
                {
                    dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());

                    if (dt2.Rows.Count == 0)
                    {
                        dt2.Dispose();
                        return;
                    }

                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(dt2.Rows[i][0].ToString(), 0, 7 + i, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 80);
                    }
                }
            }
            catch(Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Team in charge", "UPH.RES_GRP_1", "UPH.RES_GRP_1", "RES_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Part", "UPH.RES_GRP_2", "UPH.RES_GRP_2", "RES_GRP_2", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation", "UPH.RES_GRP_3", "UPH.RES_GRP_3", "RES_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Maker", "UPH.RES_GRP_5", "UPH.RES_GRP_5", "RES_GRP_5", false);            
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "UPH.RES_GRP_6", "UPH.RES_GRP_6", "RES_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("UPEH", "UPH.RES_GRP_7", "UPH.RES_GRP_7", "RES_GRP_7", false);            
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

            strSqlString.AppendFormat("SELECT {0}, ' ' " + "\n", QueryCond1);

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                //strSqlString.Append("                   , SUM(DECODE(UPH.RES_ID, '"+ dt2.Rows[i][0]+"', UPEH, 0))AS UPEH"+ i + "\n");
                strSqlString.Append("   , MAX(NVL(DECODE(UPH.RES_ID, '" + dt2.Rows[i][0] + "', REPLACE(DATA_4, ' ', 0)), 0)) UPEH" + i + "\n");
                //, MAX(NVL(DECODE(UPH.RES_ID, 'BB001', REPLACE(DATA_4, ' ', 0)), 0)) UPEH0
            }

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                //strSqlString.Append("                   , ROUND(SUM(DECODE(UPH.RES_ID, '" + dt2.Rows[i][0] + "', DECODE((1440 - NVL(TIME_SUM,0)/60), 0, 0, QTY*(60/(1440 - NVL(TIME_SUM,0)/60)), 0), 0)), 3)AS REAL_UPEH" + i + "\n");
                strSqlString.Append("   , ROUND(SUM(NVL(DECODE(UPH.RES_ID, '" + dt2.Rows[i][0] + "', LOT_QTY*(60/((((TO_DATE('" + strToDate + "', 'YYYYMMDDHH24MISS') - TO_DATE('" + strFromDate + "', 'YYYYMMDDHH24MISS'))*24*60*60+1)-(DWH.TIME_SUM))/60)) , 0), 0)), 3) REAL_UPEH" + i + "\n");
                //, ROUND(SUM(NVL(DECODE(UPH.RES_ID, 'BB001', LOT_QTY*(60/((((TO_DATE('20090220215959', 'YYYYMMDDHH24MISS') - TO_DATE('20090218220000', 'YYYYMMDDHH24MISS'))*24*60*60+1)-(DWH.TIME_SUM))/60)) , 0), 0)), 3) REAL_UPEH0
            }
           
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                //strSqlString.Append("                   , ROUND(SUM(DECODE(UPH.RES_ID, '" + dt2.Rows[i][0] + "', (TO_DATE('" + strToDate + "', 'YYYYMMDDHH24MISS') - TO_DATE('" + strFromDate + "', 'YYYYMMDDHH24MISS'))*1440 - TIME_SUM/60, 0)), 3) AS RUN_TIME" + i + "\n");
                strSqlString.Append("   , ROUND(SUM(DECODE(UPH.RES_ID, '" + dt2.Rows[i][0] + "', (TO_DATE('" + strToDate + "', 'YYYYMMDDHH24MISS') - TO_DATE('" + strFromDate + "', 'YYYYMMDDHH24MISS'))*1440 - DWH.TIME_SUM/60, 0)), 3) AS RUN_TIME" + i + "\n");
            }
       
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                //strSqlString.Append("                   , SUM(DECODE(UPH.RES_ID, '" + dt2.Rows[i][0] + "', UPH.QTY, 0))AS QTY" + i + "\n");
                strSqlString.Append("   , SUM(NVL(DECODE(UPH.RES_ID, '" + dt2.Rows[i][0] + "', UPH.LOT_QTY), 0)) LOT_QTY" + i + "\n");
                //, SUM(NVL(DECODE(UPH.RES_ID, 'BB001', UPH.LOT_QTY), 0)) LOT_QTY0
            }

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                //strSqlString.Append("                   , SUM(DECODE(UPH.RES_ID, '" + dt2.Rows[i][0] + "', NVL(LOT.LOT_COUNT,0), 0))AS LOT_COUNT" + i + "\n");
                strSqlString.Append("   , SUM(NVL(DECODE(UPH.RES_ID, '" + dt2.Rows[i][0] + "', UPH.LOT_COUNT), 0)) LOT_COUNT" + i + "\n");
                //, SUM(NVL(DECODE(UPH.RES_ID, 'BB001', UPH.LOT_COUNT), 0)) LOT_COUNT0
            }

            strSqlString.Append("FROM " + "\n");
            strSqlString.Append("    ( " + "\n");
            strSqlString.Append("        SELECT " + "\n");
            strSqlString.Append("            FACTORY, RES_GRP_1, RES_GRP_2, RES_ID, RES_GRP_3, RES_GRP_5, RES_GRP_6, RES_GRP_7 " + "\n");
            strSqlString.Append("            , SUM(UPEH) UPEH_SUM " + "\n");
            strSqlString.Append("            , COUNT(LOT_ID) LOT_COUNT " + "\n");
            strSqlString.Append("            , SUM(QTY_1) LOT_QTY " + "\n");
            strSqlString.Append("        FROM " + "\n");
            strSqlString.Append("            ( " + "\n");
            strSqlString.Append("                SELECT " + "\n");
            strSqlString.Append("                    G1.DATA_1 RES_GRP_1 " + "\n");
            strSqlString.Append("                    , G2.DATA_1 RES_GRP_2 " + "\n");
            strSqlString.Append("                    , LOT.* " + "\n");
            strSqlString.Append("                    , RES.RES_ID " + "\n");
            strSqlString.Append("                    , RES.RES_GRP_3 " + "\n");
            strSqlString.Append("                    , RES.RES_GRP_5 " + "\n");
            strSqlString.Append("                    , RES.RES_GRP_6 " + "\n");
            strSqlString.Append("                    , RES.RES_GRP_7 " + "\n");
            strSqlString.Append("                FROM " + "\n");
            strSqlString.Append("                    ( " + "\n");
            strSqlString.Append("                        SELECT * FROM CWIPLOTEND " + "\n");
            strSqlString.Append("                        WHERE (LOT_ID, HIST_SEQ) IN ( " + "\n");
            strSqlString.Append("                            SELECT LOT_ID, HIST_SEQ FROM CWIPLOTEND LOT " + "\n");
            strSqlString.Append("                            WHERE 1 = 1 " + "\n");
            strSqlString.Append("                                AND LOT.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                                AND LOT.TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "' " + "\n");
            strSqlString.Append("                            ) " + "\n");
            strSqlString.Append("                    ) LOT " + "\n");
            strSqlString.Append("                    , MRASRESDEF RES " + "\n");
            strSqlString.Append("                    , MGCMTBLDAT G1 " + "\n");
            strSqlString.Append("                    , MGCMTBLDAT G2 " + "\n");
            strSqlString.Append("                WHERE 1 = 1 " + "\n");
            strSqlString.Append("                    AND RES.FACTORY = LOT.FACTORY                     " + "\n");
            strSqlString.Append("                    AND RES.RES_TYPE NOT IN ('DUMMY') " + "\n");
            strSqlString.Append("                    AND RES.RES_ID = LOT.END_RES_ID " + "\n");
            strSqlString.Append("                    AND RES.RES_GRP_3 IN ( SELECT KEY_1 FROM MGCMTBLDAT WHERE TABLE_NAME ='H_EQP_GRP' AND DATA_2 = 'Y' AND FACTORY " + cdvFactory.SelectedValueToQueryString + ")" + "\n");

            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("                    AND RES.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);
            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("                    AND RES.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);
            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("                    AND RES.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);
            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("                    AND RES.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);
            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("                    AND RES.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("                    AND RES.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

            strSqlString.Append("                    AND G1.FACTORY = RES.FACTORY " + "\n");
            strSqlString.Append("                    AND G1.TABLE_NAME = 'H_DEPARTMENT' " + "\n");
            strSqlString.Append("                    AND G1.KEY_1 = RES.RES_GRP_1 " + "\n");
            strSqlString.Append("                    AND G2.FACTORY = RES.FACTORY " + "\n");
            strSqlString.Append("                    AND G2.TABLE_NAME = 'H_DEPARTMENT' " + "\n");
            strSqlString.Append("                    AND G2.KEY_1 = RES.RES_GRP_2 " + "\n");
            strSqlString.Append("            ) " + "\n");
            strSqlString.Append("        GROUP BY FACTORY, RES_GRP_1, RES_GRP_2, RES_ID, RES_GRP_3, RES_GRP_5, RES_GRP_6, RES_GRP_7 " + "\n");
            strSqlString.Append("    ) UPH " + "\n");
            strSqlString.Append("    ,( " + "\n");
            strSqlString.Append("        SELECT " + "\n");
            strSqlString.Append("            FACTORY, RES_ID " + "\n");
            strSqlString.Append("            , SUM(TIME_SUM) TIME_SUM " + "\n");
            strSqlString.Append("        FROM " + "\n");
            strSqlString.Append("            CSUMRESDWH " + "\n");
            strSqlString.Append("        WHERE 1=1 " + "\n");
            strSqlString.Append("            AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("            AND DOWN_DATE BETWEEN '" + strFromDate + "' AND '" + strToDate + "' " + "\n");
            strSqlString.Append("        GROUP BY FACTORY, RES_ID " + "\n");
            strSqlString.Append("    ) DWH " + "\n");
            strSqlString.Append("    , MGCMTBLDAT GCM " + "\n");
            strSqlString.Append("WHERE 1 = 1 " + "\n");
            strSqlString.Append("    AND DWH.FACTORY = UPH.FACTORY(+) " + "\n");
            strSqlString.Append("    AND DWH.RES_ID = UPH.RES_ID(+) " + "\n");
            strSqlString.Append("    AND UPH.LOT_COUNT <> 0 " + "\n");
            strSqlString.Append("    AND GCM.FACTORY = UPH.FACTORY " + "\n");
            strSqlString.Append("    AND GCM.TABLE_NAME = 'H_EQ_MODEL' " + "\n");
            strSqlString.Append("    AND GCM.KEY_1 = UPH.RES_GRP_6 " + "\n");
            strSqlString.AppendFormat("GROUP BY UPH.FACTORY, {0} " + "\n", QueryCond2);


            /*
            strSqlString.Append("                FROM ( " + "\n");
            strSqlString.Append("                     SELECT RES.FACTORY, RES.RES_GRP_1, RES.RES_GRP_2, RES.RES_GRP_3, RES.RES_GRP_5, RES.RES_GRP_6, RES.RES_ID, RES.RES_GRP_7 " + "\n");
            strSqlString.Append("                          , RES.OPER, RES.MAT_ID, RES.QTY, NVL(UPH.UPEH, 0) UPEH " + "\n");
            strSqlString.Append("                       FROM ( " + "\n");
            strSqlString.Append("                             SELECT RES.FACTORY, RES.RES_GRP_1, RES.RES_GRP_2, RES.RES_GRP_3, RES.RES_GRP_5, RES.RES_GRP_6, RES.RES_ID, RES.RES_GRP_7 " + "\n");
            strSqlString.Append("                                  , MOV.OPER, MOV.MAT_ID, SUM(MOV.END_QTY_1) QTY " + "\n");
            strSqlString.Append("                               FROM MRASRESDEF RES " + "\n");
            strSqlString.Append("                                  , RSUMRESMOV MOV " + "\n");
            strSqlString.Append("                              WHERE 1=1 " + "\n");
            strSqlString.Append("                                AND RES.FACTORY = MOV.FACTORY(+) " + "\n");
            strSqlString.Append("                                AND RES.RES_ID = MOV.RES_ID(+) " + "\n");              
            strSqlString.Append("                                AND RES.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                                AND RES.RES_TYPE NOT IN ('DUMMY')  " + "\n");
            strSqlString.Append("                                AND MOV.WORK_DATE BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  " + "\n");

            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("                                AND RES.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("                                AND RES.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("                                AND RES.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            //if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
            //    strSqlString.AppendFormat("     AND RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("                                AND RES.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("                                AND RES.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("                                AND RES.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

            strSqlString.Append("                              GROUP BY RES.FACTORY, RES.RES_GRP_1, RES.RES_GRP_2, RES.RES_GRP_3, RES.RES_GRP_5, RES.RES_GRP_6, RES.RES_GRP_7 " + "\n");
            strSqlString.Append("                                  , RES.RES_ID, MOV.OPER, MOV.MAT_ID " + "\n");
            strSqlString.Append("                            ) RES " + "\n");
            strSqlString.Append("                          , CRASUPHDEF UPH " + "\n");
            strSqlString.Append("                          , MWIPMATDEF MAT " + "\n");
            strSqlString.Append("                      WHERE 1=1 " + "\n");
            strSqlString.Append("                        AND RES.FACTORY = UPH.FACTORY(+) " + "\n");
            strSqlString.Append("                        AND RES.OPER = UPH.OPER(+) " + "\n");
            strSqlString.Append("                        AND RES.RES_GRP_6  = UPH.RES_MODEL(+) " + "\n");
            strSqlString.Append("                        AND RES.RES_GRP_7 = UPH.UPEH_GRP(+) " + "\n");
            strSqlString.Append("                        AND RES.MAT_ID = UPH.MAT_ID(+) " + "\n");
            strSqlString.Append("                        AND RES.MAT_ID = MAT.MAT_ID(+) " + "\n");
            strSqlString.Append("                     ) UPH " + "\n");
            strSqlString.Append("                   , ( " + "\n");
            strSqlString.Append("                     SELECT FACTORY, RES_ID, TIME_SUM, DOWN_DATE  " + "\n");
            strSqlString.Append("                       FROM CSUMRESDWH    " + "\n");
            strSqlString.Append("                      WHERE 1=1   " + "\n");
            strSqlString.Append("                        AND DOWN_DATE BETWEEN '" + strFromDate + "' AND '" + strToDate + "'    " + "\n");
            strSqlString.Append("                        AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   GROUP BY FACTORY, RES_ID, TIME_SUM, DOWN_DATE  " + "\n");
            strSqlString.Append("                     ) DWH " + "\n");
            strSqlString.Append("                   , (  " + "\n");
            strSqlString.Append("                     SELECT FACTORY, MAT_ID, COUNT(UNIQUE(LOT_ID))AS LOT_COUNT  " + "\n");
            strSqlString.Append("                       FROM MRASRESLTH " + "\n");
            strSqlString.Append("                      WHERE EVENT_ID = 'END_LOT' " + "\n");
            strSqlString.Append("                        AND RES_HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                        AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'   " + "\n");
            strSqlString.Append("                   GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                     ) LOT                       " + "\n");
            strSqlString.Append("               WHERE 1=1 " + "\n");
            strSqlString.Append("                 AND UPH.FACTORY = DWH.FACTORY(+) " + "\n");
            strSqlString.Append("                 AND UPH.RES_ID = DWH.RES_ID(+) " + "\n");
            strSqlString.Append("                 AND UPH.FACTORY = LOT.FACTORY(+) " + "\n");
            strSqlString.Append("                 AND UPH.MAT_ID = LOT.MAT_ID(+)                  " + "\n");
            strSqlString.AppendFormat("            GROUP BY UPH.FACTORY, {0} " + "\n", QueryCond2);
            */

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
            
            return strSqlString.ToString();
        }

        private string MakeSqlString1()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            string strFromDate = cdvFromTo.ExactFromDate;
            string strToDate = cdvFromTo.ExactToDate;

            strSqlString.Append("SELECT RES_ID FROM MRASRESDEF WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");

            strSqlString.Append(" AND RES_TYPE <> 'DUMMY' " + "\n");
            strSqlString.Append(" AND RES_GRP_3 IN ( SELECT KEY_1 FROM MGCMTBLDAT WHERE TABLE_NAME ='H_EQP_GRP' AND DATA_2 = 'Y' AND FACTORY " + cdvFactory.SelectedValueToQueryString + ")" + "\n");

            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("     AND RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("     AND RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("     AND RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            //if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
            //    strSqlString.AppendFormat("     AND RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("     AND RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("     AND RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("     AND RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

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
            
            udcChartFX1.RPT_3_OpenData(1, dt2.Rows.Count);
            int[] Upeh_Columns = new Int32[dt2.Rows.Count];            
            int[] columnsHeader = new Int32[dt2.Rows.Count];

            for (int i = 0; i < Upeh_Columns.Length; i++)
            {
                columnsHeader[i] = 7 + i;
                Upeh_Columns[i] = 7 + i;
                //Mtba_Columns[i] = 7 + i;
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
            string[] exam = new string[] {"실 UPEH"};  

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
            double Upeh = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 1 }, Upeh_Columns, SeriseType.Rows);

            //MTBF
           // double Mtbf = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount+3, rowCount+4, rowCount+5 }, Mtba_Columns, SeriseType.Rows);
            
            udcChartFX1.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 0, 1, "UPEH", AsixType.Y, DataTypes.Initeger, Upeh * 1.3);
            //udcChartFX1.RPT_6_SetGallery(ChartType.Line, 1, 4, "MTBF", AsixType.Y2, DataTypes.Initeger, Mtbf * 1.2);
            
            

            //각 Serise별로 동일한 타입을 사용할 경우
            //udcChartFX1.RPT_6_SetGallery(ChartType.Bar, "[단위 : sls]", AsixType.Y, DataTypes.Initeger, yield * 1.2);

            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            
            udcChartFX1.RPT_8_SetSeriseLegend(exam, SoftwareFX.ChartFX.Docked.Top);

            // 기타 설정
            udcChartFX1.PointLabels = true;
            udcChartFX1.Series[0].PointLabelColor = Color.Blue;                        
            udcChartFX1.RightGap = 10;
            udcChartFX1.AxisY.DataFormat.Decimals = 0;            
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
                    //LoadingPopUp.LoadingPopUpHidden();
                    udcChartFX1.RPT_1_ChartInit();
                    udcChartFX1.RPT_2_ClearData();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));                    
                    return;
                }

                //by John
                //1.Griid 합계 표시
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 10, null, null, btnSort);
                //토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용


                 int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 0, 7, 7, 5, dt2.Rows.Count, btnSort);

                //spdData.DataSource = dt;
                //spdData.RPT_DivideRows(9,  7, cdvFromTo.SubtractBetweenFromToDate+1);

                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 6, 0, 5, true, Align.Center, VerticalAlign.Center);


                //4. Column Auto Fit4
                spdData.RPT_AutoFit(false);

                spdData.RPT_FillColumnData(6, new string[] { "Standard UPEH", "Actual UPEH", "Uptime", "output", "Production Lot Count" });

                ShowChart(0);                
            }
            catch (Exception ex)
            {
               //LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                //LoadingPopUp.LoadingPopUpHidden();
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
        }

        private void RAS020210_Load(object sender, EventArgs e)
        {
            if (b_load_flag == false)
            {
                //RES_ID가 필수이므로 Detail이 기본으로 보이도록
                btnDetail.PerformClick();
                b_load_flag = true;
            }
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