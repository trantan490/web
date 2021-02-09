using System;
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

namespace Hana.PQC
{
    public partial class PQC031202 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        #region " PQC031202 : Program Initial "

        public PQC031202()
        {
            InitializeComponent();

            pnlTrend.Dock = DockStyle.Fill;
            pnlList.Dock = DockStyle.Fill;

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;

            cdvDate.Value = System.DateTime.Now;

            lblSdtDate.Text = "*기준 : " + cdvDate.Value.ToShortDateString() + " 6:00";
            cboGraph.SelectedIndex = 0;

            //InitChart();
        }

        #endregion


        #region " Common Function "

        private void fnSSInitial(Miracom.SmartWeb.UI.Controls.udcFarPoint SS)
        {
            /****************************************************
             * Comment : SS의 Header를 설정한다.
             * 
             * Created By : 김선호(2012-07-26-목요일)
             * 
             * Modified By : 
             ****************************************************/

            // 분기
            string squery1 = "";
            DataTable dtQuarter = null;
            //월
            string squery2 = "";
            DataTable dtMonth = null;
            //주차
            string squery3 = "";
            DataTable dtWeek = null;
            //일가져오기
            string squery4 = "";
            DataTable dtDay = null;            

            int iindex = 0;
            try
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                SS.RPT_ColumnInit();

                string todate = cdvDate.Value.ToString("yyyyMMddHHmmss");

                if (cboGraph.SelectedIndex == -1 || cboGraph.SelectedIndex == 0)
                {
                    #region - QCN 24시간 이상 정체 Lot Trend

                    squery1 = "SELECT CASE WHEN TO_CHAR(TO_DATE('" + todate + "'), 'Q') - 2 > 0 \n"
                            + "            THEN SUBSTR(TO_CHAR(TO_DATE('" + todate + "'),'YYYY'),3,2) || 'Y ' || TO_CHAR(TO_CHAR(TO_DATE('" + todate + "'), 'Q') - 2)|| 'Q' \n"
                            + "            WHEN TO_CHAR(TO_DATE('" + todate + "'), 'Q') - 2 = 0 \n"
                            + "            THEN SUBSTR(TO_CHAR(TO_DATE('" + todate + "'),'YYYY'),3,2) - 1 || 'Y ' || '4' || 'Q' \n"
                            + "            WHEN TO_CHAR(TO_DATE('" + todate + "'), 'Q') - 2 = -1 \n"
                            + "            THEN SUBSTR(TO_CHAR(TO_DATE('" + todate + "'),'YYYY'),3,2) - 1 || 'Y ' || '3' || 'Q' \n"
                            + "       END AS Q2, \n"
                            + "       CASE WHEN TO_CHAR(TO_DATE('" + todate + "'), 'Q') - 1 > 0 \n"
                            + "            THEN SUBSTR(TO_CHAR(TO_DATE('" + todate + "'),'YYYY'),3,2) || 'Y ' || TO_CHAR(TO_CHAR(TO_DATE('" + todate + "'), 'Q') - 1)|| 'Q' \n"
                            + "            WHEN TO_CHAR(TO_DATE('" + todate + "'), 'Q') - 1 = 0 \n"
                            + "            THEN SUBSTR(TO_CHAR(TO_DATE('" + todate + "'),'YYYY'),3,2) - 1 || 'Y ' || '4' || 'Q' \n"
                            + "            END AS Q1 \n"
                            + "  FROM DUAL";

                    dtQuarter = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", squery1.Replace((char)Keys.Tab, ' '));

                    squery2 = "SELECT SUBSTR(TO_CHAR(TO_DATE('" + todate + "'),'YYYYMM'),5,2)||'월' AS MM7 \n"
                           + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-1),'YYYYMM'),5,2)||'월' AS MM6 \n"
                           + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-2),'YYYYMM'),5,2)||'월' AS MM5 \n"
                           + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-3),'YYYYMM'),5,2)||'월' AS MM4 \n"
                           + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-4),'YYYYMM'),5,2)||'월' AS MM3 \n"
                           + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-5),'YYYYMM'),5,2)||'월' AS MM2 \n"
                           + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-6),'YYYYMM'),5,2)||'월' AS MM1 \n"
                           + "  FROM DUAL";

                    dtMonth = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", squery2.Replace((char)Keys.Tab, ' '));

                    todate = cdvDate.Value.ToString("yyyyMMdd");

                    squery3 = "SELECT DECODE(PLAN_WEEK - 4,-3,50,-2,51,-1,52, 0,53, PLAN_WEEK - 4)||'주' AS WW1 \n"
                           + "      , DECODE(PLAN_WEEK - 3,-2,51,-1,52, 0,53, PLAN_WEEK - 3)||'주' AS WW2 \n"
                           + "      , DECODE(PLAN_WEEK - 2,-1,52, 0,53, PLAN_WEEK - 2)||'주' AS WW3 \n"
                           + "	    , DECODE(PLAN_WEEK - 1, 0,53, PLAN_WEEK - 1)||'주' AS WW4 \n"
                           + "	    , PLAN_WEEK||'주' AS WW5 \n"
                           + "	 FROM MWIPCALDEF \n"
                           + "	WHERE CALENDAR_ID = 'QC' \n"
                           + "	  AND SYS_DATE = TO_CHAR(TO_DATE('" + todate + "','YYYYMMDD'),'YYYYMMDD')";

                    dtWeek = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", squery3.Replace((char)Keys.Tab, ' '));

                    squery4 = "SELECT SUBSTR(TO_CHAR(TO_DATE('" + todate + "'),'YYYYMMDD'),7,2)||'일' AS DD \n"
                           + "      , SUBSTR(TO_CHAR(TO_DATE('" + todate + "')-1,'YYYYMMDD'),7,2)||'일' AS DD1 \n"
                           + "      , SUBSTR(TO_CHAR(TO_DATE('" + todate + "')-2,'YYYYMMDD'),7,2)||'일' AS DD2 \n"
                           + "      , SUBSTR(TO_CHAR(TO_DATE('" + todate + "')-3,'YYYYMMDD'),7,2)||'일' AS DD3 \n"
                           + "      , SUBSTR(TO_CHAR(TO_DATE('" + todate + "')-4,'YYYYMMDD'),7,2)||'일' AS DD4 \n"
                           + "      , SUBSTR(TO_CHAR(TO_DATE('" + todate + "')-5,'YYYYMMDD'),7,2)||'일' AS DD5 \n"
                           + "      , SUBSTR(TO_CHAR(TO_DATE('" + todate + "')-6,'YYYYMMDD'),7,2)||'일' AS DD6 \n"
                           + "      , SUBSTR(TO_CHAR(TO_DATE('" + todate + "')-6,'YYYYMMDD'),7,2)||'일' AS DD7 \n"
                           + "      , SUBSTR(TO_CHAR(TO_DATE('" + todate + "')-7,'YYYYMMDD'),7,2)||'일' AS DD8 \n"
                           + "      , SUBSTR(TO_CHAR(TO_DATE('" + todate + "')-8,'YYYYMMDD'),7,2)||'일' AS DD9 \n"
                           + "      , SUBSTR(TO_CHAR(TO_DATE('" + todate + "')-9,'YYYYMMDD'),7,2)||'일' AS DD10 \n"
                           + "      , SUBSTR(TO_CHAR(TO_DATE('" + todate + "')-10,'YYYYMMDD'),7,2)||'일' AS DD11 \n"
                           + "      , SUBSTR(TO_CHAR(TO_DATE('" + todate + "')-11,'YYYYMMDD'),7,2)||'일' AS DD12 \n"
                           + "      , SUBSTR(TO_CHAR(TO_DATE('" + todate + "')-12,'YYYYMMDD'),7,2)||'일' AS DD13 \n"
                           + "      , SUBSTR(TO_CHAR(TO_DATE('" + todate + "')-13,'YYYYMMDD'),7,2)||'일' AS DD14 \n"
                           + "      , SUBSTR(TO_CHAR(TO_DATE('" + todate + "')-14,'YYYYMMDD'),7,2)||'일' AS DD15 \n"
                           + "	 FROM DUAL";
                    dtDay = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", squery4.Replace((char)Keys.Tab, ' '));

                    SS.RPT_AddBasicColumn("구 분", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);

                    if (dtQuarter.Rows.Count <= 0)
                    {
                        dtQuarter.Dispose();
                        SS.RPT_AddBasicColumn("Q-1", 0, iindex + 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("Q-2", 0, iindex + 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                    }
                    else
                    {
                        SS.RPT_AddBasicColumn(dtQuarter.Rows[0][0].ToString(), 0, iindex + 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtQuarter.Rows[0][1].ToString(), 0, iindex + 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                    }

                    if (dtMonth.Rows.Count <= 0)
                    {
                        dtMonth.Dispose();
                        SS.RPT_AddBasicColumn("MM-1", 0, iindex + 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("MM-2", 0, iindex + 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("MM-3", 0, iindex + 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("MM-4", 0, iindex + 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("MM-5", 0, iindex + 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("MM-6", 0, iindex + 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("MM-7", 0, iindex + 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                    }
                    else
                    {
                        SS.RPT_AddBasicColumn(dtMonth.Rows[0][6].ToString(), 0, iindex + 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtMonth.Rows[0][5].ToString(), 0, iindex + 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtMonth.Rows[0][4].ToString(), 0, iindex + 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtMonth.Rows[0][3].ToString(), 0, iindex + 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtMonth.Rows[0][2].ToString(), 0, iindex + 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtMonth.Rows[0][1].ToString(), 0, iindex + 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtMonth.Rows[0][0].ToString(), 0, iindex + 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                    }

                    if (dtWeek.Rows.Count <= 0)
                    {
                        dtWeek.Dispose();
                        SS.RPT_AddBasicColumn("WW-5", 0, iindex + 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("WW-4", 0, iindex + 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("WW-3", 0, iindex + 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("WW-2", 0, iindex + 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("WW-1", 0, iindex + 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                    }
                    else
                    {
                        SS.RPT_AddBasicColumn(dtWeek.Rows[0][0].ToString(), 0, iindex + 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtWeek.Rows[0][1].ToString(), 0, iindex + 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtWeek.Rows[0][2].ToString(), 0, iindex + 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtWeek.Rows[0][3].ToString(), 0, iindex + 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtWeek.Rows[0][4].ToString(), 0, iindex + 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                    }

                    if (dtDay.Rows.Count <= 0)
                    {
                        dtDay.Dispose();
                        SS.RPT_AddBasicColumn("DD-1", 0, iindex + 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("DD-2", 0, iindex + 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("DD-3", 0, iindex + 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("DD-4", 0, iindex + 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("DD-5", 0, iindex + 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("DD-6", 0, iindex + 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("DD-7", 0, iindex + 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("DD-8", 0, iindex + 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("DD-9", 0, iindex + 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("DD-10", 0, iindex + 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("DD-11", 0, iindex + 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("DD-12", 0, iindex + 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("DD-13", 0, iindex + 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn("DD-14", 0, iindex + 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                    }
                    else
                    {
                        SS.RPT_AddBasicColumn(dtDay.Rows[0][13].ToString(), 0, iindex + 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtDay.Rows[0][12].ToString(), 0, iindex + 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtDay.Rows[0][11].ToString(), 0, iindex + 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtDay.Rows[0][10].ToString(), 0, iindex + 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtDay.Rows[0][9].ToString(), 0, iindex + 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtDay.Rows[0][8].ToString(), 0, iindex + 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtDay.Rows[0][7].ToString(), 0, iindex + 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtDay.Rows[0][6].ToString(), 0, iindex + 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtDay.Rows[0][5].ToString(), 0, iindex + 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtDay.Rows[0][4].ToString(), 0, iindex + 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtDay.Rows[0][3].ToString(), 0, iindex + 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtDay.Rows[0][2].ToString(), 0, iindex + 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtDay.Rows[0][1].ToString(), 0, iindex + 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                        SS.RPT_AddBasicColumn(dtDay.Rows[0][0].ToString(), 0, iindex + 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                    }

                    #endregion
                }
                else if (cboGraph.SelectedIndex == 1)
                {
                    #region - QCN 24시간 이상 정체 Lot List

                    SS.RPT_AddBasicColumn("published date", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    SS.RPT_AddBasicColumn("Inconsistence number", 0, iindex + 1, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                    SS.RPT_AddBasicColumn("고객", 0, iindex + 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                    SS.RPT_AddBasicColumn("PKG", 0, iindex + 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    SS.RPT_AddBasicColumn("LOT ID", 0, iindex + 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    SS.RPT_AddBasicColumn("공정", 0, iindex + 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                    SS.RPT_AddBasicColumn("Issue time", 0, iindex + 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    SS.RPT_AddBasicColumn("Elapsed time", 0, iindex + 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    SS.RPT_AddBasicColumn("Defect Code", 0, iindex + 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    SS.RPT_AddBasicColumn("Current STEP", 0, iindex + 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    SS.RPT_AddBasicColumn("the department concerned", 0, iindex + 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);

                    #endregion
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
        }

        private bool fnDataFind()
        {
            /****************************************************
             * Comment : DataBase에 저장된 데이터를 조회한다.
             * 
             * Created By : 김선호(2012-07-26-목요일)
             * 
             * Modified By :
             ****************************************************/
            DataTable dt = null;
            string QRY = "";

            try
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                LoadingPopUp.LoadIngPopUpShow(this);

                // Sheet / Listview를 초기화 한다.
                CmnInitFunction.ClearList(spdData, true);

                string date = cdvDate.Value.ToString("yyyyMMddHHmmss");

                QRY = "	SELECT GUBUN	\n"
                    + "	    , SUM(QTY_QQ_1) AS QTY_QQ_1, SUM(QTY_QQ_2) AS QTY_QQ_2 \n"
                    + "	    , SUM(QTY_MM_1) AS QTY_MM_1, SUM(QTY_MM_2) AS QTY_MM_2, SUM(QTY_MM_3) AS QTY_MM_3 \n"
                    + "	    , SUM(QTY_MM_4) AS QTY_MM_4, SUM(QTY_MM_5) AS QTY_MM_5, SUM(QTY_MM_6) AS QTY_MM_6, SUM(QTY_MM_7) AS QTY_MM_7 \n"
                    + "	    , SUM(QTY_WW_1) AS QTY_WW_1, SUM(QTY_WW_2) AS QTY_WW_2, SUM(QTY_WW_3) AS QTY_WW_3, SUM(QTY_WW_4) AS QTY_WW_4, SUM(QTY_WW_5) AS QTY_WW_5	\n"
                    + "	    , SUM(QTY_DD_1) AS QTY_DD_1, SUM(QTY_DD_2) AS QTY_DD_2, SUM(QTY_DD_3) AS QTY_DD_3, SUM(QTY_DD_4) AS QTY_DD_4 \n"
                    + "	    , SUM(QTY_DD_5) AS QTY_DD_5, SUM(QTY_DD_6) AS QTY_DD_6, SUM(QTY_DD_7) AS QTY_DD_7 \n"
                    + "	    , SUM(QTY_DD_8) AS QTY_DD_8, SUM(QTY_DD_9) AS QTY_DD_9, SUM(QTY_DD_10) AS QTY_DD_10, SUM(QTY_DD_11) AS QTY_DD_11 \n"
                    + "	    , SUM(QTY_DD_12) AS QTY_DD_12, SUM(QTY_DD_13) AS QTY_DD_13, SUM(QTY_DD_14) AS QTY_DD_14	\n"
                    + "	FROM (	\n"
                    + "	    SELECT '정체Lot건수' AS GUBUN	\n"
                    + "	        ,ROUND(SUM(CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'Q')	\n"
                    + "	                    WHEN '4' THEN 	\n"
                    + "	                                CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'03', 'YYYYMM')), 'YYYYMMDD')||'220000'	\n"
                    + "	                                            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'03', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000'	\n"
                    + "	                                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                               THEN 1	\n"
                    + "	                                               ELSE 0	\n"
                    + "	                                          END	\n"
                    + "	                                END	\n"
                    + "	                    WHEN '3' THEN 	\n"
                    + "	                                CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'12', 'YYYYMM')), 'YYYYMMDD')||'220000'	\n"
                    + "	                                            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'12', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000'	\n"
                    + "	                                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                               THEN 1	\n"
                    + "	                                               ELSE 0	\n"
                    + "	                                          END 	\n"
                    + "	                                END	\n"
                    + "	                    WHEN '2' THEN 	\n"
                    + "	                                CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'09', 'YYYYMM')), 'YYYYMMDD')||'220000'	\n"
                    + "	                                            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'09', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000'	\n"
                    + "	                                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                               THEN 1	\n"
                    + "	                                               ELSE 0	\n"
                    + "	                                          END 	\n"
                    + "	                                END	\n"
                    + "	                    WHEN '1' THEN 	\n"
                    + "	                                CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'06', 'YYYYMM')), 'YYYYMMDD')||'220000'	\n"
                    + "	                                            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'06', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000'	\n"
                    + "	                                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                               THEN 1	\n"
                    + "	                                               ELSE 0	\n"
                    + "	                                          END 	\n"
                    + "	                                END	\n"
                    + "	                  END) / 90, 1) AS QTY_QQ_1	\n"
                    //+ "	                  END) / 3, 1) AS QTY_QQ_1	\n"
                    + "	        ,ROUND(SUM(CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'Q')	\n"
                    + "	                    WHEN '4' THEN 	\n"
                    + "	                                CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'06', 'YYYYMM')), 'YYYYMMDD')||'220000'	\n"
                    + "	                                            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'06', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000'	\n"
                    + "	                                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                               THEN 1	\n"
                    + "	                                               ELSE 0	\n"
                    + "	                                          END	\n"
                    + "	                                END	\n"
                    + "	                    WHEN '3' THEN 	\n"
                    + "	                                CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'03', 'YYYYMM')), 'YYYYMMDD')||'220000'	\n"
                    + "	                                            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'03', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000'	\n"
                    + "	                                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                               THEN 1	\n"
                    + "	                                               ELSE 0	\n"
                    + "	                                          END 	\n"
                    + "	                                END	\n"
                    + "	                    WHEN '2' THEN 	\n"
                    + "	                                CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'12', 'YYYYMM')), 'YYYYMMDD')||'220000'	\n"
                    + "	                                            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'12', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000'	\n"
                    + "	                                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                               THEN 1	\n"
                    + "	                                               ELSE 0	\n"
                    + "	                                          END 	\n"
                    + "	                                END	\n"
                    + "	                    WHEN '1' THEN 	\n"
                    + "	                                CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'09', 'YYYYMM')), 'YYYYMMDD')||'220000'	\n"
                    + "	                                            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'09', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000'	\n"
                    + "	                                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                               THEN 1	\n"
                    + "	                                               ELSE 0	\n"
                    + "	                                          END 	\n"
                    + "	                                END	\n"
                    + "	                  END) / 90, 1) AS QTY_QQ_2	\n"
                    //+ "	                  END) / 3, 1) AS QTY_QQ_2	\n"
                    + "	        , 0 AS QTY_MM_1	\n"
                    + "	        , 0 AS QTY_MM_2	\n"
                    + "	        , 0 AS QTY_MM_3	\n"
                    + "	        , 0 AS QTY_MM_4	\n"
                    + "	        , 0 AS QTY_MM_5	\n"
                    + "	        , 0 AS QTY_MM_6	\n"
                    + "	        , 0 AS QTY_MM_7	\n"
                    + "	        , 0 AS QTY_WW_1	\n"
                    + "	        , 0 AS QTY_WW_2	\n"
                    + "	        , 0 AS QTY_WW_3	\n"
                    + "	        , 0 AS QTY_WW_4	\n"
                    + "	        , 0 AS QTY_WW_5	\n"
                    + "	        , 0 AS QTY_DD_1	\n"
                    + "	        , 0 AS QTY_DD_2	\n"
                    + "	        , 0 AS QTY_DD_3	\n"
                    + "	        , 0 AS QTY_DD_4	\n"
                    + "	        , 0 AS QTY_DD_5	\n"
                    + "	        , 0 AS QTY_DD_6	\n"
                    + "	        , 0 AS QTY_DD_7	\n"
                    + "	        , 0 AS QTY_DD_8	\n"
                    + "	        , 0 AS QTY_DD_9	\n"
                    + "	        , 0 AS QTY_DD_10 \n"
                    + "	        , 0 AS QTY_DD_11 \n"
                    + "	        , 0 AS QTY_DD_12 \n"
                    + "	        , 0 AS QTY_DD_13 \n"
                    + "	        , 0 AS QTY_DD_14 \n"
                    + "	    FROM CABRLOTSTS@RPTTOMES \n"
                    + "	    WHERE   1=1	\n"
                    + "	    AND FACTORY = '" + cdvFactory.Text + "' \n"
                    + "	    AND STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -7)), 'YYYYMMDD')||'220000'	\n"
                    + "	    AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMMDD')||'220000' \n"
                    + "	    AND OPER_1 >= 'A0000'	\n"
                    + "	    AND OPER_1 <= 'AZ010'	\n"
                    + "	    AND CLOSE_FLAG IN (' ','Y') \n"
                    + "	    AND ABR_NO LIKE 'QCN%'	\n"
                    + "	    AND DEL_FLAG = ' '	\n"
                    + "	    UNION ALL	\n"
                    + "	    SELECT '정체Lot건수' AS GUBUN \n"
                    + "	        , 0 AS QTY_QQ_1	\n"
                    + "	        , 0 AS QTY_QQ_2	\n"
                    + "	        , ROUND(SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -7)), 'YYYYMMDD')||'220000'	\n"
                    + "	                            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -6)), 'YYYYMMDD')||'220000'	\n"
                    + "	                         THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                                THEN 1	\n"
                    + "	                                                ELSE 0	\n"
                    + "	                              END	\n"
                    + "	                END) / TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -6)), 'DD'), 1) AS QTY_MM_1	\n"
                    + "	        , ROUND(SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -6)), 'YYYYMMDD')||'220000'	\n"
                    + "	                            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -5)), 'YYYYMMDD')||'220000'	\n"
                    + "	                         THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                                THEN 1	\n"
                    + "	                                                ELSE 0	\n"
                    + "	                              END	\n"
                    + "	                END) / TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -5)), 'DD'), 1) AS QTY_MM_2	\n"
                    + "	        , ROUND(SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -5)), 'YYYYMMDD')||'220000'	\n"
                    + "	                            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'YYYYMMDD')||'220000'	\n"
                    + "	                         THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                                THEN 1	\n"
                    + "	                                                ELSE 0	\n"
                    + "	                              END	\n"
                    + "	                END) / TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'DD'), 1) AS QTY_MM_3	\n"
                    + "	        , ROUND(SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'YYYYMMDD')||'220000'	\n"
                    + "	                            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'YYYYMMDD')||'220000'	\n"
                    + "	                         THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                                THEN 1	\n"
                    + "	                                                ELSE 0	\n"
                    + "	                              END	\n"
                    + "	                END) / TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'DD'), 1) AS QTY_MM_4	\n"
                    + "	        , ROUND(SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'YYYYMMDD')||'220000'	\n"
                    + "	                            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'YYYYMMDD')||'220000'	\n"
                    + "	                         THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                                THEN 1	\n"
                    + "	                                                ELSE 0	\n"
                    + "	                              END	\n"
                    + "	                END) / TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'DD'), 1) AS QTY_MM_5	\n"
                    + "	        , ROUND(SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'YYYYMMDD')||'220000'	\n"
                    + "	                AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'YYYYMMDD')||'220000'	\n"
                    + "	                 THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                        THEN 1	\n"
                    + "	                                        ELSE 0	\n"
                    + "	                      END	\n"
                    + "	        END) / TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'DD'), 1) AS QTY_MM_6	\n"
                    + "	        , ROUND(SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'YYYYMMDD')||'220000'	\n"
                    + "	                        AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -0)), 'YYYYMMDD')||'220000'	\n"
                    + "	                         THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                                THEN 1	\n"
                    + "	                                                ELSE 0	\n"
                    + "	                              END	\n"
                    + "	                END) / TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -0)), 'DD'), 1) AS QTY_MM_7	\n"
                    + "	        , 0 AS QTY_WW_1	\n"
                    + "	        , 0 AS QTY_WW_2	\n"
                    + "	        , 0 AS QTY_WW_3	\n"
                    + "	        , 0 AS QTY_WW_4	\n"
                    + "	        , 0 AS QTY_WW_5	\n"
                    + "	        , 0 AS QTY_DD_1	\n"
                    + "	        , 0 AS QTY_DD_2	\n"
                    + "	        , 0 AS QTY_DD_3	\n"
                    + "	        , 0 AS QTY_DD_4	\n"
                    + "	        , 0 AS QTY_DD_5	\n"
                    + "	        , 0 AS QTY_DD_6	\n"
                    + "	        , 0 AS QTY_DD_7	\n"
                    + "	        , 0 AS QTY_DD_8	\n"
                    + "	        , 0 AS QTY_DD_9	\n"
                    + "	        , 0 AS QTY_DD_10	\n"
                    + "	        , 0 AS QTY_DD_11	\n"
                    + "	        , 0 AS QTY_DD_12	\n"
                    + "	        , 0 AS QTY_DD_13	\n"
                    + "	        , 0 AS QTY_DD_14	\n"
                    + "	    FROM CABRLOTSTS@RPTTOMES	\n"
                    + "	    WHERE   1=1	\n"
                    + "	    AND FACTORY = '" + cdvFactory.Text + "'  	\n"
                    + "	    AND STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -7)), 'YYYYMMDD')||'220000'	\n"
                    + "	    AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMMDD')||'220000'	\n"
                    + "	    AND OPER_1 >= 'A0000'	\n"
                    + "	    AND OPER_1 <= 'AZ010'	\n"
                    + "	    AND CLOSE_FLAG IN (' ','Y') 	\n"
                    + "	    AND ABR_NO LIKE 'QCN%'	\n"
                    + "	    AND DEL_FLAG = ' '	\n"
                    + "	    UNION ALL	\n"
                    + "	    SELECT '정체Lot건수' AS GUBUN	\n"
                    + "	        , 0 AS QTY_QQ_1	\n"
                    + "	        , 0 AS QTY_QQ_2	\n"
                    + "	        , 0 AS QTY_MM_1	\n"
                    + "	        , 0 AS QTY_MM_2	\n"
                    + "	        , 0 AS QTY_MM_3	\n"
                    + "	        , 0 AS QTY_MM_4	\n"
                    + "	        , 0 AS QTY_MM_5	\n"
                    + "	        , 0 AS QTY_MM_6	\n"
                    + "	        , 0 AS QTY_MM_7	\n"
                    + "	        , ROUND(SUM(CASE WHEN STEP10_TIME >= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 29, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 30, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 31, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 32, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 33, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 34, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 35, 'YYYYMMDD')||'220000'	\n"
                    + "	                                       END	\n"
                    + "	                    AND STEP10_TIME <= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 23, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 24, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 25, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 26, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 27, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 28, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 29, 'YYYYMMDD')||'220000'	\n"
                    + "	                                       END THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                                            THEN 1	\n"
                    + "	                                                            ELSE 0	\n"
                    + "	                                                END	\n"
                    + "	                END) / 7, 1) AS QTY_WW_1 	\n"
                    + "	        , ROUND(SUM(CASE WHEN STEP10_TIME >= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 22, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 23, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 24, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 25, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 26, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 27, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 28, 'YYYYMMDD')||'220000'	\n"
                    + "	                                       END	\n"
                    + "	                    AND STEP10_TIME <= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 16, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 17, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 18, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 19, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 20, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 21, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 22, 'YYYYMMDD')||'220000'	\n"
                    + "	                                       END THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                                            THEN 1	\n"
                    + "	                                                            ELSE 0	\n"
                    + "	                                                END	\n"
                    + "	                END) / 7, 1) AS QTY_WW_2	\n"
                    + "	        , ROUND(SUM(CASE WHEN STEP10_TIME >= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 15, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 16, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 17, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 18, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 19, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 20, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 21, 'YYYYMMDD')||'220000'	\n"
                    + "	                                       END	\n"
                    + "	                    AND STEP10_TIME <= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 9, 'YYYYMMDD')||'220000' 	\n"
                    + "	                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 10, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 11, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 12, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 13, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 14, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 15, 'YYYYMMDD')||'220000'	\n"
                    + "	                                       END THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                                            THEN 1	\n"
                    + "	                                                            ELSE 0	\n"
                    + "	                                                END	\n"
                    + "	                END) / 7, 1) AS QTY_WW_3 	\n"
                    + "	        , ROUND(SUM(CASE WHEN STEP10_TIME >= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 8, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 9, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 10, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 11, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 12, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 13, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 14, 'YYYYMMDD')||'220000'	\n"
                    + "	                                       END 	\n"
                    + "	                    AND STEP10_TIME <= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 2, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 3, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 4, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 5, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 6, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 7, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 8, 'YYYYMMDD')||'220000'	\n"
                    + "	                                       END THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                                            THEN 1	\n"
                    + "	                                                            ELSE 0	\n"
                    + "	                                                END 	\n"
                    + "	                END) / 7, 1) AS QTY_WW_4	\n"
                    + "	        , ROUND(SUM(CASE WHEN STEP10_TIME >= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 1, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 2, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 3, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 4, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 5, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 6, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 7, 'YYYYMMDD')||'220000'	\n"
                    + "	                                       END	\n"
                    + "	                    AND STEP10_TIME <= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 7, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 6, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 5, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 4, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 3, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 2, 'YYYYMMDD')||'220000'	\n"
                    + "	                                                                   WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 1, 'YYYYMMDD')||'220000'	\n"
                    + "	                                       END THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                                            THEN 1	\n"
                    + "	                                                            ELSE 0	\n"
                    + "	                                                END	\n"
                    + "	                END) / 7, 1) AS QTY_WW_5	\n"
                    + "	        , 0 AS QTY_DD_1	\n"
                    + "	        , 0 AS QTY_DD_2	\n"
                    + "	        , 0 AS QTY_DD_3	\n"
                    + "	        , 0 AS QTY_DD_4	\n"
                    + "	        , 0 AS QTY_DD_5	\n"
                    + "	        , 0 AS QTY_DD_6	\n"
                    + "	        , 0 AS QTY_DD_7	\n"
                    + "	        , 0 AS QTY_DD_8	\n"
                    + "	        , 0 AS QTY_DD_9	\n"
                    + "	        , 0 AS QTY_DD_10	\n"
                    + "	        , 0 AS QTY_DD_11	\n"
                    + "	        , 0 AS QTY_DD_12	\n"
                    + "	        , 0 AS QTY_DD_13	\n"
                    + "	        , 0 AS QTY_DD_14	\n"
                    + "	    FROM CABRLOTSTS@RPTTOMES	\n"
                    + "	    WHERE   1=1	\n"
                    + "	    AND FACTORY = '" + cdvFactory.Text + "'  	\n"
                    + "	    AND STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -7)), 'YYYYMMDD')||'220000'	\n"
                    + "	    AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMMDD')||'220000'	\n"
                    + "	    AND OPER_1 >= 'A0000'	\n"
                    + "	    AND OPER_1 <= 'AZ010'	\n"
                    + "	    AND CLOSE_FLAG IN (' ','Y') 	\n"
                    + "	    AND ABR_NO LIKE 'QCN%'	\n"
                    + "	    AND DEL_FLAG = ' '	\n"
                    + "	    UNION ALL	\n"
                    + "	    SELECT '정체Lot건수' AS GUBUN	\n"
                    + "	        , 0 AS QTY_QQ_1	\n"
                    + "	        , 0 AS QTY_QQ_2	\n"
                    + "	        , 0 AS QTY_MM_1	\n"
                    + "	        , 0 AS QTY_MM_2	\n"
                    + "	        , 0 AS QTY_MM_3	\n"
                    + "	        , 0 AS QTY_MM_4	\n"
                    + "	        , 0 AS QTY_MM_5	\n"
                    + "	        , 0 AS QTY_MM_6	\n"
                    + "	        , 0 AS QTY_MM_7	\n"
                    + "	        , 0 AS QTY_WW_1	\n"
                    + "	        , 0 AS QTY_WW_2	\n"
                    + "	        , 0 AS QTY_WW_3	\n"
                    + "	        , 0 AS QTY_WW_4	\n"
                    + "	        , 0 AS QTY_WW_5	\n"
                    + "	        ,SUM(CASE WHEN  STEP10_TIME >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 14, 'YYYYMMDD')||'220000'	\n"
                    + "	                        AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 13, 'YYYYMMDD')||'220000'	\n"
                    + "	                    THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                THEN 1	\n"
                    + "	                                ELSE 0	\n"
                    + "	                         END	\n"
                    + "	             END) AS QTY_DD_1	\n"
                    + "	        ,SUM(CASE WHEN  STEP10_TIME >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 13, 'YYYYMMDD')||'220000'	\n"
                    + "	                        AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 12, 'YYYYMMDD')||'220000'	\n"
                    + "	                    THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                THEN 1	\n"
                    + "	                                ELSE 0	\n"
                    + "	                         END	\n"
                    + "	             END) AS QTY_DD_2	\n"
                    + "	        ,SUM(CASE WHEN  STEP10_TIME >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 12, 'YYYYMMDD')||'220000'	\n"
                    + "	                        AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 11, 'YYYYMMDD')||'220000'	\n"
                    + "	                    THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                THEN 1	\n"
                    + "	                                ELSE 0	\n"
                    + "	                         END	\n"
                    + "	             END) AS QTY_DD_3	\n"
                    + "	        ,SUM(CASE WHEN  STEP10_TIME >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 11, 'YYYYMMDD')||'220000'	\n"
                    + "	                        AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 10, 'YYYYMMDD')||'220000'	\n"
                    + "	                    THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                THEN 1	\n"
                    + "	                                ELSE 0	\n"
                    + "	                         END	\n"
                    + "	             END) AS QTY_DD_4	\n"
                    + "	        ,SUM(CASE WHEN  STEP10_TIME >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 10, 'YYYYMMDD')||'220000'	\n"
                    + "	                        AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 9, 'YYYYMMDD')||'220000'	\n"
                    + "	                    THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                THEN 1	\n"
                    + "	                                ELSE 0	\n"
                    + "	                         END	\n"
                    + "	             END) AS QTY_DD_5	\n"
                    + "	        ,SUM(CASE WHEN  STEP10_TIME >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 9, 'YYYYMMDD')||'220000'	\n"
                    + "	                        AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 8, 'YYYYMMDD')||'220000'	\n"
                    + "	                    THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                THEN 1	\n"
                    + "	                                ELSE 0	\n"
                    + "	                         END	\n"
                    + "	             END) AS QTY_DD_6	\n"
                    + "	        ,SUM(CASE WHEN  STEP10_TIME >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 8, 'YYYYMMDD')||'220000'	\n"
                    + "	                        AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 7, 'YYYYMMDD')||'220000'	\n"
                    + "	                    THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                THEN 1	\n"
                    + "	                                ELSE 0	\n"
                    + "	                         END	\n"
                    + "	             END) AS QTY_DD_7	\n"
                    + "	        ,SUM(CASE WHEN  STEP10_TIME >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 7, 'YYYYMMDD')||'220000'	\n"
                    + "	                        AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 6, 'YYYYMMDD')||'220000'	\n"
                    + "	                    THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                THEN 1	\n"
                    + "	                                ELSE 0	\n"
                    + "	                         END	\n"
                    + "	             END) AS QTY_DD_8	\n"
                    + "	        ,SUM(CASE WHEN  STEP10_TIME >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 6, 'YYYYMMDD')||'220000'	\n"
                    + "	                        AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 5, 'YYYYMMDD')||'220000'	\n"
                    + "	                    THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                THEN 1	\n"
                    + "	                                ELSE 0	\n"
                    + "	                         END	\n"
                    + "	             END) AS QTY_DD_9	\n"
                    + "	        ,SUM(CASE WHEN  STEP10_TIME >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 5, 'YYYYMMDD')||'220000'	\n"
                    + "	                        AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 4, 'YYYYMMDD')||'220000'	\n"
                    + "	                    THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                THEN 1	\n"
                    + "	                                ELSE 0	\n"
                    + "	                         END	\n"
                    + "	             END) AS QTY_DD_10	\n"
                    + "	        ,SUM(CASE WHEN  STEP10_TIME >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 4, 'YYYYMMDD')||'220000'	\n"
                    + "	                        AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 3, 'YYYYMMDD')||'220000'	\n"
                    + "	                    THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                THEN 1	\n"
                    + "	                                ELSE 0	\n"
                    + "	                         END	\n"
                    + "	             END) AS QTY_DD_11	\n"
                    + "	        ,SUM(CASE WHEN  STEP10_TIME >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 3, 'YYYYMMDD')||'220000'	\n"
                    + "	                        AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 2, 'YYYYMMDD')||'220000'	\n"
                    + "	                    THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                THEN 1	\n"
                    + "	                                ELSE 0	\n"
                    + "	                         END	\n"
                    + "	             END) AS QTY_DD_12	\n"
                    + "	        ,SUM(CASE WHEN  STEP10_TIME >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 2, 'YYYYMMDD')||'220000'	\n"
                    + "	                        AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 1, 'YYYYMMDD')||'220000'	\n"
                    + "	                    THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                THEN 1	\n"
                    + "	                                ELSE 0	\n"
                    + "	                         END	\n"
                    + "	             END) AS QTY_DD_13	\n"
                    + "	        ,SUM(CASE WHEN  STEP10_TIME >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 1, 'YYYYMMDD')||'220000'	\n"
                    + "	                        AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 0, 'YYYYMMDD')||'220000'	\n"
                    + "	                    THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                    + "	                                THEN 1	\n"
                    + "	                                ELSE 0	\n"
                    + "	                         END	\n"
                    + "	             END) AS QTY_DD_14	\n"
                    + "	    FROM CABRLOTSTS@RPTTOMES	\n"
                    + "	    WHERE   1=1	\n"
                    + "	    AND FACTORY = '" + cdvFactory.Text + "'	\n"
                    + "	    AND STEP10_TIME >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 14, 'YYYYMMDD')||'220000' \n"
                    + "	    AND STEP10_TIME <= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMMDD')||'060000'	\n"
                    + "	    AND OPER_1 >= 'A0000'	\n"
                    + "	    AND OPER_1 <= 'AZ010'	\n"
                    + "	    AND CLOSE_FLAG IN (' ','Y')	\n"
                    + "	    AND ABR_NO LIKE 'QCN%'	\n"
                    + "	    AND DEL_FLAG = ' '	\n"
                    + "	)GROUP BY GUBUN	\n";


                if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                {
                    System.Windows.Forms.Clipboard.SetText(QRY.Replace((char)Keys.Tab, ' '));
                }

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", QRY.Replace((char)Keys.Tab, ' '));

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return false;
                }

                spdData.DataSource = dt;
                spdData.RPT_AutoFit(false);

                fnMakeChart(spdData, 0);

                return true;
            }
            catch (Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
        }

        private bool fnLotListDataFind()
        {
            /****************************************************
             * Comment : DataBase에 저장된 데이터를 조회한다.(QCN 24시간 이상 정체 Lot List)
             * 
             * Created By : 김선호(2012-07-27-금요일)
             * 
             * Modified By :
             ****************************************************/
            DataTable dt = null;
            string QRY = "";

            try
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                LoadingPopUp.LoadIngPopUpShow(this);

                // Sheet / Listview를 초기화 한다.
                CmnInitFunction.ClearList(spdData, true);

                string date = cdvDate.Value.ToString("yyyyMMddHHmmss");

                QRY = "	SELECT *	\n"
                    + "	FROM	\n"
                    + "	(	\n"
                    + "	    SELECT TO_DATE(SUBSTR(STS.STEP10_TIME,0,8), 'YYYYMMDD') AS START_DATE	\n"
                    + "	        , STS.ABR_NO, STS.CUS_ID, MAT.MAT_GRP_3 AS PKG, STS.LOT_ID, STS.OPER_1	\n"
                    + "	        , TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS') AS START_TIME	\n"
                    + "	        , ROUND( (DECODE(STS.CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(STS.CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STS.STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24, 2) AS DUR_TIME	\n"
                    + "	        , (SELECT DESC_1 FROM CABRDFTDEF@RPTTOMES WHERE DEFECT_CODE = STS.DEFECT_CODE) AS DEFECT	\n"
                    + "	        , (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + cdvFactory.Text + "' AND TABLE_NAME = 'H_ABR_STEP' AND KEY_1 = STS.CUR_STEP) AS STEP	\n"
                    + "	        , (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + cdvFactory.Text + "' AND TABLE_NAME = 'H_DEPARTMENT' AND KEY_1 = STS.CUR_DEPT) AS DEPT	\n"
                    + "	    FROM CABRLOTSTS@RPTTOMES STS, MWIPMATDEF@RPTTOMES MAT	\n"
                    + "	    WHERE 1=1	\n"
                    + "	    AND STS.FACTORY=MAT.FACTORY 	\n"
                    + "	    AND STS.MAT_ID=MAT.MAT_ID	\n"
                    + "	    AND STS.FACTORY = '" + cdvFactory.Text + "'	\n"
                    + "	    AND STS.STEP10_TIME >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 30, 'YYYYMMDD')||'220000'	\n"
                    + "	    AND STS.STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMMDD')||'060000'	\n"
                    + "	    AND STS.OPER_1 >= 'A0000'	\n"
                    + "	    AND STS.OPER_1 <= 'AZ010'	\n"
                    + "	    AND STS.CLOSE_FLAG = ' '	\n"
                    + "	    AND STS.ABR_NO LIKE 'QCN%'	\n"
                    + "	    AND STS.DEL_FLAG = ' '	\n"
                    + "	    )	\n"
                    + "	WHERE DUR_TIME >= 24	\n";

                if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                {
                    System.Windows.Forms.Clipboard.SetText(QRY.Replace((char)Keys.Tab, ' '));
                }

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", QRY.Replace((char)Keys.Tab, ' '));

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    //return false;
                }
                else
                {
                    spdData.DataSource = dt;
                    spdData.RPT_AutoFit(false);
                }

                ShowChart(0);

                return true;
            }
            catch (Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
        }

        //private void InitChart()
        //{
            //Legend legend1 = null;
            //Legend legend2 = null;

            //chart1.Series.Clear();
            //chart2.Series.Clear();
            //chart3.Series.Clear();

            //legend1 = new Legend("Default");
            //legend2 = new Legend("Default");

            ////chart1.Legends.Add(inspLegend);
            //chart2.Legends.Add(legend1);
            //chart3.Legends.Add(legend2);

            //legend1.Docking = Docking.Top;
            //legend1.Alignment = StringAlignment.Center;
            //legend2.Docking = Docking.Top;
            //legend2.Alignment = StringAlignment.Center;
            
            //chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            //chart2.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            //chart3.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
        //}

        private void fnMakeChart(Miracom.SmartWeb.UI.Controls.udcFarPoint SS, int iselrow)
        {
            /****************************************************
             * Comment : SS의 데이터를 Chart에 표시한다.
             * 
             * Created By : 김선호(2012-07-26-목요일)
             * 
             * Modified By : 오득연 ChartFX -> MS Chart로 변경 (2015-03-13)
             ****************************************************/
            int[] ich_mm = new int[28]; int[] icols_mm = new int[28]; int[] irows_mm = new int[1]; string[] stitle_mm = new string[1];
            int icol = 0, irow = 0;
            try
            {
                // 기존 CHART
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                // SS에 데이터가 없으면 종료한다.
                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                //cf01.RPT_1_ChartInit(); cf02.RPT_1_ChartInit(); cf03.RPT_1_ChartInit();
                //cf01.RPT_2_ClearData(); cf02.RPT_2_ClearData(); cf03.RPT_2_ClearData();
                udcMSChart1.RPT_1_ChartInit();
                udcMSChart2.RPT_1_ChartInit();
                udcMSChart3.RPT_1_ChartInit();
                udcMSChart1.RPT_2_ClearData(); udcMSChart2.RPT_2_ClearData(); udcMSChart3.RPT_2_ClearData();

                //cf01.AxisX.Title.Text = "[QCN Stagnant Lot Trend over 24 hours]";
                //cf01.AxisX.Staggered = false;

                for (icol = 0; icol < ich_mm.Length; icol++)
                {
                    ich_mm[icol] = icol + 1;
                    icols_mm[icol] = icol + 1;
                }
                // 2012-07-26-김선호 : 선택한 Row에서 아래로 1개의 Row만 Display한다.
                for (irow = 0; irow < 1; irow++)
                {
                    if ((iselrow + irow) >= SS.Sheets[0].RowCount)
                    {
                        break;
                    }
                    irows_mm[irow] = iselrow + irow;
                    stitle_mm[irow] = SS.Sheets[0].Cells[iselrow + irow, 0].Text;
                }

                String legendQCN = "건수 [단위 : 건]";
                //cf01.RPT_3_OpenData(1, icols_mm.Length);
                //double max1 = cf01.RPT_4_AddData(SS, new int[] { 0 }, icols_mm, SeriseType.Rows);
                udcMSChart1.RPT_3_OpenData(1, icols_mm.Length);
                double max1 = udcMSChart1.RPT_4_AddData(SS, new int[] { 0 }, icols_mm, SeriseType.Rows);

                //cf01.RPT_5_CloseData();
                //cf01.RPT_6_SetGallery(ChartType.Bar, 0, 1, legendQCN, AsixType.Y, DataTypes.Double, max1 * 1.2);
                //cf01.Series[0].PointLabels = true;
                //cf01.Series[0].PointLabelAlign = SoftwareFX.ChartFX.LabelAlign.Bottom | SoftwareFX.ChartFX.LabelAlign.Center;
                //cf01.Series[0].PointLabelColor = Color.Blue;
                //cf01.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, icols_mm);
                ////cf01.RPT_8_SetSeriseLegend(new string[] { "24시간 이상 정체 건수" }, SoftwareFX.ChartFX.Docked.Top);

                //cf01.AxisY.Gridlines = true;
                //cf01.AxisY.DataFormat.Decimals = 1;
                //cf01.AxisY.Step = 2;
                //cf01.AxisY.Max = 10.0;
                //cf01.AxisY.Min = 0.0;

                udcMSChart1.RPT_6_SetGallery(SeriesChartType.Column, 0, 1, legendQCN, AsixType.Y, DataTypes.Double, max1 * 1.2);
                udcMSChart1.Series[0]["DrawingStyle"] = "Cylinder";
                udcMSChart1.Series[0].IsValueShownAsLabel = true;
                udcMSChart1.Series[0].IsVisibleInLegend = false;
                udcMSChart1.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, icols_mm);

                udcMSChart1.ChartAreas[0].AxisX.Title = "[QCN Stagnant Lot Trend over 24 hours]";
                udcMSChart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                udcMSChart1.ChartAreas[0].AxisY.IsInterlaced = true;


                // MS CHART (2015-03-13)
                //Series series1 = null;
                
                //chart1.Series.Clear();
                //chart2.Series.Clear();
                //chart3.Series.Clear();

                //series1 = new Series();
                //chart1.Series.Add(series1);
                
                ////chart1.Series[0].Points.Clear();
                //chart1.ChartAreas[0].AxisX.Title = "[QCN Stagnant Lot Trend over 24 hours]";
                //chart1.ChartAreas[0].AxisY.Title = "건수 [단위 : 건]";
                //chart1.ChartAreas[0].AxisY.IsInterlaced = true;
                //chart1.ChartAreas[0].AxisY.InterlacedColor = Color.FromArgb(120, Color.DarkGray);
                //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0.0}";
                //chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{0:0.0}";

                //chart1.Series[0]["DrawingStyle"] = "Cylinder";
                //chart1.Series[0].ChartType = SeriesChartType.Column;
                //chart1.Series[0].IsVisibleInLegend = false;
                //chart1.Series[0].IsValueShownAsLabel = true;
                //chart1.Series[0].LabelForeColor = Color.Blue;
                
                //for (int a = 0; a < icols_mm.Length; a++)
                //{//SS.Sheets[0].ColumnCount                    

                //    string header = SS.ActiveSheet.ColumnHeader.Cells[irows_mm[0], a+1].Text;
                //    double value = Convert.ToDouble(SS.ActiveSheet.Cells[irows_mm[0], a+1].Value);

                //    chart1.Series[0].Points.AddXY(header, value);
                //}

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
        }

        private void ShowChart(int rowCount)
        {
            DataTable dt_graph = null;
            //cf01.RPT_1_ChartInit(); cf02.RPT_1_ChartInit(); cf03.RPT_1_ChartInit();
            //cf01.RPT_2_ClearData(); cf02.RPT_2_ClearData(); cf03.RPT_2_ClearData();

            udcMSChart1.RPT_1_ChartInit();
            udcMSChart2.RPT_1_ChartInit();
            udcMSChart3.RPT_1_ChartInit();
            udcMSChart1.RPT_2_ClearData(); udcMSChart2.RPT_2_ClearData(); udcMSChart3.RPT_2_ClearData();

            //Series series2 = null;
            //Series series3 = null;
            chart1.Series.Clear();
            chart2.Series.Clear();        
            chart3.Series.Clear();

            int monthCount = 3;

            #region Chart No.1 - 24시간 이상 정체 QCN 공정별 누적 현황

            //// 그래프 설정 //                      
            //cf02.AxisY.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            //cf02.AxisX.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            //cf02.PointLabels = true;
            //cf02.AxisY.DataFormat.Decimals = 0;
            //cf02.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
            //cf02.AxisX.Staggered = false;

            ////cf02.PointLabelColor 
            //cf02.PointLabelColor = System.Drawing.Color.Black;

            //// ToolBar 보여주기
            ////cf02.ToolBar = true;

            //// contion attribute 를 이용한 0 point label hidden
            //SoftwareFX.ChartFX.ConditionalAttributes contion = cf02.ConditionalAttributes[0];
            //contion.Condition.From = 0;
            //contion.Condition.To = 0;
            //contion.PointLabels = false;

            //cf02.MultipleColors = false;
            dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(0));

            if (dt_graph.Rows.Count == 0)
            {
                dt_graph.Dispose();
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
            }
            else
            {
                // DB에서 얻어온 컬럼헤더 이름을 변경 시킨다.
                for (int iCol = 1; iCol < dt_graph.Columns.Count; iCol++)
                {
                    dt_graph.Columns[iCol].ColumnName = System.DateTime.Now.AddMonths(-monthCount).Month.ToString() + "월";
                    monthCount--;
                }

                // MS CHART 
                ////chart2.Series[0].Points.Clear();
                //chart2.ChartAreas[0].AxisX.Title = "[Cumulative Status by Stagnant QCN Process over 24 Hours]";
                //chart2.ChartAreas[0].AxisY.Title = "(Count)";
                //chart2.ChartAreas[0].AxisY.IsInterlaced = true;
                //chart2.ChartAreas[0].AxisY.InterlacedColor = Color.FromArgb(120, Color.DarkGray);

                //chart2.DataSource = dt_graph;
                //for (int i = 0; i < dt_graph.Columns.Count-1; i++)
                //{
                //    series2 = new Series();
                //    series2.Name = Convert.ToString(dt_graph.Columns[i + 1]);

                //    series2.IsValueShownAsLabel = true;    //값이 0인 경우 표시 안하는 방법이?
                //    series2.IsVisibleInLegend = true;
                //    series2.ChartType = SeriesChartType.Column;

                //    chart2.Series.Add(series2);

                //    chart2.Series[i].Legend = "Default";
                //    chart2.Series[i]["DrawingStyle"] = "Cylinder";

                //    chart2.Series[i].XValueMember = Convert.ToString(dt_graph.Columns[0]);
                //    chart2.Series[i].YValueMembers = Convert.ToString(dt_graph.Columns[i+1]);
                //}
                //chart2.DataBind();


                ////기존 CHART
                //cf02.DataSource = dt_graph;

                //cf02.SerLegBox = true;
                //cf02.AxisY.Max = cf02.AxisY.Max * 1.1;
                //cf02.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;

                //cf02.AxisX.Title.Text = "[Cumulative Status by Stagnant QCN Process over 24 Hours]";
                //cf02.AxisX.Title.Alignment = StringAlignment.Center;
                //cf02.AxisY.Title.Text = "(Count)";
                //cf02.AxisY.Title.Alignment = StringAlignment.Far;

                udcMSChart2.DataSource = dt_graph;
                udcMSChart2.RPT_3_OpenData(dt_graph.Columns.Count - 1, dt_graph.Rows.Count);

                for (int i = 0; i < dt_graph.Columns.Count - 1; i++)
                {
                    udcMSChart2.Series[i].XValueMember = Convert.ToString(dt_graph.Columns[0]);
                    udcMSChart2.Series[i].YValueMembers = Convert.ToString(dt_graph.Columns[i + 1]);
                    
                    udcMSChart2.Series[i].IsValueShownAsLabel = true;
                    udcMSChart2.Series[i].IsVisibleInLegend = true;
                    udcMSChart2.Series[i]["DrawingStyle"] = "Cylinder";
                    udcMSChart2.Series[i].LegendText = dt_graph.Columns[i + 1].ToString();
                }
                udcMSChart2.DataBind();

                udcMSChart2.Series[0].ChartType = SeriesChartType.Column;

                udcMSChart2.Legends[0].Docking = Docking.Top;
                udcMSChart2.Legends[0].Alignment = StringAlignment.Center;
                udcMSChart2.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                udcMSChart2.ChartAreas[0].AxisX.Title = "[Cumulative Status by Stagnant QCN Process over 24 Hours]";                
                udcMSChart2.ChartAreas[0].AxisY.IsInterlaced = true;
            }

            #endregion

            dt_graph = null;

            #region Chart No.2 - 24시간 이상 정체 QCN 불량 Mode별 누적 TOP7

            //// 그래프 설정 //                      
            //cf03.AxisY.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            //cf03.AxisX.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            //cf03.PointLabels = true;
            //cf03.AxisY.DataFormat.Decimals = 0;
            //cf03.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
            //cf03.AxisX.Staggered = true;

            ////cf03.PointLabelColor 
            //cf03.PointLabelColor = System.Drawing.Color.Black;

            //// ToolBar 보여주기
            ////cf03.ToolBar = true;

            //// contion attribute 를 이용한 0 point label hidden
            //contion = cf03.ConditionalAttributes[0];
            //contion.Condition.From = 0;
            //contion.Condition.To = 0;
            //contion.PointLabels = false;

            //cf03.MultipleColors = false;
            
            dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(1));
            if (dt_graph.Rows.Count == 0)
            {
                dt_graph.Dispose();
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                return;
            }

            // DB에서 얻어온 컬럼헤더 이름을 변경 시킨다.
            monthCount = 3;
            for (int iCol = 1; iCol < dt_graph.Columns.Count; iCol++)
            {
                dt_graph.Columns[iCol].ColumnName = System.DateTime.Now.AddMonths(-monthCount).Month.ToString() + "월";
                monthCount--;
            }

            //Total 컬럼 제거
            dt_graph.Columns.RemoveAt(5);


            // MS CHART
            ////chart3.Series[0].Points.Clear();
            //chart3.ChartAreas[0].AxisX.Title = "[Cumulative TOP 7 by QCN Failure Mode over 24 Hours]";
            //chart3.ChartAreas[0].AxisY.Title = "(Count)";
            //chart3.ChartAreas[0].AxisY.IsInterlaced = true;
            //chart3.ChartAreas[0].AxisY.InterlacedColor = Color.FromArgb(120, Color.DarkGray);

            //chart3.DataSource = dt_graph;
            //for (int i = 0; i < dt_graph.Columns.Count - 1; i++)
            //{
            //    series3 = new Series();
            //    series3.Name = Convert.ToString(dt_graph.Columns[i + 1]);

            //    series3.IsValueShownAsLabel = true;    //값이 0인 경우 표시 안하는 방법이?
            //    series3.IsVisibleInLegend = true;
            //    series3.ChartType = SeriesChartType.Column;

            //    chart3.Series.Add(series3);

            //    chart3.Series[i].Legend = "Default";
            //    chart3.Series[i]["DrawingStyle"] = "Cylinder";
            //    chart3.Series[i].XValueMember = Convert.ToString(dt_graph.Columns[0]);
            //    chart3.Series[i].YValueMembers = Convert.ToString(dt_graph.Columns[i + 1]);
            //}
            //chart3.DataBind();
            

            ////기존 CHART
            //cf03.DataSource = dt_graph;

            //cf03.SerLegBox = true;
            //cf03.AxisY.Max = cf03.AxisY.Max * 1.1;
            //cf03.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;

            //cf03.AxisX.Title.Text = "[Cumulative TOP 7 by QCN Failure Mode over 24 Hours]";
            //cf03.AxisX.Title.Alignment = StringAlignment.Center;
            //cf03.AxisY.Title.Text = "(Count)";
            //cf03.AxisY.Title.Alignment = StringAlignment.Far;

            udcMSChart3.DataSource = dt_graph;
            udcMSChart3.RPT_3_OpenData(dt_graph.Columns.Count - 1, dt_graph.Rows.Count);

            for (int i = 0; i < dt_graph.Columns.Count - 1; i++)
            {
                udcMSChart3.Series[i].XValueMember = Convert.ToString(dt_graph.Columns[0]);
                udcMSChart3.Series[i].YValueMembers = Convert.ToString(dt_graph.Columns[i + 1]);

                udcMSChart3.Series[i].IsValueShownAsLabel = true;
                udcMSChart3.Series[i].IsVisibleInLegend = true;
                udcMSChart3.Series[i]["DrawingStyle"] = "Cylinder";
                udcMSChart3.Series[i].LegendText = dt_graph.Columns[i + 1].ToString();
            }
            udcMSChart3.DataBind();

            udcMSChart3.Series[0].ChartType = SeriesChartType.Column;

            udcMSChart3.Legends[0].Docking = Docking.Top;
            udcMSChart3.Legends[0].Alignment = StringAlignment.Center;
            udcMSChart3.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            udcMSChart3.ChartAreas[0].AxisX.Title = "[Cumulative TOP 7 by QCN Failure Mode over 24 Hours]";
            udcMSChart3.ChartAreas[0].AxisY.IsInterlaced = true;

            #endregion
        }

        #region Chart 생성을 위한 쿼리

        private string MakeSqlString(int Step)
        {
            string QRY = "";

            string date = cdvDate.Value.ToString("yyyyMMddHHmmss");

            switch (Step)
            {
                case 0:
                    {
                        #region Chart No.1

                        QRY = "	SELECT 'W/B' AS OPER 	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'YYYYMMDD')||'220000'	\n"
                            + "	                        AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_1	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'YYYYMMDD')||'220000'	\n"
                            + "	                        AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_2	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'YYYYMMDD')||'220000'	\n"
                            + "	            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'YYYYMMDD')||'220000'	\n"
                            + "	             THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                    THEN 1	\n"
                            + "	                                    ELSE 0	\n"
                            + "	                  END	\n"
                            + "	    END) AS QTY_MM_3	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'YYYYMMDD')||'220000'	\n"
                            + "	                    AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -0)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_4	\n"
                            + "	FROM CABRLOTSTS@RPTTOMES	\n"
                            + "	    WHERE 1=1	\n"
                            + "	    AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'	\n"
                            + "	    AND STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'YYYYMMDD')||'220000'	\n"
                            + "	    AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMMDD')||'060000'	\n"
                            + "	    AND OPER_1 >= 'A0600'	\n"
                            + "	    AND OPER_1 <= 'A0609'	\n"
                            + "	    AND CLOSE_FLAG IN ( ' ', 'Y')	\n"
                            + "	    AND ABR_NO LIKE 'QCN%'	\n"
                            + "	    AND DEL_FLAG = ' '	\n"
                            + "	UNION ALL	\n"
                            + "	SELECT 'D/A' AS OPER 	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'YYYYMMDD')||'220000'	\n"
                            + "	                        AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_1	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'YYYYMMDD')||'220000'	\n"
                            + "	                        AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_2	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'YYYYMMDD')||'220000'	\n"
                            + "	            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'YYYYMMDD')||'220000'	\n"
                            + "	             THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                    THEN 1	\n"
                            + "	                                    ELSE 0	\n"
                            + "	                  END	\n"
                            + "	    END) AS QTY_MM_3	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'YYYYMMDD')||'220000'	\n"
                            + "	                    AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -0)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_4	\n"
                            + "	FROM CABRLOTSTS@RPTTOMES	\n"
                            + "	    WHERE 1=1	\n"
                            + "	    AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'	\n"
                            + "	    AND STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'YYYYMMDD')||'220000'	\n"
                            + "	    AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMMDD')||'060000'	\n"
                            + "	    AND OPER_1 >= 'A0400'	\n"
                            + "	    AND OPER_1 <= 'A0409'	\n"
                            + "	    AND CLOSE_FLAG IN ( ' ', 'Y')	\n"
                            + "	    AND ABR_NO LIKE 'QCN%'	\n"
                            + "	    AND DEL_FLAG = ' '	\n"
                            + "	UNION ALL	\n"
                            + "	SELECT 'M/D' AS OPER 	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'YYYYMMDD')||'220000'	\n"
                            + "	                        AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_1	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'YYYYMMDD')||'220000'	\n"
                            + "	                        AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_2	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'YYYYMMDD')||'220000'	\n"
                            + "	            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'YYYYMMDD')||'220000'	\n"
                            + "	             THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                    THEN 1	\n"
                            + "	                                    ELSE 0	\n"
                            + "	                  END	\n"
                            + "	    END) AS QTY_MM_3	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'YYYYMMDD')||'220000'	\n"
                            + "	                    AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -0)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_4	\n"
                            + "	FROM CABRLOTSTS@RPTTOMES	\n"
                            + "	    WHERE 1=1	\n"
                            + "	    AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'	\n"
                            + "	    AND STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'YYYYMMDD')||'220000'	\n"
                            + "	    AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMMDD')||'060000'	\n"
                            + "	    AND OPER_1 = 'A1000'	\n"
                            + "	    AND CLOSE_FLAG IN ( ' ', 'Y')	\n"
                            + "	    AND ABR_NO LIKE 'QCN%'	\n"
                            + "	    AND DEL_FLAG = ' '	\n"
                            + "	UNION ALL	\n"
                            + "	SELECT 'IVI GATE' AS OPER 	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'YYYYMMDD')||'220000'	\n"
                            + "	                        AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_1	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'YYYYMMDD')||'220000'	\n"
                            + "	                        AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_2	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'YYYYMMDD')||'220000'	\n"
                            + "	            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'YYYYMMDD')||'220000'	\n"
                            + "	             THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                    THEN 1	\n"
                            + "	                                    ELSE 0	\n"
                            + "	                  END	\n"
                            + "	    END) AS QTY_MM_3	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'YYYYMMDD')||'220000'	\n"
                            + "	                    AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -0)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_4	\n"
                            + "	FROM CABRLOTSTS@RPTTOMES	\n"
                            + "	    WHERE 1=1	\n"
                            + "	    AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'	\n"
                            + "	    AND STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'YYYYMMDD')||'220000'	\n"
                            + "	    AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMMDD')||'060000'	\n"
                            + "	    AND OPER_1 >= 'A0800'	\n"
                            + "	    AND OPER_1 <= 'A0809'	\n"
                            + "	    AND CLOSE_FLAG IN ( ' ', 'Y')	\n"
                            + "	    AND ABR_NO LIKE 'QCN%'	\n"
                            + "	    AND DEL_FLAG = ' '	\n"
                            + "	UNION ALL	\n"
                            + "	SELECT 'PVI GATE' AS OPER 	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'YYYYMMDD')||'220000'	\n"
                            + "	                        AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_1	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'YYYYMMDD')||'220000'	\n"
                            + "	                        AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_2	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'YYYYMMDD')||'220000'	\n"
                            + "	            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'YYYYMMDD')||'220000'	\n"
                            + "	             THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                    THEN 1	\n"
                            + "	                                    ELSE 0	\n"
                            + "	                  END	\n"
                            + "	    END) AS QTY_MM_3	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'YYYYMMDD')||'220000'	\n"
                            + "	                    AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -0)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_4	\n"
                            + "	FROM CABRLOTSTS@RPTTOMES	\n"
                            + "	    WHERE 1=1	\n"
                            + "	    AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'	\n"
                            + "	    AND STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'YYYYMMDD')||'220000'	\n"
                            + "	    AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMMDD')||'060000'	\n"
                            + "	    AND OPER_1 = 'A2100'	\n"
                            + "	    AND CLOSE_FLAG IN ( ' ', 'Y')	\n"
                            + "	    AND ABR_NO LIKE 'QCN%'	\n"
                            + "	    AND DEL_FLAG = ' '	\n"
                            + "	UNION ALL	\n"
                            + "	SELECT 'FINISH' AS OPER 	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'YYYYMMDD')||'220000'	\n"
                            + "	                        AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_1	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'YYYYMMDD')||'220000'	\n"
                            + "	                        AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_2	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'YYYYMMDD')||'220000'	\n"
                            + "	            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'YYYYMMDD')||'220000'	\n"
                            + "	             THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                    THEN 1	\n"
                            + "	                                    ELSE 0	\n"
                            + "	                  END	\n"
                            + "	    END) AS QTY_MM_3	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'YYYYMMDD')||'220000'	\n"
                            + "	                    AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -0)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_4	\n"
                            + "	FROM CABRLOTSTS@RPTTOMES	\n"
                            + "	    WHERE 1=1	\n"
                            + "	    AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'	\n"
                            + "	    AND STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'YYYYMMDD')||'220000'	\n"
                            + "	    AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMMDD')||'060000'	\n"
                            + "	    AND OPER_1 >= 'A1040'	\n"
                            + "	    AND OPER_1 <= 'A2050'	\n"
                            + "	    AND CLOSE_FLAG IN ( ' ', 'Y')	\n"
                            + "	    AND ABR_NO LIKE 'QCN%'	\n"
                            + "	    AND DEL_FLAG = ' '	\n"
                            + "	UNION ALL	\n"
                            + "	SELECT '2ND GATE' AS OPER 	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'YYYYMMDD')||'220000'	\n"
                            + "	                        AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_1	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'YYYYMMDD')||'220000'	\n"
                            + "	                        AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_2	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'YYYYMMDD')||'220000'	\n"
                            + "	            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'YYYYMMDD')||'220000'	\n"
                            + "	             THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                    THEN 1	\n"
                            + "	                                    ELSE 0	\n"
                            + "	                  END	\n"
                            + "	    END) AS QTY_MM_3	\n"
                            + "	    , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'YYYYMMDD')||'220000'	\n"
                            + "	                    AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -0)), 'YYYYMMDD')||'220000'	\n"
                            + "	                     THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                            THEN 1	\n"
                            + "	                                            ELSE 0	\n"
                            + "	                          END	\n"
                            + "	            END) AS QTY_MM_4	\n"
                            + "	FROM CABRLOTSTS@RPTTOMES	\n"
                            + "	    WHERE 1=1	\n"
                            + "	    AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'	\n"
                            + "	    AND STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'YYYYMMDD')||'220000'	\n"
                            + "	    AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMMDD')||'060000'	\n"
                            + "	    AND OPER_1 = 'A0300'	\n"
                            + "	    AND CLOSE_FLAG IN ( ' ', 'Y')	\n"
                            + "	    AND ABR_NO LIKE 'QCN%'	\n"
                            + "	    AND DEL_FLAG = ' '	\n";

                        #endregion
                    }
                    break;

                case 1:
                    {
                        #region Chart No.2

                        QRY = "	SELECT *	\n"
                            + "	FROM	\n"
                            + "	(	\n"
                            + "	    SELECT DEFECT, QTY_MM_1, QTY_MM_2, QTY_MM_3, QTY_MM_4	\n"
                            + "	        ,SUM(QTY_MM_1 + QTY_MM_2 + QTY_MM_3 + QTY_MM_4) AS TOTAL	\n"
                            + "	    FROM	\n"
                            + "	    (	\n"
                            + "	        SELECT DESC_1 AS DEFECT	\n"
                            + "	            , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'YYYYMMDD')||'220000'	\n"
                            + "	                                AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'YYYYMMDD')||'220000'	\n"
                            + "	                             THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                                    THEN 1	\n"
                            + "	                                                    ELSE 0	\n"
                            + "	                                  END	\n"
                            + "	                             ELSE 0	\n"
                            + "	                    END) AS QTY_MM_1	\n"
                            + "	            , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'YYYYMMDD')||'220000'	\n"
                            + "	                                AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'YYYYMMDD')||'220000'	\n"
                            + "	                             THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                                    THEN 1	\n"
                            + "	                                                    ELSE 0	\n"
                            + "	                                  END	\n"
                            + "	                             ELSE 0	\n"
                            + "	                    END) AS QTY_MM_2	\n"
                            + "	            , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'YYYYMMDD')||'220000'	\n"
                            + "	                                AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'YYYYMMDD')||'220000'	\n"
                            + "	                             THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                                    THEN 1	\n"
                            + "	                                                    ELSE 0	\n"
                            + "	                                  END	\n"
                            + "	                             ELSE 0	\n"
                            + "	            END) AS QTY_MM_3	\n"
                            + "	            , SUM(CASE WHEN STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'YYYYMMDD')||'220000'	\n"
                            + "	                            AND STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -0)), 'YYYYMMDD')||'220000'	\n"
                            + "	                             THEN CASE WHEN (DECODE(CLOSE_TIME, ' ', TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), TO_DATE(CLOSE_TIME,'YYYYMMDD HH24:MI:SS')) - TO_DATE(STEP10_TIME,'YYYYMMDD HH24:MI:SS')) * 24 >= 24	\n"
                            + "	                                                    THEN 1	\n"
                            + "	                                                    ELSE 0	\n"
                            + "	                                  END	\n"
                            + "	                             ELSE 0	\n"
                            + "	                    END) AS QTY_MM_4	\n"
                            + "	        FROM CABRLOTSTS@RPTTOMES STS, CABRDFTDEF@RPTTOMES DEF	\n"
                            + "	        WHERE 1=1	\n"
                            + "	        AND STS.FACTORY = DEF.FACTORY	\n"
                            + "	        AND STS.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'	\n"
                            + "	        AND STS.DEFECT_CODE = DEF.DEFECT_CODE	\n"
                            + "	        AND STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'YYYYMMDD')||'220000'	\n"
                            + "	        AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMMDD')||'060000'	\n"
                            + "	        AND STS.OPER_1 >= 'A0000'	\n"
                            + "	        AND STS.OPER_1 <= 'AZ010'	\n"
                            + "	        AND CLOSE_FLAG IN ( ' ', 'Y')	\n"
                            + "	        AND ABR_NO LIKE 'QCN%'	\n"
                            + "	        AND STS.DEL_FLAG = ' '	\n"
                            + "	        GROUP BY DEF.DESC_1	\n"
                            + "	    )GROUP BY DEFECT, QTY_MM_1, QTY_MM_2, QTY_MM_3, QTY_MM_4	\n"
                            + "	    ORDER BY TOTAL DESC	\n"
                            + "	    )	\n"
                            + "	WHERE ROWNUM <= 7	\n";
                        #endregion
                    }
                    break;
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(QRY);
            }

            return QRY.Replace((char)Keys.Tab, ' ');
        }

        #endregion

        #endregion


        #region " Form Event "

        private void btnView_Click(object sender, EventArgs e)
        {
            /****************************************************
             * Comment : View Button을 클릭하면
             * 
             * Created By : 김선호(2012-07-26-목요일)
             * 
             * Modified By : 
             ****************************************************/
            try
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                fnSSInitial(spdData);

                this.Refresh();

                switch (cboGraph.SelectedIndex)
                {
                    case 0:
                        fnDataFind();
                        break;

                    case 1:
                        fnLotListDataFind();
                        break;
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            /****************************************************
             * Comment : Excel Export Button을 클릭하면
             * 
             * Created By : 김선호(2012-07-26-목요일)
             * 
             * Modified By :
             ****************************************************/
            try
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                spdData.ExportExcel();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
        }

        private void cboGraph_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboGraph.SelectedIndex == 0)
            {
                lblDesc.Visible = true;
                pnlTrend.Visible = true;
                pnlList.Visible = false;
                lblSdtDate.Text = "*기준 : " + System.DateTime.Now.ToShortDateString() + " 6:00";
            }
            else if (cboGraph.SelectedIndex == 1)
            {
                lblDesc.Visible = false;
                pnlTrend.Visible = false;
                pnlList.Visible = true;
                lblSdtDate.Text = "*기준 : " + System.DateTime.Now.AddDays(-30).ToShortDateString() + " ~ " +System.DateTime.Now.ToShortDateString() + " 6:00";
            }
        }

        private void cdvDate_CloseUp(object sender, EventArgs e)
        {
            lblSdtDate.Text = "*기준 : " + cdvDate.Value.ToShortDateString() + " 6:00";
        }

        #endregion


    }
}