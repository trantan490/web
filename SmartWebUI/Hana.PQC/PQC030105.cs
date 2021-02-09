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
    /// 클  래  스: PQC030105<br/>
    /// 클래스요약: 설비평가현황조회<br/>
    /// 작  성  자: 미라콤 김민규<br/>
    /// 최초작성일: 2009-01-22<br/>
    /// 상세  설명: 설비평가현황조회<br/>
    /// 변경  내용: <br/>
    /// </summary>
    /// 

    public partial class PQC030105 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public PQC030105()
        {
            InitializeComponent();
            GridColumnInit();
            cdvFromTo.AutoBinding();
            udcChartFX1.RPT_1_ChartInit();  //차트 초기화. 

            this.cdvKind.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvResult.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvOper.sFactory = GlobalVariable.gsAssyDefaultFactory;

            cboGraph.SelectedIndexChanged -= cboGraph_SelectedIndexChanged;
            cboGraph.SelectedIndex = 0;
            cboGraph.SelectedIndexChanged += cboGraph_SelectedIndexChanged;
        }

        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            //if(udcRASCondition6.Text.TrimEnd() == "ALL")
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD028", GlobalVariable.gcLanguage));
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

            spdData.RPT_AddBasicColumn("Equipment Evaluation Status", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Evaluation start date", 1, 0, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Evaluation Type", 1, 1, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Equipment number", 1, 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("공정", 1, 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Customer", 1, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Lot No.", 1, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Lot QtY", 1, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Defect quantity", 1, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_MerageHeaderColumnSpan(0, 0, 8);

            spdData.RPT_AddBasicColumn("PKG Type", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LEAD수", 1, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PKG", 1, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Density", 1, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_MerageHeaderColumnSpan(0, 8, 3);

            spdData.RPT_AddBasicColumn("Lot quantity", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Defective rate" + "\n" + "(ppm)", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Evaluation completion date", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Evaluation results", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Reliability Input Date", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Reliability Evaluation Results", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Customer Approval", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Evaluation TAT", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("The details", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);

            spdData.RPT_MerageHeaderRowSpan(0, 11, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 12, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 13, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 14, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 15, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 16, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 17, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 18, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 19, 2);
        }


        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            // 사용 않음
        }


        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        #region Chart 생성을 위한 쿼리
        private string MakeSqlString(int Step)
        {
            StringBuilder strSqlString = new StringBuilder();

            string[] selectDate1 = new string[cdvFromTo.SubtractBetweenFromToDate + 1];
            string strFromDate = cdvFromTo.ExactFromDate;
            string strToDate = cdvFromTo.ExactToDate;
            string strDate = string.Empty;

            int Between = cdvFromTo.SubtractBetweenFromToDate + 1;

            selectDate1 = cdvFromTo.getSelectDate();

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

            switch (Step)
            {
                case 0:
                    {
                        strSqlString.Append("            SELECT A.CREATE_TIME " + "\n");
                        strSqlString.Append("                 , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_PARITY_CLASS' AND KEY_1 = A.PARITY_CLASS AND ROWNUM=1 ), '-') AS P_CLASS " + "\n");                        
                        strSqlString.Append("                 , A.RES_ID " + "\n");
                        strSqlString.Append("                 , A.OPER " + "\n");
                        strSqlString.Append("                 , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), 0) AS CUSTOMER " + "\n");
                        strSqlString.Append("                 , B.LOT_ID " + "\n");
                        strSqlString.Append("                 , NVL((SELECT CREATE_QTY_1 FROM RWIPLOTSTS WHERE FACTORY = A.FACTORY AND LOT_ID = B.LOT_ID), 0) AS LOT_QTY   " + "\n");
                        strSqlString.Append("                 , A.DEFECT_QTY " + "\n");
                        strSqlString.Append("                 , C.MAT_GRP_6 " + "\n");
                        strSqlString.Append("                 , C.MAT_GRP_3 " + "\n");
                        strSqlString.Append("                 , C.MAT_GRP_7 " + "\n");
                        strSqlString.Append("                 , A.LOT_CNT " + "\n");
                        strSqlString.Append("                 , CASE WHEN NVL((SELECT CREATE_QTY_1 FROM RWIPLOTSTS WHERE FACTORY = A.FACTORY AND LOT_ID = B.LOT_ID),0) = 0   " + "\n");
                        strSqlString.Append("                        THEN 0 " + "\n");
                        strSqlString.Append("                        ELSE DEFECT_QTY/(SELECT CREATE_QTY_1 FROM RWIPLOTSTS WHERE FACTORY = A.FACTORY AND LOT_ID = B.LOT_ID)  " + "\n");
                        strSqlString.Append("                   END AS PPM " + "\n");
                        strSqlString.Append("                 , A.UPDATE_TIME  " + "\n");
                        strSqlString.Append("                 , A.RESULT_FLAG  " + "\n");
                        strSqlString.Append("                 , NVL(D.CREATE_TIME, ' ')  " + "\n");
                        strSqlString.Append("                 , NVL(D.RESULT_FLAG, ' ') " + "\n");
                        strSqlString.Append("                 , ' ' C_APPROVAL " + "\n");
                        strSqlString.Append("                 ,  CASE WHEN A.RESULT_FLAG <> ' '  " + "\n");
                        strSqlString.Append("                         THEN TRUNC(TO_DATE(A.UPDATE_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(A.CREATE_TIME, 'YYYYMMDDHH24MISS'),0)  " + "\n");
                        strSqlString.Append("                         ELSE 0 " + "\n");
                        strSqlString.Append("                    END AS TAT " + "\n");
                        strSqlString.Append("                 , A.USER_COMMENT " + "\n");
                        strSqlString.Append("              FROM CQCMPRTSTS A " + "\n");
                        strSqlString.Append("                 , CQCMPRTLOT B " + "\n");
                        strSqlString.Append("                 , MWIPMATDEF C " + "\n");
                        strSqlString.Append("                 , CQCMPRTREL D " + "\n");
                        strSqlString.Append("             WHERE 1=1 " + "\n");
                        strSqlString.Append("               AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                        strSqlString.Append("               AND A.FACTORY = B.FACTORY " + "\n");
                        strSqlString.Append("               AND A.FACTORY = C.FACTORY " + "\n");
                        strSqlString.Append("               AND A.MAT_ID = C.MAT_ID " + "\n");
                        strSqlString.Append("               AND A.PARITY_ID = B.PARITY_ID " + "\n");
                        strSqlString.Append("               AND B.FACTORY = D.FACTORY(+) " + "\n");
                        strSqlString.Append("               AND B.LOT_ID = D.LOT_NO(+)   " + "\n");
                        strSqlString.Append("               AND A.CREATE_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("               AND A.PARITY_CLASS " + cdvKind.SelectedValueToQueryString + "\n");                        
                        strSqlString.Append("               AND A.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                        strSqlString.Append("               AND C.MAT_GRP_3 " + cdvPkg.SelectedValueToQueryString + "\n");
                        strSqlString.Append("               AND A.RESULT_FLAG " + cdvResult.SelectedValueToQueryString + "\n");
                        //strSqlString.Append("               AND ?????? " + cdvApproval.SelectedValueToQueryString + "\n"); 고객승인

                        if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                        {
                            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
                        }

                    }
                    break;

                case 1:
                    {
                        #region Chart No.1
                        strSqlString.AppendFormat("        SELECT {0} " + "\n", strDate);
                        strSqlString.Append("             , SUM(DECODE(PARITY_CLASS, 'ASSY', 1, 0)) AS ASSY " + "\n");
                        strSqlString.Append("             , SUM(DECODE(PARITY_CLASS, 'CSS', 1, 0)) AS CSS " + "\n");
                        strSqlString.Append("             , SUM(DECODE(PARITY_CLASS, 'EQUIP', 1, 0)) AS EQUIP " + "\n");
                        strSqlString.Append("             , SUM(DECODE(PARITY_CLASS, 'REL', 1, 0)) AS REL             " + "\n");
                        strSqlString.Append("          FROM (SELECT A.PARITY_CLASS  " + "\n");
                        strSqlString.Append("                     , GET_WORK_DATE(A.CREATE_TIME, 'D') AS WORK_DATE " + "\n");
                        strSqlString.Append("                     , GET_WORK_DATE(A.CREATE_TIME, 'W') AS WORK_WEEK " + "\n");
                        strSqlString.Append("                     , GET_WORK_DATE(A.CREATE_TIME, 'M') AS WORK_MONTH   " + "\n");
                        strSqlString.Append("                  FROM CQCMPRTSTS_TMP A  " + "\n");
                        strSqlString.Append("                     , CQCMPRTLOT_TMP B  " + "\n");
                        strSqlString.Append("                     , MWIPMATDEF C  " + "\n");
                        strSqlString.Append("                 WHERE 1=1  " + "\n");
                        strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
                        strSqlString.Append("                   AND A.FACTORY = B.FACTORY  " + "\n");
                        strSqlString.Append("                   AND A.FACTORY = C.FACTORY  " + "\n");
                        strSqlString.Append("                   AND A.MAT_ID = C.MAT_ID  " + "\n");
                        strSqlString.Append("                   AND A.PARITY_ID = B.PARITY_ID  " + "\n");
                        strSqlString.Append("               AND A.CREATE_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("               AND A.PARITY_CLASS " + cdvKind.SelectedValueToQueryString + "\n");
                        strSqlString.Append("               AND A.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                        strSqlString.Append("               AND C.MAT_GRP_3 " + cdvPkg.SelectedValueToQueryString + "\n");
                        strSqlString.Append("               AND A.RESULT_FLAG " + cdvResult.SelectedValueToQueryString + "\n");
                        //strSqlString.Append("               AND ?????? " + cdvApproval.SelectedValueToQueryString + "\n"); 고객승인
                        strSqlString.Append("              ) " + "\n");
                        strSqlString.AppendFormat("     GROUP BY {0} " + "\n", strDate);
                        strSqlString.AppendFormat("     ORDER BY {0} " + "\n", strDate);
                        #endregion
                    }
                    break;

                case 2:
                    {
                        #region Chart No.2
                        strSqlString.Append( "        SELECT OPER " + "\n");

                        for (int i = 0; i < Between; i++)
                        {
                            strSqlString.AppendFormat("             , SUM(DECODE({0}, '{1}', 1, 0)) AS \"{1}\" " + "\n", strDate, selectDate1[i].ToString());
                        }
                        
                        strSqlString.Append("          FROM (SELECT A.OPER  " + "\n");
                        strSqlString.Append("                     , GET_WORK_DATE(A.CREATE_TIME, 'D') AS WORK_DATE " + "\n");
                        strSqlString.Append("                     , GET_WORK_DATE(A.CREATE_TIME, 'W') AS WORK_WEEK " + "\n");
                        strSqlString.Append("                     , GET_WORK_DATE(A.CREATE_TIME, 'M') AS WORK_MONTH   " + "\n");
                        strSqlString.Append("                  FROM CQCMPRTSTS_TMP A  " + "\n");
                        strSqlString.Append("                     , CQCMPRTLOT_TMP B  " + "\n");
                        strSqlString.Append("                     , MWIPMATDEF C  " + "\n");
                        strSqlString.Append("                 WHERE 1=1  " + "\n");
                        strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
                        strSqlString.Append("                   AND A.FACTORY = B.FACTORY  " + "\n");
                        strSqlString.Append("                   AND A.FACTORY = C.FACTORY  " + "\n");
                        strSqlString.Append("                   AND A.MAT_ID = C.MAT_ID  " + "\n");
                        strSqlString.Append("                   AND A.PARITY_ID = B.PARITY_ID  " + "\n");
                        strSqlString.Append("               AND A.CREATE_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("               AND A.PARITY_CLASS " + cdvKind.SelectedValueToQueryString + "\n");
                        strSqlString.Append("               AND A.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                        strSqlString.Append("               AND C.MAT_GRP_3 " + cdvPkg.SelectedValueToQueryString + "\n");
                        strSqlString.Append("               AND A.RESULT_FLAG " + cdvResult.SelectedValueToQueryString + "\n");
                        //strSqlString.Append("               AND ?????? " + cdvApproval.SelectedValueToQueryString + "\n"); 고객승인
                        strSqlString.Append("              ) " + "\n");
                        strSqlString.Append("     GROUP BY OPER " + "\n");
                        strSqlString.Append("     ORDER BY OPER " + "\n");
                        #endregion
                    }
                    break;

                case 3:
                    {
                        #region Chart No.3
                        strSqlString.AppendFormat("        SELECT {0} " + "\n", strDate);
                        strSqlString.Append("             , SUM(DECODE(RESULT_FLAG, 'P', 1, 0)) AS PASS " + "\n");
                        strSqlString.Append("             , SUM(DECODE(RESULT_FLAG, 'R', 1, 0)) AS REJECT " + "\n");
                        strSqlString.Append("          FROM (SELECT A.RESULT_FLAG  " + "\n");
                        strSqlString.Append("                     , GET_WORK_DATE(A.CREATE_TIME, 'D') AS WORK_DATE " + "\n");
                        strSqlString.Append("                     , GET_WORK_DATE(A.CREATE_TIME, 'W') AS WORK_WEEK " + "\n");
                        strSqlString.Append("                     , GET_WORK_DATE(A.CREATE_TIME, 'M') AS WORK_MONTH   " + "\n");
                        strSqlString.Append("                  FROM CQCMPRTSTS_TMP A  " + "\n");
                        strSqlString.Append("                     , CQCMPRTLOT_TMP B  " + "\n");
                        strSqlString.Append("                     , MWIPMATDEF C  " + "\n");
                        strSqlString.Append("                 WHERE 1=1  " + "\n");
                        strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
                        strSqlString.Append("                   AND A.FACTORY = B.FACTORY  " + "\n");
                        strSqlString.Append("                   AND A.FACTORY = C.FACTORY  " + "\n");
                        strSqlString.Append("                   AND A.MAT_ID = C.MAT_ID  " + "\n");
                        strSqlString.Append("                   AND A.RESULT_FLAG <> ' '  " + "\n");
                        strSqlString.Append("                   AND A.PARITY_ID = B.PARITY_ID  " + "\n");
                        strSqlString.Append("               AND A.CREATE_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("               AND A.PARITY_CLASS " + cdvKind.SelectedValueToQueryString + "\n");
                        strSqlString.Append("               AND A.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                        strSqlString.Append("               AND C.MAT_GRP_3 " + cdvPkg.SelectedValueToQueryString + "\n");
                        strSqlString.Append("               AND A.RESULT_FLAG " + cdvResult.SelectedValueToQueryString + "\n");
                        //strSqlString.Append("               AND ?????? " + cdvApproval.SelectedValueToQueryString + "\n"); 고객승인
                        strSqlString.Append("              ) " + "\n");
                        strSqlString.AppendFormat("     GROUP BY {0} " + "\n", strDate);
                        strSqlString.AppendFormat("     ORDER BY {0} " + "\n", strDate);
                        #endregion
                    }
                    break;

                case 4:
                    {

                        #region Chart No.4
                        strSqlString.Append("   SELECT ' ' as temp  " + "\n");
                        strSqlString.Append("        ,DECODE(CNT, 0, 0, (PASS/CNT)*100) AS PASS  " + "\n");
                        strSqlString.Append("        , DECODE(CNT, 0, 0, (FAIL/CNT)*100) AS FAIL  " + "\n");
                        strSqlString.Append("     FROM " + "\n");
                        strSqlString.Append("        ( " + "\n");
                        strSqlString.Append("        SELECT COUNT(*) AS CNT " + "\n");
                        strSqlString.Append("             , SUM(DECODE(RESULT_FLAG, 'P', 1, 0)) AS PASS " + "\n");
                        strSqlString.Append("             , SUM(DECODE(RESULT_FLAG, 'F', 1, 0)) AS FAIL " + "\n");
                        strSqlString.Append("          FROM (SELECT A.RESULT_FLAG  " + "\n");
                        strSqlString.Append("                     , GET_WORK_DATE(A.CREATE_TIME, 'D') AS WORK_DATE " + "\n");
                        strSqlString.Append("                     , GET_WORK_DATE(A.CREATE_TIME, 'W') AS WORK_WEEK " + "\n");
                        strSqlString.Append("                     , GET_WORK_DATE(A.CREATE_TIME, 'M') AS WORK_MONTH   " + "\n");
                        strSqlString.Append("                  FROM CQCMPRTSTS_TMP A  " + "\n");
                        strSqlString.Append("                     , CQCMPRTLOT_TMP B  " + "\n");
                        strSqlString.Append("                     , MWIPMATDEF C  " + "\n");
                        strSqlString.Append("                 WHERE 1=1  " + "\n");
                        strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
                        strSqlString.Append("                   AND A.FACTORY = B.FACTORY  " + "\n");
                        strSqlString.Append("                   AND A.FACTORY = C.FACTORY  " + "\n");
                        strSqlString.Append("                   AND A.MAT_ID = C.MAT_ID  " + "\n");
                        strSqlString.Append("                   AND A.PARITY_ID = B.PARITY_ID  " + "\n");
                        strSqlString.Append("               AND A.CREATE_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("               AND A.PARITY_CLASS " + cdvKind.SelectedValueToQueryString + "\n");
                        strSqlString.Append("               AND A.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                        strSqlString.Append("               AND C.MAT_GRP_3 " + cdvPkg.SelectedValueToQueryString + "\n");
                        strSqlString.Append("               AND A.RESULT_FLAG " + cdvResult.SelectedValueToQueryString + "\n");
                        //strSqlString.Append("               AND ?????? " + cdvApproval.SelectedValueToQueryString + "\n"); 고객승인
                        strSqlString.Append("              ) " + "\n");
                        strSqlString.AppendFormat("     GROUP BY {0} " + "\n", strDate);
                        strSqlString.AppendFormat("     ORDER BY {0} " + "\n", strDate);
                        strSqlString.Append( "       ) " + "\n");
                        #endregion
                    }
                    break;            
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion


        /// <summary>
        /// 5. Chart 생성
        /// </summary>
        /// <param name="rowCount">Row Number</param>
        private void ShowChart(int rowCount)
        {
            DataTable dt_graph = null;

            udcChartFX1.RPT_1_ChartInit();
            udcChartFX1.RPT_2_ClearData();

            // 그래프 설정 //                      
            udcChartFX1.AxisY.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisY2.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisX.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.PointLabels = true;

            switch (cboGraph.SelectedIndex)
            {
                case 0:
                    {
                        #region Chart No.1
                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(1));
                        udcChartFX1.DataSource = dt_graph;

                        udcChartFX1.SerLegBox = true;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        udcChartFX1.Chart3D = true;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                        udcChartFX1.AxisX.Title.Text = "기간";
                        udcChartFX1.AxisY.Title.Text = "number of occurrences";
                        #endregion
                    }
                    break;

                case 1:
                    {
                        #region Chart No.2
                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(2));
                        dt_graph = GetRotatedDataTable(ref dt_graph);
                        udcChartFX1.DataSource = dt_graph;

                        udcChartFX1.SerLegBox = true;
                        udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        udcChartFX1.AxisX.Title.Text = "기간";
                        udcChartFX1.AxisY.Title.Text = "number of occurrences";
                        #endregion
                    }
                    break;

                case 2:
                    {
                        #region Chart No.3
                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(3));
                        //dt_graph = GetRotatedDataTable(ref dt_graph);
                        udcChartFX1.DataSource = dt_graph;

                        udcChartFX1.Stacked = SoftwareFX.ChartFX.Stacked.Normal;
                        udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        udcChartFX1.Chart3D = true;
                        udcChartFX1.SerLegBox = true;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                        udcChartFX1.AxisX.Title.Text = "기간";
                        udcChartFX1.AxisY.Title.Text = "number of occurrences";
                        #endregion
                    }
                    break;

                case 3:
                    {
                        #region Chart No.4
                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(4));
                        dt_graph = GetRotatedDataTable(ref dt_graph);
                        udcChartFX1.DataSource = dt_graph;

                        udcChartFX1.LegendBox = true;
                        udcChartFX1.PointLabels = true;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Pie;
                        udcChartFX1.Chart3D = true;                        
                        #endregion
                    }
                    break;        
            }
        }
        #endregion

        /// <summary>
        /// 버튼 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Button Click Event
        private void btnView_Click(object sender, EventArgs e)
        {
            if (CheckField() == false) return;

            DataTable dt = null;
            GridColumnInit();

            try
            {
                this.Refresh();
                LoadingPopUp.LoadIngPopUpShow(this);
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(0));

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    udcChartFX1.RPT_1_ChartInit();
                    udcChartFX1.RPT_2_ClearData();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                spdData.DataSource = dt;
                spdData.RPT_AutoFit(false);
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
            ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ");
        }
        #endregion


        #region DataTable Pivot Function
        public DataTable GetRotatedDataTable(ref DataTable dt)
        {
            int nColToRow = 0;  // Column Position of dt => Legend Column of dtNew

            DataTable dtNew = new DataTable();
            Object[] dr = null;

            // Get Series Type
            Type type = dt.Columns[1].DataType;

            // Adding Columns
            dtNew.Columns.Add("GUBUN", typeof(String));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dtNew.Columns.Add(dt.Rows[i][0].ToString(), type);
            }

            // Filling Data
            for (int j = nColToRow + 1; j < dt.Columns.Count; j++)
            {
                dr = dtNew.NewRow().ItemArray;
                dr[0] = dt.Columns[j].Caption;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr[i + 1] = dt.Rows[i][j];
                }
                dtNew.Rows.Add(dr);
            }
            return dtNew;
        }
        #endregion

        private void cboGraph_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowChart(0);
        }
    }
}