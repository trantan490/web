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

namespace Hana.TRN
{
    public partial class TRN090401 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        String[] stDayList1 = new String[7];
        String[] stDayList2 = new String[7];
        String stLastMonth = null;
        String stThisMonth = null;

        /// <summary>
        /// 클  래  스: TRN090401<br/>
        /// 클래스요약: TAT 지표관리<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2010-08-04<br/>
        /// 상세  설명: 지표관리 TAT를 고객사, 제품별로 조회 한다.<br/>
        /// 변경  내용: <br/>
        /// </summary>
        public TRN090401()
        {
            InitializeComponent();

            GridColumnInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            //this.cdvMatType.SetFactory = GlobalVariable.gsAssyDefaultFactory;          
        }

        private Boolean CheckField()
        {
            if (cdvCustom.Text.Trim() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD033", GlobalVariable.gcLanguage));
                return false;
            }

            if (cdvPkg.Text.Trim() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD081", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();

            GetDayList();

            spdData.RPT_AddBasicColumn("Classification", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn(stThisMonth.Substring(4, 2) + "월 AVG", 0, 1, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.String, 80);

            for (int i = 0; i < 7; i++)
            {
                spdData.RPT_AddBasicColumn(stDayList2[i], 0, spdData_Sheet1.ColumnHeader.Columns.Count, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.String, 80);
            }                       
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            if (CheckField() == false) return;

            GridColumnInit();

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
                               
                // 첫 컬럼 색상 지정
                spdData.ActiveSheet.Columns[0].BackColor = Color.YellowGreen;

                if(spdData.ActiveSheet.RowCount > 0)
                    ShowChart(0);

                dt.Dispose();

            }
            catch(Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT *" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT 'TAT' AS GUBUN" + "\n");
            strSqlString.Append("             , ROUND(AVG(DECODE(WORK_MONTH, '" + stThisMonth + "', TAT)), 2) AS MON_TAT" + "\n");
            strSqlString.Append("             , ROUND(SUM(DECODE(WORK_DATE, '" + stDayList1[0] + "', TAT, 0)), 2) AS D0" + "\n");
            strSqlString.Append("             , ROUND(SUM(DECODE(WORK_DATE, '" + stDayList1[1] + "', TAT, 0)), 2) AS D1" + "\n");
            strSqlString.Append("             , ROUND(SUM(DECODE(WORK_DATE, '" + stDayList1[2] + "', TAT, 0)), 2) AS D2" + "\n");
            strSqlString.Append("             , ROUND(SUM(DECODE(WORK_DATE, '" + stDayList1[3] + "', TAT, 0)), 2) AS D3" + "\n");
            strSqlString.Append("             , ROUND(SUM(DECODE(WORK_DATE, '" + stDayList1[4] + "', TAT, 0)), 2) AS D4" + "\n");
            strSqlString.Append("             , ROUND(SUM(DECODE(WORK_DATE, '" + stDayList1[5] + "', TAT, 0)), 2) AS D5" + "\n");
            strSqlString.Append("             , ROUND(SUM(DECODE(WORK_DATE, '" + stDayList1[6] + "', TAT, 0)), 2) AS D6" + "\n");
            strSqlString.Append("          FROM RSUMTRNTAT" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND WORK_MONTH IN ('" + stLastMonth + "','" + stThisMonth + "')" + "\n");
            strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND CUSTOMER = '" + cdvCustom.Text + "'" + "\n");
            strSqlString.Append("           AND PKG = '" + cdvPkg.Text + "'" + "\n");
            strSqlString.Append("           AND OPER_GRP = 'ASSY_TAT'" + "\n");
            strSqlString.Append("         UNION ALL" + "\n");
            strSqlString.Append("        SELECT 'SHIP' AS GUBUN" + "\n");

            if (chkKpcs.Checked == true)
            {
                strSqlString.Append("             , ROUND(AVG(DECODE(WORK_MONTH, '" + stThisMonth + "', SHIP_QTY)) / 1000, 2) AS MON_SHIP" + "\n");
                strSqlString.Append("             , ROUND(SUM(DECODE(WORK_DATE, '" + stDayList1[0] + "', SHIP_QTY, 0)) / 1000, 2) AS D0" + "\n");
                strSqlString.Append("             , ROUND(SUM(DECODE(WORK_DATE, '" + stDayList1[1] + "', SHIP_QTY, 0)) / 1000, 2) AS D1" + "\n");
                strSqlString.Append("             , ROUND(SUM(DECODE(WORK_DATE, '" + stDayList1[2] + "', SHIP_QTY, 0)) / 1000, 2) AS D2" + "\n");
                strSqlString.Append("             , ROUND(SUM(DECODE(WORK_DATE, '" + stDayList1[3] + "', SHIP_QTY, 0)) / 1000, 2) AS D3" + "\n");
                strSqlString.Append("             , ROUND(SUM(DECODE(WORK_DATE, '" + stDayList1[4] + "', SHIP_QTY, 0)) / 1000, 2) AS D4" + "\n");
                strSqlString.Append("             , ROUND(SUM(DECODE(WORK_DATE, '" + stDayList1[5] + "', SHIP_QTY, 0)) / 1000, 2) AS D5" + "\n");
                strSqlString.Append("             , ROUND(SUM(DECODE(WORK_DATE, '" + stDayList1[6] + "', SHIP_QTY, 0)) / 1000, 2) AS D6" + "\n");
            }
            else
            {
                strSqlString.Append("             , ROUND(AVG(DECODE(WORK_MONTH, '" + stThisMonth + "', SHIP_QTY)), 2) AS MON_SHIP" + "\n");
                strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + stDayList1[0] + "', SHIP_QTY, 0)) AS D0" + "\n");
                strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + stDayList1[1] + "', SHIP_QTY, 0)) AS D1" + "\n");
                strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + stDayList1[2] + "', SHIP_QTY, 0)) AS D2" + "\n");
                strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + stDayList1[3] + "', SHIP_QTY, 0)) AS D3" + "\n");
                strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + stDayList1[4] + "', SHIP_QTY, 0)) AS D4" + "\n");
                strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + stDayList1[5] + "', SHIP_QTY, 0)) AS D5" + "\n");
                strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + stDayList1[6] + "', SHIP_QTY, 0)) AS D6" + "\n");
            }

            strSqlString.Append("          FROM RSUMTRNTAT" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND WORK_MONTH IN ('" + stLastMonth + "','" + stThisMonth + "')" + "\n");
            strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND CUSTOMER = '" + cdvCustom.Text + "'" + "\n");
            strSqlString.Append("           AND PKG = '" + cdvPkg.Text + "'" + "\n");
            strSqlString.Append("           AND OPER_GRP = 'ASSY_TAT'" + "\n");
            strSqlString.Append("         UNION ALL" + "\n");
            strSqlString.Append("        SELECT 'TAT 목표' AS GUBUN" + "\n");
            strSqlString.Append("             , SUM(DATA_2)" + "\n");
            strSqlString.Append("             , SUM(DATA_2)" + "\n");
            strSqlString.Append("             , SUM(DATA_2)" + "\n");
            strSqlString.Append("             , SUM(DATA_2)" + "\n");
            strSqlString.Append("             , SUM(DATA_2)" + "\n");
            strSqlString.Append("             , SUM(DATA_2)" + "\n");
            strSqlString.Append("             , SUM(DATA_2)" + "\n");
            strSqlString.Append("             , SUM(DATA_2)" + "\n");
            strSqlString.Append("          FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND TABLE_NAME = 'H_RPT_TAT_MAINOBJECT'" + "\n");
            strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND KEY_1 <= '" + cdvDate.SelectedValue() + "'" + "\n");
            strSqlString.Append("           AND DATA_1 >= '" + cdvDate.SelectedValue() + "'" + "\n");
            strSqlString.Append("           AND KEY_2 = '" + cdvCustom.Text + "'" + "\n");
            strSqlString.Append("           AND KEY_3 = '" + cdvPkg.Text + "'" + "\n");
            strSqlString.Append("       )" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private void ShowChart(int rowCount)
        {
            double max_temp = 0;
            double max1 = 0, max2 = 0;
            int[] tat_columns = new Int32[8];
            int[] ship_columns = new Int32[8];
            int[] target_columns = new Int32[8];
            int[] columnsHeader = new Int32[8];
            String legendDescShip = "SHIP [단위 : ea]";
            String legendDescTAT = "TAT [단위 : day]";

            for (int i = 0; i < tat_columns.Length; i++)
            {
                columnsHeader[i] = 1 + i;
                tat_columns[i] = 1 + i;
                ship_columns[i] = 1 + i;
                target_columns[i] = 1 + i;
            }
            
            if (chkKpcs.Checked == true)
            {
                legendDescShip = "SHIP [단위 : kpcs]";
                legendDescTAT = "TAT [단위 : day]";
            }

            udcMSChart1.RPT_1_ChartInit();
            udcMSChart1.RPT_2_ClearData();
            udcMSChart1.RPT_3_OpenData(3, 8);
            max2 = udcMSChart1.RPT_4_AddData(spdData, new int[] { rowCount + 1 }, ship_columns, SeriseType.Rows);
            max1 = udcMSChart1.RPT_4_AddData(spdData, new int[] { rowCount + 0 }, tat_columns, SeriseType.Rows);
            if (max1 > max_temp)
            {
                max_temp = max1;
            }
            max1 = udcMSChart1.RPT_4_AddData(spdData, new int[] { rowCount + 2 }, target_columns, SeriseType.Rows);
            if (max1 > max_temp)
            {
                max_temp = max1;
            }
            max1 = max_temp;

            udcMSChart1.RPT_5_CloseData();
            udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 0, 1, legendDescShip, AsixType.Y2, DataTypes.Initeger, max2 * 1.2);
            udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 1, 1, legendDescTAT, AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, 2, 1, legendDescTAT, AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            udcMSChart1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            udcMSChart1.RPT_8_SetSeriseLegend(new String[] { "SHIP", "TAT", "TAT 목표" }, System.Windows.Forms.DataVisualization.Charting.Docking.Top);
        }

        // 날짜 구하기
        private void GetDayList()
        {
            int y = -6;

            for (int i = 0; i < 7; i++)
            {
                stDayList1[i] = cdvDate.Value.AddDays(y).ToString("yyyyMMdd");
                stDayList2[i] = cdvDate.Value.AddDays(y).ToString("MM.dd");
                y++;
            }

            stLastMonth = cdvDate.Value.AddMonths(-1).ToString("yyyyMM");
            stThisMonth = cdvDate.Value.ToString("yyyyMM");
        }
    }
}
