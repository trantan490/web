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

namespace Hana.TAT
{
    public partial class TAT050601 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        private DataTable dtDate = null;
        private DataTable dtCode = null;
        /// <summary>
        /// 클  래  스: TAT050601<br/>
        /// 클래스요약: TAT Trend by operation<br/>
        /// 작  성  자: 미라콤 김태순<br/>
        /// 최초작성일: 2009-01-19<br/>
        /// 상세  설명: TAT trend 를 조회한다.<br/>
        /// 변경  내용: <br/>
        /// </summary>
        public TAT050601()
        {
            InitializeComponent();
            OptionInIt(); // 초기화
            //udcDurationDate1.AutoBinding();
            string From = DateTime.Now.ToString();
            string To = DateTime.Now.AddDays(-1).ToString();
            udcDurationDate1.AutoBinding(From.Substring(0, 7) + "-01", To.Substring(0, 10));

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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1", "MAT_GRP_1", "MAT.MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2", "MAT_GRP_2", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3", "MAT_GRP_3", "MAT.MAT_GRP_3", true);
            // ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Depart", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME='H_DEPARTMENT' AND KEY_1 = OPER_GRP_2 AND ROWNUM=1), '-') AS TEAM", "OPER_GRP_2", "OPER_GRP_2", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4", "MAT_GRP_4", "MAT.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5", "MAT_GRP_5", "MAT.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6", "MAT_GRP_6", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7", "MAT_GRP_7", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8", "MAT_GRP_8", "MAT.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10", "MAT_CMF_10", "MAT.MAT_CMF_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID", "MAT_ID", "MAT.MAT_ID", false);
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
                spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Team", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("goal", 0, 11, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Double2, 80);
                //spdData.RPT_AddBasicColumn("Classification", 0, 11, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                //spdData.RPT_AddDynamicColumn(udcDurationDate1, 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 60);

                if (dtDate != null)
                {
                    //for (int i = 0; i < dtDate.Rows.Count; i++)
                    //{
                    //    spdData.RPT_AddBasicColumn("CNT" + i, 0, spdData.ActiveSheet.Columns.Count, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.Number, 120);

                    //}

                    for (int i = 0; i < dtDate.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(dtDate.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 120);

                    }
                }


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
            if (!CheckField()) return;

            DataTable dt = null;

            dtDate = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDate());
            dtCode = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlCode());



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


                ////표구성에따른 항목 Display
                spdData.RPT_ColumnConfigFromTable(btnSort);

                //by John
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 10, 10, null, null, btnSort);
                //구분항목 값 생성(구분이 들어가는 위치임. 0부터 시작)
                //spdData.RPT_FillColumnData(10, new string[] { "TAT", "INQTY", "OUTQTY", "WIP"});

                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 11;

                //3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 4, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);
                //--------------------------------------------

                int countOper = 0;
                countOper = dtDate.Rows.Count;

                for (int i = 0; i < countOper; i++)
                {
                    // Sub Total 평균값으로 구하기(AVG)
                    SetAvgVertical(0, 11+i,  true);
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

        #region SubTotal 평균
        public void SetAvgVertical(int nSampleNormalRowPos, int nColPos, bool bWithNull)
        {
            double sum = 0;
            double cnt = 0;            
            double totalSum = 0;
            double totalCnt = 0;
            double subSum = 0; // 동일 분류의 서브 토탈의 합
            double subCnt = 0; // 동일 분류의 서브 토탈의 합


            double divide = 0;
            double totalDivide = 0;
            double subDivide = 0; // 동일 분류의 서브 토탈 수량

            int count = ((udcTableForm)(this.btnSort.BindingForm)).GetSelectedCount();

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
                    {
                        spdData.ActiveSheet.Cells[i, nColPos].Value = sum;

                        if (count > 2) // Group 항목에서 체크된게 2개 이상인것(서브토탈이 2 Depth 이상인것)
                        {
                            subSum += sum;
                            subCnt += cnt;
                            subDivide += divide;

                            if (spdData.ActiveSheet.Cells[i, nColPos].BackColor == spdData.ActiveSheet.Cells[i + 1, nColPos].BackColor)
                            {
                                spdData.ActiveSheet.Cells[i, nColPos].Value = subSum;

                                subSum = 0;
                                subCnt = 0;
                                subDivide = 0;
                            }
                        }
                    }

                    totalSum += sum;
                    totalCnt += cnt;
                    totalDivide += divide;

                    sum = 0;
                    cnt = 0;
                    divide = 0;
                }
            }
            if (totalDivide != 0)
                if (totalCnt == 0)
                    spdData.ActiveSheet.Cells[0, nColPos].Value = totalSum;
            return;
        }
        #endregion

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

            return true;
        }

        #endregion


        private void OptionInIt()
        {

            udcDurationDate1.AutoBinding(DateTime.Now.AddDays(-1).ToString(), DateTime.Now.AddDays(-1).ToString());

            //cdvFromToDate.AutoBinding(DateTime.Now.AddDays(-2).ToString(), DateTime.Now.AddDays(-1).ToString());
        }


        #region MakeSqlCode
        private string MakeSqlCode()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("SELECT DISTINCT B.KEY_1 CODE, B.DATA_1 DATA  FROM MWIPOPRDEF A, MGCMTBLDAT B " + "\n");
            strSqlString.AppendFormat(" WHERE A.OPER_GRP_2 " + cdvTeam.SelectedValueToQueryString + "\n");
            if (cdvPart.SelectCount != 0 || cdvPart.SelectCount != 0)
            {
                strSqlString.AppendFormat("AND B.KEY_1 " + cdvPart.SelectedValueToQueryString + "\n");
            }
            strSqlString.AppendFormat("AND A.FACTORY ='" + cdvFactory.Text.ToString() + "'  " + "\n");
            strSqlString.AppendFormat("AND B.KEY_1 = A.OPER_GRP_3 " + "\n");
            strSqlString.AppendFormat("AND A.FACTORY = B.FACTORY " + "\n");

            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());

            return strSqlString.ToString();
        }
        #endregion


        #region MakeSqlDate
        private string MakeSqlDate()
        {
            StringBuilder strSqlString = new StringBuilder();

            string GET_WORK_DATE = null;
            string QueryCond1 = null;
            string QueryCond2 = null;

            string sFrom = udcDurationDate1.HmFromDay;
            string sTo = udcDurationDate1.HmToDay;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "DAY")
            {
                GET_WORK_DATE = "SUBSTR(GET_WORK_DATE(SHIP_DATE,'D'),LENGTH(GET_WORK_DATE(SHIP_DATE,'D'))-3,4)  AS TMPDATE, SHIP_DATE AS TMP2DATE";
            }
            else if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                GET_WORK_DATE = "SUBSTR(GET_WORK_DATE(SHIP_DATE,'W'),LENGTH(GET_WORK_DATE(SHIP_DATE,'W'))-3,4)  AS TMPDATE, SHIP_DATE AS TMP2DATE";
            }
            else
            {
                GET_WORK_DATE = "SUBSTR(GET_WORK_DATE(SHIP_DATE,'M'),LENGTH(GET_WORK_DATE(SHIP_DATE,'M'))-3,4)  AS TMPDATE, SHIP_DATE AS TMP2DATE";
            }

            strSqlString.AppendFormat("SELECT DISTINCT " + GET_WORK_DATE + "\n");
            strSqlString.AppendFormat("FROM CSUMTATMAT@RPTTOMES TAT, MWIPMATDEF MAT " + "\n");
            strSqlString.AppendFormat("WHERE 1=1 " + "\n");
            strSqlString.AppendFormat("AND TAT.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.AppendFormat("AND TAT.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.AppendFormat("AND MAT.MAT_VER = 1 " + "\n");
            strSqlString.AppendFormat("AND TAT.FACTORY = '" + cdvFactory.Text.ToString() + "' " + "\n");

            if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                strSqlString.AppendFormat("        AND TAT.OPER = 'AZ010' " + "\n");
            else if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
                strSqlString.AppendFormat("        AND TAT.OPER = 'TZ010' " + "\n");
            else if (cdvFactory.Text == "HMKE1")
                strSqlString.AppendFormat("        AND TAT.OPER = 'EZ010' " + "\n");
            else if (cdvFactory.Text == "HMKS1")
                strSqlString.AppendFormat("        AND TAT.OPER = 'SZ010' " + "\n");
            else if (cdvFactory.Text == "FGS")
                strSqlString.AppendFormat("        AND TAT.OPER = 'F0000' " + "\n");

            strSqlString.AppendFormat("   AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);


            strSqlString.AppendFormat("ORDER BY TMPDATE" + "\n");


            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());

            return strSqlString.ToString();
        }
        #endregion


        #region MakeSqlString
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            string sFrom = udcDurationDate1.HmFromDay;
            string sTo = udcDurationDate1.HmToDay;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            strSqlString.AppendFormat("SELECT {0}, TAT.DATA_1, DATA_2               " + "\n", QueryCond2);
            for (int i = 0; i < dtDate.Rows.Count; i++)
            {
//                strSqlString.AppendFormat(" ,SUM(AVG_TAT"+i+") AS AVG_TAT"+i+"\n");
                strSqlString.AppendFormat(" ,ROUND(SUM(TAT_QTY" + i + ")/SUM(SHIP_QTY" + i + ") ,2)  AS AVG_TAT" + i + "\n");
            }
            strSqlString.AppendFormat(" FROM    " + "\n");
            strSqlString.AppendFormat("         ( " + "\n");
            strSqlString.AppendFormat("             SELECT  {0}, TAT.FACTORY, DATA_1, KEY_1,SHIP_DATE " + "\n", QueryCond1);
            for (int i = 0; i < dtDate.Rows.Count; i++)
            {
//              strSqlString.AppendFormat("                 ,DECODE(SHIP_DATE, '" + dtDate.Rows[i]["TMP2DATE"].ToString() + "',SUM(SHIP_QTY)) SHIP_QTY" + i + "\n");
                strSqlString.AppendFormat("                 ,DECODE(SHIP_DATE, '" + dtDate.Rows[i]["TMP2DATE"].ToString() + "',SUM(SHIP_QTY)) SHIP_QTY" + i + "\n");
                strSqlString.AppendFormat("                 ,DECODE(SHIP_DATE, '" + dtDate.Rows[i]["TMP2DATE"].ToString() + "',SUM(TAT_QTY)) TAT_QTY" + i + "\n");
            }

            strSqlString.AppendFormat("                 FROM" + "\n");
            strSqlString.AppendFormat("                         (" + "\n");
            strSqlString.AppendFormat("                                 SELECT  GCM.DATA_1,GCM.KEY_1,TAT.MAT_ID,TAT.FACTORY, OPER_DESC" + "\n");
            //strSqlString.AppendFormat("                                     , SUM(TAT.TOTAL_TIME)/60/60/24 AS DATA_TOT" + "\n");
            strSqlString.AppendFormat("                                        , SUM(TAT.TOTAL_TAT_QTY) AS TAT_QTY" + "\n");
            strSqlString.AppendFormat("                                        , SUM(A.SHIP_QTY) SHIP_QTY" + "\n");
            strSqlString.AppendFormat("                                         , TAT.SHIP_DATE" + " \n");
            strSqlString.AppendFormat("                                     FROM    CSUMTATMAT@RPTTOMES TAT," + "\n");
            strSqlString.AppendFormat("                                         MWIPOPRDEF OPR," + "\n");
            strSqlString.AppendFormat("                                         MGCMTBLDAT GCM" + "\n");

            strSqlString.AppendFormat("                                         ,(SELECT SUM(SHIP_QTY) SHIP_QTY,MAT_ID FROM  CSUMTATMAT@RPTTOMES GROUP BY MAT_ID) A" + "\n");

            strSqlString.AppendFormat("                                     WHERE   1=1" + "\n");
            strSqlString.AppendFormat("                                         AND TAT.OPER=OPR.OPER" + "\n");
            strSqlString.AppendFormat("                                         AND OPR.OPER_GRP_3 = GCM.KEY_1" + "\n");
            strSqlString.AppendFormat("                                         AND GCM.FACTORY='" + cdvFactory.Text.ToString() + "'" + "\n");
            strSqlString.AppendFormat("                                         AND GCM.TABLE_NAME='H_DEPARTMENT'" + "\n");
            strSqlString.AppendFormat("                                         AND OPR.OPER_GRP_2 " + cdvTeam.SelectedValueToQueryString + "\n");
            strSqlString.AppendFormat("                                         AND GCM.KEY_1 " + cdvPart.SelectedValueToQueryString + "\n");
            strSqlString.AppendFormat("                                         AND OPR.FACTORY ='" + cdvFactory.Text.ToString() + "'             " + "\n");
            strSqlString.AppendFormat("                                         AND TAT.FACTORY ='" + cdvFactory.Text.ToString() + "'             " + "\n");
            strSqlString.AppendFormat("                                         AND TAT.OPER = OPR.OPER" + "\n");
            strSqlString.AppendFormat("                                         AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);

            strSqlString.AppendFormat("                                         AND TAT.MAT_ID = A.MAT_ID" + "\n");

            strSqlString.AppendFormat("                                         GROUP BY DATA_1,KEY_1,TAT.MAT_ID,TAT.FACTORY,OPR.OPER_DESC,TAT.SHIP_DATE" + "\n");
            if (cdvTeam.SelectedValueToQueryString.IndexOf("A0000") > 0)
            {
                strSqlString.AppendFormat("           UNION ALL " + "\n");
                strSqlString.AppendFormat("               SELECT '투입대기' AS DATA_1, 'A0000' AS KEY_1,TAT.MAT_ID,TAT.FACTORY, OPER_DESC " + "\n");
                //strSqlString.AppendFormat("               , SUM(TAT.TOTAL_TIME)/60/60/24 AS DATA_TOT" + "\n");
                strSqlString.AppendFormat("               , SUM(TAT.TOTAL_TAT_QTY) AS TAT_QTY" + "\n");
                strSqlString.AppendFormat("               , SUM(A.SHIP_QTY) SHIP_QTY" + "\n");
                strSqlString.AppendFormat("               , TAT.SHIP_DATE" + "\n");
                strSqlString.AppendFormat("               FROM     CSUMTATMAT@RPTTOMES TAT," + "\n");
                strSqlString.AppendFormat("               MWIPOPRDEF OPR" + "\n");

                strSqlString.AppendFormat("               ,(SELECT SUM(SHIP_QTY) SHIP_QTY,MAT_ID FROM  CSUMTATMAT@RPTTOMES GROUP BY MAT_ID) A" + "\n");

                strSqlString.AppendFormat("               WHERE   1=1" + "\n");
                strSqlString.AppendFormat("               AND OPR.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
                strSqlString.AppendFormat("               AND TAT.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
                strSqlString.AppendFormat("               AND TAT.OPER=OPR.OPER" + "\n");
                strSqlString.AppendFormat("               AND TAT.OPER " + cdvPart.SelectedValueToQueryString + "\n");
                strSqlString.AppendFormat("               AND TAT.OPER = OPR.OPER" + "\n");
                strSqlString.AppendFormat("               AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);

                strSqlString.AppendFormat("               AND TAT.MAT_ID = A.MAT_ID" + "\n");

                strSqlString.AppendFormat("               GROUP BY '투입대기','A0000',TAT.MAT_ID,TAT.FACTORY,OPR.OPER_DESC,TAT.SHIP_DATE" + "\n");
            }
            if (cdvTeam.SelectedValueToQueryString.IndexOf("A0800") > 0)
            {
                strSqlString.AppendFormat("           UNION ALL " + "\n");
                strSqlString.AppendFormat("               SELECT 'IVIGATE' AS DATA_1, 'A0800' AS KEY_1,TAT.MAT_ID,TAT.FACTORY, OPER_DESC " + "\n");
                //strSqlString.AppendFormat("               , SUM(TAT.TOTAL_TIME)/60/60/24 AS DATA_TOT" + "\n");

                strSqlString.AppendFormat("               , SUM(TAT.TOTAL_TAT_QTY) AS TAT_QTY" + "\n");
                strSqlString.AppendFormat("               , SUM(A.SHIP_QTY) SHIP_QTY" + "\n");

                strSqlString.AppendFormat("               , TAT.SHIP_DATE " + "\n");
                strSqlString.AppendFormat("               FROM     CSUMTATMAT@RPTTOMES TAT," + "\n");
                strSqlString.AppendFormat("               MWIPOPRDEF OPR" + "\n");

                strSqlString.AppendFormat("               ,(SELECT SUM(SHIP_QTY) SHIP_QTY,MAT_ID FROM  CSUMTATMAT@RPTTOMES GROUP BY MAT_ID) A" + "\n");

                strSqlString.AppendFormat("               WHERE   1=1" + "\n");
                strSqlString.AppendFormat("               AND OPR.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
                strSqlString.AppendFormat("               AND TAT.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
                strSqlString.AppendFormat("               AND TAT.OPER=OPR.OPER" + "\n");
                strSqlString.AppendFormat("               AND TAT.OPER " + cdvPart.SelectedValueToQueryString + "\n");
                strSqlString.AppendFormat("               AND TAT.OPER = OPR.OPER" + "\n");
                strSqlString.AppendFormat("               AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);

                strSqlString.AppendFormat("               AND TAT.MAT_ID = A.MAT_ID" + "\n");

                strSqlString.AppendFormat("               GROUP BY 'IVIGATE','A0800',TAT.MAT_ID,TAT.FACTORY,OPR.OPER_DESC,TAT.SHIP_DATE" + "\n");
            }
            if (cdvTeam.SelectedValueToQueryString.IndexOf("AZ010") > 0)
            {
                strSqlString.AppendFormat("           UNION ALL " + "\n");
                strSqlString.AppendFormat("               SELECT '출하대기' AS DATA_1, 'AZ010' AS KEY_1,TAT.MAT_ID,TAT.FACTORY, OPER_DESC " + "\n");
                //strSqlString.AppendFormat("               , SUM(TAT.TOTAL_TIME)/60/60/24 AS DATA_TOT" + "\n");

                strSqlString.AppendFormat("               , SUM(TAT.TOTAL_TAT_QTY) AS TAT_QTY" + "\n");
                strSqlString.AppendFormat("               , SUM(A.SHIP_QTY) SHIP_QTY" + "\n");

                strSqlString.AppendFormat("               , TAT.SHIP_DATE" + "\n");
                strSqlString.AppendFormat("               FROM     CSUMTATMAT@RPTTOMES TAT," + "\n");
                strSqlString.AppendFormat("               MWIPOPRDEF OPR" + "\n");

                strSqlString.AppendFormat("               ,(SELECT SUM(SHIP_QTY) SHIP_QTY,MAT_ID FROM  CSUMTATMAT@RPTTOMES GROUP BY MAT_ID) A" + "\n");

                strSqlString.AppendFormat("               WHERE   1=1" + "\n");
                strSqlString.AppendFormat("               AND OPR.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
                strSqlString.AppendFormat("               AND TAT.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
                strSqlString.AppendFormat("               AND TAT.OPER=OPR.OPER" + "\n");
                strSqlString.AppendFormat("               AND TAT.OPER " + cdvPart.SelectedValueToQueryString + "\n");
                strSqlString.AppendFormat("               AND TAT.OPER = OPR.OPER" + "\n");
                strSqlString.AppendFormat("               AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);

                strSqlString.AppendFormat("               AND TAT.MAT_ID = A.MAT_ID" + "\n");

                strSqlString.AppendFormat("               GROUP BY '출하대기','AZ010',TAT.MAT_ID,TAT.FACTORY,OPR.OPER_DESC,TAT.SHIP_DATE" + "\n");
            }

            strSqlString.AppendFormat("                             ) TAT" + "\n");
            strSqlString.AppendFormat("                             ,MWIPMATDEF MAT" + "\n");
            strSqlString.AppendFormat("                             WHERE MAT.MAT_GRP_1 " + udcWIPCondition1.SelectedValueToQueryString + "\n");
            strSqlString.AppendFormat("                             AND MAT.FACTORY=TAT.FACTORY" + "\n");
            strSqlString.AppendFormat("                             AND MAT.FACTORY='" + cdvFactory.Text.ToString() + "'" + "\n");
            strSqlString.AppendFormat("                             AND MAT.MAT_ID=TAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("                             AND MAT.MAT_VER=1" + "\n");
            strSqlString.AppendFormat("                             AND MAT.DELETE_FLAG=' '" + "\n");
            strSqlString.AppendFormat("                     GROUP BY {0}, TAT.FACTORY, DATA_1,KEY_1,SHIP_DATE" + "\n", QueryCond3);
            strSqlString.AppendFormat("             ) TAT" + "\n");
            strSqlString.AppendFormat("             ,MGCMTBLDAT GCM" + "\n");
            strSqlString.AppendFormat(" WHERE   1=1" + "\n");
            strSqlString.AppendFormat("         AND GCM.FACTORY='" + cdvFactory.Text.ToString() + "'" + "\n");
            strSqlString.AppendFormat("         AND GCM.TABLE_NAME='H_RPT_TAT_OBJECT'" + "\n");
            strSqlString.AppendFormat("         AND TAT.KEY_1 = GCM.KEY_2" + "\n");
            strSqlString.AppendFormat("         AND GCM.DATA_3 = TAT.MAT_GRP_3" + "\n");
            strSqlString.AppendFormat("         AND GCM.KEY_1 = (SELECT MAX(KEY_1) FROM MGCMTBLDAT WHERE FACTORY='" + cdvFactory.Text.ToString() + "' AND TABLE_NAME='H_RPT_TAT_OBJECT')" + "\n");
            strSqlString.AppendFormat("GROUP BY {0} , TAT.DATA_1, DATA_2                " + "\n", QueryCond2);
            strSqlString.AppendFormat("ORDER BY {0} , TAT.DATA_1                " + "\n", QueryCond2);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region ShowChart

        private void ShowChart(int rowCount)
        {
            double max1 = 0;
            double max_temp = 0;
            int cnt = 0;
            int cnt2 = 0;
            // 차트 설정
            udcChartFX1.RPT_2_ClearData();
            udcChartFX1.RPT_3_OpenData(1, dtDate.Rows.Count);
            int[] tat_columns = new Int32[dtDate.Rows.Count];
            int[] wip_columns = new Int32[dtDate.Rows.Count];
            int[] columnsHeader = new Int32[dtDate.Rows.Count];

            for (int i = 0; i < wip_columns.Length; i++)
            {
                columnsHeader[i] = 12  + i;
                tat_columns[i] = 12  + i;
                wip_columns[i] = 12  + i;
            }



            //TAT
            max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 0 }, tat_columns, SeriseType.Rows);
            max_temp = max1;

            Color color = spdData.ActiveSheet.Cells[1, 9].BackColor;
            for (int j = 1; j < spdData.ActiveSheet.Rows.Count; j++)
            {
                cnt++;
                if (spdData.ActiveSheet.Cells[j, 9].BackColor == color)
                {
                    cnt2++;
                    max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { cnt }, wip_columns, SeriseType.Rows);
                    if (max1 > max_temp)
                    {
                        max_temp = max1;
                    }
                }
            }
            max1 = max_temp;


            udcChartFX1.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            udcChartFX1.RPT_6_SetGallery(ChartType.Line , 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            udcChartFX1.Series[0].Color = System.Drawing.Color.Black;

            int[] LegBox = new int[cnt2 + 1];
            LegBox[0] = 0;
            cnt = 0;
            for (int j = 1; j < spdData.ActiveSheet.Rows.Count; j++)
            {
                if (spdData.ActiveSheet.Cells[j, 12].BackColor == color)
                {
                    cnt++;
                    udcChartFX1.RPT_6_SetGallery(ChartType.Line, cnt, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
                    LegBox[cnt] = j;

                }
            }



            //각 Serise별로 동일한 타입을 사용할 경우
            //udcChartFX1.RPT_6_SetGallery(ChartType.Bar, "[단위 : sls]", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            //udcChartFX1.RPT_6_SetGallery(ChartType.Line, "[단위 : EA]", AsixType.Y, DataTypes.Initeger, max1 * 1.2);

            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            //udcChartFX1.RPT_8_SetSeriseLegend(spdData, rows, 9-1, SoftwareFX.ChartFX.Docked.Top);
            //udcChartFX1.RPT_8_SetSeriseLegend(new string[] {"" }, SoftwareFX.ChartFX.Docked.Top);

            udcChartFX1.RPT_8_SetSeriseLegend(spdData, LegBox, 10, SoftwareFX.ChartFX.Docked.Right);

            udcChartFX1.PointLabels = true;
            udcChartFX1.AxisY.Gridlines = true;
            udcChartFX1.AxisY.DataFormat.Decimals = 2;
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
            ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1, this.lblTitle.Text, null, null);
        }

        #endregion

        #region Cell을 더블클릭 했을 경우의 ShowChart_Selected()
        /// <summary>
        /// CellDoubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spdData_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;

            int i = 0;
            i = e.Row;
            ShowChart_Selected(i);              // 챠트 그리기
        }
        #endregion

        #region ShowChart_Selected
        private void ShowChart_Selected(int rowCount)
        {            
            udcChartFX1.RPT_2_ClearData();
            udcChartFX1.RPT_3_OpenData(1, dtDate.Rows.Count);
            int[] tat_columns = new Int32[dtDate.Rows.Count];
            int[] wip_columns = new Int32[dtDate.Rows.Count];
            int[] columnsHeader = new Int32[dtDate.Rows.Count];

            for (int i = 0; i < wip_columns.Length; i++)
            {
                columnsHeader[i] = 12 + i;
                tat_columns[i] = 12 + i;
                wip_columns[i] = 12 + i;
            }

            double max1 = 0;
            double max_temp = 0;
            int cnt = 0;
            int cnt2 = 0;

            //TAT
            max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 0 }, tat_columns, SeriseType.Rows);
            max_temp = max1;

            Color color = spdData.ActiveSheet.Cells[1, 9].BackColor;
            for (int j = 1; j < spdData.ActiveSheet.Rows.Count; j++)
            {
                cnt++;
                if (spdData.ActiveSheet.Cells[j, 9].BackColor == color)
                {
                    cnt2++;
                    max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { cnt }, wip_columns, SeriseType.Rows);
                    if (max1 > max_temp)
                    {
                        max_temp = max1;
                    }
                }
            }
            max1 = max_temp;

            udcChartFX1.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            udcChartFX1.RPT_6_SetGallery(ChartType.Line, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            udcChartFX1.Series[0].Color = System.Drawing.Color.Black;

            int[] LegBox = new int[cnt2 + 1];
            LegBox[0] = 0;
            cnt = 0;
            for (int j = 1; j < spdData.ActiveSheet.Rows.Count; j++)
            {
                if (spdData.ActiveSheet.Cells[j, 12].BackColor == color)
                {
                    cnt++;
                    udcChartFX1.RPT_6_SetGallery(ChartType.Line, cnt, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
                    LegBox[cnt] = j;

                }
            }



            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            udcChartFX1.RPT_8_SetSeriseLegend(spdData, LegBox, 10, SoftwareFX.ChartFX.Docked.Right);
            udcChartFX1.PointLabels = true;         // 라인 그래프 일경우 정점에 수치 표시
            udcChartFX1.AxisY.Gridlines = true;     // Y축 그리드 표시
            udcChartFX1.AxisY.DataFormat.Decimals = 2;
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
            this.cdvTeam.sFactory = cdvFactory.txtValue;

            string strQuery = string.Empty;

            strQuery += "SELECT KEY_1 CODE, DATA_1 DATA" + "\n";
            strQuery += "  FROM MGCMTBLDAT " + "\n";
            strQuery += "  WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n";
            strQuery += "  AND TABLE_NAME = 'H_DEPARTMENT' " + "\n";
            strQuery += "  AND KEY_1 IN ('650','660')" + "\n";
            strQuery += "  UNION ALL" + "\n";
            strQuery += "  SELECT 'A0000' CODE, '투입대기' DATA FROM DUAL" + "\n";
            strQuery += "  UNION ALL" + "\n";
            strQuery += "  SELECT 'A0800' CODE, 'IVI Gate' DATA FROM DUAL" + "\n";
            strQuery += "  UNION ALL" + "\n";
            strQuery += "  SELECT 'AZ010' CODE, '출하대기' DATA FROM DUAL" + "\n";
            strQuery += "ORDER BY DATA, CODE " + "\n";
            System.Windows.Forms.Clipboard.SetText(strQuery.ToString());

            if (cdvFactory.txtValue != "")
                cdvTeam.sDynamicQuery = strQuery;
            else
                cdvTeam.sDynamicQuery = "";


        }

        #endregion


        private void cdvPart_ValueButtonPress(object sender, EventArgs e)
        {
            cdvPart.SetChangedFlag(true);
            cdvPart.Text = "";

            string strQuery = string.Empty;

            strQuery += "SELECT DISTINCT B.KEY_1 CODE, B.DATA_1 DATA" + "\n";
            strQuery += "  FROM MWIPOPRDEF A, MGCMTBLDAT B " + "\n";
            strQuery += "  WHERE A.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n";
            strQuery += "  AND A.FACTORY = B.FACTORY" + "\n";
            strQuery += "  AND B.TABLE_NAME = 'H_DEPARTMENT' " + "\n";
            strQuery += "  AND A.OPER_GRP_2 " + cdvTeam.SelectedValueToQueryString + "\n";
            strQuery += "  AND B.KEY_1 = A.OPER_GRP_3 " + "\n";
            if (cdvTeam.SelectedValueToQueryString.IndexOf("A0000") > 0)
            {
                strQuery += "  UNION ALL" + "\n";
                strQuery += "  SELECT 'A0000' CODE, '투입대기' DATA FROM DUAL" + "\n";
            }
            if (cdvTeam.SelectedValueToQueryString.IndexOf("A0800") > 0)
            {
                strQuery += "  UNION ALL" + "\n";
                strQuery += "  SELECT 'A0800' CODE, 'IVI Gate' DATA FROM DUAL" + "\n";
            }
            if (cdvTeam.SelectedValueToQueryString.IndexOf("AZ010") > 0)
            {
                strQuery += "  UNION ALL" + "\n";
                strQuery += "  SELECT 'AZ010' CODE, '출하대기' DATA FROM DUAL" + "\n";
            }
            strQuery += "ORDER BY DATA, CODE " + "\n";
            System.Windows.Forms.Clipboard.SetText(strQuery.ToString());

            if (cdvFactory.txtValue != "")
                cdvPart.sDynamicQuery = strQuery;
            else
                cdvPart.sDynamicQuery = "";
        }

    }
}
