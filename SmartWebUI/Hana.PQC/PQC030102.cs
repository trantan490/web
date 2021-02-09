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
    /// <summary>
    /// 클  래  스: PQC030102<br/>
    /// 클래스요약: 고객불만<br/>
    /// 작  성  자: 미라콤 김민규<br/>
    /// 최초작성일: 2008-01-19<br/>
    /// 상세  설명: 고객불만<br/>
    /// 변경  내용: [2009-08-26] 장은희 <br/>
    ///        
    /// 
    /// </summary>
    /// 

    public partial class PQC030102 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        // Defect_code 가져오기
        //private DataTable dtDc = null;
        private StringBuilder strSqlString1 = new StringBuilder();

        public PQC030102()
        {
            InitializeComponent();
            SortInit();
            GridColumnInit();
            cdvFromTo.AutoBinding();
            udcChartFX1.RPT_1_ChartInit();  //차트 초기화. 

            // Factory = "HMAK1" 으로 기본 설정
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.cdvCp.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvLoss.sFactory = GlobalVariable.gsAssyDefaultFactory;

            cboGraph.SelectedIndex = 0;
        }

        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            //if(udcRASCondition6.Text.TrimEnd() == "ALL")
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD028", GlobalVariable.gcLanguage));
            //    return false;
            //}
            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        /// 
        
        private void GridColumnInit()
        {
            #region 2. Header 생성

         //   spdData.ActiveSheet.RowHeader.ColumnCount = 0;
            spdData.RPT_ColumnInit();


            spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("FAMILY", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("PACKAGE", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("TYPE1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("TYPE2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("LD COUNT", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("DENSITY", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("GENERATION", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PIN TYPE", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 130);

            spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 8, 2);


            spdData.RPT_AddBasicColumn("Customer Complaints status", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);

            if (cboGraph.SelectedIndex == -1 || cboGraph.SelectedIndex == 0 || cboGraph.SelectedIndex == 5)
            {
                spdData.RPT_AddBasicColumn("Issue Number", 1, 9, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Date of occurrence", 1, 10, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type of complaint", 1, 11, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Defective Content", 1, 12, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Customer", 1, 13, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Defect name", 1, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot No", 1, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot Qty", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Defect quantity", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_MerageHeaderColumnSpan(0, 9, 9);
            }

            else if(cboGraph.SelectedIndex == 1)
            {
                spdData.RPT_AddBasicColumn("Customer", 1, 9, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Issue Number", 1, 10, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Date of occurrence", 1, 11, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type of complaint", 1, 12, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Defective Content", 1, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Defect name", 1, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot No", 1, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot Qty", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Defect quantity", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_MerageHeaderColumnSpan(0, 9, 9);
            }
            else if (cboGraph.SelectedIndex == 2 || cboGraph.SelectedIndex == 4)
            {
                spdData.RPT_AddBasicColumn("Defect name", 1, 9, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Issue Number", 1, 10, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Date of occurrence", 1, 11, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type of complaint", 1, 12, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Defective Content", 1, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Customer", 1, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot No", 1, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot Qty", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Defect quantity", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_MerageHeaderColumnSpan(0, 9, 9);
            }
            else if (cboGraph.SelectedIndex == 3)
            {
                spdData.RPT_AddBasicColumn("Type of complaint", 1, 9, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Issue Number", 1, 10, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Date of occurrence", 1, 11, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Defective Content", 1, 12, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Customer", 1, 13, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Defect name", 1, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot No", 1, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot Qty", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Defect quantity", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_MerageHeaderColumnSpan(0, 9, 9);
            }

            spdData.RPT_AddBasicColumn("PKG Type", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LEAD수", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("PKG", 1, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Density", 1, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_MerageHeaderColumnSpan(0, 18, 3);

            spdData.RPT_AddBasicColumn("Lot quantity", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Defective rate" + "\n" + "(ppm)", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("matters of progress ", 0, 23, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("CAR" + "\n" + "송부일자", 0, 24, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("CAR" + "\n" + " TAT", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("The details", 0, 26, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);


            spdData.RPT_MerageHeaderRowSpan(0, 21, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 22, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 23, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 24, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 25, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 26, 2);

            
            // Group항목이 있을경우 반드시 선언해줄것.
            spdData.RPT_ColumnConfigFromTable(btnSort);
            #endregion
        }
       
        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "B.MAT_GRP_1", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "B.MAT_GRP_2", "B.MAT_GRP_2 AS FAMILY", "MIN(FAB_SEQ) AS FAMILY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "B.MAT_GRP_3", "B.MAT_GRP_3 AS PACKAGE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "B.MAT_GRP_4", "B.MAT_GRP_4 AS TYPE1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "B.MAT_GRP_5", "B.MAT_GRP_5 AS TYPE2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "B.MAT_GRP_6", "B.MAT_GRP_6 AS \"LD COUNT\"", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "B.MAT_GRP_7", "B.MAT_GRP_7 AS DENSITY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "B.MAT_GRP_8", "B.MAT_GRP_8 AS GENERATION", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "B.MAT_CMF_10", "B.MAT_CMF_10 AS PIN_TYPE", false);

     
        }


        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        /// 

        //전체 대비하여 불량명을 보기 원함으로 필요없어짐.
        #region MakeSqlDc : 불량명
        //private string MakeSqlDc()
        //{
        //    StringBuilder strSqlString1 = new StringBuilder();
        //    string[] selectDate1 = new string[cdvFromTo.SubtractBetweenFromToDate + 1];
        //    string strFromDate = cdvFromTo.ExactFromDate;
        //    string strToDate = cdvFromTo.ExactToDate;
        //    string strDate = string.Empty;

        //    string QueryCond1 = null;

        //    udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
        //    QueryCond1 = tableForm.SelectedValueToQueryContainNull;

        //    int Between = cdvFromTo.SubtractBetweenFromToDate + 1;

        //    selectDate1 = cdvFromTo.getSelectDate();

        //    switch (cdvFromTo.DaySelector.SelectedValue.ToString())
        //    {
        //        case "DAY":
        //            strDate = "WORK_DATE";
        //            break;
        //        case "WEEK":
        //            strDate = "WORK_WEEK";
        //            break;
        //        case "MONTH":
        //            strDate = "WORK_MONTH";
        //            break;
        //        default:
        //            strDate = "WORK_DATE";
        //            break;
        //    }


        //    strSqlString1.Append("        SELECT DISTINCT  NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'DEFECT_CODE' AND KEY_1 = DEFECT_CODE AND ROWNUM=1), ' ') AS DEFECT_CODE " + "\n");
        //    strSqlString1.Append("          FROM CIQCABRLOT A " + "\n");
        //    strSqlString1.Append("             , MWIPMATDEF B  " + "\n");
        //    strSqlString1.Append("         WHERE 1 = 1  " + "\n");
        //    strSqlString1.AppendFormat("              AND A.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
        //    strSqlString1.Append("              AND A.FACTORY = B.FACTORY(+) " + "\n");
        //    strSqlString1.Append("              AND A.MAT_ID = B.MAT_ID(+) " + "\n");
        //    strSqlString1.Append("              AND A.MAT_VER(+) = 0 " + "\n");
        //    strSqlString1.Append("              AND B.MAT_VER(+) = 1 " + "\n");
        //    strSqlString1.Append("              AND SUBSTR(A.ABR_NO, 0,3) = 'CDN' " + "\n");
        //    strSqlString1.Append("              AND A.STEP1_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
        //    strSqlString1.Append("              AND A.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
        //    strSqlString1.Append("              AND A.RESV_FIELD_6 " + cdvCp.SelectedValueToQueryString + "\n");
        //    strSqlString1.Append("              AND A.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");

        //    #region 상세 조회에 따른 SQL문 생성
        //    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
        //        strSqlString1.AppendFormat("   AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

        //    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
        //        strSqlString1.AppendFormat("   AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

        //    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
        //        strSqlString1.AppendFormat("   AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

        //    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
        //        strSqlString1.AppendFormat("   AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

        //    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
        //        strSqlString1.AppendFormat("   AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

        //    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
        //        strSqlString1.AppendFormat("   AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

        //    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
        //        strSqlString1.AppendFormat("   AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

        //    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
        //        strSqlString1.AppendFormat("   AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
        //    #endregion

        //    return strSqlString1.ToString();
        //}
       #endregion   

        #region Chart 생성을 위한 쿼리
        private string MakeSqlString(int Step)
        {
            StringBuilder strSqlString = new StringBuilder();
          
            string[] selectDate1 = new string[cdvFromTo.SubtractBetweenFromToDate + 1];
            string strFromDate = cdvFromTo.ExactFromDate;
            string strToDate = cdvFromTo.ExactToDate;
            string strDate = string.Empty;

            string QueryCond1 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;

            int Between = cdvFromTo.SubtractBetweenFromToDate + 1;

            selectDate1 = cdvFromTo.getSelectDate();

            switch (cdvFromTo.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    strDate = "WORK_DATE";
                    break;
                case "WEEK":
                    strDate = "WORK_WEEK";
                    break;
                case "MONTH":
                    strDate = "WORK_MONTH";
                    break;
                default:
                    strDate = "WORK_DATE";
                    break;
            }

            switch (Step)
            {
                case 0:
                    {
                        #region Spread용 쿼리

                        strSqlString.Append("     SELECT  " + QueryCond1 + " \n");

                        if (cboGraph.SelectedIndex == -1 || cboGraph.SelectedIndex == 0 || cboGraph.SelectedIndex == 5 )
                        {
                            strSqlString.Append("             , ABR_NO , TO_CHAR(TO_DATE(A.STEP1_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') AS START_DAY " + "\n");
                            strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_CLAIM' AND KEY_1 = RESV_FIELD_6 AND ROWNUM=1), '-') AS KIND " + "\n");
                            strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_CLAIM_RESULT' AND KEY_1 = RESV_FIELD_7 AND ROWNUM=1), '-') AS RESULT " + "\n");
                            strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = CUS_ID AND ROWNUM=1), '-') AS CUSTOMER " + "\n");
                            strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'DEFECT_CODE' AND KEY_1 = DEFECT_CODE AND ROWNUM=1), ' ') AS DEFECT_CODE " + "\n");
                        }
                        else if (cboGraph.SelectedIndex == 1)
                        {
                            strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = CUS_ID AND ROWNUM=1), '-') AS CUSTOMER " + "\n");
                            strSqlString.Append("             , A.ABR_NO , TO_CHAR(TO_DATE(A.STEP1_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') AS START_DAY " + "\n");
                            strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_CLAIM' AND KEY_1 = RESV_FIELD_6 AND ROWNUM=1), '-') AS KIND " + "\n");
                            strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_CLAIM_RESULT' AND KEY_1 = RESV_FIELD_7 AND ROWNUM=1), '-') AS RESULT " + "\n");
                            strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'DEFECT_CODE' AND KEY_1 = DEFECT_CODE AND ROWNUM=1), ' ') AS DEFECT_CODE " + "\n");
                        }
                        else if (cboGraph.SelectedIndex == 2 || cboGraph.SelectedIndex == 4)
                        {
                            strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'DEFECT_CODE' AND KEY_1 = DEFECT_CODE AND ROWNUM=1), ' ') AS DEFECT_CODE " + "\n");
                            strSqlString.Append("             , A.ABR_NO , TO_CHAR(TO_DATE(A.STEP1_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') AS START_DAY " + "\n");
                            strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_CLAIM' AND KEY_1 = RESV_FIELD_6 AND ROWNUM=1), '-') AS KIND " + "\n");
                            strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_CLAIM_RESULT' AND KEY_1 = RESV_FIELD_7 AND ROWNUM=1), '-') AS RESULT " + "\n");
                            strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = CUS_ID AND ROWNUM=1), '-') AS CUSTOMER " + "\n");
                         }
                        else if (cboGraph.SelectedIndex == 3)
                         {
                             strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_CLAIM' AND KEY_1 = RESV_FIELD_6 AND ROWNUM=1), '-') AS KIND " + "\n");
                            strSqlString.Append("             , A.ABR_NO , TO_CHAR(TO_DATE(A.STEP1_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') AS START_DAY " + "\n");
                            strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'ABRLOT_CLAIM_RESULT' AND KEY_1 = RESV_FIELD_7 AND ROWNUM=1), '-') AS RESULT " + "\n");
                            strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = CUS_ID AND ROWNUM=1), '-') AS CUSTOMER " + "\n");
                            strSqlString.Append("             , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'DEFECT_CODE' AND KEY_1 = DEFECT_CODE AND ROWNUM=1), ' ') AS DEFECT_CODE " + "\n");
                        }
                    
                    
                        strSqlString.Append("             , A.LOT_ID " + "\n");
                        strSqlString.Append("             , A.QTY_1 " + "\n");
                        strSqlString.Append("             , A.FAIL_QTY  " + "\n");
                        strSqlString.Append("             , B.MAT_GRP_6, B.MAT_GRP_3, B.MAT_GRP_7 " + "\n");
                        strSqlString.Append("             , A.INSP_QTY " + "\n");
                        strSqlString.Append("             , CASE WHEN QTY_1 > 0   " + "\n");
                        strSqlString.Append("                    THEN ROUND((FAIL_QTY/QTY_1)*1000000 , 1) " + "\n");
                        strSqlString.Append("                    ELSE 0 " + "\n");
                        strSqlString.Append("               END AS YIELD " + "\n");
                        strSqlString.Append("             , DECODE(A.CUR_STEP,8,'완료',A.CUR_STEP||'Step') CUR_STEP " + "\n");
                        strSqlString.Append("             , DECODE(RESV_FIELD_8, ' ', ' ', TO_CHAR(TO_DATE(RESV_FIELD_8, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD')) AS S_DAY " + "\n");
                        strSqlString.Append("             , CASE WHEN RESV_FIELD_8 <> ' '   " + "\n");
                        strSqlString.Append("                               THEN  TRUNC(TO_DATE(RESV_FIELD_8, 'YYYYMMDDHH24MISS') - (TO_DATE(STEP1_TIME, 'YYYYMMDDHH24MISS')),2) " + "\n");
                        strSqlString.Append("                        WHEN RESV_FIELD_8  =  ' ' " + "\n");
                        strSqlString.Append("                               THEN TRUNC(TO_DATE(TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS'),'YYYYMMDDHH24MISS') - (TO_DATE(STEP1_TIME, 'YYYYMMDDHH24MISS')),2) " + "\n");
                        strSqlString.Append("               END AS CAR_TAT                " + "\n");
                        strSqlString.Append("             , STEP1_CMT AS DETAIL  " + "\n");
                        strSqlString.Append("          FROM CIQCABRLOT A " + "\n");
                        strSqlString.Append("             , MWIPMATDEF B " + "\n");
                        strSqlString.Append("         WHERE 1 = 1 " + "\n");
                        strSqlString.AppendFormat("           AND A.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("           AND A.FACTORY = B.FACTORY(+) " + "\n");
                        strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+) " + "\n");
                        strSqlString.Append("           AND A.MAT_VER(+) = 0 " + "\n");
                        strSqlString.Append("           AND B.MAT_VER(+) = 1 " + "\n");
                        strSqlString.Append("           AND SUBSTR(A.ABR_NO, 0,3) = 'CDN' " + "\n");
                        strSqlString.Append("           AND A.STEP1_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("           AND A.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
                        strSqlString.Append("           AND A.RESV_FIELD_6 " + cdvCp.SelectedValueToQueryString + "\n");
                        strSqlString.Append("           AND A.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                        if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("ORDER BY   " + QueryCond1 + " \n");
                        strSqlString.Append("           , A.ABR_NO, A.STEP1_TIME, A.RESV_FIELD_6, A.RESV_FIELD_7, A.CUS_ID, A.DEFECT_CODE,A.LOT_ID     " + "\n");

                        System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
                        #endregion
                    }
                    break;

                case 1:
                    {
                        #region Chart No.1

                        strSqlString.Append(" SELECT CUS_ID, (STEP_1+STEP_2+STEP_3+STEP_4+STEP_5+STEP_6+STEP_7) AS  진행중, STEP_8 AS 완료  " + "\n");
                        strSqlString.Append("  FROM ( " + "\n");
                        strSqlString.Append("      SELECT NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = CUS_ID AND ROWNUM=1), '-') AS CUS_ID " + "\n");
                        strSqlString.Append("              , SUM(DECODE(CUR_STEP, 8, 1, 0)) AS STEP_8 " + "\n");
                        strSqlString.Append("              , SUM(DECODE(CUR_STEP, 7, 1, 0)) AS STEP_7 " + "\n");
                        strSqlString.Append("              , SUM(DECODE(CUR_STEP, 6, 1, 0)) AS STEP_6 " + "\n");
                        strSqlString.Append("              , SUM(DECODE(CUR_STEP, 5, 1, 0)) AS STEP_5 " + "\n");
                        strSqlString.Append("              , SUM(DECODE(CUR_STEP, 4, 1, 0)) AS STEP_4 " + "\n");
                        strSqlString.Append("              , SUM(DECODE(CUR_STEP, 3, 1, 0)) AS STEP_3 " + "\n");
                        strSqlString.Append("              , SUM(DECODE(CUR_STEP, 2, 1, 0)) AS STEP_2 " + "\n");
                        strSqlString.Append("              , SUM(DECODE(CUR_STEP, 1, 1, 0)) AS STEP_1 " + "\n");
                        strSqlString.Append("        FROM CIQCABRLOT A " + "\n");
                        strSqlString.Append("                , MWIPMATDEF B " + "\n");
                        strSqlString.Append("       WHERE 1 = 1 " + "\n");
                        strSqlString.AppendFormat("              AND A.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("              AND A.FACTORY = B.FACTORY(+) " + "\n");
                        strSqlString.Append("              AND A.MAT_ID = B.MAT_ID(+) " + "\n");
                        strSqlString.Append("              AND A.MAT_VER(+) = 0 " + "\n");
                        strSqlString.Append("              AND B.MAT_VER(+) = 1 " + "\n");
                        strSqlString.Append("              AND SUBSTR(A.ABR_NO, 0,3) = 'CDN' " + "\n");
                        strSqlString.Append("              AND A.STEP1_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("              AND A.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
                        strSqlString.Append("              AND A.RESV_FIELD_6 " + cdvCp.SelectedValueToQueryString + "\n");
                        strSqlString.Append("              AND A.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                        if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("       GROUP BY CUS_ID  ) " + "\n");         
                        #endregion
                    }
                    break;

                case 2:
                    {
                        #region Chart No.2

                        strSqlString.Append(" SELECT CUSTOMER , ROUND(SUM(CAR_TAT)/COUNT(CUSTOMER),2) CAR_TAT " + "\n");
                        strSqlString.Append("    FROM (                " + "\n");
                        strSqlString.Append("        SELECT NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = CUS_ID AND ROWNUM=1), '-') AS CUSTOMER    " + "\n");
                        strSqlString.Append("             , CASE WHEN RESV_FIELD_8 <> ' '     " + "\n");
                        strSqlString.Append("                           THEN  TRUNC(TO_DATE(RESV_FIELD_8, 'YYYYMMDDHH24MISS') - (TO_DATE(STEP1_TIME, 'YYYYMMDDHH24MISS')),2)  " + "\n");
                        strSqlString.Append("                        WHEN RESV_FIELD_8  =  ' '  " + "\n");
                        strSqlString.Append("                            THEN TRUNC(TO_DATE(TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS'),'YYYYMMDDHH24MISS') - (TO_DATE(STEP1_TIME, 'YYYYMMDDHH24MISS')),2) " + "\n");
                        strSqlString.Append("                      END AS CAR_TAT   " + "\n");
                        strSqlString.Append("          FROM CIQCABRLOT A  " + "\n");
                        strSqlString.Append("             , MWIPMATDEF B  " + "\n");
                        strSqlString.Append("         WHERE 1 = 1  " + "\n");
                        strSqlString.AppendFormat("           AND A.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("           AND A.FACTORY = B.FACTORY(+) " + "\n");
                        strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+) " + "\n");
                        strSqlString.Append("           AND A.MAT_VER(+) = 0 " + "\n");
                        strSqlString.Append("           AND B.MAT_VER(+) = 1 " + "\n");
                        strSqlString.Append("           AND SUBSTR(A.ABR_NO, 0,3) = 'CDN' " + "\n");                        
                        strSqlString.Append("           AND A.STEP1_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("           AND A.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
                        strSqlString.Append("           AND A.RESV_FIELD_6 " + cdvCp.SelectedValueToQueryString + "\n");
                        strSqlString.Append("           AND A.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                        if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("       ) GROUP BY CUSTOMER " + "\n");
                        #endregion
                    }
                    break;

                case 3:
                    {
                        #region Chart No.3
                        
                        //strSqlString.Append("    SELECT   " + "\n");
                        //if (dtDc != null)
                        //{
                        //    for (int i = 0; i < dtDc.Rows.Count; i++)
                        //    {
                        //        if (dtDc.Rows.Count - i == 1)
                        //        {
                        //            strSqlString.Append("          ROUND((SUM(DECODE(DEFECT_CODE , '" + dtDc.Rows[i]["DEFECT_CODE"].ToString() + "' ,1,0))/ MAX(CNT)*100),2)   AS \"" + dtDc.Rows[i]["DEFECT_CODE"].ToString() + "\"" + '\n');
                        //        }
                        //        else
                        //        {
                        //            strSqlString.Append("          ROUND((SUM(DECODE(DEFECT_CODE , '" + dtDc.Rows[i]["DEFECT_CODE"].ToString() + "' ,1,0))/ MAX(CNT)*100),2)   AS \"" + dtDc.Rows[i]["DEFECT_CODE"].ToString() + "\"" + ", \n");
                        //        }
                        //    }
                        //}

                        //strSqlString.Append("      FROM ( " + "\n");
                        //strSqlString.Append("          SELECT  NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'DEFECT_CODE' AND KEY_1 = DEFECT_CODE AND ROWNUM=1), '-') AS DEFECT_CODE, ROWNUM AS CNT  " + "\n");
                        strSqlString.Append("          SELECT  NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'DEFECT_CODE' AND KEY_1 = DEFECT_CODE AND ROWNUM=1), '-') AS DEFECT_CODE, COUNT(*) CNT  " + "\n");
                        strSqlString1.Append("          FROM CIQCABRLOT A " + "\n");
                        strSqlString.Append("              FROM CIQCABRLOT A              " + "\n");
                        strSqlString.Append("                 , MWIPMATDEF B  " + "\n");
                        strSqlString.Append("             WHERE 1 = 1  " + "\n");

                        strSqlString.AppendFormat("              AND A.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("              AND A.FACTORY = B.FACTORY(+) " + "\n");
                        strSqlString.Append("              AND A.MAT_ID = B.MAT_ID(+) " + "\n");
                        strSqlString.Append("              AND A.MAT_VER(+) = 0 " + "\n");
                        strSqlString.Append("              AND B.MAT_VER(+) = 1 " + "\n");
                        strSqlString.Append("              AND SUBSTR(A.ABR_NO, 0,3) = 'CDN' " + "\n");
                        strSqlString.Append("              AND A.STEP1_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("              AND A.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
                        strSqlString.Append("              AND A.RESV_FIELD_6 " + cdvCp.SelectedValueToQueryString + "\n");
                        strSqlString.Append("              AND A.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                        if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                        #endregion

                        //strSqlString.Append("           ) " + "\n");
                        strSqlString.Append("     GROUP BY DEFECT_CODE  " + "\n");
                        #endregion
                    }
                    break;

                case 4:
                    {

                        #region Chart No.4
                        strSqlString.Append("        SELECT NVL(B.MAT_GRP_3, '-') AS MAT_GRP_3 " + "\n");
                        strSqlString.Append("             , SUM(DECODE(A.FACTORY, A.FACTORY, 1, 0)) AS CNT " + "\n");
                        strSqlString.Append("          FROM CIQCABRLOT A  " + "\n");
                        strSqlString.Append("             , MWIPMATDEF B  " + "\n");
                        strSqlString.Append("        WHERE 1 = 1 " + "\n");
                        strSqlString.AppendFormat("              AND A.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("              AND A.FACTORY = B.FACTORY(+) " + "\n");
                        strSqlString.Append("              AND A.MAT_ID = B.MAT_ID(+) " + "\n");
                        strSqlString.Append("              AND A.MAT_VER(+) = 0 " + "\n");
                        strSqlString.Append("              AND B.MAT_VER(+) = 1 " + "\n");
                        strSqlString.Append("              AND SUBSTR(A.ABR_NO, 0,3) = 'CDN' " + "\n");
                        strSqlString.Append("              AND A.STEP1_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("              AND A.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
                        strSqlString.Append("              AND A.RESV_FIELD_6 " + cdvCp.SelectedValueToQueryString + "\n");
                        strSqlString.Append("              AND A.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                        if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                            strSqlString.AppendFormat("           AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("      GROUP BY B.MAT_GRP_3   " + "\n");
                        #endregion
                    }
                    break;

                case 5:
                    {
                        #region Chart No.5
                        strSqlString.Append("        SELECT NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'DEFECT_CODE' AND KEY_1 = DEFECT_CODE AND ROWNUM=1), ' ') AS DEFECT_CODE " + "\n");
                        strSqlString.Append("             , SUM(DECODE(A.FACTORY, A.FACTORY, 1, 0)) AS CNT " + "\n");
                        strSqlString.Append("          FROM CIQCABRLOT A  " + "\n");
                        strSqlString.Append("             , MWIPMATDEF B  " + "\n");
                        strSqlString.Append("         WHERE 1 = 1  " + "\n");
                        strSqlString.AppendFormat("              AND A.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("              AND A.FACTORY = B.FACTORY(+) " + "\n");
                        strSqlString.Append("              AND A.MAT_ID = B.MAT_ID(+) " + "\n");
                        strSqlString.Append("              AND A.MAT_VER(+) = 0 " + "\n");
                        strSqlString.Append("              AND B.MAT_VER(+) = 1 " + "\n");
                        strSqlString.Append("              AND SUBSTR(A.ABR_NO, 0,3) = 'CDN' " + "\n");

                        strSqlString.Append("              AND A.STEP1_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("              AND A.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
                        strSqlString.Append("              AND A.RESV_FIELD_6 " + cdvCp.SelectedValueToQueryString + "\n");
                        strSqlString.Append("              AND A.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("   AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("   AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("   AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("   AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("   AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("   AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("   AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                        if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                            strSqlString.AppendFormat("   AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("        GROUP BY DEFECT_CODE   " + "\n");
                        #endregion
                    }
                    break;

                case 6:
                    {
                        #region Chart No.6
                        strSqlString.Append("     SELECT CUS_ID  " + "\n");

                        for (int i = 0; i < Between; i++)
                        {
                            strSqlString.AppendFormat("          , SUM(DECODE({0}, '{1}', 1, 0)) as \"{1}\"" + "\n", strDate, selectDate1[i].ToString());
                        }

                        strSqlString.Append("       FROM (  " + "\n");
                        strSqlString.Append("            SELECT NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = CUS_ID AND ROWNUM=1), '-') AS CUS_ID  " + "\n");
                        strSqlString.Append("                 , GET_WORK_DATE(STEP1_TIME, 'D')  AS WORK_DATE " + "\n");
                        strSqlString.Append("                 , GET_WORK_DATE(STEP1_TIME, 'W')  AS WORK_WEEK " + "\n");
                        strSqlString.Append("                 , GET_WORK_DATE(STEP1_TIME, 'M')  AS WORK_MONTH " + "\n");
                        strSqlString.Append("          FROM CIQCABRLOT A  " + "\n");
                        strSqlString.Append("             , MWIPMATDEF B  " + "\n");
                        strSqlString.Append("         WHERE 1 = 1  " + "\n");
                        strSqlString.AppendFormat("           AND A.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("           AND A.FACTORY = B.FACTORY(+) " + "\n");
                        strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+) " + "\n");
                        strSqlString.Append("           AND A.MAT_VER(+) = 0 " + "\n");
                        strSqlString.Append("           AND B.MAT_VER(+) = 1 " + "\n");
                        strSqlString.Append("              AND SUBSTR(A.ABR_NO, 0,3) = 'CDN' " + "\n");

                        strSqlString.Append("           AND A.STEP1_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("           AND A.CUR_STEP " + cdvStep.SelectedValueToQueryString + "\n");
                        strSqlString.Append("           AND A.RESV_FIELD_6 " + cdvCp.SelectedValueToQueryString + "\n");
                        strSqlString.Append("           AND A.DEFECT_CODE " + cdvLoss.SelectedValueToQueryString + "\n");

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("   AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("   AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("   AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("   AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("   AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("   AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("   AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                        if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                            strSqlString.AppendFormat("   AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("            )   " + "\n");
                        strSqlString.Append("   GROUP BY CUS_ID   " + "\n");

                        #endregion
                    }
                    break;

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
        private void ShowChart(int rowCount)
        {
            DataTable dt_graph = null;

            udcChartFX1.RPT_1_ChartInit();
            udcChartFX1.RPT_2_ClearData();

            // 그래프 설정 //                      
            udcChartFX1.AxisY.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisY2.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisX.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.PointLabels = true;
            udcChartFX1.AxisY.DataFormat.Decimals = 0;

            //// Label 위치
            //udcChartFX1.PointLabelAlign = SoftwareFX.ChartFX.LabelAlign.Bottom;


            //udcChartFX1.PointLabelColor 
            udcChartFX1.PointLabelColor = System.Drawing.Color.Black  ;

            // ToolBar 보여주기
            udcChartFX1.ToolBar = true;

            //3D 
            udcChartFX1.Chart3D = true;

            // contion attribute 를 이용한 0 point label hidden
            SoftwareFX.ChartFX.ConditionalAttributes contion = udcChartFX1.ConditionalAttributes[0];
            contion.Condition.From = 0;
            contion.Condition.To = 0;
            contion.PointLabels = false;
            

            udcChartFX1.MultipleColors = false;
            switch (cboGraph.SelectedIndex)
            {

                case 0:
                    {
                        #region Chart No.1
                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(1));
                        udcChartFX1.DataSource = dt_graph;

                        udcChartFX1.SerLegBox = true;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.1;
                        udcChartFX1.Stacked = SoftwareFX.ChartFX.Stacked.Normal;
                        udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Bottom;
                        
                        udcChartFX1.AxisX.Title.Text = "Customer";
                        udcChartFX1.AxisY.Title.Text = "number of occurrences";
                        #endregion
                    }
                    break;

                case 1:
                    {
                        #region Chart No.2
                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(2));
                        udcChartFX1.DataSource = dt_graph;

                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Lines ;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        udcChartFX1.LegendBox = false;
                        udcChartFX1.SerLegBox = false;
                        udcChartFX1.AxisY.DataFormat.Decimals = 2;
                        udcChartFX1.AxisX.Title.Text = "Customer";
                        udcChartFX1.AxisY.Title.Text = "CAR TAT";
                        #endregion
                    }
                    break;

                case 2:
                    {
                        #region Chart No.3
                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(3));
                      // dt_graph = GetRotatedDataTable(ref dt_graph);
                        udcChartFX1.DataSource = dt_graph;

                        udcChartFX1.AxisY.DataFormat.Decimals = 2;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Pie ;
                        udcChartFX1.SerLegBox = false;
                        udcChartFX1.LegendBox = false;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max;
                        #endregion
                    }
                    break;

                case 3:
                    {
                        #region Chart No.4
                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(4));
                        udcChartFX1.DataSource = dt_graph;

                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max ;
                        udcChartFX1.MultipleColors = true;
                        //udcChartFX1.PointLabelColor 
                        udcChartFX1.PointLabelColor = System.Drawing.Color.Red;
                        
                        udcChartFX1.LegendBox = false;
                        udcChartFX1.SerLegBox = false;
                        udcChartFX1.AxisX.Title.Text = "PKG";
                        udcChartFX1.AxisY.Title.Text = "number of occurrences";
                        #endregion
                    }
                    break;

                case 4:
                    {
                        #region Chart No.5
                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(5));
                        udcChartFX1.DataSource = dt_graph;

                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                        udcChartFX1.MultipleColors = true;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max ;
                        udcChartFX1.AxisX.Title.Text = "Defect name";
                        udcChartFX1.AxisY.Title.Text = "number of occurrences";
                        #endregion
                    }
                    break;

                case 5:
                    {
                        #region Chart No.6
                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(6));
                       dt_graph = GetRotatedDataTable(ref dt_graph);
                        udcChartFX1.DataSource = dt_graph;
                        udcChartFX1.Stacked = SoftwareFX.ChartFX.Stacked.Normal;
                        //udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Lines; 
                        //udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Pareto;
                        udcChartFX1.SerLegBox = true;
                        udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        udcChartFX1.AxisY.Title.Text = "number of occurrences";


                        #endregion
                    }
                    break;
            }
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

            //불량명 비율을 전체대비하여 보기 원함으로 불필요
            //if (cboGraph.SelectedIndex == 2)
            //{
            //    dtDc = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDc());
            //}

            DataTable dt = null;

      //      SortInit();
            GridColumnInit();

            try
            {
                this.Refresh();
                //LoadingPopUp.LoadIngPopUpShow(this);
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(0));

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    udcChartFX1.RPT_1_ChartInit();
                    udcChartFX1.RPT_2_ClearData();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                spdData.DataSource = dt;
                spdData.RPT_AutoFit(false);
                ShowChart(0);
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

        //private void btnExcelExport_Click(object sender, EventArgs e)
        //{
        //    ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ");
        //}

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            spdData.ExportExcel();
        }

        #endregion


        #region DataTable Pivot Function
        public DataTable GetRotatedDataTable(ref DataTable dt)
        {
            int nColToRow = 0;  // Column Position of dt => Legend Column of dtNew

            DataTable dtNew = new DataTable();
            Object[] dr = null;

            // Get Series Type
            Type type = dt.Columns[1].DataType;

            // Adding Columns
            dtNew.Columns.Add("GUBUN", typeof(String));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dtNew.Columns.Add(dt.Rows[i][0].ToString(), type);
            }

            // Filling Data
            for (int j = nColToRow + 1; j < dt.Columns.Count; j++)
            {
                dr = dtNew.NewRow().ItemArray;
                dr[0] = dt.Columns[j].Caption;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr[i + 1] = dt.Rows[i][j];
                }
                dtNew.Rows.Add(dr);
            }
            return dtNew;
        }
        #endregion

        // Spread 에 조회된 목록 중에서 원하는 건을 클릭하면 PopUp 창이 떠서 Step1~7까지 내용을 보여준다.
        private void spdData_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string strAbrNo = null;
            string strLotId = null;

          if (cboGraph.SelectedIndex == 1 || cboGraph.SelectedIndex == 2 || cboGraph.SelectedIndex == 4 || cboGraph.SelectedIndex == 3)
            {
                strAbrNo = spdData.ActiveSheet.Cells[e.Row, 10].Value.ToString();
                strLotId = spdData.ActiveSheet.Cells[e.Row, 15].Value.ToString();
            }

            else if (cboGraph.SelectedIndex == -1 || cboGraph.SelectedIndex == 0 || cboGraph.SelectedIndex == 5)
            {
                strAbrNo = spdData.ActiveSheet.Cells[e.Row, 9].Value.ToString();
                strLotId = spdData.ActiveSheet.Cells[e.Row, 15].Value.ToString();
            }



            //     System.Windows.Forms.Form frm = new PQC030102_P1(strAbrNo, strLotid);
            System.Windows.Forms.Form frm = new PQC030102_P1(strAbrNo, strLotId);

            frm.ShowDialog();

        }

    }
}