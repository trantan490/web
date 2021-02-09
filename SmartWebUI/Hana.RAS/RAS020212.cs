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
    /// 클  래  스: RAS020212<br/>
    /// 클래스요약: 정비사 별 설비 정지 이력<br/>
    /// 작  성  자: 김민우<br/>
    /// 최초작성일: 2011-04-12<br/>
    /// 상세  설명: 정비사별 설비 정지 이력<br/>
    /// 변경  내용: <br/>
       
    /// </summary>
    public partial class RAS020212 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public RAS020212()
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


            spdData.RPT_AddBasicColumn("a time zone", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Equipment name", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("name", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Summary", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Maintenance contents", 0, 4, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);


            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("a time zone", "SUBSTR(UP_TRAN_TIME,9,2)", "SUBSTR(UP_TRAN_TIME,9,2)", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment name", "DWH.RES_ID", "DWH.RES_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("name", "(SELECT USER_DESC FROM RWEBUSRDEF WHERE USER_ID = DWH.UP_TRAN_USER_ID) || '(' || UP_TRAN_USER_ID || ')' AS USER_DESC", "UP_TRAN_USER_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Summary", "COUNT(*) AS CNT", "1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Maintenance contents", "UP_TRAN_COMMENT", "UP_TRAN_COMMENT", true);
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
            
            string strFromDate = udcFromToDate.ExactFromDate;
            string strToDate = udcFromToDate.ExactToDate;

            strSqlString.Append("SELECT " + QueryCond1 + "\n");
            strSqlString.Append("  FROM CRASRESDWH DWH" + "\n");
            strSqlString.Append("     , MRASRESDEF RES" + "\n");
            strSqlString.Append(" WHERE 1=1  " + "\n");
            strSqlString.Append("   AND RES.FACTORY = DWH.FACTORY  " + "\n");
            strSqlString.Append("   AND RES.RES_ID = DWH.RES_ID  " + "\n");
            strSqlString.Append("   AND RES.FACTORY" + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("   AND RES.RES_TYPE NOT IN ('DUMMY')" + "\n");
            strSqlString.Append("   AND DWH.UP_TRAN_TIME <> ' '" + "\n");
            strSqlString.Append("   AND DWH.HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("   AND DWH.UP_TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "' " + "\n");

            if (cbTime.Text != "ALL" )
            {
                strSqlString.Append("   AND SUBSTR(DWH.UP_TRAN_TIME,9,2) = '" + cbTime.Text + "' " + "\n");
            }
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
            
            strSqlString.Append(" GROUP BY " + QueryCond2 + "\n");
            strSqlString.Append(" ORDER BY " + QueryCond2 + "\n");
            #endregion
           
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
        }

        #endregion

    }
}