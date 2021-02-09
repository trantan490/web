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
    public partial class TRN090105 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        
        /// <summary>
        /// 클  래  스: TRN090105<br/>
        /// 클래스요약: 삼성입고 KIT현황<br/>
        /// 작  성  자: 하나마이크론 김민우<br/>
        /// 최초작성일: 2013-05-09<br/>
        /// 상세  설명: <br/>
        /// 변경  내용: <br/>
        /// </summary>
        public TRN090105()
        {
            InitializeComponent();

            //SortInit();
            //cdvDate.Value = DateTime.Now.AddDays(-1);
            cdvDate.Value = DateTime.Now;
            GridColumnInit(); //헤더 한줄짜리 
            
            // 2015-07-13-정비재 : chartfx를 mschart로 변경함.
            //udcChartFX1.RPT_1_ChartInit();  //차트 초기화   
            udcMSChart1.RPT_1_ChartInit();

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;            
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
        }

        #endregion

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
                spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("MAT_GRP_10", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("DEGREE", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("DD-00", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("DD-01", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("DD-02", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("DD-03", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("DD-04", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("DD-05", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("DD-06", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("DD-07", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("DD-08", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("DD-09", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("DD-10", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("DD-11", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("DD-12", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("DD-13", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 80);
               
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        #endregion

        #region 조회

        /// <summary>
        /// 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            //if (!CheckField()) return;

            DataTable dt = null;
          
            GridColumnInit();

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                spdData_Sheet1.RowCount = 0;
                this.Refresh();
                //udcChartFX1.RPT_2_ClearData();
                udcMSChart1.RPT_2_ClearData();

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }


                ////표구성에따른 항목 Display
                spdData.RPT_ColumnConfigFromTable(btnSort);
               
                spdData.DataSource = dt;

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);
                //--------------------------------------------
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


        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }
            return true;
        }

        #endregion
        

        #region MakeSqlString
        private string MakeSqlString()
        {            
            StringBuilder strSqlString = new StringBuilder();
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            string date;

            date = cdvDate.SelectedValue();

            strSqlString.Append("SELECT MAT_GRP_10 AS MAT_GRP_10" + "\n");
            strSqlString.Append("     , DEGREE AS DEGREE" + "\n");
            strSqlString.Append("     , SUM(CASE WORK_DATE WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "-  0, 'YYYYMMDD') THEN QTY_1 ELSE 0 END) AS D00" + "\n");
            strSqlString.Append("     , SUM(CASE WORK_DATE WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "-  1, 'YYYYMMDD') THEN QTY_1 ELSE 0 END) AS D01" + "\n");
            strSqlString.Append("     , SUM(CASE WORK_DATE WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "-  2, 'YYYYMMDD') THEN QTY_1 ELSE 0 END) AS D02" + "\n");
            strSqlString.Append("     , SUM(CASE WORK_DATE WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "-  3, 'YYYYMMDD') THEN QTY_1 ELSE 0 END) AS D03" + "\n");
            strSqlString.Append("     , SUM(CASE WORK_DATE WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "-  4, 'YYYYMMDD') THEN QTY_1 ELSE 0 END) AS D04" + "\n");
            strSqlString.Append("     , SUM(CASE WORK_DATE WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "-  5, 'YYYYMMDD') THEN QTY_1 ELSE 0 END) AS D05" + "\n");
            strSqlString.Append("     , SUM(CASE WORK_DATE WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "-  6, 'YYYYMMDD') THEN QTY_1 ELSE 0 END) AS D06" + "\n");
            strSqlString.Append("     , SUM(CASE WORK_DATE WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "-  7, 'YYYYMMDD') THEN QTY_1 ELSE 0 END) AS D07" + "\n");
            strSqlString.Append("     , SUM(CASE WORK_DATE WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "-  8, 'YYYYMMDD') THEN QTY_1 ELSE 0 END) AS D08" + "\n");
            strSqlString.Append("     , SUM(CASE WORK_DATE WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "-  9, 'YYYYMMDD') THEN QTY_1 ELSE 0 END) AS D09" + "\n");
            strSqlString.Append("     , SUM(CASE WORK_DATE WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "- 10, 'YYYYMMDD') THEN QTY_1 ELSE 0 END) AS D10" + "\n");
            strSqlString.Append("     , SUM(CASE WORK_DATE WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "- 11, 'YYYYMMDD') THEN QTY_1 ELSE 0 END) AS D11" + "\n");
            strSqlString.Append("     , SUM(CASE WORK_DATE WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "- 12, 'YYYYMMDD') THEN QTY_1 ELSE 0 END) AS D12" + "\n");
            strSqlString.Append("     , SUM(CASE WORK_DATE WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "- 13, 'YYYYMMDD') THEN QTY_1 ELSE 0 END) AS D13" + "\n");
            strSqlString.Append("  FROM (" + "\n");
			// 2016-12-13-정비재 : 1차 Stack
            strSqlString.Append("        SELECT B.MAT_GRP_10 AS MAT_GRP_10" + "\n");
            strSqlString.Append("             , A.WORK_DATE AS WORK_DATE " + "\n");
            strSqlString.Append("             , '01차' AS DEGREE " + "\n");
            strSqlString.Append("             , ROUND(SUM(A.S1_OPER_IN_QTY_1 + A.S2_OPER_IN_QTY_1 + A.S3_OPER_IN_QTY_1) / 1000) AS QTY_1 " + "\n");
            strSqlString.Append("          FROM RSUMWIPMOV A " + "\n");
            strSqlString.Append("             , (SELECT FACTORY AS FACTORY " + "\n");
            strSqlString.Append("                     , MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                     , MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU')) B " + "\n");
            strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("           AND A.WORK_DATE BETWEEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "- 13, 'YYYYMMDD') AND TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + ", 'YYYYMMDD') " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND A.OPER = 'A0000' " + "\n");
            strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE 'SE%' " + "\n");
            strSqlString.Append("           AND A.MAT_ID IN (SELECT MAT_ID AS TARGET_MAT_ID " + "\n");
            strSqlString.Append("                              FROM MTAPCBMDEF@RPTTOMES " + "\n");
            strSqlString.Append("                             WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                               AND MAT_ID IN (SELECT MAT_ID " + "\n");
            strSqlString.Append("                                                FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                                               WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                                 AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                                                 AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                                                 AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU') " + "\n");
            strSqlString.Append("                                                 AND UPPER(MAT_GRP_5) IN ('1ST', '1 ST'))) " + "\n");
            strSqlString.Append("        GROUP BY B.MAT_GRP_10, A.WORK_DATE " + "\n");
			// 2016-12-13-정비재 : 2차 Stack
			strSqlString.Append("        UNION ALL " + "\n");
			strSqlString.Append("        SELECT B.MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("             , A.WORK_DATE AS WORK_DATE " + "\n");
			strSqlString.Append("             , '02차' AS DEGREE " + "\n");
            strSqlString.Append("             , ROUND(SUM(A.S1_OPER_IN_QTY_1 + A.S2_OPER_IN_QTY_1 + A.S3_OPER_IN_QTY_1) / 1000) AS QTY_1 " + "\n");
            strSqlString.Append("          FROM RSUMWIPMOV A " + "\n");
            strSqlString.Append("             , (SELECT FACTORY AS FACTORY " + "\n");
            strSqlString.Append("                     , MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                     , MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU')) B " + "\n");
            strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("           AND A.WORK_DATE BETWEEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "- 13, 'YYYYMMDD') AND TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + ", 'YYYYMMDD') " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND A.OPER = 'A0000' " + "\n");
            strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE 'SE%' " + "\n");
            strSqlString.Append("           AND A.MAT_ID IN (SELECT SRC_MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                              FROM MTAPCBMDEF@RPTTOMES " + "\n");
            strSqlString.Append("                             WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                               AND SRC_MAT_ID IN (SELECT MAT_ID " + "\n");
            strSqlString.Append("                                                    FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                                                   WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                                     AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU') " + "\n");
            strSqlString.Append("                                                     AND UPPER(MAT_GRP_5) IN ('2ND', '2 ND'))) " + "\n");
            strSqlString.Append("        GROUP BY B.MAT_GRP_10, A.WORK_DATE " + "\n");
			// 2016-12-13-정비재 : 3차 Stack
            strSqlString.Append("        UNION ALL " + "\n");
            strSqlString.Append("        SELECT B.MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("             , A.WORK_DATE AS WORK_DATE " + "\n");
			strSqlString.Append("             , '03차' AS DEGREE " + "\n");
            strSqlString.Append("             , ROUND(SUM(A.S1_OPER_IN_QTY_1 + A.S2_OPER_IN_QTY_1 + A.S3_OPER_IN_QTY_1) / 1000) AS QTY_1 " + "\n");
            strSqlString.Append("          FROM RSUMWIPMOV A " + "\n");
            strSqlString.Append("             , (SELECT FACTORY AS FACTORY " + "\n");
            strSqlString.Append("                     , MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                     , MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU')) B " + "\n");
            strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("           AND A.WORK_DATE BETWEEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "- 13, 'YYYYMMDD') AND TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + ", 'YYYYMMDD') " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND A.OPER = 'A0000' " + "\n");
            strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE 'SE%' " + "\n");
            strSqlString.Append("           AND A.MAT_ID IN (SELECT SRC_MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                              FROM MTAPCBMDEF@RPTTOMES " + "\n");
            strSqlString.Append("                             WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                               AND SRC_MAT_ID IN (SELECT MAT_ID " + "\n");
            strSqlString.Append("                                                    FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                                                   WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                                     AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU') " + "\n");
            strSqlString.Append("                                                     AND UPPER(MAT_GRP_5) IN ('3RD', '3 RD'))) " + "\n");
            strSqlString.Append("        GROUP BY B.MAT_GRP_10, A.WORK_DATE " + "\n");
			// 2016-12-13-정비재 : 4차 Stack
            strSqlString.Append("        UNION ALL " + "\n");
            strSqlString.Append("        SELECT B.MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("             , A.WORK_DATE AS WORK_DATE " + "\n");
			strSqlString.Append("             , '04차' AS DEGREE " + "\n");
            strSqlString.Append("             , ROUND(SUM(A.S1_OPER_IN_QTY_1 + A.S2_OPER_IN_QTY_1 + A.S3_OPER_IN_QTY_1) / 1000) AS QTY_1 " + "\n");
            strSqlString.Append("          FROM RSUMWIPMOV A " + "\n");
            strSqlString.Append("             , (SELECT FACTORY AS FACTORY " + "\n");
            strSqlString.Append("                     , MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                     , MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU')) B " + "\n");
            strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("           AND A.WORK_DATE BETWEEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "- 13, 'YYYYMMDD') AND TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + ", 'YYYYMMDD') " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND A.OPER = 'A0000' " + "\n");
            strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE 'SE%' " + "\n");
            strSqlString.Append("           AND A.MAT_ID IN (SELECT SRC_MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                              FROM MTAPCBMDEF@RPTTOMES " + "\n");
            strSqlString.Append("                             WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                               AND SRC_MAT_ID IN (SELECT MAT_ID " + "\n");
            strSqlString.Append("                                                    FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                                                   WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                                     AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU') " + "\n");
            strSqlString.Append("                                                     AND UPPER(MAT_GRP_5) IN ('4TH', '4 TH'))) " + "\n");
            strSqlString.Append("         GROUP BY B.MAT_GRP_10, A.WORK_DATE " + "\n");
			// 2016-12-13-정비재 : 5차 Stack
            strSqlString.Append("        UNION ALL " + "\n");
            strSqlString.Append("        SELECT B.MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("             , A.WORK_DATE AS WORK_DATE " + "\n");
			strSqlString.Append("             , '05차' AS DEGREE " + "\n");
            strSqlString.Append("             , ROUND(SUM(A.S1_OPER_IN_QTY_1 + A.S2_OPER_IN_QTY_1 + A.S3_OPER_IN_QTY_1) / 1000) AS QTY_1 " + "\n");
            strSqlString.Append("          FROM RSUMWIPMOV A " + "\n");
            strSqlString.Append("             , (SELECT FACTORY AS FACTORY " + "\n");
            strSqlString.Append("                     , MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                     , MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU')) B " + "\n");
            strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("           AND A.WORK_DATE BETWEEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "- 13, 'YYYYMMDD') AND TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + ", 'YYYYMMDD') " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND A.OPER = 'A0000' " + "\n");
            strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE 'SE%' " + "\n");
            strSqlString.Append("           AND A.MAT_ID IN (SELECT SRC_MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                              FROM MTAPCBMDEF@RPTTOMES " + "\n");
            strSqlString.Append("                             WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                               AND SRC_MAT_ID IN (SELECT MAT_ID " + "\n");
            strSqlString.Append("                                                    FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                                                   WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                                     AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU') " + "\n");
            strSqlString.Append("                                                     AND UPPER(MAT_GRP_5) IN ('5TH', '5 TH'))) " + "\n");
            strSqlString.Append("         GROUP BY B.MAT_GRP_10, A.WORK_DATE " + "\n");
			// 2016-12-13-정비재 : 6차 Stack
            strSqlString.Append("        UNION ALL " + "\n");
            strSqlString.Append("        SELECT B.MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("             , A.WORK_DATE AS WORK_DATE " + "\n");
			strSqlString.Append("             , '06차' AS DEGREE " + "\n");
            strSqlString.Append("             , ROUND(SUM(A.S1_OPER_IN_QTY_1 + A.S2_OPER_IN_QTY_1 + A.S3_OPER_IN_QTY_1) / 1000) AS QTY_1 " + "\n");
            strSqlString.Append("          FROM RSUMWIPMOV A " + "\n");
            strSqlString.Append("             , (SELECT FACTORY AS FACTORY " + "\n");
            strSqlString.Append("                     , MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                     , MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU')) B " + "\n");
            strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("           AND A.WORK_DATE BETWEEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "- 13, 'YYYYMMDD') AND TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + ", 'YYYYMMDD') " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND A.OPER = 'A0000' " + "\n");
            strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE 'SE%' " + "\n");
            strSqlString.Append("           AND A.MAT_ID IN (SELECT SRC_MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                              FROM MTAPCBMDEF@RPTTOMES " + "\n");
            strSqlString.Append("                             WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                               AND SRC_MAT_ID IN (SELECT MAT_ID " + "\n");
            strSqlString.Append("                                                    FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                                                   WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                                     AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU') " + "\n");
            strSqlString.Append("                                                     AND UPPER(MAT_GRP_5) IN ('6TH', '6 TH'))) " + "\n");
            strSqlString.Append("         GROUP BY B.MAT_GRP_10, A.WORK_DATE " + "\n");
			// 2016-12-13-정비재 : 7차 Stack
            strSqlString.Append("        UNION ALL " + "\n");
            strSqlString.Append("        SELECT B.MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("             , A.WORK_DATE AS WORK_DATE " + "\n");
			strSqlString.Append("             , '07차' AS DEGREE " + "\n");
            strSqlString.Append("             , ROUND(SUM(A.S1_OPER_IN_QTY_1 + A.S2_OPER_IN_QTY_1 + A.S3_OPER_IN_QTY_1) / 1000) AS QTY_1 " + "\n");
            strSqlString.Append("          FROM RSUMWIPMOV A " + "\n");
            strSqlString.Append("             , (SELECT FACTORY AS FACTORY " + "\n");
            strSqlString.Append("                     , MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                     , MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU')) B " + "\n");
            strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("           AND A.WORK_DATE BETWEEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "- 13, 'YYYYMMDD') AND TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + ", 'YYYYMMDD') " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND A.OPER = 'A0000' " + "\n");
            strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE 'SE%' " + "\n");
            strSqlString.Append("           AND A.MAT_ID IN (SELECT SRC_MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                              FROM MTAPCBMDEF@RPTTOMES " + "\n");
            strSqlString.Append("                             WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                               AND SRC_MAT_ID IN (SELECT MAT_ID " + "\n");
            strSqlString.Append("                                                    FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                                                   WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                                     AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU') " + "\n");
            strSqlString.Append("                                                     AND UPPER(MAT_GRP_5) IN ('7TH', '7 TH'))) " + "\n");
            strSqlString.Append("         GROUP BY B.MAT_GRP_10, A.WORK_DATE " + "\n");
			// 2016-12-13-정비재 : 8차 Stack
            strSqlString.Append("        UNION ALL " + "\n");
            strSqlString.Append("        SELECT B.MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("             , A.WORK_DATE AS WORK_DATE " + "\n");
			strSqlString.Append("             , '08차' AS DEGREE " + "\n");
            strSqlString.Append("             , ROUND(SUM(A.S1_OPER_IN_QTY_1 + A.S2_OPER_IN_QTY_1 + A.S3_OPER_IN_QTY_1) / 1000) AS QTY_1 " + "\n");
            strSqlString.Append("          FROM RSUMWIPMOV A " + "\n");
            strSqlString.Append("             , (SELECT FACTORY AS FACTORY " + "\n");
            strSqlString.Append("                     , MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                     , MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU')) B " + "\n");
            strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("           AND A.WORK_DATE BETWEEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "- 13, 'YYYYMMDD') AND TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + ", 'YYYYMMDD') " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND A.OPER = 'A0000' " + "\n");
            strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE 'SE%' " + "\n");
            strSqlString.Append("           AND A.MAT_ID IN (SELECT SRC_MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                              FROM MTAPCBMDEF@RPTTOMES " + "\n");
            strSqlString.Append("                             WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                               AND SRC_MAT_ID IN (SELECT MAT_ID " + "\n");
            strSqlString.Append("                                                    FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                                                   WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                                     AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU') " + "\n");
            strSqlString.Append("                                                     AND UPPER(MAT_GRP_5) IN ('8TH', '8 TH'))) " + "\n");
            strSqlString.Append("         GROUP BY B.MAT_GRP_10, A.WORK_DATE " + "\n");
			// 2016-12-13-정비재 : 9차 Stack
            strSqlString.Append("        UNION ALL " + "\n");
            strSqlString.Append("        SELECT B.MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("             , A.WORK_DATE AS WORK_DATE " + "\n");
			strSqlString.Append("             , '09차' AS DEGREE " + "\n");
            strSqlString.Append("             , ROUND(SUM(A.S1_OPER_IN_QTY_1 + A.S2_OPER_IN_QTY_1 + A.S3_OPER_IN_QTY_1) / 1000) AS QTY_1 " + "\n");
            strSqlString.Append("          FROM RSUMWIPMOV A " + "\n");
            strSqlString.Append("             , (SELECT FACTORY AS FACTORY " + "\n");
            strSqlString.Append("                     , MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                     , MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU')) B " + "\n");
            strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("           AND A.WORK_DATE BETWEEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "- 13, 'YYYYMMDD') AND TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + ", 'YYYYMMDD') " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND A.OPER = 'A0000' " + "\n");
            strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE 'SE%' " + "\n");
            strSqlString.Append("           AND A.MAT_ID IN (SELECT SRC_MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                              FROM MTAPCBMDEF@RPTTOMES " + "\n");
            strSqlString.Append("                             WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                               AND SRC_MAT_ID IN (SELECT MAT_ID " + "\n");
            strSqlString.Append("                                                    FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                                                   WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                                     AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU') " + "\n");
            strSqlString.Append("                                                     AND UPPER(MAT_GRP_5) IN ('9TH', '9 TH'))) " + "\n");
            strSqlString.Append("         GROUP BY B.MAT_GRP_10, A.WORK_DATE " + "\n");
			// 2016-12-13-정비재 : 10차 Stack
            strSqlString.Append("        UNION ALL " + "\n");
            strSqlString.Append("        SELECT B.MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("             , A.WORK_DATE AS WORK_DATE " + "\n");
            strSqlString.Append("             , '10차' AS DEGREE " + "\n");
            strSqlString.Append("             , ROUND(SUM(A.S1_OPER_IN_QTY_1 + A.S2_OPER_IN_QTY_1 + A.S3_OPER_IN_QTY_1) / 1000) AS QTY_1 " + "\n");
            strSqlString.Append("          FROM RSUMWIPMOV A " + "\n");
            strSqlString.Append("             , (SELECT FACTORY AS FACTORY " + "\n");
            strSqlString.Append("                     , MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                     , MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU')) B " + "\n");
            strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("           AND A.WORK_DATE BETWEEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "- 13, 'YYYYMMDD') AND TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + ", 'YYYYMMDD') " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND A.OPER = 'A0000' " + "\n");
            strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE 'SE%' " + "\n");
            strSqlString.Append("           AND A.MAT_ID IN (SELECT SRC_MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                              FROM MTAPCBMDEF@RPTTOMES " + "\n");
            strSqlString.Append("                             WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                               AND SRC_MAT_ID IN (SELECT MAT_ID " + "\n");
            strSqlString.Append("                                                    FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                                                   WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                                     AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU') " + "\n");
            strSqlString.Append("                                                     AND UPPER(MAT_GRP_5) IN ('10TH', '10 TH'))) " + "\n");
            strSqlString.Append("         GROUP BY B.MAT_GRP_10, A.WORK_DATE " + "\n");
			// 2016-12-13-정비재 : 11차 Stack
            strSqlString.Append("        UNION ALL " + "\n");
            strSqlString.Append("        SELECT B.MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("             , A.WORK_DATE AS WORK_DATE " + "\n");
            strSqlString.Append("             , '11차' AS DEGREE " + "\n");
            strSqlString.Append("             , ROUND(SUM(A.S1_OPER_IN_QTY_1 + A.S2_OPER_IN_QTY_1 + A.S3_OPER_IN_QTY_1) / 1000) AS QTY_1 " + "\n");
            strSqlString.Append("          FROM RSUMWIPMOV A " + "\n");
            strSqlString.Append("             , (SELECT FACTORY AS FACTORY " + "\n");
            strSqlString.Append("                     , MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                     , MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU')) B " + "\n");
            strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("           AND A.WORK_DATE BETWEEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "- 13, 'YYYYMMDD') AND TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + ", 'YYYYMMDD') " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND A.OPER = 'A0000' " + "\n");
            strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE 'SE%' " + "\n");
            strSqlString.Append("           AND A.MAT_ID IN (SELECT SRC_MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                              FROM MTAPCBMDEF@RPTTOMES " + "\n");
            strSqlString.Append("                             WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                               AND SRC_MAT_ID IN (SELECT MAT_ID " + "\n");
            strSqlString.Append("                                                    FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                                                   WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                                     AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU') " + "\n");
            strSqlString.Append("                                                     AND UPPER(MAT_GRP_5) IN ('11TH', '11 TH'))) " + "\n");
            strSqlString.Append("         GROUP BY B.MAT_GRP_10, A.WORK_DATE " + "\n");
			// 2016-12-13-정비재 : 12차 Stack
            strSqlString.Append("        UNION ALL " + "\n");
            strSqlString.Append("        SELECT B.MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("             , A.WORK_DATE AS WORK_DATE " + "\n");
            strSqlString.Append("             , '12차' AS DEGREE " + "\n");
            strSqlString.Append("             , ROUND(SUM(A.S1_OPER_IN_QTY_1 + A.S2_OPER_IN_QTY_1 + A.S3_OPER_IN_QTY_1) / 1000) AS QTY_1 " + "\n");
            strSqlString.Append("          FROM RSUMWIPMOV A " + "\n");
            strSqlString.Append("             , (SELECT FACTORY AS FACTORY " + "\n");
            strSqlString.Append("                     , MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                     , MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU')) B " + "\n");
            strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("           AND A.WORK_DATE BETWEEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "- 13, 'YYYYMMDD') AND TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + ", 'YYYYMMDD') " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND A.OPER = 'A0000' " + "\n");
            strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE 'SE%' " + "\n");
            strSqlString.Append("           AND A.MAT_ID IN (SELECT SRC_MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                              FROM MTAPCBMDEF@RPTTOMES " + "\n");
            strSqlString.Append("                             WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                               AND SRC_MAT_ID IN (SELECT MAT_ID " + "\n");
            strSqlString.Append("                                                    FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                                                   WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                                     AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU') " + "\n");
            strSqlString.Append("                                                     AND UPPER(MAT_GRP_5) IN ('12TH', '12 TH'))) " + "\n");
            strSqlString.Append("         GROUP BY B.MAT_GRP_10, A.WORK_DATE " + "\n");
			// 2016-12-13-정비재 : 13차 Stack
            strSqlString.Append("        UNION ALL " + "\n");
            strSqlString.Append("        SELECT B.MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("             , A.WORK_DATE AS WORK_DATE " + "\n");
            strSqlString.Append("             , '13차' AS DEGREE " + "\n");
            strSqlString.Append("             , ROUND(SUM(A.S1_OPER_IN_QTY_1 + A.S2_OPER_IN_QTY_1 + A.S3_OPER_IN_QTY_1) / 1000) AS QTY_1 " + "\n");
            strSqlString.Append("          FROM RSUMWIPMOV A " + "\n");
            strSqlString.Append("             , (SELECT FACTORY AS FACTORY " + "\n");
            strSqlString.Append("                     , MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                     , MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU')) B " + "\n");
            strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("           AND A.WORK_DATE BETWEEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "- 13, 'YYYYMMDD') AND TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + ", 'YYYYMMDD') " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND A.OPER = 'A0000' " + "\n");
            strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE 'SE%' " + "\n");
            strSqlString.Append("           AND A.MAT_ID IN (SELECT SRC_MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                              FROM MTAPCBMDEF@RPTTOMES " + "\n");
            strSqlString.Append("                             WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                               AND SRC_MAT_ID IN (SELECT MAT_ID " + "\n");
            strSqlString.Append("                                                    FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                                                   WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                                     AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU') " + "\n");
            strSqlString.Append("                                                     AND UPPER(MAT_GRP_5) IN ('13TH', '13 TH'))) " + "\n");
            strSqlString.Append("         GROUP BY B.MAT_GRP_10, A.WORK_DATE " + "\n");
			// 2016-12-13-정비재 : 14차 Stack
            strSqlString.Append("        UNION ALL " + "\n");
            strSqlString.Append("        SELECT B.MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("             , A.WORK_DATE AS WORK_DATE " + "\n");
            strSqlString.Append("             , '14차' AS DEGREE " + "\n");
            strSqlString.Append("             , ROUND(SUM(A.S1_OPER_IN_QTY_1 + A.S2_OPER_IN_QTY_1 + A.S3_OPER_IN_QTY_1) / 1000) AS QTY_1 " + "\n");
            strSqlString.Append("          FROM RSUMWIPMOV A " + "\n");
            strSqlString.Append("             , (SELECT FACTORY AS FACTORY " + "\n");
            strSqlString.Append("                     , MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                     , MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU')) B " + "\n");
            strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("           AND A.WORK_DATE BETWEEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "- 13, 'YYYYMMDD') AND TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + ", 'YYYYMMDD') " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND A.OPER = 'A0000' " + "\n");
            strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE 'SE%' " + "\n");
            strSqlString.Append("           AND A.MAT_ID IN (SELECT SRC_MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                              FROM MTAPCBMDEF@RPTTOMES " + "\n");
            strSqlString.Append("                             WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                               AND SRC_MAT_ID IN (SELECT MAT_ID " + "\n");
            strSqlString.Append("                                                    FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                                                   WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                                     AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU') " + "\n");
            strSqlString.Append("                                                     AND UPPER(MAT_GRP_5) IN ('14TH', '14 TH'))) " + "\n");
            strSqlString.Append("         GROUP BY B.MAT_GRP_10, A.WORK_DATE " + "\n");
			// 2016-12-13-정비재 : 15차 Stack
            strSqlString.Append("        UNION ALL " + "\n");
            strSqlString.Append("        SELECT B.MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("             , A.WORK_DATE AS WORK_DATE " + "\n");
            strSqlString.Append("             , '15차' AS DEGREE " + "\n");
            strSqlString.Append("             , ROUND(SUM(A.S1_OPER_IN_QTY_1 + A.S2_OPER_IN_QTY_1 + A.S3_OPER_IN_QTY_1) / 1000) AS QTY_1 " + "\n");
            strSqlString.Append("          FROM RSUMWIPMOV A " + "\n");
            strSqlString.Append("             , (SELECT FACTORY AS FACTORY " + "\n");
            strSqlString.Append("                     , MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                     , MAT_GRP_10 AS MAT_GRP_10 " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                   AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU')) B " + "\n");
            strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("           AND A.WORK_DATE BETWEEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + "- 13, 'YYYYMMDD') AND TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "')" + ", 'YYYYMMDD') " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND A.OPER = 'A0000' " + "\n");
            strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE 'SE%' " + "\n");
            strSqlString.Append("           AND A.MAT_ID IN (SELECT SRC_MAT_ID AS MAT_ID " + "\n");
            strSqlString.Append("                              FROM MTAPCBMDEF@RPTTOMES " + "\n");
            strSqlString.Append("                             WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                               AND SRC_MAT_ID IN (SELECT MAT_ID " + "\n");
            strSqlString.Append("                                                    FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                                                   WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                                     AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                                                     AND MAT_GRP_4 NOT IN ('-', 'FD', 'FU') " + "\n");
            strSqlString.Append("                                                     AND UPPER(MAT_GRP_5) IN ('15TH', '15 TH'))) " + "\n");
            strSqlString.Append("         GROUP BY B.MAT_GRP_10, A.WORK_DATE) " + "\n");
            strSqlString.Append("  GROUP BY MAT_GRP_10, DEGREE " + "\n");
            strSqlString.Append("  ORDER BY MAT_GRP_10 ASC, DEGREE ASC " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region fnMakeChart

        private void fnMakeChart(Miracom.SmartWeb.UI.Controls.udcFarPoint SS, int iselrow, string pkg2)
        {
            /****************************************************
             * Comment : SS의 데이터를 Chart에 표시한다.
             * 
             * Created By : min-woo kim(2013-05-09-화요일)
             * 
             * Modified By : min-woo kim(2013-05-09-화요일)
             ****************************************************/
            int i = 0;
            int icol = 0, irow = 0 ;
            int chartRow = 0;
            int[] ich_mm = new int[14];
            int[] icols_mm = new int[14];
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                // Spread Cell 클릭 시 해당 PKG2 행 카운트
                for (irow = 0; irow < iselrow; irow++)
                {
                    if (SS.Sheets[0].Cells[irow, 0].Text == pkg2)
                    {
                        i++;
                    }
                }

                // 2015-07-14-정비재 : 선택한 항목에 대한 배열 만큼 설정한다.
                int[] irows_mm = new int[i];
                string[] stitle_mm = new string[i];

                for (irow = 0; irow < iselrow; irow++)
                {
                    if (SS.Sheets[0].Cells[irow, 0].Text == pkg2)
                    {
                        irows_mm[chartRow] = irow;
                        stitle_mm[chartRow] = SS.Sheets[0].Cells[irow, 1].Text;
                        chartRow++;
                    }
                }

                for (icol = 0; icol < ich_mm.Length; icol++)
                {
                    icols_mm[icol] = icol + 2;
                }

                // 2015-07-13-정비재 : 위의 chartfx에 관련된 logic을 mschart로 수정한 것
                udcMSChart1.RPT_1_ChartInit();
                udcMSChart1.RPT_2_ClearData();
                udcMSChart1.ChartAreas[0].AxisX.Title = pkg2;
                udcMSChart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Tahoma", 10.25F);
                udcMSChart1.ChartAreas[0].AxisY.Title = "CHIP warehousing";
                udcMSChart1.RPT_3_OpenData(irows_mm.Length, icols_mm.Length);
                double max1 = udcMSChart1.RPT_4_AddData(SS, irows_mm, icols_mm, SeriseType.Rows);
                udcMSChart1.RPT_5_CloseData();
                udcMSChart1.RPT_6_SetGallery(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.05);
                udcMSChart1.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, 2);
                udcMSChart1.RPT_8_SetSeriseLegend(stitle_mm, System.Windows.Forms.DataVisualization.Charting.Docking.Top);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion
               

        #region ToExcel

        /// <summary>
        /// ToExcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1, this.lblTitle.Text, null, null);
            ExcelHelper.Instance.subMakeMsChartExcel(spdData, udcMSChart1, this.lblTitle.Text, null, null);
        }

        #endregion


		private void spdData_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
		{
			/****************************************************
			 * comment : spread sheet의 cell이 변경되면
			 * 
			 * created by : bee-jae jung(2016-10-04-화요일)
			 * 
			 * modified by : bee-jae jung(2016-10-04-화요일)
			 ****************************************************/
			String pkg2;
			try
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

				pkg2 = spdData.ActiveSheet.Cells[e.Row, 0].Value.ToString();

				fnMakeChart(spdData, spdData.ActiveSheet.RowCount, pkg2);
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox(ex.Message);
			}
			finally
			{
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
		}
      
    }
}