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

namespace Hana.PRD
{
    public partial class PRD010214 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010214<br/>
        /// 클래스요약: S-LSI 일일 보고<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2012-11-15<br/>
        /// 상세  설명: S-LSI 일일 보고<br/>
        /// 변경  내용: <br/>
        /// 2012-11-29-임종우 : Partial Ship 로직 추가, 누계 차이 부분을 그냥 차이로 변경 (김은석 요청)
        /// 2012-12-06-임종우 : 예상값 DB에 가져오는 부분 추가

        /// </summary>

        DataTable DateDT = null;        

        public PRD010214()
        {
            InitializeComponent();
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();

            this.SetFactory(GlobalVariable.gsTestDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsTestDefaultFactory;

            //udcWIPCondition1.Text = "SE";
            //udcWIPCondition1.Enabled = false;
        }

        #region 유효성검사 및 초기화
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
            GetDayArray();
            spdData.RPT_ColumnInit();            

            try
            {
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LD Count", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 130);
                spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 180);
                spdData.RPT_AddBasicColumn("Cust Device", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 180);

                spdData.RPT_AddBasicColumn("Classification", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);               

                spdData.RPT_AddBasicColumn(DateDT.Rows[0][1].ToString(), 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("월", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(DateDT.Rows[1][1].ToString(), 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Tue.", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(DateDT.Rows[2][1].ToString(), 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Wed.", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(DateDT.Rows[3][1].ToString(), 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Thur.", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(DateDT.Rows[4][1].ToString(), 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Fri.", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(DateDT.Rows[5][1].ToString(), 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Sat.", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(DateDT.Rows[6][1].ToString(), 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("일", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("TOTAL", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 8, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 9, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 10, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 11, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 19, 2);

                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "B.MAT_GRP_1", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = B.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "B.MAT_GRP_2", "MAT_GRP_2", "B.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "B.MAT_GRP_6", "MAT_GRP_6", "B.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "B.MAT_GRP_3", "MAT_GRP_3", "B.MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "B.MAT_GRP_4", "MAT_GRP_4", "B.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "B.MAT_GRP_5", "MAT_GRP_5", "B.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "B.MAT_GRP_7", "MAT_GRP_7", "B.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "B.MAT_GRP_8", "MAT_GRP_8", "B.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "B.MAT_CMF_10", "MAT_CMF_10", "B.MAT_CMF_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "B.MAT_ID", "MAT_ID", "B.MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "B.MAT_CMF_7", "MAT_CMF_7", "B.MAT_CMF_7", false);
        }
        #endregion


        #region 금주 월요일부터 일요일 일자 가져오기
        private void GetDayArray()
        {            
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT SYS_DATE, SUBSTR(SYS_DATE, 5, 2)||'월 '||SUBSTR(SYS_DATE, 7, 2)||'일' AS CONV");
            strSqlString.Append("  FROM MWIPCALDEF");
            strSqlString.Append(" WHERE CALENDAR_ID = 'SE'");
            strSqlString.Append("   AND (PLAN_YEAR, PLAN_WEEK) = (SELECT PLAN_YEAR, PLAN_WEEK FROM MWIPCALDEF WHERE CALENDAR_ID = 'SE' AND SYS_DATE = '" + cdvDate.Value.ToString("yyyyMMdd") + "') ");
            strSqlString.Append(" ORDER BY SYS_DATE");

            DateDT = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
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
            string QueryCond3;                    
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;            
                        
            strSqlString.Append("WITH DT AS (" + "\n");
            strSqlString.Append("SELECT SYS_DATE, MAT_ID" + "\n");
            strSqlString.Append("     , SHP_QTY, SUM_SHP_QTY, PLN_QTY, SUM_PLN_QTY" + "\n");
            strSqlString.Append("     , SHP_QTY - PLN_QTY AS DEF_QTY" + "\n");
            // 2012-11-29-임종우 : 누계차이 부분 사용 안함 (김은석 요청)
            //strSqlString.Append("     , SUM_SHP_QTY - SUM_PLN_QTY AS DEF_QTY" + "\n");
            strSqlString.Append("     , EXP_QTY" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT SYS_DATE" + "\n");
            strSqlString.Append("             , A.MAT_ID" + "\n");
            strSqlString.Append("             , NVL(SHP_QTY,0) AS SHP_QTY" + "\n");
            strSqlString.Append("             , SUM(NVL(SHP_QTY,0)) OVER(PARTITION BY A.MAT_ID ORDER BY SYS_DATE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS SUM_SHP_QTY     " + "\n");
            strSqlString.Append("             , NVL(PLN_QTY,0) AS PLN_QTY" + "\n");
            strSqlString.Append("             , SUM(NVL(PLN_QTY,0)) OVER(PARTITION BY A.MAT_ID ORDER BY SYS_DATE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS SUM_PLN_QTY " + "\n");
            strSqlString.Append("             , NVL(D.EXP_QTY,0) AS EXP_QTY" + "\n");            
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT SYS_DATE, MAT_ID" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT *" + "\n");
            strSqlString.Append("                          FROM MWIPMATDEF" + "\n");
            strSqlString.Append("                         WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND MAT_ID LIKE 'SES%'" + "\n");
            strSqlString.Append("                           AND DELETE_FLAG = ' '" + "\n");

            //상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.Append("                       ) A" + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT *" + "\n");
            strSqlString.Append("                          FROM MWIPCALDEF" + "\n");
            strSqlString.Append("                         WHERE CALENDAR_ID = 'SE'" + "\n");
            strSqlString.Append("                           AND (PLAN_YEAR, PLAN_WEEK) = (SELECT PLAN_YEAR, PLAN_WEEK FROM MWIPCALDEF WHERE CALENDAR_ID = 'SE' AND SYS_DATE = '" + cdvDate.Value.ToString("yyyyMMdd") + "') " + "\n");
            strSqlString.Append("                       ) B" + "\n");
            strSqlString.Append("               ) A" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT WORK_DATE" + "\n");
            strSqlString.Append("                     , MAT_ID" + "\n");
            strSqlString.Append("                     , SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) AS SHP_QTY       " + "\n");
            strSqlString.Append("                  FROM RSUMFACMOV" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND WORK_DATE BETWEEN '" + DateDT.Rows[0][0].ToString() + "' AND '" + DateDT.Rows[6][0].ToString() + "'" + "\n");
            strSqlString.Append("                   AND FACTORY NOT IN ('RETURN','" + GlobalVariable.gsAssyDefaultFactory + "') " + "\n");
            strSqlString.Append("                   AND MAT_ID LIKE 'SES%'" + "\n");
            strSqlString.Append("                 GROUP BY WORK_DATE, MAT_ID" + "\n");
            strSqlString.Append("               ) B" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT PLAN_DAY, MAT_ID, SUM(PLAN_QTY) AS PLN_QTY" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT PLAN_DAY, MAT_ID, PLAN_QTY" + "\n");
            strSqlString.Append("                          FROM CWIPPLNDAY" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND PLAN_DAY BETWEEN '" + DateDT.Rows[0][0].ToString() + "' AND '" + DateDT.Rows[6][0].ToString() + "'" + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("                         UNION" + "\n");
            strSqlString.Append("                        SELECT PLAN_DAY, MAT_ID, PLAN_QTY" + "\n");
            strSqlString.Append("                          FROM CWIPPLNSNP@RPTTOMES " + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND SNAPSHOT_DAY BETWEEN '" + DateDT.Rows[0][0].ToString() + "' AND '" + DateDT.Rows[6][0].ToString() + "'" + "\n");
            strSqlString.Append("                           AND PLAN_DAY = SNAPSHOT_DAY" + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY PLAN_DAY, MAT_ID" + "\n");
            strSqlString.Append("               ) C" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT PLAN_DATE, MAT_ID, SUM(QTY_1) AS EXP_QTY" + "\n");
            strSqlString.Append("                  FROM CPLNSHPDAY@RPTTOMES" + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND PLAN_DATE BETWEEN '" + DateDT.Rows[0][0].ToString() + "' AND '" + DateDT.Rows[6][0].ToString() + "'" + "\n");
            strSqlString.Append("                   GROUP BY PLAN_DATE, MAT_ID" + "\n");         
            strSqlString.Append("               ) D" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND A.MAT_ID = C.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND A.MAT_ID = D.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND A.SYS_DATE = B.WORK_DATE(+)" + "\n");
            strSqlString.Append("           AND A.SYS_DATE = C.PLAN_DAY(+)" + "\n");
            strSqlString.Append("           AND A.SYS_DATE = D.PLAN_DATE(+)" + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(") " + "\n");
            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond3);
            strSqlString.Append("     , RN" + "\n");
            strSqlString.Append("     , SUM(D0) AS D0 " + "\n");
            strSqlString.Append("     , SUM(D1) AS D1 " + "\n");
            strSqlString.Append("     , SUM(D2) AS D2 " + "\n");
            strSqlString.Append("     , SUM(D3) AS D3 " + "\n");
            strSqlString.Append("     , SUM(D4) AS D4 " + "\n");
            strSqlString.Append("     , SUM(D5) AS D5 " + "\n");
            strSqlString.Append("     , SUM(D6) AS D6 " + "\n");
            strSqlString.Append("     , SUM(TTL) AS TTL " + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT '계획' AS RN" + "\n");
            strSqlString.Append("             , MAT_ID" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[0][0].ToString() + "', PLN_QTY)) AS D0" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[1][0].ToString() + "', PLN_QTY)) AS D1" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[2][0].ToString() + "', PLN_QTY)) AS D2" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[3][0].ToString() + "', PLN_QTY)) AS D3" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[4][0].ToString() + "', PLN_QTY)) AS D4" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[5][0].ToString() + "', PLN_QTY)) AS D5" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[6][0].ToString() + "', PLN_QTY)) AS D6" + "\n");
            strSqlString.Append("             , SUM(PLN_QTY) AS TTL " + "\n");
            strSqlString.Append("          FROM DT" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("         UNION ALL" + "\n");
            strSqlString.Append("        SELECT '실적' AS RN" + "\n");
            strSqlString.Append("             , MAT_ID" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[0][0].ToString() + "', SHP_QTY)) AS D0" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[1][0].ToString() + "', SHP_QTY)) AS D1" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[2][0].ToString() + "', SHP_QTY)) AS D2" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[3][0].ToString() + "', SHP_QTY)) AS D3" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[4][0].ToString() + "', SHP_QTY)) AS D4" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[5][0].ToString() + "', SHP_QTY)) AS D5" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[6][0].ToString() + "', SHP_QTY)) AS D6" + "\n");
            strSqlString.Append("             , SUM(SHP_QTY) AS TTL " + "\n");
            strSqlString.Append("          FROM DT" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("         UNION ALL" + "\n");
            strSqlString.Append("        SELECT '차이(누적)' AS RN" + "\n");
            strSqlString.Append("             , MAT_ID" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[0][0].ToString() + "', DEF_QTY)) AS D0" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[1][0].ToString() + "', DEF_QTY)) AS D1" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[2][0].ToString() + "', DEF_QTY)) AS D2" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[3][0].ToString() + "', DEF_QTY)) AS D3" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[4][0].ToString() + "', DEF_QTY)) AS D4" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[5][0].ToString() + "', DEF_QTY)) AS D5" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[6][0].ToString() + "', DEF_QTY)) AS D6" + "\n");
            strSqlString.Append("             , SUM(DEF_QTY) AS TTL " + "\n");
            strSqlString.Append("          FROM DT" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID    " + "\n");
            strSqlString.Append("         UNION ALL" + "\n");
            strSqlString.Append("        SELECT '예상' AS RN" + "\n");
            strSqlString.Append("             , MAT_ID" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[0][0].ToString() + "', EXP_QTY)) AS D0" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[1][0].ToString() + "', EXP_QTY)) AS D1" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[2][0].ToString() + "', EXP_QTY)) AS D2" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[3][0].ToString() + "', EXP_QTY)) AS D3" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[4][0].ToString() + "', EXP_QTY)) AS D4" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[5][0].ToString() + "', EXP_QTY)) AS D5" + "\n");
            strSqlString.Append("             , SUM(DECODE(SYS_DATE, '" + DateDT.Rows[6][0].ToString() + "', EXP_QTY)) AS D6" + "\n");
            strSqlString.Append("             , SUM(EXP_QTY) AS TTL " + "\n");
            strSqlString.Append("          FROM DT" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID    " + "\n");
            strSqlString.Append("       ) A" + "\n");
            strSqlString.Append("     , MWIPMATDEF B" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("   AND A.MAT_ID IN (SELECT DISTINCT MAT_ID FROM DT WHERE DEF_QTY <> 0)" + "\n");
            strSqlString.Append("   AND B.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat(" GROUP BY {0}, RN" + "\n", QueryCond1);
            strSqlString.AppendFormat(" ORDER BY {0}, DECODE(RN, '계획', 1, '실적', 2, '차이(누적)', 3, 4)" + "\n", QueryCond1);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }               

        #endregion


        #region EVENT 처리
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;            

            StringBuilder strSqlString = new StringBuilder();      

            if (CheckField() == false) return;

            GridColumnInit();            

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
                                
                spdData.DataSource = dt;
                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 11, null, null, btnSort);

                ////Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 1, true, Align.Center, VerticalAlign.Center);
                                
                spdData.RPT_AutoFit(false);

                for (int i = 0; i <= 11; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LemonChiffon;
                }
                // 당일 잔량 대비 재공 음영표시
                //for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                //{
                //    int sum = 0;
                //    int value = 0;

                //    if (spdData.ActiveSheet.Cells[i, 36].BackColor.IsEmpty)
                //    {
                //        if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, 36].Value) > 0)
                //        {
                //            for (int y = 33; y >= 25; y--)
                //            {
                //                value = Convert.ToInt32(spdData.ActiveSheet.Cells[i, y].Value);
                //                sum += value;

                //                if (Convert.ToInt32(spdData.ActiveSheet.Cells[i, 36].Value) > sum)
                //                {
                //                    spdData.ActiveSheet.Cells[i, y].ForeColor = Color.Red;
                //                    spdData.ActiveSheet.Cells[i, y].BackColor = Color.Pink;
                //                }
                //                else
                //                {
                //                    spdData.ActiveSheet.Cells[i, y].ForeColor = Color.Blue;
                //                    spdData.ActiveSheet.Cells[i, y].BackColor = Color.LightGreen;
                //                    break;
                //                }
                //            }
                //        }
                //    }
                //}

                dt.Dispose();

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
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.ActiveSheet.Rows.Count > 0)
            {
                StringBuilder Condition = new StringBuilder();

                ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null, true);

            }
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            //spdData.ExportExcel();            
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);         
        }
        #endregion
    }
}
