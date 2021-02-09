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
    public partial class PRD010306 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010306<br/>
        /// 클래스요약: 정체 재공 현황<br/>
        /// 작  성  자: 미라콤 양형석<br/>
        /// 최초작성일: 2009-02-02<br/>
        /// 상세  설명: 정체 재공 현황<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// 2011-05-19-임종우 : HMKT1일 경우 H72(장기정체) HOLD 재공 제외 (김권수 요청)
        /// 2013-10-14-김민우: LOT TYPE ALL, P%, E% 구분으로변경
        /// </summary>
        public PRD010306()
        {
            InitializeComponent();

            SortInit();
            GridColumnInit(); //헤더 한줄짜리
            udcChartFX1.RPT_1_ChartInit();

            cdvDate.Value = DateTime.Today;
            txtHoldDate.Text = "2";
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = MAT.FACTORY AND TABLE_NAME = 'H_CUSTOMER' AND ROWNUM=1 AND KEY_1 = MAT.MAT_GRP_1), '-') AS CUSTOMER", "MAT.MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2 as Family", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3 as Package", "MAT.MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4 as Type1", "MAT.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5 as Type2", "MAT.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6 as LD_Count", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7 as Density", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8 as Generation", "MAT.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "MAT.MAT_CMF_10 as PIN_TYPE", "MAT.MAT_CMF_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID as Product", "MAT.MAT_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST_DEVICE", "MAT.MAT_CMF_7 as CUST_DEVICE", "MAT.MAT_CMF_7", false);
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
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PIN TYPE", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("CUST_DEVICE", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);

            spdData.RPT_AddBasicColumn("구분", 0, 11, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.ActiveSheet.Columns[11].Tag = "구분";

            spdData.RPT_AddBasicColumn("TOTAL", 0, 12, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 80);

            spdData.RPT_AddDynamicColumn(udcCUSFromToCondition1, 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

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
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(0, string.Empty));

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, sub + 1, 11, 12, 3, udcCUSFromToCondition1.getItemsFromToValue().Items.Count + 1, btnSort);
                //spdData.DataSource = dt;

                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 3, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                spdData.RPT_FillColumnData(11, new string[] { "Possession WIP", "Congestion WIP", "Retention rate(%)" });

                SetSpreadExpression();

                ShowChart(0);

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


            if (udcCUSFromToCondition1.FromText == "" || udcCUSFromToCondition1.ToText == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
                return false;
            }            

            return true;
        }

        #endregion

        #region MakeSqlString
        private string MakeSqlString(int nSwitch, string strArgs)
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQuery;

            bool isToday = false;

            string strDate = cdvDate.Value.ToString("yyyyMMdd");

            if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
            {
                strDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                isToday = true;
            }
            else
            {
                strDate = strDate + "220000";
            }

            switch (nSwitch)
            {
                case 0: // 스프레드용 쿼리
                    string strTot = string.Empty;
                    string strHold = string.Empty;
                    string strBoyu = string.Empty;

                    string total1 = string.Empty;
                    string total2 = string.Empty;
                    string total3 = string.Empty;

                    string temp1 = string.Empty;
                    string temp2 = string.Empty;
                    string temp3 = string.Empty;

                    ListView.ListViewItemCollection arrOper = null;                    
                    arrOper= udcCUSFromToCondition1.getItemsFromToValue().Items;                    

                    string strOper = string.Empty;
                    foreach (ListViewItem item in arrOper)
                    {
                        strOper += string.Format(", '{0}'", item.Text);
                    }
                    strOper = strOper.Substring(2);

                    strTot = string.Empty;




                    for (int i = 0; i < arrOper.Count; i++)
                    {
                        strTot += "     , SUM(DECODE(LOT.OPER, '" + arrOper[i].Text + "', LOT.QTY, 0)) TOT_" + i.ToString() + "\n";
                        strHold += "     , SUM(CASE WHEN LOT.OPER = '" + arrOper[i].Text + "' AND LOT.HOLD_DATE > " + txtHoldDate.Text + " THEN LOT.QTY ELSE 0 END) HOLD_" + i.ToString() + "\n";
                        strBoyu += "     , 0 BOYU_" + i.ToString() + "\n";

                        temp1 += ", TOT_" + i.ToString();
                        temp2 += ", HOLD_" + i.ToString();
                        temp3 += ", BOYU_" + i.ToString();

                        if (i == 0)
                        {
                            total1 += "TOT_0";
                            total2 += "HOLD_0";
                            total3 += "BOYU_0";
                        }
                        else
                        {
                            total1 += " + TOT_" + i.ToString();
                            total2 += " + HOLD_" + i.ToString();
                            total3 += " + BOYU_" + i.ToString();
                        }
                    }
                    
                  

                    strSqlString.Append("SELECT " + QueryCond1 + "\n");
                    strSqlString.Append("     , ' ' GUBUN " + "\n");

                    strSqlString.AppendFormat("     , ({0}) AS TOTAL1" + "\n", total1);
                    strSqlString.AppendFormat("     {0}" + "\n", temp1);

                    strSqlString.AppendFormat("     , ({0}) AS TOTAL2 " + "\n", total2);
                    strSqlString.AppendFormat("     {0}" + "\n", temp2);

                    strSqlString.AppendFormat("     , ({0}) AS TOTAL3" + "\n", total3);
                    strSqlString.AppendFormat("     {0}" + "\n", temp3);

                    strSqlString.Append("         FROM (     " + "\n");
                    strSqlString.AppendFormat("                SELECT MAT.FACTORY, {0}" + "\n", QueryCond2);
                    strSqlString.Append(strTot + strHold + strBoyu);
                    strSqlString.Append("  FROM (  " + "\n");
                    strSqlString.Append("        SELECT LOT.FACTORY " + "\n");
                    strSqlString.Append("             , LOT.MAT_ID " + "\n");
                    strSqlString.Append("             , OPR.OPER_GRP_1 OPER  " + "\n");
                    strSqlString.Append("             , LOT.LOT_ID " + "\n");
                    strSqlString.Append("             , LOT.QTY_1 QTY " + "\n");

                    if (rbtCreate.Checked)
                        strSqlString.Append("             , TO_DATE('" + strDate + "', 'YYYYMMDDHH24MISS') - TO_DATE(LOT.LOT_CMF_14, 'YYYYMMDDHH24MISS') HOLD_DATE " + "\n");
                    else
                        strSqlString.Append("             , TO_DATE('" + strDate + "', 'YYYYMMDDHH24MISS') - TO_DATE(LOT.OPER_IN_TIME, 'YYYYMMDDHH24MISS') HOLD_DATE " + "\n");

                    if (isToday)
                        strSqlString.Append("          FROM RWIPLOTSTS LOT,  " + "\n");
                    else
                        strSqlString.Append("          FROM RWIPLOTSTS_BOH LOT,  " + "\n");

                    strSqlString.Append("               MWIPOPRDEF OPR  " + "\n");
                    strSqlString.Append("         WHERE 1=1  " + "\n");

                    if (!isToday)
                        strSqlString.Append("           AND LOT.CUTOFF_DT = '" + strDate.Substring(0, 10) + "' " + "\n");

                    // Step
                    if (udcCUSFromToCondition1.FromText != "" && udcCUSFromToCondition1.ToText != "")
                        strSqlString.AppendFormat("           AND OPR.OPER_GRP_1 IN ({0})" + "\n", strOper);

                    strSqlString.Append("           AND LOT.FACTORY = OPR.FACTORY  " + "\n");
                    strSqlString.Append("           AND LOT.OPER = OPR.OPER  " + "\n");
                    strSqlString.Append("           AND LOT.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");

                    if (cdvFactory.Text.Trim() == GlobalVariable.gsAssyDefaultFactory)
                        strSqlString.Append("           AND LOT.OPER NOT IN ('00001','00002')  " + "\n");

                    // 2011-05-19-임종우 : HMKT1일 경우 H72(장기정체) HOLD 재공 제외 (김권수 요청)
                    if (cdvFactory.Text.Trim() == GlobalVariable.gsTestDefaultFactory)
                        strSqlString.Append("           AND LOT.HOLD_CODE <> 'H72'  " + "\n");

                    strSqlString.Append("           AND LOT.LOT_DEL_FLAG = ' '  " + "\n");
                    strSqlString.Append("           AND LOT.MAT_VER = 1  " + "\n");
                    strSqlString.Append("           AND LOT.LOT_TYPE = 'W'  " + "\n");
                    
                    //strSqlString.Append("           AND LOT.LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
                    if (cdvLotType.Text != "ALL")
                    {
                        strSqlString.Append("           AND LOT.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                    }
                    
                    
                    
                    strSqlString.Append("       ) LOT " + "\n");
                    strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    strSqlString.Append("   AND MAT.FACTORY = LOT.FACTORY " + "\n");
                    strSqlString.Append("   AND MAT.MAT_ID = LOT.MAT_ID " + "\n");
                    strSqlString.Append("   AND MAT.MAT_VER = 1 " + "\n");

                    // Product
                    if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                        strSqlString.AppendFormat("   AND MAT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);

                    #region 상세 조회에 따른 SQL문 생성
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
                    #endregion

                    strSqlString.Append(" GROUP BY MAT.FACTORY, " + QueryCond2 + "\n");
                    strSqlString.Append(" ORDER BY MAT.FACTORY, " + QueryCond2 + "\n");
                    strSqlString.Append("              ) MAT " + "\n");


                    break;
                case 1: // 팝업용 쿼리
                    QueryCond1 = tableForm.SelectedValueToQuery;
                    strSqlString.Append("SELECT NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = MAT.FACTORY AND TABLE_NAME = 'H_CUSTOMER' AND ROWNUM=1 AND KEY_1 = MAT.MAT_GRP_1), '-') Customer " + "\n");
                    strSqlString.Append("     , MAT.MAT_GRP_3 AS Pakage " + "\n");
                    strSqlString.Append("     , MAT.MAT_GRP_6 AS Lead " + "\n");
                    strSqlString.Append("     , NVL(MAT.MAT_CMF_10, '-')AS PIN_TYPE" + "\n");                    
                    strSqlString.Append("     , MAT.MAT_ID  " + "\n");
                    strSqlString.Append("     , LOT.LOT_ID" + "\n");
                    strSqlString.Append("     , LOT.OPER  " + "\n");
                    strSqlString.Append("     , SUM(LOT.QTY_1) QTY " + "\n");
                    strSqlString.Append("     , TRUNC(TO_DATE('" + strDate + "','YYYYMMDDHH24MISS') - DECODE(LOT.LOT_CMF_14, ' ', TO_DATE(LOT.LOT_CMF_14,'YYYYMMDDHH24MISS'), TO_DATE(LOT.LOT_CMF_14,'YYYYMMDDHH24MISS')), 2) AS \"당사일\"  " + "\n");
                    strSqlString.Append("     , TRUNC(TO_DATE('" + strDate + "','YYYYMMDDHH24MISS') - TO_DATE(LOT.OPER_IN_TIME,'YYYYMMDDHH24MISS'), 2) AS \"공정일\" " + "\n");

                    if (isToday)
                        strSqlString.Append("  FROM RWIPLOTSTS LOT  " + "\n");
                    else
                        strSqlString.Append("  FROM RWIPLOTSTS_BOH LOT  " + "\n");
                    strSqlString.Append("     , MWIPOPRDEF OPR " + "\n");
                    strSqlString.Append("     , MWIPMATDEF MAT  " + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");

                    if (!isToday)
                        strSqlString.Append("   AND LOT.CUTOFF_DT = '" + strDate.Substring(0, 10) + "' " + "\n");

                    strSqlString.Append("   AND LOT.FACTORY = OPR.FACTORY " + "\n");
                    strSqlString.Append("   AND LOT.OPER = OPR.OPER " + "\n");

                    strSqlString.Append("   AND OPR.OPER_GRP_1 IN (" + strArgs.Split('¿')[0] + ")" + "\n");

                    // Product
                    if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                        strSqlString.AppendFormat("   AND MAT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);

                    strSqlString.Append("   AND LOT.FACTORY = MAT.FACTORY  " + "\n");
                    strSqlString.Append("   AND LOT.MAT_ID = MAT.MAT_ID  " + "\n");
                    strSqlString.Append("   AND LOT.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");

                    if (cdvFactory.Text.Trim() == GlobalVariable.gsAssyDefaultFactory)
                        strSqlString.Append("   AND LOT.OPER NOT IN ('00001','00002')  " + "\n");

                    // 2011-05-19-임종우 : HMKT1일 경우 H72(장기정체) HOLD 재공 제외 (김권수 요청)
                    if (cdvFactory.Text.Trim() == GlobalVariable.gsTestDefaultFactory)
                        strSqlString.Append("   AND LOT.HOLD_CODE <> 'H72'  " + "\n");

                    strSqlString.Append("   AND LOT.MAT_VER = 1 " + "\n");
                    strSqlString.Append("   AND LOT.LOT_DEL_FLAG = ' '  " + "\n");
                    strSqlString.Append("   AND LOT.LOT_TYPE = 'W'  " + "\n");
                    
                    //strSqlString.Append("   AND LOT.LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
                    if (cdvLotType.Text != "ALL")
                    {
                        strSqlString.Append("   AND LOT.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                    }

                    if (rbtCreate.Checked)
                        strSqlString.Append("   AND TRUNC(TO_DATE('" + strDate + "','YYYYMMDDHH24MISS') - TO_DATE(LOT.LOT_CMF_14,'YYYYMMDDHH24MISS'), 2) > " + txtHoldDate.Text + "\n");
                    else
                        strSqlString.Append("   AND TRUNC(TO_DATE('" + strDate + "','YYYYMMDDHH24MISS') - TO_DATE(LOT.OPER_IN_TIME,'YYYYMMDDHH24MISS'), 2) > " + txtHoldDate.Text + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    int row = Convert.ToInt32(strArgs.Split('¿')[1]);
                    if (spdData.ActiveSheet.Cells[row, 0].Text != " ")
                        strSqlString.AppendFormat("   AND NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = MAT.FACTORY AND TABLE_NAME = 'H_CUSTOMER' AND ROWNUM=1 AND KEY_1 = MAT.MAT_GRP_1), '-') = '{0}' " + "\n", spdData.ActiveSheet.Cells[row, 0].Text);

                    if (spdData.ActiveSheet.Cells[row, 1].Text != " ")
                        strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 = '{0}' " + "\n", spdData.ActiveSheet.Cells[row, 1].Text);

                    if (spdData.ActiveSheet.Cells[row, 2].Text != " ")
                        strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 = '{0}' " + "\n", spdData.ActiveSheet.Cells[row, 2].Text);

                    if (spdData.ActiveSheet.Cells[row, 3].Text != " ")
                        strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 = '{0}' " + "\n", spdData.ActiveSheet.Cells[row, 3].Text);

                    if (spdData.ActiveSheet.Cells[row, 4].Text != " ")
                        strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 = '{0}' " + "\n", spdData.ActiveSheet.Cells[row, 4].Text);

                    if (spdData.ActiveSheet.Cells[row, 5].Text != " ")
                        strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 = '{0}' " + "\n", spdData.ActiveSheet.Cells[row, 5].Text);

                    if (spdData.ActiveSheet.Cells[row, 6].Text != " ")
                        strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 = '{0}' " + "\n", spdData.ActiveSheet.Cells[row, 6].Text);

                    if (spdData.ActiveSheet.Cells[row, 7].Text != " ")
                        strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 = '{0}' " + "\n", spdData.ActiveSheet.Cells[row, 7].Text);

                    if (spdData.ActiveSheet.Cells[row, 8].Text != " ")
                        strSqlString.AppendFormat("   AND MAT.MAT_ID = '{0}' " + "\n", spdData.ActiveSheet.Cells[row, 9].Text);
                    #endregion

                    strSqlString.Append(" GROUP BY MAT.FACTORY, LOT.LOT_ID, MAT.MAT_GRP_1 , MAT.MAT_GRP_3, MAT.MAT_GRP_6, MAT_CMF_10, MAT.MAT_ID, LOT.OPER, LOT.LOT_CMF_14, LOT.OPER_IN_TIME " + "\n");
                    strSqlString.Append(" ORDER BY LOT.LOT_ID, MAT.MAT_GRP_1 , MAT.MAT_GRP_3, MAT.MAT_GRP_6, MAT_CMF_10, MAT.MAT_ID, LOT.OPER, LOT.LOT_CMF_14, LOT.OPER_IN_TIME " + "\n");

                    break;
            }

            System.Windows.Forms.Clipboard.Clear();

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #endregion

        #region ToExcel

        /// <summary>
        /// Excel Export
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

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);            
            udcCUSFromToCondition1.sFactory = cdvFactory.txtValue;
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }

        private void ShowChart(int rowCount)
        {
            // 차트 설정
            udcChartFX1.RPT_1_ChartInit();
            udcChartFX1.RPT_2_ClearData();

            if (((DataTable)spdData.DataSource).Rows.Count < 6)
                return;

            int posGubun = spdData.ActiveSheet.Columns["구분"].Index + 2;
            int step = spdData.ActiveSheet.Columns.Count - posGubun;
            if (step < 1)
                return;

            udcChartFX1.RPT_3_OpenData(2, step);

            //udcChartFX1.Legend[0] = "Total";
            for (int i = 0; i < step; i++)
            {
                udcChartFX1.Legend[i] = spdData.ActiveSheet.Columns[posGubun + i].Label;
                udcChartFX1.Value[0, i] = Convert.ToDouble(spdData.ActiveSheet.Cells[0, posGubun + i].Value);
                udcChartFX1.Value[1, i] = Convert.ToDouble(spdData.ActiveSheet.Cells[1, posGubun + i].Value);
                //udcChartFX1.Value[0, 0] += udcChartFX1.Value[0, i];
                //udcChartFX1.Value[1, 0] += udcChartFX1.Value[1, i];
            }

            udcChartFX1.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            udcChartFX1.Series[0].Gallery = SoftwareFX.ChartFX.Gallery.Bar;
            udcChartFX1.Series[1].Gallery = SoftwareFX.ChartFX.Gallery.Lines;
            udcChartFX1.AxisY.Max *= 1.2;

            //udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "재공", "정체" }, SoftwareFX.ChartFX.Docked.Top);
            udcChartFX1.SerLeg[0] = "재공";
            udcChartFX1.SerLeg[1] = "정체";
            udcChartFX1.SerLegBox = true;
            udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;

            // 기타 설정
            udcChartFX1.PointLabels = false;
            udcChartFX1.Series[0].PointLabelColor = Color.Blue;
            udcChartFX1.Series[1].PointLabelColor = Color.Green;
            udcChartFX1.Series[1].LineWidth = 2;

            udcChartFX1.AxisY.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisY2.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisX.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);

        }

        private void SetSpreadExpression()
        {
            int nStartValueColumn = 12;     // 값들이 시작되는 열 위치
            int nStartRow = 0;              // 값들이 시작되는 행 위치
            int nRowCountPerUnit = 3;       // 피봇으로 묶였을 때 하나의 그룹당 행 개수, 피봇이 아니면 1
            int nRowOffset = 2;             // 해당 그룹에서의 행 위치

            // Cell Type
            FarPoint.Win.Spread.CellType.TextCellType ct = new FarPoint.Win.Spread.CellType.TextCellType();
            //FarPoint.Win.Spread.CellType.NumberCellType ctDouble = new FarPoint.Win.Spread.CellType.NumberCellType();
            //ctNumber.DecimalPlaces = 0;
            //ctDouble.DecimalPlaces = 2;

            Double value = 0;
            for (int i = nStartRow + nRowOffset; i < spdData.ActiveSheet.Rows.Count; )
            {
                for (int j = nStartValueColumn; j < spdData.ActiveSheet.Columns.Count; j++)
                {
                    //spdData.ActiveSheet.Cells[i, j].CellType = ctNumber;

                    // 수식
                    value = Convert.ToDouble(spdData.ActiveSheet.Cells[i - 1, j].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[i - 2, j].Value) * 100;
                    if (!double.IsNaN(value) && !double.IsInfinity(value))
                    {
                        spdData.ActiveSheet.Cells[i, j].CellType = ct;
                        if (value == 0)
                        {
                            spdData.ActiveSheet.Cells[i, j].Text = "";
                        }
                        else
                        {
                            spdData.ActiveSheet.Cells[i, j].Value = String.Format("{0:n2}", value);
                        }
                    }
                }
                i += nRowCountPerUnit;
            }
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string strOper = string.Empty;

            if (e.Column <= spdData.ActiveSheet.Columns["구분"].Index)
                return;

            if (((e.Row + 1) % 3) != 2)
                return;

            if (spdData.ActiveSheet.Cells[e.Row, e.Column].Text == " ")
                return;

            if (spdData.ActiveSheet.Columns[e.Column].Label == "TOTAL")
            {
                for (int i = spdData.ActiveSheet.Columns["구분"].Index + 2; i < spdData.ActiveSheet.ColumnCount; i++)
                {
                    if (i == spdData.ActiveSheet.Columns["구분"].Index + 2)
                    {
                        strOper += "'" + spdData.ActiveSheet.Columns[i].Label + "'";
                    }
                    else
                    {
                        strOper += ", '" + spdData.ActiveSheet.Columns[i].Label + "'";
                    }

                }
            }
            else
            {
                strOper = "'" + spdData.ActiveSheet.Columns[e.Column].Label + "'";
            }

            string strHoldDate = txtHoldDate.Text;
            bool isCreate = rbtCreate.Checked;

            string strTitle = (isCreate ? "당사일 기준, " : "공정일 기준, ") + "정체일 " + strHoldDate + "일 이상 " + strOper + "공정 정체 Lot ID List";


            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(1, strOper + "¿" + e.Row.ToString() + "¿" + e.Column.ToString()));


            if (dt != null && dt.Rows.Count > 0)
            {
                System.Windows.Forms.Form frm = new PRD010306_P1(strTitle, dt);
                frm.ShowDialog();
            }

        }
        
    }
}

