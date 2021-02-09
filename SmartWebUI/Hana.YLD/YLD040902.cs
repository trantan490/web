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

using System.Windows.Forms.DataVisualization.Charting;

namespace Hana.PQC
{
    /// <summary>
    /// 클  래  스: YLD040902<br/>
    /// 클래스요약: TEST Prime yield 관리<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2011-07-06<br/>
    /// 상세  설명: TEST Prime yield 를 조회한다.<br/>
    /// 변경  내용: <br/>
    /// 2011-07-12-임종우 : U_S_LIMIT 선 삭제, Product 조회 기능 추가, TEST 설비, PARA수, TEST TIME 추가 (고재명 요청)
    /// 2011-07-15-배수민 : 콤보박스 생성 및 콤보박스 내 일자기준(일자별 차트로 표현될 수 있도록) 추가 (고재명D 요청)
    /// 2011-08-01-배수민 : 한 Lot에 최종 TRAN_TIME만 나오도록 수정 (고재명D 요청) 
    /// 2011-08-17-배수민 : 설비별 검색조건 추가 (고재명D 요청)
    /// 2011-10-26-임종우 : TEST 공정의 데이터만 나오도록 (고재명 요청)
    /// 2012-05-16-임종우 : 현 재공 공정 표시 및 BAKE TIME(START ~ END) 표시 (고재명 요청)
    /// 2012-05-25-임종우 : Final Test TIME(T0100, T0400) 표시 - 단 Rework 은 제외 함. (고재명 요청)
    /// 2012-07-17-임종우 : Final Test Comment 표시 (고재명 요청)
    /// 2012-11-22-임종우 : Retest yield 5% 이상인 제품 색상 표시 (최윤혁 요청)
    /// 2012-12-17-임종우 : BAKE TIME(T1080) Rework 은 제외 함. 스칼라 쿼리 에러로 인해....
    /// 2013-04-25-임종우 : Retest yield 4% 이상인 제품 색상 표시 (황혜리 요청)
    /// 2013-06-05-임종우 : Prime Yield, FT Yield 의 Sub Total 과 Grand Total 을 직접 구하는걸로 변경 & Prime 수량, FT 수량 추가 (황혜리 요청)
    /// 2013-07-24-임종우 : Retest yield Sub Total, Grand Total 직접 구하는 걸로 변경 : Retest yield = FT Yield - Prime Yield (황혜리 요청)
    /// 2013-08-27-임종우 : 작업시작 시간 표시 (황혜리 요청)
    /// 2013-08-28-임종우 : Retest yield -> Recovery yield 로 컬럼명 변경 (김문한 요청)
    /// 2013-09-27-임종우 : Retest yield 3% 이상인 제품 색상 표시 (김문한 요청)
    /// 2015-07-02-임종우 : ChartFx -> MS Chart 변경
    /// 2018-01-08-임종우 : SubTotal, GrandTotal 평균값 구하기 Function 변경
    /// </summary>
    /// 

    public partial class YLD040902 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public YLD040902()
        {
            InitializeComponent();
            SortInit();
            GridColumnInit();
            cdvFromToDate.AutoBinding();
            this.SetFactory(GlobalVariable.gsTestDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsTestDefaultFactory;

            udcMSChart1.RPT_1_ChartInit();  //차트 초기화. 
        }

        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.Trim().Length == 0)
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
            spdData.RPT_ColumnInit();

            try
            {                
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot No", 0, 10, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Res ID", 0, 11, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Para cnt", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Test Time", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Oper", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Working start time", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Working End Time", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot Size", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Test Size", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Retest 율", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("Prime Out", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("FT Out", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Prime yield", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("FT yield", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("Recovery yield", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("Retest recall", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("Status", 0, 26, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Bake Time", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("F/T Time", 0, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Comment", 0, 29, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("ASSY_SITE", 0, 30, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);

                // Group항목이 있을경우 반드시 선언해줄것.
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1", "MAT.MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID", "MAT.MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lot No", "LOT_ID", "LOT_ID", true);
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        #region Spread 생성을 위한 쿼리
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;


            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond1);
            strSqlString.AppendFormat("     , RES_ID, PARA_CNT, TEST_TIME, OPER, START_TIME, TRAN_TIME " + "\n");
            strSqlString.AppendFormat("     , SUM(LOT_SIZE) AS LOT_SIZE " + "\n");
            strSqlString.AppendFormat("     , SUM(TEST_SIZE) AS TEST_SIZE" + "\n");
            strSqlString.AppendFormat("     , AVG(RETEST_PER) AS RETEST_PER" + "\n");
            strSqlString.AppendFormat("     , SUM(OUT_PRIME) AS OUT_PRIME" + "\n");
            strSqlString.AppendFormat("     , SUM(OUT_FT) AS OUT_FT" + "\n");
            strSqlString.AppendFormat("     , AVG(YIELD_PRIME) AS YIELD_PRIME" + "\n");
            strSqlString.AppendFormat("     , AVG(YIELD_FT) AS YIELD_FT" + "\n");
            strSqlString.AppendFormat("     , AVG(YIELD_RETEST) AS YIELD_RETEST" + "\n");
            strSqlString.AppendFormat("     , AVG(CNT) AS CNT" + "\n");
            strSqlString.AppendFormat("     , (SELECT DECODE(OPER, ' ', 'SHIP', OPER) FROM RWIPLOTSTS WHERE LOT_ID = DAT.LOT_ID) AS STATUS" + "\n");
            strSqlString.AppendFormat("     , ROUND((SELECT (TO_DATE(TRAN_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(START_TIME, 'YYYYMMDDHH24MISS')) * 24 * 60 FROM CWIPLOTEND WHERE LOT_ID = DAT.LOT_ID AND OLD_OPER = 'T1080' AND RWK_FLAG = ' '  AND HIST_DEL_FLAG = ' '),0) AS BAKE_TIME" + "\n");
            strSqlString.AppendFormat("     , ROUND((SELECT (TO_DATE(TRAN_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(START_TIME, 'YYYYMMDDHH24MISS')) * 24 * 60 FROM CWIPLOTEND WHERE LOT_ID = DAT.LOT_ID AND OLD_OPER IN ('T0100', 'T0400') AND RWK_FLAG = ' ' AND HIST_DEL_FLAG = ' '),0) AS FT_TIME" + "\n");
            
            // 2012-07-30-김민우- 중복발생 
            // SELECT MAX(LAST_COMMENT) FROM CWIPLOTEND WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND OLD_OPER IN ('T0100','T0400') AND LOT_DEL_FLAG = ' ' AND LOT_ID = DAT.LOT_ID) AS LAST_COMMENT 로하면 오라클 연결 끊김...이유는......
            //strSqlString.AppendFormat("     , MAX((SELECT LAST_COMMENT FROM CWIPLOTEND WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND OLD_OPER IN ('T0100','T0400') AND LOT_DEL_FLAG = ' ' AND LOT_ID = DAT.LOT_ID)) AS LAST_COMMENT" + "\n");
            strSqlString.AppendFormat("     , (SELECT LAST_COMMENT FROM CWIPLOTEND WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND OLD_OPER IN ('T0100','T0400') AND LOT_DEL_FLAG = ' ' AND LOT_ID = DAT.LOT_ID AND ROWNUM = 1) AS LAST_COMMENT" + "\n");
            // 2014-08-21-장한별 ASSY조립처 확인요청 황혜리사원
            strSqlString.AppendFormat("     , (SELECT LOT_CMF_7 FROM RWIPLOTSTS WHERE LOT_ID = DAT.LOT_ID) AS ASSY_SITE" + "\n");
           
            strSqlString.AppendFormat("  FROM MWIPMATDEF MAT  " + "\n");
            strSqlString.AppendFormat("     , (  " + "\n");
            strSqlString.AppendFormat("        SELECT MAT_ID, LOT_ID, RES_ID, OPER, TRAN_TIME " + "\n");            
            strSqlString.AppendFormat("             , MAX(RETEST_INQTY) AS LOT_SIZE " + "\n");
            strSqlString.AppendFormat("             , SUM(RETEST_INQTY) AS TEST_SIZE " + "\n");
            strSqlString.AppendFormat("             , MAX(RETEST_GOODQTY) AS OUT_PRIME " + "\n");
            strSqlString.AppendFormat("             , SUM(RETEST_GOODQTY) AS OUT_FT " + "\n");
            strSqlString.AppendFormat("             , ROUND(SUM(RETEST_INQTY) / MAX(RETEST_INQTY), 2) AS RETEST_PER  " + "\n");
            strSqlString.AppendFormat("             , MIN(RETEST_YIELD) AS YIELD_PRIME " + "\n");
            strSqlString.AppendFormat("             , MAX(RETEST_YIELD) AS YIELD_FT" + "\n");
            strSqlString.AppendFormat("             , MAX(RETEST_YIELD) - MIN(RETEST_YIELD) AS YIELD_RETEST " + "\n");
            strSqlString.AppendFormat("             , COUNT(*) - 1 AS CNT " + "\n");
            // 한 Lot에 최종 TRAN_TIME만 나오도록
            strSqlString.AppendFormat("             , ROW_NUMBER() OVER(PARTITION BY LOT_ID ORDER BY TRAN_TIME DESC) AS ROW_NUM  " + "\n");
            strSqlString.AppendFormat("             , ( " + "\n");
            strSqlString.AppendFormat("                SELECT MAX(TRAN_TIME) " + "\n");
            strSqlString.AppendFormat("                  FROM RWIPLOTHIS " + "\n");
            strSqlString.AppendFormat("                 WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.AppendFormat("                   AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.AppendFormat("                   AND TRAN_CODE = 'START' " + "\n");
            strSqlString.AppendFormat("                   AND OPER IN ('T0100','T0400') " + "\n");
            strSqlString.AppendFormat("                   AND LOT_ID = A.LOT_ID " + "\n");
            strSqlString.AppendFormat("               ) AS START_TIME " + "\n");
            strSqlString.AppendFormat("          FROM MWIPLOTRTT@RPTTOMES A" + "\n");            
            strSqlString.AppendFormat("         WHERE 1=1 " + "\n");            
            strSqlString.AppendFormat("           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            // 설비별 검색
            strSqlString.AppendFormat("           AND RES_ID " + cdvResID.SelectedValueToQueryString + "\n");
            strSqlString.AppendFormat("           AND TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", cdvFromToDate.Start_Tran_Time, cdvFromToDate.End_Tran_Time);
            strSqlString.AppendFormat("           AND RETEST_YIELD > 0" + "\n");

            // 2011-10-26-임종우 : TEST 공정의 데이터만 나오도록 (고재명 요청)
            strSqlString.AppendFormat("           AND OPER IN ('T0100','T0400')" + "\n");

            strSqlString.AppendFormat("         GROUP BY MAT_ID, LOT_ID, RES_ID, OPER, TRAN_TIME " + "\n");
            strSqlString.AppendFormat("       ) DAT " + "\n");
            strSqlString.AppendFormat("     , (  " + "\n");
            strSqlString.AppendFormat("        SELECT * " + "\n");
            strSqlString.AppendFormat("          FROM ( " + "\n");
            strSqlString.AppendFormat("                SELECT ATTR_KEY AS MAT_ID " + "\n");
            strSqlString.AppendFormat("                     , DECODE(ATTR_NAME, 'TEST_TIME_QUAD', '4', 'TEST_TIME_DUAL', '2', '1') AS PARA_CNT " + "\n");
            strSqlString.AppendFormat("                     , ATTR_VALUE AS TEST_TIME " + "\n");
            strSqlString.AppendFormat("                     , RANK() OVER(PARTITION BY ATTR_KEY ORDER BY ATTR_NAME DESC) AS RNK " + "\n");
            strSqlString.AppendFormat("                  FROM MATRNAMSTS " + "\n");
            strSqlString.AppendFormat("                 WHERE 1=1 " + "\n");
            strSqlString.AppendFormat("                   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("                   AND ATTR_TYPE = 'MAT_TEST_PGM'" + "\n");
            strSqlString.AppendFormat("                   AND ATTR_NAME LIKE 'TEST_TIME%'" + "\n");
            strSqlString.AppendFormat("                   AND ATTR_VALUE <> ' '" + "\n");
            strSqlString.AppendFormat("               ) " + "\n");
            strSqlString.AppendFormat("         WHERE RNK = 1 " + "\n");
            strSqlString.AppendFormat("       ) ATT " + "\n");
            strSqlString.AppendFormat(" WHERE 1=1 " + "\n");
            strSqlString.AppendFormat("   AND DAT.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.AppendFormat("   AND DAT.MAT_ID = ATT.MAT_ID(+) " + "\n");
            strSqlString.AppendFormat("   AND MAT.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.AppendFormat("   AND MAT.MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");
            strSqlString.AppendFormat("   AND DAT.ROW_NUM = '1' " + "\n");

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

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

            strSqlString.AppendFormat(" GROUP BY {0}, RES_ID, PARA_CNT, TEST_TIME, OPER, START_TIME, TRAN_TIME " + "\n", QueryCond2);
            strSqlString.AppendFormat(" ORDER BY {0}, RES_ID, PARA_CNT, TEST_TIME, OPER, START_TIME, TRAN_TIME " + "\n", QueryCond2);


            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region Chart 생성을 위한 쿼리
        private string MakeChart()
        {
            StringBuilder strSqlString = new StringBuilder();

            //if (cdvMat_Day.Text == "제품기준")
            if (cdvMat_Day.SelectedIndex == 0)
            {
                strSqlString.AppendFormat("SELECT DAT.MAT_ID" + "\n");
                strSqlString.AppendFormat("     , ROUND(AVG(YIELD_RETEST), 2) AS YIELD_RETEST" + "\n");
                strSqlString.AppendFormat("     , ROUND(AVG(YIELD_1ST), 2) AS YIELD_PRIME" + "\n");
                strSqlString.AppendFormat("     , ROUND(AVG(YIELD_FT), 2) AS YIELD_FT" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(SUM(YIELD_RETEST)) OVER() / SUM(SUM(1)) OVER(), 2) AS AVG_RETEST" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(SUM(YIELD_1ST)) OVER() / SUM(SUM(1)) OVER(), 2) AS AVG_PRIME" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(SUM(YIELD_FT)) OVER() / SUM(SUM(1)) OVER(), 2) AS AVG_FT" + "\n");
                strSqlString.AppendFormat("     , ROUND(AVG(CNT), 2) AS RETEST_CNT" + "\n");
                strSqlString.AppendFormat("     , ROUND(AVG(RETEST_PER), 2) AS RETEST_PER    " + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(SUM(CNT)) OVER() / SUM(SUM(1)) OVER(), 2) AS AVG_RETEST_CNT" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(SUM(RETEST_PER)) OVER() / SUM(SUM(1)) OVER(), 2) AS AVG_RETEST_PER " + "\n");
                strSqlString.AppendFormat("  FROM (" + "\n");
                strSqlString.AppendFormat("        SELECT MAT_ID, LOT_ID" + "\n");
                strSqlString.AppendFormat("             , MIN(RETEST_YIELD) AS YIELD_1ST" + "\n");
                strSqlString.AppendFormat("             , MAX(RETEST_YIELD) - MIN(RETEST_YIELD) AS YIELD_RETEST" + "\n");
                strSqlString.AppendFormat("             , MAX(RETEST_YIELD) AS YIELD_FT             " + "\n");
                strSqlString.AppendFormat("             , COUNT(*) - 1 AS CNT" + "\n");
                strSqlString.AppendFormat("             , SUM(RETEST_INQTY) / MAX(RETEST_INQTY) AS RETEST_PER" + "\n");
                strSqlString.AppendFormat("          FROM MWIPLOTRTT@RPTTOMES" + "\n");
                strSqlString.AppendFormat("         WHERE 1=1" + "\n");
                strSqlString.AppendFormat("           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                // 설비별 검색
                strSqlString.AppendFormat("           AND RES_ID " + cdvResID.SelectedValueToQueryString + "\n");
                strSqlString.AppendFormat("           AND TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", cdvFromToDate.Start_Tran_Time, cdvFromToDate.End_Tran_Time);
                strSqlString.AppendFormat("           AND RETEST_YIELD > 0" + "\n");

                // 2011-10-26-임종우 : TEST 공정의 데이터만 나오도록 (고재명 요청)
                strSqlString.AppendFormat("           AND OPER IN ('T0100','T0400')" + "\n");

                strSqlString.AppendFormat("         GROUP BY MAT_ID, LOT_ID" + "\n");
                strSqlString.AppendFormat("       ) DAT" + "\n");
                strSqlString.AppendFormat("     , MWIPMATDEF MAT" + "\n");
                strSqlString.AppendFormat(" WHERE 1=1" + "\n");
                strSqlString.AppendFormat("   AND DAT.MAT_ID = MAT.MAT_ID" + "\n");
                strSqlString.AppendFormat("   AND MAT.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.AppendFormat("   AND MAT.MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");

                //상세 조회에 따른 SQL문 생성                        
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

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

                strSqlString.AppendFormat(" GROUP BY DAT.MAT_ID" + "\n");
            }

            //else if (cdvMat_Day.Text == "일자기준")
            else if (cdvMat_Day.SelectedIndex == 1)
            {
                strSqlString.AppendFormat("SELECT DDAY" + "\n");
                strSqlString.AppendFormat("     , ROUND(AVG(YIELD_RETEST), 2) AS YIELD_RETEST" + "\n");
                strSqlString.AppendFormat("     , ROUND(AVG(YIELD_1ST), 2) AS YIELD_PRIME" + "\n");
                strSqlString.AppendFormat("     , ROUND(AVG(YIELD_FT), 2) AS YIELD_FT" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(SUM(YIELD_RETEST)) OVER() / SUM(SUM(1)) OVER(), 2) AS AVG_RETEST" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(SUM(YIELD_1ST)) OVER() / SUM(SUM(1)) OVER(), 2) AS AVG_PRIME" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(SUM(YIELD_FT)) OVER() / SUM(SUM(1)) OVER(), 2) AS AVG_FT" + "\n");
                strSqlString.AppendFormat("     , ROUND(AVG(CNT), 2) AS RETEST_CNT" + "\n");
                strSqlString.AppendFormat("     , ROUND(AVG(RETEST_PER), 2) AS RETEST_PER    " + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(SUM(CNT)) OVER() / SUM(SUM(1)) OVER(), 2) AS AVG_RETEST_CNT" + "\n");
                strSqlString.AppendFormat("     , ROUND(SUM(SUM(RETEST_PER)) OVER() / SUM(SUM(1)) OVER(), 2) AS AVG_RETEST_PER " + "\n");
                strSqlString.AppendFormat("  FROM (" + "\n");
                strSqlString.AppendFormat("        SELECT GET_WORK_DATE(TRAN_TIME, 'D') AS DDAY, MAT_ID, LOT_ID" + "\n");
                strSqlString.AppendFormat("             , MIN(RETEST_YIELD) AS YIELD_1ST" + "\n");
                strSqlString.AppendFormat("             , MAX(RETEST_YIELD) - MIN(RETEST_YIELD) AS YIELD_RETEST" + "\n");
                strSqlString.AppendFormat("             , MAX(RETEST_YIELD) AS YIELD_FT             " + "\n");
                strSqlString.AppendFormat("             , COUNT(*) - 1 AS CNT" + "\n");
                strSqlString.AppendFormat("             , SUM(RETEST_INQTY) / MAX(RETEST_INQTY) AS RETEST_PER" + "\n");
                strSqlString.AppendFormat("          FROM MWIPLOTRTT@RPTTOMES" + "\n");
                strSqlString.AppendFormat("         WHERE 1=1" + "\n");
                strSqlString.AppendFormat("           AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                // 설비별 검색
                strSqlString.AppendFormat("           AND RES_ID " + cdvResID.SelectedValueToQueryString + "\n");
                strSqlString.AppendFormat("           AND TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", cdvFromToDate.Start_Tran_Time, cdvFromToDate.End_Tran_Time);
                strSqlString.AppendFormat("           AND RETEST_YIELD > 0" + "\n");
                strSqlString.AppendFormat("         GROUP BY MAT_ID, LOT_ID, TRAN_TIME" + "\n");
                strSqlString.AppendFormat("       ) DAT" + "\n");
                strSqlString.AppendFormat("     , MWIPMATDEF MAT" + "\n");
                strSqlString.AppendFormat(" WHERE 1=1" + "\n");
                strSqlString.AppendFormat("   AND DAT.MAT_ID = MAT.MAT_ID" + "\n");
                strSqlString.AppendFormat("   AND MAT.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.AppendFormat("   AND MAT.MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");

                //상세 조회에 따른 SQL문 생성                        
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

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

                strSqlString.AppendFormat(" GROUP BY DDAY" + "\n");
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion
        
        /// <summary>
        /// 5. Chart 생성
        /// </summary>
        /// <param name="rowCount">Row Number</param>
        private void ShowChart()
        {
            DataTable dtChart = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeChart());

            udcMSChart1.RPT_2_ClearData();
            udcMSChart1.RPT_3_OpenData(6, dtChart.Rows.Count);

            int[] wip_rows = new Int32[dtChart.Rows.Count];
            int[] tat_rows = new Int32[dtChart.Rows.Count];
            int arrCnt = 0;

            if (rdbYield.Checked == true)
            {
                string[] LegBox = new string[6]; //데이터 테이블의 2째 컬럼헤더 부터 사용하기에 -1을 한다.

                for (int i = 0; i < dtChart.Rows.Count; i++)
                {
                    wip_rows[i] = i;
                    tat_rows[i] = i;
                }

                // 데이터 테이블의 컬럼헤더를 저장한다. LegBox 표시 값으로 사용하려...
                for (int x = 1; x < 7; x++)
                {
                    LegBox[arrCnt] = dtChart.Columns[x].ToString();

                    arrCnt++;
                }

                double max = 0;
                double max_temp = 0;

                // 데이터 테이블의 1~6 컬럼의 데이터를 표시한다.
                for (int j = 1; j < 7; j++)
                {
                    max = udcMSChart1.RPT_4_AddData(dtChart, wip_rows, new int[] { j }, SeriseType.Column);

                    if (max > max_temp)
                    {
                        max_temp = max;
                    }
                }

                max = max_temp;

                udcMSChart1.RPT_5_CloseData();

                udcMSChart1.RPT_6_SetGallery(SeriesChartType.Bar, 0, 1, "", AsixType.Y, DataTypes.Initeger, 100);

                for (int i = 1; i < 6; i++)
                {
                    udcMSChart1.RPT_6_SetGallery(SeriesChartType.Line, i, 1, "", AsixType.Y, DataTypes.Initeger, 100);
                }

                udcMSChart1.RPT_7_SetXAsixTitleUsingDataTable(dtChart, 0);
                udcMSChart1.RPT_8_SetSeriseLegend(LegBox, System.Windows.Forms.DataVisualization.Charting.Docking.Bottom);

                Title t = new Title("TEST Yield", Docking.Top, new System.Drawing.Font("Tahoma", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                udcMSChart1.Titles.Add(t);                
                //udcMSChart1.Series[3].LineStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                //udcMSChart1.Series[4].LineStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                //udcMSChart1.Series[5].LineStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                //udcMSChart1.Series[3].LineWidth = 1;
                //udcMSChart1.Series[4].LineWidth = 1;
                //udcMSChart1.Series[5].LineWidth = 1;
            }
            else
            {
                string[] LegBox = new string[4]; //데이터 테이블의 2째 컬럼헤더 부터 사용하기에 -1을 한다.

                for (int i = 0; i < dtChart.Rows.Count; i++)
                {
                    wip_rows[i] = i;
                    tat_rows[i] = i;
                }

                // 데이터 테이블의 컬럼헤더를 저장한다. LegBox 표시 값으로 사용하려...
                for (int x = 7; x < 11; x++)
                {
                    LegBox[arrCnt] = dtChart.Columns[x].ToString();

                    arrCnt++;
                }

                double max = 0;
                double max_temp = 0;

                // 데이터 테이블의 7 ~ 10 컬럼의 데이터를 표시한다.
                for (int j = 7; j < 11; j++)
                {
                    max = udcMSChart1.RPT_4_AddData(dtChart, wip_rows, new int[] { j }, SeriseType.Column);

                    if (max > max_temp)
                    {
                        max_temp = max;
                    }
                }

                max = max_temp;

                udcMSChart1.RPT_5_CloseData();

                udcMSChart1.RPT_6_SetGallery(SeriesChartType.Bar, 0, 1, "", AsixType.Y, DataTypes.Initeger, max * 1.1);

                for (int i = 1; i < 4; i++)
                {
                    udcMSChart1.RPT_6_SetGallery(SeriesChartType.Line, i, 1, "", AsixType.Y, DataTypes.Initeger, max * 1.1);
                }

                udcMSChart1.RPT_7_SetXAsixTitleUsingDataTable(dtChart, 0);
                udcMSChart1.RPT_8_SetSeriseLegend(LegBox, System.Windows.Forms.DataVisualization.Charting.Docking.Bottom);

                Title t = new Title("Retest Proportion", Docking.Top, new System.Drawing.Font("Tahoma", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                udcMSChart1.Titles.Add(t); 

                //udcMSChart1.Titles[0].Text = "Retest Proportion";
                //udcMSChart1.Series[2].LineStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                //udcMSChart1.Series[3].LineStyle = System.Drawing.Drawing2D.DashStyle.Dot;                
                //udcMSChart1.Series[2].LineWidth = 1;
                //udcMSChart1.Series[3].LineWidth = 1;
                //udcMSChart1.Series[4].Color = Color.Red;
            }

            // 기타 설정
            //udcMSChart1.PointLabels = true;
            //udcMSChart1.AxisY.LabelsFormat.Decimals = 2;
            //udcMSChart1.AxisY.DataFormat.Decimals = 2;
            ////udcMSChart1.AxisX.LabelAngle = 90;
            //udcMSChart1.AxisX.Visible = false; // x축 Label 값 표시 안함.
            //udcMSChart1.AxisX.Staggered = false;
            //udcMSChart1.LegendBoxObj.AutoSize = true; // LegendBox 텍스트 전부 다 보이기..
            //udcMSChart1.Scrollable = true;

            //udcMSChart1.Series[1].Gallery = System.Windows.Forms.DataVisualization.Charting.Gallery.Lines;
            //udcMSChart1.MarkerShape = System.Windows.Forms.DataVisualization.Charting.MarkerShape.None; // 포인트 점 없애기..
            //udcMSChart1.AxisX.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            //udcMSChart1.AxisY.Gridlines = false;
        }

        #endregion

        /// <summary>
        /// 버튼 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Button Click Event

        private void btnView_Click(object sender, EventArgs e)
        {
            if (CheckField() == false) return;

            LoadingPopUp.LoadIngPopUpShow(this);

            DataTable dt = null;
            GridColumnInit();

            try
            {
                this.Refresh();
                LoadingPopUp.LoadIngPopUpShow(this);
                spdData_Sheet1.RowCount = 0;                
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    udcMSChart1.RPT_1_ChartInit();
                    udcMSChart1.RPT_2_ClearData();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                int nGroupCount = ((udcTableForm)(this.btnSort.BindingForm)).GetSelectedCount();

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 17, null, null, btnSort);


                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 17, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);
                //--------------------------------------------

                //5. 평균값
                spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 19, nGroupCount, true);
                spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 25, nGroupCount, true);
                spdData.RPT_SetPerSubTotalAndGrandTotal(1, 20, 17, 22);
                spdData.RPT_SetPerSubTotalAndGrandTotal(1, 21, 17, 23);    

                Color color = spdData.ActiveSheet.Cells[1, 24].BackColor;                
                Decimal dYield ;

                // 2012-11-22-임종우 : Retest yield 5% 이상인 제품 색상 표시 (최윤혁 요청)
                // 2013-04-25-임종우 : Retest yield 4% 이상인 제품 색상 표시 (황혜리 요청)
                // 2013-07-24-임종우 : Retest yield Sub Total, Grand Total 구하는 부분 추가 (황혜리 요청)
                // 2013-09-27-임종우 : Retest yield 3% 이상인 제품 색상 표시 (김문한 요청)
                for (int i = 1; i < spdData.ActiveSheet.RowCount; i++)
                {
                    dYield = Convert.ToDecimal(spdData.ActiveSheet.Cells[i, 24].Value);

                    if (spdData.ActiveSheet.Cells[i, 24].BackColor == color && spdData.ActiveSheet.Cells[i, 24].Value != null)
                    {
                        if (dYield >= 3)
                        {
                            for (int y = 17; y <= 29; y++)
                            {
                                spdData.ActiveSheet.Cells[i, y].BackColor = Color.Pink;
                            }
                        }  
                    }

                    // Retest yield Sub Total 부분
                    if (spdData.ActiveSheet.Cells[i, 24].BackColor != color)
                    {
                        spdData.ActiveSheet.Cells[i, 24].Value = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 23].Value) -Convert.ToDouble(spdData.ActiveSheet.Cells[i, 22].Value);
                    }
                }

                // Retest yield Grand Total 부분
                spdData.ActiveSheet.Cells[0, 24].Value = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 23].Value) - Convert.ToDouble(spdData.ActiveSheet.Cells[0, 22].Value);

                dt.Dispose();

                ShowChart();
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

        //2011-08-17-배수민 : 설비별 검색조건 추가 (고재명D 요청)
        private void cdvResID_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            cdvResID.Init(); //검색일마다 가져오는 설비 다르므로 초기화

            strQuery += "SELECT DISTINCT RES_ID AS CODE, ' ' AS NAME FROM MWIPLOTRTT@RPTTOMES" + "\n";
            strQuery += " WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n";
            strQuery += "   AND TRAN_TIME BETWEEN '" + cdvFromToDate.Start_Tran_Time + "' AND '" + cdvFromToDate.End_Tran_Time + "'" + "\n";
            strQuery += "   AND RETEST_YIELD > 0 " + "\n";
            strQuery += " ORDER BY RES_ID " + "\n";

            cdvResID.sDynamicQuery = strQuery;
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, udcMSChart1, this.lblTitle.Text, null, null);
            spdData.ExportExcel();
        }

        #endregion        
    }
}