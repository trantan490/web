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
    /// 클  래  스: RAS020310<br/>
    /// 클래스요약: LOT별 순간정지 TOP 5 <br/>
    /// 작  성  자: 김민우<br/>
    /// 최초작성일: 2011-09-15<br/>
    /// 상세  설명: LOT별 순간정지 TOP 5 <br/>
    /// 변경  내용: <br/>
       
    /// </summary>
    public partial class RAS020310 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public RAS020310()
        {
            InitializeComponent();
            SortInit();
            GridColumnInit();
        }

        #region " Function Definition "
        #endregion

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (txtSearchLOT.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD009", GlobalVariable.gcLanguage));
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
            spdData.RPT_AddBasicColumn("ALARM_ID", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("ALARM_DESC", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 250);
            spdData.RPT_AddBasicColumn("COUNT", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 80);
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            //WB공정에서 해당 LOT의 START 시간과 END 시간을 가져온다.
            DataTable stDay = null;
            DataTable edDay = null;
            stDay = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStDay());
            edDay = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlEdDay());

            StringBuilder strSqlString = new StringBuilder();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            strSqlString.Append("SELECT ALARM_ID, ALARM_DESC, COUNT(*) AS CNT" + "\n");
            //strSqlString.Append("  FROM CRASALMHIS@RPTTOMES" + "\n");
            strSqlString.Append("  FROM MRASALMHIS@RPTTOMES" + "\n");
            strSqlString.Append(" WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("   AND TRAN_TIME BETWEEN '" + stDay.Rows[0][0].ToString() + "' AND '" + edDay.Rows[0][0].ToString() + "'  " + "\n");
            strSqlString.Append("   AND LOT_ID = '"+ txtSearchLOT.Text +"'  " + "\n");
            //strSqlString.Append("   AND CLEAR_TIME > 0  " + "\n");
            strSqlString.Append("   AND PERIOD > 0  " + "\n");
            strSqlString.Append("   AND UP_DOWN_FLAG = 'U'  " + "\n");
            strSqlString.Append("   AND ALARM_USE = 'Y'  " + "\n");
            strSqlString.Append(" GROUP BY ALARM_ID,ALARM_DESC  " + "\n");
            strSqlString.Append(" ORDER BY COUNT(*) DESC" + "\n");
           
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        // 해당 LOT이 WB 공정에서 START 된 날짜
        private string MakeSqlStDay()
        {
            StringBuilder strSqlString = new StringBuilder();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            strSqlString.Append("SELECT MIN(TRAN_TIME) AS TRAN_TIME FROM MWIPLOTHIS@RPTTOMES" + "\n");
            strSqlString.Append(" WHERE LOT_ID = '" + txtSearchLOT.Text + "'" + "\n");
            strSqlString.Append("   AND OPER LIKE 'A06%'" + "\n");
            strSqlString.Append("   AND TRAN_CODE = 'START'  " + "\n");
           
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        // 해당 LOT이 WB 공정에서 END 된 날짜
        private string MakeSqlEdDay()
        {
            StringBuilder strSqlString = new StringBuilder();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            strSqlString.Append("SELECT MAX(TRAN_TIME) AS TRAN_TIME FROM MWIPLOTHIS@RPTTOMES" + "\n");
            strSqlString.Append(" WHERE LOT_ID = '" + txtSearchLOT.Text + "'" + "\n");
            strSqlString.Append("   AND OLD_OPER LIKE 'A06%'" + "\n");
            strSqlString.Append("   AND TRAN_CODE = 'END'  " + "\n");
           
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

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
                //1.Griid 합계 표시
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

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ");
            spdData.ExportExcel();
        }
    }
}