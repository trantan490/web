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

namespace Hana.RAS
{
    /// <summary>
    /// 클  래  스: RAS020106<br/>
    /// 클래스요약: 설비별 생산량<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2009-07-14<br/>
    /// 상세  설명: 설비별 생산량<br/>
    /// 변경  내용: 
    /// 2012-05-25-배수민 : WAFER QTY 추가 (김문한K 요청)<br/>
    /// 2014-01-06-임종우 : 원부자재 제품 제외 (김상천 요청)
    /// 2014-10-30-임종우 : 환산 UPEH 추가 (임태성K 요청)
    /// </summary>
    public partial class RAS020106 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public RAS020106()
        {
            InitializeComponent();
            udcFromToDate.AutoBinding();
            SortInit();
            GridColumnInit();
        }

        #region " Function Definition "

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
            spdData.ActiveSheet.RowHeader.ColumnCount = 0;
            spdData.RPT_ColumnInit();
            
            spdData.RPT_AddBasicColumn("Equipment name", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Operation", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Team in charge", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("파트", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("EQP Type", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Maker", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Model", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);

            spdData.RPT_AddBasicColumn("Customer", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Family", 0, 8, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Package", 0, 9, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Type1", 0, 10, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Type2", 0, 11, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("LD Count", 0, 12, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Density", 0, 13, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Generation", 0, 14, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Pin Type", 0, 15, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Product", 0, 16, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Cust Device", 0, 17, Visibles.False, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);

            spdData.RPT_AddBasicColumn("Qty", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("W_Qty", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("UPEH", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double2, 80);
            spdData.RPT_AddBasicColumn("UPEH Conversion", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double2, 80);
            spdData.RPT_AddBasicColumn("Uptime", 0, 22, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment name", "END_RES_ID", "END_RES_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation", "OLD_OPER", "OLD_OPER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Team in charge", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_1), '-') AS TEAM", "TEAM", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Part", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_2), '-') AS PART", "PART", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("EQP Type", "RES.RES_GRP_3 AS EQP_TYPE", "EQP_TYPE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Maker", "RES.RES_GRP_5 AS MAKER", "MAKER", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "RES.RES_GRP_6 AS MODEL", "MODEL", false);

            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", "CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3", "MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10", "MAT_CMF_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID", "MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "MAT.MAT_CMF_7", "MAT_CMF_7", false);  

        }

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

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            strSqlString.AppendFormat("SELECT " + QueryCond2 + "\n");
            strSqlString.Append("     , SUM(QTY) AS QTY" + "\n");
            strSqlString.Append("     , SUM(W_QTY) AS W_QTY" + "\n");
            strSqlString.Append("     , ROUND(AVG(UPEH),2) AS UPEH" + "\n");
            strSqlString.Append("     , ROUND(AVG(UPEH * CONVERT_QTY),2)" + "\n");
            strSqlString.Append("     , MAX(VALUETIME) VALUETIME" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.AppendFormat("        SELECT " + QueryCond1 + "\n");
            strSqlString.Append("             , OPER_IN_QTY_1 AS QTY" + "\n");
            strSqlString.Append("             , QTY_2 AS W_QTY" + "\n");
            strSqlString.Append("             , DECODE(UPEH,0,(SELECT UPEH FROM CRESMSTUPH@RPTTOMES WHERE OPER=HIS.OPER AND MODEL=RES.RES_GRP_6 AND UPEH_GROUP=RES.RES_GRP_7 AND RES.DELETE_FLAG=' '),UPEH) AS UPEH " + "\n");
            strSqlString.Append("             , CASE WHEN MAT.MAT_GRP_1 = 'SE' AND OLD_OPER LIKE 'A04%' THEN TO_NUMBER(COMP_CNT)" + "\n");
            strSqlString.Append("                    WHEN OLD_OPER LIKE 'A06%' THEN NVL((SELECT MAX(TO_NUMBER(TCD_CMF_2)) FROM CWIPTCDDEF@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND SET_FLAG = 'Y' AND TCD_CMF_2 <> ' ' AND MAT_ID = HIS.MAT_ID AND OPER = OLD_OPER), MAT.MAT_GRP_6)" + "\n");
            strSqlString.Append("                    ELSE 1 " + "\n");
            strSqlString.Append("               END CONVERT_QTY " + "\n");
            strSqlString.Append("             , (SELECT VALUE_RUN_TIME FROM CSUMRASMOV@RPTTOMES WHERE MAT_ID = HIS.MAT_ID AND WORK_DAY = '" + udcFromToDate.End_Tran_Time.Substring(0,8) + "' AND FACTORY = HIS.FACTORY  AND RES_ID = RES.RES_ID AND OPER = OLD_OPER  ) VALUETIME" + "\n");
            strSqlString.Append("          FROM CWIPLOTEND HIS " + "\n");
            strSqlString.Append("             , MRASRESDEF RES " + "\n");
            strSqlString.Append("             , VWIPMATDEF MAT " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND RES.FACTORY = HIS.FACTORY " + "\n");
            strSqlString.Append("           AND RES.RES_ID = HIS.END_RES_ID " + "\n");
            strSqlString.Append("           AND HIS.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("           AND HIS.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("           AND RES.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND MAT.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("           AND HIS.FACTORY = '" + cdvFactory.txtValue + "'" + "\n");
            strSqlString.Append("           AND HIS.TRAN_TIME BETWEEN '" + udcFromToDate.Start_Tran_Time + "' AND '" + udcFromToDate.End_Tran_Time + "'" + "\n");
            strSqlString.Append("           AND HIS.END_RES_ID != ' ' " + "\n");            


            #region " 조회 조건 "

            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                strSqlString.AppendFormat("           AND HIS.LOT_CMF_5 LIKE '{0}'" + "\n", txtSearchProduct.Text);

            #endregion

            #region " WIP 상세 조회 "
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

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

            #region " RAS 상세 조회 "
            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("           AND RES.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);
            
            #endregion

            strSqlString.Append("       ) A" + "\n");
            strSqlString.Append(" GROUP BY " + QueryCond2 + "\n");
            strSqlString.Append(" ORDER BY " + QueryCond2 + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
                        
            return strSqlString.ToString();
        }

        /// <summary>
        /// 5. Chart 생성
        /// </summary>
        /// <param name="DT">Chart를 생성할 데이터 테이블</param>
        private void MakeChart(DataTable DT)
        {

        }

        private void ShowChart(int rowCount)
        {

        }

        #endregion

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

                //by John
                //1.Griid 합계 표시
                spdData.DataSource = dt;
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 0, null, null, btnSort);
                //spdData.Sheets[0].Rows[0].Remove();
                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                //3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                //ShowChart(5);
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
            //spdData.ExportExcel();

            if (spdData.ActiveSheet.Rows.Count > 0)
            {
                StringBuilder Condition = new StringBuilder();

                Condition.Append("조회시간 : " + DateTime.Now.ToString());

                ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);
            }
        }

        #region 기타
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
        }
        #endregion
    }
}