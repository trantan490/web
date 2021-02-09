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
    public partial class BUM010202 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: BUM010202<br/>
        /// 클래스요약: BUMP Lot 직행율<br/>
        /// 작  성  자: 임종우<br/>
        /// 최초작성일: 2016-04-27<br/>
        /// 상세  설명: BUMP Lot 직행율(이병길K 요청)<br/>
        /// 변경  내용: <br/>    
        /// 2018-01-08-임종우 : SubTotal, GrandTotal 평균값 구하기 Function 변경
        /// </summary>
        public BUM010202()
        {
            InitializeComponent();                                   
            SortInit();
            cdvFromToDate.AutoBinding();
            cdvFromToDate.DaySelector.Visible = false;
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
            
            try
            {
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Layer", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Bumping Type", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Operation Flow", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);                
                spdData.RPT_AddBasicColumn("PKG Type", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("RDL Plating", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Final Bump", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Sub. Material", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Size", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Thickness", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Flat Type", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Wafer Orientation", 0, 11, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Product", 0, 12, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);

                spdData.RPT_AddBasicColumn("Invoice No", 0, 13, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Ship Site", 0, 14, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Lot Id", 0, 15, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("SHIP", 0, 16, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Daily standard", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("WF Ship Qty", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("In Qty", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Ship Qty", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Yield", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("HOLD Count", 0, 23, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                                                
                spdData.RPT_AddBasicColumn("HMK3B", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PACKING", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("OGI", 0, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("AVI", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("SORT", 0, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("FINAL_INSP", 0, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                spdData.RPT_AddBasicColumn("BUMP", 0, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("REFLOW", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("BALL_MOUNT", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("ETCH", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("SNAG_PLAT", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("CU_PLAT", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PHOTO", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("SPUTTER", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_MerageHeaderColumnSpan(0, 30, 7);   //BUMP                

                spdData.RPT_AddBasicColumn("PSV3", 0, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PHOTO", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);                

                spdData.RPT_AddBasicColumn("RDL3", 0, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("ETCH", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PLAT", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PHOTO", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("SPUTTER", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);                
                spdData.RPT_MerageHeaderColumnSpan(0, 38, 4);   //RDL3

                spdData.RPT_AddBasicColumn("PSV2", 0, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PHOTO", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);                

                spdData.RPT_AddBasicColumn("RDL2", 0, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("ETCH", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PLAT", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PHOTO", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("SPUTTER", 1, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_MerageHeaderColumnSpan(0, 43, 4);   //RDL2

                spdData.RPT_AddBasicColumn("PSV1", 0, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PHOTO", 1, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);                

                spdData.RPT_AddBasicColumn("RDL1", 0, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("ETCH", 1, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PLAT", 1, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PHOTO", 1, 50, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("SPUTTER", 1, 51, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_MerageHeaderColumnSpan(0, 48, 4);   //RDL1

                spdData.RPT_AddBasicColumn("RCF", 0, 52, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PHOTO", 1, 52, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);                

                spdData.RPT_AddBasicColumn("I-STOCK", 0, 53, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("IQC", 0, 54, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                for (int i = 0; i <= 29; i++)
                {
                    spdData.RPT_MerageHeaderRowSpan(0, i, 2);
                }

                spdData.RPT_MerageHeaderRowSpan(0, 53, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 54, 2);                
               
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "(SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = 'HMKB1' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.CUSTOMER_ID) AS CUSTOMER", "MAT.CUSTOMER_ID", "CUSTOMER_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "MAT.MAT_GRP_03 AS LAYER", "MAT.MAT_GRP_03", "MAT_GRP_03", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("BUMPING TYPE", "MAT.MAT_GRP_01 AS BUMPING_TYPE", "MAT.MAT_GRP_01", "MAT_GRP_01", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PROCESS FLOW", "MAT.MAT_GRP_02 AS PROCESS_FLOW", "MAT.MAT_GRP_02", "MAT_GRP_02", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG TYPE", "MAT.MAT_GRP_04 AS PKG_TYPE", "MAT.MAT_GRP_04", "MAT_GRP_04", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL PLATING", "MAT.MAT_GRP_05 AS RDL_PLATING", "MAT.MAT_GRP_05", "MAT_GRP_05", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FINAL BUMP", "MAT.MAT_GRP_06 AS FINAL_BUMP", "MAT.MAT_GRP_06", "MAT_GRP_06", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SUB. MATERIAL", "MAT.MAT_GRP_07 AS SUB_MATERIAL", "MAT.MAT_GRP_07", "MAT_GRP_07", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SIZE", "MAT.MAT_CMF_01 AS WF_SIZE", "MAT.MAT_CMF_01", "MAT_CMF_01", false); // \"SIZE\"
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("THICKNESS", "MAT.MAT_CMF_02 AS THICKNESS", "MAT.MAT_CMF_02", "MAT_CMF_02", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FLAT TYPE", "MAT.MAT_CMF_03 AS FLAT_TYPE", "MAT.MAT_CMF_03", "MAT_CMF_03", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("WAFER ORIENTATION", "MAT.MAT_CMF_04 AS WAFER_ORIENTATION", "MAT.MAT_CMF_04", "MAT_CMF_04", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT.MAT_ID AS PRODUCT", "MAT.MAT_ID", "MAT_ID", true);       
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
            string sFromDate;
            string sEndDate;
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            sFromDate = cdvFromToDate.ExactFromDate;
            sEndDate = cdvFromToDate.ExactToDate;
            
            strSqlString.Append("WITH DT AS" + "\n");
            strSqlString.Append("(" + "\n");
            strSqlString.Append(" SELECT MAT_ID, INVOICE_NO, LOT_CMF_11 AS SHIP_SITE, LOT_ID, TRAN_TIME, WORK_DATE, LOT_CMF_5 AS LOT_TYPE, SHIP_QTY_1, SHIP_QTY_2" + "\n");
            strSqlString.Append("      , (SELECT SUM(LOSS_QTY) FROM RWIPLOTLSM WHERE FACTORY = A.FROM_FACTORY AND LOT_ID = A.LOT_ID AND HIST_DEL_FLAG = ' ') AS LOSS_QTY" + "\n");
            strSqlString.Append("   FROM VWIPSHPLOT A" + "\n");
            strSqlString.Append("  WHERE 1=1" + "\n");
            strSqlString.Append("    AND FROM_FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("    AND TRAN_TIME BETWEEN '" + sFromDate + "' AND '" + sEndDate + "'" + "\n");

            if (cdvType.Text != "ALL")
            {
                strSqlString.Append("    AND LOT_CMF_5 LIKE '" + cdvType.Text + "' " + "\n");
            }

            strSqlString.Append(")" + "\n");
            strSqlString.Append("SELECT " + QueryCond1 + "\n");            
            strSqlString.Append("     , SHP.INVOICE_NO" + "\n");
            strSqlString.Append("     , SHP.SHIP_SITE" + "\n");
            strSqlString.Append("     , SHP.LOT_ID" + "\n");
            strSqlString.Append("     , SHP.TRAN_TIME" + "\n");
            strSqlString.Append("     , SHP.WORK_DATE" + "\n");
            strSqlString.Append("     , SHP.LOT_TYPE" + "\n");
            strSqlString.Append("     , SHP.SHIP_QTY_1" + "\n");
            strSqlString.Append("     , NVL(SHP.SHIP_QTY_2,0) + NVL(SHP.LOSS_QTY,0) AS IN_QTY" + "\n");
            strSqlString.Append("     , SHP.SHIP_QTY_2" + "\n");
            strSqlString.Append("     , ROUND(NVL(SHP.SHIP_QTY_2,0) / (NVL(SHP.SHIP_QTY_2,0) + NVL(SHP.LOSS_QTY,0)) * 100, 1) AS YIELD" + "\n");            
            strSqlString.Append("     , HLD.TTL_HOLD_CNT" + "\n");
            strSqlString.Append("     , HLD.HMK3B, HLD.PACKING, HLD.OGI, HLD.AVI, HLD.SORT, HLD.FINAL_INSP" + "\n");
            strSqlString.Append("     , HLD.BUMP_REFLOW, HLD.BUMP_BALL_MOUNT, HLD.BUMP_ETCH, HLD.BUMP_SNAG_PLAT, HLD.BUMP_CU_PLAT, HLD.BUMP_PHOTO, HLD.BUMP_SPUTTER" + "\n");
            strSqlString.Append("     , HLD.PSV3_PHOTO, HLD.RDL3_ETCH, HLD.RDL3_PLAT, HLD.RDL3_PHOTO, HLD.RDL3_SPUTTER" + "\n");
            strSqlString.Append("     , HLD.PSV2_PHOTO, HLD.RDL2_ETCH, HLD.RDL2_PLAT, HLD.RDL2_PHOTO, HLD.RDL2_SPUTTER" + "\n");
            strSqlString.Append("     , HLD.PSV1_PHOTO, HLD.RDL1_ETCH, HLD.RDL1_PLAT, HLD.RDL1_PHOTO, HLD.RDL1_SPUTTER" + "\n");
            strSqlString.Append("     , HLD.RCF_PHOTO, HLD.I_STOCK, HLD.IQC" + "\n");
            strSqlString.Append("  FROM CWIPMATINF@RPTTOMES MAT" + "\n");
            strSqlString.Append("     , DT SHP" + "\n");            
            strSqlString.Append("     , (   " + "\n");
            strSqlString.Append("        SELECT LOT_ID" + "\n");
            strSqlString.Append("             , COUNT(LOT_ID) AS TTL_HOLD_CNT" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'HMK3B', CNT, 0)) AS HMK3B" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PACKING', CNT, 0)) AS PACKING" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'OGI', CNT, 0)) AS OGI " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'AVI', CNT, 0)) AS AVI" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'SORT', CNT, 0)) AS SORT" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'FINAL_INSP', CNT, 0)) AS FINAL_INSP" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BUMP_REFLOW', CNT, 0)) AS BUMP_REFLOW" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BUMP_BALL_MOUNT', CNT, 0)) AS BUMP_BALL_MOUNT" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BUMP_ETCH', CNT, 0)) AS BUMP_ETCH" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BUMP_SNAG_PLAT', CNT, 0)) AS BUMP_SNAG_PLAT" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BUMP_CU_PLAT', CNT, 0)) AS BUMP_CU_PLAT" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BUMP_PHOTO', CNT, 0)) AS BUMP_PHOTO" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'BUMP_SPUTTER', CNT, 0)) AS BUMP_SPUTTER" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PSV3_PHOTO', CNT, 0)) AS PSV3_PHOTO" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL3_ETCH', CNT, 0)) AS RDL3_ETCH" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL3_PLAT', CNT, 0)) AS RDL3_PLAT             " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL3_PHOTO', CNT, 0)) AS RDL3_PHOTO             " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL3_SPUTTER', CNT, 0)) AS RDL3_SPUTTER" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PSV2_PHOTO', CNT, 0)) AS PSV2_PHOTO" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL2_ETCH', CNT, 0)) AS RDL2_ETCH" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL2_PLAT', CNT, 0)) AS RDL2_PLAT" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL2_PHOTO', CNT, 0)) AS RDL2_PHOTO" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL2_SPUTTER', CNT, 0)) AS RDL2_SPUTTER" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'PSV1_PHOTO', CNT, 0)) AS PSV1_PHOTO" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL1_ETCH', CNT, 0)) AS RDL1_ETCH" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL1_PLAT', CNT, 0)) AS RDL1_PLAT" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL1_PHOTO', CNT, 0)) AS RDL1_PHOTO" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RDL1_SPUTTER', CNT, 0)) AS RDL1_SPUTTER" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'RCF_PHOTO', CNT, 0)) AS RCF_PHOTO" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'I-STOCK', CNT, 0)) AS I_STOCK" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP_1, 'IQC', CNT, 0)) AS IQC      " + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT A.LOT_ID, B.OPER_GRP_1, COUNT(*) AS CNT " + "\n");
            strSqlString.Append("                  FROM RWIPLOTHLD A" + "\n");
            strSqlString.Append("                     , MWIPOPRDEF B" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                  AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                  AND A.OPER = B.OPER          " + "\n");
            strSqlString.Append("                  AND A.FACTORY = 'HMKB1'" + "\n");
            strSqlString.Append("                  AND B.OPER_GRP_1 <> 'HMK2B'" + "\n");
            strSqlString.Append("                  AND A.LOT_ID IN (SELECT LOT_ID FROM DT)" + "\n");

            if (cdvHoldCode.Text != "ALL" && cdvHoldCode.Text != "")
            {
                strSqlString.AppendFormat("                  AND A.HOLD_CODE {0}" + "\n", cdvHoldCode.SelectedValueToQueryString);
            }

            strSqlString.Append("                GROUP BY A.LOT_ID, B.OPER_GRP_1" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY LOT_ID" + "\n");
            strSqlString.Append("       ) HLD" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = SHP.MAT_ID" + "\n");
            strSqlString.Append("   AND SHP.LOT_ID = HLD.LOT_ID(+)" + "\n");

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

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 19, null, null, btnSort);                
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함
             
                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 19, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);

                spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 22, nGroupCount, false);
                
                SetPerVertical(23, 54); // 시작컬럼, 종료컬럼

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
    
        public void SetPerVertical(int nStartColPos, int nEndColPos)
        {         
            double divide = 0;
            double totalDivide = 0;
            double subDivide = 0;

            double[] sumArray = new double[nEndColPos - nStartColPos + 1];
            double[] subSumArray = new double[nEndColPos - nStartColPos + 1];
            double[] totalSumArray = new double[nEndColPos - nStartColPos + 1];

            int checkValue = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(1);
                        
            Color checkValueColor = spdData.ActiveSheet.Cells[1, checkValue].BackColor; // 첫번째 그룹의 첫 열 색 (기준색)
                        
            for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                int x = 0;                

                if (spdData.ActiveSheet.Cells[i, checkValue].BackColor == checkValueColor) //subTotal Sum 구하기 위함.
                {
                    Color color = spdData.ActiveSheet.Cells[1, nStartColPos].BackColor; // 각 컬럼의 색이 동일하기에 대표로 시작컬럼 색을 기준으로 함.

                    if (spdData.ActiveSheet.Cells[i, nStartColPos].BackColor == color)
                    {
                        for (int y = nStartColPos; y <= nEndColPos; y++)
                        {
                            if (Convert.ToDouble(spdData.ActiveSheet.Cells[i, y].Value) > 0)
                            {
                                sumArray[x] += 1;
                            }

                            x += 1;
                        }

                        divide += 1;
                    }
                    else
                    {   
                        for (int y = nStartColPos; y <= nEndColPos; y++)
                        {
                            spdData.ActiveSheet.Cells[i, y].Value = (divide - sumArray[x]) / divide * 100;

                            subSumArray[x] += sumArray[x];
                            totalSumArray[x] += sumArray[x];

                            sumArray[x] = 0;                           

                            x += 1;
                        }

                        subDivide += divide;
                        totalDivide += divide;

                        divide = 0;

                    }                       
                }
                else
                {                    
                    for (int y = nStartColPos; y <= nEndColPos; y++)
                    {
                        spdData.ActiveSheet.Cells[i, y].Value = (subDivide - subSumArray[x]) / subDivide * 100;

                        subSumArray[x] = 0;

                        x += 1;
                    }
                                                            
                    subDivide = 0;
                } 

                               
            }

            // GrandTotal 구함.   
            int xx = 0;

            for (int y = nStartColPos; y <= nEndColPos; y++)
            {
                spdData.ActiveSheet.Cells[0, y].Value = (totalDivide - totalSumArray[xx]) / totalDivide * 100;                

                xx += 1;
            }            

            return;
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
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }
                
        #endregion
    }       
}
