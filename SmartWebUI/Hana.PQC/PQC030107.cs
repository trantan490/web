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

using System.Data.OleDb;
//using Oracle.DataAccess.Client;
//using Oracle.DataAccess.Types;

/****************************************************
 * Comment : 공정검사 품질불량현황
 *
 * Created By : bee-jae jung (2010-05-31-월요일)
 *
 * Modified By : bee-jae jung (2010-06-29-화요일)
 * 2011-03-30-김민우: 전 고객사 볼 수 있도록 체크박스 추가
 ****************************************************/

namespace Hana.PQC
{
    public partial class PQC030107 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        #region " PQC030107 : Program Initial "

        public PQC030107()
        {
            InitializeComponent();
            //fnSSInitial(SS01);
            //fnSSInitial(SS02);
            //fnSSSortInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            cdvBaseDate.Value = DateTime.Today;
        }

        #endregion


        #region " Common Function "

        private void fnSSInitial(Miracom.SmartWeb.UI.Controls.udcFarPoint SS)
        {
            /****************************************************
             * Comment : SS의 Header를 설정한다.
             * 
             * Created By : bee-jae jung(2010-05-31-월요일)
             * 
             * Modified By : bee-jae jung(2010-05-31-월요일)
             ****************************************************/
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

            int iindex = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                SS.RPT_ColumnInit();
                // CUSTOMER
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                {
                    iindex = 0;
                    SS.RPT_AddBasicColumn("CUSTOMER", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // FAMILY
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("FAMILY", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // PACKAGE
                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("PACKAGE", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // TYPE1
                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("TYPE1", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // TYPE2
                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("TYPE2", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // LEAD COUNT
                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("LEAD_COUNT", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // DENSITY
                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("DENSITY", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // GENERATION
                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("GENERATION", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }

                if (iindex > 0)
                {
                    iindex++;
                }
                if (SS.Name.ToUpper() == "SS01")
                {
                    if (rbTrand01.Checked == true)
                    {
                        string todate = cdvBaseDate.Value.ToString("yyyyMMdd");

                        squery = "SELECT SUBSTR(TO_CHAR(TO_DATE('" + todate + "'),'YYYYMM'),5,2)||'월' AS MM3 \n"
                               + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-1),'YYYYMM'),5,2)||'월' AS MM2 \n"
                               + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-2),'YYYYMM'),5,2)||'월' AS MM1 \n"
                               + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-3),'YYYYMM'),5,2)||'월' AS MM \n"
                               + "  FROM DUAL";

                        dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", squery.Replace((char)Keys.Tab, ' '));

                        //2011-03-03 김민우: Spread 타이틀 동적으로 수정
                        squery2 = "SELECT DECODE(PLAN_WEEK - 3,-2,51,-1,52, 0,53, PLAN_WEEK - 3)||'주' AS WW1 \n"
                               + "      , DECODE(PLAN_WEEK - 2,-1,52, 0,53, PLAN_WEEK - 2)||'주' AS WW2 \n"
                               + "	    , DECODE(PLAN_WEEK - 1, 0,53, PLAN_WEEK - 1)||'주' AS WW3 \n"
                               + "	    , PLAN_WEEK||'주' AS WW4 \n"
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

                        // 2010-06-03-정비재 : 고객사별 불량발생 현황

                        SS.RPT_AddBasicColumn("구 분", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                        SS.RPT_AddBasicColumn("CUSTOMER", 0, iindex + 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                        
                        
                        if (dt.Rows.Count <= 0)
                        {
                            dt.Dispose();
                            SS.RPT_AddBasicColumn("MM-3", 0, iindex + 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn("MM-2", 0, iindex + 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn("MM-1", 0, iindex + 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn("MM", 0, iindex + 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                        }
                        else
                        {
                            SS.RPT_AddBasicColumn(dt.Rows[0][3].ToString(), 0, iindex + 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn(dt.Rows[0][2].ToString(), 0, iindex + 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn(dt.Rows[0][1].ToString(), 0, iindex + 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn(dt.Rows[0][0].ToString(), 0, iindex + 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                        }

                        if (dtWeek.Rows.Count <= 0)
                        {
                            dtWeek.Dispose();
                            SS.RPT_AddBasicColumn("WW-3", 0, iindex + 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn("WW-2", 0, iindex + 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn("WW-1", 0, iindex + 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn("WW", 0, iindex + 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                        }
                        else
                        {
                            SS.RPT_AddBasicColumn(dtWeek.Rows[0][0].ToString(), 0, iindex + 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn(dtWeek.Rows[0][1].ToString(), 0, iindex + 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn(dtWeek.Rows[0][2].ToString(), 0, iindex + 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn(dtWeek.Rows[0][3].ToString(), 0, iindex + 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                        }

                        if (dtDay.Rows.Count <= 0)
                        {
                            dtDay.Dispose();
                            SS.RPT_AddBasicColumn("금(-6)", 0, iindex + 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn("토(-5)", 0, iindex + 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn("일(-4)", 0, iindex + 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn("월(-3)", 0, iindex + 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn("화(-2)", 0, iindex + 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn("수(-1)", 0, iindex + 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn("목(-0)", 0, iindex + 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                        }
                        else
                        {

                            SS.RPT_AddBasicColumn(dtDay.Rows[0][6].ToString(), 0, iindex + 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn(dtDay.Rows[0][5].ToString(), 0, iindex + 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn(dtDay.Rows[0][4].ToString(), 0, iindex + 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn(dtDay.Rows[0][3].ToString(), 0, iindex + 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn(dtDay.Rows[0][2].ToString(), 0, iindex + 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn(dtDay.Rows[0][1].ToString(), 0, iindex + 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                            SS.RPT_AddBasicColumn(dtDay.Rows[0][0].ToString(), 0, iindex + 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                        }
                    }
                    else if (rbTrand02.Checked == true)
                    {
                        // 2010-06-03-정비재 : 주간별 불량발생 현황
                        SS.RPT_AddBasicColumn("Defect code (month)", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 120);
                        SS.RPT_AddBasicColumn("Defect quantity (month)", 0, iindex + 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                        SS.RPT_AddBasicColumn("Sample quantity (month)", 0, iindex + 2, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                        SS.RPT_AddBasicColumn("Defect code (week)", 0, iindex + 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
                        SS.RPT_AddBasicColumn("Defect quantity (week)", 0, iindex + 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                        SS.RPT_AddBasicColumn("Sample quantity", 0, iindex + 5, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                        SS.RPT_AddBasicColumn("Defect code (days)", 0, iindex + 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
                        SS.RPT_AddBasicColumn("Defect quantity (days)", 0, iindex + 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                        SS.RPT_AddBasicColumn("Sample quantity (day)", 0, iindex + 8, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                    }
                }
                else if (SS.Name.ToUpper() == "SS02")
                {
                    // 2010-06-03-정비재 : 일자별 불량발생 현황
                    SS.RPT_AddBasicColumn("Defect Code", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                    SS.RPT_AddBasicColumn("Last month", 0, iindex + 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("01", 0, iindex + 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("02", 0, iindex + 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("03", 0, iindex + 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("04", 0, iindex + 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("05", 0, iindex + 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("06", 0, iindex + 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("07", 0, iindex + 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("08", 0, iindex + 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("09", 0, iindex + 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("10", 0, iindex + 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("11", 0, iindex + 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("12", 0, iindex + 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("13", 0, iindex + 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("14", 0, iindex + 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("15", 0, iindex + 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("16", 0, iindex + 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("17", 0, iindex + 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("18", 0, iindex + 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("19", 0, iindex + 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("20", 0, iindex + 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("21", 0, iindex + 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("22", 0, iindex + 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("23", 0, iindex + 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("24", 0, iindex + 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("25", 0, iindex + 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("26", 0, iindex + 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("27", 0, iindex + 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("28", 0, iindex + 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("29", 0, iindex + 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("30", 0, iindex + 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                    SS.RPT_AddBasicColumn("31", 0, iindex + 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
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

        private void fnSSSortInit()
        {
            /****************************************************
             * Comment : SS의 데이터의 정렬규칙을 설정하다.
             * 
             * Created By : bee-jae jung(2010-05-31-월요일)
             * 
             * Modified By : bee-jae jung(2010-05-31-월요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                // CUSTOMER
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "B.MAT_GRP_1", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
                }
                // FAMILY
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "B.MAT_GRP_2", "MAT_GRP_2", "B.MAT_GRP_2", true);
                }
                // PACKAGE
                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "B.MAT_GRP_3", "MAT_GRP_3", "B.MAT_GRP_3", true);
                }
                // TYPE1
                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "B.MAT_GRP_4", "MAT_GRP_4", "B.MAT_GRP_4", true);
                }
                // TYPE2
                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "B.MAT_GRP_5", "MAT_GRP_5", "B.MAT_GRP_5", true);
                }
                // LEAD COUNT
                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LEAD_COUNT", "B.MAT_GRP_6", "MAT_GRP_6", "B.MAT_GRP_6", true);
                }
                // DENSITY
                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "B.MAT_GRP_7", "MAT_GRP_7", "B.MAT_GRP_7", true);
                }
                // GENERATION
                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "B.MAT_GRP_8", "MAT_GRP_8", "B.MAT_GRP_8", true);
                }
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.MAT_ID", "MAT_ID", "A.MAT_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RETURN_TYPE", "A.RETURN_TYPE", "RETURN_TYPE", "A.RETURN_TYPE", true);
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

        // 2010-09-01-정비재 : Package/Procedure를 호출하여 실행하는 구문이 속도가 현저하게 느림.(WebService 쪽에 문제가 있는 것 같음)
        // 2010-07-06-정비재 : Oracle Procedure / Package를 이용하여 데이터를 조회하는 함수
        //private bool fnDataFind()
        //{
        //    /****************************************************
        //     * Comment : DataBase에 저장된 데이터를 조회한다.
        //     * 
        //     * Created By : bee-jae jung(2010-06-29-화요일)
        //     * 
        //     * Modified By : bee-jae jung(2010-06-29-화요일)
        //     ****************************************************/
        //    Miracom.SmartWeb.UI.Controls.udcFarPoint SS;
        //    String sPackage = "", sParameter = "";
        //    try
        //    {
        //        Cursor.Current = Cursors.WaitCursor;
        //        LoadingPopUp.LoadIngPopUpShow(this);

        //        // 2010-06-24-정비재 : Sheet / Listview를 초기화 한다.
        //        CmnInitFunction.ClearList(SS01, true);
        //        CmnInitFunction.ClearList(SS02, true);

        //        cf01.RPT_1_ChartInit(); cf02.RPT_1_ChartInit(); cf03.RPT_1_ChartInit(); cf04.RPT_1_ChartInit();
        //        cf01.RPT_2_ClearData(); cf02.RPT_2_ClearData(); cf03.RPT_2_ClearData(); cf04.RPT_2_ClearData();

        //        if (rbTrand01.Checked == true)
        //        {
        //            // 2010-08-12-정비재 : 고객사별
        //            if (rbS01.Checked == true)
        //            {
        //                // 2010-08-12-정비재 : PPM
        //                sPackage = "PACKAGE_PQC030107.PROC_PQC030107_CUSTOMER_PPM";
        //                sParameter = "P_FACTORY:" + cdvFactory.Text
        //                        + "│ P_BASE_MONTH:" + cdvBaseDate.Value.ToString("yyyyMM")
        //                        + "│ P_BASE_TIME:" + cdvBaseDate.Value.ToString("yyyyMMdd")
        //                        + "│ P_QC_OPER:" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "%"
        //                        + "│ P_QC_TYPE:" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "%"
        //                        + "│ P_CUSTOMER:" + (udcWIPCondition1.Text == "ALL" ? "%" : cdvCustomer.Text) + "%";
        //            }
        //            else if (rbS02.Checked == true)
        //            {
        //                // 2010-08-12-정비재 : LRR
        //                sPackage = "PACKAGE_PQC030107.PROC_PQC030107_CUSTOMER_LRR";
        //                sParameter = "P_FACTORY:" + cdvFactory.Text
        //                        + "│ P_BASE_MONTH:" + cdvBaseDate.Value.ToString("yyyyMM")
        //                        + "│ P_BASE_TIME:" + cdvBaseDate.Value.ToString("yyyyMMdd")
        //                        + "│ P_QC_OPER:" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "%"
        //                        + "│ P_QC_TYPE:" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "%"
        //                        + "│ P_CUSTOMER:" + (udcWIPCondition1.Text == "ALL" ? "%" : cdvCustomer.Text) + "%";
        //            }
        //            else if (rbS03.Checked == true)
        //            {
        //                // 2010-08-12-정비재 : YIELD
        //                sPackage = "PACKAGE_PQC030107.PROC_PQC030107_CUSTOMER_YIELD";
        //                sParameter = "P_FACTORY:" + cdvFactory.Text
        //                        + "│ P_BASE_MONTH:" + cdvBaseDate.Value.ToString("yyyyMM")
        //                        + "│ P_BASE_TIME:" + cdvBaseDate.Value.ToString("yyyyMMdd")
        //                        + "│ P_QC_OPER:" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "%"
        //                        + "│ P_QC_TYPE:" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "%"
        //                        + "│ P_CUSTOMER:" + (udcWIPCondition1.Text == "ALL" ? "%" : cdvCustomer.Text) + "%";
        //            }
        //        }
        //        else if (rbTrand02.Checked == true)
        //        {
        //            // 2010-08-12-정비재 : 주간별
        //            sPackage = "PACKAGE_PQC030107.PROC_PQC030107_WEEK";
        //            sParameter = "P_FACTORY:" + cdvFactory.Text
        //                    + "│ P_BASE_MONTH:" + cdvBaseDate.Value.ToString("yyyyMM")
        //                    + "│ P_BASE_TIME:" + cdvBaseDate.Value.ToString("yyyyMMdd")
        //                    + "│ P_QC_OPER:" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "%"
        //                    + "│ P_QC_TYPE:" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "%";
        //        }
        //        else if (rbTrand03.Checked == true)
        //        {
        //            // 2010-08-12-정비재 : 일자별
        //            sPackage = "PACKAGE_PQC030107.PROC_PQC030107_DAYS";
        //            sParameter = "P_FACTORY:" + cdvFactory.Text
        //                    + "│ P_BASE_MONTH:" + cdvBaseDate.Value.ToString("yyyyMM")
        //                    + "│ P_BASE_TIME:" + cdvBaseDate.Value.ToString("yyyyMMdd")
        //                    + "│ P_QC_OPER:" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "%"
        //                    + "│ P_QC_TYPE:" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "%";
        //        }

        //        DataTable dt = CmnFunction.oComm.fnExecutePackage("DYNAMIC", sPackage, sParameter);

        //        if (dt.Rows.Count <= 0)
        //        {
        //            dt.Dispose();
        //            LoadingPopUp.LoadingPopUpHidden();
        //            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
        //            return false;
        //        }
        //        if (rbTrand03.Checked == true)
        //        {
        //            SS = SS02;
        //        }
        //        else
        //        {
        //            SS = SS01;
        //        }
        //        SS.DataSource = dt;

        //        fnMakeChart(SS, 0);

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        LoadingPopUp.LoadingPopUpHidden();
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //    finally
        //    {
        //        LoadingPopUp.LoadingPopUpHidden();
        //        Cursor.Current = Cursors.Default;
        //    }
        //}

        private bool fnDataFind()
        {
            /****************************************************
             * Comment : DataBase에 저장된 데이터를 조회한다.
             * 
             * Created By : bee-jae jung(2010-06-29-화요일)
             * 
             * Modified By : bee-jae jung(2010-06-29-화요일)
             ****************************************************/
            DataTable dt = null;
            Miracom.SmartWeb.UI.Controls.udcFarPoint SS = null;
            string QRY = "";
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                LoadingPopUp.LoadIngPopUpShow(this);

                // 2010-06-24-정비재 : Sheet / Listview를 초기화 한다.
                CmnInitFunction.ClearList(SS01, true);
                CmnInitFunction.ClearList(SS02, true);

                // 2010-06-29-정비재 : 고객사별 조건이고
                if (rbTrand01.Checked == true)
                {
                    if (rbS01.Checked == true)
                    {
                        QRY = "	SELECT * FROM ( \n"
                            + " SELECT 'PPM' AS GUBUN \n"
                            + "		 , A.CUSTOMER AS CUSTOMER \n"
                            + "		 , SUM(A.DEFECT_PPM_MM_1) AS DEFECT_PPM_MM_1 \n"
                            + "		 , SUM(A.DEFECT_PPM_MM_2) AS DEFECT_PPM_MM_2 \n"
                            + "		 , SUM(A.DEFECT_PPM_MM_3) AS DEFECT_PPM_MM_3 \n"
                            + "		 , SUM(A.DEFECT_PPM_MM_4) AS DEFECT_PPM_MM_4 \n"
                            + "		 , SUM(A.DEFECT_PPM_WW_1) AS DEFECT_PPM_WW_1 \n"
                            + "		 , SUM(A.DEFECT_PPM_WW_2) AS DEFECT_PPM_WW_2 \n"
                            + "		 , SUM(A.DEFECT_PPM_WW_3) AS DEFECT_PPM_WW_3 \n"
                            + "		 , SUM(A.DEFECT_PPM_WW_4) AS DEFECT_PPM_WW_4 \n"
                            + "		 , SUM(A.DEFECT_PPM_DD_1) AS DEFECT_PPM_DD_1 \n"
                            + "		 , SUM(A.DEFECT_PPM_DD_2) AS DEFECT_PPM_DD_2 \n"
                            + "		 , SUM(A.DEFECT_PPM_DD_3) AS DEFECT_PPM_DD_3 \n"
                            + "		 , SUM(A.DEFECT_PPM_DD_4) AS DEFECT_PPM_DD_4 \n"
                            + "		 , SUM(A.DEFECT_PPM_DD_5) AS DEFECT_PPM_DD_5 \n"
                            + "		 , SUM(A.DEFECT_PPM_DD_6) AS DEFECT_PPM_DD_6 \n"
                            + "		 , SUM(A.DEFECT_PPM_DD_7) AS DEFECT_PPM_DD_7 \n"
                            + "	  FROM (SELECT A.CUSTOMER AS CUSTOMER \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_MM_1 > 0 AND B.SAMPLE_QTY_MM_1 > 0 THEN ROUND((A.DEFECT_QTY_MM_1 * 1000000) / B.SAMPLE_QTY_MM_1, 2) ELSE 0 END AS DEFECT_PPM_MM_1 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_MM_2 > 0 AND B.SAMPLE_QTY_MM_2 > 0 THEN ROUND((A.DEFECT_QTY_MM_2 * 1000000) / B.SAMPLE_QTY_MM_2, 2) ELSE 0 END AS DEFECT_PPM_MM_2 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_MM_3 > 0 AND B.SAMPLE_QTY_MM_3 > 0 THEN ROUND((A.DEFECT_QTY_MM_3 * 1000000) / B.SAMPLE_QTY_MM_3, 2) ELSE 0 END AS DEFECT_PPM_MM_3 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_MM_4 > 0 AND B.SAMPLE_QTY_MM_4 > 0 THEN ROUND((A.DEFECT_QTY_MM_4 * 1000000) / B.SAMPLE_QTY_MM_4, 2) ELSE 0 END AS DEFECT_PPM_MM_4 \n"


                            + "	             , 0 AS DEFECT_PPM_WW_1 \n"
                            + "	             , 0 AS DEFECT_PPM_WW_2 \n"
                            + "	             , 0 AS DEFECT_PPM_WW_3 \n"
                            + "	             , 0 AS DEFECT_PPM_WW_4 \n"
                            + "	             , 0 AS DEFECT_PPM_DD_1 \n"
                            + "	             , 0 AS DEFECT_PPM_DD_2 \n"
                            + "	             , 0 AS DEFECT_PPM_DD_3 \n"
                            + "	             , 0 AS DEFECT_PPM_DD_4 \n"
                            + "	             , 0 AS DEFECT_PPM_DD_5 \n"
                            + "	             , 0 AS DEFECT_PPM_DD_6 \n"
                            + "	             , 0 AS DEFECT_PPM_DD_7 \n"
                            ;
                        // 2011-03-30-김민우 ETC로 보여질지 고객사 다 보여질지 선택
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                                + "	                                        WHEN 'HX' THEN 'HX' \n"
                                + "	                                        ELSE 'ETC' \n"
                                + "	                       END AS CUSTOMER  \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n";
                        }

                        QRY = QRY
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_1  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_2 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_3 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_4 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                            + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                            ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                    AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                    AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }

                        QRY = QRY
                            + "	\n                    AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                       
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                                + "	                                           WHEN 'HX' THEN 'HX'  \n"
                                + "	                                           ELSE 'ETC'  \n"
                                + "	                          END \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          GROUP BY B.MAT_GRP_1 \n";
                        }
                        QRY = QRY
                            + "                 UNION ALL \n"
                            + "                 SELECT 'TOTAL' CUSTOMER  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_1  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_2 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_3 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_4 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                            + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                            ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                    AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                    AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }

                        QRY = QRY
                            + "	\n                    AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";

                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        QRY = QRY
                            + "                ) A \n";

                        // 2011-03-30-김민우 ETC로 보여질지 고객사 다 보여질지 선택
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	          , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                                + "	                                        WHEN 'HX' THEN 'HX' \n"
                                + "	                                        ELSE 'ETC' \n"
                                + "	                       END AS CUSTOMER  \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          , (SELECT B.MAT_GRP_1 AS CUSTOMER \n";
                        }

                        QRY = QRY
                            
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_1  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_2 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_3 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_4 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                            + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"

                            ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }

                        QRY = QRY
                            + "	\n                    AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                                + "	                                           WHEN 'HX' THEN 'HX'  \n"
                                + "	                                           ELSE 'ETC'  \n"
                                + "	                          END \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          GROUP BY B.MAT_GRP_1 \n";
                        }
                        QRY = QRY
                           
                            + " UNION ALL \n"
                            + " SELECT 'TOTAL' CUSTOMER  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_1  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_2 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_3 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_4 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                            + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"

                            ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }

                        QRY = QRY
                            + "	\n                    AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                            
                            if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        QRY = QRY
                            + " ) B \n"
                            + "	         WHERE A.CUSTOMER = B.CUSTOMER \n"
                            + "			UNION ALL \n"
                            + "			SELECT A.CUSTOMER AS CUSTOMER \n"
                            + "	             , 0 AS DEFECT_PPM_MM_1  \n"
                            + "	             , 0 AS DEFECT_PPM_MM_2 \n"
                            + "	             , 0 AS DEFECT_PPM_MM_3 \n"
                            + "	             , 0 AS DEFECT_PPM_MM_4 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_WW_1 > 0 AND B.SAMPLE_QTY_WW_1 > 0 THEN ROUND((A.DEFECT_QTY_WW_1 * 1000000) / B.SAMPLE_QTY_WW_1, 2) ELSE 0 END AS DEFECT_PPM_WW_1 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_WW_2 > 0 AND B.SAMPLE_QTY_WW_2 > 0 THEN ROUND((A.DEFECT_QTY_WW_2 * 1000000) / B.SAMPLE_QTY_WW_2, 2) ELSE 0 END AS DEFECT_PPM_WW_2 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_WW_3 > 0 AND B.SAMPLE_QTY_WW_3 > 0 THEN ROUND((A.DEFECT_QTY_WW_3 * 1000000) / B.SAMPLE_QTY_WW_3, 2) ELSE 0 END AS DEFECT_PPM_WW_3 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_WW_4 > 0 AND B.SAMPLE_QTY_WW_4 > 0 THEN ROUND((A.DEFECT_QTY_WW_4 * 1000000) / B.SAMPLE_QTY_WW_4, 2) ELSE 0 END AS DEFECT_PPM_WW_4 \n"
                            + "	             , 0 AS DEFECT_PPM_DD_1 \n"
                            + "	             , 0 AS DEFECT_PPM_DD_2 \n"
                            + "	             , 0 AS DEFECT_PPM_DD_3 \n"
                            + "	             , 0 AS DEFECT_PPM_DD_4 \n"
                            + "	             , 0 AS DEFECT_PPM_DD_5 \n"
                            + "	             , 0 AS DEFECT_PPM_DD_6 \n"
                            + "	             , 0 AS DEFECT_PPM_DD_7 \n"
                            ;
                        // 2011-03-30-김민우 ETC로 보여질지 고객사 다 보여질지 선택
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                                + "	                                        WHEN 'HX' THEN 'HX' \n"
                                + "	                                        ELSE 'ETC' \n"
                                + "	                       END AS CUSTOMER  \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n";
                        }
                        QRY = QRY
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_1 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_2 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_3 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_4 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                      END \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                            ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                                + "	                                           WHEN 'HX' THEN 'HX'  \n"
                                + "	                                           ELSE 'ETC'  \n"
                                + "	                          END \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          GROUP BY B.MAT_GRP_1 \n";
                        }
                        QRY = QRY
                            
                            
                            + "                 UNION ALL \n"
                            + "	                SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_1 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_2 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_3 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_4 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                      END \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                            ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                            if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        QRY = QRY
                            + "                ) A \n";
                           
                        // 2011-03-30-김민우 ETC로 보여질지 고객사 다 보여질지 선택
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	          , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                                + "	                                        WHEN 'HX' THEN 'HX' \n"
                                + "	                                        ELSE 'ETC' \n"
                                + "	                       END AS CUSTOMER  \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          , (SELECT B.MAT_GRP_1 AS CUSTOMER \n";
                        }

                        QRY = QRY
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_1 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_2 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_3 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_4 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                      END \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                                + "	                                           WHEN 'HX' THEN 'HX'  \n"
                                + "	                                           ELSE 'ETC'  \n"
                                + "	                          END \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          GROUP BY B.MAT_GRP_1 \n";
                        }
                        QRY = QRY
                           
                            + "               UNION ALL \n"
                            + "	              SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_1 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_2 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_3 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_4 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                      END \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        QRY = QRY
                            + "                ) B \n"
                            + "	         WHERE A.CUSTOMER = B.CUSTOMER \n"
                            + "			UNION ALL \n"
                            + "			SELECT A.CUSTOMER AS CUSTOMER \n"
                            + "	             , 0 AS DEFECT_PPM_MM_1  \n"
                            + "	             , 0 AS DEFECT_PPM_MM_2 \n"
                            + "	             , 0 AS DEFECT_PPM_MM_3 \n"
                            + "	             , 0 AS DEFECT_PPM_MM_4 \n"
                            + "	             , 0 AS DEFECT_PPM_WW_1 \n"
                            + "	             , 0 AS DEFECT_PPM_WW_2 \n"
                            + "	             , 0 AS DEFECT_PPM_WW_3 \n"
                            + "	             , 0 AS DEFECT_PPM_WW_4 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_1 > 0 AND B.SAMPLE_QTY_DD_1 > 0 THEN ROUND((A.DEFECT_QTY_DD_1 * 1000000) / B.SAMPLE_QTY_DD_1, 2) ELSE 0 END AS DEFECT_PPM_DD_1 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_2 > 0 AND B.SAMPLE_QTY_DD_2 > 0 THEN ROUND((A.DEFECT_QTY_DD_2 * 1000000) / B.SAMPLE_QTY_DD_2, 2) ELSE 0 END AS DEFECT_PPM_DD_2 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_3 > 0 AND B.SAMPLE_QTY_DD_3 > 0 THEN ROUND((A.DEFECT_QTY_DD_3 * 1000000) / B.SAMPLE_QTY_DD_3, 2) ELSE 0 END AS DEFECT_PPM_DD_3 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_4 > 0 AND B.SAMPLE_QTY_DD_4 > 0 THEN ROUND((A.DEFECT_QTY_DD_4 * 1000000) / B.SAMPLE_QTY_DD_4, 2) ELSE 0 END AS DEFECT_PPM_DD_4 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_5 > 0 AND B.SAMPLE_QTY_DD_5 > 0 THEN ROUND((A.DEFECT_QTY_DD_5 * 1000000) / B.SAMPLE_QTY_DD_5, 2) ELSE 0 END AS DEFECT_PPM_DD_5 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_6 > 0 AND B.SAMPLE_QTY_DD_6 > 0 THEN ROUND((A.DEFECT_QTY_DD_6 * 1000000) / B.SAMPLE_QTY_DD_6, 2) ELSE 0 END AS DEFECT_PPM_DD_6 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_7 > 0 AND B.SAMPLE_QTY_DD_7 > 0 THEN ROUND((A.DEFECT_QTY_DD_7 * 1000000) / B.SAMPLE_QTY_DD_7, 2) ELSE 0 END AS DEFECT_PPM_DD_7 \n"
                           ;
                        // 2011-03-30-김민우 ETC로 보여질지 고객사 다 보여질지 선택
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                                + "	                                        WHEN 'HX' THEN 'HX' \n"
                                + "	                                        ELSE 'ETC' \n"
                                + "	                       END AS CUSTOMER  \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n";
                        }
                        QRY = QRY
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_1  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_2 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_3 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_4 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_5 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_6 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                      END  \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                                + "	                                           WHEN 'HX' THEN 'HX'  \n"
                                + "	                                           ELSE 'ETC'  \n"
                                + "	                          END \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          GROUP BY B.MAT_GRP_1 \n";
                        }
                        QRY = QRY
                           
                            + "	                 UNION ALL \n"
                            + "	                 SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_1  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_2 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_3 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_4 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_5 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_6 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                      END  \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        QRY = QRY
                            + "                ) A \n";
                        // 2011-03-30-김민우 ETC로 보여질지 고객사 다 보여질지 선택
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	          , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                                + "	                                        WHEN 'HX' THEN 'HX' \n"
                                + "	                                        ELSE 'ETC' \n"
                                + "	                       END AS CUSTOMER  \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          , (SELECT B.MAT_GRP_1 AS CUSTOMER \n";
                        }
                        QRY = QRY
                        +"	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_1  \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_2 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_3 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_4 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_5 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_6 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_7 \n"
                        + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                        + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                        + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                        + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                        + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                        + "	                                      END  \n"
                        + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n";
                        
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                                + "	                                           WHEN 'HX' THEN 'HX'  \n"
                                + "	                                           ELSE 'ETC'  \n"
                                + "	                          END \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          GROUP BY B.MAT_GRP_1 \n";
                        }
                        QRY = QRY
                           
                            + "	                 UNION ALL \n"
                            + "                  SELECT 'TOTAL' CUSTOMER  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_1  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_2 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_3 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_4 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_5 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_6 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                      END  \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        QRY = QRY
                            + "                    ) B \n"
                            + "	         WHERE A.CUSTOMER = B.CUSTOMER) A \n"
                            + "	 GROUP BY A.CUSTOMER  \n"
                            + "	 UNION ALL  \n";
                        //  + "	 ORDER BY DECODE(A.CUSTOMER, 'SE', 0, 'HX', 1, 2)";




                        //1414 LRR
                        // 2010-06-29-정비재 : 공정검사 품질불량현황 고객사별 LRR
                        QRY = QRY
                            + " SELECT 'LRR' AS GUBUN \n"
                            + "      , A.CUSTOMER AS CUSTOMER \n"
                            + "		 , SUM(A.DEFECT_LRR_MM_1) AS DEFECT_LRR_MM_1 \n"
                            + "		 , SUM(A.DEFECT_LRR_MM_2) AS DEFECT_LRR_MM_2 \n"
                            + "		 , SUM(A.DEFECT_LRR_MM_3) AS DEFECT_LRR_MM_3 \n"
                            + "		 , SUM(A.DEFECT_LRR_MM_4) AS DEFECT_LRR_MM_4 \n"
                            + "		 , SUM(A.DEFECT_LRR_WW_1) AS DEFECT_LRR_WW_1 \n"
                            + "		 , SUM(A.DEFECT_LRR_WW_2) AS DEFECT_LRR_WW_2 \n"
                            + "		 , SUM(A.DEFECT_LRR_WW_3) AS DEFECT_LRR_WW_3 \n"
                            + "		 , SUM(A.DEFECT_LRR_WW_4) AS DEFECT_LRR_WW_4 \n"
                            + "		 , SUM(A.DEFECT_LRR_DD_1) AS DEFECT_LRR_DD_1 \n"
                            + "		 , SUM(A.DEFECT_LRR_DD_2) AS DEFECT_LRR_DD_2 \n"
                            + "		 , SUM(A.DEFECT_LRR_DD_3) AS DEFECT_LRR_DD_3 \n"
                            + "		 , SUM(A.DEFECT_LRR_DD_4) AS DEFECT_LRR_DD_4 \n"
                            + "		 , SUM(A.DEFECT_LRR_DD_5) AS DEFECT_LRR_DD_5 \n"
                            + "		 , SUM(A.DEFECT_LRR_DD_6) AS DEFECT_LRR_DD_6 \n"
                            + "		 , SUM(A.DEFECT_LRR_DD_7) AS DEFECT_LRR_DD_7 \n"
                            + "	  FROM (SELECT A.CUSTOMER AS CUSTOMER \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_MM_1 > 0 AND B.SAMPLE_QTY_MM_1 > 0 THEN ROUND((A.DEFECT_QTY_MM_1 * 100) / B.SAMPLE_QTY_MM_1, 2) ELSE 0 END AS DEFECT_LRR_MM_1 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_MM_2 > 0 AND B.SAMPLE_QTY_MM_2 > 0 THEN ROUND((A.DEFECT_QTY_MM_2 * 100) / B.SAMPLE_QTY_MM_2, 2) ELSE 0 END AS DEFECT_LRR_MM_2 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_MM_3 > 0 AND B.SAMPLE_QTY_MM_3 > 0 THEN ROUND((A.DEFECT_QTY_MM_3 * 100) / B.SAMPLE_QTY_MM_3, 2) ELSE 0 END AS DEFECT_LRR_MM_3 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_MM_4 > 0 AND B.SAMPLE_QTY_MM_4 > 0 THEN ROUND((A.DEFECT_QTY_MM_4 * 100) / B.SAMPLE_QTY_MM_4, 2) ELSE 0 END AS DEFECT_LRR_MM_4 \n"
                            + "	             , 0 AS DEFECT_LRR_WW_1 \n"
                            + "	             , 0 AS DEFECT_LRR_WW_2 \n"
                            + "	             , 0 AS DEFECT_LRR_WW_3 \n"
                            + "	             , 0 AS DEFECT_LRR_WW_4 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_1 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_2 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_3 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_4 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_5 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_6 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_7 \n"
                            ;
                        // 2011-03-30-김민우 ETC로 보여질지 고객사 다 보여질지 선택
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                                + "	                                        WHEN 'HX' THEN 'HX' \n"
                                + "	                                        ELSE 'ETC' \n"
                                + "	                       END AS CUSTOMER  \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n";
                        }

                        QRY = QRY
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_1  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_2 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_3 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_4 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                            + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                                + "	                                           WHEN 'HX' THEN 'HX'  \n"
                                + "	                                           ELSE 'ETC'  \n"
                                + "	                          END \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          GROUP BY B.MAT_GRP_1 \n";
                        }
                        QRY = QRY
                           
                            + "	          UNION ALL \n"
                            + "	          SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_1  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_2 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_3 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_4 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                            + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        QRY = QRY
                            + "                ) A \n";    
                        // 2011-03-30-김민우 ETC로 보여질지 고객사 다 보여질지 선택
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	          , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                                + "	                                        WHEN 'HX' THEN 'HX' \n"
                                + "	                                        ELSE 'ETC' \n"
                                + "	                       END AS CUSTOMER  \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          , (SELECT B.MAT_GRP_1 AS CUSTOMER \n";
                        }
                        QRY = QRY
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_1  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_2 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_3 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_4 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                            + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                                + "	                                           WHEN 'HX' THEN 'HX'  \n"
                                + "	                                           ELSE 'ETC'  \n"
                                + "	                          END \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          GROUP BY B.MAT_GRP_1 \n";
                        }
                        QRY = QRY
                           
                            + "	             UNION ALL \n"
                            + "	             SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_1  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_2 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_3 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_4 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                            + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        QRY = QRY
                            + "              ) B \n"
                            + "	         WHERE A.CUSTOMER = B.CUSTOMER \n"
                            + "			UNION ALL \n"
                            + "			SELECT A.CUSTOMER AS CUSTOMER \n"
                            + "	             , 0 AS DEFECT_LRR_MM_1  \n"
                            + "	             , 0 AS DEFECT_LRR_MM_2 \n"
                            + "	             , 0 AS DEFECT_LRR_MM_3 \n"
                            + "	             , 0 AS DEFECT_LRR_MM_4 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_WW_1 > 0 AND B.SAMPLE_QTY_WW_1 > 0 THEN ROUND((A.DEFECT_QTY_WW_1 * 100) / B.SAMPLE_QTY_WW_1, 2) ELSE 0 END AS DEFECT_LRR_WW_1 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_WW_2 > 0 AND B.SAMPLE_QTY_WW_2 > 0 THEN ROUND((A.DEFECT_QTY_WW_2 * 100) / B.SAMPLE_QTY_WW_2, 2) ELSE 0 END AS DEFECT_LRR_WW_2 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_WW_3 > 0 AND B.SAMPLE_QTY_WW_3 > 0 THEN ROUND((A.DEFECT_QTY_WW_3 * 100) / B.SAMPLE_QTY_WW_3, 2) ELSE 0 END AS DEFECT_LRR_WW_3 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_WW_4 > 0 AND B.SAMPLE_QTY_WW_4 > 0 THEN ROUND((A.DEFECT_QTY_WW_4 * 100) / B.SAMPLE_QTY_WW_4, 2) ELSE 0 END AS DEFECT_LRR_WW_4 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_1 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_2 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_3 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_4 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_5 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_6 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_7 \n"
                            ;
                        // 2011-03-30-김민우 ETC로 보여질지 고객사 다 보여질지 선택
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                                + "	                                        WHEN 'HX' THEN 'HX' \n"
                                + "	                                        ELSE 'ETC' \n"
                                + "	                       END AS CUSTOMER  \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n";
                        }
                        QRY = QRY
                            
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_1 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_2 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_3 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_4 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                      END \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                                + "	                                           WHEN 'HX' THEN 'HX'  \n"
                                + "	                                           ELSE 'ETC'  \n"
                                + "	                          END \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          GROUP BY B.MAT_GRP_1 \n";
                        }
                        QRY = QRY
                           
                            + "	          UNION ALL \n"
                            + "	          SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_1 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_2 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_3 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_4 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                      END \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        QRY = QRY
                            + "                ) A \n";    
                        // 2011-03-30-김민우 ETC로 보여질지 고객사 다 보여질지 선택
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	          , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                                + "	                                        WHEN 'HX' THEN 'HX' \n"
                                + "	                                        ELSE 'ETC' \n"
                                + "	                       END AS CUSTOMER  \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          , (SELECT B.MAT_GRP_1 AS CUSTOMER \n";
                        }
                        QRY = QRY
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_1 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_2 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_3 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_4 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                      END \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                                + "	                                           WHEN 'HX' THEN 'HX'  \n"
                                + "	                                           ELSE 'ETC'  \n"
                                + "	                          END \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          GROUP BY B.MAT_GRP_1 \n";
                        }
                        QRY = QRY
                            + "               UNION ALL \n"
                            + "	              SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_1 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_2 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_3 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_4 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                      END \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        QRY = QRY
                            + "                 ) B \n"
                            + "	         WHERE A.CUSTOMER = B.CUSTOMER \n"
                            + "			UNION ALL \n"
                            + "			SELECT A.CUSTOMER AS CUSTOMER \n"
                            + "	             , 0 AS DEFECT_LRR_MM_1  \n"
                            + "	             , 0 AS DEFECT_LRR_MM_2 \n"
                            + "	             , 0 AS DEFECT_LRR_MM_3 \n"
                            + "	             , 0 AS DEFECT_LRR_MM_4 \n"
                            + "	             , 0 AS DEFECT_LRR_WW_1 \n"
                            + "	             , 0 AS DEFECT_LRR_WW_2 \n"
                            + "	             , 0 AS DEFECT_LRR_WW_3 \n"
                            + "	             , 0 AS DEFECT_LRR_WW_4 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_1 > 0 AND B.SAMPLE_QTY_DD_1 > 0 THEN ROUND((A.DEFECT_QTY_DD_1 * 100) / B.SAMPLE_QTY_DD_1, 2) ELSE 0 END AS DEFECT_LRR_DD_1 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_2 > 0 AND B.SAMPLE_QTY_DD_2 > 0 THEN ROUND((A.DEFECT_QTY_DD_2 * 100) / B.SAMPLE_QTY_DD_2, 2) ELSE 0 END AS DEFECT_LRR_DD_2 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_3 > 0 AND B.SAMPLE_QTY_DD_3 > 0 THEN ROUND((A.DEFECT_QTY_DD_3 * 100) / B.SAMPLE_QTY_DD_3, 2) ELSE 0 END AS DEFECT_LRR_DD_3 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_4 > 0 AND B.SAMPLE_QTY_DD_4 > 0 THEN ROUND((A.DEFECT_QTY_DD_4 * 100) / B.SAMPLE_QTY_DD_4, 2) ELSE 0 END AS DEFECT_LRR_DD_4 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_5 > 0 AND B.SAMPLE_QTY_DD_5 > 0 THEN ROUND((A.DEFECT_QTY_DD_5 * 100) / B.SAMPLE_QTY_DD_5, 2) ELSE 0 END AS DEFECT_LRR_DD_5 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_6 > 0 AND B.SAMPLE_QTY_DD_6 > 0 THEN ROUND((A.DEFECT_QTY_DD_6 * 100) / B.SAMPLE_QTY_DD_6, 2) ELSE 0 END AS DEFECT_LRR_DD_6 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_7 > 0 AND B.SAMPLE_QTY_DD_7 > 0 THEN ROUND((A.DEFECT_QTY_DD_7 * 100) / B.SAMPLE_QTY_DD_7, 2) ELSE 0 END AS DEFECT_LRR_DD_7 \n"
                            ;
                        // 2011-03-30-김민우 ETC로 보여질지 고객사 다 보여질지 선택
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                                + "	                                        WHEN 'HX' THEN 'HX' \n"
                                + "	                                        ELSE 'ETC' \n"
                                + "	                       END AS CUSTOMER  \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          FROM (SELECT B.MAT_GRP_1 AS CUSTOMER \n";
                        }
                        QRY = QRY
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_1  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_2 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_3 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_4 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_5 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_6 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                      END  \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                                + "	                                           WHEN 'HX' THEN 'HX'  \n"
                                + "	                                           ELSE 'ETC'  \n"
                                + "	                          END \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          GROUP BY B.MAT_GRP_1 \n";
                        }
                        QRY = QRY
                            + "	                UNION ALL \n"
                            + "	                SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_1  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_2 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_3 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_4 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_5 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_6 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                      END  \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        QRY = QRY
                            + "                ) A \n";    
                        // 2011-03-30-김민우 ETC로 보여질지 고객사 다 보여질지 선택
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	          , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                                + "	                                        WHEN 'HX' THEN 'HX' \n"
                                + "	                                        ELSE 'ETC' \n"
                                + "	                       END AS CUSTOMER  \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          , (SELECT B.MAT_GRP_1 AS CUSTOMER \n";
                        }

                        QRY = QRY
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_1  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_2 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_3 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_4 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_5 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_6 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                      END  \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                                + "	                                           WHEN 'HX' THEN 'HX'  \n"
                                + "	                                           ELSE 'ETC'  \n"
                                + "	                          END \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          GROUP BY B.MAT_GRP_1 \n";
                        }
                        QRY = QRY
                            + "	                UNION ALL \n"
                            + "	                SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_1  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_2 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_3 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_4 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_5 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_6 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                      END  \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        QRY = QRY
                            + "          ) B \n"
                            + "	         WHERE A.CUSTOMER = B.CUSTOMER) A \n"
                            + "	 GROUP BY A.CUSTOMER  \n";
                        QRY = QRY
                            + " UNION ALL \n"
                            + "	SELECT GUBUN, CUSTOMER \n"
                            + "		 , SUM(QTY_MM_1) AS QTY_MM_1 \n"
                            + "		 , SUM(QTY_MM_2) AS QTY_MM_2 \n"
                            + "		 , SUM(QTY_MM_3) AS QTY_MM_3 \n"
                            + "		 , SUM(QTY_MM_4) AS QTY_MM_4 \n"
                            + "		 , SUM(QTY_WW_1) AS QTY_WW_1 \n"
                            + "		 , SUM(QTY_WW_2) AS QTY_WW_2 \n"
                            + "		 , SUM(QTY_WW_3) AS QTY_WW_3 \n"
                            + "		 , SUM(QTY_WW_4) AS QTY_WW_4 \n"
                            + "		 , SUM(QTY_DD_1) AS QTY_DD_1 \n"
                            + "		 , SUM(QTY_DD_2) AS QTY_DD_2 \n"
                            + "		 , SUM(QTY_DD_3) AS QTY_DD_3 \n"
                            + "		 , SUM(QTY_DD_4) AS QTY_DD_4 \n"
                            + "		 , SUM(QTY_DD_5) AS QTY_DD_5 \n"
                            + "		 , SUM(QTY_DD_6) AS QTY_DD_6 \n"
                            + "		 , SUM(QTY_DD_7) AS QTY_DD_7 \n"
                            ;
                        // 2011-03-30-김민우 ETC로 보여질지 고객사 다 보여질지 선택
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC' \n"
                                + "	                                        WHEN 'HX' THEN 'Hynix' \n"
                                + "	                                        ELSE 'ETC' \n"
                                + "	                       END AS GUBUN  \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          FROM (SELECT B.MAT_GRP_1 AS GUBUN \n";
                        }
                        QRY = QRY
                           
                            + "	                     , '불량수' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS QTY_MM_1 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS QTY_MM_2 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS QTY_MM_3 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS QTY_MM_4 \n"
                            + "		                 , 0 AS QTY_WW_1 \n"
                            + "		                 , 0 AS QTY_WW_2 \n"
                            + "		                 , 0 AS QTY_WW_3 \n"
                            + "		                 , 0 AS QTY_WW_4 \n"
                            + "		                 , 0 AS QTY_DD_1 \n"
                            + "		                 , 0 AS QTY_DD_2 \n"
                            + "		                 , 0 AS QTY_DD_3 \n"
                            + "		                 , 0 AS QTY_DD_4 \n"
                            + "		                 , 0 AS QTY_DD_5 \n"
                            + "		                 , 0 AS QTY_DD_6 \n"
                            + "		                 , 0 AS QTY_DD_7 \n"

                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                            + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                            ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                    AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                    AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }

                        QRY = QRY
                            + "	\n                    AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC'  \n"
                                + "	                                           WHEN 'HX' THEN 'Hynix'  \n"
                                + "	                                           ELSE 'ETC'  \n"
                                + "	                          END \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          GROUP BY B.MAT_GRP_1 \n";
                        }
                        QRY = QRY
                           
                            + "                 UNION ALL \n"
                            + "                 SELECT 'TOTAL(세부내용)' AS GUBUN  \n"
                            + "                      , '불량수' GUBUN  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS QTY_MM_1  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS QTY_MM_2 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS QTY_MM_3 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS QTY_MM_4 \n"
                            + "		                 , 0 AS QTY_WW_1 \n"
                            + "		                 , 0 AS QTY_WW_2 \n"
                            + "		                 , 0 AS QTY_WW_3 \n"
                            + "		                 , 0 AS QTY_WW_4 \n"
                            + "		                 , 0 AS QTY_DD_1 \n"
                            + "		                 , 0 AS QTY_DD_2 \n"
                            + "		                 , 0 AS QTY_DD_3 \n"
                            + "		                 , 0 AS QTY_DD_4 \n"
                            + "		                 , 0 AS QTY_DD_5 \n"
                            + "		                 , 0 AS QTY_DD_6 \n"
                            + "		                 , 0 AS QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                            + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                            ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                    AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                    AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }

                        QRY = QRY
                            + "	\n                    AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        QRY = QRY
                            + "	             UNION ALL \n";
                        // 2011-03-30-김민우 ETC로 보여질지 고객사 다 보여질지 선택
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	          SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC' \n"
                                + "	                                        WHEN 'HX' THEN 'Hynix' \n"
                                + "	                                        ELSE 'ETC' \n"
                                + "	                       END AS GUBUN \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          SELECT B.MAT_GRP_1 AS GUBUN \n";
                        }

                        QRY = QRY
                            + "	                     , '검사수' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS QTY_MM_1  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS QTY_MM_2 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS QTY_MM_3 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS QTY_MM_4 \n"
                            + "		                 , 0 AS QTY_WW_1 \n"
                            + "		                 , 0 AS QTY_WW_2 \n"
                            + "		                 , 0 AS QTY_WW_3 \n"
                            + "		                 , 0 AS QTY_WW_4 \n"
                            + "		                 , 0 AS QTY_DD_1 \n"
                            + "		                 , 0 AS QTY_DD_2 \n"
                            + "		                 , 0 AS QTY_DD_3 \n"
                            + "		                 , 0 AS QTY_DD_4 \n"
                            + "		                 , 0 AS QTY_DD_5 \n"
                            + "		                 , 0 AS QTY_DD_6 \n"
                            + "		                 , 0 AS QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                            + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"

                            ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }

                        QRY = QRY
                            + "	\n                    AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC'  \n"
                                + "	                                           WHEN 'HX' THEN 'Hynix'  \n"
                                + "	                                           ELSE 'ETC'  \n"
                                + "	                          END \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          GROUP BY B.MAT_GRP_1 \n";
                        }
                        QRY = QRY
                          
                            + " UNION ALL \n"
                            + " SELECT 'TOTAL(세부내용)' GUBUN  \n"
                            + "        , '검사수' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_1  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_2 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_3 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_4 \n"
                            + "		                 , 0 AS QTY_WW_1 \n"
                            + "		                 , 0 AS QTY_WW_2 \n"
                            + "		                 , 0 AS QTY_WW_3 \n"
                            + "		                 , 0 AS QTY_WW_4 \n"
                            + "		                 , 0 AS QTY_DD_1 \n"
                            + "		                 , 0 AS QTY_DD_2 \n"
                            + "		                 , 0 AS QTY_DD_3 \n"
                            + "		                 , 0 AS QTY_DD_4 \n"
                            + "		                 , 0 AS QTY_DD_5 \n"
                            + "		                 , 0 AS QTY_DD_6 \n"
                            + "		                 , 0 AS QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                            + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"

                            ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }

                        QRY = QRY
                            + "	\n                    AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        QRY = QRY
                            + "			UNION ALL \n";
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	          SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC' \n"
                                + "	                                        WHEN 'HX' THEN 'Hynix' \n"
                                + "	                                        ELSE 'ETC' \n"
                                + "	                       END AS GUBUN \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          SELECT B.MAT_GRP_1 AS GUBUN \n";
                        }
                        QRY = QRY
                            + "	                     , '불량수' AS CUSTOMER  \n"
                            + "	                     , 0 AS QTY_MM_1  \n"
                            + "	                     , 0 AS QTY_MM_2  \n"
                            + "	                     , 0 AS QTY_MM_3  \n"
                            + "	                     , 0 AS QTY_MM_4  \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_1 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_2 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_3 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_4 \n"

                            + "	                     , 0 AS QTY_DD_1 \n"
                            + "	                     , 0 AS QTY_DD_2 \n"
                            + "	                     , 0 AS QTY_DD_3 \n"
                            + "	                     , 0 AS QTY_DD_4 \n"
                            + "	                     , 0 AS QTY_DD_5 \n"
                            + "	                     , 0 AS QTY_DD_6 \n"
                            + "	                     , 0 AS QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                      END \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                            ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC'  \n"
                                + "	                                           WHEN 'HX' THEN 'Hynix'  \n"
                                + "	                                           ELSE 'ETC'  \n"
                                + "	                          END \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          GROUP BY B.MAT_GRP_1 \n";
                        }
                        QRY = QRY
                           
                            + "                 UNION ALL \n"
                            + "	                SELECT 'TOTAL(세부내용)' AS GUBUN  \n"
                            + "	                     , '불량수' AS CUSTOMER  \n"
                            + "	                     , 0 AS QTY_MM_1  \n"
                            + "	                     , 0 AS QTY_MM_2  \n"
                            + "	                     , 0 AS QTY_MM_3  \n"
                            + "	                     , 0 AS QTY_MM_4  \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_1 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_2 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_3 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_4 \n"

                            + "	                     , 0 AS QTY_DD_1 \n"
                            + "	                     , 0 AS QTY_DD_2 \n"
                            + "	                     , 0 AS QTY_DD_3 \n"
                            + "	                     , 0 AS QTY_DD_4 \n"
                            + "	                     , 0 AS QTY_DD_5 \n"
                            + "	                     , 0 AS QTY_DD_6 \n"
                            + "	                     , 0 AS QTY_DD_7 \n"



                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                      END \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                            ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        QRY = QRY
                            + "	               UNION ALL \n";
                         
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	          SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC' \n"
                                + "	                                        WHEN 'HX' THEN 'Hynix' \n"
                                + "	                                        ELSE 'ETC' \n"
                                + "	                       END AS GUBUN \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          SELECT B.MAT_GRP_1 AS GUBUN \n";
                        }
                        QRY = QRY
                            + "	                     , '검사수' AS CUSTOMER  \n"

                            + "	                     , 0 AS QTY_MM_1  \n"
                            + "	                     , 0 AS QTY_MM_2  \n"
                            + "	                     , 0 AS QTY_MM_3  \n"
                            + "	                     , 0 AS QTY_MM_4  \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_1 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_2 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_3 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_4 \n"
                            + "	                     , 0 AS QTY_DD_1 \n"
                            + "	                     , 0 AS QTY_DD_2 \n"
                            + "	                     , 0 AS QTY_DD_3 \n"
                            + "	                     , 0 AS QTY_DD_4 \n"
                            + "	                     , 0 AS QTY_DD_5 \n"
                            + "	                     , 0 AS QTY_DD_6 \n"
                            + "	                     , 0 AS QTY_DD_7 \n"

                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                      END \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC'  \n"
                                + "	                                           WHEN 'HX' THEN 'Hynix'  \n"
                                + "	                                           ELSE 'ETC'  \n"
                                + "	                          END \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          GROUP BY B.MAT_GRP_1 \n";
                        }
                        QRY = QRY
                          
                            + "                 UNION ALL \n"
                            + "	                SELECT 'TOTAL(세부내용)' AS GUBUN  \n"
                            + "	                     , '검사수' AS CUSTOMER  \n"
                            + "	                     , 0 AS QTY_MM_1  \n"
                            + "	                     , 0 AS QTY_MM_2  \n"
                            + "	                     , 0 AS QTY_MM_3  \n"
                            + "	                     , 0 AS QTY_MM_4  \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_1 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_2 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_3 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_4 \n"
                            + "	                     , 0 AS QTY_DD_1 \n"
                            + "	                     , 0 AS QTY_DD_2 \n"
                            + "	                     , 0 AS QTY_DD_3 \n"
                            + "	                     , 0 AS QTY_DD_4 \n"
                            + "	                     , 0 AS QTY_DD_5 \n"
                            + "	                     , 0 AS QTY_DD_6 \n"
                            + "	                     , 0 AS QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                      END \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        QRY = QRY
                            + "			UNION ALL \n";
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	          SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC' \n"
                                + "	                                        WHEN 'HX' THEN 'Hynix' \n"
                                + "	                                        ELSE 'ETC' \n"
                                + "	                       END AS GUBUN \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          SELECT B.MAT_GRP_1 AS GUBUN \n";
                        }
                        QRY = QRY
                        + "	                     , '불량수' AS CUSTOMER  \n"
                        + "	                     , 0 AS QTY_MM_1  \n"
                        + "	                     , 0 AS QTY_MM_2  \n"
                        + "	                     , 0 AS QTY_MM_3  \n"
                        + "	                     , 0 AS QTY_MM_4  \n"
                        + "	                     , 0 AS QTY_WW_1 \n"
                        + "	                     , 0 AS QTY_WW_2 \n"
                        + "	                     , 0 AS QTY_WW_3 \n"
                        + "	                     , 0 AS QTY_WW_4 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.DEFECT_QTY_1 ELSE 0 END) AS QTY_DD_1  \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.DEFECT_QTY_1 ELSE 0 END) AS QTY_DD_2 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.DEFECT_QTY_1 ELSE 0 END) AS QTY_DD_3 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.DEFECT_QTY_1 ELSE 0 END) AS QTY_DD_4 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.DEFECT_QTY_1 ELSE 0 END) AS QTY_DD_5 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.DEFECT_QTY_1 ELSE 0 END) AS QTY_DD_6 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.DEFECT_QTY_1 ELSE 0 END) AS QTY_DD_7 \n"
                        + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                        + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                        + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                        + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                        + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                        + "	                                      END  \n"
                        + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n";

                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC'  \n"
                                + "	                                           WHEN 'HX' THEN 'Hynix'  \n"
                                + "	                                           ELSE 'ETC'  \n"
                                + "	                          END \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          GROUP BY B.MAT_GRP_1 \n";
                        }
                        QRY = QRY
                           
                            + "	                 UNION ALL \n"
                            + "	                SELECT 'TOTAL(세부내용)' AS GUBUN  \n"
                            + "	                     , '불량수' AS CUSTOMER  \n"
                            + "	                     , 0 AS QTY_MM_1  \n"
                            + "	                     , 0 AS QTY_MM_2  \n"
                            + "	                     , 0 AS QTY_MM_3  \n"
                            + "	                     , 0 AS QTY_MM_4  \n"
                            + "	                     , 0 AS QTY_WW_1 \n"
                            + "	                     , 0 AS QTY_WW_2 \n"
                            + "	                     , 0 AS QTY_WW_3 \n"
                            + "	                     , 0 AS QTY_WW_4 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_1  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_2 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_3 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_4 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_5 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_6 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                      END  \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        QRY = QRY
                            + "                UNION ALL \n";
                        if (ckDetail.Checked == false)
                        {
                            QRY = QRY
                                + "	          SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC' \n"
                                + "	                                        WHEN 'HX' THEN 'Hynix' \n"
                                + "	                                        ELSE 'ETC' \n"
                                + "	                       END AS GUBUN \n";

                        }
                        else
                        {
                            QRY = QRY
                                + "	          SELECT B.MAT_GRP_1 AS GUBUN \n";
                        }
                        QRY = QRY
                        + "	                     , '검사수' AS CUSTOMER  \n"
                        + "	                     , 0 AS QTY_MM_1  \n"
                        + "	                     , 0 AS QTY_MM_2  \n"
                        + "	                     , 0 AS QTY_MM_3  \n"
                        + "	                     , 0 AS QTY_MM_4  \n"
                        + "	                     , 0 AS QTY_WW_1 \n"
                        + "	                     , 0 AS QTY_WW_2 \n"
                        + "	                     , 0 AS QTY_WW_3 \n"
                        + "	                     , 0 AS QTY_WW_4 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_1  \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_2 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_3 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_4 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_5 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_6 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_7 \n"
                        + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                        + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                        + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                        + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                        + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                        + "	                                      END  \n"
                        + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                    ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }
                    QRY = QRY
                        + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                        + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                    if (cbMemory.Text.Equals("MEMORY"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                    }
                    if (cbMemory.Text.Equals("S-LSI"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                    }
                    if (ckDetail.Checked == false)
                    {
                        QRY = QRY
                            + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC'  \n"
                            + "	                                           WHEN 'HX' THEN 'Hynix'  \n"
                            + "	                                           ELSE 'ETC'  \n"
                            + "	                          END \n";

                    }
                    else
                    {
                        QRY = QRY
                            + "	          GROUP BY B.MAT_GRP_1 \n";
                    }
                    QRY = QRY
                       
                        + "	                 UNION ALL \n"
                        + "                  SELECT 'TOTAL(세부내용)' AS GUBUN  \n"
                        + "                      , '검사수' AS CUSTOMER  \n"
                        + "	                     , 0 AS QTY_MM_1  \n"
                        + "	                     , 0 AS QTY_MM_2  \n"
                        + "	                     , 0 AS QTY_MM_3  \n"
                        + "	                     , 0 AS QTY_MM_4  \n"
                        + "	                     , 0 AS QTY_WW_1 \n"
                        + "	                     , 0 AS QTY_WW_2 \n"
                        + "	                     , 0 AS QTY_WW_3 \n"
                        + "	                     , 0 AS QTY_WW_4 \n"

                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_1  \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_2 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_3 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_4 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_5 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_6 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_7 \n"
                        + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                        + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                        + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                        + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                        + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                        + "	                                      END  \n"
                        + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                    ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }
                    QRY = QRY
                        + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                        + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                    if (cbMemory.Text.Equals("MEMORY"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                    }
                    if (cbMemory.Text.Equals("S-LSI"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                    }
                    QRY = QRY
                        + " UNION ALL \n"
                        ;
                    if (ckDetail.Checked == false)
                    {
                        QRY = QRY
                            + "	          SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC' \n"
                            + "	                                        WHEN 'HX' THEN 'Hynix' \n"
                            + "	                                        ELSE 'ETC' \n"
                            + "	                       END AS GUBUN \n";

                    }
                    else
                    {
                        QRY = QRY
                            + "	          SELECT B.MAT_GRP_1 AS GUBUN \n";
                    }
                    QRY = QRY
                        + "	                     , '불합격LOT수' AS CUSTOMER  \n"
                        + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_MM_1 \n"
                        + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_MM_2 \n"
                        + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_MM_3 \n"
                        + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_MM_4 \n"
                        + "		                 , 0 AS QTY_WW_1 \n"
                        + "		                 , 0 AS QTY_WW_2 \n"
                        + "		                 , 0 AS QTY_WW_3 \n"
                        + "		                 , 0 AS QTY_WW_4 \n"
                        + "		                 , 0 AS QTY_DD_1 \n"
                        + "		                 , 0 AS QTY_DD_2 \n"
                        + "		                 , 0 AS QTY_DD_3 \n"
                        + "		                 , 0 AS QTY_DD_4 \n"
                        + "		                 , 0 AS QTY_DD_5 \n"
                        + "		                 , 0 AS QTY_DD_6 \n"
                        + "		                 , 0 AS QTY_DD_7 \n"

                        + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                        + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                        + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                        + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                        + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                        + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                        ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                    AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                    AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }

                    QRY = QRY
                        + "	\n                    AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                        + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                    if (cbMemory.Text.Equals("MEMORY"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                    }
                    if (cbMemory.Text.Equals("S-LSI"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                    }
                    if (ckDetail.Checked == false)
                    {
                        QRY = QRY
                            + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC'  \n"
                            + "	                                           WHEN 'HX' THEN 'Hynix'  \n"
                            + "	                                           ELSE 'ETC'  \n"
                            + "	                          END \n";

                    }
                    else
                    {
                        QRY = QRY
                            + "	          GROUP BY B.MAT_GRP_1 \n";
                    }
                    QRY = QRY
                       
                        + "                 UNION ALL \n"
                        + "                 SELECT 'TOTAL(세부내용)' AS GUBUN  \n"
                        + "                      , '불합격LOT수' GUBUN  \n"
                        + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_MM_1  \n"
                        + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_MM_2 \n"
                        + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_MM_3 \n"
                        + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_MM_4 \n"
                        + "		                 , 0 AS QTY_WW_1 \n"
                        + "		                 , 0 AS QTY_WW_2 \n"
                        + "		                 , 0 AS QTY_WW_3 \n"
                        + "		                 , 0 AS QTY_WW_4 \n"
                        + "		                 , 0 AS QTY_DD_1 \n"
                        + "		                 , 0 AS QTY_DD_2 \n"
                        + "		                 , 0 AS QTY_DD_3 \n"
                        + "		                 , 0 AS QTY_DD_4 \n"
                        + "		                 , 0 AS QTY_DD_5 \n"
                        + "		                 , 0 AS QTY_DD_6 \n"
                        + "		                 , 0 AS QTY_DD_7 \n"
                        + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                        + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                        + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                        + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                        + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                        + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                        ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                    AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                    AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }

                    QRY = QRY
                        + "	\n                    AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                        + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        QRY = QRY
                        + "	             UNION ALL \n";
                        
                    if (ckDetail.Checked == false)
                    {
                        QRY = QRY
                            + "	          SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC' \n"
                            + "	                                        WHEN 'HX' THEN 'Hynix' \n"
                            + "	                                        ELSE 'ETC' \n"
                            + "	                       END AS GUBUN \n";

                    }
                    else
                    {
                        QRY = QRY
                            + "	          SELECT B.MAT_GRP_1 AS GUBUN \n";
                    }
                    QRY = QRY
                        + "	                     , '검사LOT수' AS CUSTOMER  \n"
                        + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_MM_1  \n"
                        + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_MM_2 \n"
                        + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_MM_3 \n"
                        + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_MM_4 \n"
                        + "		                 , 0 AS QTY_WW_1 \n"
                        + "		                 , 0 AS QTY_WW_2 \n"
                        + "		                 , 0 AS QTY_WW_3 \n"
                        + "		                 , 0 AS QTY_WW_4 \n"
                        + "		                 , 0 AS QTY_DD_1 \n"
                        + "		                 , 0 AS QTY_DD_2 \n"
                        + "		                 , 0 AS QTY_DD_3 \n"
                        + "		                 , 0 AS QTY_DD_4 \n"
                        + "		                 , 0 AS QTY_DD_5 \n"
                        + "		                 , 0 AS QTY_DD_6 \n"
                        + "		                 , 0 AS QTY_DD_7 \n"
                        + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                        + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                        + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                        + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                        + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                        + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"

                        ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }

                    QRY = QRY
                        + "	\n                    AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                        + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                    if (cbMemory.Text.Equals("MEMORY"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                    }
                    if (cbMemory.Text.Equals("S-LSI"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                    }
                    if (ckDetail.Checked == false)
                    {
                        QRY = QRY
                            + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC'  \n"
                            + "	                                           WHEN 'HX' THEN 'Hynix'  \n"
                            + "	                                           ELSE 'ETC'  \n"
                            + "	                          END \n";

                    }
                    else
                    {
                        QRY = QRY
                            + "	          GROUP BY B.MAT_GRP_1 \n";
                    }
                    QRY = QRY
                       
                        + " UNION ALL \n"
                        + " SELECT 'TOTAL(세부내용)' GUBUN  \n"
                        + "        , '검사LOT수' AS CUSTOMER  \n"
                        + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_1  \n"
                        + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_2 \n"
                        + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_3 \n"
                        + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_4 \n"
                        + "		                 , 0 AS QTY_WW_1 \n"
                        + "		                 , 0 AS QTY_WW_2 \n"
                        + "		                 , 0 AS QTY_WW_3 \n"
                        + "		                 , 0 AS QTY_WW_4 \n"
                        + "		                 , 0 AS QTY_DD_1 \n"
                        + "		                 , 0 AS QTY_DD_2 \n"
                        + "		                 , 0 AS QTY_DD_3 \n"
                        + "		                 , 0 AS QTY_DD_4 \n"
                        + "		                 , 0 AS QTY_DD_5 \n"
                        + "		                 , 0 AS QTY_DD_6 \n"
                        + "		                 , 0 AS QTY_DD_7 \n"
                        + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                        + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                        + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                        + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                        + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                        + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"

                        ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }

                    QRY = QRY
                        + "	\n                    AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                        + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        QRY = QRY
                            + "			UNION ALL \n";
                    if (ckDetail.Checked == false)
                    {
                        QRY = QRY
                            + "	          SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC' \n"
                            + "	                                        WHEN 'HX' THEN 'Hynix' \n"
                            + "	                                        ELSE 'ETC' \n"
                            + "	                       END AS GUBUN \n";

                    }
                    else
                    {
                        QRY = QRY
                            + "	          SELECT B.MAT_GRP_1 AS GUBUN \n";
                    }
                    QRY = QRY
                        + "	                     , '불합격LOT수' AS CUSTOMER  \n"
                        + "	                     , 0 AS QTY_MM_1  \n"
                        + "	                     , 0 AS QTY_MM_2  \n"
                        + "	                     , 0 AS QTY_MM_3  \n"
                        + "	                     , 0 AS QTY_MM_4  \n"
                        + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                        + "	                                                    END \n"
                        + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                        + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_WW_1 \n"
                        + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                        + "	                                                    END \n"
                        + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                        + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_WW_2 \n"
                        + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                        + "	                                                    END \n"
                        + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                        + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_WW_3 \n"
                        + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                        + "	                                                    END \n"
                        + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                        + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_WW_4 \n"

                        + "	                     , 0 AS QTY_DD_1 \n"
                        + "	                     , 0 AS QTY_DD_2 \n"
                        + "	                     , 0 AS QTY_DD_3 \n"
                        + "	                     , 0 AS QTY_DD_4 \n"
                        + "	                     , 0 AS QTY_DD_5 \n"
                        + "	                     , 0 AS QTY_DD_6 \n"
                        + "	                     , 0 AS QTY_DD_7 \n"
                        + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                        + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                        + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                        + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                        + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                        + "	                                                      END \n"
                        + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }
                    QRY = QRY
                        + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                        + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                    if (cbMemory.Text.Equals("MEMORY"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                    }
                    if (cbMemory.Text.Equals("S-LSI"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                    }
                    if (ckDetail.Checked == false)
                    {
                        QRY = QRY
                            + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC'  \n"
                            + "	                                           WHEN 'HX' THEN 'Hynix'  \n"
                            + "	                                           ELSE 'ETC'  \n"
                            + "	                          END \n";

                    }
                    else
                    {
                        QRY = QRY
                            + "	          GROUP BY B.MAT_GRP_1 \n";
                    }
                    QRY = QRY
                      
                        + "                 UNION ALL \n"
                        + "	                SELECT 'TOTAL(세부내용)' AS GUBUN  \n"
                        + "	                     , '불합격LOT수' AS CUSTOMER  \n"
                        + "	                     , 0 AS QTY_MM_1  \n"
                        + "	                     , 0 AS QTY_MM_2  \n"
                        + "	                     , 0 AS QTY_MM_3  \n"
                        + "	                     , 0 AS QTY_MM_4  \n"
                        + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                        + "	                                                    END \n"
                        + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                        + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_WW_1 \n"
                        + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                        + "	                                                    END \n"
                        + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                        + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_WW_2 \n"
                        + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                        + "	                                                    END \n"
                        + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                        + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_WW_3 \n"
                        + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                        + "	                                                    END \n"
                        + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                        + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_WW_4 \n"

                        + "	                     , 0 AS QTY_DD_1 \n"
                        + "	                     , 0 AS QTY_DD_2 \n"
                        + "	                     , 0 AS QTY_DD_3 \n"
                        + "	                     , 0 AS QTY_DD_4 \n"
                        + "	                     , 0 AS QTY_DD_5 \n"
                        + "	                     , 0 AS QTY_DD_6 \n"
                        + "	                     , 0 AS QTY_DD_7 \n"



                        + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                        + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                        + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                        + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                        + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                        + "	                                                      END \n"
                        + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }
                    QRY = QRY
                        + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                        + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                        if (cbMemory.Text.Equals("MEMORY"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                        }
                        if (cbMemory.Text.Equals("S-LSI"))
                        {
                            QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                        }
                        QRY = QRY
                            + "	               UNION ALL \n";
                        
                    if (ckDetail.Checked == false)
                    {
                        QRY = QRY
                            + "	          SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC' \n"
                            + "	                                        WHEN 'HX' THEN 'Hynix' \n"
                            + "	                                        ELSE 'ETC' \n"
                            + "	                       END AS GUBUN \n";

                    }
                    else
                    {
                        QRY = QRY
                            + "	          SELECT B.MAT_GRP_1 AS GUBUN \n";
                    }
                    QRY = QRY
                        + "	                     , '검사LOT수' AS CUSTOMER  \n"

                        + "	                     , 0 AS QTY_MM_1  \n"
                        + "	                     , 0 AS QTY_MM_2  \n"
                        + "	                     , 0 AS QTY_MM_3  \n"
                        + "	                     , 0 AS QTY_MM_4  \n"
                        + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                        + "	                                                    END \n"
                        + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                        + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS WW_1 \n"
                        + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                        + "	                                                    END \n"
                        + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                        + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_WW_2 \n"
                        + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                        + "	                                                    END \n"
                        + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                        + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_WW_3 \n"
                        + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                        + "	                                                    END \n"
                        + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                        + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_WW_4 \n"
                        + "	                     , 0 AS QTY_DD_1 \n"
                        + "	                     , 0 AS QTY_DD_2 \n"
                        + "	                     , 0 AS QTY_DD_3 \n"
                        + "	                     , 0 AS QTY_DD_4 \n"
                        + "	                     , 0 AS QTY_DD_5 \n"
                        + "	                     , 0 AS QTY_DD_6 \n"
                        + "	                     , 0 AS QTY_DD_7 \n"

                        + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                        + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                        + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                        + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                        + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                        + "	                                                      END \n"
                        + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                    ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }
                    QRY = QRY
                        + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                        + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                    if (cbMemory.Text.Equals("MEMORY"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                    }
                    if (cbMemory.Text.Equals("S-LSI"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                    }
                    if (ckDetail.Checked == false)
                    {
                        QRY = QRY
                            + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC'  \n"
                            + "	                                           WHEN 'HX' THEN 'Hynix'  \n"
                            + "	                                           ELSE 'ETC'  \n"
                            + "	                          END \n";

                    }
                    else
                    {
                        QRY = QRY
                            + "	          GROUP BY B.MAT_GRP_1 \n";
                    }
                    QRY = QRY
                        
                        + "                 UNION ALL \n"
                        + "	                SELECT 'TOTAL(세부내용)' AS GUBUN  \n"
                        + "	                     , '검사LOT수' AS CUSTOMER  \n"
                        + "	                     , 0 AS QTY_MM_1  \n"
                        + "	                     , 0 AS QTY_MM_2  \n"
                        + "	                     , 0 AS QTY_MM_3  \n"
                        + "	                     , 0 AS QTY_MM_4  \n"
                        + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                        + "	                                                    END \n"
                        + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                        + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_WW_1 \n"
                        + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                        + "	                                                    END \n"
                        + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                        + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_WW_2 \n"
                        + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                        + "	                                                    END \n"
                        + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                        + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_WW_3 \n"
                        + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                        + "	                                                    END \n"
                        + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                        + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                        + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS QTY_WW_4 \n"
                        + "	                     , 0 AS QTY_DD_1 \n"
                        + "	                     , 0 AS QTY_DD_2 \n"
                        + "	                     , 0 AS QTY_DD_3 \n"
                        + "	                     , 0 AS QTY_DD_4 \n"
                        + "	                     , 0 AS QTY_DD_5 \n"
                        + "	                     , 0 AS QTY_DD_6 \n"
                        + "	                     , 0 AS QTY_DD_7 \n"
                        + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                        + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                        + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                        + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                        + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                        + "	                                                      END \n"
                        + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                    ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }
                    QRY = QRY
                        + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                        + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                    if (cbMemory.Text.Equals("MEMORY"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                    }
                    if (cbMemory.Text.Equals("S-LSI"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                    }
                    QRY = QRY
                        + "			UNION ALL \n";
                    if (ckDetail.Checked == false)
                    {
                        QRY = QRY
                            + "	          SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC' \n"
                            + "	                                        WHEN 'HX' THEN 'Hynix' \n"
                            + "	                                        ELSE 'ETC' \n"
                            + "	                       END AS GUBUN \n";

                    }
                    else
                    {
                        QRY = QRY
                            + "	          SELECT B.MAT_GRP_1 AS GUBUN \n";
                    }
                    QRY = QRY
                    + "	                     , '불합격LOT수' AS CUSTOMER  \n"
                    + "	                     , 0 AS QTY_MM_1  \n"
                    + "	                     , 0 AS QTY_MM_2  \n"
                    + "	                     , 0 AS QTY_MM_3  \n"
                    + "	                     , 0 AS QTY_MM_4  \n"
                    + "	                     , 0 AS QTY_WW_1 \n"
                    + "	                     , 0 AS QTY_WW_2 \n"
                    + "	                     , 0 AS QTY_WW_3 \n"
                    + "	                     , 0 AS QTY_WW_4 \n"
                    + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_DD_1  \n"
                    + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_DD_2 \n"
                    + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_DD_3 \n"
                    + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_DD_4 \n"
                    + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_DD_5 \n"
                    + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_DD_6 \n"
                    + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS QTY_DD_7 \n"
                    + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                    + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                    + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                    + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                    + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                    + "	                                      END  \n"
                    + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n";

                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }
                    QRY = QRY
                        + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                        + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                    if (cbMemory.Text.Equals("MEMORY"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                    }
                    if (cbMemory.Text.Equals("S-LSI"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                    }
                    if (ckDetail.Checked == false)
                    {
                        QRY = QRY
                            + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC'  \n"
                            + "	                                           WHEN 'HX' THEN 'Hynix'  \n"
                            + "	                                           ELSE 'ETC'  \n"
                            + "	                          END \n";

                    }
                    else
                    {
                        QRY = QRY
                            + "	          GROUP BY B.MAT_GRP_1 \n";
                    }
                    QRY = QRY
                       
                        + "	                 UNION ALL \n"
                        + "	                SELECT 'TOTAL(세부내용)' AS GUBUN  \n"
                        + "	                     , '불합격LOT수' AS CUSTOMER  \n"
                        + "	                     , 0 AS QTY_MM_1  \n"
                        + "	                     , 0 AS QTY_MM_2  \n"
                        + "	                     , 0 AS QTY_MM_3  \n"
                        + "	                     , 0 AS QTY_MM_4  \n"
                        + "	                     , 0 AS QTY_WW_1 \n"
                        + "	                     , 0 AS QTY_WW_2 \n"
                        + "	                     , 0 AS QTY_WW_3 \n"
                        + "	                     , 0 AS QTY_WW_4 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_1  \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_2 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_3 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_4 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_5 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_6 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_7 \n"
                        + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                        + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                        + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                        + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                        + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                        + "	                                      END  \n"
                        + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                    ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }
                    QRY = QRY
                        + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                        + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                    if (cbMemory.Text.Equals("MEMORY"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                    }
                    if (cbMemory.Text.Equals("S-LSI"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                    }
                    QRY = QRY
                        + "                UNION ALL \n";
                    if (ckDetail.Checked == false)
                    {
                        QRY = QRY
                            + "	          SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC' \n"
                            + "	                                        WHEN 'HX' THEN 'Hynix' \n"
                            + "	                                        ELSE 'ETC' \n"
                            + "	                       END AS GUBUN \n";

                    }
                    else
                    {
                        QRY = QRY
                            + "	          SELECT B.MAT_GRP_1 AS GUBUN \n";
                    }
                    QRY = QRY
                    + "	                     , '검사LOT수' AS CUSTOMER  \n"
                    + "	                     , 0 AS QTY_MM_1  \n"
                    + "	                     , 0 AS QTY_MM_2  \n"
                    + "	                     , 0 AS QTY_MM_3  \n"
                    + "	                     , 0 AS QTY_MM_4  \n"
                    + "	                     , 0 AS QTY_WW_1 \n"
                    + "	                     , 0 AS QTY_WW_2 \n"
                    + "	                     , 0 AS QTY_WW_3 \n"
                    + "	                     , 0 AS QTY_WW_4 \n"
                    + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_1  \n"
                    + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_2 \n"
                    + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_3 \n"
                    + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_4 \n"
                    + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_5 \n"
                    + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_6 \n"
                    + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_7 \n"
                    + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                    + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                    + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                    + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                    + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                    + "	                                      END  \n"
                    + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }
                    QRY = QRY
                        + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                        + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                    if (cbMemory.Text.Equals("MEMORY"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                    }
                    if (cbMemory.Text.Equals("S-LSI"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                    }
                    if (ckDetail.Checked == false)
                    {
                        QRY = QRY
                            + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SEC'  \n"
                            + "	                                           WHEN 'HX' THEN 'Hynix'  \n"
                            + "	                                           ELSE 'ETC'  \n"
                            + "	                          END \n";

                    }
                    else
                    {
                        QRY = QRY
                            + "	          GROUP BY B.MAT_GRP_1 \n";
                    }
                    QRY = QRY
                       
                        + "	                 UNION ALL \n"
                        + "                  SELECT 'TOTAL(세부내용)' AS GUBUN  \n"
                        + "                      , '검사LOT수' AS CUSTOMER  \n"
                        + "	                     , 0 AS QTY_MM_1  \n"
                        + "	                     , 0 AS QTY_MM_2  \n"
                        + "	                     , 0 AS QTY_MM_3  \n"
                        + "	                     , 0 AS QTY_MM_4  \n"
                        + "	                     , 0 AS QTY_WW_1 \n"
                        + "	                     , 0 AS QTY_WW_2 \n"
                        + "	                     , 0 AS QTY_WW_3 \n"
                        + "	                     , 0 AS QTY_WW_4 \n"

                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_1  \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_2 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_3 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_4 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_5 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_6 \n"
                        + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_7 \n"
                        + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                        + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                        + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                        + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                        + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                        + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                        + "	                                      END  \n"
                        + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                    ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }
                    QRY = QRY
                        + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                        + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n";
                    if (cbMemory.Text.Equals("MEMORY"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'MEMORY' \n";

                    }
                    if (cbMemory.Text.Equals("S-LSI"))
                    {
                        QRY = QRY + "                    AND B.MAT_GRP_9 = 'S-LSI' \n";

                    }
                    QRY = QRY
                                
                        
                        + "                    )GROUP BY GUBUN,CUSTOMER \n"


                        + "                    ) \n"
                        + "                    ORDER BY DECODE(GUBUN,'PPM',0,'LRR',1,'TOTAL(세부내용)',2,'SEC',3,'Hynix',4,5) \n"
                        + "                    , DECODE(CUSTOMER,'TOTAL',0,'SE',1,'HX',2,'ETC',3,'검사수',4,'불량수',5,'검사LOT수',6,'불합격LOT수',7,8) \n";



                        /*
                        + "                    ) B \n"
                        + "	         WHERE A.CUSTOMER = B.CUSTOMER) A \n"
                        + "	 GROUP BY A.CUSTOMER  \n"
                    */
                    
























                    }

                    /*
                    //1313
                    else if (rbS02.Checked == true)
                    {
                        // 2010-06-29-정비재 : 공정검사 품질불량현황 고객사별 LRR
                        QRY = "SELECT A.CUSTOMER AS CUSTOMER \n"
                            + "		 , SUM(A.DEFECT_LRR_MM_1) AS DEFECT_LRR_MM_1 \n"
                            + "		 , SUM(A.DEFECT_LRR_MM_2) AS DEFECT_LRR_MM_2 \n"
                            + "		 , SUM(A.DEFECT_LRR_MM_3) AS DEFECT_LRR_MM_3 \n"
                            + "		 , SUM(A.DEFECT_LRR_MM_4) AS DEFECT_LRR_MM_4 \n"
                            + "		 , SUM(A.DEFECT_LRR_WW_1) AS DEFECT_LRR_WW_1 \n"
                            + "		 , SUM(A.DEFECT_LRR_WW_2) AS DEFECT_LRR_WW_2 \n"
                            + "		 , SUM(A.DEFECT_LRR_WW_3) AS DEFECT_LRR_WW_3 \n"
                            + "		 , SUM(A.DEFECT_LRR_WW_4) AS DEFECT_LRR_WW_4 \n"
                            + "		 , SUM(A.DEFECT_LRR_DD_1) AS DEFECT_LRR_DD_1 \n"
                            + "		 , SUM(A.DEFECT_LRR_DD_2) AS DEFECT_LRR_DD_2 \n"
                            + "		 , SUM(A.DEFECT_LRR_DD_3) AS DEFECT_LRR_DD_3 \n"
                            + "		 , SUM(A.DEFECT_LRR_DD_4) AS DEFECT_LRR_DD_4 \n"
                            + "		 , SUM(A.DEFECT_LRR_DD_5) AS DEFECT_LRR_DD_5 \n"
                            + "		 , SUM(A.DEFECT_LRR_DD_6) AS DEFECT_LRR_DD_6 \n"
                            + "		 , SUM(A.DEFECT_LRR_DD_7) AS DEFECT_LRR_DD_7 \n"
                            + "	  FROM (SELECT A.CUSTOMER AS CUSTOMER \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_MM_1 > 0 AND B.SAMPLE_QTY_MM_1 > 0 THEN ROUND((A.DEFECT_QTY_MM_1 * 100) / B.SAMPLE_QTY_MM_1, 2) ELSE 0 END AS DEFECT_LRR_MM_1 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_MM_2 > 0 AND B.SAMPLE_QTY_MM_2 > 0 THEN ROUND((A.DEFECT_QTY_MM_2 * 100) / B.SAMPLE_QTY_MM_2, 2) ELSE 0 END AS DEFECT_LRR_MM_2 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_MM_3 > 0 AND B.SAMPLE_QTY_MM_3 > 0 THEN ROUND((A.DEFECT_QTY_MM_3 * 100) / B.SAMPLE_QTY_MM_3, 2) ELSE 0 END AS DEFECT_LRR_MM_3 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_MM_4 > 0 AND B.SAMPLE_QTY_MM_4 > 0 THEN ROUND((A.DEFECT_QTY_MM_4 * 100) / B.SAMPLE_QTY_MM_4, 2) ELSE 0 END AS DEFECT_LRR_MM_4 \n"
                            + "	             , 0 AS DEFECT_LRR_WW_1 \n"
                            + "	             , 0 AS DEFECT_LRR_WW_2 \n"
                            + "	             , 0 AS DEFECT_LRR_WW_3 \n"
                            + "	             , 0 AS DEFECT_LRR_WW_4 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_1 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_2 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_3 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_4 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_5 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_6 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_7 \n"
                            + "	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                            + "	                                        WHEN 'HX' THEN 'HX' \n"
                            + "	                                        ELSE 'ETC' \n"
                            + "	                       END AS CUSTOMER  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_1  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_2 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_3 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_4 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                            + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                            + "	                                           WHEN 'HX' THEN 'HX'  \n"
                            + "	                                           ELSE 'ETC'  \n"
                            + "	                          END \n"
                        //1212
                            + "	          UNION ALL \n"
                            + "	          SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_1  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_2 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_3 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_4 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                            + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "                ) A \n"
                            + "	             , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                            + "	                                        WHEN 'HX' THEN 'HX' \n"
                            + "	                                        ELSE 'ETC' \n"
                            + "	                       END AS CUSTOMER  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_1  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_2 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_3 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_4 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                            + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                            + "	                                           WHEN 'HX' THEN 'HX'  \n"
                            + "	                                           ELSE 'ETC'  \n"
                            + "	                          END \n"
                        //1212
                            + "	             UNION ALL \n"
                            + "	             SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_1  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_2 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_3 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_4 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                            + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "              ) B \n"
                            + "	         WHERE A.CUSTOMER = B.CUSTOMER \n"
                            + "			UNION ALL \n"
                            + "			SELECT A.CUSTOMER AS CUSTOMER \n"
                            + "	             , 0 AS DEFECT_LRR_MM_1  \n"
                            + "	             , 0 AS DEFECT_LRR_MM_2 \n"
                            + "	             , 0 AS DEFECT_LRR_MM_3 \n"
                            + "	             , 0 AS DEFECT_LRR_MM_4 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_WW_1 > 0 AND B.SAMPLE_QTY_WW_1 > 0 THEN ROUND((A.DEFECT_QTY_WW_1 * 100) / B.SAMPLE_QTY_WW_1, 2) ELSE 0 END AS DEFECT_LRR_WW_1 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_WW_2 > 0 AND B.SAMPLE_QTY_WW_2 > 0 THEN ROUND((A.DEFECT_QTY_WW_2 * 100) / B.SAMPLE_QTY_WW_2, 2) ELSE 0 END AS DEFECT_LRR_WW_2 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_WW_3 > 0 AND B.SAMPLE_QTY_WW_3 > 0 THEN ROUND((A.DEFECT_QTY_WW_3 * 100) / B.SAMPLE_QTY_WW_3, 2) ELSE 0 END AS DEFECT_LRR_WW_3 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_WW_4 > 0 AND B.SAMPLE_QTY_WW_4 > 0 THEN ROUND((A.DEFECT_QTY_WW_4 * 100) / B.SAMPLE_QTY_WW_4, 2) ELSE 0 END AS DEFECT_LRR_WW_4 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_1 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_2 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_3 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_4 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_5 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_6 \n"
                            + "	             , 0 AS DEFECT_LRR_DD_7 \n"
                            + "	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                            + "	                                        WHEN 'HX' THEN 'HX' \n"
                            + "	                                        ELSE 'ETC' \n"
                            + "	                       END AS CUSTOMER  \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_1 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_2 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_3 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_4 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                      END \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                            + "	                                           WHEN 'HX' THEN 'HX'  \n"
                            + "	                                           ELSE 'ETC'  \n"
                            + "	                          END \n"
                        //1212
                            + "	          UNION ALL \n"
                            + "	          SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_1 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_2 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_3 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_4 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                      END \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "                ) A \n"
                            + "	             , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                            + "	                                        WHEN 'HX' THEN 'HX' \n"
                            + "	                                        ELSE 'ETC' \n"
                            + "	                       END AS CUSTOMER  \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_1 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_2 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_3 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_4 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                      END \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                            + "	                                           WHEN 'HX' THEN 'HX'  \n"
                            + "	                                           ELSE 'ETC'  \n"
                            + "	                          END \n"
                            + "               UNION ALL \n"
                        //1212
                            + "	              SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_1 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_2 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_3 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_4 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                      END \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "                 ) B \n"
                            + "	         WHERE A.CUSTOMER = B.CUSTOMER \n"
                            + "			UNION ALL \n"
                            + "			SELECT A.CUSTOMER AS CUSTOMER \n"
                            + "	             , 0 AS DEFECT_LRR_MM_1  \n"
                            + "	             , 0 AS DEFECT_LRR_MM_2 \n"
                            + "	             , 0 AS DEFECT_LRR_MM_3 \n"
                            + "	             , 0 AS DEFECT_LRR_MM_4 \n"
                            + "	             , 0 AS DEFECT_LRR_WW_1 \n"
                            + "	             , 0 AS DEFECT_LRR_WW_2 \n"
                            + "	             , 0 AS DEFECT_LRR_WW_3 \n"
                            + "	             , 0 AS DEFECT_LRR_WW_4 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_1 > 0 AND B.SAMPLE_QTY_DD_1 > 0 THEN ROUND((A.DEFECT_QTY_DD_1 * 100) / B.SAMPLE_QTY_DD_1, 2) ELSE 0 END AS DEFECT_LRR_DD_1 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_2 > 0 AND B.SAMPLE_QTY_DD_2 > 0 THEN ROUND((A.DEFECT_QTY_DD_2 * 100) / B.SAMPLE_QTY_DD_2, 2) ELSE 0 END AS DEFECT_LRR_DD_2 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_3 > 0 AND B.SAMPLE_QTY_DD_3 > 0 THEN ROUND((A.DEFECT_QTY_DD_3 * 100) / B.SAMPLE_QTY_DD_3, 2) ELSE 0 END AS DEFECT_LRR_DD_3 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_4 > 0 AND B.SAMPLE_QTY_DD_4 > 0 THEN ROUND((A.DEFECT_QTY_DD_4 * 100) / B.SAMPLE_QTY_DD_4, 2) ELSE 0 END AS DEFECT_LRR_DD_4 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_5 > 0 AND B.SAMPLE_QTY_DD_5 > 0 THEN ROUND((A.DEFECT_QTY_DD_5 * 100) / B.SAMPLE_QTY_DD_5, 2) ELSE 0 END AS DEFECT_LRR_DD_5 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_6 > 0 AND B.SAMPLE_QTY_DD_6 > 0 THEN ROUND((A.DEFECT_QTY_DD_6 * 100) / B.SAMPLE_QTY_DD_6, 2) ELSE 0 END AS DEFECT_LRR_DD_6 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_7 > 0 AND B.SAMPLE_QTY_DD_7 > 0 THEN ROUND((A.DEFECT_QTY_DD_7 * 100) / B.SAMPLE_QTY_DD_7, 2) ELSE 0 END AS DEFECT_LRR_DD_7 \n"
                            + "	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                            + "	                                        WHEN 'HX' THEN 'HX' \n"
                            + "	                                        ELSE 'ETC' \n"
                            + "	                       END AS CUSTOMER  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_1  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_2 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_3 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_4 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_5 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_6 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                      END  \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                            + "	                                           WHEN 'HX' THEN 'HX'  \n"
                            + "	                                           ELSE 'ETC'  \n"
                            + "	                          END \n"
                        //1212
                            + "	                UNION ALL \n"
                            + "	                SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_1  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_2 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_3 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_4 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_5 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_6 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.DEFECT_LOT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                      END  \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "                ) A \n"
                            + "	             , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                            + "	                                        WHEN 'HX' THEN 'HX' \n"
                            + "	                                        ELSE 'ETC' \n"
                            + "	                       END AS CUSTOMER  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_1  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_2 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_3 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_4 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_5 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_6 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                      END  \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                            + "	                                           WHEN 'HX' THEN 'HX'  \n"
                            + "	                                           ELSE 'ETC'  \n"
                            + "	                          END  \n"
                            //1212
                            + "	                UNION ALL \n"
                            + "	                SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_1  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_2 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_3 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_4 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_5 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_6 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.QC_LOT_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                      END  \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "          ) B \n"
                            + "	         WHERE A.CUSTOMER = B.CUSTOMER) A \n"
                            + "	 GROUP BY A.CUSTOMER  \n"
                            + "	 ORDER BY DECODE(A.CUSTOMER, 'SE', 0, 'HX', 1, 2)";
                    }
                    */
                    else if (rbS03.Checked == true)
                    {
                        // 2010-06-29-정비재 : 공정검사 품질불량현황 고객사별 YIELD
                        QRY = "SELECT 'YIELD' AS GUBUN, A.CUSTOMER AS CUSTOMER \n"
                            + "		 , SUM(A.DEFECT_YIELD_MM_1) AS DEFECT_YIELD_MM_1 \n"
                            + "		 , SUM(A.DEFECT_YIELD_MM_2) AS DEFECT_YIELD_MM_2 \n"
                            + "		 , SUM(A.DEFECT_YIELD_MM_3) AS DEFECT_YIELD_MM_3 \n"
                            + "		 , SUM(A.DEFECT_YIELD_MM_4) AS DEFECT_YIELD_MM_4 \n"
                            + "		 , SUM(A.DEFECT_YIELD_WW_1) AS DEFECT_YIELD_WW_1 \n"
                            + "		 , SUM(A.DEFECT_YIELD_WW_2) AS DEFECT_YIELD_WW_2 \n"
                            + "		 , SUM(A.DEFECT_YIELD_WW_3) AS DEFECT_YIELD_WW_3 \n"
                            + "		 , SUM(A.DEFECT_YIELD_WW_4) AS DEFECT_YIELD_WW_4 \n"
                            + "		 , SUM(A.DEFECT_YIELD_DD_1) AS DEFECT_YIELD_DD_1 \n"
                            + "		 , SUM(A.DEFECT_YIELD_DD_2) AS DEFECT_YIELD_DD_2 \n"
                            + "		 , SUM(A.DEFECT_YIELD_DD_3) AS DEFECT_YIELD_DD_3 \n"
                            + "		 , SUM(A.DEFECT_YIELD_DD_4) AS DEFECT_YIELD_DD_4 \n"
                            + "		 , SUM(A.DEFECT_YIELD_DD_5) AS DEFECT_YIELD_DD_5 \n"
                            + "		 , SUM(A.DEFECT_YIELD_DD_6) AS DEFECT_YIELD_DD_6 \n"
                            + "		 , SUM(A.DEFECT_YIELD_DD_7) AS DEFECT_YIELD_DD_7 \n"
                            + "	  FROM (SELECT A.CUSTOMER AS CUSTOMER \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_MM_1 > 0 AND B.SAMPLE_QTY_MM_1 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_MM_1 * 100) / B.SAMPLE_QTY_MM_1), 2) ELSE 0 END AS DEFECT_YIELD_MM_1  \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_MM_2 > 0 AND B.SAMPLE_QTY_MM_2 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_MM_2 * 100) / B.SAMPLE_QTY_MM_2), 2) ELSE 0 END AS DEFECT_YIELD_MM_2 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_MM_3 > 0 AND B.SAMPLE_QTY_MM_3 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_MM_3 * 100) / B.SAMPLE_QTY_MM_3), 2) ELSE 0 END AS DEFECT_YIELD_MM_3 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_MM_4 > 0 AND B.SAMPLE_QTY_MM_4 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_MM_4 * 100) / B.SAMPLE_QTY_MM_4), 2) ELSE 0 END AS DEFECT_YIELD_MM_4 \n"
                            + "	             , 0 AS DEFECT_YIELD_WW_1 \n"
                            + "	             , 0 AS DEFECT_YIELD_WW_2 \n"
                            + "	             , 0 AS DEFECT_YIELD_WW_3 \n"
                            + "	             , 0 AS DEFECT_YIELD_WW_4 \n"
                            + "	             , 0 AS DEFECT_YIELD_DD_1 \n"
                            + "	             , 0 AS DEFECT_YIELD_DD_2 \n"
                            + "	             , 0 AS DEFECT_YIELD_DD_3 \n"
                            + "	             , 0 AS DEFECT_YIELD_DD_4 \n"
                            + "	             , 0 AS DEFECT_YIELD_DD_5 \n"
                            + "	             , 0 AS DEFECT_YIELD_DD_6 \n"
                            + "	             , 0 AS DEFECT_YIELD_DD_7 \n"
                            + "	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                            + "	                                        WHEN 'HX' THEN 'HX' \n"
                            + "	                                        ELSE 'ETC' \n"
                            + "	                       END AS CUSTOMER  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_1  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_2 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_3 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_4 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                            + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                            + "	                                           WHEN 'HX' THEN 'HX'  \n"
                            + "	                                           ELSE 'ETC'  \n"
                            + "	                          END  \n"
                        //1717
                            + "                 UNION ALL \n"
                            + "                 SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_1  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_2 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_3 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_MM_4 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                            + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "                ) A \n"
                            + "	             , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                            + "	                                        WHEN 'HX' THEN 'HX' \n"
                            + "	                                        ELSE 'ETC' \n"
                            + "	                       END AS CUSTOMER  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_1  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_2 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_3 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_4 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                            + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                            + "	                                           WHEN 'HX' THEN 'HX'  \n"
                            + "	                                           ELSE 'ETC'  \n"
                            + "	                          END \n"
                           //1717
                            + "	             UNION ALL \n"
                            + "	             SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_1  \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -2), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_2 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_3 \n"
                            + "	                     , SUM(CASE A.WORK_MONTH WHEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -0), 'YYYYMM') THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_MM_4 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_MONTH >= TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -3), 'YYYYMM') \n"
                            + "	                   AND A.WORK_MONTH <= '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "          ) B \n"
                            + "	         WHERE A.CUSTOMER = B.CUSTOMER \n"
                            + "			UNION ALL \n"
                            + "			SELECT A.CUSTOMER AS CUSTOMER \n"
                            + "	             , 0 AS DEFECT_YIELD_MM_1  \n"
                            + "	             , 0 AS DEFECT_YIELD_MM_2 \n"
                            + "	             , 0 AS DEFECT_YIELD_MM_3 \n"
                            + "	             , 0 AS DEFECT_YIELD_MM_4 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_WW_1 > 0 AND B.SAMPLE_QTY_WW_1 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_WW_1 * 100) / B.SAMPLE_QTY_WW_1), 2) ELSE 0 END AS DEFECT_YIELD_WW_1 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_WW_2 > 0 AND B.SAMPLE_QTY_WW_2 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_WW_2 * 100) / B.SAMPLE_QTY_WW_2), 2) ELSE 0 END AS DEFECT_YIELD_WW_2 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_WW_3 > 0 AND B.SAMPLE_QTY_WW_3 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_WW_3 * 100) / B.SAMPLE_QTY_WW_3), 2) ELSE 0 END AS DEFECT_YIELD_WW_3 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_WW_4 > 0 AND B.SAMPLE_QTY_WW_4 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_WW_4 * 100) / B.SAMPLE_QTY_WW_4), 2) ELSE 0 END AS DEFECT_YIELD_WW_4 \n"
                            + "	             , 0 AS DEFECT_YIELD_DD_1 \n"
                            + "	             , 0 AS DEFECT_YIELD_DD_2 \n"
                            + "	             , 0 AS DEFECT_YIELD_DD_3 \n"
                            + "	             , 0 AS DEFECT_YIELD_DD_4 \n"
                            + "	             , 0 AS DEFECT_YIELD_DD_5 \n"
                            + "	             , 0 AS DEFECT_YIELD_DD_6 \n"
                            + "	             , 0 AS DEFECT_YIELD_DD_7 \n"
                            + "	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                            + "	                                        WHEN 'HX' THEN 'HX' \n"
                            + "	                                        ELSE 'ETC' \n"
                            + "	                       END AS CUSTOMER  \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_1 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_2 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_3 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_4 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                      END \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                            + "	                                           WHEN 'HX' THEN 'HX'  \n"
                            + "	                                           ELSE 'ETC'  \n"
                            + "	                          END \n"
                            //1717
                            + "	                UNION ALL \n"
                            + "	                SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_1 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_2 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_3 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_WW_4 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                      END \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "                 ) A \n"
                            + "	             , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                            + "	                                        WHEN 'HX' THEN 'HX' \n"
                            + "	                                        ELSE 'ETC' \n"
                            + "	                       END AS CUSTOMER  \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_1 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_2 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_3 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_4 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                      END \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                            + "	                                           WHEN 'HX' THEN 'HX'  \n"
                            + "	                                           ELSE 'ETC'  \n"
                            + "	                          END  \n"
                            //1717
                            + "	                UNION ALL \n"
                            + "	                SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_1 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 15, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 16, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 17, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 18, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 19, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 20, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 14, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_2 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 8, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 9, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 10, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 11, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 12, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 13, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_3 \n"
                            + "	                     , SUM(CASE WHEN A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                                    END \n"
                            + "	                                 AND A.WORK_DATE <= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 6, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 5, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 4, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 3, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 2, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 1, 'YYYYMMDD') \n"
                            + "	                                                                                                         WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') + 0, 'YYYYMMDD') \n"
                            + "	                                                    END THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_WW_4 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 21, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 22, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 23, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 24, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 25, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 26, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 27, 'YYYYMMDD') \n"
                            + "	                                                      END \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "          ) B \n"
                            + "	         WHERE A.CUSTOMER = B.CUSTOMER \n"
                            + "			UNION ALL \n"
                            + "			SELECT A.CUSTOMER AS CUSTOMER \n"
                            + "	             , 0 AS DEFECT_YIELD_MM_1  \n"
                            + "	             , 0 AS DEFECT_YIELD_MM_2 \n"
                            + "	             , 0 AS DEFECT_YIELD_MM_3 \n"
                            + "	             , 0 AS DEFECT_YIELD_MM_4 \n"
                            + "	             , 0 AS DEFECT_YIELD_WW_1 \n"
                            + "	             , 0 AS DEFECT_YIELD_WW_2 \n"
                            + "	             , 0 AS DEFECT_YIELD_WW_3 \n"
                            + "	             , 0 AS DEFECT_YIELD_WW_4 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_1 > 0 AND B.SAMPLE_QTY_DD_1 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_DD_1 * 100) / B.SAMPLE_QTY_DD_1), 2) ELSE 0 END AS DEFECT_YIELD_DD_1 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_2 > 0 AND B.SAMPLE_QTY_DD_2 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_DD_2 * 100) / B.SAMPLE_QTY_DD_2), 2) ELSE 0 END AS DEFECT_YIELD_DD_2 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_3 > 0 AND B.SAMPLE_QTY_DD_3 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_DD_3 * 100) / B.SAMPLE_QTY_DD_3), 2) ELSE 0 END AS DEFECT_YIELD_DD_3 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_4 > 0 AND B.SAMPLE_QTY_DD_4 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_DD_4 * 100) / B.SAMPLE_QTY_DD_4), 2) ELSE 0 END AS DEFECT_YIELD_DD_4 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_5 > 0 AND B.SAMPLE_QTY_DD_5 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_DD_5 * 100) / B.SAMPLE_QTY_DD_5), 2) ELSE 0 END AS DEFECT_YIELD_DD_5 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_6 > 0 AND B.SAMPLE_QTY_DD_6 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_DD_6 * 100) / B.SAMPLE_QTY_DD_6), 2) ELSE 0 END AS DEFECT_YIELD_DD_6 \n"
                            + "	             , CASE WHEN A.DEFECT_QTY_DD_7 > 0 AND B.SAMPLE_QTY_DD_7 > 0 THEN ROUND(100 - ((A.DEFECT_QTY_DD_7 * 100) / B.SAMPLE_QTY_DD_7), 2) ELSE 0 END AS DEFECT_YIELD_DD_7 \n"
                            + "	          FROM (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                            + "	                                        WHEN 'HX' THEN 'HX' \n"
                            + "	                                        ELSE 'ETC' \n"
                            + "	                       END AS CUSTOMER  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_1  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_2 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_3 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_4 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_5 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_6 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                      END  \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                            + "	                                           WHEN 'HX' THEN 'HX'  \n"
                            + "	                                           ELSE 'ETC'  \n"
                            + "	                          END  \n"
                            //1717
                            + "	                UNION ALL \n"
                            + "	                SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_1  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_2 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_3 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_4 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_5 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_6 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCDFT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                      END  \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "                ) A \n"
                            + "	             , (SELECT CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE' \n"
                            + "	                                        WHEN 'HX' THEN 'HX' \n"
                            + "	                                        ELSE 'ETC' \n"
                            + "	                       END AS CUSTOMER  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_1  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_2 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_3 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_4 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_5 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_6 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                      END  \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            + "	                 GROUP BY CASE B.MAT_GRP_1 WHEN 'SE' THEN 'SE'  \n"
                            + "	                                           WHEN 'HX' THEN 'HX'  \n"
                            + "	                                           ELSE 'ETC'  \n"
                            + "	                          END \n"
                            //1717
                            + "	                UNION ALL \n"
                            + "	                SELECT 'TOTAL' AS CUSTOMER  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '금' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_1  \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '토' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_2 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '일' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_3 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '월' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_4 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '화' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_5 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '수' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_6 \n"
                            + "	                     , SUM(CASE TO_CHAR(TO_DATE(A.WORK_DATE, 'YYYYMMDD'), 'DY') WHEN '목' THEN A.SAMPLE_QTY_1 ELSE 0 END) AS SAMPLE_QTY_DD_7 \n"
                            + "	                  FROM RSUMPQCLOT A, MESMGR.MWIPMATDEF@RPTTOMES B  \n"
                            + "	                 WHERE A.FACTORY = B.FACTORY  \n"
                            + "	                   AND A.MAT_ID = B.MAT_ID  \n"
                            + "	                   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                            + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                            + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                            + "	                                      END  \n"
                            + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                        ;
                        // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                        if (cdvQCOper.Text == "QC GATE")
                        {
                            QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                        }
                        else
                        {
                            QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                        }
                        QRY = QRY
                            + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                            + "                    AND B.MAT_GRP_1 " + (udcWIPCondition1.Text == "ALL" ? "LIKE '%'" : udcWIPCondition1.SelectedValueToQueryString) + "\n"
                            +"           ) B \n"
                            + "	         WHERE A.CUSTOMER = B.CUSTOMER) A \n"
                            + "	 GROUP BY A.CUSTOMER  \n"
                            + "	 ORDER BY DECODE(A.CUSTOMER, 'SE', 0, 'HX', 1, 2)";
                    }
                }
                else if (rbTrand02.Checked == true)
                {
                    // 2010-06-29-정비재 : 공정검사 품질불량현황 주간별 데이터를 조회한다.
                    QRY = "SELECT A.DEFECT_CODE_MM AS DEFECT_CODE_MM \n"
                        + "		 , A.DEFECT_QTY_MM AS DEFECT_QTY_MM \n"
                        + "		 , A.SAMPLE_QTY_MM AS SAMPLE_QTY_MM \n"
                        + "		 , NVL(B.DEFECT_CODE_WW, ' ') AS DEFECT_CODE_WW \n"
                        + "		 , NVL(B.DEFECT_QTY_WW, 0) AS DEFECT_QTY_WW \n"
                        + "		 , NVL(B.SAMPLE_QTY_WW, 0) AS SAMPLE_QTY_WW \n"
                        + "		 , NVL(C.DEFECT_CODE_DD, ' ') AS DEFECT_CODE_DD \n"
                        + "		 , NVL(C.DEFECT_QTY_DD, 0) AS DEFECT_QTY_DD \n"
                        + "		 , NVL(C.SAMPLE_QTY_DD, 0) AS SAMPLE_QTY_DD \n"
                        + "	  FROM (SELECT A.ROW_LEVEL AS ROW_LEVEL \n"
                        + "	             , A.DEFECT_CODE AS DEFECT_CODE_MM \n"
                        + "	             , A.DEFECT_QTY_1 AS DEFECT_QTY_MM \n"
                        + "	             , B.SAMPLE_QTY_1 AS SAMPLE_QTY_MM \n"
                        + "	          FROM (SELECT A.DEFECT_CODE AS DEFECT_CODE \n"
                        + "	                     , SUM(A.DEFECT_LOT_QTY_1) AS DEFECT_QTY_1 \n"
                        + "	                     , ROW_NUMBER() OVER(ORDER BY SUM(A.DEFECT_LOT_QTY_1) DESC) AS ROW_LEVEL \n"
                        + "	                  FROM RSUMPQCDFT A \n"
                        + "	                 WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
                        + "	                   AND A.WORK_MONTH = '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                    ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }
                    QRY = QRY
                    + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                    + "	                 GROUP BY A.DEFECT_CODE) A \n"
                    + "	             , (SELECT SUM(A.QC_LOT_QTY_1) AS SAMPLE_QTY_1 \n"
                    + "	                  FROM RSUMPQCLOT A \n"
                    + "	                 WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
                    + "	                   AND A.WORK_MONTH = '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }
                    QRY = QRY
                    + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%') B \n"
                    + "	         WHERE A.ROW_LEVEL <= 5) A \n"
                    + "		 , (SELECT A.ROW_LEVEL AS ROW_LEVEL \n"
                    + "	             , A.DEFECT_CODE AS DEFECT_CODE_WW \n"
                    + "	             , A.DEFECT_QTY_1 AS DEFECT_QTY_WW \n"
                    + "	             , B.SAMPLE_QTY_1 AS SAMPLE_QTY_WW \n"
                    + "	          FROM (SELECT A.DEFECT_CODE AS DEFECT_CODE \n"
                    + "	                     , SUM(A.DEFECT_LOT_QTY_1) AS DEFECT_QTY_1 \n"
                    + "	                     , ROW_NUMBER() OVER(ORDER BY SUM(A.DEFECT_LOT_QTY_1) DESC) AS ROW_LEVEL \n"
                    + "	                  FROM RSUMPQCDFT A \n"
                    + "	                 WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
                    + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                    + "	                                      END   \n"
                    + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }
                    QRY = QRY
                    + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                    + "	                 GROUP BY A.DEFECT_CODE) A \n"
                    + "	             , (SELECT SUM(A.QC_LOT_QTY_1) AS SAMPLE_QTY_1 \n"
                    + "	                  FROM RSUMPQCLOT A \n"
                    + "	                 WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
                    + "	                   AND A.WORK_DATE >= CASE TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), 'DY') WHEN '금' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 0, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '토' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '일' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '월' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '화' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '수' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD') \n"
                    + "	                                                                                           WHEN '목' THEN TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD') \n"
                    + "	                                      END   \n"
                    + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }
                    QRY = QRY
                    + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%') B \n"
                    + "	         WHERE A.ROW_LEVEL <= 5) B \n"
                    + "		 , (SELECT A.ROW_LEVEL AS ROW_LEVEL  \n"
                    + "	             , A.DEFECT_CODE AS DEFECT_CODE_DD \n"
                    + "	             , A.DEFECT_QTY_1 AS DEFECT_QTY_DD \n"
                    + "	             , B.SAMPLE_QTY_1 AS SAMPLE_QTY_DD \n"
                    + "	          FROM (SELECT A.DEFECT_CODE AS DEFECT_CODE \n"
                    + "	                     , SUM(A.DEFECT_LOT_QTY_1) AS DEFECT_QTY_1 \n"
                    + "	                     , ROW_NUMBER() OVER(ORDER BY SUM(A.DEFECT_LOT_QTY_1) DESC) AS ROW_LEVEL \n"
                    + "	                  FROM RSUMPQCDFT A \n"
                    + "	                 WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
                    + "	                   AND A.WORK_DATE >= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                    + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }
                    QRY = QRY
                    + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                    + "	                 GROUP BY A.DEFECT_CODE) A \n"
                    + "	             , (SELECT SUM(A.QC_LOT_QTY_1) AS SAMPLE_QTY_1 \n"
                    + "	                  FROM RSUMPQCLOT A \n"
                    + "	                 WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
                    + "	                   AND A.WORK_DATE >= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                    + "	                   AND A.WORK_DATE <= '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }
                    QRY = QRY
                    + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%') B \n"
                    + "	         WHERE A.ROW_LEVEL <= 5) C \n"
                    + "	 WHERE A.ROW_LEVEL = B.ROW_LEVEL(+) \n"
                    + "	   AND A.ROW_LEVEL = C.ROW_LEVEL(+) \n"
                    + "	 ORDER BY A.ROW_LEVEL ASC";
                }
                else if (rbTrand03.Checked == true)
                {
                    // 2010-06-29-정비재 : 공정검사 품질불량현황 일자별 데이터를 조회한다.
                    QRY = "SELECT B.DEFECT_CODE_PREV AS DEFECT_CODE_PREV  \n"
                        + "		 , B.DEFECT_QTY_PREV AS DEFECT_QTY_PREV  \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '01' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_01 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '02' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_02 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '03' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_03 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '04' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_04 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '05' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_05 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '06' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_06 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '07' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_07 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '08' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_08 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '09' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_09 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '10' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_10 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '11' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_11 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '12' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_12 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '13' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_13 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '14' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_14 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '15' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_15 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '16' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_16 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '17' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_17 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '18' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_18 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '19' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_19 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '20' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_20 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '21' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_21 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '22' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_22 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '23' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_23 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '24' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_24 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '25' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_25 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '26' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_26 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '27' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_27 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '28' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_28 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '29' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_29 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '30' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_30 \n"
                        + "		 , SUM(CASE SUBSTR(A.WORK_DATE, -2) WHEN '31' THEN A.DEFECT_QTY_1 ELSE 0 END) AS DEFECT_QTY_31 \n"
                        + "	  FROM RSUMPQCDFT A \n"
                        + "		 , (SELECT A.DEFECT_CODE AS DEFECT_CODE_PREV \n"
                        + "	             , A.DEFECT_QTY_1 AS DEFECT_QTY_PREV \n"
                        + "	             , A.ROW_LEVEL AS ROW_LEVEL_PREV \n"
                        + "	          FROM (SELECT A.DEFECT_CODE AS DEFECT_CODE  \n"
                        + "	                     , SUM(A.DEFECT_QTY_1) AS DEFECT_QTY_1 \n"
                        + "	                     , ROW_NUMBER() OVER(ORDER BY SUM(A.DEFECT_QTY_1) DESC) AS ROW_LEVEL  \n"
                        + "	                  FROM RSUMPQCDFT A  \n"
                        + "	                 WHERE A.FACTORY = '" + cdvFactory.Text + "' \n"
                        + "	                   AND A.WORK_MONTH = TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMM") + "', 'YYYYMM'), -1), 'YYYYMM') \n"
                    ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }
                    QRY = QRY
                    + "	\n                   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                    + "	                 GROUP BY A.DEFECT_CODE) A \n"
                    + "	         WHERE A.ROW_LEVEL <= 5) B \n"
                    + "	 WHERE A.DEFECT_CODE = B.DEFECT_CODE_PREV \n"
                    + "	   AND A.FACTORY = '" + cdvFactory.Text + "' \n"
                    + "	   AND A.WORK_MONTH = '" + cdvBaseDate.Value.ToString("yyyyMM") + "' \n"
                ;
                    // 2011-02-16-김민우 PVI 공정이 나눠지면서 QC_OPER가 PVI에서 QC GATE로 변경
                    if (cdvQCOper.Text == "QC GATE")
                    {
                        QRY = QRY + "                     AND A.QC_OPER IN ('PVI','QC GATE')";

                    }
                    else
                    {
                        QRY = QRY + "                     AND A.QC_OPER LIKE '" + (cdvQCOper.Text == "ALL" ? "%" : cdvQCOper.Text) + "' || '%' ";
                    }
                    QRY = QRY
                    + "	\n   AND A.QC_TYPE LIKE '" + (cdvQCType.Text == "ALL" ? "%" : cdvQCType.Text) + "' || '%'  \n"
                    + "	 GROUP BY B.DEFECT_CODE_PREV, B.DEFECT_QTY_PREV, B.ROW_LEVEL_PREV \n"
                    + "	 ORDER BY B.ROW_LEVEL_PREV ASC";
                }

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

                if (rbTrand03.Checked == true)
                {
                    SS = SS02;
                }
                else
                {
                    SS = SS01;
                }
                SS.DataSource = dt;
                SS.RPT_AutoFit(false);
                if (ckDetail.Checked == false)
                {
                    fnMakeChart(SS, 0);
                }
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
             * Created By : bee-jae jung(2010-06-01-화요일)
             * 
             * Modified By : bee-jae jung(2010-06-01-화요일)
             ****************************************************/
            int[] ich_mm = new int[4]; int[] icols_mm = new int[4]; int[] irows_mm = new int[4]; string[] stitle_mm = new string[4];
            int[] ich_ww = new int[4]; int[] icols_ww = new int[4]; int[] irows_ww = new int[4]; string[] stitle_ww = new string[4];
            int[] ich_dd = new int[7]; int[] icols_dd = new int[7]; int[] irows_dd = new int[4]; string[] stitle_dd = new string[4];
            int[] ich_md = new int[31]; int[] icols_md = new int[31]; int[] irows_md = new int[5]; string[] stitle_md = new string[5];
            int icol = 0, irow = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // 2010-06-01-정비재 : SS에 데이터가 없으면 종료한다.
                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                cf01.RPT_1_ChartInit(); cf02.RPT_1_ChartInit(); cf03.RPT_1_ChartInit(); cf04.RPT_1_ChartInit();
                cf01.RPT_2_ClearData(); cf02.RPT_2_ClearData(); cf03.RPT_2_ClearData(); cf04.RPT_2_ClearData();

                if (rbTrand01.Checked == true)
                {
                    // 2010-06-29-정비재 : 공정검사 불량현황 고객사별 현황
                    // 월별 Chart
                    cf01.AxisX.Title.Text = "Monthly Status";
                    if (iselrow == 0)
                    {
                        cf01.AxisY.Title.Text = "PPM";
                    }
                    else if (iselrow == 4)
                    {
                        cf01.AxisY.Title.Text = "LRR";
                    }
                    else
                    {
                        cf01.AxisY.Title.Text = "PPM";
                    }
                    
                    if (rbS03.Checked == true)
                    {
                        cf01.AxisY.Title.Text = "YIELD";
                    }
                    
                    cf01.Gallery = SoftwareFX.ChartFX.Gallery.Curve;
                    cf01.AxisY.DataFormat.Format = 0;
                    cf01.AxisY.DataFormat.Decimals = 2;
                    cf01.AxisX.Staggered = false;
                    cf01.PointLabels = true;
                    for (icol = 0; icol < ich_mm.Length; icol++)
                    {
                        ich_mm[icol] = icol + 2;
                        icols_mm[icol] = icol + 2;
                    }
                    for (irow = 0; irow < 4; irow++)
                    {
                        irows_mm[irow] = iselrow + irow;
                        stitle_mm[irow] = SS.Sheets[0].Cells[iselrow + irow, 1].Text;
                    }
                    cf01.RPT_3_OpenData(irows_mm.Length, icols_mm.Length);
                    cf01.RPT_4_AddData(SS, irows_mm, icols_mm, SeriseType.Rows);
                    cf01.RPT_5_CloseData();
                    cf01.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, 2);
                    cf01.RPT_8_SetSeriseLegend(stitle_mm, SoftwareFX.ChartFX.Docked.Top);
                   
                    
                    // 주별 Chart
                    cf02.AxisX.Title.Text = "Weekly Status";
                    if (iselrow == 0)
                    {
                        cf02.AxisY.Title.Text = "PPM";
                    }
                    else if (iselrow == 4)
                    {
                        cf02.AxisY.Title.Text = "LRR";
                    }
                    else
                    {
                        cf02.AxisY.Title.Text = "PPM";
                    }

                    if (rbS03.Checked == true)
                    {
                        cf02.AxisY.Title.Text = "YIELD";
                    }
                    cf02.Gallery = SoftwareFX.ChartFX.Gallery.Curve;
                    cf02.AxisY.DataFormat.Format = 0;
                    cf02.AxisY.DataFormat.Decimals = 2;
                    cf02.AxisX.Staggered = false;
                    cf02.PointLabels = true;
                    for (icol = 0; icol < ich_ww.Length; icol++)
                    {
                        ich_ww[icol] = icol + 6;
                        icols_ww[icol] = icol + 6;
                    }
                    for (irow = 0; irow < 4; irow++)
                    {
                        irows_ww[irow] = iselrow + irow;
                        stitle_ww[irow] = SS.Sheets[0].Cells[iselrow + irow, 1].Text;
                    }
                    cf02.RPT_3_OpenData(irows_ww.Length, icols_ww.Length);
                    cf02.RPT_4_AddData(SS, irows_ww, icols_ww, SeriseType.Rows);
                    cf02.RPT_5_CloseData();
                    cf02.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, 6);
                    cf02.RPT_8_SetSeriseLegend(stitle_ww, SoftwareFX.ChartFX.Docked.Top);

                    // 일별 Chart
                    cf03.AxisX.Title.Text = "Daily Status";
                    if (iselrow == 0)
                    {
                        cf03.AxisY.Title.Text = "PPM";
                    }
                    else if (iselrow == 4)
                    {
                        cf03.AxisY.Title.Text = "LRR";
                    }
                    else
                    {
                        cf03.AxisY.Title.Text = "PPM";
                    }

                    if (rbS03.Checked == true)
                    {
                        cf03.AxisY.Title.Text = "YIELD";
                    }
                    cf03.Gallery = SoftwareFX.ChartFX.Gallery.Curve;
                    cf03.AxisY.DataFormat.Format = 0;
                    cf03.AxisY.DataFormat.Decimals = 2;
                    cf03.AxisX.Staggered = false;
                    cf03.PointLabels = true;
                    for (icol = 0; icol < ich_dd.Length; icol++)
                    {
                        ich_dd[icol] = icol + 10;
                        icols_dd[icol] = icol + 10;
                    }
                    for (irow = 0; irow < 4; irow++)
                    {
                        irows_dd[irow] = iselrow + irow;
                        stitle_dd[irow] = SS.Sheets[0].Cells[iselrow + irow, 1].Text;
                    }
                    cf03.RPT_3_OpenData(irows_dd.Length, icols_dd.Length);
                    cf03.RPT_4_AddData(SS, irows_dd, icols_dd, SeriseType.Rows);
                    cf03.RPT_5_CloseData();
                    cf03.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, 10);
                    cf03.RPT_8_SetSeriseLegend(stitle_dd, SoftwareFX.ChartFX.Docked.Top);
                   
                }

                else if (rbTrand02.Checked == true)
                {
                    // 2010-06-29-정비재 : 공정검사 불량현황 주간별 현황
                    cf01.Titles[0].Text = "Bad Top 5 this month";
                    cf01.Gallery = SoftwareFX.ChartFX.Gallery.Pie;
                    cf01.Chart3D = false;
                    ((SoftwareFX.ChartFX.GalleryObj.Pie)cf01.GalleryObj).LabelsInside = true;
                    cf01.OpenData(SoftwareFX.ChartFX.COD.Values, 1, 1);
                    for (irow = 0; irow < SS.Sheets[0].RowCount; irow++)
                    {
                        cf01.AxisX.Label[irow] = SS.Sheets[0].Cells[irow, 0].Text;
                        cf01.Value[0, irow] = Convert.ToDouble(SS.Sheets[0].Cells[irow, 1].Value);
                    }
                    cf01.CloseData(SoftwareFX.ChartFX.COD.Values);
                    cf01.PointLabelMask = "%v";             // 2010-07-19-정비재 : 불량숫자
                    //cf01.PointLabelMask = "%l:%v";			// 2010-06-04-정비재 : 불량코드에 불량숫자
                    //cf01.PointLabelMask = "%l:%p";		// 2010-06-04-정비재 : 불량코드에 불량 %
                    cf01.PointLabels = true;


                    // 2010-06-03-정비재 : 주간별 불량 TOP 5 - TOP 5 불량발생 현황(주간) Chart를 Display한다.
                    cf02.Titles[0].Text = "This week's bad TOP 5";
                    cf02.Gallery = SoftwareFX.ChartFX.Gallery.Pie;
                    cf02.Chart3D = false;
                    ((SoftwareFX.ChartFX.GalleryObj.Pie)cf02.GalleryObj).LabelsInside = true;
                    cf02.OpenData(SoftwareFX.ChartFX.COD.Values, 1, 1);
                    for (irow = 0; irow < SS.Sheets[0].RowCount; irow++)
                    {
                        cf02.AxisX.Label[irow] = SS.Sheets[0].Cells[irow, 3].Text;
                        cf02.Value[0, irow] = Convert.ToDouble(SS.Sheets[0].Cells[irow, 4].Value);
                    }
                    cf02.CloseData(SoftwareFX.ChartFX.COD.Values);
                    cf02.PointLabelMask = "%v";             // 2010-07-19-정비재 : 불량숫자
                    //cf02.PointLabelMask = "%l:%v";			// 2010-06-04-정비재 : 불량코드에 불량숫자
                    //cf02.PointLabelMask = "%l:%p";		// 2010-06-04-정비재 : 불량코드에 불량 %
                    cf02.PointLabels = true;


                    // 2010-06-03-정비재 : 주간별 불량 TOP 5 - TOP 5 불량발생 현황(일간) Chart를 Display한다.
                    cf03.Titles[0].Text = "Today's bad TOP 5";
                    cf03.Gallery = SoftwareFX.ChartFX.Gallery.Pie;
                    cf03.Chart3D = false;
                    ((SoftwareFX.ChartFX.GalleryObj.Pie)cf03.GalleryObj).LabelsInside = true;
                    cf03.OpenData(SoftwareFX.ChartFX.COD.Values, 1, 1);
                    for (irow = 0; irow < SS.Sheets[0].RowCount; irow++)
                    {
                        cf03.AxisX.Label[irow] = SS.Sheets[0].Cells[irow, 6].Text;
                        cf03.Value[0, irow] = Convert.ToDouble(SS.Sheets[0].Cells[irow, 7].Value);
                    }
                    cf03.CloseData(SoftwareFX.ChartFX.COD.Values);
                    cf03.PointLabelMask = "%v";             // 2010-07-19-정비재 : 불량숫자
                    //cf03.PointLabelMask = "%l:%v";			// 2010-06-04-정비재 : 불량코드에 불량숫자
                    //cf03.PointLabelMask = "%l:%p";		// 2010-06-04-정비재 : 불량코드에 불량 %
                    cf03.PointLabels = true;
                }
                else if (rbTrand03.Checked == true)
                {
                    // 2010-06-29-정비재 : 공정검사 불량현황 일자별 현황
                    cf04.AxisX.Title.Text = "Defective status by date (based on TOP 5 monthly defects)";
                    cf04.Gallery = SoftwareFX.ChartFX.Gallery.Curve;
                    cf04.AxisY.DataFormat.Format = 0;
                    cf04.AxisY.DataFormat.Decimals = 0;
                    cf04.PointLabels = true;
                    for (icol = 0; icol < ich_md.Length; icol++)
                    {
                        ich_md[icol] = icol + 2;
                        icols_md[icol] = icol + 2;
                    }
                    for (irow = 0; irow < SS.Sheets[0].RowCount; irow++)
                    {
                        irows_md[irow] = irow;
                        stitle_md[irow] = SS.Sheets[0].Cells[irow, 0].Text;
                    }
                    cf04.RPT_3_OpenData(irows_md.Length, icols_md.Length);
                    cf04.RPT_4_AddData(SS, irows_md, icols_md, SeriseType.Rows);
                    cf04.RPT_5_CloseData();
                    cf04.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, 2);
                    cf04.RPT_8_SetSeriseLegend(stitle_md, SoftwareFX.ChartFX.Docked.Top);
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

        #endregion


        #region " Form Event "

        private void rbTrand_CheckedChanged(object sender, EventArgs e)
        {
            /****************************************************
             * Comment : Trand RadioButton을 선택하면
             * 
             * Created By : bee-jae jung(2010-06-02-수요일)
             * 
             * Modified By : bee-jae jung(2010-06-02-수요일)
             ****************************************************/
            RadioButton rb;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                rb = (RadioButton)sender;

                if (rb.Checked == true)
                {
                    cf01.RPT_1_ChartInit(); cf02.RPT_1_ChartInit(); cf03.RPT_1_ChartInit();
                    cf01.RPT_2_ClearData(); cf02.RPT_2_ClearData(); cf03.RPT_2_ClearData();

                    SS01.Sheets[0].RowCount = 0;
                    SS02.Sheets[0].RowCount = 0;
                    switch (rb.Name.ToUpper())
                    {
                        case "RBTRAND01":		// 2010-06-02-정비재 : 고객사별
                            gbSearch.Visible = true;
                            scLeft.Visible = true;
                            SS01.Visible = true;

                            //lblBaseDate.Visible = false;
                            //cdvBaseDate.Visible = false;
                            //cdvCustomer.Visible = true;

                            cf04.Visible = false;
                            SS02.Visible = false;
                            break;

                        case "RBTRAND02":		// 2010-06-02-정비재 : 주간별
                            gbSearch.Visible = false;
                            scLeft.Visible = true;
                            SS01.Visible = true;

                            lblBaseDate.Visible = true;
                            cdvBaseDate.Visible = true;
                            cdvCustomer.Visible = false;

                            cf04.Visible = false;
                            SS02.Visible = false;
                            break;

                        case "RBTRAND03":		// 2010-06-02-정비재 : 일자별
                            gbSearch.Visible = false;
                            scLeft.Visible = false;
                            SS01.Visible = false;

                            lblBaseDate.Visible = true;
                            cdvBaseDate.Visible = true;
                            cdvCustomer.Visible = false;

                            cf04.Visible = true;
                            SS02.Visible = true;
                            break;
                    }
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

        private void btnView_Click(object sender, EventArgs e)
        {
            /****************************************************
             * Comment : View Button을 클릭하면
             * 
             * Created By : bee-jae jung(2010-05-11-화요일)
             * 
             * Modified By : bee-jae jung(2010-05-11-화요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                cf01.RPT_1_ChartInit(); cf02.RPT_1_ChartInit(); cf03.RPT_1_ChartInit();
                cf01.RPT_2_ClearData(); cf02.RPT_2_ClearData(); cf03.RPT_2_ClearData();

                String aa = udcWIPCondition1.SelectedValueToQueryString;

                if (rbTrand01.Checked == true || rbTrand02.Checked == true)
                {
                    // 2010-06-03-정비재 : 고객사별/주간별 불량발생 현황
                    fnSSInitial(SS01);
                }
                else if (rbTrand03.Checked == true)
                {
                    // 2010-06-03-정비재 : 일자별 불량발생 현황
                    fnSSInitial(SS02);
                }


                fnDataFind();
                //// 2010-08-11-정비재 : Test 용입니다.
                //if (GlobalVariable.gsUserID == "ADMIN" || GlobalVariable.gsUserID == "WEBADMIN")
                //{
                //    // 2010-08-11-정비재 : Oracle Procedure/Package 호출 Test중
                //    fnDataFind2();
                //}
                //else
                //{
                //    fnDataFind();
                //}
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
             * Created By : bee-jae jung(2010-05-11-화요일)
             * 
             * Modified By : bee-jae jung(2010-05-11-화요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (rbTrand01.Checked == true || rbTrand02.Checked == true)
                {
                    SS01.ExportExcel();
                }
                else if (rbTrand03.Checked == true)
                {
                    SS02.ExportExcel();
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

        private void SS02_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            /****************************************************
             * Comment : SS의 Cell을 선택하면
             * 
             * Created By : bee-jae jung(2010-06-05-토요일)
             * 
             * Modified By : bee-jae jung(2010-06-05-토요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (rbTrand03.Checked == true)
                {
                    fnMakeChart(SS02, e.Row);
                }
                else
                {
                    fnMakeChart(SS01, 0);
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

        #endregion

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            /****************************************************
			 * Comment : SS의 Cell을 선택하면
			 * 
			 * Created By : KIM MINWOO(2011-03-05-토요일)
			 * 
			 * Modified By : KIM MINWOO(2011-03-05-토요일)
			 ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (ckDetail.Checked == false)
                {
                    if (e.Row >= 1)
                    {
                        if (e.Row <= 3)
                            fnMakeChart(SS01, 0);
                        if (e.Row > 3 && e.Row <= 7)
                            fnMakeChart(SS01, 4);

                    }
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

        private void ckDetail_CheckedChanged(object sender, EventArgs e)
        {
            if (ckDetail.Checked == true)
            {
                btnDetail.Visible = true;
            }
            else
            {
                btnDetail.Visible = false;

            }
 
        }


    }
}
