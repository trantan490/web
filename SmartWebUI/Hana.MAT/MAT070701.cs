using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI.Controls;

namespace Hana.MAT
{
    public partial class MAT070701 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        /// <summary>
        /// 클  래  스: MAT070701<br/>
        /// 클래스요약: 제품별 적용 Vendor 정보 조회<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2014-02-24<br/>
        /// 상세  설명: 제품별 적용 Vendor 정보 조회(요청자 : 김권수 대리)<br/>
        /// 변경  내용: <br/>  
        /// 2014-09-24-임종우 : LF 제품 타입 추가 (이승희 요청)
        /// 2015-11-23-임종우 : Vendor 7개까지 추가 표시 (이승희 요청)
        /// </summary>
        public MAT070701()
        {
            InitializeComponent();
                        
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.udcWIPCondition1.sFactory = GlobalVariable.gsAssyDefaultFactory;
            cdvFactory.Enabled = false;
            SortInit();
            GridColumnInit();            
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Material Type", "B.RESV_FIELD_2", "CB.RESV_FIELD_2", "B.RESV_FIELD_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "A.MAT_GRP_1", "A.MAT_GRP_1", "A.MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR", "A.MAT_GRP_9", "A.MAT_GRP_9", "A.MAT_GRP_9", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "A.MAT_GRP_10", "A.MAT_GRP_10", "A.MAT_GRP_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "A.MAT_GRP_6", "A.MAT_GRP_6", "A.MAT_GRP_6", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "A.MAT_GRP_7", "A.MAT_GRP_7", "A.MAT_GRP_7", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "A.MAT_GRP_8", "A.MAT_GRP_8", "A.MAT_GRP_8", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "A.MAT_CMF_11", "A.MAT_CMF_11", "A.MAT_CMF_11", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.MAT_ID", "A.MAT_ID", "A.MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("STEP", "B.STEPID", "B.STEPID", "B.STEPID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Material code", "B.MATCODE", "B.MATCODE", "B.MATCODE", true);            
        }

        #endregion

        #region 한줄헤더생성

        /// <summary>
        /// 헤더생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {  
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Material Type", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("CUSTOMER", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("MAJOR", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PACKAGE", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LD COUNT", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("DENSITY", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("GENERATION", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);            
            spdData.RPT_AddBasicColumn("PKG CODE", 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("PRODUCT", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("STEP", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Material code", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);

            spdData.RPT_AddBasicColumn("VENDOR", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("1st", 1, 11, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("2nd", 1, 12, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("3rd", 1, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("4th", 1, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("5th", 1, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("6th", 1, 16, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("7th", 1, 17, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_MerageHeaderColumnSpan(0, 11, 7);

            for (int i = 0; i <= 10; i++)
            {
                spdData.RPT_MerageHeaderRowSpan(0, i, 2);
            }
                        
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

                //  1. column header
                for (int i = 0; i <= 10; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LemonChiffon;
                }

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
            return true;
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
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            
            // 쿼리
            strSqlString.Append("SELECT " + QueryCond1 + "\n");
            strSqlString.Append("     , (SELECT KEY_1 || ' ' || DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'VENDOR' AND KEY_1 = REGEXP_SUBSTR(B.RESV_FIELD_5, '[^,]+', 1)) AS VENDOR1" + "\n");
            strSqlString.Append("     , (SELECT KEY_1 || ' ' || DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'VENDOR' AND KEY_1 = REGEXP_SUBSTR(B.RESV_FIELD_5, '[^,]+', 7)) AS VENDOR2" + "\n");
            strSqlString.Append("     , (SELECT KEY_1 || ' ' || DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'VENDOR' AND KEY_1 = REGEXP_SUBSTR(B.RESV_FIELD_5, '[^,]+', 14)) AS VENDOR3" + "\n");
            strSqlString.Append("     , (SELECT KEY_1 || ' ' || DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'VENDOR' AND KEY_1 = REGEXP_SUBSTR(B.RESV_FIELD_5, '[^,]+', 21)) AS VENDOR4" + "\n");
            strSqlString.Append("     , (SELECT KEY_1 || ' ' || DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'VENDOR' AND KEY_1 = REGEXP_SUBSTR(B.RESV_FIELD_5, '[^,]+', 28)) AS VENDOR5" + "\n");
            strSqlString.Append("     , (SELECT KEY_1 || ' ' || DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'VENDOR' AND KEY_1 = REGEXP_SUBSTR(B.RESV_FIELD_5, '[^,]+', 35)) AS VENDOR6" + "\n");
            strSqlString.Append("     , (SELECT KEY_1 || ' ' || DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'VENDOR' AND KEY_1 = REGEXP_SUBSTR(B.RESV_FIELD_5, '[^,]+', 42)) AS VENDOR7" + "\n");
            strSqlString.Append("  FROM MWIPMATDEF A" + "\n");
            strSqlString.Append("     , CWIPBOMDEF B" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND A.MAT_ID = B.PARTNUMBER" + "\n");
            strSqlString.Append("   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("   AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("   AND A.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("   AND B.RESV_FLAG_1 = 'Y'" + "\n");
            strSqlString.Append("   AND B.RESV_FIELD_2 IN ('PB', 'LF','GW','CW','SW')" + "\n");
            

            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                strSqlString.Append("   AND A.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            if (txtMatCode.Text.Trim() != "%" && txtMatCode.Text.Trim() != "")
                strSqlString.Append("   AND B.MATCODE LIKE '" + txtMatCode.Text + "'" + "\n");

            if (cdvVendor.Text == "-")
            {
                strSqlString.Append("   AND B.RESV_FIELD_5 = ' '" + "\n");
            }
            else if (cdvVendor.Text != "ALL" && cdvVendor.Text != "")
            {
                strSqlString.Append("   AND B.RESV_FIELD_5 LIKE '%" + cdvVendor.Text + "%'" + "\n");
            }

            if (cdvMatType.Text != "ALL" && cdvMatType.Text != "")
                strSqlString.AppendFormat("   AND B.RESV_FIELD_2 {0} " + "\n", cdvMatType.SelectedValueToQueryString);

            //Detailed inquiry에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            
            
            strSqlString.Append(" ORDER BY " + QueryCond1 + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #endregion

        #region Event Handler

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

        private void cdvVendor_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "WITH DT AS" + "\n";
            strQuery += "( " + "\n";
            strQuery += " SELECT DISTINCT DECODE(B.RESV_FIELD_5, ' ', '-', B.RESV_FIELD_5) AS RESV_FIELD_5 " + "\n";
            strQuery += "   FROM MWIPMATDEF A" + "\n";
            strQuery += "      , CWIPBOMDEF B " + "\n";
            strQuery += "  WHERE 1=1 " + "\n";
            strQuery += "    AND A.MAT_ID = B.PARTNUMBER " + "\n";
            strQuery += "    AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n";
            strQuery += "    AND A.DELETE_FLAG = ' ' " + "\n";
            strQuery += "    AND A.MAT_TYPE = 'FG' " + "\n";
            strQuery += "    AND B.RESV_FLAG_1 = 'Y' " + "\n";
            strQuery += "    AND B.RESV_FIELD_2 IN ('PB', 'LF','GW','CW','SW') " + "\n";
            strQuery += ") " + "\n";
            strQuery += "SELECT 'ALL' AS Code, '전체' AS Data FROM DUAL " + "\n";
            strQuery += " UNION ALL " + "\n";
            strQuery += "SELECT A.KEY_1 AS Code, NVL(B.DATA_1, '미등록') AS Data " + "\n";            
            strQuery += "  FROM ( " + "\n";
            strQuery += "        SELECT REGEXP_SUBSTR(RESV_FIELD_5, '[^,]+',  1) AS KEY_1 FROM DT " + "\n";
            strQuery += "         UNION " + "\n";
            strQuery += "        SELECT REGEXP_SUBSTR(RESV_FIELD_5, '[^,]+',  7) AS KEY_1 FROM DT " + "\n";
            strQuery += "         UNION " + "\n";
            strQuery += "        SELECT REGEXP_SUBSTR(RESV_FIELD_5, '[^,]+',  14) AS KEY_1 FROM DT " + "\n";
            strQuery += "         UNION " + "\n";
            strQuery += "        SELECT REGEXP_SUBSTR(RESV_FIELD_5, '[^,]+',  21) AS KEY_1 FROM DT " + "\n";
            strQuery += "         UNION " + "\n";
            strQuery += "        SELECT REGEXP_SUBSTR(RESV_FIELD_5, '[^,]+',  28) AS KEY_1 FROM DT " + "\n";
            strQuery += "         UNION " + "\n";
            strQuery += "        SELECT REGEXP_SUBSTR(RESV_FIELD_5, '[^,]+',  35) AS KEY_1 FROM DT " + "\n";
            strQuery += "         UNION " + "\n";
            strQuery += "        SELECT REGEXP_SUBSTR(RESV_FIELD_5, '[^,]+',  42) AS KEY_1 FROM DT " + "\n";
            strQuery += "       ) A " + "\n";
            strQuery += "     , MGCMTBLDAT B " + "\n";
            strQuery += " WHERE 1=1 " + "\n";
            strQuery += "   AND A.KEY_1 = B.KEY_1(+) " + "\n";
            strQuery += "   AND A.KEY_1 <> ' ' " + "\n";
            strQuery += "   AND B.FACTORY(+) = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n";
            strQuery += "   AND B.TABLE_NAME(+) = 'VENDOR' " + "\n";

            cdvVendor.sDynamicQuery = strQuery;
        }

        private void cdvMatType_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DECODE(ROWNUM, 1, A, 2, B, 3, C, 4, D, 5, E) AS Code, '' AS Data" + "\n";
            strQuery += "  FROM (SELECT 1 FROM DUAL CONNECT BY LEVEL <= 5) " + "\n";
            strQuery += "     , ( " + "\n";
            strQuery += "        SELECT 'PB' AS A, 'LF' AS B, 'GW' AS C" + "\n";
            strQuery += "             , 'CW' AS D, 'SW' AS E" + "\n";            
            strQuery += "           FROM DUAL " + "\n";
            strQuery += "       ) " + "\n";


            cdvMatType.sDynamicQuery = strQuery;
        }

    }
}

