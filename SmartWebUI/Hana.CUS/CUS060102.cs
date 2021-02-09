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

namespace Hana.CUS
{
    public partial class CUS060102 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: CUS060102<br/>
        /// 클래스요약: 고객사 SHIP<br/>
        /// 작  성  자: 미라콤 김민규<br/>
        /// 최초작성일: 2008-10-09<br/>
        /// 상세  설명: 고객사 SHIP<br/>
        /// 변경  내용: <br/>
        /// 2013-01-28-임종우 : Receive Time, TAT 항목 추가 (김태완 요청)
        /// 2013-01-28-임종우 : 고객사를 제외한 사용자 접속 시 업체 선택 가능하도록 수정
        /// 2015-06-19-임종우 : partial Ship 로직 반영
        /// 2019-07-25-임종우 : BUMP 데이터 추가
        /// </summary>     
        

        public CUS060102()
        {
            InitializeComponent();
            SortInit();
            GridColumnInit(); //해더 한줄짜리 샘플      
            cdvFromTo.AutoBinding();

            this.udcWIPCondition1.Enabled = false;
            //this.udcWIPCondition2.Enabled = false;
            //this.udcWIPCondition3.Enabled = false;
            //this.udcWIPCondition4.Enabled = false;
            //this.udcWIPCondition5.Enabled = false;
            //this.udcWIPCondition6.Enabled = false;
            //this.udcWIPCondition7.Enabled = false;
            //this.udcWIPCondition8.Enabled = false;

            //관리자만 고객사 화면에서 CUSTOMER 별로 조회 가능
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "OPERATOR_GERNERAL" || GlobalVariable.gsUserGroup == "PRODUCTION_MANAGER")
            {
                this.udcWIPCondition1.Enabled = true;
            }          
            else
            {
                this.udcWIPCondition1.Enabled = false;            
            }
        }

        private Boolean CheckField()
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }          
            return true;
        }

        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string sFrom = string.Empty;
            string sTo = string.Empty;
                        
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            
            strSqlString.AppendFormat(" SELECT {0} " + "\n", QueryCond2);
            strSqlString.Append("      , A.LOT_ID" + "\n");
            strSqlString.Append("      , SUM(A.SHIP_QTY_1)" + "\n");
            strSqlString.Append("      , (SELECT COUNT(A.LOT_ID) FROM CWIPLOTWFR WHERE LOT_ID = A.LOT_ID) AS SHIP_QTY_2" + "\n");
            strSqlString.Append("      , A.FROM_FACTORY " + "\n");
            strSqlString.Append("      , A.TO_FACTORY      " + "\n");
            strSqlString.Append("      , A.FROM_OPER        " + "\n");
            strSqlString.Append("      , A.TRAN_USER_ID" + "\n");
            strSqlString.Append("      , TO_CHAR(TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS'),'YYYY-MM-DD PM HH12:MI:SS') AS RECEIVE_TIME" + "\n");
            strSqlString.Append("      , TO_CHAR(TO_DATE(A.TRAN_TIME, 'YYYYMMDDHH24MISS'),'YYYY-MM-DD PM HH12:MI:SS') AS TRANS_TIME" + "\n");
            strSqlString.Append("      , ROUND(TO_DATE(A.TRAN_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS'), 2) AS TAT" + "\n");
            strSqlString.Append("   FROM (    " + "\n");
            strSqlString.Append("        SELECT *" + "\n");
            strSqlString.Append("          FROM VWIPSHPLOT" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND FROM_FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("           AND TO_FACTORY = 'CUSTOMER'" + "\n");
            strSqlString.Append("           AND OWNER_CODE = 'PROD'" + "\n");
            strSqlString.Append("           AND FROM_OPER IN ('AZ010','AZ009','TZ010','EZ010', 'F0000', 'BZ010')" + "\n");
            //strSqlString.Append("           AND TO_OPER = ' ' " + "\n");
            
            //기간 선택시 SQL 조건문 생성
            if (cdvFromTo.DaySelector.SelectedValue.ToString() == "DAY")
            {
                sFrom = cdvFromTo.FromDate.Text;
                sTo = cdvFromTo.ToDate.Text.Replace("-", "") + "215959";

                DateTime FromDate = DateTime.Parse(sFrom);
                FromDate = FromDate.AddDays(-1);

                sFrom = FromDate.ToString("yyyyMMdd") + "220000";
                
                strSqlString.AppendFormat("           AND TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);

            }
            else if (cdvFromTo.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                StringBuilder strSqlString1 = new StringBuilder();

                sFrom = cdvFromTo.FromWeek.SelectedValue.ToString();
                sTo = cdvFromTo.ToWeek.SelectedValue.ToString();

                string sFromWeek = sFrom.Substring(4,2).Replace('0', ' ').Trim();
                string sToWeek = sTo.Substring(4,2).Replace('0' ,' ').Trim();

                strSqlString1.Append("SELECT A.SYS_DATE, B.SYS_DATE " + "\n");
                strSqlString1.Append("  FROM " + "\n");
                strSqlString1.Append("       (" + "\n");
                strSqlString1.Append("       SELECT SYS_DATE " + "\n");
                strSqlString1.Append("         FROM MWIPCALDEF " + "\n");
                strSqlString1.Append("        WHERE 1=1 " + "\n");
                strSqlString1.Append("          AND CALENDAR_ID = 'HM' " + "\n");
                strSqlString1.AppendFormat("          AND PLAN_YEAR = '{0}' " + "\n", sFrom.Substring(0,4));
                strSqlString1.AppendFormat("          AND PLAN_WEEK = '{0}' " + "\n", sFromWeek);
                strSqlString1.Append("     ORDER BY SYS_DATE ASC       " + "\n");
                strSqlString1.Append("       ) A," + "\n");
                strSqlString1.Append("       (" + "\n");
                strSqlString1.Append("       SELECT SYS_DATE " + "\n");
                strSqlString1.Append("         FROM MWIPCALDEF " + "\n");
                strSqlString1.Append("        WHERE 1=1 " + "\n");
                strSqlString1.Append("          AND CALENDAR_ID = 'HM' " + "\n");
                strSqlString1.AppendFormat("          AND PLAN_YEAR = '{0}' " + "\n", sTo.Substring(0,4));
                strSqlString1.AppendFormat("          AND PLAN_WEEK = '{0}' " + "\n", sToWeek);
                strSqlString1.Append("     ORDER BY SYS_DATE DESC" + "\n");
                strSqlString1.Append("       ) B        " + "\n");
                strSqlString1.Append(" WHERE ROWNUM=1" + "\n");

                DataTable dt1 = null;
                dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString1.ToString());

                sFrom = dt1.Rows[0][0].ToString() + "220000";
                sTo = dt1.Rows[0][1].ToString() + "215959";

                strSqlString.AppendFormat("           AND TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
            }
            else
            {
                sFrom = cdvFromTo.FromYearMonth.Value.ToString("yyyyMM") + "01";
                sTo = cdvFromTo.ToYearMonth.Value.ToString("yyyyMM");

                DataTable dt1 = null;
                string lastday = DateTime.Now.ToString("yyyyMM").ToString();
                string ToDate = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + lastday + "', 'YYYYMM')), 'YYYYMMDD') FROM DUAL)";
                dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", ToDate);
                lastday = dt1.Rows[0][0].ToString() + "215959";

                DateTime From = DateTime.Parse(sFrom).AddDays(-1);
                sFrom = From.ToString("yyyyMMdd") + "220000";
                
                strSqlString.AppendFormat("           AND TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", sFrom, lastday);
            }            

            strSqlString.Append("        ) A" + "\n");
            strSqlString.Append("      , MWIPMATDEF B " + "\n");
            strSqlString.Append("WHERE 1=1" + "\n");
            strSqlString.Append("    AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("    AND A.FROM_FACTORY = B.FACTORY         " + "\n");
            strSqlString.Append("    AND B.MAT_VER = 1" + "\n");

            if(txtRunID.Text != "%" && txtRunID.Text != "")
                strSqlString.AppendFormat("    AND A.LOT_CMF_4 LIKE '{0}' " + "\n", txtRunID.Text);

            // 2013-01-28-임종우 : 고객사를 제외한 사용자들은 선택하여 조회 가능하도록 변경
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "OPERATOR_GERNERAL" || GlobalVariable.gsUserGroup == "PRODUCTION_MANAGER")
            {
                this.udcWIPCondition1.Enabled = true;

                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("    AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
            }
            else
            {
                this.udcWIPCondition1.Enabled = false;
                strSqlString.Append("    AND B.MAT_GRP_1 = '" + GlobalVariable.gsCustomer + "'" + "\n");
            }
            
            
            //상세 조회에 따른 SQL문 생성                        
            
            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("    AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("    AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("    AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("    AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("    AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("    AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("    AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("    AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            
            strSqlString.AppendFormat("GROUP BY {0} " + "\n", QueryCond3);
            strSqlString.Append("       , A.LOT_ID, A.FROM_FACTORY, A.TO_FACTORY, A.FROM_OPER, A.TRAN_USER_ID, A.TRAN_TIME, A.LOT_CMF_14" + "\n");
            strSqlString.AppendFormat("ORDER BY {0} ", QueryCond3);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();

        }        

        private void btnView_Click(object sender, EventArgs e)
        {
            if (CheckField() == false)
                return;

            DataTable dt = null;
            GridColumnInit();

            try
            {
                spdData_Sheet1.RowCount = 0;
                this.Refresh();
                LoadingPopUp.LoadIngPopUpShow(this);
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 11, null, null, btnSort);
                // 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함


                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);
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

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null, true);
            spdData.ExportExcel();
        }


        //한줄짜리 해더 샘플
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Invoice No", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);                
            spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Run ID", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Lot ID", 0, 11, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);

            spdData.RPT_AddBasicColumn("Lot Qty", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("Wafer Qty", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);            
            spdData.RPT_AddBasicColumn("From Factory", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("To Factory", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Ship Oper", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);            
            spdData.RPT_AddBasicColumn("User", 0, 17, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Receive Time", 0, 18, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("Ship Time", 0, 19, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("TAT", 0, 20, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
            
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
            
        }


        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = B.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", "B.MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "B.MAT_GRP_2", "B.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "B.MAT_GRP_3", "B.MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "B.MAT_GRP_4", "B.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "B.MAT_GRP_5", "B.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "B.MAT_GRP_6", "B.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "B.MAT_GRP_7", "B.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "B.MAT_GRP_8", "B.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Invoice No", "INVOICE_NO", "A.INVOICE_NO", "A.INVOICE_NO", true);       
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT_CMF_7", "B.MAT_CMF_7", "B.MAT_CMF_7", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Run Id", "A.LOT_CMF_4", "A.LOT_CMF_4", "A.LOT_CMF_4", true);            
        }              

    }
}

