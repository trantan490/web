using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.UI.Controls;

namespace Hana.PQC
{
    public partial class PQC031003 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {               
        /// <summary>
        /// 클  래  스: PQC031003<br/>
        /// 클래스요약: 오븐 온도 ProFile<br/>
        /// 작  성  자: 미라콤 김태순<br/>
        /// 최초작성일: 2009-01-19<br/>
        /// 상세  설명: 오븐 온도 ProFile<br/>
        /// 변경  내용: <br/>
        /// </summary>
        public PQC031003()
        {
            InitializeComponent();
            string NowDate = DateTime.Now.ToString();
            cdvFromToDate.AutoBinding(NowDate.Substring(0, 10), NowDate.Substring(0, 10));
            GridColumnInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory; 
        }

        #region 한줄헤더생성

        /// <summary>
        /// 한줄헤더생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {
            try
            {
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("Oven number", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Operation", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PATTERN_NO", 0, 2, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Start time", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Start_date", 0, 4, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("End time", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LotID", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("IN_Qty", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Product", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("Temperature history file name", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 200);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        #endregion

        #region 조회

        #region MakeSqlString
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string sFrom = cdvFromToDate.ExactFromDate;
            string sTo = cdvFromToDate.ExactToDate;

            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;


            strSqlString.AppendFormat("SELECT RES_ID, A.OPER, PATTERN_NO, TO_CHAR(TO_DATE(MIN_TIME,'YYYYMMDDHH24MISS'), 'YYYY-MM-DD HH24:MI:SS') AS MIN_DATE, MIN_TIME " + "\n");
            strSqlString.AppendFormat("     , TO_CHAR(TO_DATE(MAX_TIME,'YYYYMMDDHH24MISS'), 'YYYY-MM-DD HH24:MI:SS') AS MAX_TIME, A.LOT_ID, QTY_1, A.MAT_ID, '' AS FILE_NAME" + "\n");
            strSqlString.AppendFormat("     FROM(" + "\n");
            strSqlString.AppendFormat("         SELECT UNIQUE RES_ID, MAT_ID, OPER, LOT_ID, PATTERN_NO, MIN(TEMP_TIME) AS MIN_TIME, MAX(TEMP_TIME) AS MAX_TIME" + "\n");
            strSqlString.AppendFormat("             FROM MRASLOTOVN@RPTTOMES" + "\n");
            strSqlString.AppendFormat("         WHERE 1=1" + "\n");
            strSqlString.AppendFormat("             AND TEMP_TIME BETWEEN '{0}' AND '{1}'" + "\n", sFrom, sTo);
            strSqlString.AppendFormat("             AND RES_ID {0}" + "\n", cdvOven.SelectedValueToQueryString);
            strSqlString.AppendFormat("         GROUP BY  RES_ID, MAT_ID, OPER ,LOT_ID, PATTERN_NO) A," + "\n");
            strSqlString.AppendFormat("         (SELECT UNIQUE MAT_ID, OLD_OPER, LOT_ID, MAX(TRAN_TIME), QTY_1" + "\n");
            strSqlString.AppendFormat("             FROM RWIPLOTHIS" + "\n");
            strSqlString.AppendFormat("         WHERE 1=1" + "\n");
            strSqlString.AppendFormat("             AND TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", sFrom, sTo);
            strSqlString.AppendFormat("             AND TRAN_CODE IN ('END')" + "\n");
            if (cdvStep.SelectedValueToQueryString.Equals(""))
            {
                strSqlString.AppendFormat("             AND OLD_OPER in (select ATTR_KEY" + "\n");
                strSqlString.AppendFormat("                                 FROM MATRNAMSTS" + "\n");
                strSqlString.AppendFormat("                             WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.AppendFormat("                                 AND ATTR_TYPE = 'OPER'" + "\n");
                strSqlString.AppendFormat("                                 AND ATTR_NAME = 'PATTERN_CHECK'" + "\n");
                strSqlString.AppendFormat("                                 AND ATTR_VALUE = 'Y')" + "\n");
            }
            else
            {
                strSqlString.AppendFormat("             AND OLD_OPER {0}" + "\n", cdvStep.SelectedValueToQueryString);
            }
            strSqlString.AppendFormat("                             GROUP BY MAT_ID, OLD_OPER, LOT_ID, QTY_1" + "\n");
            strSqlString.AppendFormat("         ) B" + "\n");
            strSqlString.AppendFormat("WHERE 1=1" + "\n");
            strSqlString.AppendFormat("     AND A.LOT_ID = B.LOT_ID" + "\n");
            strSqlString.AppendFormat("     AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.AppendFormat("     AND A.OPER = B.OLD_OPER" + "\n");
            strSqlString.AppendFormat("ORDER BY RES_ID, MIN_TIME, LOT_ID, A.OPER" + "\n");
            strSqlString.AppendFormat(" " + "\n");

            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());

            return strSqlString.ToString();
        }
        #endregion


        private string DataChart()
        {
            StringBuilder strSqlString = new StringBuilder();

            string sFrom = cdvFromToDate.ExactFromDate;
            string sTo = cdvFromToDate.ExactToDate;


            string JoupTime = GetJoupTime(sFrom, sTo);

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            strSqlString.AppendFormat("SELECT MINS AS OPER, DECODE(TEMP, '', 25, TEMP) AS TEMP, TEMP_HU, TEMP_MINS" + "\n");
            strSqlString.AppendFormat("     FROM (" + "\n");
            strSqlString.AppendFormat("     SELECT DISTINCT TEMP.MINS, OVN.TEMP, TEMP.TEMP_HU, TEMP.TEMP_MINS" + "\n");
            strSqlString.AppendFormat("             FROM(" + "\n");
            strSqlString.AppendFormat("             SELECT (TRUNC(TO_DATE(TEMP_TIME, 'YYYYMMDDHH24MISS') - TO_DATE('{0}', 'YYYYMMDDHH24MISS'))) AS TEMP_HU," + "\n", sFrom);
            strSqlString.AppendFormat("                 (TRUNC(MOD((TO_DATE(TEMP_TIME,'YYYYMMDDHH24MISS')-TO_DATE('{0}','YYYYMMDDHH24MISS')),1)*24)*60) AS TEMP_MINS," + "\n", sFrom);
            strSqlString.AppendFormat("                 TEMP, TO_CHAR(TO_DATE(substr(TEMP_TIME,9, 2)||':'||substr(TEMP_TIME,11, 2),'HH24:MI'),'HH24:MI') AS MINS" + "\n");
            strSqlString.AppendFormat("                 FROM(" + "\n");
            strSqlString.AppendFormat("                     SELECT TEMP_TIME, TEMP," + "\n");
            strSqlString.AppendFormat("                         (SELECT MIN(TEMP_TIME) AS MIN_TIME FROM MRASLOTOVN@RPTTOMES WHERE 1=1 AND TEMP_TIME BETWEEN '{0}' AND '{1}'" + "\n", sFrom, sTo);
            strSqlString.AppendFormat("                         AND RES_ID {0} ) AS START_TIME" + "\n", cdvOven.SelectedValueToQueryString);
            strSqlString.AppendFormat("                         FROM MRASLOTOVN@RPTTOMES" + "\n");
            strSqlString.AppendFormat("                 WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                     AND TEMP_TIME BETWEEN '{0}' AND '{1}'" + "\n", sFrom, sTo);
            strSqlString.AppendFormat("                     AND RES_ID  {0} " + "\n", cdvOven.SelectedValueToQueryString);
            strSqlString.AppendFormat("                     AND LOT_ID IN (" + "\n");
            strSqlString.AppendFormat("                         SELECT LOT_ID FROM(" + "\n");
            strSqlString.AppendFormat("                             SELECT MAX_TIME, MAX(LOT_ID) AS LOT_ID, MIN(MIN_TIME) AS MIN_TIME " + "\n");
            strSqlString.AppendFormat("                                 FROM(" + "\n");
            strSqlString.AppendFormat("                                     SELECT MAT_ID, OPER, PATTERN_NO, MAX(LOT_ID) AS LOT_ID, MIN(TEMP_TIME) AS MIN_TIME, MAX(TEMP_TIME) AS MAX_TIME" + "\n");
            strSqlString.AppendFormat("                                         FROM MRASLOTOVN@RPTTOMES" + "\n");
            strSqlString.AppendFormat("                                     WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                                         AND TEMP_TIME BETWEEN '{0}' AND '{1}'" + "\n", sFrom, sTo);
            strSqlString.AppendFormat("                                         AND RES_ID  {0} " + "\n", cdvOven.SelectedValueToQueryString);
            strSqlString.AppendFormat("                                     GROUP BY  MAT_ID, OPER ,LOT_ID, PATTERN_NO" + "\n");
            strSqlString.AppendFormat("                                 )" + "\n");
            strSqlString.AppendFormat("                             GROUP BY MAX_TIME" + "\n");
            strSqlString.AppendFormat("                         ) " + "\n");
            strSqlString.AppendFormat("                 )" + "\n");
            strSqlString.AppendFormat("             )" + "\n");
            strSqlString.AppendFormat("             ORDER BY TEMP_MINS, TEMP_HU, MINS" + "\n");
            strSqlString.AppendFormat("             ) OVN," + "\n");
            strSqlString.AppendFormat("             (SELECT TO_CHAR(TO_DATE('21:59','HH24:MI')+ LEVEL/24/60,'HH24:MI') AS MINS" + "\n");
            strSqlString.AppendFormat("                 , (TRUNC((TO_DATE('20090826215959','YYYYMMDDHH24MISS')+ LEVEL/24/60) - TO_DATE('20090826220000', 'YYYYMMDDHH24MISS'))) AS TEMP_HU" + "\n");
            strSqlString.AppendFormat("                 , (TRUNC(MOD(((TO_DATE('20090826215959','YYYYMMDDHH24MISS')+ LEVEL/24/60)-TO_DATE('20090826220000','YYYYMMDDHH24MISS')),1)*24)*60) AS TEMP_MINS" + "\n");
            strSqlString.AppendFormat("                 FROM DUAL WHERE LEVEL >= 0 connect by level <= " + JoupTime + ") TEMP" + "\n");
            strSqlString.AppendFormat("             WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                 AND OVN.TEMP_HU(+) = TEMP.TEMP_HU" + "\n");
            strSqlString.AppendFormat("                 AND OVN.MINS(+) = TEMP.MINS" + "\n");
            strSqlString.AppendFormat("                 AND OVN.TEMP_MINS(+) = TEMP.TEMP_MINS" + "\n");
            strSqlString.AppendFormat("       )" + "\n");
            strSqlString.AppendFormat("       ORDER BY TEMP_HU, TEMP_MINS, MINS" + "\n");


            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }


        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {

            if (cdvStep.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
                return false;
            }

            if (cdvOven.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("PQC001", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        #endregion



        /// <summary>
        /// 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            if (!CheckField()) return;

            DataTable dt = null;

            GridColumnInit();

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                spdData_Sheet1.RowCount = 0;
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

                spdData.isShowZero = true;

                spdData.RPT_AutoFit(false);

                //Chart 생성
                if (spdData.ActiveSheet.RowCount > 0)
                    ChartShow(0);

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
        #endregion


        private void ChartShow(int rowCount)
        {

            DataTable Data_dt = null;

            Data_dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", DataChart());


            string JoupTime = GetJoupTime(cdvFromToDate.ExactFromDate, cdvFromToDate.ExactToDate);


            int columnCount = 0;
            double max1 = 0;

            // 차트 설정
             udcChartFX1.RPT_1_ChartInit();
            udcChartFX1.RPT_2_ClearData();
            udcChartFX1.RPT_3_OpenData(1, Data_dt.Rows.Count);

            int[] tat_columns = new Int32[Data_dt.Rows.Count];
            int[] columnsHeader = new Int32[Data_dt.Rows.Count];
            int[] dt_columns = new Int32[Data_dt.Rows.Count];


            for (int i = 0; i < dt_columns.Length; i++)
            {
                tat_columns[i] = 0 + i;
                dt_columns[i] = 0 + i;
                columnsHeader[i] = 0 + i;
            }

            max1 = udcChartFX1.RPT_4_AddData(Data_dt, tat_columns, new int[] { columnCount + 1 }, SeriseType.Column);

            udcChartFX1.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            udcChartFX1.RPT_6_SetGallery(ChartType.Curve, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            udcChartFX1.Series[0].Color = System.Drawing.Color.Blue;

            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(Data_dt);
            udcChartFX1.Scrollable = true;
            udcChartFX1.AxisX.Step = 10;
            //udcChartFX1.AxisX.Staggered = true;
           // udcChartFX1.AxisX.PixPerUnit = (Convert.ToInt16(JoupTime)/14);
            udcChartFX1.AxisX.SetScrollView(0, Convert.ToInt16(JoupTime) / 8 / (Convert.ToInt16(JoupTime)/24/60));
            udcChartFX1.AxisY.Gridlines = true;
            udcChartFX1.Highlight.PointAttributes.PointLabelColor = System.Drawing.Color.Transparent;
            udcChartFX1.AxisY.DataFormat.Decimals = 0;



        }

        private string GetJoupTime(string strFromDate, string strToDate)
        {
            int year = 0;
            int month = 0;
            int day = 0;

            DateTime dtFrom = DateTime.MinValue;
            DateTime dtTo = DateTime.MinValue;

            int retValue = 0;

            try
            {
                year = Convert.ToInt32(strFromDate.Substring(0, 4));
                month = Convert.ToInt32(strFromDate.Substring(4, 2));
                day = Convert.ToInt32(strFromDate.Substring(6, 2));

                dtFrom = new DateTime(year, month, day);

                string NowDate = Convert.ToString(DateTime.Now).Substring(0, 7);

                year = Convert.ToInt32(strToDate.Substring(0, 4));
                month = Convert.ToInt32(strToDate.Substring(4, 2));
                day = Convert.ToInt32(strToDate.Substring(6, 2));

                dtTo = new DateTime(year, month, day);

                string TmpsTo = Convert.ToString(dtTo).Substring(0, 7);

                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH" && TmpsTo == NowDate)
                {
                    retValue = (new TimeSpan(DateTime.Now.Ticks - dtFrom.Ticks)).Days * 1440;
                }
                else
                {
                    retValue = (new TimeSpan(dtTo.Ticks - dtFrom.Ticks)).Days * 1440;
                }
            }
            catch
            {
                throw new InvalidCastException(RptMessages.GetMessage("STD099", GlobalVariable.gcLanguage));
            }


            return retValue.ToString();
        }

        private void cdvStep_ValueButtonPress(object sender, EventArgs e)
        {

            string strQuery = string.Empty;
            strQuery += "SELECT ATTR_KEY, OPER_DESC" + "\n";
            strQuery += "   FROM MATRNAMSTS A," + "\n";
            strQuery += "   MWIPOPRDEF B" + "\n";
            strQuery += "WHERE 1=1" + "\n";
            strQuery += "   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n";
            strQuery += "   AND A.ATTR_NAME = 'PATTERN_CHECK'" + "\n";
            strQuery += "   AND A.ATTR_KEY = B.OPER" + "\n";
            strQuery += "ORDER BY ATTR_KEY" + "\n";

            if (cdvFactory.txtValue != "")
                cdvStep.sDynamicQuery = strQuery;
            else
                cdvStep.sDynamicQuery = "";
        }

        private void cdvOven_ValueButtonPress(object sender, EventArgs e)
        {
            cdvOven.SetChangedFlag(true);
            cdvOven.Text = "";

            string strQuery = string.Empty;

            strQuery += "SELECT DISTINCT B.RES_ID, '' AS TEMP FROM " + "\n";
            strQuery += "   MRASRESDEF A," + "\n";
            strQuery += "   MRASRESMFO B" + "\n";
            strQuery += "WHERE 1=1" + "\n";
            strQuery += "   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n";
            strQuery += "   AND A.RES_ID LIKE 'BO%'" + "\n";
            strQuery += "   AND A.RES_PROC_MODE = 'FULL AUTO'" + "\n";
            strQuery += "   AND B.OPER " + cdvStep.SelectedValueToQueryString + "\n";
            strQuery += "   AND B.RES_ID = A.RES_ID" + "\n";
            strQuery += "ORDER BY B.RES_ID" + "\n";

            if (cdvStep.txtValue != "")
                cdvOven.sDynamicQuery = strQuery;
            else
                cdvOven.sDynamicQuery = "";

        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == 6)
            {
                string strOper = spdData.ActiveSheet.Cells[e.Row, 1].Value.ToString();
                string strLotId = spdData.ActiveSheet.Cells[e.Row, 6].Value.ToString();
                string strStartTime = spdData.ActiveSheet.Cells[e.Row, 4].Value.ToString();
                string strPattern = spdData.ActiveSheet.Cells[e.Row, 2].Value.ToString();

                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", LotIDStringForPopup(strOper, strLotId, strStartTime, strPattern));

                if (dt != null && dt.Rows.Count > 0)
                {
                    System.Windows.Forms.Form frm = new PQC031002_P1(strLotId, dt);
                    frm.ShowDialog();
                }

            }
        }

        private string LotIDStringForPopup(string strOper, string strLotId, string strStartTime, string strPattern)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("SELECT DEF.OPER, DECODE(DEF.MARK, '', lag(DEF.MARK) over (order by rownum), DEF.MARK) AS MARK, DECODE(REAL.REAL_DATA, '', DECODE(DEF.MARK, '', lag(DEF.MARK) over (order by rownum), DEF.MARK), REAL.REAL_DATA) AS REAL_DATA" + "\n");
            strSqlString.AppendFormat("     FROM(" + "\n");
            strSqlString.AppendFormat("         SELECT OPER, 0 AS MARK, MAX(REAL) AS REAL_DATA" + "\n");
            strSqlString.AppendFormat("             FROM(" + "\n");
            strSqlString.AppendFormat("                 SELECT (TRUNC(MOD((TO_DATE(TEMP_TIME,'YYYYMMDDHH24MISS')-TO_DATE('" + strStartTime + "','YYYYMMDDHH24MISS')),1)*24)*60) +" + "\n");
            strSqlString.AppendFormat("                     (TRUNC(MOD((TO_DATE(TEMP_TIME,'YYYYMMDDHH24MISS')-TO_DATE('" + strStartTime + "','YYYYMMDDHH24MISS'))*24,1)*60)) AS OPER," + "\n");
            strSqlString.AppendFormat("                     0 AS MARK, TEMP AS REAL" + "\n");
            strSqlString.AppendFormat("                     FROM MRASLOTOVN@RPTTOMES" + "\n");
            strSqlString.AppendFormat("                 WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                     AND LOT_ID ='" + strLotId + "'" + "\n");
            strSqlString.AppendFormat("                     AND OPER = '" + strOper + "'" + "\n");
            strSqlString.AppendFormat("         )" + "\n");
            strSqlString.AppendFormat("         GROUP BY OPER" + "\n");
            strSqlString.AppendFormat("     ) REAL," + "\n");
            strSqlString.AppendFormat("     (" + "\n");
            strSqlString.AppendFormat("     SELECT OPER, NVL(ROUND(FROM_TEMP+(TO_TEMP-FROM_TEMP)/TIME_GAP*(OPER-FROM_TIME)),TO_TEMP) AS MARK, 0 AS REAL_DATA" + "\n");
            strSqlString.AppendFormat("         FROM (" + "\n");
            strSqlString.AppendFormat("             SELECT NVL(LAG(TIME)OVER(ORDER BY SEQUENCE),0) FROM_TIME, TIME TO_TIME, TIME_GAP, NVL(LAG(TEMP)OVER(ORDER BY SEQUENCE),25) FROM_TEMP, TEMP TO_TEMP" + "\n");
            strSqlString.AppendFormat("                 FROM (" + "\n");
            strSqlString.AppendFormat("                     SELECT SEQUENCE, SUM(TIME)OVER(ORDER BY SEQUENCE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS TIME, TIME TIME_GAP,  TEMP" + "\n");
            strSqlString.AppendFormat("                     FROM MRASOVNDEF@RPTTOMES" + "\n");
            strSqlString.AppendFormat("                     WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                     AND PATTERN_NO ='" + strPattern + "'" + "\n");
            strSqlString.AppendFormat("                 )" + "\n");
            strSqlString.AppendFormat("             ) DEF," + "\n");
            strSqlString.AppendFormat("      (" + "\n");
            strSqlString.AppendFormat("         SELECT OPER FROM (" + "\n");
            strSqlString.AppendFormat("             SELECT 0 AS OPER FROM DUAL UNION ALL" + "\n");
            strSqlString.AppendFormat("             SELECT LEVEL AS OPER FROM DUAL WHERE LEVEL >= 0 connect by level <= ( " + "\n");
            strSqlString.AppendFormat("                 SELECT MAX(TIME) AS TIME" + "\n");
            strSqlString.AppendFormat("                     FROM(" + "\n");
            strSqlString.AppendFormat("                         SELECT SUM(TIME)OVER(ORDER BY SEQUENCE ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS TIME" + "\n");
            strSqlString.AppendFormat("                         FROM MRASOVNDEF@RPTTOMES" + "\n");
            strSqlString.AppendFormat("                         WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                          AND PATTERN_NO='" + strPattern + "'" + "\n");
            strSqlString.AppendFormat("                         )" + "\n");
            strSqlString.AppendFormat("                     )" + "\n");
            strSqlString.AppendFormat("                 )" + "\n");
            strSqlString.AppendFormat("             ) TIME" + "\n");
            strSqlString.AppendFormat("             WHERE (TIME.OPER >= DEF.FROM_TIME(+) and TIME.OPER < DEF.TO_TIME(+))" + "\n");
            strSqlString.AppendFormat("       ) DEF" + "\n");
            strSqlString.AppendFormat("WHERE REAL.OPER(+) = DEF.OPER" + "\n");
            strSqlString.AppendFormat("ORDER BY DEF.OPER" + "\n");

            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            return strSqlString.ToString();
        }

    }
}
