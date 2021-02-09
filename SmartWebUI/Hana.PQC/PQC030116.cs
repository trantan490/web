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

namespace Hana.PQC
{
    public partial class PQC030116 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {


        /// <summary>
        /// 클  래  스: PQC030116<br/>
        /// 클래스요약: 제품별 공정불량현황2(YIELD&LOSS TREND) <br/>
        /// 작  성  자: 김민우<br/>
        /// 최초작성일: 2010-10-15<br/>
        /// 상세  설명: YIELD&LOSS TREND<br/>
        /// 변경  내용: <br/> 
        /// 2010-12-18-임종우 : 월별 검색시 주차 시작일, 종료일 가져오는 부분 사용으로 인한 에러 수정. 
        /// 2011-05-16-김민우 : 검색 조건에 Detail 추가(품질팀 이광문 요청))
        
        private String[] dayArry = new String[7];

        /// </summary>
        public PQC030116()
        {
            InitializeComponent();
            SortInit();

            GridColumnInit();
            cdvFromToDate.AutoBinding();
            cdvFromToDate.DaySelector.SelectedValue = "DAY";
            if(!cdvFromToDate.ToWeek.Text.Equals("1"))
            {
                cdvFromToDate.FromWeek.Text = (Convert.ToInt64(cdvFromToDate.ToWeek.Text)-1) +"";
            }
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            cdvOper.sFactory = cdvFactory.txtValue;
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();
            spdData2.RPT_ColumnInit();

            LabelTextChange();

            String ss = DateTime.Now.ToString("MM-dd");
            string[] strDate = cdvFromToDate.getSelectDate();


            try
            {
                // 전체 공정
                spdData.RPT_AddBasicColumn("DATE", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("IN_QTY", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("OUT_QTY", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("YIELD", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.Double2, 50);
                spdData.RPT_AddBasicColumn("LOSS", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Defective rate", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.Double2, 50);
                spdData.RPT_AddBasicColumn("LOSS_CODE_1", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("LOSS_1", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 50);
                spdData.RPT_AddBasicColumn("LOSS_CODE_2", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("LOSS_2", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 50);
                spdData.RPT_AddBasicColumn("LOSS_CODE_3", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("LOSS_3", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 50);
                spdData.RPT_AddBasicColumn("LOSS_CODE_4", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("LOSS_4", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 50);
                spdData.RPT_AddBasicColumn("LOSS_CODE_5", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("LOSS_5", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 50);
                spdData.RPT_AddBasicColumn("LOSS_CODE_ETC", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("LOSS_ETC", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 50);
                // 공정별 LOSS
                spdData2.RPT_AddBasicColumn("DATE", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData2.RPT_AddBasicColumn("OPER", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData2.RPT_AddBasicColumn("LOSS_CODE_1", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData2.RPT_AddBasicColumn("LOSS_1", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 50);
                spdData2.RPT_AddBasicColumn("LOSS_CODE_2", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData2.RPT_AddBasicColumn("LOSS_2", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 50);
                spdData2.RPT_AddBasicColumn("LOSS_CODE_3", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData2.RPT_AddBasicColumn("LOSS_3", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 50);
                spdData2.RPT_AddBasicColumn("LOSS_CODE_4", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData2.RPT_AddBasicColumn("LOSS_4", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 50);
                spdData2.RPT_AddBasicColumn("LOSS_CODE_5", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData2.RPT_AddBasicColumn("LOSS_5", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 50);
                spdData2.RPT_AddBasicColumn("LOSS_CODE_ETC", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData2.RPT_AddBasicColumn("LOSS_ETC", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 50);

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

            string FromDate = Convert.ToDateTime(cdvFromToDate.FromDate.Text).AddDays(-1).ToString("yyyyMMdd");

            string[] strDate = cdvFromToDate.getSelectDate();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            // 2010-12-18-임종우 : 월별 검색시 53주차 발생하여 에러 발생 함. 주별 검색시에만 사용으로 변경 함.
            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                GetFirstDay();
                GetLastDay();
            }

            // 공정 선택 유무로 보여지는 화면이 다르기 때문에 그에 따른 쿼리문도 다름
            if (cdvOper.Text.TrimEnd() == "ALL")
            {
                strSqlString.Append("SELECT A.*, B.LOSS_CODE_1, B.TOP_1, B.LOSS_CODE_2, B.TOP_2, B.LOSS_CODE_3, B.TOP_3, B.LOSS_CODE_4, B.TOP_4, B.LOSS_CODE_5, B.TOP_5, B.LOSS_CODE_ETC, B.TOP_ETC" + "\n");
                strSqlString.Append("  FROM" + "\n");
                strSqlString.Append("     (" + "\n");
                strSqlString.Append("     SELECT WORK_DATE" + "\n");
                strSqlString.Append("          , SUM(IN_QTY) AS IN_QTY, SUM(OUT_QTY) AS OUT_QTY, ROUND(SUM(OUT_QTY)/SUM(IN_QTY) * 100,2) AS YIELD " + "\n");
                strSqlString.Append("          , SUM(LOSS_QTY) AS LOSS , ROUND(SUM(LOSS_QTY)/SUM(IN_QTY) * 100,2) AS LOSS_YIELD" + "\n");
                strSqlString.Append("       FROM" + "\n");
                strSqlString.Append("          (" + "\n");

                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    strSqlString.Append("          SELECT SUBSTR(B.TRAN_TIME,0,6) AS WORK_DATE, B.LOT_ID,B.SHIP_QTY_1 + NVL(SUM(LOSS_QTY),0) AS IN_QTY, B.SHIP_QTY_1 AS OUT_QTY" + "\n");
                }
                else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
                {
                    strSqlString.Append("          SELECT SYS_YEAR||PLAN_WEEK AS WORK_DATE, B.LOT_ID,B.SHIP_QTY_1 + NVL(SUM(LOSS_QTY),0) AS IN_QTY, B.SHIP_QTY_1 AS OUT_QTY" + "\n");
                }
                else
                {
                    strSqlString.Append("          SELECT SUBSTR(B.TRAN_TIME,0,8) AS WORK_DATE, B.LOT_ID,B.SHIP_QTY_1 + NVL(SUM(LOSS_QTY),0) AS IN_QTY, B.SHIP_QTY_1 AS OUT_QTY" + "\n");
                }

                strSqlString.Append("               , NVL(SUM(LOSS_QTY),0) AS LOSS_QTY" + "\n");
                strSqlString.Append("            FROM" + "\n");
                strSqlString.Append("               (" + "\n");
                strSqlString.Append("               SELECT * FROM RWIPLOTLSM" + "\n");
                strSqlString.Append("                WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("               ) A ," + "\n");
                strSqlString.Append("               (" + "\n");
                strSqlString.Append("               SELECT *" + "\n");
                strSqlString.Append("                 FROM VWIPSHPLOT LOT, MWIPCALDEF, MWIPMATDEF@RPTTOMES MAT" + "\n");
                strSqlString.Append("                WHERE FROM_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                  AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                  AND LOT.FROM_FACTORY = MAT.FACTORY " + "\n");
                strSqlString.Append("                  AND LOT.MAT_ID = MAT.MAT_ID " + "\n");
                strSqlString.Append("                  AND CALENDAR_ID = 'HM' " + "\n");
                strSqlString.Append("                  AND SUBSTR(TRAN_TIME,0,8) = SYS_DATE " + "\n");
                strSqlString.Append("                  AND LOT_TYPE = 'W'" + "\n");

                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    strSqlString.Append("                  AND TRAN_TIME BETWEEN '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "01000000" + "' AND '" + cdvFromToDate.ToYearMonth.Value.ToString("yyyyMM") + "31235959" + "'" + "\n");
                }
                else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
                {
                    strSqlString.Append("                  AND TRAN_TIME BETWEEN '" + dayArry[0] + "000000" + "' AND '" + dayArry[1] + "235959" + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("                  AND TRAN_TIME BETWEEN '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "000000" + "' AND '" + cdvFromToDate.ToDate.Text.Replace("-", "") + "235959" + "'" + "\n");
                }
                strSqlString.Append("                  AND LOT.MAT_ID LIKE '" + cdvCustomer.Text + "%' " + "\n");
                strSqlString.Append("                  AND FROM_OPER = 'AZ010'" + "\n");
                strSqlString.Append("                  AND MAT.MAT_VER = 1" + "\n");

                #region 상세 조회에 따른 SQL문 생성
                
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                #endregion
                strSqlString.Append("               ) B" + "\n");
                strSqlString.Append("           WHERE A.LOT_ID(+) = B.LOT_ID" + "\n");

                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    strSqlString.Append("          GROUP BY SUBSTR(B.TRAN_TIME,0,6) , B.LOT_ID ,B.SHIP_QTY_1" + "\n");
                    strSqlString.Append("          ORDER BY SUBSTR(B.TRAN_TIME,0,6)" + "\n");
                }
                else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
                {
                    strSqlString.Append("          GROUP BY B.SYS_YEAR||B.PLAN_WEEK , B.LOT_ID ,B.SHIP_QTY_1" + "\n");
                    strSqlString.Append("          ORDER BY B.SYS_YEAR||B.PLAN_WEEK" + "\n");
                }
                else
                {
                    strSqlString.Append("          GROUP BY SUBSTR(B.TRAN_TIME,0,8) , B.LOT_ID ,B.SHIP_QTY_1" + "\n");
                    strSqlString.Append("          ORDER BY SUBSTR(B.TRAN_TIME,0,8)" + "\n");
                }
                strSqlString.Append("          )" + "\n");
                strSqlString.Append("     GROUP BY WORK_DATE" + "\n");
                strSqlString.Append("     ORDER BY WORK_DATE" + "\n");
                strSqlString.Append("     ) A," + "\n");
                strSqlString.Append("     (" + "\n");
                strSqlString.Append("     SELECT WORK_DATE" + "\n");
                strSqlString.Append("          , MAX(DECODE(NUM,1, LOSS_CODE||' '||LOSS_DESC,0)) AS LOSS_CODE_1" + "\n");
                strSqlString.Append("          , SUM(DECODE(NUM,1,QTY,0)) AS TOP_1" + "\n");
                strSqlString.Append("          , MAX(DECODE(NUM,2, LOSS_CODE||' '||LOSS_DESC,0)) AS LOSS_CODE_2" + "\n");
                strSqlString.Append("          , SUM(DECODE(NUM,2,QTY,0)) AS TOP_2" + "\n");
                strSqlString.Append("          , MAX(DECODE(NUM,3, LOSS_CODE||' '||LOSS_DESC,0)) AS LOSS_CODE_3" + "\n");
                strSqlString.Append("          , SUM(DECODE(NUM,3,QTY,0)) AS TOP_3" + "\n");
                strSqlString.Append("          , MAX(DECODE(NUM,4, LOSS_CODE||' '||LOSS_DESC,0)) AS LOSS_CODE_4" + "\n");
                strSqlString.Append("          , SUM(DECODE(NUM,4,QTY,0)) AS TOP_4" + "\n");
                strSqlString.Append("          , MAX(DECODE(NUM,5, LOSS_CODE||' '||LOSS_DESC,0)) AS LOSS_CODE_5" + "\n");
                strSqlString.Append("          , SUM(DECODE(NUM,5,QTY,0)) AS TOP_5" + "\n");
                strSqlString.Append("          , 'ETC' AS LOSS_CODE_ETC" + "\n");
                strSqlString.Append("          , SUM(DECODE(SIGN(NUM-5),1,QTY,0)) AS TOP_ETC" + "\n");
                strSqlString.Append("       FROM" + "\n");
                strSqlString.Append("          (" + "\n");
                strSqlString.Append("          SELECT ROW_NUMBER() OVER(PARTITION BY WORK_DATE ORDER BY QTY DESC) AS NUM, A.*, B.LOSS_DESC" + "\n");
                strSqlString.Append("            FROM" + "\n");
                strSqlString.Append("               (" + "\n");

                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    strSqlString.Append("               SELECT SUBSTR(B.TRAN_TIME,0,6) AS WORK_DATE, LOSS_CODE, SUM(NVL(LOSS_QTY,0)) AS QTY" + "\n");
                }
                else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
                {
                    strSqlString.Append("               SELECT SYS_YEAR||PLAN_WEEK AS WORK_DATE, LOSS_CODE, SUM(NVL(LOSS_QTY,0)) AS QTY" + "\n");
                }
                else
                {
                    strSqlString.Append("               SELECT SUBSTR(B.TRAN_TIME,0,8) AS WORK_DATE, LOSS_CODE, SUM(NVL(LOSS_QTY,0)) AS QTY" + "\n");
                }

                strSqlString.Append("                 FROM RWIPLOTLSM A ," + "\n");
                strSqlString.Append("                      (" + "\n");
                strSqlString.Append("                      SELECT *" + "\n");
                strSqlString.Append("                        FROM VWIPSHPLOT LOT, MWIPCALDEF, MWIPMATDEF@RPTTOMES MAT" + "\n");
                strSqlString.Append("                       WHERE FROM_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                         AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                         AND LOT.FROM_FACTORY = MAT.FACTORY" + "\n");
                strSqlString.Append("                         AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
                strSqlString.Append("                         AND CALENDAR_ID = 'HM'" + "\n");
                strSqlString.Append("                         AND SUBSTR(TRAN_TIME,0,8) = SYS_DATE" + "\n");
                strSqlString.Append("                         AND LOT_TYPE = 'W'" + "\n");
                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    strSqlString.Append("                         AND TRAN_TIME BETWEEN '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "01000000" + "' AND '" + cdvFromToDate.ToYearMonth.Value.ToString("yyyyMM") + "31235959" + "'" + "\n");
                }
                else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
                {
                    strSqlString.Append("                         AND TRAN_TIME BETWEEN '" + dayArry[0] + "000000" + "' AND '" + dayArry[1] + "235959" + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("                         AND TRAN_TIME BETWEEN '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "000000" + "' AND '" + cdvFromToDate.ToDate.Text.Replace("-", "") + "235959" + "'" + "\n");
                }
                strSqlString.Append("                         AND LOT.MAT_ID LIKE '" + cdvCustomer.Text + "%' " + "\n");
                strSqlString.Append("                         AND FROM_OPER = 'AZ010'" + "\n");
                strSqlString.Append("                         AND MAT.MAT_VER = 1" + "\n");
                #region 상세 조회에 따른 SQL문 생성
                
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                #endregion
                strSqlString.Append("                       ) B" + "\n");
                strSqlString.Append("                WHERE A.LOT_ID(+) = B.LOT_ID" + "\n");
                strSqlString.Append("                  AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");

                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    strSqlString.Append("                GROUP BY SUBSTR(B.TRAN_TIME,0,6), LOSS_CODE" + "\n");
                    strSqlString.Append("                ORDER BY  SUBSTR(B.TRAN_TIME,0,6), QTY DESC" + "\n");
                }
                else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
                {
                    strSqlString.Append("          GROUP BY B.SYS_YEAR||B.PLAN_WEEK, LOSS_CODE" + "\n");
                    strSqlString.Append("          ORDER BY B.SYS_YEAR||B.PLAN_WEEK, QTY DESC" + "\n");
                }
                else
                {
                    strSqlString.Append("                GROUP BY SUBSTR(B.TRAN_TIME,0,8), LOSS_CODE" + "\n");
                    strSqlString.Append("                ORDER BY SUBSTR(B.TRAN_TIME,0,8), QTY DESC" + "\n");
                }

                strSqlString.Append("                ) A," + "\n");
                strSqlString.Append("                (" + "\n");
                strSqlString.Append("SELECT KEY_1 AS LOSS_CODE" + "\n");
                strSqlString.Append(", DATA_1 AS LOSS_DESC" + "\n");
                strSqlString.Append(", DATA_5 AS LOSS_OPER_DESC" + "\n");
                strSqlString.Append("FROM MGCMTBLDAT" + "\n");
                strSqlString.Append("WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("AND TABLE_NAME = 'LOSS_CODE'" + "\n");
                //strSqlString.Append("AND DATA_1 <> 'ETC'" + "\n");
                //strSqlString.Append("AND DATA_2 <> 'Y'" + "\n");
                strSqlString.Append("                 ) B" + "\n");
                strSqlString.Append("          WHERE A.LOSS_CODE = B.LOSS_CODE" + "\n");
                strSqlString.Append("          )" + "\n");
                strSqlString.Append("          GROUP BY WORK_DATE" + "\n");
                strSqlString.Append("     ) B" + "\n");
                strSqlString.Append("WHERE A.WORK_DATE = B.WORK_DATE" + "\n");
                strSqlString.Append("ORDER BY A.WORK_DATE" + "\n");
            }
            // 공정 선택시( IN,OUT 수량을 제외한 LOSS 정보만 표기
            else
            {

                strSqlString.Append("     SELECT WORK_DATE, OPER" + "\n");
                strSqlString.Append("          , MAX(DECODE(NUM,1, LOSS_CODE||' '||LOSS_DESC,0)) AS LOSS_CODE_1" + "\n");
                strSqlString.Append("          , SUM(DECODE(NUM,1,QTY,0)) AS TOP_1" + "\n");
                strSqlString.Append("          , MAX(DECODE(NUM,2, LOSS_CODE||' '||LOSS_DESC,0)) AS LOSS_CODE_2" + "\n");
                strSqlString.Append("          , SUM(DECODE(NUM,2,QTY,0)) AS TOP_2" + "\n");
                strSqlString.Append("          , MAX(DECODE(NUM,3, LOSS_CODE||' '||LOSS_DESC,0)) AS LOSS_CODE_3" + "\n");
                strSqlString.Append("          , SUM(DECODE(NUM,3,QTY,0)) AS TOP_3" + "\n");
                strSqlString.Append("          , MAX(DECODE(NUM,4, LOSS_CODE||' '||LOSS_DESC,0)) AS LOSS_CODE_4" + "\n");
                strSqlString.Append("          , SUM(DECODE(NUM,4,QTY,0)) AS TOP_4" + "\n");
                strSqlString.Append("          , MAX(DECODE(NUM,5, LOSS_CODE||' '||LOSS_DESC,0)) AS LOSS_CODE_5" + "\n");
                strSqlString.Append("          , SUM(DECODE(NUM,5,QTY,0)) AS TOP_5" + "\n");
                strSqlString.Append("          , 'ETC' AS LOSS_CODE_ETC" + "\n");
                strSqlString.Append("          , SUM(DECODE(SIGN(NUM-5),1,QTY,0)) AS TOP_ETC" + "\n");
                strSqlString.Append("       FROM" + "\n");
                strSqlString.Append("          (" + "\n");
                strSqlString.Append("          SELECT ROW_NUMBER() OVER(PARTITION BY WORK_DATE, OPER ORDER BY QTY DESC) AS NUM, A.*, B.LOSS_DESC" + "\n");
                strSqlString.Append("            FROM" + "\n");
                strSqlString.Append("               (" + "\n");
                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    strSqlString.Append("               SELECT SUBSTR(B.TRAN_TIME,0,6) AS WORK_DATE, A.OPER, LOSS_CODE, SUM(NVL(LOSS_QTY,0)) AS QTY" + "\n");
                }
                else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
                {
                    strSqlString.Append("               SELECT SYS_YEAR||PLAN_WEEK AS WORK_DATE, A.OPER, LOSS_CODE, SUM(NVL(LOSS_QTY,0)) AS QTY" + "\n");
                }
                else
                {
                    strSqlString.Append("               SELECT SUBSTR(B.TRAN_TIME,0,8) AS WORK_DATE, A.OPER, LOSS_CODE, SUM(NVL(LOSS_QTY,0)) AS QTY" + "\n");
                }
                strSqlString.Append("                 FROM ( " + "\n");
                strSqlString.Append("                      SELECT * FROM RWIPLOTLSM " + "\n");
                strSqlString.Append("                       WHERE OPER " + cdvOper.SelectedValueToQueryString + ") A," + "\n");
                strSqlString.Append("                      (" + "\n");
                strSqlString.Append("                      SELECT *" + "\n");
                strSqlString.Append("                        FROM VWIPSHPLOT LOT, MWIPCALDEF, MWIPMATDEF@RPTTOMES MAT" + "\n");
                strSqlString.Append("                       WHERE FROM_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                         AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                         AND LOT.FROM_FACTORY = MAT.FACTORY" + "\n");
                strSqlString.Append("                         AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
                strSqlString.Append("                         AND CALENDAR_ID = 'HM'" + "\n");
                strSqlString.Append("                         AND SUBSTR(TRAN_TIME,0,8) = SYS_DATE" + "\n");
                strSqlString.Append("                         AND LOT_TYPE = 'W'" + "\n");
                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    strSqlString.Append("                  AND TRAN_TIME BETWEEN '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "01000000" + "' AND '" + cdvFromToDate.ToYearMonth.Value.ToString("yyyyMM") + "31235959" + "'" + "\n");
                }
                else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
                {
                    strSqlString.Append("                  AND TRAN_TIME BETWEEN '" + dayArry[0] + "000000" + "' AND '" + dayArry[1] + "235959" + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("                  AND TRAN_TIME BETWEEN '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "000000" + "' AND '" + cdvFromToDate.ToDate.Text.Replace("-", "") + "235959" + "'" + "\n");
                }
                strSqlString.Append("                         AND LOT.MAT_ID LIKE '" + cdvCustomer.Text + "%' " + "\n");
                strSqlString.Append("                         AND FROM_OPER = 'AZ010'" + "\n");
                strSqlString.Append("                         AND MAT.MAT_VER = 1" + "\n");

                #region 상세 조회에 따른 SQL문 생성

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                #endregion
               
                strSqlString.Append("                       ) B" + "\n");
                strSqlString.Append("                WHERE A.LOT_ID(+) = B.LOT_ID" + "\n");
                strSqlString.Append("                  AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");

                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    strSqlString.Append("                GROUP BY SUBSTR(B.TRAN_TIME,0,6), A.OPER, LOSS_CODE" + "\n");
                    strSqlString.Append("                ORDER BY  SUBSTR(B.TRAN_TIME,0,6), QTY DESC" + "\n");
                }
                else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
                {
                    strSqlString.Append("          GROUP BY B.SYS_YEAR||B.PLAN_WEEK , A.OPER, LOSS_CODE" + "\n");
                    strSqlString.Append("          ORDER BY B.SYS_YEAR||B.PLAN_WEEK, QTY DESC" + "\n");
                }
                else
                {
                    strSqlString.Append("                GROUP BY SUBSTR(B.TRAN_TIME,0,8), A.OPER, LOSS_CODE" + "\n");
                    strSqlString.Append("                ORDER BY SUBSTR(B.TRAN_TIME,0,8), QTY DESC" + "\n");
                }
                strSqlString.Append("                ) A," + "\n");
                strSqlString.Append("                (" + "\n");
                strSqlString.Append("SELECT KEY_1 AS LOSS_CODE" + "\n");
                strSqlString.Append(", DATA_1 AS LOSS_DESC" + "\n");
                strSqlString.Append(", DATA_5 AS LOSS_OPER_DESC" + "\n");
                strSqlString.Append("FROM MGCMTBLDAT" + "\n");
                strSqlString.Append("WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("AND TABLE_NAME = 'LOSS_CODE'" + "\n");
                //strSqlString.Append("AND DATA_1 <> 'ETC'" + "\n");
                //strSqlString.Append("AND DATA_2 <> 'Y'" + "\n");
                strSqlString.Append("                 ) B" + "\n");
                strSqlString.Append("          WHERE A.LOSS_CODE = B.LOSS_CODE" + "\n");
                strSqlString.Append("          )" + "\n");
                strSqlString.Append("          GROUP BY WORK_DATE, OPER" + "\n");
                strSqlString.Append("ORDER BY WORK_DATE" + "\n");
            }
        

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region 주차로 그 주차의 첫날, 마지막날 가져오기

        // 주차의 첫날
        private void GetFirstDay()
        {
            DataTable dt = null;
            StringBuilder strSqlString = new StringBuilder();

            // 22시 기준이라 금주 첫 시작일에서 -1일한 날의 215959부터가 시작 일
            //strSqlString.Append("SELECT TO_CHAR(TO_DATE(SYS_DATE)-1,'YYYYMMDD'), SYS_YEAR||PLAN_WEEK FROM MWIPCALDEF" + "\n");
            strSqlString.Append("SELECT SYS_DATE, SYS_YEAR||PLAN_WEEK FROM MWIPCALDEF" + "\n");
            strSqlString.Append(" WHERE CALENDAR_ID = 'HM'" + "\n");
            strSqlString.Append("   AND SYS_YEAR||TRIM(TO_CHAR(PLAN_WEEK,'00')) = " + cdvFromToDate.HmFromWeek + "\n");
            strSqlString.Append("ORDER BY SYS_DATE" + "\n");
            
            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
            dayArry[0] = dt.Rows[0][0].ToString(); // 금 주 시작일자
                              
        }

        // 주차의 마지막날
        private void GetLastDay()
        {
            DataTable dtLast = null;
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT SYS_DATE, SYS_YEAR||PLAN_WEEK FROM MWIPCALDEF" + "\n");
            strSqlString.Append(" WHERE CALENDAR_ID = 'HM'" + "\n");
            strSqlString.Append("   AND SYS_YEAR||TRIM(TO_CHAR(PLAN_WEEK,'00')) = " + cdvFromToDate.HmToWeek + "\n");
            strSqlString.Append("ORDER BY SYS_DATE" + "\n");

            dtLast = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
            int count = dtLast.Rows.Count;
            dayArry[1] = dtLast.Rows[count-1][0].ToString(); // 금 주 마지막일자

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

            /*
            if (cdvCustomer.Text.Equals("") || cdvCustomer.Text.Equals("ALL"))
            {
                CmnFunction.ShowMsgBox(" 고객사를 입력하세요. ");
                return;
            }
           */

            GridColumnInit();
            LabelTextChange();
            spdData_Sheet1.RowCount = 0;
            sheetView1.RowCount = 0;
            if (cdvOper.Text.TrimEnd() == "ALL")
            {
                spdData_Sheet1.RowCount = 0;
                spdData.Visible = true;
                spdData2.Visible = false;
            }
            else
            {
                sheetView1.RowCount = 0;
                spdData2.Visible = true;
                spdData.Visible = false;
            }

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
                if (cdvOper.Text.TrimEnd() == "ALL")
                {

                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 1, null, null, btnSort);
                    //Total부분 셀머지
                    spdData.RPT_FillDataSelectiveCells("TOTAL", 0, 1, 0, 1, true, Align.Center, VerticalAlign.Center);
                    spdData.ActiveSheet.Cells[0, 3].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, 2].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 1].Value)) * 100;
                    spdData.ActiveSheet.Cells[0, 5].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, 4].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 1].Value)) * 100;
                }
                else
                {
                    int[] rowType = spdData2.RPT_DataBindingWithSubTotal(dt, 0, 0, 1, null, null, btnSort);
                    //Total부분 셀머지
                    spdData2.RPT_FillDataSelectiveCells("TOTAL", 0, 1, 0, 1, true, Align.Center, VerticalAlign.Center);
                }
            

               dt.Dispose();
               if (cdvOper.Text.TrimEnd() == "ALL")
               {
                   //Chart 생성(LOSS 수량, 수율)
                   if (spdData.ActiveSheet.RowCount > 0)
                       fnMakeChart2(spdData, spdData.ActiveSheet.RowCount);
               }

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

        //공정 미선택 시 LOSS 수량과 YIELD 차트
        private void fnMakeChart2(Miracom.SmartWeb.UI.Controls.udcFarPoint SS, int iselrow)
        {
            /****************************************************
             * Comment : SS의 데이터를 Chart에 표시한다.
             * 
             * Created By : min-woo kim(2010-10-20)
             * 
             * Modified By : min-woo kim(2010-10-20)
             ****************************************************/
            int[] ich_mm = new int[iselrow-1]; int[] icols_mm = new int[iselrow-1]; int[] irows_mm = new int[iselrow-1]; string[] stitle_mm = new string[1];
            int[] yield_rows = new int[iselrow - 1]; 


            int icol = 0, irow = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                cf02.RPT_1_ChartInit();
                cf02.RPT_2_ClearData();
              

                cf02.AxisX.Staggered = false;
                for (icol = 0; icol < ich_mm.Length; icol++)
                {
                   icols_mm[icol] = icol + 1;
                    
                }
                for (irow = 0; irow < irows_mm.Length; irow++)
                {
                    irows_mm[irow] = irow + 1;
                    yield_rows[irow] = irow + 1;
                
                }
                String legendDescLoss = "LOSS [단위 : pcs]";
                String legendDescYield = "YIELD [단위 :%]";
                cf02.RPT_3_OpenData(2, icols_mm.Length);
                double max1 = cf02.RPT_4_AddData(SS, irows_mm, new int[] { 4 }, SeriseType.Column);
                double max2 = cf02.RPT_4_AddData(SS, irows_mm, new int[] { 3 }, SeriseType.Column);
                cf02.RPT_5_CloseData();
                cf02.RPT_6_SetGallery(ChartType.Bar, 0, 1, legendDescLoss, AsixType.Y, DataTypes.Initeger, max1 * 1.2);
                cf02.RPT_6_SetGallery(ChartType.Line, 1, 1, legendDescYield, AsixType.Y2, DataTypes.Initeger, max2 * 1.2);
                cf02.Series[0].PointLabels = true;
                cf02.Series[1].PointLabels = true;
                cf02.RPT_7_SetXAsixTitleUsingSpread(SS, irows_mm, 0);
                cf02.RPT_8_SetSeriseLegend(new string[] { "LOSS","YIELD"}, SoftwareFX.ChartFX.Docked.Top);
                cf02.AxisY.Gridlines = true;
                cf02.AxisY2.Gridlines = true;
                cf02.AxisY.DataFormat.Decimals = 0; 
                cf02.AxisY2.DataFormat.Decimals = 2;

                cf02.AxisY2.Step = 1;
                cf02.AxisY2.Max = 100;
                cf02.AxisY2.Min = 95;
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

        // 공정 미선택 시 불량율 차트
        private void fnMakeChart(Miracom.SmartWeb.UI.Controls.udcFarPoint SS, int iselrow)
        {
            /****************************************************
             * Comment : SS의 데이터를 Chart에 표시한다.
             * 
             * Created By : min-woo kim(2010-10-20)
             * 
             * Modified By : min-woo kim(2010-10-20)
             ****************************************************/
            int[] ich_mm = new int[6]; int[] icols_mm = new int[6]; int[] irows_mm = new int[1]; string[] stitle_mm = new string[1];
            int[] ich_ww = new int[iselrow - 1]; int[] icols_ww = new int[iselrow - 1]; int[] irows_ww = new int[iselrow]; string[] stitle_ww = new string[iselrow];
            
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                cf01.RPT_1_ChartInit();
                cf01.RPT_2_ClearData();

                cf01.AxisX.Title.Text = "Defect Status";
                cf01.Gallery = SoftwareFX.ChartFX.Gallery.Pie;

                cf01.LeftGap = 1;
                cf01.RightGap = 1;
                cf01.TopGap = 1;
                cf01.BottomGap = 1;

                cf01.AxisY.DataFormat.Format = 0;
                cf01.AxisX.Staggered = false;
                cf01.PointLabels = true;
                
                icols_mm[0] = 7;
                icols_mm[1] = 9;
                icols_mm[2] = 11;
                icols_mm[3] = 13;
                icols_mm[4] = 15;
                icols_mm[5] = 17;
                
                irows_mm[0] = iselrow;
                stitle_mm[0] = SS.Sheets[0].Cells[iselrow, 0].Text;

                cf01.RPT_3_OpenData(irows_mm.Length, icols_mm.Length);
                cf01.RPT_4_AddData(SS, irows_mm, icols_mm, SeriseType.Rows);
                cf01.RPT_5_CloseData();

                //cf01.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 1, 6);

                cf01.Legend[0] = SS.Sheets[0].Cells[iselrow, 6].Text;
                cf01.Legend[1] = SS.Sheets[0].Cells[iselrow, 8].Text;
                cf01.Legend[2] = SS.Sheets[0].Cells[iselrow, 10].Text;
                cf01.Legend[3] = SS.Sheets[0].Cells[iselrow, 12].Text;
                cf01.Legend[4] = SS.Sheets[0].Cells[iselrow, 14].Text;
                cf01.Legend[5] = SS.Sheets[0].Cells[iselrow, 16].Text;
                cf01.RPT_8_SetSeriseLegend(stitle_mm, SoftwareFX.ChartFX.Docked.Top);

                cf01.AxisY.DataFormat.Decimals = 0;
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

        // 공정 선택 시 불량율 차트
        private void fnMakeChart3(Miracom.SmartWeb.UI.Controls.udcFarPoint SS, int iselrow)
        {
            /****************************************************
             * Comment : SS의 데이터를 Chart에 표시한다.
             * 
             * Created By : bee-jae jung(2010-06-28-월요일)
             * 
             * Modified By : bee-jae jung(2010-06-28-월요일)
             ****************************************************/
            int[] ich_mm = new int[6]; int[] icols_mm = new int[6]; int[] irows_mm = new int[1]; string[] stitle_mm = new string[1];
            int[] ich_ww = new int[iselrow - 1]; int[] icols_ww = new int[iselrow - 1]; int[] irows_ww = new int[iselrow]; string[] stitle_ww = new string[iselrow];
                        
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }
                
                cf01.RPT_1_ChartInit(); 
                cf01.RPT_2_ClearData(); 

                // 2010-10-19-정비재 : 불량현황을 Chart에 Display한다.
                cf01.AxisX.Title.Text = "Defect Status";
                cf01.Gallery = SoftwareFX.ChartFX.Gallery.Pie;

                cf01.LeftGap = 1;
                cf01.RightGap = 1;
                cf01.TopGap = 1;
                cf01.BottomGap = 1;
                
                cf01.AxisY.DataFormat.Format = 0;
                //cf01.AxisY.DataFormat.Decimals = 2;
                cf01.AxisX.Staggered = false;
                cf01.PointLabels = true;
                
                icols_mm[0] = 3;
                icols_mm[1] = 5;
                icols_mm[2] = 7;
                icols_mm[3] = 9;
                icols_mm[4] = 11;
                icols_mm[5] = 13;

                irows_mm[0] = iselrow;
                stitle_mm[0] = SS.Sheets[0].Cells[iselrow, 0].Text;
 
                cf01.RPT_3_OpenData(irows_mm.Length, icols_mm.Length);
                cf01.RPT_4_AddData(SS, irows_mm, icols_mm, SeriseType.Rows);
                cf01.RPT_5_CloseData();
                
                // 차트의 헤더를 이용한 타이틀이 아닌 직접 타이틀 입력 
                //cf01.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 1, 6);
                cf01.Legend[0] = SS.Sheets[0].Cells[iselrow, 2].Text;
                cf01.Legend[1] = SS.Sheets[0].Cells[iselrow, 4].Text;
                cf01.Legend[2] = SS.Sheets[0].Cells[iselrow, 6].Text;
                cf01.Legend[3] = SS.Sheets[0].Cells[iselrow, 8].Text;
                cf01.Legend[4] = SS.Sheets[0].Cells[iselrow, 10].Text;
                cf01.Legend[5] = SS.Sheets[0].Cells[iselrow, 12].Text;
                cf01.RPT_8_SetSeriseLegend(stitle_mm, SoftwareFX.ChartFX.Docked.Top);

                cf01.AxisY.DataFormat.Decimals = 0; 
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
                //StringBuilder Condition = new StringBuilder();
                //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);
                ExcelHelper.Instance.subMakeExcel(spdData, cf01, this.lblTitle.Text, null, null);
                //spdData.ExportExcel();            
            }
            else if(spdData2.ActiveSheet.Rows.Count > 0)
            {
                ExcelHelper.Instance.subMakeExcel(spdData2, cf01, this.lblTitle.Text, null, null);
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

        }

        /// <summary>
        /// 7. 상단 Lebel 표시
        /// </summary>
        private void LabelTextChange()
        {


        }
        #endregion

        // 그리드에서 셀 선택시 해당 셀에 대한 Pie 차트
        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
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

                if (e.Row >= 1)
                {
                    fnMakeChart(spdData, e.Row);
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
        // 그리드에서 셀 선택시 해당 셀에 대한 Pie 차트(공정별)
        private void spdData2_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
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

                if (e.Row >= 1)
                {
                    fnMakeChart3(spdData2, e.Row);
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


    }
}
