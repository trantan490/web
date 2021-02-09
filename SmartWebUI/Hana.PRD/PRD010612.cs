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
    public partial class PRD010612 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010612<br/>
        /// 클래스요약: Wafer Yield Trend<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2015-11-30<br/>
        /// 상세  설명: Wafer Yield Trend(민재훈C 요청)<br/>
        /// 변경  내용: <br/>
        /// </summary>
        public PRD010612()
        {
            InitializeComponent();

            cdvFromToDate.AutoBinding(DateTime.Now.AddDays(-2).ToString(), DateTime.Now.AddDays(-1).ToString());
            //cdvFromToDate.AutoBinding();
            SortInit();
            GridColumnInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
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
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;

            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("MAJOR CODE", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("FAMILY", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("PACKAGE", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("TYPE1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("TYPE2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("LD COUNT", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);            
            spdData.RPT_AddBasicColumn("DENSITY", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("GENERATION", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("PIN TYPE", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);
            spdData.RPT_AddBasicColumn("PRODUCT", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("NET DIE", 0, 11, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 70);
            spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);            
                        
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
         
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "B.MAT_GRP_1", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", "MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "B.MAT_GRP_9", "MAT_GRP_9", "MAT_GRP_9", "MAT_GRP_9", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "B.MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "B.MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "B.MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "B.MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "B.MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "B.MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "B.MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "B.MAT_CMF_10", "MAT_CMF_10", "MAT_CMF_10", "MAT_CMF_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "B.MAT_ID", "MAT_ID", "MAT_ID", "MAT_ID", true);           
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

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string QueryCond4 = null;

            string strDate = null;
            string sFrom = null;
            string sTo = null;
            string sYield = null;                       

            string[] selectDate = new string[cdvFromToDate.SubtractBetweenFromToDate + 1];
            selectDate = cdvFromToDate.getSelectDate();                

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            switch (cdvFromToDate.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    sFrom = cdvFromToDate.FromDate.Text.Replace("-", "");
                    sTo = cdvFromToDate.ToDate.Text.Replace("-", "");
                    strDate = "WORK_DATE";
                    sYield = "DAY_YIELD";
                    break;
                case "WEEK":
                    sFrom = cdvFromToDate.FromWeek.SelectedValue.ToString();
                    sTo = cdvFromToDate.ToWeek.SelectedValue.ToString();
                    strDate = "WORK_WEEK";
                    sYield = "WEEK_YIELD";
                    break;
                case "MONTH":
                    sFrom = cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM");
                    sTo = cdvFromToDate.ToYearMonth.Value.ToString("yyyMM");
                    strDate = "WORK_MONTH";
                    sYield = "MONTH_YIELD";
                    break;
                default:
                    sFrom = cdvFromToDate.FromDate.Text.Replace("-", "");
                    sTo = cdvFromToDate.ToDate.Text.Replace("-", "");
                    strDate = "WORK_DATE";
                    sYield = "DAY_YIELD";
                    break;
            }

            strSqlString.AppendFormat("SELECT " + QueryCond3 + "\n");
            strSqlString.AppendFormat("     , ROUND(AVG(NET_DIE),0) AS NET_DIE" + "\n");

            for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                strSqlString.AppendFormat("     , ROUND(AVG(D{0}), 2) AS D{0}" + "\n", i.ToString());
            }

            strSqlString.AppendFormat("  FROM (" + "\n");
            strSqlString.AppendFormat("        SELECT " + QueryCond1 + "\n");
            strSqlString.AppendFormat("             , MAX(A.NET_DIE) AS NET_DIE" + "\n");            
            
            for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                strSqlString.AppendFormat("             , MAX(DECODE(A.{0}, '{1}', A.{2}, 0)) AS D{3}" + "\n", strDate, selectDate[i].ToString(), sYield, i.ToString());                
            }

            strSqlString.AppendFormat("          FROM RSUMWAFYLD A" + "\n");
            strSqlString.AppendFormat("             , MWIPMATDEF B" + "\n");
            
            // 주, 월 검색 시
            if (strDate != "WORK_DATE")
            {
                strSqlString.AppendFormat("             , (" + "\n");
                strSqlString.AppendFormat("                SELECT *" + "\n");
                strSqlString.AppendFormat("                  FROM (" + "\n");
                strSqlString.AppendFormat("                        SELECT WORK_DATE, {0}" + "\n", strDate);
                strSqlString.AppendFormat("                             , ROW_NUMBER() OVER(PARTITION BY {0} ORDER BY WORK_DATE DESC) AS RN" + "\n", strDate);
                strSqlString.AppendFormat("                          FROM RSUMWAFYLD" + "\n");
                strSqlString.AppendFormat("                         WHERE FACTORY = '{0}'" + "\n", cdvFactory.Text);
                strSqlString.AppendFormat("                           AND {0} BETWEEN '{1}' AND '{2}'" + "\n", strDate, sFrom, sTo);
                strSqlString.AppendFormat("                       )" + "\n");
                strSqlString.AppendFormat("                 WHERE RN = 1" + "\n");                
                strSqlString.AppendFormat("               ) C" + "\n");
                strSqlString.AppendFormat("         WHERE 1=1" + "\n");
                strSqlString.AppendFormat("           AND A.WORK_DATE = C.WORK_DATE " + "\n");
            }
            else
            {
                strSqlString.AppendFormat("         WHERE A.WORK_DATE BETWEEN '{0}' AND '{1}'" + "\n", sFrom, sTo);
            }

            strSqlString.AppendFormat("           AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.AppendFormat("           AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.AppendFormat("           AND A.FACTORY = '{0}'" + "\n", cdvFactory.Text);
            strSqlString.AppendFormat("           AND B.DELETE_FLAG = ' ' " + "\n");
            strSqlString.AppendFormat("           AND A.{0} > 0 " + "\n", sYield);
            
            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}' " + "\n", txtSearchProduct.Text);
            }

            #region 상세 조회에 따른 SQL문 생성
            
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("           AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            
            #endregion

            strSqlString.AppendFormat("         GROUP BY " + QueryCond1 + "\n");
            strSqlString.AppendFormat("       )" + "\n");
            strSqlString.AppendFormat(" GROUP BY " + QueryCond2 + "\n");
            strSqlString.AppendFormat(" ORDER BY " + QueryCond2 + "\n");           

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
            if (CheckField() == false) return;

            GridColumnInit();

            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                this.Refresh();

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                spdData.DataSource = dt;

                for (int i = 0; i <= 11; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LemonChiffon;
                }

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
               

                //by John
                //1.Griid 합계 표시
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 12, null, null, btnSort);

                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 7, 10, null, null, btnSort);
                //                  토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함


                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                //3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 13, 0, 1, true, Align.Center, VerticalAlign.Center);


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
    }
}