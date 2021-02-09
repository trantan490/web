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

namespace Hana.TAT
{
    public partial class TAT050101 : Miracom.SmartWeb.UI.Controls.udcCUSReportMain001
    {

        private DataTable dadt = null;
        private DataTable chdt = null;
        private DataTable wkdt = null;
        private DataTable temdt = null;
        string factory = GlobalVariable.gsAssyDefaultFactory;
        string customer = "SE";

        public TAT050101()
        {
            InitializeComponent();
            GridColumnInit(); //헤더 한줄짜리 

        }

        private void InitMainPageSpread(FarPoint.Win.Spread.FpSpread spdCtl)
        {
            FarPoint.Win.Spread.SheetSkin spSkin;

            try
            {
                FarPoint.Win.Spread.DefaultSpreadSkins.GetAt(1).Apply(spdCtl);
                spSkin = new FarPoint.Win.Spread.SheetSkin("InitSkin",
                                           System.Drawing.Color.White,
                                           System.Drawing.Color.Empty,
                                           System.Drawing.Color.Empty,
                                           System.Drawing.Color.Black,
                                           FarPoint.Win.Spread.GridLines.Both,
                                           System.Drawing.Color.FromArgb(108, 144, 188),
                                           System.Drawing.Color.White,
                                           System.Drawing.Color.Transparent,
                                           //System.Drawing.Color.FromArgb(192, 192, 255),
                                           System.Drawing.Color.White,
                                           System.Drawing.Color.Empty,
                                           //System.Drawing.Color.FromArgb(231, 231, 255),
                                           System.Drawing.Color.White,
                                           true,
                                           true,
                                           false,
                                           true,
                                           false);

                spdCtl.BorderStyle = BorderStyle.FixedSingle;
                spdCtl.BackColor = System.Drawing.Color.Silver;
                spdCtl.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
                spdCtl.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
                spdCtl.MoveActiveOnFocus = false;

                //spdCtl.ColumnSplitBoxAlignment = FarPoint.Win.Spread.SplitBoxAlignment.Leading;
                //spdCtl.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.AsNeeded;

                //spdCtl.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;

                spdCtl.SetCursor(FarPoint.Win.Spread.CursorType.Normal, System.Windows.Forms.Cursors.Arrow);

                for (int i = 0; i < spdCtl.Sheets.Count; i++)
                {
                    //"Verdana", "Tahoma", "Times New Roman", "돋움"
                    Font myFonts = new Font("Tahoma", 8F, FontStyle.Bold);

                    spSkin.Apply(spdCtl.Sheets[i]);

                    //spdCtl.Sheets[i].RowCount = 0;
                    spdCtl.Sheets[i].OperationMode = FarPoint.Win.Spread.OperationMode.Normal;
                    spdCtl.Sheets[i].Columns[-1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                    spdCtl.Sheets[i].ColumnHeader.DefaultStyle.Font = myFonts;

                    spdCtl.Sheets[i].DefaultStyle.Font = myFonts;
                    spdCtl.Sheets[i].DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GridColumnInit()
        {
            try
            {
                udcFarPoint_Date.RPT_ColumnInit();
                udcFarPoint_Date.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                udcFarPoint_Date.RPT_AddBasicColumn("Pkg", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                if (dadt != null)
                {
                    for (int i = 0; i < dadt.Rows.Count; i++)
                    {
                        udcFarPoint_Date.RPT_AddBasicColumn("D" + dadt.Rows[i][0].ToString(), 0, udcFarPoint_Date.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Double2, 80);
                        udcFarPoint_Date.RPT_AddBasicColumn("TOT", 0, udcFarPoint_Date.ActiveSheet.Columns.Count, Visibles.False, Frozen.False, Align.Right, Merge.True, Formatter.Double2, 80);
                        udcFarPoint_Date.RPT_AddBasicColumn("CNT", 0, udcFarPoint_Date.ActiveSheet.Columns.Count, Visibles.False, Frozen.False, Align.Right, Merge.True, Formatter.Double2, 80);
                    }
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        private void GridColumnInit1()
        {
            try
            {
                udcFarPoint_Work.RPT_ColumnInit();
                udcFarPoint_Work.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                udcFarPoint_Work.RPT_AddBasicColumn("TAT", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                udcFarPoint_Work.RPT_AddBasicColumn("Input waiting", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                udcFarPoint_Work.RPT_AddBasicColumn("SAW", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                udcFarPoint_Work.RPT_AddBasicColumn("DA", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                udcFarPoint_Work.RPT_AddBasicColumn("WB", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                udcFarPoint_Work.RPT_AddBasicColumn("GATE", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                udcFarPoint_Work.RPT_AddBasicColumn("MOLD", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                udcFarPoint_Work.RPT_AddBasicColumn("FINISH", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                udcFarPoint_Work.RPT_AddBasicColumn("Wait for Shipment", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        private void GridColumnInit2()
        {
            try
            {
                udcFarPoint_Team.RPT_ColumnInit();
                udcFarPoint_Team.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                udcFarPoint_Team.RPT_AddBasicColumn("Production 1P", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                udcFarPoint_Team.RPT_AddBasicColumn("Production 2P", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                udcFarPoint_Team.RPT_AddBasicColumn("Production 3P", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                udcFarPoint_Team.RPT_AddBasicColumn("Production 4P", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }


        private string DateSqlString(string flag)
        {
            string GET_WORK_DATE = null;
            string sTo = null;
            DateTime TmpDate = DateTime.Parse(cdvDate.Value.ToString());
            string toDate = cdvDate.Value.ToString("yyyy-MM-dd");
            string sFrom = DateTime.Parse(toDate).AddDays(1 - TmpDate.Day).ToString("yyyyMMdd");
            if(flag == "menu"){
                sTo = DateTime.Parse(toDate).AddDays(-1).ToString("yyyyMMdd");
            }else{
                sTo = DateTime.Parse(toDate).ToString("yyyyMMdd");
            }


            StringBuilder strSqlString = new StringBuilder();

             GET_WORK_DATE = "SUBSTR(GET_WORK_DATE(SHIP_DATE,'D'),LENGTH(GET_WORK_DATE(SHIP_DATE,'D'))-3,4) AS TMPDATE, SHIP_DATE AS TMP2DATE";

            strSqlString.AppendFormat("SELECT DISTINCT " + GET_WORK_DATE + "\n");
            strSqlString.AppendFormat("FROM (" + "\n");
            strSqlString.AppendFormat("SELECT DISTINCT SHIP_DATE" + "\n");


            strSqlString.AppendFormat("FROM CSUMTATMAT@RPTTOMES TAT, MWIPMATDEF MAT " + "\n");
            strSqlString.AppendFormat("WHERE 1=1 " + "\n");
            strSqlString.AppendFormat("AND TAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.AppendFormat("AND TAT.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.AppendFormat("AND TAT.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.AppendFormat("AND MAT.MAT_VER = 1 " + "\n");
            strSqlString.AppendFormat(" AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
            strSqlString.AppendFormat(") ORDER BY TMPDATE" + "\n");



            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            return strSqlString.ToString();

       }

        private void rbtAssy_CheckedChanged(object sender, EventArgs e)
        {
            
            if (rbtAssy.Checked == true)
            {

                factory = GlobalVariable.gsAssyDefaultFactory;

                chdt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", ChartSqlString("btn"));
                ChartGride(chdt);

                tabPage1.Text = "Samsung(M)";
                tabPage2.Text = "HYNIX";
                tabPage3.Text = "Other(LSI)";
            }
            else
            {
                factory = "HMKE1";

                chdt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", ChartSqlString("btn"));
                ChartGride(chdt);

                tabPage1.Text = "Samsung(LSI)";
                tabPage2.Text = "HYNIX";
                tabPage3.Text = "Other(LSI)";

            }
        }

        #region Chart
        private string ChartSqlString(string flag)
        {
            string sTo = null;

            DateTime TmpDate = DateTime.Parse(cdvDate.Value.ToString());
            string toDate = cdvDate.Value.ToString("yyyy-MM-dd");
            string sFrom = DateTime.Parse(toDate).AddDays(1-TmpDate.Day).ToString("yyyyMMdd");
            if (flag == "menu")
            {
                sTo = DateTime.Parse(toDate).AddDays(-1).ToString("yyyyMMdd");
            }
            else
            {
                sTo = DateTime.Parse(toDate).ToString("yyyyMMdd");
            }

            StringBuilder strSqlString = new StringBuilder();
            strSqlString.AppendFormat("SELECT MAT_GRP_1, MAT_GRP_3" + "\n");
            for (int i = 0; i < dadt.Rows.Count; i++)
            {
                strSqlString.AppendFormat("   , SUM(TAT"+i+") AS TAT"+i+"\n");
                strSqlString.AppendFormat("   , SUM(TAT_QTY" + i + ") AS TAT_QTY" + i + "\n");
                strSqlString.AppendFormat("   , SUM(SHIP_QTY" + i + ") AS SHIP_QTY" + i + "\n");
            }
            
            strSqlString.AppendFormat("FROM (" + "\n");
            strSqlString.AppendFormat("   SELECT MAT_GRP_1, MAT_GRP_3" + "\n");
            for (int i = 0; i < dadt.Rows.Count; i++)
            {
                strSqlString.AppendFormat("       , DECODE(SHIP_DATE, '" + dadt.Rows[i]["TMP2DATE"].ToString() + "', ROUND(SUM(TAT_QTY)/SUM(SHIP_QTY),2)) AS TAT" + i + "\n");
                strSqlString.AppendFormat("       , DECODE(SHIP_DATE, '" + dadt.Rows[i]["TMP2DATE"].ToString() + "', ROUND(SUM(TAT_QTY),2)) AS TAT_QTY" + i + "\n");
                strSqlString.AppendFormat("       , DECODE(SHIP_DATE, '" + dadt.Rows[i]["TMP2DATE"].ToString() + "', SUM(SHIP_QTY)) AS SHIP_QTY" + i + "\n");
            }
            
            strSqlString.AppendFormat("   FROM (" + "\n");
            if (customer.Equals("OT"))
            {
                strSqlString.AppendFormat("       SELECT 'Others' AS MAT_GRP_1" + "\n");
            }
            else
            {
                strSqlString.AppendFormat("       SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1" + "\n");
            }
            strSqlString.AppendFormat("           , MAT.MAT_GRP_3, TAT.MAT_ID, TAT.SHIP_DATE" + "\n");
            strSqlString.AppendFormat("           , SUM(SHIP_QTY) AS SHIP_QTY" + "\n");
            strSqlString.AppendFormat("           , SUM(TOTAL_TAT_QTY) AS TAT_QTY" + "\n");
            strSqlString.AppendFormat("       FROM CSUMTATMAT@RPTTOMES TAT" + "\n");
            strSqlString.AppendFormat("           , MWIPMATDEF MAT" + "\n");
            strSqlString.AppendFormat("       WHERE 1=1" + "\n");
            strSqlString.AppendFormat("           AND TAT.FACTORY ='"+factory+"'" + "\n");
            strSqlString.AppendFormat("           AND MAT.FACTORY = '" + factory + "'" + "\n");
            strSqlString.AppendFormat("           AND TAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.AppendFormat("           AND TAT.MAT_ID=MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("           AND MAT.MAT_VER = 1" + "\n");
            if (customer.Equals("OT"))
            {
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 NOT IN ('SE', 'HX')" + "\n");
            }
            else
            {
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 = '" + customer + "'" + "\n");
            }
            strSqlString.AppendFormat("           AND SHIP_DATE BETWEEN '{0}' AND '{1}'" + "\n", sFrom, sTo);
            strSqlString.AppendFormat("       GROUP BY MAT_GRP_1, MAT.MAT_GRP_3, TAT.MAT_ID, TAT.SHIP_DATE" + "\n");
            strSqlString.AppendFormat("   )" + "\n");
            strSqlString.AppendFormat("   GROUP BY MAT_GRP_1, MAT_GRP_3, SHIP_DATE" + "\n");
            strSqlString.AppendFormat(")" + "\n");
            strSqlString.AppendFormat("GROUP BY MAT_GRP_1, MAT_GRP_3" + "\n");
            strSqlString.AppendFormat("ORDER BY DECODE(MAT_GRP_1, 'SAMSUNG', 1, 'Hynix', 2 , 'Others', 3)" + "\n");

            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            return strSqlString.ToString();
        }

        private void tabChart_Click(object sender, EventArgs e)
        {
            if (tabChart.SelectedTab == tabPage1)
            {
                customer = "SE";
                chdt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", ChartSqlString("btn"));
                ChartGride(chdt);
            }
            else if (tabChart.SelectedTab == tabPage2)
            {
                customer = "HX";
                chdt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", ChartSqlString("btn"));
                ChartGride(chdt);
            }
            else if (tabChart.SelectedTab == tabPage3)
            {
                customer = "OT";
                chdt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", ChartSqlString("btn"));
                ChartGride(chdt);
            }
        }


        private void ChartGride(DataTable chdt)
        {
            if (chdt.Rows.Count == 0)
            {
                chdt.Dispose();
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                return;
            }

            //1.Griid 합계 표시
            int[] rowType = udcFarPoint_Date.RPT_DataBindingWithSubTotal(chdt, 0, 1, 1);

            udcFarPoint_Date.RPT_FillDataSelectiveCells("Total", 0, 1, 0, 1, true, Align.Center, VerticalAlign.Center);
            udcFarPoint_Date.isShowZero = true;

            int count = 0;
            count = dadt.Rows.Count * 3;
            for (int i = 0; i < count; i += 3)
            {
                SetAvgVertical(1, 2 + i, true);
            }


            udcFarPoint_Date.RPT_AutoFit(false);

            //Chart 생성
            if (udcFarPoint_Date.ActiveSheet.RowCount > 0)
                if (customer == "SE")
                {
                    ShowChart_SE(0);
                }
                else if (customer == "HX")
                {
                    ShowChart_HX(0);
                }
                else if (customer == "OT")
                {
                    ShowChart_OT(0);
                }
        }


        public void SetAvgVertical(int nSampleNormalRowPos, int nColPos, bool bWithNull)
        {
            double sum = 0;
            double cnt = 0;            
            double totalSum = 0;
            double totalCnt = 0;
            double divide = 0;
            double totalDivide = 0;            

            Color color = udcFarPoint_Date.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;

            for (int i = 1; i < udcFarPoint_Date.ActiveSheet.Rows.Count; i++)
            {
                if (udcFarPoint_Date.ActiveSheet.Cells[i, nColPos].BackColor == color)
                {
                    sum += Convert.ToDouble(udcFarPoint_Date.ActiveSheet.Cells[i, nColPos + 1].Value);
                    cnt += Convert.ToDouble(udcFarPoint_Date.ActiveSheet.Cells[i, nColPos + 2].Value);
                    if (!bWithNull && (udcFarPoint_Date.ActiveSheet.Cells[i, nColPos + 1].Value == null || udcFarPoint_Date.ActiveSheet.Cells[i, nColPos + 1].Value.ToString().Trim() == ""))
                        continue;



                    divide += 1;
                }
                else
                {
                    if (divide != 0)
                    {
                        if (cnt == 0)
                        {
                            udcFarPoint_Date.ActiveSheet.Cells[i, nColPos].Value = sum / 1;
                        }
                        else
                        {
                            udcFarPoint_Date.ActiveSheet.Cells[i, nColPos].Value = sum / cnt;
                        }

                    }

                    totalSum += sum;
                    totalCnt += cnt;
                    totalDivide += divide;

                    sum = 0;
                    cnt = 0;
                    divide = 0;
                }
            }
            if (totalDivide != 0)
                if (totalCnt == 0)
                {
                    udcFarPoint_Date.ActiveSheet.Cells[0, nColPos].Value = totalSum / 1;
                }
                else
                {
                    udcFarPoint_Date.ActiveSheet.Cells[0, nColPos].Value = totalSum / totalCnt;
                }

            return;
        }


        #region ShowChart

        private void ShowChart_SE(int RowCount)
        {
            // 차트 설정
            udcChartFX_SE.RPT_2_ClearData();
            udcChartFX_SE.RPT_3_OpenData(1, dadt.Rows.Count);
            int[] total_columns = new Int32[dadt.Rows.Count];
            int[] columnsHeader = new Int32[dadt.Rows.Count];
            int columns = 0;
            for (int i = 0; i < dadt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    total_columns[i] = 2 + i;
                    columnsHeader[i] = 2 + i;
                }
                else
                {
                    total_columns[i] = 2 + columns + 3;
                    columnsHeader[i] = 2 + columns + 3;
                    columns += 3;
                }
            }

            double max1 = udcChartFX_SE.RPT_4_AddData(udcFarPoint_Date, new int[] { RowCount + 0 }, total_columns, SeriseType.Rows, DataTypes.Initeger);

            udcChartFX_SE.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            udcChartFX_SE.RPT_6_SetGallery(ChartType.Line, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);

            udcChartFX_SE.RPT_7_SetXAsixTitleUsingSpreadHeader(udcFarPoint_Date, 0, columnsHeader);

            udcChartFX_SE.PointLabels = true;
            udcChartFX_SE.AxisY.Gridlines = false;
            udcChartFX_SE.AxisY.DataFormat.Decimals = 2;
        }
        private void ShowChart_HX(int RowCount)
        {
            // 차트 설정
            udcChartFX_HX.RPT_2_ClearData();
            udcChartFX_HX.RPT_3_OpenData(1, dadt.Rows.Count);
            int[] total_columns = new Int32[dadt.Rows.Count];
            int[] columnsHeader = new Int32[dadt.Rows.Count];
            int columns = 0;
            for (int i = 0; i < dadt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    total_columns[i] = 2 + i;
                    columnsHeader[i] = 2 + i;
                }
                else
                {
                    total_columns[i] = 2 + columns + 3;
                    columnsHeader[i] = 2 + columns + 3;
                    columns += 3;
                }
            }

            double max1 = udcChartFX_HX.RPT_4_AddData(udcFarPoint_Date, new int[] { RowCount + 0 }, total_columns, SeriseType.Rows, DataTypes.Initeger);

            udcChartFX_HX.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            udcChartFX_HX.RPT_6_SetGallery(ChartType.Line, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);

            udcChartFX_HX.RPT_7_SetXAsixTitleUsingSpreadHeader(udcFarPoint_Date, 0, columnsHeader);

            udcChartFX_HX.PointLabels = true;
            udcChartFX_HX.AxisY.Gridlines = false;
            udcChartFX_HX.AxisY.DataFormat.Decimals = 2;
        }
        private void ShowChart_OT(int RowCount)
        {
            // 차트 설정
            udcChartFX_OT.RPT_2_ClearData();
            udcChartFX_OT.RPT_3_OpenData(1, dadt.Rows.Count);
            int[] total_columns = new Int32[dadt.Rows.Count];
            int[] columnsHeader = new Int32[dadt.Rows.Count];
            int columns = 0;
            for (int i = 0; i < dadt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    total_columns[i] = 2 + i;
                    columnsHeader[i] = 2 + i;
                }
                else
                {
                    total_columns[i] = 2 + columns + 3;
                    columnsHeader[i] = 2 + columns + 3;
                    columns += 3;
                }
            }

            double max1 = udcChartFX_OT.RPT_4_AddData(udcFarPoint_Date, new int[] { RowCount + 0 }, total_columns, SeriseType.Rows, DataTypes.Initeger);

            udcChartFX_OT.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            udcChartFX_OT.RPT_6_SetGallery(ChartType.Line, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);

            udcChartFX_OT.RPT_7_SetXAsixTitleUsingSpreadHeader(udcFarPoint_Date, 0, columnsHeader);

            udcChartFX_OT.PointLabels = true;
            udcChartFX_OT.AxisY.Gridlines = false;
            udcChartFX_OT.AxisY.DataFormat.Decimals = 2;
        }
        #endregion
        #endregion

        private string MakeSqlString(string flag)
        {

            string sTo = null;
            DateTime TmpDate = DateTime.Parse(cdvDate.Value.ToString());
            string toDate = cdvDate.Value.ToString("yyyy-MM-dd");
            string sFrom = DateTime.Parse(toDate).AddDays(1 - TmpDate.Day).ToString("yyyyMMdd");
            if (flag == "menu")
            {
                sTo = DateTime.Parse(toDate).AddDays(-1).ToString("yyyyMMdd");
            }
            else
            {
                sTo = DateTime.Parse(toDate).ToString("yyyyMMdd");
            }


            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1,  " + "\n");
            strSqlString.AppendFormat("   DATA_2, DATA_TOT, ROUND(DATA_2/DATA_TOT*100,2) AS PERSENT, '',  DATA_TODAY" + "\n");
            strSqlString.AppendFormat("   FROM " + "\n");
            strSqlString.AppendFormat("( " + "\n");
            strSqlString.AppendFormat("SELECT MAT_GRP_1, FACTORY, DATA_TODAY " + "\n");
            strSqlString.AppendFormat(", ROUND(SUM(TAT_QTY)/SUM(SHIP_QTY),2) AS DATA_TOT " + "\n");
            strSqlString.AppendFormat("FROM( " + "\n");
            strSqlString.AppendFormat("       SELECT  MAT.MAT_GRP_1, TAT.MAT_ID, TAT.FACTORY, DATA_TODAY, TAT.SHIP_DATE" + "\n");
            strSqlString.AppendFormat("           , SUM(TAT.SHIP_QTY) AS SHIP_QTY " + "\n");
            strSqlString.AppendFormat("           , SUM(TAT.TOTAL_TAT_QTY) AS TAT_QTY " + "\n");
            strSqlString.AppendFormat("           FROM    CSUMTATMAT@RPTTOMES TAT," + "\n");
            strSqlString.AppendFormat("           MWIPMATDEF MAT, " + "\n");
            strSqlString.AppendFormat("           ( " + "\n");
            strSqlString.AppendFormat("           SELECT MAT_GRP_1, FACTORY, ROUND(SUM(TAT_QTY)/SUM(SHIP_QTY),2) AS DATA_TODAY  " + "\n");
            strSqlString.AppendFormat("           FROM( " + "\n");
            strSqlString.AppendFormat("           SELECT MAT.MAT_GRP_1, TAT.MAT_ID, TAT.FACTORY " + "\n");
            strSqlString.AppendFormat("             , SUM(TAT.SHIP_QTY) AS SHIP_QTY " + "\n");
            strSqlString.AppendFormat("             , SUM(TAT.TOTAL_TAT_QTY) AS TAT_QTY " + "\n");
            strSqlString.AppendFormat("           FROM " + "\n");
            strSqlString.AppendFormat("             CSUMTATMAT@RPTTOMES TAT," + "\n");
            strSqlString.AppendFormat("             MWIPMATDEF MAT" + "\n");
            strSqlString.AppendFormat("           WHERE 1=1" + "\n");
            strSqlString.AppendFormat("               AND TAT.FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("               AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("               AND TAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.AppendFormat("               AND TAT.MAT_ID=MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("               AND MAT_VER = 1" + "\n");
            strSqlString.AppendFormat("               AND MAT.MAT_GRP_1 IN('SE', 'HX')" + "\n");
            strSqlString.AppendFormat("               AND SHIP_DATE BETWEEN '{0}' AND '{1}'" + "\n", sTo, sTo);
            strSqlString.AppendFormat("               GROUP BY MAT_GRP_1,TAT.FACTORY, TAT.MAT_ID " + "\n");
            strSqlString.AppendFormat("           ) " + "\n");
            strSqlString.AppendFormat("           GROUP BY MAT_GRP_1, FACTORY " + "\n");
            strSqlString.AppendFormat("           ) TODAY" + "\n");
            strSqlString.AppendFormat("       WHERE   1=1" + "\n");
            strSqlString.AppendFormat("           AND TAT.FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("           AND TAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.AppendFormat("           AND TAT.FACTORY = TODAY.FACTORY" + "\n");
            strSqlString.AppendFormat("           AND TAT.MAT_ID=MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("           AND MAT_VER = 1" + "\n");
            strSqlString.AppendFormat("           AND TODAY.MAT_GRP_1 = MAT.MAT_GRP_1" + "\n");
            strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 IN('SE', 'HX')" + "\n");
            strSqlString.AppendFormat("           AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
            strSqlString.AppendFormat("         GROUP BY MAT.MAT_GRP_1,TAT.MAT_ID, TAT.FACTORY, DATA_TODAY, TAT.SHIP_DATE" + "\n");
            strSqlString.AppendFormat("         )" + "\n");
            strSqlString.AppendFormat("       GROUP BY MAT_GRP_1,FACTORY, DATA_TODAY" + "\n");
            strSqlString.AppendFormat("       UNION ALL " + "\n");
            strSqlString.AppendFormat("       SELECT  MAT_GRP_1, FACTORY, DATA_TODAY, ROUND(SUM(TAT_QTY)/SUM(SHIP_QTY),2) " + "\n");
            strSqlString.AppendFormat("       FROM( " + "\n");
            strSqlString.AppendFormat("       SELECT  TODAY.MAT_GRP_1, TAT.MAT_ID, TAT.FACTORY, DATA_TODAY, TAT.SHIP_DATE " + "\n");
            strSqlString.AppendFormat("             , SUM(TAT.SHIP_QTY) AS SHIP_QTY " + "\n");
            strSqlString.AppendFormat("             , SUM(TAT.TOTAL_TAT_QTY) AS TAT_QTY " + "\n");
            strSqlString.AppendFormat("           FROM    CSUMTATMAT@RPTTOMES TAT," + "\n");
            strSqlString.AppendFormat("           MWIPMATDEF MAT, " + "\n");
            strSqlString.AppendFormat("           ( " + "\n" );
            strSqlString.AppendFormat("           SELECT MAT_GRP_1, FACTORY, ROUND(SUM(TAT_QTY)/SUM(SHIP_QTY),2) AS DATA_TODAY" + "\n");
            strSqlString.AppendFormat("           FROM( " + "\n");
            strSqlString.AppendFormat("           SELECT 'Other' AS MAT_GRP_1, TAT.MAT_ID, TAT.FACTORY " + "\n");
            strSqlString.AppendFormat("                 , SUM(TAT.SHIP_QTY) AS SHIP_QTY " + "\n");
            strSqlString.AppendFormat("                 , SUM(TAT.TOTAL_TAT_QTY) AS TAT_QTY " + "\n");
            strSqlString.AppendFormat("           FROM " + "\n");
            strSqlString.AppendFormat("             CSUMTATMAT@RPTTOMES TAT," + "\n");
            strSqlString.AppendFormat("             MWIPMATDEF MAT" + "\n");
            strSqlString.AppendFormat("           WHERE 1=1" + "\n");
            strSqlString.AppendFormat("               AND TAT.FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("               AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("               AND TAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.AppendFormat("               AND TAT.MAT_ID=MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("               AND MAT_VER = 1" + "\n");
            strSqlString.AppendFormat("               AND MAT.MAT_GRP_1 NOT IN('SE', 'HX')" + "\n");
            strSqlString.AppendFormat("               AND SHIP_DATE BETWEEN '{0}' AND '{1}'" + "\n", sTo, sTo);
            strSqlString.AppendFormat("               GROUP BY 'Other',TAT.MAT_ID, TAT.FACTORY " + "\n");
            strSqlString.AppendFormat("               ) " + "\n");
            strSqlString.AppendFormat("               GROUP BY MAT_GRP_1, FACTORY" + "\n");
            strSqlString.AppendFormat("               ) TODAY" + "\n");
            strSqlString.AppendFormat("       WHERE   1=1" + "\n");
            strSqlString.AppendFormat("           AND TAT.FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("           AND TAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.AppendFormat("           AND TAT.FACTORY = TODAY.FACTORY" + "\n");
            strSqlString.AppendFormat("           AND TAT.MAT_ID=MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("           AND MAT_VER = 1" + "\n");
            strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 NOT IN('SE', 'HX')" + "\n");
            strSqlString.AppendFormat("           AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
            strSqlString.AppendFormat("       GROUP BY TODAY.MAT_GRP_1,TAT.MAT_ID, TAT.FACTORY, DATA_TODAY, TAT.SHIP_DATE" + "\n");
            strSqlString.AppendFormat("   ) " + "\n");
            strSqlString.AppendFormat("   GROUP BY MAT_GRP_1,FACTORY, DATA_TODAY" + "\n");
            strSqlString.AppendFormat("   ) TOT" + "\n");
            strSqlString.AppendFormat("   , MGCMTBLDAT GCM" + "\n");
            strSqlString.AppendFormat("WHERE 1=1" + "\n");
            strSqlString.AppendFormat("   AND GCM.FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("   AND GCM.FACTORY = TOT.FACTORY" + "\n");
            strSqlString.AppendFormat("   AND GCM.TABLE_NAME='H_RPT_TAT_MAINOBJECT'" + "\n");
            strSqlString.AppendFormat("   AND GCM.KEY_1 = (SELECT MAX(KEY_1) FROM MGCMTBLDAT WHERE FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME='H_RPT_TAT_MAINOBJECT')" + "\n");
            strSqlString.AppendFormat("   AND TOT.MAT_GRP_1 = GCM.KEY_2" + "\n");
            strSqlString.AppendFormat("ORDER BY DECODE(MAT_GRP_1, 'SAMSUNG', 1, 'Hynix', 2 , 'Others', 3)" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private string MakeSqlString2(string flag)
        {
            string sTo = null;
            DateTime TmpDate = DateTime.Parse(cdvDate.Value.ToString());
            string toDate = cdvDate.Value.ToString("yyyy-MM-dd");
            string sFrom = DateTime.Parse(toDate).AddDays(1-TmpDate.Day).ToString("yyyyMMdd");
            if (flag == "menu")
            {
                sTo = DateTime.Parse(toDate).AddDays(-1).ToString("yyyyMMdd");
            }
            else
            {
                sTo = DateTime.Parse(toDate).ToString("yyyyMMdd");
            }

            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1,  " + "\n");
            strSqlString.AppendFormat("   DATA_2, DATA_TOT, ROUND(DATA_2/DATA_TOT*100,2) AS PERSENT, '',  DATA_TODAY" + "\n");
            strSqlString.AppendFormat("   FROM " + "\n");
            strSqlString.AppendFormat("( " + "\n");
            strSqlString.AppendFormat("SELECT MAT_GRP_1, FACTORY, DATA_TODAY " + "\n");
            strSqlString.AppendFormat(", ROUND(SUM(TAT_QTY)/SUM(SHIP_QTY),2) AS DATA_TOT " + "\n");
            strSqlString.AppendFormat("FROM( " + "\n");
            strSqlString.AppendFormat("       SELECT  MAT.MAT_GRP_1, TAT.MAT_ID, TAT.FACTORY, DATA_TODAY, TAT.SHIP_DATE" + "\n");
            strSqlString.AppendFormat("           , SUM(TAT.SHIP_QTY) AS SHIP_QTY " + "\n");
            strSqlString.AppendFormat("           , SUM(TAT.TOTAL_TAT_QTY) AS TAT_QTY " + "\n");
            strSqlString.AppendFormat("           FROM    CSUMTATMAT@RPTTOMES TAT," + "\n");
            strSqlString.AppendFormat("           MWIPMATDEF MAT, " + "\n");
            strSqlString.AppendFormat("           ( " + "\n");
            strSqlString.AppendFormat("           SELECT MAT_GRP_1, FACTORY, ROUND(SUM(TAT_QTY)/SUM(SHIP_QTY),2) AS DATA_TODAY  " + "\n");
            strSqlString.AppendFormat("           FROM( " + "\n");
            strSqlString.AppendFormat("           SELECT MAT.MAT_GRP_1, TAT.MAT_ID, TAT.FACTORY " + "\n");
            strSqlString.AppendFormat("             , SUM(TAT.SHIP_QTY) AS SHIP_QTY " + "\n");
            strSqlString.AppendFormat("             , SUM(TAT.TOTAL_TAT_QTY) AS TAT_QTY " + "\n");
            strSqlString.AppendFormat("           FROM " + "\n");
            strSqlString.AppendFormat("             CSUMTATMAT@RPTTOMES TAT," + "\n");
            strSqlString.AppendFormat("             MWIPMATDEF MAT" + "\n");
            strSqlString.AppendFormat("           WHERE 1=1" + "\n");
            strSqlString.AppendFormat("               AND TAT.FACTORY ='HMKE1'" + "\n");
            strSqlString.AppendFormat("               AND MAT.FACTORY = 'HMKE1'" + "\n");
            strSqlString.AppendFormat("               AND TAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.AppendFormat("               AND TAT.MAT_ID=MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("               AND MAT_VER = 1" + "\n");
            strSqlString.AppendFormat("               AND MAT.MAT_GRP_1 IN('SE', 'HX')" + "\n");
            strSqlString.AppendFormat("               AND SHIP_DATE BETWEEN '{0}' AND '{1}'" + "\n", sTo, sTo);
            strSqlString.AppendFormat("               GROUP BY MAT_GRP_1,TAT.FACTORY, TAT.MAT_ID " + "\n");
            strSqlString.AppendFormat("           ) " + "\n");
            strSqlString.AppendFormat("           GROUP BY MAT_GRP_1, FACTORY " + "\n");
            strSqlString.AppendFormat("           ) TODAY" + "\n");
            strSqlString.AppendFormat("       WHERE   1=1" + "\n");
            strSqlString.AppendFormat("           AND TAT.FACTORY ='HMKE1'" + "\n");
            strSqlString.AppendFormat("           AND MAT.FACTORY = 'HMKE1'" + "\n");
            strSqlString.AppendFormat("           AND TAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.AppendFormat("           AND TAT.FACTORY = TODAY.FACTORY" + "\n");
            strSqlString.AppendFormat("           AND TAT.MAT_ID=MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("           AND MAT_VER = 1" + "\n");
            strSqlString.AppendFormat("           AND TODAY.MAT_GRP_1 = MAT.MAT_GRP_1" + "\n");
            strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 IN('SE', 'HX')" + "\n");
            strSqlString.AppendFormat("           AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
            strSqlString.AppendFormat("         GROUP BY MAT.MAT_GRP_1,TAT.MAT_ID, TAT.FACTORY, DATA_TODAY, TAT.SHIP_DATE" + "\n");
            strSqlString.AppendFormat("         )" + "\n");
            strSqlString.AppendFormat("       GROUP BY MAT_GRP_1,FACTORY, DATA_TODAY" + "\n");
            strSqlString.AppendFormat("       UNION ALL " + "\n");
            strSqlString.AppendFormat("       SELECT  MAT_GRP_1, FACTORY, DATA_TODAY, ROUND(SUM(TAT_QTY)/SUM(SHIP_QTY),2) " + "\n");
            strSqlString.AppendFormat("       FROM( " + "\n");
            strSqlString.AppendFormat("       SELECT  TODAY.MAT_GRP_1, TAT.MAT_ID, TAT.FACTORY, DATA_TODAY, TAT.SHIP_DATE " + "\n");
            strSqlString.AppendFormat("             , SUM(TAT.SHIP_QTY) AS SHIP_QTY " + "\n");
            strSqlString.AppendFormat("             , SUM(TAT.TOTAL_TAT_QTY) AS TAT_QTY " + "\n");
            strSqlString.AppendFormat("           FROM    CSUMTATMAT@RPTTOMES TAT," + "\n");
            strSqlString.AppendFormat("           MWIPMATDEF MAT, " + "\n");
            strSqlString.AppendFormat("           ( " + "\n");
            strSqlString.AppendFormat("           SELECT MAT_GRP_1, FACTORY, ROUND(SUM(TAT_QTY)/SUM(SHIP_QTY),2) AS DATA_TODAY" + "\n");
            strSqlString.AppendFormat("           FROM( " + "\n");
            strSqlString.AppendFormat("           SELECT 'Other' AS MAT_GRP_1, TAT.MAT_ID, TAT.FACTORY " + "\n");
            strSqlString.AppendFormat("                 , SUM(TAT.SHIP_QTY) AS SHIP_QTY " + "\n");
            strSqlString.AppendFormat("                 , SUM(TAT.TOTAL_TAT_QTY) AS TAT_QTY " + "\n");
            strSqlString.AppendFormat("           FROM " + "\n");
            strSqlString.AppendFormat("             CSUMTATMAT@RPTTOMES TAT," + "\n");
            strSqlString.AppendFormat("             MWIPMATDEF MAT" + "\n");
            strSqlString.AppendFormat("           WHERE 1=1" + "\n");
            strSqlString.AppendFormat("               AND TAT.FACTORY ='HMKE1'" + "\n");
            strSqlString.AppendFormat("               AND MAT.FACTORY = 'HMKE1'" + "\n");
            strSqlString.AppendFormat("               AND TAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.AppendFormat("               AND TAT.MAT_ID=MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("               AND MAT_VER = 1" + "\n");
            strSqlString.AppendFormat("               AND MAT.MAT_GRP_1 NOT IN('SE', 'HX')" + "\n");
            strSqlString.AppendFormat("               AND SHIP_DATE BETWEEN '{0}' AND '{1}'" + "\n", sTo, sTo);
            strSqlString.AppendFormat("               GROUP BY 'Other',TAT.MAT_ID, TAT.FACTORY " + "\n");
            strSqlString.AppendFormat("               ) " + "\n");
            strSqlString.AppendFormat("               GROUP BY MAT_GRP_1, FACTORY" + "\n");
            strSqlString.AppendFormat("               ) TODAY" + "\n");
            strSqlString.AppendFormat("       WHERE   1=1" + "\n");
            strSqlString.AppendFormat("           AND TAT.FACTORY ='HMKE1'" + "\n");
            strSqlString.AppendFormat("           AND MAT.FACTORY = 'HMKE1'" + "\n");
            strSqlString.AppendFormat("           AND TAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.AppendFormat("           AND TAT.FACTORY = TODAY.FACTORY" + "\n");
            strSqlString.AppendFormat("           AND TAT.MAT_ID=MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("           AND MAT_VER = 1" + "\n");
            strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 NOT IN('SE', 'HX')" + "\n");
            strSqlString.AppendFormat("           AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
            strSqlString.AppendFormat("       GROUP BY TODAY.MAT_GRP_1,TAT.MAT_ID, TAT.FACTORY, DATA_TODAY, TAT.SHIP_DATE" + "\n");
            strSqlString.AppendFormat("   ) " + "\n");
            strSqlString.AppendFormat("   GROUP BY MAT_GRP_1,FACTORY, DATA_TODAY" + "\n");
            strSqlString.AppendFormat("   ) TOT" + "\n");
            strSqlString.AppendFormat("   , MGCMTBLDAT GCM" + "\n");
            strSqlString.AppendFormat("WHERE 1=1" + "\n");
            strSqlString.AppendFormat("   AND GCM.FACTORY ='HMKE1'" + "\n");
            strSqlString.AppendFormat("   AND GCM.FACTORY = TOT.FACTORY" + "\n");
            strSqlString.AppendFormat("   AND GCM.TABLE_NAME='H_RPT_TAT_MAINOBJECT'" + "\n");
            strSqlString.AppendFormat("   AND GCM.KEY_1 = (SELECT MAX(KEY_1) FROM MGCMTBLDAT WHERE FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME='H_RPT_TAT_MAINOBJECT')" + "\n");
            strSqlString.AppendFormat("   AND TOT.MAT_GRP_1 = GCM.KEY_2" + "\n");
            strSqlString.AppendFormat("ORDER BY DECODE(MAT_GRP_1, 'SAMSUNG', 1, 'Hynix', 2 , 'Others', 3)" + "\n");

            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            return strSqlString.ToString();
        }

        private string WorkSqlString(string flag)
        {
            DateTime TmpDate = DateTime.Parse(cdvDate.Value.ToString());
            string toDate = cdvDate.Value.ToString("yyyy-MM-dd");
            string sFrom = DateTime.Parse(toDate).AddDays(-1).ToString("yyyyMMdd");
            string sTo = DateTime.Parse(toDate).AddDays(-1).ToString("yyyyMMdd");

            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("SELECT CASE WHEN (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) = 'SAMSUNG' THEN 'SAMSUNG'" + "\n");
            strSqlString.AppendFormat("            WHEN (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) = 'Hynix' THEN 'Hynix'" + "\n");
            strSqlString.AppendFormat("            ELSE 'Others'" + "\n");
            strSqlString.AppendFormat("            END AS MAT_GRP_1" + "\n");
            strSqlString.AppendFormat("            , TOT_TAT, HMK2A, SAW, DA, WB, GATE, MOLD, FINISH, HMK3A" + "\n");
            strSqlString.AppendFormat("FROM (" + "\n");
            strSqlString.AppendFormat(" SELECT TAT.MAT_GRP_1" + "\n");
            strSqlString.AppendFormat("     ,ROUND(TAT.TOT_TAT/SHIP.SHIP_QTY,2) AS TOT_TAT " + "\n");
            strSqlString.AppendFormat("     ,HMK2A AS HMK2A" + "\n");
            strSqlString.AppendFormat("     ,SAW AS SAW" + "\n");
            strSqlString.AppendFormat("     ,DA AS DA" + "\n");
            strSqlString.AppendFormat("     ,WB AS WB" + "\n");
            strSqlString.AppendFormat("     ,GATE AS GATE" + "\n");
            strSqlString.AppendFormat("     ,MOLD AS MOLD" + "\n");
            strSqlString.AppendFormat("     ,FINISH AS FINISH" + "\n");
            strSqlString.AppendFormat("     ,HMK3A AS HMK3A" + "\n");
            strSqlString.AppendFormat(" FROM (" + "\n");
            strSqlString.AppendFormat("     SELECT MAT_GRP_1" + "\n");
            strSqlString.AppendFormat("         ,SUM(TAT_QTY) AS TOT_TAT" + "\n");
            strSqlString.AppendFormat("         ,ROUND(SUM(HMK2A),2) AS HMK2A" + "\n");
            strSqlString.AppendFormat("         ,ROUND(SUM(SAW),2) AS SAW" + "\n");
            strSqlString.AppendFormat("         ,ROUND(SUM(DA),2) AS DA" + "\n");
            strSqlString.AppendFormat("         ,ROUND(SUM(WB),2) AS WB" + "\n");
            strSqlString.AppendFormat("         ,ROUND(SUM(GATE),2) AS GATE" + "\n");
            strSqlString.AppendFormat("         ,ROUND(SUM(MOLD),2) AS MOLD" + "\n");
            strSqlString.AppendFormat("         ,ROUND(SUM(FINISH),2) AS FINISH" + "\n");
            strSqlString.AppendFormat("         ,ROUND(SUM(HMK3A),2) AS HMK3A" + "\n");
            strSqlString.AppendFormat("     FROM(" + "\n");
            strSqlString.AppendFormat("             SELECT TAT.MAT_GRP_1, TAT.OPER_GRP_1" + "\n");
            strSqlString.AppendFormat("                , SUM(TAT.TAT_QTY) AS TAT_QTY" + "\n");
            strSqlString.AppendFormat("                , SUM(TAT.SHIP_QTY) AS SHIP_QTY" + "\n");
            strSqlString.AppendFormat("                , DECODE(OPER_GRP_1,'HMK2A',SUM(TAT.TAT_QTY)/SUM(SHIP_QTY)) AS HMK2A" + "\n");
            strSqlString.AppendFormat("                , DECODE(OPER_GRP_1,'SAW',SUM(TAT.TAT_QTY)/SUM(SHIP_QTY)) AS SAW" + "\n");
            strSqlString.AppendFormat("                , DECODE(OPER_GRP_1,'D/A',SUM(TAT.TAT_QTY)/SUM(SHIP_QTY)) AS DA" + "\n");
            strSqlString.AppendFormat("                , DECODE(OPER_GRP_1,'W/B',SUM(TAT.TAT_QTY)/SUM(SHIP_QTY)) AS WB" + "\n");
            strSqlString.AppendFormat("                , DECODE(OPER_GRP_1,'GATE',SUM(TAT.TAT_QTY)/SUM(SHIP_QTY)) AS GATE" + "\n");
            strSqlString.AppendFormat("                , DECODE(OPER_GRP_1,'MOLD',SUM(TAT.TAT_QTY)/SUM(SHIP_QTY)) AS MOLD" + "\n");
            strSqlString.AppendFormat("                , DECODE(OPER_GRP_1,'FINISH',SUM(TAT.TAT_QTY)/SUM(SHIP_QTY)) AS FINISH" + "\n");
            strSqlString.AppendFormat("                , DECODE(OPER_GRP_1,'HMK3A',SUM(TAT.TAT_QTY)/SUM(SHIP_QTY)) AS HMK3A" + "\n");
            strSqlString.AppendFormat("             FROM(" + "\n");
            strSqlString.AppendFormat("                 SELECT TAT.FACTORY, MAT.MAT_GRP_1," + "\n");
            strSqlString.AppendFormat("                     TAT.OPER_GRP_1," + "\n");
            strSqlString.AppendFormat("                     SUM(TAT.TAT_QTY) AS TAT_QTY," + "\n");
            strSqlString.AppendFormat("                     SUM(TAT.SHIP_QTY) AS SHIP_QTY" + "\n");
            strSqlString.AppendFormat("                 FROM(" + "\n");
            strSqlString.AppendFormat("                     SELECT TAT.FACTORY, OPER " + "\n");
            strSqlString.AppendFormat("                            , CASE WHEN OPER >= 'A0000' AND OPER <= 'A000N' THEN 'HMK2A'" + "\n");
            strSqlString.AppendFormat("                                 WHEN OPER >= 'A0020' AND OPER <= 'A0390' THEN 'SAW'" + "\n");
            strSqlString.AppendFormat("                                 WHEN OPER >= 'A0400' AND OPER <= 'A0540' THEN 'D/A'" + "\n");
            strSqlString.AppendFormat("                                 WHEN OPER >= 'A0550' AND OPER <= 'A0650' THEN 'W/B'" + "\n");
            strSqlString.AppendFormat("                                 WHEN OPER >= 'A0670' AND OPER <= 'A0809' THEN 'GATE'" + "\n");
            strSqlString.AppendFormat("                                 WHEN OPER >= 'A0950' AND OPER <= 'A1100' THEN 'MOLD'" + "\n");
            strSqlString.AppendFormat("                                 WHEN OPER >= 'A1110' AND OPER <= 'A2300' THEN 'FINISH'" + "\n");
            strSqlString.AppendFormat("                                 WHEN OPER >= 'AZ009' AND OPER <= 'AZ010' THEN 'HMK3A'" + "\n");
            strSqlString.AppendFormat("                             END OPER_GRP_1" + "\n");
            strSqlString.AppendFormat("                         , TAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("                         , SUM(TAT.TOTAL_TAT_QTY) AS TAT_QTY" + "\n");
            strSqlString.AppendFormat("                         , SUM(SHIP.SHIP_QTY) AS SHIP_QTY" + "\n");
            strSqlString.AppendFormat("                     FROM CSUMTATMAT@RPTTOMES TAT, " + "\n");
            strSqlString.AppendFormat("                     (" + "\n");
            strSqlString.AppendFormat("                         SELECT FACTORY, MAT_ID," + "\n");
            strSqlString.AppendFormat("                             SHIP_QTY" + "\n");
            strSqlString.AppendFormat("                         FROM CSUMTATMAT@RPTTOMES TAT" + "\n");
            strSqlString.AppendFormat("                         WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                             AND TAT.FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("                             AND (TAT.MAT_ID LIKE 'SE%' OR TAT.MAT_ID LIKE 'HX%')" + "\n");
            strSqlString.AppendFormat("                             AND OPER ='AZ010'" + "\n");
            strSqlString.AppendFormat("                             AND TAT.SHIP_DATE BETWEEN '" + sFrom + "' AND '" + sTo + "'" + "\n", sFrom, sTo);
            strSqlString.AppendFormat("                     ) SHIP" + "\n");
            strSqlString.AppendFormat("                     WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                         AND TAT.FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("                         AND TAT.FACTORY = SHIP.FACTORY" + "\n");
            strSqlString.AppendFormat("                         AND TAT.MAT_ID = SHIP.MAT_ID" + "\n");
            strSqlString.AppendFormat("                         AND (TAT.MAT_ID LIKE 'SE%' OR TAT.MAT_ID LIKE 'HX%')" + "\n");
            strSqlString.AppendFormat("                         AND TAT.SHIP_DATE BETWEEN '" + sFrom + "' AND '" + sTo + "'" + "\n", sFrom, sTo);
            strSqlString.AppendFormat("                     GROUP BY TAT.FACTORY,OPER,TAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("                 ) TAT," + "\n");
            strSqlString.AppendFormat("                 MWIPOPRDEF OPR," + "\n");
            strSqlString.AppendFormat("                 MWIPMATDEF MAT" + "\n");
            strSqlString.AppendFormat("             WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                 AND TAT.FACTORY = OPR.FACTORY" + "\n");
            strSqlString.AppendFormat("                 AND TAT.OPER = OPR.OPER" + "\n");
            strSqlString.AppendFormat("                 AND TAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.AppendFormat("                 AND TAT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("                 AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.AppendFormat("              GROUP BY TAT.FACTORY, MAT.MAT_GRP_1, TAT.OPER_GRP_1" + "\n");
            strSqlString.AppendFormat("              ) TAT" + "\n");
            strSqlString.AppendFormat("           GROUP BY MAT_GRP_1, OPER_GRP_1" + "\n");
            strSqlString.AppendFormat("         ) GROUP BY MAT_GRP_1" + "\n");
            strSqlString.AppendFormat("         ) TAT," + "\n");
            strSqlString.AppendFormat("         (" + "\n");
            strSqlString.AppendFormat("             SELECT MAT_GRP_1," + "\n");
            strSqlString.AppendFormat("                 SUM(SHIP_QTY) AS SHIP_QTY" + "\n");
            strSqlString.AppendFormat("             FROM CSUMTATMAT@RPTTOMES TAT," + "\n");
            strSqlString.AppendFormat("                 MWIPMATDEF MAT" + "\n");
            strSqlString.AppendFormat("             WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                 AND TAT.FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("                 AND TAT.FACTORY=MAT.FACTORY" + "\n");
            strSqlString.AppendFormat("                 AND TAT.MAT_ID=MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("                 AND (TAT.MAT_ID LIKE 'SE%' OR TAT.MAT_ID LIKE 'HX%')" + "\n");
            strSqlString.AppendFormat("                 AND OPER ='AZ010'" + "\n");
            strSqlString.AppendFormat("                 AND TAT.SHIP_DATE BETWEEN '" + sFrom + "' AND '" + sTo + "'" + "\n", sFrom, sTo);
            strSqlString.AppendFormat("             GROUP BY MAT_GRP_1" + "\n");
            strSqlString.AppendFormat("         ) SHIP" + "\n");
            strSqlString.AppendFormat("         WHERE 1=1" + "\n");
            strSqlString.AppendFormat("             AND TAT.MAT_GRP_1 = SHIP.MAT_GRP_1" + "\n");
            strSqlString.AppendFormat("         UNION ALL" + "\n");
            strSqlString.AppendFormat(" SELECT TAT.MAT_GRP_1" + "\n");
            strSqlString.AppendFormat("     ,ROUND(TAT.TOT_TAT/SHIP.SHIP_QTY,2) AS TOT_TAT " + "\n");
            strSqlString.AppendFormat("     ,HMK2A AS HMK2A" + "\n");
            strSqlString.AppendFormat("     ,SAW AS SAW" + "\n");
            strSqlString.AppendFormat("     ,DA AS DA" + "\n");
            strSqlString.AppendFormat("     ,WB AS WB" + "\n");
            strSqlString.AppendFormat("     ,GATE AS GATE" + "\n");
            strSqlString.AppendFormat("     ,MOLD AS MOLD" + "\n");
            strSqlString.AppendFormat("     ,FINISH AS FINISH" + "\n");
            strSqlString.AppendFormat("     ,HMK3A AS HMK3A" + "\n");
            strSqlString.AppendFormat(" FROM (" + "\n");
            strSqlString.AppendFormat("             SELECT MAT_GRP_1 " + "\n");
            strSqlString.AppendFormat("                 ,SUM(TAT_QTY) AS TOT_TAT" + "\n");
            strSqlString.AppendFormat("                 ,ROUND(SUM(HMK2A),2) AS HMK2A" + "\n");
            strSqlString.AppendFormat("                 ,ROUND(SUM(SAW),2) AS SAW" + "\n");
            strSqlString.AppendFormat("                 ,ROUND(SUM(DA),2) AS DA" + "\n");
            strSqlString.AppendFormat("                 ,ROUND(SUM(WB),2) AS WB" + "\n");
            strSqlString.AppendFormat("                 ,ROUND(SUM(GATE),2) AS GATE" + "\n");
            strSqlString.AppendFormat("                 ,ROUND(SUM(MOLD),2) AS MOLD" + "\n");
            strSqlString.AppendFormat("                 ,ROUND(SUM(FINISH),2) AS FINISH" + "\n");
            strSqlString.AppendFormat("                 ,ROUND(SUM(HMK3A),2) AS HMK3A" + "\n");
            strSqlString.AppendFormat("              FROM(" + "\n");
            strSqlString.AppendFormat("                     SELECT TAT.MAT_GRP_1, TAT.OPER_GRP_1," + "\n");
            strSqlString.AppendFormat("                         SUM(TAT.TAT_QTY) AS TAT_QTY," + "\n");
            strSqlString.AppendFormat("                         SUM(TAT.SHIP_QTY) AS SHIP_QTY," + "\n");
            strSqlString.AppendFormat("                         DECODE(OPER_GRP_1,'HMK2A',SUM(TAT.TAT_QTY)/SUM(SHIP_QTY)) AS HMK2A," + "\n");
            strSqlString.AppendFormat("                         DECODE(OPER_GRP_1,'SAW',SUM(TAT.TAT_QTY)/SUM(SHIP_QTY)) AS SAW," + "\n");
            strSqlString.AppendFormat("                         DECODE(OPER_GRP_1,'D/A',SUM(TAT.TAT_QTY)/SUM(SHIP_QTY)) AS DA," + "\n");
            strSqlString.AppendFormat("                         DECODE(OPER_GRP_1,'W/B',SUM(TAT.TAT_QTY)/SUM(SHIP_QTY)) AS WB," + "\n");
            strSqlString.AppendFormat("                         DECODE(OPER_GRP_1,'GATE',SUM(TAT.TAT_QTY)/SUM(SHIP_QTY)) AS GATE," + "\n");
            strSqlString.AppendFormat("                         DECODE(OPER_GRP_1,'MOLD',SUM(TAT.TAT_QTY)/SUM(SHIP_QTY)) AS MOLD," + "\n");
            strSqlString.AppendFormat("                         DECODE(OPER_GRP_1,'FINISH',SUM(TAT.TAT_QTY)/SUM(SHIP_QTY)) AS FINISH," + "\n");
            strSqlString.AppendFormat("                         DECODE(OPER_GRP_1,'HMK3A',SUM(TAT.TAT_QTY)/SUM(SHIP_QTY)) AS HMK3A" + "\n");
            strSqlString.AppendFormat("                     FROM (" + "\n");
            strSqlString.AppendFormat("                         SELECT TAT.FACTORY, 'Others' AS MAT_GRP_1," + "\n");
            strSqlString.AppendFormat("                             TAT.OPER_GRP_1," + "\n");
            strSqlString.AppendFormat("                             SUM(TAT_QTY) AS TAT_QTY," + "\n");
            strSqlString.AppendFormat("                             SUM(SHIP_QTY) AS SHIP_QTY" + "\n");
            strSqlString.AppendFormat("                          FROM (" + "\n");
            strSqlString.AppendFormat("                                 SELECT TAT.FACTORY, OPER " + "\n");
            strSqlString.AppendFormat("                                     ,CASE WHEN OPER >= 'A0000' AND OPER <= 'A000N' THEN 'HMK2A'" + "\n");
            strSqlString.AppendFormat("                                         WHEN OPER >= 'A0020' AND OPER <= 'A0390' THEN 'SAW'" + "\n");
            strSqlString.AppendFormat("                                         WHEN OPER >= 'A0400' AND OPER <= 'A0540' THEN 'D/A'" + "\n");
            strSqlString.AppendFormat("                                         WHEN OPER >= 'A0550' AND OPER <= 'A0650' THEN 'W/B'" + "\n");
            strSqlString.AppendFormat("                                         WHEN OPER >= 'A0670' AND OPER <= 'A0809' THEN 'GATE'" + "\n");
            strSqlString.AppendFormat("                                         WHEN OPER >= 'A0950' AND OPER <= 'A1100' THEN 'MOLD'" + "\n");
            strSqlString.AppendFormat("                                         WHEN OPER >= 'A1110' AND OPER <= 'A2300' THEN 'FINISH'" + "\n");
            strSqlString.AppendFormat("                                         WHEN OPER >= 'AZ009' AND OPER <= 'AZ010' THEN 'HMK3A'" + "\n");
            strSqlString.AppendFormat("                                     END OPER_GRP_1" + "\n");
            strSqlString.AppendFormat("                                 , MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("                                 , SUM(TAT.TOTAL_TAT_QTY) AS TAT_QTY " + "\n");
            strSqlString.AppendFormat("                                 , SUM(SHIP.SHIP_QTY) AS SHIP_QTY" + "\n");
            strSqlString.AppendFormat("                             FROM CSUMTATMAT@RPTTOMES TAT," + "\n");
            strSqlString.AppendFormat("                             (" + "\n");
            strSqlString.AppendFormat("                                 SELECT TAT.FACTORY, TAT.MAT_ID," + "\n");
            strSqlString.AppendFormat("                                         SHIP_QTY" + "\n");
            strSqlString.AppendFormat("                                 FROM CSUMTATMAT@RPTTOMES TAT," + "\n");
            strSqlString.AppendFormat("                                     MWIPMATDEF MAT" + "\n");
            strSqlString.AppendFormat("                                 WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                                     AND TAT.FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("                                     AND TAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.AppendFormat("                                     AND TAT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("                                     AND MAT.MAT_GRP_1 NOT IN ('SE', 'HX')" + "\n");
            strSqlString.AppendFormat("                                     AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.AppendFormat("                                     AND OPER ='AZ010'" + "\n");
            strSqlString.AppendFormat("                                     AND TAT.SHIP_DATE BETWEEN '" + sFrom + "' AND '" + sTo + "'" + "\n", sFrom, sTo);
            strSqlString.AppendFormat("                             ) SHIP," + "\n");
            strSqlString.AppendFormat("                                 MWIPMATDEF MAT" + "\n");
            strSqlString.AppendFormat("                             WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                                 AND TAT.FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("                                 AND TAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.AppendFormat("                                 AND TAT.FACTORY = SHIP.FACTORY" + "\n");
            strSqlString.AppendFormat("                                 AND TAT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("                                 AND TAT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("                                 AND MAT.MAT_GRP_1 NOT IN ('SE', 'HX')" + "\n");
            strSqlString.AppendFormat("                                 AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.AppendFormat("                                 AND TAT.SHIP_DATE BETWEEN '" + sFrom + "' AND '" + sTo + "'" + "\n", sFrom, sTo);
            strSqlString.AppendFormat("                             GROUP BY TAT.FACTORY,OPER,MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("                             ) TAT," + "\n");
            strSqlString.AppendFormat("                             MWIPOPRDEF OPR" + "\n");
            strSqlString.AppendFormat("                     WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                         AND TAT.FACTORY = OPR.FACTORY" + "\n");
            strSqlString.AppendFormat("                         AND TAT.OPER = OPR.OPER" + "\n");
            strSqlString.AppendFormat("                     GROUP BY TAT.FACTORY, TAT.OPER_GRP_1" + "\n");
            strSqlString.AppendFormat("                     ) TAT" + "\n");
            strSqlString.AppendFormat("                 GROUP BY MAT_GRP_1, OPER_GRP_1" + "\n");
            strSqlString.AppendFormat("         ) GROUP BY MAT_GRP_1" + "\n");
            strSqlString.AppendFormat("             ) TAT," + "\n");
            strSqlString.AppendFormat("             (" + "\n");
            strSqlString.AppendFormat("                 SELECT 'Others' AS MAT_GRP_1," + "\n");
            strSqlString.AppendFormat("                     SUM(SHIP_QTY) AS SHIP_QTY" + "\n");
            strSqlString.AppendFormat("                 FROM CSUMTATMAT@RPTTOMES TAT," + "\n");
            strSqlString.AppendFormat("                     MWIPMATDEF MAT" + "\n");
            strSqlString.AppendFormat("                 WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                     AND TAT.FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("                     AND TAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.AppendFormat("                     AND TAT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("                     AND MAT.MAT_GRP_1 NOT IN ('SE', 'HX')" + "\n");
            strSqlString.AppendFormat("                     AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.AppendFormat("                     AND OPER ='AZ010'" + "\n");
            strSqlString.AppendFormat("                     AND TAT.SHIP_DATE BETWEEN '" + sFrom + "' AND '" + sTo + "'" + "\n", sFrom, sTo);
            strSqlString.AppendFormat("                 ) SHIP" + "\n");
            strSqlString.AppendFormat("                 WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                     AND TAT.MAT_GRP_1 = SHIP.MAT_GRP_1" + "\n");
            strSqlString.AppendFormat("            )ORDER BY DECODE(MAT_GRP_1, 'SAMSUNG', 1, 'Hynix', 2 , 'Others', 3)" + "\n");


            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            return strSqlString.ToString();
        }


        private string TeamSqlString(string flag)
        {
            string sTo = null;
            DateTime TmpDate = DateTime.Parse(cdvDate.Value.ToString());
            string toDate = cdvDate.Value.ToString("yyyy-MM-dd");
            string sFrom = DateTime.Parse(toDate).AddDays(1 - TmpDate.Day).ToString("yyyyMMdd");
            if (flag == "menu")
            {
                sTo = DateTime.Parse(toDate).AddDays(-1).ToString("yyyyMMdd");
            }
            else
            {
                sTo = DateTime.Parse(toDate).ToString("yyyyMMdd");
            }

            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1" + "\n");
            strSqlString.AppendFormat(" , ROUND(SUM(TAT_QTY0)/SUM(SHIP_QTY0),2) AS TEAM_TAT0" + "\n");
            strSqlString.AppendFormat(" , ROUND(SUM(TAT_QTY1)/SUM(SHIP_QTY1),2) AS TEAM_TAT1" + "\n");
            strSqlString.AppendFormat(" , ROUND(SUM(TAT_QTY2)/SUM(SHIP_QTY2),2) AS TEAM_TAT2" + "\n");
            strSqlString.AppendFormat(" , ROUND(SUM(TAT_QTY3)/SUM(SHIP_QTY3),2) AS TEAM_TAT3" + "\n");
            strSqlString.AppendFormat("FROM (" + "\n");
            strSqlString.AppendFormat(" SELECT MAT_GRP_1" + "\n");
            strSqlString.AppendFormat("     , DECODE(KEY_1, '1236', SUM(TAT_QTY)) AS TAT_QTY0" + "\n");
            strSqlString.AppendFormat("     , DECODE(KEY_1, '1106', SUM(TAT_QTY)) AS TAT_QTY1" + "\n");
            strSqlString.AppendFormat("     , DECODE(KEY_1, '1126', SUM(TAT_QTY)) AS TAT_QTY2" + "\n");
            strSqlString.AppendFormat("     , DECODE(KEY_1, '1146', SUM(TAT_QTY)) AS TAT_QTY3" + "\n");
            strSqlString.AppendFormat("     , DECODE(KEY_1, '1236', SUM(SHIP_QTY)) AS SHIP_QTY0" + "\n");
            strSqlString.AppendFormat("     , DECODE(KEY_1, '1106', SUM(SHIP_QTY)) AS SHIP_QTY1" + "\n");
            strSqlString.AppendFormat("     , DECODE(KEY_1, '1126', SUM(SHIP_QTY)) AS SHIP_QTY2" + "\n");
            strSqlString.AppendFormat("     , DECODE(KEY_1, '1146', SUM(SHIP_QTY)) AS SHIP_QTY3" + "\n");
            strSqlString.AppendFormat(" FROM (" + "\n");
            strSqlString.AppendFormat("     SELECT MAT_GRP_1, DATA_1,KEY_1,FACTORY ,MAT_ID, SHIP_DATE" + "\n");
            strSqlString.AppendFormat("     , SUM(TAT_QTY) AS TAT_QTY" + "\n");
            strSqlString.AppendFormat("     , SUM(SHIP_QTY) AS SHIP_QTY" + "\n");
            strSqlString.AppendFormat("     FROM ( " + "\n");
            strSqlString.AppendFormat("         SELECT MAT.MAT_GRP_1, GCM.DATA_1,GCM.KEY_1,TAT.MAT_ID,TAT.FACTORY, OPR.OPER" + "\n");
            strSqlString.AppendFormat("             , SUM(TAT.TOTAL_TAT_QTY) AS TAT_QTY" + "\n");
            strSqlString.AppendFormat("             , SUM(SHIP.SHIP_QTY) AS SHIP_QTY" + "\n");
            strSqlString.AppendFormat("             , TAT.SHIP_DATE" + "\n");
            strSqlString.AppendFormat("         FROM CSUMTATMAT@RPTTOMES TAT" + "\n");
            strSqlString.AppendFormat("             , MWIPOPRDEF OPR" + "\n");
            strSqlString.AppendFormat("             , MGCMTBLDAT GCM" + "\n");
            strSqlString.AppendFormat("             , MWIPMATDEF MAT" + "\n");
            strSqlString.AppendFormat("             ,(" + "\n");
            strSqlString.AppendFormat("                 SELECT TAT.FACTORY, TAT.MAT_ID, TAT.SHIP_DATE," + "\n");
            strSqlString.AppendFormat("                     SHIP_QTY" + "\n");
            strSqlString.AppendFormat("                 FROM CSUMTATMAT@RPTTOMES TAT," + "\n");
            strSqlString.AppendFormat("                     MWIPMATDEF MAT" + "\n");
            strSqlString.AppendFormat("                 WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                     AND TAT.FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("                     AND TAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.AppendFormat("                     AND TAT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("                     AND MAT.MAT_GRP_1 IN ('SE', 'HX')" + "\n");
            strSqlString.AppendFormat("                     AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.AppendFormat("                     AND OPER ='AZ010'" + "\n");
            strSqlString.AppendFormat("                     AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
            strSqlString.AppendFormat("             ) SHIP" + "\n");
            strSqlString.AppendFormat("         WHERE 1=1" + "\n");
            strSqlString.AppendFormat("             AND OPR.FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("             AND TAT.FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("             AND GCM.FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("             AND MAT.FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("             AND TAT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("             AND TAT.MAT_ID = SHIP.MAT_ID" + "\n");
            strSqlString.AppendFormat("             AND MAT.MAT_VER = 1" + "\n");
            strSqlString.AppendFormat("             AND TAT.OPER=OPR.OPER" + "\n");
            strSqlString.AppendFormat("             AND OPR.OPER_GRP_3 = GCM.KEY_1" + "\n");
            strSqlString.AppendFormat("             AND GCM.TABLE_NAME='H_DEPARTMENT'" + "\n");
            strSqlString.AppendFormat("             AND OPR.OPER_GRP_2 IN ('650', '660')" + "\n");
            strSqlString.AppendFormat("             AND GCM.KEY_1 LIKE '%' " + "\n");
            strSqlString.AppendFormat("             AND TAT.OPER = OPR.OPER" + "\n");
            strSqlString.AppendFormat("             AND MAT.MAT_GRP_1 IN ('SE', 'HX')" + "\n");
            strSqlString.AppendFormat("             AND TAT.SHIP_DATE = SHIP.SHIP_DATE" + "\n");
            strSqlString.AppendFormat("             AND TAT.SHIP_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, sTo);
            strSqlString.AppendFormat("         GROUP BY MAT_GRP_1,DATA_1,KEY_1,TAT.MAT_ID,TAT.FACTORY,OPR.OPER,TAT.SHIP_DATE" + "\n");
            strSqlString.AppendFormat("     ) GROUP BY MAT_GRP_1, DATA_1,KEY_1,FACTORY ,MAT_ID, SHIP_DATE" + "\n");
            strSqlString.AppendFormat(" ) GROUP BY MAT_GRP_1, KEY_1" + "\n");
            strSqlString.AppendFormat(") GROUP BY MAT_GRP_1" + "\n");
            strSqlString.AppendFormat("ORDER BY DECODE(MAT_GRP_1, 'SAMSUNG', 1, 'Hynix', 2)" + "\n");

            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            return strSqlString.ToString();
        }

        private void WorkTAT(DataTable wkdt)
        {
            if (wkdt.Rows.Count == 0)
            {
                chdt.Dispose();
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                return;
            }

            udcFarPoint_Work.DataSource = wkdt;

            udcFarPoint_Work.isShowZero = true;

            udcFarPoint_Work.RPT_AutoFit(false);
        }

        private void TeamTAT(DataTable temdt)
        {
            if (temdt.Rows.Count == 0)
            {
                chdt.Dispose();
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                return;
            }

            udcFarPoint_Team.DataSource = temdt;

            udcFarPoint_Team.isShowZero = true;

            udcFarPoint_Team.RPT_AutoFit(false);
        }


        #region 조회

        /// <summary>
        /// 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            DataTable dt2 = null;

            InitMainPageSpread(spdAssembly);
            InitMainPageSpread(spdTest);
            dadt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", DateSqlString("btn"));
            GridColumnInit();
            GridColumnInit1();
            GridColumnInit2();
            chdt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", ChartSqlString("btn"));
            ChartGride(chdt);
            wkdt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", WorkSqlString("btn"));
            WorkTAT(wkdt);
            temdt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", TeamSqlString("btn"));
            TeamTAT(temdt);


            try
            {

            //Assembly 달성률
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString("btn"));
            //Test 달성률
                dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2("btn"));
            FarPoint.Win.Spread.CellType.ImageCellType img = new FarPoint.Win.Spread.CellType.ImageCellType(FarPoint.Win.RenderStyle.Normal);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                spdAssembly_Sheet1.Cells[i, 0].Value = dt.Rows[i][0].ToString();
                spdAssembly_Sheet1.Cells[i, 1].Value = dt.Rows[i][1].ToString();
                spdAssembly_Sheet1.Cells[i, 2].Value = dt.Rows[i][2].ToString();
                spdAssembly_Sheet1.Cells[i, 3].Value = dt.Rows[i][3].ToString()+"%";
                if (Convert.ToDouble(dt.Rows[i][3].ToString()) >= 91.00)
                {
                    spdAssembly_Sheet1.Cells[i, 4].Value = new Bitmap(global::Hana.TAT.Properties.Resources.Best);
                }
                else if (Convert.ToDouble(dt.Rows[i][3].ToString()) <= 90.99 && Convert.ToDouble(dt.Rows[i][3].ToString()) >= 81.00)
                {
                    spdAssembly_Sheet1.Cells[i, 4].Value = new Bitmap(global::Hana.TAT.Properties.Resources.Good);
                }
                else if (Convert.ToDouble(dt.Rows[i][3].ToString()) <= 80.99 && Convert.ToDouble(dt.Rows[i][3].ToString()) >= 71.00)
                {
                    spdAssembly_Sheet1.Cells[i, 4].Value = new Bitmap(global::Hana.TAT.Properties.Resources.Warning);
                }
                else if (Convert.ToDouble(dt.Rows[i][3].ToString()) <= 70.99 && Convert.ToDouble(dt.Rows[i][3].ToString()) >= 61.00)
                {
                    spdAssembly_Sheet1.Cells[i, 4].Value = new Bitmap(global::Hana.TAT.Properties.Resources.Bad);
                }
                else if (Convert.ToDouble(dt.Rows[i][3].ToString()) <= 60.99)
                {
                    spdAssembly_Sheet1.Cells[i, 4].Value = new Bitmap(global::Hana.TAT.Properties.Resources.Worst);
                }
                spdAssembly_Sheet1.Cells[i, 4].CellType = img;
                spdAssembly_Sheet1.Cells[i, 5].Value = dt.Rows[i][5].ToString();
                
            }
            dt.Dispose();
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                spdTest_Sheet1.Cells[i, 0].Value = dt2.Rows[i][0].ToString();
                spdTest_Sheet1.Cells[i, 1].Value = dt2.Rows[i][1].ToString();
                spdTest_Sheet1.Cells[i, 2].Value = dt2.Rows[i][2].ToString();
                spdTest_Sheet1.Cells[i, 3].Value = dt2.Rows[i][3].ToString();
                if (Convert.ToDouble(dt2.Rows[i][3].ToString()) >= 91.00)
                {
                    spdTest_Sheet1.Cells[i, 4].Value = new Bitmap(global::Hana.TAT.Properties.Resources.Best);
                }
                else if (Convert.ToDouble(dt2.Rows[i][3].ToString()) <= 90.99 && Convert.ToDouble(dt2.Rows[i][3].ToString()) >= 81.00)
                {
                    spdTest_Sheet1.Cells[i, 4].Value = new Bitmap(global::Hana.TAT.Properties.Resources.Good);
                }
                else if (Convert.ToDouble(dt2.Rows[i][3].ToString()) <= 80.99 && Convert.ToDouble(dt2.Rows[i][3].ToString()) >= 71.00)
                {
                    spdTest_Sheet1.Cells[i, 4].Value = new Bitmap(global::Hana.TAT.Properties.Resources.Warning);
                }
                else if (Convert.ToDouble(dt2.Rows[i][3].ToString()) <= 70.99 && Convert.ToDouble(dt2.Rows[i][3].ToString()) >= 61.00)
                {
                    spdTest_Sheet1.Cells[i, 4].Value = new Bitmap(global::Hana.TAT.Properties.Resources.Bad);
                }
                else if (Convert.ToDouble(dt2.Rows[i][3].ToString()) <= 60.99)
                {
                    spdTest_Sheet1.Cells[i, 4].Value = new Bitmap(global::Hana.TAT.Properties.Resources.Worst);
                }
                spdTest_Sheet1.Cells[i, 4].CellType = img;
                spdTest_Sheet1.Cells[i, 5].Value = dt2.Rows[i][5].ToString();
            }
            
            this.Refresh();
            dt2.Dispose();

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

        private void TAT0000_Load(object sender, EventArgs e)
        {
            DataTable dt = null;
            DataTable dt2 = null;

            cdvDate.Value = DateTime.Now;

            tabPage1.Text = "Samsung(M)";
            tabPage2.Text = "HYNIX";
            tabPage3.Text = "Other(LSI)";

            InitMainPageSpread(spdAssembly);
            InitMainPageSpread(spdTest);

            dadt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", DateSqlString("menu"));
            GridColumnInit();
            GridColumnInit1();
            GridColumnInit2();
            chdt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", ChartSqlString("menu"));
            ChartGride(chdt);
            wkdt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", WorkSqlString("menu"));
            WorkTAT(wkdt);
            temdt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", TeamSqlString("menu"));
            TeamTAT(temdt);

            //Assembly 달성률
            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString("menu"));
            //Test 달성률
            dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2("menu"));
            FarPoint.Win.Spread.CellType.ImageCellType img = new FarPoint.Win.Spread.CellType.ImageCellType(FarPoint.Win.RenderStyle.Normal);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                spdAssembly_Sheet1.Cells[i, 0].Value = dt.Rows[i][0].ToString();
                spdAssembly_Sheet1.Cells[i, 1].Value = dt.Rows[i][1].ToString();
                spdAssembly_Sheet1.Cells[i, 2].Value = dt.Rows[i][2].ToString();
                spdAssembly_Sheet1.Cells[i, 3].Value = dt.Rows[i][3].ToString()+"%";
                if (Convert.ToDouble(dt.Rows[i][3].ToString()) >= 91.00)
                {
                    spdAssembly_Sheet1.Cells[i, 4].Value = new Bitmap(global::Hana.TAT.Properties.Resources.Best);
                }
                else if (Convert.ToDouble(dt.Rows[i][3].ToString()) <= 90.99 && Convert.ToDouble(dt.Rows[i][3].ToString()) >= 81.00)
                {
                    spdAssembly_Sheet1.Cells[i, 4].Value = new Bitmap(global::Hana.TAT.Properties.Resources.Good);
                }
                else if (Convert.ToDouble(dt.Rows[i][3].ToString()) <= 80.99 && Convert.ToDouble(dt.Rows[i][3].ToString()) >= 71.00)
                {
                    spdAssembly_Sheet1.Cells[i, 4].Value = new Bitmap(global::Hana.TAT.Properties.Resources.Warning);
                }
                else if (Convert.ToDouble(dt.Rows[i][3].ToString()) <= 70.99 && Convert.ToDouble(dt.Rows[i][3].ToString()) >= 61.00)
                {
                    spdAssembly_Sheet1.Cells[i, 4].Value = new Bitmap(global::Hana.TAT.Properties.Resources.Bad);
                }
                else if (Convert.ToDouble(dt.Rows[i][3].ToString()) <= 60.99)
                {
                    spdAssembly_Sheet1.Cells[i, 4].Value = new Bitmap(global::Hana.TAT.Properties.Resources.Worst);
                }
                spdAssembly_Sheet1.Cells[i, 4].CellType = img;
                spdAssembly_Sheet1.Cells[i, 5].Value = dt.Rows[i][5].ToString();

            } 
            dt.Dispose();
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                spdTest_Sheet1.Cells[i, 0].Value = dt2.Rows[i][0].ToString();
                spdTest_Sheet1.Cells[i, 1].Value = dt2.Rows[i][1].ToString();
                spdTest_Sheet1.Cells[i, 2].Value = dt2.Rows[i][2].ToString();
                spdTest_Sheet1.Cells[i, 3].Value = dt2.Rows[i][3].ToString();
                if (Convert.ToDouble(dt2.Rows[i][3].ToString()) >= 91.00)
                {
                    spdTest_Sheet1.Cells[i, 4].Value = new Bitmap(global::Hana.TAT.Properties.Resources.Best);
                }
                else if (Convert.ToDouble(dt2.Rows[i][3].ToString()) <= 90.99 && Convert.ToDouble(dt2.Rows[i][3].ToString()) >= 81.00)
                {
                    spdTest_Sheet1.Cells[i, 4].Value = new Bitmap(global::Hana.TAT.Properties.Resources.Good);
                }
                else if (Convert.ToDouble(dt2.Rows[i][3].ToString()) <= 80.99 && Convert.ToDouble(dt2.Rows[i][3].ToString()) >= 71.00)
                {
                    spdTest_Sheet1.Cells[i, 4].Value = new Bitmap(global::Hana.TAT.Properties.Resources.Warning);
                }
                else if (Convert.ToDouble(dt2.Rows[i][3].ToString()) <= 70.99 && Convert.ToDouble(dt2.Rows[i][3].ToString()) >= 61.00)
                {
                    spdTest_Sheet1.Cells[i, 4].Value = new Bitmap(global::Hana.TAT.Properties.Resources.Bad);
                }
                else if (Convert.ToDouble(dt2.Rows[i][3].ToString()) <= 60.99)
                {
                    spdTest_Sheet1.Cells[i, 4].Value = new Bitmap(global::Hana.TAT.Properties.Resources.Worst);
                }
                spdTest_Sheet1.Cells[i, 4].CellType = img;
                spdTest_Sheet1.Cells[i, 5].Value = dt2.Rows[i][5].ToString();
            }
            
            this.Refresh();

            dt2.Dispose();
        }

        private void TAT0000_Resize(object sender, EventArgs e)
        {
            if (spdAssembly.Width >= 300)
            {
                //spdAssembly_Sheet1.ColumnHeader.Rows[0].Height = spdAssembly.Height - 20 - 280;
                spdAssembly_Sheet1.Columns[0].Width = spdAssembly.Width - 300;
                spdTest_Sheet1.ColumnHeader.Rows[0].Height = spdTest.Height - 20 - 120;
                spdTest_Sheet1.Columns[0].Width = spdTest.Width - 300;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.OnCloseLayoutForm();
                this.Dispose();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("TAT050101.btnClose_Click()" + "\r\n" + ex.Message);
            }
        }

    }
}

