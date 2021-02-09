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
    public partial class PRD010426 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010426<br/>
        /// 클래스요약: V2 공용 일별 Trend 조회<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2020-08-27<br/>
        /// 상세  설명: V2 공용 일별 Trend 조회<br/>
        /// 변경  내용: <br/>
        /// 2020-09-08-임종우 : 장기정체 Lot 조회 기능 추가 (김성업K 요청)
        /// </summary>
        public PRD010426()
        {
            InitializeComponent();

            cdvFromToDate.AutoBinding();
            SortInit();
            GridColumnInit();
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
            //if (cdvOper.Text.TrimEnd() == "")
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
            //    return false;
            //}

            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;

            if (cboType.Text == "설비 가동률")
            {
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("담당팀", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("설비명", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("TYPE", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);               
                spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
            }
            else
            {
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("공정", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("MAT_ID", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 100);                
                spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
            }

            spdData.RPT_ColumnConfigFromTable_New(btnSort);
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            
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

            string sInQuery = string.Empty;
            string sInQuery2 = string.Empty;
            string strDate = string.Empty;
            string sFrom = null;
            string sTo = null;

            string[] selectDate = new string[cdvFromToDate.SubtractBetweenFromToDate + 1];
            selectDate = cdvFromToDate.getSelectDate();

            for (int i = 0; i <= cdvFromToDate.SubtractBetweenFromToDate; i++)
            {
                if(i != cdvFromToDate.SubtractBetweenFromToDate)
                {
                    sInQuery += "'" + selectDate[i] + "',";
                    sInQuery2 += "'" + selectDate[i] + "22',";
                }
                else
                {
                    sInQuery += "'" + selectDate[i] + "'";
                    sInQuery2 += "'" + selectDate[i] + "22'";
                }
            }
 
                        
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
                    break;
                case "WEEK":
                    sFrom = cdvFromToDate.FromWeek.SelectedValue.ToString();
                    sTo = cdvFromToDate.ToWeek.SelectedValue.ToString();
                    strDate = "WORK_WEEK";
                    break;
                case "MONTH":
                    sFrom = cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM");
                    sTo = cdvFromToDate.ToYearMonth.Value.ToString("yyyMM");
                    strDate = "WORK_MONTH";
                    break;
                default:
                    sFrom = cdvFromToDate.FromDate.Text.Replace("-", "");
                    sTo = cdvFromToDate.ToDate.Text.Replace("-", "");
                    strDate = "WORK_DATE";
                    break;
            }

            if (cboType.Text == "설비 가동률")
            {
                strSqlString.Append("SELECT NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = 'HMVA2' AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = B.RES_GRP_1), '-') AS TEAM " + "\n");
                strSqlString.Append("     , A.*" + "\n");
                strSqlString.Append("  FROM ( " + "\n");
                strSqlString.Append("        SELECT *" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT WORK_DATE, RES_ID, 'TOTAL_PERCENT' AS TYPE, TOTAL_PERCENT" + "\n");
                strSqlString.Append("                  FROM FMB_SUMRAS@RPTTOMES" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + sFrom + "' AND '" + sTo + "' " + "\n");
                strSqlString.Append("               )" + "\n");
                strSqlString.Append("         PIVOT (MAX(TOTAL_PERCENT) FOR WORK_DATE IN (" + sInQuery + "))" + "\n");
                strSqlString.Append("         UNION ALL" + "\n");
                strSqlString.Append("        SELECT *" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT WORK_DATE, RES_ID, 'ABILITY_PERCENT' AS TYPE, ABILITY_PERCENT" + "\n");
                strSqlString.Append("                  FROM FMB_SUMRAS@RPTTOMES" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + sFrom + "' AND '" + sTo + "' " + "\n");
                strSqlString.Append("               )" + "\n");
                strSqlString.Append("         PIVOT (MAX(ABILITY_PERCENT) FOR WORK_DATE IN (" + sInQuery + "))" + "\n");
                strSqlString.Append("         UNION ALL" + "\n");
                strSqlString.Append("        SELECT *" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT WORK_DATE, RES_ID, 'TIME_PERCENT' AS TYPE, TIME_PERCENT" + "\n");
                strSqlString.Append("                  FROM FMB_SUMRAS@RPTTOMES" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                   AND WORK_DATE BETWEEN '" + sFrom + "' AND '" + sTo + "' " + "\n");
                strSqlString.Append("               )" + "\n");
                strSqlString.Append("         PIVOT (MAX(TIME_PERCENT) FOR WORK_DATE IN (" + sInQuery + "))" + "\n");
                strSqlString.Append("       ) A" + "\n");
                strSqlString.Append("     , MRASRESDEF B" + "\n");
                strSqlString.Append(" WHERE 1=1" + "\n");
                strSqlString.Append("   AND B.FACTORY = 'HMVA2'" + "\n");
                strSqlString.Append("   AND A.RES_ID = B.RES_ID" + "\n");
                strSqlString.Append(" ORDER BY TEAM, A.RES_ID, DECODE(TYPE, 'TOTAL_PERCENT', 1, 'ABILITY_PERCENT', 2, 3)" + "\n");
            }
            else
            {
                strSqlString.Append("SELECT *" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT SUBSTR(CUTOFF_DT, 1, 8) AS WORK_DATE, OPER, MAT_ID, COUNT(*) AS LOT_QTY" + "\n");
                strSqlString.Append("          FROM RWIPLOTSTS_BOH" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("           AND CUTOFF_DT IN (" + sInQuery2 + ")" + "\n");
                strSqlString.Append("           AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("           AND LOT_DEL_FLAG = ' '" + "\n");
                strSqlString.Append("           AND OPER NOT IN ('00001','00002')" + "\n");
                strSqlString.Append("           AND TRUNC(TO_DATE(CUTOFF_DT, 'YYYYMMDDHH24') - TO_DATE(LOT_CMF_14,'YYYYMMDDHH24MISS'), 2) > 2" + "\n");
                strSqlString.Append("         GROUP BY SUBSTR(CUTOFF_DT, 1, 8), OPER, MAT_ID" + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == sTo)
                {
                    strSqlString.Append("         UNION ALL" + "\n");
                    strSqlString.Append("        SELECT '" + sTo + "' WORK_DATE, OPER, MAT_ID, COUNT(*) AS LOT_QTY" + "\n");
                    strSqlString.Append("          FROM RWIPLOTSTS" + "\n");
                    strSqlString.Append("         WHERE 1=1" + "\n");
                    strSqlString.Append("           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                    strSqlString.Append("           AND LOT_TYPE = 'W'" + "\n");
                    strSqlString.Append("           AND LOT_DEL_FLAG = ' '" + "\n");
                    strSqlString.Append("           AND OPER NOT IN ('00001','00002')" + "\n");
                    strSqlString.Append("           AND TRUNC(TO_DATE('" + sTo + "', 'YYYYMMDD') - TO_DATE(LOT_CMF_14,'YYYYMMDDHH24MISS'), 2) > 2" + "\n");
                    strSqlString.Append("         GROUP BY OPER, MAT_ID" + "\n");
                }

                strSqlString.Append("       )" + "\n");
                strSqlString.Append(" PIVOT (SUM(LOT_QTY) FOR WORK_DATE IN (" + sInQuery + "))" + "\n");
                strSqlString.Append(" ORDER BY OPER, MAT_ID" + "\n");
            }          

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

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {          
            this.SetFactory(cdvFactory.txtValue);            
            cdvOper.sFactory = cdvFactory.txtValue;

            SortInit(); //Add. 20150602
        }
        #endregion
    }
}