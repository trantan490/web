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
using System.Collections;

namespace Hana.TAT
{
    public partial class TAT050801 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

   
        /// <summary>
        /// 클  래  스: TAT050801<br/>
        /// 클래스요약: 고객사별 PKG별 월,주,일 TAT<br/>
        /// 작  성  자: 김민우 <br/>
        /// 최초작성일: 2011-04-18<br/>
        /// 상세  설명: 월,주,일 TAT trend 를 조회한다.<br/>
        /// 
        /// 변경  내용: 
        /// 2011-06-04-김민우 : Lot type P% / E% 로 조회기능 추가 (임태성 요청)
        /// 2011-06-06-김민우 : 투입대기(HMK2) / 출하대기(HMK3) Wait time 제외하여 조회할 수 있는 기능 추가 (임태성 요청)
        /// </summary>
        /// 


        //private DataTable dtDate = null;
        private String stLotType = null;
        private String stCheckdData = null;    // 투입대기, 출하대기 체크 선택 유무에 따라 Data 쿼리 저장
        private String stCheckdObject = null;  // 투입대기, 출하대기 체크 선택 유무에 따라 목표 쿼리 저장

        //월 가져오기
        string squery = "";
        DataTable dtMonth = null;
        //주차
        string squery2 = "";
        DataTable dtWeek = null;
        //일가져오기
        string squery3 = "";
        DataTable dtDay = null;
        // 주차 시작일, 마지막일 구하기
        string squery4 = "";
        DataTable dtWeekDay = null;

        DataTable dtLast = null;
        public TAT050801()
        {
            InitializeComponent();
            OptionInIt(); // 초기화
            cdvBaseDate.Value = DateTime.Today.AddDays(-1);
            
            SortInit();
            GridColumnInit(); //헤더 한줄짜리 
            udcChartFX1.RPT_1_ChartInit();  //차트 초기화. 
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            cbLotType.Text = "ALL";
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
            

            //2011-04-19 김민우: Spread 타이틀 동적으로 수정(월)
            string todate = cdvBaseDate.Value.ToString("yyyyMMdd");

            squery = "SELECT TO_CHAR(TO_DATE('" + todate + "'),'YYYYMMDD') AS MM1 \n"
                   + "	   , TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-1),'YYYYMMDD') AS MM2 \n"
                   + "	   , TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "'),-2),'YYYYMMDD') AS MM3 \n"
                   + "  FROM DUAL";
            dtMonth = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", squery.Replace((char)Keys.Tab, ' '));

            //2011-04-19 김민우: Spread 타이틀 동적으로 수정(주)
            squery2 = "SELECT DECODE(PLAN_WEEK - 2,-1,52,0,53, PLAN_WEEK - 2) AS WW3 \n"
                   + "	    , DECODE(PLAN_WEEK - 1, 0,53, PLAN_WEEK - 1) AS WW2 \n"
                   + "	    , PLAN_WEEK AS WW1 \n"
                   + "	 FROM MWIPCALDEF \n"
                   + "	WHERE CALENDAR_ID = 'HM' \n"
                   + "	  AND SYS_DATE = TO_CHAR(TO_DATE('" + todate + "','YYYYMMDD'),'YYYYMMDD')";

            dtWeek = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", squery2.Replace((char)Keys.Tab, ' '));

            squery3 = "SELECT TO_CHAR(TO_DATE('" + todate + "'),'YYYYMMDD') AS DD \n"
                               + "      , TO_CHAR(TO_DATE('" + todate + "')-1,'YYYYMMDD') AS DD1 \n"
                               + "      , TO_CHAR(TO_DATE('" + todate + "')-2,'YYYYMMDD') AS DD2 \n"
                               + "      , TO_CHAR(TO_DATE('" + todate + "')-3,'YYYYMMDD') AS DD3 \n"
                               + "      , TO_CHAR(TO_DATE('" + todate + "')-4,'YYYYMMDD') AS DD4 \n"
                               + "      , TO_CHAR(TO_DATE('" + todate + "')-5,'YYYYMMDD') AS DD5 \n"
                               + "      , TO_CHAR(TO_DATE('" + todate + "')-6,'YYYYMMDD') AS DD6 \n"
                               + "	 FROM DUAL";
            dtDay = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", squery3.Replace((char)Keys.Tab, ' '));

            try
            {
                 
                spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("구분", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn(dtMonth.Rows[0][2].ToString().Substring(4, 2) + "월", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn(dtMonth.Rows[0][1].ToString().Substring(4, 2) + "월", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn(dtMonth.Rows[0][0].ToString().Substring(4, 2) + "월", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn(dtWeek.Rows[0][0].ToString() + "주", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn(dtWeek.Rows[0][1].ToString() + "주", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn(dtWeek.Rows[0][2].ToString() + "주", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn(dtDay.Rows[0][5].ToString().Substring(6, 2) + "일", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn(dtDay.Rows[0][4].ToString().Substring(6, 2) + "일", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn(dtDay.Rows[0][3].ToString().Substring(6, 2) + "일", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn(dtDay.Rows[0][2].ToString().Substring(6, 2) + "일", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn(dtDay.Rows[0][1].ToString().Substring(6, 2) + "일", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn(dtDay.Rows[0][0].ToString().Substring(6, 2) + "일", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
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

                spdData.isShowZero = true;

                spdData.DataSource = dt;
             
                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

              
             

                //Chart 생성
                fnMakeChart(spdData, spdData.ActiveSheet.RowCount);
               // if (spdData.ActiveSheet.RowCount > 0) ShowChart(0);
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

            if (udcWIPCondition1.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD038", GlobalVariable.gcLanguage));
                return false;
            }

            if (udcWIPCondition3.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD039", GlobalVariable.gcLanguage));
                return false;
            }



            return true;
        }

        #endregion

        #region OptionInIt
        private void OptionInIt()
        {
            //dtDate = null;
        }
        #endregion
                       
        #region MakeSqlString
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            stLotType = cbLotType.Text; // Lot Type
            GetLastDay();

            //------ 투입대기, 출하대기 체크 유무에 따라 해당 쿼리 생성 - 시작 -----------
            if (ckbHMK2A.Checked == false && ckbHMK3A.Checked == false)
            {
                stCheckdData = "' ','HMK2A','HMK3A'";
                stCheckdObject = "                       AND KEY_4 NOT IN ('HMK2A','HMK3A')";
            }
            else if (ckbHMK2A.Checked == false && ckbHMK3A.Checked == true)
            {
                stCheckdData = "' ','HMK2A'";
                stCheckdObject = "                       AND KEY_4 <> 'HMK2A'";
            }
            else if (ckbHMK2A.Checked == true && ckbHMK3A.Checked == false)
            {
                stCheckdData = "' ','HMK3A'";
                stCheckdObject = "                       AND KEY_4 <> 'HMK3A'";
            }
            else
            {
                stCheckdData = "' '";
                stCheckdObject = "";
            }
            //------ 투입대기, 출하대기 체크 유무에 따라 해당 쿼리 생성 - 종료 -----------



            strSqlString.Append(" SELECT 'TAT 목표' AS TARGET" + "\n");

            if (ckbTime.Checked == true)
            {
                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtMonth.Rows[0][2].ToString().Substring(0, 6) + "01' AND DATA_1 >= '" + dtMonth.Rows[0][2].ToString().Substring(0, 6) + "28' THEN SUM(DATA_2) END) * 24 AS TT" + "\n");
                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtMonth.Rows[0][1].ToString().Substring(0, 6) + "01' AND DATA_1 >= '" + dtMonth.Rows[0][1].ToString().Substring(0, 6) + "28' THEN SUM(DATA_2) END) * 24 AS TT" + "\n");
                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtMonth.Rows[0][0].ToString().Substring(0, 6) + "01' AND DATA_1 >= '" + dtMonth.Rows[0][0].ToString().Substring(0, 6) + "28' THEN SUM(DATA_2) END) *24 AS TT" + "\n");

                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtLast.Rows[0][0] + "' AND DATA_1 >= '" + dtLast.Rows[0][1] + "' THEN SUM(DATA_2) END) * 24 AS TT" + "\n");
                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtLast.Rows[1][0] + "' AND DATA_1 >= '" + dtLast.Rows[1][1] + "' THEN SUM(DATA_2) END) * 24 AS TT" + "\n");
                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtLast.Rows[2][0] + "' AND DATA_1 >= '" + dtLast.Rows[2][1] + "' THEN SUM(DATA_2) END) * 24 AS TT" + "\n");

                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtDay.Rows[0][5] + "' AND DATA_1 >= '" + dtDay.Rows[0][5] + "' THEN SUM(DATA_2) END) * 24 AS TT" + "\n");
                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtDay.Rows[0][4] + "' AND DATA_1 >= '" + dtDay.Rows[0][4] + "' THEN SUM(DATA_2) END) * 24 AS TT" + "\n");
                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtDay.Rows[0][3] + "' AND DATA_1 >= '" + dtDay.Rows[0][3] + "' THEN SUM(DATA_2) END) * 24 AS TT" + "\n");
                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtDay.Rows[0][2] + "' AND DATA_1 >= '" + dtDay.Rows[0][2] + "' THEN SUM(DATA_2) END) * 24 AS TT" + "\n");
                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtDay.Rows[0][1] + "' AND DATA_1 >= '" + dtDay.Rows[0][1] + "' THEN SUM(DATA_2) END) * 24 AS TT" + "\n");
                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtDay.Rows[0][0] + "' AND DATA_1 >= '" + dtDay.Rows[0][0] + "' THEN SUM(DATA_2) END) * 24 AS TT" + "\n");

            }
            else
            {
                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtMonth.Rows[0][2].ToString().Substring(0, 6) + "01' AND DATA_1 >= '" + dtMonth.Rows[0][2].ToString().Substring(0, 6) + "28' THEN SUM(DATA_2) END) AS TT" + "\n");
                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtMonth.Rows[0][1].ToString().Substring(0, 6) + "01' AND DATA_1 >= '" + dtMonth.Rows[0][1].ToString().Substring(0, 6) + "28' THEN SUM(DATA_2) END) AS TT" + "\n");
                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtMonth.Rows[0][0].ToString().Substring(0, 6) + "01' AND DATA_1 >= '" + dtMonth.Rows[0][0].ToString().Substring(0, 6) + "28' THEN SUM(DATA_2) END) AS TT" + "\n");

                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtLast.Rows[0][0] + "' AND DATA_1 >= '" + dtLast.Rows[0][1] + "' THEN SUM(DATA_2) END) AS TT" + "\n");
                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtLast.Rows[1][0] + "' AND DATA_1 >= '" + dtLast.Rows[1][1] + "' THEN SUM(DATA_2) END) AS TT" + "\n");
                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtLast.Rows[2][0] + "' AND DATA_1 >= '" + dtLast.Rows[2][1] + "' THEN SUM(DATA_2) END) AS TT" + "\n");

                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtDay.Rows[0][5] + "' AND DATA_1 >= '" + dtDay.Rows[0][5] + "' THEN SUM(DATA_2) END) AS TT" + "\n");
                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtDay.Rows[0][4] + "' AND DATA_1 >= '" + dtDay.Rows[0][4] + "' THEN SUM(DATA_2) END) AS TT" + "\n");
                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtDay.Rows[0][3] + "' AND DATA_1 >= '" + dtDay.Rows[0][3] + "' THEN SUM(DATA_2) END) AS TT" + "\n");
                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtDay.Rows[0][2] + "' AND DATA_1 >= '" + dtDay.Rows[0][2] + "' THEN SUM(DATA_2) END) AS TT" + "\n");
                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtDay.Rows[0][1] + "' AND DATA_1 >= '" + dtDay.Rows[0][1] + "' THEN SUM(DATA_2) END) AS TT" + "\n");
                strSqlString.Append("       , MAX(CASE WHEN KEY_1 <= '" + dtDay.Rows[0][0] + "' AND DATA_1 >= '" + dtDay.Rows[0][0] + "' THEN SUM(DATA_2) END) AS TT" + "\n");
            }

            
            
            strSqlString.Append("   FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("  WHERE 1=1" + "\n");
            strSqlString.Append("    AND TABLE_NAME = 'H_RPT_TAT_MAINOBJECT'" + "\n");
            strSqlString.Append("    AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("    AND KEY_1 <= '29991231'" + "\n");
            strSqlString.Append("    AND DATA_1 >= '20100101'" + "\n");
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("    AND KEY_2 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("    AND KEY_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);
            strSqlString.Append(stCheckdObject + "\n");
            strSqlString.Append("  GROUP BY KEY_1,DATA_1" + "\n");
            strSqlString.Append(" UNION ALL" + "\n");
            
            
            
            
            strSqlString.Append(" SELECT DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000','TAT','001','WAIT','011','RUN','ETC') AS GUBUN" + "\n");
            if (ckbTime.Checked == true)
            {
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',MON_TAT1,'001',SUM(MON_WAIT_TAT1),'011',SUM(MON_PROC_TAT1),0),2) * 24 AS MON_TAT1" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',MON_TAT2,'001',SUM(MON_WAIT_TAT2),'011',SUM(MON_PROC_TAT2),0),2) * 24 AS MON_TAT2" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',MON_TAT3,'001',SUM(MON_WAIT_TAT3),'011',SUM(MON_PROC_TAT3),0),2) * 24 AS MON_TAT3" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',SUM(WEEK_TAT1),'001',SUM(WEEK_WAIT_TAT1),'011',SUM(WEEK_PROC_TAT1),0),2) * 24 AS WEEK_TAT1" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',SUM(WEEK_TAT2),'001',SUM(WEEK_WAIT_TAT2),'011',SUM(WEEK_PROC_TAT2),0),2) * 24 AS WEEK_TAT2" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',SUM(WEEK_TAT3),'001',SUM(WEEK_WAIT_TAT3),'011',SUM(WEEK_PROC_TAT3),0),2) * 24 AS WEEK_TAT3" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',SUM(DAY_TAT1),'001',SUM(DAY_WAIT_TAT1),'011',SUM(DAY_PROC_TAT1),0),2) * 24 AS DAY_TAT1" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',SUM(DAY_TAT2),'001',SUM(DAY_WAIT_TAT2),'011',SUM(DAY_PROC_TAT2),0),2) * 24 AS DAY_TAT2" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',SUM(DAY_TAT3),'001',SUM(DAY_WAIT_TAT3),'011',SUM(DAY_PROC_TAT3),0),2) * 24 AS DAY_TAT3" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',SUM(DAY_TAT4),'001',SUM(DAY_WAIT_TAT4),'011',SUM(DAY_PROC_TAT4),0),2) * 24 AS DAY_TAT4" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',SUM(DAY_TAT5),'001',SUM(DAY_WAIT_TAT5),'011',SUM(DAY_PROC_TAT5),0),2) * 24 AS DAY_TAT5" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',SUM(DAY_TAT6),'001',SUM(DAY_WAIT_TAT6),'011',SUM(DAY_PROC_TAT6),0),2) * 24 AS DAY_TAT6" + "\n");
            }
            else
            {
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',MON_TAT1,'001',SUM(MON_WAIT_TAT1),'011',SUM(MON_PROC_TAT1),0),2) AS MON_TAT1" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',MON_TAT2,'001',SUM(MON_WAIT_TAT2),'011',SUM(MON_PROC_TAT2),0),2) AS MON_TAT2" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',MON_TAT3,'001',SUM(MON_WAIT_TAT3),'011',SUM(MON_PROC_TAT3),0),2) AS MON_TAT3" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',SUM(WEEK_TAT1),'001',SUM(WEEK_WAIT_TAT1),'011',SUM(WEEK_PROC_TAT1),0),2) AS WEEK_TAT1" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',SUM(WEEK_TAT2),'001',SUM(WEEK_WAIT_TAT2),'011',SUM(WEEK_PROC_TAT2),0),2) AS WEEK_TAT2" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',SUM(WEEK_TAT3),'001',SUM(WEEK_WAIT_TAT3),'011',SUM(WEEK_PROC_TAT3),0),2) AS WEEK_TAT3" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',SUM(DAY_TAT1),'001',SUM(DAY_WAIT_TAT1),'011',SUM(DAY_PROC_TAT1),0),2) AS DAY_TAT1" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',SUM(DAY_TAT2),'001',SUM(DAY_WAIT_TAT2),'011',SUM(DAY_PROC_TAT2),0),2) AS DAY_TAT2" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',SUM(DAY_TAT3),'001',SUM(DAY_WAIT_TAT3),'011',SUM(DAY_PROC_TAT3),0),2) AS DAY_TAT3" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',SUM(DAY_TAT4),'001',SUM(DAY_WAIT_TAT4),'011',SUM(DAY_PROC_TAT4),0),2) AS DAY_TAT4" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',SUM(DAY_TAT5),'001',SUM(DAY_WAIT_TAT5),'011',SUM(DAY_PROC_TAT5),0),2) AS DAY_TAT5" + "\n");
                strSqlString.Append("      , ROUND(DECODE(GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3),'000',SUM(DAY_TAT6),'001',SUM(DAY_WAIT_TAT6),'011',SUM(DAY_PROC_TAT6),0),2) AS DAY_TAT6" + "\n");
            }

            strSqlString.Append("   FROM (" + "\n");
            strSqlString.Append("         SELECT TOTAL_TAT_QTY_MON1/SHIP_QTY_MON1 AS MON_TAT1" + "\n");
            strSqlString.Append("              , TOTAL_TAT_QTY_MON2/SHIP_QTY_MON2 AS MON_TAT2" + "\n");
            strSqlString.Append("              , TOTAL_TAT_QTY_MON3/SHIP_QTY_MON3 AS MON_TAT3" + "\n");
            strSqlString.Append("              , WAIT_TAT_QTY_MON1/SHIP_QTY_MON1 AS MON_WAIT_TAT1" + "\n");
            strSqlString.Append("              , WAIT_TAT_QTY_MON2/SHIP_QTY_MON2 AS MON_WAIT_TAT2" + "\n");
            strSqlString.Append("              , WAIT_TAT_QTY_MON3/SHIP_QTY_MON3 AS MON_WAIT_TAT3" + "\n");
            strSqlString.Append("              , PROC_TAT_QTY_MON1/SHIP_QTY_MON1 AS MON_PROC_TAT1" + "\n");
            strSqlString.Append("              , PROC_TAT_QTY_MON2/SHIP_QTY_MON2 AS MON_PROC_TAT2" + "\n");
            strSqlString.Append("              , PROC_TAT_QTY_MON3/SHIP_QTY_MON3 AS MON_PROC_TAT3" + "\n");
            strSqlString.Append("              , TOTAL_TAT_QTY_WEEK1/SHIP_QTY_WEEK1 AS WEEK_TAT1" + "\n");
            strSqlString.Append("              , TOTAL_TAT_QTY_WEEK2/SHIP_QTY_WEEK2 AS WEEK_TAT2" + "\n");
            strSqlString.Append("              , TOTAL_TAT_QTY_WEEK3/SHIP_QTY_WEEK3 AS WEEK_TAT3" + "\n");
            strSqlString.Append("              , WAIT_TAT_QTY_WEEK1/SHIP_QTY_WEEK1 AS WEEK_WAIT_TAT1" + "\n");
            strSqlString.Append("              , WAIT_TAT_QTY_WEEK2/SHIP_QTY_WEEK2 AS WEEK_WAIT_TAT2" + "\n");
            strSqlString.Append("              , WAIT_TAT_QTY_WEEK3/SHIP_QTY_WEEK3 AS WEEK_WAIT_TAT3" + "\n");
            strSqlString.Append("              , PROC_TAT_QTY_WEEK1/SHIP_QTY_WEEK1 AS WEEK_PROC_TAT1" + "\n");
            strSqlString.Append("              , PROC_TAT_QTY_WEEK2/SHIP_QTY_WEEK2 AS WEEK_PROC_TAT2" + "\n");
            strSqlString.Append("              , PROC_TAT_QTY_WEEK3/SHIP_QTY_WEEK3 AS WEEK_PROC_TAT3" + "\n");
            strSqlString.Append("              , TOTAL_TAT_QTY_DAY1/SHIP_QTY_DAY1 AS DAY_TAT1" + "\n");
            strSqlString.Append("              , TOTAL_TAT_QTY_DAY2/SHIP_QTY_DAY2 AS DAY_TAT2" + "\n");
            strSqlString.Append("              , TOTAL_TAT_QTY_DAY3/SHIP_QTY_DAY3 AS DAY_TAT3" + "\n");
            strSqlString.Append("              , TOTAL_TAT_QTY_DAY4/SHIP_QTY_DAY4 AS DAY_TAT4" + "\n");
            strSqlString.Append("              , TOTAL_TAT_QTY_DAY5/SHIP_QTY_DAY5 AS DAY_TAT5" + "\n");
            strSqlString.Append("              , TOTAL_TAT_QTY_DAY6/SHIP_QTY_DAY6 AS DAY_TAT6" + "\n");
            strSqlString.Append("              , WAIT_TAT_QTY_DAY1/SHIP_QTY_DAY1 AS DAY_WAIT_TAT1" + "\n");
            strSqlString.Append("              , WAIT_TAT_QTY_DAY2/SHIP_QTY_DAY2 AS DAY_WAIT_TAT2" + "\n");
            strSqlString.Append("              , WAIT_TAT_QTY_DAY3/SHIP_QTY_DAY3 AS DAY_WAIT_TAT3" + "\n");
            strSqlString.Append("              , WAIT_TAT_QTY_DAY4/SHIP_QTY_DAY4 AS DAY_WAIT_TAT4" + "\n");
            strSqlString.Append("              , WAIT_TAT_QTY_DAY5/SHIP_QTY_DAY5 AS DAY_WAIT_TAT5" + "\n");
            strSqlString.Append("              , WAIT_TAT_QTY_DAY6/SHIP_QTY_DAY6 AS DAY_WAIT_TAT6" + "\n");
            strSqlString.Append("              , PROC_TAT_QTY_DAY1/SHIP_QTY_DAY1 AS DAY_PROC_TAT1" + "\n");
            strSqlString.Append("              , PROC_TAT_QTY_DAY2/SHIP_QTY_DAY2 AS DAY_PROC_TAT2" + "\n");
            strSqlString.Append("              , PROC_TAT_QTY_DAY3/SHIP_QTY_DAY3 AS DAY_PROC_TAT3" + "\n");
            strSqlString.Append("              , PROC_TAT_QTY_DAY4/SHIP_QTY_DAY4 AS DAY_PROC_TAT4" + "\n");
            strSqlString.Append("              , PROC_TAT_QTY_DAY5/SHIP_QTY_DAY5 AS DAY_PROC_TAT5" + "\n");
            strSqlString.Append("              , PROC_TAT_QTY_DAY6/SHIP_QTY_DAY6 AS DAY_PROC_TAT6" + "\n");
            strSqlString.Append("           FROM (" + "\n");

            // 2011-06-03-김민우 : Lot_Type별 검색 추가
            if (stLotType.Equals("P%"))
            {
                strSqlString.Append("                 SELECT SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][2].ToString().Substring(4, 2) + "', SUM(TOTAL_TAT_QTY_P))) AS TOTAL_TAT_QTY_MON1 " + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][1].ToString().Substring(4, 2) + "', SUM(TOTAL_TAT_QTY_P))) AS TOTAL_TAT_QTY_MON2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][0].ToString().Substring(4, 2) + "', SUM(TOTAL_TAT_QTY_P))) AS TOTAL_TAT_QTY_MON3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][2].ToString().Substring(4, 2) + "', SUM(WAIT_TAT_QTY_P))) AS WAIT_TAT_QTY_MON1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][1].ToString().Substring(4, 2) + "', SUM(WAIT_TAT_QTY_P))) AS WAIT_TAT_QTY_MON2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][0].ToString().Substring(4, 2) + "', SUM(WAIT_TAT_QTY_P))) AS WAIT_TAT_QTY_MON3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][2].ToString().Substring(4, 2) + "', SUM(PROC_TAT_QTY_P))) AS PROC_TAT_QTY_MON1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][1].ToString().Substring(4, 2) + "', SUM(PROC_TAT_QTY_P))) AS PROC_TAT_QTY_MON2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][0].ToString().Substring(4, 2) + "', SUM(PROC_TAT_QTY_P))) AS PROC_TAT_QTY_MON3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][0] + "', SUM(TOTAL_TAT_QTY_P))) AS TOTAL_TAT_QTY_WEEK1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][1] + "', SUM(TOTAL_TAT_QTY_P))) AS TOTAL_TAT_QTY_WEEK2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][2] + "', SUM(TOTAL_TAT_QTY_P))) AS TOTAL_TAT_QTY_WEEK3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][0] + "', SUM(WAIT_TAT_QTY_P))) AS WAIT_TAT_QTY_WEEK1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][1] + "', SUM(WAIT_TAT_QTY_P))) AS WAIT_TAT_QTY_WEEK2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][2] + "', SUM(WAIT_TAT_QTY_P))) AS WAIT_TAT_QTY_WEEK3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][0] + "', SUM(PROC_TAT_QTY_P))) AS PROC_TAT_QTY_WEEK1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][1] + "', SUM(PROC_TAT_QTY_P))) AS PROC_TAT_QTY_WEEK2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][2] + "', SUM(PROC_TAT_QTY_P))) AS PROC_TAT_QTY_WEEK3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][5] + "', SUM(TOTAL_TAT_QTY_P))) AS TOTAL_TAT_QTY_DAY1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][4] + "', SUM(TOTAL_TAT_QTY_P))) AS TOTAL_TAT_QTY_DAY2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][3] + "', SUM(TOTAL_TAT_QTY_P))) AS TOTAL_TAT_QTY_DAY3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][2] + "', SUM(TOTAL_TAT_QTY_P))) AS TOTAL_TAT_QTY_DAY4" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][1] + "', SUM(TOTAL_TAT_QTY_P))) AS TOTAL_TAT_QTY_DAY5" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][0] + "', SUM(TOTAL_TAT_QTY_P))) AS TOTAL_TAT_QTY_DAY6" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][5] + "', SUM(WAIT_TAT_QTY_P))) AS WAIT_TAT_QTY_DAY1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][4] + "', SUM(WAIT_TAT_QTY_P))) AS WAIT_TAT_QTY_DAY2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][3] + "', SUM(WAIT_TAT_QTY_P))) AS WAIT_TAT_QTY_DAY3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][2] + "', SUM(WAIT_TAT_QTY_P))) AS WAIT_TAT_QTY_DAY4" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][1] + "', SUM(WAIT_TAT_QTY_P))) AS WAIT_TAT_QTY_DAY5" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][0] + "', SUM(WAIT_TAT_QTY_P))) AS WAIT_TAT_QTY_DAY6" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][5] + "', SUM(PROC_TAT_QTY_P))) AS PROC_TAT_QTY_DAY1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][4] + "', SUM(PROC_TAT_QTY_P))) AS PROC_TAT_QTY_DAY2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][3] + "', SUM(PROC_TAT_QTY_P))) AS PROC_TAT_QTY_DAY3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][2] + "', SUM(PROC_TAT_QTY_P))) AS PROC_TAT_QTY_DAY4" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][1] + "', SUM(PROC_TAT_QTY_P))) AS PROC_TAT_QTY_DAY5" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][0] + "', SUM(PROC_TAT_QTY_P))) AS PROC_TAT_QTY_DAY6" + "\n");
            }
            else if (stLotType.Equals("E%"))
            {
                strSqlString.Append("                 SELECT SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][2].ToString().Substring(4, 2) + "', SUM(TOTAL_TAT_QTY_E))) AS TOTAL_TAT_QTY_MON1 " + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][1].ToString().Substring(4, 2) + "', SUM(TOTAL_TAT_QTY_E))) AS TOTAL_TAT_QTY_MON2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][0].ToString().Substring(4, 2) + "', SUM(TOTAL_TAT_QTY_E))) AS TOTAL_TAT_QTY_MON3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][2].ToString().Substring(4, 2) + "', SUM(WAIT_TAT_QTY_E))) AS WAIT_TAT_QTY_MON1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][1].ToString().Substring(4, 2) + "', SUM(WAIT_TAT_QTY_E))) AS WAIT_TAT_QTY_MON2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][0].ToString().Substring(4, 2) + "', SUM(WAIT_TAT_QTY_E))) AS WAIT_TAT_QTY_MON3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][2].ToString().Substring(4, 2) + "', SUM(PROC_TAT_QTY_E))) AS PROC_TAT_QTY_MON1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][1].ToString().Substring(4, 2) + "', SUM(PROC_TAT_QTY_E))) AS PROC_TAT_QTY_MON2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][0].ToString().Substring(4, 2) + "', SUM(PROC_TAT_QTY_E))) AS PROC_TAT_QTY_MON3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][0] + "', SUM(TOTAL_TAT_QTY_E))) AS TOTAL_TAT_QTY_WEEK1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][1] + "', SUM(TOTAL_TAT_QTY_E))) AS TOTAL_TAT_QTY_WEEK2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][2] + "', SUM(TOTAL_TAT_QTY_E))) AS TOTAL_TAT_QTY_WEEK3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][0] + "', SUM(WAIT_TAT_QTY_E))) AS WAIT_TAT_QTY_WEEK1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][1] + "', SUM(WAIT_TAT_QTY_E))) AS WAIT_TAT_QTY_WEEK2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][2] + "', SUM(WAIT_TAT_QTY_E))) AS WAIT_TAT_QTY_WEEK3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][0] + "', SUM(PROC_TAT_QTY_E))) AS PROC_TAT_QTY_WEEK1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][1] + "', SUM(PROC_TAT_QTY_E))) AS PROC_TAT_QTY_WEEK2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][2] + "', SUM(PROC_TAT_QTY_E))) AS PROC_TAT_QTY_WEEK3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][5] + "', SUM(TOTAL_TAT_QTY_E))) AS TOTAL_TAT_QTY_DAY1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][4] + "', SUM(TOTAL_TAT_QTY_E))) AS TOTAL_TAT_QTY_DAY2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][3] + "', SUM(TOTAL_TAT_QTY_E))) AS TOTAL_TAT_QTY_DAY3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][2] + "', SUM(TOTAL_TAT_QTY_E))) AS TOTAL_TAT_QTY_DAY4" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][1] + "', SUM(TOTAL_TAT_QTY_E))) AS TOTAL_TAT_QTY_DAY5" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][0] + "', SUM(TOTAL_TAT_QTY_E))) AS TOTAL_TAT_QTY_DAY6" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][5] + "', SUM(WAIT_TAT_QTY_E))) AS WAIT_TAT_QTY_DAY1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][4] + "', SUM(WAIT_TAT_QTY_E))) AS WAIT_TAT_QTY_DAY2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][3] + "', SUM(WAIT_TAT_QTY_E))) AS WAIT_TAT_QTY_DAY3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][2] + "', SUM(WAIT_TAT_QTY_E))) AS WAIT_TAT_QTY_DAY4" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][1] + "', SUM(WAIT_TAT_QTY_E))) AS WAIT_TAT_QTY_DAY5" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][0] + "', SUM(WAIT_TAT_QTY_E))) AS WAIT_TAT_QTY_DAY6" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][5] + "', SUM(PROC_TAT_QTY_E))) AS PROC_TAT_QTY_DAY1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][4] + "', SUM(PROC_TAT_QTY_E))) AS PROC_TAT_QTY_DAY2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][3] + "', SUM(PROC_TAT_QTY_E))) AS PROC_TAT_QTY_DAY3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][2] + "', SUM(PROC_TAT_QTY_E))) AS PROC_TAT_QTY_DAY4" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][1] + "', SUM(PROC_TAT_QTY_E))) AS PROC_TAT_QTY_DAY5" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][0] + "', SUM(PROC_TAT_QTY_E))) AS PROC_TAT_QTY_DAY6" + "\n");
            }
            else
            {
                strSqlString.Append("                 SELECT SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][2].ToString().Substring(4, 2) + "', SUM(TOTAL_TAT_QTY))) AS TOTAL_TAT_QTY_MON1 " + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][1].ToString().Substring(4, 2) + "', SUM(TOTAL_TAT_QTY))) AS TOTAL_TAT_QTY_MON2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][0].ToString().Substring(4, 2) + "', SUM(TOTAL_TAT_QTY))) AS TOTAL_TAT_QTY_MON3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][2].ToString().Substring(4, 2) + "', SUM(WAIT_TAT_QTY))) AS WAIT_TAT_QTY_MON1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][1].ToString().Substring(4, 2) + "', SUM(WAIT_TAT_QTY))) AS WAIT_TAT_QTY_MON2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][0].ToString().Substring(4, 2) + "', SUM(WAIT_TAT_QTY))) AS WAIT_TAT_QTY_MON3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][2].ToString().Substring(4, 2) + "', SUM(PROC_TAT_QTY))) AS PROC_TAT_QTY_MON1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][1].ToString().Substring(4, 2) + "', SUM(PROC_TAT_QTY))) AS PROC_TAT_QTY_MON2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][0].ToString().Substring(4, 2) + "', SUM(PROC_TAT_QTY))) AS PROC_TAT_QTY_MON3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][0] + "', SUM(TOTAL_TAT_QTY))) AS TOTAL_TAT_QTY_WEEK1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][1] + "', SUM(TOTAL_TAT_QTY))) AS TOTAL_TAT_QTY_WEEK2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][2] + "', SUM(TOTAL_TAT_QTY))) AS TOTAL_TAT_QTY_WEEK3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][0] + "', SUM(WAIT_TAT_QTY))) AS WAIT_TAT_QTY_WEEK1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][1] + "', SUM(WAIT_TAT_QTY))) AS WAIT_TAT_QTY_WEEK2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][2] + "', SUM(WAIT_TAT_QTY))) AS WAIT_TAT_QTY_WEEK3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][0] + "', SUM(PROC_TAT_QTY))) AS PROC_TAT_QTY_WEEK1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][1] + "', SUM(PROC_TAT_QTY))) AS PROC_TAT_QTY_WEEK2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][2] + "', SUM(PROC_TAT_QTY))) AS PROC_TAT_QTY_WEEK3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][5] + "', SUM(TOTAL_TAT_QTY))) AS TOTAL_TAT_QTY_DAY1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][4] + "', SUM(TOTAL_TAT_QTY))) AS TOTAL_TAT_QTY_DAY2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][3] + "', SUM(TOTAL_TAT_QTY))) AS TOTAL_TAT_QTY_DAY3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][2] + "', SUM(TOTAL_TAT_QTY))) AS TOTAL_TAT_QTY_DAY4" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][1] + "', SUM(TOTAL_TAT_QTY))) AS TOTAL_TAT_QTY_DAY5" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][0] + "', SUM(TOTAL_TAT_QTY))) AS TOTAL_TAT_QTY_DAY6" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][5] + "', SUM(WAIT_TAT_QTY))) AS WAIT_TAT_QTY_DAY1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][4] + "', SUM(WAIT_TAT_QTY))) AS WAIT_TAT_QTY_DAY2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][3] + "', SUM(WAIT_TAT_QTY))) AS WAIT_TAT_QTY_DAY3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][2] + "', SUM(WAIT_TAT_QTY))) AS WAIT_TAT_QTY_DAY4" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][1] + "', SUM(WAIT_TAT_QTY))) AS WAIT_TAT_QTY_DAY5" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][0] + "', SUM(WAIT_TAT_QTY))) AS WAIT_TAT_QTY_DAY6" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][5] + "', SUM(PROC_TAT_QTY))) AS PROC_TAT_QTY_DAY1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][4] + "', SUM(PROC_TAT_QTY))) AS PROC_TAT_QTY_DAY2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][3] + "', SUM(PROC_TAT_QTY))) AS PROC_TAT_QTY_DAY3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][2] + "', SUM(PROC_TAT_QTY))) AS PROC_TAT_QTY_DAY4" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][1] + "', SUM(PROC_TAT_QTY))) AS PROC_TAT_QTY_DAY5" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][0] + "', SUM(PROC_TAT_QTY))) AS PROC_TAT_QTY_DAY6" + "\n");
            }
            strSqlString.Append("                   FROM CSUMTATMAT@RPTTOMES TAT" + "\n");
            strSqlString.Append("                      , MWIPMATDEF@RPTTOMES MAT" + "\n");
            strSqlString.Append("                      , MWIPCALDEF@RPTTOMES CAL" + "\n");
            strSqlString.Append("                      , MWIPOPRDEF@RPTTOMES OPR" + "\n");
            strSqlString.Append("                  WHERE 1=1" + "\n");
            strSqlString.Append("                    AND TAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                    AND TAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("                    AND TAT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                    AND TAT.FACTORY = OPR.FACTORY" + "\n");
            strSqlString.Append("                    AND TAT.OPER = OPR.OPER" + "\n");
            strSqlString.Append("                    AND TAT.SHIP_DATE BETWEEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), -3), 'YYYYMMDD')"  + " AND '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "'" + "\n");
            strSqlString.Append("                    AND CALENDAR_ID = 'HM'" + "\n");
            strSqlString.Append("                    AND TAT.SHIP_DATE = CAL.SYS_DATE" + "\n");
            strSqlString.Append("                    AND OPR.OPER_GRP_1 NOT IN (" + stCheckdData + ")    " + "\n");
            

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                    AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                    AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                    AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                    AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                    AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                    AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                    AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                    AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                    AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion
            strSqlString.Append("                   GROUP BY PLAN_MONTH, PLAN_WEEK, SYS_DATE" + "\n");
            strSqlString.Append("                ) A," + "\n");
            strSqlString.Append("                (" + "\n");

            // 2011-06-03-김민우 : Lot_Type별 검색 추가
            if (stLotType.Equals("P%"))
            {
                strSqlString.Append("                 SELECT SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][2].ToString().Substring(4, 2) + "', SUM(SHIP_QTY_P))) AS SHIP_QTY_MON1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][1].ToString().Substring(4, 2) + "', SUM(SHIP_QTY_P))) AS SHIP_QTY_MON2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][0].ToString().Substring(4, 2) + "', SUM(SHIP_QTY_P))) AS SHIP_QTY_MON3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][0] + "', SUM(SHIP_QTY_P))) AS SHIP_QTY_WEEK1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][1] + "', SUM(SHIP_QTY_P))) AS SHIP_QTY_WEEK2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][2] + "', SUM(SHIP_QTY_P))) AS SHIP_QTY_WEEK3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][5] + "', SUM(SHIP_QTY_P))) AS SHIP_QTY_DAY1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][4] + "', SUM(SHIP_QTY_P))) AS SHIP_QTY_DAY2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][3] + "', SUM(SHIP_QTY_P))) AS SHIP_QTY_DAY3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][2] + "', SUM(SHIP_QTY_P))) AS SHIP_QTY_DAY4" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][1] + "', SUM(SHIP_QTY_P))) AS SHIP_QTY_DAY5" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][0] + "', SUM(SHIP_QTY_P))) AS SHIP_QTY_DAY6" + "\n");
            }
            else if (stLotType.Equals("E%"))
            {
                strSqlString.Append("                 SELECT SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][2].ToString().Substring(4, 2) + "', SUM(SHIP_QTY_E))) AS SHIP_QTY_MON1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][1].ToString().Substring(4, 2) + "', SUM(SHIP_QTY_E))) AS SHIP_QTY_MON2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][0].ToString().Substring(4, 2) + "', SUM(SHIP_QTY_E))) AS SHIP_QTY_MON3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][0] + "', SUM(SHIP_QTY_E))) AS SHIP_QTY_WEEK1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][1] + "', SUM(SHIP_QTY_E))) AS SHIP_QTY_WEEK2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][2] + "', SUM(SHIP_QTY_E))) AS SHIP_QTY_WEEK3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][5] + "', SUM(SHIP_QTY_E))) AS SHIP_QTY_DAY1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][4] + "', SUM(SHIP_QTY_E))) AS SHIP_QTY_DAY2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][3] + "', SUM(SHIP_QTY_E))) AS SHIP_QTY_DAY3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][2] + "', SUM(SHIP_QTY_E))) AS SHIP_QTY_DAY4" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][1] + "', SUM(SHIP_QTY_E))) AS SHIP_QTY_DAY5" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][0] + "', SUM(SHIP_QTY_E))) AS SHIP_QTY_DAY6" + "\n");
            }
            else
            {
                strSqlString.Append("                 SELECT SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][2].ToString().Substring(4, 2) + "', SUM(SHIP_QTY))) AS SHIP_QTY_MON1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][1].ToString().Substring(4, 2) + "', SUM(SHIP_QTY))) AS SHIP_QTY_MON2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(TRIM(TO_CHAR(PLAN_MONTH,'00')),'" + dtMonth.Rows[0][0].ToString().Substring(4, 2) + "', SUM(SHIP_QTY))) AS SHIP_QTY_MON3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][0] + "', SUM(SHIP_QTY))) AS SHIP_QTY_WEEK1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][1] + "', SUM(SHIP_QTY))) AS SHIP_QTY_WEEK2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(PLAN_WEEK,'" + dtWeek.Rows[0][2] + "', SUM(SHIP_QTY))) AS SHIP_QTY_WEEK3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][5] + "', SUM(SHIP_QTY))) AS SHIP_QTY_DAY1" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][4] + "', SUM(SHIP_QTY))) AS SHIP_QTY_DAY2" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][3] + "', SUM(SHIP_QTY))) AS SHIP_QTY_DAY3" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][2] + "', SUM(SHIP_QTY))) AS SHIP_QTY_DAY4" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][1] + "', SUM(SHIP_QTY))) AS SHIP_QTY_DAY5" + "\n");
                strSqlString.Append("                      , SUM(DECODE(SYS_DATE,'" + dtDay.Rows[0][0] + "', SUM(SHIP_QTY))) AS SHIP_QTY_DAY6" + "\n");
            }
            strSqlString.Append("                   FROM CSUMTATMAT@RPTTOMES TAT" + "\n");
            strSqlString.Append("                      , MWIPMATDEF@RPTTOMES MAT" + "\n");
            strSqlString.Append("                      , MWIPCALDEF@RPTTOMES CAL" + "\n");
            strSqlString.Append("                      , MWIPOPRDEF@RPTTOMES OPR" + "\n");
            strSqlString.Append("                  WHERE 1=1" + "\n");
            strSqlString.Append("                    AND TAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                    AND TAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("                    AND TAT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                    AND TAT.FACTORY = OPR.FACTORY" + "\n");
            strSqlString.Append("                    AND TAT.OPER = OPR.OPER" + "\n");
            strSqlString.Append("                    AND TAT.SHIP_DATE BETWEEN TO_CHAR(ADD_MONTHS(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD'), -3), 'YYYYMMDD')" + " AND '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "'" + "\n");
            strSqlString.Append("                    AND CALENDAR_ID = 'HM'" + "\n");
            strSqlString.Append("                    AND TAT.SHIP_DATE = CAL.SYS_DATE" + "\n");
        
            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                    AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                    AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                    AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                    AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                    AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                    AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                    AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                    AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                    AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion
            strSqlString.Append("                   GROUP BY PLAN_MONTH, PLAN_WEEK, SYS_DATE" + "\n");
            strSqlString.Append("                ) B" + "\n");








            strSqlString.Append("        )" + "\n");
            strSqlString.Append("  GROUP BY ROLLUP (MON_TAT1,MON_TAT2,MON_TAT3)" + "\n");
            strSqlString.Append("  HAVING GROUPING(MON_TAT1)||GROUPING(MON_TAT2)||GROUPING(MON_TAT3) IN ('000','001','011')" + "\n");
       
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion
    

        #endregion

        #region ToExcel

        /// <summary>
        /// ToExcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1, this.lblTitle.Text, null, null);
        }

        #endregion

        private void GetLastDay()
        {
            
            StringBuilder strSqlString = new StringBuilder();
            string todate = cdvBaseDate.Value.ToString("yyyyMMdd");

            squery4 = "SELECT DECODE(PLAN_WEEK - 2,-1, TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "','YYYYMMDD'),-12),'YYYY')||'52' \n"
                   + "                            , 0, TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "','YYYYMMDD'),-12),'YYYY')||'53' \n"
                   + "                            , TO_CHAR(TO_DATE('" + todate + "','YYYYMMDD'),'YYYY')||(PLAN_WEEK - 2)) AS WW3 \n"
                   + "	    , DECODE(PLAN_WEEK - 1, 0, TO_CHAR(ADD_MONTHS(TO_DATE('" + todate + "','YYYYMMDD'),-12),'YYYY')||'53' \n"
                   + "	                             , TO_CHAR(TO_DATE('" + todate + "','YYYYMMDD'),'YYYY')||(PLAN_WEEK - 1)) AS WW2  \n"
                   + "	    , TO_CHAR(TO_DATE('" + todate + "','YYYYMMDD'),'YYYY')||PLAN_WEEK AS WW1 \n"
                   + "	 FROM MWIPCALDEF \n"
                   + "	WHERE CALENDAR_ID = 'HM' \n"
                   + "	  AND SYS_DATE = TO_CHAR(TO_DATE('" + todate + "','YYYYMMDD'),'YYYYMMDD')";

            dtWeekDay = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", squery4.Replace((char)Keys.Tab, ' '));
                        
            strSqlString.Append("SELECT MIN(SYS_DATE) AS START_DATE" + "\n");
            strSqlString.Append("     , MAX(SYS_DATE) AS END_DATE" + "\n");
            strSqlString.Append("  FROM MWIPCALDEF" + "\n");
            strSqlString.Append(" WHERE CALENDAR_ID = 'HM'" + "\n");
            strSqlString.Append("   AND SYS_YEAR||PLAN_WEEK IN ('" + dtWeekDay.Rows[0][0] + "','" + dtWeekDay.Rows[0][1] + "','" + dtWeekDay.Rows[0][2] + "')\n");
            strSqlString.Append("GROUP BY SYS_YEAR||TRIM(TO_CHAR(PLAN_WEEK,'00'))" + "\n");
            strSqlString.Append("ORDER BY SYS_YEAR||TRIM(TO_CHAR(PLAN_WEEK,'00'))" + "\n");

            dtLast = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
           
        }

        //차트
        private void fnMakeChart(Miracom.SmartWeb.UI.Controls.udcFarPoint SS, int iselrow)
        {
            // SS의 데이터를 Chart에 표시

            int[] ich_mm = new int[12]; int[] icols_mm = new int[12]; int[] irows_mm = new int[iselrow-2]; string[] stitle_mm = new string[iselrow-2];
            int icol = 0, irow = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                udcChartFX1.RPT_1_ChartInit();
                udcChartFX1.RPT_2_ClearData();
                udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Lines;
          

                udcChartFX1.AxisX.Staggered = false; //udcChartFX1
                for (icol = 0; icol < ich_mm.Length; icol++)
                {
                    ich_mm[icol] = icol + 1;
                    icols_mm[icol] = icol + 1;

                }
                
                for (irow = 0; irow < irows_mm.Length; irow++)
                {
                    irows_mm[irow] = irow;
                    //stitle_mm[irow] = SS.Sheets[0].Cells[irow, 0].Text;
                                       
                }
                
                stitle_mm[1] = SS.Sheets[0].Cells[0, 0].Text;
                stitle_mm[0] = SS.Sheets[0].Cells[1, 0].Text;

                udcChartFX1.RPT_3_OpenData(irows_mm.Length, icols_mm.Length);
                double max2 = udcChartFX1.RPT_4_AddData(SS, new int[] { 1 }, icols_mm, SeriseType.Rows);
                double max1 = udcChartFX1.RPT_4_AddData(SS, new int[] { 0 }, icols_mm, SeriseType.Rows);
                //double max3 = udcChartFX1.RPT_4_AddData(SS, irows_mm, icols_mm, SeriseType.Rows);



                udcChartFX1.RPT_5_CloseData();

                udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 0, 1, "", AsixType.Y, DataTypes.Initeger, max2 * 1.05);
                udcChartFX1.RPT_6_SetGallery(ChartType.Line, 1, 1, "", AsixType.Y, DataTypes.Initeger, max2 * 1.05);
                
                
                
                
                udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, 1);
                udcChartFX1.RPT_8_SetSeriseLegend(stitle_mm, SoftwareFX.ChartFX.Docked.Top);

                
                
                udcChartFX1.AxisY.Gridlines = true;
                

                udcChartFX1.AxisY.DataFormat.Decimals = 0;

                udcChartFX1.AxisX.Font = new System.Drawing.Font("Tahoma", 8F);
                


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

        private void TAT050801_Load(object sender, EventArgs e)
        {
            //기본적으로 Detail이 보이도록..
            pnlWIPDetail.Visible = true;

        }
        
    }
}
