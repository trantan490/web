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
    public partial class PRD011009 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD011009<br/>
        /// 클래스요약: Chip Arrange현황 <br/>
        /// 작  성  자: THE James<br/>
        /// 최초작성일: 2013-07-02<br/>
        /// 상세  설명: 칩기준 D/A W/B공정세분화[1,2,3,4차].<br/>
        /// 변경  내용: <br/>
        /// </summary>
        /// 
        List<UnitPKG> lstDataset    = new List<UnitPKG>();
        List<string>  lstKeyRes     = new List<string>();
        List<string>  lstKeyCap     = new List<string>();
        List<string>  lstKeyChip    = new List<string>();
        List<string>  lstKeyMerge   = new List<string>();
        List<string>  lstKeyPFM     = new List<string>();
        List<string> selectDate = new List<string>();

        Color colorTotal        = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
        Color colorSubTotal     = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));	
        Color colorFixedColumn  = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(218)));
        public int spdWipSheetWidth = 0;
        public int spdWipSheetHeight = 0;
        private static FarPoint.Win.Spread.CellType.NumberCellType _number;
        private static FarPoint.Win.Spread.CellType.TextCellType _text;

        class UnitPKG
        {
            public string strCUSTOMER   = "";
            public string strMAT_GRP_1  = "";
            public string strMAT_GRP_9  = "";
            public string strMAT_GRP_10 = "";

            // Key값 
            // DA01,DA02,DA03,DA04,DA05,TTL   [DA01 : DA1차CHIP 컬럼 ...]
            // WB01,WB02,WB03,WB04,WB05,TTL   [WB03 : WB3차CHIP 컬럼 ...] 
            public Dictionary<string, long> dicRes                  = new Dictionary<string, long>();
            public Dictionary<string, long> dicCapa                 = new Dictionary<string, long>();
            public Dictionary<string, long> dicLotChip              = new Dictionary<string, long>();
            public Dictionary<string, long> dicLotMerge             = new Dictionary<string, long>();
            public Dictionary<string, long> dicPerformance          = new Dictionary<string, long>();
            public Dictionary<string, long> dicForcastPerformance   = new Dictionary<string, long>();
        }

        public PRD011009()
        {
            InitializeComponent();
            SortInit();
            GridColumnInit();
            lblNumericSum.Text = "";
            cdvDate.Value = DateTime.Now;
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            cdvFactory.Enabled = false;

            spdWipSheetWidth = pnlWip.Width;
            spdWipSheetHeight = pnlWip.Height;
        }
        #region " Constant Definition "

        #endregion

        #region " Function Definition "

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
            spdData.RPT_ColumnInit();
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 2;
            selectDate.Clear();

            try
            {
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Major"      , 0, 1,  Visibles.True, Frozen.True, Align.Center, Merge.True,  Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PKG"        , 0, 2,  Visibles.True, Frozen.True, Align.Center, Merge.True,  Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD_CNT"     , 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PKG_CODE"   , 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("classification", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("TTL"        , 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(""           , 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Primary Chip"   , 0, 8,  Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(""           , 0, 9,  Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("2nd Chip"   , 0, 10,  Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(""           , 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("3rd Chip"   , 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(""           , 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("4th Chip"   , 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(""           , 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("5th Chip"   , 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(""           , 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Etc Chip"   , 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(""           , 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("D/A", 1, 6,  Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W/B", 1, 7,  Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A", 1, 8,  Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W/B", 1, 9,  Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A", 1, 10,  Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W/B", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W/B", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W/B", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W/B", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W/B", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_MerageHeaderColumnSpan(0, 6,  2);
                spdData.RPT_MerageHeaderColumnSpan(0, 8,  2);
                spdData.RPT_MerageHeaderColumnSpan(0, 10,  2);
                spdData.RPT_MerageHeaderColumnSpan(0, 12, 2);
                spdData.RPT_MerageHeaderColumnSpan(0, 14, 2);
                spdData.RPT_MerageHeaderColumnSpan(0, 16, 2);
                spdData.RPT_MerageHeaderColumnSpan(0, 18, 2);

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 2);


                DateTime stDate = Convert.ToDateTime(cdvDate.Value.ToString("yyyy-MM") + "-01");
                DateTime edDate = Convert.ToDateTime(cdvDate.Value.ToString("yyyy-MM-dd"));

                int kk = 0;

                for (DateTime ii = stDate; ii <= edDate; ii = ii.AddDays(1))
                {
                    selectDate.Add(ii.ToString("yyyyMMdd"));
                    
                    kk++;
                }

                if (_number == null)
                {
                    _number = new FarPoint.Win.Spread.CellType.NumberCellType();
                    _number.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
                    _number.DecimalPlaces = 0;
                    _number.DropDownButton = false;
                    _number.MaximumValue = 9999999;
                    _number.MinimumValue = -9999999;
                    _number.LeadingZero = FarPoint.Win.Spread.CellType.LeadingZero.No;
                    _number.Separator = ",";
                    _number.ShowSeparator = true;
                    _number.NegativeRed = true;

                    _text = new FarPoint.Win.Spread.CellType.TextCellType();
                }

                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "A.MAT_GRP_1 AS CUSTOMER", "A.MAT_GRP_1", "DECODE(CUSTOMER, 'ZZZZ', 'TOTAL',DECODE(CUSTOMER,'SE','SEC','HX','HYNIX','IM','iML','FC','FCI','IG','IMAGIS' , (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = CUSTOMER)) ) CUSTOMER", "CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "A.MAT_GRP_9 AS FAMILY", "A.MAT_GRP_9", "DECODE(FAMILY, 'ZZZZ', 'TOTAL', 'ZZ', 'CUSTOMER TOTAL', FAMILY) FAMILY", "FAMILY", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "A.MAT_GRP_10 AS PKG", "A.MAT_GRP_10", "DECODE(PKG, 'ZZ SUB TOT', 'SUB TOTAL', PKG)  PKG", "PKG", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lead", "A.MAT_GRP_6 AS LD_CNT", "A.MAT_GRP_6", "LD_CNT", "LD_CNT", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG Code", "A.MAT_CMF_11 AS PKG_CODE", "A.MAT_CMF_11", "PKG_CODE", "PKG_CODE", false);
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        /// 
        
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();
                        
            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string QueryCond4 = null;
            string QueryCond1NotNull;
            string QueryCond2NotNull;
            string QueryCond3NotNull;
            string QueryCond4NotNull;
            string start_date;
            string yesterday;
            string date;
            string month;
            string year;
            string lastMonth;
            String Check_Day;
                        
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            QueryCond1NotNull = tableForm.SelectedValueToQuery;
            QueryCond2NotNull = tableForm.SelectedValue2ToQuery;
            QueryCond3NotNull = tableForm.SelectedValue3ToQuery;
            QueryCond4NotNull = tableForm.SelectedValue4ToQuery;

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
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(year,Select_date.ToString("yyyyMMdd")));
            string Lastweek_lastday = dt1.Rows[0][0].ToString();
            
            Check_Day = selectDate[selectDate.Count - 1];

            strSqlString.Append("SELECT " + QueryCond3 + ", GUBUN, DA_TTL,  WB_TTL, DA1,  WB1, DA2, WB2, DA3, WB3,DA4, WB4,DA5, WB5, DA_ETC, WB_ETC" + "\n");
            strSqlString.Append("  FROM (" + "\n");

            if (chkKcps.Checked == false)
            {
                strSqlString.Append(" SELECT " + QueryCond4 + ", GUBUN,  GUBUN_SEQ, DA_TTL,  WB_TTL, DA1,  WB1, DA2, WB2, DA3, WB3,DA4, WB4,DA5, WB5, DA_ETC, WB_ETC" + "\n");
            }
            else
            {
                strSqlString.Append(" SELECT " + QueryCond4 + ", GUBUN,  GUBUN_SEQ," + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, DA_TTL, DECODE(NVL(DA_TTL, 0), 0, 0, NVL(DA_TTL, 0) /1000)) ) DA_TTL," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, WB_TTL, DECODE(NVL(WB_TTL, 0), 0, 0, NVL(WB_TTL, 0) /1000)) ) WB_TTL," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, DA1,DECODE(NVL(DA1, 0), 0, 0, NVL(DA1, 0) /1000)) ) DA1," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, WB1,DECODE(NVL(WB1, 0), 0, 0, NVL(WB1, 0)/1000)) ) WB1," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, DA2,DECODE(NVL(DA2, 0), 0, 0, NVL(DA2, 0)/1000)) ) DA2," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, WB2,DECODE(NVL(WB2, 0), 0, 0, NVL(WB2, 0)/1000)) ) WB2," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, DA3,DECODE(NVL(DA3, 0), 0, 0, NVL(DA3, 0)/1000)) ) DA3," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, WB3,DECODE(NVL(WB3, 0), 0, 0, NVL(WB3, 0)/1000)) ) WB3," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, DA4,DECODE(NVL(DA4, 0), 0, 0, NVL(DA4, 0)/1000)) ) DA4," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, WB4,DECODE(NVL(WB4, 0), 0, 0, NVL(WB4, 0)/1000)) ) WB4," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, DA5,DECODE(NVL(DA5, 0), 0, 0, NVL(DA5, 0)/1000)) ) DA5," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, WB5,DECODE(NVL(WB5, 0), 0, 0, NVL(WB5, 0)/1000)) ) WB5," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, DA_ETC, DECODE(NVL(DA_ETC, 0), 0, 0, NVL(DA_ETC, 0)/1000)) ) DA_ETC," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, WB_ETC, DECODE(NVL(WB_ETC, 0), 0, 0, NVL(WB_ETC, 0)/1000)) ) WB_ETC" + "\n");
                }
                else
                {
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, DA_TTL, 8, 0, DECODE(NVL(DA_TTL, 0), 0, 0, NVL(DA_TTL, 0) /1000)) ) DA_TTL," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, WB_TTL, 8, 0, DECODE(NVL(WB_TTL, 0), 0, 0, NVL(WB_TTL, 0) /1000)) ) WB_TTL," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, DA1, 8, 0, DECODE(NVL(DA1, 0), 0, 0, NVL(DA1, 0) /1000)) ) DA1," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, WB1, 8, 0, DECODE(NVL(WB1, 0), 0, 0, NVL(WB1, 0)/1000)) ) WB1," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, DA2, 8, 0, DECODE(NVL(DA2, 0), 0, 0, NVL(DA2, 0)/1000)) ) DA2," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, WB2, 8, 0, DECODE(NVL(WB2, 0), 0, 0, NVL(WB2, 0)/1000)) ) WB2," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, DA3, 8, 0, DECODE(NVL(DA3, 0), 0, 0, NVL(DA3, 0)/1000)) ) DA3," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, WB3, 8, 0, DECODE(NVL(WB3, 0), 0, 0, NVL(WB3, 0)/1000)) ) WB3," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, DA4, 8, 0, DECODE(NVL(DA4, 0), 0, 0, NVL(DA4, 0)/1000)) ) DA4," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, WB4, 8, 0, DECODE(NVL(WB4, 0), 0, 0, NVL(WB4, 0)/1000)) ) WB4," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, DA5, 8, 0, DECODE(NVL(DA5, 0), 0, 0, NVL(DA5, 0)/1000)) ) DA5," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, WB5, 8, 0, DECODE(NVL(WB5, 0), 0, 0, NVL(WB5, 0)/1000)) ) WB5," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, DA_ETC, 8, 0, DECODE(NVL(DA_ETC, 0), 0, 0, NVL(DA_ETC, 0)/1000)) ) DA_ETC," + "\n");
                    strSqlString.Append("        ROUND(DECODE(GUBUN_SEQ, 3, WB_ETC, 8, 0, DECODE(NVL(WB_ETC, 0), 0, 0, NVL(WB_ETC, 0)/1000)) ) WB_ETC" + "\n");
                }

            }
            strSqlString.Append("   FROM ( " + "\n");
            
           // strSqlString.Append("/* ------------------------------------------------------ " + "\n");
           // strSqlString.Append("      PKG SUB TOTAL Data " + "\n");
           // strSqlString.Append(" ------------------------------------------------------*/ " + "\n");

            strSqlString.Append("     SELECT A.MAT_GRP_1 AS CUSTOMER,  A.MAT_GRP_9 AS FAMILY ,  'ZZ SUB TOT' AS PKG, 'SUB TOT'  LD_CNT,  'SUB TOT'  PKG_CODE," + "\n");
            strSqlString.Append("               DECODE(SEQ, 1, 'SOP 잔량', 2, '일목표', 3, '소요대수', 4, 'CAPA', 5, 'CHIP', 6, 'MERGE', 7, '실적', '예상실적') GUBUN, SEQ GUBUN_SEQ," + "\n");

            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA_TTL - RCV.DA_TTL, A.DA_TTL), 0 ) ) DA_TTL," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB_TTL - RCV.WB_TTL, A.WB_TTL), 0 ) ) WB_TTL," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA1 - RCV.DA1, A.DA1), 0 ) ) DA1," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA2 - RCV.DA2, A.DA2), 0 ) ) DA2," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA3 - RCV.DA3, A.DA3), 0 ) ) DA3," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA4 - RCV.DA4, A.DA4), 0 ) ) DA4," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA5 - RCV.DA5, A.DA5), 0 ) ) DA5," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB1 - RCV.WB1, A.WB1), 0 ) ) WB1," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB2 - RCV.WB2, A.WB2), 0 ) ) WB2," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB3 - RCV.WB3, A.WB3), 0 ) ) WB3," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB4 - RCV.WB4, A.WB4), 0 ) ) WB4," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB5 - RCV.WB5, A.WB5), 0 ) ) WB5," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA_ETC - RCV.DA_ETC, A.DA_ETC), 0 ) ) DA_ETC," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB_ETC - RCV.WB_ETC, A.WB_ETC), 0 ) ) WB_ETC" + "\n");

            strSqlString.Append("        FROM ( " + "\n");

            strSqlString.Append("       SELECT MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, GUBUN" + "\n");
            strSqlString.Append("           , SUM(DA1) +SUM(DA2) +SUM(DA3) + SUM(DA4) + SUM(DA5) DA_TTL," + "\n");
            strSqlString.Append("           SUM(WB1) +SUM(WB2) +SUM(WB3) +SUM(WB4) +SUM(WB5) WB_TTL," + "\n");
            strSqlString.Append("           SUM(DA1) DA1,SUM(WB1) WB1," + "\n");
            strSqlString.Append("           SUM(DA2) DA2,SUM(WB2) WB2," + "\n");
            strSqlString.Append("           SUM(DA3) DA3,SUM(WB3) WB3," + "\n");
            strSqlString.Append("           SUM(DA4) DA4,SUM(WB4) WB4," + "\n");
            strSqlString.Append("           SUM(DA5) DA5,SUM(WB5) WB5," + "\n");
            strSqlString.Append("           SUM(DA6) + SUM(DA7) + SUM(DA8) + SUM(DA9) DA_ETC," + "\n");
            strSqlString.Append("           SUM(WB6) +SUM(WB7) +SUM(WB8) +SUM(WB9) WB_ETC" + "\n");
            strSqlString.Append("  FROM  (" + "\n");

            strSqlString.Append("           SELECT MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, MAT_KEY,'1_SOP_잔량'  GUBUN," + "\n");
            strSqlString.Append("                   DECODE(MAX_DA1,0,MIN_DA1,MAX_DA1) DA1,  DECODE(MAX_DA6,0,MIN_DA6,MAX_DA6) DA6" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA2,0,MIN_DA2,MAX_DA2) DA2, DECODE(MAX_DA7,0,MIN_DA7,MAX_DA7) DA7" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA3,0,MIN_DA3,MAX_DA3) DA3, DECODE(MAX_DA8,0,MIN_DA8,MAX_DA8) DA8" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA4,0,MIN_DA4,MAX_DA4) DA4, DECODE(MAX_DA9,0,MIN_DA9,MAX_DA9) DA9" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA5,0,MIN_DA5,MAX_DA5) DA5" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB1,0,MIN_WB1,MAX_WB1) WB1, DECODE(MAX_WB6,0,MIN_WB6,MAX_WB6) WB6" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB2,0,MIN_WB2,MAX_WB2) WB2, DECODE(MAX_WB7,0,MIN_WB7,MAX_WB7) WB7" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB3,0,MIN_WB3,MAX_WB3) WB3, DECODE(MAX_WB8,0,MIN_WB8,MAX_WB8) WB8" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB4,0,MIN_WB4,MAX_WB4) WB4, DECODE(MAX_WB9,0,MIN_WB9,MAX_WB9) WB9" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB5,0,MIN_WB5,MAX_WB5) WB5" + "\n");
            strSqlString.Append("             FROM (" + "\n");
            strSqlString.Append("                     SELECT CUSTOMER MAT_GRP_1, FAMILY MAT_GRP_2, PKG MAT_GRP_3, TYPE1 MAT_GRP_4, TYPE2 MAT_GRP_5, LD_COUNT MAT_GRP_6, DENSITY MAT_GRP_7, GENERATION MAT_GRP_8, MAJOR_CODE MAT_GRP_9, PKG2 MAT_GRP_10, PKG_CODE MAT_CMF_11, MAT_KEY," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA1), 'HX', MAX(DA1), SUM(DA1) ) MAX_DA1,DECODE(CUSTOMER, 'SE', MIN(DA1), 'HX', MIN(DA1),0) MIN_DA1," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA2), 'HX', MAX(DA2), SUM(DA2) ) MAX_DA2,DECODE(CUSTOMER, 'SE', MIN(DA2), 'HX', MIN(DA2),0) MIN_DA2," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA3), 'HX', MAX(DA3), SUM(DA3) ) MAX_DA3,DECODE(CUSTOMER, 'SE', MIN(DA3), 'HX', MIN(DA3),0) MIN_DA3," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA4), 'HX', MAX(DA4), SUM(DA4) ) MAX_DA4,DECODE(CUSTOMER, 'SE', MIN(DA4), 'HX', MIN(DA4),0) MIN_DA4," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA5), 'HX', MAX(DA5), SUM(DA5) ) MAX_DA5,DECODE(CUSTOMER, 'SE', MIN(DA5), 'HX', MIN(DA5),0) MIN_DA5," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA6), 'HX', MAX(DA6), SUM(DA6) ) MAX_DA6,DECODE(CUSTOMER, 'SE', MIN(DA6), 'HX', MIN(DA6),0) MIN_DA6," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA7), 'HX', MAX(DA7), SUM(DA7) ) MAX_DA7,DECODE(CUSTOMER, 'SE', MIN(DA7), 'HX', MIN(DA7),0) MIN_DA7," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA8), 'HX', MAX(DA8), SUM(DA8) ) MAX_DA8,DECODE(CUSTOMER, 'SE', MIN(DA8), 'HX', MIN(DA8),0) MIN_DA8," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA9), 'HX', MAX(DA9), SUM(DA9) ) MAX_DA9,DECODE(CUSTOMER, 'SE', MIN(DA9), 'HX', MIN(DA9),0) MIN_DA9," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB1), 'HX', MAX(WB1), SUM(WB1) ) MAX_WB1,DECODE(CUSTOMER, 'SE', MIN(WB1), 'HX', MIN(WB1),0) MIN_WB1," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB2), 'HX', MAX(WB2), SUM(WB2) ) MAX_WB2,DECODE(CUSTOMER, 'SE', MIN(WB2), 'HX', MIN(WB2),0) MIN_WB2," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB3), 'HX', MAX(WB3), SUM(WB3) ) MAX_WB3,DECODE(CUSTOMER, 'SE', MIN(WB3), 'HX', MIN(WB3),0) MIN_WB3," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB4), 'HX', MAX(WB4), SUM(WB4) ) MAX_WB4,DECODE(CUSTOMER, 'SE', MIN(WB4), 'HX', MIN(WB4),0) MIN_WB4," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB5), 'HX', MAX(WB5), SUM(WB5) ) MAX_WB5,DECODE(CUSTOMER, 'SE', MIN(WB5), 'HX', MIN(WB5),0) MIN_WB5," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB6), 'HX', MAX(WB6), SUM(WB6) ) MAX_WB6,DECODE(CUSTOMER, 'SE', MIN(WB6), 'HX', MIN(WB6),0) MIN_WB6," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB7), 'HX', MAX(WB7), SUM(WB7) ) MAX_WB7,DECODE(CUSTOMER, 'SE', MIN(WB7), 'HX', MIN(WB7),0) MIN_WB7," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB8), 'HX', MAX(WB8), SUM(WB8) ) MAX_WB8,DECODE(CUSTOMER, 'SE', MIN(WB8), 'HX', MIN(WB8),0) MIN_WB8," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB9), 'HX', MAX(WB9), SUM(WB9) ) MAX_WB9, DECODE(CUSTOMER, 'SE', MIN(WB9), 'HX', MIN(WB9),0) MIN_WB9" + "\n"); 
            strSqlString.Append("                       FROM RSUMOPRREM" + "\n");
            strSqlString.Append("                      WHERE WORK_DATE = '" + selectDate[selectDate.Count - 1] + "'" + "\n");
            strSqlString.Append("                        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                        AND RESV_FIELD_1 = ' '" + "\n");
            strSqlString.Append("                        AND MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
            strSqlString.Append("                      GROUP BY CUSTOMER , FAMILY , PKG , TYPE1 , TYPE2 , LD_COUNT , DENSITY , GENERATION , MAJOR_CODE , PKG2 , PKG_CODE  , MAT_KEY ) )" + "\n");
            strSqlString.Append("               GROUP BY MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, GUBUN" + "\n");
            strSqlString.Append("       UNION ALL" + "\n");
            strSqlString.Append("       SELECT MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, GUBUN" + "\n");
            strSqlString.Append("              , SUM(DA1) +SUM(DA2) +SUM(DA3) + SUM(DA4) + SUM(DA5) DA_TTL," + "\n");
            strSqlString.Append("              SUM(WB1) +SUM(WB2) +SUM(WB3) +SUM(WB4) +SUM(WB5) WB_TTL," + "\n");
            strSqlString.Append("              SUM(DA1) DA1,SUM(WB1) WB1," + "\n");
            strSqlString.Append("              SUM(DA2) DA2,SUM(WB2) WB2," + "\n");
            strSqlString.Append("              SUM(DA3) DA3,SUM(WB3) WB3," + "\n");
            strSqlString.Append("              SUM(DA4) DA4,SUM(WB4) WB4," + "\n");
            strSqlString.Append("              SUM(DA5) DA5,SUM(WB5) WB5," + "\n");
            strSqlString.Append("              SUM(DA6) + SUM(DA7) + SUM(DA8) + SUM(DA9) DA_ETC," + "\n");
            strSqlString.Append("              SUM(WB6) +SUM(WB7) +SUM(WB8) +SUM(WB9) WB_ETC" + "\n");
            strSqlString.Append("  FROM  (" + "\n");
            strSqlString.Append("           SELECT MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, MAT_KEY,'2_목표'  GUBUN," + "\n");
            strSqlString.Append("                   DECODE(MAX_DA1,0,MIN_DA1,MAX_DA1) DA1,  DECODE(MAX_DA6,0,MIN_DA6,MAX_DA6) DA6" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA2,0,MIN_DA2,MAX_DA2) DA2, DECODE(MAX_DA7,0,MIN_DA7,MAX_DA7) DA7" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA3,0,MIN_DA3,MAX_DA3) DA3, DECODE(MAX_DA8,0,MIN_DA8,MAX_DA8) DA8" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA4,0,MIN_DA4,MAX_DA4) DA4, DECODE(MAX_DA9,0,MIN_DA9,MAX_DA9) DA9" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA5,0,MIN_DA5,MAX_DA5) DA5" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB1,0,MIN_WB1,MAX_WB1) WB1, DECODE(MAX_WB6,0,MIN_WB6,MAX_WB6) WB6" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB2,0,MIN_WB2,MAX_WB2) WB2, DECODE(MAX_WB7,0,MIN_WB7,MAX_WB7) WB7" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB3,0,MIN_WB3,MAX_WB3) WB3, DECODE(MAX_WB8,0,MIN_WB8,MAX_WB8) WB8" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB4,0,MIN_WB4,MAX_WB4) WB4, DECODE(MAX_WB9,0,MIN_WB9,MAX_WB9) WB9" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB5,0,MIN_WB5,MAX_WB5) WB5" + "\n");
            strSqlString.Append("             FROM (" + "\n");
            strSqlString.Append("                     SELECT CUSTOMER MAT_GRP_1, FAMILY MAT_GRP_2, PKG MAT_GRP_3, TYPE1 MAT_GRP_4, TYPE2 MAT_GRP_5, LD_COUNT MAT_GRP_6, DENSITY MAT_GRP_7, GENERATION MAT_GRP_8, MAJOR_CODE MAT_GRP_9, PKG2 MAT_GRP_10, PKG_CODE MAT_CMF_11, MAT_KEY," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA1), 'HX', MAX(DA1), SUM(DA1) ) MAX_DA1,DECODE(CUSTOMER, 'SE', MIN(DA1), 'HX', MIN(DA1),0) MIN_DA1," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA2), 'HX', MAX(DA2), SUM(DA2) ) MAX_DA2,DECODE(CUSTOMER, 'SE', MIN(DA2), 'HX', MIN(DA2),0) MIN_DA2," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA3), 'HX', MAX(DA3), SUM(DA3) ) MAX_DA3,DECODE(CUSTOMER, 'SE', MIN(DA3), 'HX', MIN(DA3),0) MIN_DA3," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA4), 'HX', MAX(DA4), SUM(DA4) ) MAX_DA4,DECODE(CUSTOMER, 'SE', MIN(DA4), 'HX', MIN(DA4),0) MIN_DA4," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA5), 'HX', MAX(DA5), SUM(DA5) ) MAX_DA5,DECODE(CUSTOMER, 'SE', MIN(DA5), 'HX', MIN(DA5),0) MIN_DA5," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA6), 'HX', MAX(DA6), SUM(DA6) ) MAX_DA6,DECODE(CUSTOMER, 'SE', MIN(DA6), 'HX', MIN(DA6),0) MIN_DA6," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA7), 'HX', MAX(DA7), SUM(DA7) ) MAX_DA7,DECODE(CUSTOMER, 'SE', MIN(DA7), 'HX', MIN(DA7),0) MIN_DA7," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA8), 'HX', MAX(DA8), SUM(DA8) ) MAX_DA8,DECODE(CUSTOMER, 'SE', MIN(DA8), 'HX', MIN(DA8),0) MIN_DA8," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA9), 'HX', MAX(DA9), SUM(DA9) ) MAX_DA9,DECODE(CUSTOMER, 'SE', MIN(DA9), 'HX', MIN(DA9),0) MIN_DA9," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB1), 'HX', MAX(WB1), SUM(WB1) ) MAX_WB1,DECODE(CUSTOMER, 'SE', MIN(WB1), 'HX', MIN(WB1),0) MIN_WB1," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB2), 'HX', MAX(WB2), SUM(WB2) ) MAX_WB2,DECODE(CUSTOMER, 'SE', MIN(WB2), 'HX', MIN(WB2),0) MIN_WB2," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB3), 'HX', MAX(WB3), SUM(WB3) ) MAX_WB3,DECODE(CUSTOMER, 'SE', MIN(WB3), 'HX', MIN(WB3),0) MIN_WB3," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB4), 'HX', MAX(WB4), SUM(WB4) ) MAX_WB4,DECODE(CUSTOMER, 'SE', MIN(WB4), 'HX', MIN(WB4),0) MIN_WB4," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB5), 'HX', MAX(WB5), SUM(WB5) ) MAX_WB5,DECODE(CUSTOMER, 'SE', MIN(WB5), 'HX', MIN(WB5),0) MIN_WB5," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB6), 'HX', MAX(WB6), SUM(WB6) ) MAX_WB6,DECODE(CUSTOMER, 'SE', MIN(WB6), 'HX', MIN(WB6),0) MIN_WB6," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB7), 'HX', MAX(WB7), SUM(WB7) ) MAX_WB7,DECODE(CUSTOMER, 'SE', MIN(WB7), 'HX', MIN(WB7),0) MIN_WB7," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB8), 'HX', MAX(WB8), SUM(WB8) ) MAX_WB8,DECODE(CUSTOMER, 'SE', MIN(WB8), 'HX', MIN(WB8),0) MIN_WB8," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB9), 'HX', MAX(WB9), SUM(WB9) ) MAX_WB9, DECODE(CUSTOMER, 'SE', MIN(WB9), 'HX', MIN(WB9),0) MIN_WB9" + "\n");
            strSqlString.Append("                       FROM RSUMOPRREM" + "\n");
            strSqlString.Append("                      WHERE WORK_DATE = '" + selectDate[selectDate.Count - 1] + "'" + "\n");
            strSqlString.Append("                        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                        AND RESV_FIELD_1 = 'TARGET'" + "\n");
            strSqlString.Append("                        AND MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
            strSqlString.Append("                      GROUP BY  CUSTOMER , FAMILY , PKG , TYPE1 , TYPE2 , LD_COUNT , DENSITY , GENERATION , MAJOR_CODE , PKG2 , PKG_CODE  , MAT_KEY ) )" + "\n");
            strSqlString.Append("               GROUP BY MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, GUBUN" + "\n");
           
            strSqlString.Append("          UNION ALL" + "\n");
            strSqlString.Append("         SELECT B.MAT_GRP_1,  B.MAT_GRP_2,  B.MAT_GRP_3,  B.MAT_GRP_4,  B.MAT_GRP_5,  B.MAT_GRP_6,  B.MAT_GRP_7,  B.MAT_GRP_8,  B.MAT_GRP_9,  B.MAT_GRP_10,  B.MAT_CMF_11, GUBUN,  DA_TTL, WB_TTL, DA1, WB1, DA2, WB2, DA3, WB3, DA4, WB4, DA5, WB5, DA_ETC, WB_ETC" + "\n");
            strSqlString.Append("         FROM ( SELECT  '3_설비댓수' GUBUN, MAT_ID, " + "\n");
            strSqlString.Append("               SUM( CASE WHEN OPER LIKE 'A040%' THEN  RES_CNT  ELSE 0  END ) DA_TTL, SUM( CASE WHEN OPER LIKE 'A060%' THEN  RES_CNT  ELSE 0  END ) WB_TTL," + "\n");
            strSqlString.Append("               SUM( DECODE(OPER, 'A0400', RES_CNT, 'A0401', RES_CNT, 0) ) DA1, SUM( DECODE(OPER, 'A0600', RES_CNT, 'A0601', RES_CNT, 0)) WB1," + "\n");
            strSqlString.Append("               SUM( DECODE(OPER, 'A0402', RES_CNT, 0)) DA2, SUM( DECODE(OPER, 'A0602', RES_CNT, 0)) WB2," + "\n");
            strSqlString.Append("               SUM( DECODE(OPER, 'A0403', RES_CNT, 0)) DA3,SUM( DECODE(OPER, 'A0603', RES_CNT, 0)) WB3," + "\n");
            strSqlString.Append("               SUM( DECODE(OPER, 'A0404', RES_CNT, 0)) DA4, SUM( DECODE(OPER, 'A0604', RES_CNT, 0)) WB4," + "\n");
            strSqlString.Append("               SUM( DECODE(OPER, 'A0405', RES_CNT, 0)) DA5, SUM( DECODE(OPER, 'A0605', RES_CNT, 0)) WB5," + "\n");
            strSqlString.Append("               SUM( CASE WHEN OPER IN ( 'A0406',  'A0407', 'A0408', 'A0409' ) THEN  RES_CNT  ELSE 0 END ) DA_ETC," + "\n");
            strSqlString.Append("               SUM( CASE WHEN OPER IN ( 'A0606',  'A0607', 'A0608', 'A0609' ) THEN  RES_CNT  ELSE 0 END ) WB_ETC" + "\n");
            strSqlString.Append("            FROM (  SELECT FACTORY, RES_STS_2 MAT_ID, RES_GRP_6 RES_MODEL, RES_GRP_7 UPEH_GROUP, RES_STS_8 OPER , COUNT(RES_ID) RES_CNT" + "\n");
            strSqlString.Append("                  FROM  MRASRESDEF  " + "\n");
            strSqlString.Append("               WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("                AND DELETE_FLAG  = ' '  " + "\n");
            strSqlString.Append("                AND RES_TYPE  = 'EQUIPMENT' " + "\n");
            strSqlString.Append("                AND RES_STS_8 IN('A0400','A0401','A0402','A0403','A0404','A0405','A0600','A0601','A0602','A0603','A0604','A0605') " + "\n");
            strSqlString.Append("               GROUP BY FACTORY, RES_STS_2, RES_GRP_6, RES_GRP_7, RES_STS_8 )" + "\n");

            strSqlString.Append("            WHERE MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            strSqlString.Append("            GROUP BY  MAT_ID" + "\n");

            strSqlString.Append("            UNION ALL" + "\n");
            strSqlString.Append("            SELECT '4_CAPA' GUBUN, MAT_ID," + "\n");
            strSqlString.Append("                   SUM( CASE WHEN OPER LIKE 'A040%' THEN   RES_CNT * NVL(UPEH,0) * 24   ELSE 0  END ) RES_DA_TTL," + "\n");
            strSqlString.Append("                   SUM( CASE WHEN  OPER LIKE 'A060%' THEN   RES_CNT * NVL(UPEH,0) * 24  ELSE 0  END ) RES_WB_TTL," + "\n");
            strSqlString.Append("                   SUM( CASE WHEN OPER IN ( 'A0400',  'A0401' ) THEN  RES_CNT * NVL(UPEH,0) * 24 ELSE 0 END ) RES_CNT_DA1" + "\n");
            strSqlString.Append("                   , SUM( CASE WHEN OPER IN ( 'A0600',  'A0601' ) THEN  RES_CNT * NVL(UPEH,0) * 24 ELSE 0 END )  RES_CNT_WB1," + "\n");
            strSqlString.Append("                   SUM( DECODE( OPER, 'A0402',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_DA2, SUM( DECODE( OPER, 'A0602', RES_CNT *  NVL(UPEH,0) * 24, 0)) RES_CNT_WB2," + "\n");
            strSqlString.Append("                   SUM( DECODE( OPER, 'A0403',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_DA3, SUM( DECODE( OPER, 'A0603',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_WB3," + "\n");
            strSqlString.Append("                   SUM( DECODE( OPER, 'A0404',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_DA4, SUM( DECODE( OPER, 'A0604',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_WB4," + "\n");
            strSqlString.Append("                   SUM( DECODE( OPER, 'A0405',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_DA5, SUM( DECODE( OPER, 'A0605',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_WB5," + "\n");
            strSqlString.Append("                   SUM( CASE WHEN OPER IN ( 'A0406',  'A0407', 'A0408', 'A0409' ) THEN  RES_CNT * NVL(UPEH,0) * 24 ELSE 0 END ) DA_ETC," + "\n");
            strSqlString.Append("                   SUM( CASE WHEN OPER IN ( 'A0606',  'A0607', 'A0608', 'A0609' ) THEN  RES_CNT * NVL(UPEH,0) * 24 ELSE 0 END ) WB_ETC" + "\n");
            strSqlString.Append("              FROM (  SELECT RAS.FACTORY, RAS.RES_GRP_6 RES_MODEL, RAS.RES_STS_2 MAT_ID, RAS.RES_STS_8 OPER , COUNT(RES_ID) RES_CNT," + "\n");
            strSqlString.Append("                             MAX(DECODE(SUBSTR(RAS.RES_STS_8, 1, 3), 'A04', NVL(UPEH.UPEH, 0) * 0.75,  NVL(UPEH.UPEH, 0))  ) UPEH " + "\n");
            strSqlString.Append("                        FROM MRASRESDEF RAS, CRASUPHDEF UPEH" + "\n");
            strSqlString.Append("                       WHERE RAS.FACTORY   = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                         AND RAS.RES_CMF_9 = 'Y'" + "\n");
            strSqlString.Append("                         AND RAS.DELETE_FLAG  = ' '" + "\n");
            strSqlString.Append("                         AND RAS.RES_TYPE  = 'EQUIPMENT'" + "\n");
            strSqlString.Append("                         AND (RAS.RES_STS_8 LIKE 'A040%' OR RAS.RES_STS_8 LIKE 'A060%') " + "\n");
            strSqlString.Append("                         AND RAS.RES_STS_2 LIKE '" + txtSearchProduct.Text + "'" + "\n");
            strSqlString.Append("                         AND RAS.FACTORY = UPEH.FACTORY(+)" + "\n");
            strSqlString.Append("                         AND RAS.RES_GRP_6 = UPEH.RES_MODEL(+)" + "\n");
            strSqlString.Append("                         AND RAS.RES_STS_2 = UPEH.MAT_ID(+)" + "\n");
            strSqlString.Append("                         AND RAS.RES_STS_8 = UPEH.OPER(+)" + "\n");
            strSqlString.Append("                       GROUP BY RAS.FACTORY, RAS.RES_GRP_6, RAS.RES_STS_2, RAS.RES_STS_8 )" + "\n");
            strSqlString.Append("             GROUP BY MAT_ID" + "\n");

            strSqlString.Append("          UNION ALL" + "\n");
            strSqlString.Append("          SELECT B.GUBUN, A.MAT_ID" + "\n");
            strSqlString.Append("              , ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0), 'Merge', NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0)" + "\n");
            strSqlString.Append("                 END),0) )  ) DA_WIP_TTL" + "\n");
            strSqlString.Append("              , ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0), 'Merge', NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) WB_WIP_TTL" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN " + "\n");
            strSqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 0)" + "\n");
            strSqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)  ELSE 0 END" + "\n");
            strSqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)" + "\n");
            strSqlString.Append("                       END), 0)  ) ) AS DA_1" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1+GATE_1,0), 'Merge', NVL(WB_1+GATE_1,0), 0)" + "\n");
            strSqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_1+GATE_1,0) ELSE 0 END" + "\n");
            strSqlString.Append("                         ELSE NVL(WB_1+GATE_1,0)" + "\n");
            strSqlString.Append("                       END),0)   ) ) AS WB_1" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_2,0), 'Merge', NVL(DA_2,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_2,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_2,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) AS DA_2" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_2+GATE_2,0), 'Merge', NVL(WB_2+GATE_2,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_2+GATE_2,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_2+GATE_2,0)" + "\n");
            strSqlString.Append("                 END),0)   )  ) AS WB_2" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_3,0), 'Merge', NVL(DA_3,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_3,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_3,0)" + "\n");
            strSqlString.Append("                 END),0)  )  ) AS DA_3" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_3+GATE_3,0), 'Merge', NVL(WB_3+GATE_3,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_3+GATE_3,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_3+GATE_3,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS WB_3" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_4,0), 'Merge', NVL(DA_4,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_4,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_4,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS DA_4" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_4+GATE_4,0), 'Merge', NVL(WB_4+GATE_4,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_4+GATE_4,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_4+GATE_4,0)" + "\n");
            strSqlString.Append("                 END),0)  ) ) AS WB_4" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_5,0), 'Merge', NVL(DA_5,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_5,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_5,0)" + "\n");
            strSqlString.Append("                 END),0)   )) AS DA_5" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_5+GATE_5,0), 'Merge', NVL(WB_5+GATE_5,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_5+GATE_5,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_5+GATE_5,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS WB_5" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_ETC,0), 'Merge', NVL(DA_ETC,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_ETC,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)  ) ) AS DA_ETC" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_ETC+GATE_ETC,0), 'Merge', NVL(WB_ETC+GATE_ETC,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_ETC+GATE_ETC,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_ETC+GATE_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) AS WB_ETC" + "\n");
            strSqlString.Append("           FROM MWIPMATDEF A," + "\n");
            strSqlString.Append("              (  SELECT '6_WIP_MERGE' GUBUN, LOT.MAT_ID " + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0250', 'A0305', 'A0306', 'A0310', 'A0400', 'A0401', 'A0500', 'A0501', 'A0530', 'A0531' ) THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0402', 'A0502', 'A0532') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_2" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0403', 'A0503', 'A0533') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_3" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0404', 'A0504', 'A0534') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_4" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0405', 'A0505', 'A0535') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_5" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ( 'A0406', 'A0506', 'A0536','A0407', 'A0507', 'A0537','A0408', 'A0508', 'A0538','A0409', 'A0509', 'A0539' ) THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_ETC  " + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0550', 'A0551', 'A0600','A0601' ) THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0552', 'A0602') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_2" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0553', 'A0603') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_3" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0554', 'A0604') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_4" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0555', 'A0605') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_5" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A0606', 'A0556',  'A0607', 'A0557',  'A0608', 'A0558',  'A0609', 'A0559' ) THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_ETC" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0800', 'A0801') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0802') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_2" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0803') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_3" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0804') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_4" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0805') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_5" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0806','A0807','A0808','A0809') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_ETC" + "\n");
            strSqlString.Append("                FROM ( SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1" + "\n");
            strSqlString.Append("                      , SUM(A.QTY_1) QTY" + "\n");
            strSqlString.Append("                  FROM RWIPLOTSTS A, MWIPOPRDEF B " + "\n");
            strSqlString.Append("                   WHERE 1 = 1 " + "\n");
            strSqlString.Append("                    AND A.FACTORY = B.FACTORY(+)" + "\n");
            strSqlString.Append("                    AND A.OPER = B.OPER(+)" + "\n");
            strSqlString.Append("                    AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                    AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                    AND A.LOT_CMF_5 LIKE 'P%' " + "\n");
            strSqlString.Append("                    AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                   GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT " + "\n");
            strSqlString.Append("               WHERE 1 = 1" + "\n");
            strSqlString.Append("                 AND LOT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("                 AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                 AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-'" + "\n");
            strSqlString.Append("                 AND MAT.MAT_TYPE = 'FG' AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            strSqlString.Append("                 AND MAT.MAT_GRP_2 <> '-'" + "\n");
            
            strSqlString.Append("               GROUP BY LOT.MAT_ID" + "\n");
            
            strSqlString.Append("      ) B," + "\n");
            strSqlString.Append("               ( SELECT KEY_1,DATA_1" + "\n");
            strSqlString.Append("                FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("               WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                 AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')" + "\n");
            strSqlString.Append("                ) G" + "\n");
            strSqlString.Append("           WHERE B.MAT_ID = A.MAT_ID(+)" + "\n");
            strSqlString.Append("            AND B.MAT_ID = G.KEY_1(+)" + "\n");

            strSqlString.Append("            AND B.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            strSqlString.Append("            AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("            GROUP BY B.GUBUN, A.MAT_ID" + "\n");

            strSqlString.Append("  UNION ALL SELECT B.GUBUN, A.MAT_ID" + "\n");
            strSqlString.Append("              , ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0), 'Merge', NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0)" + "\n");
            strSqlString.Append("                 END),0) )  ) DA_WIP_TTL" + "\n");
            strSqlString.Append("              , ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0), 'Merge', NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) WB_WIP_TTL" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN " + "\n");
            strSqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 0)" + "\n");
            strSqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)" + "\n");
            strSqlString.Append("                       END), 0)  ) ) AS DA_1" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1+GATE_1,0), 'Merge', NVL(WB_1+GATE_1,0), 0)" + "\n");
            strSqlString.Append("                         ELSE NVL(WB_1+GATE_1,0) " + "\n");
            strSqlString.Append("                       END),0)   ) ) AS WB_1" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) AS DA_2" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_2+GATE_2,0), 'Merge', NVL(WB_2+GATE_2,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_2+GATE_2,0)" + "\n");
            strSqlString.Append("                 END),0)   )  ) AS WB_2 " + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0)" + "\n");
            strSqlString.Append("                 END),0)  )  ) AS DA_3" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_3+GATE_3,0), 'Merge', NVL(WB_3+GATE_3,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_3+GATE_3,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS WB_3" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS DA_4" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_4+GATE_4,0), 'Merge', NVL(WB_4+GATE_4,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_4+GATE_4,0)" + "\n");
            strSqlString.Append("                 END),0)  ) ) AS WB_4" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0)" + "\n");
            strSqlString.Append("                 END),0)   )) AS DA_5" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_5+GATE_5,0), 'Merge', NVL(WB_5+GATE_5,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_5+GATE_5,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS WB_5 " + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_ETC,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_ETC,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)  ) ) AS DA_ETC" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_ETC+GATE_ETC,0), 'Merge', NVL(WB_ETC+GATE_ETC,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_ETC+GATE_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) AS WB_ETC" + "\n");
            strSqlString.Append("           FROM MWIPMATDEF A," + "\n");
            strSqlString.Append("              (SELECT  '5_WIP_CHIP' GUBUN, LOT.MAT_ID" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1,  0 DA_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0402', 'A0502', 'A0532') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                 ELSE 0  END, 0) ) DA_2" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0403', 'A0503', 'A0533') AND  MAT.MAT_GRP_5 = '3rd' THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) DA_3" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0404', 'A0504', 'A0534') AND  MAT.MAT_GRP_5 = '4th'  THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) DA_4" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0405', 'A0505', 'A0535') AND  MAT.MAT_GRP_5 = '5th' THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) DA_5" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ( 'A0406', 'A0506', 'A0536','A0407', 'A0507', 'A0537','A0408', 'A0508', 'A0538','A0409', 'A0509', 'A0539' ) AND MAT.MAT_GRP_5 IN ( '6th',  '7th', '8th', '9th') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                ELSE 0 END, 0) ) DA_ETC" + "\n");
            strSqlString.Append("                  , 0 WB_1, 0 WB_2, 0 WB_3, 0 WB_4, 0 WB_5, 0 WB_ETC, 0 GATE_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0802') AND  MAT.MAT_GRP_5 = '2nd'THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                 ELSE 0 END, 0) ) GATE_2" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0803') AND  MAT.MAT_GRP_5 = '3rd'THEN" + "\n");
            strSqlString.Append("                                     DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) GATE_3" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0804') AND  MAT.MAT_GRP_5 = '4th' THEN" + "\n");
            strSqlString.Append("                                     DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) GATE_4" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0805') AND  MAT.MAT_GRP_5 = '5th'THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) GATE_5" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0806','A0807','A0808','A0809') AND MAT.MAT_GRP_5 IN ( '6th',  '7th', '8th', '9th')  THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) GATE_ETC" + "\n");
            strSqlString.Append("               FROM (   SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1" + "\n");
            strSqlString.Append("                    , SUM(A.QTY_1) QTY " + "\n");
            strSqlString.Append("                   FROM RWIPLOTSTS A, MWIPOPRDEF B " + "\n");
            strSqlString.Append("                 WHERE 1 = 1" + "\n");
            strSqlString.Append("                  AND A.FACTORY = B.FACTORY(+) " + "\n");
            strSqlString.Append("                  AND A.OPER = B.OPER(+)" + "\n");
            strSqlString.Append("                  AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                  AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                  AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
            strSqlString.Append("                  AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                 GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT" + "\n");
            strSqlString.Append("              WHERE 1 = 1" + "\n");
            strSqlString.Append("               AND LOT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("               AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("               AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU')" + "\n");
            strSqlString.Append("               AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
            strSqlString.Append("               AND MAT.MAT_TYPE = 'FG' AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            strSqlString.Append("               AND MAT.MAT_GRP_2 <> '-'" + "\n");
            strSqlString.Append("              GROUP BY  LOT.MAT_ID ) B," + "\n");
            strSqlString.Append("               ( SELECT KEY_1,DATA_1" + "\n");
            strSqlString.Append("                FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("               WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                 AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')" + "\n");
            strSqlString.Append("                ) G   " + "\n");
            strSqlString.Append("           WHERE B.MAT_ID = A.MAT_ID(+) " + "\n");
            strSqlString.Append("            AND B.MAT_ID = G.KEY_1(+)" + "\n");

            strSqlString.Append("            AND B.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            strSqlString.Append("            AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("            GROUP BY B.GUBUN, A.MAT_ID" + "\n");

            strSqlString.Append("            UNION ALL" + "\n");
            strSqlString.Append("            SELECT '7_실적' GUBUN, MAT_ID," + "\n");
            strSqlString.Append("              SUM( CASE WHEN OPER LIKE 'A040%' THEN  QTY  ELSE 0  END ) DA_RST_TTL ,  SUM( CASE WHEN OPER LIKE 'A060%' THEN  QTY  ELSE 0  END ) WB_RST_TTL," + "\n");
            strSqlString.Append("              SUM( CASE WHEN OPER IN ( 'A0400', 'A0401') THEN QTY  ELSE 0 END ) DA401 , SUM( CASE WHEN OPER IN ( 'A0600', 'A0601') THEN QTY ELSE 0 END )  WB601," + "\n");
            strSqlString.Append("              SUM(DECODE(OPER, 'A0402', QTY,  0)) DA402  , SUM(DECODE(OPER, 'A0602', QTY,  0)) WB602," + "\n");
            strSqlString.Append("              SUM(DECODE(OPER, 'A0403', QTY,  0)) DA403  , SUM(DECODE(OPER, 'A0603', QTY,  0)) WB603," + "\n");
            strSqlString.Append("              SUM(DECODE(OPER, 'A0404', QTY,  0)) DA404  , SUM(DECODE(OPER, 'A0604', QTY,  0)) WB604," + "\n");
            strSqlString.Append("              SUM(DECODE(OPER, 'A0405', QTY,  0)) DA405  , SUM(DECODE(OPER, 'A0605', QTY,  0)) WB605," + "\n");
            strSqlString.Append("              SUM( CASE WHEN OPER IN ( 'A0406',   'A0407',  'A0408',  'A0409') THEN QTY  ELSE 0 END ) DA_Etc," + "\n");
            strSqlString.Append("              SUM( CASE WHEN OPER IN ( 'A0606',   'A0607',  'A0608',  'A0609') THEN  QTY ELSE 0 END ) WB_Etc" + "\n");
            strSqlString.Append("             FROM (  SELECT MAT_ID, OPER, SUM(S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ) QTY" + "\n");
            strSqlString.Append("                 FROM RSUMWIPMOV A" + "\n");
            strSqlString.Append("              WHERE 1=1" + "\n");
            strSqlString.Append("               AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("               AND A.MAT_VER  = 1 " + "\n");

            strSqlString.Append("               AND A.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            strSqlString.Append("               AND A.WORK_DATE = '" + selectDate[selectDate.Count - 1] + "'" + "\n");
            strSqlString.Append("               AND A.LOT_TYPE  = 'W'" + "\n");
            strSqlString.Append("               AND ( A.OPER LIKE  'A040%' OR  A.OPER LIKE  'A060%'  )" + "\n");
            strSqlString.Append("               AND A.CM_KEY_3 LIKE 'P%'" + "\n");
            strSqlString.Append("              GROUP BY A.MAT_ID, A.OPER  )" + "\n");
            strSqlString.Append("            GROUP BY MAT_ID" + "\n");
            strSqlString.Append("           UNION ALL" + "\n");
            strSqlString.Append("           SELECT '8_예상실적' GUBUN, MAT_ID," + "\n");
            strSqlString.Append("               ROUND( (DA_RST_TTL / NOW_MM * 60*24) )  DA_RST_TTL, ROUND( ( WB_RST_TTL/ NOW_MM * 60*24)) WB_RST_TTL," + "\n");
            strSqlString.Append("               ROUND((DA401 /NOW_MM * 60 * 24)) DA401, ROUND((WB601 /NOW_MM * 60 * 24)) WB601," + "\n");
            strSqlString.Append("               ROUND((DA402 /NOW_MM * 60 * 24)) DA402, ROUND((WB602 /NOW_MM * 60 * 24)) WB602," + "\n");
            strSqlString.Append("               ROUND((DA403 /NOW_MM * 60 * 24)) DA403, ROUND((WB603 /NOW_MM * 60 * 24)) WB603," + "\n");
            strSqlString.Append("               ROUND((DA404 /NOW_MM * 60 * 24)) DA404, ROUND((WB604 /NOW_MM * 60 * 24)) WB604," + "\n");
            strSqlString.Append("               ROUND((DA405 /NOW_MM * 60 * 24)) DA405, ROUND((WB605 /NOW_MM * 60 * 24)) WB605," + "\n");
            strSqlString.Append("               ROUND((DA_Etc/NOW_MM * 60 * 24)) DA_Etc, ROUND((WB_Etc/NOW_MM * 60 * 24)) WB_Etc" + "\n");
            strSqlString.Append("           FROM (  SELECT MAT_ID," + "\n");
            strSqlString.Append("                 SUM( CASE WHEN OPER LIKE 'A040%' THEN  QTY  ELSE 0  END ) DA_RST_TTL ,  SUM( CASE WHEN OPER LIKE 'A060%' THEN  QTY  ELSE 0  END ) WB_RST_TTL," + "\n");
            strSqlString.Append("                 SUM( CASE WHEN OPER IN ( 'A0400', 'A0401') THEN QTY  ELSE 0 END ) DA401 , SUM( CASE WHEN OPER IN ( 'A0600', 'A0601') THEN QTY ELSE 0 END )  WB601," + "\n");
            strSqlString.Append("                 SUM(DECODE(OPER, 'A0402', QTY,  0)) DA402  , SUM(DECODE(OPER, 'A0602', QTY,  0)) WB602," + "\n");
            strSqlString.Append("                 SUM(DECODE(OPER, 'A0403', QTY,  0)) DA403  , SUM(DECODE(OPER, 'A0603', QTY,  0)) WB603," + "\n");
            strSqlString.Append("                 SUM(DECODE(OPER, 'A0404', QTY,  0)) DA404  , SUM(DECODE(OPER, 'A0604', QTY,  0)) WB604," + "\n");
            strSqlString.Append("                 SUM(DECODE(OPER, 'A0405', QTY,  0)) DA405  , SUM(DECODE(OPER, 'A0605', QTY,  0)) WB605," + "\n");
            strSqlString.Append("                 SUM( CASE WHEN OPER IN ( 'A0406',   'A0407',  'A0408',  'A0409') THEN QTY  ELSE 0 END ) DA_Etc," + "\n");

            if (Check_Day.Substring(6).Equals("01"))
                strSqlString.Append("                 SUM( CASE WHEN OPER IN ( 'A0606',   'A0607',  'A0608',  'A0609') THEN  QTY ELSE 0 END ) WB_Etc,  (SYSDATE - TO_DATE('" + Last_Month_Last_day + "220000', 'YYYYMMDDHH24MISS') ) * 24 * 60 NOW_MM " + "\n");
            else strSqlString.Append("                 SUM( CASE WHEN OPER IN ( 'A0606',   'A0607',  'A0608',  'A0609') THEN  QTY ELSE 0 END ) WB_Etc,  (SYSDATE - TO_DATE('" + selectDate[selectDate.Count - 2] + "220000', 'YYYYMMDDHH24MISS') ) * 24 * 60 NOW_MM " + "\n");

            strSqlString.Append("                FROM (  SELECT MAT_ID, OPER, SUM(S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ) QTY" + "\n");
            strSqlString.Append("                    FROM RSUMWIPMOV A  " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                  AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                  AND A.MAT_VER  = 1" + "\n");

            strSqlString.Append("                  AND A.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            strSqlString.Append("                  AND A.WORK_DATE = '" + selectDate[selectDate.Count - 1] + "'" + "\n");
            strSqlString.Append("                  AND A.LOT_TYPE  = 'W'" + "\n");
            strSqlString.Append("                  AND ( A.OPER LIKE  'A040%' OR  A.OPER LIKE  'A060%'  )" + "\n");
            strSqlString.Append("                  AND A.CM_KEY_3 LIKE 'P%'" + "\n");
            strSqlString.Append("                 GROUP BY A.MAT_ID, A.OPER  )" + "\n");
            strSqlString.Append("               GROUP BY MAT_ID  ) ) A,  (SELECT * FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  AND MAT_TYPE = 'FG' AND MAT_VER =  1 AND  DELETE_FLAG <> 'Y' AND MAT_GRP_2 <> '-' ) B" + "\n");
            strSqlString.Append("            WHERE A.MAT_ID = B.MAT_ID(+)  " + "\n");

            strSqlString.Append("             ) A," + "\n");

            strSqlString.Append("           ( SELECT SEQ FROM HRTDSUMSEQ@RPTTOMES WHERE SEQ < 9 ) C," + "\n");

            strSqlString.Append("           (  SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER LIKE 'A040%' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0) ELSE 0 END) DA_TTL," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER LIKE 'A060%' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) WB_TTL," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER IN ('A0400', 'A0401') THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA1," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0402' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA2," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0403' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA3," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0404' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA4," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0405' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA5," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER IN ('A0600', 'A0601') THEN NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) WB1," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0602' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0) ELSE 0 END) WB2," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0603' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0) ELSE 0 END) WB3," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0604' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) WB4," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0605' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) WB5," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER IN ('A0406', 'A0407', 'A0408', 'A0409') THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA_ETC," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER IN ('A0606', 'A0607', 'A0608', 'A0609') THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) WB_ETC " + "\n");
            strSqlString.Append("                 FROM RSUMWIPMOV A," + "\n");
            strSqlString.Append("                      (SELECT *  FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  AND MAT_TYPE = 'FG' AND MAT_VER =  1 AND  DELETE_FLAG <> 'Y' AND MAT_GRP_2 <> '-' ) MAT" + "\n");
            strSqlString.Append("               WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                    AND A.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                    AND A.CM_KEY_2 = 'PROD'" + "\n");
            strSqlString.Append("                    AND A.MAT_VER = 1 " + "\n");
            strSqlString.Append("                    AND ( A.OPER LIKE 'A040%'  OR  A.OPER LIKE 'A060%' )" + "\n");
            strSqlString.Append("                    AND A.WORK_DATE = '" + selectDate[selectDate.Count - 1] + "' AND A.MAT_ID = MAT.MAT_ID(+)" + "\n");
            strSqlString.Append("              GROUP BY MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11   )  RCV" + "\n");
            strSqlString.Append("     WHERE A.MAT_GRP_1 = RCV.MAT_GRP_1(+)" + "\n");
            strSqlString.Append("       AND A.MAT_GRP_9 = RCV.MAT_GRP_9(+)" + "\n");
            strSqlString.Append("       AND A.MAT_GRP_10 =RCV.MAT_GRP_10(+)" + "\n");
            strSqlString.Append("       AND A.MAT_GRP_6=RCV.MAT_GRP_6(+)" + "\n");
            strSqlString.Append("       AND A.MAT_CMF_11=RCV.MAT_CMF_11(+)" + "\n");

            //상세조회
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);
            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);
            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);
            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);
            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);
            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);
            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);


            strSqlString.Append("     GROUP BY A.MAT_GRP_1, A.MAT_GRP_9," + "\n");
            strSqlString.Append("              DECODE(SEQ, 1, 'SOP 잔량', 2, '일목표', 3, '소요대수', 4, 'CAPA', 5, 'CHIP', 6, 'MERGE', 7, '실적', '예상실적'), SEQ" + "\n");

            /**************************************************************************
             *  Row Data                                                              *
             **************************************************************************/

            strSqlString.Append("     UNION ALL SELECT " + QueryCond1 + "\n");
            strSqlString.Append("                  , DECODE(SEQ, 1, 'SOP 잔량', 2, '일목표', 3, 'Arange 댓수', 4, 'CAPA', 5, 'CHIP', 6, 'MERGE', 7, '실적', '예상실적') GUBUN, SEQ GUBUN_SEQ," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA_TTL - RCV.DA_TTL, A.DA_TTL), 0 ) ) DA_TTL," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB_TTL - RCV.WB_TTL, A.WB_TTL), 0 ) ) WB_TTL," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA1 - RCV.DA1, A.DA1), 0 ) ) DA1," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA2 - RCV.DA2, A.DA2), 0 ) ) DA2," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA3 - RCV.DA3, A.DA3), 0 ) ) DA3," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA4 - RCV.DA4, A.DA4), 0 ) ) DA4," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA5 - RCV.DA5, A.DA5), 0 ) ) DA5," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB1 - RCV.WB1, A.WB1), 0 ) ) WB1," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB2 - RCV.WB2, A.WB2), 0 ) ) WB2," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB3 - RCV.WB3, A.WB3), 0 ) ) WB3," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB4 - RCV.WB4, A.WB4), 0 ) ) WB4," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB5 - RCV.WB5, A.WB5), 0 ) ) WB5," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA_ETC - RCV.DA_ETC, A.DA_ETC), 0 ) ) DA_ETC," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB_ETC - RCV.WB_ETC, A.WB_ETC), 0 ) ) WB_ETC" + "\n");

            strSqlString.Append("        FROM (" + "\n");

            strSqlString.Append("       SELECT MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, GUBUN" + "\n");
            strSqlString.Append("           , SUM(DA1) +SUM(DA2) +SUM(DA3) + SUM(DA4) + SUM(DA5) DA_TTL," + "\n");
            strSqlString.Append("           SUM(WB1) +SUM(WB2) +SUM(WB3) +SUM(WB4) +SUM(WB5) WB_TTL," + "\n");
            strSqlString.Append("           SUM(DA1) DA1,SUM(WB1) WB1," + "\n");
            strSqlString.Append("           SUM(DA2) DA2,SUM(WB2) WB2," + "\n");
            strSqlString.Append("           SUM(DA3) DA3,SUM(WB3) WB3," + "\n");
            strSqlString.Append("           SUM(DA4) DA4,SUM(WB4) WB4," + "\n");
            strSqlString.Append("           SUM(DA5) DA5,SUM(WB5) WB5," + "\n");
            strSqlString.Append("           SUM(DA6) + SUM(DA7) + SUM(DA8) + SUM(DA9) DA_ETC," + "\n");
            strSqlString.Append("           SUM(WB6) +SUM(WB7) +SUM(WB8) +SUM(WB9) WB_ETC" + "\n");
            strSqlString.Append("  FROM  (" + "\n");

            strSqlString.Append("           SELECT MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, MAT_KEY,'1_SOP_잔량'  GUBUN," + "\n");
            strSqlString.Append("                   DECODE(MAX_DA1,0,MIN_DA1,MAX_DA1) DA1,  DECODE(MAX_DA6,0,MIN_DA6,MAX_DA6) DA6" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA2,0,MIN_DA2,MAX_DA2) DA2, DECODE(MAX_DA7,0,MIN_DA7,MAX_DA7) DA7" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA3,0,MIN_DA3,MAX_DA3) DA3, DECODE(MAX_DA8,0,MIN_DA8,MAX_DA8) DA8" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA4,0,MIN_DA4,MAX_DA4) DA4, DECODE(MAX_DA9,0,MIN_DA9,MAX_DA9) DA9" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA5,0,MIN_DA5,MAX_DA5) DA5" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB1,0,MIN_WB1,MAX_WB1) WB1, DECODE(MAX_WB6,0,MIN_WB6,MAX_WB6) WB6" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB2,0,MIN_WB2,MAX_WB2) WB2, DECODE(MAX_WB7,0,MIN_WB7,MAX_WB7) WB7" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB3,0,MIN_WB3,MAX_WB3) WB3, DECODE(MAX_WB8,0,MIN_WB8,MAX_WB8) WB8" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB4,0,MIN_WB4,MAX_WB4) WB4, DECODE(MAX_WB9,0,MIN_WB9,MAX_WB9) WB9" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB5,0,MIN_WB5,MAX_WB5) WB5" + "\n");
            strSqlString.Append("             FROM (" + "\n");
            strSqlString.Append("                     SELECT CUSTOMER MAT_GRP_1, FAMILY MAT_GRP_2, PKG MAT_GRP_3, TYPE1 MAT_GRP_4, TYPE2 MAT_GRP_5, LD_COUNT MAT_GRP_6, DENSITY MAT_GRP_7, GENERATION MAT_GRP_8, MAJOR_CODE MAT_GRP_9, PKG2 MAT_GRP_10, PKG_CODE MAT_CMF_11, MAT_KEY," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA1), 'HX', MAX(DA1), SUM(DA1) ) MAX_DA1,DECODE(CUSTOMER, 'SE', MIN(DA1), 'HX', MIN(DA1),0) MIN_DA1," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA2), 'HX', MAX(DA2), SUM(DA2) ) MAX_DA2,DECODE(CUSTOMER, 'SE', MIN(DA2), 'HX', MIN(DA2),0) MIN_DA2," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA3), 'HX', MAX(DA3), SUM(DA3) ) MAX_DA3,DECODE(CUSTOMER, 'SE', MIN(DA3), 'HX', MIN(DA3),0) MIN_DA3," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA4), 'HX', MAX(DA4), SUM(DA4) ) MAX_DA4,DECODE(CUSTOMER, 'SE', MIN(DA4), 'HX', MIN(DA4),0) MIN_DA4," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA5), 'HX', MAX(DA5), SUM(DA5) ) MAX_DA5,DECODE(CUSTOMER, 'SE', MIN(DA5), 'HX', MIN(DA5),0) MIN_DA5," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA6), 'HX', MAX(DA6), SUM(DA6) ) MAX_DA6,DECODE(CUSTOMER, 'SE', MIN(DA6), 'HX', MIN(DA6),0) MIN_DA6," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA7), 'HX', MAX(DA7), SUM(DA7) ) MAX_DA7,DECODE(CUSTOMER, 'SE', MIN(DA7), 'HX', MIN(DA7),0) MIN_DA7," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA8), 'HX', MAX(DA8), SUM(DA8) ) MAX_DA8,DECODE(CUSTOMER, 'SE', MIN(DA8), 'HX', MIN(DA8),0) MIN_DA8," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA9), 'HX', MAX(DA9), SUM(DA9) ) MAX_DA9,DECODE(CUSTOMER, 'SE', MIN(DA9), 'HX', MIN(DA9),0) MIN_DA9," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB1), 'HX', MAX(WB1), SUM(WB1) ) MAX_WB1,DECODE(CUSTOMER, 'SE', MIN(WB1), 'HX', MIN(WB1),0) MIN_WB1," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB2), 'HX', MAX(WB2), SUM(WB2) ) MAX_WB2,DECODE(CUSTOMER, 'SE', MIN(WB2), 'HX', MIN(WB2),0) MIN_WB2," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB3), 'HX', MAX(WB3), SUM(WB3) ) MAX_WB3,DECODE(CUSTOMER, 'SE', MIN(WB3), 'HX', MIN(WB3),0) MIN_WB3," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB4), 'HX', MAX(WB4), SUM(WB4) ) MAX_WB4,DECODE(CUSTOMER, 'SE', MIN(WB4), 'HX', MIN(WB4),0) MIN_WB4," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB5), 'HX', MAX(WB5), SUM(WB5) ) MAX_WB5,DECODE(CUSTOMER, 'SE', MIN(WB5), 'HX', MIN(WB5),0) MIN_WB5," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB6), 'HX', MAX(WB6), SUM(WB6) ) MAX_WB6,DECODE(CUSTOMER, 'SE', MIN(WB6), 'HX', MIN(WB6),0) MIN_WB6," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB7), 'HX', MAX(WB7), SUM(WB7) ) MAX_WB7,DECODE(CUSTOMER, 'SE', MIN(WB7), 'HX', MIN(WB7),0) MIN_WB7," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB8), 'HX', MAX(WB8), SUM(WB8) ) MAX_WB8,DECODE(CUSTOMER, 'SE', MIN(WB8), 'HX', MIN(WB8),0) MIN_WB8," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB9), 'HX', MAX(WB9), SUM(WB9) ) MAX_WB9, DECODE(CUSTOMER, 'SE', MIN(WB9), 'HX', MIN(WB9),0) MIN_WB9" + "\n");
            strSqlString.Append("                       FROM RSUMOPRREM" + "\n");
            strSqlString.Append("                      WHERE WORK_DATE = '" + selectDate[selectDate.Count - 1] + "'" + "\n");
            strSqlString.Append("                        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                        AND RESV_FIELD_1 = ' '" + "\n");
            strSqlString.Append("                        AND MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
            strSqlString.Append("                      GROUP BY CUSTOMER , FAMILY , PKG , TYPE1 , TYPE2 , LD_COUNT , DENSITY , GENERATION , MAJOR_CODE , PKG2 , PKG_CODE  , MAT_KEY ) )" + "\n");
            strSqlString.Append("               GROUP BY MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, GUBUN" + "\n");
            strSqlString.Append("       UNION ALL" + "\n");
            strSqlString.Append("       SELECT MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, GUBUN" + "\n");
            strSqlString.Append("              , SUM(DA1) +SUM(DA2) +SUM(DA3) + SUM(DA4) + SUM(DA5) DA_TTL," + "\n");
            strSqlString.Append("              SUM(WB1) +SUM(WB2) +SUM(WB3) +SUM(WB4) +SUM(WB5) WB_TTL," + "\n");
            strSqlString.Append("              SUM(DA1) DA1,SUM(WB1) WB1," + "\n");
            strSqlString.Append("              SUM(DA2) DA2,SUM(WB2) WB2," + "\n");
            strSqlString.Append("              SUM(DA3) DA3,SUM(WB3) WB3," + "\n");
            strSqlString.Append("              SUM(DA4) DA4,SUM(WB4) WB4," + "\n");
            strSqlString.Append("              SUM(DA5) DA5,SUM(WB5) WB5," + "\n");
            strSqlString.Append("              SUM(DA6) + SUM(DA7) + SUM(DA8) + SUM(DA9) DA_ETC," + "\n");
            strSqlString.Append("              SUM(WB6) +SUM(WB7) +SUM(WB8) +SUM(WB9) WB_ETC" + "\n");
            strSqlString.Append("  FROM  (" + "\n");
            strSqlString.Append("           SELECT MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, MAT_KEY,'2_목표'  GUBUN," + "\n");
            strSqlString.Append("                   DECODE(MAX_DA1,0,MIN_DA1,MAX_DA1) DA1,  DECODE(MAX_DA6,0,MIN_DA6,MAX_DA6) DA6" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA2,0,MIN_DA2,MAX_DA2) DA2, DECODE(MAX_DA7,0,MIN_DA7,MAX_DA7) DA7" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA3,0,MIN_DA3,MAX_DA3) DA3, DECODE(MAX_DA8,0,MIN_DA8,MAX_DA8) DA8" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA4,0,MIN_DA4,MAX_DA4) DA4, DECODE(MAX_DA9,0,MIN_DA9,MAX_DA9) DA9" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA5,0,MIN_DA5,MAX_DA5) DA5" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB1,0,MIN_WB1,MAX_WB1) WB1, DECODE(MAX_WB6,0,MIN_WB6,MAX_WB6) WB6" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB2,0,MIN_WB2,MAX_WB2) WB2, DECODE(MAX_WB7,0,MIN_WB7,MAX_WB7) WB7" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB3,0,MIN_WB3,MAX_WB3) WB3, DECODE(MAX_WB8,0,MIN_WB8,MAX_WB8) WB8" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB4,0,MIN_WB4,MAX_WB4) WB4, DECODE(MAX_WB9,0,MIN_WB9,MAX_WB9) WB9" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB5,0,MIN_WB5,MAX_WB5) WB5" + "\n");
            strSqlString.Append("             FROM (" + "\n");
            strSqlString.Append("                     SELECT CUSTOMER MAT_GRP_1, FAMILY MAT_GRP_2, PKG MAT_GRP_3, TYPE1 MAT_GRP_4, TYPE2 MAT_GRP_5, LD_COUNT MAT_GRP_6, DENSITY MAT_GRP_7, GENERATION MAT_GRP_8, MAJOR_CODE MAT_GRP_9, PKG2 MAT_GRP_10, PKG_CODE MAT_CMF_11, MAT_KEY," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA1), 'HX', MAX(DA1), SUM(DA1) ) MAX_DA1,DECODE(CUSTOMER, 'SE', MIN(DA1), 'HX', MIN(DA1),0) MIN_DA1," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA2), 'HX', MAX(DA2), SUM(DA2) ) MAX_DA2,DECODE(CUSTOMER, 'SE', MIN(DA2), 'HX', MIN(DA2),0) MIN_DA2," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA3), 'HX', MAX(DA3), SUM(DA3) ) MAX_DA3,DECODE(CUSTOMER, 'SE', MIN(DA3), 'HX', MIN(DA3),0) MIN_DA3," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA4), 'HX', MAX(DA4), SUM(DA4) ) MAX_DA4,DECODE(CUSTOMER, 'SE', MIN(DA4), 'HX', MIN(DA4),0) MIN_DA4," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA5), 'HX', MAX(DA5), SUM(DA5) ) MAX_DA5,DECODE(CUSTOMER, 'SE', MIN(DA5), 'HX', MIN(DA5),0) MIN_DA5," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA6), 'HX', MAX(DA6), SUM(DA6) ) MAX_DA6,DECODE(CUSTOMER, 'SE', MIN(DA6), 'HX', MIN(DA6),0) MIN_DA6," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA7), 'HX', MAX(DA7), SUM(DA7) ) MAX_DA7,DECODE(CUSTOMER, 'SE', MIN(DA7), 'HX', MIN(DA7),0) MIN_DA7," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA8), 'HX', MAX(DA8), SUM(DA8) ) MAX_DA8,DECODE(CUSTOMER, 'SE', MIN(DA8), 'HX', MIN(DA8),0) MIN_DA8," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA9), 'HX', MAX(DA9), SUM(DA9) ) MAX_DA9,DECODE(CUSTOMER, 'SE', MIN(DA9), 'HX', MIN(DA9),0) MIN_DA9," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB1), 'HX', MAX(WB1), SUM(WB1) ) MAX_WB1,DECODE(CUSTOMER, 'SE', MIN(WB1), 'HX', MIN(WB1),0) MIN_WB1," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB2), 'HX', MAX(WB2), SUM(WB2) ) MAX_WB2,DECODE(CUSTOMER, 'SE', MIN(WB2), 'HX', MIN(WB2),0) MIN_WB2," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB3), 'HX', MAX(WB3), SUM(WB3) ) MAX_WB3,DECODE(CUSTOMER, 'SE', MIN(WB3), 'HX', MIN(WB3),0) MIN_WB3," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB4), 'HX', MAX(WB4), SUM(WB4) ) MAX_WB4,DECODE(CUSTOMER, 'SE', MIN(WB4), 'HX', MIN(WB4),0) MIN_WB4," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB5), 'HX', MAX(WB5), SUM(WB5) ) MAX_WB5,DECODE(CUSTOMER, 'SE', MIN(WB5), 'HX', MIN(WB5),0) MIN_WB5," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB6), 'HX', MAX(WB6), SUM(WB6) ) MAX_WB6,DECODE(CUSTOMER, 'SE', MIN(WB6), 'HX', MIN(WB6),0) MIN_WB6," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB7), 'HX', MAX(WB7), SUM(WB7) ) MAX_WB7,DECODE(CUSTOMER, 'SE', MIN(WB7), 'HX', MIN(WB7),0) MIN_WB7," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB8), 'HX', MAX(WB8), SUM(WB8) ) MAX_WB8,DECODE(CUSTOMER, 'SE', MIN(WB8), 'HX', MIN(WB8),0) MIN_WB8," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB9), 'HX', MAX(WB9), SUM(WB9) ) MAX_WB9, DECODE(CUSTOMER, 'SE', MIN(WB9), 'HX', MIN(WB9),0) MIN_WB9" + "\n");
            strSqlString.Append("                       FROM RSUMOPRREM" + "\n");
            strSqlString.Append("                      WHERE WORK_DATE = '" + selectDate[selectDate.Count - 1] + "'" + "\n");
            strSqlString.Append("                        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                        AND RESV_FIELD_1 = 'TARGET'" + "\n");
            strSqlString.Append("                        AND MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
            strSqlString.Append("                      GROUP BY  CUSTOMER , FAMILY , PKG , TYPE1 , TYPE2 , LD_COUNT , DENSITY , GENERATION , MAJOR_CODE , PKG2 , PKG_CODE  , MAT_KEY ) )" + "\n");
            strSqlString.Append("               GROUP BY MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, GUBUN" + "\n");

            strSqlString.Append("          UNION ALL" + "\n");
            strSqlString.Append("         SELECT B.MAT_GRP_1,  B.MAT_GRP_2,  B.MAT_GRP_3,  B.MAT_GRP_4,  B.MAT_GRP_5,  B.MAT_GRP_6,  B.MAT_GRP_7,  B.MAT_GRP_8,  B.MAT_GRP_9,  B.MAT_GRP_10,  B.MAT_CMF_11, GUBUN,  DA_TTL, WB_TTL, DA1, WB1, DA2, WB2, DA3, WB3, DA4, WB4, DA5, WB5, DA_ETC, WB_ETC" + "\n");
            
            strSqlString.Append("         FROM ( SELECT  '3_설비댓수' GUBUN, MAT_ID, " + "\n");
            strSqlString.Append("               SUM( CASE WHEN OPER LIKE 'A040%' THEN  RES_CNT  ELSE 0  END ) DA_TTL, SUM( CASE WHEN OPER LIKE 'A060%' THEN  RES_CNT  ELSE 0  END ) WB_TTL," + "\n");
            strSqlString.Append("               SUM( DECODE(OPER, 'A0400', RES_CNT, 'A0401', RES_CNT, 0) ) DA1, SUM( DECODE(OPER, 'A0600', RES_CNT, 'A0601', RES_CNT, 0)) WB1," + "\n");
            strSqlString.Append("               SUM( DECODE(OPER, 'A0402', RES_CNT, 0)) DA2, SUM( DECODE(OPER, 'A0602', RES_CNT, 0)) WB2," + "\n");
            strSqlString.Append("               SUM( DECODE(OPER, 'A0403', RES_CNT, 0)) DA3,SUM( DECODE(OPER, 'A0603', RES_CNT, 0)) WB3," + "\n");
            strSqlString.Append("               SUM( DECODE(OPER, 'A0404', RES_CNT, 0)) DA4, SUM( DECODE(OPER, 'A0604', RES_CNT, 0)) WB4," + "\n");
            strSqlString.Append("               SUM( DECODE(OPER, 'A0405', RES_CNT, 0)) DA5, SUM( DECODE(OPER, 'A0605', RES_CNT, 0)) WB5," + "\n");
            strSqlString.Append("               SUM( CASE WHEN OPER IN ( 'A0406',  'A0407', 'A0408', 'A0409' ) THEN  RES_CNT  ELSE 0 END ) DA_ETC," + "\n");
            strSqlString.Append("               SUM( CASE WHEN OPER IN ( 'A0606',  'A0607', 'A0608', 'A0609' ) THEN  RES_CNT  ELSE 0 END ) WB_ETC" + "\n");
            strSqlString.Append("            FROM (  SELECT FACTORY, RES_STS_2 MAT_ID, RES_GRP_6 RES_MODEL, RES_GRP_7 UPEH_GROUP, RES_STS_8 OPER , COUNT(RES_ID) RES_CNT" + "\n");
            strSqlString.Append("                  FROM  MRASRESDEF  " + "\n");
            strSqlString.Append("               WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("                AND DELETE_FLAG  = ' '  " + "\n");
            strSqlString.Append("                AND RES_TYPE  = 'EQUIPMENT' " + "\n");
            strSqlString.Append("                AND RES_STS_8 IN('A0400','A0401','A0402','A0403','A0404','A0405','A0600','A0601','A0602','A0603','A0604','A0605') " + "\n");

            strSqlString.Append("               GROUP BY FACTORY, RES_STS_2, RES_GRP_6, RES_GRP_7, RES_STS_8 )" + "\n");

            strSqlString.Append("            WHERE MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            strSqlString.Append("            GROUP BY  MAT_ID" + "\n");

            strSqlString.Append("  UNION ALL" + "\n");
            strSqlString.Append("            SELECT '4_CAPA' GUBUN, MAT_ID," + "\n");
            strSqlString.Append("                   SUM( CASE WHEN OPER LIKE 'A040%' THEN   RES_CNT * NVL(UPEH,0) * 24   ELSE 0  END ) RES_DA_TTL," + "\n");
            strSqlString.Append("                   SUM( CASE WHEN  OPER LIKE 'A060%' THEN   RES_CNT * NVL(UPEH,0) * 24  ELSE 0  END ) RES_WB_TTL," + "\n");
            strSqlString.Append("                   SUM( CASE WHEN OPER IN ( 'A0400',  'A0401' ) THEN  RES_CNT * NVL(UPEH,0) * 24 ELSE 0 END ) RES_CNT_DA1" + "\n");
            strSqlString.Append("                   , SUM( CASE WHEN OPER IN ( 'A0600',  'A0601' ) THEN  RES_CNT * NVL(UPEH,0) * 24 ELSE 0 END )  RES_CNT_WB1," + "\n");
            strSqlString.Append("                   SUM( DECODE( OPER, 'A0402',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_DA2, SUM( DECODE( OPER, 'A0602', RES_CNT *  NVL(UPEH,0) * 24, 0)) RES_CNT_WB2," + "\n");
            strSqlString.Append("                   SUM( DECODE( OPER, 'A0403',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_DA3, SUM( DECODE( OPER, 'A0603',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_WB3," + "\n");
            strSqlString.Append("                   SUM( DECODE( OPER, 'A0404',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_DA4, SUM( DECODE( OPER, 'A0604',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_WB4," + "\n");
            strSqlString.Append("                   SUM( DECODE( OPER, 'A0405',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_DA5, SUM( DECODE( OPER, 'A0605',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_WB5," + "\n");
            strSqlString.Append("                   SUM( CASE WHEN OPER IN ( 'A0406',  'A0407', 'A0408', 'A0409' ) THEN  RES_CNT * NVL(UPEH,0) * 24 ELSE 0 END ) DA_ETC," + "\n");
            strSqlString.Append("                   SUM( CASE WHEN OPER IN ( 'A0606',  'A0607', 'A0608', 'A0609' ) THEN  RES_CNT * NVL(UPEH,0) * 24 ELSE 0 END ) WB_ETC" + "\n");
            strSqlString.Append("              FROM (  SELECT RAS.FACTORY, RAS.RES_GRP_6 RES_MODEL, RAS.RES_STS_2 MAT_ID, RAS.RES_STS_8 OPER , COUNT(RES_ID) RES_CNT," + "\n");
            strSqlString.Append("                             MAX(DECODE(SUBSTR(RAS.RES_STS_8, 1, 3), 'A04', NVL(UPEH.UPEH, 0) * 0.75,  NVL(UPEH.UPEH, 0))  ) UPEH " + "\n");
            strSqlString.Append("                        FROM MRASRESDEF RAS, CRASUPHDEF UPEH" + "\n");
            strSqlString.Append("                       WHERE RAS.FACTORY   = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                         AND RAS.RES_CMF_9 = 'Y'" + "\n");
            strSqlString.Append("                         AND RAS.DELETE_FLAG  = ' '" + "\n");
            strSqlString.Append("                         AND RAS.RES_TYPE  = 'EQUIPMENT'" + "\n");
            strSqlString.Append("                         AND (RAS.RES_STS_8 LIKE 'A040%' OR RAS.RES_STS_8 LIKE 'A060%') " + "\n");
            strSqlString.Append("                         AND RAS.RES_STS_2 LIKE '" + txtSearchProduct.Text + "'" + "\n");
            strSqlString.Append("                         AND RAS.FACTORY = UPEH.FACTORY(+)" + "\n");
            strSqlString.Append("                         AND RAS.RES_GRP_6 = UPEH.RES_MODEL(+)" + "\n");
            strSqlString.Append("                         AND RAS.RES_STS_2 = UPEH.MAT_ID(+)" + "\n");
            strSqlString.Append("                         AND RAS.RES_STS_8 = UPEH.OPER(+)" + "\n");
            strSqlString.Append("                       GROUP BY RAS.FACTORY, RAS.RES_GRP_6, RAS.RES_STS_2, RAS.RES_STS_8 )" + "\n");
            strSqlString.Append("             GROUP BY MAT_ID" + "\n");

            strSqlString.Append("          UNION ALL" + "\n");
            strSqlString.Append("          SELECT B.GUBUN, A.MAT_ID" + "\n");
            strSqlString.Append("              , ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0), 'Merge', NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0)" + "\n");
            strSqlString.Append("                 END),0) )  ) DA_WIP_TTL" + "\n");
            strSqlString.Append("              , ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0), 'Merge', NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) WB_WIP_TTL" + "\n");

            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN " + "\n");
            strSqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 0)" + "\n");
            strSqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)  ELSE 0 END" + "\n");
            strSqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)" + "\n");
            strSqlString.Append("                       END), 0)  ) ) AS DA_1" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1+GATE_1,0), 'Merge', NVL(WB_1+GATE_1,0), 0)" + "\n");
            strSqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_1+GATE_1,0) ELSE 0 END" + "\n");
            strSqlString.Append("                         ELSE NVL(WB_1+GATE_1,0)" + "\n");
            strSqlString.Append("                       END),0)   ) ) AS WB_1" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_2,0), 'Merge', NVL(DA_2,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_2,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_2,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) AS DA_2" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_2+GATE_2,0), 'Merge', NVL(WB_2+GATE_2,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_2+GATE_2,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_2+GATE_2,0)" + "\n");
            strSqlString.Append("                 END),0)   )  ) AS WB_2" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_3,0), 'Merge', NVL(DA_3,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_3,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_3,0)" + "\n");
            strSqlString.Append("                 END),0)  )  ) AS DA_3" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_3+GATE_3,0), 'Merge', NVL(WB_3+GATE_3,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_3+GATE_3,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_3+GATE_3,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS WB_3" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_4,0), 'Merge', NVL(DA_4,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_4,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_4,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS DA_4" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_4+GATE_4,0), 'Merge', NVL(WB_4+GATE_4,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_4+GATE_4,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_4+GATE_4,0)" + "\n");
            strSqlString.Append("                 END),0)  ) ) AS WB_4" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_5,0), 'Merge', NVL(DA_5,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_5,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_5,0)" + "\n");
            strSqlString.Append("                 END),0)   )) AS DA_5" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_5+GATE_5,0), 'Merge', NVL(WB_5+GATE_5,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_5+GATE_5,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_5+GATE_5,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS WB_5" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_ETC,0), 'Merge', NVL(DA_ETC,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_ETC,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)  ) ) AS DA_ETC" + "\n");
            
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_ETC+GATE_ETC,0), 'Merge', NVL(WB_ETC+GATE_ETC,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_ETC+GATE_ETC,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_ETC+GATE_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) AS WB_ETC" + "\n");
            strSqlString.Append("           FROM MWIPMATDEF A," + "\n");
            strSqlString.Append("              (  SELECT '6_WIP_MERGE' GUBUN, LOT.MAT_ID " + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0250', 'A0305', 'A0306', 'A0310', 'A0400', 'A0401', 'A0500', 'A0501', 'A0530', 'A0531' ) THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0402', 'A0502', 'A0532') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_2" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0403', 'A0503', 'A0533') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_3" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0404', 'A0504', 'A0534') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_4" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0405', 'A0505', 'A0535') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_5" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ( 'A0406', 'A0506', 'A0536','A0407', 'A0507', 'A0537','A0408', 'A0508', 'A0538','A0409', 'A0509', 'A0539' ) THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_ETC  " + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0550', 'A0551', 'A0600','A0601' ) THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0552', 'A0602') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_2" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0553', 'A0603') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_3" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0554', 'A0604') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_4" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0555', 'A0605') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_5" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A0606', 'A0556',  'A0607', 'A0557',  'A0608', 'A0558',  'A0609', 'A0559' ) THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_ETC" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0800', 'A0801') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0802') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_2" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0803') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_3" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0804') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_4" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0805') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_5" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0806','A0807','A0808','A0809') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_ETC" + "\n");
            strSqlString.Append("                FROM ( SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1" + "\n");
            strSqlString.Append("                      , SUM(A.QTY_1) QTY" + "\n");
            strSqlString.Append("                  FROM RWIPLOTSTS A, MWIPOPRDEF B " + "\n");
            strSqlString.Append("                   WHERE 1 = 1 " + "\n");
            strSqlString.Append("                    AND A.FACTORY = B.FACTORY(+)" + "\n");
            strSqlString.Append("                    AND A.OPER = B.OPER(+)" + "\n");
            strSqlString.Append("                    AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                    AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                    AND A.LOT_CMF_5 LIKE 'P%' " + "\n");
            strSqlString.Append("                    AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                   GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT " + "\n");
            strSqlString.Append("               WHERE 1 = 1" + "\n");
            strSqlString.Append("                 AND LOT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("                 AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                 AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-'" + "\n");
            strSqlString.Append("                 AND MAT.MAT_TYPE = 'FG' AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            strSqlString.Append("                 AND MAT.MAT_GRP_2 <> '-'" + "\n");
            strSqlString.Append("               GROUP BY LOT.MAT_ID" + "\n");

            strSqlString.Append("      ) B," + "\n");
            strSqlString.Append("               ( SELECT KEY_1,DATA_1" + "\n");
            strSqlString.Append("                FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("               WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                 AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')" + "\n");
            strSqlString.Append("                ) G" + "\n");
            strSqlString.Append("           WHERE B.MAT_ID = A.MAT_ID(+)" + "\n");

            strSqlString.Append("             AND B.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            strSqlString.Append("             AND B.MAT_ID = G.KEY_1(+)" + "\n");
            strSqlString.Append("             AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("            GROUP BY B.GUBUN, A.MAT_ID" + "\n");

            strSqlString.Append("  UNION ALL SELECT B.GUBUN, A.MAT_ID" + "\n");
            strSqlString.Append("              , ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0), 'Merge', NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0)" + "\n");
            strSqlString.Append("                 END),0) )  ) DA_WIP_TTL" + "\n");
            strSqlString.Append("              , ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0), 'Merge', NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) WB_WIP_TTL" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN " + "\n");
            strSqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 0)" + "\n");
            strSqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)" + "\n");
            strSqlString.Append("                       END), 0)  ) ) AS DA_1" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1+GATE_1,0), 'Merge', NVL(WB_1+GATE_1,0), 0)" + "\n");
            strSqlString.Append("                         ELSE NVL(WB_1+GATE_1,0) " + "\n");
            strSqlString.Append("                       END),0)   ) ) AS WB_1" + "\n");

            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_2,0), 'Merge', NVL(DA_2,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_2,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) AS DA_2" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_2+GATE_2,0), 'Merge', NVL(WB_2+GATE_2,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_2+GATE_2,0)" + "\n");
            strSqlString.Append("                 END),0)   )  ) AS WB_2 " + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_3,0), 'Merge', NVL(DA_3,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_3,0)" + "\n");
            strSqlString.Append("                 END),0)  )  ) AS DA_3" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_3+GATE_3,0), 'Merge', NVL(WB_3+GATE_3,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_3+GATE_3,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS WB_3" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_4,0), 'Merge', NVL(DA_4,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_4,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS DA_4" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_4+GATE_4,0), 'Merge', NVL(WB_4+GATE_4,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_4+GATE_4,0)" + "\n");
            strSqlString.Append("                 END),0)  ) ) AS WB_4" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_5,0), 'Merge', NVL(DA_5,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_5,0)" + "\n");
            strSqlString.Append("                 END),0)   )) AS DA_5" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_5+GATE_5,0), 'Merge', NVL(WB_5+GATE_5,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_5+GATE_5,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS WB_5 " + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_ETC,0), 'Merge', NVL(DA_ETC,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)  ) ) AS DA_ETC" + "\n");

            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_ETC+GATE_ETC,0), 'Merge', NVL(WB_ETC+GATE_ETC,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_ETC+GATE_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) AS WB_ETC" + "\n");
            strSqlString.Append("           FROM MWIPMATDEF A," + "\n");
            strSqlString.Append("              (SELECT  '5_WIP_CHIP' GUBUN, LOT.MAT_ID" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1,  0 DA_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0402', 'A0502', 'A0532') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                 ELSE 0  END, 0) ) DA_2" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0403', 'A0503', 'A0533') AND  MAT.MAT_GRP_5 = '3rd' THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) DA_3" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0404', 'A0504', 'A0534') AND  MAT.MAT_GRP_5 = '4th'  THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) DA_4" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0405', 'A0505', 'A0535') AND  MAT.MAT_GRP_5 = '5th' THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) DA_5" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ( 'A0406', 'A0506', 'A0536','A0407', 'A0507', 'A0537','A0408', 'A0508', 'A0538','A0409', 'A0509', 'A0539' ) AND MAT.MAT_GRP_5 IN ( '6th',  '7th', '8th', '9th') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                ELSE 0 END, 0) ) DA_ETC" + "\n");
            strSqlString.Append("                  , 0 WB_1, 0 WB_2, 0 WB_3, 0 WB_4, 0 WB_5, 0 WB_ETC, 0 GATE_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0802') AND  MAT.MAT_GRP_5 = '2nd'THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                 ELSE 0 END, 0) ) GATE_2" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0803') AND  MAT.MAT_GRP_5 = '3rd'THEN" + "\n");
            strSqlString.Append("                                     DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) GATE_3" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0804') AND  MAT.MAT_GRP_5 = '4th' THEN" + "\n");
            strSqlString.Append("                                     DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) GATE_4" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0805') AND  MAT.MAT_GRP_5 = '5th'THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) GATE_5" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0806','A0807','A0808','A0809') AND MAT.MAT_GRP_5 IN ( '6th',  '7th', '8th', '9th')  THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) GATE_ETC" + "\n");
            strSqlString.Append("               FROM (   SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1" + "\n");
            strSqlString.Append("                    , SUM(A.QTY_1) QTY " + "\n");
            strSqlString.Append("                   FROM RWIPLOTSTS A, MWIPOPRDEF B " + "\n");
            strSqlString.Append("                 WHERE 1 = 1" + "\n");
            strSqlString.Append("                  AND A.FACTORY = B.FACTORY(+) " + "\n");
            strSqlString.Append("                  AND A.OPER = B.OPER(+)" + "\n");
            strSqlString.Append("                  AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                  AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                  AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
            strSqlString.Append("                  AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                 GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT" + "\n");
            strSqlString.Append("              WHERE 1 = 1" + "\n");
            strSqlString.Append("               AND LOT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("               AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("               AND MAT.MAT_TYPE = 'FG' AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU')" + "\n");
            strSqlString.Append("               AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
            strSqlString.Append("               AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            strSqlString.Append("               AND MAT.MAT_GRP_2 <> '-'" + "\n");
            strSqlString.Append("              GROUP BY  LOT.MAT_ID ) B," + "\n");
            strSqlString.Append("               ( SELECT KEY_1,DATA_1" + "\n");
            strSqlString.Append("                FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("               WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                 AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')" + "\n");
            strSqlString.Append("                ) G   " + "\n");
            strSqlString.Append("           WHERE B.MAT_ID = A.MAT_ID(+) " + "\n");

            strSqlString.Append("             AND B.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            strSqlString.Append("             AND B.MAT_ID = G.KEY_1(+)" + "\n");
            strSqlString.Append("             AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("            GROUP BY B.GUBUN, A.MAT_ID" + "\n");

            strSqlString.Append("            UNION ALL" + "\n");
            strSqlString.Append("            SELECT '7_실적' GUBUN, MAT_ID," + "\n");
            strSqlString.Append("              SUM( CASE WHEN OPER LIKE 'A040%' THEN  QTY  ELSE 0  END ) DA_RST_TTL ,  SUM( CASE WHEN OPER LIKE 'A060%' THEN  QTY  ELSE 0  END ) WB_RST_TTL," + "\n");
            strSqlString.Append("              SUM( CASE WHEN OPER IN ( 'A0400', 'A0401') THEN QTY  ELSE 0 END ) DA401 , SUM( CASE WHEN OPER IN ( 'A0600', 'A0601') THEN QTY ELSE 0 END )  WB601," + "\n");
            strSqlString.Append("              SUM(DECODE(OPER, 'A0402', QTY,  0)) DA402  , SUM(DECODE(OPER, 'A0602', QTY,  0)) WB602," + "\n");
            strSqlString.Append("              SUM(DECODE(OPER, 'A0403', QTY,  0)) DA403  , SUM(DECODE(OPER, 'A0603', QTY,  0)) WB603," + "\n");
            strSqlString.Append("              SUM(DECODE(OPER, 'A0404', QTY,  0)) DA404  , SUM(DECODE(OPER, 'A0604', QTY,  0)) WB604," + "\n");
            strSqlString.Append("              SUM(DECODE(OPER, 'A0405', QTY,  0)) DA405  , SUM(DECODE(OPER, 'A0605', QTY,  0)) WB605," + "\n");
            strSqlString.Append("              SUM( CASE WHEN OPER IN ( 'A0406',   'A0407',  'A0408',  'A0409') THEN QTY  ELSE 0 END ) DA_Etc," + "\n");
            strSqlString.Append("              SUM( CASE WHEN OPER IN ( 'A0606',   'A0607',  'A0608',  'A0609') THEN  QTY ELSE 0 END ) WB_Etc" + "\n");
            strSqlString.Append("             FROM (  SELECT MAT_ID, OPER, SUM(S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ) QTY" + "\n");
            strSqlString.Append("                 FROM RSUMWIPMOV A" + "\n");
            strSqlString.Append("              WHERE 1=1" + "\n");
            strSqlString.Append("               AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("               AND A.MAT_VER  = 1 " + "\n");
            strSqlString.Append("               AND A.WORK_DATE = '" + selectDate[selectDate.Count - 1] + "'" + "\n");
            strSqlString.Append("               AND A.LOT_TYPE  = 'W'" + "\n");

            strSqlString.Append("               AND A.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            strSqlString.Append("               AND ( A.OPER LIKE  'A040%' OR  A.OPER LIKE  'A060%'  )" + "\n");
            strSqlString.Append("               AND A.CM_KEY_3 LIKE 'P%'" + "\n");
            strSqlString.Append("              GROUP BY A.MAT_ID, A.OPER  )" + "\n");
            strSqlString.Append("            GROUP BY MAT_ID" + "\n");
            strSqlString.Append("           UNION ALL" + "\n");
            strSqlString.Append("           SELECT '8_예상실적' GUBUN, MAT_ID," + "\n");
            strSqlString.Append("               ROUND( (DA_RST_TTL / NOW_MM * 60*24) )  DA_RST_TTL, ROUND( ( WB_RST_TTL/ NOW_MM * 60*24)) WB_RST_TTL," + "\n");
            strSqlString.Append("               ROUND((DA401 /NOW_MM * 60 * 24)) DA401, ROUND((WB601 /NOW_MM * 60 * 24)) WB601," + "\n");
            strSqlString.Append("               ROUND((DA402 /NOW_MM * 60 * 24)) DA402, ROUND((WB602 /NOW_MM * 60 * 24)) WB602," + "\n");
            strSqlString.Append("               ROUND((DA403 /NOW_MM * 60 * 24)) DA403, ROUND((WB603 /NOW_MM * 60 * 24)) WB603," + "\n");
            strSqlString.Append("               ROUND((DA404 /NOW_MM * 60 * 24)) DA404, ROUND((WB604 /NOW_MM * 60 * 24)) WB604," + "\n");
            strSqlString.Append("               ROUND((DA405 /NOW_MM * 60 * 24)) DA405, ROUND((WB605 /NOW_MM * 60 * 24)) WB605," + "\n");
            strSqlString.Append("               ROUND((DA_Etc/NOW_MM * 60 * 24)) DA_Etc, ROUND((WB_Etc/NOW_MM * 60 * 24)) WB_Etc" + "\n");
            strSqlString.Append("           FROM (  SELECT MAT_ID," + "\n");
            strSqlString.Append("                 SUM( CASE WHEN OPER LIKE 'A040%' THEN  QTY  ELSE 0  END ) DA_RST_TTL ,  SUM( CASE WHEN OPER LIKE 'A060%' THEN  QTY  ELSE 0  END ) WB_RST_TTL," + "\n");
            strSqlString.Append("                 SUM( CASE WHEN OPER IN ( 'A0400', 'A0401') THEN QTY  ELSE 0 END ) DA401 , SUM( CASE WHEN OPER IN ( 'A0600', 'A0601') THEN QTY ELSE 0 END )  WB601," + "\n");
            strSqlString.Append("                 SUM(DECODE(OPER, 'A0402', QTY,  0)) DA402  , SUM(DECODE(OPER, 'A0602', QTY,  0)) WB602," + "\n");
            strSqlString.Append("                 SUM(DECODE(OPER, 'A0403', QTY,  0)) DA403  , SUM(DECODE(OPER, 'A0603', QTY,  0)) WB603," + "\n");
            strSqlString.Append("                 SUM(DECODE(OPER, 'A0404', QTY,  0)) DA404  , SUM(DECODE(OPER, 'A0604', QTY,  0)) WB604," + "\n");
            strSqlString.Append("                 SUM(DECODE(OPER, 'A0405', QTY,  0)) DA405  , SUM(DECODE(OPER, 'A0605', QTY,  0)) WB605," + "\n");
            strSqlString.Append("                 SUM( CASE WHEN OPER IN ( 'A0406',   'A0407',  'A0408',  'A0409') THEN QTY  ELSE 0 END ) DA_Etc," + "\n");

            if (Check_Day.Substring(6).Equals("01"))
                strSqlString.Append("                 SUM( CASE WHEN OPER IN ( 'A0606',   'A0607',  'A0608',  'A0609') THEN  QTY ELSE 0 END ) WB_Etc,  (SYSDATE - TO_DATE('" + Last_Month_Last_day + "220000', 'YYYYMMDDHH24MISS') ) * 24 * 60 NOW_MM " + "\n");
            else strSqlString.Append("                 SUM( CASE WHEN OPER IN ( 'A0606',   'A0607',  'A0608',  'A0609') THEN  QTY ELSE 0 END ) WB_Etc,  (SYSDATE - TO_DATE('" + selectDate[selectDate.Count - 2] + "220000', 'YYYYMMDDHH24MISS') ) * 24 * 60 NOW_MM " + "\n");

            strSqlString.Append("                FROM (  SELECT MAT_ID, OPER, SUM(S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ) QTY" + "\n");
            strSqlString.Append("                    FROM RSUMWIPMOV A  " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                  AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                  AND A.MAT_VER  = 1" + "\n");
            strSqlString.Append("                  AND A.WORK_DATE = '" + selectDate[selectDate.Count - 1] + "'" + "\n");
            strSqlString.Append("                  AND A.LOT_TYPE  = 'W'" + "\n");

            strSqlString.Append("                  AND A.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            strSqlString.Append("                  AND ( A.OPER LIKE  'A040%' OR  A.OPER LIKE  'A060%'  )" + "\n");
            strSqlString.Append("                  AND A.CM_KEY_3 LIKE 'P%'" + "\n");
            strSqlString.Append("                 GROUP BY A.MAT_ID, A.OPER  )" + "\n");
            strSqlString.Append("               GROUP BY MAT_ID  ) ) A,  (SELECT * FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  AND MAT_TYPE = 'FG' AND MAT_VER =  1 AND  DELETE_FLAG <> 'Y' AND MAT_GRP_2 <> '-' ) B" + "\n");
            strSqlString.Append("            WHERE A.MAT_ID = B.MAT_ID(+)" + "\n"); 
                
            strSqlString.Append("            ) A," + "\n");
            
            strSqlString.Append("           ( SELECT SEQ FROM HRTDSUMSEQ@RPTTOMES WHERE SEQ < 9 ) C," + "\n");

            strSqlString.Append("           (  SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER LIKE 'A040%' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0) ELSE 0 END) DA_TTL," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER LIKE 'A060%' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) WB_TTL," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER IN ('A0400', 'A0401') THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA1," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0402' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA2," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0403' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA3," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0404' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA4," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0405' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA5," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER IN ('A0600', 'A0601') THEN NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) WB1," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0602' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0) ELSE 0 END) WB2," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0603' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0) ELSE 0 END) WB3," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0604' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) WB4," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0605' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) WB5," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER IN ('A0406', 'A0407', 'A0408', 'A0409') THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA_ETC," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER IN ('A0606', 'A0607', 'A0608', 'A0609') THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) WB_ETC " + "\n");
            strSqlString.Append("                 FROM RSUMWIPMOV A," + "\n");
            strSqlString.Append("                      (SELECT *  FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  AND MAT_TYPE = 'FG' AND MAT_VER =  1 AND  DELETE_FLAG <> 'Y' AND MAT_GRP_2 <> '-' ) MAT" + "\n");
            strSqlString.Append("               WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                    AND A.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                    AND A.CM_KEY_2 = 'PROD'" + "\n");
            strSqlString.Append("                    AND A.MAT_VER = 1 " + "\n");
            strSqlString.Append("                    AND ( A.OPER LIKE 'A040%'  OR  A.OPER LIKE 'A060%' )" + "\n");
            strSqlString.Append("                    AND A.WORK_DATE = '" + selectDate[selectDate.Count - 1] + "' AND A.MAT_ID = MAT.MAT_ID(+)" + "\n");
            strSqlString.Append("              GROUP BY MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11   )  RCV" + "\n");
            strSqlString.Append("     WHERE A.MAT_GRP_1 = RCV.MAT_GRP_1(+)" + "\n");
            strSqlString.Append("       AND A.MAT_GRP_9 = RCV.MAT_GRP_9(+)" + "\n");
            strSqlString.Append("       AND A.MAT_GRP_10 =RCV.MAT_GRP_10(+)" + "\n");
            strSqlString.Append("       AND A.MAT_GRP_6=RCV.MAT_GRP_6(+)" + "\n");
            strSqlString.Append("       AND A.MAT_CMF_11=RCV.MAT_CMF_11(+)" + "\n");

            //상세조회
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);
            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);
            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);
            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);
            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);
            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);
            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);


            strSqlString.Append("     GROUP BY " + QueryCond2 + "\n");
            strSqlString.Append("              , DECODE(SEQ, 1, 'SOP 잔량', 2, '일목표', 3, '소요대수', 4, 'CAPA', 5, 'CHIP', 6, 'MERGE', 7, '실적', '예상실적'), SEQ" + "\n");

          //  strSqlString.Append("/* ------------------------------------------------------ " + "\n");
          //  strSqlString.Append("     CUSTOMER TOTAL  Raw Data  " + "\n");
          //  strSqlString.Append(" ------------------------------------------------------*/ " + "\n");

            strSqlString.Append("   UNION ALL SELECT A.MAT_GRP_1 CUSTOMER,  'ZZ'  FAMILY, 'CUSTOM TOTAL' PKG, 'CUSTOM TOTAL'  LD_COUNT,  'CUSTOM TOTAL'  PKG_CODE," + "\n");
            strSqlString.Append("               DECODE(SEQ, 1, 'SOP 잔량', 2, '일목표', 3, '소요대수', 4, 'CAPA', 5, 'CHIP', 6, 'MERGE', 7, '실적', '예상실적') GUBUN, SEQ GUBUN_SEQ," + "\n");

            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA_TTL - RCV.DA_TTL, A.DA_TTL), 0 ) ) DA_TTL," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB_TTL - RCV.WB_TTL, A.WB_TTL), 0 ) ) WB_TTL," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA1 - RCV.DA1, A.DA1), 0 ) ) DA1," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA2 - RCV.DA2, A.DA2), 0 ) ) DA2," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA3 - RCV.DA3, A.DA3), 0 ) ) DA3," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA4 - RCV.DA4, A.DA4), 0 ) ) DA4," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA5 - RCV.DA5, A.DA5), 0 ) ) DA5," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB1 - RCV.WB1, A.WB1), 0 ) ) WB1," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB2 - RCV.WB2, A.WB2), 0 ) ) WB2," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB3 - RCV.WB3, A.WB3), 0 ) ) WB3," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB4 - RCV.WB4, A.WB4), 0 ) ) WB4," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB5 - RCV.WB5, A.WB5), 0 ) ) WB5," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA_ETC - RCV.DA_ETC, A.DA_ETC), 0 ) ) DA_ETC," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB_ETC - RCV.WB_ETC, A.WB_ETC), 0 ) ) WB_ETC" + "\n");

            strSqlString.Append("        FROM (" + "\n");

            strSqlString.Append("       SELECT MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, GUBUN" + "\n");
            strSqlString.Append("           , SUM(DA1) +SUM(DA2) +SUM(DA3) + SUM(DA4) + SUM(DA5) DA_TTL," + "\n");
            strSqlString.Append("           SUM(WB1) +SUM(WB2) +SUM(WB3) +SUM(WB4) +SUM(WB5) WB_TTL," + "\n");
            strSqlString.Append("           SUM(DA1) DA1,SUM(WB1) WB1," + "\n");
            strSqlString.Append("           SUM(DA2) DA2,SUM(WB2) WB2," + "\n");
            strSqlString.Append("           SUM(DA3) DA3,SUM(WB3) WB3," + "\n");
            strSqlString.Append("           SUM(DA4) DA4,SUM(WB4) WB4," + "\n");
            strSqlString.Append("           SUM(DA5) DA5,SUM(WB5) WB5," + "\n");
            strSqlString.Append("           SUM(DA6) + SUM(DA7) + SUM(DA8) + SUM(DA9) DA_ETC," + "\n");
            strSqlString.Append("           SUM(WB6) +SUM(WB7) +SUM(WB8) +SUM(WB9) WB_ETC" + "\n");
            strSqlString.Append("  FROM  (" + "\n");

            strSqlString.Append("           SELECT MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, MAT_KEY,'1_SOP_잔량'  GUBUN," + "\n");
            strSqlString.Append("                   DECODE(MAX_DA1,0,MIN_DA1,MAX_DA1) DA1,  DECODE(MAX_DA6,0,MIN_DA6,MAX_DA6) DA6" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA2,0,MIN_DA2,MAX_DA2) DA2, DECODE(MAX_DA7,0,MIN_DA7,MAX_DA7) DA7" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA3,0,MIN_DA3,MAX_DA3) DA3, DECODE(MAX_DA8,0,MIN_DA8,MAX_DA8) DA8" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA4,0,MIN_DA4,MAX_DA4) DA4, DECODE(MAX_DA9,0,MIN_DA9,MAX_DA9) DA9" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA5,0,MIN_DA5,MAX_DA5) DA5" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB1,0,MIN_WB1,MAX_WB1) WB1, DECODE(MAX_WB6,0,MIN_WB6,MAX_WB6) WB6" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB2,0,MIN_WB2,MAX_WB2) WB2, DECODE(MAX_WB7,0,MIN_WB7,MAX_WB7) WB7" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB3,0,MIN_WB3,MAX_WB3) WB3, DECODE(MAX_WB8,0,MIN_WB8,MAX_WB8) WB8" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB4,0,MIN_WB4,MAX_WB4) WB4, DECODE(MAX_WB9,0,MIN_WB9,MAX_WB9) WB9" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB5,0,MIN_WB5,MAX_WB5) WB5" + "\n");
            strSqlString.Append("             FROM (" + "\n");
            strSqlString.Append("                     SELECT CUSTOMER MAT_GRP_1, FAMILY MAT_GRP_2, PKG MAT_GRP_3, TYPE1 MAT_GRP_4, TYPE2 MAT_GRP_5, LD_COUNT MAT_GRP_6, DENSITY MAT_GRP_7, GENERATION MAT_GRP_8, MAJOR_CODE MAT_GRP_9, PKG2 MAT_GRP_10, PKG_CODE MAT_CMF_11, MAT_KEY," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA1), 'HX', MAX(DA1), SUM(DA1) ) MAX_DA1,DECODE(CUSTOMER, 'SE', MIN(DA1), 'HX', MIN(DA1),0) MIN_DA1," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA2), 'HX', MAX(DA2), SUM(DA2) ) MAX_DA2,DECODE(CUSTOMER, 'SE', MIN(DA2), 'HX', MIN(DA2),0) MIN_DA2," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA3), 'HX', MAX(DA3), SUM(DA3) ) MAX_DA3,DECODE(CUSTOMER, 'SE', MIN(DA3), 'HX', MIN(DA3),0) MIN_DA3," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA4), 'HX', MAX(DA4), SUM(DA4) ) MAX_DA4,DECODE(CUSTOMER, 'SE', MIN(DA4), 'HX', MIN(DA4),0) MIN_DA4," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA5), 'HX', MAX(DA5), SUM(DA5) ) MAX_DA5,DECODE(CUSTOMER, 'SE', MIN(DA5), 'HX', MIN(DA5),0) MIN_DA5," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA6), 'HX', MAX(DA6), SUM(DA6) ) MAX_DA6,DECODE(CUSTOMER, 'SE', MIN(DA6), 'HX', MIN(DA6),0) MIN_DA6," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA7), 'HX', MAX(DA7), SUM(DA7) ) MAX_DA7,DECODE(CUSTOMER, 'SE', MIN(DA7), 'HX', MIN(DA7),0) MIN_DA7," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA8), 'HX', MAX(DA8), SUM(DA8) ) MAX_DA8,DECODE(CUSTOMER, 'SE', MIN(DA8), 'HX', MIN(DA8),0) MIN_DA8," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA9), 'HX', MAX(DA9), SUM(DA9) ) MAX_DA9,DECODE(CUSTOMER, 'SE', MIN(DA9), 'HX', MIN(DA9),0) MIN_DA9," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB1), 'HX', MAX(WB1), SUM(WB1) ) MAX_WB1,DECODE(CUSTOMER, 'SE', MIN(WB1), 'HX', MIN(WB1),0) MIN_WB1," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB2), 'HX', MAX(WB2), SUM(WB2) ) MAX_WB2,DECODE(CUSTOMER, 'SE', MIN(WB2), 'HX', MIN(WB2),0) MIN_WB2," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB3), 'HX', MAX(WB3), SUM(WB3) ) MAX_WB3,DECODE(CUSTOMER, 'SE', MIN(WB3), 'HX', MIN(WB3),0) MIN_WB3," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB4), 'HX', MAX(WB4), SUM(WB4) ) MAX_WB4,DECODE(CUSTOMER, 'SE', MIN(WB4), 'HX', MIN(WB4),0) MIN_WB4," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB5), 'HX', MAX(WB5), SUM(WB5) ) MAX_WB5,DECODE(CUSTOMER, 'SE', MIN(WB5), 'HX', MIN(WB5),0) MIN_WB5," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB6), 'HX', MAX(WB6), SUM(WB6) ) MAX_WB6,DECODE(CUSTOMER, 'SE', MIN(WB6), 'HX', MIN(WB6),0) MIN_WB6," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB7), 'HX', MAX(WB7), SUM(WB7) ) MAX_WB7,DECODE(CUSTOMER, 'SE', MIN(WB7), 'HX', MIN(WB7),0) MIN_WB7," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB8), 'HX', MAX(WB8), SUM(WB8) ) MAX_WB8,DECODE(CUSTOMER, 'SE', MIN(WB8), 'HX', MIN(WB8),0) MIN_WB8," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB9), 'HX', MAX(WB9), SUM(WB9) ) MAX_WB9, DECODE(CUSTOMER, 'SE', MIN(WB9), 'HX', MIN(WB9),0) MIN_WB9" + "\n");
            strSqlString.Append("                       FROM RSUMOPRREM" + "\n");
            strSqlString.Append("                      WHERE WORK_DATE = '" + selectDate[selectDate.Count - 1] + "'" + "\n");
            strSqlString.Append("                        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                        AND RESV_FIELD_1 = ' '" + "\n");
            strSqlString.Append("                        AND MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
            strSqlString.Append("                      GROUP BY CUSTOMER , FAMILY , PKG , TYPE1 , TYPE2 , LD_COUNT , DENSITY , GENERATION , MAJOR_CODE , PKG2 , PKG_CODE  , MAT_KEY ) )" + "\n");
            strSqlString.Append("               GROUP BY MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, GUBUN" + "\n");
            strSqlString.Append("       UNION ALL" + "\n");
            strSqlString.Append("       SELECT MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, GUBUN" + "\n");
            strSqlString.Append("              , SUM(DA1) +SUM(DA2) +SUM(DA3) + SUM(DA4) + SUM(DA5) DA_TTL," + "\n");
            strSqlString.Append("              SUM(WB1) +SUM(WB2) +SUM(WB3) +SUM(WB4) +SUM(WB5) WB_TTL," + "\n");
            strSqlString.Append("              SUM(DA1) DA1,SUM(WB1) WB1," + "\n");
            strSqlString.Append("              SUM(DA2) DA2,SUM(WB2) WB2," + "\n");
            strSqlString.Append("              SUM(DA3) DA3,SUM(WB3) WB3," + "\n");
            strSqlString.Append("              SUM(DA4) DA4,SUM(WB4) WB4," + "\n");
            strSqlString.Append("              SUM(DA5) DA5,SUM(WB5) WB5," + "\n");
            strSqlString.Append("              SUM(DA6) + SUM(DA7) + SUM(DA8) + SUM(DA9) DA_ETC," + "\n");
            strSqlString.Append("              SUM(WB6) +SUM(WB7) +SUM(WB8) +SUM(WB9) WB_ETC" + "\n");
            strSqlString.Append("  FROM  (" + "\n");
            strSqlString.Append("           SELECT MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, MAT_KEY,'2_목표'  GUBUN," + "\n");
            strSqlString.Append("                   DECODE(MAX_DA1,0,MIN_DA1,MAX_DA1) DA1,  DECODE(MAX_DA6,0,MIN_DA6,MAX_DA6) DA6" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA2,0,MIN_DA2,MAX_DA2) DA2, DECODE(MAX_DA7,0,MIN_DA7,MAX_DA7) DA7" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA3,0,MIN_DA3,MAX_DA3) DA3, DECODE(MAX_DA8,0,MIN_DA8,MAX_DA8) DA8" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA4,0,MIN_DA4,MAX_DA4) DA4, DECODE(MAX_DA9,0,MIN_DA9,MAX_DA9) DA9" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA5,0,MIN_DA5,MAX_DA5) DA5" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB1,0,MIN_WB1,MAX_WB1) WB1, DECODE(MAX_WB6,0,MIN_WB6,MAX_WB6) WB6" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB2,0,MIN_WB2,MAX_WB2) WB2, DECODE(MAX_WB7,0,MIN_WB7,MAX_WB7) WB7" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB3,0,MIN_WB3,MAX_WB3) WB3, DECODE(MAX_WB8,0,MIN_WB8,MAX_WB8) WB8" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB4,0,MIN_WB4,MAX_WB4) WB4, DECODE(MAX_WB9,0,MIN_WB9,MAX_WB9) WB9" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB5,0,MIN_WB5,MAX_WB5) WB5" + "\n");
            strSqlString.Append("             FROM (" + "\n");
            strSqlString.Append("                     SELECT CUSTOMER MAT_GRP_1, FAMILY MAT_GRP_2, PKG MAT_GRP_3, TYPE1 MAT_GRP_4, TYPE2 MAT_GRP_5, LD_COUNT MAT_GRP_6, DENSITY MAT_GRP_7, GENERATION MAT_GRP_8, MAJOR_CODE MAT_GRP_9, PKG2 MAT_GRP_10, PKG_CODE MAT_CMF_11, MAT_KEY," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA1), 'HX', MAX(DA1), SUM(DA1) ) MAX_DA1,DECODE(CUSTOMER, 'SE', MIN(DA1), 'HX', MIN(DA1),0) MIN_DA1," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA2), 'HX', MAX(DA2), SUM(DA2) ) MAX_DA2,DECODE(CUSTOMER, 'SE', MIN(DA2), 'HX', MIN(DA2),0) MIN_DA2," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA3), 'HX', MAX(DA3), SUM(DA3) ) MAX_DA3,DECODE(CUSTOMER, 'SE', MIN(DA3), 'HX', MIN(DA3),0) MIN_DA3," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA4), 'HX', MAX(DA4), SUM(DA4) ) MAX_DA4,DECODE(CUSTOMER, 'SE', MIN(DA4), 'HX', MIN(DA4),0) MIN_DA4," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA5), 'HX', MAX(DA5), SUM(DA5) ) MAX_DA5,DECODE(CUSTOMER, 'SE', MIN(DA5), 'HX', MIN(DA5),0) MIN_DA5," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA6), 'HX', MAX(DA6), SUM(DA6) ) MAX_DA6,DECODE(CUSTOMER, 'SE', MIN(DA6), 'HX', MIN(DA6),0) MIN_DA6," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA7), 'HX', MAX(DA7), SUM(DA7) ) MAX_DA7,DECODE(CUSTOMER, 'SE', MIN(DA7), 'HX', MIN(DA7),0) MIN_DA7," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA8), 'HX', MAX(DA8), SUM(DA8) ) MAX_DA8,DECODE(CUSTOMER, 'SE', MIN(DA8), 'HX', MIN(DA8),0) MIN_DA8," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA9), 'HX', MAX(DA9), SUM(DA9) ) MAX_DA9,DECODE(CUSTOMER, 'SE', MIN(DA9), 'HX', MIN(DA9),0) MIN_DA9," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB1), 'HX', MAX(WB1), SUM(WB1) ) MAX_WB1,DECODE(CUSTOMER, 'SE', MIN(WB1), 'HX', MIN(WB1),0) MIN_WB1," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB2), 'HX', MAX(WB2), SUM(WB2) ) MAX_WB2,DECODE(CUSTOMER, 'SE', MIN(WB2), 'HX', MIN(WB2),0) MIN_WB2," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB3), 'HX', MAX(WB3), SUM(WB3) ) MAX_WB3,DECODE(CUSTOMER, 'SE', MIN(WB3), 'HX', MIN(WB3),0) MIN_WB3," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB4), 'HX', MAX(WB4), SUM(WB4) ) MAX_WB4,DECODE(CUSTOMER, 'SE', MIN(WB4), 'HX', MIN(WB4),0) MIN_WB4," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB5), 'HX', MAX(WB5), SUM(WB5) ) MAX_WB5,DECODE(CUSTOMER, 'SE', MIN(WB5), 'HX', MIN(WB5),0) MIN_WB5," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB6), 'HX', MAX(WB6), SUM(WB6) ) MAX_WB6,DECODE(CUSTOMER, 'SE', MIN(WB6), 'HX', MIN(WB6),0) MIN_WB6," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB7), 'HX', MAX(WB7), SUM(WB7) ) MAX_WB7,DECODE(CUSTOMER, 'SE', MIN(WB7), 'HX', MIN(WB7),0) MIN_WB7," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB8), 'HX', MAX(WB8), SUM(WB8) ) MAX_WB8,DECODE(CUSTOMER, 'SE', MIN(WB8), 'HX', MIN(WB8),0) MIN_WB8," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB9), 'HX', MAX(WB9), SUM(WB9) ) MAX_WB9, DECODE(CUSTOMER, 'SE', MIN(WB9), 'HX', MIN(WB9),0) MIN_WB9" + "\n");
            strSqlString.Append("                       FROM RSUMOPRREM" + "\n");
            strSqlString.Append("                      WHERE WORK_DATE = '" + selectDate[selectDate.Count - 1] + "'" + "\n");
            strSqlString.Append("                        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                        AND RESV_FIELD_1 = 'TARGET'" + "\n");
            strSqlString.Append("                        AND MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
            strSqlString.Append("                      GROUP BY  CUSTOMER , FAMILY , PKG , TYPE1 , TYPE2 , LD_COUNT , DENSITY , GENERATION , MAJOR_CODE , PKG2 , PKG_CODE  , MAT_KEY ) )" + "\n");
            strSqlString.Append("               GROUP BY MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, GUBUN" + "\n");

            strSqlString.Append("          UNION ALL" + "\n");
            strSqlString.Append("         SELECT B.MAT_GRP_1,  B.MAT_GRP_2,  B.MAT_GRP_3,  B.MAT_GRP_4,  B.MAT_GRP_5,  B.MAT_GRP_6,  B.MAT_GRP_7,  B.MAT_GRP_8,  B.MAT_GRP_9,  B.MAT_GRP_10,  B.MAT_CMF_11, GUBUN,  DA_TTL, WB_TTL, DA1, WB1, DA2, WB2, DA3, WB3, DA4, WB4, DA5, WB5, DA_ETC, WB_ETC" + "\n");
            
            strSqlString.Append("         FROM ( SELECT  '3_설비댓수' GUBUN, MAT_ID, " + "\n");
            strSqlString.Append("               SUM( CASE WHEN OPER LIKE 'A040%' THEN  RES_CNT  ELSE 0  END ) DA_TTL, SUM( CASE WHEN OPER LIKE 'A060%' THEN  RES_CNT  ELSE 0  END ) WB_TTL," + "\n");
            strSqlString.Append("               SUM( DECODE(OPER, 'A0400', RES_CNT, 'A0401', RES_CNT, 0) ) DA1, SUM( DECODE(OPER, 'A0600', RES_CNT, 'A0601', RES_CNT, 0)) WB1," + "\n");
            strSqlString.Append("               SUM( DECODE(OPER, 'A0402', RES_CNT, 0)) DA2, SUM( DECODE(OPER, 'A0602', RES_CNT, 0)) WB2," + "\n");
            strSqlString.Append("               SUM( DECODE(OPER, 'A0403', RES_CNT, 0)) DA3,SUM( DECODE(OPER, 'A0603', RES_CNT, 0)) WB3," + "\n");
            strSqlString.Append("               SUM( DECODE(OPER, 'A0404', RES_CNT, 0)) DA4, SUM( DECODE(OPER, 'A0604', RES_CNT, 0)) WB4," + "\n");
            strSqlString.Append("               SUM( DECODE(OPER, 'A0405', RES_CNT, 0)) DA5, SUM( DECODE(OPER, 'A0605', RES_CNT, 0)) WB5," + "\n");
            strSqlString.Append("               SUM( CASE WHEN OPER IN ( 'A0406',  'A0407', 'A0408', 'A0409' ) THEN  RES_CNT  ELSE 0 END ) DA_ETC," + "\n");
            strSqlString.Append("               SUM( CASE WHEN OPER IN ( 'A0606',  'A0607', 'A0608', 'A0609' ) THEN  RES_CNT  ELSE 0 END ) WB_ETC" + "\n");
            strSqlString.Append("            FROM (  SELECT FACTORY, RES_STS_2 MAT_ID, RES_GRP_6 RES_MODEL, RES_GRP_7 UPEH_GROUP, RES_STS_8 OPER , COUNT(RES_ID) RES_CNT" + "\n");
            strSqlString.Append("                  FROM  MRASRESDEF  " + "\n");
            strSqlString.Append("               WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("                AND DELETE_FLAG  = ' '  " + "\n");
            strSqlString.Append("                AND RES_TYPE  = 'EQUIPMENT' " + "\n");
            strSqlString.Append("                AND RES_STS_8 IN('A0400','A0401','A0402','A0403','A0404','A0405','A0600','A0601','A0602','A0603','A0604','A0605') " + "\n");
            strSqlString.Append("               GROUP BY FACTORY, RES_STS_2, RES_GRP_6, RES_GRP_7, RES_STS_8 )" + "\n");

            strSqlString.Append("            WHERE MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            strSqlString.Append("            GROUP BY  MAT_ID" + "\n");

            strSqlString.Append("  UNION ALL" + "\n");
            strSqlString.Append("            SELECT '4_CAPA' GUBUN, MAT_ID," + "\n");
            strSqlString.Append("                   SUM( CASE WHEN OPER LIKE 'A040%' THEN   RES_CNT * NVL(UPEH,0) * 24   ELSE 0  END ) RES_DA_TTL," + "\n");
            strSqlString.Append("                   SUM( CASE WHEN  OPER LIKE 'A060%' THEN   RES_CNT * NVL(UPEH,0) * 24  ELSE 0  END ) RES_WB_TTL," + "\n");
            strSqlString.Append("                   SUM( CASE WHEN OPER IN ( 'A0400',  'A0401' ) THEN  RES_CNT * NVL(UPEH,0) * 24 ELSE 0 END ) RES_CNT_DA1" + "\n");
            strSqlString.Append("                   , SUM( CASE WHEN OPER IN ( 'A0600',  'A0601' ) THEN  RES_CNT * NVL(UPEH,0) * 24 ELSE 0 END )  RES_CNT_WB1," + "\n");
            strSqlString.Append("                   SUM( DECODE( OPER, 'A0402',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_DA2, SUM( DECODE( OPER, 'A0602', RES_CNT *  NVL(UPEH,0) * 24, 0)) RES_CNT_WB2," + "\n");
            strSqlString.Append("                   SUM( DECODE( OPER, 'A0403',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_DA3, SUM( DECODE( OPER, 'A0603',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_WB3," + "\n");
            strSqlString.Append("                   SUM( DECODE( OPER, 'A0404',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_DA4, SUM( DECODE( OPER, 'A0604',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_WB4," + "\n");
            strSqlString.Append("                   SUM( DECODE( OPER, 'A0405',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_DA5, SUM( DECODE( OPER, 'A0605',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_WB5," + "\n");
            strSqlString.Append("                   SUM( CASE WHEN OPER IN ( 'A0406',  'A0407', 'A0408', 'A0409' ) THEN  RES_CNT * NVL(UPEH,0) * 24 ELSE 0 END ) DA_ETC," + "\n");
            strSqlString.Append("                   SUM( CASE WHEN OPER IN ( 'A0606',  'A0607', 'A0608', 'A0609' ) THEN  RES_CNT * NVL(UPEH,0) * 24 ELSE 0 END ) WB_ETC" + "\n");
            strSqlString.Append("              FROM (  SELECT RAS.FACTORY, RAS.RES_GRP_6 RES_MODEL, RAS.RES_STS_2 MAT_ID, RAS.RES_STS_8 OPER , COUNT(RES_ID) RES_CNT," + "\n");
            strSqlString.Append("                             MAX(DECODE(SUBSTR(RAS.RES_STS_8, 1, 3), 'A04', NVL(UPEH.UPEH, 0) * 0.75,  NVL(UPEH.UPEH, 0))  ) UPEH " + "\n");
            strSqlString.Append("                        FROM MRASRESDEF RAS, CRASUPHDEF UPEH" + "\n");
            strSqlString.Append("                       WHERE RAS.FACTORY   = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                         AND RAS.RES_CMF_9 = 'Y'" + "\n");
            strSqlString.Append("                         AND RAS.DELETE_FLAG  = ' '" + "\n");
            strSqlString.Append("                         AND RAS.RES_TYPE  = 'EQUIPMENT'" + "\n");
            strSqlString.Append("                         AND (RAS.RES_STS_8 LIKE 'A040%' OR RAS.RES_STS_8 LIKE 'A060%') " + "\n");
            strSqlString.Append("                         AND RAS.RES_STS_2 LIKE '" + txtSearchProduct.Text + "'" + "\n");
            strSqlString.Append("                         AND RAS.FACTORY = UPEH.FACTORY(+)" + "\n");
            strSqlString.Append("                         AND RAS.RES_GRP_6 = UPEH.RES_MODEL(+)" + "\n");
            strSqlString.Append("                         AND RAS.RES_STS_2 = UPEH.MAT_ID(+)" + "\n");
            strSqlString.Append("                         AND RAS.RES_STS_8 = UPEH.OPER(+)" + "\n");
            strSqlString.Append("                       GROUP BY RAS.FACTORY, RAS.RES_GRP_6, RAS.RES_STS_2, RAS.RES_STS_8 )" + "\n");
            strSqlString.Append("             GROUP BY MAT_ID" + "\n");

            strSqlString.Append("          UNION ALL" + "\n");
            strSqlString.Append("          SELECT B.GUBUN, A.MAT_ID" + "\n");
            strSqlString.Append("              , ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0), 'Merge', NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0)" + "\n");
            strSqlString.Append("                 END),0) )  ) DA_WIP_TTL" + "\n");
            strSqlString.Append("              , ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0), 'Merge', NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) WB_WIP_TTL" + "\n");

            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN " + "\n");
            strSqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 0)" + "\n");
            strSqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)  ELSE 0 END" + "\n");
            strSqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)" + "\n");
            strSqlString.Append("                       END), 0)  ) ) AS DA_1" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1+GATE_1,0), 'Merge', NVL(WB_1+GATE_1,0), 0)" + "\n");
            strSqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_1+GATE_1,0) ELSE 0 END" + "\n");
            strSqlString.Append("                         ELSE NVL(WB_1+GATE_1,0)" + "\n");
            strSqlString.Append("                       END),0)   ) ) AS WB_1" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_2,0), 'Merge', NVL(DA_2,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_2,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_2,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) AS DA_2" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_2+GATE_2,0), 'Merge', NVL(WB_2+GATE_2,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_2+GATE_2,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_2+GATE_2,0)" + "\n");
            strSqlString.Append("                 END),0)   )  ) AS WB_2" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_3,0), 'Merge', NVL(DA_3,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_3,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_3,0)" + "\n");
            strSqlString.Append("                 END),0)  )  ) AS DA_3" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_3+GATE_3,0), 'Merge', NVL(WB_3+GATE_3,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_3+GATE_3,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_3+GATE_3,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS WB_3" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_4,0), 'Merge', NVL(DA_4,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_4,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_4,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS DA_4" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_4+GATE_4,0), 'Merge', NVL(WB_4+GATE_4,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_4+GATE_4,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_4+GATE_4,0)" + "\n");
            strSqlString.Append("                 END),0)  ) ) AS WB_4" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_5,0), 'Merge', NVL(DA_5,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_5,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_5,0)" + "\n");
            strSqlString.Append("                 END),0)   )) AS DA_5" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_5+GATE_5,0), 'Merge', NVL(WB_5+GATE_5,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_5+GATE_5,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_5+GATE_5,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS WB_5" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_ETC,0), 'Merge', NVL(DA_ETC,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_ETC,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)  ) ) AS DA_ETC" + "\n");
            
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_ETC+GATE_ETC,0), 'Merge', NVL(WB_ETC+GATE_ETC,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_ETC+GATE_ETC,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_ETC+GATE_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) AS WB_ETC" + "\n");
            strSqlString.Append("           FROM MWIPMATDEF A," + "\n");
            strSqlString.Append("              (  SELECT '6_WIP_MERGE' GUBUN, LOT.MAT_ID " + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0250', 'A0305', 'A0306', 'A0310', 'A0400', 'A0401', 'A0500', 'A0501', 'A0530', 'A0531' ) THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0402', 'A0502', 'A0532') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_2" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0403', 'A0503', 'A0533') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_3" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0404', 'A0504', 'A0534') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_4" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0405', 'A0505', 'A0535') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_5" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ( 'A0406', 'A0506', 'A0536','A0407', 'A0507', 'A0537','A0408', 'A0508', 'A0538','A0409', 'A0509', 'A0539' ) THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_ETC  " + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0550', 'A0551', 'A0600','A0601' ) THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0552', 'A0602') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_2" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0553', 'A0603') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_3" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0554', 'A0604') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_4" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0555', 'A0605') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_5" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A0606', 'A0556',  'A0607', 'A0557',  'A0608', 'A0558',  'A0609', 'A0559' ) THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_ETC" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0800', 'A0801') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0802') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_2" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0803') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_3" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0804') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_4" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0805') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_5" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0806','A0807','A0808','A0809') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_ETC" + "\n");
            strSqlString.Append("                FROM ( SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1" + "\n");
            strSqlString.Append("                      , SUM(A.QTY_1) QTY" + "\n");
            strSqlString.Append("                  FROM RWIPLOTSTS A, MWIPOPRDEF B " + "\n");
            strSqlString.Append("                   WHERE 1 = 1 " + "\n");
            strSqlString.Append("                    AND A.FACTORY = B.FACTORY(+)" + "\n");
            strSqlString.Append("                    AND A.OPER = B.OPER(+)" + "\n");
            strSqlString.Append("                    AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                    AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                    AND A.LOT_CMF_5 LIKE 'P%' " + "\n");
            strSqlString.Append("                    AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                   GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT " + "\n");
            strSqlString.Append("               WHERE 1 = 1" + "\n");
            strSqlString.Append("                 AND LOT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("                 AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                 AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-'" + "\n");
            strSqlString.Append("                 AND MAT.MAT_TYPE = 'FG' AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            strSqlString.Append("                 AND MAT.MAT_GRP_2 <> '-'" + "\n");
            strSqlString.Append("               GROUP BY LOT.MAT_ID" + "\n");
            
            strSqlString.Append("              ) B," + "\n");
            strSqlString.Append("               ( SELECT KEY_1,DATA_1" + "\n");
            strSqlString.Append("                FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("               WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                 AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')" + "\n");
            strSqlString.Append("                ) G" + "\n");
            strSqlString.Append("           WHERE B.MAT_ID = A.MAT_ID(+)" + "\n");
            strSqlString.Append("            AND B.MAT_ID = G.KEY_1(+)" + "\n");

            strSqlString.Append("            AND B.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            strSqlString.Append("            AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("            GROUP BY B.GUBUN, A.MAT_ID" + "\n");

            strSqlString.Append("  UNION ALL SELECT B.GUBUN, A.MAT_ID" + "\n");
            strSqlString.Append("              , ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0), 'Merge', NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0)" + "\n");
            strSqlString.Append("                 END),0) )  ) DA_WIP_TTL" + "\n");
            strSqlString.Append("              , ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0), 'Merge', NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) WB_WIP_TTL" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN " + "\n");
            strSqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 0)" + "\n");
            strSqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)" + "\n");
            strSqlString.Append("                       END), 0)  ) ) AS DA_1" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1+GATE_1,0), 'Merge', NVL(WB_1+GATE_1,0), 0)" + "\n");
            strSqlString.Append("                         ELSE NVL(WB_1+GATE_1,0) " + "\n");
            strSqlString.Append("                       END),0)   ) ) AS WB_1" + "\n");

            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_2,0), 'Merge', NVL(DA_2,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_2,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) AS DA_2" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_2+GATE_2,0), 'Merge', NVL(WB_2+GATE_2,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_2+GATE_2,0)" + "\n");
            strSqlString.Append("                 END),0)   )  ) AS WB_2 " + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_3,0), 'Merge', NVL(DA_3,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_3,0)" + "\n");
            strSqlString.Append("                 END),0)  )  ) AS DA_3" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_3+GATE_3,0), 'Merge', NVL(WB_3+GATE_3,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_3+GATE_3,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS WB_3" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_4,0), 'Merge', NVL(DA_4,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_4,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS DA_4" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_4+GATE_4,0), 'Merge', NVL(WB_4+GATE_4,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_4+GATE_4,0)" + "\n");
            strSqlString.Append("                 END),0)  ) ) AS WB_4" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_5,0), 'Merge', NVL(DA_5,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_5,0)" + "\n");
            strSqlString.Append("                 END),0)   )) AS DA_5" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_5+GATE_5,0), 'Merge', NVL(WB_5+GATE_5,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_5+GATE_5,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS WB_5 " + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_ETC,0), 'Merge', NVL(DA_ETC,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)  ) ) AS DA_ETC" + "\n");

            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_ETC+GATE_ETC,0), 'Merge', NVL(WB_ETC+GATE_ETC,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_ETC+GATE_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) AS WB_ETC" + "\n");
            strSqlString.Append("           FROM MWIPMATDEF A," + "\n");
            strSqlString.Append("              (SELECT  '5_WIP_CHIP' GUBUN, LOT.MAT_ID" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1,  0 DA_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0402', 'A0502', 'A0532') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                 ELSE 0  END, 0) ) DA_2" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0403', 'A0503', 'A0533') AND  MAT.MAT_GRP_5 = '3rd' THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) DA_3" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0404', 'A0504', 'A0534') AND  MAT.MAT_GRP_5 = '4th'  THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) DA_4" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0405', 'A0505', 'A0535') AND  MAT.MAT_GRP_5 = '5th' THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) DA_5" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ( 'A0406', 'A0506', 'A0536','A0407', 'A0507', 'A0537','A0408', 'A0508', 'A0538','A0409', 'A0509', 'A0539' ) AND MAT.MAT_GRP_5 IN ( '6th',  '7th', '8th', '9th') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                ELSE 0 END, 0) ) DA_ETC" + "\n");
            strSqlString.Append("                  , 0 WB_1, 0 WB_2, 0 WB_3, 0 WB_4, 0 WB_5, 0 WB_ETC, 0 GATE_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0802') AND  MAT.MAT_GRP_5 = '2nd'THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                 ELSE 0 END, 0) ) GATE_2" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0803') AND  MAT.MAT_GRP_5 = '3rd'THEN" + "\n");
            strSqlString.Append("                                     DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) GATE_3" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0804') AND  MAT.MAT_GRP_5 = '4th' THEN" + "\n");
            strSqlString.Append("                                     DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) GATE_4" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0805') AND  MAT.MAT_GRP_5 = '5th'THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) GATE_5" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0806','A0807','A0808','A0809') AND MAT.MAT_GRP_5 IN ( '6th',  '7th', '8th', '9th')  THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) GATE_ETC" + "\n");
            strSqlString.Append("               FROM (   SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1" + "\n");
            strSqlString.Append("                    , SUM(A.QTY_1) QTY " + "\n");
            strSqlString.Append("                   FROM RWIPLOTSTS A, MWIPOPRDEF B " + "\n");
            strSqlString.Append("                 WHERE 1 = 1" + "\n");
            strSqlString.Append("                  AND A.FACTORY = B.FACTORY(+) " + "\n");
            strSqlString.Append("                  AND A.OPER = B.OPER(+)" + "\n");
            strSqlString.Append("                  AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                  AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                  AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
            strSqlString.Append("                  AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                 GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT" + "\n");
            strSqlString.Append("              WHERE 1 = 1" + "\n");
            strSqlString.Append("               AND LOT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("               AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("               AND MAT.MAT_TYPE = 'FG' AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU')" + "\n");
            strSqlString.Append("               AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
            strSqlString.Append("               AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            strSqlString.Append("               AND MAT.MAT_GRP_2 <> '-'" + "\n");
            strSqlString.Append("              GROUP BY  LOT.MAT_ID ) B," + "\n");
            strSqlString.Append("               ( SELECT KEY_1,DATA_1" + "\n");
            strSqlString.Append("                FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("               WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                 AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')" + "\n");
            strSqlString.Append("                ) G   " + "\n");
            strSqlString.Append("           WHERE B.MAT_ID = A.MAT_ID(+) " + "\n");
            strSqlString.Append("            AND B.MAT_ID = G.KEY_1(+)" + "\n");

            strSqlString.Append("            AND B.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            strSqlString.Append("            AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("            GROUP BY B.GUBUN, A.MAT_ID" + "\n");


            strSqlString.Append("            UNION ALL" + "\n");
            strSqlString.Append("            SELECT '7_실적' GUBUN, MAT_ID," + "\n");
            strSqlString.Append("              SUM( CASE WHEN OPER LIKE 'A040%' THEN  QTY  ELSE 0  END ) DA_RST_TTL ,  SUM( CASE WHEN OPER LIKE 'A060%' THEN  QTY  ELSE 0  END ) WB_RST_TTL," + "\n");
            strSqlString.Append("              SUM( CASE WHEN OPER IN ( 'A0400', 'A0401') THEN QTY  ELSE 0 END ) DA401 , SUM( CASE WHEN OPER IN ( 'A0600', 'A0601') THEN QTY ELSE 0 END )  WB601," + "\n");
            strSqlString.Append("              SUM(DECODE(OPER, 'A0402', QTY,  0)) DA402  , SUM(DECODE(OPER, 'A0602', QTY,  0)) WB602," + "\n");
            strSqlString.Append("              SUM(DECODE(OPER, 'A0403', QTY,  0)) DA403  , SUM(DECODE(OPER, 'A0603', QTY,  0)) WB603," + "\n");
            strSqlString.Append("              SUM(DECODE(OPER, 'A0404', QTY,  0)) DA404  , SUM(DECODE(OPER, 'A0604', QTY,  0)) WB604," + "\n");
            strSqlString.Append("              SUM(DECODE(OPER, 'A0405', QTY,  0)) DA405  , SUM(DECODE(OPER, 'A0605', QTY,  0)) WB605," + "\n");
            strSqlString.Append("              SUM( CASE WHEN OPER IN ( 'A0406',   'A0407',  'A0408',  'A0409') THEN QTY  ELSE 0 END ) DA_Etc," + "\n");
            strSqlString.Append("              SUM( CASE WHEN OPER IN ( 'A0606',   'A0607',  'A0608',  'A0609') THEN  QTY ELSE 0 END ) WB_Etc" + "\n");
            strSqlString.Append("             FROM (  SELECT MAT_ID, OPER, SUM(S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ) QTY" + "\n");
            strSqlString.Append("                 FROM RSUMWIPMOV A" + "\n");
            strSqlString.Append("              WHERE 1=1" + "\n");
            strSqlString.Append("               AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("               AND A.MAT_VER  = 1 " + "\n");
            strSqlString.Append("               AND A.WORK_DATE = '" + selectDate[selectDate.Count - 1] + "'" + "\n");

            strSqlString.Append("               AND A.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            strSqlString.Append("               AND A.LOT_TYPE  = 'W'" + "\n");
            strSqlString.Append("               AND ( A.OPER LIKE  'A040%' OR  A.OPER LIKE  'A060%'  )" + "\n");
            strSqlString.Append("               AND A.CM_KEY_3 LIKE 'P%'" + "\n");
            strSqlString.Append("              GROUP BY A.MAT_ID, A.OPER  )" + "\n");
            strSqlString.Append("            GROUP BY MAT_ID" + "\n");
            strSqlString.Append("           UNION ALL" + "\n");
            strSqlString.Append("           SELECT '8_예상실적' GUBUN, MAT_ID," + "\n");
            strSqlString.Append("               ROUND( (DA_RST_TTL / NOW_MM * 60*24) )  DA_RST_TTL, ROUND( ( WB_RST_TTL/ NOW_MM * 60*24)) WB_RST_TTL," + "\n");
            strSqlString.Append("               ROUND((DA401 /NOW_MM * 60 * 24)) DA401, ROUND((WB601 /NOW_MM * 60 * 24)) WB601," + "\n");
            strSqlString.Append("               ROUND((DA402 /NOW_MM * 60 * 24)) DA402, ROUND((WB602 /NOW_MM * 60 * 24)) WB602," + "\n");
            strSqlString.Append("               ROUND((DA403 /NOW_MM * 60 * 24)) DA403, ROUND((WB603 /NOW_MM * 60 * 24)) WB603," + "\n");
            strSqlString.Append("               ROUND((DA404 /NOW_MM * 60 * 24)) DA404, ROUND((WB604 /NOW_MM * 60 * 24)) WB604," + "\n");
            strSqlString.Append("               ROUND((DA405 /NOW_MM * 60 * 24)) DA405, ROUND((WB605 /NOW_MM * 60 * 24)) WB605," + "\n");
            strSqlString.Append("               ROUND((DA_Etc/NOW_MM * 60 * 24)) DA_Etc, ROUND((WB_Etc/NOW_MM * 60 * 24)) WB_Etc" + "\n");
            strSqlString.Append("           FROM (  SELECT MAT_ID," + "\n");
            strSqlString.Append("                 SUM( CASE WHEN OPER LIKE 'A040%' THEN  QTY  ELSE 0  END ) DA_RST_TTL ,  SUM( CASE WHEN OPER LIKE 'A060%' THEN  QTY  ELSE 0  END ) WB_RST_TTL," + "\n");
            strSqlString.Append("                 SUM( CASE WHEN OPER IN ( 'A0400', 'A0401') THEN QTY  ELSE 0 END ) DA401 , SUM( CASE WHEN OPER IN ( 'A0600', 'A0601') THEN QTY ELSE 0 END )  WB601," + "\n");
            strSqlString.Append("                 SUM(DECODE(OPER, 'A0402', QTY,  0)) DA402  , SUM(DECODE(OPER, 'A0602', QTY,  0)) WB602," + "\n");
            strSqlString.Append("                 SUM(DECODE(OPER, 'A0403', QTY,  0)) DA403  , SUM(DECODE(OPER, 'A0603', QTY,  0)) WB603," + "\n");
            strSqlString.Append("                 SUM(DECODE(OPER, 'A0404', QTY,  0)) DA404  , SUM(DECODE(OPER, 'A0604', QTY,  0)) WB604," + "\n");
            strSqlString.Append("                 SUM(DECODE(OPER, 'A0405', QTY,  0)) DA405  , SUM(DECODE(OPER, 'A0605', QTY,  0)) WB605," + "\n");
            strSqlString.Append("                 SUM( CASE WHEN OPER IN ( 'A0406',   'A0407',  'A0408',  'A0409') THEN QTY  ELSE 0 END ) DA_Etc," + "\n");

            if (Check_Day.Substring(6).Equals("01"))
                strSqlString.Append("                 SUM( CASE WHEN OPER IN ( 'A0606',   'A0607',  'A0608',  'A0609') THEN  QTY ELSE 0 END ) WB_Etc,  (SYSDATE - TO_DATE('" + Last_Month_Last_day + "220000', 'YYYYMMDDHH24MISS') ) * 24 * 60 NOW_MM " + "\n");
            else strSqlString.Append("                 SUM( CASE WHEN OPER IN ( 'A0606',   'A0607',  'A0608',  'A0609') THEN  QTY ELSE 0 END ) WB_Etc,  (SYSDATE - TO_DATE('" + selectDate[selectDate.Count - 2] + "220000', 'YYYYMMDDHH24MISS') ) * 24 * 60 NOW_MM " + "\n");
            
            strSqlString.Append("                FROM (  SELECT MAT_ID, OPER, SUM(S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ) QTY" + "\n");
            strSqlString.Append("                    FROM RSUMWIPMOV A  " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                  AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                  AND A.MAT_VER  = 1" + "\n");

            strSqlString.Append("                  AND A.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            strSqlString.Append("                  AND A.WORK_DATE = '" + selectDate[selectDate.Count - 1] + "'" + "\n");
            strSqlString.Append("                  AND A.LOT_TYPE  = 'W'" + "\n");
            strSqlString.Append("                  AND ( A.OPER LIKE  'A040%' OR  A.OPER LIKE  'A060%'  )" + "\n");
            strSqlString.Append("                  AND A.CM_KEY_3 LIKE 'P%'" + "\n");
            strSqlString.Append("                 GROUP BY A.MAT_ID, A.OPER  )" + "\n");
            strSqlString.Append("               GROUP BY MAT_ID  ) ) A,  (SELECT * FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  AND MAT_TYPE = 'FG' AND MAT_VER =  1 AND  DELETE_FLAG <> 'Y' AND MAT_GRP_2 <> '-' ) B" + "\n");
            strSqlString.Append("            WHERE A.MAT_ID = B.MAT_ID(+) " + "\n"); 
                
           
            strSqlString.Append("            ) A," + "\n");                
            strSqlString.Append("           ( SELECT SEQ FROM HRTDSUMSEQ@RPTTOMES WHERE SEQ < 9 ) C," + "\n");

            strSqlString.Append("           (  SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER LIKE 'A040%' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0) ELSE 0 END) DA_TTL," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER LIKE 'A060%' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) WB_TTL," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER IN ('A0400', 'A0401') THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA1," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0402' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA2," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0403' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA3," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0404' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA4," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0405' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA5," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER IN ('A0600', 'A0601') THEN NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) WB1," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0602' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0) ELSE 0 END) WB2," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0603' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0) ELSE 0 END) WB3," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0604' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) WB4," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0605' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) WB5," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER IN ('A0406', 'A0407', 'A0408', 'A0409') THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA_ETC," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER IN ('A0606', 'A0607', 'A0608', 'A0609') THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) WB_ETC " + "\n");
            strSqlString.Append("                 FROM RSUMWIPMOV A," + "\n");
            strSqlString.Append("                      (SELECT *  FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  AND MAT_TYPE = 'FG' AND MAT_VER =  1 AND  DELETE_FLAG <> 'Y' AND MAT_GRP_2 <> '-' ) MAT" + "\n");
            strSqlString.Append("               WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                    AND A.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                    AND A.CM_KEY_2 = 'PROD'" + "\n");
            strSqlString.Append("                    AND A.MAT_VER = 1 " + "\n");
            strSqlString.Append("                    AND ( A.OPER LIKE 'A040%'  OR  A.OPER LIKE 'A060%' )" + "\n");
            strSqlString.Append("                    AND A.WORK_DATE = '" + selectDate[selectDate.Count - 1] + "' AND A.MAT_ID = MAT.MAT_ID(+)" + "\n");
            strSqlString.Append("              GROUP BY MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11   )  RCV" + "\n");
            strSqlString.Append("     WHERE  A.MAT_GRP_1 = RCV.MAT_GRP_1(+)" + "\n");
            strSqlString.Append("       AND A.MAT_GRP_9 = RCV.MAT_GRP_9(+)" + "\n");
            strSqlString.Append("       AND  A.MAT_GRP_10 =RCV.MAT_GRP_10(+)" + "\n");
            strSqlString.Append("       AND  A.MAT_GRP_6=RCV.MAT_GRP_6(+)" + "\n");
            strSqlString.Append("       AND A.MAT_CMF_11=RCV.MAT_CMF_11(+)" + "\n");

            //상세조회
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);
            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);
            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);
            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);
            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);
            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);
            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);


            strSqlString.Append("     GROUP BY A.MAT_GRP_1, DECODE(SEQ, 1, 'SOP 잔량', 2, '일목표', 3, '소요대수', 4, 'CAPA', 5, 'CHIP', 6, 'MERGE', 7, '실적', '예상실적'), SEQ" + "\n");
          
         //   strSqlString.Append("/* ------------------------------------------------------ " + "\n");
         //   strSqlString.Append("     GRAND TOTAL  Raw Data  " + "\n");
         //   strSqlString.Append(" ------------------------------------------------------*/ " + "\n");

            strSqlString.Append("       UNION ALL" + "\n");
            strSqlString.Append(" SELECT 'ZZZZ' CUSTOMER,  'ZZZZ'  FAMILY, ' TOTAL' PKG,  'TOTAL'  LD_COUNT,  'TOTAL'  PKG_CODE, GUBUN, GUBUN_SEQ," + "\n");

            strSqlString.Append("                  SUM(DA_TTL),  SUM(WB_TTL),  SUM(DA1) , SUM(DA2),   SUM(DA3), SUM(DA4), SUM(DA5), SUM(WB1),SUM(WB2), SUM(WB3),  SUM(WB4),SUM(WB5), SUM(DA_ETC), SUM(WB_ETC)" + "\n");
            strSqlString.Append("         FROM (" + "\n");
            strSqlString.Append("          SELECT A.MAT_GRP_1 CUSTOMER, A.MAT_GRP_9 FAMILY ,  A.MAT_GRP_10 PKG, DECODE(SEQ, 1, 'SOP 잔량', 2, '일목표', 3, '소요대수', 4, 'CAPA', 5, 'CHIP', 6, 'MERGE', 7, '실적', '예상실적') GUBUN," + "\n");
            strSqlString.Append("              SEQ GUBUN_SEQ," + "\n");

            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA_TTL - RCV.DA_TTL, A.DA_TTL), 0 ) ) DA_TTL," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB_TTL - RCV.WB_TTL, A.WB_TTL), 0 ) ) WB_TTL," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA1 - RCV.DA1, A.DA1), 0 ) ) DA1," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA2 - RCV.DA2, A.DA2), 0 ) ) DA2," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA3 - RCV.DA3, A.DA3), 0 ) ) DA3," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA4 - RCV.DA4, A.DA4), 0 ) ) DA4," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA5 - RCV.DA5, A.DA5), 0 ) ) DA5," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB1 - RCV.WB1, A.WB1), 0 ) ) WB1," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB2 - RCV.WB2, A.WB2), 0 ) ) WB2," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB3 - RCV.WB3, A.WB3), 0 ) ) WB3," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB4 - RCV.WB4, A.WB4), 0 ) ) WB4," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB5 - RCV.WB5, A.WB5), 0 ) ) WB5," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.DA_ETC - RCV.DA_ETC, A.DA_ETC), 0 ) ) DA_ETC," + "\n");
            strSqlString.Append("               SUM( DECODE( TO_CHAR(SEQ), SUBSTR(GUBUN, 1, 1) , DECODE( SEQ, 1, A.WB_ETC - RCV.WB_ETC, A.WB_ETC), 0 ) ) WB_ETC" + "\n");

            strSqlString.Append("        FROM (" + "\n");

            strSqlString.Append("       SELECT MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, GUBUN" + "\n");
            strSqlString.Append("           , SUM(DA1) +SUM(DA2) +SUM(DA3) + SUM(DA4) + SUM(DA5) DA_TTL," + "\n");
            strSqlString.Append("           SUM(WB1) +SUM(WB2) +SUM(WB3) +SUM(WB4) +SUM(WB5) WB_TTL," + "\n");
            strSqlString.Append("           SUM(DA1) DA1,SUM(WB1) WB1," + "\n");
            strSqlString.Append("           SUM(DA2) DA2,SUM(WB2) WB2," + "\n");
            strSqlString.Append("           SUM(DA3) DA3,SUM(WB3) WB3," + "\n");
            strSqlString.Append("           SUM(DA4) DA4,SUM(WB4) WB4," + "\n");
            strSqlString.Append("           SUM(DA5) DA5,SUM(WB5) WB5," + "\n");
            strSqlString.Append("           SUM(DA6) + SUM(DA7) + SUM(DA8) + SUM(DA9) DA_ETC," + "\n");
            strSqlString.Append("           SUM(WB6) +SUM(WB7) +SUM(WB8) +SUM(WB9) WB_ETC" + "\n");
            strSqlString.Append("  FROM  (" + "\n");

            strSqlString.Append("           SELECT MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, MAT_KEY,'1_SOP_잔량'  GUBUN," + "\n");
            strSqlString.Append("                   DECODE(MAX_DA1,0,MIN_DA1,MAX_DA1) DA1,  DECODE(MAX_DA6,0,MIN_DA6,MAX_DA6) DA6" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA2,0,MIN_DA2,MAX_DA2) DA2, DECODE(MAX_DA7,0,MIN_DA7,MAX_DA7) DA7" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA3,0,MIN_DA3,MAX_DA3) DA3, DECODE(MAX_DA8,0,MIN_DA8,MAX_DA8) DA8" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA4,0,MIN_DA4,MAX_DA4) DA4, DECODE(MAX_DA9,0,MIN_DA9,MAX_DA9) DA9" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA5,0,MIN_DA5,MAX_DA5) DA5" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB1,0,MIN_WB1,MAX_WB1) WB1, DECODE(MAX_WB6,0,MIN_WB6,MAX_WB6) WB6" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB2,0,MIN_WB2,MAX_WB2) WB2, DECODE(MAX_WB7,0,MIN_WB7,MAX_WB7) WB7" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB3,0,MIN_WB3,MAX_WB3) WB3, DECODE(MAX_WB8,0,MIN_WB8,MAX_WB8) WB8" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB4,0,MIN_WB4,MAX_WB4) WB4, DECODE(MAX_WB9,0,MIN_WB9,MAX_WB9) WB9" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB5,0,MIN_WB5,MAX_WB5) WB5" + "\n");
            strSqlString.Append("             FROM (" + "\n");
            strSqlString.Append("                     SELECT CUSTOMER MAT_GRP_1, FAMILY MAT_GRP_2, PKG MAT_GRP_3, TYPE1 MAT_GRP_4, TYPE2 MAT_GRP_5, LD_COUNT MAT_GRP_6, DENSITY MAT_GRP_7, GENERATION MAT_GRP_8, MAJOR_CODE MAT_GRP_9, PKG2 MAT_GRP_10, PKG_CODE MAT_CMF_11, MAT_KEY," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA1), 'HX', MAX(DA1), SUM(DA1) ) MAX_DA1,DECODE(CUSTOMER, 'SE', MIN(DA1), 'HX', MIN(DA1),0) MIN_DA1," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA2), 'HX', MAX(DA2), SUM(DA2) ) MAX_DA2,DECODE(CUSTOMER, 'SE', MIN(DA2), 'HX', MIN(DA2),0) MIN_DA2," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA3), 'HX', MAX(DA3), SUM(DA3) ) MAX_DA3,DECODE(CUSTOMER, 'SE', MIN(DA3), 'HX', MIN(DA3),0) MIN_DA3," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA4), 'HX', MAX(DA4), SUM(DA4) ) MAX_DA4,DECODE(CUSTOMER, 'SE', MIN(DA4), 'HX', MIN(DA4),0) MIN_DA4," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA5), 'HX', MAX(DA5), SUM(DA5) ) MAX_DA5,DECODE(CUSTOMER, 'SE', MIN(DA5), 'HX', MIN(DA5),0) MIN_DA5," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA6), 'HX', MAX(DA6), SUM(DA6) ) MAX_DA6,DECODE(CUSTOMER, 'SE', MIN(DA6), 'HX', MIN(DA6),0) MIN_DA6," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA7), 'HX', MAX(DA7), SUM(DA7) ) MAX_DA7,DECODE(CUSTOMER, 'SE', MIN(DA7), 'HX', MIN(DA7),0) MIN_DA7," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA8), 'HX', MAX(DA8), SUM(DA8) ) MAX_DA8,DECODE(CUSTOMER, 'SE', MIN(DA8), 'HX', MIN(DA8),0) MIN_DA8," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA9), 'HX', MAX(DA9), SUM(DA9) ) MAX_DA9,DECODE(CUSTOMER, 'SE', MIN(DA9), 'HX', MIN(DA9),0) MIN_DA9," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB1), 'HX', MAX(WB1), SUM(WB1) ) MAX_WB1,DECODE(CUSTOMER, 'SE', MIN(WB1), 'HX', MIN(WB1),0) MIN_WB1," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB2), 'HX', MAX(WB2), SUM(WB2) ) MAX_WB2,DECODE(CUSTOMER, 'SE', MIN(WB2), 'HX', MIN(WB2),0) MIN_WB2," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB3), 'HX', MAX(WB3), SUM(WB3) ) MAX_WB3,DECODE(CUSTOMER, 'SE', MIN(WB3), 'HX', MIN(WB3),0) MIN_WB3," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB4), 'HX', MAX(WB4), SUM(WB4) ) MAX_WB4,DECODE(CUSTOMER, 'SE', MIN(WB4), 'HX', MIN(WB4),0) MIN_WB4," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB5), 'HX', MAX(WB5), SUM(WB5) ) MAX_WB5,DECODE(CUSTOMER, 'SE', MIN(WB5), 'HX', MIN(WB5),0) MIN_WB5," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB6), 'HX', MAX(WB6), SUM(WB6) ) MAX_WB6,DECODE(CUSTOMER, 'SE', MIN(WB6), 'HX', MIN(WB6),0) MIN_WB6," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB7), 'HX', MAX(WB7), SUM(WB7) ) MAX_WB7,DECODE(CUSTOMER, 'SE', MIN(WB7), 'HX', MIN(WB7),0) MIN_WB7," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB8), 'HX', MAX(WB8), SUM(WB8) ) MAX_WB8,DECODE(CUSTOMER, 'SE', MIN(WB8), 'HX', MIN(WB8),0) MIN_WB8," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB9), 'HX', MAX(WB9), SUM(WB9) ) MAX_WB9, DECODE(CUSTOMER, 'SE', MIN(WB9), 'HX', MIN(WB9),0) MIN_WB9" + "\n");
            strSqlString.Append("                       FROM RSUMOPRREM" + "\n");
            strSqlString.Append("                      WHERE WORK_DATE = '" + selectDate[selectDate.Count - 1] + "'" + "\n");
            strSqlString.Append("                        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                        AND RESV_FIELD_1 = ' '" + "\n");
            strSqlString.Append("                        AND MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
            strSqlString.Append("                      GROUP BY CUSTOMER , FAMILY , PKG , TYPE1 , TYPE2 , LD_COUNT , DENSITY , GENERATION , MAJOR_CODE , PKG2 , PKG_CODE  , MAT_KEY ) )" + "\n");
            strSqlString.Append("               GROUP BY MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, GUBUN" + "\n");
            strSqlString.Append("       UNION ALL" + "\n");
            strSqlString.Append("       SELECT MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, GUBUN" + "\n");
            strSqlString.Append("              , SUM(DA1) +SUM(DA2) +SUM(DA3) + SUM(DA4) + SUM(DA5) DA_TTL," + "\n");
            strSqlString.Append("              SUM(WB1) +SUM(WB2) +SUM(WB3) +SUM(WB4) +SUM(WB5) WB_TTL," + "\n");
            strSqlString.Append("              SUM(DA1) DA1,SUM(WB1) WB1," + "\n");
            strSqlString.Append("              SUM(DA2) DA2,SUM(WB2) WB2," + "\n");
            strSqlString.Append("              SUM(DA3) DA3,SUM(WB3) WB3," + "\n");
            strSqlString.Append("              SUM(DA4) DA4,SUM(WB4) WB4," + "\n");
            strSqlString.Append("              SUM(DA5) DA5,SUM(WB5) WB5," + "\n");
            strSqlString.Append("              SUM(DA6) + SUM(DA7) + SUM(DA8) + SUM(DA9) DA_ETC," + "\n");
            strSqlString.Append("              SUM(WB6) +SUM(WB7) +SUM(WB8) +SUM(WB9) WB_ETC" + "\n");
            strSqlString.Append("  FROM  (" + "\n");
            strSqlString.Append("           SELECT MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, MAT_KEY,'2_목표'  GUBUN," + "\n");
            strSqlString.Append("                   DECODE(MAX_DA1,0,MIN_DA1,MAX_DA1) DA1,  DECODE(MAX_DA6,0,MIN_DA6,MAX_DA6) DA6" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA2,0,MIN_DA2,MAX_DA2) DA2, DECODE(MAX_DA7,0,MIN_DA7,MAX_DA7) DA7" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA3,0,MIN_DA3,MAX_DA3) DA3, DECODE(MAX_DA8,0,MIN_DA8,MAX_DA8) DA8" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA4,0,MIN_DA4,MAX_DA4) DA4, DECODE(MAX_DA9,0,MIN_DA9,MAX_DA9) DA9" + "\n");
            strSqlString.Append("                   , DECODE(MAX_DA5,0,MIN_DA5,MAX_DA5) DA5" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB1,0,MIN_WB1,MAX_WB1) WB1, DECODE(MAX_WB6,0,MIN_WB6,MAX_WB6) WB6" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB2,0,MIN_WB2,MAX_WB2) WB2, DECODE(MAX_WB7,0,MIN_WB7,MAX_WB7) WB7" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB3,0,MIN_WB3,MAX_WB3) WB3, DECODE(MAX_WB8,0,MIN_WB8,MAX_WB8) WB8" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB4,0,MIN_WB4,MAX_WB4) WB4, DECODE(MAX_WB9,0,MIN_WB9,MAX_WB9) WB9" + "\n");
            strSqlString.Append("                   , DECODE(MAX_WB5,0,MIN_WB5,MAX_WB5) WB5" + "\n");
            strSqlString.Append("             FROM (" + "\n");
            strSqlString.Append("                     SELECT CUSTOMER MAT_GRP_1, FAMILY MAT_GRP_2, PKG MAT_GRP_3, TYPE1 MAT_GRP_4, TYPE2 MAT_GRP_5, LD_COUNT MAT_GRP_6, DENSITY MAT_GRP_7, GENERATION MAT_GRP_8, MAJOR_CODE MAT_GRP_9, PKG2 MAT_GRP_10, PKG_CODE MAT_CMF_11, MAT_KEY," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA1), 'HX', MAX(DA1), SUM(DA1) ) MAX_DA1,DECODE(CUSTOMER, 'SE', MIN(DA1), 'HX', MIN(DA1),0) MIN_DA1," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA2), 'HX', MAX(DA2), SUM(DA2) ) MAX_DA2,DECODE(CUSTOMER, 'SE', MIN(DA2), 'HX', MIN(DA2),0) MIN_DA2," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA3), 'HX', MAX(DA3), SUM(DA3) ) MAX_DA3,DECODE(CUSTOMER, 'SE', MIN(DA3), 'HX', MIN(DA3),0) MIN_DA3," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA4), 'HX', MAX(DA4), SUM(DA4) ) MAX_DA4,DECODE(CUSTOMER, 'SE', MIN(DA4), 'HX', MIN(DA4),0) MIN_DA4," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA5), 'HX', MAX(DA5), SUM(DA5) ) MAX_DA5,DECODE(CUSTOMER, 'SE', MIN(DA5), 'HX', MIN(DA5),0) MIN_DA5," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA6), 'HX', MAX(DA6), SUM(DA6) ) MAX_DA6,DECODE(CUSTOMER, 'SE', MIN(DA6), 'HX', MIN(DA6),0) MIN_DA6," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA7), 'HX', MAX(DA7), SUM(DA7) ) MAX_DA7,DECODE(CUSTOMER, 'SE', MIN(DA7), 'HX', MIN(DA7),0) MIN_DA7," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA8), 'HX', MAX(DA8), SUM(DA8) ) MAX_DA8,DECODE(CUSTOMER, 'SE', MIN(DA8), 'HX', MIN(DA8),0) MIN_DA8," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(DA9), 'HX', MAX(DA9), SUM(DA9) ) MAX_DA9,DECODE(CUSTOMER, 'SE', MIN(DA9), 'HX', MIN(DA9),0) MIN_DA9," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB1), 'HX', MAX(WB1), SUM(WB1) ) MAX_WB1,DECODE(CUSTOMER, 'SE', MIN(WB1), 'HX', MIN(WB1),0) MIN_WB1," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB2), 'HX', MAX(WB2), SUM(WB2) ) MAX_WB2,DECODE(CUSTOMER, 'SE', MIN(WB2), 'HX', MIN(WB2),0) MIN_WB2," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB3), 'HX', MAX(WB3), SUM(WB3) ) MAX_WB3,DECODE(CUSTOMER, 'SE', MIN(WB3), 'HX', MIN(WB3),0) MIN_WB3," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB4), 'HX', MAX(WB4), SUM(WB4) ) MAX_WB4,DECODE(CUSTOMER, 'SE', MIN(WB4), 'HX', MIN(WB4),0) MIN_WB4," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB5), 'HX', MAX(WB5), SUM(WB5) ) MAX_WB5,DECODE(CUSTOMER, 'SE', MIN(WB5), 'HX', MIN(WB5),0) MIN_WB5," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB6), 'HX', MAX(WB6), SUM(WB6) ) MAX_WB6,DECODE(CUSTOMER, 'SE', MIN(WB6), 'HX', MIN(WB6),0) MIN_WB6," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB7), 'HX', MAX(WB7), SUM(WB7) ) MAX_WB7,DECODE(CUSTOMER, 'SE', MIN(WB7), 'HX', MIN(WB7),0) MIN_WB7," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB8), 'HX', MAX(WB8), SUM(WB8) ) MAX_WB8,DECODE(CUSTOMER, 'SE', MIN(WB8), 'HX', MIN(WB8),0) MIN_WB8," + "\n");
            strSqlString.Append("                            DECODE(CUSTOMER, 'SE', MAX(WB9), 'HX', MAX(WB9), SUM(WB9) ) MAX_WB9, DECODE(CUSTOMER, 'SE', MIN(WB9), 'HX', MIN(WB9),0) MIN_WB9" + "\n");
            strSqlString.Append("                       FROM RSUMOPRREM" + "\n");
            strSqlString.Append("                      WHERE WORK_DATE = '" + selectDate[selectDate.Count - 1] + "'" + "\n");
            strSqlString.Append("                        AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                        AND RESV_FIELD_1 = 'TARGET'" + "\n");
            strSqlString.Append("                        AND MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
            strSqlString.Append("                      GROUP BY  CUSTOMER , FAMILY , PKG , TYPE1 , TYPE2 , LD_COUNT , DENSITY , GENERATION , MAJOR_CODE , PKG2 , PKG_CODE  , MAT_KEY ) )" + "\n");
            strSqlString.Append("               GROUP BY MAT_GRP_1,  MAT_GRP_2,  MAT_GRP_3,  MAT_GRP_4,  MAT_GRP_5,  MAT_GRP_6,  MAT_GRP_7,  MAT_GRP_8,  MAT_GRP_9,  MAT_GRP_10,  MAT_CMF_11, GUBUN" + "\n");

            strSqlString.Append("          UNION ALL" + "\n");
            strSqlString.Append("         SELECT B.MAT_GRP_1,  B.MAT_GRP_2,  B.MAT_GRP_3,  B.MAT_GRP_4,  B.MAT_GRP_5,  B.MAT_GRP_6,  B.MAT_GRP_7,  B.MAT_GRP_8,  B.MAT_GRP_9,  B.MAT_GRP_10,  B.MAT_CMF_11, GUBUN,  DA_TTL, WB_TTL, DA1, WB1, DA2, WB2, DA3, WB3, DA4, WB4, DA5, WB5, DA_ETC, WB_ETC" + "\n");
            
            strSqlString.Append("         FROM ( SELECT  '3_설비댓수' GUBUN, MAT_ID, " + "\n");
            strSqlString.Append("               SUM( CASE WHEN OPER LIKE 'A040%' THEN  RES_CNT  ELSE 0  END ) DA_TTL, SUM( CASE WHEN OPER LIKE 'A060%' THEN  RES_CNT  ELSE 0  END ) WB_TTL," + "\n");
            strSqlString.Append("               SUM( DECODE(OPER, 'A0400', RES_CNT, 'A0401', RES_CNT, 0) ) DA1, SUM( DECODE(OPER, 'A0600', RES_CNT, 'A0601', RES_CNT, 0)) WB1," + "\n");
            strSqlString.Append("               SUM( DECODE(OPER, 'A0402', RES_CNT, 0)) DA2, SUM( DECODE(OPER, 'A0602', RES_CNT, 0)) WB2," + "\n");
            strSqlString.Append("               SUM( DECODE(OPER, 'A0403', RES_CNT, 0)) DA3,SUM( DECODE(OPER, 'A0603', RES_CNT, 0)) WB3," + "\n");
            strSqlString.Append("               SUM( DECODE(OPER, 'A0404', RES_CNT, 0)) DA4, SUM( DECODE(OPER, 'A0604', RES_CNT, 0)) WB4," + "\n");
            strSqlString.Append("               SUM( DECODE(OPER, 'A0405', RES_CNT, 0)) DA5, SUM( DECODE(OPER, 'A0605', RES_CNT, 0)) WB5," + "\n");
            strSqlString.Append("               SUM( CASE WHEN OPER IN ( 'A0406',  'A0407', 'A0408', 'A0409' ) THEN  RES_CNT  ELSE 0 END ) DA_ETC," + "\n");
            strSqlString.Append("               SUM( CASE WHEN OPER IN ( 'A0606',  'A0607', 'A0608', 'A0609' ) THEN  RES_CNT  ELSE 0 END ) WB_ETC" + "\n");
            strSqlString.Append("            FROM (  SELECT FACTORY, RES_STS_2 MAT_ID, RES_GRP_6 RES_MODEL, RES_GRP_7 UPEH_GROUP, RES_STS_8 OPER , COUNT(RES_ID) RES_CNT" + "\n");
            strSqlString.Append("                  FROM  MRASRESDEF  " + "\n");
            strSqlString.Append("               WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("                AND DELETE_FLAG  = ' '  " + "\n");
            strSqlString.Append("                AND RES_TYPE  = 'EQUIPMENT' " + "\n");
            strSqlString.Append("                AND RES_STS_8 IN('A0400','A0401','A0402','A0403','A0404','A0405','A0600','A0601','A0602','A0603','A0604','A0605') " + "\n");
            strSqlString.Append("               GROUP BY FACTORY, RES_STS_2, RES_GRP_6, RES_GRP_7, RES_STS_8 )" + "\n");

            strSqlString.Append("            WHERE MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            strSqlString.Append("            GROUP BY  MAT_ID" + "\n");

            strSqlString.Append("  UNION ALL" + "\n");
            strSqlString.Append("            SELECT '4_CAPA' GUBUN, MAT_ID," + "\n");
            strSqlString.Append("                   SUM( CASE WHEN OPER LIKE 'A040%' THEN   RES_CNT * NVL(UPEH,0) * 24   ELSE 0  END ) RES_DA_TTL," + "\n");
            strSqlString.Append("                   SUM( CASE WHEN  OPER LIKE 'A060%' THEN   RES_CNT * NVL(UPEH,0) * 24  ELSE 0  END ) RES_WB_TTL," + "\n");
            strSqlString.Append("                   SUM( CASE WHEN OPER IN ( 'A0400',  'A0401' ) THEN  RES_CNT * NVL(UPEH,0) * 24 ELSE 0 END ) RES_CNT_DA1" + "\n");
            strSqlString.Append("                   , SUM( CASE WHEN OPER IN ( 'A0600',  'A0601' ) THEN  RES_CNT * NVL(UPEH,0) * 24 ELSE 0 END )  RES_CNT_WB1," + "\n");
            strSqlString.Append("                   SUM( DECODE( OPER, 'A0402',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_DA2, SUM( DECODE( OPER, 'A0602', RES_CNT *  NVL(UPEH,0) * 24, 0)) RES_CNT_WB2," + "\n");
            strSqlString.Append("                   SUM( DECODE( OPER, 'A0403',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_DA3, SUM( DECODE( OPER, 'A0603',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_WB3," + "\n");
            strSqlString.Append("                   SUM( DECODE( OPER, 'A0404',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_DA4, SUM( DECODE( OPER, 'A0604',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_WB4," + "\n");
            strSqlString.Append("                   SUM( DECODE( OPER, 'A0405',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_DA5, SUM( DECODE( OPER, 'A0605',  RES_CNT * NVL(UPEH,0) * 24, 0)) RES_CNT_WB5," + "\n");
            strSqlString.Append("                   SUM( CASE WHEN OPER IN ( 'A0406',  'A0407', 'A0408', 'A0409' ) THEN  RES_CNT * NVL(UPEH,0) * 24 ELSE 0 END ) DA_ETC," + "\n");
            strSqlString.Append("                   SUM( CASE WHEN OPER IN ( 'A0606',  'A0607', 'A0608', 'A0609' ) THEN  RES_CNT * NVL(UPEH,0) * 24 ELSE 0 END ) WB_ETC" + "\n");
            strSqlString.Append("              FROM (  SELECT RAS.FACTORY, RAS.RES_GRP_6 RES_MODEL, RAS.RES_STS_2 MAT_ID, RAS.RES_STS_8 OPER , COUNT(RES_ID) RES_CNT," + "\n");
            strSqlString.Append("                             MAX(DECODE(SUBSTR(RAS.RES_STS_8, 1, 3), 'A04', NVL(UPEH.UPEH, 0) * 0.75,  NVL(UPEH.UPEH, 0))  ) UPEH " + "\n");
            strSqlString.Append("                        FROM MRASRESDEF RAS, CRASUPHDEF UPEH" + "\n");
            strSqlString.Append("                       WHERE RAS.FACTORY   = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                         AND RAS.RES_CMF_9 = 'Y'" + "\n");
            strSqlString.Append("                         AND RAS.DELETE_FLAG  = ' '" + "\n");
            strSqlString.Append("                         AND RAS.RES_TYPE  = 'EQUIPMENT'" + "\n");
            strSqlString.Append("                         AND (RAS.RES_STS_8 LIKE 'A040%' OR RAS.RES_STS_8 LIKE 'A060%') " + "\n");
            strSqlString.Append("                         AND RAS.RES_STS_2 LIKE '" + txtSearchProduct.Text + "'" + "\n");
            strSqlString.Append("                         AND RAS.FACTORY = UPEH.FACTORY(+)" + "\n");
            strSqlString.Append("                         AND RAS.RES_GRP_6 = UPEH.RES_MODEL(+)" + "\n");
            strSqlString.Append("                         AND RAS.RES_STS_2 = UPEH.MAT_ID(+)" + "\n");
            strSqlString.Append("                         AND RAS.RES_STS_8 = UPEH.OPER(+)" + "\n");
            strSqlString.Append("                       GROUP BY RAS.FACTORY, RAS.RES_GRP_6, RAS.RES_STS_2, RAS.RES_STS_8 )" + "\n");
            strSqlString.Append("             GROUP BY MAT_ID" + "\n");

            strSqlString.Append("          UNION ALL" + "\n");
            strSqlString.Append("          SELECT B.GUBUN, A.MAT_ID" + "\n");
            strSqlString.Append("              , ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0), 'Merge', NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0)" + "\n");
            strSqlString.Append("                 END),0) )  ) DA_WIP_TTL" + "\n");
            strSqlString.Append("              , ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0), 'Merge', NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) WB_WIP_TTL" + "\n");

            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN " + "\n");
            strSqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 0)" + "\n");
            strSqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)  ELSE 0 END" + "\n");
            strSqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)" + "\n");
            strSqlString.Append("                       END), 0)  ) ) AS DA_1" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1+GATE_1,0), 'Merge', NVL(WB_1+GATE_1,0), 0)" + "\n");
            strSqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_1+GATE_1,0) ELSE 0 END" + "\n");
            strSqlString.Append("                         ELSE NVL(WB_1+GATE_1,0)" + "\n");
            strSqlString.Append("                       END),0)   ) ) AS WB_1" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_2,0), 'Merge', NVL(DA_2,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_2,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_2,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) AS DA_2" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_2+GATE_2,0), 'Merge', NVL(WB_2+GATE_2,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_2+GATE_2,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_2+GATE_2,0)" + "\n");
            strSqlString.Append("                 END),0)   )  ) AS WB_2" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_3,0), 'Merge', NVL(DA_3,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_3,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_3,0)" + "\n");
            strSqlString.Append("                 END),0)  )  ) AS DA_3" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_3+GATE_3,0), 'Merge', NVL(WB_3+GATE_3,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_3+GATE_3,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_3+GATE_3,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS WB_3" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_4,0), 'Merge', NVL(DA_4,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_4,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_4,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS DA_4" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_4+GATE_4,0), 'Merge', NVL(WB_4+GATE_4,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_4+GATE_4,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_4+GATE_4,0)" + "\n");
            strSqlString.Append("                 END),0)  ) ) AS WB_4" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_5,0), 'Merge', NVL(DA_5,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_5,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_5,0)" + "\n");
            strSqlString.Append("                 END),0)   )) AS DA_5" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_5+GATE_5,0), 'Merge', NVL(WB_5+GATE_5,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_5+GATE_5,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_5+GATE_5,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS WB_5" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_ETC,0), 'Merge', NVL(DA_ETC,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(DA_ETC,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)  ) ) AS DA_ETC" + "\n");
            
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_ETC+GATE_ETC,0), 'Merge', NVL(WB_ETC+GATE_ETC,0), 0)" + "\n");
            strSqlString.Append("                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_ETC+GATE_ETC,0) ELSE 0 END" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_ETC+GATE_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) AS WB_ETC" + "\n");
            strSqlString.Append("           FROM MWIPMATDEF A," + "\n");
            strSqlString.Append("              (  SELECT '6_WIP_MERGE' GUBUN, LOT.MAT_ID " + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0250', 'A0305', 'A0306', 'A0310', 'A0400', 'A0401', 'A0500', 'A0501', 'A0530', 'A0531' ) THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0402', 'A0502', 'A0532') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_2" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0403', 'A0503', 'A0533') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_3" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0404', 'A0504', 'A0534') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_4" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0405', 'A0505', 'A0535') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_5" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ( 'A0406', 'A0506', 'A0536','A0407', 'A0507', 'A0537','A0408', 'A0508', 'A0538','A0409', 'A0509', 'A0539' ) THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) DA_ETC  " + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0550', 'A0551', 'A0600','A0601' ) THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0552', 'A0602') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_2" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0553', 'A0603') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_3" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0554', 'A0604') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_4" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0555', 'A0605') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_5" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A0606', 'A0556',  'A0607', 'A0557',  'A0608', 'A0558',  'A0609', 'A0559' ) THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) WB_ETC" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0800', 'A0801') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0802') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_2" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0803') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_3" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0804') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_4" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0805') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_5" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0806','A0807','A0808','A0809') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                   ELSE 0 END, 0) ) GATE_ETC" + "\n");
            strSqlString.Append("                FROM ( SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1" + "\n");
            strSqlString.Append("                      , SUM(A.QTY_1) QTY" + "\n");
            strSqlString.Append("                  FROM RWIPLOTSTS A, MWIPOPRDEF B " + "\n");
            strSqlString.Append("                   WHERE 1 = 1 " + "\n");
            strSqlString.Append("                    AND A.FACTORY = B.FACTORY(+)" + "\n");
            strSqlString.Append("                    AND A.OPER = B.OPER(+)" + "\n");
            strSqlString.Append("                    AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                    AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                    AND A.LOT_CMF_5 LIKE 'P%' " + "\n");
            strSqlString.Append("                    AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                   GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT " + "\n");
            strSqlString.Append("               WHERE 1 = 1" + "\n");
            strSqlString.Append("                 AND LOT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("                 AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                 AND MAT.MAT_TYPE = 'FG' AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-'" + "\n");
            strSqlString.Append("                 AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            strSqlString.Append("                 AND MAT.MAT_GRP_2 <> '-'" + "\n");
            strSqlString.Append("               GROUP BY LOT.MAT_ID" + "\n");

            strSqlString.Append("               ) B," + "\n");
            strSqlString.Append("               ( SELECT KEY_1,DATA_1" + "\n");
            strSqlString.Append("                FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("               WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                 AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')" + "\n");
            strSqlString.Append("                ) G" + "\n");
            strSqlString.Append("           WHERE B.MAT_ID = A.MAT_ID(+)" + "\n");
            strSqlString.Append("            AND B.MAT_ID = G.KEY_1(+)" + "\n");

            strSqlString.Append("            AND B.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            strSqlString.Append("            AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("            GROUP BY B.GUBUN, A.MAT_ID" + "\n");

            strSqlString.Append("  UNION ALL SELECT B.GUBUN, A.MAT_ID" + "\n");
            strSqlString.Append("              , ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0), 'Merge', NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_1+DA_2+DA_3+DA_4+DA_5+DA_ETC,0)" + "\n");
            strSqlString.Append("                 END),0) )  ) DA_WIP_TTL" + "\n");
            strSqlString.Append("              , ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0), 'Merge', NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_1+WB_2+WB_3+WB_4+WB_5+WB_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) WB_WIP_TTL" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN " + "\n");
            strSqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 0)" + "\n");
            strSqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)" + "\n");
            strSqlString.Append("                       END), 0)  ) ) AS DA_1" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1+GATE_1,0), 'Merge', NVL(WB_1+GATE_1,0), 0)" + "\n");
            strSqlString.Append("                         ELSE NVL(WB_1+GATE_1,0) " + "\n");
            strSqlString.Append("                       END),0)   ) ) AS WB_1" + "\n");

            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_2,0), 'Merge', NVL(DA_2,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_2,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) AS DA_2" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_2+GATE_2,0), 'Merge', NVL(WB_2+GATE_2,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_2+GATE_2,0)" + "\n");
            strSqlString.Append("                 END),0)   )  ) AS WB_2 " + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_3,0), 'Merge', NVL(DA_3,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_3,0)" + "\n");
            strSqlString.Append("                 END),0)  )  ) AS DA_3" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_3+GATE_3,0), 'Merge', NVL(WB_3+GATE_3,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_3+GATE_3,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS WB_3" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_4,0), 'Merge', NVL(DA_4,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_4,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS DA_4" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_4+GATE_4,0), 'Merge', NVL(WB_4+GATE_4,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_4+GATE_4,0)" + "\n");
            strSqlString.Append("                 END),0)  ) ) AS WB_4" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_5,0), 'Merge', NVL(DA_5,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_5,0)" + "\n");
            strSqlString.Append("                 END),0)   )) AS DA_5" + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_5+GATE_5,0), 'Merge', NVL(WB_5+GATE_5,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_5+GATE_5,0)" + "\n");
            strSqlString.Append("                 END),0) ) ) AS WB_5 " + "\n");
            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(DA_ETC,0), 'Merge', NVL(DA_ETC,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(DA_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)  ) ) AS DA_ETC" + "\n");

            strSqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_ETC+GATE_ETC,0), 'Merge', NVL(WB_ETC+GATE_ETC,0), 0)" + "\n");
            strSqlString.Append("                   ELSE NVL(WB_ETC+GATE_ETC,0)" + "\n");
            strSqlString.Append("                 END),0)   ) ) AS WB_ETC" + "\n");
            strSqlString.Append("           FROM MWIPMATDEF A," + "\n");
            strSqlString.Append("              (SELECT  '5_WIP_CHIP' GUBUN, LOT.MAT_ID" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1,  0 DA_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0402', 'A0502', 'A0532') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                 ELSE 0  END, 0) ) DA_2" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0403', 'A0503', 'A0533') AND  MAT.MAT_GRP_5 = '3rd' THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) DA_3" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0404', 'A0504', 'A0534') AND  MAT.MAT_GRP_5 = '4th'  THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) DA_4" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0405', 'A0505', 'A0535') AND  MAT.MAT_GRP_5 = '5th' THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) DA_5" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ( 'A0406', 'A0506', 'A0536','A0407', 'A0507', 'A0537','A0408', 'A0508', 'A0538','A0409', 'A0509', 'A0539' ) AND MAT.MAT_GRP_5 IN ( '6th',  '7th', '8th', '9th') THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                ELSE 0 END, 0) ) DA_ETC" + "\n");
            strSqlString.Append("                  , 0 WB_1, 0 WB_2, 0 WB_3, 0 WB_4, 0 WB_5, 0 WB_ETC, 0 GATE_1" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0802') AND  MAT.MAT_GRP_5 = '2nd'THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                 ELSE 0 END, 0) ) GATE_2" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0803') AND  MAT.MAT_GRP_5 = '3rd'THEN" + "\n");
            strSqlString.Append("                                     DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) GATE_3" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0804') AND  MAT.MAT_GRP_5 = '4th' THEN" + "\n");
            strSqlString.Append("                                     DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) GATE_4" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0805') AND  MAT.MAT_GRP_5 = '5th'THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) GATE_5" + "\n");
            strSqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0806','A0807','A0808','A0809') AND MAT.MAT_GRP_5 IN ( '6th',  '7th', '8th', '9th')  THEN" + "\n");
            strSqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            strSqlString.Append("                                  ELSE 0 END, 0) ) GATE_ETC" + "\n");
            strSqlString.Append("               FROM (   SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1" + "\n");
            strSqlString.Append("                    , SUM(A.QTY_1) QTY " + "\n");
            strSqlString.Append("                   FROM RWIPLOTSTS A, MWIPOPRDEF B " + "\n");
            strSqlString.Append("                 WHERE 1 = 1" + "\n");
            strSqlString.Append("                  AND A.FACTORY = B.FACTORY(+) " + "\n");
            strSqlString.Append("                  AND A.OPER = B.OPER(+)" + "\n");
            strSqlString.Append("                  AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                  AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                  AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
            strSqlString.Append("                  AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                 GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT" + "\n");
            strSqlString.Append("              WHERE 1 = 1" + "\n");
            strSqlString.Append("               AND LOT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("               AND LOT.MAT_ID = MAT.MAT_ID" + "\n");

            strSqlString.Append("               AND MAT.MAT_TYPE = 'FG' AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU')" + "\n");
            strSqlString.Append("               AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
            strSqlString.Append("               AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            strSqlString.Append("               AND MAT.MAT_GRP_2 <> '-'" + "\n");
            strSqlString.Append("              GROUP BY  LOT.MAT_ID ) B," + "\n");
            strSqlString.Append("               ( SELECT KEY_1,DATA_1" + "\n");
            strSqlString.Append("                FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("               WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                 AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')" + "\n");
            strSqlString.Append("                ) G   " + "\n");
            strSqlString.Append("           WHERE B.MAT_ID = A.MAT_ID(+) " + "\n");
            strSqlString.Append("            AND B.MAT_ID = G.KEY_1(+)" + "\n");

            strSqlString.Append("            AND B.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            strSqlString.Append("            AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("            GROUP BY B.GUBUN, A.MAT_ID" + "\n");

            strSqlString.Append("            UNION ALL" + "\n");
            strSqlString.Append("            SELECT '7_실적' GUBUN, MAT_ID," + "\n");
            strSqlString.Append("              SUM( CASE WHEN OPER LIKE 'A040%' THEN  QTY  ELSE 0  END ) DA_RST_TTL ,  SUM( CASE WHEN OPER LIKE 'A060%' THEN  QTY  ELSE 0  END ) WB_RST_TTL," + "\n");
            strSqlString.Append("              SUM( CASE WHEN OPER IN ( 'A0400', 'A0401') THEN QTY  ELSE 0 END ) DA401 , SUM( CASE WHEN OPER IN ( 'A0600', 'A0601') THEN QTY ELSE 0 END )  WB601," + "\n");
            strSqlString.Append("              SUM(DECODE(OPER, 'A0402', QTY,  0)) DA402  , SUM(DECODE(OPER, 'A0602', QTY,  0)) WB602," + "\n");
            strSqlString.Append("              SUM(DECODE(OPER, 'A0403', QTY,  0)) DA403  , SUM(DECODE(OPER, 'A0603', QTY,  0)) WB603," + "\n");
            strSqlString.Append("              SUM(DECODE(OPER, 'A0404', QTY,  0)) DA404  , SUM(DECODE(OPER, 'A0604', QTY,  0)) WB604," + "\n");
            strSqlString.Append("              SUM(DECODE(OPER, 'A0405', QTY,  0)) DA405  , SUM(DECODE(OPER, 'A0605', QTY,  0)) WB605," + "\n");
            strSqlString.Append("              SUM( CASE WHEN OPER IN ( 'A0406',   'A0407',  'A0408',  'A0409') THEN QTY  ELSE 0 END ) DA_Etc," + "\n");
            strSqlString.Append("              SUM( CASE WHEN OPER IN ( 'A0606',   'A0607',  'A0608',  'A0609') THEN  QTY ELSE 0 END ) WB_Etc" + "\n");
            strSqlString.Append("             FROM (  SELECT MAT_ID, OPER, SUM(S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ) QTY" + "\n");
            strSqlString.Append("                 FROM RSUMWIPMOV A" + "\n");
            strSqlString.Append("              WHERE 1=1" + "\n");
            strSqlString.Append("               AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("               AND A.MAT_VER  = 1 " + "\n");

            strSqlString.Append("               AND A.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            strSqlString.Append("               AND A.WORK_DATE = '" + selectDate[selectDate.Count - 1] + "'" + "\n");
            strSqlString.Append("               AND A.LOT_TYPE  = 'W'" + "\n");
            strSqlString.Append("               AND ( A.OPER LIKE  'A040%' OR  A.OPER LIKE  'A060%'  )" + "\n");
            strSqlString.Append("               AND A.CM_KEY_3 LIKE 'P%'" + "\n");
            strSqlString.Append("              GROUP BY A.MAT_ID, A.OPER  )" + "\n");
            strSqlString.Append("            GROUP BY MAT_ID" + "\n");
            strSqlString.Append("           UNION ALL" + "\n");
            strSqlString.Append("           SELECT '8_예상실적' GUBUN, MAT_ID," + "\n");
            strSqlString.Append("               ROUND( (DA_RST_TTL / NOW_MM * 60*24) )  DA_RST_TTL, ROUND( ( WB_RST_TTL/ NOW_MM * 60*24)) WB_RST_TTL," + "\n");
            strSqlString.Append("               ROUND((DA401 /NOW_MM * 60 * 24)) DA401, ROUND((WB601 /NOW_MM * 60 * 24)) WB601," + "\n");
            strSqlString.Append("               ROUND((DA402 /NOW_MM * 60 * 24)) DA402, ROUND((WB602 /NOW_MM * 60 * 24)) WB602," + "\n");
            strSqlString.Append("               ROUND((DA403 /NOW_MM * 60 * 24)) DA403, ROUND((WB603 /NOW_MM * 60 * 24)) WB603," + "\n");
            strSqlString.Append("               ROUND((DA404 /NOW_MM * 60 * 24)) DA404, ROUND((WB604 /NOW_MM * 60 * 24)) WB604," + "\n");
            strSqlString.Append("               ROUND((DA405 /NOW_MM * 60 * 24)) DA405, ROUND((WB605 /NOW_MM * 60 * 24)) WB605," + "\n");
            strSqlString.Append("               ROUND((DA_Etc/NOW_MM * 60 * 24)) DA_Etc, ROUND((WB_Etc/NOW_MM * 60 * 24)) WB_Etc" + "\n");
            strSqlString.Append("           FROM (  SELECT MAT_ID," + "\n");
            strSqlString.Append("                 SUM( CASE WHEN OPER LIKE 'A040%' THEN  QTY  ELSE 0  END ) DA_RST_TTL ,  SUM( CASE WHEN OPER LIKE 'A060%' THEN  QTY  ELSE 0  END ) WB_RST_TTL," + "\n");
            strSqlString.Append("                 SUM( CASE WHEN OPER IN ( 'A0400', 'A0401') THEN QTY  ELSE 0 END ) DA401 , SUM( CASE WHEN OPER IN ( 'A0600', 'A0601') THEN QTY ELSE 0 END )  WB601," + "\n");
            strSqlString.Append("                 SUM(DECODE(OPER, 'A0402', QTY,  0)) DA402  , SUM(DECODE(OPER, 'A0602', QTY,  0)) WB602," + "\n");
            strSqlString.Append("                 SUM(DECODE(OPER, 'A0403', QTY,  0)) DA403  , SUM(DECODE(OPER, 'A0603', QTY,  0)) WB603," + "\n");
            strSqlString.Append("                 SUM(DECODE(OPER, 'A0404', QTY,  0)) DA404  , SUM(DECODE(OPER, 'A0604', QTY,  0)) WB604," + "\n");
            strSqlString.Append("                 SUM(DECODE(OPER, 'A0405', QTY,  0)) DA405  , SUM(DECODE(OPER, 'A0605', QTY,  0)) WB605," + "\n");
            strSqlString.Append("                 SUM( CASE WHEN OPER IN ( 'A0406',   'A0407',  'A0408',  'A0409') THEN QTY  ELSE 0 END ) DA_Etc," + "\n");
            
            if ( Check_Day.Substring(6).Equals("01") )
                strSqlString.Append("                 SUM( CASE WHEN OPER IN ( 'A0606',   'A0607',  'A0608',  'A0609') THEN  QTY ELSE 0 END ) WB_Etc,  (SYSDATE - TO_DATE('" + Last_Month_Last_day + "220000', 'YYYYMMDDHH24MISS') ) * 24 * 60 NOW_MM " + "\n");
            else strSqlString.Append("                 SUM( CASE WHEN OPER IN ( 'A0606',   'A0607',  'A0608',  'A0609') THEN  QTY ELSE 0 END ) WB_Etc,  (SYSDATE - TO_DATE('" + selectDate[selectDate.Count - 2] + "220000', 'YYYYMMDDHH24MISS') ) * 24 * 60 NOW_MM " + "\n");

            strSqlString.Append("                FROM (  SELECT MAT_ID, OPER, SUM(S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ) QTY" + "\n");
            strSqlString.Append("                    FROM RSUMWIPMOV A  " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                  AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                  AND A.MAT_VER  = 1" + "\n");

            strSqlString.Append("                  AND A.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
             
            strSqlString.Append("                  AND A.WORK_DATE = '" + selectDate[selectDate.Count - 1] + "'" + "\n");
            strSqlString.Append("                  AND A.LOT_TYPE  = 'W'" + "\n");
            strSqlString.Append("                  AND ( A.OPER LIKE  'A040%' OR  A.OPER LIKE  'A060%'  )" + "\n");
            strSqlString.Append("                  AND A.CM_KEY_3 LIKE 'P%'" + "\n");
            strSqlString.Append("                 GROUP BY A.MAT_ID, A.OPER  )" + "\n");
            strSqlString.Append("               GROUP BY MAT_ID  ) ) A,  (SELECT * FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  AND MAT_TYPE = 'FG' AND MAT_VER =  1 AND  DELETE_FLAG <> 'Y' AND MAT_GRP_2 <> '-' ) B" + "\n");
            strSqlString.Append("            WHERE A.MAT_ID = B.MAT_ID(+)" + "\n"); 
                
            strSqlString.Append("            ) A," + "\n");                
                
            strSqlString.Append("           ( SELECT SEQ FROM HRTDSUMSEQ@RPTTOMES WHERE SEQ < 9 ) C, " + "\n");

            strSqlString.Append("           (  SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER LIKE 'A040%' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0) ELSE 0 END) DA_TTL," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER LIKE 'A060%' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) WB_TTL," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER IN ('A0400', 'A0401') THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA1," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0402' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA2," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0403' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA3," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0404' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA4," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0405' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA5," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER IN ('A0600', 'A0601') THEN NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) WB1," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0602' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0) ELSE 0 END) WB2," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0603' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0) ELSE 0 END) WB3," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0604' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) WB4," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER =  'A0605' THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) WB5," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER IN ('A0406', 'A0407', 'A0408', 'A0409') THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) DA_ETC," + "\n");
            strSqlString.Append("                            SUM(CASE WHEN OPER IN ('A0606', 'A0607', 'A0608', 'A0609') THEN  NVL(S1_END_QTY_1,0) + NVL(S2_END_QTY_1,0) + NVL(S3_END_QTY_1,0)  ELSE 0 END) WB_ETC " + "\n");
            strSqlString.Append("                 FROM RSUMWIPMOV A," + "\n");
            strSqlString.Append("                      (SELECT *  FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  AND MAT_TYPE = 'FG' AND MAT_VER =  1 AND  DELETE_FLAG <> 'Y' AND MAT_GRP_2 <> '-' ) MAT" + "\n");
            strSqlString.Append("               WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                    AND A.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                    AND A.CM_KEY_2 = 'PROD'" + "\n");
            strSqlString.Append("                    AND A.MAT_VER = 1 " + "\n");
            strSqlString.Append("                    AND ( A.OPER LIKE 'A040%'  OR  A.OPER LIKE 'A060%' )" + "\n");
            strSqlString.Append("                    AND A.WORK_DATE = '" + selectDate[selectDate.Count - 1] + "' AND A.MAT_ID = MAT.MAT_ID(+)" + "\n");
            strSqlString.Append("              GROUP BY MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11   )  RCV" + "\n");
            strSqlString.Append("     WHERE  A.MAT_GRP_1 = RCV.MAT_GRP_1(+)" + "\n");
            strSqlString.Append("       AND A.MAT_GRP_9 = RCV.MAT_GRP_9(+)" + "\n");
            strSqlString.Append("       AND  A.MAT_GRP_10 =RCV.MAT_GRP_10(+)" + "\n");
            strSqlString.Append("       AND  A.MAT_GRP_6=RCV.MAT_GRP_6(+)" + "\n");
            strSqlString.Append("       AND A.MAT_CMF_11=RCV.MAT_CMF_11(+)" + "\n");
            
            //상세 조회에 따른 SQL문 생성                        

            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);
            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);
            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);
            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);
            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);
            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);
            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            
            strSqlString.Append(" GROUP BY A.MAT_GRP_1, A.MAT_GRP_9 , A.MAT_GRP_10, DECODE(SEQ, 1, 'SOP 잔량', 2, '일목표', 3, '소요대수', 4, 'CAPA', 5, 'CHIP', 6, 'MERGE', 7, '실적', '예상실적'), SEQ   )" + "\n");
            strSqlString.Append(" GROUP BY GUBUN, GUBUN_SEQ ) " + "\n");
          
            strSqlString.Append(" WHERE CUSTOMER IS NOT NULL" + "\n");

            strSqlString.Append(" ORDER BY DECODE(CUSTOMER, 'ZZZZ', 1, 'SE', 2, 'HX', 3, 'IM', 4, 'FC', 5, 6)," + QueryCond4 + ", GUBUN_SEQ )" + "\n");

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

        #endregion

        #region EventHandler

        private void btnView_Click(object sender, EventArgs e)
        {

            if (CheckField() == false) return;

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

                //2. 칼럼 고정(필요하다면..)
                spdData.Sheets[0].FrozenColumnCount = 4;
                spdData.Sheets[0].FrozenRowCount = 8;

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    if (i > 7 && !spdData.ActiveSheet.Cells[i, 2].Text.Equals("SUB TOTAL") && !spdData.ActiveSheet.Cells[i, 1].Text.Equals("CUSTOMER TOTAL")) spdData.ActiveSheet.Cells[i, 0, i, 4].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(204)));
                }
                // total 컬럼 색 지정
                spdData.ActiveSheet.Rows[0].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
                spdData.ActiveSheet.Rows[1].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
                spdData.ActiveSheet.Rows[2].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
                spdData.ActiveSheet.Rows[3].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
                spdData.ActiveSheet.Rows[4].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
                spdData.ActiveSheet.Rows[5].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
                spdData.ActiveSheet.Rows[6].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
                spdData.ActiveSheet.Rows[7].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
             
                /*for (int i = 0; i < 8; i++)
                {
                    if (checkLDcnt.Checked == false) spdData.ActiveSheet.Cells[i, 0].ColumnSpan = 3;
                    else spdData.ActiveSheet.Cells[i, 0].ColumnSpan = 5;

                }
             
                if (checkLDcnt.Checked == false) spdData.ActiveSheet.Cells[0, 0, 0, 2].RowSpan = 8;
                else spdData.ActiveSheet.Cells[0, 0, 0, 3].RowSpan = 8;
                */

                if (spdData.ActiveSheet.Columns[3].Visible == true && spdData.ActiveSheet.Columns[4].Visible == false)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        spdData.ActiveSheet.Cells[i, 0].ColumnSpan = 4;
                    }
                    spdData.ActiveSheet.Cells[0, 0, 0, 3].RowSpan = 8;
                }
                else if (spdData.ActiveSheet.Columns[3].Visible == true && spdData.ActiveSheet.Columns[4].Visible == true)
                {
                    for (int i = 0; i < 8; i++)
                    {
                       spdData.ActiveSheet.Cells[i, 0].ColumnSpan = 5;

                    }
                    spdData.ActiveSheet.Cells[0, 0, 0, 4].RowSpan = 8;
                }
                else if (spdData.ActiveSheet.Columns[3].Visible == false && spdData.ActiveSheet.Columns[4].Visible == false)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        spdData.ActiveSheet.Cells[i, 0].ColumnSpan = 3;
                    }
                    spdData.ActiveSheet.Cells[0, 0, 0, 2].RowSpan = 8;
                }

                // LD CNT가 체크되지 않을때 4번 컬럼 숨기기.
                //if (checkLDcnt.Checked == false && spdData.ActiveSheet.Rows.Count > 0)
                //    spdData.ActiveSheet.Columns[4].Visible = false;

                // sub total 컬럼 색 지정
                for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    for (int j = 0; j < 11; j++)
                    {
                        if (spdData.ActiveSheet.Cells[i, j].Text.Equals("SUB TOTAL")) spdData.ActiveSheet.Rows[i].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));
                        if (spdData.ActiveSheet.Cells[i, j].Text.Equals("CUSTOMER TOTAL")) spdData.ActiveSheet.Rows[i].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));
                    }
                }

               
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
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel("Chip Arrage 현황");            
        }

        #endregion

        #region "Imitate Excel Sum"
        private bool IsNumeric(string value)
        {
            value = value.Trim();
            value = value.Replace(".", "");

            foreach (char cData in value)
            {
                if (false == Char.IsNumber(cData))
                {
                    return false;
                }
            }
            return true;
        }

        private void spdData_KeyUp(object sender, KeyEventArgs e)
        {
            if (spdData.ActiveSheet.RowCount <= 0) return;

            FarPoint.Win.Spread.Model.CellRange crRange;

            try
            {
                crRange = spdData.ActiveSheet.GetSelection(0);
                spdData.ActiveSheet.GetClip(crRange.Row, crRange.Column, crRange.RowCount, crRange.ColumnCount);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "You didn't make a selection!!");
                return;
            }

            double dblSum = 0; long lngCnt = 0;
            for (int ii = crRange.Row; ii < crRange.Row + crRange.RowCount; ii++)
            {
                for (int kk = crRange.Column; kk < crRange.Column + crRange.ColumnCount; kk++)
                {
                    string strTmpValue = spdData.ActiveSheet.Cells[ii, kk].Text;
                    strTmpValue = strTmpValue.Replace(",", "");
                    if (strTmpValue.Trim() == "") strTmpValue = "0";

                    if (this.IsNumeric(strTmpValue))
                    {
                        dblSum += double.Parse(strTmpValue);
                    }
                    else
                    {
                        lblNumericSum.Text = "Characters included.";
                        return;
                    }
                    lngCnt++;
                }
            }

            if (lngCnt > 0 && dblSum != 0)
                lblNumericSum.Text = "개수: " + lngCnt.ToString("#,###").Trim() + "    합계: " + dblSum.ToString("#,###").Trim();
            else
                lblNumericSum.Text = "";
        }

        private void spdData_MouseUp(object sender, MouseEventArgs e)
        {
            spdData_KeyUp(sender, new KeyEventArgs(Keys.Return));
        }
        #endregion

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string colHeaderStepStr;
            string colHeaderChipStr;
            string rowHeaderCustomStr;
            string rowHeaderMajorStr;
            string rowHeaderPKGStr;
            string rowHeaderLDStr;
            string rowHeaderGubunStr;




            if (pnlEqp.Visible == true) pnlEqp.Visible = false;
            if (pnlWip.Visible == true) pnlWip.Visible = false;

            try
            {
                if (e.Row > 0)
                {
                    colHeaderStepStr = spdData.Sheets[0].ColumnHeader.Cells[1, e.Column].Text;
                    colHeaderChipStr = "";

                    rowHeaderCustomStr = spdData.Sheets[0].Cells[e.Row, 0].Text;
                    rowHeaderMajorStr = spdData.Sheets[0].Cells[e.Row, 1].Text;
                    rowHeaderPKGStr = spdData.Sheets[0].Cells[e.Row, 2].Text;
                    rowHeaderLDStr = spdData.Sheets[0].Cells[e.Row, 3].Text;
                    rowHeaderGubunStr = spdData.Sheets[0].Cells[e.Row, 5].Text;

                    if (colHeaderStepStr.Equals("W/B")) colHeaderChipStr = spdData.Sheets[0].ColumnHeader.Cells[0, e.Column - 1].Text;
                    else if (colHeaderStepStr.Equals("D/A")) colHeaderChipStr = spdData.Sheets[0].ColumnHeader.Cells[0, e.Column].Text;

                    if (!rowHeaderPKGStr.Equals("SUB TOTAL") && !rowHeaderPKGStr.Equals("CUSTOMER TOTAL") && !rowHeaderPKGStr.Equals("TOTAL"))
                    {
                        if (colHeaderChipStr.Equals("TTL") || colHeaderChipStr.Equals("Primary Chip") || colHeaderChipStr.Equals("2nd Chip") || colHeaderChipStr.Equals("3rd Chip") || colHeaderChipStr.Equals("4th Chip") || colHeaderChipStr.Equals("5th Chip"))
                            showThePopup(rowHeaderCustomStr, rowHeaderMajorStr, rowHeaderPKGStr, rowHeaderGubunStr, colHeaderChipStr, colHeaderStepStr);
                    }
                }
            }
            catch (Exception)
            {
                pnlEqp.Visible = false;
                pnlWip.Visible = false;
            }
            
           
        }

        //POPUP 쿼리 및 WINDOW 실행.
        private void showThePopup(string rowCustomStr, string rowMajorStr, string rowPKGStr, string rowGubunStr, string colChipStr, string colStepStr)
        {
            DataTable dtPop = null;
            FarPoint.Win.Spread.FpSpread spdTemp = new FarPoint.Win.Spread.FpSpread();
            //int sizeWidth;
            //int sizeHeight;
            
            popGridColumnInit(rowPKGStr, rowGubunStr, colChipStr, colStepStr);
            
            try
            {
                //"Arange 댓수", "CAPA", "CHIP", "MERGE" 만 수행.
                if (rowGubunStr.Equals("Arange 댓수") || rowGubunStr.Equals("CAPA") || rowGubunStr.Equals("CHIP") || rowGubunStr.Equals("MERGE") ) 
                {
                    LoadingPopUp.LoadIngPopUpShow(this);
                    spdData_Eqp_Pos.Sheets[0].RowCount = 0;
                    spdData_Wip_Pos.Sheets[0].RowCount = 0; 
                
                    this.Refresh();

                    if (rowGubunStr.Equals("Arange 댓수") || rowGubunStr.Equals("CAPA"))
                    {
                        if (colChipStr.Equals("TTL"))
                            dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", popupWindowEQPSQL(rowCustomStr, rowMajorStr, rowPKGStr, colStepStr));
                        else if (!colChipStr.Equals("TTL") && !colChipStr.Equals("Etc Chip"))
                        {
                            if (colStepStr.Equals("D/A"))                                      //(string rowCustomStr, string rowMajorStr, string rowPKGStr, string colChipStr)
                                dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", popupWindowDAEQPSQL(rowCustomStr, rowMajorStr, rowPKGStr, colChipStr));
                            else dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", popupWindowWBEQPSQL(rowCustomStr, rowMajorStr, rowPKGStr, colChipStr));
                        }

                        if (dtPop != null && dtPop.Rows.Count > 0)
                        {
                            LoadingPopUp.LoadingPopUpHidden();
                            System.Windows.Forms.Form frm = new PRD011009_P1("", dtPop);
                            frm.ShowDialog();
                        }
                    }
                    else if (rowGubunStr.Equals("CHIP") || rowGubunStr.Equals("MERGE"))
                    {
                        if (colChipStr.Equals("TTL"))
                        {
                            if (rowGubunStr.Equals("CHIP"))
                                dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", popupWindowChipTTLSQL(rowCustomStr, rowMajorStr, rowPKGStr, rowGubunStr, colChipStr, colStepStr));
                            else if (rowGubunStr.Equals("MERGE"))
                                dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", popupWindowMergeTTLSQL(rowCustomStr, rowMajorStr, rowPKGStr, rowGubunStr, colChipStr, colStepStr));

                            if (dtPop != null && dtPop.Rows.Count > 0)
                            {
                                LoadingPopUp.LoadingPopUpHidden();
                                System.Windows.Forms.Form frm = new PRD011009_P3("", dtPop);
                                frm.ShowDialog();
                            }
                        }
                        else if (!colChipStr.Equals("TTL") && !colChipStr.Equals("Etc Chip"))
                        {
                            if (rowGubunStr.Equals("CHIP") && colStepStr.Equals("D/A"))
                                dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", popupWindowChipDASQL(rowCustomStr, rowMajorStr, rowPKGStr, rowGubunStr, colChipStr, colStepStr));
                            else if (rowGubunStr.Equals("CHIP") && colStepStr.Equals("W/B"))
                                dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", popupWindowChipWBSQL(rowCustomStr, rowMajorStr, rowPKGStr, rowGubunStr, colChipStr, colStepStr));
                            else if (rowGubunStr.Equals("MERGE") && colStepStr.Equals("D/A"))
                                dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", popupWindowMergeDASQL(rowCustomStr, rowMajorStr, rowPKGStr, rowGubunStr, colChipStr, colStepStr));
                            else if (rowGubunStr.Equals("MERGE") && colStepStr.Equals("W/B"))
                                dtPop = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", popupWindowMergeWBSQL(rowCustomStr, rowMajorStr, rowPKGStr, rowGubunStr, colChipStr, colStepStr));

                            if (dtPop != null && dtPop.Rows.Count > 0)
                            {
                                LoadingPopUp.LoadingPopUpHidden();
                                System.Windows.Forms.Form frm = new PRD011009_P2(colChipStr, colStepStr, dtPop);
                                frm.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        LoadingPopUp.LoadingPopUpHidden();
                        return;
                    }
                   
                    if (dtPop.Rows.Count == 0)
                    {
                        dtPop.Dispose();
                        LoadingPopUp.LoadingPopUpHidden();
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("PRD011009", GlobalVariable.gcLanguage));
                        return;
                    }
                    /*
                    if (rowGubunStr.Equals("Arange 댓수") || rowGubunStr.Equals("CAPA")) spdTemp = spdData_Eqp_Pos;
                    else spdTemp = spdData_Wip_Pos;
                   
                    spdTemp.Sheets[0].RowCount = dtPop.Rows.Count;

                    for (int irow = 0; irow < dtPop.Rows.Count; irow++)
                    {
                        for (int icol = 0; icol < dtPop.Columns.Count; icol++)
                        {
                            spdTemp.Sheets[0].Cells[irow, icol].Value = Convert.ToString(dtPop.Rows[irow][icol].ToString()); 
                        }
                    }

                    if (rowGubunStr.Equals("Arange 댓수") || rowGubunStr.Equals("CAPA"))  pnlEqp.Visible = false;
                    else
                    {
                        if (colChipStr.Equals("TTL"))
                        {
                            sizeWidth = spdData.Width;
                            sizeHeight = spdData.Height;
                            pnlWip.Height = (sizeHeight / 2);
                            pnlWip.Width = spdWipSheetWidth + (sizeWidth / 3);
                        }
                        else
                        {
                            pnlWip.Width = spdWipSheetWidth ;
                            pnlWip.Height = spdWipSheetHeight;
                        }

                        spdData_Wip_Pos.Sheets[0].SetRowMerge(-1, FarPoint.Win.Spread.Model.MergePolicy.None);
                        spdData_Wip_Pos.Sheets[0].SetColumnMerge(-1, FarPoint.Win.Spread.Model.MergePolicy.Always);
                        spdData_Wip_Pos.SetBounds(spdData_Wip_Pos.Bounds.X, spdData_Wip_Pos.Bounds.Y, spdData_Wip_Pos.Bounds.Width + 1, spdData_Wip_Pos.Bounds.Height);
                        pnlWip.Visible = false;
                    }
                     */
                }

                
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

        private void popGridColumnInit(string rowPKGStr, string rowGubunStr, string colChipStr, string colStepStr)
        {
            spdData_Wip_Pos.ActiveSheet.ColumnCount = 6;

            if (rowGubunStr == "CHIP" || rowGubunStr == "MERGE")
            {
                    if (colChipStr != "TTL")
                    {

                        if (colStepStr == "D/A")
                        {
                            spdData_Wip_Pos.ActiveSheet.ColumnCount = 7;
                            spdData_Wip_Pos.ActiveSheet.Columns[6].CellType = _number;
                            spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[0, 6].Text = colStepStr + " " + colChipStr.Substring(0, 3);
                            spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 6].Text = "A040" + colChipStr.Substring(0, 1);
                        }
                        else if (colStepStr == "W/B")
                        {
                                spdData_Wip_Pos.ActiveSheet.ColumnCount = 9;
                                spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[0, 6].ColumnSpan = 3;
                                spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[0, 6].Text = colStepStr + " " + colChipStr.Substring(0, 3);
                                spdData_Wip_Pos.ActiveSheet.Columns[6].CellType = _number;
                                spdData_Wip_Pos.ActiveSheet.Columns[7].CellType = _number;
                                spdData_Wip_Pos.ActiveSheet.Columns[8].CellType = _number;
                       
                                spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 6].Text = "A050" + colChipStr.Substring(0, 1);
                                spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 7].Text = "A055" + colChipStr.Substring(0, 1);
                                spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 8].Text = "A060" + colChipStr.Substring(0, 1);
                        }
                    }
                    else
                    {
                        spdData_Wip_Pos.ActiveSheet.ColumnCount = 26;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[0, 6].ColumnSpan = 20;
                        spdData_Wip_Pos.ActiveSheet.Columns[6].CellType = _number;
                        spdData_Wip_Pos.ActiveSheet.Columns[7].CellType = _number;
                        spdData_Wip_Pos.ActiveSheet.Columns[8].CellType = _number;
                        spdData_Wip_Pos.ActiveSheet.Columns[9].CellType = _number;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[0, 6].Text = " TTL " ;

                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 6].Text = "A0401" ;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 7].Text = "A0501" ;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 8].Text = "A0551" ;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 9].Text = "A0601" ;

                        spdData_Wip_Pos.ActiveSheet.Columns[10].CellType = _number;
                        spdData_Wip_Pos.ActiveSheet.Columns[11].CellType = _number;
                        spdData_Wip_Pos.ActiveSheet.Columns[12].CellType = _number;
                        spdData_Wip_Pos.ActiveSheet.Columns[13].CellType = _number;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 10].Text = "A0402" ;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 11].Text = "A0502" ;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 12].Text = "A0552" ;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 13].Text = "A0602" ;

                        spdData_Wip_Pos.ActiveSheet.Columns[14].CellType = _number;
                        spdData_Wip_Pos.ActiveSheet.Columns[15].CellType = _number;
                        spdData_Wip_Pos.ActiveSheet.Columns[16].CellType = _number;
                        spdData_Wip_Pos.ActiveSheet.Columns[17].CellType = _number;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 14].Text = "A0403" ;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 15].Text = "A0503" ;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 16].Text = "A0553" ;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 17].Text = "A0603" ;

                        spdData_Wip_Pos.ActiveSheet.Columns[18].CellType = _number;
                        spdData_Wip_Pos.ActiveSheet.Columns[19].CellType = _number;
                        spdData_Wip_Pos.ActiveSheet.Columns[20].CellType = _number;
                        spdData_Wip_Pos.ActiveSheet.Columns[21].CellType = _number;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 18].Text = "A0404" ;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 19].Text = "A0504" ;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 20].Text = "A0554" ;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 21].Text = "A0604" ;

                        spdData_Wip_Pos.ActiveSheet.Columns[22].CellType = _number;
                        spdData_Wip_Pos.ActiveSheet.Columns[23].CellType = _number;
                        spdData_Wip_Pos.ActiveSheet.Columns[24].CellType = _number;
                        spdData_Wip_Pos.ActiveSheet.Columns[25].CellType = _number;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 22].Text = "A0405" ;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 23].Text = "A0505" ;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 24].Text = "A0555" ;
                        spdData_Wip_Pos.Sheets[0].ColumnHeader.Cells[1, 25].Text = "A0605" ;

                    }
            }
            
            
        }

        //USER가 DATA Sheet에서 선택한 컬럼이 TTL이 아닐 경우 SQL생성하는 함수.
        private string popupWindowChipTTLSQL(string rowCustomStr, string rowMajorStr, string rowPKGStr, string rowGubunStr, string colChipStr, string colStepStr)
        {
            StringBuilder sqlString = new StringBuilder();
            string strCustomer = "";

            switch (rowCustomStr)
            {
                case "SEC":
                    strCustomer = "SE";
                    break;
                case "HYNIX":
                    strCustomer = "HX";
                    break;
                case "iML":
                    strCustomer = "IM";
                    break;
                case "FCI":
                    strCustomer = "FC";
                    break;
                case "IMAGIS":
                    strCustomer = "IG";
                    break;
                default:
                    strCustomer = "-";
                    break;
            }

             //sqlString.Append("SELECT  CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11," + "\n");
             sqlString.Append("SELECT  MAT_GRP_10, MAT_GRP_6, MAT_CMF_11," + "\n");
             sqlString.Append(" DECODE(TTLSEQ.SEQ,1,'WIP_QTY',2,'LOT_CNT',3,'RUN_QTY','WAIT_QTY') GUBUN," + "\n");
             sqlString.Append(" SUM(DECODE(TTLSEQ.SEQ,TO_NUMBER(SUBSTR(GUBUN,1,1)),NVL(DA_1,0),0)) DA_1," + "\n");
             sqlString.Append(" SUM(DECODE(TTLSEQ.SEQ,TO_NUMBER(SUBSTR(GUBUN,1,1)),NVL(DA_2,0),0)) DA_2," + "\n");
             sqlString.Append(" SUM(DECODE(TTLSEQ.SEQ,TO_NUMBER(SUBSTR(GUBUN,1,1)),NVL(WB_1,0),0)) WB_1," + "\n");
             sqlString.Append(" SUM(DECODE(TTLSEQ.SEQ,TO_NUMBER(SUBSTR(GUBUN,1,1)),NVL(WB_2,0),0)) WB_2," + "\n");
             sqlString.Append(" SUM(DECODE(TTLSEQ.SEQ,TO_NUMBER(SUBSTR(GUBUN,1,1)),NVL(DA_3,0),0)) DA_3," + "\n");
             sqlString.Append(" SUM(DECODE(TTLSEQ.SEQ,TO_NUMBER(SUBSTR(GUBUN,1,1)),NVL(DA_4,0),0)) DA_4," + "\n");
             sqlString.Append(" SUM(DECODE(TTLSEQ.SEQ,TO_NUMBER(SUBSTR(GUBUN,1,1)),NVL(WB_3,0),0)) WB_3," + "\n");
             sqlString.Append(" SUM(DECODE(TTLSEQ.SEQ,TO_NUMBER(SUBSTR(GUBUN,1,1)),NVL(WB_4,0),0)) WB_4," + "\n");
             sqlString.Append(" SUM(DECODE(TTLSEQ.SEQ,TO_NUMBER(SUBSTR(GUBUN,1,1)),NVL(DA_5,0),0)) DA_5," + "\n");
             sqlString.Append(" SUM(DECODE(TTLSEQ.SEQ,TO_NUMBER(SUBSTR(GUBUN,1,1)),NVL(DA_6,0),0)) DA_6," + "\n");
             sqlString.Append(" SUM(DECODE(TTLSEQ.SEQ,TO_NUMBER(SUBSTR(GUBUN,1,1)),NVL(WB_5,0),0)) WB_5," + "\n");
             sqlString.Append(" SUM(DECODE(TTLSEQ.SEQ,TO_NUMBER(SUBSTR(GUBUN,1,1)),NVL(WB_6,0),0)) WB_6," + "\n");
             sqlString.Append(" SUM(DECODE(TTLSEQ.SEQ,TO_NUMBER(SUBSTR(GUBUN,1,1)),NVL(DA_7,0),0)) DA_7," + "\n");
             sqlString.Append(" SUM(DECODE(TTLSEQ.SEQ,TO_NUMBER(SUBSTR(GUBUN,1,1)),NVL(DA_8,0),0)) DA_8," + "\n");
             sqlString.Append(" SUM(DECODE(TTLSEQ.SEQ,TO_NUMBER(SUBSTR(GUBUN,1,1)),NVL(WB_7,0),0)) WB_7," + "\n");
             sqlString.Append(" SUM(DECODE(TTLSEQ.SEQ,TO_NUMBER(SUBSTR(GUBUN,1,1)),NVL(WB_8,0),0)) WB_8," + "\n");
             sqlString.Append(" SUM(DECODE(TTLSEQ.SEQ,TO_NUMBER(SUBSTR(GUBUN,1,1)),NVL(DA_9,0),0)) DA_9," + "\n");
             sqlString.Append(" SUM(DECODE(TTLSEQ.SEQ,TO_NUMBER(SUBSTR(GUBUN,1,1)),NVL(DA_10,0),0)) DA_10," + "\n");
             sqlString.Append(" SUM(DECODE(TTLSEQ.SEQ,TO_NUMBER(SUBSTR(GUBUN,1,1)),NVL(WB_9,0),0)) WB_9," + "\n");
             sqlString.Append(" SUM(DECODE(TTLSEQ.SEQ,TO_NUMBER(SUBSTR(GUBUN,1,1)),NVL(WB_10,0),0)) WB_10" + "\n");
             sqlString.Append(" FROM(" + "\n");
             sqlString.Append(" SELECT A.MAT_GRP_1 CUS,A.MAT_GRP_9,A.MAT_GRP_10,A.MAT_GRP_6,A.MAT_CMF_11,GUBUN" + "\n");
             sqlString.Append(" ,0 DA_1,0 DA_2" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_3,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0)END),0))) AS DA_3" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN(SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_4,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0)END),0))) AS DA_4" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_5,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0)END),0))) AS DA_5" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_6,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_6,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_6,0)END),0))) AS DA_6" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_7,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_7,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_7,0)END),0))) AS DA_7" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_8,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_8,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_8,0)END),0))) AS DA_8" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_9,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_9,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_9,0)END),0))) AS DA_9" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "'AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_10,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_10,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_10,0)END),0))) AS DA_10,0 WB_1,0 WB_2,0 WB_3,0 WB_4,0 WB_5,0 WB_6,0 WB_7,0 WB_8,0 WB_9,0 WB_10" + "\n");
             sqlString.Append(" FROM MWIPMATDEF A," + "\n");
             sqlString.Append(" ( SELECT '1_WIP_QTY' GUBUN, LOT.MAT_ID" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'S/P',DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN', ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),QTY),0))SP_1,0 DA_1,0 DA_2" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0402') AND MAT.MAT_GRP_5='2nd' THEN" + "\n");
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0))DA_3" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0502','A0532') AND MAT.MAT_GRP_5='2nd' THEN" + "\n");   
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0))DA_4" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0403') AND MAT.MAT_GRP_5='3rd' THEN" + "\n");    
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0))DA_5" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0503','A0533') AND MAT.MAT_GRP_5='3rd' THEN" + "\n");    
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0))DA_6" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0404') AND MAT.MAT_GRP_5='4th' THEN" + "\n");    
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0))DA_7" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0504','A0534') AND MAT.MAT_GRP_5='4th' THEN" + "\n");    
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0))DA_8" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0405') AND MAT.MAT_GRP_5='5th' THEN" + "\n");    
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0))DA_9" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0505','A0535') AND MAT.MAT_GRP_5='5th' THEN" + "\n");    
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0))DA_10" + "\n");
             sqlString.Append(" ,0 WB_1,0 WB_2,0 WB_3,0 WB_4,0 WB_5,0 GATE_1,0 GATE_2,0 GATE_3,0 GATE_4,0 GATE_5" + "\n");
             sqlString.Append(" FROM ( SELECT A.FACTORY,A.MAT_ID,B.OPER,B.OPER_GRP_1,SUM(A.QTY_1) QTY" + "\n");
             sqlString.Append(" FROM RWIPLOTSTS A,MWIPOPRDEF B" + "\n");
             sqlString.Append(" WHERE A.FACTORY = B.FACTORY(+)" + "\n");
             sqlString.Append(" AND A.OPER = B.OPER(+)" + "\n");
             sqlString.Append(" AND A.FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
             sqlString.Append(" AND A.LOT_TYPE ='W'" + "\n");
             sqlString.Append(" AND A.LOT_CMF_5 LIKE'P%'" + "\n");
             sqlString.Append(" AND A.LOT_DEL_FLAG = ' '" + "\n");
             sqlString.Append(" GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT,MWIPMATDEF MAT" + "\n");
             sqlString.Append(" WHERE LOT.FACTORY = MAT.FACTORY" + "\n");
             sqlString.Append(" AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
             sqlString.Append(" AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
             sqlString.Append(" AND MAT.DELETE_FLAG <> 'Y'" + "\n");
             sqlString.Append(" AND MAT.MAT_GRP_2 <> '-'" + "\n");
             sqlString.Append(" GROUP BY LOT.MAT_ID)B," + "\n");
             sqlString.Append(" ( SELECT KEY_1,DATA_1" + "\n");
             sqlString.Append(" FROM MGCMTBLDAT" + "\n");
             sqlString.Append(" WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
             sqlString.Append(" AND TABLE_NAME IN('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')) G" + "\n");
             sqlString.Append(" WHERE B.MAT_ID = A.MAT_ID(+)" + "\n");
             sqlString.Append(" AND B.MAT_ID = G.KEY_1(+)" + "\n");
             sqlString.Append(" AND B.MAT_ID LIKE '%'" + "\n");
             sqlString.Append(" AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
             sqlString.Append(" GROUP BY A.MAT_GRP_1,A.MAT_GRP_9,A.MAT_GRP_10,A.MAT_GRP_6,A.MAT_CMF_11,GUBUN" + "\n");
             sqlString.Append(" UNION ALL" + "\n");
             sqlString.Append(" SELECT CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11, GUBUN," + "\n");
             sqlString.Append(" SUM(CASE WHEN OPER IN ('A0400','A0401')THEN LOT_CNT ELSE 0 END) DA_1," + "\n");
             sqlString.Append(" SUM(CASE WHEN OPER IN ('A0500','A0530','A0501','A0531') THEN LOT_CNT ELSE 0 END)DA_2," + "\n");
             sqlString.Append(" SUM(CASE WHEN OPER IN ('A0402')THEN LOT_CNT ELSE 0 END) DA_3," + "\n");
             sqlString.Append(" SUM(CASE WHEN OPER IN ('A0502','A0532')THEN LOT_CNT ELSE 0 END) DA_4," + "\n");
             sqlString.Append(" SUM(CASE WHEN OPER IN ('A0403')THEN LOT_CNT ELSE 0 END) DA_5," + "\n");
             sqlString.Append(" SUM(CASE WHEN OPER IN ('A0503','A0533')THEN LOT_CNT ELSE 0 END) DA_6," + "\n");
             sqlString.Append(" SUM(CASE WHEN OPER IN ('A0404')THEN LOT_CNT ELSE 0 END) DA_7," + "\n");
             sqlString.Append(" SUM(CASE WHEN OPER IN ('A0504','A0534')THEN LOT_CNT ELSE 0 END) DA_8," + "\n");
             sqlString.Append(" SUM(CASE WHEN OPER IN ('A0405')THEN LOT_CNT ELSE 0 END) DA_9," + "\n");
             sqlString.Append(" SUM(CASE WHEN OPER IN ('A0505','A0535')THEN LOT_CNT ELSE 0 END) DA_10," + "\n");
             sqlString.Append(" 0 WB_1,0 WB_2,0 WB_3,0 WB_4,0 WB_5,0 WB_6,0 WB_7,0 WB_8,0 WB_9,0 WB_10" + "\n");
             sqlString.Append(" FROM ( SELECT '2_LOT_QTY' GUBUN,MAT.MAT_GRP_1 CUS,MAT.MAT_GRP_9,MAT.MAT_GRP_10,MAT.MAT_GRP_6,MAT.MAT_CMF_11,LOT.MAT_ID,LOT.OPER,COUNT(LOT.LOT_ID) LOT_CNT" + "\n");
             sqlString.Append(" FROM RWIPLOTSTS LOT,MWIPMATDEF MAT" + "\n");
             sqlString.Append(" WHERE LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
             sqlString.Append(" AND LOT.LOT_TYPE = 'W'" + "\n");
             sqlString.Append(" AND LOT.LOT_CMF_5 LIKE 'P%'" + "\n");
             sqlString.Append(" AND LOT.LOT_DEL_FLAG = ' '" + "\n");
             sqlString.Append(" AND ( LOT.OPER LIKE 'A040%' OR LOT.OPER LIKE 'A050%' OR LOT.OPER LIKE 'A055%' OR LOT.OPER LIKE 'A060%')" + "\n");
             sqlString.Append(" AND LOT.FACTORY = MAT.FACTORY" + "\n");
             sqlString.Append(" AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
             sqlString.Append(" AND MAT.MAT_TYPE = 'FG'" + "\n");
             sqlString.Append(" AND MAT.DELETE_FLAG = ' '" + "\n");
             sqlString.Append(" AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
             sqlString.Append(" GROUP BY MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11, LOT.MAT_ID, LOT.OPER )" + "\n");
             sqlString.Append(" GROUP BY CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11, GUBUN" + "\n");
             sqlString.Append(" UNION ALL" + "\n");
             sqlString.Append(" SELECT  A.MAT_GRP_1 CUS, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");
             sqlString.Append(" ,0 DA_1,0 DA_2" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_3,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0)END),0))) AS DA_3" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN(SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_4,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0)END),0))) AS DA_4" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_5,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0)END),0))) AS DA_5" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_6,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_6,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_6,0)END),0))) AS DA_6" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_7,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_7,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_7,0)END),0))) AS DA_7" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_8,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_8,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_8,0)END),0))) AS DA_8" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_9,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_9,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_9,0)END),0))) AS DA_9" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "'AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_10,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_10,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_10,0)END),0))) AS DA_10,0 WB_1,0 WB_2,0 WB_3,0 WB_4,0 WB_5,0 WB_6,0 WB_7,0 WB_8,0 WB_9,0 WB_10" + "\n");
             sqlString.Append(" FROM MWIPMATDEF A," + "\n"); 
             sqlString.Append(" ( SELECT  '3_WIP_QTY' GUBUN, LOT.MAT_ID" + "\n"); 
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'S/P',DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN', ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),QTY),0))SP_1,0DA_1,0DA_2" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0402') AND MAT.MAT_GRP_5='2nd' THEN" + "\n");
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0)) DA_3" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0502','A0532') AND MAT.MAT_GRP_5='2nd' THEN" + "\n");    
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0)) DA_4" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0403') AND MAT.MAT_GRP_5='3rd' THEN" + "\n");    
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0)) DA_5" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0503','A0533') AND MAT.MAT_GRP_5='3rd' THEN" + "\n");    
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0)) DA_6" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0404') AND MAT.MAT_GRP_5='4th' THEN" + "\n");    
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0)) DA_7" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0504','A0534') AND MAT.MAT_GRP_5='4th' THEN" + "\n");    
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0)) DA_8" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0405') AND MAT.MAT_GRP_5='5th' THEN" + "\n");    
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0)) DA_9" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0505','A0535') AND MAT.MAT_GRP_5='5th' THEN" + "\n");    
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0)) DA_10" + "\n");
             sqlString.Append(" ,0 WB_1,0 WB_2,0 WB_3,0 WB_4,0 WB_5,0 GATE_1,0 GATE_2,0 GATE_3,0 GATE_4,0 GATE_5" + "\n");
             sqlString.Append(" FROM ( SELECT A.FACTORY,A.MAT_ID,B.OPER,B.OPER_GRP_1,SUM(A.QTY_1) QTY" + "\n");
             sqlString.Append(" FROM RWIPLOTSTS A,MWIPOPRDEF B" + "\n");
             sqlString.Append(" WHERE A.FACTORY = B.FACTORY(+)" + "\n");
             sqlString.Append(" AND A.OPER = B.OPER(+)" + "\n");
             sqlString.Append(" AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
             sqlString.Append(" AND A.LOT_TYPE = 'W'" + "\n");
             sqlString.Append(" AND A.LOT_STATUS = 'PROC'" + "\n");
             sqlString.Append(" AND A.LOT_CMF_5 LIKE'P%'" + "\n");
             sqlString.Append(" AND A.LOT_DEL_FLAG = ' '" + "\n");
             sqlString.Append(" GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1) LOT, MWIPMATDEF MAT" + "\n");
             sqlString.Append(" WHERE LOT.FACTORY = MAT.FACTORY" + "\n");
             sqlString.Append(" AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
             sqlString.Append(" AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
             sqlString.Append(" AND MAT.DELETE_FLAG<>'Y'" + "\n");
             sqlString.Append(" AND MAT.MAT_GRP_2<>'-'" + "\n");
             sqlString.Append(" GROUP BY LOT.MAT_ID)B," + "\n");
             sqlString.Append(" (SELECT KEY_1,DATA_1" + "\n");
             sqlString.Append(" FROM MGCMTBLDAT" + "\n");
             sqlString.Append(" WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
             sqlString.Append(" AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')) G" + "\n");
             sqlString.Append(" WHERE B.MAT_ID = A.MAT_ID(+)" + "\n");
             sqlString.Append(" AND B.MAT_ID = G.KEY_1(+)" + "\n");
             sqlString.Append(" AND B.MAT_ID LIKE '%'" + "\n");
             sqlString.Append(" AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
             sqlString.Append(" GROUP BY A.MAT_GRP_1,A.MAT_GRP_9,A.MAT_GRP_10,A.MAT_GRP_6,A.MAT_CMF_11,GUBUN" + "\n");
             sqlString.Append(" UNION ALL" + "\n");    
             sqlString.Append(" SELECT  A.MAT_GRP_1 CUS, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");
             sqlString.Append(" ,0 DA_1,0 DA_2" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_3,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0)END),0))) AS DA_3" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN(SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_4,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0)END),0))) AS DA_4" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_5,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0)END),0))) AS DA_5" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_6,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_6,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_6,0)END),0))) AS DA_6" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_7,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_7,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_7,0)END),0))) AS DA_7" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_8,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_8,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_8,0)END),0))) AS DA_8" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_9,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_9,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_9,0)END),0))) AS DA_9" + "\n");
             sqlString.Append(" ,ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "'AND MAT_GRP_5='1st' AND MAT_ID LIKE 'SEKS%')THEN DECODE(A.MAT_GRP_5,'2nd',NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_10,0),'Merge',NVL(SP_1,0)/NVL(G.DATA_1,1) + NVL(DA_10,0),0)" + "\n");
             sqlString.Append(" ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_10,0)END),0))) AS DA_10,0 WB_1,0 WB_2,0 WB_3,0 WB_4,0 WB_5,0 WB_6,0 WB_7,0 WB_8,0 WB_9,0 WB_10" + "\n");
             sqlString.Append(" FROM MWIPMATDEF A," + "\n");
             sqlString.Append(" ( SELECT '4_WAIT_QTY' GUBUN, LOT.MAT_ID" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'S/P',DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN', ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),QTY),0))SP_1,0DA_1,0DA_2" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0402') AND MAT.MAT_GRP_5='2nd' THEN" + "\n");
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0))DA_3" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0502','A0532') AND MAT.MAT_GRP_5='2nd' THEN" + "\n");    
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0))DA_4" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0403') AND MAT.MAT_GRP_5='3rd' THEN" + "\n"); 
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0))DA_5" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0503','A0533') AND MAT.MAT_GRP_5='3rd' THEN" + "\n");    
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0))DA_6" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0404') AND MAT.MAT_GRP_5='4th' THEN" + "\n");   
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0))DA_7" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0504','A0534') AND MAT.MAT_GRP_5='4th' THEN" + "\n");    
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0))DA_8" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0405') AND MAT.MAT_GRP_5='5th' THEN" + "\n");    
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0))DA_9" + "\n");
             sqlString.Append(" ,SUM(DECODE(OPER_GRP_1,'D/A',CASE WHEN OPER IN('A0505','A0535') AND MAT.MAT_GRP_5='5th' THEN" + "\n");    
             sqlString.Append(" DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,'',1,'-',1,MAT.MAT_CMF_13)),0),'BGN',ROUND( QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-',1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
             sqlString.Append(" ELSE 0 END,0))DA_10" + "\n");
             sqlString.Append(" ,0 WB_1,0 WB_2,0 WB_3,0 WB_4,0 WB_5,0 GATE_1,0 GATE_2,0 GATE_3,0 GATE_4,0 GATE_5" + "\n");
             sqlString.Append(" FROM ( SELECT A.FACTORY,A.MAT_ID,B.OPER,B.OPER_GRP_1,SUM(A.QTY_1) QTY" + "\n");
             sqlString.Append(" FROM RWIPLOTSTS A, MWIPOPRDEF B" + "\n");
             sqlString.Append(" WHERE A.FACTORY = B.FACTORY(+)" + "\n");
             sqlString.Append(" AND A.OPER = B.OPER(+)" + "\n");
             sqlString.Append(" AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
             sqlString.Append(" AND A.LOT_TYPE = 'W'" + "\n");
             sqlString.Append(" AND A.LOT_STATUS = 'WAIT'" + "\n");
             sqlString.Append(" AND A.LOT_CMF_5 LIKE'P%'" + "\n");
             sqlString.Append(" AND A.LOT_DEL_FLAG=' '" + "\n");
             sqlString.Append(" GROUP BY A.FACTORY,A.MAT_ID,B.OPER,B.OPER_GRP_1) LOT, MWIPMATDEF MAT" + "\n");
             sqlString.Append(" WHERE LOT.FACTORY = MAT.FACTORY" + "\n");
             sqlString.Append(" AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
             sqlString.Append(" AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
             sqlString.Append(" AND MAT.DELETE_FLAG <> 'Y'" + "\n");
             sqlString.Append(" AND MAT.MAT_GRP_2 <> '-'" + "\n");
             sqlString.Append(" GROUP BY LOT.MAT_ID) B," + "\n");
             sqlString.Append(" (SELECT KEY_1,DATA_1" + "\n");
             sqlString.Append(" FROM MGCMTBLDAT" + "\n");
             sqlString.Append(" WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
             sqlString.Append(" AND TABLE_NAME IN('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')) G" + "\n");
             sqlString.Append(" WHERE B.MAT_ID = A.MAT_ID(+)" + "\n");
             sqlString.Append(" AND B.MAT_ID = G.KEY_1(+)" + "\n");
             sqlString.Append(" AND B.MAT_ID LIKE '%'" + "\n");
             sqlString.Append(" AND A.FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
             sqlString.Append(" GROUP BY A.MAT_GRP_1,A.MAT_GRP_9,A.MAT_GRP_10,A.MAT_GRP_6,A.MAT_CMF_11,GUBUN)A," + "\n");
             sqlString.Append(" (SELECT SEQ FROM HRTDSUMSEQ@RPTTOMES WHERE SEQ < 5)TTLSEQ" + "\n");
             if (strCustomer.Equals("-"))
             {
                 sqlString.Append(" WHERE CUS = (SELECT KEY_1" + "\n");
                 sqlString.Append(" FROM MGCMTBLDAT@RPTTOMES" + "\n");
                 sqlString.Append(" WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                 sqlString.Append(" AND TABLE_NAME = 'H_CUSTOMER'" + "\n");
                 sqlString.Append(" AND DATA_1 = '" + rowCustomStr + "')" + "\n");
             }
             else sqlString.Append(" WHERE CUS = '" + strCustomer + "'" + "\n");

             sqlString.Append(" AND MAT_GRP_9 = '" + rowMajorStr + "'" + "\n");
             sqlString.Append(" AND MAT_GRP_10 = '" + rowPKGStr + "'" + "\n");

             sqlString.Append(" GROUP BY  CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11," + "\n");
             sqlString.Append(" DECODE(TTLSEQ.SEQ, 1, 'WIP_QTY', 2, 'LOT_CNT', 3, 'RUN_QTY', 'WAIT_QTY')" + "\n");
             sqlString.Append(" ORDER BY  CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11," + "\n"); 
             sqlString.Append(" DECODE(GUBUN, 'WIP_QTY', 1,  'LOT_CNT', 2,  'RUN_QTY', 3, 4)" + "\n");
             

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(sqlString.ToString());
            }

            return sqlString.ToString();

        }

        //USER가 DATA Sheet에서 선택한 컬럼이 TTL이 아닐 경우 SQL생성하는 함수.
        private string popupWindowMergeTTLSQL(string rowCustomStr, string rowMajorStr, string rowPKGStr, string rowGubunStr, string colChipStr, string colStepStr)
        {
            StringBuilder sqlString = new StringBuilder();
            string strCustomer = "";

            switch (rowCustomStr)
            {
                case "SEC":
                    strCustomer = "SE";
                    break;
                case "HYNIX":
                    strCustomer = "HX";
                    break;
                case "iML":
                    strCustomer = "IM";
                    break;
                case "FCI":
                    strCustomer = "FC";
                    break;
                case "IMAGIS":
                    strCustomer = "IG";
                    break;
                default:
                    strCustomer = "-";
                    break;
            }

           // sqlString.Append(" SELECT  CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11,  " + "\n");
            sqlString.Append(" SELECT  MAT_GRP_10, MAT_GRP_6, MAT_CMF_11," + "\n");
            sqlString.Append("            DECODE(TTLSEQ.SEQ, 1, 'WIP_QTY', 2, 'LOT_CNT', 3, 'RUN_QTY', 'WAIT_QTY') GUBUN, " + "\n");
            sqlString.Append("            SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(DA_1, 0), 0 )  )  DA_1," + "\n");
            sqlString.Append("            SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(DA_2, 0), 0 )  )  DA_2," + "\n");
            sqlString.Append("            SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(WB_1, 0), 0 )  )  WB_1," + "\n");
            sqlString.Append("            SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(WB_2, 0), 0 )  )  WB_2," + "\n");
            sqlString.Append("            SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(DA_3, 0), 0 )  )  DA_3," + "\n");
            sqlString.Append("            SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(DA_4, 0), 0 )  )  DA_4," + "\n");
            sqlString.Append("            SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(WB_3, 0), 0 )  )  WB_3," + "\n");
            sqlString.Append("            SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(WB_4, 0), 0 )  )  WB_4," + "\n");
            sqlString.Append("            SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(DA_5, 0), 0 )  )  DA_5," + "\n");
            sqlString.Append("            SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(DA_6, 0), 0 )  )  DA_6," + "\n");
            sqlString.Append("            SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(WB_5, 0), 0 )  )  WB_5," + "\n");
            sqlString.Append("            SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(WB_6, 0), 0 )  )  WB_6," + "\n");
            sqlString.Append("            SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(DA_7, 0), 0 )  )  DA_7," + "\n");
            sqlString.Append("            SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(DA_8, 0), 0 )  )  DA_8," + "\n");
            sqlString.Append("            SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(WB_7, 0), 0 )  )  WB_7," + "\n");
            sqlString.Append("            SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(WB_8, 0), 0 )  )  WB_8," + "\n");
            sqlString.Append("            SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(DA_9, 0), 0 )  )  DA_9," + "\n");
            sqlString.Append("            SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(DA_10, 0), 0 ) ) DA_10," + "\n");
            sqlString.Append("            SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(WB_9, 0), 0 )  )  WB_9," + "\n");
            sqlString.Append("            SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(WB_10, 0), 0 ) )  WB_10" + "\n");
            sqlString.Append("   FROM ( " + "\n");
            sqlString.Append(" SELECT  A.MAT_GRP_1 CUS, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN " + "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_1"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_2"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1,0), 'Merge', NVL(WB_1,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_1,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_1,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_1"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_2+GATE_1,0), 'Merge', NVL(WB_2+GATE_1,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_2+GATE_1,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_2+GATE_1,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_2                      "+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_3"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_4"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_3,0), 'Merge', NVL(WB_3,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_3,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_3,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_3"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_4+GATE_1,0), 'Merge', NVL(WB_4+GATE_1,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_4+GATE_1,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_4+GATE_1,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_4"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_5"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_6,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_6,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_6,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_6,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_6"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_5,0), 'Merge', NVL(WB_5,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_5,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_5,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_5"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_6+GATE_1,0), 'Merge', NVL(WB_6+GATE_1,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_6+GATE_1,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_6+GATE_1,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_6                      "+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_7,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_7,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_7,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_7,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_7"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_8,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_8,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_8,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_8,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_8"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_7,0), 'Merge', NVL(WB_7,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_7,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_7,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_7"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_8+GATE_1,0), 'Merge', NVL(WB_8+GATE_1,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_8+GATE_1,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_8+GATE_1,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_8"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_9,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_9,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_9,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_9,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_9"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_10,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_10,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_10,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_10,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_10"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_9,0), 'Merge', NVL(WB_9,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_9,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_9,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_9"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_10+GATE_1,0), 'Merge', NVL(WB_10+GATE_1,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_10+GATE_1,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_10+GATE_1,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_10"+ "\n");
            sqlString.Append("    FROM MWIPMATDEF A, " + "\n");
            sqlString.Append("              ( SELECT  '1_WIP_QTY' GUBUN, LOT.MAT_ID " + "\n");
            sqlString.Append(" , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0250', 'A0305', 'A0306', 'A0310', 'A0400', 'A0401' ) THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_1"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN (  'A0500', 'A0501', 'A0530', 'A0531' ) THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_2"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0402') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_3"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ( 'A0502', 'A0532') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_4"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0403') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_5"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ( 'A0503', 'A0533') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_6"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0404') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_7"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0504', 'A0534') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_8"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0405') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_9"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ( 'A0505', 'A0535') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_10"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0550', 'A0551') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_1"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0600','A0601' ) THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_2"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0552') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_3"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A0602') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_4                 "+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0553') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_5"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A0603') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_6"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0554') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_7"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A0604') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_8"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0555') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_9"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0605') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_10"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0800', 'A0801') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) GATE_1"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0802') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) GATE_2"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0803') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) GATE_3"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0804') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) GATE_4"+ "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0805') THEN"+ "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)"+ "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) GATE_5"+ "\n");


            sqlString.Append("                  FROM (   SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1, SUM(A.QTY_1) QTY  " + "\n");
            sqlString.Append("                                   FROM RWIPLOTSTS A, MWIPOPRDEF B  " + "\n");
            sqlString.Append("                                 WHERE A.FACTORY = B.FACTORY(+)  " + "\n");
            sqlString.Append("                                      AND A.OPER = B.OPER(+) " + "\n");
            sqlString.Append("                                      AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            sqlString.Append("                                      AND A.LOT_TYPE = 'W'  " + "\n");
            sqlString.Append("                                      AND A.LOT_CMF_5 LIKE 'P%' " + "\n");
            sqlString.Append("                                      AND A.LOT_DEL_FLAG = ' ' " + "\n");
            sqlString.Append("                                  GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT " + "\n");
            sqlString.Append("                WHERE LOT.FACTORY = MAT.FACTORY " + "\n");
            sqlString.Append("                     AND LOT.MAT_ID = MAT.MAT_ID " + "\n");

            if (rowGubunStr.Equals("CHIP"))
                sqlString.Append("  AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
            else sqlString.Append("                     AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' " + "\n");

            sqlString.Append("                     AND MAT.DELETE_FLAG <> 'Y' " + "\n");
            sqlString.Append("                     AND MAT.MAT_GRP_2 <> '-' " + "\n");
            sqlString.Append("                 GROUP BY  LOT.MAT_ID ) B, " + "\n");
            sqlString.Append("              ( SELECT KEY_1,DATA_1 " + "\n");
            sqlString.Append("                   FROM MGCMTBLDAT " + "\n");
            sqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            sqlString.Append("                     AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')  ) G    " + "\n");
            sqlString.Append("   WHERE B.MAT_ID = A.MAT_ID(+)  " + "\n");
            sqlString.Append("       AND B.MAT_ID = G.KEY_1(+) " + "\n");
            sqlString.Append("       AND B.MAT_ID LIKE '%' " + "\n");
            sqlString.Append("       AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            sqlString.Append("   GROUP BY A.MAT_GRP_1, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN " + "\n");
            sqlString.Append("   UNION ALL " + "\n");
            sqlString.Append("   SELECT CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11, GUBUN," + "\n");
            sqlString.Append("               SUM( CASE WHEN OPER IN ('A0400', 'A0401') THEN  LOT_CNT ELSE 0 END)  DA_1," + "\n");
            sqlString.Append("               SUM( CASE WHEN OPER IN ('A0500', 'A0530', 'A0501','A0531' ) THEN  LOT_CNT ELSE 0 END)  DA_2," + "\n");
            sqlString.Append("               SUM( CASE WHEN OPER IN ('A0550', 'A0551') THEN  LOT_CNT ELSE 0 END)  WB_1," + "\n");
            sqlString.Append("               SUM( CASE WHEN OPER IN ('A0600','A0601') THEN  LOT_CNT ELSE 0 END)  WB_2," + "\n");
            sqlString.Append("               SUM( CASE WHEN OPER IN ('A0402') THEN  LOT_CNT ELSE 0 END)  DA_3," + "\n");
            sqlString.Append("               SUM( CASE WHEN OPER IN ('A0502', 'A0532') THEN  LOT_CNT ELSE 0 END)  DA_4," + "\n");
            sqlString.Append("               SUM( CASE WHEN OPER IN ('A0552') THEN  LOT_CNT ELSE 0 END)  WB_3," + "\n");
            sqlString.Append("               SUM( CASE WHEN OPER IN ('A0602') THEN  LOT_CNT ELSE 0 END)  WB_4," + "\n");
            sqlString.Append("               SUM( CASE WHEN OPER IN ('A0403') THEN  LOT_CNT ELSE 0 END)  DA_5," + "\n");
            sqlString.Append("               SUM( CASE WHEN OPER IN ('A0503', 'A0533') THEN  LOT_CNT ELSE 0 END)  DA_6," + "\n");
            sqlString.Append("               SUM( CASE WHEN OPER IN ('A0553') THEN  LOT_CNT ELSE 0 END)  WB_5," + "\n");
            sqlString.Append("               SUM( CASE WHEN OPER IN ('A0603') THEN  LOT_CNT ELSE 0 END)  WB_6," + "\n");
            sqlString.Append("               SUM( CASE WHEN OPER IN ('A0404' ) THEN  LOT_CNT ELSE 0 END)  DA_7," + "\n");
            sqlString.Append("               SUM( CASE WHEN OPER IN ('A0504', 'A0534') THEN  LOT_CNT ELSE 0 END)  DA_8," + "\n");
            sqlString.Append("               SUM( CASE WHEN OPER IN ('A0554') THEN  LOT_CNT ELSE 0 END)  WB_7," + "\n");
            sqlString.Append("               SUM( CASE WHEN OPER IN ('A0604') THEN  LOT_CNT ELSE 0 END)  WB_8," + "\n");
            sqlString.Append("               SUM( CASE WHEN OPER IN ('A0405') THEN  LOT_CNT ELSE 0 END)  DA_9," + "\n");
            sqlString.Append("               SUM( CASE WHEN OPER IN ('A0505', 'A0535') THEN  LOT_CNT ELSE 0 END)  DA_10," + "\n");
            sqlString.Append("               SUM( CASE WHEN OPER IN ('A0555') THEN  LOT_CNT ELSE 0 END)  WB_9," + "\n");
            sqlString.Append("               SUM( CASE WHEN OPER IN ('A0605') THEN  LOT_CNT ELSE 0 END)  WB_10 " + "\n");
            sqlString.Append("      FROM ( SELECT '2_LOT_QTY'  GUBUN, MAT.MAT_GRP_1 CUS, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11, LOT.MAT_ID, LOT.OPER, COUNT(LOT.LOT_ID) LOT_CNT " + "\n");
            sqlString.Append("                     FROM RWIPLOTSTS LOT,  MWIPMATDEF MAT " + "\n");
            sqlString.Append("                  WHERE  LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            sqlString.Append("                      AND LOT.LOT_TYPE = 'W'  " + "\n");
            sqlString.Append("                      AND LOT.LOT_CMF_5 LIKE 'P%' " + "\n");
            sqlString.Append("                      AND LOT.LOT_DEL_FLAG = ' ' " + "\n");
            sqlString.Append("                      AND ( LOT.OPER LIKE  'A040%' OR LOT.OPER LIKE  'A050%' OR LOT.OPER LIKE  'A055%' OR  LOT.OPER LIKE  'A060%' ) " + "\n");
            sqlString.Append("                      AND LOT.FACTORY = MAT.FACTORY " + "\n");
            sqlString.Append("                      AND LOT.MAT_ID = MAT.MAT_ID " + "\n");
            sqlString.Append("                      AND MAT.MAT_TYPE = 'FG' " + "\n");
            sqlString.Append("                      AND MAT.DELETE_FLAG = ' ' " + "\n");

            if (rowGubunStr.Equals("CHIP"))
                sqlString.Append("  AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
            else sqlString.Append("                     AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' " + "\n");

            sqlString.Append("                  GROUP BY  MAT.MAT_GRP_1 , MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11, LOT.MAT_ID, LOT.OPER ) " + "\n");
            sqlString.Append("    GROUP BY CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11, GUBUN " + "\n");
            sqlString.Append(" UNION ALL    " + "\n");
            sqlString.Append(" SELECT  A.MAT_GRP_1 CUS, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN " + "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_1"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_2"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1,0), 'Merge', NVL(WB_1,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_1,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_1,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_1"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_2+GATE_1,0), 'Merge', NVL(WB_2+GATE_1,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_2+GATE_1,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_2+GATE_1,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_2                      "+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_3"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_4"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_3,0), 'Merge', NVL(WB_3,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_3,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_3,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_3"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_4+GATE_1,0), 'Merge', NVL(WB_4+GATE_1,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_4+GATE_1,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_4+GATE_1,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_4"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_5"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_6,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_6,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_6,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_6,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_6"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_5,0), 'Merge', NVL(WB_5,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_5,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_5,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_5"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_6+GATE_1,0), 'Merge', NVL(WB_6+GATE_1,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_6+GATE_1,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_6+GATE_1,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_6                      "+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_7,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_7,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_7,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_7,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_7"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_8,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_8,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_8,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_8,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_8"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_7,0), 'Merge', NVL(WB_7,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_7,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_7,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_7"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_8+GATE_1,0), 'Merge', NVL(WB_8+GATE_1,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_8+GATE_1,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_8+GATE_1,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_8"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_9,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_9,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_9,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_9,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_9"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_10,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_10,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_10,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_10,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_10"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_9,0), 'Merge', NVL(WB_9,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_9,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_9,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_9"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_10+GATE_1,0), 'Merge', NVL(WB_10+GATE_1,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_10+GATE_1,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_10+GATE_1,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_10"+ "\n");
            sqlString.Append("    FROM MWIPMATDEF A, " + "\n");
            sqlString.Append("              ( SELECT  '3_WIP_QTY' GUBUN, LOT.MAT_ID " + "\n");
            sqlString.Append(" , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0250', 'A0305', 'A0306', 'A0310', 'A0400', 'A0401' ) THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_1" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN (  'A0500', 'A0501', 'A0530', 'A0531' ) THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_2" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0402') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_3" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ( 'A0502', 'A0532') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_4" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0403') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_5" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ( 'A0503', 'A0533') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_6" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0404') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_7" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0504', 'A0534') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_8" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0405') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_9" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ( 'A0505', 'A0535') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_10" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0550', 'A0551') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_1" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0600','A0601' ) THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_2" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0552') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_3" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A0602') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_4                 " + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0553') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_5" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A0603') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_6" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0554') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_7" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A0604') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_8" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0555') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_9" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0605') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_10" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0800', 'A0801') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) GATE_1" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0802') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) GATE_2" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0803') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) GATE_3" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0804') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) GATE_4" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0805') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) GATE_5" + "\n");

            sqlString.Append("                  FROM (   SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1, SUM(A.QTY_1) QTY  " + "\n");
            sqlString.Append("                                   FROM RWIPLOTSTS A, MWIPOPRDEF B  " + "\n");
            sqlString.Append("                                 WHERE A.FACTORY = B.FACTORY(+)  " + "\n");
            sqlString.Append("                                      AND A.OPER = B.OPER(+) " + "\n");
            sqlString.Append("                                      AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            sqlString.Append("                                      AND A.LOT_TYPE = 'W'  " + "\n");
            sqlString.Append("                                      AND A.LOT_STATUS = 'PROC' " + "\n");
            sqlString.Append("                                      AND A.LOT_CMF_5 LIKE 'P%' " + "\n");
            sqlString.Append("                                      AND A.LOT_DEL_FLAG = ' ' " + "\n");
            sqlString.Append("                                  GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT " + "\n");
            sqlString.Append("                WHERE LOT.FACTORY = MAT.FACTORY " + "\n");
            sqlString.Append("                     AND LOT.MAT_ID = MAT.MAT_ID " + "\n");

            if (rowGubunStr.Equals("CHIP"))
                sqlString.Append("  AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
            else sqlString.Append("                     AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' " + "\n");

            sqlString.Append("                     AND MAT.DELETE_FLAG <> 'Y' " + "\n");
            sqlString.Append("                     AND MAT.MAT_GRP_2 <> '-' " + "\n");
            sqlString.Append("                 GROUP BY  LOT.MAT_ID ) B, " + "\n");
            sqlString.Append("              ( SELECT KEY_1,DATA_1 " + "\n");
            sqlString.Append("                   FROM MGCMTBLDAT " + "\n");
            sqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            sqlString.Append("                     AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')  ) G    " + "\n");
            sqlString.Append("   WHERE B.MAT_ID = A.MAT_ID(+)  " + "\n");
            sqlString.Append("       AND B.MAT_ID = G.KEY_1(+) " + "\n");
            sqlString.Append("       AND B.MAT_ID LIKE '%' " + "\n");
            sqlString.Append("       AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            sqlString.Append("   GROUP BY A.MAT_GRP_1, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN " + "\n");
            sqlString.Append(" UNION ALL    " + "\n");
            sqlString.Append(" SELECT  A.MAT_GRP_1 CUS, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN " + "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_1"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_2"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1,0), 'Merge', NVL(WB_1,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_1,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_1,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_1"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_2+GATE_1,0), 'Merge', NVL(WB_2+GATE_1,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_2+GATE_1,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_2+GATE_1,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_2                      "+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_3,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_3"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_4,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_4"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_3,0), 'Merge', NVL(WB_3,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_3,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_3,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_3"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_4+GATE_1,0), 'Merge', NVL(WB_4+GATE_1,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_4+GATE_1,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_4+GATE_1,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_4"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_5,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_5"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_6,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_6,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_6,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_6,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_6"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_5,0), 'Merge', NVL(WB_5,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_5,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_5,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_5"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_6+GATE_1,0), 'Merge', NVL(WB_6+GATE_1,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_6+GATE_1,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_6+GATE_1,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_6                      "+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_7,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_7,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_7,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_7,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_7"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_8,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_8,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_8,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_8,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_8"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_7,0), 'Merge', NVL(WB_7,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_7,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_7,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_7"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_8+GATE_1,0), 'Merge', NVL(WB_8+GATE_1,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_8+GATE_1,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_8+GATE_1,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_8"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_9,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_9,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_9,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_9,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_9"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN "+ "\n");
            sqlString.Append("                           DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_10,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_10,0), 0)"+ "\n");
            sqlString.Append("                       WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_10,0)  ELSE 0 END"+ "\n");
            sqlString.Append("                       ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_10,0)"+ "\n");
            sqlString.Append("                       END), 0)  ) ) AS DA_10"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_9,0), 'Merge', NVL(WB_9,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_9,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_9,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_9"+ "\n");
            sqlString.Append("              , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_10+GATE_1,0), 'Merge', NVL(WB_10+GATE_1,0), 0)"+ "\n");
            sqlString.Append("                         WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_10+GATE_1,0) ELSE 0 END"+ "\n");
            sqlString.Append("                         ELSE NVL(WB_10+GATE_1,0)"+ "\n");
            sqlString.Append("                       END),0)   ) ) AS WB_10"+ "\n");
            sqlString.Append("    FROM MWIPMATDEF A, " + "\n");
            sqlString.Append("              ( SELECT  '4_WAIT_QTY' GUBUN, LOT.MAT_ID " + "\n");
            sqlString.Append(" , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0250', 'A0305', 'A0306', 'A0310', 'A0400', 'A0401' ) THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_1" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN (  'A0500', 'A0501', 'A0530', 'A0531' ) THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_2" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0402') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_3" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ( 'A0502', 'A0532') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_4" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0403') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_5" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ( 'A0503', 'A0533') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_6" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0404') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_7" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0504', 'A0534') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_8" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0405') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_9" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ( 'A0505', 'A0535') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) DA_10" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0550', 'A0551') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_1" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0600','A0601' ) THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_2" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0552') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_3" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A0602') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_4                 " + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0553') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_5" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A0603') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_6" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0554') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_7" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A0604') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_8" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0555') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_9" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0605') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) WB_10" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0800', 'A0801') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) GATE_1" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0802') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) GATE_2" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0803') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) GATE_3" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0804') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) GATE_4" + "\n");
            sqlString.Append("                  , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0805') THEN" + "\n");
            sqlString.Append("                                   DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                   ELSE 0 END, 0) ) GATE_5" + "\n");

            sqlString.Append("                  FROM (   SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1, SUM(A.QTY_1) QTY  " + "\n");
            sqlString.Append("                                   FROM RWIPLOTSTS A, MWIPOPRDEF B  " + "\n");
            sqlString.Append("                                 WHERE A.FACTORY = B.FACTORY(+)  " + "\n");
            sqlString.Append("                                      AND A.OPER = B.OPER(+) " + "\n");
            sqlString.Append("                                      AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            sqlString.Append("                                      AND A.LOT_TYPE = 'W'  " + "\n");
            sqlString.Append("                                      AND A.LOT_STATUS = 'WAIT' " + "\n");
            sqlString.Append("                                      AND A.LOT_CMF_5 LIKE 'P%' " + "\n");
            sqlString.Append("                                      AND A.LOT_DEL_FLAG = ' ' " + "\n");
            sqlString.Append("                                  GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT " + "\n");
            sqlString.Append("                WHERE LOT.FACTORY = MAT.FACTORY " + "\n");
            sqlString.Append("                     AND LOT.MAT_ID = MAT.MAT_ID " + "\n");

            if (rowGubunStr.Equals("CHIP"))
                sqlString.Append("  AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
            else sqlString.Append("                     AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' " + "\n");

            sqlString.Append("                     AND MAT.DELETE_FLAG <> 'Y' " + "\n");
            sqlString.Append("                     AND MAT.MAT_GRP_2 <> '-' " + "\n");
            sqlString.Append("                 GROUP BY  LOT.MAT_ID ) B, " + "\n");
            sqlString.Append("              ( SELECT KEY_1,DATA_1 " + "\n");
            sqlString.Append("                   FROM MGCMTBLDAT " + "\n");
            sqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            sqlString.Append("                     AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')  ) G    " + "\n");
            sqlString.Append("   WHERE B.MAT_ID = A.MAT_ID(+)  " + "\n");
            sqlString.Append("       AND B.MAT_ID = G.KEY_1(+) " + "\n");
            sqlString.Append("       AND B.MAT_ID LIKE '%' " + "\n");
            sqlString.Append("       AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            sqlString.Append("   GROUP BY A.MAT_GRP_1, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN ) A, " + "\n");
            sqlString.Append("   ( SELECT SEQ FROM HRTDSUMSEQ@RPTTOMES WHERE SEQ < 5 ) TTLSEQ " + "\n");

            if (strCustomer.Equals("-"))
            {
                sqlString.Append("              WHERE CUS =   ( SELECT KEY_1" + "\n");
                sqlString.Append("                                                         FROM MGCMTBLDAT@RPTTOMES" + "\n");
                sqlString.Append("                                                       WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                sqlString.Append("                                                           AND TABLE_NAME = 'H_CUSTOMER'" + "\n");
                sqlString.Append("                                                           AND DATA_1 = '" + rowCustomStr + "' )" + "\n");
            }
            else sqlString.Append("             WHERE CUS = '" + strCustomer + "'" + "\n");

            sqlString.Append("    AND MAT_GRP_9 = '" + rowMajorStr + "'" + "\n");
            sqlString.Append("    AND MAT_GRP_10 = '" + rowPKGStr + "'" + "\n");

            sqlString.Append(" GROUP BY  CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11, " + "\n");
            sqlString.Append("            DECODE(TTLSEQ.SEQ, 1, 'WIP_QTY', 2, 'LOT_CNT', 3, 'RUN_QTY', 'WAIT_QTY') " + "\n");
            sqlString.Append(" ORDER BY  CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11,  " + "\n");
            sqlString.Append("            DECODE(GUBUN, 'WIP_QTY', 1,  'LOT_CNT', 2,  'RUN_QTY', 3, 4) " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(sqlString.ToString());
            }

            return sqlString.ToString();

        }

        //USER가 DATA Sheet에서 선택한 컬럼이 TTL일 경우 SQL생성하는 함수.
        private string popupWindowChipWBSQL(string rowCustomStr, string rowMajorStr, string rowPKGStr, string rowGubunStr, string colChipStr, string colStepStr)
        {
            StringBuilder sqlString = new StringBuilder();
            string strCustomer = "";

            switch (rowCustomStr)
            {
                case "SEC":
                    strCustomer = "SE";
                    break;
                case "HYNIX":
                    strCustomer = "HX";
                    break;
                case "iML":
                    strCustomer = "IM";
                    break;
                case "FCI":
                    strCustomer = "FC";
                    break;
                case "IMAGIS":
                    strCustomer = "IG";
                    break;
                default:
                    strCustomer = "-";
                    break;
            }

           // sqlString.Append(" SELECT  CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11," + "\n");
            sqlString.Append(" SELECT  MAT_GRP_10, MAT_GRP_6, MAT_CMF_11," + "\n");
            sqlString.Append("             DECODE(TTLSEQ.SEQ, 1, 'WIP_QTY', 2, 'LOT_CNT', 3, 'RUN_QTY', 'WAIT_QTY') GUBUN," + "\n");

            if ( colChipStr.Equals("Primary Chip") )
            {
                sqlString.Append("             SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(WB_1, 0), 0 )  )  WB_1," + "\n");
                sqlString.Append("             SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(WB_2, 0), 0 )  )  WB_2," + "\n");
                sqlString.Append("             SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(WB_3, 0), 0 )  )  WB_3," + "\n"); 
            }

            sqlString.Append("             SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(WB_4, 0), 0 )  )  WB_4," + "\n");
            sqlString.Append("             SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(WB_5, 0), 0 )  )  WB_5," + "\n");
            sqlString.Append("             SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(WB_6, 0), 0 )  )  WB_6" + "\n");          
            sqlString.Append("    FROM (" + "\n");
            sqlString.Append(" SELECT  A.MAT_GRP_1 CUS, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");

            if ( colChipStr.Equals("Primary Chip") )
            {
                sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_1,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_1,0), 0)" + "\n");
                sqlString.Append("                    ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_1,0)" + "\n");
                sqlString.Append("                  END),0)   ) ) AS WB_1" + "\n");
                sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_2,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_2,0), 0)" + "\n");
                sqlString.Append("                    ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_2,0)" + "\n");
                sqlString.Append("                  END),0)   ) ) AS WB_2" + "\n");
                sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_3,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_3,0), 0)" + "\n");
                sqlString.Append("                    ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_3,0)" + "\n");
                sqlString.Append("                  END),0)   ) ) AS WB_3" + "\n");
            }
            sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_4,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_4,0), 0)" + "\n");
            sqlString.Append("                    ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_4,0)" + "\n");
            sqlString.Append("                  END),0)   ) ) AS WB_4" + "\n");
            sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_5,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_5,0), 0)" + "\n");
            sqlString.Append("                    ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_5,0)" + "\n");
            sqlString.Append("                  END),0)   ) ) AS WB_5" + "\n");
            sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_6,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_6,0), 0)" + "\n");
            sqlString.Append("                    ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_6,0)" + "\n");
            sqlString.Append("                  END),0)   ) ) AS WB_6" + "\n");
            sqlString.Append("     FROM MWIPMATDEF A," + "\n");
            sqlString.Append("               ( SELECT  '1_WIP_QTY' GUBUN, LOT.MAT_ID" + "\n");
            sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1,  0 DA_1" + "\n");
            
            if ( colChipStr.Equals("Primary Chip") )
            {
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0500') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_1" + "\n");
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0550') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_2" + "\n");
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A0600') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_3" + "\n");
            }
            sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A050" + colChipStr.Substring(0, 1) + "') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
            sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_4" + "\n");
            sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A055" + colChipStr.Substring(0, 1) + "') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
            sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_5" + "\n");
            sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A060" + colChipStr.Substring(0, 1) + "') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
            sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_6 " + "\n");                                                                                    
            sqlString.Append("                   FROM (   SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1, SUM(A.QTY_1) QTY " + "\n");
            sqlString.Append("                                    FROM RWIPLOTSTS A, MWIPOPRDEF B" + "\n"); 
            sqlString.Append("                                  WHERE A.FACTORY = B.FACTORY(+)" + "\n"); 
            sqlString.Append("                                       AND A.OPER = B.OPER(+)" + "\n");
            sqlString.Append("                                       AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                                       AND A.LOT_TYPE = 'W'" + "\n"); 
            sqlString.Append("                                       AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
            sqlString.Append("                                       AND A.LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                                   GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT" + "\n");
            sqlString.Append("                 WHERE LOT.FACTORY = MAT.FACTORY" + "\n");
            sqlString.Append("                      AND LOT.MAT_ID = MAT.MAT_ID" + "\n");

            if (rowGubunStr.Equals("CHIP"))
                sqlString.Append("  AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
            else sqlString.Append("                     AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' " + "\n");

            sqlString.Append("                      AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            sqlString.Append("                      AND MAT.MAT_GRP_2 <> '-'" + "\n");
            sqlString.Append("                  GROUP BY  LOT.MAT_ID ) B," + "\n");
            sqlString.Append("               ( SELECT KEY_1,DATA_1" + "\n");
            sqlString.Append("                    FROM MGCMTBLDAT" + "\n");
            sqlString.Append("                  WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n"); 
            sqlString.Append("                      AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')  ) G " + "\n");  
            sqlString.Append("    WHERE B.MAT_ID = A.MAT_ID(+)" + "\n"); 
            sqlString.Append("        AND B.MAT_ID = G.KEY_1(+)" + "\n");
            sqlString.Append("        AND B.MAT_ID LIKE '%'" + "\n");
            sqlString.Append("        AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("    GROUP BY A.MAT_GRP_1, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");
            sqlString.Append("    UNION ALL" + "\n");
            sqlString.Append("    SELECT CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11, GUBUN," + "\n"); 

            if ( colChipStr.Equals("Primary Chip") )
            {
                sqlString.Append("                SUM(DECODE( OPER, 'A0500', LOT_CNT, 0)) WB_1," + "\n");
                sqlString.Append("                SUM(DECODE( OPER, 'A0550', LOT_CNT, 0)) WB_2," + "\n");
                sqlString.Append("                SUM(DECODE( OPER, 'A0600', LOT_CNT, 0)) WB_3," + "\n");
            }
            sqlString.Append("                SUM(DECODE( OPER, 'A050" + colChipStr.Substring(0, 1) + "', LOT_CNT, 0)) WB_4," + "\n");
            sqlString.Append("                SUM(DECODE( OPER, 'A055" + colChipStr.Substring(0, 1) + "', LOT_CNT, 0)) WB_5," + "\n");
            sqlString.Append("                SUM(DECODE( OPER, 'A060" + colChipStr.Substring(0, 1) + "', LOT_CNT, 0)) WB_6" + "\n");
            sqlString.Append("       FROM ( SELECT '2_LOT_QTY'  GUBUN, MAT.MAT_GRP_1 CUS, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11, LOT.MAT_ID, LOT.OPER, COUNT(LOT.LOT_ID) LOT_CNT" + "\n");
            sqlString.Append("                      FROM RWIPLOTSTS LOT,  MWIPMATDEF MAT" + "\n");
            sqlString.Append("                   WHERE  LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                       AND LOT.LOT_TYPE = 'W'" + "\n"); 
            sqlString.Append("                       AND LOT.LOT_CMF_5 LIKE 'P%'" + "\n");
            sqlString.Append("                       AND LOT.LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                       AND ( LOT.OPER LIKE  'A050%' OR LOT.OPER LIKE  'A055%' OR  LOT.OPER LIKE  'A060%' )" + "\n");
            sqlString.Append("                       AND LOT.FACTORY = MAT.FACTORY" + "\n");
            sqlString.Append("                       AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
            sqlString.Append("                       AND MAT.MAT_TYPE = 'FG'" + "\n");
            sqlString.Append("                       AND MAT.DELETE_FLAG = ' '" + "\n");

            if (rowGubunStr.Equals("CHIP"))
                sqlString.Append("  AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
            else sqlString.Append("                     AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' " + "\n");

            sqlString.Append("                   GROUP BY  MAT.MAT_GRP_1 , MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11, LOT.MAT_ID, LOT.OPER )" + "\n");
            sqlString.Append("     GROUP BY CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11, GUBUN" + "\n");
            sqlString.Append("  UNION ALL" + "\n");   
            sqlString.Append("  SELECT  A.MAT_GRP_1 CUS, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");

            if ( colChipStr.Equals("Primary Chip") )
            {
                sqlString.Append("                , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_1,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_1,0), 0)" + "\n");
                sqlString.Append("                    ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_1,0)" + "\n");
                sqlString.Append("                  END),0)   ) ) AS WB_1" + "\n");
                sqlString.Append("                , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_2,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_2,0), 0)" + "\n");
                sqlString.Append("                     ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_2,0)" + "\n");
                sqlString.Append("                   END),0)   ) ) AS WB_2" + "\n");
                sqlString.Append("                , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_3,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_3,0), 0)" + "\n");
                sqlString.Append("                     ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_3,0)" + "\n");
                sqlString.Append("                   END),0)   ) ) AS WB_3" + "\n");
            }

            sqlString.Append("                , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_4,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_4,0), 0)" + "\n");
            sqlString.Append("                     ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_4,0)" + "\n");
            sqlString.Append("                   END),0)   ) ) AS WB_4" + "\n");
            sqlString.Append("                , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_5,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_5,0), 0)" + "\n");
            sqlString.Append("                     ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_5,0)" + "\n");
            sqlString.Append("                   END),0)   ) ) AS WB_5" + "\n");
            sqlString.Append("                , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_6,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_6,0), 0)" + "\n");
            sqlString.Append("                     ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_6,0)" + "\n");
            sqlString.Append("                   END),0)   ) ) AS WB_6" + "\n");
            sqlString.Append("     FROM MWIPMATDEF A," + "\n");
            sqlString.Append("               ( SELECT  '3_WIP_QTY' GUBUN, LOT.MAT_ID" + "\n");
            sqlString.Append("                              , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1,  0 DA_1" + "\n");
            if ( colChipStr.Equals("Primary Chip") )
            {
                sqlString.Append("                              , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0500') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
                sqlString.Append("                                                                                            DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                  ELSE 0  END, 0) ) WB_1" + "\n");
                sqlString.Append("                              , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0550') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
                sqlString.Append("                                                                                            DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                  ELSE 0  END, 0) ) WB_2" + "\n");
                sqlString.Append("                              , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A0600') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
                sqlString.Append("                                                                                            DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                  ELSE 0  END, 0) ) WB_3" + "\n");
            }
            sqlString.Append("                              , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A050" + colChipStr.Substring(0, 1) + "') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
            sqlString.Append("                                                                                            DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                                                                  ELSE 0  END, 0) ) WB_4" + "\n");
            sqlString.Append("                              , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A055" + colChipStr.Substring(0, 1) + "') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
            sqlString.Append("                                                                                            DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                                                                  ELSE 0  END, 0) ) WB_5" + "\n");
            sqlString.Append("                              , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A060" + colChipStr.Substring(0, 1) + "') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
            sqlString.Append("                                                                                            DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                                                                   ELSE 0  END, 0) ) WB_6" + "\n");             
            sqlString.Append("                   FROM (   SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1, SUM(A.QTY_1) QTY" + "\n"); 
            sqlString.Append("                                    FROM RWIPLOTSTS A, MWIPOPRDEF B " + "\n");
            sqlString.Append("                                  WHERE A.FACTORY = B.FACTORY(+)" + "\n"); 
            sqlString.Append("                                       AND A.OPER = B.OPER(+)" + "\n");
            sqlString.Append("                                       AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                                       AND A.LOT_TYPE = 'W'" + "\n"); 
            sqlString.Append("                                       AND A.LOT_STATUS = 'PROC'" + "\n");
            sqlString.Append("                                       AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
            sqlString.Append("                                       AND A.LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                                   GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT" + "\n");
            sqlString.Append("                 WHERE LOT.FACTORY = MAT.FACTORY" + "\n");
            sqlString.Append("                      AND LOT.MAT_ID = MAT.MAT_ID" + "\n");

            if (rowGubunStr.Equals("CHIP"))
                sqlString.Append("  AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
            else sqlString.Append("                     AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' " + "\n");

            sqlString.Append("                      AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            sqlString.Append("                      AND MAT.MAT_GRP_2 <> '-'" + "\n");
            sqlString.Append("                  GROUP BY  LOT.MAT_ID ) B," + "\n");
            sqlString.Append("               ( SELECT KEY_1,DATA_1" + "\n");
            sqlString.Append("                    FROM MGCMTBLDAT" + "\n");
            sqlString.Append("                  WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n"); 
            sqlString.Append("                      AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')  ) G" + "\n");   
            sqlString.Append("    WHERE B.MAT_ID = A.MAT_ID(+) " + "\n");
            sqlString.Append("        AND B.MAT_ID = G.KEY_1(+)" + "\n");
            sqlString.Append("        AND B.MAT_ID LIKE '%'" + "\n");
            sqlString.Append("        AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("    GROUP BY A.MAT_GRP_1, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");
            sqlString.Append("  UNION ALL " + "\n");
            sqlString.Append("  SELECT  A.MAT_GRP_1 CUS, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");

            if ( colChipStr.Equals("Primary Chip") )
            {
                sqlString.Append("                , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_1,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_1,0), 0)" + "\n");
                sqlString.Append("                    ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_1,0)" + "\n");
                sqlString.Append("                  END),0)   ) ) AS WB_1" + "\n");
                sqlString.Append("                , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_2,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_2,0), 0)" + "\n");
                sqlString.Append("                     ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_2,0)" + "\n");
                sqlString.Append("                   END),0)   ) ) AS WB_2" + "\n");
                sqlString.Append("                , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_3,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_3,0), 0)" + "\n");
                sqlString.Append("                     ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_3,0)" + "\n");
                sqlString.Append("                   END),0)   ) ) AS WB_3" + "\n");
            }
            sqlString.Append("                , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_4,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_4,0), 0)" + "\n");
            sqlString.Append("                     ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_4,0)" + "\n");
            sqlString.Append("                   END),0)   ) ) AS WB_4" + "\n");
            sqlString.Append("                , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_5,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_5,0), 0)" + "\n");
            sqlString.Append("                     ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_5,0)" + "\n");
            sqlString.Append("                   END),0)   ) ) AS WB_5" + "\n");
            sqlString.Append("                , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_6,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_6,0), 0)" + "\n");
            sqlString.Append("                     ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(WB_6,0)" + "\n");
            sqlString.Append("                   END),0)   ) ) AS WB_6" + "\n");
            sqlString.Append("     FROM MWIPMATDEF A," + "\n");
            sqlString.Append("               ( SELECT  '4_WAIT_QTY' GUBUN, LOT.MAT_ID" + "\n");
            sqlString.Append("                              , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1,  0 DA_1" + "\n");

            if (colChipStr.Equals("Primary Chip"))
            {
                sqlString.Append("                              , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0500') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
                sqlString.Append("                                                                                            DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                  ELSE 0  END, 0) ) WB_1" + "\n");
                sqlString.Append("                              , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0550') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
                sqlString.Append("                                                                                            DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                  ELSE 0  END, 0) ) WB_2" + "\n");
                sqlString.Append("                              , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A0600') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
                sqlString.Append("                                                                                            DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                  ELSE 0  END, 0) ) WB_3" + "\n");
            }

            sqlString.Append("                              , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A050" + colChipStr.Substring(0, 1) + "') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
            sqlString.Append("                                                                                            DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                                                                  ELSE 0  END, 0) ) WB_4" + "\n");
            sqlString.Append("                              , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A055" + colChipStr.Substring(0, 1) + "') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
            sqlString.Append("                                                                                            DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                                                                  ELSE 0  END, 0) ) WB_5" + "\n");
            sqlString.Append("                              , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A060" + colChipStr.Substring(0, 1) + "') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
            sqlString.Append("                                                                                            DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                                                                   ELSE 0  END, 0) ) WB_6" + "\n");         
            sqlString.Append("                   FROM (   SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1, SUM(A.QTY_1) QTY " + "\n");
            sqlString.Append("                                    FROM RWIPLOTSTS A, MWIPOPRDEF B " + "\n");
            sqlString.Append("                                  WHERE A.FACTORY = B.FACTORY(+)" + "\n"); 
            sqlString.Append("                                       AND A.OPER = B.OPER(+)" + "\n");
            sqlString.Append("                                       AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                                       AND A.LOT_TYPE = 'W' " + "\n");
            sqlString.Append("                                       AND A.LOT_STATUS = 'WAIT'" + "\n");
            sqlString.Append("                                       AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
            sqlString.Append("                                       AND A.LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                                   GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT" + "\n");
            sqlString.Append("                 WHERE LOT.FACTORY = MAT.FACTORY" + "\n");
            sqlString.Append("                      AND LOT.MAT_ID = MAT.MAT_ID" + "\n");

            if (rowGubunStr.Equals("CHIP"))
                sqlString.Append("  AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
            else sqlString.Append("                     AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' " + "\n");

            sqlString.Append("                      AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            sqlString.Append("                      AND MAT.MAT_GRP_2 <> '-'" + "\n");
            sqlString.Append("                  GROUP BY  LOT.MAT_ID ) B," + "\n");
            sqlString.Append("               ( SELECT KEY_1,DATA_1" + "\n");
            sqlString.Append("                    FROM MGCMTBLDAT" + "\n");
            sqlString.Append("                  WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n"); 
            sqlString.Append("                      AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')  ) G" + "\n");   
            sqlString.Append("    WHERE B.MAT_ID = A.MAT_ID(+)" + "\n"); 
            sqlString.Append("        AND B.MAT_ID = G.KEY_1(+)" + "\n");
            sqlString.Append("        AND B.MAT_ID LIKE '%'" + "\n");
            sqlString.Append("        AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("    GROUP BY A.MAT_GRP_1, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN ) A," + "\n");
            sqlString.Append("    ( SELECT SEQ FROM HRTDSUMSEQ@RPTTOMES WHERE SEQ < 5 ) TTLSEQ" + "\n");

            if (strCustomer.Equals("-"))
            {
                sqlString.Append("              WHERE CUS =   ( SELECT KEY_1" + "\n");
                sqlString.Append("                                                         FROM MGCMTBLDAT@RPTTOMES" + "\n");
                sqlString.Append("                                                       WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                sqlString.Append("                                                           AND TABLE_NAME = 'H_CUSTOMER'" + "\n");
                sqlString.Append("                                                           AND DATA_1 = '" + rowCustomStr + "' )" + "\n");
            }
            else sqlString.Append("             WHERE CUS = '" + strCustomer + "'" + "\n");

            sqlString.Append("    AND MAT_GRP_9 = '" + rowMajorStr + "'" + "\n");
            sqlString.Append("    AND MAT_GRP_10 = '" + rowPKGStr + "'" + "\n");

            sqlString.Append(" GROUP BY  CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11," + "\n");
            sqlString.Append("             DECODE(TTLSEQ.SEQ, 1, 'WIP_QTY', 2, 'LOT_CNT', 3, 'RUN_QTY', 'WAIT_QTY')" + "\n");
            sqlString.Append(" ORDER BY  CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11, " + "\n");
            sqlString.Append("             DECODE(GUBUN, 'WIP_QTY', 1,  'LOT_CNT', 2,  'RUN_QTY', 3, 4)" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(sqlString.ToString());
            }

            return sqlString.ToString();

        }

        private string popupWindowChipDASQL(string rowCustomStr, string rowMajorStr, string rowPKGStr, string rowGubunStr, string colChipStr, string colStepStr)
        {
            StringBuilder sqlString = new StringBuilder();
            string strCustomer = "";

            switch (rowCustomStr)
            {
                case "SEC":
                    strCustomer = "SE";
                    break;
                case "HYNIX":
                    strCustomer = "HX";
                    break;
                case "iML":
                    strCustomer = "IM";
                    break;
                case "FCI":
                    strCustomer = "FC";
                    break;
                case "IMAGIS":
                    strCustomer = "IG";
                    break;
                default:
                    strCustomer = "-";
                    break;
            }
            
            //sqlString.Append(" SELECT  CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11," + "\n");

            sqlString.Append(" SELECT  MAT_GRP_10, MAT_GRP_6, MAT_CMF_11," + "\n");
            sqlString.Append("             DECODE(TTLSEQ.SEQ, 1, 'WIP_QTY', 2, 'LOT_CNT', 3, 'RUN_QTY', 'WAIT_QTY') GUBUN," + "\n");
            sqlString.Append("             SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(DA_2, 0), 0 )  )  DA " + "\n");
            sqlString.Append("    FROM (" + "\n");
            sqlString.Append(" SELECT  A.MAT_GRP_1 CUS, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");
            sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0), 0)" + "\n");
            sqlString.Append("                    ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0)" + "\n");
            sqlString.Append("                  END),0)   ) ) AS DA_2" + "\n");
            sqlString.Append("     FROM MWIPMATDEF A," + "\n");
            sqlString.Append("               ( SELECT  '1_WIP_QTY' GUBUN, LOT.MAT_ID" + "\n");
            sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1,  0 DA_1" + "\n");
            sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A040" + colChipStr.Substring(0, 1) + "') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
            sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                                                                 ELSE 0  END, 0) ) DA_2" + "\n");
            sqlString.Append("                   FROM (   SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1, SUM(A.QTY_1) QTY" + "\n");
            sqlString.Append("                                    FROM RWIPLOTSTS A, MWIPOPRDEF B" + "\n");
            sqlString.Append("                                  WHERE A.FACTORY = B.FACTORY(+)" + "\n");
            sqlString.Append("                                       AND A.OPER = B.OPER(+)" + "\n");
            sqlString.Append("                                       AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                                       AND A.LOT_TYPE = 'W'" + "\n");
            sqlString.Append("                                       AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
            sqlString.Append("                                       AND A.LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                                   GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT" + "\n");
            sqlString.Append("                 WHERE LOT.FACTORY = MAT.FACTORY" + "\n");
            sqlString.Append("                      AND LOT.MAT_ID = MAT.MAT_ID" + "\n");

            if (rowGubunStr.Equals("CHIP"))
                sqlString.Append("  AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
            else sqlString.Append("                     AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' " + "\n");

            sqlString.Append("                      AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            sqlString.Append("                      AND MAT.MAT_GRP_2 <> '-'" + "\n");
            sqlString.Append("                  GROUP BY  LOT.MAT_ID ) B," + "\n");
            sqlString.Append("               ( SELECT KEY_1,DATA_1" + "\n");
            sqlString.Append("                    FROM MGCMTBLDAT" + "\n");
            sqlString.Append("                  WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                      AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')  ) G " + "\n");
            sqlString.Append("    WHERE B.MAT_ID = A.MAT_ID(+) " + "\n");
            sqlString.Append("        AND B.MAT_ID = G.KEY_1(+)" + "\n");
            sqlString.Append("        AND B.MAT_ID LIKE '%'" + "\n");
            sqlString.Append("        AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("    GROUP BY A.MAT_GRP_1, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");
            sqlString.Append("    UNION ALL" + "\n");
            sqlString.Append("    SELECT CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11, GUBUN, SUM(LOT_CNT) LOT_CNT" + "\n");
            sqlString.Append("       FROM ( SELECT '2_LOT_QTY'  GUBUN, MAT.MAT_GRP_1 CUS, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11, LOT.MAT_ID, COUNT(LOT.LOT_ID) LOT_CNT" + "\n");
            sqlString.Append("                      FROM RWIPLOTSTS LOT,  MWIPMATDEF MAT" + "\n");
            sqlString.Append("                   WHERE  LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                       AND LOT.LOT_TYPE = 'W' " + "\n");
            sqlString.Append("                       AND LOT.LOT_CMF_5 LIKE 'P%'" + "\n");
            sqlString.Append("                       AND LOT.LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                       AND LOT.OPER = 'A040" + colChipStr.Substring(0, 1) + "'" + "\n");
            sqlString.Append("                       AND LOT.FACTORY = MAT.FACTORY" + "\n");
            sqlString.Append("                       AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
            sqlString.Append("                       AND MAT.MAT_TYPE = 'FG'" + "\n");
            sqlString.Append("                       AND MAT.DELETE_FLAG = ' '" + "\n");

            if (rowGubunStr.Equals("CHIP"))
                sqlString.Append("  AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
            else sqlString.Append("                     AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' " + "\n");

            sqlString.Append("                   GROUP BY  MAT.MAT_GRP_1 , MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11, LOT.MAT_ID )" + "\n");
            sqlString.Append("     GROUP BY CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11, GUBUN" + "\n");
            sqlString.Append("  UNION ALL" + "\n");
            sqlString.Append("  SELECT  A.MAT_GRP_1 CUS, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");
            sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0), 0)" + "\n");
            sqlString.Append("                    ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0)" + "\n");
            sqlString.Append("                  END),0)   ) ) AS DA_2" + "\n");
            sqlString.Append("     FROM MWIPMATDEF A," + "\n");
            sqlString.Append("               ( SELECT  '3_WIP_QTY' GUBUN, LOT.MAT_ID" + "\n");
            sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1,  0 DA_1" + "\n");
            sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A040" + colChipStr.Substring(0, 1) + "') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
            sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                                                                 ELSE 0  END, 0) ) DA_2" + "\n");
            sqlString.Append("                   FROM (   SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1, SUM(A.QTY_1) QTY " + "\n");
            sqlString.Append("                                    FROM RWIPLOTSTS A, MWIPOPRDEF B" + "\n");
            sqlString.Append("                                  WHERE A.FACTORY = B.FACTORY(+) " + "\n");
            sqlString.Append("                                       AND A.OPER = B.OPER(+)" + "\n");
            sqlString.Append("                                       AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                                       AND A.LOT_TYPE = 'W' " + "\n");
            sqlString.Append("                                       AND A.LOT_STATUS = 'PROC'" + "\n");
            sqlString.Append("                                       AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
            sqlString.Append("                                       AND A.LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                                   GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT" + "\n");
            sqlString.Append("                 WHERE LOT.FACTORY = MAT.FACTORY" + "\n");
            sqlString.Append("                      AND LOT.MAT_ID = MAT.MAT_ID" + "\n");

            if (rowGubunStr.Equals("CHIP"))
                sqlString.Append("  AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
            else sqlString.Append("                     AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' " + "\n");

            sqlString.Append("                      AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            sqlString.Append("                      AND MAT.MAT_GRP_2 <> '-'" + "\n");
            sqlString.Append("                  GROUP BY  LOT.MAT_ID ) B," + "\n");
            sqlString.Append("               ( SELECT KEY_1,DATA_1" + "\n");
            sqlString.Append("                    FROM MGCMTBLDAT" + "\n");
            sqlString.Append("                  WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            sqlString.Append("                      AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')  ) G  " + "\n");
            sqlString.Append("    WHERE B.MAT_ID = A.MAT_ID(+) " + "\n");
            sqlString.Append("        AND B.MAT_ID = G.KEY_1(+)" + "\n");
            sqlString.Append("        AND B.MAT_ID LIKE '%'" + "\n");
            sqlString.Append("        AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("    GROUP BY A.MAT_GRP_1, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");
            sqlString.Append("  UNION ALL" + "\n");
            sqlString.Append("  SELECT  A.MAT_GRP_1 CUS, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");
            sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0), 0)" + "\n");
            sqlString.Append("                    ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_2,0)" + "\n");
            sqlString.Append("                  END),0)   ) ) AS DA_2" + "\n");
            sqlString.Append("     FROM MWIPMATDEF A," + "\n");
            sqlString.Append("               ( SELECT  '4_WAIT_QTY' GUBUN, LOT.MAT_ID" + "\n");
            sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1" + "\n");
            sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A040" + colChipStr.Substring(0, 1) + "') AND  MAT.MAT_GRP_5 = '2nd' THEN" + "\n");
            sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
            sqlString.Append("                                                                                 ELSE 0  END, 0) ) DA_2" + "\n");
            sqlString.Append("                   FROM (   SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1, SUM(A.QTY_1) QTY " + "\n");
            sqlString.Append("                                    FROM RWIPLOTSTS A, MWIPOPRDEF B " + "\n");
            sqlString.Append("                                  WHERE A.FACTORY = B.FACTORY(+) " + "\n");
            sqlString.Append("                                       AND A.OPER = B.OPER(+)" + "\n");
            sqlString.Append("                                       AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                                       AND A.LOT_TYPE = 'W' " + "\n");
            sqlString.Append("                                       AND A.LOT_STATUS = 'WAIT'" + "\n");
            sqlString.Append("                                       AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
            sqlString.Append("                                       AND A.LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                                   GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT" + "\n");
            sqlString.Append("                 WHERE LOT.FACTORY = MAT.FACTORY" + "\n");
            sqlString.Append("                      AND LOT.MAT_ID = MAT.MAT_ID" + "\n");

            if (rowGubunStr.Equals("CHIP"))
                sqlString.Append("  AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
            else sqlString.Append("                     AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' " + "\n");

            sqlString.Append("                      AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            sqlString.Append("                      AND MAT.MAT_GRP_2 <> '-'" + "\n");
            sqlString.Append("                  GROUP BY  LOT.MAT_ID ) B," + "\n");
            sqlString.Append("               ( SELECT KEY_1,DATA_1" + "\n");
            sqlString.Append("                    FROM MGCMTBLDAT" + "\n");
            sqlString.Append("                  WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            sqlString.Append("                      AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')  ) G  " + "\n");
            sqlString.Append("    WHERE B.MAT_ID = A.MAT_ID(+) " + "\n");
            sqlString.Append("        AND B.MAT_ID = G.KEY_1(+)" + "\n");
            sqlString.Append("        AND B.MAT_ID LIKE '%'" + "\n");
            sqlString.Append("        AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("    GROUP BY A.MAT_GRP_1, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN ) A," + "\n");
            sqlString.Append("    ( SELECT SEQ FROM HRTDSUMSEQ@RPTTOMES WHERE SEQ < 5 ) TTLSEQ" + "\n");

            if (strCustomer.Equals("-"))
            {
                sqlString.Append("              WHERE CUS =   ( SELECT KEY_1" + "\n");
                sqlString.Append("                                                         FROM MGCMTBLDAT@RPTTOMES" + "\n");
                sqlString.Append("                                                       WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                sqlString.Append("                                                           AND TABLE_NAME = 'H_CUSTOMER'" + "\n");
                sqlString.Append("                                                           AND DATA_1 = '" + rowCustomStr + "' )" + "\n");
            }
            else sqlString.Append("             WHERE CUS = '" + strCustomer + "'" + "\n");

            sqlString.Append("    AND MAT_GRP_9 = '" + rowMajorStr + "'" + "\n");
            sqlString.Append("    AND MAT_GRP_10 = '" + rowPKGStr + "'" + "\n");

            sqlString.Append(" GROUP BY  CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11," + "\n");
            sqlString.Append("             DECODE(TTLSEQ.SEQ, 1, 'WIP_QTY', 2, 'LOT_CNT', 3, 'RUN_QTY', 'WAIT_QTY')" + "\n");
            sqlString.Append(" ORDER BY  CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11, " + "\n");
            sqlString.Append("             DECODE(GUBUN, 'WIP_QTY', 1,  'LOT_CNT', 2,  'RUN_QTY', 3, 4)" + "\n");
    

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(sqlString.ToString());
            }

            return sqlString.ToString();

        }

        private string popupWindowMergeWBSQL(string rowCustomStr, string rowMajorStr, string rowPKGStr, string rowGubunStr, string colChipStr, string colStepStr)
        {
            StringBuilder sqlString = new StringBuilder();
            string strCustomer = "";

            switch (rowCustomStr)
            {
                case "SEC":
                    strCustomer = "SE";
                    break;
                case "HYNIX":
                    strCustomer = "HX";
                    break;
                case "iML":
                    strCustomer = "IM";
                    break;
                case "FCI":
                    strCustomer = "FC";
                    break;
                case "IMAGIS":
                    strCustomer = "IG";
                    break;
                default:
                    strCustomer = "-";
                    break;
            }

            //sqlString.Append(" SELECT  CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11," + "\n");

            sqlString.Append(" SELECT  MAT_GRP_10, MAT_GRP_6, MAT_CMF_11," + "\n");
            sqlString.Append("             DECODE(TTLSEQ.SEQ, 1, 'WIP_QTY', 2, 'LOT_CNT', 3, 'RUN_QTY', 'WAIT_QTY') GUBUN," + "\n");
            sqlString.Append("             SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(WB_1, 0), 0 )  )  WB_1," + "\n");
            sqlString.Append("             SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(WB_2, 0), 0 )  )  WB_2," + "\n");
            sqlString.Append("             SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(WB_3, 0), 0 )  )  WB_3" + "\n");
            sqlString.Append("    FROM (" + "\n");
            sqlString.Append(" SELECT  A.MAT_GRP_1 CUS, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");
            sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1,0), 'Merge', NVL(WB_1,0), 0)" + "\n");
            sqlString.Append("                  WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_1,0) ELSE 0 END" + "\n");
            sqlString.Append("                  ELSE NVL(WB_1,0)" + "\n");
            sqlString.Append("                END),0) ) ) AS WB_1" + "\n");
            sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_2,0), 'Merge', NVL(WB_2,0), 0)" + "\n");
            sqlString.Append("                  WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_2,0) ELSE 0 END" + "\n");
            sqlString.Append("                  ELSE NVL(WB_2,0)" + "\n");
            sqlString.Append("                END),0) ) ) AS WB_2" + "\n");
            sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_3+GATE_0,0), 'Merge', NVL(WB_3+GATE_0,0), 0)" + "\n");
            sqlString.Append("                  WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_3+GATE_0,0) ELSE 0 END" + "\n");
            sqlString.Append("                  ELSE NVL(WB_3+GATE_0,0)" + "\n");
            sqlString.Append("                END),0) ) ) AS WB_3" + "\n");
            sqlString.Append("     FROM MWIPMATDEF A," + "\n");
            sqlString.Append("               ( SELECT  '1_WIP_QTY' GUBUN, LOT.MAT_ID" + "\n");
            sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1" + "\n");

            if (colChipStr.Equals("Primary Chip"))
            {
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0500', 'A0501')  THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_1" + "\n");
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0550', 'A0551')  THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_2" + "\n");
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A0600', 'A0601') THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_3" + "\n");
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0800', 'A0801')  THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) GATE_0" + "\n");
            }
            else
            {
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A050" + colChipStr.Substring(0, 1) + "') THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_1" + "\n");
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A055" + colChipStr.Substring(0, 1) + "') THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_2" + "\n");
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A060" + colChipStr.Substring(0, 1) + "') THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_3 " + "\n");
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A080" + colChipStr.Substring(0, 1) + "') THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) GATE_0" + "\n");
            }

            sqlString.Append("                   FROM (   SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1, SUM(A.QTY_1) QTY " + "\n");
            sqlString.Append("                                    FROM RWIPLOTSTS A, MWIPOPRDEF B" + "\n");
            sqlString.Append("                                  WHERE A.FACTORY = B.FACTORY(+)" + "\n");
            sqlString.Append("                                       AND A.OPER = B.OPER(+)" + "\n");
            sqlString.Append("                                       AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                                       AND A.LOT_TYPE = 'W'" + "\n");
            sqlString.Append("                                       AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
            sqlString.Append("                                       AND A.LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                                   GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT" + "\n");
            sqlString.Append("                 WHERE LOT.FACTORY = MAT.FACTORY" + "\n");
            sqlString.Append("                      AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
            sqlString.Append("                      AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' " + "\n");
            sqlString.Append("                      AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            sqlString.Append("                      AND MAT.MAT_GRP_2 <> '-'" + "\n");
            sqlString.Append("                  GROUP BY  LOT.MAT_ID ) B," + "\n");
            sqlString.Append("               ( SELECT KEY_1,DATA_1" + "\n");
            sqlString.Append("                    FROM MGCMTBLDAT" + "\n");
            sqlString.Append("                  WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                      AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')  ) G " + "\n");
            sqlString.Append("    WHERE B.MAT_ID = A.MAT_ID(+)" + "\n");
            sqlString.Append("        AND B.MAT_ID = G.KEY_1(+)" + "\n");
            sqlString.Append("        AND B.MAT_ID LIKE '%'" + "\n");
            sqlString.Append("        AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("    GROUP BY A.MAT_GRP_1, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");
            sqlString.Append("    UNION ALL" + "\n");
            sqlString.Append("    SELECT CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11, GUBUN," + "\n");

            if (colChipStr.Equals("Primary Chip"))
            {
                sqlString.Append("                SUM(CASE WHEN OPER IN ('A0500', 'A0501') THEN LOT_CNT ELSE 0 END) WB_1," + "\n");
                sqlString.Append("                SUM(CASE WHEN OPER IN ('A0550', 'A0551') THEN LOT_CNT ELSE 0 END) WB_2," + "\n");
                sqlString.Append("                SUM(CASE WHEN OPER IN ('A0600', 'A0601') THEN LOT_CNT ELSE 0 END) WB_3" + "\n");
            }
            else
            {
                sqlString.Append("                SUM(DECODE( OPER, 'A050" + colChipStr.Substring(0, 1) + "', LOT_CNT, 0)) WB_1," + "\n");
                sqlString.Append("                SUM(DECODE( OPER, 'A055" + colChipStr.Substring(0, 1) + "', LOT_CNT, 0)) WB_2," + "\n");
                sqlString.Append("                SUM(DECODE( OPER, 'A060" + colChipStr.Substring(0, 1) + "', LOT_CNT, 0)) WB_3" + "\n");
            }
            sqlString.Append("       FROM ( SELECT '2_LOT_QTY'  GUBUN, MAT.MAT_GRP_1 CUS, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11, LOT.MAT_ID, LOT.OPER, COUNT(LOT.LOT_ID) LOT_CNT" + "\n");
            sqlString.Append("                      FROM RWIPLOTSTS LOT,  MWIPMATDEF MAT" + "\n");
            sqlString.Append("                   WHERE  LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                       AND LOT.LOT_TYPE = 'W'" + "\n");
            sqlString.Append("                       AND LOT.LOT_CMF_5 LIKE 'P%'" + "\n");
            sqlString.Append("                       AND LOT.LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                       AND ( LOT.OPER LIKE  'A050%' OR LOT.OPER LIKE  'A055%' OR  LOT.OPER LIKE  'A060%' )" + "\n");
            sqlString.Append("                       AND LOT.FACTORY = MAT.FACTORY" + "\n");
            sqlString.Append("                       AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
            sqlString.Append("                       AND MAT.MAT_TYPE = 'FG'" + "\n");
            sqlString.Append("                       AND MAT.DELETE_FLAG = ' '" + "\n");
            sqlString.Append("                       AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' " + "\n");
            sqlString.Append("                   GROUP BY  MAT.MAT_GRP_1 , MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11, LOT.MAT_ID, LOT.OPER )" + "\n");
            sqlString.Append("     GROUP BY CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11, GUBUN" + "\n");
            sqlString.Append("  UNION ALL" + "\n");
            sqlString.Append("  SELECT  A.MAT_GRP_1 CUS, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");
            sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1,0), 'Merge', NVL(WB_1,0), 0)" + "\n");
            sqlString.Append("                  WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_1,0) ELSE 0 END" + "\n");
            sqlString.Append("                  ELSE NVL(WB_1,0)" + "\n");
            sqlString.Append("                END),0) ) ) AS WB_1" + "\n");
            sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_2,0), 'Merge', NVL(WB_2,0), 0)" + "\n");
            sqlString.Append("                  WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_2,0) ELSE 0 END" + "\n");
            sqlString.Append("                  ELSE NVL(WB_2,0)" + "\n");
            sqlString.Append("                END),0) ) ) AS WB_2" + "\n");
            sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_3+GATE_0,0), 'Merge', NVL(WB_3+GATE_0,0), 0)" + "\n");
            sqlString.Append("                  WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_3+GATE_0,0) ELSE 0 END" + "\n");
            sqlString.Append("                  ELSE NVL(WB_3+GATE_0,0)" + "\n");
            sqlString.Append("                END),0) ) ) AS WB_3" + "\n");
            sqlString.Append("     FROM MWIPMATDEF A," + "\n");
            sqlString.Append("               ( SELECT  '3_WIP_QTY' GUBUN, LOT.MAT_ID" + "\n");
            sqlString.Append("                              , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1,  0 DA_1" + "\n");

            if (colChipStr.Equals("Primary Chip"))
            {
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0500', 'A0501')  THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_1" + "\n");
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0550', 'A0551')  THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_2" + "\n");
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A0600', 'A0601') THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_3" + "\n");
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0800', 'A0801')  THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) GATE_0" + "\n");
            }
            else
            {
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A050" + colChipStr.Substring(0, 1) + "')  THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_1" + "\n");
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A055" + colChipStr.Substring(0, 1) + "')  THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_2" + "\n");
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A060" + colChipStr.Substring(0, 1) + "')  THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_3 " + "\n");
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A080" + colChipStr.Substring(0, 1) + "')  THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) GATE_0" + "\n");
            }

            sqlString.Append("                   FROM (   SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1, SUM(A.QTY_1) QTY" + "\n");
            sqlString.Append("                                    FROM RWIPLOTSTS A, MWIPOPRDEF B " + "\n");
            sqlString.Append("                                  WHERE A.FACTORY = B.FACTORY(+)" + "\n");
            sqlString.Append("                                       AND A.OPER = B.OPER(+)" + "\n");
            sqlString.Append("                                       AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                                       AND A.LOT_TYPE = 'W'" + "\n");
            sqlString.Append("                                       AND A.LOT_STATUS = 'PROC'" + "\n");
            sqlString.Append("                                       AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
            sqlString.Append("                                       AND A.LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                                   GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT" + "\n");
            sqlString.Append("                 WHERE LOT.FACTORY = MAT.FACTORY" + "\n");
            sqlString.Append("                      AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
            sqlString.Append("                      AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' " + "\n");
            sqlString.Append("                      AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            sqlString.Append("                      AND MAT.MAT_GRP_2 <> '-'" + "\n");
            sqlString.Append("                  GROUP BY  LOT.MAT_ID ) B," + "\n");
            sqlString.Append("               ( SELECT KEY_1,DATA_1" + "\n");
            sqlString.Append("                    FROM MGCMTBLDAT" + "\n");
            sqlString.Append("                  WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                      AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')  ) G" + "\n");
            sqlString.Append("    WHERE B.MAT_ID = A.MAT_ID(+) " + "\n");
            sqlString.Append("        AND B.MAT_ID = G.KEY_1(+)" + "\n");
            sqlString.Append("        AND B.MAT_ID LIKE '%'" + "\n");
            sqlString.Append("        AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("    GROUP BY A.MAT_GRP_1, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");
            sqlString.Append("  UNION ALL " + "\n");
            sqlString.Append("  SELECT  A.MAT_GRP_1 CUS, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");
            sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_1,0), 'Merge', NVL(WB_1,0), 0)" + "\n");
            sqlString.Append("                  WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_1,0) ELSE 0 END" + "\n");
            sqlString.Append("                  ELSE NVL(WB_1,0)" + "\n");
            sqlString.Append("                END),0) ) ) AS WB_1" + "\n");
            sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_2,0), 'Merge', NVL(WB_2,0), 0)" + "\n");
            sqlString.Append("                  WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_2,0) ELSE 0 END" + "\n");
            sqlString.Append("                  ELSE NVL(WB_2,0)" + "\n");
            sqlString.Append("                END),0) ) ) AS WB_2" + "\n");
            sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', NVL(WB_3+GATE_0,0), 'Merge', NVL(WB_3+GATE_0,0), 0)" + "\n");
            sqlString.Append("                  WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(WB_3+GATE_0,0) ELSE 0 END" + "\n");
            sqlString.Append("                  ELSE NVL(WB_3+GATE_0,0)" + "\n");
            sqlString.Append("                END),0) ) ) AS WB_3" + "\n");
            sqlString.Append("     FROM MWIPMATDEF A," + "\n");
            sqlString.Append("               ( SELECT  '4_WAIT_QTY' GUBUN, LOT.MAT_ID" + "\n");
            sqlString.Append("                              , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1" + "\n");

            if (colChipStr.Equals("Primary Chip"))
            {
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0500', 'A0501')  THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_1" + "\n");
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A0550', 'A0551')  THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_2" + "\n");
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A0600', 'A0601') THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_3" + "\n");
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A0800', 'A0801')  THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) GATE_0" + "\n");
            }
            else
            {
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A050" + colChipStr.Substring(0, 1) + "')  THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_1" + "\n");
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ('A055" + colChipStr.Substring(0, 1) + "')  THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_2" + "\n");
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'W/B', CASE WHEN OPER IN ( 'A060" + colChipStr.Substring(0, 1) + "')  THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) WB_3 " + "\n");
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'GATE', CASE WHEN OPER IN ('A080" + colChipStr.Substring(0, 1) + "')  THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) GATE_0" + "\n");
            }

            sqlString.Append("                   FROM (   SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1, SUM(A.QTY_1) QTY " + "\n");
            sqlString.Append("                                    FROM RWIPLOTSTS A, MWIPOPRDEF B " + "\n");
            sqlString.Append("                                  WHERE A.FACTORY = B.FACTORY(+)" + "\n");
            sqlString.Append("                                       AND A.OPER = B.OPER(+)" + "\n");
            sqlString.Append("                                       AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                                       AND A.LOT_TYPE = 'W' " + "\n");
            sqlString.Append("                                       AND A.LOT_STATUS = 'WAIT'" + "\n");
            sqlString.Append("                                       AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
            sqlString.Append("                                       AND A.LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                                   GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT" + "\n");
            sqlString.Append("                 WHERE LOT.FACTORY = MAT.FACTORY" + "\n");
            sqlString.Append("                      AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
            sqlString.Append("                      AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' " + "\n");
            sqlString.Append("                      AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            sqlString.Append("                      AND MAT.MAT_GRP_2 <> '-'" + "\n");
            sqlString.Append("                  GROUP BY  LOT.MAT_ID ) B," + "\n");
            sqlString.Append("               ( SELECT KEY_1,DATA_1" + "\n");
            sqlString.Append("                    FROM MGCMTBLDAT" + "\n");
            sqlString.Append("                  WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                      AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')  ) G" + "\n");
            sqlString.Append("    WHERE B.MAT_ID = A.MAT_ID(+)" + "\n");
            sqlString.Append("        AND B.MAT_ID = G.KEY_1(+)" + "\n");
            sqlString.Append("        AND B.MAT_ID LIKE '%'" + "\n");
            sqlString.Append("        AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("    GROUP BY A.MAT_GRP_1, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN ) A," + "\n");
            sqlString.Append("    ( SELECT SEQ FROM HRTDSUMSEQ@RPTTOMES WHERE SEQ < 5 ) TTLSEQ" + "\n");

            if (strCustomer.Equals("-"))
            {
                sqlString.Append("              WHERE CUS =   ( SELECT KEY_1" + "\n");
                sqlString.Append("                                                         FROM MGCMTBLDAT@RPTTOMES" + "\n");
                sqlString.Append("                                                       WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                sqlString.Append("                                                           AND TABLE_NAME = 'H_CUSTOMER'" + "\n");
                sqlString.Append("                                                           AND DATA_1 = '" + rowCustomStr + "' )" + "\n");
            }
            else sqlString.Append("             WHERE CUS = '" + strCustomer + "'" + "\n");

            sqlString.Append("    AND MAT_GRP_9 = '" + rowMajorStr + "'" + "\n");
            sqlString.Append("    AND MAT_GRP_10 = '" + rowPKGStr + "'" + "\n");

            sqlString.Append(" GROUP BY  CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11," + "\n");
            sqlString.Append("             DECODE(TTLSEQ.SEQ, 1, 'WIP_QTY', 2, 'LOT_CNT', 3, 'RUN_QTY', 'WAIT_QTY')" + "\n");
            sqlString.Append(" ORDER BY  CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11, " + "\n");
            sqlString.Append("             DECODE(GUBUN, 'WIP_QTY', 1,  'LOT_CNT', 2,  'RUN_QTY', 3, 4)" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(sqlString.ToString());
            }

            return sqlString.ToString();

        }

        private string popupWindowMergeDASQL(string rowCustomStr, string rowMajorStr, string rowPKGStr, string rowGubunStr, string colChipStr, string colStepStr)
        {
            StringBuilder sqlString = new StringBuilder();
            string strCustomer = "";

            switch (rowCustomStr)
            {
                case "SEC":
                    strCustomer = "SE";
                    break;
                case "HYNIX":
                    strCustomer = "HX";
                    break;
                case "iML":
                    strCustomer = "IM";
                    break;
                case "FCI":
                    strCustomer = "FC";
                    break;
                case "IMAGIS":
                    strCustomer = "IG";
                    break;
                default:
                    strCustomer = "-";
                    break;
            }

            //sqlString.Append(" SELECT  CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11," + "\n");

            sqlString.Append(" SELECT  MAT_GRP_10, MAT_GRP_6, MAT_CMF_11," + "\n");
            sqlString.Append("             DECODE(TTLSEQ.SEQ, 1, 'WIP_QTY', 2, 'LOT_CNT', 3, 'RUN_QTY', 'WAIT_QTY') GUBUN," + "\n");
            sqlString.Append("             SUM( DECODE( TTLSEQ.SEQ, TO_NUMBER( SUBSTR( GUBUN, 1,1) ),  NVL(DA_1, 0), 0 )  )  DA_1" + "\n");
            sqlString.Append("    FROM (" + "\n");
            sqlString.Append(" SELECT  A.MAT_GRP_1 CUS, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");
            sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN " + "\n");
            sqlString.Append("                              DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 0)" + "\n");
            sqlString.Append("                          WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)  ELSE 0 END" + "\n");
            sqlString.Append("                          ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)" + "\n");
            sqlString.Append("                         END), 0)  ) ) AS DA_1" + "\n");

            sqlString.Append("     FROM MWIPMATDEF A," + "\n");
            sqlString.Append("               ( SELECT  '1_WIP_QTY' GUBUN, LOT.MAT_ID" + "\n");
            sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1" + "\n");

            if (colChipStr.Equals("Primary Chip"))
            {
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0250', 'A0305', 'A0306', 'A0310', 'A0400', 'A0401', 'A0500', 'A0501', 'A0530', 'A0531') THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) DA_1" + "\n");
            }
            else
            {
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A040" + colChipStr.Substring(0, 1) + "','A050" + colChipStr.Substring(0, 1) + "', 'A053" + colChipStr.Substring(0, 1) + "') THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) DA_1" + "\n");
            }
            sqlString.Append("                   FROM (   SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1, SUM(A.QTY_1) QTY" + "\n");
            sqlString.Append("                                    FROM RWIPLOTSTS A, MWIPOPRDEF B" + "\n");
            sqlString.Append("                                  WHERE A.FACTORY = B.FACTORY(+)" + "\n");
            sqlString.Append("                                       AND A.OPER = B.OPER(+)" + "\n");
            sqlString.Append("                                       AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                                       AND A.LOT_TYPE = 'W'" + "\n");
            sqlString.Append("                                       AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
            sqlString.Append("                                       AND A.LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                                   GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT" + "\n");
            sqlString.Append("                 WHERE LOT.FACTORY = MAT.FACTORY" + "\n");
            sqlString.Append("                      AND LOT.MAT_ID = MAT.MAT_ID" + "\n");

            if (rowGubunStr.Equals("CHIP"))
                sqlString.Append("  AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
            else sqlString.Append("                     AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' " + "\n");

            sqlString.Append("                      AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            sqlString.Append("                      AND MAT.MAT_GRP_2 <> '-'" + "\n");
            sqlString.Append("                  GROUP BY  LOT.MAT_ID ) B," + "\n");
            sqlString.Append("               ( SELECT KEY_1,DATA_1" + "\n");
            sqlString.Append("                    FROM MGCMTBLDAT" + "\n");
            sqlString.Append("                  WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                      AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')  ) G " + "\n");
            sqlString.Append("    WHERE B.MAT_ID = A.MAT_ID(+) " + "\n");
            sqlString.Append("        AND B.MAT_ID = G.KEY_1(+)" + "\n");
            sqlString.Append("        AND B.MAT_ID LIKE '%'" + "\n");
            sqlString.Append("        AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("    GROUP BY A.MAT_GRP_1, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");
            sqlString.Append("    UNION ALL" + "\n");
            sqlString.Append("    SELECT CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11, GUBUN," + "\n");

            if (colChipStr.Equals("Primary Chip")) 
                 sqlString.Append("                SUM( CASE WHEN OPER IN ('A0250', 'A0305', 'A0306', 'A0310', 'A0400', 'A0401', 'A0500', 'A0501', 'A0530', 'A0531') THEN LOT_CNT ELSE 0 END) DA_1" + "\n");
            else
                 sqlString.Append("                SUM( CASE WHEN OPER IN ('A040" + colChipStr.Substring(0, 1) + "','A050" + colChipStr.Substring(0, 1) + "', 'A053" + colChipStr.Substring(0, 1) + "') THEN LOT_CNT ELSE 0 END) DA_1" + "\n");

            sqlString.Append("       FROM ( SELECT '2_LOT_QTY'  GUBUN, MAT.MAT_GRP_1 CUS, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11, LOT.MAT_ID, LOT.OPER, COUNT(LOT.LOT_ID) LOT_CNT" + "\n");
            sqlString.Append("                      FROM RWIPLOTSTS LOT,  MWIPMATDEF MAT" + "\n");
            sqlString.Append("                   WHERE  LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                       AND LOT.LOT_TYPE = 'W' " + "\n");
            sqlString.Append("                       AND LOT.LOT_CMF_5 LIKE 'P%'" + "\n");
            sqlString.Append("                       AND LOT.LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                       AND LOT.OPER = 'A040" + colChipStr.Substring(0, 1) + "'" + "\n");
            sqlString.Append("                       AND LOT.FACTORY = MAT.FACTORY" + "\n");
            sqlString.Append("                       AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
            sqlString.Append("                       AND MAT.MAT_TYPE = 'FG'" + "\n");
            sqlString.Append("                       AND MAT.DELETE_FLAG = ' '" + "\n");

            if (rowGubunStr.Equals("CHIP"))
                sqlString.Append("  AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
            else sqlString.Append("                     AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' " + "\n");

            sqlString.Append("                   GROUP BY  MAT.MAT_GRP_1 , MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_GRP_6, MAT.MAT_CMF_11, LOT.MAT_ID, LOT.OPER )" + "\n");
            sqlString.Append("     GROUP BY CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11, GUBUN" + "\n");
            sqlString.Append("  UNION ALL" + "\n");
            sqlString.Append("  SELECT  A.MAT_GRP_1 CUS, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");
            sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN " + "\n");
            sqlString.Append("                              DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 0)" + "\n");
            sqlString.Append("                          WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)  ELSE 0 END" + "\n");
            sqlString.Append("                          ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)" + "\n");
            sqlString.Append("                         END), 0)  ) ) AS DA_1" + "\n");

            sqlString.Append("     FROM MWIPMATDEF A," + "\n");
            sqlString.Append("               ( SELECT  '3_WIP_QTY' GUBUN, LOT.MAT_ID" + "\n");
            sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1" + "\n");

            if (colChipStr.Equals("Primary Chip"))
            {
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0250', 'A0305', 'A0306', 'A0310', 'A0400', 'A0401', 'A0500', 'A0501', 'A0530', 'A0531') THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) DA_1" + "\n");
            }
            else
            {
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A040" + colChipStr.Substring(0, 1) + "','A050" + colChipStr.Substring(0, 1) + "', 'A053" + colChipStr.Substring(0, 1) + "') THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) DA_1" + "\n");
            }
            sqlString.Append("                   FROM (   SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1, SUM(A.QTY_1) QTY " + "\n");
            sqlString.Append("                                    FROM RWIPLOTSTS A, MWIPOPRDEF B" + "\n");
            sqlString.Append("                                  WHERE A.FACTORY = B.FACTORY(+) " + "\n");
            sqlString.Append("                                       AND A.OPER = B.OPER(+)" + "\n");
            sqlString.Append("                                       AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                                       AND A.LOT_TYPE = 'W' " + "\n");
            sqlString.Append("                                       AND A.LOT_STATUS = 'PROC'" + "\n");
            sqlString.Append("                                       AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
            sqlString.Append("                                       AND A.LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                                   GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT" + "\n");
            sqlString.Append("                 WHERE LOT.FACTORY = MAT.FACTORY" + "\n");
            sqlString.Append("                      AND LOT.MAT_ID = MAT.MAT_ID" + "\n");

            if (rowGubunStr.Equals("CHIP"))
                sqlString.Append("  AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
            else sqlString.Append("                     AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' " + "\n");

            sqlString.Append("                      AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            sqlString.Append("                      AND MAT.MAT_GRP_2 <> '-'" + "\n");
            sqlString.Append("                  GROUP BY  LOT.MAT_ID ) B," + "\n");
            sqlString.Append("               ( SELECT KEY_1,DATA_1" + "\n");
            sqlString.Append("                    FROM MGCMTBLDAT" + "\n");
            sqlString.Append("                  WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            sqlString.Append("                      AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')  ) G  " + "\n");
            sqlString.Append("    WHERE B.MAT_ID = A.MAT_ID(+) " + "\n");
            sqlString.Append("        AND B.MAT_ID = G.KEY_1(+)" + "\n");
            sqlString.Append("        AND B.MAT_ID LIKE '%'" + "\n");
            sqlString.Append("        AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("    GROUP BY A.MAT_GRP_1, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");
            sqlString.Append("  UNION ALL" + "\n");
            sqlString.Append("  SELECT  A.MAT_GRP_1 CUS, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN" + "\n");

            sqlString.Append("               , ROUND( SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN " + "\n");
            sqlString.Append("                              DECODE(A.MAT_GRP_5, '2nd', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 'Merge', NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0), 0)" + "\n");
            sqlString.Append("                          WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)  ELSE 0 END" + "\n");
            sqlString.Append("                          ELSE NVL(SP_1,0)/NVL(G.DATA_1,1)+NVL(DA_1,0)" + "\n");
            sqlString.Append("                         END), 0)  ) ) AS DA_1" + "\n");


            sqlString.Append("     FROM MWIPMATDEF A," + "\n");
            sqlString.Append("               ( SELECT  '4_WAIT_QTY' GUBUN, LOT.MAT_ID" + "\n");
            sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY), 0)) SP_1" + "\n");

            if (colChipStr.Equals("Primary Chip"))
            {
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A0250', 'A0305', 'A0306', 'A0310', 'A0400', 'A0401', 'A0500', 'A0501', 'A0530', 'A0531') THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) DA_1" + "\n");
            }
            else
            {
                sqlString.Append("                             , SUM(DECODE(OPER_GRP_1, 'D/A', CASE WHEN OPER IN ('A040" + colChipStr.Substring(0, 1) + "','A050" + colChipStr.Substring(0, 1) + "', 'A053" + colChipStr.Substring(0, 1) + "') THEN" + "\n");
                sqlString.Append("                                                                                           DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1, '-', 1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,'-', 1,MAT.MAT_CMF_13)),0),QTY)" + "\n");
                sqlString.Append("                                                                                 ELSE 0  END, 0) ) DA_1" + "\n");
            }

            sqlString.Append("                   FROM (   SELECT A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1, SUM(A.QTY_1) QTY " + "\n");
            sqlString.Append("                                    FROM RWIPLOTSTS A, MWIPOPRDEF B " + "\n");
            sqlString.Append("                                  WHERE A.FACTORY = B.FACTORY(+) " + "\n");
            sqlString.Append("                                       AND A.OPER = B.OPER(+)" + "\n");
            sqlString.Append("                                       AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                                       AND A.LOT_TYPE = 'W' " + "\n");
            sqlString.Append("                                       AND A.LOT_STATUS = 'WAIT'" + "\n");
            sqlString.Append("                                       AND A.LOT_CMF_5 LIKE 'P%'" + "\n");
            sqlString.Append("                                       AND A.LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                                   GROUP BY A.FACTORY, A.MAT_ID, B.OPER, B.OPER_GRP_1 ) LOT, MWIPMATDEF MAT" + "\n");
            sqlString.Append("                 WHERE LOT.FACTORY = MAT.FACTORY" + "\n");
            sqlString.Append("                      AND LOT.MAT_ID = MAT.MAT_ID" + "\n");

            if (rowGubunStr.Equals("CHIP"))
                sqlString.Append("  AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 IN ( '2nd',  '3rd', '4th', '5th', '6th',  '7th', '8th', '9th')" + "\n");
            else sqlString.Append("                     AND MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' " + "\n");

            sqlString.Append("                      AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            sqlString.Append("                      AND MAT.MAT_GRP_2 <> '-'" + "\n");
            sqlString.Append("                  GROUP BY  LOT.MAT_ID ) B," + "\n");
            sqlString.Append("               ( SELECT KEY_1,DATA_1" + "\n");
            sqlString.Append("                    FROM MGCMTBLDAT" + "\n");
            sqlString.Append("                  WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            sqlString.Append("                      AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS')  ) G  " + "\n");
            sqlString.Append("    WHERE B.MAT_ID = A.MAT_ID(+) " + "\n");
            sqlString.Append("        AND B.MAT_ID = G.KEY_1(+)" + "\n");
            sqlString.Append("        AND B.MAT_ID LIKE '%'" + "\n");
            sqlString.Append("        AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("    GROUP BY A.MAT_GRP_1, A.MAT_GRP_9, A.MAT_GRP_10, A.MAT_GRP_6, A.MAT_CMF_11, GUBUN ) A," + "\n");
            sqlString.Append("    ( SELECT SEQ FROM HRTDSUMSEQ@RPTTOMES WHERE SEQ < 5 ) TTLSEQ" + "\n");

            if (strCustomer.Equals("-"))
            {
                sqlString.Append("              WHERE CUS =   ( SELECT KEY_1" + "\n");
                sqlString.Append("                                                         FROM MGCMTBLDAT@RPTTOMES" + "\n");
                sqlString.Append("                                                       WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                sqlString.Append("                                                           AND TABLE_NAME = 'H_CUSTOMER'" + "\n");
                sqlString.Append("                                                           AND DATA_1 = '" + rowCustomStr + "' )" + "\n");
            }
            else sqlString.Append("             WHERE CUS = '" + strCustomer + "'" + "\n");

            sqlString.Append("    AND MAT_GRP_9 = '" + rowMajorStr + "'" + "\n");
            sqlString.Append("    AND MAT_GRP_10 = '" + rowPKGStr + "'" + "\n");

            sqlString.Append(" GROUP BY  CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11," + "\n");
            sqlString.Append("             DECODE(TTLSEQ.SEQ, 1, 'WIP_QTY', 2, 'LOT_CNT', 3, 'RUN_QTY', 'WAIT_QTY')" + "\n");
            sqlString.Append(" ORDER BY  CUS, MAT_GRP_9, MAT_GRP_10, MAT_GRP_6, MAT_CMF_11, " + "\n");
            sqlString.Append("             DECODE(GUBUN, 'WIP_QTY', 1,  'LOT_CNT', 2,  'RUN_QTY', 3, 4)" + "\n");


            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(sqlString.ToString());
            }

            return sqlString.ToString();

        }

        private string popupWindowDAEQPSQL(string rowCustomStr, string rowMajorStr, string rowPKGStr, string colChipStr)
        {
            StringBuilder sqlString = new StringBuilder();
            string strCustomer = "";
            
            switch (rowCustomStr)
            {
                case "SEC" : 
                      strCustomer = "SE";
                      break;
                case "HYNIX" : 
                      strCustomer = "HX";
                      break;
                case "iML" : 
                      strCustomer = "IM";
                      break;
                case "FCI" : 
                      strCustomer = "FC";
                      break;
                case "IMAGIS": 
                      strCustomer = "IG";
                      break;
                default :
                      strCustomer = "-";
                      break;
            }

            sqlString.Append("SELECT DECODE(RES_MODEL, 'Z TOTAL', 'TOTAL', RES_MODEL) RES_MODEL, RES_CNT, UPEH, CAPA, WAIT_CNT, RUN_CNT, RES_DOWN_CNT, DEV_CHANG_CNT" + "\n");
            sqlString.Append("  FROM ( SELECT  DECODE(TTLSEQ.SEQ, 2, 'Z TOTAL', RAS.RES_MODEL) RES_MODEL, SUM(NVL(RAS.RES_CNT, 0)) RES_CNT, MAX( NVL(UPEH.UPEH, 0) ) UPEH," + "\n");
            sqlString.Append("                           SUM( DECODE(SUBSTR(RAS.OPER, 1, 3), 'A04',   NVL(RAS.RES_CNT, 0) *  NVL(UPEH.UPEH,0) * 24 *0.75,  NVL(RAS.RES_CNT, 0) *  NVL(UPEH.UPEH,0) * 24 *0.8  ) ) CAPA," + "\n");
            sqlString.Append("                           SUM(RAS.WAIT_CNT)  WAIT_CNT, SUM(RAS.RUN_CNT) RUN_CNT, SUM(RAS.RES_DOWN_CNT) RES_DOWN_CNT, SUM(RAS.DEV_CHANG_CNT) DEV_CHANG_CNT" + "\n");
            sqlString.Append("                 FROM ( SELECT RAS.FACTORY, RAS.RES_GRP_6 RES_MODEL, MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10,  RAS.RES_STS_2 MAT_ID, RAS.RES_STS_8 OPER , COUNT(RES_ID) RES_CNT," + "\n");
            sqlString.Append("                                         NVL(SUM(DECODE(RES_UP_DOWN_FLAG, 'U', DECODE(NVL(LOT.LOT_ID, '-'), '-', 1, 0))), 0)  WAIT_CNT," + "\n");
            sqlString.Append("                                         SUM( DECODE(NVL(LOT.LOT_ID, '-'), '-', 0, 1 ) ) RUN_CNT," + "\n");
            sqlString.Append("                                         SUM(DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(NVL(SUBSTR(RES_STS_1, 1,1), '-'), 'D', 0, 1), 0) ) RES_DOWN_CNT," + "\n");
            sqlString.Append("                                         SUM(DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(NVL(SUBSTR(RES_STS_1, 1,1), '-'), 'D', 1, 0), 0) ) DEV_CHANG_CNT" + "\n");
            sqlString.Append("                              FROM  MRASRESDEF RAS," + "\n");
            sqlString.Append("                                        ( SELECT *" + "\n");
            sqlString.Append("                                             FROM MWIPLOTSTS LOT" + "\n");
            sqlString.Append("                                           WHERE LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                                                AND LOT.LOT_TYPE = 'W' AND LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                                                AND LOT.LOT_CMF_5 LIKE 'P%' " + "\n");
            sqlString.Append("                                                AND LOT.OPER LIKE 'A040%' " + "\n");
            sqlString.Append("                                                AND LOT.LOT_STATUS = 'PROC' ) LOT, MWIPMATDEF MAT" + "\n");
            sqlString.Append("                         WHERE RAS.FACTORY   = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                             AND RAS.RES_CMF_9 = 'Y'" + "\n");
            sqlString.Append("                             AND RAS.DELETE_FLAG  = ' '" + "\n");
            sqlString.Append("                             AND RAS.RES_TYPE  = 'EQUIPMENT'" + "\n");
            sqlString.Append("                             AND RAS.RES_STS_8 LIKE 'A040%' " + "\n");
            sqlString.Append("                             AND RAS.FACTORY = LOT.FACTORY(+)" + "\n");
            sqlString.Append("                             AND RAS.RES_ID = LOT.START_RES_ID(+)" + "\n");
            sqlString.Append("                             AND RAS.FACTORY = MAT.FACTORY(+)" + "\n");
            sqlString.Append("                             AND RAS.RES_STS_2 = MAT.MAT_ID(+)" + "\n");
            sqlString.Append("                             AND MAT.MAT_TYPE = 'FG'" + "\n");
            sqlString.Append("                             AND MAT.DELETE_FLAG = ' '" + "\n");
            sqlString.Append("                             AND MAT.MAT_VER = 1" + "\n");
            sqlString.Append("                         GROUP BY RAS.FACTORY,RAS. RES_STS_2,RAS.RES_GRP_6,RAS.RES_STS_8,MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10  ) RAS, CRASUPHDEF UPEH , ( SELECT SEQ FROM HRTDSUMSEQ@RPTTOMES WHERE SEQ < 3 ) TTLSEQ" + "\n");
            sqlString.Append("          WHERE RAS.FACTORY = UPEH.FACTORY(+)" + "\n");
            sqlString.Append("               AND RAS.RES_MODEL = UPEH.RES_MODEL(+)" + "\n");
            sqlString.Append("               AND RAS.MAT_ID = UPEH.MAT_ID(+)" + "\n");
            sqlString.Append("               AND RAS.OPER = UPEH.OPER(+)" + "\n");

            if (strCustomer.Equals("-"))
            {
                sqlString.Append("              AND RAS.MAT_GRP_1 =   ( SELECT KEY_1" + "\n");
                sqlString.Append("                                                         FROM MGCMTBLDAT@RPTTOMES" + "\n");
                sqlString.Append("                                                       WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                sqlString.Append("                                                           AND TABLE_NAME = 'H_CUSTOMER'" + "\n");
                sqlString.Append("                                                           AND DATA_1 = '" + rowCustomStr + "' )" + "\n");
            }
            else sqlString.Append("              AND RAS.MAT_GRP_1 = '" + strCustomer + "'" + "\n");

            sqlString.Append("              AND RAS.MAT_GRP_9 = '" + rowMajorStr + "'" + "\n");
            sqlString.Append("              AND RAS.MAT_GRP_10 = '" + rowPKGStr + "'" + "\n");
            sqlString.Append("              AND RAS.OPER = 'A040" + colChipStr.Substring(0, 1) + "'" + "\n");

            sqlString.Append("    GROUP BY DECODE(TTLSEQ.SEQ, 2, 'Z TOTAL', RAS.RES_MODEL)" + "\n");
            sqlString.Append("    ORDER BY RES_MODEL" + "\n");
            sqlString.Append("   )" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(sqlString.ToString());
            }

            return sqlString.ToString();

        }


        private string popupWindowWBEQPSQL(string rowCustomStr, string rowMajorStr, string rowPKGStr, string colChipStr)
        {
            StringBuilder sqlString = new StringBuilder();
            string strCustomer = "";

            switch (rowCustomStr)
            {
                case "SEC":
                    strCustomer = "SE";
                    break;
                case "HYNIX":
                    strCustomer = "HX";
                    break;
                case "iML":
                    strCustomer = "IM";
                    break;
                case "FCI":
                    strCustomer = "FC";
                    break;
                case "IMAGIS":
                    strCustomer = "IG";
                    break;
                default:
                    strCustomer = "-";
                    break;
            }

            sqlString.Append("SELECT DECODE(RES_MODEL, 'Z TOTAL', 'TOTAL', RES_MODEL) RES_MODEL, RES_CNT, UPEH, CAPA, WAIT_CNT, RUN_CNT, RES_DOWN_CNT, DEV_CHANG_CNT" + "\n");
            sqlString.Append("  FROM ( SELECT  DECODE(TTLSEQ.SEQ, 2, 'Z TOTAL', RAS.RES_MODEL) RES_MODEL, SUM(NVL(RAS.RES_CNT, 0)) RES_CNT, MAX( NVL(UPEH.UPEH, 0) ) UPEH," + "\n");
            sqlString.Append("                           SUM( DECODE(SUBSTR(RAS.OPER, 1, 3), 'A04',   NVL(RAS.RES_CNT, 0) *  NVL(UPEH.UPEH,0) * 24 *0.75,  NVL(RAS.RES_CNT, 0) *  NVL(UPEH.UPEH,0) * 24 *0.8  ) )CAPA," + "\n");
            sqlString.Append("                           SUM(RAS.WAIT_CNT)  WAIT_CNT, SUM(RAS.RUN_CNT) RUN_CNT, SUM(RAS.RES_DOWN_CNT) RES_DOWN_CNT, SUM(RAS.DEV_CHANG_CNT) DEV_CHANG_CNT" + "\n");
            sqlString.Append("                 FROM ( SELECT RAS.FACTORY, RAS.RES_GRP_6 RES_MODEL, MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10,  RAS.RES_STS_2 MAT_ID, RAS.RES_STS_8 OPER , COUNT(RES_ID) RES_CNT," + "\n");
            sqlString.Append("                                         NVL(SUM(DECODE(RES_UP_DOWN_FLAG, 'U', DECODE(NVL(LOT.LOT_ID, '-'), '-', 1, 0))), 0)  WAIT_CNT," + "\n");
            sqlString.Append("                                         SUM( DECODE(NVL(LOT.LOT_ID, '-'), '-', 0, 1 ) ) RUN_CNT," + "\n");
            sqlString.Append("                                         SUM(DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(NVL(SUBSTR(RES_STS_1, 1,1), '-'), 'D', 0, 1), 0) ) RES_DOWN_CNT," + "\n");
            sqlString.Append("                                         SUM(DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(NVL(SUBSTR(RES_STS_1, 1,1), '-'), 'D', 1, 0), 0) ) DEV_CHANG_CNT" + "\n");
            sqlString.Append("                              FROM  MRASRESDEF RAS," + "\n");
            sqlString.Append("                                        ( SELECT *" + "\n");
            sqlString.Append("                                             FROM MWIPLOTSTS LOT" + "\n");
            sqlString.Append("                                           WHERE LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                                                AND LOT.LOT_TYPE = 'W' AND LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                                                AND LOT.LOT_CMF_5 LIKE 'P%' " + "\n");
            sqlString.Append("                                                AND LOT.OPER LIKE 'A060%' " + "\n");
            sqlString.Append("                                                AND LOT.LOT_STATUS = 'PROC' ) LOT, MWIPMATDEF MAT" + "\n");
            sqlString.Append("                         WHERE RAS.FACTORY   = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                             AND RAS.RES_CMF_9 = 'Y'" + "\n");
            sqlString.Append("                             AND RAS.DELETE_FLAG  = ' '" + "\n");
            sqlString.Append("                             AND RAS.RES_TYPE  = 'EQUIPMENT'" + "\n");
            sqlString.Append("                             AND (RAS.RES_STS_8 LIKE 'A060%')" + "\n");
            sqlString.Append("                             AND RAS.FACTORY = LOT.FACTORY(+)" + "\n");
            sqlString.Append("                             AND RAS.RES_ID = LOT.START_RES_ID(+)" + "\n");
            sqlString.Append("                             AND RAS.FACTORY = MAT.FACTORY(+)" + "\n");
            sqlString.Append("                             AND RAS.RES_STS_2 = MAT.MAT_ID(+)" + "\n");
            sqlString.Append("                             AND MAT.MAT_TYPE = 'FG'" + "\n");
            sqlString.Append("                             AND MAT.DELETE_FLAG = ' '" + "\n");
            sqlString.Append("                             AND MAT.MAT_VER = 1" + "\n");
            sqlString.Append("                         GROUP BY RAS.FACTORY,RAS. RES_STS_2,RAS.RES_GRP_6,RAS.RES_STS_8,MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10  ) RAS, CRASUPHDEF UPEH , ( SELECT SEQ FROM HRTDSUMSEQ@RPTTOMES WHERE SEQ < 3 ) TTLSEQ" + "\n");
            sqlString.Append("          WHERE RAS.FACTORY = UPEH.FACTORY(+)" + "\n");
            sqlString.Append("               AND RAS.RES_MODEL = UPEH.RES_MODEL(+)" + "\n");
            sqlString.Append("               AND RAS.MAT_ID = UPEH.MAT_ID(+)" + "\n");
            sqlString.Append("               AND RAS.OPER = UPEH.OPER(+)" + "\n");

            if (strCustomer.Equals("-"))
            {
                sqlString.Append("              AND RAS.MAT_GRP_1 =   ( SELECT KEY_1" + "\n");
                sqlString.Append("                                                         FROM MGCMTBLDAT@RPTTOMES" + "\n");
                sqlString.Append("                                                       WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                sqlString.Append("                                                           AND TABLE_NAME = 'H_CUSTOMER'" + "\n");
                sqlString.Append("                                                           AND DATA_1 = '" + rowCustomStr + "' )" + "\n");
            }
            else sqlString.Append("              AND RAS.MAT_GRP_1 = '" + strCustomer + "'" + "\n");

            sqlString.Append("              AND RAS.MAT_GRP_9 = '" + rowMajorStr + "'" + "\n");
            sqlString.Append("              AND RAS.MAT_GRP_10 = '" + rowPKGStr + "'" + "\n");
            sqlString.Append("              AND RAS.OPER = 'A060" + colChipStr.Substring(0,1) + "'" + "\n");

            sqlString.Append("    GROUP BY DECODE(TTLSEQ.SEQ, 2, 'Z TOTAL', RAS.RES_MODEL)" + "\n");
            sqlString.Append("    ORDER BY RES_MODEL" + "\n");
            sqlString.Append("   )" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(sqlString.ToString());
            }

            return sqlString.ToString();

        }

        private string popupWindowEQPSQL(string rowCustomStr, string rowMajorStr, string rowPKGStr, string colStepStr)
        {
            StringBuilder sqlString = new StringBuilder();
            string strCustomer = "";

            switch (rowCustomStr)
            {
                case "SEC":
                    strCustomer = "SE";
                    break;
                case "HYNIX":
                    strCustomer = "HX";
                    break;
                case "iML":
                    strCustomer = "IM";
                    break;
                case "FCI":
                    strCustomer = "FC";
                    break;
                case "IMAGIS":
                    strCustomer = "IG";
                    break;
                default:
                    strCustomer = "-";
                    break;
            }

            sqlString.Append("SELECT DECODE(RES_MODEL, 'Z TOTAL', 'TOTAL', RES_MODEL) RES_MODEL, RES_CNT, UPEH, CAPA, WAIT_CNT, RUN_CNT, RES_DOWN_CNT, DEV_CHANG_CNT" + "\n");
            sqlString.Append("  FROM ( SELECT  DECODE(TTLSEQ.SEQ, 2, 'Z TOTAL', RAS.RES_MODEL) RES_MODEL, SUM(NVL(RAS.RES_CNT, 0)) RES_CNT, MAX( NVL(UPEH.UPEH, 0) ) UPEH," + "\n");
            sqlString.Append("                           SUM( DECODE(SUBSTR(RAS.OPER, 1, 3), 'A04',   NVL(RAS.RES_CNT, 0) *  NVL(UPEH.UPEH,0) * 24 *0.75,  NVL(RAS.RES_CNT, 0) *  NVL(UPEH.UPEH,0) * 24 *0.8  ) )CAPA," + "\n");
            sqlString.Append("                           SUM(RAS.WAIT_CNT)  WAIT_CNT, SUM(RAS.RUN_CNT) RUN_CNT, SUM(RAS.RES_DOWN_CNT) RES_DOWN_CNT, SUM(RAS.DEV_CHANG_CNT) DEV_CHANG_CNT" + "\n");
            sqlString.Append("                 FROM ( SELECT RAS.FACTORY, RAS.RES_GRP_6 RES_MODEL, MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10,  RAS.RES_STS_2 MAT_ID, RAS.RES_STS_8 OPER , COUNT(RES_ID) RES_CNT," + "\n");
            sqlString.Append("                                         NVL(SUM(DECODE(RES_UP_DOWN_FLAG, 'U', DECODE(NVL(LOT.LOT_ID, '-'), '-', 1, 0))), 0)  WAIT_CNT," + "\n");
            sqlString.Append("                                         SUM( DECODE(NVL(LOT.LOT_ID, '-'), '-', 0, 1 ) ) RUN_CNT," + "\n");
            sqlString.Append("                                         SUM(DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(NVL(SUBSTR(RES_STS_1, 1,1), '-'), 'D', 0, 1), 0) ) RES_DOWN_CNT," + "\n");
            sqlString.Append("                                         SUM(DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(NVL(SUBSTR(RES_STS_1, 1,1), '-'), 'D', 1, 0), 0) ) DEV_CHANG_CNT" + "\n");
            sqlString.Append("                              FROM  MRASRESDEF RAS," + "\n");
            sqlString.Append("                                        ( SELECT *" + "\n");
            sqlString.Append("                                             FROM MWIPLOTSTS LOT" + "\n");
            sqlString.Append("                                           WHERE LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                                                AND LOT.LOT_TYPE = 'W' AND LOT_DEL_FLAG = ' '" + "\n");
            sqlString.Append("                                                AND LOT.LOT_CMF_5 LIKE 'P%' " + "\n");
            sqlString.Append("                                                AND (LOT.OPER LIKE 'A040%' OR LOT.OPER LIKE 'A060%' )" + "\n");
            sqlString.Append("                                                AND LOT.LOT_STATUS = 'PROC' ) LOT, MWIPMATDEF MAT" + "\n");
            sqlString.Append("                         WHERE RAS.FACTORY   = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            sqlString.Append("                             AND RAS.RES_CMF_9 = 'Y'" + "\n");
            sqlString.Append("                             AND RAS.DELETE_FLAG  = ' '" + "\n");
            sqlString.Append("                             AND RAS.RES_TYPE  = 'EQUIPMENT'" + "\n");
            sqlString.Append("                             AND (RAS.RES_STS_8 LIKE 'A040%' OR RAS.RES_STS_8 LIKE 'A060%')" + "\n");
            sqlString.Append("                             AND RAS.FACTORY = LOT.FACTORY(+)" + "\n");
            sqlString.Append("                             AND RAS.RES_ID = LOT.START_RES_ID(+)" + "\n");
            sqlString.Append("                             AND RAS.FACTORY = MAT.FACTORY(+)" + "\n");
            sqlString.Append("                             AND RAS.RES_STS_2 = MAT.MAT_ID(+)" + "\n");
            sqlString.Append("                             AND MAT.MAT_TYPE = 'FG'" + "\n");
            sqlString.Append("                             AND MAT.DELETE_FLAG = ' '" + "\n");
            sqlString.Append("                             AND MAT.MAT_VER = 1" + "\n");
            sqlString.Append("                         GROUP BY RAS.FACTORY,RAS. RES_STS_2,RAS.RES_GRP_6,RAS.RES_STS_8,MAT.MAT_GRP_1, MAT.MAT_GRP_9, MAT.MAT_GRP_10  ) RAS, CRASUPHDEF UPEH , ( SELECT SEQ FROM HRTDSUMSEQ@RPTTOMES WHERE SEQ < 3 ) TTLSEQ" + "\n");
            sqlString.Append("          WHERE RAS.FACTORY = UPEH.FACTORY(+)" + "\n");
            sqlString.Append("               AND RAS.RES_MODEL = UPEH.RES_MODEL(+)" + "\n");
            sqlString.Append("               AND RAS.MAT_ID = UPEH.MAT_ID(+)" + "\n");
            sqlString.Append("               AND RAS.OPER = UPEH.OPER(+)" + "\n");

            if (strCustomer.Equals("-"))
            {
                sqlString.Append("              AND RAS.MAT_GRP_1 =   ( SELECT KEY_1" + "\n");
                sqlString.Append("                                                         FROM MGCMTBLDAT@RPTTOMES" + "\n");
                sqlString.Append("                                                       WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                sqlString.Append("                                                           AND TABLE_NAME = 'H_CUSTOMER'" + "\n");
                sqlString.Append("                                                           AND DATA_1 = '" + rowCustomStr + "' )" + "\n");
            }
            else sqlString.Append("              AND RAS.MAT_GRP_1 = '" + strCustomer + "'" + "\n");

            sqlString.Append("              AND RAS.MAT_GRP_9 = '" + rowMajorStr + "'" + "\n");
            sqlString.Append("              AND RAS.MAT_GRP_10 = '" + rowPKGStr + "'" + "\n");
            
            if (colStepStr.Equals("W/B"))
                 sqlString.Append("              AND RAS.OPER LIKE 'A060%'" + "\n");
             else sqlString.Append("              AND RAS.OPER LIKE 'A040%'" + "\n");

            sqlString.Append("    GROUP BY DECODE(TTLSEQ.SEQ, 2, 'Z TOTAL', RAS.RES_MODEL)" + "\n");
            sqlString.Append("    ORDER BY RES_MODEL" + "\n");
            sqlString.Append("   )" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(sqlString.ToString());
            }

            return sqlString.ToString();

        }

        private void lblEqp_Click(object sender, EventArgs e)
        {   
            if (pnlEqp.Visible == true) pnlEqp.Visible = false;
        }

        private void lblWip_Click(object sender, EventArgs e)
        {
            if (pnlWip.Visible == true) pnlWip.Visible = false;
        }

      
        
    }
}
