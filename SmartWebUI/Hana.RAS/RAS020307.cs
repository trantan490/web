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
    /// 클  래  스: RAS020307<br/>
    /// 클래스요약: 순간정지 항목별 점유율<br/>
    /// 작  성  자: 미라콤 양형석<br/>
    /// 최초작성일: 2008-12-31<br/>
    /// 상세  설명: 순간정지 점유율<br/>
    /// 변경  내용: <br/>
    /// 2010-04-15-임종우 : CRASALMDEF 에서 알람 ID 별 DESC 가져오기로 수정...
    /// 2011-03-24-배수민 : 기존쿼리 속도 너무 느려 CSUMALMCNT@RPTTOMES(summary) 테이블 만들어 재작업.
    /// 2012-02-29-김민우 : CSUMALMCNT@RPTTOMES(summary) 테이블에서 MSUMALMCNT 테이블로 변경 (정병주사원 요청)
    /// 2012-03-21-김민우 : MSUMALMCNT@RPTTOMES(summary) 테이블에서 CSUMALMCNT 테이블로 원복 (정병주사원 요청)
    /// </summary>
    public partial class RAS020307 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public RAS020307()
        {
            InitializeComponent();
            udcFromToDate.yesterday_flag = true;
            udcFromToDate.AutoBinding();
            udcFromToDate.DaySelector.SelectedValue = "DAY";
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

    
            spdData.RPT_AddBasicColumn("Rank", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
            spdData.RPT_AddBasicColumn("Error name", 0, 1, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 120);
            //spdData.RPT_AddBasicColumn("Code", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("number of cases", 0, 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("Ratio", 0, 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);
            spdData.RPT_AddBasicColumn("Cumulative occupation", 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);

            //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            
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

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            ////////////////////////////////////////////////////
            // udcDurationPlus에서 정확한 조회시간을 받아오기
            string strFromDate = udcFromToDate.ExactFromDate;
            string strToDate = udcFromToDate.ExactToDate;
            ////////////////////////////////////////////////////


            strSqlString.Append("  SELECT TO_CHAR(RANK_CODE) AS RANK_CODE" + "\n");
            strSqlString.Append("       , ALARM_EQ_ID " + "\n");
            strSqlString.Append("       , ALARM_CNT " + "\n");
            strSqlString.Append("       , RATIO " + "\n");
            strSqlString.Append("       , ADDEDE_RATIO " + "\n");
            strSqlString.Append("   FROM ( " + "\n");
            strSqlString.Append("         SELECT TO_CHAR(RANK_CODE) AS RANK_CODE " + "\n");
            strSqlString.Append("              , ALARM_EQ_ID " + "\n");
            strSqlString.Append("              , ALARM_CNT " + "\n");
            strSqlString.Append("              , RATIO " + "\n");
            strSqlString.Append("              , ADDEDE_RATIO " + "\n");
            strSqlString.Append("           FROM ( " + "\n");
            strSqlString.Append("                 SELECT RANK_CODE " + "\n");
            strSqlString.Append("                      , ALARM_EQ_ID " + "\n");
            strSqlString.Append("                      , ALARM_CNT " + "\n");
            strSqlString.Append("                      , RATIO " + "\n");
            strSqlString.Append("                      , SUM(RATIO) OVER (ORDER BY RANK_CODE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS ADDEDE_RATIO " + "\n");
            strSqlString.Append("                    FROM ( " + "\n");
            strSqlString.Append("                          SELECT ROW_NUMBER() OVER(ORDER BY ALARM_CNT DESC) AS RANK_CODE " + "\n");
            strSqlString.Append("                               , ALARM_EQ_ID " + "\n");
            strSqlString.Append("                               , ALARM_CNT " + "\n");
            strSqlString.Append("                               , ROUND((RATIO_TO_REPORT(ALARM_CNT) OVER())*100, 2) AS RATIO " + "\n");
            strSqlString.Append("                          FROM ( " + "\n");
            strSqlString.Append("                                SELECT CNT.ALARM_EQ_ID " + "\n");
            strSqlString.Append("                                     , SUM(CNT.ALARM_CNT) AS ALARM_CNT " + "\n");
            strSqlString.Append("                                 FROM CSUMALMCNT@RPTTOMES CNT, MRASRESDEF RES " + "\n");
            strSqlString.Append("                                WHERE 1=1 " + "\n");
            strSqlString.Append("                                  AND CNT.WORK_DAY BETWEEN '" + strFromDate + "' AND '" + strToDate + "' " + "\n");
            strSqlString.Append("                                  AND CNT.FACTORY = RES.FACTORY " + "\n");
            strSqlString.Append("                                  AND CNT.RES_ID = RES.RES_ID " + "\n");

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

            strSqlString.Append("                             GROUP BY CNT.ALARM_EQ_ID " + "\n");
            strSqlString.Append("                               ) " + "\n");
            strSqlString.Append("                         ) " + "\n");
            strSqlString.Append("                   WHERE 1=1 " + "\n");
            strSqlString.Append("                     AND RANK_CODE < 51  " + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("                UNION ALL  " + "\n");
            strSqlString.Append("        ( " + "\n");
            strSqlString.Append("         SELECT ' ' " + "\n");
            strSqlString.Append("              , 'OTHER' " + "\n");
            strSqlString.Append("              , NVL(SUM(ALARM_CNT), 0) AS ALARM_CNT " + "\n");
            strSqlString.Append("              , NVL(SUM(RATIO), 0) AS RATIO " + "\n");
            strSqlString.Append("              , 100 " + "\n");
            strSqlString.Append("           FROM ( " + "\n");
            strSqlString.Append("                 SELECT ROW_NUMBER() OVER(ORDER BY ALARM_CNT DESC) AS RANK_CODE " + "\n");
            strSqlString.Append("                      , ALARM_EQ_ID " + "\n");
            strSqlString.Append("                      , ALARM_CNT " + "\n");
            strSqlString.Append("                      , ROUND((RATIO_TO_REPORT(ALARM_CNT) OVER())*100, 2) AS RATIO  " + "\n");
            strSqlString.Append("                   FROM ( " + "\n");
            strSqlString.Append("                         SELECT CNT.ALARM_EQ_ID " + "\n");
            strSqlString.Append("                              , SUM(CNT.ALARM_CNT) AS ALARM_CNT " + "\n");
            strSqlString.Append("                           FROM CSUMALMCNT@RPTTOMES CNT, MRASRESDEF RES  " + "\n");
            strSqlString.Append("                          WHERE 1=1 " + "\n");
            strSqlString.Append("                            AND CNT.WORK_DAY BETWEEN '" + strFromDate + "' AND '" + strToDate + "' " + "\n");
            strSqlString.Append("                            AND CNT.FACTORY = RES.FACTORY " + "\n");
            strSqlString.Append("                            AND CNT.RES_ID = RES.RES_ID " + "\n");

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

            strSqlString.Append("                       GROUP BY CNT.ALARM_EQ_ID " + "\n");
            strSqlString.Append("                        ) " + "\n");
            strSqlString.Append("                ) " + "\n");
            strSqlString.Append("   WHERE 1=1 " + "\n");
            strSqlString.Append("     AND RANK_CODE > 50   " + "\n");
            strSqlString.Append("         ) " + "\n");
            strSqlString.Append("       ) " + "\n");



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

            DataTable dt = (DataTable)spdData.DataSource;
            rowCount = dt.Rows.Count;
            if (rowCount < 1)
                return;

            udcChartFX1.RPT_3_OpenData(2, rowCount - 1);
            int[] cnt_rows = new Int32[rowCount - 1];

            for (int i = 1; i < cnt_rows.Length + 1; i++)
            {
                cnt_rows[i - 1] = i;
            }

            // 건수
            double cnt = udcChartFX1.RPT_4_AddData(spdData, cnt_rows, new int[] { 9 }, SeriseType.Column);

            // 누적점유율
            double percent = udcChartFX1.RPT_4_AddData(spdData, cnt_rows, new int[] { 11 }, SeriseType.Column);

            udcChartFX1.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 0, 1, "건수", AsixType.Y, DataTypes.Initeger, cnt * 1.2);
            udcChartFX1.RPT_6_SetGallery(ChartType.Line, 1, 1, "누적점유", AsixType.Y2, DataTypes.Initeger, percent);

            //각 Serise별로 동일한 타입을 사용할 경우
            //udcChartFX1.RPT_6_SetGallery(ChartType.Bar, "[단위 : sls]", AsixType.Y, DataTypes.Initeger, yield * 1.2);

            udcChartFX1.RPT_7_SetXAsixTitleUsingSpread(spdData, cnt_rows, 8);
            udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "건수", "누적점유" }, SoftwareFX.ChartFX.Docked.Top);

            // 기타 설정
            udcChartFX1.PointLabels = false;
            udcChartFX1.Series[1].LineWidth = 2;
            udcChartFX1.AxisY.LabelsFormat.Decimals = 0;
            udcChartFX1.AxisY.DataFormat.Decimals = 0;
            udcChartFX1.AxisY2.DataFormat.Format = SoftwareFX.ChartFX.AxisFormat.Number;
            udcChartFX1.AxisY2.DataFormat.Decimals = 2;
            udcChartFX1.AxisX.LabelAngle = 90;
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
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 2, null, null);
                //spdData.DataSource = dt;

                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                //3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 3, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit4
                spdData.RPT_AutoFit(false);

                //spdData.RPT_FillColumnData(14, new string[] { "Yield", "In Qty", "Out Qty", "Loss" });

                // 누적점유율 수정
                spdData.ActiveSheet.Cells[0, 4].Value = 100.00;

                //ShowChart(0);
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