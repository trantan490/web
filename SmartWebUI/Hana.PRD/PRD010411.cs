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
    public partial class PRD010411 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        

        #region " Program Initial "

        public PRD010411()
        {
            /****************************************************
             * comment : Program의 초기설정을 한다.
             * 
             * created by : bee-jae jung(2011-01-17-월요일)
             * 
             * modified by : bee-jae jung(2011-01-17-월요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                InitializeComponent();
                fnSSInitial();

                // 2009-02-20-정비재 : Factory Control을 사용하지 않기 위하여, 값을 입력한다.
                cdvFactory.Text = "HMKA1, HMKT1";
                cdvFactory.Visible = false;
                cdvFromDate.Value = DateTime.Now;
                cdvToDate.Value = DateTime.Now;
                txtMatID.Text = "%";
                txtLotType.Text = "P%";
                rbBaseTime22.Checked = true;        // 2011-01-17-정비재 : 초기값은 22시로 한다.
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

        private void fnSSInitial()
        {
            /****************************************************
             * comment : Spread의 초기값을 설정한다.
             * 
             * created by : bee-jae jung(2011-01-17-월요일)
             * 
             * modified by : bee-jae jung(2011-01-17-월요일)
             ****************************************************/
            int iIdx = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                SS01.RPT_ColumnInit();
                SS01.RPT_AddBasicColumn("MAT_ID", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 160);
                SS01.RPT_AddBasicColumn("OPER", 0, iIdx + 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
                SS01.RPT_AddBasicColumn("BOH", 0, iIdx + 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                SS01.RPT_AddBasicColumn("OPER_IN", 0, iIdx + 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                SS01.RPT_AddBasicColumn("START", 0, iIdx + 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                SS01.RPT_AddBasicColumn("END", 0, iIdx + 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                SS01.RPT_AddBasicColumn("HOLD", 0, iIdx + 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                SS01.RPT_AddBasicColumn("LOSS", 0, iIdx + 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                SS01.RPT_AddBasicColumn("BONUS", 0, iIdx + 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                SS01.RPT_AddBasicColumn("EOH", 0, iIdx + 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
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

        #endregion


        #region " Common Function "

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        /// 
        private Boolean fnDataFind()
        {
            /****************************************************
             * comment : 검색조건에 맞는 데이터를 조회한다.
             *                                                 
             * created by : bee-jae jung(2011-01-17-월요일)
             *                                                 
             * modified by : bee-jae jung(2011-01-17-월요일)
             ****************************************************/
            DataTable DT = null;
            String QRY = "", sBoh_Date = "", sEoh_Date = "";
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                LoadingPopUp.LoadIngPopUpShow(this);

                // 2010-07-20-정비재 : Sheet / Listview를 초기화 한다.
                CmnInitFunction.ClearList(SS01, true);

                // 2011-01-17-정비재 : BOH, EOH 일자를 조회한다.
                if (rbBaseTime06.Checked == true)
                {
                    // 2011-01-17-정비재 : 06시 기준의 일자
                    sBoh_Date = cdvToDate.SelectedValue() + "06";
                    sEoh_Date = DateTime.Parse(cdvToDate.Value.ToString("yyyy-MM-dd")).AddDays(+1).ToString("yyyyMMdd") + "06";
                }
                else if (rbBaseTime22.Checked == true)
                {
                    // 2011-01-17-정비재 : 22시 기준의 일자
                    sBoh_Date = DateTime.Parse(cdvFromDate.Value.ToString("yyyy-MM-dd")).AddDays(-1).ToString("yyyyMMdd") + "22";
                    sEoh_Date = cdvToDate.SelectedValue() + "22";
                }
                
                QRY = "SELECT A.MAT_ID AS MAT_ID"
                    + "     , A.OPER AS OPER"
                    + "     , SUM(A.BOH_QTY) AS BOH_QTY"
                    + "     , SUM(A.OPER_IN_QTY) AS OPER_IN_QTY"
                    + "     , SUM(A.START_QTY) AS START_QTY"
                    + "     , SUM(A.END_QTY) AS END_QTY"
                    + "     , SUM(A.HOLD_QTY) AS HOLD_QTY"
                    + "     , SUM(A.LOSS_QTY) AS LOSS_QTY"
                    + "     , SUM(A.BONUS_QTY) AS BONUS_QTY"
                    + "     , SUM(A.EOH_QTY) AS EOH_QTY"
                    + "  FROM (SELECT MAT_ID AS MAT_ID"
                    + "             , OPER AS OPER"
                    + "             , 0 AS BOH_QTY"
                    + "             , SUM(S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1) AS OPER_IN_QTY"
                    + "             , SUM(S1_START_QTY_1 + S2_START_QTY_1 + S3_START_QTY_1) AS START_QTY"
                    + "             , SUM(S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1) AS END_QTY"
                    + "             , SUM(S1_TO_HOLD_QTY_1 + S2_TO_HOLD_QTY_1 + S3_TO_HOLD_QTY_1) AS HOLD_QTY"
                    + "             , SUM(S1_LOSS_QTY_1 + S2_LOSS_QTY_1 + S3_LOSS_QTY_1) AS LOSS_QTY"
                    + "             , SUM(S1_BONUS_QTY_1 + S2_BONUS_QTY_1 + S3_BONUS_QTY_1) AS BONUS_QTY"
                    + "             , 0 AS EOH_QTY";
                if (rbBaseTime06.Checked == true)
                {
                    QRY += "          FROM CSUMWIPMOV";     // 2011-01-17-정비재 : 06시 기준
                }
                else if (rbBaseTime22.Checked == true)
                {
                    QRY += "          FROM RSUMWIPMOV";     // 2011-01-17-정비재 : 22시 기준
                }
                QRY += "         WHERE FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "')"
                    + "           AND WORK_DATE >= '" + cdvFromDate.SelectedValue() + "'"
                    + "           AND WORK_DATE <= '" + cdvToDate.SelectedValue() + "'"
                    + "           AND LOT_TYPE = 'W'"
                    + "           AND MAT_ID LIKE '" + txtMatID.Text + "%'" 
                    + "           AND CM_KEY_3 LIKE '" + txtLotType.Text + "%'" 
                    + "         GROUP BY MAT_ID, OPER"
                    + "        UNION ALL"
                    + "        SELECT MAT_ID AS MAT_ID"
                    + "             , OPER AS OPER"
                    + "             , SUM(QTY_1) AS BOH_QTY"
                    + "             , 0 AS OPER_IN_QTY"
                    + "             , 0 AS START_QTY"
                    + "             , 0 AS END_QTY"
                    + "             , 0 AS HOLD_QTY"
                    + "             , 0 AS LOSS_QTY"
                    + "             , 0 AS BONUS_QTY"
                    + "             , 0 AS EOH_QTY"
                    + "          FROM RWIPLOTSTS_BOH"
                    + "         WHERE FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "')"
                    + "           AND LOT_TYPE = 'W'"
                    + "           AND MAT_ID LIKE '" + txtMatID.Text + "%'" 
                    + "           AND LOT_CMF_5 LIKE '" + txtLotType.Text + "%'"
                    + "           AND CUTOFF_DT = '" + sBoh_Date + "'"
                    + "         GROUP BY MAT_ID, OPER"
                    + "        UNION ALL"
                    + "        SELECT MAT_ID AS MAT_ID"
                    + "             , OPER AS OPER"
                    + "             , 0 AS BOH_QTY"
                    + "             , 0 AS OPER_IN_QTY"
                    + "             , 0 AS START_QTY"
                    + "             , 0 AS END_QTY"
                    + "             , 0 AS HOLD_QTY"
                    + "             , 0 AS LOSS_QTY"
                    + "             , 0 AS BONUS_QTY"
                    + "             , SUM(QTY_1) AS EOH_QTY"
                    + "          FROM RWIPLOTSTS_BOH"
                    + "         WHERE FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "')"
                    + "           AND LOT_TYPE = 'W'"
                    + "           AND MAT_ID LIKE '" + txtMatID.Text + "%'"
                    + "           AND LOT_CMF_5 LIKE '" + txtLotType.Text + "%'"
                    + "           AND CUTOFF_DT = '" + sEoh_Date + "'"
                    + "         GROUP BY MAT_ID, OPER) A"
                    + " GROUP BY A.MAT_ID, A.OPER"
                    + " ORDER BY A.MAT_ID ASC, A.OPER ASC";

                if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                {
                    System.Windows.Forms.Clipboard.SetText(QRY.Replace((char)Keys.Tab, ' '));
                }

                DT = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", QRY.Replace((char)Keys.Tab, ' '));

                if (DT.Rows.Count == 0)
                {
                    DT.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return false;
                }

                SS01.DataSource = DT;
                SS01.RPT_AutoFit(false);

                return true;
            }
            catch (Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion



        #region " Controls Event "

        private void btnView_Click(object sender, EventArgs e)
        {
            /****************************************************
             * comment : 조회버튼을 선택하면
             *                                                 
             * created by : bee-jae jung(2011-01-17-월요일)
             *                                                 
             * modified by : bee-jae jung(2011-01-17-월요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                fnDataFind();    
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

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            /****************************************************
             * comment : excel로 내보내기를 선택하면
             *                                                 
             * created by : bee-jae jung(2011-01-17-월요일)
             *                                                 
             * modified by : bee-jae jung(2011-01-17-월요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                SS01.ExportExcel();
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

        private void txtMatID_TextChanged(object sender, EventArgs e)
        {
            /****************************************************
             * comment : 제품코드에서 변경이 발생되면
             *                                                 
             * created by : bee-jae jung(2011-01-17-월요일)
             *                                                 
             * modified by : bee-jae jung(2011-01-17-월요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                txtMatID.Text = txtMatID.Text.ToUpper();
                if (txtMatID.Text.IndexOf("%") > 0)
                {
                    txtMatID.SelectionStart = txtMatID.Text.Length - 1;
                }
                else
                {
                    txtMatID.SelectionStart = txtMatID.Text.Length;
                }
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

        
    }
}
