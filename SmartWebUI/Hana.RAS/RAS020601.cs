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
    /// 클  래  스: RAS020601<br/>
    /// 클래스요약: LOT 별 설비 정지 이력<br/>
    /// 작  성  자: 김민우<br/>
    /// 최초작성일: 2011-03-31<br/>
    /// 상세  설명: LOT 별 설비 정지 이력<br/>
    /// 변경  내용: <br/>
    /// 
    /// </summary>
    
    public partial class RAS020601 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        private String[] lotArry = new String[2];

        public RAS020601()
        {
            InitializeComponent();
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

            if (txtLotId.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD009", GlobalVariable.gcLanguage));
                return false;
            }

            if (cdvOper.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD064", GlobalVariable.gcLanguage));
                return false;
            }

            if (txtTime.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD074", GlobalVariable.gcLanguage));
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


            spdData.RPT_AddBasicColumn("EventID", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("상태", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Product", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Equipment number", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Stop time", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("DOWN content", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("waiting time", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Completion Code", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Complete Description", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Total stop (minutes)", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Completed content", 0, 10, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("worker", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
            spdData.RPT_AddBasicColumn("Mechanic", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
         
            //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            /*
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("date", "TO_CHAR(TO_DATE(DOWN_TRAN_TIME, 'YYYYMMDDHH24MISS') + 2/24, 'YYYYMMDD') AS DOWN_DATE", "DOWN_TRAN_TIME", "DOWN_TRAN_TIME", "DOWN_DATE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Work Week", "' '", "' '", "' '", "PLAN_YEAR||PLAN_WEEK AS DOWN_WEEK", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Team in charge", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_1), '-') AS TEAM", "RES.RES_GRP_1", "RES.RES_GRP_1", "TEAM", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Part", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_2), '-') AS PART", "RES.RES_GRP_2", "RES.RES_GRP_2", "PART", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("EQP Type", "RES.RES_GRP_3 AS EQP_TYPE", "RES_GRP_3", "RES_GRP_3", "EQP_TYPE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Maker", "RES.RES_GRP_5 AS MAKER", "RES.RES_GRP_5", "RES.RES_GRP_5", "MAKER", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "RES.RES_GRP_6 AS MODEL", "RES.RES_GRP_6", "RES.RES_GRP_6", "MODEL", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment name", "RES.RES_ID AS RES", "RES.RES_ID", "RES.RES_ID", "RES", true);
        
             */
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>

        // LOT_ID 입력 시 설비 번호 및 END_TIME 가져오기
        private void GetLotInfo()
        {
            DataTable dtLotInfo = null;
            StringBuilder strSqlString = new StringBuilder();
            strSqlString.Append("SELECT END_TIME,END_RES_ID FROM MWIPLOTHIS@RPTTOMES" + "\n");
            strSqlString.Append(" WHERE LOT_ID = '" + txtLotId.Text + "' \n");
            strSqlString.Append("   AND TRAN_CODE = 'END'" + "\n");
            strSqlString.Append("   AND OLD_OPER " + cdvOper.SelectedValueToQueryString + "\n");
            dtLotInfo = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
            lotArry[0] = dtLotInfo.Rows[0][0].ToString(); // 해당 LOT END 시간
            lotArry[1] = dtLotInfo.Rows[0][1].ToString(); // 해당 LOT END 설비

        }

        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            strSqlString.Append("SELECT DOWN_EVENT_ID , UP_EVENT_ID, DOWN_NEW_STS_2, RES_ID, DOWN_TRAN_TIME, DOWN_TRAN_COMMENT" + "\n");
            strSqlString.Append("     , ROUND((DECODE(MAINT_START_TIME,' ',TO_DATE(UP_TRAN_TIME,'YYYYMMDDhh24miss'),TO_DATE(MAINT_START_TIME,'YYYYMMDDhh24miss'))  - TO_DATE(DOWN_TRAN_TIME,'YYYYMMDDhh24miss')) * 24 * 60) AS WAIT" + "\n");
            strSqlString.Append("     , MAINT_CODE, MAINT_CODE_DESC" + "\n");
            strSqlString.Append("     , ROUND((TO_DATE(UP_TRAN_TIME,'YYYYMMDDhh24miss') - TO_DATE(DOWN_TRAN_TIME,'YYYYMMDDhh24miss')) * 24 * 60) AS TOTAL_TIME" + "\n");
            strSqlString.Append("     , UP_TRAN_COMMENT, DOWN_TRAN_USER_ID, UP_TRAN_USER_ID" + "\n");
            strSqlString.Append("  FROM CRASRESDWH@RPTTOMES" + "\n");
            strSqlString.Append(" WHERE RES_ID  = '" + lotArry[1] + "' \n");
            strSqlString.Append("   AND DOWN_TRAN_TIME BETWEEN TO_CHAR(TO_DATE('" + lotArry[0] + "','YYYYMMDDhh24miss') - " + txtTime.Text + "/24,'YYYYMMDDhh24miss') AND TO_CHAR(TO_DATE('" + lotArry[0] + "','YYYYMMDDhh24miss') + " + txtTime.Text + "/24,'YYYYMMDDhh24miss')" + "\n");
            strSqlString.Append(" ORDER BY DOWN_TRAN_TIME" + "\n");

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
                
                //LOT 정보 가져오기
                GetLotInfo();
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
            cdvOper.sFactory = cdvFactory.txtValue;
        }
               
        #endregion

        private void txtLotID_TextChanged(object sender, EventArgs e)
        {
            // 입력한 LotID 를 대문자로 변경한다.

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                txtLotId.Text = txtLotId.Text.ToUpper();
                txtLotId.SelectionStart = txtLotId.Text.Length;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }
    }
}