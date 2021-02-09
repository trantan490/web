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

namespace Hana.PQC
{
    public partial class PQC031201 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        #region " PQC031201 : Program Initial "

        public PQC031201()
        {
            InitializeComponent();

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;

            cdvDate.Value = System.DateTime.Now;
        }

        #endregion


        #region " Common Function "

        private void fnSSInitial(Miracom.SmartWeb.UI.Controls.udcFarPoint SS)
        {
            /****************************************************
             * Comment : SS의 Header를 설정한다.
             * 
             * Created By : 김선호(2012-07-24-화)
             * 
             * Modified By : 
             ****************************************************/
            //월
            string squery = "";
            DataTable dt = null;
            //주차
            string squery2 = "";
            DataTable dtWeek = null;
            // 목요일 가져오기
            string squery3 = "";
            DataTable dtThu = null;
            //일가져오기
            string squery4 = "";
            DataTable dtDay = null;
            // 분기
            string squery5 = "";
            DataTable dtQuarter = null;

            int iindex = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                SS.RPT_ColumnInit();

                string todate = cdvDate.Value.ToString("yyyyMMddHHmmss");

                squery5 = "SELECT CASE WHEN TO_CHAR(TO_DATE('" + todate + "'), 'Q') - 3 > 0 \n"
                        + "            THEN SUBSTR(TO_CHAR(TO_DATE(TO_DATE('" + todate + "')),'YYYY'),3,2) || 'Y ' || TO_CHAR(TO_CHAR(TO_DATE('" + todate + "'), 'Q') - 3) || 'Q' \n"
                        + "            WHEN TO_CHAR(TO_DATE('" + todate + "'), 'Q') - 3 = 0 \n"
                        + "            THEN SUBSTR(TO_CHAR(TO_DATE(TO_DATE('" + todate + "')),'YYYY'),3,2) - 1 || 'Y ' || '4' || 'Q' \n"
                        + "            WHEN TO_CHAR(TO_DATE('" + todate + "'), 'Q') - 3 = -1 \n"
                        + "            THEN SUBSTR(TO_CHAR(TO_DATE(TO_DATE('" + todate + "')),'YYYY'),3,2) - 1 || 'Y ' || '3' || 'Q' \n"
                        + "            WHEN TO_CHAR(TO_DATE('" + todate + "'), 'Q') - 3 = -2 \n"
                        + "            THEN SUBSTR(TO_CHAR(TO_DATE(TO_DATE('" + todate + "')),'YYYY'),3,2) - 1 || 'Y ' || '2' || 'Q' \n"
                        + "       END AS Q4, \n"
                        + "       CASE WHEN TO_CHAR(TO_DATE('" + todate + "'), 'Q') - 2 > 0 \n"
                        + "            THEN SUBSTR(TO_CHAR(TO_DATE(TO_DATE('" + todate + "')),'YYYY'),3,2) || 'Y ' || TO_CHAR(TO_CHAR(TO_DATE('" + todate + "'), 'Q') - 2)|| 'Q' \n"
                        + "            WHEN TO_CHAR(TO_DATE('" + todate + "'), 'Q') - 2 = 0 \n"
                        + "            THEN SUBSTR(TO_CHAR(TO_DATE(TO_DATE('" + todate + "')),'YYYY'),3,2) - 1 || 'Y ' || '4' || 'Q' \n"
                        + "            WHEN TO_CHAR(TO_DATE('" + todate + "'), 'Q') - 2 = -1 \n"
                        + "            THEN SUBSTR(TO_CHAR(TO_DATE(TO_DATE('" + todate + "')),'YYYY'),3,2) - 1 || 'Y ' || '3' || 'Q' \n"
                        + "       END AS Q3, \n"
                        + "       CASE WHEN TO_CHAR(TO_DATE('" + todate + "'), 'Q') - 1 > 0 \n"
                        + "            THEN SUBSTR(TO_CHAR(TO_DATE(TO_DATE('" + todate + "')),'YYYY'),3,2) || 'Y ' || TO_CHAR(TO_CHAR(TO_DATE('" + todate + "'), 'Q') - 1)|| 'Q' \n"
                        + "            WHEN TO_CHAR(TO_DATE('" + todate + "'), 'Q') - 1 = 0 \n"
                        + "            THEN SUBSTR(TO_CHAR(TO_DATE(TO_DATE('" + todate + "')),'YYYY'),3,2) - 1 || 'Y ' || '4' || 'Q' \n"
                        + "            END AS Q2, \n"
                        + "            SUBSTR(TO_CHAR(TO_DATE(TO_DATE('" + todate + "')),'YYYY'),3,2) || 'Y ' || TO_CHAR(TO_DATE('" + todate + "'), 'Q') || 'Q' AS Q1 \n"
                        + "  FROM DUAL";

                dtQuarter = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", squery5.Replace((char)Keys.Tab, ' '));

                squery = "SELECT SUBSTR(TO_CHAR(TO_DATE('" + todate + "'),'YYYYMM'),5,2)||'월' AS MM12 \n"
                       + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-1),'YYYYMM'),5,2)||'월' AS MM11 \n"
                       + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-2),'YYYYMM'),5,2)||'월' AS MM10 \n"
                       + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-3),'YYYYMM'),5,2)||'월' AS MM9 \n"
                       + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-4),'YYYYMM'),5,2)||'월' AS MM8 \n"
                       + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-5),'YYYYMM'),5,2)||'월' AS MM7 \n"
                       + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-6),'YYYYMM'),5,2)||'월' AS MM6 \n"
                       + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-7),'YYYYMM'),5,2)||'월' AS MM5 \n"
                       + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-8),'YYYYMM'),5,2)||'월' AS MM4 \n"
                       + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-9),'YYYYMM'),5,2)||'월' AS MM3 \n"
                       + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-10),'YYYYMM'),5,2)||'월' AS MM2 \n"
                       + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-11),'YYYYMM'),5,2)||'월' AS MM1 \n"
                       + "  FROM DUAL";

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", squery.Replace((char)Keys.Tab, ' '));

                todate = cdvDate.Value.ToString("yyyyMMdd");

                squery2 = "SELECT DECODE(PLAN_WEEK - 4,-3,50,-2,51,-1,52, 0,53, PLAN_WEEK - 4)||'주' AS WW1 \n"
                       + "      , DECODE(PLAN_WEEK - 3,-2,51,-1,52, 0,53, PLAN_WEEK - 3)||'주' AS WW2 \n"
                       + "      , DECODE(PLAN_WEEK - 2,-1,52, 0,53, PLAN_WEEK - 2)||'주' AS WW3 \n"
                       + "	    , DECODE(PLAN_WEEK - 1, 0,53, PLAN_WEEK - 1)||'주' AS WW4 \n"
                       + "	    , PLAN_WEEK||'주' AS WW5 \n"
                       + "	 FROM MWIPCALDEF \n"
                       + "	WHERE CALENDAR_ID = 'QC' \n"
                       + "	  AND SYS_DATE = TO_CHAR(TO_DATE('" + todate + "','YYYYMMDD'),'YYYYMMDD')";

                dtWeek = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", squery2.Replace((char)Keys.Tab, ' '));


                squery3 = "SELECT CASE TO_CHAR(TO_DATE('" + todate + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + todate + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                       + "        WHEN '토' THEN TO_CHAR(TO_DATE('" + todate + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                       + "        WHEN '일' THEN TO_CHAR(TO_DATE('" + todate + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                       + "        WHEN '월' THEN TO_CHAR(TO_DATE('" + todate + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                       + "        WHEN '화' THEN TO_CHAR(TO_DATE('" + todate + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                       + "        WHEN '수' THEN TO_CHAR(TO_DATE('" + todate + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                       + "        WHEN '목' THEN TO_CHAR(TO_DATE('" + todate + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                       + "        END AS DAY \n"
                       + "	 FROM DUAL";

                dtThu = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", squery3.Replace((char)Keys.Tab, ' '));

                squery4 = "SELECT SUBSTR(TO_CHAR(TO_DATE('" + dtThu.Rows[0][0] + "'),'YYYYMMDD'),7,2)||'일(목)' AS DD \n"
                       + "      , SUBSTR(TO_CHAR(TO_DATE('" + dtThu.Rows[0][0] + "')-1,'YYYYMMDD'),7,2)||'일(수)' AS DD1 \n"
                       + "      , SUBSTR(TO_CHAR(TO_DATE('" + dtThu.Rows[0][0] + "')-2,'YYYYMMDD'),7,2)||'일(화)' AS DD2 \n"
                       + "      , SUBSTR(TO_CHAR(TO_DATE('" + dtThu.Rows[0][0] + "')-3,'YYYYMMDD'),7,2)||'일(월)' AS DD3 \n"
                       + "      , SUBSTR(TO_CHAR(TO_DATE('" + dtThu.Rows[0][0] + "')-4,'YYYYMMDD'),7,2)||'일(일)' AS DD4 \n"
                       + "      , SUBSTR(TO_CHAR(TO_DATE('" + dtThu.Rows[0][0] + "')-5,'YYYYMMDD'),7,2)||'일(토)' AS DD5 \n"
                       + "      , SUBSTR(TO_CHAR(TO_DATE('" + dtThu.Rows[0][0] + "')-6,'YYYYMMDD'),7,2)||'일(금)' AS DD6 \n"
                       + "	 FROM DUAL";
                dtDay = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", squery4.Replace((char)Keys.Tab, ' '));

                SS.RPT_AddBasicColumn("구 분", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                //SS.RPT_AddBasicColumn("CUSTOMER", 0, iindex + 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);

                if (dtQuarter.Rows.Count <= 0)
                {
                    dtQuarter.Dispose();
                    SS.RPT_AddBasicColumn("Q-1", 0, iindex + 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("Q-2", 0, iindex + 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("Q-3", 0, iindex + 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("Q-4", 0, iindex + 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                }
                else
                {
                    SS.RPT_AddBasicColumn(dtQuarter.Rows[0][0].ToString(), 0, iindex + 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dtQuarter.Rows[0][1].ToString(), 0, iindex + 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dtQuarter.Rows[0][2].ToString(), 0, iindex + 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dtQuarter.Rows[0][3].ToString(), 0, iindex + 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                }

                if (dt.Rows.Count <= 0)
                {
                    dt.Dispose();
                    SS.RPT_AddBasicColumn("MM-1", 0, iindex + 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("MM-2", 0, iindex + 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("MM-3", 0, iindex + 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("MM-4", 0, iindex + 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("MM-5", 0, iindex + 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("MM-6", 0, iindex + 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("MM-7", 0, iindex + 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("MM-8", 0, iindex + 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("MM-9", 0, iindex + 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("MM-10", 0, iindex + 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("MM-11", 0, iindex + 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("MM-12", 0, iindex + 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                }
                else
                {
                    SS.RPT_AddBasicColumn(dt.Rows[0][11].ToString(), 0, iindex + 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dt.Rows[0][10].ToString(), 0, iindex + 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dt.Rows[0][9].ToString(), 0, iindex + 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dt.Rows[0][8].ToString(), 0, iindex + 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dt.Rows[0][7].ToString(), 0, iindex + 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dt.Rows[0][6].ToString(), 0, iindex + 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dt.Rows[0][5].ToString(), 0, iindex + 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dt.Rows[0][4].ToString(), 0, iindex + 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dt.Rows[0][3].ToString(), 0, iindex + 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dt.Rows[0][2].ToString(), 0, iindex + 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dt.Rows[0][1].ToString(), 0, iindex + 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dt.Rows[0][0].ToString(), 0, iindex + 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                }

                if (dtWeek.Rows.Count <= 0)
                {
                    dtWeek.Dispose();
                    SS.RPT_AddBasicColumn("WW-5", 0, iindex + 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("WW-4", 0, iindex + 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("WW-3", 0, iindex + 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("WW-2", 0, iindex + 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("WW-1", 0, iindex + 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                }
                else
                {
                    SS.RPT_AddBasicColumn(dtWeek.Rows[0][0].ToString(), 0, iindex + 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dtWeek.Rows[0][1].ToString(), 0, iindex + 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dtWeek.Rows[0][2].ToString(), 0, iindex + 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dtWeek.Rows[0][3].ToString(), 0, iindex + 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dtWeek.Rows[0][4].ToString(), 0, iindex + 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                }

                if (dtDay.Rows.Count <= 0)
                {
                    dtDay.Dispose();
                    SS.RPT_AddBasicColumn("금(-6)", 0, iindex + 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("토(-5)", 0, iindex + 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("일(-4)", 0, iindex + 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("월(-3)", 0, iindex + 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("화(-2)", 0, iindex + 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("수(-1)", 0, iindex + 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn("목(-0)", 0, iindex + 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                }
                else
                {

                    SS.RPT_AddBasicColumn(dtDay.Rows[0][6].ToString(), 0, iindex + 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dtDay.Rows[0][5].ToString(), 0, iindex + 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dtDay.Rows[0][4].ToString(), 0, iindex + 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dtDay.Rows[0][3].ToString(), 0, iindex + 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dtDay.Rows[0][2].ToString(), 0, iindex + 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dtDay.Rows[0][1].ToString(), 0, iindex + 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                    SS.RPT_AddBasicColumn(dtDay.Rows[0][0].ToString(), 0, iindex + 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                }
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

        private bool fnDataFind()
        {
            /****************************************************
             * Comment : DataBase에 저장된 데이터를 조회한다.
             * 
             * Created By : 김선호(2012-07-24-화요일)
             * 
             * Modified By :
             ****************************************************/
            DataTable dt = null;
            string QRY = "";

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                LoadingPopUp.LoadIngPopUpShow(this);

                // Sheet / Listview를 초기화 한다.
                CmnInitFunction.ClearList(spdData, true);

                string date = cdvDate.Value.ToString("yyyyMMddHHmmss");

                QRY = " SELECT GUBUN \n"
                    + "     , SUM(QTY_QQ_1) AS QTY_QQ_1, SUM(QTY_QQ_2) AS QTY_QQ_2, SUM(QTY_QQ_3) AS QTY_QQ_3, SUM(QTY_QQ_4) AS QTY_QQ_4 \n"
                    + "     , SUM(QTY_MM_1) AS QTY_MM_1, SUM(QTY_MM_2) AS QTY_MM_2, SUM(QTY_MM_3) AS QTY_MM_3 \n"
                    + "     , SUM(QTY_MM_4) AS QTY_MM_4, SUM(QTY_MM_5) AS QTY_MM_5, SUM(QTY_MM_6) AS QTY_MM_6 \n"
                    + "     , SUM(QTY_MM_7) AS QTY_MM_7, SUM(QTY_MM_8) AS QTY_MM_8, SUM(QTY_MM_9) AS QTY_MM_9 \n"
                    + "     , SUM(QTY_MM_10) AS QTY_MM_10, SUM(QTY_MM_11) AS QTY_MM_11, SUM(QTY_MM_12) AS QTY_MM_12 \n"
                    + "     , SUM(QTY_WW_1) AS QTY_WW_1, SUM(QTY_WW_2) AS QTY_WW_2, SUM(QTY_WW_3) AS QTY_WW_3, SUM(QTY_WW_4) AS QTY_WW_4, SUM(QTY_WW_5) AS QTY_WW_5 \n"
                    + "     , SUM(QTY_DD_1) AS QTY_DD_1, SUM(QTY_DD_2) AS QTY_DD_2, SUM(QTY_DD_3) AS QTY_DD_3, SUM(QTY_DD_4) AS QTY_DD_4 \n"
                    + "     , SUM(QTY_DD_5) AS QTY_DD_5, SUM(QTY_DD_6) AS QTY_DD_6, SUM(QTY_DD_7) AS QTY_DD_7 \n"
                    + " FROM ( \n"
                    + "     SELECT '검사LOT수' AS GUBUN \n"
                    + "         , SUM(CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'Q') \n"
                    + "                 WHEN '4' THEN  \n"
                    + "                             CASE WHEN A.WORK_MONTH >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'01' \n"
                    + "                                         AND A.WORK_MONTH <= TO_CHAR(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'01', 'YYYYMM'),2), 'YYYYMM') \n"
                    + "                                 THEN A.QC_LOT_QTY_1 \n"
                    + "                                 ELSE 0 \n"
                    + "                             END \n"
                    + "                 WHEN '3' THEN  \n"
                    + "                             CASE WHEN A.WORK_MONTH >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'10' \n"
                    + "                                         AND A.WORK_MONTH <= TO_CHAR(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'10', 'YYYYMM'),2), 'YYYYMM') \n"
                    + "                                 THEN A.QC_LOT_QTY_1 \n"
                    + "                                 ELSE 0 \n"
                    + "                             END \n"
                    + "                 WHEN '2' THEN  \n"
                    + "                             CASE WHEN A.WORK_MONTH >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'07' \n"
                    + "                                         AND A.WORK_MONTH <= TO_CHAR(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'07', 'YYYYMM'),2), 'YYYYMM') \n"
                    + "                                 THEN A.QC_LOT_QTY_1 \n"
                    + "                                 ELSE 0 \n"
                    + "                             END \n"
                    + "                 WHEN '1' THEN  \n"
                    + "                             CASE WHEN A.WORK_MONTH >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'04' \n"
                    + "                                         AND A.WORK_MONTH <= TO_CHAR(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'04', 'YYYYMM'),2), 'YYYYMM') \n"
                    + "                                 THEN A.QC_LOT_QTY_1 \n"
                    + "                                 ELSE 0 \n"
                    + "                             END \n"
                    + "               END) AS QTY_QQ_1 \n"
                    + "         , SUM(CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'Q') \n"
                    + "                 WHEN '4' THEN  \n"
                    + "                             CASE WHEN A.WORK_MONTH >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'04' \n"
                    + "                                         AND A.WORK_MONTH <= TO_CHAR(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'04', 'YYYYMM'),2), 'YYYYMM') \n"
                    + "                                 THEN A.QC_LOT_QTY_1 \n"
                    + "                                 ELSE 0 \n"
                    + "                             END \n"
                    + "                 WHEN '3' THEN  \n"
                    + "                             CASE WHEN A.WORK_MONTH >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'01' \n"
                    + "                                         AND A.WORK_MONTH <= TO_CHAR(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'01', 'YYYYMM'),2), 'YYYYMM') \n"
                    + "                                 THEN A.QC_LOT_QTY_1 \n"
                    + "                                 ELSE 0 \n"
                    + "                             END \n"
                    + "                 WHEN '2' THEN  \n"
                    + "                             CASE WHEN A.WORK_MONTH >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'10' \n"
                    + "                                         AND A.WORK_MONTH <= TO_CHAR(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'10', 'YYYYMM'),2), 'YYYYMM') \n"
                    + "                                 THEN A.QC_LOT_QTY_1 \n"
                    + "                                 ELSE 0 \n"
                    + "                             END \n"
                    + "                 WHEN '1' THEN  \n"
                    + "                             CASE WHEN A.WORK_MONTH >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'07' \n"
                    + "                                         AND A.WORK_MONTH <= TO_CHAR(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'07', 'YYYYMM'),2), 'YYYYMM') \n"
                    + "                                 THEN A.QC_LOT_QTY_1 \n"
                    + "                                 ELSE 0 \n"
                    + "                             END \n"
                    + "               END) AS QTY_QQ_2 \n"
                    + "         , SUM(CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'Q') \n"
                    + "                 WHEN '4' THEN  \n"
                    + "                             CASE WHEN A.WORK_MONTH >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'07' \n"
                    + "                                         AND A.WORK_MONTH <= TO_CHAR(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'07', 'YYYYMM'),2), 'YYYYMM') \n"
                    + "                                 THEN A.QC_LOT_QTY_1 \n"
                    + "                                 ELSE 0 \n"
                    + "                             END \n"
                    + "                 WHEN '3' THEN  \n"
                    + "                             CASE WHEN A.WORK_MONTH >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'04' \n"
                    + "                                         AND A.WORK_MONTH <= TO_CHAR(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'04', 'YYYYMM'),2), 'YYYYMM') \n"
                    + "                                 THEN A.QC_LOT_QTY_1 \n"
                    + "                                 ELSE 0 \n"
                    + "                             END \n"
                    + "                 WHEN '2' THEN  \n"
                    + "                             CASE WHEN A.WORK_MONTH >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'01' \n"
                    + "                                         AND A.WORK_MONTH <= TO_CHAR(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'01', 'YYYYMM'),2), 'YYYYMM') \n"
                    + "                                 THEN A.QC_LOT_QTY_1 \n"
                    + "                                 ELSE 0 \n"
                    + "                             END \n"
                    + "                 WHEN '1' THEN  \n"
                    + "                             CASE WHEN A.WORK_MONTH >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'10' \n"
                    + "                                         AND A.WORK_MONTH <= TO_CHAR(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'10', 'YYYYMM'),2), 'YYYYMM') \n"
                    + "                                 THEN A.QC_LOT_QTY_1 \n"
                    + "                                 ELSE 0 \n"
                    + "                             END \n"
                    + "               END) AS QTY_QQ_3 \n"
                    + "         , SUM(CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'Q') \n"
                    + "                 WHEN '4' THEN  \n"
                    + "                             CASE WHEN A.WORK_MONTH >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'10' \n"
                    + "                                         AND A.WORK_MONTH <= TO_CHAR(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'10', 'YYYYMM'),2), 'YYYYMM') \n"
                    + "                                 THEN A.QC_LOT_QTY_1 \n"
                    + "                                 ELSE 0  \n"
                    + "                             END \n"
                    + "                 WHEN '3' THEN  \n"
                    + "                             CASE WHEN A.WORK_MONTH >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'07' \n"
                    + "                                         AND A.WORK_MONTH <= TO_CHAR(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'07', 'YYYYMM'),2), 'YYYYMM') \n"
                    + "                                 THEN A.QC_LOT_QTY_1 \n"
                    + "                                 ELSE 0  \n"
                    + "                             END \n"
                    + "                 WHEN '2' THEN  \n"
                    + "                             CASE WHEN A.WORK_MONTH >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'04' \n"
                    + "                                         AND A.WORK_MONTH <= TO_CHAR(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'04', 'YYYYMM'),2), 'YYYYMM') \n"
                    + "                                 THEN A.QC_LOT_QTY_1 \n"
                    + "                                 ELSE 0  \n"
                    + "                             END \n"
                    + "                 WHEN '1' THEN  \n"
                    + "                             CASE WHEN A.WORK_MONTH >= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'01' \n"
                    + "                                         AND A.WORK_MONTH <= TO_CHAR(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'01', 'YYYYMM'),2), 'YYYYMM') \n"
                    + "                                 THEN A.QC_LOT_QTY_1 \n"
                    + "                                 ELSE 0  \n"
                    + "                             END \n"
                    + "               END) AS QTY_QQ_4 \n"
                    + "         , 0 AS QTY_MM_1 \n"
                    + "         , 0 AS QTY_MM_2 \n"
                    + "         , 0 AS QTY_MM_3 \n"
                    + "         , 0 AS QTY_MM_4 \n"
                    + "         , 0 AS QTY_MM_5 \n"
                    + "         , 0 AS QTY_MM_6 \n"
                    + "         , 0 AS QTY_MM_7 \n"
                    + "         , 0 AS QTY_MM_8 \n"
                    + "         , 0 AS QTY_MM_9 \n"
                    + "         , 0 AS QTY_MM_10 \n"
                    + "         , 0 AS QTY_MM_11 \n"
                    + "         , 0 AS QTY_MM_12 \n"
                    + "         , 0 AS QTY_WW_1 \n"
                    + "         , 0 AS QTY_WW_2 \n"
                    + "         , 0 AS QTY_WW_3 \n"
                    + "         , 0 AS QTY_WW_4 \n"
                    + "         , 0 AS QTY_WW_5 \n"
                    + "         , 0 AS QTY_DD_1 \n"
                    + "         , 0 AS QTY_DD_2 \n"
                    + "         , 0 AS QTY_DD_3 \n"
                    + "         , 0 AS QTY_DD_4 \n"
                    + "         , 0 AS QTY_DD_5 \n"
                    + "         , 0 AS QTY_DD_6 \n"
                    + "         , 0 AS QTY_DD_7 \n"
                    + "     FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B   \n"
                    + "     WHERE A.FACTORY = B.FACTORY   \n"
                    + "     AND A.MAT_ID = B.MAT_ID   \n"
                    + "     AND A.FACTORY = '" + cdvFactory.Text + "'  \n"
                    + "     AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -11), 'YYYYMM')  \n"
                    + "     AND A.WORK_MONTH <= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMM') \n"
                    + "     AND A.QC_OPER IN ('PVI','QC GATE')     \n"
                    + "     AND A.QC_TYPE LIKE 'GATE' || '%'   \n"
                    + "     AND B.MAT_GRP_1 LIKE '%' \n"
                    + "     UNION ALL \n"
                    + "     SELECT '검사LOT수' AS GUBUN \n"
                    + "         , 0 AS QTY_QQ_1 \n"
                    + "         , 0 AS QTY_QQ_2 \n"
                    + "         , 0 AS QTY_QQ_3 \n"
                    + "         , 0 AS QTY_QQ_4 \n"
                    + "         , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -11), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_MM_1 \n"
                    + "         , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -10), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_MM_2 \n"
                    + "         , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -9), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_MM_3 \n"
                    + "         , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -8), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_MM_4 \n"
                    + "         , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -7), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_MM_5 \n"
                    + "         , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -6), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_MM_6 \n"
                    + "         , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -5), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_MM_7 \n"
                    + "         , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_MM_8 \n"
                    + "         , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_MM_9 \n"
                    + "         , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_MM_10 \n"
                    + "         , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_MM_11 \n"
                    + "         , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -0), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_MM_12 \n"
                    + "         , 0 AS QTY_WW_1 \n"
                    + "         , 0 AS QTY_WW_2 \n"
                    + "         , 0 AS QTY_WW_3 \n"
                    + "         , 0 AS QTY_WW_4 \n"
                    + "         , 0 AS QTY_WW_5 \n"
                    + "         , 0 AS QTY_DD_1 \n"
                    + "         , 0 AS QTY_DD_2 \n"
                    + "         , 0 AS QTY_DD_3 \n"
                    + "         , 0 AS QTY_DD_4 \n"
                    + "         , 0 AS QTY_DD_5 \n"
                    + "         , 0 AS QTY_DD_6 \n"
                    + "         , 0 AS QTY_DD_7 \n"
                    + "     FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B \n"
                    + "     WHERE A.FACTORY = B.FACTORY \n"
                    + "     AND A.MAT_ID = B.MAT_ID \n"
                    + "     AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                    + "     AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -11), 'YYYYMM')  \n"
                    + "     AND A.WORK_MONTH <= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMM') \n"
                    + "     AND A.QC_OPER IN ('PVI','QC GATE') \n"
                    + "     AND A.QC_TYPE LIKE 'GATE' || '%' \n"
                    + "     AND B.MAT_GRP_1 LIKE '%' \n"
                    + "     UNION ALL  \n"
                    + "     SELECT '검사LOT수' AS GUBUN \n"
                    + "         , 0 AS QTY_QQ_1 \n"
                    + "         , 0 AS QTY_QQ_2 \n"
                    + "         , 0 AS QTY_QQ_3 \n"
                    + "         , 0 AS QTY_QQ_4 \n"
                    + "         , 0 AS QTY_MM_1 \n"
                    + "         , 0 AS QTY_MM_2 \n"
                    + "         , 0 AS QTY_MM_3 \n"
                    + "         , 0 AS QTY_MM_4 \n"
                    + "         , 0 AS QTY_MM_5 \n"
                    + "         , 0 AS QTY_MM_6 \n"
                    + "         , 0 AS QTY_MM_7 \n"
                    + "         , 0 AS QTY_MM_8 \n"
                    + "         , 0 AS QTY_MM_9 \n"
                    + "         , 0 AS QTY_MM_10 \n"
                    + "         , 0 AS QTY_MM_11 \n"
                    + "         , 0 AS QTY_MM_12 \n"
                    + "         , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 28, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 29, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 30, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 31, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 32, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 33, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 34, 'YYYYMMDD') \n"
                    + "                                        END  \n"
                    + "                     AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 22, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 23, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 24, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 25, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 26, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 27, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 28, 'YYYYMMDD') \n"
                    + "                                        END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_WW_1  \n"
                    + "         , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 21, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 22, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 23, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 24, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 25, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 26, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 27, 'YYYYMMDD') \n"
                    + "                                        END \n"
                    + "                     AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 15, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 16, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 17, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 18, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 19, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 20, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 21, 'YYYYMMDD') \n"
                    + "                                        END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_WW_2  \n"
                    + "         , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 14, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 15, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 16, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 17, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 18, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 19, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 20, 'YYYYMMDD') \n"
                    + "                                        END \n"
                    + "                     AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 8, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 9, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 10, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 11, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 12, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 13, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 14, 'YYYYMMDD') \n"
                    + "                                        END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_WW_3  \n"
                    + "         , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 7, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 8, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 9, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 10, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 11, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 12, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 13, 'YYYYMMDD') \n"
                    + "                                        END \n"
                    + "                     AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 1, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 2, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 3, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 4, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 5, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 6, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 7, 'YYYYMMDD') \n"
                    + "                                        END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_WW_4 \n"
                    + "         , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 0, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 1, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 2, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 3, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 4, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 5, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 6, 'YYYYMMDD') \n"
                    + "                                        END \n"
                    + "                     AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 6, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 5, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 4, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 3, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 2, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 1, 'YYYYMMDD') \n"
                    + "                                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 0, 'YYYYMMDD') \n"
                    + "                                        END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_WW_5 \n"
                    + "         , 0 AS QTY_DD_1  \n"
                    + "         , 0 AS QTY_DD_2  \n"
                    + "         , 0 AS QTY_DD_3  \n"
                    + "         , 0 AS QTY_DD_4  \n"
                    + "         , 0 AS QTY_DD_5  \n"
                    + "         , 0 AS QTY_DD_6  \n"
                    + "         , 0 AS QTY_DD_7  \n"
                    + "     FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B   \n"
                    + "     WHERE A.FACTORY = B.FACTORY   \n"
                    + "     AND A.MAT_ID = B.MAT_ID   \n"
                    + "     AND A.FACTORY = '" + cdvFactory.Text + "'  \n"
                    + "     AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 28, 'YYYYMMDD')  \n"
                    + "                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 29, 'YYYYMMDD')  \n"
                    + "                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 30, 'YYYYMMDD')  \n"
                    + "                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 31, 'YYYYMMDD')  \n"
                    + "                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 32, 'YYYYMMDD')  \n"
                    + "                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 33, 'YYYYMMDD')  \n"
                    + "                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 34, 'YYYYMMDD')  \n"
                    + "                                            END  \n"
                    + "     AND A.WORK_DATE <= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMMDD')  \n"
                    + "     AND A.QC_OPER IN ('PVI','QC GATE')     \n"
                    + "     AND A.QC_TYPE LIKE 'GATE' || '%'   \n"
                    + "     AND B.MAT_GRP_1 LIKE '%' \n"
                    + "     UNION ALL \n"
                    + "     SELECT '검사LOT수' AS GUBUN  \n"
                    + "         , 0 AS QTY_QQ_1 \n"
                    + "         , 0 AS QTY_QQ_2 \n"
                    + "         , 0 AS QTY_QQ_3 \n"
                    + "         , 0 AS QTY_QQ_4 \n"
                    + "         , 0 AS QTY_MM_1 \n"
                    + "         , 0 AS QTY_MM_2 \n"
                    + "         , 0 AS QTY_MM_3 \n"
                    + "         , 0 AS QTY_MM_4 \n"
                    + "         , 0 AS QTY_MM_5 \n"
                    + "         , 0 AS QTY_MM_6 \n"
                    + "         , 0 AS QTY_MM_7 \n"
                    + "         , 0 AS QTY_MM_8 \n"
                    + "         , 0 AS QTY_MM_9 \n"
                    + "         , 0 AS QTY_MM_10 \n"
                    + "         , 0 AS QTY_MM_11 \n"
                    + "         , 0 AS QTY_MM_12 \n"
                    + "         , 0 AS QTY_WW_1 \n"
                    + "         , 0 AS QTY_WW_2 \n"
                    + "         , 0 AS QTY_WW_3 \n"
                    + "         , 0 AS QTY_WW_4 \n"
                    + "         , 0 AS QTY_WW_5 \n"
                    + "         , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_DD_1  \n"
                    + "         , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_DD_2  \n"
                    + "         , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_DD_3  \n"
                    + "         , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_DD_4  \n"
                    + "         , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_DD_5  \n"
                    + "         , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_DD_6  \n"
                    + "         , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_DD_7  \n"
                    + "     FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B   \n"
                    + "     WHERE A.FACTORY = B.FACTORY   \n"
                    + "     AND A.MAT_ID = B.MAT_ID   \n"
                    + "     AND A.FACTORY = '" + cdvFactory.Text + "'  \n"
                    + "     AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 0, 'YYYYMMDD')  \n"
                    + "                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 1, 'YYYYMMDD')  \n"
                    + "                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 2, 'YYYYMMDD')  \n"
                    + "                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 3, 'YYYYMMDD')  \n"
                    + "                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 4, 'YYYYMMDD')  \n"
                    + "                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 5, 'YYYYMMDD')  \n"
                    + "                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 6, 'YYYYMMDD')  \n"
                    + "                        END   \n"
                    + "     AND A.WORK_DATE <= TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMMDD')  \n"
                    + "     AND A.QC_OPER IN ('PVI','QC GATE')     \n"
                    + "     AND A.QC_TYPE LIKE 'GATE' || '%'   \n"
                    + "     AND B.MAT_GRP_1 LIKE '%' \n"
                    + "     UNION ALL \n"
                    + "     SELECT 'QCN건수' AS GUBUN \n"
                    + "         , SUM(CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'Q') \n"
                    + "                 WHEN '4' THEN  \n"
                    + "                             CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'12', 'YYYYMM')), 'YYYYMMDD')||'220000' \n"
                    + "                                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'12', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000' \n"
                    + "                                 THEN 1 \n"
                    + "                                 ELSE 0  \n"
                    + "                             END \n"
                    + "                 WHEN '3' THEN  \n"
                    + "                             CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'09', 'YYYYMM')), 'YYYYMMDD')||'220000' \n"
                    + "                                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'09', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000' \n"
                    + "                                 THEN 1 \n"
                    + "                                 ELSE 0  \n"
                    + "                             END \n"
                    + "                 WHEN '2' THEN  \n"
                    + "                             CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'06', 'YYYYMM')), 'YYYYMMDD')||'220000' \n"
                    + "                                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'06', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000' \n"
                    + "                                 THEN 1 \n"
                    + "                                 ELSE 0  \n"
                    + "                             END \n"
                    + "                 WHEN '1' THEN  \n"
                    + "                             CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'03', 'YYYYMM')), 'YYYYMMDD')||'220000' \n"
                    + "                                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'03', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000' \n"
                    + "                                 THEN 1 \n"
                    + "                                 ELSE 0  \n"
                    + "                             END \n"
                    + "               END) AS QTY_QQ_1 \n"
                    + "         , SUM(CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'Q') \n"
                    + "                 WHEN '4' THEN  \n"
                    + "                             CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'03', 'YYYYMM')), 'YYYYMMDD')||'220000' \n"
                    + "                                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'03', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000' \n"
                    + "                                 THEN 1 \n"
                    + "                                 ELSE 0  \n"
                    + "                             END \n"
                    + "                 WHEN '3' THEN  \n"
                    + "                             CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'12', 'YYYYMM')), 'YYYYMMDD')||'220000' \n"
                    + "                                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'12', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000' \n"
                    + "                                 THEN 1 \n"
                    + "                                 ELSE 0  \n"
                    + "                             END \n"
                    + "                 WHEN '2' THEN  \n"
                    + "                             CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'09', 'YYYYMM')), 'YYYYMMDD')||'220000' \n"
                    + "                                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'09', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000' \n"
                    + "                                 THEN 1 \n"
                    + "                                 ELSE 0  \n"
                    + "                             END \n"
                    + "                 WHEN '1' THEN  \n"
                    + "                             CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'06', 'YYYYMM')), 'YYYYMMDD')||'220000' \n"
                    + "                                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'06', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000' \n"
                    + "                                 THEN 1 \n"
                    + "                                 ELSE 0  \n"
                    + "                             END \n"
                    + "               END) AS QTY_QQ_2 \n"
                    + "         , SUM(CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'Q') \n"
                    + "                 WHEN '4' THEN  \n"
                    + "                             CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'06', 'YYYYMM')), 'YYYYMMDD')||'220000' \n"
                    + "                                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'06', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000' \n"
                    + "                                 THEN 1 \n"
                    + "                                 ELSE 0  \n"
                    + "                             END \n"
                    + "                 WHEN '3' THEN  \n"
                    + "                             CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'03', 'YYYYMM')), 'YYYYMMDD')||'220000' \n"
                    + "                                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'03', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000' \n"
                    + "                                 THEN 1 \n"
                    + "                                 ELSE 0  \n"
                    + "                             END \n"
                    + "                 WHEN '2' THEN  \n"
                    + "                             CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'12', 'YYYYMM')), 'YYYYMMDD')||'220000' \n"
                    + "                                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'12', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000' \n"
                    + "                                 THEN 1 \n"
                    + "                                 ELSE 0  \n"
                    + "                             END \n"
                    + "                 WHEN '1' THEN  \n"
                    + "                             CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'09', 'YYYYMM')), 'YYYYMMDD')||'220000' \n"
                    + "                                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'09', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000' \n"
                    + "                                 THEN 1 \n"
                    + "                                 ELSE 0  \n"
                    + "                             END \n"
                    + "               END) AS QTY_QQ_3 \n"
                    + "         , SUM(CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'Q') \n"
                    + "                 WHEN '4' THEN  \n"
                    + "                             CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'09', 'YYYYMM')), 'YYYYMMDD')||'220000' \n"
                    + "                                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'09', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000' \n"
                    + "                                 THEN 1 \n"
                    + "                                 ELSE 0  \n"
                    + "                             END \n"
                    + "                 WHEN '3' THEN  \n"
                    + "                             CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'06', 'YYYYMM')), 'YYYYMMDD')||'220000' \n"
                    + "                                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'06', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000' \n"
                    + "                                 THEN 1 \n"
                    + "                                 ELSE 0  \n"
                    + "                             END \n"
                    + "                 WHEN '2' THEN  \n"
                    + "                             CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'03', 'YYYYMM')), 'YYYYMMDD')||'220000' \n"
                    + "                                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')||'03', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000' \n"
                    + "                                 THEN 1 \n"
                    + "                                 ELSE 0  \n"
                    + "                             END \n"
                    + "                 WHEN '1' THEN  \n"
                    + "                             CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'12', 'YYYYMM')), 'YYYYMMDD')||'220000' \n"
                    + "                                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYY')-1||'12', 'YYYYMM'), 3)), 'YYYYMMDD')||'220000' \n"
                    + "                                 THEN 1 \n"
                    + "                                 ELSE 0  \n"
                    + "                             END \n"
                    + "               END) AS QTY_QQ_4 \n"
                    + "         , 0 AS QTY_MM_1 \n"
                    + "         , 0 AS QTY_MM_2 \n"
                    + "         , 0 AS QTY_MM_3 \n"
                    + "         , 0 AS QTY_MM_4 \n"
                    + "         , 0 AS QTY_MM_5 \n"
                    + "         , 0 AS QTY_MM_6 \n"
                    + "         , 0 AS QTY_MM_7 \n"
                    + "         , 0 AS QTY_MM_8 \n"
                    + "         , 0 AS QTY_MM_9 \n"
                    + "         , 0 AS QTY_MM_10 \n"
                    + "         , 0 AS QTY_MM_11 \n"
                    + "         , 0 AS QTY_MM_12 \n"
                    + "         , 0 AS QTY_WW_1 \n"
                    + "         , 0 AS QTY_WW_2 \n"
                    + "         , 0 AS QTY_WW_3 \n"
                    + "         , 0 AS QTY_WW_4 \n"
                    + "         , 0 AS QTY_WW_5 \n"
                    + "         , 0 AS QTY_DD_1 \n"
                    + "         , 0 AS QTY_DD_2 \n"
                    + "         , 0 AS QTY_DD_3 \n"
                    + "         , 0 AS QTY_DD_4 \n"
                    + "         , 0 AS QTY_DD_5 \n"
                    + "         , 0 AS QTY_DD_6 \n"
                    + "         , 0 AS QTY_DD_7 \n"
                    + "     FROM CABRLOTSTS@RPTTOMES STS, MWIPMATDEF@RPTTOMES MAT  \n"
                    + "     WHERE 1=1  \n"
                    + "     AND SUBSTR(STS.ABR_NO,1,3) = 'QCN' \n"
                    + "     AND STS.FACTORY = '" + cdvFactory.Text + "' \n"
                    + "     AND STS.FACTORY = MAT.FACTORY  \n"
                    + "     AND STS.MAT_ID = MAT.MAT_ID  \n"
                    + "     AND STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -13)), 'YYYYMMDD')||'220000' \n"
                    + "     AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMMDD')||'220000' \n"
                    + "     AND SUBSTR(STS.ABR_NO, 0, 3) LIKE '%' \n"
                    + "     AND STS.DEFECT_CODE LIKE '%' \n"
                    + "     AND STS.CUR_STEP LIKE '%' \n"
                    + "     AND STS.OPER_1 >= 'A0000' \n"
                    + "     AND STS.OPER_1 <= 'AZ010' \n"
                    + "     AND STS.DEL_FLAG = ' ' \n"
                    + "     UNION ALL \n"
                    + "     SELECT 'QCN건수' AS GUBUN \n"
                    + "         , 0 AS QTY_QQ_1  \n"
                    + "         , 0 AS QTY_QQ_2  \n"
                    + "         , 0 AS QTY_QQ_3  \n"
                    + "         , 0 AS QTY_QQ_4 \n"
                    + "         ,SUM(CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -12)), 'YYYYMMDD')||'220000' \n"
                    + "                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -11)), 'YYYYMMDD')||'220000' \n"
                    + "                     THEN 1 \n"
                    + "                     ELSE 0 \n"
                    + "              END) AS QTY_MM_1 \n"
                    + "         ,SUM(CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -11)), 'YYYYMMDD')||'220000' \n"
                    + "                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -10)), 'YYYYMMDD')||'220000' \n"
                    + "                     THEN 1 \n"
                    + "                     ELSE 0 \n"
                    + "              END) AS QTY_MM_2 \n"
                    + "         ,SUM(CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -10)), 'YYYYMMDD')||'220000' \n"
                    + "                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -9)), 'YYYYMMDD')||'220000' \n"
                    + "                     THEN 1 \n"
                    + "                     ELSE 0 \n"
                    + "              END) AS QTY_MM_3 \n"
                    + "         ,SUM(CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -9)), 'YYYYMMDD')||'220000' \n"
                    + "                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -8)), 'YYYYMMDD')||'220000' \n"
                    + "                     THEN 1 \n"
                    + "                     ELSE 0 \n"
                    + "              END) AS QTY_MM_4 \n"
                    + "         ,SUM(CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -8)), 'YYYYMMDD')||'220000' \n"
                    + "                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -7)), 'YYYYMMDD')||'220000' \n"
                    + "                     THEN 1 \n"
                    + "                     ELSE 0 \n"
                    + "              END) AS QTY_MM_5 \n"
                    + "         ,SUM(CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -7)), 'YYYYMMDD')||'220000' \n"
                    + "                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -6)), 'YYYYMMDD')||'220000' \n"
                    + "                     THEN 1 \n"
                    + "                     ELSE 0 \n"
                    + "              END) AS QTY_MM_6 \n"
                    + "         ,SUM(CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -6)), 'YYYYMMDD')||'220000' \n"
                    + "                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -5)), 'YYYYMMDD')||'220000' \n"
                    + "                     THEN 1 \n"
                    + "                     ELSE 0 \n"
                    + "              END) AS QTY_MM_7 \n"
                    + "         ,SUM(CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -5)), 'YYYYMMDD')||'220000' \n"
                    + "                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'YYYYMMDD')||'220000' \n"
                    + "                     THEN 1 \n"
                    + "                     ELSE 0 \n"
                    + "              END) AS QTY_MM_8 \n"
                    + "         ,SUM(CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -4)), 'YYYYMMDD')||'220000' \n"
                    + "                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'YYYYMMDD')||'220000' \n"
                    + "                     THEN 1 \n"
                    + "                     ELSE 0 \n"
                    + "              END) AS QTY_MM_9 \n"
                    + "         ,SUM(CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -3)), 'YYYYMMDD')||'220000' \n"
                    + "                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'YYYYMMDD')||'220000' \n"
                    + "                     THEN 1 \n"
                    + "                     ELSE 0 \n"
                    + "              END) AS QTY_MM_10 \n"
                    + "         ,SUM(CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -2)), 'YYYYMMDD')||'220000' \n"
                    + "                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'YYYYMMDD')||'220000' \n"
                    + "                     THEN 1 \n"
                    + "                     ELSE 0 \n"
                    + "              END) AS QTY_MM_11 \n"
                    + "         ,SUM(CASE WHEN STS.STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -1)), 'YYYYMMDD')||'220000' \n"
                    + "                         AND STS.STEP10_TIME < TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 0)), 'YYYYMMDD')||'220000' \n"
                    + "                     THEN 1 \n"
                    + "                     ELSE 0 \n"
                    + "              END) AS QTY_MM_12 \n"
                    + "         , 0 AS QTY_WW_1 \n"
                    + "         , 0 AS QTY_WW_2 \n"
                    + "         , 0 AS QTY_WW_3 \n"
                    + "         , 0 AS QTY_WW_4 \n"
                    + "         , 0 AS QTY_WW_5 \n"
                    + "         , 0 AS QTY_DD_1 \n"
                    + "         , 0 AS QTY_DD_2 \n"
                    + "         , 0 AS QTY_DD_3 \n"
                    + "         , 0 AS QTY_DD_4 \n"
                    + "         , 0 AS QTY_DD_5 \n"
                    + "         , 0 AS QTY_DD_6 \n"
                    + "         , 0 AS QTY_DD_7 \n"
                    + "     FROM CABRLOTSTS@RPTTOMES STS, MWIPMATDEF@RPTTOMES MAT  \n"
                    + "     WHERE 1=1  \n"
                    + "     AND SUBSTR(STS.ABR_NO,1,3) = 'QCN' \n"
                    + "     AND STS.FACTORY = '" + cdvFactory.Text + "' \n"
                    + "     AND STS.FACTORY = MAT.FACTORY  \n"
                    + "     AND STS.MAT_ID = MAT.MAT_ID  \n"
                    + "     AND STEP10_TIME >= TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), -12)), 'YYYYMMDD')||'220000' \n"
                    + "     AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMMDD')||'220000' \n"
                    + "     AND SUBSTR(STS.ABR_NO, 0, 3) LIKE '%' \n"
                    + "     AND STS.DEFECT_CODE LIKE '%' \n"
                    + "     AND STS.CUR_STEP LIKE '%' \n"
                    + "     AND STS.OPER_1 >= 'A0000' \n"
                    + "     AND STS.OPER_1 <= 'AZ010' \n"
                    + "     AND STS.DEL_FLAG = ' ' \n"
                    + "     UNION ALL \n"
                    + "     SELECT 'QCN건수' AS GUBUN \n"
                    + "         , 0 AS QTY_QQ_1 \n"
                    + "         , 0 AS QTY_QQ_2 \n"
                    + "         , 0 AS QTY_QQ_3 \n"
                    + "         , 0 AS QTY_QQ_4 \n"
                    + "         , 0 AS QTY_MM_1 \n"
                    + "         , 0 AS QTY_MM_2 \n"
                    + "         , 0 AS QTY_MM_3 \n"
                    + "         , 0 AS QTY_MM_4 \n"
                    + "         , 0 AS QTY_MM_5 \n"
                    + "         , 0 AS QTY_MM_6 \n"
                    + "         , 0 AS QTY_MM_7 \n"
                    + "         , 0 AS QTY_MM_8 \n"
                    + "         , 0 AS QTY_MM_9 \n"
                    + "         , 0 AS QTY_MM_10 \n"
                    + "         , 0 AS QTY_MM_11 \n"
                    + "         , 0 AS QTY_MM_12 \n"
                    + "         , SUM(CASE WHEN STEP10_TIME >= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 29, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 30, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 31, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 32, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 33, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 34, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 35, 'YYYYMMDD')||'220000' \n"
                    + "                                        END  \n"
                    + "                     AND STEP10_TIME <= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 22, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 23, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 24, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 25, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 26, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 27, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 28, 'YYYYMMDD')||'220000' \n"
                    + "                                        END THEN 1 ELSE 0 END) AS QTY_WW_1  \n"
                    + "         , SUM(CASE WHEN STEP10_TIME >= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 22, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 23, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 24, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 25, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 26, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 27, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 28, 'YYYYMMDD')||'220000' \n"
                    + "                                        END \n"
                    + "                     AND STEP10_TIME <= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 15, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 16, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 17, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 18, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 19, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 20, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 21, 'YYYYMMDD')||'220000' \n"
                    + "                                        END THEN 1 ELSE 0 END) AS QTY_WW_2  \n"
                    + "         , SUM(CASE WHEN STEP10_TIME >= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 15, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 16, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 17, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 18, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 19, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 20, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 21, 'YYYYMMDD')||'220000' \n"
                    + "                                        END \n"
                    + "                     AND STEP10_TIME <= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 8, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 9, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 10, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 11, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 12, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 13, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 14, 'YYYYMMDD')||'220000' \n"
                    + "                                        END THEN 1 ELSE 0 END) AS QTY_WW_3 \n"
                    + "         , SUM(CASE WHEN STEP10_TIME >= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 8, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 9, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 10, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 11, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 12, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 13, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 14, 'YYYYMMDD')||'220000' \n"
                    + "                                        END \n"
                    + "                     AND STEP10_TIME <= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 1, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 2, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 3, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 4, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 5, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 6, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 7, 'YYYYMMDD')||'220000' \n"
                    + "                                        END THEN 1 ELSE 0 END) AS QTY_WW_4  \n"
                    + "         , SUM(CASE WHEN STEP10_TIME >= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 1, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 2, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 3, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 4, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 5, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 6, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 7, 'YYYYMMDD')||'220000' \n"
                    + "                                        END \n"
                    + "                     AND STEP10_TIME <= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 6, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 5, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 4, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 3, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 2, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 1, 'YYYYMMDD')||'220000' \n"
                    + "                                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') + 0, 'YYYYMMDD')||'220000' \n"
                    + "                                        END THEN 1 ELSE 0 END) AS QTY_WW_5 \n"
                    + "         , 0 AS QTY_DD_1  \n"
                    + "         , 0 AS QTY_DD_2  \n"
                    + "         , 0 AS QTY_DD_3  \n"
                    + "         , 0 AS QTY_DD_4  \n"
                    + "         , 0 AS QTY_DD_5  \n"
                    + "         , 0 AS QTY_DD_6  \n"
                    + "         , 0 AS QTY_DD_7  \n"
                    + "     FROM CABRLOTSTS@RPTTOMES STS, MWIPMATDEF@RPTTOMES MAT  \n"
                    + "     WHERE 1=1  \n"
                    + "     AND SUBSTR(STS.ABR_NO,1,3) = 'QCN' \n"
                    + "     AND STS.FACTORY = '" + cdvFactory.Text + "' \n"
                    + "     AND STS.FACTORY = MAT.FACTORY  \n"
                    + "     AND STS.MAT_ID = MAT.MAT_ID \n"
                    + "     AND STEP10_TIME >= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 29, 'YYYYMMDD')||'220000' \n"
                    + "                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 30, 'YYYYMMDD')||'220000' \n"
                    + "                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 31, 'YYYYMMDD')||'220000' \n"
                    + "                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 32, 'YYYYMMDD')||'220000' \n"
                    + "                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 33, 'YYYYMMDD')||'220000' \n"
                    + "                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 34, 'YYYYMMDD')||'220000' \n"
                    + "                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 35, 'YYYYMMDD')||'220000' \n"
                    + "                                            END  \n"
                    + "     AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMMDD')||'220000' \n"
                    + "     AND SUBSTR(STS.ABR_NO, 0, 3) LIKE '%' \n"
                    + "     AND STS.DEFECT_CODE LIKE '%' \n"
                    + "     AND STS.CUR_STEP LIKE '%' \n"
                    + "     AND STS.OPER_1 >= 'A0000' \n"
                    + "     AND STS.OPER_1 <= 'AZ010' \n"
                    + "     AND STS.DEL_FLAG = ' ' \n"
                    + "     UNION ALL \n"
                    + "     SELECT 'QCN건수' AS GUBUN \n"
                    + "         , 0 AS QTY_QQ_1 \n"
                    + "         , 0 AS QTY_QQ_2 \n"
                    + "         , 0 AS QTY_QQ_3 \n"
                    + "         , 0 AS QTY_QQ_4 \n"
                    + "         , 0 AS QTY_MM_1 \n"
                    + "         , 0 AS QTY_MM_2 \n"
                    + "         , 0 AS QTY_MM_3 \n"
                    + "         , 0 AS QTY_MM_4 \n"
                    + "         , 0 AS QTY_MM_5 \n"
                    + "         , 0 AS QTY_MM_6 \n"
                    + "         , 0 AS QTY_MM_7 \n"
                    + "         , 0 AS QTY_MM_8 \n"
                    + "         , 0 AS QTY_MM_9 \n"
                    + "         , 0 AS QTY_MM_10 \n"
                    + "         , 0 AS QTY_MM_11 \n"
                    + "         , 0 AS QTY_MM_12 \n"
                    + "         , 0 AS QTY_WW_1 \n"
                    + "         , 0 AS QTY_WW_2 \n"
                    + "         , 0 AS QTY_WW_3 \n"
                    + "         , 0 AS QTY_WW_4 \n"
                    + "         , 0 AS QTY_WW_5 \n"
                    + "	        , SUM(CASE TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'DY')	\n"
                    + "	                    WHEN '금' 	\n"
                    + "	                    THEN CASE WHEN STS.STEP10_TIME < TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'YYYYMMDD')||'220000'	\n"
                    + "	                              THEN 1	\n"
                    + "	                              ELSE 0	\n"
                    + "	                         END	\n"
                    + "	                    WHEN '목'	\n"
                    + "	                    THEN CASE WHEN STS.STEP10_TIME >= TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'YYYYMMDD')||'220000'	\n"
                    + "	                              THEN 1	\n"
                    + "	                              ELSE 0	\n"
                    + "	                         END	\n"
                    + "	              END) AS QTY_DD_1	\n"
                    + "	        , SUM(CASE TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'DY')	\n"
                    + "	                    WHEN '토'	\n"
                    + "	                    THEN CASE WHEN STS.STEP10_TIME < TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'YYYYMMDD')||'220000'	\n"
                    + "	                              THEN 1	\n"
                    + "	                              ELSE 0	\n"
                    + "	                         END	\n"
                    + "	                    WHEN '금'	\n"
                    + "	                    THEN CASE WHEN STS.STEP10_TIME >= TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'YYYYMMDD')||'220000'	\n"
                    + "	                              THEN 1	\n"
                    + "	                              ELSE 0	\n"
                    + "	                         END	\n"
                    + "	              END) AS QTY_DD_2 	\n"
                    + "	        , SUM(CASE TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'DY')	\n"
                    + "	                    WHEN '일' 	\n"
                    + "	                    THEN CASE WHEN STS.STEP10_TIME < TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'YYYYMMDD')||'220000'	\n"
                    + "	                              THEN 1	\n"
                    + "	                              ELSE 0	\n"
                    + "	                         END	\n"
                    + "	                    WHEN '토'	\n"
                    + "	                    THEN CASE WHEN STS.STEP10_TIME >= TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'YYYYMMDD')||'220000'	\n"
                    + "	                              THEN 1	\n"
                    + "	                              ELSE 0	\n"
                    + "	                         END	\n"
                    + "	              END) AS QTY_DD_3 	\n"
                    + "	        , SUM(CASE TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'DY')	\n"
                    + "	                    WHEN '월' 	\n"
                    + "	                    THEN CASE WHEN STS.STEP10_TIME < TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'YYYYMMDD')||'220000'	\n"
                    + "	                              THEN 1	\n"
                    + "	                              ELSE 0	\n"
                    + "	                         END	\n"
                    + "	                    WHEN '일'	\n"
                    + "	                    THEN CASE WHEN STS.STEP10_TIME >= TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'YYYYMMDD')||'220000'	\n"
                    + "	                              THEN 1	\n"
                    + "	                              ELSE 0	\n"
                    + "	                         END	\n"
                    + "	              END) AS QTY_DD_4 	\n"
                    + "	        , SUM(CASE TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'DY')	\n"
                    + "	                    WHEN '화'	\n"
                    + "	                    THEN CASE WHEN STS.STEP10_TIME < TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'YYYYMMDD')||'220000'	\n"
                    + "	                              THEN 1	\n"
                    + "	                              ELSE 0	\n"
                    + "	                         END	\n"
                    + "	                    WHEN '월'	\n"
                    + "	                    THEN CASE WHEN STS.STEP10_TIME >= TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'YYYYMMDD')||'220000'	\n"
                    + "	                              THEN 1	\n"
                    + "	                              ELSE 0	\n"
                    + "	                         END	\n"
                    + "	              END) AS QTY_DD_5 	\n"
                    + "	        , SUM(CASE TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'DY')	\n"
                    + "	                    WHEN '수'	\n"
                    + "	                    THEN CASE WHEN STS.STEP10_TIME < TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'YYYYMMDD')||'220000'	\n"
                    + "	                              THEN 1	\n"
                    + "	                              ELSE 0	\n"
                    + "	                         END	\n"
                    + "	                    WHEN '화'	\n"
                    + "	                    THEN CASE WHEN STS.STEP10_TIME >= TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'YYYYMMDD')||'220000'	\n"
                    + "	                              THEN 1	\n"
                    + "	                              ELSE 0	\n"
                    + "	                         END	\n"
                    + "	              END) AS QTY_DD_6 	\n"
                    + "	        , SUM(CASE TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'DY')	\n"
                    + "	                    WHEN '목' 	\n"
                    + "	                    THEN CASE WHEN STS.STEP10_TIME < TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'YYYYMMDD')||'220000'	\n"
                    + "	                              THEN 1	\n"
                    + "	                              ELSE 0	\n"
                    + "	                         END	\n"
                    + "	                    WHEN '수'	\n"
                    + "	                    THEN CASE WHEN STS.STEP10_TIME >= TO_CHAR(TO_DATE(STS.STEP10_TIME, 'YYYYMMDD HH24:MI:SS'), 'YYYYMMDD')||'220000'	\n"
                    + "	                              THEN 1	\n"
                    + "	                              ELSE 0	\n"
                    + "	                         END	\n"
                    + "	              END) AS QTY_DD_7	\n"
                    + "     FROM CABRLOTSTS@RPTTOMES STS, MWIPMATDEF@RPTTOMES MAT  \n"
                    + "     WHERE 1=1  \n"
                    + "     AND SUBSTR(STS.ABR_NO,1,3) = 'QCN' \n"
                    + "     AND STS.FACTORY = '" + cdvFactory.Text + "' \n"
                    + "     AND STS.FACTORY = MAT.FACTORY  \n"
                    + "     AND STS.MAT_ID = MAT.MAT_ID \n"
                    + "     AND STEP10_TIME >= CASE TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 1, 'YYYYMMDD')||'220000' \n"
                    + "                                                    WHEN '토' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 2, 'YYYYMMDD')||'220000' \n"
                    + "                                                    WHEN '일' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 3, 'YYYYMMDD')||'220000' \n"
                    + "                                                    WHEN '월' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 4, 'YYYYMMDD')||'220000' \n"
                    + "                                                    WHEN '화' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 5, 'YYYYMMDD')||'220000' \n"
                    + "                                                    WHEN '수' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 6, 'YYYYMMDD')||'220000' \n"
                    + "                                                    WHEN '목' THEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS') - 7, 'YYYYMMDD')||'220000' \n"
                    + "                                            END  \n"
                    + "     AND STEP10_TIME < TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD HH24MISS'), 'YYYYMMDD')||'220000' \n"
                    + "     AND SUBSTR(STS.ABR_NO, 0, 3) LIKE '%' \n"
                    + "     AND STS.DEFECT_CODE LIKE '%' \n"
                    + "     AND STS.CUR_STEP LIKE '%' \n"
                    + "     AND STS.OPER_1 >= 'A0000' \n"
                    + "     AND STS.OPER_1 <= 'AZ010' \n"
                    + "     AND STS.DEL_FLAG = ' ' \n"
                    + " )GROUP BY GUBUN \n";

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

                DataRow addDr = dt.NewRow();

                addDr[0] = "LRR";

                for (int col = 1; col < dt.Columns.Count; col++)
                {
                    if (string.IsNullOrEmpty(dt.Rows[0][col].ToString()) == false && dt.Rows[0][col].ToString() != "0")
                    {
                        addDr[col] = System.Math.Round(CmnFunction.ToDbl(dt.Rows[1][col]) / CmnFunction.ToDbl(dt.Rows[0][col]) * 100, 2);
                    }
                    else
                    {
                        addDr[col] = 0;
                    }
                }

                dt.Rows.Add(addDr);

                spdData.DataSource = dt;
                spdData.RPT_AutoFit(false);

                fnMakeChart(spdData, 1);

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
                Cursor.Current = Cursors.Default;
            }
        }

        private void fnMakeChart(Miracom.SmartWeb.UI.Controls.udcFarPoint SS, int iselrow)
        {
            /****************************************************
             * Comment : SS의 데이터를 Chart에 표시한다.
             * 
             * Created By : 김선호(2012-07-24-화요일)
             * 
             * Modified By :
             ****************************************************/
            int[] ich_mm = new int[28]; int[] icols_mm = new int[28]; int[] irows_mm = new int[2]; string[] stitle_mm = new string[2];
            int[] ich_ww = new int[4]; int[] icols_ww = new int[4]; int[] irows_ww = new int[2]; string[] stitle_ww = new string[2];
            int[] ich_dd = new int[7]; int[] icols_dd = new int[7]; int[] irows_dd = new int[2]; string[] stitle_dd = new string[2];
            int icol = 0, irow = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // SS에 데이터가 없으면 종료한다.
                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                cf01.RPT_1_ChartInit();
                cf01.RPT_2_ClearData();

                cf01.Scrollable = true;
                //cf01.AxisX.Title.Text = "[QCN Incidence Trend]";
                //cf01.AxisX.Staggered = true;

                cf01.AxisX.LabelValue = 1;

                for (icol = 0; icol < ich_mm.Length; icol++)
                {
                    ich_mm[icol] = icol + 1;
                    icols_mm[icol] = icol + 1;
                }

                // 2012-07-26-김선호 : 선택한 Row에서 아래로 2개의 Row만 Display한다.
                for (irow = 0; irow < 2; irow++)
                {
                    if ((iselrow + irow) >= SS.Sheets[0].RowCount)
                    {
                        break;
                    }
                    irows_mm[irow] = iselrow + irow;
                    stitle_mm[irow] = SS.Sheets[0].Cells[iselrow + irow, 0].Text;
                }

                String legendQCN = "QCN 건수 [단위 : 건]";
                String legendLRR = "LRR [단위 : %]";
                cf01.RPT_3_OpenData(2, icols_mm.Length);
                double max1 = cf01.RPT_4_AddData(SS, new int[] { 1 }, icols_mm, SeriseType.Rows);
                double max2 = cf01.RPT_4_AddData(SS, new int[] { 2 }, icols_mm, SeriseType.Rows);
                cf01.RPT_5_CloseData();
                cf01.RPT_6_SetGallery(ChartType.Bar, 0, 1, legendQCN, AsixType.Y2, DataTypes.Initeger, max1 * 1.2);
                cf01.RPT_6_SetGallery(ChartType.Line, 1, 1, legendLRR, AsixType.Y, DataTypes.PercentDouble, max2 * 1.2);
                cf01.Series[0].PointLabels = true;
                cf01.Series[1].PointLabels = true;
                cf01.Series[0].PointLabelAlign = SoftwareFX.ChartFX.LabelAlign.Bottom | SoftwareFX.ChartFX.LabelAlign.Center;
                cf01.Series[1].PointLabelAlign = SoftwareFX.ChartFX.LabelAlign.Bottom | SoftwareFX.ChartFX.LabelAlign.Center;
                cf01.Series[0].PointLabelColor = Color.Blue;
                cf01.Series[1].PointLabelColor = Color.Red;
                cf01.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, icols_mm);
                cf01.RPT_8_SetSeriseLegend(new string[] { "QCN 건수", "LRR" }, SoftwareFX.ChartFX.Docked.Top);

                cf01.AxisY.Gridlines = true;
                cf01.AxisY2.Gridlines = true;
                cf01.AxisY2.LabelsFormat.Format = SoftwareFX.ChartFX.AxisFormat.Number;
                cf01.AxisY.DataFormat.Decimals = 2;
                cf01.AxisY2.DataFormat.Decimals = 0;
                cf01.AxisY.Step = 0.50;
                //cf01.AxisY.Max = 3.00;
                cf01.AxisY.Min = 0.00;
                cf01.AxisY2.LogBase = 10;
                //cf01.AxisY2.Max = 10000;
                cf01.AxisY2.Min = 1;

                SoftwareFX.ChartFX.AxisSection section1 = cf01.AxisX.Sections[0];                
                section1.From = 1;
                section1.To = 4;
                section1.Gridlines = true;
                section1.TextColor = System.Drawing.Color.FromArgb(138, 43, 226);
                section1.BackColor = System.Drawing.Color.FromArgb(32, 138, 43, 226);
                //section1.Grid.Color = section1.TextColor;

                SoftwareFX.ChartFX.AxisSection section2 = cf01.AxisX.Sections[1];
                section2.From = 5;
                section2.To = 16;
                section2.Gridlines = true;
                section2.TextColor = System.Drawing.Color.Red;
                section2.BackColor = System.Drawing.Color.FromArgb(32, 255, 0, 0);
                //section2.Grid.Color = section2.TextColor;

                SoftwareFX.ChartFX.AxisSection section3 = cf01.AxisX.Sections[2];
                section3.From = 17;
                section3.To = 21;
                section3.Gridlines = true;
                section3.TextColor = System.Drawing.Color.OliveDrab;
                section3.BackColor = System.Drawing.Color.LightYellow;
                //section3.Grid.Color = section3.TextColor;

                SoftwareFX.ChartFX.AxisSection section4 = cf01.AxisX.Sections[3];
                section4.From = 22;
                section4.To = 28;
                section4.Gridlines = true;
                section4.TextColor = System.Drawing.Color.DarkKhaki;
                section4.BackColor = System.Drawing.Color.WhiteSmoke;
                //section4.Grid.Color = section4.TextColor;

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

        #endregion


        #region " Form Event "

        private void btnView_Click(object sender, EventArgs e)
        {
            /****************************************************
             * Comment : View Button을 클릭하면
             * 
             * Created By : 김선호(2012-07-24-화요일)
             * 
             * Modified By : 
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                fnSSInitial(spdData);

                fnDataFind();
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

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            /****************************************************
             * Comment : Excel Export Button을 클릭하면
             * 
             * Created By : 김선호(2012-07-24-화요일)
             * 
             * Modified By :
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                spdData.ExportExcel();
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

        #endregion

    }
}
