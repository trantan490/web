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
    /// 클  래  스: RAS020213<br/>
    /// 클래스요약: 삼성귀책 설비 이력<br/>
    /// 작  성  자: 이희석<br/>
    /// 최초작성일: 2020-06-04<br/>
    /// 상세  설명: 삼성귀책 설비 이력<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class RAS020213 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public RAS020213()
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


            spdData.RPT_AddBasicColumn("date", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Work Week", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Team in charge", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("파트", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("EQP Type", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Maker", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Model", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Equipment name", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);

            spdData.RPT_AddBasicColumn("Down Code", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Up Code", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Down Time", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);            
            spdData.RPT_AddBasicColumn("Disable Time", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 100);
            spdData.RPT_AddBasicColumn("Maint Start Time", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Enable Time", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 100);
            spdData.RPT_AddBasicColumn("Maint Comment", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 100);
            spdData.RPT_AddBasicColumn("Down Comment", 0, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 150);
            spdData.RPT_AddBasicColumn("Up Comment", 0, 16, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 150);
            spdData.RPT_AddBasicColumn("Down User", 0, 17, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
            spdData.RPT_AddBasicColumn("Up User", 0, 18, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("date", "TO_CHAR(TO_DATE(DOWN_TRAN_TIME, 'YYYYMMDDHH24MISS') + 2/24, 'YYYYMMDD') AS DOWN_DATE", "DOWN_TRAN_TIME", "DOWN_TRAN_TIME", "DOWN_DATE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Work Week", "' '", "' '", "' '", "PLAN_YEAR||PLAN_WEEK AS DOWN_WEEK", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Team in charge", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_1), '-') AS TEAM", "RES.RES_GRP_1", "RES.RES_GRP_1", "TEAM", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Part", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_2), '-') AS PART", "RES.RES_GRP_2", "RES.RES_GRP_2", "PART", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("EQP Type", "RES.RES_GRP_3 AS EQP_TYPE", "RES_GRP_3", "RES_GRP_3", "EQP_TYPE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Maker", "RES.RES_GRP_5 AS MAKER", "RES.RES_GRP_5", "RES.RES_GRP_5", "MAKER", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "RES.RES_GRP_6 AS MODEL", "RES.RES_GRP_6", "RES.RES_GRP_6", "MODEL", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment name", "DWH.RES_ID AS RES", "RES.RES_ID", "RES.RES_ID", "RES", true);
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
            string QueryCond4 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            string strFromDate = udcFromToDate.ExactFromDate;
            string strToDate = udcFromToDate.ExactToDate;

            // 2010-09-30-임종우 : Down_Event 가 DEV_CHG 이면 품종교체(PKG교체, DVC 교체)후 처음 START 한 LOT의 Start_Time 과 Start 까지 걸린 시간 표시 (배진우 요청)
            strSqlString.Append("SELECT " + QueryCond4 + "\n");
            strSqlString.Append("     , DOWN_CODE, UP_CODE, DOWN_TIME, DISABLE_TIME, MAINT_START_TIME" + "\n");
            strSqlString.Append("     , ENABLE_TIME, MAINT_START_COMMENT, DOWN_TRAN_COMMENT, UP_TRAN_COMMENT, DOWN_TRAN_USER, UP_TRAN_USER_ID" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT " + QueryCond1 + "\n");
            strSqlString.Append("             , DOWN_NEW_STS_1 DOWN_CODE  " + "\n");
            strSqlString.Append("             , UP_NEW_STS_1 UP_CODE  " + "\n");
            strSqlString.Append("             , CASE WHEN UP_TRAN_TIME <> ' '  " + "\n");
            strSqlString.Append("                    THEN (CASE WHEN (UP_TRAN_TIME > '" + strToDate + "' and DOWN_TRAN_TIME < '" + strFromDate + "' ) " + "\n");
            strSqlString.Append("                                    THEN ROUND(TO_CHAR(TO_DATE('" + strToDate + "', 'YYYYMMDDHH24MISS') - TO_DATE('" + strFromDate + "', 'YYYYMMDDHH24MISS'))* 24 * 60,0)" + "\n");
            strSqlString.Append("                               WHEN DOWN_TRAN_TIME < '" + strFromDate + "'" + "\n");
            strSqlString.Append("                                    THEN ROUND(TO_CHAR(TO_DATE(UP_TRAN_TIME, 'YYYYMMDDHH24MISS') - TO_DATE('" + strFromDate + "', 'YYYYMMDDHH24MISS'))* 24 * 60,0)" + "\n");
            strSqlString.Append("                               WHEN UP_TRAN_TIME > '" + strToDate + "'" + "\n");
            strSqlString.Append("                                    THEN ROUND(TO_CHAR(TO_DATE('" + strToDate + "', 'YYYYMMDDHH24MISS') - TO_DATE(DOWN_TRAN_TIME, 'YYYYMMDDHH24MISS'))* 24 * 60,0)" + "\n");
            strSqlString.Append("                               ELSE ROUND(TO_CHAR(TO_DATE(UP_TRAN_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(DOWN_TRAN_TIME, 'YYYYMMDDHH24MISS'))* 24 * 60,0)" + "\n");
            strSqlString.Append("                          END)" + "\n");
            strSqlString.Append("                    ELSE (CASE WHEN DOWN_TRAN_TIME < '" + strFromDate + "'" + "\n");
            strSqlString.Append("                                    THEN ROUND(TO_CHAR(TO_DATE('" + strToDate + "', 'YYYYMMDDHH24MISS') - TO_DATE('" + strFromDate + "', 'YYYYMMDDHH24MISS'))* 24 * 60,0)" + "\n");
            strSqlString.Append("                               ELSE ROUND(TO_CHAR(TO_DATE('" + strToDate + "', 'YYYYMMDDHH24MISS') - TO_DATE(DOWN_TRAN_TIME, 'YYYYMMDDHH24MISS'))* 24 * 60,0)" + "\n");
            strSqlString.Append("                          END) END AS DOWN_TIME" + "\n");
            strSqlString.Append("             , DECODE(DOWN_TRAN_TIME, ' ', ' ', TO_CHAR(TO_DATE(DOWN_TRAN_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD HH24:MI:SS')) DISABLE_TIME " + "\n");
            strSqlString.Append("             , DECODE(MAINT_START_TIME, ' ', ' ', TO_CHAR(TO_DATE(MAINT_START_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD HH24:MI:SS')) MAINT_START_TIME" + "\n");
            strSqlString.Append("             , DECODE(UP_TRAN_TIME, ' ', ' ', TO_CHAR(TO_DATE(UP_TRAN_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD HH24:MI:SS')) ENABLE_TIME " + "\n");
            strSqlString.Append("             , UP_TRAN_TIME " + "\n");
            strSqlString.Append("             , MAINT_START_COMMENT " + "\n");
            strSqlString.Append("             , DOWN_TRAN_COMMENT  " + "\n");
            strSqlString.Append("             , UP_TRAN_COMMENT  " + "\n");
            strSqlString.Append("             , DECODE(DOWN_TRAN_USER_ID, ' ',' ', (SELECT USER_DESC FROM RWEBUSRDEF WHERE USER_ID=DOWN_TRAN_USER_ID)) || '(' || DECODE(DOWN_TRAN_USER_ID, ' ',' ',DOWN_TRAN_USER_ID) || ')' AS DOWN_TRAN_USER " + "\n");
            strSqlString.Append("             , DECODE(DOWN_TRAN_USER_ID, ' ',' ', (SELECT USER_DESC FROM RWEBUSRDEF WHERE USER_ID=UP_TRAN_USER_ID)) || '(' || DECODE(UP_TRAN_USER_ID, ' ',' ',UP_TRAN_USER_ID) || ')' AS UP_TRAN_USER_ID " + "\n");
            strSqlString.Append("          FROM CRASRESDWH DWH  " + "\n");
            strSqlString.Append("             , MRASRESDEF RES  " + "\n");
            strSqlString.Append("         WHERE 1=1  " + "\n");
            strSqlString.Append("           AND RES.FACTORY = DWH.FACTORY  " + "\n");
            strSqlString.Append("           AND RES.RES_ID = DWH.RES_ID  " + "\n");
            strSqlString.Append("           AND RES.FACTORY" + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("           AND RES.RES_TYPE NOT IN ('DUMMY')" + "\n");
            // 2011-04-05-김민우 8대 LOSS CODE 검색 조건 추가
            strSqlString.Append("           AND DWH.DOWN_NEW_STS_1 LIKE '" + cdvDownCode.Text + "%'" + "\n");
            strSqlString.Append("           AND (DWH.UP_TRAN_TIME >  '" + strFromDate + "' AND DWH.DOWN_TRAN_TIME <  '" + strToDate + "'  or  DWH.UP_TRAN_TIME=' ' AND DWH.DOWN_TRAN_TIME <  '" + strToDate + "')" + "\n");
            strSqlString.Append("           AND RES.RES_TYPE IN ('EQUIPMENT', 'LINE')" + "\n");
            strSqlString.Append("           AND (RES.RES_ID IN (SELECT KEY_1 FROM MGCMTBLDAT WHERE FACTORY = '" + cdvFactory.Text + "' AND TABLE_NAME = 'H_RES_CARE_LIST') or RES.RES_ID=' ')" + "\n");
            strSqlString.Append("           AND (UP_NEW_STS_1 IN (select KEY_1 from MGCMTBLDAT where table_name='H_SEC_FAULT_CODE') OR UP_NEW_STS_1=' ')" + "\n");


                #region " RAS 상세 조회 "
                if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                    strSqlString.AppendFormat("           AND RES.SUBRES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

                if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                    strSqlString.AppendFormat("           AND RES.SUBRES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

                if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                    strSqlString.AppendFormat("           AND RES.SUBRES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

                if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                    strSqlString.AppendFormat("           AND RES.SUBRES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

                if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                    strSqlString.AppendFormat("           AND RES.SUBRES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

                if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    strSqlString.AppendFormat("           AND RES.SUBRES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

                if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                    strSqlString.AppendFormat("           AND RES.SUBRES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);
                #endregion
     

            strSqlString.Append("         ORDER BY " + QueryCond3 + "\n");
            strSqlString.Append("       ) DAT" + "\n");

            if (QueryCond4.Contains("DOWN_WEEK") == true)
            {
                strSqlString.Append("     , (" + "\n");
                strSqlString.Append("        SELECT * FROM MWIPCALDEF WHERE CALENDAR_ID = 'HM'" + "\n");
                strSqlString.Append("       ) CAL" + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND DAT.DOWN_DATE = CAL.SYS_DATE(+)" + "\n");
            }

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
                //else if (dt.Rows.Count > 20000)
                //{
                //    dt.Dispose();
                //    LoadingPopUp.LoadingPopUpHidden();
                //    CmnFunction.ShowMsgBox("조회 건수가 20,000 건이 넘습니다.\n 검색 조건을 조정하여 다시 조회 하여 주세요.");
                //    return;
                //}

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
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ");
            spdData.ExportExcel();            
        }

        #region 기타
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            cdvDownCode.sFactory = cdvFactory.txtValue;
        }
        #endregion

        //2011-04-05-김민우: 8대 Loss Code 항목 초기화
        private void cdvDownCode_press(object sender, EventArgs e)
        {
            cdvDownCode.Text = "";
        }        
    }
}