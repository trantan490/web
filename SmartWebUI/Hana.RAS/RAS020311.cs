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
    /// 클  래  스: RAS020311<br/>
    /// 클래스요약: 순간 정지 조치 시간<br/>
    /// 작  성  자: 정병주<br/>
    /// 최초작성일: 2010-04-29<br/>
    /// 상세  설명: 순간 정지 조치 시간<br/>
    /// 변경  내용: <br/>
    /// 
    /// </summary>
    public partial class RAS020311 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public RAS020311()
        {
            InitializeComponent();
            udcFromToDate.yesterday_flag = true; 
            udcFromToDate.AutoBinding();
            SortInit();
            GridColumnInit();
            // 2010 - 04 - 30 정병주 : 04/30 현재 설비는 WIRE BOND 설비만 조회 되기 때문에 임의로 FACTORY 및 공정을 집어 넣음.
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            cdvStep.Text = "A0600";
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
            int iIdx;

            iIdx = 0;
            spdData.RPT_AddBasicColumn("RES_ID", 0, iIdx, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            iIdx += 1;
            spdData.RPT_AddBasicColumn("MAKER", 0, iIdx, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            iIdx += 1;
            spdData.RPT_AddBasicColumn("MODEL", 0, iIdx, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            iIdx += 1;
            spdData.RPT_AddBasicColumn("Total time required (minutes)", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
            iIdx += 1;
            spdData.RPT_AddBasicColumn("Number of momentary stops", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            iIdx += 1;
            spdData.RPT_AddBasicColumn("Average Action Time (min)", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
            
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {   // 2010 - 04 - 30 정병주 : 
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAKER", "MAKE_MODEL", "MAKE_MODEL", "MAKE_MODEL", "AA.MAKE_MODEL", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MODEL", "MODEL", "MODEL", "' '", "AA.MODEL", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RES_ID", "RES_ID", "RES_ID", "RES_ID", "AA.RES_ID", true);

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

            strSqlString.AppendFormat("SELECT {0},AA.CLEAR_TIME,AA.CNT, " + "\n", QueryCond4);
            strSqlString.Append("ROUND(AA.CLEAR_TIME / AA.CNT, 2) AS AVG" + "\n");
            strSqlString.Append("FROM   (" + "\n");
            strSqlString.AppendFormat("     SELECT  {0}, " + "\n", QueryCond1);
            strSqlString.Append("            SUM(CLEAR_TIME) AS CLEAR_TIME," + "\n");
            strSqlString.Append("            SUM(CNT) AS CNT" + "\n");
            strSqlString.Append("     FROM   (SELECT RES.FACTORY AS FACTORY," + "\n");
            strSqlString.Append("                    RES.RES_STS_8 AS OPER," + "\n");
            strSqlString.Append("                    RES.RES_ID AS RES_ID," + "\n");
            strSqlString.Append("                    RES.RES_GRP_5 AS MAKE_MODEL," + "\n");
            strSqlString.Append("                    RES.RES_GRP_6 AS MODEL," + "\n");
            strSqlString.Append("                    ROUND((VAL.CLEAR_TIME / 60), 2) AS CLEAR_TIME," + "\n");
            strSqlString.Append("                    VAL.CNT AS CNT," + "\n");
            strSqlString.Append("                    ROUND(VAL.CLEAR_TIME / VAL.CNT , 2) AS AVG" + "\n");
            strSqlString.Append("             FROM   MRASRESDEF RES," + "\n");
            strSqlString.Append("                    (SELECT HIS.RES_ID AS RES_ID," + "\n");
            strSqlString.Append("                            DEF.RES_GRP_5 AS MODEL," + "\n");
            strSqlString.Append("                            SUM(HIS.PERIOD) AS CLEAR_TIME," + "\n");
            strSqlString.Append("                            COUNT(*) AS CNT" + "\n");
            strSqlString.Append("                     FROM   (SELECT FACTORY," + "\n");
            strSqlString.Append("                                    RES_ID," + "\n");
            strSqlString.Append("                                    ALARM_CAT," + "\n");
            strSqlString.Append("                                    ALARM_DESC," + "\n");
            strSqlString.Append("                                    TRAN_TIME," + "\n");
            strSqlString.Append("                                    PERIOD" + "\n");
            //strSqlString.Append("                             FROM   CRASALMHIS@RPTTOMES" + "\n");
            strSqlString.Append("                             FROM   MRASALMHIS@RPTTOMES" + "\n");
            strSqlString.AppendFormat("               WHERE  TRAN_TIME BETWEEN  '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
            //strSqlString.Append("                       AND CLEAR_TIME > 0  " + "\n");
            strSqlString.Append("                       AND    PERIOD > 0  " + "\n");
            strSqlString.Append("                       AND    UP_DOWN_FLAG = 'U'  " + "\n");
            strSqlString.Append("                       AND    ALARM_USE = 'Y'  " + "\n");
            if (cdvRes_ID.Text != "" || cdvRes_ID.Text != "ALL")
            {
                //strSqlString.AppendFormat("               AND    RES_ID = '{0}' " + "\n", cdvRes_ID.Text);
                strSqlString.AppendFormat("               AND    RES_ID " + cdvRes_ID.SelectedValueToQueryString + "\n");
            }
            strSqlString.Append("                             GROUP BY FACTORY, RES_ID, ALARM_CAT, ALARM_DESC, TRAN_TIME, PERIOD) HIS," + "\n");
            strSqlString.Append("                            (SELECT RES_ID," + "\n");
            strSqlString.Append("                                    RES_GRP_5," + "\n");
            strSqlString.Append("                                    RES_GRP_6," + "\n");
            strSqlString.Append("                                    RES_STS_8" + "\n");
            strSqlString.Append("                             FROM   MRASRESDEF" + "\n");
            if (cdvModel.Text != "" || cdvModel.Text != "ALL")
            {
               // strSqlString.AppendFormat("               WHERE RES_GRP_6 IN ('{0}') " + "\n", cdvModel.Text);
                strSqlString.AppendFormat("               WHERE RES_GRP_6 " + cdvModel.SelectedValueToQueryString + "\n");
            }
            strSqlString.Append("                             GROUP BY RES_ID, RES_GRP_5, RES_GRP_6, RES_STS_8) DEF" + "\n");
            strSqlString.Append("                     WHERE  1 = 1" + "\n");
            strSqlString.Append("                     AND    HIS.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                     AND    HIS.RES_ID = DEF.RES_ID" + "\n");
            strSqlString.Append("                     GROUP BY HIS.RES_ID, DEF.RES_GRP_5" + "\n");
            strSqlString.Append("                     ORDER BY HIS.RES_ID, DEF.RES_GRP_5) VAL" + "\n");
            strSqlString.Append("             WHERE  1 = 1" + "\n");
            strSqlString.Append("             AND    RES.RES_ID = VAL.RES_ID" + "\n");
            strSqlString.Append("             GROUP BY RES.FACTORY, RES.RES_STS_8, RES.RES_ID, RES.RES_GRP_5, RES.RES_GRP_6, VAL.CLEAR_TIME, VAL.CNT)" + "\n");
            strSqlString.Append("     WHERE  1 = 1" + "\n");
            strSqlString.AppendFormat(" GROUP BY {0}", QueryCond2);
            strSqlString.AppendFormat(" ORDER BY {0}", QueryCond2);
            strSqlString.Append(") AA " + "\n");
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
            int Com_CNT = 0;
            DataTable dt = null;
            if (CheckField() == false) return;
            int iSubTotalCount = 0;
             udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
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
                if (tableForm.SelectedValueToQueryContainNull.Contains("RES_ID") == false)
                {

                    iSubTotalCount += 1;
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, iSubTotalCount, 3, null, null);
                    spdData.RPT_AutoFit(false);
                    dt.Dispose();
                    Com_CNT = spdData.ActiveSheet.ColumnCount;
                    SetAvgVertical(1, Com_CNT, true);

                }

                //cdvRes_ID.Text = "";
                //cdvModel.Text = "";
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

        #region SubTotal 평균
        public void SetAvgVertical(int nSampleNormalRowPos, int nColPos, bool bWithNull)
        {

            double Time = 0;
            double Cnt = 0;
            Time = Convert.ToDouble(spdData.ActiveSheet.Cells[0, nColPos - 3].Value);
            Cnt  = Convert.ToDouble(spdData.ActiveSheet.Cells[0, nColPos - 2].Value);
            spdData.ActiveSheet.Cells[0, nColPos - 1].Value = Math.Round(Time / Cnt, 0);
            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPos - 1].BackColor;

            for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                if (spdData.ActiveSheet.Cells[i, nColPos - 1].BackColor == color)
                {

                }
                else
                { 
                    Time = 0;
                    Cnt = 0;
                    Time = Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos - 3].Value);
                    Cnt = Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos - 2].Value);
                    spdData.ActiveSheet.Cells[i, nColPos - 1].Value = Math.Round(Time / Cnt, 0);

                }
            }

        }
        #endregion


        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            spdData.ExportExcel();
        }

        #region 기타
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            //this.SetFactory(cdvFactory.txtValue);
            //cdvStep.sFactory = cdvFactory.Text;
            //cdvRes_ID.Text = "";
            //cdvAlarm_ID.Text = "";
            //cdvStep.Text = "";
            
        }
        #endregion

        private void cdvRes_ID_ValueButtonPress(object sender, EventArgs e)
        {
            string strquery1 = string.Empty;
            strquery1 = "SELECT DISTINCT RES_ID,RES_GRP_5 FROM MRASRESDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND RES_GRP_3 IN ('BACK LAP','SAW','WIRE BOND','DIE ATTACH') ORDER BY RES_ID";
            cdvRes_ID.sDynamicQuery = strquery1;
           // cdvAlarm_ID.Text = "";
        }

        //private void cdvAlarm_ID_ValueButtonPress(object sender, EventArgs e)
        //{
        //    if (cdvRes_ID.Text == "")
        //    {
        //        MessageBox.Show("설비를 선택해주세요!" , this.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        return;
        //    }
        //    else
        //    {
        //        string strquery1 = string.Empty;
        //        strquery1 = "SELECT DISTINCT KEY_2,DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_EQ_ALARM_ALL' AND KEY_1 IN(SELECT DISTINCT RES_GRP_6 FROM MRASRESDEF WHERE RES_ID = '" + cdvRes_ID.Text + "')ORDER BY KEY_2";
        //        cdvAlarm_ID.sDynamicQuery = strquery1;
        //    }
        //}

        private void cdvStep_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            //cdvRes_ID.Text = "";
            //cdvAlarm_ID.Text = "";
        }

        private void cdvModel_ValueButtonPress(object sender, EventArgs e)
        {
            string strquery1 = string.Empty;
            strquery1 = "SELECT DISTINCT RES_GRP_6, ' ' AS BB FROM MRASRESDEF WHERE  1 = 1 AND RES_GRP_3 IN ('BACK LAP','SAW','WIRE BOND','DIE ATTACH') GROUP BY RES_GRP_6 ORDER BY RES_GRP_6";
            cdvModel.sDynamicQuery = strquery1;
        }
    }
}