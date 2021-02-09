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
    public partial class PRD010418 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010418<br/>
        /// 클래스요약: 제조오더별 생산실적 조회<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2011-12-27<br/>
        /// 상세  설명: 제조오더별 생산실적 조회(정비재C 요청)<br/>
        /// 변경  내용: <br/>  
        /// 2012-01-06-임종우 : 제조오더별 해당 LOT 상세 이력 POPUP 창 추가 (정비재C 요청)
        /// 2012-03-02-김민우 3월1일 조회이면 2월29일 22시부터로 변경 (정비재C 요청)
        /// 2012-03-20-김민우 CANCEL_FLAG 추가 및 CANCEL_FLAG = 'Y' 인 것은 붉은 색으로 표시(정비재C 요청)
        /// </summary>
        public PRD010418()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                InitializeComponent();
                SortInit();
                GridColumnInit();

                // 2012-02-10-정비재 :  프로그램을 실행할 때, Menu에 Setup되어 있는 메뉴명을 Form의 Title로 설정한다.
                lblTitle.Text = GlobalVariable.gsSelFuncName;

                cdvFromToDate.AutoBinding();
                cdvFromToDate.DaySelector.Visible = false;
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
            //if (cdvFactory.Text.TrimEnd() == "")
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
            //    return false;
            //}
            
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
                spdData.RPT_AddBasicColumn("SAP_ORDER_ID", 0, 0, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("ORDER_ID", 0, 1, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("BASE_MAT_ID", 0, 2, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MAT_ID", 0, 3, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("OPER", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("OPER_SEQ_NUM", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("CANCEL_FLAG", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("ORDER_CREATE_QTY", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("IN_QTY", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("OUT_QTY", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("LOSS_QTY", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("CV_QTY", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
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
           
            strSqlString.Append("SELECT SAP_ORDER_ID " + "\n");
            strSqlString.Append("     , ORDER_ID " + "\n");
            strSqlString.Append("     , BASE_MAT_ID " + "\n");
            strSqlString.Append("     , MAT_ID " + "\n");
            strSqlString.Append("     , OPER " + "\n");
            strSqlString.Append("     , OPER_SEQ_NUM " + "\n");
            strSqlString.Append("     , CANCEL_FLAG " + "\n");
            strSqlString.Append("     , SUM(CASE OPER_GUBUN WHEN '1' THEN SUBUL_OUT " + "\n");
            strSqlString.Append("                           ELSE 0 " + "\n");
            strSqlString.Append("           END) AS ORDER_CREATE_QTY " + "\n");
            strSqlString.Append("     , SUM(SUBUL_OUT + SUBUL_LOSS + SUBUL_CV) AS IN_QTY " + "\n");
            strSqlString.Append("     , SUM(SUBUL_OUT) AS OUT_QTY " + "\n");
            strSqlString.Append("     , SUM(SUBUL_LOSS) AS LOSS_QTY " + "\n");
            strSqlString.Append("     , SUM(SUBUL_CV) AS CV_QTY " + "\n");
            strSqlString.Append("  FROM ISAPSUBUL@RPTTOMES " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            //strSqlString.Append("   AND TRANS_TP = 'I' " + "\n");
            strSqlString.Append("   AND TRANS_TP IN ('I','D') " + "\n");
            strSqlString.Append("   AND SAP_ORDER_ID LIKE '" + txtSapOrderId.Text + "'" + "\n");
            strSqlString.Append("   AND ORDER_ID LIKE '" + txtOrderId.Text + "'" + "\n");
            strSqlString.Append("   AND BASE_MAT_ID LIKE '" + txtBaseMatId.Text + "'" + "\n");
            strSqlString.Append("   AND MAT_ID LIKE '" + txtMatID.Text + "'" + "\n");
            //2012-03-02-김민우 3월1일 조회이면 2월29일 22시부터로 변경
            //strSqlString.Append("   AND FROM_DATE >= '" + cdvFromToDate.HmFromDay.ToString() + "'" + "\n");

            //if (cboTime.Text.Equals("22시"))
            if (cboTime.SelectedIndex == 0)
            {
                strSqlString.Append("   AND FROM_DATE >= '" + DateTime.Parse(cdvFromToDate.FromDate.Text).AddDays(-1).ToString("yyyyMMdd") + "'" + "\n");
            }
            else
            {
                strSqlString.Append("   AND FROM_DATE >= '" + cdvFromToDate.HmFromDay.ToString() + "'" + "\n");
            }
            strSqlString.Append("   AND TO_DATE <= '" + cdvFromToDate.HmToDay.ToString() + "'" + "\n");
            //strSqlString.Append("   AND FROM_TIME LIKE '" + cboTime.Text.Replace("시", "") + "%'" + "\n");
            strSqlString.Append("   AND FROM_TIME LIKE '" + cboTime.Text.Substring(0, 2) + "%'" + "\n");
            strSqlString.Append("   AND (SUBUL_OUT + SUBUL_LOSS + SUBUL_CV) <> 0 " + "\n");
            strSqlString.Append(" GROUP BY SAP_ORDER_ID, ORDER_ID, BASE_MAT_ID, MAT_ID, OPER, OPER_SEQ_NUM, CANCEL_FLAG " + "\n");
            strSqlString.Append(" ORDER BY SAP_ORDER_ID ASC, ORDER_ID ASC, BASE_MAT_ID ASC, MAT_ID ASC, OPER_SEQ_NUM ASC, DECODE(CANCEL_FLAG, 'Y', 0, 1) ASC " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();

        }

        #endregion

        #region popup 창 쿼리
        private string MakeSqlDetail(string sOrderId, string sOper)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT TRAN_TIME AS TRAN_TIME" + "\n");
            strSqlString.Append("     , ORDER_ID AS ORDER_ID" + "\n");
            strSqlString.Append("     , MAT_ID AS MAT_ID" + "\n");
            strSqlString.Append("     , LOT_ID AS LOT_ID" + "\n");
            strSqlString.Append("     , HIST_SEQ AS HIST_SEQ" + "\n");
            strSqlString.Append("     , TRAN_CODE AS TRAN_CODE" + "\n");
            strSqlString.Append("     , LOT_TYPE AS LOT_TYPE" + "\n");
            strSqlString.Append("     , FLOW AS FLOW" + "\n");
            strSqlString.Append("     , OPER_SEQ_NUM AS OPER_SEQ_NUM" + "\n");
            strSqlString.Append("     , OPER AS OPER" + "\n");
            strSqlString.Append("     , HIST_DEL_FLAG AS HIST_DEL_FLAG" + "\n");
            strSqlString.Append("     , QTY AS QTY" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT A.TRAN_TIME AS TRAN_TIME" + "\n");
            strSqlString.Append("             , A.ORDER_ID AS ORDER_ID" + "\n");
            strSqlString.Append("             , A.MAT_ID AS MAT_ID" + "\n");
            strSqlString.Append("             , A.LOT_ID AS LOT_ID" + "\n");
            strSqlString.Append("             , A.HIST_SEQ AS HIST_SEQ" + "\n");
            strSqlString.Append("             , A.TRAN_CODE AS TRAN_CODE" + "\n");
            strSqlString.Append("             , A.LOT_CMF_5 AS LOT_TYPE" + "\n");
            strSqlString.Append("             , A.OLD_FLOW AS FLOW" + "\n");
            strSqlString.Append("             , B.SEQ_NUM AS OPER_SEQ_NUM" + "\n");
            strSqlString.Append("             , A.OLD_OPER AS OPER" + "\n");
            strSqlString.Append("             , A.HIST_DEL_FLAG AS HIST_DEL_FLAG" + "\n");
            strSqlString.Append("             , CASE A.TRAN_CODE WHEN 'END'   THEN A.QTY_1" + "\n");
            strSqlString.Append("                                WHEN 'LOSS'  THEN A.OLD_QTY_1 - A.QTY_1" + "\n");
            strSqlString.Append("                                WHEN 'ADAPT' THEN CASE WHEN A.OLD_QTY_1 >  A.QTY_1 THEN A.OLD_QTY_1 - A.QTY_1" + "\n");
            strSqlString.Append("                                                       WHEN A.OLD_QTY_1 <= A.QTY_1 THEN A.QTY_1 - A.OLD_QTY_1" + "\n");
            strSqlString.Append("                                                       ELSE 0" + "\n");
            strSqlString.Append("                                                  END" + "\n");
            strSqlString.Append("                                ELSE 0" + "\n");
            strSqlString.Append("               END AS QTY" + "\n");
            strSqlString.Append("          FROM RWIPLOTHIS A" + "\n");
            strSqlString.Append("             , MESMGR.MWIPFLWOPR@RPTTOMES B" + "\n");
            strSqlString.Append("         WHERE A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("           AND A.FLOW = B.FLOW" + "\n");
            strSqlString.Append("           AND A.OLD_OPER = B.OPER" + "\n");
            strSqlString.Append("           AND A.TRAN_CODE IN ('END', 'LOSS', 'ADAPT')" + "\n");
            strSqlString.Append("           AND A.OLD_OPER = '" + sOper + "'" + "\n");
            strSqlString.Append("           AND A.LOT_ID IN (" + "\n");
            strSqlString.Append("                            SELECT LOT_ID" + "\n");
            strSqlString.Append("                              FROM RWIPLOTSTS " + "\n");
            strSqlString.Append("                             WHERE LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                               AND LOT_CMF_5 LIKE 'P%'" + "\n");
            strSqlString.Append("                               AND ORDER_ID = '" + sOrderId + "'" + "\n");
            strSqlString.Append("                            )" + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" WHERE QTY > 0" + "\n");
            strSqlString.Append(" ORDER BY LOT_ID ASC, HIST_SEQ ASC" + "\n");

            //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            //{
            //    System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            //}

            return strSqlString.ToString();
        }
        #endregion

        #region " Controls Event "

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

                for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    if (spdData.ActiveSheet.Cells[i, 6].Value.Equals("Y"))
                    {
                        spdData.ActiveSheet.Rows[i].BackColor = Color.Red;
                    }
                }
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

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //string stType = spdData.ActiveSheet.Cells[e.Row, e.Column].Column.Label.ToString();

            string sOrderId = null;
            string sOper = null;

            // Order ID 이면서 Null 값이 아닌경우 클릭 시 팝업 창 띄움.
            if (e.Column == 1 && spdData.ActiveSheet.Cells[e.Row, e.Column].Text != " ")
            {
                // 로딩 창 시작
                LoadingPopUp.LoadIngPopUpShow(this);

                sOrderId = spdData.ActiveSheet.Cells[e.Row, 1].Value.ToString();
                sOper = spdData.ActiveSheet.Cells[e.Row, 4].Value.ToString();

                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetail(sOrderId, sOper));
                
                // 로딩 창 종료
                LoadingPopUp.LoadingPopUpHidden();

                if (dt != null && dt.Rows.Count > 0)
                {
                    System.Windows.Forms.Form frm = new PRD010418_P1("", dt);
                                       
                    frm.ShowDialog();
                }                
            }
            else
            {
                return;
            }
        }

        
    }
}
