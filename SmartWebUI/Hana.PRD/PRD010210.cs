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
    public partial class PRD010210 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        string[] dayArry = new string[8];
        string[] dayArry1 = new string[8];

        /// <summary>
        /// 클  래  스: PRD010210<br/>
        /// 클래스요약: D/A 공정 생산일보<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2011-03-31<br/>
        /// 상세  설명: D/A 공정 생산일보 조회<br/>
        /// 변경  내용: <br/>
        /// 2011-11-21-임종우 : 월 계획 제품 중복 오류 수정
        /// 2011-12-26-임종우 : MWIPCALDEF 의 작년,올해 마지막 주차 겹치는 에러 발생으로 SYS_YEAR -> PLAN_YEAR 으로 변경
        /// 2012-02-01-김민우 : MRASRESSTS의 공정정보 RES_STS_9에서 RES_STS_8로 변경

        /// </summary>
        public PRD010210()
        {
            InitializeComponent();
            cdvDate.Value = DateTime.Now;
            SortInit();
            GridColumnInit();            
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;            
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            //if (cdvFactory.Text.TrimEnd() == "")
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
            //    return false;
            //}

            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {           
            spdData.RPT_ColumnInit();

            LabelTextChange();

            try
            {                
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Major Code", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Family", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);                
                spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD Count", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Type1", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Type2", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);                
                spdData.RPT_AddBasicColumn("Pin Type", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Density", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Generation", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);                    
                spdData.RPT_AddBasicColumn("Product", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Cust Device", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);                
                spdData.RPT_AddBasicColumn("Monthly plan", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                                        
                spdData.RPT_AddBasicColumn("Cumulative Performance", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Difference", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Achievement rate", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("Status of work", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("STOCK", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("1층", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A 1", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A 2", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A 3", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("sum", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 16, 6);
                                                 
                spdData.RPT_AddBasicColumn("Today's performance", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A number", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("CAPA", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("Chip Supply Status", 0, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(dayArry[0], 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[1], 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[2], 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[3], 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[4], 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[5], 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[6], 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Monthly average", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 25, 8);

                spdData.RPT_AddBasicColumn("D/A Production Performance", 0, 33, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(dayArry[0], 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[1], 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[2], 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[3], 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[4], 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[5], 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn(dayArry[6], 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Monthly average", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 33, 8);
                
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
                spdData.RPT_MerageHeaderRowSpan(0, 22, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 23, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 24, 2);
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT.MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "MAT.MAT_GRP_9", "MAT.MAT_GRP_9 AS MAJOR_CODE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2 AS FAMILY", false);            
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3 AS PACKAGE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6 AS \"LD COUNT\"", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4 AS TYPE1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5 AS TYPE2", false);            
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10 AS PIN_TYPE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7 AS DENSITY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8 AS GENERATION", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT.MAT_ID", "MAT.MAT_ID AS PRODUCT", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST DEVICE", "MAT.MAT_CMF_7", "MAT.MAT_CMF_7 AS CUST_DEVICE", false);
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
            string start_day;
            string start_day2;
            string end_day;
            string today;
            string year;
            string month;            

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;            
            
            string getStartDate = cdvDate.Value.ToString("yyyy-MM") + "-01";
            
            start_day = cdvDate.Value.ToString("yyyyMM") + "01";
            end_day = Convert.ToDateTime(getStartDate).AddMonths(1).AddDays(-1).ToString("yyyyMMdd");
            today = cdvDate.Value.ToString("yyyyMMdd");
            year = cdvDate.Value.ToString("yyyy");
            month = cdvDate.Value.ToString("yyyyMM");

            // 7일 이후에만 기준월의 시작일을 가지고 사용함.
            if (today.Substring(6, 2) == "01" || today.Substring(6, 2) == "02" || today.Substring(6, 2) == "03" || today.Substring(6, 2) == "04" || today.Substring(6, 2) == "05" || today.Substring(6, 2) == "06")
            {
                start_day2 = dayArry1[0];
            }
            else
            {
                start_day2 = start_day;
            }

            // 지난주차의 마지막일 가져오기
            DataTable dt = null;
            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1(year, today));
            string Lastweek_lastday = dt.Rows[0][0].ToString();

            if (ckbKpcs.Checked == true)
            {
                strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond2);
                strSqlString.Append("     , ROUND(SUM(NVL(PLN.PLAN_QTY_ASSY,0)) / 1000,0) AS MON_PLAN" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.MON_END,0)) / 1000,0) AS MON_END" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.MON_END,0) - NVL(PLN.PLAN_QTY_ASSY,0)) / 1000,0) AS MON_DEF" + "\n");
                strSqlString.Append("     , ROUND(DECODE(SUM(NVL(PLN.PLAN_QTY_ASSY,0)), 0, 0, SUM(NVL(SHP.MON_END,0))/SUM(NVL(PLN.PLAN_QTY_ASSY,0))*100),0) AS MON_PER" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.WIP_STOCK,0)) / 1000,0) AS WIP_STOCK" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.WIP_1F,0)) / 1000,0) AS WIP_1F" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.WIP_DA1,0)) / 1000,0) AS WIP_DA1" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.WIP_DA2,0)) / 1000,0) AS WIP_DA2" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.WIP_DA3,0)) / 1000,0)AS WIP_DA3" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.WIP_STOCK,0)+NVL(WIP.WIP_1F,0)+NVL(WIP.WIP_DA1,0)+NVL(WIP.WIP_DA2,0)+NVL(WIP.WIP_DA3,0)) / 1000,0) AS WIP_TOTAL" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.DAY_END7,0)) / 1000,0) AS DAY_END7" + "\n");
                strSqlString.Append("     , SUM(NVL(ARR.ARR_CNT,0)) AS ARR_CNT" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(ARR.CAPA,0)) / 1000,0) AS CAPA" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.W0,0)) / 1000,0) AS W0" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.W1,0)) / 1000,0) AS W1" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.W2,0)) / 1000,0) AS W2" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.W3,0)) / 1000,0) AS W3" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.W4,0)) / 1000,0) AS W4" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.W5,0)) / 1000,0) AS W5" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.W6,0)) / 1000,0)AS W6" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.W_AVG,0)) / 1000,0) AS W_AVG" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.DAY_END0,0)) / 1000,0) AS DAY_END0" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.DAY_END1,0)) / 1000,0) AS DAY_END1" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.DAY_END2,0)) / 1000,0) AS DAY_END2" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.DAY_END3,0)) / 1000,0) AS DAY_END3" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.DAY_END4,0)) / 1000,0) AS DAY_END4" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.DAY_END5,0)) / 1000,0) AS DAY_END5" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.DAY_END6,0)) / 1000,0) AS DAY_END6" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.MON_END_AVG,0)) / 1000,0) AS MON_END_AVG" + "\n");
            }
            else
            {
                strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond2);
                strSqlString.Append("     , SUM(NVL(PLN.PLAN_QTY_ASSY,0)) AS MON_PLAN" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.MON_END,0)) AS MON_END" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.MON_END,0)) - SUM(NVL(PLN.PLAN_QTY_ASSY,0)) AS MON_DEF" + "\n");
                strSqlString.Append("     , ROUND(DECODE(SUM(NVL(PLN.PLAN_QTY_ASSY,0)), 0, 0, SUM(NVL(SHP.MON_END,0))/SUM(NVL(PLN.PLAN_QTY_ASSY,0))*100),0) AS MON_PER" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.WIP_STOCK,0)) AS WIP_STOCK" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.WIP_1F,0)) AS WIP_1F" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.WIP_DA1,0)) AS WIP_DA1" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.WIP_DA2,0)) AS WIP_DA2" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.WIP_DA3,0)) AS WIP_DA3" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.WIP_STOCK,0)+NVL(WIP.WIP_1F,0)+NVL(WIP.WIP_DA1,0)+NVL(WIP.WIP_DA2,0)+NVL(WIP.WIP_DA3,0)) AS WIP_TOTAL" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.DAY_END7,0)) AS DAY_END7" + "\n");
                strSqlString.Append("     , SUM(NVL(ARR.ARR_CNT,0)) AS ARR_CNT" + "\n");
                strSqlString.Append("     , SUM(NVL(ARR.CAPA,0)) AS CAPA" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.W0,0)) AS W0" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.W1,0)) AS W1" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.W2,0)) AS W2" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.W3,0)) AS W3" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.W4,0)) AS W4" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.W5,0)) AS W5" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.W6,0)) AS W6" + "\n");
                strSqlString.Append("     , SUM(NVL(WIP.W_AVG,0)) AS W_AVG" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.DAY_END0,0)) AS DAY_END0" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.DAY_END1,0)) AS DAY_END1" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.DAY_END2,0)) AS DAY_END2" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.DAY_END3,0)) AS DAY_END3" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.DAY_END4,0)) AS DAY_END4" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.DAY_END5,0)) AS DAY_END5" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.DAY_END6,0)) AS DAY_END6" + "\n");
                strSqlString.Append("     , SUM(NVL(SHP.MON_END_AVG,0)) AS MON_END_AVG" + "\n");
            }

            strSqlString.Append("  FROM MWIPMATDEF MAT " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT FACTORY,MAT_ID,PLAN_QTY_ASSY,PLAN_MONTH " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, PLAN_MONTH " + "\n");
            strSqlString.Append("                  FROM CWIPPLNMON " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND MAT_ID NOT IN ( " + "\n");
            strSqlString.Append("                                      SELECT MAT_ID " + "\n");
            strSqlString.Append("                                        FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                                       WHERE 1=1 " + "\n");
            strSqlString.Append("                                         AND MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                                         AND MAT_GRP_9 = 'S-LSI' " + "\n");
            strSqlString.Append("                                     ) " + "\n");
            strSqlString.Append("                 GROUP BY FACTORY, MAT_ID, PLAN_MONTH " + "\n");
            strSqlString.Append("                 UNION ALL " + "\n");
            strSqlString.Append("                SELECT A.FACTORY, A.MAT_ID, SUM(A.PLAN_QTY) AS PLAN_QTY_ASSY , '" + month + "' AS PLAN_MONTH " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
            strSqlString.Append("                          FROM CWIPPLNDAY " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND PLAN_DAY BETWEEN '" + start_day + "' AND '" + end_day + "'\n");
            strSqlString.Append("                           AND IN_OUT_FLAG = 'OUT' " + "\n");
            strSqlString.Append("                           AND CLASS = 'ASSY' " + "\n");
            strSqlString.Append("                         GROUP BY FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                         UNION ALL " + "\n");
            strSqlString.Append("                        SELECT CM_KEY_1 AS FACTORY, MAT_ID " + "\n");
            strSqlString.Append("                             , SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS PLAN_QTY " + "\n");
            strSqlString.Append("                          FROM RSUMFACMOV " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND CM_KEY_2 = 'PROD' " + "\n");
            strSqlString.Append("                           AND CM_KEY_3 LIKE 'P%' " + "\n");
            strSqlString.Append("                           AND WORK_DATE BETWEEN '" + start_day + "' AND '" + Lastweek_lastday + "'\n");
            strSqlString.Append("                         GROUP BY CM_KEY_1, MAT_ID " + "\n");
            strSqlString.Append("                       ) A" + "\n");
            strSqlString.Append("                       , MWIPMATDEF B " + "\n");
            strSqlString.Append("                   WHERE 1=1  " + "\n");
            strSqlString.Append("                     AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                     AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("                     AND B.MAT_GRP_1 = 'SE' " + "\n");
            strSqlString.Append("                     AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
            strSqlString.Append("                   GROUP BY A.FACTORY, A.MAT_ID, B.MAT_GRP_3,B.MAT_CMF_13 " + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("       ) PLN " + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArry1[0] + "', QTY, 0)) AS DAY_END0" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArry1[1] + "', QTY, 0)) AS DAY_END1" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArry1[2] + "', QTY, 0)) AS DAY_END2" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArry1[3] + "', QTY, 0)) AS DAY_END3" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArry1[4] + "', QTY, 0)) AS DAY_END4" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArry1[5] + "', QTY, 0)) AS DAY_END5" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArry1[6] + "', QTY, 0)) AS DAY_END6" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + dayArry1[7] + "', QTY, 0)) AS DAY_END7" + "\n");
            strSqlString.Append("             , SUM(DECODE(SUBSTR(WORK_DATE,1, 6), '" + month + "', QTY, 0)) AS MON_END" + "\n");
            strSqlString.Append("             , ROUND(SUM(DECODE(SUBSTR(WORK_DATE,1, 6), '" + month + "', QTY, 0))/" + Convert.ToInt32(today.Substring(6, 2)) + ", 0) AS MON_END_AVG" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT MOV.MAT_ID, WORK_DATE" + "\n");
            strSqlString.Append("                     , SUM(S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ) AS QTY" + "\n");
            strSqlString.Append("                  FROM RSUMWIPMOV MOV" + "\n");            
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT * " + "\n");
            strSqlString.Append("                          FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                           AND ((MAT_GRP_3 IN ('MCP', 'DDP', 'QDP') AND MAT_GRP_5 IN ('Merge')) OR MAT_GRP_3 NOT IN ('MCP', 'DDP', 'QDP'))" + "\n");
            strSqlString.Append("                       ) MAT" + "\n");
            strSqlString.Append("                 WHERE MOV.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                   AND WORK_DATE BETWEEN '" + start_day2 + "' AND '" + dayArry1[7] + "'" + "\n");                        
            strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                   AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND OPER LIKE 'A040%'" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                 GROUP BY MOV.MAT_ID, WORK_DATE" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) SHP" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT A.MAT_ID, B.W0, B.W1, B.W2, B.W3, B.W4, B.W5, B.W6, B.W_AVG" + "\n");
            strSqlString.Append("             , C.WIP_STOCK, C.WIP_1F, C.WIP_DA1, C.WIP_DA2, C.WIP_DA3" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT * " + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                   AND ((MAT_GRP_3 IN ('MCP', 'DDP', 'QDP') AND MAT_GRP_5 IN ('1st','Merge')) OR MAT_GRP_3 NOT IN ('MCP', 'DDP', 'QDP'))" + "\n");
            strSqlString.Append("               ) A" + "\n");
            strSqlString.Append("             , (             " + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");
            strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + dayArry1[0] + "', QTY_1, 0)) W0" + "\n");
            strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + dayArry1[1] + "', QTY_1, 0)) W1" + "\n");
            strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + dayArry1[2] + "', QTY_1, 0)) W2" + "\n");
            strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + dayArry1[3] + "', QTY_1, 0)) W3" + "\n");
            strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + dayArry1[4] + "', QTY_1, 0)) W4" + "\n");
            strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + dayArry1[5] + "', QTY_1, 0)) W5" + "\n");
            strSqlString.Append("                     , SUM(DECODE(WORK_DATE, '" + dayArry1[6] + "', QTY_1, 0)) W6" + "\n");
            strSqlString.Append("                     , ROUND(SUM(DECODE(SUBSTR(WORK_DATE,1, 6), '" + month + "', QTY_1, 0))/" + Convert.ToInt32(today.Substring(6, 2)) + ",0) AS W_AVG" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT FACTORY, MAT_ID, WORK_DATE, SUM(QTY_1) AS QTY_1" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT FACTORY, MAT_ID, WORK_DATE, (S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1) AS QTY_1" + "\n");
            strSqlString.Append("                                  FROM RSUMFACMOV " + "\n");
            strSqlString.Append("                                 WHERE WORK_DATE BETWEEN '" + start_day2 + "' AND '" + dayArry1[7] + "'" + "\n");
            strSqlString.Append("                                   AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                   AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                                   AND OPER = 'A0000' " + "\n");
            strSqlString.Append("                                   AND MAT_ID NOT LIKE 'HX%'" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                                 UNION ALL " + "\n");
            strSqlString.Append("                                SELECT FACTORY, MAT_ID, WORK_DATE AS CREATE_DAY, (S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1) AS QTY_1" + "\n");
            strSqlString.Append("                                  FROM CSUMFACMOV " + "\n");
            strSqlString.Append("                                 WHERE WORK_DATE BETWEEN '" + start_day2 + "' AND '" + dayArry1[7] + "'" + "\n");          
            strSqlString.Append("                                   AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                                   AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                                   AND OPER = 'A0000' " + "\n");
            strSqlString.Append("                                   AND MAT_ID LIKE 'HX%'" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                                   AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                               )" + "\n");
            strSqlString.Append("                         GROUP BY FACTORY, MAT_ID, WORK_DATE" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
            strSqlString.Append("               ) B" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0000', QTY_1, 0)) AS WIP_STOCK" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER BETWEEN 'A0020' AND 'A0300' THEN QTY_1" + "\n");
            strSqlString.Append("                                ELSE 0" + "\n");
            strSqlString.Append("                           END) AS WIP_1F" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER BETWEEN 'A0340' AND 'A0401' THEN QTY_1" + "\n");
            strSqlString.Append("                                ELSE 0" + "\n");
            strSqlString.Append("                           END) AS WIP_DA1" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0402','A0501') THEN QTY_1" + "\n");
            strSqlString.Append("                                ELSE 0" + "\n");
            strSqlString.Append("                           END) AS WIP_DA2" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN OPER IN ('A0403','A0502') THEN QTY_1" + "\n");
            strSqlString.Append("                                ELSE 0" + "\n");
            strSqlString.Append("                           END) AS WIP_DA3" + "\n");
            strSqlString.Append("                  FROM RWIPLOTSTS" + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                   AND LOT_DEL_FLAG = ' '" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                 GROUP BY MAT_ID" + "\n");
            strSqlString.Append("               ) C" + "\n");
            strSqlString.Append("         WHERE A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND A.MAT_ID = C.MAT_ID(+) " + "\n");
            strSqlString.Append("       ) WIP" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT RES.RES_STS_2 AS MAT_ID" + "\n");
            strSqlString.Append("             , SUM(RES.RES_CNT) AS ARR_CNT" + "\n");
            strSqlString.Append("             , SUM(TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.85 * RES.RES_CNT, 0))) AS CAPA" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT DISTINCT FACTORY, RES_STS_2, RES_STS_8 AS OPER, RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, COUNT(RES_ID) AS RES_CNT " + "\n");
            strSqlString.Append("                  FROM MRASRESDEF " + "\n");
            strSqlString.Append("                 WHERE 1 = 1  " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("                   AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND RES_STS_8 IN ('A0400', 'A0401', 'A0402', 'A0403', 'A0404', 'A0405', 'A0406', 'A0407', 'A0408', 'A0409')" + "\n");
            strSqlString.Append("                 GROUP BY FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7 " + "\n");
            strSqlString.Append("               ) RES" + "\n");
            strSqlString.Append("             , CRASUPHDEF UPH" + "\n");
            strSqlString.Append("         WHERE 1=1  " + "\n");
            strSqlString.Append("           AND RES.FACTORY=UPH.FACTORY(+)" + "\n");
            strSqlString.Append("           AND RES.OPER=UPH.OPER(+)" + "\n");
            strSqlString.Append("           AND RES.RES_MODEL = UPH.RES_MODEL(+) " + "\n");
            strSqlString.Append("           AND RES.UPEH_GRP = UPH.UPEH_GRP(+) " + "\n");
            strSqlString.Append("           AND RES.RES_STS_2 = UPH.MAT_ID(+)" + "\n");
            strSqlString.Append("         GROUP BY RES.OPER, RES.RES_STS_2 " + "\n");
            strSqlString.Append("       ) ARR" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND MAT.FACTORY = PLN.FACTORY(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = ARR.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("   AND PLN.PLAN_MONTH(+) = '" + month + "' " + "\n");
            strSqlString.Append("   AND MAT.MAT_TYPE= 'FG' " + "\n");
            strSqlString.Append("   AND MAT.DELETE_FLAG <> 'Y' " + "\n");

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.AppendFormat(" GROUP BY {0}" + "\n", QueryCond1);
            strSqlString.Append(" HAVING (" + "\n");
            strSqlString.Append("         SUM(NVL(PLN.PLAN_QTY_ASSY,0)) + SUM(NVL(SHP.MON_END,0)) + SUM(NVL(WIP.WIP_STOCK,0)) + SUM(NVL(WIP.WIP_1F,0)) +" + "\n");
            strSqlString.Append("         SUM(NVL(WIP.WIP_DA1,0)) + SUM(NVL(WIP.WIP_DA2,0)) + SUM(NVL(WIP.WIP_DA3,0)) + SUM(NVL(SHP.DAY_END7,0)) +" + "\n");
            strSqlString.Append("         SUM(NVL(ARR.ARR_CNT,0)) + SUM(NVL(WIP.W0,0)) + SUM(NVL(WIP.W1,0)) + SUM(NVL(WIP.W2,0)) + SUM(NVL(WIP.W3,0)) +" + "\n");
            strSqlString.Append("         SUM(NVL(WIP.W4,0)) + SUM(NVL(WIP.W5,0)) + SUM(NVL(WIP.W6,0)) + SUM(NVL(WIP.W_AVG,0)) + SUM(NVL(SHP.DAY_END0,0)) +" + "\n");
            strSqlString.Append("         SUM(NVL(SHP.DAY_END1,0)) + SUM(NVL(SHP.DAY_END2,0)) + SUM(NVL(SHP.DAY_END3,0)) + SUM(NVL(SHP.DAY_END4,0)) +" + "\n");
            strSqlString.Append("         SUM(NVL(SHP.DAY_END5,0)) + SUM(NVL(SHP.DAY_END6,0)) + SUM(NVL(SHP.MON_END_AVG,0))" + "\n");
            strSqlString.Append("         ) <> 0" + "\n");
            strSqlString.AppendFormat(" ORDER BY {0}", QueryCond1 + "\n");
                    
            return strSqlString.ToString();
        }

        // 지난 주차의 마지막일 가져오기
        private string MakeSqlString1(string year, string date)
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

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 12, null, null, btnSort);                
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함
                //spdData.Sheets[0].FrozenColumnCount = 3;
                //spdData.RPT_AutoFit(false);

                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 12, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);

                spdData.RPT_SetPerSubTotalAndGrandTotal(0, 13, 12, 15);                

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
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.ActiveSheet.Rows.Count > 0)
            {
                string sNowDate = null;

                sNowDate = DateTime.Now.ToString();
                StringBuilder Condition = new StringBuilder();
                
                Condition.AppendFormat("기준일자: {0}        진도율: {1}        잔여일수: {2}        LOT TYPE : {3}        조회시간 : {4}", lblToday.Text, lblJindo.Text, lblRemain.Text, cdvLotType.Text, sNowDate);
                
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
            //this.SetFactory(cdvFactory.txtValue);
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }

        /// <summary>
        /// 7. 상단 Lebel 표시
        /// </summary>
        private void LabelTextChange()
        {               
            string strToday = cdvDate.Value.ToString("yyyyMMdd");

            string getStartDate = cdvDate.Value.ToString("yyyy-MM") + "-01";   
            string strEndday = Convert.ToDateTime(getStartDate).AddMonths(1).AddDays(-1).ToString("yyyyMMdd");

            double jindo = Convert.ToDouble(strToday.Substring(6, 2)) / Convert.ToDouble(strEndday.Substring(6, 2)) * 100;

            int remain = Convert.ToInt32(strEndday.Substring(6, 2)) - Convert.ToInt32(strToday.Substring(6, 2));

            // 해당월의 마지막날은 잔여일을 1로 계산한다.
            if (remain == 0)
            {
                remain = 1;
            }

            // 진도율 소수점 1째자리 까지 표시
            decimal jindoPer = Math.Round(Convert.ToDecimal(jindo),1);

            lblToday.Text = cdvDate.Value.ToString("yyyy-MM-dd");
            lblJindo.Text = jindoPer.ToString() + "%";
            lblRemain.Text = remain.ToString();

            dayArry[0] = cdvDate.Value.AddDays(-7).ToString("MM.dd");
            dayArry[1] = cdvDate.Value.AddDays(-6).ToString("MM.dd");
            dayArry[2] = cdvDate.Value.AddDays(-5).ToString("MM.dd");
            dayArry[3] = cdvDate.Value.AddDays(-4).ToString("MM.dd");
            dayArry[4] = cdvDate.Value.AddDays(-3).ToString("MM.dd");
            dayArry[5] = cdvDate.Value.AddDays(-2).ToString("MM.dd");
            dayArry[6] = cdvDate.Value.AddDays(-1).ToString("MM.dd");
            dayArry[7] = cdvDate.Value.ToString("MM.dd");


            dayArry1[0] = cdvDate.Value.AddDays(-7).ToString("yyyyMMdd");
            dayArry1[1] = cdvDate.Value.AddDays(-6).ToString("yyyyMMdd");
            dayArry1[2] = cdvDate.Value.AddDays(-5).ToString("yyyyMMdd");
            dayArry1[3] = cdvDate.Value.AddDays(-4).ToString("yyyyMMdd");
            dayArry1[4] = cdvDate.Value.AddDays(-3).ToString("yyyyMMdd");
            dayArry1[5] = cdvDate.Value.AddDays(-2).ToString("yyyyMMdd");
            dayArry1[6] = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");
            dayArry1[7] = cdvDate.Value.ToString("yyyyMMdd");            
        }
        #endregion
    }       
}
