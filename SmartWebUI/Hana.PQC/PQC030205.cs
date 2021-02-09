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

namespace Hana.PQC
{
    public partial class PQC030205 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        DataTable dt = new DataTable();


        #region " PQC030205 : Program Initial "

        /// <summary>
        /// 클  래  스: PQC030205<br/>
        /// 클래스요약: SPC Chart 기준정보<br/>
        /// 작  성  자: 임종우<br/>
        /// 최초작성일: 2014-04-15 <br/>
        /// 상세  설명: SPC Chart 기준정보<br/>
        /// 변경  내용: 
        /// </summary>
        /// 
        public PQC030205()
        {
            InitializeComponent();            
            udcFromToDate.AutoBinding();
            SortInit();
            GridColumnInit(); 

            // 기본공정은 HMKA1으로 설정
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;            
            udcFromToDate.DaySelector.Visible = false;            
        }

        #endregion


        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if ((cdvOper.Text == "ALL" || cdvOper.Text == ""))
            {                
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        #endregion


        #region " GridColumnInit : Sheet Title 설정 " : SPREAD가 없으니 필요없음

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();

            try
            {
                spdData.RPT_AddBasicColumn("SPC Chart ID", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("SPC Chart Desc", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("Collection ID", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("Character id", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 150);                
                spdData.RPT_AddBasicColumn("SPC Chart Ver", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("USL", 0, 5, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("LSL", 0, 6, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("UCL", 0, 7, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("LCL", 0, 8, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("Customer", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Family", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Package", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type1", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type2", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD Count", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Density", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Generation", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 18, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 130);                
                spdData.RPT_AddBasicColumn("Distribution time", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Distributor", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);                 

                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        #endregion


        #region " SortInit : Group By 설정 " : SPREAD가 없으니 필요없다.

        /// <summary>
        /// 3. Group By 정의 
        /// </summary>
        private void SortInit()
        {
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Date", "TRAN_DATE", "TRAN_DATE", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("COL SET ID", "COL_SET_ID", "A.COL_SET_ID", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Character ID", "CHAR_ID", "B.CHAR_ID", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "CHT_GRP_1", "B.CHT_GRP_1", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "CHT_GRP_2", "B.CHT_GRP_2", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "CHT_GRP_3", "B.CHT_GRP_3", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "CHT_GRP_4", "B.CHT_GRP_4", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "CHT_GRP_5", "B.CHT_GRP_5", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "CHT_GRP_6", "B.CHT_GRP_6", true);            
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Res ID", "RES_ID", "A.RES_ID", false);
        }

        #endregion


        #region " MakeSqlString : Sql Query문 "

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            //string QueryCond1 = null;
            //string QueryCond2 = null;
            //string QueryCond3 = null;
            //string strCheckd = null;

            //udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            //QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            //QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            //QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            string strFromDate = udcFromToDate.ExactFromDate;
            string strToDate = udcFromToDate.ExactToDate;

            // 쿼리 
            strSqlString.Append("SELECT A.CHART_ID" + "\n");
            strSqlString.Append("     , A.CHART_DESC" + "\n");
            strSqlString.Append("     , A.COL_SET_ID" + "\n");
            strSqlString.Append("     , A.CHAR_ID" + "\n");
            strSqlString.Append("     , B.VERSION " + "\n");
            strSqlString.Append("     , B.USL" + "\n");
            strSqlString.Append("     , B.LSL" + "\n");
            strSqlString.Append("     , B.UCL" + "\n");
            strSqlString.Append("     , B.LCL" + "\n");
            strSqlString.Append("     , D.MAT_ID" + "\n");
            strSqlString.Append("     , D.MAT_GRP_1, D.MAT_GRP_2, D.MAT_GRP_3, D.MAT_GRP_4, D.MAT_GRP_5" + "\n");
            strSqlString.Append("     , D.MAT_GRP_6, D.MAT_GRP_7, D.MAT_GRP_8, D.MAT_CMF_10" + "\n");            
            strSqlString.Append("     , TO_CHAR(TO_DATE(B.RELEASE_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD HH24:MI:SS') AS RELEASE_TIME " + "\n");
            strSqlString.Append("     , (SELECT USER_DESC FROM MSECUSRDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND USER_ID = B.RELEASE_USER_ID) || '(' || B.RELEASE_USER_ID || ')' AS RELEASE_USER_ID" + "\n");
            strSqlString.Append("  FROM MSPCCHTDEF@RPTTOMES A" + "\n");
            strSqlString.Append("     , MSPCSPEHIS@RPTTOMES B" + "\n");
            strSqlString.Append("     , MWIPCOLDEF@RPTTOMES C" + "\n");
            strSqlString.Append("     , MWIPMATDEF D" + "\n");

            if (ckdVersion.Checked == true)
            {
                strSqlString.Append("     , (" + "\n");
                strSqlString.Append("        SELECT CHART_ID, MAX(VERSION) AS VERSION" + "\n");
                strSqlString.Append("          FROM MSPCSPEHIS" + "\n");
                strSqlString.Append("         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("           AND RELEASE_FLAG = 'Y'" + "\n");
                strSqlString.Append("           AND RELEASE_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                strSqlString.Append("         GROUP BY CHART_ID" + "\n");
                strSqlString.Append("       ) E" + "\n");
                strSqlString.Append(" WHERE 1=1" + "\n");
                strSqlString.Append("   AND A.CHART_ID = E.CHART_ID" + "\n");
                strSqlString.Append("   AND B.VERSION = E.VERSION" + "\n");
            }
            else
            {
                strSqlString.Append(" WHERE 1=1" + "\n");
            }

            strSqlString.Append("   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("   AND A.FACTORY = C.FACTORY" + "\n");
            strSqlString.Append("   AND A.FACTORY = D.FACTORY" + "\n");
            strSqlString.Append("   AND A.CHART_ID = B.CHART_ID" + "\n");            
            strSqlString.Append("   AND A.COL_SET_ID = C.COL_SET_ID" + "\n");            
            strSqlString.Append("   AND C.MAT_ID = D.MAT_ID" + "\n");
            strSqlString.Append("   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("   AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("   AND A.CHT_CMF_4 = 'PQC'" + "\n");
            strSqlString.Append("   AND B.RELEASE_FLAG = 'Y'" + "\n");
            strSqlString.Append("   AND B.RELEASE_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
            strSqlString.Append("   AND D.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("   AND C.OPER = '" + cdvOper.Text + "'" + "\n");



            if (cdvChartID.Text != "ALL" && cdvChartID.Text != "")
                strSqlString.AppendFormat("   AND A.CHART_ID {0} " + "\n", cdvChartID.SelectedValueToQueryString);

            if (cdvColID.Text != "ALL" && cdvColID.Text != "")
                strSqlString.AppendFormat("   AND A.COL_SET_ID {0} " + "\n", cdvColID.SelectedValueToQueryString);

            if (cdvCharID.Text != "ALL" && cdvCharID.Text != "")
                strSqlString.AppendFormat("   AND A.CHAR_ID {0} " + "\n", cdvCharID.SelectedValueToQueryString);

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND D.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND D.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND D.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND D.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND D.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND D.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND D.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND D.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND D.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.Append(" ORDER BY A.CHART_ID, A.COL_SET_ID, A.CHAR_ID, B.VERSION DESC " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion


        #region " Button Event "
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private void btnView_Click(object sender, EventArgs e)
        {
            if (CheckField() == false) return;
            LoadingPopUp.LoadIngPopUpShow(this);
            dt = null;

            try
            {
                this.Refresh();
                //LoadingPopUp.LoadIngPopUpShow(this);

                GridColumnInit();

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

                for (int i = 0; i <= 8; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LemonChiffon;
                }
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

        #endregion

        private void cdvCharID_ValueButtonPress(object sender, EventArgs e)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT B.CHAR_ID AS Code, A.CHAR_DESC AS Data " + "\n");
            strSqlString.Append("  FROM MEDCCHRDEF A" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT DISTINCT CHAR_ID" + "\n");
            strSqlString.Append("          FROM MSPCCHTDEF" + "\n");
            strSqlString.Append("         WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("           AND DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("           AND CHT_CMF_4 = 'PQC'" + "\n");
            strSqlString.Append("       ) B" + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND A.CHAR_ID = B.CHAR_ID " + "\n");
            strSqlString.Append(" ORDER BY B.CHAR_ID " + "\n");

            cdvCharID.sDynamicQuery = strSqlString.ToString();
        }

        private void cdvColID_ValueButtonPress(object sender, EventArgs e)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT B.COL_SET_ID AS Code, A.COL_SET_DESC AS Data " + "\n");
            strSqlString.Append("  FROM MEDCCOLDEF A" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT DISTINCT COL_SET_ID" + "\n");
            strSqlString.Append("          FROM MSPCCHTDEF" + "\n");
            strSqlString.Append("         WHERE FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("           AND DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("           AND CHT_CMF_4 = 'PQC'" + "\n");
            strSqlString.Append("       ) B" + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND A.COL_SET_ID = B.COL_SET_ID " + "\n");
            strSqlString.Append("   AND A.DELETE_FLAG = ' ' " + "\n");            
            strSqlString.Append(" ORDER BY B.COL_SET_ID " + "\n");

            cdvColID.sDynamicQuery = strSqlString.ToString();
        }

        private void cdvOper_ValueButtonPress(object sender, EventArgs e)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT DISTINCT OPER AS Code, OPER_DESC AS Data " + "\n");
            strSqlString.Append("  FROM MWIPOPRDEF " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("   AND OPER_GRP_1 <> '-' " + "\n");
            strSqlString.Append(" ORDER BY OPER " + "\n");

            cdvOper.sDynamicQuery = strSqlString.ToString();
        }

        private void cdvChartID_ValueButtonPress(object sender, EventArgs e)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT CHART_ID AS Code, CHART_DESC AS Data " + "\n");
            strSqlString.Append("  FROM MSPCCHTDEF " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("   AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("   AND CHT_CMF_4 = 'PQC' " + "\n");
            strSqlString.Append(" ORDER BY CHART_ID " + "\n");

            cdvChartID.sDynamicQuery = strSqlString.ToString();
        }        
    }
}
