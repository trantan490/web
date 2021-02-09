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

namespace Hana.YLD
{
    public partial class BUM010201 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        decimal jindoPer;
        GlobalVariable.FindWeek FindWeek = new GlobalVariable.FindWeek();

        /// <summary>
        /// 클  래  스: BUM010201<br/>
        /// 클래스요약: BUMP 재공 & 실적 Report<br/>
        /// 작  성  자: 임종우<br/>
        /// 최초작성일: 2016-03-04<br/>
        /// 상세  설명: BUMP 재공 & 실적 Report(이병길K 요청)<br/>
        /// 변경  내용: <br/>   
        /// 2018-01-08-임종우 : SubTotal, GrandTotal 평균값 구하기 Function 변경
        /// </summary>
        public BUM010201()
        {
            InitializeComponent();                                   
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();

            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory("HMKB1");
            cdvFactory.Text = "HMKB1";            

            // BUMP 용 Detail 로 초기 셋팅
            BaseFormType = eBaseFormType.BUMP_BASE;
            pnlBUMPDetail.Visible = false;
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

            String ss = DateTime.Now.ToString("MM-dd");
            FindWeek = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");

            try
            {
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Layer", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Bumping Type", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Operation Flow", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);                
                spdData.RPT_AddBasicColumn("PKG Type", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("RDL Plating", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Final Bump", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Sub. Material", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Size", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Thickness", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Flat Type", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Wafer Orientation", 0, 11, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Product", 0, 12, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            
                spdData.RPT_AddBasicColumn("Monthly plan", 0, 13, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Monthly Plan Rev", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                
                
                spdData.RPT_AddBasicColumn("Production Status", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);                
                spdData.RPT_AddBasicColumn("actual", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Progress rate (%)", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 90);
                spdData.RPT_AddBasicColumn("Difference", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 15, 3);

                spdData.RPT_AddBasicColumn("a daily goal", 0, 18, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Daily Performance", 0, 19, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn(FindWeek.ThisWeek.Substring(0, 4) + "." + FindWeek.ThisWeek.Substring(4, 2) + " 주차", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Plan", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                
                spdData.RPT_AddBasicColumn("actual", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 90);
                spdData.RPT_AddBasicColumn("residual quantity", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 90);
                spdData.RPT_MerageHeaderColumnSpan(0, 20, 3);                
                                
                spdData.RPT_AddBasicColumn("HMK3B", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PACKING", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("OGI", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("AVI", 0, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("SORT", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("FINAL_INSP", 0, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                spdData.RPT_AddBasicColumn("BUMP", 0, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("REFLOW", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("BALL_MOUNT", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("ETCH", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("SNAG_PLAT", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("CU_PLAT", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PHOTO", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("SPUTTER", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_MerageHeaderColumnSpan(0, 29, 7);   //BUMP                

                spdData.RPT_AddBasicColumn("PSV3", 0, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PHOTO", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);                

                spdData.RPT_AddBasicColumn("RDL3", 0, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("ETCH", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PLAT", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PHOTO", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("SPUTTER", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);                
                spdData.RPT_MerageHeaderColumnSpan(0, 37, 4);   //RDL3

                spdData.RPT_AddBasicColumn("PSV2", 0, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PHOTO", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);                

                spdData.RPT_AddBasicColumn("RDL2", 0, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("ETCH", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PLAT", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PHOTO", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("SPUTTER", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_MerageHeaderColumnSpan(0, 42, 4);   //RDL2

                spdData.RPT_AddBasicColumn("PSV1", 0, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PHOTO", 1, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);                

                spdData.RPT_AddBasicColumn("RDL1", 0, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("ETCH", 1, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PLAT", 1, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PHOTO", 1, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("SPUTTER", 1, 50, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_MerageHeaderColumnSpan(0, 50, 4);   //RDL1

                spdData.RPT_AddBasicColumn("RCF", 0, 51, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PHOTO", 1, 51, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);                

                spdData.RPT_AddBasicColumn("I-STOCK", 0, 52, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("IQC", 0, 53, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("HMK2B", 0, 54, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);                
                spdData.RPT_AddBasicColumn("TOTAL", 0, 55, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

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
                spdData.RPT_MerageHeaderRowSpan(0, 18, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 19, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 23, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 24, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 25, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 26, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 27, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 28, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 52, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 53, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 54, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 55, 2);
                
               
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
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "(SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = 'HMKB1' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = CUSTOMER_ID) AS CUSTOMER", "MAT.CUSTOMER_ID", "CUSTOMER_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "MAT_GRP_03 AS LAYER", "MAT.MAT_GRP_03", "MAT_GRP_03", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("BUMPING TYPE", "MAT_GRP_01 AS BUMPING_TYPE", "MAT.MAT_GRP_01", "MAT_GRP_01", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PROCESS FLOW", "MAT_GRP_02 AS PROCESS_FLOW", "MAT.MAT_GRP_02", "MAT_GRP_02", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG TYPE", "MAT_GRP_04 AS PKG_TYPE", "MAT.MAT_GRP_04", "MAT_GRP_04", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL PLATING", "MAT_GRP_05 AS RDL_PLATING", "MAT.MAT_GRP_05", "MAT_GRP_05", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FINAL BUMP", "MAT_GRP_06 AS FINAL_BUMP", "MAT.MAT_GRP_06", "MAT_GRP_06", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SUB. MATERIAL", "MAT_GRP_07 AS SUB_MATERIAL", "MAT.MAT_GRP_07", "MAT_GRP_07", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SIZE", "MAT_CMF_01 AS WF_SIZE", "MAT.MAT_CMF_01", "MAT_CMF_01", true); // \"SIZE\"
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("THICKNESS", "MAT_CMF_02 AS THICKNESS", "MAT.MAT_CMF_02", "MAT_CMF_02", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FLAT TYPE", "MAT_CMF_03 AS FLAT_TYPE", "MAT.MAT_CMF_03", "MAT_CMF_03", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("WAFER ORIENTATION", "MAT_CMF_04 AS WAFER_ORIENTATION", "MAT.MAT_CMF_04", "MAT_CMF_04", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT_ID AS PRODUCT", "MAT.MAT_ID", "MAT_ID", false);       
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
            string date;
            string month;
            string year;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;            
            
            date = cdvDate.SelectedValue();

            DateTime Select_date;
            Select_date = DateTime.Parse(cdvDate.Text.ToString());

            year = Select_date.ToString("yyyy");
            month = Select_date.ToString("yyyyMM");
            start_date = month + "01";            
           
            strSqlString.Append("SELECT " + QueryCond1 + "\n");            
            strSqlString.Append("     , SUM(MON_PLN.PLN_MON) AS PLN_MON" + "\n");
            strSqlString.Append("     , SUM(MON_PLN.PLN_MON_REV) AS PLN_MON_REV" + "\n");
            strSqlString.Append("     , SUM(SHP.SHP_MONTH) AS SHP_MONTH" + "\n");
            strSqlString.Append("     , DECODE(SUM(NVL(MON_PLN.PLN_MON_REV,0)), 0, 0, ROUND((SUM(NVL(SHP.SHP_MONTH,0)) / SUM(NVL(MON_PLN.PLN_MON_REV,0)))*100, 1)) AS JINDO" + "\n");
            strSqlString.Append("     , SUM(NVL(SHP.SHP_MONTH,0)) - ROUND(((SUM(NVL(MON_PLN.PLN_MON_REV,0)) * " + jindoPer + ") / 100),1) AS DEF" + "\n");

            if (lblRemain.Text != "0 day")  // 잔여일이 0 일이 아닐때
            {
                strSqlString.Append("     , ROUND(((SUM(NVL(MON_PLN.PLN_MON_REV,0)) - SUM(NVL(SHP.SHP_MONTH,0))) /" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "), 1) AS TARGET_DAY " + "\n");
            }
            else  // 잔여일이 0 일 일대
            {
                strSqlString.Append("     , 0 AS TARGET_DAY " + "\n");
            }
                        
            strSqlString.Append("     , SUM(SHP.SHP_DAY) AS SHP_DAY" + "\n");
            strSqlString.Append("     , SUM(WEEK_PLN.WEEK_PLAN) AS WEEK_PLAN" + "\n");
            strSqlString.Append("     , SUM(SHP.SHP_WEEK) AS SHP_WEEK     " + "\n");
            strSqlString.Append("     , SUM(NVL(WEEK_PLN.WEEK_PLAN,0)) - SUM(NVL(SHP.SHP_WEEK,0)) AS WEEK_DEF" + "\n");
            strSqlString.Append("     , SUM(HMK3B) AS HMK3B, SUM(PACKING) AS PACKING, SUM(OGI) AS OGI, SUM(AVI) AS AVI, SUM(SORT) AS SORT, SUM(FINAL_INSP) AS FINAL_INSP" + "\n");
            strSqlString.Append("     , SUM(BUMP_REFLOW) AS BUMP_REFLOW, SUM(BUMP_BALL_MOUNT) AS BUMP_BALL_MOUNT, SUM(BUMP_ETCH) AS BUMP_ETCH, SUM(BUMP_SNAG_PLAT) AS BUMP_SNAG_PLAT" + "\n");
            strSqlString.Append("     , SUM(BUMP_CU_PLAT) AS BUMP_CU_PLAT, SUM(BUMP_PHOTO) AS BUMP_PHOTO, SUM(BUMP_SPUTTER) AS BUMP_SPUTTER" + "\n");
            strSqlString.Append("     , SUM(PSV3_PHOTO) AS PSV3_PHOTO, SUM(RDL3_ETCH) AS RDL3_ETCH, SUM(RDL3_PLAT) AS RDL3_PLAT, SUM(RDL3_PHOTO) AS RDL3_PHOTO, SUM(RDL3_SPUTTER) AS RDL3_SPUTTER" + "\n");
            strSqlString.Append("     , SUM(PSV2_PHOTO) AS PSV2_PHOTO, SUM(RDL2_ETCH) AS RDL2_ETCH, SUM(RDL2_PLAT) AS RDL2_PLAT, SUM(RDL2_PHOTO) AS RDL2_PHOTO, SUM(RDL2_SPUTTER) AS RDL2_SPUTTER" + "\n");
            strSqlString.Append("     , SUM(PSV1_PHOTO) AS PSV1_PHOTO, SUM(RDL1_ETCH) AS RDL1_ETCH, SUM(RDL1_PLAT) AS RDL1_PLAT, SUM(RDL1_PHOTO) AS RDL1_PHOTO, SUM(RDL1_SPUTTER) AS RDL1_SPUTTER" + "\n");
            strSqlString.Append("     , SUM(RCF_PHOTO) AS RCF_PHOTO, SUM(I_STOCK) AS I_STOCK, SUM(IQC) AS IQC, SUM(HMK2B) AS HMK2B, SUM(TTL_QTY) AS TTL_QTY" + "\n");
            strSqlString.Append("  FROM CWIPMATINF@RPTTOMES MAT" + "\n");
            strSqlString.Append("     , (    " + "\n");
            strSqlString.Append("        SELECT MAT_ID" + "\n");
            strSqlString.Append("             , SUM(TO_NUMBER(DECODE(RESV_FIELD7, ' ', 0, RESV_FIELD7))) AS PLN_MON " + "\n");
            strSqlString.Append("             , SUM(TO_NUMBER(DECODE(RESV_FIELD8, ' ', 0, RESV_FIELD8))) AS PLN_MON_REV" + "\n");
            strSqlString.Append("          FROM CWIPPLNMON " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("           AND PLAN_MONTH = '" + month + "'" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) MON_PLN" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID, SUM(WW_QTY) AS WEEK_PLAN " + "\n");
            strSqlString.Append("          FROM RWIPPLNWEK " + "\n");
            strSqlString.Append("         WHERE FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("           AND PLAN_WEEK = '" + FindWeek.ThisWeek + "'" + "\n");
            strSqlString.Append("           AND GUBUN = '3' " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) WEEK_PLN" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE = '" + date + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_DAY" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + date + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_WEEK" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "' THEN NVL(SHP_QTY_1, 0) END) AS SHP_MONTH" + "\n");
            strSqlString.Append("          FROM VSUMWIPOUT " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND WORK_DATE BETWEEN LEAST('" + FindWeek.StartDay_ThisWeek + "', '" + start_date + "') AND '" + date + "'" + "\n");
            strSqlString.Append("           AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("           AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND CM_KEY_2 = 'PROD' " + "\n");

            if (cdvType.Text != "ALL")
            {
                strSqlString.Append("           AND CM_KEY_3 LIKE '" + cdvType.Text + "' " + "\n");
            }
                        
            strSqlString.Append("         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("       ) SHP" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK3B', QTY_1, 0)) AS HMK3B" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PACKING', QTY_1, 0)) AS PACKING" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'OGI', QTY_1, 0)) AS OGI" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'AVI', QTY_1, 0)) AS AVI" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SORT', QTY_1, 0)) AS SORT" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'FINAL_INSP', QTY_1, 0)) AS FINAL_INSP" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BUMP_REFLOW', QTY_1, 0)) AS BUMP_REFLOW" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BUMP_BALL_MOUNT', QTY_1, 0)) AS BUMP_BALL_MOUNT" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BUMP_ETCH', QTY_1, 0)) AS BUMP_ETCH" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BUMP_SNAG_PLAT', QTY_1, 0)) AS BUMP_SNAG_PLAT" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BUMP_CU_PLAT', QTY_1, 0)) AS BUMP_CU_PLAT" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BUMP_PHOTO', QTY_1, 0)) AS BUMP_PHOTO" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BUMP_SPUTTER', QTY_1, 0)) AS BUMP_SPUTTER" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PSV3_PHOTO', QTY_1, 0)) AS PSV3_PHOTO" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL3_ETCH', QTY_1, 0)) AS RDL3_ETCH" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL3_PLAT', QTY_1, 0)) AS RDL3_PLAT             " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL3_PHOTO', QTY_1, 0)) AS RDL3_PHOTO             " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL3_SPUTTER', QTY_1, 0)) AS RDL3_SPUTTER" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PSV2_PHOTO', QTY_1, 0)) AS PSV2_PHOTO" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL2_ETCH', QTY_1, 0)) AS RDL2_ETCH" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL2_PLAT', QTY_1, 0)) AS RDL2_PLAT" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL2_PHOTO', QTY_1, 0)) AS RDL2_PHOTO" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL2_SPUTTER', QTY_1, 0)) AS RDL2_SPUTTER" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PSV1_PHOTO', QTY_1, 0)) AS PSV1_PHOTO" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL1_ETCH', QTY_1, 0)) AS RDL1_ETCH" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL1_PLAT', QTY_1, 0)) AS RDL1_PLAT" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL1_PHOTO', QTY_1, 0)) AS RDL1_PHOTO" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL1_SPUTTER', QTY_1, 0)) AS RDL1_SPUTTER" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RCF_PHOTO', QTY_1, 0)) AS RCF_PHOTO" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'I-STOCK', QTY_1, 0)) AS I_STOCK" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'IQC', QTY_1, 0)) AS IQC" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK2B', QTY_1, 0)) AS HMK2B" + "\n");
            strSqlString.Append("             , SUM(QTY_1) AS TTL_QTY" + "\n");
            strSqlString.Append("          FROM (        " + "\n");

            if (rdbWIP.Checked == true)
            {
                #region 1.재공기준
                strSqlString.Append("                SELECT A.MAT_ID, B.OPER, B.OPER_GRP_1, A.LOT_CMF_5 AS LOT_TYPE, A.QTY_1  " + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") != cdvDate.SelectedValue())
                {
                    strSqlString.Append("                  FROM RWIPLOTSTS_BOH A" + "\n");
                    strSqlString.Append("                     , MWIPOPRDEF B" + "\n");
                    strSqlString.Append("                 WHERE A.CUTOFF_DT = '" + cdvDate.SelectedValue() + "22' " + "\n");
                }
                else if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    strSqlString.Append("                  FROM RWIPLOTSTS A" + "\n");
                    strSqlString.Append("                     , MWIPOPRDEF B" + "\n");
                    strSqlString.Append("                 WHERE 1 = 1 " + "\n");
                }

                strSqlString.Append("                   AND A.FACTORY = B.FACTORY" + "\n");
                strSqlString.Append("                   AND A.OPER = B.OPER" + "\n");
                strSqlString.Append("                   AND A.FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                   AND A.LOT_TYPE = 'W'  " + "\n");
                strSqlString.Append("                   AND A.LOT_DEL_FLAG = ' '" + "\n");
                #endregion
            }
            else
            {
                #region 2.실적기준
                strSqlString.Append("                SELECT A.MAT_ID, A.OPER, B.OPER_GRP_1, A.CM_KEY_3 AS LOT_TYPE, (A.S1_END_QTY_1+A.S2_END_QTY_1+A.S3_END_QTY_1+A.S4_END_QTY_1) AS QTY_1  " + "\n");
                strSqlString.Append("                  FROM RSUMWIPMOV A" + "\n");
                strSqlString.Append("                     , MWIPOPRDEF B" + "\n");
                strSqlString.Append("                 WHERE 1 = 1 " + "\n");
                strSqlString.Append("                   AND A.FACTORY = B.FACTORY" + "\n");
                strSqlString.Append("                   AND A.OPER = B.OPER" + "\n");
                strSqlString.Append("                   AND A.WORK_DATE = '" + date + "' " + "\n");
                strSqlString.Append("                   AND A.FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("                   AND A.LOT_TYPE = 'W'  " + "\n");                
                #endregion
            }

            strSqlString.Append("               ) " + "\n");

            if (cdvType.Text != "ALL")
            {
                strSqlString.Append("         WHERE LOT_TYPE LIKE '" + cdvType.Text + "' " + "\n");
            }

            strSqlString.Append("         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("       ) WIP" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = MON_PLN.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = WEEK_PLN.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("   AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("   AND MAT.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("   AND NVL(MON_PLN.PLN_MON,0) + NVL(MON_PLN.PLN_MON_REV,0) + NVL(SHP.SHP_MONTH,0) + NVL(WEEK_PLN.WEEK_PLAN,0) + NVL(SHP.SHP_WEEK,0) + NVL(WIP.TTL_QTY,0) > 0" + "\n");
            
            // Product
            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("   AND MAT.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);
            }

            #region 상세 조회에 따른 SQL문 생성
            if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                strSqlString.AppendFormat("   AND MAT.CUSTOMER_ID {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

            if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_01 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

            if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_02 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

            if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_03 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

            if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_04 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

            if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_05 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

            if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_06 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

            if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_07 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

            if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_CMF_01 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

            if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_CMF_02 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

            if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_CMF_03 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

            if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_CMF_04 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);

            #endregion

            strSqlString.Append(" GROUP BY " + QueryCond2 + "\n");
            strSqlString.Append(" ORDER BY " + QueryCond2 + "\n");            
            
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
            
            return strSqlString.ToString();
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
                int nGroupCount = ((udcTableForm)(this.btnSort.BindingForm)).GetSelectedCount();

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 13, null, null, btnSort);                
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함
           
                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 13, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);

                spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 17, nGroupCount, false);

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
                StringBuilder Condition = new StringBuilder();
                Condition.AppendFormat("기준일자: {0}     today: {1}      workday: {2}     remain: {3}      표준진도율: {4} " + "\n", cdvDate.Text, lblToday.Text.ToString(), lblLastDay.Text.ToString(), lblRemain.Text.ToString(), lblJindo.Text.ToString());                

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
        }

        /// <summary>
        /// 7. 상단 Lebel 표시
        /// </summary>
        private void LabelTextChange()
        {
            int remain = 0;

            //string getStartDate = cdvDate.Value.ToString("yyyyMM") + "01";
            DateTime getStartDate = Convert.ToDateTime(cdvDate.Value.ToString("yyyy-MM") + "-01");

            string getEndDate = getStartDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");
            //string getDate = cdvDate.Value.ToString("yyyy-MM-dd");

            string strDate = cdvDate.Value.ToString("yyyyMMdd");

            string selectday = strDate.Substring(6, 2);
            string lastday = getEndDate.Substring(6, 2);

            double jindo = (Convert.ToDouble(selectday)) / Convert.ToDouble(lastday) * 100;

            // 진도율 소수점 1째자리 까지 표시 (2009.08.17 임종우)
            jindoPer = Math.Round(Convert.ToDecimal(jindo),1);

            lblToday.Text = selectday + " day";
            lblLastDay.Text = lastday + " day";

            // 금일 조회일 경우 잔여일에 금일 포함함.
            if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
            {
                remain = (Convert.ToInt32(lastday) - Convert.ToInt32(selectday) + 1);
            }
            else
            {
                remain = (Convert.ToInt32(lastday) - Convert.ToInt32(selectday));
            }

            lblRemain.Text = remain.ToString() + " day";             
            lblJindo.Text = jindoPer.ToString() + "%";

        }
        #endregion
    }       
}
