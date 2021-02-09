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
    public partial class TAT050402 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        private DataTable dtOper = null;

        /// <summary>
        /// 클  래  스: TAT050402<br/>
        /// 클래스요약: TAT Time Table<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2013-02-20<br/>
        /// 상세  설명: TAT Time Table<br/>
        /// 변경  내용: <br/>
        /// </summary>
        public TAT050402()
        {
            InitializeComponent();
            SortInit();
            GridColumnInit(); //헤더 한줄짜리     
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
        }

        #region SortInit


        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1", "MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Major", "MAT_GRP_9", "MAT_GRP_9", "MAT_GRP_9", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", true);
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

                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Major", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Family", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LD Count", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Density", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type2", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Generation", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("SAP Code", 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PKG Code", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("PinType", 0, 11, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 100);

                spdData.RPT_AddBasicColumn("Manufacturing", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("WAIT", 1, 12, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 60);
                spdData.RPT_AddBasicColumn("RUN", 1, 13, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 60);
                spdData.RPT_MerageHeaderColumnSpan(0, 12, 2);

                if (dtOper != null)
                {
                    for (int i = 0; i < dtOper.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(dtOper.Rows[i][0].ToString(), 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 120);
                        spdData.RPT_AddBasicColumn("Wait", 1, spdData.ActiveSheet.ColumnCount - 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                        spdData.RPT_AddBasicColumn("Run", 1, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                        spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnCount - 2, 2); ;
                    }
                }

                for (int y = 0; y <= 11; y++)
                {
                    spdData.RPT_MerageHeaderRowSpan(0, y, 2);
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

            // 공정 코드 가져오기
            dtOper = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeOperTable());

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

                spdData.DataSource = dt;

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

        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {
            //if (cdvFactory.Text.Trim().Length == 0)
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
            //    return false;
            //}

            return true;
        }

        #endregion

        private String MakeOperTable()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT DISTINCT OPER" + "\n");
            strSqlString.Append("  FROM CWIPSAPTAT@RPTTOMES A " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT DISTINCT BASE_MAT_ID" + "\n");
            strSqlString.Append("          FROM MWIPMATDEF " + "\n");
            strSqlString.Append("         WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("           AND DELETE_FLAG = ' ' " + "\n");

            // 상세 조회 부분
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

            strSqlString.Append("       ) B " + "\n");
            strSqlString.Append(" WHERE A.SAP_CODE = B.BASE_MAT_ID" + "\n");
            strSqlString.Append("   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("   AND A.RESV_FIELD_1 = ' '" + "\n");
            strSqlString.Append("   AND A.RESV_FIELD_2 = ' '" + "\n");
            strSqlString.Append(" ORDER BY OPER" + "\n");

            return strSqlString.ToString();
        }


        #region MakeSqlString
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

            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond2);
            strSqlString.AppendFormat("     , SAP_CODE " + "\n");
            strSqlString.AppendFormat("     , SUBSTR(SAP_CODE, 5, 4) PKG_CODE" + "\n");
            strSqlString.AppendFormat("     , MAT_CMF_10 " + "\n");
            strSqlString.AppendFormat("     , ROUND(SUM(TAT_DAY_WAIT), 2) AS W_TTL " + "\n");
            strSqlString.AppendFormat("     , ROUND(SUM(TAT_DAY), 2) AS R_TTL " + "\n");

            for (int i = 0; i < dtOper.Rows.Count; i++)
            {
                strSqlString.AppendFormat("     , ROUND(SUM(DECODE(OPER, '" + dtOper.Rows[i]["OPER"].ToString() + "', TAT_DAY_WAIT, 0)), 2) AS W" + i + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(DECODE(OPER, '" + dtOper.Rows[i]["OPER"].ToString() + "', TAT_DAY, 0)), 2) AS R" + i + "\n");
            }

            strSqlString.AppendFormat("  FROM CWIPSAPTAT@RPTTOMES A " + "\n");
            strSqlString.AppendFormat("     , ( " + "\n");
            strSqlString.AppendFormat("        SELECT DISTINCT BASE_MAT_ID, MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, MAT_GRP_9, MAT_GRP_10, MAT_CMF_10" + "\n");
            strSqlString.AppendFormat("          FROM MWIPMATDEF" + "\n");
            strSqlString.AppendFormat("         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.AppendFormat("           AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.AppendFormat("           AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.AppendFormat("           AND SUBSTR(BASE_MAT_ID, 5, 4) NOT IN ('0000', '00ER') " + "\n");

            //상세 조회에 따른 SQL문 생성                        
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

            strSqlString.AppendFormat("       ) B " + "\n");
            strSqlString.AppendFormat(" WHERE 1=1 " + "\n");
            strSqlString.AppendFormat("   AND A.SAP_CODE = B.BASE_MAT_ID " + "\n");
            strSqlString.AppendFormat("   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.AppendFormat("   AND A.RESV_FIELD_1 = ' ' " + "\n");
            strSqlString.AppendFormat("   AND A.RESV_FIELD_2 = ' ' " + "\n");

            if (txtSapCode.Text != "%" && txtSapCode.Text != "")
            {
                strSqlString.AppendFormat("   AND A.SAP_CODE LIKE '" + txtSapCode.Text + "' " + "\n");
            }

            strSqlString.AppendFormat("   AND A.RESV_FIELD_2 = ' ' " + "\n");
            strSqlString.AppendFormat(" GROUP BY {0}, SAP_CODE, MAT_CMF_10 " + "\n", QueryCond1);
            strSqlString.AppendFormat(" ORDER BY SAP_CODE, {0}, MAT_CMF_10 " + "\n", QueryCond1);


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
        /// ToExcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1, this.lblTitle.Text, null, null);
            spdData.ExportExcel();
        }

        #endregion

    }
}
