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
    /// 클  래  스: RAS020309<br/>
    /// 클래스요약: 순간정지 이력<br/>
    /// 작  성  자: 미라콤 양형석<br/>
    /// 최초작성일: 2008-12-29<br/>
    /// 상세  설명: 순간정지 이력<br/>
    /// 변경  내용: <br/>
    /// 2009.10.22 (임종우)
    /// 1. 알람 코드를 알람 ID로 변경(기준정보 맵핑)  2. 가동시간 표시  3. MTBA 표시
    /// 2011.02.24 (김민우)
    /// 1. GET_WORK_DATE FUNCTION 사용으로 인하여 쿼리 속도가 늦어 CRASALMHIS@RPTTOMES 테이블 RESV_FIELD_2 컬럼에 날짜를 저장하고 검색시 해당 컬럼 사용으로 변경 
    /// 2. 주코드삭제
    /// </summary>
    public partial class RAS020309 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public RAS020309()
        {
            InitializeComponent();
            udcFromToDate.yesterday_flag = true;
            udcFromToDate.AutoBinding();
            //udcFromToDate.DaySelector.SelectedValue = "MONTH";
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

            spdData.RPT_AddBasicColumn("date", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            //spdData.RPT_AddBasicColumn("Work Week", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Team in charge", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("파트", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("EQP Type", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Maker", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Model", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Equipment name", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);

            spdData.RPT_AddBasicColumn("Uptime", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("MTBA", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 70);
            //spdData.RPT_AddBasicColumn("Momentary stop code", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Alarm ID", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("number of cases", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            //spdData.RPT_AddBasicColumn("Momentary stop description", 0, 10, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("date", "RES.WORK_DAY", "RES.WORK_DAY", "WORK_DATE", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Work Week", "GET_WORK_DATE(ALM.TRAN_TIME, 'W') AS WORK_WEEK", "ALM.TRAN_TIME", "WORK_DATE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Team in charge", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = DEF.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = DEF.RES_GRP_1), '-') AS TEAM", "DEF.RES_GRP_1", "RES.RES_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Part", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = DEF.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = DEF.RES_GRP_2), '-') AS PART", "DEF.RES_GRP_2", "RES.RES_GRP_2", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("EQP Type", "DEF.RES_GRP_3 AS EQP_TYPE", "DEF.RES_GRP_3", "RES_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Maker", "DEF.RES_GRP_5 AS MAKER", "DEF.RES_GRP_5", "RES.RES_GRP_5", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "DEF.RES_GRP_6 AS MODEL", "DEF.RES_GRP_6", "RES.RES_GRP_6", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment name", "DEF.RES_ID AS RES", "DEF.RES_ID", "RES.RES_ID", true);
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

            if(QueryCond1.Contains("RES.WORK_DAY"))
            {

            }

            string strFromDate = udcFromToDate.ExactFromDate;
            string strToDate = udcFromToDate.ExactToDate;
            string strFromDay = udcFromToDate.HmFromDay;
            string strToDay = udcFromToDate.HmToDay;

            string JoupTime = GetJoupTime(strFromDate, strToDate);

            strSqlString.Append("SELECT " + QueryCond1 + "\n");
            strSqlString.Append("     , ROUND(RES_SUM.RUN_TIME,2) AS RUN_TIME" + "\n");
            strSqlString.Append("     , ROUND(RES_SUM.MTBA,2) AS MTBA " + "\n");
            strSqlString.Append("     , ALM.ALARM_EQ_ID, SUM(ALM.ALARM_CNT) AS CNT " + "\n");
            strSqlString.Append("  FROM CSUMALMRES@RPTTOMES RES " + "\n"); 
            strSqlString.Append("     , CSUMALMCNT@RPTTOMES ALM " + "\n");
            strSqlString.Append("     , MRASRESDEF DEF " + "\n");
            strSqlString.Append("     , ( " + "\n");

            if (QueryCond1.Contains("RES.WORK_DAY"))
            {
                strSqlString.Append("        SELECT WORK_DAY, RES_ID  " + "\n");
                strSqlString.Append("             , SUM(RUN_TIME) AS RUN_TIME " + "\n");
                strSqlString.Append("             , SUM(ALARM_CNT) AS CNT " + "\n");
                strSqlString.Append("             , SUM(RUN_TIME) / DECODE(SUM(ALARM_CNT),0,1,SUM(ALARM_CNT)) AS MTBA " + "\n");
                strSqlString.Append("          FROM CSUMALMRES@RPTTOMES " + "\n");
                strSqlString.Append("         WHERE WORK_DAY BETWEEN '" + strFromDay + "' AND '" + strToDay + "' " + "\n");
                strSqlString.Append("        GROUP BY WORK_DAY,RES_ID " + "\n");

            }
            else
            {
                strSqlString.Append("        SELECT RES_ID  " + "\n");
                strSqlString.Append("             , SUM(RUN_TIME) AS RUN_TIME " + "\n");
                strSqlString.Append("             , SUM(ALARM_CNT) AS CNT " + "\n");
                strSqlString.Append("             , SUM(RUN_TIME) / DECODE(SUM(ALARM_CNT),0,1,SUM(ALARM_CNT)) AS MTBA " + "\n");
                strSqlString.Append("          FROM CSUMALMRES@RPTTOMES " + "\n");
                strSqlString.Append("         WHERE WORK_DAY BETWEEN '" + strFromDay + "' AND '" + strToDay + "' " + "\n");
                strSqlString.Append("        GROUP BY RES_ID " + "\n");
            }
            strSqlString.Append("       ) RES_SUM " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND RES.FACTORY = DEF.FACTORY " + "\n");
            strSqlString.Append("   AND RES.RES_ID = ALM.RES_ID " + "\n");
            strSqlString.Append("   AND RES.RES_ID = DEF.RES_ID " + "\n");
            strSqlString.Append("   AND RES.RES_ID = RES_SUM.RES_ID " + "\n");
            strSqlString.Append("   AND RES.WORK_DAY = ALM.WORK_DAY " + "\n");
            if (QueryCond1.Contains("RES.WORK_DAY"))
            {
                strSqlString.Append("   AND RES.WORK_DAY = RES_SUM.WORK_DAY " + "\n");
            }
            strSqlString.Append("   AND RES.WORK_DAY BETWEEN '" + strFromDay + "' AND '" + strToDay + "' " + "\n");

            #region " RAS 상세 조회 "
            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("   AND DEF.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("   AND DEF.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("   AND DEF.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("   AND DEF.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("   AND DEF.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("   AND DEF.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("   AND DEF.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

            #endregion

            strSqlString.Append(" GROUP BY DEF.FACTORY, " + QueryCond2 + ", ALM.ALARM_EQ_ID, RES_SUM.RUN_TIME, RES_SUM.MTBA" + "\n");
            strSqlString.Append(" ORDER BY DEF.FACTORY, " + QueryCond2 + ", ALM.ALARM_EQ_ID, RES_SUM.RUN_TIME, RES_SUM.MTBA" + "\n");
                       
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private string GetJoupTime(string strFromDate, string strToDate)
        {
            int year = 0;
            int month = 0;
            int day = 0;

            DateTime dtFrom = DateTime.MinValue;
            DateTime dtTo = DateTime.MinValue;

            int retValue = 0;

            try
            {
                year = Convert.ToInt32(strFromDate.Substring(0, 4));
                month = Convert.ToInt32(strFromDate.Substring(4, 2));
                day = Convert.ToInt32(strFromDate.Substring(6, 2));

                dtFrom = new DateTime(year, month, day);

                string NowDate = Convert.ToString(DateTime.Now).Substring(0, 7);

                year = Convert.ToInt32(strToDate.Substring(0, 4));
                month = Convert.ToInt32(strToDate.Substring(4, 2));
                day = Convert.ToInt32(strToDate.Substring(6, 2));

                dtTo = new DateTime(year, month, day);

                string TmpsTo = Convert.ToString(dtTo).Substring(0, 7);

                if (udcFromToDate.DaySelector.SelectedValue.ToString() == "MONTH" && TmpsTo == NowDate)
                {
                    // (금일 - 1) 로 변경 (2009.09.22 임종우)
                    retValue = (new TimeSpan(DateTime.Now.AddDays(-1).Ticks - dtFrom.Ticks)).Days * 1440;
                }
                else
                {
                    retValue = (new TimeSpan(dtTo.Ticks - dtFrom.Ticks)).Days * 1440;
                }
            }
            catch
            {
                throw new InvalidCastException(RptMessages.GetMessage("STD099", GlobalVariable.gcLanguage));
            }


            return retValue.ToString();
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
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ");
            spdData.ExportExcel();          
        }

        #region 기타
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
        }
        #endregion
    }
}