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
    public partial class PQC030206 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        DataTable dt = new DataTable();


        #region " PQC030206 : Program Initial "

        /// <summary>
        /// 클  래  스: PQC030206<br/>
        /// 클래스요약: CPK 기준 이상 발생 현황<br/>
        /// 작  성  자: 임종우<br/>
        /// 최초작성일: 2015-02-10 <br/>
        /// 상세  설명: CPK 기준 이상 발생 현황(최재주D 요청)<br/>
        /// 변경  내용:         
        /// </summary>
        /// 
        public PQC030206()
        {
            InitializeComponent();
            cdvFromToDate.DaySelector.SelectedValue = "WEEK";
            cdvFromToDate.AutoBindingUserSetting(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));            
            cdvFromToDate.FromDate.Visible = false;
            cdvFromToDate.ToDate.Visible = false;

            SortInit();
            GridColumnInit();

            // 기본공정은 HMKA1으로 설정
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            cdvRasId.sFactory = cdvFactory.Text;            
        }

        #endregion


        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if ((cdvCharID.Text == "ALL" || cdvCharID.Text == "") && (udcWIPCondition3.Text == "ALL" || udcWIPCondition3.Text == ""))
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD044", GlobalVariable.gcLanguage));
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
                if (rdbChar.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("Character ID", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                    spdData.RPT_AddBasicColumn("Collection ID Number", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 100);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Collection ID", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                    spdData.RPT_AddBasicColumn("Character ID number", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 100);
                }

                spdData.RPT_AddBasicColumn("Raw data number", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Cpk Target", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);                
                spdData.RPT_AddBasicColumn("Total", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);

                spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);

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

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string strDecode = null;
            string sWork = null;
            string sColChar = null;
            string[] selectDate = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            string strFromDate = cdvFromToDate.ExactFromDate;
            string strToDate = cdvFromToDate.ExactToDate;

            selectDate = cdvFromToDate.getSelectDate();

            for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                strDecode += "     , NVL(TO_CHAR(ROUND(100 - (SUM(DECODE(WORK_DATE, '" + selectDate[i] + "', CPKN, 0)) / SUM(DECODE(WORK_DATE, '" + selectDate[i] + "', 1)) * 100),1)), '-') AS A" + i + "\n";
            }

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
            {
                sWork = "W";
            }
            else
            {
                sWork = "M";
            }

            if (rdbChar.Checked == true)
            {
                sColChar = "CHAR_ID";
            }
            else
            {
                sColChar = "COL_SET_ID";
            }

            // 쿼리 
            strSqlString.Append("SELECT " + sColChar + "\n");
            strSqlString.Append("     , COUNT(*) AS CNT" + "\n");
            strSqlString.Append("     , SUM(VALUE_COUNT) AS VALUE_COUNT" + "\n");
            strSqlString.Append("     , '" + txtTargetCpk.Text.Trim() + "' AS CPK_TARGET" + "\n");
            strSqlString.Append("     , ROUND((COUNT(*) - SUM(CPKN)) / COUNT(*) * 100, 1) AS TOTAL" + "\n");
            strSqlString.Append(strDecode);            
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT CHAR_ID, COL_SET_ID" + "\n");
            strSqlString.Append("             , VALUE_COUNT " + "\n");
            strSqlString.Append("             , WORK_DATE" + "\n");
            strSqlString.Append("             , ROUND(ABS(CASE WHEN (RANGE = 0) THEN 0" + "\n");
            strSqlString.Append("                              WHEN (USL = 0) THEN ((AVERAGE - LSL) / (3 * (RANGE / D2)))" + "\n");
            strSqlString.Append("                              WHEN (LSL = 0) THEN ((USL - AVERAGE) / (3 * (RANGE / D2)))" + "\n");
            strSqlString.Append("                              WHEN USL - LSL = 0 THEN 0" + "\n");
            strSqlString.Append("                              ELSE (1 - (ABS(((USL + LSL) / 2) - AVERAGE) / ((USL - LSL) / 2))) *  ((USL - LSL) / (6 * (RANGE / D2)))" + "\n");
            strSqlString.Append("                         END), 2) AS CPK " + "\n");
            strSqlString.Append("             , CASE WHEN (ROUND(ABS(CASE WHEN (RANGE = 0) THEN 0" + "\n");
            strSqlString.Append("                              WHEN (USL = 0) THEN ((AVERAGE - LSL) / (3 * (RANGE / D2)))" + "\n");
            strSqlString.Append("                              WHEN (LSL = 0) THEN ((USL - AVERAGE) / (3 * (RANGE / D2)))" + "\n");
            strSqlString.Append("                              WHEN USL - LSL = 0 THEN 0" + "\n");
            strSqlString.Append("                              ELSE (1 - (ABS(((USL + LSL) / 2) - AVERAGE) / ((USL - LSL) / 2))) *  ((USL - LSL) / (6 * (RANGE / D2)))" + "\n");
            strSqlString.Append("                         END), 2)) < " + txtTargetCpk.Text.Trim() + " THEN 1 " + "\n");
            strSqlString.Append("                    ELSE 0" + "\n");
            strSqlString.Append("                END AS CPKN" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT B.CHAR_ID, A.COL_SET_ID   " + "\n");
            strSqlString.Append("                     , RPTMGR.GET_WORK_DATE(TRAN_TIME, '" + sWork + "') AS WORK_DATE" + "\n");
            strSqlString.Append("                     , SUM(A.VALUE_COUNT) AS VALUE_COUNT" + "\n");
            strSqlString.Append("                     , AVG(A.AVERAGE) AS AVERAGE" + "\n");
            strSqlString.Append("                     , AVG(A.RANGE) AS RANGE" + "\n");
            strSqlString.Append("                     , CASE WHEN SUM(DECODE(A.USL, ' ', 0, 1)) = 0 THEN 0" + "\n");
            strSqlString.Append("                            ELSE SUM(DECODE(A.USL, ' ', 0, A.USL)) / SUM(DECODE(A.USL, ' ', 0, 1))" + "\n");
            strSqlString.Append("                       END USL" + "\n");
            strSqlString.Append("                     , CASE WHEN SUM(DECODE(A.LSL, ' ', 0, 1)) = 0 THEN 0" + "\n");
            strSqlString.Append("                            ELSE SUM(DECODE(A.LSL, ' ', 0, A.LSL)) / SUM(DECODE(A.LSL, ' ', 0, 1))" + "\n");
            strSqlString.Append("                       END LSL " + "\n");
            strSqlString.Append("                     , MAX((SELECT DATA_2 FROM MGCMTBLDAT WHERE FACTORY = '" + cdvFactory.Text + "' AND TABLE_NAME = 'H_SPC_CHAR_D2' AND KEY_1 = B.CHAR_ID)) AS D2" + "\n");
            strSqlString.Append("                  FROM MSPCCALDAT@RPTTOMES A  " + "\n");
            strSqlString.Append("                     , MSPCCHTDEF@RPTTOMES B" + "\n");
            strSqlString.Append("                 WHERE 1=1   " + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                   AND A.CHART_ID = B.CHART_ID" + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                   AND A.TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
            strSqlString.Append("                   AND A.RES_ID " + cdvRasId.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND A.VALUE_COUNT >= 2" + "\n");
            strSqlString.Append("                   AND A.EXCLUDE_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND B.LOT_RES_FLAG = 'L'" + "\n");
            strSqlString.Append("                   AND B.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND B.SYNC_EDC_FLAG = 'Y'" + "\n");

            #region 상세 조회에 따른 SQL문 생성

            if (cdvCharID.Text != "ALL" && cdvCharID.Text != "")
                strSqlString.AppendFormat("                   AND B.CHAR_ID " + cdvCharID.SelectedValueToQueryString + "\n");

            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                   AND B.CHT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                   AND B.CHT_GRP_2 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                   AND B.CHT_GRP_3 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                   AND B.CHT_GRP_4 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                   AND B.CHT_GRP_5 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                   AND B.CHT_GRP_6 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            #endregion


            strSqlString.Append("                 GROUP BY B.CHAR_ID, A.COL_SET_ID, RPTMGR.GET_WORK_DATE(TRAN_TIME, '" + sWork + "')" + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" GROUP BY " + sColChar + "\n");

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

            strSqlString.Append("SELECT DISTINCT CHAR_ID Code, '' Data " + "\n");
            strSqlString.Append("  FROM MSPCCHTDEF " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("   AND LOT_RES_FLAG = 'L' " + "\n");
            strSqlString.Append("   AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("   AND SYNC_EDC_FLAG = 'Y' " + "\n");
            strSqlString.Append("   AND CHAR_ID <> ' ' " + "\n");
            strSqlString.Append(" ORDER BY CHAR_ID " + "\n");

            cdvCharID.sDynamicQuery = strSqlString.ToString();
        }

        private void PQC030206_Load(object sender, EventArgs e)
        {
            pnlWIPDetail.Visible = true;
        }
    }
}
