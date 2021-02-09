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
    public partial class PRD010425 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010425<br/>
        /// 클래스요약: 제품별 기본 수불<br/>
        /// 작  성  자: 하나마이크론 김미경<br/>
        /// 최초작성일: 2019-10-23<br/>
        /// 상세  설명: 제품별 기본 수불(정비재수석 요청)<br/>
        /// 변경  내용: <br/>  
        /// 2019-12-03-김미경 : DIFF_ADJUSTMENT 0이 아닌 값만 볼 수 있도록 CHECK BOX 생성 (정비재 그룹자 요청)
        /// </summary>
        public PRD010425()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                InitializeComponent();
                SortInit();
                GridColumnInit();  
                           
                txtMatID.Text = "%";
                cdvOper.Text = "ALL";               
                this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
                cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
                cdvOper.sFactory = cdvFactory.txtValue;
                udcDurationDate1.AutoBinding(DateTime.Now.AddDays(-1).ToString(), DateTime.Now.ToString());
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
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                spdData.RPT_ColumnInit();

                spdData.RPT_AddBasicColumn("FACTORY", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("PART_NO", 0, 1, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("OPER", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("BOH", 0, 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("SUBUL", 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("IN", 1, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("OUT", 1, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("LOSS", 1, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_MerageHeaderColumnSpan(0, 4, 3);    

                spdData.RPT_AddBasicColumn("CHG", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("EOH", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("DIFF", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 8, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 9, 2);                          
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
                // 사용안함
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
            string stable_wip, stable_fac;
            string s_BOH_time;
            string s_EOH_time;
            string s_Last_tran_time;
            string s_Mat_id;
            string s_Lot_type;

            # region "값 할당"

            //if (cboTime.Text == "22시")
            if (cboTime.SelectedIndex == 0)
            {
                stable_wip = "RSUMWIPMOV";
                stable_fac = "RSUMFACMOV";
                s_BOH_time = "TO_CHAR(TO_DATE('" + udcDurationDate1.HmFromDay + "') - 1, 'YYYYMMDD') || '" + cboTime.Text.Substring(0, 2) + "'";
                s_EOH_time = "'" + udcDurationDate1.HmToDay + "' || '" + cboTime.Text.Substring(0, 2) + "'";
                s_Last_tran_time = "TO_CHAR(TO_DATE('" + udcDurationDate1.HmFromDay + "') - 1, 'YYYYMMDD') || '220000' AND '" + udcDurationDate1.HmToDay + "' || '215959'";
            }
            else
            {
                stable_wip = "CSUMWIPMOV";
                stable_fac = "CSUMFACMOV";
                s_BOH_time = "'" + udcDurationDate1.HmFromDay + "' || '" + cboTime.Text.Substring(0, 2) + "'";
                s_EOH_time = "TO_CHAR(TO_DATE('" + udcDurationDate1.HmToDay + "') + 1, 'YYYYMMDD') || '" + cboTime.Text.Substring(0, 2) + "'";
                s_Last_tran_time = "'" + udcDurationDate1.HmFromDay + "' || '060000' AND TO_CHAR(TO_DATE('" + udcDurationDate1.HmToDay + "') + 1, 'YYYYMMDD') || '055959'";
            }

            if (txtMatID.Text.Trim() == "") s_Mat_id = "%";
            else s_Mat_id = txtMatID.Text.Trim();

            if (cbLotType.Text.Trim() == "ALL") s_Lot_type = "%";
            else  s_Lot_type = cbLotType.Text.Trim();  
        
            # endregion

            strSqlString.Append("SELECT *" + "\n");
            strSqlString.Append("FROM (SELECT A.FACTORY AS FACTORY," + "\n");
            strSqlString.Append("             A.MAT_ID AS MAT_ID," + "\n");
            strSqlString.Append("             A.OPER AS OPER," + "\n");
            strSqlString.Append("             NVL(B.BOH, 0) AS BOH," + "\n");
            strSqlString.Append("             A.OPER_IN AS SUBUL_IN," + "\n");
            strSqlString.Append("             A.END_QTY + NVL(F.SHIP_HMKA1_CUSTOMER_QTY, 0) + NVL(G.SHIP_HMKA1_HMKT1_QTY, 0) AS SUBUL_OUT," + "\n");
            strSqlString.Append("             A.LOSS + NVL(D.TERMINATE_NORMAL_QTY, 0) + NVL(E.TERMINATE_COMBINE_QTY, 0) AS SUBUL_LOSS," + "\n");
            strSqlString.Append("             A.CHG AS CHG," + "\n");
            strSqlString.Append("             NVL(C.EOH, 0) AS EOH," + "\n");
            strSqlString.Append("             CASE" + "\n");
            strSqlString.Append("               WHEN NVL(A.CHG, 0) < 0 THEN (NVL(B.BOH, 0) + NVL(A.OPER_IN, 0)) - (NVL(A.END_QTY, 0) + NVL(F.SHIP_HMKA1_CUSTOMER_QTY, 0) + NVL(G.SHIP_HMKA1_HMKT1_QTY, 0) + NVL(A.LOSS, 0) + NVL(D.TERMINATE_NORMAL_QTY, 0) + NVL(E.TERMINATE_COMBINE_QTY, 0) + ABS(NVL(A.CHG, 0)) + NVL(C.EOH, 0))" + "\n");
            strSqlString.Append("               WHEN NVL(A.CHG, 0) >= 0 THEN (NVL(B.BOH, 0) + NVL(A.OPER_IN, 0) + NVL(A.CHG, 0)) - (NVL(A.END_QTY, 0) + NVL(F.SHIP_HMKA1_CUSTOMER_QTY, 0) + NVL(G.SHIP_HMKA1_HMKT1_QTY, 0) + NVL(A.LOSS, 0) + NVL(D.TERMINATE_NORMAL_QTY, 0) + NVL(E.TERMINATE_COMBINE_QTY, 0) + NVL(C.EOH, 0))" + "\n");
            strSqlString.Append("             END AS DIFF" + "\n");
            strSqlString.Append("       FROM (" + "\n");
            strSqlString.Append("            SELECT FACTORY AS FACTORY," + "\n");
            strSqlString.Append("                   MAT_ID AS MAT_ID," + "\n");
            strSqlString.Append("                  OPER AS OPER," + "\n");
            strSqlString.Append("                  SUM(S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1) AS OPER_IN," + "\n");
            strSqlString.Append("                  SUM(S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1) AS END_QTY," + "\n");
            strSqlString.Append("                  ABS(SUM(S1_LOSS_QTY_1 + S2_LOSS_QTY_1 + S3_LOSS_QTY_1)) AS LOSS," + "\n");
            strSqlString.Append("                  SUM(S1_BONUS_QTY_1 + S2_BONUS_QTY_1 + S3_BONUS_QTY_1) AS BONUS," + "\n");
            strSqlString.Append("                  SUM(S1_CHG_QTY_1 + S2_CHG_QTY_1 + S3_CHG_QTY_1) AS CHG" + "\n");
            strSqlString.Append("             FROM " + stable_wip + "\n");
            strSqlString.Append("            WHERE WORK_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.Append("              AND MAT_ID LIKE '" + s_Mat_id + "'" + "\n");
            strSqlString.Append("              AND CM_KEY_3 LIKE '" + s_Lot_type + "'" + "\n");
            strSqlString.Append("              AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("              AND OPER NOT IN (' '," + "\n");
            strSqlString.Append("                               'A000N'," + "\n");
            strSqlString.Append("                               'T000N'," + "\n");
            strSqlString.Append("                               'F000N'," + "\n");
            strSqlString.Append("                               'E000N'," + "\n");
            strSqlString.Append("                               'S000N'," + "\n");
            strSqlString.Append("                               'B000N')" + "\n");
            strSqlString.Append("        GROUP BY FACTORY," + "\n");
            strSqlString.Append("                 MAT_ID," + "\n");
            strSqlString.Append("                 OPER" + "\n");
            strSqlString.Append("           ) A," + "\n");
            strSqlString.Append("           (" + "\n");
            strSqlString.Append("           SELECT FACTORY AS FACTORY," + "\n");
            strSqlString.Append("                  MAT_ID AS MAT_ID," + "\n");
            strSqlString.Append("                  OPER AS OPER," + "\n");
            strSqlString.Append("                  SUM(QTY_1) AS BOH" + "\n");
            strSqlString.Append("            FROM RWIPLOTSTS_BOH" + "\n");
            strSqlString.Append("           WHERE CUTOFF_DT = " + s_BOH_time + "\n");
            strSqlString.Append("             AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("             AND LOT_CMF_5 LIKE '" + s_Lot_type +"'" + "\n");
            strSqlString.Append("        GROUP BY FACTORY," + "\n");
            strSqlString.Append("                 MAT_ID," + "\n");
            strSqlString.Append("                 OPER" + "\n");
            strSqlString.Append("           ) B," + "\n");
            strSqlString.Append("           (" + "\n");
            strSqlString.Append("           SELECT FACTORY AS FACTORY," + "\n");
            strSqlString.Append("                  MAT_ID AS MAT_ID," + "\n");
            strSqlString.Append("                  OPER AS OPER," + "\n");
            strSqlString.Append("                  SUM(QTY_1) AS EOH" + "\n");
            strSqlString.Append("            FROM RWIPLOTSTS_BOH" + "\n");
            strSqlString.Append("           WHERE CUTOFF_DT = " + s_EOH_time + "\n");
            strSqlString.Append("             AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("             AND LOT_CMF_5 LIKE '"+ s_Lot_type + "'" + "\n");
            strSqlString.Append("        GROUP BY FACTORY," + "\n");
            strSqlString.Append("                 MAT_ID," + "\n");
            strSqlString.Append("                 OPER" + "\n");
            strSqlString.Append("           ) C," + "\n");
            strSqlString.Append("           (" + "\n");
            strSqlString.Append("           SELECT FACTORY AS FACTORY," + "\n");
            strSqlString.Append("                  MAT_ID AS MAT_ID," + "\n");
            strSqlString.Append("                  OPER AS OPER," + "\n");
            strSqlString.Append("                  SUM(QTY_1) AS TERMINATE_NORMAL_QTY" + "\n");
            strSqlString.Append("             FROM RWIPLOTSTS" + "\n");
            strSqlString.Append("            WHERE LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("              AND LAST_TRAN_CODE = 'TERMINATE'" + "\n");
            strSqlString.Append("              AND LOT_DEL_FLAG = 'Y'" + "\n");
            strSqlString.Append("              AND RWK_FLAG = ' '" + "\n");
            strSqlString.Append("              AND REP_FLAG = ' '" + "\n");
            strSqlString.Append("              AND MAT_ID LIKE '" + s_Mat_id + "'" + "\n");
            strSqlString.Append("              AND LOT_CMF_5 LIKE '" + s_Lot_type + "'" + "\n");
            strSqlString.Append("              AND LAST_TRAN_TIME BETWEEN " + s_Last_tran_time + "\n");
            strSqlString.Append("         GROUP BY FACTORY," + "\n");
            strSqlString.Append("                  MAT_ID," + "\n");
            strSqlString.Append("                  OPER" + "\n");
            strSqlString.Append("          ) D," + "\n");
            strSqlString.Append("          (" + "\n");
            strSqlString.Append("          SELECT FACTORY AS FACTORY," + "\n");
            strSqlString.Append("                 MAT_ID AS MAT_ID," + "\n");
            strSqlString.Append("                 OPER AS OPER," + "\n");
            strSqlString.Append("                 SUM(FROM_TO_QTY_1) AS TERMINATE_COMBINE_QTY" + "\n");
            strSqlString.Append("            FROM RWIPLOTCMB" + "\n");
            strSqlString.Append("           WHERE FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "'," + "\n");
            strSqlString.Append("                             '" + GlobalVariable.gsTestDefaultFactory + "'," + "\n");
            strSqlString.Append("                             'HMKE1'," + "\n");
            strSqlString.Append("                             'HMKS1')" + "\n");
            strSqlString.Append("             AND TRAN_TIME BETWEEN " + s_Last_tran_time + "\n");
            strSqlString.Append("             AND MAT_ID LIKE '" + s_Mat_id + "'" + "\n");
            strSqlString.Append("             AND FROM_TO_FLAG = 'F'" + "\n");
            strSqlString.Append("        GROUP BY FACTORY," + "\n");
            strSqlString.Append("                 MAT_ID," + "\n");
            strSqlString.Append("                 OPER" + "\n");
            strSqlString.Append("          ) E," + "\n");
            strSqlString.Append("          (" + "\n");
            strSqlString.Append("           SELECT '" + GlobalVariable.gsAssyDefaultFactory + "' AS FACTORY," + "\n");
            strSqlString.Append("                  MAT_ID AS MAT_ID," + "\n");
            strSqlString.Append("                  'AZ010' AS OPER," + "\n");
            strSqlString.Append("                  SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1) AS SHIP_HMKA1_CUSTOMER_QTY" + "\n");
            strSqlString.Append("             FROM " + stable_fac + "\n");
            strSqlString.Append("            WHERE FACTORY = 'CUSTOMER'" + "\n");
            strSqlString.Append("              AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("              AND OPER = ' '" + "\n");
            strSqlString.Append("              AND WORK_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.Append("              AND MAT_ID LIKE '" + s_Mat_id + "'" + "\n");
            strSqlString.Append("              AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("              AND CM_KEY_3 LIKE '" + s_Lot_type + "'" + "\n");
            strSqlString.Append("         GROUP BY FACTORY," + "\n");
            strSqlString.Append("                  MAT_ID," + "\n");
            strSqlString.Append("                  OPER" + "\n");
            strSqlString.Append("          ) F," + "\n");
            strSqlString.Append("          (" + "\n");
            strSqlString.Append("           SELECT '" + GlobalVariable.gsAssyDefaultFactory + "' AS FACTORY," + "\n");
            strSqlString.Append("                  MAT_ID AS MAT_ID," + "\n");
            strSqlString.Append("                  'AZ010' AS OPER," + "\n");
            strSqlString.Append("                  SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1) AS SHIP_HMKA1_HMKT1_QTY" + "\n");
            strSqlString.Append("             FROM " + stable_fac + "\n");
            strSqlString.Append("            WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("              AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("              AND OPER = 'T000N'" + "\n");
            strSqlString.Append("              AND WORK_DATE BETWEEN '" + udcDurationDate1.HmFromDay + "' AND '" + udcDurationDate1.HmToDay + "'" + "\n");
            strSqlString.Append("              AND MAT_ID LIKE '" + s_Mat_id + "'" + "\n");
            strSqlString.Append("              AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("              AND CM_KEY_3 LIKE '" + s_Lot_type + "'" + "\n");
            strSqlString.Append("         GROUP BY FACTORY," + "\n");
            strSqlString.Append("                  MAT_ID," + "\n");
            strSqlString.Append("                  OPER" + "\n");
            strSqlString.Append("          ) G" + "\n");
            strSqlString.Append("       WHERE A.FACTORY = B.FACTORY(+)" + "\n");
            strSqlString.Append("         AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("         AND A.OPER = B.OPER(+)" + "\n");
            strSqlString.Append("         AND A.FACTORY = C.FACTORY(+)" + "\n");
            strSqlString.Append("         AND A.MAT_ID = C.MAT_ID(+)" + "\n");
            strSqlString.Append("         AND A.OPER = C.OPER(+)" + "\n");
            strSqlString.Append("         AND A.FACTORY = D.FACTORY(+)" + "\n");
            strSqlString.Append("         AND A.MAT_ID = D.MAT_ID(+)" + "\n");
            strSqlString.Append("         AND A.OPER = D.OPER(+)" + "\n");
            strSqlString.Append("         AND A.FACTORY = E.FACTORY(+)" + "\n");
            strSqlString.Append("         AND A.MAT_ID = E.MAT_ID(+)" + "\n");
            strSqlString.Append("         AND A.OPER = E.OPER(+)" + "\n");
            strSqlString.Append("         AND A.FACTORY = F.FACTORY(+)" + "\n");
            strSqlString.Append("         AND A.MAT_ID = F.MAT_ID(+)" + "\n");
            strSqlString.Append("         AND A.OPER = F.OPER(+)" + "\n");
            strSqlString.Append("         AND A.FACTORY = G.FACTORY(+)" + "\n");
            strSqlString.Append("         AND A.MAT_ID = G.MAT_ID(+)" + "\n");
            strSqlString.Append("         AND A.OPER = G.OPER(+)" + "\n");

            if (cdvOper.Text.Trim() != "ALL")
            {
                strSqlString.Append("         AND A.OPER " + cdvOper.SelectedValueToQueryString + "\n");
            }         

            strSqlString.Append(" ORDER BY DECODE(A.FACTORY, '" + GlobalVariable.gsAssyDefaultFactory + "', 1, '" + GlobalVariable.gsTestDefaultFactory + "', 2, 'HMKE1', 3, 'HMKS1', 4, 'FGS', 5, 6) ASC," + "\n");
            strSqlString.Append("       A.MAT_ID ASC," + "\n");
            strSqlString.Append("       A.OPER ASC)" + "\n");

            if (chkDiff.Checked == true)
            {
                strSqlString.Append(" WHERE DIFF <> 0" + "\n");
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

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            cdvOper.Text = "ALL";
            this.SetFactory(cdvFactory.txtValue);
            cdvOper.sFactory = cdvFactory.txtValue;
        }

        #endregion
                    
    }
}
