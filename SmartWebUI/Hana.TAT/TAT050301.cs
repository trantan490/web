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
using System.Collections;

namespace Hana.TAT
{
    public partial class TAT050301 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        private DataTable dtDate = null;
        /// <summary>
        /// 클  래  스: TAT050301<br/>
        /// 클래스요약: TAT Trend by operation<br/>
        /// 작  성  자: 미라콤 김태순<br/>
        /// 최초작성일: 2009-01-19<br/>
        /// 상세  설명: TAT trend 를 조회한다.<br/>
        /// 
        /// 변  경  일: 2009-12-21
        /// 변  경  자: 김준용
        /// 변경  내용: 프로그램 실행 속도 개선을 위해 다시 만듬 (완전히!!!)<br/>
        /// 2011-02-07-임종우 : TAT 단위 시간 단위 추가 표시 (임태성 요청)
        /// 2013-05-08-김민우 : Package2 추가
        /// </summary>
        public TAT050301()
        {
            InitializeComponent();
            OptionInIt(); // 초기화
            string From = DateTime.Now.ToString();
            string To = DateTime.Now.AddDays(-1).ToString();
            udcDurationDate1.AutoBinding(From.Substring(0, 7) + "-01", To.Substring(0, 10)); // 매월 1일 부터 조회

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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1", "MAT_GRP_1", "LOT.MAT_GRP_1", "MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "MAT_GRP_2","MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT_CMF_10", "MAT_CMF_10", "MAT_CMF_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID", "MAT.MAT_ID", "MAT_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package2", "MAT_GRP_10", "MAT_GRP_10", "MAT_GRP_10", false);
            
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
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Package2", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                
                spdData.RPT_AddBasicColumn("Classification", 0, 11, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);

                dtDate = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDate());

                // 날짜수 * 3 만큼의 컬럼을 추가한다.
                // Sub Total 에서 SUB Average = (TAT QTY / SHIP QTY) 를 계산 하기 위해서 이다.
                // 1 : Data , Visible True
                // 2 : TAT QTY , Visible False
                // 3 : SHIP QTY , Visible False
                //
                for (int i = 0; i < dtDate.Rows.Count; i++)
                {
                    spdData.RPT_AddBasicColumn(dtDate.Rows[i][0].ToString(), 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                }
                //for (int i = 0; i < dtDate.Rows.Count; i++)
                //{
                //    spdData.RPT_AddBasicColumn(dtDate.Rows[i][0].ToString() + "_T_QTY", 0, spdData.ActiveSheet.ColumnCount, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.Double2, 80);
                //    spdData.RPT_AddBasicColumn(dtDate.Rows[i][0].ToString() + "_S_QTY", 0, spdData.ActiveSheet.ColumnCount, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.Double2, 80);
                //}
                
                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
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

                spdData.isShowZero = true;

                spdData.DataSource = dt;
                
                //2. 칼럼 고정(필요하다면..)
                spdData.Sheets[0].FrozenColumnCount = 12;
                spdData.Sheets[0].FrozenRowCount = 4;

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                // total 컬럼 색 지정
                spdData.ActiveSheet.Rows[0].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
                spdData.ActiveSheet.Rows[1].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
                spdData.ActiveSheet.Rows[2].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
                spdData.ActiveSheet.Rows[3].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));

                // sub total 컬럼 색 지정
                for (int i = 0; i < spdData.ActiveSheet.Rows.Count;i++ )
                {
                    for( int j=0;j<11;j++)
                    {
                        if (spdData.ActiveSheet.Cells[i, j].Text.Equals("SUB TOTAL")) spdData.ActiveSheet.Rows[i].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));
                    }
                }

                //Chart 생성
                if (spdData.ActiveSheet.RowCount > 0) ShowChart(0);
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

            return true;
        }

        #endregion

        #region OptionInIt
        private void OptionInIt()
        {
            udcDurationDate1.AutoBinding(DateTime.Now.AddDays(-1).ToString(), DateTime.Now.AddDays(-1).ToString());
            udcDurationDate1.DaySelector.Enabled = false;
            cbLotType.Text = "P%";
            dtDate = null;
        }
        #endregion

        #region MakeSqlDate
        /// <summary>
        /// 선택한 기간의 날짜를 가져온다.
        /// </summary>
        /// <returns></returns>
        private string MakeSqlDate()
        {
            StringBuilder strSqlString = new StringBuilder();

            string GET_WORK_DATE = null;
            string QueryCond1 = null;
            string QueryCond2 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            string sFrom = udcDurationDate1.HmFromDay;
            string sTo = udcDurationDate1.HmToDay;

            if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "DAY")
            {
                GET_WORK_DATE = "SUBSTR(GET_WORK_DATE(SHIP_DATE,'D'),5,2)||'/'||SUBSTR(GET_WORK_DATE(SHIP_DATE,'D'),7,2) AS TMPDATE, SHIP_DATE AS TMP2DATE";
            }
            else if (udcDurationDate1.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                GET_WORK_DATE = "SUBSTR(GET_WORK_DATE(SHIP_DATE,'W'),LENGTH(GET_WORK_DATE(SHIP_DATE,'W'))-3,4) AS TMPDATE, SHIP_DATE AS TMP2DATE";
            }
            else
            {
                GET_WORK_DATE = "SUBSTR(GET_WORK_DATE(SHIP_DATE,'M'),LENGTH(GET_WORK_DATE(SHIP_DATE,'M'))-3,4) AS TMPDATE, SHIP_DATE AS TMP2DATE";
            }

            strSqlString.AppendFormat("SELECT DISTINCT " + GET_WORK_DATE + "\n");
            strSqlString.AppendFormat("FROM (" + "\n");
            strSqlString.AppendFormat("SELECT DISTINCT SHIP_DATE" + "\n");


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


            strSqlString.AppendFormat(" AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
            strSqlString.AppendFormat(") ORDER BY TMPDATE" + "\n");

            return strSqlString.ToString();
        }
        #endregion

        #region Make UdcContion
        /// <summary>
        /// 상세조회 부분 반복되는게 짜증나서 만듬
        /// 반복 금지의 법칙!!
        /// 프로그래머는 게을러질 수 있는 권리가 있음
        /// </summary>
        /// <returns></returns>
        private String MakeUDCWipContion()
        {
            StringBuilder strSqlString = new StringBuilder();

            // 상세조회 SQL
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                                AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                                AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                                AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                                AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                                AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                                AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                                AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                                AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                                AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            return strSqlString.ToString();
        }
        #endregion

        #region MakeUDCOrderTotal
        /// <summary>
        /// Check 된 조건을 검색하여 최종 ORDER BY 문을 만들어 낸다.
        /// 0 : 최종 order by
        /// 1 : sub total 최종 select field
        /// 2 : total 최종 select field
        /// 3 : sub total 최종 Group field
        /// </summary>
        /// <returns></returns>
        private ArrayList MakeUDCOrderTotal()
        {
            ArrayList arr = new ArrayList();

            StringBuilder strSqlLastOrder = new StringBuilder();
            StringBuilder strSqlSubField = new StringBuilder();
            StringBuilder strSqlTotalField = new StringBuilder();
            StringBuilder strSqlSubGroup = new StringBuilder();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            ArrayList list = tableForm.SelectedValueToArrayListToValuedTreeNode;
            Boolean isFirst = false;

            strSqlLastOrder.Append("ORDER BY ");
            strSqlSubGroup.Append("GROUP BY ");
            //(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1,  'SUB TOTAL',  'SUB TOTAL',  'SUB TOTAL',  'SUB TOTAL',  'SUB TOTAL',  'SUB TOTAL',  'SUB TOTAL', 'SUB TOTAL',  'SUB TOTAL'
            //'TOTAL',  ' ',  ' ',  ' ',  ' ',  ' ',  ' ',  ' ', ' ',  ' '
            //GROUP BY MAT_GRP_1,  ' ',  ' ',  ' ',  ' ',  ' ',  ' ',  ' ', 'SUB TOTAL' ,  'SUB TOTAL',DATA.SORT,DATA.TITLE
            for(int i=0;i<list.Count;i++)
            {
                ValuedTreeNode node = (ValuedTreeNode)list[i];

                switch(node.Text)
                {
                    case "Customer":
                        if (isFirst == false)
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append("DECODE(MAT_GRP_1,'TOTAL','!','-','11111',MAT_GRP_1)");
                                strSqlSubField.Append("(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1");
                                strSqlTotalField.AppendFormat("'TOTAL'");
                                strSqlSubGroup.Append("MAT_GRP_1");
                                isFirst = true;
                            }
                            else
                            {
                                strSqlLastOrder.Append("' '");
                                strSqlSubField.Append("' '");
                                strSqlTotalField.Append("' '");
                                strSqlSubGroup.Append("' '");
                            }
                        }
                        else
                        {
                            strSqlLastOrder.Append("' '");
                            strSqlSubField.Append("' '");
                            strSqlTotalField.Append("' '");
                            strSqlSubGroup.Append("' '");
                        }
                        break;
                    case "Family":
                        if (isFirst == false)
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append(",DECODE(MAT_GRP_2,'TOTAL','!','-','11111',MAT_GRP_2)");
                                strSqlSubField.Append(",MAT.MAT_GRP_2");
                                strSqlTotalField.AppendFormat(",'TOTAL'");
                                strSqlSubGroup.Append(",MAT_GRP_2");
                                isFirst = true;
                            }
                            else
                            {
                                strSqlLastOrder.Append(",' '");
                                strSqlSubField.Append(",' '");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                        }
                        else
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append(",DECODE(MAT_GRP_2,'SUB TOTAL','zzzzzzzzzzzzzzz',MAT_GRP_2)");
                                strSqlSubField.Append(",'SUB TOTAL'");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                            else
                            {
                                strSqlLastOrder.Append(",' '");
                                strSqlSubField.Append(",' '");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                        }
                        break;
                    case "Package":
                        if (isFirst == false)
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append(",DECODE(MAT_GRP_3,'TOTAL','!','-','11111',MAT_GRP_3)");
                                strSqlSubField.Append(",MAT.MAT_GRP_3");
                                strSqlTotalField.AppendFormat(",'TOTAL'");
                                strSqlSubGroup.Append(",MAT_GRP_3");
                                isFirst = true;
                            }
                            else
                            {
                                strSqlLastOrder.Append(",' '");
                                strSqlSubField.Append(",' '");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                        }
                        else
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append(",DECODE(MAT_GRP_3,'SUB TOTAL','zzzzzzzzzzzzzzz',MAT_GRP_3)");
                                strSqlSubField.Append(",'SUB TOTAL'");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                            else
                            {
                                strSqlLastOrder.Append(",' '");
                                strSqlSubField.Append(",' '");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                        }
                        break;
                    case "Type1":
                        if (isFirst == false)
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append(",DECODE(MAT_GRP_4,'TOTAL','!','-','11111',MAT_GRP_4)");
                                strSqlSubField.Append(",MAT.MAT_GRP_4");
                                strSqlTotalField.AppendFormat(",'TOTAL'");
                                strSqlSubGroup.Append(",MAT_GRP_4");
                                isFirst = true;
                            }
                            else
                            {
                                strSqlLastOrder.Append(",' '");
                                strSqlSubField.Append(",' '");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                        }
                        else
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append(",DECODE(MAT_GRP_4,'SUB TOTAL','zzzzzzzzzzzzzzz',MAT_GRP_4)");
                                strSqlSubField.Append(",'SUB TOTAL'");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                            else
                            {
                                strSqlLastOrder.Append(",' '");
                                strSqlSubField.Append(",' '");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                        }
                        break;
                    case "Type2":
                        if (isFirst == false)
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append(",DECODE(MAT_GRP_5,'TOTAL','!','-','11111',MAT_GRP_5)");
                                strSqlSubField.Append(",MAT.MAT_GRP_5");
                                strSqlTotalField.AppendFormat(",'TOTAL'");
                                strSqlSubGroup.Append(",MAT_GRP_5");
                                isFirst = true;
                            }
                            else
                            {
                                strSqlLastOrder.Append(",' '");
                                strSqlSubField.Append(",' '");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                        }
                        else
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append(",DECODE(MAT_GRP_5,'SUB TOTAL','zzzzzzzzzzzzzzz',MAT_GRP_5)");
                                strSqlSubField.Append(",'SUB TOTAL'");
                                strSqlTotalField.Append(",' '");
                            }
                            else
                            {
                                strSqlLastOrder.Append(",' '");
                                strSqlSubField.Append(",' '");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                        }
                        break;
                    case "LD Count":
                        if (isFirst == false)
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append(",DECODE(MAT_GRP_6,'TOTAL','!','-','11111',MAT_GRP_6)");
                                strSqlSubField.Append(",MAT.MAT_GRP_6");
                                strSqlTotalField.AppendFormat(",'TOTAL'");
                                strSqlSubGroup.Append(",MAT_GRP_6");
                                isFirst = true;
                            }
                            else
                            {
                                strSqlLastOrder.Append(",' '");
                                strSqlSubField.Append(",' '");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                        }
                        else
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append(",DECODE(MAT_GRP_6,'SUB TOTAL','zzzzzzzzzzzzzzz',MAT_GRP_6)");
                                strSqlSubField.Append(",'SUB TOTAL'");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                            else
                            {
                                strSqlLastOrder.Append(",' '");
                                strSqlSubField.Append(",' '");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                        }
                        break;
                    case "Density":
                        if (isFirst == false)
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append(",DECODE(MAT_GRP_7,'TOTAL','!','-','11111',MAT_GRP_7)");
                                strSqlSubField.Append(",MAT.MAT_GRP_7");
                                strSqlTotalField.AppendFormat(",'TOTAL'");
                                strSqlSubGroup.Append(",MAT_GRP_7");
                                isFirst = true;
                            }
                            else
                            {
                                strSqlLastOrder.Append(",' '");
                                strSqlSubField.Append(",' '");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                        }
                        else
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append(",DECODE(MAT_GRP_7,'SUB TOTAL','zzzzzzzzzzzzzzz',MAT_GRP_7)");
                                strSqlSubField.Append(",'SUB TOTAL'");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                            else
                            {
                                strSqlLastOrder.Append(",' '");
                                strSqlSubField.Append(",' '");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                        }
                        break;
                    case "Generation":
                        if (isFirst == false)
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append(",DECODE(MAT_GRP_8,'TOTAL','!','-','11111',MAT_GRP_8)");
                                strSqlSubField.Append(",MAT.MAT_GRP_8");
                                strSqlTotalField.AppendFormat(",'TOTAL'");
                                strSqlSubGroup.Append(",MAT_GRP_8");
                                isFirst = true;
                            }
                            else
                            {
                                strSqlLastOrder.Append(",' '");
                                strSqlSubField.Append(",' '");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                        }
                        else
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append(",DECODE(MAT_GRP_8,'SUB TOTAL','zzzzzzzzzzzzzzz',MAT_GRP_8)");
                                strSqlSubField.Append(",'SUB TOTAL'");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                            else
                            {
                                strSqlLastOrder.Append(",' '");
                                strSqlSubField.Append(",' '");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                        }
                        break;
                    case "Pin Type":
                        if (isFirst == false)
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append(",DECODE(MAT_CMF_10,'TOTAL','!','-','11111',MAT_CMF_10)");
                                strSqlSubField.Append(",MAT.MAT_CMF_10");
                                strSqlTotalField.AppendFormat(",'TOTAL'");
                                strSqlSubGroup.Append(",MAT_CMF_10");
                                isFirst = true;
                            }
                            else
                            {
                                strSqlLastOrder.Append(",' '");
                                strSqlSubField.Append(",' '");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                        }
                        else
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append(",DECODE(MAT_CMF_10,'SUB TOTAL','zzzzzzzzzzzzzzz',MAT_CMF_10)");
                                strSqlSubField.Append(",'SUB TOTAL'");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                            else
                            {
                                strSqlLastOrder.Append(",' '");
                                strSqlSubField.Append(",' '");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                        }
                        break;
                    case "Product":
                        if (isFirst == false)
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append(",DECODE(MAT_ID,'TOTAL','!','-','11111',MAT_ID)");
                                strSqlSubField.Append(",MAT.MAT_ID");
                                strSqlTotalField.AppendFormat(",'TOTAL'");
                                strSqlSubGroup.Append(",MAT_ID");
                                isFirst = true;
                            }
                            else
                            {
                                strSqlLastOrder.Append(",' '");
                                strSqlSubField.Append(",' '");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                        }
                        else
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append(",DECODE(MAT_ID,'SUB TOTAL','zzzzzzzzzzzzzzz',MAT_ID)");
                                strSqlSubField.Append(",'SUB TOTAL'");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                            else
                            {
                                strSqlLastOrder.Append(",' '");
                                strSqlSubField.Append(",' '");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                        }
                        break;
                    case "Package2":
                        if (isFirst == false)
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append(",DECODE(MAT_GRP_10,'TOTAL','!','-','11111',MAT_GRP_10)");
                                strSqlSubField.Append(",MAT.MAT_GRP_10");
                                strSqlTotalField.AppendFormat(",'TOTAL'");
                                strSqlSubGroup.Append(",MAT_GRP_10");
                                isFirst = true;
                            }
                            else
                            {
                                strSqlLastOrder.Append(",' '");
                                strSqlSubField.Append(",' '");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                        }
                        else
                        {
                            if (node.Value.Equals("TRUE"))
                            {
                                strSqlLastOrder.Append(",DECODE(MAT_GRP_10,'SUB TOTAL','zzzzzzzzzzzzzzz',MAT_GRP_10)");
                                strSqlSubField.Append(",'SUB TOTAL'");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                            else
                            {
                                strSqlLastOrder.Append(",' '");
                                strSqlSubField.Append(",' '");
                                strSqlTotalField.Append(",' '");
                                strSqlSubGroup.Append(",' '");
                            }
                        }
                        break;
                }
            }
            strSqlLastOrder.Append(",SORT ");
            strSqlSubGroup.Append(",TITLE,SORT");

            arr.Add(strSqlLastOrder.ToString());
            arr.Add(strSqlSubField.ToString());
            arr.Add(strSqlTotalField.ToString());
            arr.Add(strSqlSubGroup);

            return arr;
        }
        #endregion

        #region MakeSqlString
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            int selectedCount = 0;

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string QueryCond4 = null;

            string sField = null; // 최종 출력 필드

            string sFieldRaw1 = null; // Raw Data 
            string sFieldRaw2 = null;
            string sFieldRaw3 = null;
            string sFieldRaw4 = null;
            
            string sFieldSubtotal1 = null; // Sub Total
            string sFieldSubtotal2 = null;
            string sFieldSubtotal3 = null;
            string sFieldSubtotal4 = null;

            string sFieldTotal1 = null; // Total Data
            string sFieldTotal2 = null;
            string sFieldTotal3 = null;
            string sFieldTotal4 = null;

            String sWipWhere = null; // Wip Where 
            ArrayList arrSQLCond = null; // last order by 

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            string sFrom = udcDurationDate1.HmFromDay;
            string sTo = udcDurationDate1.HmToDay;

            selectedCount = tableForm.GetSelectedCount();

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            sField = udcDurationDate1.getMultyRepeatQuery("DATA_?", "DATA_");

            // 2011-02-07-임종우 : TAT 단위 시간 단위 추가 표시 (임태성 요청)
            if (ckbTime.Checked == true)
            {                
                sFieldRaw1 = udcDurationDate1.getMultyRepeatQuery("        DECODE(DATA.TITLE,'TAT',ROUND(NVL(SUM(WORK_DATE_TQTY_?)/DECODE(SUM(WORK_DATE_SQTY_?),0,NULL,SUM(WORK_DATE_SQTY_?)) * 24,0),2),SUM(WORK_DATE_?)) AS ", "DATA_");
                sFieldRaw2 = udcDurationDate1.getDecodeQuery("        DECODE(WORK_DATE", "SUM(DATA),0) AS ", "WORK_DATE_");
                sFieldRaw3 = udcDurationDate1.getDecodeQuery("                  DECODE(WORK_DATE", "SUM(TAT_QTY),0) AS ", "WORK_DATE_TQTY_");
                sFieldRaw4 = udcDurationDate1.getDecodeQuery("                  DECODE(WORK_DATE", "SUM(SHIP_QTY),0) AS ", "WORK_DATE_SQTY_");

                sFieldTotal1 = udcDurationDate1.getMultyRepeatQuery("        DECODE(DATA.TITLE,'TAT',ROUND(NVL(SUM(WORK_DATE_TQTY_?)/DECODE(SUM(WORK_DATE_SQTY_?),0,NULL,SUM(WORK_DATE_SQTY_?)) * 24,0),2),SUM(WORK_DATE_?)) AS ", "DATA_");
                sFieldTotal2 = udcDurationDate1.getDecodeQuery("                  DECODE(WORK_DATE", "SUM(DATA),0) AS ", "WORK_DATE_");
                sFieldTotal3 = udcDurationDate1.getDecodeQuery("                  DECODE(WORK_DATE", "SUM(TAT_QTY),0) AS ", "WORK_DATE_TQTY_");
                sFieldTotal4 = udcDurationDate1.getDecodeQuery("                  DECODE(WORK_DATE", "SUM(SHIP_QTY),0) AS ", "WORK_DATE_SQTY_");

                sFieldSubtotal1 = udcDurationDate1.getMultyRepeatQuery("        DECODE(DATA.TITLE,'TAT',ROUND(NVL(SUM(WORK_DATE_TQTY_?)/DECODE(SUM(WORK_DATE_SQTY_?),0,NULL,SUM(WORK_DATE_SQTY_?)) * 24,0),2),SUM(WORK_DATE_?)) AS", "DATA_");
                sFieldSubtotal2 = udcDurationDate1.getDecodeQuery("                DECODE(WORK_DATE", " SUM(DATA),0) AS ", "WORK_DATE_");
                sFieldSubtotal3 = udcDurationDate1.getDecodeQuery("                DECODE(WORK_DATE", " SUM(TAT_QTY),0) AS ", "WORK_DATE_TQTY_");
                sFieldSubtotal4 = udcDurationDate1.getDecodeQuery("                DECODE(WORK_DATE", " SUM(SHIP_QTY),0) AS ", "WORK_DATE_SQTY_");
            }
            else
            {
                //sFieldRaw1 = udcDurationDate1.getMultyRepeatQuery("        SUM(WORK_DATE_?) AS ", "DATA_");
                sFieldRaw1 = udcDurationDate1.getMultyRepeatQuery("        DECODE(DATA.TITLE,'TAT',ROUND(NVL(SUM(WORK_DATE_TQTY_?)/DECODE(SUM(WORK_DATE_SQTY_?),0,NULL,SUM(WORK_DATE_SQTY_?)),0),2),SUM(WORK_DATE_?)) AS ", "DATA_");
                sFieldRaw2 = udcDurationDate1.getDecodeQuery("        DECODE(WORK_DATE", "SUM(DATA),0) AS ", "WORK_DATE_");
                sFieldRaw3 = udcDurationDate1.getDecodeQuery("                  DECODE(WORK_DATE", "SUM(TAT_QTY),0) AS ", "WORK_DATE_TQTY_");
                sFieldRaw4 = udcDurationDate1.getDecodeQuery("                  DECODE(WORK_DATE", "SUM(SHIP_QTY),0) AS ", "WORK_DATE_SQTY_");

                sFieldTotal1 = udcDurationDate1.getMultyRepeatQuery("        DECODE(DATA.TITLE,'TAT',ROUND(NVL(SUM(WORK_DATE_TQTY_?)/DECODE(SUM(WORK_DATE_SQTY_?),0,NULL,SUM(WORK_DATE_SQTY_?)),0),2),SUM(WORK_DATE_?)) AS ", "DATA_");
                sFieldTotal2 = udcDurationDate1.getDecodeQuery("                  DECODE(WORK_DATE", "SUM(DATA),0) AS ", "WORK_DATE_");
                sFieldTotal3 = udcDurationDate1.getDecodeQuery("                  DECODE(WORK_DATE", "SUM(TAT_QTY),0) AS ", "WORK_DATE_TQTY_");
                sFieldTotal4 = udcDurationDate1.getDecodeQuery("                  DECODE(WORK_DATE", "SUM(SHIP_QTY),0) AS ", "WORK_DATE_SQTY_");

                sFieldSubtotal1 = udcDurationDate1.getMultyRepeatQuery("        DECODE(DATA.TITLE,'TAT',ROUND(NVL(SUM(WORK_DATE_TQTY_?)/DECODE(SUM(WORK_DATE_SQTY_?),0,NULL,SUM(WORK_DATE_SQTY_?)),0),2),SUM(WORK_DATE_?)) AS", "DATA_");
                sFieldSubtotal2 = udcDurationDate1.getDecodeQuery("                DECODE(WORK_DATE", " SUM(DATA),0) AS ", "WORK_DATE_");
                sFieldSubtotal3 = udcDurationDate1.getDecodeQuery("                DECODE(WORK_DATE", " SUM(TAT_QTY),0) AS ", "WORK_DATE_TQTY_");
                sFieldSubtotal4 = udcDurationDate1.getDecodeQuery("                DECODE(WORK_DATE", " SUM(SHIP_QTY),0) AS ", "WORK_DATE_SQTY_");
            }

            // Where 조건 만들기
            sWipWhere = MakeUDCWipContion();

            // Total Group By 조건 만들기
            arrSQLCond = MakeUDCOrderTotal();

            strSqlString.AppendFormat("SELECT  {0}" + "\n", QueryCond4);
            strSqlString.Append("        ,TITLE" + "\n");
            strSqlString.AppendFormat("        {0}" + "\n", sField);
            strSqlString.Append("FROM    (" + "\n");
            strSqlString.Append("" + "\n");

            /* ------------------------------------------------------------------------------------
             * Raw Data
             * ------------------------------------------------------------------------------------
             */
            strSqlString.Append("/* ------------------------------------------------------ " + "\n");
            strSqlString.Append("                     Raw Data " + "\n");
            strSqlString.Append(" ------------------------------------------------------*/ " + "\n");
            strSqlString.AppendFormat("SELECT  {0}" + "\n", QueryCond1);
            strSqlString.Append("        , DATA.TITLE" + "\n");
            strSqlString.Append("        , DATA.SORT" + "\n");
            strSqlString.AppendFormat("        {0}" + "\n", sFieldRaw1);
            strSqlString.Append("FROM    (" + "\n");
            strSqlString.Append("" + "\n");
            strSqlString.Append("        SELECT  FACTORY" + "\n");
            strSqlString.Append("                ,MAT_ID" + "\n");
            strSqlString.Append("                ,TITLE" + "\n");
            strSqlString.Append("                ,SORT" + "\n");
            strSqlString.AppendFormat("        {0}" + "\n", sFieldRaw2);
            strSqlString.AppendFormat("        {0}" + "\n", sFieldRaw3);
            strSqlString.AppendFormat("        {0}" + "\n", sFieldRaw4);
            strSqlString.Append("        FROM    (" + "\n");
            strSqlString.Append("                SELECT  DATA.FACTORY" + "\n");
            strSqlString.Append("                        ,TMP.WORK_DATE" + "\n");
            strSqlString.Append("                        ,TMP.MAT_ID" + "\n");
            strSqlString.Append("                        ,TMP.TITLE" + "\n");
            strSqlString.Append("                        ,DECODE(DATA.DATA,NULL,0,DATA.DATA) AS DATA" + "\n");
            strSqlString.Append("                        ,TMP.SORT" + "\n");
            strSqlString.Append("                        ,DATA.TAT_QTY" + "\n");
            strSqlString.Append("                        ,DATA.SHIP_QTY" + "\n");
            strSqlString.Append("                FROM    (" + "\n");
            strSqlString.Append("                        SELECT  MAT.FACTORY" + "\n");
            strSqlString.Append("                                ,SHIP_DATE AS WORK_DATE" + "\n");
            strSqlString.Append("                                ,MAT.MAT_ID" + "\n");
            strSqlString.Append("                                ,'TAT' AS TITLE" + "\n");

            if (cbLotType.Text.Equals("P%"))
            {
                strSqlString.Append("                                ,ROUND(SUM(TOTAL_TAT_QTY_P)/SUM(SHIP_QTY_P),2) AS DATA" + "\n");
                strSqlString.Append("                                ,SUM(TOTAL_TAT_QTY_P) AS TAT_QTY" + "\n");
                strSqlString.Append("                                ,SUM(SHIP_QTY_P) AS SHIP_QTY" + "\n");
            }
            else if (cbLotType.Text.Equals("E%"))
            {
                strSqlString.Append("                                ,ROUND(SUM(TOTAL_TAT_QTY_E)/SUM(SHIP_QTY_E),2) AS DATA" + "\n");
                strSqlString.Append("                                ,SUM(TOTAL_TAT_QTY_E) AS TAT_QTY" + "\n");
                strSqlString.Append("                                ,SUM(SHIP_QTY_E) AS SHIP_QTY" + "\n");
            }
            else
            {
                strSqlString.Append("                                ,ROUND(SUM(TOTAL_TAT_QTY)/SUM(SHIP_QTY),2) AS DATA" + "\n");
                strSqlString.Append("                                ,SUM(TOTAL_TAT_QTY) AS TAT_QTY" + "\n");
                strSqlString.Append("                                ,SUM(SHIP_QTY) AS SHIP_QTY" + "\n");
            }
            
            strSqlString.Append("                        FROM    CSUMTATMAT@RPTTOMES TAT," + "\n");
            strSqlString.Append("                                MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                        WHERE   1=1" + "\n");
            strSqlString.Append("                                AND MAT.FACTORY = TAT.FACTORY" + "\n");
            strSqlString.Append("                                AND MAT.MAT_ID = TAT.MAT_ID" + "\n");
            strSqlString.Append("                                AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                AND MAT.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                                AND TAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                                AND SHIP_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.AppendFormat("                                {0}", sWipWhere);
            strSqlString.Append("                        GROUP BY MAT.FACTORY,SHIP_DATE,MAT.MAT_ID" + "\n");
            strSqlString.Append("                        UNION ALL" + "\n");
            strSqlString.Append("                        " + "\n");
            strSqlString.Append("                        SELECT  RCV.FACTORY" + "\n");
            strSqlString.Append("                                ,RCV.WORK_DATE" + "\n");
            strSqlString.Append("                                ,RCV.MAT_ID" + "\n");
            strSqlString.Append("                                ,'INQTY' AS TITLE" + "\n");

            if (chkKpcs.Checked == false) strSqlString.Append("                                ,SUM(RCV_QTY_1) AS DATA" + "\n");
            else strSqlString.Append("                                ,ROUND(SUM(RCV_QTY_1)/1000) AS DATA" + "\n");

            strSqlString.Append("                                ,0 AS TAT_QTY" + "\n");
            strSqlString.Append("                                ,0 AS SHIP_QTY" + "\n");
            strSqlString.Append("                        FROM    VSUMWIPRCV RCV," + "\n");
            strSqlString.Append("                                MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                        WHERE   1=1 " + "\n");
            strSqlString.Append("                                AND RCV.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("                                AND RCV.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                                AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                AND MAT.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                                AND RCV.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                                AND RCV.WORK_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.AppendFormat("                                {0}", sWipWhere);
            strSqlString.Append("                        GROUP BY RCV.FACTORY,RCV.WORK_DATE,RCV.MAT_ID" + "\n");
            strSqlString.Append("" + "\n");
            strSqlString.Append("                        UNION ALL" + "\n");
            strSqlString.Append("                        " + "\n");
            strSqlString.Append("                        SELECT  SHP.CM_KEY_1" + "\n");
            strSqlString.Append("                                ,WORK_DATE" + "\n");
            strSqlString.Append("                                ,MAT.MAT_ID" + "\n");
            strSqlString.Append("                                ,'OUTQTY' AS TITLE" + "\n");

            if (chkKpcs.Checked == false) strSqlString.Append("                                ,SUM(SHP.S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) AS DATA" + "\n");
            else strSqlString.Append("                                ,ROUND(SUM(SHP.S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1)/1000) AS DATA" + "\n");

            strSqlString.Append("                                ,0 AS TAT_QTY" + "\n");
            strSqlString.Append("                                ,0 AS SHIP_QTY" + "\n");
            strSqlString.Append("                        FROM    RSUMFACMOV SHP," + "\n");
            strSqlString.Append("                                MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                        WHERE   1=1" + "\n");
            strSqlString.Append("                                AND SHP.CM_KEY_1 = MAT.FACTORY" + "\n");
            strSqlString.Append("                                AND SHP.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                                AND SHP.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                                AND MAT.MAT_VER = 1" + "\n");
            strSqlString.Append("                                AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                AND SHP.FACTORY NOT IN ('RETURN')" + "\n");
            strSqlString.Append("                                AND SHP.CM_KEY_1 = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                                AND SHP.WORK_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.AppendFormat("                                {0}", sWipWhere);
            strSqlString.Append("                        GROUP BY SHP.CM_KEY_1,MAT.MAT_ID, WORK_DATE" + "\n");
            strSqlString.Append("                        " + "\n");
            strSqlString.Append("                        UNION ALL" + "\n");
            strSqlString.Append("                        " + "\n");
            strSqlString.Append("                        SELECT  WIP.FACTORY" + "\n");
            //strSqlString.Append("                                ,TO_CHAR(TO_DATE(WIP.WORK_DATE,'YYYYMMDD')+1,'YYYYMMDD') AS WORK_DATE" + "\n");
            strSqlString.Append("                                ,TO_CHAR(TO_DATE(WIP.WORK_DATE,'YYYYMMDD'),'YYYYMMDD') AS WORK_DATE" + "\n");
            strSqlString.Append("                                ,MAT.MAT_ID" + "\n");
            strSqlString.Append("                                ,'WIP' AS TITLE" + "\n");

            if (chkKpcs.Checked == false) strSqlString.Append("                                ,SUM(EOH_QTY_1) AS DATA" + "\n");
            else strSqlString.Append("                                ,ROUND(SUM(EOH_QTY_1)/1000) AS DATA" + "\n");

            strSqlString.Append("                                ,0 AS TAT_QTY" + "\n");
            strSqlString.Append("                                ,0 AS SHIP_QTY" + "\n");
            strSqlString.Append("                        FROM    RSUMWIPEOH WIP," + "\n");
            strSqlString.Append("                                MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                        WHERE   1=1" + "\n");
            strSqlString.Append("                                AND WIP.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("                                AND WIP.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                                AND WIP.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                                AND WIP.OPER NOT IN ('00001','00002')" + "\n");
            strSqlString.Append("                                AND WIP.SHIFT = 3" + "\n");
            strSqlString.Append("                                AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                AND WIP.WORK_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.Append("                                AND MAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.AppendFormat("                                {0}", sWipWhere);
            strSqlString.Append("                        GROUP BY WIP.FACTORY,WIP.WORK_DATE,MAT.MAT_ID" + "\n");
            strSqlString.Append("                        ) DATA," + "\n");
            strSqlString.Append("                        (" + "\n");
            strSqlString.Append("                        SELECT  DATA.FACTORY,CAL.WORK_DATE,CAL.TITLE,CAL.SORT,DATA.MAT_ID,0 AS TAT_QTY" + "\n");
            strSqlString.Append("                                ,0 AS SHIP_QTY" + "\n");
            strSqlString.Append("                        FROM    (" + "\n");
            strSqlString.Append("                                    (" + "\n");
            strSqlString.Append("                                    SELECT  '" + GlobalVariable.gsAssyDefaultFactory + "' AS FACTORY" + "\n");
            strSqlString.Append("                                            ,SYS_DATE AS WORK_DATE" + "\n");
            strSqlString.Append("                                            ,'TAT' TITLE" + "\n");
            strSqlString.Append("                                            ,1 AS SORT" + "\n");
            strSqlString.Append("                                    FROM    MWIPCALDEF" + "\n");
            strSqlString.Append("                                    WHERE   1=1" + "\n");
            strSqlString.Append("                                            AND CALENDAR_ID='HM' " + "\n");
            strSqlString.Append("                                            AND SYS_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.Append("                                    )" + "\n");
            strSqlString.Append("                                    UNION ALL" + "\n");
            strSqlString.Append("                                    (" + "\n");
            strSqlString.Append("                                    SELECT  '" + GlobalVariable.gsAssyDefaultFactory + "' AS FACTORY" + "\n");
            strSqlString.Append("                                            ,SYS_DATE AS WORK_DATE" + "\n");
            strSqlString.Append("                                            ,'INQTY' TITLE" + "\n");
            strSqlString.Append("                                            ,2 AS SORT" + "\n");
            strSqlString.Append("                                    FROM    MWIPCALDEF" + "\n");
            strSqlString.Append("                                    WHERE   1=1" + "\n");
            strSqlString.Append("                                            AND CALENDAR_ID='HM' " + "\n");
            strSqlString.Append("                                            AND SYS_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.Append("                                    )" + "\n");
            strSqlString.Append("                                    UNION ALL" + "\n");
            strSqlString.Append("                                    (" + "\n");
            strSqlString.Append("                                    SELECT  '" + GlobalVariable.gsAssyDefaultFactory + "' AS FACTORY" + "\n");
            strSqlString.Append("                                            ,SYS_DATE AS WORK_DATE" + "\n");
            strSqlString.Append("                                            ,'OUTQTY' TITLE" + "\n");
            strSqlString.Append("                                            ,3 AS SORT" + "\n");
            strSqlString.Append("                                    FROM    MWIPCALDEF" + "\n");
            strSqlString.Append("                                    WHERE   1=1" + "\n");
            strSqlString.Append("                                            AND CALENDAR_ID='HM' " + "\n");
            strSqlString.Append("                                            AND SYS_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.Append("                                    )" + "\n");
            strSqlString.Append("                                    UNION ALL" + "\n");
            strSqlString.Append("                                    (" + "\n");
            strSqlString.Append("                                    SELECT  '" + GlobalVariable.gsAssyDefaultFactory + "' AS FACTORY" + "\n");
            strSqlString.Append("                                            ,SYS_DATE AS WORK_DATE" + "\n");
            strSqlString.Append("                                            ,'WIP' TITLE" + "\n");
            strSqlString.Append("                                            ,4 AS SORT" + "\n");
            strSqlString.Append("                                    FROM    MWIPCALDEF" + "\n");
            strSqlString.Append("                                    WHERE   1=1" + "\n");
            strSqlString.Append("                                            AND CALENDAR_ID='HM' " + "\n");
            strSqlString.Append("                                            AND SYS_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.Append("                                    )" + "\n");
            strSqlString.Append("                                ) CAL," + "\n");
            strSqlString.Append("                                (" + "\n");

            strSqlString.Append("                                SELECT  FACTORY" + "\n");
            strSqlString.Append("                                      , MAT.MAT_ID" + "\n");
            strSqlString.Append("                                FROM    MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                                WHERE   1=1" + "\n");
            strSqlString.Append("                                        AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                                        AND MAT.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                                        AND MAT.MAT_VER = 1" + "\n");
            strSqlString.Append("                                        AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.AppendFormat("                                        {0}" + "\n", sWipWhere);
            

            /*
            strSqlString.Append("                                SELECT  SHP.CM_KEY_1 AS FACTORY" + "\n");
            strSqlString.Append("                                        ,WORK_DATE" + "\n");
            strSqlString.Append("                                        ,MAT.MAT_ID" + "\n");
            strSqlString.Append("                                FROM    RSUMFACMOV SHP," + "\n");
            strSqlString.Append("                                        MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                                WHERE   1=1" + "\n");
            strSqlString.Append("                                        AND SHP.CM_KEY_1 = MAT.FACTORY" + "\n");
            strSqlString.Append("                                        AND SHP.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                                        AND SHP.FACTORY NOT IN ('RETURN')" + "\n");
            strSqlString.Append("                                        AND SHP.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                                        AND MAT.MAT_VER = 1" + "\n");
            strSqlString.Append("                                        AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                        AND SHP.CM_KEY_1 = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                                        AND SHP.WORK_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.AppendFormat("                                        {0}" + "\n", sWipWhere);
            strSqlString.Append("                                        GROUP BY SHP.CM_KEY_1,MAT.MAT_ID, WORK_DATE" + "\n");
            */
            strSqlString.Append("                                ) DATA" + "\n");
            //strSqlString.Append("                        WHERE   CAL.WORK_DATE = DATA.WORK_DATE(+)" + "\n");
            strSqlString.Append("                        ORDER BY CAL.WORK_DATE,DATA.MAT_ID,CAL.SORT     " + "\n");
            strSqlString.Append("                        ) TMP" + "\n");
            
            strSqlString.Append("                WHERE   1=1" + "\n");
            strSqlString.Append("                        AND TMP.WORK_DATE = DATA.WORK_DATE(+)" + "\n");
            strSqlString.Append("                        AND TMP.MAT_ID = DATA.MAT_ID(+)" + "\n");
            strSqlString.Append("                        AND TMP.TITLE = DATA.TITLE(+)" + "\n");
            strSqlString.Append("                )" + "\n");
            strSqlString.Append("                GROUP BY FACTORY,WORK_DATE,MAT_ID,TITLE,SORT" + "\n");
            strSqlString.Append("        ) DATA," + "\n");
            strSqlString.Append("        MWIPMATDEF MAT" + "\n");
            strSqlString.Append("WHERE   1=1" + "\n");
            strSqlString.Append("        AND DATA.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("        AND DATA.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("GROUP BY {0},DATA.SORT,DATA.TITLE" + "\n",QueryCond2);
            strSqlString.Append("" + "\n");
            strSqlString.Append("" + "\n");

            
            /* ------------------------------------------------------------------------------------
             * SUB TOTAL Data
             * ------------------------------------------------------------------------------------
             */
            if (selectedCount > 1)
            {
                strSqlString.Append("/* ------------------------------------------------------ " + "\n");
                strSqlString.Append("                     Sub Total " + "\n");
                strSqlString.Append(" ------------------------------------------------------*/ " + "\n");
                strSqlString.Append("UNION ALL" + "\n");
                strSqlString.Append("" + "\n");
                strSqlString.Append("" + "\n");
                strSqlString.AppendFormat("SELECT  {0}" + "\n",arrSQLCond[1]);
                strSqlString.Append("        ,DATA.TITLE" + "\n");
                strSqlString.Append("        ,DATA.SORT" + "\n");
                strSqlString.AppendFormat("{0}" + "\n", sFieldSubtotal1);
                strSqlString.Append("FROM    (" + "\n");
                strSqlString.Append("" + "\n");
                strSqlString.Append("        SELECT  FACTORY" + "\n");
                strSqlString.Append("                ,MAT_ID" + "\n");
                strSqlString.Append("                ,TITLE" + "\n");
                strSqlString.Append("                ,SORT" + "\n");
                strSqlString.AppendFormat("{0}" + "\n", sFieldSubtotal2);
                strSqlString.AppendFormat("{0}" + "\n", sFieldSubtotal3);
                strSqlString.AppendFormat("{0}" + "\n", sFieldSubtotal4);
                strSqlString.Append("" + "\n");
                strSqlString.Append("        FROM    (" + "\n");
                strSqlString.Append("                SELECT  DATA.FACTORY" + "\n");
                strSqlString.Append("                        ,TMP.WORK_DATE" + "\n");
                strSqlString.Append("                        ,TMP.MAT_ID" + "\n");
                strSqlString.Append("                        ,TMP.TITLE" + "\n");
                strSqlString.Append("                        ,DECODE(DATA.DATA,NULL,0,DATA.DATA) AS DATA" + "\n");
                strSqlString.Append("                        ,TMP.SORT" + "\n");
                strSqlString.Append("                        ,DATA.TAT_QTY" + "\n");
                strSqlString.Append("                        ,DATA.SHIP_QTY" + "\n");
                strSqlString.Append("                FROM    (" + "\n");
                strSqlString.Append("                        SELECT  MAT.FACTORY" + "\n");
                strSqlString.Append("                                ,SHIP_DATE AS WORK_DATE" + "\n");
                strSqlString.Append("                                ,MAT.MAT_ID" + "\n");
                strSqlString.Append("                                ,'TAT' AS TITLE" + "\n");
                if (cbLotType.Text.Equals("P%"))
                {
                    strSqlString.Append("                                ,ROUND(SUM(TOTAL_TAT_QTY_P)/SUM(SHIP_QTY_P),2) AS DATA" + "\n");
                    strSqlString.Append("                                ,SUM(TOTAL_TAT_QTY_P) AS TAT_QTY" + "\n");
                    strSqlString.Append("                                ,SUM(SHIP_QTY_P) AS SHIP_QTY" + "\n");
                }
                else if (cbLotType.Text.Equals("E%"))
                {
                    strSqlString.Append("                                ,ROUND(SUM(TOTAL_TAT_QTY_E)/SUM(SHIP_QTY_E),2) AS DATA" + "\n");
                    strSqlString.Append("                                ,SUM(TOTAL_TAT_QTY_E) AS TAT_QTY" + "\n");
                    strSqlString.Append("                                ,SUM(SHIP_QTY_E) AS SHIP_QTY" + "\n");
                }
                else
                {
                    strSqlString.Append("                                ,ROUND(SUM(TOTAL_TAT_QTY)/SUM(SHIP_QTY),2) AS DATA" + "\n");
                    strSqlString.Append("                                ,SUM(TOTAL_TAT_QTY) AS TAT_QTY" + "\n");
                    strSqlString.Append("                                ,SUM(SHIP_QTY) AS SHIP_QTY" + "\n");
                }
                strSqlString.Append("                        FROM    CSUMTATMAT@RPTTOMES TAT," + "\n");
                strSqlString.Append("                                MWIPMATDEF MAT" + "\n");
                strSqlString.Append("                        WHERE   1=1" + "\n");
                strSqlString.Append("                                AND MAT.FACTORY = TAT.FACTORY" + "\n");
                strSqlString.Append("                                AND MAT.MAT_ID = TAT.MAT_ID" + "\n");
                strSqlString.Append("                                AND MAT.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                                AND MAT.MAT_TYPE = 'FG'" + "\n");
                strSqlString.Append("                                AND TAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                                AND SHIP_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
                strSqlString.AppendFormat("                                {0}", sWipWhere);
                strSqlString.Append("                        GROUP BY MAT.FACTORY,SHIP_DATE,MAT.MAT_ID" + "\n");
                strSqlString.Append("                        UNION ALL" + "\n");
                strSqlString.Append("                        " + "\n");
                strSqlString.Append("                        SELECT  RCV.FACTORY" + "\n");
                strSqlString.Append("                                ,RCV.WORK_DATE" + "\n");
                strSqlString.Append("                                ,RCV.MAT_ID" + "\n");
                strSqlString.Append("                                ,'INQTY' AS TITLE" + "\n");

                if (chkKpcs.Checked == false) strSqlString.Append("                                ,SUM(RCV_QTY_1) AS DATA" + "\n");
                else strSqlString.Append("                                ,ROUND(SUM(RCV_QTY_1)/1000) AS DATA" + "\n");

                strSqlString.Append("                                ,0 AS TAT_QTY" + "\n");
                strSqlString.Append("                                ,0 AS SHIP_QTY" + "\n");
                strSqlString.Append("                        FROM    VSUMWIPRCV RCV," + "\n");
                strSqlString.Append("                                MWIPMATDEF MAT" + "\n");
                strSqlString.Append("                        WHERE   1=1 " + "\n");
                strSqlString.Append("                                AND RCV.FACTORY = MAT.FACTORY" + "\n");
                strSqlString.Append("                                AND RCV.MAT_ID = MAT.MAT_ID" + "\n");
                strSqlString.Append("                                AND MAT.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                                AND MAT.MAT_TYPE = 'FG'" + "\n");
                strSqlString.Append("                                AND RCV.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                                AND RCV.WORK_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
                strSqlString.AppendFormat("                                {0}", sWipWhere);
                strSqlString.Append("                        GROUP BY RCV.FACTORY,RCV.WORK_DATE,RCV.MAT_ID" + "\n");
                strSqlString.Append("" + "\n");
                strSqlString.Append("                        UNION ALL" + "\n");
                strSqlString.Append("                        " + "\n");
                strSqlString.Append("                        SELECT  SHP.CM_KEY_1" + "\n");
                strSqlString.Append("                                ,WORK_DATE" + "\n");
                strSqlString.Append("                                ,MAT.MAT_ID" + "\n");
                strSqlString.Append("                                ,'OUTQTY' AS TITLE" + "\n");

                if (chkKpcs.Checked == false) strSqlString.Append("                                ,SUM(SHP.S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) AS DATA" + "\n");
                else strSqlString.Append("                                ,ROUND(SUM(SHP.S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1)/1000) AS DATA" + "\n");

                strSqlString.Append("                                ,0 AS TAT_QTY" + "\n");
                strSqlString.Append("                                ,0 AS SHIP_QTY" + "\n");
                strSqlString.Append("                        FROM    RSUMFACMOV SHP," + "\n");
                strSqlString.Append("                                MWIPMATDEF MAT" + "\n");
                strSqlString.Append("                        WHERE   1=1" + "\n");
                strSqlString.Append("                                AND SHP.CM_KEY_1 = MAT.FACTORY" + "\n");
                strSqlString.Append("                                AND SHP.MAT_ID = MAT.MAT_ID" + "\n");
                strSqlString.Append("                                AND SHP.LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                                AND MAT.MAT_VER = 1" + "\n");
                strSqlString.Append("                                AND MAT.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                                AND SHP.FACTORY NOT IN ('RETURN')" + "\n");
                strSqlString.Append("                                AND SHP.CM_KEY_1 = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                                AND SHP.WORK_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
                strSqlString.AppendFormat("                                {0}", sWipWhere);
                strSqlString.Append("                        GROUP BY SHP.CM_KEY_1,MAT.MAT_ID, WORK_DATE" + "\n");
                strSqlString.Append("                        " + "\n");
                strSqlString.Append("                        UNION ALL" + "\n");
                strSqlString.Append("                        " + "\n");
                strSqlString.Append("                        SELECT  WIP.FACTORY" + "\n");
                //strSqlString.Append("                                ,TO_CHAR(TO_DATE(WIP.WORK_DATE,'YYYYMMDD')+1,'YYYYMMDD') AS WORK_DATE" + "\n");
                strSqlString.Append("                                ,TO_CHAR(TO_DATE(WIP.WORK_DATE,'YYYYMMDD'),'YYYYMMDD') AS WORK_DATE" + "\n");
                strSqlString.Append("                                ,MAT.MAT_ID" + "\n");
                strSqlString.Append("                                ,'WIP' AS TITLE" + "\n");

                if (chkKpcs.Checked == false) strSqlString.Append("                                ,SUM(EOH_QTY_1) AS DATA" + "\n");
                else strSqlString.Append("                                ,ROUND(SUM(EOH_QTY_1)/1000) AS DATA" + "\n");

                strSqlString.Append("                                ,0 AS TAT_QTY" + "\n");
                strSqlString.Append("                                ,0 AS SHIP_QTY" + "\n");
                strSqlString.Append("                        FROM    RSUMWIPEOH WIP," + "\n");
                strSqlString.Append("                                MWIPMATDEF MAT" + "\n");
                strSqlString.Append("                        WHERE   1=1" + "\n");
                strSqlString.Append("                                AND WIP.FACTORY = MAT.FACTORY" + "\n");
                strSqlString.Append("                                AND WIP.MAT_ID = MAT.MAT_ID" + "\n");
                strSqlString.Append("                                AND WIP.LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                                AND WIP.OPER NOT IN ('00001','00002')" + "\n");
                strSqlString.Append("                                AND WIP.SHIFT = 3" + "\n");
                strSqlString.Append("                                AND MAT.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                                AND WIP.WORK_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
                strSqlString.Append("                                AND MAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.AppendFormat("                                {0}", sWipWhere);
                strSqlString.Append("                        GROUP BY WIP.FACTORY,WIP.WORK_DATE,MAT.MAT_ID" + "\n");
                strSqlString.Append("                        ) DATA," + "\n");
                strSqlString.Append("                        (" + "\n");
                strSqlString.Append("                        SELECT  DATA.FACTORY,CAL.WORK_DATE,CAL.TITLE,CAL.SORT,DATA.MAT_ID,0 AS TAT_QTY" + "\n");
                strSqlString.Append("                                ,0 AS SHIP_QTY" + "\n");
                strSqlString.Append("                        FROM    (" + "\n");
                strSqlString.Append("                                    (" + "\n");
                strSqlString.Append("                                    SELECT  '" + cdvFactory.Text + "' AS FACTORY" + "\n");
                strSqlString.Append("                                            ,SYS_DATE AS WORK_DATE" + "\n");
                strSqlString.Append("                                            ,'TAT' TITLE" + "\n");
                strSqlString.Append("                                            ,1 AS SORT" + "\n");
                strSqlString.Append("                                    FROM    MWIPCALDEF" + "\n");
                strSqlString.Append("                                    WHERE   1=1" + "\n");
                strSqlString.Append("                                            AND CALENDAR_ID='HM' " + "\n");
                strSqlString.Append("                                            AND SYS_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
                strSqlString.Append("                                    )" + "\n");
                strSqlString.Append("                                    UNION ALL" + "\n");
                strSqlString.Append("                                    (" + "\n");
                strSqlString.Append("                                    SELECT  '" + cdvFactory.Text + "' AS FACTORY" + "\n");
                strSqlString.Append("                                            ,SYS_DATE AS WORK_DATE" + "\n");
                strSqlString.Append("                                            ,'INQTY' TITLE" + "\n");
                strSqlString.Append("                                            ,2 AS SORT" + "\n");
                strSqlString.Append("                                    FROM    MWIPCALDEF" + "\n");
                strSqlString.Append("                                    WHERE   1=1" + "\n");
                strSqlString.Append("                                            AND CALENDAR_ID='HM' " + "\n");
                strSqlString.Append("                                            AND SYS_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
                strSqlString.Append("                                    )" + "\n");
                strSqlString.Append("                                    UNION ALL" + "\n");
                strSqlString.Append("                                    (" + "\n");
                strSqlString.Append("                                    SELECT  '" + cdvFactory.Text + "' AS FACTORY" + "\n");
                strSqlString.Append("                                            ,SYS_DATE AS WORK_DATE" + "\n");
                strSqlString.Append("                                            ,'OUTQTY' TITLE" + "\n");
                strSqlString.Append("                                            ,3 AS SORT" + "\n");
                strSqlString.Append("                                    FROM    MWIPCALDEF" + "\n");
                strSqlString.Append("                                    WHERE   1=1" + "\n");
                strSqlString.Append("                                            AND CALENDAR_ID='HM' " + "\n");
                strSqlString.Append("                                            AND SYS_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
                strSqlString.Append("                                    )" + "\n");
                strSqlString.Append("                                    UNION ALL" + "\n");
                strSqlString.Append("                                    (" + "\n");
                strSqlString.Append("                                    SELECT  '" + cdvFactory.Text + "' AS FACTORY" + "\n");
                strSqlString.Append("                                            ,SYS_DATE AS WORK_DATE" + "\n");
                strSqlString.Append("                                            ,'WIP' TITLE" + "\n");
                strSqlString.Append("                                            ,4 AS SORT" + "\n");
                strSqlString.Append("                                    FROM    MWIPCALDEF" + "\n");
                strSqlString.Append("                                    WHERE   1=1" + "\n");
                strSqlString.Append("                                            AND CALENDAR_ID='HM' " + "\n");
                strSqlString.Append("                                            AND SYS_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
                strSqlString.Append("                                    )" + "\n");
                strSqlString.Append("                                ) CAL," + "\n");
                strSqlString.Append("                                (" + "\n");


                strSqlString.Append("                                SELECT  FACTORY" + "\n");
                strSqlString.Append("                                      , MAT.MAT_ID" + "\n");
                strSqlString.Append("                                FROM    MWIPMATDEF MAT" + "\n");
                strSqlString.Append("                                WHERE   1=1" + "\n");
                strSqlString.Append("                                        AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                                        AND MAT.MAT_TYPE = 'FG'" + "\n");
                strSqlString.Append("                                        AND MAT.MAT_VER = 1" + "\n");
                strSqlString.Append("                                        AND MAT.DELETE_FLAG = ' '" + "\n");
                strSqlString.AppendFormat("                                        {0}" + "\n", sWipWhere);

                /*
                strSqlString.Append("                                SELECT  SHP.CM_KEY_1 AS FACTORY" + "\n");
                strSqlString.Append("                                        ,WORK_DATE" + "\n");
                strSqlString.Append("                                        ,MAT.MAT_ID" + "\n");
                strSqlString.Append("                                FROM    RSUMFACMOV SHP," + "\n");
                strSqlString.Append("                                        MWIPMATDEF MAT" + "\n");
                strSqlString.Append("                                WHERE   1=1" + "\n");
                strSqlString.Append("                                        AND SHP.CM_KEY_1 = MAT.FACTORY" + "\n");
                strSqlString.Append("                                        AND SHP.MAT_ID = MAT.MAT_ID" + "\n");
                strSqlString.Append("                                        AND SHP.FACTORY NOT IN ('RETURN')" + "\n");
                strSqlString.Append("                                        AND SHP.LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                                        AND MAT.MAT_VER = 1" + "\n");
                strSqlString.Append("                                        AND MAT.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                                        AND SHP.CM_KEY_1 = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                                        AND SHP.WORK_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
                strSqlString.AppendFormat("                                        {0}" + "\n", sWipWhere);
                strSqlString.Append("                                        GROUP BY SHP.CM_KEY_1,MAT.MAT_ID, WORK_DATE" + "\n");
                */
                strSqlString.Append("                                ) DATA" + "\n");
                //strSqlString.Append("                        WHERE   CAL.WORK_DATE = DATA.WORK_DATE(+)" + "\n");
                strSqlString.Append("                        ORDER BY CAL.WORK_DATE,DATA.MAT_ID,CAL.SORT     " + "\n");
                strSqlString.Append("                        ) TMP" + "\n");
                strSqlString.Append("                WHERE   1=1" + "\n");
                strSqlString.Append("                        AND TMP.WORK_DATE = DATA.WORK_DATE(+)" + "\n");
                strSqlString.Append("                        AND TMP.MAT_ID = DATA.MAT_ID(+)" + "\n");
                strSqlString.Append("                        AND TMP.TITLE = DATA.TITLE(+)" + "\n");
                strSqlString.Append("                )" + "\n");
                strSqlString.Append("                GROUP BY FACTORY,WORK_DATE,MAT_ID,TITLE,SORT" + "\n");
                strSqlString.Append("        ) DATA," + "\n");
                strSqlString.Append("        MWIPMATDEF MAT" + "\n");
                strSqlString.Append("WHERE   1=1" + "\n");
                strSqlString.Append("        AND DATA.FACTORY = MAT.FACTORY" + "\n");
                strSqlString.Append("        AND DATA.MAT_ID = MAT.MAT_ID" + "\n");
                strSqlString.AppendFormat("{0}" + "\n",arrSQLCond[3]);
                strSqlString.Append("" + "\n");

            }

            /* ------------------------------------------------------------------------------------
             * TOTAL Data
             * ------------------------------------------------------------------------------------
             */
            strSqlString.Append("/* ------------------------------------------------------ " + "\n");
            strSqlString.Append("                     Total Data " + "\n");
            strSqlString.Append(" ------------------------------------------------------*/ " + "\n");
            strSqlString.Append("UNION ALL" + "\n");
            strSqlString.Append("" + "\n");
            strSqlString.AppendFormat("SELECT  {0}" + "\n", arrSQLCond[2]);
            strSqlString.Append("        ,DATA.TITLE" + "\n");
            strSqlString.Append("        ,DATA.SORT" + "\n");
            strSqlString.AppendFormat("{0}" + "\n", sFieldTotal1);
            strSqlString.Append("FROM    (" + "\n");
            strSqlString.Append("" + "\n");
            strSqlString.Append("        SELECT  FACTORY" + "\n");
            strSqlString.Append("                ,MAT_ID" + "\n");
            strSqlString.Append("                ,TITLE" + "\n");
            strSqlString.Append("                ,SORT" + "\n");
            strSqlString.AppendFormat("{0}" + "\n", sFieldTotal2);
            strSqlString.AppendFormat("{0}" + "\n", sFieldTotal3);
            strSqlString.AppendFormat("{0}" + "\n", sFieldTotal4);
            strSqlString.Append("        FROM    (" + "\n");
            strSqlString.Append("                SELECT  DATA.FACTORY" + "\n");
            strSqlString.Append("                        ,TMP.WORK_DATE" + "\n");
            strSqlString.Append("                        ,TMP.MAT_ID" + "\n");
            strSqlString.Append("                        ,TMP.TITLE" + "\n");
            strSqlString.Append("                        ,DECODE(DATA.DATA,NULL,0,DATA.DATA) AS DATA" + "\n");
            strSqlString.Append("                        ,TMP.SORT" + "\n");
            strSqlString.Append("                        ,DATA.TAT_QTY" + "\n");
            strSqlString.Append("                        ,DATA.SHIP_QTY" + "\n");
            strSqlString.Append("                FROM    (" + "\n");
            strSqlString.Append("                        SELECT  MAT.FACTORY" + "\n");
            strSqlString.Append("                                ,SHIP_DATE AS WORK_DATE" + "\n");
            strSqlString.Append("                                ,MAT.MAT_ID" + "\n");
            strSqlString.Append("                                ,'TAT' AS TITLE" + "\n");
            if (cbLotType.Text.Equals("P%"))
            {
                strSqlString.Append("                                ,ROUND(SUM(TOTAL_TAT_QTY_P)/SUM(SHIP_QTY_P),2) AS DATA" + "\n");
                strSqlString.Append("                                ,SUM(TOTAL_TAT_QTY_P) AS TAT_QTY" + "\n");
                strSqlString.Append("                                ,SUM(SHIP_QTY_P) AS SHIP_QTY" + "\n");
            }
            else if (cbLotType.Text.Equals("E%"))
            {
                strSqlString.Append("                                ,ROUND(SUM(TOTAL_TAT_QTY_E)/SUM(SHIP_QTY_E),2) AS DATA" + "\n");
                strSqlString.Append("                                ,SUM(TOTAL_TAT_QTY_E) AS TAT_QTY" + "\n");
                strSqlString.Append("                                ,SUM(SHIP_QTY_E) AS SHIP_QTY" + "\n");
            }
            else
            {
                strSqlString.Append("                                ,ROUND(SUM(TOTAL_TAT_QTY)/SUM(SHIP_QTY),2) AS DATA" + "\n");
                strSqlString.Append("                                ,SUM(TOTAL_TAT_QTY) AS TAT_QTY" + "\n");
                strSqlString.Append("                                ,SUM(SHIP_QTY) AS SHIP_QTY" + "\n");
            }
            strSqlString.Append("                        FROM    CSUMTATMAT@RPTTOMES TAT," + "\n");
            strSqlString.Append("                                MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                        WHERE   1=1" + "\n");
            strSqlString.Append("                                AND MAT.FACTORY = TAT.FACTORY" + "\n");
            strSqlString.Append("                                AND MAT.MAT_ID = TAT.MAT_ID" + "\n");
            strSqlString.Append("                                AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                AND MAT.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                                AND TAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                                AND SHIP_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.AppendFormat("                                {0}", sWipWhere);
            strSqlString.Append("                        GROUP BY MAT.FACTORY,SHIP_DATE,MAT.MAT_ID" + "\n");
            strSqlString.Append("                        UNION ALL" + "\n");
            strSqlString.Append("                        " + "\n");
            strSqlString.Append("                        SELECT  RCV.FACTORY" + "\n");
            strSqlString.Append("                                ,RCV.WORK_DATE" + "\n");
            strSqlString.Append("                                ,RCV.MAT_ID" + "\n");
            strSqlString.Append("                                ,'INQTY' AS TITLE" + "\n");

            if (chkKpcs.Checked == false) strSqlString.Append("                                ,SUM(RCV_QTY_1) AS DATA" + "\n");
            else strSqlString.Append("                                ,ROUND(SUM(RCV_QTY_1)/1000) AS DATA" + "\n");

            strSqlString.Append("                                ,0 AS TAT_QTY" + "\n");
            strSqlString.Append("                                ,0 AS SHIP_QTY" + "\n");
            strSqlString.Append("                        FROM    VSUMWIPRCV RCV," + "\n");
            strSqlString.Append("                                MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                        WHERE   1=1 " + "\n");
            strSqlString.Append("                                AND RCV.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("                                AND RCV.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                                AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                AND MAT.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                                AND RCV.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                                AND RCV.WORK_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.AppendFormat("                                {0}", sWipWhere);
            strSqlString.Append("                        GROUP BY RCV.FACTORY,RCV.WORK_DATE,RCV.MAT_ID" + "\n");
            strSqlString.Append("" + "\n");
            strSqlString.Append("                        UNION ALL" + "\n");
            strSqlString.Append("                        " + "\n");
            strSqlString.Append("                        SELECT  SHP.CM_KEY_1" + "\n");
            strSqlString.Append("                                ,WORK_DATE" + "\n");
            strSqlString.Append("                                ,MAT.MAT_ID" + "\n");
            strSqlString.Append("                                ,'OUTQTY' AS TITLE" + "\n");

            if (chkKpcs.Checked == false) strSqlString.Append("                                ,SUM(SHP.S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) AS DATA" + "\n");
            else strSqlString.Append("                                ,ROUND(SUM(SHP.S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1)/1000) AS DATA" + "\n");

            strSqlString.Append("                                ,0 AS TAT_QTY" + "\n");
            strSqlString.Append("                                ,0 AS SHIP_QTY" + "\n");
            strSqlString.Append("                        FROM    RSUMFACMOV SHP," + "\n");
            strSqlString.Append("                                MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                        WHERE   1=1" + "\n");
            strSqlString.Append("                                AND SHP.CM_KEY_1 = MAT.FACTORY" + "\n");
            strSqlString.Append("                                AND SHP.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                                AND SHP.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                                AND MAT.MAT_VER = 1" + "\n");
            strSqlString.Append("                                AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                AND SHP.FACTORY NOT IN ('RETURN')" + "\n");
            strSqlString.Append("                                AND SHP.CM_KEY_1 = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                                AND SHP.WORK_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.AppendFormat("                                {0}", sWipWhere);
            strSqlString.Append("                        GROUP BY SHP.CM_KEY_1,MAT.MAT_ID, WORK_DATE" + "\n");
            strSqlString.Append("                        " + "\n");
            strSqlString.Append("                        UNION ALL" + "\n");
            strSqlString.Append("                        " + "\n");
            strSqlString.Append("                        SELECT  WIP.FACTORY" + "\n");
            //strSqlString.Append("                                ,TO_CHAR(TO_DATE(WIP.WORK_DATE,'YYYYMMDD')+1,'YYYYMMDD') AS WORK_DATE" + "\n");
            strSqlString.Append("                                ,TO_CHAR(TO_DATE(WIP.WORK_DATE,'YYYYMMDD'),'YYYYMMDD') AS WORK_DATE" + "\n");
            strSqlString.Append("                                ,MAT.MAT_ID" + "\n");
            strSqlString.Append("                                ,'WIP' AS TITLE" + "\n");

            if (chkKpcs.Checked == false) strSqlString.Append("                                ,SUM(EOH_QTY_1) AS DATA" + "\n");
            else strSqlString.Append("                                ,ROUND(SUM(EOH_QTY_1)/1000) AS DATA" + "\n");

            strSqlString.Append("                                ,0 AS TAT_QTY" + "\n");
            strSqlString.Append("                                ,0 AS SHIP_QTY" + "\n");
            strSqlString.Append("                        FROM    RSUMWIPEOH WIP," + "\n");
            strSqlString.Append("                                MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                        WHERE   1=1" + "\n");
            strSqlString.Append("                                AND WIP.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("                                AND WIP.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                                AND WIP.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                                AND WIP.OPER NOT IN ('00001','00002')" + "\n");
            strSqlString.Append("                                AND WIP.SHIFT = 3" + "\n");
            strSqlString.Append("                                AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                AND WIP.WORK_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.Append("                                AND MAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.AppendFormat("                                {0}", sWipWhere);
            strSqlString.Append("                        GROUP BY WIP.FACTORY,WIP.WORK_DATE,MAT.MAT_ID" + "\n");
            strSqlString.Append("                        ) DATA," + "\n");
            strSqlString.Append("                        (" + "\n");
            strSqlString.Append("                        SELECT  DATA.FACTORY,CAL.WORK_DATE,CAL.TITLE,CAL.SORT,DATA.MAT_ID,0 AS TAT_QTY" + "\n");
            strSqlString.Append("                                ,0 AS SHIP_QTY" + "\n");
            strSqlString.Append("                        FROM    (" + "\n");
            strSqlString.Append("                                    (" + "\n");
            strSqlString.Append("                                    SELECT  '" + cdvFactory.Text + "' AS FACTORY" + "\n");
            strSqlString.Append("                                            ,SYS_DATE AS WORK_DATE" + "\n");
            strSqlString.Append("                                            ,'TAT' TITLE" + "\n");
            strSqlString.Append("                                            ,1 AS SORT" + "\n");
            strSqlString.Append("                                    FROM    MWIPCALDEF" + "\n");
            strSqlString.Append("                                    WHERE   1=1" + "\n");
            strSqlString.Append("                                            AND CALENDAR_ID='HM' " + "\n");
            strSqlString.Append("                                            AND SYS_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.Append("                                    )" + "\n");
            strSqlString.Append("                                    UNION ALL" + "\n");
            strSqlString.Append("                                    (" + "\n");
            strSqlString.Append("                                    SELECT  '" + cdvFactory.Text + "' AS FACTORY" + "\n");
            strSqlString.Append("                                            ,SYS_DATE AS WORK_DATE" + "\n");
            strSqlString.Append("                                            ,'INQTY' TITLE" + "\n");
            strSqlString.Append("                                            ,2 AS SORT" + "\n");
            strSqlString.Append("                                    FROM    MWIPCALDEF" + "\n");
            strSqlString.Append("                                    WHERE   1=1" + "\n");
            strSqlString.Append("                                            AND CALENDAR_ID='HM' " + "\n");
            strSqlString.Append("                                            AND SYS_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.Append("                                    )" + "\n");
            strSqlString.Append("                                    UNION ALL" + "\n");
            strSqlString.Append("                                    (" + "\n");
            strSqlString.Append("                                    SELECT  '" + cdvFactory.Text + "' AS FACTORY" + "\n");
            strSqlString.Append("                                            ,SYS_DATE AS WORK_DATE" + "\n");
            strSqlString.Append("                                            ,'OUTQTY' TITLE" + "\n");
            strSqlString.Append("                                            ,3 AS SORT" + "\n");
            strSqlString.Append("                                    FROM    MWIPCALDEF" + "\n");
            strSqlString.Append("                                    WHERE   1=1" + "\n");
            strSqlString.Append("                                            AND CALENDAR_ID='HM' " + "\n");
            strSqlString.Append("                                            AND SYS_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.Append("                                    )" + "\n");
            strSqlString.Append("                                    UNION ALL" + "\n");
            strSqlString.Append("                                    (" + "\n");
            strSqlString.Append("                                    SELECT  '" + cdvFactory.Text + "' AS FACTORY" + "\n");
            strSqlString.Append("                                            ,SYS_DATE AS WORK_DATE" + "\n");
            strSqlString.Append("                                            ,'WIP' TITLE" + "\n");
            strSqlString.Append("                                            ,4 AS SORT" + "\n");
            strSqlString.Append("                                    FROM    MWIPCALDEF" + "\n");
            strSqlString.Append("                                    WHERE   1=1" + "\n");
            strSqlString.Append("                                            AND CALENDAR_ID='HM' " + "\n");
            strSqlString.Append("                                            AND SYS_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.Append("                                    )" + "\n");
            strSqlString.Append("                                ) CAL," + "\n");
            strSqlString.Append("                                (" + "\n");

            strSqlString.Append("                                SELECT  FACTORY" + "\n");
            strSqlString.Append("                                      , MAT.MAT_ID" + "\n");
            strSqlString.Append("                                FROM    MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                                WHERE   1=1" + "\n");
            strSqlString.Append("                                        AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                                        AND MAT.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                                        AND MAT.MAT_VER = 1" + "\n");
            strSqlString.Append("                                        AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.AppendFormat("                                        {0}" + "\n", sWipWhere);

            /*
            strSqlString.Append("                                SELECT  SHP.CM_KEY_1 AS FACTORY" + "\n");
            strSqlString.Append("                                        ,WORK_DATE" + "\n");
            strSqlString.Append("                                        ,MAT.MAT_ID" + "\n");
            strSqlString.Append("                                FROM    RSUMFACMOV SHP," + "\n");
            strSqlString.Append("                                        MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                                WHERE   1=1" + "\n");
            strSqlString.Append("                                        AND SHP.CM_KEY_1 = MAT.FACTORY" + "\n");
            strSqlString.Append("                                        AND SHP.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                                        AND SHP.FACTORY NOT IN ('RETURN')" + "\n");
            strSqlString.Append("                                        AND SHP.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                                        AND MAT.MAT_VER = 1" + "\n");
            strSqlString.Append("                                        AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                        AND SHP.CM_KEY_1 = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                                        AND SHP.WORK_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.AppendFormat("                                        {0}" + "\n", sWipWhere);
            strSqlString.Append("                                GROUP BY SHP.CM_KEY_1,MAT.MAT_ID, WORK_DATE" + "\n");
            */
            strSqlString.Append("                                ) DATA" + "\n");
            //strSqlString.Append("                        WHERE   CAL.WORK_DATE = DATA.WORK_DATE(+)" + "\n");
            strSqlString.Append("                        ORDER BY CAL.WORK_DATE,DATA.MAT_ID,CAL.SORT     " + "\n");
            strSqlString.Append("                        ) TMP" + "\n");
            strSqlString.Append("                WHERE   1=1" + "\n");
            strSqlString.Append("                        AND TMP.WORK_DATE = DATA.WORK_DATE(+)" + "\n");
            strSqlString.Append("                        AND TMP.MAT_ID = DATA.MAT_ID(+)" + "\n");
            strSqlString.Append("                        AND TMP.TITLE = DATA.TITLE(+)" + "\n");
            strSqlString.Append("                )" + "\n");
            strSqlString.Append("                GROUP BY FACTORY,WORK_DATE,MAT_ID,TITLE,SORT" + "\n");
            strSqlString.Append("        ) DATA," + "\n");
            strSqlString.Append("        MWIPMATDEF MAT" + "\n");
            strSqlString.Append("WHERE   1=1" + "\n");
            strSqlString.Append("        AND DATA.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("        AND DATA.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("GROUP BY 'TOTAL' ,  ' ',  ' ',  ' ',  ' ',  ' ',  ' ',  ' ', ' ' ,' ',DATA.SORT,DATA.TITLE" + "\n");
            strSqlString.Append(")" + "\n");
            //strSqlString.Append("ORDER BY DECODE(MAT_GRP_1,'TOTAL','A',MAT_GRP_1),  ' ',  ' ',  ' ',  ' ',  ' ',  ' ',  ' ', DECODE(MAT_CMF_10,'SUB TOTAL','ZZZZZZZZZZZZZZ',MAT_CMF_10),  DECODE(MAT_ID,'SUB TOTAL','ZZZZZZZZZZZZZZ',MAT_ID),SORT" + "\n");
            strSqlString.AppendFormat("{0}" + "\n", arrSQLCond[0]);

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

            double max_temp = 0;
            double max1 = 0;
            // 차트 설정
            udcChartFX1.RPT_2_ClearData();
            udcChartFX1.RPT_3_OpenData(4, udcDurationDate1.SubtractBetweenFromToDate + 1);
            int[] tat_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];
            int[] in_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];
            int[] out_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];
            int[] wip_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];
            int[] columnsHeader = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];

            for (int i = 0; i < wip_columns.Length; i++)
            {
                columnsHeader[i] = 12 + i;
                tat_columns[i] = 12 + i;
                in_columns[i] = 12 + i;
                out_columns[i] = 12 + i;
                wip_columns[i] = 12 + i;
            }


            //WIP
            max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 3 }, wip_columns, SeriseType.Rows);
            if (max1 > max_temp)
            {
                max_temp = max1;
            }

            //OUT
            max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 1 }, out_columns, SeriseType.Rows);
            if (max1 > max_temp)
            {
                max_temp = max1;
            }

            //IN
            max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 2 }, in_columns, SeriseType.Rows);
            if (max1 > max_temp)
            {
                max_temp = max1;
            }
            max1 = max_temp;

            //TAT
            double max2 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 0 }, tat_columns, SeriseType.Rows);

            udcChartFX1.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            String legendDescWip = "WIP [단위 : ea]";
            String legendDescIn = "IN [단위 : ea]";
            String legendDescOut = "OUT [단위 : ea]";
            String legendDescTAT = "TAT [단위 : day]";
            if (chkKpcs.Checked == true)
            {
                legendDescWip = "WIP [단위 : kpcs]";
                legendDescIn = "IN [단위 : ea]";
                legendDescOut = "OUT [단위 : ea]";
            }
            udcChartFX1.RPT_6_SetGallery(ChartType.CurveArea, 0, 1, legendDescWip, AsixType.Y2, DataTypes.Initeger, max1 * 1.2);
            udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 1, 1, legendDescIn, AsixType.Y2, DataTypes.Initeger, max1 * 1.2);
            udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 2, 1, legendDescOut, AsixType.Y2, DataTypes.Initeger, max1 * 1.2);
            udcChartFX1.RPT_6_SetGallery(ChartType.Line, 3, 1, legendDescTAT, AsixType.Y, DataTypes.Initeger, max2 * 1.2);
            udcChartFX1.Series[0].Color = System.Drawing.Color.LawnGreen;
            udcChartFX1.Series[1].Color = System.Drawing.Color.Red;
            udcChartFX1.Series[2].Color = System.Drawing.Color.LightSeaGreen;
            udcChartFX1.Series[3].Color = System.Drawing.Color.BlueViolet;
            udcChartFX1.Series[0].PointLabels = false;
            udcChartFX1.Series[1].PointLabels = false;
            udcChartFX1.Series[2].PointLabels = false;
            udcChartFX1.Series[3].PointLabels = true;
            udcChartFX1.Series[3].PointLabelColor = Color.Black;
            udcChartFX1.Series[3].PointLabelAlign = SoftwareFX.ChartFX.LabelAlign.Bottom;

            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "WIP", "IN", "OUT", "TAT" }, SoftwareFX.ChartFX.Docked.Top);
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

        #region ShowChart_Selected
        private void ShowChart_Selected(int rowCount)
        {
            double max_temp = 0;
            double max1 = 0;
            // 차트 설정
            udcChartFX1.RPT_2_ClearData();
            udcChartFX1.RPT_3_OpenData(4, udcDurationDate1.SubtractBetweenFromToDate + 1);
            int[] tat_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];
            int[] in_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];
            int[] out_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];
            int[] wip_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];
            int[] columnsHeader = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];

            for (int i = 0; i < wip_columns.Length; i++)
            {
                columnsHeader[i] = 12 + dtDate.Rows.Count + i;
                tat_columns[i] = 1 + i;
                in_columns[i] = 1 + i;
                out_columns[i] = 1 + i;
                wip_columns[i] = 1 + i;
            }


            //WIP
            max1 = udcChartFX1.RPT_4_AddData(udcFarPoint1, new int[] { rowCount + 0 }, wip_columns, SeriseType.Rows);
            if (max1 > max_temp)
            {
                max_temp = max1;
            }

            //OUT
            max1 = udcChartFX1.RPT_4_AddData(udcFarPoint1, new int[] { rowCount + 1 }, out_columns, SeriseType.Rows);
            if (max1 > max_temp)
            {
                max_temp = max1;
            }

            //IN
            max1 = udcChartFX1.RPT_4_AddData(udcFarPoint1, new int[] { rowCount + 2 }, in_columns, SeriseType.Rows);
            if (max1 > max_temp)
            {
                max_temp = max1;
            }
            max1 = max_temp;

            //TAT
            double max2 = udcChartFX1.RPT_4_AddData(udcFarPoint1, new int[] { rowCount + 3 }, tat_columns, SeriseType.Rows);

            udcChartFX1.RPT_5_CloseData();


            //각 Serise별로 다른 타입을 사용할 경우
            udcChartFX1.RPT_6_SetGallery(ChartType.Area, 0, 1, "WIP [단위 : ea]", AsixType.Y2, DataTypes.Initeger, max1 * 1.2);
            udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 1, 1, "IN [단위 : ea]", AsixType.Y2, DataTypes.Initeger, max1 * 1.2);
            udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 2, 1, "OUT [단위 : ea]", AsixType.Y2, DataTypes.Initeger, max1 * 1.2);
            udcChartFX1.RPT_6_SetGallery(ChartType.Line, 3, 1, "TAT [단위 : day]", AsixType.Y, DataTypes.Initeger, max2 * 1.2);
            udcChartFX1.Series[0].Color = System.Drawing.Color.YellowGreen;
            udcChartFX1.Series[1].Color = System.Drawing.Color.Red;
            udcChartFX1.Series[2].Color = System.Drawing.Color.LightSeaGreen;
            udcChartFX1.Series[3].Color = System.Drawing.Color.Blue;

            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "WIP", "IN", "OUT", "TAT" }, SoftwareFX.ChartFX.Docked.Top);
            udcChartFX1.PointLabels = true;
            udcChartFX1.AxisY.Gridlines = true;
            udcChartFX1.AxisY.DataFormat.Decimals = 2;
        }
        #endregion

    }
}
