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
    public partial class PRD010229 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010229<br/>
        /// 클래스요약: 일별 계획 대비 실적 모니터링<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2020-11-05<br/>
        /// 상세  설명: 일별 계획 대비 실적 모니터링 (김성업과장 요청)<br/>
        /// 변경  내용: <br/>        
        /// 2020-11-25-임종우 : Lot Type 검색 기능 추가 (마세열과장 요청)
        /// </summary>
                
        //GlobalVariable.FindWeek FindWeek = new GlobalVariable.FindWeek();

        public PRD010229()
        {
            InitializeComponent();
            //cdvFromToDate.DaySelector.SelectedValue = "WEEK";
            cdvFromToDate.AutoBindingUserSetting(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd")); 
            cdvFromToDate.ToWeek.Visible = false;
            cdvFromToDate.DaySelector.Visible = false;            

            SortInit();            
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
            spdData.RPT_ColumnInit();            

            try
            {                
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);             
                spdData.RPT_AddBasicColumn("Part No", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);  
                spdData.RPT_AddBasicColumn("Operation", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Operation", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);                
                spdData.RPT_AddBasicColumn("Classification", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);

                spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                       

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

            string[] selectDate = new string[cdvFromToDate.SubtractBetweenFromToDate + 1];
            selectDate = cdvFromToDate.getSelectDate();

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

            strSqlString.Append("SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = SUBSTR(MAT_ID,1,2) AND ROWNUM=1) AS CUSTOMER" + "\n");
            strSqlString.Append("     , MAT_ID" + "\n");
            strSqlString.Append("     , (SELECT OPER_DESC FROM MWIPOPRDEF WHERE FACTORY = '" + cdvFactory.Text + "' AND OPER = DAT.OPER) AS OPER_DESC " + "\n");
            strSqlString.Append("     , DAT.OPER" + "\n");
            strSqlString.Append("     , GUBUN" + "\n");

            for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
            {                
                strSqlString.AppendFormat("     , SUM(DECODE(PLAN_DATE, '{0}', QTY, 0)) AS D{1}" + "\n", selectDate[i].ToString(), i.ToString());
            }
                  
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT PLAN_DATE" + "\n");
            strSqlString.Append("             , MAT_ID" + "\n");
            strSqlString.Append("             , OPER" + "\n");
            strSqlString.Append("             , DECODE(SEQ, 1, 'PLAN', 2, 'ACT', 3, 'BAL') AS GUBUN" + "\n");
            strSqlString.Append("             , DECODE(SEQ, 1, PLN_QTY, 2, END_QTY, 3, BAL_QTY) AS QTY  " + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT A.PLAN_DATE" + "\n");
            strSqlString.Append("                     , A.MAT_ID" + "\n");
            strSqlString.Append("                     , A.OPER" + "\n");
            strSqlString.Append("                     , A.QTY_1 AS PLN_QTY" + "\n");
            strSqlString.Append("                     , NVL(C.END_QTY,0) AS END_QTY " + "\n");
            strSqlString.Append("                     , SUM(NVL(C.END_QTY,0) - A.QTY_1) OVER(PARTITION BY A.MAT_ID, A.OPER ORDER BY A.PLAN_DATE) AS BAL_QTY" + "\n");
            strSqlString.Append("                  FROM CPLNDAYOPR@RPTTOMES A" + "\n");
            strSqlString.Append("                     , MWIPMATDEF B" + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT A.WORK_DATE" + "\n");
            strSqlString.Append("                             , A.MAT_ID" + "\n");
            strSqlString.Append("                             , A.OPER" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN A.OPER = 'AZ010' THEN DECODE(B.MAT_GRP_3, 'COB', 0, 'BGN', 0, (S1_MOVE_QTY_1+S2_MOVE_QTY_1+S3_MOVE_QTY_1))            " + "\n");
            strSqlString.Append("                                        ELSE (S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1)" + "\n");
            strSqlString.Append("                                   END) AS END_QTY" + "\n");
            strSqlString.Append("                          FROM RSUMWIPMOV A" + "\n");
            strSqlString.Append("                             , MWIPMATDEF B" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                           AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                           AND A.WORK_DATE BETWEEN '" + cdvFromToDate.HmFromDay + "' AND '" + cdvFromToDate.HmToDay + "'" + "\n");
            strSqlString.Append("                           AND A.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                           AND B.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND B.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                           AND A.FACTORY NOT IN ('RETURN') " + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                           AND A.CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }
                        
            strSqlString.Append("                         GROUP BY A.WORK_DATE, A.MAT_ID, A.OPER" + "\n");
            strSqlString.Append("                       ) C" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                   AND A.PLAN_DATE = C.WORK_DATE(+)" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = C.MAT_ID(+)" + "\n");
            strSqlString.Append("                   AND A.OPER = C.OPER(+)" + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                   AND A.PLAN_DATE BETWEEN '" + cdvFromToDate.HmFromDay + "' AND '" + cdvFromToDate.HmToDay + "'" + "\n");
            strSqlString.Append("                   AND B.DELETE_FLAG = ' '" + "\n");

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
            {
                strSqlString.Append("                   AND A.MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");
            }

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                   AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                   AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                   AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                   AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                   AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                   AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                   AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                   AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                   AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion

            strSqlString.Append("               ) A" + "\n");
            strSqlString.Append("             , (SELECT LEVEL AS SEQ FROM DUAL CONNECT BY LEVEL <= 3) B" + "\n");
            strSqlString.Append("       ) DAT" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");

            if (cdvOper.Text != "ALL" && cdvOper.Text != "")
                strSqlString.AppendFormat("   AND OPER {0} " + "\n", cdvOper.SelectedValueToQueryString);

            if (cdvGubun.Text != "ALL" && cdvGubun.Text != "")
                strSqlString.AppendFormat("   AND GUBUN {0} " + "\n", cdvGubun.SelectedValueToQueryString);

            strSqlString.Append(" GROUP BY MAT_ID, OPER, GUBUN" + "\n");
            strSqlString.Append(" ORDER BY MAT_ID, DAT.OPER, DECODE(GUBUN, 'PLAN', 1, 'ACT', 2, 3)" + "\n");

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
            strQuery += "        SELECT DISTINCT OPER" + "\n";
            strQuery += "          FROM CPLNDAYOPR@RPTTOMES" + "\n";
            strQuery += "         WHERE 1=1" + "\n";
            strQuery += "           AND FACTORY = '" + cdvFactory.Text + "' " + "\n";            
            strQuery += "           AND PLAN_DATE BETWEEN '" + cdvFromToDate.HmFromDay + "' AND '" + cdvFromToDate.HmToDay + "' " + "\n";
            strQuery += "         ORDER BY OPER " + "\n";
            strQuery += "       ) " + "\n";

            cdvOper.sDynamicQuery = strQuery;
        }

        private void cdvGubun_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DECODE (ROWNUM, 1, A, 2, B, 3, C) AS Code, '' Data" + "\n";
            strQuery += "  FROM ( " + "\n";
            strQuery += "        SELECT 1 FROM DUAL CONNECT BY LEVEL <= 3" + "\n";
            strQuery += "       ) " + "\n";
            strQuery += "     , ( " + "\n";
            strQuery += "        SELECT 'PLAN' AS A " + "\n";
            strQuery += "             , 'ACT' AS B" + "\n";
            strQuery += "             , 'BAL' AS C " + "\n";            
            strQuery += "          FROM DUAL " + "\n";
            strQuery += "       ) " + "\n";

            cdvGubun.sDynamicQuery = strQuery;
        }

    }
}
