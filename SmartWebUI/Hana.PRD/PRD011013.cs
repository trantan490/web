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
    public partial class PRD011013 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD011013<br/>
        /// 클래스요약: BE공정 모니터링<br/>
        /// 작  성  자: 김태호<br/>
        /// 최초작성일: 2013-10-25<br/>
        /// 상세  설명: BE공정 모니터링<br/>
        /// 변경  내용: <br/>
        /// 2013-11-21-임종우 : CAPA 중복 오류 수정
        /// </summary>
        /// 
        GlobalVariable.FindWeek FindWeek_SOP_A = new GlobalVariable.FindWeek();
        GlobalVariable.FindWeek FindWeek_SOP_T = new GlobalVariable.FindWeek();

        string[] dayArry = new string[3];
        string[] dayArry2 = new string[3];
        decimal jindoPer;

        string strKpcs = "";    //Kpcs
        Visibles ViMonth = Visibles.True;
        Visibles ViWeek = Visibles.True;
        Visibles ViDay = Visibles.True;


        public PRD011013()
        {
            InitializeComponent();
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            GetWorkDay();
            spdData.RPT_ColumnInit();

            LabelTextChange();

            DateTime Select_date;

            Select_date = cdvDate.Value;


            string strWeek = FindWeek_SOP_A.ThisWeek.Substring(4, 2);

            if (rdbMonth.Checked == true)
            {
                ViMonth = Visibles.True;
                ViWeek = Visibles.False;
                ViDay = Visibles.False;
            }
            else if (rdbWeek.Checked == true)
            {
                ViMonth = Visibles.False;
                ViWeek = Visibles.True;
                ViDay = Visibles.False;
            }
            else if (rdbDay.Checked == true)
            {
                ViMonth = Visibles.False;
                ViWeek = Visibles.False;
                ViDay = Visibles.True;
            }

            try
            {
                spdData.RPT_AddBasicColumn("구분", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("CUSTOMER", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("MAJOR_CODE", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("FAMILY", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PACKAGE", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("TYPE_1", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("TYPE_2", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("LD_COUNT", 0, 7, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("DENSITY", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("GENERATION", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PIN_TYPE", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("CUST_DEVICE", 0, 12, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("PKG_CODE", 0, 13, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("BODY_SIZE", 0, 14, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                
                //월기준 - START
                spdData.RPT_AddBasicColumn(cdvDate.Value.ToString("MM") + "Monthly plan", 0, 15, ViMonth, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("actual", 0, 16, ViMonth, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 0, 17, ViMonth, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Daily requirement", 0, 18, ViMonth, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("the day's performance", 0, 19, ViMonth, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("a daily goal", 0, 20, ViMonth, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);
                spdData.RPT_AddBasicColumn("Required number", 1, 20, ViMonth, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);

                spdData.RPT_AddBasicColumn("CAPA Status", 0, 21, ViMonth, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("대수", 1, 21, ViMonth, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("CAPA", 1, 22, ViMonth, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Overs and shorts ", 1, 23, ViMonth, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_MerageHeaderColumnSpan(0, 21, 3);
                //월기준 - END

                //주기준 - START
                spdData.RPT_AddBasicColumn("WW" + strWeek + " 계획", 0, 24, ViWeek, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("actual", 0, 25, ViWeek, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 0, 26, ViWeek, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Daily requirement", 0, 27, ViWeek, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("the day's performance", 0, 28, ViWeek, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("Allocation Required count", 0, 29, ViWeek, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);

                spdData.RPT_AddBasicColumn("CAPA Status", 0, 30, ViWeek, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("대수", 1, 30, ViWeek, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("CAPA", 1, 31, ViWeek, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Overs and shorts ", 1, 32, ViWeek, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_MerageHeaderColumnSpan(0, 30, 3);
                //주기준 - END

                //일기준 - START
                spdData.RPT_AddBasicColumn(cdvDate.Value.ToString("dd") + "일 계획", 0, 33, ViDay, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("actual", 0, 34, ViDay, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 0, 35, ViDay, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Daily requirement", 0, 36, ViDay, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("the day's performance", 0, 37, ViDay, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("a daily goal", 0, 38, ViDay, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);
                spdData.RPT_AddBasicColumn("Required number", 1, 38, ViDay, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);

                spdData.RPT_AddBasicColumn("CAPA Status", 0, 39, ViDay, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("대수", 1, 39, ViDay, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("CAPA", 1, 40, ViDay, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Overs and shorts ", 1, 41, ViDay, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_MerageHeaderColumnSpan(0, 39, 3);
                //일기준 - END


                spdData.RPT_AddBasicColumn("WIP", 0, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("SST", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("SBA", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("MK", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("PMC", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("MOLD", 1, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("GATE", 1, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("WB", 1, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_MerageHeaderColumnSpan(0, 42, 7);

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 8, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 9, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 10, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 11, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 12, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 13, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 14, 2);

                spdData.RPT_MerageHeaderRowSpan(0, 15, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 16, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 17, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 18, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 19, 2);

                spdData.RPT_MerageHeaderRowSpan(0, 24, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 25, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 26, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 27, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 28, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 29, 2);

                spdData.RPT_MerageHeaderRowSpan(0, 33, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 34, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 35, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 36, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 37, 2);
                

                spdData.RPT_ColumnConfigFromTable(btnSort);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("구분", "A.GUBUN AS \"구분\"", "A.GUBUN", "DECODE(\"구분\", 'SST', 1, 'SBA', 2, '2D MK', 3, 'C-MOLD', 4, 5)", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "DECODE(A.MAT_GRP_1,'SE','SEC','AB','ABOV','IM','iML','FC','FCI' , (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.MAT_GRP_1)) AS CUSTOMER", "A.MAT_GRP_1", "DECODE(CUSTOMER, 'SEC', 1, 'HYNIX', 2, 'iML', 3, 'FCI', 4, 'IMAGIS', 5, 6), CUSTOMER", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR_CODE", "A.MAT_GRP_9 AS MAJOR_CODE", "A.MAT_GRP_9", "MAJOR_CODE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "A.MAT_GRP_2 AS FAMILY", "A.MAT_GRP_2", "FAMILY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "A.MAT_GRP_10 AS PKG", "A.MAT_GRP_10", "PKG", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE_1", "A.MAT_GRP_4 AS TYPE_1", "A.MAT_GRP_4", "TYPE_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE_2", "A.MAT_GRP_5 AS TYPE_2", "A.MAT_GRP_5", "TYPE_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD_COUNT", "A.MAT_GRP_6 AS LD_COUNT", "A.MAT_GRP_6", "LD_COUNT", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "A.MAT_GRP_7 AS DENSITY", "A.MAT_GRP_7", "DENSITY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "A.MAT_GRP_8 AS GENERATION", "A.MAT_GRP_8", "GENERATION", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "A.MAT_CMF_10 AS PIN_TYPE", "A.MAT_CMF_10", "PIN_TYPE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.MAT_ID AS PRODUCT", "A.MAT_ID", "PRODUCT", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST_DEVICE", "A.MAT_CMF_7 AS CUST_DEVICE", "A.MAT_CMF_7", "CUST_DEVICE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG_CODE", "A.MAT_CMF_11 AS PKG_CODE", "A.MAT_CMF_11", "PKG_CODE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("BODY_SIZE", "A.MAT_CMF_9 AS BODY_SIZE", "A.MAT_CMF_9", "BODY_SIZE", true);
        }
        #endregion

        #region 시간관련 함수
        private void GetWorkDay()
        {
            DateTime Now = cdvDate.Value;
            FindWeek_SOP_A = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");
            FindWeek_SOP_T = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "SE");

            //for (int i = 0; i < 8; i++)
            //{
            //    DateArray[i] = Now.ToString("MM-dd");
            //    DateArray2[i] = Now.ToString("yyyyMMdd");
            //    Now = Now.AddDays(1);
            //}

        }

        #endregion

        #region SQL 쿼리 Build
        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1;
            string QueryCond2;
            string QueryCond3;
            string start_date;
            string yesterday;
            string date;
            string month;
            string year;
            string lastMonth;
            DataTable dtWeekList;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            int remain = Convert.ToInt32(lblLastDay.Text.Substring(0, 2)) - Convert.ToInt32(lblToday.Text.Substring(0, 2)) + 1;

            dtWeekList = GetWeekList();
            date = cdvDate.SelectedValue();

            DateTime Select_date;
            Select_date = DateTime.Parse(cdvDate.Text.ToString());

            year = Select_date.ToString("yyyy");
            month = Select_date.ToString("yyyyMM");
            start_date = month + "01";
            yesterday = Select_date.AddDays(-1).ToString("yyyyMMdd");
            lastMonth = Select_date.AddMonths(-1).ToString("yyyyMM"); // 지난달

            // 지난달 마지막일 구하기
            DataTable dt1 = null;
            string Last_Month_Last_day = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + lastMonth + "', 'YYYYMM')),'YYYYMMDD') FROM DUAL)";
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", Last_Month_Last_day);
            Last_Month_Last_day = dt1.Rows[0][0].ToString();

            // 달의 마지막일 구하기
            DataTable dt2 = null;
            string last_day = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + month + "', 'YYYYMM')),'YYYYMMDD') FROM DUAL)";
            dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", last_day);
            last_day = dt2.Rows[0][0].ToString();

            // 지난주차의 마지막일 가져오기
            dt1 = null;
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(year, Select_date.ToString("yyyyMMdd")));
            string Lastweek_lastday = dt1.Rows[0][0].ToString();

            //남은 시간 구하기
            DateTime nTime = new DateTime(cdvDate.Value.Year, cdvDate.Value.Month, cdvDate.Value.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            DateTime cutTime = new DateTime(cdvDate.Value.Year, cdvDate.Value.Month, cdvDate.Value.Day, 22, 0, 0);
            TimeSpan defTime = cutTime - nTime;

            if (ckbKpcs.Checked == true)
            {
                strKpcs = "1000";
            }
            else
            {
                strKpcs = "1";
            }

            strSqlString.Append("SELECT " + QueryCond1 + " " + "\n");

            #region 월기준
            strSqlString.Append("     /*월기준*/ " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.MON_PLAN,0))/" + strKpcs + ",0) AS MON_PLAN " + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(PLN.SHIP_MON,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))/" + strKpcs + ",0) AS SHIP_MON " + "\n");
            strSqlString.Append("     , CASE WHEN ROUND((SUM(NVL(PLN.MON_PLAN,0) - (NVL(PLN.SHIP_MON,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0)))))/" + strKpcs + ",0) <=0 THEN 0  " + "\n");
            strSqlString.Append("            ELSE ROUND((SUM(NVL(PLN.MON_PLAN,0) - (NVL(PLN.SHIP_MON,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0)))))/" + strKpcs + ",0) END AS MON_DEF  " + "\n");
            strSqlString.Append("     , CASE WHEN ROUND((SUM(NVL(PLN.MON_PLAN,0) - (NVL(PLN.SHIP_MON,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0)))))/" + strKpcs + ",0) <=0 THEN 0  " + "\n");
            strSqlString.Append("            ELSE ROUND(SUM(NVL(PLN.MON_PLAN,0) - (NVL(PLN.T_SHIP_MON,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "/" + strKpcs + ", 0) END AS MON_TARGET_DAY  " + "\n");
            //strSqlString.Append("     , ROUND(SUM(NVL(OPER_AO.SHIP_TODAY,0))/" + strKpcs + ",0) AS MON_SHIP_TODAY " + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(PLN.SHIP_TODAY,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))/" + strKpcs + ",0) AS MON_SHIP_TODAY " + "\n");
            //strSqlString.Append("     , CASE WHEN ROUND((SUM(NVL(PLN.MON_PLAN,0) - (NVL(PLN.SHIP_MON,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0)))))/" + strKpcs + ",0) <=0 THEN 0 " + "\n");
            //strSqlString.Append("            ELSE DECODE(SUM(NVL(WS_CAPA.CAPA,0)), 0, 0, ROUND(ROUND(SUM(NVL(PLN.MON_PLAN,0) - (NVL(PLN.T_SHIP_MON,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + ", 2)/SUM(NVL(WS_CAPA.CAPA,0)), 2)) END AS MON_WNT_CNT " + "\n");
            strSqlString.Append("     , CASE WHEN  " + "\n");
            strSqlString.Append("         (CASE WHEN ROUND((SUM(NVL(PLN.MON_PLAN,0) - (NVL(PLN.SHIP_MON,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))),0) <=0 THEN 0   " + "\n");
            strSqlString.Append("            ELSE ROUND(SUM(NVL(PLN.MON_PLAN,0) - (NVL(PLN.T_SHIP_MON,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + ", 0) END " + "\n");
            strSqlString.Append("            - (SUM(NVL(PLN.SHIP_TODAY,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0)))) " + "\n");
            strSqlString.Append("       ) <= 0 THEN 0 " + "\n");
            strSqlString.Append("       ELSE " + "\n");
            strSqlString.Append("         ROUND(DECODE(AVG(NVL(WS_CAPA.CAPA,NULL)), 0, 0, " + "\n");
            strSqlString.Append("         ((SUM(NVL(PLN.MON_PLAN,0) - (NVL(PLN.T_SHIP_MON,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + ")  " + "\n");
            strSqlString.Append("            - ((SUM(NVL(PLN.SHIP_TODAY,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))) " + "\n");
            strSqlString.Append("       ) / " + defTime.TotalHours + " / AVG(NVL(WS_CAPA.CAPA,NULL))),2) " + "\n");
            strSqlString.Append("        END  AS MON_WNT_CNT " + "\n");
            strSqlString.Append("     , SUM(NVL(CAPA.RES_CNT,0)) AS MON_RES_CNT " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(CAPA.CAPA,0))/" + strKpcs + ",0) AS MON_CAPA " + "\n");
            //strSqlString.Append("     , ROUND((SUM(NVL(CAPA.CAPA,0)) - ROUND(SUM(NVL(PLN.MON_PLAN,0) - (NVL(PLN.T_SHIP_MON,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + ", 0))/" + strKpcs + ",0) AS MON_OVR " + "\n");
            strSqlString.Append("     , CASE WHEN  " + "\n");
            strSqlString.Append("         (CASE WHEN ROUND((SUM(NVL(PLN.MON_PLAN,0) - (NVL(PLN.SHIP_MON,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))),0) <=0 THEN 0   " + "\n");
            strSqlString.Append("            ELSE ROUND(SUM(NVL(PLN.MON_PLAN,0) - (NVL(PLN.T_SHIP_MON,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + ", 0) END " + "\n");
            strSqlString.Append("            - (SUM(NVL(PLN.SHIP_TODAY,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0)))) " + "\n");
            strSqlString.Append("       ) <= 0 THEN 0 " + "\n");
            strSqlString.Append("       ELSE " + "\n");
            strSqlString.Append("         ROUND(DECODE(AVG(NVL(WS_CAPA.CAPA,NULL)), 0, 0, " + "\n");
            strSqlString.Append("         ((SUM(NVL(PLN.MON_PLAN,0) - (NVL(PLN.T_SHIP_MON,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + ")  " + "\n");
            strSqlString.Append("            - ((SUM(NVL(PLN.SHIP_TODAY,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))) " + "\n");
            strSqlString.Append("       ) / " + defTime.TotalHours + " / AVG(NVL(WS_CAPA.CAPA,NULL))),2) - SUM(NVL(CAPA.RES_CNT,0))" + "\n");
            strSqlString.Append("        END  AS MON_OVR " + "\n");

            #endregion

            #region 주기준
            strSqlString.Append("     /*주기준*/ " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.WEEK_PLAN,0))/" + strKpcs + ",0) AS WEEK_PLAN " + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(PLN.SHIP_WEEK,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))/" + strKpcs + ",0) AS SHIP_WEEK " + "\n");
            strSqlString.Append("     , CASE WHEN ROUND((SUM(NVL(PLN.WEEK_PLAN,0) - (NVL(PLN.SHIP_WEEK,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0)))))/" + strKpcs + ",0) <=0 THEN 0  " + "\n");
            strSqlString.Append("            ELSE ROUND((SUM(NVL(PLN.WEEK_PLAN,0) - (NVL(PLN.SHIP_WEEK,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0)))))/" + strKpcs + ",0) END AS WEEK_DEF  " + "\n");
            //strSqlString.Append("     , CASE WHEN ROUND((SUM(NVL(PLN.WEEK_PLAN,0) - (NVL(PLN.SHIP_WEEK,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0)))))/" + strKpcs + ",0) <=0 THEN 0 " + "\n");
            //strSqlString.Append("            ELSE ROUND(SUM(NVL(PLN.WEEK_PLAN,0) - (NVL(PLN.T_SHIP_WEEK,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "/" + strKpcs + ", 0) END AS WEEK_TARGET_DAY " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(D0_PLAN,0))/" + strKpcs + ",0) AS WEEK_TARGET_DAY " + "\n");
            //strSqlString.Append("     , ROUND(SUM(NVL(OPER_AO.SHIP_TODAY,0))/" + strKpcs + ",0) AS WEEK_SHIP_TODAY " + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(PLN.SHIP_TODAY,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))/" + strKpcs + ",0) AS WEEK_SHIP_TODAY " + "\n");
            //strSqlString.Append("     , CASE WHEN ROUND((SUM(NVL(PLN.WEEK_PLAN,0) - (NVL(PLN.SHIP_WEEK,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0)))))/" + strKpcs + ",0) <=0 THEN 0 " + "\n");
            //strSqlString.Append("            ELSE DECODE(SUM(NVL(WS_CAPA.CAPA,0)), 0, 0, ROUND(ROUND(SUM(NVL(PLN.WEEK_PLAN,0) - (NVL(PLN.T_SHIP_WEEK,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + ", 2)/SUM(NVL(WS_CAPA.CAPA,0)), 2)) END AS WEEK_WNT_CNT  " + "\n");
            strSqlString.Append("     , CASE WHEN SUM(NVL(D0_PLAN,0)-(NVL(PLN.SHIP_TODAY,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0)))) <= 0 THEN 0 " + "\n");
            strSqlString.Append("            ELSE DECODE(AVG(NVL(WS_CAPA.CAPA,NULL)), 0, 0, ROUND(SUM((NVL(D0_PLAN,0)-(NVL(PLN.SHIP_TODAY,0)+(DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))/" + defTime.TotalHours + ")/AVG(NVL(WS_CAPA.CAPA,NULL)), 2)) " + "\n");
            strSqlString.Append("        END AS WEEK_WNT_CNT " + "\n");
            strSqlString.Append("     , SUM(NVL(CAPA.RES_CNT,0)) AS WEEK_RES_CNT " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(CAPA.CAPA,0))/" + strKpcs + ",0) AS WEEK_CAPA " + "\n");
            //strSqlString.Append("     , ROUND((SUM(NVL(CAPA.CAPA,0)) - ROUND(SUM(NVL(PLN.WEEK_PLAN,0) - (NVL(PLN.T_SHIP_WEEK,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))/" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + ", 0))/" + strKpcs + ",0) AS WEEK_OVR " + "\n");
            strSqlString.Append("     , CASE WHEN SUM(NVL(D0_PLAN,0)-(NVL(PLN.SHIP_TODAY,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0)))) <= 0 THEN 0 " + "\n");
            strSqlString.Append("            ELSE DECODE(AVG(NVL(WS_CAPA.CAPA,NULL)), 0, 0, ROUND(SUM((NVL(D0_PLAN,0)-(NVL(PLN.SHIP_TODAY,0)+(DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))/" + defTime.TotalHours + ")/AVG(NVL(WS_CAPA.CAPA,NULL)), 2)) " + "\n");
            strSqlString.Append("        END - SUM(NVL(CAPA.RES_CNT,0)) AS WEEK_OVR " + "\n");
            #endregion

            #region 일기준
            strSqlString.Append("     /*일기준*/ " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(D0_PLAN,0))/" + strKpcs + ",0) AS DAY_PLAN " + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(PLN.SHIP_TODAY,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))/" + strKpcs + ",0) AS DAY_SHIP " + "\n");
            strSqlString.Append("     , CASE WHEN ROUND((SUM(NVL(D0_PLAN,0) - (NVL(PLN.SHIP_TODAY,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0)))))/" + strKpcs + ",0) <=0 THEN 0  " + "\n");
            strSqlString.Append("            ELSE ROUND((SUM(NVL(D0_PLAN,0) - (NVL(PLN.SHIP_TODAY,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0)))))/" + strKpcs + ",0) END AS DAY_DEF  " + "\n");
            //strSqlString.Append("     , CASE WHEN ROUND((SUM(NVL(D0_PLAN,0) - (NVL(PLN.SHIP_TODAY,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0)))))/" + strKpcs + ",0) <=0 THEN 0 " + "\n");
            //strSqlString.Append("            ELSE ROUND(SUM(NVL(D0_PLAN,0))/" + strKpcs + ",0) END AS DAY_TARGET_DAY " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(D0_PLAN,0))/" + strKpcs + ",0) AS DAY_TARGET_DAY" + "\n");
            //strSqlString.Append("     , ROUND(SUM(NVL(OPER_AO.SHIP_TODAY,0))/" + strKpcs + ",0) AS DAY_SHIP_TODAY " + "\n");
            strSqlString.Append("     , ROUND((SUM(NVL(PLN.SHIP_TODAY,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))/" + strKpcs + ",0) AS DAY_SHIP_TODAY " + "\n");
            //strSqlString.Append("     , CASE WHEN ROUND((SUM(NVL(D0_PLAN,0) - (NVL(PLN.SHIP_TODAY,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0)))))/" + strKpcs + ",0) <=0 THEN 0 " + "\n");
            //strSqlString.Append("            ELSE DECODE(SUM(NVL(WS_CAPA.CAPA,0)), 0, 0, ROUND(SUM(NVL(D0_PLAN,0))/SUM(NVL(WS_CAPA.CAPA,0)), 2)) END AS DAY_WNT_CNT  " + "\n");
            strSqlString.Append("     , CASE WHEN SUM(NVL(D0_PLAN,0)-(NVL(PLN.SHIP_TODAY,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0)))) <= 0 THEN 0 " + "\n");
            strSqlString.Append("            ELSE DECODE(AVG(NVL(WS_CAPA.CAPA,NULL)), 0, 0, ROUND(SUM((NVL(D0_PLAN,0)-(NVL(PLN.SHIP_TODAY,0)+(DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))/" + defTime.TotalHours + ")/AVG(NVL(WS_CAPA.CAPA,NULL)), 2)) " + "\n");
            strSqlString.Append("        END AS DAY_WNT_CNT " + "\n");
            strSqlString.Append("     , SUM(NVL(CAPA.RES_CNT,0)) AS DAY_RES_CNT " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(CAPA.CAPA,0))/" + strKpcs + ",0) AS DAY_CAPA " + "\n");
            //strSqlString.Append("     , ROUND((SUM(NVL(CAPA.CAPA,0)) - SUM(NVL(D0_PLAN,0)))/" + strKpcs + ",0) AS DAY_OVR " + "\n");
            strSqlString.Append("     , CASE WHEN SUM(NVL(D0_PLAN,0)-(NVL(PLN.SHIP_TODAY,0) + (DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0)))) <= 0 THEN 0 " + "\n");
            strSqlString.Append("            ELSE DECODE(AVG(NVL(WS_CAPA.CAPA,NULL)), 0, 0, ROUND(SUM((NVL(D0_PLAN,0)-(NVL(PLN.SHIP_TODAY,0)+(DECODE(A.OPER, 'A1750', NVL(SHP_SST,0), 'A1300', NVL(SHP_SBA,0), 'A1150', NVL(SHP_MK,0), 'A1000', NVL(SHP_MOLD,0), 0))))/" + defTime.TotalHours + ")/AVG(NVL(WS_CAPA.CAPA,NULL)), 2)) " + "\n");
            strSqlString.Append("        END - SUM(NVL(CAPA.RES_CNT,0)) AS DAY_OVR " + "\n");
            strSqlString.Append("      " + "\n");
            #endregion

            strSqlString.Append("     , ROUND(SUM(NVL(WIP.SST,0))/" + strKpcs + ",0) AS SST " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.SBA,0))/" + strKpcs + ",0) AS SBA " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.MK,0))/" + strKpcs + ",0) AS MK " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.PMC,0))/" + strKpcs + ",0) AS PMC " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.MOLD,0))/" + strKpcs + ",0) AS MOLD " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.GATE,0))/" + strKpcs + ",0) AS GATE " + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(WIP.WB,0))/" + strKpcs + ",0) AS WB " + "\n");
            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT * " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT DECODE(A.OPER, 'A1750', 'SST', 'A1300', 'SBA', 'A1150', '2D MK', 'A1000', 'C-MOLD') AS GUBUN, A.OPER, B.* " + "\n");
            strSqlString.Append("                   FROM MWIPFLWOPR@RPTTOMES A  " + "\n");
            strSqlString.Append("                      , MWIPMATDEF B " + "\n");
            strSqlString.Append("                 WHERE 1 = 1  " + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY  " + "\n");
            strSqlString.Append("                   AND A.FLOW = B.FIRST_FLOW  " + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND B.MAT_TYPE = 'FG'   " + "\n");
            strSqlString.Append("                   AND B.DELETE_FLAG = ' '  " + "\n");

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                   AND B.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                   AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                   AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                   AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                   AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                   AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                   AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                   AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                   AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.AppendFormat("                   AND B.MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");

            strSqlString.Append("                   AND A.OPER IN ('A1750', 'A1300', 'A1150', 'A1000') " + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND (GUBUN IN ('SST', 'SBA') " + "\n");
            strSqlString.Append("            OR (GUBUN = '2D MK' AND MAT_GRP_1 IN (SELECT DISTINCT KEY_2 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_PKG_2D_CMOLD' AND KEY_1 = GUBUN) " + "\n");
            strSqlString.Append("                                AND MAT_CMF_11 IN (SELECT DISTINCT KEY_3 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_PKG_2D_CMOLD' AND KEY_1 = GUBUN) " + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("            OR ( " + "\n");
            strSqlString.Append("                GUBUN = 'C-MOLD' AND MAT_GRP_1 IN (SELECT DISTINCT KEY_2 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_PKG_2D_CMOLD' AND KEY_1 = GUBUN) " + "\n");
            strSqlString.Append("                                AND MAT_CMF_11 IN (SELECT DISTINCT KEY_3 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_PKG_2D_CMOLD' AND KEY_1 = GUBUN) " + "\n");
            strSqlString.Append("               )) " + "\n");

            if (cdvGroup.SelectedItem.ToString() != "ALL")
                strSqlString.Append("           AND GUBUN = '" + cdvGroup.SelectedItem.ToString() + "' " + "\n");

            strSqlString.Append("       ) A " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7 " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN.RESV_FIELD1)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(PLAN.RESV_FIELD1)) AS MON_PLAN  " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN.PLAN_QTY_TEST)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(PLAN.PLAN_QTY_TEST)) AS ORI_PLAN  " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP.SHIP_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(SHP.SHIP_MON)) AS SHIP_MON  " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(W_SHP.SHIP_WEEK)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(W_SHP.SHIP_WEEK)) AS SHIP_WEEK  " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP.T_SHIP_MON)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(SHP.T_SHIP_MON)) AS T_SHIP_MON  " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(W_SHP.T_SHIP_WEEK)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(W_SHP.T_SHIP_WEEK)) AS T_SHIP_WEEK  " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(W_SHP.SHIP_TODAY)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(W_SHP.SHIP_TODAY)) AS SHIP_TODAY  " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(W_PLN.WEEK1_PLAN)/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(W_PLN.WEEK1_PLAN)) AS WEEK_PLAN   " + "\n");
            strSqlString.Append("             , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(NVL(W_PLN.D0,0)+NVL(W_PLN.WW,0)-NVL(W_SHP.T_SHIP_WEEK,0))/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0), SUM(NVL(W_PLN.D0,0)+NVL(W_PLN.WW,0)-NVL(W_SHP.T_SHIP_WEEK,0))) AS D0_PLAN " + "\n");
            strSqlString.Append("          FROM MWIPMATDEF MAT " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT MAT_ID,PLAN_QTY_TEST,PLAN_MONTH, RESV_FIELD1 " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT MAT_ID,SUM(PLAN_QTY_ASSY) AS PLAN_QTY_TEST,PLAN_MONTH, SUM(RESV_FIELD1) AS RESV_FIELD1 " + "\n");
            strSqlString.Append("                          FROM ( " + "\n");
            strSqlString.Append("                                SELECT MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, PLAN_MONTH, SUM(TO_NUMBER(DECODE(RESV_FIELD1,' ',0,RESV_FIELD1))) AS RESV_FIELD1 " + "\n");
            strSqlString.Append("                                  FROM CWIPPLNMON " + "\n");
            strSqlString.Append("                                 WHERE 1=1 " + "\n");
            strSqlString.Append("                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                   AND PLAN_MONTH = '" + month + "' " + "\n");
            strSqlString.Append("                                 GROUP BY MAT_ID, PLAN_MONTH " + "\n");
            strSqlString.Append("                               ) " + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID,PLAN_MONTH " + "\n");
            strSqlString.Append("                       ) " + "\n");
            strSqlString.Append("               ) PLAN " + "\n");
            strSqlString.Append("             , (     " + "\n");
            strSqlString.Append("                SELECT FACTORY, MAT_ID  " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 1 THEN D0_QTY " + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 2 THEN D0_QTY + D1_QTY " + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 3 THEN D0_QTY + D1_QTY + D2_QTY " + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 4 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY " + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 5 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY " + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 6 THEN D0_QTY + D1_QTY + D2_QTY + D3_QTY + D4_QTY + D5_QTY " + "\n");
            strSqlString.Append("                                ELSE 0 " + "\n");
            strSqlString.Append("                           END) AS WW  " + "\n");
            strSqlString.Append("                     , SUM(WW) AS WEEK1_PLAN " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 7 THEN D0_QTY " + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 1 THEN D1_QTY " + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 2 THEN D2_QTY " + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 3 THEN D3_QTY " + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 4 THEN D4_QTY " + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 5 THEN D5_QTY " + "\n");
            strSqlString.Append("                                WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') = 6 THEN D6_QTY " + "\n");
            strSqlString.Append("                                ELSE 0 " + "\n");
            strSqlString.Append("                           END) AS D0  " + "\n");
            strSqlString.Append("                  FROM (  " + "\n");
            strSqlString.Append("                        SELECT FACTORY, MAT_ID  " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D0_QTY, 0)) AS D0_QTY  " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D1_QTY, 0)) AS D1_QTY  " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D2_QTY, 0)) AS D2_QTY  " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D3_QTY, 0)) AS D3_QTY  " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D4_QTY, 0)) AS D4_QTY  " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D5_QTY, 0)) AS D5_QTY  " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', D6_QTY, 0)) AS D6_QTY  " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D0_QTY, 0)) AS D7_QTY  " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D1_QTY, 0)) AS D8_QTY  " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D2_QTY, 0)) AS D9_QTY  " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D3_QTY, 0)) AS D10_QTY  " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D4_QTY, 0)) AS D11_QTY  " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D5_QTY, 0)) AS D12_QTY  " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.NextWeek + "', D6_QTY, 0)) AS D13_QTY  " + "\n");
            strSqlString.Append("                             , SUM(DECODE(PLAN_WEEK, '" + FindWeek_SOP_A.ThisWeek + "', WW_QTY, 0)) AS WW  " + "\n");
            strSqlString.Append("                          FROM RWIPPLNWEK  " + "\n");
            strSqlString.Append("                         WHERE 1=1  " + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                           AND GUBUN = '3'  " + "\n");
            strSqlString.Append("                           AND PLAN_WEEK IN ('" + FindWeek_SOP_A.ThisWeek + "','" + FindWeek_SOP_A.NextWeek + "') " + "\n");
            strSqlString.Append("                         GROUP BY FACTORY, MAT_ID  " + "\n");
            strSqlString.Append("                       )  " + "\n");
            strSqlString.Append("                GROUP BY FACTORY, MAT_ID  " + "\n");
            strSqlString.Append("               ) W_PLN   " + "\n");
            strSqlString.Append("             , (  " + "\n");
            strSqlString.Append("                SELECT FACTORY, MAT_ID  " + "\n");
            strSqlString.Append("                     , SUM(DECODE(PLAN_DAY, '" + Select_date.AddDays(0).ToString("yyyyMMdd") + "', PLAN_QTY, 0)) AS D0 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN PLAN_DAY BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + FindWeek_SOP_A.EndDay_ThisWeek + "' THEN PLAN_QTY ELSE 0 END) AS WW  " + "\n");
            strSqlString.Append("                  FROM CWIPPLNDAY  " + "\n");
            strSqlString.Append("                 WHERE 1=1  " + "\n");
            strSqlString.Append("                   AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                   AND PLAN_DAY BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + FindWeek_SOP_A.EndDay_NextWeek + "'  " + "\n");
            strSqlString.Append("                   AND IN_OUT_FLAG = 'IN'  " + "\n");
            strSqlString.Append("                   AND CLASS = 'SLIS'  " + "\n");
            strSqlString.Append("                 GROUP BY FACTORY, MAT_ID  " + "\n");
            strSqlString.Append("               ) W_S_PLN  " + "\n");
            strSqlString.Append("             , (   " + "\n");
            strSqlString.Append("                SELECT MAT_ID " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) SHIP_MON  " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + start_date + "' AND '" + Select_date.AddDays(-1).ToString("yyyyMMdd") + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) T_SHIP_MON  " + "\n");
            strSqlString.Append("                  FROM RSUMFACMOV  " + "\n");
            strSqlString.Append("                 WHERE 1=1   " + "\n");
            strSqlString.Append("                   AND CM_KEY_1 IN ('" + GlobalVariable.gsAssyDefaultFactory + "')  " + "\n");
            strSqlString.Append("                   AND CM_KEY_2 = 'PROD'   " + "\n");

            if (cdvLotType.SelectedItem.ToString() != "ALL")
                strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.SelectedItem.ToString() + "' " + "\n");

            strSqlString.Append("                   AND FACTORY <> 'RETURN'   " + "\n");
            strSqlString.Append("                   AND WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "' " + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID   " + "\n");
            strSqlString.Append("               ) SHP   " + "\n");
            strSqlString.Append("             , (   " + "\n");
            strSqlString.Append("                SELECT MAT_ID   " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + date + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) SHIP_WEEK  " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + Select_date.AddDays(-1).ToString("yyyyMMdd") + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) T_SHIP_WEEK  " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE ='" + date + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) SHIP_TODAY  " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN TO_CHAR(TO_DATE('" + date + "', 'YYYYMMDD'), 'D') IN ('6','7','1') THEN 0  " + "\n");
            strSqlString.Append("                                WHEN WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + date + "' THEN NVL(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1, 0)   " + "\n");
            strSqlString.Append("                                ELSE 0   " + "\n");
            strSqlString.Append("                           END) AS SHP_RTF_WEEK  " + "\n");
            strSqlString.Append("                  FROM RSUMFACMOV  " + "\n");
            strSqlString.Append("                 WHERE 1=1   " + "\n");
            strSqlString.Append("                   AND CM_KEY_1 IN ('" + GlobalVariable.gsAssyDefaultFactory + "')  " + "\n");
            strSqlString.Append("                   AND CM_KEY_2 = 'PROD'   " + "\n");

            if (cdvLotType.SelectedItem.ToString() != "ALL")
                strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.SelectedItem.ToString() + "' " + "\n");

            strSqlString.Append("                   AND FACTORY <> 'RETURN'   " + "\n");
            strSqlString.Append("                   AND WORK_DATE BETWEEN '" + FindWeek_SOP_A.StartDay_ThisWeek + "' AND '" + date + "'   " + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID   " + "\n");
            strSqlString.Append("               ) W_SHP   " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = PLAN.MAT_ID(+) " + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = W_PLN.MAT_ID(+) " + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = W_S_PLN.MAT_ID(+) " + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = SHP.MAT_ID(+) " + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = W_SHP.MAT_ID(+) " + "\n");
            strSqlString.Append("           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND MAT.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("         GROUP BY MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7, MAT.MAT_CMF_13 " + "\n");
            strSqlString.Append("       ) PLN " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT RES.RES_STS_2 AS MAT_ID, RES.OPER " + "\n");
            strSqlString.Append("             , SUM(RES.RES_CNT) AS RES_CNT " + "\n");
            strSqlString.Append("             , SUM(TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.75 * RES.RES_CNT, 0))) AS CAPA " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT FACTORY, RES_STS_2, RES_STS_8 AS OPER, RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, COUNT(RES_ID) AS RES_CNT  " + "\n");

            if (date == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                  FROM MRASRESDEF " + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
            }
            else
            {
                strSqlString.Append("                  FROM MRASRESDEF_BOH " + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
                strSqlString.Append("                   AND CUTOFF_DT = '" + date + "' || '22' " + "\n");
            }

            strSqlString.Append("                   AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                   AND RES_CMF_9 = 'Y'  " + "\n");
            strSqlString.Append("                   AND DELETE_FLAG = ' '  " + "\n");
            strSqlString.Append("                   AND RES_TYPE='EQUIPMENT'  " + "\n");
            strSqlString.Append("                   AND RES_STS_8 LIKE '%' " + "\n");
            strSqlString.Append("                 GROUP BY FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7  " + "\n");
            strSqlString.Append("                ) RES,  " + "\n");
            strSqlString.Append("               MWIPMATDEF MAT, " + "\n");
            strSqlString.Append("               CRASUPHDEF UPH " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND MAT.DELETE_FLAG=' ' " + "\n");
            strSqlString.Append("           AND RES.FACTORY=MAT.FACTORY " + "\n");
            strSqlString.Append("           AND RES.RES_STS_2 = MAT.MAT_ID " + "\n");
            strSqlString.Append("           AND RES.FACTORY=UPH.FACTORY(+) " + "\n");
            strSqlString.Append("           AND RES.OPER=UPH.OPER(+) " + "\n");
            strSqlString.Append("           AND RES.RES_MODEL = UPH.RES_MODEL(+)  " + "\n");
            strSqlString.Append("           AND RES.UPEH_GRP = UPH.UPEH_GRP(+)  " + "\n");
            strSqlString.Append("           AND RES.RES_STS_2 = UPH.MAT_ID(+) " + "\n");
            strSqlString.Append("           AND RES.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("           AND RES.OPER NOT IN ('00001','00002')  " + "\n");

            strSqlString.AppendFormat("           AND RES.RES_STS_2 LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");

            strSqlString.Append("         GROUP BY RES.RES_STS_2, RES.OPER " + "\n");
            strSqlString.Append("       ) CAPA " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT.MAT_GRP_1, MAT.MAT_CMF_11, UPEH.OPER, AVG(NVL(UPEH,NULL))*0.75 AS CAPA " + "\n");
            strSqlString.Append("          FROM CRASUPHDEF UPEH " + "\n");
            strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND MAT.FACTORY = UPEH.FACTORY " + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = UPEH.MAT_ID " + "\n");
            strSqlString.Append("           AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND UPEH.OPER IN ('A1750', 'A1300', 'A1150', 'A1000')  " + "\n");

            strSqlString.AppendFormat("           AND MAT.MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");

            strSqlString.Append("         GROUP BY MAT.MAT_GRP_1, MAT.MAT_CMF_11, UPEH.OPER " + "\n");
            strSqlString.Append("       ) WS_CAPA " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT_ID, OPER, SUM(NVL(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1,0)) AS SHIP_TODAY " + "\n");
            strSqlString.Append("          FROM RSUMWIPMOV " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND WORK_DATE = '" + date + "' " + "\n");
            strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            
            if (cdvLotType.SelectedItem.ToString() != "ALL")
                strSqlString.Append("           AND CM_KEY_3 LIKE '" + cdvLotType.SelectedItem.ToString() + "' " + "\n");

            strSqlString.Append("           AND OPER IN ('A1750', 'A1300', 'A1150', 'A1000') " + "\n");

            strSqlString.AppendFormat("           AND MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");

            strSqlString.Append("         GROUP BY MAT_ID, OPER " + "\n");
            strSqlString.Append("       ) OPER_AO " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("             , SUM(V0+V1) AS SST " + "\n");
            strSqlString.Append("             , SUM(V2) AS SBA " + "\n");
            strSqlString.Append("             , SUM(V3) AS MK " + "\n");
            strSqlString.Append("             , SUM(V4) AS PMC " + "\n");
            strSqlString.Append("             , SUM(V5) AS MOLD " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN MAT_GRP_4 IN ('FD', 'FU') THEN V6 ELSE CASE WHEN SUBSTR(MAT_GRP_4, LENGTH(MAT_GRP_4), 1) = SUBSTR(OPER, LENGTH(OPER), 1) THEN V6 ELSE 0 END END) AS GATE " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN MAT_GRP_4 IN ('FD', 'FU') THEN V7 ELSE CASE WHEN SUBSTR(MAT_GRP_4, LENGTH(MAT_GRP_4), 1) = SUBSTR(OPER, LENGTH(OPER), 1) THEN V7 ELSE 0 END END) AS WB " + "\n");
            strSqlString.Append("             , SUM(V8) AS SHP_SST " + "\n");
            strSqlString.Append("             , SUM(V9) AS SHP_SBA " + "\n");
            strSqlString.Append("             , SUM(V10) AS SHP_MK " + "\n");
            strSqlString.Append("             , SUM(V11) AS SHP_MOLD " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT LOT.MAT_ID, MAT.MAT_GRP_3, MAT.MAT_GRP_4, OPER_GRP_1, OPER " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'SIG', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) AS V0 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'TRIM', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) AS V1 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'S/B/A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) AS V2 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'M/K', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) AS V3 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'CURE', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) AS V4 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'MOLD', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) AS V5 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'GATE', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) AS V6 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'W/B', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) AS V7 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN (OPER BETWEEN 'A1750' AND 'AZ010') AND OPER <> 'A1750' THEN DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY) ELSE 0 END) AS V8 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN (OPER BETWEEN 'A1300' AND 'AZ010') AND OPER <> 'A1300' THEN DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY) ELSE 0 END) AS V9 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN (OPER BETWEEN 'A1150' AND 'AZ010') AND OPER <> 'A1150' THEN DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY) ELSE 0 END) AS V10 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN (OPER BETWEEN 'A1000' AND 'AZ010') AND OPER <> 'A1000' THEN DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY) ELSE 0 END) AS V11 " + "\n");
            strSqlString.Append("                  FROM (   " + "\n");
            strSqlString.Append("                        SELECT FACTORY, MAT_ID, OPER_GRP_1, OPER  " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1)  " + "\n");
            strSqlString.Append("                                        ELSE QTY_1  " + "\n");
            strSqlString.Append("                                   END) QTY  " + "\n");
            strSqlString.Append("                          FROM (  " + "\n");
            strSqlString.Append("                                SELECT A.FACTORY, A.MAT_ID, B.OPER_GRP_1, B.OPER, A.QTY_1  " + "\n");
            strSqlString.Append("                                     , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') AND KEY_1 = A.MAT_ID) AS COMP_CNT   " + "\n");

            if (date == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                                  FROM RWIPLOTSTS A  " + "\n");
                strSqlString.Append("                                     , MWIPOPRDEF B  " + "\n");
                strSqlString.Append("                                 WHERE 1 = 1  " + "\n");
            }
            else
            {
                strSqlString.Append("                                  FROM RWIPLOTSTS_BOH A   " + "\n");
                strSqlString.Append("                                     , MWIPOPRDEF B   " + "\n");
                strSqlString.Append("                                 WHERE 1 = 1  " + "\n");
                strSqlString.Append("                                   AND A.CUTOFF_DT = '" + date + "' || '22' " + "\n");
            }

            strSqlString.Append("                                   AND A.FACTORY = B.FACTORY(+)   " + "\n");
            strSqlString.Append("                                   AND A.OPER = B.OPER(+)   " + "\n");
            strSqlString.Append("                                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'   " + "\n");
            strSqlString.Append("                                   AND A.LOT_TYPE = 'W'  " + "\n");
            strSqlString.Append("                                   AND A.LOT_DEL_FLAG = ' '  " + "\n");

            if (cdvLotType.SelectedItem.ToString() != "ALL")
                strSqlString.Append("                                   AND A.LOT_CMF_5 LIKE '" + cdvLotType.SelectedItem.ToString() + "' " + "\n");

            strSqlString.Append("                               )  " + "\n");
            strSqlString.Append("                         GROUP BY FACTORY, MAT_ID, OPER_GRP_1, OPER " + "\n");
            strSqlString.Append("                       ) LOT  " + "\n");
            strSqlString.Append("                     , MWIPMATDEF MAT  " + "\n");
            strSqlString.Append("                 WHERE 1 = 1  " + "\n");
            strSqlString.Append("                   AND LOT.FACTORY = MAT.FACTORY  " + "\n");
            strSqlString.Append("                   AND LOT.MAT_ID = MAT.MAT_ID  " + "\n");
            strSqlString.Append("                   AND MAT.DELETE_FLAG <> 'Y'  " + "\n");
            strSqlString.Append("                   AND MAT.MAT_GRP_2 <> '-'  " + "\n");
            strSqlString.Append("                 GROUP BY LOT.MAT_ID ,MAT.MAT_GRP_3, MAT.MAT_GRP_4, OPER_GRP_1, OPER " + "\n");
            strSqlString.Append("                 ORDER BY OPER_GRP_1, OPER " + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("       ) WIP " + "\n");
            strSqlString.Append(" WHERE 1 = 1  " + "\n");
            strSqlString.Append("   AND A.MAT_ID = PLN.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND A.MAT_ID = CAPA.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND A.OPER = CAPA.OPER(+) " + "\n");
            strSqlString.Append("   AND A.MAT_ID = OPER_AO.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND A.OPER = OPER_AO.OPER(+)  " + "\n");
            strSqlString.Append("   AND A.MAT_GRP_1 = WS_CAPA.MAT_GRP_1(+)  " + "\n");
            strSqlString.Append("   AND A.MAT_CMF_11 = WS_CAPA.MAT_CMF_11(+)  " + "\n");
            strSqlString.Append("   AND A.OPER = WS_CAPA.OPER(+)  " + "\n");
            strSqlString.Append("   AND A.MAT_ID = WIP.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("   AND A.MAT_TYPE = 'FG'   " + "\n");
            strSqlString.Append("   AND A.DELETE_FLAG = ' '  " + "\n");
            strSqlString.Append("   AND A.MAT_ID LIKE '%' " + "\n");
            strSqlString.Append(" GROUP BY " + QueryCond2 + "\n");
            strSqlString.Append("HAVING ( " + "\n");

            if (rdbMonth.Checked == true)
            {
                strSqlString.Append("        SUM(NVL(PLN.MON_PLAN,0)+NVL(PLN.SHIP_MON,0)+NVL(PLN.T_SHIP_MON,0))+ " + "\n");
            }
            else if (rdbWeek.Checked == true)
            {
                strSqlString.Append("        SUM(NVL(PLN.WEEK_PLAN,0)+NVL(PLN.SHIP_WEEK,0)+NVL(PLN.T_SHIP_WEEK,0))+ " + "\n");
            }
            else if (rdbDay.Checked == true)
            {
                strSqlString.Append("        SUM(NVL(D0_PLAN,0))+ " + "\n");
            }

            strSqlString.Append("        SUM(NVL(PLN.SHIP_TODAY,0)+NVL(OPER_AO.SHIP_TODAY,0))+  " + "\n");
            strSqlString.Append("        SUM(NVL(SHP_SST,0)+NVL(SHP_SBA,0)+NVL(SHP_MK,0)+NVL(SHP_MOLD,0))+ " + "\n");
            strSqlString.Append("        SUM(NVL(CAPA.CAPA,0)+NVL(CAPA.RES_CNT,0))+ " + "\n");
            strSqlString.Append("        SUM(NVL(WIP.SST,0)+NVL(WIP.SBA,0)+NVL(WIP.MK,0)+NVL(WIP.PMC,0)+NVL(WIP.MOLD,0)+NVL(WIP.GATE,0)+NVL(WIP.WB,0)) " + "\n");
            strSqlString.Append("       ) <> 0 " + "\n");
            strSqlString.Append(" ORDER BY " + QueryCond3 + "\n");


            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region 시간관련
        private DataTable GetWeekList()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT SYS_YEAR||LPAD(SYS_MONTH, 2,'0')||LPAD(SYS_DAY, 2,'0') AS DATE_WEEK " + "\n");
            strSqlString.Append("  FROM MWIPCALDEF " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND PLAN_YEAR || PLAN_WEEK = '" + FindWeek_SOP_A.ThisWeek + "' " + "\n");
            strSqlString.Append("   AND CALENDAR_ID = 'OTD' " + "\n");
            strSqlString.Append(" ORDER BY SYS_YEAR, SYS_MONTH, SYS_DAY " + "\n");

            return CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
        }
        #endregion

        #region EVENT 처리
        /// <summary>
        /// 6. View 버튼 Action
        /// </summary>        ///         
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            if (CheckField() == false) return;

            GridColumnInit();
            LabelTextChange();

            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 15, null, null, btnSort);

                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 15, 0, 1, true, Align.Center, VerticalAlign.Center);

                //spdData.RPT_AutoFit(false);

                

                dt.Dispose();
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
        
        /// <summary>
        /// 주기준 잔여일 구하기
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        private string GetWeekRemain(string strDate)
        {
            int remain = 0;
            string startDay = FindWeek_SOP_A.StartDay_ThisWeek;
            DateTime dt = DateTime.Parse(startDay.Substring(0,4)+"-"+startDay.Substring(4,2)+"-"+startDay.Substring(6,2));

            for (int i = 0; i < 7; i++)
            {
                if (dt.AddDays(i).ToString("yyyyMMdd") == strDate)
                {
                    remain = i;
                    break;
                }
            }

            remain = 7 - remain;

            return Convert.ToString(remain);
            
        }

        // 지난 주차의 마지막일 가져오기
        private string MakeSqlString2(string year, string date)
        {
            StringBuilder sqlString = new StringBuilder();

            sqlString.Append("SELECT MIN(SYS_DATE-1) " + "\n");
            sqlString.Append("  FROM MWIPCALDEF " + "\n");
            sqlString.Append(" WHERE 1=1" + "\n");
            sqlString.Append("   AND CALENDAR_ID='SE'" + "\n");
            sqlString.Append("   AND PLAN_YEAR='" + year + "'\n");
            sqlString.Append("   AND PLAN_WEEK=(" + "\n");
            sqlString.Append("                  SELECT PLAN_WEEK " + "\n");
            sqlString.Append("                    FROM MWIPCALDEF " + "\n");
            sqlString.Append("                   WHERE 1=1 " + "\n");
            sqlString.Append("                     AND CALENDAR_ID='SE' " + "\n");
            sqlString.Append("                     AND SYS_DATE=TO_CHAR(TO_DATE('" + date + "','YYYYMMDD'),'YYYYMMDD')" + "\n");
            sqlString.Append("                 )" + "\n");

            return sqlString.ToString();
        }

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.ActiveSheet.Rows.Count > 0)
            {
                StringBuilder Condition = new StringBuilder();
                Condition.AppendFormat("기준일자: {0}     today: {1}      workday: {2}     표준진도율: {3} " + "\n", cdvDate.Text, lblToday.Text.ToString(), lblLastDay.Text.ToString(), lblJindo.Text.ToString());

                ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);
                //spdData.ExportExcel();            
            }
        }

        /// <summary>
        /// Factory 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }

        /// <summary>
        /// 7. 상단 Lebel 표시
        /// </summary>
        private void LabelTextChange()
        {
            int remain = 0;

            DateTime getStartDate = Convert.ToDateTime(cdvDate.Value.ToString("yyyy-MM") + "-01");

            string getEndDate = getStartDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");

            string strDate = cdvDate.Value.ToString("yyyyMMdd");

            // ASSY 진도는 LSI=해당월 말일 - 2일 기준,MEMORY 해당월 말일 기준으로 현재일 계산함.
            string selectday = strDate.Substring(6, 2);
            string lastday = getEndDate.Substring(6, 2);

            string weekReamin = GetWeekRemain(strDate); //주기준 reamain

            double jindo = 0.0f;

            if (rdbMonth.Checked == true)
            {
                jindo = (Convert.ToDouble(selectday)) / Convert.ToDouble(lastday) * 100;
            }
            else
            {
                jindo = Convert.ToDouble(8-(Convert.ToDouble(weekReamin))) / Convert.ToDouble(7) * 100;
            }
            

            jindoPer = Math.Round(Convert.ToDecimal(jindo), 1);

            //금일조회일 경우 조회조건은 REALTIME
            if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
            {
                dayArry[0] = cdvDate.Value.AddDays(-3).ToString("MM.dd");
                dayArry[1] = cdvDate.Value.AddDays(-2).ToString("MM.dd");
                dayArry[2] = cdvDate.Value.AddDays(-1).ToString("MM.dd");

                dayArry2[0] = cdvDate.Value.AddDays(-3).ToString("yyyyMMdd");
                dayArry2[1] = cdvDate.Value.AddDays(-2).ToString("yyyyMMdd");
                dayArry2[2] = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");

            }
            else
            {
                dayArry[0] = cdvDate.Value.AddDays(-2).ToString("MM.dd");
                dayArry[1] = cdvDate.Value.AddDays(-1).ToString("MM.dd");
                dayArry[2] = cdvDate.Value.ToString("MM.dd");

                dayArry2[0] = cdvDate.Value.AddDays(-2).ToString("yyyyMMdd");
                dayArry2[1] = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");
                dayArry2[2] = cdvDate.Value.ToString("yyyyMMdd");
            }

            if (rdbMonth.Checked == true)
            {
                lblToday.Text = selectday + " day";
                lblLastDay.Text = lastday + " day";
            }
            else
            {
                lblToday.Text = selectday + " day";
                lblLastDay.Text = "7 day";
            }

            if (rdbMonth.Checked == true)
            {
                // 금일조회일 경우 잔여일에 금일 포함함.
                if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
                {
                    remain = (Convert.ToInt32(lastday) - Convert.ToInt32(selectday) + 1);
                }
                else
                {
                    remain = (Convert.ToInt32(lastday) - Convert.ToInt32(selectday));
                }
            }
            else
            {
                // 금일조회일 경우 잔여일에 금일 포함함.
                if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
                {
                    remain = Convert.ToInt32(weekReamin);
                }
                else
                {
                    remain = Convert.ToInt32(weekReamin)-1;
                }
            }
            lblRemain.Text = remain.ToString() + " day";
            lblJindo.Text = jindoPer.ToString() + "%";

        }

        #endregion

       
    }
}
