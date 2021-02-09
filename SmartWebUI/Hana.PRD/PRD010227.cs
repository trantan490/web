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
    public partial class PRD010227 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010227<br/>
        /// 클래스요약: ASSY 계획 대비 실적 모니터링<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2018-12-04<br/>
        /// 상세  설명: ASSY 계획 대비 실적 모니터링 (김성업과장 요청)<br/>
        /// 변경  내용: <br/>
        /// 2019-01-14-임종우 : PIN_TYPE, 공정검색 기능 추가 (김성업과장 요청)
        /// </summary>

        DataTable DateDT = null;
        //GlobalVariable.FindWeek FindWeek = new GlobalVariable.FindWeek();

        public PRD010227()
        {
            InitializeComponent();
            cdvFromToDate.DaySelector.SelectedValue = "WEEK";
            cdvFromToDate.AutoBindingUserSetting(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd")); 
            cdvFromToDate.ToWeek.Visible = false;
            cdvFromToDate.DaySelector.Visible = false;            

            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;

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
    
            //FindWeek = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");            

            DateTime sStartDate = DateTime.ParseExact(DateDT.Rows[0][0].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

            spdData.RPT_ColumnInit();            

            try
            {                
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);             
                spdData.RPT_AddBasicColumn("Package", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);                
                spdData.RPT_AddBasicColumn("SAP Code", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);

                spdData.RPT_AddBasicColumn("Operation", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("plan", 0, 5, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 0, 6, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);                
                spdData.RPT_AddBasicColumn("Classification", 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);

                spdData.RPT_AddBasicColumn(sStartDate.ToString("MM.dd"), 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(sStartDate.AddDays(1).ToString("MM.dd"), 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(sStartDate.AddDays(2).ToString("MM.dd"), 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(sStartDate.AddDays(3).ToString("MM.dd"), 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(sStartDate.AddDays(4).ToString("MM.dd"), 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(sStartDate.AddDays(5).ToString("MM.dd"), 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(sStartDate.AddDays(6).ToString("MM.dd"), 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(sStartDate.AddDays(7).ToString("MM.dd"), 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(sStartDate.AddDays(8).ToString("MM.dd"), 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(sStartDate.AddDays(9).ToString("MM.dd"), 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(sStartDate.AddDays(10).ToString("MM.dd"), 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(sStartDate.AddDays(11).ToString("MM.dd"), 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(sStartDate.AddDays(12).ToString("MM.dd"), 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(sStartDate.AddDays(13).ToString("MM.dd"), 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                

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
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type", "DECODE(CUST_TYPE, 'SEC', 1, 'Hynix', 2, 'Fabless', 3, 4)", "CUST_TYPE", "CUST_TYPE", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Major", "MAT_GRP_9", "MAT_GRP_9", "MAT_GRP_9", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_10", "MAT_GRP_10", "MAT_GRP_10", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Mat ID", "CONV_MAT_ID", "CONV_MAT_ID", "CONV_MAT_ID", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG Code", "MAT_CMF_11", "MAT_CMF_11", "MAT_CMF_11", false);
        }
        #endregion

        #region 금주 시작일자 가져오기
        private void GetDayArray()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT MIN(SYS_DATE)");
            strSqlString.Append("  FROM MWIPCALDEF");
            strSqlString.Append(" WHERE CALENDAR_ID = 'OTD'");
            strSqlString.Append("   AND SYS_YEAR||LPAD(PLAN_WEEK,2,'0') = '" + cdvFromToDate.HmFromWeek + "'");            

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
            string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값       

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            // kpcs 선택에 의한 분모 값 저장 한다.
            if (ckbKpcs.Checked == true)
            {
                sKpcsValue = "1000";
            }
            else
            {
                sKpcsValue = "1";
            }

            strSqlString.Append("SELECT (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.CUSTOMER) AS CUSTOMER, PKG, SAP_CODE" + "\n");
            strSqlString.Append("     , RESV_FIELD_1 AS PIN_TYPE" + "\n");
            strSqlString.Append("     , OPER" + "\n");
            strSqlString.Append("     , ROUND(TTL_PLAN / " + sKpcsValue + ", 0) AS TTL_PLAN" + "\n");
            strSqlString.Append("     , ROUND(TTL_DEF / " + sKpcsValue + ", 0) AS TTL_DEF" + "\n");            
            strSqlString.Append("     , DECODE(SEQ, 1, 'PLAN', 2, 'ACT', 3, 'BAL', 4, 'BOH') AS GUBUN" + "\n");
            strSqlString.Append("     , ROUND(DECODE(SEQ, 1, PLAN_D0, 2, SHP_D0, 3, BAL_D0, 4, WIP_D0) / " + sKpcsValue + ", 0) AS D0" + "\n");
            strSqlString.Append("     , ROUND(DECODE(SEQ, 1, PLAN_D1, 2, SHP_D1, 3, BAL_D1, 4, WIP_D1) / " + sKpcsValue + ", 0) AS D1" + "\n");
            strSqlString.Append("     , ROUND(DECODE(SEQ, 1, PLAN_D2, 2, SHP_D2, 3, BAL_D2, 4, WIP_D2) / " + sKpcsValue + ", 0) AS D2" + "\n");
            strSqlString.Append("     , ROUND(DECODE(SEQ, 1, PLAN_D3, 2, SHP_D3, 3, BAL_D3, 4, WIP_D3) / " + sKpcsValue + ", 0) AS D3" + "\n");
            strSqlString.Append("     , ROUND(DECODE(SEQ, 1, PLAN_D4, 2, SHP_D4, 3, BAL_D4, 4, WIP_D4) / " + sKpcsValue + ", 0) AS D4" + "\n");
            strSqlString.Append("     , ROUND(DECODE(SEQ, 1, PLAN_D5, 2, SHP_D5, 3, BAL_D5, 4, WIP_D5) / " + sKpcsValue + ", 0) AS D5" + "\n");
            strSqlString.Append("     , ROUND(DECODE(SEQ, 1, PLAN_D6, 2, SHP_D6, 3, BAL_D6, 4, WIP_D6) / " + sKpcsValue + ", 0) AS D6" + "\n");
            strSqlString.Append("     , ROUND(DECODE(SEQ, 1, PLAN_D7, 2, SHP_D7, 3, BAL_D7, 4, WIP_D7) / " + sKpcsValue + ", 0) AS D7" + "\n");
            strSqlString.Append("     , ROUND(DECODE(SEQ, 1, PLAN_D8, 2, SHP_D8, 3, BAL_D8, 4, WIP_D8) / " + sKpcsValue + ", 0) AS D8" + "\n");
            strSqlString.Append("     , ROUND(DECODE(SEQ, 1, PLAN_D9, 2, SHP_D9, 3, BAL_D9, 4, WIP_D9) / " + sKpcsValue + ", 0) AS D9" + "\n");
            strSqlString.Append("     , ROUND(DECODE(SEQ, 1, PLAN_D10, 2, SHP_D10, 3, BAL_D10, 4, WIP_D10) / " + sKpcsValue + ", 0) AS D10" + "\n");
            strSqlString.Append("     , ROUND(DECODE(SEQ, 1, PLAN_D11, 2, SHP_D11, 3, BAL_D11, 4, WIP_D11) / " + sKpcsValue + ", 0) AS D11" + "\n");
            strSqlString.Append("     , ROUND(DECODE(SEQ, 1, PLAN_D12, 2, SHP_D12, 3, BAL_D12, 4, WIP_D12) / " + sKpcsValue + ", 0) AS D12" + "\n");
            strSqlString.Append("     , ROUND(DECODE(SEQ, 1, PLAN_D13, 2, SHP_D13, 3, BAL_D13, 4, WIP_D13) / " + sKpcsValue + ", 0) AS D13" + "\n");
            strSqlString.Append("  FROM RSUMSCMDAT A" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT DISTINCT MAT_GRP_10 AS PKG, VENDOR_ID" + "\n");
            strSqlString.Append("          FROM MWIPMATDEF" + "\n");
            strSqlString.Append("         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("           AND MAT_TYPE = 'FG'" + "\n");

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
            {
                strSqlString.Append("           AND MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");
            }

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("           AND MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion

            strSqlString.Append("       ) B" + "\n");
            strSqlString.Append("     , (SELECT LEVEL AS SEQ FROM DUAL CONNECT BY LEVEL <= 4) C" + "\n");
            strSqlString.Append(" WHERE 1=1   " + "\n");
            strSqlString.Append("   AND A.SAP_CODE = B.VENDOR_ID" + "\n");
            strSqlString.Append("   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("   AND A.PLAN_WEEK = '" + cdvFromToDate.HmFromWeek + "'" + "\n");

            if (cdvOper.Text != "ALL" && cdvOper.Text != "")
                strSqlString.AppendFormat("   AND A.OPER {0} " + "\n", cdvOper.SelectedValueToQueryString);
               
            if (cdvGubun.Text != "ALL" && cdvGubun.Text != "")
                strSqlString.AppendFormat("   AND DECODE(SEQ, 1, 'PLAN', 2, 'ACT', 3, 'BAL', 4, 'BOH') {0} " + "\n", cdvGubun.SelectedValueToQueryString);

            strSqlString.Append(" ORDER BY CUSTOMER, PKG, SAP_CODE, OPER_SEQ DESC, SEQ" + "\n");            

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
                //int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, sub + 1, 9, 10, 4, 9, btnSort);

                //3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 9, 0, 4, true, Align.Center, VerticalAlign.Center);

                //spdData.RPT_FillColumnData(9, new string[] { "plan", "actual", "Achievement rate", "cumulative total" });

                spdData.RPT_AutoFit(false);

                //Color color = spdData.ActiveSheet.Cells[5, 9].BackColor;

                //for (int i = 7; i < spdData.ActiveSheet.Rows.Count; i = i + 4)
                //{
                //    if (spdData.ActiveSheet.Cells[i, 9].BackColor == color)
                //    {
                //        spdData.ActiveSheet.Rows[i].BackColor = Color.LightYellow;
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
                //StringBuilder Condition = new StringBuilder();

                //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null, true);

                spdData.ExportExcel();
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

        private void cdvOper_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT OPER AS Code, '' AS Data" + "\n";
            strQuery += "  FROM ( " + "\n";
            strQuery += "        SELECT OPER_GRP_8 AS OPER, MIN(OPER) AS OPR_SEQ" + "\n";
            strQuery += "          FROM MWIPOPRDEF" + "\n";
            strQuery += "         WHERE 1=1" + "\n";
            strQuery += "           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n";
            strQuery += "           AND REGEXP_LIKE(OPER_GRP_8, 'D/A*|W/B*|MOLD|HMK3A') " + "\n";
            strQuery += "           AND OPER_GRP_8 NOT LIKE '%CURE%' " + "\n";
            strQuery += "           AND OPER_GRP_8 <> 'W/B' " + "\n";
            strQuery += "         GROUP BY OPER_GRP_8 " + "\n";
            strQuery += "         ORDER BY OPR_SEQ " + "\n";
            strQuery += "       ) " + "\n";

            cdvOper.sDynamicQuery = strQuery;
        }

        private void cdvGubun_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DECODE (ROWNUM, 1, A, 2, B, 3, C, 4, D) AS Code, '' Data" + "\n";
            strQuery += "  FROM ( " + "\n";
            strQuery += "        SELECT 1 FROM DUAL CONNECT BY LEVEL <= 4" + "\n";
            strQuery += "       ) " + "\n";
            strQuery += "     , ( " + "\n";
            strQuery += "        SELECT 'PLAN' AS A " + "\n";
            strQuery += "             , 'ACT' AS B" + "\n";
            strQuery += "             , 'BAL' AS C " + "\n";
            strQuery += "             , 'BOH' AS D " + "\n";
            strQuery += "          FROM DUAL " + "\n";
            strQuery += "       ) " + "\n";

            cdvGubun.sDynamicQuery = strQuery;
        }

    }
}
