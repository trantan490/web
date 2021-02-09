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

namespace Hana.PQC
{
    /// <summary>
    /// 클  래  스: PQC031002<br/>
    /// 클래스요약: 오븐온도 이력조회<br/>
    /// 작  성  자: 미라콤 양형석<br/>
    /// 최초작성일: 2009-01-09<br/>
    /// 상세  설명: 오븐온도 이력조회<br/>
    /// 변경  내용: <br/>
    /// 2020-03-27-김미경 : Ass'y -> 모든 factory 조회 가능하도록 확대 
    ///                   : 삭제된 설비는 제외 (이창훈 D)
    /// </summary>
    public partial class PQC031002 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        #region " Form Initial "
        
        int SS_ActiveIndex = 0;

        public PQC031002()
        {
            /************************************************
             * comment : Form이 처음 Load될 때 발생된다.
             * 
             * created by : 
             * 
             * modified by : bee-jae jung(2010-08-10-화요일)
             ************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                InitializeComponent();
                cdvFromToDate.AutoBinding(DateTime.Now.ToString().Substring(0, 10), DateTime.Now.ToString().Substring(0, 10));
                GridColumnInit(SS);
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

        private void GridColumnInit(Miracom.SmartWeb.UI.Controls.udcFarPoint SS)
        {
            /************************************************
             * comment : SS의 Column의 Header를 설정한다.
             * 
             * created by : 
             * 
             * modified by : bee-jae jung(2010-08-10-화요일)
             ************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                SS.RPT_ColumnInit();
                SS.RPT_AddBasicColumn("RES_ID", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                SS.RPT_AddBasicColumn("OPER", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                SS.RPT_AddBasicColumn("START_TIME", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                SS.RPT_AddBasicColumn("END_TIME", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                SS.RPT_AddBasicColumn("LOT_ID", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                SS.RPT_AddBasicColumn("↓", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 20);
                SS.RPT_AddBasicColumn("MAT_ID", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 120);
                SS.RPT_AddBasicColumn("PATTERN_NO", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                SS.RPT_AddBasicColumn("START_TIME", 0, 8, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                // 특정 Columns의 색상을 지정한다.
                SS.ActiveSheet.Columns[4].BackColor = Color.AliceBlue;
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


        #region " Common Function "

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

        private Boolean fnDataFind(char cStep)
        {
            /****************************************************
             * comment : 검색조건에 맞는 데이터를 검색한다.
             * 
             * created by : bee-jae jung(2010-08-10-화요일)
             * 
             * modified by : bee-jae jung(2010-08-10-화요일)
             ****************************************************/
            FarPoint.Win.Spread.CellType.ButtonCellType ButtonCellType = new FarPoint.Win.Spread.CellType.ButtonCellType();
            String QRY = "";
            DataTable DT = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                LoadingPopUp.LoadIngPopUpShow(this);

                switch (cStep)
                {
                    case '1':       // 2010-08-10-정비재 : 입력한 검색조건의 데이터를 조회한다.
                        GridColumnInit(SS);

                        SS.Sheets[0].RowCount = 0;
                        
                        // 2010-08-10-정비재 : 검색조건에 따른 Open의 온도이력을 조회한다.
                        QRY = "SELECT A.RES_ID AS RES_ID \n"
                            + "     , A.OPER AS OPER \n"
                            + "     , TO_CHAR(TO_DATE(A.MIN_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD HH24:MI:SS') AS MIN_TIME \n"
                            + "     , TO_CHAR(TO_DATE(A.MAX_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD HH24:MI:SS') AS MAX_TIME \n"
                            + "     , A.LOT_ID AS LOT_ID \n"
                            + "     , '↓' \n"
                            + "     , A.MAT_ID AS MAT_ID \n"
                            + "     , A.PATTERN_NO AS PATTERN_NO \n"
                            + "     , A.MIN_TIME AS START_TIME \n"
                            + "  FROM (SELECT RES_ID AS RES_ID \n"
                            + "             , OPER AS OPER \n"
                            + "             , PATTERN_NO AS PATTERN_NO \n"
                            + "             , MIN(TEMP_TIME) AS MIN_TIME \n"
                            + "             , MAX(TEMP_TIME) AS MAX_TIME \n"
                            + "             , LOT_ID AS LOT_ID \n"
                            + "             , MAT_ID AS MAT_ID \n"
                            + "             , ' ' AS FILE_NAME \n"
                            // 2014-04-01-KJY: Change Database Source (MESMGR -> OVNMGR)
                            //+ "          FROM MRASLOTOVN@RPTTOMES \n"
                            + "          FROM OVNMGR.MRASLOTOVN \n"
                            + "         WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n"
                            + "           AND TEMP_TIME >= '" + cdvFromToDate.ExactFromDate + "' \n"
                            + "           AND TEMP_TIME <= '" + cdvFromToDate.ExactToDate + "' \n"
                            + "           AND OPER LIKE '" + (cdvStep.Text == "ALL" ? "%" : cdvStep.Text) + "' || '%' \n"
                            + "           AND RES_ID LIKE '" + (cdvOven.Text == "ALL" ? "%" : cdvOven.Text) + "' || '%' \n"
                            + "           AND MAT_ID LIKE '" + txtProduct.Text + "' || '%' \n"
                            + "           AND LOT_ID LIKE '" + txtLotID.Text + "' || '%' \n"
                            + "         GROUP BY RES_ID, MAT_ID, OPER, LOT_ID, PATTERN_NO) A \n"
                            + " ORDER BY A.RES_ID, A.OPER, A.MIN_TIME, A.LOT_ID";

                        DT = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", QRY);

                        if (DT.Rows.Count <= 0)
                        {
                            DT.Dispose();
                            LoadingPopUp.LoadingPopUpHidden();
                            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                            return false;
                        }
                        SS.DataSource = DT;
                        SS.RPT_AutoFit(false);
                        // 2010-09-16-정비재 : SS의 Columns 항목을 Button으로 변경한다.
                        ButtonCellType.Text = "↓";
                        SS.Sheets[0].Columns[5].CellType = ButtonCellType;
                        break;

                    case '2':
                        QRY = "SELECT DEF.OPER \n"
                            + "     , DECODE(DEF.MARK, '', LAG(DEF.MARK) OVER(ORDER BY ROWNUM), DEF.MARK) AS MARK \n"
                            + "     , DECODE(REAL.REAL_DATA, '', DECODE(DEF.MARK, '', LAG(DEF.MARK) OVER(ORDER BY ROWNUM), DEF.MARK), REAL.REAL_DATA) AS REAL_DATA \n"
                            + "  FROM (SELECT OPER \n"
                            + "             , 0 AS MARK \n"
                            + "             , MAX(REAL) AS REAL_DATA \n"
                            + "          FROM (SELECT (TRUNC(MOD((TO_DATE(TEMP_TIME, 'YYYYMMDDHH24MISS') - TO_DATE('" + SS.Sheets[0].Cells[SS_ActiveIndex, 8].Text + "', 'YYYYMMDDHH24MISS')), 1) * 24) * 60) \n"
                            + "                     + (TRUNC(MOD((TO_DATE(TEMP_TIME, 'YYYYMMDDHH24MISS') - TO_DATE('" + SS.Sheets[0].Cells[SS_ActiveIndex, 8].Text + "', 'YYYYMMDDHH24MISS')) * 24, 1) * 60)) AS OPER \n"
                            + "                     , 0 AS MARK \n"
                            + "                     , TEMP AS REAL \n"
                            + "                  FROM OVNMGR.MRASLOTOVN \n"
                            + "                 WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n"
                            + "                   AND LOT_ID = '" + SS.Sheets[0].Cells[SS_ActiveIndex, 4].Text + "' \n"
                            + "                   AND OPER = '" + SS.Sheets[0].Cells[SS_ActiveIndex, 1].Text + "') \n"
                            + "         GROUP BY OPER) REAL \n"
                            + "     , (SELECT OPER \n"
                            + "             , NVL(ROUND(FROM_TEMP + (TO_TEMP - FROM_TEMP) / TIME_GAP * (OPER - FROM_TIME)), TO_TEMP) AS MARK \n"
                            + "             , 0 AS REAL_DATA \n"
                            + "          FROM (SELECT NVL(LAG(TIME) OVER(ORDER BY SEQUENCE), 0) AS FROM_TIME \n"
                            + "                     , TIME AS TO_TIME \n"
                            + "                     , TIME_GAP AS TIME_GAP \n"
                            + "                     , NVL(LAG(TEMP) OVER(ORDER BY SEQUENCE), 25) AS FROM_TEMP \n"
                            + "                     , TEMP AS TO_TEMP \n"
                            + "                  FROM (SELECT SEQUENCE, SUM(TIME) OVER(ORDER BY SEQUENCE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS TIME \n"
                            + "                             , TIME AS TIME_GAP \n"
                            + "                             , TEMP AS TEMP \n"
                            + "                          FROM MRASOVNDEF@RPTTOMES \n"
                            + "                         WHERE PATTERN_NO = '" + SS.Sheets[0].Cells[SS_ActiveIndex, 7].Text + "')) DEF \n"
                            + "     , (SELECT OPER AS OPER \n"
                            + "          FROM (SELECT 0 AS OPER FROM DUAL \n"
                            + "                UNION ALL \n"
                            + "                SELECT LEVEL AS OPER FROM DUAL \n"
                            + "         WHERE LEVEL >= 0 \n"
                            + "       CONNECT BY LEVEL <= (SELECT MAX(TIME) AS TIME \n"
                            + "                              FROM (SELECT SUM(TIME) OVER(ORDER BY SEQUENCE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS TIME \n"
                            + "                                      FROM MRASOVNDEF@RPTTOMES \n"
                            + "                                     WHERE PATTERN_NO = '" + SS.Sheets[0].Cells[SS_ActiveIndex, 7].Text + "')))) TIME \n"
                            + "                             WHERE TIME.OPER >= DEF.FROM_TIME(+) \n"
                            + "                               AND TIME.OPER < DEF.TO_TIME(+)) DEF \n"
                            + " WHERE REAL.OPER(+) = DEF.OPER \n"
                            + " ORDER BY DEF.OPER";

                        DT = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", QRY);

                        if (DT != null && DT.Rows.Count > 0)
                        {
                            Form frmChart = new PQC031002_P1(SS.Sheets[0].Cells[SS_ActiveIndex, 4].Text, DT);
                            LoadingPopUp.LoadingPopUpHidden();
                            frmChart.ShowDialog();
                        }
                        break;
                }

                return true;
            }
            catch (Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(ex.Message);
                return false;
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion


        #region " Form Event "

        private void cdvStep_ValueButtonPress(object sender, EventArgs e)
        {
            /****************************************************
			 * Comment : Oven 자동화 대상 공정만 표시한다.
			 * 
			 * Created By : bee-jae jung(2010-08-09-월요일)
			 * 
			 * Modified By : bee-jae jung(2010-08-09-월요일)
			 ****************************************************/
            String QRY = "";
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                QRY = "SELECT A.OPER AS OPER_CODE, A.OPER_DESC AS OPER_DESC"
                    + "  FROM MWIPOPRDEF@RPTTOMES A, MATRNAMSTS@RPTTOMES B"
                    + " WHERE A.FACTORY = B.FACTORY"
                    + "   AND A.OPER = B.ATTR_KEY"
                    + "   AND A.FACTORY " + cdvFactory.SelectedValueToQueryString + ""
                    + "   AND B.ATTR_TYPE = 'OPER'"
                    + "   AND B.ATTR_NAME = 'PATTERN_CHECK'"
                    + "   AND B.ATTR_VALUE = 'Y'"
                    + " ORDER BY A.OPER ASC";

                if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                {
                    Clipboard.SetText(QRY);
                }

                cdvStep.sDynamicQuery = QRY;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                cdvStep.sDynamicQuery = "";
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void cdvOven_ValueButtonPress(object sender, EventArgs e)
        {
            /****************************************************
			 * Comment : Oven 자동화 대상 설비만 표시한다.
			 * 
			 * Created By : bee-jae jung(2010-08-09-월요일)
			 * 
			 * Modified By : bee-jae jung(2010-08-09-월요일)
			 ****************************************************/
            string QRY = "";
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                cdvOven.SetChangedFlag(true);
                cdvOven.Text = "";

                // 2010-08-09-정비재 : Oven 자동화 대상 설비만 조회한다.
                QRY = "SELECT A.RES_ID AS RES_ID, A.RES_DESC AS RES_DESC"
                    + "  FROM MRASRESDEF A"
                    + " WHERE A.FACTORY " + cdvFactory.SelectedValueToQueryString + ""
                    + "   AND A.RES_GRP_3 IN ('CURE OVEN', 'UV', 'PLASMA', 'BAKE')"
                    + "   AND A.RES_PROC_MODE = 'FULL AUTO'"
                    + "   AND A.DELETE_FLAG <> 'Y'"
                    + " ORDER BY A.RES_ID ASC";

                if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                {
                    Clipboard.SetText(QRY);
                }

                cdvOven.sDynamicQuery = QRY;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                cdvOven.sDynamicQuery = "";
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            /****************************************************
			 * Comment : 조회버튼을 클릭하면
			 * 
			 * Created By : bee-jae jung(2010-08-09-월요일)
			 * 
			 * Modified By : bee-jae jung(2010-08-09-월요일)
			 ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (CheckField() == false) return;

                fnDataFind('1');
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
			 * Comment : SS의 데이터를 Excel파일로 내보내기 한다.
			 * 
			 * Created By : 안시홍(2010-08-10)
			 * 
			 * Modified By :
			 ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                SS.ExportExcel();
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

        private void SS_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            /****************************************************
			 * Comment : Oven 자동화 대상 설비만 표시한다.
			 * 
			 * Created By : bee-jae jung(2010-08-09-월요일)
			 * 
			 * Modified By : bee-jae jung(2010-08-10-화요일)
			 ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // 2010-08-10-정비재 : 선택한 Lot ID에 대한 Temp Profile을 Display한다.
                if (e.Column == 4)
                {
                    fnDataFind('2');
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

        private void SS_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            /****************************************************
             * Comment : 셀 선택하면 배경색 표시한다.
             * 
             * Created By : Si-hong An(2010-08-10-화요일)
             * 
             * Modified By : Si-hong An(2010-08-10-화요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                for (int i = 0; i <= SS.ActiveSheet.RowCount - 1; i++)
                {
                    for (int j = 0; j <= SS.ActiveSheet.ColumnCount - 1; j++)
                    {
                        SS.ActiveSheet.Cells[i, j].BackColor = Color.Empty;
                    }
                }
                
                // 2010-09-15-정비재 : 선택한 Row의 Index를 변수에 저장한다.
                SS_ActiveIndex = e.Row;
                // 2010-09-15-정비재 : 아! ButtonClicked Event가 왜 않되는 것이야(미라콤 정말 싫다.)
                // 2010-09-15-정비재 : 선택한 Lot ID에 대한 Temp Profile을 Display한다.
                if (e.Column == 5)
                {
                    SS.ActiveSheet.Cells[SS_ActiveIndex, e.Column - 1].BackColor = Color.LawnGreen;
                    fnDataFind('2');
                }
                else
                {
                    SS.ActiveSheet.Cells[SS_ActiveIndex, e.Column].BackColor = Color.LawnGreen;
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

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            cdvStep.sFactory = cdvFactory.txtValue;
        }   

        #endregion     
    }
}