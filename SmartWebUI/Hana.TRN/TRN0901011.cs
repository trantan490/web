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
    public partial class TRN0901011 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
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
        public TRN0901011()
        {
            InitializeComponent();
            SortInit();
            cdvDate.Value = DateTime.Now.AddDays(-1);
            GridColumnInit();
            this.SetFactory("HMKA1");
            cdvFactory.Text = "HMKA1";
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





                /*
                  if (cdvFactory.Text == "HMKA1")
            
                  {
                strSqlString.Append("SELECT MAX(CUSTOMER) AS CUSTOMER, DECODE(SUM(TARGET_MON), 0, 0, ROUND((SUM(ASSY_MON)/SUM(TARGET_MON))*100, 2)) JINDO" + "\n");
                strSqlString.Append(", DECODE(SUM(TARGET_MON_1), 0, 0, ROUND((SUM(ASSY_MON_1)/SUM(TARGET_MON_1))*100, 2)) JINDO_1" + "\n");
                strSqlString.Append(", DECODE(SUM(TARGET_MON_2), 0, 0, ROUND((SUM(ASSY_MON_2)/SUM(TARGET_MON_2))*100, 2)) JINDO_2" + "\n");
                strSqlString.Append(", DECODE(SUM(TARGET_MON_3), 0, 0, ROUND((SUM(ASSY_MON_3)/SUM(TARGET_MON_3))*100, 2)) JINDO_3" + "\n");
                strSqlString.Append(", DECODE(SUM(TARGET_MON_4), 0, 0, ROUND((SUM(ASSY_MON_4)/SUM(TARGET_MON_4))*100, 2)) JINDO_4" + "\n");
                strSqlString.Append(", DECODE(SUM(TARGET_MON_5), 0, 0, ROUND((SUM(ASSY_MON_5)/SUM(TARGET_MON_5))*100, 2)) JINDO_5" + "\n");
                strSqlString.Append(", DECODE(SUM(TARGET_MON_6), 0, 0, ROUND((SUM(ASSY_MON_6)/SUM(TARGET_MON_6))*100, 2)) JINDO_6" + "\n");
                strSqlString.Append(", DECODE(SUM(TARGET_MON_7), 0, 0, ROUND((SUM(ASSY_MON_7)/SUM(TARGET_MON_7))*100, 2)) JINDO_7" + "\n");

                strSqlString.Append(" FROM( " + "\n");
                strSqlString.Append("SELECT A.MON_PLAN , A.TARGET_MON, A.TARGET_MON_1, A.ASSY_MON, A.ASSY_MON_1, A.CUSTOMER" + "\n");
                strSqlString.Append(", A.TARGET_MON_2, A.ASSY_MON_2, A.TARGET_MON_3, A.ASSY_MON_3, A.TARGET_MON_4, A.ASSY_MON_4");
                strSqlString.Append(", A.TARGET_MON_5, A.ASSY_MON_5, A.TARGET_MON_6, A.ASSY_MON_6, A.TARGET_MON_7, A.ASSY_MON_7");
                strSqlString.Append(" FROM( " + "\n");
                strSqlString.Append("             SELECT G.MAT_GRP_3 AS PKG  " + "\n");
                strSqlString.Append("             , SUM(NVL(A.MON_PLAN,0)) AS MON_PLAN " + "\n");
                strSqlString.Append("             , ROUND(((SUM(NVL(A.MON_PLAN,0)) * " + jindoPer + ") / 100),0) AS TARGET_MON " + "\n");
                strSqlString.Append("             , ROUND(((SUM(NVL(A.MON_PLAN_1,0)) * " + jindoPer1 + ") / 100),0) AS TARGET_MON_1 " + "\n");
                strSqlString.Append("             , ROUND(((SUM(NVL(A.MON_PLAN_2,0)) * " + jindoPer2 + ") / 100),0) AS TARGET_MON_2 " + "\n");
                strSqlString.Append("             , ROUND(((SUM(NVL(A.MON_PLAN_3,0)) * " + jindoPer3 + ") / 100),0) AS TARGET_MON_3 " + "\n");
                strSqlString.Append("             , ROUND(((SUM(NVL(A.MON_PLAN_4,0)) * " + jindoPer4 + ") / 100),0) AS TARGET_MON_4 " + "\n");
                strSqlString.Append("             , ROUND(((SUM(NVL(A.MON_PLAN_5,0)) * " + jindoPer5 + ") / 100),0) AS TARGET_MON_5 " + "\n");
                strSqlString.Append("             , ROUND(((SUM(NVL(A.MON_PLAN_6,0)) * " + jindoPer6 + ") / 100),0) AS TARGET_MON_6 " + "\n");
                strSqlString.Append("             , ROUND(((SUM(NVL(A.MON_PLAN_7,0)) * " + jindoPer7 + ") / 100),0) AS TARGET_MON_7 " + "\n");
                strSqlString.Append("             , SUM(NVL(A.ASSY_MON,0)) AS ASSY_MON " + "\n");
                strSqlString.Append("             , SUM(NVL(A.ASSY_MON_1,0)) AS ASSY_MON_1 " + "\n");
                strSqlString.Append("             , SUM(NVL(A.ASSY_MON_2,0)) AS ASSY_MON_2 " + "\n");
                strSqlString.Append("             , SUM(NVL(A.ASSY_MON_3,0)) AS ASSY_MON_3 " + "\n");
                strSqlString.Append("             , SUM(NVL(A.ASSY_MON_4,0)) AS ASSY_MON_4 " + "\n");
                strSqlString.Append("             , SUM(NVL(A.ASSY_MON_5,0)) AS ASSY_MON_5 " + "\n");
                strSqlString.Append("             , SUM(NVL(A.ASSY_MON_6,0)) AS ASSY_MON_6 " + "\n");
                strSqlString.Append("             , SUM(NVL(A.ASSY_MON_7,0)) AS ASSY_MON_7 " + "\n");
                strSqlString.Append("             , G.MAT_GRP_1 AS CUSTOMER " + "\n");
                strSqlString.Append("               FROM( " + "\n");
                strSqlString.Append("       SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7 " + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN.PLAN_QTY_ASSY)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(PLAN.PLAN_QTY_ASSY)) AS MON_PLAN" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN.PLAN_QTY_ASSY)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(PLAN_1.PLAN_QTY_ASSY)) AS MON_PLAN_1" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN.PLAN_QTY_ASSY)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(PLAN_2.PLAN_QTY_ASSY)) AS MON_PLAN_2" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN.PLAN_QTY_ASSY)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(PLAN_3.PLAN_QTY_ASSY)) AS MON_PLAN_3" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN.PLAN_QTY_ASSY)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(PLAN_4.PLAN_QTY_ASSY)) AS MON_PLAN_4" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN.PLAN_QTY_ASSY)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(PLAN_5.PLAN_QTY_ASSY)) AS MON_PLAN_5" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN.PLAN_QTY_ASSY)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(PLAN_6.PLAN_QTY_ASSY)) AS MON_PLAN_6" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN.PLAN_QTY_ASSY)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(PLAN_7.PLAN_QTY_ASSY)) AS MON_PLAN_7" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(MON_AO.ASSY_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(MON_AO.ASSY_MON)) AS ASSY_MON" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(MON_A1.ASSY_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(MON_A1.ASSY_MON)) AS ASSY_MON_1" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(MON_A2.ASSY_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(MON_A2.ASSY_MON)) AS ASSY_MON_2" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(MON_A3.ASSY_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(MON_A3.ASSY_MON)) AS ASSY_MON_3" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(MON_A4.ASSY_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(MON_A4.ASSY_MON)) AS ASSY_MON_4" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(MON_A5.ASSY_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(MON_A5.ASSY_MON)) AS ASSY_MON_5" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(MON_A6.ASSY_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(MON_A6.ASSY_MON)) AS ASSY_MON_6" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(MON_A7.ASSY_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(MON_A7.ASSY_MON)) AS ASSY_MON_7" + "\n");
                strSqlString.Append("         FROM MWIPMATDEF MAT " + "\n");
                //월 계획 SLIS 제품은 MP계획과 동일하게
                strSqlString.Append("            , ( " + "\n");
                strSqlString.Append("                 SELECT FACTORY,MAT_ID,PLAN_QTY_ASSY,PLAN_MONTH " + "\n");
                strSqlString.Append("                   FROM ( " + "\n");
                strSqlString.Append("                          SELECT FACTORY, MAT_ID, PLAN_QTY_ASSY, PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                          WHERE 1=1 " + "\n");
                strSqlString.Append("                          AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                          AND MAT_ID NOT IN ( " + "\n");
                strSqlString.Append("                                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                 FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                                               ) " + "\n");
                strSqlString.Append("                          UNION ALL " + "\n");
                strSqlString.Append("                          SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_ASSY , '" + cdvDate.SelectedValue().Substring(0, 6) + "' AS PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                // 월계획 금일이면 기존대로
                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                    strSqlString.Append("                                     FROM CWIPPLNDAY " + "\n");
                    strSqlString.Append("                                    WHERE 1=1 " + "\n");
                    strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                    strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + start_date + "' AND '" + last_day + "'\n");
                    strSqlString.Append("                                      AND IN_OUT_FLAG = 'OUT' " + "\n");
                    strSqlString.Append("                                      AND CLASS = 'ASSY' " + "\n");
                    strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                }
                else// 금일이 아니면 스냅샷 떠놓은 테이블에서 가져옴.
                {
                    strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                    strSqlString.Append("                                     FROM CWIPPLNSNP@RPTTOMES " + "\n");
                    strSqlString.Append("                                    WHERE 1=1 " + "\n");
                    strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                    strSqlString.Append("                                      AND SNAPSHOT_DAY = '" + cdvDate.SelectedValue() + "'" + "\n");
                    strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + start_date + "' AND '" + last_day + "'\n");
                    strSqlString.Append("                                      AND IN_OUT_FLAG = 'OUT' " + "\n");
                    strSqlString.Append("                                      AND CLASS = 'ASSY' " + "\n");
                    strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                }
                strSqlString.Append("                                   UNION ALL " + "\n");
                strSqlString.Append("                                   SELECT CM_KEY_1 AS FACTORY, MAT_ID, " + "\n");
                strSqlString.Append("                                          SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                                      AND WORK_DATE BETWEEN '" + start_date + "' AND '" + Lastweek_lastday + "'\n");
                strSqlString.Append("                                   GROUP BY CM_KEY_1, MAT_ID " + "\n");
                strSqlString.Append("                                 ) A," + "\n");
                strSqlString.Append("                                 MWIPMATDEF B " + "\n");
                strSqlString.Append("                           WHERE 1=1  " + "\n");
                strSqlString.Append("                             AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                             AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                          GROUP BY A.FACTORY, A.MAT_ID, B.MAT_GRP_3,B.MAT_CMF_13 " + "\n");
                strSqlString.Append("                        ) " + "\n");
                strSqlString.Append("               ) PLAN " + "\n");
                strSqlString.Append("            , ( " + "\n");
                strSqlString.Append("                 SELECT FACTORY,MAT_ID,PLAN_QTY_ASSY,PLAN_MONTH " + "\n");
                strSqlString.Append("                   FROM ( " + "\n");
                strSqlString.Append("                          SELECT FACTORY, MAT_ID, PLAN_QTY_ASSY, PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                          WHERE 1=1 " + "\n");
                strSqlString.Append("                          AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                          AND MAT_ID NOT IN ( " + "\n");
                strSqlString.Append("                                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                 FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                                               ) " + "\n");
                strSqlString.Append("                          UNION ALL " + "\n");
                strSqlString.Append("                          SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_ASSY , '" + month1 + "' AS PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM CWIPPLNSNP@RPTTOMES " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND SNAPSHOT_DAY = '" + strDate1 + "'" + "\n");
                strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + startDate1 + "' AND '" + getEndDate1 + "'\n");
                strSqlString.Append("                                      AND IN_OUT_FLAG = 'OUT' " + "\n");
                strSqlString.Append("                                      AND CLASS = 'ASSY' " + "\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                   UNION ALL " + "\n");
                strSqlString.Append("                                   SELECT CM_KEY_1 AS FACTORY, MAT_ID, " + "\n");
                strSqlString.Append("                                          SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                                      AND WORK_DATE BETWEEN '" + startDate1 + "' AND '" + Lastweek_lastday1 + "'\n");
                strSqlString.Append("                                   GROUP BY CM_KEY_1, MAT_ID " + "\n");
                strSqlString.Append("                                 ) A," + "\n");
                strSqlString.Append("                                 MWIPMATDEF B " + "\n");
                strSqlString.Append("                           WHERE 1=1  " + "\n");
                strSqlString.Append("                             AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                             AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                          GROUP BY A.FACTORY, A.MAT_ID, B.MAT_GRP_3,B.MAT_CMF_13 " + "\n");
                strSqlString.Append("                        ) " + "\n");
                strSqlString.Append("               ) PLAN_1 " + "\n");
                strSqlString.Append("            , ( " + "\n");
                strSqlString.Append("                 SELECT FACTORY,MAT_ID,PLAN_QTY_ASSY,PLAN_MONTH " + "\n");
                strSqlString.Append("                   FROM ( " + "\n");
                strSqlString.Append("                          SELECT FACTORY, MAT_ID, PLAN_QTY_ASSY, PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                          WHERE 1=1 " + "\n");
                strSqlString.Append("                          AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                          AND MAT_ID NOT IN ( " + "\n");
                strSqlString.Append("                                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                 FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                                               ) " + "\n");
                strSqlString.Append("                          UNION ALL " + "\n");
                strSqlString.Append("                          SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_ASSY , '" + month2 + "' AS PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM CWIPPLNSNP@RPTTOMES " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND SNAPSHOT_DAY = '" + strDate2 + "'" + "\n");
                strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + startDate2 + "' AND '" + getEndDate2 + "'\n");
                strSqlString.Append("                                      AND IN_OUT_FLAG = 'OUT' " + "\n");
                strSqlString.Append("                                      AND CLASS = 'ASSY' " + "\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                   UNION ALL " + "\n");
                strSqlString.Append("                                   SELECT CM_KEY_1 AS FACTORY, MAT_ID, " + "\n");
                strSqlString.Append("                                          SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                                      AND WORK_DATE BETWEEN '" + startDate2 + "' AND '" + Lastweek_lastday2 + "'\n");
                strSqlString.Append("                                   GROUP BY CM_KEY_1, MAT_ID " + "\n");
                strSqlString.Append("                                 ) A," + "\n");
                strSqlString.Append("                                 MWIPMATDEF B " + "\n");
                strSqlString.Append("                           WHERE 1=1  " + "\n");
                strSqlString.Append("                             AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                             AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                          GROUP BY A.FACTORY, A.MAT_ID, B.MAT_GRP_3,B.MAT_CMF_13 " + "\n");
                strSqlString.Append("                        ) " + "\n");
                strSqlString.Append("               ) PLAN_2 " + "\n");
                strSqlString.Append("            , ( " + "\n");
                strSqlString.Append("                 SELECT FACTORY,MAT_ID,PLAN_QTY_ASSY,PLAN_MONTH " + "\n");
                strSqlString.Append("                   FROM ( " + "\n");
                strSqlString.Append("                          SELECT FACTORY, MAT_ID, PLAN_QTY_ASSY, PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                          WHERE 1=1 " + "\n");
                strSqlString.Append("                          AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                          AND MAT_ID NOT IN ( " + "\n");
                strSqlString.Append("                                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                 FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                                               ) " + "\n");
                strSqlString.Append("                          UNION ALL " + "\n");
                strSqlString.Append("                          SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_ASSY , '" + month3 + "' AS PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM CWIPPLNSNP@RPTTOMES " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND SNAPSHOT_DAY = '" + strDate3 + "'" + "\n");
                strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + startDate3 + "' AND '" + getEndDate3 + "'\n");
                strSqlString.Append("                                      AND IN_OUT_FLAG = 'OUT' " + "\n");
                strSqlString.Append("                                      AND CLASS = 'ASSY' " + "\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                   UNION ALL " + "\n");
                strSqlString.Append("                                   SELECT CM_KEY_1 AS FACTORY, MAT_ID, " + "\n");
                strSqlString.Append("                                          SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                                      AND WORK_DATE BETWEEN '" + startDate3 + "' AND '" + Lastweek_lastday3 + "'\n");
                strSqlString.Append("                                   GROUP BY CM_KEY_1, MAT_ID " + "\n");
                strSqlString.Append("                                 ) A," + "\n");
                strSqlString.Append("                                 MWIPMATDEF B " + "\n");
                strSqlString.Append("                           WHERE 1=1  " + "\n");
                strSqlString.Append("                             AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                             AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                          GROUP BY A.FACTORY, A.MAT_ID, B.MAT_GRP_3,B.MAT_CMF_13 " + "\n");
                strSqlString.Append("                        ) " + "\n");
                strSqlString.Append("               ) PLAN_3 " + "\n");
                strSqlString.Append("            , ( " + "\n");
                strSqlString.Append("                 SELECT FACTORY,MAT_ID,PLAN_QTY_ASSY,PLAN_MONTH " + "\n");
                strSqlString.Append("                   FROM ( " + "\n");
                strSqlString.Append("                          SELECT FACTORY, MAT_ID, PLAN_QTY_ASSY, PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                          WHERE 1=1 " + "\n");
                strSqlString.Append("                          AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                          AND MAT_ID NOT IN ( " + "\n");
                strSqlString.Append("                                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                 FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                                               ) " + "\n");
                strSqlString.Append("                          UNION ALL " + "\n");
                strSqlString.Append("                          SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_ASSY , '" + month4 + "' AS PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM CWIPPLNSNP@RPTTOMES " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND SNAPSHOT_DAY = '" + strDate4 + "'" + "\n");
                strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + startDate4 + "' AND '" + getEndDate4 + "'\n");
                strSqlString.Append("                                      AND IN_OUT_FLAG = 'OUT' " + "\n");
                strSqlString.Append("                                      AND CLASS = 'ASSY' " + "\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                   UNION ALL " + "\n");
                strSqlString.Append("                                   SELECT CM_KEY_1 AS FACTORY, MAT_ID, " + "\n");
                strSqlString.Append("                                          SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                                      AND WORK_DATE BETWEEN '" + startDate4 + "' AND '" + Lastweek_lastday4 + "'\n");
                strSqlString.Append("                                   GROUP BY CM_KEY_1, MAT_ID " + "\n");
                strSqlString.Append("                                 ) A," + "\n");
                strSqlString.Append("                                 MWIPMATDEF B " + "\n");
                strSqlString.Append("                           WHERE 1=1  " + "\n");
                strSqlString.Append("                             AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                             AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                          GROUP BY A.FACTORY, A.MAT_ID, B.MAT_GRP_3,B.MAT_CMF_13 " + "\n");
                strSqlString.Append("                        ) " + "\n");
                strSqlString.Append("               ) PLAN_4 " + "\n");
                strSqlString.Append("            , ( " + "\n");
                strSqlString.Append("                 SELECT FACTORY,MAT_ID,PLAN_QTY_ASSY,PLAN_MONTH " + "\n");
                strSqlString.Append("                   FROM ( " + "\n");
                strSqlString.Append("                          SELECT FACTORY, MAT_ID, PLAN_QTY_ASSY, PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                          WHERE 1=1 " + "\n");
                strSqlString.Append("                          AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                          AND MAT_ID NOT IN ( " + "\n");
                strSqlString.Append("                                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                 FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                                               ) " + "\n");
                strSqlString.Append("                          UNION ALL " + "\n");
                strSqlString.Append("                          SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_ASSY , '" + month5 + "' AS PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM CWIPPLNSNP@RPTTOMES " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND SNAPSHOT_DAY = '" + strDate5 + "'" + "\n");
                strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + startDate5 + "' AND '" + getEndDate5 + "'\n");
                strSqlString.Append("                                      AND IN_OUT_FLAG = 'OUT' " + "\n");
                strSqlString.Append("                                      AND CLASS = 'ASSY' " + "\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                   UNION ALL " + "\n");
                strSqlString.Append("                                   SELECT CM_KEY_1 AS FACTORY, MAT_ID, " + "\n");
                strSqlString.Append("                                          SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                                      AND WORK_DATE BETWEEN '" + startDate5 + "' AND '" + Lastweek_lastday5 + "'\n");
                strSqlString.Append("                                   GROUP BY CM_KEY_1, MAT_ID " + "\n");
                strSqlString.Append("                                 ) A," + "\n");
                strSqlString.Append("                                 MWIPMATDEF B " + "\n");
                strSqlString.Append("                           WHERE 1=1  " + "\n");
                strSqlString.Append("                             AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                             AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                          GROUP BY A.FACTORY, A.MAT_ID, B.MAT_GRP_3,B.MAT_CMF_13 " + "\n");
                strSqlString.Append("                        ) " + "\n");
                strSqlString.Append("               ) PLAN_5 " + "\n");
                strSqlString.Append("            , ( " + "\n");
                strSqlString.Append("                 SELECT FACTORY,MAT_ID,PLAN_QTY_ASSY,PLAN_MONTH " + "\n");
                strSqlString.Append("                   FROM ( " + "\n");
                strSqlString.Append("                          SELECT FACTORY, MAT_ID, PLAN_QTY_ASSY, PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                          WHERE 1=1 " + "\n");
                strSqlString.Append("                          AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                          AND MAT_ID NOT IN ( " + "\n");
                strSqlString.Append("                                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                 FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                                               ) " + "\n");
                strSqlString.Append("                          UNION ALL " + "\n");
                strSqlString.Append("                          SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_ASSY , '" + month6 + "' AS PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM CWIPPLNSNP@RPTTOMES " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND SNAPSHOT_DAY = '" + strDate6 + "'" + "\n");
                strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + startDate6 + "' AND '" + getEndDate6 + "'\n");
                strSqlString.Append("                                      AND IN_OUT_FLAG = 'OUT' " + "\n");
                strSqlString.Append("                                      AND CLASS = 'ASSY' " + "\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                   UNION ALL " + "\n");
                strSqlString.Append("                                   SELECT CM_KEY_1 AS FACTORY, MAT_ID, " + "\n");
                strSqlString.Append("                                          SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                                      AND WORK_DATE BETWEEN '" + startDate6 + "' AND '" + Lastweek_lastday6 + "'\n");
                strSqlString.Append("                                   GROUP BY CM_KEY_1, MAT_ID " + "\n");
                strSqlString.Append("                                 ) A," + "\n");
                strSqlString.Append("                                 MWIPMATDEF B " + "\n");
                strSqlString.Append("                           WHERE 1=1  " + "\n");
                strSqlString.Append("                             AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                             AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                          GROUP BY A.FACTORY, A.MAT_ID, B.MAT_GRP_3,B.MAT_CMF_13 " + "\n");
                strSqlString.Append("                        ) " + "\n");
                strSqlString.Append("               ) PLAN_6 " + "\n");
                strSqlString.Append("            , ( " + "\n");
                strSqlString.Append("                 SELECT FACTORY,MAT_ID,PLAN_QTY_ASSY,PLAN_MONTH " + "\n");
                strSqlString.Append("                   FROM ( " + "\n");
                strSqlString.Append("                          SELECT FACTORY, MAT_ID, PLAN_QTY_ASSY, PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                          WHERE 1=1 " + "\n");
                strSqlString.Append("                          AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                          AND MAT_ID NOT IN ( " + "\n");
                strSqlString.Append("                                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                 FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                                               ) " + "\n");
                strSqlString.Append("                          UNION ALL " + "\n");
                strSqlString.Append("                          SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_ASSY , '" + month7 + "' AS PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM CWIPPLNSNP@RPTTOMES " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND SNAPSHOT_DAY = '" + strDate7 + "'" + "\n");
                strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + startDate7 + "' AND '" + getEndDate7 + "'\n");
                strSqlString.Append("                                      AND IN_OUT_FLAG = 'OUT' " + "\n");
                strSqlString.Append("                                      AND CLASS = 'ASSY' " + "\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                   UNION ALL " + "\n");
                strSqlString.Append("                                   SELECT CM_KEY_1 AS FACTORY, MAT_ID, " + "\n");
                strSqlString.Append("                                          SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                                      AND WORK_DATE BETWEEN '" + startDate7 + "' AND '" + Lastweek_lastday7 + "'\n");
                strSqlString.Append("                                   GROUP BY CM_KEY_1, MAT_ID " + "\n");
                strSqlString.Append("                                 ) A," + "\n");
                strSqlString.Append("                                 MWIPMATDEF B " + "\n");
                strSqlString.Append("                           WHERE 1=1  " + "\n");
                strSqlString.Append("                             AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                             AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                          GROUP BY A.FACTORY, A.MAT_ID, B.MAT_GRP_3,B.MAT_CMF_13 " + "\n");
                strSqlString.Append("                        ) " + "\n");
                strSqlString.Append("               ) PLAN_7 " + "\n");
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("               SELECT MAT_ID " + "\n");
                // 2010-04-02-임종우 : 매월 1일, 2일 경우 3일자 가져온 데이터로 WORK_DATE 사용하며 월 A/O 값에서 전월 데이터 뺀다.
                if (date.Substring(6, 2).Equals("01"))
                {
                    if (DateTime.Now.ToString("yyyyMMdd").Equals(date)) // 조회날짜 : 1일, 오늘 날짜 : 1일이면 -> 과거 3일자 (전월 말일 -2, 전월 말일 -1, 전월 말일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2[0] + "', 0,'" + dayArry2[1] + "', 0,'" + dayArry2[2] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2[0] + "' AND '" + date + "'" + "\n");
                    }
                    else // 조회날짜 : 1일, 오늘 날짜 : 1일이 아니면.. 즉 과거 조회이면 -> 조회일자 포함 3일자 (전월 말일 -1, 전월 말일, 1일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2[0] + "', 0,'" + dayArry2[1] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2[0] + "' AND '" + date + "'" + "\n");
                    }
                }
                else if (date.Substring(6, 2).Equals("02"))
                {
                    if (DateTime.Now.ToString("yyyyMMdd").Equals(date)) // 조회날짜 : 2일, 오늘 날짜 : 2일이면 -> 과거 3일자 (전월 말일 -1, 전월 말일, 1일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2[0] + "', 0,'" + dayArry2[1] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2[0] + "' AND '" + date + "'" + "\n");
                    }
                    else // 조회날짜 : 2일, 오늘 날짜 : 2일이 아니면.. 즉 과거 조회이면 -> 조회일자 포함 3일자 (전월 말일, 1일, 2일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2[0] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2[0] + "' AND '" + date + "'" + "\n");
                    }
                }
                else
                {
                    strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + Last_Month_Last_day + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                    strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                    strSqlString.Append("                WHERE 1=1 " + "\n");
                    strSqlString.Append("                  AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + date + "'" + "\n");
                }

                strSqlString.Append("                  AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                  AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                  AND FACTORY NOT IN ('RETURN') " + "\n");

                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                  AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }

                strSqlString.Append("                GROUP BY MAT_ID " + "\n");
                strSqlString.Append("               ) MON_AO " + "\n");
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("               SELECT MAT_ID " + "\n");

                // 전일 기준(-1)
                // 2010-04-02-임종우 : 매월 1일, 2일 경우 3일자 가져온 데이터로 WORK_DATE 사용하며 월 A/O 값에서 전월 데이터 뺀다.
                if (strDate1.Substring(6, 2).Equals("01"))
                {
                    if (DateTime.Now.ToString("yyyyMMdd").Equals(startDate1)) // 조회날짜 : 1일, 오늘 날짜 : 1일이면 -> 과거 3일자 (전월 말일 -2, 전월 말일 -1, 전월 말일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_1[0] + "', 0,'" + dayArry2_1[1] + "', 0,'" + dayArry2_1[2] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_1[0] + "' AND '" + strDate1 + "'" + "\n");
                    }
                    else // 조회날짜 : 1일, 오늘 날짜 : 1일이 아니면.. 즉 과거 조회이면 -> 조회일자 포함 3일자 (전월 말일 -1, 전월 말일, 1일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_1[0] + "', 0,'" + dayArry2_1[1] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_1[0] + "' AND '" + strDate1 + "'" + "\n");
                    }
                }
                else if (strDate1.Substring(6, 2).Equals("02"))
                {
                    if (DateTime.Now.ToString("yyyyMMdd").Equals(startDate1)) // 조회날짜 : 2일, 오늘 날짜 : 2일이면 -> 과거 3일자 (전월 말일 -1, 전월 말일, 1일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_1[0] + "', 0,'" + dayArry2_1[1] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_1[0] + "' AND '" + strDate1 + "'" + "\n");
                    }
                    else // 조회날짜 : 2일, 오늘 날짜 : 2일이 아니면.. 즉 과거 조회이면 -> 조회일자 포함 3일자 (전월 말일, 1일, 2일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_1[0] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_1[0] + "' AND '" + strDate1 + "'" + "\n");
                    }
                }
                else
                {
                    strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + lastMonth1 + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                    strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                    strSqlString.Append("                WHERE 1=1 " + "\n");
                    strSqlString.Append("                  AND WORK_DATE BETWEEN '" + lastMonth1 + "' AND '" + strDate1 + "'" + "\n");
                }
                strSqlString.Append("                  AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                  AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                  AND FACTORY NOT IN ('RETURN') " + "\n");

                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                  AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }

                strSqlString.Append("                GROUP BY MAT_ID " + "\n");

                strSqlString.Append("               ) MON_A1 " + "\n");


                strSqlString.Append("             , ( " + "\n");

                strSqlString.Append("               SELECT MAT_ID " + "\n");
                //-2일
                // 2010-04-02-임종우 : 매월 1일, 2일 경우 3일자 가져온 데이터로 WORK_DATE 사용하며 월 A/O 값에서 전월 데이터 뺀다.
                if (strDate2.Substring(6, 2).Equals("01"))
                {
                    if (DateTime.Now.ToString("yyyyMMdd").Equals(startDate2)) // 조회날짜 : 1일, 오늘 날짜 : 1일이면 -> 과거 3일자 (전월 말일 -2, 전월 말일 -1, 전월 말일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_2[0] + "', 0,'" + dayArry2_2[1] + "', 0,'" + dayArry2_2[2] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_2[0] + "' AND '" + strDate2 + "'" + "\n");
                    }
                    else // 조회날짜 : 1일, 오늘 날짜 : 1일이 아니면.. 즉 과거 조회이면 -> 조회일자 포함 3일자 (전월 말일 -1, 전월 말일, 1일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_2[0] + "', 0,'" + dayArry2_2[1] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_2[0] + "' AND '" + strDate2 + "'" + "\n");
                    }
                }
                else if (strDate2.Substring(6, 2).Equals("02"))
                {
                    if (DateTime.Now.ToString("yyyyMMdd").Equals(startDate2)) // 조회날짜 : 2일, 오늘 날짜 : 2일이면 -> 과거 3일자 (전월 말일 -1, 전월 말일, 1일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_2[0] + "', 0,'" + dayArry2_2[1] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_2[0] + "' AND '" + strDate2 + "'" + "\n");
                    }
                    else // 조회날짜 : 2일, 오늘 날짜 : 2일이 아니면.. 즉 과거 조회이면 -> 조회일자 포함 3일자 (전월 말일, 1일, 2일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_2[0] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_2[0] + "' AND '" + strDate2 + "'" + "\n");
                    }
                }
                else
                {
                    strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + lastMonth2 + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                    strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                    strSqlString.Append("                WHERE 1=1 " + "\n");
                    strSqlString.Append("                  AND WORK_DATE BETWEEN '" + lastMonth2 + "' AND '" + strDate2 + "'" + "\n");
                }
                strSqlString.Append("                  AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                  AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                  AND FACTORY NOT IN ('RETURN') " + "\n");

                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                  AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }

                strSqlString.Append("                GROUP BY MAT_ID " + "\n");
                strSqlString.Append("               ) MON_A2 " + "\n");

                strSqlString.Append("             , ( " + "\n");

                strSqlString.Append("               SELECT MAT_ID " + "\n");
                //-3
                // 2010-04-02-임종우 : 매월 1일, 2일 경우 3일자 가져온 데이터로 WORK_DATE 사용하며 월 A/O 값에서 전월 데이터 뺀다.
                if (strDate3.Substring(6, 2).Equals("01"))
                {
                    if (DateTime.Now.ToString("yyyyMMdd").Equals(startDate3)) // 조회날짜 : 1일, 오늘 날짜 : 1일이면 -> 과거 3일자 (전월 말일 -2, 전월 말일 -1, 전월 말일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_3[0] + "', 0,'" + dayArry2_3[1] + "', 0,'" + dayArry2_3[2] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_3[0] + "' AND '" + strDate3 + "'" + "\n");
                    }
                    else // 조회날짜 : 1일, 오늘 날짜 : 1일이 아니면.. 즉 과거 조회이면 -> 조회일자 포함 3일자 (전월 말일 -1, 전월 말일, 1일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_3[0] + "', 0,'" + dayArry2_3[1] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_3[0] + "' AND '" + strDate3 + "'" + "\n");
                    }
                }
                else if (strDate3.Substring(6, 2).Equals("02"))
                {
                    if (DateTime.Now.ToString("yyyyMMdd").Equals(startDate3)) // 조회날짜 : 2일, 오늘 날짜 : 2일이면 -> 과거 3일자 (전월 말일 -1, 전월 말일, 1일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_3[0] + "', 0,'" + dayArry2_3[1] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_3[0] + "' AND '" + strDate3 + "'" + "\n");
                    }
                    else // 조회날짜 : 2일, 오늘 날짜 : 2일이 아니면.. 즉 과거 조회이면 -> 조회일자 포함 3일자 (전월 말일, 1일, 2일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_3[0] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_3[0] + "' AND '" + strDate3 + "'" + "\n");
                    }
                }
                else
                {
                    strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + lastMonth3 + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                    strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                    strSqlString.Append("                WHERE 1=1 " + "\n");
                    strSqlString.Append("                  AND WORK_DATE BETWEEN '" + lastMonth3 + "' AND '" + strDate3 + "'" + "\n");
                }
                strSqlString.Append("                  AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                  AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                  AND FACTORY NOT IN ('RETURN') " + "\n");

                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                  AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }

                strSqlString.Append("                GROUP BY MAT_ID " + "\n");
                strSqlString.Append("               ) MON_A3 " + "\n");

                strSqlString.Append("             , ( " + "\n");

                strSqlString.Append("               SELECT MAT_ID " + "\n");
                //-4
                // 2010-04-02-임종우 : 매월 1일, 2일 경우 3일자 가져온 데이터로 WORK_DATE 사용하며 월 A/O 값에서 전월 데이터 뺀다.
                if (strDate4.Substring(6, 2).Equals("01"))
                {
                    if (DateTime.Now.ToString("yyyyMMdd").Equals(startDate4)) // 조회날짜 : 1일, 오늘 날짜 : 1일이면 -> 과거 3일자 (전월 말일 -2, 전월 말일 -1, 전월 말일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_4[0] + "', 0,'" + dayArry2_4[1] + "', 0,'" + dayArry2_4[2] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_4[0] + "' AND '" + strDate4 + "'" + "\n");
                    }
                    else // 조회날짜 : 1일, 오늘 날짜 : 1일이 아니면.. 즉 과거 조회이면 -> 조회일자 포함 3일자 (전월 말일 -1, 전월 말일, 1일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_4[0] + "', 0,'" + dayArry2_4[1] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_4[0] + "' AND '" + strDate4 + "'" + "\n");
                    }
                }
                else if (strDate4.Substring(6, 2).Equals("02"))
                {
                    if (DateTime.Now.ToString("yyyyMMdd").Equals(startDate4)) // 조회날짜 : 2일, 오늘 날짜 : 2일이면 -> 과거 3일자 (전월 말일 -1, 전월 말일, 1일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_4[0] + "', 0,'" + dayArry2_4[1] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_4[0] + "' AND '" + strDate4 + "'" + "\n");
                    }
                    else // 조회날짜 : 2일, 오늘 날짜 : 2일이 아니면.. 즉 과거 조회이면 -> 조회일자 포함 3일자 (전월 말일, 1일, 2일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_4[0] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_4[0] + "' AND '" + strDate4 + "'" + "\n");
                    }
                }
                else
                {
                    strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + lastMonth4 + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                    strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                    strSqlString.Append("                WHERE 1=1 " + "\n");
                    strSqlString.Append("                  AND WORK_DATE BETWEEN '" + lastMonth4 + "' AND '" + strDate4 + "'" + "\n");
                }
                strSqlString.Append("                  AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                  AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                  AND FACTORY NOT IN ('RETURN') " + "\n");

                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                  AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }

                strSqlString.Append("                GROUP BY MAT_ID " + "\n");
                strSqlString.Append("               ) MON_A4 " + "\n");

                strSqlString.Append("             , ( " + "\n");

                strSqlString.Append("               SELECT MAT_ID " + "\n");
                //-5
                // 2010-04-02-임종우 : 매월 1일, 2일 경우 3일자 가져온 데이터로 WORK_DATE 사용하며 월 A/O 값에서 전월 데이터 뺀다.
                if (strDate5.Substring(6, 2).Equals("01"))
                {
                    if (DateTime.Now.ToString("yyyyMMdd").Equals(startDate5)) // 조회날짜 : 1일, 오늘 날짜 : 1일이면 -> 과거 3일자 (전월 말일 -2, 전월 말일 -1, 전월 말일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_5[0] + "', 0,'" + dayArry2_5[1] + "', 0,'" + dayArry2_5[2] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_5[0] + "' AND '" + strDate5 + "'" + "\n");
                    }
                    else // 조회날짜 : 1일, 오늘 날짜 : 1일이 아니면.. 즉 과거 조회이면 -> 조회일자 포함 3일자 (전월 말일 -1, 전월 말일, 1일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_5[0] + "', 0,'" + dayArry2_5[1] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_5[0] + "' AND '" + strDate5 + "'" + "\n");
                    }
                }
                else if (strDate5.Substring(6, 2).Equals("02"))
                {
                    if (DateTime.Now.ToString("yyyyMMdd").Equals(startDate5)) // 조회날짜 : 2일, 오늘 날짜 : 2일이면 -> 과거 3일자 (전월 말일 -1, 전월 말일, 1일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_5[0] + "', 0,'" + dayArry2_5[1] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_5[0] + "' AND '" + strDate5 + "'" + "\n");
                    }
                    else // 조회날짜 : 2일, 오늘 날짜 : 2일이 아니면.. 즉 과거 조회이면 -> 조회일자 포함 3일자 (전월 말일, 1일, 2일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_5[0] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_5[0] + "' AND '" + strDate5 + "'" + "\n");
                    }
                }
                else
                {
                    strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + lastMonth5 + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                    strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                    strSqlString.Append("                WHERE 1=1 " + "\n");
                    strSqlString.Append("                  AND WORK_DATE BETWEEN '" + lastMonth5 + "' AND '" + strDate5 + "'" + "\n");
                }
                strSqlString.Append("                  AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                  AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                  AND FACTORY NOT IN ('RETURN') " + "\n");

                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                  AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }

                strSqlString.Append("                GROUP BY MAT_ID " + "\n");
                strSqlString.Append("               ) MON_A5 " + "\n");

                strSqlString.Append("             , ( " + "\n");

                strSqlString.Append("               SELECT MAT_ID " + "\n");
                //-6
                // 2010-04-02-임종우 : 매월 1일, 2일 경우 3일자 가져온 데이터로 WORK_DATE 사용하며 월 A/O 값에서 전월 데이터 뺀다.
                if (strDate6.Substring(6, 2).Equals("01"))
                {
                    if (DateTime.Now.ToString("yyyyMMdd").Equals(startDate6)) // 조회날짜 : 1일, 오늘 날짜 : 1일이면 -> 과거 3일자 (전월 말일 -2, 전월 말일 -1, 전월 말일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_6[0] + "', 0,'" + dayArry2_6[1] + "', 0,'" + dayArry2_6[2] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_6[0] + "' AND '" + strDate6 + "'" + "\n");
                    }
                    else // 조회날짜 : 1일, 오늘 날짜 : 1일이 아니면.. 즉 과거 조회이면 -> 조회일자 포함 3일자 (전월 말일 -1, 전월 말일, 1일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_6[0] + "', 0,'" + dayArry2_6[1] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_6[0] + "' AND '" + strDate6 + "'" + "\n");
                    }
                }
                else if (strDate6.Substring(6, 2).Equals("02"))
                {
                    if (DateTime.Now.ToString("yyyyMMdd").Equals(startDate6)) // 조회날짜 : 2일, 오늘 날짜 : 2일이면 -> 과거 3일자 (전월 말일 -1, 전월 말일, 1일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_6[0] + "', 0,'" + dayArry2_6[1] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_6[0] + "' AND '" + strDate6 + "'" + "\n");
                    }
                    else // 조회날짜 : 2일, 오늘 날짜 : 2일이 아니면.. 즉 과거 조회이면 -> 조회일자 포함 3일자 (전월 말일, 1일, 2일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_6[0] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_6[0] + "' AND '" + strDate6 + "'" + "\n");
                    }
                }
                else
                {
                    strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + lastMonth6 + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                    strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                    strSqlString.Append("                WHERE 1=1 " + "\n");
                    strSqlString.Append("                  AND WORK_DATE BETWEEN '" + lastMonth6 + "' AND '" + strDate6 + "'" + "\n");
                }
                strSqlString.Append("                  AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                  AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                  AND FACTORY NOT IN ('RETURN') " + "\n");

                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                  AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }

                strSqlString.Append("                GROUP BY MAT_ID " + "\n");
                strSqlString.Append("               ) MON_A6 " + "\n");

                strSqlString.Append("             , ( " + "\n");

                strSqlString.Append("               SELECT MAT_ID " + "\n");
                //-7
                // 2010-04-02-임종우 : 매월 1일, 2일 경우 3일자 가져온 데이터로 WORK_DATE 사용하며 월 A/O 값에서 전월 데이터 뺀다.
                if (strDate7.Substring(6, 2).Equals("01"))
                {
                    if (DateTime.Now.ToString("yyyyMMdd").Equals(startDate7)) // 조회날짜 : 1일, 오늘 날짜 : 1일이면 -> 과거 3일자 (전월 말일 -2, 전월 말일 -1, 전월 말일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_7[0] + "', 0,'" + dayArry2_7[1] + "', 0,'" + dayArry2_7[2] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_7[0] + "' AND '" + strDate7 + "'" + "\n");
                    }
                    else // 조회날짜 : 1일, 오늘 날짜 : 1일이 아니면.. 즉 과거 조회이면 -> 조회일자 포함 3일자 (전월 말일 -1, 전월 말일, 1일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_7[0] + "', 0,'" + dayArry2_1[1] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_7[0] + "' AND '" + strDate7 + "'" + "\n");
                    }
                }
                else if (strDate7.Substring(6, 2).Equals("02"))
                {
                    if (DateTime.Now.ToString("yyyyMMdd").Equals(startDate7)) // 조회날짜 : 2일, 오늘 날짜 : 2일이면 -> 과거 3일자 (전월 말일 -1, 전월 말일, 1일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_7[0] + "', 0,'" + dayArry2_7[1] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_7[0] + "' AND '" + strDate7 + "'" + "\n");
                    }
                    else // 조회날짜 : 2일, 오늘 날짜 : 2일이 아니면.. 즉 과거 조회이면 -> 조회일자 포함 3일자 (전월 말일, 1일, 2일) 이기에..
                    {
                        strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + dayArry2_7[0] + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                        strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                        strSqlString.Append("                WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND WORK_DATE BETWEEN '" + dayArry2_7[0] + "' AND '" + strDate7 + "'" + "\n");
                    }
                }
                else
                {
                    strSqlString.Append("                    , SUM(DECODE(WORK_DATE,'" + lastMonth7 + "', 0, NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0))) AS ASSY_MON " + "\n");
                    strSqlString.Append("                 FROM RSUMFACMOV " + "\n");
                    strSqlString.Append("                WHERE 1=1 " + "\n");
                    strSqlString.Append("                  AND WORK_DATE BETWEEN '" + lastMonth7 + "' AND '" + strDate7 + "'" + "\n");
                }
                strSqlString.Append("                  AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                  AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                  AND FACTORY NOT IN ('RETURN') " + "\n");

                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                  AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }

                strSqlString.Append("                GROUP BY MAT_ID " + "\n");
                strSqlString.Append("               ) MON_A7 " + "\n");





                strSqlString.Append("         WHERE 1 = 1 " + "\n");
                strSqlString.Append("           AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("           AND PLAN.PLAN_MONTH(+) = '" + month + "' " + "\n");
                strSqlString.Append("           AND PLAN_1.PLAN_MONTH(+) = '" + month1 + "' " + "\n");
                strSqlString.Append("           AND PLAN_2.PLAN_MONTH(+) = '" + month2 + "' " + "\n");
                strSqlString.Append("           AND PLAN_3.PLAN_MONTH(+) = '" + month3 + "' " + "\n");
                strSqlString.Append("           AND PLAN_4.PLAN_MONTH(+) = '" + month4 + "' " + "\n");
                strSqlString.Append("           AND PLAN_5.PLAN_MONTH(+) = '" + month5 + "' " + "\n");
                strSqlString.Append("           AND PLAN_6.PLAN_MONTH(+) = '" + month6 + "' " + "\n");
                strSqlString.Append("           AND PLAN_7.PLAN_MONTH(+) = '" + month7 + "' " + "\n");
                strSqlString.Append("           AND MAT.MAT_TYPE= 'FG' " + "\n");
                // 상세 검색 조건
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                {
                    strSqlString.AppendFormat("       AND MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
                }
                else
                {
                    strSqlString.Append("           AND MAT_GRP_1 IN ('SE','HX') " + "\n");
                }
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("                     AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("                     AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("                     AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("                     AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("                     AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("                     AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("                     AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                strSqlString.Append("           AND MAT.DELETE_FLAG <> 'Y' " + "\n");
                strSqlString.Append("           AND MAT.FACTORY =PLAN.FACTORY(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PLAN.MAT_ID(+) " + "\n");

                strSqlString.Append("           AND MAT.FACTORY =PLAN_1.FACTORY(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PLAN_1.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.FACTORY =PLAN_2.FACTORY(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PLAN_2.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.FACTORY =PLAN_3.FACTORY(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PLAN_3.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.FACTORY =PLAN_4.FACTORY(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PLAN_4.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.FACTORY =PLAN_5.FACTORY(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PLAN_5.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.FACTORY =PLAN_6.FACTORY(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PLAN_6.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.FACTORY =PLAN_7.FACTORY(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PLAN_7.MAT_ID(+) " + "\n");

                strSqlString.Append("           AND MAT.MAT_ID = MON_AO.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = MON_A1.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = MON_A2.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = MON_A3.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = MON_A4.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = MON_A5.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = MON_A6.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = MON_A7.MAT_ID(+) " + "\n");




                strSqlString.Append("      GROUP BY MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7, MAT.MAT_CMF_13" + "\n");
                strSqlString.Append("     ) A  " + "\n");
                strSqlString.Append("     , MWIPMATDEF G " + "\n");
                strSqlString.Append(" WHERE 1 = 1 " + "\n");
                strSqlString.Append("   AND G.FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("   AND A.MAT_ID = G.MAT_ID(+)" + "\n");
                strSqlString.Append("   GROUP BY G.MAT_GRP_3,G.MAT_GRP_1" + "\n");
                strSqlString.Append("   HAVING (" + "\n");
                strSqlString.Append("   NVL(SUM(A.MON_PLAN), 0)+ " + "\n");
                strSqlString.Append("   NVL(SUM(A.ASSY_MON), 0) " + "\n");
                strSqlString.Append("   ) <> 0 " + "\n");
                strSqlString.Append("   ) A " + "\n");
                strSqlString.Append("    WHERE 1=1 " + "\n");
                strSqlString.Append("   AND A.PKG <> '-' " + "\n");
                strSqlString.Append("    ) A " + "\n");
                strSqlString.Append("   GROUP BY A.CUSTOMER" + "\n");
               
            }
            // TEST 달성율
            else
            {
                strSqlString.Append("SELECT CUSTOMER , DECODE(SUM(TARGET_MON), 0, 0, ROUND((SUM(SHIP_MON)/SUM(TARGET_MON))*100, 2)) JINDO" + "\n");
                strSqlString.Append(", DECODE(SUM(TARGET_MON_1), 0, 0, ROUND((SUM(SHIP_MON_1)/SUM(TARGET_MON_1))*100, 2)) JINDO_1" + "\n");
                strSqlString.Append(", DECODE(SUM(TARGET_MON_2), 0, 0, ROUND((SUM(SHIP_MON_2)/SUM(TARGET_MON_2))*100, 2)) JINDO_2" + "\n");
                strSqlString.Append(", DECODE(SUM(TARGET_MON_3), 0, 0, ROUND((SUM(SHIP_MON_3)/SUM(TARGET_MON_3))*100, 2)) JINDO_3" + "\n");
                strSqlString.Append(", DECODE(SUM(TARGET_MON_4), 0, 0, ROUND((SUM(SHIP_MON_4)/SUM(TARGET_MON_4))*100, 2)) JINDO_4" + "\n");
                strSqlString.Append(", DECODE(SUM(TARGET_MON_5), 0, 0, ROUND((SUM(SHIP_MON_5)/SUM(TARGET_MON_5))*100, 2)) JINDO_5" + "\n");
                strSqlString.Append(", DECODE(SUM(TARGET_MON_6), 0, 0, ROUND((SUM(SHIP_MON_6)/SUM(TARGET_MON_6))*100, 2)) JINDO_6" + "\n");
                strSqlString.Append(", DECODE(SUM(TARGET_MON_7), 0, 0, ROUND((SUM(SHIP_MON_7)/SUM(TARGET_MON_7))*100, 2)) JINDO_7" + "\n");
                strSqlString.Append(" FROM( " + "\n");
                strSqlString.Append("SELECT C.MAT_GRP_1 AS CUSTOMER" + "\n");
                strSqlString.Append("       , ROUND(((SUM(NVL(A.MON_PLAN,0)) * " + jindoPer + ") / 100),0) AS TARGET_MON " + "\n");
                strSqlString.Append("       , ROUND(((SUM(NVL(A.MON_PLAN_1,0)) * " + jindoPer1 + ") / 100),0) AS TARGET_MON_1 " + "\n");
                strSqlString.Append("       , ROUND(((SUM(NVL(A.MON_PLAN_2,0)) * " + jindoPer2 + ") / 100),0) AS TARGET_MON_2 " + "\n");
                strSqlString.Append("       , ROUND(((SUM(NVL(A.MON_PLAN_3,0)) * " + jindoPer3 + ") / 100),0) AS TARGET_MON_3 " + "\n");
                strSqlString.Append("       , ROUND(((SUM(NVL(A.MON_PLAN_4,0)) * " + jindoPer4 + ") / 100),0) AS TARGET_MON_4 " + "\n");
                strSqlString.Append("       , ROUND(((SUM(NVL(A.MON_PLAN_5,0)) * " + jindoPer5 + ") / 100),0) AS TARGET_MON_5 " + "\n");
                strSqlString.Append("       , ROUND(((SUM(NVL(A.MON_PLAN_6,0)) * " + jindoPer6 + ") / 100),0) AS TARGET_MON_6 " + "\n");
                strSqlString.Append("       , ROUND(((SUM(NVL(A.MON_PLAN_7,0)) * " + jindoPer7 + ") / 100),0) AS TARGET_MON_7 " + "\n");
                strSqlString.Append("       , SUM(NVL(A.SHIP_MON,0)) AS SHIP_MON " + "\n");
                strSqlString.Append("       , SUM(NVL(A.SHIP_MON_1,0)) AS SHIP_MON_1 " + "\n");
                strSqlString.Append("       , SUM(NVL(A.SHIP_MON_2,0)) AS SHIP_MON_2 " + "\n");
                strSqlString.Append("       , SUM(NVL(A.SHIP_MON_3,0)) AS SHIP_MON_3 " + "\n");
                strSqlString.Append("       , SUM(NVL(A.SHIP_MON_4,0)) AS SHIP_MON_4 " + "\n");
                strSqlString.Append("       , SUM(NVL(A.SHIP_MON_5,0)) AS SHIP_MON_5 " + "\n");
                strSqlString.Append("       , SUM(NVL(A.SHIP_MON_6,0)) AS SHIP_MON_6 " + "\n");
                strSqlString.Append("       , SUM(NVL(A.SHIP_MON_7,0)) AS SHIP_MON_7 " + "\n");
                strSqlString.Append("       FROM( " + "\n");
                strSqlString.Append("       SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7 " + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN.PLAN_QTY_TEST)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(PLAN.PLAN_QTY_TEST)) AS MON_PLAN" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN_1.PLAN_QTY_TEST)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(PLAN_1.PLAN_QTY_TEST)) AS MON_PLAN_1" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN_2.PLAN_QTY_TEST)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(PLAN_2.PLAN_QTY_TEST)) AS MON_PLAN_2" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN_3.PLAN_QTY_TEST)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(PLAN_3.PLAN_QTY_TEST)) AS MON_PLAN_3" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN_4.PLAN_QTY_TEST)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(PLAN_4.PLAN_QTY_TEST)) AS MON_PLAN_4" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN_5.PLAN_QTY_TEST)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(PLAN_5.PLAN_QTY_TEST)) AS MON_PLAN_5" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN_6.PLAN_QTY_TEST)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(PLAN_6.PLAN_QTY_TEST)) AS MON_PLAN_6" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN_7.PLAN_QTY_TEST)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(PLAN_7.PLAN_QTY_TEST)) AS MON_PLAN_7" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP.SHIP_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(SHP.SHIP_MON)) AS SHIP_MON" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP_1.SHIP_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(SHP_1.SHIP_MON)) AS SHIP_MON_1" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP_2.SHIP_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(SHP_2.SHIP_MON)) AS SHIP_MON_2" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP_3.SHIP_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(SHP_3.SHIP_MON)) AS SHIP_MON_3" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP_4.SHIP_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(SHP_4.SHIP_MON)) AS SHIP_MON_4" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP_5.SHIP_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(SHP_5.SHIP_MON)) AS SHIP_MON_5" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP_6.SHIP_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(SHP_6.SHIP_MON)) AS SHIP_MON_6" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP_7.SHIP_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(SHP_7.SHIP_MON)) AS SHIP_MON_7" + "\n");
                strSqlString.Append("         FROM MWIPMATDEF MAT " + "\n");

                //월 계획 SLIS 제품은 MP계획과 동일하게
                strSqlString.Append("            , ( " + "\n");
                strSqlString.Append("                 SELECT FACTORY,MAT_ID,PLAN_QTY_TEST,PLAN_MONTH " + "\n");
                strSqlString.Append("                   FROM ( " + "\n");
                strSqlString.Append("                          SELECT FACTORY, MAT_ID, PLAN_QTY_TEST, PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                          WHERE 1=1 " + "\n");
                strSqlString.Append("                          AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                          AND MAT_ID NOT IN ( " + "\n");
                strSqlString.Append("                                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                 FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                                               ) " + "\n");
                strSqlString.Append("                          UNION ALL " + "\n");
                strSqlString.Append("                          SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_TEST , '" + cdvDate.SelectedValue().Substring(0, 6) + "' AS PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");

                // 월계획 금일이면 기존대로
                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                    strSqlString.Append("                                     FROM CWIPPLNDAY " + "\n");
                    strSqlString.Append("                                    WHERE 1=1 " + "\n");
                    strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                    strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + start_date + "' AND '" + last_day + "'\n");
                    strSqlString.Append("                                      AND IN_OUT_FLAG = 'IN' " + "\n");
                    strSqlString.Append("                                      AND CLASS = 'SLIS' " + "\n");
                    strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                }
                else// 금일이 아니면 스냅샷 떠놓은 테이블에서 가져옴.
                {
                    strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                    strSqlString.Append("                                     FROM CWIPPLNSNP@RPTTOMES " + "\n");
                    strSqlString.Append("                                    WHERE 1=1 " + "\n");
                    strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                    strSqlString.Append("                                      AND SNAPSHOT_DAY = '" + cdvDate.SelectedValue() + "'" + "\n");
                    strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + start_date + "' AND '" + last_day + "'\n");
                    strSqlString.Append("                                      AND IN_OUT_FLAG = 'IN' " + "\n");
                    strSqlString.Append("                                      AND CLASS = 'SLIS' " + "\n");
                    strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                }
                strSqlString.Append("                                   UNION ALL " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, " + "\n");
                strSqlString.Append("                                          SUM(SHP_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM VSUMWIPSHP " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                                      AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                      AND WORK_DATE BETWEEN '" + start_date + "' AND '" + Lastweek_lastday + "'\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                 ) A," + "\n");
                strSqlString.Append("                                 MWIPMATDEF B " + "\n");
                strSqlString.Append("                           WHERE 1=1  " + "\n");
                strSqlString.Append("                             AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                             AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                          GROUP BY A.FACTORY, A.MAT_ID, B.MAT_GRP_3,B.MAT_CMF_13 " + "\n");
                strSqlString.Append("                        ) " + "\n");
                strSqlString.Append("               ) PLAN " + "\n");

                strSqlString.Append("            , ( " + "\n");
                strSqlString.Append("                 SELECT FACTORY,MAT_ID,PLAN_QTY_TEST,PLAN_MONTH " + "\n");
                strSqlString.Append("                   FROM ( " + "\n");
                strSqlString.Append("                          SELECT FACTORY, MAT_ID, PLAN_QTY_TEST, PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                          WHERE 1=1 " + "\n");
                strSqlString.Append("                          AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                          AND MAT_ID NOT IN ( " + "\n");
                strSqlString.Append("                                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                 FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                                               ) " + "\n");
                strSqlString.Append("                          UNION ALL " + "\n");
                strSqlString.Append("                          SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_TEST , '" + month1 + "' AS PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM CWIPPLNSNP@RPTTOMES " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND SNAPSHOT_DAY = '" + strDate1 + "'" + "\n");
                strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + startDate1 + "' AND '" + getEndDate1 + "'\n");
                strSqlString.Append("                                      AND IN_OUT_FLAG = 'IN' " + "\n");
                strSqlString.Append("                                      AND CLASS = 'SLIS' " + "\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                   UNION ALL " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, " + "\n");
                strSqlString.Append("                                          SUM(SHP_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM VSUMWIPSHP " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                                      AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                      AND WORK_DATE BETWEEN '" + startDate1 + "' AND '" + Lastweek_lastday1 + "'\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                 ) A," + "\n");
                strSqlString.Append("                                 MWIPMATDEF B " + "\n");
                strSqlString.Append("                           WHERE 1=1  " + "\n");
                strSqlString.Append("                             AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                             AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                          GROUP BY A.FACTORY, A.MAT_ID, B.MAT_GRP_3,B.MAT_CMF_13 " + "\n");
                strSqlString.Append("                        ) " + "\n");
                strSqlString.Append("               ) PLAN_1 " + "\n");

                strSqlString.Append("            , ( " + "\n");
                strSqlString.Append("                 SELECT FACTORY,MAT_ID,PLAN_QTY_TEST,PLAN_MONTH " + "\n");
                strSqlString.Append("                   FROM ( " + "\n");
                strSqlString.Append("                          SELECT FACTORY, MAT_ID, PLAN_QTY_TEST, PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                          WHERE 1=1 " + "\n");
                strSqlString.Append("                          AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                          AND MAT_ID NOT IN ( " + "\n");
                strSqlString.Append("                                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                 FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                                               ) " + "\n");
                strSqlString.Append("                          UNION ALL " + "\n");
                strSqlString.Append("                          SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_TEST , '" + month2 + "' AS PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM CWIPPLNSNP@RPTTOMES " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND SNAPSHOT_DAY = '" + strDate2 + "'" + "\n");
                strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + startDate2 + "' AND '" + getEndDate2 + "'\n");
                strSqlString.Append("                                      AND IN_OUT_FLAG = 'IN' " + "\n");
                strSqlString.Append("                                      AND CLASS = 'SLIS' " + "\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                   UNION ALL " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, " + "\n");
                strSqlString.Append("                                          SUM(SHP_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM VSUMWIPSHP " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                                      AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                      AND WORK_DATE BETWEEN '" + startDate2 + "' AND '" + Lastweek_lastday2 + "'\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                 ) A," + "\n");
                strSqlString.Append("                                 MWIPMATDEF B " + "\n");
                strSqlString.Append("                           WHERE 1=1  " + "\n");
                strSqlString.Append("                             AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                             AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                          GROUP BY A.FACTORY, A.MAT_ID, B.MAT_GRP_3,B.MAT_CMF_13 " + "\n");
                strSqlString.Append("                        ) " + "\n");
                strSqlString.Append("               ) PLAN_2 " + "\n");

                strSqlString.Append("            , ( " + "\n");
                strSqlString.Append("                 SELECT FACTORY,MAT_ID,PLAN_QTY_TEST,PLAN_MONTH " + "\n");
                strSqlString.Append("                   FROM ( " + "\n");
                strSqlString.Append("                          SELECT FACTORY, MAT_ID, PLAN_QTY_TEST, PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                          WHERE 1=1 " + "\n");
                strSqlString.Append("                          AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                          AND MAT_ID NOT IN ( " + "\n");
                strSqlString.Append("                                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                 FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                                               ) " + "\n");
                strSqlString.Append("                          UNION ALL " + "\n");
                strSqlString.Append("                          SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_TEST , '" + month3 + "' AS PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM CWIPPLNSNP@RPTTOMES " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND SNAPSHOT_DAY = '" + strDate3 + "'" + "\n");
                strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + startDate3 + "' AND '" + getEndDate3 + "'\n");
                strSqlString.Append("                                      AND IN_OUT_FLAG = 'IN' " + "\n");
                strSqlString.Append("                                      AND CLASS = 'SLIS' " + "\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                   UNION ALL " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, " + "\n");
                strSqlString.Append("                                          SUM(SHP_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM VSUMWIPSHP " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                                      AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                      AND WORK_DATE BETWEEN '" + startDate3 + "' AND '" + Lastweek_lastday3 + "'\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                 ) A," + "\n");
                strSqlString.Append("                                 MWIPMATDEF B " + "\n");
                strSqlString.Append("                           WHERE 1=1  " + "\n");
                strSqlString.Append("                             AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                             AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                          GROUP BY A.FACTORY, A.MAT_ID, B.MAT_GRP_3,B.MAT_CMF_13 " + "\n");
                strSqlString.Append("                        ) " + "\n");
                strSqlString.Append("               ) PLAN_3 " + "\n");

                strSqlString.Append("            , ( " + "\n");
                strSqlString.Append("                 SELECT FACTORY,MAT_ID,PLAN_QTY_TEST,PLAN_MONTH " + "\n");
                strSqlString.Append("                   FROM ( " + "\n");
                strSqlString.Append("                          SELECT FACTORY, MAT_ID, PLAN_QTY_TEST, PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                          WHERE 1=1 " + "\n");
                strSqlString.Append("                          AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                          AND MAT_ID NOT IN ( " + "\n");
                strSqlString.Append("                                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                 FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                                               ) " + "\n");
                strSqlString.Append("                          UNION ALL " + "\n");
                strSqlString.Append("                          SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_TEST , '" + month4 + "' AS PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM CWIPPLNSNP@RPTTOMES " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND SNAPSHOT_DAY = '" + strDate4 + "'" + "\n");
                strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + startDate4 + "' AND '" + getEndDate4 + "'\n");
                strSqlString.Append("                                      AND IN_OUT_FLAG = 'IN' " + "\n");
                strSqlString.Append("                                      AND CLASS = 'SLIS' " + "\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                   UNION ALL " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, " + "\n");
                strSqlString.Append("                                          SUM(SHP_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM VSUMWIPSHP " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                                      AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                      AND WORK_DATE BETWEEN '" + startDate4 + "' AND '" + Lastweek_lastday4 + "'\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                 ) A," + "\n");
                strSqlString.Append("                                 MWIPMATDEF B " + "\n");
                strSqlString.Append("                           WHERE 1=1  " + "\n");
                strSqlString.Append("                             AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                             AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                          GROUP BY A.FACTORY, A.MAT_ID, B.MAT_GRP_3,B.MAT_CMF_13 " + "\n");
                strSqlString.Append("                        ) " + "\n");
                strSqlString.Append("               ) PLAN_4 " + "\n");

                strSqlString.Append("            , ( " + "\n");
                strSqlString.Append("                 SELECT FACTORY,MAT_ID,PLAN_QTY_TEST,PLAN_MONTH " + "\n");
                strSqlString.Append("                   FROM ( " + "\n");
                strSqlString.Append("                          SELECT FACTORY, MAT_ID, PLAN_QTY_TEST, PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                          WHERE 1=1 " + "\n");
                strSqlString.Append("                          AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                          AND MAT_ID NOT IN ( " + "\n");
                strSqlString.Append("                                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                 FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                                               ) " + "\n");
                strSqlString.Append("                          UNION ALL " + "\n");
                strSqlString.Append("                          SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_TEST , '" + month5 + "' AS PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM CWIPPLNSNP@RPTTOMES " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND SNAPSHOT_DAY = '" + strDate5 + "'" + "\n");
                strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + startDate5 + "' AND '" + getEndDate5 + "'\n");
                strSqlString.Append("                                      AND IN_OUT_FLAG = 'IN' " + "\n");
                strSqlString.Append("                                      AND CLASS = 'SLIS' " + "\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                   UNION ALL " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, " + "\n");
                strSqlString.Append("                                          SUM(SHP_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM VSUMWIPSHP " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                                      AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                      AND WORK_DATE BETWEEN '" + startDate5 + "' AND '" + Lastweek_lastday5 + "'\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                 ) A," + "\n");
                strSqlString.Append("                                 MWIPMATDEF B " + "\n");
                strSqlString.Append("                           WHERE 1=1  " + "\n");
                strSqlString.Append("                             AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                             AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                          GROUP BY A.FACTORY, A.MAT_ID, B.MAT_GRP_3,B.MAT_CMF_13 " + "\n");
                strSqlString.Append("                        ) " + "\n");
                strSqlString.Append("               ) PLAN_5 " + "\n");

                strSqlString.Append("            , ( " + "\n");
                strSqlString.Append("                 SELECT FACTORY,MAT_ID,PLAN_QTY_TEST,PLAN_MONTH " + "\n");
                strSqlString.Append("                   FROM ( " + "\n");
                strSqlString.Append("                          SELECT FACTORY, MAT_ID, PLAN_QTY_TEST, PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                          WHERE 1=1 " + "\n");
                strSqlString.Append("                          AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                          AND MAT_ID NOT IN ( " + "\n");
                strSqlString.Append("                                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                 FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                                               ) " + "\n");
                strSqlString.Append("                          UNION ALL " + "\n");
                strSqlString.Append("                          SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_TEST , '" + month6 + "' AS PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM CWIPPLNSNP@RPTTOMES " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND SNAPSHOT_DAY = '" + strDate6 + "'" + "\n");
                strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + startDate6 + "' AND '" + getEndDate6 + "'\n");
                strSqlString.Append("                                      AND IN_OUT_FLAG = 'IN' " + "\n");
                strSqlString.Append("                                      AND CLASS = 'SLIS' " + "\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                   UNION ALL " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, " + "\n");
                strSqlString.Append("                                          SUM(SHP_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM VSUMWIPSHP " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                                      AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                      AND WORK_DATE BETWEEN '" + startDate6 + "' AND '" + Lastweek_lastday6 + "'\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                 ) A," + "\n");
                strSqlString.Append("                                 MWIPMATDEF B " + "\n");
                strSqlString.Append("                           WHERE 1=1  " + "\n");
                strSqlString.Append("                             AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                             AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                          GROUP BY A.FACTORY, A.MAT_ID, B.MAT_GRP_3,B.MAT_CMF_13 " + "\n");
                strSqlString.Append("                        ) " + "\n");
                strSqlString.Append("               ) PLAN_6 " + "\n");

                strSqlString.Append("            , ( " + "\n");
                strSqlString.Append("                 SELECT FACTORY,MAT_ID,PLAN_QTY_TEST,PLAN_MONTH " + "\n");
                strSqlString.Append("                   FROM ( " + "\n");
                strSqlString.Append("                          SELECT FACTORY, MAT_ID, PLAN_QTY_TEST, PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM CWIPPLNMON " + "\n");
                strSqlString.Append("                          WHERE 1=1 " + "\n");
                strSqlString.Append("                          AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                          AND MAT_ID NOT IN ( " + "\n");
                strSqlString.Append("                                                SELECT MAT_ID " + "\n");
                strSqlString.Append("                                                 FROM MWIPMATDEF " + "\n");
                strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                                                 AND MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                                               ) " + "\n");
                strSqlString.Append("                          UNION ALL " + "\n");
                strSqlString.Append("                          SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_TEST , '" + month7 + "' AS PLAN_MONTH " + "\n");
                strSqlString.Append("                          FROM ( " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM CWIPPLNSNP@RPTTOMES " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND SNAPSHOT_DAY = '" + strDate7 + "'" + "\n");
                strSqlString.Append("                                      AND PLAN_DAY BETWEEN '" + startDate7 + "' AND '" + getEndDate7 + "'\n");
                strSqlString.Append("                                      AND IN_OUT_FLAG = 'IN' " + "\n");
                strSqlString.Append("                                      AND CLASS = 'SLIS' " + "\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                   UNION ALL " + "\n");
                strSqlString.Append("                                   SELECT FACTORY, MAT_ID, " + "\n");
                strSqlString.Append("                                          SUM(SHP_QTY_1) AS PLAN_QTY " + "\n");
                strSqlString.Append("                                     FROM VSUMWIPSHP " + "\n");
                strSqlString.Append("                                    WHERE 1=1 " + "\n");
                strSqlString.Append("                                      AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                                      AND CM_KEY_3 LIKE 'P%' " + "\n");
                strSqlString.Append("                                      AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                                      AND WORK_DATE BETWEEN '" + startDate7 + "' AND '" + Lastweek_lastday7 + "'\n");
                strSqlString.Append("                                   GROUP BY FACTORY, MAT_ID " + "\n");
                strSqlString.Append("                                 ) A," + "\n");
                strSqlString.Append("                                 MWIPMATDEF B " + "\n");
                strSqlString.Append("                           WHERE 1=1  " + "\n");
                strSqlString.Append("                             AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("                             AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_1 = 'SE' " + "\n");
                strSqlString.Append("                             AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
                strSqlString.Append("                          GROUP BY A.FACTORY, A.MAT_ID, B.MAT_GRP_3,B.MAT_CMF_13 " + "\n");
                strSqlString.Append("                        ) " + "\n");
                strSqlString.Append("               ) PLAN_7 " + "\n");
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("               SELECT MAT_ID " + "\n");
                strSqlString.AppendFormat("                     , SUM(CASE WHEN WORK_DATE BETWEEN '{0}' AND '{1}'" + "\n", start_date, date);
                strSqlString.Append("                    THEN SHP_QTY_1 " + "\n");
                strSqlString.Append("                    ELSE 0 END) SHIP_MON " + "\n");
                strSqlString.Append("                 FROM VSUMWIPSHP " + "\n");
                strSqlString.Append("                WHERE 1=1 " + "\n");

                // 2010-04-02-임종우 : 매월 1일, 2일 경우 3일자 가져온 데이터로 WORK_DATE 사용한다.
                if (date.Substring(6, 2).Equals("01") || date.Substring(6, 2).Equals("02"))
                {
                    strSqlString.Append("                   AND WORK_DATE BETWEEN '" + dayArry2[0] + "' AND '" + date + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("                   AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + date + "'" + "\n");
                }

                strSqlString.Append("                  AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                  AND LOT_TYPE = 'W' " + "\n");
                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                  AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }
                strSqlString.Append("                GROUP BY MAT_ID " + "\n");
                strSqlString.Append("               ) SHP " + "\n");
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("               SELECT MAT_ID " + "\n");
                strSqlString.AppendFormat("                     , SUM(CASE WHEN WORK_DATE BETWEEN '{0}' AND '{1}'" + "\n", startDate1, strDate1);
                strSqlString.Append("                    THEN SHP_QTY_1 " + "\n");
                strSqlString.Append("                    ELSE 0 END) SHIP_MON " + "\n");
                strSqlString.Append("                 FROM VSUMWIPSHP " + "\n");
                strSqlString.Append("                WHERE 1=1 " + "\n");
                // -1일
                // 2010-04-02-임종우 : 매월 1일, 2일 경우 3일자 가져온 데이터로 WORK_DATE 사용한다.
                if (strDate1.Substring(6, 2).Equals("01") || strDate1.Substring(6, 2).Equals("02"))
                {
                    strSqlString.Append("                   AND WORK_DATE BETWEEN '" + dayArry2_1[0] + "' AND '" + strDate1 + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("                   AND WORK_DATE BETWEEN '" + lastMonth1 + "' AND '" + strDate1 + "'" + "\n");
                }
                strSqlString.Append("                  AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                  AND LOT_TYPE = 'W' " + "\n");
                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                  AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }
                strSqlString.Append("                GROUP BY MAT_ID " + "\n");
                strSqlString.Append("               ) SHP_1 " + "\n");
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("               SELECT MAT_ID " + "\n");
                strSqlString.AppendFormat("                     , SUM(CASE WHEN WORK_DATE BETWEEN '{0}' AND '{1}'" + "\n", startDate2, strDate2);
                strSqlString.Append("                    THEN SHP_QTY_1 " + "\n");
                strSqlString.Append("                    ELSE 0 END) SHIP_MON " + "\n");
                strSqlString.Append("                 FROM VSUMWIPSHP " + "\n");
                strSqlString.Append("                WHERE 1=1 " + "\n");
                // -2일
                // 2010-04-02-임종우 : 매월 1일, 2일 경우 3일자 가져온 데이터로 WORK_DATE 사용한다.
                if (strDate2.Substring(6, 2).Equals("01") || strDate2.Substring(6, 2).Equals("02"))
                {
                    strSqlString.Append("                   AND WORK_DATE BETWEEN '" + dayArry2_2[0] + "' AND '" + strDate2 + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("                   AND WORK_DATE BETWEEN '" + lastMonth2 + "' AND '" + strDate2 + "'" + "\n");
                }
                strSqlString.Append("                  AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                  AND LOT_TYPE = 'W' " + "\n");
                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                  AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }
                strSqlString.Append("                GROUP BY MAT_ID " + "\n");
                strSqlString.Append("               ) SHP_2 " + "\n");
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("               SELECT MAT_ID " + "\n");
                strSqlString.AppendFormat("                     , SUM(CASE WHEN WORK_DATE BETWEEN '{0}' AND '{1}'" + "\n", startDate3, strDate3);
                strSqlString.Append("                    THEN SHP_QTY_1 " + "\n");
                strSqlString.Append("                    ELSE 0 END) SHIP_MON " + "\n");
                strSqlString.Append("                 FROM VSUMWIPSHP " + "\n");
                strSqlString.Append("                WHERE 1=1 " + "\n");
                // -3일
                // 2010-04-02-임종우 : 매월 1일, 2일 경우 3일자 가져온 데이터로 WORK_DATE 사용한다.
                if (strDate3.Substring(6, 2).Equals("01") || strDate3.Substring(6, 2).Equals("02"))
                {
                    strSqlString.Append("                   AND WORK_DATE BETWEEN '" + dayArry2_3[0] + "' AND '" + strDate3 + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("                   AND WORK_DATE BETWEEN '" + lastMonth3 + "' AND '" + strDate3 + "'" + "\n");
                }
                strSqlString.Append("                  AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                  AND LOT_TYPE = 'W' " + "\n");
                //1111111111111111
                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                  AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }

                strSqlString.Append("                GROUP BY MAT_ID " + "\n");
                strSqlString.Append("               ) SHP_3 " + "\n");

                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("               SELECT MAT_ID " + "\n");
                strSqlString.AppendFormat("                     , SUM(CASE WHEN WORK_DATE BETWEEN '{0}' AND '{1}'" + "\n", startDate4, strDate4);
                strSqlString.Append("                    THEN SHP_QTY_1 " + "\n");
                strSqlString.Append("                    ELSE 0 END) SHIP_MON " + "\n");
                strSqlString.Append("                 FROM VSUMWIPSHP " + "\n");
                strSqlString.Append("                WHERE 1=1 " + "\n");
                // -4일
                // 2010-04-02-임종우 : 매월 1일, 2일 경우 3일자 가져온 데이터로 WORK_DATE 사용한다.
                if (strDate4.Substring(6, 2).Equals("01") || strDate4.Substring(6, 2).Equals("02"))
                {
                    strSqlString.Append("                   AND WORK_DATE BETWEEN '" + dayArry2_4[0] + "' AND '" + strDate4 + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("                   AND WORK_DATE BETWEEN '" + lastMonth4 + "' AND '" + strDate4 + "'" + "\n");
                }
                strSqlString.Append("                  AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                  AND LOT_TYPE = 'W' " + "\n");
                //1111111111111111

                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                  AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }

                strSqlString.Append("                GROUP BY MAT_ID " + "\n");
                strSqlString.Append("               ) SHP_4 " + "\n");

                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("               SELECT MAT_ID " + "\n");
                strSqlString.AppendFormat("                     , SUM(CASE WHEN WORK_DATE BETWEEN '{0}' AND '{1}'" + "\n", startDate5, strDate5);
                strSqlString.Append("                    THEN SHP_QTY_1 " + "\n");
                strSqlString.Append("                    ELSE 0 END) SHIP_MON " + "\n");



                strSqlString.Append("                 FROM VSUMWIPSHP " + "\n");
                strSqlString.Append("                WHERE 1=1 " + "\n");
                // -5일
                // 2010-04-02-임종우 : 매월 1일, 2일 경우 3일자 가져온 데이터로 WORK_DATE 사용한다.
                if (strDate5.Substring(6, 2).Equals("01") || strDate5.Substring(6, 2).Equals("02"))
                {
                    strSqlString.Append("                   AND WORK_DATE BETWEEN '" + dayArry2_5[0] + "' AND '" + strDate5 + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("                   AND WORK_DATE BETWEEN '" + lastMonth5 + "' AND '" + strDate5 + "'" + "\n");
                }
                strSqlString.Append("                  AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                  AND LOT_TYPE = 'W' " + "\n");
                //1111111111111111

                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                  AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }

                strSqlString.Append("                GROUP BY MAT_ID " + "\n");
                strSqlString.Append("               ) SHP_5 " + "\n");

                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("               SELECT MAT_ID " + "\n");
                strSqlString.AppendFormat("                     , SUM(CASE WHEN WORK_DATE BETWEEN '{0}' AND '{1}'" + "\n", startDate6, strDate6);
                strSqlString.Append("                    THEN SHP_QTY_1 " + "\n");
                strSqlString.Append("                    ELSE 0 END) SHIP_MON " + "\n");



                strSqlString.Append("                 FROM VSUMWIPSHP " + "\n");
                strSqlString.Append("                WHERE 1=1 " + "\n");
                // -6일
                // 2010-04-02-임종우 : 매월 1일, 2일 경우 3일자 가져온 데이터로 WORK_DATE 사용한다.
                if (strDate6.Substring(6, 2).Equals("01") || strDate6.Substring(6, 2).Equals("02"))
                {
                    strSqlString.Append("                   AND WORK_DATE BETWEEN '" + dayArry2_6[0] + "' AND '" + strDate6 + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("                   AND WORK_DATE BETWEEN '" + lastMonth6 + "' AND '" + strDate6 + "'" + "\n");
                }
                strSqlString.Append("                  AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                  AND LOT_TYPE = 'W' " + "\n");
                //1111111111111111

                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                  AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }

                strSqlString.Append("                GROUP BY MAT_ID " + "\n");
                strSqlString.Append("               ) SHP_6 " + "\n");

                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("               SELECT MAT_ID " + "\n");
                strSqlString.AppendFormat("                     , SUM(CASE WHEN WORK_DATE BETWEEN '{0}' AND '{1}'" + "\n", startDate7, strDate7);
                strSqlString.Append("                    THEN SHP_QTY_1 " + "\n");
                strSqlString.Append("                    ELSE 0 END) SHIP_MON " + "\n");



                strSqlString.Append("                 FROM VSUMWIPSHP " + "\n");
                strSqlString.Append("                WHERE 1=1 " + "\n");
                // -7일
                // 2010-04-02-임종우 : 매월 1일, 2일 경우 3일자 가져온 데이터로 WORK_DATE 사용한다.
                if (strDate7.Substring(6, 2).Equals("01") || strDate7.Substring(6, 2).Equals("02"))
                {
                    strSqlString.Append("                   AND WORK_DATE BETWEEN '" + dayArry2_7[0] + "' AND '" + strDate7 + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("                   AND WORK_DATE BETWEEN '" + lastMonth7 + "' AND '" + strDate7 + "'" + "\n");
                }
                strSqlString.Append("                  AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                  AND CM_KEY_2 = 'PROD' " + "\n");
                strSqlString.Append("                  AND LOT_TYPE = 'W' " + "\n");
                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                  AND CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }

                strSqlString.Append("                GROUP BY MAT_ID " + "\n");
                strSqlString.Append("               ) SHP_7 " + "\n");
                strSqlString.Append("         WHERE 1 = 1 " + "\n");
                strSqlString.Append("           AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("           AND PLAN.PLAN_MONTH(+) = '" + month + "' " + "\n");
                strSqlString.Append("           AND PLAN_1.PLAN_MONTH(+) = '" + month1 + "' " + "\n");
                strSqlString.Append("           AND PLAN_2.PLAN_MONTH(+) = '" + month2 + "' " + "\n");
                strSqlString.Append("           AND PLAN_3.PLAN_MONTH(+) = '" + month3 + "' " + "\n");
                strSqlString.Append("           AND PLAN_4.PLAN_MONTH(+) = '" + month4 + "' " + "\n");
                strSqlString.Append("           AND PLAN_5.PLAN_MONTH(+) = '" + month5 + "' " + "\n");
                strSqlString.Append("           AND PLAN_6.PLAN_MONTH(+) = '" + month6 + "' " + "\n");
                strSqlString.Append("           AND PLAN_7.PLAN_MONTH(+) = '" + month7 + "' " + "\n");
                strSqlString.Append("           AND MAT.MAT_TYPE= 'FG' " + "\n");
                //상세 검색 조건
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                {
                    strSqlString.AppendFormat("       AND MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
                }
                else
                {
                    strSqlString.Append("           AND MAT_GRP_1 IN ('SE','HX') " + "\n");
                }
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                {
                    strSqlString.AppendFormat("       AND MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
                }
                else
                {
                    strSqlString.Append("           AND MAT_GRP_1 IN ('SE','HX') " + "\n");
                }
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("                     AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("                     AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("                     AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("                     AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("                     AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("                     AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("                     AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                strSqlString.Append("           AND MAT.DELETE_FLAG <> 'Y' " + "\n");
                strSqlString.Append("           AND MAT.FACTORY =PLAN.FACTORY(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PLAN.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.FACTORY =PLAN_1.FACTORY(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PLAN_1.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.FACTORY =PLAN_2.FACTORY(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PLAN_2.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.FACTORY =PLAN_3.FACTORY(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PLAN_3.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.FACTORY =PLAN_4.FACTORY(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PLAN_4.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.FACTORY =PLAN_5.FACTORY(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PLAN_5.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.FACTORY =PLAN_6.FACTORY(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PLAN_6.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.FACTORY =PLAN_7.FACTORY(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = PLAN_7.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = SHP.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = SHP_1.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = SHP_2.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = SHP_3.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = SHP_4.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = SHP_5.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = SHP_6.MAT_ID(+) " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = SHP_7.MAT_ID(+) " + "\n");
                strSqlString.Append("      GROUP BY MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7, MAT.MAT_CMF_13" + "\n");
                strSqlString.Append("     ) A  " + "\n");
                strSqlString.Append("     , MWIPMATDEF C " + "\n");
                strSqlString.Append(" WHERE 1 = 1 " + "\n");
                strSqlString.Append("   AND C.FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("   AND A.MAT_ID = C.MAT_ID(+)" + "\n");
                strSqlString.Append("   AND C.MAT_GRP_3 <> 'COB'" + "\n");
                strSqlString.Append("   GROUP BY C.MAT_GRP_1 , C.MAT_GRP_3" + "\n");
                strSqlString.Append("   HAVING (" + "\n");
                strSqlString.Append("   NVL(SUM(A.MON_PLAN), 0)+ " + "\n");
                strSqlString.Append("   NVL(SUM(A.SHIP_MON), 0) " + "\n");
                strSqlString.Append("   ) <> 0 " + "\n");
                strSqlString.Append("    ORDER BY C.MAT_GRP_1 ) A " + "\n");
                strSqlString.Append("   GROUP BY A.CUSTOMER" + "\n");

            }
            */
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
                spdData.RPT_FillDataSelectiveCells("목표", 0, 1, 0, 1, true, Align.Center, VerticalAlign.Center);

                // 목표
                spdData.ActiveSheet.Cells[0, 1].Value = 100;
                spdData.ActiveSheet.Cells[0, 2].Value = 100;
                spdData.ActiveSheet.Cells[0, 3].Value = 100;
                spdData.ActiveSheet.Cells[0, 4].Value = 100;
                spdData.ActiveSheet.Cells[0, 5].Value = 100;
                spdData.ActiveSheet.Cells[0, 6].Value = 100;
                spdData.ActiveSheet.Cells[0, 7].Value = 100;
                spdData.ActiveSheet.Cells[0, 8].Value = 100;
                
                /*
                //Totla의 평균 값
                SetAvgVertical2(1);
                SetAvgVertical2(2);
                SetAvgVertical2(3);
                SetAvgVertical2(4);
                SetAvgVertical2(5);
                SetAvgVertical2(6);
                SetAvgVertical2(7);
                SetAvgVertical2(8);
                */

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




        /// <summary>
        /// 2009.10.15 임종우
        /// AVG 구하기.. SubTotal, GrandTotal 구할때 특정 컬럼 내용들로 직접 구할때..
        /// </summary>
        /// <param name="nSampleNormalRowPos"></param>
        /// <param name="nColPos"></param>
        public void SetAvgVertical2(int nColPos)
        {
            double sum = 0;
            double divide = 0;


            for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                sum += Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos].Value);
                divide += 1;
            }
            spdData.ActiveSheet.Cells[0, nColPos].Value = sum / divide;
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

                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                udcChartFX1.RPT_1_ChartInit();
                udcChartFX1.RPT_2_ClearData();
                udcChartFX1.AxisY.Font = new System.Drawing.Font("Tahoma", 10.25F);
                udcChartFX1.AxisX.Font = new System.Drawing.Font("Tahoma", 10.25F);
                if (cdvFactory.Text == "HMKA1")
                {
                    udcChartFX1.AxisX.Title.Text = "ASSY 달성율현황";
                    udcChartFX1.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 13.25F);
                }
                else if (cdvFactory.Text == "HMKT1")
                {
                    udcChartFX1.AxisX.Title.Text = "TEST 달성율현황";
                }
                else
                {
                    udcChartFX1.AxisX.Title.Text = "달성율현황";
                }
                udcChartFX1.AxisY.Title.Text = "달성율";

                udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Lines;
                udcChartFX1.AxisY.DataFormat.Format = 0;
                udcChartFX1.AxisY.DataFormat.Decimals = 2;
                // Y축의 범위는 80~130이고 5씩 증가한다.
                              
                udcChartFX1.AxisY.Step = 5;
                //udcChartFX1.AxisY.Max = 130;
                udcChartFX1.AxisY.Min = 70;
                udcChartFX1.LineWidth = 2;

                


                udcChartFX1.AxisX.Staggered = false;
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

                udcChartFX1.RPT_3_OpenData(irows_mm.Length, icols_mm.Length);
                double max1 = udcChartFX1.RPT_4_AddData(SS, irows_mm, icols_mm, SeriseType.Rows);
                udcChartFX1.RPT_5_CloseData();
                udcChartFX1.RPT_6_SetGallery(ChartType.Line, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.05);
                udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, 1);
                udcChartFX1.RPT_8_SetSeriseLegend(stitle_mm, SoftwareFX.ChartFX.Docked.Top);
             
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

                ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);
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
