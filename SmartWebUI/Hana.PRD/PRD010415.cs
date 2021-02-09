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
    public partial class PRD010415 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010415<br/>
        /// 클래스요약: 공정 시간(SAP Interface 용)<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2011-12-02<br/>
        /// 상세  설명: 공정 시간(SAP Interface 용-정비재C 요청)<br/>
        /// 변경  내용: <br/>
        /// 2011-12-07-임종우 : 전송 여부 검색 조건 추가 - 성공, 실패
        /// </summary>
        public PRD010415()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                InitializeComponent();
                SortInit();
                GridColumnInit();

                // 2012-02-10-정비재 :  프로그램을 실행할 때, Menu에 Setup되어 있는 메뉴명을 Form의 Title로 설정한다.
                lblTitle.Text = GlobalVariable.gsSelFuncName;

                cdvDate.Value = DateTime.Now;
                cdvToDate.Value = DateTime.Now;
                txtMatID.Text = "%";                
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

        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            /*
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }
            */
            
            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {            
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                spdData.RPT_ColumnInit();
                //if (cboS.Text.Equals("일자"))
                if (cboS.SelectedIndex == 0)
                {

                    spdData.RPT_AddBasicColumn("TRANS_SEQ", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("TRANS_TP", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("FACTORY", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("BASE_DATE", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("LOT_TYPE", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("SAP_ORDER_ID", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("ORDER_ID", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("FLOW", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("CANCEL_FLAG", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("OPER_SEQ_NUM", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("OPER", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("MAT_ID", 0, 11, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("BASE_MAT_ID", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("OPER_GUBUN", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("FROM_DATE", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("TO_DATE", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("FROM_TIME", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("TO_TIME", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("PROC_TIME", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("ZFINAL", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("RESULT_REC", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("WORK_STATUS", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("E_RETCD", 0, 22, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("E_ERRMSG", 0, 23, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                }
                //else if (cboS.Text.Equals("기간"))
                else if (cboS.SelectedIndex == 1)
                {
                    spdData.RPT_AddBasicColumn("TRANS_TP", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("ORDER_ID", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("MAT_ID", 0, 2, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("BASE_MAT_ID", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("PROC_TIME", 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            try
            {
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "B.MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = B.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "B.MAT_GRP_2", "B.MAT_GRP_2 AS FAMILY", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "B.MAT_GRP_3", "B.MAT_GRP_3 AS PACKAGE", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "B.MAT_GRP_4", "B.MAT_GRP_4 AS TYPE1", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "B.MAT_GRP_5", "B.MAT_GRP_5 AS TYPE2", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "B.MAT_GRP_6", "B.MAT_GRP_6 AS \"LD COUNT\"", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "B.MAT_GRP_7", "B.MAT_GRP_7 AS DENSITY", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "B.MAT_GRP_8", "B.MAT_GRP_8 AS GENERATION", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "B.MAT_CMF_10", "B.MAT_CMF_10 AS PIN_TYPE", true);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "B.MAT_ID", "B.MAT_ID AS PRODUCT", true);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
            finally
            {
                Cursor.Current = Cursor.Current;
            }
        }
        #endregion


        #region " Common Function "
        
        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns> 
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();
            string sTranTime = null;

            //sTranTime = cdvDate.SelectedValue() + cboTime.Text.Replace("시", "");
            sTranTime = cdvDate.SelectedValue() + cboTime.Text.Substring(0, 2);

            //if (cboS.Text.Equals("일자"))
            if (cboS.SelectedIndex == 0)
            {
                strSqlString.Append("SELECT * " + "\n");
                strSqlString.Append("  FROM MESMGR.ISAPPROCTIME@RPTTOMES " + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND TRANS_SEQ LIKE '" + sTranTime + "%'" + "\n");
                strSqlString.Append("   AND MAT_ID LIKE '" + txtMatID.Text + "'" + "\n");
            }
            //else if (cboS.Text.Equals("기간"))
            else if (cboS.SelectedIndex == 1)
            {
                strSqlString.Append("SELECT TRANS_TP, ORDER_ID, MAT_ID, BASE_MAT_ID " + "\n");
                strSqlString.Append("     , SUM(PROC_TIME) " + "\n");
                strSqlString.Append("  FROM MESMGR.ISAPPROCTIME@RPTTOMES " + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND MAT_ID LIKE '" + txtMatID.Text + "'" + "\n");
                //if (cboTime.Text.Equals("22시"))
                if (cboTime.SelectedIndex == 0)
                {
                    strSqlString.Append("   AND TO_DATE BETWEEN '" + cdvDate.SelectedValue() + "' AND '" + cdvToDate.SelectedValue() + "'" + "\n");
                    strSqlString.Append("   AND FROM_TIME = '220000'" + "\n");
                }
                //else if (cboTime.Text.Equals("06시"))
                else if (cboTime.SelectedIndex == 1)
                {
                    strSqlString.Append("   AND FROM_DATE BETWEEN '" + cdvDate.SelectedValue() + "' AND '" + cdvToDate.SelectedValue() + "'" + "\n");
                    strSqlString.Append("   AND FROM_TIME = '060000'" + "\n");
                }
                strSqlString.Append(" GROUP BY TRANS_TP, ORDER_ID, MAT_ID, BASE_MAT_ID" + "\n");
            }
            // 2011-12-07-임종우 : 전송 여부 검색 조건 추가 - 성공, 실패
            //if (cboStatus.Text == "성공")
            if (cboStatus.SelectedIndex == 1)
            {
                strSqlString.Append("   AND E_RETCD = 'S'" + "\n");
            }

            //if (cboStatus.Text == "실패")
            if (cboStatus.SelectedIndex == 2)
            {
                strSqlString.Append("   AND E_RETCD = 'E'" + "\n");
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();

        }

        #endregion



        #region " Controls Event "

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            //if (CheckField() == false) return;

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

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                spdData.ExportExcel();
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

        #endregion

        private void cboS_Changed(object sender, EventArgs e)
        {
            //if (cboS.Text.Equals("일자"))
            if (cboS.SelectedIndex == 0)
            {
                label9.Visible = false;
                cdvToDate.Visible = false;
            }
            //else if (cboS.Text.Equals("기간"))
            else if (cboS.SelectedIndex == 1)
            {
                label9.Visible = true;
                cdvToDate.Visible = true;
            }
        }

        
    }
}
