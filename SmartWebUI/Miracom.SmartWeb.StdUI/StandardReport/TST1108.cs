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

using System.Windows.Forms.DataVisualization.Charting;

namespace Miracom.SmartWeb.UI
{
    public partial class TST1108 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: TST1108<br/>
        /// 클래스요약: MENU LOG<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2010-08-02<br/>
        /// 상세  설명: MENU LOG 기록의 데이터를 표시한다.<br/>
        /// 변경  내용: <br/>
        /// 2012-06-08-임종우 : 쿼리 수정
        
        /// </summary>
        public TST1108()
        {
            InitializeComponent();

            //SortInit();
            GridColumnInit(); //헤더 한줄짜리 
        }

        #region SortInit


        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Date", "SHIP_DATE", "SHIP_DATE", "(SELECT TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_WEEK,2,'0') FROM MWIPCALDEF WHERE SYS_DATE = TAT.SHIP_DATE AND CALENDAR_ID='HM') AS SHIP_DATE", "(SELECT TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_MONTH,2,'0') FROM MWIPCALDEF WHERE SYS_DATE = TAT.SHIP_DATE AND CALENDAR_ID='HM') AS SHIP_DATE", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1", true);
            //// 2009.06.15 Bongjun Park, PinType 추가
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PinType", "MAT_CMF_10", "MAT_CMF_10", "MAT_CMF_10", "MAT_CMF_10", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
        }

        #endregion

        #region 한줄헤더생성

        /// <summary>
        /// 한줄헤더생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {
            try
            {
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("Function Group", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Function Name", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Function Code", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("View Count", 0, 3, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 100);
                spdData.RPT_AddBasicColumn("View Percent", 0, 4, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Double2, 100); 

                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        #endregion

        #region 조회

        /// <summary>
        /// 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            GridColumnInit();
            
            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                spdData_Sheet1.RowCount = 0;
                this.Refresh();
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

                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1 , 3, null, null, btnSort);

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 3, 0, 1, true, Align.Center, VerticalAlign.Center);


                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);
                //--------------------------------------------

                               //Chart 생성
                //if (spdData.ActiveSheet.RowCount > 0)
                //    ShowChart();
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

        
        //#region MakeSqlChart
        //private string MakeSqlChart()
        //{
        //    StringBuilder strSqlString = new StringBuilder();

        //    // 그룹 선택하여 조회하면 5위까지 표시하고 그외는 Others로 표시함..목록이 너무 많기에..
        //    if (cdvFuncGrp.Text.TrimEnd() != "")
        //    {
        //        strSqlString.AppendFormat("SELECT FUNC_DESC" + "\n");
        //        strSqlString.AppendFormat("     , ROUND(RATIO_TO_REPORT(SUM(CNT)) OVER () * 100, 2) AS PER " + "\n");
        //        strSqlString.AppendFormat("  FROM (" + "\n");
        //        strSqlString.AppendFormat("        SELECT CASE WHEN ROW_NO < 6 THEN FUNC_DESC" + "\n");
        //        strSqlString.AppendFormat("                    ELSE 'Others'" + "\n");
        //        strSqlString.AppendFormat("               END FUNC_DESC" + "\n");
        //        strSqlString.AppendFormat("             , CNT             " + "\n");
        //        strSqlString.AppendFormat("          FROM (" + "\n");
        //        strSqlString.AppendFormat("                SELECT INF.FUNC_DESC " + "\n");
        //        strSqlString.AppendFormat("                     , COUNT(*) AS CNT                  " + "\n");
        //        strSqlString.AppendFormat("                     , ROW_NUMBER() OVER(ORDER BY COUNT(*) DESC) AS ROW_NO" + "\n");
        //        strSqlString.AppendFormat("                  FROM RWEBFUNLOG HIS " + "\n");
        //        strSqlString.AppendFormat("                     , RWEBFUNDEF INF " + "\n");
        //        strSqlString.AppendFormat("                     , ( " + "\n");
        //        strSqlString.AppendFormat("                        SELECT * " + "\n");
        //        strSqlString.AppendFormat("                          FROM RWEBGRPFUN  " + "\n");
        //        strSqlString.AppendFormat("                         WHERE FACTORY = 'SYSTEM' " + "\n");
        //        strSqlString.AppendFormat("                           AND SEC_GRP_ID = 'ADMIN_GROUP' " + "\n");
        //        strSqlString.AppendFormat("                           AND FUNC_NAME <> ' ' " + "\n");
        //        strSqlString.AppendFormat("                       ) GRP " + "\n");
        //        strSqlString.AppendFormat("                 WHERE 1=1 " + "\n");
        //        strSqlString.AppendFormat("                   AND HIS.FUNC_NAME = INF.FUNC_NAME " + "\n");
        //        strSqlString.AppendFormat("                   AND HIS.FUNC_NAME = GRP.FUNC_NAME " + "\n");
        //        strSqlString.AppendFormat("                   AND HIS.VIEW_TIME BETWEEN '{0}' AND '{1}' " + "\n", StartDate(), EndDate());
        //        strSqlString.AppendFormat("                   AND GRP.FUNC_GRP_ID = '" + cdvFuncGrp.Text + "'" + "\n");
        //        strSqlString.AppendFormat("                 GROUP BY INF.FUNC_DESC " + "\n");
        //        strSqlString.AppendFormat("               )         " + "\n");
        //        strSqlString.AppendFormat("       )" + "\n");
        //        strSqlString.AppendFormat(" GROUP BY FUNC_DESC" + "\n");
        //        strSqlString.AppendFormat(" ORDER BY DECODE(FUNC_DESC, 'Others', 10) DESC, PER DESC" + "\n"); 
        //    }
        //    else
        //    {
        //        strSqlString.AppendFormat("SELECT HIS.FUNC_GROUP " + "\n");
        //        strSqlString.AppendFormat("     , ROUND(RATIO_TO_REPORT(COUNT(*)) OVER () * 100, 2) AS PER " + "\n");
        //        strSqlString.AppendFormat("  FROM RWEBFUNLOG HIS " + "\n");
        //        strSqlString.AppendFormat(" WHERE 1=1 " + "\n");
        //        strSqlString.AppendFormat("   AND HIS.VIEW_TIME BETWEEN '{0}' AND '{1}' " + "\n", StartDate(), EndDate());
        //        strSqlString.AppendFormat(" GROUP BY HIS.FUNC_GROUP" + "\n");
        //        strSqlString.AppendFormat(" ORDER BY PER DESC" + "\n");
        //    }

        //    //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
        //    //{
        //    //    System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
        //    //}

        //    return strSqlString.ToString();
        //}
        //#endregion MakeSqlChart

        #region MakeSqlString
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();
                       
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            strSqlString.AppendFormat("SELECT GRP.FUNC_GRP_ID " + "\n");
            strSqlString.AppendFormat("     , INF.FUNC_DESC " + "\n");
            strSqlString.AppendFormat("     , INF.FUNC_NAME " + "\n");
            strSqlString.AppendFormat("     , COUNT(*) AS CNT" + "\n");
            strSqlString.AppendFormat("     , ROUND(RATIO_TO_REPORT(COUNT(*)) OVER () * 100, 2) AS PER " + "\n");
            strSqlString.AppendFormat("  FROM RWEBFUNLOG HIS " + "\n");
            strSqlString.AppendFormat("     , RWEBFUNDEF INF " + "\n");
            strSqlString.AppendFormat("     , ( " + "\n");
            strSqlString.AppendFormat("        SELECT * " + "\n");
            strSqlString.AppendFormat("          FROM RWEBGRPFUN  " + "\n");
            strSqlString.AppendFormat("         WHERE FACTORY = 'SYSTEM' " + "\n");
            strSqlString.AppendFormat("           AND SEC_GRP_ID = 'ADMIN_GROUP' " + "\n");
            strSqlString.AppendFormat("           AND FUNC_NAME <> ' ' " + "\n");
            strSqlString.AppendFormat("       ) GRP " + "\n");
            strSqlString.AppendFormat(" WHERE 1=1 " + "\n");
            strSqlString.AppendFormat("   AND HIS.FUNC_NAME = INF.FUNC_NAME " + "\n");
            strSqlString.AppendFormat("   AND HIS.FUNC_NAME = GRP.FUNC_NAME " + "\n");
            strSqlString.AppendFormat("   AND HIS.VIEW_TIME BETWEEN '{0}' AND '{1}' " + "\n", StartDate(), EndDate());
            strSqlString.AppendFormat("   AND HIS.USER_ID NOT IN ('ADMIN', 'WEBADMIN', '1050410') " + "\n");

            // 그룹 검색 여부에 따른 쿼리문
            if (cdvFuncGrp.Text.TrimEnd() != "")
            {
                strSqlString.AppendFormat("   AND GRP.FUNC_GRP_ID = '" + cdvFuncGrp.Text + "'" + "\n");
                strSqlString.AppendFormat(" GROUP BY GRP.FUNC_GRP_ID, INF.FUNC_DESC, INF.FUNC_NAME " + "\n");
                strSqlString.AppendFormat(" ORDER BY CNT DESC " + "\n");
            }
            else
            {
                strSqlString.AppendFormat(" GROUP BY GRP.FUNC_GRP_ID, INF.FUNC_DESC, INF.FUNC_NAME " + "\n");
                strSqlString.AppendFormat(" ORDER BY GRP.FUNC_GRP_ID, CNT DESC, INF.FUNC_DESC " + "\n");
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
                        
            return strSqlString.ToString();
        }

        #endregion

        #region ShowChart

        private void ShowChart(DataTable dt)
        {
            udcMSChart1.RPT_1_ChartInit();
            udcMSChart1.RPT_2_ClearData();
            
            udcMSChart1.RPT_3_OpenData(1, dt.Rows.Count);

            int[] total_rows = new Int32[dt.Rows.Count];
            //int[] columnsHeader = new Int32[dtChart.Rows.Count + 1];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                total_rows[i] = 0 + i;
            }
                        
            double max1 = udcMSChart1.RPT_4_AddData(dt, total_rows, new int[] { 1 }, SeriseType.Rows, DataTypes.Initeger);

            //각 Serise별로 다른 타입을 사용할 경우
            udcMSChart1.RPT_6_SetGallery(SeriesChartType.Line, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
                        
            udcMSChart1.Series[0].Color = System.Drawing.Color.Black;
            udcMSChart1.Series[0].IsVisibleInLegend = false;
            udcMSChart1.Series[0].IsValueShownAsLabel = true;
            udcMSChart1.Series[0].MarkerStyle = MarkerStyle.Circle;
            udcMSChart1.Series[0].MarkerSize = 8;
            
            udcMSChart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;


            //udcMSChart1.ChartAreas[0].AxisX.Title = "Menu Log Report";
        }
        #endregion

        #endregion

        #region ToExcel

        /// <summary>
        /// ToExcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1, this.lblTitle.Text, null, null);
            spdData.ExportExcel();
        }

        #endregion

         private string StartDate()
        {
            string FromDate = null;

            if (optYesterday.Checked == true)
            {
                FromDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optLastWeek.Checked == true)
            {
                FromDate = DateTime.Now.AddDays(-(int)(DateTime.Now.DayOfWeek) - 7).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optLastMonth.Checked == true)
            {
                FromDate = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd").Substring(0, 6) + "01";
            }
            else if (optPeriod.Checked == true)
            {
                FromDate = dtpFromDate.Value.ToString("yyyyMMdd");
            }
            else if (optToday.Checked == true)
            {
                FromDate = DateTime.Now.ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optThisWeek.Checked == true)
            {
                FromDate = DateTime.Now.AddDays(-(int)(DateTime.Now.DayOfWeek)).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optThisMonth.Checked == true)
            {
                FromDate = DateTime.Now.ToString("yyyyMMdd").Substring(0, 6) + "01";
            }
            return FromDate + "000000";
        }

        private string EndDate()
        {
            string ToDate = null;

            if (optYesterday.Checked == true)
            {
                ToDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optLastWeek.Checked == true)
            {
                ToDate = DateTime.Now.AddDays(-(int)(DateTime.Now.DayOfWeek) - 1).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optLastMonth.Checked == true)
            {
                ToDate = DateTime.Now.AddDays(-(DateTime.Now.Day)).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optPeriod.Checked == true)
            {
                ToDate = dtpToDate.Value.ToString("yyyyMMdd");
            }
            else if (optToday.Checked == true)
            {
                ToDate = DateTime.Now.ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optThisWeek.Checked == true)
            {
                ToDate = DateTime.Now.AddDays(-(int)(DateTime.Now.DayOfWeek) + 6).ToString("yyyyMMdd").Substring(0, 8);
            }
            else if (optThisMonth.Checked == true)
            {
                DataTable dt1 = null;

                string temp = DateTime.Now.ToString("yyyyMM").ToString();
                ToDate = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + temp + "', 'YYYYMM')), 'YYYYMMDD') ||'215959' FROM DUAL)";

                dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", ToDate);

                temp = dt1.Rows[0][0].ToString();
                return temp;
            }
            return ToDate + "235959";
        }

        public static void ViewFuncGrpList(Control objControl)
        {
            DataTable dt = null;
            ListViewItem TmpItmX = null;
            ListViewItem itmX = null;
            int iCnt;

            string QueryCond = null;

            dt = CmnFunction.oComm.GetFuncDataTable("VIEW_FUNCGRP_LIST", QueryCond);
            if (objControl is ListView)
            {
                for (iCnt = 0; iCnt < dt.Rows.Count; iCnt++)
                {
                    if (iCnt == 0)
                    {
                        TmpItmX = new ListViewItem(" ");
                        ((ListView)objControl).Items.Add(TmpItmX);
                    }

                    itmX = new ListViewItem(dt.Rows[iCnt]["FUNC_GRP_ID"].ToString().TrimEnd());
                    ((ListView)objControl).Items.Add(itmX);
                }
            }
        }

        private void cdvFuncGrp_ButtonPress(object sender, EventArgs e)
        {
            cdvFuncGrp.Init();
            CmnInitFunction.InitListView(cdvFuncGrp.GetListView);
            cdvFuncGrp.Columns.Add("FUNC_GRP_ID", 100, HorizontalAlignment.Left);
            cdvFuncGrp.SelectedSubItemIndex = 0;

            ViewFuncGrpList(cdvFuncGrp.GetListView);
        }

        private void optPeriod_CheckedChanged(object sender, EventArgs e)
        {
            if (optPeriod.Checked == true)
            {
                dtpFromDate.Visible = true;
                dtpToDate.Visible = true;
            }
            else
            {
                dtpFromDate.Visible = false;
                dtpToDate.Visible = false;
            }
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sFunction = null;

            sFunction = spdData.ActiveSheet.Cells[e.Row, 2].Text;

            DataTable dtChart = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlChart(sFunction));

            if (dtChart != null && dtChart.Rows.Count > 0)
            {
                ShowChart(dtChart);
            }            
        }

        private string MakeSqlChart(string sFunction)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("SELECT A.VIEW_DATE " + "\n");
            strSqlString.AppendFormat("     , NVL(B.CNT, 0) AS CNT " + "\n");
            strSqlString.AppendFormat("  FROM (" + "\n");
            strSqlString.AppendFormat("        SELECT TO_CHAR(TO_DATE('{0}', 'YYYYMMDDHH24MISS') + ((ROWNUM-1)), 'YYYYMMDD') AS VIEW_DATE" + "\n", StartDate());
            strSqlString.AppendFormat("          FROM DUAL" + "\n");
            strSqlString.AppendFormat("       CONNECT BY LEVEL <= (TO_DATE('{0}', 'YYYYMMDDHH24MISS') - TO_DATE('{1}', 'YYYYMMDDHH24MISS')) + 1" + "\n", EndDate(), StartDate());
            strSqlString.AppendFormat("       ) A" + "\n");
            strSqlString.AppendFormat("     , (" + "\n");
            strSqlString.AppendFormat("        SELECT SUBSTR(VIEW_TIME, 1, 8) AS VIEW_DATE " + "\n");
            strSqlString.AppendFormat("             , COUNT(*) AS CNT " + "\n");
            strSqlString.AppendFormat("          FROM RWEBFUNLOG" + "\n");
            strSqlString.AppendFormat("         WHERE 1=1 " + "\n");
            strSqlString.AppendFormat("           AND VIEW_TIME BETWEEN '{0}' AND '{1}' " + "\n", StartDate(), EndDate());
            strSqlString.AppendFormat("           AND FUNC_NAME = '" + sFunction + "'" + "\n");
            strSqlString.AppendFormat("           AND USER_ID NOT IN ('ADMIN', 'WEBADMIN', '1050410')" + "\n");
            strSqlString.AppendFormat("         GROUP BY SUBSTR(VIEW_TIME, 1, 8)" + "\n");
            strSqlString.AppendFormat("       ) B" + "\n");
            strSqlString.AppendFormat(" WHERE 1=1 " + "\n");
            strSqlString.AppendFormat("   AND A.VIEW_DATE = B.VIEW_DATE(+)" + "\n");
            strSqlString.AppendFormat(" ORDER BY VIEW_DATE" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

    }
}
