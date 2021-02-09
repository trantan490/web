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
    public partial class PRD010224 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        string[] dayArry = new string[3];
        string[] dayArry2 = new string[3];
        DataTable dtWeek = null;
        GlobalVariable.FindWeek FindWeek = new GlobalVariable.FindWeek();

        /// <summary>
        /// 클  래  스: PRD010224<br/>
        /// 클래스요약: 삼성 메모리 재공일수 관리<br/>
        /// 작  성  자: 임종우<br/>
        /// 최초작성일: 2014-12-17<br/>
        /// 상세  설명: 삼성 메모리 재공일수 관리(임태성K 요청)<br/>
        /// 변경  내용: <br/>  
        /// 2014-12-23-임종우 : 주계획 영업계획으로 변경, Kpcs 기능 추가 (임태성K 요청)
        /// 2016-12-13-임종우 : 10단 칩 이상 스택 수 오류 수정
        /// 2020-04-28-김미경 : 제품 TYPE(STACK) 추가 반영 (이승희 D)
        /// </summary>
        public PRD010224()
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
                        
            FindWeek = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");
            
            try
            {                 
                spdData.RPT_AddBasicColumn("PKG", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("CODE", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("차수", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("CHIP", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("STACK", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("weight", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("WIP", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("FRONT", 1, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("FRONT conversion", 1, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("B/E", 1, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TOTAL", 1, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 6, 4);                

                spdData.RPT_AddBasicColumn("PLAN", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("2주", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("4주", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 10, 2);

                spdData.RPT_AddBasicColumn("WIP days", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("2주", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("4주", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 12, 2);
                
                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 5);                

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
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "DECODE(A.CUSTOMER,'SEC','SEC','HYNIX','HYNIX','iML','iML','FCI','FCI','IMAGIS','IMAGIS' , (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.CUSTOMER)) AS CUSTOMER", "(CASE WHEN A.MAT_GRP_1 = 'SE' THEN 'SEC' WHEN A.MAT_GRP_1 = 'HX' THEN 'HYNIX'  WHEN A.MAT_GRP_1 = 'IM' THEN 'iML'  WHEN A.MAT_GRP_1 = 'FC' THEN 'FCI' WHEN A.MAT_GRP_1 = 'IG' THEN 'IMAGIS' ELSE A.MAT_GRP_1 END)", "(CASE WHEN A.MAT_GRP_1 = 'SE' THEN 'SEC' WHEN A.MAT_GRP_1 = 'HX' THEN 'HYNIX' WHEN A.MAT_GRP_1 = 'IM' THEN 'iML' WHEN A.MAT_GRP_1 = 'FC' THEN 'FCI' WHEN A.MAT_GRP_1 = 'IG' THEN 'IMAGIS' ELSE A.MAT_GRP_1 END) CUSTOMER", "(CASE WHEN MAT_GRP_1 = 'SE' THEN 'SEC' WHEN MAT_GRP_1 = 'HX' THEN 'HYNIX'  WHEN MAT_GRP_1 = 'IM' THEN 'iML'  WHEN MAT_GRP_1 = 'FC' THEN 'FCI' WHEN MAT_GRP_1 = 'IG' THEN 'IMAGIS' ELSE MAT_GRP_1 END)", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "A.MAJOR", "A.MAT_GRP_9", "A.MAT_GRP_9 AS MAJOR","MAT_GRP_9", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "A.PKG", "A.MAT_GRP_10", "A.MAT_GRP_10 AS PKG","MAT_GRP_10", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "A.TYPE1", "A.MAT_GRP_4","A.MAT_GRP_4 AS TYPE1","MAT_GRP_4", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "A.TYPE2", "A.MAT_GRP_5", "A.MAT_GRP_5 AS TYPE2", "MAT_GRP_5", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "A.LD_COUNT", "A.MAT_GRP_6", "A.MAT_GRP_6 AS LD_COUNT","MAT_GRP_6", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "A.PKG_CODE", "A.MAT_CMF_11", "A.MAT_CMF_11 AS PKG_CODE","MAT_CMF_11", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "A.DENSITY", "A.MAT_GRP_7", "A.MAT_GRP_7 AS DENSITY", "MAT_GRP_7", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "A.GENERATION", "A.MAT_GRP_8", "A.MAT_GRP_8 AS GENERATION", "MAT_GRP_8", false); 
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "A.PIN_TYPE", "A.MAT_CMF_10", "A.MAT_CMF_10 AS PIN_TYPE","MAT_CMF_10", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.MAT_ID", "A.MAT_ID", "A.MAT_ID", "MAT_ID", false);            
 
        }
        #endregion

        #region 시간관련 함수
        private void GetWorkDay()
        {
            FindWeek = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");
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
            string start_date;
            string yesterday;
            string date;
            string month;
            string year;
            string lastMonth;
            string sKpcsValue;            

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            
            GetWeek();
            date = cdvDate.SelectedValue();

            DateTime Select_date;
            Select_date = DateTime.Parse(cdvDate.Text.ToString());

            year = Select_date.ToString("yyyy");
            month = Select_date.ToString("yyyyMM");
            start_date = month + "01";
            yesterday = Select_date.AddDays(-1).ToString("yyyyMMdd");
            lastMonth = Select_date.AddMonths(-1).ToString("yyyyMM"); // 지난달

            if (ckbKpcs.Checked == true)
            {
                sKpcsValue = "1000";
            }
            else
            {
                sKpcsValue = "1";
            }

            strSqlString.Append("SELECT MAT_GRP_10, MAT_CMF_11, MAT_GRP_5, CHIP, STACK" + "\n");
            strSqlString.Append("     , KAJUNGCHI" + "\n");
            strSqlString.Append("     , ROUND((WIP_2GROUP + WIP_SAW + WIP_BG) / " + sKpcsValue + ", 0) AS FRONT" + "\n");
            strSqlString.Append("     , ROUND((WIP_2GROUP + WIP_SAW + WIP_BG) / STACK / " + sKpcsValue + ", 0) AS CONV_FRONT" + "\n");
            strSqlString.Append("     , ROUND(WIP_BE / " + sKpcsValue + ", 0) AS WIP_BE" + "\n");
            strSqlString.Append("     , ROUND((((WIP_2GROUP + WIP_SAW + WIP_BG) / STACK) + WIP_BE) / " + sKpcsValue + ", 0) AS WIP_TTL" + "\n");
            strSqlString.Append("     , ROUND(PLN_WEEK2 / " + sKpcsValue + ", 0) AS PLN_WEEK2" + "\n");
            strSqlString.Append("     , ROUND(PLN_WEEK4 / " + sKpcsValue + ", 0) AS PLN_WEEK4" + "\n");
            strSqlString.Append("     , 0, 0" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT A.MAT_GRP_1" + "\n");
            strSqlString.Append("             , A.MAT_GRP_10" + "\n");
            strSqlString.Append("             , A.MAT_CMF_11" + "\n");
            strSqlString.Append("             , A.MAT_GRP_5     " + "\n");
            strSqlString.Append("             , A.CHIP" + "\n");
            strSqlString.Append("             , A.STACK" + "\n");
            strSqlString.Append("             , ROUND(100 / A.STACK,0) AS KAJUNGCHI" + "\n");
            strSqlString.Append("             , CASE WHEN A.MAT_GRP_5 IN ('-', '1st') THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER,0)" + "\n");
            strSqlString.Append("                    WHEN A.MAT_GRP_5 = '2nd' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER,0)" + "\n");
            strSqlString.Append("                    WHEN A.MAT_GRP_5 = '3rd' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER_3,0)" + "\n");
            strSqlString.Append("                    WHEN A.MAT_GRP_5 = '4th' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER_4,0)" + "\n");
            strSqlString.Append("                    WHEN A.MAT_GRP_5 = '5th' THEN NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER_5,0)" + "\n");
            strSqlString.Append("                    ELSE 0" + "\n");
            strSqlString.Append("               END WIP_2GROUP" + "\n");
            strSqlString.Append("             , NVL(A.WIP_SAW,0) AS WIP_SAW" + "\n");
            strSqlString.Append("             , NVL(A.WIP_BG,0) AS WIP_BG" + "\n");
            strSqlString.Append("             , CASE WHEN A.MAT_GRP_5 IN ('-', '1st') THEN NVL(B.WIP_BE,0) ELSE 0 END WIP_BE " + "\n");
            strSqlString.Append("             , CASE WHEN A.MAT_GRP_5 IN ('-', '1st') THEN NVL(C.PLN_WEEK2,0) ELSE 0 END PLN_WEEK2 " + "\n");
            strSqlString.Append("             , CASE WHEN A.MAT_GRP_5 IN ('-', '1st') THEN NVL(C.PLN_WEEK4,0) ELSE 0 END PLN_WEEK4 " + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT MAT_GRP_1, MAT_GRP_10, MAT_CMF_11, MAT_GRP_5" + "\n");
            strSqlString.Append("                     , CASE WHEN MAT_GRP_4 IN ('-', 'FD', 'FU') THEN '1'" + "\n");
            strSqlString.Append("                            WHEN MAT_GRP_4 IN ('DDP') THEN '2'" + "\n");
            strSqlString.Append("                            WHEN MAT_GRP_4 IN ('QDP') THEN '4'" + "\n");
            strSqlString.Append("                            WHEN MAT_GRP_4 IN ('ODP') THEN '8'" + "\n");
            strSqlString.Append("                            ELSE REGEXP_REPLACE(MAT_GRP_4, '[^[:digit:]]')" + "\n");
            strSqlString.Append("                       END AS STACK" + "\n");
            //strSqlString.Append("                     , CASE WHEN MAT_GRP_4 IN ('-','FD','FU') THEN '1'" + "\n");
            //strSqlString.Append("                            ELSE SUBSTR(MAT_GRP_4, 3)" + "\n");
            //strSqlString.Append("                       END AS STACK" + "\n");
            strSqlString.Append("                     , MAX(CASE WHEN MAT.MAT_ID LIKE 'SEKS3%' THEN SUBSTR(MAT.MAT_ID, INSTR(MAT.MAT_ID, '-')-3, 3)" + "\n");
            strSqlString.Append("                                WHEN MAT.MAT_ID LIKE 'SEK9%' THEN 'N'" + "\n");
            strSqlString.Append("                                WHEN MAT.MAT_ID LIKE 'SEKY%' THEN 'S'" + "\n");
            strSqlString.Append("                                WHEN MAT.MAT_ID LIKE 'SEK%' THEN 'D'" + "\n");
            strSqlString.Append("                                ELSE ' '" + "\n");
            strSqlString.Append("                           END) CHIP" + "\n");
            strSqlString.Append("                     , SUM(WIP_BG / COMP_CNT) AS WIP_BG" + "\n");
            strSqlString.Append("                     , SUM(WIP_SAW / COMP_CNT) AS WIP_SAW" + "\n");
            strSqlString.Append("                     , SUM(WIP_DA_WB) AS WIP_DA_WB" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT A.*" + "\n");
            strSqlString.Append("                          FROM VWIPMATDEF A" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                           AND DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND MAT_GRP_5 <> 'Merge'" + "\n");
            strSqlString.Append("                           AND MAT_GRP_5 NOT LIKE 'Middle%'" + "\n");
            strSqlString.Append("                           AND MAT_ID LIKE 'SEK%'" + "\n");

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("                           AND MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);
            }

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                           AND MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion

            strSqlString.Append("                       ) MAT" + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID " + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER BETWEEN 'A0001' AND 'A0040' THEN QTY_1 ELSE 0 END) AS WIP_BG" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER BETWEEN 'A0041' AND 'A0300' AND OPER <> 'A0250' THEN QTY_1 ELSE 0 END) AS WIP_SAW" + "\n");
            strSqlString.Append("                             , SUM(CASE WHEN OPER BETWEEN 'A0400' AND 'A0809' OR OPER = 'A0250' THEN QTY_1 ELSE 0 END) AS WIP_DA_WB" + "\n");

            if (date == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                          FROM RWIPLOTSTS " + "\n");
                strSqlString.Append("                         WHERE 1=1  " + "\n");
            }
            else
            {
                strSqlString.Append("                          FROM RWIPLOTSTS_BOH " + "\n");
                strSqlString.Append("                         WHERE 1=1  " + "\n");
                strSqlString.Append("                           AND CUTOFF_DT = '" + date + "22' " + "\n");
            }

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                           AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }
                        
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("                       ) WIP" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND MAT.MAT_ID = WIP.MAT_ID(+)   " + "\n");
            strSqlString.Append("                 GROUP BY MAT_GRP_1, MAT_GRP_10, MAT_CMF_11, MAT_GRP_5, MAT_GRP_4" + "\n");
            strSqlString.Append("               ) A" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT A.MAT_GRP_1, A.MAT_GRP_10, A.MAT_CMF_11 " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN B.OPER BETWEEN 'A0400' AND 'A0609' AND MAT_GRP_5 NOT IN ('-') THEN QTY_1 ELSE 0 END) AS WIP_MID_MER" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN B.OPER BETWEEN 'A0400' AND 'A0609' AND MAT_GRP_5 NOT IN ('-', 'Middle') THEN QTY_1 ELSE 0 END) AS WIP_MID_MER_3" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN B.OPER BETWEEN 'A0400' AND 'A0609' AND MAT_GRP_5 NOT IN ('-', 'Middle', 'Middle 1') THEN QTY_1 ELSE 0 END) AS WIP_MID_MER_4" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN B.OPER BETWEEN 'A0400' AND 'A0609' AND MAT_GRP_5 NOT IN ('-', 'Middle', 'Middle 1', 'Middle 2') THEN QTY_1 ELSE 0 END) AS WIP_MID_MER_5" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN C.OPER_GRP_1 IN ('MOLD','CURE','M/K','TRIM','TIN','S/B/A','SIG','AVI','V/I','HMK3A') THEN QTY_1 ELSE 0 END) AS WIP_BE                          " + "\n");

            if (date == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("                  FROM MWIPMATDEF A" + "\n");
                strSqlString.Append("                     , RWIPLOTSTS B" + "\n");
                strSqlString.Append("                     , MWIPOPRDEF C" + "\n");
                strSqlString.Append("                 WHERE 1=1  " + "\n");
            }
            else
            {
                strSqlString.Append("                  FROM MWIPMATDEF A" + "\n");
                strSqlString.Append("                     , RWIPLOTSTS_BOH B" + "\n");
                strSqlString.Append("                     , MWIPOPRDEF C" + "\n");
                strSqlString.Append("                 WHERE 1=1  " + "\n");                
                strSqlString.Append("                   AND B.CUTOFF_DT = '" + date + "22' " + "\n");
            }

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND B.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                   AND A.FACTORY = C.FACTORY" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                   AND B.OPER = C.OPER" + "\n");
            strSqlString.Append("                   AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND B.OPER BETWEEN 'A0400' AND 'AZ010'" + "\n");
            strSqlString.Append("                   AND B.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                   AND B.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND B.MAT_ID LIKE 'SEK%'" + "\n");
            strSqlString.Append("                   AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND REGEXP_LIKE(A.MAT_GRP_5, 'Middle*|Merge|-')" + "\n");            
            strSqlString.Append("                 GROUP BY A.MAT_GRP_1, A.MAT_GRP_10, A.MAT_CMF_11 " + "\n");
            strSqlString.Append("               ) B" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT A.MAT_GRP_1, A.MAT_GRP_10, A.MAT_CMF_11" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN PLAN_WEEK IN ('" + dtWeek.Rows[0][0] + "', '" + dtWeek.Rows[1][0] + "') THEN WW_QTY ELSE 0 END) AS PLN_WEEK2" + "\n");
            strSqlString.Append("                     , SUM(WW_QTY) AS PLN_WEEK4" + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF A" + "\n");
            strSqlString.Append("                     , RWIPPLNWEK B" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                   AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND B.PLAN_WEEK IN ('" + dtWeek.Rows[0][0] + "','" + dtWeek.Rows[1][0] + "', '" + dtWeek.Rows[2][0] + "', '" + dtWeek.Rows[3][0] + "')" + "\n");
            strSqlString.Append("                   AND B.GUBUN = '0'" + "\n");
            strSqlString.Append("                   AND B.MAT_ID LIKE 'SEK%'" + "\n");
            strSqlString.Append("                 GROUP BY A.MAT_GRP_1, A.MAT_GRP_10, A.MAT_CMF_11" + "\n");
            strSqlString.Append("               ) C" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND A.MAT_GRP_1 = B.MAT_GRP_1(+)" + "\n");
            strSqlString.Append("           AND A.MAT_GRP_1 = C.MAT_GRP_1(+)" + "\n");
            strSqlString.Append("           AND A.MAT_GRP_10 = B.MAT_GRP_10(+)" + "\n");
            strSqlString.Append("           AND A.MAT_GRP_10 = C.MAT_GRP_10(+)" + "\n");
            strSqlString.Append("           AND A.MAT_CMF_11 = B.MAT_CMF_11(+)" + "\n");
            strSqlString.Append("           AND A.MAT_CMF_11 = C.MAT_CMF_11(+)" + "\n");
            strSqlString.Append("           AND NVL(A.WIP_DA_WB,0) + NVL(B.WIP_MID_MER,0) + NVL(B.WIP_BE,0) + NVL(A.WIP_SAW,0) +" + "\n");
            strSqlString.Append("               NVL(A.WIP_BG,0) + NVL(C.PLN_WEEK2,0) + NVL(C.PLN_WEEK4,0) > 0" + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" ORDER BY MAT_GRP_10, MAT_CMF_11, MAT_GRP_5" + "\n");            

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
            
            return strSqlString.ToString();
        }

        private void GetWeek()
        {

            StringBuilder strSqlString = new StringBuilder();            

            strSqlString.Append("SELECT WEEK" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT DISTINCT PLAN_YEAR||DECODE(LENGTH(PLAN_WEEK), 1, '0'||PLAN_WEEK, PLAN_WEEK) AS WEEK " + "\n");
            strSqlString.Append("          FROM MWIPCALDEF" + "\n");
            strSqlString.Append("         WHERE CALENDAR_ID = 'OTD'" + "\n");
            strSqlString.Append("           AND SYS_DATE BETWEEN '" + cdvDate.SelectedValue() + "' AND TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "', 'YYYYMMDD')+28, 'YYYYMMDD')" + "\n");
            strSqlString.Append("         ORDER BY WEEK" + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" WHERE ROWNUM <= 4" + "\n");
            strSqlString.Append(" ORDER BY WEEK" + "\n");

            dtWeek = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());

        }

        #endregion


        #region EVENT 처리
        /// <summary>
        /// 6. View 버튼 Action
        /// </summary>        ///         
        private void btnView_Click(object sender, EventArgs e)
        {                       
            DataTable dt = null;
            double iTTLWip = 0;
            double iPlan2 = 0;
            double iPlan4 = 0;
            double iWipDay2 = 0;
            double iWipDay4 = 0;

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
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2); 

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 2, 5, null, null, btnSort);                
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함
        
                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 5, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);

                Color color = spdData.ActiveSheet.Cells[1, 0].BackColor;
                                
                for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    // PKG Sub Total 삭제하기
                    if (spdData.ActiveSheet.Cells[i, 0].BackColor != color)
                    {
                        spdData.ActiveSheet.Rows[i].Remove();
                        i = i - 1;
                    }
                    
                    iTTLWip = Convert.ToInt32(spdData.ActiveSheet.Cells[i, 9].Value);
                    iPlan2 = Convert.ToInt32(spdData.ActiveSheet.Cells[i, 10].Value);
                    iPlan4 = Convert.ToInt32(spdData.ActiveSheet.Cells[i, 11].Value);

                    // PKG CODE 기준의 SubTotal 은 표시 하되 안에 데이터는 지운다.
                    if (spdData.ActiveSheet.Cells[i, 1].BackColor != color)
                    {
                        if (iPlan2 != 0)
                        {
                            iWipDay2 = iTTLWip / (iPlan2 / 14);
                            spdData.ActiveSheet.Cells[i, 12].Value = Math.Round(iWipDay2, 1);
                        }

                        if (iPlan4 != 0)
                        {
                            iWipDay4 = iTTLWip / (iPlan4 / 28);
                            spdData.ActiveSheet.Cells[i, 13].Value = Math.Round(iWipDay4, 1);
                        }
                    }
                    else
                    {
                        spdData.ActiveSheet.Cells[i, 10].Text = "";
                        spdData.ActiveSheet.Cells[i, 11].Text = "";
                    }
                }

                iTTLWip = Convert.ToInt32(spdData.ActiveSheet.Cells[0, 9].Value);
                iPlan2 = Convert.ToInt32(spdData.ActiveSheet.Cells[0, 10].Value);
                iPlan4 = Convert.ToInt32(spdData.ActiveSheet.Cells[0, 11].Value);

                if (iPlan2 != 0)
                {
                    iWipDay2 = iTTLWip / (iPlan2 / 14);
                    spdData.ActiveSheet.Cells[0, 12].Value = Math.Round(iWipDay2, 1);
                }

                if (iPlan4 != 0)
                {
                    iWipDay4 = iTTLWip / (iPlan4 / 28);
                    spdData.ActiveSheet.Cells[0, 13].Value = Math.Round(iWipDay4, 1);
                }

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
                spdData.ExportExcel();            
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
        }

        
        #endregion

    }       
}
