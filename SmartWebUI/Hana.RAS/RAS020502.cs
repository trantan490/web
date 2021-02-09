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
    /// 클  래  스: RAS020502<br/>
    /// 클래스요약: 순간정지 MTBA TREND 현황<br/>
    /// 작  성  자: 김민우<br/>
    /// 최초작성일: 2011-03-24<br/>
    /// 상세  설명: 순간정지 MTBA TREND 현황 <br/>
    /// 변경  내용: <br/>
    /// 
    /// </summary>
    public partial class RAS020502 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public RAS020502()
        {
            InitializeComponent();
            //udcFromToDate.yesterday_flag = true;
            //udcFromToDate.AutoBinding();
            //udcFromToDate.DaySelector.SelectedValue = "DAY";
            SortInit();
            cdvBaseDate.Value = DateTime.Today;
            GridColumnInit();
            cf02.RPT_1_ChartInit();  //차트 초기화.

            this.cdvResMaker.sFactory = GlobalVariable.gsAssyDefaultFactory;
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
            //월
            string squery = "";
            DataTable dt = null;

            //주차
            string squery2 = "";
            DataTable dtWeek = null;

            string todate = cdvBaseDate.Value.ToString("yyyyMMdd");
            // 월
            squery = "SELECT SUBSTR(TO_CHAR(TO_DATE('" + todate + "'),'YYYYMM'),5,2)||'월' AS MM3 \n"
                               + "	   , SUBSTR(TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-1),'YYYYMM'),5,2)||'월' AS MM2 \n"
                               + "  FROM DUAL";

            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", squery.Replace((char)Keys.Tab, ' '));
            // 주

            squery2 = "SELECT DECODE(PLAN_WEEK - 3,-2,51, PLAN_WEEK - 3)||'주' AS WW1 \n"
                               + "      , DECODE(PLAN_WEEK - 2,-1,52, PLAN_WEEK - 2)||'주' AS WW2 \n"
                               + "	    , DECODE(PLAN_WEEK - 1, 0,53, PLAN_WEEK - 1)||'주' AS WW3 \n"
                               + "	    , PLAN_WEEK||'주' AS WW4 \n"
                               + "	 FROM MWIPCALDEF \n"
                               + "	WHERE CALENDAR_ID = 'HM' \n"
                               + "	  AND SYS_DATE = TO_CHAR(TO_DATE('" + todate + "','YYYYMMDD'),'YYYYMMDD')";

            dtWeek = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", squery2.Replace((char)Keys.Tab, ' '));


            spdData.RPT_ColumnInit();


            spdData.RPT_AddBasicColumn("구분", 0, 0, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn(dt.Rows[0][1].ToString(), 0, 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
            spdData.RPT_AddBasicColumn(dt.Rows[0][0].ToString(), 0, 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
            spdData.RPT_AddBasicColumn(dtWeek.Rows[0][0].ToString(), 0, 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
            spdData.RPT_AddBasicColumn(dtWeek.Rows[0][1].ToString(), 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
            spdData.RPT_AddBasicColumn(dtWeek.Rows[0][2].ToString(), 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
            spdData.RPT_AddBasicColumn(dtWeek.Rows[0][3].ToString(), 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
          
            //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
           
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

           

            #region " 반복되는 내부 쿼리 "

            string strSql = string.Empty;
            strSql += "        SELECT '건수'   " + "\n";
            strSql += "              , SUM(DECODE(WORK_MONTH,'201102',ALARM_CNT)) AS MON1 " + "\n";
            strSql += "              , SUM(DECODE(WORK_MONTH,'201103',ALARM_CNT)) AS MON2 " + "\n";
            strSql += "              , SUM(DECODE(WORK_WEEK,'201113',ALARM_CNT)) AS WEEK1 " + "\n";
            strSql += "              , SUM(DECODE(WORK_WEEK,'201112',ALARM_CNT)) AS WEEK2 " + "\n";
            strSql += "              , SUM(DECODE(WORK_WEEK,'201111',ALARM_CNT)) AS WEEK3 " + "\n";
            strSql += "              , SUM(DECODE(WORK_WEEK,'201110',ALARM_CNT)) AS WEEK4 " + "\n";
            strSql += "          FROM CSUMALMRES@RPTTOMES " + "\n";
            strSql += "         WHERE WORK_MONTH BETWEEN '201102' AND '201103' " + "\n";
            
            strSql += "        UNION ALL " + "\n";
            strSql += "        SELECT '가동시간'   " + "\n";
            strSql += "              , ROUND(SUM(DECODE(WORK_MONTH,'201102',RUN_TIME))) AS MON1 " + "\n";
            strSql += "              , ROUND(SUM(DECODE(WORK_MONTH,'201103',RUN_TIME))) AS MON2 " + "\n";
            strSql += "              , ROUND(SUM(DECODE(WORK_WEEK,'201113',RUN_TIME))) AS WEEK1 " + "\n";
            strSql += "              , ROUND(SUM(DECODE(WORK_WEEK,'201112',RUN_TIME))) AS WEEK2 " + "\n";
            strSql += "              , ROUND(SUM(DECODE(WORK_WEEK,'201111',RUN_TIME))) AS WEEK3 " + "\n";
            strSql += "              , ROUND(SUM(DECODE(WORK_WEEK,'201110',RUN_TIME))) AS WEEK4 " + "\n";
            strSql += "          FROM CSUMALMRES@RPTTOMES " + "\n";
            strSql += "         WHERE WORK_MONTH BETWEEN '201102' AND '201103' " + "\n";

            strSql += "        UNION ALL " + "\n";
            strSql += "        SELECT '적용대수'   " + "\n";
            strSql += "              , ROUND(COUNT(DECODE(WORK_MONTH,'201102',RES_ID))/28) AS MON1 " + "\n";
            strSql += "              , ROUND(COUNT(DECODE(WORK_MONTH,'201103',RES_ID))/25) AS MON2 " + "\n";
            strSql += "              , ROUND(COUNT(DECODE(WORK_WEEK,'201113',RES_ID))/7) AS WEEK1 " + "\n";
            strSql += "              , ROUND(COUNT(DECODE(WORK_WEEK,'201112',RES_ID))/7) AS WEEK2 " + "\n";
            strSql += "              , ROUND(COUNT(DECODE(WORK_WEEK,'201111',RES_ID))/7) AS WEEK3 " + "\n";
            strSql += "              , ROUND(COUNT(DECODE(WORK_WEEK,'201110',RES_ID))/7) AS WEEK4 " + "\n";
            strSql += "          FROM CSUMALMRES@RPTTOMES " + "\n";
            strSql += "         WHERE WORK_MONTH BETWEEN '201102' AND '201103' " + "\n";

            strSql += "        UNION ALL " + "\n";
            strSql += "        SELECT 'MTBA(분)'   " + "\n";
            strSql += "              , ROUND(ROUND(SUM(DECODE(WORK_MONTH,'201102',RUN_TIME)))/SUM(DECODE(WORK_MONTH,'201102',ALARM_CNT)),1) AS MON1 " + "\n";
            strSql += "              , ROUND(ROUND(SUM(DECODE(WORK_MONTH,'201103',RUN_TIME)))/SUM(DECODE(WORK_MONTH,'201103',ALARM_CNT)),1) AS MON2 " + "\n";
            strSql += "              , ROUND(ROUND(SUM(DECODE(WORK_WEEK,'201113',RUN_TIME)))/SUM(DECODE(WORK_WEEK,'201113',ALARM_CNT)),1) AS WEEK1 " + "\n";
            strSql += "              , ROUND(ROUND(SUM(DECODE(WORK_WEEK,'201112',RUN_TIME)))/SUM(DECODE(WORK_WEEK,'201112',ALARM_CNT)),1) AS WEEK2 " + "\n";
            strSql += "              , ROUND(ROUND(SUM(DECODE(WORK_WEEK,'201111',RUN_TIME)))/SUM(DECODE(WORK_WEEK,'201111',ALARM_CNT)),1) AS WEEK3 " + "\n";
            strSql += "              , ROUND(ROUND(SUM(DECODE(WORK_WEEK,'201110',RUN_TIME)))/SUM(DECODE(WORK_WEEK,'201110',ALARM_CNT)),1) AS WEEK4 " + "\n";
            strSql += "          FROM CSUMALMRES@RPTTOMES " + "\n";
            strSql += "         WHERE WORK_MONTH BETWEEN '201102' AND '201103' " + "\n";


            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSql.ToString());
            }

            return strSql.ToString();
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
            cf02.RPT_1_ChartInit();
            cf02.RPT_2_ClearData();

            DataTable dt = (DataTable)spdData.DataSource;
            rowCount = dt.Rows.Count;
            if (rowCount < 1)
                return;

            cf02.RPT_3_OpenData(2, rowCount-1);
            int[] cnt_rows = new Int32[rowCount-1];

            for (int i = 1; i < cnt_rows.Length+1; i++)
            {
                cnt_rows[i-1] = i;
            }

            // 건수
            double cnt = cf02.RPT_4_AddData(spdData, cnt_rows, new int[] { 9 }, SeriseType.Column);

            // 누적점유율
            double percent = cf02.RPT_4_AddData(spdData, cnt_rows, new int[] { 11 }, SeriseType.Column);

            cf02.RPT_5_CloseData();

            //각 Serise별로 다른 타입을 사용할 경우
            cf02.RPT_6_SetGallery(ChartType.Bar, 0, 1, "건수", AsixType.Y, DataTypes.Initeger, cnt * 1.2);
            cf02.RPT_6_SetGallery(ChartType.Line, 1, 1, "누적점유", AsixType.Y2, DataTypes.Initeger, percent);

            //각 Serise별로 동일한 타입을 사용할 경우
            //udcChartFX1.RPT_6_SetGallery(ChartType.Bar, "[단위 : sls]", AsixType.Y, DataTypes.Initeger, yield * 1.2);

            cf02.RPT_7_SetXAsixTitleUsingSpread(spdData, cnt_rows, 8);
            cf02.RPT_8_SetSeriseLegend(new string[] { "건수", "누적점유" }, SoftwareFX.ChartFX.Docked.Top);
            
            // 기타 설정
            cf02.PointLabels = false;
            cf02.Series[1].LineWidth = 2;
            cf02.AxisY.LabelsFormat.Decimals = 0;
            cf02.AxisY.DataFormat.Decimals = 0;
            cf02.AxisY2.DataFormat.Format = SoftwareFX.ChartFX.AxisFormat.Number;
            cf02.AxisY2.DataFormat.Decimals = 2;
            cf02.AxisX.LabelAngle = 90;
        }

        #endregion

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

                //by John
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 2, null, null);
                //spdData.DataSource = dt;

                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                //3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 3, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit4
                spdData.RPT_AutoFit(false);

                //spdData.RPT_FillColumnData(14, new string[] { "Yield", "In Qty", "Out Qty", "Loss" });

                // 누적점유율 수정
                spdData.ActiveSheet.Cells[0,5].Value = 100.00;

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

        // 설비 Maker 가져오기
        private void cdvResMaker_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;
            strQuery += "SELECT DISTINCT RES_MAKER AS Code, '' AS Data FROM CSUMALMRES@RPTTOMES" + "\n";
            cdvResMaker.sDynamicQuery = strQuery;

        }

    }
} 
#endregion