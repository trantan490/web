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

namespace Hana.TRN
{
    public partial class TRN090101 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        string[] dayArry = new string[3];
        string[] dayArry2 = new string[3];

        string[] dayArry2_1 = new string[3];
        string[] dayArry2_2 = new string[3];
        string[] dayArry2_3 = new string[3];
        string[] dayArry2_4 = new string[3];
        string[] dayArry2_5 = new string[3];
        string[] dayArry2_6 = new string[3];
        string[] dayArry2_7 = new string[3];

        decimal jindoPer;
        decimal jindoPer1;
        decimal jindoPer2;
        decimal jindoPer3;
        decimal jindoPer4;
        decimal jindoPer5;
        decimal jindoPer6;
        decimal jindoPer7;

        string strDate1;
        string strDate2;
        string strDate3;
        string strDate4;
        string strDate5;
        string strDate6;
        string strDate7;

        string year1;
        string year2;
        string year3;
        string year4;
        string year5;
        string year6;
        string year7;

        string month1;
        string month2;
        string month3;
        string month4;
        string month5;
        string month6;
        string month7;

        string lastMonth1;
        string lastMonth2;
        string lastMonth3;
        string lastMonth4;
        string lastMonth5;
        string lastMonth6;
        string lastMonth7;

        string startDate1;
        string startDate2;
        string startDate3;
        string startDate4;
        string startDate5;
        string startDate6;
        string startDate7;

        string getEndDate1;
        string getEndDate2;
        string getEndDate3;
        string getEndDate4;
        string getEndDate5;
        string getEndDate6;
        string getEndDate7;

        // 컬럼에 보여줄 날짜 값
        string[] dayArry_col = new string[7];
        string month_col;

        /// <summary>
        /// 클  래  스: TRN010101<br/>
        /// 클래스요약: ASSY, TEST 달성율<br/>
        /// 작  성  자: 김민우<br/>
        /// 최초작성일: 2010-07-07<br/>
        /// 상세  설명: ASSY, TEST 달성율<br/>
        /// 변경  내용: <br/> 
        
        /// </summary>
        public TRN090101()
        {
            InitializeComponent();
            SortInit();
            cdvDate.Value = DateTime.Now.AddDays(-1);
            GridColumnInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.cdvLotType.sFactory = GlobalVariable.gsAssyDefaultFactory;
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            //if (cdvFactory.Text.TrimEnd() == "")
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
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

            LabelTextChange();

            String ss = DateTime.Now.ToString("MM-dd");

            try
            {
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(month_col+" 월", 0, 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 90);
                spdData.RPT_AddBasicColumn(dayArry_col[0], 0, 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 100);
                spdData.RPT_AddBasicColumn(dayArry_col[1], 0, 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 100);
                spdData.RPT_AddBasicColumn(dayArry_col[2], 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 100);
                spdData.RPT_AddBasicColumn(dayArry_col[3], 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 100);
                spdData.RPT_AddBasicColumn(dayArry_col[4], 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 100);
                spdData.RPT_AddBasicColumn(dayArry_col[5], 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 100);
                spdData.RPT_AddBasicColumn(dayArry_col[6], 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 100);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {

        }
        #endregion


        #region SQL 쿼리 Build
        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1;
            string QueryCond2;
            string start_date;
            string yesterday;
            string date;
            string month;
            string year;
            string lastMonth;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            //int remain = Convert.ToInt32(lblLastDay.Text.Substring(0,2)) - Convert.ToInt32(lblToday.Text.Substring(0,2)) + 1;

            date = cdvDate.SelectedValue();
            DateTime Select_date;
            Select_date = DateTime.Parse(cdvDate.Text.ToString());

            year = Select_date.ToString("yyyy");
            month = Select_date.ToString("yyyyMM");
            start_date = month + "01";
            yesterday = Select_date.AddDays(-1).ToString("yyyyMMdd");
            lastMonth = Select_date.AddMonths(-1).ToString("yyyyMM"); // 지난달

            // 지난달 마지막일 구하기
            DataTable dt1 = null;
            string Last_Month_Last_day = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + lastMonth + "', 'YYYYMM')),'YYYYMMDD') FROM DUAL)";
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", Last_Month_Last_day);
            Last_Month_Last_day = dt1.Rows[0][0].ToString();


            // 달의 마지막일 구하기
            DataTable dt2 = null;
            string last_day = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + month + "', 'YYYYMM')),'YYYYMMDD') FROM DUAL)";
            dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", last_day);
            last_day = dt2.Rows[0][0].ToString();

            // 지난주차의 마지막일 가져오기
            dt1 = null;
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(year, Select_date.ToString("yyyyMMdd")));
            string Lastweek_lastday = dt1.Rows[0][0].ToString();


            // -1일 지난주차의 마지막일 가져오기
            dt1 = null;
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(year1, strDate1));
            string Lastweek_lastday1 = dt1.Rows[0][0].ToString();

            // -2일 지난주차의 마지막일 가져오기
            dt1 = null;
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(year2, strDate2));
            string Lastweek_lastday2 = dt1.Rows[0][0].ToString();

            // -3일 지난주차의 마지막일 가져오기
            dt1 = null;
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(year3, strDate3));
            string Lastweek_lastday3 = dt1.Rows[0][0].ToString();

            // -4일 지난주차의 마지막일 가져오기
            dt1 = null;
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(year4, strDate4));
            string Lastweek_lastday4 = dt1.Rows[0][0].ToString();

            // -5일 지난주차의 마지막일 가져오기
            dt1 = null;
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(year5, strDate5));
            string Lastweek_lastday5 = dt1.Rows[0][0].ToString();

            // -6일 지난주차의 마지막일 가져오기
            dt1 = null;
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(year6, strDate6));
            string Lastweek_lastday6 = dt1.Rows[0][0].ToString();

            // -7일 지난주차의 마지막일 가져오기
            dt1 = null;
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(year7, strDate7));
            string Lastweek_lastday7 = dt1.Rows[0][0].ToString();

            strSqlString.Append("SELECT A.CUSTOMER, TOTAL.JINDO, A.JINDO, B.JINDO, C.JINDO, D.JINDO, E.JINDO, F.JINDO, G.JINDO" + "\n");
            strSqlString.Append(" FROM( " + "\n");
            strSqlString.Append("      SELECT CUSTOMER, ROUND((SUM(ASSY_MON)/SUM(TARGET_MON))* 100, 2) JINDO" + "\n");
            strSqlString.Append("      FROM( " + "\n");
            strSqlString.Append("           SELECT CUSTOMER, (SUM(PLAN_QTY) * " + jindoPer + " ) / 100 AS TARGET_MON ,SUM(ASSY_QTY) AS ASSY_MON" + "\n");
            strSqlString.Append("             FROM RSUMTRNGOL " + "\n");
            strSqlString.Append("            WHERE 1=1 " + "\n");
            strSqlString.Append("             AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("             AND WORK_DATE = '" + cdvDate.SelectedValue() + "'" + "\n");
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
            {
                strSqlString.AppendFormat("       AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
            }
            else
            {
                strSqlString.Append("           AND CUSTOMER IN ('SE','HX') " + "\n");
            }

            strSqlString.Append("           GROUP BY CUSTOMER ) " + "\n");
            strSqlString.Append("           WHERE TARGET_MON <> 0 " + "\n");
            strSqlString.Append("           GROUP BY CUSTOMER " + "\n");
            strSqlString.Append("           ) TOTAL , " + "\n");

            strSqlString.Append("    ( SELECT CUSTOMER, ROUND((SUM(ASSY_MON)/SUM(TARGET_MON))* 100, 2) JINDO" + "\n");
            strSqlString.Append("      FROM( " + "\n");
            strSqlString.Append("           SELECT CUSTOMER, (SUM(PLAN_QTY) * " + jindoPer1 + " ) / 100 AS TARGET_MON ,SUM(ASSY_QTY) AS ASSY_MON" + "\n");
            strSqlString.Append("             FROM RSUMTRNGOL " + "\n");
            strSqlString.Append("            WHERE 1=1 " + "\n");
            strSqlString.Append("             AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("             AND WORK_DATE = '" + strDate1 + "'" + "\n");
            strSqlString.Append("           GROUP BY CUSTOMER ) " + "\n");
            strSqlString.Append("           WHERE TARGET_MON <> 0 " + "\n");
            strSqlString.Append("           GROUP BY CUSTOMER " + "\n");
            strSqlString.Append("           ) A , " + "\n");

            strSqlString.Append("    ( SELECT CUSTOMER, ROUND((SUM(ASSY_MON)/SUM(TARGET_MON))* 100, 2) JINDO" + "\n");
            strSqlString.Append("      FROM( " + "\n");
            strSqlString.Append("           SELECT CUSTOMER, (SUM(PLAN_QTY) * " + jindoPer2 + " ) / 100 AS TARGET_MON ,SUM(ASSY_QTY) AS ASSY_MON" + "\n");
            strSqlString.Append("             FROM RSUMTRNGOL " + "\n");
            strSqlString.Append("            WHERE 1=1 " + "\n");
            strSqlString.Append("             AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("             AND WORK_DATE = '" + strDate2 + "'" + "\n");
            strSqlString.Append("           GROUP BY CUSTOMER ) " + "\n");
            strSqlString.Append("           WHERE TARGET_MON <> 0 " + "\n");
            strSqlString.Append("           GROUP BY CUSTOMER " + "\n");
            strSqlString.Append("           ) B , " + "\n");

            strSqlString.Append("    ( SELECT CUSTOMER, ROUND((SUM(ASSY_MON)/SUM(TARGET_MON))* 100, 2) JINDO" + "\n");
            strSqlString.Append("      FROM( " + "\n");
            strSqlString.Append("           SELECT CUSTOMER, (SUM(PLAN_QTY) * " + jindoPer3 + " ) / 100 AS TARGET_MON ,SUM(ASSY_QTY) AS ASSY_MON" + "\n");
            strSqlString.Append("             FROM RSUMTRNGOL " + "\n");
            strSqlString.Append("            WHERE 1=1 " + "\n");
            strSqlString.Append("             AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("             AND WORK_DATE = '" + strDate3 + "'" + "\n");
            strSqlString.Append("           GROUP BY CUSTOMER ) " + "\n");
            strSqlString.Append("           WHERE TARGET_MON <> 0 " + "\n");
            strSqlString.Append("           GROUP BY CUSTOMER " + "\n");
            strSqlString.Append("           ) C , " + "\n");

            strSqlString.Append("    ( SELECT CUSTOMER, ROUND((SUM(ASSY_MON)/SUM(TARGET_MON))* 100, 2) JINDO" + "\n");
            strSqlString.Append("      FROM( " + "\n");
            strSqlString.Append("           SELECT CUSTOMER, (SUM(PLAN_QTY) * " + jindoPer4 + " ) / 100 AS TARGET_MON ,SUM(ASSY_QTY) AS ASSY_MON" + "\n");
            strSqlString.Append("             FROM RSUMTRNGOL " + "\n");
            strSqlString.Append("            WHERE 1=1 " + "\n");
            strSqlString.Append("             AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("             AND WORK_DATE = '" + strDate4 + "'" + "\n");
            strSqlString.Append("           GROUP BY CUSTOMER ) " + "\n");
            strSqlString.Append("           WHERE TARGET_MON <> 0 " + "\n");
            strSqlString.Append("           GROUP BY CUSTOMER " + "\n");
            strSqlString.Append("           ) D , " + "\n");

            strSqlString.Append("    ( SELECT CUSTOMER, ROUND((SUM(ASSY_MON)/SUM(TARGET_MON))* 100, 2) JINDO" + "\n");
            strSqlString.Append("      FROM( " + "\n");
            strSqlString.Append("           SELECT CUSTOMER, (SUM(PLAN_QTY) * " + jindoPer5 + " ) / 100 AS TARGET_MON ,SUM(ASSY_QTY) AS ASSY_MON" + "\n");
            strSqlString.Append("             FROM RSUMTRNGOL " + "\n");
            strSqlString.Append("            WHERE 1=1 " + "\n");
            strSqlString.Append("             AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("             AND WORK_DATE = '" + strDate5 + "'" + "\n");
            strSqlString.Append("           GROUP BY CUSTOMER ) " + "\n");
            strSqlString.Append("           WHERE TARGET_MON <> 0 " + "\n");
            strSqlString.Append("           GROUP BY CUSTOMER " + "\n");
            strSqlString.Append("           ) E , " + "\n");

            strSqlString.Append("    ( SELECT CUSTOMER, ROUND((SUM(ASSY_MON)/SUM(TARGET_MON))* 100, 2) JINDO" + "\n");
            strSqlString.Append("      FROM( " + "\n");
            strSqlString.Append("           SELECT CUSTOMER, (SUM(PLAN_QTY) * " + jindoPer6 + " ) / 100 AS TARGET_MON ,SUM(ASSY_QTY) AS ASSY_MON" + "\n");
            strSqlString.Append("             FROM RSUMTRNGOL " + "\n");
            strSqlString.Append("            WHERE 1=1 " + "\n");
            strSqlString.Append("             AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("             AND WORK_DATE = '" + strDate6 + "'" + "\n");
            strSqlString.Append("           GROUP BY CUSTOMER ) " + "\n");
            strSqlString.Append("           WHERE TARGET_MON <> 0 " + "\n");
            strSqlString.Append("           GROUP BY CUSTOMER " + "\n");
            strSqlString.Append("           ) F , " + "\n");

            strSqlString.Append("    ( SELECT CUSTOMER, ROUND((SUM(ASSY_MON)/SUM(TARGET_MON))* 100, 2) JINDO" + "\n");
            strSqlString.Append("      FROM( " + "\n");
            strSqlString.Append("           SELECT CUSTOMER, (SUM(PLAN_QTY) * " + jindoPer7 + " ) / 100 AS TARGET_MON ,SUM(ASSY_QTY) AS ASSY_MON" + "\n");
            strSqlString.Append("             FROM RSUMTRNGOL " + "\n");
            strSqlString.Append("            WHERE 1=1 " + "\n");
            strSqlString.Append("             AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("             AND WORK_DATE = '" + strDate7 + "'" + "\n");
            strSqlString.Append("           GROUP BY CUSTOMER ) " + "\n");
            strSqlString.Append("           WHERE TARGET_MON <> 0 " + "\n");
            strSqlString.Append("           GROUP BY CUSTOMER " + "\n");
            strSqlString.Append("           ) G  " + "\n");
            strSqlString.Append("      WHERE 1=1  " + "\n");
            strSqlString.Append("        AND A.CUSTOMER = TOTAL.CUSTOMER  " + "\n");
            strSqlString.Append("        AND A.CUSTOMER = B.CUSTOMER  " + "\n");
            strSqlString.Append("        AND A.CUSTOMER = C.CUSTOMER  " + "\n");
            strSqlString.Append("        AND A.CUSTOMER = D.CUSTOMER  " + "\n");
            strSqlString.Append("        AND A.CUSTOMER = E.CUSTOMER  " + "\n");
            strSqlString.Append("        AND A.CUSTOMER = F.CUSTOMER  " + "\n");
            strSqlString.Append("        AND A.CUSTOMER = G.CUSTOMER  " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion


        #region EVENT 처리
        /// <summary>
        /// 6. View 버튼 Action
        /// </summary>        ///         
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            if (CheckField() == false) return;

            GridColumnInit();
            LabelTextChange();

            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 1, null, null, btnSort);
                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("goal", 0, 1, 0, 1, true, Align.Center, VerticalAlign.Center);

                // 목표
                spdData.ActiveSheet.Cells[0, 1].Value = 100;
                spdData.ActiveSheet.Cells[0, 2].Value = 100;
                spdData.ActiveSheet.Cells[0, 3].Value = 100;
                spdData.ActiveSheet.Cells[0, 4].Value = 100;
                spdData.ActiveSheet.Cells[0, 5].Value = 100;
                spdData.ActiveSheet.Cells[0, 6].Value = 100;
                spdData.ActiveSheet.Cells[0, 7].Value = 100;
                spdData.ActiveSheet.Cells[0, 8].Value = 100;
                
                dt.Dispose();

                //Chart 생성
                if (spdData.ActiveSheet.RowCount > 0)
                    fnMakeChart(spdData, spdData.ActiveSheet.RowCount);
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

        // 지난 주차의 마지막일 가져오기
        private string MakeSqlString2(string year, string date)
        {
            StringBuilder sqlString = new StringBuilder();

            sqlString.Append("SELECT MIN(SYS_DATE-1) " + "\n");
            sqlString.Append("  FROM MWIPCALDEF " + "\n");
            sqlString.Append(" WHERE 1=1" + "\n");
            sqlString.Append("   AND CALENDAR_ID='SE'" + "\n");
            sqlString.Append("   AND SYS_YEAR='" + year + "'\n");
            sqlString.Append("   AND PLAN_WEEK=(" + "\n");
            sqlString.Append("                  SELECT PLAN_WEEK " + "\n");
            sqlString.Append("                    FROM MWIPCALDEF " + "\n");
            sqlString.Append("                   WHERE 1=1 " + "\n");
            sqlString.Append("                     AND CALENDAR_ID='SE' " + "\n");
            sqlString.Append("                     AND SYS_DATE=TO_CHAR(TO_DATE('" + date + "','YYYYMMDD'),'YYYYMMDD')" + "\n");
            sqlString.Append("                 )" + "\n");

            return sqlString.ToString();
        }

        private void fnMakeChart(Miracom.SmartWeb.UI.Controls.udcFarPoint SS, int iselrow)
        {
            /****************************************************
             * Comment : SS의 데이터를 Chart에 표시한다.
             * 
             * Created By : min-woo kim(2010-07-06-화요일)
             * 
             * Modified By : min-woo kim(2010-07-06-화요일)
             ****************************************************/
            int[] ich_mm = new int[8]; int[] icols_mm = new int[8]; int[] irows_mm = new int[iselrow]; string[] stitle_mm = new string[iselrow];
            int icol = 0, irow = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                // 2015-12-02-정비재 : 데이터가 없을 경우 종료
                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                // 2015-12-02-정비재 : chart에 표시할 데이터를 배열 변수에 입력한다.
                for (icol = 0; icol < ich_mm.Length; icol++)
                {
                    ich_mm[icol] = icol + 1;
                    icols_mm[icol] = icol + 1;
                }
                for (irow = 0; irow < irows_mm.Length; irow++)
                {
                    irows_mm[irow] = irow;
                    stitle_mm[irow] = SS.Sheets[0].Cells[irow, 0].Text;
                }

                // 2015-12-02-정비재 : chart를 초기화 한다.
                udcMSChart1.RPT_1_ChartInit();
                udcMSChart1.RPT_2_ClearData();
                // 2015-12-02-정비재 : chart의 title을 설정한다.
                if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                {
                    udcMSChart1.ChartAreas[0].AxisX.Title = "ASSY 달성율현황";
                }
                else if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
                {
                    udcMSChart1.ChartAreas[0].AxisX.Title = "TEST 달성율현황";
                }
                else
                {
                    udcMSChart1.ChartAreas[0].AxisX.Title = "달성율현황";
                }
                udcMSChart1.ChartAreas[0].AxisY.Title = "Achievement rate";
                udcMSChart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Tahoma", 10.25F);
                udcMSChart1.RPT_3_OpenData(irows_mm.Length, icols_mm.Length);
                double max1 = udcMSChart1.RPT_4_AddData(SS, irows_mm, icols_mm, SeriseType.Rows);
                udcMSChart1.RPT_5_CloseData();
                udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.05);
                udcMSChart1.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, 1);
                udcMSChart1.RPT_8_SetSeriseLegend(stitle_mm, System.Windows.Forms.DataVisualization.Charting.Docking.Top);
             
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.ActiveSheet.Rows.Count > 0)
            {
                StringBuilder Condition = new StringBuilder();
                Condition.AppendFormat("기준일자: {0}     today: {1}      workday: {2}     remain: {3}      표준진도율: {4} " + "\n", cdvDate.Text, lblToday.Text.ToString(), lblLastDay.Text.ToString(), lblRemain.Text.ToString(), lblJindo.Text.ToString());
                //Condition.AppendFormat("today: {0}    workday: {1}     표준진도율: {2} " + lblToday.Text.ToString() , lblLastDay.Text.ToString(), lblJindo.Text.ToString());
                Condition.Append("        단위 : PKG (pcs) , COB (매) ");
                //Condition.AppendFormat("전일실적기준: {0}    MEMO:   {1}     월마감: {2}     진도율: {3}    잔여일수: " + lblRemain2.Text.ToString(), lblYesterday.Text.ToString(), lblMemo.Text.ToString(), lblMagam2.Text.ToString(), lblJindo2.Text.ToString());

                //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);
                //spdData.ExportExcel();            
            }
        }

        /// <summary>
        /// Factory 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            cdvLotType.sFactory = cdvFactory.txtValue;
        }

        /// <summary>
        /// 7. 상단 Lebel 표시
        /// </summary>
        private void LabelTextChange()
        {
            int remain = 0;

            DateTime getStartDate = Convert.ToDateTime(cdvDate.Value.ToString("yyyy-MM") + "-01");
            string getEndDate = getStartDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");
            string strDate = cdvDate.Value.ToString("yyyyMMdd");
            string selectday = strDate.Substring(6, 2);
            string lastday = getEndDate.Substring(6, 2);

            // 선택 날짜에서 -1
            DateTime getStartDate1 = Convert.ToDateTime(cdvDate.Value).AddDays(-1);
            strDate1 = getStartDate1.ToString("yyyyMMdd");
            // 선택 날짜에서 -1이 달이 바뀔 경우 때문에 
            startDate1 = getStartDate1.ToString("yyyyMM") + "01";
            string selectday1 = strDate1.Substring(6, 2);
            DateTime getStartDate_1 = Convert.ToDateTime(getStartDate1.ToString("yyyy-MM") + "-01");
            getEndDate1 = getStartDate_1.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");
            string lastday1 = getEndDate1.Substring(6, 2);
            year1 = getStartDate1.ToString("yyyy");
            month1 = getStartDate1.ToString("yyyyMM");
            //지난 달 마지막 일
            lastMonth1 = getStartDate_1.AddDays(-1).ToString("yyyyMMdd");


            // 선택 날짜에서 -2
            DateTime getStartDate2 = Convert.ToDateTime(cdvDate.Value).AddDays(-2);
            strDate2 = getStartDate2.ToString("yyyyMMdd");
            year2 = getStartDate2.ToString("yyyy");
            startDate2 = getStartDate2.ToString("yyyyMM") + "01";
            string selectday2 = strDate2.Substring(6, 2);
            DateTime getStartDate_2 = Convert.ToDateTime(getStartDate2.ToString("yyyy-MM") + "-01");
            getEndDate2 = getStartDate_2.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");
            string lastday2 = getEndDate2.Substring(6, 2);
            month2 = getStartDate2.ToString("yyyyMM");
            //지난 달 마지막 일
            lastMonth2 = getStartDate_2.AddDays(-1).ToString("yyyyMMdd");

            // 선택 날짜에서 -3
            DateTime getStartDate3 = Convert.ToDateTime(cdvDate.Value).AddDays(-3);
            strDate3 = getStartDate3.ToString("yyyyMMdd");
            year3 = getStartDate3.ToString("yyyy");
            startDate3 = getStartDate3.ToString("yyyyMM") + "01";
            string selectday3 = strDate3.Substring(6, 2);
            DateTime getStartDate_3 = Convert.ToDateTime(getStartDate3.ToString("yyyy-MM") + "-01");
            getEndDate3 = getStartDate_3.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");
            string lastday3 = getEndDate3.Substring(6, 2);
            month3 = getStartDate3.ToString("yyyyMM");
            //지난 달 마지막 일
            lastMonth3 = getStartDate_3.AddDays(-1).ToString("yyyyMMdd");

            // 선택 날짜에서 -4
            DateTime getStartDate4 = Convert.ToDateTime(cdvDate.Value).AddDays(-4);
            strDate4 = getStartDate4.ToString("yyyyMMdd");
            year4 = getStartDate4.ToString("yyyy");
            startDate4 = getStartDate4.ToString("yyyyMM") + "01";
            string selectday4 = strDate4.Substring(6, 2);
            DateTime getStartDate_4 = Convert.ToDateTime(getStartDate4.ToString("yyyy-MM") + "-01");
            getEndDate4 = getStartDate_4.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");
            string lastday4 = getEndDate4.Substring(6, 2);
            month4 = getStartDate4.ToString("yyyyMM");
            //지난 달 마지막 일
            lastMonth4 = getStartDate_4.AddDays(-1).ToString("yyyyMMdd");

            // 선택 날짜에서 -5
            DateTime getStartDate5 = Convert.ToDateTime(cdvDate.Value).AddDays(-5);
            strDate5 = getStartDate5.ToString("yyyyMMdd");
            year5 = getStartDate5.ToString("yyyy");
            startDate5 = getStartDate5.ToString("yyyyMM") + "01";
            string selectday5 = strDate5.Substring(6, 2);
            DateTime getStartDate_5 = Convert.ToDateTime(getStartDate5.ToString("yyyy-MM") + "-01");
            getEndDate5 = getStartDate_5.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");
            string lastday5 = getEndDate5.Substring(6, 2);
            month5 = getStartDate5.ToString("yyyyMM");
            //지난 달 마지막 일
            lastMonth5 = getStartDate_5.AddDays(-1).ToString("yyyyMMdd");

            // 선택 날짜에서 -6
            DateTime getStartDate6 = Convert.ToDateTime(cdvDate.Value).AddDays(-6);
            strDate6 = getStartDate6.ToString("yyyyMMdd");
            year6 = getStartDate6.ToString("yyyy");
            startDate6 = getStartDate6.ToString("yyyyMM") + "01";
            string selectday6 = strDate6.Substring(6, 2);
            DateTime getStartDate_6 = Convert.ToDateTime(getStartDate6.ToString("yyyy-MM") + "-01");
            getEndDate6 = getStartDate_6.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");
            string lastday6 = getEndDate6.Substring(6, 2);
            month6 = getStartDate6.ToString("yyyyMM");
            //지난 달 마지막 일
            lastMonth6 = getStartDate_6.AddDays(-1).ToString("yyyyMMdd");

            // 선택 날짜에서 -7
            DateTime getStartDate7 = Convert.ToDateTime(cdvDate.Value).AddDays(-7);
            strDate7 = getStartDate7.ToString("yyyyMMdd");
            year7 = getStartDate7.ToString("yyyy");
            startDate7 = getStartDate7.ToString("yyyyMM") + "01";
            string selectday7 = strDate7.Substring(6, 2);
            DateTime getStartDate_7 = Convert.ToDateTime(getStartDate7.ToString("yyyy-MM") + "-01");
            getEndDate7 = getStartDate_7.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");
            string lastday7 = getEndDate7.Substring(6, 2);
            month7 = getStartDate6.ToString("yyyyMM");
            //지난 달 마지막 일
            lastMonth7 = getStartDate_7.AddDays(-1).ToString("yyyyMMdd");

            double jindo = (Convert.ToDouble(selectday)) / Convert.ToDouble(lastday) * 100;
            double jindo1 = (Convert.ToDouble(selectday1)) / Convert.ToDouble(lastday1) * 100;
            double jindo2 = (Convert.ToDouble(selectday2)) / Convert.ToDouble(lastday2) * 100;
            double jindo3 = (Convert.ToDouble(selectday3)) / Convert.ToDouble(lastday3) * 100;
            double jindo4 = (Convert.ToDouble(selectday4)) / Convert.ToDouble(lastday4) * 100;
            double jindo5 = (Convert.ToDouble(selectday5)) / Convert.ToDouble(lastday5) * 100;
            double jindo6 = (Convert.ToDouble(selectday6)) / Convert.ToDouble(lastday6) * 100;
            double jindo7 = (Convert.ToDouble(selectday7)) / Convert.ToDouble(lastday7) * 100;

            // 진도율 소수점 1째자리 까지 표시 (2009.08.17 임종우)
            jindoPer = Math.Round(Convert.ToDecimal(jindo), 1);
            jindoPer1 = Math.Round(Convert.ToDecimal(jindo1), 1);
            jindoPer2 = Math.Round(Convert.ToDecimal(jindo2), 1);
            jindoPer3 = Math.Round(Convert.ToDecimal(jindo3), 1);
            jindoPer4 = Math.Round(Convert.ToDecimal(jindo4), 1);
            jindoPer5 = Math.Round(Convert.ToDecimal(jindo5), 1);
            jindoPer6 = Math.Round(Convert.ToDecimal(jindo6), 1);
            jindoPer7 = Math.Round(Convert.ToDecimal(jindo7), 1);

            //금일조회일 경우 조회조건은 REALTIME
            if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
            {
                dayArry[0] = cdvDate.Value.AddDays(-3).ToString("MM.dd");
                dayArry[1] = cdvDate.Value.AddDays(-2).ToString("MM.dd");
                dayArry[2] = cdvDate.Value.AddDays(-1).ToString("MM.dd");

                dayArry2[0] = cdvDate.Value.AddDays(-3).ToString("yyyyMMdd");
                dayArry2[1] = cdvDate.Value.AddDays(-2).ToString("yyyyMMdd");
                dayArry2[2] = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");

            }
            else
            {
                dayArry[0] = cdvDate.Value.AddDays(-2).ToString("MM.dd");
                dayArry[1] = cdvDate.Value.AddDays(-1).ToString("MM.dd");
                dayArry[2] = cdvDate.Value.ToString("MM.dd");

                dayArry2[0] = cdvDate.Value.AddDays(-2).ToString("yyyyMMdd");
                dayArry2[1] = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");
                dayArry2[2] = cdvDate.Value.ToString("yyyyMMdd");
            }
            // 컬럼에 보여줄 값
            month_col = cdvDate.Value.ToString("MM");
            dayArry_col[0] = cdvDate.Value.AddDays(-1).ToString("MM.dd");
            dayArry_col[1] = cdvDate.Value.AddDays(-2).ToString("MM.dd");
            dayArry_col[2] = cdvDate.Value.AddDays(-3).ToString("MM.dd");
            dayArry_col[3] = cdvDate.Value.AddDays(-4).ToString("MM.dd");
            dayArry_col[4] = cdvDate.Value.AddDays(-5).ToString("MM.dd");
            dayArry_col[5] = cdvDate.Value.AddDays(-6).ToString("MM.dd");
            dayArry_col[6] = cdvDate.Value.AddDays(-7).ToString("MM.dd");




            // 일주일치 달성율을 보여 주기때문에 일주일 전의 날짜 중 1일이나 2일이 포함 될 수 있기 때문에
            // -1일
            dayArry2_1[0] = cdvDate.Value.AddDays(-3).ToString("yyyyMMdd");
            dayArry2_1[1] = cdvDate.Value.AddDays(-2).ToString("yyyyMMdd");
            dayArry2_1[2] = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");
            //-2일 
            dayArry2_2[0] = cdvDate.Value.AddDays(-4).ToString("yyyyMMdd");
            dayArry2_2[1] = cdvDate.Value.AddDays(-3).ToString("yyyyMMdd");
            dayArry2_2[2] = cdvDate.Value.AddDays(-2).ToString("yyyyMMdd");
            //-3일 
            dayArry2_3[0] = cdvDate.Value.AddDays(-5).ToString("yyyyMMdd");
            dayArry2_3[1] = cdvDate.Value.AddDays(-4).ToString("yyyyMMdd");
            dayArry2_3[2] = cdvDate.Value.AddDays(-3).ToString("yyyyMMdd");
            //-4일 
            dayArry2_4[0] = cdvDate.Value.AddDays(-6).ToString("yyyyMMdd");
            dayArry2_4[1] = cdvDate.Value.AddDays(-5).ToString("yyyyMMdd");
            dayArry2_4[2] = cdvDate.Value.AddDays(-4).ToString("yyyyMMdd");
            //-5일 
            dayArry2_5[0] = cdvDate.Value.AddDays(-7).ToString("yyyyMMdd");
            dayArry2_5[1] = cdvDate.Value.AddDays(-6).ToString("yyyyMMdd");
            dayArry2_5[2] = cdvDate.Value.AddDays(-5).ToString("yyyyMMdd");
            //-6일 
            dayArry2_6[0] = cdvDate.Value.AddDays(-8).ToString("yyyyMMdd");
            dayArry2_6[1] = cdvDate.Value.AddDays(-7).ToString("yyyyMMdd");
            dayArry2_6[2] = cdvDate.Value.AddDays(-6).ToString("yyyyMMdd");
            //-7일 
            dayArry2_7[0] = cdvDate.Value.AddDays(-9).ToString("yyyyMMdd");
            dayArry2_7[1] = cdvDate.Value.AddDays(-8).ToString("yyyyMMdd");
            dayArry2_7[2] = cdvDate.Value.AddDays(-7).ToString("yyyyMMdd");

            lblToday.Text = selectday + " day";
            lblLastDay.Text = lastday + " day";

            // 금일 조회일 경우 잔여일에 금일 포함함.
            if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
            {
                remain = (Convert.ToInt32(lastday) - Convert.ToInt32(selectday) + 1);
            }
            else
            {
                remain = (Convert.ToInt32(lastday) - Convert.ToInt32(selectday));
            }

            lblRemain.Text = remain.ToString() + " day";
            lblJindo.Text = jindoPer.ToString() + "%";

        }
        #endregion
    }
}
