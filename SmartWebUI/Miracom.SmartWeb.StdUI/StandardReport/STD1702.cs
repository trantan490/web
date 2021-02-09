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

namespace Miracom.SmartWeb.UI
{
    public partial class STD1702 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {           
        /// <summary>
        /// 클  래  스: STD1702<br/>
        /// 클래스요약: 설비 제품별 T-Card 정보<br/>
        /// 작  성  자: 배수민<br/>
        /// 최초작성일: 2011-03-29<br/>
        /// 상세  설명: 설비 제품별 T-Card 정보<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 배수민<br />
        /// </summary>
        public STD1702()
        {
            InitializeComponent();

            SortInit();
            GridColumnInit();

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.cdvOper.sFactory = GlobalVariable.gsAssyDefaultFactory;

            cdvOper.Text = "";

        }

        #region " Constant Definition "

        #endregion

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

            if (cdvOper.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
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

            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Maj Code", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
 
            spdData.RPT_AddBasicColumn("Equipment name", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("제품", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("공정", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Equipment status", 0, 12, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("TAPE", 0, 13, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("WBL_TAPE", 0, 14, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("TARGET", 0, 15, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("BLADE", 0, 16, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("SUBSTRATE", 0, 17, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("CAP", 0, 18, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("GOLDWIRE", 0, 19, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("EMC", 0, 20, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("SB", 0, 21, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("TRAY", 0, 22, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

            int pos = spdData.ActiveSheet.Columns.Count;
            for (int i = 0; i < pos; i++)
            {
                spdData.RPT_MerageHeaderRowSpan(0, i, 5);
            }
            
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment name", "RES_ID as RES_ID", "RES_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = C.MAT_GRP_1 AND ROWNUM=1) as Customer", "C.MAT_GRP_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "C.MAT_GRP_2 as Family", "C.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "C.MAT_GRP_3 as Package", "C.MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "C.MAT_GRP_4 as Type1", "C.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "C.MAT_GRP_5 as Type2", "C.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "C.MAT_GRP_6 as LDCount", "C.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "C.MAT_GRP_7 as Density", "C.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "C.MAT_GRP_8 as Generation", "C.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Maj Code", "C.MAT_GRP_9 as Maj_Code", "C.MAT_GRP_9", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "RES_STS_2 AS MAT_ID", "RES_STS_2", true);
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
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            
            strSqlString.Append("SELECT   " + QueryCond1 + " \n");
            strSqlString.Append("     , RES_STS_8 AS OPER, A.RES_UP_DOWN_FLAG AS RES_UP_DOWN_FLAG, LM_1 AS TAPE, WM_TYPE1 AS WBL_TAPE, BG_1 AS TARGET, BLADE_1 AS BLADE " + "\n");
            strSqlString.Append("     , LF_MATERIAL AS SUBSTRATE, CAP_1 AS CAP, GW_1 AS GOLDWIRE, EMC_1 AS EMC, SB as SB, TRAY as TRAY " + "\n");
            strSqlString.Append("  FROM MRASRESDEF A " + "\n");
            strSqlString.Append("     , CLOTCRDDAT B " + "\n");
            strSqlString.Append("     , MWIPMATDEF C " + "\n");
            strSqlString.Append(" WHERE 1 = 1 " + "\n");
            strSqlString.Append("  AND A.FACTORY = B.FACTORY(+) " + "\n");
            strSqlString.Append("  AND A.FACTORY = C.FACTORY(+) " + "\n");
            strSqlString.Append("  AND A.RES_STS_2 = B.MAT_ID(+) " + "\n");
            strSqlString.Append("  AND A.RES_STS_2 = C.MAT_ID(+) " + "\n");
            strSqlString.Append("  AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("  AND A.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("  AND A.RES_STS_8 != ' ' " + "\n");
            strSqlString.Append("  AND A.RES_ID NOT LIKE 'BSP%' " + "\n");

            strSqlString.Append("  AND A.RES_STS_8 " + cdvOper.SelectedValueToQueryString + "\n");
            strSqlString.Append("  AND A.RES_STS_2 LIKE '" + txtSearchProduct.Text + "' " + "\n" + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("  AND C.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("  AND C.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("  AND C.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("  AND C.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("  AND C.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("  AND C.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("  AND C.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("  AND C.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("  AND C.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion

            strSqlString.AppendFormat(" GROUP BY {0}, RES_STS_8, A.RES_UP_DOWN_FLAG, LM_1, WM_TYPE1, BG_1, BLADE_1, LF_MATERIAL, CAP_1, GW_1, EMC_1, SB, TRAY " + "\n", QueryCond2);
            strSqlString.AppendFormat(" ORDER BY RES_ID, OPER " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
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

                //Total, Sub Total 적용 안하므로 아래 소스들 모두 주석 처리 
                spdData.DataSource = dt;

                // 2012-04-17-배수민 : 주요 컬럼마다 색깔 부여
                for (int i = 0; i < spdData.ActiveSheet.ColumnCount; i++)
                {
                    if (spdData.ActiveSheet.Columns[i].Label == "Equipment name")
                    {
                        spdData.ActiveSheet.Columns[i].BackColor = Color.Turquoise;
                    }
                    else if (spdData.ActiveSheet.Columns[i].Label == "제품")
                    {
                        spdData.ActiveSheet.Columns[i].BackColor = Color.Lavender;
                    }
                    else if (spdData.ActiveSheet.Columns[i].Label == "공정")
                    {
                        spdData.ActiveSheet.Columns[i].BackColor = Color.LightYellow;
                    }
                }

                ////4. Column Auto Fit
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
            spdData.ExportExcel();
        }



        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            cdvOper.sFactory = cdvFactory.txtValue;
        }

        private void cdvOper_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DISTINCT RES_STS_8 AS CODE, ' ' AS NAME" + "\n";
            strQuery += "  FROM MRASRESDEF@RPTTOMES" + "\n";
            strQuery += " WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n"; // HMKA1 고정 
            strQuery += "   AND RES_CMF_9 = 'Y' " + "\n";
            strQuery += "   AND DELETE_FLAG = ' ' " + "\n";
            strQuery += "   AND RES_STS_8 != ' ' " + "\n";
            strQuery += "   AND RES_ID NOT LIKE 'BSP%' " + "\n";
            strQuery += " ORDER BY CODE " + "\n";

            cdvOper.sDynamicQuery = strQuery;
        }

    }
}
