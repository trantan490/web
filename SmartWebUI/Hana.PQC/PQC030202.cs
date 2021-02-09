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

namespace Hana.PQC
{
    public partial class PQC030202 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        DataTable dt = new DataTable();


        #region " PQC030202 : Program Initial "

        /// <summary>
        /// 클  래  스: PQC030202<br/>
        /// 클래스요약: CPK 결과 조회<br/>
        /// 작  성  자: 임종우<br/>
        /// 최초작성일: 2011-11-22 <br/>
        /// 상세  설명: CPK 결과 조회<br/>
        /// 변경  내용: 
        /// 변 경 자 :  <br />
        /// 2012-03-05-임종우 : CHART_ID 검색조건을 CHAR_ID 로 변경, 고객사, PKG, .. 등 챠트 셋업 그룹 검색 기능 추가
        /// 2012-03-07-임종우 : 캐릭터 ID 데이터 표시 되도록 수정 함.
        /// 2012-03-08-임종우 : USL, LSL 데이터 표시 되도록 수정 함.
        /// 2012-03-22-임종우 : 샘플수량 2개 이상인것 모두 표현되도록 수정 함, CPK - PPK 절대 값으로 표시 요청(최재주 요청)
        /// 2012-04-10-임종우 : 캐릭터별 D2 값 반영하여 계산하도록 수정 (최재주 요청)
        /// 2012-06-08-임종우 : 삭제 된 LOT 제외 시킴 (이민희 요청)
        /// 2014-10-16-임종우 : PPK 항목 삭제 - 고객사 Audit 중 데이터 확인함에 혼선유발을 예방하기 위해서... (박상현 요청)
        /// 2017-11-13-임종우 : Bump 쪽 쿼리 정리 및 D2 값 YMS 에서 가져오도록 수정 (서동휘 요청)
        /// 2017-12-06-임종우 : Bump 쪽 쿼리 신규 개발
        /// 2018-04-18-임종우 : Bump 쪽 그룹정보에 설비 추가 (서동휘 요청)
        /// </summary>        
        public PQC030202()
        {
            InitializeComponent();            
            udcFromToDate.AutoBinding();
            SortInit();
            GridColumnInit(); 

            // 기본공정은 HMKA1으로 설정
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;

            cdvCharID.Visible = true;
            cdvRasId.Visible = true;
            cdvRasId.sFactory = cdvFactory.Text;

            cdvOperID.Visible = false;
            cdvSpecID.Visible = false;
            cdvOperID.sFactory = cdvFactory.Text;

            udcFromToDate.DaySelector.Visible = false;
        }

        #endregion


        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.Trim() == "HMKB1")
            {
            }
            else
            {
                if ((cdvCharID.Text == "ALL" || cdvCharID.Text == "") && (udcWIPCondition3.Text == "ALL" || udcWIPCondition3.Text == ""))
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD044", GlobalVariable.gcLanguage));
                    return false;
                }
            }

            return true;
        }

        #endregion


        #region " GridColumnInit : Sheet Title 설정 " : SPREAD가 없으니 필요없음

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();

            try
            {
                if (cdvFactory.Text.Trim() == "HMKB1")
                {
                    spdData.RPT_AddBasicColumn("Date", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Customer", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Bumping Type", 0, 2, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Operation Flow", 0, 3, Visibles.False, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Layer classification", 0, 4, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PKG Type", 0, 5, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("RDL Plating", 0, 6, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Final Bump", 0, 7, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Sub. Material", 0, 8, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Size", 0, 9, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Thickness", 0, 10, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Flat Type", 0, 11, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Wafer Orientation", 0, 12, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Oper", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);                    
                    spdData.RPT_AddBasicColumn("Spec ID", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 150);
                    spdData.RPT_AddBasicColumn("RES ID", 0, 15, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    
                    spdData.RPT_AddBasicColumn("Sample", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("USL", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LSL", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Min", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Max", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Ave", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Std", 0, 22, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Cpk", 0, 23, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    //spdData.RPT_AddBasicColumn("Ppk", 0, 25, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Date", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("COL SET ID", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 150);
                    spdData.RPT_AddBasicColumn("Character ID", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 150);
                    spdData.RPT_AddBasicColumn("Customer", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);                
                    spdData.RPT_AddBasicColumn("Package", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Type1", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LD Count", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Density", 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Generation", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);                
                    spdData.RPT_AddBasicColumn("RES ID", 0, 9, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Sample", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("USL", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LSL", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Min", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Max", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Ave", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Std", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Cpk", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Ppk", 0, 18, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 70); // 표시 안함 (임시적으로..)
                }

                spdData.RPT_ColumnConfigFromTable_New(btnSort);
                //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        #endregion


        #region " SortInit : Group By 설정 " : SPREAD가 없으니 필요없다.

        /// <summary>
        /// 3. Group By 정의 
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();

            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Date", "TRAN_DATE", "TRAN_DATE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "MAT_GRP_1", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Bumping Type", "MAT_GRP_2", "MAT_GRP_2", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation Flow", "MAT_GRP_3", "MAT_GRP_3", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "MAT_GRP_4", "MAT_GRP_4", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG Type", "MAT_GRP_5", "MAT_GRP_5", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL Plating", "MAT_GRP_6", "MAT_GRP_6", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Final Bump", "MAT_GRP_7", "MAT_GRP_7", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Sub. Material", "MAT_GRP_8", "MAT_GRP_8", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Size", "MAT_CMF_14", "MAT_CMF_14", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Thickness", "MAT_CMF_2", "MAT_CMF_2", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Flat Type", "MAT_CMF_3", "MAT_CMF_3", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Wafer Orientation", "MAT_CMF_4", "MAT_CMF_4", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Oper", "STEP_ID", "A.STEP_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Spec ID", "SPEC_ID", "A.SPEC_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Res ID", "RES_ID", "A.RES_ID", false);
            }
            else
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Date", "TRAN_DATE", "TRAN_DATE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("COL SET ID", "COL_SET_ID", "A.COL_SET_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Character ID", "CHAR_ID", "B.CHAR_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "CHT_GRP_1", "B.CHT_GRP_1", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "CHT_GRP_2", "B.CHT_GRP_2", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "CHT_GRP_3", "B.CHT_GRP_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "CHT_GRP_4", "B.CHT_GRP_4", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "CHT_GRP_5", "B.CHT_GRP_5", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "CHT_GRP_6", "B.CHT_GRP_6", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Res ID", "RES_ID", "A.RES_ID", false);
            }
        }

        #endregion


        #region " MakeSqlString : Sql Query문 "

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
            string strCheckd = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            string strFromDate = udcFromToDate.ExactFromDate;
            string strToDate = udcFromToDate.ExactToDate;

            if (rdbDay.Checked)
                strCheckd = "                     , SYS_DATE AS TRAN_DATE";
            else if(rdbWeek.Checked)
                strCheckd = "                     , PLAN_YEAR || PLAN_WEEK AS TRAN_DATE";
            else if(rdbMonth.Checked)
                strCheckd = "                     , PLAN_YEAR || PLAN_MONTH AS TRAN_DATE";
            else
                strCheckd = "                     , PLAN_YEAR || PLAN_QUARTER AS TRAN_DATE";

            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond1);      
                strSqlString.AppendFormat("     , SAMPLE_QTY " + "\n");
                strSqlString.AppendFormat("     , ROUND(USL, 1) AS USL " + "\n");
                strSqlString.AppendFormat("     , ROUND(LSL, 1) AS LSL " + "\n");
                strSqlString.AppendFormat("     , ROUND(MIN_VALUE, 2) AS MIN_VALUE" + "\n");
                strSqlString.AppendFormat("     , ROUND(MAX_VALUE, 2) AS MAX_VALUE" + "\n");
                strSqlString.AppendFormat("     , ROUND(AVG_VALUE, 2) AS AVG_VALUE" + "\n");
                strSqlString.AppendFormat("     , ROUND(RNG_VALUE / D2, 2) AS STDDEV_VALUE" + "\n");
                strSqlString.AppendFormat("     , ROUND(ABS(CASE WHEN (RNG_VALUE = 0) THEN 0 " + "\n");
                strSqlString.AppendFormat("                      WHEN (USL = 0) THEN ((AVG_VALUE - LSL) / (3 * (RNG_VALUE / D2))) " + "\n");
                strSqlString.AppendFormat("                      WHEN (LSL = 0) THEN ((USL - AVG_VALUE) / (3 * (RNG_VALUE / D2))) " + "\n");
                strSqlString.AppendFormat("                      WHEN (USL - LSL = 0) THEN 0 " + "\n");
                strSqlString.AppendFormat("                      WHEN (D2 = 0) THEN 0 " + "\n");
                strSqlString.AppendFormat("                      ELSE (1 - (ABS(((USL + LSL) / 2) - AVG_VALUE) / ((USL - LSL) / 2))) * ((USL - LSL) / (6 * (RNG_VALUE / D2))) " + "\n");
                strSqlString.AppendFormat("                 END), 2) AS CPK " + "\n");
                strSqlString.AppendFormat("  FROM (" + "\n");
                strSqlString.AppendFormat("        SELECT {0} " + "\n", QueryCond2);           
                strSqlString.AppendFormat("             , SUM(SITE_QTY) AS SAMPLE_QTY " + "\n");                
                strSqlString.AppendFormat("             , MIN(A.EXT_MIN) AS MIN_VALUE " + "\n");
                strSqlString.AppendFormat("             , MAX(A.EXT_MAX) AS MAX_VALUE " + "\n");
                strSqlString.AppendFormat("             , AVG((A.EXT_MAX - A.EXT_MIN)) AS RNG_VALUE " + "\n");
                strSqlString.AppendFormat("             , AVG(A.EXT_AVERAGE) AS AVG_VALUE " + "\n");
                strSqlString.AppendFormat("             , CASE WHEN SUM(DECODE(TRIM(USL), '', 0, 1)) = 0 THEN 0" + "\n");
                strSqlString.AppendFormat("                    ELSE SUM(DECODE(TRIM(USL), '', 0, USL)) / SUM(DECODE(TRIM(USL), '', 0, 1))" + "\n");
                strSqlString.AppendFormat("               END USL" + "\n");
                strSqlString.AppendFormat("             , CASE WHEN SUM(DECODE(TRIM(LSL), '', 0, 1)) = 0 THEN 0" + "\n");
                strSqlString.AppendFormat("                    ELSE SUM(DECODE(TRIM(LSL), '', 0, LSL)) / SUM(DECODE(TRIM(LSL), '', 0, 1))" + "\n");
                strSqlString.AppendFormat("               END LSL " + "\n");
                strSqlString.AppendFormat("             , MAX((SELECT USER_CMF_05 FROM TQS_SPEC_CONFIG@RPTTOFA WHERE SPEC_PARENT_SEQ = A.PARENT_SEQ)) AS D2 " + "\n");
                strSqlString.AppendFormat("          FROM (" + "\n");
                strSqlString.AppendFormat("                SELECT A.* " + "\n");
                strSqlString.AppendFormat(strCheckd + " \n");
                strSqlString.AppendFormat("                  FROM TQS_SUMMARY_DATA@RPTTOFA A " + "\n");
                strSqlString.AppendFormat("                     , TQS_EDCSPEC_EXKEY@RPTTOFA B  " + "\n");
                strSqlString.AppendFormat("                     , MWIPCALDEF C " + "\n");
                strSqlString.AppendFormat("                 WHERE 1=1 " + "\n");
                strSqlString.AppendFormat("                   AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.AppendFormat("                   AND A.SPEC_SEQ = B.SPEC_SEQ " + "\n");
                strSqlString.AppendFormat("                   AND A.FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.AppendFormat("                   AND TO_CHAR(A.TRAN_TIME + 2/24, 'YYYYMMDD') = C.SYS_DATE " + "\n");                
                strSqlString.AppendFormat("                   AND A.DISPLAY_FLAG = 'Y' " + "\n");
                strSqlString.AppendFormat("                   AND A.TRAN_TIME BETWEEN TO_DATE('" + strFromDate + "', 'YYYYMMDDHH24MISS') AND TO_DATE('" + strToDate + "', 'YYYYMMDDHH24MISS')" + "\n");
                strSqlString.AppendFormat("                   AND B.DELETE_FLAG <> 'Y' " + "\n");
                strSqlString.AppendFormat("                   AND NVL(B.DELETE_FLAG, 'N') <> 'Y' " + "\n");
                strSqlString.AppendFormat("                   AND C.CALENDAR_ID = 'HM' " + "\n");

                if (cdvSpecID.Text != "ALL" && cdvSpecID.Text != "")
                    strSqlString.AppendFormat("                   AND A.SPEC_ID " + cdvSpecID.SelectedValueToQueryString + "\n");

                if (cdvOperID.Text != "ALL" && cdvOperID.Text != "")
                    strSqlString.AppendFormat("                   AND A.STEP_ID " + cdvOperID.SelectedValueToQueryString + "\n");
                                
                strSqlString.AppendFormat("               ) A " + "\n");
                strSqlString.AppendFormat("             , MWIPMATDEF MAT " + "\n");                
                strSqlString.AppendFormat("         WHERE 1=1 " + "\n");
                strSqlString.AppendFormat("           AND A.FACTORY = MAT.FACTORY " + "\n");
                strSqlString.AppendFormat("           AND A.MAT_ID = MAT.MAT_ID " + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                #endregion

                strSqlString.AppendFormat("         GROUP BY {0}" + " \n", QueryCond2);                      
                strSqlString.AppendFormat("       ) A " + "\n");
                strSqlString.AppendFormat(" WHERE 1=1 " + "\n");
                strSqlString.AppendFormat(" ORDER BY {0}" + "\n", QueryCond1);
            }
            else
            {
                // 쿼리 
                strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond1);
                strSqlString.AppendFormat("     , VALUE_COUNT " + "\n");
                strSqlString.AppendFormat("     , ROUND(USL, 1) AS USL" + "\n");
                strSqlString.AppendFormat("     , ROUND(LSL, 1) AS LSL " + "\n");
                strSqlString.AppendFormat("     , ROUND(MIN_VALUE, 2) AS MIN_VALUE" + "\n");
                strSqlString.AppendFormat("     , ROUND(MAX_VALUE, 2) AS MAX_VALUE" + "\n");
                strSqlString.AppendFormat("     , ROUND(AVERAGE, 2) AS AVERAGE" + "\n");
                strSqlString.AppendFormat("     , ROUND(STDDEV, 2) AS STDDEV" + "\n");
                strSqlString.AppendFormat("     , ROUND(ABS(CASE WHEN (RANGE = 0) THEN 0" + "\n");
                strSqlString.AppendFormat("                      WHEN (USL = 0) THEN ((AVERAGE - LSL) / (3 * (RANGE / D2)))" + "\n");
                strSqlString.AppendFormat("                      WHEN (LSL = 0) THEN ((USL - AVERAGE) / (3 * (RANGE / D2)))" + "\n");
                strSqlString.AppendFormat("                      WHEN USL - LSL = 0 THEN 0" + "\n");
                strSqlString.AppendFormat("                      ELSE (1 - (ABS(((USL + LSL) / 2) - AVERAGE) / ((USL - LSL) / 2))) *  ((USL - LSL) / (6 * (RANGE / D2)))" + "\n");
                strSqlString.AppendFormat("                 END), 2) AS CPK " + "\n");
                strSqlString.AppendFormat("     , ROUND(ABS(CASE WHEN (USL = 0) THEN DECODE(STDDEV, 0, 0, ((AVERAGE - LSL) / (3 * STDDEV)))" + "\n");
                strSqlString.AppendFormat("                      WHEN (LSL = 0) THEN DECODE(STDDEV, 0, 0, ((USL - AVERAGE) / (3 * STDDEV)))" + "\n");
                strSqlString.AppendFormat("                      WHEN USL - LSL = 0 THEN 0" + "\n");
                strSqlString.AppendFormat("                      ELSE (1 - (ABS(((USL + LSL) / 2) - AVERAGE) / ((USL - LSL) / 2))) * DECODE(STDDEV, 0, 0, ((USL - LSL) / (6 * STDDEV)))" + "\n");
                strSqlString.AppendFormat("                 END), 2) AS PPK " + "\n");
                strSqlString.AppendFormat("  FROM (" + "\n");
                strSqlString.AppendFormat("        SELECT {0}" + "\n", QueryCond2);
                strSqlString.AppendFormat("             , SUM(VALUE_COUNT) AS VALUE_COUNT" + "\n");
                strSqlString.AppendFormat("             , MIN(TO_NUMBER(MIN_VALUE)) AS MIN_VALUE" + "\n");
                strSqlString.AppendFormat("             , MAX(TO_NUMBER(MAX_VALUE)) AS MAX_VALUE" + "\n");
                strSqlString.AppendFormat("             , AVG(AVERAGE) AS AVERAGE" + "\n");
                strSqlString.AppendFormat("             , STDDEV(AVERAGE) AS STDDEV_AVG" + "\n");
                strSqlString.AppendFormat("             , AVG(STDDEV) AS STDDEV_ORI" + "\n");

                // 해당 테이블에 측정값들이 존재하지 않아 표준편차의 평균으로 산출하다 보니 Gap 발생.
                // 그래서 X_Bar(평균) 값으로 측정 값 대신하여 표준편차 산출한다. 단 0 이면 해당 테이블의 표준편차 평균으로 산출 한다.
                strSqlString.AppendFormat("             , DECODE(STDDEV(AVERAGE), 0, AVG(STDDEV), STDDEV(AVERAGE)) AS STDDEV" + "\n");
                strSqlString.AppendFormat("             , AVG(RANGE) AS RANGE" + "\n");
                strSqlString.AppendFormat("             , SUM(DECODE(USL, ' ', 0, 1)) AS USL_CNT" + "\n");
                strSqlString.AppendFormat("             , SUM(DECODE(LSL, ' ', 0, 1)) AS LSL_CNT" + "\n");
                strSqlString.AppendFormat("             , CASE WHEN SUM(DECODE(USL, ' ', 0, 1)) = 0 THEN 0" + "\n");
                strSqlString.AppendFormat("                    ELSE SUM(DECODE(USL, ' ', 0, USL)) / SUM(DECODE(USL, ' ', 0, 1))" + "\n");
                strSqlString.AppendFormat("               END USL" + "\n");
                strSqlString.AppendFormat("             , CASE WHEN SUM(DECODE(LSL, ' ', 0, 1)) = 0 THEN 0" + "\n");
                strSqlString.AppendFormat("                    ELSE SUM(DECODE(LSL, ' ', 0, LSL)) / SUM(DECODE(LSL, ' ', 0, 1))" + "\n");
                strSqlString.AppendFormat("               END LSL " + "\n");
                strSqlString.AppendFormat("             , MAX((SELECT DATA_2 FROM MGCMTBLDAT WHERE FACTORY = '" + cdvFactory.Text + "' AND TABLE_NAME = 'H_SPC_CHAR_D2' AND KEY_1 = B.CHAR_ID)) AS D2" + "\n");
                strSqlString.AppendFormat("          FROM (       " + "\n");
                strSqlString.AppendFormat("                SELECT A.*" + "\n");
                strSqlString.AppendFormat(strCheckd + "\n");
                strSqlString.AppendFormat("                  FROM MESMGR.MSPCCALDAT@RPTTOMES A " + "\n");
                strSqlString.AppendFormat("                     , MWIPCALDEF B" + "\n");
                strSqlString.AppendFormat("                 WHERE 1=1         " + "\n");
                strSqlString.AppendFormat("                   AND TO_CHAR(TO_DATE(TRAN_TIME, 'YYYYMMDDHH24MISS') + 2/24, 'YYYYMMDD') = B.SYS_DATE" + "\n");
                strSqlString.AppendFormat("                   AND B.CALENDAR_ID = 'HM'" + "\n");
                strSqlString.AppendFormat("                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.AppendFormat("                   AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                strSqlString.AppendFormat("                   AND RES_ID " + cdvRasId.SelectedValueToQueryString + "\n");
                strSqlString.AppendFormat("                   AND VALUE_COUNT >= 2" + "\n");

                //2012-06-08-임종우 : 삭제 된 LOT 제외 시킴 (이민희 요청)
                strSqlString.AppendFormat("                   AND EXCLUDE_FLAG = ' ' " + "\n");

                strSqlString.AppendFormat("               ) A" + "\n");
                strSqlString.AppendFormat("             , MESMGR.MSPCCHTDEF@RPTTOMES B" + "\n");
                strSqlString.AppendFormat("         WHERE 1=1" + "\n");
                strSqlString.AppendFormat("           AND A.FACTORY = B.FACTORY" + "\n");
                strSqlString.AppendFormat("           AND A.CHART_ID = B.CHART_ID" + "\n");
                strSqlString.AppendFormat("           AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.AppendFormat("           AND B.LOT_RES_FLAG = 'L'" + "\n");
                strSqlString.AppendFormat("           AND B.DELETE_FLAG = ' '" + "\n");
                strSqlString.AppendFormat("           AND B.SYNC_EDC_FLAG = 'Y'" + "\n");
            }
            
            #region 상세 조회에 따른 SQL문 생성

            if (cdvFactory.Text.Trim() != "HMKB1")
            {
                if (cdvCharID.Text != "ALL" && cdvCharID.Text != "")
                    strSqlString.AppendFormat("           AND B.CHAR_ID " + cdvCharID.SelectedValueToQueryString + "\n");

                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("           AND B.CHT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("           AND B.CHT_GRP_2 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("           AND B.CHT_GRP_3 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("           AND B.CHT_GRP_4 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("           AND B.CHT_GRP_5 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("           AND B.CHT_GRP_6 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                strSqlString.AppendFormat("         GROUP BY {0}" + "\n", QueryCond2);
                strSqlString.AppendFormat("       )" + "\n");
                strSqlString.AppendFormat(" ORDER BY {0}" + "\n", QueryCond1);
            }

            #endregion

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion


        #region " Button Event "
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private void btnView_Click(object sender, EventArgs e)
        {
            if (CheckField() == false) return;
            LoadingPopUp.LoadIngPopUpShow(this);
            dt = null;

            try
            {
                this.Refresh();
                //LoadingPopUp.LoadIngPopUpShow(this);

                GridColumnInit();

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                spdData.DataSource = dt;

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
            spdData.ExportExcel();
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            if (cdvFactory.txtValue.Equals("HMKB1"))
            {
                BaseFormType = eBaseFormType.BUMP_BASE;
                pnlBUMPDetail.Visible = false;

                cdvCharID.Visible = false;
                cdvRasId.Visible = false;

                cdvOperID.Visible = true;
                cdvSpecID.Visible = true;                
            }
            else
            {
                BaseFormType = eBaseFormType.WIP_BASE;
                pnlWIPDetail.Visible = false;

                cdvCharID.Visible = true;
                cdvRasId.Visible = true;

                cdvOperID.Visible = false;
                cdvSpecID.Visible = false;                
            }

            this.SetFactory(cdvFactory.txtValue);
            cdvRasId.sFactory = cdvFactory.txtValue;
            cdvOperID.sFactory = cdvFactory.txtValue;

            //GridColumnInit();
            SortInit();
        }

        private void cdvCharID_ValueButtonPress(object sender, EventArgs e)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT DISTINCT CHAR_ID Code, '' Data " + "\n");
            strSqlString.Append("  FROM MSPCCHTDEF " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("   AND LOT_RES_FLAG = 'L' " + "\n");
            strSqlString.Append("   AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("   AND SYNC_EDC_FLAG = 'Y' " + "\n");
            strSqlString.Append("   AND CHAR_ID <> ' ' " + "\n");
            strSqlString.Append(" ORDER BY CHAR_ID " + "\n");

            cdvCharID.sDynamicQuery = strSqlString.ToString();
        }

        private void cdvSpecID_ValueButtonPress(object sender, EventArgs e)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT DISTINCT C.SPEC_ID AS Code, '' Data " + "\n");
            strSqlString.Append("  FROM TQS_EDCPLAN_CONFIG@RPTTOFA A " + "\n");
            strSqlString.Append("  , TQS_SPEC_CONFIG@RPTTOFA B " + "\n");
            strSqlString.Append("  , TQS_SPEC_DEFINE@RPTTOFA C  " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append(" AND A.PLAN_SEQ = B.PLAN_SEQ  " + "\n");
            strSqlString.Append(" AND B.SPEC_PARENT_SEQ = C.PARENT_SEQ " + "\n");
            strSqlString.Append(" AND A.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append(" ORDER BY SPEC_ID " + "\n");
                       
            cdvSpecID.sDynamicQuery = strSqlString.ToString();
        }

        #endregion

        private void PQC030202_Load(object sender, EventArgs e)
        {
            pnlWIPDetail.Visible = true;
        }

    }
}
