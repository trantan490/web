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

namespace Hana.YLD
{
    public partial class YLD040301 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: YLD040301<br/>
        /// 클래스요약: 수율 공정별<br/>
        /// 작  성  자: 미라콤 김태순<br/>
        /// 최초작성일: 2008-10-08<br/>
        /// 상세  설명: 수율 공정별 데이터를 조회한다.<br/>
        /// 변경  내용: <br/>
        /// </summary>
        public YLD040301()
        {
            InitializeComponent();
            cdvFromTo.AutoBinding();

            SortInit();
            GridColumnInit(); //헤더 한줄짜리 

            udcChartFX1.RPT_1_ChartInit();  //차트 초기화.
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Step", "OPER", "OPER", "OPER_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') as Customer", "MAT_GRP_1", "MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT_ID", "MAT_ID", "MAT_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lot ID", "LOT_ID", "LOT_ID", "LOT_ID", false);
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
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("Oper", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Customer", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Family", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("LD Count", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Density", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Generation", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Lot ID", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);            
            spdData.RPT_AddDynamicColumn(cdvFromTo, 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Double3, 60);
                       
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
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
            if (!CheckField()) return;

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

                //// 두줄짜리이상 샘플코드(부분합계 제공 안됨-총 합계만 가능.)
                //spdData.DataSource = dt;

                ////표구성에따른 항목 Display
                //spdData.RPT_ColumnConfigFromTable(btnSort);

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);


                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 10, null, null, btnSort);

                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 9, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                for (int i = 0; i < cdvFromTo.SubtractBetweenFromToDate + 1; i++)
                {
                    SetAvgVertical(1, i + 11, false);
                }


                //Chart 생성
                if (spdData.ActiveSheet.RowCount > 0)
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

        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            if (cdvOper.FromText.Trim().Length == 0 || cdvOper.ToText.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        #endregion

        #region MakeSqlString
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string[] FromTo = new string[cdvFromTo.SubtractBetweenFromToDate + 1];
            string strFromDate = cdvFromTo.ExactFromDate;
            string strToDate = cdvFromTo.ExactToDate;
            string strDate = string.Empty;
            string option = null;
            string sFrom = null;
            string sTo = null;

            int Between = cdvFromTo.SubtractBetweenFromToDate + 1;

            FromTo = cdvFromTo.getSelectDate();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            switch (cdvFromTo.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    sFrom = cdvFromTo.FromDate.Text.Replace("-", "");
                    sTo = cdvFromTo.ToDate.Text.Replace("-", "");
                    strDate = "WORK_DATE";
                    option = "D";
                    break;
                case "WEEK":
                    sFrom = cdvFromTo.FromWeek.SelectedValue.ToString();
                    sTo = cdvFromTo.ToWeek.SelectedValue.ToString();
                    strDate = "WORK_WEEK";
                    option = "W";
                    break;
                case "MONTH":
                    sFrom = cdvFromTo.FromYearMonth.Value.ToString("yyyyMM");
                    sTo = cdvFromTo.ToYearMonth.Value.ToString("yyyMM");
                    strDate = "WORK_MONTH";
                    option = "M";
                    break;
                default:
                    sFrom = cdvFromTo.FromDate.Text.Replace("-", "");
                    sTo = cdvFromTo.ToDate.Text.Replace("-", "");
                    strDate = "WORK_DATE";
                    option = "D";
                    break;
            }

            strSqlString.AppendFormat("   SELECT {0}" + "\n", QueryCond1);

            for (int i = 0; i < Between;i++ )
            {
                strSqlString.AppendFormat("        , SUM(YIELD{0}) AS YIELD{0} " + "\n", i.ToString());
            }
            
            strSqlString.Append("     FROM (" + "\n");
            strSqlString.AppendFormat("          SELECT {0}, {1}" + "\n", QueryCond2, strDate);

            for (int i = 0; i < Between; i++)
            {
                strSqlString.AppendFormat("               , ROUND(DECODE({0}, '{1}', DECODE(SUM(IN_QTY), 0, 0, (SUM(OUT_QTY)/SUM(IN_QTY)*100)), 0), 3) AS YIELD{2}  " + "\n", strDate, FromTo[i].ToString(), i.ToString());
            }

            strSqlString.Append("            FROM (            " + "\n");
            strSqlString.AppendFormat("                 SELECT {0}, {1}" + "\n", QueryCond2, strDate);
            strSqlString.Append("                      , SUM(IN_QTY) AS IN_QTY, SUM(OUT_QTY) AS OUT_QTY " + "\n");
            strSqlString.Append("                   FROM (" + "\n");
            strSqlString.Append("                        SELECT OPER, B.MAT_GRP_1, B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_4, B.MAT_GRP_5, B.MAT_GRP_6, B.MAT_GRP_7, B.MAT_GRP_8, A.MAT_ID, A.LOT_ID" + "\n");
            strSqlString.AppendFormat("                             , GET_WORK_DATE(END_TIME, '{0}') AS {1}" + "\n", option, strDate);
            strSqlString.AppendFormat("                             , SUM(CASE WHEN OPER_IN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
            strSqlString.Append("                                        THEN OPER_IN_QTY_1  " + "\n");
            strSqlString.Append("                                        ELSE 0" + "\n");
            strSqlString.Append("                               END) AS IN_QTY" + "\n");
            strSqlString.AppendFormat("                             , SUM(CASE WHEN END_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
            strSqlString.Append("                                        THEN END_QTY_1  " + "\n");
            strSqlString.Append("                                        ELSE 0" + "\n");
            strSqlString.Append("                               END) AS OUT_QTY  " + "\n");
            strSqlString.Append("                          FROM RSUMWIPLTH A     " + "\n");
            strSqlString.Append("                             , MWIPMATDEF B" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND A.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                           AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                           AND B.MAT_VER = 1                    " + "\n");
            strSqlString.AppendFormat("                           AND A.OPER IN ({0})" + "\n", cdvOper.getInQuery());
            strSqlString.AppendFormat("                           AND A.OPER_IN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
            strSqlString.AppendFormat("                           AND A.END_TIME BETWEEN '{0}' AND '{1}'  " + "\n", strFromDate, strToDate);

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);


            strSqlString.Append("                      GROUP BY OPER, B.MAT_GRP_1, B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_4, B.MAT_GRP_5, B.MAT_GRP_6, B.MAT_GRP_7, B.MAT_GRP_8, A.MAT_ID, A.LOT_ID, END_TIME " + "\n");
            strSqlString.Append("                        )" + "\n");
            strSqlString.Append("                  WHERE IN_QTY <> 0 AND OUT_QTY <> 0 " + "\n");
            strSqlString.AppendFormat("               GROUP BY {0}, {1}" + "\n", QueryCond2, strDate);
            strSqlString.Append("                 )" + "\n");
            strSqlString.AppendFormat("             GROUP BY {0}, {1}" + "\n", QueryCond2, strDate);
            strSqlString.Append("          )" + "\n");
            strSqlString.AppendFormat("         GROUP BY {0} " + "\n", QueryCond2);
            strSqlString.AppendFormat("         ORDER BY {0} " + "\n", QueryCond2);


            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
            //System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());

            return strSqlString.ToString();
        }

        // CHART 생성을 위한 쿼리문

        private string DistinctQuery()     
        {
            StringBuilder strSqlString = new StringBuilder();

            string[] FromTo = new string[cdvFromTo.SubtractBetweenFromToDate + 1];
            string strFromDate = cdvFromTo.ExactFromDate;
            string strToDate = cdvFromTo.ExactToDate;
            string strDate = string.Empty;
            string option = null;
            string sFrom = null;
            string sTo = null;

            int Between = cdvFromTo.SubtractBetweenFromToDate + 1;

            FromTo = cdvFromTo.getSelectDate();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQuery;
            QueryCond3 = tableForm.SelectedValue3ToQuery;

            
            string[] Query1 = QueryCond2.Split(',');
            string where = null;

            for(int i=0; i<Query1.Length; i++)
            {
                int cnt = 0;
                if (Query1[i] != "OPER" && Query1[i] != "MAT_ID" && Query1[i] != "LOT_ID")
                {
                    if( cnt == 0 )
                    {
                        where = where + Query1[i];
                        cnt++;
                    }
                    else
                    {
                        where = where + ", " + Query1[i];
                        cnt++;
                    }
                    
                }
            }

            switch (cdvFromTo.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    sFrom = cdvFromTo.FromDate.Text.Replace("-", "");
                    sTo = cdvFromTo.ToDate.Text.Replace("-", "");
                    strDate = "WORK_DATE";
                    option = "D";
                    break;
                case "WEEK":
                    sFrom = cdvFromTo.FromWeek.SelectedValue.ToString();
                    sTo = cdvFromTo.ToWeek.SelectedValue.ToString();
                    strDate = "WORK_WEEK";
                    option = "W";
                    break;
                case "MONTH":
                    sFrom = cdvFromTo.FromYearMonth.Value.ToString("yyyyMM");
                    sTo = cdvFromTo.ToYearMonth.Value.ToString("yyyMM");
                    strDate = "WORK_MONTH";
                    option = "M";
                    break;
                default:
                    sFrom = cdvFromTo.FromDate.Text.Replace("-", "");
                    sTo = cdvFromTo.ToDate.Text.Replace("-", "");
                    strDate = "WORK_DATE";
                    option = "D";
                    break;
            }

            strSqlString.AppendFormat("    SELECT DISTINCT {0}   " + "\n", where);
            strSqlString.Append("      FROM (" + "\n");

            strSqlString.AppendFormat("   SELECT {0}" + "\n", QueryCond2);

            for (int i = 0; i < Between; i++)
            {
                strSqlString.AppendFormat("        , SUM(YIELD{0}) AS YIELD{0} " + "\n", i.ToString());
            }

            strSqlString.Append("     FROM (" + "\n");
            strSqlString.AppendFormat("          SELECT {0}, {1}" + "\n", QueryCond2, strDate);

            for (int i = 0; i < Between; i++)
            {
                strSqlString.AppendFormat("               , ROUND(DECODE({0}, '{1}', DECODE(SUM(IN_QTY), 0, 0, (SUM(OUT_QTY)/SUM(IN_QTY)*100)), 0), 3) AS YIELD{2}  " + "\n", strDate, FromTo[i].ToString(), i.ToString());
            }

            strSqlString.Append("            FROM (            " + "\n");
            strSqlString.AppendFormat("                 SELECT {0}, {1}" + "\n", QueryCond2, strDate);
            strSqlString.Append("                      , SUM(IN_QTY) AS IN_QTY, SUM(OUT_QTY) AS OUT_QTY " + "\n");
            strSqlString.Append("                   FROM (" + "\n");
            strSqlString.Append("                        SELECT OPER, B.MAT_GRP_1, B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_4, B.MAT_GRP_5, B.MAT_GRP_6, B.MAT_GRP_7, B.MAT_GRP_8, A.MAT_ID, A.LOT_ID" + "\n");
            strSqlString.AppendFormat("                             , GET_WORK_DATE(END_TIME, '{0}') AS {1}" + "\n", option, strDate);
            strSqlString.AppendFormat("                             , SUM(CASE WHEN OPER_IN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
            strSqlString.Append("                                        THEN OPER_IN_QTY_1  " + "\n");
            strSqlString.Append("                                        ELSE 0" + "\n");
            strSqlString.Append("                               END) AS IN_QTY" + "\n");
            strSqlString.AppendFormat("                             , SUM(CASE WHEN END_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
            strSqlString.Append("                                        THEN END_QTY_1  " + "\n");
            strSqlString.Append("                                        ELSE 0" + "\n");
            strSqlString.Append("                               END) AS OUT_QTY  " + "\n");
            strSqlString.Append("                          FROM RSUMWIPLTH A     " + "\n");
            strSqlString.Append("                             , MWIPMATDEF B" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND A.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                           AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                           AND B.MAT_VER = 1                    " + "\n");
            strSqlString.AppendFormat("                           AND A.OPER IN ({0})" + "\n", cdvOper.getInQuery());
            strSqlString.AppendFormat("                           AND A.OPER_IN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
            strSqlString.AppendFormat("                           AND A.END_TIME BETWEEN '{0}' AND '{1}'  " + "\n", strFromDate, strToDate);

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                           AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);


            strSqlString.Append("                      GROUP BY OPER, B.MAT_GRP_1, B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_4, B.MAT_GRP_5, B.MAT_GRP_6, B.MAT_GRP_7, B.MAT_GRP_8, A.MAT_ID, A.LOT_ID, END_TIME " + "\n");
            strSqlString.Append("                        )" + "\n");
            strSqlString.Append("                  WHERE IN_QTY <> 0 AND OUT_QTY <> 0 " + "\n");
            strSqlString.AppendFormat("               GROUP BY {0}, {1}" + "\n", QueryCond2, strDate);
            strSqlString.Append("                 )" + "\n");
            strSqlString.AppendFormat("             GROUP BY {0}, {1}" + "\n", QueryCond2, strDate);
            strSqlString.Append("          )" + "\n");
            strSqlString.AppendFormat("         GROUP BY {0} " + "\n", QueryCond2);
            strSqlString.AppendFormat("         ORDER BY {0} " + "\n", QueryCond2);
            strSqlString.Append("              )" + "\n");

            return strSqlString.ToString();
        }

        private string MakeSqlString1()  // Chart 생성
        {
            StringBuilder strSqlString = new StringBuilder();

            string[] FromTo = new string[cdvFromTo.SubtractBetweenFromToDate + 1];
            string strFromDate = cdvFromTo.ExactFromDate;
            string strToDate = cdvFromTo.ExactToDate;
            string strDate = string.Empty;            
            string sFrom = null;
            string sTo = null;
            string name = null;

            int Between = cdvFromTo.SubtractBetweenFromToDate + 1;

            FromTo = cdvFromTo.getSelectDate();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQuery;
            
            switch (cdvFromTo.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    sFrom = cdvFromTo.FromDate.Text.Replace("-", "");
                    sTo = cdvFromTo.ToDate.Text.Replace("-", "");
                    strDate = "WORK_DATE";
                    break;
                case "WEEK":
                    sFrom = cdvFromTo.FromWeek.SelectedValue.ToString();
                    sTo = cdvFromTo.ToWeek.SelectedValue.ToString();
                    strDate = "WORK_WEEK";                    
                    break;
                case "MONTH":
                    sFrom = cdvFromTo.FromYearMonth.Value.ToString("yyyyMM");
                    sTo = cdvFromTo.ToYearMonth.Value.ToString("yyyMM");
                    strDate = "WORK_MONTH";                    
                    break;
                default:
                    sFrom = cdvFromTo.FromDate.Text.Replace("-", "");
                    sTo = cdvFromTo.ToDate.Text.Replace("-", "");
                    strDate = "WORK_DATE";                    
                    break;
            }

            DataTable Condition = null;

            try
            {
                Condition = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", DistinctQuery());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Condition.Dispose();
            }



            strSqlString.AppendFormat("          SELECT OPER_GRP_1 " + "\n");

            strSqlString.Append("                , SUM(IN_QTY) IN_QTY" + "\n");
            strSqlString.Append("                , SUM(OUT_QTY) OUT_QTY" + "\n");
            strSqlString.Append("                , DECODE(SUM(IN_QTY), 0, 0, ROUND((SUM(OUT_QTY)/SUM(IN_QTY))*100, 3)) YIELD" + "\n");



            for (int i = 0; i < Condition.Rows.Count; i++)
            {
                strSqlString.Append("                , SUM(CASE WHEN ");
                
                for (int j = 0; j < Condition.Columns.Count; j++)
                {
                    strSqlString.AppendFormat(" {0} = '{1}' ", Condition.Columns[j].ToString(), Condition.Rows[i][j].ToString());                    
                }
                strSqlString.Append("" + "\n");

                strSqlString.Append("                           THEN DECODE(IN_QTY, 0, 0, ROUND((OUT_QTY/IN_QTY)*100, 3))  " + "\n");
                strSqlString.Append("                           ELSE 0" + "\n");


                for (int j = 0; j < Condition.Columns.Count; j++)
                {
                    name = name + Condition.Rows[i][j].ToString();
                }
                strSqlString.AppendFormat("                      END ) AS \"{0}\"" + "\n", name);
                name = null;
            }

            strSqlString.Append("            FROM (            " + "\n");
            strSqlString.AppendFormat("                 SELECT {0}" + "\n", QueryCond3);
            strSqlString.Append("                      , SUM(IN_QTY) AS IN_QTY, SUM(OUT_QTY) AS OUT_QTY, SUM(LOSS_QTY) AS LOSS_QTY " + "\n");
            strSqlString.Append("                   FROM (" + "\n");
            strSqlString.Append("                        SELECT OPER_GRP_1, MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, A.MAT_ID, A.LOT_ID" + "\n");            
            strSqlString.AppendFormat("                             , SUM(CASE WHEN OPER_IN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
            strSqlString.Append("                                        THEN OPER_IN_QTY_1  " + "\n");
            strSqlString.Append("                                        ELSE 0" + "\n");
            strSqlString.Append("                               END) AS IN_QTY" + "\n");
            strSqlString.AppendFormat("                             , SUM(CASE WHEN END_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
            strSqlString.Append("                                        THEN END_QTY_1  " + "\n");
            strSqlString.Append("                                        ELSE 0" + "\n");
            strSqlString.Append("                               END) AS OUT_QTY  " + "\n");
            strSqlString.Append("                                   , SUM(LOSS_QTY) AS LOSS_QTY" + "\n");
            strSqlString.Append("                          FROM RSUMWIPLTH A     " + "\n");
            strSqlString.Append("                             , MWIPMATDEF B" + "\n");
            strSqlString.Append("                             , MWIPOPRDEF C" + "\n");
            strSqlString.Append("                             , RWIPLOTLSM D" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND A.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                           AND A.FACTORY = B.FACTORY(+)" + "\n");
            strSqlString.Append("                           AND A.FACTORY = C.FACTORY(+)" + "\n");
            strSqlString.Append("                           AND A.FACTORY = D.FACTORY(+)" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("                           AND B.MAT_VER(+) = 1    " + "\n");
            strSqlString.Append("                           AND D.MAT_VER(+) = 1    " + "\n");
            strSqlString.Append("                           AND A.OPER = C.OPER(+)  " + "\n");
            strSqlString.Append("                           AND A.OPER = D.OPER(+)  " + "\n");
            strSqlString.AppendFormat("                           AND A.OPER_IN_TIME(+) BETWEEN  '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
            strSqlString.AppendFormat("                           AND A.END_TIME(+) BETWEEN '{0}' AND '{1}'  " + "\n", strFromDate, strToDate);
            strSqlString.AppendFormat("                           AND D.TRAN_TIME(+) BETWEEN '{0}' AND '{1}'  " + "\n", strFromDate, strToDate);
            strSqlString.Append("                      GROUP BY OPER_GRP_1, MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, A.MAT_ID, A.LOT_ID, END_TIME " + "\n");
            strSqlString.Append("                        )" + "\n");
            strSqlString.Append("                  WHERE IN_QTY <> 0 AND OUT_QTY <> 0 " + "\n");
            strSqlString.AppendFormat("               GROUP BY {0}" + "\n", QueryCond3);
            strSqlString.Append("                 )" + "\n");
            strSqlString.Append("             GROUP BY OPER_GRP_1 " + "\n");
            strSqlString.Append("             ORDER BY OPER_GRP_1 " + "\n");
            
            return strSqlString.ToString();
        }

        #endregion

        #region ShowChart

        private void ShowChart(int rowCount)
        {
            // 차트 설정
            udcChartFX1.RPT_1_ChartInit();
            udcChartFX1.RPT_2_ClearData();

            udcChartFX1.AxisY.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisY2.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisX.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);

            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());

            if (dt == null || dt.Rows.Count < 1)
                return;

            //dt = GetRotatedDataTable(ref dt);

            //int series = dt.Rows.Count;
            //int step = dt.Columns.Count - 1;
            //udcChartFX1.RPT_3_OpenData(series, step);

            //// Chart Value 셋팅
            //for (int i = 0; i < series; i++)
            //{
            //    udcChartFX1.SerLeg[i] = dt.Rows[i][0].ToString();
            //    for (int j = 0; j < step; j++)
            //    {
            //        udcChartFX1.Legend[i] = dt.Columns[i + 1].Caption;
            //        udcChartFX1.Value[i, j] = Convert.ToDouble(dt.Rows[i][j+1]);
            //    }

            //    if(i > 2)
            //    {
            //        udcChartFX1.Series[i].Gallery = SoftwareFX.ChartFX.Gallery.Lines;
            //        udcChartFX1.Series[i].LineWidth = 2;
            //        udcChartFX1.Series[i].YAxis = SoftwareFX.ChartFX.YAxis.Secondary;
            //    }
            //    else
            //    {
            //        udcChartFX1.Series[i].Gallery = SoftwareFX.ChartFX.Gallery.Bar;
            //    }
            //}

            udcChartFX1.DataSource = dt;
            udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Lines;

            udcChartFX1.Series[0].YAxis = SoftwareFX.ChartFX.YAxis.Secondary;
            udcChartFX1.Series[1].YAxis = SoftwareFX.ChartFX.YAxis.Secondary;
            udcChartFX1.Series[2].YAxis = SoftwareFX.ChartFX.YAxis.Secondary;
            udcChartFX1.Series[0].Gallery = SoftwareFX.ChartFX.Gallery.Bar;
            udcChartFX1.Series[1].Gallery = SoftwareFX.ChartFX.Gallery.Bar;
            udcChartFX1.Series[2].Gallery = SoftwareFX.ChartFX.Gallery.Bar;
            udcChartFX1.SerLegBox = true;
            udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
            udcChartFX1.LegendBox = false;
            udcChartFX1.PointLabels = true;

            //udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
            udcChartFX1.AxisY.Max = 101;
            udcChartFX1.AxisY.Min = 97;

            udcChartFX1.RecalcScale();
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
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();
        }

        #endregion

        #region cdvFactory_ValueSelectedItemChanged

        /// <summary>
        /// cdvFactory_ValueSelectedItemChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            cdvOper.sFactory = cdvFactory.txtValue;
        }

        #endregion

        public void SetAvgVertical(int nSampleNormalRowPos, int nColPos, bool bWithNull)
        {
            double sum = 0;
            double totalSum = 0;

            double divide = 0;
            double totalDivide = 0;

            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;

            for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                if (spdData.ActiveSheet.Cells[i, nColPos].BackColor == color)
                {
                    sum += Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos].Value);

                    if (!bWithNull && (spdData.ActiveSheet.Cells[i, nColPos].Value == null || spdData.ActiveSheet.Cells[i, nColPos].Value.ToString().Trim() == ""))
                        continue;

                    divide += 1;
                }
                else
                {
                    if (divide != 0)
                        spdData.ActiveSheet.Cells[i, nColPos].Value = sum / divide;

                    totalSum += sum;
                    totalDivide += divide;

                    sum = 0;
                    divide = 0;
                }
            }
            if (totalDivide != 0)
                spdData.ActiveSheet.Cells[0, nColPos].Value = totalSum / totalDivide;

            return;
        }

        /// <summary>
        /// DataTable의 행과 열을 서로 바꿔주는 메서드입니다.
        /// </summary>
        /// <param name="dt">변환되어야 할 DataTable을 입력받습니다.</param>
        /// <returns>변환된 DataTable을 반환합니다.</returns>
        public DataTable GetRotatedDataTable(ref DataTable dt)
        {
            // Column Position of dt => Legend Column of dtNew
            int nColToRow = 0;

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
    }
}
 