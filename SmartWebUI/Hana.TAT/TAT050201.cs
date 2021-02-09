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
    public partial class TAT050201 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        private DataTable dtDate = null;
        private String stLotType = null;
        /// <summary>
        /// 클  래  스: TAT050201<br/>
        /// 클래스요약: TAT Trend<br/>
        /// 작  성  자: 미라콤 김태순<br/>
        /// 최초작성일: 2009-01-19<br/>
        /// 상세  설명: TAT trend를 조회한다.<br/>
        /// 변경  내용: <br/>
        /// 2010-03-15-임종우 : 그룹 검색 조건 추가 함. 투입대기,출하대기 선택 기능 추가함.
        /// 2011-02-07-임종우 : TAT 단위 시간 단위 추가 표시 (임태성 요청)
        /// 2011-06-03-김민우 : Lot type P% / E% 로 조회기능 추가 (임태성 요청)
        /// 2013-05-08-김민우 : Package2 추가
        /// </summary>
        public TAT050201()
        {
            InitializeComponent();
            OptionInIt(); // 초기화
            string From = DateTime.Now.ToString();
            string To = DateTime.Now.AddDays(-1).ToString();
            udcDurationDate1.AutoBinding(From.Substring(0, 7) + "-01", To.Substring(0, 10));
            //udcDurationDate1.FromDate.MinDate = DateTime.Parse("2009-08-01");
            SortInit();
            GridColumnInit(); //헤더 한줄짜리 
            udcChartFX2.RPT_1_ChartInit();  //차트 초기화. 
            cbLotType.Text = "ALL";
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1", "MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3", "MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10", "MAT_CMF_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package2", "MAT.MAT_GRP_10", "MAT_GRP_10", false);
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
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Package2", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);

                spdData.RPT_AddBasicColumn("AVG", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Double2, 80);
                // 총 평균을 구하기위해 임시로 만들어줌
                spdData.RPT_AddBasicColumn("TSUM", 0, 11, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("TCNT", 0, 12, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.Double2, 80);
                //spdData.RPT_AddDynamicColumn(udcDurationDate1, 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Double2, 60);
                if (dtDate != null)
                {
                    for (int i = 0; i < dtDate.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(dtDate.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 120);
                        spdData.RPT_AddBasicColumn("SUM", 0, spdData.ActiveSheet.Columns.Count, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                        spdData.RPT_AddBasicColumn("CNT", 0, spdData.ActiveSheet.Columns.Count, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                    }
                }


                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
            }
            catch(Exception ex)
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
            


            GridColumnInit();
            udcChartFX1.RPT_1_ChartInit();
            udcChartFX2.RPT_1_ChartInit();            

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
                spdData.RPT_ColumnConfigFromTable(btnSort);

                ////by John (한줄짜리)
                ////1.Griid 합계 표시

                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 10, null, null, btnSort);

                ////2. 칼럼 고정(필요하다면..)
                spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);
                //--------------------------------------------

                //SetAvgVertical(1,udcDurationDate1.SubtractBetweenFromToDate+9, false);

                int countOper = 0;
                countOper = dtDate.Rows.Count * 3;

                for (int i = 0; i < countOper; i += 3)
                {
                    SetAvgVertical(1, 13 + i, true);
                }

                // Sub Total 평균값으로 구하기(AVG)
                SetAvgVertical(1, 10, true);

                //Chart 생성
                if (spdData.ActiveSheet.RowCount > 0)
                {
                    if (chkGraph1.Checked == true && chkGraph2.Checked == false)
                    {
                        splitContainer3.Panel1Collapsed = false;
                        splitContainer3.Panel2Collapsed = true;
                        udcChartFX1.Visible = true;
                     //   udcChartFX1.Size = new System.Drawing.Size(794, 200);
                        udcChartFX2.Visible = false;
                        ShowChart(0);
                    }
                    else if (chkGraph2.Checked == true && chkGraph1.Checked == false)
                    {
                        splitContainer3.Panel2Collapsed = false;
                        splitContainer3.Panel1Collapsed = true;
                        udcChartFX2.AutoSize = true;
                        udcChartFX2.Visible = true;
                     //   udcChartFX2.Size = new System.Drawing.Size(794, 200);
                        udcChartFX1.Visible = false;
                        ShowChart2(0);
                    }
                    else
                    {

                        splitContainer3.Panel2Collapsed = false;
                        splitContainer3.Panel1Collapsed = false;
                     //   udcChartFX1.Size = new System.Drawing.Size(794, 100);
                     //   udcChartFX2.Size = new System.Drawing.Size(794, 100);
                        udcChartFX1.Visible = true;
                        udcChartFX2.Visible = true;
                        ShowChart(0);
                        ShowChart2(0);
                    }

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
                    sum += Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos + 1].Value);
                    if (Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos + 1].Value) != 0)
                    {
                        cnt += Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos + 2].Value);
                    }
                    if (!bWithNull && (spdData.ActiveSheet.Cells[i, nColPos + 1].Value == null || spdData.ActiveSheet.Cells[i, nColPos + 1].Value.ToString().Trim() == ""))
                        continue;

                    divide += 1;
                }
                else
                {
                    if (divide != 0)
                    {
                        if (ckbTime.Checked == true)
                        {
                            if (cnt == 0)
                            {
                                spdData.ActiveSheet.Cells[i, nColPos].Value = sum / 1 * 24;
                            }
                            else
                            {
                                spdData.ActiveSheet.Cells[i, nColPos].Value = Math.Round(sum / cnt * 24, 2);
                            }
                        }
                        else
                        {
                            if (cnt == 0)
                            {
                                spdData.ActiveSheet.Cells[i, nColPos].Value = sum / 1;
                            }
                            else
                            {
                                spdData.ActiveSheet.Cells[i, nColPos].Value = Math.Round(sum / cnt, 2);
                            }
                        }

                        if (count > 2) // Group 항목에서 체크된게 2개 이상인것(서브토탈이 2 Depth 이상인것)
                        {
                            subSum += sum;
                            subCnt += cnt;
                            subDivide += divide;

                            if (spdData.ActiveSheet.Cells[i, nColPos].BackColor == spdData.ActiveSheet.Cells[i + 1, nColPos].BackColor)
                            {
                                if (ckbTime.Checked == true)
                                {
                                    if (subCnt == 0)
                                    {
                                        spdData.ActiveSheet.Cells[i + 1, nColPos].Value = subSum / 1 * 24;
                                    }
                                    else
                                    {
                                        spdData.ActiveSheet.Cells[i + 1, nColPos].Value = Math.Round(subSum / subCnt * 24, 2);
                                    }
                                }
                                else
                                {
                                    if (subCnt == 0)
                                    {
                                        spdData.ActiveSheet.Cells[i + 1, nColPos].Value = subSum / 1;
                                    }
                                    else
                                    {
                                        spdData.ActiveSheet.Cells[i + 1, nColPos].Value = Math.Round(subSum / subCnt, 2);
                                    }
                                }

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
            {
                if (ckbTime.Checked == true)
                {
                    if (totalCnt == 0)
                    {
                        spdData.ActiveSheet.Cells[0, nColPos].Value = totalSum / 1 * 24;
                    }
                    else
                    {
                        spdData.ActiveSheet.Cells[0, nColPos].Value = Math.Round(totalSum / totalCnt * 24, 2);
                    }
                }
                else
                {
                    if (totalCnt == 0)
                    {
                        spdData.ActiveSheet.Cells[0, nColPos].Value = totalSum / 1;
                    }
                    else
                    {
                        spdData.ActiveSheet.Cells[0, nColPos].Value = Math.Round(totalSum / totalCnt, 2);
                    }
                }
            }

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


        #region DateSqlString
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
                GET_WORK_DATE = "SUBSTR(SHIP_DATE,LENGTH(SHIP_DATE)-3,4) AS TMPDATE, SHIP_DATE AS TMP2DATE";
            }
            else if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                GET_WORK_DATE = "SUBSTR(GET_WORK_DATE(SHIP_DATE,'W'),LENGTH(GET_WORK_DATE(SHIP_DATE,'W'))-3,4) AS TMPDATE, GET_WORK_DATE(SHIP_DATE,'W') AS TMP2DATE";
            }
            else
            {
                GET_WORK_DATE = "SUBSTR(GET_WORK_DATE(SHIP_DATE,'M'),LENGTH(GET_WORK_DATE(SHIP_DATE,'M'))-3,4) AS TMPDATE, GET_WORK_DATE(SHIP_DATE,'M') AS TMP2DATE";
            }

            strSqlString.AppendFormat("SELECT DISTINCT " + GET_WORK_DATE + "\n");
            strSqlString.AppendFormat("FROM CSUMTATMAT@RPTTOMES TAT, MWIPMATDEF MAT " + "\n");
            strSqlString.AppendFormat("WHERE 1=1 " + "\n");
            strSqlString.AppendFormat("AND TAT.FACTORY = '" + cdvFactory.Text.ToString() + "' " + "\n");
            strSqlString.AppendFormat("AND MAT.FACTORY = '" + cdvFactory.Text.ToString() + "' " + "\n");
            strSqlString.AppendFormat("AND TAT.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.AppendFormat("AND TAT.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.AppendFormat("AND MAT.MAT_VER = 1 " + "\n");

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

            //기간별 조회 SQL문 생성

            strSqlString.AppendFormat(" AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

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

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            string sFrom = udcDurationDate1.HmFromDay;
            string sTo = udcDurationDate1.HmToDay;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            stLotType = cbLotType.Text; // Lot Type

            strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond2);

            if (ckbTime.Checked == true)
            {
                strSqlString.AppendFormat("     , ROUND(SUM(TOT_TAT)/SUM(TOT_SHIP) * 24,2) AS TOT_AVG " + "\n");
            }
            else
            {
                strSqlString.AppendFormat("     , ROUND(SUM(TOT_TAT)/SUM(TOT_SHIP),2) AS TOT_AVG " + "\n");
            }

            strSqlString.AppendFormat("     , ROUND(SUM(TOT_TAT),2) AS TSUM  " + "\n");
            strSqlString.AppendFormat("     , SUM(TOT_SHIP) AS TCNT  " + "\n");

            if (ckbTime.Checked == true)
            {
                for (int i = 0; i < dtDate.Rows.Count; i++)
                {
                    strSqlString.AppendFormat("     , ROUND(SUM(TAT" + i + ") * 24,2) AS  V" + i + " " + "\n");
                    strSqlString.AppendFormat("     , ROUND(SUM(TAT_QTY" + i + "),2) AS  TAT_QTY" + i + " " + "\n");
                    strSqlString.AppendFormat("     , ROUND(SUM(SHIP_QTY" + i + "),2) AS  SHIP_QTY" + i + " " + "\n");
                }
            }
            else
            {
                for (int i = 0; i < dtDate.Rows.Count; i++)
                {
                    strSqlString.AppendFormat("     , ROUND(SUM(TAT" + i + "),2) AS  V" + i + " " + "\n");
                    strSqlString.AppendFormat("     , ROUND(SUM(TAT_QTY" + i + "),2) AS  TAT_QTY" + i + " " + "\n");
                    strSqlString.AppendFormat("     , ROUND(SUM(SHIP_QTY" + i + "),2) AS  SHIP_QTY" + i + " " + "\n");
                }
            }

            strSqlString.AppendFormat("  FROM (  " + "\n");
            strSqlString.AppendFormat("         SELECT {0} " + "\n", QueryCond2);
            strSqlString.AppendFormat("              , SHIP_DATE, SUM(TAT_QTY) AS TOT_TAT   , SUM(SHIP_QTY) AS TOT_SHIP    " + "\n");
            for (int i = 0; i < dtDate.Rows.Count; i++)
            {
                strSqlString.AppendFormat("              , DECODE(SHIP_DATE, '" + dtDate.Rows[i]["TMP2DATE"].ToString() + "', ROUND(SUM(TAT_QTY)/SUM(SHIP_QTY),2), 0) TAT" + i + " " + "\n");
                strSqlString.AppendFormat("              , DECODE(SHIP_DATE, '" + dtDate.Rows[i]["TMP2DATE"].ToString() + "', SUM(TAT_QTY), 0) AS TAT_QTY" + i + " " + "\n");
                strSqlString.AppendFormat("              , DECODE(SHIP_DATE, '" + dtDate.Rows[i]["TMP2DATE"].ToString() + "', SUM(SHIP_QTY), 0) AS SHIP_QTY" + i + " " + "\n");
            }
            strSqlString.AppendFormat("           FROM (   " + "\n");
            strSqlString.AppendFormat("                  SELECT {0}, A.MAT_ID " + "\n", QueryCond2);
            strSqlString.AppendFormat("                       , A.TAT_QTY, B.SHIP_QTY " + "\n");

            if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "DAY")
            {
                strSqlString.AppendFormat("                       , A.SHIP_DATE AS SHIP_DATE" + "\n");
            }
            else if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                strSqlString.AppendFormat("                       , GET_WORK_DATE(A.SHIP_DATE,'W') AS SHIP_DATE " + "\n");
            }
            else if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                strSqlString.AppendFormat("                       , GET_WORK_DATE(A.SHIP_DATE,'M') AS SHIP_DATE " + "\n");
            }

            strSqlString.AppendFormat("                    FROM ( " + "\n");
            strSqlString.AppendFormat("                           SELECT {0}, MAT_ID " + "\n", QueryCond2);
            strSqlString.AppendFormat("                                , SUM(TOTAL_TAT_QTY) AS TAT_QTY, SHIP_DATE " + "\n");
            strSqlString.AppendFormat("                             FROM ( " + "\n");
            strSqlString.AppendFormat("                                    SELECT {0}, TAT.MAT_ID " + "\n", QueryCond1);

            // 2011-06-03-김민우 : Lot_Type별 검색 추가
            if (stLotType.Equals("P%"))
            {
                strSqlString.AppendFormat("                                         , TAT.TOTAL_TAT_QTY_P AS TOTAL_TAT_QTY " + "\n");
            }
            else if (stLotType.Equals("E%"))
            {
                strSqlString.AppendFormat("                                         , TAT.TOTAL_TAT_QTY_E AS TOTAL_TAT_QTY " + "\n");
            }
            else
            {
                strSqlString.AppendFormat("                                         , TAT.TOTAL_TAT_QTY" + "\n");
            }
            
            
            strSqlString.AppendFormat("                                         , TAT.SHIP_DATE " + "\n");
            strSqlString.AppendFormat("                                      FROM CSUMTATMAT@RPTTOMES TAT, MWIPMATDEF MAT, MWIPOPRDEF OPR " + "\n");
            strSqlString.AppendFormat("                                     WHERE 1=1                                                                     " + "\n");
            strSqlString.AppendFormat("                                       AND TAT.FACTORY = '" + cdvFactory.Text.ToString() + "' " + "\n");
            strSqlString.AppendFormat("                                       AND MAT.FACTORY = '" + cdvFactory.Text.ToString() + "' " + "\n");
            strSqlString.AppendFormat("                                       AND OPR.FACTORY = '" + cdvFactory.Text.ToString() + "' " + "\n");
            strSqlString.AppendFormat("                                       AND TAT.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.AppendFormat("                                       AND TAT.FACTORY = OPR.FACTORY " + "\n");
            strSqlString.AppendFormat("                                       AND TAT.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.AppendFormat("                                       AND TAT.OPER = OPR.OPER " + "\n");
            strSqlString.AppendFormat("                                       AND MAT.MAT_VER = 1 " + "\n");
            strSqlString.AppendFormat("                                       AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                                       AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                                       AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                                       AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                                       AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                                       AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                                       AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                                       AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                                       AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                                       AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
            {
                // 2010-03-12-임종우 : 투입대기, 출하대기 체크 유무에 따른 SQL문 생성
                if (ckbInWait.Checked == false && ckbOutWait.Checked == false)
                {
                    strSqlString.AppendFormat("                                       AND OPR.OPER_GRP_1 NOT IN ('-', 'HMK2A','HMK3A') " + "\n");
                }
                else if (ckbInWait.Checked == false && ckbOutWait.Checked == true)
                {
                    strSqlString.AppendFormat("                                       AND OPR.OPER_GRP_1 NOT IN ('-', 'HMK2A') " + "\n");
                }
                else if (ckbInWait.Checked == true && ckbOutWait.Checked == false)
                {
                    strSqlString.AppendFormat("                                       AND OPR.OPER_GRP_1 NOT IN ('-', 'HMK3A') " + "\n");
                }
                else
                {
                    strSqlString.AppendFormat("                                       AND OPR.OPER_GRP_1 NOT IN ('-') " + "\n");
                }
            }
            else if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
            {
                // 2010-03-12-임종우 : 투입대기, 출하대기 체크 유무에 따른 SQL문 생성
                if (ckbInWait.Checked == false && ckbOutWait.Checked == false)
                {
                    strSqlString.AppendFormat("                                       AND OPR.OPER_GRP_1 NOT IN ('-', 'HMK3T','HMK4T') " + "\n");
                }
                else if (ckbInWait.Checked == false && ckbOutWait.Checked == true)
                {
                    strSqlString.AppendFormat("                                       AND OPR.OPER_GRP_1 NOT IN ('-', 'HMK3T') " + "\n");
                }
                else if (ckbInWait.Checked == true && ckbOutWait.Checked == false)
                {
                    strSqlString.AppendFormat("                                       AND OPR.OPER_GRP_1 NOT IN ('-', 'HMK4T') " + "\n");
                }
                else
                {
                    strSqlString.AppendFormat("                                       AND OPR.OPER_GRP_1 NOT IN ('-') " + "\n");
                }
            }

            // 2010-03-12-임종우 : GROUP 검색 조건에 따른 SQL문 생성
            if (cdvGroup.SelectedItem.Equals("HMK2A") || cdvGroup.SelectedItem.Equals("D/A") || cdvGroup.SelectedItem.Equals("W/B") || cdvGroup.SelectedItem.Equals("GATE") || cdvGroup.SelectedItem.Equals("HMK3A"))
            {
                strSqlString.AppendFormat("                                       AND OPR.OPER_GRP_1 IN ('" + cdvGroup.SelectedItem + "') " + "\n");
            }
            else if (cdvGroup.SelectedItem.Equals("SAW"))
            {
                strSqlString.AppendFormat("                                       AND OPR.OPER_GRP_1 IN ('B/G', 'SAW', 'S/P', 'SMT') " + "\n");
            }
            else if (cdvGroup.SelectedItem.Equals("MOLD"))
            {
                strSqlString.AppendFormat("                                       AND OPR.OPER_GRP_1 IN ('MOLD', 'CURE') " + "\n");
            }
            else if (cdvGroup.SelectedItem.Equals("FINISH"))
            {
                strSqlString.AppendFormat("                                       AND OPR.OPER_GRP_1 IN ('M/K', 'TRIM', 'S/B/A', 'TIN', 'SIG', 'AVI', V/I') " + "\n");
            }
            else if (cdvGroup.SelectedItem.Equals("FRONT"))
            {
                strSqlString.AppendFormat("                                       AND OPR.OPER_GRP_1 IN ('HMK2A', 'B/G', 'SAW', 'S/P', 'SMT', 'D/A', 'W/B', 'GATE') " + "\n");
            }
            else if (cdvGroup.SelectedItem.Equals("BACK_END"))
            {
                strSqlString.AppendFormat("                                       AND OPR.OPER_GRP_1 IN ('MOLD', 'CURE', 'M/K', 'TRIM', 'S/B/A', 'TIN', 'SIG', 'AVI', V/I', 'HMK3A') " + "\n");
            }            
            
            strSqlString.AppendFormat("                                  ) " + "\n");
            strSqlString.AppendFormat("                            GROUP BY {0}, MAT_ID, SHIP_DATE" + "\n", QueryCond2);
            strSqlString.AppendFormat("                         ) A" + "\n");
            strSqlString.AppendFormat("                       , (     " + "\n");
            strSqlString.AppendFormat("                           SELECT MAT_ID " + "\n");
            strSqlString.AppendFormat("                                , SHIP_DATE " + "\n");
            strSqlString.AppendFormat("                                , SHIP_QTY" + "\n");
            strSqlString.AppendFormat("                             FROM CSUMTATMAT@RPTTOMES TAT    " + "\n");
            strSqlString.AppendFormat("                            WHERE 1=1     " + "\n");
            strSqlString.AppendFormat("                              AND TAT.FACTORY = '" + cdvFactory.Text.ToString() + "' " + "\n");

            if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                strSqlString.AppendFormat("                              AND TAT.OPER = 'AZ010' " + "\n");
            else if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
                strSqlString.AppendFormat("                              AND TAT.OPER = 'TZ010' " + "\n");
            else if (cdvFactory.Text == "HMKE1")
                strSqlString.AppendFormat("                              AND TAT.OPER = 'EZ010' " + "\n");
            else if (cdvFactory.Text == "HMKS1")
                strSqlString.AppendFormat("                              AND TAT.OPER = 'SZ010' " + "\n");
            else if (cdvFactory.Text == "FGS")
                strSqlString.AppendFormat("                              AND TAT.OPER = 'F0000' " + "\n");

            strSqlString.AppendFormat("                              AND SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
            strSqlString.AppendFormat("                         ) B" + "\n");
            strSqlString.AppendFormat("                   WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                     AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.AppendFormat("                     AND A.SHIP_DATE = B.SHIP_DATE " + "\n");
            strSqlString.AppendFormat("                )   " + "\n");
            strSqlString.AppendFormat("          GROUP BY {0}, SHIP_DATE" + "\n", QueryCond2);
            strSqlString.AppendFormat("       )  " + "\n");
            strSqlString.AppendFormat(" GROUP BY {0} " + "\n", QueryCond2);
            strSqlString.AppendFormat(" ORDER BY {0}" + "\n", QueryCond2);


            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
            ;
            return strSqlString.ToString();
        }

        #endregion

        #region ShowChart

        private void ShowChart(int rowCount)
        {

            int chcnt = 0;
            // 차트 설정
            udcChartFX1.RPT_2_ClearData();
            udcChartFX1.RPT_3_OpenData(2, dtDate.Rows.Count);
            int[] wip_columns = new Int32[dtDate.Rows.Count];
            int[] tat_columns = new Int32[dtDate.Rows.Count];
            int[] columnsHeader = new Int32[dtDate.Rows.Count];

            for (int i = 0; i < wip_columns.Length; i++)
            {
                columnsHeader[i] = 13 + chcnt;
                wip_columns[i] = 13 + chcnt;
                tat_columns[i] = 13 + chcnt;
                chcnt +=3;
            }

            int cnt = 0;
            int cnt2 = 0;            
            double max_temp = 0;
            int max_rownum = 0;
            int[] rows = new Int32[spdData.ActiveSheet.Rows.Count];



            //TAT
            double max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 0 }, tat_columns, SeriseType.Rows);
            max_temp = max1;

            Color color = spdData.ActiveSheet.Cells[1, 10].BackColor;

            for (int j = 1; j < spdData.ActiveSheet.Rows.Count; j++)
            {
                cnt++;
                if (spdData.ActiveSheet.Cells[j, 10].BackColor != color)
                {
                    cnt2++;
                    max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { cnt }, wip_columns, SeriseType.Rows);
                    if (max1 > max_temp)
                    {
                        max_temp = max1;
                        max_rownum = cnt;
                    }
                }
            }
            max1 = max_temp;

            udcChartFX1.RPT_5_CloseData();

            udcChartFX1.RPT_6_SetGallery(ChartType.Line, 0, 1, "TOT", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            udcChartFX1.Series[0].Color = System.Drawing.Color.Black;
            int[] LegBox = new int[cnt2+1];
            LegBox[0] = 0;
            cnt = 0;
            for (int j = 1; j < spdData.ActiveSheet.Rows.Count; j++)
            {
                if (spdData.ActiveSheet.Cells[j, 10].BackColor != color)
                {
                    cnt++;
                    udcChartFX1.RPT_6_SetGallery(ChartType.Line, cnt, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
                    LegBox[cnt] = j;
                
                }
            }
 
            //udcChartFX1.RPT_6_SetGallery(ChartType.Line, "[단위 : %]", AsixType.Y, DataTypes.Initeger, max1 * 1.2);

            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            //udcChartFX1.SerLegBox = true;
            //udcChartFX1.RPT_8_SetSeriseLegend(spdData, rows, 5-1, SoftwareFX.ChartFX.Docked.Top);
            udcChartFX1.RPT_8_SetSeriseLegend(spdData, LegBox, 0, SoftwareFX.ChartFX.Docked.Right);
            //udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "TAT" + Legend }, SoftwareFX.ChartFX.Docked.Top);
            udcChartFX1.PointLabels = true;
            udcChartFX1.AxisY.Gridlines = true;
            udcChartFX1.AxisY.DataFormat.Decimals = 2;
        }
        #endregion

        #region ShowChart2

        private void ShowChart2(int rowCount)
        {
            int chcnt = 0;
            // 차트 설정
            udcChartFX2.RPT_2_ClearData();
            udcChartFX2.RPT_3_OpenData(2, dtDate.Rows.Count);
            int[] wip_columns = new Int32[dtDate.Rows.Count];
            int[] tat_columns = new Int32[dtDate.Rows.Count];
            int[] columnsHeader = new Int32[dtDate.Rows.Count];

            for (int i = 0; i < wip_columns.Length; i++)
            {
                columnsHeader[i] = 13 + chcnt;
                wip_columns[i] = 13 + chcnt;
                tat_columns[i] = 13 + chcnt;
                chcnt += 3;
            }

            int cnt = 0;
            double max1 = 0;
            //double max2 = 0;
            double max_temp = 0;
            int max_rownum = 0;
            int[] rows = new Int32[spdData.ActiveSheet.Rows.Count];



            //TAT
            max1 = udcChartFX2.RPT_4_AddData(spdData, new int[] { rowCount + 0 }, tat_columns, SeriseType.Rows);
            max_temp = max1;
            //max2 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 1 }, wip_columns, SeriseType.Rows);
            Color color = spdData.ActiveSheet.Cells[1, 10].BackColor;

            for (int j = 1; j < spdData.ActiveSheet.Rows.Count; j++)
            {
                
                if (spdData.ActiveSheet.Cells[j, 10].BackColor == color)
                {
                    cnt++;
                    max1 = udcChartFX2.RPT_4_AddData(spdData, new int[] { cnt }, wip_columns, SeriseType.Rows);
                    if (max1 > max_temp)
                    {
                        max_temp = max1;
                        max_rownum = cnt;
                    }
                }
            }
            max1 = max_temp;
            udcChartFX2.RPT_5_CloseData();

            udcChartFX2.RPT_6_SetGallery(ChartType.Bar, 0, 1, "TOT", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            udcChartFX2.Series[0].Color = System.Drawing.Color.Black;
            int[] LegBox = new int[cnt + 1];
            LegBox[0] = 0;
            cnt = 0;
            for (int j = 1; j < spdData.ActiveSheet.Rows.Count; j++)
            {
                if (spdData.ActiveSheet.Cells[j, 10].BackColor == color)
                {
                    cnt++;
                    udcChartFX2.RPT_6_SetGallery(ChartType.Line, cnt, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
                    LegBox[cnt] = j;
                }
            }

            //udcChartFX1.RPT_6_SetGallery(ChartType.Line, "[단위 : %]", AsixType.Y, DataTypes.Initeger, max1 * 1.2);

            udcChartFX2.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            // 범례
            udcChartFX2.RPT_8_SetSeriseLegend(spdData, LegBox, 8, SoftwareFX.ChartFX.Docked.Right);
            //udcChartFX1.RPT_8_SetSeriseLegend(spdData, rows, 9-1, SoftwareFX.ChartFX.Docked.Top);
            //udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "WIP", "TAT" }, SoftwareFX.ChartFX.Docked.Top);
            udcChartFX2.PointLabels = true;
            udcChartFX2.AxisY.Gridlines = true;
            udcChartFX2.AxisY.DataFormat.Decimals = 2;
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
            //2010-03-16-임종우 : 선택된 그래프가 엑셀에 출력되도록 수정..
            if (chkGraph1.Checked == true && chkGraph2.Checked == false)
            {
                ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1, this.lblTitle.Text, null, null);
            }
            else if (chkGraph2.Checked == true && chkGraph1.Checked == false)
            {
                ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX2, this.lblTitle.Text, null, null);
            }
            else //임의로 Chart2가 출력되게 함..원래 이거였으니까...그냥...
            {
                ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX2, this.lblTitle.Text, null, null);
            }                        
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

            if (chkGraph1.Checked == true)
            {
                ShowChart_Selected(i);              // 챠트 그리기
            }
            if (chkGraph2.Checked == true)
            {
                ShowChart_Selected2(i);              // 챠트 그리기
            }

            
        }
        #endregion

        #region ShowChart_Selected
        private void ShowChart_Selected(int rowCount)
        {
            int chcnt = 0;
            // 차트 설정
            udcChartFX1.RPT_2_ClearData();
            udcChartFX1.RPT_3_OpenData(2, dtDate.Rows.Count);
            int[] wip_columns = new Int32[dtDate.Rows.Count];
            int[] tat_columns = new Int32[dtDate.Rows.Count];
            int[] columnsHeader = new Int32[dtDate.Rows.Count];

            for (int i = 0; i < wip_columns.Length; i++)
            {
                columnsHeader[i] = 11 + chcnt;
                wip_columns[i] = 11 + chcnt;
                tat_columns[i] = 11 + chcnt;
                chcnt += 3;
            }

            int cnt = 0;
            double max2 = 0;
            double max_temp = 0;
            int max_rownum = 0;
            int[] rows = new Int32[spdData.ActiveSheet.Rows.Count];



            //TAT
            double max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 0 }, tat_columns, SeriseType.Rows);

            //max2 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 1 }, wip_columns, SeriseType.Rows);
            Color color = spdData.ActiveSheet.Cells[1, 9].BackColor;

            for (int j = 1; j < spdData.ActiveSheet.Rows.Count - 1; j++)
            {
                if (spdData.ActiveSheet.Cells[j, 9].BackColor != color)
                {
                    cnt++;
                    max2 = udcChartFX1.RPT_4_AddData(spdData, new int[] { cnt }, wip_columns, SeriseType.Rows);
                    if (max2 > max_temp)
                    {
                        max_temp = max2;
                        max_rownum = cnt;
                    }
                    rows[j] = cnt;
                }
            }

            udcChartFX1.RPT_5_CloseData();

            udcChartFX1.RPT_6_SetGallery(ChartType.Line, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            udcChartFX1.Series[0].Color = System.Drawing.Color.Black;
            cnt = 0;
            for (int j = 1; j < spdData.ActiveSheet.Rows.Count; j++)
            {
                if (spdData.ActiveSheet.Cells[j, 9].BackColor != color)
                {
                    cnt++;
                    udcChartFX1.RPT_6_SetGallery(ChartType.Line, cnt, 1, "", AsixType.Y2, DataTypes.Initeger, max2 * 1.2);
                }
            }


            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            udcChartFX1.PointLabels = true;
            udcChartFX1.AxisY.Gridlines = true;
            udcChartFX1.AxisY.DataFormat.Decimals = 3;
        }
        #endregion

        #region ShowChart_Selected2
        private void ShowChart_Selected2(int rowCount)
        {
            int chcnt = 0;
            // 차트 설정
            udcChartFX2.RPT_2_ClearData();
            udcChartFX2.RPT_3_OpenData(2, dtDate.Rows.Count);
            int[] wip_columns = new Int32[dtDate.Rows.Count];
            int[] tat_columns = new Int32[dtDate.Rows.Count];
            int[] columnsHeader = new Int32[dtDate.Rows.Count];

            for (int i = 0; i < wip_columns.Length; i++)
            {
                columnsHeader[i] = 11 + chcnt;
                wip_columns[i] = 11 + chcnt;
                tat_columns[i] = 11 + chcnt;
                chcnt += 3;
            }

            int cnt = 0;
            double max2 = 0;
            double max_temp = 0;
            int max_rownum = 0;
            int[] rows = new Int32[spdData.ActiveSheet.Rows.Count];



            //TAT
            double max1 = udcChartFX2.RPT_4_AddData(spdData, new int[] { rowCount + 0 }, tat_columns, SeriseType.Rows);

            Color color = spdData.ActiveSheet.Cells[1, 9].BackColor;

            for (int j = 1; j < spdData.ActiveSheet.Rows.Count - 1; j++)
            {
                if (spdData.ActiveSheet.Cells[j, 9].BackColor == color)
                {
                    cnt++;
                    max2 = udcChartFX2.RPT_4_AddData(spdData, new int[] { cnt }, wip_columns, SeriseType.Rows);
                    if (max2 > max_temp)
                    {
                        max_temp = max2;
                        max_rownum = cnt;
                    }
                    rows[j] = cnt;
                }
            }

            udcChartFX2.RPT_5_CloseData();

            udcChartFX2.RPT_6_SetGallery(ChartType.Line, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            udcChartFX2.Series[0].Color = System.Drawing.Color.Black;
            cnt = 0;
            for (int j = 1; j < spdData.ActiveSheet.Rows.Count; j++)
            {
                if (spdData.ActiveSheet.Cells[j, 9].BackColor == color)
                {
                    cnt++;
                    udcChartFX2.RPT_6_SetGallery(ChartType.Line, cnt, 1, "", AsixType.Y2, DataTypes.Initeger, max2 * 1.2);
                }
            }
                        
            udcChartFX2.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            udcChartFX2.PointLabels = true;
            udcChartFX2.AxisY.Gridlines = true;
            udcChartFX2.AxisY.DataFormat.Decimals = 3;
        }
        #endregion

        //2010-03-22-임종우 : 팩토리 선택에 의한 그룹 조회기능 설정.
        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            // HMKA 일때만 Group 설정 할 수 있게...
            if (cdvFactory.Text != GlobalVariable.gsAssyDefaultFactory)
            {
                cdvGroup.Enabled = false;
            }
            else
            {
                cdvGroup.Enabled = true;
            }
        }

    }
}
