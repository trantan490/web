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
    public partial class PRD010803 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010803<br/>
        /// 클래스요약: 제품 기준 정보 조회<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2013-01-10<br/>
        /// 상세  설명: 제품 기준 정보 조회 화면<br/>
        /// 변경  내용: <br/>     
        /// 2013-02-22-임종우 : 생산관리 카드 셋업 데이터 표시 요청 (민재훈 요청)
        /// 2014-12-12-임종우 : 기준정보 정리 및 추가 (이승희 요청)
        /// 2014-12-24-임종우 : 그룹 순서 변경 (임태성K 요청)
        /// 2015-01-21-임종우 : PKG SAW BOM 정보 추가 (이승희 요청)
        /// 2015-04-10-임종우 : Wire Count 정보 생산관리 T-Cart정보 ->  공정별 T-Card 정보로 변경
        /// 2016-07-19-임종우 : WBL Tape, LM Tape 분리하여 표시 (이승희 요청)
        /// </summary>
        /// 
        public PRD010803()
        {
            InitializeComponent();                       
            
            SortInit();
            GridColumnInit();            
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        ///
        /// </summary>        
        private Boolean CheckField()
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            if (cdvMatType.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD003", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        /// <summary 2. 헤더 생성> 
        ///
        /// </summary>
        private void GridColumnInit()
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            
            try
            {
                spdData.RPT_ColumnInit();

                if (cdvFactory.Text.Trim() != "HMKB1")
                {
                    spdData.RPT_AddBasicColumn("PRODUCT", 0, 0, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("CUSTOMER", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("FAMILY", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PACKAGE", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("TYPE1", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("TYPE2", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("LD COUNT", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("DENSITY", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("GENERATION", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("MAJOR CODE", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("PACKAGE2", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);

                    spdData.RPT_AddBasicColumn("MAT DESC", 0, 11, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("MAT TYPE", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 40);
                    spdData.RPT_AddBasicColumn("Plating method", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LEAD FREE", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("HALOGEN FREE", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("STD_MG QTY", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("STD_SP QTY", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("MA RULE", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("CUSTOMER DEVICE", 0, 19, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SALES CODE", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("BODY SIZE", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PIN TYPE", 0, 22, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PACK_CODE", 0, 23, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("EMP NO", 0, 24, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("NET DIE", 0, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("WF INCH", 0, 26, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("FIRST FLOW", 0, 27, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LAST FLOW", 0, 28, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("ERP CODE(BG)", 0, 29, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("ERP CODE(A/T)", 0, 30, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LF Material", 0, 31, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("BD Diagram", 0, 32, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("MK Diagram", 0, 33, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("GW_1", 0, 34, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("CAP_1", 0, 35, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("EMC_1", 0, 36, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SB", 0, 37, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("TRAY", 0, 38, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LM_1", 0, 39, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("BG_1", 0, 40, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("WM_TYPE1", 0, 41, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("BLADE", 0, 42, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("CUT METHOD", 0, 43, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PMC_TIME", 0, 44, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PCB/LF", 0, 45, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("WIRE", 0, 46, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LM", 0, 47, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("WBL", 0, 48, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("EPOXY", 0, 49, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("TRAY", 0, 50, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("EMC", 0, 51, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SOLDER BALL", 0, 52, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PKG SAW BLADE", 0, 53, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Unit/Strip", 0, 54, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Polishing", 0, 55, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("GDP", 0, 56, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("NCH", 0, 57, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Surfactants", 0, 58, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("WIRE COUNT", 0, 59, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Create Date", 0, 60, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);

                    //2020-05-28-이희석 : Assy일경우 Type 보이게
                    if (this.cdvFactory.Text.Trim() == GlobalVariable.gsAssyDefaultFactory)
                    {
                        spdData.RPT_AddBasicColumn("WF_TYPE", 0, 61, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    }
                   
                    
                    
                }
                else
                {
                    spdData.RPT_AddBasicColumn("PRODUCT", 0, 0, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("CUSTOMER", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("BUMP TYPE", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("PROCESS FLOW", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("LAYER", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("PKG TYPE", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RDL PLATING", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("FINAL BUMP", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("SUB. MATERIAL", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 110);
                    spdData.RPT_AddBasicColumn("SIZE", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("THICKNESS", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("FLAT TYPE", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("WAFER", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);

                    spdData.RPT_AddBasicColumn("BUMP HEIGHT", 0, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("BUMP SIZE", 0, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("BUMP SHAPE", 0, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("MIN. BUMP PITCH", 0, 16, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("MAT. REQ", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("BALL DROP", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("RCF MAT.", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("RCF THICKNESS", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("PSV MAT.", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("PSV THICKNESS", 0, 22, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("SEED MAT.", 0, 23, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SEED THICKNESS", 0, 24, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("RDL MAT.", 0, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RDL THICKNESS", 0, 26, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("FAB SITE", 0, 27, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("FAB TECH", 0, 28, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("FAB PASS MAT", 0, 29, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
                    spdData.RPT_AddBasicColumn("FAB PASS THKCK", 0, 30, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("FAB PI MAT", 0, 31, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("FAB PI THICK", 0, 32, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
                    spdData.RPT_AddBasicColumn("WAFER TEST", 0, 33, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("BACKGRINDING", 0, 34, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
                    spdData.RPT_AddBasicColumn("BACKSIDE PROTECT", 0, 35, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                    spdData.RPT_AddBasicColumn("LASER MARK", 0, 36, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("SAWING", 0, 37, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("T&R", 0, 38, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("END USER", 0, 39, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                }

                // Group항목이 있을경우 반드시 선언해줄것.
                spdData.RPT_ColumnConfigFromTable_New(btnSort);
                //spdData.RPT_ColumnConfigFromTable(btnSort);
                //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
            }

            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        /// <summary 3. SortInit>
        /// 
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();

            if (cdvFactory.Text.Trim() != "HMKB1")
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.MAT_ID", "A.MAT_ID", "A.MAT_ID AS PRODUCT", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT_GRP_1", "A.MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT_GRP_2", "A.MAT_GRP_2", "MAT_GRP_2 AS FAMILY", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT_GRP_3", "A.MAT_GRP_3", "MAT_GRP_3 AS PKG", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT_GRP_4", "A.MAT_GRP_4", "MAT_GRP_4 AS TYPE1", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT_GRP_5", "A.MAT_GRP_5", "MAT_GRP_5 AS TYPE2", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT_GRP_6", "A.MAT_GRP_6", "MAT_GRP_6 AS LEAD_COUNT", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT_GRP_7", "A.MAT_GRP_7", "MAT_GRP_7 AS DENSITY", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT_GRP_8", "A.MAT_GRP_8", "MAT_GRP_8 AS GENERATION", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "MAT_GRP_9", "A.MAT_GRP_9", "MAT_GRP_9 AS MAJOR_CODE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE2", "MAT_GRP_10", "A.MAT_GRP_10", "MAT_GRP_10 AS PACKAGE2", true);
            }
            else
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.MAT_ID", "A.MAT_ID AS PRODUCT", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "A.MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("BUMPING TYPE", "A.MAT_GRP_2", "A.MAT_GRP_2 AS BUMPING_TYPE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PROCESS FLOW", "A.MAT_GRP_3", "A.MAT_GRP_3 AS PROCESS_FLOW", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "A.MAT_GRP_4", "A.MAT_GRP_4 AS LAYER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG TYPE", "A.MAT_GRP_5", "A.MAT_GRP_5 AS PKG_TYPE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL PLATING", "A.MAT_GRP_6", "A.MAT_GRP_6 AS RDL_PLATING", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FINAL BUMP", "A.MAT_GRP_7", "A.MAT_GRP_7 AS FINAL_BUMP", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SUB. MATERIAL", "A.MAT_GRP_8", "A.MAT_GRP_8 AS SUB_MATERIAL", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SIZE", "A.MAT_CMF_14", "A.MAT_CMF_14 AS WF_SIZE", true); // \"SIZE\"
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("THICKNESS", "A.MAT_CMF_2", "A.MAT_CMF_2 AS THICKNESS", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FLAT TYPE", "A.MAT_CMF_3", "A.MAT_CMF_3 AS FLAT_TYPE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("WAFER ORIENTATION", "A.MAT_CMF_4", "A.MAT_CMF_4 AS WAFER_ORIENTATION", true);
            }
        }
        #endregion


        #region SQL 쿼리 Build
        /// <summary 4. SQL 쿼리 Build>
        /// 
        /// </summary>
        /// <returns> strSqlString </returns>

        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            if (cdvFactory.Text.Trim() != "HMKB1")
            {
                //strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond3);
                //strSqlString.Append("     , MAT_DESC, MAT_TYPE " + "\n");
                //strSqlString.Append("     , MAT_CMF_1 AS PLATING, MAT_CMF_2 AS LEAD_FREE, MAT_CMF_3 AS HALOGEN_FREE, MAT_CMF_4 AS STD_MG_QTY, MAT_CMF_5 AS STD_SP_QTY " + "\n");
                //strSqlString.Append("     , MAT_CMF_6 AS MA_RULE, MAT_CMF_7 AS CUSTOMER_DEVICE, MAT_CMF_8 AS SALES_CODE, MAT_CMF_9 AS BODY_SIZE, MAT_CMF_10 AS PIN_TYPE " + "\n");
                //strSqlString.Append("     , MAT_CMF_11 AS PACK_CODE, MAT_CMF_12 AS EMP_NO, MAT_CMF_13 AS NET_DIE, MAT_CMF_14 AS WF_INCH, MAT_CMF_15 AS LAST_SHIPPED " + "\n");
                //strSqlString.Append("     , FIRST_FLOW, LAST_FLOW, VENDOR_MAT_ID AS ERP_CODE_BG, BASE_MAT_ID AS ERP_CODE_ASSY_TEST " + "\n");
                //strSqlString.Append("     , (SELECT USER_DESC FROM RWEBUSRDEF WHERE USER_ID = A.CREATE_USER_ID) || '(' || CREATE_USER_ID || ')' AS CREATE_USER " + "\n");
                //strSqlString.Append("     , CREATE_TIME " + "\n");
                //strSqlString.Append("     , DECODE(UPDATE_USER_ID, ' ', ' ', (SELECT USER_DESC FROM RWEBUSRDEF WHERE USER_ID = A.UPDATE_USER_ID) || '(' || UPDATE_USER_ID || ')') AS UPDATE_USER " + "\n");
                //strSqlString.Append("     , UPDATE_TIME " + "\n");
                //strSqlString.Append("     , UNIT " + "\n");
                //strSqlString.Append("     , BD_DIAGRAM " + "\n");
                //strSqlString.Append("     , BG_1 " + "\n");
                //strSqlString.Append("     , ADH_8 AS POLISHING " + "\n");
                //strSqlString.Append("     , ADH_6 AS NCH " + "\n");
                //strSqlString.Append("     , ADH_6 AS GDP " + "\n");

                strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond3);
                strSqlString.Append("     , MAT_DESC, MAT_TYPE " + "\n");
                strSqlString.Append("     , MAT_CMF_1 AS PLATING, MAT_CMF_2 AS LEAD_FREE, MAT_CMF_3 AS HALOGEN_FREE, MAT_CMF_4 AS STD_MG_QTY, MAT_CMF_5 AS STD_SP_QTY " + "\n");
                strSqlString.Append("     , MAT_CMF_6 AS MA_RULE, MAT_CMF_7 AS CUSTOMER_DEVICE, MAT_CMF_8 AS SALES_CODE, MAT_CMF_9 AS BODY_SIZE, MAT_CMF_10 AS PIN_TYPE " + "\n");
                strSqlString.Append("     , MAT_CMF_11 AS PACK_CODE, MAT_CMF_12 AS EMP_NO, MAT_CMF_13 AS NET_DIE, MAT_CMF_14 AS WF_INCH " + "\n");
                strSqlString.Append("     , FIRST_FLOW, LAST_FLOW, VENDOR_MAT_ID AS ERP_CODE_BG, BASE_MAT_ID AS ERP_CODE_ASSY_TEST " + "\n");
                strSqlString.Append("     , LF_MATERIAL, BD_DIAGRAM, MK_DIAGRAM " + "\n");
                strSqlString.Append("     , GW_1, CAP_1, EMC_1, SB, TRAY, LM_1, BG_1, WM_TYPE1, BLADE_1 " + "\n");
                strSqlString.Append("     , RESV_FIELD_1 AS CUT_METHOD, BLADE_8 AS PMC_TIME " + "\n");
                strSqlString.Append("     , BOM_LF_PB, BOM_GW_CW, BOM_LM, BOM_WBL, BOM_AE, BOM_NT, BOM_MC, BOM_SB, BOM_PKG_SAW " + "\n");
                strSqlString.Append("     , UNIT, ADH_8 AS POLISHING, ADH_6 AS GDP, ADH_5 AS NCH, ADH_9 AS GEMAN " + "\n");
                strSqlString.Append("     , (SELECT SUM(DECODE(TCD_CMF_2, ' ', 0, TCD_CMF_2)) FROM CWIPTCDDEF@RPTTOMES WHERE FACTORY = A.FACTORY AND MAT_ID = A.MAT_ID) AS WIRE_COUNT " + "\n");
                strSqlString.Append("     , TO_CHAR(TO_DATE(A.CREATE_TIME,'YYYYMMDDHH24MISS'),'YYYY-MM-DD') AS CREATE_TIME" + "\n");
                if (cdvFactory.Text.Trim() == GlobalVariable.gsAssyDefaultFactory)
                {
                    strSqlString.Append("     , MAT_CMF_16 AS WF_TYPE" + "\n");
                }
                strSqlString.Append("  FROM MWIPMATDEF A " + "\n");
                strSqlString.Append("     , CLOTCRDDAT@RPTTOMES B " + "\n");
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT MAT_ID, MAX(UNIT) AS UNIT " + "\n");
                strSqlString.Append("          FROM ( " + "\n");
                strSqlString.Append("                SELECT DISTINCT B.PARTNUMBER AS MAT_ID, A.C_MAT_CMF_4 AS UNIT " + "\n");
                strSqlString.Append("                  FROM CWIPMATDEF@RPTTOMES A " + "\n");
                strSqlString.Append("                     , CWIPBOMDEF B " + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
                strSqlString.Append("                   AND A.MAT_ID = B.MATCODE " + "\n");
                strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND B.RESV_FIELD_2 IN ('LF','PB') " + "\n");
                strSqlString.Append("                   AND B.RESV_FLAG_1 = 'Y' " + "\n");
                strSqlString.Append("                   AND A.C_MAT_CMF_4 <> ' ' " + "\n");
                strSqlString.Append("               ) " + "\n");
                strSqlString.Append("         GROUP BY MAT_ID " + "\n");
                strSqlString.Append("       ) C" + "\n");
                strSqlString.Append("     , ( " + "\n");
                strSqlString.Append("        SELECT MAT_ID " + "\n");
                strSqlString.Append("             , MAX(CASE WHEN RESV_FIELD_2 IN ('LF', 'PB') THEN MATCODE ELSE ' ' END) BOM_LF_PB " + "\n");
                strSqlString.Append("             , MAX(CASE WHEN RESV_FIELD_2 IN ('GW', 'CW') THEN MATCODE ELSE ' ' END) BOM_GW_CW " + "\n");
                strSqlString.Append("             , MAX(CASE WHEN RESV_FIELD_2 = 'LM' THEN MATCODE ELSE ' ' END) BOM_LM " + "\n");
                strSqlString.Append("             , MAX(CASE WHEN RESV_FIELD_2 = 'WBL' THEN MATCODE ELSE ' ' END) BOM_WBL " + "\n");
                strSqlString.Append("             , MAX(CASE WHEN RESV_FIELD_2 = 'AE' THEN MATCODE ELSE ' ' END) BOM_AE " + "\n");
                strSqlString.Append("             , MAX(CASE WHEN RESV_FIELD_2 = 'NT' THEN MATCODE ELSE ' ' END) BOM_NT " + "\n");
                strSqlString.Append("             , MAX(CASE WHEN RESV_FIELD_2 = 'MC' THEN MATCODE ELSE ' ' END) BOM_MC " + "\n");
                strSqlString.Append("             , MAX(CASE WHEN RESV_FIELD_2 = 'SB' THEN MATCODE ELSE ' ' END) BOM_SB " + "\n");
                strSqlString.Append("             , MAX(CASE WHEN RESV_FIELD_2 = 'BD' THEN MATCODE ELSE ' ' END) BOM_PKG_SAW " + "\n");
                strSqlString.Append("          FROM ( " + "\n");
                strSqlString.Append("                SELECT MAT_ID, RESV_FIELD_2 " + "\n");
                strSqlString.Append("                     , MAX(MATCODE) KEEP(DENSE_RANK FIRST ORDER BY CREATE_DT DESC) MATCODE " + "\n");
                strSqlString.Append("                  FROM ( " + "\n");
                strSqlString.Append("                        SELECT PARTNUMBER AS MAT_ID, MATCODE" + "\n");
                strSqlString.Append("                             , CASE WHEN RESV_FIELD_2 = 'TE' AND STEPID = 'A0020' THEN 'LM'" + "\n");
                strSqlString.Append("                                    WHEN RESV_FIELD_2 = 'TE' AND STEPID = 'A0040' THEN 'WBL'" + "\n");
                strSqlString.Append("                                    ELSE RESV_FIELD_2" + "\n");
                strSqlString.Append("                               END AS RESV_FIELD_2" + "\n");
                strSqlString.Append("                             , CREATE_DT " + "\n");
                strSqlString.Append("                          FROM CWIPBOMDEF " + "\n");
                strSqlString.Append("                         WHERE 1=1 " + "\n");
                strSqlString.Append("                           AND RESV_FLAG_1 = 'Y' " + "\n");
                strSqlString.Append("                           AND (RESV_FIELD_2 IN ('LF', 'PB', 'GW', 'CW', 'TE', 'AE', 'NT', 'MC', 'SB') OR (RESV_FIELD_2 = 'BD' AND STEPID = 'A1750'))" + "\n");                
                strSqlString.Append("                       ) " + "\n");
                strSqlString.Append("                 GROUP BY MAT_ID, RESV_FIELD_2 " + "\n");
                strSqlString.Append("               ) " + "\n");
                strSqlString.Append("         GROUP BY MAT_ID " + "\n");
                strSqlString.Append("       ) D" + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND A.FACTORY = B.FACTORY(+)" + "\n");
                strSqlString.Append("   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
                strSqlString.Append("   AND A.MAT_ID = C.MAT_ID(+)" + "\n");
                strSqlString.Append("   AND A.MAT_ID = D.MAT_ID(+)" + "\n");
                strSqlString.Append("   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("   AND A.DELETE_FLAG = ' ' " + "\n");
                strSqlString.Append("   AND A.MAT_ID LIKE '" + txtMatID.Text.Trim() + "' " + "\n");
                strSqlString.AppendFormat("   AND A.MAT_TYPE = '" + cdvMatType.Text + "'" + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("   AND MAT_GRP_2 {0}" + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("   AND MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("   AND MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("   AND MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("   AND MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("   AND MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("   AND MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("   AND MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                #endregion

                strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond2);
            }
            else
            {
                strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond3);
                strSqlString.Append("     , MAT_INFO_01, MAT_INFO_02, MAT_INFO_03, MAT_INFO_04, MAT_INFO_05, MAT_INFO_06, MAT_INFO_07, MAT_INFO_08, MAT_INFO_09, MAT_INFO_10 " + "\n");
                strSqlString.Append("     , MAT_INFO_11, MAT_INFO_12, MAT_INFO_13, MAT_INFO_14 " + "\n");
                strSqlString.Append("     , MAT_FAB_01, MAT_FAB_02, MAT_FAB_03, MAT_FAB_04, MAT_FAB_05, MAT_FAB_06 " + "\n");
                strSqlString.Append("     , MAT_FLOW_01, MAT_FLOW_02, MAT_FLOW_03, MAT_FLOW_04, MAT_FLOW_05, MAT_FLOW_06, MAT_FLOW_07 " + "\n");
                strSqlString.Append("  FROM MWIPMATDEF A " + "\n");
                strSqlString.Append("     , CWIPMATATR@RPTTOMES B " + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND A.FACTORY = B.FACTORY" + "\n");
                strSqlString.Append("   AND A.MAT_ID = B.MAT_ID" + "\n");
                strSqlString.Append("   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("   AND A.DELETE_FLAG = ' ' " + "\n");
                strSqlString.Append("   AND A.MAT_ID LIKE '" + txtMatID.Text.Trim() + "' " + "\n");
                strSqlString.AppendFormat("   AND A.MAT_TYPE = '" + cdvMatType.Text + "'" + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND A.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                    strSqlString.AppendFormat("   AND A.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                    strSqlString.AppendFormat("   AND A.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                    strSqlString.AppendFormat("   AND A.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                    strSqlString.AppendFormat("   AND A.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                    strSqlString.AppendFormat("   AND A.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                    strSqlString.AppendFormat("   AND A.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                    strSqlString.AppendFormat("   AND A.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                    strSqlString.AppendFormat("   AND A.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                    strSqlString.AppendFormat("   AND A.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                    strSqlString.AppendFormat("   AND A.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                    strSqlString.AppendFormat("   AND A.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                #endregion

                strSqlString.Append(" ORDER BY A.MAT_ID " + "\n");
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion


        #region EVENT 처리
        /// <summary 5. View>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (CheckField() == false) return;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                this.Refresh();                               

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

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);
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



        /// <summary 6. Excel Export>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();
        }
        #endregion

        private void cdvMatType_ValueButtonPress(object sender, EventArgs e)
        {
            StringBuilder strQuery = new StringBuilder();

            strQuery.Append("SELECT KEY_1 AS Code, DATA_1 AS Data " + "\n");
            strQuery.Append("  FROM MGCMTBLDAT " + "\n");
            if(cdvFactory.Text.Trim() != "")
                strQuery.AppendFormat(" WHERE FACTORY= '{0}'\n", cdvFactory.Text.Trim());
            else
                strQuery.Append(" WHERE FACTORY= '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strQuery.Append("   AND TABLE_NAME = 'MATERIAL_TYPE' " + "\n");
            strQuery.Append("   AND KEY_1 <> 'ALL' " + "\n");
            strQuery.Append(" ORDER BY KEY_1 " + "\n");

            cdvMatType.sDynamicQuery = strQuery.ToString();

        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                BaseFormType = eBaseFormType.BUMP_BASE;
                pnlBUMPDetail.Visible = false;
            }
            else
            {
                BaseFormType = eBaseFormType.WIP_BASE;
                pnlWIPDetail.Visible = false;
            }

            SortInit();     //add 150529
        }

    }
}
