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
    /// 클  래  스: RAS020308<br/>
    /// 클래스요약: 순간정지 설비별 점유율<br/>
    /// 작  성  자: 미라콤 양형석<br/>
    /// 최초작성일: 2009-01-07<br/>
    /// 상세  설명: 순간정지 설비별 점유율 조회 화면<br/>
    /// 변경  내용: <br/>
    /// 2010-04-30-임종우 : 순간정지 TOP50 이 아닌 전체 보이도록 수정함(권순태 요청)
    /// 2010-04-30-임종우 : 날짜 조회 시간까지 조회 할 수 있도록 수정함(권준호 요청)
    /// 2010-05-03-임종우 : Chart 숨김. (Spread 속성 Dock -> Left, 맨 뒤로 보내기 설정 하면 Chart 보임)
    /// 2010-06-17-임종우 : Factory = '" + GlobalVariable.gsAssyDefaultFactory + "'으로 고정함. (순간정지는 ASSY 만 적용 되어 있음.)
    /// 2010-06-17-임종우 : 순간정지 조회 기능 추가, 설비별 현재 생산 제품 표시 (권순태 요청)
    /// 2012-10-24-김민우 : 파트별 순간정지 조회 기능 추가  (권순태 요청)
    /// </summary>
    public partial class RAS020308 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public RAS020308()
        {
            InitializeComponent();
            //cdvFromToDate.AutoBinding();            

            udcFromToDate.yesterday_flag = true;
            udcFromToDate.AutoBinding();

            SortInit();
            GridColumnInit();
            udcChartFX1.RPT_1_ChartInit();  //차트 초기화. 

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.cdvAlm.sFactory = GlobalVariable.gsAssyDefaultFactory;
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
            //spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            //spdData.ActiveSheet.RowHeader.ColumnCount = 0;
            spdData.RPT_ColumnInit();


            spdData.RPT_AddBasicColumn("Team in charge", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Part", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("EQP Type", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 90);
            spdData.RPT_AddBasicColumn("Maker", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Model", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);

            spdData.RPT_AddBasicColumn("Rank", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
            spdData.RPT_AddBasicColumn("Equipment name", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PRODUCT", 0, 7, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("number of cases", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);

            spdData.RPT_AddBasicColumn("Ratio", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);
            spdData.RPT_AddBasicColumn("Cumulative occupation", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 60);

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Team in charge", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_1), '-') AS TEAM", "TEAM", "RES.RES_GRP_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Part", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_2), '-') AS PART", "PART", "RES.RES_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("EQP Type", "RES.RES_GRP_3 AS EQP_TYPE", "EQP_TYPE", "RES_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Maker", "RES.RES_GRP_5 AS MAKER", "MAKER", "RES.RES_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "RES.RES_GRP_6 AS MODEL", "MODEL", "RES.RES_GRP_6", false);
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            ////////////////////////////////////////////////////
            // udcDurationDate2에서 정확한 조회시간을 받아오기
            //string strFromDate = cdvFromToDate.Start_Tran_Time;
            //string strToDate = cdvFromToDate.End_Tran_Time;
            string strFromDate = udcFromToDate.ExactFromDate;
            string strToDate = udcFromToDate.ExactToDate;
            ////////////////////////////////////////////////////

            #region " 조회조건에 해당하는 총 순간정지건수 합계 구하기 : TOT_CNT "

            string TOT_CNT = string.Empty;
            DataTable dt = null;

            strSqlString.Append("SELECT NVL(SUM(ALM.CNT), 0) TOT_CNT  " + "\n");
            strSqlString.Append("  FROM (   " + "\n");
            strSqlString.Append("            SELECT FACTORY, RES_ID, COUNT(*) CNT   " + "\n");
            //2012-09-13-김민우 CRASALMHIS 에서 MRASALMHIS로 변경
            //strSqlString.Append("              FROM CRASALMHIS@RPTTOMES " + "\n");
            strSqlString.Append("              FROM MRASALMHIS@RPTTOMES " + "\n");
            strSqlString.Append("             WHERE 1=1  " + "\n");
            strSqlString.Append("               AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "' " + "\n");
            strSqlString.Append("               AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            //strSqlString.Append("               AND CLEAR_TIME > 0 " + "\n");
            strSqlString.Append("               AND PERIOD > 0 " + "\n");
            strSqlString.Append("               AND UP_DOWN_FLAG = 'U' " + "\n");
            strSqlString.Append("               AND ALARM_USE = 'Y' " + "\n");

            // 2010-06-17-임종우 : 순간정지 조회 기능 추가
            if (cdvAlm.Text != "ALL" && cdvAlm.Text != "")
            {
                //strSqlString.Append("               AND ALARM_ID IN (SELECT ALARM_EQ_ID FROM CRASALMDEF@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND ALARM_ID = '" + cdvAlm.Text + "') " + "\n");
                strSqlString.Append("               AND ALARM_ID " + cdvAlm.SelectedValueToQueryString + "\n");
            }

            strSqlString.Append("             GROUP BY FACTORY, RES_ID  " + "\n");
            strSqlString.Append("       ) ALM " + "\n");
            strSqlString.Append("     , MRASRESDEF RES   " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND ALM.FACTORY = RES.FACTORY " + "\n");
            strSqlString.Append("   AND ALM.RES_ID = RES.RES_ID " + "\n");

            #region " RAS 상세 조회 "
            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

            strSqlString.Append("   AND RES.RES_TYPE NOT IN ('DUMMY')" + "\n");
            #endregion

            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
            if (dt != null)
                TOT_CNT = dt.Rows[0][0].ToString();

            #endregion

            #region " 반복되는 내부 쿼리 "

            string strSql = string.Empty;

            // 2010-04-30-임종우 : 전체 조회로 변경(권순태 요청)
            //strSql += "        SELECT  RNK, FACTORY, RES_ID, CNT    " + "\n";
            //strSql += "          FROM (  " + "\n";
            //strSql += "                    (  " + "\n";
            strSql += "                        SELECT  RNK, FACTORY, RES_ID, CNT    " + "\n";
            strSql += "                          FROM (   " + "\n";
            strSql += "                                SELECT ROW_NUMBER() OVER(ORDER BY CNT DESC) RNK " + "\n";
            strSql += "                                     , FACTORY   " + "\n";
            strSql += "                                     , RES_ID " + "\n";
            strSql += "                                     , CNT " + "\n";
            strSql += "                                  FROM (   " + "\n";
            strSql += "                                            SELECT FACTORY, RES_ID, COUNT(*) CNT   " + "\n";
            //strSql += "                                              FROM CRASALMHIS@RPTTOMES " + "\n";
            strSql += "                                              FROM MRASALMHIS@RPTTOMES " + "\n";
            strSql += "                                             WHERE 1=1  " + "\n";
            strSql += "                                               AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "' " + "\n";
            strSql += "                                               AND FACTORY = '" + cdvFactory.Text + "'" + "\n";
            //strSql += "                                               AND CLEAR_TIME > 0 "+ "\n";
            strSql += "                                               AND PERIOD > 0 "+ "\n";
            strSql += "                                               AND UP_DOWN_FLAG = 'U' "+ "\n";
            strSql += "                                               AND ALARM_USE = 'Y' " + "\n";
            // 2010-06-17-임종우 : 순간정지 조회 기능 추가
            if (cdvAlm.Text != "ALL" && cdvAlm.Text != "")
            {
                //strSql += "                                               AND ALARM_ID IN (SELECT ALARM_EQ_ID FROM CRASALMDEF@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND ALARM_ID = '" + cdvAlm.Text + "') " + "\n";
                strSql += "                                               AND ALARM_ID " + cdvAlm.SelectedValueToQueryString + "\n";
            }

            strSql += "                                             GROUP BY FACTORY, RES_ID " + "\n";
            strSql += "                                       ) " + "\n";
            strSql += "                               )   " + "\n";
            //strSql += "                         WHERE RNK < 51  " + "\n";
            //strSql += "                    )  " + "\n";
            //strSql += "                    UNION ALL  " + "\n";
            //strSql += "                    (  " + "\n";
            //strSql += "                        SELECT  51, FACTORY , 'OTHER', CNT      " + "\n";
            //strSql += "                          FROM (   " + "\n";
            //strSql += "                                SELECT ROW_NUMBER() OVER(ORDER BY CNT DESC) RNK " + "\n";
            //strSql += "                                     , FACTORY   " + "\n";
            //strSql += "                                     , RES_ID " + "\n";
            //strSql += "                                     , CNT " + "\n";
            //strSql += "                                  FROM (   " + "\n";
            //strSql += "                                            SELECT FACTORY, RES_ID, COUNT(*) CNT   " + "\n";
            //strSql += "                                              FROM CRASALMHIS@RPTTOMES  " + "\n";
            //strSql += "                                             WHERE 1=1  " + "\n";
            //strSql += "                                               AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "' " + "\n";
            //strSql += "                                               AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n";
            //strSql += "                                             GROUP BY FACTORY, RES_ID " + "\n";
            //strSql += "                                       ) " + "\n";
            //strSql += "                               )   " + "\n";
            //strSql += "                         WHERE RNK > 50  " + "\n";
            //strSql += "                    )  " + "\n";
            //strSql += "               ) " + "\n";

            #endregion

            strSqlString.Remove(0, strSqlString.Length - 1);
            strSqlString.Append("SELECT  " + QueryCond1 + "\n");
            strSqlString.Append("     , TO_CHAR(A.RNK) RNK " + "\n");
            strSqlString.Append("     , A.RES_ID " + "\n");
            strSqlString.Append("     , RES.RES_STS_2 " + "\n");
            strSqlString.Append("     , A.CNT  " + "\n");
            strSqlString.Append("     , ROUND(RATIO_TO_REPORT(A.CNT) OVER(), 4)*100 RATIO  " + "\n");
            strSqlString.Append("     , ROUND(SUM(B.CNT)/" + TOT_CNT + ",4)*100 ADDED_RATIO  " + "\n");
            strSqlString.Append("  FROM (  " + "\n");
            strSqlString.Append(strSql);
            strSqlString.Append("       ) A  " + "\n");
            strSqlString.Append("     , (  " + "\n");
            strSqlString.Append(strSql);
            strSqlString.Append("       ) B " + "\n");
            strSqlString.Append("     , MRASRESDEF RES  " + "\n");
            strSqlString.Append(" WHERE 1=1  " + "\n");
            strSqlString.Append("   AND A.RNK > B.RNK(+)-1  " + "\n");
            strSqlString.Append("   AND A.CNT IS NOT NULL  " + "\n");
            strSqlString.Append("   AND A.FACTORY = RES.FACTORY  " + "\n");
            strSqlString.Append("   AND A.RES_ID = RES.RES_ID " + "\n");

            #region " RAS 상세 조회 "
            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

            strSqlString.Append("   AND RES.RES_TYPE NOT IN ('DUMMY')" + "\n");
            #endregion

            strSqlString.Append(" GROUP BY RES.FACTORY, " + QueryCond3 + ", A.RNK, A.RES_ID, RES.RES_STS_2, A.CNT " + "\n");
            strSqlString.Append(" ORDER BY A.RNK  " + "\n");

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
            // 차트 설정
            udcChartFX1.RPT_1_ChartInit();
            udcChartFX1.RPT_2_ClearData();

            DataTable dt = (DataTable)spdData.DataSource;
            rowCount = dt.Rows.Count;
            if (rowCount < 1)
                return;

            udcChartFX1.RPT_3_OpenData(2, rowCount - 1);
            int[] cnt_rows = new Int32[rowCount - 1];

            for (int i = 1; i < cnt_rows.Length + 1; i++)
            {
                cnt_rows[i - 1] = i;
            }

            // 건수
            double cnt = udcChartFX1.RPT_4_AddData(spdData, cnt_rows, new int[] { 7 }, SeriseType.Column);

            // 누적점유율
            double percent = udcChartFX1.RPT_4_AddData(spdData, cnt_rows, new int[] { 9 }, SeriseType.Column);

            udcChartFX1.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 0, 1, "건수", AsixType.Y, DataTypes.Initeger, cnt * 1.2);
            udcChartFX1.RPT_6_SetGallery(ChartType.Line, 1, 1, "누적점유", AsixType.Y2, DataTypes.Initeger, percent);

            //각 Serise별로 동일한 타입을 사용할 경우
            //udcChartFX1.RPT_6_SetGallery(ChartType.Bar, "[단위 : sls]", AsixType.Y, DataTypes.Initeger, yield * 1.2);

            udcChartFX1.RPT_7_SetXAsixTitleUsingSpread(spdData, cnt_rows, 6);
            udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "건수", "누적점유" }, SoftwareFX.ChartFX.Docked.Top);

            // 기타 설정
            udcChartFX1.PointLabels = false;
            udcChartFX1.Series[1].LineWidth = 2;
            udcChartFX1.AxisY.LabelsFormat.Decimals = 0;
            udcChartFX1.AxisY.DataFormat.Decimals = 0;
            udcChartFX1.AxisY2.DataFormat.Format = SoftwareFX.ChartFX.AxisFormat.Number;
            udcChartFX1.AxisY2.DataFormat.Decimals = 2;
            udcChartFX1.AxisX.LabelAngle = 90;
        }

        #endregion

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (CheckField() == false) return;

            GridColumnInit();
            udcChartFX1.RPT_1_ChartInit();

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

                //by John
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 8, null, null);
                //spdData.DataSource = dt;

                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 8, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit4
                spdData.RPT_AutoFit(false);

                //spdData.RPT_FillColumnData(14, new string[] { "Yield", "In Qty", "Out Qty", "Loss" });

                // 누적점유율 수정
                spdData.ActiveSheet.Cells[0, 9, 0, 10].Value = 100.00;

                //ShowChart(0);
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
            //cdvProduct.sFactory = cdvFactory.txtValue;
        }

        #endregion

        // 2012-10-24-김민우 : 파트별 순간정지 조회 기능 추가  (권순태 요청)
        private void cdvAlm_ValueButtonPress(object sender, EventArgs e)
        {
            cdvAlm.AutoBinding();

            string strQuery = string.Empty; 
            strQuery += "SELECT DISTINCT ALARM_ID AS Code, ALARM_DESC AS Data FROM MESMGR.MRASALMDEF@RPTTOMES" + "\n";
            strQuery += "  WHERE MODEL IN ( " + "\n";
            strQuery += "                  SELECT DISTINCT RES_GRP_6 FROM MRASRESDEF@RPTTOMES " + "\n";
            strQuery += "                   WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n";
            strQuery += "                     AND RES_GRP_3 " + udcRASCondition3.SelectedValueToQueryString + "\n";
            strQuery += "                     AND RES_CMF_9 = 'Y'" + "\n";
            strQuery += "                     AND RES_ID NOT LIKE 'BW$%'" + "\n";
            strQuery += "                  ) " + "\n";
            strQuery += "    AND ALARM_USE = 'Y' " + "\n";
            strQuery += "  ORDER BY ALARM_ID" + "\n";
            cdvAlm.sDynamicQuery = strQuery;

        }
    }
}